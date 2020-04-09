using cRegis.Mobile.Views;
using cRegis.Mobile.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using cRegis.Mobile.Interfaces;
using cRegis.Mobile.Services;

namespace cRegis.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private IAuthService _authService;

        public MainPage()
        {
            InitializeComponent();
            Init();
            _authService = new AuthService();
        }

        void Init()
        {
            ActivitySpinner.IsVisible = false;
        }

        async void ValidateStudent(object sender, EventArgs e)
        {
            HttpResponseMessage response = await _authService.jwtAuthenticate(Entry_userName.Text, Entry_password.Text);
            
            if (response.IsSuccessStatusCode)
            {
                //put the jwt in a sessional storage
                Application.Current.Properties["jwt"] = await response.Content.ReadAsStringAsync();

                if (Device.OS == TargetPlatform.Android)
                {
                    Application.Current.MainPage = new NavigationPage(new MasterPage());
                }
                else if (Device.OS == TargetPlatform.iOS)
                {
                    await Navigation.PushModalAsync(new NavigationPage(new MasterPage()));
                }
            }
            else
            {
                await DisplayAlert("Login", "Wrong username or password", "Retry");
            }
        }
    }
}
