
namespace Generator
{
	class MainClass
	{
		const string mongoConnStr = "mongodb://127.0.0.1/sletat";

		public static void Main (string [] args)
		{
			SletatRepository.InitMappings ();

			var countryId = int.Parse (args [0]);
			var path = args [1];

			var repo = new SletatRepository (mongoConnStr);
			var foundHotels = repo.FindHotelsForCountry (countryId);

			var xls = new XlsManager (Template.Meta);
			xls.PopulateXls (path, foundHotels);
		}
	}
}
