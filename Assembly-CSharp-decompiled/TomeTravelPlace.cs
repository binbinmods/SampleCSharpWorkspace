// Decompiled with JetBrains decompiler
// Type: TomeTravelPlace
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class TomeTravelPlace : MonoBehaviour
{
  public SpriteRenderer icon;
  public TMP_Text name;
  public Sprite townSprite;
  public Sprite energySprite;
  public Sprite namedSprite;
  public Sprite bossSprite;

  public void SetNode(
    bool isObeliskChallenge,
    string nodeId,
    string nodeAction,
    string obeliskIcon)
  {
    string[] strArray = nodeId.Split('_', StringSplitOptions.None);
    if (!isObeliskChallenge)
    {
      NodeData nodeData = Globals.Instance.GetNodeData(nodeId);
      if ((UnityEngine.Object) nodeData != (UnityEngine.Object) null)
        this.name.text = nodeData.NodeName;
    }
    else
    {
      EventData eventData = Globals.Instance.GetEventData(nodeAction);
      if ((UnityEngine.Object) eventData != (UnityEngine.Object) null)
        this.name.text = Texts.Instance.GetText(eventData.EventId + "_nm", "events");
      else if (nodeAction == "destination" || strArray != null && strArray.Length == 2 && strArray[1] == "0")
        this.name.text = Texts.Instance.GetText("mapEntryPoint");
    }
    if ((UnityEngine.Object) Globals.Instance.GetCombatData(nodeAction) != (UnityEngine.Object) null)
    {
      if (!isObeliskChallenge)
        return;
      switch (obeliskIcon)
      {
        case "boss":
          this.icon.sprite = this.namedSprite;
          break;
        case "finalboss":
          this.icon.sprite = this.bossSprite;
          break;
      }
      this.name.text = Texts.Instance.GetText("combat");
    }
    else if (nodeAction == "destination" || strArray != null && strArray.Length == 2 && strArray[1] == "0")
      this.icon.sprite = this.energySprite;
    else if (nodeAction.ToLower() == "town")
    {
      this.icon.sprite = this.townSprite;
    }
    else
    {
      EventData eventData = Globals.Instance.GetEventData(nodeAction);
      if (!((UnityEngine.Object) eventData != (UnityEngine.Object) null))
        return;
      this.icon.sprite = eventData.EventSpriteMap;
    }
  }
}
