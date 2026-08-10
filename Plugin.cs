using BepInEx;
using BepInEx.Logging;
using System.IO;
using System.Reflection;
using Tosox.DynamicItemWeights.Configuration;
using Tosox.DynamicItemWeights.Helpers;
using Tosox.DynamicItemWeights.Patches;

namespace Tosox.DynamicItemWeights
{
    [BepInPlugin("de.tosox.dynamicitemweights", PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        internal const string PluginName = "Dynamic Item Weights";
        internal const string PluginVersion = "1.2.0";
        internal const string PluginAuthor = "Tosox";
        internal const string PluginSource = "https://github.com/Tosox/SPT-DynamicItemWeights";

        internal static readonly string PluginFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        internal static readonly string ConfigFolder = Path.Combine(PluginFolder, Assembly.GetExecutingAssembly().GetName().Name);

        internal static ManualLogSource Log { get; private set; }

        internal void Awake()
        {
            Log = Logger;
            Settings.Init(Config);

            if (!TareWeightDB.Load())
            {
                Logger.LogWarning("Plugin will be disabled");
                return;
            }

            new ItemWeightPatch().Enable();

            Logger.LogInfo("Plugin loaded successfully");
        }
    }
}
