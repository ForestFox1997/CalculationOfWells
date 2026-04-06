using Microsoft.Win32;
using CalculationOfWells.Services.Interfaces;

namespace CalculationOfWells.Services
{
    public class FileDialogService : IFileDialogService
    {
        public string? OpenFile(string filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*")
        {
            var dialog = new OpenFileDialog { Filter = filter, Multiselect = false };
            var filePath = dialog.ShowDialog() == true ? dialog.FileName : null;

            return filePath;
        }
    }
}
