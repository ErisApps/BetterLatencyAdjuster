using System;
using System.Collections.Generic;
using Harmony;
using HMUI;

namespace BetterLatencyAdjuster.HarmonyPatches
{
    [HarmonyPatch(typeof(NavigationController), "LayoutViewControllers",
        new Type[] { 
            typeof(List<ViewController>)
        })]
    class CurrentControllerPatch
    {
        static void Prefix(ref List<ViewController> viewControllers)
        {
            try
            {
                if (viewControllers.Exists(a => a.GetType().ToString() == "BeatSaberMarkupLanguage.Settings.UI.ViewControllers.SettingsMenuListViewController"))
                    Plugin.enableSettings();
            }
            catch (Exception) { }
        }
    }
}
    