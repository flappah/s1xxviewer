using Microsoft.Isam.Esent.Collections.Generic;
using S1xxViewer.Storage.Interfaces;

namespace S1xxViewer.Storage
{
    public class OptionsStorage : IOptionsStorage
    {
        private static PersistentDictionary<string, string> _persistentDictionary;

        /// <summary>
        ///     Returns the number of items in the persistent storage
        /// </summary>
        public int Count
        {
            get
            {
                return _persistentDictionary.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public OptionsStorage()
        {
            if (_persistentDictionary == null)
            {
                _persistentDictionary = new PersistentDictionary<string, string>("Options");
            }
        }

        /// <summary>
        ///     Stores the given key, value to the persistent data store
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <returns></returns>
        public virtual bool Store(string key, string value)
        {
            if (_persistentDictionary.ContainsKey(key))
            {
                _persistentDictionary.Remove(key);
            }

            _persistentDictionary.Add(key, value);
            return true;
        }

        /// <summary>
        ///     Retrieves the value associated with the specified key from the persistent store
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>tuple containing both the status and the result</returns>
        public virtual string Retrieve(string key)
        {
            if (_persistentDictionary.ContainsKey(key))
            {
                return _persistentDictionary[key];
            }

            return "";
        }
    }
}
