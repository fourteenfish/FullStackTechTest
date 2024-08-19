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

    /// <summary>
    /// Updates an Persons Specialities in the database with new parameters.
    /// </summary>
    /// <param name="specialityId">Speciality to update</param>
    /// <param name="personId">Person to update</param>
    /// <returns>Async Task.</returns>
    public async Task SaveAsync(int specialityId, int personId)
    {
        PeopleSpecialities peopleSpecialities = new()
        {
            PersonId = personId,
            SpecialityId = specialityId
        };
        await this.InsertAsync(peopleSpecialities);
    }

    /// <summary>
    /// Updates an Persons Specialities in the database with new parameters.
    /// </summary>
    /// <param name="specialityId">Speciality to update</param>
    /// <param name="personId">Person to update</param>
    /// <returns>Async Task.</returns>
    public async Task RemoveAsync(int specialityId, int personId)
    {
        var entities = await this.ListAllAsync();
        var entity = entities.FirstOrDefault(x => x.SpecialityId == specialityId &&  x.PersonId == personId);
        if (entity != null)
        {
            dbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}