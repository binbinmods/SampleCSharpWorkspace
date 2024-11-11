// Decompiled with JetBrains decompiler
// Type: DemoRandomColorSwap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DemoRandomColorSwap : MonoBehaviour
{
  [SerializeField]
  private Gradient gradient;
  private Material mat;
  private Texture texture;

  private void Start()
  {
    if (!((Object) this.GetComponent<SpriteRenderer>() != (Object) null))
      return;
    this.mat = this.GetComponent<Renderer>().material;
    this.mat.SetFloat("_Alpha", 1f);
    this.mat.SetColor("_Color", new Color(0.5f, 1f, 0.0f, 1f));
    this.mat.SetTexture("_MainTex", this.texture);
    this.InvokeRepeating("NewColor", 0.0f, 0.6f);
  }

  private void NewColor()
  {
    this.mat.SetColor("_ColorSwapRed", this.gradient.Evaluate(Random.value));
    this.mat.SetColor("_ColorSwapGreen", this.gradient.Evaluate(Random.value));
    this.mat.SetColor("_ColorSwapBlue", this.gradient.Evaluate(Random.value));
  }
}
