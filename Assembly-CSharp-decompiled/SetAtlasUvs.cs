// Decompiled with JetBrains decompiler
// Type: SetAtlasUvs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SetAtlasUvs : MonoBehaviour
{
  [SerializeField]
  private bool updateEveryFrame;
  private Renderer render;
  private SpriteRenderer spriteRender;
  private Image uiImage;
  private bool isUI;

  private void Start()
  {
    if (this.GetRendererReferencesIfNeeded())
      this.GetAndSetUVs();
    if (this.updateEveryFrame || !Application.isPlaying)
      return;
    this.enabled = false;
  }

  private void OnWillRenderObject()
  {
    if (!this.updateEveryFrame)
      return;
    this.GetAndSetUVs();
  }

  public void GetAndSetUVs()
  {
    if (!this.GetRendererReferencesIfNeeded())
      return;
    if (!this.isUI)
    {
      Rect rect = this.spriteRender.sprite.rect;
      rect.x /= (float) this.spriteRender.sprite.texture.width;
      rect.width /= (float) this.spriteRender.sprite.texture.width;
      rect.y /= (float) this.spriteRender.sprite.texture.height;
      rect.height /= (float) this.spriteRender.sprite.texture.height;
      this.render.sharedMaterial.SetFloat("_MinXUV", rect.xMin);
      this.render.sharedMaterial.SetFloat("_MaxXUV", rect.xMax);
      this.render.sharedMaterial.SetFloat("_MinYUV", rect.yMin);
      this.render.sharedMaterial.SetFloat("_MaxYUV", rect.yMax);
    }
    else
    {
      Rect rect = this.uiImage.sprite.rect;
      rect.x /= (float) this.uiImage.sprite.texture.width;
      rect.width /= (float) this.uiImage.sprite.texture.width;
      rect.y /= (float) this.uiImage.sprite.texture.height;
      rect.height /= (float) this.uiImage.sprite.texture.height;
      this.uiImage.material.SetFloat("_MinXUV", rect.xMin);
      this.uiImage.material.SetFloat("_MaxXUV", rect.xMax);
      this.uiImage.material.SetFloat("_MinYUV", rect.yMin);
      this.uiImage.material.SetFloat("_MaxYUV", rect.yMax);
    }
  }

  public void ResetAtlasUvs()
  {
    if (!this.GetRendererReferencesIfNeeded())
      return;
    if (!this.isUI)
    {
      this.render.sharedMaterial.SetFloat("_MinXUV", 0.0f);
      this.render.sharedMaterial.SetFloat("_MaxXUV", 1f);
      this.render.sharedMaterial.SetFloat("_MinYUV", 0.0f);
      this.render.sharedMaterial.SetFloat("_MaxYUV", 1f);
    }
    else
    {
      this.uiImage.material.SetFloat("_MinXUV", 0.0f);
      this.uiImage.material.SetFloat("_MaxXUV", 1f);
      this.uiImage.material.SetFloat("_MinYUV", 0.0f);
      this.uiImage.material.SetFloat("_MaxYUV", 1f);
    }
  }

  public void UpdateEveryFrame(bool everyFrame) => this.updateEveryFrame = everyFrame;

  private bool GetRendererReferencesIfNeeded()
  {
    if ((Object) this.spriteRender == (Object) null)
      this.spriteRender = this.GetComponent<SpriteRenderer>();
    if ((Object) this.spriteRender != (Object) null)
    {
      if ((Object) this.spriteRender.sprite == (Object) null)
        return false;
      if ((Object) this.render == (Object) null)
        this.render = this.GetComponent<Renderer>();
      this.isUI = false;
    }
    else
    {
      if ((Object) this.uiImage == (Object) null)
      {
        this.uiImage = this.GetComponent<Image>();
        if (!((Object) this.uiImage != (Object) null))
        {
          Object.DestroyImmediate((Object) this);
          return false;
        }
      }
      if ((Object) this.render == (Object) null)
        this.render = this.GetComponent<Renderer>();
      this.isUI = true;
    }
    if (!((Object) this.spriteRender == (Object) null) || !((Object) this.uiImage == (Object) null))
      return true;
    Object.DestroyImmediate((Object) this);
    return false;
  }
}
