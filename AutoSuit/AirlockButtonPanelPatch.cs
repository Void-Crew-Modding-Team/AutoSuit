using CG.Client.Ship;
using CG.Game.Player;
using CG.Ship.Hull;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoSuit
{
    [HarmonyPatch(typeof(AirlockButtonPanel))]
    internal class AirlockButtonPanelPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("OnPressurizeButtonPressed")]
        static void OnPressurizeButtonPressed(AirlockButtonPanel __instance, AirlockPressurizationType type, Airlock ____airlock)
        {
            if (__instance.name == "AirlockButtonPanel (1)" && !____airlock.IsPressurizing && !____airlock.airlockDoors.Any(door => door.IsOpen))
            {
                JetpackItem jetpack = LocalPlayer.Instance.gameObject.GetComponentInChildren<JetpackItem>();
                if (type == AirlockPressurizationType.Pressurize)
                {
                    DelayDo(jetpack.Unequip, 2000); //TODO replace with VoidManager.Utilities.Tools.DelayDo
                }
                else
                {
                    jetpack.Equip(true);
                }
            }
        }

        //TODO remove everything below
        private static List<Tuple<Action, DateTime>> tasks = new();

        private static void DelayDo(Action action, double delayMs)
        {
            DateTime time = DateTime.Now.AddMilliseconds(delayMs);
            tasks.Add(Tuple.Create(action, time));

            if (tasks.Count == 1)
            {
                VoidManager.Events.Instance.LateUpdate += DoTasks;
            }
        }

        private static void DoTasks(object sender, EventArgs e)
        {
            for (int i = tasks.Count - 1; i >= 0; i--)
            {
                if (tasks[i].Item2 <= DateTime.Now)
                {
                    tasks[i].Item1.Invoke();
                    tasks.RemoveAt(i);
                }
            }

            if (tasks.Count == 0)
            {
                VoidManager.Events.Instance.LateUpdate -= DoTasks;
            }
        }
    }
}
