using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;

namespace appointment
{
    class Program
    {
        static void Main(string[] args)
        {

            // Create a request for the URL. 		
            WebRequest request = WebRequest.Create("https://online.mfa.gov.ua/api/v1/queue/consulates/97/schedule?date=2021-05-19&dateEnd=2021-05-19&serviceId=840");
            //WebRequest request = WebRequest.Create("https://online.mfa.gov.ua/api/v1/queue/consulates/6/schedule?date=2021-05-19&dateEnd=2021-05-19&serviceId=91");
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Display the status.
            Console.WriteLine(response.StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.

            //List<Date> l  = JsonConvert.DeserializeObject<List<Date>>(responseFromServer);
            //Deserializing Json object from string






            var serializer = new JavaScriptSerializer();
            //var deserializedResult = serializer.Deserialize<List<AppDate>>(responseFromServer);
            List<AppDate> deserializedResult = serializer.Deserialize<List<AppDate>> (responseFromServer);
            //List<AppDate> dates = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<AppDate>>(responseFromServer);


            for (int i = 0; i < deserializedResult.Count; i++)
            {
                AppDate dateObj=deserializedResult[i];
                string date = dateObj.Date.ToShortDateString();
                for (int j = 0; j < dateObj.Time.Count; j++)
                {
                    var times=dateObj.Time[j];
                    for (int k = 0; k < times.Count; k++)
                    {
                        var v = times.ElementAt(k);
                        string key =v.Key;
                        string value =v.Value;
                    }
                }
            }




            Console.WriteLine(responseFromServer);
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
        }
    }
}
