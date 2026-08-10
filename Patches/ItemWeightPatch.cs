using EFT;
using EFT.InventoryLogic;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;
using Tosox.DynamicItemWeights.Configuration;
using Tosox.DynamicItemWeights.Helpers;
using UnityEngine;

namespace Tosox.DynamicItemWeights.Patches
{
    internal class ItemWeightPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.PropertyGetter(typeof(Item), nameof(Item.Weight));
        }

        [PatchPostfix]
        public static void PatchPostfix(Item __instance, ref float __result)
        {
            if (!Settings.Enabled.Value)
                return;

            // Only modify items with consumable usage, and leave full ones alone
            if (!UsageHelper.TryGetUsageFraction(__instance, out float usage) || usage >= 0.9999f)
                return;

            float original = __result;
            string templateId = __instance.StringTemplateId;

            // Determine tare weight
            if (!TareWeightDB.TryGet(templateId, out float tareWeight))
            {
                tareWeight = Settings.DefaultTareFraction.Value * original;
                Logger.LogWarning($"Item '{__instance.LocalizedName()}' ({templateId}) not found in tare weight DB, falling back to the default fraction");
            }

            // A fully used item can never weigh more than a full one
            tareWeight = Mathf.Clamp(tareWeight, 0.0f, original);

            __result = Mathf.Lerp(tareWeight, original, usage);

            if (Settings.VerboseLogging.Value)
                Logger.LogDebug($"Item '{__instance.LocalizedName()}' weight adjusted: {original} -> {__result} (tare: {tareWeight}, usage: {usage})");
        }
    }
}
