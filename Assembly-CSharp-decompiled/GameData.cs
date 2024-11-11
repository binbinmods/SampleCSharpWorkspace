// Decompiled with JetBrains decompiler
// Type: GameData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
  private string version = "";
  private string steamUserId;
  private string steamUserIdOriginal = "";
  private string gameId;
  private string gameDate;
  private string gameNick = "";
  private int weekly;
  private Enums.GameMode gameMode;
  private Enums.GameType gameType;
  private int ngPlus;
  private string madnessCorruptors;
  private int obeliskMadness;
  private int playerDust;
  private int playerGold;
  private string teamAtO;
  private string teamAtOBackup;
  private int divinationsNumber;
  private bool inTown;
  private int townTier;
  private string townZoneId;
  private int combatExpertise;
  private int combatTotal;
  private int bossesKilled;
  private List<string> bossesKilledName;
  private int monstersKilled;
  private string currentCombatData;
  private string fromEventCombatData;
  private string fromEventEventData;
  private string currentMapNode;
  private Dictionary<string, string> gameNodeAssigned;
  private Dictionary<string, int> gameNodeAssignedOptions;
  private Dictionary<string, List<string>> heroPerks;
  private List<string> mapVisitedNodes;
  private List<string> mapVisitedNodesAction;
  private List<string> playerRequeriments;
  private Dictionary<string, int> mpPlayersGold;
  private Dictionary<string, int> mpPlayersDust;
  private int totalGoldGained;
  private int totalDustGained;
  private List<string> unlockedCards;
  private List<Dictionary<string, int>> craftedCards;
  private Dictionary<string, List<string>> boughtItems;
  private Dictionary<string, int> boughtItemInShopByWho;
  private Dictionary<string, string> playerNickRealDict;
  private List<string> challengeTraits;
  private string owner0 = "";
  private string owner1 = "";
  private string owner2 = "";
  private string owner3 = "";
  private string owner0Original = "";
  private string owner1Original = "";
  private string owner2Original = "";
  private string owner3Original = "";
  private List<int> lootCharacterOrder;
  private List<int> conflictCharacterOrder;
  private Dictionary<string, string> townRerollList;
  private float playedTime;
  private int[,] combatStats;
  private string playerPositionList;
  private int gameHandicap;
  private bool corruptionAccepted;
  private int corruptionType;
  private string corruptionId;
  private string corruptionIdCard;
  private string corruptionRewardCard;
  private int corruptionRewardChar;
  private int corruptionCommonCompleted;
  private int corruptionUncommonCompleted;
  private int corruptionRareCompleted;
  private int corruptionEpicCompleted;
  private int townTutorialStep = -1;
  private string sandboxMods = "";

  public void FillData(bool newGame)
  {
    this.version = GameManager.Instance.gameVersion;
    this.steamUserId = !GameManager.Instance.GetDeveloperMode() ? SteamManager.Instance.steamId.ToString() : (string) null;
    if (this.steamUserIdOriginal == "")
      this.steamUserIdOriginal = this.steamUserId;
    this.gameId = AtOManager.Instance.GetGameId();
    this.gameDate = DateTime.Now.ToString();
    if (this.gameNick == "")
      this.gameNick = PlayerManager.Instance.GetPlayerName();
    this.gameMode = GameManager.Instance.GameMode;
    this.gameType = GameManager.Instance.GameType;
    this.ngPlus = AtOManager.Instance.GetNgPlus(true);
    this.madnessCorruptors = AtOManager.Instance.GetMadnessCorruptors();
    this.obeliskMadness = AtOManager.Instance.GetObeliskMadness();
    this.weekly = AtOManager.Instance.GetWeekly();
    this.playerDust = AtOManager.Instance.GetPlayerDust();
    this.playerGold = AtOManager.Instance.GetPlayerGold();
    AtOManager.Instance.DistributeGoldDustBetweenHeroes();
    Hero[] team = AtOManager.Instance.GetTeam();
    this.teamAtO = JsonHelper.ToJson<Hero>(team);
    this.teamAtOBackup = JsonHelper.ToJson<Hero>(AtOManager.Instance.GetTeamBackup());
    this.divinationsNumber = AtOManager.Instance.divinationsNumber;
    this.inTown = AtOManager.Instance.CharInTown();
    this.townTier = AtOManager.Instance.GetTownTier();
    this.townZoneId = AtOManager.Instance.GetTownZoneId();
    this.combatExpertise = AtOManager.Instance.combatExpertise;
    this.combatTotal = AtOManager.Instance.combatTotal;
    this.bossesKilled = AtOManager.Instance.bossesKilled;
    this.bossesKilledName = AtOManager.Instance.bossesKilledName;
    this.monstersKilled = AtOManager.Instance.monstersKilled;
    CombatData currentCombatData = AtOManager.Instance.GetCurrentCombatData();
    this.currentCombatData = !((UnityEngine.Object) currentCombatData != (UnityEngine.Object) null) ? "" : currentCombatData.CombatId;
    this.fromEventCombatData = !((UnityEngine.Object) AtOManager.Instance.fromEventCombatData != (UnityEngine.Object) null) ? "" : AtOManager.Instance.fromEventCombatData.CombatId;
    this.fromEventEventData = !((UnityEngine.Object) AtOManager.Instance.fromEventEventData != (UnityEngine.Object) null) ? "" : AtOManager.Instance.fromEventEventData.EventId;
    this.currentMapNode = AtOManager.Instance.currentMapNode;
    this.mapVisitedNodes = AtOManager.Instance.mapVisitedNodes;
    this.mapVisitedNodesAction = AtOManager.Instance.mapVisitedNodesAction;
    this.gameNodeAssigned = AtOManager.Instance.gameNodeAssigned;
    this.playerRequeriments = AtOManager.Instance.GetPlayerRequeriments();
    this.mpPlayersGold = AtOManager.Instance.GetMpPlayersGold();
    this.mpPlayersDust = AtOManager.Instance.GetMpPlayersDust();
    this.totalGoldGained = AtOManager.Instance.totalGoldGained;
    this.totalDustGained = AtOManager.Instance.totalDustGained;
    this.unlockedCards = AtOManager.Instance.unlockedCards;
    this.craftedCards = AtOManager.Instance.craftedCards;
    this.boughtItems = AtOManager.Instance.boughtItems;
    this.heroPerks = AtOManager.Instance.heroPerks;
    this.boughtItemInShopByWho = AtOManager.Instance.boughtItemInShopByWho;
    this.lootCharacterOrder = AtOManager.Instance.lootCharacterOrder;
    this.conflictCharacterOrder = AtOManager.Instance.conflictCharacterOrder;
    this.combatStats = AtOManager.Instance.combatStats;
    this.townRerollList = AtOManager.Instance.townRerollList;
    if (!GameManager.Instance.IsMultiplayer() && this.townRerollList != null)
    {
      using (Dictionary<string, string>.Enumerator enumerator = this.townRerollList.GetEnumerator())
      {
        if (enumerator.MoveNext())
          AtOManager.Instance.shopItemReroll = enumerator.Current.Value;
      }
    }
    if (GameManager.Instance.IsMultiplayer())
    {
      this.playerNickRealDict = NetworkManager.Instance.PlayerNickRealDict;
      if (team[0] != null && (UnityEngine.Object) team[0].HeroData != (UnityEngine.Object) null && this.playerNickRealDict.ContainsKey(team[0].Owner))
        this.owner0 = this.playerNickRealDict[team[0].Owner];
      if (team[1] != null && (UnityEngine.Object) team[1].HeroData != (UnityEngine.Object) null && this.playerNickRealDict.ContainsKey(team[1].Owner))
        this.owner1 = this.playerNickRealDict[team[1].Owner];
      if (team[2] != null && (UnityEngine.Object) team[2].HeroData != (UnityEngine.Object) null && this.playerNickRealDict.ContainsKey(team[2].Owner))
        this.owner2 = this.playerNickRealDict[team[2].Owner];
      if (team[3] != null && (UnityEngine.Object) team[3].HeroData != (UnityEngine.Object) null && this.playerNickRealDict.ContainsKey(team[3].Owner))
        this.owner3 = this.playerNickRealDict[team[3].Owner];
      if (this.owner0Original == "" && this.owner0 != "")
        this.owner0Original = this.owner0;
      if (this.owner1Original == "" && this.owner1 != "")
        this.owner1Original = this.owner0;
      if (this.owner2Original == "" && this.owner2 != "")
        this.owner2Original = this.owner0;
      if (this.owner3Original == "" && this.owner3 != "")
        this.owner3Original = this.owner0;
    }
    this.playedTime = AtOManager.Instance.playedTime;
    this.corruptionAccepted = AtOManager.Instance.corruptionAccepted;
    this.corruptionType = AtOManager.Instance.corruptionType;
    this.corruptionId = AtOManager.Instance.corruptionId;
    this.corruptionIdCard = AtOManager.Instance.corruptionIdCard;
    this.corruptionRewardCard = AtOManager.Instance.corruptionRewardCard;
    this.corruptionRewardChar = AtOManager.Instance.corruptionRewardChar;
    this.corruptionCommonCompleted = AtOManager.Instance.corruptionCommonCompleted;
    this.corruptionUncommonCompleted = AtOManager.Instance.corruptionUncommonCompleted;
    this.corruptionRareCompleted = AtOManager.Instance.corruptionRareCompleted;
    this.corruptionEpicCompleted = AtOManager.Instance.corruptionEpicCompleted;
    this.townTutorialStep = AtOManager.Instance.TownTutorialStep;
    this.gameHandicap = AtOManager.Instance.gameHandicap;
    this.sandboxMods = AtOManager.Instance.GetSandboxMods();
    AtOManager.Instance.saveLoadStatus = false;
    Functions.DebugLogGD("SAVING GAME", "save");
  }

  public void LoadData()
  {
    AtOManager.Instance.ClearGame();
    AtOManager.Instance.SetGameId(this.gameId);
    GameManager.Instance.GameMode = this.gameMode;
    GameManager.Instance.GameType = this.gameType;
    GameManager.Instance.SetGameStatus(Enums.GameStatus.LoadGame);
    AtOManager.Instance.SetNgPlus(this.ngPlus);
    AtOManager.Instance.SetMadnessCorruptors(this.madnessCorruptors);
    AtOManager.Instance.SetObeliskMadness(this.obeliskMadness);
    AtOManager.Instance.SetWeekly(this.weekly);
    AtOManager.Instance.SetPlayerDust(this.playerDust);
    AtOManager.Instance.SetPlayerGold(this.playerGold);
    AtOManager.Instance.AssignTeamFromSaveGame(JsonHelper.FromJson<Hero>(this.teamAtO));
    if (this.teamAtOBackup != "" && this.teamAtOBackup != null)
      AtOManager.Instance.AssignTeamBackupFromSaveGame(JsonHelper.FromJson<Hero>(this.teamAtOBackup));
    AtOManager.Instance.divinationsNumber = this.divinationsNumber;
    AtOManager.Instance.SetCharInTown(this.inTown);
    AtOManager.Instance.SetTownTier(this.townTier);
    AtOManager.Instance.SetTownZoneId(this.townZoneId);
    AtOManager.Instance.combatExpertise = this.combatExpertise;
    AtOManager.Instance.combatTotal = this.combatTotal;
    AtOManager.Instance.bossesKilled = this.bossesKilled;
    AtOManager.Instance.bossesKilledName = this.bossesKilledName;
    AtOManager.Instance.monstersKilled = this.monstersKilled;
    AtOManager.Instance.currentMapNode = this.currentMapNode;
    AtOManager.Instance.mapVisitedNodes = this.mapVisitedNodes;
    AtOManager.Instance.mapVisitedNodesAction = this.mapVisitedNodesAction;
    AtOManager.Instance.gameNodeAssigned = this.gameNodeAssigned;
    AtOManager.Instance.SetPlayerRequeriments(this.playerRequeriments);
    AtOManager.Instance.townRerollList = this.townRerollList;
    if (this.currentCombatData != "")
      AtOManager.Instance.SetCombatData(Globals.Instance.GetCombatData(this.currentCombatData));
    if (this.fromEventCombatData != "")
    {
      AtOManager.Instance.fromEventCombatData = Globals.Instance.GetCombatData(this.fromEventCombatData);
      if ((UnityEngine.Object) AtOManager.Instance.GetCurrentCombatData() == (UnityEngine.Object) null)
        AtOManager.Instance.SetCombatData(Globals.Instance.GetCombatData(this.fromEventCombatData));
    }
    if (this.fromEventEventData != "")
      AtOManager.Instance.fromEventEventData = Globals.Instance.GetEventData(this.fromEventEventData);
    AtOManager.Instance.heroPerks = this.heroPerks;
    AtOManager.Instance.SetMpPlayersGold(this.mpPlayersGold);
    AtOManager.Instance.SetMpPlayersDust(this.mpPlayersDust);
    AtOManager.Instance.totalGoldGained = this.totalGoldGained;
    AtOManager.Instance.totalDustGained = this.totalDustGained;
    AtOManager.Instance.unlockedCards = this.unlockedCards;
    bool flag = false;
    if (this.craftedCards.GetType().IsGenericType && this.craftedCards.GetType().GetGenericTypeDefinition() == typeof (List<>))
      flag = true;
    if (flag)
      AtOManager.Instance.craftedCards = this.craftedCards;
    AtOManager.Instance.boughtItems = this.boughtItems;
    AtOManager.Instance.boughtItemInShopByWho = this.boughtItemInShopByWho;
    AtOManager.Instance.lootCharacterOrder = this.lootCharacterOrder;
    AtOManager.Instance.conflictCharacterOrder = this.conflictCharacterOrder;
    AtOManager.Instance.gameHandicap = this.gameHandicap;
    AtOManager.Instance.combatStats = this.combatStats;
    AtOManager.Instance.playedTime = this.playedTime;
    AtOManager.Instance.corruptionAccepted = this.corruptionAccepted;
    AtOManager.Instance.corruptionType = this.corruptionType;
    AtOManager.Instance.corruptionId = this.corruptionId;
    AtOManager.Instance.corruptionIdCard = this.corruptionIdCard;
    AtOManager.Instance.corruptionRewardCard = this.corruptionRewardCard;
    AtOManager.Instance.corruptionRewardChar = this.corruptionRewardChar;
    AtOManager.Instance.corruptionCommonCompleted = this.corruptionCommonCompleted;
    AtOManager.Instance.corruptionUncommonCompleted = this.corruptionUncommonCompleted;
    AtOManager.Instance.corruptionRareCompleted = this.corruptionRareCompleted;
    AtOManager.Instance.corruptionEpicCompleted = this.corruptionEpicCompleted;
    AtOManager.Instance.TownTutorialStep = this.townTutorialStep;
    NetworkManager.Instance.Owner0 = this.owner0;
    NetworkManager.Instance.Owner1 = this.owner1;
    NetworkManager.Instance.Owner2 = this.owner2;
    NetworkManager.Instance.Owner3 = this.owner3;
    AtOManager.Instance.SetSandboxMods(this.sandboxMods);
    AtOManager.Instance.saveLoadStatus = false;
    AtOManager.Instance.DoLoadGame();
  }

  public string GameDate
  {
    get => this.gameDate;
    set => this.gameDate = value;
  }

  public string CurrentMapNode
  {
    get => this.currentMapNode;
    set => this.currentMapNode = value;
  }

  public string TeamAtO
  {
    get => this.teamAtO;
    set => this.teamAtO = value;
  }

  public string PlayerPositionList
  {
    get => this.playerPositionList;
    set => this.playerPositionList = value;
  }

  public Enums.GameMode GameMode
  {
    get => this.gameMode;
    set => this.gameMode = value;
  }

  public Dictionary<string, string> PlayerNickRealDict
  {
    get => this.playerNickRealDict;
    set => this.playerNickRealDict = value;
  }

  public int NgPlus
  {
    get => this.ngPlus;
    set => this.ngPlus = value;
  }

  public string MadnessCorruptors
  {
    get => this.madnessCorruptors;
    set => this.madnessCorruptors = value;
  }

  public string SteamUserId
  {
    get => this.steamUserId;
    set => this.steamUserId = value;
  }

  public string Owner0
  {
    get => this.owner0;
    set => this.owner0 = value;
  }

  public string Owner1
  {
    get => this.owner1;
    set => this.owner1 = value;
  }

  public string Owner2
  {
    get => this.owner2;
    set => this.owner2 = value;
  }

  public string Owner3
  {
    get => this.owner3;
    set => this.owner3 = value;
  }

  public string Version
  {
    get => this.version;
    set => this.version = value;
  }

  public int Weekly
  {
    get => this.weekly;
    set => this.weekly = value;
  }

  public int ObeliskMadness
  {
    get => this.obeliskMadness;
    set => this.obeliskMadness = value;
  }

  public Enums.GameType GameType
  {
    get => this.gameType;
    set => this.gameType = value;
  }

  public int TownTier
  {
    get => this.townTier;
    set => this.townTier = value;
  }
}
