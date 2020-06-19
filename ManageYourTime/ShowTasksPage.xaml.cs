using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ManageYourTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowTasksPage : ContentPage
    {
        private SQLiteConnection con;
        public MyTask task;
        public ShowTasksPage()
        {
            InitializeComponent();
            con = DependencyService.Get<Sqllite>().GetConnection();
            listview1.HasUnevenRows = true;          
               
        }
        protected override void OnAppearing()
        {
            PopulateTaskList();
        }
        public void PopulateTaskList()
        {
            listview1.ItemsSource = null;
            listview1.ItemsSource = DependencyService.Get<Sqllite>().GetMyTasks();
        }
        private void Listview1_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
        }

        private async void DeleteEmployee(object sender, EventArgs e)
        {
            
            bool ress = await DisplayAlert("Message", "Do you want to delete this Task?", "Ok", "Cancel");
            if (ress)
            {
                var menu = sender as MenuItem;
                MyTask details = menu.CommandParameter as MyTask;
                DependencyService.Get<Sqllite>().DeleteTasks(details.description,details.Title);
               
                PopulateTaskList();
            }
        }

        private void EditTask(object sender, ItemTappedEventArgs e)
        {
            MyTask details = e.Item as MyTask;
            if (details != null)
            {
                Navigation.PushAsync(new AddTaskPage(details));
            }
        }

        private void Listview1_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}