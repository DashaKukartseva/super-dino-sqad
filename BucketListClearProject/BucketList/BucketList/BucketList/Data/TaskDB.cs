using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using BucketList.Models;
using System.Threading.Tasks;
using BucketList.Views;
using System.IO;

namespace BucketList.Data
{
    public class TaskDB
    {
        public static Dictionary<string, SQLiteAsyncConnection> CathegoryDictionary = new Dictionary<string, SQLiteAsyncConnection>();

        public TaskDB(string connectionString)
        {
            if (!CathegoryDictionary.ContainsKey(CathegoryPage.Cathegory))
            {
                CathegoryDictionary.Add(CathegoryPage.Cathegory, new SQLiteAsyncConnection(connectionString));
            }
            CathegoryDictionary[CathegoryPage.Cathegory].CreateTableAsync<Models.Task>().Wait();
        }

        public Task<List<Models.Task>> GetTasksAsync() 
        {
            return CathegoryDictionary[CathegoryPage.Cathegory].Table<Models.Task>()
                .OrderBy(x => x.Completed)
                .ToListAsync();
        }

        public Task<Models.Task> GetTaskAsync(int id)
        {
            return CathegoryDictionary[CathegoryPage.Cathegory].Table<Models.Task>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveTaskAsync(Models.Task task)
        {
            return task.Id != 0
                ? CathegoryDictionary[CathegoryPage.Cathegory].UpdateAsync(task)
                : CathegoryDictionary[CathegoryPage.Cathegory].InsertAsync(task);
        }

        public Task<int> DeleteTaskASync(Models.Task task)
        {
            return CathegoryDictionary[CathegoryPage.Cathegory].DeleteAsync(task);
        }
    }
}
