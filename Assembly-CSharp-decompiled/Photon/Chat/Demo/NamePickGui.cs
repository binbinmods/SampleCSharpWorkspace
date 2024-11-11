// Decompiled with JetBrains decompiler
// Type: Photon.Chat.Demo.NamePickGui
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace Photon.Chat.Demo
{
  [RequireComponent(typeof (ChatGui))]
  public class NamePickGui : MonoBehaviour
  {
    private const string UserNamePlayerPref = "NamePickUserName";
    public ChatGui chatNewComponent;
    public InputField idInput;

    public void Start()
    {
      this.chatNewComponent = Object.FindObjectOfType<ChatGui>();
      string str = PlayerPrefs.GetString("NamePickUserName");
      if (string.IsNullOrEmpty(str))
        return;
      this.idInput.text = str;
    }

    public void EndEditOnEnter()
    {
      if (!Input.GetKey(KeyCode.Return) && !Input.GetKey(KeyCode.KeypadEnter))
        return;
      this.StartChat();
    }

    public void StartChat()
    {
      ChatGui objectOfType = Object.FindObjectOfType<ChatGui>();
      objectOfType.UserName = this.idInput.text.Trim();
      objectOfType.Connect();
      this.enabled = false;
      PlayerPrefs.SetString("NamePickUserName", objectOfType.UserName);
    }
  }
}
