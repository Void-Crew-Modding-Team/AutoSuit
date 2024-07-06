using VoidManager.MPModChecks;

namespace AutoSuit
{
    public class VoidManagerPlugin : VoidManager.VoidPlugin
    {
        public override MultiplayerType MPType => MultiplayerType.Client;

        public override string Author => "18107";

        public override string Description => "Automatically equips (or unequips) the suit when pressing the pressurize lever in the airlock";
    }
}
