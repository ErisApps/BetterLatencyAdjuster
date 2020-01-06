using System;
using Harmony;
using HMUI;

namespace BetterLatencyAdjuster.HarmonyPatches
{
    [HarmonyPatch(typeof(AudioLatencyViewController), "DidActivate",
        new Type[] { 
            typeof(bool), typeof(ViewController.ActivationType)
        })]
    class SaveOverridePatch
    {
        private const float MILLISECONDS_TO_SECONDS = 1000;
        static void Postfix(ref AudioLatencyViewController __instance, ref FloatSO ____audioLatency)
        {
            if(Plugin.getCheckboxValue())
            {
                __instance.HandleOverrideAudioLatencyToggleValueChanged(true);
                __instance.SliderValueDidChange(null, (float) Plugin.getSliderValue() / (float) MILLISECONDS_TO_SECONDS);
            }
        }
    }
}
    