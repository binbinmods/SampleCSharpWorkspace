// Decompiled with JetBrains decompiler
// Type: PlayerStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
  public Image lineImg;
  public TMP_Text playerName;
  public Image[] characterImg;
  public Transform arrowOnline;
  public Transform arrowOffline;
  public Image statusImg;

  public void SetPlayer(string nick)
  {
    this.playerName.text = nick;
    this.playerName.color = this.lineImg.color = Functions.HexToColor(NetworkManager.Instance.GetColorFromNick(nick));
    this.HideStatusReady();
  }

  public void SetOnline()
  {
    this.arrowOnline.gameObject.SetActive(true);
    this.arrowOffline.gameObject.SetActive(false);
  }

  public void SetOffline()
  {
    this.arrowOnline.gameObject.SetActive(false);
    this.arrowOffline.gameObject.SetActive(true);
  }

  public void HideStatusReady() => this.statusImg.gameObject.SetActive(false);

  public void ShowStatusReady()
  {
    this.statusImg.gameObject.SetActive(true);
    this.statusImg.color = Functions.HexToColor(Globals.Instance.ClassColor["warrior"]);
  }

  public void SetStatus(bool status)
  {
    if (status)
      this.statusImg.color = Functions.HexToColor(Globals.Instance.ClassColor["scout"]);
    else
      this.statusImg.color = Functions.HexToColor(Globals.Instance.ClassColor["warrior"]);
  }
}
