using System;
using System.Collections.Generic;
using Harmony;
using HMUI;

namespace BetterLatencyAdjuster.HarmonyPatches
{
    [HarmonyPatch(typeof(NavigationController), "PushViewController",
        new Type[] { 
            typeof(ViewController), typeof(Action), typeof(bool)
        })]
    class EnterControllerPatch
    {
        static void Postfix(ref ViewController viewController, ref List<ViewController> ____viewControllers)
        {
            if (viewController.gameObject.GetInstanceID() == CurrentControllerPatch.instanceID) //Enable Settings when in it, disable everywhere else
                Plugin.enableSettings();
            else
                Plugin.disableSettings();
        }
    }
}
    