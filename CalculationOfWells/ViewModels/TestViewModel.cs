using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalculationOfWells.ViewModels
{
    public partial class TestViewModel : ObservableObject
    {
        public TestViewModel()
        {
            Text = "Начальное значение";
            MyCommand = new RelayCommand(() => Text = "Привязка работает!");
        }

        // генерирует свойство string Text и уведомления
        [ObservableProperty]
        private string _text;

        // команда, которая поменяет Text
        public IRelayCommand MyCommand { get; }
    }
}
