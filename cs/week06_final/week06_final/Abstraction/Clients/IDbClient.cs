using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week06_final.Abstraction.Clients
{
    public interface IDbClient
    {
        Task<bool> AddAsync<T>(string key, T item);
        Task<T> GetAsync<T>(string key);
        Task<List<T>> GetAllAsync<T>();
    }
}
