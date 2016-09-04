using System.Collections.Generic;

using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Generator
{
	public class SletatRepository
	{
		readonly IMongoCollection<Hotel> hotels;

		public SletatRepository (string mongoConnectionString)
		{
			var mongoUrl = new MongoUrl (mongoConnectionString);
			var client = new MongoClient (mongoUrl);
			var db = client.GetDatabase (mongoUrl.DatabaseName);
			hotels = db.GetCollection<Hotel> ("hotels");
		}

		public IEnumerable<Hotel> FindHotelsForCountry (int countryId)
		{
			var filter = Builders<Hotel>.Filter.Eq (h => h.CountryId, countryId);
			return hotels.Find (filter).ToCursor ().ToEnumerable ();
		}

		public static void InitMappings ()
		{
			BsonClassMap.RegisterClassMap<Hotel> (cm => {
				cm.AutoMap ();
				cm.SetIgnoreExtraElements (true);
			});

			BsonClassMap.RegisterClassMap<HotelFacility> (cm => {
				cm.AutoMap ();
				cm.MapProperty (hf => hf.HotelFacilityId).SetElementName ("Id");
				//cm.SetIgnoreExtraElements (true);
			});

			BsonClassMap.RegisterClassMap<Facility> (cm => {
				cm.AutoMap ();
				cm.MapProperty (f => f.FacilityId).SetElementName ("Id");
				//cm.SetIgnoreExtraElements (true);
			});
		}
	}
}
