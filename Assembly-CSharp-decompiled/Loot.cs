// Decompiled with JetBrains decompiler
// Type: Loot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public static class Loot
{
  public static List<string> GetLootItems(string _itemListId, string _idAux = "")
  {
    if (AtOManager.Instance.GetGameId() == "")
      return (List<string>) null;
    LootData lootData = Globals.Instance.GetLootData(_itemListId);
    if ((Object) lootData == (Object) null)
      return (List<string>) null;
    List<string> ts1 = new List<string>();
    List<string> ts2 = new List<string>();
    for (int index = 0; index < Globals.Instance.CardListByClass[Enums.CardClass.Item].Count; ++index)
      ts2.Add(Globals.Instance.CardListByClass[Enums.CardClass.Item][index]);
    int deterministicHashCode = (AtOManager.Instance.currentMapNode + AtOManager.Instance.GetGameId() + _idAux).GetDeterministicHashCode();
    Random.InitState(deterministicHashCode);
    ts2.Shuffle<string>(deterministicHashCode);
    int index1 = 0;
    int num1 = !GameManager.Instance.IsObeliskChallenge() ? lootData.NumItems : lootData.LootItemTable.Length;
    for (int index2 = 0; index2 < num1 && ts1.Count < lootData.NumItems; ++index2)
    {
      if (index2 < lootData.LootItemTable.Length)
      {
        LootItem lootItem = lootData.LootItemTable[index2];
        if ((double) Random.Range(0, 100) < (double) lootItem.LootPercent)
        {
          if ((Object) lootItem.LootCard != (Object) null)
          {
            ts1.Add(lootItem.LootCard.Id);
          }
          else
          {
            bool flag = false;
            int num2 = 0;
            CardData cardData = (CardData) null;
            for (; !flag && num2 < 10000; ++num2)
            {
              if (index1 >= ts2.Count)
                index1 = 0;
              string str = ts2[index1];
              if (!ts1.Contains(str) && (!AtOManager.Instance.ItemBoughtOnThisShop(_itemListId, str) && AtOManager.Instance.HowManyTownRerolls() > 0 || AtOManager.Instance.HowManyTownRerolls() == 0))
              {
                cardData = Globals.Instance.GetCardData(str, false);
                if ((Object) cardData.Item != (Object) null && !cardData.Item.DropOnly)
                {
                  if (cardData.CardUpgraded == Enums.CardUpgraded.Rare)
                    flag = false;
                  else if (cardData.Item.PercentRetentionEndGame > 0 && (AtOManager.Instance.GetNgPlus() > 2 || GameManager.Instance.IsObeliskChallenge()))
                    flag = false;
                  else if (cardData.Item.PercentDiscountShop > 0 && (MadnessManager.Instance.IsMadnessTraitActive("poverty") || AtOManager.Instance.IsChallengeTraitActive("poverty")))
                    flag = false;
                  else if (lootItem.LootType == Enums.CardType.None && cardData.CardRarity == lootItem.LootRarity)
                    flag = true;
                  else if (cardData.CardType == lootItem.LootType && cardData.CardRarity == lootItem.LootRarity)
                    flag = true;
                }
              }
              ++index1;
            }
            if (flag && (Object) cardData != (Object) null)
              ts1.Add(cardData.Id);
          }
        }
      }
    }
    for (int count = ts1.Count; count < lootData.NumItems; ++count)
    {
      bool flag = false;
      int num3 = 0;
      CardData cardData = (CardData) null;
      int num4 = Random.Range(0, 100);
      while (!flag && num3 < 10000)
      {
        if (index1 >= ts2.Count)
          index1 = 0;
        string str = ts2[index1];
        if (!ts1.Contains(str) && (!AtOManager.Instance.ItemBoughtOnThisShop(_itemListId, str) && AtOManager.Instance.HowManyTownRerolls() > 0 || AtOManager.Instance.HowManyTownRerolls() == 0))
        {
          cardData = Globals.Instance.GetCardData(str, false);
          if ((Object) cardData.Item != (Object) null && !cardData.Item.DropOnly)
          {
            if (cardData.CardUpgraded == Enums.CardUpgraded.Rare)
              flag = false;
            else if (cardData.Item.PercentRetentionEndGame > 0 && (AtOManager.Instance.GetNgPlus() > 2 || GameManager.Instance.IsObeliskChallenge()))
              flag = false;
            else if (cardData.Item.PercentDiscountShop > 0 && (MadnessManager.Instance.IsMadnessTraitActive("poverty") || AtOManager.Instance.IsChallengeTraitActive("poverty")))
              flag = false;
            else if ((double) num4 < (double) lootData.DefaultPercentMythic)
            {
              if (cardData.CardRarity == Enums.CardRarity.Mythic)
                flag = true;
            }
            else if ((double) num4 < (double) lootData.DefaultPercentEpic + (double) lootData.DefaultPercentMythic)
            {
              if (cardData.CardRarity == Enums.CardRarity.Epic)
                flag = true;
            }
            else if ((double) num4 < (double) lootData.DefaultPercentRare + (double) lootData.DefaultPercentEpic + (double) lootData.DefaultPercentMythic)
            {
              if (cardData.CardRarity == Enums.CardRarity.Rare)
                flag = true;
            }
            else if ((double) num4 < (double) lootData.DefaultPercentUncommon + (double) lootData.DefaultPercentRare + (double) lootData.DefaultPercentEpic + (double) lootData.DefaultPercentMythic)
            {
              if (cardData.CardRarity == Enums.CardRarity.Uncommon)
                flag = true;
            }
            else if (cardData.CardRarity == Enums.CardRarity.Common)
              flag = true;
          }
        }
        ++index1;
        ++num3;
        if (!flag && num3 % 100 == 0)
          num4 += 10;
      }
      if (flag && (Object) cardData != (Object) null)
        ts1.Add(cardData.Id);
      else
        break;
    }
    ts1.Shuffle<string>(deterministicHashCode);
    if (!AtOManager.Instance.CharInTown() && (!GameManager.Instance.IsObeliskChallenge() && AtOManager.Instance.GetMadnessDifficulty() > 0 || GameManager.Instance.IsObeliskChallenge()))
    {
      int num5 = 0;
      if (AtOManager.Instance.corruptionId == "exoticshop")
        num5 += 8;
      else if (AtOManager.Instance.corruptionId == "rareshop")
        num5 += 4;
      if (GameManager.Instance.IsObeliskChallenge())
      {
        if (AtOManager.Instance.GetObeliskMadness() > 8)
          num5 += 4;
        else if (AtOManager.Instance.GetObeliskMadness() > 4)
          num5 += 2;
      }
      else
        num5 += Functions.FuncRoundToInt(0.2f * (float) AtOManager.Instance.GetMadnessDifficulty());
      for (int index3 = 0; index3 < ts1.Count; ++index3)
      {
        int num6 = Random.Range(0, 100);
        CardData cardData = Globals.Instance.GetCardData(ts1[index3], false);
        if (!((Object) cardData == (Object) null))
        {
          bool flag = false;
          if ((cardData.CardRarity == Enums.CardRarity.Mythic || cardData.CardRarity == Enums.CardRarity.Epic) && num6 < 3 + num5)
            flag = true;
          else if (cardData.CardRarity == Enums.CardRarity.Rare && num6 < 5 + num5)
            flag = true;
          else if (cardData.CardRarity == Enums.CardRarity.Uncommon && num6 < 7 + num5)
            flag = true;
          else if (cardData.CardRarity == Enums.CardRarity.Common && num6 < 9 + num5)
            flag = true;
          if (flag && (Object) cardData.UpgradesToRare != (Object) null)
            ts1[index3] = cardData.UpgradesToRare.Id;
        }
      }
    }
    if (GameManager.Instance.IsTutorialGame() && ts1 != null && ts1.Count > 0)
      ts1[ts1.Count - 1] = "spyglass";
    return ts1;
  }
}
