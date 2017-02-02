using HotDogs.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HotDogs.Web.Context
{
    public class HotDogRepository : IDisposable
    {
        private HotDogContext _context;

        public HotDogRepository()
        {
            _context = new HotDogContext();
        }

        public void AddStore(HotDogStore store)
        {
            _context.Stores.Add(store);
        }

        public IEnumerable<HotDogStore> GetAllStores()
        {
            return _context.Stores.ToList();
        }

        public IEnumerable<HotDogStore> GetStoresByUsername(string name)
        {
            return _context.Stores
                .Where(s => s.ManagerName == name)
                .ToList();
        }

        public HotDogStore GetStoreById(int storeId)
        {
            if (storeId <= 0)
                throw new ArgumentOutOfRangeException("StoreId doit être superieur à 0");

            return _context.Stores
                    .Where(s => s.Id == storeId)
                    .Include(s => s.HotDogs)
                    .FirstOrDefault();
        }

        public void AddHotDog(int storeId, HotDog newHotDog)
        {
            var store = GetStoreById(storeId);

            if (store != null)
            {
                newHotDog.HotDogStoreId = store.Id;
                store.HotDogs.Add(newHotDog);
            }
        }

        public IEnumerable<HotDog> GetHotDogsByStoreId(int storeId)
        {
            if (storeId <= 0)
                throw new ArgumentOutOfRangeException("StoreId doit être superieur à 0");

            return _context.HotDogs
                .Where(h => h.HotDogStoreId == storeId)
                .ToList();
        }

        public HotDog GetHotDogById(int hotdogId)
        {
            if (hotdogId <= 0)
                throw new ArgumentOutOfRangeException("HotDogId doit être superieur à 0");

            return (from h in _context.HotDogs
                    where h.Id == hotdogId
                    select h).SingleOrDefault();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        internal void DeleteHotDog(HotDog hotdog)
        {
            _context.HotDogs.Remove(hotdog);
        }
        internal void DeleteHotDogById(int hotdogId)
        {
            var hotdog = GetHotDogById(hotdogId);

            if (hotdog != null)
                DeleteHotDog(hotdog);
        }
        public void Dispose()
        {
            _context.Dispose();
        }


    }
}