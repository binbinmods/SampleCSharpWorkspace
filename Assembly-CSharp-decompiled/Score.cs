// Decompiled with JetBrains decompiler
// Type: Score
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
  public Transform icon;
  public TMP_Text index;
  public TMP_Text name;
  public TMP_Text score;
  public Transform sep;
  private Color indexColor;
  private Color nameColor;

  private void Awake()
  {
    this.indexColor = this.index.color;
    this.nameColor = this.name.color;
  }

  public void SetScore(int _index, string _name, int _score = -1, ulong _userId = 999999999999999)
  {
    if (_index > 0)
    {
      this.index.text = _index.ToString();
      this.name.text = _name;
      this.score.text = Functions.ScoreFormat(_score);
      this.sep.gameObject.SetActive(true);
      if ((long) _userId != (long) (ulong) SteamManager.Instance.steamId)
      {
        this.icon.gameObject.SetActive(false);
        this.name.color = this.nameColor;
      }
      else
      {
        this.icon.gameObject.SetActive(true);
        this.name.color = this.indexColor;
        TomeManager.Instance.playerOnScoreboard = true;
      }
    }
    else
    {
      this.index.text = "";
      this.name.text = _name;
      this.score.text = "";
      this.sep.gameObject.SetActive(false);
    }
  }

  public void Show() => this.gameObject.SetActive(true);

  public void Hide() => this.gameObject.SetActive(false);
}
