using System;
namespace Samauma.UseCases
{
	public interface IUseCase<Input, Output>
	{
		Task<Output> Run(Input Input);
	}
}

