// Decompiled with JetBrains decompiler
// Type: AI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public static class AI
{
  public static bool DoAI(NPC _npc, Hero[] _teamHero, NPC[] _teamNPC)
  {
    if ((UnityEngine.Object) _npc.NPCItem == (UnityEngine.Object) null || _npc.HpCurrent <= 0 || MatchManager.Instance.CheckMatchIsOver())
      return false;
    List<AICards> aiCardsList = new List<AICards>();
    AICards aiCards1 = (AICards) null;
    int length1 = _npc.NpcData.AICards.Length;
    for (int index1 = 0; index1 < length1; ++index1)
    {
      AICards aiCards2 = (AICards) null;
      int num = 1000;
      for (int index2 = 0; index2 < length1; ++index2)
      {
        if (_npc.NpcData.AICards[index2].Priority < num && !aiCardsList.Contains(_npc.NpcData.AICards[index2]))
        {
          aiCards2 = _npc.NpcData.AICards[index2];
          num = aiCards2.Priority;
        }
      }
      aiCardsList.Add(aiCards2);
    }
    List<string> stringList = new List<string>();
    for (int position = 0; position < MatchManager.Instance.CountNPCHand(_npc.NPCIndex); ++position)
    {
      string str = MatchManager.Instance.CardFromNPCHand(_npc.NPCIndex, position);
      if (str != "")
      {
        string[] strArray = str.Split('_', StringSplitOptions.None);
        if (strArray != null && strArray[0] != null)
          stringList.Add(strArray[0]);
      }
    }
    bool flag1 = false;
    for (int index3 = 0; index3 < stringList.Count; ++index3)
    {
      for (int index4 = 0; index4 < aiCardsList.Count; ++index4)
      {
        if (aiCardsList[index4] != null && (UnityEngine.Object) aiCardsList[index4].Card != (UnityEngine.Object) null && aiCardsList[index4].Card.Id == stringList[index3])
        {
          flag1 = true;
          break;
        }
      }
      if (!flag1)
        aiCardsList.Add(new AICards()
        {
          Card = Globals.Instance.GetCardData(stringList[index3])
        });
    }
    int energy = _npc.GetEnergy();
    for (int index = aiCardsList.Count - 1; index >= 0; --index)
    {
      if ((UnityEngine.Object) aiCardsList[index].Card == (UnityEngine.Object) null || !_npc.CanPlayCard(aiCardsList[index].Card) || !_npc.CanPlayCardSummon(aiCardsList[index].Card) || _npc.GetCardFinalCost(aiCardsList[index].Card) > energy || !stringList.Contains(aiCardsList[index].Card.Id))
        aiCardsList.RemoveAt(index);
    }
    int count1 = aiCardsList.Count;
    Dictionary<string, List<Hero>> dictionary1 = new Dictionary<string, List<Hero>>();
    Dictionary<string, List<NPC>> dictionary2 = new Dictionary<string, List<NPC>>();
    bool flag2 = false;
    for (int index5 = count1 - 1; index5 >= 0; --index5)
    {
      List<Hero> heroList = new List<Hero>();
      List<NPC> npcList = new List<NPC>();
      AICards aiCards3 = aiCardsList[index5];
      if (aiCards3.Card.TargetType == Enums.CardTargetType.Global)
      {
        if (aiCards3.Card.TargetSide == Enums.CardTargetSide.Enemy || aiCards3.Card.TargetSide == Enums.CardTargetSide.Anyone)
        {
          for (int index6 = 0; index6 < _teamHero.Length; ++index6)
          {
            Hero hero = _teamHero[index6];
            if (hero != null && hero.Alive)
              heroList.Add(hero);
          }
        }
        if (aiCards3.Card.TargetSide == Enums.CardTargetSide.Friend || aiCards3.Card.TargetSide == Enums.CardTargetSide.Anyone)
        {
          for (int index7 = 0; index7 < _teamNPC.Length; ++index7)
          {
            NPC npc = _teamNPC[index7];
            if (npc != null && npc.Alive)
              npcList.Add(_npc);
          }
        }
      }
      else if (aiCards3.Card.TargetSide == Enums.CardTargetSide.Enemy || aiCards3.Card.TargetSide == Enums.CardTargetSide.Anyone)
      {
        for (int index8 = 0; index8 < _teamHero.Length; ++index8)
        {
          Hero hero = _teamHero[index8];
          if (hero != null && hero.Alive && hero.HasEffect("taunt") && !hero.HasEffect("stealth"))
          {
            heroList.Add(hero);
            flag2 = true;
          }
        }
        if (!flag2)
        {
          for (int index9 = 0; index9 < _teamHero.Length; ++index9)
          {
            Hero hero = _teamHero[index9];
            if (hero != null && hero.Alive && !hero.HasEffect("stealth") && (aiCards3.Card.TargetPosition != Enums.CardTargetPosition.Front || MatchManager.Instance.PositionIsFront(true, hero.Position)) && (aiCards3.Card.TargetPosition != Enums.CardTargetPosition.Back || MatchManager.Instance.PositionIsBack((Character) hero)) && (aiCards3.Card.TargetPosition != Enums.CardTargetPosition.Middle || MatchManager.Instance.PositionIsMiddle((Character) hero)))
            {
              bool flag3 = false;
              if (aiCards3.OnlyCastIf == Enums.OnlyCastIf.Always)
                flag3 = true;
              else if (aiCards3.OnlyCastIf == Enums.OnlyCastIf.TargetLifeLessThanPercent)
              {
                float valueCastIf = aiCards3.ValueCastIf;
                if ((double) hero.GetHpPercent() <= (double) valueCastIf)
                  flag3 = true;
              }
              else if (aiCards3.OnlyCastIf == Enums.OnlyCastIf.TargetHasNotAuraCurse)
              {
                string id = aiCards3.AuracurseCastIf.Id;
                if (!hero.HasEffect(id))
                  flag3 = true;
              }
              else if (aiCards3.OnlyCastIf == Enums.OnlyCastIf.TargetLifeHigherThanPercent)
              {
                float valueCastIf = aiCards3.ValueCastIf;
                if ((double) hero.GetHpPercent() >= (double) valueCastIf)
                  flag3 = true;
              }
              else if (aiCards3.OnlyCastIf == Enums.OnlyCastIf.TargetHasAuraCurse)
              {
                string id = aiCards3.AuracurseCastIf.Id;
                if (hero.HasEffect(id))
                  flag3 = true;
              }
              else if (aiCards3.OnlyCastIf == Enums.OnlyCastIf.TargetHasAnyAura)
              {
                if (hero.HasAnyAura())
                  flag3 = true;
              }
              else if (aiCards3.OnlyCastIf == Enums.OnlyCastIf.TargetHasAnyCurse && hero.HasAnyCurse())
                flag3 = true;
              if (flag3)
                heroList.Add(hero);
            }
          }
        }
      }
      else
      {
        for (int index10 = 0; index10 < _teamNPC.Length; ++index10)
        {
          NPC npc = _teamNPC[index10];
          if (npc != null && npc.Alive)
          {
            bool flag4 = true;
            if (aiCards3.Card.TargetSide == Enums.CardTargetSide.Self && _npc.Id != npc.Id)
              flag4 = false;
            else if (aiCards3.Card.TargetSide == Enums.CardTargetSide.FriendNotSelf && _npc.Id == npc.Id)
              flag4 = false;
            if (flag4 && npc.Alive && (npc.Position <= 0 || aiCards3.Card.TargetPosition != Enums.CardTargetPosition.Front))
            {
              bool flag5 = false;
              if (aiCards3.OnlyCastIf == Enums.OnlyCastIf.Always)
                flag5 = true;
              else if (aiCards3.OnlyCastIf == Enums.OnlyCastIf.TargetLifeLessThanPercent)
              {
                float valueCastIf = aiCards3.ValueCastIf;
                if ((double) npc.GetHpPercent() <= (double) valueCastIf)
                  flag5 = true;
              }
              else if (aiCards3.OnlyCastIf == Enums.OnlyCastIf.TargetHasNotAuraCurse)
              {
                string id = aiCards3.AuracurseCastIf.Id;
                if (!npc.HasEffect(id))
                  flag5 = true;
              }
              else if (aiCards3.OnlyCastIf == Enums.OnlyCastIf.TargetLifeHigherThanPercent)
              {
                float valueCastIf = aiCards3.ValueCastIf;
                if ((double) npc.GetHpPercent() >= (double) valueCastIf)
                  flag5 = true;
              }
              else if (aiCards3.OnlyCastIf == Enums.OnlyCastIf.TargetHasAuraCurse)
              {
                string id = aiCards3.AuracurseCastIf.Id;
                if (npc.HasEffect(id))
                  flag5 = true;
              }
              else if (aiCards3.OnlyCastIf == Enums.OnlyCastIf.TargetHasAnyAura)
              {
                if (npc.HasAnyAura())
                  flag5 = true;
              }
              else if (aiCards3.OnlyCastIf == Enums.OnlyCastIf.TargetHasAnyCurse && npc.HasAnyCurse())
                flag5 = true;
              if (flag5)
                npcList.Add(npc);
            }
          }
        }
      }
      int num1;
      if (!flag2 && heroList.Count > 1)
      {
        int num2 = -1;
        float num3 = 1000f;
        float num4 = 10000f;
        float num5 = 0.0f;
        float num6 = 0.0f;
        num1 = 0;
        int num7 = 10;
        int num8 = 100;
        int num9 = -1;
        float num10 = 0.0f;
        float num11 = 10000f;
        int count2 = heroList.Count;
        for (int index11 = 0; index11 < heroList.Count; ++index11)
        {
          if (heroList[index11].Position > num2)
            num2 = heroList[index11].Position;
          if (heroList[index11].Position < num7)
            num7 = heroList[index11].Position;
          int num12 = heroList[index11].GetSpeed()[0];
          if (num12 < num8)
            num8 = num12;
          if (num12 > num9)
            num9 = num12;
          float hp = (float) heroList[index11].GetHp();
          if ((double) hp < (double) num4)
            num4 = hp;
          if ((double) hp > (double) num6)
            num6 = hp;
          float hpPercent = heroList[index11].GetHpPercent();
          if ((double) hpPercent < (double) num3)
            num3 = hpPercent;
          if ((double) hpPercent > (double) num5)
            num5 = hpPercent;
          float num13 = hp + (float) heroList[index11].GetBlock();
          if ((double) num13 < (double) num11)
            num11 = num13;
          if ((double) num13 > (double) num10)
            num10 = num13;
        }
        for (int index12 = heroList.Count - 1; index12 >= 0; --index12)
        {
          Hero hero = heroList[index12];
          if (aiCards3.Card.TargetPosition == Enums.CardTargetPosition.Slowest)
          {
            if (hero.GetSpeed()[0] > num8)
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.Card.TargetPosition == Enums.CardTargetPosition.Fastest)
          {
            if (hero.GetSpeed()[0] < num9)
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.Card.TargetPosition == Enums.CardTargetPosition.LeastHP)
          {
            if ((double) hero.GetHp() > (double) num4)
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.Card.TargetPosition == Enums.CardTargetPosition.MostHP)
          {
            if ((double) hero.GetHp() < (double) num6)
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.Front)
          {
            if (hero.Position > num7)
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.Middle)
          {
            if (count2 > 2 && (hero.Position == num7 || hero.Position == num2))
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.Back)
          {
            if (hero.Position < num2)
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.AnyButFront)
          {
            if (hero.Position == num7)
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.AnyButBack)
          {
            if (hero.Position == num2)
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.LessHealthPercent)
          {
            if ((double) hero.GetHpPercent() > (double) num3)
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.MoreHealthPercent)
          {
            if ((double) hero.GetHpPercent() < (double) num5)
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.LessHealthFlat)
          {
            if ((double) hero.GetHp() > (double) num4)
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.MoreHealthFlat)
          {
            if ((double) hero.GetHp() < (double) num6)
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.LessHealthAbsolute)
          {
            if ((double) (hero.GetHp() + hero.GetBlock()) > (double) num11)
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.MoreHealthAbsolute)
          {
            if ((double) (hero.GetHp() + hero.GetBlock()) < (double) num10)
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.LessInitiative)
          {
            if (hero.GetSpeed()[0] > num8)
              heroList.RemoveAt(index12);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.MoreInitiative && hero.GetSpeed()[0] < num9)
            heroList.RemoveAt(index12);
        }
      }
      if (npcList.Count > 1)
      {
        int num14 = -1;
        float num15 = 1000f;
        float num16 = 10000f;
        float num17 = 0.0f;
        float num18 = 0.0f;
        num1 = 0;
        int num19 = 10;
        int num20 = 100;
        int num21 = -1;
        float num22 = 0.0f;
        float num23 = 10000f;
        int count3 = npcList.Count;
        for (int index13 = 0; index13 < npcList.Count; ++index13)
        {
          if (npcList[index13].Position > num14)
            num14 = npcList[index13].Position;
          if (npcList[index13].Position < num19)
            num19 = npcList[index13].Position;
          int num24 = npcList[index13].GetSpeed()[0];
          if (num24 < num20)
            num20 = num24;
          if (num24 > num21)
            num21 = num24;
          float hp = (float) npcList[index13].GetHp();
          if ((double) hp < (double) num16)
            num16 = hp;
          if ((double) hp > (double) num18)
            num18 = hp;
          float hpPercent = npcList[index13].GetHpPercent();
          if ((double) hpPercent < (double) num15)
            num15 = hpPercent;
          if ((double) hpPercent > (double) num17)
            num17 = hpPercent;
          float num25 = hp + (float) npcList[index13].GetBlock();
          if ((double) num25 < (double) num23)
            num23 = num25;
          if ((double) num25 > (double) num22)
            num22 = num25;
        }
        for (int index14 = npcList.Count - 1; index14 >= 0; --index14)
        {
          NPC npc = npcList[index14];
          if (aiCards3.Card.TargetPosition == Enums.CardTargetPosition.Slowest)
          {
            if (npc.GetSpeed()[0] > num20)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.Card.TargetPosition == Enums.CardTargetPosition.Fastest)
          {
            if (npc.GetSpeed()[0] < num21)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.Card.TargetPosition == Enums.CardTargetPosition.LeastHP)
          {
            if ((double) npc.GetHp() > (double) num16)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.Card.TargetPosition == Enums.CardTargetPosition.MostHP)
          {
            if ((double) npc.GetHp() < (double) num18)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.Front)
          {
            if (npc.Position > num19)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.Middle)
          {
            if (count3 > 2 && (npc.Position == num19 || npc.Position == num14))
              npcList.RemoveAt(index14);
            else if (count3 == 2 && npc.Position == num19)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.Back)
          {
            if (npc.Position < num14)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.AnyButFront)
          {
            if (npc.Position == num19)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.AnyButBack)
          {
            if (npc.Position == num14)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.LessHealthPercent)
          {
            if ((double) npc.GetHpPercent() > (double) num15)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.MoreHealthPercent)
          {
            if ((double) npc.GetHpPercent() < (double) num17)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.LessHealthFlat)
          {
            if ((double) npc.GetHp() > (double) num16)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.MoreHealthFlat)
          {
            if ((double) npc.GetHp() < (double) num18)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.LessHealthAbsolute)
          {
            if ((double) (npc.GetHp() + npc.GetBlock()) > (double) num23)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.MoreHealthAbsolute)
          {
            if ((double) (npc.GetHp() + npc.GetBlock()) < (double) num22)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.LessInitiative)
          {
            if (npc.GetSpeed()[0] > num20)
              npcList.RemoveAt(index14);
          }
          else if (aiCards3.TargetCast == Enums.TargetCast.MoreInitiative && npc.GetSpeed()[0] < num21)
            npcList.RemoveAt(index14);
        }
      }
      if (heroList.Count == 0 && npcList.Count == 0)
      {
        aiCardsList.RemoveAt(index5);
      }
      else
      {
        if (!dictionary1.ContainsKey(aiCards3.Card.Id))
          dictionary1.Add(aiCards3.Card.Id, heroList);
        if (!dictionary2.ContainsKey(aiCards3.Card.Id))
          dictionary2.Add(aiCards3.Card.Id, npcList);
      }
    }
    string key = "";
    int index15 = -1;
    if (aiCardsList.Count > 0)
    {
      int length2 = 1;
      int priority = aiCardsList[0].Priority;
      for (int index16 = 1; index16 < aiCardsList.Count && aiCardsList[index16].Priority == priority; ++index16)
        ++length2;
      if (length2 > 1)
      {
        float[] numArray = new float[length2];
        float num26 = 0.0f;
        for (int index17 = 0; index17 < length2; ++index17)
          num26 += aiCardsList[index17].PercentToCast;
        for (int index18 = 0; index18 < length2; ++index18)
          numArray[index18] = aiCardsList[index18].PercentToCast * 100f / num26;
        float num27 = 0.0f;
        aiCards1 = (AICards) null;
        int randomIntRange = MatchManager.Instance.GetRandomIntRange(1, 101);
        for (int index19 = 0; index19 < length2; ++index19)
        {
          num27 += numArray[index19];
          if ((double) randomIntRange <= (double) num27)
          {
            aiCards1 = aiCardsList[index19];
            break;
          }
        }
      }
      if (aiCards1 == null)
        aiCards1 = aiCardsList[0];
      key = aiCards1.Card.Id;
      for (int position = 0; position < MatchManager.Instance.CountNPCHand(_npc.NPCIndex); ++position)
      {
        if (MatchManager.Instance.CardFromNPCHand(_npc.NPCIndex, position) != null && MatchManager.Instance.CardFromNPCHand(_npc.NPCIndex, position).Split(char.Parse("_"), StringSplitOptions.None)[0] == key)
        {
          index15 = position;
          break;
        }
      }
    }
    if (index15 <= -1)
      return false;
    for (int index20 = 0; index20 < _npc.NPCItem.cardsCI.Length; ++index20)
    {
      if ((UnityEngine.Object) _npc.NPCItem.cardsCI[index20] != (UnityEngine.Object) null && (UnityEngine.Object) _npc.NPCItem.cardsCI[index20].CardData != (UnityEngine.Object) null && _npc.NPCItem.cardsCI[index20].CardData.Id == key && (UnityEngine.Object) _npc.NPCItem.cardsCI[index20] != (UnityEngine.Object) null && _npc.NPCItem.cardsCI[index20].IsRevealed())
      {
        index15 = index20;
        break;
      }
    }
    Transform transform1 = _npc.NPCItem.transform;
    if ((UnityEngine.Object) _npc.NPCItem == (UnityEngine.Object) null || _npc.NPCItem.cardsCI == null || index15 >= _npc.NPCItem.cardsCI.Length || (UnityEngine.Object) _npc.NPCItem.cardsCI[index15] == (UnityEngine.Object) null)
      return false;
    CardData cardData = _npc.NPCItem.cardsCI[index15].CardData;
    Transform transform2;
    if (dictionary1[key].Count == 0)
    {
      int randomIntRange = MatchManager.Instance.GetRandomIntRange(0, dictionary2[key].Count);
      transform2 = dictionary2[key][randomIntRange].NPCItem.transform;
    }
    else
    {
      int randomIntRange = MatchManager.Instance.GetRandomIntRange(0, dictionary1[key].Count);
      Hero hero = dictionary1[key][randomIntRange];
      if (hero == null || !((UnityEngine.Object) hero.HeroItem != (UnityEngine.Object) null))
        return false;
      transform2 = hero.HeroItem.transform;
    }
    if (!((UnityEngine.Object) _npc.NPCItem.cardsCI[index15] != (UnityEngine.Object) null) || !_npc.NPCItem.cardsCI[index15].gameObject.activeSelf)
      return false;
    _npc.CastCardNPC(index15, transform2);
    MatchManager.Instance.CastAutomatic(cardData, transform1, transform2);
    MatchManager.Instance.NPCDiscard(_npc.NPCIndex, index15, true);
    return true;
  }
}
