using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week06_final.Abstraction.Clients;

namespace week06_final.Clients
{
    public class DbClient : IDbClient
    {
        public async Task<bool> AddAsync<T>(T item)
        {
            await Task.Delay(100); 
            return true;
        }

        public async Task<T> GetAsync<T>(string name)
        {
           
            await Task.Delay(100); 
            return default(T);
        }

        public async Task<List<T>> GetAllAsync<T>()
        {
            await Task.Delay(100);
            return new List<T>();
        }
    }
}
