using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SyncDonThuService
{
    public class ApiService
    {
        public HttpResponseMessage PostApi(string Authorization, string Accept, string Uri, HttpContent content)
        {
            try
            {
                var client = new HttpClient();
                if (Accept != null && Accept != "")
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Accept));
                if (Authorization != null && Authorization != "")
                    client.DefaultRequestHeaders.Add("Authorization", Authorization);
                using (client)
                {
                    var response = client.PostAsync(Uri, content).Result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                Utilities.WriteLog(ex);
                return null;
            }
        }
        public HttpResponseMessage GetApi(string url)
        {
            try
            {
                var client = new HttpClient();
              
                using (client)
                {
                    var response = client.GetAsync(url).Result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                Utilities.WriteLog(ex);
                return null;
            }
        }
       

    }
}