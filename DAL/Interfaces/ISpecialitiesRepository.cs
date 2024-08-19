using Models;

namespace DAL.Interfaces;

public interface ISpecialitiesRepository
{
    Task SaveAsync(Speciality obj);
    Task<List<string>> RetrieveSpecialityNamesAsync(List<int> specialityIds);
    Task InsertAsync(Speciality obj);
    Task InsertAsync(string specialityName);
    Task InsertAsync(List<Speciality> objects);
    Task<List<Speciality>> ListAllAsync();
    Task RemoveAsync(int objId);
}