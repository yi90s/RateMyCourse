using cRegis.Core.Entities;
using cRegis.Tests.IntegrationTest.Infrastructure;
using cRegis.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace cRegis.Tests.IntegrationTest.ActionTests.Web
{
    public class RateCourseTests:WebActionTestBase
    {
        public RateCourseTests() : base() { }

        [Theory]
        [InlineData(3, 75, "Very interesting prof. Does a good job breaking down ideas into chunks you can easily understand")]
        [InlineData(17,40, "The course thinks that everyone learns the same way (auditory). ")]
        public async Task GetRate_Test(int eid,int rate, string comment )
        {
            //act
            ViewResult result =(ViewResult) await _courseController.Rate(eid);
            RateCourseViewModel model = (RateCourseViewModel)result.ViewData.Model;

            //assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.True(model.Rating.GetValueOrDefault() == rate);
            Assert.Equal(comment, model.Comment);

        }

        [Theory]
        [InlineData(9,80,"Not bad")]
        public async Task SetRate_Test(int eid, int rate, string comment)
        {
            //arrange

            RateCourseViewModel model = new RateCourseViewModel { EnrollId = eid, Rating = rate, Comment = comment };

            //act 

            var result = (RedirectToActionResult)await _courseController.Rate(model);
            Enrolled updatedEnroll = await _enrollService.getEnrollAsync(eid);

            //assert
            Assert.NotNull(result);
            Assert.NotNull(updatedEnroll);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
            Assert.True(rate==updatedEnroll.rating);
            Assert.Equal(comment, updatedEnroll.comment);

        }
    }
}
