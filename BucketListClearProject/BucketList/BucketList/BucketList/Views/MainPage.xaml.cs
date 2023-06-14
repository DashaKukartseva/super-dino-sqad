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
            collectionView.ItemsSource = await App.CathegoryDB.GetThreeCathegoriesAsync();
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

        private async void All_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AllCathegories));
        }

        private async void AllSec_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AllProgress));
        }

        private async void ImageButton1_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ArticlePage));
        }
    }
}