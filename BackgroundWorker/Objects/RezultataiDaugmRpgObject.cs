using System;
using System.Collections.Generic;

namespace BackgroundWorker
{
	public class RezultataiDaugmRpgHeader
	{
		public string resource { get; set; }
		public string date { get; set; }
		public string hash { get; set; }
	}

	public class RezultataiDaugmRpgBalsai
	{
		public string saraso_numeris { get; set; }
		public string partija { get; set; }
		public string rorg_id { get; set; }
		public string balsadezese { get; set; }
		public string pastu { get; set; }
		public string is_viso { get; set; }
		public string proc_nuo_dal_rinkeju { get; set; }
		public string proc_nuo_dal_rinkeju_lt { get; set; }
		public string rt_id { get; set; }
		public string rpg_id { get; set; }
		public string db_viso_rusiavimui { get; set; }
	}

	public class RezultataiDaugmRpgBiuleteniai
	{
		public string viso_rinkeju { get; set; }
		public string viso_dalyvavo { get; set; }
		public string viso_dalyvavo_proc { get; set; }
		public string negaliojanciu { get; set; }
		public string negaliojanciu_proc { get; set; }
		public string galiojanciu { get; set; }
		public string galiojanciu_proc { get; set; }
		public string rt_id { get; set; }
		public string rpg_id { get; set; }
		public string rpl_id { get; set; }
	}

	public class RezultataiDaugmRpgData
	{
		public List<RezultataiDaugmRpgBalsai> balsai { get; set; }
		public List<RezultataiDaugmRpgBiuleteniai> biuleteniai { get; set; }
	}

	public class RezultataiDaugmRpgObject
	{
		public RezultataiDaugmRpgHeader header { get; set; }
		public RezultataiDaugmRpgData data { get; set; }
	}
}
