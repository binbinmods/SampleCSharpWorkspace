﻿// Decompiled with JetBrains decompiler
// Type: TMPro.Examples.TeleType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

namespace TMPro.Examples
{
  public class TeleType : MonoBehaviour
  {
    private string label01 = "Example <sprite=2> of using <sprite=7> <#ffa000>Graphics Inline</color> <sprite=5> with Text in <font=\"Bangers SDF\" material=\"Bangers SDF - Drop Shadow\">TextMesh<#40a0ff>Pro</color></font><sprite=0> and Unity<sprite=1>";
    private string label02 = "Example <sprite=2> of using <sprite=7> <#ffa000>Graphics Inline</color> <sprite=5> with Text in <font=\"Bangers SDF\" material=\"Bangers SDF - Drop Shadow\">TextMesh<#40a0ff>Pro</color></font><sprite=0> and Unity<sprite=2>";
    private TMP_Text m_textMeshPro;

    private void Awake()
    {
      this.m_textMeshPro = this.GetComponent<TMP_Text>();
      this.m_textMeshPro.text = this.label01;
      this.m_textMeshPro.enableWordWrapping = true;
      this.m_textMeshPro.alignment = TextAlignmentOptions.Top;
    }

    private IEnumerator Start()
    {
      this.m_textMeshPro.ForceMeshUpdate();
      int totalVisibleCharacters = this.m_textMeshPro.textInfo.characterCount;
      int counter = 0;
      while (true)
      {
        int num = counter % (totalVisibleCharacters + 1);
        this.m_textMeshPro.maxVisibleCharacters = num;
        if (num >= totalVisibleCharacters)
        {
          yield return (object) new WaitForSeconds(1f);
          this.m_textMeshPro.text = this.label02;
          yield return (object) new WaitForSeconds(1f);
          this.m_textMeshPro.text = this.label01;
          yield return (object) new WaitForSeconds(1f);
        }
        ++counter;
        yield return (object) new WaitForSeconds(0.05f);
      }
    }
  }
}
