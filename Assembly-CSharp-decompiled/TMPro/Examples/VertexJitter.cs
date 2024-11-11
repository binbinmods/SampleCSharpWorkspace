// Decompiled with JetBrains decompiler
// Type: TMPro.Examples.VertexJitter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

namespace TMPro.Examples
{
  public class VertexJitter : MonoBehaviour
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
      int loopCount = 0;
      this.hasTextChanged = true;
      VertexJitter.VertexAnim[] vertexAnim = new VertexJitter.VertexAnim[1024];
      for (int index = 0; index < 1024; ++index)
      {
        vertexAnim[index].angleRange = UnityEngine.Random.Range(10f, 25f);
        vertexAnim[index].speed = UnityEngine.Random.Range(1f, 3f);
      }
      TMP_MeshInfo[] cachedMeshInfo = textInfo.CopyMeshInfoVertexData();
      while (true)
      {
        if (this.hasTextChanged)
        {
          cachedMeshInfo = textInfo.CopyMeshInfoVertexData();
          this.hasTextChanged = false;
        }
        int characterCount = textInfo.characterCount;
        if (characterCount == 0)
        {
          yield return (object) Globals.Instance.WaitForSeconds(0.25f);
        }
        else
        {
          for (int index = 0; index < characterCount; ++index)
          {
            if (textInfo.characterInfo[index].isVisible)
            {
              VertexJitter.VertexAnim vertexAnim1 = vertexAnim[index];
              int materialReferenceIndex = textInfo.characterInfo[index].materialReferenceIndex;
              int vertexIndex = textInfo.characterInfo[index].vertexIndex;
              Vector3[] vertices1 = cachedMeshInfo[materialReferenceIndex].vertices;
              Vector3 vector3 = (Vector3) (Vector2) ((vertices1[vertexIndex] + vertices1[vertexIndex + 2]) / 2f);
              Vector3[] vertices2 = textInfo.meshInfo[materialReferenceIndex].vertices;
              vertices2[vertexIndex] = vertices1[vertexIndex] - vector3;
              vertices2[vertexIndex + 1] = vertices1[vertexIndex + 1] - vector3;
              vertices2[vertexIndex + 2] = vertices1[vertexIndex + 2] - vector3;
              vertices2[vertexIndex + 3] = vertices1[vertexIndex + 3] - vector3;
              vertexAnim1.angle = Mathf.SmoothStep(-vertexAnim1.angleRange, vertexAnim1.angleRange, Mathf.PingPong((float) loopCount / 25f * vertexAnim1.speed, 1f));
              Matrix4x4 matrix4x4 = Matrix4x4.TRS(new Vector3(UnityEngine.Random.Range(-0.25f, 0.25f), UnityEngine.Random.Range(-0.25f, 0.25f), 0.0f) * this.CurveScale, Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(-5f, 5f) * this.AngleMultiplier), Vector3.one);
              vertices2[vertexIndex] = matrix4x4.MultiplyPoint3x4(vertices2[vertexIndex]);
              vertices2[vertexIndex + 1] = matrix4x4.MultiplyPoint3x4(vertices2[vertexIndex + 1]);
              vertices2[vertexIndex + 2] = matrix4x4.MultiplyPoint3x4(vertices2[vertexIndex + 2]);
              vertices2[vertexIndex + 3] = matrix4x4.MultiplyPoint3x4(vertices2[vertexIndex + 3]);
              vertices2[vertexIndex] += vector3;
              vertices2[vertexIndex + 1] += vector3;
              vertices2[vertexIndex + 2] += vector3;
              vertices2[vertexIndex + 3] += vector3;
              vertexAnim[index] = vertexAnim1;
            }
          }
          for (int index = 0; index < textInfo.meshInfo.Length; ++index)
          {
            textInfo.meshInfo[index].mesh.vertices = textInfo.meshInfo[index].vertices;
            this.m_TextComponent.UpdateGeometry(textInfo.meshInfo[index].mesh, index);
          }
          ++loopCount;
          yield return (object) Globals.Instance.WaitForSeconds(0.1f);
        }
      }
    }

    private struct VertexAnim
    {
      public float angleRange;
      public float angle;
      public float speed;
    }
  }
}
