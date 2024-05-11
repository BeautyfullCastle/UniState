using System.Collections.Generic;

namespace UniState.Core
{
    public class StoreManager
    {
        public static StoreManager Instance => instance ??= new StoreManager();
        
        private static StoreManager instance = null;
        private Dictionary<string, object> stores = new ();
        
        public void RegisterStore<T>(string key, Store<T> store)
        {
            stores[key] = store;
        }

        public Store<T> GetStore<T>(string key)
        {
            if (stores.TryGetValue(key, out var store))
                return store as Store<T>;
            
            return null;
        }

        public Dictionary<string, object> GetAllStores()
        {
            return stores;
        }
    }
}