// Decompiled with JetBrains decompiler
// Type: Photon.Chat.Demo.ChannelSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Photon.Chat.Demo
{
  public class ChannelSelector : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
  {
    public string Channel;

    public void SetChannel(string channel)
    {
      this.Channel = channel;
      this.GetComponentInChildren<Text>().text = this.Channel;
    }

    public void OnPointerClick(PointerEventData eventData) => Object.FindObjectOfType<ChatGui>().ShowChannel(this.Channel);
  }
}
