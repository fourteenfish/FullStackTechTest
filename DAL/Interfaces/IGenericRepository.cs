using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> ListAllAsync();
        Task SaveAsync(T itemToSave);
        Task<T?> GetByIdAsync(int objectId);
        Task InsertAsync(T obj);
        Task InsertListAsync(List<T> objects);
    }
}
