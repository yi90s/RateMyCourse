using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using cReg_Mobile.Views;

namespace cReg_Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public void LogOut()
        {
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
