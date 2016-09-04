using System;
using System.Collections.Generic;
using System.Net.Http;

using Data;

namespace PullDescriptions
{
	class MainClass
	{
		const string mongoConnStr = "mongodb://127.0.0.1/sletat";
		static HttpClient client;

		public static void Main (string [] args)
		{
			Repository.InitMappings ();

			var countryId = int.Parse (args [0]);

			var repo = new Repository (mongoConnStr);
			var foundHotels = repo.FindHotelsForCountry (countryId);

			client = new HttpClient ();

			PullDescription (foundHotels, repo);
		}

		static void PullDescription (IEnumerable<Hotel> hotels, Repository repo)
		{
			foreach (var h in hotels) {
				var descr = PullDescription (h);
				repo.UpdateDescription (h, descr);
			}
		}

		static string PullDescription (Hotel hotel)
		{
			var url = GetUrl (hotel.Description);
			return client.GetStringAsync (url).Result;
		}

		static string GetUrl (string description)
		{
			const string marker = "src=\"";
			var markerStart = description.IndexOf (marker, StringComparison.InvariantCulture);
			if (markerStart < 0)
				return null;

			int start = markerStart + marker.Length;
			var end = description.IndexOf ('"', start);

			return description.Substring (start, end - start);
		}
	}
}
