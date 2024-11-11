// Decompiled with JetBrains decompiler
// Type: TMPro.Examples.Benchmark01
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

namespace TMPro.Examples
{
  public class Benchmark01 : MonoBehaviour
  {
    public int BenchmarkType;
    public TMP_FontAsset TMProFont;
    public Font TextMeshFont;
    private TextMeshPro m_textMeshPro;
    private TextContainer m_textContainer;
    private TextMesh m_textMesh;
    private const string label01 = "The <#0050FF>count is: </color>{0}";
    private const string label02 = "The <color=#0050FF>count is: </color>";
    private Material m_material01;
    private Material m_material02;

    private IEnumerator Start()
    {
      Benchmark01 benchmark01 = this;
      if (benchmark01.BenchmarkType == 0)
      {
        benchmark01.m_textMeshPro = benchmark01.gameObject.AddComponent<TextMeshPro>();
        benchmark01.m_textMeshPro.autoSizeTextContainer = true;
        if ((Object) benchmark01.TMProFont != (Object) null)
          benchmark01.m_textMeshPro.font = benchmark01.TMProFont;
        benchmark01.m_textMeshPro.fontSize = 48f;
        benchmark01.m_textMeshPro.alignment = TextAlignmentOptions.Center;
        benchmark01.m_textMeshPro.extraPadding = true;
        benchmark01.m_textMeshPro.enableWordWrapping = false;
        benchmark01.m_material01 = benchmark01.m_textMeshPro.font.material;
        benchmark01.m_material02 = UnityEngine.Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - Drop Shadow");
      }
      else if (benchmark01.BenchmarkType == 1)
      {
        benchmark01.m_textMesh = benchmark01.gameObject.AddComponent<TextMesh>();
        if ((Object) benchmark01.TextMeshFont != (Object) null)
        {
          benchmark01.m_textMesh.font = benchmark01.TextMeshFont;
          benchmark01.m_textMesh.GetComponent<Renderer>().sharedMaterial = benchmark01.m_textMesh.font.material;
        }
        else
        {
          benchmark01.m_textMesh.font = UnityEngine.Resources.Load("Fonts/ARIAL", typeof (Font)) as Font;
          benchmark01.m_textMesh.GetComponent<Renderer>().sharedMaterial = benchmark01.m_textMesh.font.material;
        }
        benchmark01.m_textMesh.fontSize = 48;
        benchmark01.m_textMesh.anchor = TextAnchor.MiddleCenter;
      }
      for (int i = 0; i <= 1000000; ++i)
      {
        if (benchmark01.BenchmarkType == 0)
        {
          benchmark01.m_textMeshPro.SetText("The <#0050FF>count is: </color>{0}", (float) (i % 1000));
          if (i % 1000 == 999)
            benchmark01.m_textMeshPro.fontSharedMaterial = (Object) benchmark01.m_textMeshPro.fontSharedMaterial == (Object) benchmark01.m_material01 ? (benchmark01.m_textMeshPro.fontSharedMaterial = benchmark01.m_material02) : (benchmark01.m_textMeshPro.fontSharedMaterial = benchmark01.m_material01);
        }
        else if (benchmark01.BenchmarkType == 1)
          benchmark01.m_textMesh.text = "The <color=#0050FF>count is: </color>" + (i % 1000).ToString();
        yield return (object) null;
      }
      yield return (object) null;
    }
  }
}
