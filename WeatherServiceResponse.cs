
using Newtonsoft.Json;

namespace qfe
{
	//	ReSharper disable once ClassNeverInstantiated.Global
	//	required for serialization
	public class WeatherServiceResponse
	{
		//	ReSharper disable UnusedAutoPropertyAccessor.Global
		//	fileds are needed for serialization

		[JsonProperty(PropertyName = "icao")]
		public string PortIcaoCode { get; set; }

		[JsonProperty(PropertyName = "time")]
		public string TakenAt { get; set; }

		[JsonProperty(PropertyName = "wind")]
		public int WindDirection { get; set; }

		[JsonProperty(PropertyName = "value")]
		public string WindSpeed { get; set; }

		[JsonProperty(PropertyName = "tl")]
		public string TransitionLevel { get; set; }

		[JsonProperty(PropertyName = "qnh")]
		public int QNH { get; set; }

		[JsonProperty(PropertyName = "qfe")]
		public int QFE { get; set; }
	}
	// ReSharper restore UnusedAutoPropertyAccessor.Global
}
