using System.Diagnostics;
using DAL;
using Microsoft.AspNetCore.Mvc;
using FullStackTechTest.Models.Home;
using FullStackTechTest.Models.Shared;
using Models;
using System.Text.Json;

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
                        List<PersonWithAddress>? newPersonWithAddresses = JsonSerializer.Deserialize<List<PersonWithAddress>>(stream, jsonoptions);
                        if (newPersonWithAddresses != null )
                        {
                            
                            await ImportPersons(newPersonWithAddresses);
                        }
                    }
                    return Redirect("Index");
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


    /// <summary>
    /// Updates each newPersonWithAddresses person and all of their addresses
    /// </summary>
    /// <param name="newPersonWithAddresses"></param>
    public async Task ImportPersons(List<PersonWithAddress> newPersonWithAddresses)
    {
        //import each person
        if (newPersonWithAddresses != null)
        {
            foreach (var newPersonWithAddress in newPersonWithAddresses)
            {
                if (newPersonWithAddress.GMC.ToString().Length == 7)
                {
                    //check if the person exists by GMC number, the import data doesn't have personId
                    Person personCheck = await _personRepository.GetByGMCAsync(newPersonWithAddress.GMC);
                    if (personCheck.FirstName != null)
                    {
                        //update person (already exists)
                        //Validation done in the model attributes
                        newPersonWithAddress.Id = personCheck.Id;
                        //update the person
                        //this is seems like the best thing to do here as the user will probably expect this;
                        await _personRepository.SaveAsync(newPersonWithAddress);

                        //look through all the addresses this person has
                        //check to see if the address already exists
                        //if it exists update
                        //if it does not already exist then insert
                        if (newPersonWithAddress.address != null)
                        {
                            ImportAddresses(newPersonWithAddress);
                        }
                    }
                    else
                    {
                        //insert person
                        newPersonWithAddress.Id = await _personRepository.InsertAsync(newPersonWithAddress);
                        //insert the addresses
                        ImportAddresses(newPersonWithAddress);
                    }
                }
                //TODO: throw this back to the page to show them this one isnt imported
                    

            }
        }
    }
    /// <summary>
    /// Updates/Inserts addresses of the newPerson
    /// </summary>
    /// <param name="newPersonWithAddress"></param>
    public async void ImportAddresses(PersonWithAddress newPersonWithAddress)
    {
        //if it exists update
        //if it does not already exist then insert
        if (newPersonWithAddress.address != null)
        {
            // look through all the addresses this person has
            foreach (Address newAddress in newPersonWithAddress.address)
            {
                //check to see if the address already exists - use a fuzzy match on the postcode

                Address addressCheck = await _addressRepository.GetForPersonIdAsync(newPersonWithAddress.Id);
                if (addressCheck.Postcode != null) {
                    if (addressCheck.Postcode.ToLower().Replace(" ", String.Empty) == newAddress.Postcode.ToLower().Replace(" ", String.Empty))
                    {
                        ////update the address
                        newAddress.Id = addressCheck.Id;
                        await _addressRepository.SaveAsync(newAddress);
                    } else
                    {
                        //Insert this new address for this person
                        newAddress.PersonId = newPersonWithAddress.Id;
                        await _addressRepository.InsertAsync(newAddress);
                    }
                } else
                {
                    //Insert this new address for this person
                    newAddress.PersonId = newPersonWithAddress.Id;
                    await _addressRepository.InsertAsync(newAddress);
                }
            }
        }
    }

    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
}