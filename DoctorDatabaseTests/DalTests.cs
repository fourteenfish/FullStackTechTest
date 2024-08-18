using AutoFixture;
using DAL;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using Moq;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace DoctorDatabaseTests
{
    /// <summary>
    /// Test class for the data access layer.
    /// </summary>
    [TestClass]
    public class DalTests
    {
        /// <summary>
        /// Mocked DB Context.
        /// </summary>
        private readonly Mock<DoctorDbContext> mockedDbContext = new ();

        /// <summary>
        /// Mocked DB set - addresses.
        /// </summary>
        private readonly Mock<DbSet<Address>> mockedAddressDbSet = new();

        /// <summary>
        /// Mocked DB set - people.
        /// </summary>
        private readonly Mock<DbSet<Person>> mockedPersonDbSet = new();

        private readonly List<Person> sourceListPeople = new();
        private readonly List<Address> sourceListAddresses = new();

        /// <summary>
        /// Fixture for setting up object values.
        /// </summary>
        private readonly IFixture fixture = new Fixture();

        /// <summary>
        /// Set up tests.
        /// </summary>
        [TestInitialize]
        public void TestInitialise()
        {
            mockedPersonDbSet.Setup(d => d.Add(It.IsAny<Person>())).Callback<Person>((s) => sourceListPeople.Add(s));
            mockedAddressDbSet.Setup(d => d.Add(It.IsAny<Address>())).Callback<Address>((s) => sourceListAddresses.Add(s));
            mockedAddressDbSet.Setup(d => d.Add(It.IsAny<Address>())).Callback<Address>((s) => sourceListAddresses.Add(s));
            mockedAddressDbSet.Setup(d => d.Add(It.IsAny<Address>())).Callback<Address>((s) => sourceListAddresses.Add(s));

            mockedDbContext.Setup(x => x.people).Returns(mockedPersonDbSet.Object);
            mockedDbContext.Setup(x => x.addresses).Returns(mockedAddressDbSet.Object);

        }

    }
}