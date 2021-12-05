using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web
{
    public enum RequestMethod
    {
        GET,
        POST,
        PUT,
        DELETE
    }
    public class HTTPClient
    {
        private string _url;
        public HTTPClient(string url)
        {

            _url = url;
        }
        public async Task<string> SendAsync(string suffix, RequestMethod method, string body, HttpStatusCode responseCode)
        {
            return await Task.Run(() =>
            {
                HttpWebRequest request = WebRequest.CreateHttp(_url + suffix);
                request.Method = method.ToString();
                request.ContentType = "application/json";
                request.Expect = "application/json";
                if (body.Length > 0)
                {
                    using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                    {
                        writer.Write(body);
                    }
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != responseCode) throw new Exception($"Bad Request - StatusCode: {response.StatusCode}");
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        return reader.ReadToEnd();
                    }
                }
            });
        }
    }
}
