// Decompiled with JetBrains decompiler
// Type: AnimatedCurveObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AnimatedCurveObject : MonoBehaviour
{
  [SerializeField]
  private AnimationCurve animationCurve;
  [SerializeField]
  private float speed = 100f;
  private float curveDeltaTime;

  private void Start() => Object.Destroy((Object) this.gameObject, 15f);

  private void Update()
  {
    Vector3 position = this.transform.position;
    position.z += this.speed * Time.deltaTime;
    this.curveDeltaTime += Time.deltaTime;
    position.y = this.animationCurve.Evaluate(this.curveDeltaTime);
    this.transform.position = position;
  }
}
