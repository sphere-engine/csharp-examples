using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace csharpexamples
{
	public class createSubmission
	{
		public static void Main(string[] args)
		{
			string endpoint = "<endpoint>";
			string accessToken = "<access_token>";

			try {
				WebClient client = new WebClient ();

				NameValueCollection formData = new NameValueCollection ();
				formData.Add("source", "<source_code>");
				formData.Add("compilerId", "11");

				byte[] responseBytes = client.UploadValues ("http://" + endpoint + "/api/v3/submissions?access_token=" + accessToken, "POST", formData);
				string responseBody = Encoding.UTF8.GetString(responseBytes);

				Console.WriteLine(responseBody);

			} catch (WebException exception) {
				WebResponse response = exception.Response;
				HttpStatusCode statusCode = (((HttpWebResponse)response).StatusCode);

				if (statusCode == HttpStatusCode.Unauthorized) {
					Console.WriteLine("Invalid access token");
				}

				response.Close();
			}
		}
	}
}

