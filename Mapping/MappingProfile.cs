using AutoMapper;
using cReg_WebApp.Models.entities;
using cReg_WebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cReg_WebApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //add all entity model to viewmodel mapping to this class;

            CreateMap<Enrolled, RateCourseViewModel>();
            CreateMap<Course, RateCourseViewModel>();
        }
    }
}
