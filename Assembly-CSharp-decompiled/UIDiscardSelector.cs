// Decompiled with JetBrains decompiler
// Type: UIDiscardSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDiscardSelector : MonoBehaviour
{
  public TMP_Text textInstructions;
  public Transform button;
  public Transform buttonHide;
  public Canvas canvas;
  private Button buttonB;
  public GameObject elements;
  public TMP_Text buttonShowText;
  private bool showStatus = true;
  public Transform cardContainer;
  private bool nonLimitedNumCards;
  private Enums.CardPlace cardPlace;

  private void Awake()
  {
    this.buttonB = this.button.GetComponent<Button>();
    this.canvas.gameObject.SetActive(false);
  }

  public bool IsActive() => this.canvas.gameObject.activeSelf;

  public void HideShow(bool doMask = true)
  {
    if (this.showStatus)
    {
      this.elements.gameObject.SetActive(false);
      this.buttonShowText.text = Texts.Instance.GetText("show").ToUpper();
      if (doMask)
        MatchManager.Instance.ShowMaskFromUIScreen(false);
    }
    else
    {
      this.elements.gameObject.SetActive(true);
      this.buttonShowText.text = Texts.Instance.GetText("hide").ToUpper();
      if (doMask)
        MatchManager.Instance.ShowMaskFromUIScreen(true);
    }
    this.showStatus = !this.showStatus;
  }

  public void TextInstructions()
  {
    int num = MatchManager.Instance.CardsLeftForDiscard();
    if (num < 0)
      num = 0;
    StringBuilder stringBuilder = new StringBuilder();
    if (this.cardPlace == Enums.CardPlace.TopDeck)
      stringBuilder.Append(Texts.Instance.GetText("chooseTopDeck"));
    else
      stringBuilder.Append(Texts.Instance.GetText("chooseDiscard"));
    if (!this.nonLimitedNumCards)
    {
      stringBuilder.Append("\n<size=3><color=#bbb>");
      stringBuilder.Append(Texts.Instance.GetText("cardsLeft"));
      stringBuilder.Append(" <color=green>");
      stringBuilder.Append(num.ToString());
      stringBuilder.Append("</color>");
    }
    this.textInstructions.text = stringBuilder.ToString();
    if (GameManager.Instance.IsMultiplayer() && (Object) MatchManager.Instance != (Object) null && !MatchManager.Instance.IsYourTurn())
    {
      this.buttonB.gameObject.SetActive(false);
      this.buttonB.interactable = false;
    }
    else
    {
      this.buttonB.gameObject.SetActive(true);
      if (num == 0 || this.nonLimitedNumCards)
        this.buttonB.interactable = true;
      else
        this.buttonB.interactable = false;
    }
  }

  public void TurnOn(Enums.CardPlace _cardPlace, bool _nonLimitedNumCards = false)
  {
    this.buttonB.interactable = false;
    MatchManager.Instance.ShowMask(true);
    MatchManager.Instance.lockHideMask = true;
    this.cardPlace = _cardPlace;
    this.nonLimitedNumCards = _nonLimitedNumCards;
    this.TextInstructions();
    this.showStatus = false;
    this.HideShow(false);
    this.canvas.gameObject.SetActive(true);
    MatchManager.Instance.WarningMultiplayerIfNotActive();
  }

  public void Action()
  {
    if (!this.buttonB.interactable || !MatchManager.Instance.WaitingForDiscardAssignment && !MatchManager.Instance.WaitingForLookDiscardWindow)
      return;
    this.buttonB.gameObject.SetActive(false);
    MatchManager.Instance.AssignDiscardAction();
  }

  public void TurnOff()
  {
    MatchManager.Instance.lockHideMask = false;
    MatchManager.Instance.ShowMask(false);
    this.canvas.gameObject.SetActive(false);
  }
}
