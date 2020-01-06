using System;
using BeatSaberMarkupLanguage;
using BetterLatencyAdjuster.UI;
using Harmony;
using HMUI;
using UnityEngine;

namespace BetterLatencyAdjuster.HarmonyPatches
{
    [HarmonyPatch(typeof(AudioLatencyViewController), "DidActivate",
        new Type[] { 
            typeof(bool), typeof(ViewController.ActivationType)
        })]
    class SaveOverridePatch
    {
        static void Postfix(ref AudioLatencyViewController __instance)
        {
            Logger.log.Critical("Activated");
            if(Plugin.getCheckboxValue())
            {
                __instance.HandleOverrideAudioLatencyToggleValueChanged(true);
                __instance.SliderValueDidChange(null, Plugin.getSliderValue());
            }
        }
    }
}
    