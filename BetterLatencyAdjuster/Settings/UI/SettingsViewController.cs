using System;
using BeatSaberMarkupLanguage.Attributes;
using IPA.Utilities;
using SiraUtil.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace BetterLatencyAdjuster.Settings.UI
{
	internal class SettingsController : ITickable
	{
		private static readonly FieldAccessor<MenuTransitionsHelper, MainSettingsModelSO>.Accessor MainSettingsModelAccessor =
			FieldAccessor<MenuTransitionsHelper, MainSettingsModelSO>.GetAccessor("_mainSettingsModel");

		private static readonly FieldAccessor<MainSettingsMenuViewController, SettingsSubMenuInfo[]>.Accessor SettingsSubMenuInfoAccessor =
			FieldAccessor<MainSettingsMenuViewController, SettingsSubMenuInfo[]>.GetAccessor("_settingsSubMenuInfos");

		private static readonly FieldAccessor<AudioLatencyViewController, VisualMetronome>.Accessor VisualMetronomeAccessor =
			FieldAccessor<AudioLatencyViewController, VisualMetronome>.GetAccessor("_visualMetronome");

		private static readonly FieldAccessor<VisualMetronome, AudioSource>.Accessor AudioSourceAccessor =
			FieldAccessor<VisualMetronome, AudioSource>.GetAccessor("_audioSource");

		private readonly byte[] _whiteImage = Convert.FromBase64String(
			"iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAABhWlDQ1BJQ0MgcHJvZmlsZQAAKJF9kT1Iw0AYht+mSkUqHewgIpihOlkQLeIoVSyChdJWaNXB5NI/aNKQpLg4Cq4FB38Wqw4uzro6uAqC4A+Ii6uToouU+F1SaBHjwd09vPe9L3ffAUKzylSzZxJQNctIJ+JiLr8qBl4RxChCtMYkZurJzGIWnuPrHj6+30V5lnfdn2NAKZgM8InEc0w3LOIN4plNS+e8TxxmZUkhPieeMOiCxI9cl11+41xyWOCZYSObnicOE4ulLpa7mJUNlThGHFFUjfKFnMsK5y3OarXO2vfkLwwWtJUM12mOIIElJJGCCBl1VFCFhSjtGikm0nQe9/APO/4UuWRyVcDIsYAaVEiOH/wPfvfWLE5PuUnBOND7YtsfY0BgF2g1bPv72LZbJ4D/GbjSOv5aE5j9JL3R0SJHQGgbuLjuaPIecLkDDD3pkiE5kp+mUCwC72f0TXlg8BboX3P71j7H6QOQpV4t3wAHh8B4ibLXPd7d1923f2va/fsBuKlyw7LqvioAAAAJcEhZcwAALiMAAC4jAXilP3YAAAAHdElNRQfjDB8PFjs3QXKKAAAAGXRFWHRDb21tZW50AENyZWF0ZWQgd2l0aCBHSU1QV4EOFwAAAAxJREFUCNdj+P//PwAF/gL+3MxZ5wAAAABJRU5ErkJggg==");

		private readonly byte[] _blackImage = Convert.FromBase64String(
			"iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAABhWlDQ1BJQ0MgcHJvZmlsZQAAKJF9kT1Iw0AYht+mSkUqHewgIpihOlkQLeIoVSyChdJWaNXB5NI/aNKQpLg4Cq4FB38Wqw4uzro6uAqC4A+Ii6uToouU+F1SaBHjwd09vPe9L3ffAUKzylSzZxJQNctIJ+JiLr8qBl4RxChCtMYkZurJzGIWnuPrHj6+30V5lnfdn2NAKZgM8InEc0w3LOIN4plNS+e8TxxmZUkhPieeMOiCxI9cl11+41xyWOCZYSObnicOE4ulLpa7mJUNlThGHFFUjfKFnMsK5y3OarXO2vfkLwwWtJUM12mOIIElJJGCCBl1VFCFhSjtGikm0nQe9/APO/4UuWRyVcDIsYAaVEiOH/wPfvfWLE5PuUnBOND7YtsfY0BgF2g1bPv72LZbJ4D/GbjSOv5aE5j9JL3R0SJHQGgbuLjuaPIecLkDDD3pkiE5kp+mUCwC72f0TXlg8BboX3P71j7H6QOQpV4t3wAHh8B4ibLXPd7d1923f2va/fsBuKlyw7LqvioAAAAJcEhZcwAALiMAAC4jAXilP3YAAAAHdElNRQfjDB8PDRLcxSF8AAAAGXRFWHRDb21tZW50AENyZWF0ZWQgd2l0aCBHSU1QV4EOFwAAAAxJREFUCNdjYGBgAAAABAABJzQnCgAAAABJRU5ErkJggg==");

		private const float INTERVAL = 3000;
		private readonly Texture2D _blackTexture = new Texture2D(1, 1);
		private readonly Texture2D _whiteTexture = new Texture2D(1, 1);
		private float _currentInterval;
		private const float SOUND_INTERVAL = 100;
		private AudioSource _audioSource = null!;
		private int _prevUpdate = 1;
		private bool _hasReachedCountdown;

		private SiraLog _siraLog = null!;
		private MainSettingsModelSO _mainSettingsModel = null!;

		[Inject]
		internal void Construct(SiraLog siraLog, MenuTransitionsHelper menuTransitionsHelper, MainSettingsMenuViewController mainSettingsMenuViewController)
		{
			_siraLog = siraLog;
			_mainSettingsModel = MainSettingsModelAccessor(ref menuTransitionsHelper);

			var settingsSubMenuInfos = SettingsSubMenuInfoAccessor(ref mainSettingsMenuViewController);
			foreach (var settingsSubMenuInfo in settingsSubMenuInfos)
			{
				if (settingsSubMenuInfo.viewController is AudioLatencyViewController audioLatencyViewController)
				{
					var visualMetronome = VisualMetronomeAccessor(ref audioLatencyViewController);
					_audioSource = Object.Instantiate(AudioSourceAccessor(ref visualMetronome));
					_audioSource.Stop();
					_audioSource.loop = false;
					_audioSource.playOnAwake = false;
					_audioSource.volume = _mainSettingsModel.volume.value;
					break;
				}
			}

			OverrideAudioLatency = _mainSettingsModel.overrideAudioLatency.value;
			AudioLatency = (int) (_mainSettingsModel.audioLatency.value * 1000f);

			_siraLog.Logger.Notice($"Override: {OverrideAudioLatency}; Latency: {AudioLatency}");
		}

		[UIComponent("flashImage")]
		private RawImage image = null!;

		[UIComponent("countdownText")]
		private TextMeshProUGUI countDownText = null!;

		[UIValue("checkboxValue")]
		internal bool OverrideAudioLatency { get; set; }

		[UIValue("slider-value")]
		internal int AudioLatency { get; set; }

		[UIAction("#post-parse")]
		internal void Setup()
		{
			image.texture = _blackTexture;
			_blackTexture.LoadImage(_blackImage);
			_whiteTexture.LoadImage(_whiteImage);
			_currentInterval = INTERVAL;
			countDownText.text = _currentInterval.ToString("F0");
		}

		[UIAction("#apply")]
		internal void Save()
		{
			_mainSettingsModel.overrideAudioLatency.value = OverrideAudioLatency;
			_mainSettingsModel.audioLatency.value = AudioLatency / 1000f;
			_mainSettingsModel.Save();
		}

		[UIAction("checkboxOnChange")]
		internal void CheckboxOnChange(bool newVal)
		{
			if (!newVal)
			{
				_currentInterval = INTERVAL;
				countDownText.text = _currentInterval.ToString("F0");
				image.texture = _blackTexture;
			}
		}

		private void FlashImage(int difference)
		{
			try
			{
				_currentInterval -= difference; //Interval reduces based on time passed since last update
				countDownText.text = _currentInterval > 0 ? _currentInterval.ToString("F0") : "0";

				if (_currentInterval <= AudioLatency && !_hasReachedCountdown)
				{
					_audioSource.enabled = true;
					_audioSource.time = 0f;
					_audioSource.Play();
					_audioSource.SetScheduledEndTime(AudioSettings.dspTime + 0.4d);

					_hasReachedCountdown = true;
				}

				if (_currentInterval <= 0)
				{
					image.texture = _whiteTexture;
				}

				if (_currentInterval <= -SOUND_INTERVAL)
				{
					_currentInterval = INTERVAL;
					image.texture = _blackTexture;
					_hasReachedCountdown = false;
				}
			}
			catch (Exception)
			{
				// ignored
			}
		}

		// TODO:
		public void Tick()
		{
			if (Time.time * 1000 >= _prevUpdate) //If time has passed
			{
				var currentUpdate = Mathf.FloorToInt(Time.time * 1000);
				var difference = currentUpdate - _prevUpdate; //Calculate difference in time
				FlashImage(difference); //Pass difference to flash image
				_prevUpdate = currentUpdate;
			}
		}
	}
}