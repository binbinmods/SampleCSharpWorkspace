// Decompiled with JetBrains decompiler
// Type: ZoneData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Zone", menuName = "Zone Data", order = 59)]
public class ZoneData : ScriptableObject
{
  [SerializeField]
  private string zoneId;
  [SerializeField]
  private string zoneName;
  [SerializeField]
  private bool obeliskLow;
  [SerializeField]
  private bool obeliskHigh;
  [SerializeField]
  private bool obeliskFinal;
  [Header("Team Management")]
  [SerializeField]
  private bool changeTeamOnEntrance;
  [SerializeField]
  private List<SubClassData> newTeam;
  [SerializeField]
  private bool restoreTeamOnExit;
  [Header("Experience")]
  [SerializeField]
  private bool disableExperienceOnThisZone;
  [Header("Madness")]
  [SerializeField]
  private bool disableMadnessOnThisZone;

  public string ZoneId
  {
    get => this.zoneId;
    set => this.zoneId = value;
  }

  public string ZoneName
  {
    get => this.zoneName;
    set => this.zoneName = value;
  }

  public bool ObeliskLow
  {
    get => this.obeliskLow;
    set => this.obeliskLow = value;
  }

  public bool ObeliskHigh
  {
    get => this.obeliskHigh;
    set => this.obeliskHigh = value;
  }

  public bool ObeliskFinal
  {
    get => this.obeliskFinal;
    set => this.obeliskFinal = value;
  }

  public bool ChangeTeamOnEntrance
  {
    get => this.changeTeamOnEntrance;
    set => this.changeTeamOnEntrance = value;
  }

  public List<SubClassData> NewTeam
  {
    get => this.newTeam;
    set => this.newTeam = value;
  }

  public bool RestoreTeamOnExit
  {
    get => this.restoreTeamOnExit;
    set => this.restoreTeamOnExit = value;
  }

  public bool DisableExperienceOnThisZone
  {
    get => this.disableExperienceOnThisZone;
    set => this.disableExperienceOnThisZone = value;
  }

  public bool DisableMadnessOnThisZone
  {
    get => this.disableMadnessOnThisZone;
    set => this.disableMadnessOnThisZone = value;
  }
}
