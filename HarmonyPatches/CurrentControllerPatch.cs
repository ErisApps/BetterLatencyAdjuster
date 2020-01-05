using System;
using System.Xml;
using BeatSaberMarkupLanguage;
using BetterLatencyAdjuster.UI;
using Harmony;
using UnityEngine;

namespace BetterLatencyAdjuster.HarmonyPatches
{
    [HarmonyPatch(typeof(BSMLParser), "Parse",
        new Type[] { 
            typeof(XmlNode), typeof(GameObject), typeof(object)
        })]
    class CurrentControllerPatch
    {
        public const string VIEW_CONTROLLER_NAME = "BetterLatencyAdjuster";
        public static int instanceID { get; private set; } // The ID of the game object of the Settings View Controller
        static void Postfix(ref object host, ref BSMLParser __instance, ref GameObject parent)
        {
            if (host.GetType() == typeof(SettingsViewController))
                instanceID = parent.GetInstanceID();
        }
    }
}
    