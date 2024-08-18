using Models;

namespace DAL.Interfaces;

public interface IPersonRepository
{
    Task<List<Person>> ListAllAsync();
    Task<Person?> GetByIdAsync(int personId);
    Person? GetByGmcAsync(int gmcId);
    Task SaveAsync(Person person);
    Task InsertAsync(Person obj);
    Task InsertListAsync(List<Person> people);

}