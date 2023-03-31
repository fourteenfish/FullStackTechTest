using DAL;
using Models;

namespace FullStackTechTest.Models.Home;

public class IndexViewModel
{
    public List<Person> PeopleList { get; set; }

    public static async Task<IndexViewModel> CreateAsync(IPersonRepository personRepository)
    {
        var model = new IndexViewModel
        {
            PeopleList = await personRepository.ListAllAsync()
        };
        return model;
    }
}