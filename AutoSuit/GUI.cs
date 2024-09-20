using UnityEngine;
using VoidManager.CustomGUI;
using VoidManager.Utilities;

namespace AutoSuit
{
    internal class GUI : ModSettingsMenu
    {
        public override string Name() => "Auto Suit";

        public override void Draw()
        {
            if (!VoidManagerPlugin.Enabled) GUILayout.Label("<color=red><size=30><b>MOD IS DISABLED UNTIL SESSION IS MOD_SESSION TYPE</b></size></color>");
            GUITools.DrawCheckbox("Keep suit on when too cold", ref Configs.KeepColdConfig);
            GUITools.DrawCheckbox("Keep suit on when too hot", ref Configs.KeepHotConfig);
            GUITools.DrawCheckbox("Keep suit on when no oxygen present", ref Configs.KeepOxygenConfig);
        }
    }
}
