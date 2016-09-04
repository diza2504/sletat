using System;
using System.Collections.Generic;

using Data;

namespace PullDescriptions
{
	class MainClass
	{
		const string mongoConnStr = "mongodb://127.0.0.1/sletat";

		public static void Main (string [] args)
		{
			Repository.InitMappings ();

			var countryId = int.Parse (args [0]);
			var path = args [1];

			var repo = new Repository (mongoConnStr);
			var foundHotels = repo.FindHotelsForCountry (countryId);
		}

		static void PullDescription (IEnumerable<Hotel> hotels)
		{
			foreach (var h in hotels)
				PullDescription (h);
		}

		static void PullDescription (Hotel hotel)
		{
			throw new NotImplementedException ();
		}
	}
}
