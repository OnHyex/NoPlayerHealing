using static UnityEngine.GUILayout;
using PulsarModLoader.CustomGUI;
using System;

namespace No_Player_Healing
{
    internal class GUI : ModSettingsMenu
    {
        public override string Name()
        {
            return Mod.Instance.Name;
        }

        public override void Draw()
        {
            bool modOn = Toggle(Mod.Instance.IsEnabled(), "Mod Active");
            if (modOn != Mod.Instance.IsEnabled())
            {
                if (modOn)
                {
                    Mod.Instance.Enable();
                }
                else
                {
                    Mod.Instance.Disable();
                }
            }
        }
    }
}
