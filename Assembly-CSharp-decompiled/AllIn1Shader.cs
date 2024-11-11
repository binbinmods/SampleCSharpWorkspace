// Decompiled with JetBrains decompiler
// Type: AllIn1Shader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.IO;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[AddComponentMenu("AllIn1SpriteShader/AddAllIn1Shader")]
public class AllIn1Shader : MonoBehaviour
{
  private Material currMaterial;
  private Material prevMaterial;
  private bool matAssigned;
  private bool destroyed;

  public void MakeNewMaterial() => this.SetMaterial(AllIn1Shader.AfterSetAction.Clear);

  public void MakeCopy() => this.SetMaterial(AllIn1Shader.AfterSetAction.CopyMaterial);

  private void ResetAllProperties() => this.SetMaterial(AllIn1Shader.AfterSetAction.Reset);

  private void SetMaterial(AllIn1Shader.AfterSetAction action)
  {
    Shader shader = UnityEngine.Resources.Load("AllIn1SpriteShader", typeof (Shader)) as Shader;
    if (!Application.isPlaying && Application.isEditor && (Object) shader != (Object) null)
    {
      bool flag = false;
      if ((Object) this.GetComponent<SpriteRenderer>() != (Object) null)
      {
        flag = true;
        this.prevMaterial = new Material(this.GetComponent<Renderer>().sharedMaterial);
        this.currMaterial = new Material(shader);
        this.GetComponent<Renderer>().sharedMaterial = this.currMaterial;
        this.GetComponent<Renderer>().sharedMaterial.hideFlags = HideFlags.None;
        this.matAssigned = true;
        this.DoAfterSetAction(action);
      }
      else
      {
        Image component = this.GetComponent<Image>();
        if ((Object) component != (Object) null)
        {
          flag = true;
          this.prevMaterial = new Material(component.material);
          this.currMaterial = new Material(shader);
          component.material = this.currMaterial;
          component.material.hideFlags = HideFlags.None;
          this.matAssigned = true;
          this.DoAfterSetAction(action);
        }
      }
      if (!flag)
        this.MissingRenderer();
      else
        this.SetSceneDirty();
    }
    else
    {
      if (!((Object) shader == (Object) null))
        return;
      Debug.LogError((object) "Make sure the AllIn1SpriteShader file is inside the Resource folder!");
    }
  }

  private void DoAfterSetAction(AllIn1Shader.AfterSetAction action)
  {
    if (action != AllIn1Shader.AfterSetAction.Clear)
    {
      if (action != AllIn1Shader.AfterSetAction.CopyMaterial)
        return;
      this.currMaterial.CopyPropertiesFromMaterial(this.prevMaterial);
    }
    else
      this.ClearAllKeywords();
  }

  public void TryCreateNew()
  {
    bool flag = false;
    if ((Object) this.GetComponent<SpriteRenderer>() != (Object) null)
    {
      flag = true;
      Renderer component = this.GetComponent<Renderer>();
      if ((Object) component != (Object) null && (Object) component.sharedMaterial != (Object) null && component.sharedMaterial.name.Contains("AllIn1"))
      {
        this.ResetAllProperties();
        this.ClearAllKeywords();
      }
      else
      {
        this.CleanMaterial();
        this.MakeNewMaterial();
      }
    }
    else
    {
      Image component = this.GetComponent<Image>();
      if ((Object) component != (Object) null)
      {
        flag = true;
        if (component.material.name.Contains("AllIn1"))
        {
          this.ResetAllProperties();
          this.ClearAllKeywords();
        }
        else
          this.MakeNewMaterial();
      }
    }
    if (!flag)
      this.MissingRenderer();
    this.SetSceneDirty();
  }

