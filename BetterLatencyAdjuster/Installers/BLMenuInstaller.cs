using BetterLatencyAdjuster.Settings.UI;
using IPA.Logging;
using SiraUtil;
using Zenject;

namespace BetterLatencyAdjuster.Installers
{
	internal class BLMenuInstaller : Installer<Logger, BLMenuInstaller>
	{
		private readonly Logger _logger;

		public BLMenuInstaller(Logger logger)
		{
			_logger = logger;
		}

		public override void InstallBindings()
		{
			Container.BindLoggerAsSiraLogger(_logger);

			Container.BindInterfacesAndSelfTo<SettingsController>().AsSingle();
			Container.BindInterfacesTo<SettingsControllerManager>().AsSingle();
		}
	}
}