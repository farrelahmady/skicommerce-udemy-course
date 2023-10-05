using API.Errors;
using API.Middlewares;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(opt =>
{
	opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<ApiBehaviorOptions>(options =>
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

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseStaticFiles(); // ? wwwroot

// app.UseStaticFiles(
// 	new StaticFileOptions
// 	{
// 		FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Storage")),
// 	}
// ); // ? Custom file provider (Storage folder)



app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
	await context.Database.MigrateAsync();
	await StoreContextSeed.SeedAsync(context);
}
catch (System.Exception e)
{
	logger.LogError(e, "An error occurred during migration");
}


app.Run();
