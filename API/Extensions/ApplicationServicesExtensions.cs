using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

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
				opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
			});
			services.AddSingleton<IConnectionMultiplexer>(c =>
			{
				var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
				return ConnectionMultiplexer.Connect(options);
			});
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IBasketRepository, BasketRepository>();
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

			services.AddCors(opt =>
			{
				opt.AddPolicy("CorsPolicy", policy =>
				{
					policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
				});
			});

			return services;
		}
	}
}