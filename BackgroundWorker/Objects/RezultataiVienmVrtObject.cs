using System;
using System.Collections.Generic;

namespace BackgroundWorker
{
	public class RezultataiVienmVrtHeader
	{
		public string resource { get; set; }
		public string date { get; set; }
		public string hash { get; set; }
	}

	public class RezultataiVienmVrtBendraInfo
	{
		public string apygardu_viso { get; set; }
		public string apylinkiu_viso { get; set; }
		public string apygardu_sk_pateike { get; set; }
		public string apylinkiu_sk_pateike { get; set; }
		public string viso_rinkeju { get; set; }
		public string viso_dalyvavo { get; set; }
		public string viso_dalyvavo_proc { get; set; }
		public string negaliojanciu { get; set; }
		public string negaliojanciu_proc { get; set; }
		public string galiojanciu { get; set; }
		public string galiojanciu_proc { get; set; }
	}

	public class RezultataiVienmVrtIsrinkti
	{
		public string kandidatas { get; set; }
		public string apygarda { get; set; }
		public string iskele { get; set; }
		public string rknd_id { get; set; }
		public string rpg_id { get; set; }
	}

	public class RezultataiVienmVrtDalyvausPakartotiniame
	{
		public string kandidatas { get; set; }
		public string apygarda { get; set; }
		public string iskele { get; set; }
		public string is_viso { get; set; }
		public string proc_nuo_gal_biul { get; set; }
		public string rknd_id { get; set; }
		public string rpg_id { get; set; }
	}

	public class RezultataiVienmVrtBiuleteniai
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

	public class RezultataiVienmVrtData
	{
		public RezultataiVienmVrtBendraInfo bendraInfo { get; set; }
		public List<RezultataiVienmVrtIsrinkti> isrinkti { get; set; }
		public List<RezultataiVienmVrtDalyvausPakartotiniame> dalyvausPakartotiniame { get; set; }
		public List<RezultataiVienmVrtBiuleteniai> biuleteniai { get; set; }
	}

	public class RezultataiVienmVrtObject
	{
		public RezultataiVienmVrtHeader header { get; set; }
		public RezultataiVienmVrtData data { get; set; }
	}
}
