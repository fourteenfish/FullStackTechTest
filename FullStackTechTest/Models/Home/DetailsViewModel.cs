using DAL.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Models;
using System.Linq;

namespace FullStackTechTest.Models.Home;

public class DetailsViewModel
{
    public Person Person { get; set; }
    public Address Address { get; set; }
    public List<string> PeopleSpecialities { get; set; }
    public bool IsEditing { get; set; }

    public static async Task<DetailsViewModel> CreateAsync(int personId, 
        bool isEditing,
        IPersonRepository personRepository,
        IAddressRepository addressRepository,
        IPersonSpecialitiesRepository peopleSpecialitiesRepository,
        ISpecialitiesRepository specialitiesRepository)
    {
        var personsSpecialities = await peopleSpecialitiesRepository.GetAllSpecialitiesAsync(personId);
        var model = new DetailsViewModel
        {
            Person = await personRepository.GetByIdAsync(personId) ?? new(),
            Address = await addressRepository.GetForPersonIdAsync(personId) ?? new(),
            PeopleSpecialities = await specialitiesRepository.RetrieveSpecialityNamesAsync(personsSpecialities),
            IsEditing = isEditing
        };
        return model;
    }

}