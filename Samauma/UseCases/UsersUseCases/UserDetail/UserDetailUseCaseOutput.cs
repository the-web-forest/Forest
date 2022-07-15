using System;
namespace Samauma.UseCases.UserDetail
{
	public class UserDetailUseCaseOutput
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public bool EmailVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}

