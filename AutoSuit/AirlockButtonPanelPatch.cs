using CG.Client.Ship;
using CG.Game;
using CG.Game.Player;
using CG.Ship.Hull;
using Gameplay.Atmosphere;
using Gameplay.Utilities;
using HarmonyLib;
using Opsive.UltimateCharacterController.Traits;
using System.Linq;
using System.Reflection;
using VoidManager.Utilities;

namespace AutoSuit
{
    [HarmonyPatch(typeof(AirlockButtonPanel))]
    internal class AirlockButtonPanelPatch
    {
        private static readonly FieldInfo flyAbilityField = AccessTools.Field(typeof(JetpackItem), "_flyAbility");
        private static readonly FieldInfo OxygenDepositField = AccessTools.Field(typeof(LocalPlayer), "OxygenDeposit");

        [HarmonyPrefix]
        [HarmonyPatch("OnPressurizeButtonPressed")]
        static void OnPressurizeButtonPressed(AirlockButtonPanel __instance, AirlockPressurizationType type, Airlock ____airlock)
        {
            if (__instance.name == "AirlockButtonPanel (1)" && !____airlock.IsPressurizing && !____airlock.airlockDoors.Any(door => door.IsOpen))
            {
                ItemEquipper equipper = ClientGame.Current.PlayerShip.gameObject.GetComponentsInChildren<ItemEquipper>().FirstOrDefault(item => item.name == "JetpackRack");
                if (equipper == null) return;
                JetpackItem jetpack = LocalPlayer.Instance.gameObject.GetComponentInChildren<JetpackItem>();
                FlyJetpack flyAbility = (FlyJetpack)flyAbilityField.GetValue(jetpack);
                ModifiableFloat jetpackOxygen = LocalPlayer.Instance.JetpackOxygen;
                float oxygen = jetpackOxygen.Value;
                if (type == AirlockPressurizationType.Pressurize)
                {
                    ____airlock.StateChanged += CheckRemoveSuit;
                }
                else
                {
                    if (!(flyAbility?.Enabled ?? false))
                        equipper.ToggleEquippedItem();
                }

                void CheckRemoveSuit()
                {
                    if (____airlock.IsPressurizing) return;
                    ____airlock.StateChanged -= CheckRemoveSuit;

                    AtmosphereValues atmosphere = LocalPlayer.Instance.GetComponent<CharacterAtmosphericDataTracker>().AtmosphereData;
                    if ((flyAbility?.Enabled ?? false) &&
                        (!Configs.KeepColdConfig.Value || atmosphere.Temperature > -100) &&
                        (!Configs.KeepHotConfig.Value || atmosphere.Temperature < 100) &&
                        (!Configs.KeepOxygenConfig.Value || atmosphere.Oxygen > 0.02f))
                    {
                        equipper.ToggleEquippedItem();
                        Attribute oxygen = (Attribute)OxygenDepositField.GetValue(LocalPlayer.Instance);
                        oxygen.Value = oxygen.MaxValue;
                    }
                }
            }
        }
    }
}
