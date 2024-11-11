// Decompiled with JetBrains decompiler
// Type: TMPro.Examples.SkewTextExample
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

namespace TMPro.Examples
{
  public class SkewTextExample : MonoBehaviour
  {
    private TMP_Text m_TextComponent;
    public AnimationCurve VertexCurve = new AnimationCurve(new Keyframe[5]
    {
      new Keyframe(0.0f, 0.0f),
      new Keyframe(0.25f, 2f),
      new Keyframe(0.5f, 0.0f),
      new Keyframe(0.75f, 2f),
      new Keyframe(1f, 0.0f)
    });
    public float CurveScale = 1f;
    public float ShearAmount = 1f;

    private void Awake() => this.m_TextComponent = this.gameObject.GetComponent<TMP_Text>();

    private void Start() => this.StartCoroutine(this.WarpText());

    private AnimationCurve CopyAnimationCurve(AnimationCurve curve) => new AnimationCurve()
    {
      keys = curve.keys
    };

    private IEnumerator WarpText()
    {
      this.VertexCurve.preWrapMode = WrapMode.Once;
      this.VertexCurve.postWrapMode = WrapMode.Once;
      this.m_TextComponent.havePropertiesChanged = true;
      this.CurveScale *= 10f;
      float old_CurveScale = this.CurveScale;
      float old_ShearValue = this.ShearAmount;
      AnimationCurve old_curve = this.CopyAnimationCurve(this.VertexCurve);
      while (true)
      {
        while (this.m_TextComponent.havePropertiesChanged || (double) old_CurveScale != (double) this.CurveScale || (double) old_curve.keys[1].value != (double) this.VertexCurve.keys[1].value || (double) old_ShearValue != (double) this.ShearAmount)
        {
          old_CurveScale = this.CurveScale;
          old_curve = this.CopyAnimationCurve(this.VertexCurve);
          old_ShearValue = this.ShearAmount;
          this.m_TextComponent.ForceMeshUpdate();
          TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
          int characterCount = textInfo.characterCount;
          if (characterCount != 0)
          {
            float x1 = this.m_TextComponent.bounds.min.x;
            float x2 = this.m_TextComponent.bounds.max.x;
            for (int index = 0; index < characterCount; ++index)
            {
              if (textInfo.characterInfo[index].isVisible)
              {
                int vertexIndex = textInfo.characterInfo[index].vertexIndex;
                int materialReferenceIndex = textInfo.characterInfo[index].materialReferenceIndex;
                Vector3[] vertices = textInfo.meshInfo[materialReferenceIndex].vertices;
                Vector3 vector3_1 = (Vector3) new Vector2((float) (((double) vertices[vertexIndex].x + (double) vertices[vertexIndex + 2].x) / 2.0), textInfo.characterInfo[index].baseLine);
                vertices[vertexIndex] += -vector3_1;
                vertices[vertexIndex + 1] += -vector3_1;
                vertices[vertexIndex + 2] += -vector3_1;
                vertices[vertexIndex + 3] += -vector3_1;
                float num1 = this.ShearAmount * 0.01f;
                Vector3 vector3_2 = new Vector3(num1 * (textInfo.characterInfo[index].topRight.y - textInfo.characterInfo[index].baseLine), 0.0f, 0.0f);
                Vector3 vector3_3 = new Vector3(num1 * (textInfo.characterInfo[index].baseLine - textInfo.characterInfo[index].bottomRight.y), 0.0f, 0.0f);
                vertices[vertexIndex] += -vector3_3;
                vertices[vertexIndex + 1] += vector3_2;
                vertices[vertexIndex + 2] += vector3_2;
                vertices[vertexIndex + 3] += -vector3_3;
                float time1 = (float) (((double) vector3_1.x - (double) x1) / ((double) x2 - (double) x1));
                float time2 = time1 + 0.0001f;
                float y1 = this.VertexCurve.Evaluate(time1) * this.CurveScale;
                float y2 = this.VertexCurve.Evaluate(time2) * this.CurveScale;
                Vector3 lhs = new Vector3(1f, 0.0f, 0.0f);
                Vector3 rhs = new Vector3(time2 * (x2 - x1) + x1, y2) - new Vector3(vector3_1.x, y1);
                float num2 = Mathf.Acos(Vector3.Dot(lhs, rhs.normalized)) * 57.29578f;
                float z = (double) Vector3.Cross(lhs, rhs).z > 0.0 ? num2 : 360f - num2;
                Matrix4x4 matrix4x4 = Matrix4x4.TRS(new Vector3(0.0f, y1, 0.0f), Quaternion.Euler(0.0f, 0.0f, z), Vector3.one);
                vertices[vertexIndex] = matrix4x4.MultiplyPoint3x4(vertices[vertexIndex]);
                vertices[vertexIndex + 1] = matrix4x4.MultiplyPoint3x4(vertices[vertexIndex + 1]);
                vertices[vertexIndex + 2] = matrix4x4.MultiplyPoint3x4(vertices[vertexIndex + 2]);
                vertices[vertexIndex + 3] = matrix4x4.MultiplyPoint3x4(vertices[vertexIndex + 3]);
                vertices[vertexIndex] += vector3_1;
                vertices[vertexIndex + 1] += vector3_1;
                vertices[vertexIndex + 2] += vector3_1;
                vertices[vertexIndex + 3] += vector3_1;
              }
            }
            this.m_TextComponent.UpdateVertexData();
            yield return (object) null;
          }
        }
        yield return (object) null;
      }
    }
  }
}
