# BetterLatencyAdjuster
**This mod is now archived and no longer supported.**

- A better gui to adjust audio latency in beat saber. (As suggested by Mdot)
- Instead of the bouncing ball UI in the base game, this uses a black square that flashes white whenever audio is played, 
making it easier to adjust the latency offset.

## How to use
- Go to Adjust Latency under Mod Settings
- Enable "Override Audio Latency"
- Move the slider around until you hear the audio at the same time as the box flashing
![](https://i.imgur.com/Cy8JxE5.png)
- Click Apply and Ok
- Go to Settings>Audio Latency (the ingame latency adjuster) and just click Apply and Ok for the changes to take effect in game (the values from the mod settings should automatically transfer over after)
![](https://i.imgur.com/JHhxjVc.png)
- To disable the override, disable "Override Audio Latency" in the Mod's Settings **first**, then do it in the game's settings
- If you uninstall the mod and don't want to keep the latency override, you must disable the latency override from the game's settings

## Download and Installation instructions
- Download the latest version of the mod (v1.0.2) [here](https://github.com/rithik-b/BetterLatencyAdjuster/releases/tag/1.0.2 "here")
- Get the latest version of BSML and BS Utils from [ModAssistant](https://github.com/Assistant/ModAssistant "ModAssistant")
- Drag and drop the dll file into the "Plugins" folder of your Beat Saber Installation Directory

## Issues and Support
If you encounter any issues with this mod, PM me on Discord PixelBoom#6948 or file an Issue on GitHub and I'll fix it as soon as I can.
For support with the mod, DM me or @ me on the #pc-mod-support channel on the BSMG discord.

## Compilation Instructions (If you just want to use the plugin, download it from the Releases Tab)
- Open the project in Visual Studio
- Add missing references by locating them in your Beat Saber\Beat Saber_Data\Managed and Beat Saber\Plugins directories
- Build the project
- The plugin would be under BetterLatencyAdjuster\bin\Debug
