namespace FullStackTechTest.Services
{
    public interface IDataImportService
    {
        Task<bool> LoadJsonDataAsync(string pathToData);
    }
}
