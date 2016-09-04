using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

using Data;

namespace Generator
{
	public static class Template
	{
		static readonly NumberFormatInfo nfi = new NumberFormatInfo {
			NumberDecimalSeparator = "."
		};

		public static Dictionary<string, Func<Hotel, string>> Meta { get; } = new Dictionary<string, Func<Hotel, string>> {
			{"article : Art", h => h.HotelId.ToString() },
			{"name : Name", h => h.Name },
			{"cf_countryid : CountryId", h => h.CountryId.ToString() },
			{"cf_countryname : Страна", h => h.CountryName },
			{"cf_resortid : ResortId", h => h.ResortId.ToString() },
			{"cf_region : Регион", h => h.Region },
			{"cf_resort : Курорт (район)", h => h.Resort },
			{"cf_hotelid : HotelId", h => h.HotelId.ToString () },
			{"cf_oldlatinname : OldLatinName", h => h.OldLatinName },
			{"cf_oldcyrillicname : OldCyrillicName", h => h.OldCyrillicName },
			{"cf_starid : StarId", h => h.StarId.ToString () },
			{"cf_starname : Категория отеля", h => h.StarName },
			{"cf_hotelrate : Рейтинг отеля", h => h.HotelRate.ToString() },
			{"cf_roomscount : Количество номеров", h => h.RoomsCount.ToString() },
			{"cf_square : Площадь отеля", h => h.Square },
			{"cf_buildingdate : Дата постройки:", h => h.BuildingDate },
			{"cf_renovation : Дата реконструкции:", h => h.Renovation },
			{"cf_citycenterdistance : Расстояние до центра:", h => h.CityCenterDistance?.ToString() },
			{"cf_distancetolifts : Расстояние до подъемников:", h => h.DistanceToLifts },
			{"cf_nativeaddress : NativeAddress", h => h.NativeAddress },
			{"cf_airportdistance : Расстояние до аэропорта:", h => h.AirportDistance?.ToString() },
			{"cf_postindex : PostIndex", h => h.PostIndex },
			{"cf_housenumber : HouseNumber", h => h.HouseNumber },
			{"cf__street_ : \"Street\"", h => h.Street },
			{"cf_phone : Телефон:", h => h.Phone },
			{"cf_fax : Факс:", h => h.Fax },
			{"cf_email : E-mail:", h => h.Email },
			{"cf_site : Сайт:", h => h.Site },
			{"cf_latitude : Latitude", h => h.Longitude?.ToString () },
			{"cf_longitude : Longitude", h => h.Latitude?.ToString () },
			{"cf__ratingmeal_ : \"RatingMeal\"", h => h.RatingMeal?.ToString() },
			{"cf__ratingoverall_ : \"RatingOverall\"", h => h.RatingOverall?.ToString () },
			{"cf__ratingplace_ : \"RatingPlace\"", h => h.RatingPlace?.ToString () },
			{"cf__ratingservice_ : \"RatingService\"", h => h.RatingService?.ToString ()},
			{"cf__video_ : \"Video\"", h => h.Video },
			{"cf__area_ : \"Area\"", h => h.Area },
			{"cf__district_ : \"District\"", h => h.District },
			{"cf__error_ : \"Error\"", h => h.Error.ToString () },
			{"cf__errordescription_ : \"ErrorDescription\"", h => h.ErrorDescription },
			{"cf__address_ : \"Address\"", h => h.Address },
			{"cf_api_google_maps : Посмотреть отель на карте:", h => $@"<style><!--
#map {{height:250px;width:250px}}
--></style>
<div id=""map""></div>
<p>
<script type=""text/javascript"">// <![CDATA[
function initMap() {{var myLatLng = {{lat: {h.Latitude?.ToString(nfi)}, lng: {h.Longitude?.ToString(nfi)}}};var map = new google.maps.Map(document.getElementById('map'), {{zoom: 12,center: myLatLng}});var marker = new google.maps.Marker({{position: myLatLng,map: map,title: '{h.Name}'}});}} // ]]></script>
<script async="""" defer=""defer"" src=""https://maps.googleapis.com/maps/api/js?key=AIzaSyDCtUvbAM6TqKrEEsu6jY_favvXdxkgp9w&callback=initMap"" type=""text/javascript""></script>
</p>"},
			{"cf_sletat_script : Туры в этот отель:", h => $@"<p>
<script type=""text/javascript"" src=""//ui.sletat.ru/module-4.0/core.js"" charset=""utf-8""></script>
<script type=""text/javascript"">// <![CDATA[
sletat.FrameSearch.$create({{city:1264,country:{h.CountryId},resorts:[{h.ResortId}],hotels:[{h.HotelId}],nightsMin:5,sta:!0,namespace:""macstyle"",usePricePerson:!0,enabledCurrencies:[""RUB""],agencyContact1:{{header:""Сеть туристических агентств Тур Малина"",phone:""8-800-700-85-48"",email:""tmalina.ru@yandex.ru"",content:""нет пока дополнительной информации, тут могут быть адреса офисов""}},agencyContact2:{{header:""Сеть туристических агентств Тур Малина"",phone:""8-800-700-85-48"",email:""tmalina.ru@yandex.ru"",content:""нет пока дополнительной информации, тут могут быть адреса офисов""}},googleMapKey:""AIzaSyDCtUvbAM6TqKrEEsu6jY_favvXdxkgp9w"",useCard:!1,useAccountSettings:!1,useTicketsIncludedControl:!0,dateOffset:1,dateRange:89}});
// ]]></script>
</p>
<p><span class=""sletat-copyright"">Идет загрузка модуля <a href=""http://sletat.ru/"" title=""поиск туров"" target=""_blank"">поиска туров</a> &hellip;</span></p>"},
			{"cf__id_6_name_plaznaa_linia : Расстояние до пляжа:", h => FlattenByName(h, "Пляжная линия")},
			{"cf__id_1_name_internet_ : \"Интернет\"", h => FlattenByName(h, "Интернет")},
			{"cf__id_2_name_obsie_uslugi_ : \"Общие услуги\"", h => FlattenByName(h, "Общие услуги")},
			{"cf__id_3_name_parkovka_ : \"Парковка\"", h => FlattenByName(h, "Парковка")},
			{"cf__id_5_name_sport_ : \"Спорт\"", h => FlattenByName (h, "Спорт")},
			{"cf__id_7_name_biznes_uslugi_ : \"Бизнес-услуги\"", h => FlattenByName (h, "Бизнес-услуги")},
			{"cf__id_8_name_udobstva_v_nom : \"Удобства в номерах\"", h => FlattenByName (h, "Удобства в номерах")},
			{"cf__id_9_name_special_nye_no : \"Специальные номера\"", h => FlattenByName (h, "Специальные номера")},
			{"cf__id_10_name_pitanie_ : \"Питание\"", h => FlattenByName (h, "Питание")},
			{"cf__id_11_name_transport_ : \"Транспорт\"", h => FlattenByName(h, "Транспорт")},
			{"cf__id_12_name_uslugi_po_cis : \"Услуги по чистке одежды\"", h => FlattenByName(h, "Услуги по чистке одежды")},
			{"cf__id_13_name_razvlecenia_ : \"Развлечения\"", h => FlattenByName(h, "Развлечения")},
			{"cf__id_14_name_otdyh_na_vode : \"Отдых на воде\"", h => FlattenByName(h, "Отдых на воде")},
			{"cf__id_15_name_zdorov_e_i_kr : \"Здоровье и красота\"", h => FlattenByName(h, "Здоровье и красота")},
			{"cf__id_16_name_kurenie_ : \"Курение\"", h => FlattenByName(h, "Курение")},
			{"cf__id_18_name_uslugi_dla_de : \"Услуги для детей\"", h => FlattenByName(h, "Услуги для детей")},
			{"cf__id_19_name_plaz_ : \"Пляж\"", h => FlattenByName(h, "Пляж")},
			{"cf__id_20_name_tip_plaza_ : \"Тип пляжа\"", h => FlattenByName(h, "Тип пляжа")},
			{"cf__id_21_name_tip_otela_ : \"Тип отеля\"", h => FlattenByName(h, "Тип отеля")},
			{"cf_description : Описание отеля:", h => h.HtmlDescription }
		};

		static string FlattenByName (Hotel h, string key)
		{
			var hFacilities = h.HotelFacilities;
			if (hFacilities == null)
				return string.Empty;

			var hf = hFacilities.FirstOrDefault (f => f.Name == key);
			if (hf == null || hf.Facilities == null)
				return string.Empty;

			var facilities = hf.Facilities.Select (f => string.IsNullOrWhiteSpace (f.Hit) ? f.Name : $"{f.Hit}, {f.Name}")
										  .Select (s => $"<li>{s}</li>");
			return $"<ul>{string.Join (string.Empty, facilities)}</ul>";
		}
	}
}
