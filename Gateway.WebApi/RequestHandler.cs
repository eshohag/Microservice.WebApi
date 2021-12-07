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
        public RequestHandler()
        {

        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestUrl = request.RequestUri.ToString();
            Trace.WriteLine(requestUrl);

            request.Headers.TryGetValues("Authorization", out var tokenInformation);
            var token = tokenInformation.FirstOrDefault()?.Split(" ").Last();

            if (token is null && !requestUrl.Contains("authenticate"))
            {
                return await ReturnBadRequest("The request could not be understood by the server.");
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
