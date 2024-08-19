using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FullStackTechTest.Models.Home;
using FullStackTechTest.Models.Shared;
using DAL.Interfaces;
using FullStackTechTest.Services;
using Microsoft.AspNetCore.Components.Forms;
using DAL;
using Models;

namespace FullStackTechTest.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPersonRepository _personRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly ISpecialitiesRepository _specialitiesRepository;
    private readonly IPersonSpecialitiesRepository _personSpecialitiesRepository;
    private readonly IDataImportService _dataImportService;

    public HomeController(ILogger<HomeController> logger,
        IPersonRepository personRepository, 
        IAddressRepository addressRepository,
        ISpecialitiesRepository specialitiesRepository,
        IPersonSpecialitiesRepository personSpecialitiesRepository,
        IDataImportService dataImportService
        )
    {
        _logger = logger;
        _personRepository = personRepository;
        _addressRepository = addressRepository;
        this._specialitiesRepository = specialitiesRepository;
        this._personSpecialitiesRepository = personSpecialitiesRepository;
        this._dataImportService = dataImportService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await IndexViewModel.CreateAsync(_personRepository);
        return View(model);
    }
    public async Task<IActionResult> Specialities()
    {
        var model = await SpecialitiesViewModel.CreateAsync(_specialitiesRepository);
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var model = await DetailsViewModel.CreateAsync(id, false, _personRepository, _addressRepository, _personSpecialitiesRepository, _specialitiesRepository);
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var model = await DetailsViewModel.CreateAsync(id, true, _personRepository, _addressRepository, _personSpecialitiesRepository, _specialitiesRepository);
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
    public async Task<IActionResult> Import(IFormFile file)
    {
        await _dataImportService.LoadJsonDataAsync(file.FileName);
        var model = await IndexViewModel.CreateAsync(_personRepository);
        return View("Index", model);
    }

    [HttpPost]
    public async Task<IActionResult> AddSpeciality([FromForm] string specialityName)
    {
        await _specialitiesRepository.InsertAsync(specialityName);
        var model = await SpecialitiesViewModel.CreateAsync(_specialitiesRepository);
        return View("Specialities", model);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveSpeciality(int specialityId)
    {
        await _specialitiesRepository.RemoveAsync(specialityId);
        var model = await SpecialitiesViewModel.CreateAsync(_specialitiesRepository);
        return View("Specialities", model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}