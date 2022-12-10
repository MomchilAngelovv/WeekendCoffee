namespace WeekendCoffee.Services
{
	using Microsoft.EntityFrameworkCore;
	using System.Globalization;
	using WeekendCoffee.Data;

	public interface IMembersService
	{
		Task<Member> InsertOneAsync(string firstName, string middleName, string lastName, string nickName, string phoneNumber, string password);
		Task<Member> GetOneAsync(int id);
		Task<Member> GetOneAsync(string nickName);
		Task<List<Member>> GetManyAsync();
		Task<Member> UpdateOneAsync(Member member, string newFirstName, string newMiddleName, string newLastName, string newNickName, string newPhoneNumber, string newPassword);
	}

	public class MembersService : IMembersService
	{
		private readonly WeekendCoffeeDbContext db;

		public MembersService(
			WeekendCoffeeDbContext db)
		{
			this.db = db;
		}

		public async Task<Member> InsertOneAsync(string firstName, string middleName, string lastName, string nickName, string phoneNumber, string password)
		{
			var newMember = new Member
			{
				FirstName = firstName,
				MiddleName = middleName,
				LastName = lastName,
				NickName = nickName,
				PhoneNumber = phoneNumber,
				Password = password
			};

			await this.db.Members.AddAsync(newMember);
			await this.db.SaveChangesAsync();

			return newMember;
		}

		public async Task<Member> GetOneAsync(int id)
		{
			return await this.db.Members.SingleOrDefaultAsync(m => m.Id == id);
		}

		public async Task<Member> GetOneAsync(string nickName)
		{
			return await this.db.Members.SingleOrDefaultAsync(m => m.NickName == nickName);
		}

		public async Task<List<Member>> GetManyAsync()
		{
			return await this.db.Members.ToListAsync();
		}

		public async Task<Member> UpdateOneAsync(Member member, string newFirstName, string newMiddleName, string newLastName, string newNickName, string newPhoneNumber, string newPassword)
		{
			if (!string.IsNullOrWhiteSpace(newFirstName))
			{
				member.FirstName = newFirstName;
			}

			if (!string.IsNullOrWhiteSpace(newMiddleName))
			{
				member.MiddleName = newMiddleName;
			}

			if (!string.IsNullOrWhiteSpace(newLastName))
			{
				member.LastName = newLastName;
			}

			if (!string.IsNullOrWhiteSpace(newNickName))
			{
				member.NickName = newNickName;
			}

			if (!string.IsNullOrWhiteSpace(newPhoneNumber))
			{
				member.PhoneNumber = newPhoneNumber;
			}

			if (!string.IsNullOrWhiteSpace(newPassword))
			{
				member.Password = newPassword;
			}

			await this.db.SaveChangesAsync();

			return member;
		}
	}
}
