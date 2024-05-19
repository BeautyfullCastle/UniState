using System.Collections.Generic;

namespace UniState.Core
{
    public class StoreManager
    {
        public static StoreManager Instance => instance ??= new StoreManager();
        
        private static StoreManager instance = null;
        private readonly Dictionary<string, IStore> stores = new ();
        
        public void RegisterStore(string key, IStore store)
        {
            stores[key] = store;
        }

        public IStore GetStore(string key)
        {
            if (stores.TryGetValue(key, out var store))
                return store;
            
            return null;
        }

        public Dictionary<string, IStore> GetAllStores()
        {
            return stores;
        }
    }
}