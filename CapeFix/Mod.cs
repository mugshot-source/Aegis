using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BepInEx;
using HarmonyLib;
using JotunnLib;
using JotunnLib.Managers;
using JotunnLib.Entities;
using BepInEx.Logging;

namespace Aegis
{
    [BepInPlugin("Aegis", "MagicShield", "1.0.0")]
    [BepInDependency(JotunnLib.JotunnLib.ModGuid)]  

    public class Mod : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("Aegis");
        public static ManualLogSource log;
        private void Awake()
        {
            PrefabManager.Instance.PrefabRegister += RegisterPrefabs;
            ObjectManager.Instance.ObjectRegister += registerObjects;
            harmony.PatchAll();
        }

        private void RegisterPrefabs(object sender, EventArgs e)
        {
            //AccessTools.Method(typeof(PrefabManager), "RegisterPrefab", new Type[] { typeof(GameObject), typeof(string) }).Invoke(PrefabManager.Instance, new Object[] {, });
            PrefabManager.Instance.RegisterPrefab(new Aegis());
        }
        private void registerObjects(object sender, EventArgs e)
        {
            ObjectManager.Instance.RegisterItem("Aegis");

            ObjectManager.Instance.RegisterRecipe(new RecipeConfig()
            {
                Name = "Recipe_Aegis",
                Item = "Aegis",
                CraftingStation = "forge",
                RepairStation = "forge",

                Requirements = new PieceRequirementConfig[]
                {
                    new PieceRequirementConfig()
                    {
                        Item = "Crystal",
                        Amount = 5
                    },
                    new PieceRequirementConfig()
                    {
                        Item = "Silver",
                        Amount = 30
                    },
                     new PieceRequirementConfig()
                    {
                        Item = "FineWood",
                        Amount = 10
                    },
                     new PieceRequirementConfig()
                    {
                        Item = "GoblinTotem",
                        Amount = 4
                    }
                }
            });
            }


        static void PlayEffect(string prefabN, Vector3 pos)
        {
            GameObject prefab = ZNetScene.instance.GetPrefab(prefabN);
            if (prefab != null)
            {
                UnityEngine.Object.Instantiate<GameObject>(prefab, pos, Quaternion.identity);
                return;
            }
            log.LogWarning("Failed to locate effect " + prefabN + " prefab");
        }

        [HarmonyPatch(typeof(SE_Shielded), "Setup")]
        class ShieldPatch
        {
            static void Prefix()
            {
                Player localPlayer = Player.m_localPlayer;
                Vector3 vector = localPlayer.transform.position;
                PlayEffect("fx_guardstone_permitted_add", vector);
                //PlayEffect("fx_guardstone_permitted_add", vector);
            }
        }

        [HarmonyPatch(typeof(SE_Shielded), "OnDamaged")]
        class DamagePatch
        {
            static void Prefix()
            {
                Player localPlayer = Player.m_localPlayer;
                Vector3 vector = localPlayer.transform.position;
                PlayEffect("fx_GoblinShieldHit", vector);
                //PlayEffect("fx_guardstone_permitted_removed", vector);
            }
        }

/*        [HarmonyPatch(typeof(SE_Shielded), "IsDone")]
        class BreakPatch
        {
            static void Prefix()
            {
                Player localPlayer = Player.m_localPlayer;
                Vector3 vector = localPlayer.transform.position;
                PlayEffect("fx_guardstone_permitted_remove", vector);
            }
        }*/
    }
}
