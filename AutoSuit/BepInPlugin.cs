﻿using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using VoidManager.MPModChecks;

namespace AutoSuit
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.USERS_PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Void Crew.exe")]
    [BepInDependency(VoidManager.MyPluginInfo.PLUGIN_GUID)]
    public class BepinPlugin : BaseUnityPlugin
    {
        internal static BepinPlugin instance;
        internal static ManualLogSource Log;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "N/A")]
        private void Awake()
        {
            instance = this;
            Log = Logger;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            Configs.Load(this);
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
    public class VoidManagerPlugin : VoidManager.VoidPlugin
    {
        internal static bool Enabled = true;
        public override MultiplayerType MPType => MultiplayerType.Client;

        public override string Author => MyPluginInfo.PLUGIN_ORIGINAL_AUTHOR;

        public override string Description => MyPluginInfo.PLUGIN_DESCRIPTION;
        public override VoidManager.SessionChangedReturn OnSessionChange(VoidManager.SessionChangedInput input)
        {
            Enabled = (input.IsHost || input.IsMod_Session);
            VoidManager.SessionChangedReturn _return = new VoidManager.SessionChangedReturn() { SetMod_Session = true};
            return _return;
        }
    }
}