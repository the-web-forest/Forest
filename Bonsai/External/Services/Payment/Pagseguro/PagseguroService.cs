using System;
using System.Text;
using System.Text.Json;
using Bonsai.Domain.Enums;
using Bonsai.Domain.Models;
using Bonsai.External.Services.Payment.Pagseguro.Adapters;
using Bonsai.External.Services.Payment.Pagseguro.DTOS;
using Bonsai.External.Services.Payment.Pagseguro.Enums;
using Bonsai.Helpers;
using Bonsai.UseCases.Interfaces.Services;
using Bonsai.UseCases.Interfaces.Services.Payment;
using Bonsai.UseCases.NewPaymentUseCase;
using Microsoft.Net.Http.Headers;

namespace Bonsai.External.Services.Payment.Pagseguro
{
	public class PagseguroService : IPaymentService
	{

		private readonly IConfiguration _configuration;
		private readonly string _baseUrl;
		private readonly string _authorization;
		private readonly IHttpClientFactory _httpClientFactory;

		public PagseguroService(
			IConfiguration configuration,
			IHttpClientFactory httpClientFactory
			)
		{
			_configuration = configuration;
			_httpClientFactory = httpClientFactory;
			_baseUrl = _configuration["Payment:BaseUrl"];
			_authorization = _configuration["Payment:Authorization"];
		}

		public async Task<PaymentOutput> Pay(NewPaymentUseCaseInput Input, Order Order)
		{ 
			var Url = _baseUrl + "charges";
			var HttpClient = GetHttpClient();
			var Payload = NewOrderPayloadAdapter.BuildPayload(Input, Order);
			var Body = JsonSerializer.Serialize(Payload).ToString();
			var HttpRequest = new HttpRequestMessage(HttpMethod.Post, Url)
			{
				Content = new StringContent(Body, Encoding.UTF8, "application/json")
			};

			var HttpResponse = await HttpClient.SendAsync(HttpRequest);
			var ResponseBody = HttpResponse.Content.ReadAsStringAsync().Result;
			var RequestBody = JsonSerializer.Serialize(Payload).ToString();

			var Output = new PaymentOutput
			{
				PaymentRequest = RequestBody,
				PaymentResponse = ResponseBody,
			};

			try
			{
				var ResponseObject = JsonSerializer.Deserialize<NewOrderResponsePayload>(ResponseBody);
				Output.IsPaymentSuccess = IsPaymentSuccess(ResponseObject);
				Output.PaymentStatus = GetPaymentStatus(ResponseObject);
            } catch (Exception)
            {
				Output.IsPaymentSuccess = false;
				Output.PaymentStatus = PaymentStatus.ERROR.ToString();
			}

			return Output;
        }

		private HttpClient GetHttpClient()
        {
			var Client = _httpClientFactory.CreateClient();
			Client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", _authorization);
			return Client;
        }

		private static bool IsPaymentSuccess(NewOrderResponsePayload Response)
        {
			var IsSuccessStatus = Response.Status == PagseguroPaymentStatus.PAID.ToString();
			var IsSuccessMessage = Response.PaymentResponse.Message == PagseguroPaymentMessage.SUCESSO.ToString();
			return IsSuccessStatus && IsSuccessMessage;
		}

		private static string GetPaymentStatus(NewOrderResponsePayload Response)
		{
			return Response.Status;
		}

	}
}

