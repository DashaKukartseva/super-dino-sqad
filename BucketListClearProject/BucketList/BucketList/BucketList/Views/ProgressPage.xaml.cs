using BucketList.Data;
using BucketList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BucketList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgressPage : ContentPage
    {
        public static Cathegory CurrentCathegory { get; set; }
        public ProgressPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            collectionView.ItemsSource = await App.TaskDB.GetTasksAsync(CurrentCathegory);
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
                AddNewItem.EditingCathegory = CurrentCathegory;
                await Shell.Current.GoToAsync(
                    $"{nameof(AddNewItem)}?{nameof(AddNewItem.ItemId)}={task.Id}");
            }
        }

        private async void DeleteCathegory_Clicked(object sender, EventArgs e)
        {
            await TaskDB.CathegoryDictionary[CurrentCathegory.Name].DeleteAllAsync<Models.Task>();
            await App.CathegoryDB.DeleteCathegoryASync(CurrentCathegory);
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }
}