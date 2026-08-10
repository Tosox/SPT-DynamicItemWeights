using EFT.InventoryLogic;
using UnityEngine;

namespace Tosox.DynamicItemWeights.Helpers
{
    internal static class UsageHelper
    {
        internal static bool TryGetUsageFraction(Item item, out float fraction)
        {
            if (item.TryGetItemComponent<MedKitComponent>(out var medkit))
            {
                fraction = medkit.RelativeValue;
            }
            else if (item.TryGetItemComponent<FoodDrinkComponent>(out var food) && food.MaxResource > 1.0f)
            {
                fraction = food.RelativeValue;
            }
            else if (item is Fuel && item.TryGetItemComponent<ResourceComponent>(out var resource))
            {
                fraction = resource.RelativeValue;
            }
            else if (item.TryGetItemComponent<RepairKitComponent>(out var repairKit))
            {
                fraction = repairKit.Resource / repairKit._template.MaxRepairResource;
            }
            else
            {
                fraction = 1.0f;
                return false;
            }

            // Treat a bad ratio as full so the weight is left alone
            fraction = float.IsNaN(fraction) ? 1.0f : Mathf.Clamp01(fraction);
            return true;
        }
    }
}
