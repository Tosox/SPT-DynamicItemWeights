using EFT.InventoryLogic;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;
using Tosox.DynamicItemWeights.Configuration;
using Tosox.DynamicItemWeights.Helpers;

namespace Tosox.DynamicItemWeights.Patches
{
    internal class ItemRefreshPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(Item), nameof(Item.RaiseRefreshEvent), new[] { typeof(bool), typeof(bool) });
        }

        [PatchPostfix]
        public static void PatchPostfix(Item __instance)
        {
            if (!Settings.Enabled.Value)
                return;

            // Only items whose weight this mod actually changes are worth a recalculation
            if (!UsageHelper.TryGetUsageFraction(__instance, out _))
                return;

            if (!(__instance.Owner is InventoryController controller) || controller.Inventory == null)
                return;

            // Hand the game its own event type so its equipment check still decides what counts
            controller.Inventory.UpdateTotalWeight(
                new ItemEventArgs(__instance, CommandStatus.Succeed, __instance.Owner, __instance.CurrentAddress));
        }
    }
}
