// Decompiled with JetBrains decompiler
// Type: TomeRunDetails
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TomeRunDetails : MonoBehaviour
{
  public PlayerRun playerRun;
  private int runIndex;
  private List<string> characterCards;
  public int[,] combatStats;
  public Transform cardListContainer;
  public GameObject cardVerticalPrefab;
  public TomeButton[] tomeButtons;
  public Transform[] characters;
  public Transform[] charactersButtons;
  private SpriteRenderer[] charactersSpr;
  private TMP_Text[] charactersPlayerName;
  public TMP_Text type;
  public TMP_Text typeRight;
  public TMP_Text description;
  public TMP_Text date;
  public TMP_Text score;
  public TMP_Text cards;
  public TMP_Text characterActiveName;
  public Transform closeButton;
  public Transform madness;
  public TMP_Text madnessText;
  public SpriteRenderer characterActiveSpr;
  public ItemCombatIcon iconWeapon;
  public ItemCombatIcon iconArmor;
  public ItemCombatIcon iconAccesory;
  public ItemCombatIcon iconJewelry;
  public ItemCombatIcon iconPet;
  public TraitRollOver[] traits;
  public Transform characterBlock;
  public Transform travelBlock;
  public Transform travelGroups;
  public TMP_Text timePlayed;
  private List<string> characterItems;
  private List<string> characterTraits;
  public GameObject travelPlaceGO;
  public GameObject travelPlaceTitleGO;
  public Transform adventureTitle;
  public Transform pathNext;
  public Transform pathPrev;
  private List<Transform> pathGroupsList;
  public TMP_Text pathPaginator;
  public Transform bossesBlock;
  public TMP_Text bossesDescription;
  public Transform[] bosses;
  private SpriteRenderer[] bossesSpr;
  private TMP_Text[] bossesName;
  private int activeCharacter = -1;
  private int pathIndex;
  private int pathIndexMax;
  private float pathColumnDistance = 3.4f;

  private void Awake()
  {
    if (this.characters.Length != 0)
    {
      this.charactersSpr = new SpriteRenderer[this.characters.Length];
      this.charactersPlayerName = new TMP_Text[this.characters.Length];
      for (int index = 0; index < this.characters.Length; ++index)
      {
        this.charactersSpr[index] = this.characters[index].GetComponent<SpriteRenderer>();
        this.charactersPlayerName[index] = this.characters[index].GetChild(0).GetComponent<TMP_Text>();
      }
    }
    if (this.bosses.Length == 0)
      return;
    this.bossesSpr = new SpriteRenderer[this.bosses.Length];
    this.bossesName = new TMP_Text[this.bosses.Length];
    for (int index = 0; index < this.bosses.Length; ++index)
    {
      this.bossesSpr[index] = this.bosses[index].GetComponent<SpriteRenderer>();
      this.bossesName[index] = this.bosses[index].GetChild(0).GetComponent<TMP_Text>();
    }
  }

  public void SetRun(int _index)
  {
    this.runIndex = _index;
    this.characterBlock.gameObject.SetActive(false);
    if (_index <= -1)
      return;
    this.travelBlock.gameObject.SetActive(true);
    this.travelBlock.gameObject.SetActive(false);
    this.characterBlock.gameObject.SetActive(true);
    this.characterBlock.gameObject.SetActive(false);
    this.playerRun = TomeManager.Instance.playerRunsList[_index];
    if (this.playerRun.CombatStats0 != null)
      TomeManager.Instance.TomeButtons[21].gameObject.SetActive(true);
    else
      TomeManager.Instance.TomeButtons[21].gameObject.SetActive(false);
    this.DoPortraits();
    this.SetPaths();
    this.SetDescription();
    this.DoBosses();
  }

  private void DoBosses()
  {
    if (this.playerRun.BossesKilledName.Count < 1)
    {
      this.bossesBlock.gameObject.SetActive(false);
    }
    else
    {
      this.bossesBlock.gameObject.SetActive(true);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Texts.Instance.GetText("bossesKilledTome"));
      stringBuilder.Append(" <color=#303030>");
      stringBuilder.Append(this.playerRun.BossesKilledName.Count);
      stringBuilder.Append("</color>");
      this.bossesDescription.text = stringBuilder.ToString();
      int num = this.bosses.Length;
      if (this.playerRun.BossesKilledName.Count < num)
        num = this.playerRun.BossesKilledName.Count;
      for (int index = 0; index < num; ++index)
      {
        NPCData npc = Globals.Instance.GetNPC(this.playerRun.BossesKilledName[index]);
        this.bosses[index].gameObject.SetActive(true);
        this.bossesSpr[index].sprite = npc.SpriteSpeed;
        this.bossesName[index].text = npc.NPCName;
      }
      for (int index = num; index < this.bosses.Length; ++index)
        this.bosses[index].gameObject.SetActive(false);
    }
  }

  private void SetDescription()
  {
    StringBuilder stringBuilder = new StringBuilder();
    this.madness.gameObject.SetActive(false);
    if (this.playerRun.TotalPlayers > 1)
    {
      stringBuilder.Append("<size=2><color=#24646D>");
      stringBuilder.Append(Texts.Instance.GetText("menuMultiplayer"));
      stringBuilder.Append("</color></size>\n");
    }
    if (!this.playerRun.ObeliskChallenge)
      stringBuilder.Append(Texts.Instance.GetText("adventure"));
    else if (this.playerRun.WeeklyChallenge)
      stringBuilder.Append(Texts.Instance.GetText("menuWeekly"));
    else
      stringBuilder.Append(Texts.Instance.GetText("menuChallenge"));
    this.type.text = stringBuilder.ToString();
    stringBuilder.Clear();
    if (this.playerRun.WeeklyChallenge)
    {
      stringBuilder.Append(string.Format(Texts.Instance.GetText("weekNumber"), (object) this.playerRun.WeekChallenge.ToString()));
    }
    else
    {
      stringBuilder.Append("<size=2><color=#24646D>");
      stringBuilder.Append(Texts.Instance.GetText("gameSeed"));
      stringBuilder.Append("</color></size>\n");
      stringBuilder.Append(this.playerRun.GameSeed);
    }
    this.typeRight.text = stringBuilder.ToString();
    stringBuilder.Clear();
    if (!this.playerRun.ObeliskChallenge)
    {
      int madnessTotal = MadnessManager.Instance.CalculateMadnessTotal(this.playerRun.MadnessLevel, this.playerRun.MadnessCorruptors);
      if (madnessTotal > 0)
      {
        this.madness.gameObject.SetActive(true);
        this.madnessText.text = madnessTotal.ToString();
      }
    }
    else if (!this.playerRun.WeeklyChallenge && this.playerRun.ObeliskMadness > 0)
    {
      int obeliskMadness = this.playerRun.ObeliskMadness;
      this.madness.gameObject.SetActive(true);
      this.madnessText.text = obeliskMadness.ToString();
    }
    this.date.text = Convert.ToDateTime(this.playerRun.gameDate).ToString("d");
    stringBuilder.Append(Functions.ScoreFormat(this.playerRun.FinalScore));
    stringBuilder.Append("  <voffset=.3><size=-.8><sprite name=experience></size></voffset>");
    this.score.text = stringBuilder.ToString();
    stringBuilder.Clear();
    int num = this.playerRun.CommonCorruptions + this.playerRun.UncommonCorruptions + this.playerRun.RareCorruptions + this.playerRun.EpicCorruptions;
    stringBuilder.Append(Texts.Instance.GetText("corruptionsCompleted"));
    stringBuilder.Append(" <color=#222>");
    stringBuilder.Append(num);
    stringBuilder.Append("</color>        ");
    if (num > 0)
    {
      string str = "<space=1.1><voffset=.3><size=-.5><color=#333>|</color></size></voffset><space=1.1>";
      stringBuilder.Append("<sprite name=rarityCommon><space=.6>");
      stringBuilder.Append(this.playerRun.CommonCorruptions);
      stringBuilder.Append(str);
      stringBuilder.Append("<sprite name=rarityUncommon><space=.6>");
      stringBuilder.Append(this.playerRun.UncommonCorruptions);
      stringBuilder.Append(str);
      stringBuilder.Append("<sprite name=rarityRare><space=.6>");
      stringBuilder.Append(this.playerRun.RareCorruptions);
      stringBuilder.Append(str);
      stringBuilder.Append("<sprite name=rarityEpic><space=.6>");
      stringBuilder.Append(this.playerRun.EpicCorruptions);
    }
    stringBuilder.Append("\n");
    stringBuilder.Append(Texts.Instance.GetText("placesVisited"));
    stringBuilder.Append(": <color=#222>");
    stringBuilder.Append(this.playerRun.VisitedNodes.Count);
    stringBuilder.Append("</color>\n");
    stringBuilder.Append(Texts.Instance.GetText("monstersKilledTome"));
    stringBuilder.Append(" <color=#222>");
    stringBuilder.Append(this.playerRun.MonstersKilled);
    stringBuilder.Append("</color>\n");
    stringBuilder.Append(Texts.Instance.GetText("heroDeaths"));
    stringBuilder.Append(" <color=#222>");
    stringBuilder.Append(this.playerRun.TotalDeaths);
    stringBuilder.Append("</color>\n");
    stringBuilder.Append(Texts.Instance.GetText("experienceGainedTome"));
    stringBuilder.Append("  <size=-.2><sprite name=experience></size> <color=#222>");
    stringBuilder.Append(Functions.ScoreFormat(this.playerRun.ExperienceGained));
    stringBuilder.Append("  <color=#505050><size=-.3>(");
    stringBuilder.Append(Functions.ScoreFormat(Functions.FuncRoundToInt((float) this.playerRun.ExperienceGained / 4f)));
    stringBuilder.Append("/e)</size></color>");
    stringBuilder.Append("</color>\n");
    stringBuilder.Append(Texts.Instance.GetText("totalResources"));
    stringBuilder.Append("  <color=#222><size=-.2><sprite name=gold></size> ");
    stringBuilder.Append(Functions.ScoreFormat(this.playerRun.TotalGoldGained));
    stringBuilder.Append("   ");
    stringBuilder.Append("<size=-.2><sprite name=dust></size> ");
    stringBuilder.Append(Functions.ScoreFormat(this.playerRun.TotalDustGained));
    stringBuilder.Append("</color>\n");
    this.description.text = stringBuilder.ToString();
  }

  private void SetPaths()
  {
    this.characterBlock.gameObject.SetActive(false);
    this.travelBlock.gameObject.SetActive(true);
    this.timePlayed.text = string.Format(Texts.Instance.GetText("timePlayed"), (object) Functions.FloatToTime(this.playerRun.PlayedTime));
    foreach (Transform travelGroup in this.travelGroups)
    {
      if (travelGroup.gameObject.name == "tGroup")
        UnityEngine.Object.Destroy((UnityEngine.Object) travelGroup.gameObject);
    }
    if (this.playerRun.VisitedNodes.Count == 0 || this.playerRun.VisitedNodesAction.Count == 0 || this.playerRun.VisitedNodes.Count != this.playerRun.VisitedNodesAction.Count)
    {
      this.tomeButtons[0].gameObject.SetActive(false);
      this.travelBlock.gameObject.SetActive(false);
    }
    else
    {
      this.tomeButtons[0].gameObject.SetActive(true);
      this.ActivateButtons(0);
      this.pathGroupsList = new List<Transform>();
      string str1 = "";
      int num1 = -1;
      GameObject gameObject1 = (GameObject) null;
      this.pathIndex = 0;
      this.pathIndexMax = 0;
      float num2 = 0.0f;
      float num3 = 14f;
      float y = 0.0f;
      for (int index = 0; index < this.playerRun.VisitedNodes.Count; ++index)
      {
        string visitedNode = this.playerRun.VisitedNodes[index];
        string[] strArray1 = this.playerRun.VisitedNodesAction[index].Split('|', StringSplitOptions.None);
        if (strArray1.Length >= 2 && !(strArray1[0] != visitedNode))
        {
          string nodeAction = strArray1[1];
          string obeliskIcon = "";
          if (strArray1.Length > 2)
            obeliskIcon = strArray1[2];
          string str2 = Texts.Instance.GetText(Globals.Instance.ZoneDataSource[Globals.Instance.GetNodeData(visitedNode).NodeZone.ZoneId.ToLower()].ZoneName);
          bool flag = false;
          string[] strArray2 = visitedNode.Split('_', StringSplitOptions.None);
          if (this.playerRun.ObeliskChallenge && (nodeAction == "destination" || strArray2 != null && strArray2.Length == 2 && strArray2[1] == "0"))
          {
            str2 = !strArray2[0].Contains("h") ? (!strArray2[0].Contains("l") ? Texts.Instance.GetText("finalObelisk") : Texts.Instance.GetText("lowerObelisk")) : Texts.Instance.GetText("upperObelisk");
            flag = true;
          }
          if ((double) num2 == 0.0 || (double) num2 > (double) num3 || (double) num2 + 2.5 > (double) num3 && str1 != str2)
          {
            ++num1;
            y = 0.0f;
            num2 = 0.0f;
            gameObject1 = new GameObject();
            gameObject1.name = "tGroup";
            gameObject1.transform.parent = this.travelGroups;
            gameObject1.transform.localPosition = new Vector3((float) (7.75 + (double) num1 * (double) this.pathColumnDistance), 7f, 0.0f);
            this.pathGroupsList.Add(gameObject1.transform);
          }
          if (((!(str1 != str2) ? 0 : (!this.playerRun.ObeliskChallenge ? 1 : 0)) | (flag ? 1 : 0)) != 0)
          {
            if ((double) y != 0.0)
              y -= 0.12f;
            GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.travelPlaceTitleGO, Vector3.zero, Quaternion.identity, gameObject1.transform);
            gameObject2.transform.localPosition = new Vector3(0.6f, y, 0.0f);
            gameObject2.transform.GetChild(0).GetComponent<TMP_Text>().text = str2;
            str1 = str2;
            num2 += 2f;
            y -= 0.5f;
          }
          if ((double) y == 0.0)
            y -= 0.09f;
          GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.travelPlaceGO, Vector3.zero, Quaternion.identity, gameObject1.transform);
          gameObject3.transform.localPosition = new Vector3(-0.2f, y, 0.0f);
          gameObject3.GetComponent<TomeTravelPlace>().SetNode(this.playerRun.ObeliskChallenge, visitedNode, nodeAction, obeliskIcon);
          ++num2;
          y -= 0.48f;
        }
      }
      this.pathIndexMax = num1 - 1;
      if (this.pathIndexMax < 0)
        this.pathIndexMax = 0;
      this.SetPathVisibility();
    }
  }

  private void SetPathVisibility()
  {
    if (this.pathIndex > 0)
      this.pathPrev.gameObject.SetActive(true);
    else
      this.pathPrev.gameObject.SetActive(false);
    if (this.pathIndex < this.pathIndexMax)
      this.pathNext.gameObject.SetActive(true);
    else
      this.pathNext.gameObject.SetActive(false);
    for (int index = 0; index < this.pathGroupsList.Count; ++index)
    {
      if (index < this.pathIndex || index > this.pathIndex + 1)
        this.pathGroupsList[index].gameObject.SetActive(false);
      else
        this.pathGroupsList[index].gameObject.SetActive(true);
    }
    this.pathPaginator.text = this.pathIndexMax <= 0 ? "" : (Functions.FuncRoundToInt((float) this.pathIndex * 0.5f) + 1).ToString() + "/" + (Functions.FuncRoundToInt((float) this.pathIndexMax * 0.5f) + 1).ToString();
    this.travelGroups.localPosition = new Vector3(-this.pathColumnDistance * (float) this.pathIndex, this.travelGroups.localPosition.y, this.travelGroups.localPosition.z);
  }

  public void NextPath()
  {
    if (this.pathIndex < this.pathIndexMax)
    {
      ++this.pathIndex;
      ++this.pathIndex;
    }
    this.SetPathVisibility();
  }

  public void PrevPath()
  {
    if (this.pathIndex > 0)
    {
      --this.pathIndex;
      --this.pathIndex;
    }
    this.SetPathVisibility();
  }

  public void RunDetailButton(int _index)
  {
    if (_index == 0)
    {
      this.characterBlock.gameObject.SetActive(false);
      this.travelBlock.gameObject.SetActive(true);
      this.ActivateButtons(0);
    }
    else
      this.DoCharacter(_index != 1 ? (_index != 2 ? (_index != 3 ? 0 : 1) : 2) : 3);
  }

  private void DoPortraits()
  {
    SkinData skd = (SkinData) null;
    string[] source = new string[4]
    {
      "warrior",
      "scout",
      "mage",
      "healer"
    };
    bool flag1 = false;
    this.tomeButtons[1].gameObject.SetActive(false);
    this.tomeButtons[2].gameObject.SetActive(false);
    this.tomeButtons[3].gameObject.SetActive(false);
    this.tomeButtons[4].gameObject.SetActive(false);
    if (this.playerRun.Char0Skin != "")
    {
      skd = Globals.Instance.GetSkinData(this.playerRun.Char0Skin);
      if ((UnityEngine.Object) skd != (UnityEngine.Object) null)
      {
        this.charactersSpr[0].sprite = skd.SpritePortraitGrande;
        flag1 = true;
      }
    }
    if (!flag1)
    {
      if (((IEnumerable<string>) source).Contains<string>(this.playerRun.Char0))
        this.playerRun.Char0 = Functions.GetClassFromCards(this.playerRun.Char0Cards);
      if (this.playerRun.Char0 != "")
      {
        skd = Globals.Instance.GetSkinData(this.playerRun.Char0);
        this.charactersSpr[0].sprite = !((UnityEngine.Object) skd != (UnityEngine.Object) null) ? (Sprite) null : skd.SpritePortraitGrande;
      }
    }
    this.charactersPlayerName[0].text = this.playerRun.Char0Owner;
    this.SetCharacterButton(skd, 4);
    bool flag2 = false;
    if (this.playerRun.Char1Skin != "")
    {
      skd = Globals.Instance.GetSkinData(this.playerRun.Char1Skin);
      if ((UnityEngine.Object) skd != (UnityEngine.Object) null)
      {
        this.charactersSpr[1].sprite = skd.SpritePortraitGrande;
        flag2 = true;
      }
    }
    if (!flag2)
    {
      if (((IEnumerable<string>) source).Contains<string>(this.playerRun.Char1))
        this.playerRun.Char1 = Functions.GetClassFromCards(this.playerRun.Char1Cards);
      if (this.playerRun.Char1 != "")
      {
        skd = Globals.Instance.GetSkinData(this.playerRun.Char1);
        this.charactersSpr[1].sprite = !((UnityEngine.Object) skd != (UnityEngine.Object) null) ? (Sprite) null : skd.SpritePortraitGrande;
      }
    }
    this.charactersPlayerName[1].text = this.playerRun.Char1Owner;
    this.SetCharacterButton(skd, 3);
    bool flag3 = false;
    if (this.playerRun.Char2Skin != "")
    {
      skd = Globals.Instance.GetSkinData(this.playerRun.Char2Skin);
      if ((UnityEngine.Object) skd != (UnityEngine.Object) null)
      {
        this.charactersSpr[2].sprite = skd.SpritePortraitGrande;
        flag3 = true;
      }
    }
    if (!flag3)
    {
      if (((IEnumerable<string>) source).Contains<string>(this.playerRun.Char2))
        this.playerRun.Char2 = Functions.GetClassFromCards(this.playerRun.Char2Cards);
      if (this.playerRun.Char2 != "")
      {
        skd = Globals.Instance.GetSkinData(this.playerRun.Char2);
        this.charactersSpr[2].sprite = !((UnityEngine.Object) skd != (UnityEngine.Object) null) ? (Sprite) null : skd.SpritePortraitGrande;
      }
    }
    this.charactersPlayerName[2].text = this.playerRun.Char2Owner;
    this.SetCharacterButton(skd, 2);
    bool flag4 = false;
    if (this.playerRun.Char3Skin != "")
    {
      skd = Globals.Instance.GetSkinData(this.playerRun.Char3Skin);
      if ((UnityEngine.Object) skd != (UnityEngine.Object) null)
      {
        this.charactersSpr[3].sprite = skd.SpritePortraitGrande;
        flag4 = true;
      }
    }
    if (!flag4)
    {
      if (((IEnumerable<string>) source).Contains<string>(this.playerRun.Char3))
        this.playerRun.Char3 = Functions.GetClassFromCards(this.playerRun.Char3Cards);
      if (this.playerRun.Char3 != "")
      {
        skd = Globals.Instance.GetSkinData(this.playerRun.Char3);
        this.charactersSpr[3].sprite = !((UnityEngine.Object) skd != (UnityEngine.Object) null) ? (Sprite) null : skd.SpritePortraitGrande;
      }
    }
    this.charactersPlayerName[3].text = this.playerRun.Char3Owner;
    this.SetCharacterButton(skd, 1);
    if (this.playerRun.Char0 == "")
    {
      this.characters[0].gameObject.SetActive(false);
      this.charactersButtons[0].gameObject.SetActive(false);
      this.tomeButtons[4].gameObject.SetActive(false);
    }
    else
    {
      this.characters[0].gameObject.SetActive(true);
      this.charactersButtons[0].gameObject.SetActive(true);
      this.tomeButtons[4].gameObject.SetActive(true);
    }
    if (this.playerRun.Char1 == "")
    {
      this.characters[1].gameObject.SetActive(false);
      this.charactersButtons[1].gameObject.SetActive(false);
      this.tomeButtons[3].gameObject.SetActive(false);
    }
    else
    {
      this.characters[1].gameObject.SetActive(true);
      this.charactersButtons[1].gameObject.SetActive(true);
      this.tomeButtons[3].gameObject.SetActive(true);
    }
    if (this.playerRun.Char2 == "")
    {
      this.characters[2].gameObject.SetActive(false);
      this.charactersButtons[2].gameObject.SetActive(false);
      this.tomeButtons[2].gameObject.SetActive(false);
    }
    else
    {
      this.characters[2].gameObject.SetActive(true);
      this.charactersButtons[2].gameObject.SetActive(true);
      this.tomeButtons[2].gameObject.SetActive(true);
    }
    if (this.playerRun.Char3 == "")
    {
      this.characters[3].gameObject.SetActive(false);
      this.charactersButtons[3].gameObject.SetActive(false);
      this.tomeButtons[1].gameObject.SetActive(false);
    }
    else
    {
      this.characters[3].gameObject.SetActive(true);
      this.charactersButtons[3].gameObject.SetActive(true);
      this.tomeButtons[1].gameObject.SetActive(true);
    }
  }

  private void SetCharacterButton(SkinData skd, int _index)
  {
    if (!((UnityEngine.Object) skd != (UnityEngine.Object) null))
      return;
    SubClassData subClassData = Globals.Instance.GetSubClassData(skd.SkinSubclass.Id);
    if (!((UnityEngine.Object) subClassData != (UnityEngine.Object) null))
      return;
    this.tomeButtons[_index].gameObject.SetActive(true);
    this.tomeButtons[_index].SetText(subClassData.CharacterName);
    this.tomeButtons[_index].SetColor(Globals.Instance.ClassColor[subClassData.HeroClass.ToString().ToLower()]);
  }

  private void ActivateButtons(int _index)
  {
    this.tomeButtons[_index].Activate();
    for (int index = 0; index < 5; ++index)
    {
      if (index != _index)
        this.tomeButtons[index].Deactivate();
      this.tomeButtons[index].transform.localPosition = new Vector3(this.tomeButtons[index].transform.localPosition.x, this.tomeButtons[index].transform.localPosition.y, -3f);
    }
  }

  public void DoCharacter(int _index)
  {
    this.characterBlock.gameObject.SetActive(true);
    this.travelBlock.gameObject.SetActive(false);
    this.activeCharacter = _index;
    this.characterItems = (List<string>) null;
    int num = 1;
    SubClassData subClassData = (SubClassData) null;
    if (this.activeCharacter == 0)
    {
      this.characterCards = this.playerRun.Char0Cards;
      if (this.playerRun.Char0Items != null)
        this.characterItems = this.playerRun.Char0Items;
      if (this.playerRun.Char0Traits != null)
        this.characterTraits = this.playerRun.Char0Traits;
      Globals.Instance.GetSkinData(this.playerRun.Char0Skin);
      num = this.playerRun.Char0Rank;
      subClassData = Globals.Instance.GetSubClassData(this.playerRun.Char0);
      this.ActivateButtons(4);
    }
    else if (this.activeCharacter == 1)
    {
      this.characterCards = this.playerRun.Char1Cards;
      if (this.playerRun.Char1Items != null)
        this.characterItems = this.playerRun.Char1Items;
      if (this.playerRun.Char1Traits != null)
        this.characterTraits = this.playerRun.Char1Traits;
      Globals.Instance.GetSkinData(this.playerRun.Char1Skin);
      num = this.playerRun.Char1Rank;
      subClassData = Globals.Instance.GetSubClassData(this.playerRun.Char1);
      this.ActivateButtons(3);
    }
    else if (this.activeCharacter == 2)
    {
      this.characterCards = this.playerRun.Char2Cards;
      if (this.playerRun.Char2Items != null)
        this.characterItems = this.playerRun.Char2Items;
      if (this.playerRun.Char2Traits != null)
        this.characterTraits = this.playerRun.Char2Traits;
      Globals.Instance.GetSkinData(this.playerRun.Char2Skin);
      num = this.playerRun.Char2Rank;
      subClassData = Globals.Instance.GetSubClassData(this.playerRun.Char2);
      this.ActivateButtons(2);
    }
    else if (this.activeCharacter == 3)
    {
      this.characterCards = this.playerRun.Char3Cards;
      if (this.playerRun.Char3Items != null)
        this.characterItems = this.playerRun.Char3Items;
      if (this.playerRun.Char3Traits != null)
        this.characterTraits = this.playerRun.Char3Traits;
      Globals.Instance.GetSkinData(this.playerRun.Char3Skin);
      num = this.playerRun.Char3Rank;
      subClassData = Globals.Instance.GetSubClassData(this.playerRun.Char3);
      this.ActivateButtons(1);
    }
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.Append(Texts.Instance.GetText("cards"));
    stringBuilder1.Append(" <size=-.3>(");
    stringBuilder1.Append(this.characterCards.Count);
    stringBuilder1.Append(")</size>");
    this.cards.text = stringBuilder1.ToString();
    this.characterActiveSpr.sprite = this.charactersSpr[this.activeCharacter].sprite;
    if ((UnityEngine.Object) subClassData != (UnityEngine.Object) null)
    {
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append(subClassData.CharacterName);
      stringBuilder2.Append("<br><size=2.6><color=#303030>");
      if (!this.playerRun.ObeliskChallenge)
        stringBuilder2.Append(string.Format(Texts.Instance.GetText("rankProgress"), (object) num));
      this.characterActiveName.text = stringBuilder2.ToString();
    }
    else
      this.characterActiveName.text = "";
    this.SetCards();
    this.SetItems();
    this.SetTraits();
    this.characterBlock.gameObject.SetActive(true);
  }

  private void SetItems()
  {
    if (this.characterItems != null && this.characterItems.Count >= 1 && this.characterItems[0] != null)
    {
      this.iconWeapon.gameObject.SetActive(true);
      this.iconWeapon.ShowIcon("weapon", this.characterItems[0], true);
    }
    else
      this.iconWeapon.gameObject.SetActive(false);
    if (this.characterItems != null && this.characterItems.Count >= 2 && this.characterItems[1] != null)
    {
      this.iconArmor.gameObject.SetActive(true);
      this.iconArmor.ShowIcon("armor", this.characterItems[1], true);
    }
    else
      this.iconArmor.gameObject.SetActive(false);
    if (this.characterItems != null && this.characterItems.Count >= 3 && this.characterItems[2] != null)
    {
      this.iconJewelry.gameObject.SetActive(true);
      this.iconJewelry.ShowIcon("jewelry", this.characterItems[2], true);
    }
    else
      this.iconJewelry.gameObject.SetActive(false);
    if (this.characterItems != null && this.characterItems.Count >= 4 && this.characterItems[3] != null)
    {
      this.iconAccesory.gameObject.SetActive(true);
      this.iconAccesory.ShowIcon("accesory", this.characterItems[3], true);
    }
    else
      this.iconAccesory.gameObject.SetActive(false);
    if (this.characterItems != null && this.characterItems.Count >= 5 && this.characterItems[4] != null)
    {
      this.iconPet.gameObject.SetActive(true);
      this.iconPet.ShowIcon("pet", this.characterItems[4], true);
    }
    else
      this.iconPet.gameObject.SetActive(false);
  }

  private void SetCards()
  {
    this.ClearCardListContainer();
    Dictionary<string, int> source = new Dictionary<string, int>();
    int count = this.characterCards.Count;
    SortedList sortedList = new SortedList();
    for (int index = 0; index < count; ++index)
      sortedList.Add((object) (Globals.Instance.GetCardData(this.characterCards[index], false).CardName + this.characterCards[index] + index.ToString()), (object) (this.characterCards[index] + "_" + index.ToString()));
    GameObject[] gameObjectArray = new GameObject[count];
    for (int index = 0; index < count; ++index)
    {
      CardData cardData = Globals.Instance.GetCardData(sortedList.GetByIndex(index).ToString().Split('_', StringSplitOptions.None)[0], false);
      int num = cardData.CardClass != Enums.CardClass.Injury ? (cardData.CardClass != Enums.CardClass.Boon ? cardData.EnergyCost : -2) : -1;
      source.Add(cardData.Id + "_" + sortedList.GetByIndex(index).ToString().Split('_', StringSplitOptions.None)[1], num);
    }
    Dictionary<string, int> dictionary = source.OrderBy<KeyValuePair<string, int>, int>((Func<KeyValuePair<string, int>, int>) (x => x.Value)).ToDictionary<KeyValuePair<string, int>, string, int>((Func<KeyValuePair<string, int>, string>) (x => x.Key), (Func<KeyValuePair<string, int>, int>) (x => x.Value));
    CardVertical[] cardVerticalArray = new CardVertical[dictionary.Count * 2];
    for (int index1 = 0; index1 < dictionary.Count; ++index1)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cardVerticalPrefab, new Vector3(0.0f, 0.0f, -1f), Quaternion.identity, this.cardListContainer);
      gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0.0f);
      string key = dictionary.ElementAt<KeyValuePair<string, int>>(index1).Key;
      int index2 = int.Parse(key.Split('_', StringSplitOptions.None)[1]);
      gameObject.name = "deckcard_" + index2.ToString();
      gameObjectArray[index2] = gameObject;
      cardVerticalArray[index2] = gameObject.GetComponent<CardVertical>();
      cardVerticalArray[index2].SetCard(key);
    }
    this.cardListContainer.GetComponent<GridLayoutGroup>().enabled = false;
    this.cardListContainer.GetComponent<GridLayoutGroup>().enabled = true;
  }

  private void SetTraits()
  {
    for (int index = 0; index < 5; ++index)
    {
      if (this.characterTraits != null && this.characterTraits.Count > index && this.characterTraits[index] != "")
      {
        this.traits[index].gameObject.SetActive(true);
        this.traits[index].SetTrait(this.characterTraits[index]);
      }
      else
        this.traits[index].gameObject.SetActive(false);
    }
  }

  private void ClearCardListContainer()
  {
    foreach (Component component in this.cardListContainer)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
  }
}
