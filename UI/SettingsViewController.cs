using BeatSaberMarkupLanguage.Attributes;
using TMPro;
using UnityEngine;
using System;

namespace BetterLatencyAdjuster.UI
{
    class SettingsViewController : PersistentSingleton<SettingsViewController>
    {
        public BS_Utils.Utilities.Config config = new BS_Utils.Utilities.Config("BetterLatencyAdjuster");

        private readonly byte[] WHITE_IMAGE = System.Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAABhWlDQ1BJQ0MgcHJvZmlsZQAAKJF9kT1Iw0AYht+mSkUqHewgIpihOlkQLeIoVSyChdJWaNXB5NI/aNKQpLg4Cq4FB38Wqw4uzro6uAqC4A+Ii6uToouU+F1SaBHjwd09vPe9L3ffAUKzylSzZxJQNctIJ+JiLr8qBl4RxChCtMYkZurJzGIWnuPrHj6+30V5lnfdn2NAKZgM8InEc0w3LOIN4plNS+e8TxxmZUkhPieeMOiCxI9cl11+41xyWOCZYSObnicOE4ulLpa7mJUNlThGHFFUjfKFnMsK5y3OarXO2vfkLwwWtJUM12mOIIElJJGCCBl1VFCFhSjtGikm0nQe9/APO/4UuWRyVcDIsYAaVEiOH/wPfvfWLE5PuUnBOND7YtsfY0BgF2g1bPv72LZbJ4D/GbjSOv5aE5j9JL3R0SJHQGgbuLjuaPIecLkDDD3pkiE5kp+mUCwC72f0TXlg8BboX3P71j7H6QOQpV4t3wAHh8B4ibLXPd7d1923f2va/fsBuKlyw7LqvioAAAAJcEhZcwAALiMAAC4jAXilP3YAAAAHdElNRQfjDB8PFjs3QXKKAAAAGXRFWHRDb21tZW50AENyZWF0ZWQgd2l0aCBHSU1QV4EOFwAAAAxJREFUCNdj+P//PwAF/gL+3MxZ5wAAAABJRU5ErkJggg==");
        private readonly byte[] BLACK_IMAGE = System.Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAABhWlDQ1BJQ0MgcHJvZmlsZQAAKJF9kT1Iw0AYht+mSkUqHewgIpihOlkQLeIoVSyChdJWaNXB5NI/aNKQpLg4Cq4FB38Wqw4uzro6uAqC4A+Ii6uToouU+F1SaBHjwd09vPe9L3ffAUKzylSzZxJQNctIJ+JiLr8qBl4RxChCtMYkZurJzGIWnuPrHj6+30V5lnfdn2NAKZgM8InEc0w3LOIN4plNS+e8TxxmZUkhPieeMOiCxI9cl11+41xyWOCZYSObnicOE4ulLpa7mJUNlThGHFFUjfKFnMsK5y3OarXO2vfkLwwWtJUM12mOIIElJJGCCBl1VFCFhSjtGikm0nQe9/APO/4UuWRyVcDIsYAaVEiOH/wPfvfWLE5PuUnBOND7YtsfY0BgF2g1bPv72LZbJ4D/GbjSOv5aE5j9JL3R0SJHQGgbuLjuaPIecLkDDD3pkiE5kp+mUCwC72f0TXlg8BboX3P71j7H6QOQpV4t3wAHh8B4ibLXPd7d1923f2va/fsBuKlyw7LqvioAAAAJcEhZcwAALiMAAC4jAXilP3YAAAAHdElNRQfjDB8PDRLcxSF8AAAAGXRFWHRDb21tZW50AENyZWF0ZWQgd2l0aCBHSU1QV4EOFwAAAAxJREFUCNdjYGBgAAAABAABJzQnCgAAAABJRU5ErkJggg==");
        private const float INTERVAL = 3000;
        private Texture2D blackTexture = new Texture2D(1, 1);
        private Texture2D whiteTexture = new Texture2D(1, 1);
        private float currentInterval;
        private const float SOUND_INTERVAL = 100;
        private static AudioSource _audioSource;
        private static AudioClip _audioClip;
        public bool currentCheckboxVal { get; private set; }
        private int currentSliderVal;
        private int prevUpdate = 1;
        private bool hasReachedCountdown = false;

        [UIComponent("flashImage")]
        private UnityEngine.UI.RawImage image;

        [UIComponent("countdownText")]
        private TextMeshProUGUI countDownText;

        [UIValue("checkboxValue")]
        public bool checkboxValue
        {
            get => config.GetBool("BetterLatencyAdjuster", "Override Audio Latency", false);
            set => config.SetBool("BetterLatencyAdjuster", "Override Audio Latency", value);
        }

        [UIValue("sliderValue")]
        public int sliderValue
        {
            get => config.GetInt("BetterLatencyAdjuster", "Audio Latency Offset", 0);
            set => config.SetInt("BetterLatencyAdjuster", "Audio Latency Offset", value);
        }

        [UIAction("#post-parse")]
        internal void Setup()
        {
            enabled = false;
            currentCheckboxVal = checkboxValue;
            currentSliderVal = sliderValue;
            image.texture = blackTexture;
            blackTexture.LoadImage(BLACK_IMAGE);
            whiteTexture.LoadImage(WHITE_IMAGE);
            currentInterval = INTERVAL;
            countDownText.text = currentInterval.ToString();
        }

        [UIAction("checkboxOnChange")]
        internal void checkboxOnChange(bool newVal)
        {
            enabled = newVal;
            currentCheckboxVal = newVal;
            if(!newVal)
            {
                currentInterval = INTERVAL;
                countDownText.text = currentInterval.ToString();
                image.texture = blackTexture;
            }
        }

        [UIAction("sliderOnChange")]
        internal void sliderOnChange(int newVal)
        {
            currentSliderVal = newVal;
        }

        void Update()
        {
            if (Time.time * 1000 >= prevUpdate)
            {
                int currentUpdate = Mathf.FloorToInt(Time.time * 1000);
                int difference = currentUpdate - prevUpdate;
                prevUpdate = Mathf.FloorToInt(Time.time * 1000);
                flashImage(difference);
            }
        }

        private void flashImage(int difference)
        {
            try
            {
                currentInterval -= difference;
                if (currentInterval > 0)
                    countDownText.text = currentInterval.ToString();
                else
                    countDownText.text = "0";
                if (currentInterval <= currentSliderVal && !hasReachedCountdown)
                {
                    _audioSource.PlayOneShot(_audioClip);
                    hasReachedCountdown = true;
                }
                if (currentInterval <= 0)
                {
                    image.texture = whiteTexture;
                }
                if (currentInterval <= -SOUND_INTERVAL)
                {
                    currentInterval = INTERVAL;
                    image.texture = blackTexture;
                    hasReachedCountdown = false;
                }
            }
            catch(Exception) { }
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