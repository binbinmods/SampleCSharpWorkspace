// Decompiled with JetBrains decompiler
// Type: SetSpriteLayerFromBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SetSpriteLayerFromBase : MonoBehaviour
{
  public SpriteRenderer sprBase;

  public void ReOrderLayer()
  {
    if ((Object) this.sprBase == (Object) null)
      return;
    if ((Object) this.GetComponent<SpriteRenderer>() != (Object) null)
    {
      SpriteRenderer component = this.GetComponent<SpriteRenderer>();
      component.sortingOrder = this.sprBase.sortingOrder + 1;
      this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.sprBase.transform.localPosition.z - 1f);
      component.sortingLayerName = this.sprBase.sortingLayerName;
    }
    if (!((Object) this.GetComponent<ParticleSystemRenderer>() != (Object) null))
      return;
    ParticleSystemRenderer component1 = this.GetComponent<ParticleSystemRenderer>();
    component1.sortingOrder = this.sprBase.sortingOrder + 1;
    this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.sprBase.transform.localPosition.z - 1f);
    component1.sortingLayerName = this.sprBase.sortingLayerName;
  }
}
