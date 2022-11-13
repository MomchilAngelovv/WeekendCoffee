namespace WeekendCoffee.Data
{
	public class Member
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string NickName { get; set; }
		public string PhoneNumber { get; set; }

		public virtual ICollection<Attendance> Attendances { get; set; }
	}
}
