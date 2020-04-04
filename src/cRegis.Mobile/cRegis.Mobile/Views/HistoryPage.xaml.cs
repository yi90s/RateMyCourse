using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cRegis.Mobile.Interfaces;
using cRegis.Mobile.Services;
using cRegis.Mobile.ViewModels;
using cRegis.Mobile.Models.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cRegis.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {

        private ICourseService _courseService;

        public HistoryPage()
        {
            InitializeComponent();
            Init();
        }

        async void Init()
        {
            _courseService = new CourseService((string)Application.Current.Properties["jwt"]);
            List<Course> l = await _courseService.getHistoryListAsync();
            CourseViewModel test = new CourseViewModel(l);
            //test.test();
            BindingContext = test;
        }

        public async void ViewCourseDetail(object sender, EventArgs e)
        {
            //var chosenCourse = (Course)registeredCourseList.SelectedItem;

            var menuItem = sender as Button;
            var chosenCourse = menuItem.CommandParameter as Course;

            await Navigation.PushAsync(new CourseDetailPage(chosenCourse));
        }

        public async void RateCourse(object sender, EventArgs e)
        {
            var menuItem = sender as Button;
            var chosenCourse = menuItem.CommandParameter as Course;

            await Navigation.PushAsync(new CourseRatingPage(chosenCourse));
        }
    }
}