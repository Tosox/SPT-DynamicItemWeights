using BepInEx.Configuration;

namespace Tosox.DynamicItemWeights.Configuration
{
    internal static class Settings
    {
        private const string GeneralSection = "1. General";
        private const string DebugSection = "2. Debug";

        internal static ConfigEntry<bool> Enabled;
        internal static ConfigEntry<float> DefaultTareFraction;
        internal static ConfigEntry<bool> VerboseLogging;

        internal static void Init(ConfigFile config)
        {
            Enabled = config.Bind(GeneralSection, "Enabled", true,
                new ConfigDescription("Untick to disable dynamic item weights",
                    null, Order(0)));

            DefaultTareFraction = config.Bind(GeneralSection, "Default Tare Fraction", 0.125f,
                new ConfigDescription("Fallback tare weight as fraction of original weight",
                    new AcceptableValueRange<float>(0.0f, 1.0f), Order(1)));

            VerboseLogging = config.Bind(DebugSection, "Verbose Logging", false,
                new ConfigDescription("Log every weight adjustment",
                    null, Order(2)));
        }

        private static ConfigurationManagerAttributes Order(int position)
        {
            return new ConfigurationManagerAttributes { Order = short.MaxValue - position };
        }
    }
}
