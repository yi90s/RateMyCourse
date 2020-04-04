using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cRegis.Mobile.Models.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using cRegis.Mobile.Interfaces;
using cRegis.Mobile.Services;
using cRegis.Mobile.ViewModels;

namespace cRegis.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetailPage : ContentPage
    {
        private ICourseService _courseService;
        private Course c;

        public CourseDetailPage(Course chosenCourse)
        {
            InitializeComponent();
            c = chosenCourse;
            Init();
        }


        async void Init()
        {
            _courseService = new CourseService((string)Application.Current.Properties["jwt"]);
            List<Comment> l = await _courseService.getCourseCommentAsync(c.courseId);

            CourseDetailViewModel model = new CourseDetailViewModel(c, l);

            BindingContext = model;
        }
    }
}