namespace WeekendCoffee.Api.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using WeekendCoffee.Services;
	using WeekendCoffee.Api.Models.Requests;

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

			var response = new 
			{
				newMember.Id,
				newMember.Name,
				newMember.NickName,
				newMember.PhoneNumber
			};

			return this.Ok(response);
		}
	}
}
