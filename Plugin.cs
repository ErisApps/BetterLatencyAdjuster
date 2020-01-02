using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using IPA;
using IPA.Config;
using IPA.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;
using BeatSaberMarkupLanguage.Settings;
using BetterLatencyAdjuster.UI;

namespace BetterLatencyAdjuster
{
    public class Plugin : IBeatSaberPlugin
    {
        internal static string Name => "BetterLatencyAdjuster";
        private SettingsViewController settingsView;
        public void Init(IPALogger logger)
        {
            Logger.log = logger;
            Logger.log.Debug("Logger initialised.");
        }

        #region BSIPA Config
        // Uncomment to use BSIPA's config
        //internal static Ref<PluginConfig> config;
        //internal static IConfigProvider configProvider;
        //public void Init(IPALogger logger, [Config.Prefer("json")] IConfigProvider cfgProvider)
        //{
        //    Logger.log = logger;
        //    Logger.log.Debug("Logger initialised.");

        //    configProvider = cfgProvider;

        //    config = configProvider.MakeLink<PluginConfig>((p, v) =>
        //    {
        //        // Build new config file if it doesn't exist or RegenerateConfig is true
        //        if (v.Value == null || v.Value.RegenerateConfig)
        //        {
        //            Logger.log.Debug("Regenerating PluginConfig");
        //            p.Store(v.Value = new PluginConfig()
        //            {
        //                // Set your default settings here.
        //                RegenerateConfig = false
        //            });
        //        }
        //        config = v;
        //    });
        //}
        #endregion
        public void OnApplicationStart()
        {
            Logger.log.Debug("OnApplicationStart");
            settingsView = SettingsViewController.instance;
            BSMLSettings.instance.AddSettingsMenu("Adjust Latency", "BetterLatencyAdjuster.UI.Settings.bsml", settingsView);
        }

        public void OnApplicationQuit()
        {
            Logger.log.Debug("OnApplicationQuit");

        }

        public void OnUpdate()
        {
            try
            {
                settingsView.FlashImage();
            }
            catch (Exception e)
            {
            }
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

    }
}
