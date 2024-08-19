using Models;

namespace DAL.Interfaces;

public interface ISpecialitiesRepository
{
    Task SaveAsync(Speciality obj);
    Task InsertAsync(Speciality obj);
    Task InsertAsync(List<Speciality> objects);
    Task<List<Speciality>> ListAllAsync();
    Task RemoveAsync(int objId);
}