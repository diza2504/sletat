using System;
using System.IO;
using System.Collections.Generic;

using NPOI.SS.UserModel;

namespace Generator
{
	public class XlsManager
	{
		readonly Dictionary<string, Func<Hotel, string>> meta;

		public XlsManager (Dictionary<string, Func<Hotel, string>> meta)
		{
			this.meta = meta;
		}

		public void PopulateXls (string path, IEnumerable<Hotel> hotels)
		{
			IWorkbook wb;
			using (var stream = new FileStream (path, FileMode.Open, FileAccess.Read))
				wb = WorkbookFactory.Create (stream);
			ISheet sheet = wb.GetSheetAt (0);

			var headersRow = sheet.GetRow (0);
			var headerToIndex = new Dictionary<string, int> ();
			for (int i = 0; i <= headersRow.LastCellNum; i++) {
				var c = headersRow.GetCell (i);
				if (c == null)
					continue;

				headerToIndex.Add (c.StringCellValue, i);
			}

			int index = 0;
			foreach (var h in hotels) {
				PopulateXls (sheet, headerToIndex, h);
				Console.WriteLine ($"{++index} {h.Name}");
			}

			using (var stream = new FileStream (path, FileMode.Open, FileAccess.Write))
				wb.Write (stream);
		}

		void PopulateXls (ISheet sheet, Dictionary<string, int> headerToIndex, Hotel hotel)
		{
			var r = sheet.CreateRow (sheet.LastRowNum + 1);

			foreach (var kvp in meta) {
				var cell = r.CreateCell (headerToIndex [kvp.Key]);
				cell.SetCellValue (kvp.Value (hotel));
			}
		}
	}
}
