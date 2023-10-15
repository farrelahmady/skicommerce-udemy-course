using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class AuthController : BaseApiController
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;

		public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var user = await this.userManager.FindByEmailAsync(loginDto.Email);

			if (user == null) return Unauthorized(new ApiResponse(401));

			var result = await this.signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

			if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

			return new UserDto
			{
				Email = user.Email,
				DisplayName = user.DisplayName,
				Token = "This is my custom token",
			};
		}
	}
}