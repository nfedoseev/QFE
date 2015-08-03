
using System;
using System.Collections;
using System.Collections.Generic;

namespace qfe
{
	public class WeatherInfoByPortIcaoCode : IEnumerable<KeyValuePair<string, WeatherServiceResponse>>
	{
		private readonly Dictionary<string, WeatherServiceResponse> portInfoDictionary;

		public WeatherInfoByPortIcaoCode(int capacity)
		{
			portInfoDictionary = new Dictionary<string, WeatherServiceResponse>(capacity);
		}

		public void Add(
			string portIcaoCode,
			WeatherServiceResponse weatherInfo,
			bool throwExceptionOnKeyDuplicate = false
			)
		{
			var keyExists = portInfoDictionary.ContainsKey(portIcaoCode);

			if (
				throwExceptionOnKeyDuplicate
				&& keyExists
				)
			{
				throw new ArgumentException(@"Key already exists.", "portIcaoCode");
			}

			if (!keyExists)
			{
				portInfoDictionary.Add(portIcaoCode, weatherInfo);
			}
			else
			{
				portInfoDictionary[portIcaoCode] = weatherInfo;
			}
		}


		#region IEnumerable implementation

		public IEnumerator<KeyValuePair<string, WeatherServiceResponse>> GetEnumerator()
		{
			return portInfoDictionary.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

	}
}
