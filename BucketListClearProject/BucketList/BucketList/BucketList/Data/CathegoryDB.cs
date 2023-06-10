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

        public void LoadCathegoriesToDict()
        {
            foreach (var cathegory in dataBase.Table<Cathegory>().ToListAsync().Result)
            {
                if (!TaskDB.CathegoryDictionary.ContainsKey(cathegory.Name))
                {
                    TaskDB.CathegoryDictionary.Add(cathegory.Name, new SQLiteAsyncConnection(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        cathegory.Name + "TasksDatabase.db3")));
                }
            }
        }

        public Task<Cathegory> GetCathegoryAsync(int id)
        {
            return dataBase.Table<Cathegory>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<List<Cathegory>> GetCathegoriesAsync()
        {
            LoadCathegoriesToDict();
            foreach (var cathegory in dataBase.Table<Cathegory>().ToListAsync().Result)
            {
                cathegory.UpdateProgressAndTaskCount();
                dataBase.UpdateAsync(cathegory);
            }
            return dataBase.Table<Cathegory>().OrderBy(x => x.Progress).ToListAsync();
           
        }

        public Task<List<Cathegory>> GetThreeCathegoriesAsync()
        {
            LoadCathegoriesToDict();
            foreach (var cathegory in dataBase.Table<Cathegory>().ToListAsync().Result)
            {
                cathegory.UpdateProgressAndTaskCount();
                dataBase.UpdateAsync(cathegory);
            }
            return dataBase.Table<Cathegory>().OrderBy(x => x.Progress).Take(3).ToListAsync();

        }

        public Task<int> SaveCathegoryAsync(Cathegory cathegory)
        {
            if (cathegory.Id != 0)
            {
                return dataBase.UpdateAsync(cathegory);
            }
            else
            {
                if (!TaskDB.CathegoryDictionary.ContainsKey(cathegory.Name))
                {
                    TaskDB.CathegoryDictionary.Add(cathegory.Name, new SQLiteAsyncConnection(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        cathegory.Name + "TasksDatabase.db3")));
                }
                return dataBase.InsertAsync(cathegory);
            }
        }

        public Task<int> DeleteCathegoryASync(Cathegory cathegory)
        {
            return dataBase.DeleteAsync(cathegory);
        }
    }
}
