// Decompiled with JetBrains decompiler
// Type: EventRequirementData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(fileName = "New Event Requirement", menuName = "Event Requirement Data", order = 63)]
public class EventRequirementData : ScriptableObject
{
  [SerializeField]
  private string requirementId;
  [SerializeField]
  private string requirementName;
  [TextArea]
  [SerializeField]
  private string description;
  [SerializeField]
  private bool assignToPlayerAtBegin;
  [SerializeField]
  private bool requirementTrack;
  [SerializeField]
  private Enums.Zone requirementZoneFinishTrack;
  [SerializeField]
  private bool itemTrack;
  [SerializeField]
  private Sprite itemSprite;
  [SerializeField]
  private Sprite trackSprite;

  public bool CanShowRequeriment(Enums.Zone fromZone, Enums.Zone toZone)
  {
    if (fromZone == Enums.Zone.SpiderLair)
      fromZone = Enums.Zone.Aquarfall;
    if (toZone == Enums.Zone.SpiderLair)
      toZone = Enums.Zone.Aquarfall;
    if (fromZone == Enums.Zone.Sectarium)
      fromZone = Enums.Zone.Senenthia;
    if (toZone == Enums.Zone.Sectarium)
      toZone = Enums.Zone.Senenthia;
    if (fromZone == Enums.Zone.FrozenSewers)
      fromZone = Enums.Zone.Faeborg;
    if (toZone == Enums.Zone.FrozenSewers)
      toZone = Enums.Zone.Faeborg;
    if (fromZone == Enums.Zone.BlackForge)
      fromZone = Enums.Zone.Velkarath;
    if (toZone == Enums.Zone.BlackForge)
      toZone = Enums.Zone.Velkarath;
    if (this.requirementZoneFinishTrack == Enums.Zone.Senenthia)
      return fromZone == Enums.Zone.Senenthia;
    if (this.requirementZoneFinishTrack == Enums.Zone.VoidLow)
      return fromZone != Enums.Zone.VoidHigh && toZone != Enums.Zone.VoidHigh;
    if (this.requirementZoneFinishTrack == Enums.Zone.VoidHigh || this.requirementZoneFinishTrack == Enums.Zone.None)
      return true;
    if (AtOManager.Instance.GetActNumberForText() == 4)
      return false;
    return AtOManager.Instance.GetActNumberForText() == 3 ? toZone == Enums.Zone.None && this.requirementZoneFinishTrack == fromZone : (AtOManager.Instance.GetActNumberForText() == 2 ? this.requirementZoneFinishTrack == toZone || toZone == Enums.Zone.None : this.requirementZoneFinishTrack == toZone || toZone == Enums.Zone.None);
  }

  public string RequirementName
  {
    get => this.requirementName;
    set => this.requirementName = value;
  }

  public string Description
  {
    get => this.description;
    set => this.description = value;
  }

  public string RequirementId
  {
    get => this.requirementId;
    set => this.requirementId = value;
  }

  public bool RequirementTrack
  {
    get => this.requirementTrack;
    set => this.requirementTrack = value;
  }

  public bool ItemTrack
  {
    get => this.itemTrack;
    set => this.itemTrack = value;
  }

  public Sprite ItemSprite
  {
    get => this.itemSprite;
    set => this.itemSprite = value;
  }

  public bool AssignToPlayerAtBegin
  {
    get => this.assignToPlayerAtBegin;
    set => this.assignToPlayerAtBegin = value;
  }

  public Sprite TrackSprite
  {
    get => this.trackSprite;
    set => this.trackSprite = value;
  }
}
