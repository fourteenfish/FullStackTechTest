using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    public AddressRepository(DoctorDbContext doctorDbContext) :base(doctorDbContext)
    {
       
    }

    /// <summary>
    /// Get an address of a person given their ID.
    /// </summary>
    /// <param name="personId">The persons ID</param>
    /// <returns>The address of the person.</returns>
    public async Task<Address?> GetForPersonIdAsync(int personId)
    {
        return await dbContext.addresses.FirstOrDefaultAsync(adr => adr.PersonId == personId);
    }
}