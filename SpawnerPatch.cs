using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace NoPlayerHealing
{
    [HarmonyPatch(typeof(PLPlayer), "SpawnPawnForPlayer")]
    internal class SpawnPawnPatch
    {
        // Token: 0x06000011 RID: 17 RVA: 0x00002B90 File Offset: 0x00000D90
        private static void Postfix(PLPlayer __instance, ref PLPawn __result)
        {
            if (__instance == PLNetworkManager.Instance.LocalPlayer)
            {
                __result.MyPlayer = __instance;
                NoPlayerHealingManager temp = __result.gameObject.AddComponent<NoPlayerHealingManager>();
            }
        }
    }
}
