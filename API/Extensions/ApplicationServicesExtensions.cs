using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
	public static class ApplicationServicesExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			services.AddDbContext<StoreContext>(opt =>
			{
				opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
			});
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = actionContext =>
				{
					var errors = actionContext.ModelState
						.Where(x => x.Value.Errors.Any())
						.ToDictionary(
								x => x.Key,
								x => x.Value.Errors.Select(y => y.ErrorMessage).ToArray()
							);

					return new BadRequestObjectResult(new ApiValidationErrorResponse
					{
						Errors = errors
					});
				};
			});

			return services;
		}
	}
}