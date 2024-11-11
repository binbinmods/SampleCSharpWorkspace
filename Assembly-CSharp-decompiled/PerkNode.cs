// Decompiled with JetBrains decompiler
// Type: PerkNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class PerkNode : MonoBehaviour
{
  public PerkNodeData PND;
  public LineRenderer nodeLine;
  public Transform iconLock;
  public Transform bgSquarePlain;
  public Transform bgSquareGold;
  public Transform bgSquareSelected;
  public Transform bgSquareHover;
  public Transform bgSquareLocked;
  public Transform bgCirclePlain;
  public Transform bgCirclePlainSelected;
  public Transform bgCirclePlainHover;
  public Transform bgCirclePlainLocked;
  public Transform bgCircleGold;
  public Transform bgCircleGoldSelected;
  public Transform bgCircleGoldHover;
  public Transform bgCircleGoldLocked;
  public Transform teamSelected;
  public TMP_Text teamSelectedNum;
  private List<string> teamSelectedCharacters;
  public Transform amplify;
  public SpriteRenderer nodeSprite;
  private BoxCollider2D boxCollider;
  private Vector2 boxColliderSizeOri;
  private Vector2 bcOffset = new Vector2(0.0f, -1.1f);
  private Vector2 bcSize2 = new Vector2(4.5f, 3.6f);
  private Vector2 bcSize3 = new Vector2(5.4f, 3.6f);
  private Vector2 bcSize4 = new Vector2(6.3f, 3.6f);
  private string nodeId;
  private PerkNode perkNodeConnected;
  private int nodeType;
  private int nodeRow;
  private int nodeCost;
  private bool nodeLocked;
  private bool nodeRequired;
  private bool nodeSelected;
  private bool nodeIsChild;
  private string textPopup = "";
  private Coroutine amplifyCo;
  private float oriScale;
  private PerkNodeAmplify nodeAmplify;

  private void Awake()
  {
    this.boxCollider = this.GetComponent<BoxCollider2D>();
    this.boxColliderSizeOri = this.boxCollider.size;
  }

  private void Update()
  {
    if (!((UnityEngine.Object) this.nodeAmplify != (UnityEngine.Object) null) || (double) this.transform.localPosition.z != -2.0 || this.nodeAmplify.gameObject.activeSelf)
      return;
    this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0.0f);
  }

  public void SetIconLock(bool _state = false)
  {
    if (_state)
      this.iconLock.gameObject.SetActive(true);
    else
      this.iconLock.gameObject.SetActive(false);
  }

  public void SetNodeAsChild(bool _state = true)
  {
    this.nodeIsChild = _state;
    if (!_state)
      return;
    this.SetChildLayers();
  }

  public bool IsChildNode() => this.nodeIsChild;

  public void SetValuesAsChildNode(PerkNode _pn)
  {
    this.nodeSprite.sprite = _pn.nodeSprite.sprite;
    this.perkNodeConnected = _pn;
    this.SetSelected(true);
  }

  public void SetDefaultIcon()
  {
    this.nodeSprite.sprite = this.PND.Sprite;
    this.perkNodeConnected = (PerkNode) null;
  }

  public void SetPND(PerkNodeData _pnd)
  {
    this.oriScale = this.transform.localScale.x;
    this.nodeLine.transform.parent = this.transform.parent;
    this.PND = _pnd;
    this.nodeId = this.PND.Id;
    this.nodeRow = this.PND.Row;
    this.nodeCost = PerkTree.Instance.GetPointsForNode(this.PND);
    this.nodeType = !this.nodeIsChild ? (_pnd.PerksConnected.Length == 0 ? (_pnd.Cost != Enums.PerkCost.PerkCostBase ? 1 : 0) : (_pnd.Cost != Enums.PerkCost.PerkCostBase ? 3 : 2)) : 3;
    this.nodeSprite.sprite = _pnd.Sprite;
    this.Init();
  }

  public void TeamSelected(int _number, List<string> _members = null)
  {
    if (_number > 0)
    {
      this.teamSelected.gameObject.SetActive(true);
      this.teamSelectedNum.text = _number.ToString();
      this.teamSelectedCharacters = _members;
    }
    else
    {
      this.teamSelected.gameObject.SetActive(false);
      this.teamSelectedCharacters = (List<string>) null;
    }
  }

  public void Init()
  {
    this.DeselectNode();
    this.TeamSelected(0);
    foreach (Transform transform in PerkTree.Instance.nodes.GetChild(this.PND.Type))
    {
      if (transform.gameObject.name == this.gameObject.name + "_amplify")
      {
        this.amplify = transform;
        this.nodeAmplify = this.amplify.gameObject.GetComponent<PerkNodeAmplify>();
        break;
      }
    }
    if ((UnityEngine.Object) this.amplify != (UnityEngine.Object) null)
      this.amplify.gameObject.SetActive(false);
    this.SetNodeGraphic();
  }

  private void SetNodeGraphic()
  {
    if (this.nodeType == 0)
    {
      this.bgSquarePlain.gameObject.SetActive(false);
      this.bgSquareGold.gameObject.SetActive(false);
      this.bgCirclePlain.gameObject.SetActive(true);
      this.bgCircleGold.gameObject.SetActive(false);
    }
    else if (this.nodeType == 1)
    {
      this.bgSquarePlain.gameObject.SetActive(false);
      this.bgSquareGold.gameObject.SetActive(false);
      this.bgCirclePlain.gameObject.SetActive(false);
      this.bgCircleGold.gameObject.SetActive(true);
    }
    else if (this.nodeType == 2)
    {
      this.bgSquarePlain.gameObject.SetActive(true);
      this.bgSquareGold.gameObject.SetActive(false);
      this.bgCirclePlain.gameObject.SetActive(false);
      this.bgCircleGold.gameObject.SetActive(false);
    }
    else
    {
      if (this.nodeType != 3)
        return;
      this.bgSquarePlain.gameObject.SetActive(false);
      this.bgSquareGold.gameObject.SetActive(true);
      this.bgCirclePlain.gameObject.SetActive(false);
      this.bgCircleGold.gameObject.SetActive(false);
    }
  }

  public void SetArrow(Vector3 _from, Vector3 _to)
  {
    if (!this.nodeIsChild)
    {
      this.nodeLine.SetPosition(0, _from);
      this.nodeLine.SetPosition(1, _to);
      this.nodeLine.transform.gameObject.SetActive(true);
    }
    else
      this.nodeLine.transform.gameObject.SetActive(false);
  }

  public void SetRequired(bool _status) => this.nodeRequired = _status;

  public void SetLocked(bool _status)
  {
    this.nodeLocked = _status;
    if (this.nodeType == 0)
      this.bgCirclePlainLocked.gameObject.SetActive(_status);
    else if (this.nodeType == 1)
      this.bgCircleGoldLocked.gameObject.SetActive(_status);
    else
      this.bgSquareLocked.gameObject.SetActive(_status);
    if (_status)
    {
      this.nodeSprite.color = new Color(0.5f, 0.5f, 0.5f);
      LineRenderer nodeLine1 = this.nodeLine;
      LineRenderer nodeLine2 = this.nodeLine;
      Color color1 = new Color(this.nodeLine.startColor.r, this.nodeLine.startColor.g, this.nodeLine.startColor.b, 0.3f);
      Color color2 = color1;
      nodeLine2.endColor = color2;
      Color color3 = color1;
      nodeLine1.startColor = color3;
    }
    else
    {
      this.nodeSprite.color = !this.nodeSelected ? new Color(0.8f, 0.8f, 0.8f) : Color.white;
      LineRenderer nodeLine3 = this.nodeLine;
      LineRenderer nodeLine4 = this.nodeLine;
      Color color4 = new Color(this.nodeLine.startColor.r, this.nodeLine.startColor.g, this.nodeLine.startColor.b, 1f);
      Color color5 = color4;
      nodeLine4.endColor = color5;
      Color color6 = color4;
      nodeLine3.startColor = color6;
    }
  }

  public bool IsLocked() => this.nodeLocked;

  public void SetSelected(bool _status)
  {
    this.nodeSelected = _status;
    if (this.nodeType == 0)
      this.bgCirclePlainSelected.gameObject.SetActive(_status);
    else if (this.nodeType == 1)
      this.bgCircleGoldSelected.gameObject.SetActive(_status);
    else
      this.bgSquareSelected.gameObject.SetActive(_status);
  }

  public bool IsSelected() => this.nodeSelected;

  public int GetRow() => this.nodeRow;

  private void ResetNode() => this.DeselectNode();

  private void DeselectNode()
  {
    this.bgSquareSelected.gameObject.SetActive(false);
    this.bgCirclePlainSelected.gameObject.SetActive(false);
    this.bgCircleGoldSelected.gameObject.SetActive(false);
  }

  public string NewPerkDescription(PerkData perkData)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (this.PND.PerksConnected.Length != 0)
    {
      if (this.PND.PerksConnected != null && this.PND.PerksConnected.Length != 0 && (UnityEngine.Object) this.perkNodeConnected != (UnityEngine.Object) null)
      {
        perkData = this.perkNodeConnected.PND.Perk;
      }
      else
      {
        stringBuilder.Append("<size=+5>");
        stringBuilder.Append(Texts.Instance.GetText("chooseOnePerk"));
        stringBuilder.Append("</size>");
        perkData = (PerkData) null;
      }
    }
    if ((UnityEngine.Object) perkData != (UnityEngine.Object) null)
    {
      if (perkData.CustomDescription != null && perkData.CustomDescription.Trim() != "")
        stringBuilder.Append(Texts.Instance.GetText(perkData.CustomDescription));
      else if (perkData.MaxHealth > 0)
        stringBuilder.Append(string.Format(Texts.Instance.GetText("itemMaxHp"), (object) perkData.IconTextValue));
      else if (perkData.AdditionalCurrency > 0)
        stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemInitialCurrencySingle")), (object) perkData.IconTextValue));
      else if (perkData.AdditionalShards > 0)
        stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemInitialShardsSingle")), (object) perkData.IconTextValue));
      else if (perkData.SpeedQuantity > 0)
        stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemSpeed")), (object) perkData.IconTextValue));
      else if (perkData.HealQuantity > 0)
        stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemHealDone").Replace("<c>", "")), (object) perkData.IconTextValue));
      else if (perkData.EnergyBegin > 0)
        stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemInitialEnergy")), (object) perkData.IconTextValue));
      else if ((UnityEngine.Object) perkData.AuracurseBonus != (UnityEngine.Object) null)
        stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("perkAuraDescription")), (object) perkData.IconTextValue));
      else if (perkData.ResistModified == Enums.DamageType.All)
        stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemAllResistances")), (object) perkData.IconTextValue));
      else if (perkData.DamageFlatBonus == Enums.DamageType.All)
        stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemAllDamages")), (object) perkData.IconTextValue));
      else if (perkData.DamageFlatBonus == Enums.DamageType.All)
        stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemAllDamages")), (object) perkData.IconTextValue));
      else if (perkData.DamageFlatBonus != Enums.DamageType.None && perkData.DamageFlatBonus != Enums.DamageType.All)
        stringBuilder.Append(string.Format(Functions.UppercaseFirst(Texts.Instance.GetText("itemSingleDamage")), (object) Texts.Instance.GetText(Enum.GetName(typeof (Enums.DamageType), (object) perkData.DamageFlatBonus)), (object) perkData.IconTextValue));
      if (stringBuilder.Length > 0)
      {
        if (this.PND.NotStack || (UnityEngine.Object) this.perkNodeConnected != (UnityEngine.Object) null && (UnityEngine.Object) this.perkNodeConnected.PND != (UnityEngine.Object) null && this.perkNodeConnected.PND.NotStack)
        {
          stringBuilder.Append("  <nobr><color=#A0A0A0>");
          stringBuilder.Append(Texts.Instance.GetText("notStack"));
          stringBuilder.Append("</color></nobr>");
        }
        stringBuilder.Append("  <nobr><color=#A0A0A0>[");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("cardsCost"), (object) (": " + this.nodeCost.ToString())));
        stringBuilder.Append("]</color></nobr>");
        if (this.nodeLocked)
        {
          stringBuilder.Append("<br></size><line-height=5><color=#FF6666>");
          if (!this.nodeRequired)
            stringBuilder.Append(string.Format(Texts.Instance.GetText("requiredPoints"), (object) PerkTree.Instance.GetPointsNeeded(this.nodeRow)));
          else
            stringBuilder.Append(Texts.Instance.GetText("previousRequired"));
        }
        else if (!this.nodeSelected)
        {
          if (PerkTree.Instance.GetPointsAvailable() > 0)
          {
            if (PerkTree.Instance.GetPointsAvailable() < this.nodeCost)
            {
              stringBuilder.Append("<br></size><line-height=5><color=#FF6666>");
              stringBuilder.Append(string.Format(Texts.Instance.GetText("requiredPoints"), (object) (this.nodeCost - PerkTree.Instance.GetPointsAvailable())));
            }
            else if (PerkTree.Instance.CanModify() && !this.iconLock.gameObject.activeSelf)
            {
              stringBuilder.Append("<br></size><line-height=5><color=#3BA12A>");
              stringBuilder.Append(Texts.Instance.GetText("rankPerkPress"));
            }
          }
          else
          {
            stringBuilder.Append("<br></size><line-height=5><color=#FF6666>");
            stringBuilder.Append(Texts.Instance.GetText("rankPerkNotEnough"));
            this.enabled = false;
          }
        }
        stringBuilder.Insert(0, "<color=#FFF><size=+5>");
      }
    }
    if (this.teamSelectedCharacters != null)
    {
      stringBuilder.Append("</line-height><br><line-height=18><color=#E5A462><size=20>");
      stringBuilder.Append(Texts.Instance.GetText("partyMembersWithPerk"));
      stringBuilder.Append("<br>(");
      stringBuilder.Append(this.teamSelectedCharacters.Count);
      stringBuilder.Append(")  ");
      for (int index = 0; index < this.teamSelectedCharacters.Count; ++index)
      {
        if (this.teamSelectedCharacters[index] != null && (UnityEngine.Object) Globals.Instance.GetSubClassData(this.teamSelectedCharacters[index]) != (UnityEngine.Object) null)
        {
          stringBuilder.Append(Globals.Instance.GetSubClassData(this.teamSelectedCharacters[index]).CharacterName);
          if (index < this.teamSelectedCharacters.Count - 1)
            stringBuilder.Append(", ");
        }
      }
      stringBuilder.Append("</size></color></line-height>");
    }
    if (stringBuilder.Length <= 0)
      return "";
    stringBuilder.Replace("<c>", "");
    stringBuilder.Replace("</c>", "");
    return stringBuilder.ToString();
  }

  private void DoPopup()
  {
    if ((this.PND.PerksConnected == null || this.PND.PerksConnected.Length == 0) && ((UnityEngine.Object) this.PND.Perk == (UnityEngine.Object) null || !this.PND.Perk.MainPerk))
      return;
    this.textPopup = this.NewPerkDescription(this.PND.Perk);
    if (!(this.textPopup != "") || (UnityEngine.Object) this.nodeSprite == (UnityEngine.Object) null)
      return;
    AuraCurseData auraCurseData = Globals.Instance.GetAuraCurseData(this.nodeSprite.sprite.name);
    string keynote = "";
    if ((UnityEngine.Object) auraCurseData != (UnityEngine.Object) null)
      keynote = this.nodeSprite.sprite.name;
    PopupManager.Instance.SetPerk(this.nodeId, this.textPopup, keynote);
    if (!this.nodeLocked)
      PopupManager.Instance.SetBackgroundColor("#226529");
    else
      PopupManager.Instance.SetBackgroundColor("#652523");
  }

  public void SetChildLayers()
  {
    int num1 = 150;
    foreach (Transform transform1 in this.transform)
    {
      SpriteRenderer component1 = transform1.GetComponent<SpriteRenderer>();
      if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
        component1.sortingOrder += num1;
      int num2 = 0;
      foreach (Transform transform2 in transform1)
      {
        SpriteRenderer component2 = transform2.GetComponent<SpriteRenderer>();
        if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
        {
          component2.sortingOrder += num1;
          if (transform2.name == "Circle")
            num2 = component2.sortingOrder;
        }
        else if ((UnityEngine.Object) transform2.GetComponent<MeshRenderer>() != (UnityEngine.Object) null)
          transform2.GetComponent<MeshRenderer>().sortingOrder = num2 + 1;
        foreach (Component component3 in transform2)
        {
          SpriteRenderer component4 = component3.GetComponent<SpriteRenderer>();
          if ((UnityEngine.Object) component4 != (UnityEngine.Object) null)
            component4.sortingOrder += num1;
        }
      }
    }
  }

  public void OnMouseUp()
  {
    if (!Functions.ClickedThisTransform(this.transform) || SettingsManager.Instance.IsActive() || AlertManager.Instance.IsActive() || MadnessManager.Instance.IsActive() || SandboxManager.Instance.IsActive() || !PerkTree.Instance.IsOwner || this.nodeLocked || !PerkTree.Instance.CanModify() || this.iconLock.gameObject.activeSelf)
      return;
    if ((UnityEngine.Object) this.PND.Perk != (UnityEngine.Object) null)
    {
      PerkTree.Instance.SelectPerk(this.PND.Perk.Id, this);
      this.DoPopup();
    }
    else
    {
      if (this.PND.PerksConnected == null || this.PND.PerksConnected.Length == 0 || !((UnityEngine.Object) this.perkNodeConnected != (UnityEngine.Object) null))
        return;
      PerkTree.Instance.SelectPerk(this.perkNodeConnected.PND.Perk.Id, this.perkNodeConnected);
      this.DoPopup();
    }
  }

  private void OnMouseEnter()
  {
    if (SettingsManager.Instance.IsActive() || AlertManager.Instance.IsActive() || MadnessManager.Instance.IsActive() || SandboxManager.Instance.IsActive())
      return;
    this.DoPopup();
    if (this.PND.PerksConnected.Length != 0)
    {
      if (!((UnityEngine.Object) this.amplify != (UnityEngine.Object) null))
        return;
      this.amplifyCo = this.StartCoroutine(this.OpenAmplify());
    }
    else
    {
      if (this.nodeLocked || !PerkTree.Instance.CanModify() || this.iconLock.gameObject.activeSelf)
        return;
      if (this.nodeType == 0)
        this.bgCirclePlainHover.gameObject.SetActive(true);
      else if (this.nodeType == 1)
        this.bgCircleGoldHover.gameObject.SetActive(true);
      else
        this.bgSquareHover.gameObject.SetActive(true);
      this.transform.localScale = new Vector3(this.oriScale + 0.1f, this.oriScale + 0.1f, 1f);
      GameManager.Instance.PlayLibraryAudio("ui_menu_popup_01");
      GameManager.Instance.SetCursorHover();
    }
  }

  private IEnumerator OpenAmplify()
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    PerkNode perkNode = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      perkNode.transform.localPosition = new Vector3(perkNode.transform.localPosition.x, perkNode.transform.localPosition.y, -2f);
      perkNode.nodeAmplify.Show();
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) Globals.Instance.WaitForSeconds(0.2f);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  private void OnMouseExit()
  {
    GameManager.Instance.SetCursorPlain();
    PopupManager.Instance.ClosePopup();
    if (this.amplifyCo != null)
      this.StopCoroutine(this.amplifyCo);
    if (SettingsManager.Instance.IsActive() || AlertManager.Instance.IsActive() || MadnessManager.Instance.IsActive() || SandboxManager.Instance.IsActive() || !PerkTree.Instance.IsOwner)
      return;
    if (this.PND.PerksConnected.Length != 0)
    {
      if (!((UnityEngine.Object) this.amplify != (UnityEngine.Object) null))
        ;
    }
    else if (!this.nodeLocked && PerkTree.Instance.CanModify() && !this.iconLock.gameObject.activeSelf)
    {
      if (this.nodeType == 0)
        this.bgCirclePlainHover.gameObject.SetActive(false);
      else if (this.nodeType == 1)
        this.bgCircleGoldHover.gameObject.SetActive(false);
      else
        this.bgSquareHover.gameObject.SetActive(false);
    }
    this.transform.localScale = new Vector3(this.oriScale, this.oriScale, 1f);
  }
}
