using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BucketList.Data;
using BucketList.Models;

namespace BucketList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CathegoryPage : ContentPage
    {
        public static string Cathegory;
        public static Cathegory CurrentCathegory;
        public CathegoryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            collectionView.ItemsSource = await App.TaskDB.GetTasksAsync();
            base.OnAppearing();
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddNewItem));
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            if (e.CurrentSelection != null)
            {
                var task = (Models.Task)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync(
                    $"{nameof(AddNewItem)}?{nameof(AddNewItem.ItemId)}={task.Id.ToString()}");
            }
        }

        private async void DeleteCathegory_Clicked(object sender, EventArgs e)
        {
            await TaskDB.cathegoryDictionary[CathegoryPage.Cathegory].DeleteAllAsync<Models.Task>();
            await App.CathegoryDB.DeleteCathegoryASync(CurrentCathegory);
            await Shell.Current.GoToAsync("..");
        }
    }
}