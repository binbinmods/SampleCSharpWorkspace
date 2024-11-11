// Decompiled with JetBrains decompiler
// Type: TomeNumber
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;

public class TomeNumber : MonoBehaviour
{
  public Transform backgroundT;
  public Transform activeT;
  public SpriteRenderer background;
  public SpriteRenderer border;
  public TMP_Text numberTxt;
  private string number;
  private Color colorActive;
  private bool active;
  private bool visible;
  private Vector3 positionOri;
  private Vector3 vectorTranslate = new Vector3(0.05f, 0.0f, 0.0f);

  private void Awake() => this.colorActive = Functions.HexToColor("#FFA400");

  public void Activate()
  {
    this.active = true;
    this.activeT.gameObject.SetActive(true);
    this.border.color = this.colorActive;
    this.background.color = this.colorActive;
    this.transform.localPosition = this.transform.localPosition - new Vector3(0.2f, 0.0f, 0.0f);
  }

  public void Deactivate()
  {
    this.active = false;
    this.background.color = new Color(1f, 1f, 1f, 1f);
    this.activeT.gameObject.SetActive(false);
    this.transform.localPosition = this.positionOri;
  }

  public bool IsActive() => this.active;

  public void Init(int _number)
  {
    this.number = _number.ToString();
    this.numberTxt.text = this.number;
    this.positionOri = this.transform.localPosition;
  }

  public void SetText(string _text) => this.numberTxt.text = _text;

  public void Show()
  {
    if (!this.gameObject.activeSelf || !this.transform.parent.gameObject.activeSelf)
      return;
    this.StartCoroutine(this.ShowCo());
  }

  private IEnumerator ShowCo()
  {
    TomeNumber tomeNumber = this;
    float destinationX = tomeNumber.positionOri.x;
    while ((double) tomeNumber.transform.localPosition.x > (double) destinationX)
    {
      tomeNumber.transform.localPosition -= tomeNumber.vectorTranslate;
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    }
    tomeNumber.transform.localPosition = new Vector3(tomeNumber.transform.localPosition.x, tomeNumber.transform.localPosition.y, 0.0f);
    tomeNumber.visible = true;
  }

  public void Hide()
  {
    if (!this.gameObject.activeSelf || !this.transform.parent.gameObject.activeSelf)
      return;
    this.StartCoroutine(this.HideCo());
  }

  private IEnumerator HideCo()
  {
    TomeNumber tomeNumber = this;
    float destinationX = tomeNumber.positionOri.x + 1f;
    while ((double) tomeNumber.transform.localPosition.x < (double) destinationX)
    {
      tomeNumber.transform.localPosition += tomeNumber.vectorTranslate;
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    }
    tomeNumber.transform.localPosition = new Vector3(tomeNumber.transform.localPosition.x, tomeNumber.transform.localPosition.y, 100f);
    tomeNumber.visible = false;
  }

  public bool IsVisible() => this.visible;

  private void OnMouseEnter()
  {
    if (this.active)
      return;
    this.border.color = Functions.HexToColor("#BBBBBB");
    this.activeT.gameObject.SetActive(true);
    GameManager.Instance.SetCursorHover();
  }

  private void OnMouseExit()
  {
    if (!this.active)
      this.activeT.gameObject.SetActive(false);
    GameManager.Instance.SetCursorPlain();
  }

  public void OnMouseUp()
  {
    if (!Functions.ClickedThisTransform(this.transform))
      return;
    TomeManager.Instance.SetPage(int.Parse(this.number));
  }
}
