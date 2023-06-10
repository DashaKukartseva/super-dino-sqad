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
        public int TaskCount { get; set; }
        public string TaskCountTitle { get; set; }  

        public void UpdateProgressAndTaskCount()
        {
            var taskDataBase = new SQLiteAsyncConnection(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        Name + "TasksDatabase.db3"));
            taskDataBase.CreateTableAsync<Task>().Wait();
            double completedTasksCount = taskDataBase
               .Table<Task>().Where(x => x.Completed == "Выполнено!").CountAsync().Result;
            TaskCount = taskDataBase
                .Table<Task>().CountAsync().Result;
            Progress = (TaskCount == 0 || completedTasksCount == 0)
                ? 0
                : Math.Ceiling(((double)(completedTasksCount / TaskCount)) * 100);
            ProgressTitle = Progress.ToString() + "%";
            if (TaskCount % 10 == 1 && TaskCount % 100 != 11)
            {
                TaskCountTitle = TaskCount.ToString() + " задача";
            }
            else if (TaskCount % 10 <= 4 && TaskCount % 10 >= 2 && !(TaskCount % 100 >= 12 && TaskCount <= 14))
            {
                TaskCountTitle = TaskCount.ToString() + " задачи";
            }
            else
            {
                TaskCountTitle = TaskCount.ToString() + " задач";
            }
        }
    }
}
