using System;
using System.Collections.Generic;

namespace BackgroundWorker
{
	public class HeaderAktyvumasVrt
	{
		public string resource { get; set; }
		public string date { get; set; }
		public string hash { get; set; }
	}

	public class DatumAktyvumasVrt
	{
		public string rinkeju_skaicius { get; set; }
		public string isanksto { get; set; }
		public string val8 { get; set; }
		public string val9 { get; set; }
		public string val10 { get; set; }
		public string val11 { get; set; }
		public string val12 { get; set; }
		public string val13 { get; set; }
		public string val14 { get; set; }
		public string val15 { get; set; }
		public string val16 { get; set; }
		public string val17 { get; set; }
		public string val18 { get; set; }
		public string val19 { get; set; }
		public string val20 { get; set; }
		public string val_viso { get; set; }
		public string rpg_id { get; set; }
		public string vr_id { get; set; }
		public string rt_nr { get; set; }
	}

	public class AktyvumasVrtObject
	{
		public HeaderAktyvumasVrt header { get; set; }
		public List<DatumAktyvumasVrt> data { get; set; }
	}
}
