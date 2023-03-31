using DAL;
using Models;

namespace FullStackTechTest.Models.Home;

public class DetailsViewModel
{
    public Person Person { get; set; }
    public Address Address { get; set; }
    public bool IsEditing { get; set; }

    public static async Task<DetailsViewModel> CreateAsync(int personId, bool isEditing, IPersonRepository personRepository, IAddressRepository addressRepository)
    {
        var model = new DetailsViewModel
        {
            Person = await personRepository.GetByIdAsync(personId),
            Address = await addressRepository.GetForPersonIdAsync(personId),
            IsEditing = isEditing
        };
        return model;
    }
}