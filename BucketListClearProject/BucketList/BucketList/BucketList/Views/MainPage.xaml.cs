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
    public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            collectionViewProgress.ItemsSource = await App.CathegoryDB.GetThreeCathegoriesAsync();
            collectionView.ItemsSource = await App.CathegoryDB.GetCathegoriesAsync();
            base.OnAppearing();
        }

        private async void OnProgressSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var cathegory = (Cathegory)e.CurrentSelection.FirstOrDefault();
                ProgressPage.CurrentCathegory = cathegory;
                await Shell.Current.GoToAsync(nameof(ProgressPage));
            }
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var cathegory = (Cathegory)e.CurrentSelection.FirstOrDefault();
                CathegoryPage.CurrentCathegory = cathegory;
                await Shell.Current.GoToAsync(nameof(CathegoryPage));
            }
        }

        private async void AddCathegory_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddNewCathegory));
        }

        private void All_Clicked(object sender, EventArgs e)
        {

        }

        private void AllSec_Clicked(object sender, EventArgs e)
        {

        }
    }
}