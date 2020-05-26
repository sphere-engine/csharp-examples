using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace csharpexamples {
	
	public class getSubmission {
		
		public static void Main(string[] args) {
			
			// define access parameters
			string endpoint = "<endpoint>";
			string accessToken = "<access_token>";

			// define request parameters
			int submissionId = 2017;

			try {
				WebClient client = new WebClient();

				// send request
				byte[] responseBytes = client.DownloadData("https://" + endpoint + "/api/v4/submissions/" + submissionId + "?access_token=" + accessToken);
				string responseBody = Encoding.UTF8.GetString(responseBytes);

				// process response
				Console.WriteLine(responseBody);

			} catch (WebException exception) {
				WebResponse response = exception.Response;
				HttpStatusCode statusCode = (((HttpWebResponse) response).StatusCode);

				// fetch errors
				if (statusCode == HttpStatusCode.Unauthorized) {
					Console.WriteLine("Invalid access token");
				} else if (statusCode == HttpStatusCode.Forbidden) {
					Console.WriteLine("Access denied");
				} else if (statusCode == HttpStatusCode.NotFound) {
					Console.WriteLine("Submission not found");
				}

				response.Close();
			}
		}
	}
}

