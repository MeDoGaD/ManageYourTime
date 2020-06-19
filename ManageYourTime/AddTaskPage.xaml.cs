using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Xml;
using SQLite;

namespace ManageYourTime
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTaskPage : ContentPage
    {
        private SQLiteConnection con;
        public MyTask task;
        public AddTaskPage(MyTask t)
        {
            InitializeComponent();
            
            con = DependencyService.Get<Sqllite>().GetConnection();
            if (t != null)
            {
                task = t;
                PopulateDetails(t);
            }
            if (addbtn.Text != "Add Task")
            {
                Task_Title.IsReadOnly = true;
                Task_Title.IsEnabled = false;
            }
        }
        public AddTaskPage()
        {
            InitializeComponent();

            Task_date.Date = DateTime.Now;
            if (addbtn.Text != "Add Task")
            {
                Task_Title.IsReadOnly = true;
                Task_Title.IsEnabled = false;
            }
            con = DependencyService.Get<Sqllite>().GetConnection();
        }
        private void PopulateDetails(MyTask details)
        {
            Task_Title.Text = details.Title;
            Task_des.Text = details.description;
            Task_date.Date=DateTime.Parse(details.Date);
            addbtn.Text = "Update";
            //this.Title = "Edit Task";
            rpic.Source = "@drawable/up";
            lpic.Source = "@drawable/up2";
            titlelbl.Text = "Update your Task";
            Task_des.TextColor = Color.FromHex("#0976B6");
            Task_date.TextColor = Color.FromHex("#0976B6");
        }
        private void Focused(object sender, FocusEventArgs e)
        {
            Entry tmp = (Entry)sender;
            if (tmp.Text == "Enter Task Title" || tmp.Text == "Enter Task Description")
            {
                tmp.Text = "";
                tmp.TextColor = Color.FromHex("#0976B6");
            }
        }

        private void Unfocused(object sender, FocusEventArgs e)
        {
            Entry tmp = (Entry)sender;
            if (tmp.Text == "" && tmp == Task_Title)
                tmp.Text = "Enter Task Title";
            else if (tmp.Text == "" && tmp == Task_des)
                tmp.Text = "Enter Task Description";

            tmp.TextColor = Color.Gray;
        }

        private void Add(object sender, EventArgs e)
        {
            
            if (Task_Title.Text == "Enter Task Title" || Task_des.Text == "Enter Task Description"|| Task_Title.Text == "" || Task_des.Text == "")
            {
                DisplayAlert("Error", "Please fill all feilds", "OK");
            }
            else if(Task_date.Date < DateTime.Today)
            {
                DisplayAlert("Error", "You have entered an old date", "OK");
            }
            else
            {
                if (addbtn.Text == "Add Task")
                {
                    MyTask task = new MyTask();
                    task.Title = Task_Title.Text;
                    task.description = Task_des.Text;
                    task.Date = Task_date.Date.ToShortDateString();
                    bool res = DependencyService.Get<Sqllite>().saveTasks(task);
                    if (res)
                        DisplayAlert("Done", "Added Successfully :)", "OK");
                    else
                        DisplayAlert("Sorry", "Data failed to save :(", "OK");

                    Task_Title.Text = "";
                    Task_des.Text = "";
                    Task_date.Date = DateTime.Now;
                }
                else
                {
                    if (Task_date.Date < DateTime.Today)
                    {
                        DisplayAlert("Error", "You have entered an old date", "OK");
                    }
                    else
                    {                       
                        task.Title = Task_Title.Text;
                        task.description = Task_des.Text;
                        task.Date = Task_date.Date.ToShortDateString();
                        bool res = DependencyService.Get<Sqllite>().UpdateTasks(task);
                        if (res)
                        {
                            Navigation.PopAsync();
                        }
                        else
                        {
                            DisplayAlert("Message", "Data Failed To Update", "Ok");
                        }
                    }

                }
            }
        }

        async void Back(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private void Task_date_DateSelected(object sender, DateChangedEventArgs e)
        {
            Task_date.TextColor = Color.FromHex("#0976B6");
        }
    }
}