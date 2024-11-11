// Decompiled with JetBrains decompiler
// Type: CardCraftItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Text;
using TMPro;
using UnityEngine;

public class CardCraftItem : MonoBehaviour
{
  public Transform container;
  public Transform button;
  public Transform buttonItem;
  public Transform availability;
  public Transform availabilityYes;
  public Transform availabilityNo;
  public TMP_Text availabilityYesText;
  public TMP_Text availabilityNoText;
  public Transform lockIcon;
  public Transform lockIconBackground;
  public PopupText lockPopup;
  public string cardId;
  private string shopId;
  private CardItem CI;
  private BotonGeneric bGeneric;
  private BotonGeneric bGenericItem;
  public Vector3 position;
  private Hero currentHero;
  private bool available = true;
  private int index;
  private bool enabled;
  private CardData cData;
  public Transform arrowPointer;

  public bool Available
  {
    get => this.available;
    set => this.available = value;
  }

  public bool Enabled
  {
    get => this.enabled;
    set => this.enabled = value;
  }

  public void SetHero(Hero _hero) => this.currentHero = _hero;

  public void SetIndex(int _index) => this.index = _index;

  public void SetButtonText(string _buttonText)
  {
    this.bGeneric.gameObject.SetActive(true);
    this.availability.gameObject.SetActive(true);
    this.bGenericItem.gameObject.SetActive(false);
    this.bGeneric.SetText(_buttonText);
  }

  public void SetButtonTextItem(string _buttonTextItem)
  {
    this.bGeneric.gameObject.SetActive(false);
    this.availability.gameObject.SetActive(false);
    this.bGenericItem.gameObject.SetActive(true);
    this.bGenericItem.SetText(_buttonTextItem);
  }

  public void SetPosition(Vector3 _position) => this.position = _position;

  public CardItem GetCI() => (Object) this.CI != (Object) null ? this.CI : (CardItem) null;

  public void EnableButton(bool _state)
  {
    this.enabled = _state;
    if (_state)
    {
      this.bGeneric.Enable();
      this.bGenericItem.Enable();
    }
    else
    {
      this.bGeneric.Disable();
      this.bGenericItem.Disable();
    }
  }

  public void ShowLock(bool _state, string _text = "")
  {
    if (this.lockIcon.gameObject.activeSelf != _state)
      this.lockIcon.gameObject.SetActive(_state);
    if (this.lockIconBackground.gameObject.activeSelf != _state)
      this.lockIconBackground.gameObject.SetActive(_state);
    if (!_state)
      return;
    this.lockPopup.text = _text;
  }

  public void SetGenericCard(bool item = false)
  {
    this.bGeneric = this.button.transform.GetComponent<BotonGeneric>();
    this.bGenericItem = this.buttonItem.transform.GetComponent<BotonGeneric>();
    this.CI = Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.container).GetComponent<CardItem>();
    this.CI.TopLayeringOrder("UI", -500);
    this.CI.SetLocalPosition(this.position);
    if (item)
      this.CI.SetLocalScale(new Vector3(1.25f, 1.25f, 1f));
    else
      this.CI.SetLocalScale(new Vector3(1.1f, 1.1f, 1f));
    this.CI.cardforaddcard = true;
    this.CI.DisableTrail();
    if (item)
    {
      this.buttonItem.localPosition = new Vector3(this.position.x, this.position.y - 2.05f, 0.0f);
      this.buttonItem.gameObject.name = "CraftItemBuyButton";
    }
    else
    {
      this.availability.localPosition = new Vector3(this.position.x - 0.75f, this.position.y - 1.82f, 0.0f);
      this.button.localPosition = new Vector3(this.position.x + 0.25f, this.position.y - 1.82f, 0.0f);
      this.button.gameObject.name = "CraftBuyButton";
    }
  }

  public void SetCard(string _cardId, string _shopId = "", Hero _hero = null)
  {
    if (_hero == null)
      _hero = this.currentHero;
    this.cardId = _cardId;
    this.shopId = _shopId;
    this.cData = Globals.Instance.GetCardData(this.cardId, false);
    this.CI.SetCard(this.cardId, false, _hero);
    this.CI.CreateColliderAdjusted();
    this.CI.cardmakebig = true;
    if (this.cData.CardClass != Enums.CardClass.Item)
    {
      this.CI.DrawEnergyCost();
      this.CI.cardmakebigSizeMax = 1.25f;
    }
    else
      this.CI.cardmakebigSizeMax = 1.4f;
    this.CI.TopLayeringOrder("UI", 1000);
    this.bGeneric.auxString = this.cardId;
    this.bGenericItem.auxString = this.cardId;
    this.SetAvailability();
  }

  public void SetAvailability()
  {
    int[] cardAvailability = CardCraftManager.Instance.GetCardAvailability(this.cData.Id, this.shopId);
    int num1 = cardAvailability[0];
    int num2 = cardAvailability[1];
    if ((Object) this.cData.Item == (Object) null && AtOManager.Instance.Sandbox_unlimitedAvailableCards)
    {
      num2 = num1 + 10;
      this.availability.gameObject.SetActive(false);
    }
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<size=1.6><color=#BDA08A>");
    stringBuilder.Append(Texts.Instance.GetText("maximumAbb"));
    stringBuilder.Append("</size></color>\n");
    stringBuilder.Append((num2 - num1).ToString());
    if (num1 < num2)
    {
      this.availabilityYesText.text = stringBuilder.ToString();
      this.availabilityYes.gameObject.SetActive(true);
      this.availabilityNo.gameObject.SetActive(false);
    }
    else
    {
      this.availabilityNoText.text = stringBuilder.ToString();
      this.availabilityYes.gameObject.SetActive(false);
      this.availabilityNo.gameObject.SetActive(true);
    }
    if (num1 >= num2)
    {
      this.available = false;
      this.EnableButton(false);
      this.CI.ShowDisable(true);
      if (num2 <= 0)
        return;
      this.CI.ShowSold();
    }
    else
    {
      this.available = true;
      this.CI.ShowDisable(false);
      if (this.cData.CardClass == Enums.CardClass.Item)
      {
        if (CardCraftManager.Instance.CanBuy("Item", this.cardId))
          this.EnableButton(true);
        else
          this.EnableButton(false);
      }
      else if (CardCraftManager.Instance.CanBuy("Craft", this.cardId))
        this.EnableButton(true);
      else
        this.EnableButton(false);
    }
  }
}
