using Samauma.UseCases.ListTrees.DTOS;

namespace Samauma.UseCases.ListTrees
{
    public class ListTreesUseCaseOutput
	{
		public int TotalPages { get; set; }
		public int CurrentPage { get; set; }
		public int ItemsPerPage { get; set; }
		public List<LightTree> Trees { get; set; }
	}
}