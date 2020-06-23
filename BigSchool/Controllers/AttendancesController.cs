using bigschool.DTOs;
using bigschool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bigschool.Controllers
{
	[Authorize]
    public class AttendancesController : ApiController
    {
		private ApplicationDbContext _dbContext;

		public AttendancesController()
		{
			_dbContext = new ApplicationDbContext();
		}

		[HttpPost]
		public IHttpActionResult Attend(AttendanceDto attendanceDto)
		{
			var userId = User.Identity.GetUserId();
			try
            {
				Attendance attendances = _dbContext.Attendances.Where(a => a.AttendeeID == userId && a.CourseID == attendanceDto.CourseID).FirstOrDefault();

				//if (_dbContext.Attendances.Any(a => a.AttendeeID == userId && a.CourseID == attendanceDto.CourseID))
				if (attendances != null)
				{
					_dbContext.Attendances.Remove(attendances);
					_dbContext.SaveChanges();
				}
				else
				{
					var attendance = new Attendance
					{
						CourseID = attendanceDto.CourseID,
						AttendeeID = userId
					};

					_dbContext.Attendances.Add(attendance);
					_dbContext.SaveChanges();
				}
			}
			catch(Exception ex)
            {
				return BadRequest("Error!");
			}
			
			return Ok();
		}
    }
}
