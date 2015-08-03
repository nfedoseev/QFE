
using System.Collections.Generic;
using System.Configuration;

namespace qfe
{
	public static class Configurator
	{
		public static string[] IcaoCodesToMonitor
		{
			get
			{
				return GetStringArray("IcaoCodesToMonitor");
			}
		}

		public static int UpdateIntervalInMinutes
		{
			get
			{
				return GetInt("UpdateIntervalInMinutes", 3);
			}
		}

		public static string ServiceUrl
		{
			get
			{
				return GetString("ServiceUrl");
			}
		}

		private static string GetString(string parameterName)
		{
			return ConfigurationManager.AppSettings.Get(parameterName);
		}

		private static int GetInt(string parameterName, int defaultValue = 0)
		{
			int outVal;

			var retVal = int.TryParse(ConfigurationManager.AppSettings.Get(parameterName), out outVal) ? outVal : defaultValue;

			return retVal;
		}

		private static string[] GetStringArray(string parameterName)
		{
			var configString = GetString(parameterName);
			var configuredValues = !string.IsNullOrEmpty(configString) ? configString.Split(',') : new string[] { };

			var retVal = new List<string>(configuredValues.Length);

			foreach (var someValue in configuredValues)
			{
				var trimmedString = someValue.Trim();
				if (string.IsNullOrEmpty(trimmedString))
				{
					continue;
				}

				retVal.Add(trimmedString);
			}

			return retVal.ToArray();
		}
	}
}
