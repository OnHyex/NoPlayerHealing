using HarmonyLib;
using PulsarModLoader;
using PulsarModLoader.CustomGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NoPlayerHealing
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

        public override string Author => "OnHyex";

        public override string ShortDescription => "Prevents the player from healing";

        public override string Name => "No Player Healing";

        public override string HarmonyIdentifier()
        {
            return $"{Author}.{Name}";
        }
    }
}