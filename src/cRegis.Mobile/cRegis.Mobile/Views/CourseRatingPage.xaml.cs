using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cRegis.Mobile.Models.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cRegis.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseRatingPage : ContentPage
    {
        private Course chosenCourse;

        public CourseRatingPage(Course c)
        {
            InitializeComponent();
            BindingContext = c;
            chosenCourse = c;
        }

        public async void PostComment(object sender, EventArgs e)
        {
            await DisplayAlert("Rate Course", "Rate Successfully", "Okay");
            await Navigation.PushAsync(new CourseDetailPage(chosenCourse));
        }
    }
}