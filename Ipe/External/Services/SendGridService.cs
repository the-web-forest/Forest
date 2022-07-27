using Ipe.UseCases.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ipe.External.Services
{
	public class SendGridService: IEmailService
	{

		private readonly SendGridClient _sendGridClient;
		private readonly IConfiguration _configuration;

		public SendGridService(
			SendGridClient sendGridClient,
			IConfiguration configuration
			)
		{
			_sendGridClient = sendGridClient;
			_configuration = configuration;
		}

		public async Task<bool> SendWelcomeEmail(string Email, string Name, string Token, string Role) {
			var WelcomeEmailTemplateId = _configuration["Email:Templates:WelcomeEmail"];
			var From = new EmailAddress(_configuration["Email:FromEmail"], _configuration["Email:FromName"]);
			var To = new EmailAddress(Email, Name);
			var ButtonUrl = BuildWelcomeButtonUrl(Token, Role, Email);
			var TemplateData = new { buttonUrl = ButtonUrl };
			var Message = MailHelper.CreateSingleTemplateEmail(From, To, WelcomeEmailTemplateId, TemplateData);
			var Response = await _sendGridClient.SendEmailAsync(Message);
			return Response.IsSuccessStatusCode;
		}

		private string BuildWelcomeButtonUrl(string Token, string Role, string Email)
        {
			var BaseUrl = _configuration["Email:Urls:WelcomeEmail"];
			BaseUrl = BaseUrl.Replace("{{_EMAIL_}}", Email);
			BaseUrl = BaseUrl.Replace("{{_TOKEN_}}", Token);
			BaseUrl = BaseUrl.Replace("{{_ROLE_}}", Role);
			return BaseUrl;
		}

		public async Task<bool> SendPasswordResetEmail(string Email, string Name, string Token, string Role)
		{
			var UserFirstName = Name.Split(" ")[0];
			var WelcomeEmailTemplateId = _configuration["Email:Templates:PasswordResetEmail"];
			var From = new EmailAddress(_configuration["Email:FromEmail"], _configuration["Email:FromName"]);
			var To = new EmailAddress(Email, Name);
			var ButtonUrl = BuildPasswordResetButtonUrl(Token, Role, Email);
			var TemplateData = new { buttonUrl = ButtonUrl, userName = UserFirstName };
			var Message = MailHelper.CreateSingleTemplateEmail(From, To, WelcomeEmailTemplateId, TemplateData);
			var Response = await _sendGridClient.SendEmailAsync(Message);
			return Response.IsSuccessStatusCode;
		}

        public async Task<bool> SendPlantSuccessEmail(string Email, string Name)
        {
            var UserFirstName = Name.Split(" ")[0];
            var WelcomeEmailTemplateId = _configuration["Email:Templates:PlantSuccessEmail"];
            var From = new EmailAddress(_configuration["Email:FromEmail"], _configuration["Email:FromName"]);
            var To = new EmailAddress(Email, Name);
            var Message = MailHelper.CreateSingleTemplateEmail(From, To, WelcomeEmailTemplateId, null);
            var Response = await _sendGridClient.SendEmailAsync(Message);
            return Response.IsSuccessStatusCode;
        }

        public async Task<bool> SendPlantFailEmail(string Email, string Name)
        {
            var UserFirstName = Name.Split(" ")[0];
            var WelcomeEmailTemplateId = _configuration["Email:Templates:PlantFailEmail"];
            var From = new EmailAddress(_configuration["Email:FromEmail"], _configuration["Email:FromName"]);
            var To = new EmailAddress(Email, Name);
            var Message = MailHelper.CreateSingleTemplateEmail(From, To, WelcomeEmailTemplateId, null);
            var Response = await _sendGridClient.SendEmailAsync(Message);
            return Response.IsSuccessStatusCode;
        }

        private string BuildPasswordResetButtonUrl(string Token, string Role, string Email)
		{
			var BaseUrl = _configuration["Email:Urls:PasswordResetEmail"];
			BaseUrl = BaseUrl.Replace("{{_EMAIL_}}", Email);
			BaseUrl = BaseUrl.Replace("{{_TOKEN_}}", Token);
			BaseUrl = BaseUrl.Replace("{{_ROLE_}}", Role);
			return BaseUrl;
		}
	}
}

