using System;
using PulsarModLoader.CustomGUI;
using PulsarModLoader;
using UnityEngine;

namespace NoPlayerHealing
{
    internal class GUI : ModSettingsMenu
    {
        public static SaveValue<bool> Active = new SaveValue<bool>("Active", true);
        public override string Name()
        {
            return "NoPlayerHealing";
        }
        public override void Draw()
        {
            Active.Value = GUILayout.Toggle(Active.Value, "Enabled", Array.Empty<GUILayoutOption>());
        }
    }
}