// Decompiled with JetBrains decompiler
// Type: NodesConnectedRequirement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class NodesConnectedRequirement
{
  [SerializeField]
  private NodeData nodeData;
  [SerializeField]
  private EventRequirementData conectionRequeriment;
  [SerializeField]
  private NodeData conectionIfNotNode;

  public NodeData NodeData
  {
    get => this.nodeData;
    set => this.nodeData = value;
  }

  public EventRequirementData ConectionRequeriment
  {
    get => this.conectionRequeriment;
    set => this.conectionRequeriment = value;
  }

  public NodeData ConectionIfNotNode
  {
    get => this.conectionIfNotNode;
    set => this.conectionIfNotNode = value;
  }
}
