using System;
using IPA;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using BeatSaberMarkupLanguage.Settings;
using BetterLatencyAdjuster.UI;
using Harmony;
using System.Reflection;

namespace BetterLatencyAdjuster
{
    public class Plugin : IBeatSaberPlugin
    {
        internal static string Name => "BetterLatencyAdjuster";
        public const string HarmonyId = "com.github.rithik-b.BetterLatencyAdjuster";
        internal static HarmonyInstance harmony;
        private static SettingsViewController settingsViewController = SettingsViewController.instance;

        public void Init(IPALogger logger)
        {
            Logger.log = logger;
            harmony = HarmonyInstance.Create(HarmonyId);
        }

       public void OnApplicationStart()
        {
            BSMLSettings.instance.AddSettingsMenu("Adjust Latency", "BetterLatencyAdjuster.UI.Settings.bsml", settingsViewController);
            ApplyHarmonyPatches();
        }

        public void OnApplicationQuit()
        {
        }

        public void OnUpdate()
        {
            
        }

        public void OnFixedUpdate()
        {
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
        {
        }

        public void OnSceneUnloaded(Scene scene)
        {
        }

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
        }
        
        public static void ApplyHarmonyPatches()
        {
            try
            {
                Logger.log.Debug("Applying Harmony patches.");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Logger.log.Critical("Error applying Harmony patches: " + ex.Message);
                Logger.log.Debug(ex);
            }
        }

        public static bool getCheckboxValue()
        {
            return settingsViewController.checkboxValue;
        }

        public static int getSliderValue()
        {
            return settingsViewController.sliderValue;
        }

        public static void disableSettings()
        {
            settingsViewController.enabled = false;
        }

        public static void enableSettings()
        {
            settingsViewController.enabled = settingsViewController.currentCheckboxVal;
        }
    }
}
