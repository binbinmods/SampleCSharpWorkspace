// Decompiled with JetBrains decompiler
// Type: PopupText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PopupText : MonoBehaviour
{
  public string id;
  public string position = "";
  public string text = "";

  public void SetId(string _id) => this.id = _id;

  private void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive())
      return;
    if (this.id.Trim() != "")
    {
      PopupManager.Instance.SetText(Texts.Instance.GetText(this.id), true, this.position, true);
    }
    else
    {
      if (!(this.text.Trim() != ""))
        return;
      PopupManager.Instance.SetText(this.text.Trim(), true, this.position, true);
    }
  }

  private void OnMouseExit() => PopupManager.Instance.ClosePopup();
}
