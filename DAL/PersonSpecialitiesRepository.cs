using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL;

public class PersonSpecialitiesRepository : GenericRepository<PeopleSpecialities>, IPersonSpecialitiesRepository
{
    public PersonSpecialitiesRepository(DoctorDbContext doctorDbContext) :base(doctorDbContext)
    {
       
    }

    /// <summary>
    /// Gets a list of the people ids that match a given speciality id
    /// </summary>
    /// <param name="specialityId">The id of the speciality</param>
    /// <returns>List of people with that speciality</returns>
    public async Task<List<int>> GetAllPeopleAsync(int specialityId)
    {
        return await this.dbContext.people_specialities.Where(x => x.SpecialityId == specialityId).Select(x => x.PersonId).ToListAsync();
    }

    /// <summary>
    /// Gets a list of the speciality ids that match a given person id
    /// </summary>
    /// <param name="personId">The id of the person</param>
    /// <returns>List of all specialities that person holds.</returns>
    public async Task<List<int>> GetAllSpecialitiesAsync(int personId)
    {
        return await this.dbContext.people_specialities.Where(x => x.PersonId == personId).Select(x => x.SpecialityId).ToListAsync();
    }
}