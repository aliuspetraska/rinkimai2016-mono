using System;
using System.Collections.Generic;

namespace BackgroundWorker
{
	public static class Cities
	{
		public static List<string> GetVilniausApygardos()
		{
			var list = new List<string>();

			foreach (var item in MainClass._RpgObject.data)
			{
				if (item.pav.Contains("Naujamiesčio")) { list.Add(item.id); }
				if (item.pav.Contains("Senamiesčio")) { list.Add(item.id); }
				if (item.pav.Contains("Antakalnio")) { list.Add(item.id); }
				if (item.pav.Contains("Žirmūnų")) { list.Add(item.id); }
				if (item.pav.Contains("Fabijoniškių")) { list.Add(item.id); }
				if (item.pav.Contains("Šeškinės")) { list.Add(item.id); }
				if (item.pav.Contains("Justiniškių")) { list.Add(item.id); }
				if (item.pav.Contains("Karoliniškių")) { list.Add(item.id); }
				if (item.pav.Contains("Lazdynų")) { list.Add(item.id); }
				if (item.pav.Contains("Naujosios Vilnios")) { list.Add(item.id); }
				if (item.pav.Contains("Panerių")) { list.Add(item.id); }
				if (item.pav.Contains("Verkių")) { list.Add(item.id); }
			}

			return list;
		}

		public static List<string> GetKaunoApygardos()
		{
			var list = new List<string>();

			foreach (var item in MainClass._RpgObject.data)
			{
				if (item.pav.Contains("Centro-Žaliakalnio")) { list.Add(item.id); }
				if (item.pav.Contains("Šilainių")) { list.Add(item.id); }
				if (item.pav.Contains("Kalniečių")) { list.Add(item.id); }
				if (item.pav.Contains("Dainavos")) { list.Add(item.id); }
				if (item.pav.Contains("Petrašiūnų")) { list.Add(item.id); }
				if (item.pav.Contains("Panemunės")) { list.Add(item.id); }
				if (item.pav.Contains("Aleksoto-Vilijampolės")) { list.Add(item.id); }
			}

			return list;
		}

		public static List<string> GetKlaipedosApygardos()
		{
			var list = new List<string>();

			foreach (var item in MainClass._RpgObject.data)
			{
				if (item.pav.Contains("Baltijos")) { list.Add(item.id); }
				if (item.pav.Contains("Marių")) { list.Add(item.id); }
				if (item.pav.Contains("Pajūrio")) { list.Add(item.id); }
				if (item.pav.Contains("Danės")) { list.Add(item.id); }
			}

			return list;
		}

		public static List<string> GetSiauliuApygardos()
		{
			var list = new List<string>();

			foreach (var item in MainClass._RpgObject.data)
			{
				if (item.pav.Contains("Saulės")) { list.Add(item.id); }
				if (item.pav.Contains("Aušros")) { list.Add(item.id); }
			}

			return list;
		}

		public static List<string> GetPanevezioApygardos()
		{
			var list = new List<string>();

			foreach (var item in MainClass._RpgObject.data)
			{
				if (item.pav.Contains("Nevėžio")) { list.Add(item.id); }
				if (item.pav.Contains("Vakarinė")) { list.Add(item.id); }
			}

			return list;
		}
	}

	/*

	Vilniaus apygardos:
	
	1. Naujamiesčio	
	2. Senamiesčio	
	3. Antakalnio	
	4. Žirmūnų	
	5. Fabijoniškių	
	6. Šeškinės	
	7. Justiniškių	
	8. Karoliniškių	
	9. Lazdynų	
	10. Naujosios Vilnios	
	11. Panerių	
	12. Verkių

	Kauno apygardos:
	
	13. Centro-Žaliakalnio	
	14. Šilainių	
	15. Kalniečių	
	16. Dainavos	
	17. Petrašiūnų	
	18. Panemunės	
	19. Aleksoto-Vilijampolės

	Klaipėdos apygardos:
	
	20. Baltijos	
	21. Marių	
	22. Pajūrio	
	23. Danės	

	Šiaulių apygardos:
	
	24. Saulės
	25. Aušros

	Panevėžio apylinkės:
	
	26. Nevėžio
	27. Vakarinė

	*/
}
