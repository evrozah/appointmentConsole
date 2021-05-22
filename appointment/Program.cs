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

using Twilio;
using Twilio.Rest.Api.V2010.Account;


using System.Net.Mail;
using System.Threading;

namespace appointment
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
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
                    List<AppDate> deserializedResult = serializer.Deserialize<List<AppDate>>(responseFromServer);
                    //List<AppDate> dates = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<AppDate>>(responseFromServer);

                    if (deserializedResult.Count > 0)
                    {
                        /* for (int i = 0; i < deserializedResult.Count; i++)
                         {
                             AppDate dateObj = deserializedResult[i];
                             string date = dateObj.Date.ToShortDateString();
                             for (int j = 0; j < dateObj.Time.Count; j++)
                             {
                                 var times = dateObj.Time[j];
                                 for (int k = 0; k < times.Count; k++)
                                 {
                                     var v = times.ElementAt(k);
                                     string key = v.Key;
                                     string value = v.Value;
                                 }
                             }
                         }*/


                        







                        var fromAddress = new MailAddress("dimitreska@gmail.com", "From Name");
                        var toAddress = new MailAddress("dimitreska@gmail.com", "To Name");
                        const string fromPassword = "fTegWCnAT#FvDx";
                        const string subject = "APPOINTMENT";
                        const string body = "APPOINTMENT";

                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                        };
                        using (var message = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = subject,
                            Body = body
                        })
                        {
                            //smtp.Send(message);
                        }
                        Console.WriteLine("APPOINTMENT");


                        // Find your Account SID and Auth Token at twilio.com/console
                        // and set the environment variables. See http://twil.io/secure
                        string accountSid = Environment.GetEnvironmentVariable("ACdb4dc0e6c34ef63c664414a6807a6a54");
                        string authToken = Environment.GetEnvironmentVariable("e7a9534a7a6ac0aff9add741e47e0a2b");

                        TwilioClient.Init(accountSid, authToken);

                        var message2 = MessageResource.Create(
                            body: "This is the ship that made the Kessel Run in fourteen parsecs?",
                            from: new Twilio.Types.PhoneNumber("+13169999690"),
                            to: new Twilio.Types.PhoneNumber("+972524745607")
                        );
                    }
                    else
                    {
                        Console.WriteLine("no appointment");
                    }


                    //Console.WriteLine(responseFromServer);
                    // Cleanup the streams and the response.
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Thread.Sleep(30000);
            }
           
        }
    }
}
