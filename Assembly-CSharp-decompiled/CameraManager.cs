// Decompiled with JetBrains decompiler
// Type: CameraManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class CameraManager : MonoBehaviour
{
  public float horizontalResolution = 1920f;
  public float verticalResolution = 1080f;
  public bool resize = true;
  private float ortoDestine;
  private float ortoInit;
  private CameraShake cameraShake;
  private float lastScreenWidth;
  private float lastScreenHeight;
  private Coroutine zoomCoroutine;

  private void Awake()
  {
    this.cameraShake = this.GetComponent<CameraShake>();
    this.ortoInit = Camera.main.orthographicSize;
    Camera.main.orthographicSize = 5.4f;
  }

  private void Update()
  {
    if (Time.frameCount % 24 != 0 || (double) this.lastScreenWidth == (double) Screen.width && (double) this.lastScreenHeight == (double) Screen.height)
      return;
    this.lastScreenWidth = (float) Screen.width;
    this.lastScreenHeight = (float) Screen.height;
    this.resize = true;
    if (!this.resize)
      return;
    float num1 = (float) Screen.width / (float) Screen.height / 1.77777779f;
    Camera component = this.GetComponent<Camera>();
    if ((double) num1 < 1.0)
    {
      Rect rect = component.rect with
      {
        width = 1f,
        height = num1,
        x = 0.0f,
        y = (float) ((1.0 - (double) num1) / 2.0)
      };
      component.rect = rect;
    }
    else
    {
      float num2 = 1f / num1;
      Rect rect = component.rect with
      {
        width = num2,
        height = 1f,
        x = (float) ((1.0 - (double) num2) / 2.0),
        y = 0.0f
      };
      component.rect = rect;
    }
    if ((Object) GameManager.Instance != (Object) null)
      GameManager.Instance.Resize();
    this.resize = false;
  }

  public void Shake()
  {
    if (!GameManager.Instance.ConfigScreenShakeOption)
      return;
    this.cameraShake.Shake();
  }

  public void Zoom()
  {
    this.ortoDestine = Camera.main.orthographicSize - 0.15f;
    if (this.zoomCoroutine != null)
      this.StopCoroutine(this.zoomCoroutine);
    this.zoomCoroutine = this.StartCoroutine(this.ZoomCo(this.ortoInit, this.ortoDestine));
  }

  private IEnumerator ZoomCo(float source, float destine)
  {
    float step = Mathf.Abs(source - destine) / 10f;
    float index = source;
    float end = destine;
    if ((double) source > (double) destine)
    {
      while ((double) index > (double) end)
      {
        index -= step;
        Camera.main.orthographicSize = index;
        yield return (object) Globals.Instance.WaitForSeconds(0.005f);
      }
    }
    else
    {
      step = (float) (((double) destine - (double) source) / 10.0);
      while ((double) index < (double) end)
      {
        index += step;
        Camera.main.orthographicSize = index;
        yield return (object) Globals.Instance.WaitForSeconds(0.005f);
      }
    }
    Camera.main.orthographicSize = destine;
  }

  public void ZoomBack()
  {
    if (this.zoomCoroutine != null)
      this.StopCoroutine(this.zoomCoroutine);
    this.zoomCoroutine = this.StartCoroutine(this.ZoomCo(this.ortoDestine, this.ortoInit));
  }

  public void ZoomToTransform(Transform transformPosition)
  {
    this.ortoInit = Camera.main.orthographicSize;
    this.ortoDestine = Camera.main.orthographicSize - 2f;
    this.StartCoroutine(this.ZoomToTransformCO(transformPosition));
  }

  private IEnumerator ZoomToTransformCO(Transform transformPosition)
  {
    CameraManager cameraManager = this;
    cameraManager.resize = false;
    float index = Camera.main.orthographicSize;
    while ((double) index > (double) cameraManager.ortoDestine)
    {
      index -= 0.2f;
      cameraManager.transform.localRotation = Quaternion.Slerp(cameraManager.transform.localRotation, Quaternion.Euler(0.0f, 0.0f, -15f), Time.deltaTime * 5f);
      Camera.main.orthographicSize = index;
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    }
    yield return (object) Globals.Instance.WaitForSeconds(1.5f);
    cameraManager.ortoDestine = cameraManager.ortoInit;
    index = Camera.main.orthographicSize;
    Debug.Log((object) (index.ToString() + "<" + cameraManager.ortoDestine.ToString()));
    while ((double) index < (double) cameraManager.ortoDestine)
    {
      index += 0.2f;
      cameraManager.transform.localRotation = Quaternion.Slerp(cameraManager.transform.localRotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 5f);
      Camera.main.orthographicSize = index;
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    }
    cameraManager.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
  }
}
