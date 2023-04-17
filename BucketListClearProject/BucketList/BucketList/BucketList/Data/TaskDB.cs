using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using BucketList.Models;
using System.Threading.Tasks;
using BucketList.Views;

namespace BucketList.Data
{
    public class TaskDB
    { 
        public static Dictionary<string, SQLiteAsyncConnection> cathegoryDictionary = new Dictionary<string, SQLiteAsyncConnection>();

        public TaskDB(string connectionString)
        {
            if (!cathegoryDictionary.ContainsKey(CathegoryPage.Cathegory))
            {
                cathegoryDictionary.Add(CathegoryPage.Cathegory, new SQLiteAsyncConnection(connectionString));
            }
            cathegoryDictionary[CathegoryPage.Cathegory].CreateTableAsync<Models.Task>().Wait();
        }

        public Task<List<Models.Task>> GetTasksAsync() 
        {
            return cathegoryDictionary[CathegoryPage.Cathegory].Table<Models.Task>().ToListAsync();
        }

        public Task<Models.Task> GetTaskAsync(int id)
        {
            return cathegoryDictionary[CathegoryPage.Cathegory].Table<Models.Task>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveTaskAsync(Models.Task task)
        {
            return task.Id != 0
                ? cathegoryDictionary[CathegoryPage.Cathegory].UpdateAsync(task)
                : cathegoryDictionary[CathegoryPage.Cathegory].InsertAsync(task);
        }

        public Task<int> DeleteTaskASync(Models.Task task)
        {
            return cathegoryDictionary[CathegoryPage.Cathegory].DeleteAsync(task);
        }
    }
}
