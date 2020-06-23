using bigschool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using bigschool.ViewModels;
using Microsoft.AspNet.Identity;

namespace bigschool.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
           

            var upcommingCourses = _dbContext.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Category)
                .Where(c => c.DateTime > DateTime.Now && c.IsCanceled == false);

            

			var viewModel = new CourseViewModel
			{
				UpcommingCourses = upcommingCourses,
				ShowAction = User.Identity.IsAuthenticated
			};

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var follow = _dbContext.Followings.Where(f => f.FollowerId == userId).ToList();
                var attendance = _dbContext.Attendances.Where(f => f.AttendeeID == userId).ToList();

                viewModel.ListFollowing = follow;
                viewModel.ListAttendance = attendance;
            }

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult DeleteFollowing(string id)
        {
            var userId = User.Identity.GetUserId();
            Following following = _dbContext.Followings.Where(f => f.FolloweeId == id && f.FollowerId == userId).FirstOrDefault();
            if (following == null)
            {
                return null;
            }

            _dbContext.Followings.Remove(following);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}