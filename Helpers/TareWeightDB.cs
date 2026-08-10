using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Tosox.DynamicItemWeights.Helpers
{
    internal static class TareWeightDB
    {
        private static readonly string ConfigPath = Path.Combine(Plugin.ConfigFolder, "tareweights.jsonc");

        private static Dictionary<string, float> _tareWeights = new Dictionary<string, float>();

        internal static bool Load()
        {
            if (!File.Exists(ConfigPath))
            {
                Plugin.Log.LogError($"Failed to load tare weights: '{ConfigPath}' is missing");
                return false;
            }

            try
            {
                var weights = JsonConvert.DeserializeObject<Dictionary<string, float>>(File.ReadAllText(ConfigPath));
                if (weights == null)
                {
                    Plugin.Log.LogError("Failed to load tare weights: deserialized data is null");
                    return false;
                }

                _tareWeights = weights;
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"Failed to load tare weights: {ex}");
                return false;
            }

            Plugin.Log.LogInfo($"Loaded {_tareWeights.Count} tare weights");
            return true;
        }

        internal static bool TryGet(string templateId, out float tareWeight)
        {
            return _tareWeights.TryGetValue(templateId, out tareWeight);
        }
    }
}
