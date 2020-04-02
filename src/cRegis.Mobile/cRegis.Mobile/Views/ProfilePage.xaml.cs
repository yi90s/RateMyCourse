using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cRegis.Mobile.ViewModels;
using cRegis.Mobile.Models.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using cRegis.Mobile.Interfaces;
using cRegis.Mobile.Services;

namespace cRegis.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private IStudentService _studentService;

        public ProfilePage()
        {
            InitializeComponent();
            testInit();
            _studentService = new StudentService((string)Application.Current.Properties["jwt"]);
        }

        async void testInit()
        {
            StudentViewModel test = new StudentViewModel();
            //calling api to get student's detail infomation
            Student curStudent =  await _studentService.getStudentAsync();

            BindingContext = test;
        }

        public async void ViewCourseDetail(object sender, EventArgs e)
        {
            //var chosenCourse = (Course)registeredCourseList.SelectedItem;

            var menuItem = sender as Button;
            var chosenCourse = menuItem.CommandParameter as Course;

            await Navigation.PushAsync(new CourseDetailPage(chosenCourse));
        }
    }
}