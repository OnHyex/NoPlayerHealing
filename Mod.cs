using PulsarModLoader;
using PulsarModLoader.Keybinds;

namespace NoPlayerHealing
{
    public class Mod : PulsarMod, IKeybind
    {
        public static Mod Instance { get; private set; }

        public Mod()
        {
            Instance = this;
        }

        public override string Version => "1.0.0";

        public override string Author => "OnHyex";

        public override string ShortDescription => "Prevents the player from healing";

        public override string Name => "No Player Healing";

        public override string HarmonyIdentifier()
        {
            return $"{Author}.{Name}";
        }
        public void RegisterBinds(KeybindManager manager)
        {
            manager.NewBind("No Player Healing", "NoPlayerHealing", "Basics", "p");
        }
    }
}