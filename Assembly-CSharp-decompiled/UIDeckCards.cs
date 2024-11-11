// Decompiled with JetBrains decompiler
// Type: UIDeckCards
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDeckCards : MonoBehaviour
{
  public TMP_Text textInstructions;
  public Transform buttonClose;
  public Transform buttonDiscard;
  public Transform buttonHide;
  public Canvas canvas;
  public Transform cardContainer;
  public Transform scroll;
  public RectTransform cardContainerRT;
  public GameObject elements;
  public TMP_Text buttonShowText;
  private bool showStatus = true;
  private Button buttonCloseB;
  private Button buttonAction;
  private int cardQuantity = -1;
  private bool mustSelect;
  private Coroutine enabledCoroutine;

  private void Awake()
  {
    this.buttonCloseB = this.buttonClose.GetComponent<Button>();
    this.buttonAction = this.buttonDiscard.GetComponent<Button>();
    this.canvas.gameObject.SetActive(false);
  }

  private IEnumerator mustSelectCo()
  {
    while (true)
    {
      if (this.mustSelect)
      {
        if (MatchManager.Instance.CardsLeftForAddcard() > 0)
          this.buttonAction.interactable = false;
        else
          this.buttonAction.interactable = true;
      }
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    }
  }

  public void HideShow(bool doMask = true)
  {
    if (this.showStatus)
    {
      this.elements.gameObject.SetActive(false);
      this.buttonShowText.text = Texts.Instance.GetText("show").ToUpper();
      if (doMask)
        MatchManager.Instance.ShowMaskFromUIScreen(false);
      if (this.enabledCoroutine != null)
        this.StopCoroutine(this.enabledCoroutine);
    }
    else
    {
      this.elements.gameObject.SetActive(true);
      this.buttonShowText.text = Texts.Instance.GetText("hide").ToUpper();
      if (doMask)
        MatchManager.Instance.ShowMaskFromUIScreen(true);
      if (this.enabledCoroutine != null)
        this.StopCoroutine(this.enabledCoroutine);
      this.enabledCoroutine = this.StartCoroutine(this.mustSelectCo());
      MatchManager.Instance.controllerCurrentIndex = -1;
    }
    this.showStatus = !this.showStatus;
  }

  public bool IsActive() => this.canvas.gameObject.activeSelf;

  private string FormatNumer(string text, int numCards)
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    stringBuilder2.Append("<color=green>");
    stringBuilder2.Append(numCards.ToString());
    stringBuilder2.Append("</color>");
    stringBuilder1.Append("\n<size=3><color=#bbb>");
    stringBuilder1.Append(string.Format(text, (object) stringBuilder2.ToString()));
    stringBuilder1.Append("</color></size>");
    return stringBuilder1.ToString();
  }

  public void TextInstructions(int type, int _cardQuantity, int _cardTotal = -1, bool _toVanish = false)
  {
    int numCards1 = 0;
    string str = "";
    this.cardQuantity = _cardQuantity;
    this.buttonCloseB.interactable = true;
    this.buttonClose.gameObject.SetActive(false);
    StringBuilder stringBuilder = new StringBuilder();
    this.mustSelect = false;
    switch (type)
    {
      case 0:
        numCards1 = MatchManager.Instance.CountHeroDeck();
        stringBuilder.Append(Texts.Instance.GetText("deckPile"));
        stringBuilder.Append(this.FormatNumer(Texts.Instance.GetText("cardsNum"), numCards1));
        str = stringBuilder.ToString();
        this.buttonClose.gameObject.SetActive(true);
        break;
      case 1:
        numCards1 = MatchManager.Instance.CountHeroDiscard();
        stringBuilder.Append(Texts.Instance.GetText("discardPile"));
        stringBuilder.Append(this.FormatNumer(Texts.Instance.GetText("cardsNum"), numCards1));
        str = stringBuilder.ToString();
        this.buttonClose.gameObject.SetActive(true);
        break;
      case 2:
        if (this.cardQuantity > 0)
        {
          stringBuilder.Append(Texts.Instance.GetText("deckPile"));
          if (!_toVanish)
            stringBuilder.Append(this.FormatNumer(Texts.Instance.GetText("youCanDiscardUpTo"), this.cardQuantity));
          else
            stringBuilder.Append(this.FormatNumer(Texts.Instance.GetText("youCanVanishUpTo"), this.cardQuantity));
          str = stringBuilder.ToString();
        }
        else
        {
          stringBuilder.Append(Texts.Instance.GetText("deckPile"));
          stringBuilder.Append("\n<size=3><color=#bbb>");
          stringBuilder.Append(Texts.Instance.GetText("pressWhenReady"));
          stringBuilder.Append("</size>");
          str = stringBuilder.ToString();
        }
        numCards1 = _cardTotal;
        MatchManager.Instance.CardsLeftForAddcard();
        if (GameManager.Instance.IsMultiplayer() && (Object) MatchManager.Instance != (Object) null && !MatchManager.Instance.IsYourTurn())
        {
          this.buttonClose.gameObject.SetActive(false);
          break;
        }
        break;
      case 3:
        numCards1 = _cardTotal;
        stringBuilder.Append(Texts.Instance.GetText("deckPile"));
        int numCards2 = MatchManager.Instance.CardsLeftForAddcard();
        if (numCards2 == 1)
          stringBuilder.Append(this.FormatNumer(Texts.Instance.GetText("youCanAdd"), 1));
        else
          stringBuilder.Append(this.FormatNumer(Texts.Instance.GetText("youCanAddPlural"), numCards2));
        str = stringBuilder.ToString();
        this.mustSelect = true;
        break;
    }
    this.textInstructions.text = str;
    if (numCards1 > 10)
    {
      this.scroll.gameObject.SetActive(true);
      this.cardContainerRT.sizeDelta = new Vector2(this.cardContainerRT.sizeDelta.x, (float) (3.2000000476837158 * (double) Mathf.CeilToInt((float) numCards1 / 5f) - 4.0 + 2.0));
      this.cardContainerRT.anchoredPosition = new Vector2(this.cardContainerRT.anchoredPosition.x, (float) (3.0 - (double) this.cardContainerRT.sizeDelta.y * 0.5 - 1.0));
      this.cardContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(2.5f, (float) ((double) this.cardContainerRT.sizeDelta.y * 0.5 - 7.5 + 1.0));
    }
    else
    {
      this.scroll.gameObject.SetActive(false);
      this.cardContainerRT.sizeDelta = new Vector2(this.cardContainerRT.sizeDelta.x, 4f);
      this.cardContainerRT.anchoredPosition = new Vector2(this.cardContainerRT.anchoredPosition.x, 0.0f);
      this.cardContainer.GetComponent<RectTransform>().anchoredPosition = new Vector2(2.5f, -4.5f);
    }
    this.cardContainerRT.gameObject.SetActive(true);
  }

  public void TurnOn(int type, int cardQuantity = -1, int cardTotal = -1, bool toVanish = false)
  {
    MatchManager.Instance.ShowMask(true);
    MatchManager.Instance.lockHideMask = true;
    this.buttonAction.interactable = true;
    this.TextInstructions(type, cardQuantity, cardTotal, toVanish);
    this.showStatus = false;
    this.HideShow(false);
    this.canvas.gameObject.SetActive(true);
    if (cardQuantity != -1)
      this.buttonDiscard.gameObject.SetActive(true);
    else
      this.buttonDiscard.gameObject.SetActive(false);
    if (!GameManager.Instance.IsMultiplayer() || !((Object) MatchManager.Instance != (Object) null))
      return;
    MatchManager.Instance.WarningMultiplayerIfNotActive();
    if (MatchManager.Instance.heroIndexWaitingForAddDiscard > -1)
    {
      if (MatchManager.Instance.IsYourTurnForAddDiscard())
        return;
      this.buttonDiscard.gameObject.SetActive(false);
      this.buttonAction.interactable = false;
    }
    else
    {
      if (MatchManager.Instance.IsYourTurn())
        return;
      this.buttonDiscard.gameObject.SetActive(false);
    }
  }

  public float GetScrolled() => this.cardContainer.position.y;

  public void Action()
  {
    if (!this.buttonAction.interactable)
      return;
    MatchManager.Instance.AssignLookDiscardAction();
  }

  public void CloseBG()
  {
    if (this.cardQuantity != -1)
      return;
    this.Close();
  }

  public void Close() => MatchManager.Instance.DrawDeckScreenDestroy();

  public void TurnOff()
  {
    this.cardContainerRT.gameObject.SetActive(false);
    MatchManager.Instance.lockHideMask = false;
    MatchManager.Instance.ShowMask(false);
    this.canvas.gameObject.SetActive(false);
    if (this.enabledCoroutine == null)
      return;
    this.StopCoroutine(this.enabledCoroutine);
  }
}
