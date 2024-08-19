using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Models;

namespace FullStackTechTest.Models.Home;

public class SpecialitiesViewModel
{
    public List<Speciality>? SpecialityList { get; set; }

    public static async Task<SpecialitiesViewModel> CreateAsync(ISpecialitiesRepository specialitiesRepository)
    {
        var model = new SpecialitiesViewModel
        {
            SpecialityList = await specialitiesRepository.ListAllAsync()
        };
        return model;
    }
}