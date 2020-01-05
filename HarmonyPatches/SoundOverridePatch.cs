using System;
using Harmony;
using UnityEngine;

namespace BetterLatencyAdjuster.HarmonyPatches
{
    [HarmonyPatch(typeof(BasicUIAudioManager), "Start",
        new Type[] { 
        })]
    class SoundOverridePatch
    {
        static void Postfix(ref AudioSource ____audioSource, ref AudioClip[] ____clickSounds)
        {
            UI.SettingsViewController.setSource(____audioSource);
            UI.SettingsViewController.setClip(____clickSounds[0]);
        }
    }
}
