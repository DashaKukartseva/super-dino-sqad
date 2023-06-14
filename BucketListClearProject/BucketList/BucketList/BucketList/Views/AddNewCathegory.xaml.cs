using System;
using BucketList.Models;
using Xamarin.Forms;


namespace BucketList.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class AddNewCathegory : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadTask(value);
            }
        }

        private async void LoadTask(string value)
        {
            try
            {
                var id = Convert.ToInt32(value);
                var task = await App.CathegoryDB.GetCathegoryAsync(id);
                BindingContext = task;
            }
            catch { }
        }

        public AddNewCathegory()
        {
            InitializeComponent();
            BindingContext = new Cathegory();
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            var cathegory = (Cathegory)BindingContext;
            if (!string.IsNullOrWhiteSpace(cathegory.Name))
            {
                await App.CathegoryDB.SaveCathegoryAsync(cathegory);
                CathegoryPage.CurrentCathegory = cathegory;
                await Shell.Current.GoToAsync(nameof(CathegoryPage));
            }
            else
            {
                await DisplayAlert("Внимание", "Вы ничего не ввели!", "OK");
                return;
            }
        }
    }
}