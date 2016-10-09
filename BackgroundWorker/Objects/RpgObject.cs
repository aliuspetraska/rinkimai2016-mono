using System;
using System.Collections.Generic;

namespace BackgroundWorker
{
	public class HeaderRpg
	{
		public string resource { get; set; }
		public string date { get; set; }
		public string hash { get; set; }
	}

	public class DatumRpg
	{
		public string id { get; set; }
		public string vr_id { get; set; }
		public string rt_nr { get; set; }
		public string nr { get; set; }
		public string pav { get; set; }
		public string adr { get; set; }
		public string tel { get; set; }
		public string ep { get; set; }
		public string uzsienio { get; set; }
	}

	public class RpgObject
	{
		public HeaderRpg header { get; set; }
		public List<DatumRpg> data { get; set; }
	}
}
