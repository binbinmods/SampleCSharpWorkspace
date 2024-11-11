// Decompiled with JetBrains decompiler
// Type: TMPro.Examples.VertexZoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro.Examples
{
  public class VertexZoom : MonoBehaviour
  {
    public float AngleMultiplier = 1f;
    public float SpeedMultiplier = 1f;
    public float CurveScale = 1f;
    private TMP_Text m_TextComponent;
    private bool hasTextChanged;

    private void Awake() => this.m_TextComponent = this.GetComponent<TMP_Text>();

    private void OnEnable() => TMPro_EventManager.TEXT_CHANGED_EVENT.Add(new Action<UnityEngine.Object>(this.ON_TEXT_CHANGED));

    private void OnDisable() => TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(new Action<UnityEngine.Object>(this.ON_TEXT_CHANGED));

    private void Start() => this.StartCoroutine(this.AnimateVertexColors());

    private void ON_TEXT_CHANGED(UnityEngine.Object obj)
    {
      if (!(obj == (UnityEngine.Object) this.m_TextComponent))
        return;
      this.hasTextChanged = true;
    }

    private IEnumerator AnimateVertexColors()
    {
      this.m_TextComponent.ForceMeshUpdate();
      TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
      TMP_MeshInfo[] cachedMeshInfoVertexData = textInfo.CopyMeshInfoVertexData();
      List<float> modifiedCharScale = new List<float>();
      List<int> scaleSortingOrder = new List<int>();
      this.hasTextChanged = true;
      while (true)
      {
        if (this.hasTextChanged)
        {
          cachedMeshInfoVertexData = textInfo.CopyMeshInfoVertexData();
          this.hasTextChanged = false;
        }
        int characterCount = textInfo.characterCount;
        if (characterCount == 0)
        {
          yield return (object) new WaitForSeconds(0.25f);
        }
        else
        {
          modifiedCharScale.Clear();
          scaleSortingOrder.Clear();
          for (int index = 0; index < characterCount; ++index)
          {
            if (textInfo.characterInfo[index].isVisible)
            {
              int materialReferenceIndex = textInfo.characterInfo[index].materialReferenceIndex;
              int vertexIndex = textInfo.characterInfo[index].vertexIndex;
              Vector3[] vertices1 = cachedMeshInfoVertexData[materialReferenceIndex].vertices;
              Vector3 vector3 = (Vector3) (Vector2) ((vertices1[vertexIndex] + vertices1[vertexIndex + 2]) / 2f);
              Vector3[] vertices2 = textInfo.meshInfo[materialReferenceIndex].vertices;
              vertices2[vertexIndex] = vertices1[vertexIndex] - vector3;
              vertices2[vertexIndex + 1] = vertices1[vertexIndex + 1] - vector3;
              vertices2[vertexIndex + 2] = vertices1[vertexIndex + 2] - vector3;
              vertices2[vertexIndex + 3] = vertices1[vertexIndex + 3] - vector3;
              float num = UnityEngine.Random.Range(1f, 1.5f);
              modifiedCharScale.Add(num);
              scaleSortingOrder.Add(modifiedCharScale.Count - 1);
              Matrix4x4 matrix4x4 = Matrix4x4.TRS(new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, Vector3.one * num);
              vertices2[vertexIndex] = matrix4x4.MultiplyPoint3x4(vertices2[vertexIndex]);
              vertices2[vertexIndex + 1] = matrix4x4.MultiplyPoint3x4(vertices2[vertexIndex + 1]);
              vertices2[vertexIndex + 2] = matrix4x4.MultiplyPoint3x4(vertices2[vertexIndex + 2]);
              vertices2[vertexIndex + 3] = matrix4x4.MultiplyPoint3x4(vertices2[vertexIndex + 3]);
              vertices2[vertexIndex] += vector3;
              vertices2[vertexIndex + 1] += vector3;
              vertices2[vertexIndex + 2] += vector3;
              vertices2[vertexIndex + 3] += vector3;
              Vector2[] uvs0_1 = cachedMeshInfoVertexData[materialReferenceIndex].uvs0;
              Vector2[] uvs0_2 = textInfo.meshInfo[materialReferenceIndex].uvs0;
              uvs0_2[vertexIndex] = uvs0_1[vertexIndex];
              uvs0_2[vertexIndex + 1] = uvs0_1[vertexIndex + 1];
              uvs0_2[vertexIndex + 2] = uvs0_1[vertexIndex + 2];
              uvs0_2[vertexIndex + 3] = uvs0_1[vertexIndex + 3];
              Color32[] colors32_1 = cachedMeshInfoVertexData[materialReferenceIndex].colors32;
              Color32[] colors32_2 = textInfo.meshInfo[materialReferenceIndex].colors32;
              colors32_2[vertexIndex] = colors32_1[vertexIndex];
              colors32_2[vertexIndex + 1] = colors32_1[vertexIndex + 1];
              colors32_2[vertexIndex + 2] = colors32_1[vertexIndex + 2];
              colors32_2[vertexIndex + 3] = colors32_1[vertexIndex + 3];
            }
          }
          for (int index = 0; index < textInfo.meshInfo.Length; ++index)
          {
            scaleSortingOrder.Sort((Comparison<int>) ((a, b) => modifiedCharScale[a].CompareTo(modifiedCharScale[b])));
            textInfo.meshInfo[index].SortGeometry((IList<int>) scaleSortingOrder);
            textInfo.meshInfo[index].mesh.vertices = textInfo.meshInfo[index].vertices;
            textInfo.meshInfo[index].mesh.uv = textInfo.meshInfo[index].uvs0;
            textInfo.meshInfo[index].mesh.colors32 = textInfo.meshInfo[index].colors32;
            this.m_TextComponent.UpdateGeometry(textInfo.meshInfo[index].mesh, index);
          }
          yield return (object) new WaitForSeconds(0.1f);
        }
      }
    }
  }
}
