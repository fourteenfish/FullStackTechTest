using System.Diagnostics;
using DAL;
using Microsoft.AspNetCore.Mvc;
using FullStackTechTest.Models.Home;
using FullStackTechTest.Models.Shared;
using Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Collections.Generic;

namespace FullStackTechTest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPersonRepository _personRepository;
    private readonly IAddressRepository _addressRepository;

    public HomeController(ILogger<HomeController> logger, IPersonRepository personRepository, IAddressRepository addressRepository)
    {
        _logger = logger;
        _personRepository = personRepository;
        _addressRepository = addressRepository;
    }

    public async Task<IActionResult> Index()
    {
        var model = await IndexViewModel.CreateAsync(_personRepository);
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var model = await DetailsViewModel.CreateAsync(id, false, _personRepository, _addressRepository);
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var model = await DetailsViewModel.CreateAsync(id, true, _personRepository, _addressRepository);
        return View("Details", model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [FromForm] DetailsViewModel model)
    {
        await _personRepository.SaveAsync(model.Person);
        await _addressRepository.SaveAsync(model.Address);
        return RedirectToAction("Details", new { id = model.Person.Id });
    }

    [HttpPost]
    public async Task<IActionResult> Upload(List<IFormFile> files)
    {
        long size = files.Sum(f => f.Length);

        var filePaths = new List<string>();
        foreach (var formFile in files)
        {
            if (formFile.Length > 0)
            {
                //lets do validation for this file then
                if (formFile.ContentType == "application/json")
                {
                    JsonSerializerOptions jsonoptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true};

                    var filePath = Path.GetTempFileName();
                    filePaths.Add(filePath);
                    using (var stream = formFile.OpenReadStream())
                    {
                        List<PersonWithAddress>? newPersons = JsonSerializer.Deserialize<List<PersonWithAddress>>(stream, jsonoptions);

                        //import each person
                        foreach (var newPerson in newPersons)
                        {
                            //check if the person exists by GMC number, the import data doesn't have personId
                            Person personCheck =  await _personRepository.GetByGMCAsync(newPerson.GMC);
                            if (personCheck.FirstName != null)
                            {
                                //Validation done in the model attributes
                                newPerson.Id = personCheck.Id;
                                //update the person
                                await _personRepository.SaveAsync(newPerson);
                                ////update the address
                                //Address addressCheck = await _addressRepository.GetForPersonIdAsync(personCheck.Id);
                                //if (addressCheck != null)
                                //{
                                //    await _addressRepository.SaveAsync(address);
                                //}
                                    
                                
                            }
                            //import each address for each person
                            //check if it exists by postcode first
                            await _addressRepository.GetForPersonIdAsync(newPerson.Id);


                        }
                        Console.WriteLine("newPersons=" + newPersons?.ToString());
                    }


                } else
                {
                    return BadRequest();
                }


            } else
            {
                return NoContent();
            }
        }
        

        return Ok(new { count = files.Count, size, filePaths });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
}