using Models;

namespace DAL.Interfaces;

public interface IAddressRepository
{
    Task<Address?> GetForPersonIdAsync(int personId);
    Task SaveAsync(Address address);
    Task InsertAsync(Address obj);
    Task InsertListAsync(List<Address> objects);

}