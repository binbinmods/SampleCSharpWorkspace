// Decompiled with JetBrains decompiler
// Type: CycleColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class CycleColor : MonoBehaviour
{
  private float timeLeft;
  private Color targetColor = new Color(1f, 1f, 1f);
  private Image img;

  private void Awake() => this.img = this.GetComponent<Image>();

  private void Update()
  {
    if ((double) this.timeLeft <= (double) Time.deltaTime)
    {
      this.img.color = this.targetColor;
      this.targetColor = new Color(Random.value, Random.value, Random.value);
      while ((double) this.targetColor.r < 0.20000000298023224 && (double) this.targetColor.g < 0.20000000298023224 && (double) this.targetColor.b < 0.20000000298023224)
        this.targetColor = new Color(Random.value, Random.value, Random.value);
      this.timeLeft = 10f;
    }
    else
    {
      this.img.color = Color.Lerp(this.img.color, this.targetColor, Time.deltaTime / this.timeLeft);
      this.timeLeft -= Time.deltaTime;
    }
  }
}
