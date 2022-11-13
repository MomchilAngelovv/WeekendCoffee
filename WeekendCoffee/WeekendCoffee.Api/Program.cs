using WeekendCoffee.Data;
using Microsoft.EntityFrameworkCore;
using WeekendCoffee.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<WeekendCoffeeDbContext>((options) => 
{
	options.UseSqlServer("Server=.;Database=WeekendCoffee_Db;Trusted_Connection=True;"); 
});

builder.Services.AddTransient<IAttendancesService, AttendancesService>();
builder.Services.AddTransient<IMeetingsService, MeetingsService>();
builder.Services.AddTransient<IMembersService, MembersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
