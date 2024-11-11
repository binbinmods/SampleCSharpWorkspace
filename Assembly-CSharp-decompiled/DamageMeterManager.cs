// Decompiled with JetBrains decompiler
// Type: DamageMeterManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DamageMeterManager : MonoBehaviour
{
  public Transform panel;
  public CharacterMeter[] characterMeter;
  public TMP_Text[] statsTotal;
  public Transform panelElements;
  public Transform detailedData;
  public Transform buttonClose;
  public Transform banners;
  private bool opened = true;
  public int[,] combatStatsOld;
  public int[,] combatStatsCurrentOld;

  public static DamageMeterManager Instance { get; private set; }

  private void Awake()
  {
    if ((Object) DamageMeterManager.Instance == (Object) null)
      DamageMeterManager.Instance = this;
    else if ((Object) DamageMeterManager.Instance != (Object) this)
      Object.Destroy((Object) this.gameObject);
    Object.DontDestroyOnLoad((Object) this.gameObject);
  }

  private void Start() => this.Hide();

  public bool IsActive() => this.panel.gameObject.activeSelf;

  public void ShowHide()
  {
    if (this.opened)
      this.Hide();
    else
      this.Show();
  }

  public void SaveATOStats()
  {
    if (AtOManager.Instance.combatStats != null)
    {
      this.combatStatsOld = new int[AtOManager.Instance.combatStats.GetLength(0), AtOManager.Instance.combatStats.GetLength(1)];
      for (int index1 = 0; index1 < AtOManager.Instance.combatStats.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < AtOManager.Instance.combatStats.GetLength(1); ++index2)
          this.combatStatsOld[index1, index2] = AtOManager.Instance.combatStats[index1, index2];
      }
    }
    if (AtOManager.Instance.combatStatsCurrent == null)
      return;
    this.combatStatsCurrentOld = new int[AtOManager.Instance.combatStatsCurrent.GetLength(0), AtOManager.Instance.combatStatsCurrent.GetLength(1)];
    for (int index3 = 0; index3 < AtOManager.Instance.combatStatsCurrent.GetLength(0); ++index3)
    {
      for (int index4 = 0; index4 < AtOManager.Instance.combatStatsCurrent.GetLength(1); ++index4)
        this.combatStatsCurrentOld[index3, index4] = AtOManager.Instance.combatStatsCurrent[index3, index4];
    }
  }

  public void RestoreATOStats()
  {
    if (this.combatStatsOld != null)
    {
      AtOManager.Instance.combatStats = new int[this.combatStatsOld.GetLength(0), this.combatStatsOld.GetLength(1)];
      for (int index1 = 0; index1 < this.combatStatsOld.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.combatStatsOld.GetLength(1); ++index2)
          AtOManager.Instance.combatStats[index1, index2] = this.combatStatsOld[index1, index2];
      }
    }
    if (this.combatStatsCurrentOld == null)
      return;
    AtOManager.Instance.combatStatsCurrent = new int[this.combatStatsCurrentOld.GetLength(0), this.combatStatsCurrentOld.GetLength(1)];
    for (int index3 = 0; index3 < this.combatStatsCurrentOld.GetLength(0); ++index3)
    {
      for (int index4 = 0; index4 < this.combatStatsCurrentOld.GetLength(1); ++index4)
        AtOManager.Instance.combatStatsCurrent[index3, index4] = this.combatStatsCurrentOld[index3, index4];
    }
  }

  public void Show(int[,] _combatStats = null)
  {
    this.opened = true;
    this.panel.gameObject.SetActive(true);
    if (AtOManager.Instance.combatStats != null && AtOManager.Instance.combatStats.GetLength(1) == 10)
    {
      this.detailedData.gameObject.SetActive(false);
      for (int index = 5; index < 12; ++index)
        this.statsTotal[index].gameObject.SetActive(false);
    }
    else
    {
      this.detailedData.gameObject.SetActive(true);
      for (int index = 5; index < 12; ++index)
        this.statsTotal[index].gameObject.SetActive(true);
    }
    if (AtOManager.Instance.combatStats.GetLength(1) > 10)
    {
      for (int index1 = 0; index1 < AtOManager.Instance.combatStats.GetLength(0); ++index1)
      {
        AtOManager.Instance.combatStats[index1, 5] = AtOManager.Instance.combatStats[index1, 0];
        if (AtOManager.Instance.combatStatsCurrent != null)
          AtOManager.Instance.combatStatsCurrent[index1, 5] = AtOManager.Instance.combatStatsCurrent[index1, 0];
        for (int index2 = 6; index2 < AtOManager.Instance.combatStats.GetLength(1); ++index2)
        {
          AtOManager.Instance.combatStats[index1, 5] -= AtOManager.Instance.combatStats[index1, index2];
          if (AtOManager.Instance.combatStatsCurrent != null && AtOManager.Instance.combatStatsCurrent.GetLength(1) > 10)
            AtOManager.Instance.combatStatsCurrent[index1, 5] -= AtOManager.Instance.combatStatsCurrent[index1, index2];
        }
      }
    }
    this.DoStats();
  }

  public void Hide() => this.StartCoroutine(this.HideCo());

  private IEnumerator HideCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    this.opened = false;
    this.panel.gameObject.SetActive(false);
  }

  public void DoStats()
  {
    for (int _index = 0; _index < this.characterMeter.Length; ++_index)
      this.characterMeter[_index].DoStats(_index);
    if (!TomeManager.Instance.IsActive())
      return;
    this.RestoreATOStats();
  }

  public void SetTotal(int index, int total) => this.statsTotal[index].text = total.ToString();

  public void ControllerMovement(bool goingUp = false, bool goingRight = false, bool goingDown = false, bool goingLeft = false) => Mouse.current.WarpCursorPosition((Vector2) (this.buttonClose.position + new Vector3(0.0f, 70f, 0.0f)));
}
