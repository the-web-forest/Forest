using System;
using EllipticCurve.Utils;
using System.Text;
using Ipe.UseCases.Interfaces.Services;
using System.Net;
using System.Text.Json;
using Ipe.UseCases;
using System.Net.Http;
using Ipe.External.Services.DTOs;

namespace Ipe.External.Services
{
    public class PaymentService: IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _paymentUrl;
        private readonly string _paymentToken;

        public PaymentService(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _paymentUrl = _configuration["Payment:BaseUrl"];
            _paymentToken = _configuration["Payment:Token"];
        }

        public async Task<NewPaymentOutput> NewPayment(NewPaymentInput Input)
        {
            var Url = _paymentUrl + "payment/new";
            var HttpClient = GetHttpClient();
            var Body = JsonSerializer.Serialize(Input).ToString();
            var HttpRequest = new HttpRequestMessage(HttpMethod.Post, Url)
            {
                Content = new StringContent(Body, Encoding.UTF8, "application/json")
            };

            var HttpResponse = await HttpClient.SendAsync(HttpRequest);
            var RequestBody = JsonSerializer.Serialize(Input).ToString();

            return BuildResponse(HttpResponse, RequestBody);
        }

        private HttpClient GetHttpClient()
        {
            var Client = _httpClientFactory.CreateClient();
            Client.DefaultRequestHeaders.Add("X-Seed-Key", _paymentToken);
            return Client;
        }

        private static NewPaymentOutput BuildResponse(HttpResponseMessage HttpResponse, string RequestBody)
        {
            var Response = new NewPaymentOutput {
                RequestBody = RequestBody,
                ResponseBody = HttpResponse.Content.ReadAsStringAsync().Result,
                Success = false,
                PaymentId = null
            };

            if(HttpResponse.StatusCode == HttpStatusCode.OK)
            {
                var PaymentResponse = JsonSerializer.Deserialize<NewPaymentResponse>(Response.ResponseBody);
                Response.Success = true;
                Response.PaymentId = PaymentResponse.PaymentId;
            }

            return Response;
        }
    }
}

