namespace Ipe.UseCases.Login
{

	public class OutputUser
    {
		public string Id { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
    }

	public class LoginUseCaseOutput
	{
		public string AccessToken { get; set; }
		public string TokenType { get; set; }
		public OutputUser User { get; set; }
	}
}

