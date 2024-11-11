// Decompiled with JetBrains decompiler
// Type: ChallengeData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New ChallengeData", menuName = "New ChallengeData", order = 64)]
public class ChallengeData : ScriptableObject
{
  [SerializeField]
  private string id;
  [SerializeField]
  private string idSteam;
  [SerializeField]
  private int week;
  [SerializeField]
  private string seed;
  [Header("Weekly dates")]
  [Tooltip("dd-mm-yyyy")]
  [SerializeField]
  private string fromDay;
  [Tooltip("hh:mm")]
  [SerializeField]
  private string fromHour;
  [Tooltip("dd-mm-yyyy")]
  [SerializeField]
  private string toDay;
  [Tooltip("hh:mm")]
  [SerializeField]
  private string toHour;
  [Header("Heroes")]
  [SerializeField]
  private SubClassData hero1;
  [SerializeField]
  private SubClassData hero2;
  [SerializeField]
  private SubClassData hero3;
  [SerializeField]
  private SubClassData hero4;
  [Header("Boss")]
  [SerializeField]
  private NPCData boss1;
  [SerializeField]
  private NPCData boss2;
  [SerializeField]
  private CombatData bossCombat;
  [Header("Loot")]
  [SerializeField]
  private LootData loot;
  [Header("Traits")]
  [SerializeField]
  private List<ChallengeTrait> traits;
  [Header("Corruptions")]
  [SerializeField]
  private List<CardData> corruptionList;

  public string Id
  {
    get => this.id;
    set => this.id = value;
  }

  public string IdSteam
  {
    get => this.idSteam;
    set => this.idSteam = value;
  }

  public int Week
  {
    get => this.week;
    set => this.week = value;
  }

  public string Seed
  {
    get => this.seed;
    set => this.seed = value;
  }

  public SubClassData Hero1
  {
    get => this.hero1;
    set => this.hero1 = value;
  }

  public SubClassData Hero2
  {
    get => this.hero2;
    set => this.hero2 = value;
  }

  public SubClassData Hero3
  {
    get => this.hero3;
    set => this.hero3 = value;
  }

  public SubClassData Hero4
  {
    get => this.hero4;
    set => this.hero4 = value;
  }

  public List<ChallengeTrait> Traits
  {
    get => this.traits;
    set => this.traits = value;
  }

  public LootData Loot
  {
    get => this.loot;
    set => this.loot = value;
  }

  public List<CardData> CorruptionList
  {
    get => this.corruptionList;
    set => this.corruptionList = value;
  }

  public NPCData Boss1
  {
    get => this.boss1;
    set => this.boss1 = value;
  }

  public NPCData Boss2
  {
    get => this.boss2;
    set => this.boss2 = value;
  }

  public CombatData BossCombat
  {
    get => this.bossCombat;
    set => this.bossCombat = value;
  }

  public DateTime GetDateFrom()
  {
    if (this.fromDay == "" || this.fromHour == "")
      return DateTime.Now.AddYears(1);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(this.fromDay);
    stringBuilder.Append(" ");
    stringBuilder.Append(this.fromHour);
    return DateTime.ParseExact(stringBuilder.ToString(), "dd-MM-yyyy HH:mm", (IFormatProvider) null);
  }

  public DateTime GetDateTo()
  {
    if (this.toDay == "" || this.toHour == "")
      return DateTime.Now.AddYears(-1);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(this.toDay);
    stringBuilder.Append(" ");
    stringBuilder.Append(this.toHour);
    return DateTime.ParseExact(stringBuilder.ToString(), "dd-MM-yyyy HH:mm", (IFormatProvider) null);
  }

  public CardbackData GetCardbackData()
  {
    CardbackData cardbackData = (CardbackData) null;
    if (this.idSteam != "")
    {
      foreach (KeyValuePair<string, CardbackData> keyValuePair in Globals.Instance.CardbackDataSource)
      {
        if (keyValuePair.Value.SteamStat == this.idSteam)
        {
          cardbackData = keyValuePair.Value;
          break;
        }
      }
    }
    return cardbackData;
  }
}
