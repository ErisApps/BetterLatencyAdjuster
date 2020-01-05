using BeatSaberMarkupLanguage.Attributes;
using TMPro;
using UnityEngine;
using System;

namespace BetterLatencyAdjuster.UI
{
    class SettingsViewController : PersistentSingleton<SettingsViewController>
    {
        private readonly byte[] WHITE_IMAGE = System.Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAABhWlDQ1BJQ0MgcHJvZmlsZQAAKJF9kT1Iw0AYht+mSkUqHewgIpihOlkQLeIoVSyChdJWaNXB5NI/aNKQpLg4Cq4FB38Wqw4uzro6uAqC4A+Ii6uToouU+F1SaBHjwd09vPe9L3ffAUKzylSzZxJQNctIJ+JiLr8qBl4RxChCtMYkZurJzGIWnuPrHj6+30V5lnfdn2NAKZgM8InEc0w3LOIN4plNS+e8TxxmZUkhPieeMOiCxI9cl11+41xyWOCZYSObnicOE4ulLpa7mJUNlThGHFFUjfKFnMsK5y3OarXO2vfkLwwWtJUM12mOIIElJJGCCBl1VFCFhSjtGikm0nQe9/APO/4UuWRyVcDIsYAaVEiOH/wPfvfWLE5PuUnBOND7YtsfY0BgF2g1bPv72LZbJ4D/GbjSOv5aE5j9JL3R0SJHQGgbuLjuaPIecLkDDD3pkiE5kp+mUCwC72f0TXlg8BboX3P71j7H6QOQpV4t3wAHh8B4ibLXPd7d1923f2va/fsBuKlyw7LqvioAAAAJcEhZcwAALiMAAC4jAXilP3YAAAAHdElNRQfjDB8PFjs3QXKKAAAAGXRFWHRDb21tZW50AENyZWF0ZWQgd2l0aCBHSU1QV4EOFwAAAAxJREFUCNdj+P//PwAF/gL+3MxZ5wAAAABJRU5ErkJggg==");
        private readonly byte[] BLACK_IMAGE = System.Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAABhWlDQ1BJQ0MgcHJvZmlsZQAAKJF9kT1Iw0AYht+mSkUqHewgIpihOlkQLeIoVSyChdJWaNXB5NI/aNKQpLg4Cq4FB38Wqw4uzro6uAqC4A+Ii6uToouU+F1SaBHjwd09vPe9L3ffAUKzylSzZxJQNctIJ+JiLr8qBl4RxChCtMYkZurJzGIWnuPrHj6+30V5lnfdn2NAKZgM8InEc0w3LOIN4plNS+e8TxxmZUkhPieeMOiCxI9cl11+41xyWOCZYSObnicOE4ulLpa7mJUNlThGHFFUjfKFnMsK5y3OarXO2vfkLwwWtJUM12mOIIElJJGCCBl1VFCFhSjtGikm0nQe9/APO/4UuWRyVcDIsYAaVEiOH/wPfvfWLE5PuUnBOND7YtsfY0BgF2g1bPv72LZbJ4D/GbjSOv5aE5j9JL3R0SJHQGgbuLjuaPIecLkDDD3pkiE5kp+mUCwC72f0TXlg8BboX3P71j7H6QOQpV4t3wAHh8B4ibLXPd7d1923f2va/fsBuKlyw7LqvioAAAAJcEhZcwAALiMAAC4jAXilP3YAAAAHdElNRQfjDB8PDRLcxSF8AAAAGXRFWHRDb21tZW50AENyZWF0ZWQgd2l0aCBHSU1QV4EOFwAAAAxJREFUCNdjYGBgAAAABAABJzQnCgAAAABJRU5ErkJggg==");
        private float interval = 10 / Time.deltaTime;
        private Texture2D blackTexture = new Texture2D(1, 1);
        private Texture2D whiteTexture = new Texture2D(1, 1);
        private float currentInterval;
        private float soundInterval = (float) 0.5 / Time.deltaTime;
        private static AudioSource _audioSource;
        private static AudioClip _audioClip;

        [UIComponent("flashImage")]
        private UnityEngine.UI.RawImage _flashImage;

        [UIComponent("countdownText")]
        private TextMeshProUGUI _countDownText;

        [UIAction("#post-parse")]
        internal void Setup()
        {
            enabled = false;
            _flashImage.texture = blackTexture;
            blackTexture.LoadImage(BLACK_IMAGE);
            whiteTexture.LoadImage(WHITE_IMAGE);
            currentInterval = interval;
        }

        void Update()
        {
            try
            {
                currentInterval--;
                _countDownText.text = currentInterval.ToString();
                if (currentInterval == 0)
                {
                    _flashImage.texture = whiteTexture;
                    _audioSource.PlayOneShot(_audioClip);
                }
                else if (currentInterval == -soundInterval)
                {
                    currentInterval = interval;
                    _flashImage.texture = blackTexture;
                }
            } catch (Exception)
            {
            }
        }

        public static void setSource(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public static void setClip(AudioClip audioClip)
        {
            _audioClip = audioClip;
        }
    }
}