// Decompiled with JetBrains decompiler
// Type: PlayerData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
  private string steamUserId;
  private string[] lastUsedTeam;
  private int townTutorialStep;
  private List<string> tutorialWatched;
  private List<string> unlockedHeroes;
  private List<string> unlockedCards;
  private List<string> unlockedNodes;
  private List<string> playerRuns;
  private List<string> bossesKilledName;
  private List<string> supplyBought;
  private bool ngUnlocked;
  private int ngLevel;
  private int playerRankProgress;
  private int maxAdventureMadnessLevel;
  private int obeliskMadnessLevel;
  private int bossesKilled;
  private int monstersKilled;
  private int expGained;
  private int cardsCrafted;
  private int cardsUpgraded;
  private int goldGained;
  private int dustGained;
  private int bestScore;
  private int purchasedItems;
  private int supplyGained;
  private int supplyActual;
  private int corruptionsCompleted;
  private List<string> treasuresClaimed;
  private Dictionary<string, List<string>> unlockedCardsByGame;
  private Dictionary<string, int> heroProgress;
  private Dictionary<string, List<string>> heroPerks;
  private Dictionary<string, string> skinUsed;
  private Dictionary<string, string> cardbackUsed;
  private string sandboxSettings = "";

  public List<string> UnlockedHeroes
  {
    get => this.unlockedHeroes;
    set => this.unlockedHeroes = value;
  }

  public List<string> UnlockedCards
  {
    get => this.unlockedCards;
    set => this.unlockedCards = value;
  }

  public List<string> UnlockedNodes
  {
    get => this.unlockedNodes;
    set => this.unlockedNodes = value;
  }

  public List<string> PlayerRuns
  {
    get => this.playerRuns;
    set => this.playerRuns = value;
  }

  public List<string> TreasuresClaimed
  {
    get => this.treasuresClaimed;
    set => this.treasuresClaimed = value;
  }

  public Dictionary<string, List<string>> UnlockedCardsByGame
  {
    get => this.unlockedCardsByGame;
    set => this.unlockedCardsByGame = value;
  }

  public List<string> TutorialWatched
  {
    get => this.tutorialWatched;
    set => this.tutorialWatched = value;
  }

  public string[] LastUsedTeam
  {
    get => this.lastUsedTeam;
    set => this.lastUsedTeam = value;
  }

  public int BossesKilled
  {
    get => this.bossesKilled;
    set => this.bossesKilled = value;
  }

  public int MonstersKilled
  {
    get => this.monstersKilled;
    set => this.monstersKilled = value;
  }

  public int CardsCrafted
  {
    get => this.cardsCrafted;
    set => this.cardsCrafted = value;
  }

  public int CardsUpgraded
  {
    get => this.cardsUpgraded;
    set => this.cardsUpgraded = value;
  }

  public int GoldGained
  {
    get => this.goldGained;
    set => this.goldGained = value;
  }

  public int DustGained
  {
    get => this.dustGained;
    set => this.dustGained = value;
  }

  public int BestScore
  {
    get => this.bestScore;
    set => this.bestScore = value;
  }

  public int ExpGained
  {
    get => this.expGained;
    set => this.expGained = value;
  }

  public int PurchasedItems
  {
    get => this.purchasedItems;
    set => this.purchasedItems = value;
  }

  public Dictionary<string, int> HeroProgress
  {
    get => this.heroProgress;
    set => this.heroProgress = value;
  }

  public Dictionary<string, List<string>> HeroPerks
  {
    get => this.heroPerks;
    set => this.heroPerks = value;
  }

  public List<string> BossesKilledName
  {
    get => this.bossesKilledName;
    set => this.bossesKilledName = value;
  }

  public int SupplyGained
  {
    get => this.supplyGained;
    set => this.supplyGained = value;
  }

  public int SupplyActual
  {
    get => this.supplyActual;
    set => this.supplyActual = value;
  }

  public List<string> SupplyBought
  {
    get => this.supplyBought;
    set => this.supplyBought = value;
  }

  public bool NgUnlocked
  {
    get => this.ngUnlocked;
    set => this.ngUnlocked = value;
  }

  public int CorruptionsCompleted
  {
    get => this.corruptionsCompleted;
    set => this.corruptionsCompleted = value;
  }

  public string SteamUserId
  {
    get => this.steamUserId;
    set => this.steamUserId = value;
  }

  public int NgLevel
  {
    get => this.ngLevel;
    set => this.ngLevel = value;
  }

  public Dictionary<string, string> SkinUsed
  {
    get => this.skinUsed;
    set => this.skinUsed = value;
  }

  public Dictionary<string, string> CardbackUsed
  {
    get => this.cardbackUsed;
    set => this.cardbackUsed = value;
  }

  public int ObeliskMadnessLevel
  {
    get => this.obeliskMadnessLevel;
    set => this.obeliskMadnessLevel = value;
  }

  public int MaxAdventureMadnessLevel
  {
    get => this.maxAdventureMadnessLevel;
    set => this.maxAdventureMadnessLevel = value;
  }

  public int PlayerRankProgress
  {
    get => this.playerRankProgress;
    set => this.playerRankProgress = value;
  }

  public int TownTutorialStep
  {
    get => this.townTutorialStep;
    set => this.townTutorialStep = value;
  }

  public string SandboxSettings
  {
    get => this.sandboxSettings;
    set => this.sandboxSettings = value;
  }
}
