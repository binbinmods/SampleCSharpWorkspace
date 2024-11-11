// Decompiled with JetBrains decompiler
// Type: WeeklySelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeeklySelector : MonoBehaviour
{
  public Dropdown[] dropElements;
  private List<string> weeklyList = new List<string>();
  private SortedDictionary<string, string> weeklyCorrespondence = new SortedDictionary<string, string>();
  private bool created;

  private void Start()
  {
  }

  public void Draw(bool force = false)
  {
    if (this.gameObject.activeSelf && !force)
    {
      this.gameObject.SetActive(false);
    }
    else
    {
      this.gameObject.SetActive(true);
      if (!this.created)
      {
        this.GenerateWeekly();
        foreach (Dropdown dropElement in this.dropElements)
          dropElement.options.Clear();
        this.dropElements[0].AddOptions(this.weeklyList);
        this.created = true;
      }
    }
    if (AtOManager.Instance.weeklyForcedId != "")
    {
      int num = 0;
      foreach (KeyValuePair<string, string> keyValuePair in this.weeklyCorrespondence)
      {
        if (keyValuePair.Value == AtOManager.Instance.weeklyForcedId)
        {
          this.dropElements[0].value = num;
          return;
        }
        ++num;
      }
    }
    this.dropElements[0].value = -1;
  }

  private void GenerateWeekly() => this.StartCoroutine(this.GenerateWeeklyWait());

  private IEnumerator GenerateWeeklyWait()
  {
    foreach (KeyValuePair<string, ChallengeData> keyValuePair in Globals.Instance.WeeklyDataSource)
    {
      int _week = int.Parse(keyValuePair.Value.Id.Replace("week", ""));
      string str = AtOManager.Instance.GetWeeklyName(_week);
      if (str.Length > 14)
        str = str.Substring(0, 14) + ".";
      string key = str + " (" + _week.ToString() + ")";
      this.weeklyCorrespondence.Add(key, keyValuePair.Value.Id);
      this.weeklyList.Add(key);
    }
    this.weeklyList.Sort();
    yield break;
  }

  public void GenerateAction()
  {
  }
}
