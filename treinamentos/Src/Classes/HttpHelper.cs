using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace treinamentos.Src.Classes {
    class HttpHelper {
        
        public static Uri url = new Uri("http://treinamentos.mobi/api/v1/course/");

        public static bool CheckInternetConnection() {
            if (NetworkInterface.GetIsNetworkAvailable()) {
                return true;
            } else {
                return false;
            }
        }

        public static async Task<String> getAsync() {
            HttpClient client = new HttpClient();
            String json = await client.GetStringAsync(url);
            return json;
        }

        
    }
}
