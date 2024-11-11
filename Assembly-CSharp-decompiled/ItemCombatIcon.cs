// Decompiled with JetBrains decompiler
// Type: ItemCombatIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemCombatIcon : MonoBehaviour
{
  public string itemType;
  public Sprite[] spriteBackgroundList;
  public Sprite[] spriteBackgroundHoverList;
  public SpriteRenderer spriteBackground;
  public SpriteRenderer spriteBackgroundHover;
  public SpriteRenderer spriteBackgroundHoverBorder;
  public SpriteRenderer spriteBorder;
  public Transform rareParticles;
  public TMP_Text timesExecuted;
  public Transform animatedUse;
  public TMP_Text animatedUseText;
  private Transform iconOff;
  private Transform iconOn;
  private SpriteRenderer iconSpr;
  private Vector3 vectorDestination;
  private float movementSpeed = 0.2f;
  private CardData cardData;
  private CardItem CI;
  private BoxCollider2D collider;
  private GameObject card;
  private Coroutine showCardCo;
  private bool mouseIsOver;
  private Hero theHero;
  private NPC theNPC;

  public Hero TheHero
  {
    get => this.theHero;
    set => this.theHero = value;
  }

  public NPC TheNPC
  {
    get => this.theNPC;
    set => this.theNPC = value;
  }

  private void Awake()
  {
    this.iconOn = this.transform.GetChild(0).transform;
    this.iconOff = this.transform.GetChild(1).transform;
    this.iconSpr = this.iconOn.GetChild(0).transform.GetComponent<SpriteRenderer>();
    this.CI = this.GetComponent<CardItem>();
    this.collider = this.GetComponent<BoxCollider2D>();
    this.animatedUse.gameObject.SetActive(false);
  }

  private void Start()
  {
    if ((Object) MatchManager.Instance != (Object) null && this.transform.parent.gameObject.name != "TomeItemIcons")
    {
      string lower = this.gameObject.name.ToLower();
      if (lower != "corruptionicon" && lower != "enchantment" && lower != "enchantment2" && lower != "enchantment3")
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, (float) (((double) Globals.Instance.sizeH * 0.5 - 4.5) * -2.0), this.transform.localPosition.z);
    }
    this.SetBackground();
  }

  public void SetActivated() => this.StartCoroutine(this.SetActivatedCo());

  private IEnumerator SetActivatedCo()
  {
    GameManager.Instance.PlayLibraryAudio("ui_item_usedcharge");
    this.animatedUse.gameObject.SetActive(false);
    this.animatedUse.gameObject.SetActive(true);
    this.animatedUseText.text = Texts.Instance.GetText("itemActivated");
    yield return (object) null;
  }

  public void SetTimesExecuted(int times, bool doAnim = true)
  {
    if ((Object) this.animatedUse == (Object) null)
      return;
    this.animatedUse.gameObject.SetActive(false);
    if (times < 0)
      times = 0;
    if (!((Object) this.cardData != (Object) null))
      return;
    int num = 0;
    if ((Object) this.cardData.Item != (Object) null)
      num = this.cardData.Item.TimesPerCombat <= 0 ? this.cardData.Item.TimesPerTurn : this.cardData.Item.TimesPerCombat;
    else if ((Object) this.cardData.ItemEnchantment != (Object) null && (Object) MatchManager.Instance != (Object) null && this.cardData.ItemEnchantment.DestroyAfterUses > 0)
    {
      num = this.cardData.ItemEnchantment.DestroyAfterUses;
      if (this.theHero != null)
        times = MatchManager.Instance.EnchantmentExecutedTimes(this.theHero.Id, this.cardData.ItemEnchantment.Id);
      else if (this.theNPC != null)
        times = MatchManager.Instance.EnchantmentExecutedTimes(this.theNPC.Id, this.cardData.ItemEnchantment.Id);
    }
    if (num == 0)
    {
      if (!this.timesExecuted.gameObject.activeSelf)
        return;
      this.timesExecuted.gameObject.SetActive(false);
    }
    else
    {
      if (!this.timesExecuted.gameObject.activeSelf)
        this.timesExecuted.gameObject.SetActive(true);
      StringBuilder stringBuilder = new StringBuilder();
      if (times == num)
      {
        stringBuilder.Append("<color=#F3404E>");
        stringBuilder.Append(0);
        stringBuilder.Append("</color>");
      }
      else if (times > 0 && times < num)
      {
        stringBuilder.Append("<color=#FFFFFF>");
        stringBuilder.Append(num - times);
        stringBuilder.Append("</color>");
      }
      else
        stringBuilder.Append(num);
      stringBuilder.Append("/");
      stringBuilder.Append(num);
      this.timesExecuted.text = stringBuilder.ToString();
      if (times != 0 & doAnim && !this.animatedUse.gameObject.activeSelf)
        this.animatedUse.gameObject.SetActive(true);
    }
  }

  private void SetBackground()
  {
    if (this.itemType == "weapon")
    {
      this.spriteBackground.sprite = this.spriteBackgroundList[0];
      this.spriteBackgroundHover.sprite = this.spriteBackgroundList[0];
    }
    else if (this.itemType == "armor")
    {
      this.spriteBackground.sprite = this.spriteBackgroundList[1];
      this.spriteBackgroundHover.sprite = this.spriteBackgroundList[1];
    }
    else if (this.itemType == "jewelry")
    {
      this.spriteBackground.sprite = this.spriteBackgroundList[2];
      this.spriteBackgroundHover.sprite = this.spriteBackgroundList[2];
    }
    else if (this.itemType == "accesory")
    {
      this.spriteBackground.sprite = this.spriteBackgroundList[3];
      this.spriteBackgroundHover.sprite = this.spriteBackgroundList[3];
    }
    else if (this.itemType == "pet")
    {
      this.spriteBackground.sprite = this.spriteBackgroundList[4];
      this.spriteBackgroundHover.sprite = this.spriteBackgroundList[4];
    }
    if (!((Object) MatchManager.Instance != (Object) null))
      return;
    string str = "Background";
    if (this.transform.parent.gameObject.name == "TomeItemIcons")
      str = "UI";
    else if (this.gameObject.name == "enchantment" || this.gameObject.name == "enchantment2" || this.gameObject.name == "enchantment3")
    {
      str = "Default";
      this.itemType = "enchantment";
    }
    this.spriteBackground.sortingLayerName = str;
    this.spriteBackgroundHover.sortingLayerName = str;
    this.spriteBackgroundHoverBorder.sortingLayerName = str;
    this.spriteBorder.sortingLayerName = str;
    this.iconSpr.sortingLayerName = str;
    this.timesExecuted.GetComponent<MeshRenderer>().sortingLayerName = str;
    if (!this.rareParticles.gameObject.activeSelf)
      return;
    this.rareParticles.gameObject.SetActive(false);
  }

  public void MoveIn(string type, float delay, Hero _hero)
  {
    this.animatedUse.gameObject.SetActive(false);
    this.theHero = _hero;
    this.gameObject.SetActive(true);
    this.timesExecuted.text = "";
    this.StartCoroutine(this.MoveInCo(type, delay));
  }

  private IEnumerator MoveInCo(string type, float delay)
  {
    ItemCombatIcon itemCombatIcon = this;
    itemCombatIcon.ShowIcon(type);
    yield return (object) Globals.Instance.WaitForSeconds(delay);
    float y = 0.0f;
    itemCombatIcon.transform.localPosition = new Vector3(itemCombatIcon.transform.localPosition.x, -4.5f, itemCombatIcon.transform.localPosition.z);
    itemCombatIcon.vectorDestination = new Vector3(itemCombatIcon.transform.localPosition.x, y, itemCombatIcon.transform.localPosition.z);
    while ((double) Vector3.Distance(itemCombatIcon.transform.localPosition, itemCombatIcon.vectorDestination) > 0.05000000074505806)
    {
      itemCombatIcon.transform.localPosition = Vector3.Lerp(itemCombatIcon.transform.localPosition, itemCombatIcon.vectorDestination, itemCombatIcon.movementSpeed);
      yield return (object) null;
    }
    itemCombatIcon.transform.localPosition = itemCombatIcon.vectorDestination;
  }

  public void MoveOut(float delay)
  {
    if (!this.transform.gameObject.activeSelf)
      return;
    this.StartCoroutine(this.MoveOutCo(delay));
  }

  private IEnumerator MoveOutCo(float delay)
  {
    ItemCombatIcon itemCombatIcon = this;
    yield return (object) Globals.Instance.WaitForSeconds(delay);
    float y = (float) (((double) Globals.Instance.sizeH * 0.5 - 4.5) * -2.0);
    itemCombatIcon.vectorDestination = new Vector3(itemCombatIcon.transform.localPosition.x, y, itemCombatIcon.transform.localPosition.z);
    while ((double) Vector3.Distance(itemCombatIcon.transform.localPosition, itemCombatIcon.vectorDestination) > 0.05000000074505806)
    {
      itemCombatIcon.transform.localPosition = Vector3.Lerp(itemCombatIcon.transform.localPosition, itemCombatIcon.vectorDestination, itemCombatIcon.movementSpeed * 2f);
      yield return (object) null;
    }
    itemCombatIcon.transform.localPosition = itemCombatIcon.vectorDestination - new Vector3(0.0f, 100f, 0.0f);
    itemCombatIcon.animatedUse.gameObject.SetActive(false);
  }

  public void ShowIconCorruption(CardData cData)
  {
    this.cardData = cData;
    this.collider.enabled = true;
    this.iconSpr.sprite = this.cardData.Sprite;
    this.iconOn.gameObject.SetActive(true);
    this.iconOff.gameObject.SetActive(false);
  }

  public void ShowIconExternal(string type, Character _character)
  {
    if (_character == null)
      return;
    string cardItem = "";
    switch (type)
    {
      case "weapon":
        cardItem = _character.Weapon;
        break;
      case "armor":
        cardItem = _character.Armor;
        break;
      case "jewelry":
        cardItem = _character.Jewelry;
        break;
      case "accesory":
        cardItem = _character.Accesory;
        break;
      case "pet":
        cardItem = _character.Pet;
        break;
      case "enchantment":
        cardItem = _character.Enchantment;
        break;
      case "enchantment2":
        cardItem = _character.Enchantment2;
        break;
      case "enchantment3":
        cardItem = _character.Enchantment3;
        break;
    }
    if (cardItem != "")
    {
      if (_character.IsHero)
      {
        this.theHero = (Hero) _character;
        if (!this.theHero.Alive)
          return;
      }
      else
      {
        this.theNPC = (NPC) _character;
        if (!this.theNPC.Alive)
          return;
      }
    }
    this.ShowIconFunc(cardItem);
  }

  public void ShowIcon(string type, string itemId = "", bool fromTome = false)
  {
    string cardItem = "";
    if ((bool) (Object) MatchManager.Instance && !fromTome)
    {
      Hero heroHeroActive = MatchManager.Instance.GetHeroHeroActive();
      if (heroHeroActive == null)
        return;
      switch (type)
      {
        case "weapon":
          cardItem = heroHeroActive.Weapon;
          break;
        case "armor":
          cardItem = heroHeroActive.Armor;
          break;
        case "jewelry":
          cardItem = heroHeroActive.Jewelry;
          break;
        case "accesory":
          cardItem = heroHeroActive.Accesory;
          break;
        case "pet":
          cardItem = heroHeroActive.Pet;
          break;
      }
      this.ShowIconFunc(cardItem);
    }
    else
      this.ShowIconFunc(itemId);
  }

  private void ShowIconFunc(string cardItem)
  {
    if (cardItem == "")
    {
      this.cardData = (CardData) null;
      this.collider.enabled = false;
      this.iconOn.gameObject.SetActive(false);
      this.iconOff.gameObject.SetActive(true);
      this.rareParticles.gameObject.SetActive(false);
    }
    else
    {
      this.cardData = Globals.Instance.GetCardData(cardItem, false);
      if ((Object) this.cardData == (Object) null)
        return;
      if ((bool) (Object) MatchManager.Instance && this.theHero != null && (Object) this.cardData.Item != (Object) null && this.cardData.Item.TimesPerCombat > 0)
        this.SetTimesExecuted(MatchManager.Instance.ItemExecutedInThisCombat(this.theHero.Id, cardItem), false);
      else
        this.SetTimesExecuted(0, false);
      this.collider.enabled = true;
      this.iconSpr.sprite = this.cardData.Sprite;
      this.iconOn.gameObject.SetActive(true);
      this.iconOff.gameObject.SetActive(false);
      if (this.rareParticles.gameObject.activeSelf)
        this.rareParticles.gameObject.SetActive(false);
      if (this.cardData.CardUpgraded != Enums.CardUpgraded.Rare)
        return;
      if (this.transform.parent.gameObject.name == "ItemIcons")
      {
        if (!this.rareParticles.gameObject.activeSelf)
          return;
        this.rareParticles.gameObject.SetActive(false);
      }
      else
      {
        if (this.rareParticles.gameObject.activeSelf)
          return;
        this.rareParticles.gameObject.SetActive(true);
      }
    }
  }

  public void DoHover(bool state)
  {
    if (state)
    {
      if (this.showCardCo != null)
        this.StopCoroutine(this.showCardCo);
      if (this.gameObject.activeSelf && this.gameObject.activeInHierarchy)
        this.showCardCo = this.StartCoroutine(this.ShowCardCo());
    }
    else
    {
      if (this.showCardCo != null)
        this.StopCoroutine(this.showCardCo);
      if ((Object) this.CI != (Object) null)
        this.CI.HideKeyNotes();
      if ((Object) this.card != (Object) null)
      {
        Object.Destroy((Object) this.card);
        this.CI = (CardItem) null;
      }
    }
    if (!((Object) this.cardData == (Object) null) && this.cardData.CardType == Enums.CardType.Corruption)
      return;
    this.spriteBackgroundHover.gameObject.SetActive(state);
  }

  public void StopCardAnimation()
  {
    if (this.showCardCo == null)
      return;
    this.StopCoroutine(this.showCardCo);
  }

  private IEnumerator ShowCardCo()
  {
    ItemCombatIcon itemCombatIcon = this;
    if (!((Object) itemCombatIcon.cardData == (Object) null))
    {
      bool isCorruptionOrEnchantment = false;
      Vector3 offsetDestination;
      if ((Object) MatchManager.Instance != (Object) null && itemCombatIcon.transform.parent.gameObject.name != "TomeItemIcons")
      {
        string lower = itemCombatIcon.gameObject.name.ToLower();
        if (lower == "corruptionicon" || lower == "enchantment" || lower == "enchantment2" || lower == "enchantment3")
        {
          offsetDestination = new Vector3(0.0f, -2.1f, 0.0f);
          isCorruptionOrEnchantment = false;
        }
        else
        {
          offsetDestination = new Vector3(0.0f, 2.1f, 0.0f);
          if (itemCombatIcon.itemType == "pet")
            offsetDestination -= new Vector3(0.4f, 0.0f, 0.0f);
        }
      }
      else
        offsetDestination = !((Object) LootManager.Instance != (Object) null) ? new Vector3(-2.1f, 0.0f, 0.0f) : new Vector3(0.0f, 1.95f, 0.0f);
      yield return (object) Globals.Instance.WaitForSeconds(0.02f);
      Object.Destroy((Object) itemCombatIcon.card);
      Transform parent = GameManager.Instance.TempContainer;
      if ((Object) MatchManager.Instance != (Object) null)
        parent = MatchManager.Instance.amplifiedTransform;
      itemCombatIcon.card = Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, parent);
      itemCombatIcon.card.name = itemCombatIcon.cardData.Id;
      itemCombatIcon.CI = itemCombatIcon.card.GetComponent<CardItem>();
      itemCombatIcon.CI.SetCard(itemCombatIcon.cardData.Id, _theHero: itemCombatIcon.theHero, _theNPC: itemCombatIcon.theNPC, GetFromGlobal: true);
      itemCombatIcon.CI.DisableTrail();
      if ((bool) (Object) LootManager.Instance)
        itemCombatIcon.CI.TopLayeringOrder("UI", 32000);
      else
        itemCombatIcon.CI.TopLayeringOrder("UI", 32000);
      if (!isCorruptionOrEnchantment)
        itemCombatIcon.CI.DisableCollider();
      else
        itemCombatIcon.CI.EnableCollider();
      itemCombatIcon.card.transform.position = itemCombatIcon.transform.position + new Vector3(0.0f, 0.4f, 0.0f);
      itemCombatIcon.CI.SetDestinationScaleRotation(itemCombatIcon.transform.position + offsetDestination, 1.2f, Quaternion.Euler(0.0f, 0.0f, 0.0f));
      if (itemCombatIcon.mouseIsOver)
        itemCombatIcon.CI.ShowKeyNotes(true);
      if ((Object) LootManager.Instance != (Object) null)
        itemCombatIcon.CI.SetDestinationLocalScale(1.2f);
      else if ((Object) CardCraftManager.Instance != (Object) null)
        itemCombatIcon.CI.SetDestinationLocalScale(1.4f);
      itemCombatIcon.CI.active = true;
    }
  }

  private void ShowCardScreenManager()
  {
    if (!((Object) CardScreenManager.Instance != (Object) null))
      return;
    CardScreenManager.Instance.ShowCardScreen(true);
    CardScreenManager.Instance.SetCardData(this.cardData);
  }

  private void OnMouseEnter()
  {
    if (SettingsManager.Instance.IsActive() || AlertManager.Instance.IsActive() || (bool) (Object) MatchManager.Instance && MatchManager.Instance.CardDrag || (bool) (Object) MatchManager.Instance && EventSystem.current.IsPointerOverGameObject() && !TomeManager.Instance.IsActive() || CardScreenManager.Instance.IsActive())
      return;
    this.mouseIsOver = true;
    this.DoHover(true);
    GameManager.Instance.SetCursorHover();
    GameManager.Instance.PlayLibraryAudio("ui_menu_popup_01");
  }

  private void OnMouseExit()
  {
    if ((bool) (Object) MatchManager.Instance && MatchManager.Instance.CardDrag || (bool) (Object) MatchManager.Instance && EventSystem.current.IsPointerOverGameObject() && !TomeManager.Instance.IsActive())
      return;
    this.mouseIsOver = false;
    this.DoHover(false);
    GameManager.Instance.SetCursorPlain();
  }

  public void OnMouseUp()
  {
    if (!Functions.ClickedThisTransform(this.transform))
      return;
    this.fOnMouseUp();
  }

  private void fOnMouseUp()
  {
    if (SettingsManager.Instance.IsActive() || AlertManager.Instance.IsActive() || (bool) (Object) MatchManager.Instance && (MatchManager.Instance.CardDrag || EventSystem.current.IsPointerOverGameObject()))
      return;
    this.ShowCardScreenManager();
  }

  private void OnMouseOver()
  {
    if ((Object) MatchManager.Instance != (Object) null)
      MatchManager.Instance.amplifiedTransformShow = true;
    if (!Input.GetMouseButtonUp(1))
      return;
    this.fOnMouseUp();
  }
}
