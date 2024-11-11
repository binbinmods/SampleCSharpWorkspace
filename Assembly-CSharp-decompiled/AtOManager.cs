// Decompiled with JetBrains decompiler
// Type: AtOManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Newtonsoft.Json;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AtOManager : MonoBehaviour
{
  private string gameId = "";
  public Dictionary<string, string> gameNodeAssigned;
  public List<string> mapVisitedNodes;
  public List<string> mapVisitedNodesAction;
  public Dictionary<string, List<string>> shopList;
  private Dictionary<string, int> mpPlayersGold;
  private Dictionary<string, int> mpPlayersDust;
  public int totalGoldGained;
  public int totalDustGained;
  public List<int> lootCharacterOrder;
  public List<int> conflictCharacterOrder;
  public bool followingTheLeader;
  private bool charInTown;
  private int townTier;
  private string townZoneId = "";
  private int ngPlus;
  private string madnessCorruptors = "";
  private int obeliskMadness;
  public int combatTotal;
  public int combatExpertise;
  public int bossesKilled;
  public List<string> bossesKilledName;
  public int monstersKilled;
  private int totalScoreTMP;
  public int mapVisitedNodesTMP;
  private int experienceGainedTMP;
  private int totalDeathsTMP;
  private int combatExpertiseTMP;
  private int bossesKilledTMP;
  private int corruptionCommonCompletedTMP;
  private int corruptionUncommonCompletedTMP;
  private int corruptionRareCompletedTMP;
  private int corruptionEpicCompletedTMP;
  public string currentMapNode = "";
  public bool comingFromCombatDoRewards;
  public List<string> upgradedCardsList;
  private CombatData currentCombatData;
  [SerializeField]
  private int playerDust;
  [SerializeField]
  private int playerGold;
  [SerializeField]
  private Hero[] teamAtO;
  [SerializeField]
  private Hero[] teamAtOBackup;
  [SerializeField]
  private string[] teamNPCAtO;
  [SerializeField]
  private List<string> playerRequeriments;
  private PhotonView photonView;
  private bool GameLoaded;
  public TierRewardData townDivinationTier;
  public string townDivinationCreator;
  public int townDivinationCost;
  public int divinationsNumber;
  private TierRewardData eventRewardTier;
  public int currentRewardTier;
  public string shopItemReroll = "";
  public Dictionary<string, string> townRerollList = new Dictionary<string, string>();
  private string lootListId;
  public CombatData fromEventCombatData;
  public EventData fromEventEventData;
  public NodeData fromEventDestinationNode;
  public SubClassData characterUnlockData;
  public SkinData skinUnlockData;
  public ThermometerData combatThermometerData;
  public GameObject cardcratPrefab;
  private GameObject cardcraftGO;
  private CardCraftManager cardCraftManager;
  public List<Dictionary<string, int>> craftedCards;
  public Dictionary<string, List<string>> boughtItems;
  public Dictionary<string, int> boughtItemInShopByWho;
  public List<string> unlockedCards;
  public int[,] combatStats;
  public int[,] combatStatsCurrent;
  public bool advancedCraft;
  public bool affordableCraft;
  public List<string> craftFilterAura = new List<string>();
  public List<string> craftFilterCurse = new List<string>();
  public List<string> craftFilterDT = new List<string>();
  public bool saveLoadStatus;
  private int saveSlot = -1;
  public bool combatResigned;
  public int gameHandicap;
  public float playedTime;
  public Dictionary<string, List<string>> heroPerks = new Dictionary<string, List<string>>();
  public bool dreadfulPerformance;
  public bool corruptionAccepted;
  public int corruptionType = -1;
  public string corruptionId = "";
  public string corruptionIdCard = "";
  public string corruptionRewardCard = "";
  public int corruptionRewardChar = -1;
  public int corruptionCommonCompleted;
  public int corruptionUncommonCompleted;
  public int corruptionRareCompleted;
  public int corruptionEpicCompleted;
  private int townTutorialStep = -1;
  private bool adventureCompleted;
  private int weekly;
  private List<string> ChallengeTraits;
  public string combatGameCode = "";
  public Dictionary<string, CardData> combatCardDictionary;
  public string combatScarab = "";
  public string obeliskLow = "";
  public string obeliskHigh = "";
  public string obeliskFinal = "";
  private List<string> mapNodeObeliskBoss = new List<string>();
  public CardPlayerPackData cardPlayerPackData;
  public CardPlayerPairsPackData cardPlayerPairsPackData;
  public Dictionary<string, Dictionary<string, string>> skinsInTheGame;
  public string CinematicId = "";
  private Dictionary<string, AuraCurseData> cacheGlobalACModification = new Dictionary<string, AuraCurseData>();
  private bool useCache = true;
  public string weeklyForcedId = "";
  private DateTime currentDate;
  private DateTime startDate;
  private DateTime endDate;
  private int sandbox_startingEnergy;
  private int sandbox_startingSpeed;
  private int sandbox_additionalGold;
  private int sandbox_additionalShards;
  private int sandbox_cardCraftPrice;
  private int sandbox_cardUpgradePrice;
  private int sandbox_cardTransformPrice;
  private int sandbox_cardRemovePrice;
  private int sandbox_itemsPrice;
  private int sandbox_petsPrice;
  private bool sandbox_craftUnlocked;
  private bool sandbox_allRarities;
  private bool sandbox_unlimitedAvailableCards;
  private bool sandbox_freeRerolls;
  private bool sandbox_unlimitedRerolls;
  private int sandbox_divinationsPrice;
  private bool sandbox_noMinimumDecksize;
  private bool sandbox_alwaysPassEventRoll;
  private int sandbox_additionalMonsterHP;
  private int sandbox_additionalMonsterDamage;
  private int sandbox_totalHeroes;
  private int sandbox_lessNPCs;
  private bool sandbox_doubleChampions;

  public static AtOManager Instance { get; private set; }

  private void Awake()
  {
    if ((UnityEngine.Object) AtOManager.Instance == (UnityEngine.Object) null)
      AtOManager.Instance = this;
    else if ((UnityEngine.Object) AtOManager.Instance != (UnityEngine.Object) this)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    this.photonView = PhotonView.Get((Component) this);
  }

  private void Start() => this.InvokeRepeating("PlayedCounter", 0.0f, 1f);

  private void PlayedCounter()
  {
    if (this.gameId != "" && !(bool) (UnityEngine.Object) HeroSelectionManager.Instance && !(bool) (UnityEngine.Object) FinishRunManager.Instance)
    {
      this.currentDate = DateTime.Now;
      this.playedTime += (float) this.currentDate.Subtract(this.startDate).TotalSeconds;
      this.startDate = this.currentDate;
    }
    else
      this.startDate = DateTime.Now;
  }

  private void UpdateOLD()
  {
    if (!(this.gameId != "") || (bool) (UnityEngine.Object) HeroSelectionManager.Instance || (bool) (UnityEngine.Object) FinishRunManager.Instance)
      return;
    this.currentDate = DateTime.Now;
    TimeSpan timeSpan = this.currentDate.Subtract(this.startDate);
    this.playedTime += (float) timeSpan.TotalSeconds;
    this.startDate = this.currentDate;
    Debug.Log((object) (this.playedTime.ToString() + "// " + timeSpan.TotalSeconds.ToString()));
  }

  public void GiveControlDbg()
  {
    for (byte _hero = 0; _hero < (byte) 4; ++_hero)
      this.GiveControlToPlayer(_hero, (byte) 0);
  }

  [PunRPC]
  private void NET_GiveControlToPlayer(byte _hero, byte _player) => this.GiveControlToPlayer(_hero, _player, true);

  public void GiveControlToPlayer(byte _hero, byte _player, bool _fromNet = false)
  {
    if (this.teamAtO[(int) _hero] != null)
    {
      string playerNickPosition = NetworkManager.Instance.GetPlayerNickPosition((int) _player);
      if (playerNickPosition != "")
      {
        this.teamAtO[(int) _hero].Owner = playerNickPosition;
        if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null)
          MatchManager.Instance.SetOwnerForTeamHero((int) _hero, playerNickPosition);
      }
    }
    if (!GameManager.Instance.IsMultiplayer() || _fromNet)
      return;
    this.photonView.RPC("NET_GiveControlToPlayer", RpcTarget.Others, (object) _hero, (object) _player);
  }

  public void ClearCorruption()
  {
    this.corruptionAccepted = false;
    this.corruptionType = -1;
    this.corruptionId = "";
    this.corruptionIdCard = "";
    this.corruptionRewardCard = "";
    this.corruptionRewardChar = -1;
  }

  public string GetSandboxMods() => JsonConvert.SerializeObject((object) new Dictionary<string, int>()
  {
    {
      "sb_isEnabled",
      Convert.ToInt32(SandboxManager.Instance.IsEnabled())
    },
    {
      "sb_startingEnergy",
      this.sandbox_startingEnergy
    },
    {
      "sb_startingSpeed",
      this.sandbox_startingSpeed
    },
    {
      "sb_additionalGold",
      this.sandbox_additionalGold
    },
    {
      "sb_additionalShards",
      this.sandbox_additionalShards
    },
    {
      "sb_cardCraftPrice",
      this.sandbox_cardCraftPrice
    },
    {
      "sb_cardUpgradePrice",
      this.sandbox_cardUpgradePrice
    },
    {
      "sb_cardTransformPrice",
      this.sandbox_cardTransformPrice
    },
    {
      "sb_cardRemovePrice",
      this.sandbox_cardRemovePrice
    },
    {
      "sb_itemsPrice",
      this.sandbox_itemsPrice
    },
    {
      "sb_petsPrice",
      this.sandbox_petsPrice
    },
    {
      "sb_divinationsPrice",
      this.sandbox_divinationsPrice
    },
    {
      "sb_craftUnlocked",
      Convert.ToInt32(this.sandbox_craftUnlocked)
    },
    {
      "sb_allRarities",
      Convert.ToInt32(this.sandbox_allRarities)
    },
    {
      "sb_unlimitedAvailableCards",
      Convert.ToInt32(this.sandbox_unlimitedAvailableCards)
    },
    {
      "sb_freeRerolls",
      Convert.ToInt32(this.sandbox_freeRerolls)
    },
    {
      "sb_unlimitedRerolls",
      Convert.ToInt32(this.sandbox_unlimitedRerolls)
    },
    {
      "sb_noMinimumDecksize",
      Convert.ToInt32(this.sandbox_noMinimumDecksize)
    },
    {
      "sb_alwaysPassEventRoll",
      Convert.ToInt32(this.sandbox_alwaysPassEventRoll)
    },
    {
      "sb_additionalMonsterHP",
      this.sandbox_additionalMonsterHP
    },
    {
      "sb_additionalMonsterDamage",
      this.sandbox_additionalMonsterDamage
    },
    {
      "sb_totalHeroes",
      this.sandbox_totalHeroes
    },
    {
      "sb_lessNPCs",
      this.sandbox_lessNPCs
    },
    {
      "sb_doubleChampions",
      Convert.ToInt32(this.sandbox_doubleChampions)
    }
  });

  public void SetSandboxMods(string json)
  {
    if (json == null || !(json != ""))
      return;
    Dictionary<string, int> dictionary = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
    if (dictionary["sb_isEnabled"] == 1)
      SandboxManager.Instance.EnableSandbox();
    else
      SandboxManager.Instance.DisableSandbox();
    this.sandbox_startingEnergy = dictionary.ContainsKey("sb_startingEnergy") ? dictionary["sb_startingEnergy"] : 0;
    this.sandbox_startingSpeed = dictionary.ContainsKey("sb_startingSpeed") ? dictionary["sb_startingSpeed"] : 0;
    this.sandbox_additionalGold = dictionary.ContainsKey("sb_additionalGold") ? dictionary["sb_additionalGold"] : 0;
    this.sandbox_additionalShards = dictionary.ContainsKey("sb_additionalShards") ? dictionary["sb_additionalShards"] : 0;
    this.sandbox_cardCraftPrice = dictionary.ContainsKey("sb_cardCraftPrice") ? dictionary["sb_cardCraftPrice"] : 0;
    this.sandbox_cardUpgradePrice = dictionary.ContainsKey("sb_cardUpgradePrice") ? dictionary["sb_cardUpgradePrice"] : 0;
    this.sandbox_cardTransformPrice = dictionary.ContainsKey("sb_cardTransformPrice") ? dictionary["sb_cardTransformPrice"] : 0;
    this.sandbox_cardRemovePrice = dictionary.ContainsKey("sb_cardRemovePrice") ? dictionary["sb_cardRemovePrice"] : 0;
    this.sandbox_itemsPrice = dictionary.ContainsKey("sb_itemsPrice") ? dictionary["sb_itemsPrice"] : 0;
    this.sandbox_petsPrice = dictionary.ContainsKey("sb_petsPrice") ? dictionary["sb_petsPrice"] : 0;
    this.sandbox_divinationsPrice = dictionary.ContainsKey("sb_divinationsPrice") ? dictionary["sb_divinationsPrice"] : 0;
    this.sandbox_craftUnlocked = dictionary.ContainsKey("sb_craftUnlocked") && Convert.ToBoolean(dictionary["sb_craftUnlocked"]);
    this.sandbox_allRarities = dictionary.ContainsKey("sb_allRarities") && Convert.ToBoolean(dictionary["sb_allRarities"]);
    this.sandbox_unlimitedAvailableCards = dictionary.ContainsKey("sb_unlimitedAvailableCards") && Convert.ToBoolean(dictionary["sb_unlimitedAvailableCards"]);
    this.sandbox_freeRerolls = dictionary.ContainsKey("sb_freeRerolls") && Convert.ToBoolean(dictionary["sb_freeRerolls"]);
    this.sandbox_unlimitedRerolls = dictionary.ContainsKey("sb_unlimitedRerolls") && Convert.ToBoolean(dictionary["sb_unlimitedRerolls"]);
    this.sandbox_noMinimumDecksize = dictionary.ContainsKey("sb_noMinimumDecksize") && Convert.ToBoolean(dictionary["sb_noMinimumDecksize"]);
    this.sandbox_alwaysPassEventRoll = dictionary.ContainsKey("sb_alwaysPassEventRoll") && Convert.ToBoolean(dictionary["sb_alwaysPassEventRoll"]);
    this.sandbox_additionalMonsterHP = dictionary.ContainsKey("sb_additionalMonsterHP") ? dictionary["sb_additionalMonsterHP"] : 0;
    this.sandbox_additionalMonsterDamage = dictionary.ContainsKey("sb_additionalMonsterDamage") ? dictionary["sb_additionalMonsterDamage"] : 0;
    this.sandbox_totalHeroes = dictionary.ContainsKey("sb_totalHeroes") ? dictionary["sb_totalHeroes"] : 0;
    this.sandbox_lessNPCs = dictionary.ContainsKey("sb_lessNPCs") ? dictionary["sb_lessNPCs"] : 0;
    this.sandbox_doubleChampions = dictionary.ContainsKey("sb_doubleChampions") && Convert.ToBoolean(dictionary["sb_doubleChampions"]);
  }

  private void ShareSandbox()
  {
    this.photonView.RPC("NET_ShareSandbox", RpcTarget.Others, (object) this.GetSandboxMods());
    SandboxManager.Instance.LoadValuesFromAtOManager();
  }

  [PunRPC]
  private void NET_ShareSandbox(string _sandboxJson)
  {
    this.SetSandboxMods(_sandboxJson);
    SandboxManager.Instance.LoadValuesFromAtOManager();
  }

  public void ClearSandbox()
  {
    this.sandbox_startingEnergy = 0;
    this.sandbox_startingSpeed = 0;
    this.sandbox_additionalGold = 0;
    this.sandbox_additionalShards = 0;
    this.sandbox_cardCraftPrice = 0;
    this.sandbox_cardUpgradePrice = 0;
    this.sandbox_cardTransformPrice = 0;
    this.sandbox_cardRemovePrice = 0;
    this.sandbox_itemsPrice = 0;
    this.sandbox_petsPrice = 0;
    this.sandbox_divinationsPrice = 0;
    this.sandbox_craftUnlocked = false;
    this.sandbox_allRarities = false;
    this.sandbox_unlimitedAvailableCards = false;
    this.sandbox_freeRerolls = false;
    this.sandbox_unlimitedRerolls = false;
    this.sandbox_noMinimumDecksize = false;
    this.sandbox_alwaysPassEventRoll = false;
    this.sandbox_totalHeroes = 0;
    this.sandbox_lessNPCs = 0;
    this.sandbox_additionalMonsterHP = 0;
    this.sandbox_additionalMonsterDamage = 0;
    this.sandbox_doubleChampions = false;
  }

  public void InitCombatStats() => this.combatStats = new int[4, 12];

  public void InitCombatStatsCurrent()
  {
    if (this.combatStats == null)
      this.InitCombatStats();
    this.combatStatsCurrent = new int[this.combatStats.GetLength(0), this.combatStats.GetLength(1)];
  }

  public void ClearGame()
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("[ATO] ClearGame", "trace");
    PlayerManager.Instance.RecreateSkins();
    this.playedTime = 0.0f;
    this.teamAtO = (Hero[]) null;
    this.combatGameCode = "";
    this.weeklyForcedId = "";
    this.CleanGameId();
    this.ClearCurrentCombatData();
    this.charInTown = false;
    this.townTier = 0;
    this.currentMapNode = "";
    this.mapVisitedNodes = new List<string>();
    this.mapVisitedNodesAction = new List<string>();
    this.comingFromCombatDoRewards = false;
    this.mpPlayersGold = new Dictionary<string, int>();
    this.mpPlayersDust = new Dictionary<string, int>();
    this.totalGoldGained = 0;
    this.totalDustGained = 0;
    this.townTutorialStep = -1;
    this.ngPlus = 0;
    this.madnessCorruptors = "";
    this.characterUnlockData = (SubClassData) null;
    this.skinUnlockData = (SkinData) null;
    this.adventureCompleted = false;
    this.obeliskMadness = 0;
    this.ChallengeTraits = new List<string>();
    this.weekly = 0;
    this.upgradedCardsList = new List<string>();
    this.shopList = new Dictionary<string, List<string>>();
    this.boughtItems = new Dictionary<string, List<string>>();
    this.boughtItemInShopByWho = new Dictionary<string, int>();
    this.lootCharacterOrder = new List<int>();
    this.conflictCharacterOrder = new List<int>();
    this.gameNodeAssigned = new Dictionary<string, string>();
    this.playerRequeriments = new List<string>();
    this.divinationsNumber = 0;
    this.eventRewardTier = (TierRewardData) null;
    this.townDivinationTier = (TierRewardData) null;
    this.currentRewardTier = 0;
    this.craftedCards = new List<Dictionary<string, int>>();
    for (int index = 0; index < 4; ++index)
      this.craftedCards.Add(new Dictionary<string, int>());
    this.playerDust = 0;
    this.playerGold = 0;
    this.fromEventCombatData = (CombatData) null;
    this.fromEventEventData = (EventData) null;
    this.unlockedCards = new List<string>();
    this.InitCombatStats();
    this.totalScoreTMP = 0;
    this.combatExpertise = 0;
    this.combatTotal = 0;
    this.bossesKilled = 0;
    this.bossesKilledName = new List<string>();
    this.monstersKilled = 0;
    this.combatResigned = false;
    PlayerUIManager.Instance.ClearBag();
    this.corruptionCommonCompleted = 0;
    this.corruptionUncommonCompleted = 0;
    this.corruptionRareCompleted = 0;
    this.corruptionEpicCompleted = 0;
    this.ClearCorruption();
    this.ClearReroll();
    this.ClearSandbox();
    this.ClearTemporalNodeScore();
    GameManager.Instance.SetGameStatus(Enums.GameStatus.NewGame);
    this.obeliskLow = "";
    this.obeliskHigh = "";
    this.obeliskFinal = "";
    this.mapNodeObeliskBoss.Clear();
    this.heroPerks = new Dictionary<string, List<string>>();
    NetworkManager.Instance.ClearSyncro();
    NetworkManager.Instance.ClearPlayerStatus();
    NetworkManager.Instance.ClearWaitingCalls();
  }

  public void RemoveSave()
  {
    if (this.saveSlot == -1)
      return;
    SaveManager.DeleteGame(this.saveSlot);
  }

  public void SetSaveSlot(int slot) => this.saveSlot = slot;

  public int GetSaveSlot() => this.saveSlot;

  public void CleanSaveSlot() => this.saveSlot = -1;

  public void SaveCauseTreasure() => this.photonView.RPC("NET_SaveCauseTresure", RpcTarget.MasterClient);

  [PunRPC]
  public void NET_SaveCauseTresure() => this.SaveGame();

  public void SaveGameTurn()
  {
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster() || this.saveSlot <= -1)
      return;
    SaveManager.SaveGameTurn(this.saveSlot);
  }

  public void LoadGameTurn() => SaveManager.LoadGameTurn(this.saveSlot);

  public void DeleteSaveGameTurn()
  {
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
      return;
    SaveManager.DeleteSaveGameTurn(this.saveSlot);
  }

  public void SaveGame(int slot = -1, bool backUp = false)
  {
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
    {
      this.saveLoadStatus = false;
    }
    else
    {
      this.saveLoadStatus = true;
      if (slot == -1)
        slot = this.saveSlot;
      if (slot != -1)
        SaveManager.SaveGame(slot, backUp);
      else
        this.saveLoadStatus = false;
    }
  }

  public void LoadGame(int slot = -1)
  {
    this.saveLoadStatus = true;
    if (slot == -1)
      slot = this.saveSlot;
    if (slot != -1)
      SaveManager.LoadGame(slot);
    else
      this.saveLoadStatus = false;
  }

  public void MovePetFromAccesoryItem()
  {
    if (this.teamAtO == null)
      return;
    for (int index = 0; index < 4; ++index)
    {
      if (this.teamAtO[index] != null)
      {
        string accesory = this.teamAtO[index].Accesory;
        if (accesory != "")
        {
          CardData cardData = Globals.Instance.GetCardData(accesory, false);
          if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && cardData.CardType == Enums.CardType.Pet)
          {
            this.teamAtO[index].Pet = accesory;
            this.teamAtO[index].Accesory = "";
          }
        }
      }
    }
  }

  public void DoLoadGame()
  {
    if (!GameManager.Instance.IsTutorialGame())
      this.townTutorialStep = -1;
    this.MovePetFromAccesoryItem();
    if (!GameManager.Instance.IsMultiplayer())
    {
      this.RedoSkins();
      if (this.charInTown)
        SceneStatic.LoadByName("Town");
      else
        SceneStatic.LoadByName("Map");
    }
    else
      SceneStatic.LoadByName("Lobby");
  }

  public void DoLoadGameFromMP()
  {
    this.MovePetFromAccesoryItem();
    Debug.Log((object) nameof (DoLoadGameFromMP));
    this.StartCoroutine(this.ShareGameMP());
  }

  private void ShareNGPlus() => this.photonView.RPC("NET_ShareNGPlus", RpcTarget.Others, (object) this.ngPlus, (object) this.madnessCorruptors);

  [PunRPC]
  private void NET_ShareNGPlus(int _ngPlus, string _madnessCorruptors)
  {
    this.ngPlus = _ngPlus;
    this.madnessCorruptors = _madnessCorruptors;
  }

  private void ShareObeliskMadnessAndMaps() => this.photonView.RPC("NET_ShareObeliskMadness", RpcTarget.Others, (object) this.obeliskMadness, (object) this.obeliskLow, (object) this.obeliskHigh, (object) this.obeliskFinal);

  [PunRPC]
  private void NET_ShareObeliskMadness(
    int _obeliskMadness,
    string _oLow,
    string _oHigh,
    string _oFinal)
  {
    this.SetObeliskMadness(_obeliskMadness);
    this.obeliskLow = _oLow;
    this.obeliskHigh = _oHigh;
    this.obeliskFinal = _oFinal;
  }

  public IEnumerator ShareGameMP()
  {
    AtOManager atOmanager = this;
    atOmanager.combatGameCode = "";
    string gameId = atOmanager.gameId;
    string currentMapNode = atOmanager.currentMapNode;
    int ngPlus = atOmanager.ngPlus;
    string madnessCorruptors = atOmanager.madnessCorruptors;
    int obeliskMadness = atOmanager.obeliskMadness;
    string str1 = "";
    if ((UnityEngine.Object) atOmanager.fromEventCombatData != (UnityEngine.Object) null)
      str1 = atOmanager.fromEventCombatData.CombatId;
    string str2 = "";
    if ((UnityEngine.Object) atOmanager.fromEventEventData != (UnityEngine.Object) null)
      str2 = atOmanager.fromEventEventData.EventId;
    string json1 = JsonHelper.ToJson<string>(atOmanager.mapVisitedNodes.ToArray());
    string str3 = "";
    if (atOmanager.mapVisitedNodesAction != null)
      str3 = JsonHelper.ToJson<string>(atOmanager.mapVisitedNodesAction.ToArray());
    string json2 = JsonHelper.ToJson<string>(atOmanager.playerRequeriments.ToArray());
    string json3 = JsonHelper.ToJson<int>(atOmanager.lootCharacterOrder.ToArray());
    string json4 = JsonHelper.ToJson<int>(atOmanager.conflictCharacterOrder.ToArray());
    string str4 = "";
    string str5 = "";
    string str6 = "";
    for (int index1 = 0; index1 < atOmanager.combatStats.GetLength(0); ++index1)
    {
      for (int index2 = 0; index2 < atOmanager.combatStats.GetLength(1); ++index2)
        str6 = str6 + atOmanager.combatStats[index1, index2].ToString() + "_";
      str6 += "/";
    }
    if (atOmanager.boughtItemInShopByWho != null)
    {
      string[] array1 = new string[atOmanager.boughtItemInShopByWho.Count];
      atOmanager.boughtItemInShopByWho.Keys.CopyTo(array1, 0);
      int[] array2 = new int[atOmanager.boughtItemInShopByWho.Count];
      atOmanager.boughtItemInShopByWho.Values.CopyTo(array2, 0);
      str4 = JsonHelper.ToJson<string>(array1);
      str5 = JsonHelper.ToJson<int>(array2);
    }
    int townTier = atOmanager.townTier;
    int combatExpertise = atOmanager.combatExpertise;
    int bossesKilled = atOmanager.bossesKilled;
    int monstersKilled = atOmanager.monstersKilled;
    string json5 = JsonHelper.ToJson<string>(atOmanager.bossesKilledName.ToArray());
    bool corruptionAccepted = atOmanager.corruptionAccepted;
    int corruptionType = atOmanager.corruptionType;
    string corruptionId = atOmanager.corruptionId;
    string corruptionIdCard = atOmanager.corruptionIdCard;
    string corruptionRewardCard = atOmanager.corruptionRewardCard;
    int corruptionRewardChar = atOmanager.corruptionRewardChar;
    int corruptionCommonCompleted = atOmanager.corruptionCommonCompleted;
    if (atOmanager.corruptionCommonCompleted < 0)
      atOmanager.corruptionCommonCompleted = 0;
    int uncommonCompleted = atOmanager.corruptionUncommonCompleted;
    if (atOmanager.corruptionUncommonCompleted < 0)
      atOmanager.corruptionUncommonCompleted = 0;
    int corruptionRareCompleted = atOmanager.corruptionRareCompleted;
    if (atOmanager.corruptionRareCompleted < 0)
      atOmanager.corruptionRareCompleted = 0;
    int corruptionEpicCompleted = atOmanager.corruptionEpicCompleted;
    if (atOmanager.corruptionEpicCompleted < 0)
      atOmanager.corruptionEpicCompleted = 0;
    string str7 = atOmanager.corruptionCommonCompleted.ToString() + "|" + atOmanager.corruptionUncommonCompleted.ToString() + "|" + atOmanager.corruptionRareCompleted.ToString() + "|" + atOmanager.corruptionEpicCompleted.ToString();
    string str8 = "";
    string str9 = "";
    if (atOmanager.craftedCards != null && atOmanager.craftedCards != null)
    {
      for (int index = 0; index < 4; ++index)
      {
        if (atOmanager.craftedCards[index] == null || atOmanager.craftedCards[index].Count == 0)
        {
          str8 += "_";
          str9 += "_";
        }
        else
        {
          string[] array3 = new string[atOmanager.craftedCards[index].Count];
          atOmanager.craftedCards[index].Keys.CopyTo(array3, 0);
          int[] array4 = new int[atOmanager.craftedCards[index].Count];
          atOmanager.craftedCards[index].Values.CopyTo(array4, 0);
          str8 = str8 + JsonHelper.ToJson<string>(array3) + "_";
          str9 = str9 + JsonHelper.ToJson<int>(array4) + "_";
        }
      }
    }
    float playedTime = atOmanager.playedTime;
    int num = 0;
    if (GameManager.Instance.IsObeliskChallenge())
      num = !GameManager.Instance.IsWeeklyChallenge() ? 1 : 2;
    int weekly = atOmanager.weekly;
    JsonHelper.ToJson<Dictionary<string, int>>(atOmanager.craftedCards.ToArray());
    string str10 = "";
    string str11 = "";
    if (atOmanager.townRerollList != null && atOmanager.townRerollList.Count > 0)
    {
      string[] array5 = new string[atOmanager.townRerollList.Count];
      atOmanager.townRerollList.Keys.CopyTo(array5, 0);
      string[] array6 = new string[atOmanager.townRerollList.Count];
      atOmanager.townRerollList.Values.CopyTo(array6, 0);
      str10 = JsonHelper.ToJson<string>(array5);
      str11 = JsonHelper.ToJson<string>(array6);
    }
    NetworkManager.Instance.ClearAllPlayerManualReady();
    atOmanager.photonView.RPC("NET_ShareGameMP", RpcTarget.Others, (object) gameId, (object) townTier, (object) currentMapNode, (object) ngPlus, (object) madnessCorruptors, (object) obeliskMadness, (object) str1, (object) str2, (object) json1, (object) str3, (object) json2, (object) str4, (object) str5, (object) json3, (object) json4, (object) str6, (object) combatExpertise, (object) bossesKilled, (object) json5, (object) monstersKilled, (object) corruptionAccepted, (object) corruptionType, (object) corruptionId, (object) corruptionIdCard, (object) corruptionRewardCard, (object) corruptionRewardChar, (object) str8, (object) str9, (object) str7, (object) playedTime, (object) num, (object) weekly, (object) str10, (object) str11);
    while (!NetworkManager.Instance.AllPlayersReady("sharegamemp"))
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    atOmanager.GetTotalPlayersGold();
    atOmanager.GetTotalPlayersDust();
    int numPlayers = NetworkManager.Instance.GetNumPlayers();
    atOmanager.mpPlayersGold = new Dictionary<string, int>();
    atOmanager.mpPlayersDust = new Dictionary<string, int>();
    for (int position = 0; position < numPlayers; ++position)
    {
      float f1 = 0.0f;
      float f2 = 0.0f;
      string playerNickPosition = NetworkManager.Instance.GetPlayerNickPosition(position);
      for (int index = 0; index < 4; ++index)
      {
        if (NetworkManager.Instance.PlayerHeroPositionOwner[index] == playerNickPosition)
        {
          f1 += atOmanager.teamAtO[index].HeroGold;
          f2 += atOmanager.teamAtO[index].HeroDust;
        }
      }
      atOmanager.GivePlayer(0, Mathf.CeilToInt(f1), playerNickPosition, anim: false);
      atOmanager.GivePlayer(1, Mathf.CeilToInt(f2), playerNickPosition, anim: false);
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance)
      atOmanager.StartCoroutine(atOmanager.ShareTeam("Combat", setOwners: true));
    else if (atOmanager.charInTown)
      atOmanager.StartCoroutine(atOmanager.ShareTeam("Town", setOwners: true));
    else
      atOmanager.StartCoroutine(atOmanager.ShareTeam("Map", setOwners: true));
  }

  [PunRPC]
  private void NET_ShareGameMP(
    string _gameId,
    int _townTier,
    string _currentMapNode,
    int _ngPlus,
    string _madnessCorruptors,
    int _obeliskMadness,
    string _fromEventCombatDataId,
    string _fromEventEventDataId,
    string _mapVisitedNodes,
    string _mapVisitedNodesAction,
    string _playerRequeriments,
    string _boughtKeys,
    string _boughtValues,
    string _lootCharacterOrder,
    string _conflictCharacterOrder,
    string _combatStats,
    int _combatExpertise,
    int _bossesKilled,
    string _bossesKilledName,
    int _monstersKilled,
    bool _corruptionAccepted,
    int _corruptionType,
    string _corruptionId,
    string _corruptionIdCard,
    string _corruptionRewardCard,
    int _corruptionRewardChar,
    string _craftedKeys,
    string _craftedValues,
    string _corruptionsCompleted,
    float _playedTime,
    int _gameType,
    int _weekly,
    string _rerollKeys,
    string _rerollValues)
  {
    this.combatGameCode = "";
    this.corruptionAccepted = _corruptionAccepted;
    this.corruptionType = _corruptionType;
    this.corruptionId = _corruptionId;
    this.corruptionIdCard = _corruptionIdCard;
    this.corruptionRewardCard = _corruptionRewardCard;
    this.corruptionRewardChar = _corruptionRewardChar;
    string[] strArray1 = _corruptionsCompleted.Split('|', StringSplitOptions.None);
    if (strArray1[0] != null)
      this.corruptionCommonCompleted = int.Parse(strArray1[0]);
    if (strArray1[1] != null)
      this.corruptionUncommonCompleted = int.Parse(strArray1[1]);
    if (strArray1[2] != null)
      this.corruptionRareCompleted = int.Parse(strArray1[2]);
    if (strArray1[3] != null)
      this.corruptionEpicCompleted = int.Parse(strArray1[3]);
    this.ngPlus = _ngPlus;
    this.madnessCorruptors = _madnessCorruptors;
    this.SetObeliskMadness(_obeliskMadness);
    this.gameId = _gameId;
    this.townTier = _townTier;
    this.combatExpertise = _combatExpertise;
    this.bossesKilled = _bossesKilled;
    this.monstersKilled = _monstersKilled;
    this.SetTownTier(this.townTier);
    this.currentMapNode = _currentMapNode;
    if ((UnityEngine.Object) Globals.Instance.GetNodeData(_currentMapNode) != (UnityEngine.Object) null)
      this.SetTownZoneId(Globals.Instance.GetNodeData(_currentMapNode).NodeZone.ZoneId);
    this.fromEventCombatData = Globals.Instance.GetCombatData(_fromEventCombatDataId);
    this.fromEventEventData = Globals.Instance.GetEventData(_fromEventEventDataId);
    this.mapVisitedNodes = ((IEnumerable<string>) JsonHelper.FromJson<string>(_mapVisitedNodes)).ToList<string>();
    this.mapVisitedNodesAction = _mapVisitedNodesAction == null || !(_mapVisitedNodesAction != "") ? new List<string>() : ((IEnumerable<string>) JsonHelper.FromJson<string>(_mapVisitedNodesAction)).ToList<string>();
    this.playerRequeriments = ((IEnumerable<string>) JsonHelper.FromJson<string>(_playerRequeriments)).ToList<string>();
    this.bossesKilledName = ((IEnumerable<string>) JsonHelper.FromJson<string>(_bossesKilledName)).ToList<string>();
    string[] strArray2 = _combatStats.Split('/', StringSplitOptions.None);
    string[] strArray3 = strArray2[0].Split('_', StringSplitOptions.None);
    this.combatStats = new int[strArray2.Length - 1, strArray3.Length - 1];
    string[] strArray4 = _combatStats.Split('/', StringSplitOptions.None);
    for (int index1 = 0; index1 < this.combatStats.GetLength(0); ++index1)
    {
      string[] strArray5 = strArray4[index1].Split('_', StringSplitOptions.None);
      for (int index2 = 0; index2 < this.combatStats.GetLength(1); ++index2)
        this.combatStats[index1, index2] = index2 >= strArray5.Length || strArray5[index2] == null ? 0 : (string.IsNullOrEmpty(strArray5[index2]) ? 0 : int.Parse(strArray5[index2]));
    }
    if (_boughtKeys != "")
    {
      string[] strArray6 = JsonHelper.FromJson<string>(_boughtKeys);
      int[] numArray = JsonHelper.FromJson<int>(_boughtValues);
      this.boughtItemInShopByWho = new Dictionary<string, int>();
      for (int index = 0; index < strArray6.Length; ++index)
        this.boughtItemInShopByWho.Add(strArray6[index], numArray[index]);
    }
    this.lootCharacterOrder = ((IEnumerable<int>) JsonHelper.FromJson<int>(_lootCharacterOrder)).ToList<int>();
    this.conflictCharacterOrder = ((IEnumerable<int>) JsonHelper.FromJson<int>(_conflictCharacterOrder)).ToList<int>();
    if (_craftedKeys != "")
    {
      string[] strArray7 = _craftedKeys.Split('_', StringSplitOptions.None);
      string[] strArray8 = _craftedValues.Split('_', StringSplitOptions.None);
      this.craftedCards = new List<Dictionary<string, int>>();
      for (int index3 = 0; index3 < 4; ++index3)
      {
        if (strArray7[index3].Length > 0)
        {
          string[] strArray9 = JsonHelper.FromJson<string>(strArray7[index3]);
          if (strArray9.Length != 0)
          {
            int[] numArray = JsonHelper.FromJson<int>(strArray8[index3]);
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for (int index4 = 0; index4 < strArray9.Length; ++index4)
              dictionary.Add(strArray9[index4], numArray[index4]);
            this.craftedCards.Add(dictionary);
          }
          else
            this.craftedCards.Add(new Dictionary<string, int>());
        }
        else
          this.craftedCards.Add(new Dictionary<string, int>());
      }
    }
    this.playedTime = _playedTime;
    switch (_gameType)
    {
      case 0:
        GameManager.Instance.GameType = Enums.GameType.Adventure;
        break;
      case 1:
        GameManager.Instance.GameType = Enums.GameType.Challenge;
        break;
      case 2:
        GameManager.Instance.GameType = Enums.GameType.WeeklyChallenge;
        break;
    }
    this.SetWeekly(_weekly);
    if (_rerollKeys != "")
    {
      string[] strArray10 = JsonHelper.FromJson<string>(_rerollKeys);
      string[] strArray11 = JsonHelper.FromJson<string>(_rerollValues);
      this.townRerollList = new Dictionary<string, string>();
      string playerNick = NetworkManager.Instance.GetPlayerNick();
      for (int index = 0; index < strArray10.Length; ++index)
      {
        this.townRerollList.Add(strArray10[index], strArray11[index]);
        Debug.Log((object) (strArray10[index] + "=>" + strArray11[index]));
        if (strArray10[index] == playerNick)
        {
          string[] strArray12 = strArray11[index].Split("_", StringSplitOptions.None);
          if (strArray12.Length != 0)
            this.shopItemReroll = strArray12[strArray12.Length - 1];
        }
      }
    }
    NetworkManager.Instance.SetStatusReady("sharegamemp");
  }

  public void SwapCharacter(SubClassData _scd, int _position)
  {
    for (int index = 0; index < 4; ++index)
    {
      if (index == _position && this.teamAtO[index] != null)
      {
        string owner = this.teamAtO[index].Owner;
        this.teamAtO[index] = GameManager.Instance.CreateHero(_scd.SubClassName);
        this.teamAtO[index].Owner = owner;
        break;
      }
    }
  }

  public void TravelBetweenZones(ZoneData _fromZone, ZoneData _toZone)
  {
    if (_toZone.ChangeTeamOnEntrance)
    {
      if (_toZone.NewTeam != null && _toZone.NewTeam.Count == 4)
      {
        this.CreateTeamBackup();
        Array.Copy((Array) this.teamAtO, (Array) this.teamAtOBackup, 4);
        string[] _team = new string[4];
        for (int index = 0; index < 4; ++index)
          _team[index] = _toZone.NewTeam[index].SubClassName;
        this.SetTeamFromArray(_team);
        for (int index = 0; index < 4; ++index)
        {
          if (this.teamAtOBackup[index] != null && this.teamAtO[index] != null)
            this.teamAtO[index].Owner = this.teamAtOBackup[index].Owner;
        }
        if (GameManager.Instance.IsMultiplayer() && SandboxManager.Instance.IsEnabled())
        {
          if (this.sandbox_totalHeroes == 2)
          {
            this.teamAtO[2].Owner = this.teamAtOBackup[0].Owner;
            this.teamAtO[3].Owner = this.teamAtOBackup[1].Owner;
          }
          else if (this.sandbox_totalHeroes == 3)
            this.teamAtO[3].Owner = this.teamAtOBackup[0].Owner;
        }
      }
    }
    else if (_fromZone.RestoreTeamOnExit && this.teamAtOBackup != null)
    {
      for (int index = 0; index < 4; ++index)
      {
        if (this.teamAtOBackup[index] != null && this.teamAtO[index] != null)
          this.teamAtOBackup[index].Owner = this.teamAtO[index].Owner;
      }
      this.CreateTeam();
      Array.Copy((Array) this.teamAtOBackup, (Array) this.teamAtO, 4);
      this.teamAtOBackup = new Hero[4];
    }
    this.SetTownZoneId(_toZone.ZoneId);
    SceneStatic.LoadByName("IntroNewGame");
  }

  public void SetTownZoneId(string _townZoneId) => this.townZoneId = _townZoneId;

  public string GetTownZoneId() => this.townZoneId;

  public void SetCharInTown(bool _state)
  {
    this.charInTown = _state;
    this.TownRerollRestore();
  }

  public bool CharInTown() => this.charInTown;

  public void SetTownTier(int _townTier)
  {
    this.townTier = _townTier;
    this.gameHandicap = this.townTier;
    if (!GameManager.Instance.IsMultiplayer() || !NetworkManager.Instance.IsMaster())
      return;
    this.photonView.RPC("NET_SetTownTier", RpcTarget.Others, (object) this.townTier);
  }

  [PunRPC]
  private void NET_SetTownTier(int _townTier) => this.SetTownTier(_townTier);

  public int GetActNumberForText(string _currentMapNode = "")
  {
    int actNumberForText1 = 0;
    if (_currentMapNode == "")
      _currentMapNode = this.currentMapNode;
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      if (_currentMapNode != "")
      {
        if (_currentMapNode == "sen_0" || _currentMapNode == "tutorial_0" || _currentMapNode == "tutorial_1" || _currentMapNode == "tutorial_2")
          return 1;
        actNumberForText1 = this.townTier + 1;
        if ((UnityEngine.Object) Globals.Instance.GetNodeData(_currentMapNode) != (UnityEngine.Object) null)
        {
          string[] strArray = Globals.Instance.GetNodeData(_currentMapNode).NodeId.Split('_', StringSplitOptions.None);
          if (strArray.Length == 2 && strArray[1] == "0")
          {
            int actNumberForText2 = actNumberForText1 + 1;
            if (actNumberForText2 > 4)
              actNumberForText2 = 4;
            return actNumberForText2;
          }
        }
      }
    }
    else
      actNumberForText1 = !this.NodeIsObeliskLow() ? (!this.NodeIsObeliskFinal() ? 2 : 3) : 1;
    return actNumberForText1;
  }

  public int GetTownTier() => this.townTier;

  public void UpgradeTownTier()
  {
    int _townTier = 0;
    if ((UnityEngine.Object) Globals.Instance.GetRequirementData("_tier4") != (UnityEngine.Object) null && this.PlayerHasRequirement(Globals.Instance.GetRequirementData("_tier4")))
      _townTier = 4;
    else if ((UnityEngine.Object) Globals.Instance.GetRequirementData("_tier3") != (UnityEngine.Object) null && this.PlayerHasRequirement(Globals.Instance.GetRequirementData("_tier3")))
      _townTier = 3;
    else if ((UnityEngine.Object) Globals.Instance.GetRequirementData("_tier2") != (UnityEngine.Object) null && this.PlayerHasRequirement(Globals.Instance.GetRequirementData("_tier2")))
      _townTier = 2;
    else if ((UnityEngine.Object) Globals.Instance.GetRequirementData("_tier1") != (UnityEngine.Object) null && this.PlayerHasRequirement(Globals.Instance.GetRequirementData("_tier1")))
      _townTier = 1;
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("UpgradeTownTier " + _townTier.ToString());
    this.SetTownTier(_townTier);
  }

  private void InitGame()
  {
    this.combatResigned = false;
    int num1 = 300;
    int num2 = 300;
    if (GameManager.Instance.IsObeliskChallenge())
    {
      num1 = num2 = 200;
      if (this.IsChallengeTraitActive("wealthyheroes"))
        num1 = num2 = 1200;
    }
    else if (this.ngPlus > 0)
    {
      if (this.ngPlus == 3)
        num1 = num2 = 3200;
      else if (this.ngPlus == 4)
        num1 = num2 = 2800;
      else if (this.ngPlus == 5)
        num1 = num2 = 2400;
      else if (this.ngPlus == 6)
        num1 = num2 = 2000;
      else if (this.ngPlus == 7)
        num1 = num2 = 1600;
      else if (this.ngPlus == 8)
        num1 = num2 = 1200;
    }
    for (int index = 0; index < 4; ++index)
    {
      if (this.teamAtO[index] != null)
      {
        num1 += PlayerManager.Instance.GetPerkCurrency(this.teamAtO[index].SubclassName);
        num2 += PlayerManager.Instance.GetPerkShards(this.teamAtO[index].SubclassName);
      }
    }
    int quantity1 = num1 + this.sandbox_additionalGold;
    int quantity2 = num2 + this.sandbox_additionalShards;
    this.GivePlayer(0, quantity1, anim: false);
    this.GivePlayer(1, quantity2, anim: false);
  }

  private void InitGameMP()
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("InitGameMP gameid->" + this.gameId);
    NetworkManager.Instance.PlayerPositionListArray();
    for (int position = 0; position < NetworkManager.Instance.GetNumPlayers(); ++position)
    {
      string playerNickPosition = NetworkManager.Instance.GetPlayerNickPosition(position);
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < 4; ++index)
      {
        if (NetworkManager.Instance.PlayerHeroPositionOwner[index] == playerNickPosition)
        {
          int num3 = 75;
          int num4 = 75;
          if (GameManager.Instance.IsObeliskChallenge())
          {
            num3 = num4 = 50;
            if (this.IsChallengeTraitActive("wealthyheroes"))
              num3 = num4 = 300;
          }
          else if (this.ngPlus > 0)
          {
            if (this.ngPlus == 3)
              num3 = num4 = 800;
            else if (this.ngPlus == 4)
              num3 = num4 = 700;
            else if (this.ngPlus == 5)
              num3 = num4 = 600;
            else if (this.ngPlus == 6)
              num3 = num4 = 500;
            else if (this.ngPlus == 7)
              num3 = num4 = 400;
            else if (this.ngPlus == 8)
              num3 = num4 = 300;
          }
          num1 += num3;
          num2 += num4;
          if (this.teamAtO[index] != null && (UnityEngine.Object) this.teamAtO[index].HeroData != (UnityEngine.Object) null)
          {
            num1 += PlayerManager.Instance.GetPerkCurrency(this.teamAtO[index].SubclassName);
            num2 += PlayerManager.Instance.GetPerkShards(this.teamAtO[index].SubclassName);
          }
        }
      }
      int quantity1 = num1 + this.sandbox_additionalGold;
      int quantity2 = num2 + this.sandbox_additionalShards;
      this.GivePlayer(0, quantity1, playerNickPosition, anim: false);
      this.GivePlayer(1, quantity2, playerNickPosition, anim: false);
    }
  }

  public void SetGameId(string _gameId = "")
  {
    if (_gameId == "")
    {
      this.gameId = !GameManager.Instance.IsWeeklyChallenge() ? Functions.RandomStringSafe(7f).ToUpper() : this.GetWeeklySeed();
      if (!GameManager.Instance.IsMultiplayer() || !NetworkManager.Instance.IsMaster())
        return;
      this.photonView.RPC("NET_SetGameId", RpcTarget.Others, (object) this.gameId);
    }
    else
      this.gameId = _gameId.ToUpper();
  }

  public void SetWeeklyRandomState() => UnityEngine.Random.InitState(("WeeklyChallengeWeek" + Functions.GetCurrentWeeklyWeek().ToString()).GetDeterministicHashCode());

  private string GetWeeklySeed()
  {
    this.SetWeeklyRandomState();
    return Functions.RandomStringSafe(7f).ToUpper();
  }

  public void CleanGameId() => this.gameId = "";

  [PunRPC]
  private void NET_SetGameId(string _gameId) => this.SetGameId(_gameId);

  public string GetGameId() => this.gameId;

  public void SaveCraftedCard(int index, string cardId)
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("SaveCraftedCard ->" + cardId);
    if (this.craftedCards == null || this.craftedCards.Count == 0)
    {
      this.craftedCards = new List<Dictionary<string, int>>();
      for (int index1 = 0; index1 < 4; ++index1)
        this.craftedCards.Add(new Dictionary<string, int>());
    }
    if (this.craftedCards[index] == null)
      this.craftedCards[index] = new Dictionary<string, int>();
    if (this.craftedCards[index].ContainsKey(cardId))
      this.craftedCards[index][cardId]++;
    else
      this.craftedCards[index].Add(cardId, 1);
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
      this.photonView.RPC("NET_SaveCraftedCard", RpcTarget.MasterClient, (object) index, (object) cardId);
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
      return;
    this.SaveGame();
  }

  [PunRPC]
  public void NET_SaveCraftedCard(int index, string cardId) => this.SaveCraftedCard(index, cardId);

  public int HowManyCrafted(int index, string cardId) => this.craftedCards == null || index >= this.craftedCards.Count || this.craftedCards[index] == null || !this.craftedCards[index].ContainsKey(cardId) ? 0 : this.craftedCards[index][cardId];

  public void SideBarRefresh()
  {
    if ((bool) (UnityEngine.Object) MapManager.Instance)
    {
      MapManager.Instance.sideCharacters.Refresh();
    }
    else
    {
      if (!(bool) (UnityEngine.Object) TownManager.Instance)
        return;
      TownManager.Instance.sideCharacters.Refresh();
    }
  }

  public void ReDrawLevel()
  {
    if ((bool) (UnityEngine.Object) MapManager.Instance)
    {
      MapManager.Instance.characterWindow.ReDrawLevel();
    }
    else
    {
      if (!(bool) (UnityEngine.Object) TownManager.Instance)
        return;
      TownManager.Instance.characterWindow.ReDrawLevel();
    }
  }

  public void SideBarRefreshCards(int heroIndex = 0)
  {
    if ((bool) (UnityEngine.Object) MapManager.Instance)
    {
      MapManager.Instance.sideCharacters.RefreshCards(heroIndex);
    }
    else
    {
      if (!(bool) (UnityEngine.Object) TownManager.Instance)
        return;
      TownManager.Instance.sideCharacters.RefreshCards(heroIndex);
    }
  }

  public void SideBarCharacterClicked(int characterIndex)
  {
    if ((bool) (UnityEngine.Object) CardCraftManager.Instance)
    {
      if (CardCraftManager.Instance.craftType == 3)
        return;
      if (!((UnityEngine.Object) MapManager.Instance != (UnityEngine.Object) null) || !MapManager.Instance.characterWindow.IsActive())
        CardCraftManager.Instance.SelectCharacter(characterIndex);
    }
    if ((bool) (UnityEngine.Object) MapManager.Instance)
      MapManager.Instance.sideCharacters.SetActive(characterIndex);
    else if ((bool) (UnityEngine.Object) TownManager.Instance)
      TownManager.Instance.sideCharacters.SetActive(characterIndex);
    else if ((bool) (UnityEngine.Object) MatchManager.Instance)
    {
      MatchManager.Instance.sideCharacters.SetActive(characterIndex);
    }
    else
    {
      if (!(bool) (UnityEngine.Object) ChallengeSelectionManager.Instance)
        return;
      ChallengeSelectionManager.Instance.sideCharacters.SetActive(characterIndex);
    }
  }

  public int GetPlayerGold() => this.playerGold;

  public void SetPlayerGold(int _playerGold)
  {
    this.playerGold = _playerGold;
    this.RefreshQuantities(0, false);
  }

  public int GetPlayerDust() => this.playerDust;

  public void SetPlayerDust(int _playerDust)
  {
    this.playerDust = _playerDust;
    this.RefreshQuantities(1, false);
  }

  public Dictionary<string, int> GetMpPlayersGold() => this.mpPlayersGold;

  public void SetMpPlayersGold(Dictionary<string, int> _mpPlayersGold) => this.mpPlayersGold = _mpPlayersGold;

  public Dictionary<string, int> GetMpPlayersDust() => this.mpPlayersDust;

  public void SetMpPlayersDust(Dictionary<string, int> _mpPlayersDust) => this.mpPlayersDust = _mpPlayersDust;

  public void RefreshQuantities(int type, bool anim = true)
  {
    switch (type)
    {
      case 0:
        PlayerUIManager.Instance.SetGold(anim);
        break;
      case 1:
        PlayerUIManager.Instance.SetDust(anim);
        break;
    }
    this.RefreshScreens();
  }

  private void RefreshScreens()
  {
    if (!((UnityEngine.Object) CardCraftManager.Instance != (UnityEngine.Object) null))
      return;
    CardCraftManager.Instance.RefreshCardPrices();
  }

  public bool IsFirstGame()
  {
    if (GameManager.Instance.ProfileId != 0 && GameManager.Instance.TutorialWatched("firstTurnEnergy"))
      return false;
    int monstersKilled = PlayerManager.Instance.MonstersKilled;
    return PlayerManager.Instance.MonstersKilled < 1;
  }

  public void AskGivePlayerToPlayer(int type, int quantity, string to, string from) => this.photonView.RPC("NET_MASTERGivePlayer", RpcTarget.MasterClient, (object) type, (object) quantity, (object) to, (object) from, (object) true, (object) true);

  public void AskForGold(string nick, int quantity) => this.photonView.RPC("NET_MASTERGivePlayer", RpcTarget.MasterClient, (object) 0, (object) quantity, (object) nick, (object) "", (object) true, (object) true);

  public void AskForDust(string nick, int quantity) => this.photonView.RPC("NET_MASTERGivePlayer", RpcTarget.MasterClient, (object) 1, (object) quantity, (object) nick, (object) "", (object) true, (object) true);

  [PunRPC]
  public void NET_MASTERGivePlayer(
    int type,
    int quantity,
    string to,
    string from,
    bool anim = true,
    bool save = false)
  {
    this.GivePlayer(type, quantity, to, from, anim, save);
  }

  public void GivePlayer(int type, int quantity, string to = "", string from = "", bool anim = true, bool save = false)
  {
    if (this.mpPlayersGold == null)
      return;
    if (type == 0 && quantity > 0)
      this.totalGoldGained += quantity;
    else if (type == 1 && quantity > 0)
      this.totalDustGained += quantity;
    if (!GameManager.Instance.IsMultiplayer())
    {
      if (type == 0)
      {
        this.playerGold += quantity;
        if (this.playerGold < 0)
          this.playerGold = 0;
        this.mpPlayersGold[NetworkManager.Instance.GetPlayerNick()] = this.playerGold;
        this.RefreshQuantities(0, anim);
      }
      if (type == 1)
      {
        this.playerDust += quantity;
        if (this.playerDust < 0)
          this.playerDust = 0;
        this.mpPlayersDust[NetworkManager.Instance.GetPlayerNick()] = this.playerDust;
        this.RefreshQuantities(1, anim);
      }
      if (type != 2)
        return;
      PlayerManager.Instance.GainSupply(quantity);
    }
    else
    {
      if (!NetworkManager.Instance.IsMaster())
        return;
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("GIVE " + type.ToString() + " " + quantity.ToString() + " TO " + to);
      switch (type)
      {
        case 0:
          if (from != "")
          {
            if (this.mpPlayersGold.ContainsKey(from))
            {
              if (this.mpPlayersGold[from] < quantity)
                quantity = this.mpPlayersGold[from];
              this.mpPlayersGold[from] -= quantity;
              if (this.mpPlayersGold[from] < 0)
                this.mpPlayersGold[from] = 0;
            }
            else
            {
              this.mpPlayersGold.Add(from, 0);
              return;
            }
          }
          if (to == "")
            to = NetworkManager.Instance.GetPlayerNick();
          if (this.mpPlayersGold.ContainsKey(to))
            this.mpPlayersGold[to] += quantity;
          else
            this.mpPlayersGold.Add(to, quantity);
          if (this.mpPlayersGold[to] < 0)
            this.mpPlayersGold[to] = 0;
          this.ShareQuantities(0, anim);
          if (this.mpPlayersGold != null && this.mpPlayersGold.ContainsKey(from) && Globals.Instance.ShowDebug)
          {
            Functions.DebugLogGD("Gold from ->" + from + " = " + this.mpPlayersGold[from].ToString());
            break;
          }
          break;
        case 1:
          if (from != "")
          {
            if (this.mpPlayersDust.ContainsKey(from))
            {
              if (this.mpPlayersDust[from] < quantity)
                quantity = this.mpPlayersDust[from];
              this.mpPlayersDust[from] -= quantity;
              if (this.mpPlayersDust[from] < 0)
                this.mpPlayersDust[from] = 0;
            }
            else
            {
              this.mpPlayersDust.Add(from, 0);
              return;
            }
          }
          if (to == "")
            to = NetworkManager.Instance.GetPlayerNick();
          if (this.mpPlayersDust.ContainsKey(to))
            this.mpPlayersDust[to] += quantity;
          else
            this.mpPlayersDust.Add(to, quantity);
          if (this.mpPlayersDust[to] < 0)
            this.mpPlayersDust[to] = 0;
          this.ShareQuantities(1, anim);
          break;
        case 2:
          this.photonView.RPC("NET_GainSupply", RpcTarget.All, (object) to, (object) quantity);
          break;
      }
      if (!save)
        return;
      this.SaveGame();
    }
  }

  [PunRPC]
  private void NET_GainSupply(string nickname, int quantity)
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD(nickname + "== " + NetworkManager.Instance.GetPlayerNick());
    if (!(nickname == NetworkManager.Instance.GetPlayerNick()))
      return;
    PlayerManager.Instance.GainSupply(quantity);
  }

  public void SellSupply(int _quantity)
  {
    int supplyActual = PlayerManager.Instance.SupplyActual;
    if (_quantity <= 0 || _quantity > PlayerManager.Instance.SupplyActual)
      return;
    if (!GameManager.Instance.IsMultiplayer())
    {
      this.GivePlayer(0, _quantity * 100);
      this.GivePlayer(1, _quantity * 100);
    }
    else
    {
      this.AskForGold(NetworkManager.Instance.GetPlayerNick(), _quantity * 100);
      this.AskForDust(NetworkManager.Instance.GetPlayerNick(), _quantity * 100);
    }
    PlayerManager.Instance.SpendSupply(_quantity);
    SaveManager.SavePlayerData();
  }

  public void DistributeGoldDustBetweenHeroes()
  {
    for (int position = 0; position < NetworkManager.Instance.GetNumPlayers(); ++position)
    {
      string playerNickPosition = NetworkManager.Instance.GetPlayerNickPosition(position);
      int num1 = 0;
      for (int index = 0; index < 4; ++index)
      {
        if (NetworkManager.Instance.PlayerHeroPositionOwner[index] == playerNickPosition)
          ++num1;
      }
      float num2 = 0.0f;
      float num3 = 0.0f;
      if (this.mpPlayersGold.ContainsKey(playerNickPosition))
        num2 = (float) this.mpPlayersGold[playerNickPosition] / (float) num1;
      if (this.mpPlayersDust.ContainsKey(playerNickPosition))
        num3 = (float) this.mpPlayersDust[playerNickPosition] / (float) num1;
      for (int index = 0; index < 4; ++index)
      {
        if (NetworkManager.Instance.PlayerHeroPositionOwner[index] == playerNickPosition)
        {
          this.teamAtO[index].HeroGold = num2;
          this.teamAtO[index].HeroDust = num3;
        }
      }
    }
  }

  private void ShareQuantities(int type, bool anim = true)
  {
    string[] array1;
    int[] array2;
    if (type == 0)
    {
      array1 = new string[this.mpPlayersGold.Count];
      this.mpPlayersGold.Keys.CopyTo(array1, 0);
      array2 = new int[this.mpPlayersGold.Count];
      this.mpPlayersGold.Values.CopyTo(array2, 0);
    }
    else
    {
      array1 = new string[this.mpPlayersDust.Count];
      this.mpPlayersDust.Keys.CopyTo(array1, 0);
      array2 = new int[this.mpPlayersDust.Count];
      this.mpPlayersDust.Values.CopyTo(array2, 0);
    }
    string json1 = JsonHelper.ToJson<string>(array1);
    string json2 = JsonHelper.ToJson<int>(array2);
    this.photonView.RPC("NET_ShareGoldDustDict", RpcTarget.All, (object) type, (object) json1, (object) json2, (object) anim, (object) this.totalGoldGained, (object) this.totalDustGained);
  }

  [PunRPC]
  private void NET_ShareGoldDustDict(
    int type,
    string _keys,
    string _values,
    bool anim,
    int _totalGoldGained,
    int _totalDustGained)
  {
    string[] strArray = JsonHelper.FromJson<string>(_keys);
    int[] numArray = JsonHelper.FromJson<int>(_values);
    string playerNick = NetworkManager.Instance.GetPlayerNick();
    switch (type)
    {
      case 0:
        this.mpPlayersGold = new Dictionary<string, int>();
        for (int index = 0; index < strArray.Length; ++index)
        {
          this.mpPlayersGold.Add(strArray[index], numArray[index]);
          if (strArray[index] == playerNick)
            this.playerGold = numArray[index];
        }
        break;
      case 1:
        this.mpPlayersDust = new Dictionary<string, int>();
        for (int index = 0; index < strArray.Length; ++index)
        {
          this.mpPlayersDust.Add(strArray[index], numArray[index]);
          if (strArray[index] == playerNick)
            this.playerDust = numArray[index];
        }
        break;
    }
    this.totalGoldGained = _totalGoldGained;
    this.totalDustGained = _totalDustGained;
    this.RefreshQuantities(type, anim);
  }

  public void PayGold(int goldCost, bool paySingle = true, bool isReroll = false)
  {
    if (!GameManager.Instance.IsMultiplayer())
      this.GivePlayer(0, -goldCost);
    else if (paySingle)
    {
      if (NetworkManager.Instance.IsMaster())
      {
        if (!isReroll)
          this.GivePlayer(0, -goldCost);
        else
          this.GivePlayer(0, -goldCost, save: true);
        this.ShareQuantities(0);
      }
      else if (!isReroll)
        this.photonView.RPC("NET_MASTERGivePlayer", RpcTarget.MasterClient, (object) 0, (object) -goldCost, (object) NetworkManager.Instance.GetPlayerNick(), (object) "", (object) true, (object) false);
      else
        this.photonView.RPC("NET_MASTERGivePlayer", RpcTarget.MasterClient, (object) 0, (object) -goldCost, (object) NetworkManager.Instance.GetPlayerNick(), (object) "", (object) true, (object) true);
    }
    else
    {
      if (!NetworkManager.Instance.IsMaster())
        return;
      int num1 = 0;
      int num2 = goldCost / 4;
      for (int index = 0; index < 4; ++index)
      {
        if (this.GetHero(index) != null && (UnityEngine.Object) this.GetHero(index).HeroData != (UnityEngine.Object) null)
        {
          int num3 = num2;
          if (num3 > this.mpPlayersGold[this.GetHero(index).Owner])
            num3 = this.mpPlayersGold[this.GetHero(index).Owner];
          this.mpPlayersGold[this.GetHero(index).Owner] -= num3;
          num1 += num3;
        }
      }
      int index1 = 0;
      while (num1 < goldCost)
      {
        if (this.mpPlayersGold[NetworkManager.Instance.PlayerPositionList[index1]] > 0)
        {
          --this.mpPlayersGold[NetworkManager.Instance.PlayerPositionList[index1]];
          ++num1;
        }
        ++index1;
        if (index1 >= this.mpPlayersGold.Count)
          index1 = 0;
      }
      this.ShareQuantities(0);
    }
  }

  public int GetTotalPlayersGold()
  {
    int totalPlayersGold = 0;
    foreach (KeyValuePair<string, int> keyValuePair in this.mpPlayersGold)
      totalPlayersGold += keyValuePair.Value;
    return totalPlayersGold;
  }

  public int GetTotalPlayersDust()
  {
    int totalPlayersDust = 0;
    foreach (KeyValuePair<string, int> keyValuePair in this.mpPlayersDust)
      totalPlayersDust += keyValuePair.Value;
    return totalPlayersDust;
  }

  public void PayDust(int dustCost, bool paySingle = true)
  {
    if (!GameManager.Instance.IsMultiplayer())
      this.GivePlayer(1, -dustCost);
    else if (paySingle)
    {
      if (NetworkManager.Instance.IsMaster())
      {
        this.GivePlayer(1, -dustCost);
        this.ShareQuantities(1);
      }
      else
        this.photonView.RPC("NET_MASTERGivePlayer", RpcTarget.MasterClient, (object) 1, (object) -dustCost, (object) NetworkManager.Instance.GetPlayerNick(), (object) "", (object) true, (object) false);
    }
    else
    {
      if (!NetworkManager.Instance.IsMaster())
        return;
      int num1 = 0;
      int num2 = num1 / 4;
      for (int index = 0; index < 4; ++index)
      {
        int num3 = num2;
        if (num3 > this.mpPlayersDust[this.GetHero(index).Owner])
          num3 = this.mpPlayersDust[this.GetHero(index).Owner];
        this.mpPlayersDust[this.GetHero(index).Owner] -= num3;
        num1 += num3;
      }
      int index1 = 0;
      while (num1 < dustCost)
      {
        if (this.mpPlayersDust[NetworkManager.Instance.PlayerPositionList[index1]] > 0)
        {
          --this.mpPlayersDust[NetworkManager.Instance.PlayerPositionList[index1]];
          ++num1;
        }
        ++index1;
        if (index1 >= this.mpPlayersDust.Count)
          index1 = 0;
      }
      this.ShareQuantities(1);
    }
  }

  private void CreateTeam() => this.teamAtO = new Hero[4];

  private void CreateTeamBackup() => this.teamAtOBackup = new Hero[4];

  private void CreateTeamNPC() => this.teamNPCAtO = new string[4];

  public void ClearTeamNPC() => this.teamNPCAtO = (string[]) null;

  public Hero[] GetTeam() => this.teamAtO;

  public Hero[] GetTeamBackup() => this.teamAtOBackup;

  public string[] GetTeamNPC() => this.teamNPCAtO;

  public Hero GetHero(int index) => index >= 0 && index < 5 && this.teamAtO != null && this.teamAtO.Length != 0 ? this.teamAtO[index] : (Hero) null;

  public int GetTeamTotalHp()
  {
    int teamTotalHp = 0;
    for (int index = 0; index < this.teamAtO.Length; ++index)
      teamTotalHp += this.teamAtO[index].GetHp();
    return teamTotalHp;
  }

  public int GetTeamTotalExperience()
  {
    int teamTotalExperience = 0;
    for (int index = 0; index < this.teamAtO.Length; ++index)
      teamTotalExperience += this.teamAtO[index].Experience;
    return teamTotalExperience;
  }

  public void SetTeamFromArray(string[] _team)
  {
    this.CreateTeam();
    for (int index = 0; index < _team.Length; ++index)
    {
      if (_team[index] != null && _team[index] != "")
      {
        this.teamAtO[index] = GameManager.Instance.CreateHero(_team[index].ToLower());
        if (this.teamAtO[index] != null && !((UnityEngine.Object) this.teamAtO[index].HeroData == (UnityEngine.Object) null) && (UnityEngine.Object) HeroSelectionManager.Instance != (UnityEngine.Object) null)
        {
          this.teamAtO[index].PerkRank = HeroSelectionManager.Instance.heroSelectionDictionary[this.teamAtO[index].SubclassName].rankTMHidden;
          this.teamAtO[index].SkinUsed = HeroSelectionManager.Instance.playerHeroSkinsDict[this.teamAtO[index].SubclassName];
          this.teamAtO[index].CardbackUsed = HeroSelectionManager.Instance.playerHeroCardbackDict[this.teamAtO[index].SubclassName];
          this.SetSkinIntoSubclassData(this.teamAtO[index].SubclassName, this.teamAtO[index].SkinUsed);
          this.teamAtO[index].RedoSubclassFromSkin();
          string subclassName = this.teamAtO[index].SubclassName;
          if (this.heroPerks.ContainsKey(subclassName))
            this.teamAtO[index].PerkList = this.heroPerks[subclassName];
          if (this.teamAtO[index].Energy < 0)
            this.teamAtO[index].Energy = 0;
          if (this.teamAtO[index].EnergyCurrent < 0)
            this.teamAtO[index].EnergyCurrent = 0;
        }
      }
    }
  }

  public void SetTeamFromTeamHero(Hero[] _team)
  {
    this.CreateTeam();
    for (int index = 0; index < _team.Length; ++index)
      this.teamAtO[index] = _team[index];
  }

  public bool TeamHaveTrait(string _id)
  {
    _id = _id.ToLower();
    for (int index1 = 0; index1 < this.teamAtO.Length; ++index1)
    {
      if (this.teamAtO[index1].Traits != null)
      {
        for (int index2 = 0; index2 < this.teamAtO[index1].Traits.Length; ++index2)
        {
          if (this.teamAtO[index1].Traits[index2] == _id)
            return true;
        }
      }
    }
    return false;
  }

  public bool CharacterHaveTrait(string _subclassId, string _id)
  {
    _id = _id.ToLower();
    for (int index1 = 0; index1 < this.teamAtO.Length; ++index1)
    {
      if (this.teamAtO[index1].SubclassName == _subclassId)
      {
        for (int index2 = 0; index2 < this.teamAtO[index1].Traits.Length; ++index2)
        {
          if (this.teamAtO[index1].Traits[index2] == _id)
            return true;
        }
      }
    }
    return false;
  }

  public bool CharacterHavePerk(string _subclassId, string _id)
  {
    _id = _id.ToLower();
    for (int index1 = 0; index1 < this.teamAtO.Length; ++index1)
    {
      if (this.teamAtO[index1].SubclassName == _subclassId && this.teamAtO[index1].PerkList != null)
      {
        for (int index2 = 0; index2 < this.teamAtO[index1].PerkList.Count; ++index2)
        {
          if (this.teamAtO[index1].PerkList[index2] == _id)
            return true;
        }
      }
    }
    return false;
  }

  public bool TeamHavePerk(string _id)
  {
    _id = _id.ToLower();
    for (int index1 = 0; index1 < this.teamAtO.Length; ++index1)
    {
      if (this.teamAtO[index1].PerkList != null)
      {
        for (int index2 = 0; index2 < this.teamAtO[index1].PerkList.Count; ++index2)
        {
          if (this.teamAtO[index1].PerkList[index2] == _id)
            return true;
        }
      }
    }
    return false;
  }

  public bool CharacterHaveItem(Character _character, string _id)
  {
    _id = _id.ToLower();
    return _character != null && _character.HaveItem(_id);
  }

  public bool TeamHaveItem(string _id, int _itemSlot = -1, bool _checkRareToo = false)
  {
    _id = _id.ToLower();
    for (int index = 0; index < this.teamAtO.Length; ++index)
    {
      if (this.teamAtO[index] != null && this.teamAtO[index].HaveItem(_id, _itemSlot, _checkRareToo))
        return true;
    }
    return false;
  }

  public AuraCurseData GlobalAuraCurseModifyDamage(
    AuraCurseData _AC,
    Enums.DamageType _DT,
    int _value,
    int _perStack,
    int _percent)
  {
    if (_AC.AuraDamageType == _DT)
    {
      _AC.AuraDamageIncreasedTotal += _value;
      _AC.AuraDamageIncreasedPerStack += (float) _perStack;
      _AC.AuraDamageIncreasedPercent += _percent;
    }
    else if (_AC.AuraDamageType2 == _DT)
    {
      _AC.AuraDamageIncreasedTotal2 += _value;
      _AC.AuraDamageIncreasedPerStack2 += (float) _perStack;
      _AC.AuraDamageIncreasedPercent2 += _percent;
    }
    else if (_AC.AuraDamageType3 == _DT)
    {
      _AC.AuraDamageIncreasedTotal3 += _value;
      _AC.AuraDamageIncreasedPerStack3 += (float) _perStack;
      _AC.AuraDamageIncreasedPercent3 += _percent;
    }
    else if (_AC.AuraDamageType4 == _DT)
    {
      _AC.AuraDamageIncreasedTotal4 += _value;
      _AC.AuraDamageIncreasedPerStack4 += (float) _perStack;
      _AC.AuraDamageIncreasedPercent4 += _percent;
    }
    else if (_AC.AuraDamageType == Enums.DamageType.None)
    {
      _AC.AuraDamageType = _DT;
      _AC.AuraDamageIncreasedTotal += _value;
      _AC.AuraDamageIncreasedPerStack += (float) _perStack;
      _AC.AuraDamageIncreasedPercent += _percent;
    }
    else if (_AC.AuraDamageType2 == Enums.DamageType.None)
    {
      _AC.AuraDamageType2 = _DT;
      _AC.AuraDamageIncreasedTotal2 += _value;
      _AC.AuraDamageIncreasedPerStack2 += (float) _perStack;
      _AC.AuraDamageIncreasedPercent2 += _percent;
    }
    else if (_AC.AuraDamageType3 == Enums.DamageType.None)
    {
      _AC.AuraDamageType3 = _DT;
      _AC.AuraDamageIncreasedTotal3 += _value;
      _AC.AuraDamageIncreasedPerStack3 += (float) _perStack;
      _AC.AuraDamageIncreasedPercent3 += _percent;
    }
    else if (_AC.AuraDamageType4 == Enums.DamageType.None)
    {
      _AC.AuraDamageType4 = _DT;
      _AC.AuraDamageIncreasedTotal4 += _value;
      _AC.AuraDamageIncreasedPerStack4 += (float) _perStack;
      _AC.AuraDamageIncreasedPercent4 += _percent;
    }
    return _AC;
  }

  public AuraCurseData GlobalAuraCurseModifyResist(
    AuraCurseData _AC,
    Enums.DamageType _DT,
    int _value,
    float _valuePerStack)
  {
    if (_AC.ResistModified == _DT)
    {
      _AC.ResistModifiedValue = _value;
      _AC.ResistModifiedPercentagePerStack += _valuePerStack;
    }
    else if (_AC.ResistModified2 == _DT)
    {
      _AC.ResistModifiedValue2 = _value;
      _AC.ResistModifiedPercentagePerStack2 += _valuePerStack;
    }
    else if (_AC.ResistModified3 == _DT)
    {
      _AC.ResistModifiedValue3 = _value;
      _AC.ResistModifiedPercentagePerStack3 += _valuePerStack;
    }
    else if (_AC.ResistModified == Enums.DamageType.None)
    {
      _AC.ResistModified = _DT;
      _AC.ResistModifiedValue = _value;
      _AC.ResistModifiedPercentagePerStack += _valuePerStack;
    }
    else if (_AC.ResistModified2 == Enums.DamageType.None)
    {
      _AC.ResistModified2 = _DT;
      _AC.ResistModifiedValue2 = _value;
      _AC.ResistModifiedPercentagePerStack2 += _valuePerStack;
    }
    else if (_AC.ResistModified3 == Enums.DamageType.None)
    {
      _AC.ResistModified3 = _DT;
      _AC.ResistModifiedValue3 = _value;
      _AC.ResistModifiedPercentagePerStack3 += _valuePerStack;
    }
    return _AC;
  }

  public void ClearCacheGlobalACModification() => this.cacheGlobalACModification.Clear();

  public AuraCurseData GlobalAuraCurseModificationByTraitsAndItems(
    string _type,
    string _acId,
    Character _characterCaster,
    Character _characterTarget)
  {
    if (_characterCaster == null && _characterTarget == null)
      return (AuraCurseData) null;
    string key = "";
    if (this.useCache)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(_type);
      stringBuilder.Append("|");
      stringBuilder.Append(_acId);
      stringBuilder.Append("|");
      if (_characterCaster != null)
        stringBuilder.Append(_characterCaster.Id);
      stringBuilder.Append("|");
      if (_characterTarget != null)
        stringBuilder.Append(_characterTarget.Id);
      key = stringBuilder.ToString();
      if (this.cacheGlobalACModification.ContainsKey(key))
        return this.cacheGlobalACModification[key];
    }
    AuraCurseData _AC = UnityEngine.Object.Instantiate<AuraCurseData>(Globals.Instance.GetAuraCurseData(_acId));
    if ((UnityEngine.Object) _AC == (UnityEngine.Object) null)
      return (AuraCurseData) null;
    bool flag1 = false;
    bool flag2 = false;
    if (_characterCaster != null && _characterCaster.IsHero)
      flag1 = _characterCaster.IsHero;
    if (_characterTarget != null && _characterTarget.IsHero)
      flag2 = true;
    switch (_acId)
    {
      case "bleed":
        switch (_type)
        {
          case "set":
            if (flag2)
            {
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkfury1c"))
              {
                _AC.Preventable = false;
                _AC.DamageWhenConsumedPerCharge = 1.5f;
              }
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkbleed2b"))
                _AC.Removable = false;
            }
            else if (this.TeamHavePerk("mainperkbleed2c"))
              _AC.Preventable = false;
            if (this.IsChallengeTraitActive("hemorrhage"))
            {
              _AC.Preventable = false;
              break;
            }
            break;
          case "consume":
            if (!flag1 && this.TeamHavePerk("mainperkbleed2c"))
            {
              _AC.ConsumedAtTurnBegin = false;
              _AC.ConsumedAtTurn = true;
            }
            if (_characterCaster != null && this.CharacterHavePerk(_characterCaster.SubclassName, "mainperkbleed2b"))
            {
              _AC.ConsumedAtTurnBegin = false;
              _AC.ConsumedAtTurn = true;
            }
            if (_characterCaster != null && this.CharacterHavePerk(_characterCaster.SubclassName, "mainperkfury1c"))
              _AC.DamageWhenConsumedPerCharge = 1.5f;
            if (this.IsChallengeTraitActive("hemorrhage"))
            {
              _AC.ConsumedAtTurnBegin = false;
              _AC.ConsumedAtTurn = true;
              break;
            }
            break;
        }
        break;
      case "bless":
        switch (_type)
        {
          case "set":
            if (flag2)
            {
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkbless1a"))
              {
                _AC.AuraDamageIncreasedPerStack = 1.5f;
                _AC.HealReceivedPerStack = 0;
              }
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkbless1b"))
                _AC.HealDonePercentPerStack = 1;
              if (this.TeamHavePerk("mainperkbless1c"))
              {
                _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Holy, 0, 0.5f);
                _AC.ConsumedAtTurn = false;
                _AC.AuraConsumed = 0;
                break;
              }
              break;
            }
            break;
          case "consume":
            if (flag1 && this.TeamHavePerk("mainperkbless1c"))
            {
              _AC.ConsumedAtTurn = false;
              _AC.AuraConsumed = 0;
              break;
            }
            break;
        }
        break;
      case "burn":
        if (_type == "set")
        {
          if (flag2)
          {
            if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkburn2b"))
            {
              _AC.ResistModifiedPercentagePerStack = -0.3f;
              _AC.CharacterStatModified = Enums.CharacterStat.Speed;
              _AC.CharacterStatChargesMultiplierNeededForOne = 8;
              _AC.CharacterStatModifiedValuePerStack = 1;
            }
          }
          else
          {
            if (_characterTarget != null && this.TeamHavePerk("mainperkburn2c"))
            {
              _AC.ResistModifiedPercentagePerStack = -0.3f;
              _AC.ResistModified2 = Enums.DamageType.Cold;
              _AC.ResistModifiedPercentagePerStack2 = -0.3f;
            }
            if (this.TeamHavePerk("mainperkburn2d"))
              _AC.DoubleDamageIfCursesLessThan = 3;
          }
          if (this.IsChallengeTraitActive("extremeburning"))
            _AC.DoubleDamageIfCursesLessThan = 3;
          if (this.IsChallengeTraitActive("frostfire"))
          {
            _AC.ResistModifiedPercentagePerStack = -0.3f;
            _AC.ResistModified2 = Enums.DamageType.Cold;
            _AC.ResistModifiedPercentagePerStack2 = -0.3f;
          }
          if (flag2 && _characterTarget != null && this.CharacterHaveTrait(_characterTarget.SubclassName, "righteousflame"))
          {
            _AC.ProduceDamageWhenConsumed = false;
            _AC.DamageWhenConsumedPerCharge = 0.0f;
            _AC.ProduceHealWhenConsumed = true;
            _AC.HealWhenConsumedPerCharge = 0.3f;
          }
        }
        if (_type == "consume")
        {
          if (!flag1)
          {
            if (this.TeamHavePerk("mainperkburn2c"))
              _AC.DamageTypeWhenConsumed = Enums.DamageType.Cold;
            if (this.TeamHavePerk("mainperkburn2d"))
              _AC.DoubleDamageIfCursesLessThan = 3;
          }
          if (this.IsChallengeTraitActive("extremeburning"))
            _AC.DoubleDamageIfCursesLessThan = 3;
          if (flag1 && _characterCaster != null && this.CharacterHaveTrait(_characterCaster.SubclassName, "righteousflame"))
          {
            _AC.ProduceDamageWhenConsumed = false;
            _AC.DamageWhenConsumedPerCharge = 0.0f;
            _AC.ProduceHealWhenConsumed = true;
            _AC.HealWhenConsumedPerCharge = 0.3f;
            break;
          }
          break;
        }
        break;
      case "chill":
        if (_type == "set")
        {
          if (!flag2)
          {
            if (this.TeamHavePerk("mainperkChill2b"))
              _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Blunt, 0, -0.3f);
            if (this.TeamHavePerk("mainperkChill2c"))
              _AC.CharacterStatChargesMultiplierNeededForOne = 4;
          }
          else if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkChill2d"))
            _AC.CharacterStatChargesMultiplierNeededForOne = 8;
          if (this.IsChallengeTraitActive("intensecold"))
          {
            _AC.CharacterStatChargesMultiplierNeededForOne = 4;
            break;
          }
          break;
        }
        break;
      case "courage":
        if (_type == "set" && flag2)
        {
          if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkCourage0"))
            _AC.Removable = false;
          if (this.TeamHavePerk("mainperkCourage1b"))
            _AC = this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Holy, 35, 0.0f), Enums.DamageType.Shadow, 35, 0.0f), Enums.DamageType.Mind, 35, 0.0f);
          if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkCourage1c"))
          {
            _AC = this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Holy, 0, 7f), Enums.DamageType.Shadow, 0, 7f), Enums.DamageType.Mind, 0, 7f);
            _AC.GainCharges = true;
            _AC.MaxCharges = _AC.MaxMadnessCharges = 8;
            break;
          }
          break;
        }
        break;
      case "dark":
        switch (_type)
        {
          case "set":
            if (!flag2)
            {
              if (this.TeamHaveItem("thedarkone", 0, true))
                _AC.ExplodeAtStacks = Globals.Instance.GetItemData("thedarkone").AuracurseCustomModValue1;
              else if (this.TeamHaveItem("blackdeck", 0, true))
                _AC.ExplodeAtStacks = Globals.Instance.GetItemData("blackdeck").AuracurseCustomModValue1;
              else if (this.TeamHaveItem("cupofdeath", 0, true))
                _AC.ExplodeAtStacks = Globals.Instance.GetItemData("cupofdeath").AuracurseCustomModValue1;
              if (this.TeamHaveTrait("putrefaction"))
                _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Shadow, 0, -1.5f);
              if (this.TeamHaveTrait("absolutedarkness"))
                _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Shadow, 0, -1f);
              if (this.TeamHavePerk("mainperkdark2c"))
              {
                _AC.DamageWhenConsumedPerCharge = 2.7f;
                break;
              }
              break;
            }
            if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkdark2b"))
            {
              _AC.ExplodeAtStacks = 38;
              break;
            }
            break;
          case "consume":
            if (!flag1)
            {
              if (this.TeamHavePerk("mainperkdark2c"))
                _AC.DamageWhenConsumedPerCharge = 2.7f;
              if (this.TeamHavePerk("mainperkdark2d"))
                _AC.ProduceDamageWhenConsumed = true;
            }
            if (this.IsChallengeTraitActive("darkestnight"))
            {
              _AC.ProduceDamageWhenConsumed = true;
              break;
            }
            break;
        }
        break;
      case "decay":
        if (_type == "set" && !flag2)
        {
          if (this.TeamHavePerk("mainperkdecay1b"))
          {
            _AC.HealReceivedPercent = -75;
            _AC.Removable = false;
          }
          if (this.TeamHavePerk("mainperkdecay1c"))
          {
            _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Shadow, 0, -8f);
            break;
          }
          break;
        }
        break;
      case "fortify":
        switch (_type)
        {
          case "set":
            if (flag2)
            {
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkfortify1a"))
              {
                _AC.AuraDamageType = Enums.DamageType.Blunt;
                _AC.AuraDamageIncreasedPerStack = 1f;
                _AC.AuraDamageType2 = Enums.DamageType.Fire;
                _AC.AuraDamageIncreasedPerStack2 = 1f;
                _AC.GainCharges = true;
                _AC.MaxCharges = _AC.MaxMadnessCharges = 50;
              }
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkfortify1b"))
              {
                _AC.Removable = false;
                break;
              }
              break;
            }
            break;
          case "consume":
            if (flag1 && this.TeamHavePerk("mainperkfortify1c"))
            {
              _AC.ConsumeAll = true;
              break;
            }
            break;
        }
        break;
      case "fury":
        switch (_type)
        {
          case "set":
            if (flag2)
            {
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkfury1b"))
              {
                _AC.AuraDamageIncreasedPercentPerStack = 1f;
                _AC.GainAuraCurseConsumption = (AuraCurseData) null;
              }
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkfury1c"))
                _AC.AuraDamageIncreasedPercentPerStack = 5f;
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkbleed2b"))
                _AC.MaxCharges = _AC.MaxMadnessCharges = 25;
            }
            if (this.IsChallengeTraitActive("containedfury"))
            {
              _AC.AuraDamageIncreasedPercentPerStack = 2f;
              _AC.GainAuraCurseConsumption = (AuraCurseData) null;
              break;
            }
            break;
          case "consume":
            if (flag1 && _characterCaster != null && this.CharacterHavePerk(_characterCaster.SubclassName, "mainperkfury1b"))
              _AC.GainAuraCurseConsumption = (AuraCurseData) null;
            if (this.IsChallengeTraitActive("containedfury"))
            {
              _AC.GainAuraCurseConsumption = (AuraCurseData) null;
              break;
            }
            break;
        }
        break;
      case "insane":
        switch (_type)
        {
          case "set":
            if (flag2)
            {
              if (this.TeamHavePerk("mainperkinsane2c"))
              {
                _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Mind, 0, 0.5f);
                _AC.AuraDamageIncreasedPercentPerStack = -0.3f;
                break;
              }
              break;
            }
            if (this.TeamHavePerk("mainperkinsane2b"))
            {
              _AC.CharacterStatModified = Enums.CharacterStat.Hp;
              _AC.CharacterStatModifiedValuePerStack = -2;
              break;
            }
            break;
        }
        break;
      case "inspire":
        if (_type == "set" && flag2)
        {
          if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkInspire0b"))
          {
            _AC.MaxCharges = _AC.MaxMadnessCharges = 1;
            _AC.CardsDrawPerStack = 2;
          }
          if (this.TeamHaveItem("goldenlaurel", 3, true))
          {
            _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.All, 0, (float) Globals.Instance.GetItemData("goldenlaurel").AuracurseCustomModValue1);
            _AC.HealReceivedPercentPerStack = Globals.Instance.GetItemData("goldenlaurel").AuracurseCustomModValue2;
            break;
          }
          break;
        }
        break;
      case "insulate":
        if (_type == "set" && flag2)
        {
          if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkInsulate0"))
            _AC.Removable = false;
          if (this.TeamHavePerk("mainperkInsulate1b"))
            _AC = this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Fire, 35, 0.0f), Enums.DamageType.Cold, 35, 0.0f), Enums.DamageType.Blunt, 35, 0.0f);
          if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkInsulate1c"))
          {
            _AC = this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Fire, 0, 7f), Enums.DamageType.Cold, 0, 7f), Enums.DamageType.Lightning, 0, 7f);
            _AC.GainCharges = true;
            _AC.MaxCharges = _AC.MaxMadnessCharges = 8;
            break;
          }
          break;
        }
        break;
      case "mark":
        switch (_type)
        {
          case "set":
            if (!flag2)
            {
              if (this.TeamHavePerk("mainperkmark1b"))
              {
                _AC.ConsumedAtTurn = false;
                _AC.AuraConsumed = 0;
              }
              if (this.TeamHavePerk("mainperkmark1c"))
              {
                _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Slashing, 0, -0.3f);
                break;
              }
              break;
            }
            break;
          case "consume":
            if (!flag1 && this.TeamHavePerk("mainperkmark1b"))
            {
              _AC.ConsumedAtTurn = false;
              _AC.AuraConsumed = 0;
              break;
            }
            break;
        }
        break;
      case "poison":
        switch (_type)
        {
          case "set":
            if (!flag2)
            {
              if (this.TeamHavePerk("mainperkpoison2b"))
                _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Shadow, 0, -0.3f);
              if (this.TeamHavePerk("mainperkpoison2c"))
                _AC.DamageWhenConsumedPerCharge = 1.5f;
            }
            if (this.IsChallengeTraitActive("lethalpoison"))
            {
              _AC.DamageWhenConsumedPerCharge = 1.5f;
              break;
            }
            break;
          case "consume":
            if (!flag1 && this.TeamHavePerk("mainperkpoison2c"))
            {
              _AC.ConsumeAll = true;
              _AC.DamageWhenConsumedPerCharge = 1.5f;
            }
            if (this.IsChallengeTraitActive("lethalpoison"))
            {
              _AC.ConsumeAll = true;
              _AC.DamageWhenConsumedPerCharge = 1.5f;
              break;
            }
            break;
        }
        break;
      case "powerful":
        switch (_type)
        {
          case "set":
            if (flag2)
            {
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkpowerful1b"))
              {
                _AC.MaxCharges += 2;
                _AC.MaxMadnessCharges += 2;
                _AC.AuraConsumed = 1;
              }
              if (this.TeamHaveItem("powercoilrare", 2))
              {
                _AC.MaxCharges += Globals.Instance.GetItemData("powercoilrare").AuracurseCustomModValue1;
                _AC.MaxMadnessCharges += Globals.Instance.GetItemData("powercoilrare").AuracurseCustomModValue1;
              }
              else if (this.TeamHaveItem("powercoil", 2))
              {
                _AC.MaxCharges += Globals.Instance.GetItemData("powercoil").AuracurseCustomModValue1;
                _AC.MaxMadnessCharges += Globals.Instance.GetItemData("powercoil").AuracurseCustomModValue1;
              }
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkpowerful1c"))
              {
                _AC.AuraDamageIncreasedPercentPerStack = 10f;
                _AC.HealDonePercentPerStack = 10;
                _AC.MaxCharges -= 3;
                _AC.MaxMadnessCharges -= 3;
                _AC.ConsumeAll = true;
                break;
              }
              break;
            }
            break;
          case "consume":
            if (flag1)
            {
              if (_characterCaster != null && this.CharacterHavePerk(_characterCaster.SubclassName, "mainperkpowerful1b"))
                _AC.AuraConsumed = 1;
              if (_characterCaster != null && this.CharacterHavePerk(_characterCaster.SubclassName, "mainperkpowerful1c"))
              {
                _AC.ConsumeAll = true;
                break;
              }
              break;
            }
            break;
        }
        break;
      case "regeneration":
        switch (_type)
        {
          case "set":
            if (flag2)
            {
              if (this.TeamHavePerk("mainperkregeneration1b"))
                _AC.HealReceivedPercentPerStack = 1;
              if (this.TeamHavePerk("mainperkregeneration1c"))
              {
                _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Shadow, 0, 0.5f);
                _AC.AuraConsumed = 0;
                break;
              }
              break;
            }
            break;
          case "consume":
            if (flag1)
            {
              if (_characterCaster != null && this.CharacterHavePerk(_characterCaster.SubclassName, "mainperkregeneration1a"))
              {
                _AC.HealSidesWhenConsumed = _AC.HealWhenConsumed;
                _AC.HealSidesWhenConsumedPerCharge = _AC.HealWhenConsumedPerCharge;
              }
              if (this.TeamHavePerk("mainperkregeneration1c"))
              {
                _AC.AuraConsumed = 0;
                break;
              }
              break;
            }
            break;
        }
        break;
      case "reinforce":
        if (_type == "set" && flag2)
        {
          if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkReinforce0"))
            _AC.Removable = false;
          if (this.TeamHavePerk("mainperkReinforce1b"))
            _AC = this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Slashing, 35, 0.0f), Enums.DamageType.Piercing, 35, 0.0f), Enums.DamageType.Blunt, 35, 0.0f);
          if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkReinforce1c"))
          {
            _AC = this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Slashing, 0, 7f), Enums.DamageType.Piercing, 0, 7f), Enums.DamageType.Blunt, 0, 7f);
            _AC.GainCharges = true;
            _AC.MaxCharges = _AC.MaxMadnessCharges = 8;
            break;
          }
          break;
        }
        break;
      case "sanctify":
        if (_type == "set" && !flag2 && this.TeamHavePerk("mainperkSanctify2b"))
        {
          _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Holy, 0, -0.5f);
          break;
        }
        break;
      case "scourge":
        if (_type == "set" && !flag2)
        {
          if (this.TeamHaveTrait("auraofdespair"))
            _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.All, 0, -5f);
          if (this.TeamHaveTrait("unholyblight"))
          {
            _AC.GainCharges = true;
            break;
          }
          break;
        }
        break;
      case "sharp":
        switch (_type)
        {
          case "set":
            if (flag2)
            {
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkSharp1a"))
              {
                _AC.AuraDamageIncreasedPerStack = 1.5f;
                _AC.AuraDamageType2 = Enums.DamageType.None;
                _AC.AuraDamageIncreasedPerStack2 = 0.0f;
              }
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkSharp1b"))
              {
                _AC.AuraDamageType = Enums.DamageType.Piercing;
                _AC.AuraDamageIncreasedPerStack = 1.5f;
                _AC.AuraDamageType2 = Enums.DamageType.None;
                _AC.AuraDamageIncreasedPerStack2 = 0.0f;
              }
              if (this.TeamHavePerk("mainperkSharp1c"))
              {
                _AC.ConsumedAtTurn = false;
                _AC.AuraConsumed = 0;
                _AC.Removable = false;
              }
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkSharp1d"))
                _AC = this.GlobalAuraCurseModifyDamage(_AC, Enums.DamageType.Shadow, 0, 1, 0);
              if (this.TeamHaveTrait("shrilltone"))
              {
                _AC = this.GlobalAuraCurseModifyDamage(_AC, Enums.DamageType.Mind, 0, 1, 0);
                break;
              }
              break;
            }
            break;
          case "consume":
            if (flag1 && this.TeamHavePerk("mainperkSharp1c"))
            {
              _AC.ConsumedAtTurn = false;
              _AC.AuraConsumed = 0;
              break;
            }
            break;
        }
        break;
      case "sight":
        switch (_type)
        {
          case "set":
            if (!flag2)
            {
              if (this.TeamHavePerk("mainperksight1b"))
                _AC.Removable = false;
              if (this.TeamHavePerk("mainperksight1c"))
                _AC.Preventable = false;
              if (this.TeamHaveTrait("keensight"))
              {
                _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Piercing, 0, -0.3f);
                break;
              }
              break;
            }
            break;
          case "consume":
            if (!flag1 && this.TeamHavePerk("mainperksight1c"))
            {
              _AC.ConsumeAll = true;
              break;
            }
            break;
        }
        break;
      case "spark":
        switch (_type)
        {
          case "set":
            if (!flag1 && this.TeamHavePerk("mainperkspark1b"))
            {
              _AC.DamageWhenConsumedPerCharge = 1f;
              break;
            }
            break;
          case "consume":
            if (!flag1 && this.TeamHaveTrait("voltaicarc"))
            {
              _AC.DamageWhenConsumedPerCharge = 1f;
              break;
            }
            break;
        }
        break;
      case "stanzai":
        switch (_type)
        {
          case "set":
            if (flag2 && _characterTarget != null && this.TeamHavePerk("mainperkstanza0a"))
            {
              _AC.AuraDamageType = Enums.DamageType.All;
              break;
            }
            break;
          case "consume":
            if (flag1 && _characterCaster != null && this.CharacterHavePerk(_characterCaster.SubclassName, "mainperkstanza0b"))
            {
              _AC.GainAuraCurseConsumption2 = Globals.Instance.GetAuraCurseData("inspire");
              _AC.GainAuraCurseConsumptionPerCharge2 = 1;
              break;
            }
            break;
        }
        break;
      case "stanzaii":
        if (_type == "set" && flag2 && _characterTarget != null && this.TeamHavePerk("mainperkstanza0a"))
        {
          _AC.AuraDamageType = Enums.DamageType.All;
          break;
        }
        break;
      case "stanzaiii":
        switch (_type)
        {
          case "set":
            if (flag2 && _characterTarget != null && this.TeamHavePerk("mainperkstanza0a"))
            {
              _AC.AuraDamageType = Enums.DamageType.All;
              break;
            }
            break;
          case "consume":
            if (flag1 && this.TeamHaveTrait("choir"))
            {
              _AC.ConsumedAtTurn = false;
              _AC.AuraConsumed = 0;
              break;
            }
            break;
        }
        break;
      case "stealth":
        switch (_type)
        {
          case "set":
            if (flag2)
            {
              if (this.TeamHavePerk("mainperkstealth1a"))
              {
                _AC.AuraDamageIncreasedPercentPerStack = 25f;
                _AC.HealDonePercentPerStack = 25;
              }
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkstealth1b"))
              {
                _AC.ConsumedAtTurnBegin = false;
                _AC.AuraConsumed = 0;
              }
              if (this.TeamHavePerk("mainperkstealth1c"))
              {
                _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.All, 0, 5f);
                break;
              }
              break;
            }
            break;
          case "consume":
            if (flag1 && _characterCaster != null && this.CharacterHavePerk(_characterCaster.SubclassName, "mainperkstealth1b"))
            {
              _AC.ConsumedAtTurnBegin = false;
              _AC.AuraConsumed = 0;
              break;
            }
            break;
        }
        break;
      case "stealthbonus":
        if (_type == "set" && flag2 && this.TeamHavePerk("mainperkstealth1a"))
        {
          _AC.AuraDamageIncreasedPercentPerStack = 25f;
          _AC.HealDonePercentPerStack = 25;
          break;
        }
        break;
      case "taunt":
        if (_type == "set" && flag2)
        {
          if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkTaunt1b"))
            _AC = this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Slashing, 0, 10f), Enums.DamageType.Piercing, 0, 10f), Enums.DamageType.Blunt, 0, 10f);
          if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkTaunt1c"))
            _AC = this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Fire, 0, 10f), Enums.DamageType.Cold, 0, 10f), Enums.DamageType.Lightning, 0, 10f);
          if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkTaunt1d"))
          {
            _AC = this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Mind, 0, 10f), Enums.DamageType.Holy, 0, 10f), Enums.DamageType.Shadow, 0, 10f);
            break;
          }
          break;
        }
        break;
      case "thorns":
        if (_type == "set")
        {
          if (flag2)
          {
            if (this.TeamHavePerk("mainperkthorns1a"))
              _AC.DamageReflectedConsumeCharges = 0;
            if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkthorns1b"))
              _AC.DamageReflectedType = Enums.DamageType.Holy;
          }
          if (this.IsChallengeTraitActive("sacredthorns"))
          {
            _AC.DamageReflectedType = Enums.DamageType.Holy;
            break;
          }
          break;
        }
        break;
      case "vitality":
        switch (_type)
        {
          case "set":
            if (flag2)
            {
              if (_characterTarget != null && this.CharacterHavePerk(_characterTarget.SubclassName, "mainperkvitality1a"))
                _AC.CharacterStatModifiedValuePerStack = 8;
              if (this.TeamHavePerk("mainperkvitality1c"))
              {
                _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Mind, 0, 0.5f);
                _AC.ConsumedAtTurnBegin = false;
                _AC.AuraConsumed = 0;
                break;
              }
              break;
            }
            break;
          case "consume":
            if (flag1 && this.TeamHavePerk("mainperkvitality1c"))
            {
              _AC.ConsumedAtTurnBegin = false;
              _AC.AuraConsumed = 0;
              break;
            }
            break;
        }
        break;
      case "vulnerable":
        switch (_type)
        {
          case "set":
            if (!flag2)
            {
              if (MadnessManager.Instance.IsMadnessTraitActive("resistantmonsters"))
                _AC.MaxCharges = _AC.MaxMadnessCharges = 6;
              if (this.TeamHavePerk("mainperkVulnerable0b"))
              {
                ++_AC.MaxCharges;
                ++_AC.MaxMadnessCharges;
                _AC.AuraConsumed = 1;
              }
              if (this.TeamHaveItem("nullifierrare", 3))
              {
                _AC.MaxCharges += Globals.Instance.GetItemData("nullifierrare").AuracurseCustomModValue1;
                _AC.MaxMadnessCharges += Globals.Instance.GetItemData("nullifierrare").AuracurseCustomModValue1;
              }
              else if (this.TeamHaveItem("nullifier", 3))
              {
                _AC.MaxCharges += Globals.Instance.GetItemData("nullifier").AuracurseCustomModValue1;
                _AC.MaxMadnessCharges += Globals.Instance.GetItemData("nullifier").AuracurseCustomModValue1;
              }
              if (this.TeamHavePerk("mainperkVulnerable0c"))
              {
                _AC.ResistModified = Enums.DamageType.None;
                _AC.ResistModifiedValue = 0;
                _AC.ResistModifiedPercentagePerStack = 0.0f;
                _AC = this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Slashing, 0, -8f), Enums.DamageType.Piercing, 0, -8f), Enums.DamageType.Blunt, 0, -8f);
                break;
              }
              break;
            }
            break;
          case "consume":
            if (!flag1 && this.TeamHavePerk("mainperkVulnerable0b"))
            {
              _AC.AuraConsumed = 1;
              break;
            }
            break;
        }
        break;
      case "wet":
        switch (_type)
        {
          case "set":
            if (!flag2)
            {
              if (this.TeamHavePerk("mainperkwet1a"))
              {
                _AC.IncreasedDamageReceivedType2 = Enums.DamageType.Cold;
                _AC.IncreasedDirectDamageReceivedPerStack2 = 1;
              }
              if (this.TeamHavePerk("mainperkwet1b"))
              {
                _AC = this.GlobalAuraCurseModifyResist(_AC, Enums.DamageType.Lightning, 0, -1f);
                _AC.AuraConsumed = 0;
              }
            }
            else if (this.TeamHavePerk("mainperkwet1c"))
            {
              _AC.PreventedAuraCurseStackPerStack = 3;
              _AC.AuraConsumed = 0;
            }
            if (this.IsChallengeTraitActive("icydeluge"))
            {
              _AC.IncreasedDamageReceivedType2 = Enums.DamageType.Cold;
              _AC.IncreasedDirectDamageReceivedPerStack2 = 1;
              break;
            }
            break;
          case "consume":
            if (!flag1)
            {
              if (this.TeamHavePerk("mainperkwet1b"))
              {
                _AC.AuraConsumed = 0;
                break;
              }
              break;
            }
            if (this.TeamHavePerk("mainperkwet1c"))
            {
              _AC.AuraConsumed = 0;
              break;
            }
            break;
        }
        break;
      case "zeal":
        switch (_type)
        {
          case "set":
            if (flag2)
            {
              if (_characterTarget != null && this.CharacterHaveTrait(_characterTarget.SubclassName, "zealotry"))
              {
                _AC.AuraDamageIncreasedPercentPerStack = 1.5f;
                _AC.ConsumeAll = false;
                _AC.AuraConsumed = 2;
              }
              if (_characterTarget != null && this.CharacterHaveTrait(_characterTarget.SubclassName, "righteousflame"))
              {
                _AC.AuraDamageType2 = Enums.DamageType.All;
                _AC.AuraDamageIncreasedPercentPerStack2 = 7f;
                break;
              }
              break;
            }
            break;
          case "consume":
            if (flag1 && _characterCaster != null && this.CharacterHaveTrait(_characterCaster.SubclassName, "zealotry"))
            {
              _AC.ConsumeAll = false;
              _AC.AuraConsumed = 2;
              break;
            }
            break;
        }
        break;
    }
    if (this.useCache)
    {
      if (this.cacheGlobalACModification.ContainsKey(key))
        this.cacheGlobalACModification[key] = _AC;
      else
        this.cacheGlobalACModification.Add(key, _AC);
    }
    return _AC;
  }

  public bool ValidCardback(CardbackData cbd, int rankProgressN)
  {
    bool flag = false;
    if (cbd.Sku != "" && SteamManager.Instance.PlayerHaveDLC(cbd.Sku))
      flag = true;
    else if (cbd.SteamStat != "" && SteamManager.Instance.GetStatInt(cbd.SteamStat) == 1)
      flag = true;
    else if (cbd.RankLevel > 0)
    {
      if (cbd.RankLevel <= rankProgressN)
        flag = true;
    }
    else if (cbd.AdventureLevel > 0)
    {
      if (cbd.AdventureLevel == 1 && PlayerManager.Instance.NgLevel > 0)
        flag = true;
      else if (cbd.AdventureLevel <= PlayerManager.Instance.MaxAdventureMadnessLevel)
        flag = true;
    }
    else if (cbd.ObeliskLevel > 0)
    {
      if (cbd.ObeliskLevel == 1 && PlayerManager.Instance.ObeliskMadnessLevel > 0)
        flag = true;
      else if (cbd.ObeliskLevel < PlayerManager.Instance.ObeliskMadnessLevel)
        flag = true;
    }
    else
      flag = !cbd.Locked || PlayerManager.Instance.IsCardbackUnlocked(cbd.CardbackId);
    return flag;
  }

  public void RedoSkins()
  {
    for (int index = 0; index < this.teamAtO.Length; ++index)
    {
      if (this.teamAtO[index] != null)
      {
        if (this.teamAtO[index].SkinUsed != null && this.teamAtO[index].SkinUsed != "")
          this.SetSkinIntoSubclassData(this.teamAtO[index].SubclassName, this.teamAtO[index].SkinUsed);
        else
          this.SetSkinIntoSubclassData(this.teamAtO[index].SubclassName, Globals.Instance.GetSkinBaseIdBySubclass(this.teamAtO[index].SubclassName));
        this.teamAtO[index].RedoSubclassFromSkin();
      }
    }
  }

  public void AssignTeamFromSaveGame(Hero[] _team)
  {
    this.CreateTeam();
    for (int index = 0; index < _team.Length; ++index)
      this.teamAtO[index] = GameManager.Instance.AssignDataToHero(_team[index]);
  }

  public void AssignTeamBackupFromSaveGame(Hero[] _team)
  {
    this.CreateTeamBackup();
    if (_team == null || _team.Length != 4)
      return;
    for (int index = 0; index < _team.Length; ++index)
      this.teamAtOBackup[index] = GameManager.Instance.AssignDataToHero(_team[index]);
  }

  public void SetTeam(Hero[] _team)
  {
    this.CreateTeam();
    for (int index = 0; index < _team.Length; ++index)
    {
      if (_team[index] != null)
        this.teamAtO[index] = _team[index];
    }
  }

  public void SetTeamNPC(string[] _team)
  {
    this.teamNPCAtO = new string[4];
    for (int index = 0; index < _team.Length; ++index)
    {
      if (_team[index] != null)
        this.teamNPCAtO[index] = _team[index];
    }
  }

  public void SetTeamSingle(Hero _hero, int position)
  {
    if (this.teamAtO == null || this.teamAtO.Length == 0)
      this.CreateTeam();
    this.teamAtO[position] = _hero;
  }

  public void SetTeamNPCSingle(string _npc, int position)
  {
    if (this.teamNPCAtO == null || this.teamNPCAtO.Length == 0)
      this.CreateTeamNPC();
    this.teamNPCAtO[position] = _npc;
  }

  public void SetTeamNPCFromCombatData(CombatData _combatData)
  {
    bool flag1 = false;
    if (MadnessManager.Instance.IsMadnessTraitActive("randomcombats") || GameManager.Instance.IsObeliskChallenge())
      flag1 = true;
    bool flag2 = false;
    NodeData nodeData = Globals.Instance.GetNodeData(this.currentMapNode);
    if ((UnityEngine.Object) nodeData != (UnityEngine.Object) null && nodeData.NodeCombatTier != Enums.CombatTier.T0)
      flag2 = true;
    if ((UnityEngine.Object) nodeData != (UnityEngine.Object) null && nodeData.DisableRandom)
      flag2 = false;
    if (flag2 && (_combatData.CombatId == "eaqua_37a" || _combatData.CombatId == "eaqua_37b"))
      flag2 = false;
    if (flag1 & flag2 && (UnityEngine.Object) _combatData != (UnityEngine.Object) null)
    {
      int deterministicHashCode = (this.currentMapNode + this.GetGameId() + _combatData.CombatId).GetDeterministicHashCode();
      NPCData[] randomCombat = Functions.GetRandomCombat(nodeData.NodeCombatTier, deterministicHashCode, this.currentMapNode);
      for (int position = 0; position < 4; ++position)
      {
        if (position < randomCombat.Length && (UnityEngine.Object) randomCombat[position] != (UnityEngine.Object) null)
          this.SetTeamNPCSingle(randomCombat[position].Id, position);
        else
          this.SetTeamNPCSingle("", position);
      }
    }
    else
    {
      for (int position = 0; position < 4; ++position)
      {
        if (position < _combatData.NPCList.Length && (UnityEngine.Object) _combatData.NPCList[position] != (UnityEngine.Object) null)
          this.SetTeamNPCSingle(_combatData.NPCList[position].Id, position);
        else
          this.SetTeamNPCSingle("", position);
      }
    }
  }

  public void SetObeliskNodes()
  {
    this.obeliskLow = "";
    this.obeliskHigh = "";
    this.obeliskFinal = "";
    List<string> stringList = new List<string>((IEnumerable<string>) Globals.Instance.ZoneDataSource.Keys);
    for (int index = stringList.Count - 1; index >= 0; --index)
    {
      if (stringList[index] == "pyramid" || stringList[index] == "ulminin")
        stringList.RemoveAt(index);
    }
    UnityEngine.Random.InitState(this.GetGameId().GetDeterministicHashCode());
    bool flag = false;
    int num = -1;
    while (!flag)
    {
      int index = UnityEngine.Random.Range(0, stringList.Count);
      string key = stringList[index];
      if (this.obeliskLow == "")
      {
        if (Globals.Instance.ZoneDataSource[key].ObeliskLow)
        {
          this.obeliskLow = Globals.Instance.ZoneDataSource[key].ZoneId.ToLower();
          num = index;
        }
      }
      else if (this.obeliskHigh == "")
      {
        if (index != num)
        {
          if (Globals.Instance.ZoneDataSource[key].ObeliskHigh)
            this.obeliskHigh = Globals.Instance.ZoneDataSource[key].ZoneId.ToLower();
        }
        else
          continue;
      }
      else if (this.obeliskFinal == "" && Globals.Instance.ZoneDataSource[key].ObeliskFinal)
        this.obeliskFinal = Globals.Instance.ZoneDataSource[key].ZoneId.ToLower();
      if (this.obeliskLow != "" && this.obeliskHigh != "" && this.obeliskFinal != "")
        flag = true;
    }
    if (!((UnityEngine.Object) MapManager.Instance != (UnityEngine.Object) null))
      return;
    MapManager.Instance.IncludeMapPrefab(this.obeliskLow + "_0");
    MapManager.Instance.IncludeMapPrefab(this.obeliskHigh + "_0");
    MapManager.Instance.IncludeMapPrefab(this.obeliskFinal + "_0");
    MapManager.Instance.IncludeObeliskBgs();
  }

  public void GenerateObeliskMap()
  {
    List<string> stringList1 = new List<string>();
    int deterministicHashCode = this.GetGameId().GetDeterministicHashCode();
    UnityEngine.Random.InitState(deterministicHashCode);
    this.SetObeliskBosses();
    List<string> ts1 = new List<string>();
    List<string> ts2 = new List<string>();
    List<string> ts3 = new List<string>();
    List<string> ts4 = new List<string>();
    List<EventData> eventDataList = new List<EventData>();
    foreach (KeyValuePair<string, EventData> keyValuePair in Globals.Instance.Events)
    {
      if (keyValuePair.Value.EventTier == Enums.CombatTier.T1)
        ts1.Add(keyValuePair.Value.EventId);
      else if (keyValuePair.Value.EventTier == Enums.CombatTier.T2)
        ts2.Add(keyValuePair.Value.EventId);
      else if (keyValuePair.Value.EventTier == Enums.CombatTier.T3)
        ts3.Add(keyValuePair.Value.EventId);
      else if (keyValuePair.Value.EventTier == Enums.CombatTier.T4)
        ts4.Add(keyValuePair.Value.EventId);
      else if (keyValuePair.Value.EventTier == Enums.CombatTier.T5)
        eventDataList.Add(keyValuePair.Value);
    }
    UnityEngine.Random.InitState(deterministicHashCode);
    List<string> stringList2 = ts1.ShuffleList<string>();
    List<string> stringList3 = ts2.ShuffleList<string>();
    List<string> stringList4 = ts3.ShuffleList<string>();
    List<string> stringList5 = ts4.ShuffleList<string>();
    List<NodeData> ts5 = new List<NodeData>();
    List<NodeData> nodeDataList1 = new List<NodeData>();
    List<NodeData> nodeDataList2 = new List<NodeData>();
    foreach (KeyValuePair<string, string> keyValuePair in this.gameNodeAssigned)
    {
      NodeData nodeData = Globals.Instance.GetNodeData(keyValuePair.Key);
      if (!((UnityEngine.Object) nodeData == (UnityEngine.Object) null) && nodeData.NodeEvent != null && (nodeData.NodeZone.ZoneId.ToLower() == this.obeliskLow || nodeData.NodeZone.ZoneId.ToLower() == this.obeliskHigh || nodeData.NodeZone.ZoneId.ToLower() == this.obeliskFinal))
        ts5.Add(nodeData);
    }
    List<NodeData> nodeDataList3 = ts5.ShuffleList<NodeData>();
    for (int index = 0; index < nodeDataList3.Count; ++index)
    {
      if (!(nodeDataList3[index].NodeZone.ZoneId.ToLower() == this.obeliskHigh))
        nodeDataList2.Add(nodeDataList3[index]);
    }
    int index1 = 0;
    while (index1 < nodeDataList3.Count)
    {
      NodeData nodeData = nodeDataList3[index1];
      Enums.CombatTier nodeEventTier = nodeData.NodeEventTier;
      EventData eventData = (EventData) null;
      switch (nodeEventTier)
      {
        case Enums.CombatTier.T1:
          if (stringList2.Count > 0)
          {
            eventData = Globals.Instance.GetEventData(stringList2[0]);
            stringList2.RemoveAt(0);
            break;
          }
          break;
        case Enums.CombatTier.T2:
          if (stringList3.Count > 0)
          {
            eventData = Globals.Instance.GetEventData(stringList3[0]);
            stringList3.RemoveAt(0);
            break;
          }
          if (stringList2.Count > 0)
          {
            eventData = Globals.Instance.GetEventData(stringList2[0]);
            stringList2.RemoveAt(0);
            break;
          }
          break;
        case Enums.CombatTier.T3:
          if (stringList5.Count > 0)
          {
            eventData = Globals.Instance.GetEventData(stringList5[0]);
            stringList5.RemoveAt(0);
            break;
          }
          if (stringList4.Count > 0)
          {
            eventData = Globals.Instance.GetEventData(stringList4[0]);
            stringList4.RemoveAt(0);
            break;
          }
          break;
      }
      if ((UnityEngine.Object) eventData != (UnityEngine.Object) null)
      {
        if (eventData.EventUniqueId == "" || !stringList1.Contains(eventData.EventUniqueId))
        {
          this.gameNodeAssigned[nodeData.NodeId] = "event:" + eventData.EventId;
          if ((eventData.EventTier == Enums.CombatTier.T3 || eventData.EventTier == Enums.CombatTier.T4) && (this.mapVisitedNodes == null || !this.mapVisitedNodes.Contains(nodeData.NodeId)))
            nodeDataList1.Add(nodeData);
          if (eventData.EventUniqueId != "")
            stringList1.Add(eventData.EventUniqueId);
          ++index1;
        }
        else if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("Unique Id already used: " + eventData.EventUniqueId, "ocmap");
      }
      else
      {
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("Event data is null", "ocmap");
        ++index1;
      }
    }
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("------- requirements -------", "ocmap");
    int index2 = 0;
    for (int index3 = 0; index3 < this.playerRequeriments.Count; ++index3)
    {
      for (int index4 = 0; index4 < eventDataList.Count; ++index4)
      {
        if ((UnityEngine.Object) eventDataList[index4] != (UnityEngine.Object) null && (UnityEngine.Object) eventDataList[index4].Requirement != (UnityEngine.Object) null && eventDataList[index4].Requirement.RequirementId == this.playerRequeriments[index3] && nodeDataList1 != null && nodeDataList1.Count > 0)
        {
          this.gameNodeAssigned[nodeDataList1[nodeDataList1.Count - 1].NodeId] = "event:" + eventDataList[index4].EventId;
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD("Node replaced " + nodeDataList1[index2].NodeId + " -> event:" + eventDataList[index4].EventId, "ocmap");
          nodeDataList1.RemoveAt(nodeDataList1.Count - 1);
        }
      }
    }
    for (int index5 = 0; index5 < this.mapVisitedNodesAction.Count; ++index5)
    {
      string[] strArray = this.mapVisitedNodesAction[index5].Split('|', StringSplitOptions.None);
      if (strArray != null && strArray[0] != null && strArray[1] != null && (UnityEngine.Object) Globals.Instance.GetEventData(strArray[1]) != (UnityEngine.Object) null)
        this.gameNodeAssigned[strArray[0]] = "event:" + strArray[1];
    }
  }

  public void SetObeliskBosses()
  {
    this.mapNodeObeliskBoss.Clear();
    int num = 1;
    if (this.IsChallengeTraitActive("morechampions"))
      num = 2;
    List<string> stringList1 = new List<string>();
    for (int index1 = 0; index1 < num; ++index1)
    {
      for (int index2 = 0; index2 < 11; ++index2)
      {
        Enums.CombatTier combatTier = Enums.CombatTier.T0;
        if (index2 == 0)
          combatTier = Enums.CombatTier.T1;
        else if (index2 == 1)
          combatTier = Enums.CombatTier.T2;
        else if (index2 == 2)
          combatTier = Enums.CombatTier.T3;
        else if (index2 == 3)
          combatTier = Enums.CombatTier.T4;
        else if (index2 == 4)
          combatTier = Enums.CombatTier.T5;
        else if (index2 == 5)
          combatTier = Enums.CombatTier.T6;
        else if (index2 == 6)
          combatTier = Enums.CombatTier.T7;
        else if (index2 == 7)
          combatTier = Enums.CombatTier.T8;
        else if (index2 == 8)
          combatTier = Enums.CombatTier.T9;
        else if (index2 == 9)
          combatTier = Enums.CombatTier.T11;
        else if (index2 == 10)
          combatTier = Enums.CombatTier.T12;
        if (combatTier != Enums.CombatTier.T0)
        {
          List<string> stringList2 = new List<string>();
          foreach (KeyValuePair<string, string> keyValuePair in this.gameNodeAssigned)
          {
            NodeData nodeData = Globals.Instance.GetNodeData(keyValuePair.Key);
            if (nodeData.NodeCombatTier == combatTier && (nodeData.NodeZone.ZoneId.ToLower() == this.obeliskLow || nodeData.NodeZone.ZoneId.ToLower() == this.obeliskHigh || nodeData.NodeZone.ZoneId.ToLower() == this.obeliskFinal) && !stringList1.Contains(keyValuePair.Key))
              stringList2.Add(keyValuePair.Key);
          }
          if (stringList2.Count > 0)
          {
            string str = stringList2[UnityEngine.Random.Range(0, stringList2.Count)];
            if (!stringList1.Contains(str))
              stringList1.Add(str);
            switch (combatTier)
            {
              case Enums.CombatTier.T8:
                this.mapNodeObeliskBoss.Add(str);
                continue;
              case Enums.CombatTier.T9:
                this.mapNodeObeliskBoss.Add(str);
                continue;
              default:
                this.mapNodeObeliskBoss.Add(str);
                continue;
            }
          }
        }
      }
    }
  }

  public bool NodeHaveBossRare(string _nodeId) => this.mapNodeObeliskBoss.Contains(_nodeId);

  public void ReplaceCardInDeck(int heroIndex, int cardIndex, string cardId)
  {
    if (cardIndex >= this.teamAtO[heroIndex].Cards.Count)
      return;
    this.teamAtO[heroIndex].Cards[cardIndex] = cardId;
    if (!GameManager.Instance.IsMultiplayer())
      return;
    this.photonView.RPC("NET_ReplaceCardInDeck", RpcTarget.Others, (object) heroIndex, (object) cardIndex, (object) cardId);
  }

  [PunRPC]
  private void NET_ReplaceCardInDeck(int heroIndex, int cardIndex, string cardId)
  {
    if (cardIndex < this.teamAtO[heroIndex].Cards.Count)
      this.teamAtO[heroIndex].Cards[cardIndex] = cardId;
    this.SideBarRefreshCards(heroIndex);
  }

  public void RemoveCardInDeck(int heroIndex, int cardIndex)
  {
    if (cardIndex >= this.teamAtO[heroIndex].Cards.Count)
      return;
    this.teamAtO[heroIndex].Cards.RemoveAt(cardIndex);
    if (!GameManager.Instance.IsMultiplayer())
      return;
    this.photonView.RPC("NET_RemoveCardInDeck", RpcTarget.Others, (object) heroIndex, (object) cardIndex);
  }

  [PunRPC]
  private void NET_RemoveCardInDeck(int heroIndex, int cardIndex)
  {
    if (cardIndex < this.teamAtO[heroIndex].Cards.Count)
      this.teamAtO[heroIndex].Cards.RemoveAt(cardIndex);
    this.SideBarRefreshCards(heroIndex);
  }

  public IEnumerator ShareTeam(string sceneToLoad = "", bool showMask = true, bool setOwners = false)
  {
    if (NetworkManager.Instance.IsMaster())
    {
      for (int index = 0; index < 4; ++index)
      {
        if (this.teamAtO[index] != null && !((UnityEngine.Object) this.teamAtO[index].HeroData == (UnityEngine.Object) null))
        {
          if (NetworkManager.Instance.PlayerHeroPositionOwner[index] != "" && NetworkManager.Instance.PlayerHeroPositionOwner[index] != null)
            this.teamAtO[index].AssignOwner(NetworkManager.Instance.PlayerHeroPositionOwner[index]);
          if (this.teamAtO[index].HpCurrent <= 0)
            this.teamAtO[index].HpCurrent = 1;
          if (this.teamAtOBackup != null && index < this.teamAtOBackup.Length && this.teamAtOBackup[index] != null && (UnityEngine.Object) this.teamAtOBackup[index].HeroData != (UnityEngine.Object) null && NetworkManager.Instance.PlayerHeroPositionOwner[index] != "" && NetworkManager.Instance.PlayerHeroPositionOwner[index] != null)
            this.teamAtOBackup[index].AssignOwner(NetworkManager.Instance.PlayerHeroPositionOwner[index]);
          if (this.heroPerks != null && this.heroPerks.ContainsKey(this.teamAtO[index].SubclassName))
            this.teamAtO[index].PerkList = this.heroPerks[this.teamAtO[index].SubclassName];
        }
      }
      string json1 = JsonHelper.ToJson<Hero>(this.teamAtO);
      string json2 = JsonHelper.ToJson<string>(this.teamNPCAtO);
      string json3 = JsonHelper.ToJson<Hero>(this.teamAtOBackup);
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("shareTeam MP", "net");
      this.RedoSkins();
      NetworkManager.Instance.ClearAllPlayerManualReady();
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("NET_SetTeam CALL", "net");
      int corruptionCommonCompleted = this.corruptionCommonCompleted;
      if (this.corruptionCommonCompleted < 0)
        this.corruptionCommonCompleted = 0;
      int uncommonCompleted = this.corruptionUncommonCompleted;
      if (this.corruptionUncommonCompleted < 0)
        this.corruptionUncommonCompleted = 0;
      int corruptionRareCompleted = this.corruptionRareCompleted;
      if (this.corruptionRareCompleted < 0)
        this.corruptionRareCompleted = 0;
      int corruptionEpicCompleted = this.corruptionEpicCompleted;
      if (this.corruptionEpicCompleted < 0)
        this.corruptionEpicCompleted = 0;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.corruptionCommonCompleted.ToString());
      stringBuilder.Append("|");
      stringBuilder.Append(this.corruptionUncommonCompleted.ToString());
      stringBuilder.Append("|");
      stringBuilder.Append(this.corruptionRareCompleted.ToString());
      stringBuilder.Append("|");
      stringBuilder.Append(this.corruptionEpicCompleted.ToString());
      this.photonView.RPC("NET_SetTeam", RpcTarget.Others, (object) this.gameId, (object) this.currentMapNode, (object) Functions.CompressString(json1), (object) Functions.CompressString(json2), (object) Functions.CompressString(json3), (object) stringBuilder.ToString());
      while (!NetworkManager.Instance.AllPlayersReady("shareteam"))
        yield return (object) Globals.Instance.WaitForSeconds(0.01f);
      if (sceneToLoad != "")
        NetworkManager.Instance.LoadScene(sceneToLoad);
      else if ((UnityEngine.Object) MapManager.Instance != (UnityEngine.Object) null)
        MapManager.Instance.sideCharacters.Refresh();
    }
  }

  [PunRPC]
  public void NET_SetTeam(
    string _gameId,
    string _currentMapNode,
    string _TeamHero,
    string _TeamNPC,
    string _TeamHeroBackup,
    string _CorruptionCompleted)
  {
    this.gameId = _gameId;
    this.currentMapNode = _currentMapNode;
    Hero[] heroArray1 = JsonHelper.FromJson<Hero>(Functions.DecompressString(_TeamHero));
    string[] strArray1 = JsonHelper.FromJson<string>(Functions.DecompressString(_TeamNPC));
    Hero[] heroArray2 = JsonHelper.FromJson<Hero>(Functions.DecompressString(_TeamHeroBackup));
    string[] strArray2 = _CorruptionCompleted.Split('|', StringSplitOptions.None);
    this.corruptionCommonCompleted = int.Parse(strArray2[0]);
    this.corruptionUncommonCompleted = int.Parse(strArray2[1]);
    this.corruptionRareCompleted = int.Parse(strArray2[2]);
    this.corruptionEpicCompleted = int.Parse(strArray2[3]);
    this.CreateTeam();
    this.CreateTeamBackup();
    for (int index = 0; index < heroArray1.Length; ++index)
    {
      if (heroArray1[index] != null)
      {
        this.teamAtO[index] = GameManager.Instance.AssignDataToHero(heroArray1[index]);
        if (this.teamAtO[index] != null && (UnityEngine.Object) this.teamAtO[index].HeroData != (UnityEngine.Object) null)
        {
          if (this.heroPerks == null)
            this.heroPerks = new Dictionary<string, List<string>>();
          string subclassName = this.teamAtO[index].SubclassName;
          if (this.heroPerks.ContainsKey(subclassName))
            this.heroPerks[subclassName] = this.teamAtO[index].PerkList;
          else
            this.heroPerks.Add(subclassName, this.teamAtO[index].PerkList);
        }
        if (this.teamAtOBackup == null)
          this.CreateTeamBackup();
        if (heroArray2 != null && index < heroArray2.Length)
          this.teamAtOBackup[index] = GameManager.Instance.AssignDataToHero(heroArray2[index]);
      }
    }
    this.RedoSkins();
    this.teamNPCAtO = new string[strArray1.Length];
    for (int index = 0; index < strArray1.Length; ++index)
      this.teamNPCAtO[index] = strArray1[index];
    NetworkManager.Instance.SetStatusReady("shareteam");
    if (!((UnityEngine.Object) MapManager.Instance != (UnityEngine.Object) null))
      return;
    MapManager.Instance.sideCharacters.Refresh();
  }

  public void SetCraftReaminingUses(int heroIndex, int uses)
  {
    if (this.teamAtO == null || this.teamAtO.Length == 0 || heroIndex > this.teamAtO.Length || heroIndex < 0 || this.teamAtO[heroIndex] == null)
      return;
    this.teamAtO[heroIndex].CraftRemainingUses = uses;
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
      return;
    this.photonView.RPC("NET_SetCraftReaminingUses", RpcTarget.MasterClient, (object) heroIndex, (object) uses);
  }

  [PunRPC]
  private void NET_SetCraftReaminingUses(int heroIndex, int uses) => this.teamAtO[heroIndex].CraftRemainingUses = uses;

  public int GetCraftReaminingUses(int heroIndex) => heroIndex > -1 && heroIndex < this.teamAtO.Length ? this.teamAtO[heroIndex].CraftRemainingUses : 0;

  public void SubstractCraftReaminingUses(int heroIndex, int quantity = 1)
  {
    if (this.teamAtO[heroIndex].CraftRemainingUses > 0)
      this.teamAtO[heroIndex].CraftRemainingUses -= quantity;
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
      return;
    this.photonView.RPC("NET_SubstractCraftReaminingUses", RpcTarget.MasterClient, (object) heroIndex, (object) quantity);
  }

  [PunRPC]
  private void NET_SubstractCraftReaminingUses(int heroIndex, int quantity) => this.teamAtO[heroIndex].CraftRemainingUses -= quantity;

  public void HeroCraftCrafted(int heroIndex)
  {
    ++this.teamAtO[heroIndex].CardsCrafted;
    PlayerManager.Instance.CardCrafted();
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
      return;
    this.photonView.RPC("NET_HeroCraftCrafted", RpcTarget.MasterClient, (object) heroIndex);
  }

  [PunRPC]
  private void NET_HeroCraftCrafted(int heroIndex) => ++this.teamAtO[heroIndex].CardsCrafted;

  public void HeroCraftUpgraded(int heroIndex)
  {
    ++this.teamAtO[heroIndex].CardsUpgraded;
    PlayerManager.Instance.CardUpgraded();
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
      return;
    this.photonView.RPC("NET_HeroCraftUpgraded", RpcTarget.MasterClient, (object) heroIndex);
  }

  [PunRPC]
  private void NET_HeroCraftUpgraded(int heroIndex) => ++this.teamAtO[heroIndex].CardsUpgraded;

  public void HeroCraftRemoved(int heroIndex)
  {
    if (this.CharInTown() && this.GetTownTier() == 0)
      return;
    ++this.teamAtO[heroIndex].CardsRemoved;
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
      return;
    this.photonView.RPC("NET_HeroCraftRemoved", RpcTarget.MasterClient, (object) heroIndex);
  }

  [PunRPC]
  private void NET_HeroCraftRemoved(int heroIndex) => ++this.teamAtO[heroIndex].CardsRemoved;

  public int GetHeroCraftRemovedTimes(int heroIndex) => this.teamAtO[heroIndex].CardsRemoved;

  public void HeroLevelUp(int heroIndex, string traitId)
  {
    Hero hero = this.teamAtO[heroIndex];
    if (hero.AssignTrait(traitId))
    {
      TraitData traitData = Globals.Instance.GetTraitData(traitId);
      if ((UnityEngine.Object) traitData != (UnityEngine.Object) null && (UnityEngine.Object) traitData.TraitCard != (UnityEngine.Object) null)
      {
        int num = PlayerManager.Instance.GetCharacterTier("", "trait", hero.PerkRank);
        if (GameManager.Instance.IsObeliskChallenge())
        {
          if (GameManager.Instance.IsWeeklyChallenge())
            num = 2;
          else if (this.obeliskMadness >= 5)
            num = this.obeliskMadness < 8 ? 1 : 2;
        }
        string str = traitData.TraitCard.Id;
        switch (num)
        {
          case 1:
            str = Globals.Instance.GetCardData(str, false).UpgradesTo1;
            break;
          case 2:
            str = Globals.Instance.GetCardData(str, false).UpgradesTo2;
            break;
        }
        this.AddCardToHero(heroIndex, str);
      }
      if ((UnityEngine.Object) traitData.TraitCardForAllHeroes != (UnityEngine.Object) null)
      {
        string id = traitData.TraitCardForAllHeroes.Id;
        for (int _heroIndex = 0; _heroIndex < 4; ++_heroIndex)
          this.AddCardToHero(_heroIndex, id);
      }
      hero.LevelUp();
    }
    GameManager.Instance.PlayLibraryAudio("ui_level_up");
    this.SideBarRefresh();
    this.ReDrawLevel();
  }

  public void HeroLevelUpMP(int _heroIndex, string _traitId) => this.photonView.RPC("NET_HeroLevelUpMP", RpcTarget.MasterClient, (object) _heroIndex, (object) _traitId);

  [PunRPC]
  private void NET_HeroLevelUpMP(int _heroIndex, string _traitId)
  {
    this.HeroLevelUp(_heroIndex, _traitId);
    this.photonView.RPC("NET_SetHeroLevelUp", RpcTarget.Others, (object) _heroIndex, (object) _traitId);
  }

  [PunRPC]
  public void NET_SetHeroLevelUp(int _heroIndex, string _traitId) => this.HeroLevelUp(_heroIndex, _traitId);

  public void ModifyHeroLife(int _heroIndex = -1, int _flat = 0, float _percent = 0.0f)
  {
    for (int index = 0; index < 4; ++index)
    {
      if (_heroIndex == -1 || _heroIndex == index)
      {
        if (_flat != 0)
          this.teamAtO[index].HpCurrent += _flat;
        if ((double) _percent != 0.0)
          this.teamAtO[index].HpCurrent += Functions.FuncRoundToInt((float) ((double) this.teamAtO[index].Hp * (double) _percent / 100.0));
        if (this.teamAtO[index].HpCurrent > this.teamAtO[index].Hp)
          this.teamAtO[index].HpCurrent = this.teamAtO[index].Hp;
        else if (this.teamAtO[index].HpCurrent <= 0)
          this.teamAtO[index].HpCurrent = 1;
      }
    }
  }

  public void UpgradeRandomCardToHero(int _heroIndex, int _quantity = 1)
  {
    for (int index1 = 0; index1 < _quantity; ++index1)
    {
      bool flag = false;
      int num = 0;
      while (!flag)
      {
        int index2 = UnityEngine.Random.Range(0, this.teamAtO[_heroIndex].Cards.Count);
        CardData cardData = Globals.Instance.GetCardData(this.teamAtO[_heroIndex].Cards[index2]);
        if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && cardData.CardClass != Enums.CardClass.Injury && cardData.CardClass != Enums.CardClass.Boon && cardData.CardClass != Enums.CardClass.Item && cardData.CardUpgraded == Enums.CardUpgraded.No)
        {
          string str = "";
          if (UnityEngine.Random.Range(0, 2) == 0 && cardData.UpgradesTo1 != "")
          {
            this.teamAtO[_heroIndex].Cards[index2] = cardData.UpgradesTo1;
            str = cardData.UpgradesTo1;
          }
          else if (cardData.UpgradesTo2 != "")
          {
            this.teamAtO[_heroIndex].Cards[index2] = cardData.UpgradesTo2;
            str = cardData.UpgradesTo2;
          }
          flag = true;
          this.upgradedCardsList.Add(str);
        }
        if (!flag)
        {
          ++num;
          if (num > 1000)
          {
            if (!Globals.Instance.ShowDebug)
              return;
            Functions.DebugLogGD("Exhasusted!!!");
            return;
          }
        }
      }
    }
    if ((UnityEngine.Object) MapManager.Instance != (UnityEngine.Object) null)
      MapManager.Instance.sideCharacters.ShowUpgrade(_heroIndex);
    if (!GameManager.Instance.IsMultiplayer())
      return;
    string json1 = JsonHelper.ToJson<string>(this.upgradedCardsList.ToArray());
    string json2 = JsonHelper.ToJson<string>(this.teamAtO[_heroIndex].Cards.ToArray());
    this.photonView.RPC("NET_SetTeamHeroCards", RpcTarget.Others, (object) _heroIndex, (object) json2, (object) json1);
  }

  public void AddCardToHero(int _heroIndex, string _cardName)
  {
    CardData cardData = Globals.Instance.GetCardData(_cardName, false);
    if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && cardData.CardClass == Enums.CardClass.Injury && this.ngPlus > 0 && this.IsZoneAffectedByMadness())
    {
      if (this.ngPlus >= 3 && this.ngPlus <= 5 && cardData.UpgradesTo1 != "")
        _cardName = cardData.UpgradesTo1;
      else if (this.ngPlus >= 6 && cardData.UpgradesTo2 != "")
        _cardName = cardData.UpgradesTo2;
    }
    if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && cardData.CardType == Enums.CardType.Pet)
      this.teamAtO[_heroIndex].Pet = _cardName;
    else
      this.teamAtO[_heroIndex].Cards.Add(_cardName);
    if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
    {
      if (cardData.CardClass == Enums.CardClass.Boon && this.IsChallengeTraitActive("doubleboons"))
        this.teamAtO[_heroIndex].Cards.Add(_cardName);
      if (cardData.CardClass == Enums.CardClass.Injury && this.IsChallengeTraitActive("doubleinjuries"))
        this.teamAtO[_heroIndex].Cards.Add(_cardName);
    }
    if (cardData.CardClass != Enums.CardClass.Boon)
      return;
    PlayerManager.Instance.CardUnlock(_cardName, true);
  }

  public void AddDeckToHeroMP(int _heroIndex, List<string> _cardList)
  {
    string json = JsonHelper.ToJson<string>(_cardList.ToArray());
    this.photonView.RPC("NET_AddDeckToHeroMP", RpcTarget.Others, (object) _heroIndex, (object) json);
  }

  [PunRPC]
  private void NET_AddDeckToHeroMP(int _heroIndex, string _cardList)
  {
    List<string> stringList = new List<string>();
    stringList.AddRange((IEnumerable<string>) JsonHelper.FromJson<string>(_cardList));
    this.teamAtO[_heroIndex].Cards = stringList;
    this.SideBarRefreshCards(_heroIndex);
  }

  public void AddCardToHeroMP(int _heroIndex, string _cardName) => this.photonView.RPC("NET_AddCardToHeroMP", RpcTarget.MasterClient, (object) _heroIndex, (object) _cardName);

  [PunRPC]
  private void NET_AddCardToHeroMP(int _heroIndex, string _cardName)
  {
    this.teamAtO[_heroIndex].Cards.Add(_cardName);
    string json = JsonHelper.ToJson<string>(this.teamAtO[_heroIndex].Cards.ToArray());
    this.photonView.RPC("NET_SetTeamHeroCards", RpcTarget.Others, (object) _heroIndex, (object) json, (object) "");
    this.SideBarRefreshCards(_heroIndex);
  }

  [PunRPC]
  public void NET_SetTeamHeroCards(int _heroIndex, string _heroCards, string _upgradedCards)
  {
    List<string> stringList1 = new List<string>();
    stringList1.AddRange((IEnumerable<string>) JsonHelper.FromJson<string>(_heroCards));
    this.teamAtO[_heroIndex].Cards = stringList1;
    List<string> stringList2 = new List<string>();
    if (_upgradedCards != "")
      stringList2.AddRange((IEnumerable<string>) JsonHelper.FromJson<string>(_upgradedCards));
    this.upgradedCardsList = stringList2;
    this.SideBarRefreshCards(_heroIndex);
  }

  private void ClearNodeInfo()
  {
    this.ClearCurrentCombatData();
    this.ResetEventRewardTier();
  }

  public void SetPositionText(string position = "")
  {
    string str = !(position == "") ? position : Globals.Instance.GetNodeData(this.currentMapNode).NodeName;
    if (str == "" && GameManager.Instance.IsObeliskChallenge())
      str = !GameManager.Instance.IsWeeklyChallenge() ? Texts.Instance.GetText("modeObelisk") : Texts.Instance.GetText("modeWeekly");
    SteamManager.Instance.SetRichPresence("location", str);
    SteamManager.Instance.SetRichPresence("steam_display", "#Status_Location");
  }

  public Enums.Zone GetMapZone(string _nodeName)
  {
    NodeData nodeData = Globals.Instance.GetNodeData(_nodeName);
    if ((UnityEngine.Object) nodeData != (UnityEngine.Object) null && (UnityEngine.Object) nodeData.NodeZone != (UnityEngine.Object) null)
    {
      string lower = nodeData.NodeZone.ZoneId.ToLower();
      string[] names = Enum.GetNames(typeof (Enums.Zone));
      for (int index = 0; index < names.Length; ++index)
      {
        if (names[index].ToLower() == lower)
          return (Enums.Zone) Enum.Parse(typeof (Enums.Zone), names[index]);
      }
    }
    return Enums.Zone.None;
  }

  public bool IsZoneAffectedByMadness()
  {
    if (this.currentMapNode == "")
      return true;
    return (UnityEngine.Object) Globals.Instance.GetNodeData(this.currentMapNode) != (UnityEngine.Object) null && !Globals.Instance.GetNodeData(this.currentMapNode).NodeZone.DisableMadnessOnThisZone;
  }

  public bool SetCurrentNode(string _nodeName, string _nodeNameUnlock = "", string _nodeObeliskIcon = "")
  {
    if (_nodeNameUnlock == "")
      _nodeNameUnlock = _nodeName;
    PlayerManager.Instance.NodeUnlock(_nodeNameUnlock);
    this.currentMapNode = _nodeName;
    this.SetTownZoneId(Globals.Instance.GetNodeData(this.currentMapNode).NodeZone.ZoneId);
    if (GameManager.Instance.IsWeeklyChallenge() && this.NodeIsObeliskFinal() && this.currentMapNode.Contains("_0"))
    {
      ChallengeData weeklyData = Globals.Instance.GetWeeklyData(this.weekly);
      if (weeklyData.IdSteam != "")
        SteamManager.Instance.SetStatInt(weeklyData.IdSteam, 1);
    }
    this.SetPositionText();
    if (GameManager.Instance.IsMultiplayer())
    {
      if (NetworkManager.Instance.IsMaster())
        this.photonView.RPC("NET_SetCurrentNode", RpcTarget.Others, (object) this.currentMapNode, (object) _nodeNameUnlock, (object) _nodeObeliskIcon);
      else
        this.StartCoroutine(this.EndCurrentNodeMP());
    }
    this.ClearNodeInfo();
    if (this.mapVisitedNodes.Contains(this.currentMapNode))
      return false;
    this.mapVisitedNodes.Add(this.currentMapNode);
    string str1 = this.currentMapNode + "|" + _nodeNameUnlock;
    string str2 = !GameManager.Instance.IsObeliskChallenge() || !(_nodeObeliskIcon != "") ? str1 + "|" : str1 + "|" + _nodeObeliskIcon;
    if (this.mapVisitedNodesAction == null)
      this.mapVisitedNodesAction = new List<string>();
    if (!this.mapVisitedNodesAction.Contains(str2))
      this.mapVisitedNodesAction.Add(str2);
    return true;
  }

  [PunRPC]
  private void NET_SetCurrentNode(
    string _nodeName,
    string _nodeNameUnlock,
    string _nodeObeliskIcon)
  {
    this.SetCurrentNode(_nodeName, _nodeNameUnlock, _nodeObeliskIcon);
  }

  private IEnumerator EndCurrentNodeMP()
  {
    NetworkManager.Instance.SetWaitingSyncro("waitingSetCurrentNode", true);
    NetworkManager.Instance.SetStatusReady("waitingSetCurrentNode");
    while (NetworkManager.Instance.WaitingSyncro.ContainsKey("waitingSetCurrentNode") && NetworkManager.Instance.WaitingSyncro["waitingSetCurrentNode"])
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("waitingSetCurrentNode, we can continue!");
  }

  public void GoToTown(string _nodeName)
  {
    this.currentMapNode = _nodeName;
    this.ClearReroll();
    if (!this.mapVisitedNodes.Contains(this.currentMapNode))
    {
      this.mapVisitedNodes.Add(this.currentMapNode);
      Debug.Log((object) ("Add to mapVisitedNodes " + this.currentMapNode));
      string str = this.currentMapNode + "|Town";
      if (!this.mapVisitedNodesAction.Contains(str))
      {
        Debug.Log((object) ("Add to mapVisitedNodesAction " + str));
        this.mapVisitedNodesAction.Add(str);
      }
    }
    this.UpgradeTownTier();
    if (GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
      this.photonView.RPC("NET_GoToTown", RpcTarget.Others, (object) this.currentMapNode);
    SceneStatic.LoadByName("Town");
  }

  [PunRPC]
  private void NET_GoToTown(string _nodeName) => this.GoToTown(_nodeName);

  public CombatData GetCurrentCombatData() => this.currentCombatData;

  public void ClearCurrentCombatData() => this.currentCombatData = (CombatData) null;

  private void ReAssignNodeByRequeriment(string _requerimentId)
  {
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster() || (UnityEngine.Object) MapManager.Instance == (UnityEngine.Object) null)
      return;
    Dictionary<string, Node> mapNodeDict = MapManager.Instance.GetMapNodeDict();
    if (mapNodeDict == null)
      return;
    foreach (KeyValuePair<string, Node> keyValuePair in mapNodeDict)
    {
      if (!this.gameNodeAssigned.ContainsKey(keyValuePair.Key) || !this.mapVisitedNodes.Contains(keyValuePair.Key))
      {
        Node _node = keyValuePair.Value;
        for (int index = 0; index < _node.nodeData.NodeEvent.Length; ++index)
        {
          if ((UnityEngine.Object) _node.nodeData.NodeEvent[index].Requirement != (UnityEngine.Object) null && _node.nodeData.NodeEvent[index].Requirement.RequirementId == _requerimentId && this.gameNodeAssigned.ContainsKey(_node.nodeData.NodeId))
          {
            this.gameNodeAssigned.Remove(_node.nodeData.NodeId);
            this.AssignSingleGameNode(_node);
          }
        }
      }
    }
  }

  public void AssignSingleGameNode(Node _node)
  {
    if (_node.nodeData.ExistsSku != "" && !SteamManager.Instance.PlayerHaveDLC(_node.nodeData.ExistsSku))
    {
      this.gameNodeAssigned.Remove(_node.nodeData.NodeId);
    }
    else
    {
      UnityEngine.Random.InitState((_node.nodeData.NodeId + this.GetGameId() + nameof (AssignSingleGameNode)).GetDeterministicHashCode());
      if (UnityEngine.Random.Range(0, 100) >= _node.nodeData.ExistsPercent)
      {
        this.gameNodeAssigned.Remove(_node.nodeData.NodeId);
      }
      else
      {
        bool flag1 = true;
        bool flag2 = true;
        if (_node.nodeData.NodeEvent != null && _node.nodeData.NodeEvent.Length != 0 && _node.nodeData.NodeCombat != null && _node.nodeData.NodeCombat.Length != 0)
        {
          if (UnityEngine.Random.Range(0, 100) < _node.nodeData.CombatPercent)
            flag1 = false;
          else
            flag2 = false;
        }
        if (_node.nodeData.GoToTown)
        {
          if (this.gameNodeAssigned.ContainsKey(_node.nodeData.NodeId))
            this.gameNodeAssigned[_node.nodeData.NodeId] = "town:town";
          else
            this.gameNodeAssigned.Add(_node.nodeData.NodeId, "town:town");
        }
        else if (_node.nodeData.TravelDestination)
        {
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD("TravelDestination **** " + _node.nodeData.NodeId, "map");
          if (this.gameNodeAssigned.ContainsKey(_node.nodeData.NodeId))
            this.gameNodeAssigned[_node.nodeData.NodeId] = "destination:destination";
          else
            this.gameNodeAssigned.Add(_node.nodeData.NodeId, "destination:destination");
        }
        else if (flag1 && _node.nodeData.NodeEvent != null && _node.nodeData.NodeEvent.Length != 0)
        {
          string str = "";
          Dictionary<string, int> source = new Dictionary<string, int>();
          for (int index = 0; index < _node.nodeData.NodeEvent.Length; ++index)
          {
            bool flag3 = true;
            if ((UnityEngine.Object) _node.nodeData.NodeEvent[index].Requirement != (UnityEngine.Object) null && !this.PlayerHasRequirement(_node.nodeData.NodeEvent[index].Requirement))
              flag3 = false;
            if ((UnityEngine.Object) _node.nodeData.NodeEvent[index].RequiredClass != (UnityEngine.Object) null && !this.PlayerHasRequirementClass(_node.nodeData.NodeEvent[index].RequiredClass.Id))
              flag3 = false;
            if (flag3)
            {
              int num = 10000;
              if (index < _node.nodeData.NodeEventPriority.Length)
                num = _node.nodeData.NodeEventPriority[index];
              source.Add(_node.nodeData.NodeEvent[index].EventId, num);
            }
          }
          if (source.Count == 0)
            return;
          Dictionary<string, int> dictionary1 = source.OrderBy<KeyValuePair<string, int>, int>((Func<KeyValuePair<string, int>, int>) (x => x.Value)).ToDictionary<KeyValuePair<string, int>, string, int>((Func<KeyValuePair<string, int>, string>) (x => x.Key), (Func<KeyValuePair<string, int>, int>) (x => x.Value));
          int num1 = 1;
          int num2 = dictionary1.ElementAt<KeyValuePair<string, int>>(0).Value;
          while (num1 < dictionary1.Count && dictionary1.ElementAt<KeyValuePair<string, int>>(num1).Value == num2)
            ++num1;
          if (num1 == 1)
          {
            str = dictionary1.ElementAt<KeyValuePair<string, int>>(0).Key;
          }
          else
          {
            if (_node.nodeData.NodeEventPercent != null && _node.nodeData.NodeEvent.Length == _node.nodeData.NodeEventPercent.Length)
            {
              Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
              int index1 = 0;
              for (int index2 = 0; index2 < num1; ++index2)
              {
                int index3 = 0;
                while (index2 < _node.nodeData.NodeEvent.Length)
                {
                  if (_node.nodeData.NodeEvent[index3].EventId == dictionary1.ElementAt<KeyValuePair<string, int>>(index1).Key)
                  {
                    dictionary2.Add(_node.nodeData.NodeEvent[index3].EventId, _node.nodeData.NodeEventPercent[index3]);
                    ++index1;
                    break;
                  }
                  ++index3;
                }
              }
              int num3 = UnityEngine.Random.Range(0, 100);
              int num4 = 0;
              foreach (KeyValuePair<string, int> keyValuePair in dictionary2)
              {
                num4 += keyValuePair.Value;
                if (num3 < num4)
                {
                  str = keyValuePair.Key;
                  break;
                }
              }
            }
            if (str == "")
            {
              int index = UnityEngine.Random.Range(0, num1);
              str = dictionary1.ElementAt<KeyValuePair<string, int>>(index).Key;
            }
          }
          if (this.gameNodeAssigned.ContainsKey(_node.nodeData.NodeId))
            this.gameNodeAssigned[_node.nodeData.NodeId] = "event:" + str;
          else
            this.gameNodeAssigned.Add(_node.nodeData.NodeId, "event:" + str);
        }
        else if (flag2 && _node.nodeData.NodeCombat != null && _node.nodeData.NodeCombat.Length != 0)
        {
          string str = "";
          if (GameManager.Instance.IsWeeklyChallenge() && (_node.nodeData.NodeId == "of1_10" || _node.nodeData.NodeId == "of2_10"))
          {
            ChallengeData weeklyData = Globals.Instance.GetWeeklyData(this.weekly);
            if ((UnityEngine.Object) weeklyData != (UnityEngine.Object) null && (UnityEngine.Object) weeklyData.BossCombat != (UnityEngine.Object) null)
              str = weeklyData.BossCombat.CombatId;
          }
          if (str == "")
          {
            int index = 0;
            if (_node.nodeData.NodeId == "of1_10" || _node.nodeData.NodeId == "of2_10")
            {
              UnityEngine.Random.State state = UnityEngine.Random.state;
              UnityEngine.Random.InitState((_node.nodeData.NodeId + this.GetGameId() + "finalBoss").GetDeterministicHashCode());
              index = UnityEngine.Random.Range(0, _node.nodeData.NodeCombat.Length);
              UnityEngine.Random.state = state;
            }
            str = _node.nodeData.NodeCombat[index].CombatId;
          }
          if (this.gameNodeAssigned.ContainsKey(_node.nodeData.NodeId))
            this.gameNodeAssigned[_node.nodeData.NodeId] = "combat:" + str;
          else
            this.gameNodeAssigned.Add(_node.nodeData.NodeId, "combat:" + str);
        }
        else if (this.gameNodeAssigned.ContainsKey(_node.nodeData.NodeId))
          this.gameNodeAssigned[_node.nodeData.NodeId] = "";
        else
          this.gameNodeAssigned.Add(_node.nodeData.NodeId, "");
      }
    }
  }

  public TierRewardData GetTeamNPCReward()
  {
    int num = 0;
    for (int index = 0; index < this.teamNPCAtO.Length; ++index)
    {
      if (this.teamNPCAtO[index] != null && this.teamNPCAtO[index] != "")
      {
        NPCData npcData = Globals.Instance.GetNPC(this.teamNPCAtO[index]);
        if ((UnityEngine.Object) npcData != (UnityEngine.Object) null && this.PlayerHasRequirement(Globals.Instance.GetRequirementData("_tier2")) && (UnityEngine.Object) npcData.UpgradedMob != (UnityEngine.Object) null)
          npcData = npcData.UpgradedMob;
        if ((UnityEngine.Object) npcData != (UnityEngine.Object) null && this.ngPlus > 0)
          npcData = npcData.NgPlusMob;
        if (MadnessManager.Instance.IsMadnessTraitActive("despair") && (UnityEngine.Object) npcData.HellModeMob != (UnityEngine.Object) null)
          npcData = npcData.HellModeMob;
        if ((UnityEngine.Object) npcData != (UnityEngine.Object) null && (UnityEngine.Object) npcData.TierReward != (UnityEngine.Object) null && npcData.TierReward.TierNum > num)
          num = npcData.TierReward.TierNum;
      }
    }
    return Globals.Instance.GetTierRewardData(num);
  }

  public void ResetCombatScarab() => this.combatScarab = "";

  public void FinishCardRewards(string[] arrRewards)
  {
    for (int _heroIndex = 0; _heroIndex < 4; ++_heroIndex)
    {
      if (this.teamAtO[_heroIndex] != null && !((UnityEngine.Object) this.teamAtO[_heroIndex].HeroData == (UnityEngine.Object) null))
      {
        if (arrRewards[_heroIndex] != "" && arrRewards[_heroIndex] != "dust")
          this.AddCardToHero(_heroIndex, arrRewards[_heroIndex]);
        else if (arrRewards[_heroIndex] == "dust")
        {
          int dust = Globals.Instance.GetTierRewardData(this.currentRewardTier).Dust;
          if (GameManager.Instance.IsObeliskChallenge() && Globals.Instance.ZoneDataSource[this.GetTownZoneId().ToLower()].ObeliskLow)
            dust *= 2;
          if (MadnessManager.Instance.IsMadnessTraitActive("poverty") || this.IsChallengeTraitActive("poverty"))
          {
            if (!GameManager.Instance.IsObeliskChallenge())
              dust -= Functions.FuncRoundToInt((float) dust * 0.5f);
            else
              dust -= Functions.FuncRoundToInt((float) dust * 0.3f);
          }
          if (this.IsChallengeTraitActive("prosperity"))
            dust += Functions.FuncRoundToInt((float) dust * 0.5f);
          this.GivePlayer(1, dust, this.teamAtO[_heroIndex].Owner);
        }
      }
    }
    if (GameManager.Instance.IsObeliskChallenge() && this.mapNodeObeliskBoss.Contains(this.currentMapNode))
    {
      NodeData nodeData = Globals.Instance.GetNodeData(this.currentMapNode);
      if (nodeData.NodeCombatTier == Enums.CombatTier.T8)
        this.DoLoot("challenge_boss_low");
      else if (nodeData.NodeCombatTier == Enums.CombatTier.T9)
        this.DoLoot("challenge_boss_high");
      else if (this.townZoneId.ToLower() == this.obeliskLow)
        this.DoLoot("challenge_chest_low");
      else if (this.townZoneId.ToLower() == this.obeliskHigh)
        this.DoLoot("challenge_chest_high");
      else
        this.DoLoot("challenge_chest_final");
    }
    else if (GameManager.Instance.IsMultiplayer())
    {
      if ((UnityEngine.Object) this.townDivinationTier != (UnityEngine.Object) null)
        this.StartCoroutine(this.ShareTeam("Town"));
      else
        this.StartCoroutine(this.ShareTeam("Map"));
    }
    else if ((UnityEngine.Object) this.townDivinationTier != (UnityEngine.Object) null)
      SceneStatic.LoadByName("Town");
    else
      SceneStatic.LoadByName("Map");
  }

  public void FinishObeliskDraft()
  {
    Dictionary<int, List<string>> dictionary = new Dictionary<int, List<string>>();
    for (int key = 0; key < 4; ++key)
    {
      List<string> cards = this.teamAtO[key].Cards;
      dictionary.Add(key, cards);
    }
    string[] _team = new string[4];
    string[] strArray1 = new string[4];
    string[] strArray2 = new string[4];
    for (int index = 0; index < 4; ++index)
    {
      if (this.teamAtO[index] != null && !((UnityEngine.Object) this.teamAtO[index].HeroData == (UnityEngine.Object) null))
      {
        _team[index] = this.teamAtO[index].HeroData.HeroSubClass.SubClassName;
        strArray1[index] = this.teamAtO[index].SkinUsed;
        strArray2[index] = this.teamAtO[index].CardbackUsed;
      }
    }
    this.SetTeamFromArray(_team);
    for (int key = 0; key < 4; ++key)
    {
      if (this.teamAtO[key] != null && !((UnityEngine.Object) this.teamAtO[key].HeroData == (UnityEngine.Object) null))
      {
        this.teamAtO[key].SkinUsed = strArray1[key];
        this.teamAtO[key].CardbackUsed = strArray2[key];
        if (this.heroPerks.ContainsKey(_team[key]))
          this.teamAtO[key].PerkList = this.heroPerks[_team[key]];
        this.teamAtO[key].Cards = dictionary[key];
        if (this.teamAtO[key] != null && (UnityEngine.Object) this.teamAtO[key].HeroData != (UnityEngine.Object) null && (UnityEngine.Object) this.teamAtO[key].HeroData.HeroSubClass != (UnityEngine.Object) null)
          this.teamAtO[key].AssignTrait(this.teamAtO[key].HeroData.HeroSubClass.Trait0.Id);
      }
    }
    string _lootListId = "challenge_start";
    if (GameManager.Instance.IsWeeklyChallenge())
    {
      ChallengeData weeklyData = Globals.Instance.GetWeeklyData(Functions.GetCurrentWeeklyWeek());
      if ((UnityEngine.Object) weeklyData != (UnityEngine.Object) null && (UnityEngine.Object) weeklyData.Loot != (UnityEngine.Object) null && weeklyData.Loot.Id != "")
        _lootListId = weeklyData.Loot.Id;
    }
    this.DoLoot(_lootListId);
  }

  public void SetObeliskLootReroll() => this.shopItemReroll = this.currentMapNode + this.gameId;

  public void DoTownReroll()
  {
    this.shopItemReroll = Functions.RandomString(6f);
    this.SetTownReroll(NetworkManager.Instance.GetPlayerNick(), this.shopItemReroll);
  }

  public void SetTownReroll(string _nick, string _code, bool _sendCoop = true)
  {
    if (this.townRerollList == null)
      this.townRerollList = new Dictionary<string, string>();
    string playerNickReal = NetworkManager.Instance.GetPlayerNickReal(_nick);
    if (!this.townRerollList.ContainsKey(playerNickReal))
    {
      this.townRerollList.Add(playerNickReal, _code);
    }
    else
    {
      Dictionary<string, string> townRerollList = this.townRerollList;
      string key = playerNickReal;
      townRerollList[key] = townRerollList[key] + "_" + _code;
    }
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
      this.SaveGame();
    if (!_sendCoop || !GameManager.Instance.IsMultiplayer())
      return;
    this.photonView.RPC("NET_SetTownReroll", RpcTarget.Others, (object) _nick, (object) _code);
  }

  [PunRPC]
  private void NET_SetTownReroll(string _nick, string _code) => this.SetTownReroll(_nick, _code, false);

  public bool IsTownRerollLimited() => !this.sandbox_unlimitedRerolls && !GameManager.Instance.IsObeliskChallenge() && this.GetNgPlus() > 3;

  public bool IsTownRerollAvailable() => !this.IsTownRerollLimited() || this.HowManyTownRerolls() <= 0;

  public int HowManyTownRerolls()
  {
    if (this.townRerollList == null)
    {
      this.townRerollList = new Dictionary<string, string>();
      return 0;
    }
    if (!GameManager.Instance.IsMultiplayer())
    {
      if (this.townRerollList.Count > 0)
        return this.townRerollList.First<KeyValuePair<string, string>>().Value.Split("_", StringSplitOptions.None).Length;
    }
    else
    {
      string playerNickReal = NetworkManager.Instance.GetPlayerNickReal(NetworkManager.Instance.GetPlayerNick());
      if (this.townRerollList.ContainsKey(playerNickReal))
        return this.townRerollList[playerNickReal].Split("_", StringSplitOptions.None).Length;
    }
    return 0;
  }

  private void TownRerollRestore()
  {
    if (this.townRerollList == null || !this.townRerollList.ContainsKey(NetworkManager.Instance.GetMyNickReal()))
      return;
    string[] strArray = this.townRerollList[NetworkManager.Instance.GetMyNickReal()].Split('_', StringSplitOptions.None);
    if (strArray.Length == 0 || strArray[strArray.Length - 1] == null)
      return;
    this.shopItemReroll = strArray[strArray.Length - 1];
  }

  public int GetExperienceFromCombat()
  {
    int experienceFromCombat = 0;
    if (this.teamNPCAtO != null)
    {
      for (int index = 0; index < this.teamNPCAtO.Length; ++index)
      {
        if (this.teamNPCAtO[index] != null && this.teamNPCAtO[index] != "")
        {
          NPCData npcData = Globals.Instance.GetNPC(this.teamNPCAtO[index]);
          if ((UnityEngine.Object) npcData != (UnityEngine.Object) null && this.PlayerHasRequirement(Globals.Instance.GetRequirementData("_tier2")) && (UnityEngine.Object) npcData.UpgradedMob != (UnityEngine.Object) null)
            npcData = npcData.UpgradedMob;
          if ((UnityEngine.Object) npcData != (UnityEngine.Object) null && this.ngPlus > 0)
            npcData = npcData.NgPlusMob;
          if (MadnessManager.Instance.IsMadnessTraitActive("despair") && (UnityEngine.Object) npcData.HellModeMob != (UnityEngine.Object) null)
            npcData = npcData.HellModeMob;
          if ((UnityEngine.Object) npcData != (UnityEngine.Object) null && npcData.ExperienceReward > 0)
            experienceFromCombat += npcData.ExperienceReward;
        }
      }
    }
    return experienceFromCombat;
  }

  public int GetGoldFromCombat()
  {
    int goldFromCombat = 0;
    if (this.teamNPCAtO != null)
    {
      for (int index = 0; index < this.teamNPCAtO.Length; ++index)
      {
        if (this.teamNPCAtO[index] != null && this.teamNPCAtO[index] != "")
        {
          NPCData npcData = Globals.Instance.GetNPC(this.teamNPCAtO[index]);
          if ((UnityEngine.Object) npcData != (UnityEngine.Object) null && this.PlayerHasRequirement(Globals.Instance.GetRequirementData("_tier2")) && (UnityEngine.Object) npcData.UpgradedMob != (UnityEngine.Object) null)
            npcData = npcData.UpgradedMob;
          if ((UnityEngine.Object) npcData != (UnityEngine.Object) null && this.ngPlus > 0)
            npcData = npcData.NgPlusMob;
          if (MadnessManager.Instance.IsMadnessTraitActive("despair") && (UnityEngine.Object) npcData.HellModeMob != (UnityEngine.Object) null)
            npcData = npcData.HellModeMob;
          if ((UnityEngine.Object) npcData != (UnityEngine.Object) null && npcData.GoldReward > 0)
            goldFromCombat += npcData.GoldReward;
        }
      }
    }
    return goldFromCombat;
  }

  public void ClearCombatThermometerData() => this.combatThermometerData = (ThermometerData) null;

  public void SetCombatExpertise(ThermometerData _thermometerData)
  {
    if (this.currentMapNode == "tutorial_0" || this.currentMapNode == "tutorial_1" || this.currentMapNode == "tutorial_2")
      return;
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
    {
      if ((UnityEngine.Object) _thermometerData != (UnityEngine.Object) null)
        this.combatExpertise += _thermometerData.ExpertiseBonus;
      else
        this.combatExpertise = this.combatExpertise;
      if (GameManager.Instance.IsMultiplayer())
        this.photonView.RPC("NET_SetCombatExpertise", RpcTarget.Others, (object) this.combatExpertise);
    }
    ++this.combatTotal;
  }

  [PunRPC]
  private void NET_SetCombatExpertise(int _combatExpertise) => this.combatExpertise = _combatExpertise;

  public void SetCombatThermometerData(ThermometerData _thermometerData) => this.combatThermometerData = _thermometerData;

  public ThermometerData GetCombatThermometerData() => this.combatThermometerData;

  public List<string> GetPlayerRequeriments() => this.playerRequeriments;

  public void SetPlayerRequeriments(List<string> _playerRequeriments) => this.playerRequeriments = _playerRequeriments;

  private void AssignGlobalEventRequirements()
  {
    foreach (KeyValuePair<string, EventRequirementData> requirement in Globals.Instance.Requirements)
    {
      if (requirement.Value.AssignToPlayerAtBegin)
        this.AddPlayerRequirement(requirement.Value, false);
    }
    if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_6_4") && (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster()))
      this.AddPlayerRequirement(Globals.Instance.GetRequirementData("caravan"), false);
    if (!GameManager.Instance.IsMultiplayer() || !NetworkManager.Instance.IsMaster())
      return;
    this.photonView.RPC("NET_AssignGlobalEventRequirements", RpcTarget.Others, (object[]) this.playerRequeriments.ToArray());
  }

  [PunRPC]
  private void NET_AssignGlobalEventRequirements(string[] _requeriments) => this.playerRequeriments = ((IEnumerable<string>) _requeriments).ToList<string>();

  public void AddPlayerRequirementOthers(string _requirementId) => this.photonView.RPC("NET_AddPlayerRequirement", RpcTarget.Others, (object) _requirementId);

  [PunRPC]
  private void NET_AddPlayerRequirement(string _requirementId)
  {
    EventRequirementData requirementData = Globals.Instance.GetRequirementData(_requirementId);
    if (this.playerRequeriments.Contains(requirementData.RequirementId))
      return;
    this.playerRequeriments.Add(requirementData.RequirementId);
  }

  public void AddPlayerRequirement(EventRequirementData requirement, bool share = true)
  {
    if (requirement.RequirementId == "_demo" && (!GameManager.Instance.IsDemo() || GameManager.Instance.GetDeveloperMode()))
      return;
    bool flag = false;
    if (!GameManager.Instance.IsObeliskChallenge() && (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster()))
      flag = true;
    if (this.playerRequeriments.Contains(requirement.RequirementId))
      return;
    this.playerRequeriments.Add(requirement.RequirementId);
    if (!flag)
      return;
    this.ReAssignNodeByRequeriment(requirement.RequirementId);
  }

  public void RemovePlayerRequirement(EventRequirementData requirement, bool share = true)
  {
    bool flag = false;
    if (!GameManager.Instance.IsObeliskChallenge() && (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster()))
      flag = true;
    if (!this.playerRequeriments.Contains(requirement.RequirementId))
      return;
    this.playerRequeriments.Remove(requirement.RequirementId);
    if (flag)
      this.ReAssignNodeByRequeriment(requirement.RequirementId);
    Debug.Log((object) ("-- Player removed requeriment [" + requirement.RequirementId + "] --"));
  }

  public void ShowPlayerRequirements()
  {
    for (int index = 0; index < this.playerRequeriments.Count; ++index)
      Debug.Log((object) (index.ToString() + ".- " + this.playerRequeriments[index]));
  }

  public bool PlayerHasRequirement(EventRequirementData requirement) => (UnityEngine.Object) requirement == (UnityEngine.Object) null || this.playerRequeriments.Contains(requirement.RequirementId);

  public bool PlayerHasRequirementClass(string _classId)
  {
    for (int index = 0; index < this.teamAtO.Length; ++index)
    {
      if (this.teamAtO[index] != null && this.teamAtO[index].SubclassName == _classId.ToLower())
        return true;
    }
    return false;
  }

  public int PlayerHasRequirementItem(CardData requirementItem, SubClassData requiredSCD = null)
  {
    for (int index = 0; index < this.teamAtO.Length; ++index)
    {
      if (this.teamAtO[index] != null && this.teamAtO[index].HasItemCardData(requirementItem) && ((UnityEngine.Object) requiredSCD == (UnityEngine.Object) null || this.teamAtO[index].SubclassName == requiredSCD.Id.ToLower()))
        return index;
    }
    return -1;
  }

  public bool PlayerHasRequirementCard(
    CardData requirementCard,
    SubClassData requiredSCD = null,
    bool checkUpgrades = true)
  {
    if ((UnityEngine.Object) requirementCard != (UnityEngine.Object) null)
    {
      for (int index = 0; index < this.teamAtO.Length; ++index)
      {
        if (this.teamAtO[index] != null && (UnityEngine.Object) this.teamAtO[index].HeroData != (UnityEngine.Object) null && this.teamAtO[index].HasCardDataString(requirementCard.Id, checkUpgrades) && ((UnityEngine.Object) requiredSCD == (UnityEngine.Object) null || this.teamAtO[index].SubclassName == requiredSCD.Id.ToLower()))
          return true;
      }
    }
    return false;
  }

  public TierRewardData GetEventRewardTier() => this.eventRewardTier;

  public void SetEventRewardTier(TierRewardData tierNum) => this.eventRewardTier = tierNum;

  public void ResetEventRewardTier() => this.eventRewardTier = (TierRewardData) null;

  public bool SubClassDataHaveAnythingInSlot(Enums.ItemSlot _slot, SubClassData requiredSCD)
  {
    if ((UnityEngine.Object) requiredSCD == (UnityEngine.Object) null)
      return false;
    int index1 = -1;
    for (int index2 = 0; index2 < this.teamAtO.Length; ++index2)
    {
      if (this.teamAtO[index2] != null && (UnityEngine.Object) this.teamAtO[index2].HeroData != (UnityEngine.Object) null && this.teamAtO[index2].SubclassName == requiredSCD.Id.ToLower())
      {
        index1 = index2;
        break;
      }
    }
    return index1 != -1 && _slot != Enums.ItemSlot.None && (_slot == Enums.ItemSlot.Weapon && this.teamAtO[index1].Weapon != "" || _slot == Enums.ItemSlot.Armor && this.teamAtO[index1].Armor != "" || _slot == Enums.ItemSlot.Jewelry && this.teamAtO[index1].Jewelry != "" || _slot == Enums.ItemSlot.Accesory && this.teamAtO[index1].Accesory != "" || _slot == Enums.ItemSlot.Pet && this.teamAtO[index1].Pet != "");
  }

  public void CorruptItemSlot(int _heroIndex, string _slot)
  {
    CardData cardData = (CardData) null;
    if (_slot == "weapon" && this.teamAtO[_heroIndex].Weapon != "")
      cardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(this.teamAtO[_heroIndex].Weapon, false), "");
    else if (_slot == "armor" && this.teamAtO[_heroIndex].Armor != "")
      cardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(this.teamAtO[_heroIndex].Armor, false), "");
    else if (_slot == "jewelry" && this.teamAtO[_heroIndex].Jewelry != "")
      cardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(this.teamAtO[_heroIndex].Jewelry, false), "");
    else if (_slot == "accesory" && this.teamAtO[_heroIndex].Accesory != "")
      cardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(this.teamAtO[_heroIndex].Accesory, false), "");
    else if (_slot == "pet" && this.teamAtO[_heroIndex].Pet != "")
      cardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(this.teamAtO[_heroIndex].Pet, false), "");
    if (!((UnityEngine.Object) cardData != (UnityEngine.Object) null) || !((UnityEngine.Object) cardData.UpgradesToRare != (UnityEngine.Object) null))
      return;
    if (!GameManager.Instance.IsMultiplayer())
      this.AddItemToHero(_heroIndex, cardData.UpgradesToRare.Id);
    else
      this.AddItemToHeroMP(_heroIndex, cardData.UpgradesToRare.Id);
  }

  [PunRPC]
  public void AddItemToHero(int _heroIndex, string _cardName, string _itemListId = "")
  {
    CardData cardData = Globals.Instance.GetCardData(_cardName, false);
    string _cardId = _cardName;
    if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
    {
      if (cardData.CardType == Enums.CardType.Weapon)
      {
        if (!((UnityEngine.Object) cardData.UpgradesToRare != (UnityEngine.Object) null) || !(cardData.UpgradesToRare.Id == this.teamAtO[_heroIndex].Weapon))
        {
          if (_cardName == this.teamAtO[_heroIndex].Weapon)
            _cardName = Functions.GetCardDataFromCardData(cardData, "RARE").Id;
          else
            this.RemoveItemFromHero(true, _heroIndex, "weapon");
          this.teamAtO[_heroIndex].Weapon = _cardName;
        }
      }
      else if (cardData.CardType == Enums.CardType.Armor)
      {
        if (!((UnityEngine.Object) cardData.UpgradesToRare != (UnityEngine.Object) null) || !(cardData.UpgradesToRare.Id == this.teamAtO[_heroIndex].Armor))
        {
          if (_cardName == this.teamAtO[_heroIndex].Armor)
            _cardName = Functions.GetCardDataFromCardData(cardData, "RARE").Id;
          else
            this.RemoveItemFromHero(true, _heroIndex, "armor");
          this.teamAtO[_heroIndex].Armor = _cardName;
        }
      }
      else if (cardData.CardType == Enums.CardType.Jewelry)
      {
        if (!((UnityEngine.Object) cardData.UpgradesToRare != (UnityEngine.Object) null) || !(cardData.UpgradesToRare.Id == this.teamAtO[_heroIndex].Jewelry))
        {
          if (_cardName == this.teamAtO[_heroIndex].Jewelry)
            _cardName = Functions.GetCardDataFromCardData(cardData, "RARE").Id;
          else
            this.RemoveItemFromHero(true, _heroIndex, "jewelry");
          this.teamAtO[_heroIndex].Jewelry = _cardName;
        }
      }
      else if (cardData.CardType == Enums.CardType.Accesory)
      {
        if (!((UnityEngine.Object) cardData.UpgradesToRare != (UnityEngine.Object) null) || !(cardData.UpgradesToRare.Id == this.teamAtO[_heroIndex].Accesory))
        {
          if (_cardName == this.teamAtO[_heroIndex].Accesory)
            _cardName = Functions.GetCardDataFromCardData(cardData, "RARE").Id;
          else
            this.RemoveItemFromHero(true, _heroIndex, "accesory");
          this.teamAtO[_heroIndex].Accesory = _cardName;
        }
      }
      else if (cardData.CardType == Enums.CardType.Pet)
      {
        if (!((UnityEngine.Object) cardData.UpgradesToRare != (UnityEngine.Object) null) || !(cardData.UpgradesToRare.Id == this.teamAtO[_heroIndex].Pet))
        {
          if (_cardName == this.teamAtO[_heroIndex].Pet)
            _cardName = Functions.GetCardDataFromCardData(cardData, "RARE").Id;
          else
            this.RemoveItemFromHero(true, _heroIndex, "pet");
          this.teamAtO[_heroIndex].Pet = _cardName;
        }
      }
      else if (cardData.CardType == Enums.CardType.Enchantment)
        this.teamAtO[_heroIndex].AssignEnchantment(_cardName);
      else
        this.teamAtO[_heroIndex].Corruption = "";
      this.teamAtO[_heroIndex].ResetItemDataBySlotCache();
      this.teamAtO[_heroIndex].ClearCaches();
    }
    if (!(bool) (UnityEngine.Object) TownManager.Instance || cardData.CardType != Enums.CardType.Pet)
      PlayerManager.Instance.CardUnlock(_cardName);
    ItemData itemData = cardData.Item;
    if (itemData.MaxHealth != 0)
      this.teamAtO[_heroIndex].ModifyMaxHP(itemData.MaxHealth);
    this.teamAtO[_heroIndex].SetEvent(Enums.EventActivation.CharacterAssign, auxString: _cardName);
    if (_itemListId != "")
      this.SaveBoughtItem(_heroIndex, _itemListId, _cardId);
    this.teamAtO[_heroIndex].ResetItemDataBySlotCache();
    this.teamAtO[_heroIndex].ClearCaches();
    this.ClearCacheGlobalACModification();
    if (!(bool) (UnityEngine.Object) TownManager.Instance || GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
      return;
    this.SaveGame();
  }

  public void AddItemToHeroMP(int _heroIndex, string _itemName, string _itemListId = "") => this.photonView.RPC("AddItemToHero", RpcTarget.All, (object) _heroIndex, (object) _itemName, (object) _itemListId);

  public void RemoveItemFromHeroFromEvent(bool _isHero, int _heroIndex, string _slot, string _id = "")
  {
    if (!GameManager.Instance.IsMultiplayer())
      this.RemoveItemFromHero(_isHero, _heroIndex, _slot, _id);
    else
      this.RemoveItemFromHeroMP(_isHero, _heroIndex, _slot, _id);
  }

  public void RemoveItemFromHeroMP(bool _isHero, int _heroIndex, string _slot, string _id = "") => this.photonView.RPC("RemoveItemFromHero", RpcTarget.All, (object) _isHero, (object) _heroIndex, (object) _slot, (object) _id);

  [PunRPC]
  public void RemoveItemFromHero(bool _isHero, int _heroIndex, string _slot, string _id = "")
  {
    Character character;
    if (!_isHero)
    {
      if (!(bool) (UnityEngine.Object) MatchManager.Instance)
        return;
      NPC[] teamNpc = MatchManager.Instance.GetTeamNPC();
      if (teamNpc == null || _heroIndex >= teamNpc.Length)
        return;
      character = (Character) teamNpc[_heroIndex];
    }
    else
      character = (Character) this.teamAtO[_heroIndex];
    string id = _id.ToLower();
    if (id != "")
    {
      if (character.Weapon == id)
        character.Weapon = "";
      if (character.Armor == id)
        character.Armor = "";
      if (character.Jewelry == id)
        character.Jewelry = "";
      if (character.Accesory == id)
        character.Accesory = "";
      if (character.Pet == id)
        character.Pet = "";
      if (character.Enchantment == id)
      {
        character.Enchantment = "";
        character.ReorganizeEnchantments();
      }
      if (character.Enchantment2 == id)
      {
        character.Enchantment2 = "";
        character.ReorganizeEnchantments();
      }
      if (character.Enchantment3 == id)
      {
        character.Enchantment3 = "";
        character.ReorganizeEnchantments();
      }
    }
    else
    {
      switch (_slot)
      {
        case "weapon":
          if (character.Weapon != "")
          {
            id = character.Weapon;
            character.Weapon = "";
            break;
          }
          break;
        case "armor":
          if (character.Armor != "")
          {
            id = character.Armor;
            character.Armor = "";
            break;
          }
          break;
        case "jewelry":
          if (character.Jewelry != "")
          {
            id = character.Jewelry;
            character.Jewelry = "";
            break;
          }
          break;
        case "accesory":
          if (character.Accesory != "")
          {
            id = character.Accesory;
            character.Accesory = "";
            break;
          }
          break;
        case "pet":
          if (character.Pet != "")
          {
            id = character.Pet;
            character.Pet = "";
            break;
          }
          break;
        case "enchantment":
          if (character.Enchantment != "")
          {
            id = character.Enchantment;
            character.Enchantment = "";
            break;
          }
          break;
        case "enchantment2":
          if (character.Enchantment2 != "")
          {
            id = character.Enchantment2;
            character.Enchantment2 = "";
            break;
          }
          break;
        case "enchantment3":
          if (character.Enchantment3 != "")
          {
            id = character.Enchantment3;
            character.Enchantment3 = "";
            break;
          }
          break;
        default:
          if (character.Corruption != "")
          {
            id = character.Corruption;
            character.Corruption = "";
            break;
          }
          break;
      }
    }
    if (id != "")
    {
      ItemData itemData = Globals.Instance.GetItemData(id);
      if ((UnityEngine.Object) itemData != (UnityEngine.Object) null && itemData.MaxHealth != 0)
        character.DecreaseMaxHP(itemData.MaxHealth);
    }
    character.ResetItemDataBySlotCache();
    character.ClearCaches();
    this.ClearCacheGlobalACModification();
  }

  public void AddPerkToHeroGlobal(int _heroIndex, string _perkId)
  {
    this.AddPerkToHero(_heroIndex, _perkId);
    if (!GameManager.Instance.IsMultiplayer())
      return;
    this.photonView.RPC("NET_AddPerkToHero", RpcTarget.Others, (object) _heroIndex, (object) _perkId);
  }

  public void AddPerkToHeroGlobalList(int _heroIndex, List<string> _perkIdList)
  {
    this.AddPerkToHeroList(_heroIndex, _perkIdList);
    if (!GameManager.Instance.IsMultiplayer())
      return;
    string json = JsonHelper.ToJson<string>(_perkIdList.ToArray());
    this.photonView.RPC("NET_AddPerkToHeroGlobalList", RpcTarget.Others, (object) _heroIndex, (object) json);
  }

  [PunRPC]
  private void NET_AddPerkToHeroGlobalList(int _heroIndex, string _perksJson)
  {
    List<string> list = ((IEnumerable<string>) JsonHelper.FromJson<string>(_perksJson)).ToList<string>();
    this.AddPerkToHeroList(_heroIndex, list);
  }

  private void AddPerkToHeroList(int _heroIndex, List<string> _perkIdList)
  {
    string subclassName = this.teamAtO[_heroIndex].SubclassName;
    if (this.heroPerks.ContainsKey(subclassName))
      this.heroPerks[subclassName] = new List<string>();
    if (this.CharInTown() && this.GetTownTier() == 0)
      this.teamAtO[_heroIndex].InitHP();
    for (int index = 0; index < _perkIdList.Count; ++index)
      this.AddPerkToHero(_heroIndex, _perkIdList[index]);
    if (!this.CharInTown() || this.GetTownTier() != 0)
      return;
    this.teamAtO[_heroIndex].ModifyMaxHP(this.teamAtO[_heroIndex].GetItemsMaxHPModifier());
    this.SideBarRefresh();
  }

  [PunRPC]
  private void NET_AddPerkToHero(int _heroIndex, string _perkId) => this.AddPerkToHero(_heroIndex, _perkId);

  public void AddPerkToHero(int _heroIndex, string _perkId, bool _initHealth = true)
  {
    _perkId = _perkId.ToLower();
    PerkData perkData = Globals.Instance.GetPerkData(_perkId);
    if (!((UnityEngine.Object) perkData != (UnityEngine.Object) null))
      return;
    string subclassName = this.teamAtO[_heroIndex].SubclassName;
    if (!this.heroPerks.ContainsKey(subclassName))
      this.heroPerks.Add(subclassName, new List<string>());
    List<string> stringList = this.heroPerks[subclassName] ?? new List<string>();
    if (!stringList.Contains(perkData.Id))
      stringList.Add(perkData.Id);
    this.heroPerks[subclassName] = stringList;
    this.teamAtO[_heroIndex].PerkList = this.heroPerks[subclassName];
    if (_initHealth && perkData.MaxHealth != 0)
      this.teamAtO[_heroIndex].ModifyMaxHP(perkData.MaxHealth);
    this.teamAtO[_heroIndex].InitEnergy();
    this.teamAtO[_heroIndex].InitSpeed();
    this.teamAtO[_heroIndex].InitResist();
  }

  public void ClearReroll()
  {
    this.shopItemReroll = "";
    if (this.townRerollList != null)
      this.townRerollList.Clear();
    else
      this.townRerollList = new Dictionary<string, string>();
  }

  public void RemoveItemList(string itemListId)
  {
    this.shopList = (Dictionary<string, List<string>>) null;
    this.boughtItems = (Dictionary<string, List<string>>) null;
    this.boughtItemInShopByWho = (Dictionary<string, int>) null;
  }

  public List<string> GetItemList(string itemListId)
  {
    string key = itemListId + "_" + this.gameId;
    if (GameManager.Instance.IsObeliskChallenge())
      this.SetObeliskLootReroll();
    if (this.shopItemReroll != "")
      key = key + "_" + this.shopItemReroll;
    if (this.shopList == null)
      this.shopList = new Dictionary<string, List<string>>();
    if (this.shopList.ContainsKey(key))
      return this.shopList[key];
    List<string> lootItems = Loot.GetLootItems(itemListId, this.shopItemReroll);
    this.shopList.Add(key, lootItems);
    return lootItems;
  }

  public void SaveBoughtItem(int _heroIndex, string _shopId, string _cardId)
  {
    if (this.boughtItems == null)
      this.boughtItems = new Dictionary<string, List<string>>();
    string key1 = _shopId + "_" + this.mapVisitedNodes.Count<string>().ToString();
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("SaveBoughtItem => " + key1);
    if (!this.boughtItems.ContainsKey(key1))
      this.boughtItems.Add(key1, new List<string>());
    if (this.boughtItems[key1].Contains(_cardId))
      return;
    this.boughtItems[key1].Add(_cardId);
    string key2 = _shopId + _cardId;
    if (this.boughtItemInShopByWho == null)
      this.boughtItemInShopByWho = new Dictionary<string, int>();
    if (!this.boughtItemInShopByWho.ContainsKey(key2))
      this.boughtItemInShopByWho.Add(key2, _heroIndex);
    if (GameManager.Instance.IsMultiplayer())
    {
      if (NetworkManager.Instance.IsMaster())
        this.photonView.RPC("NET_SaveBoughtItem", RpcTarget.Others, (object) _heroIndex, (object) _shopId, (object) _cardId);
      else
        this.photonView.RPC("NET_SaveBoughtItem", RpcTarget.MasterClient, (object) _heroIndex, (object) _shopId, (object) _cardId);
    }
    if (!((UnityEngine.Object) CardCraftManager.Instance != (UnityEngine.Object) null) || CardCraftManager.Instance.craftType != 4)
      return;
    CardCraftManager.Instance.ShowItemsForBuy(CardCraftManager.Instance.CurrentItemsPageNum, _cardId);
  }

  [PunRPC]
  private void NET_SaveBoughtItem(int _heroIndex, string _shopId, string _cardId) => this.SaveBoughtItem(_heroIndex, _shopId, _cardId);

  public bool ItemBoughtOnThisShop(string _shopId, string _cardId)
  {
    if (this.boughtItems == null)
      this.boughtItems = new Dictionary<string, List<string>>();
    string key = _shopId + "_" + this.mapVisitedNodes.Count<string>().ToString();
    if (!this.boughtItems.ContainsKey(key))
      this.boughtItems.Add(key, new List<string>());
    return this.boughtItems[key].Contains(_cardId);
  }

  public int WhoBoughtOnThisShop(string _shopId, string _cardId)
  {
    if (this.boughtItems == null)
      this.boughtItems = new Dictionary<string, List<string>>();
    string key1 = _shopId + "_" + this.mapVisitedNodes.Count<string>().ToString();
    if (!this.boughtItems.ContainsKey(key1))
      this.boughtItems.Add(key1, new List<string>());
    if (this.boughtItems[key1].Contains(_cardId))
    {
      string key2 = _shopId + _cardId;
      if (this.boughtItemInShopByWho == null)
        this.boughtItemInShopByWho = new Dictionary<string, int>();
      if (this.boughtItemInShopByWho.ContainsKey(key2))
        return this.boughtItemInShopByWho[key2];
    }
    return -1;
  }

  public void DoLoot(string _lootListId)
  {
    this.lootListId = _lootListId;
    if (GameManager.Instance.IsMultiplayer())
      this.StartCoroutine(this.ShareTeam("Loot"));
    else
      SceneStatic.LoadByName("Loot");
  }

  private IEnumerator DoLootCo(string _lootListId)
  {
    AtOManager atOmanager = this;
    atOmanager.photonView.RPC("NET_ShareLootId", RpcTarget.Others, (object) atOmanager.lootListId);
    while (!NetworkManager.Instance.AllPlayersReady("dolootco"))
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    atOmanager.StartCoroutine(atOmanager.ShareTeam("Loot"));
  }

  [PunRPC]
  private void NET_ShareLootId(string _lootListId)
  {
    this.lootListId = _lootListId;
    NetworkManager.Instance.SetStatusReady("dolootco");
  }

  public void SetLootListId(string _lootListId)
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("SetLootListId ->" + _lootListId);
    this.lootListId = _lootListId;
  }

  public string GetLootListId() => this.lootListId != "" && this.lootListId != null ? this.lootListId : "";

  public void DoCardPlayerGame(CardPlayerPackData _packData)
  {
    this.cardPlayerPackData = _packData;
    if (GameManager.Instance.IsMultiplayer())
      this.StartCoroutine(this.ShareTeam("CardPlayer"));
    else
      SceneStatic.LoadByName("CardPlayer");
  }

  public void DoCardPlayerPairsGame(CardPlayerPairsPackData _packData)
  {
    this.cardPlayerPairsPackData = _packData;
    if (GameManager.Instance.IsMultiplayer())
      this.StartCoroutine(this.ShareTeam("CardPlayerPairs"));
    else
      SceneStatic.LoadByName("CardPlayerPairs");
  }

  public void FinishCardPlayer()
  {
    this.cardPlayerPackData = (CardPlayerPackData) null;
    this.cardPlayerPairsPackData = (CardPlayerPairsPackData) null;
    if (GameManager.Instance.IsMultiplayer())
      this.StartCoroutine(this.ShareTeam("Map"));
    else
      SceneStatic.LoadByName("Map");
  }

  public void FinishLoot()
  {
    this.lootListId = "";
    if (GameManager.Instance.IsMultiplayer())
      this.StartCoroutine(this.ShareTeam("Map"));
    else
      SceneStatic.LoadByName("Map");
  }

  public List<int> GetLootCharacterOrder()
  {
    this.lootCharacterOrder = new List<int>();
    this.lootCharacterOrder.Add(0);
    this.lootCharacterOrder.Add(1);
    this.lootCharacterOrder.Add(2);
    this.lootCharacterOrder.Add(3);
    this.lootCharacterOrder.Shuffle<int>();
    return this.lootCharacterOrder;
  }

  public TierRewardData GetTownDivinationTier() => this.townDivinationTier;

  public void SetTownDivinationTier(int tierNum, string nickCreator = "", int divinationCost = 0)
  {
    if (!((UnityEngine.Object) this.townDivinationTier == (UnityEngine.Object) null))
      return;
    this.townDivinationTier = Globals.Instance.GetTierRewardData(tierNum);
    this.townDivinationCreator = nickCreator;
    this.townDivinationCost = divinationCost;
    if (!GameManager.Instance.IsMultiplayer())
      return;
    this.photonView.RPC("NET_SetTownDivination", RpcTarget.MasterClient, (object) tierNum, (object) nickCreator, (object) divinationCost);
  }

  [PunRPC]
  private void NET_SetTownDivination(int tierNum, string nickCreator, int divinationCost)
  {
    if ((UnityEngine.Object) this.townDivinationTier != (UnityEngine.Object) null && this.townDivinationCreator != nickCreator)
      return;
    this.townDivinationTier = Globals.Instance.GetTierRewardData(tierNum);
    this.townDivinationCreator = nickCreator;
    this.townDivinationCost = divinationCost;
    NetworkManager.Instance.ClearAllPlayerDivinationReady();
    this.photonView.RPC("NET_InviteToTownDivination", RpcTarget.All, (object) tierNum, (object) nickCreator);
  }

  [PunRPC]
  private void NET_InviteToTownDivination(int tierNum, string nickCreator)
  {
    this.townDivinationTier = Globals.Instance.GetTierRewardData(tierNum);
    this.townDivinationCreator = nickCreator;
    TownManager.Instance.ShowJoinDivination();
  }

  public void JoinCardDivination()
  {
    TownManager.Instance.DisableReady();
    TownManager.Instance.characterWindow.Hide(true);
    TownManager.Instance.ShowJoinDivination(false);
    this.DoCardDivination();
    this.photonView.RPC("NET_JoinTownDivination", RpcTarget.MasterClient, (object) NetworkManager.Instance.GetPlayerNick());
  }

  [PunRPC]
  private void NET_JoinTownDivination(string nickPlayer) => NetworkManager.Instance.SetPlayerDivinationReady(nickPlayer);

  public void UpdateDivinationStatus()
  {
    if (!((UnityEngine.Object) this.cardCraftManager != (UnityEngine.Object) null))
      return;
    this.cardCraftManager.RefreshDivinationWaiting();
  }

  public void ResetTownDivination()
  {
    this.townDivinationTier = (TierRewardData) null;
    this.townDivinationCreator = "";
    this.townDivinationCost = 0;
  }

  public void SetCombatData(CombatData _combatData)
  {
    this.SetTeamNPCFromCombatData(_combatData);
    this.currentCombatData = _combatData;
  }

  public void LaunchCombat(CombatData _combatData = null)
  {
    if ((UnityEngine.Object) _combatData != (UnityEngine.Object) null)
    {
      this.SetCombatData(_combatData);
      if (GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
        this.photonView.RPC("NET_SetCombatData", RpcTarget.Others, (object) _combatData.CombatId, (object) this.corruptionAccepted, (object) this.corruptionId);
    }
    if ((UnityEngine.Object) this.fromEventCombatData == (UnityEngine.Object) null)
    {
      this.fromEventCombatData = _combatData;
      this.SaveGame();
    }
    if (GameManager.Instance.IsMultiplayer())
      this.StartCoroutine(this.ShareTeam("Combat"));
    else
      SceneStatic.LoadByName("Combat");
  }

  [PunRPC]
  public void NET_SetCombatData(string _combatId, bool _corruptionAccepted, string _corruptionId)
  {
    this.currentCombatData = Globals.Instance.GetCombatData(_combatId);
    this.corruptionAccepted = _corruptionAccepted;
    this.corruptionId = _corruptionId;
  }

  public void LaunchRewards(bool showMask = true, bool isFromDivination = false)
  {
    if (!GameManager.Instance.IsMultiplayer())
    {
      SceneStatic.LoadByName("Rewards", showMask);
    }
    else
    {
      if (!NetworkManager.Instance.IsMaster())
        return;
      if (isFromDivination)
        this.GivePlayer(0, -this.townDivinationCost, this.townDivinationCreator);
      this.StartCoroutine(this.ShareTeam("Rewards", showMask));
    }
  }

  public void RelaunchRewards()
  {
    if (!GameManager.Instance.IsMultiplayer())
      SceneStatic.LoadByName("Rewards");
    else
      this.StartCoroutine(this.ShareTeam("Rewards"));
  }

  public void FinishRun()
  {
    if (GameManager.Instance.IsMultiplayer())
      this.StartCoroutine(this.ShareTeam(nameof (FinishRun)));
    else
      SceneStatic.LoadByName(nameof (FinishRun));
  }

  public void FinishObeliskMap()
  {
    if (!GameManager.Instance.IsObeliskChallenge())
      return;
    ZoneData zoneData = Globals.Instance.ZoneDataSource[this.GetTownZoneId().ToLower()];
    if (!((UnityEngine.Object) zoneData != (UnityEngine.Object) null) || !zoneData.ObeliskLow)
      return;
    MapManager.Instance.TravelToThisNode(MapManager.Instance.GetNodeFromId(this.obeliskHigh + "_0"));
    this.UpgradeTownTier();
  }

  public void SetNgPlus(int _ngPlus) => this.ngPlus = _ngPlus;

  public int GetNgPlus(bool _fromSave = false) => !this.IsZoneAffectedByMadness() && !_fromSave ? 0 : this.ngPlus;

  public void SetObeliskMadness(int _value)
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("*** SET OBELISK MADNESS **** " + _value.ToString(), "trace");
    this.obeliskMadness = _value;
    if (!GameManager.Instance.IsObeliskChallenge() || GameManager.Instance.IsWeeklyChallenge())
      return;
    this.SetObeliskMadnessTraits(this.obeliskMadness);
  }

  public int GetObeliskMadness() => this.obeliskMadness;

  public string GetMadnessCorruptors() => this.madnessCorruptors;

  public int GetMadnessDifficulty() => MadnessManager.Instance.CalculateMadnessTotal(this.ngPlus, this.madnessCorruptors);

  public void SetMadnessCorruptors(string corruptors) => this.madnessCorruptors = corruptors;

  public void SetAdventureCompleted(bool state) => this.adventureCompleted = state;

  public bool IsAdventureCompleted() => this.adventureCompleted;

  public void FinishGame()
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("//FinishGAME", "trace");
    this.SetAdventureCompleted(true);
    PlayerManager.Instance.AchievementUnlock("MISC_HERO");
    if (this.ngPlus > 0)
      PlayerManager.Instance.AchievementUnlock("MISC_LEGENDARYHERO");
    PlayerManager.Instance.IncreaseNg();
    if (this.ngPlus == 0)
      SceneStatic.LoadByName("IntroNewGame");
    else
      SceneStatic.LoadByName("FinishRun");
  }

  public void FinishObeliskChallenge()
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("//FinishObelisk", "trace");
    this.SetAdventureCompleted(true);
    if (!GameManager.Instance.IsWeeklyChallenge())
      PlayerManager.Instance.IncreaseObeliskMadness();
    this.FinishRun();
  }

  public void FinishCombat(bool resigned)
  {
    if (this.gameId == "" && GameManager.Instance.GetDeveloperMode())
    {
      SceneStatic.LoadByName("TeamManagement");
    }
    else
    {
      this.combatResigned = resigned;
      if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
        return;
      bool flag = true;
      if (!this.combatResigned)
      {
        for (int index = 0; index < 4; ++index)
        {
          if (this.teamAtO[index] != null && (UnityEngine.Object) this.teamAtO[index].HeroData != (UnityEngine.Object) null && this.teamAtO[index].Alive)
          {
            flag = false;
            break;
          }
        }
      }
      if (!flag)
      {
        for (int _heroIndex = 0; _heroIndex < 4; ++_heroIndex)
        {
          if (this.teamAtO[_heroIndex] != null && !((UnityEngine.Object) this.teamAtO[_heroIndex].HeroData == (UnityEngine.Object) null))
          {
            this.teamAtO[_heroIndex].ResetDataForNewCombat();
            if (!this.teamAtO[_heroIndex].Alive)
            {
              this.teamAtO[_heroIndex].Alive = true;
              this.teamAtO[_heroIndex].HpCurrent = Functions.FuncRoundToInt((float) (this.teamAtO[_heroIndex].Hp * 70 / 100));
              ++this.teamAtO[_heroIndex].TotalDeaths;
              if (this.teamAtO[_heroIndex].Cards != null)
                this.AddCardToHero(_heroIndex, "DeathsDoor");
            }
          }
        }
        PopupManager.Instance.ClosePopup();
        if (this.currentMapNode == "tutorial_1")
        {
          if (!this.combatResigned)
            this.LaunchMap();
          else
            this.FinishRun();
        }
        else if (this.CinematicId != "")
          this.LaunchCinematic();
        else if (this.currentMapNode == "of1_10" || this.currentMapNode == "of2_10")
        {
          this.SetCombatCorruptionForScore();
          this.ClearCorruption();
          this.LaunchMap();
        }
        else if ((UnityEngine.Object) this.combatThermometerData != (UnityEngine.Object) null)
        {
          if (this.combatThermometerData.CardReward)
            this.LaunchRewards(false);
          else
            this.LaunchMap();
        }
        else
          this.LaunchMap();
      }
      else
        this.FinishRun();
    }
  }

  public void DoCardUpgrade(int discount = 0, int maxQuantity = -1)
  {
    if (maxQuantity == 0)
      maxQuantity = -1;
    this.StartCoroutine(this.DoCraftHealerCo(discount: discount, maxQuantity: maxQuantity));
  }

  public void DoCardHealer(int discount = 0, int maxQuantity = -1)
  {
    if (maxQuantity == 0)
      maxQuantity = -1;
    this.StartCoroutine(this.DoCraftHealerCo(1, discount, maxQuantity));
  }

  public void DoCardCorruption(int discount = 0, int maxQuantity = -1)
  {
    if (maxQuantity == 0)
      maxQuantity = -1;
    this.StartCoroutine(this.DoCraftHealerCo(6, discount, maxQuantity));
  }

  public void DoCardCraft(int discount = 0, int maxQuantity = -1, Enums.CardRarity maxCraftRarity = Enums.CardRarity.Common)
  {
    if (maxQuantity == 0)
      maxQuantity = -1;
    this.StartCoroutine(this.DoCraftHealerCo(2, discount, maxQuantity, maxCraftRarity: maxCraftRarity));
  }

  public void DoCardDivination(int discount = 0, int maxQuantity = -1)
  {
    if (maxQuantity == 0)
      maxQuantity = -1;
    this.StartCoroutine(this.DoCraftHealerCo(3, discount, maxQuantity));
  }

  public void DoItemShop(string shopListId, int discount = 0) => this.StartCoroutine(this.DoCraftHealerCo(4, discount, itemListId: shopListId));

  public void DoChallengeShop() => this.StartCoroutine(this.DoCraftHealerCo(5));

  public string GetTownItemListId()
  {
    string townItemListId = "towntier" + this.GetTownTier().ToString();
    if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_5_6"))
      townItemListId += "_b";
    else if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_5_3"))
      townItemListId += "_a";
    return townItemListId;
  }

  public void GenerateTownItemList() => this.GetItemList(this.GetTownItemListId());

  private IEnumerator DoCraftHealerCo(
    int type = 0,
    int discount = 0,
    int maxQuantity = -1,
    string itemListId = "",
    Enums.CardRarity maxCraftRarity = Enums.CardRarity.Common)
  {
    if ((UnityEngine.Object) this.cardCraftManager != (UnityEngine.Object) null)
    {
      this.CloseCardCraft();
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    }
    if ((bool) (UnityEngine.Object) MapManager.Instance)
      MapManager.Instance.ShowMask(false);
    this.cardcraftGO = UnityEngine.Object.Instantiate<GameObject>(this.cardcratPrefab, new Vector3(0.0f, 0.0f, -2f), Quaternion.identity);
    this.cardCraftManager = this.cardcraftGO.GetComponent<CardCraftManager>();
    this.cardCraftManager.SetDiscount(discount);
    if (type != 3)
    {
      if (itemListId == "" && (UnityEngine.Object) TownManager.Instance != (UnityEngine.Object) null)
        itemListId = this.GetTownItemListId();
      this.cardCraftManager.SetItemList(itemListId);
    }
    this.cardCraftManager.ShowCardCraft(type);
    if (type != 3)
    {
      this.cardCraftManager.SetMaxQuantity(maxQuantity);
      this.cardCraftManager.SetMaxCraftRarity(maxCraftRarity);
    }
    yield return (object) null;
  }

  public void CloseCardCraft()
  {
    UnityEngine.Object.Destroy((UnityEngine.Object) this.cardcraftGO);
    this.cardcraftGO = (GameObject) null;
    this.cardCraftManager = (CardCraftManager) null;
    if ((bool) (UnityEngine.Object) MapManager.Instance)
    {
      AudioManager.Instance.DoBSO("Map");
      MapManager.Instance.CloseEventFromEvent();
      MapManager.Instance.sideCharacters.ResetCharacters();
      MapManager.Instance.sideCharacters.InCharacterScreen(false);
      SaveManager.SavePlayerData();
    }
    else if ((bool) (UnityEngine.Object) TownManager.Instance)
    {
      TownManager.Instance.sideCharacters.ResetCharacters();
      TownManager.Instance.ShowButtons(true);
    }
    PopupManager.Instance.ClosePopup();
    GameManager.Instance.CleanTempContainer();
  }

  public List<int> GetConflictResolutionOrder()
  {
    if (this.conflictCharacterOrder == null)
      this.conflictCharacterOrder = new List<int>();
    if (this.conflictCharacterOrder.Count == 0)
    {
      this.conflictCharacterOrder.Add(0);
      this.conflictCharacterOrder.Add(1);
      this.conflictCharacterOrder.Add(2);
      this.conflictCharacterOrder.Add(3);
      this.conflictCharacterOrder.Shuffle<int>();
    }
    else
    {
      int num = this.conflictCharacterOrder[0];
      this.conflictCharacterOrder.RemoveAt(0);
      this.conflictCharacterOrder.Add(num);
    }
    return this.conflictCharacterOrder;
  }

  public void BossKilled(string bossName)
  {
    if (this.bossesKilledName == null)
      this.bossesKilledName = new List<string>();
    if (!this.bossesKilledName.Contains(bossName))
      this.bossesKilledName.Add(bossName);
    ++this.bossesKilled;
  }

  public bool IsBossKilled(string bossName)
  {
    for (int index = 0; index < this.bossesKilledName.Count; ++index)
      Debug.Log((object) ("->" + this.bossesKilledName[index]));
    if (this.bossesKilledName == null)
      return false;
    if (this.bossesKilledName.Contains(bossName))
      return true;
    string str = bossName.ToLower().Replace(" ", "");
    for (int index = 0; index < this.bossesKilledName.Count; ++index)
    {
      if (this.bossesKilledName[index].Contains(str))
        return true;
    }
    return false;
  }

  public void MonsterKilled() => ++this.monstersKilled;

  public void BeginAdventure()
  {
    Debug.Log((object) "AtO Begin Adventure");
    if (this.teamAtO == null || this.teamAtO.Length == 0)
      this.CreateTeam();
    if (GameManager.Instance.IsObeliskChallenge())
      SandboxManager.Instance.DisableSandbox();
    if (!SandboxManager.Instance.IsEnabled())
      this.ClearSandbox();
    else
      SandboxManager.Instance.SaveValuesToAtOManager();
    if (GameManager.Instance.IsMultiplayer())
      this.ShareSandbox();
    if (GameManager.Instance.IsObeliskChallenge())
    {
      this.ngPlus = 0;
      this.madnessCorruptors = "";
    }
    else
    {
      this.obeliskMadness = 0;
      if (GameManager.Instance.GameStatus != Enums.GameStatus.LoadGame && !GameManager.Instance.IsMultiplayer() && GameManager.Instance.IsTutorialGame())
        this.townTutorialStep = 0;
    }
    for (int index1 = 0; index1 < 4; ++index1)
    {
      if (this.teamAtO[index1] != null && (UnityEngine.Object) this.teamAtO[index1].HeroData != (UnityEngine.Object) null && (UnityEngine.Object) this.teamAtO[index1].HeroData.HeroSubClass != (UnityEngine.Object) null)
      {
        for (int index2 = 0; index2 < this.teamAtO[index1].HeroData.HeroSubClass.Traits.Count; ++index2)
        {
          if ((UnityEngine.Object) this.teamAtO[index1].HeroData.HeroSubClass.Traits[index2] != (UnityEngine.Object) null)
            this.teamAtO[index1].AssignTrait(this.teamAtO[index1].HeroData.HeroSubClass.Traits[index2].Id);
        }
        if (!GameManager.Instance.IsObeliskChallenge())
          this.teamAtO[index1].ReassignInitialItem();
      }
    }
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      this.AssignGlobalEventRequirements();
      if (this.ngPlus >= 7)
        this.SetGameId();
      if (GameManager.Instance.IsMultiplayer())
        this.ShareNGPlus();
    }
    else
    {
      if (!GameManager.Instance.IsWeeklyChallenge() && this.obeliskMadness >= 8)
        this.SetGameId();
      this.SetObeliskNodes();
      if (!GameManager.Instance.IsWeeklyChallenge() && GameManager.Instance.IsMultiplayer())
        this.ShareObeliskMadnessAndMaps();
    }
    if (this.GameLoaded)
      return;
    for (int index = 0; index < 4; ++index)
    {
      if (this.teamAtO[index] != null && (UnityEngine.Object) this.teamAtO[index].HeroData != (UnityEngine.Object) null && (UnityEngine.Object) this.teamAtO[index].HeroData.HeroSubClass != (UnityEngine.Object) null)
        this.teamAtO[index].AssignTrait(this.teamAtO[index].HeroData.HeroSubClass.Trait0.Id);
    }
    if (GameManager.Instance.IsMultiplayer())
      this.InitGameMP();
    else
      this.InitGame();
    this.SetTownTier(0);
    if (GameManager.Instance.IsObeliskChallenge())
      this.SetCurrentNode(this.obeliskLow + "_0");
    else if (!GameManager.Instance.IsMultiplayer() && this.IsFirstGame())
      this.SetCurrentNode("tutorial_0");
    else
      this.SetCurrentNode("sen_0");
    if (GameManager.Instance.IsMultiplayer())
    {
      if (GameManager.Instance.IsObeliskChallenge())
        this.StartCoroutine(this.ShareTeam("ChallengeSelection", setOwners: true));
      else
        this.StartCoroutine(this.ShareTeam("IntroNewGame", setOwners: true));
    }
    else
    {
      if (GameManager.Instance.IsMultiplayer())
        return;
      if (GameManager.Instance.IsObeliskChallenge())
        SceneStatic.LoadByName("ChallengeSelection");
      else
        SceneStatic.LoadByName("IntroNewGame");
    }
  }

  public void LaunchCinematic()
  {
    if (!GameManager.Instance.IsMultiplayer())
    {
      SceneStatic.LoadByName("Cinematic");
    }
    else
    {
      if (!NetworkManager.Instance.IsMaster())
        return;
      this.StartCoroutine(this.ShareTeam("Cinematic"));
    }
  }

  public void LaunchMap()
  {
    if (!((UnityEngine.Object) MapManager.Instance == (UnityEngine.Object) null))
      return;
    if (!GameManager.Instance.IsMultiplayer())
    {
      SceneStatic.LoadByName("Map");
    }
    else
    {
      if (!NetworkManager.Instance.IsMaster())
        return;
      this.StartCoroutine(this.ShareTeam("Map"));
    }
  }

  public void SetPlayerPerks(Dictionary<string, List<string>> _playerHeroPerks, string[] teamString)
  {
    this.heroPerks = new Dictionary<string, List<string>>();
    if (!GameManager.Instance.IsMultiplayer())
    {
      for (int index = 0; index < 4; ++index)
      {
        string lower = teamString[index].ToLower();
        if (lower != "")
        {
          List<string> heroPerks = PlayerManager.Instance.GetHeroPerks(lower, true);
          this.heroPerks.Add(lower, heroPerks);
        }
      }
    }
    else
    {
      for (int index = 0; index < 4; ++index)
      {
        string lower1 = teamString[index].ToLower();
        if (lower1 != "")
        {
          string lower2 = (NetworkManager.Instance.PlayerHeroPositionOwner[index] + "_" + lower1).ToLower();
          if (_playerHeroPerks.ContainsKey(lower2))
            this.heroPerks.Add(lower1, _playerHeroPerks[lower2]);
        }
      }
    }
  }

  public void SetPlayerPerksChallenge(Dictionary<int, List<int>> heroPerksDict)
  {
    this.heroPerks = new Dictionary<string, List<string>>();
    for (int key = 0; key < 4; ++key)
    {
      if (this.teamAtO[key] != null && !((UnityEngine.Object) this.teamAtO[key].HeroData == (UnityEngine.Object) null))
      {
        string subclassName = this.teamAtO[key].SubclassName;
        List<PerkData> perkDataClass = Globals.Instance.GetPerkDataClass((Enums.CardClass) Enum.Parse(typeof (Enums.CardClass), Enum.GetName(typeof (Enums.HeroClass), (object) this.teamAtO[key].HeroData.HeroClass)));
        SortedDictionary<int, PerkData> sortedDictionary = new SortedDictionary<int, PerkData>();
        for (int index = 0; index < perkDataClass.Count; ++index)
        {
          if (perkDataClass[index].ObeliskPerk && !sortedDictionary.ContainsKey(perkDataClass[index].Level))
            sortedDictionary.Add(perkDataClass[index].Level, perkDataClass[index]);
        }
        List<string> stringList = new List<string>();
        foreach (KeyValuePair<int, PerkData> keyValuePair in sortedDictionary)
        {
          if (heroPerksDict[key].Contains(keyValuePair.Key - 1))
            stringList.Add(keyValuePair.Value.Id);
        }
        this.heroPerks.Add(subclassName, stringList);
      }
    }
  }

  public void ClearTemporalNodeScore()
  {
    this.totalScoreTMP = 0;
    this.mapVisitedNodesTMP = 0;
    this.experienceGainedTMP = 0;
    this.totalDeathsTMP = 0;
    this.combatExpertiseTMP = 0;
    this.bossesKilledTMP = 0;
    this.corruptionCommonCompletedTMP = 0;
    this.corruptionUncommonCompletedTMP = 0;
    this.corruptionRareCompletedTMP = 0;
    this.corruptionEpicCompletedTMP = 0;
  }

  public void SetCombatCorruptionForScore()
  {
    if (!this.corruptionAccepted)
      return;
    CardData cardData = Globals.Instance.GetCardData(this.corruptionIdCard, false);
    if (!((UnityEngine.Object) cardData != (UnityEngine.Object) null))
      return;
    if (cardData.CardRarity == Enums.CardRarity.Common)
      ++this.corruptionCommonCompleted;
    else if (cardData.CardRarity == Enums.CardRarity.Uncommon)
      ++this.corruptionUncommonCompleted;
    else if (cardData.CardRarity == Enums.CardRarity.Rare)
      ++this.corruptionRareCompleted;
    else
      ++this.corruptionEpicCompleted;
  }

  public void NodeScore()
  {
    if (this.teamAtO == null)
      return;
    bool flag = this.mapVisitedNodesTMP == 0;
    int num1 = 0;
    for (int index = 0; index < this.mapVisitedNodes.Count; ++index)
    {
      if ((UnityEngine.Object) Globals.Instance.GetNodeData(this.mapVisitedNodes[index]) != (UnityEngine.Object) null && (UnityEngine.Object) Globals.Instance.GetNodeData(this.mapVisitedNodes[index]).NodeZone != (UnityEngine.Object) null && !Globals.Instance.GetNodeData(this.mapVisitedNodes[index]).NodeZone.DisableExperienceOnThisZone)
        ++num1;
    }
    int num2 = num1 - this.mapVisitedNodesTMP;
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      if (num1 < 2)
      {
        this.mapVisitedNodesTMP = 0;
        num2 = 0;
      }
      else
      {
        if (this.mapVisitedNodesTMP == 0)
          num2 -= 2;
        this.mapVisitedNodesTMP = num1;
      }
    }
    else if (num1 < 1)
    {
      this.mapVisitedNodesTMP = 0;
      num2 = 0;
    }
    else
    {
      if (this.mapVisitedNodesTMP == 0)
        --num2;
      this.mapVisitedNodesTMP = num1;
    }
    int num3 = num2 * 36;
    int num4 = this.combatExpertise - this.combatExpertiseTMP;
    this.combatExpertiseTMP = this.combatExpertise;
    int num5 = num4;
    if (num5 < 0)
      num5 = 0;
    int num6 = num5 * 13;
    int num7 = 0;
    int num8 = 0;
    if (this.teamAtO != null)
    {
      for (int index = 0; index < this.teamAtO.Length; ++index)
      {
        if (this.teamAtO[index] != null && !((UnityEngine.Object) this.teamAtO[index].HeroData == (UnityEngine.Object) null))
        {
          num7 += this.teamAtO[index].Experience;
          num8 += this.teamAtO[index].TotalDeaths;
        }
      }
    }
    int num9 = num7 - this.experienceGainedTMP;
    this.experienceGainedTMP = num7;
    int num10 = Functions.FuncRoundToInt((float) num9 * 0.5f);
    int num11 = num8 - this.totalDeathsTMP;
    this.totalDeathsTMP = num8;
    int num12 = -num11 * 100;
    int num13 = this.bossesKilled - this.bossesKilledTMP;
    this.bossesKilledTMP = this.bossesKilled;
    int num14 = num13 * 80;
    int num15 = this.corruptionCommonCompleted - this.corruptionCommonCompletedTMP;
    this.corruptionCommonCompletedTMP = this.corruptionCommonCompleted;
    int num16 = this.corruptionUncommonCompleted - this.corruptionUncommonCompletedTMP;
    this.corruptionUncommonCompletedTMP = this.corruptionUncommonCompleted;
    int num17 = this.corruptionRareCompleted - this.corruptionRareCompletedTMP;
    this.corruptionRareCompletedTMP = this.corruptionRareCompleted;
    int num18 = this.corruptionEpicCompleted - this.corruptionEpicCompletedTMP;
    this.corruptionEpicCompletedTMP = this.corruptionEpicCompleted;
    int num19 = num15 * 40 + num16 * 80 + num17 * 130 + num18 * 200;
    int num20 = num3 + num6 + num12 + num10 + num14 + num19;
    if (num20 < 0)
      num20 = 0;
    this.totalScoreTMP += num20;
    if (flag || num20 <= 0)
      return;
    int _quantity = num20;
    if (GameManager.Instance.IsObeliskChallenge())
      _quantity = Functions.FuncRoundToInt((float) num20 * 0.5f);
    for (int _index = 0; _index < 4; ++_index)
    {
      if (this.teamAtO[_index] != null)
        PlayerManager.Instance.ModifyProgress(this.teamAtO[_index].SubclassName, _quantity, _index);
    }
    PlayerManager.Instance.ModifyPlayerRankProgress(_quantity);
  }

  public int CalculateScore(bool _calculateMadnessMultiplier = true, int _auxValue = 0)
  {
    if (this.teamAtO == null)
      return 0;
    int score = this.totalScoreTMP;
    if (_auxValue > 0)
      score += _auxValue;
    if (_calculateMadnessMultiplier)
    {
      int level = 0;
      if (!GameManager.Instance.IsObeliskChallenge())
        level = this.GetMadnessDifficulty();
      else if (!GameManager.Instance.IsWeeklyChallenge())
        level = this.GetObeliskMadness();
      int madnessScoreMultiplier = Functions.GetMadnessScoreMultiplier(level, !GameManager.Instance.IsObeliskChallenge());
      score += Functions.FuncRoundToInt((float) (score * madnessScoreMultiplier / 100));
    }
    if (score < 0)
      score = 0;
    return score;
  }

  public void SetWeekly(int _weekly)
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("SetWeekly(" + _weekly.ToString() + ")", "trace");
    this.weekly = _weekly;
    if (this.weekly <= 0)
      return;
    if (this.ChallengeTraits == null)
      this.ChallengeTraits = new List<string>();
    this.ChallengeTraits.Add("energizedheroeslow");
    ChallengeData weeklyData = Globals.Instance.GetWeeklyData(_weekly);
    if (!((UnityEngine.Object) weeklyData != (UnityEngine.Object) null) || weeklyData.Traits == null)
      return;
    for (int index = 0; index < weeklyData.Traits.Count; ++index)
    {
      string lower = weeklyData.Traits[index].Id.ToLower();
      if (!this.ChallengeTraits.Contains(lower))
        this.ChallengeTraits.Add(lower);
    }
  }

  public int GetWeekly() => this.weekly;

  public string GetWeeklyName(int _week)
  {
    ChallengeData weeklyData = Globals.Instance.GetWeeklyData(_week);
    return !((UnityEngine.Object) weeklyData != (UnityEngine.Object) null) || !(weeklyData.IdSteam != "") || !(Texts.Instance.GetText(weeklyData.IdSteam) != "") ? string.Format(Texts.Instance.GetText("weekNumber"), (object) _week) : Texts.Instance.GetText(weeklyData.IdSteam);
  }

  public void SetObeliskMadnessTraits(int _madnessLevel)
  {
    this.ChallengeTraits = new List<string>();
    foreach (KeyValuePair<string, ChallengeTrait> keyValuePair in Globals.Instance.ChallengeTraitsSource)
    {
      if (keyValuePair.Value.IsMadnessTrait && keyValuePair.Value.Order <= _madnessLevel)
        this.ChallengeTraits.Add(keyValuePair.Value.Id);
    }
    if (_madnessLevel >= 0)
      this.ChallengeTraits.Add("energizedheroeslow");
    if (_madnessLevel < 8)
      return;
    this.ChallengeTraits.Add("energizedheroes");
  }

  public void SetSkinIntoSubclassData(string _subclass, string _skinId)
  {
    SubClassData subClassData = Globals.Instance.GetSubClassData(_subclass);
    if (!((UnityEngine.Object) subClassData != (UnityEngine.Object) null))
      return;
    SkinData skinData = Globals.Instance.GetSkinData(_skinId);
    if (!((UnityEngine.Object) skinData != (UnityEngine.Object) null))
      return;
    subClassData.GameObjectAnimated = skinData.SkinGo;
    subClassData.SpriteBorder = skinData.SpriteSiluetaGrande;
    subClassData.SpriteBorderSmall = skinData.SpriteSilueta;
    subClassData.SpriteSpeed = skinData.SpritePortrait;
    subClassData.SpritePortrait = skinData.SpritePortraitGrande;
  }

  public bool NodeIsObeliskLow()
  {
    if (!GameManager.Instance.IsObeliskChallenge())
      return false;
    NodeData nodeData = Globals.Instance.GetNodeData(this.currentMapNode);
    return (UnityEngine.Object) nodeData != (UnityEngine.Object) null && (UnityEngine.Object) nodeData.NodeZone != (UnityEngine.Object) null && nodeData.NodeZone.ZoneId.ToLower() == this.obeliskLow;
  }

  public bool NodeIsObeliskFinal()
  {
    if (!GameManager.Instance.IsObeliskChallenge())
      return false;
    NodeData nodeData = Globals.Instance.GetNodeData(this.currentMapNode);
    return (UnityEngine.Object) nodeData != (UnityEngine.Object) null && (UnityEngine.Object) nodeData.NodeZone != (UnityEngine.Object) null && nodeData.NodeZone.ZoneId.ToLower() == this.obeliskFinal;
  }

  public bool IsChallengeTraitActive(string _trait)
  {
    if (!GameManager.Instance.IsObeliskChallenge() || this.ChallengeTraits == null || !this.ChallengeTraits.Contains(_trait.ToLower()))
      return false;
    if (this.ChallengeTraits.Contains("toughermonsters"))
    {
      if (new List<string>()
      {
        "fastmonsters",
        "vigorousmonsters",
        "resistantmonsters"
      }.Contains(_trait.ToLower()))
        return false;
    }
    return true;
  }

  public int ModifyQuantityObeliskTraits(int type, int quantity)
  {
    if (type == 0)
    {
      if (MadnessManager.Instance.IsMadnessTraitActive("poverty") || this.IsChallengeTraitActive("poverty"))
      {
        if (!GameManager.Instance.IsObeliskChallenge())
          quantity -= Functions.FuncRoundToInt((float) quantity * 0.5f);
        else
          quantity -= Functions.FuncRoundToInt((float) quantity * 0.3f);
        if (quantity < 0)
          quantity = 0;
      }
      if (this.IsChallengeTraitActive("prosperity"))
        quantity += Functions.FuncRoundToInt((float) quantity * 0.5f);
    }
    else
    {
      if (MadnessManager.Instance.IsMadnessTraitActive("poverty") || this.IsChallengeTraitActive("poverty"))
      {
        if (!GameManager.Instance.IsObeliskChallenge())
          quantity -= Functions.FuncRoundToInt((float) quantity * 0.5f);
        else
          quantity -= Functions.FuncRoundToInt((float) quantity * 0.3f);
        if (quantity < 0)
          quantity = 0;
      }
      if (this.IsChallengeTraitActive("prosperity"))
        quantity += Functions.FuncRoundToInt((float) quantity * 0.5f);
    }
    return quantity;
  }

  public void IncreaseTownTutorialStep() => ++this.townTutorialStep;

  public void GoSandboxFromMadness()
  {
    if (MadnessManager.Instance.IsActive())
      MadnessManager.Instance.ShowMadness();
    if (SandboxManager.Instance.IsActive())
      return;
    SandboxManager.Instance.ShowSandbox();
  }

  public void GoMadnessFromSandbox()
  {
    if (SandboxManager.Instance.IsActive())
      SandboxManager.Instance.CloseSandbox();
    if (MadnessManager.Instance.IsActive())
      return;
    MadnessManager.Instance.ShowMadness();
  }

  public int TownTutorialStep
  {
    get => this.townTutorialStep;
    set => this.townTutorialStep = value;
  }

  public int Sandbox_startingEnergy
  {
    get => this.sandbox_startingEnergy;
    set => this.sandbox_startingEnergy = value;
  }

  public int Sandbox_startingSpeed
  {
    get => this.sandbox_startingSpeed;
    set => this.sandbox_startingSpeed = value;
  }

  public int Sandbox_additionalGold
  {
    get => this.sandbox_additionalGold;
    set => this.sandbox_additionalGold = value;
  }

  public int Sandbox_additionalShards
  {
    get => this.sandbox_additionalShards;
    set => this.sandbox_additionalShards = value;
  }

  public int Sandbox_cardCraftPrice
  {
    get => this.sandbox_cardCraftPrice;
    set => this.sandbox_cardCraftPrice = value;
  }

  public int Sandbox_cardUpgradePrice
  {
    get => this.sandbox_cardUpgradePrice;
    set => this.sandbox_cardUpgradePrice = value;
  }

  public int Sandbox_cardTransformPrice
  {
    get => this.sandbox_cardTransformPrice;
    set => this.sandbox_cardTransformPrice = value;
  }

  public int Sandbox_cardRemovePrice
  {
    get => this.sandbox_cardRemovePrice;
    set => this.sandbox_cardRemovePrice = value;
  }

  public int Sandbox_itemsPrice
  {
    get => this.sandbox_itemsPrice;
    set => this.sandbox_itemsPrice = value;
  }

  public int Sandbox_petsPrice
  {
    get => this.sandbox_petsPrice;
    set => this.sandbox_petsPrice = value;
  }

  public bool Sandbox_allRarities
  {
    get => this.sandbox_allRarities;
    set => this.sandbox_allRarities = value;
  }

  public bool Sandbox_unlimitedAvailableCards
  {
    get => this.sandbox_unlimitedAvailableCards;
    set => this.sandbox_unlimitedAvailableCards = value;
  }

  public bool Sandbox_freeRerolls
  {
    get => this.sandbox_freeRerolls;
    set => this.sandbox_freeRerolls = value;
  }

  public bool Sandbox_unlimitedRerolls
  {
    get => this.sandbox_unlimitedRerolls;
    set => this.sandbox_unlimitedRerolls = value;
  }

  public int Sandbox_divinationsPrice
  {
    get => this.sandbox_divinationsPrice;
    set => this.sandbox_divinationsPrice = value;
  }

  public bool Sandbox_noMinimumDecksize
  {
    get => this.sandbox_noMinimumDecksize;
    set => this.sandbox_noMinimumDecksize = value;
  }

  public bool Sandbox_alwaysPassEventRoll
  {
    get => this.sandbox_alwaysPassEventRoll;
    set => this.sandbox_alwaysPassEventRoll = value;
  }

  public int Sandbox_additionalMonsterHP
  {
    get => this.sandbox_additionalMonsterHP;
    set => this.sandbox_additionalMonsterHP = value;
  }

  public int Sandbox_totalHeroes
  {
    get => this.sandbox_totalHeroes;
    set => this.sandbox_totalHeroes = value;
  }

  public int Sandbox_lessNPCs
  {
    get => this.sandbox_lessNPCs;
    set => this.sandbox_lessNPCs = value;
  }

  public bool Sandbox_doubleChampions
  {
    get => this.sandbox_doubleChampions;
    set => this.sandbox_doubleChampions = value;
  }

  public int Sandbox_additionalMonsterDamage
  {
    get => this.sandbox_additionalMonsterDamage;
    set => this.sandbox_additionalMonsterDamage = value;
  }

  public bool Sandbox_craftUnlocked
  {
    get => this.sandbox_craftUnlocked;
    set => this.sandbox_craftUnlocked = value;
  }
}
