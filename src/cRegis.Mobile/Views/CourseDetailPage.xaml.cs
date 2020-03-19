﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cReg_Mobile.Models.Entities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cReg_Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetailPage : ContentPage
    {
        public CourseDetailPage(Course chosenCourse)
        {
            InitializeComponent();
            BindingContext = chosenCourse;
        }
    }
}