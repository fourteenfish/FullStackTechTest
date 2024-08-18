using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    /// <summary>
    /// A generic repository class holding common properties and methods used for most requests.
    /// </summary>
    /// <typeparam name="T">The object for this repository</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        /// <summary>
        /// The DB set for this repository
        /// </summary>
        private DbSet<T> dbSet;

        /// <summary>
        /// The DB Context for database interactions.
        /// </summary>
        public DoctorDbContext dbContext;

        /// <summary>
        /// Constructor to instantiate a new instance of the <see cref="GenericRepository{T}"/> class.
        /// </summary>
        /// <param name="doctorDbContext">The database context.</param>
        public GenericRepository(DoctorDbContext doctorDbContext)
        {
            this.dbContext = doctorDbContext;
            this.dbSet = dbContext.Set<T>();
        }

        /// <summary>
        /// Method to return a generic entity given an id.
        /// </summary>
        /// <param name="objectId">ID used to find entity</param>
        /// <returns>Entity</returns>
        public async Task<T?> GetByIdAsync(int objectId)
        {
            return await dbSet.FindAsync(objectId);
        }

        /// <summary>
        /// Returns all entities.
        /// </summary>
        /// <returns>All entities in database for this object.</returns>
        public async Task<List<T>> ListAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        /// <summary>
        /// Updates an entity in the database with new parameters.
        /// </summary>
        /// <param name="itemToSave">Item to update</param>
        /// <returns>Async Task.</returns>
        public async Task SaveAsync(T itemToSave)
        {
            dbSet.Attach(itemToSave);
            dbContext.Entry(itemToSave).State = EntityState.Modified;   
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Inserts an object into the database.
        /// </summary>
        /// <param name="obj">Object to insert</param>
        /// <returns>Task of operation.</returns>
        public async Task InsertAsync(T obj)
        {
            await dbSet.AddAsync(obj);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Inserts a list of objects into the database.
        /// </summary>
        /// <param name="obj">Object to insert</param>
        /// <returns>Task of operation.</returns>
        public async Task InsertListAsync(List<T> objects)
        {
            foreach (var obj in objects)
            {
                await dbSet.AddAsync(obj);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
