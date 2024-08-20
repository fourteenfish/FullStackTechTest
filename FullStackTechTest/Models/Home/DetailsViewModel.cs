using DAL.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq;

namespace FullStackTechTest.Models.Home;

public class DetailsViewModel
{
    public Person Person { get; set; } = new();
    public Address Address { get; set; } = new();
    public List<string> PersonsSpecialities { get; set; } = new();
    public List<Speciality> AllSpecialities { get; set; } = new();
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
            PersonsSpecialities = await specialitiesRepository.RetrieveSpecialityNamesAsync(personsSpecialities),
            IsEditing = isEditing,
            AllSpecialities = await specialitiesRepository.ListAllAsync()
        };
        return model;
    }

}