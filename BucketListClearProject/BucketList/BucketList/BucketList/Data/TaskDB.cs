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

        public TaskDB()
        {
        }

        public Task<List<Models.Task>> GetTasksAsync(Cathegory cathegory) 
        {
            CathegoryDictionary[cathegory.Name].CreateTableAsync<Models.Task>().Wait();
            return CathegoryDictionary[cathegory.Name].Table<Models.Task>()
                .OrderBy(x => x.Completed)
                .ToListAsync();
        }

        public Task<Models.Task> GetTaskAsync(int id)
        {
            return CathegoryDictionary[ProgressPage.CurrentCathegory.Name].Table<Models.Task>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveTaskAsync(Models.Task task)
        {
            return task.Id != 0
                ? CathegoryDictionary[CathegoryPage.CurrentCathegory.Name].UpdateAsync(task)
                : CathegoryDictionary[CathegoryPage.CurrentCathegory.Name].InsertAsync(task);
        }

        public Task<int> DeleteTaskASync(Models.Task task)
        {
            return CathegoryDictionary[CathegoryPage.CurrentCathegory.Name].DeleteAsync(task);
        }
    }
}
