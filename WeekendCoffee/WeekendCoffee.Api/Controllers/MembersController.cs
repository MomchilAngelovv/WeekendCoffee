namespace WeekendCoffee.Api.Controllers
{
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

	using WeekendCoffee.Common;
	using WeekendCoffee.Services;
	using WeekendCoffee.Api.Models.Requests;

	public class MembersController : BaseController
	{
		private readonly IMembersService membersService;

		public MembersController(
			IMembersService membersService)
		{
			this.membersService = membersService;
		}

		[HttpGet]
		public async Task<IActionResult> GetMany()
		{
			var members = await this.membersService.GetManyAsync();

			var responseData = new
			{
				Members = members.Select(m => new
				{
					m.Id,
					m.NickName,
					m.Password
				})
				.ToList()
			};

			return this.SuccessResponse(responseData);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetOne(int id)
		{
			var member = await this.membersService.GetOneAsync(id);
			if (member is null)
			{
				return this.ErrorResponse(GlobalErrorMessages.CannotFindMember);
			}

			var responseData = new
			{
				member.Id,
				member.NickName,
				member.Password
			};

			return this.SuccessResponse(responseData);
		}

		[HttpPost]
		public async Task<IActionResult> InsertMember(InsertMemberRequest request)
		{
			var existingMember = await this.membersService.GetOneAsync(request.NickName);
			if (existingMember is not null)
			{
				return ErrorResponse(GlobalErrorMessages.MemberAlreadyExists);
			}

			var newMember = await this.membersService.InsertOneAsync(request.FirstName, request.MiddleName, request.LastName, request.NickName, request.PhoneNumber, request.Password);

			var responseData = new
			{
				newMember.Id,
				newMember.FirstName,
				newMember.MiddleName,
				newMember.LastName,
				newMember.NickName,
				newMember.PhoneNumber,
				newMember.Password
			};

			return this.SuccessResponse(responseData);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> GetOne(int id, UpdateMemberRequest request)
		{
			var member = await this.membersService.GetOneAsync(id);
			if (member is null)
			{
				return this.ErrorResponse(GlobalErrorMessages.CannotFindMember);
			}

			var updatedMember = await this.membersService.UpdateOneAsync(member, request.NewFirstName, request.NewMiddleName, request.NewLastName, request.NewNickName, request.NewPhoneNumber, request.NewPassword);

			var responseData = new
			{
				updatedMember.Id,
				updatedMember.NickName,
				updatedMember.Password
			};

			return this.SuccessResponse(responseData);
		}
	}
}
