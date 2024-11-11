// Decompiled with JetBrains decompiler
// Type: GameTurnData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;

[Serializable]
public class GameTurnData
{
  private string turnId = "";
  private string turnCombatData = "";
  private string turnData = "";
  private string turnCombatDictionaryKeys = "";
  private string turnCombatDictionaryValues = "";
  private string turnHeroItems = "";
  private string turnCombatStatsEffects = "";
  private string turnCombatStatsCurrent = "";
  private string turnHeroLifeArr = "";

  public void FillData()
  {
    this.turnId = AtOManager.Instance.GetGameId() + "_" + AtOManager.Instance.mapVisitedNodes.Count.ToString() + "_" + AtOManager.Instance.currentMapNode;
    CombatData currentCombatData = AtOManager.Instance.GetCurrentCombatData();
    this.turnCombatData = !((UnityEngine.Object) currentCombatData == (UnityEngine.Object) null) ? currentCombatData.CombatId : "";
    this.turnData = MatchManager.Instance.CurrentGameCodeForReload;
    this.turnCombatDictionaryKeys = MatchManager.Instance.GetCardDictionaryKeys();
    this.turnCombatDictionaryValues = MatchManager.Instance.GetCardDictionaryValues();
    this.turnHeroItems = MatchManager.Instance.GetHeroItemsForTurnSave();
    this.turnCombatStatsEffects = MatchManager.Instance.GetCombatStatsForTurnSave();
    this.turnCombatStatsCurrent = MatchManager.Instance.GetCombatStatsCurrentForTurnSave();
    this.turnHeroLifeArr = MatchManager.Instance.GetHeroLifeArrForTurnSave();
  }

  public void LoadData()
  {
    if (AtOManager.Instance.GetGameId() + "_" + AtOManager.Instance.mapVisitedNodes.Count.ToString() + "_" + AtOManager.Instance.currentMapNode != this.turnId)
      return;
    CombatData currentCombatData = AtOManager.Instance.GetCurrentCombatData();
    if ((UnityEngine.Object) currentCombatData == (UnityEngine.Object) null && this.turnCombatData != "" || (UnityEngine.Object) currentCombatData != (UnityEngine.Object) null && currentCombatData.CombatId != this.turnCombatData)
      return;
    MatchManager.Instance.SetLoadTurn(this.turnData, this.turnCombatDictionaryKeys, this.turnCombatDictionaryValues, this.turnHeroItems, this.turnCombatStatsEffects, this.turnCombatStatsCurrent, this.turnHeroLifeArr);
  }
}
