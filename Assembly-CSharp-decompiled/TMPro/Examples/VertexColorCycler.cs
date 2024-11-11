// Decompiled with JetBrains decompiler
// Type: TMPro.Examples.VertexColorCycler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

namespace TMPro.Examples
{
  public class VertexColorCycler : MonoBehaviour
  {
    private TMP_Text m_TextComponent;

    private void Awake() => this.m_TextComponent = this.GetComponent<TMP_Text>();

    private void Start() => this.StartCoroutine(this.AnimateVertexColors());

    private IEnumerator AnimateVertexColors()
    {
      this.m_TextComponent.ForceMeshUpdate();
      TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
      int currentCharacter = 0;
      Color32 color32 = (Color32) this.m_TextComponent.color;
      while (true)
      {
        int characterCount = textInfo.characterCount;
        if (characterCount == 0)
        {
          yield return (object) new WaitForSeconds(0.25f);
        }
        else
        {
          Color32[] colors32 = textInfo.meshInfo[textInfo.characterInfo[currentCharacter].materialReferenceIndex].colors32;
          int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;
          if (textInfo.characterInfo[currentCharacter].isVisible)
          {
            color32 = new Color32((byte) Random.Range(0, (int) byte.MaxValue), (byte) Random.Range(0, (int) byte.MaxValue), (byte) Random.Range(0, (int) byte.MaxValue), byte.MaxValue);
            colors32[vertexIndex] = color32;
            colors32[vertexIndex + 1] = color32;
            colors32[vertexIndex + 2] = color32;
            colors32[vertexIndex + 3] = color32;
            this.m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
          }
          currentCharacter = (currentCharacter + 1) % characterCount;
          yield return (object) new WaitForSeconds(0.05f);
        }
      }
    }
  }
}
