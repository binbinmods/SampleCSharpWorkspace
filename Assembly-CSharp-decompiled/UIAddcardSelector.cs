// Decompiled with JetBrains decompiler
// Type: UIAddcardSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAddcardSelector : MonoBehaviour
{
  public TMP_Text textInstructions;
  public Transform button;
  public Canvas canvas;
  public GameObject elements;
  public TMP_Text buttonShowText;
  private bool showStatus = true;
  private Button buttonB;
  private bool hideEnabled;

  private void Awake()
  {
    this.buttonB = this.button.GetComponent<Button>();
    this.canvas.gameObject.SetActive(false);
  }

  public bool IsActive() => this.canvas.gameObject.activeSelf;

  public void HideShow()
  {
    if (!this.hideEnabled)
      return;
    if (this.showStatus)
    {
      this.elements.gameObject.SetActive(false);
      this.buttonShowText.text = Texts.Instance.GetText("show");
    }
    else
    {
      this.elements.gameObject.SetActive(true);
      this.buttonShowText.text = Texts.Instance.GetText("hide");
    }
  }

  public void TextInstructions()
  {
    int num = MatchManager.Instance.CardsLeftForAddcard();
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Texts.Instance.GetText("addCardInstructions"));
    stringBuilder.Append("\n<size=3><color=#bbb>");
    stringBuilder.Append(Texts.Instance.GetText("cardsLeft"));
    stringBuilder.Append(" <color=green>");
    stringBuilder.Append(num.ToString());
    stringBuilder.Append("</color>");
    this.textInstructions.text = stringBuilder.ToString();
    if (GameManager.Instance.IsMultiplayer() && (Object) MatchManager.Instance != (Object) null && !MatchManager.Instance.IsYourTurn())
    {
      this.buttonB.gameObject.SetActive(false);
      this.buttonB.interactable = false;
    }
    else
    {
      this.buttonB.gameObject.SetActive(true);
      if (num == 0)
        this.buttonB.interactable = true;
      else
        this.buttonB.interactable = false;
    }
  }

  public void TurnOn()
  {
    this.hideEnabled = false;
    this.buttonB.interactable = false;
    MatchManager.Instance.ShowMask(true);
    MatchManager.Instance.lockHideMask = true;
    this.TextInstructions();
    this.canvas.gameObject.SetActive(true);
    MatchManager.Instance.controllerCurrentIndex = -1;
    this.StartCoroutine(this.EnableHide());
  }

  private IEnumerator EnableHide()
  {
    yield return (object) Globals.Instance.WaitForSeconds(10.5f);
    this.hideEnabled = true;
  }

  public void Action()
  {
    if (!this.buttonB.interactable || !MatchManager.Instance.WaitingForAddcardAssignment)
      return;
    MatchManager.Instance.AssignAddcardAction();
    this.TurnOff();
  }

  public void TurnOff()
  {
    MatchManager.Instance.lockHideMask = false;
    MatchManager.Instance.ShowMask(false);
    this.canvas.gameObject.SetActive(false);
  }
}
