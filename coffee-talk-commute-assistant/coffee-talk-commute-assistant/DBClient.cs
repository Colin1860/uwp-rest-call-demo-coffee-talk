using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coffee_talk_commute_assistant
{
   class DBClient
    {

        private const string DB_PATH = "https://api.deutschebahn.com/freeplan/v1";
        private const string GET_LOCATION = "/location/";
        private const string GET_ARRIVALS = "/arrivalBoard/";


        public static async Task<string> getLocationId(string location)
        {
            string httpResponseBody = await getRequest(DB_PATH + GET_LOCATION + location);


            var parsedJArray = JArray.Parse(httpResponseBody).DefaultIfEmpty();

            return parsedJArray.FirstOrDefault()?["id"].ToString();
        }

        public static async Task<List<Connection>> getPossibleTrains(string origin, string destinationId, DateTime time)
        {
            string date = time.ToString("yyyy-MM-ddTHH:mm");
            string httpResponseBody = await getRequest(DB_PATH + GET_ARRIVALS + destinationId+"?date="+date);

            var parsedJArray = JArray.Parse(httpResponseBody).DefaultIfEmpty();

            List<JToken> lists = parsedJArray.Where(x => x["origin"].ToString() == origin).ToList();
            return lists.Select(x => x.ToObject<Connection>()).ToList();
        }

        private static async Task<string> getRequest(string path)
        {
            //Create an HTTP client object
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            path = path.Replace(" ", "%20");

            Uri requestUri = new Uri(path);

            //Send the GET request asynchronously and retrieve the response as a string.
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();

            string httpResponseBody;

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            return httpResponseBody;
        }
        
   }
}
