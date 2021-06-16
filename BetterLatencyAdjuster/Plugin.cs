using BetterLatencyAdjuster.Installers;
using IPA;
using IPA.Logging;
using SiraUtil.Zenject;

namespace BetterLatencyAdjuster
{
	[Plugin(RuntimeOptions.DynamicInit)]
	public class Plugin
	{
		[Init]
		public void Init(Logger logger, Zenjector zenjector)
		{
			zenjector.OnMenu<BLMenuInstaller>().WithParameters(logger);
		}

		[OnEnable, OnDisable]
		public void OnStateChanged()
		{
			// Zenject is poggies
		}
	}
}