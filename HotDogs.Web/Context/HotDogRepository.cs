using HotDogs.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace HotDogs.Web.Context
{
    public class HotDogRepository : IDisposable
    {
        private readonly HotDogContext _context;

        public HotDogRepository()
        {
            _context = new HotDogContext();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        // Hotdogs Store

        public void AddStore(HotDogStore store)
        {
            _context.Stores.Add(store);
        }

        public IEnumerable<HotDogStore> GetAllStores()
        {
            return _context.Stores
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

        public IEnumerable<HotDogStore> GetStoresByManagerName(string managerUserName)
        {
            return _context.StoreManagers
                .Where(m => m.UserName == managerUserName).FirstOrDefault()
                .Stores;
        }

        public IEnumerable<HotDogStore> GetUserFavoriteStores(string customerUserName)
        {
            return _context.Customers
                .Where(c => c.UserName == customerUserName).FirstOrDefault()
                .FavoriteStores;
        }

        public void UpdateStore (HotDogStore store)
        {
            if (store == null)
                throw new ArgumentNullException("Store ne peut pas être null");

            _context.Stores.AddOrUpdate(store);
        }

        public void DeleteStore (HotDogStore store)
        {
            if (store != null)
                _context.Stores.Remove(store);
        }

        public void deleteStoreById(int storeId)
        {
            var store = GetStoreById(storeId);

            DeleteStore(store);
        }

        // HotDogs

        public void AddHotDog(int storeId, HotDog newHotDog)
        {
            var store = GetStoreById(storeId);

            if (store != null)
            {
                newHotDog.Store = store;
                store.HotDogs.Add(newHotDog);
            }
        }

        public IEnumerable<HotDog> GetHotDogsByStoreId(int storeId)
        {
            if (storeId <= 0)
                throw new ArgumentOutOfRangeException("StoreId doit être superieur à 0");

            return _context.HotDogs
                .Where(h => h.Store.Id == storeId)
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

        public void UpdateHotDog(HotDog hotdog)
        {
            if (hotdog == null)
                throw new ArgumentNullException("HotDog ne peut pas être null");

            _context.HotDogs.AddOrUpdate(hotdog);
        }

        public void DeleteHotDog(HotDog hotdog)
        {
            _context.HotDogs.Remove(hotdog);
        }

        public void DeleteHotDogById(int hotdogId)
        {
            var hotdog = GetHotDogById(hotdogId);

            if (hotdog != null)
                DeleteHotDog(hotdog);
        }

        // Customer

        public HotDogCustomer GetCustomerByGuid (string customerGuid)
        {
            return _context.Customers
                .Where(c => c.Id == customerGuid)
                .FirstOrDefault();
        }

        // Managers

        public HotDogStoreManager GetManagerByName(string userName)
        {
            return _context.StoreManagers
                .Where(m => m.UserName == userName)
                .FirstOrDefault();
        }

        public bool isValidManagerForStore(int storeId, string managerName)
        {
            // var storeResult = GetStoreById(storeId).Managers
            //     .Where(m => m.UserName == managerName)
            //     .FirstOrDefault();

             var storeResult = GetStoreById(storeId).Managers
                .FirstOrDefault(m => m.UserName == managerName);

            if (storeResult != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Orders

        public void AddOrder (HotDogOrder order)
        {
            _context.Orders.Add(order);
        }

        public void AddOrder (int storeId, string customerGuid, IEnumerable<int> hotdogsIds)
        {
            HotDogOrder order = new HotDogOrder();

            order.Customer = GetCustomerByGuid(customerGuid);
            order.Store = GetStoreById(storeId);

            foreach (var hotDogId in hotdogsIds)
            {
                order.HotDogs.Add(GetHotDogById(hotDogId));
            }

            AddOrder(order);
        }

        public IEnumerable<HotDogOrder> GetOrdersByCustomerId (string customerGuid)
        {
            return _context.Customers
                .Where(c => c.Id == customerGuid).FirstOrDefault()
                .Orders;
        }

        public IEnumerable<HotDogOrder> GetOrdersByStoreId (int storeId)
        {
            return _context.Stores
                .Where(s => s.Id == storeId).FirstOrDefault()
                .Orders;
        }

        // IDispose
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}