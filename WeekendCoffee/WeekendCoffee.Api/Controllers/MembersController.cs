namespace WeekendCoffee.Api.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using WeekendCoffee.Services;
	using WeekendCoffee.Api.Models;

	[ApiController]
	[Route("[controller]")]
	public class MembersController : ControllerBase
	{
		private readonly IMembersService membersService;

		public MembersController(
			IMembersService membersService)
		{
			this.membersService = membersService;
		}

		[HttpPost]
		public async Task<IActionResult> Post(AddMemberRequestModel requestModel)
		{
			var newMember = await this.membersService.AddMemberAsync(requestModel.Name, requestModel.NickName, requestModel.PhoneNumber);
			return this.Ok(newMember);
		}
	}
}
