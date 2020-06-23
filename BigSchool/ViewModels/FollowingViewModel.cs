using bigschool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bigschool.ViewModels
{
    public class FollowingViewModel
    {
        public IEnumerable<ApplicationUser> FollowingCourse { get; set; }
        public List<Following> ListFollowing { get; set; }
        public bool ShowAction { get; set; }
    }
}