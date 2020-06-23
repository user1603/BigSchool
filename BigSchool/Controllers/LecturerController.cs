using bigschool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bigschool.ViewModels;
using Microsoft.AspNet.Identity;

namespace bigschool.Controllers
{
    public class LecturerController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        public LecturerController()
        {
            _dbContext = new ApplicationDbContext();
        }


        // GET: Lecturer
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();

            var following = _dbContext.Followings
                .Where(a => a.FollowerId == userId)
                .Select(a => a.Followee)
                .ToList();

            var viewModel = new FollowingViewModel
            {
                FollowingCourse = following,
                ShowAction = User.Identity.IsAuthenticated
            };

            if (User.Identity.IsAuthenticated)
            {
                var follow = _dbContext.Followings.Where(f => f.FollowerId == userId).ToList();
                viewModel.ListFollowing = follow;
            }
               
            return View(viewModel);
        }
    }
}