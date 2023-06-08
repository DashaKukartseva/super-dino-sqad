using BucketList.Data;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Transactions;

namespace BucketList.Models
{
    public class Cathegory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProgressTitle { get; set; }
        public double Progress { get; set; }    

        public void UpdateProgress()
        {
            var taskDataBase = new SQLiteAsyncConnection(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        Name + "TasksDatabase.db3"));
            double completedTasksCount = taskDataBase
               .Table<Task>().Where(x => x.Completed == "Выполнено!").CountAsync().Result;
            double tasksCount = taskDataBase
                .Table<Task>().CountAsync().Result;
            Progress = (tasksCount == 0 || completedTasksCount == 0)
                ? 0
                : Math.Ceiling(((double)(completedTasksCount / tasksCount)) * 100);
            ProgressTitle = Progress.ToString() + "%";
        }
    }
}
