using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using StoreHouse.Models;
using Xamarin.Forms;

namespace StoreHouse.DAL
{
    public class Database
    {

        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {

             _database = new SQLiteAsyncConnection(dbPath);
           // _database.DropTableAsync<StoreItem>().Wait();
           // _database.DropTableAsync<StorePlace>().Wait();
          //  _database = DependencyService.Get<IDatabase>().CreateConnection();
            _database.CreateTableAsync<StoreItem>().Wait();
            _database.CreateTableAsync<StorePlace>().Wait();
        }

        #region get

        public async Task<StoreItem> GetItem(Guid codeId)
        {
            return await _database.Table<StoreItem>().FirstOrDefaultAsync(item => item.CodeId == codeId);
        }

        public async Task<StorePlace> GetPlace(int placeId)
        {
            return await _database.Table<StorePlace>().FirstOrDefaultAsync(item => item.Id == placeId);
        }

        public async Task<List<object>> GetAsync(Type type)
        {
            
            if (type == typeof(StoreItem))
                return (await _database.Table<StoreItem>().ToListAsync()).ToList<object>();
            if (type == typeof(StorePlace))
                return (await _database.Table<StorePlace>().ToListAsync()).ToList<object>();
            return null;
        }

        public async Task<int> GetCountAsync(Type type)
        {

            if (type == typeof(StoreItem))
                return (await _database.Table<StoreItem>().CountAsync());
            if (type == typeof(StorePlace))
                return (await _database.Table<StorePlace>().CountAsync());
            return 0;
        }
        #endregion

        #region save
        public async Task<int> SaveAsync(object item)
        {
      
            if (item is StorePlace place)
                return (place.Id != 0) ? await _database.UpdateAsync(place) :
                    await _database.InsertAsync(place);
            if (item is StoreItem storeItem)
                return (storeItem.Id != 0) ? await _database.UpdateAsync(storeItem) : await _database.InsertAsync(storeItem);
            return 0;
        }
        #endregion

        #region delete
        public async Task<int> DeleteAsync(object item)
        {
            return await _database.DeleteAsync(item);
        }
        #endregion
    }
}
