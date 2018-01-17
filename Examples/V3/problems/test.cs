using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace csharpexamples {
	
	public class test {
		
		public static void Main(string[] args) {
			
			// define access parameters
			string endpoint = "<endpoint>";
			string accessToken = "<access_token>";
				
			try {
				WebClient client = new WebClient();
					
				// send request
				byte[] responseBytes = client.DownloadData("http://" + endpoint + "/api/v3/test?access_token=" + accessToken);
				string responseBody = Encoding.UTF8.GetString(responseBytes);
					
				// process response
				Console.WriteLine(responseBody);

			} catch (WebException exception) {
				WebResponse response = exception.Response;
				HttpStatusCode statusCode = (((HttpWebResponse) response).StatusCode);

				// fetch errors
				if (statusCode == HttpStatusCode.Unauthorized) {
					Console.WriteLine("Invalid access token");
				}
					
				response.Close();
			}
		}
	}
}

