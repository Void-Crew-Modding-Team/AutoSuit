using VoidManager.CustomGUI;
using VoidManager.Utilities;

namespace AutoSuit
{
    internal class GUI : ModSettingsMenu
    {
        public override string Name() => "Auto Suit";

        public override void Draw()
        {
            GUITools.DrawCheckbox("Keep suit on when too cold", ref Configs.KeepColdConfig);
            GUITools.DrawCheckbox("Keep suit on when too hot", ref Configs.KeepHotConfig);
            GUITools.DrawCheckbox("Keep suit on when no oxygen present", ref Configs.KeepOxygenConfig);
        }
    }
}
