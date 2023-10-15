using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
	public class AppIdentityDbContextSeed
	{
		public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
		{
			if (!userManager.Users.Any())
			{
				var user = new AppUser
				{
					DisplayName = "User",
					Email = "user@example.com",
					UserName = "user@example.com",
					Address = new Address
					{
						FirstName = "User",
						LastName = "Local",
						City = "Tangerang",
						State = "Banten",
						ZipCode = "12345"
					}
				};

				await userManager.CreateAsync(user, "Pa$$w0rd");
			}
		}
	}
}