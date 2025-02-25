using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
// using Obeliskial_Content;
// using Obeliskial_Essentials;
using System.IO;
using static UnityEngine.Mathf;
using UnityEngine.TextCore.LowLevel;
using static ChaoticCorruptions.Plugin;
using System.Collections.ObjectModel;

namespace ChaoticCorruptions
{
    public class ChaoticCorruptionsFunctions
    {

        public static bool CanCraftRarity(CardCraftManager __instance, CardData cardData)
        {
            CardData cData = cardData;
            cData = Functions.GetCardDataFromCardData(cData, "");
            Enums.CardRarity maxCraftRarity = Traverse.Create(__instance).Field("maxCraftRarity")?.GetValue<Enums.CardRarity>() ?? Enums.CardRarity.Epic;
            if ((bool)(UnityEngine.Object)MapManager.Instance && GameManager.Instance.IsObeliskChallenge())            
                return maxCraftRarity == Enums.CardRarity.Mythic || maxCraftRarity == Enums.CardRarity.Epic && cData.CardRarity != Enums.CardRarity.Mythic || maxCraftRarity == Enums.CardRarity.Rare && cData.CardRarity != Enums.CardRarity.Mythic && cData.CardRarity != Enums.CardRarity.Epic || maxCraftRarity == Enums.CardRarity.Uncommon && cData.CardRarity != Enums.CardRarity.Mythic && cData.CardRarity != Enums.CardRarity.Epic && cData.CardRarity != Enums.CardRarity.Rare || maxCraftRarity == Enums.CardRarity.Common && cData.CardRarity == Enums.CardRarity.Common;
            if (AtOManager.Instance.Sandbox_allRarities)
                return true;
            if (cData.CardRarity == Enums.CardRarity.Mythic)
                return false;
            if (AtOManager.Instance.GetTownTier() == 0)
            {
                if (cData.CardRarity == Enums.CardRarity.Rare && (!PlayerManager.Instance.PlayerHaveSupply("townUpgrade_1_4") || AtOManager.Instance.GetNgPlus() >= 8) || cData.CardRarity == Enums.CardRarity.Epic || cData.CardRarity == Enums.CardRarity.Mythic)
                    return false;
            }
            else if (AtOManager.Instance.GetTownTier() == 1 && cData.CardRarity == Enums.CardRarity.Epic && (!PlayerManager.Instance.PlayerHaveSupply("townUpgrade_1_6") || AtOManager.Instance.GetNgPlus() >= 8))
                return false;
            return true;
        }

        public static CardData GetRandomCardWeighted(Hero hero, bool craftableOnly = true)
        {
            int madness = AtOManager.Instance?.GetNgPlus() ?? 0;
            int commonChance = craftableOnly ? (madness < 5 ? 10 : 35) : 30;
            // int commonChance = craftableOnly ? 70: 35;
            int uncommonChance = craftableOnly ? (madness < 5 ? 65 : 60) : 35;
            int rareChance = craftableOnly ? (madness < 5 ? 20 : 5) : 20;
            int epicChance = craftableOnly ? (madness < 5 ? 5 : 0) : 10;
            // int mythicChance = craftableOnly ? (madness < 5 ? 0 : 0) : 5;

            Enums.CardClass result1 = Enums.CardClass.None;
            Enum.TryParse<Enums.CardClass>(Enum.GetName(typeof(Enums.HeroClass), (object)hero.HeroData.HeroClass), out result1);
            Enums.CardClass result2 = Enums.CardClass.None;
            Enum.TryParse<Enums.CardClass>(Enum.GetName(typeof(Enums.HeroClass), (object)hero.HeroData.HeroSubClass.HeroClassSecondary), out result2);
            List<string> stringList1 = Globals.Instance.CardListNotUpgradedByClass[result1];
            List<string> stringList2 = result2 == Enums.CardClass.None ? new List<string>() : Globals.Instance.CardListNotUpgradedByClass[result2];
            int index1 = UnityEngine.Random.Range(0, 2);
            int num10 = UnityEngine.Random.Range(0, 100);
            CardData _cardData = Globals.Instance.GetCardData(index1 < 1 || result2 == Enums.CardClass.None ? stringList1[UnityEngine.Random.Range(0, stringList1.Count)] : stringList2[UnityEngine.Random.Range(0, stringList2.Count)], false);
            LogDebug($"Randomizing card: {_cardData.Id}");

            bool flag2 = true;
            while (flag2)
            {
                flag2 = false;
                bool flag3 = false;
                while (!flag3)
                {
                    flag2 = false;
                    _cardData = Globals.Instance.GetCardData(index1 < 1 || result2 == Enums.CardClass.None ? stringList1[UnityEngine.Random.Range(0, stringList1.Count)] : stringList2[UnityEngine.Random.Range(0, stringList2.Count)], false);
                    if (!flag2)
                    {
                        if (num10 < commonChance)
                        {
                            if (_cardData.CardRarity == Enums.CardRarity.Common)
                                flag3 = true;
                        }
                        else if (num10 < commonChance + uncommonChance)
                        {
                            if (_cardData.CardRarity == Enums.CardRarity.Uncommon)
                                flag3 = true;
                        }
                        else if (num10 < commonChance + uncommonChance + rareChance)
                        {
                            if (_cardData.CardRarity == Enums.CardRarity.Rare)
                                flag3 = true;
                        }
                        else if (num10 < commonChance + uncommonChance + rareChance + epicChance)
                        {
                            if (_cardData.CardRarity == Enums.CardRarity.Epic)
                                flag3 = true;
                        }
                        else if (_cardData.CardRarity == Enums.CardRarity.Mythic)
                            flag3 = true;
                    }
                }
            }

            return _cardData;
        }
    }
}

