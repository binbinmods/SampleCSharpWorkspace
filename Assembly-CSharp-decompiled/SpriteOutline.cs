// Decompiled with JetBrains decompiler
// Type: SpriteOutline
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SpriteOutline : MonoBehaviour
{
  public Color color = Color.white;
  [Range(0.0f, 16f)]
  public float outlineSize = 1f;
  public bool autoResize = true;
  private float outlineSizeDest;
  private float outlineSizeShow = 3f;
  private SpriteRenderer spriteRenderer;
  private Color colorWhite = new Color(1f, 0.8f, 0.0f, 0.9f);
  private Color colorRed = new Color(1f, 0.0f, 0.0f, 0.5f);
  private Color colorGreen = new Color(0.0f, 1f, 0.0f, 0.5f);

  public void EnableGreen()
  {
    if (!(this.color != this.colorGreen))
      return;
    this.color = this.colorGreen;
    if (this.autoResize)
      this.outlineSizeDest = this.outlineSizeShow;
    else
      this.outlineSize = this.outlineSizeDest;
    this.UpdateOutline(true);
  }

  public void EnableRed()
  {
    if (!(this.color != this.colorRed))
      return;
    this.color = this.colorRed;
    if (this.autoResize)
      this.outlineSizeDest = this.outlineSizeShow;
    else
      this.outlineSize = this.outlineSizeDest;
    this.UpdateOutline(true);
  }

  public void EnableWhite()
  {
    if (!(this.color != this.colorWhite))
      return;
    this.color = this.colorWhite;
    if (this.autoResize)
      this.outlineSizeDest = this.outlineSizeShow;
    else
      this.outlineSize = this.outlineSizeDest;
    this.UpdateOutline(true);
  }

  public void Hide()
  {
    this.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    this.outlineSizeDest = 0.0f;
  }

  private void OnEnable()
  {
    this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    this.UpdateOutline(true);
  }

  private void OnDisable() => this.UpdateOutline(false);

  private void UpdateOutline(bool outline)
  {
    MaterialPropertyBlock properties = new MaterialPropertyBlock();
    this.spriteRenderer.GetPropertyBlock(properties);
    properties.SetFloat("_Outline", outline ? 1f : 0.0f);
    properties.SetColor("_OutlineColor", this.color);
    properties.SetFloat("_OutlineSize", this.outlineSize);
    this.spriteRenderer.SetPropertyBlock(properties);
  }
}
