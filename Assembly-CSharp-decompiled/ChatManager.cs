// Decompiled with JetBrains decompiler
// Type: ChatManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ChatManager : MonoBehaviour
{
  public Transform close;
  public Transform open;
  public Transform chatMessages;
  public TMP_Text chatMessagesText;
  public Transform chatGO;
  public TMP_Text chatText;
  private StringBuilder chatSB;
  public TMP_InputField chatInput;
  private List<string> chatContent = new List<string>();
  private string status = "closed";
  private int messages;

  public static ChatManager Instance { get; private set; }

  private void Awake()
  {
    if ((Object) ChatManager.Instance == (Object) null)
      ChatManager.Instance = this;
    else if ((Object) ChatManager.Instance != (Object) this)
      Object.Destroy((Object) this.gameObject);
    Object.DontDestroyOnLoad((Object) this.gameObject);
  }

  private void Start() => this.DisableChat();

  public void ChatSend(string chatStr = "")
  {
    string str1 = this.chatInput.text.Trim();
    if (chatStr != "")
      str1 = chatStr.Trim();
    if (!(str1 != ""))
      return;
    int myPosition = NetworkManager.Instance.GetMyPosition();
    string playerNick = NetworkManager.Instance.GetPlayerNick();
    string str2 = NetworkManager.Instance.ColorFromPosition(myPosition);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<b><color=");
    stringBuilder.Append(str2);
    stringBuilder.Append(">[");
    stringBuilder.Append(NetworkManager.Instance.GetPlayerNickReal(playerNick));
    stringBuilder.Append("]</color></b> ");
    stringBuilder.Append(str1);
    NetworkManager.Instance.ChatSend(stringBuilder.ToString(), true);
    this.ChatText(stringBuilder.ToString(), false);
    this.chatInput.text = string.Empty;
    this.chatInput.ActivateInputField();
  }

  public void WelcomeMsg(string roomName)
  {
    this.chatSB = new StringBuilder();
    this.chatSB.Append("<size=-1><color=#EFEAC5>");
    this.chatSB.Append(string.Format(Texts.Instance.GetText("chatWelcome"), (object) roomName));
    this.chatSB.Append("</color></size>");
    this.chatContent = new List<string>();
    this.chatContent.Add(this.chatSB.ToString());
    this.chatSB.Append("\n");
    this.chatText.text = this.chatSB.ToString();
  }

  public void ChatText(string text, bool showAlertIfClosed)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<size=-2><color=#777>[");
    stringBuilder.Append(Functions.GetTimestampString());
    stringBuilder.Append("]</color></size> ");
    stringBuilder.Append(text);
    this.chatContent.Add(stringBuilder.ToString());
    if (this.chatContent.Count > 20)
      this.chatContent.RemoveAt(0);
    this.chatSB.Clear();
    for (int index = 0; index < this.chatContent.Count; ++index)
    {
      this.chatSB.Append(this.chatContent[index]);
      this.chatSB.Append("\n");
    }
    this.chatText.text = this.chatSB.ToString();
    if ((bool) (Object) KeyboardManager.Instance)
      KeyboardManager.Instance.ChatText(this.chatSB.ToString());
    if (!showAlertIfClosed || !(this.status == "closed"))
      return;
    ++this.messages;
    this.WriteChatMessagesWarning();
  }

  public void ClearMessages()
  {
    this.messages = 0;
    this.WriteChatMessagesWarning();
  }

  private void WriteChatMessagesWarning()
  {
    if (this.messages > 0)
    {
      this.chatMessagesText.text = this.messages.ToString();
      this.chatMessages.gameObject.SetActive(true);
    }
    else
      this.chatMessages.gameObject.SetActive(false);
  }

  public void ChatButton()
  {
    this.ClearMessages();
    if (this.status == "opened")
      this.HideChat();
    else
      this.ShowChat();
  }

  public void ShowChat()
  {
    this.ClearMessages();
    this.chatInput.onSubmit.AddListener(new UnityAction<string>(this.ChatSend));
    this.chatMessages.gameObject.SetActive(false);
    this.chatGO.gameObject.SetActive(true);
    this.close.gameObject.SetActive(true);
    this.open.gameObject.SetActive(false);
    this.status = "closed";
    SaveManager.SaveIntoPrefsBool("collapsedChat", false);
  }

  public void HideChat()
  {
    this.ClearMessages();
    this.chatInput.onSubmit.RemoveListener(new UnityAction<string>(this.ChatSend));
    this.chatGO.gameObject.SetActive(false);
    this.close.gameObject.SetActive(false);
    this.open.gameObject.SetActive(true);
    SaveManager.SaveIntoPrefsBool("collapsedChat", true);
  }

  public void DisableChat()
  {
    if (!((Object) this.gameObject != (Object) null))
      return;
    this.chatText.text = "";
    this.gameObject.SetActive(false);
    this.chatMessages.gameObject.SetActive(false);
  }

  public void EnableChat()
  {
    if (!((Object) this.gameObject != (Object) null))
      return;
    this.gameObject.SetActive(true);
    bool flag = false;
    if (Gamepad.current != null)
      flag = true;
    else if (SaveManager.PrefsHasKey("collapsedChat"))
      flag = SaveManager.LoadPrefsBool("collapsedChat");
    if (!flag)
      this.ShowChat();
    else
      this.HideChat();
  }
}
