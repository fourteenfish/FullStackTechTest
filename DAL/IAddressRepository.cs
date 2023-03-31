using Models;

namespace DAL;

public interface IAddressRepository
{
    Task<Address> GetForPersonIdAsync(int personId);
    Task SaveAsync(Address address);
}