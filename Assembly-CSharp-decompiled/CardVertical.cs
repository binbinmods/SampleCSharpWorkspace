// Decompiled with JetBrains decompiler
// Type: CardVertical
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardVertical : MonoBehaviour
{
  private Hero currentHero;
  public Transform button;
  public Transform iconlock;
  public TMP_Text name;
  public TMP_Text nameOver;
  public Transform energyT;
  public TMP_Text energy;
  public Image bg;
  public Image bgClass;
  public Image bgRare;
  public Image skill;
  public Sprite bgPlain;
  public Sprite bgOver;
  public Sprite bgActive;
  public Transform rarityT;
  public ParticleSystem particleA;
  public ParticleSystem particleB;
  public ParticleSystem particleRare;
  private bool active;
  private CardItem CI;
  public CardData cardData;
  private string cardName;
  private string internalId;
  private int cardIndex;
  private Color colorA;
  private Color colorB;
  private Color colorRare;
  private Color colorActive;

  private void Awake()
  {
    this.CI = this.GetComponent<CardItem>();
    this.colorA = Functions.HexToColor("#67BCECFF");
    this.colorB = Functions.HexToColor("#FFB410FF");
    this.colorRare = Functions.HexToColor("#DA73FFFF");
    this.colorActive = Functions.HexToColor("#333333FF");
  }

  private void Start()
  {
    if (CardScreenManager.Instance.IsActive())
    {
      if (this.button.gameObject.activeSelf)
        return;
      this.button.gameObject.SetActive(true);
    }
    else
    {
      if (this.button.gameObject.activeSelf)
        this.button.gameObject.SetActive(false);
      this.GetComponent<BoxCollider2D>().enabled = true;
    }
  }

  public void ShowLock(bool state, bool paintItBlack = true)
  {
    this.iconlock.gameObject.SetActive(state);
    if (state & paintItBlack)
      this.bgClass.color = new Color(0.2f, 0.2f, 0.2f);
    else
      this.bgClass.color = Functions.HexToColor(Globals.Instance.ClassColor[Enum.GetName(typeof (Enums.CardClass), (object) this.cardData.CardClass)]);
  }

  public void SetBgColor(string _theColor) => this.bgClass.color = Functions.HexToColor(_theColor);

  public void SetCard(string _cardId, int _cardType = 0, Hero _hero = null)
  {
    this.currentHero = _hero;
    this.internalId = _cardId;
    this.cardData = Globals.Instance.GetCardData(_cardId.Split('_', StringSplitOptions.None)[0]);
    if ((UnityEngine.Object) this.cardData == (UnityEngine.Object) null)
      return;
    if (!_cardId.Contains("_"))
      _cardId += "_0";
    this.cardIndex = int.Parse(_cardId.Split('_', StringSplitOptions.None)[1]);
    if (this.cardData.CardUpgraded == Enums.CardUpgraded.Rare)
    {
      this.name.color = this.colorRare;
      this.name.text = Globals.Instance.GetCardData(this.cardData.UpgradedFrom, false).CardName;
    }
    else if (this.cardData.CardUpgraded == Enums.CardUpgraded.A)
    {
      this.name.color = this.colorA;
      this.name.text = Globals.Instance.GetCardData(this.cardData.UpgradedFrom, false).CardName;
    }
    else if (this.cardData.CardUpgraded == Enums.CardUpgraded.B)
    {
      this.name.color = this.colorB;
      this.name.text = Globals.Instance.GetCardData(this.cardData.UpgradedFrom, false).CardName;
    }
    else
      this.name.text = this.cardData.CardName;
    this.nameOver.text = this.name.text;
    this.energy.text = AtOManager.Instance.GetHero(0) == null ? this.cardData.EnergyCost.ToString() : AtOManager.Instance.GetHero(0).GetCardFinalCost(this.cardData).ToString();
    this.skill.sprite = this.cardData.Sprite;
    if (!this.cardData.FlipSprite)
    {
      if ((double) this.skill.transform.localScale.x < 0.0)
        this.skill.transform.localScale = new Vector3(-1f * this.skill.transform.localScale.x, this.skill.transform.localScale.y, this.skill.transform.localScale.z);
    }
    else if (this.cardData.FlipSprite && (double) this.skill.transform.localScale.x > 0.0)
      this.skill.transform.localScale = new Vector3(-1f * this.skill.transform.localScale.x, this.skill.transform.localScale.y, this.skill.transform.localScale.z);
    this.bgClass.color = Functions.HexToColor(Globals.Instance.ClassColor[Enum.GetName(typeof (Enums.CardClass), (object) this.cardData.CardClass)]);
    if (this.cardData.Playable)
    {
      if (!this.energyT.gameObject.activeSelf)
        this.energyT.gameObject.SetActive(true);
    }
    else if (this.energyT.gameObject.activeSelf)
      this.energyT.gameObject.SetActive(false);
    if (this.cardData.CardRarity == Enums.CardRarity.Uncommon)
    {
      if (!this.rarityT.gameObject.activeSelf)
        this.rarityT.gameObject.SetActive(true);
      if (!this.rarityT.GetChild(0).gameObject.activeSelf)
        this.rarityT.GetChild(0).gameObject.SetActive(true);
      if (!this.rarityT.GetChild(1).gameObject.activeSelf)
        this.rarityT.GetChild(1).gameObject.SetActive(true);
    }
    else if (this.cardData.CardRarity == Enums.CardRarity.Rare)
    {
      if (!this.rarityT.gameObject.activeSelf)
        this.rarityT.gameObject.SetActive(true);
      if (!this.rarityT.GetChild(0).gameObject.activeSelf)
        this.rarityT.GetChild(0).gameObject.SetActive(true);
      if (!this.rarityT.GetChild(2).gameObject.activeSelf)
        this.rarityT.GetChild(2).gameObject.SetActive(true);
    }
    else if (this.cardData.CardRarity == Enums.CardRarity.Epic)
    {
      if (!this.rarityT.gameObject.activeSelf)
        this.rarityT.gameObject.SetActive(true);
      if (!this.rarityT.GetChild(0).gameObject.activeSelf)
        this.rarityT.GetChild(0).gameObject.SetActive(true);
      if (!this.rarityT.GetChild(3).gameObject.activeSelf)
        this.rarityT.GetChild(3).gameObject.SetActive(true);
    }
    else if (this.cardData.CardRarity == Enums.CardRarity.Mythic)
    {
      if (!this.rarityT.gameObject.activeSelf)
        this.rarityT.gameObject.SetActive(true);
      if (!this.rarityT.GetChild(0).gameObject.activeSelf)
        this.rarityT.GetChild(0).gameObject.SetActive(true);
      if (!this.rarityT.GetChild(4).gameObject.activeSelf)
        this.rarityT.GetChild(4).gameObject.SetActive(true);
    }
    else if (this.rarityT.gameObject.activeSelf)
      this.rarityT.gameObject.SetActive(false);
    if (!this.bgRare.gameObject.activeSelf)
      return;
    this.bgRare.gameObject.SetActive(false);
  }

  public void SetCardData(CardData _cardData) => this.SetCard(_cardData.Id + "_" + this.cardIndex.ToString(), _hero: this.currentHero);

  public void ReplaceWithCard(CardData _cardData, string type, bool showParticles = true)
  {
    this.SetCardData(_cardData);
    this.StartCoroutine(this.SetNameOverCo(type, showParticles));
  }

  public void PlayParticle(string type)
  {
    if (type == "A")
      this.particleA.Play();
    else
      this.particleB.Play();
  }

  private IEnumerator SetNameOverCo(string type, bool showParticles = true)
  {
    switch (type)
    {
      case "A":
        this.nameOver.color = this.colorA;
        if (showParticles)
        {
          this.particleA.Play();
          break;
        }
        break;
      case "B":
        this.nameOver.color = this.colorB;
        if (showParticles)
        {
          this.particleB.Play();
          break;
        }
        break;
      case "RARE":
        this.nameOver.color = this.colorRare;
        if (showParticles)
        {
          this.particleRare.Play();
          break;
        }
        break;
    }
    if (!this.nameOver.gameObject.activeSelf)
      this.nameOver.gameObject.SetActive(true);
    if (this.name.gameObject.activeSelf)
      this.name.gameObject.SetActive(false);
    yield return (object) Globals.Instance.WaitForSeconds(1.2f);
    if (this.active)
      this.ClearActive();
  }

  private void SetActive()
  {
    CardCraftManager.Instance.ClearActiveCard();
    CardCraftManager.Instance.SetActiveCard(this);
    this.bg.sprite = this.bgActive;
    this.bgClass.color = this.colorActive;
    this.active = true;
    this.transform.localPosition = new Vector3(0.15f, this.transform.localPosition.y, this.transform.localPosition.z);
  }

  public void ClearActive()
  {
    this.active = false;
    this.bg.sprite = this.bgPlain;
    this.bgClass.color = Functions.HexToColor(Globals.Instance.ClassColor[Enum.GetName(typeof (Enums.CardClass), (object) this.cardData.CardClass)]);
    this.transform.localPosition = new Vector3(0.0f, this.transform.localPosition.y, this.transform.localPosition.z);
  }

  public bool IsLocked() => this.iconlock.gameObject.activeSelf;

  public void OnMouseUp()
  {
    if (this.button.gameObject.activeSelf || this.iconlock.gameObject.activeSelf)
      return;
    this.fMouseUp();
  }

  public void fMouseUp()
  {
    if (AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || TomeManager.Instance.IsActive() || CardScreenManager.Instance.IsActive() || CardCraftManager.Instance.blocked || CardCraftManager.Instance.craftType != 0 && CardCraftManager.Instance.craftType != 1 && CardCraftManager.Instance.craftType != 6)
      return;
    this.SetActive();
    CardCraftManager.Instance.SelectCard(this.internalId);
    this.CI.DestroyReveleadOutside();
  }

  public void OnMouseExit()
  {
    if (this.button.gameObject.activeSelf)
      return;
    this.fMouseExit();
  }

  public void fMouseExit()
  {
    GameManager.Instance.CleanTempContainer();
    if (this.active)
      return;
    this.bg.sprite = this.bgPlain;
  }

  public void OnMouseEnter()
  {
    if (this.button.gameObject.activeSelf)
      return;
    this.fMouseEnter();
  }

  public void fMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || CardScreenManager.Instance.IsActive() && this.transform.parent.name != "VerticalContainer_Canvas")
      return;
    this.CI.cardoutsideverticallist = true;
    this.CI.CreateAmplifyOutsideCard(this.cardData, this.GetComponent<BoxCollider2D>(), this.currentHero);
    if (this.active)
      return;
    GameManager.Instance.PlayLibraryAudio("castnpccardfast");
    this.bg.sprite = this.bgOver;
  }
}
