namespace Samauma.UseCases.AdmnistratorLogin
{

	public class OutputUser
    {
		public string Id { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
    }

	public class AdmnistratorLoginUseCaseOutput
    {
		public string AccessToken { get; set; }
		public string TokenType { get; set; }
		public OutputUser User { get; set; }
	}
}

