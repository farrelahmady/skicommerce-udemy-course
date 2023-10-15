using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
	public static class IdentityServiceExtensions
	{
		// Adds identity services to the specified IServiceCollection.
		// Parameters:
		//   services: The IServiceCollection to add the identity services to.
		//   config: The IConfiguration object used to configure the identity services.
		// Returns:
		//   The modified IServiceCollection with the identity services added.
		public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
		{
			services.AddDbContext<AppIdentityDbContext>(opt =>
			{
				opt.UseSqlServer(config.GetConnectionString("IdentityConnection"));
			});

			services.AddIdentityCore<AppUser>(opt =>
			{
			})
			.AddEntityFrameworkStores<AppIdentityDbContext>()
			.AddSignInManager<SignInManager<AppUser>>();

			services.AddAuthentication();
			services.AddAuthorization();

			return services;
		}
	}
}