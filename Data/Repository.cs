using System.Collections.Generic;

using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Data
{
	public class Repository
	{
		readonly IMongoCollection<Hotel> hotels;

		public Repository (string mongoConnectionString)
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

		public void UpdateDescription (Hotel hotel, string description)
		{
			var idFilter = Builders<Hotel>.Filter.Eq (h => h.Id, hotel.Id);
			var update = Builders<Hotel>.Update.Set(h => h.HtmlDescription, description);
			hotels.UpdateOne (idFilter, update);
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
