// Decompiled with JetBrains decompiler
// Type: TextFade
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;

public class TextFade : MonoBehaviour
{
  private TMP_Text m_TextComponent;
  public float FadeSpeed = 1f;
  public int RolloverCharacterSpread = 10;
  public Color ColorTint;

  private void Awake() => this.m_TextComponent = this.GetComponent<TMP_Text>();

  private void Start()
  {
    this.m_TextComponent.color = new Color(this.m_TextComponent.color.r, this.m_TextComponent.color.g, this.m_TextComponent.color.b, 0.0f);
    this.StartCoroutine(this.AnimateVertexColors());
  }

  private void OnEnable()
  {
    this.m_TextComponent.color = new Color(this.m_TextComponent.color.r, this.m_TextComponent.color.g, this.m_TextComponent.color.b, 0.0f);
    this.StartCoroutine(this.AnimateVertexColors());
  }

  private IEnumerator AnimateVertexColors()
  {
    this.m_TextComponent.ForceMeshUpdate();
    TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
    Color32 color = (Color32) this.m_TextComponent.color;
    int wordStart = 0;
    int characterCount = textInfo.characterCount;
    if (characterCount == 0)
    {
      this.m_TextComponent.color = new Color(this.m_TextComponent.color.r, this.m_TextComponent.color.g, this.m_TextComponent.color.b, 1f);
    }
    else
    {
      while (true)
      {
        int num1 = wordStart + 3;
        if (num1 > characterCount)
          num1 = characterCount - 1;
        float num2 = 1f;
        for (float num3 = 0.0f; (double) num3 < (double) num2; ++num3)
        {
          byte num4 = (byte) ((double) byte.MaxValue * (((double) num3 + 1.0) / (double) num2));
          for (int index = wordStart; index <= num1 && index < textInfo.characterInfo.Length; ++index)
          {
            Color32[] colors32 = textInfo.meshInfo[textInfo.characterInfo[index].materialReferenceIndex].colors32;
            int vertexIndex = textInfo.characterInfo[index].vertexIndex;
            if (colors32.Length >= vertexIndex + 4)
            {
              colors32[vertexIndex].a = num4;
              colors32[vertexIndex + 1].a = num4;
              colors32[vertexIndex + 2].a = num4;
              colors32[vertexIndex + 3].a = num4;
            }
          }
          this.m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }
        wordStart = num1 + 1;
        if (wordStart <= characterCount)
          yield return (object) null;
        else
          break;
      }
      this.m_TextComponent.color = new Color(this.m_TextComponent.color.r, this.m_TextComponent.color.g, this.m_TextComponent.color.b, 1f);
    }
  }
}
