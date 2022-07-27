namespace Ipe.UseCases.Interfaces.Repositories
{
	public interface IBaseRepository<T>
	{
		Task Create(T Data);
		Task Update(T Data);
		Task CreateMany(List<T> DataList);
	}
}

