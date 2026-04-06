using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CalculationOfWells.Models;
using CalculationOfWells.Services.Interfaces;

namespace CalculationOfWells.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IImportService _importService;
        private readonly IFileDialogService _fileDialog;
        private readonly IValidationService _validation;
        private readonly IAggregationService _aggregation;
        [ObservableProperty] private bool _isBusy;

        public MainViewModel(IImportService importService, IFileDialogService fileDialogService,
            IValidationService validationService, IAggregationService aggregationService)
        {
            _importService = importService;
            _fileDialog = fileDialogService;
            _validation = validationService;
            _aggregation = aggregationService;
            Summaries = [];
            Errors = [];
            ImportWellsCommand = new RelayCommand(async () => await ImportAsync());
            ClearWellCommand = new RelayCommand(async () => await ClearAsync());
        }

        public IRelayCommand ImportWellsCommand { get; }
        public IRelayCommand ClearWellCommand { get; }
        public ObservableCollection<WellSummary> Summaries { get; }
        public ObservableCollection<ValidationError> Errors { get; }

        private async Task ImportAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            try
            {
                var path = _fileDialog.OpenFile();
                if (string.IsNullOrEmpty(path))
                {
                    return;
                }

                var raw = await _importService.ReadAllAsync(path);
                var parseErrors = raw
                    .Where(r => r.ParseError != null)
                    .Select(r => new ValidationError { LineNumber = r.Line, WellId = "", Message = r.ParseError } )
                    .ToList();
                var validRows = raw.Where(r => r.Row != null).Select(r => (r.Line, r.Row!));
                var (validationErrors, wells) = _validation.ValidateAndBuildWells(validRows);
                var summaries = _aggregation.SummarizeAll(wells);
                summaries.ForEach(Summaries.Add);
                parseErrors.ForEach(Errors.Add);
                validationErrors.ForEach(Errors.Add);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private Task ClearAsync()
        {
            if (IsBusy)
            {
                return Task.CompletedTask;
            }

            IsBusy = true;

            Summaries.Clear();
            Errors.Clear();

            IsBusy = false;

            return Task.CompletedTask;
        }
    }
}
