using PulsarModLoader;
using static PLBurrowArena;

namespace No_Player_Healing
{
    public class Mod : PulsarMod
    {
        public static Mod Instance { get; private set; }

        public Mod()
        {
            Instance = this;
        }

        public override void Enable()
        {
            Player.currentPlayerHealth = PLNetworkManager.Instance.MyLocalPawn.Health;
            base.Enable();
        }

        public override string Version => "0.0.3";

        public override string Author => "18107";

        public override string ShortDescription => "Prevents the player from healing";

        public override string Name => "No Player Healing";

        public override string ModID => "noplayerhealing";

        public override string HarmonyIdentifier()
        {
            return "id107.noplayerhealing";
        }
    }
}
