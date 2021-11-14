using System.IO;
using System.Net;

namespace ClientApps
{
    public static class JsonDataHelper
    {
        public static string JsonDataPost(string jsonData, string fToken, string aToken, string url)
        {
            var result = string.Empty;
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            httpRequest.Accept = "application/json";
            httpRequest.Headers.Add("Authorization", $"Bearer {aToken}");
            httpRequest.Headers.Add("F-TOKEN", $"{fToken}");

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
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

        internal static string GetJsonData(string url, string token)
        {
            var result = string.Empty;
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";
            httpRequest.ContentType = "application/json";
            httpRequest.Accept = "application/json";
            httpRequest.Headers.Add("Authorization", $"Bearer {token}");

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            if (httpResponse.StatusCode == HttpStatusCode.OK)
                return result;
            return result;
        }

        public static string GenerateTokenJsonDataPost(string jsonData, string url)
        {
            string responseResult = string.Empty;
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            httpRequest.Accept = "application/json";
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                responseResult = streamReader.ReadToEnd();
            }
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                //var ftoken = httpResponse.Headers["F-TOKEN"];
                //return new KeyValuePair<string, string>(ftoken, responseResult);
                return responseResult;
            }
            return responseResult;
        }
    }
}
