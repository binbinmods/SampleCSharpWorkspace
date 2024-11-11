// Decompiled with JetBrains decompiler
// Type: TomeCard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class TomeCard : MonoBehaviour
{
  public Transform buttonGold;
  public Transform buttonBlue;
  public Transform buttonRare;

  public void ShowButtons(bool state)
  {
    this.buttonGold.gameObject.SetActive(state);
    this.buttonBlue.gameObject.SetActive(state);
  }

  public void ShowButtonRare(bool state) => this.buttonRare.gameObject.SetActive(state);
}
