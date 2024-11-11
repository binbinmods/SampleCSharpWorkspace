// Decompiled with JetBrains decompiler
// Type: EnergyPoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class EnergyPoint : MonoBehaviour
{
  private SpriteRenderer sr;
  private Coroutine Co;

  private void Awake() => this.sr = this.GetComponent<SpriteRenderer>();

  public void Stop()
  {
    if (this.Co == null)
      return;
    this.StopCoroutine(this.Co);
  }
}
