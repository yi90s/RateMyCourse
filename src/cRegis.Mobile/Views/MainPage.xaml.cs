using cReg_Mobile.Views;
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
            string authHeader64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", Entry_userName.Text, Entry_password.Text)));
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader64);
            var stringContent = new StringContent("");
            var response = await client.PostAsync("http://ec2-15-223-82-164.ca-central-1.compute.amazonaws.com/auth", stringContent);

            if (response.IsSuccessStatusCode)
            {
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

            //if (id == "123" && password == "password")
            //{
            //    //await DisplayAlert("Login", "Login Success", "Okay");
            //    if (Device.OS == TargetPlatform.Android)
            //    {
            //        Application.Current.MainPage = new NavigationPage(new MasterPage());
            //    } else if (Device.OS == TargetPlatform.iOS)
            //    {
            //        await Navigation.PushModalAsync(new NavigationPage(new MasterPage()));
            //    }
            //} else
            //{
            //    await DisplayAlert("Login", "Wrong username or password", "Retry");
            //}
        }
    }
}
