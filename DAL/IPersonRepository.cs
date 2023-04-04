using Models;

namespace DAL;

public interface IPersonRepository
{
    Task<List<Person>> ListAllAsync();
    Task<Person> GetByIdAsync(int personId);

    Task<Person> GetByGMCAsync(int GMC);
    Task SaveAsync(Person person);

    Task<int> InsertAsync(Person person);
}