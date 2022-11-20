namespace WeekendCoffee.Services
{
	using Microsoft.EntityFrameworkCore;

	using WeekendCoffee.Data;

	public interface IMembersService
	{
		Task<Member> AddMemberAsync(string firstName, string middleName, string lastName, string nickName, string phoneNumber);
		Task<Member> GetOneAsync(int id);
	}

	public class MembersService : IMembersService
	{
		private readonly WeekendCoffeeDbContext db;

		public MembersService(
			WeekendCoffeeDbContext db)
		{
			this.db = db;
		}

		public async Task<Member> AddMemberAsync(string firstName, string middleName, string lastName, string nickName, string phoneNumber)
		{
			var newMember = new Member
			{
				FirstName = firstName,
				MiddleName = middleName,
				LastName = lastName,
				NickName = nickName,
				PhoneNumber = phoneNumber
			};

			await this.db.Members.AddAsync(newMember);
			await this.db.SaveChangesAsync();

			return newMember;
		}

		public async Task<Member> GetOneAsync(int id)
		{
			return await this.db.Members.SingleOrDefaultAsync(m => m.Id == id);
		}
	}
}
