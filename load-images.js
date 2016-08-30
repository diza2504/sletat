var CountryId = 40;
db.hotels.aggregate([
	{ $match: {"CountryId" : CountryId}},
	{ $project: {"_id": 0, "ImageUrls": 1}},
	{ $unwind: "$ImageUrls" }
]).map(function (e) {
	return e.ImageUrls;
}).forEach(function(item) {
	print(item);
});