using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL;

public class SpecialitiesRepository : GenericRepository<Speciality>, ISpecialitiesRepository
{
    public SpecialitiesRepository(DoctorDbContext doctorDbContext) :base(doctorDbContext)
    {       

    }

    /// <summary>
    /// Retrieves the names of specilalities given a list of IDs.
    /// </summary>
    /// <param name="specialityIds">List of IDs to lookup</param>
    /// <returns>List of friendly specilaity names</returns>    
    public async Task<List<string>> RetrieveSpecialityNamesAsync(List<int> specialityIds)
    {
        return await this.dbContext.specialities
                                    .Where(e => specialityIds.Contains(e.Id))
                                    .Select(e => e.SpecialityName)
                                    .ToListAsync();
    }

    /// <summary>
    /// Adds a new speciality name. Generates a new ID.
    /// </summary>
    /// <param name="specialityName">Name of new speciality</param>
    /// <returns>Task of operation</returns>
    public async Task InsertAsync(string specialityName)
    {
        if (specialityName != null)
        {
            var specialities = await this.dbContext.specialities.ToListAsync();
            Speciality speciality = new()
            {
                SpecialityName = specialityName,
                Id = specialities.Select(e => e.Id).Max() + 1
            };
            await this.InsertAsync(speciality);
            await dbContext.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Removes a speciality from the repository but first removes its dependancy.
    /// </summary>
    /// <param name="specialityId">Id of object to remove</param>
    /// <returns>Task of operation.</returns>
    public new async Task RemoveAsync(int specialityId)
    {
        var objectToDelete = await this.GetByIdAsync(specialityId);
        var dependantObjectsToDelete = dbContext.people_specialities.Where(x => x.SpecialityId == specialityId);
        if (dependantObjectsToDelete != null)
        {
            await dependantObjectsToDelete.ForEachAsync(x => dbContext.Remove(x));
        }
        if(objectToDelete != null)
        {
            dbContext.Remove(objectToDelete);
        }
        await dbContext.SaveChangesAsync();
        
    }
}