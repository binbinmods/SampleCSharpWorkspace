// Decompiled with JetBrains decompiler
// Type: Hero
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Hero : Character
{
  [SerializeField]
  private GameObject gameObjectAnimated;
  [SerializeField]
  private string internalId = "";
  private Sprite heroSprite;
  private Sprite borderSprite;

  public void RedoSubclassFromSkin()
  {
    if ((UnityEngine.Object) this.HeroData == (UnityEngine.Object) null)
      return;
    SubClassData heroSubClass = this.HeroData.HeroSubClass;
    this.heroSprite = heroSubClass.Sprite;
    this.borderSprite = heroSubClass.SpriteBorder;
    if ((UnityEngine.Object) this.borderSprite == (UnityEngine.Object) null)
      Debug.LogError((object) "Error with borderSprite");
    this.SpriteSpeed = heroSubClass.SpriteSpeed;
    this.SpritePortrait = heroSubClass.SpritePortrait;
    this.gameObjectAnimated = heroSubClass.GameObjectAnimated;
  }

  public void InitGeneralData()
  {
    if ((UnityEngine.Object) this.HeroData == (UnityEngine.Object) null)
      return;
    SubClassData heroSubClass = this.HeroData.HeroSubClass;
    this.heroSprite = heroSubClass.Sprite;
    this.borderSprite = heroSubClass.SpriteBorder;
    if ((UnityEngine.Object) this.borderSprite == (UnityEngine.Object) null)
      Debug.LogError((object) "Error with borderSprite");
    this.SpriteSpeed = heroSubClass.SpriteSpeed;
    this.SpritePortrait = heroSubClass.SpritePortrait;
    this.gameObjectAnimated = heroSubClass.GameObjectAnimated;
    this.SourceName = heroSubClass.CharacterName;
    this.ClassName = this.HeroData.HeroName.ToLower();
    this.SubclassName = heroSubClass.Id;
    if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null)
      this.internalId = MatchManager.Instance.GetRandomString();
    else
      this.internalId = Functions.RandomString(6f);
  }

  public void InitHP()
  {
    if ((UnityEngine.Object) this.HeroData == (UnityEngine.Object) null)
      return;
    SubClassData heroSubClass = this.HeroData.HeroSubClass;
    this.Hp = this.HpCurrent = heroSubClass.Hp;
    this.Hp += PlayerManager.Instance.GetPerkMaxHealth(heroSubClass.Id);
    this.HpCurrent += PlayerManager.Instance.GetPerkMaxHealth(heroSubClass.Id);
  }

  public void InitEnergy()
  {
    if ((UnityEngine.Object) this.HeroData == (UnityEngine.Object) null)
      return;
    SubClassData heroSubClass = this.HeroData.HeroSubClass;
    this.Energy = heroSubClass.Energy + PlayerManager.Instance.GetPerkEnergyBegin(heroSubClass.Id);
    this.EnergyCurrent = this.Energy;
    if (AtOManager.Instance.IsChallengeTraitActive("energizedheroeslow"))
    {
      ++this.Energy;
      ++this.EnergyCurrent;
    }
    if (AtOManager.Instance.IsChallengeTraitActive("energizedheroes"))
    {
      ++this.Energy;
      ++this.EnergyCurrent;
    }
    int ngPlus = AtOManager.Instance.GetNgPlus();
    if (ngPlus > 3)
    {
      switch (ngPlus)
      {
        case 4:
          --this.Energy;
          --this.EnergyCurrent;
          break;
        case 5:
          --this.Energy;
          --this.EnergyCurrent;
          break;
        case 6:
          --this.Energy;
          --this.EnergyCurrent;
          break;
        case 7:
          this.Energy -= 2;
          this.EnergyCurrent -= 2;
          break;
        case 8:
          this.Energy -= 2;
          this.EnergyCurrent -= 2;
          break;
      }
      if (this.Energy < 0)
        this.Energy = 0;
      if (this.EnergyCurrent < 0)
        this.EnergyCurrent = 0;
    }
    this.EnergyTurn = heroSubClass.EnergyTurn;
    if (this.Traits != null)
    {
      for (int index = 0; index < this.Traits.Length; ++index)
      {
        if (this.Traits[index] != null && this.Traits[index] != "")
        {
          TraitData traitData = Globals.Instance.GetTraitData(this.Traits[index]);
          if ((UnityEngine.Object) traitData != (UnityEngine.Object) null && traitData.CharacterStatModified == Enums.CharacterStat.EnergyTurn)
            this.EnergyTurn += traitData.CharacterStatModifiedValue;
        }
      }
    }
    if (!SandboxManager.Instance.IsEnabled() || AtOManager.Instance.Sandbox_startingEnergy == 0)
      return;
    this.Energy += AtOManager.Instance.Sandbox_startingEnergy;
    this.EnergyCurrent += AtOManager.Instance.Sandbox_startingEnergy;
  }

  public void InitSpeed()
  {
    if ((UnityEngine.Object) this.HeroData == (UnityEngine.Object) null)
      return;
    SubClassData heroSubClass = this.HeroData.HeroSubClass;
    this.Speed = heroSubClass.Speed;
    this.Speed += PlayerManager.Instance.GetPerkSpeed(heroSubClass.Id);
    if (AtOManager.Instance.IsChallengeTraitActive("slowheroes"))
      --this.Speed;
    if (AtOManager.Instance.IsChallengeTraitActive("fastheroes"))
      ++this.Speed;
    if (!SandboxManager.Instance.IsEnabled() || AtOManager.Instance.Sandbox_startingSpeed == 0)
      return;
    this.Speed += AtOManager.Instance.Sandbox_startingSpeed;
  }

  public void InitResist()
  {
    if ((UnityEngine.Object) this.HeroData == (UnityEngine.Object) null)
      return;
    SubClassData heroSubClass = this.HeroData.HeroSubClass;
    this.ResistSlashing = heroSubClass.ResistSlashing + PlayerManager.Instance.GetPerkResistBonus(heroSubClass.Id, Enums.DamageType.Slashing);
    if (this.ResistSlashing >= 100)
      this.ImmuneSlashing = true;
    this.ResistBlunt = heroSubClass.ResistBlunt + PlayerManager.Instance.GetPerkResistBonus(heroSubClass.Id, Enums.DamageType.Blunt);
    if (this.ResistBlunt >= 100)
      this.ImmuneBlunt = true;
    this.ResistPiercing = heroSubClass.ResistPiercing + PlayerManager.Instance.GetPerkResistBonus(heroSubClass.Id, Enums.DamageType.Piercing);
    if (this.ResistPiercing >= 100)
      this.ImmunePiercing = true;
    this.ResistFire = heroSubClass.ResistFire + PlayerManager.Instance.GetPerkResistBonus(heroSubClass.Id, Enums.DamageType.Fire);
    if (this.ResistFire >= 100)
      this.ImmuneFire = true;
    this.ResistCold = heroSubClass.ResistCold + PlayerManager.Instance.GetPerkResistBonus(heroSubClass.Id, Enums.DamageType.Cold);
    if (this.ResistCold >= 100)
      this.ImmuneCold = true;
    this.ResistLightning = heroSubClass.ResistLightning + PlayerManager.Instance.GetPerkResistBonus(heroSubClass.Id, Enums.DamageType.Lightning);
    if (this.ResistLightning >= 100)
      this.ImmuneLightning = true;
    this.ResistMind = heroSubClass.ResistMind + PlayerManager.Instance.GetPerkResistBonus(heroSubClass.Id, Enums.DamageType.Mind);
    if (this.ResistMind >= 100)
      this.ImmuneMind = true;
    this.ResistHoly = heroSubClass.ResistHoly + PlayerManager.Instance.GetPerkResistBonus(heroSubClass.Id, Enums.DamageType.Holy);
    if (this.ResistHoly >= 100)
      this.ImmuneHoly = true;
    this.ResistShadow = heroSubClass.ResistShadow + PlayerManager.Instance.GetPerkResistBonus(heroSubClass.Id, Enums.DamageType.Shadow);
    if (this.ResistShadow < 100)
      return;
    this.ImmuneShadow = true;
  }

  public void InitData()
  {
    if (!((UnityEngine.Object) this.HeroData != (UnityEngine.Object) null))
      return;
    this.InitGeneralData();
    SubClassData heroSubClass = this.HeroData.HeroSubClass;
    this.Id = heroSubClass.Id + "_" + this.internalId;
    this.InitHP();
    this.InitEnergy();
    this.InitSpeed();
    this.Experience = 0;
    this.Level = 1;
    this.IsHero = true;
    this.InitResist();
    this.Alive = true;
    this.AuraList = new List<Aura>();
    this.Traits = new string[5];
    this.SetInitialItems(heroSubClass.Item, 0);
    if (GameManager.Instance.IsObeliskChallenge())
      this.ReassignInitialItem();
    this.Cards = new List<string>();
    if (GameManager.Instance.IsObeliskChallenge())
      return;
    this.SetInitalCards(this.HeroData);
  }

  public void AssignOwner(string nickName)
  {
    this.Owner = nickName;
    if (!(this.OwnerOriginal == ""))
      return;
    this.OwnerOriginal = NetworkManager.Instance.GetPlayerNickReal(nickName);
  }

  public override void BeginTurn() => base.BeginTurn();

  public override void BeginTurnPerks() => base.BeginTurnPerks();

  public bool AssignTrait(string traitName)
  {
    TraitData traitData = Globals.Instance.GetTraitData(traitName);
    if (!((UnityEngine.Object) traitData != (UnityEngine.Object) null))
      return false;
    if (traitData.Id == "eternalbond")
      this.Pet = "harleyrare";
    int traitLevel = this.HeroData.HeroSubClass.GetTraitLevel(traitName);
    if (traitLevel < 0 || this.Traits[traitLevel] != null && this.Traits[traitLevel] != "")
      return false;
    this.Traits[traitLevel] = traitName.ToLower();
    if (traitData.CharacterStatModified != Enums.CharacterStat.None)
    {
      if (traitData.CharacterStatModified == Enums.CharacterStat.Hp)
      {
        if (this.Hp == this.HpCurrent)
          this.HpCurrent += traitData.CharacterStatModifiedValue;
        this.Hp += traitData.CharacterStatModifiedValue;
      }
      else if (traitData.CharacterStatModified == Enums.CharacterStat.Speed)
        this.Speed += traitData.CharacterStatModifiedValue;
      else if (traitData.CharacterStatModified == Enums.CharacterStat.Energy)
        this.Energy += traitData.CharacterStatModifiedValue;
      else if (traitData.CharacterStatModified == Enums.CharacterStat.EnergyTurn)
        this.EnergyTurn += traitData.CharacterStatModifiedValue;
    }
    if (traitData.ResistModified1 != Enums.DamageType.None)
      this.ModifyResist(this, traitData.ResistModified1, traitData.ResistModifiedValue1);
    if (traitData.ResistModified2 != Enums.DamageType.None)
      this.ModifyResist(this, traitData.ResistModified2, traitData.ResistModifiedValue2);
    if (traitData.ResistModified3 != Enums.DamageType.None)
      this.ModifyResist(this, traitData.ResistModified3, traitData.ResistModifiedValue3);
    if (traitData.AuracurseImmune1.Trim() != "")
      this.AuracurseImmune.Add(traitData.AuracurseImmune1.Trim().ToLower());
    if (traitData.AuracurseImmune2.Trim() != "")
      this.AuracurseImmune.Add(traitData.AuracurseImmune2.Trim().ToLower());
    if (traitData.AuracurseImmune3.Trim() != "")
      this.AuracurseImmune.Add(traitData.AuracurseImmune3.Trim().ToLower());
    this.ClearCaches();
    return true;
  }

  private void ModifyResist(Hero hero, Enums.DamageType damageType, int value)
  {
    switch (damageType)
    {
      case Enums.DamageType.Slashing:
        hero.ResistSlashing += value;
        break;
      case Enums.DamageType.Blunt:
        hero.ResistBlunt += value;
        break;
      case Enums.DamageType.Piercing:
        hero.ResistPiercing += value;
        break;
      case Enums.DamageType.Fire:
        hero.ResistFire += value;
        break;
      case Enums.DamageType.Cold:
        hero.ResistCold += value;
        break;
      case Enums.DamageType.Lightning:
        hero.ResistLightning += value;
        break;
      case Enums.DamageType.Mind:
        hero.ResistMind += value;
        break;
      case Enums.DamageType.Holy:
        hero.ResistHoly += value;
        break;
      case Enums.DamageType.Shadow:
        hero.ResistShadow += value;
        break;
      case Enums.DamageType.All:
        hero.ResistSlashing += value;
        hero.ResistBlunt += value;
        hero.ResistPiercing += value;
        hero.ResistFire += value;
        hero.ResistCold += value;
        hero.ResistLightning += value;
        hero.ResistMind += value;
        hero.ResistHoly += value;
        hero.ResistShadow += value;
        break;
    }
  }

  private void SetInitalCards(HeroData heroData)
  {
    if ((UnityEngine.Object) this.HeroData == (UnityEngine.Object) null)
      return;
    SubClassData heroSubClass = heroData.HeroSubClass;
    if (heroSubClass.Cards == null)
      return;
    int _rank = this.PerkRank;
    if ((UnityEngine.Object) HeroSelectionManager.Instance != (UnityEngine.Object) null)
      _rank = HeroSelectionManager.Instance.heroSelectionDictionary[this.SubclassName].rankTMHidden;
    int characterTier = PlayerManager.Instance.GetCharacterTier("", "card", _rank);
    for (int index1 = 0; index1 < heroSubClass.Cards.Length; ++index1)
    {
      if (heroSubClass.Cards[index1] != null)
      {
        for (int index2 = 0; index2 < heroSubClass.Cards[index1].UnitsInDeck; ++index2)
        {
          if (heroSubClass.Cards[index1] != null)
          {
            string id = heroSubClass.Cards[index1].Card.Id;
            if (heroSubClass.Cards[index1].Card.Starter)
            {
              switch (characterTier)
              {
                case 1:
                  id = Globals.Instance.GetCardData(id, false).UpgradesTo1;
                  break;
                case 2:
                  id = Globals.Instance.GetCardData(id, false).UpgradesTo2;
                  break;
              }
            }
            this.Cards.Add(id);
          }
        }
      }
    }
  }

  private void SetInitialItems(CardData _cardData, int _rankLevel)
  {
    if ((UnityEngine.Object) _cardData == (UnityEngine.Object) null || !((UnityEngine.Object) _cardData.Item != (UnityEngine.Object) null))
      return;
    string id = _cardData.Id;
    switch (_rankLevel)
    {
      case 1:
        id = Globals.Instance.GetCardData(id, false).UpgradesTo1;
        break;
      case 2:
        id = Globals.Instance.GetCardData(id, false).UpgradesTo2;
        break;
    }
    if (_cardData.CardType == Enums.CardType.Weapon)
      this.Weapon = id;
    else if (_cardData.CardType == Enums.CardType.Armor)
      this.Armor = id;
    else if (_cardData.CardType == Enums.CardType.Jewelry)
      this.Jewelry = id;
    else if (_cardData.CardType == Enums.CardType.Accesory)
      this.Accesory = id;
    else if (_cardData.CardType == Enums.CardType.Pet)
      this.Pet = id;
    CardData cardData = Globals.Instance.GetCardData(id, false);
    if (!((UnityEngine.Object) cardData != (UnityEngine.Object) null))
      return;
    ItemData itemData = cardData.Item;
    if (!((UnityEngine.Object) itemData != (UnityEngine.Object) null) || itemData.MaxHealth == 0)
      return;
    this.ModifyMaxHP(itemData.MaxHealth);
  }

  public void ReassignInitialItem()
  {
    if ((UnityEngine.Object) this.HeroData == (UnityEngine.Object) null)
      return;
    int num = 0;
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      int _rank = this.PerkRank;
      if ((UnityEngine.Object) HeroSelectionManager.Instance != (UnityEngine.Object) null)
        _rank = HeroSelectionManager.Instance.heroSelectionDictionary[this.SubclassName].rankTMHidden;
      num = PlayerManager.Instance.GetCharacterTier("", "item", _rank);
    }
    else if (GameManager.Instance.IsWeeklyChallenge())
    {
      num = 2;
    }
    else
    {
      int obeliskMadness = AtOManager.Instance.GetObeliskMadness();
      if (obeliskMadness >= 5)
        num = obeliskMadness > 8 ? 2 : 1;
    }
    if (num == 1)
    {
      this.SetInitialItems(this.HeroData.HeroSubClass.Item, 1);
    }
    else
    {
      if (num != 2)
        return;
      this.SetInitialItems(this.HeroData.HeroSubClass.Item, 2);
    }
  }

  public CardData GetPet()
  {
    if (this.Pet != null && this.Pet != "")
    {
      CardData cardData = Globals.Instance.GetCardData(this.Pet);
      if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && cardData.CardType == Enums.CardType.Pet)
        return cardData;
    }
    return (CardData) null;
  }

  public int GetTotalCardsInDeck(bool excludeInjuriesAndBoons = false)
  {
    int count = this.Cards.Count;
    if (excludeInjuriesAndBoons)
    {
      CardData cardData1 = (CardData) null;
      for (int index = 0; index < this.Cards.Count; ++index)
      {
        CardData cardData2 = Globals.Instance.GetCardData(this.Cards[index], false);
        if (cardData2.CardType == Enums.CardType.Boon || cardData2.CardType == Enums.CardType.Injury)
          --count;
      }
      cardData1 = (CardData) null;
    }
    return count;
  }

  public override void CreateOverDeck(bool getCardFromDeck)
  {
  }

  public Sprite HeroSprite
  {
    get => this.heroSprite;
    set => this.heroSprite = value;
  }

  public Sprite BorderSprite
  {
    get => this.borderSprite;
    set => this.borderSprite = value;
  }

  public GameObject GameObjectAnimated
  {
    get => this.gameObjectAnimated;
    set => this.gameObjectAnimated = value;
  }

  public string InternalId
  {
    get => this.internalId;
    set => this.internalId = value;
  }
}
