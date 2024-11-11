// Decompiled with JetBrains decompiler
// Type: PerkTree
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PerkTree : MonoBehaviour
{
  public Transform elements;
  public Transform nodes;
  public GameObject perkNodeGO;
  public GameObject perkNodeAmplifyGO;
  public Transform posBegin;
  public Transform posEnd;
  public SpriteRenderer charSprite;
  public TMP_Text availablePerksPoints;
  public TMP_Text usedPerksPoints;
  public PerkBackgroundRow[] perkBgRow;
  public Transform[] categoryT;
  public BotonGeneric[] buttonType;
  public BotonGeneric buttonConfirm;
  public BotonGeneric buttonReset;
  public BotonGeneric buttonImport;
  public BotonGeneric buttonExport;
  public Transform buttonExit;
  public Transform saveSlots;
  public Sprite iconPerkMultiple;
  private float oriX;
  private float endX;
  private float oriY;
  private float endY;
  private float cols = 12f;
  private float rows = 7f;
  private float offsetX;
  private float offsetY;
  private Dictionary<string, PerkNodeData> perkNodeDatas;
  private Dictionary<string, PerkNode> perkNodes;
  private Dictionary<string, GameObject> perkNodesGO;
  private Dictionary<string, List<string>> perkChildIncompatible;
  private Dictionary<string, List<string>> teamPerks;
  private List<string> selectedPerks;
  private int totalAvailablePoints;
  private int availablePoints;
  private int usedPoints;
  private string subClassId = "";
  private int[] usedPointsArray;
  private int[] usedPointsCategory;
  private bool canModify;
  private int savingSlot;
  private int loadingSlot;
  public PerkSlot[] perkSlot;
  public bool IsOwner;
  public TMP_Text rankProgress;
  public TMP_Text maxProgress;
  public Transform perkBarMask;
  public SpriteRenderer perkBar;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();
  private List<Transform> _controllerVerticalList = new List<Transform>();

  public static PerkTree Instance { get; private set; }

  private void Awake()
  {
    if ((UnityEngine.Object) PerkTree.Instance == (UnityEngine.Object) null)
      PerkTree.Instance = this;
    else if ((UnityEngine.Object) PerkTree.Instance != (UnityEngine.Object) this)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    this.usedPointsArray = new int[7];
    this.usedPointsArray[0] = 0;
    this.usedPointsArray[1] = 3;
    this.usedPointsArray[2] = 6;
    this.usedPointsArray[3] = 10;
    this.usedPointsArray[4] = 16;
    this.usedPointsArray[5] = 22;
    this.usedPointsArray[6] = 30;
    this.usedPointsCategory = new int[4];
  }

  public void InitPerkTree()
  {
    this.oriX = this.posBegin.localPosition.x;
    this.oriY = this.posBegin.localPosition.y;
    this.endX = this.posEnd.localPosition.x;
    this.endY = this.posEnd.localPosition.y;
    if (this.posBegin.gameObject.activeSelf)
      this.posBegin.gameObject.SetActive(false);
    if (this.posEnd.gameObject.activeSelf)
      this.posEnd.gameObject.SetActive(false);
    this.offsetX = Mathf.Abs(this.oriX - this.endX) / (this.cols - 1f);
    this.offsetY = Mathf.Abs(this.oriY - this.endY) / (this.rows - 1f);
    for (int index = 0; index < this.perkBgRow.Length; ++index)
      this.perkBgRow[index].SetRequired(this.usedPointsArray[index]);
    this.buttonConfirm.Disable();
    if (this.buttonConfirm.gameObject.activeSelf)
      this.buttonConfirm.gameObject.SetActive(false);
    this.DrawTree();
    this.StartCoroutine(this.HideCo());
  }

  private IEnumerator HideCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    this.Hide();
  }

  public int GetPointsNeeded(int _row) => this.usedPointsArray[_row];

  public int GetPointsAvailable() => this.availablePoints;

  public string GetActiveHero() => this.subClassId;

  public void ExportTree()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(this.usedPoints);
    stringBuilder.Append("_");
    for (int index = 0; index < this.selectedPerks.Count; ++index)
    {
      stringBuilder.Append(this.selectedPerks[index]);
      if (index < this.selectedPerks.Count - 1)
        stringBuilder.Append("-");
    }
    string inputText = Functions.CompressString(stringBuilder.ToString());
    AlertManager.Instance.AlertCopyPaste(Texts.Instance.GetText("pressForCopyPaste"), inputText);
  }

  public void ImportTree()
  {
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.ImportTreeAction);
    AlertManager.Instance.AlertPasteCopy(Texts.Instance.GetText("pressForImportTree"));
  }

  public void ImportTreeAction()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.ImportTreeAction);
    if (!AlertManager.Instance.GetConfirmAnswer())
      return;
    string compressedText = Functions.OnlyAscii(AlertManager.Instance.GetInputPCValue()).Trim();
    string str = "";
    bool flag = false;
    try
    {
      str = Functions.DecompressString(compressedText);
    }
    catch
    {
      flag = true;
    }
    if (flag)
    {
      this.ErrorImport();
    }
    else
    {
      if (!(str != ""))
        return;
      string[] strArray1 = str.Split('_', StringSplitOptions.None);
      if (strArray1.Length == 2)
      {
        int _points = int.Parse(strArray1[0]);
        if (this.totalAvailablePoints < _points)
        {
          this.ErrorImport(1, _points);
        }
        else
        {
          string[] strArray2 = strArray1[1].Split('-', StringSplitOptions.None);
          this.selectedPerks = new List<string>();
          for (int index = 0; index < strArray2.Length; ++index)
            this.selectedPerks.Add(strArray2[index]);
          this.Refresh();
          this.ErrorImport(-1);
          this.buttonConfirm.Enable();
        }
      }
      else
        this.ErrorImport();
    }
  }

  private void ErrorImport(int _error = 0, int _points = 0) => this.StartCoroutine(this.ErrorImportCo(_error, _points));

  private IEnumerator ErrorImportCo(int _error, int _points)
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    switch (_error)
    {
      case -1:
        AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("importedTree"));
        break;
      case 0:
        AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("invalidImportTalentTreeCode"));
        break;
      case 1:
        AlertManager.Instance.AlertConfirm(string.Format(Texts.Instance.GetText("importPerkTreeNotPoints"), (object) _points));
        break;
    }
  }

  private void DoPerkRank()
  {
    int playerRankProgress = PlayerManager.Instance.GetPlayerRankProgress();
    int perkPrevLevelPoints = PlayerManager.Instance.GetPerkPrevLevelPoints("");
    int perkNextLevelPoints = PlayerManager.Instance.GetPerkNextLevelPoints("");
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<color=#FFF>");
    stringBuilder.Append(playerRankProgress.ToString());
    if (perkNextLevelPoints != 0)
    {
      stringBuilder.Append("</color><size=-.5> / ");
      stringBuilder.Append(perkNextLevelPoints.ToString());
    }
    this.maxProgress.text = stringBuilder.ToString();
    int highestCharacterRank = PlayerManager.Instance.GetHighestCharacterRank();
    this.rankProgress.text = string.Format(Texts.Instance.GetText("rankProgress"), (object) highestCharacterRank);
    this.perkBarMask.localScale = new Vector3((float) (((double) playerRankProgress - (double) perkPrevLevelPoints) / ((double) perkNextLevelPoints - (double) perkPrevLevelPoints) * 3.3650000095367432), this.perkBarMask.localScale.y, this.perkBarMask.localScale.z);
  }

  public void Show(string _subClassId = "", int currentHeroIndex = -1)
  {
    this.DoPerkRank();
    this.canModify = (bool) (UnityEngine.Object) HeroSelectionManager.Instance && !GameManager.Instance.IsLoadingGame() || AtOManager.Instance.CharInTown() && AtOManager.Instance.GetTownTier() == 0;
    if (!GameManager.Instance.IsMultiplayer() || (bool) (UnityEngine.Object) HeroSelectionManager.Instance && !GameManager.Instance.IsLoadingGame() || currentHeroIndex > -1 && AtOManager.Instance.GetHero(currentHeroIndex).Owner == NetworkManager.Instance.GetPlayerNick())
    {
      this.IsOwner = true;
      if (!this.buttonReset.gameObject.activeSelf)
        this.buttonReset.gameObject.SetActive(true);
    }
    else
    {
      this.IsOwner = false;
      if (this.buttonReset.gameObject.activeSelf)
        this.buttonReset.gameObject.SetActive(false);
    }
    this.selectedPerks = new List<string>();
    this.subClassId = _subClassId;
    this.charSprite.sprite = Globals.Instance.GetSubClassData(this.subClassId).SpritePortrait;
    this.totalAvailablePoints = PlayerManager.Instance.GetHighestCharacterRank();
    if (this.totalAvailablePoints > Globals.MaxPerkPoints)
      this.totalAvailablePoints = Globals.MaxPerkPoints;
    this.buttonConfirm.Disable();
    if ((bool) (UnityEngine.Object) HeroSelectionManager.Instance && !GameManager.Instance.IsLoadingGame())
    {
      if (!this.buttonReset.gameObject.activeSelf)
        this.buttonReset.gameObject.SetActive(true);
      if (!this.buttonImport.gameObject.activeSelf)
        this.buttonImport.gameObject.SetActive(true);
      if (!this.buttonExport.gameObject.activeSelf)
        this.buttonExport.gameObject.SetActive(true);
      if (!this.saveSlots.gameObject.activeSelf)
        this.saveSlots.gameObject.SetActive(true);
    }
    else
    {
      if (this.buttonReset.gameObject.activeSelf)
        this.buttonReset.gameObject.SetActive(false);
      if (this.buttonImport.gameObject.activeSelf)
        this.buttonImport.gameObject.SetActive(false);
      if (this.buttonExport.gameObject.activeSelf)
        this.buttonExport.gameObject.SetActive(false);
      if (this.saveSlots.gameObject.activeSelf)
        this.saveSlots.gameObject.SetActive(false);
    }
    if (this.buttonConfirm.gameObject.activeSelf != this.canModify)
      this.buttonConfirm.gameObject.SetActive(this.canModify);
    this.GetHeroPerks();
    this.SetCategory();
    this.LoadSavedPerks();
    this.DoTeamPerks();
    if (!this.elements.gameObject.activeSelf)
      this.elements.gameObject.SetActive(true);
    this.controllerHorizontalIndex = -1;
  }

  public void Hide()
  {
    if (this.buttonConfirm.gameObject.activeSelf && this.buttonConfirm.buttonEnabled && SceneStatic.GetSceneName() != "MainMenu")
    {
      AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.HideConfirm);
      AlertManager.Instance.AlertConfirmDouble(Texts.Instance.GetText("wantExitPerks"));
    }
    else
      this.HideAction();
  }

  public void HideConfirm()
  {
    int num = AlertManager.Instance.GetConfirmAnswer() ? 1 : 0;
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.HideConfirm);
    if (num == 0)
      return;
    this.HideAction();
  }

  public void HideAction(bool checkSubclass = true)
  {
    if (checkSubclass && this.subClassId != "")
    {
      if ((UnityEngine.Object) HeroSelectionManager.Instance != (UnityEngine.Object) null)
      {
        HeroSelectionManager.Instance.RefreshPerkPoints(this.subClassId);
      }
      else
      {
        Hero[] team = AtOManager.Instance.GetTeam();
        if (team != null)
        {
          for (int characterIndex = 0; characterIndex < team.Length; ++characterIndex)
          {
            if (team[characterIndex].HeroData.HeroSubClass.Id == this.subClassId)
            {
              AtOManager.Instance.SideBarCharacterClicked(characterIndex);
              break;
            }
          }
        }
      }
    }
    this.buttonConfirm.Disable();
    if (this.elements.gameObject.activeSelf)
      this.elements.gameObject.SetActive(false);
    PopupManager.Instance.ClosePopup();
  }

  public bool IsActive() => this.elements.gameObject.activeSelf;

  public void SetCategory(int _type = 0)
  {
    for (int index = 0; index < this.buttonType.Length; ++index)
    {
      if (_type == index)
      {
        this.buttonType[index].transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
        this.buttonType[index].transform.localPosition = new Vector3(this.buttonType[index].transform.localPosition.x, 0.05f, this.buttonType[index].transform.localPosition.z);
        this.buttonType[index].Disable();
      }
      else
      {
        this.buttonType[index].transform.localScale = new Vector3(1f, 1f, 1f);
        this.buttonType[index].transform.localPosition = new Vector3(this.buttonType[index].transform.localPosition.x, 0.0f, this.buttonType[index].transform.localPosition.z);
        this.buttonType[index].Enable();
      }
    }
    for (int index = 0; index < this.categoryT.Length; ++index)
    {
      if (_type != index)
      {
        if (this.categoryT[index].gameObject.activeSelf)
          this.categoryT[index].gameObject.SetActive(false);
      }
      else if (!this.categoryT[index].gameObject.activeSelf)
        this.categoryT[index].gameObject.SetActive(true);
    }
    this.Refresh();
  }

  private void DrawTree()
  {
    this.perkNodeDatas = new Dictionary<string, PerkNodeData>();
    this.perkNodes = new Dictionary<string, PerkNode>();
    this.perkNodesGO = new Dictionary<string, GameObject>();
    this.perkChildIncompatible = new Dictionary<string, List<string>>();
    foreach (KeyValuePair<string, PerkNodeData> keyValuePair in Globals.Instance.PerksNodesSource)
      this.perkNodeDatas.Add(keyValuePair.Value.Id, keyValuePair.Value);
    foreach (KeyValuePair<string, PerkNodeData> perkNodeData in this.perkNodeDatas)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.perkNodeGO, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, this.categoryT[perkNodeData.Value.Type]);
      gameObject.name = perkNodeData.Key;
      gameObject.transform.localPosition = new Vector3(this.oriX + this.offsetX * (float) perkNodeData.Value.Column, this.oriY - this.offsetY * (float) perkNodeData.Value.Row, 0.0f);
      this.perkNodesGO.Add(perkNodeData.Key, gameObject);
    }
    foreach (KeyValuePair<string, PerkNodeData> perkNodeData in this.perkNodeDatas)
    {
      List<string> stringList = new List<string>();
      if (perkNodeData.Value.PerksConnected.Length != 0)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.perkNodeAmplifyGO, new Vector3(0.0f, -1f, 0.0f), Quaternion.identity, this.categoryT[perkNodeData.Value.Type]);
        gameObject.name = perkNodeData.Key + "_amplify";
        gameObject.transform.localPosition = new Vector3(this.oriX + this.offsetX * (float) perkNodeData.Value.Column, (float) ((double) this.oriY - (double) this.offsetY * (double) perkNodeData.Value.Row - 1.1000000238418579), -1f);
        for (int index = 0; index < perkNodeData.Value.PerksConnected.Length; ++index)
        {
          this.perkNodesGO[perkNodeData.Value.PerksConnected[index].Id].transform.parent = gameObject.transform.GetComponent<PerkNodeAmplify>().amplifyNodes;
          this.perkNodesGO[perkNodeData.Value.PerksConnected[index].Id].transform.localPosition = new Vector3((float) index * 1.4f, 0.0f, -1f);
          this.perkNodesGO[perkNodeData.Value.PerksConnected[index].Id].transform.GetComponent<PerkNode>().SetNodeAsChild();
          stringList.Add(perkNodeData.Value.PerksConnected[index].Id);
        }
        gameObject.transform.GetComponent<PerkNodeAmplify>().SetForNodes(perkNodeData.Value.PerksConnected.Length);
      }
      for (int index1 = 0; index1 < stringList.Count; ++index1)
      {
        this.perkChildIncompatible.Add(stringList[index1], new List<string>());
        for (int index2 = 0; index2 < stringList.Count; ++index2)
        {
          if (index1 != index2)
            this.perkChildIncompatible[stringList[index1]].Add(stringList[index2]);
        }
      }
    }
    foreach (KeyValuePair<string, PerkNodeData> perkNodeData in this.perkNodeDatas)
    {
      PerkNode component = this.perkNodesGO[perkNodeData.Value.Id].GetComponent<PerkNode>();
      component.SetPND(perkNodeData.Value);
      if ((UnityEngine.Object) perkNodeData.Value.PerkRequired != (UnityEngine.Object) null && this.perkNodesGO.ContainsKey(perkNodeData.Value.PerkRequired.Id))
      {
        GameObject gameObject1 = this.perkNodesGO[perkNodeData.Value.Id];
        GameObject gameObject2 = this.perkNodesGO[perkNodeData.Value.PerkRequired.Id];
        component.SetArrow(new Vector3(0.0f, gameObject2.transform.localPosition.y - gameObject1.transform.localPosition.y, 0.0f), Vector3.zero);
      }
      this.perkNodes.Add(perkNodeData.Value.Id, component);
    }
    this.perkNodeGO.SetActive(false);
  }

  private void GetHeroPerks()
  {
    List<string> heroPerks = PlayerManager.Instance.GetHeroPerks(this.subClassId);
    if (heroPerks == null)
      return;
    for (int index = 0; index < heroPerks.Count; ++index)
      this.selectedPerks.Add(heroPerks[index]);
  }

  public void DoTeamPerks()
  {
    this.teamPerks = new Dictionary<string, List<string>>();
    if ((bool) (UnityEngine.Object) HeroSelectionManager.Instance && !GameManager.Instance.IsLoadingGame())
    {
      if (!GameManager.Instance.IsMultiplayer())
      {
        for (int _index = 0; _index < 4; ++_index)
        {
          if ((UnityEngine.Object) HeroSelectionManager.Instance.GetBoxHeroFromIndex(_index) != (UnityEngine.Object) null)
          {
            string subclassName = HeroSelectionManager.Instance.GetBoxHeroFromIndex(_index).GetSubclassName();
            if (subclassName != "" && subclassName != this.subClassId)
            {
              List<string> heroPerks = PlayerManager.Instance.GetHeroPerks(subclassName, true);
              if (heroPerks != null)
              {
                for (int index = 0; index < heroPerks.Count; ++index)
                {
                  if (!this.teamPerks.ContainsKey(heroPerks[index]))
                    this.teamPerks[heroPerks[index]] = new List<string>();
                  this.teamPerks[heroPerks[index]].Add(subclassName);
                }
              }
            }
          }
        }
      }
      else
      {
        for (int _index = 0; _index < 4; ++_index)
        {
          if ((UnityEngine.Object) HeroSelectionManager.Instance.GetBoxHeroFromIndex(_index) != (UnityEngine.Object) null)
          {
            string subclassName = HeroSelectionManager.Instance.GetBoxHeroFromIndex(_index).GetSubclassName();
            string lower = (HeroSelectionManager.Instance.GetBoxOwnerFromIndex(_index) + "_" + subclassName).ToLower();
            if (subclassName != "" && subclassName != this.subClassId && HeroSelectionManager.Instance.PlayerHeroPerksDict != null && HeroSelectionManager.Instance.PlayerHeroPerksDict.ContainsKey(lower))
            {
              List<string> stringList = HeroSelectionManager.Instance.PlayerHeroPerksDict[lower];
              if (stringList != null)
              {
                for (int index = 0; index < stringList.Count; ++index)
                {
                  if (!this.teamPerks.ContainsKey(stringList[index]))
                    this.teamPerks[stringList[index]] = new List<string>();
                  this.teamPerks[stringList[index]].Add(subclassName);
                }
              }
            }
          }
        }
      }
    }
    else
    {
      Hero[] team = AtOManager.Instance.GetTeam();
      if (team != null)
      {
        for (int index1 = 0; index1 < team.Length; ++index1)
        {
          if (team[index1] != null && !((UnityEngine.Object) team[index1].HeroData == (UnityEngine.Object) null))
          {
            if (team[index1].HeroData.HeroSubClass.Id != this.subClassId && team[index1].PerkList != null)
            {
              for (int index2 = 0; index2 < team[index1].PerkList.Count; ++index2)
              {
                if (!this.teamPerks.ContainsKey(team[index1].PerkList[index2]))
                  this.teamPerks[team[index1].PerkList[index2]] = new List<string>();
                this.teamPerks[team[index1].PerkList[index2]].Add(team[index1].HeroData.HeroSubClass.Id);
              }
            }
            team[index1].ClearCaches();
          }
        }
      }
    }
    foreach (KeyValuePair<string, PerkNode> perkNode in this.perkNodes)
    {
      if ((UnityEngine.Object) perkNode.Value.PND != (UnityEngine.Object) null)
      {
        string key = "";
        if ((UnityEngine.Object) perkNode.Value.PND.Perk != (UnityEngine.Object) null)
          key = perkNode.Value.PND.Perk.Id;
        if (key != "" && this.teamPerks.ContainsKey(key))
          perkNode.Value.TeamSelected(this.teamPerks[key].Count, this.teamPerks[key]);
        else if (perkNode.Value.PND.PerksConnected.Length != 0)
        {
          int _number = 0;
          for (int index = 0; index < perkNode.Value.PND.PerksConnected.Length; ++index)
          {
            if (this.teamPerks.ContainsKey(perkNode.Value.PND.PerksConnected[index].Perk.Id))
              _number += this.teamPerks[perkNode.Value.PND.PerksConnected[index].Perk.Id].Count;
          }
          perkNode.Value.TeamSelected(_number);
        }
        else
          perkNode.Value.TeamSelected(0);
      }
    }
  }

  private void Refresh()
  {
    this.SetUsedPerks();
    this.SetNodes();
    this.SetRows();
    this.SetButtons();
  }

  private void SetRows()
  {
    for (int index = 0; index < this.perkBgRow.Length; ++index)
    {
      if (this.usedPoints >= this.usedPointsArray[index])
        this.perkBgRow[index].ShowLockIcon(false);
      else
        this.perkBgRow[index].ShowLockIcon(true);
    }
  }

  private void SetUsedPerks()
  {
    this.usedPoints = 0;
    for (int index = 0; index < 4; ++index)
      this.usedPointsCategory[index] = 0;
    foreach (KeyValuePair<string, PerkNodeData> perkNodeData in this.perkNodeDatas)
    {
      PerkNode component = this.perkNodesGO[perkNodeData.Value.Id].GetComponent<PerkNode>();
      if (this.IsThisPerkNodeDataSelected(perkNodeData.Value))
      {
        int pointsForNode = this.GetPointsForNode(perkNodeData.Value);
        this.usedPoints += pointsForNode;
        this.usedPointsCategory[perkNodeData.Value.Type] += pointsForNode;
        component.SetSelected(true);
      }
      else
      {
        component.SetSelected(false);
        bool flag = false;
        for (int index = 0; index < perkNodeData.Value.PerksConnected.Length; ++index)
        {
          if (this.IsThisPerkNodeDataSelected(perkNodeData.Value.PerksConnected[index]))
          {
            using (Dictionary<string, PerkNode>.Enumerator enumerator = this.perkNodes.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                KeyValuePair<string, PerkNode> current = enumerator.Current;
                if (current.Value.PND.Id == perkNodeData.Value.PerksConnected[index].Id)
                {
                  component.SetValuesAsChildNode(current.Value);
                  flag = true;
                }
              }
              break;
            }
          }
        }
        if (!flag)
          component.SetDefaultIcon();
      }
    }
    this.availablePoints = this.totalAvailablePoints - this.usedPoints;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<voffset=.2><size=-.4><sprite name=perk></size></voffset>");
    stringBuilder.Append(Texts.Instance.GetText("perks"));
    if (this.IsOwner && this.canModify)
    {
      stringBuilder.Append("<br><space=-.7><b><size=+2><color=#FFF>");
      stringBuilder.Append(this.availablePoints);
      stringBuilder.Append("</color></size>/");
      stringBuilder.Append(this.totalAvailablePoints);
    }
    this.availablePerksPoints.text = stringBuilder.ToString();
    stringBuilder.Clear();
    stringBuilder.Append("<size=+.2>[ <color=#FFF>");
    stringBuilder.Append(this.usedPoints);
    stringBuilder.Append(" </color>]");
    this.usedPerksPoints.text = string.Format(Texts.Instance.GetText("usedPoints"), (object) stringBuilder.ToString());
  }

  public int GetPointsForNode(PerkNodeData _pnd) => (int) Enum.Parse(typeof (Enums.PerkCost), Enum.GetName(typeof (Enums.PerkCost), (object) _pnd.Cost));

  private bool IsThisPerkNodeDataSelected(PerkNodeData _pnd) => (UnityEngine.Object) _pnd.Perk != (UnityEngine.Object) null && this.selectedPerks.Contains(_pnd.Perk.Id.ToLower());

  private void SetButtons()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Texts.Instance.GetText("general"));
    stringBuilder.Append(" (<color=#FFF><size=+.2>");
    stringBuilder.Append(this.usedPointsCategory[0].ToString());
    stringBuilder.Append("</size></color>)");
    this.buttonType[0].SetText(stringBuilder.ToString());
    stringBuilder.Clear();
    stringBuilder.Append(Texts.Instance.GetText("physical"));
    stringBuilder.Append(" (<color=#FFF><size=+.2>");
    stringBuilder.Append(this.usedPointsCategory[1].ToString());
    stringBuilder.Append("</size></color>)");
    this.buttonType[1].SetText(stringBuilder.ToString());
    stringBuilder.Clear();
    stringBuilder.Append(Texts.Instance.GetText("elemental"));
    stringBuilder.Append(" (<color=#FFF><size=+.2>");
    stringBuilder.Append(this.usedPointsCategory[2].ToString());
    stringBuilder.Append("</size></color>)");
    this.buttonType[2].SetText(stringBuilder.ToString());
    stringBuilder.Clear();
    stringBuilder.Append(Texts.Instance.GetText("mystical"));
    stringBuilder.Append(" (<color=#FFF><size=+.2>");
    stringBuilder.Append(this.usedPointsCategory[3].ToString());
    stringBuilder.Append("</size></color>)");
    this.buttonType[3].SetText(stringBuilder.ToString());
  }

  private void SetNodes()
  {
    foreach (KeyValuePair<string, PerkNode> perkNode in this.perkNodes)
    {
      if (this.usedPoints >= this.usedPointsArray[perkNode.Value.GetRow()])
        perkNode.Value.SetLocked(false);
      else
        perkNode.Value.SetLocked(true);
      perkNode.Value.SetRequired(false);
      if (AtOManager.Instance.CharInTown() && AtOManager.Instance.GetTownTier() == 0 && perkNode.Value.PND.LockedInTown)
        perkNode.Value.SetIconLock(true);
      else
        perkNode.Value.SetIconLock();
    }
    foreach (KeyValuePair<string, PerkNode> perkNode in this.perkNodes)
    {
      if (!perkNode.Value.IsLocked() && (UnityEngine.Object) perkNode.Value.PND.PerkRequired != (UnityEngine.Object) null && !this.IsThisPerkNodeDataSelected(perkNode.Value.PND.PerkRequired))
      {
        perkNode.Value.SetLocked(true);
        perkNode.Value.SetRequired(true);
      }
    }
  }

  public void SelectPerk(string _perkId, PerkNode _perkNode)
  {
    if ((!AtOManager.Instance.CharInTown() || AtOManager.Instance.GetTownTier() != 0 || _perkNode.PND.LockedInTown) && (!(bool) (UnityEngine.Object) HeroSelectionManager.Instance || GameManager.Instance.IsLoadingGame()))
      return;
    bool flag1 = false;
    _perkId = _perkId.ToLower();
    if (!this.selectedPerks.Contains(_perkId))
    {
      bool flag2 = false;
      if (_perkNode.IsChildNode() && this.perkChildIncompatible.ContainsKey(_perkNode.PND.Id))
      {
        for (int index = 0; index < this.perkChildIncompatible[_perkNode.PND.Id].Count; ++index)
        {
          string key = this.perkChildIncompatible[_perkNode.PND.Id][index];
          if (key != "")
          {
            string lower = this.perkNodes[key].PND.Perk.Id.ToLower();
            if (this.selectedPerks.Contains(lower))
            {
              this.selectedPerks.Remove(lower);
              this.selectedPerks.Add(_perkId);
              flag2 = true;
              flag1 = true;
              break;
            }
          }
        }
      }
      if (!flag2 && this.availablePoints >= this.GetPointsForNode(_perkNode.PND))
      {
        this.selectedPerks.Add(_perkId);
        flag1 = true;
      }
    }
    else
    {
      PerkNodeData pnd = _perkNode.PND;
      foreach (KeyValuePair<string, PerkNodeData> perkNodeData in this.perkNodeDatas)
      {
        if ((UnityEngine.Object) perkNodeData.Value.PerkRequired != (UnityEngine.Object) null && perkNodeData.Value.PerkRequired.Id == pnd.Id && this.IsThisPerkNodeDataSelected(perkNodeData.Value))
          return;
      }
      int num1 = 0;
      foreach (KeyValuePair<string, PerkNode> perkNode in this.perkNodes)
      {
        if (perkNode.Value.PND.Id != pnd.Id && perkNode.Value.IsSelected() && perkNode.Value.GetRow() <= _perkNode.GetRow())
          num1 += this.GetPointsForNode(_perkNode.PND);
      }
      foreach (KeyValuePair<string, PerkNode> perkNode1 in this.perkNodes)
      {
        if (perkNode1.Value.IsSelected() && perkNode1.Value.PND.Id != pnd.Id)
        {
          int row = perkNode1.Value.GetRow();
          if (row > _perkNode.GetRow())
          {
            int num2 = 0;
            foreach (KeyValuePair<string, PerkNode> perkNode2 in this.perkNodes)
            {
              if (perkNode2.Value.PND.Id != pnd.Id && perkNode2.Value.PND.Id != perkNode1.Value.PND.Id && perkNode2.Value.IsSelected() && perkNode2.Value.GetRow() < row)
                num2 += this.GetPointsForNode(perkNode2.Value.PND);
            }
            if (this.usedPointsArray[row] > num2)
              return;
          }
        }
      }
      this.selectedPerks.Remove(_perkId);
      flag1 = true;
    }
    if (flag1)
      this.buttonConfirm.Enable();
    this.Refresh();
    if (this.availablePoints >= 0)
      return;
    this.buttonConfirm.Disable();
  }

  public void PerksAssignConfirm()
  {
    if ((bool) (UnityEngine.Object) HeroSelectionManager.Instance)
    {
      PlayerManager.Instance.AssignPerkList(this.subClassId, this.selectedPerks);
      if (!GameManager.Instance.IsMultiplayer())
        this.DoTeamPerks();
    }
    else
    {
      Hero[] team = AtOManager.Instance.GetTeam();
      for (int _heroIndex = 0; _heroIndex < team.Length; ++_heroIndex)
      {
        if (team[_heroIndex].HeroData.HeroSubClass.Id == this.subClassId)
        {
          AtOManager.Instance.AddPerkToHeroGlobalList(_heroIndex, this.selectedPerks);
          if (!GameManager.Instance.IsMultiplayer())
          {
            this.DoTeamPerks();
            break;
          }
          break;
        }
      }
    }
    this.buttonConfirm.Disable();
  }

  public void PerksReset()
  {
    if (this.selectedPerks.Count <= 0)
      return;
    this.selectedPerks = new List<string>();
    this.buttonConfirm.Enable();
    this.Refresh();
  }

  public Dictionary<string, PerkNodeData> GetPerkNodeDatas() => this.perkNodeDatas;

  public bool CanModify() => this.canModify;

  public void SavePerkSlot(int slot)
  {
    this.savingSlot = slot;
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.SavePerkSlotAction);
    AlertManager.Instance.AlertInput(Texts.Instance.GetText("inputConfigSaveName"), Texts.Instance.GetText("accept").ToUpper());
  }

  public void SavePerkSlotAction()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.SavePerkSlotAction);
    string str = Functions.OnlyAscii(AlertManager.Instance.GetInputValue()).Trim();
    if (str == "")
      return;
    if (!PlayerManager.Instance.PlayerSavedPerk.PerkConfigTitle.ContainsKey(this.subClassId))
      PlayerManager.Instance.PlayerSavedPerk.PerkConfigTitle.Add(this.subClassId, new string[10]);
    if (!PlayerManager.Instance.PlayerSavedPerk.PerkConfigPerks.ContainsKey(this.subClassId))
      PlayerManager.Instance.PlayerSavedPerk.PerkConfigPerks.Add(this.subClassId, new List<string>[10]);
    if (!PlayerManager.Instance.PlayerSavedPerk.PerkConfigPoints.ContainsKey(this.subClassId))
      PlayerManager.Instance.PlayerSavedPerk.PerkConfigPoints.Add(this.subClassId, new int[10]);
    List<string> stringList = new List<string>();
    for (int index = 0; index < this.selectedPerks.Count; ++index)
      stringList.Add(this.selectedPerks[index]);
    PlayerManager.Instance.PlayerSavedPerk.PerkConfigTitle[this.subClassId][this.savingSlot] = str;
    PlayerManager.Instance.PlayerSavedPerk.PerkConfigPerks[this.subClassId][this.savingSlot] = stringList;
    PlayerManager.Instance.PlayerSavedPerk.PerkConfigPoints[this.subClassId][this.savingSlot] = this.usedPoints;
    SaveManager.SavePlayerPerkConfig();
    this.LoadSavedPerks();
  }

  public void RemovePerkSlot(int slot)
  {
    this.savingSlot = slot;
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.RemovePerkSlotAction);
    AlertManager.Instance.AlertConfirmDouble(Texts.Instance.GetText("savedConfigDeleteConfirm"));
  }

  public void RemovePerkSlotAction()
  {
    int num = AlertManager.Instance.GetConfirmAnswer() ? 1 : 0;
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.RemovePerkSlotAction);
    if (num == 0)
      return;
    PlayerManager.Instance.PlayerSavedPerk.PerkConfigTitle[this.subClassId][this.savingSlot] = "";
    PlayerManager.Instance.PlayerSavedPerk.PerkConfigPerks[this.subClassId][this.savingSlot] = new List<string>();
    PlayerManager.Instance.PlayerSavedPerk.PerkConfigPoints[this.subClassId][this.savingSlot] = 0;
    SaveManager.SavePlayerPerkConfig();
    this.LoadSavedPerks();
  }

  private void LoadSavedPerks()
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < this.perkSlot.Length; ++index)
    {
      if ((UnityEngine.Object) this.perkSlot[index] != (UnityEngine.Object) null)
      {
        stringBuilder.Clear();
        if (PlayerManager.Instance.PlayerSavedPerk.PerkConfigTitle.ContainsKey(this.subClassId) && PlayerManager.Instance.PlayerSavedPerk.PerkConfigTitle[this.subClassId][index] != null && PlayerManager.Instance.PlayerSavedPerk.PerkConfigTitle[this.subClassId][index] != "")
        {
          stringBuilder.Append(PlayerManager.Instance.PlayerSavedPerk.PerkConfigTitle[this.subClassId][index]);
          this.perkSlot[index].SetActive(stringBuilder.ToString(), PlayerManager.Instance.PlayerSavedPerk.PerkConfigPoints[this.subClassId][index].ToString());
        }
        else
          this.perkSlot[index].SetEmpty(true);
        if ((bool) (UnityEngine.Object) HeroSelectionManager.Instance)
          this.perkSlot[index].SetDisable(false);
        else
          this.perkSlot[index].SetDisable(true);
      }
    }
  }

  public void LoadPerkConfig(int slot)
  {
    this.loadingSlot = slot;
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.LoadPerkConfigAction);
    AlertManager.Instance.AlertConfirmDouble(Texts.Instance.GetText("wantOverwritePerks"));
  }

  public void LoadPerkConfigAction()
  {
    int num = AlertManager.Instance.GetConfirmAnswer() ? 1 : 0;
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.LoadPerkConfigAction);
    if (num == 0)
      return;
    this.selectedPerks = new List<string>();
    for (int index = 0; index < PlayerManager.Instance.PlayerSavedPerk.PerkConfigPerks[this.subClassId][this.loadingSlot].Count; ++index)
      this.selectedPerks.Add(PlayerManager.Instance.PlayerSavedPerk.PerkConfigPerks[this.subClassId][this.loadingSlot][index]);
    this.Refresh();
    if (this.availablePoints < 0)
      this.buttonConfirm.Disable();
    else
      this.buttonConfirm.Enable();
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    int absolutePosition = -1)
  {
    this._controllerList.Clear();
    for (int index = 0; index < this.buttonType.Length; ++index)
      this._controllerList.Add(this.buttonType[index].transform);
    foreach (KeyValuePair<string, GameObject> keyValuePair in this.perkNodesGO)
    {
      if (Functions.TransformIsVisible(keyValuePair.Value.transform))
        this._controllerList.Add(keyValuePair.Value.transform);
    }
    if (Functions.TransformIsVisible(this.buttonConfirm.transform))
      this._controllerList.Add(this.buttonConfirm.transform);
    if (Functions.TransformIsVisible(this.buttonReset.transform))
      this._controllerList.Add(this.buttonReset.transform);
    if (Functions.TransformIsVisible(this.buttonExport.transform))
    {
      this._controllerList.Add(this.buttonExport.transform);
      this._controllerList.Add(this.buttonImport.transform);
    }
    this._controllerList.Add(this.buttonExit.transform);
    if (Functions.TransformIsVisible(this.perkSlot[0].transform))
    {
      for (int index = 0; index < this.perkSlot.Length; ++index)
      {
        if (this.perkSlot[index].GetComponent<BoxCollider2D>().enabled)
        {
          this._controllerList.Add(this.perkSlot[index].transform);
          if (Functions.TransformIsVisible(this.perkSlot[index].transform.GetChild(5).transform))
            this._controllerList.Add(this.perkSlot[index].transform.GetChild(5).transform);
        }
        else if (Functions.TransformIsVisible(this.perkSlot[index].transform.GetChild(4).transform))
          this._controllerList.Add(this.perkSlot[index].transform.GetChild(4).transform);
      }
    }
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }
}
