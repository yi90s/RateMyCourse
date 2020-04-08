using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cRegis.Core.Entities;
using cRegis.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace cRegis.API.Controllers
{
    [Authorize(Roles = "Student")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [Route("[controller]/{cid}")]
        [HttpPost]
        public async Task<ActionResult> addCoursetoStudentWishlist(int cid)
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            int valid = await _wishlistService.verifyWishlistEntry(sid, cid);

            if (valid <= 0)
            {
                return BadRequest("Student is not able to add the course to the wishlist");
            }

            await _wishlistService.addCoursetoStudentWishlist(sid, cid);
            return Ok("Successful Addition To Wishlist");
        }

        [Route("[controller]")]
        [HttpPost]
        public async Task<ActionResult> updatePriority(int cid, [FromQuery] MoveDirection direction)
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            await _wishlistService.updatePriority(sid, cid, direction);
            return Ok();
        }

        [Route("[controller]/{cid}")]
        [HttpDelete]
        public ActionResult removeCourseFromStudentWishlist(int cid)
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            _wishlistService.removeCourseFromStudentWishlist(sid, cid);
            return Ok();
        }

        [Route("[controller]")]
        [HttpGet]
        public ActionResult<List<Wishlist>> getStudentWishlist()
        {
            int sid = Int32.Parse(this.User.FindFirst("sid")?.Value);
            var result = _wishlistService.getStudentWishlist(sid);

            return result;
        }
    }
}
