using Models;

namespace DAL.Interfaces;

public interface IPersonSpecialitiesRepository
{
    Task<List<int>> GetAllSpecialitiesAsync(int personId);
    Task<List<int>> GetAllPeopleAsync(int specialityId);
    Task SaveAsync(PeopleSpecialities obj);
    Task InsertAsync(PeopleSpecialities obj);
    Task InsertAsync(List<PeopleSpecialities> objects);
    Task<List<PeopleSpecialities>> ListAllAsync();
    Task RemoveAsync(int objId);
}