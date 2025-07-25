using PulsarModLoader.Keybinds;
using PulsarModLoader.Utilities;
using System.Reflection;
using UnityEngine;
using System;
using HarmonyLib;

namespace NoPlayerHealing
{
    internal class NoPlayerHealingManager : MonoBehaviour
    {
        public float currentPlayerHealth = float.MaxValue;
        static bool wasDead = false;
        private int survivalBonusCounter = 0;
        private int healthBoost = 0;
        private float lastTimeAlive;
        private float StoredMaxHealth = 0;
        private PLPawn pawn = null;
        private PLPlayer player = null;
        private bool ModeSwitchCheck = false;
        private MethodBase method = AccessTools.Method(typeof(PLInput), "GetButtonDown", new Type[] { typeof(string) });
        void Awake()
        {
            pawn = this.GetComponent<PLPawn>();
            DontDestroyOnLoad(this);
            player = pawn.MyPlayer;
            currentPlayerHealth = pawn.Health;
            StoredMaxHealth = pawn.MaxHealth;
        }
        void Update()
        {
            if ((bool)method.Invoke(PLInput.Instance, new object[] { "NoPlayerHealing"}))
            {
                GUI.Active.Value = !GUI.Active;
                Messaging.Notification($"No Player Healing {(GUI.Active ? "Active" : "Inactive")}");
            }
            if (pawn == null)
            {
                pawn = this.GetComponent<PLPawn>();
                return;
            }
            if (player == null)
            {
                player = pawn.MyPlayer;
                return;
            }
            if (GUI.Active != ModeSwitchCheck)
            {
                ModeSwitchCheck = GUI.Active;
                if (ModeSwitchCheck)
                {
                    currentPlayerHealth = Mathf.Min(pawn.Health, pawn.MaxHealth);
                    StoredMaxHealth = pawn.MaxHealth;
                }
            }
            if (GUI.Active)
            {
                if (pawn.IsDead)
                {
                    StoredMaxHealth = pawn.MaxHealth;
                }
                if (pawn.MaxHealth != StoredMaxHealth)
                {
                    //Debug.Log($"{pawn.Health}, {StoredMaxHealth}, {pawn.MaxHealth - StoredMaxHealth}");
                    currentPlayerHealth = Mathf.Max(0.01f, (currentPlayerHealth + (pawn.MaxHealth - StoredMaxHealth)));
                    pawn.Health = currentPlayerHealth;
                    StoredMaxHealth = pawn.MaxHealth;
                }
                if (wasDead && !pawn.IsDead)
                {
                    lastTimeAlive = Time.time;
                    currentPlayerHealth = float.MaxValue;
                }
                wasDead = pawn.IsDead;

                //Increase health when max health is increased by warping.
                //int counter = PLServer.Instance.ClassInfos[player.GetClassID()].SurvivalBonusCounter;
                //if (counter < survivalBonusCounter)
                //{
                //    survivalBonusCounter = counter;
                //}
                //else if (counter > survivalBonusCounter)
                //{
                //    currentPlayerHealth += (counter - survivalBonusCounter) * 5;
                //    if (pawn.Health > 0)
                //    {
                //        pawn.Health += (counter - survivalBonusCounter) * 5;
                //    }
                //    survivalBonusCounter = counter;
                //}

                //Increase health when max health is increased by health boost talent points.
                //int boost = player.Talents[0] + player.Talents[57];
                //if (boost < healthBoost)
                //{
                //    currentPlayerHealth -= (healthBoost - boost) * 20;
                //    if (currentPlayerHealth <= 0)
                //    {
                //        currentPlayerHealth = 0.4f;
                //    }
                //    healthBoost = boost;
                //}
                //else if (boost > healthBoost)
                //{
                //    currentPlayerHealth += (boost - healthBoost) * 20;
                //    if (pawn.Health > 0)
                //    {
                //        pawn.Health += (boost - healthBoost) * 20;
                //    }
                //    healthBoost = boost;
                //}

                //Revert any other health increase
                if (Time.time - lastTimeAlive > 1)
                {
                    if (currentPlayerHealth > pawn.MaxHealth)
                    {
                        currentPlayerHealth = pawn.MaxHealth;
                    }

                    if (pawn.Health < currentPlayerHealth && pawn.Health > 0 && !pawn.IsDead)
                    {
                        currentPlayerHealth = pawn.Health;
                    }
                    else if (pawn.Health > currentPlayerHealth)
                    {
                        pawn.Health = currentPlayerHealth;
                    }
                }
            }
            
        }
    }
}
