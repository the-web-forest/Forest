using Ipe.UseCases.ListTrees.DTOS;

namespace Ipe.UseCases.ListTrees
{
    public class ListTreesUseCaseOutput
	{
		public int TotalPages { get; set; }
		public int CurrentPage { get; set; }
		public int ItemsPerPage { get; set; }
		public List<LightTree> Trees { get; set; }
	}
}