using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace csharpexamples
{
	public class getSubmission
	{
		public static void Main(string[] args)
		{
			string endpoint = "<endpoint>";
			string accessToken = "<access_token>";

			int submissionId = 2016;

			try {
				WebClient client = new WebClient();

				byte[] responseBytes = client.DownloadData("http://" + endpoint + "/api/v3/submissions/" + submissionId + "?access_token=" + accessToken);
				string responseBody = Encoding.UTF8.GetString(responseBytes);

				Console.WriteLine(responseBody);

			} catch (WebException exception) {
				WebResponse response = exception.Response;
				HttpStatusCode statusCode = (((HttpWebResponse) response).StatusCode);

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

