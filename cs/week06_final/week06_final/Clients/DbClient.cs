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
        private Dictionary<string, object> _dataStore = new Dictionary<string, object>();

        public async Task<bool> AddAsync<T>(string key, T item)
        {
            await Task.Delay(100);
            string fullKey = GetKey<T>(key);
            if (_dataStore.ContainsKey(fullKey))
                throw new KeyExsistException($"Key '{fullKey}' already exists.");
            _dataStore[fullKey] = item;
            return true;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            await Task.Delay(100);
            if (_dataStore.ContainsKey(GetKey<T>(key)))
            {
                return (T)_dataStore[GetKey<T>(key)];
            }
            return default(T);
        }

        public async Task<List<T>> GetAllAsync<T>()
        {
            await Task.Delay(100);
            return _dataStore.Values.OfType<T>().ToList();
        }

        private string GetKey<T>(string key)
        {
            return key + "|" + typeof(T);
        }
    }

    public class KeyExsistException : Exception
    {
        public KeyExsistException(string s): base(s)
        {
        }
    }
}
