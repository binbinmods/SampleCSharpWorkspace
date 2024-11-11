// Decompiled with JetBrains decompiler
// Type: CardScreenManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CardScreenManager : MonoBehaviour
{
  public Transform content;
  public SpriteRenderer backgroundSPR;
  public SpriteRenderer backgroundSmooth;
  public TitleMovement titleMovement;
  public BotonGeneric bGeneric;
  public Transform closeButton;
  public Transform cardContainer;
  public Transform cardUpgrade;
  public Transform cardUpgradeContainer;
  public TitleMovement titleMovementUp;
  public TitleMovement titleMovementDown;
  public GameObject relatedCard;
  public Transform relatedContainer;
  public Transform relatedText;
  public Transform cardBasicValues;
  public Transform cardItemCorruption;
  public Transform rareText;
  public TMP_Text rarityText;
  public Transform dropPlace;
  private bool state;
  private CanvasScaler relatedScaler;
  private CardData cardData;
  private CardItem CI;
  private CardItem CIBack;
  private CardItem CIUp;
  private CardItem CIDown;
  private GameObject cardGO;
  private GameObject cardGOBack;
  private GameObject cardGOUp;
  private GameObject cardGODown;
  private Coroutine co;
  private List<Transform> controllerList = new List<Transform>();
  private int controllerHorizontalIndex = -1;
  private Vector2 warpPosition;

  public static CardScreenManager Instance { get; private set; }

  private void Awake()
  {
    if ((UnityEngine.Object) CardScreenManager.Instance == (UnityEngine.Object) null)
      CardScreenManager.Instance = this;
    else if ((UnityEngine.Object) CardScreenManager.Instance != (UnityEngine.Object) this)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    this.relatedScaler = this.relatedContainer.GetComponent<CanvasScaler>();
    this.state = false;
  }

  private void Start()
  {
    if ((UnityEngine.Object) this.cardGOUp == (UnityEngine.Object) null)
    {
      this.cardGOUp = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, this.cardUpgradeContainer);
      this.CIUp = this.cardGOUp.GetComponent<CardItem>();
      this.CIUp.SetDestinationScaleRotation(new Vector3(0.0f, 0.0f, 0.0f), 1.4f, Quaternion.Euler(0.0f, 0.0f, 0.0f));
    }
    if (!((UnityEngine.Object) this.cardGODown == (UnityEngine.Object) null))
      return;
    this.cardGODown = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, this.cardUpgradeContainer);
    this.CIDown = this.cardGODown.GetComponent<CardItem>();
    this.CIDown.SetDestinationScaleRotation(new Vector3(0.0f, 0.0f, 0.0f), 1.4f, Quaternion.Euler(0.0f, 0.0f, 0.0f));
  }

  public void Resize()
  {
    if ((double) Globals.Instance.scale < 1.0)
      this.relatedScaler.matchWidthOrHeight = 1f;
    else
      this.relatedScaler.matchWidthOrHeight = 0.0f;
  }

  public void ShowCardScreen(bool _state)
  {
    PopupManager.Instance.ClosePopup();
    GameManager.Instance.CleanTempContainer();
    this.state = _state;
    this.content.gameObject.SetActive(_state);
    if (!_state)
    {
      GameManager.Instance.SceneLoaded();
      foreach (Component component in this.relatedContainer)
        UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
      if (!(bool) (UnityEngine.Object) CardCraftManager.Instance)
        return;
      CardCraftManager.Instance.ShowSearch(true);
    }
    else
    {
      OptionsManager.Instance.Hide();
      PlayerUIManager.Instance.Hide();
      if (!(bool) (UnityEngine.Object) CardCraftManager.Instance)
        return;
      CardCraftManager.Instance.ShowSearch(false);
    }
  }

  public bool IsActive() => this.state;

  public void SetCardData(CardData _cardData)
  {
    if (this.co != null)
      this.StopCoroutine(this.co);
    this.cardData = _cardData;
    this.co = this.StartCoroutine(this.SetCardDataCo());
  }

  private IEnumerator SetCardDataCo()
  {
    this.backgroundSPR.color = this.bGeneric.color = this.backgroundSmooth.color = Functions.HexToColor(this.cardData.CardClass == Enums.CardClass.Boon || this.cardData.CardClass == Enums.CardClass.Injury || this.cardData.CardClass == Enums.CardClass.Item || this.cardData.CardClass == Enums.CardClass.Special || this.cardData.CardClass == Enums.CardClass.Enchantment ? Globals.Instance.ClassColor[Enum.GetName(typeof (Enums.CardClass), (object) this.cardData.CardClass)] : Globals.Instance.ClassColor[Enum.GetName(typeof (Enums.HeroClass), (object) this.cardData.CardClass)]);
    this.backgroundSPR.color = new Color(this.backgroundSPR.color.r, this.backgroundSPR.color.g, this.backgroundSPR.color.b, 0.97f);
    this.backgroundSmooth.color = new Color(this.backgroundSmooth.color.r, this.backgroundSmooth.color.g, this.backgroundSmooth.color.b, 0.4f);
    this.bGeneric.SetColor();
    this.dropPlace.transform.gameObject.SetActive(false);
    if ((UnityEngine.Object) this.cardData.Item != (UnityEngine.Object) null)
    {
      if (this.cardBasicValues.gameObject.activeSelf)
        this.cardBasicValues.gameObject.SetActive(false);
      if (!this.cardItemCorruption.gameObject.activeSelf)
        this.cardItemCorruption.gameObject.SetActive(true);
      if ((UnityEngine.Object) this.cardData.Item.SpriteBossDrop != (UnityEngine.Object) null)
      {
        this.dropPlace.transform.gameObject.SetActive(true);
        this.dropPlace.GetComponent<SpriteRenderer>().sprite = this.cardData.Item.SpriteBossDrop;
      }
    }
    else
    {
      if (!this.cardBasicValues.gameObject.activeSelf)
        this.cardBasicValues.gameObject.SetActive(true);
      if (this.cardItemCorruption.gameObject.activeSelf)
        this.cardItemCorruption.gameObject.SetActive(false);
    }
    Vector3 position = new Vector3(-5f, 0.2f, 0.0f);
    if ((UnityEngine.Object) this.cardGO == (UnityEngine.Object) null)
    {
      this.cardGO = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, position, Quaternion.identity, this.cardContainer);
      this.CI = this.cardGO.GetComponent<CardItem>();
    }
    if ((UnityEngine.Object) this.cardGOBack == (UnityEngine.Object) null)
    {
      this.cardGOBack = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, position, Quaternion.identity, this.cardContainer);
      this.CIBack = this.cardGOBack.GetComponent<CardItem>();
    }
    if (this.cardData.CardUpgraded == Enums.CardUpgraded.No)
    {
      this.titleMovement.SetText(this.cardData.CardName);
      this.titleMovement.SetColor(Globals.Instance.ColorColor["white"]);
    }
    else if (this.cardData.CardUpgraded == Enums.CardUpgraded.A)
    {
      this.titleMovement.SetText(Globals.Instance.GetCardData(this.cardData.UpgradedFrom).CardName);
      this.titleMovement.SetColor(Globals.Instance.ColorColor["blueCardTitle"]);
    }
    else if (this.cardData.CardUpgraded == Enums.CardUpgraded.B)
    {
      this.titleMovement.SetText(Globals.Instance.GetCardData(this.cardData.UpgradedFrom).CardName);
      this.titleMovement.SetColor(Globals.Instance.ColorColor["goldCardTitle"]);
    }
    else if (this.cardData.CardUpgraded == Enums.CardUpgraded.Rare)
    {
      this.titleMovement.SetText(Globals.Instance.GetCardData(this.cardData.UpgradedFrom).CardName);
      this.titleMovement.SetColor(Globals.Instance.ColorColor["purple"]);
    }
    string id = this.cardData.Id.Split('_', StringSplitOptions.None)[0];
    this.cardGO.name = this.cardGOBack.name = id;
    this.CI.SetCard(id, GetFromGlobal: true);
    this.CIBack.SetCard(id, GetFromGlobal: true);
    this.CI.TopLayeringOrder("UI", 31600);
    this.CIBack.TopLayeringOrder("UI", 31500);
    this.CI.DisableTrail();
    this.CIBack.DisableTrail();
    this.CI.DrawBorder("black");
    this.CIBack.DrawBorder("black");
    this.CI.SetDestinationScaleRotation(position, 2f, Quaternion.Euler(0.0f, 0.0f, 0.0f));
    this.CIBack.SetDestinationScaleRotation(position, 2f, Quaternion.Euler(0.0f, 0.0f, 7f));
    this.CI.active = true;
    this.CIBack.active = true;
    this.CI.enabled = this.CIBack.enabled = true;
    this.cardGO.GetComponent<BoxCollider2D>().enabled = false;
    this.cardGOBack.GetComponent<BoxCollider2D>().enabled = false;
    if (this.cardData.RelatedCard == "")
    {
      this.relatedText.gameObject.SetActive(false);
      this.relatedContainer.gameObject.SetActive(false);
    }
    else
    {
      this.relatedText.gameObject.SetActive(true);
      this.relatedContainer.gameObject.SetActive(true);
      foreach (Component component in this.relatedContainer)
        UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
      GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.relatedCard, Vector3.zero, Quaternion.identity, this.relatedContainer);
      gameObject1.transform.localScale = new Vector3(1f, 1f, 1f);
      gameObject1.transform.localPosition = new Vector3(0.0f, -0.05f, 0.0f);
      gameObject1.GetComponent<CardVertical>().SetCard(this.cardData.RelatedCard);
      if (this.cardData.RelatedCard2 != "")
      {
        GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.relatedCard, Vector3.zero, Quaternion.identity, this.relatedContainer);
        gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
        gameObject2.transform.localPosition = new Vector3(0.0f, -0.45f, 0.0f);
        gameObject2.GetComponent<CardVertical>().SetCard(this.cardData.RelatedCard2);
        if (this.cardData.RelatedCard3 != "")
        {
          GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.relatedCard, Vector3.zero, Quaternion.identity, this.relatedContainer);
          gameObject3.transform.localScale = new Vector3(1f, 1f, 1f);
          gameObject3.transform.localPosition = new Vector3(0.0f, -0.85f, 0.0f);
          gameObject3.GetComponent<CardVertical>().SetCard(this.cardData.RelatedCard3);
        }
      }
    }
    if (this.cardData.CardRarity == Enums.CardRarity.Common)
    {
      this.rarityText.text = Texts.Instance.GetText("cardCommon");
      this.rarityText.color = new Color(0.66f, 0.66f, 0.66f);
    }
    else if (this.cardData.CardRarity == Enums.CardRarity.Uncommon)
    {
      this.rarityText.text = Texts.Instance.GetText("cardUncommon");
      this.rarityText.color = Globals.Instance.RarityColor["uncommon"];
    }
    else if (this.cardData.CardRarity == Enums.CardRarity.Rare)
    {
      this.rarityText.text = Texts.Instance.GetText("cardRare");
      this.rarityText.color = Globals.Instance.RarityColor["rare"];
    }
    else if (this.cardData.CardRarity == Enums.CardRarity.Epic)
    {
      this.rarityText.text = Texts.Instance.GetText("cardEpic");
      this.rarityText.color = Globals.Instance.RarityColor["epic"];
    }
    else if (this.cardData.CardRarity == Enums.CardRarity.Mythic)
    {
      this.rarityText.text = Texts.Instance.GetText("cardMythic");
      this.rarityText.color = Globals.Instance.RarityColor["mythic"];
    }
    else
      this.rarityText.text = "";
    if (this.cardData.CardUpgraded == Enums.CardUpgraded.Rare || this.cardData.CardClass == Enums.CardClass.Special)
    {
      this.rareText.gameObject.SetActive(true);
      if (this.cardData.CardUpgraded == Enums.CardUpgraded.Rare)
        this.rareText.GetComponent<TMP_Text>().text = Texts.Instance.GetText("corruptedVersion");
      else if (this.cardData.Starter)
        this.rareText.GetComponent<TMP_Text>().text = Texts.Instance.GetText("characterCard");
      else
        this.rareText.GetComponent<TMP_Text>().text = Texts.Instance.GetText("specialCard");
    }
    else
      this.rareText.gameObject.SetActive(false);
    if (this.cardData.CardClass == Enums.CardClass.Monster || this.cardData.CardUpgraded == Enums.CardUpgraded.No && this.cardData.UpgradesTo1 == "" && (UnityEngine.Object) this.cardData.Item == (UnityEngine.Object) null)
    {
      this.cardUpgrade.gameObject.SetActive(false);
    }
    else
    {
      this.cardUpgrade.gameObject.SetActive(true);
      float posY = 2.4f;
      if ((UnityEngine.Object) this.cardGOUp == (UnityEngine.Object) null)
      {
        this.cardGOUp = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.cardUpgradeContainer);
        this.CIUp = this.cardGOUp.GetComponent<CardItem>();
      }
      if ((UnityEngine.Object) this.cardGODown == (UnityEngine.Object) null)
      {
        this.cardGODown = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.cardUpgradeContainer);
        this.CIDown = this.cardGODown.GetComponent<CardItem>();
      }
      this.CIUp.active = this.CIDown.active = false;
      this.CIUp.transform.localPosition = new Vector3(0.0f, posY, -1f);
      this.CIDown.transform.localPosition = new Vector3(0.0f, -posY, -1f);
      bool hideItemDown = true;
      if ((UnityEngine.Object) this.cardData.Item != (UnityEngine.Object) null)
      {
        if ((UnityEngine.Object) this.cardData.UpgradesToRare != (UnityEngine.Object) null)
        {
          this.titleMovementUp.SetText(Texts.Instance.GetText("corruptedVersion"));
          this.titleMovementUp.SetColor(Globals.Instance.ColorColor["purple"]);
          this.CIUp.SetCard(this.cardData.UpgradesToRare.Id, GetFromGlobal: true);
          if (!this.CIUp.gameObject.activeSelf)
          {
            this.CIUp.gameObject.SetActive(true);
            this.titleMovementUp.gameObject.SetActive(true);
          }
        }
        else if (this.cardData.UpgradedFrom != "")
        {
          this.titleMovementUp.SetText(Texts.Instance.GetText("sourceCard"));
          this.titleMovementUp.SetColor(Globals.Instance.ColorColor["white"]);
          this.CIUp.SetCard(this.cardData.UpgradedFrom, GetFromGlobal: true);
          if (!this.CIUp.gameObject.activeSelf)
          {
            this.CIUp.gameObject.SetActive(true);
            this.titleMovementUp.gameObject.SetActive(true);
          }
        }
        else if (this.cardData.CardUpgraded == Enums.CardUpgraded.No)
        {
          if (this.cardData.UpgradesTo1 != "")
          {
            this.titleMovementUp.SetText(Texts.Instance.GetText("upgradesTo"));
            this.titleMovementUp.SetColor(Globals.Instance.ColorColor["blueCardTitle"]);
            this.CIUp.SetCard(this.cardData.UpgradesTo1, GetFromGlobal: true);
            this.CIUp.ShowDifferences(this.cardData);
          }
          else if (this.CIUp.gameObject.activeSelf)
          {
            this.CIUp.gameObject.SetActive(false);
            this.titleMovementUp.gameObject.SetActive(false);
          }
          if (this.cardData.UpgradesTo2 != "")
          {
            this.titleMovementDown.SetText(Texts.Instance.GetText("upgradesTo"));
            this.titleMovementDown.SetColor(Globals.Instance.ColorColor["goldCardTitle"]);
            this.CIDown.SetCard(this.cardData.UpgradesTo2, GetFromGlobal: true);
            this.CIDown.ShowDifferences(this.cardData);
            hideItemDown = false;
          }
          else if (this.CIDown.gameObject.activeSelf)
          {
            this.CIDown.gameObject.SetActive(false);
            this.titleMovementDown.gameObject.SetActive(false);
          }
        }
        else if (this.cardUpgrade.gameObject.activeSelf)
          this.cardUpgrade.gameObject.SetActive(false);
        if (!this.CIUp.gameObject.activeSelf && !this.CIDown.gameObject.activeSelf && this.cardUpgrade.gameObject.activeSelf)
          this.cardUpgrade.gameObject.SetActive(false);
      }
      else if (this.cardData.CardUpgraded == Enums.CardUpgraded.No)
      {
        this.titleMovementUp.SetText(Texts.Instance.GetText("upgradesTo"));
        this.titleMovementUp.SetColor(Globals.Instance.ColorColor["blueCardTitle"]);
        this.titleMovementDown.SetText(Texts.Instance.GetText("upgradesTo"));
        this.titleMovementDown.SetColor(Globals.Instance.ColorColor["goldCardTitle"]);
        this.CIUp.SetCard(this.cardData.UpgradesTo1, GetFromGlobal: true);
        this.CIDown.SetCard(this.cardData.UpgradesTo2, GetFromGlobal: true);
        this.CIUp.ShowDifferences(this.cardData);
        this.CIDown.ShowDifferences(this.cardData);
      }
      else
      {
        this.titleMovementUp.SetText(Texts.Instance.GetText("upgradedFrom"));
        this.titleMovementUp.SetColor(Globals.Instance.ColorColor["white"]);
        this.titleMovementDown.SetText(Texts.Instance.GetText("transmutesTo"));
        this.CIUp.SetCard(this.cardData.UpgradedFrom, GetFromGlobal: true);
        this.CIUp.ShowDifferences(this.cardData);
        CardData cardData = Globals.Instance.GetCardData(this.cardData.UpgradedFrom);
        if (cardData.UpgradesTo1.ToLower() == id)
        {
          this.CIDown.SetCard(cardData.UpgradesTo2, GetFromGlobal: true);
          this.CIDown.ShowDifferences(this.cardData);
          this.titleMovementDown.SetColor(Globals.Instance.ColorColor["goldCardTitle"]);
        }
        else
        {
          this.CIDown.SetCard(cardData.UpgradesTo1, GetFromGlobal: true);
          this.CIDown.ShowDifferences(this.cardData);
          this.titleMovementDown.SetColor(Globals.Instance.ColorColor["blueCardTitle"]);
        }
      }
      yield return (object) Globals.Instance.WaitForSeconds(0.05f);
      this.CIUp.TopLayeringOrder("UI", 31551);
      this.CIDown.TopLayeringOrder("UI", 31550);
      this.CIUp.DisableTrail();
      this.CIDown.DisableTrail();
      this.CIUp.active = true;
      this.CIDown.active = true;
      this.CIUp.SetDestinationScaleRotation(new Vector3(0.0f, posY, -1f), 1.4f, Quaternion.Euler(0.0f, 0.0f, 0.0f));
      this.CIDown.SetDestinationScaleRotation(new Vector3(0.0f, -posY, -1f), 1.4f, Quaternion.Euler(0.0f, 0.0f, 0.0f));
      if ((UnityEngine.Object) this.cardData.Item != (UnityEngine.Object) null)
      {
        if (hideItemDown)
        {
          if (this.CIDown.gameObject.activeSelf)
          {
            this.CIDown.gameObject.SetActive(false);
            this.titleMovementDown.gameObject.SetActive(false);
          }
        }
        else if (!this.CIDown.gameObject.activeSelf)
        {
          this.CIDown.gameObject.SetActive(true);
          this.titleMovementDown.gameObject.SetActive(true);
        }
        this.CIUp.HideDifferences();
        this.CIDown.HideDifferences();
      }
      else if (this.cardData.CardUpgraded == Enums.CardUpgraded.Rare)
      {
        this.CIUp.ShowDifferences(this.cardData);
        this.titleMovementUp.SetText(Texts.Instance.GetText("upgradesTo"));
        this.titleMovementUp.SetText(Texts.Instance.GetText("sourceCard"));
        if (this.CIDown.gameObject.activeSelf)
        {
          this.CIDown.gameObject.SetActive(false);
          this.titleMovementDown.gameObject.SetActive(false);
        }
      }
      else
      {
        if (!this.CIUp.gameObject.activeSelf)
        {
          this.CIUp.gameObject.SetActive(true);
          this.titleMovementUp.gameObject.SetActive(true);
        }
        if (!this.CIDown.gameObject.activeSelf)
        {
          this.CIDown.gameObject.SetActive(true);
          this.titleMovementDown.gameObject.SetActive(true);
        }
      }
    }
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    this.CI.ShowKeyNotes();
    Vector3 offset = Vector3.zero;
    if (this.cardData.KeyNotes.Count > 6)
      offset -= new Vector3(0.0f, 80f, 0.0f);
    PopupManager.Instance.StablishPopupPositionSize(offset, new Vector3(1.1f, 1.1f, 1f));
    yield return (object) Globals.Instance.WaitForSeconds(0.2f);
    PopupManager.Instance.StablishPopupPositionSize(offset, new Vector3(1.1f, 1.1f, 1f));
    this.CI.enabled = this.CIBack.enabled = false;
  }

  public void ControllerMovement(bool goingUp = false, bool goingRight = false, bool goingDown = false, bool goingLeft = false)
  {
    this.controllerList.Clear();
    this.controllerList.Add(this.closeButton);
    if (Functions.TransformIsVisible(this.CIUp.transform))
      this.controllerList.Add(this.CIUp.transform);
    if (Functions.TransformIsVisible(this.CIDown.transform))
      this.controllerList.Add(this.CIDown.transform);
    if (Functions.TransformIsVisible(this.relatedContainer.transform))
      this.controllerList.Add(this.relatedContainer.transform);
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this.controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this.controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((UnityEngine.Object) this.controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }
}
