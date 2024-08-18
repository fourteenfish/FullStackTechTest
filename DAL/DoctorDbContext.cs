using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL
{
    /// <summary>
    /// Database context class for the doctor database.
    /// </summary>
    public class DoctorDbContext : DbContext
    {
        /// <summary>
        /// Address data
        /// </summary>
        public virtual DbSet<Address> addresses { get; set; }
        
        /// <summary>
        /// Persons data
        /// </summary>
        public virtual DbSet<Person> people { get; set; }

        /// <summary>
        /// Connection string to connect to the database.
        /// </summary>
        private static string ConnectionString => Config.DbConnectionString;

        /// <summary>
        /// Used to access the database.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
        }
    }
}
