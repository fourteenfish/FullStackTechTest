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
        /// Specialities data
        /// </summary>
        public virtual DbSet<Speciality> specialities { get; set; }

        /// <summary>
        /// People specialities mapping junciton table.
        /// </summary>
        public virtual DbSet<PeopleSpecialities> people_specialities { get; set; }

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

        /// <summary>
        /// Override this method to explicity state the composite key for the junction table.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeopleSpecialities>()
                .HasKey(ps => new { ps.PersonId, ps.SpecialityId });

            base.OnModelCreating(modelBuilder);
        }

    }
}
