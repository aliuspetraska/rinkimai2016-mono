using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MySql.Data.MySqlClient;

namespace BackgroundWorker
{
	class MainClass
	{
		public static MySqlConnection connection = null;

		public static string server = "eu-cdbr-west-01.cleardb.com";
		public static string database = "heroku_78e58657a5a828d";
		public static string uid = "b1e77c2075766a";
		public static string password = "d1ab6ad9";

		public static string connectionString = "SERVER=" + server + ";" + "DATABASE=" + 
			database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";CharSet=utf8;";

		public static RpgObject _RpgObject = new RpgObject();
		public static AktyvumasVrtObject _AktyvumasVrtObject = new AktyvumasVrtObject();
		public static RezultataiDaugmVrtObject _RezultataiDaugmVrtObject = new RezultataiDaugmVrtObject();
		public static RezultataiVienmVrtObject _RezultataiVienmVrtObject = new RezultataiVienmVrtObject();

		public static void Main(string[] args)
		{
			while (true)
			{
				try
				{
					// http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/rpg.json
					_RpgObject = Data.GetRpgObject();

					// http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/aktyvumas/aktyvumasVrt.json
					_AktyvumasVrtObject = Data.GetAktyvumasVrtObject();

					// http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/1304/rezultatai/rezultataiDaugmVrt.json
					_RezultataiDaugmVrtObject = Data.GetRezultataiDaugmVrtObject();

					// http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/1304/rezultatai/rezultataiVienmVrt.json
					_RezultataiVienmVrtObject = Data.GetRezultataiVienmVrtObject();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}

				try
				{
					connection = new MySqlConnection(connectionString);
					connection.Open();

					Console.WriteLine("MySQL version : " + connection.ServerVersion);

					// Balsavimo aktyvumas Lietuvoje - bendri skaičiai pagal valandas.
					InsertAktyvumas();

					Console.WriteLine("InsertAktyvumas();");

					// Balsavusių % per apygardą
					InsertApygardos();

					Console.WriteLine("InsertApygardos();");

					// Daugiamandačių rezultatai pagal partijas
					InsertDaugiamandatesPartijos();

					Console.WriteLine("InsertDaugiamandatesPartijos();");

					// Apylinkes pateike skaicius
					InsertApylinkiuSuskaiciuotiRezultatai();

					Console.WriteLine("InsertApylinkiuSuskaiciuotiRezultatai();");

					// Visos Daugiamandates Apygardos
					InsertVisosDaugiamandatesApygardos();

					Console.WriteLine("InsertVisosDaugiamandatesApygardos();");

					// Visi išrinkti kandidatai vienmandatese
					InsertIsrinktiVienmandatese();

					Console.WriteLine("InsertIsrinktiVienmandatese();");

					// Visi antro turo kandidatai vienmandatese
					InsertAntroTuroKandidataiVienmandatese();

					Console.WriteLine("InsertAntroTuroKandidataiVienmandatese();");

					Console.WriteLine("Done.");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
				finally
				{
					if (connection != null)
					{
						connection.Close();
					}
				}

				// Sleep for 1 minute
				Thread.Sleep(1 * 60 * 1000);
			}
		}

		public static void InsertAntroTuroKandidataiVienmandatese()
		{
			MySqlCommand truncate = connection.CreateCommand();

			truncate.CommandText = "TRUNCATE TABLE dalyvaus_pakartotiniame;";

			truncate.ExecuteNonQuery();

			foreach (var item in _RezultataiVienmVrtObject.data.dalyvausPakartotiniame)
			{
				MySqlCommand command = connection.CreateCommand();

				command.CommandText = "INSERT INTO dalyvaus_pakartotiniame (rknd_id, timestamp, kandidatas, apygarda, rpg_id, iskele, proc_nuo_gal_biul) " +
					"VALUES (?rknd_id, ?timestamp, ?kandidatas, ?apygarda, ?rpg_id, ?iskele, ?proc_nuo_gal_biul) ON DUPLICATE " +
					"KEY UPDATE timestamp=?timestamp, kandidatas=?kandidatas, apygarda=?apygarda, rpg_id=?rpg_id, iskele=?iskele, proc_nuo_gal_biul=?proc_nuo_gal_biul;";

				command.Parameters.AddWithValue("?rknd_id", item.rknd_id);
				command.Parameters.AddWithValue("?timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				command.Parameters.AddWithValue("?kandidatas", item.kandidatas);
				command.Parameters.AddWithValue("?apygarda", GetApygardaPavadinimas(item.rpg_id));
				command.Parameters.AddWithValue("?rpg_id", item.rpg_id);
				command.Parameters.AddWithValue("?iskele", item.iskele);
				command.Parameters.AddWithValue("?proc_nuo_gal_biul", Convert.ToDouble(item.proc_nuo_gal_biul));

				command.ExecuteNonQuery();
			}
		}

		public static void InsertIsrinktiVienmandatese()
		{
			foreach (var item in _RezultataiVienmVrtObject.data.isrinkti)
			{
				MySqlCommand command = connection.CreateCommand();

				command.CommandText = "INSERT INTO isrinkti_vienmandatese (rknd_id, timestamp, kandidatas, apygarda, iskele) " +
					"VALUES (?rknd_id, ?timestamp, ?kandidatas, ?apygarda, ?iskele) ON DUPLICATE " +
					"KEY UPDATE timestamp=?timestamp, kandidatas=?kandidatas, apygarda=?apygarda, iskele=?iskele;";

				command.Parameters.AddWithValue("?rknd_id", item.rknd_id);
				command.Parameters.AddWithValue("?timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				command.Parameters.AddWithValue("?kandidatas", item.kandidatas);
				command.Parameters.AddWithValue("?apygarda", item.apygarda);
				command.Parameters.AddWithValue("?iskele", item.iskele);

				command.ExecuteNonQuery();
			}
		}

		public static void InsertVisosDaugiamandatesApygardos()
		{
			var _PartijosBalseliai = new List<PartijosBalseliai>();

			var results = GetAllResultsFromVisosApygardos();

			MySqlCommand truncate = connection.CreateCommand();

			truncate.CommandText = "TRUNCATE TABLE visos_daugiamandates_apygardos;";

			truncate.ExecuteNonQuery();

			foreach (var item in results)
			{
				if (item.data != null && item.data.balsai.Count > 0)
				{
					foreach (var balsas in item.data.balsai)
					{
						if (balsas.rpg_id != null && balsas.saraso_numeris != null)
						{
							MySqlCommand command = connection.CreateCommand();

							command.CommandText = "INSERT INTO visos_daugiamandates_apygardos (timestamp, saraso_numeris, partija, rorg_id, " +
								"is_viso, proc_nuo_dal_rinkeju, proc_nuo_dal_rinkeju_lt, rt_id, rpg_id, apygarda, city) VALUES (?timestamp, ?saraso_numeris, ?partija, ?rorg_id, " +
								"?is_viso, ?proc_nuo_dal_rinkeju, ?proc_nuo_dal_rinkeju_lt, ?rt_id, ?rpg_id, ?apygarda, ?city)";

							command.Parameters.AddWithValue("?timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
							command.Parameters.AddWithValue("?saraso_numeris", balsas.saraso_numeris);
							command.Parameters.AddWithValue("?partija", balsas.partija);
							command.Parameters.AddWithValue("?rorg_id", balsas.rorg_id);
							command.Parameters.AddWithValue("?is_viso", Convert.ToInt32(balsas.is_viso));
							command.Parameters.AddWithValue("?proc_nuo_dal_rinkeju", Convert.ToDouble(balsas.proc_nuo_dal_rinkeju));
							command.Parameters.AddWithValue("?proc_nuo_dal_rinkeju_lt", Convert.ToDouble(balsas.proc_nuo_dal_rinkeju_lt));
							command.Parameters.AddWithValue("?rt_id", balsas.rt_id);
							command.Parameters.AddWithValue("?rpg_id", balsas.rpg_id);
							command.Parameters.AddWithValue("?apygarda", GetApygardaPavadinimas(balsas.rpg_id));
							command.Parameters.AddWithValue("?city", GetMiestoPavadinimas(balsas.rpg_id));

							command.ExecuteNonQuery();

							_PartijosBalseliai.Add(new PartijosBalseliai
							{
								partija = balsas.partija,
								apygarda = GetApygardaPavadinimas(balsas.rpg_id),
								city = GetMiestoPavadinimas(balsas.rpg_id),
								is_viso = Convert.ToInt32(balsas.is_viso),
								proc_nuo_dal_rinkeju = Convert.ToDouble(balsas.proc_nuo_dal_rinkeju),
								proc_nuo_dal_rinkeju_lt = Convert.ToDouble(balsas.proc_nuo_dal_rinkeju_lt),
								rorg_id = balsas.rorg_id,
								rpg_id = balsas.rpg_id,
								rt_id = balsas.rt_id,
								saraso_numeris = balsas.saraso_numeris
							});
						}
					}
				}
			}

			MySqlCommand clear = connection.CreateCommand();

			clear.CommandText = "TRUNCATE TABLE kas_kur_laimi;";

			clear.ExecuteNonQuery();

			var balsai = new List<PartijosBalseliai>();

			var balseliai = _PartijosBalseliai.OrderBy(o => o.apygarda).ToList();

			var previous = string.Empty;

			for (int i = 0; i < balseliai.Count; i++)
			{
				if (balseliai[i].apygarda != previous && balsai.Count > 0)
				{
					var sorted = balsai.OrderByDescending(o => o.proc_nuo_dal_rinkeju).Take(1).ToList();

					MySqlCommand command = connection.CreateCommand();

					command.CommandText = "INSERT INTO kas_kur_laimi (timestamp, saraso_numeris, partija, rpg_id, apygarda, proc_nuo_dal_rinkeju, proc_nuo_dal_rinkeju_lt, is_viso) " +
						"VALUES (?timestamp, ?saraso_numeris, ?partija, ?rpg_id, ?apygarda, ?proc_nuo_dal_rinkeju, ?proc_nuo_dal_rinkeju_lt, is_viso)";

					command.Parameters.AddWithValue("?timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					command.Parameters.AddWithValue("?saraso_numeris", sorted.FirstOrDefault().saraso_numeris);
					command.Parameters.AddWithValue("?partija", sorted.FirstOrDefault().partija);
					command.Parameters.AddWithValue("?rpg_id", sorted.FirstOrDefault().rpg_id);
					command.Parameters.AddWithValue("?apygarda", sorted.FirstOrDefault().apygarda);
					command.Parameters.AddWithValue("?proc_nuo_dal_rinkeju", sorted.FirstOrDefault().proc_nuo_dal_rinkeju);
					command.Parameters.AddWithValue("?proc_nuo_dal_rinkeju_lt", sorted.FirstOrDefault().proc_nuo_dal_rinkeju_lt);
					command.Parameters.AddWithValue("?is_viso", Convert.ToInt32(sorted.FirstOrDefault().is_viso));

					command.ExecuteNonQuery();

					balsai = new List<PartijosBalseliai>();
				}

				balsai.Add(balseliai[i]);

				previous = balseliai[i].apygarda;
			}
		}

		public static void InsertApylinkiuSuskaiciuotiRezultatai()
		{
			foreach (var item in _RezultataiDaugmVrtObject.data.biuleteniai)
			{
				if (item.rpg_id == null)
				{
					MySqlCommand command = connection.CreateCommand();

					command.CommandText = "INSERT INTO apylinkiu_pateike_sk (id, timestamp, apylinkiu_sk, apylinkiu_pateike_sk) " +
						"VALUES (?id, ?timestamp, ?apylinkiu_sk, ?apylinkiu_pateike_sk) ON DUPLICATE KEY UPDATE " +
						"timestamp=?timestamp, apylinkiu_sk=?apylinkiu_sk, apylinkiu_pateike_sk=?apylinkiu_pateike_sk;";

					command.Parameters.AddWithValue("?id", 1);
					command.Parameters.AddWithValue("?timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					command.Parameters.AddWithValue("?apylinkiu_sk", Convert.ToInt32(item.apylinkiu_sk));
					command.Parameters.AddWithValue("?apylinkiu_pateike_sk", Convert.ToInt32(item.apylinkiu_pateike_sk));

					command.ExecuteNonQuery();
				}
			}
		}

		public static void InsertDaugiamandatesPartijos()
		{
			foreach (var item in _RezultataiDaugmVrtObject.data.balsai)
			{
				if (!string.IsNullOrEmpty(item.proc_nuo_dal_rinkeju))
				{
					MySqlCommand command = connection.CreateCommand();

					command.CommandText = "INSERT INTO daugiamandates_partijos (saraso_numeris, timestamp, partija, is_viso, proc_nuo_dal_rinkeju) " +
						"VALUES (?saraso_numeris, ?timestamp, ?partija, ?is_viso, ?proc_nuo_dal_rinkeju) ON DUPLICATE KEY UPDATE " +
						"timestamp=?timestamp, partija=?partija, is_viso=?is_viso, proc_nuo_dal_rinkeju=?proc_nuo_dal_rinkeju;";

					command.Parameters.AddWithValue("?saraso_numeris", item.saraso_numeris);
					command.Parameters.AddWithValue("?timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					command.Parameters.AddWithValue("?partija", item.partija);
					command.Parameters.AddWithValue("?is_viso", Convert.ToInt32(item.is_viso));
					command.Parameters.AddWithValue("?proc_nuo_dal_rinkeju", Convert.ToDouble(item.proc_nuo_dal_rinkeju));

					command.ExecuteNonQuery();
				}
			}
		}

		public static void InsertApygardos()
		{
			// Balsavusių % per apygardą

			foreach (var item in _AktyvumasVrtObject.data)
			{
				if (item.rpg_id != null)
				{
					MySqlCommand command = connection.CreateCommand();

					command.CommandText = "INSERT INTO apygardos (id, timestamp, apygarda, aktyvumas) VALUES (?id, ?timestamp, " +
						"?apygarda, ?aktyvumas) ON DUPLICATE KEY UPDATE timestamp=?timestamp, apygarda=?apygarda, aktyvumas=?aktyvumas;";

					command.Parameters.AddWithValue("?id", Convert.ToInt32(item.rpg_id));
					command.Parameters.AddWithValue("?timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					command.Parameters.AddWithValue("?apygarda", GetApygardaPavadinimas(item.rpg_id));
					command.Parameters.AddWithValue("?aktyvumas", Convert.ToDouble(item.val_viso));

					command.ExecuteNonQuery();
				}
			}
		}

		public static void InsertAktyvumas()
		{
			// Balsavimo aktyvumas Lietuvoje - bendri skaičiai pagal valandas.
			foreach (var item in _AktyvumasVrtObject.data)
			{
				if (item.rpg_id == null)
				{
					MySqlCommand command = connection.CreateCommand();

					command.CommandText = "INSERT INTO aktyvumas (id, timestamp, rinkeju_skaicius, isanksto_balsavo, is_viso_balsavo, " +
						"val8, val9, val10, val11, val12, val13, val14, val15, val16, val17, val18, val19, val20) VALUES (?id, " +
						"?timestamp, ?rinkeju_skaicius, ?isanksto_balsavo, ?is_viso_balsavo, ?val8, ?val9, ?val10, ?val11, ?val12, " +
						"?val13, ?val14, ?val15, ?val16, ?val17, ?val18, ?val19, ?val20) ON DUPLICATE KEY UPDATE timestamp=?timestamp, " +
						"rinkeju_skaicius=?rinkeju_skaicius, isanksto_balsavo=?isanksto_balsavo, is_viso_balsavo=?is_viso_balsavo, " +
						"val8=?val8, val9=?val9, val10=?val10, val11=?val11, val12=?val12, val13=?val13, val14=?val14, val15=?val15, " +
						"val16=?val16, val17=?val17, val18=?val18, val19=?val19, val20=?val20;";

					command.Parameters.AddWithValue("?id", 1);
					command.Parameters.AddWithValue("?timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
					command.Parameters.AddWithValue("?rinkeju_skaicius", Convert.ToInt32(item.rinkeju_skaicius));
					command.Parameters.AddWithValue("?isanksto_balsavo", Convert.ToDouble(item.isanksto));
					command.Parameters.AddWithValue("?is_viso_balsavo", Convert.ToDouble(item.val_viso));
					command.Parameters.AddWithValue("?val8", Convert.ToDouble(item.val8));
					command.Parameters.AddWithValue("?val9", Convert.ToDouble(item.val9));
					command.Parameters.AddWithValue("?val10", Convert.ToDouble(item.val10));
					command.Parameters.AddWithValue("?val11", Convert.ToDouble(item.val11));
					command.Parameters.AddWithValue("?val12", Convert.ToDouble(item.val12));
					command.Parameters.AddWithValue("?val13", Convert.ToDouble(item.val13));
					command.Parameters.AddWithValue("?val14", Convert.ToDouble(item.val14));
					command.Parameters.AddWithValue("?val15", Convert.ToDouble(item.val15));
					command.Parameters.AddWithValue("?val16", Convert.ToDouble(item.val16));
					command.Parameters.AddWithValue("?val17", Convert.ToDouble(item.val17));
					command.Parameters.AddWithValue("?val18", Convert.ToDouble(item.val18));
					command.Parameters.AddWithValue("?val19", Convert.ToDouble(item.val19));
					command.Parameters.AddWithValue("?val20", Convert.ToDouble(item.val20));

					command.ExecuteNonQuery();
				}
			}
		}

		public static string GetMiestoPavadinimas(string rpg_id)
		{
			foreach (var item in Cities.GetVilniausApygardos())
			{
				if (item == rpg_id)
				{
					return "Vilnius";
				}
			}

			foreach (var item in Cities.GetKaunoApygardos())
			{
				if (item == rpg_id)
				{
					return "Kaunas";
				}
			}

			foreach (var item in Cities.GetKlaipedosApygardos())
			{
				if (item == rpg_id)
				{
					return "Klaipeda";
				}
			}

			foreach (var item in Cities.GetSiauliuApygardos())
			{
				if (item == rpg_id)
				{
					return "Siauliai";
				}
			}

			foreach (var item in Cities.GetPanevezioApygardos())
			{
				if (item == rpg_id)
				{
					return "Panevezys";
				}
			}

			return "Lietuva";
		}

		public static List<RezultataiDaugmRpgObject> GetAllResultsFromVisosApygardos()
		{
			var temp = new List<RezultataiDaugmRpgObject>();

			foreach (var item in _RpgObject.data)
			{
				if (item.uzsienio == "N")
				{
					temp.Add(Data.GetRezultataiDaugmRpgObject(item.id));
				}
			}

			temp.Add(Data.GetRezultataiDaugmRpgUzsienisObject());

			return temp;
		}

		public static string GetApygardaPavadinimas(string rpg_id)
		{
			foreach (var item in _RpgObject.data)
			{
				if (item.id == rpg_id)
				{
					return item.pav;
				}
			}

			return string.Empty;
		}
	}

	public class PartijosBalseliai
	{
		public string saraso_numeris { get; set; }
		public string partija { get; set; }
		public string rorg_id { get; set; }
		public int is_viso { get; set; }
		public double proc_nuo_dal_rinkeju { get; set; }
		public double proc_nuo_dal_rinkeju_lt { get; set; }
		public string rt_id { get; set; }
		public string rpg_id { get; set; }
		public string apygarda { get; set; }
		public string city { get; set; }
	}
}
