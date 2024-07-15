using BepInEx.Configuration;

namespace AutoSuit
{
    internal class Configs
    {
        internal static ConfigEntry<bool> KeepColdConfig;
        internal static ConfigEntry<bool> KeepHotConfig;
        internal static ConfigEntry<bool> KeepOxygenConfig;

        internal static void Load(BepinPlugin plugin)
        {
            KeepColdConfig = plugin.Config.Bind("AutoSuit", "KeepOnWhenCold", true);
            KeepHotConfig = plugin.Config.Bind("AutoSuit", "KeepOnWhenHot", true);
            KeepOxygenConfig = plugin.Config.Bind("AutoSuit", "KeepOnWhenLowOxygen", true);
        }
    }
}
