using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using SletatLib.Sletat;
using Newtonsoft.Json;

namespace SletatPlayground
{
	class Program
	{
		static void Main(string[] args)
		{
			var configData = File.ReadAllText("config.json");
			var config = JsonConvert.DeserializeObject<Config>(configData);
			var auth = new AuthData
			{
				Login = config.Login,
				Password = config.Password
			};

			if (!Directory.Exists("output"))
				Directory.CreateDirectory("output");

			using (var service = new Soap11GateClient())
			{
				foreach (var hotelId in args.Select(arg => int.Parse(arg)))
				{
					var hotelInfo = service.GetHotelInformation(auth, hotelId, string.Empty, false);
					var hotelStr = JsonConvert.SerializeObject(hotelInfo);
					File.WriteAllText(Path.Combine("output", $"{hotelId}.json"), hotelStr);
				}
			}
		}
	}
}
