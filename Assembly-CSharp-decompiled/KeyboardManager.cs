// Decompiled with JetBrains decompiler
// Type: KeyboardManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyboardManager : MonoBehaviour
{
  public Transform elements;
  private TMP_InputField inputField;
  public TMP_InputField inputShow;
  public Transform chat;
  public TMP_Text textChat;
  private bool isChat;
  public RectTransform chatScroll;
  public List<TMP_Text> keyList;
  private string stringText = "";
  private bool shiftState = true;
  private int characterLimit;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();

  public static KeyboardManager Instance { get; private set; }

  private void Awake()
  {
    if ((Object) KeyboardManager.Instance == (Object) null)
      KeyboardManager.Instance = this;
    else if ((Object) KeyboardManager.Instance != (Object) this)
      Object.Destroy((Object) this.gameObject);
    Object.DontDestroyOnLoad((Object) this.gameObject);
  }

  private void Start()
  {
    for (int index = 0; index < this.keyList.Count; ++index)
    {
      if ((Object) this.keyList[index] != (Object) null)
      {
        Button button = this.keyList[index].transform.parent.GetComponent<Button>();
        button.onClick.AddListener((UnityAction) (() => KeyboardManager.Instance.DoKey(button.name.ToLower(), button.transform.GetChild(0).GetComponent<TMP_Text>().text)));
        if (Texts.Instance.GetText(button.name) != "")
          button.transform.GetChild(0).GetComponent<TMP_Text>().text = Texts.Instance.GetText(button.name);
      }
    }
    this.ShowKeyboard(false);
  }

  public void ShowChat(bool state)
  {
    if (this.chat.gameObject.activeSelf != state)
      this.chat.gameObject.SetActive(state);
    this.isChat = state;
    if (this.isChat)
    {
      this.ChatText(ChatManager.Instance.chatText.text);
      this.inputField = ChatManager.Instance.chatInput;
      ChatManager.Instance.ClearMessages();
    }
    else
      ChatManager.Instance.chatInput.text = "";
  }

  public bool IsChat() => this.isChat;

  public void ChatText(string text)
  {
    if (!this.isChat)
      return;
    this.textChat.text = text;
    ChatManager.Instance.ClearMessages();
  }

  public void DoKey(string name, string value)
  {
    if ((Object) this.inputField != (Object) null)
      this.stringText = this.inputField.text;
    switch (name)
    {
      case "keyspace":
        this.stringText += " ";
        break;
      case "keydelete":
        this.DoDelete();
        return;
      case "keyshift":
        this.DoShift();
        return;
      case "keyreturn":
        this.DoReturn();
        return;
      default:
        if (this.characterLimit <= 0 || this.stringText.Length < this.characterLimit)
        {
          this.stringText += value;
          break;
        }
        break;
    }
    this.WriteInputText();
  }

  private void WriteInputText()
  {
    this.inputShow.text = this.stringText;
    this.inputShow.MoveTextEnd(true);
    if (!((Object) this.inputField != (Object) null))
      return;
    this.inputField.text = this.stringText;
    this.inputField.MoveTextEnd(true);
  }

  public bool IsActive() => this.elements.gameObject.activeSelf;

  public void ShowKeyboard(bool state, bool chat = false)
  {
    if (this.elements.gameObject.activeSelf != state)
      this.elements.gameObject.SetActive(state);
    this.ShowChat(chat);
    if (state)
    {
      this.controllerHorizontalIndex = 24;
      Mouse.current.WarpCursorPosition((Vector2) this.keyList[this.controllerHorizontalIndex].transform.position);
    }
    else if ((Object) this.inputField != (Object) null && Functions.TransformIsVisible(this.inputField.transform))
      Mouse.current.WarpCursorPosition((Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.inputField.transform.position));
    this.stringText = "";
    this.inputShow.text = "";
    this.inputField = (TMP_InputField) null;
  }

  public void SetInputField(TMP_InputField input)
  {
    this.inputField = input;
    this.stringText = input.text;
    this.characterLimit = input.characterLimit <= 0 ? -1 : input.characterLimit;
    this.WriteInputText();
  }

  public void DoDelete()
  {
    if (this.stringText.Length <= 0)
      return;
    this.stringText = this.stringText.Remove(this.stringText.Length - 1, 1);
    this.WriteInputText();
  }

  public void DoShift()
  {
    this.shiftState = !this.shiftState;
    for (int index = 0; index < this.keyList.Count; ++index)
    {
      if ((Object) this.keyList[index] != (Object) null)
      {
        string lower = this.keyList[index].transform.parent.name.ToLower();
        if (lower != "keyspace" && lower != "keyshift" && lower != "keyreturn" && lower != "keydelete")
          this.keyList[index].text = !this.shiftState ? this.keyList[index].text.ToLower() : this.keyList[index].text.ToUpper();
      }
    }
  }

  public void DoScroll(bool goUp)
  {
    if (goUp)
      this.chatScroll.anchoredPosition = new Vector2(this.chatScroll.anchoredPosition.x, this.chatScroll.anchoredPosition.y - 20f);
    else
      this.chatScroll.anchoredPosition = new Vector2(this.chatScroll.anchoredPosition.x, this.chatScroll.anchoredPosition.y + 20f);
  }

  private void DoReturn()
  {
    if (this.isChat)
      ChatManager.Instance.ChatSend(this.stringText);
    else if (Functions.TransformIsVisible(this.inputField.transform))
      Mouse.current.WarpCursorPosition((Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.inputField.transform.position));
    this.ShowKeyboard(false);
  }

  public void ControllerMovement(bool goingUp = false, bool goingRight = false, bool goingDown = false, bool goingLeft = false)
  {
    this._controllerList.Clear();
    for (int index = 0; index < this.keyList.Count; ++index)
      this._controllerList.Add(this.keyList[index].transform);
    if (this.controllerHorizontalIndex < 0)
      this.controllerHorizontalIndex = 0;
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft, true);
    if (!((Object) this._controllerList[this.controllerHorizontalIndex] != (Object) null))
      return;
    this.warpPosition = (Vector2) this._controllerList[this.controllerHorizontalIndex].position;
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }
}
