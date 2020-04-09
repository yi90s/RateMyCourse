using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cRegis.Mobile.Models.Entities;
using cRegis.Mobile.ViewModels;
using cRegis.Mobile.Services;
using cRegis.Mobile.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cRegis.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseRatingPage : ContentPage
    {
        private EnrolledViewModel chosenCourse;
        private IHistoryService _historyService;

        public CourseRatingPage(Course c, Enrolled e)
        {
            InitializeComponent();
            _historyService = new HistoryService((string)Application.Current.Properties["jwt"]);
            chosenCourse = new EnrolledViewModel(c, e);
            BindingContext = chosenCourse;
        }

        public async void PostComment(object sender, EventArgs e)
        {
            string score = Entry_rating.Text;
            
            try
            {
                int rate = Int32.Parse(score);
                string comm = Entry_comment.Text;

                Enrolled tempE = chosenCourse.enroll;
                tempE.comment = comm;
                tempE.rating = rate;

                string result = await _historyService.postCommentAsync(tempE);

                await DisplayAlert("Rate Course", result, "Okay");
                await Navigation.PushAsync(new CourseDetailPage(chosenCourse.cour));
            } 
            catch (FormatException)
            {
                await DisplayAlert("Error", "Invalid data type", "Okay");
            }
            
        }
    }
}