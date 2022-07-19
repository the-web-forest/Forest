using Samauma.UseCases.ListUsers.Dtos;

namespace Samauma.UseCases.ListUsers
{
	public class ListUsersUseCaseOutput
	{
		public int TotalPages { get; set; }
		public int CurrentPage { get; set; }
		public int ItemsPerPage { get; set; }
		public List<LightUser> Users { get; set; }
	}
}

