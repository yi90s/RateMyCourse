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

        private IStudentService _studentService;
        private IHistoryService _historyService;

        public HistoryPage()
        {
            InitializeComponent();
            Init();
        }

        async void Init()
        {
            _studentService = new StudentService((string)Application.Current.Properties["jwt"]);
            _historyService = new HistoryService((string)Application.Current.Properties["jwt"]);

            List<Enrolled> listE = await _historyService.getHistoryEnrolledListAsync();
            List<EnrolledViewModel> listEnroll = new List<EnrolledViewModel>();

            foreach (Enrolled e in listE)
            {
                int tempI = e.courseId;
                Course tempC = await _studentService.getCourseAsync(tempI);
                listEnroll.Add(new EnrolledViewModel(tempC, e));
            }


            HistoryViewModel model = new HistoryViewModel(listEnroll);
            BindingContext = model;
        }

        public async void ViewCourseDetail(object sender, EventArgs e)
        {
            //var chosenCourse = (Course)registeredCourseList.SelectedItem;

            var menuItem = sender as Button;
            var chosenCourse = menuItem.CommandParameter as EnrolledViewModel;

            await Navigation.PushAsync(new CourseDetailPage(chosenCourse.cour));
        }

        public async void RateCourse(object sender, EventArgs e)
        {
            var menuItem = sender as Button;
            var chosenCourse = menuItem.CommandParameter as EnrolledViewModel;

            await Navigation.PushAsync(new CourseRatingPage(chosenCourse.cour, chosenCourse.enroll));
        }
    }
}