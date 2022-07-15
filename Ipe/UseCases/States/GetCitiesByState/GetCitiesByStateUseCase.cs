using System;
using Ipe.Domain.Errors;
using Ipe.UseCases.Interfaces;

namespace Ipe.UseCases.GetCitiesByState
{
	public class GetCitiesByStateUseCase: IUseCase<GetCitiesByStateUseCaseInput, GetCitiesByStateUseCaseOutput>
	{
		private readonly IStateRepository _stateRepository;

		public GetCitiesByStateUseCase(IStateRepository stateRepository)
		{
			_stateRepository = stateRepository;
		}

        public Task<GetCitiesByStateUseCaseOutput> Run(GetCitiesByStateUseCaseInput Input)
        {
			var State = _stateRepository.FindStateByInitial(Input.State);

			if (State is null)
				throw new InvalidStateException();

			var Output = new GetCitiesByStateUseCaseOutput();

			foreach(var City in State.Cities)
            {
				Output.Cities.Add(City.Name);
            }

			Output.Cities.Sort((X, Y) => string.Compare(X, Y));

			return Task.FromResult(Output);
		}
    }
}

