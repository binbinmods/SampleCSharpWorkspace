// Decompiled with JetBrains decompiler
// Type: MapLegend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class MapLegend : MonoBehaviour
{
  public Transform info;
  public Transform legend;
  private RectTransform rt;
  private RectTransform rtLegend;
  private Coroutine co;
  private Vector3 posEnd;
  private Vector3 posIni;
  private Vector3 step;
  private float legendOffset;
  private bool open;

  private void Awake() => this.rt = this.GetComponent<RectTransform>();

  private void Start()
  {
    this.rtLegend = this.legend.GetComponent<RectTransform>();
    this.legendOffset = this.rtLegend.position.y;
    this.legend.gameObject.SetActive(false);
    this.Resize();
  }

  public void Resize()
  {
    this.posIni = new Vector3(0.0f, (float) (-(double) Globals.Instance.sizeH * 0.5 - 0.57999998331069946 * (double) Globals.Instance.multiplierY), -2f);
    this.posEnd = this.posIni + new Vector3(0.0f, 1.72f * Globals.Instance.multiplierY, 0.0f);
    this.step = new Vector3(0.0f, Mathf.Abs(this.posIni.y - this.posEnd.y) * 0.1f, 0.0f);
    this.rt.position = this.posIni;
  }

  private void OnMouseEnter()
  {
    if (MapManager.Instance.corruption.gameObject.activeSelf || AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || (bool) (Object) MapManager.Instance && MapManager.Instance.IsCharacterUnlock())
      return;
    this.ShowLegend(true);
  }

  private void OnMouseOver()
  {
    if (MapManager.Instance.corruption.gameObject.activeSelf || AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || (bool) (Object) MapManager.Instance && MapManager.Instance.IsCharacterUnlock())
      return;
    this.ShowLegend(true);
  }

  private void ShowLegend(bool state)
  {
    if (state == this.open || (bool) (Object) EventManager.Instance || state && !MapManager.Instance.worldTransform.gameObject.activeSelf)
      return;
    this.open = state;
    if (this.co != null)
      this.StopCoroutine(this.co);
    this.co = this.StartCoroutine(this.ShowLegendCo(state));
  }

  private IEnumerator ShowLegendCo(bool state)
  {
    float destinationY;
    if (state)
    {
      this.info.gameObject.SetActive(false);
      this.legend.gameObject.SetActive(true);
      destinationY = this.posEnd.y - this.legendOffset;
      while ((double) this.rtLegend.localPosition.y < (double) destinationY)
      {
        RectTransform rtLegend = this.rtLegend;
        rtLegend.localPosition = rtLegend.localPosition + this.step;
        if ((double) this.rtLegend.localPosition.y + (double) this.step.y < (double) destinationY)
          yield return (object) null;
        else
          break;
      }
      this.open = true;
    }
    else
    {
      destinationY = this.posIni.y - this.legendOffset;
      while ((double) this.rtLegend.localPosition.y > (double) destinationY)
      {
        RectTransform rtLegend = this.rtLegend;
        rtLegend.localPosition = rtLegend.localPosition - this.step;
        if ((double) this.rtLegend.localPosition.y - (double) this.step.y > (double) destinationY)
          yield return (object) null;
        else
          break;
      }
      this.info.gameObject.SetActive(true);
      this.legend.gameObject.SetActive(false);
      this.open = false;
    }
    this.rtLegend.localPosition = new Vector3(this.rtLegend.localPosition.x, destinationY, this.rtLegend.localPosition.z);
    yield return (object) null;
  }

  private void OnMouseExit() => this.ShowLegend(false);
}
