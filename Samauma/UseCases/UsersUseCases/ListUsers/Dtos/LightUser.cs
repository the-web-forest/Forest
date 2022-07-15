using Samauma.Domain.Models;

namespace Samauma.UseCases.ListUsers.Dtos
{
	public class LightUser
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Email { get; set; }

        public static LightUser FromUser(User User)
        {
            return new LightUser
            {
                City = User.City,
                State = User.State,
                Id = User.Id,
                Email = User.Email,
                Name = User.Name
            };
        }
    }
}