  public void ClearAllKeywords()
  {
    this.SetKeyword("RECTSIZE_ON");
    this.SetKeyword("OFFSETUV_ON");
    this.SetKeyword("CLIPPING_ON");
    this.SetKeyword("POLARUV_ON");
    this.SetKeyword("TWISTUV_ON");
    this.SetKeyword("ROTATEUV_ON");
    this.SetKeyword("FISHEYE_ON");
    this.SetKeyword("PINCH_ON");
    this.SetKeyword("SHAKEUV_ON");
    this.SetKeyword("WAVEUV_ON");
    this.SetKeyword("ROUNDWAVEUV_ON");
    this.SetKeyword("DOODLE_ON");
    this.SetKeyword("ZOOMUV_ON");
    this.SetKeyword("FADE_ON");
    this.SetKeyword("TEXTURESCROLL_ON");
    this.SetKeyword("GLOW_ON");
    this.SetKeyword("OUTBASE_ON");
    this.SetKeyword("ONLYOUTLINE_ON");
    this.SetKeyword("OUTTEX_ON");
    this.SetKeyword("OUTDIST_ON");
    this.SetKeyword("DISTORT_ON");
    this.SetKeyword("WIND_ON");
    this.SetKeyword("GRADIENT_ON");
    this.SetKeyword("COLORSWAP_ON");
    this.SetKeyword("HSV_ON");
    this.SetKeyword("HITEFFECT_ON");
    this.SetKeyword("PIXELATE_ON");
    this.SetKeyword("NEGATIVE_ON");
    this.SetKeyword("COLORRAMP_ON");
    this.SetKeyword("GREYSCALE_ON");
    this.SetKeyword("POSTERIZE_ON");
    this.SetKeyword("BLUR_ON");
    this.SetKeyword("MOTIONBLUR_ON");
    this.SetKeyword("GHOST_ON");
    this.SetKeyword("INNEROUTLINE_ON");
    this.SetKeyword("ONLYINNEROUTLINE_ON");
    this.SetKeyword("HOLOGRAM_ON");
    this.SetKeyword("CHROMABERR_ON");
    this.SetKeyword("GLITCH_ON");
    this.SetKeyword("FLICKER_ON");
    this.SetKeyword("SHADOW_ON");
    this.SetKeyword("ALPHACUTOFF_ON");
    this.SetKeyword("CHANGECOLOR_ON");
    this.SetSceneDirty();
  }

  private void SetKeyword(string keyword, bool state = false)
  {
    if (this.destroyed)
      return;
    if ((Object) this.currMaterial == (Object) null)
    {
      this.FindCurrMaterial();
      if ((Object) this.currMaterial == (Object) null)
      {
        this.MissingRenderer();
        return;
      }
    }
    if (!state)
      this.currMaterial.DisableKeyword(keyword);
    else
      this.currMaterial.EnableKeyword(keyword);
  }

  private void FindCurrMaterial()
  {
    if ((Object) this.GetComponent<SpriteRenderer>() != (Object) null)
    {
      this.currMaterial = this.GetComponent<Renderer>().sharedMaterial;
      this.matAssigned = true;
    }
    else
    {
      Image component = this.GetComponent<Image>();
      if (!((Object) component != (Object) null))
        return;
      this.currMaterial = component.material;
      this.matAssigned = true;
    }
  }

  public void CleanMaterial()
  {
    if ((Object) this.GetComponent<SpriteRenderer>() != (Object) null)
    {
      this.GetComponent<Renderer>().sharedMaterial = new Material(Shader.Find("Sprites/Default"));
      this.matAssigned = false;
    }
    else
    {
      Image component = this.GetComponent<Image>();
      if ((Object) component != (Object) null)
      {
        component.material = new Material(Shader.Find("Sprites/Default"));
        this.matAssigned = false;
      }
    }
    this.SetSceneDirty();
  }

  public void SaveMaterial()
  {
  }

  private void SaveMaterialWithOtherName(string path, int i = 1)
  {
    int num = i;
    string str = path + "_" + num.ToString() + ".mat";
    if (File.Exists(str))
    {
      int i1 = num + 1;
      this.SaveMaterialWithOtherName(path, i1);
    }
    else
      this.DoSaving(str);
  }

  private void DoSaving(string fileName)
  {
  }

  private void SetSceneDirty()
  {
  }

  private void MissingRenderer()
  {
  }

  public void ToggleSetAtlasUvs(bool activate)
  {
    SetAtlasUvs setAtlasUvs = this.GetComponent<SetAtlasUvs>();
    if (activate)
    {
      if ((Object) setAtlasUvs == (Object) null)
        setAtlasUvs = this.gameObject.AddComponent<SetAtlasUvs>();
      setAtlasUvs.GetAndSetUVs();
      this.SetKeyword("ATLAS_ON", true);
    }
    else
    {
      if ((Object) setAtlasUvs != (Object) null)
      {
        setAtlasUvs.ResetAtlasUvs();
        Object.DestroyImmediate((Object) setAtlasUvs);
      }
      this.SetKeyword("ATLAS_ON");
    }
    this.SetSceneDirty();
  }

  private enum AfterSetAction
  {
    Clear,
    CopyMaterial,
    Reset,
  }
}
