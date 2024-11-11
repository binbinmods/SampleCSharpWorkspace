// Decompiled with JetBrains decompiler
// Type: BotonFilter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class BotonFilter : MonoBehaviour
{
  public Transform border;
  public Transform bg;
  public string id;
  public string type;
  private bool active;

  private void Awake()
  {
    this.ShowBorder(false);
    this.select(false);
  }

  private void ShowBorder(bool state) => this.border.gameObject.SetActive(state);

  private void ShowBackground(bool state) => this.bg.gameObject.SetActive(state);

  public void select(bool state)
  {
    this.active = state;
    this.ShowBackground(state);
  }

  private void OnMouseEnter()
  {
    this.ShowBorder(true);
    if (!(this.id != ""))
      return;
    PopupManager.Instance.SetText(Texts.Instance.GetText(this.id), true);
  }

  private void OnMouseExit()
  {
    this.ShowBorder(false);
    PopupManager.Instance.ClosePopup();
  }

  private void OnMouseUp()
  {
    this.select(!this.active);
    CardCraftManager.Instance.SelectFilter(this.type, this.id, this.active);
  }
}
