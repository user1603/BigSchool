using bigschool.DTOs;
using bigschool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace bigschool.Controllers
{
    public class FollowingsController : ApiController
    {
		private readonly ApplicationDbContext _dbContext;

		public FollowingsController()
		{
			_dbContext = new ApplicationDbContext();
		}

		[HttpPost]
		public IHttpActionResult Follow(FollowingDto followingDto)
		{
			var userId = User.Identity.GetUserId();
			try
            {
				Following following = _dbContext.Followings.Where(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId).FirstOrDefault();
				//if (_dbContext.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followingDto.FolloweeId))
				if (following != null)
				{
					_dbContext.Followings.Remove(following);
					_dbContext.SaveChanges();
				}
				else
				{
					var folowing = new Following
					{
						FollowerId = userId,
						FolloweeId = followingDto.FolloweeId
					};

					_dbContext.Followings.Add(folowing);
					_dbContext.SaveChanges();
				}
			}
			catch(Exception ex)
            {
				return BadRequest("Folowing error");
			}
				

			return Ok();
		}

		
	}
}
