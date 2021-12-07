using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Gateway.WebApi
{
    public class RequestHandler : DelegatingHandler
    {
        private readonly IConfiguration _configuration;

        public RequestHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestUrl = request.RequestUri.ToString();
            Trace.WriteLine(requestUrl);
            var authServer = _configuration.GetValue<string>("BaseUrl:Auth.Server");

            request.Headers.TryGetValues("Authorization", out var tokenInformation);

            if (tokenInformation is null && !requestUrl.Contains("authenticate"))
            {
                return await ReturnBadRequest("The request could not be understood by the server.");
            }
            else if (tokenInformation != null)
            {
                var token = tokenInformation.FirstOrDefault().Split(" ").Last();
                var url = $"{authServer}api/Users/validateJwtToken?token=" + token;
                var httpClient = new HttpClient();
                var req = new HttpRequestMessage(HttpMethod.Get, url);
                req.Headers.Add("Accept", "application/json");

                var response = await httpClient.SendAsync(req);
                var userId = await response.Content.ReadAsStringAsync();
                if (userId == null || userId.Length == 0)
                    return await ReturnUnauthorizedRequest("Unauthorized Users or Invalid token!");
            }

            //do stuff and optionally call the base handler..
            return await base.SendAsync(request, cancellationToken);
        }

        private Task<HttpResponseMessage> ReturnBadRequest(string message) => Task.Factory.StartNew(() =>
           new HttpResponseMessage(HttpStatusCode.BadRequest)
           {
               Content = new StringContent(message)
           });

        private Task<HttpResponseMessage> ReturnUnauthorizedRequest(string message) => Task.Factory.StartNew(() =>
         new HttpResponseMessage(HttpStatusCode.Unauthorized)
         {
             Content = new StringContent(message)
         });
    }
}
