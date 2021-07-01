using System;
using Zenject;

namespace BetterLatencyAdjuster.Settings.UI
{
	internal class SettingsControllerManager : IInitializable, IDisposable
	{
		private SettingsController? _settingsHost;

		[Inject]
		public SettingsControllerManager(SettingsController settingsHost)
		{
			_settingsHost = settingsHost;
		}

		public void Initialize()
		{
		}

		public void Dispose()
		{
			if (_settingsHost == null)
			{
				return;
			}

			_settingsHost = null!;
		}
	}
}