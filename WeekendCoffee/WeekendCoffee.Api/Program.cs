using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

using WeekendCoffee.Data;
using WeekendCoffee.Services;

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

builder.Services.AddCors();
builder.Services.AddTransient<IAttendancesService, AttendancesService>();
builder.Services.AddTransient<IMeetingsService, MeetingsService>();
builder.Services.AddTransient<IMembersService, MembersService>();
builder.Services.AddTransient<ISettingsService, SettingsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(options => 
{
	options.AllowAnyHeader();
	options.AllowAnyMethod();
	options.AllowAnyOrigin();
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Run the application
app.Run();
