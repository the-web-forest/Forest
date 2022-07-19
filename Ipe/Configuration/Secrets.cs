using System;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Ipe.Domain;

namespace Ipe.Configuration
{
    public class Secrets
    {
       public static void Configure(WebApplicationBuilder builder) {

            var VaultUri = Environment.GetEnvironmentVariable("VAULT_URL");

            SecretClientOptions options = new SecretClientOptions()
            {
                Retry = {
                    Delay = TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                 }
            };

            var client = new SecretClient(new Uri(VaultUri), new DefaultAzureCredential(), options);

            var DatabaseConnectionString = client.GetSecret("Trees-Databases-Cosmos-ConnectionString").Value.Value;
            var DatabaseName = client.GetSecret("Trees-Databases-Cosmos-Ipe-Name").Value.Value;

            var StorageConnectionString = client.GetSecret("Trees-Storage-ConnectionString").Value.Value;
            var StorageContainerName = client.GetSecret("Trees-Storage-Container-Name").Value.Value;

            var JwtKey = client.GetSecret("Trees-Ipe-Jwt-Key").Value.Value;

            var EmailApiKey = client.GetSecret("Trees-Email-ApiKey").Value.Value;
            var EmailFromEmail = client.GetSecret("Trees-Email-From-Email").Value.Value;
            var EmailFromName = client.GetSecret("Trees-Email-From-Name").Value.Value;
            var EmailTemplateWelcome = client.GetSecret("Trees-Email-Templates-Welcome").Value.Value;
            var EmailTemplatePasswordReset = client.GetSecret("Trees-Email-Templates-PasswordReset").Value.Value;
            var EmailUrlsWelcome = client.GetSecret("Trees-Email-Urls-Welcome").Value.Value;
            var EmailUrlsPasswordReset = client.GetSecret("Trees-Email-Urls-PasswordReset").Value.Value;

            builder.Configuration["Database:ConnectionString"] = DatabaseConnectionString;
            builder.Configuration["Database:Name"] = DatabaseName;

            builder.Configuration["Storage:ConnectionString"] = StorageConnectionString;
            builder.Configuration["Storage:TreeContainerName"] = StorageContainerName;

            builder.Configuration["Jwt:Key"] = JwtKey;

            builder.Configuration["Email:ApiKey"] = EmailApiKey;
            builder.Configuration["Email:FromEmail"] = EmailFromEmail;
            builder.Configuration["Email:FromName"] = EmailFromName;
            builder.Configuration["Email:Templates:WelcomeEmail"] = EmailTemplateWelcome;
            builder.Configuration["Email:Templates:PasswordResetEmail"] = EmailTemplatePasswordReset;
            builder.Configuration["Email:Urls:WelcomeEmail"] = EmailUrlsWelcome;
            builder.Configuration["Email:Urls:PasswordResetEmail"] = EmailUrlsPasswordReset;
        }

    }
}

