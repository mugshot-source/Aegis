using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using UnityEngine;
using JotunnLib;
using JotunnLib.Entities;
using JotunnLib.Managers;
using JotunnLib.Events;
using System;

namespace Aegis
{
    [BepInPlugin("Aegis", "MagicShield", "1.0.0")]
    [BepInProcess("valheim.exe")]
    public class Aegis : PrefabConfig
    {
        public Aegis() : base("Aegis", "ShieldKnight")
        {

        }

        public override void Register()
        {

            ItemDrop item = Prefab.GetComponent<ItemDrop>();
            item.m_itemData.m_shared.m_name = "Aegis";
            item.m_itemData.m_shared.m_description = "A powerful shield encrusted with crystal, and imbued with fuling magic.";
            item.m_itemData.m_shared.m_blockPower = 40;
            item.m_itemData.m_shared.m_blockPowerPerLevel = 10;
            item.m_itemData.m_shared.m_timedBlockBonus = 1.1f;
            item.m_itemData.m_shared.m_equipDuration = 1;
            item.m_itemData.m_shared.m_weight = 5;
            item.m_itemData.m_shared.m_maxDurability = 1000;
            item.m_itemData.m_shared.m_durabilityPerLevel = 100;
            item.m_itemData.m_shared.m_deflectionForce = 10000;
            item.m_itemData.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Shield;
            item.m_itemData.m_shared.m_maxQuality = 1;
            item.m_itemData.m_shared.m_movementModifier = -0.2f;
            item.m_itemData.m_shared.m_variants = 0;

            //item.m_itemData.m_shared.m_icons = new Sprite[] { AssetHelper.Icon };

            var shielded = ScriptableObject.CreateInstance<SE_Shielded>();
            //shielded.m_icon = AssetHelper.Icon;

            /*            var shieldValue = AccessTools.Field(typeof(SE_Shielded), "m_absorbDamage");

                            float shieldHP = 300f;

                            shieldValue.SetValue(shielded, shieldHP);*/

            item.m_itemData.m_shared.m_equipStatusEffect = shielded;
        }
    }
}
