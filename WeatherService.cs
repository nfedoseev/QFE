
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace qfe
{
	internal static class WeatherService
	{
		public static WeatherServiceResponse GetWx(string icaoCode)
		{
			return JsonConvert.DeserializeObject<WeatherServiceResponse>(
				GetWeatherByPortIcaoCode(icaoCode)
				);
		}

		private static string GetWeatherByPortIcaoCode(string icaoCode)
		{
			string retVal;

			var serviceUrl = Path.Combine(Configurator.ServiceUrl, icaoCode);

			using (var client = new WebClient())
			{
				retVal = client.DownloadString(serviceUrl);
			}

			return retVal;
		}
	}
}
