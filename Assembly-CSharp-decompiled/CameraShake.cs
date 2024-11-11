// Decompiled with JetBrains decompiler
// Type: CameraShake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
  public Transform camTransform;
  public float shakeDuration = 0.8f;
  public float shakeAmount = 0.7f;
  public float decreaseFactor = 2f;
  private Vector3 originalPos;
  private Coroutine cameraShakeCo;

  private void Awake()
  {
    if (!((Object) this.camTransform == (Object) null))
      return;
    this.camTransform = this.GetComponent(typeof (Transform)) as Transform;
  }

  private void OnEnable() => this.originalPos = this.camTransform.localPosition;

  public void Shake()
  {
    this.shakeDuration = 0.2f;
    if (this.cameraShakeCo != null)
      return;
    this.cameraShakeCo = this.StartCoroutine(this.ShakeAction());
  }

  private IEnumerator ShakeAction()
  {
    while ((double) this.shakeDuration > 0.0)
    {
      this.camTransform.localPosition = this.originalPos + Random.insideUnitSphere * this.shakeAmount;
      this.shakeDuration -= Time.deltaTime * this.decreaseFactor;
      yield return (object) null;
    }
    this.camTransform.localPosition = this.originalPos;
    this.cameraShakeCo = (Coroutine) null;
  }
}
