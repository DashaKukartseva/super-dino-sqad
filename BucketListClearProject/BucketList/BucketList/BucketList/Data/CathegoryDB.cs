using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using BucketList.Models;
using BucketList.Views;
using System.Threading.Tasks;
using System.IO;

namespace BucketList.Data
{
    public class CathegoryDB
    {
        readonly SQLiteAsyncConnection dataBase;

        public CathegoryDB(string connectionString)
        {
            dataBase = new SQLiteAsyncConnection(connectionString);
            dataBase.CreateTableAsync<Cathegory>().Wait();
        }

        public Task<Cathegory> GetCathegoryAsync(int id)
        {
            return dataBase.Table<Cathegory>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<List<Cathegory>> GetCathegoriesAsync()
        {

            foreach (var cathegory in dataBase.Table<Cathegory>().ToListAsync().Result)
            {
                cathegory.UpdateProgress();
                dataBase.UpdateAsync(cathegory);
            }
            return dataBase.Table<Cathegory>().OrderBy(x => x.Progress).ToListAsync();
           
        }

        public Task<int> SaveCathegoryAsync(Cathegory cathegory)
        {
            return cathegory.Id != 0
                ? dataBase.UpdateAsync(cathegory)
                : dataBase.InsertAsync(cathegory);
        }

        public Task<int> DeleteCathegoryASync(Cathegory cathegory)
        {
            return dataBase.DeleteAsync(cathegory);
        }
    }
}
