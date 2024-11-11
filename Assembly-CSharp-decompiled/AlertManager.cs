// Decompiled with JetBrains decompiler
// Type: AlertManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AlertManager : MonoBehaviour
{
  public Image backgroundImage;
  public Transform popupT;
  public GameObject canvas;
  public TMP_Text alertText;
  public TMP_InputField alertInput;
  public TMP_Text alertTextCP;
  public TMP_InputField alertInputCP;
  public TMP_InputField alertInputPC;
  public Transform exitButton;
  public Button SingleButton;
  public Button LeftButton;
  public Button RightButton;
  public TMP_Text alertInputButtonText;
  public TMP_Text alertTextSingleButton;
  public TMP_Text alertTextLeftButton;
  public TMP_Text alertTextRightButton;
  private string inputValue;
  private bool confirmAnswer;
  public Transform doorIcon;
  public Transform resignIcon;
  public Transform reloadIcon;
  public static AlertManager.OnButtonClickDelegate buttonClickDelegate;
  public List<Transform> menuControllerAlert;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();

  public static AlertManager Instance { get; private set; }

  public void OnButtonClick() => AlertManager.buttonClickDelegate();

  private void Awake()
  {
    if ((Object) AlertManager.Instance == (Object) null)
      AlertManager.Instance = this;
    else if ((Object) AlertManager.Instance != (Object) this)
      Object.Destroy((Object) this.gameObject);
    Object.DontDestroyOnLoad((Object) this.gameObject);
  }

  private void Start() => this.HideAlert();

  public void Abort()
  {
    AlertManager.buttonClickDelegate = (AlertManager.OnButtonClickDelegate) null;
    this.HideAlert();
  }

  public bool IsActive() => this.gameObject.activeSelf;

  public void HideAlert()
  {
    this.alertText.text = "";
    this.alertInput.transform.gameObject.SetActive(false);
    this.alertTextSingleButton.transform.parent.gameObject.SetActive(false);
    this.alertTextLeftButton.transform.parent.gameObject.SetActive(false);
    this.alertTextRightButton.transform.parent.gameObject.SetActive(false);
    this.alertTextCP.transform.gameObject.SetActive(false);
    this.alertInputCP.transform.gameObject.SetActive(false);
    this.alertInputPC.transform.gameObject.SetActive(false);
    this.SingleButton.interactable = false;
    this.SingleButton.interactable = true;
    this.LeftButton.interactable = false;
    this.LeftButton.interactable = true;
    this.RightButton.interactable = false;
    this.RightButton.interactable = true;
    this.gameObject.SetActive(false);
    KeyboardManager.Instance.ShowKeyboard(false);
  }

  public void ShowDoorIcon() => this.doorIcon.gameObject.SetActive(true);

  public void ShowResignIcon() => this.resignIcon.gameObject.SetActive(true);

  public void ShowReloadIcon() => this.reloadIcon.gameObject.SetActive(true);

  private void ShowAlert(bool isInput)
  {
    this.canvas.gameObject.SetActive(true);
    this.SetInitialPosition();
    this.gameObject.SetActive(true);
    this.doorIcon.gameObject.SetActive(false);
    this.resignIcon.gameObject.SetActive(false);
    this.reloadIcon.gameObject.SetActive(false);
    GameManager.Instance.PlayLibraryAudio("ui_menu_popup_01");
    if (isInput)
    {
      this.alertText.GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 130f);
      this.exitButton.gameObject.SetActive(true);
      this.inputValue = "";
    }
    else
    {
      this.alertText.GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 170f);
      this.exitButton.gameObject.SetActive(false);
    }
    if ((bool) (Object) PopupManager.Instance)
      PopupManager.Instance.ClosePopup();
    if ((bool) (Object) MapManager.Instance)
      MapManager.Instance.HidePopup();
    GameManager.Instance.CleanTempContainer();
  }

  public void CloseAlert(bool force = false)
  {
    if (this.alertTextLeftButton.transform.parent.gameObject.activeSelf || this.exitButton.gameObject.activeSelf)
    {
      this.SetConfirmAnswer(false);
    }
    else
    {
      if (!this.alertTextSingleButton.transform.parent.gameObject.activeSelf && !force)
        return;
      this.confirmAnswer = false;
      if (AlertManager.buttonClickDelegate != null)
        AlertManager.buttonClickDelegate();
      this.HideAlert();
    }
  }

  public void SetConfirmAnswer(bool status)
  {
    this.confirmAnswer = status;
    if (AlertManager.buttonClickDelegate != null)
      AlertManager.buttonClickDelegate();
    this.HideAlert();
  }

  public bool GetConfirmAnswer() => this.confirmAnswer;

  public string GetInputValue() => this.inputValue;

  public void AlertInputSuccess()
  {
    this.inputValue = this.alertInput.text;
    if (this.inputValue != "" && AlertManager.buttonClickDelegate != null)
      AlertManager.buttonClickDelegate();
    this.HideAlert();
  }

  public void HideConfirmButton() => this.alertTextSingleButton.transform.parent.gameObject.SetActive(false);

  public void AlertConfirm(string text, string textButton = "")
  {
    this.alertText.text = text;
    this.alertTextSingleButton.text = !(textButton != "") ? Texts.Instance.GetText("accept") : textButton;
    this.alertTextSingleButton.transform.parent.gameObject.SetActive(true);
    this.ShowAlert(false);
  }

  public void AlertConfirmDouble(
    string text,
    string textButtonRight = "",
    string textButtonLeft = "",
    bool showButtons = true)
  {
    this.alertText.text = text;
    this.alertTextRightButton.text = !(textButtonRight != "") ? Texts.Instance.GetText("accept") : textButtonRight;
    this.alertTextLeftButton.text = !(textButtonLeft != "") ? Texts.Instance.GetText("cancel") : textButtonLeft;
    if (showButtons)
    {
      this.alertTextLeftButton.transform.parent.gameObject.SetActive(true);
      this.alertTextRightButton.transform.parent.gameObject.SetActive(true);
    }
    else
    {
      this.alertTextLeftButton.transform.parent.gameObject.SetActive(false);
      this.alertTextRightButton.transform.parent.gameObject.SetActive(false);
    }
    this.ShowAlert(false);
  }

  public void AlertCopyPaste(string titleText, string inputText)
  {
    this.alertTextCP.transform.gameObject.SetActive(true);
    this.alertInputCP.transform.gameObject.SetActive(true);
    this.alertTextCP.text = titleText;
    this.alertInputCP.text = inputText;
    this.alertTextSingleButton.text = Texts.Instance.GetText("close");
    this.alertTextSingleButton.transform.parent.gameObject.SetActive(true);
    this.ShowAlert(false);
  }

  public void AlertPasteCopy(string text, string textButton = "")
  {
    this.alertTextCP.transform.gameObject.SetActive(true);
    this.alertInputPC.transform.gameObject.SetActive(true);
    this.alertTextCP.text = text;
    this.alertInputPC.text = textButton;
    this.alertTextSingleButton.text = Texts.Instance.GetText("accept");
    this.alertTextSingleButton.transform.parent.gameObject.SetActive(true);
    this.ShowAlert(true);
    this.StartCoroutine(this.ActivateInputPC());
  }

  private IEnumerator ActivateInputPC()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    this.alertInputPC.Select();
    this.alertInputPC.ActivateInputField();
  }

  public string GetInputPCValue() => this.alertInputPC.text;

  public void AlertInput(string text, string textButton = "")
  {
    this.alertText.text = text;
    this.alertInputButtonText.text = !(textButton != "") ? Texts.Instance.GetText("accept") : textButton;
    this.alertInput.transform.gameObject.SetActive(true);
    this.alertInput.text = "";
    this.ShowAlert(true);
    this.StartCoroutine(this.ActivateInput());
  }

  private IEnumerator ActivateInput()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    this.alertInput.Select();
    this.alertInput.ActivateInputField();
  }

  public void SetRestartPosition()
  {
    this.backgroundImage.enabled = false;
    this.popupT.localPosition = new Vector3(this.popupT.localPosition.x, 130f, this.popupT.localPosition.z);
  }

  public void SetInitialPosition()
  {
    this.backgroundImage.enabled = true;
    this.popupT.localPosition = new Vector3(this.popupT.localPosition.x, 40f, this.popupT.localPosition.z);
  }

  public void ControllerMovement(bool goingUp = false, bool goingRight = false, bool goingDown = false, bool goingLeft = false)
  {
    this._controllerList.Clear();
    for (int index = 0; index < this.menuControllerAlert.Count; ++index)
    {
      if (Functions.TransformIsVisible(this.menuControllerAlert[index]))
        this._controllerList.Add(this.menuControllerAlert[index]);
    }
    if (goingUp | goingLeft)
      --this.controllerHorizontalIndex;
    else
      ++this.controllerHorizontalIndex;
    if (this.controllerHorizontalIndex < 0)
      this.controllerHorizontalIndex = 0;
    else if (this.controllerHorizontalIndex > this._controllerList.Count - 1)
      this.controllerHorizontalIndex = this._controllerList.Count - 1;
    if (!((Object) this._controllerList[this.controllerHorizontalIndex] != (Object) null))
      return;
    this.warpPosition = (Vector2) this._controllerList[this.controllerHorizontalIndex].position;
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }

  public void MustSelect(string _charName) => this.AlertConfirm(string.Format(Texts.Instance.GetText("youMustSelectHero"), (object) _charName));

  public delegate void OnButtonClickDelegate();
}
