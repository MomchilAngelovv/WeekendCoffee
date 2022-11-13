namespace WeekendCoffee.Data
{
	using Microsoft.EntityFrameworkCore;

	public class WeekendCoffeeDbContext : DbContext
	{
		public WeekendCoffeeDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Meeting> Meetings { get; set; }
		public DbSet<Member> Members { get; set; }
		public DbSet<Attendance> Attendances { get; set; }
	}
}
