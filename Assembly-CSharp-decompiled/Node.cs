// Decompiled with JetBrains decompiler
// Type: Node
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
  public NodeData nodeData;
  public SpriteRenderer background;
  public Transform plainT;
  public Transform currentT;
  public Transform blockedT;
  public Transform availableT;
  public Transform visitedT;
  public Transform chestT;
  public Transform requisiteLayer;
  public Transform requisite;
  public SpriteRenderer requisiteSprite;
  public SpriteRenderer requisiteSpriteShadow;
  public Transform requisite2;
  public SpriteRenderer requisite2Sprite;
  public SpriteRenderer requisite2SpriteShadow;
  public Transform requisite3;
  public SpriteRenderer requisite3Sprite;
  public SpriteRenderer requisite3SpriteShadow;
  public SpriteRenderer nodeImage;
  public SpriteRenderer nodeDecor;
  public Sprite nodeImageTown;
  public Sprite nodeImageCombat;
  public Sprite nodeImageCombatRare;
  public Sprite nodeImageFinalBoss;
  public Sprite nodeImageEvent;
  public Transform nodeImageParticlesT;
  private ParticleSystem nodeImageParticlesSystem;
  private SpriteRenderer availableSR;
  private SpriteRenderer plainSR;
  private Animator anim;
  private bool highlightedNode;
  private Color colorReset = new Color(1f, 1f, 1f, 1f);
  private Color colorAvailable = new Color(0.27f, 0.85f, 0.27f);
  private Color colorAvailableOff = new Color(1f, 0.46f, 0.82f, 0.85f);
  private int nodeNumeral;
  public string action;
  public string actionId;
  public Transform playersMarkT;
  public Transform[] playersMark;
  public SpriteRenderer[] playersMarkSpr;

  private void Awake()
  {
    this.plainSR = this.plainT.GetComponent<SpriteRenderer>();
    this.availableSR = this.availableT.GetComponent<SpriteRenderer>();
    this.nodeImageParticlesSystem = this.nodeImageParticlesT.GetComponent<ParticleSystem>();
    this.anim = this.nodeImage.transform.GetComponent<Animator>();
    this.requisiteLayer.gameObject.SetActive(false);
    this.requisite.gameObject.SetActive(false);
    this.requisite2.gameObject.SetActive(false);
    this.requisite3.gameObject.SetActive(false);
  }

  private void StartNode()
  {
    this.nodeDecor.sprite = (Sprite) null;
    this.chestT.gameObject.SetActive(false);
    if (!((UnityEngine.Object) this.plainSR == (UnityEngine.Object) null))
      return;
    this.plainSR = this.plainT.GetComponent<SpriteRenderer>();
    this.availableSR = this.availableT.GetComponent<SpriteRenderer>();
    this.anim = this.nodeImage.transform.GetComponent<Animator>();
  }

  public void InitNode()
  {
    this.StartNode();
    if (!((UnityEngine.Object) this.nodeData != (UnityEngine.Object) null))
      return;
    string lower = this.nodeData.NodeId.ToLower();
    this.gameObject.name = lower;
    this.nodeNumeral = int.Parse(lower.Split('_', StringSplitOptions.None)[1]);
  }

  public bool Exists() => !(this.GetNodeAssignedId() == "");

  public string GetNodeAction() => AtOManager.Instance.gameNodeAssigned.ContainsKey(this.nodeData.NodeId) ? AtOManager.Instance.gameNodeAssigned[this.nodeData.NodeId].Split(':', StringSplitOptions.None)[0] : "";

  public string GetNodeAssignedId()
  {
    if (AtOManager.Instance.gameNodeAssigned == null || !((UnityEngine.Object) this.nodeData != (UnityEngine.Object) null) || !AtOManager.Instance.gameNodeAssigned.ContainsKey(this.nodeData.NodeId))
      return "";
    string[] strArray = AtOManager.Instance.gameNodeAssigned[this.nodeData.NodeId].Split(':', StringSplitOptions.None);
    return strArray != null && strArray.Length > 1 ? strArray[1] : "";
  }

  public void AssignNode()
  {
    this.chestT.gameObject.SetActive(false);
    bool flag1 = false;
    if (!((UnityEngine.Object) this.nodeData != (UnityEngine.Object) null))
      return;
    this.action = this.GetNodeAction();
    this.actionId = this.GetNodeAssignedId();
    if (this.actionId == "")
    {
      this.SetHidden();
    }
    else
    {
      if (this.nodeData.NodeId == "of1_10" || this.nodeData.NodeId == "of2_10")
        flag1 = true;
      if (this.nodeData.GoToTown)
        this.nodeImage.sprite = this.nodeImageTown;
      else if (this.action == "combat")
      {
        if (GameManager.Instance.IsObeliskChallenge() && AtOManager.Instance.NodeHaveBossRare(this.nodeData.NodeId) | flag1)
        {
          if ((UnityEngine.Object) this.nodeImageParticlesT != (UnityEngine.Object) null)
          {
            this.nodeImageParticlesT.gameObject.SetActive(true);
            if ((UnityEngine.Object) this.nodeImageParticlesSystem == (UnityEngine.Object) null)
              this.nodeImageParticlesSystem = this.nodeImageParticlesT.GetComponent<ParticleSystem>();
            if ((UnityEngine.Object) this.nodeImageParticlesSystem != (UnityEngine.Object) null)
              this.nodeImageParticlesSystem.main.startColor = ((this.nodeData.NodeCombatTier == Enums.CombatTier.T8 ? 1 : (this.nodeData.NodeCombatTier == Enums.CombatTier.T9 ? 1 : 0)) | (flag1 ? 1 : 0)) == 0 ? (ParticleSystem.MinMaxGradient) new Color(0.1f, 0.6f, 1f) : (ParticleSystem.MinMaxGradient) new Color(1f, 0.1f, 0.15f);
          }
          if (((this.nodeData.NodeCombatTier == Enums.CombatTier.T8 ? 1 : (this.nodeData.NodeCombatTier == Enums.CombatTier.T9 ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
          {
            this.nodeImage.sprite = this.nodeImageFinalBoss;
          }
          else
          {
            this.nodeImage.sprite = this.nodeImageCombatRare;
            this.chestT.gameObject.SetActive(true);
          }
        }
        else
          this.nodeImage.sprite = this.nodeImageCombat;
      }
      else if (this.action == "event")
      {
        EventData eventData = Globals.Instance.GetEventData(this.actionId);
        this.nodeImage.sprite = !((UnityEngine.Object) eventData != (UnityEngine.Object) null) || !((UnityEngine.Object) eventData.EventSpriteMap != (UnityEngine.Object) null) ? this.nodeImageEvent : eventData.EventSpriteMap;
        if ((UnityEngine.Object) eventData != (UnityEngine.Object) null && (UnityEngine.Object) eventData.EventSpriteDecor != (UnityEngine.Object) null)
          this.nodeDecor.sprite = eventData.EventSpriteDecor;
        if ((UnityEngine.Object) eventData != (UnityEngine.Object) null && eventData.EventIconShader != Enums.MapIconShader.None)
        {
          int num = (UnityEngine.Object) this.anim != (UnityEngine.Object) null ? 1 : 0;
          if ((UnityEngine.Object) this.nodeImageParticlesT != (UnityEngine.Object) null)
          {
            this.nodeImageParticlesT.gameObject.SetActive(true);
            if ((UnityEngine.Object) this.nodeImageParticlesSystem == (UnityEngine.Object) null)
              this.nodeImageParticlesSystem = this.nodeImageParticlesT.GetComponent<ParticleSystem>();
            if ((UnityEngine.Object) this.nodeImageParticlesSystem != (UnityEngine.Object) null)
            {
              ParticleSystem.MainModule main = this.nodeImageParticlesSystem.main;
              if (eventData.EventIconShader == Enums.MapIconShader.Green)
                main.startColor = (ParticleSystem.MinMaxGradient) Color.green;
              else if (eventData.EventIconShader == Enums.MapIconShader.Blue)
                main.startColor = (ParticleSystem.MinMaxGradient) new Color(0.1f, 0.6f, 1f);
              else if (eventData.EventIconShader == Enums.MapIconShader.Purple)
                main.startColor = (ParticleSystem.MinMaxGradient) new Color(0.91f, 0.05f, 0.87f);
              else if (eventData.EventIconShader == Enums.MapIconShader.Orange)
                main.startColor = (ParticleSystem.MinMaxGradient) new Color(1f, 0.69f, 0.0f);
              else if (eventData.EventIconShader == Enums.MapIconShader.Red)
                main.startColor = (ParticleSystem.MinMaxGradient) Color.red;
              else if (eventData.EventIconShader == Enums.MapIconShader.Black)
                main.startColor = (ParticleSystem.MinMaxGradient) Color.black;
            }
          }
        }
        else if ((UnityEngine.Object) this.nodeImageParticlesT != (UnityEngine.Object) null)
          this.nodeImageParticlesT.gameObject.SetActive(false);
        this.requisiteLayer.gameObject.SetActive(false);
        this.requisite.gameObject.SetActive(false);
        this.requisite2.gameObject.SetActive(false);
        this.requisite3.gameObject.SetActive(false);
        if (!this.currentT.gameObject.activeSelf && !this.blockedT.gameObject.activeSelf && (UnityEngine.Object) eventData != (UnityEngine.Object) null)
        {
          bool flag2 = false;
          bool flag3 = false;
          for (int index = 0; index < eventData.Replys.Length; ++index)
          {
            EventReplyData reply = eventData.Replys[index];
            if ((UnityEngine.Object) reply.Requirement != (UnityEngine.Object) null && (UnityEngine.Object) reply.Requirement.ItemSprite != (UnityEngine.Object) null && AtOManager.Instance.PlayerHasRequirement(reply.Requirement))
            {
              if (!flag2)
              {
                this.requisiteSprite.GetComponent<EventItemTrack>().SetItemTrack(Globals.Instance.GetRequirementData(reply.Requirement.RequirementId));
                this.requisiteSprite.sprite = this.requisiteSpriteShadow.sprite = reply.Requirement.ItemSprite;
                this.requisite.gameObject.SetActive(true);
                flag2 = true;
              }
              else if (!flag3)
              {
                if ((UnityEngine.Object) reply.Requirement.ItemSprite != (UnityEngine.Object) this.requisiteSprite.sprite)
                {
                  this.requisite2Sprite.GetComponent<EventItemTrack>().SetItemTrack(Globals.Instance.GetRequirementData(reply.Requirement.RequirementId));
                  this.requisite2Sprite.sprite = this.requisite2SpriteShadow.sprite = reply.Requirement.ItemSprite;
                  this.requisite2.gameObject.SetActive(true);
                  flag3 = true;
                }
              }
              else if ((UnityEngine.Object) reply.Requirement.ItemSprite != (UnityEngine.Object) this.requisiteSprite.sprite && (UnityEngine.Object) reply.Requirement.ItemSprite != (UnityEngine.Object) this.requisite2Sprite.sprite)
              {
                this.requisite3Sprite.GetComponent<EventItemTrack>().SetItemTrack(Globals.Instance.GetRequirementData(reply.Requirement.RequirementId));
                this.requisite3Sprite.sprite = this.requisite3SpriteShadow.sprite = reply.Requirement.ItemSprite;
                this.requisite3.gameObject.SetActive(true);
              }
              if (this.requisite.gameObject.activeSelf || this.requisite2.gameObject.activeSelf || this.requisite3.gameObject.activeSelf)
                this.requisiteLayer.gameObject.SetActive(true);
            }
          }
        }
      }
      this.AssignBackground();
    }
  }

  public void AssignBackground()
  {
    if (!((UnityEngine.Object) this.nodeData.NodeBackgroundImg != (UnityEngine.Object) null))
      return;
    this.background.sprite = this.nodeData.NodeBackgroundImg;
  }

  public IEnumerator ShowNode()
  {
    Node node = this;
    for (int index = 0; index < node.transform.childCount; ++index)
    {
      if (node.transform.GetChild(index).gameObject.name == "mapPiece")
        yield break;
    }
    node.transform.localScale = Vector3.zero;
    yield return (object) Globals.Instance.WaitForSeconds(0.2f);
    float size = 0.0f;
    float increment = 0.06f;
    float delay = 0.01f;
    while ((double) size < 1.2000000476837158)
    {
      size += increment;
      node.transform.localScale = new Vector3(size, size, 1f);
      yield return (object) Globals.Instance.WaitForSeconds(delay);
    }
    while ((double) size > 1.0)
    {
      size -= increment;
      node.transform.localScale = new Vector3(size, size, 1f);
      yield return (object) Globals.Instance.WaitForSeconds(delay);
    }
    node.transform.localScale = new Vector3(1f, 1f, 1f);
  }

  public void SetHidden() => this.gameObject.SetActive(false);

  public void SetPlain()
  {
    if (this.Exists())
    {
      this.HideStates();
      this.plainT.gameObject.SetActive(true);
      this.nodeImage.transform.gameObject.SetActive(true);
      this.plainSR.color = this.colorReset;
    }
    else
      this.SetHidden();
  }

  public void SetAvailable()
  {
    if (this.Exists())
    {
      this.plainT.gameObject.SetActive(false);
      this.HideStates();
      this.availableT.gameObject.SetActive(true);
      if ((UnityEngine.Object) this.anim != (UnityEngine.Object) null)
        this.anim.enabled = true;
      if (!this.gameObject.activeSelf || !this.transform.parent.transform.parent.gameObject.activeSelf)
        return;
      this.StartCoroutine(this.ShowNode());
    }
    else
      this.SetHidden();
  }

  public void SetActive()
  {
    if (this.Exists())
    {
      this.plainT.gameObject.SetActive(false);
      this.nodeImage.transform.gameObject.SetActive(false);
      this.HideStates();
      this.currentT.gameObject.SetActive(true);
      this.requisiteLayer.gameObject.SetActive(false);
      this.requisite.gameObject.SetActive(false);
      this.requisite2.gameObject.SetActive(false);
      this.requisite3.gameObject.SetActive(false);
    }
    else
      this.SetHidden();
  }

  public void SetVisited()
  {
    if (this.Exists())
    {
      this.plainT.gameObject.SetActive(false);
      this.nodeImage.transform.gameObject.SetActive(false);
      this.HideStates();
      this.visitedT.gameObject.SetActive(true);
      this.requisiteLayer.gameObject.SetActive(false);
      this.requisite.gameObject.SetActive(false);
      this.requisite2.gameObject.SetActive(false);
      this.requisite3.gameObject.SetActive(false);
    }
    else
      this.SetHidden();
  }

  public void SetBlocked()
  {
    if (this.Exists())
    {
      this.plainT.gameObject.SetActive(false);
      this.nodeImage.transform.gameObject.SetActive(false);
      this.HideStates();
      this.blockedT.gameObject.SetActive(true);
      this.requisiteLayer.gameObject.SetActive(false);
      this.requisite.gameObject.SetActive(false);
      this.requisite2.gameObject.SetActive(false);
      this.requisite3.gameObject.SetActive(false);
    }
    else
      this.SetHidden();
  }

  private void HideStates()
  {
    this.availableT.gameObject.SetActive(false);
    this.blockedT.gameObject.SetActive(false);
    this.currentT.gameObject.SetActive(false);
    this.visitedT.gameObject.SetActive(false);
  }

  public void HighlightNode(bool status)
  {
    MapManager.Instance.DrawArrow(MapManager.Instance.nodeActive, this, status);
    this.highlightedNode = status;
    if (status)
      this.availableSR.color = this.plainSR.color = this.colorAvailable;
    else
      this.availableSR.color = this.plainSR.color = this.colorReset;
  }

  private void HoverNode(bool status)
  {
    if (status)
    {
      if (!this.plainT.gameObject.activeSelf)
        return;
      this.plainSR.color = this.colorAvailableOff;
    }
    else
    {
      if (!this.plainT.gameObject.activeSelf)
        return;
      this.plainSR.color = this.colorReset;
    }
  }

  public void ShowSelectedNode(string _nick)
  {
    for (int index = 0; index < this.playersMark.Length; ++index)
    {
      if (!this.playersMark[index].gameObject.activeSelf)
      {
        this.playersMark[index].gameObject.SetActive(true);
        this.playersMarkSpr[index].color = Functions.HexToColor(NetworkManager.Instance.GetColorFromNick(_nick));
        this.playersMarkT.gameObject.SetActive(true);
        break;
      }
    }
  }

  public void ClearSelectedNode()
  {
    for (int index = 0; index < this.playersMark.Length; ++index)
      this.playersMark[index].gameObject.SetActive(false);
    this.playersMarkT.gameObject.SetActive(false);
  }

  public void OnMouseUp()
  {
    if (!Functions.ClickedThisTransform(this.transform) || AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || (bool) (UnityEngine.Object) MapManager.Instance && MapManager.Instance.IsCharacterUnlock() || (bool) (UnityEngine.Object) MapManager.Instance && (MapManager.Instance.IsCorruptionOver() || MapManager.Instance.IsConflictOver()) || (bool) (UnityEngine.Object) MapManager.Instance && MapManager.Instance.selectedNode || (bool) (UnityEngine.Object) EventManager.Instance)
      return;
    GameManager.Instance.SetCursorPlain();
    MapManager.Instance.HidePopup();
    if (!MapManager.Instance.CanTravelToThisNode(this))
      return;
    MapManager.Instance.PlayerSelectedNode(this);
  }

  private void OnMouseOver()
  {
    if (AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || (bool) (UnityEngine.Object) MapManager.Instance && MapManager.Instance.IsCharacterUnlock() || (bool) (UnityEngine.Object) MapManager.Instance && (MapManager.Instance.IsCorruptionOver() || MapManager.Instance.IsConflictOver()) || (bool) (UnityEngine.Object) EventManager.Instance || !GameManager.Instance.GetDeveloperMode() || EventSystem.current.IsPointerOverGameObject() || !Input.GetMouseButtonUp(1) || GameManager.Instance.IsMultiplayer() && (!GameManager.Instance.IsMultiplayer() || !NetworkManager.Instance.IsMaster()))
      return;
    MapManager.Instance.TravelToThisNode(this);
  }

  private void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || (bool) (UnityEngine.Object) MapManager.Instance && MapManager.Instance.IsCharacterUnlock() || (bool) (UnityEngine.Object) MapManager.Instance && (MapManager.Instance.IsCorruptionOver() || MapManager.Instance.IsConflictOver()) || (bool) (UnityEngine.Object) EventManager.Instance || EventSystem.current.IsPointerOverGameObject() || !(this.GetNodeAssignedId() != ""))
      return;
    MapManager.Instance.ShowPopup(this);
    if (MapManager.Instance.CanTravelToThisNode(this))
    {
      GameManager.Instance.SetCursorHover();
      this.HighlightNode(true);
    }
    else
      this.HoverNode(true);
    if (!((UnityEngine.Object) MapManager.Instance.nodeActive != (UnityEngine.Object) this) || this.nodeData.NodeId == "sen_41" && (AtOManager.Instance.currentMapNode == "tutorial_0" || AtOManager.Instance.currentMapNode == "tutorial_1" || AtOManager.Instance.currentMapNode == "tutorial_2"))
      return;
    MapManager.Instance.DrawArrowsTemp(this);
  }

  public void OnMouseExit()
  {
    if (AlertManager.Instance.IsActive() || (bool) (UnityEngine.Object) EventManager.Instance)
      return;
    GameManager.Instance.SetCursorPlain();
    if (EventSystem.current.IsPointerOverGameObject())
      return;
    MapManager.Instance.HidePopup();
    if (this.highlightedNode)
      this.HighlightNode(false);
    else
      this.HoverNode(false);
    if (!((UnityEngine.Object) MapManager.Instance.nodeActive != (UnityEngine.Object) this))
      return;
    MapManager.Instance.HideArrowsTemp(this);
  }
}
