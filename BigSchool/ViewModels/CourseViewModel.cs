using bigschool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace bigschool.ViewModels
{
    public class CourseViewModel 
    {
		public IEnumerable<Course> UpcommingCourses { get; set; }
		public List<Following> ListFollowing { get; set; }
		public List<Attendance> ListAttendance { get; set; }
		public bool ShowAction { get; set; }

		public int Id { get; set; }

        [Required]
        public string Place { get; set; }
        [Required]
        [FutureDate]
        public string Date { get; set; }
        [Required]
        [ValidTime]
        public string Time { get; set; }
		[Required]
        public byte Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public DateTime GetDateTime()
        {
			return DateTime.Parse(String.Format("{0} {1}", Date, Time), CultureInfo.InvariantCulture);

		}
		public string Heading { get; set; }
		public string Action
		{
			get { return (Id != 0) ? "Update" : "Create"; }
		}
    }
}