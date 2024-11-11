// Decompiled with JetBrains decompiler
// Type: FinishProgression
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;

public class FinishProgression : MonoBehaviour
{
  public TMP_Text charName;
  public TMP_Text charProgress;
  public TMP_Text charMin;
  public TMP_Text charMax;
  public TMP_Text charRank;
  public TMP_Text charPoints;
  public Transform progressBarMask;
  public SpriteRenderer progressBar;
  public Transform progressBarParticles;
  private string characterName = "";
  private string className = "";
  private string subclassId = "";
  private int iniProgress;
  private int maxProgress;
  private int charIndex = -1;
  private Coroutine coIncrement;

  public void SetCharacter(string _charName, string _className, string _subclassId, int _index)
  {
    this.characterName = _charName;
    this.className = _className;
    this.subclassId = _subclassId;
    this.charIndex = _index;
    this.charName.text = _charName;
    this.DoProgress();
  }

  public void DoProgress()
  {
    int progress = PlayerManager.Instance.GetProgress(this.subclassId);
    this.iniProgress = progress;
    int perkPrevLevelPoints = PlayerManager.Instance.GetPerkPrevLevelPoints(this.subclassId);
    int perkNextLevelPoints = PlayerManager.Instance.GetPerkNextLevelPoints(this.subclassId);
    this.maxProgress = perkNextLevelPoints;
    this.charProgress.text = progress.ToString();
    if (perkNextLevelPoints != 0)
    {
      this.charMin.text = perkPrevLevelPoints.ToString();
      this.charMax.text = perkNextLevelPoints.ToString();
    }
    else
    {
      this.charMax.text = "";
      this.charMin.text = "";
    }
    float x = (float) (((double) progress - (double) perkPrevLevelPoints) / ((double) perkNextLevelPoints - (double) perkPrevLevelPoints) * 1.6489449739456177);
    if (perkNextLevelPoints == 0)
    {
      x = 1.648945f;
      this.progressBarParticles.gameObject.SetActive(false);
      this.StopBlockProgress();
    }
    this.progressBarMask.localScale = new Vector3(x, this.progressBarMask.localScale.y, this.progressBarMask.localScale.z);
    this.progressBar.color = this.charProgress.color = Functions.HexToColor(Globals.Instance.ClassColor[this.className]);
    this.charRank.text = string.Format(Texts.Instance.GetText("rankProgress"), (object) PlayerManager.Instance.GetPerkRank(this.subclassId));
    int perkPointsAvailable = PlayerManager.Instance.GetPerkPointsAvailable(this.subclassId);
    if (perkPointsAvailable > 0)
    {
      this.charPoints.text = string.Format(Texts.Instance.GetText("rankPerkPoints"), (object) (perkPointsAvailable.ToString() + " <sprite name=experience>"));
      this.charPoints.gameObject.SetActive(true);
    }
    else
      this.charPoints.gameObject.SetActive(false);
  }

  public void Increment(int finalProgress) => this.coIncrement = this.StartCoroutine(this.IncrementTimeout(finalProgress));

  private IEnumerator IncrementTimeout(int destine)
  {
    FinishProgression finishProgression = this;
    yield return (object) Globals.Instance.WaitForSeconds(0.03f);
    int _quantity = 0;
    if (destine > 500)
      _quantity = 500;
    else if (destine > 100)
      _quantity = 100;
    else if (destine > 25)
      _quantity = 25;
    else if (destine > 10)
      _quantity = 10;
    else if (destine > 0)
      _quantity = 1;
    finishProgression.iniProgress += _quantity;
    destine -= _quantity;
    PlayerManager.Instance.ModifyProgress(finishProgression.subclassId, _quantity, finishProgression.charIndex);
    if (destine <= 0)
    {
      if (finishProgression.coIncrement != null)
        finishProgression.StopCoroutine(finishProgression.coIncrement);
      finishProgression.StopBlockProgress();
    }
    else
    {
      finishProgression.DoProgress();
      finishProgression.Increment(destine);
    }
  }

  private void StopBlockProgress()
  {
    this.progressBarParticles.gameObject.SetActive(false);
    this.charProgress.text = PlayerManager.Instance.GetProgress(this.subclassId).ToString();
    FinishRunManager.Instance.UnlockState(this.charIndex);
  }

  public bool IsActive() => this.gameObject.activeSelf;

  public void Hide() => this.gameObject.SetActive(false);
}
