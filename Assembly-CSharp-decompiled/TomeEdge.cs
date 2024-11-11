// Decompiled with JetBrains decompiler
// Type: TomeEdge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class TomeEdge : MonoBehaviour
{
  private GameObject arrow;
  private SpriteRenderer arrowSprite;
  private SpriteRenderer arrowBg;
  private Coroutine moveCo;
  public bool isPrev;

  private void Start()
  {
    this.arrow = this.transform.GetChild(0).transform.gameObject;
    this.arrowBg = this.arrow.transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
    this.arrowSprite = this.arrow.transform.GetChild(1).transform.GetComponent<SpriteRenderer>();
    SpriteRenderer arrowBg = this.arrowBg;
    SpriteRenderer arrowSprite = this.arrowSprite;
    Color color1 = new Color(1f, 1f, 1f, 0.0f);
    Color color2 = color1;
    arrowSprite.color = color2;
    Color color3 = color1;
    arrowBg.color = color3;
  }

  public void Show()
  {
    if (this.moveCo != null)
      this.StopCoroutine(this.moveCo);
    this.moveCo = this.StartCoroutine(this.ShowCo());
  }

  private IEnumerator ShowCo()
  {
    Color col = this.arrowBg.color;
    this.arrow.SetActive(true);
    while ((double) col.a < 0.5)
    {
      col.a += 0.05f;
      this.arrowBg.color = col;
      this.arrowSprite.color = col;
      yield return (object) Globals.Instance.WaitForSeconds(0.025f);
    }
  }

  public void Hide()
  {
    if (this.moveCo != null)
      this.StopCoroutine(this.moveCo);
    this.moveCo = this.StartCoroutine(this.HideCo());
  }

  private IEnumerator HideCo()
  {
    Color col = this.arrowBg.color;
    while ((double) col.a > 0.0)
    {
      col.a -= 0.05f;
      this.arrowBg.color = col;
      this.arrowSprite.color = col;
      yield return (object) Globals.Instance.WaitForSeconds(0.025f);
    }
    this.arrow.SetActive(false);
  }

  private void OnMouseEnter()
  {
    if (this.isPrev)
    {
      if (!TomeManager.Instance.IsTherePrev())
        return;
      this.Show();
    }
    else
    {
      if (!TomeManager.Instance.IsThereNext())
        return;
      this.Show();
    }
  }

  private void OnMouseExit() => this.Hide();

  private void OnMouseUp()
  {
    if (this.isPrev)
    {
      if (!TomeManager.Instance.IsTherePrev())
        return;
      if (TomeManager.Instance.IsFirstPage())
        this.Hide();
      TomeManager.Instance.DoPrevPage();
    }
    else
    {
      if (!TomeManager.Instance.IsThereNext())
        return;
      if (TomeManager.Instance.IsLastPage())
        this.Hide();
      TomeManager.Instance.DoNextPage();
    }
  }
}
