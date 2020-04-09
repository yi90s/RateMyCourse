using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using cRegis.Mobile.Views;

namespace cRegis.Mobile
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
