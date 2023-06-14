using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace BucketList.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticlePage : ContentPage
	{
		public static int CurrentArticlePoiter;
		public static string CurrentArticleTitle;
		public Dictionary<int, (string, string)> Articles = new Dictionary<int, (string, string)>()
		{
			{1, ("firstArticle.txt", "Михаил Груздев") },
			{2, ("secondArticle.txt", "Григорий Кшеминский") },
			{3, ("thirdArticle.txt", "Аманда Дадли") }
		};

		protected override async void OnAppearing()
		{
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ArticlePage)).Assembly;
            var stream = assembly.GetManifestResourceStream("BucketList."+ Articles[CurrentArticlePoiter].Item1);
			var text = "";
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            articleAuthor.Text = "Автор: " + Articles[CurrentArticlePoiter].Item2;
			currentArticle.Text = text;
			articleTitle.Text = CurrentArticleTitle;
            base.OnAppearing();
        }
        public ArticlePage ()
		{
			InitializeComponent ();
		}
	}
}