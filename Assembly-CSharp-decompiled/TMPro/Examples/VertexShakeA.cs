// Decompiled with JetBrains decompiler
// Type: TMPro.Examples.VertexShakeA
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

namespace TMPro.Examples
{
  public class VertexShakeA : MonoBehaviour
  {
    public float AngleMultiplier = 1f;
    public float SpeedMultiplier = 1f;
    public float ScaleMultiplier = 1f;
    public float RotationMultiplier = 1f;
    private TMP_Text m_TextComponent;
    private bool hasTextChanged;

    private void Awake() => this.m_TextComponent = this.GetComponent<TMP_Text>();

    private void OnEnable() => TMPro_EventManager.TEXT_CHANGED_EVENT.Add(new Action<UnityEngine.Object>(this.ON_TEXT_CHANGED));

    private void OnDisable() => TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(new Action<UnityEngine.Object>(this.ON_TEXT_CHANGED));

    private void Start() => this.StartCoroutine(this.AnimateVertexColors());

    private void ON_TEXT_CHANGED(UnityEngine.Object obj)
    {
      if (!(bool) (obj = (UnityEngine.Object) this.m_TextComponent))
        return;
      this.hasTextChanged = true;
    }

    private IEnumerator AnimateVertexColors()
    {
      this.m_TextComponent.ForceMeshUpdate();
      TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
      Vector3[][] copyOfVertices = new Vector3[0][];
      this.hasTextChanged = true;
      while (true)
      {
        if (this.hasTextChanged)
        {
          if (copyOfVertices.Length < textInfo.meshInfo.Length)
            copyOfVertices = new Vector3[textInfo.meshInfo.Length][];
          for (int index = 0; index < textInfo.meshInfo.Length; ++index)
          {
            int length = textInfo.meshInfo[index].vertices.Length;
            copyOfVertices[index] = new Vector3[length];
          }
          this.hasTextChanged = false;
        }
        if (textInfo.characterCount == 0)
        {
          yield return (object) new WaitForSeconds(0.25f);
        }
        else
        {
          int lineCount = textInfo.lineCount;
          for (int index1 = 0; index1 < lineCount; ++index1)
          {
            int firstCharacterIndex = textInfo.lineInfo[index1].firstCharacterIndex;
            int lastCharacterIndex = textInfo.lineInfo[index1].lastCharacterIndex;
            Vector3 vector3 = (textInfo.characterInfo[firstCharacterIndex].bottomLeft + textInfo.characterInfo[lastCharacterIndex].topRight) / 2f;
            Quaternion q = Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(-0.25f, 0.25f) * this.RotationMultiplier);
            for (int index2 = firstCharacterIndex; index2 <= lastCharacterIndex; ++index2)
            {
              if (textInfo.characterInfo[index2].isVisible)
              {
                int materialReferenceIndex = textInfo.characterInfo[index2].materialReferenceIndex;
                int vertexIndex = textInfo.characterInfo[index2].vertexIndex;
                Vector3[] vertices = textInfo.meshInfo[materialReferenceIndex].vertices;
                copyOfVertices[materialReferenceIndex][vertexIndex] = vertices[vertexIndex] - vector3;
                copyOfVertices[materialReferenceIndex][vertexIndex + 1] = vertices[vertexIndex + 1] - vector3;
                copyOfVertices[materialReferenceIndex][vertexIndex + 2] = vertices[vertexIndex + 2] - vector3;
                copyOfVertices[materialReferenceIndex][vertexIndex + 3] = vertices[vertexIndex + 3] - vector3;
                float num = UnityEngine.Random.Range((float) (0.99500000476837158 - 1.0 / 1000.0 * (double) this.ScaleMultiplier), (float) (1.0049999952316284 + 1.0 / 1000.0 * (double) this.ScaleMultiplier));
                Matrix4x4 matrix4x4 = Matrix4x4.TRS(Vector3.one, q, Vector3.one * num);
                copyOfVertices[materialReferenceIndex][vertexIndex] = matrix4x4.MultiplyPoint3x4(copyOfVertices[materialReferenceIndex][vertexIndex]);
                copyOfVertices[materialReferenceIndex][vertexIndex + 1] = matrix4x4.MultiplyPoint3x4(copyOfVertices[materialReferenceIndex][vertexIndex + 1]);
                copyOfVertices[materialReferenceIndex][vertexIndex + 2] = matrix4x4.MultiplyPoint3x4(copyOfVertices[materialReferenceIndex][vertexIndex + 2]);
                copyOfVertices[materialReferenceIndex][vertexIndex + 3] = matrix4x4.MultiplyPoint3x4(copyOfVertices[materialReferenceIndex][vertexIndex + 3]);
                copyOfVertices[materialReferenceIndex][vertexIndex] += vector3;
                copyOfVertices[materialReferenceIndex][vertexIndex + 1] += vector3;
                copyOfVertices[materialReferenceIndex][vertexIndex + 2] += vector3;
                copyOfVertices[materialReferenceIndex][vertexIndex + 3] += vector3;
              }
            }
          }
          for (int index = 0; index < textInfo.meshInfo.Length; ++index)
          {
            textInfo.meshInfo[index].mesh.vertices = copyOfVertices[index];
            this.m_TextComponent.UpdateGeometry(textInfo.meshInfo[index].mesh, index);
          }
          yield return (object) new WaitForSeconds(0.1f);
        }
      }
    }
  }
}
