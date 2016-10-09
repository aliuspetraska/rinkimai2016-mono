using System;
using System.Collections.Generic;

namespace BackgroundWorker
{
	public class HeaderRezultataiDaugmVrt
	{
		public string resource { get; set; }
		public string date { get; set; }
		public string hash { get; set; }
	}

	public class BalsaiRezultataiDaugmVrt
	{
		public string saraso_numeris { get; set; }
		public string partija { get; set; }
		public string rorg_id { get; set; }
		public string balsadezese { get; set; }
		public string pastu { get; set; }
		public string is_viso { get; set; }
		public string proc_nuo_dal_rinkeju { get; set; }
	}

	public class BiuleteniaiRezultataiDaugmVrt
	{
		public string apylinkiu_sk { get; set; }
		public string apylinkiu_pateike_sk { get; set; }
		public string viso_rinkeju { get; set; }
		public string viso_dalyvavo { get; set; }
		public string viso_dalyvavo_proc { get; set; }
		public string negaliojanciu { get; set; }
		public string negaliojanciu_proc { get; set; }
		public string galiojanciu { get; set; }
		public string galiojanciu_proc { get; set; }
		public string rt_id { get; set; }
		public string rpg_id { get; set; }
		public string apygarda { get; set; }
	}

	public class DataRezultataiDaugmVrt
	{
		public List<BalsaiRezultataiDaugmVrt> balsai { get; set; }
		public List<BiuleteniaiRezultataiDaugmVrt> biuleteniai { get; set; }
	}

	public class RezultataiDaugmVrtObject
	{
		public HeaderRezultataiDaugmVrt header { get; set; }
		public DataRezultataiDaugmVrt data { get; set; }
	}
}
