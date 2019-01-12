using Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;


namespace Services
{
    public class MobizonService : ISmsService
    {
        private const string apiKey = "kzdb7b227b960a6d1d41c13d3dafcda9f1c01f256429b80bff93cf3998b5fed0258bff";
        private const string host = "https://api.mobizon.kz";

        private readonly IHttpClientFactory _clientFactory;

        public MobizonService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task SendSms(string phoneNumber, string message)
        {
            string requestUri = host + "/service/message/sendsmsmessage?"
                + "recipient=" + phoneNumber
                + "&text=" + message
                + "&apiKey=" + apiKey;

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var client = _clientFactory.CreateClient();
            await client.SendAsync(requestMessage);
        }
    }
}
