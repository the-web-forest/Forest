namespace Ipe.UseCases.GetStates;

public class StateOutput
{
    public string Name { get; set; }
    public string Initial { get; set; }
}

public class GetStatesUseCaseOutput
{
    public List<StateOutput> States { get; set; } = new List<StateOutput>();
}
