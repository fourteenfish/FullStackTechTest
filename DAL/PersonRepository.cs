using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL;

public class PersonRepository : GenericRepository<Person>, IPersonRepository
{
    public PersonRepository(DoctorDbContext doctorDbContext) : base(doctorDbContext)
    {
    }
    public Person? GetByGmcAsync(int gmcId)
    {
        return this.dbContext.people.FirstOrDefault(x => x.GMC == gmcId);
    }
}