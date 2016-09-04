using System;
using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data
{
	public class Hotel
	{
		public ObjectId Id { get; set; }
		public string Address { get; set; }
		public double? AirportDistance { get; set; }
		public string Area { get; set; }
		public string BuildingDate { get; set; }
		public double? CityCenterDistance { get; set; }
		public int CountryId { get; set; }
		public string CountryName { get; set; }

		public string Description { get; set; }
		public string HtmlDescription { get; set; }

		public string DistanceToLifts { get; set; }
		public string District { get; set; }
		public string Email { get; set; }
		public bool Error { get; set; }
		public string ErrorDescription { get; set; }
		public string Fax { get; set; }
		public List<HotelFacility> HotelFacilities { get; set; }
		public int HotelId { get; set; }
		public double HotelRate { get; set; }
		public string HouseNumber { get; set; }
		public int ImageCount { get; set; }
		public List<string> ImageUrls { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
		public string Name { get; set; }
		public string NativeAddress { get; set; }
		public string OldCyrillicName { get; set; }
		public string OldLatinName { get; set; }
		public string Phone { get; set; }
		public string PostIndex { get; set; }
		public int? RatingMeal { get; set; }
		public int? RatingOverall { get; set; }
		public int? RatingPlace { get; set; }
		public int? RatingService { get; set; }
		public string Region { get; set; }
		public string Renovation { get; set; }
		public string Resort { get; set; }
		public int ResortId { get; set; }
		public int RoomsCount { get; set; }
		public string Site { get; set; }
		public string Square { get; set; }
		public int StarId { get; set; }
		public string StarName { get; set; }
		public string Street { get; set; }
		public string Video { get; set; }

	}

	public class Facility
	{
		public string Hit { get; set; }

		[BsonElement("Id")]
		public int FacilityId { get; set; }
		public string Name { get; set; }
	}

	public class HotelFacility
	{
		public List<Facility> Facilities { get; set; }
		[BsonElement ("Id")]
		public int HotelFacilityId { get; set; }
		public string Name { get; set; }
	}

}