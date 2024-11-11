// Decompiled with JetBrains decompiler
// Type: PlayerManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  [SerializeField]
  private string playerName;
  [SerializeField]
  private string[] lastUsedTeam;
  [SerializeField]
  private List<string> tutorialWatched;
  [SerializeField]
  private List<string> unlockedNodes;
  [SerializeField]
  private List<string> unlockedHeroes;
  [SerializeField]
  private List<string> unlockedCards;
  [SerializeField]
  private List<string> unlockedCardbacks;
  [SerializeField]
  private List<string> playerRuns;
  [SerializeField]
  private List<Hero> playerHeroes;
  [SerializeField]
  private int playerRankProgress;
  [SerializeField]
  private List<string> treasuresClaimed;
  [SerializeField]
  private bool ngUnlocked;
  [SerializeField]
  private int ngLevel;
  [SerializeField]
  private int maxAdventureMadnessLevel;
  [SerializeField]
  private int obeliskMadnessLevel;
  [SerializeField]
  private int bossesKilled;
  [SerializeField]
  private List<string> bossesKilledName;
  [SerializeField]
  private int monstersKilled;
  [SerializeField]
  private int expGained;
  [SerializeField]
  private int cardsCrafted;
  [SerializeField]
  private int cardsUpgraded;
  [SerializeField]
  private int goldGained;
  [SerializeField]
  private int dustGained;
  [SerializeField]
  private int bestScore;
  [SerializeField]
  private int supplyGained;
  [SerializeField]
  private int supplyActual;
  [SerializeField]
  private List<string> supplyBought;
  [SerializeField]
  private int purchasedItems;
  [SerializeField]
  private int corruptionsCompleted;
  [SerializeField]
  private Dictionary<string, string> skinUsed;
  [SerializeField]
  private Dictionary<string, string> cardbackUsed;
  [SerializeField]
  private Dictionary<string, List<string>> unlockedCardsByGame;
  [SerializeField]
  private string[] teamHeroes;
  [SerializeField]
  private string[] teamNPCs;
  [SerializeField]
  private Dictionary<string, int> heroProgress;
  [SerializeField]
  private Dictionary<string, List<string>> heroPerks;
  [SerializeField]
  private Hero[] gameTeamHeroes;
  [SerializeField]
  private PlayerDeck playerSavedDeck;
  [SerializeField]
  private PlayerPerk playerSavedPerk;
  private List<string> achievementsSent = new List<string>();

  public static PlayerManager Instance { get; private set; }

  private void Awake()
  {
    if ((UnityEngine.Object) PlayerManager.Instance == (UnityEngine.Object) null)
      PlayerManager.Instance = this;
    else if ((UnityEngine.Object) PlayerManager.Instance != (UnityEngine.Object) this)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    this.InitPlayerData();
  }

  public void InitPlayerData()
  {
    Debug.Log((object) "Initplayerdata");
    this.unlockedHeroes = new List<string>();
    this.unlockedNodes = new List<string>();
    this.unlockedCards = new List<string>();
    this.unlockedCardbacks = new List<string>();
    this.playerRuns = new List<string>();
    this.playerHeroes = new List<Hero>();
    this.treasuresClaimed = new List<string>();
    this.teamNPCs = new string[4];
    this.heroProgress = new Dictionary<string, int>();
    this.heroPerks = new Dictionary<string, List<string>>();
    this.bossesKilledName = new List<string>();
    this.supplyBought = new List<string>();
    this.playerSavedDeck = new PlayerDeck();
    this.maxAdventureMadnessLevel = 0;
    this.obeliskMadnessLevel = 0;
    this.bossesKilled = 0;
    this.playerRankProgress = 0;
    this.monstersKilled = 0;
    this.expGained = 0;
    this.cardsCrafted = 0;
    this.cardsUpgraded = 0;
    this.goldGained = 0;
    this.dustGained = 0;
    this.bestScore = 0;
    this.supplyGained = 0;
    this.supplyActual = 0;
    this.purchasedItems = 0;
    this.corruptionsCompleted = 0;
    this.ngLevel = 0;
    this.obeliskMadnessLevel = 0;
  }

  public void ClearAdventurePerks()
  {
    foreach (KeyValuePair<string, List<string>> heroPerk in this.HeroPerks)
    {
      for (int index = heroPerk.Value.Count - 1; index >= 0; --index)
      {
        PerkData perkData = Globals.Instance.GetPerkData(heroPerk.Value[index]);
        if ((UnityEngine.Object) perkData != (UnityEngine.Object) null && perkData.CardClass == Enums.CardClass.Special)
        {
          Debug.Log((object) ("PERK REMOVED -> " + perkData.Id));
          heroPerk.Value.RemoveAt(index);
        }
      }
    }
  }

  public int GetCharacterTier(string _subclassId, string _type, int _rank = -1)
  {
    int num = _rank == -1 ? this.GetPerkRank(_subclassId) : _rank;
    switch (_type)
    {
      case "trait":
        if (num >= 44)
          return 2;
        if (num >= 20)
          return 1;
        break;
      case "card":
        if (num >= 36)
          return 2;
        if (num >= 12)
          return 1;
        break;
      case "item":
        if (num >= 28)
          return 2;
        if (num >= 4)
          return 1;
        break;
    }
    return 0;
  }

  public void SetPlayerRankProgress(int _rk)
  {
    int num = Globals.Instance.PerkLevel[Globals.Instance.PerkLevel.Count - 1];
    if (_rk > num)
      _rk = num;
    this.playerRankProgress = _rk;
  }

  public int GetPlayerRankProgress()
  {
    int num = Globals.Instance.PerkLevel[Globals.Instance.PerkLevel.Count - 1];
    if (this.playerRankProgress > num)
      this.playerRankProgress = num;
    return this.playerRankProgress;
  }

  public void ModifyPlayerRankProgress(int _value)
  {
    this.playerRankProgress += _value;
    int num = Globals.Instance.PerkLevel[Globals.Instance.PerkLevel.Count - 1];
    if (this.playerRankProgress <= num)
      return;
    this.playerRankProgress = num;
  }

  public int GetHighestCharacterRank()
  {
    int highestCharacterRank = 0;
    if (this.playerRankProgress > 0)
    {
      for (int index = 0; index < Globals.Instance.PerkLevel.Count && this.playerRankProgress >= Globals.Instance.PerkLevel[index]; ++index)
        ++highestCharacterRank;
    }
    else if (this.heroProgress != null)
    {
      int num = 0;
      foreach (KeyValuePair<string, int> keyValuePair in this.heroProgress)
      {
        if (keyValuePair.Value > num)
        {
          num = keyValuePair.Value;
          highestCharacterRank = this.GetPerkRank(keyValuePair.Key);
        }
      }
      this.playerRankProgress = num;
    }
    return highestCharacterRank;
  }

  public void ResetPerks(string _hero)
  {
    _hero = _hero.ToLower();
    if (this.heroPerks == null || !this.heroPerks.ContainsKey(_hero))
      return;
    this.heroPerks[_hero].Clear();
    SaveManager.SavePlayerData();
    if (!(SceneStatic.GetSceneName() == "HeroSelection"))
      return;
    HeroSelectionManager.Instance.SendHeroPerksMP(_hero);
  }

  public int GetPerkCurrency(string _subclassId)
  {
    if (_subclassId == "" || _subclassId == null)
      return 0;
    List<string> heroPerks = this.GetHeroPerks(_subclassId);
    int perkCurrency = 0;
    if (heroPerks != null)
    {
      for (int index = 0; index < heroPerks.Count; ++index)
        perkCurrency += Perk.GetCurrencyBonus(heroPerks[index]);
    }
    return perkCurrency;
  }

  public int GetPerkShards(string _subclassId)
  {
    if (_subclassId == "" || _subclassId == null)
      return 0;
    List<string> heroPerks = this.GetHeroPerks(_subclassId);
    int perkShards = 0;
    if (heroPerks != null)
    {
      for (int index = 0; index < heroPerks.Count; ++index)
        perkShards += Perk.GetShardsBonus(heroPerks[index]);
    }
    return perkShards;
  }

  public int GetPerkMaxHealth(string _subclassId)
  {
    List<string> heroPerks = this.GetHeroPerks(_subclassId);
    int perkMaxHealth = 0;
    if (heroPerks != null)
    {
      for (int index = 0; index < heroPerks.Count; ++index)
        perkMaxHealth += Perk.GetMaxHealth(heroPerks[index]);
    }
    return perkMaxHealth;
  }

  public int GetPerkSpeed(string _subclassId)
  {
    List<string> heroPerks = this.GetHeroPerks(_subclassId);
    int perkSpeed = 0;
    if (heroPerks != null)
    {
      for (int index = 0; index < heroPerks.Count; ++index)
        perkSpeed += Perk.GetSpeed(heroPerks[index]);
    }
    return perkSpeed;
  }

  public int GetPerkEnergyBegin(string _subclassId)
  {
    List<string> heroPerks = this.GetHeroPerks(_subclassId);
    int perkEnergyBegin = 0;
    if (heroPerks != null)
    {
      for (int index = 0; index < heroPerks.Count; ++index)
        perkEnergyBegin += Perk.GetEnergyBegin(heroPerks[index]);
    }
    return perkEnergyBegin;
  }

  public int GetPerkDamageBonus(string _subclassId, Enums.DamageType _dmgType)
  {
    List<string> heroPerks = this.GetHeroPerks(_subclassId);
    int perkDamageBonus = 0;
    if (heroPerks != null)
    {
      for (int index = 0; index < heroPerks.Count; ++index)
        perkDamageBonus += Perk.GetDamageBonus(heroPerks[index], _dmgType);
    }
    return perkDamageBonus;
  }

  public int GetPerkHealBonus(string _subclassId)
  {
    List<string> heroPerks = this.GetHeroPerks(_subclassId);
    int perkHealBonus = 0;
    if (heroPerks != null)
    {
      for (int index = 0; index < heroPerks.Count; ++index)
        perkHealBonus += Perk.GetHealBonus(heroPerks[index]);
    }
    return perkHealBonus;
  }

  public int GetPerkResistBonus(string _subclassId, Enums.DamageType _resist)
  {
    List<string> heroPerks = this.GetHeroPerks(_subclassId);
    int perkResistBonus = 0;
    if (heroPerks != null)
    {
      for (int index = 0; index < heroPerks.Count; ++index)
        perkResistBonus += Perk.GetResistBonus(heroPerks[index], _resist);
    }
    return perkResistBonus;
  }

  public int GetPerkNextLevelPoints(string _subclassId)
  {
    int num = !(_subclassId != "") ? this.playerRankProgress : this.GetProgress(_subclassId);
    for (int index = 0; index < Globals.Instance.PerkLevel.Count; ++index)
    {
      if (num < Globals.Instance.PerkLevel[index])
        return Globals.Instance.PerkLevel[index];
    }
    return 0;
  }

  public int GetPerkPrevLevelPoints(string _subclassId)
  {
    int num = !(_subclassId != "") ? this.playerRankProgress : this.GetProgress(_subclassId);
    int perkPrevLevelPoints = 0;
    for (int index = 0; index < Globals.Instance.PerkLevel.Count && num >= Globals.Instance.PerkLevel[index]; ++index)
      perkPrevLevelPoints = Globals.Instance.PerkLevel[index];
    return perkPrevLevelPoints;
  }

  public int GetPerkRank(string _subclassId)
  {
    int perkRank = 0;
    int progress = this.GetProgress(_subclassId);
    for (int index = 0; index < Globals.Instance.PerkLevel.Count && progress >= Globals.Instance.PerkLevel[index]; ++index)
      ++perkRank;
    return perkRank;
  }

  public int GetPerkPointsAvailable(string _subclassId)
  {
    int num1 = this.GetHighestCharacterRank();
    if (num1 > Globals.MaxPerkPoints)
      num1 = Globals.MaxPerkPoints;
    List<string> heroPerks = this.GetHeroPerks(_subclassId);
    int num2 = 0;
    foreach (KeyValuePair<string, PerkNodeData> perkNodeData in PerkTree.Instance.GetPerkNodeDatas())
    {
      if ((UnityEngine.Object) perkNodeData.Value != (UnityEngine.Object) null && (UnityEngine.Object) perkNodeData.Value.Perk != (UnityEngine.Object) null && heroPerks != null && heroPerks.Contains(perkNodeData.Value.Perk.Id.ToLower()))
      {
        int pointsForNode = PerkTree.Instance.GetPointsForNode(perkNodeData.Value);
        num2 += pointsForNode;
      }
    }
    int perkPointsAvailable = num1 - num2;
    if (perkPointsAvailable < 0)
      perkPointsAvailable = 0;
    return perkPointsAvailable;
  }

  public void ModifyProgress(string _subclassId, int _quantity, int _index = -1)
  {
    bool flag = true;
    SubClassData subClassData1 = Globals.Instance.GetSubClassData(_subclassId);
    if ((UnityEngine.Object) subClassData1 == (UnityEngine.Object) null)
      return;
    if (!subClassData1.MainCharacter && !subClassData1.ExpansionCharacter && _index > -1)
    {
      flag = false;
      Hero[] teamBackup = AtOManager.Instance.GetTeamBackup();
      if (teamBackup != null && teamBackup.Length > _index && teamBackup[_index] != null)
      {
        SubClassData subClassData2 = Globals.Instance.GetSubClassData(teamBackup[_index].SubclassName);
        if ((UnityEngine.Object) subClassData2 != (UnityEngine.Object) null && (subClassData2.MainCharacter || subClassData2.ExpansionCharacter))
        {
          _subclassId = teamBackup[_index].SubclassName;
          flag = true;
        }
      }
    }
    if (!flag)
      return;
    if (this.heroProgress == null)
      this.heroProgress = new Dictionary<string, int>();
    if (this.heroProgress.ContainsKey(_subclassId))
      this.heroProgress[_subclassId] += _quantity;
    else
      this.heroProgress.Add(_subclassId, _quantity);
    if (this.heroProgress[_subclassId] > Globals.Instance.PerkLevel[Globals.Instance.PerkLevel.Count - 1])
      this.heroProgress[_subclassId] = Globals.Instance.PerkLevel[Globals.Instance.PerkLevel.Count - 1];
    int perkRank = this.GetPerkRank(_subclassId);
    if (perkRank > 4)
      this.AchievementUnlock("MISC_RECRUIT");
    if (perkRank > 19)
      this.AchievementUnlock("MISC_SOLDIER");
    if (perkRank <= 34)
      return;
    this.AchievementUnlock("MISC_VETERAN");
  }

  public int GetProgress(string _subclassId)
  {
    _subclassId = _subclassId.ToLower();
    if (this.heroProgress == null)
      this.heroProgress = new Dictionary<string, int>();
    return this.heroProgress.ContainsKey(_subclassId) ? this.heroProgress[_subclassId] : 0;
  }

  public void AssignPerkList(string _subclassId, List<string> _perks)
  {
    Debug.Log((object) ("AssignPerkList ->" + _subclassId));
    if (!this.heroPerks.ContainsKey(_subclassId))
      this.heroPerks.Add(_subclassId, new List<string>());
    this.heroPerks[_subclassId] = _perks;
    SaveManager.SavePlayerData();
    if (!(SceneStatic.GetSceneName() == "HeroSelection"))
      return;
    HeroSelectionManager.Instance.SendHeroPerksMP(_subclassId);
  }

  public void AssignPerk(string _subclassId, string _perk, bool _addPerk = true)
  {
    Debug.Log((object) ("AssignPerk " + _subclassId + " " + _perk));
    _perk = _perk.ToLower();
    if (this.heroPerks.ContainsKey(_subclassId))
    {
      if (_addPerk && !this.heroPerks[_subclassId].Contains(_perk))
        this.heroPerks[_subclassId].Add(_perk);
      if (!_addPerk && this.heroPerks[_subclassId].Contains(_perk))
        this.heroPerks[_subclassId].Remove(_perk);
      PopupManager.Instance.ClosePopup();
    }
    else
    {
      this.heroPerks.Add(_subclassId, new List<string>());
      if (_addPerk)
        this.heroPerks[_subclassId].Add(_perk);
    }
    SaveManager.SavePlayerData();
    if (!(SceneStatic.GetSceneName() == "HeroSelection"))
      return;
    HeroSelectionManager.Instance.SendHeroPerksMP(_subclassId);
  }

  public List<string> GetHeroPerks(string _hero, bool forceFromPlayerManager = false)
  {
    if (_hero == "")
      return (List<string>) null;
    _hero = _hero.ToLower().Split("_", StringSplitOptions.None)[0];
    Hero[] team = AtOManager.Instance.GetTeam();
    Dictionary<string, List<string>> dictionary = (forceFromPlayerManager || team == null || team.Length < 4 || SceneStatic.GetSceneName() == "FinishRun" ? this.heroPerks : AtOManager.Instance.heroPerks) ?? new Dictionary<string, List<string>>();
    return dictionary.ContainsKey(_hero) ? dictionary[_hero] : (List<string>) null;
  }

  public bool HeroHavePerk(string _hero, string _perk)
  {
    _hero = _hero.ToLower();
    _perk = _perk.ToLower();
    if (this.heroPerks == null)
      this.heroPerks = new Dictionary<string, List<string>>();
    return this.heroPerks.ContainsKey(_hero) && this.heroPerks[_hero].Contains(_perk);
  }

  public Dictionary<string, List<string>> GetHeroPerksDictionary() => this.heroPerks;

  public void PreBeginGame()
  {
    this.UnlockInitialHeroes();
    this.CreateSkinDictionary();
    this.CreateCardbackDictionary();
  }

  public void BeginGame()
  {
    this.CardUnlock("bunny");
    this.CardUnlock("scaraby");
    this.CardUnlock("fireball");
    this.CreatePlayer();
    SaveManager.LoadPlayerDeck();
    SaveManager.LoadPlayerPerkConfig();
    if (this.unlockedHeroes != null)
    {
      for (int index = 0; index < this.unlockedHeroes.Count; ++index)
        this.AchievementUnlock("UNLOCK_" + this.unlockedHeroes[index].ToUpper());
    }
    if (this.unlockedCards == null || !Globals.Instance.CardItemByType.ContainsKey(Enums.CardType.Pet))
      return;
    for (int index = 0; index < Globals.Instance.CardItemByType[Enums.CardType.Pet].Count; ++index)
    {
      string str = Globals.Instance.CardItemByType[Enums.CardType.Pet][index];
      if (this.unlockedCards.Contains(str))
      {
        string theAchievement = "EVENT_UNLOCK_" + str.ToUpper();
        if (str == "liante")
          theAchievement = "EVENT_UNLOCK_LIANTA";
        this.AchievementUnlock(theAchievement);
      }
    }
  }

  public void CreatePlayer() => this.playerName = Functions.RandomString(6f);

  public void CreateSkinDictionary()
  {
    this.skinUsed = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (KeyValuePair<string, SubClassData> keyValuePair in Globals.Instance.SubClass)
    {
      string baseIdBySubclass = Globals.Instance.GetSkinBaseIdBySubclass(keyValuePair.Key);
      if (!(baseIdBySubclass == ""))
      {
        this.skinUsed.Add(keyValuePair.Key, baseIdBySubclass);
        AtOManager.Instance.SetSkinIntoSubclassData(keyValuePair.Key, baseIdBySubclass);
      }
    }
  }

  public void RecreateSkins()
  {
    if (this.skinUsed == null)
      return;
    foreach (KeyValuePair<string, SubClassData> keyValuePair in Globals.Instance.SubClass)
    {
      if (this.skinUsed.ContainsKey(keyValuePair.Key))
      {
        string _skinId = this.skinUsed[keyValuePair.Key];
        AtOManager.Instance.SetSkinIntoSubclassData(keyValuePair.Key, _skinId);
      }
    }
  }

  public void SetSkin(string _subclass, string _skinId)
  {
    Debug.Log((object) ("SetSkin " + _subclass + " _ " + _skinId));
    _subclass = _subclass.ToLower();
    if (this.skinUsed == null)
      this.skinUsed = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    if (this.skinUsed.ContainsKey(_subclass))
      this.skinUsed[_subclass] = _skinId;
    else
      this.skinUsed.Add(_subclass, _skinId);
    SaveManager.SavePlayerData();
  }

  public void CreateCardbackDictionary()
  {
    this.cardbackUsed = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (KeyValuePair<string, SubClassData> keyValuePair in Globals.Instance.SubClass)
    {
      string baseIdBySubclass = Globals.Instance.GetCardbackBaseIdBySubclass(keyValuePair.Key);
      if (!(baseIdBySubclass == ""))
        this.cardbackUsed.Add(keyValuePair.Key, baseIdBySubclass);
    }
  }

  public void SetCardback(string _subclass, string _cardbackId)
  {
    _subclass = _subclass.ToLower();
    _cardbackId = _cardbackId.ToLower();
    if (this.cardbackUsed == null)
      this.cardbackUsed = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    if (this.cardbackUsed.ContainsKey(_subclass))
      this.cardbackUsed[_subclass] = _cardbackId;
    else
      this.cardbackUsed.Add(_subclass, _cardbackId);
    SaveManager.SavePlayerData();
  }

  public string GetActiveCardback(string _subclass)
  {
    _subclass = _subclass.ToLower();
    return this.cardbackUsed != null && this.cardbackUsed.ContainsKey(_subclass) ? this.cardbackUsed[_subclass].ToLower() : Globals.Instance.GetCardbackBaseIdBySubclass(_subclass);
  }

  public void SetPlayerName(string _name)
  {
    this.playerName = _name;
    SaveManager.SaveIntoPrefsString("LobbyNick", _name);
  }

  public string GetActiveSkin(string _subclass)
  {
    _subclass = _subclass.ToLower();
    return this.skinUsed != null && this.skinUsed.ContainsKey(_subclass) && this.skinUsed[_subclass] != "" ? this.skinUsed[_subclass] : Globals.Instance.GetSkinBaseIdBySubclass(_subclass);
  }

  public string GetPlayerName() => this.playerName;

  public void UnlockInitialHeroes()
  {
    this.HeroUnlock("mercenary", false, false);
    this.HeroUnlock("cleric", false, false);
    this.HeroUnlock("elementalist", false, false);
    this.HeroUnlock("ranger", false, false);
  }

  public void NormalizePlayerRuns()
  {
  }

  public void NodeUnlock(string nodeName)
  {
    if (this.unlockedNodes == null)
      this.unlockedNodes = new List<string>();
    if (this.unlockedNodes.Contains(nodeName))
      return;
    this.unlockedNodes.Add(nodeName);
    SaveManager.SavePlayerData();
  }

  public bool IsNodeUnlocked(string nodeName) => this.unlockedNodes != null && this.unlockedNodes.Contains(nodeName);

  public void HeroUnlock(string subclass, bool save = true, bool achievement = true)
  {
    if (this.unlockedHeroes == null)
      this.unlockedHeroes = new List<string>();
    if (!this.unlockedHeroes.Contains(subclass))
    {
      this.unlockedHeroes.Add(subclass);
      this.CardsUnlockHero(subclass);
      this.ItemsUnlockHero(subclass);
      if (save)
        SaveManager.SavePlayerData();
    }
    if (!achievement)
      return;
    this.AchievementUnlock("UNLOCK_" + subclass.ToUpper());
  }

  public bool IsHeroUnlocked(string subclass) => this.unlockedHeroes != null && this.unlockedHeroes.Contains(subclass);

  private void CardsUnlockHero(string subclass)
  {
    List<string> cardsId = Globals.Instance.GetSubClassData(subclass).GetCardsId();
    for (int index = 0; index < cardsId.Count; ++index)
      this.CardUnlock(cardsId[index]);
  }

  private void ItemsUnlockHero(string subclass)
  {
    SubClassData subClassData = Globals.Instance.GetSubClassData(subclass);
    if (!((UnityEngine.Object) subClassData.Item != (UnityEngine.Object) null))
      return;
    this.CardUnlock(subClassData.Item.Id);
  }

  public void UnlockHeroes()
  {
    foreach (KeyValuePair<string, SubClassData> keyValuePair in Globals.Instance.SubClass)
      this.HeroUnlock(keyValuePair.Key);
    SaveManager.SavePlayerData();
  }

  public void UnlockCards()
  {
    for (int index = 0; index < Globals.Instance.CardListNotUpgraded.Count; ++index)
      this.CardUnlock(Globals.Instance.CardListNotUpgraded[index]);
    SaveManager.SavePlayerData();
  }

  public void UnlockAll()
  {
    this.UnlockHeroes();
    this.UnlockCards();
    SaveManager.SavePlayerData();
  }

  public void CardUnlock(string cardId, bool save = false, CardItem cardItem = null)
  {
    if (GameManager.Instance.IsObeliskChallenge())
      return;
    if (this.unlockedCards == null)
      this.unlockedCards = new List<string>();
    if (this.unlockedCardsByGame == null)
      this.unlockedCardsByGame = new Dictionary<string, List<string>>();
    string id = "";
    int num1 = 0;
    while (id == "")
    {
      CardData cardData = Globals.Instance.GetCardData(cardId, false);
      if (!((UnityEngine.Object) cardData != (UnityEngine.Object) null))
        return;
      if (cardData.UpgradedFrom.Trim() == "")
        id = cardData.Id;
      else
        cardId = cardData.UpgradedFrom.Trim();
      ++num1;
      if (num1 > 5)
        return;
    }
    if (this.unlockedCards.Contains(id))
      return;
    this.unlockedCards.Add(id);
    if (!AtOManager.Instance.unlockedCards.Contains(id))
      AtOManager.Instance.unlockedCards.Add(id);
    string key = AtOManager.Instance.GetGameId().Trim();
    if (!(key != ""))
      return;
    if (!this.unlockedCardsByGame.ContainsKey(key))
      this.unlockedCardsByGame.Add(key, new List<string>());
    if (!this.unlockedCardsByGame[key].Contains(id))
      this.unlockedCardsByGame[key].Add(id);
    if (save)
      SaveManager.SavePlayerData();
    if ((UnityEngine.Object) cardItem != (UnityEngine.Object) null)
      cardItem.ShowUnlocked();
    CardData cardData1 = Globals.Instance.GetCardData(id, false);
    List<string> stringList = Globals.Instance.CardListNotUpgradedByClass[cardData1.CardClass];
    int count = stringList.Count;
    int num2 = 0;
    for (int index = 0; index < count; ++index)
    {
      if (this.IsCardUnlocked(stringList[index]))
        ++num2;
    }
    string upper = Enum.GetName(typeof (Enums.CardClass), (object) cardData1.CardClass).ToUpper();
    string theAchievement1 = upper + "_CARDS_30";
    if (num2 >= 30)
      this.AchievementUnlock(theAchievement1);
    string theAchievement2 = upper + "_CARDS_60";
    if (num2 >= 60)
      this.AchievementUnlock(theAchievement2);
    string theAchievement3 = upper + "_CARDS_90";
    if (num2 < 90)
      return;
    this.AchievementUnlock(theAchievement3);
  }

  public void AchievementUnlock(string theAchievement)
  {
    if (this.achievementsSent.Contains(theAchievement))
      return;
    SteamManager.Instance.AchievementUnlock(theAchievement);
    this.achievementsSent.Add(theAchievement);
  }

  public void BossKilled(string bossName, string bossNameInGame, string scriptableObjectName)
  {
    ++this.bossesKilled;
    if (this.bossesKilledName == null)
      this.bossesKilledName = new List<string>();
    if (!this.bossesKilledName.Contains(bossName))
      this.bossesKilledName.Add(bossName);
    if (this.bossesKilled > 9)
      this.AchievementUnlock("MISC_PUNISHER");
    if (this.bossesKilled > 49)
      this.AchievementUnlock("MISC_EXECUTIONER");
    string theAchievement1 = "BOSS_" + bossNameInGame.Replace(" ", "").ToUpper();
    this.AchievementUnlock(theAchievement1);
    if (AtOManager.Instance.GetNgPlus() > 0)
      theAchievement1 += "_NG";
    this.AchievementUnlock(theAchievement1);
    if (!(scriptableObjectName != ""))
      return;
    string theAchievement2 = "BOSS_" + scriptableObjectName.Replace(" ", "").ToUpper();
    this.AchievementUnlock(theAchievement2);
    if (AtOManager.Instance.GetNgPlus() > 0)
      theAchievement2 += "_NG";
    this.AchievementUnlock(theAchievement2);
  }

  public void MonsterKilled()
  {
    ++this.monstersKilled;
    if (this.monstersKilled > 19)
      this.AchievementUnlock("MISC_BLOODSHED");
    if (this.monstersKilled > 199)
      this.AchievementUnlock("MISC_MASSACRE");
    if (this.monstersKilled <= 499)
      return;
    this.AchievementUnlock("MISC_EXTERMINATION");
  }

  public void ExpGainedSum(int quantity) => this.expGained += quantity;

  public void GainSupply(int quantity)
  {
    if (this.supplyActual < 500)
    {
      if (this.supplyActual + quantity > 500)
        quantity = 500 - this.supplyActual;
      this.supplyActual += quantity;
    }
    this.supplyGained += quantity;
    PlayerUIManager.Instance.SetSupply(true);
  }

  public void SpendSupply(int quantity)
  {
    if (this.supplyActual < quantity)
      return;
    this.supplyActual -= quantity;
    PlayerUIManager.Instance.SetSupply(true);
  }

  public int GetPlayerSupplyActual() => this.supplyActual;

  public bool PlayerHaveSupply(string supplyId) => !GameManager.Instance.IsObeliskChallenge() && this.supplyBought != null && this.supplyBought.Contains(supplyId);

  public void PlayerBuySupply(string supplyId)
  {
    if (this.supplyBought == null)
      this.supplyBought = new List<string>();
    if (this.supplyBought.Contains(supplyId))
      return;
    int quantity = this.PointsRequiredForSupply(supplyId);
    if (this.supplyActual < quantity)
      return;
    string str = this.SupplyRequiredForSupply(supplyId);
    if (str != "" && !this.supplyBought.Contains(str))
      return;
    this.SpendSupply(quantity);
    this.supplyBought.Add(supplyId);
    if (supplyId == "townUpgrade_6_4")
    {
      if (!GameManager.Instance.IsMultiplayer())
        AtOManager.Instance.AddPlayerRequirement(Globals.Instance.GetRequirementData("caravan"));
      else if (NetworkManager.Instance.IsMaster())
      {
        AtOManager.Instance.AddPlayerRequirement(Globals.Instance.GetRequirementData("caravan"));
        AtOManager.Instance.AddPlayerRequirementOthers("caravan");
      }
    }
    if (this.TotalPointsSpentInSupplys() > 0)
      this.AchievementUnlock("TOWN_TAXPAYER");
    if (this.TotalPointsSpentInSupplys() > 29)
      this.AchievementUnlock("TOWN_MAYOR");
    if (this.TotalPointsSpentInSupplys() > 249)
      this.AchievementUnlock("TOWN_GOVERNOR");
    SaveManager.SavePlayerData();
  }

  public void ResetTownUpgrade()
  {
    this.supplyBought = new List<string>();
    SaveManager.SavePlayerData();
  }

  public string SupplyRequiredForSupply(string supplyId)
  {
    string[] strArray = supplyId.Split('_', StringSplitOptions.None);
    if (strArray.Length > 1)
    {
      int num = int.Parse(strArray[2]);
      if (num > 1)
        return strArray[0] + "_" + strArray[1] + "_" + (num - 1).ToString();
    }
    return "";
  }

  public int PointsRequiredForSupply(string supplyId)
  {
    string[] strArray = supplyId.Split('_', StringSplitOptions.None);
    if (strArray.Length <= 1)
      return 0;
    int num1 = int.Parse(strArray[2]);
    int num2 = 0;
    switch (num1)
    {
      case 1:
        num2 = 1;
        break;
      case 2:
        num2 = 3;
        break;
      case 3:
        num2 = 6;
        break;
      case 4:
        num2 = 9;
        break;
      case 5:
        num2 += 12;
        break;
      case 6:
        num2 = 15;
        break;
    }
    return num2;
  }

  public int TotalPointsSpentInSupplys()
  {
    int num = 0;
    if (this.supplyBought != null)
    {
      for (int index = 0; index < this.supplyBought.Count; ++index)
        num += this.PointsRequiredForSupply(this.supplyBought[index]);
    }
    return num;
  }

  public void PurchasedItem()
  {
    ++this.purchasedItems;
    if (this.purchasedItems > 4)
      this.AchievementUnlock("TOWN_CASUALSHOOPING");
    if (this.purchasedItems > 99)
      this.AchievementUnlock("TOWN_CAPITALISM");
    SaveManager.SavePlayerData();
  }

  public void CorruptionCompleted()
  {
    ++this.corruptionsCompleted;
    SaveManager.SavePlayerData();
  }

  public void CardCrafted()
  {
    ++this.cardsCrafted;
    if (this.cardsCrafted > 4)
      this.AchievementUnlock("TOWN_CRAFTMANASSISTANT");
    if (this.cardsCrafted > 99)
      this.AchievementUnlock("TOWN_CRAFTMAN");
    if (this.cardsCrafted > 249)
      this.AchievementUnlock("TOWN_MASTERCRAFTMAN");
    SaveManager.SavePlayerData();
  }

  public void CardUpgraded()
  {
    ++this.cardsUpgraded;
    if (this.cardsUpgraded > 4)
      this.AchievementUnlock("TOWN_INITIATE");
    if (this.cardsUpgraded > 99)
      this.AchievementUnlock("TOWN_ADEPT");
    if (this.cardsUpgraded > 499)
      this.AchievementUnlock("TOWN_SCHOLAR");
    SaveManager.SavePlayerData();
  }

  public void GoldGainedSum(int quantity, bool save = true)
  {
    this.goldGained += quantity;
    if (this.goldGained > 4999)
      this.AchievementUnlock("MISC_ENTREPRENEUR");
    if (this.goldGained > 99999)
      this.AchievementUnlock("MISC_FILTHYRICH");
    if (!save)
      return;
    SaveManager.SavePlayerData();
  }

  public void DustGainedSum(int quantity, bool save = true)
  {
    this.dustGained += quantity;
    if (this.dustGained > 4999)
      this.AchievementUnlock("MISC_ALCHEMIST");
    if (this.dustGained > 99999)
      this.AchievementUnlock("MISC_SPELLCRAFTER");
    if (!save)
      return;
    SaveManager.SavePlayerData();
  }

  public void SetBestScore(int score) => this.bestScore = score;

  public bool IsCardUnlocked(string _cardId)
  {
    if ((bool) (UnityEngine.Object) CardCraftManager.Instance && !TomeManager.Instance.IsActive() && SandboxManager.Instance.IsEnabled() && AtOManager.Instance.Sandbox_craftUnlocked)
      return true;
    string id = _cardId.ToLower().Split('_', StringSplitOptions.None)[0];
    string str = "";
    while (str == "")
    {
      CardData cardData = Globals.Instance.GetCardData(id, false);
      if (!((UnityEngine.Object) cardData != (UnityEngine.Object) null) || cardData.CardClass == Enums.CardClass.Monster || cardData.CardClass == Enums.CardClass.Special)
        return true;
      if (cardData.UpgradedFrom.Trim() == "")
        str = cardData.Id;
      else
        id = cardData.UpgradedFrom.Trim();
    }
    return this.unlockedCards != null && this.unlockedCards.Contains(str);
  }

  public bool IsCardbackUnlocked(string _cardbackId)
  {
    _cardbackId = _cardbackId.ToLower();
    return this.unlockedCardbacks != null && this.unlockedCardbacks.Contains(_cardbackId);
  }

  public void ClaimTreasure(string id, bool save = true)
  {
    if (this.treasuresClaimed == null)
      this.treasuresClaimed = new List<string>();
    if (this.treasuresClaimed.Contains(id))
      return;
    this.treasuresClaimed.Add(id);
    this.AchievementUnlock("MISC_FIRSTREWARD");
    if (this.treasuresClaimed.Count > 24)
      this.AchievementUnlock("MISC_TREASUREHOARDER");
    if (!save)
      return;
    SaveManager.SavePlayerData();
    if (GameManager.Instance.IsMultiplayer())
      return;
    AtOManager.Instance.SaveGame();
  }

  public bool IsTreasureClaimed(string treasureId) => this.treasuresClaimed != null && this.treasuresClaimed.Contains(treasureId);

  public void SetMaxAdventureMadnessLevel()
  {
    int madnessDifficulty = AtOManager.Instance.GetMadnessDifficulty();
    if (this.maxAdventureMadnessLevel >= madnessDifficulty)
      return;
    this.maxAdventureMadnessLevel = madnessDifficulty;
  }

  public void IncreaseNg()
  {
    if (SandboxManager.Instance.IsEnabled())
      return;
    this.ngUnlocked = true;
    int ngPlus = AtOManager.Instance.GetNgPlus();
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("current NG " + ngPlus.ToString(), "trace");
    int num = ngPlus + 1;
    if (num > 8)
      num = 8;
    if (num > this.ngLevel)
      this.ngLevel = num;
    if (this.ngLevel < 3)
      this.ngLevel = 3;
    this.SetMaxAdventureMadnessLevel();
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("final NG " + this.ngLevel.ToString(), "general");
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("final MaxMadness " + this.maxAdventureMadnessLevel.ToString(), "general");
    SaveManager.SavePlayerData();
  }

  public void IncreaseObeliskMadness()
  {
    int obeliskMadness = AtOManager.Instance.GetObeliskMadness();
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("current ObeliskLevel " + obeliskMadness.ToString(), "trace");
    int num = obeliskMadness + 1;
    if (num > 10)
      num = 10;
    if (num > this.obeliskMadnessLevel)
      this.obeliskMadnessLevel = num;
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("final Obelisk " + this.obeliskMadnessLevel.ToString(), "trace");
    SaveManager.SavePlayerData();
  }

  public bool IsNgUnlocked()
  {
    if (this.ngUnlocked && this.ngLevel == 0)
      this.ngLevel = 1;
    return this.ngUnlocked;
  }

  public void TeamAddNPC(string id, int position) => this.teamNPCs[position] = id;

  public bool PlayerHaveHero(string subclass) => false;

  public Hero[] GetTeamHero() => this.gameTeamHeroes;

  public void TeamAddHero(string subclass, int arrayPosition)
  {
    Hero hero = GameManager.Instance.CreateHero(subclass);
    if (this.gameTeamHeroes == null)
      this.CreateTeamForGame();
    this.gameTeamHeroes[arrayPosition] = hero;
  }

  private void CreateTeamForGame() => this.gameTeamHeroes = new Hero[4];

  public List<Hero> PlayerHeroes
  {
    get => this.playerHeroes;
    set => this.playerHeroes = value;
  }

  public string[] TeamHeroes
  {
    get => this.teamHeroes;
    set => this.teamHeroes = value;
  }

  public string[] TeamNPCs
  {
    get => this.teamNPCs;
    set => this.teamNPCs = value;
  }

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

  public PlayerDeck PlayerSavedDeck
  {
    get => this.playerSavedDeck;
    set => this.playerSavedDeck = value;
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

  public PlayerPerk PlayerSavedPerk
  {
    get => this.playerSavedPerk;
    set => this.playerSavedPerk = value;
  }
}
