using BucketList.Data;
using BucketList.Views;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BucketList
{
    public partial class App : Application
    {
        static TaskDB taskDB;
        static CathegoryDB cathegoryDB;

        public static CathegoryDB CathegoryDB
        {
            get
            {
                if (cathegoryDB == null)
                {
                    cathegoryDB = new CathegoryDB(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "CathegoriesDatabase.db3"));
                }
                return cathegoryDB;
            }
        }

        public static TaskDB TaskDB
        {
            get
            { 
                if (!TaskDB.cathegoryDictionary.ContainsKey(CathegoryPage.Cathegory))
                {
                    taskDB = new TaskDB(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        CathegoryPage.Cathegory + "TasksDatabase.db3"));
                }
                return taskDB;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
