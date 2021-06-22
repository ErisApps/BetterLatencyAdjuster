# BetterLatencyAdjuster

This mod provides a better (or at least slightly different) UI to adjust audio latency in beat saber.
Instead of the bouncing ball UI in the base game, this uses a black square that flashes white whenever audio is played,
making it easier to adjust the latency offset.

## Download and installation instructions
This mod requires a few other mods in order to work.

- BeatSaberMarkupLanguage v1.5.3 or higher
- BSIPA v4.1.6 or higher
- SiraUtil v2.5.5 or higher

The installation is fairly simple.

1. Grab the latest plugin release from the [releases page](https://github.com/ErisApps/BetterLatencyAdjuster/releases) (once there is actually a release)
2. Drop the .dll file in the "Plugins" folder of your Beat Saber installation.
3. Boot up (or reboot) the game.

## How to use
1. Go to Adjust Latency under Mod Settings
2. Enable "Override Audio Latency"
3. Move the slider around until you hear the audio at the same time as the box flashing
4. Once you're satisfied with the result, click Ok.
![Screenshot of the mod' settings view](https://i.imgur.com/Cy8JxE5.png)

#### Remarks
- The settings the audio latency settings can be adjusted in either the basegame settings or the settings panel of this mod. When changed in either one or the other, the values will carried over in either direction.
- If you uninstall the mod and don't want to keep the latency override, you can still disable the latency override from the game's settings.

## Issues and Support
If you encounter any issues with this mod, please create an issue on Github using the issues tab above (and DM me on Discord Eris#4747 so it doesn't take me like a month to realize there's an issue...) and I'll try to get to it as soon as I can.
For support with the mod, ask the supports in the #pc-help channel (or mention me in said channel) in the [BSMG discord](discord.gg/beatsabermods).

## Developers
To build this project you will need to create a `BetterLatencyAdjuster/BetterLatencyAdjuster.csproj.user` file specifying where the game is located.
This can be done using either of the following options:

1) Create the file using the [BSMT Visual Studio extension](https://github.com/Zingabopp/BeatSaberModdingTools).
2) Manually create the file, copy the xml below into the file and change the path to your accomodate for your installation. The file shows up as a missing file in the solution explorer, so it should be easy to spot.

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
	<PropertyGroup>
		<!-- Change this path if necessary. Make sure it doesn't end with a backslash. -->
		<BeatSaberDir>D:\Program Files (x86)\Steam\steamapps\common\Beat Saber</BeatSaberDir>
	</PropertyGroup>
</Project>
```

## Credits
Credit where credit is due:

- [@PixelBoom](https://github.com/rithik-b) for developing the original mod
- @Mdot for giving feedback during creation of the original mod