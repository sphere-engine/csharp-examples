using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace csharpexamples {
    
    public class createSubmission {
        
        public static void Main(string[] args) {
            
            // define access parameters
            string endpoint = "<endpoint>";
            string accessToken = "<access_token>";

            try {
                WebClient client = new WebClient ();

                // define request parameters
                NameValueCollection formData = new NameValueCollection ();
                formData.Add("problemId", "42");
                formData.Add("source", "<source_code>");
                formData.Add("compilerId", "11");

                // send request
                byte[] responseBytes = client.UploadValues ("https://" + endpoint + "/api/v4/submissions?access_token=" + accessToken, "POST", formData);
                string responseBody = Encoding.UTF8.GetString(responseBytes);

                // process response
                Console.WriteLine(responseBody);

            } catch (WebException exception) {
                WebResponse response = exception.Response;
                HttpStatusCode statusCode = (((HttpWebResponse)response).StatusCode);

                // fetch errors
                if (statusCode == HttpStatusCode.Unauthorized) {
                    Console.WriteLine("Invalid access token");
                } else if(statusCode == HttpStatusCode.PaymentRequired) {
                    Console.WriteLine("Unable to create submission");
                } else if(statusCode == HttpStatusCode.BadRequest) {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    Console.WriteLine(reader.ReadToEnd());
                }

                response.Close();
            }
        }
    }
}

