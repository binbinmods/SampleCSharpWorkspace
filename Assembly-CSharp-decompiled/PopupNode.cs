// Decompiled with JetBrains decompiler
// Type: PopupNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class PopupNode : MonoBehaviour
{
  public Transform Element1;
  public Transform Element2;
  public Transform Ground;
  public TMP_Text GroundText;
  public Transform Bg0;
  public Transform Bg1;
  public Transform Bg2;
  public TMP_Text Title;
  public SpriteRenderer Icon;
  public SpriteRenderer[] CharIcon;
  public TMP_Text Element1Title;
  public TMP_Text Element2Title;
  public Transform monsterSpriteFrontChampion;
  public SpriteRenderer monsterSpriteFrontChampionIcoBack;
  public Transform monsterSpriteBackChampion;
  public SpriteRenderer monsterSpriteBackChampionIcoBack;
  private bool show;
  private bool popupDone;
  private int elementsNum;
  private float popW = 1.5f;
  private Vector3 popDestination;

  private void Start()
  {
  }

  private void Update()
  {
    if (!this.show)
      return;
    this.popDestination = (double) this.transform.localPosition.x - (double) this.popW >= (double) Globals.Instance.sizeW * -0.5 ? ((double) this.transform.localPosition.x + (double) this.popW <= (double) Globals.Instance.sizeW * 0.5 ? this.CalcDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition)) : new Vector3(Globals.Instance.sizeW * 0.5f - this.popW, this.transform.localPosition.y, this.transform.localPosition.z)) : new Vector3(Globals.Instance.sizeW * -0.5f + this.popW, this.transform.localPosition.y, this.transform.localPosition.z);
    if ((double) this.popDestination.y > (double) Globals.Instance.sizeH * 0.5)
      this.popDestination = new Vector3(this.popDestination.x, Globals.Instance.sizeH * 0.5f, this.popDestination.z);
    this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, this.popDestination, 0.06f);
  }

  private Vector3 CalcDestination(Vector3 ori)
  {
    float num = this.elementsNum != 0 ? (this.elementsNum != 1 ? 1.5f : 1.25f) : 1f;
    return new Vector3(ori.x + 0.2f, (float) ((double) ori.y + (double) num + 0.30000001192092896), 0.0f);
  }

  private void ShowHideElement(int num, bool state)
  {
    switch (num)
    {
      case 0:
        this.Bg0.gameObject.SetActive(state);
        break;
      case 1:
        this.Bg1.gameObject.SetActive(state);
        this.Element1.gameObject.SetActive(state);
        break;
      case 2:
        this.Bg2.gameObject.SetActive(state);
        this.Element2.gameObject.SetActive(state);
        break;
    }
  }

  private void WriteTitle(int num, string text, Enums.MapIconShader shader = Enums.MapIconShader.None, Sprite spriteMap = null)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(text);
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      string str = "";
      if ((UnityEngine.Object) spriteMap != (UnityEngine.Object) null)
        str = spriteMap.name.ToLower();
      if (shader != Enums.MapIconShader.None || !((UnityEngine.Object) spriteMap == (UnityEngine.Object) null))
      {
        stringBuilder.Append("\n");
        stringBuilder.Append("<size=-.2><color=#");
        switch (str)
        {
          case "nodeiconeventgreen":
            stringBuilder.Append(ColorUtility.ToHtmlStringRGBA(Globals.Instance.RarityColor["uncommon"]));
            stringBuilder.Append(">");
            stringBuilder.Append(Texts.Instance.GetText("eventUncommon"));
            break;
          case "nodeiconeventblue":
            stringBuilder.Append(ColorUtility.ToHtmlStringRGBA(Globals.Instance.RarityColor["rare"]));
            stringBuilder.Append(">");
            stringBuilder.Append(Texts.Instance.GetText("eventRare"));
            break;
          case "nodeiconeventpurple":
            stringBuilder.Append(ColorUtility.ToHtmlStringRGBA(Globals.Instance.ColorColor["purple"]));
            stringBuilder.Append(">");
            stringBuilder.Append(Texts.Instance.GetText("eventEpic"));
            break;
          case "nodeiconmap":
            stringBuilder.Append(ColorUtility.ToHtmlStringRGBA(Globals.Instance.RarityColor["mythic"]));
            stringBuilder.Append(">");
            stringBuilder.Append(Texts.Instance.GetText("mapLegendMapTransition"));
            break;
          default:
            if (shader == Enums.MapIconShader.Orange)
            {
              stringBuilder.Append(ColorUtility.ToHtmlStringRGBA(Globals.Instance.RarityColor["mythic"]));
              stringBuilder.Append(">");
              stringBuilder.Append(Texts.Instance.GetText("eventCharacter"));
              break;
            }
            if (str == "nodeiconeventteal")
            {
              stringBuilder.Append("AAAAAA>");
              stringBuilder.Append(Texts.Instance.GetText("eventCommon"));
              break;
            }
            stringBuilder.Append("FFFFFF>");
            break;
        }
        stringBuilder.Append("</color>");
      }
    }
    if (num == 0)
      this.Title.text = stringBuilder.ToString();
    else if (num == 1)
      this.Element1Title.text = stringBuilder.ToString();
    else
      this.Element2Title.text = stringBuilder.ToString();
  }

  private void DoPopup(Node _node)
  {
    this.monsterSpriteFrontChampion.gameObject.SetActive(false);
    this.monsterSpriteBackChampion.gameObject.SetActive(false);
    Enums.MapIconShader shader = Enums.MapIconShader.None;
    Sprite spriteMap = (Sprite) null;
    if (!this.popupDone)
    {
      if ((UnityEngine.Object) _node.nodeData == (UnityEngine.Object) null)
      {
        this.Hide();
        return;
      }
      if (_node.nodeData.NodeName == "")
      {
        this.Hide();
        return;
      }
      string text1 = !GameManager.Instance.IsObeliskChallenge() ? Globals.Instance.GetNodeData(_node.nodeData.NodeId).NodeName : Texts.Instance.GetText("ObeliskChallenge");
      if (text1 == "")
        text1 = _node.nodeData.NodeName;
      this.WriteTitle(0, text1);
      this.Icon.gameObject.SetActive(true);
      this.Icon.sprite = _node.nodeImage.sprite;
      string text2 = "";
      bool flag1 = false;
      if (PlayerManager.Instance.IsNodeUnlocked(_node.GetNodeAssignedId()) || PlayerManager.Instance.IsNodeUnlocked(_node.nodeData.NodeId))
        flag1 = true;
      bool flag2 = false;
      if (_node.GetNodeAction() == "combat" && _node.nodeData.NodeCombatTier != Enums.CombatTier.T0 && (MadnessManager.Instance.IsMadnessTraitActive("randomcombats") || GameManager.Instance.IsObeliskChallenge()))
        flag2 = true;
      if ((UnityEngine.Object) _node.nodeData != (UnityEngine.Object) null && _node.nodeData.DisableRandom)
        flag2 = false;
      if (flag2)
      {
        string nodeId = _node.nodeData.NodeId;
        string nodeAssignedId = _node.GetNodeAssignedId();
        NodeData nodeData = Globals.Instance.GetNodeData(nodeId);
        string str = "";
        CombatData combatData = Globals.Instance.GetCombatData(nodeAssignedId);
        if ((UnityEngine.Object) combatData != (UnityEngine.Object) null)
          str = combatData.CombatId;
        int deterministicHashCode = (nodeId + AtOManager.Instance.GetGameId() + str).GetDeterministicHashCode();
        NPCData[] randomCombat = Functions.GetRandomCombat(nodeData.NodeCombatTier, deterministicHashCode, nodeId);
        if (randomCombat != null)
        {
          this.elementsNum = 2;
          this.ShowHideElement(1, false);
          this.ShowHideElement(2, true);
          float num1 = -1.02f;
          float num2 = 0.54f;
          int num3 = 0;
          int num4 = 0;
          for (int index = 0; index < randomCombat.Length; ++index)
          {
            if ((UnityEngine.Object) randomCombat[index] != (UnityEngine.Object) null)
              ++num4;
          }
          if (num4 == 3)
            num1 = -0.75f;
          for (int index = 0; index < 4; ++index)
          {
            if (index < randomCombat.Length && (UnityEngine.Object) randomCombat[index] != (UnityEngine.Object) null)
            {
              this.CharIcon[index].sprite = randomCombat[index].SpriteSpeed;
              this.CharIcon[index].gameObject.SetActive(true);
              this.CharIcon[index].transform.localPosition = new Vector3(num1 + num2 * (float) num3, this.CharIcon[index].transform.localPosition.y, this.CharIcon[index].transform.localPosition.z);
              ++num3;
            }
            else
              this.CharIcon[index].gameObject.SetActive(false);
          }
          if ((UnityEngine.Object) randomCombat[0] != (UnityEngine.Object) null && randomCombat[0].IsNamed)
          {
            AuraCurseData auraCurseData = Globals.Instance.GetAuraCurseData(Functions.GetAuraCurseImmune(randomCombat[0], nodeId));
            if ((UnityEngine.Object) auraCurseData != (UnityEngine.Object) null)
            {
              this.monsterSpriteFrontChampion.gameObject.SetActive(true);
              this.monsterSpriteFrontChampion.GetComponent<SpriteRenderer>().sprite = auraCurseData.Sprite;
              this.monsterSpriteFrontChampionIcoBack.sprite = auraCurseData.Sprite;
            }
          }
          if ((UnityEngine.Object) randomCombat[3] != (UnityEngine.Object) null && randomCombat[3].IsNamed)
          {
            AuraCurseData auraCurseData = Globals.Instance.GetAuraCurseData(Functions.GetAuraCurseImmune(randomCombat[3], nodeId));
            if ((UnityEngine.Object) auraCurseData != (UnityEngine.Object) null)
            {
              this.monsterSpriteBackChampion.gameObject.SetActive(true);
              this.monsterSpriteBackChampion.GetComponent<SpriteRenderer>().sprite = auraCurseData.Sprite;
              this.monsterSpriteBackChampionIcoBack.sprite = auraCurseData.Sprite;
            }
          }
          if (AtOManager.Instance.Sandbox_lessNPCs != 0)
          {
            SortedDictionary<int, int> source = new SortedDictionary<int, int>();
            for (int index = 0; index < randomCombat.Length; ++index)
            {
              if ((UnityEngine.Object) randomCombat[index] != (UnityEngine.Object) null && !randomCombat[index].IsNamed && !randomCombat[index].IsBoss)
                source.Add(randomCombat[index].Hp * 10000 + index, index);
            }
            int message = AtOManager.Instance.Sandbox_lessNPCs;
            if (message >= num4)
              message = num4 - 1;
            if (message > source.Count)
              message = source.Count;
            Debug.Log((object) message);
            for (int index1 = 0; index1 < message; ++index1)
            {
              KeyValuePair<int, int> keyValuePair = source.ElementAt<KeyValuePair<int, int>>(index1);
              Debug.Log((object) keyValuePair.Value);
              SpriteRenderer[] charIcon1 = this.CharIcon;
              keyValuePair = source.ElementAt<KeyValuePair<int, int>>(index1);
              int index2 = keyValuePair.Value;
              Debug.Log((object) charIcon1[index2]);
              SpriteRenderer[] charIcon2 = this.CharIcon;
              keyValuePair = source.ElementAt<KeyValuePair<int, int>>(index1);
              int index3 = keyValuePair.Value;
              charIcon2[index3].gameObject.SetActive(false);
            }
          }
        }
      }
      else if (!GameManager.Instance.IsObeliskChallenge() && !flag1 || GameManager.Instance.IsObeliskChallenge() && !AtOManager.Instance.mapVisitedNodes.Contains(_node.nodeData.NodeId))
      {
        if ((UnityEngine.Object) _node != (UnityEngine.Object) null)
        {
          EventData eventData = Globals.Instance.GetEventData(_node.GetNodeAssignedId());
          if ((UnityEngine.Object) eventData != (UnityEngine.Object) null)
          {
            shader = eventData.EventIconShader;
            spriteMap = eventData.EventSpriteMap;
          }
        }
        text2 = "?????";
        this.elementsNum = 1;
        this.ShowHideElement(1, true);
        this.ShowHideElement(2, false);
      }
      else if (_node.GetNodeAction() == "combat")
      {
        this.elementsNum = 2;
        this.ShowHideElement(1, false);
        this.ShowHideElement(2, true);
        CombatData combatData = Globals.Instance.GetCombatData(_node.GetNodeAssignedId());
        float num5 = -1.02f;
        float num6 = 0.54f;
        int num7 = 0;
        int num8 = 0;
        for (int index = 0; index < combatData.NPCList.Length; ++index)
        {
          if ((UnityEngine.Object) combatData.NPCList[index] != (UnityEngine.Object) null)
            ++num8;
        }
        if (AtOManager.Instance.GetMadnessDifficulty() == 0 && combatData.NpcRemoveInMadness0Index > -1 && AtOManager.Instance.GetActNumberForText() < 3)
          --num8;
        if (num8 == 3)
          num5 = -0.75f;
        for (int index = 0; index < 4; ++index)
          this.CharIcon[index].gameObject.SetActive(false);
        for (int index = 0; index < combatData.NPCList.Length; ++index)
        {
          if ((AtOManager.Instance.GetMadnessDifficulty() != 0 || combatData.NpcRemoveInMadness0Index != index || AtOManager.Instance.GetActNumberForText() >= 3) && (UnityEngine.Object) combatData.NPCList[index] != (UnityEngine.Object) null)
          {
            this.CharIcon[index].sprite = combatData.NPCList[index].SpriteSpeed;
            this.CharIcon[index].gameObject.SetActive(true);
            this.CharIcon[index].transform.localPosition = new Vector3(num5 + num6 * (float) num7, this.CharIcon[index].transform.localPosition.y, this.CharIcon[index].transform.localPosition.z);
            ++num7;
          }
        }
        if (AtOManager.Instance.Sandbox_lessNPCs != 0)
        {
          SortedDictionary<int, int> source = new SortedDictionary<int, int>();
          for (int index = 0; index < combatData.NPCList.Length; ++index)
          {
            if ((UnityEngine.Object) combatData.NPCList[index] != (UnityEngine.Object) null && !combatData.NPCList[index].IsNamed && !combatData.NPCList[index].IsBoss)
              source.Add(combatData.NPCList[index].Hp * 10000 + index, index);
          }
          int num9 = AtOManager.Instance.Sandbox_lessNPCs;
          if (num9 >= num7)
            num9 = num7 - 1;
          if (num9 > source.Count)
            num9 = source.Count;
          for (int index = 0; index < num9; ++index)
            this.CharIcon[source.ElementAt<KeyValuePair<int, int>>(index).Value].gameObject.SetActive(false);
        }
      }
      else
      {
        string nodeAssignedId = _node.GetNodeAssignedId();
        if (!(nodeAssignedId != ""))
          return;
        if (nodeAssignedId != "town" && nodeAssignedId != "destination")
        {
          EventData eventData = Globals.Instance.GetEventData(nodeAssignedId);
          string text3 = Texts.Instance.GetText(eventData.EventId + "_nm", "events");
          text2 = !(text3 != "") ? eventData.EventName : text3;
          shader = eventData.EventIconShader;
          spriteMap = eventData.EventSpriteMap;
          this.elementsNum = 1;
          this.ShowHideElement(1, true);
          this.ShowHideElement(2, false);
        }
        else
        {
          string text4 = Texts.Instance.GetText(AtOManager.Instance.GetTownZoneId());
          text2 = !(text4 != "") ? AtOManager.Instance.GetTownZoneId() : text4;
          this.Icon.gameObject.SetActive(false);
          this.elementsNum = 0;
          this.ShowHideElement(1, true);
          this.ShowHideElement(2, false);
        }
      }
      if (_node.nodeData.NodeGround != Enums.NodeGround.None)
      {
        if (!this.Ground.gameObject.activeSelf)
          this.Ground.gameObject.SetActive(true);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(Functions.GetNodeGroundSprite(_node.nodeData.NodeGround));
        if (stringBuilder.ToString() != "")
          stringBuilder.Append(Texts.Instance.GetText(Enum.GetName(typeof (Enums.NodeGround), (object) _node.nodeData.NodeGround)));
        this.GroundText.text = stringBuilder.ToString();
        this.Ground.localPosition = !(_node.GetNodeAction() == "combat") ? new Vector3(this.Ground.localPosition.x, -1.02f, this.Ground.localPosition.z) : new Vector3(this.Ground.localPosition.x, -1.32f, this.Ground.localPosition.z);
      }
      else if (this.Ground.gameObject.activeSelf)
        this.Ground.gameObject.SetActive(false);
      this.WriteTitle(1, text2, shader, spriteMap);
    }
    this.popupDone = true;
    this.ShowAction();
  }

  public void Show(Node _node) => this.DoPopup(_node);

  private void ShowAction()
  {
    this.gameObject.SetActive(true);
    this.show = true;
    this.transform.localPosition = this.CalcDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
  }

  public void Hide()
  {
    this.show = false;
    this.popupDone = false;
    this.gameObject.SetActive(false);
  }
}
