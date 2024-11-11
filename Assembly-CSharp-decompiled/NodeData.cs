// Decompiled with JetBrains decompiler
// Type: NodeData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(fileName = "New MapNode", menuName = "MapNode Data", order = 60)]
public class NodeData : ScriptableObject
{
  [SerializeField]
  private string nodeId;
  [SerializeField]
  private string nodeName;
  [SerializeField]
  private ZoneData nodeZone;
  [TextArea]
  [SerializeField]
  private string description;
  [Header("Node background")]
  [SerializeField]
  private Sprite nodeBackgroundImg;
  [Header("Percent to appear in map")]
  [SerializeField]
  private int existsPercent = 100;
  [SerializeField]
  private string existsSku = "";
  [Header("It's a destination for a zone/portal travel")]
  [SerializeField]
  private bool travelDestination;
  [Header("Node that opens Town")]
  [SerializeField]
  private bool goToTown;
  [Header("Combat/Event percent")]
  [SerializeField]
  private int combatPercent;
  [SerializeField]
  private int eventPercent;
  [Header("Combat")]
  [SerializeField]
  private CombatData[] nodeCombat;
  [SerializeField]
  private Enums.CombatTier nodeCombatTier;
  [Header("Event and priority (lowest == highest priority)")]
  [SerializeField]
  private EventData[] nodeEvent;
  [SerializeField]
  private Enums.CombatTier nodeEventTier;
  [SerializeField]
  private int[] nodeEventPriority;
  [SerializeField]
  private int[] nodeEventPercent;
  [Header("Connections")]
  [SerializeField]
  private NodeData[] nodesConnected;
  [SerializeField]
  private global::NodesConnectedRequirement[] nodesConnectedRequirement;
  [Header("Node Requirements")]
  [SerializeField]
  private EventRequirementData nodeRequirement;
  [Header("Show the node even if the requirement is not fulfilled")]
  [SerializeField]
  private bool visibleIfNotRequirement;
  [Header("Node misc")]
  [SerializeField]
  private bool disableCorruption;
  [SerializeField]
  private bool disableRandom;
  [Header("Node Ground")]
  [SerializeField]
  private Enums.NodeGround nodeGround;

  public string NodeId
  {
    get => this.nodeId;
    set => this.nodeId = value;
  }

  public string NodeName
  {
    get => this.nodeName;
    set => this.nodeName = value;
  }

  public string Description
  {
    get => this.description;
    set => this.description = value;
  }

  public NodeData[] NodesConnected
  {
    get => this.nodesConnected;
    set => this.nodesConnected = value;
  }

  public CombatData[] NodeCombat
  {
    get => this.nodeCombat;
    set => this.nodeCombat = value;
  }

  public EventData[] NodeEvent
  {
    get => this.nodeEvent;
    set => this.nodeEvent = value;
  }

  public int[] NodeEventPriority
  {
    get => this.nodeEventPriority;
    set => this.nodeEventPriority = value;
  }

  public EventRequirementData NodeRequirement
  {
    get => this.nodeRequirement;
    set => this.nodeRequirement = value;
  }

  public bool VisibleIfNotRequirement
  {
    get => this.visibleIfNotRequirement;
    set => this.visibleIfNotRequirement = value;
  }

  public global::NodesConnectedRequirement[] NodesConnectedRequirement
  {
    get => this.nodesConnectedRequirement;
    set => this.nodesConnectedRequirement = value;
  }

  public ZoneData NodeZone
  {
    get => this.nodeZone;
    set => this.nodeZone = value;
  }

  public bool GoToTown
  {
    get => this.goToTown;
    set => this.goToTown = value;
  }

  public int CombatPercent
  {
    get => this.combatPercent;
    set => this.combatPercent = value;
  }

  public int EventPercent
  {
    get => this.eventPercent;
    set => this.eventPercent = value;
  }

  public int ExistsPercent
  {
    get => this.existsPercent;
    set => this.existsPercent = value;
  }

  public int[] NodeEventPercent
  {
    get => this.nodeEventPercent;
    set => this.nodeEventPercent = value;
  }

  public bool TravelDestination
  {
    get => this.travelDestination;
    set => this.travelDestination = value;
  }

  public Enums.CombatTier NodeCombatTier
  {
    get => this.nodeCombatTier;
    set => this.nodeCombatTier = value;
  }

  public Enums.CombatTier NodeEventTier
  {
    get => this.nodeEventTier;
    set => this.nodeEventTier = value;
  }

  public Sprite NodeBackgroundImg
  {
    get => this.nodeBackgroundImg;
    set => this.nodeBackgroundImg = value;
  }

  public bool DisableCorruption
  {
    get => this.disableCorruption;
    set => this.disableCorruption = value;
  }

  public bool DisableRandom
  {
    get => this.disableRandom;
    set => this.disableRandom = value;
  }

  public Enums.NodeGround NodeGround
  {
    get => this.nodeGround;
    set => this.nodeGround = value;
  }

  public string ExistsSku
  {
    get => this.existsSku;
    set => this.existsSku = value;
  }
}
