// Decompiled with JetBrains decompiler
// Type: AllIn1SpriteShader.SetGlobalTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace AllIn1SpriteShader
{
  [ExecuteInEditMode]
  public class SetGlobalTime : MonoBehaviour
  {
    private int globalTime;

    private void Start() => this.globalTime = Shader.PropertyToID("globalUnscaledTime");

    private void Update() => Shader.SetGlobalFloat(this.globalTime, Time.unscaledTime / 20f);
  }
}
