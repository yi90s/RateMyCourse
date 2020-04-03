using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cRegis.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using cRegis.Mobile.Models.Entities;
using cRegis.Mobile.Interfaces;
using cRegis.Mobile.Services;

namespace cRegis.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        private ICourseService _courseService;

        public CoursePage()
        {
            InitializeComponent();
            Init();
        }

        async void Init()
        {

            _courseService = new CourseService((string)Application.Current.Properties["jwt"]);
            List<Course> l = await _courseService.getCourseListAsync();
            CourseViewModel test = new CourseViewModel(l);
            //test.test();
            BindingContext = test;
        }

        void SearchCourse(object sender, TextChangedEventArgs e)
        {
            var _context = BindingContext as CourseViewModel;
            CourseListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                CourseListView.ItemsSource = _context.AllCourses;
            else
                CourseListView.ItemsSource = _context.AllCourses.Where(i => i.courseName.Contains(e.NewTextValue));

            CourseListView.EndRefresh();

        }


        public async void ViewCourseDetail(object sender, EventArgs e)
        {
            //var chosenCourse = (Course)registeredCourseList.SelectedItem;

            var menuItem = sender as Button;
            var chosenCourse = menuItem.CommandParameter as Course;

            await Navigation.PushAsync(new CourseDetailPage(chosenCourse));
        }

        void Register(object sender, EventArgs e)
        {
            DisplayAlert("Register", "Register Success", "Okay");
        }
    }
}