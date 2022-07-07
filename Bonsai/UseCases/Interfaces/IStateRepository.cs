using Ipe.Domain.Models;
using Ipe.UseCases.Interfaces.Repositories;

namespace Ipe.UseCases.Interfaces
{
	public interface IStateRepository : IBaseRepository<State>
	{
		List<State> FindAll();
		State FindStateByInitial(string StateInitial);
	}
}

