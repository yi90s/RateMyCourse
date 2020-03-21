using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cReg_Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using cReg_Mobile.Models.Entities;

namespace cReg_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        public CoursePage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            CourseViewModel test = new CourseViewModel();
            test.test();
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