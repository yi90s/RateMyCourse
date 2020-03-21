using cReg_Mobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace cReg_Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            ActivitySpinner.IsVisible = false;
        }

        async void ValidateStudent(object sender, EventArgs e)
        {
            String id = Entry_StudentID.Text;
            String password = Entry_StudentPassword.Text;

            if (id == "123" && password == "password")
            {
                //await DisplayAlert("Login", "Login Success", "Okay");
                if (Device.OS == TargetPlatform.Android)
                {
                    Application.Current.MainPage = new NavigationPage(new MasterPage());
                } else if (Device.OS == TargetPlatform.iOS)
                {
                    await Navigation.PushModalAsync(new NavigationPage(new MasterPage()));
                }
            } else
            {
                await DisplayAlert("Login", "Wrong username or password", "Retry");
            }
        }
    }
}
