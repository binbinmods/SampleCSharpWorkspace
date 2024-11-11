// Decompiled with JetBrains decompiler
// Type: Character
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class Character
{
  private Trait traitClass = new Trait();
  private Item itemClass = new Item();
  [SerializeField]
  private string owner;
  [SerializeField]
  private string ownerOriginal = "";
  [SerializeField]
  private int perkRank;
  [SerializeField]
  private List<string> perkList;
  [SerializeField]
  private HeroData heroData;
  [SerializeField]
  private float heroGold;
  [SerializeField]
  private float heroDust;
  [SerializeField]
  private int heroIndex;
  [SerializeField]
  private NPCData npcData;
  [SerializeField]
  private int npcIndex;
  [SerializeField]
  private string id;
  [SerializeField]
  private string className;
  [SerializeField]
  private string subclassName;
  [SerializeField]
  private string scriptableObjectName;
  [SerializeField]
  private string sourceName;
  [SerializeField]
  private string gameName;
  [SerializeField]
  private Sprite spriteSpeed;
  [SerializeField]
  private Sprite spritePortrait;
  [SerializeField]
  private int position;
  [SerializeField]
  private int level;
  [SerializeField]
  private int experience;
  [SerializeField]
  private int hp;
  [SerializeField]
  private int hpCurrent;
  [SerializeField]
  private int energy;
  [SerializeField]
  private int energyCurrent;
  [SerializeField]
  private int energyTurn;
  [SerializeField]
  private int block;
  public bool consumeBlock = true;
  [SerializeField]
  private int speed;
  [SerializeField]
  private int drawModifier;
  [SerializeField]
  private List<string> cards;
  [SerializeField]
  private string[] traits;
  [SerializeField]
  private string weapon = "";
  [SerializeField]
  private string armor = "";
  [SerializeField]
  private string jewelry = "";
  [SerializeField]
  private string accesory = "";
  [SerializeField]
  private string corruption = "";
  [SerializeField]
  private string pet = "";
  [SerializeField]
  private string enchantment = "";
  [SerializeField]
  private string enchantment2 = "";
  [SerializeField]
  private string enchantment3 = "";
  [SerializeField]
  private int resistSlashing;
  private bool immuneSlashing;
  [SerializeField]
  private int resistBlunt;
  private bool immuneBlunt;
  [SerializeField]
  private int resistPiercing;
  private bool immunePiercing;
  [SerializeField]
  private int resistFire;
  private bool immuneFire;
  [SerializeField]
  private int resistCold;
  private bool immuneCold;
  [SerializeField]
  private int resistLightning;
  private bool immuneLightning;
  [SerializeField]
  private int resistMind;
  private bool immuneMind;
  [SerializeField]
  private int resistHoly;
  private bool immuneHoly;
  [SerializeField]
  private int resistShadow;
  private bool immuneShadow;
  [SerializeField]
  private bool alive;
  [SerializeField]
  private int totalDeaths;
  [SerializeField]
  private List<Aura> auraList = new List<Aura>();
  [SerializeField]
  private List<string> auracurseImmune = new List<string>();
  [SerializeField]
  private bool isHero;
  [SerializeField]
  private HeroItem heroItem;
  [SerializeField]
  private NPCItem npcItem;
  [SerializeField]
  private int roundMoved;
  [SerializeField]
  private GameObject gO;
  [SerializeField]
  private int craftRemainingUses;
  [SerializeField]
  private int cardsUpgraded;
  [SerializeField]
  private int cardsRemoved;
  [SerializeField]
  private int cardsCrafted;
  [SerializeField]
  private string skinUsed = "";
  [SerializeField]
  private string cardbackUsed = "";
  private Dictionary<Enums.CardType, int> cardsCostModification = new Dictionary<Enums.CardType, int>();
  private Dictionary<string, int> auraCurseModification = new Dictionary<string, int>();
  private List<CardData> cardsPlayed = new List<CardData>();
  private List<CardData> cardsPlayedRound = new List<CardData>();
  private Dictionary<string, int> dictBonus = new Dictionary<string, int>();
  private Dictionary<Enums.DamageType, int> ResistModsWithItems = new Dictionary<Enums.DamageType, int>();
  private CardData cardCasted;
  private int itemSlots = 9;
  private ItemData[] itemDataBySlot = new ItemData[9];
  private CardData[] cardDataBySlot = new CardData[9];
  private int perk_sparkCharges;
  private Dictionary<string, int> cacheGetAuraResistModifiers = new Dictionary<string, int>();
  private Dictionary<string, int> cacheGetAuraStatModifiers = new Dictionary<string, int>();
  private Dictionary<string, int> cachePerkGetAuraCurseBonusDict = new Dictionary<string, int>();
  private Dictionary<string, int> cacheGetTraitAuraCurseModifiers = new Dictionary<string, int>();
  private Dictionary<Enums.DamageType, int> cacheGetTraitDamageFlatModifiers = new Dictionary<Enums.DamageType, int>();
  private Dictionary<Enums.DamageType, float> cacheGetTraitDamagePercentModifiers = new Dictionary<Enums.DamageType, float>();
  private List<int> cacheGetTraitHealFlatBonus = new List<int>();
  private List<float> cacheGetTraitHealPercentBonus = new List<float>();
  private List<int> cacheGetTraitHealReceivedFlatBonus = new List<int>();
  private List<float> cacheGetTraitHealReceivedPercentBonus = new List<float>();
  private Dictionary<string, bool> cacheAuraCurseImmuneByItems = new Dictionary<string, bool>();
  private Dictionary<string, int> cacheGetItemAuraCurseModifiers = new Dictionary<string, int>();
  private Dictionary<Enums.CharacterStat, int> cacheGetItemStatModifiers = new Dictionary<Enums.CharacterStat, int>();
  private Dictionary<Enums.DamageType, int> cacheGetItemResistModifiers = new Dictionary<Enums.DamageType, int>();
  private Dictionary<Enums.DamageType, int> cacheGetItemDamageFlatModifiers = new Dictionary<Enums.DamageType, int>();
  private Dictionary<Enums.DamageType, float> cacheGetItemDamagePercentModifiers = new Dictionary<Enums.DamageType, float>();
  private List<int> cacheGetItemHealFlatBonus = new List<int>();
  private List<float> cacheGetItemHealPercentBonus = new List<float>();
  private List<int> cacheGetItemHealReceivedFlatBonus = new List<int>();
  private List<float> cacheGetItemHealReceivedPercentBonus = new List<float>();
  private bool useCache = true;

  public void ResetDataForNewCombat(bool clear = true)
  {
    if (clear)
    {
      this.auraCurseModification.Clear();
      this.cardsCostModification.Clear();
    }
    if ((UnityEngine.Object) this.heroData != (UnityEngine.Object) null)
      this.auraCurseModification = Perk.GetAuraCurseBonusDict(this.heroData.HeroSubClass.Id);
    this.ResistModsWithItems.Clear();
    this.RemoveAuraCurses();
    this.energyCurrent = this.energy;
    if (this.hpCurrent > this.hp)
      this.hpCurrent = this.hp;
    this.roundMoved = 0;
    this.block = 0;
    this.drawModifier = 0;
    this.consumeBlock = true;
    this.corruption = "";
    this.enchantment = "";
    this.enchantment2 = "";
    this.enchantment3 = "";
    this.ResetDictBonus();
    this.itemDataBySlot = new ItemData[this.itemSlots];
    this.cardDataBySlot = new CardData[this.itemSlots];
    this.ClearCaches();
  }

  public void ClearCaches()
  {
    this.ClearCacheItems();
    this.ClearCacheTraits();
    this.ClearCachePerks();
    this.ClearAuraModifiers();
  }

  private void ClearAuraModifiers()
  {
    this.cacheGetAuraResistModifiers.Clear();
    this.cacheGetAuraStatModifiers.Clear();
  }

  private void ClearCachePerks() => this.cachePerkGetAuraCurseBonusDict.Clear();

  private void ClearCacheItems()
  {
    this.cacheAuraCurseImmuneByItems.Clear();
    this.cacheGetItemAuraCurseModifiers.Clear();
    this.cacheGetItemStatModifiers.Clear();
    this.cacheGetItemResistModifiers.Clear();
    this.cacheGetItemDamageFlatModifiers.Clear();
    this.cacheGetItemDamagePercentModifiers.Clear();
    this.cacheGetItemHealFlatBonus.Clear();
    this.cacheGetItemHealPercentBonus.Clear();
    this.cacheGetItemHealReceivedFlatBonus.Clear();
    this.cacheGetItemHealReceivedPercentBonus.Clear();
  }

  private void ClearCacheTraits()
  {
    this.cacheGetTraitAuraCurseModifiers.Clear();
    this.cacheGetTraitDamageFlatModifiers.Clear();
    this.cacheGetTraitDamagePercentModifiers.Clear();
    this.cacheGetTraitHealFlatBonus.Clear();
    this.cacheGetTraitHealPercentBonus.Clear();
    this.cacheGetTraitHealReceivedFlatBonus.Clear();
    this.cacheGetTraitHealReceivedPercentBonus.Clear();
  }

  public void ModifyCardsCost(Enums.CardType type, int cost)
  {
    if (!this.cardsCostModification.ContainsKey(type))
      this.cardsCostModification[type] = cost;
    else
      this.cardsCostModification[type] += cost;
  }

  public int GetCardsCostModification(List<Enums.CardType> types)
  {
    int costModification = 0;
    for (int index = 0; index < types.Count; ++index)
    {
      if (this.cardsCostModification.ContainsKey(types[index]))
        costModification += this.cardsCostModification[types[index]];
    }
    return costModification;
  }

  public void ModifyAuraCurseCharges(string _id, int _charges)
  {
    this.id = this.id.Trim().ToLower();
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if ((UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && this.auraList[index].ACData.Id == _id)
      {
        this.auraList[index].AuraCharges = _charges;
        break;
      }
    }
  }

  public void ModifyAuraCurseQuantity(string id, int cost)
  {
    id = id.Trim().ToLower();
    if (!this.auraCurseModification.ContainsKey(id))
      this.auraCurseModification[id] = cost;
    else
      this.auraCurseModification[id] += cost;
  }

  public int GetAuraCurseQuantityModification(string id, Enums.CardClass CC)
  {
    id = id.Trim().ToLower();
    if (this.isHero && (CC == Enums.CardClass.Monster || CC == Enums.CardClass.Injury || CC == Enums.CardClass.Boon))
      return 0;
    if (!this.isHero)
    {
      if (id == "doom" || id == "paralyze" || id == "invulnerable" || id == "stress" || id == "fatigue")
        return 0;
      int quantityModification = 0;
      Dictionary<string, int> auraCurseModifiers = this.GetTraitAuraCurseModifiers();
      if (auraCurseModifiers != null && auraCurseModifiers.ContainsKey(id))
        quantityModification += auraCurseModifiers[id];
      if (MadnessManager.Instance.IsMadnessTraitActive("overchargedmonsters") || AtOManager.Instance.IsChallengeTraitActive("overchargedmonsters"))
        ++quantityModification;
      return quantityModification;
    }
    if ((bool) (UnityEngine.Object) TownManager.Instance || (bool) (UnityEngine.Object) MapManager.Instance || (bool) (UnityEngine.Object) MatchManager.Instance || (bool) (UnityEngine.Object) RewardsManager.Instance || (bool) (UnityEngine.Object) LootManager.Instance)
    {
      Dictionary<string, int> auraCurseBonusDict;
      if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
      {
        if (this.cachePerkGetAuraCurseBonusDict.Count == 0)
          this.cachePerkGetAuraCurseBonusDict = Perk.GetAuraCurseBonusDict(this.heroData.HeroSubClass.Id);
        auraCurseBonusDict = this.cachePerkGetAuraCurseBonusDict;
      }
      else
        auraCurseBonusDict = Perk.GetAuraCurseBonusDict(this.heroData.HeroSubClass.Id);
      Dictionary<string, int> auraCurseModifiers1 = this.GetItemAuraCurseModifiers();
      Dictionary<string, int> auraCurseModifiers2 = this.GetTraitAuraCurseModifiers();
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      foreach (KeyValuePair<string, int> keyValuePair in auraCurseModifiers1)
      {
        if (dictionary.ContainsKey(keyValuePair.Key))
          dictionary[keyValuePair.Key] += keyValuePair.Value;
        else
          dictionary.Add(keyValuePair.Key, keyValuePair.Value);
      }
      foreach (KeyValuePair<string, int> keyValuePair in auraCurseModifiers2)
      {
        if (dictionary.ContainsKey(keyValuePair.Key))
          dictionary[keyValuePair.Key] += keyValuePair.Value;
        else
          dictionary.Add(keyValuePair.Key, keyValuePair.Value);
      }
      foreach (KeyValuePair<string, int> keyValuePair in auraCurseBonusDict)
      {
        if (dictionary.ContainsKey(keyValuePair.Key))
          dictionary[keyValuePair.Key] += keyValuePair.Value;
        else
          dictionary.Add(keyValuePair.Key, keyValuePair.Value);
      }
      if (dictionary.ContainsKey(id))
        return dictionary[id];
    }
    return 0;
  }

  public void SetHeroIndex(int index) => this.heroIndex = index;

  public void SetNPCIndex(int index) => this.npcIndex = index;

  public void ResetDictBonus()
  {
  }

  public void SetEvent(
    Enums.EventActivation theEvent,
    Character target = null,
    int auxInt = 0,
    string auxString = "")
  {
    if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null)
    {
      MatchManager.Instance.SetEventCo(this, theEvent, target, auxInt, auxString);
    }
    else
    {
      if (this.isHero && theEvent != Enums.EventActivation.Killed)
        this.ActivateTrait(theEvent, target, auxInt, auxString);
      this.ActivateItem(theEvent, target, auxInt, auxString);
      this.FinishSetEvent(theEvent);
    }
  }

  public void FinishSetEvent(Enums.EventActivation theEvent)
  {
    switch (theEvent)
    {
      case Enums.EventActivation.BeginCombat:
        this.cardsPlayed.Clear();
        break;
      case Enums.EventActivation.BeginTurn:
        this.cardsPlayedRound.Clear();
        break;
      case Enums.EventActivation.CastCard:
        this.cardsPlayed.Add(MatchManager.Instance.CardActive);
        this.cardsPlayedRound.Add(MatchManager.Instance.CardActive);
        break;
    }
  }

  public void ActivateItem(
    Enums.EventActivation theEvent,
    Character target,
    int auxInt,
    string auxString)
  {
    if (theEvent == Enums.EventActivation.DestroyItem)
    {
      CardData cardData = Globals.Instance.GetCardData(auxString);
      if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
        this.itemClass.DoItem(theEvent, cardData, auxString, this, this, auxInt, auxString, 0, (CardData) null);
      this.ClearCacheItems();
    }
    else
    {
      if ((UnityEngine.Object) MatchManager.Instance == (UnityEngine.Object) null)
        return;
      int timesActivated = -1;
      for (int index = 0; index < this.itemSlots; ++index)
      {
        string id = "";
        switch (theEvent)
        {
          case Enums.EventActivation.BeginTurn:
            if (index == 0 && this.corruption != "")
              id = this.corruption;
            if (index == 1 && this.weapon != "")
            {
              id = this.weapon;
              break;
            }
            if (index == 2 && this.armor != "")
            {
              id = this.armor;
              break;
            }
            if (index == 3 && this.jewelry != "")
            {
              id = this.jewelry;
              break;
            }
            if (index == 4 && this.accesory != "")
            {
              id = this.accesory;
              break;
            }
            if (index == 5 && this.pet != "")
            {
              id = this.pet;
              break;
            }
            if (index == 6 && this.enchantment != "")
            {
              id = this.enchantment;
              break;
            }
            if (index == 7 && this.enchantment2 != "")
            {
              id = this.enchantment2;
              break;
            }
            if (index == 8 && this.enchantment3 != "")
            {
              id = this.enchantment3;
              break;
            }
            break;
          case Enums.EventActivation.Killed:
            if (index == 0 && this.enchantment != "")
            {
              id = this.enchantment;
              break;
            }
            if (index == 1 && this.enchantment2 != "")
            {
              id = this.enchantment2;
              break;
            }
            if (index == 2 && this.enchantment3 != "")
            {
              id = this.enchantment3;
              break;
            }
            if (index == 3 && this.weapon != "")
            {
              id = this.weapon;
              break;
            }
            if (index == 4 && this.armor != "")
            {
              id = this.armor;
              break;
            }
            if (index == 5 && this.jewelry != "")
            {
              id = this.jewelry;
              break;
            }
            if (index == 6 && this.accesory != "")
            {
              id = this.accesory;
              break;
            }
            if (index == 7 && this.corruption != "")
            {
              id = this.corruption;
              break;
            }
            if (index == 8 && this.pet != "")
            {
              id = this.pet;
              break;
            }
            break;
          default:
            if (index == 0 && this.weapon != "")
            {
              id = this.weapon;
              break;
            }
            if (index == 1 && this.armor != "")
            {
              id = this.armor;
              break;
            }
            if (index == 2 && this.jewelry != "")
            {
              id = this.jewelry;
              break;
            }
            if (index == 3 && this.accesory != "")
            {
              id = this.accesory;
              break;
            }
            if (index == 4 && this.corruption != "")
            {
              id = this.corruption;
              break;
            }
            if (index == 5 && this.pet != "")
            {
              id = this.pet;
              break;
            }
            if (index == 6 && this.enchantment != "")
            {
              id = this.enchantment;
              break;
            }
            if (index == 7 && this.enchantment2 != "")
            {
              id = this.enchantment2;
              break;
            }
            if (index == 8 && this.enchantment3 != "")
            {
              id = this.enchantment3;
              break;
            }
            break;
        }
        if (id != "")
        {
          CardData cardData = Globals.Instance.GetCardData(id, false);
          if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
          {
            ItemData itemData = (ItemData) null;
            if ((UnityEngine.Object) cardData.Item != (UnityEngine.Object) null)
              itemData = cardData.Item;
            else if ((UnityEngine.Object) cardData.ItemEnchantment != (UnityEngine.Object) null)
              itemData = cardData.ItemEnchantment;
            if ((UnityEngine.Object) itemData != (UnityEngine.Object) null && (!itemData.ActivationOnlyOnHeroes || this.isHero) && (itemData.Activation == theEvent || itemData.Activation == Enums.EventActivation.Damaged && theEvent == Enums.EventActivation.DamagedSecondary))
            {
              if (Globals.Instance.ShowDebug)
                Functions.DebugLogGD("[Character/ActivateItem] Checking if " + id + " will activate", "item");
              if (this.itemClass.DoItem(theEvent, cardData, id, this, target, auxInt, auxString, 0, this.cardCasted, true))
              {
                ++timesActivated;
                if (Globals.Instance.ShowDebug)
                  Functions.DebugLogGD("[Character/ActivateItem] " + id + "-> OK", "item");
                MatchManager.Instance.DoItem(this, theEvent, cardData, id, target, auxInt, auxString, timesActivated);
              }
              else if (Globals.Instance.ShowDebug)
                Functions.DebugLogGD(id + " -> XXXXXX", "item");
            }
          }
        }
      }
    }
  }

  public void AssignEnchantmentManual(string _id, int _slot)
  {
    switch (_slot)
    {
      case 0:
        this.enchantment = _id;
        break;
      case 1:
        this.enchantment2 = _id;
        break;
      case 2:
        this.enchantment3 = _id;
        break;
    }
    this.ClearCacheItems();
  }

  public void AssignEnchantment(string _id)
  {
    MatchManager.Instance.CleanEnchantmentExecutedTimes(this.id, _id);
    if (this.enchantment == _id)
      this.enchantment = "";
    else if (this.enchantment2 == _id)
      this.enchantment2 = "";
    else if (this.enchantment3 == _id)
      this.enchantment3 = "";
    this.ReorganizeEnchantments();
    if (this.enchantment == "")
      this.enchantment = _id;
    else if (this.enchantment2 == "")
      this.enchantment2 = _id;
    else if (this.enchantment3 == "")
    {
      this.enchantment3 = _id;
    }
    else
    {
      this.enchantment = "";
      this.ReorganizeEnchantments();
      this.enchantment3 = _id;
    }
    this.ClearCacheItems();
    this.UpdateAuraCurseFunctions();
  }

  public void ReorganizeEnchantments()
  {
    if (this.isHero && (UnityEngine.Object) this.heroItem == (UnityEngine.Object) null || !this.isHero && (UnityEngine.Object) this.npcItem == (UnityEngine.Object) null)
      return;
    if (this.enchantment == "")
    {
      if (this.enchantment2 != "")
      {
        this.enchantment = this.enchantment2;
        this.enchantment2 = "";
      }
      else if (this.enchantment3 != "")
      {
        this.enchantment = this.enchantment3;
        this.enchantment3 = "";
      }
    }
    if (this.enchantment2 == "" && this.enchantment3 != "")
    {
      this.enchantment2 = this.enchantment3;
      this.enchantment3 = "";
    }
    if (this.isHero)
      this.heroItem.ShowEnchantments();
    else
      this.npcItem.ShowEnchantments();
    this.ClearCacheItems();
  }

  public void ShowPetsFromEnchantments()
  {
    if (this.enchantment == "" && this.enchantment2 == "" && this.enchantment3 == "")
      return;
    Hero _hero = (Hero) null;
    NPC _npc = (NPC) null;
    CharacterItem characterItem;
    if (this.isHero)
    {
      characterItem = (CharacterItem) this.heroItem;
      _hero = this.heroItem.Hero;
    }
    else
    {
      characterItem = (CharacterItem) this.npcItem;
      _npc = this.npcItem.NPC;
    }
    bool flag = false;
    if (this.enchantment != "")
    {
      CardData cardData = Globals.Instance.GetCardData(this.enchantment);
      if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && (UnityEngine.Object) cardData.PetModel != (UnityEngine.Object) null)
      {
        MatchManager.Instance.CreatePet(cardData, characterItem.gameObject, _hero, _npc, true, 0);
        flag = true;
      }
    }
    if (this.enchantment2 != "")
    {
      CardData cardData = Globals.Instance.GetCardData(this.enchantment2);
      if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && (UnityEngine.Object) cardData.PetModel != (UnityEngine.Object) null)
      {
        MatchManager.Instance.CreatePet(cardData, characterItem.gameObject, _hero, _npc, true, 1);
        flag = true;
      }
    }
    if (this.enchantment3 != "")
    {
      CardData cardData = Globals.Instance.GetCardData(this.enchantment3);
      if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && (UnityEngine.Object) cardData.PetModel != (UnityEngine.Object) null)
      {
        MatchManager.Instance.CreatePet(cardData, characterItem.gameObject, _hero, _npc, true, 2);
        flag = true;
      }
    }
    if (flag)
      return;
    MatchManager.Instance.RemovePetEnchantment(characterItem.gameObject);
  }

  public void EnchantmentExecute(string enchantmentName)
  {
    if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
    {
      if (this.enchantment == enchantmentName)
        this.heroItem.EnchantmentExecute(0);
      else if (this.enchantment2 == enchantmentName)
        this.heroItem.EnchantmentExecute(1);
      else if (this.enchantment3 == enchantmentName)
        this.heroItem.EnchantmentExecute(2);
    }
    else if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
    {
      if (this.enchantment == enchantmentName)
        this.npcItem.EnchantmentExecute(0);
      else if (this.enchantment2 == enchantmentName)
        this.npcItem.EnchantmentExecute(1);
      else if (this.enchantment3 == enchantmentName)
        this.npcItem.EnchantmentExecute(2);
    }
    this.ClearCacheItems();
  }

  public void SetCastedCard(CardData _castedCard)
  {
    if ((UnityEngine.Object) _castedCard == (UnityEngine.Object) null || _castedCard.AutoplayDraw || _castedCard.AutoplayEndTurn)
      return;
    this.cardCasted = _castedCard;
  }

  public void DoItem(
    Enums.EventActivation theEvent,
    CardData cardData,
    string item,
    Character target,
    int auxInt,
    string auxString,
    int order,
    CardData _castedCard)
  {
    string str = "item:" + item;
    if ((UnityEngine.Object) _castedCard != (UnityEngine.Object) null)
      str += _castedCard.InternalId;
    string _key = str + MatchManager.Instance.LogDictionary.Count.ToString();
    if ((bool) (UnityEngine.Object) MatchManager.Instance)
    {
      switch (theEvent)
      {
        case Enums.EventActivation.PreBeginCombat:
        case Enums.EventActivation.AuraCurseSet:
          break;
        case Enums.EventActivation.CorruptionCombatStart:
          MatchManager.Instance.CreateLogEntry(true, _key, item, (Hero) null, (NPC) null, (Hero) null, (NPC) null, MatchManager.Instance.GameRound(), Enums.EventActivation.CorruptionBeginRound);
          break;
        default:
          MatchManager.Instance.CreateLogEntry(true, _key, item, (Hero) null, (NPC) null, (Hero) null, (NPC) null, MatchManager.Instance.GameRound(), Enums.EventActivation.ItemActivation);
          break;
      }
    }
    this.itemClass.DoItem(theEvent, cardData, item, this, target, auxInt, auxString, order, _castedCard);
    if (!(bool) (UnityEngine.Object) MatchManager.Instance || theEvent == Enums.EventActivation.PreBeginCombat || theEvent == Enums.EventActivation.AuraCurseSet)
      return;
    if (theEvent == Enums.EventActivation.CorruptionCombatStart)
      MatchManager.Instance.CreateLogEntry(false, _key, item, (Hero) null, (NPC) null, (Hero) null, (NPC) null, MatchManager.Instance.GameRound(), Enums.EventActivation.CorruptionBeginRound);
    else
      MatchManager.Instance.CreateLogEntry(false, _key, item, (Hero) null, (NPC) null, (Hero) null, (NPC) null, MatchManager.Instance.GameRound(), Enums.EventActivation.ItemActivation);
  }

  public bool HaveTraitToActivate(Enums.EventActivation theEvent)
  {
    for (int index = 0; index < this.traits.Length; ++index)
    {
      if (this.traits[index] != null && this.traits[index] != "")
      {
        TraitData traitData = Globals.Instance.GetTraitData(this.traits[index]);
        if ((UnityEngine.Object) traitData != (UnityEngine.Object) null && traitData.Activation == theEvent)
          return true;
      }
    }
    return false;
  }

  public void ActivateTrait(
    Enums.EventActivation theEvent,
    Character target,
    int auxInt,
    string auxString)
  {
    for (int index = 0; index < this.traits.Length; ++index)
    {
      if (this.traits[index] != null && this.traits[index] != "")
      {
        TraitData traitData = Globals.Instance.GetTraitData(this.traits[index]);
        if ((UnityEngine.Object) traitData != (UnityEngine.Object) null && traitData.Activation == theEvent)
          this.DoTrait(theEvent, this.traits[index], target, auxInt, auxString);
      }
    }
  }

  private void DoTrait(
    Enums.EventActivation theEvent,
    string trait,
    Character target,
    int auxInt,
    string auxString)
  {
    string _key = "trait:" + trait;
    if ((bool) (UnityEngine.Object) MatchManager.Instance)
      _key = _key + "_" + MatchManager.Instance.LogDictionary.Count.ToString();
    if (theEvent != Enums.EventActivation.AuraCurseSet && (bool) (UnityEngine.Object) MatchManager.Instance)
      MatchManager.Instance.CreateLogEntry(true, _key, trait, (Hero) null, (NPC) null, (Hero) null, (NPC) null, MatchManager.Instance.GameRound(), Enums.EventActivation.TraitActivation);
    this.traitClass.DoTrait(theEvent, trait, this, target, auxInt, auxString, this.cardCasted);
    if (theEvent == Enums.EventActivation.AuraCurseSet || !(bool) (UnityEngine.Object) MatchManager.Instance)
      return;
    MatchManager.Instance.CreateLogEntry(false, _key, trait, (Hero) null, (NPC) null, (Hero) null, (NPC) null, MatchManager.Instance.GameRound(), Enums.EventActivation.TraitActivation);
  }

  public void DoTraitFunction(string funcName) => this.traitClass.DoTraitFunction(funcName);

  public bool HaveTrait(string traitId)
  {
    if (this.traits != null)
    {
      for (int index = 0; index < this.traits.Length; ++index)
      {
        string trait = this.traits[index];
        if (this.traits[index] != null && this.traits[index] == traitId.ToLower())
          return true;
      }
    }
    return false;
  }

  public int GetHp() => this.hpCurrent;

  public int GetHpLeftForMax() => this.GetMaxHP() - this.GetHp();

  public float GetHpPercent() => (float) ((double) this.GetHp() / (double) this.GetMaxHP() * 100.0);

  public void HealToMax() => this.hpCurrent = this.GetMaxHP();

  public void ModifyHp(int _hp, bool _includeInStats = true, bool _refreshHP = true)
  {
    int heroActive = MatchManager.Instance.GetHeroActive();
    int maxHp = this.GetMaxHP();
    if (AtOManager.Instance.combatStats == null)
      AtOManager.Instance.InitCombatStats();
    if (_hp < 0)
    {
      int num = Mathf.Abs(_hp);
      if (num > this.hpCurrent)
        num = this.hpCurrent;
      if (_includeInStats && heroActive > -1)
      {
        AtOManager.Instance.combatStats[heroActive, 0] += num;
        AtOManager.Instance.combatStatsCurrent[heroActive, 0] += num;
      }
      if (this.isHero)
      {
        AtOManager.Instance.combatStats[this.heroIndex, 1] += num;
        AtOManager.Instance.combatStatsCurrent[this.heroIndex, 1] += num;
      }
    }
    else if (_hp > 0)
    {
      int num = _hp;
      if (num > maxHp - this.hpCurrent)
        num = maxHp - this.hpCurrent;
      if (_includeInStats && heroActive > -1)
      {
        AtOManager.Instance.combatStats[heroActive, 3] += num;
        AtOManager.Instance.combatStatsCurrent[heroActive, 3] += num;
      }
      if (this.isHero)
      {
        AtOManager.Instance.combatStats[this.heroIndex, 4] += num;
        AtOManager.Instance.combatStatsCurrent[this.heroIndex, 4] += num;
      }
    }
    this.hpCurrent += _hp;
    if (this.hpCurrent > maxHp)
      this.hpCurrent = maxHp;
    if (this.hpCurrent <= 0)
    {
      this.alive = false;
      this.hpCurrent = 0;
    }
    else
      this.alive = true;
    if (_hp < 0)
    {
      if (_hp < -15 && (UnityEngine.Object) MatchManager.Instance.CardActive != (UnityEngine.Object) null && MatchManager.Instance.CardActive.TargetType != Enums.CardTargetType.Global)
        GameManager.Instance.cameraManager.Shake();
      if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
        this.npcItem.CharacterHitted(true);
      else if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
        this.heroItem.CharacterHitted(true);
    }
    if (!_refreshHP)
      return;
    this.SetHP();
  }

  public bool HaveItem(string _itemId, int _itemSlot = -1, bool _checkRareToo = false)
  {
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (_itemSlot <= -1 || slot == _itemSlot)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null && itemDataBySlot.Id == _itemId)
          return true;
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null & _checkRareToo)
        {
          CardData cardData = Globals.Instance.GetCardData(itemDataBySlot.Id, false);
          if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && cardData.CardUpgraded == Enums.CardUpgraded.Rare && cardData.UpgradedFrom.ToLower() == _itemId)
            return true;
        }
      }
    }
    return false;
  }

  public string GetItemToPassEventRoll()
  {
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
      if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null && itemDataBySlot.PassSingleAndCharacterRolls)
        return itemDataBySlot.Id;
    }
    return "";
  }

  public bool HaveItemToActivate(
    Enums.EventActivation _theEvent,
    bool _checkForItems = false,
    bool _checkForCorruption = false)
  {
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (_checkForCorruption || !((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null && itemDataBySlot.Activation == _theEvent)
          return _theEvent != Enums.EventActivation.BeginTurnAboutToDealCards && _theEvent != Enums.EventActivation.BeginTurn || (itemDataBySlot.ExactRound <= 0 || MatchManager.Instance.GetCurrentRound() == itemDataBySlot.ExactRound) && (itemDataBySlot.RoundCycle <= 0 || MatchManager.Instance.GetCurrentRound() % itemDataBySlot.RoundCycle == 0) && (!_checkForItems || itemDataBySlot.CardNum != 0 || !((UnityEngine.Object) itemDataBySlot.CardToGain == (UnityEngine.Object) null));
      }
    }
    return false;
  }

  public bool HaveCastCardItem()
  {
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null && itemDataBySlot.CardPlace == Enums.CardPlace.Cast)
          return true;
      }
    }
    return false;
  }

  public int HaveAboutToDealCardsItemNum()
  {
    int dealCardsItemNum = 0;
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null && itemDataBySlot.Activation == Enums.EventActivation.BeginTurnAboutToDealCards)
          ++dealCardsItemNum;
      }
    }
    return dealCardsItemNum;
  }

  public int HaveCastCardItemNum()
  {
    int num = 0;
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null && itemDataBySlot.CardPlace == Enums.CardPlace.Cast)
          ++num;
      }
    }
    return num;
  }

  public bool HaveResurrectItem()
  {
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot, false);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null && itemDataBySlot.HealPercentQuantity > 0 && itemDataBySlot.Activation == Enums.EventActivation.Killed)
          return true;
      }
    }
    return false;
  }

  public void Resurrect(float percent)
  {
    this.alive = true;
    this.auraList = new List<Aura>();
    this.hpCurrent = Functions.FuncRoundToInt((float) ((double) this.GetMaxHP() * (double) percent * 0.0099999997764825821));
    this.UpdateAuraCurseFunctions();
  }

  public void KillCharacterFromOutside()
  {
    this.alive = false;
    this.hp = this.hpCurrent = 0;
    if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
    {
      this.npcItem.KillCharacterFromOutside();
    }
    else
    {
      if (!((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null))
        return;
      this.heroItem.KillCharacterFromOutside();
    }
  }

  public void SetHP()
  {
    if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
    {
      this.npcItem.SetHP();
    }
    else
    {
      if (!((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null))
        return;
      this.heroItem.SetHP();
    }
  }

  public int GetMaxHP()
  {
    int maxHp = this.hp + this.GetAuraStatModifiers(0, Enums.CharacterStat.Hp) + this.GetItemStatModifiers(Enums.CharacterStat.Hp);
    if (maxHp <= 0)
      maxHp = 1;
    if (this.hpCurrent > maxHp)
      this.hpCurrent = maxHp;
    return maxHp;
  }

  public void ModifyMaxHP(int quantity)
  {
    this.hpCurrent += quantity;
    this.hp += quantity;
    if (this.hpCurrent <= 0)
      this.hpCurrent = 1;
    if (this.hp <= 0)
      this.hp = 1;
    if (this.hpCurrent > this.hp)
      this.hpCurrent = this.hp;
    AtOManager.Instance.SideBarRefresh();
  }

  public void DecreaseMaxHP(int quantity)
  {
    this.hp -= quantity;
    if (this.hp <= 0)
      this.hp = 1;
    if (this.hpCurrent > this.hp)
      this.hpCurrent = this.hp;
    AtOManager.Instance.SideBarRefresh();
  }

  public int CalculateRewardForCharacter(int _experience)
  {
    if (AtOManager.Instance.IsChallengeTraitActive("smartheroes"))
      _experience += Functions.FuncRoundToInt((float) _experience * 0.5f);
    if (GameManager.Instance.IsObeliskChallenge())
    {
      int level = 5;
      if (this.experience + _experience > Globals.Instance.GetExperienceByLevel(level))
        _experience = Globals.Instance.GetExperienceByLevel(level) - this.experience;
    }
    else
    {
      float num1 = 0.1f;
      if (this.level > AtOManager.Instance.GetTownTier() + 1)
        return Functions.FuncRoundToInt((float) _experience * num1);
      if (this.experience >= Globals.Instance.GetExperienceByLevel(this.level))
        return Functions.FuncRoundToInt((float) _experience * num1);
      if (this.experience + _experience > Globals.Instance.GetExperienceByLevel(this.level))
      {
        int num2 = _experience - (Globals.Instance.GetExperienceByLevel(this.level) - this.experience);
        return Globals.Instance.GetExperienceByLevel(this.level) - this.experience + Functions.FuncRoundToInt((float) num2 * num1);
      }
    }
    return _experience;
  }

  public void GrantExperience(int _experience)
  {
    this.experience += _experience;
    PlayerManager.Instance.ExpGainedSum(_experience);
    AtOManager.Instance.SideBarRefresh();
  }

  public bool IsReadyForLevelUp() => this.experience >= Globals.Instance.GetExperienceByLevel(this.level);

  public void LevelUp()
  {
    this.hpCurrent += this.heroData.HeroSubClass.MaxHp[this.level];
    this.hp += this.heroData.HeroSubClass.MaxHp[this.level];
    ++this.level;
  }

  public int ModifyBlock(int _dmg)
  {
    int block = this.GetBlock();
    int charges = 0;
    if (AtOManager.Instance.combatStats == null)
      AtOManager.Instance.InitCombatStats();
    if (block > 0)
    {
      if (_dmg > block)
      {
        charges = block;
        _dmg -= block;
        this.CheckBlockHP();
      }
      else
      {
        charges = _dmg;
        _dmg = 0;
      }
      this.ConsumeEffectCharges("block", charges);
    }
    if (charges > 0 && this.isHero)
    {
      AtOManager.Instance.combatStats[this.heroIndex, 2] += charges;
      AtOManager.Instance.combatStatsCurrent[this.heroIndex, 2] += charges;
    }
    return _dmg;
  }

  private void RemoveBlock()
  {
    string text = "-" + Functions.UppercaseFirst(Texts.Instance.GetText("block"));
    if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
    {
      this.heroItem.ScrollCombatText(text, Enums.CombatScrollEffectType.Block);
    }
    else
    {
      if (!((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null))
        return;
      this.npcItem.ScrollCombatText(text, Enums.CombatScrollEffectType.Block);
    }
  }

  public void SetBlock()
  {
    int block = this.GetBlock();
    if (block > 0 && block > this.block)
      GameManager.Instance.PlayAudio(Globals.Instance.GetAuraCurseData("block").Sound, 0.5f);
    this.block = block;
    this.CheckBlockHP();
  }

  public void CheckBlockHP()
  {
    bool state = this.GetBlock() > 0;
    if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
    {
      this.heroItem.DrawBlock(state);
    }
    else
    {
      if (!((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null))
        return;
      this.npcItem.DrawBlock(state);
    }
  }

  public int GetBlock() => this.GetAuraCharges("block");

  public int GetEnergy() => this.EnergyCurrent;

  public int GetEnergyTurn()
  {
    int energyTurn = this.GetAuraStatModifiers(this.EnergyTurn, Enums.CharacterStat.Energy) + this.GetItemStatModifiers(Enums.CharacterStat.EnergyTurn);
    if (energyTurn < 0)
      energyTurn = 0;
    return energyTurn;
  }

  public void GainEnergyTurn()
  {
    this.ModifyEnergy(this.GetEnergyTurn(), true);
    if (MatchManager.Instance.GetCurrentRound() == 2 && AtOManager.Instance.CharacterHavePerk(this.SubclassName, "mainperkenergy2b"))
    {
      this.ModifyEnergy(1, true);
    }
    else
    {
      if (MatchManager.Instance.GetCurrentRound() != 4 || !AtOManager.Instance.CharacterHavePerk(this.SubclassName, "mainperkenergy2c"))
        return;
      this.ModifyEnergy(2, true);
    }
  }

  public void ModifyEnergy(int _energy, bool showScrollCombatText = false)
  {
    if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null || (UnityEngine.Object) this.heroItem == (UnityEngine.Object) null)
      return;
    int energyCurrent = this.EnergyCurrent;
    this.EnergyCurrent += _energy;
    if (this.EnergyCurrent > 10)
      this.EnergyCurrent = 10;
    else if (this.EnergyCurrent < 0)
      this.EnergyCurrent = 0;
    if (this.EnergyCurrent > energyCurrent)
    {
      GameManager.Instance.PlayAudio(Globals.Instance.GetAuraCurseData("energize").Sound, 0.5f);
      MatchManager.Instance.ShowEnergyCounterParticles();
    }
    StringBuilder stringBuilder = new StringBuilder();
    if (showScrollCombatText)
    {
      if (_energy > 0)
      {
        stringBuilder.Append("+");
        stringBuilder.Append(_energy);
      }
      else
        stringBuilder.Append(_energy);
    }
    if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
    {
      if (showScrollCombatText)
      {
        stringBuilder.Append(" ");
        stringBuilder.Append(Functions.UppercaseFirst(Texts.Instance.GetText("energy")));
        this.heroItem.ScrollCombatText(stringBuilder.ToString(), Enums.CombatScrollEffectType.Energy);
      }
      this.heroItem.DrawEnergy();
    }
    MatchManager.Instance.SetEnergyCounter(this.EnergyCurrent, _energy);
    MatchManager.Instance.RedrawCardsBorder();
  }

  public int GetDrawCardsTurnForDisplayInDeck()
  {
    int forDisplayInDeck = 5 + this.GetAuraDrawModifiers(false);
    if (forDisplayInDeck > 10)
      forDisplayInDeck = 10;
    else if (forDisplayInDeck < 0)
      forDisplayInDeck = 0;
    return forDisplayInDeck;
  }

  public int GetDrawCardsTurn()
  {
    int drawCardsTurn = 5 + this.drawModifier;
    if (drawCardsTurn > 10)
      drawCardsTurn = 10;
    else if (drawCardsTurn < 0)
      drawCardsTurn = 0;
    return drawCardsTurn;
  }

  public List<string> GetAuraCurseByOrder(int type, int max = 1, bool onlyDispeleable = false, bool giveAll = false)
  {
    List<string> auraCurseByOrder = new List<string>();
    int num = 0;
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if ((type == 0 || type == 2) && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && this.auraList[index].ACData.IsAura && this.auraList[index].ACData.Removable == onlyDispeleable | giveAll && !auraCurseByOrder.Contains(this.auraList[index].ACData.Id))
      {
        auraCurseByOrder.Add(this.auraList[index].ACData.Id);
        ++num;
        if (num >= max)
          break;
      }
      if ((type == 1 || type == 2) && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && !this.auraList[index].ACData.IsAura && this.auraList[index].ACData.Removable == onlyDispeleable | giveAll && !auraCurseByOrder.Contains(this.auraList[index].ACData.Id))
      {
        auraCurseByOrder.Add(this.auraList[index].ACData.Id);
        ++num;
        if (num >= max)
          break;
      }
    }
    return auraCurseByOrder;
  }

  public virtual void BeginRound()
  {
    MatchManager.Instance.ConsumeAuraCurse(nameof (BeginRound), this);
    if (MatchManager.Instance.GetCurrentRound() != 1)
      return;
    if (AtOManager.Instance.CharacterHavePerk(this.SubclassName, "mainperkreinforce0"))
      this.SetAuraTrait(this, "reinforce", 1);
    if (AtOManager.Instance.CharacterHavePerk(this.SubclassName, "mainperkinsulate0"))
      this.SetAuraTrait(this, "insulate", 1);
    if (!AtOManager.Instance.CharacterHavePerk(this.SubclassName, "mainperkcourage0"))
      return;
    this.SetAuraTrait(this, "courage", 1);
  }

  public virtual void EndRound()
  {
    int auraCharges = this.GetAuraCharges("fortify");
    MatchManager.Instance.ConsumeAuraCurse(nameof (EndRound), this);
    if (!this.isHero || !AtOManager.Instance.TeamHavePerk("mainperkfortify1c") || auraCharges <= 0)
      return;
    this.SetAuraTrait(this, "block", auraCharges * 10);
  }

  public virtual void BeginTurn()
  {
    this.RemoveEnchantsStartTurn();
    this.ReorganizeEnchantments();
    this.GainEnergyTurn();
    this.GetAuraDrawModifiers();
    this.perk_sparkCharges = this.GetAuraCharges("spark");
    MatchManager.Instance.ConsumeAuraCurse(nameof (BeginTurn), this);
  }

  public virtual void BeginTurnPerks()
  {
    if (!this.isHero && this.perk_sparkCharges > 0 && AtOManager.Instance.TeamHavePerk("mainperkspark2b"))
    {
      List<NPC> npcSides = MatchManager.Instance.GetNPCSides(this.position);
      for (int index = 0; index < npcSides.Count; ++index)
        npcSides[index].SetAura((Character) null, Globals.Instance.GetAuraCurseData("spark"), Functions.FuncRoundToInt((float) this.perk_sparkCharges * 0.3f));
    }
    if (MatchManager.Instance.GetCurrentRound() == 1)
    {
      if (AtOManager.Instance.CharacterHavePerk(this.SubclassName, "mainperkfortify1b"))
        this.SetAuraTrait(this, "fortify", 1);
      if (AtOManager.Instance.CharacterHavePerk(this.SubclassName, "mainperkinspire0a"))
        this.SetAuraTrait(this, "inspire", 1);
    }
    MatchManager.Instance.SetWaitExecution(false);
  }

  public virtual void EndTurn()
  {
    if (this.isHero && (UnityEngine.Object) this.heroItem == (UnityEngine.Object) null || !this.isHero && (UnityEngine.Object) this.npcItem == (UnityEngine.Object) null)
      return;
    int auraCharges1 = this.GetAuraCharges("chill");
    int auraCharges2 = this.GetAuraCharges("spark");
    int auraCharges3 = this.GetAuraCharges("crack");
    this.RemoveEnchantsEndTurn();
    this.ReorganizeEnchantments();
    if (AtOManager.Instance.CharacterHavePerk(this.SubclassName, "mainperkchill2d"))
      this.SetAuraTrait(this, "reinforce", Mathf.FloorToInt(0.0714285746f * (float) auraCharges1));
    if (!this.isHero)
    {
      if (AtOManager.Instance.TeamHavePerk("mainperkspark2c"))
        this.SetAuraTrait((Character) null, "slow", Mathf.FloorToInt(0.0714285746f * (float) auraCharges2));
      if (AtOManager.Instance.TeamHavePerk("mainperkcrack2c"))
        this.SetAuraTrait((Character) null, "vulnerable", Mathf.FloorToInt(0.0714285746f * (float) auraCharges3));
    }
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("[CHARACTER ENDTURN] Alive -> " + this.alive.ToString());
    MatchManager.Instance.ConsumeAuraCurse(nameof (EndTurn), this);
  }

  public int[] GetSpeed()
  {
    int source = this.Speed + this.GetItemStatModifiers(Enums.CharacterStat.Speed);
    int auraStatModifiers = this.GetAuraStatModifiers(source, Enums.CharacterStat.Speed);
    return new int[3]
    {
      auraStatModifiers,
      source,
      auraStatModifiers - source
    };
  }

  public void IndirectDamage(
    Enums.DamageType damageType,
    int damage,
    AudioClip sound = null,
    string effect = "",
    string sourceCharacterName = "",
    string sourceCharacterId = "")
  {
    CastResolutionForCombatText _cast = new CastResolutionForCombatText();
    if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
      new Dictionary<HeroItem, CastResolutionForCombatText>()[this.heroItem] = _cast;
    else if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
      new Dictionary<NPCItem, CastResolutionForCombatText>()[this.npcItem] = _cast;
    if (effect != "")
      _cast.effect = effect;
    bool flag = false;
    float num1 = 0.0f;
    int num2 = 0;
    if (this.IsInvulnerable())
      _cast.invulnerable = true;
    else if (damage > 0)
    {
      if (damageType != Enums.DamageType.None)
      {
        int num3 = damage;
        damage = this.ModifyBlock(damage);
        int num4 = num3 - damage;
        _cast.blocked = num4;
        if (damage == 0)
          _cast.fullblocked = true;
        if (this.IsImmune(damageType))
        {
          flag = true;
          num2 = 0;
          _cast.immune = true;
        }
      }
      if (!flag)
      {
        if (damageType != Enums.DamageType.None)
          num1 = (float) (-1 * this.BonusResists(damageType));
        num2 = Functions.FuncRoundToInt((float) damage + (float) ((double) damage * (double) num1 * 0.0099999997764825821));
      }
    }
    if (num2 < 0)
      num2 = 0;
    else if ((UnityEngine.Object) sound != (UnityEngine.Object) null)
      GameManager.Instance.PlayAudio(sound, 0.5f);
    int _damage = Mathf.Abs(num2);
    if (_damage > this.hpCurrent)
      _damage = this.hpCurrent;
    if (effect != "")
      this.ModifyHp(-num2, false);
    else
      this.ModifyHp(-num2);
    if (_damage > 0)
    {
      if (effect.ToLower() == "spark")
        MatchManager.Instance.DamageStatusFromCombatStats(sourceCharacterId, effect, _damage);
      else if (effect.ToLower() == "thorns")
        MatchManager.Instance.DamageStatusFromCombatStats(sourceCharacterId, effect, _damage);
      else if (effect.ToLower() == "dark" && sourceCharacterId != "")
        MatchManager.Instance.DamageStatusFromCombatStats(sourceCharacterId, effect, _damage);
      else
        MatchManager.Instance.DamageStatusFromCombatStats(this.id, effect, _damage);
    }
    _cast.damage += num2;
    _cast.damageType = damageType;
    if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
    {
      this.heroItem.ScrollCombatTextDamageNew(_cast);
    }
    else
    {
      if (!((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null))
        return;
      this.npcItem.ScrollCombatTextDamageNew(_cast);
    }
  }

  public void IndirectHeal(int heal, AudioClip sound = null, string effect = "", string sourceCharacterName = "")
  {
    CastResolutionForCombatText _cast = new CastResolutionForCombatText();
    if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
      new Dictionary<HeroItem, CastResolutionForCombatText>()[this.heroItem] = _cast;
    else if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
      new Dictionary<NPCItem, CastResolutionForCombatText>()[this.npcItem] = _cast;
    if (effect != "")
      _cast.effect = effect;
    heal = this.HealReceivedFinal(heal);
    if (this.GetHpLeftForMax() < heal)
      heal = this.GetHpLeftForMax();
    this.ModifyHp(heal, false);
    if (effect.ToLower() == "regeneration")
      MatchManager.Instance.DamageStatusFromCombatStats(this.id, effect, heal);
    _cast.heal = heal;
    if ((UnityEngine.Object) sound != (UnityEngine.Object) null)
      GameManager.Instance.PlayAudio(sound, 0.5f);
    if (sourceCharacterName == "")
      sourceCharacterName = this.gameName;
    string className = this.className;
    int num = effect != "" ? 1 : 0;
    if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
    {
      this.heroItem.ScrollCombatTextDamageNew(_cast);
    }
    else
    {
      if (!((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null))
        return;
      this.npcItem.ScrollCombatTextDamageNew(_cast);
    }
  }

  public void UpdateAuraCurseFunctions(
    AuraCurseData auraIncluded = null,
    int auraIncludedCharges = 0,
    int previousCharges = -1)
  {
    if (!this.Alive)
    {
      if (!Globals.Instance.ShowDebug)
        return;
      Functions.DebugLogGD("[UPDATEAURACURSEFUNCTIONS] Exit because !Alive");
    }
    else if (this.HpCurrent <= 0)
    {
      if (!Globals.Instance.ShowDebug)
        return;
      Functions.DebugLogGD("[UPDATEAURACURSEFUNCTIONS] Exit because HpCurrent <= 0");
    }
    else
    {
      this.ResetDictBonus();
      if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
      {
        this.heroItem.DrawEnergy();
        this.SetHP();
        this.SetBlock();
        this.SetTaunt();
        this.SetStealth();
        this.SetParalyze();
        this.SetOverDebuff();
        this.heroItem.SetDoomIcon();
        this.heroItem.DrawBuffs(auraIncluded, auraIncludedCharges, previousCharges);
      }
      else
      {
        if (!((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null))
          return;
        this.npcItem.DrawEnergy();
        this.npcItem.NPC.RedrawRevealedCards();
        this.SetHP();
        this.SetBlock();
        this.SetTaunt();
        this.SetStealth();
        this.SetParalyze();
        this.npcItem.DrawBuffs(auraIncluded, auraIncludedCharges, previousCharges);
      }
    }
  }

  public void SetAuraTrait(Character theCaster, string auracurse, int charges)
  {
    if (charges <= 0)
      return;
    this.SetAura(theCaster, Globals.Instance.GetAuraCurseData(auracurse), charges, true);
  }

  public string GetAuraListString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    int count = this.AuraList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (this.AuraList[index] != null && (UnityEngine.Object) this.AuraList[index].ACData != (UnityEngine.Object) null)
      {
        stringBuilder.Append(this.AuraList[index].ACData.Id);
        stringBuilder.Append(this.AuraList[index].AuraCharges);
      }
    }
    return stringBuilder.ToString();
  }

  public int SetAura(
    Character theCaster,
    AuraCurseData _acData,
    int charges,
    bool fromTrait = false,
    Enums.CardClass CC = Enums.CardClass.None,
    bool useCharacterMods = true,
    bool canBePreventable = true)
  {
    if (charges == 0 || (UnityEngine.Object) _acData == (UnityEngine.Object) null || !this.Alive || this.HpCurrent <= 0 || _acData.Id.ToLower() == "sight" && this.isHero)
      return 0;
    if (_acData.Id.ToLower() == "stanzai")
    {
      if (this.GetAuraCharges("stanzai") > 0 || this.GetAuraCharges("stanzaii") > 0 || this.GetAuraCharges("stanzaiii") > 0)
        return 0;
    }
    else if (_acData.Id.ToLower() == "stanzaii")
    {
      if (this.GetAuraCharges("stanzaii") > 0 || this.GetAuraCharges("stanzaiii") > 0)
        return 0;
    }
    else if (_acData.Id.ToLower() == "stanzaiii" && (this.GetAuraCharges("stanzaiii") > 0 || AtOManager.Instance.CharacterHavePerk(this.subclassName, "mainperkstanza0c")))
      return 0;
    if (_acData.Id.ToLower() == "block")
    {
      if (this.subclassName == "warden" && AtOManager.Instance.CharacterHaveTrait(this.subclassName, "queenofthorns"))
      {
        _acData = Globals.Instance.GetAuraCurseData("thorns");
        charges = Functions.FuncRoundToInt((float) charges * 0.3f);
      }
      if (!this.isHero && AtOManager.Instance.TeamHavePerk("mainperkcrack2b") && this.GetAuraCharges("crack") > 0)
        charges -= Functions.FuncRoundToInt((float) this.GetAuraCharges("crack") * 0.5f);
    }
    if (_acData.Id.ToLower() == "vitality" && this.isHero && AtOManager.Instance.CharacterHavePerk(this.subclassName, "mainperkvitality1b"))
      this.HealCursesName(singleCurse: "bleed");
    if (_acData.Id.ToLower() == "sight" && !this.isHero && AtOManager.Instance.TeamHavePerk("mainperksight1b"))
      this.HealCursesName(singleCurse: "stealth");
    AuraCurseData auraCurseData = AtOManager.Instance.GlobalAuraCurseModificationByTraitsAndItems("set", _acData.Id, theCaster, this);
    if ((UnityEngine.Object) auraCurseData == (UnityEngine.Object) null)
      auraCurseData = Globals.Instance.GetAuraCurseData(_acData.Id);
    if ((UnityEngine.Object) auraCurseData == (UnityEngine.Object) null || !auraCurseData.IsAura && this.IsInvulnerable() && auraCurseData.Id.ToLower() != "doom")
      return 0;
    this.ResetDictBonus();
    if (theCaster != null & useCharacterMods)
      charges += theCaster.GetAuraCurseQuantityModification(auraCurseData.Id, CC);
    if (auraCurseData.Preventable & canBePreventable)
    {
      if (this.AuracurseImmune.Contains(auraCurseData.Id) || this.AuraCurseImmuneByItems(auraCurseData.Id))
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<s>");
        stringBuilder.Append(Functions.UppercaseFirst(auraCurseData.ACName));
        stringBuilder.Append("</s>");
        string text = stringBuilder.ToString();
        Enums.CombatScrollEffectType type = !auraCurseData.IsAura ? Enums.CombatScrollEffectType.Curse : Enums.CombatScrollEffectType.Aura;
        if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
          this.heroItem.ScrollCombatText(text, type);
        else if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
          this.npcItem.ScrollCombatText(text, type);
        return 0;
      }
      for (int index = this.auraList.Count - 1; index >= 0; --index)
      {
        if (this.auraList[index] != null && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && !auraCurseData.IsAura && this.auraList[index].ACData.CursePreventedPerStack > 0 && this.auraList[index].GetCharges() > 0)
        {
          if (this.auraList[index].ACData.Id == "buffer")
          {
            if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
              EffectsManager.Instance.PlayEffectAC("buffer", true, this.heroItem.CharImageT, false);
            else if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
              EffectsManager.Instance.PlayEffectAC("buffer", false, this.npcItem.CharImageT, true);
            this.ConsumeEffectCharges(this.auraList[index].ACData.Id, 1);
          }
          else
            this.ConsumeEffectCharges(this.auraList[index].ACData.Id, 1);
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("<s>");
          stringBuilder.Append(auraCurseData.ACName);
          stringBuilder.Append("</s>");
          string text = stringBuilder.ToString();
          Enums.CombatScrollEffectType type = !auraCurseData.IsAura ? Enums.CombatScrollEffectType.Curse : Enums.CombatScrollEffectType.Aura;
          if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
            this.heroItem.ScrollCombatText(text, type);
          else if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
            this.npcItem.ScrollCombatText(text, type);
          return 0;
        }
      }
      for (int index = this.auraList.Count - 1; index >= 0; --index)
      {
        if (this.auraList[index] != null && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && this.auraList[index] != null && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && (UnityEngine.Object) this.auraList[index].ACData.PreventedAuraCurse != (UnityEngine.Object) null && this.auraList[index].ACData.PreventedAuraCurse.Id == auraCurseData.Id && !this.isHero && this.auraList[index].ACData.Id == "mark" && auraCurseData.Id == "stealth" && AtOManager.Instance.TeamHavePerk("mainperkmark1b"))
        {
          if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
          {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<s>");
            stringBuilder.Append(auraCurseData.ACName);
            stringBuilder.Append("</s>");
            this.npcItem.ScrollCombatText(stringBuilder.ToString(), Enums.CombatScrollEffectType.Aura);
          }
          return 0;
        }
      }
      for (int index = this.auraList.Count - 1; index >= 0; --index)
      {
        if (this.auraList[index] != null && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && this.auraList[index] != null && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && (UnityEngine.Object) this.auraList[index].ACData.PreventedAuraCurse != (UnityEngine.Object) null && this.auraList[index].ACData.PreventedAuraCurse.Id == auraCurseData.Id)
        {
          int num = Functions.FuncRoundToInt((float) (this.auraList[index].GetCharges() * this.auraList[index].ACData.PreventedAuraCurseStackPerStack));
          if (num >= charges)
          {
            this.ConsumeEffectCharges(this.auraList[index].ACData.Id, Functions.FuncRoundToInt((float) (charges / this.auraList[index].ACData.PreventedAuraCurseStackPerStack)));
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<s>");
            stringBuilder.Append(auraCurseData.ACName);
            stringBuilder.Append("</s>");
            string text = stringBuilder.ToString();
            Enums.CombatScrollEffectType type = !auraCurseData.IsAura ? Enums.CombatScrollEffectType.Curse : Enums.CombatScrollEffectType.Aura;
            if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
              this.heroItem.ScrollCombatText(text, type);
            else if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
              this.npcItem.ScrollCombatText(text, type);
            return 0;
          }
          this.auraList.RemoveAt(index);
          this.UpdateAuraCurseFunctions();
          charges -= num;
        }
      }
    }
    if ((UnityEngine.Object) auraCurseData.RemoveAuraCurse != (UnityEngine.Object) null || (UnityEngine.Object) auraCurseData.RemoveAuraCurse2 != (UnityEngine.Object) null)
    {
      for (int index = this.auraList.Count - 1; index >= 0; --index)
      {
        if (this.auraList[index] != null && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && ((UnityEngine.Object) auraCurseData.RemoveAuraCurse != (UnityEngine.Object) null && this.auraList[index].ACData.Id == auraCurseData.RemoveAuraCurse.Id || (UnityEngine.Object) auraCurseData.RemoveAuraCurse2 != (UnityEngine.Object) null && this.auraList[index].ACData.Id == auraCurseData.RemoveAuraCurse2.Id))
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("<s>");
          stringBuilder.Append(this.auraList[index].ACData.Id);
          stringBuilder.Append("</s>");
          string text = stringBuilder.ToString();
          Enums.CombatScrollEffectType type = !this.auraList[index].ACData.IsAura ? Enums.CombatScrollEffectType.Curse : Enums.CombatScrollEffectType.Aura;
          if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
            this.heroItem.ScrollCombatText(text, type);
          else if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
            this.npcItem.ScrollCombatText(text, type);
          this.auraList.RemoveAt(index);
        }
      }
    }
    theCaster?.SetEvent(Enums.EventActivation.AuraCurseSet, this, charges, auraCurseData.Id);
    if (auraCurseData.ExplodeAtStacks > 0 && charges >= auraCurseData.ExplodeAtStacks)
    {
      bool flag = false;
      for (int index = 0; index < this.auraList.Count; ++index)
      {
        if (this.auraList[index].ACData.Id == auraCurseData.Id)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        Aura aura = new Aura();
        aura.SetAura(auraCurseData, 0);
        this.auraList.Add(aura);
      }
    }
    AtOManager.Instance.GetNgPlus();
    if ((bool) (UnityEngine.Object) MatchManager.Instance && (UnityEngine.Object) auraCurseData != (UnityEngine.Object) null && theCaster != null)
      MatchManager.Instance.StoreStatusForCombatStats(this.id, auraCurseData.Id, theCaster.Id, charges);
    for (int index1 = 0; index1 < this.auraList.Count; ++index1)
    {
      if (this.auraList[index1] != null && (UnityEngine.Object) this.auraList[index1].ACData != (UnityEngine.Object) null && this.auraList[index1].ACData.Id == auraCurseData.Id)
      {
        int charges1 = this.auraList[index1].GetCharges();
        int num1 = auraCurseData.GetMaxCharges();
        if (num1 > 0 && num1 < charges1)
          num1 = charges1;
        int num2 = charges;
        if (auraCurseData.GainCharges)
        {
          if (num1 > -1 && num2 + charges1 > num1)
            num2 = num1 - charges1;
          this.auraList[index1].AddCharges(num2);
        }
        else if (charges1 < charges)
        {
          num2 = charges - charges1;
          if (num1 > -1 && num2 + charges1 > num1)
            num2 = num1 - charges1;
          this.auraList[index1].AddCharges(num2);
        }
        else
          num2 = 0;
        if (auraCurseData.Id == "vitality")
        {
          if (this.isHero && (MadnessManager.Instance.IsMadnessTraitActive("decadence") || AtOManager.Instance.IsChallengeTraitActive("decadence")))
            this.hpCurrent += Functions.FuncRoundToInt((float) (auraCurseData.CharacterStatModifiedValuePerStack * charges) * 0.5f);
          else
            this.hpCurrent += auraCurseData.CharacterStatModifiedValuePerStack * charges;
        }
        int charges2 = this.auraList[index1].GetCharges();
        if (auraCurseData.RevealCardsPerCharge > 0 && (UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
          this.npcItem.NPC.CheckRevealCardsCurse();
        if (auraCurseData.ExplodeAtStacks > 0 && auraCurseData.ExplodeAtStacks <= charges2)
        {
          if (!this.Alive || this.HpCurrent <= 0)
            return 0;
          if (auraCurseData.EffectTick != "")
          {
            if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
              EffectsManager.Instance.PlayEffectAC(auraCurseData.EffectTick, false, this.npcItem.CharImageT, false);
            else if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
              EffectsManager.Instance.PlayEffectAC(auraCurseData.EffectTick, true, this.heroItem.CharImageT, false);
          }
          if (auraCurseData.DamageSidesWhenConsumed > 0 || auraCurseData.DamageSidesWhenConsumedPerCharge > 0)
          {
            int damage = 0 + auraCurseData.DamageSidesWhenConsumed + auraCurseData.DamageSidesWhenConsumedPerCharge * charges2;
            if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
            {
              List<Hero> heroSides = MatchManager.Instance.GetHeroSides(this.position);
              for (int index2 = 0; index2 < heroSides.Count; ++index2)
              {
                if (auraCurseData.EffectTickSides != "" && (UnityEngine.Object) heroSides[index2].HeroItem != (UnityEngine.Object) null)
                  EffectsManager.Instance.PlayEffectAC(auraCurseData.EffectTickSides, true, heroSides[index2].HeroItem.CharImageT, false, 0.2f);
                heroSides[index2].IndirectDamage(auraCurseData.DamageTypeWhenConsumed, damage, effect: auraCurseData.Id, sourceCharacterName: this.gameName, sourceCharacterId: this.id);
              }
            }
            else if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
            {
              List<NPC> npcSides = MatchManager.Instance.GetNPCSides(this.position);
              for (int index3 = 0; index3 < npcSides.Count; ++index3)
              {
                if (auraCurseData.EffectTickSides != "" && (UnityEngine.Object) npcSides[index3].NPCItem != (UnityEngine.Object) null)
                  EffectsManager.Instance.PlayEffectAC(auraCurseData.EffectTickSides, false, npcSides[index3].NPCItem.CharImageT, false, 0.2f);
                npcSides[index3].IndirectDamage(auraCurseData.DamageTypeWhenConsumed, damage, effect: auraCurseData.Id, sourceCharacterName: this.gameName, sourceCharacterId: this.id);
              }
            }
          }
          if (auraCurseData.DamageWhenConsumed > 0 || (double) auraCurseData.DamageWhenConsumedPerCharge > 0.0)
          {
            int num3 = 0 + auraCurseData.DamageWhenConsumed;
            int num4 = charges2;
            if ((UnityEngine.Object) auraCurseData.ConsumedDamageChargesBasedOnACCharges != (UnityEngine.Object) null)
              num4 = this.GetAuraCharges(auraCurseData.ConsumedDamageChargesBasedOnACCharges.Id);
            int damage = num3 + Functions.FuncRoundToInt(auraCurseData.DamageWhenConsumedPerCharge * (float) num4);
            int auraCharges = this.GetAuraCharges("scourge");
            if (auraCharges > 0)
              damage += Functions.FuncRoundToInt((float) damage * 0.1f * (float) auraCharges);
            this.IndirectDamage(auraCurseData.DamageTypeWhenConsumed, damage, effect: auraCurseData.Id, sourceCharacterName: this.gameName);
          }
          this.ConsumeEffectCharges(auraCurseData.Id);
          return 3;
        }
        if (num2 > 0)
          this.UpdateAuraCurseFunctions(this.auraList[index1].ACData, num2, charges2);
        else
          this.UpdateAuraCurseFunctions();
        if (auraCurseData.CombatlogShow)
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("<sprite name=");
          stringBuilder.Append(auraCurseData.Id);
          stringBuilder.Append(">");
          stringBuilder.Append(Functions.UppercaseFirst(auraCurseData.ACName));
          string text = stringBuilder.ToString();
          Enums.CombatScrollEffectType type = !auraCurseData.IsAura ? Enums.CombatScrollEffectType.Curse : Enums.CombatScrollEffectType.Aura;
          if (!fromTrait)
          {
            if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
              this.heroItem.ScrollCombatText(text, type);
            else if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
              this.npcItem.ScrollCombatText(text, type);
          }
        }
        return 2;
      }
    }
    if (auraCurseData.ConsumedAtCast && auraCurseData.ConsumeAll)
    {
      this.UpdateAuraCurseFunctions();
      return 3;
    }
    Aura aura1 = new Aura();
    int num5 = charges;
    int maxCharges = auraCurseData.GetMaxCharges();
    if (maxCharges > -1 && num5 > maxCharges)
      num5 = maxCharges;
    aura1.SetAura(auraCurseData, num5);
    this.auraList.Add(aura1);
    if (this.AuraList.Count > 0)
    {
      List<int> intList = new List<int>();
      int num6 = -10;
      int num7 = -1;
      for (int index4 = 0; index4 < this.AuraList.Count; ++index4)
      {
        for (int index5 = 0; index5 < this.AuraList.Count; ++index5)
        {
          if (this.AuraList[index5] != null && !intList.Contains(index5) && (UnityEngine.Object) this.AuraList[index5].ACData != (UnityEngine.Object) null && this.AuraList[index5].ACData.PriorityOnConsumption > num6)
          {
            num7 = index5;
            num6 = this.AuraList[index5].ACData.PriorityOnConsumption;
          }
        }
        num6 = -10;
        if (num7 > -1)
          intList.Add(num7);
      }
      List<Aura> auraList = new List<Aura>();
      for (int index = 0; index < intList.Count; ++index)
        auraList.Add(this.AuraList[intList[index]]);
      this.AuraList = auraList;
    }
    if (auraCurseData.Id == "vitality")
    {
      if (this.isHero && (MadnessManager.Instance.IsMadnessTraitActive("decadence") || AtOManager.Instance.IsChallengeTraitActive("decadence")))
        this.hpCurrent += Functions.FuncRoundToInt((float) (auraCurseData.CharacterStatModifiedValuePerStack * charges) * 0.5f);
      else
        this.hpCurrent += auraCurseData.CharacterStatModifiedValuePerStack * charges;
    }
    if (auraCurseData.RevealCardsPerCharge > 0)
    {
      if ((UnityEngine.Object) this.npcItem == (UnityEngine.Object) null)
        return 0;
      this.npcItem.NPC.CheckRevealCardsCurse();
    }
    this.UpdateAuraCurseFunctions(auraCurseData, num5);
    if (auraCurseData.CombatlogShow && !fromTrait)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<sprite name=");
      stringBuilder.Append(auraCurseData.Id);
      stringBuilder.Append(">");
      stringBuilder.Append(Functions.UppercaseFirst(auraCurseData.ACName));
      string text = stringBuilder.ToString();
      Enums.CombatScrollEffectType type = !auraCurseData.IsAura ? Enums.CombatScrollEffectType.Curse : Enums.CombatScrollEffectType.Aura;
      if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
        this.heroItem.ScrollCombatText(text, type);
      else if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
        this.npcItem.ScrollCombatText(text, type);
    }
    return 1;
  }

  public void RemoveAuraCurses() => this.auraList.Clear();

  public List<string> CharacterImmunitiesList()
  {
    List<string> stringList1 = new List<string>();
    if (this.AuracurseImmune.Count > 0)
    {
      for (int index = 0; index < this.AuracurseImmune.Count; ++index)
        stringList1.Add(this.AuracurseImmune[index]);
    }
    List<string> stringList2 = this.AuraCurseImmunitiesByItemsList();
    for (int index = 0; index < stringList2.Count; ++index)
    {
      if (!stringList1.Contains(stringList2[index]))
        stringList1.Add(stringList2[index]);
    }
    return stringList1;
  }

  public List<string> AuraCurseImmunitiesByItemsList()
  {
    List<string> stringList = new List<string>();
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null)
        {
          if ((UnityEngine.Object) itemDataBySlot.AuracurseImmune1 != (UnityEngine.Object) null && !stringList.Contains(itemDataBySlot.AuracurseImmune1.Id))
            stringList.Add(itemDataBySlot.AuracurseImmune1.Id);
          if ((UnityEngine.Object) itemDataBySlot.AuracurseImmune2 != (UnityEngine.Object) null && !stringList.Contains(itemDataBySlot.AuracurseImmune2.Id))
            stringList.Add(itemDataBySlot.AuracurseImmune2.Id);
        }
      }
    }
    if (this.isHero && stringList.Contains("bleed") && AtOManager.Instance.CharacterHavePerk(this.subclassName, "mainperkfury1c"))
      stringList.Remove("bleed");
    return stringList;
  }

  public bool AuraCurseImmuneByItems(string acName)
  {
    if (acName == "bleed" && AtOManager.Instance.CharacterHavePerk(this.subclassName, "mainperkfury1c"))
      return false;
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheAuraCurseImmuneByItems.ContainsKey(acName))
      return this.cacheAuraCurseImmuneByItems[acName];
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null)
        {
          if ((UnityEngine.Object) itemDataBySlot.AuracurseImmune1 != (UnityEngine.Object) null && itemDataBySlot.AuracurseImmune1.Id == acName)
          {
            this.cacheAuraCurseImmuneByItems.Add(acName, true);
            return true;
          }
          if ((UnityEngine.Object) itemDataBySlot.AuracurseImmune2 != (UnityEngine.Object) null && itemDataBySlot.AuracurseImmune2.Id == acName)
          {
            this.cacheAuraCurseImmuneByItems.Add(acName, true);
            return true;
          }
        }
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
      this.cacheAuraCurseImmuneByItems.Add(acName, false);
    return false;
  }

  public bool HasEffect(string effect)
  {
    effect = effect.ToLower();
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if (index < this.auraList.Count && this.auraList[index] != null && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && this.auraList[index].ACData.Id == effect)
        return true;
    }
    return false;
  }

  public bool HasAnyAura()
  {
    if (this.auraList != null)
    {
      for (int index = 0; index < this.auraList.Count; ++index)
      {
        if (this.auraList[index].ACData.IsAura)
          return true;
      }
    }
    return false;
  }

  public bool HasAnyCurse()
  {
    if (this.auraList != null)
    {
      for (int index = 0; index < this.auraList.Count; ++index)
      {
        if (!this.auraList[index].ACData.IsAura)
          return true;
      }
    }
    return false;
  }

  public List<string> GetCurseList()
  {
    List<string> curseList = new List<string>();
    if (this.auraList != null)
    {
      for (int index = 0; index < this.auraList.Count; ++index)
      {
        if (!this.auraList[index].ACData.IsAura)
          curseList.Add(this.auraList[index].ACData.Id);
      }
    }
    return curseList;
  }

  public bool HasEffectSkipsTurn()
  {
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if (this.auraList[index] != null && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && this.auraList[index].ACData.SkipsNextTurn)
        return true;
    }
    return false;
  }

  public int EffectCharges(string effect)
  {
    effect = effect.ToLower();
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if (this.auraList[index] != null && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && this.auraList[index].ACData.Id == effect)
        return this.auraList[index].AuraCharges;
    }
    return 0;
  }

  public void ConsumeEffectCharges(string effect, int charges = -10000)
  {
    effect = effect.ToLower();
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if (this.auraList[index] != null && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && this.auraList[index].ACData.Id == effect)
      {
        if (charges == -10000)
          charges = this.auraList[index].AuraCharges;
        this.auraList[index].AuraCharges -= charges;
        if (effect == "block")
        {
          if (this.auraList[index].AuraCharges <= 0)
            this.auraList[index].AuraCharges = 0;
          this.UpdateAuraCurseFunctions();
          break;
        }
        if (this.auraList[index].AuraCharges <= 0)
        {
          this.ResetDictBonus();
          if (effect != "block" && effect != "dark")
            this.auraList[index].AuraCharges = 1;
          MatchManager.Instance.ConsumeAuraCurse("Now", this, effect);
          break;
        }
        this.UpdateAuraCurseFunctions();
        break;
      }
    }
  }

  public void HealCurses(int numCurses)
  {
    bool flag = false;
    if (numCurses == -1)
      numCurses = this.auraList.Count;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<s>");
    for (int index1 = 0; index1 < numCurses; ++index1)
    {
      for (int index2 = 0; index2 < this.auraList.Count; ++index2)
      {
        if (!this.auraList[index2].ACData.IsAura && this.auraList[index2].ACData.Removable)
        {
          stringBuilder.Append(Functions.UppercaseFirst(this.auraList[index2].ACData.ACName));
          stringBuilder.Append("\n");
          this.auraList.RemoveAt(index2);
          flag = true;
          break;
        }
      }
      if (!flag)
        break;
    }
    if (flag)
    {
      stringBuilder.Append("</s>");
      if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
        this.heroItem.ScrollCombatText(stringBuilder.ToString(), Enums.CombatScrollEffectType.Curse);
      else if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
        this.npcItem.ScrollCombatText(stringBuilder.ToString(), Enums.CombatScrollEffectType.Curse);
      this.UpdateAuraCurseFunctions();
    }
  }

  public void HealAuraCurse(AuraCurseData AC)
  {
    for (int index = this.auraList.Count - 1; index >= 0; --index)
    {
      if (this.auraList[index] != null && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && this.auraList[index].ACData.Id == AC.Id)
      {
        this.auraList.RemoveAt(index);
        this.UpdateAuraCurseFunctions();
        break;
      }
    }
  }

  public void HealCursesName(List<string> curseList = null, string singleCurse = "")
  {
    bool flag = false;
    if (curseList == null && singleCurse != "")
    {
      curseList = new List<string>();
      curseList.Add(singleCurse);
    }
    for (int index = this.auraList.Count - 1; index >= 0; --index)
    {
      if (this.auraList[index] != null && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && curseList.Contains(this.auraList[index].ACData.Id))
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<s>");
        stringBuilder.Append(Functions.UppercaseFirst(this.auraList[index].ACData.Id));
        stringBuilder.Append("</s>");
        if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
          this.heroItem.ScrollCombatText(stringBuilder.ToString(), Enums.CombatScrollEffectType.Curse);
        else if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
          this.npcItem.ScrollCombatText(stringBuilder.ToString(), Enums.CombatScrollEffectType.Curse);
        this.auraList.RemoveAt(index);
        flag = true;
      }
    }
    if (!flag)
      return;
    this.UpdateAuraCurseFunctions();
  }

  public void DispelAuras(int numAuras)
  {
    bool flag = false;
    if (numAuras == -1)
      numAuras = this.auraList.Count;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<s>");
    for (int index1 = 0; index1 < numAuras; ++index1)
    {
      for (int index2 = 0; index2 < this.auraList.Count; ++index2)
      {
        if (this.auraList[index2].ACData.IsAura && this.auraList[index2].ACData.Removable)
        {
          stringBuilder.Append(Functions.UppercaseFirst(this.auraList[index2].ACData.ACName));
          stringBuilder.Append("\n");
          this.auraList.RemoveAt(index2);
          flag = true;
          break;
        }
      }
      if (!flag)
        break;
    }
    if (!flag)
      return;
    stringBuilder.Append("</s>");
    if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
      this.heroItem.ScrollCombatText(stringBuilder.ToString(), Enums.CombatScrollEffectType.Curse);
    else if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null)
      this.npcItem.ScrollCombatText(stringBuilder.ToString(), Enums.CombatScrollEffectType.Curse);
    this.UpdateAuraCurseFunctions();
  }

  private void CheckGainBlockCharges(int charges)
  {
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if (this.auraList[index].ACData.BlockChargesGainedPerStack > 0)
      {
        this.SetAura(this, Globals.Instance.GetAuraCurseData("block"), this.auraList[index].ACData.BlockChargesGainedPerStack * charges);
        break;
      }
    }
  }

  public void DamageReflected(Hero theCasterHero, NPC theCasterNPC)
  {
    if (this.isHero && theCasterHero != null)
      return;
    Dictionary<Enums.DamageType, int> dictionary = new Dictionary<Enums.DamageType, int>();
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if (this.auraList[index] != null)
      {
        AuraCurseData acData = this.auraList[index].ACData;
        if ((UnityEngine.Object) acData != (UnityEngine.Object) null)
        {
          if (acData.DamageReflectedPerStack > 0)
          {
            if (acData.Id == "thorns" && theCasterNPC != null && AtOManager.Instance.CharacterHavePerk(this.subclassName, "mainperkthorns1c"))
            {
              theCasterNPC.SetAura(this, Globals.Instance.GetAuraCurseData("poison"), Functions.FuncRoundToInt((float) this.auraList[index].AuraCharges * 0.5f));
            }
            else
            {
              int damage = acData.DamageReflectedPerStack * this.auraList[index].AuraCharges;
              if (theCasterHero != null)
                theCasterHero.IndirectDamage(acData.DamageReflectedType, damage, effect: acData.Id, sourceCharacterName: this.gameName, sourceCharacterId: this.id);
              else
                theCasterNPC?.IndirectDamage(acData.DamageReflectedType, damage, effect: acData.Id, sourceCharacterName: this.gameName, sourceCharacterId: this.id);
            }
          }
          if (acData.DamageReflectedConsumeCharges > 0)
            this.ConsumeEffectCharges(acData.Id, acData.DamageReflectedConsumeCharges);
        }
      }
    }
  }

  public void SimpleHeal(int heal)
  {
    if (!this.Alive || heal <= 0)
      return;
    this.ModifyHp(heal, false);
    CastResolutionForCombatText _cast = new CastResolutionForCombatText();
    _cast.heal = heal;
    if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
    {
      this.heroItem.ScrollCombatTextDamageNew(_cast);
    }
    else
    {
      if (!((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null))
        return;
      this.npcItem.ScrollCombatTextDamageNew(_cast);
    }
  }

  public void PercentHeal(float _healPercent, bool _includeInStats)
  {
    Debug.Log((object) nameof (PercentHeal));
    if (!this.Alive)
      return;
    int _hp = Functions.FuncRoundToInt((float) ((double) this.GetMaxHP() * (double) _healPercent * 0.0099999997764825821));
    if (_hp == 0)
      return;
    this.ModifyHp(_hp, _includeInStats);
  }

  public void HealAttacker(Hero theCasterHero, NPC theCasterNPC)
  {
    if (this.isHero && theCasterHero != null || theCasterHero != null && !theCasterHero.Alive || theCasterNPC != null && !theCasterNPC.Alive)
      return;
    Dictionary<Enums.DamageType, int> dictionary = new Dictionary<Enums.DamageType, int>();
    for (int index1 = 0; index1 < this.auraList.Count; ++index1)
    {
      if (this.auraList[index1] != null)
      {
        AuraCurseData acData = this.auraList[index1].ACData;
        if ((UnityEngine.Object) acData != (UnityEngine.Object) null && acData.HealAttackerPerStack > 0)
        {
          int heal = acData.HealAttackerPerStack * this.auraList[index1].AuraCharges;
          if (theCasterHero != null)
          {
            if (theCasterHero.GetHpLeftForMax() > 0)
            {
              int num = theCasterHero.HealReceivedFinal(heal, true);
              if (theCasterHero.GetHpLeftForMax() < heal)
                num = theCasterHero.GetHpLeftForMax();
              if (num > 0)
              {
                MatchManager.Instance.DamageStatusFromCombatStats(this.id, "sanctify", num);
                theCasterHero.SimpleHeal(num);
              }
              if (acData.Id == "sanctify" && theCasterHero.SubclassName == "paladin" && AtOManager.Instance.TeamHaveTrait("beaconoflight"))
              {
                List<Hero> heroSides = MatchManager.Instance.GetHeroSides(theCasterHero.position);
                for (int index2 = 0; index2 < heroSides.Count; ++index2)
                  this.traitClass.DoTrait(Enums.EventActivation.None, "overflow", (Character) theCasterHero, (Character) heroSides[index2], theCasterHero.HealReceivedFinal(heal, true), "", (CardData) null);
              }
            }
            else if (acData.Id == "sanctify" && theCasterHero.SubclassName == "paladin" && AtOManager.Instance.TeamHaveTrait("overflow"))
            {
              List<Hero> heroSides = MatchManager.Instance.GetHeroSides(theCasterHero.position);
              for (int index3 = 0; index3 < heroSides.Count; ++index3)
                this.traitClass.DoTrait(Enums.EventActivation.None, "overflow", (Character) theCasterHero, (Character) heroSides[index3], theCasterHero.HealReceivedFinal(heal, true), "", (CardData) null);
            }
          }
          else if (theCasterNPC != null)
          {
            int num = theCasterNPC.HealReceivedFinal(heal, true);
            if (theCasterNPC.GetHpLeftForMax() < heal)
              num = theCasterNPC.GetHpLeftForMax();
            if (num > 0)
            {
              MatchManager.Instance.DamageStatusFromCombatStats(this.id, "sanctify", num);
              theCasterNPC.SimpleHeal(num);
            }
          }
          if (acData.HealAttackerConsumeCharges > 0)
            this.ConsumeEffectCharges(acData.Id, acData.HealAttackerConsumeCharges);
        }
      }
    }
  }

  public void SetStealth()
  {
    if (this.IsParalyzed())
      return;
    if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
    {
      this.heroItem.SetStealth(this.HasEffect("stealth"));
    }
    else
    {
      if (!((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null))
        return;
      this.npcItem.SetStealth(this.HasEffect("stealth"));
    }
  }

  public void SetParalyze()
  {
    if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
    {
      this.heroItem.SetParalyze(this.IsParalyzed());
    }
    else
    {
      if (!((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null))
        return;
      this.npcItem.SetParalyze(this.IsParalyzed());
    }
  }

  private void SetOverDebuff()
  {
    if (!((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null))
      return;
    StringBuilder stringBuilder = new StringBuilder();
    if (this.HasEffect("disarm"))
    {
      stringBuilder.Append("<size=+.5><sprite name=disarm></size>");
      stringBuilder.Append(Texts.Instance.GetText("disarm"));
    }
    if (this.HasEffect("silence"))
    {
      if (stringBuilder.Length > 0)
        stringBuilder.Append("<br>");
      stringBuilder.Append("<size=+.5><sprite name=silence></size>");
      stringBuilder.Append(Texts.Instance.GetText("silence"));
    }
    this.heroItem.SetOverDebuff(stringBuilder.ToString());
  }

  public void SetTaunt()
  {
    if (this.IsParalyzed())
      return;
    if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null)
    {
      this.heroItem.SetTaunt(this.IsTaunted());
    }
    else
    {
      if (!((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null))
        return;
      this.npcItem.SetTaunt(this.IsTaunted());
    }
  }

  public bool IsStealthed() => this.HasEffect("stealth");

  public bool IsTaunted() => this.HasEffect("taunt");

  public bool IsParalyzed() => this.HasEffect("paralyze");

  public bool IsInvulnerable() => this.HasEffect("invulnerable");

  public bool IsImmune(Enums.DamageType damageType)
  {
    switch (damageType)
    {
      case Enums.DamageType.Slashing:
        if (this.immuneSlashing)
          return true;
        break;
      case Enums.DamageType.Blunt:
        if (this.immuneBlunt)
          return true;
        break;
      case Enums.DamageType.Piercing:
        if (this.immunePiercing)
          return true;
        break;
      case Enums.DamageType.Fire:
        if (this.immuneFire)
          return true;
        break;
      case Enums.DamageType.Cold:
        if (this.immuneCold)
          return true;
        break;
      case Enums.DamageType.Lightning:
        if (this.immuneLightning)
          return true;
        break;
      case Enums.DamageType.Mind:
        if (this.immuneMind)
          return true;
        break;
      case Enums.DamageType.Holy:
        if (this.immuneHoly)
          return true;
        break;
      case Enums.DamageType.Shadow:
        if (this.immuneShadow)
          return true;
        break;
    }
    return false;
  }

  public int GetCardCostModifiers()
  {
    int cardCostModifiers = 0;
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if (this.auraList[index] != null)
      {
        AuraCurseData acData = this.auraList[index].ACData;
        if ((UnityEngine.Object) acData != (UnityEngine.Object) null && acData.ModifyCardCostPerChargeNeededForOne > 0)
          cardCostModifiers += Mathf.FloorToInt(1f / (float) acData.ModifyCardCostPerChargeNeededForOne * (float) this.auraList[index].AuraCharges);
      }
    }
    return cardCostModifiers;
  }

  public int GetCardFinalCost(CardData cardData) => (UnityEngine.Object) cardData != (UnityEngine.Object) null ? cardData.GetCardFinalCost() : 0;

  public int GetAuraCurseTotal(bool _auras, bool _curses)
  {
    int auraCurseTotal = 0;
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if (this.auraList[index] != null)
      {
        AuraCurseData acData = this.auraList[index].ACData;
        if ((UnityEngine.Object) acData != (UnityEngine.Object) null)
        {
          if (acData.IsAura & _auras)
            ++auraCurseTotal;
          else if (!acData.IsAura & _curses)
            ++auraCurseTotal;
        }
      }
    }
    return auraCurseTotal;
  }

  public int GetAuraDrawModifiers(bool writeVar = true)
  {
    int auraDrawModifiers = 0;
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if (this.auraList[index] != null)
      {
        AuraCurseData acData = this.auraList[index].ACData;
        if ((UnityEngine.Object) acData != (UnityEngine.Object) null && acData.CardsDrawPerStack != 0)
          auraDrawModifiers += acData.CardsDrawPerStack * this.auraList[index].AuraCharges;
      }
    }
    if (writeVar)
      this.drawModifier = auraDrawModifiers;
    return auraDrawModifiers;
  }

  public bool CanPlayCard(CardData cardData)
  {
    List<Enums.CardType> disabledCardTypes = this.GetAuraDisabledCardTypes();
    if (disabledCardTypes == null || disabledCardTypes.Count == 0)
      return true;
    if (disabledCardTypes.Contains(cardData.CardType))
      return false;
    for (int index = 0; index < cardData.CardTypeAux.Length; ++index)
    {
      if (disabledCardTypes.Contains(cardData.CardTypeAux[index]))
        return false;
    }
    return true;
  }

  public bool CanPlayCardSummon(CardData cardData) => !((UnityEngine.Object) cardData.SummonUnit != (UnityEngine.Object) null) || MatchManager.Instance.GetNPCAvailablePosition() != -1;

  public List<Enums.CardType> GetAuraDisabledCardTypes()
  {
    List<Enums.CardType> disabledCardTypes = new List<Enums.CardType>();
    for (int index1 = 0; index1 < this.auraList.Count; ++index1)
    {
      if (this.auraList[index1] != null)
      {
        AuraCurseData acData = this.auraList[index1].ACData;
        if ((UnityEngine.Object) acData != (UnityEngine.Object) null && acData.DisabledCardTypes != null && acData.DisabledCardTypes.Length != 0)
        {
          for (int index2 = 0; index2 < acData.DisabledCardTypes.Length; ++index2)
            disabledCardTypes.Add(acData.DisabledCardTypes[index2]);
        }
      }
    }
    return disabledCardTypes;
  }

  public int GetAuraCharges(string ACName)
  {
    if (this.auraList != null)
    {
      for (int index = 0; index < this.auraList.Count; ++index)
      {
        if (this.auraList[index] != null && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && this.auraList[index].ACData.Id == ACName.ToLower())
          return this.auraList[index].AuraCharges;
      }
      if (ACName.ToLower() == "stealthbonus")
      {
        for (int index = 0; index < this.auraList.Count; ++index)
        {
          if (this.auraList[index] != null && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null && this.auraList[index].ACData.Id == "stealth")
            return this.auraList[index].AuraCharges;
        }
      }
    }
    return 0;
  }

  public bool HasCardDataString(string _cardDataId, bool _checkUpgrades)
  {
    _cardDataId = _cardDataId.ToLower();
    List<string> listVarsFromCard = Functions.GetIdListVarsFromCard(_cardDataId);
    for (int index = 0; index < this.cards.Count; ++index)
    {
      if (this.cards[index] == _cardDataId || _checkUpgrades && listVarsFromCard != null && listVarsFromCard.Contains(this.cards[index]))
        return true;
    }
    return false;
  }

  public bool HasItemCardData(CardData itemCardData)
  {
    string id = itemCardData.Id;
    return this.weapon != "" && (UnityEngine.Object) Globals.Instance.GetCardData(this.weapon, false) != (UnityEngine.Object) null && Globals.Instance.GetCardData(this.weapon, false).Id == id || this.armor != "" && (UnityEngine.Object) Globals.Instance.GetCardData(this.armor, false) != (UnityEngine.Object) null && Globals.Instance.GetCardData(this.armor, false).Id == id || this.jewelry != "" && (UnityEngine.Object) Globals.Instance.GetCardData(this.jewelry, false) != (UnityEngine.Object) null && Globals.Instance.GetCardData(this.jewelry, false).Id == id || this.accesory != "" && (UnityEngine.Object) Globals.Instance.GetCardData(this.accesory, false) != (UnityEngine.Object) null && Globals.Instance.GetCardData(this.accesory, false).Id == id || this.pet != "" && (UnityEngine.Object) Globals.Instance.GetCardData(this.pet, false) != (UnityEngine.Object) null && Globals.Instance.GetCardData(this.pet, false).Id == id || this.enchantment != "" && (UnityEngine.Object) Globals.Instance.GetCardData(this.enchantment, false) != (UnityEngine.Object) null && Globals.Instance.GetCardData(this.enchantment, false).Id == id || this.enchantment2 != "" && (UnityEngine.Object) Globals.Instance.GetCardData(this.enchantment2, false) != (UnityEngine.Object) null && Globals.Instance.GetCardData(this.enchantment2, false).Id == id || this.enchantment3 != "" && (UnityEngine.Object) Globals.Instance.GetCardData(this.enchantment3, false) != (UnityEngine.Object) null && Globals.Instance.GetCardData(this.enchantment3, false).Id == id;
  }

  public CardData GetCardDataBySlot(int slot, bool useCache = true)
  {
    if (useCache && slot != 6 && slot != 4 && (UnityEngine.Object) this.cardDataBySlot[slot] != (UnityEngine.Object) null)
      return this.cardDataBySlot[slot];
    string id = "";
    switch (slot)
    {
      case 0:
        id = this.weapon;
        break;
      case 1:
        id = this.armor;
        break;
      case 2:
        id = this.jewelry;
        break;
      case 3:
        id = this.accesory;
        break;
      case 4:
        id = this.corruption;
        break;
      case 5:
        id = this.pet;
        break;
      case 6:
        id = this.enchantment;
        break;
      case 7:
        id = this.enchantment2;
        break;
      case 8:
        id = this.enchantment3;
        break;
    }
    if (id != "")
    {
      CardData cardData = Globals.Instance.GetCardData(id);
      if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
      {
        this.cardDataBySlot[slot] = cardData;
        return cardData;
      }
    }
    return (CardData) null;
  }

  public void RefreshItems(int slot = -1)
  {
    if (slot == -1)
    {
      for (int slot1 = 0; slot1 < 8; ++slot1)
        this.GetItemDataBySlot(slot1, false);
    }
    else
      this.GetItemDataBySlot(slot, false);
  }

  public void ResetItemDataBySlotCache(int slot = -1)
  {
    if (slot == -1)
    {
      this.itemDataBySlot = new ItemData[9];
      this.cardDataBySlot = new CardData[9];
    }
    else
    {
      this.itemDataBySlot[slot] = (ItemData) null;
      this.cardDataBySlot[slot] = (CardData) null;
    }
  }

  public ItemData GetItemDataBySlot(int slot, bool useCache = true)
  {
    string id = "";
    switch (slot)
    {
      case 0:
        id = this.weapon;
        break;
      case 1:
        id = this.armor;
        break;
      case 2:
        id = this.jewelry;
        break;
      case 3:
        id = this.accesory;
        break;
      case 4:
        id = this.corruption;
        break;
      case 5:
        id = this.pet;
        break;
      case 6:
        id = this.enchantment;
        break;
      case 7:
        id = this.enchantment2;
        break;
      case 8:
        id = this.enchantment3;
        break;
    }
    if (id != "")
    {
      CardData cardData = Globals.Instance.GetCardData(id, false);
      if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
      {
        this.itemDataBySlot[slot] = cardData.CardType != Enums.CardType.Enchantment ? cardData.Item : cardData.ItemEnchantment;
        return this.itemDataBySlot[slot];
      }
    }
    return (ItemData) null;
  }

  public int GetItemsMaxHPModifier()
  {
    int itemsMaxHpModifier = 0;
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null && itemDataBySlot.MaxHealth != 0)
          itemsMaxHpModifier += itemDataBySlot.MaxHealth;
      }
    }
    return itemsMaxHpModifier;
  }

  public int GetItemHealFlatBonus()
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetItemHealFlatBonus.Count > 0)
      return this.cacheGetItemHealFlatBonus[0];
    int itemHealFlatBonus = 0;
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null && itemDataBySlot.HealFlatBonus != 0)
          itemHealFlatBonus += itemDataBySlot.HealFlatBonus;
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
      this.cacheGetItemHealFlatBonus.Add(itemHealFlatBonus);
    return itemHealFlatBonus;
  }

  public float GetItemHealPercentBonus()
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetItemHealPercentBonus.Count > 0)
      return this.cacheGetItemHealPercentBonus[0];
    float healPercentBonus = 0.0f;
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null && (double) itemDataBySlot.HealPercentBonus != 0.0)
          healPercentBonus += itemDataBySlot.HealPercentBonus;
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
      this.cacheGetItemHealPercentBonus.Add(healPercentBonus);
    return healPercentBonus;
  }

  public int GetItemHealReceivedFlatBonus()
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetItemHealReceivedFlatBonus.Count > 0)
      return this.cacheGetItemHealReceivedFlatBonus[0];
    int receivedFlatBonus = 0;
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null && itemDataBySlot.HealReceivedFlatBonus != 0)
          receivedFlatBonus += itemDataBySlot.HealReceivedFlatBonus;
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
      this.cacheGetItemHealReceivedFlatBonus.Add(receivedFlatBonus);
    return receivedFlatBonus;
  }

  public float GetItemHealReceivedPercentBonus()
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetItemHealReceivedPercentBonus.Count > 0)
      return this.cacheGetItemHealReceivedPercentBonus[0];
    float receivedPercentBonus = 0.0f;
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null && (double) itemDataBySlot.HealReceivedPercentBonus != 0.0)
          receivedPercentBonus += itemDataBySlot.HealReceivedPercentBonus;
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
      this.cacheGetItemHealReceivedPercentBonus.Add(receivedPercentBonus);
    return receivedPercentBonus;
  }

  public Dictionary<string, int> GetItemHealPercentDictionary()
  {
    Dictionary<string, int> percentDictionary = new Dictionary<string, int>();
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        float num = 0.0f;
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null)
        {
          if ((double) itemDataBySlot.HealPercentBonus != 0.0)
            num += itemDataBySlot.HealPercentBonus;
          if ((double) num != 0.0)
          {
            StringBuilder stringBuilder = new StringBuilder();
            CardData cardDataBySlot = this.GetCardDataBySlot(slot);
            stringBuilder.Append(cardDataBySlot.CardName);
            stringBuilder.Append("_");
            stringBuilder.Append((object) cardDataBySlot.CardType);
            percentDictionary.Add(stringBuilder.ToString(), (int) num);
          }
        }
      }
    }
    return percentDictionary;
  }

  public Dictionary<string, int> GetItemHealFlatDictionary()
  {
    Dictionary<string, int> healFlatDictionary = new Dictionary<string, int>();
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        float num = 0.0f;
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null)
        {
          if (itemDataBySlot.HealFlatBonus != 0)
            num += (float) itemDataBySlot.HealFlatBonus;
          if ((double) num != 0.0)
          {
            StringBuilder stringBuilder = new StringBuilder();
            CardData cardDataBySlot = this.GetCardDataBySlot(slot);
            stringBuilder.Append(cardDataBySlot.CardName);
            stringBuilder.Append("_");
            stringBuilder.Append((object) cardDataBySlot.CardType);
            healFlatDictionary.Add(stringBuilder.ToString(), (int) num);
          }
        }
      }
    }
    return healFlatDictionary;
  }

  public Dictionary<string, int> GetItemAuraCurseModifiers()
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetItemAuraCurseModifiers.Count > 0)
      return this.cacheGetItemAuraCurseModifiers;
    Dictionary<string, int> auraCurseModifiers = new Dictionary<string, int>();
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null)
        {
          if ((UnityEngine.Object) itemDataBySlot.AuracurseBonus1 != (UnityEngine.Object) null)
          {
            if (auraCurseModifiers.ContainsKey(itemDataBySlot.AuracurseBonus1.Id))
              auraCurseModifiers[itemDataBySlot.AuracurseBonus1.Id] += itemDataBySlot.AuracurseBonusValue1;
            else
              auraCurseModifiers[itemDataBySlot.AuracurseBonus1.Id] = itemDataBySlot.AuracurseBonusValue1;
          }
          if ((UnityEngine.Object) itemDataBySlot.AuracurseBonus2 != (UnityEngine.Object) null)
          {
            if (auraCurseModifiers.ContainsKey(itemDataBySlot.AuracurseBonus2.Id))
              auraCurseModifiers[itemDataBySlot.AuracurseBonus2.Id] += itemDataBySlot.AuracurseBonusValue2;
            else
              auraCurseModifiers[itemDataBySlot.AuracurseBonus2.Id] = itemDataBySlot.AuracurseBonusValue2;
          }
        }
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
      this.cacheGetItemAuraCurseModifiers = auraCurseModifiers;
    return auraCurseModifiers;
  }

  public Dictionary<string, int> GetTraitAuraCurseModifiers()
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetTraitAuraCurseModifiers.Count > 0)
      return this.cacheGetTraitAuraCurseModifiers;
    Dictionary<string, int> auraCurseModifiers = new Dictionary<string, int>();
    if (this.traits != null)
    {
      for (int index = 0; index < this.traits.Length; ++index)
      {
        if (this.traits[index] != null)
        {
          TraitData traitData = Globals.Instance.GetTraitData(this.traits[index]);
          if ((UnityEngine.Object) traitData != (UnityEngine.Object) null)
          {
            if ((UnityEngine.Object) traitData.AuracurseBonus1 != (UnityEngine.Object) null)
            {
              if (auraCurseModifiers.ContainsKey(traitData.AuracurseBonus1.Id))
                auraCurseModifiers[traitData.AuracurseBonus1.Id] += traitData.AuracurseBonusValue1;
              else
                auraCurseModifiers[traitData.AuracurseBonus1.Id] = traitData.AuracurseBonusValue1;
            }
            if ((UnityEngine.Object) traitData.AuracurseBonus2 != (UnityEngine.Object) null)
            {
              if (auraCurseModifiers.ContainsKey(traitData.AuracurseBonus2.Id))
                auraCurseModifiers[traitData.AuracurseBonus2.Id] += traitData.AuracurseBonusValue2;
              else
                auraCurseModifiers[traitData.AuracurseBonus2.Id] = traitData.AuracurseBonusValue2;
            }
            if ((UnityEngine.Object) traitData.AuracurseBonus3 != (UnityEngine.Object) null)
            {
              if (auraCurseModifiers.ContainsKey(traitData.AuracurseBonus3.Id))
                auraCurseModifiers[traitData.AuracurseBonus3.Id] += traitData.AuracurseBonusValue3;
              else
                auraCurseModifiers[traitData.AuracurseBonus3.Id] = traitData.AuracurseBonusValue3;
            }
          }
        }
      }
    }
    if (AtOManager.Instance.IsChallengeTraitActive("icydeluge"))
    {
      if (auraCurseModifiers.ContainsKey("wet"))
        ++auraCurseModifiers["wet"];
      else
        auraCurseModifiers["wet"] = 1;
    }
    if (AtOManager.Instance.IsChallengeTraitActive("hemorrhage"))
    {
      if (auraCurseModifiers.ContainsKey("bleed"))
        auraCurseModifiers["bleed"] += 2;
      else
        auraCurseModifiers["bleed"] = 2;
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
    {
      if (auraCurseModifiers.Count > 0)
        this.cacheGetTraitAuraCurseModifiers = auraCurseModifiers;
      else
        this.cacheGetTraitAuraCurseModifiers.Add("", 0);
    }
    return auraCurseModifiers;
  }

  public int GetItemDamageFlatModifiers(Enums.DamageType DamageType)
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetItemDamageFlatModifiers.ContainsKey(DamageType))
      return this.cacheGetItemDamageFlatModifiers[DamageType];
    int damageFlatModifiers = 0;
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null)
        {
          if (itemDataBySlot.DamageFlatBonus == DamageType || itemDataBySlot.DamageFlatBonus == Enums.DamageType.All)
            damageFlatModifiers += itemDataBySlot.DamageFlatBonusValue;
          if (itemDataBySlot.DamageFlatBonus2 == DamageType || itemDataBySlot.DamageFlatBonus2 == Enums.DamageType.All)
            damageFlatModifiers += itemDataBySlot.DamageFlatBonusValue2;
          if (itemDataBySlot.DamageFlatBonus3 == DamageType || itemDataBySlot.DamageFlatBonus3 == Enums.DamageType.All)
            damageFlatModifiers += itemDataBySlot.DamageFlatBonusValue3;
        }
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
      this.cacheGetItemDamageFlatModifiers.Add(DamageType, damageFlatModifiers);
    return damageFlatModifiers;
  }

  public Dictionary<string, int> GetItemDamageDoneDictionary(Enums.DamageType DamageType)
  {
    Dictionary<string, int> damageDoneDictionary = new Dictionary<string, int>();
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        int num = 0;
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null)
        {
          if (itemDataBySlot.DamageFlatBonus == DamageType || itemDataBySlot.DamageFlatBonus == Enums.DamageType.All)
            num += itemDataBySlot.DamageFlatBonusValue;
          if (itemDataBySlot.DamageFlatBonus2 == DamageType || itemDataBySlot.DamageFlatBonus2 == Enums.DamageType.All)
            num += itemDataBySlot.DamageFlatBonusValue2;
          if (itemDataBySlot.DamageFlatBonus3 == DamageType || itemDataBySlot.DamageFlatBonus3 == Enums.DamageType.All)
            num += itemDataBySlot.DamageFlatBonusValue3;
          if (num != 0)
          {
            StringBuilder stringBuilder = new StringBuilder();
            CardData cardDataBySlot = this.GetCardDataBySlot(slot);
            if ((UnityEngine.Object) cardDataBySlot != (UnityEngine.Object) null)
            {
              stringBuilder.Append(cardDataBySlot.CardName);
              stringBuilder.Append("_");
              stringBuilder.Append((object) cardDataBySlot.CardType);
              damageDoneDictionary.Add(stringBuilder.ToString(), num);
            }
          }
        }
      }
    }
    return damageDoneDictionary;
  }

  public float GetItemDamagePercentModifiers(Enums.DamageType DamageType)
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetItemDamagePercentModifiers.ContainsKey(DamageType))
      return this.cacheGetItemDamagePercentModifiers[DamageType];
    float percentModifiers = 0.0f;
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null)
        {
          if (itemDataBySlot.DamagePercentBonus == DamageType || itemDataBySlot.DamagePercentBonus == Enums.DamageType.All)
            percentModifiers += itemDataBySlot.DamagePercentBonusValue;
          if (itemDataBySlot.DamagePercentBonus2 == DamageType || itemDataBySlot.DamagePercentBonus2 == Enums.DamageType.All)
            percentModifiers += itemDataBySlot.DamagePercentBonusValue2;
          if (itemDataBySlot.DamagePercentBonus3 == DamageType || itemDataBySlot.DamagePercentBonus3 == Enums.DamageType.All)
            percentModifiers += itemDataBySlot.DamagePercentBonusValue3;
        }
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
      this.cacheGetItemDamagePercentModifiers.Add(DamageType, percentModifiers);
    return percentModifiers;
  }

  public Dictionary<string, int> GetItemDamageDonePercentDictionary(Enums.DamageType DamageType)
  {
    Dictionary<string, int> percentDictionary = new Dictionary<string, int>();
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        float num = 0.0f;
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null)
        {
          if (itemDataBySlot.DamagePercentBonus == DamageType || itemDataBySlot.DamagePercentBonus == Enums.DamageType.All)
            num += itemDataBySlot.DamagePercentBonusValue;
          if (itemDataBySlot.DamagePercentBonus2 == DamageType || itemDataBySlot.DamagePercentBonus2 == Enums.DamageType.All)
            num += itemDataBySlot.DamagePercentBonusValue2;
          if (itemDataBySlot.DamagePercentBonus2 == DamageType || itemDataBySlot.DamagePercentBonus3 == Enums.DamageType.All)
            num += itemDataBySlot.DamagePercentBonusValue3;
          if ((double) num != 0.0)
          {
            StringBuilder stringBuilder = new StringBuilder();
            CardData cardDataBySlot = this.GetCardDataBySlot(slot);
            stringBuilder.Append(cardDataBySlot.CardName);
            stringBuilder.Append("_");
            stringBuilder.Append((object) cardDataBySlot.CardType);
            percentDictionary.Add(stringBuilder.ToString(), (int) num);
          }
        }
      }
    }
    return percentDictionary;
  }

  public int GetItemResistModifiers(Enums.DamageType type)
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetItemResistModifiers.ContainsKey(type))
      return this.cacheGetItemResistModifiers[type];
    int itemResistModifiers = 0;
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null)
        {
          if (itemDataBySlot.ResistModified1 == Enums.DamageType.All || itemDataBySlot.ResistModified1 == type)
            itemResistModifiers += itemDataBySlot.ResistModifiedValue1;
          if (itemDataBySlot.ResistModified2 == Enums.DamageType.All || itemDataBySlot.ResistModified2 == type)
            itemResistModifiers += itemDataBySlot.ResistModifiedValue2;
          if (itemDataBySlot.ResistModified3 == Enums.DamageType.All || itemDataBySlot.ResistModified3 == type)
            itemResistModifiers += itemDataBySlot.ResistModifiedValue3;
        }
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
      this.cacheGetItemResistModifiers.Add(type, itemResistModifiers);
    return itemResistModifiers;
  }

  public Dictionary<string, int> GetItemResistModifiersDictionary(Enums.DamageType type)
  {
    Dictionary<string, int> modifiersDictionary = new Dictionary<string, int>();
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null)
        {
          int num = 0;
          if (itemDataBySlot.ResistModified1 == Enums.DamageType.All || itemDataBySlot.ResistModified1 == type)
            num += itemDataBySlot.ResistModifiedValue1;
          if (itemDataBySlot.ResistModified2 == Enums.DamageType.All || itemDataBySlot.ResistModified2 == type)
            num += itemDataBySlot.ResistModifiedValue2;
          if (itemDataBySlot.ResistModified3 == Enums.DamageType.All || itemDataBySlot.ResistModified3 == type)
            num += itemDataBySlot.ResistModifiedValue3;
          if (num != 0)
          {
            StringBuilder stringBuilder = new StringBuilder();
            CardData cardDataBySlot = this.GetCardDataBySlot(slot);
            if ((UnityEngine.Object) cardDataBySlot != (UnityEngine.Object) null)
            {
              stringBuilder.Append(cardDataBySlot.CardName);
              stringBuilder.Append("_");
              stringBuilder.Append((object) cardDataBySlot.CardType);
            }
            if (!modifiersDictionary.ContainsKey(stringBuilder.ToString()))
              modifiersDictionary.Add(stringBuilder.ToString(), num);
            else
              modifiersDictionary[stringBuilder.ToString()] += num;
          }
        }
      }
    }
    return modifiersDictionary;
  }

  public int GetItemStatModifiers(Enums.CharacterStat stat)
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetItemStatModifiers.ContainsKey(stat))
      return this.cacheGetItemStatModifiers[stat];
    int itemStatModifiers = 0;
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      if (!((UnityEngine.Object) this.heroData != (UnityEngine.Object) null) || slot != 4)
      {
        ItemData itemDataBySlot = this.GetItemDataBySlot(slot);
        if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null)
        {
          if (itemDataBySlot.CharacterStatModified == stat)
            itemStatModifiers += itemDataBySlot.CharacterStatModifiedValue;
          if (itemDataBySlot.CharacterStatModified2 == stat)
            itemStatModifiers += itemDataBySlot.CharacterStatModifiedValue2;
          if (itemDataBySlot.CharacterStatModified3 == stat)
            itemStatModifiers += itemDataBySlot.CharacterStatModifiedValue3;
        }
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
    {
      if (!this.cacheGetItemStatModifiers.ContainsKey(stat))
        this.cacheGetItemStatModifiers.Add(stat, itemStatModifiers);
      else
        this.cacheGetItemStatModifiers[stat] = itemStatModifiers;
    }
    return itemStatModifiers;
  }

  public int GetAuraStatModifiers(int source, Enums.CharacterStat stat)
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.GetAuraListString());
      stringBuilder.Append("-");
      stringBuilder.Append(source);
      stringBuilder.Append("-");
      stringBuilder.Append((object) stat);
      string key = Functions.Md5Sum(stringBuilder.ToString());
      if (this.cacheGetAuraStatModifiers.ContainsKey(key))
        return this.cacheGetAuraStatModifiers[key];
    }
    int auraStatModifiers = source;
    bool flag = false;
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if (this.auraList[index] != null)
      {
        AuraCurseData acData = this.auraList[index].ACData;
        if ((UnityEngine.Object) acData != (UnityEngine.Object) null && acData.CharacterStatModified == stat)
        {
          if (acData.CharacterStatAbsolute)
          {
            if (acData.Id == "shackle" && stat == Enums.CharacterStat.Speed)
            {
              flag = true;
            }
            else
            {
              auraStatModifiers = acData.CharacterStatAbsoluteValue + acData.CharacterStatAbsoluteValuePerStack * this.auraList[index].AuraCharges;
              break;
            }
          }
          else
          {
            int num1 = auraStatModifiers + acData.CharacterStatModifiedValue;
            float num2 = 1f / (float) acData.CharacterStatChargesMultiplierNeededForOne;
            int num3 = acData.CharacterStatModifiedValuePerStack >= 0 ? Mathf.FloorToInt(num2 * (float) this.auraList[index].AuraCharges * (float) acData.CharacterStatModifiedValuePerStack) : -1 * Mathf.FloorToInt(Mathf.Abs(num2 * (float) this.auraList[index].AuraCharges * (float) acData.CharacterStatModifiedValuePerStack));
            auraStatModifiers = num1 + num3;
          }
        }
      }
    }
    if (auraStatModifiers > 0 & flag)
      auraStatModifiers = 0;
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.GetAuraListString());
      stringBuilder.Append("-");
      stringBuilder.Append(source);
      stringBuilder.Append("-");
      stringBuilder.Append((object) stat);
      string key = Functions.Md5Sum(stringBuilder.ToString());
      if (!this.cacheGetAuraStatModifiers.ContainsKey(key))
        this.cacheGetAuraStatModifiers.Add(key, auraStatModifiers);
      else
        this.cacheGetAuraStatModifiers[key] = auraStatModifiers;
    }
    return auraStatModifiers;
  }

  public Dictionary<string, int> GetAuraResistModifiersDictionary(Enums.DamageType damageType)
  {
    Dictionary<string, int> modifiersDictionary = new Dictionary<string, int>();
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      AuraCurseData acData = this.auraList[index].ACData;
      if ((UnityEngine.Object) acData != (UnityEngine.Object) null)
      {
        int auraCharges = this.auraList[index].AuraCharges;
        int num = 0;
        if (acData.IncreasedDamageReceivedType == damageType || acData.IncreasedDamageReceivedType == Enums.DamageType.All)
          num -= acData.IncreasedPercentDamageReceivedPerStack * auraCharges;
        if (acData.IncreasedDamageReceivedType2 == damageType)
          num -= acData.IncreasedPercentDamageReceivedPerStack2 * auraCharges;
        if (acData.ResistModified == Enums.DamageType.All)
          num = num + Functions.FuncRoundToInt(acData.ResistModifiedPercentagePerStack * (float) auraCharges) + acData.ResistModifiedValue;
        if (acData.ResistModified == damageType)
          num = num + Functions.FuncRoundToInt(acData.ResistModifiedPercentagePerStack * (float) auraCharges) + acData.ResistModifiedValue;
        if (acData.ResistModified2 == damageType)
          num = num + Functions.FuncRoundToInt(acData.ResistModifiedPercentagePerStack2 * (float) auraCharges) + acData.ResistModifiedValue2;
        if (acData.ResistModified3 == damageType)
          num = num + Functions.FuncRoundToInt(acData.ResistModifiedPercentagePerStack3 * (float) auraCharges) + acData.ResistModifiedValue3;
        if (num != 0)
          modifiersDictionary.Add(acData.Id, num);
      }
    }
    return modifiersDictionary;
  }

  public int GetAuraResistModifiers(
    Enums.DamageType damageType,
    string acId = "",
    bool countChargesConsumedPre = false,
    bool countChargesConsumedPost = false)
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.GetAuraListString());
      stringBuilder.Append("-");
      stringBuilder.Append((object) damageType);
      stringBuilder.Append("-");
      stringBuilder.Append(acId);
      stringBuilder.Append("-");
      stringBuilder.Append(countChargesConsumedPre);
      stringBuilder.Append("-");
      stringBuilder.Append(countChargesConsumedPost);
      string key = Functions.Md5Sum(stringBuilder.ToString());
      if (this.cacheGetAuraResistModifiers.ContainsKey(key))
        return this.cacheGetAuraResistModifiers[key];
    }
    int auraResistModifiers = 0;
    bool flag = false;
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      AuraCurseData acData = this.auraList[index].ACData;
      if ((UnityEngine.Object) acData != (UnityEngine.Object) null)
      {
        if (acData.Id == acId)
          flag = true;
        int auraCharges = this.auraList[index].AuraCharges;
        if (auraCharges > 0)
        {
          if (!flag & countChargesConsumedPre)
          {
            if (!acData.ConsumedAtTurnBegin || auraCharges > acData.AuraConsumed)
              auraCharges -= acData.AuraConsumed;
            else
              continue;
          }
          if (!flag & countChargesConsumedPost)
          {
            if (!acData.ConsumedAtTurn || auraCharges > acData.AuraConsumed)
              auraCharges -= acData.AuraConsumed;
            else
              continue;
          }
          if (acData.IncreasedDamageReceivedType == damageType || acData.IncreasedDamageReceivedType == Enums.DamageType.All)
            auraResistModifiers -= acData.IncreasedPercentDamageReceivedPerStack * auraCharges;
          if (acData.IncreasedDamageReceivedType2 == damageType)
            auraResistModifiers -= acData.IncreasedPercentDamageReceivedPerStack2 * auraCharges;
          if (acData.ResistModified == Enums.DamageType.All)
            auraResistModifiers = auraResistModifiers + Functions.FuncRoundToInt(acData.ResistModifiedPercentagePerStack * (float) auraCharges) + acData.ResistModifiedValue;
          if (acData.ResistModified == damageType)
            auraResistModifiers = auraResistModifiers + Functions.FuncRoundToInt(acData.ResistModifiedPercentagePerStack * (float) auraCharges) + acData.ResistModifiedValue;
          if (acData.ResistModified2 == damageType)
            auraResistModifiers = auraResistModifiers + Functions.FuncRoundToInt(acData.ResistModifiedPercentagePerStack2 * (float) auraCharges) + acData.ResistModifiedValue2;
          if (acData.ResistModified3 == damageType)
            auraResistModifiers = auraResistModifiers + Functions.FuncRoundToInt(acData.ResistModifiedPercentagePerStack3 * (float) auraCharges) + acData.ResistModifiedValue3;
        }
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.GetAuraListString());
      stringBuilder.Append("-");
      stringBuilder.Append((object) damageType);
      stringBuilder.Append("-");
      stringBuilder.Append(acId);
      stringBuilder.Append("-");
      stringBuilder.Append(countChargesConsumedPre);
      stringBuilder.Append("-");
      stringBuilder.Append(countChargesConsumedPost);
      this.cacheGetAuraResistModifiers.Add(Functions.Md5Sum(stringBuilder.ToString()), auraResistModifiers);
    }
    return auraResistModifiers;
  }

  public int BonusResists(
    Enums.DamageType damageType,
    string acId = "",
    bool countChargesConsumedPre = false,
    bool countChargesConsumedPost = false)
  {
    int num = 0;
    switch (damageType)
    {
      case Enums.DamageType.Slashing:
        if (this.immuneSlashing)
        {
          num = 1000;
          break;
        }
        num += this.resistSlashing;
        if (!this.isHero && AtOManager.Instance.IsChallengeTraitActive("reinforcedmonsters"))
        {
          num += 10;
          break;
        }
        break;
      case Enums.DamageType.Blunt:
        if (this.immuneBlunt)
        {
          num = 1000;
          break;
        }
        num += this.resistBlunt;
        if (!this.isHero && AtOManager.Instance.IsChallengeTraitActive("reinforcedmonsters"))
        {
          num += 10;
          break;
        }
        break;
      case Enums.DamageType.Piercing:
        if (this.immunePiercing)
        {
          num = 1000;
          break;
        }
        num += this.resistPiercing;
        if (!this.isHero && AtOManager.Instance.IsChallengeTraitActive("reinforcedmonsters"))
        {
          num += 10;
          break;
        }
        break;
      case Enums.DamageType.Fire:
        if (this.immuneFire)
        {
          num = 1000;
          break;
        }
        num += this.resistFire;
        if (!this.isHero && AtOManager.Instance.IsChallengeTraitActive("elementalmonsters"))
        {
          num += 10;
          break;
        }
        break;
      case Enums.DamageType.Cold:
        if (this.immuneCold)
        {
          num = 1000;
          break;
        }
        num += this.resistCold;
        if (!this.isHero && AtOManager.Instance.IsChallengeTraitActive("elementalmonsters"))
        {
          num += 10;
          break;
        }
        break;
      case Enums.DamageType.Lightning:
        if (this.immuneLightning)
        {
          num = 1000;
          break;
        }
        num += this.resistLightning;
        if (!this.isHero && AtOManager.Instance.IsChallengeTraitActive("elementalmonsters"))
        {
          num += 10;
          break;
        }
        break;
      case Enums.DamageType.Mind:
        if (this.immuneMind)
        {
          num = 1000;
          break;
        }
        num += this.resistMind;
        if (!this.isHero && AtOManager.Instance.IsChallengeTraitActive("spiritualmonsters"))
        {
          num += 10;
          break;
        }
        break;
      case Enums.DamageType.Holy:
        if (this.immuneHoly)
        {
          num = 1000;
          break;
        }
        num += this.resistHoly;
        if (!this.isHero && AtOManager.Instance.IsChallengeTraitActive("spiritualmonsters"))
        {
          num += 10;
          break;
        }
        break;
      case Enums.DamageType.Shadow:
        if (this.immuneShadow)
        {
          num = 1000;
          break;
        }
        num += this.resistShadow;
        if (!this.isHero && AtOManager.Instance.IsChallengeTraitActive("spiritualmonsters"))
        {
          num += 10;
          break;
        }
        break;
    }
    if (num < 1000)
    {
      if (!this.isHero)
      {
        if (MadnessManager.Instance.IsMadnessTraitActive("resistantmonsters") || AtOManager.Instance.IsChallengeTraitActive("resistantmonsters"))
          num += 10;
        if (AtOManager.Instance.IsChallengeTraitActive("vulnerablemonsters"))
          num -= 15;
      }
      num = num + this.GetItemResistModifiers(damageType) + this.GetAuraResistModifiers(damageType, acId, countChargesConsumedPre, countChargesConsumedPost);
    }
    return Mathf.Clamp(num, -95, 95);
  }

  public int IncreasedCursedDamagePerStack(Enums.DamageType damageType)
  {
    string str = "increasedCurseDamagePerStackWith_" + Enum.GetName(typeof (Enums.DamageType), (object) damageType);
    int num1 = 0;
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if (this.auraList[index] != null)
      {
        Aura aura = this.auraList[index];
        if ((UnityEngine.Object) aura.ACData != (UnityEngine.Object) null)
        {
          if (aura.ACData.IncreasedDamageReceivedType == damageType || aura.ACData.IncreasedDamageReceivedType == Enums.DamageType.All)
          {
            float num2 = 1f / (float) aura.ACData.IncreasedDirectDamageChargesMultiplierNeededForOne;
            int num3 = aura.ACData.IncreasedDirectDamageReceivedPerStack >= 0 ? Mathf.FloorToInt(num2 * (float) aura.AuraCharges * (float) aura.ACData.IncreasedDirectDamageReceivedPerStack) : -1 * Mathf.FloorToInt(Mathf.Abs(num2 * (float) aura.AuraCharges * (float) aura.ACData.IncreasedDirectDamageReceivedPerStack));
            num1 += num3;
          }
          if (aura.ACData.IncreasedDamageReceivedType2 == damageType)
          {
            float num4 = 1f / (float) aura.ACData.IncreasedDirectDamageChargesMultiplierNeededForOne2;
            int num5 = aura.ACData.IncreasedDirectDamageReceivedPerStack2 >= 0 ? Mathf.FloorToInt(num4 * (float) aura.AuraCharges * (float) aura.ACData.IncreasedDirectDamageReceivedPerStack2) : -1 * Mathf.FloorToInt(Mathf.Abs(num4 * (float) aura.AuraCharges * (float) aura.ACData.IncreasedDirectDamageReceivedPerStack2));
            num1 += num5;
          }
        }
      }
    }
    int ngPlus = AtOManager.Instance.GetNgPlus();
    if (!this.isHero)
    {
      switch (ngPlus)
      {
        case 2:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            --num1;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            --num1;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            --num1;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            --num1;
            break;
          }
          break;
        case 3:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            --num1;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            --num1;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num1 -= 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num1 -= 2;
            break;
          }
          break;
        case 4:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            --num1;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            num1 -= 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num1 -= 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num1 -= 2;
            break;
          }
          break;
        case 5:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            num1 -= 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            num1 -= 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num1 -= 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num1 -= 2;
            break;
          }
          break;
        case 6:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            num1 -= 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            num1 -= 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num1 -= 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num1 -= 3;
            break;
          }
          break;
        case 7:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            num1 -= 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            num1 -= 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num1 -= 3;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num1 -= 3;
            break;
          }
          break;
        case 8:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            num1 -= 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            num1 -= 3;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num1 -= 3;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num1 -= 3;
            break;
          }
          break;
      }
      if (AtOManager.Instance.IsChallengeTraitActive("ironcladmonsters"))
        num1 -= 3;
    }
    return num1;
  }

  public Dictionary<string, int> GetAuraDamageTakenDictionary(Enums.DamageType damageType)
  {
    Dictionary<string, int> damageTakenDictionary = new Dictionary<string, int>();
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      Aura aura = this.auraList[index];
      int num1 = 0;
      if (aura.ACData.IncreasedDamageReceivedType == damageType || aura.ACData.IncreasedDamageReceivedType == Enums.DamageType.All)
      {
        float num2 = 1f / (float) aura.ACData.IncreasedDirectDamageChargesMultiplierNeededForOne;
        int num3 = aura.ACData.IncreasedDirectDamageReceivedPerStack >= 0 ? Mathf.FloorToInt(num2 * (float) aura.AuraCharges * (float) aura.ACData.IncreasedDirectDamageReceivedPerStack) : -1 * Mathf.FloorToInt(Mathf.Abs(num2 * (float) aura.AuraCharges * (float) aura.ACData.IncreasedDirectDamageReceivedPerStack));
        num1 += num3;
      }
      if (aura.ACData.IncreasedDamageReceivedType2 == damageType)
      {
        float num4 = 1f / (float) aura.ACData.IncreasedDirectDamageChargesMultiplierNeededForOne2;
        int num5 = aura.ACData.IncreasedDirectDamageReceivedPerStack2 >= 0 ? Mathf.FloorToInt(num4 * (float) aura.AuraCharges * (float) aura.ACData.IncreasedDirectDamageReceivedPerStack2) : -1 * Mathf.FloorToInt(Mathf.Abs(num4 * (float) aura.AuraCharges * (float) aura.ACData.IncreasedDirectDamageReceivedPerStack2));
        num1 += num5;
      }
      if (num1 != 0)
        damageTakenDictionary.Add(aura.ACData.Id, num1);
    }
    return damageTakenDictionary;
  }

  public float[] DamageBonus(Enums.DamageType DT, int energyCost = 0)
  {
    string str1 = "damageBonusFlat_" + Enum.GetName(typeof (Enums.DamageType), (object) DT);
    string str2 = "damageBonusPercent_" + Enum.GetName(typeof (Enums.DamageType), (object) DT);
    int num1 = 0;
    int num2 = 0;
    float[] numArray = new float[2];
    if (false)
    {
      numArray[0] = (float) num1;
      numArray[1] = (float) num2;
      return numArray;
    }
    int num3 = 0;
    int num4 = 0;
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if (index < this.auraList.Count && this.auraList[index] != null)
      {
        AuraCurseData acData = this.auraList[index].ACData;
        if ((UnityEngine.Object) acData != (UnityEngine.Object) null)
        {
          int auraCharges1 = this.auraList[index].AuraCharges;
          if (acData.AuraDamageType == DT || acData.AuraDamageType == Enums.DamageType.All)
          {
            if ((UnityEngine.Object) acData.AuraDamageChargesBasedOnACCharges != (UnityEngine.Object) null)
              auraCharges1 = this.GetAuraCharges(acData.AuraDamageChargesBasedOnACCharges.Id);
            if (acData.AuraDamageIncreasedTotal != 0)
              num3 += acData.AuraDamageIncreasedTotal;
            num3 += Functions.FuncRoundToInt(acData.AuraDamageIncreasedPerStack * (float) auraCharges1);
            if (acData.AuraDamageIncreasedPercent != 0)
              num4 += acData.AuraDamageIncreasedPercent;
            if ((double) acData.AuraDamageIncreasedPercentPerStack != 0.0)
            {
              float num5 = acData.AuraDamageIncreasedPercentPerStack + acData.AuraDamageIncreasedPercentPerStackPerEnergy * (float) energyCost;
              num4 += Functions.FuncRoundToInt(num5 * (float) auraCharges1);
            }
          }
          int auraCharges2 = this.auraList[index].AuraCharges;
          if (acData.AuraDamageType2 == DT || acData.AuraDamageType2 == Enums.DamageType.All)
          {
            if (acData.AuraDamageIncreasedTotal2 != 0)
              num3 += acData.AuraDamageIncreasedTotal2;
            num3 += Functions.FuncRoundToInt(acData.AuraDamageIncreasedPerStack2 * (float) auraCharges2);
            if (acData.AuraDamageIncreasedPercent2 != 0)
              num4 += acData.AuraDamageIncreasedPercent2;
            if ((double) acData.AuraDamageIncreasedPercentPerStack2 != 0.0)
            {
              float num6 = acData.AuraDamageIncreasedPercentPerStack2 + acData.AuraDamageIncreasedPercentPerStackPerEnergy2 * (float) energyCost;
              num4 += Functions.FuncRoundToInt(num6 * (float) auraCharges2);
            }
          }
          if (acData.AuraDamageType3 == DT || acData.AuraDamageType3 == Enums.DamageType.All)
          {
            if (acData.AuraDamageIncreasedTotal3 != 0)
              num3 += acData.AuraDamageIncreasedTotal3;
            num3 += Functions.FuncRoundToInt(acData.AuraDamageIncreasedPerStack3 * (float) auraCharges2);
            if (acData.AuraDamageIncreasedPercent3 != 0)
              num4 += acData.AuraDamageIncreasedPercent3;
            if ((double) acData.AuraDamageIncreasedPercentPerStack3 != 0.0)
            {
              float num7 = acData.AuraDamageIncreasedPercentPerStack3 + acData.AuraDamageIncreasedPercentPerStackPerEnergy3 * (float) energyCost;
              num4 += Functions.FuncRoundToInt(num7 * (float) auraCharges2);
            }
          }
          if (acData.AuraDamageType4 == DT || acData.AuraDamageType4 == Enums.DamageType.All)
          {
            if (acData.AuraDamageIncreasedTotal4 != 0)
              num3 += acData.AuraDamageIncreasedTotal4;
            num3 += Functions.FuncRoundToInt(acData.AuraDamageIncreasedPerStack4 * (float) auraCharges2);
            if (acData.AuraDamageIncreasedPercent4 != 0)
              num4 += acData.AuraDamageIncreasedPercent4;
            if ((double) acData.AuraDamageIncreasedPercentPerStack4 != 0.0)
            {
              float num8 = acData.AuraDamageIncreasedPercentPerStack4 + acData.AuraDamageIncreasedPercentPerStackPerEnergy4 * (float) energyCost;
              num4 += Functions.FuncRoundToInt(num8 * (float) auraCharges2);
            }
          }
        }
      }
    }
    if (num4 < -50)
      num4 = -50;
    numArray[0] = (float) num3;
    numArray[1] = (float) num4;
    return numArray;
  }

  public Dictionary<string, int> GetAuraDamageDoneDictionary(Enums.DamageType DamageType)
  {
    Dictionary<string, int> damageDoneDictionary = new Dictionary<string, int>();
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      AuraCurseData acData = this.auraList[index].ACData;
      int auraCharges = this.auraList[index].AuraCharges;
      int num = 0;
      if (acData.AuraDamageType == DamageType || acData.AuraDamageType == Enums.DamageType.All)
      {
        if (acData.AuraDamageIncreasedTotal != 0)
          num += acData.AuraDamageIncreasedTotal;
        num += Functions.FuncRoundToInt(acData.AuraDamageIncreasedPerStack * (float) auraCharges);
      }
      if (acData.AuraDamageType2 == DamageType || acData.AuraDamageType2 == Enums.DamageType.All)
      {
        if (acData.AuraDamageIncreasedTotal2 != 0)
          num += acData.AuraDamageIncreasedTotal2;
        num += Functions.FuncRoundToInt(acData.AuraDamageIncreasedPerStack2 * (float) auraCharges);
      }
      if (acData.AuraDamageType3 == DamageType || acData.AuraDamageType3 == Enums.DamageType.All)
      {
        if (acData.AuraDamageIncreasedTotal3 != 0)
          num += acData.AuraDamageIncreasedTotal3;
        num += Functions.FuncRoundToInt(acData.AuraDamageIncreasedPerStack3 * (float) auraCharges);
      }
      if (acData.AuraDamageType4 == DamageType || acData.AuraDamageType4 == Enums.DamageType.All)
      {
        if (acData.AuraDamageIncreasedTotal4 != 0)
          num += acData.AuraDamageIncreasedTotal4;
        num += Functions.FuncRoundToInt(acData.AuraDamageIncreasedPerStack4 * (float) auraCharges);
      }
      if (num != 0)
        damageDoneDictionary.Add(acData.Id, num);
    }
    return damageDoneDictionary;
  }

  public Dictionary<string, int> GetAuraDamageDonePercentDictionary(Enums.DamageType DamageType)
  {
    Dictionary<string, int> percentDictionary = new Dictionary<string, int>();
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if (this.auraList[index] != null)
      {
        AuraCurseData acData = this.auraList[index].ACData;
        if ((UnityEngine.Object) acData != (UnityEngine.Object) null)
        {
          int auraCharges1 = this.auraList[index].AuraCharges;
          int num = 0;
          if (acData.AuraDamageType == DamageType || acData.AuraDamageType == Enums.DamageType.All)
          {
            if ((UnityEngine.Object) acData.AuraDamageChargesBasedOnACCharges != (UnityEngine.Object) null)
              auraCharges1 = this.GetAuraCharges(acData.AuraDamageChargesBasedOnACCharges.Id);
            if (acData.AuraDamageIncreasedPercent != 0)
              num += acData.AuraDamageIncreasedPercent;
            if ((double) acData.AuraDamageIncreasedPercentPerStack != 0.0)
              num += Functions.FuncRoundToInt(acData.AuraDamageIncreasedPercentPerStack * (float) auraCharges1);
          }
          int auraCharges2 = this.auraList[index].AuraCharges;
          if (acData.AuraDamageType2 == DamageType || acData.AuraDamageType2 == Enums.DamageType.All)
          {
            if (acData.AuraDamageIncreasedPercent2 != 0)
              num += acData.AuraDamageIncreasedPercent2;
            if ((double) acData.AuraDamageIncreasedPercentPerStack2 != 0.0)
              num += Functions.FuncRoundToInt(acData.AuraDamageIncreasedPercentPerStack2 * (float) auraCharges2);
          }
          if (acData.AuraDamageType3 == DamageType || acData.AuraDamageType3 == Enums.DamageType.All)
          {
            if (acData.AuraDamageIncreasedPercent3 != 0)
              num += acData.AuraDamageIncreasedPercent3;
            if ((double) acData.AuraDamageIncreasedPercentPerStack3 != 0.0)
              num += Functions.FuncRoundToInt(acData.AuraDamageIncreasedPercentPerStack3 * (float) auraCharges2);
          }
          if (acData.AuraDamageType4 == DamageType || acData.AuraDamageType4 == Enums.DamageType.All)
          {
            if (acData.AuraDamageIncreasedPercent4 != 0)
              num += acData.AuraDamageIncreasedPercent4;
            if ((double) acData.AuraDamageIncreasedPercentPerStack4 != 0.0)
              num += Functions.FuncRoundToInt(acData.AuraDamageIncreasedPercentPerStack4 * (float) auraCharges2);
          }
          if (num != 0)
            percentDictionary.Add(acData.Id, num);
        }
      }
    }
    return percentDictionary;
  }

  private int GetTraitDamageFlatModifiers(Enums.DamageType DamageType)
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetTraitDamageFlatModifiers.ContainsKey(DamageType))
      return this.cacheGetTraitDamageFlatModifiers[DamageType];
    int damageFlatModifiers = 0;
    if (this.traits != null)
    {
      for (int index = 0; index < this.traits.Length; ++index)
      {
        if (this.traits[index] != null)
        {
          TraitData traitData = Globals.Instance.GetTraitData(this.traits[index]);
          if ((UnityEngine.Object) traitData != (UnityEngine.Object) null)
          {
            if (traitData.DamageBonusFlat == DamageType || traitData.DamageBonusFlat == Enums.DamageType.All)
              damageFlatModifiers += traitData.DamageBonusFlatValue;
            if (traitData.DamageBonusFlat2 == DamageType || traitData.DamageBonusFlat2 == Enums.DamageType.All)
              damageFlatModifiers += traitData.DamageBonusFlatValue2;
            if (traitData.DamageBonusFlat3 == DamageType || traitData.DamageBonusFlat3 == Enums.DamageType.All)
              damageFlatModifiers += traitData.DamageBonusFlatValue3;
          }
        }
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
    {
      if (!this.cacheGetTraitDamageFlatModifiers.ContainsKey(DamageType))
        this.cacheGetTraitDamageFlatModifiers.Add(DamageType, damageFlatModifiers);
      else
        this.cacheGetTraitDamageFlatModifiers[DamageType] = damageFlatModifiers;
    }
    return damageFlatModifiers;
  }

  public float GetTraitDamagePercentModifiers(Enums.DamageType DamageType)
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetTraitDamagePercentModifiers.ContainsKey(DamageType))
      return this.cacheGetTraitDamagePercentModifiers[DamageType];
    bool flag = true;
    float percentModifiers = 0.0f;
    if (this.traits != null)
    {
      for (int index = 0; index < this.traits.Length; ++index)
      {
        if (this.traits[index] != null)
        {
          TraitData traitData = Globals.Instance.GetTraitData(this.traits[index]);
          if ((UnityEngine.Object) traitData != (UnityEngine.Object) null)
          {
            if (traitData.DamageBonusPercent == DamageType || traitData.DamageBonusPercent == Enums.DamageType.All)
              percentModifiers += traitData.DamageBonusPercentValue;
            if (traitData.DamageBonusPercent2 == DamageType || traitData.DamageBonusPercent2 == Enums.DamageType.All)
              percentModifiers += traitData.DamageBonusPercentValue2;
            if (traitData.DamageBonusPercent3 == DamageType || traitData.DamageBonusPercent3 == Enums.DamageType.All)
              percentModifiers += traitData.DamageBonusPercentValue3;
            if (traitData.Id == "bigbadwolf")
            {
              flag = false;
              if (this.hpCurrent > 100)
              {
                float num = (float) (this.hpCurrent - 100);
                if ((double) num > 0.0)
                  percentModifiers += (float) Functions.FuncRoundToInt(num / 3f);
              }
            }
            else if (traitData.Id == "hatred")
            {
              flag = false;
              if ((double) this.GetHpPercent() < 50.0)
              {
                float num1 = 50f - this.GetHpPercent();
                float num2 = 30f;
                float num3 = 4f;
                if (this.HaveTrait("unrelentingresentment"))
                {
                  num2 = 60f;
                  num3 = 6f;
                }
                percentModifiers += num2 + num1 * num3;
              }
            }
          }
        }
      }
    }
    if (flag && (bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
    {
      if (!this.cacheGetTraitDamagePercentModifiers.ContainsKey(DamageType))
        this.cacheGetTraitDamagePercentModifiers.Add(DamageType, percentModifiers);
      else
        this.cacheGetTraitDamagePercentModifiers[DamageType] = percentModifiers;
    }
    return percentModifiers;
  }

  public int DamageWithCharacterBonus(
    int value,
    Enums.DamageType DT,
    Enums.CardClass CC,
    int energyCost = 0)
  {
    if (this.isHero && (CC == Enums.CardClass.Monster || CC == Enums.CardClass.Injury || CC == Enums.CardClass.Boon))
      return value;
    int num1 = value + 0;
    float[] numArray = this.DamageBonus(DT, energyCost);
    float num2 = (float) (num1 + this.GetTraitDamageFlatModifiers(DT) + this.GetItemDamageFlatModifiers(DT)) + numArray[0];
    if ((UnityEngine.Object) this.heroData != (UnityEngine.Object) null)
      num2 += (float) PlayerManager.Instance.GetPerkDamageBonus(this.heroData.HeroSubClass.Id, DT);
    int ngPlus = AtOManager.Instance.GetNgPlus();
    if (!this.isHero)
    {
      switch (ngPlus)
      {
        case 1:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            --num2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            --num2;
            break;
          }
          break;
        case 3:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            ++num2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            ++num2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            ++num2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            ++num2;
            break;
          }
          break;
        case 4:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            ++num2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            ++num2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num2 += 2f;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num2 += 2f;
            break;
          }
          break;
        case 5:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            ++num2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            num2 += 2f;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num2 += 2f;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num2 += 2f;
            break;
          }
          break;
        case 6:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            num2 += 2f;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            num2 += 2f;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num2 += 2f;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num2 += 2f;
            break;
          }
          break;
        case 7:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            num2 += 2f;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            num2 += 2f;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num2 += 2f;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num2 += 3f;
            break;
          }
          break;
        case 8:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            num2 += 2f;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            num2 += 2f;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num2 += 3f;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num2 += 3f;
            break;
          }
          break;
      }
      if (AtOManager.Instance.IsChallengeTraitActive("dangerousmonsters"))
        num2 += 3f;
    }
    float num3 = numArray[1] + this.GetTraitDamagePercentModifiers(DT) + this.GetItemDamagePercentModifiers(DT);
    int num4 = Functions.FuncRoundToInt(num2 + (float) ((double) num2 * (double) num3 * 0.0099999997764825821));
    if (!this.isHero && AtOManager.Instance.Sandbox_additionalMonsterDamage != 0)
      num4 += Functions.FuncRoundToInt((float) (num4 * AtOManager.Instance.Sandbox_additionalMonsterDamage) * 0.01f);
    return num4;
  }

  public int TotalDamageWithCharacterFlatBonus(Enums.DamageType DT)
  {
    float num1 = (float) (this.GetTraitDamageFlatModifiers(DT) + this.GetItemDamageFlatModifiers(DT)) + this.DamageBonus(DT)[0];
    if ((UnityEngine.Object) this.heroData != (UnityEngine.Object) null)
      num1 += (float) PlayerManager.Instance.GetPerkDamageBonus(this.heroData.HeroSubClass.Id, DT);
    if (!this.isHero)
    {
      int ngPlus = AtOManager.Instance.GetNgPlus();
      int num2 = 0;
      switch (ngPlus)
      {
        case 3:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            ++num2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            ++num2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            ++num2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            ++num2;
            break;
          }
          break;
        case 4:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            ++num2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            ++num2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num2 += 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num2 += 2;
            break;
          }
          break;
        case 5:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            ++num2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            num2 += 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num2 += 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num2 += 2;
            break;
          }
          break;
        case 6:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            num2 += 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            num2 += 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num2 += 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num2 += 2;
            break;
          }
          break;
        case 7:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            num2 += 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            num2 += 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num2 += 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num2 += 3;
            break;
          }
          break;
        case 8:
          if (AtOManager.Instance.GetTownTier() == 0)
          {
            num2 += 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 1)
          {
            num2 += 2;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 2)
          {
            num2 += 3;
            break;
          }
          if (AtOManager.Instance.GetTownTier() == 3)
          {
            num2 += 3;
            break;
          }
          break;
      }
      num1 += (float) num2;
    }
    return Functions.FuncRoundToInt(num1);
  }

  public float[] HealReceivedBonus(bool isIndirect = false)
  {
    float[] numArray = new float[2];
    int num1 = 0;
    int num2 = 0;
    if (this.isHero && MadnessManager.Instance.IsMadnessTraitActive("decadence"))
      num2 -= 25;
    if (this.isHero && AtOManager.Instance.IsChallengeTraitActive("decadence"))
      num2 -= 20;
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if (this.auraList[index] != null && (UnityEngine.Object) this.auraList[index].ACData != (UnityEngine.Object) null)
      {
        AuraCurseData acData = this.auraList[index].ACData;
        int auraCharges = this.auraList[index].AuraCharges;
        if (acData.HealReceivedTotal != 0)
          num1 += acData.HealReceivedTotal;
        if (acData.HealReceivedPerStack != 0)
          num1 += acData.HealReceivedPerStack * auraCharges;
        if (acData.HealReceivedPercent != 0)
          num2 += acData.HealReceivedPercent;
        if (acData.HealReceivedPercentPerStack != 0)
          num2 += acData.HealReceivedPercentPerStack * auraCharges;
        if (isIndirect && acData.Id == "zeal")
          num2 += 20 * auraCharges;
      }
    }
    numArray[0] = (float) num1;
    numArray[1] = (float) num2;
    return numArray;
  }

  public int HealReceivedFinal(int heal, bool isIndirect = false)
  {
    int num1 = heal;
    float[] numArray = this.HealReceivedBonus(isIndirect);
    double num2 = (double) num1 + (double) numArray[0] + (double) this.GetTraitHealReceivedFlatBonus() + (double) this.GetItemHealReceivedFlatBonus();
    int num3 = Functions.FuncRoundToInt((float) (num2 + num2 * (double) (numArray[1] + this.GetTraitHealReceivedPercentBonus() + this.GetItemHealReceivedPercentBonus()) * 0.0099999997764825821));
    if (num3 < 0)
      num3 = 0;
    return num3;
  }

  public float[] HealBonus(int energyCost)
  {
    int num1 = 0;
    int num2 = 0;
    float[] numArray = new float[2];
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      if (this.auraList[index] != null)
      {
        AuraCurseData acData = this.auraList[index].ACData;
        if ((UnityEngine.Object) acData != (UnityEngine.Object) null)
        {
          int auraCharges = this.auraList[index].AuraCharges;
          if (acData.HealDoneTotal != 0)
            num1 += acData.HealDoneTotal;
          if (acData.HealDonePerStack != 0)
            num1 += acData.HealDonePerStack * auraCharges;
          if (acData.HealDonePercent != 0)
            num2 += acData.HealDonePercent;
          if (acData.HealDonePercentPerStack != 0)
          {
            float num3 = (float) acData.HealDonePercentPerStack + (float) (acData.HealDonePercentPerStackPerEnergy * energyCost);
            num2 += Functions.FuncRoundToInt(num3 * (float) auraCharges);
          }
        }
      }
    }
    if (this.isHero && MadnessManager.Instance.IsMadnessTraitActive("decadence"))
      num1 -= 3;
    if (!this.isHero && AtOManager.Instance.NodeIsObeliskFinal())
      num1 -= 4;
    numArray[0] = (float) num1;
    numArray[1] = (float) num2;
    return numArray;
  }

  public Dictionary<string, int> GetAuraHealPercentDictionary()
  {
    Dictionary<string, int> percentDictionary = new Dictionary<string, int>();
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      AuraCurseData acData = this.auraList[index].ACData;
      int num = 0;
      int auraCharges = this.auraList[index].AuraCharges;
      if (acData.HealDonePercent != 0)
        num += acData.HealDonePercent;
      if (acData.HealDonePercentPerStack != 0)
        num += acData.HealDonePercentPerStack * auraCharges;
      if (num != 0)
        percentDictionary.Add(acData.Id, num);
    }
    return percentDictionary;
  }

  public Dictionary<string, int> GetAuraHealFlatDictionary()
  {
    Dictionary<string, int> healFlatDictionary = new Dictionary<string, int>();
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      AuraCurseData acData = this.auraList[index].ACData;
      int num = 0;
      int auraCharges = this.auraList[index].AuraCharges;
      if (acData.HealDoneTotal != 0)
        num += acData.HealDoneTotal;
      if (acData.HealDonePerStack != 0)
        num += acData.HealDonePerStack * auraCharges;
      if (num != 0)
        healFlatDictionary.Add(acData.Id, num);
    }
    return healFlatDictionary;
  }

  public Dictionary<string, int> GetAuraHealReceivedPercentDictionary()
  {
    Dictionary<string, int> percentDictionary = new Dictionary<string, int>();
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      AuraCurseData acData = this.auraList[index].ACData;
      int num = 0;
      int auraCharges = this.auraList[index].AuraCharges;
      if (acData.HealDonePercent != 0)
        num += acData.HealReceivedPercent;
      if (acData.HealDonePercentPerStack != 0)
        num += acData.HealReceivedPercentPerStack * auraCharges;
      if (num != 0)
        percentDictionary.Add(acData.Id, num);
    }
    return percentDictionary;
  }

  public Dictionary<string, int> GetAuraHealReceivedFlatDictionary()
  {
    Dictionary<string, int> receivedFlatDictionary = new Dictionary<string, int>();
    for (int index = 0; index < this.auraList.Count; ++index)
    {
      AuraCurseData acData = this.auraList[index].ACData;
      int num = 0;
      int auraCharges = this.auraList[index].AuraCharges;
      if (acData.HealDoneTotal != 0)
        num += acData.HealReceivedTotal;
      if (acData.HealDonePerStack != 0)
        num += acData.HealReceivedPerStack * auraCharges;
      if (num != 0)
        receivedFlatDictionary.Add(acData.Id, num);
    }
    return receivedFlatDictionary;
  }

  public int HealWithCharacterBonus(int heal, Enums.CardClass CC, int energyCost = 0)
  {
    if (this.isHero && (CC == Enums.CardClass.Monster || CC == Enums.CardClass.Injury || CC == Enums.CardClass.Boon))
      return heal;
    int num1 = heal + 0;
    float[] numArray = this.HealBonus(energyCost);
    float num2 = (float) num1 + numArray[0] + (float) this.GetTraitHealFlatBonus() + (float) this.GetItemHealFlatBonus();
    float num3 = numArray[1] + this.GetTraitHealPercentBonus() + this.GetItemHealPercentBonus();
    if ((UnityEngine.Object) this.heroData != (UnityEngine.Object) null)
      num2 += (float) PlayerManager.Instance.GetPerkHealBonus(this.heroData.HeroSubClass.Id);
    return Functions.FuncRoundToInt(num2 + (float) ((double) num2 * (double) num3 * 0.0099999997764825821));
  }

  public int GetTraitHealFlatBonus()
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetTraitHealFlatBonus.Count > 0)
      return this.cacheGetTraitHealFlatBonus[0];
    int traitHealFlatBonus = 0;
    if (this.traits != null)
    {
      for (int index = 0; index < this.traits.Length; ++index)
      {
        if (this.traits[index] != null)
        {
          TraitData traitData = Globals.Instance.GetTraitData(this.traits[index]);
          if ((UnityEngine.Object) traitData != (UnityEngine.Object) null && traitData.HealFlatBonus != 0)
            traitHealFlatBonus += traitData.HealFlatBonus;
        }
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
      this.cacheGetTraitHealFlatBonus.Add(traitHealFlatBonus);
    return traitHealFlatBonus;
  }

  public float GetTraitHealPercentBonus()
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetTraitHealPercentBonus.Count > 0)
      return this.cacheGetTraitHealPercentBonus[0];
    float healPercentBonus = 0.0f;
    if (this.traits != null)
    {
      for (int index = 0; index < this.traits.Length; ++index)
      {
        if (this.traits[index] != null)
        {
          TraitData traitData = Globals.Instance.GetTraitData(this.traits[index]);
          if ((UnityEngine.Object) traitData != (UnityEngine.Object) null && (double) traitData.HealPercentBonus != 0.0)
            healPercentBonus += traitData.HealPercentBonus;
        }
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
      this.cacheGetTraitHealPercentBonus.Add(healPercentBonus);
    return healPercentBonus;
  }

  public int GetTraitHealReceivedFlatBonus()
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetTraitHealReceivedFlatBonus.Count > 0)
      return this.cacheGetTraitHealReceivedFlatBonus[0];
    int receivedFlatBonus = 0;
    if (this.traits != null)
    {
      for (int index = 0; index < this.traits.Length; ++index)
      {
        if (this.traits[index] != null)
        {
          TraitData traitData = Globals.Instance.GetTraitData(this.traits[index]);
          if ((UnityEngine.Object) traitData != (UnityEngine.Object) null && traitData.HealReceivedFlatBonus != 0)
            receivedFlatBonus += traitData.HealReceivedFlatBonus;
        }
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
      this.cacheGetTraitHealReceivedFlatBonus.Add(receivedFlatBonus);
    return receivedFlatBonus;
  }

  public float GetTraitHealReceivedPercentBonus()
  {
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache && this.cacheGetTraitHealReceivedPercentBonus.Count > 0)
      return this.cacheGetTraitHealReceivedPercentBonus[0];
    float receivedPercentBonus = 0.0f;
    if (this.traits != null)
    {
      for (int index = 0; index < this.traits.Length; ++index)
      {
        if (this.traits[index] != null)
        {
          TraitData traitData = Globals.Instance.GetTraitData(this.traits[index]);
          if ((UnityEngine.Object) traitData != (UnityEngine.Object) null && (double) traitData.HealReceivedPercentBonus != 0.0)
            receivedPercentBonus += traitData.HealReceivedPercentBonus;
        }
      }
    }
    if ((bool) (UnityEngine.Object) MatchManager.Instance && this.useCache)
      this.cacheGetTraitHealReceivedPercentBonus.Add(receivedPercentBonus);
    return receivedPercentBonus;
  }

  public int GetItemFinalRewardRetentionModification()
  {
    int retentionModification = 0;
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      ItemData itemDataBySlot = this.GetItemDataBySlot(slot, false);
      if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null && itemDataBySlot.PercentRetentionEndGame != 0)
        retentionModification += itemDataBySlot.PercentRetentionEndGame;
    }
    return retentionModification;
  }

  public int GetItemDiscountModification()
  {
    int discountModification = 0;
    for (int slot = 0; slot < this.itemSlots; ++slot)
    {
      ItemData itemDataBySlot = this.GetItemDataBySlot(slot, false);
      if ((UnityEngine.Object) itemDataBySlot != (UnityEngine.Object) null && itemDataBySlot.PercentDiscountShop != 0)
        discountModification += itemDataBySlot.PercentDiscountShop;
    }
    return discountModification;
  }

  public void DestroyCharacter()
  {
    if ((UnityEngine.Object) this.heroItem != (UnityEngine.Object) null && (UnityEngine.Object) this.heroItem.gameObject != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.heroItem.gameObject);
    else if ((UnityEngine.Object) this.npcItem != (UnityEngine.Object) null && (UnityEngine.Object) this.npcItem.gameObject != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.npcItem.gameObject);
    UnityEngine.Object.Destroy((UnityEngine.Object) this.gO);
    this.npcItem = (NPCItem) null;
    this.heroItem = (HeroItem) null;
    this.auraList.Clear();
  }

  public virtual void CreateOverDeck(bool getCardFromDeck)
  {
  }

  public void RemoveEnchantsStartTurn()
  {
    for (int index = 0; index < 3; ++index)
    {
      if (index == 0)
      {
        ItemData itemData = Globals.Instance.GetItemData(this.enchantment);
        if ((UnityEngine.Object) itemData != (UnityEngine.Object) null && itemData.DestroyStartOfTurn)
          this.enchantment = "";
      }
      else if (index == 1)
      {
        ItemData itemData = Globals.Instance.GetItemData(this.enchantment2);
        if ((UnityEngine.Object) itemData != (UnityEngine.Object) null && itemData.DestroyStartOfTurn)
          this.enchantment2 = "";
      }
      else if (index == 2)
      {
        ItemData itemData = Globals.Instance.GetItemData(this.enchantment3);
        if ((UnityEngine.Object) itemData != (UnityEngine.Object) null && itemData.DestroyStartOfTurn)
          this.enchantment3 = "";
      }
    }
  }

  public void RemoveEnchantsEndTurn()
  {
    for (int index = 0; index < 3; ++index)
    {
      if (index == 0)
      {
        ItemData itemData = Globals.Instance.GetItemData(this.enchantment);
        if ((UnityEngine.Object) itemData != (UnityEngine.Object) null && itemData.DestroyEndOfTurn)
          this.enchantment = "";
      }
      else if (index == 1)
      {
        ItemData itemData = Globals.Instance.GetItemData(this.enchantment2);
        if ((UnityEngine.Object) itemData != (UnityEngine.Object) null && itemData.DestroyEndOfTurn)
          this.enchantment2 = "";
      }
      else if (index == 2)
      {
        ItemData itemData = Globals.Instance.GetItemData(this.enchantment3);
        if ((UnityEngine.Object) itemData != (UnityEngine.Object) null && itemData.DestroyEndOfTurn)
          this.enchantment3 = "";
      }
    }
  }

  public Enums.DamageType GetEnchantModifiedDamageType()
  {
    ItemData itemData = (ItemData) null;
    Enums.DamageType modifiedDamageType = Enums.DamageType.None;
    for (int index = 0; index < 3; ++index)
    {
      if (index == 0)
        itemData = Globals.Instance.GetItemData(this.enchantment);
      else if (index == 1)
        itemData = Globals.Instance.GetItemData(this.enchantment2);
      else if (index == 2)
        itemData = Globals.Instance.GetItemData(this.enchantment3);
      if ((UnityEngine.Object) itemData != (UnityEngine.Object) null && itemData.ModifiedDamageType != Enums.DamageType.None)
        modifiedDamageType = itemData.ModifiedDamageType;
    }
    return modifiedDamageType;
  }

  public Enums.DamageType GetItemModifiedDamageType()
  {
    ItemData itemData = (ItemData) null;
    Enums.DamageType modifiedDamageType = Enums.DamageType.None;
    for (int index = 0; index < 4; ++index)
    {
      string id = "";
      if (index == 0 && this.weapon != "")
        id = this.weapon;
      else if (index == 1 && this.armor != "")
        id = this.armor;
      else if (index == 2 && this.jewelry != "")
        id = this.jewelry;
      else if (index == 3 && this.accesory != "")
        id = this.accesory;
      if (id != "")
        itemData = Globals.Instance.GetItemData(id);
      if ((UnityEngine.Object) itemData != (UnityEngine.Object) null && itemData.ModifiedDamageType != Enums.DamageType.None)
        modifiedDamageType = itemData.ModifiedDamageType;
    }
    return modifiedDamageType;
  }

  public string Owner
  {
    get => this.owner;
    set => this.owner = value;
  }

  public HeroData HeroData
  {
    get => this.heroData;
    set => this.heroData = value;
  }

  public NPCData NpcData
  {
    get => this.npcData;
    set => this.npcData = value;
  }

  public int HeroIndex
  {
    get => this.heroIndex;
    set => this.heroIndex = value;
  }

  public int NPCIndex
  {
    get => this.npcIndex;
    set => this.npcIndex = value;
  }

  public string Id
  {
    get => this.id;
    set => this.id = value;
  }

  public string SubclassName
  {
    get => this.subclassName;
    set => this.subclassName = value;
  }

  public string ClassName
  {
    get => this.className;
    set => this.className = value;
  }

  public string SourceName
  {
    get => this.sourceName;
    set => this.sourceName = value;
  }

  public string GameName
  {
    get => this.gameName;
    set => this.gameName = value;
  }

  public int Position
  {
    get => this.position;
    set => this.position = value;
  }

  public int Experience
  {
    get => this.experience;
    set => this.experience = value;
  }

  public int Level
  {
    get => this.level;
    set => this.level = value;
  }

  public int Hp
  {
    get => this.hp;
    set => this.hp = value;
  }

  public int HpCurrent
  {
    get => this.hpCurrent;
    set => this.hpCurrent = value;
  }

  public int Energy
  {
    get => this.energy;
    set => this.energy = value;
  }

  public int EnergyCurrent
  {
    get => this.energyCurrent;
    set => this.energyCurrent = value;
  }

  public int EnergyTurn
  {
    get => this.energyTurn;
    set => this.energyTurn = value;
  }

  public int Speed
  {
    get => this.speed;
    set => this.speed = value;
  }

  public bool Alive
  {
    get => this.alive;
    set => this.alive = value;
  }

  public List<string> Cards
  {
    get => this.cards;
    set => this.cards = value;
  }

  public string[] Traits
  {
    get => this.traits;
    set => this.traits = value;
  }

  public List<Aura> AuraList
  {
    get => this.auraList;
    set => this.auraList = value;
  }

  public List<string> AuracurseImmune
  {
    get => this.auracurseImmune;
    set => this.auracurseImmune = value;
  }

  public bool IsHero
  {
    get => this.isHero;
    set => this.isHero = value;
  }

  public HeroItem HeroItem
  {
    get => this.heroItem;
    set => this.heroItem = value;
  }

  public NPCItem NPCItem
  {
    get => this.npcItem;
    set => this.npcItem = value;
  }

  public int RoundMoved
  {
    get => this.roundMoved;
    set => this.roundMoved = value;
  }

  public GameObject GO
  {
    get => this.gO;
    set => this.gO = value;
  }

  public int ResistSlashing
  {
    get => this.resistSlashing;
    set => this.resistSlashing = value;
  }

  public bool ImmuneSlashing
  {
    get => this.immuneSlashing;
    set => this.immuneSlashing = value;
  }

  public int ResistBlunt
  {
    get => this.resistBlunt;
    set => this.resistBlunt = value;
  }

  public bool ImmuneBlunt
  {
    get => this.immuneBlunt;
    set => this.immuneBlunt = value;
  }

  public int ResistPiercing
  {
    get => this.resistPiercing;
    set => this.resistPiercing = value;
  }

  public bool ImmunePiercing
  {
    get => this.immunePiercing;
    set => this.immunePiercing = value;
  }

  public int ResistFire
  {
    get => this.resistFire;
    set => this.resistFire = value;
  }

  public bool ImmuneFire
  {
    get => this.immuneFire;
    set => this.immuneFire = value;
  }

  public int ResistCold
  {
    get => this.resistCold;
    set => this.resistCold = value;
  }

  public bool ImmuneCold
  {
    get => this.immuneCold;
    set => this.immuneCold = value;
  }

  public int ResistLightning
  {
    get => this.resistLightning;
    set => this.resistLightning = value;
  }

  public bool ImmuneLightning
  {
    get => this.immuneLightning;
    set => this.immuneLightning = value;
  }

  public int ResistMind
  {
    get => this.resistMind;
    set => this.resistMind = value;
  }

  public bool ImmuneMind
  {
    get => this.immuneMind;
    set => this.immuneMind = value;
  }

  public int ResistHoly
  {
    get => this.resistHoly;
    set => this.resistHoly = value;
  }

  public bool ImmuneHoly
  {
    get => this.immuneHoly;
    set => this.immuneHoly = value;
  }

  public int ResistShadow
  {
    get => this.resistShadow;
    set => this.resistShadow = value;
  }

  public bool ImmuneShadow
  {
    get => this.immuneShadow;
    set => this.immuneShadow = value;
  }

  public Sprite SpriteSpeed
  {
    get => this.spriteSpeed;
    set => this.spriteSpeed = value;
  }

  public Sprite SpritePortrait
  {
    get => this.spritePortrait;
    set => this.spritePortrait = value;
  }

  public Dictionary<string, int> AuraCurseModification
  {
    get => this.auraCurseModification;
    set => this.auraCurseModification = value;
  }

  public List<CardData> CardsPlayed
  {
    get => this.cardsPlayed;
    set => this.cardsPlayed = value;
  }

  public List<CardData> CardsPlayedRound
  {
    get => this.cardsPlayedRound;
    set => this.cardsPlayedRound = value;
  }

  public int CraftRemainingUses
  {
    get => this.craftRemainingUses;
    set => this.craftRemainingUses = value;
  }

  public int CardsUpgraded
  {
    get => this.cardsUpgraded;
    set => this.cardsUpgraded = value;
  }

  public int CardsRemoved
  {
    get => this.cardsRemoved;
    set => this.cardsRemoved = value;
  }

  public int CardsCrafted
  {
    get => this.cardsCrafted;
    set => this.cardsCrafted = value;
  }

  public string Weapon
  {
    get => this.weapon;
    set => this.weapon = value;
  }

  public string Armor
  {
    get => this.armor;
    set => this.armor = value;
  }

  public string Jewelry
  {
    get => this.jewelry;
    set => this.jewelry = value;
  }

  public string Accesory
  {
    get => this.accesory;
    set => this.accesory = value;
  }

  public string Corruption
  {
    get => this.corruption;
    set => this.corruption = value;
  }

  public string Pet
  {
    get => this.pet;
    set => this.pet = value;
  }

  public string Enchantment
  {
    get => this.enchantment;
    set => this.enchantment = value;
  }

  public string Enchantment2
  {
    get => this.enchantment2;
    set => this.enchantment2 = value;
  }

  public string Enchantment3
  {
    get => this.enchantment3;
    set => this.enchantment3 = value;
  }

  public int PerkRank
  {
    get => this.perkRank;
    set => this.perkRank = value;
  }

  public List<string> PerkList
  {
    get => this.perkList;
    set => this.perkList = value;
  }

  public float HeroGold
  {
    get => this.heroGold;
    set => this.heroGold = value;
  }

  public float HeroDust
  {
    get => this.heroDust;
    set => this.heroDust = value;
  }

  public CardData CardCasted
  {
    get => this.cardCasted;
    set => this.cardCasted = value;
  }

  public string SkinUsed
  {
    get => this.skinUsed;
    set => this.skinUsed = value;
  }

  public string OwnerOriginal
  {
    get => this.ownerOriginal;
    set => this.ownerOriginal = value;
  }

  public int TotalDeaths
  {
    get => this.totalDeaths;
    set => this.totalDeaths = value;
  }

  public string CardbackUsed
  {
    get => this.cardbackUsed;
    set => this.cardbackUsed = value;
  }

  public string ScriptableObjectName
  {
    get => this.scriptableObjectName;
    set => this.scriptableObjectName = value;
  }
}
