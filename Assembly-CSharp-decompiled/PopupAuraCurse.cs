// Decompiled with JetBrains decompiler
// Type: PopupAuraCurse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PopupAuraCurse : MonoBehaviour
{
  private AuraCurseData acData;
  private int charges;
  private bool fast;

  public void SetAuraCurse(AuraCurseData _acData, int _charges, bool _fast)
  {
    this.acData = _acData;
    this.charges = _charges;
    this.fast = _fast;
  }

  private void OnMouseEnter()
  {
    if (!((Object) this.acData != (Object) null))
      return;
    PopupManager.Instance.SetAuraCurse(this.transform, this.acData, this.charges.ToString(), this.fast);
  }

  private void OnMouseExit() => PopupManager.Instance.ClosePopup();
}
