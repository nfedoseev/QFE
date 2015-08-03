
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace qfe
{
	public partial class Main : Form
	{
		//	flag to avoid 'self-DoS'
		//
		private bool weatherRetrivialIsInProgress;

		private readonly BackgroundWorker weatherServiceBackgroundWorker;

		public Main()
		{
			#region  VS internals

			InitializeComponent();

			#endregion


			weatherRetrivialIsInProgress = false;
			weatherServiceBackgroundWorker = new BackgroundWorker();

			SetupAndRunAsyncWorker();
		}

		private void SetupAndRunAsyncWorker()
		{
			weatherServiceBackgroundWorker.DoWork += WeatherServiceBackgroundWorker_DoWork;

			weatherServiceBackgroundWorker.RunWorkerCompleted += WeatherServiceBackgroundWorker_RunWorkerCompleted;

			SetupUpdateWeatherTimer(Configurator.IcaoCodesToMonitor);

			//	run worker
			//

			UpdateTable(Configurator.IcaoCodesToMonitor);
		}

		private void SetupUpdateWeatherTimer(ICollection<string> portIcaoCodesToDisplay)
		{
			if (portIcaoCodesToDisplay.Count <= 0)
			{
				return;
			}

			var weatherUpdateTimer = new Timer
			{
				Interval = Configurator.UpdateIntervalInSeconds * 1000 * 60
			};

			weatherUpdateTimer.Tick += (
				(sender, e) => Timer_Tick(portIcaoCodesToDisplay)
				);

			weatherUpdateTimer.Start();
		}

		private void Timer_Tick(IEnumerable<string> portCodes)
		{
			UpdateTable(portCodes);
		}

		private void UpdateTable(IEnumerable portIcaoCodesToDisplay)
		{
			if (weatherRetrivialIsInProgress)
			{
				//	current weather retrivial request is in progress,
				//	we should avoid 'self-DoS attack'
				//
				return;
			}

			Text = @"Updating...";

			weatherServiceBackgroundWorker.RunWorkerAsync(portIcaoCodesToDisplay);
		}

		private void WeatherServiceBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var portIcaoCodesArray = e.Argument as string[];
			if (null == portIcaoCodesArray)
			{
				return;
			}

			weatherRetrivialIsInProgress = true;

			var retVal = new WeatherInfoByPortIcaoCode(portIcaoCodesArray.Length);

			foreach (var portIcaoCode in portIcaoCodesArray)
			{
				WeatherServiceResponse weatherInfo;

				try
				{
					weatherInfo = WeatherService.GetWx(portIcaoCode);
				}
				catch
				{
					weatherInfo = null;
				}

				if (null == weatherInfo)
				{
					continue;
				}

				retVal.Add(portIcaoCode, weatherInfo);
			}

			e.Result = retVal;
		}

		private void WeatherServiceBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			weatherRetrivialIsInProgress = false;

			if (e.Cancelled)
			{
				return;
			}

			var weatherInfos = e.Result as WeatherInfoByPortIcaoCode;
			if (null == weatherInfos)
			{
				return;
			}

			dataGridView1.Rows.Clear();

			string windowTitle = null;

			foreach (var someWeatherInfo in weatherInfos)
			{
				dataGridView1.Rows.Add(
					someWeatherInfo.Value.PortIcaoCode,
					someWeatherInfo.Value.WindDirection,
					someWeatherInfo.Value.WindSpeed,
					someWeatherInfo.Value.TransitionLevel,
					someWeatherInfo.Value.QNH,
					someWeatherInfo.Value.QFE
					);

				if (string.IsNullOrEmpty(windowTitle))
				{
					windowTitle = string.Format("QFE ({0})", someWeatherInfo.Value.TakenAt);
				}
			}

			Text = !string.IsNullOrEmpty(windowTitle) ? windowTitle : @"No any info";
		}
	}
}
