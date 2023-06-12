using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BucketList.Data;

namespace BucketList.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
	public partial class AddNewItem : ContentPage
	{
        public string ItemId
        {
            set
            {
                LoadTask(value);
            }
        }

        
		public AddNewItem ()
		{
			InitializeComponent ();
            BindingContext = new Models.Task();
		}

        private async void LoadTask(string value)
        {
            try
            {
                var id = Convert.ToInt32(value);
                var task = await App.TaskDB.GetTaskAsync(id);
                BindingContext = task;
            }
            catch { }
        }

        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            var task = (Models.Task) BindingContext;
            task.GetTreeImage();
            if (task.Completed == null)
            {
                task.NotCompleted = "Не выполнено!";
            }
            if (!string.IsNullOrWhiteSpace(task.Text))
            {
                await App.TaskDB.SaveTaskAsync(task);
            }
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButton_Clicked(object sender, EventArgs e)
        {
            var task = (Models.Task)BindingContext;
            await App.TaskDB.DeleteTaskASync(task);
            await Shell.Current.GoToAsync("..");
        }
        

        private async void Completed_Clicked(object sender, EventArgs e)
        {
            var task = (Models.Task)BindingContext;
            if (string.IsNullOrWhiteSpace(task.Text))
            {
                await DisplayAlert("Внимание", "Вы ничего не ввели!", "OK");
                return;
            }
            task.Completed = "Выполнено!";
            task.NotCompleted = "";
            await App.TaskDB.SaveTaskAsync(task);
            await Shell.Current.GoToAsync("..");
        }

        private async void NotCompleted_Clicked(object sender, EventArgs e)
        {
            var task = (Models.Task)BindingContext;
            if (string.IsNullOrWhiteSpace(task.Text))
            {
                await DisplayAlert("Внимание", "Вы ничего не ввели!", "OK");
                return;
            }
            task.Completed = "";
            task.NotCompleted = "Не выполнено!";
            await App.TaskDB.SaveTaskAsync(task);
            await Shell.Current.GoToAsync("..");
        }
    }
}