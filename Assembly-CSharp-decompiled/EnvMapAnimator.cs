// Decompiled with JetBrains decompiler
// Type: EnvMapAnimator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;

public class EnvMapAnimator : MonoBehaviour
{
  public Vector3 RotationSpeeds;
  private TMP_Text m_textMeshPro;
  private Material m_material;

  private void Awake()
  {
    this.m_textMeshPro = this.GetComponent<TMP_Text>();
    this.m_material = this.m_textMeshPro.fontSharedMaterial;
  }

  private IEnumerator Start()
  {
    Matrix4x4 matrix = new Matrix4x4();
    while (true)
    {
      matrix.SetTRS(Vector3.zero, Quaternion.Euler(Time.time * this.RotationSpeeds.x, Time.time * this.RotationSpeeds.y, Time.time * this.RotationSpeeds.z), Vector3.one);
      this.m_material.SetMatrix("_EnvMatrix", matrix);
      yield return (object) null;
    }
  }
}
