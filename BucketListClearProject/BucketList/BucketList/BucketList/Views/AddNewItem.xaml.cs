using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            task.Date = DateTime.UtcNow;
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
    }
}