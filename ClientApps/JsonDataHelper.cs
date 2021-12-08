using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace ClientApps
{
    public static class JsonDataHelper
    {
        public static string GetJsonResponseData(dynamic model, string url, string method, string token = "", string fToken = "")
        {
            try
            {
                var result = string.Empty;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = method;
                httpRequest.ContentType = "application/json";
                httpRequest.Accept = "application/json";

                if (!String.IsNullOrWhiteSpace(token))
                    httpRequest.Headers.Add("Authorization", $"Bearer {token}");
                if (!String.IsNullOrWhiteSpace(fToken))
                    httpRequest.Headers.Add("F-TOKEN", $"{fToken}");

                if (model != null)
                {
                    using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                    {
                        var data = JsonConvert.SerializeObject(model);
                        streamWriter.Write(data);
                    }
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                    return result;
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
