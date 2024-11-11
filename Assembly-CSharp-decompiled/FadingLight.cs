// Decompiled with JetBrains decompiler
// Type: FadingLight
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FadingLight : MonoBehaviour
{
  private Light myLight;
  public float maxIntensity = 1f;
  public float minIntensity;
  public float pulseSpeed = 1f;
  private float targetIntensity = 1f;
  private float currentIntensity;

  private void Start() => this.myLight = this.GetComponent<Light>();

  private void Update()
  {
    if (Time.frameCount % 3 != 0)
      return;
    this.currentIntensity = Mathf.MoveTowards(this.myLight.intensity, this.targetIntensity, Time.deltaTime * this.pulseSpeed);
    if ((double) this.currentIntensity >= (double) this.maxIntensity)
    {
      this.currentIntensity = this.maxIntensity;
      this.targetIntensity = this.minIntensity;
    }
    else if ((double) this.currentIntensity <= (double) this.minIntensity)
    {
      this.currentIntensity = this.minIntensity;
      this.targetIntensity = this.maxIntensity;
    }
    this.myLight.intensity = this.currentIntensity;
  }
}
