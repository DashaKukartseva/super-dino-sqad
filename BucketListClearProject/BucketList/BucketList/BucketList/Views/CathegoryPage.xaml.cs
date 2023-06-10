using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BucketList.Data;
using BucketList.Models;

namespace BucketList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CathegoryPage : ContentPage
    {
        public static Cathegory CurrentCathegory { get; set; }
        public CathegoryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            cathegoryTitle.Text = CurrentCathegory.Name;
            CurrentCathegory.UpdateProgressAndTaskCount();
            collectionView.ItemsSource = await App.TaskDB.GetTasksAsync(CurrentCathegory);
            base.OnAppearing();
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            if (CurrentCathegory.TaskCount == 9)
            {
                await DisplayAlert("Внимание", "Достигнуто максимальное количество задач!", "OK");
                return;
            }
            await Shell.Current.GoToAsync(nameof(AddNewItem));
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            if (e.CurrentSelection != null)
            {
                var task = (Task)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync(
                    $"{nameof(AddNewItem)}?{nameof(AddNewItem.ItemId)}={task.Id}");
            }
        }

        private async void DeleteCathegory_Clicked(object sender, EventArgs e)
        {
            await TaskDB.CathegoryDictionary[CurrentCathegory.Name].DeleteAllAsync<Task>();
            await App.CathegoryDB.DeleteCathegoryASync(CurrentCathegory);
            await Shell.Current.GoToAsync(nameof(MainPage));
        }   
    }
}