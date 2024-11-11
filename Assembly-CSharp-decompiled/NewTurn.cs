// Decompiled with JetBrains decompiler
// Type: NewTurn
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class NewTurn : MonoBehaviour
{
  public TMP_Text textTM;
  public SpriteRenderer spr;
  private Transform textT;
  private string text;
  private bool end;
  private bool passTurn;
  private bool cantDraw;
  private Coroutine co;

  private void Awake()
  {
    this.textT = this.textTM.transform;
    this.textTM.text = "";
    this.textT.gameObject.SetActive(false);
  }

  private void GoToEnd() => this.HideMask();

  private void ShowMask() => MatchManager.Instance.ShowMask(true, false);

  private void HideMask() => MatchManager.Instance.ShowMask(false);

  public void FinishCombat(bool won)
  {
    this.end = true;
    if (won)
      this.SetTurn(Texts.Instance.GetText("combatVictory"));
    else
      this.SetTurn(Texts.Instance.GetText("combatFail"));
  }

  public void CantDraw(string name)
  {
    this.passTurn = true;
    this.cantDraw = true;
    if (this.co != null)
      this.StopCoroutine(this.co);
    this.co = this.StartCoroutine(this.SetTurnCo(name));
  }

  public void PassTurn(string name)
  {
    this.passTurn = true;
    this.cantDraw = false;
    if (this.co != null)
      this.StopCoroutine(this.co);
    this.co = this.StartCoroutine(this.SetTurnCo(name));
  }

  public void SetTurn(string name)
  {
    if (this.co != null)
      this.StopCoroutine(this.co);
    this.co = this.StartCoroutine(this.SetTurnCo(name));
  }

  private IEnumerator SetTurnCo(string name)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (this.end)
    {
      stringBuilder.Append("<color=#FC0>");
      stringBuilder.Append(name);
      stringBuilder.Append("</color>");
    }
    else if (this.passTurn)
    {
      stringBuilder.Append(name);
      stringBuilder.Append("<br><color=#FC0>");
      if (this.cantDraw)
      {
        stringBuilder.Append(Texts.Instance.GetText("cantDrawAnyCard"));
        this.cantDraw = false;
      }
      else
        stringBuilder.Append(Texts.Instance.GetText("missesThisTurn"));
      stringBuilder.Append("</color>");
    }
    else
    {
      stringBuilder.Append("<size=4.5>");
      stringBuilder.Append(Texts.Instance.GetText("newTurn"));
      stringBuilder.Append("</size>\n<b><color=#FFF>");
      stringBuilder.Append(name.ToLower());
      stringBuilder.Append("</color></b>");
    }
    this.textTM.text = stringBuilder.ToString();
    this.textT.localScale = Vector3.zero;
    this.textT.gameObject.SetActive(true);
    Vector3 vectorScale = new Vector3(0.1f, 0.1f, 0.0f);
    while ((double) this.textT.localScale.x < 1.1000000238418579)
    {
      this.textT.localScale += vectorScale;
      yield return (object) null;
    }
    while ((double) this.textT.localScale.x > 1.0)
    {
      this.textT.localScale -= vectorScale;
      yield return (object) null;
    }
    this.textT.localScale = new Vector3(1f, 1f, 1f);
    if (!this.end)
    {
      if (this.passTurn)
      {
        this.passTurn = false;
        yield return (object) Globals.Instance.WaitForSeconds(1f);
      }
      else
        yield return (object) Globals.Instance.WaitForSeconds(0.5f);
      while ((double) this.textT.localScale.x > 0.0)
      {
        this.textT.localScale -= vectorScale;
        yield return (object) null;
      }
      this.textT.gameObject.SetActive(false);
    }
    else if (this.end)
    {
      yield return (object) Globals.Instance.WaitForSeconds(1f);
      MatchManager.Instance.BackToFinishGame();
    }
  }
}
