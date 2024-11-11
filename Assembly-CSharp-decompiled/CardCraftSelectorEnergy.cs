// Decompiled with JetBrains decompiler
// Type: CardCraftSelectorEnergy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class CardCraftSelectorEnergy : MonoBehaviour
{
  public TMP_Text tText;
  public Transform tOn;
  public Transform tOff;
  private bool enabled;

  private void Start() => this.SetEnable(false);

  public void SetText(string text) => this.tText.text = text;

  public void SetEnable(bool state)
  {
    this.enabled = state;
    if (state)
    {
      this.tOn.gameObject.SetActive(true);
      this.tOff.gameObject.SetActive(false);
    }
    else
    {
      this.tOn.gameObject.SetActive(false);
      this.tOff.gameObject.SetActive(true);
    }
  }

  private void OnMouseEnter()
  {
    if (this.enabled)
      return;
    this.tOn.gameObject.SetActive(true);
    this.tOff.gameObject.SetActive(false);
  }

  private void OnMouseExit()
  {
    if (this.enabled)
      return;
    this.tOn.gameObject.SetActive(false);
    this.tOff.gameObject.SetActive(true);
  }

  private void OnMouseUp()
  {
    this.SetEnable(true);
    CardCraftManager.Instance.CraftSelectorEnergy(this, this.tText.text);
  }
}
