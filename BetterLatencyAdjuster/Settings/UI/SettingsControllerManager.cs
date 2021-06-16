using System;
using BeatSaberMarkupLanguage.Settings;
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
			BSMLSettings.instance.AddSettingsMenu("<size=85%>BetterLatencyAdjuster", "BetterLatencyAdjuster.Settings.UI.Settings.bsml", _settingsHost);
		}

		public void Dispose()
		{
			if (_settingsHost == null)
			{
				return;
			}

			BSMLSettings.instance.RemoveSettingsMenu(_settingsHost);
			_settingsHost = null!;
		}
	}
}