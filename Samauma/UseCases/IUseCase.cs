using System;
namespace Ipe.UseCases
{
	public interface IUseCase<Input, Output>
	{
		Task<Output> Run(Input Input);
	}
}

