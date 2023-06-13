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
            foreach (var task in CathegoryDictionary[cathegory.Name].Table<Models.Task>().ToListAsync().Result) 
            {
                task.GetTreeImage();
                CathegoryDictionary[cathegory.Name].UpdateAsync(task);
            }
            return CathegoryDictionary[cathegory.Name].Table<Models.Task>()
                .OrderBy(x => x.Completed)
                .ToListAsync();
        }

        public Task<Models.Task> GetTaskAsync(int id)
        {

            return CathegoryDictionary[AddNewItem.EditingCathegory.Name].Table<Models.Task>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveTaskAsync(Models.Task task)
        {
            return task.Id != 0
                ? CathegoryDictionary[AddNewItem.EditingCathegory.Name].UpdateAsync(task)
                : CathegoryDictionary[AddNewItem.EditingCathegory.Name].InsertAsync(task);
        }

        public Task<int> DeleteTaskASync(Models.Task task)
        {
            return CathegoryDictionary[AddNewItem.EditingCathegory.Name].DeleteAsync(task);
        }
    }
}
