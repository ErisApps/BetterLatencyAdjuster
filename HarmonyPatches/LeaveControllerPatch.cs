using System;
using Harmony;
using HMUI;

namespace BetterLatencyAdjuster.HarmonyPatches
{
    [HarmonyPatch(typeof(ViewController), "DismissViewControllerCoroutine",
        new Type[] {
            typeof(Action), typeof(bool)
        })]
    class LeaveControllerPatch
    {
        static void Prefix(ref ViewController __instance)
        {
            try
            {
                NavigationController navigationController = (NavigationController)__instance;
                if (navigationController.viewControllers.Exists(a => a.GetType().ToString() == "BeatSaberMarkupLanguage.Settings.UI.ViewControllers.SettingsMenuListViewController"))
                    Plugin.disableSettings();
            }
            catch (Exception) { }
        }
    }
}