using System;
using Newtonsoft.Json;
using RestSharp;

namespace BackgroundWorker
{
	public static class Data
	{
		public static RpgObject GetRpgObject()
		{
			// http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/rpg.json

			var client = new RestClient("http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/");
			var request = new RestRequest("rpg.json", Method.GET);
			var response = client.Execute(request);

			return JsonConvert.DeserializeObject<RpgObject>(response.Content);
		}

		public static AktyvumasVrtObject GetAktyvumasVrtObject()
		{
			// http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/aktyvumas/aktyvumasVrt.json

			var client = new RestClient("http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/");
			var request = new RestRequest("aktyvumas/aktyvumasVrt.json", Method.GET);
			var response = client.Execute(request);

			return JsonConvert.DeserializeObject<AktyvumasVrtObject>(response.Content);
		}

		public static RezultataiDaugmVrtObject GetRezultataiDaugmVrtObject()
		{
			// http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/1304/rezultatai/rezultataiDaugmVrt.json

			var client = new RestClient("http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/");
			var request = new RestRequest("1304/rezultatai/rezultataiDaugmVrt.json", Method.GET);
			var response = client.Execute(request);

			return JsonConvert.DeserializeObject<RezultataiDaugmVrtObject>(response.Content);
		}

		public static RezultataiDaugmRpgObject GetRezultataiDaugmRpgObject(string rpg_id)
		{
			// http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/1304/rezultatai/rezultataiDaugmRpg18734.json

			var client = new RestClient("http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/");
			var request = new RestRequest("1304/rezultatai/rezultataiDaugmRpg{id}.json", Method.GET);
			request.AddUrlSegment("id", rpg_id);
			var response = client.Execute(request);

			return JsonConvert.DeserializeObject<RezultataiDaugmRpgObject>(response.Content);
		}

		public static RezultataiVienmVrtObject GetRezultataiVienmVrtObject()
		{
			// http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/1304/rezultatai/rezultataiVienmVrt.json

			var client = new RestClient("http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/");
			var request = new RestRequest("1304/rezultatai/rezultataiVienmVrt.json", Method.GET);
			var response = client.Execute(request);

			return JsonConvert.DeserializeObject<RezultataiVienmVrtObject>(response.Content);
		}

		public static RezultataiDaugmRpgObject GetRezultataiDaugmRpgUzsienisObject()
		{
			// http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/1304/rezultatai/rezultataiDaugmRpgUzsienis.json

			var client = new RestClient("http://www.vrk.lt/statiniai/puslapiai/rinkimai/102/1/");
			var request = new RestRequest("1304/rezultatai/rezultataiDaugmRpgUzsienis.json", Method.GET);
			var response = client.Execute(request);

			return JsonConvert.DeserializeObject<RezultataiDaugmRpgObject>(response.Content);
		}
	}
}
