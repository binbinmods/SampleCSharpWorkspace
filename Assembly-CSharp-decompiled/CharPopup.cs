// Decompiled with JetBrains decompiler
// Type: CharPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharPopup : MonoBehaviour
{
  private SubClassData SCD;
  private SubClassData oldSCD;
  public Transform groupCharacter;
  public Transform groupStats;
  public BotonGeneric buttonStats;
  public Transform groupRank;
  public BotonGeneric buttonRank;
  public Transform groupPerks;
  public BotonGeneric buttonPerks;
  public BotonGeneric buttonResetPerks;
  public Transform groupSkins;
  public BotonGeneric buttonSkins;
  public Transform groupCardback;
  public Transform groupCardbackGOsCharacter;
  public Transform groupCardbackGOsGeneral;
  public Transform groupCardbackGOsMisc;
  public BotonGeneric buttonCardback;
  public GameObject goCardback;
  public Transform buttonClose;
  public TMP_Text warningPerk;
  public Transform warningPerkT;
  public TMP_Text rankProgress;
  public TMP_Text maxProgress;
  public TMP_Text availablePerksPoints;
  public Transform perkBarMask;
  public SpriteRenderer perkBar;
  public Transform perksNotAvailable;
  public TMP_Text _Name;
  public TMP_Text _Class;
  public TMP_Text _Fluff;
  public TMP_Text _HP;
  public TMP_Text _Energy;
  public TMP_Text _Speed;
  public TMP_Text _Slashing;
  public TMP_Text _Blunt;
  public TMP_Text _Piercing;
  public TMP_Text _Fire;
  public TMP_Text _Cold;
  public TMP_Text _Lightning;
  public TMP_Text _Mind;
  public TMP_Text _Holy;
  public TMP_Text _Shadow;
  public TMP_Text _Trait0;
  public TMP_Text _Trait1A;
  public TMP_Text _Trait1B;
  public TMP_Text _Trait2A;
  public TMP_Text _Trait2B;
  public TMP_Text _Trait3A;
  public TMP_Text _Trait3B;
  public TMP_Text _Trait4A;
  public TMP_Text _Trait4B;
  private TraitRollOver _Trait0RO;
  private TraitRollOver _Trait1ARO;
  private TraitRollOver _Trait1BRO;
  private TraitRollOver _Trait2ARO;
  private TraitRollOver _Trait2BRO;
  private TraitRollOver _Trait3ARO;
  private TraitRollOver _Trait3BRO;
  private TraitRollOver _Trait4ARO;
  private TraitRollOver _Trait4BRO;
  public SpriteRenderer _OverAnimated;
  public TMP_Text _CardN1;
  public TMP_Text _CardN2;
  public TMP_Text _CardN3;
  public TMP_Text _CardN4;
  public TMP_Text _CardN5;
  public TMP_Text _CardN6;
  public TMP_Text _CardN7;
  public GameObject _CardPrefab;
  public Transform _CardParent;
  public Transform _HeroParent;
  public SpriteRenderer _SpriteSubstitution;
  private Vector3 destination = Vector3.zero;
  private bool moveThis;
  private bool closeThis;
  private bool opened;
  private CardItem[] cardsCI = new CardItem[7];
  private Transform[] cardsT = new Transform[7];
  private Transform[] cardsNumT = new Transform[7];
  private TMP_Text[] cardsNumText = new TMP_Text[7];
  public Transform cardItemT;
  public Transform cardItemNoneT;
  private CardItem cardItemCI;
  private Vector3 destinationCenter = Vector3.zero;
  private Vector3 destinationOut = Vector3.zero;
  private GameObject heroAnimated;
  private string activeId = "";
  public PerkColumn[] perkColumns;
  public BotonSkin[] botonSkinBase;
  public ProgressionRow[] progressionRows;
  private List<SpriteRenderer> animatedSprites;
  private List<SetSpriteLayerFromBase> animatedSpritesOutOfCharacter;
  public TMP_Text useSuppliesAvailable;
  public BotonGeneric useSuppliesButton;
  public SpriteRenderer useSuppliesBg;
  private Color useSuppliesBgOn = new Color(0.41f, 0.3f, 0.2f, 0.5f);
  private Color useSuppliesBgOff = new Color(0.4f, 0.13f, 0.1f, 0.5f);
  private Animator heroAnim;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();
  private List<Transform> _controllerVerticalList = new List<Transform>();

  public bool MoveThis
  {
    get => this.moveThis;
    set => this.moveThis = value;
  }

  private void Awake()
  {
    this.groupStats.gameObject.SetActive(true);
    this._Trait0RO = this._Trait0.transform.parent.GetComponent<TraitRollOver>();
    this._Trait1ARO = this._Trait1A.transform.parent.GetComponent<TraitRollOver>();
    this._Trait1BRO = this._Trait1B.transform.parent.GetComponent<TraitRollOver>();
    this._Trait2ARO = this._Trait2A.transform.parent.GetComponent<TraitRollOver>();
    this._Trait2BRO = this._Trait2B.transform.parent.GetComponent<TraitRollOver>();
    this._Trait3ARO = this._Trait3A.transform.parent.GetComponent<TraitRollOver>();
    this._Trait3BRO = this._Trait3B.transform.parent.GetComponent<TraitRollOver>();
    this._Trait4ARO = this._Trait4A.transform.parent.GetComponent<TraitRollOver>();
    this._Trait4BRO = this._Trait4B.transform.parent.GetComponent<TraitRollOver>();
    for (int index = 0; index < 7; ++index)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this._CardParent);
      CardItem component = gameObject.GetComponent<CardItem>();
      gameObject.name = "card_" + index.ToString();
      component.cardoutsidecombat = true;
      component.cardoutsideselection = true;
      component.TopLayeringOrder("UI");
      Transform transform = gameObject.transform;
      transform.localPosition = new Vector3((float) ((double) index * 1.2999999523162842 - 1.7999999523162842), -1.15f, 0.0f);
      transform.localScale = new Vector3(0.65f, 0.65f, 1f);
      transform.gameObject.SetActive(false);
      this.cardsCI[index] = component;
      this.cardsT[index] = transform;
    }
    this.cardsNumT[0] = this._CardN1.transform.parent;
    this.cardsNumText[0] = this._CardN1;
    this.cardsNumT[1] = this._CardN2.transform.parent;
    this.cardsNumText[1] = this._CardN2;
    this.cardsNumT[2] = this._CardN3.transform.parent;
    this.cardsNumText[2] = this._CardN3;
    this.cardsNumT[3] = this._CardN4.transform.parent;
    this.cardsNumText[3] = this._CardN4;
    this.cardsNumT[4] = this._CardN5.transform.parent;
    this.cardsNumText[4] = this._CardN5;
    this.cardsNumT[5] = this._CardN6.transform.parent;
    this.cardsNumText[5] = this._CardN6;
    this.cardsNumT[6] = this._CardN7.transform.parent;
    this.cardsNumText[6] = this._CardN7;
    this.cardItemCI = this.cardItemT.GetComponent<CardItem>();
  }

  private void Start()
  {
    if (GameManager.Instance.IsObeliskChallenge())
    {
      this.buttonRank.GetComponent<PopupText>().id = "perksNotChallenge";
      this.buttonPerks.GetComponent<PopupText>().id = "perksNotChallenge";
    }
    else
    {
      this.buttonRank.GetComponent<PopupText>().id = "";
      this.buttonPerks.GetComponent<PopupText>().id = "";
    }
    this.HideNow();
  }

  private void Update()
  {
    if (!this.moveThis)
      return;
    this.transform.position = Vector3.Lerp(this.transform.position, this.destination, Time.deltaTime * 15f);
    if ((double) Vector3.Distance(this.transform.position, this.destination) >= 0.019999999552965164)
      return;
    this.transform.position = this.destination;
    this.moveThis = false;
    if (this.closeThis)
    {
      this.closeThis = false;
      this.HideNow();
      this.opened = false;
    }
    else
      this.opened = true;
  }

  public bool IsOpened() => this.opened;

  public string GetActive() => this.activeId;

  public void Trigger(SubClassData _scd, bool isHeroStats)
  {
    if (!this.opened)
    {
      if (this.activeId != _scd.Id)
        this.Init(_scd, isHeroStats);
      if (!isHeroStats)
        return;
      this.Show();
    }
    else if (isHeroStats)
    {
      if (this.groupStats.gameObject.activeSelf && this.activeId == _scd.Id)
        this.Close();
      else if (this.activeId == _scd.Id)
        this.ShowStats();
      else
        this.Init(_scd, isHeroStats);
    }
    else if (this.groupPerks.gameObject.activeSelf && this.activeId == _scd.Id)
      this.Close();
    else if (this.activeId == _scd.Id)
      this.ShowPerks();
    else
      this.Init(_scd, isHeroStats);
  }

  public void RefreshBecauseOfPerks() => this.Init(this.SCD, false);

  public void Init(SubClassData _scd, bool doStats = true)
  {
    if ((UnityEngine.Object) _scd == (UnityEngine.Object) null)
      return;
    this.SCD = _scd;
    int characterTier1 = PlayerManager.Instance.GetCharacterTier(_scd.Id, "trait");
    int characterTier2 = PlayerManager.Instance.GetCharacterTier(_scd.Id, "item");
    int characterTier3 = PlayerManager.Instance.GetCharacterTier(_scd.Id, "card");
    this.activeId = _scd.Id;
    this._OverAnimated.color = Functions.HexToColor(Globals.Instance.ClassColor[Enum.GetName(typeof (Enums.HeroClass), (object) _scd.HeroClass)]);
    this._OverAnimated.color = new Color(this._OverAnimated.color.r, this._OverAnimated.color.g, this._OverAnimated.color.b, 0.2f);
    this._Name.text = this.buttonResetPerks.auxString = _scd.CharacterName;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Functions.UppercaseFirst(Texts.Instance.GetText(_scd.SubClassName)));
    stringBuilder.Append("  <size=-.4><sprite name=");
    stringBuilder.Append(Functions.ClassIconFromString(Enum.GetName(typeof (Enums.HeroClass), (object) _scd.HeroClass)));
    stringBuilder.Append(">");
    if (_scd.HeroClassSecondary != Enums.HeroClass.None)
    {
      stringBuilder.Append("<size=2><voffset=.5><color=#444>|</color></voffset></size>  <sprite name=");
      stringBuilder.Append(Functions.ClassIconFromString(Enum.GetName(typeof (Enums.HeroClass), (object) _scd.HeroClassSecondary)));
      stringBuilder.Append(">");
    }
    stringBuilder.Append("</size>");
    this._Class.text = stringBuilder.ToString();
    stringBuilder.Clear();
    this._Class.color = Functions.HexToColor(Globals.Instance.ClassColor[Enum.GetName(typeof (Enums.HeroClass), (object) _scd.HeroClass)]);
    stringBuilder.Append("<size=-.3>");
    stringBuilder.Append(_scd.CharacterDescription);
    stringBuilder.Append("</size><br><br><color=#F1D2A9>");
    stringBuilder.Append(_scd.CharacterDescriptionStrength);
    stringBuilder.Append("</color>");
    this._Fluff.text = stringBuilder.ToString();
    if ((UnityEngine.Object) _scd.Item == (UnityEngine.Object) null)
    {
      this.cardItemT.gameObject.SetActive(false);
      this.cardItemCI.enabled = false;
      this.cardItemNoneT.gameObject.SetActive(true);
    }
    else
    {
      this.cardItemNoneT.gameObject.SetActive(false);
      this.cardItemT.gameObject.SetActive(true);
      this.cardItemCI.enabled = true;
      string id = _scd.Item.Id;
      switch (characterTier2)
      {
        case 1:
          id = Globals.Instance.GetCardData(id, false).UpgradesTo1;
          break;
        case 2:
          id = Globals.Instance.GetCardData(id, false).UpgradesTo2;
          break;
      }
      this.cardItemCI.SetCard(id);
      this.cardItemCI.cardoutsidecombat = true;
      this.cardItemCI.cardoutsideselection = true;
      this.cardItemCI.TopLayeringOrder("UI", 10000);
      this.cardItemT.transform.localScale = new Vector3(0.83f, 0.83f, 1f);
    }
    int hp = _scd.Hp;
    if (!GameManager.Instance.IsObeliskChallenge())
      hp += PlayerManager.Instance.GetPerkMaxHealth(_scd.Id);
    this._HP.text = hp.ToString();
    stringBuilder.Clear();
    int energy = _scd.Energy;
    if (!GameManager.Instance.IsObeliskChallenge())
      energy += PlayerManager.Instance.GetPerkEnergyBegin(_scd.Id);
    stringBuilder.Append(energy);
    stringBuilder.Append(" <size=1.3>");
    stringBuilder.Append(Texts.Instance.GetText("dataPerTurn").Replace("<%>", _scd.EnergyTurn.ToString()));
    stringBuilder.Append("</size>");
    this._Energy.text = stringBuilder.ToString();
    int speed = _scd.Speed;
    if (!GameManager.Instance.IsObeliskChallenge())
      speed += PlayerManager.Instance.GetPerkSpeed(_scd.Id);
    this._Speed.text = speed.ToString();
    Color white = Color.white;
    Color color = new Color(0.665f, 0.665f, 0.665f);
    int resistSlashing = _scd.ResistSlashing;
    if (!GameManager.Instance.IsObeliskChallenge())
      resistSlashing += PlayerManager.Instance.GetPerkResistBonus(_scd.Id, Enums.DamageType.Slashing);
    this._Slashing.text = resistSlashing.ToString() + "%";
    if (resistSlashing > 0)
      this._Slashing.color = white;
    else
      this._Slashing.color = color;
    int resistBlunt = _scd.ResistBlunt;
    if (!GameManager.Instance.IsObeliskChallenge())
      resistBlunt += PlayerManager.Instance.GetPerkResistBonus(_scd.Id, Enums.DamageType.Blunt);
    this._Blunt.text = resistBlunt.ToString() + "%";
    if (resistBlunt > 0)
      this._Blunt.color = white;
    else
      this._Blunt.color = color;
    int resistPiercing = _scd.ResistPiercing;
    if (!GameManager.Instance.IsObeliskChallenge())
      resistPiercing += PlayerManager.Instance.GetPerkResistBonus(_scd.Id, Enums.DamageType.Piercing);
    this._Piercing.text = resistPiercing.ToString() + "%";
    if (resistPiercing > 0)
      this._Piercing.color = white;
    else
      this._Piercing.color = color;
    int resistFire = _scd.ResistFire;
    if (!GameManager.Instance.IsObeliskChallenge())
      resistFire += PlayerManager.Instance.GetPerkResistBonus(_scd.Id, Enums.DamageType.Fire);
    this._Fire.text = resistFire.ToString() + "%";
    if (resistFire > 0)
      this._Fire.color = white;
    else
      this._Fire.color = color;
    int resistCold = _scd.ResistCold;
    if (!GameManager.Instance.IsObeliskChallenge())
      resistCold += PlayerManager.Instance.GetPerkResistBonus(_scd.Id, Enums.DamageType.Cold);
    this._Cold.text = resistCold.ToString() + "%";
    if (resistCold > 0)
      this._Cold.color = white;
    else
      this._Cold.color = color;
    int resistLightning = _scd.ResistLightning;
    if (!GameManager.Instance.IsObeliskChallenge())
      resistLightning += PlayerManager.Instance.GetPerkResistBonus(_scd.Id, Enums.DamageType.Lightning);
    this._Lightning.text = resistLightning.ToString() + "%";
    if (resistLightning > 0)
      this._Lightning.color = white;
    else
      this._Lightning.color = color;
    int resistMind = _scd.ResistMind;
    if (!GameManager.Instance.IsObeliskChallenge())
      resistMind += PlayerManager.Instance.GetPerkResistBonus(_scd.Id, Enums.DamageType.Mind);
    this._Mind.text = resistMind.ToString() + "%";
    if (resistMind > 0)
      this._Mind.color = white;
    else
      this._Mind.color = color;
    int resistHoly = _scd.ResistHoly;
    if (!GameManager.Instance.IsObeliskChallenge())
      resistHoly += PlayerManager.Instance.GetPerkResistBonus(_scd.Id, Enums.DamageType.Holy);
    this._Holy.text = resistHoly.ToString() + "%";
    if (resistHoly > 0)
      this._Holy.color = white;
    else
      this._Holy.color = color;
    int resistShadow = _scd.ResistShadow;
    if (!GameManager.Instance.IsObeliskChallenge())
      resistShadow += PlayerManager.Instance.GetPerkResistBonus(_scd.Id, Enums.DamageType.Shadow);
    this._Shadow.text = resistShadow.ToString() + "%";
    if (resistShadow > 0)
      this._Shadow.color = white;
    else
      this._Shadow.color = color;
    this._Trait0RO.SetTrait(_scd.Trait0.Id, characterTier1);
    this._Trait1ARO.SetTrait(_scd.Trait1A.Id, characterTier1);
    this._Trait1BRO.SetTrait(_scd.Trait1B.Id, characterTier1);
    this._Trait2ARO.SetTrait(_scd.Trait2A.Id, characterTier1);
    this._Trait2BRO.SetTrait(_scd.Trait2B.Id, characterTier1);
    this._Trait3ARO.SetTrait(_scd.Trait3A.Id, characterTier1);
    this._Trait3BRO.SetTrait(_scd.Trait3B.Id, characterTier1);
    this._Trait4ARO.SetTrait(_scd.Trait4A.Id, characterTier1);
    this._Trait4BRO.SetTrait(_scd.Trait4B.Id, characterTier1);
    for (int index = 0; index < 7; ++index)
    {
      if (index < _scd.Cards.Length && _scd.Cards[index] != null)
      {
        string id = _scd.Cards[index].Card.Id;
        if (_scd.Cards[index].Card.Starter)
        {
          switch (characterTier3)
          {
            case 1:
              id = Globals.Instance.GetCardData(id, false).UpgradesTo1.ToLower();
              break;
            case 2:
              id = Globals.Instance.GetCardData(id, false).UpgradesTo2.ToLower();
              break;
          }
        }
        this.cardsCI[index].SetCard(id, false);
        this.cardsNumText[index].text = _scd.Cards[index].UnitsInDeck.ToString();
        this.cardsT[index].gameObject.SetActive(true);
        this.cardsNumT[index].gameObject.SetActive(true);
      }
      else
      {
        this.cardsT[index].gameObject.SetActive(false);
        this.cardsNumT[index].gameObject.SetActive(false);
      }
    }
    if ((UnityEngine.Object) this.oldSCD != (UnityEngine.Object) this.SCD)
    {
      this.oldSCD = this.SCD;
      this.SetHeroAnimated();
    }
    this.SetButtonPerkPoints();
    if (doStats)
      this.ShowStats();
    else
      this.ShowPerks();
  }

  private void SetHeroAnimated(GameObject heroSkin = null)
  {
    if ((UnityEngine.Object) this.heroAnimated != (UnityEngine.Object) null && (UnityEngine.Object) heroSkin != (UnityEngine.Object) null && this.heroAnimated.name == heroSkin.name)
      return;
    float normalizedTime1 = 0.0f;
    if ((UnityEngine.Object) this.heroAnimated != (UnityEngine.Object) null)
    {
      double normalizedTime2 = (double) this.heroAnimated.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
      normalizedTime1 = (float) normalizedTime2 - Mathf.Floor((float) normalizedTime2);
    }
    UnityEngine.Object.Destroy((UnityEngine.Object) this.heroAnimated);
    GameObject original = heroSkin;
    if ((UnityEngine.Object) heroSkin == (UnityEngine.Object) null)
    {
      SkinData skinData = Globals.Instance.GetSkinData(PlayerManager.Instance.GetActiveSkin(this.SCD.Id));
      original = !((UnityEngine.Object) skinData != (UnityEngine.Object) null) ? this.SCD.GameObjectAnimated : skinData.SkinGo;
    }
    if ((UnityEngine.Object) original != (UnityEngine.Object) null)
    {
      this._SpriteSubstitution.transform.gameObject.SetActive(false);
      this.heroAnimated = UnityEngine.Object.Instantiate<GameObject>(original, Vector3.zero, Quaternion.identity, this._HeroParent);
      this.heroAnimated.name = original.name;
      this.heroAnimated.GetComponent<BoxCollider2D>().enabled = false;
      this.heroAnim = this.heroAnimated.GetComponent<Animator>();
      this.heroAnim.enabled = false;
      this.heroAnimated.transform.localPosition = Vector3.zero;
      if (this.SCD.SubClassName.ToLower() == "warden")
      {
        this.heroAnimated.transform.localScale = new Vector3(0.92f, 0.92f, 1f);
        this.heroAnimated.transform.localPosition -= new Vector3(0.0f, 0.15f, 0.0f);
      }
      else if (this.SCD.SubClassName.ToLower() == "prophet")
        this.heroAnimated.transform.localScale = new Vector3(1f, 1f, 1f);
      else if (this.SCD.HeroClass == Enums.HeroClass.Warrior)
        this.heroAnimated.transform.localPosition -= new Vector3(0.0f, 0.15f, 0.0f);
      this.animatedSprites = new List<SpriteRenderer>();
      this.animatedSpritesOutOfCharacter = new List<SetSpriteLayerFromBase>();
      this.GetSpritesFromAnimated(this.heroAnimated);
      int num = 2000;
      if (this.animatedSprites != null)
      {
        for (int index = 0; index < this.animatedSprites.Count; ++index)
        {
          if ((UnityEngine.Object) this.animatedSprites[index] != (UnityEngine.Object) null)
          {
            this.animatedSprites[index].sortingOrder = num - index;
            this.animatedSprites[index].sortingLayerName = "UI";
          }
        }
      }
      if (this.animatedSpritesOutOfCharacter != null)
      {
        for (int index = 0; index < this.animatedSpritesOutOfCharacter.Count; ++index)
        {
          if ((UnityEngine.Object) this.animatedSpritesOutOfCharacter[index] != (UnityEngine.Object) null)
            this.animatedSpritesOutOfCharacter[index].ReOrderLayer();
        }
      }
      if ((double) normalizedTime1 > 0.0)
      {
        this.heroAnim.enabled = true;
        this.heroAnim.Play("Base Layer." + this.heroAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name, -1, normalizedTime1);
      }
      else
        this.heroAnim.enabled = true;
    }
    else
    {
      this._SpriteSubstitution.gameObject.SetActive(true);
      this._SpriteSubstitution.sprite = this.SCD.Sprite;
    }
  }

  public void GetSpritesFromAnimated(GameObject GO)
  {
    foreach (Transform transform in GO.transform)
    {
      if ((bool) (UnityEngine.Object) transform.GetComponent<SpriteRenderer>())
        this.animatedSprites.Add(transform.GetComponent<SpriteRenderer>());
      if ((UnityEngine.Object) transform.GetComponent<SetSpriteLayerFromBase>() != (UnityEngine.Object) null)
        this.animatedSpritesOutOfCharacter.Add(transform.GetComponent<SetSpriteLayerFromBase>());
      if (transform.childCount > 0)
        this.GetSpritesFromAnimated(transform.gameObject);
    }
  }

  public void SetButtonPerkPoints()
  {
    if (GameManager.Instance.IsLoadingGame() || GameManager.Instance.IsObeliskChallenge())
    {
      this.warningPerkT.gameObject.SetActive(false);
    }
    else
    {
      int perkPointsAvailable = PlayerManager.Instance.GetPerkPointsAvailable(this.SCD.Id);
      if (perkPointsAvailable > 0)
      {
        this.warningPerk.text = perkPointsAvailable.ToString();
        this.warningPerkT.gameObject.SetActive(true);
      }
      else
        this.warningPerkT.gameObject.SetActive(false);
    }
  }

  public void RepositionResolution()
  {
    this.SetPositions();
    if (this.opened)
      this.transform.position = this.destinationCenter;
    else
      this.transform.position = this.destinationOut;
  }

  public void SetPositions()
  {
    this.destinationCenter = new Vector3((float) ((double) Globals.Instance.sizeW * 0.5 - 9.6999998092651367), 0.0f, -1f);
    this.destinationOut = new Vector3((float) ((double) Globals.Instance.sizeW * 0.5 + 2.2000000476837158 * (double) Globals.Instance.scale), 0.0f, -1f);
  }

  public void HideNow() => this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -100f);

  public void Close()
  {
    if (!this.opened)
      return;
    this.SetPositions();
    this.activeId = "";
    this.moveThis = false;
    this.destination = this.destinationOut;
    this.closeThis = true;
    this.moveThis = true;
    this.opened = false;
  }

  public void Show()
  {
    if (this.opened)
      return;
    this.closeThis = false;
    this.SetPositions();
    this.destination = this.destinationCenter;
    if (!this.moveThis)
    {
      this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -1f);
      this.StartCoroutine(this.moveWindow());
    }
    this.opened = true;
    this.controllerHorizontalIndex = -1;
  }

  private IEnumerator moveWindow()
  {
    yield return (object) null;
    this.moveThis = true;
  }

  private void EnableCharButton(BotonGeneric bot, bool state)
  {
    if (state)
    {
      bot.Enable();
      bot.transform.localPosition = new Vector3(-0.4f, bot.transform.localPosition.y, bot.transform.localPosition.z);
      bot.SetBackgroundColor(new Color(0.25f, 0.25f, 0.25f, 1f));
    }
    else
    {
      bot.Disable();
      bot.transform.localPosition = new Vector3(-0.5f, bot.transform.localPosition.y, bot.transform.localPosition.z);
      bot.SetBackgroundColor(Functions.HexToColor("#F7A00D"));
      bot.ShowBackgroundDisable(false);
    }
  }

  public void ShowStats()
  {
    this.EnableCharButton(this.buttonStats, false);
    this.EnableCharButton(this.buttonSkins, true);
    this.EnableCharButton(this.buttonCardback, true);
    this.EnableCharButton(this.buttonPerks, true);
    this.EnableCharButton(this.buttonRank, true);
    if (GameManager.Instance.IsObeliskChallenge())
    {
      this.buttonPerks.Disable();
      this.buttonRank.Disable();
    }
    this.groupCharacter.gameObject.SetActive(true);
    this.groupStats.gameObject.SetActive(true);
    this.groupPerks.gameObject.SetActive(false);
    this.groupSkins.gameObject.SetActive(false);
    this.groupRank.gameObject.SetActive(false);
    this.groupCardback.gameObject.SetActive(false);
    this.perksNotAvailable.gameObject.SetActive(false);
  }

  public void ShowSkins()
  {
    this.EnableCharButton(this.buttonStats, true);
    this.EnableCharButton(this.buttonSkins, false);
    this.EnableCharButton(this.buttonCardback, true);
    this.EnableCharButton(this.buttonPerks, true);
    this.EnableCharButton(this.buttonRank, true);
    if (GameManager.Instance.IsObeliskChallenge())
    {
      this.buttonPerks.Disable();
      this.buttonRank.Disable();
    }
    this.groupCharacter.gameObject.SetActive(true);
    this.groupStats.gameObject.SetActive(false);
    this.groupPerks.gameObject.SetActive(false);
    this.groupSkins.gameObject.SetActive(true);
    this.groupRank.gameObject.SetActive(false);
    this.groupCardback.gameObject.SetActive(false);
    this.perksNotAvailable.gameObject.SetActive(false);
    this.DoSkins();
    this.ShowSkin();
  }

  public void ShowPerks() => this.DoPerks();

  public void ShowRank()
  {
    this.EnableCharButton(this.buttonStats, true);
    this.EnableCharButton(this.buttonPerks, true);
    this.EnableCharButton(this.buttonSkins, true);
    this.EnableCharButton(this.buttonRank, false);
    this.EnableCharButton(this.buttonCardback, true);
    this.groupCharacter.gameObject.SetActive(true);
    this.groupStats.gameObject.SetActive(false);
    this.groupPerks.gameObject.SetActive(false);
    this.groupSkins.gameObject.SetActive(false);
    this.groupRank.gameObject.SetActive(true);
    this.groupCardback.gameObject.SetActive(false);
    this.DoRank();
    if (GameManager.Instance.IsObeliskChallenge())
      this.perksNotAvailable.gameObject.SetActive(true);
    else
      this.perksNotAvailable.gameObject.SetActive(false);
  }

  public void ShowCardbacks()
  {
    this.EnableCharButton(this.buttonStats, true);
    this.EnableCharButton(this.buttonSkins, true);
    this.EnableCharButton(this.buttonCardback, false);
    this.EnableCharButton(this.buttonPerks, true);
    this.EnableCharButton(this.buttonRank, true);
    if (GameManager.Instance.IsObeliskChallenge())
    {
      this.buttonPerks.Disable();
      this.buttonRank.Disable();
    }
    this.groupCharacter.gameObject.SetActive(false);
    this.groupStats.gameObject.SetActive(false);
    this.groupPerks.gameObject.SetActive(false);
    this.groupSkins.gameObject.SetActive(false);
    this.groupRank.gameObject.SetActive(false);
    this.groupCardback.gameObject.SetActive(true);
    this.perksNotAvailable.gameObject.SetActive(false);
    this.DoCardbacks();
  }

  public void DoCardbacks()
  {
    foreach (Component component in this.groupCardbackGOsCharacter)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
    foreach (Component component in this.groupCardbackGOsGeneral)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
    foreach (Component component in this.groupCardbackGOsMisc)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
    string id = this.SCD.Id;
    int perkRank = PlayerManager.Instance.GetPerkRank(this.SCD.Id);
    SortedDictionary<string, CardbackData> sortedDictionary = new SortedDictionary<string, CardbackData>();
    List<string> stringList = new List<string>();
    foreach (KeyValuePair<string, CardbackData> keyValuePair in Globals.Instance.CardbackDataSource)
    {
      bool flag1 = false;
      bool flag2 = false;
      if ((UnityEngine.Object) keyValuePair.Value.CardbackSubclass == (UnityEngine.Object) null || keyValuePair.Value.CardbackSubclass.Id == this.SCD.Id)
      {
        flag1 = AtOManager.Instance.ValidCardback(keyValuePair.Value, perkRank);
        if (!flag1 && keyValuePair.Value.RankLevel > 0)
          flag2 = true;
      }
      if (flag1 || keyValuePair.Value.ShowIfLocked)
        flag2 = true;
      if (flag2)
      {
        int cardbackOrder;
        string str;
        if (keyValuePair.Value.CardbackOrder < 10)
        {
          cardbackOrder = keyValuePair.Value.CardbackOrder;
          str = "000" + cardbackOrder.ToString();
        }
        else if (keyValuePair.Value.CardbackOrder < 100)
        {
          cardbackOrder = keyValuePair.Value.CardbackOrder;
          str = "00" + cardbackOrder.ToString();
        }
        else if (keyValuePair.Value.CardbackOrder < 1000)
        {
          cardbackOrder = keyValuePair.Value.CardbackOrder;
          str = "0" + cardbackOrder.ToString();
        }
        else
        {
          cardbackOrder = keyValuePair.Value.CardbackOrder;
          str = cardbackOrder.ToString();
        }
        string key = (!keyValuePair.Value.BaseCardback ? "1" + str : "0" + str) + "_" + keyValuePair.Value.CardbackName;
        sortedDictionary.Add(key, keyValuePair.Value);
        if (flag1)
          stringList.Add(key);
      }
    }
    string activeCardback = PlayerManager.Instance.GetActiveCardback(id);
    if (activeCardback != "" && (UnityEngine.Object) Globals.Instance.GetCardbackData(activeCardback) == (UnityEngine.Object) null)
    {
      CardbackData cardbackData = Globals.Instance.GetCardbackData(Globals.Instance.GetCardbackBaseIdBySubclass(id));
      if ((UnityEngine.Object) cardbackData != (UnityEngine.Object) null)
        PlayerManager.Instance.SetCardback(id, cardbackData.CardbackId);
    }
    int num1 = 6;
    float num2 = 1.45f;
    float num3 = 2.25f;
    int num4 = 0;
    int num5 = 0;
    foreach (KeyValuePair<string, CardbackData> keyValuePair in sortedDictionary)
    {
      int num6 = int.Parse(keyValuePair.Key.Split('_', StringSplitOptions.None)[0]);
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.goCardback, Vector3.zero, Quaternion.identity);
      if (num6 < 10010)
        gameObject.transform.parent = this.groupCardbackGOsCharacter;
      else if (num6 < 10050)
      {
        if (num5 == 0)
        {
          num5 = 1;
          num4 = 0;
        }
        gameObject.transform.parent = this.groupCardbackGOsGeneral;
      }
      else
      {
        if (num5 == 1)
        {
          num5 = 2;
          num4 = 0;
        }
        gameObject.transform.parent = this.groupCardbackGOsMisc;
      }
      Vector3 vector3 = new Vector3(num2 * (float) (num4 % num1), -num3 * Mathf.Floor((float) num4 / (float) num1), 0.0f);
      gameObject.transform.localScale = new Vector3(0.95f, 0.95f, 1f);
      gameObject.transform.localPosition = vector3;
      if (stringList.Contains(keyValuePair.Key))
        gameObject.GetComponent<BotonCardback>().SetCardbackData(keyValuePair.Value.CardbackId, true, id);
      else
        gameObject.GetComponent<BotonCardback>().SetCardbackData(keyValuePair.Value.CardbackId, false, id);
      ++num4;
    }
  }

  public void DoRank()
  {
    string name = Enum.GetName(typeof (Enums.HeroClass), (object) this.SCD.HeroClass);
    string id = this.SCD.Id;
    this.perkBar.color = this.maxProgress.color = this.rankProgress.color = Functions.HexToColor(Globals.Instance.ClassColor[name]);
    int progress = PlayerManager.Instance.GetProgress(id);
    int perkPrevLevelPoints = PlayerManager.Instance.GetPerkPrevLevelPoints(id);
    int perkNextLevelPoints = PlayerManager.Instance.GetPerkNextLevelPoints(id);
    this.maxProgress.text = "<color=#FFF>" + progress.ToString() + "</color><size=-.5> / " + perkNextLevelPoints.ToString();
    int perkRank = PlayerManager.Instance.GetPerkRank(id);
    this.rankProgress.text = string.Format(Texts.Instance.GetText("rankProgress"), (object) perkRank);
    this.perkBarMask.localScale = new Vector3((float) (((double) progress - (double) perkPrevLevelPoints) / ((double) perkNextLevelPoints - (double) perkPrevLevelPoints) * 3.3650000095367432), this.perkBarMask.localScale.y, this.perkBarMask.localScale.z);
    for (int _rank = 0; _rank < this.progressionRows.Length; ++_rank)
      this.progressionRows[_rank].Init(_rank);
    int num = 0;
    for (int index = 0; index < Globals.Instance.RankLevel.Count && Globals.Instance.RankLevel[index] <= perkRank; ++index)
      ++num;
    for (int index = 0; index < num; ++index)
      this.progressionRows[index].Enable(true);
    this.DoSuppliesBlock();
  }

  public void DoSuppliesBlock()
  {
    int playerSupplyActual = PlayerManager.Instance.GetPlayerSupplyActual();
    this.useSuppliesAvailable.text = string.Format(Texts.Instance.GetText("useSuppliesAvailable"), (object) playerSupplyActual);
    this.useSuppliesButton.auxString = this.SCD.Id;
    if (playerSupplyActual < 1 || PlayerManager.Instance.GetPerkRank(this.SCD.Id) < 10 || PlayerManager.Instance.GetPerkRank(this.SCD.Id) >= 50 || PlayerManager.Instance.TotalPointsSpentInSupplys() < 276)
      this.useSuppliesButton.Disable();
    else
      this.useSuppliesButton.Enable();
  }

  private void DoPerks()
  {
    Enum.GetName(typeof (Enums.HeroClass), (object) this.SCD.HeroClass);
    PerkTree.Instance.Show(this.SCD.Id);
  }

  public void DoSkins()
  {
    List<SkinData> skinsBySubclass = Globals.Instance.GetSkinsBySubclass(this.SCD.Id);
    for (int index = 0; index < this.botonSkinBase.Length; ++index)
      this.botonSkinBase[index].gameObject.SetActive(false);
    StringBuilder stringBuilder = new StringBuilder();
    SortedDictionary<string, SkinData> sortedDictionary = new SortedDictionary<string, SkinData>();
    for (int index = 0; index < skinsBySubclass.Count; ++index)
    {
      stringBuilder.Clear();
      if (skinsBySubclass[index].BaseSkin)
        stringBuilder.Append("0");
      else
        stringBuilder.Append("1");
      stringBuilder.Append(skinsBySubclass[index].SkinOrder.ToString("D2"));
      stringBuilder.Append(skinsBySubclass[index].SkinId.ToLower());
      sortedDictionary.Add(stringBuilder.ToString(), skinsBySubclass[index]);
    }
    int index1 = 0;
    foreach (KeyValuePair<string, SkinData> keyValuePair in sortedDictionary)
    {
      this.botonSkinBase[index1].SetSkinData(keyValuePair.Value);
      this.botonSkinBase[index1].gameObject.SetActive(true);
      ++index1;
    }
  }

  public void ShowSkin(GameObject _skin = null)
  {
    if ((UnityEngine.Object) _skin == (UnityEngine.Object) null)
    {
      string id = "";
      if ((UnityEngine.Object) this.SCD != (UnityEngine.Object) null)
        id = PlayerManager.Instance.GetActiveSkin(this.SCD.Id);
      if (id == "")
        return;
      SkinData skinData = Globals.Instance.GetSkinData(id);
      if ((UnityEngine.Object) skinData == (UnityEngine.Object) null)
        return;
      this.SetHeroAnimated(skinData.SkinGo);
    }
    else
      this.SetHeroAnimated(_skin);
  }

  public void ResetSkin() => this.ShowSkin();

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    int absolutePosition = -1)
  {
    this._controllerList.Clear();
    this._controllerList.Add(this.buttonStats.transform);
    this._controllerList.Add(this.buttonRank.transform);
    this._controllerList.Add(this.buttonPerks.transform);
    this._controllerList.Add(this.buttonSkins.transform);
    this._controllerList.Add(this.buttonCardback.transform);
    if (Functions.TransformIsVisible(this.groupStats))
    {
      this._controllerList.Add(this._Trait0.transform);
      this._controllerList.Add(this._Trait1A.transform);
      this._controllerList.Add(this._Trait1B.transform);
      this._controllerList.Add(this._Trait2A.transform);
      this._controllerList.Add(this._Trait2B.transform);
      this._controllerList.Add(this._Trait3A.transform);
      this._controllerList.Add(this._Trait3B.transform);
      this._controllerList.Add(this._Trait4A.transform);
      this._controllerList.Add(this._Trait4B.transform);
      this._controllerList.Add(this.cardItemT);
      foreach (Transform transform in this._CardParent)
      {
        if ((bool) (UnityEngine.Object) transform.GetComponent<CardItem>() && Functions.TransformIsVisible(transform))
          this._controllerList.Add(transform);
      }
    }
    else if (Functions.TransformIsVisible(this.groupRank))
    {
      if (Functions.TransformIsVisible(this.useSuppliesButton.transform))
        this._controllerList.Add(this.useSuppliesButton.transform);
    }
    else if (Functions.TransformIsVisible(this.groupSkins))
    {
      for (int index = 0; index < this.botonSkinBase.Length; ++index)
      {
        if (Functions.TransformIsVisible(this.botonSkinBase[index].transform))
          this._controllerList.Add(this.botonSkinBase[index].transform);
      }
    }
    else if (Functions.TransformIsVisible(this.groupCardback))
    {
      foreach (Transform transform in this.groupCardbackGOsCharacter)
      {
        if ((bool) (UnityEngine.Object) transform.GetComponent<BotonCardback>() && Functions.TransformIsVisible(transform))
          this._controllerList.Add(transform);
      }
      foreach (Transform transform in this.groupCardbackGOsGeneral)
      {
        if ((bool) (UnityEngine.Object) transform.GetComponent<BotonCardback>() && Functions.TransformIsVisible(transform))
          this._controllerList.Add(transform);
      }
      foreach (Transform transform in this.groupCardbackGOsMisc)
      {
        if ((bool) (UnityEngine.Object) transform.GetComponent<BotonCardback>() && Functions.TransformIsVisible(transform))
          this._controllerList.Add(transform);
      }
    }
    this._controllerList.Add(this.buttonClose.transform);
    if (this.controllerHorizontalIndex <= -1)
    {
      this.controllerHorizontalIndex = 0;
    }
    else
    {
      if (this.controllerHorizontalIndex > this._controllerList.Count - 1)
        this.controllerHorizontalIndex = this._controllerList.Count - 1;
      Vector3 position = this._controllerList[this.controllerHorizontalIndex].position;
      bool flag = false;
      int num1 = 0;
      int controllerHorizontalIndex = this.controllerHorizontalIndex;
      while (!flag && num1 < 20)
      {
        float num2 = (float) (num1 + 1) * 0.5f;
        this.controllerHorizontalIndex = controllerHorizontalIndex;
        if (goingDown)
        {
          this.controllerHorizontalIndex = Functions.GetClosestIndexFromList(this._controllerList[this.controllerHorizontalIndex], this._controllerList, this.controllerHorizontalIndex, new Vector3(0.0f, -num2, 0.0f));
          if (this.controllerHorizontalIndex > -1 && (double) position.y - (double) this._controllerList[this.controllerHorizontalIndex].position.y > 0.25)
            flag = true;
        }
        else if (goingUp)
        {
          this.controllerHorizontalIndex = Functions.GetClosestIndexFromList(this._controllerList[this.controllerHorizontalIndex], this._controllerList, this.controllerHorizontalIndex, new Vector3(0.0f, num2, 0.0f));
          if (this.controllerHorizontalIndex > -1 && (double) this._controllerList[this.controllerHorizontalIndex].position.y - (double) position.y > 0.25)
            flag = true;
        }
        else if (goingRight)
        {
          if (this.controllerHorizontalIndex < 5 && Functions.TransformIsVisible(this.groupStats))
          {
            this.controllerHorizontalIndex = 5;
            flag = true;
          }
          else if (this.controllerHorizontalIndex < 5 && Functions.TransformIsVisible(this.groupRank))
          {
            this.controllerHorizontalIndex = 5;
            flag = true;
          }
          else
          {
            this.controllerHorizontalIndex = Functions.GetClosestIndexFromList(this._controllerList[this.controllerHorizontalIndex], this._controllerList, this.controllerHorizontalIndex, new Vector3(num2, 0.0f, 0.0f));
            if (this.controllerHorizontalIndex > -1 && (double) this._controllerList[this.controllerHorizontalIndex].position.x - (double) position.x > 0.5)
              flag = true;
          }
        }
        else if (goingLeft)
        {
          this.controllerHorizontalIndex = Functions.GetClosestIndexFromList(this._controllerList[this.controllerHorizontalIndex], this._controllerList, this.controllerHorizontalIndex, new Vector3(-num2, 0.0f, 0.0f));
          if (this.controllerHorizontalIndex > -1 && (double) position.x - (double) this._controllerList[this.controllerHorizontalIndex].position.x > 0.5)
            flag = true;
          else if (num1 > 15)
          {
            this.controllerHorizontalIndex = 0;
            flag = true;
          }
        }
        if (!flag)
          ++num1;
      }
      if (!flag)
        this.controllerHorizontalIndex = controllerHorizontalIndex;
    }
    if (this.controllerHorizontalIndex > this._controllerList.Count - 1)
      this.controllerHorizontalIndex = this._controllerList.Count - 1;
    else if (this.controllerHorizontalIndex < 0)
      this.controllerHorizontalIndex = 0;
    if (!((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }
}
