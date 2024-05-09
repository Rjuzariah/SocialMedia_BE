using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMedia_BE.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDBContext>(
    options => options.UseInMemoryDatabase("AppDb"));
builder.Services.AddDbContext<SocialMediaDBContext>(opt => opt.UseInMemoryDatabase("SocialMedia"));

//builder.Services.AddDbContext<SocialMedia_BEContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SocialMedia_BEContext") ?? throw new InvalidOperationException("Connection string 'SocialMedia_BEContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<ApplicationUser>().AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin() // Allow requests from any origin
                   .AllowAnyMethod() // Allow any HTTP method
                   .AllowAnyHeader(); // Allow any header
        });
});
var app = builder.Build();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;
//RoleInitializer.InitializeAsync(services.GetRequiredService<RoleManager<IdentityRole>>()).Wait();
//UserInitializer.InitializeAsync(services.GetRequiredService<UserManager<ApplicationUser>>()).Wait();
DataInitializer.InitializeAsync(services.GetRequiredService<UserManager<ApplicationUser>>(), services.GetRequiredService<RoleManager<IdentityRole>>()).Wait();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIdentityApi<ApplicationUser>();
app.UseAuthorization();

app.MapControllers();

app.MapSwagger().RequireAuthorization();
app.UseCors("AllowAll");

app.Run();
