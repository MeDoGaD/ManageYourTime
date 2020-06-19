using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ManageYourTime
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public static string type,state;
        async void Addbtn_Clicked(object sender, EventArgs e)
        {
            state = "Add";
            await Navigation.PushAsync(new AddTaskPage());
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert("Alert!", "Do you really want to exit?", "Yes", "No");
                //if (result) await this.Navigation.PopAsync();  // or anything else
            
                
            });

            return false;
        }
        async void todaybtn(object sender, EventArgs e)
        {
            type = "today";
            await Navigation.PushAsync(new ShowTasksPage());

        }

        private void Exitbtn(object sender, EventArgs e)
        {
            OnBackButtonPressed();  
        }

        async void AllTasksbtn(object sender, EventArgs e)
        {
            type = "all";
            await Navigation.PushAsync(new ShowTasksPage());
        }
    }
}
