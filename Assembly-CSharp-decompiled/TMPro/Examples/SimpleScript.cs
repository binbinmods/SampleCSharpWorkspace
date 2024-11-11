﻿// Decompiled with JetBrains decompiler
// Type: TMPro.Examples.SimpleScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace TMPro.Examples
{
  public class SimpleScript : MonoBehaviour
  {
    private TextMeshPro m_textMeshPro;
    private const string label = "The <#0050FF>count is: </color>{0:2}";
    private float m_frame;

    private void Start()
    {
      this.m_textMeshPro = this.gameObject.AddComponent<TextMeshPro>();
      this.m_textMeshPro.autoSizeTextContainer = true;
      this.m_textMeshPro.fontSize = 48f;
      this.m_textMeshPro.alignment = TextAlignmentOptions.Center;
      this.m_textMeshPro.enableWordWrapping = false;
    }

    private void Update()
    {
      this.m_textMeshPro.SetText("The <#0050FF>count is: </color>{0:2}", this.m_frame % 1000f);
      this.m_frame += 1f * Time.deltaTime;
    }
  }
}
