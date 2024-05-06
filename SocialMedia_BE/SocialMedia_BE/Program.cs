using Microsoft.EntityFrameworkCore;
using SocialMedia_BE.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SocialMediaDBContext>(opt => opt.UseInMemoryDatabase("SocialMedia"));

//builder.Services.AddDbContext<SocialMedia_BEContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SocialMedia_BEContext") ?? throw new InvalidOperationException("Connection string 'SocialMedia_BEContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
