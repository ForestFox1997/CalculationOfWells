namespace CalculationOfWells.Services.Interfaces
{
    /// <summary>
    /// Сервис, открывающий для пользователя представление для выбора файла
    /// </summary>
    public interface IFileDialogService
    {
        /// <summary>
        /// Передаёт фильтр для выбора файла и возвращает путь к файлу в системе
        /// </summary>
        string? OpenFile(string filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*");
    }
}
