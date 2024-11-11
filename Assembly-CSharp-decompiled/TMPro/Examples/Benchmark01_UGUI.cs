// Decompiled with JetBrains decompiler
// Type: TMPro.Examples.Benchmark01_UGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro.Examples
{
  public class Benchmark01_UGUI : MonoBehaviour
  {
    public int BenchmarkType;
    public Canvas canvas;
    public TMP_FontAsset TMProFont;
    public Font TextMeshFont;
    private TextMeshProUGUI m_textMeshPro;
    private Text m_textMesh;
    private const string label01 = "The <#0050FF>count is: </color>";
    private const string label02 = "The <color=#0050FF>count is: </color>";
    private Material m_material01;
    private Material m_material02;

    private IEnumerator Start()
    {
      Benchmark01_UGUI benchmark01Ugui = this;
      if (benchmark01Ugui.BenchmarkType == 0)
      {
        benchmark01Ugui.m_textMeshPro = benchmark01Ugui.gameObject.AddComponent<TextMeshProUGUI>();
        if ((Object) benchmark01Ugui.TMProFont != (Object) null)
          benchmark01Ugui.m_textMeshPro.font = benchmark01Ugui.TMProFont;
        benchmark01Ugui.m_textMeshPro.fontSize = 48f;
        benchmark01Ugui.m_textMeshPro.alignment = TextAlignmentOptions.Center;
        benchmark01Ugui.m_textMeshPro.extraPadding = true;
        benchmark01Ugui.m_material01 = benchmark01Ugui.m_textMeshPro.font.material;
        benchmark01Ugui.m_material02 = UnityEngine.Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - BEVEL");
      }
      else if (benchmark01Ugui.BenchmarkType == 1)
      {
        benchmark01Ugui.m_textMesh = benchmark01Ugui.gameObject.AddComponent<Text>();
        if ((Object) benchmark01Ugui.TextMeshFont != (Object) null)
          benchmark01Ugui.m_textMesh.font = benchmark01Ugui.TextMeshFont;
        benchmark01Ugui.m_textMesh.fontSize = 48;
        benchmark01Ugui.m_textMesh.alignment = TextAnchor.MiddleCenter;
      }
      for (int i = 0; i <= 1000000; ++i)
      {
        if (benchmark01Ugui.BenchmarkType == 0)
        {
          benchmark01Ugui.m_textMeshPro.text = "The <#0050FF>count is: </color>" + (i % 1000).ToString();
          if (i % 1000 == 999)
            benchmark01Ugui.m_textMeshPro.fontSharedMaterial = (Object) benchmark01Ugui.m_textMeshPro.fontSharedMaterial == (Object) benchmark01Ugui.m_material01 ? (benchmark01Ugui.m_textMeshPro.fontSharedMaterial = benchmark01Ugui.m_material02) : (benchmark01Ugui.m_textMeshPro.fontSharedMaterial = benchmark01Ugui.m_material01);
        }
        else if (benchmark01Ugui.BenchmarkType == 1)
          benchmark01Ugui.m_textMesh.text = "The <color=#0050FF>count is: </color>" + (i % 1000).ToString();
        yield return (object) null;
      }
      yield return (object) null;
    }
  }
}
