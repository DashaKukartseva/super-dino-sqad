using BucketList.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BucketList
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddNewItem), typeof(AddNewItem));
            Routing.RegisterRoute(nameof(CathegoryPage), typeof(CathegoryPage));
            Routing.RegisterRoute(nameof(ProgressPage), typeof(ProgressPage));
            Routing.RegisterRoute(nameof(AddNewCathegory), typeof(AddNewCathegory));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }

    }
}
