
using DAL.Interfaces;
using Models;
using Newtonsoft.Json;
using System;

namespace FullStackTechTest.Services
{
    public class DataImportService : IDataImportService
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<DataImportService> logger;

        /// <summary>
        /// Person Repo.
        /// </summary>
        private readonly IPersonRepository personRepository;

        /// <summary>
        /// Directory of files. 
        /// </summary>
        private const string fileDirectory = "../Import Files/";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name=""></param>
        public DataImportService(ILogger<DataImportService> logger, IPersonRepository personRepository)
        {
            this.logger = logger;
            this.personRepository = personRepository;
        }

        /// <summary>
        /// Method to import local json data into the database.
        /// </summary>
        /// <param name="pathToData">File path to json data</param>
        /// <returns>Success or fail result.</returns>
        public async Task<bool> LoadJsonDataAsync(string pathToData)
        {
            try
            {
                string strJson = File.ReadAllText(Path.GetFullPath(fileDirectory + pathToData));
                // Deserialise and validate data
                List<Person> people = JsonConvert.DeserializeObject<List<Person>>(strJson)?
                    .Where(per => isValidPerson(per)).ToList() ?? throw new JsonException();
                await this.personRepository.InsertAsync(people);
                this.logger.LogInformation($"Successfully imported {people.Count} rows of JSON data to database.");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error in loading JSON data.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates person to add
        /// </summary>
        /// <param name="person"></param>
        /// <returns>True if person isn't already in database and has a 7 digit GMC, otherwise false.</returns>
        private bool isValidPerson(Person person)
        {
            var foundPerson = this.personRepository.GetByGmcAsync(person.GMC);
            // Valid if person doesnt exist and GMC is 7 digits.
            return (foundPerson == null && Math.Floor(Math.Log10(person.GMC) + 1) == 7);
        }
    }
}
