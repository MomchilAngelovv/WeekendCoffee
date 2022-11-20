namespace WeekendCoffee.Api.Models.Requests
{
	using System.ComponentModel.DataAnnotations;

	public class InsertSettingRequest
	{
		[Required]
		public string Key { get; set; }
		[Required]
		public string Value { get; set; }
	}
}
