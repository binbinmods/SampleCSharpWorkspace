// Decompiled with JetBrains decompiler
// Type: CorruptionManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CorruptionManager : MonoBehaviour
{
  public SpriteRenderer[] monsterSprite;
  public Transform monsterSpriteFrontChampion;
  public Transform monsterSpriteBackChampion;
  public Transform boxBorder;
  public Transform bgHand;
  public Transform bgCards;
  public Transform bgCorruptionOn;
  public TMP_Text textReward;
  public TMP_Text textAcceptScore;
  public TMP_Text textDifficulty;
  public Transform corruptionButton;
  public Transform corruptionContinue;
  public Transform corruptionOnlyMaster;
  private GameObject cardCorruption;
  private GameObject cardReward;
  private string corruptionIdCard;
  private CardData cDataCorruption;
  public Transform corruptionBoxX;
  private bool clicked;
  private List<string> basicCorruptions = new List<string>();
  private List<string> advancedCorruptions = new List<string>();
  private Hero[] teamAtO;
  public BotonGeneric botonGenericX;
  public BotonGeneric rewardBotA;
  public BotonGeneric rewardBotB;
  private int corruptionRewardType = -1;
  private string corruptionRewardId = "";
  private string corruptionRewardIdB = "";
  private int corruptionRewardChar = -1;
  private string corruptionRewardCard = "";
  private Node _nodeSelected;
  private int randomCorruptionIndex = -1;
  private string nodeSelectedAssignedId = "";
  private string nodeSelectedDataId = "";
  private int mpReadyRetryIndex;
  private PhotonView photonView;
  private Coroutine coroutineReward;
  public PopupText corruptionIconPopup;
  public Transform[] corruptionIcon;
  public Transform elements;
  public TMP_Text buttonShowText;
  private bool showStatus;
  private List<Transform> controllerList = new List<Transform>();
  private int controllerHorizontalIndex = -1;
  private Vector2 warpPosition;

  private void Awake()
  {
    if ((UnityEngine.Object) MapManager.Instance != (UnityEngine.Object) null)
      this.photonView = MapManager.Instance.GetPhotonView();
    this.basicCorruptions = new List<string>();
    this.basicCorruptions.Add("goldshards0");
    this.basicCorruptions.Add("freecardremove");
    this.basicCorruptions.Add("rareshop");
    this.basicCorruptions.Add("altarupgrade");
    this.basicCorruptions.Add("heal20");
    this.basicCorruptions.Add("herocard");
    this.advancedCorruptions = new List<string>();
    this.advancedCorruptions.Add("goldshards1");
    this.advancedCorruptions.Add("freecardupgrade");
    this.advancedCorruptions.Add("freecardremove2");
    this.advancedCorruptions.Add("exoticshop");
    this.advancedCorruptions.Add("increasedqualityofcardrewards");
    this.advancedCorruptions.Add("herocard");
  }

  public void ShowHide()
  {
    if (this.showStatus)
    {
      this.elements.gameObject.SetActive(false);
      this.buttonShowText.text = Texts.Instance.GetText("show").ToUpper();
    }
    else
    {
      this.elements.gameObject.SetActive(true);
      this.buttonShowText.text = Texts.Instance.GetText("hide").ToUpper();
    }
    this.showStatus = !this.showStatus;
  }

  public void InitCorruption(Node _node, bool next = false)
  {
    if ((UnityEngine.Object) _node == (UnityEngine.Object) null)
      return;
    this.gameObject.SetActive(true);
    this.showStatus = false;
    this.ShowHide();
    this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -100f);
    string str = _node.nodeData.NodeId + AtOManager.Instance.GetGameId();
    Functions.DebugLogGD("corruption seed -> " + str);
    int deterministicHashCode = str.GetDeterministicHashCode();
    Functions.DebugLogGD("resultseed -> " + deterministicHashCode.ToString());
    UnityEngine.Random.InitState(deterministicHashCode);
    this._nodeSelected = _node;
    this.nodeSelectedAssignedId = _node.GetNodeAssignedId();
    this.nodeSelectedDataId = _node.nodeData.NodeId;
    List<string> stringList = new List<string>();
    bool flag1 = false;
    if (GameManager.Instance.IsWeeklyChallenge())
    {
      ChallengeData weeklyData = Globals.Instance.GetWeeklyData(AtOManager.Instance.GetWeekly());
      if (weeklyData.CorruptionList != null && weeklyData.CorruptionList.Count > 0)
      {
        for (int index = 0; index < weeklyData.CorruptionList.Count; ++index)
        {
          if ((UnityEngine.Object) weeklyData.CorruptionList[index] != (UnityEngine.Object) null)
            stringList.Add(weeklyData.CorruptionList[index].Id);
        }
        flag1 = true;
      }
    }
    if (!flag1 || stringList.Count == 0)
    {
      for (int index = 0; index < Globals.Instance.CardListByType[Enums.CardType.Corruption].Count; ++index)
      {
        CardData cardData = Globals.Instance.GetCardData(Globals.Instance.CardListByType[Enums.CardType.Corruption][index], false);
        if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && !cardData.OnlyInWeekly)
          stringList.Add(Globals.Instance.CardListByType[Enums.CardType.Corruption][index]);
      }
    }
    bool flag2 = false;
    while (!flag2)
    {
      int index1;
      if (!next)
      {
        index1 = UnityEngine.Random.Range(0, stringList.Count);
      }
      else
      {
        index1 = this.randomCorruptionIndex + 1;
        if (index1 >= stringList.Count)
          index1 = 0;
      }
      this.randomCorruptionIndex = index1;
      this.corruptionIdCard = stringList[index1];
      if (!(this.corruptionIdCard == "resurrection") && !(this.corruptionIdCard == "resurrectiona") && !(this.corruptionIdCard == "resurrectionb") && !(this.corruptionIdCard == "resurrectionrare"))
      {
        for (int index2 = 0; index2 < deterministicHashCode % 10; ++index2)
          UnityEngine.Random.Range(0, 100);
        this.cDataCorruption = Globals.Instance.GetCardData(this.corruptionIdCard, false);
        if (!((UnityEngine.Object) this.cDataCorruption == (UnityEngine.Object) null) && (GameManager.Instance.IsWeeklyChallenge() || !this.cDataCorruption.OnlyInWeekly))
        {
          flag2 = true;
          if (!next && PlayerManager.Instance.MonstersKilled < 30 && this.cDataCorruption.CardRarity != Enums.CardRarity.Common && this.cDataCorruption.CardRarity != Enums.CardRarity.Uncommon)
            flag2 = false;
        }
      }
    }
    if (AtOManager.Instance.GetTownTier() == 0)
    {
      this.cDataCorruption = Functions.GetCardDataFromCardData(this.cDataCorruption, "");
      if ((UnityEngine.Object) this.cDataCorruption != (UnityEngine.Object) null)
        this.corruptionIdCard = this.cDataCorruption.Id;
    }
    else if (AtOManager.Instance.GetTownTier() == 1)
    {
      this.cDataCorruption = Functions.GetCardDataFromCardData(this.cDataCorruption, "A");
      if ((UnityEngine.Object) this.cDataCorruption != (UnityEngine.Object) null)
        this.corruptionIdCard = this.cDataCorruption.Id;
    }
    else if (AtOManager.Instance.GetTownTier() == 2)
    {
      if ((UnityEngine.Object) this.cDataCorruption != (UnityEngine.Object) null)
        this.cDataCorruption = Functions.GetCardDataFromCardData(this.cDataCorruption, "B");
      this.corruptionIdCard = this.cDataCorruption.Id;
    }
    else if (AtOManager.Instance.GetTownTier() > 2)
    {
      if ((UnityEngine.Object) this.cDataCorruption != (UnityEngine.Object) null)
        this.cDataCorruption = Functions.GetCardDataFromCardData(this.cDataCorruption, "RARE");
      this.corruptionIdCard = this.cDataCorruption.Id;
    }
    if ((UnityEngine.Object) this.cDataCorruption == (UnityEngine.Object) null)
      this.cDataCorruption = Globals.Instance.GetCardData(this.corruptionIdCard, false);
    this.corruptionRewardType = this.cDataCorruption.CardRarity == Enums.CardRarity.Common || this.cDataCorruption.CardRarity == Enums.CardRarity.Uncommon ? 0 : 1;
    bool flag3 = false;
    if (this.corruptionRewardType == 0)
    {
      while (!flag3)
      {
        this.corruptionRewardId = this.corruptionRewardIdB = this.basicCorruptions[UnityEngine.Random.Range(0, this.basicCorruptions.Count)];
        while (this.corruptionRewardIdB == this.corruptionRewardId)
          this.corruptionRewardIdB = this.basicCorruptions[UnityEngine.Random.Range(0, this.basicCorruptions.Count)];
        flag3 = true;
        if (GameManager.Instance.IsObeliskChallenge() && (this.corruptionRewardId == "freecardremove" || this.corruptionRewardId == "rareshop" || this.corruptionRewardId == "randomcardupgrade" || this.corruptionRewardId == "altarupgrade" || this.corruptionRewardId == "freecardupgrade" || this.corruptionRewardId == "freecardremove2" || this.corruptionRewardId == "exoticshop" || this.corruptionRewardIdB == "freecardremove" || this.corruptionRewardIdB == "rareshop" || this.corruptionRewardIdB == "randomcardupgrade" || this.corruptionRewardIdB == "altarupgrade" || this.corruptionRewardIdB == "freecardupgrade" || this.corruptionRewardIdB == "freecardremove2" || this.corruptionRewardIdB == "exoticshop"))
          flag3 = false;
      }
    }
    else
    {
      while (!flag3)
      {
        this.corruptionRewardId = this.corruptionRewardIdB = this.advancedCorruptions[UnityEngine.Random.Range(0, this.advancedCorruptions.Count)];
        while (this.corruptionRewardIdB == this.corruptionRewardId)
          this.corruptionRewardIdB = this.advancedCorruptions[UnityEngine.Random.Range(0, this.advancedCorruptions.Count)];
        flag3 = true;
        if (GameManager.Instance.IsObeliskChallenge() && (this.corruptionRewardId == "freecardremove" || this.corruptionRewardId == "rareshop" || this.corruptionRewardId == "randomcardupgrade" || this.corruptionRewardId == "altarupgrade" || this.corruptionRewardId == "freecardupgrade" || this.corruptionRewardId == "freecardremove2" || this.corruptionRewardIdB == "freecardremove" || this.corruptionRewardIdB == "rareshop" || this.corruptionRewardIdB == "randomcardupgrade" || this.corruptionRewardIdB == "altarupgrade" || this.corruptionRewardIdB == "freecardupgrade" || this.corruptionRewardIdB == "freecardremove2"))
          flag3 = false;
      }
    }
    CorruptionPackData corruptionPackData = (CorruptionPackData) null;
    this.teamAtO = AtOManager.Instance.GetTeam();
    bool flag4 = false;
    while (!flag4)
    {
      this.corruptionRewardChar = UnityEngine.Random.Range(0, 4);
      if (this.teamAtO[this.corruptionRewardChar] != null && (UnityEngine.Object) this.teamAtO[this.corruptionRewardChar].HeroData != (UnityEngine.Object) null)
        flag4 = true;
    }
    Enums.CardClass cardClass = (Enums.CardClass) Enum.Parse(typeof (Enums.CardClass), Enum.GetName(typeof (Enums.HeroClass), (object) this.teamAtO[this.corruptionRewardChar].HeroData.HeroClass));
    foreach (KeyValuePair<string, CorruptionPackData> keyValuePair in Globals.Instance.CorruptionPackDataSource)
    {
      corruptionPackData = keyValuePair.Value;
      if (corruptionPackData.PackClass == cardClass)
      {
        if (corruptionPackData.PackTier == AtOManager.Instance.GetTownTier())
          break;
      }
    }
    if ((UnityEngine.Object) corruptionPackData != (UnityEngine.Object) null)
      this.corruptionRewardCard = this.corruptionRewardType != 0 ? corruptionPackData.HighPack[UnityEngine.Random.Range(0, corruptionPackData.HighPack.Count)].Id : corruptionPackData.LowPack[UnityEngine.Random.Range(0, corruptionPackData.LowPack.Count)].Id;
    if (GameManager.Instance.IsMultiplayer())
    {
      string corruptionRewardId = this.corruptionRewardId;
      string corruptionRewardIdB = this.corruptionRewardIdB;
      string corruptionIdCard = this.corruptionIdCard;
      int corruptionRewardChar = this.corruptionRewardChar;
      string corruptionRewardCard = this.corruptionRewardCard;
      string selectedAssignedId = this.nodeSelectedAssignedId;
      string nodeSelectedDataId = this.nodeSelectedDataId;
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("Send corruption NET data");
      this.photonView.RPC("NET_ShareCorruption", RpcTarget.Others, (object) corruptionRewardId, (object) corruptionRewardIdB, (object) corruptionIdCard, (object) corruptionRewardChar, (object) corruptionRewardCard, (object) selectedAssignedId, (object) nodeSelectedDataId);
    }
    this.StartCoroutine(this.DrawCorruptionCo());
  }

  public void DrawCorruptionFromNet(
    string _corruptionRewardId,
    string _corruptionRewardIdB,
    string _corruptionIdCard,
    int _corruptionRewardChar,
    string _corruptionRewardCard,
    string _nodeSelectedAssignedId,
    string _nodeSelectedDataId)
  {
    this.corruptionRewardId = _corruptionRewardId;
    this.corruptionRewardIdB = _corruptionRewardIdB;
    this.corruptionIdCard = _corruptionIdCard;
    this.corruptionRewardChar = _corruptionRewardChar;
    this.corruptionRewardCard = _corruptionRewardCard;
    this.nodeSelectedAssignedId = _nodeSelectedAssignedId;
    this.nodeSelectedDataId = _nodeSelectedDataId;
    this.teamAtO = AtOManager.Instance.GetTeam();
    this.DrawCorruption();
  }

  public void NextCorruption() => this.InitCorruption(this._nodeSelected, true);

  private void DrawCorruption()
  {
    this.gameObject.SetActive(true);
    this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, -100f);
    this.StartCoroutine(this.DrawCorruptionCo());
  }

  private IEnumerator DrawCorruptionCo()
  {
    CorruptionManager corruptionManager = this;
    if (GameManager.Instance.IsMultiplayer())
    {
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersReady("drawCorruption"))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("Game ready, Everybody checked");
        NetworkManager.Instance.PlayersNetworkContinue("drawCorruption");
      }
      else
      {
        NetworkManager.Instance.SetWaitingSyncro("drawCorruption", true);
        NetworkManager.Instance.SetStatusReady("drawCorruption");
        corruptionManager.mpReadyRetryIndex = 0;
        while (NetworkManager.Instance.WaitingSyncro["drawCorruption"])
        {
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
          ++corruptionManager.mpReadyRetryIndex;
          if (corruptionManager.mpReadyRetryIndex > 10)
          {
            NetworkManager.Instance.SetStatusReady("drawCorruption");
            corruptionManager.mpReadyRetryIndex = 0;
          }
        }
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("drawCorruption, we can continue!");
      }
    }
    corruptionManager.corruptionOnlyMaster.gameObject.SetActive(false);
    corruptionManager.corruptionButton.gameObject.SetActive(true);
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
    {
      corruptionManager.corruptionOnlyMaster.gameObject.SetActive(true);
      corruptionManager.corruptionButton.gameObject.SetActive(false);
    }
    corruptionManager.cDataCorruption = Globals.Instance.GetCardData(corruptionManager.corruptionIdCard, false);
    corruptionManager.transform.localPosition = new Vector3(corruptionManager.transform.localPosition.x, corruptionManager.transform.localPosition.y, 0.0f);
    UnityEngine.Object.Destroy((UnityEngine.Object) corruptionManager.cardCorruption);
    corruptionManager.cardCorruption = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, corruptionManager.elements);
    corruptionManager.cardCorruption.transform.localPosition = new Vector3(-3.35f, 0.04f, -1f);
    corruptionManager.cardCorruption.transform.localScale = new Vector3(1.52f, 1.52f, 1f);
    UnityEngine.Object.Destroy((UnityEngine.Object) corruptionManager.cardReward);
    corruptionManager.cardReward = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, corruptionManager.elements);
    corruptionManager.cardReward.transform.localPosition = new Vector3(6.65f, -1.35f, -1f);
    corruptionManager.cardReward.transform.localScale = new Vector3(1.35f, 1.35f, 1f);
    corruptionManager.cardReward.gameObject.SetActive(false);
    CombatData combatData = Globals.Instance.GetCombatData(corruptionManager.nodeSelectedAssignedId);
    bool flag = false;
    if (MadnessManager.Instance.IsMadnessTraitActive("randomcombats") || GameManager.Instance.IsObeliskChallenge())
      flag = true;
    corruptionManager.monsterSpriteFrontChampion.gameObject.SetActive(false);
    corruptionManager.monsterSpriteBackChampion.gameObject.SetActive(false);
    NodeData nodeData = Globals.Instance.GetNodeData(corruptionManager.nodeSelectedDataId);
    if (flag && (UnityEngine.Object) nodeData != (UnityEngine.Object) null && nodeData.NodeCombatTier != Enums.CombatTier.T0)
    {
      string str = "";
      if ((UnityEngine.Object) combatData != (UnityEngine.Object) null)
        str = combatData.CombatId;
      int deterministicHashCode = (corruptionManager.nodeSelectedDataId + AtOManager.Instance.GetGameId() + str).GetDeterministicHashCode();
      NPCData[] randomCombat = Functions.GetRandomCombat(nodeData.NodeCombatTier, deterministicHashCode, corruptionManager.nodeSelectedDataId);
      if (randomCombat != null)
      {
        int num1 = 0;
        for (int index = 0; index < randomCombat.Length; ++index)
        {
          if ((UnityEngine.Object) randomCombat[index] != (UnityEngine.Object) null)
          {
            corruptionManager.monsterSprite[index].gameObject.SetActive(true);
            corruptionManager.monsterSprite[index].sprite = randomCombat[index].SpriteSpeed;
            ++num1;
          }
          else
            corruptionManager.monsterSprite[index].gameObject.SetActive(false);
        }
        if ((UnityEngine.Object) randomCombat[0] != (UnityEngine.Object) null && randomCombat[0].IsNamed)
        {
          string auraCurseImmune = Functions.GetAuraCurseImmune(randomCombat[0], corruptionManager.nodeSelectedDataId);
          AuraCurseData auraCurseData = Globals.Instance.GetAuraCurseData(auraCurseImmune);
          if ((UnityEngine.Object) auraCurseData != (UnityEngine.Object) null)
          {
            corruptionManager.monsterSpriteFrontChampion.gameObject.SetActive(true);
            corruptionManager.monsterSpriteFrontChampion.GetComponent<SpriteRenderer>().sprite = auraCurseData.Sprite;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<size=+2>");
            stringBuilder.Append(Texts.Instance.GetText("championMonster"));
            stringBuilder.Append("</size><br><color=#F3404E>");
            stringBuilder.Append(Texts.Instance.GetText("immune"));
            stringBuilder.Append("  <voffset=-2><size=+5><sprite name=");
            stringBuilder.Append(auraCurseImmune);
            stringBuilder.Append(">");
            corruptionManager.monsterSpriteFrontChampion.GetComponent<PopupText>().text = stringBuilder.ToString();
          }
        }
        if ((UnityEngine.Object) randomCombat[3] != (UnityEngine.Object) null && randomCombat[3].IsNamed)
        {
          string auraCurseImmune = Functions.GetAuraCurseImmune(randomCombat[3], corruptionManager.nodeSelectedDataId);
          AuraCurseData auraCurseData = Globals.Instance.GetAuraCurseData(auraCurseImmune);
          if ((UnityEngine.Object) auraCurseData != (UnityEngine.Object) null)
          {
            corruptionManager.monsterSpriteBackChampion.gameObject.SetActive(true);
            corruptionManager.monsterSpriteBackChampion.GetComponent<SpriteRenderer>().sprite = auraCurseData.Sprite;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<size=+2>");
            stringBuilder.Append(Texts.Instance.GetText("championMonster"));
            stringBuilder.Append("</size><br><color=#F3404E>");
            stringBuilder.Append(Texts.Instance.GetText("immune"));
            stringBuilder.Append("  <voffset=-2><size=+5><sprite name=");
            stringBuilder.Append(auraCurseImmune);
            stringBuilder.Append(">");
            corruptionManager.monsterSpriteBackChampion.GetComponent<PopupText>().text = stringBuilder.ToString();
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
          int num2 = AtOManager.Instance.Sandbox_lessNPCs;
          if (num2 >= num1)
            num2 = num1 - 1;
          if (num2 > source.Count)
            num2 = source.Count;
          for (int index = 0; index < num2; ++index)
            corruptionManager.monsterSprite[source.ElementAt<KeyValuePair<int, int>>(index).Value].gameObject.SetActive(false);
        }
      }
    }
    else
    {
      int num3 = 0;
      for (int index = 0; index < combatData.NPCList.Length; ++index)
      {
        if (AtOManager.Instance.GetMadnessDifficulty() == 0 && combatData.NpcRemoveInMadness0Index == index && AtOManager.Instance.GetActNumberForText() < 3)
          corruptionManager.monsterSprite[index].gameObject.SetActive(false);
        else if ((UnityEngine.Object) combatData.NPCList[index] != (UnityEngine.Object) null)
        {
          corruptionManager.monsterSprite[index].gameObject.SetActive(true);
          corruptionManager.monsterSprite[index].sprite = combatData.NPCList[index].SpriteSpeed;
          ++num3;
        }
        else
          corruptionManager.monsterSprite[index].gameObject.SetActive(false);
      }
      if (AtOManager.Instance.Sandbox_lessNPCs != 0)
      {
        SortedDictionary<int, int> source = new SortedDictionary<int, int>();
        for (int index = 0; index < combatData.NPCList.Length; ++index)
        {
          if ((UnityEngine.Object) combatData.NPCList[index] != (UnityEngine.Object) null && !combatData.NPCList[index].IsNamed && !combatData.NPCList[index].IsBoss)
            source.Add(combatData.NPCList[index].Hp * 10000 + index, index);
        }
        int num4 = AtOManager.Instance.Sandbox_lessNPCs;
        if (num4 >= num3)
          num4 = num3 - 1;
        if (num4 > source.Count)
          num4 = source.Count;
        for (int index = 0; index < num4; ++index)
          corruptionManager.monsterSprite[source.ElementAt<KeyValuePair<int, int>>(index).Value].gameObject.SetActive(false);
      }
    }
    CardItem component1 = corruptionManager.cardCorruption.GetComponent<CardItem>();
    component1.SetCard(corruptionManager.corruptionIdCard, false);
    component1.TopLayeringOrder("Book", 1000);
    component1.cardmakebig = true;
    component1.CreateColliderAdjusted();
    component1.DrawBorder("purple");
    component1.cardmakebigSize = 1.52f;
    component1.cardmakebigSizeMax = 1.62f;
    CardItem component2 = corruptionManager.cardReward.GetComponent<CardItem>();
    component2.SetCard(corruptionManager.corruptionRewardCard, false);
    component2.TopLayeringOrder("Book", 1000);
    component2.cardmakebig = true;
    component2.CreateColliderAdjusted();
    component2.cardmakebigSize = 1.35f;
    component2.cardmakebigSizeMax = 1.45f;
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.Append("<size=+5>");
    string str1;
    int num;
    if (corruptionManager.cDataCorruption.CardRarity == Enums.CardRarity.Common)
    {
      str1 = string.Format(Texts.Instance.GetText("corruptionRewardScore"), (object) "40");
      corruptionManager.textDifficulty.text = Texts.Instance.GetText("easy");
      corruptionManager.textDifficulty.color = Functions.HexToColor("#FFFFFF");
      stringBuilder1.Append(Texts.Instance.GetText("easy"));
      num = 0;
    }
    else if (corruptionManager.cDataCorruption.CardRarity == Enums.CardRarity.Uncommon)
    {
      str1 = string.Format(Texts.Instance.GetText("corruptionRewardScore"), (object) "80");
      corruptionManager.textDifficulty.text = Texts.Instance.GetText("average");
      corruptionManager.textDifficulty.color = Globals.Instance.RarityColor["uncommon"];
      stringBuilder1.Append(Texts.Instance.GetText("average"));
      num = 1;
    }
    else if (corruptionManager.cDataCorruption.CardRarity == Enums.CardRarity.Rare)
    {
      str1 = string.Format(Texts.Instance.GetText("corruptionRewardScore"), (object) "130");
      corruptionManager.textDifficulty.text = Texts.Instance.GetText("hard");
      corruptionManager.textDifficulty.color = Globals.Instance.RarityColor["rare"];
      stringBuilder1.Append(Texts.Instance.GetText("hard"));
      num = 2;
    }
    else
    {
      str1 = string.Format(Texts.Instance.GetText("corruptionRewardScore"), (object) "200");
      corruptionManager.textDifficulty.text = Texts.Instance.GetText("extreme");
      corruptionManager.textDifficulty.color = Globals.Instance.RarityColor["epic"];
      stringBuilder1.Append(Texts.Instance.GetText("extreme"));
      num = 3;
    }
    corruptionManager.corruptionIconPopup.text = stringBuilder1.ToString();
    for (int index = 0; index < 4; ++index)
    {
      if (index != num)
        corruptionManager.corruptionIcon[index].gameObject.SetActive(false);
      else
        corruptionManager.corruptionIcon[index].gameObject.SetActive(true);
    }
    corruptionManager.textAcceptScore.text = str1;
    corruptionManager.ShowClicked();
    AtOManager.Instance.corruptionId = "";
    AtOManager.Instance.corruptionType = corruptionManager.corruptionRewardType;
    AtOManager.Instance.corruptionRewardChar = corruptionManager.corruptionRewardChar;
    AtOManager.Instance.corruptionIdCard = corruptionManager.corruptionIdCard;
    AtOManager.Instance.corruptionRewardCard = corruptionManager.corruptionRewardCard;
    string _text1 = corruptionManager.CorruptionText(corruptionManager.corruptionRewardId);
    corruptionManager.rewardBotA.SetText(_text1);
    string _text2 = corruptionManager.CorruptionText(corruptionManager.corruptionRewardIdB);
    corruptionManager.rewardBotB.SetText(_text2);
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
    {
      corruptionManager.rewardBotA.Disable();
      corruptionManager.rewardBotB.Disable();
      corruptionManager.botonGenericX.Disable();
      corruptionManager.rewardBotA.ShowDisableMask(false);
      corruptionManager.rewardBotB.ShowDisableMask(false);
      corruptionManager.botonGenericX.ShowDisableMask(false);
      corruptionManager.corruptionContinue.gameObject.SetActive(false);
    }
  }

  public void ChooseReward(string choosed)
  {
    switch (choosed)
    {
      case "A":
        this.rewardBotA.color = Functions.HexToColor("#E0A44E");
        this.rewardBotA.SetColor();
        this.rewardBotA.PermaBorder(true);
        this.rewardBotB.color = Functions.HexToColor("#FFFFFF");
        this.rewardBotB.SetColor();
        this.rewardBotB.PermaBorder(false);
        if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
        {
          AtOManager.Instance.corruptionId = this.corruptionRewardId;
          break;
        }
        break;
      case "B":
        this.rewardBotA.color = Functions.HexToColor("#FFFFFF");
        this.rewardBotA.SetColor();
        this.rewardBotA.PermaBorder(false);
        this.rewardBotB.color = Functions.HexToColor("#E0A44E");
        this.rewardBotB.SetColor();
        this.rewardBotB.PermaBorder(true);
        if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
        {
          AtOManager.Instance.corruptionId = this.corruptionRewardIdB;
          break;
        }
        break;
    }
    if (GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
    {
      if (this.coroutineReward != null)
        this.StopCoroutine(this.coroutineReward);
      short choosed1 = 0;
      switch (choosed)
      {
        case "A":
          choosed1 = (short) 1;
          break;
        case "B":
          choosed1 = (short) 2;
          break;
      }
      this.coroutineReward = this.StartCoroutine(this.coroutineRewardFunc(choosed1));
    }
    if (this.clicked)
      return;
    this.BoxClicked();
  }

  private IEnumerator coroutineRewardFunc(short choosed)
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.25f);
    this.photonView.RPC("NET_ChooseRewardCorruption", RpcTarget.Others, (object) choosed);
  }

  public bool CorruptionOk()
  {
    if (!this.clicked || !(AtOManager.Instance.corruptionId == ""))
      return true;
    AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("corruptionSelect"));
    return false;
  }

  private string CorruptionText(string _corruption)
  {
    StringBuilder stringBuilder = new StringBuilder();
    switch (_corruption)
    {
      case "altarupgrade":
        string text1 = Texts.Instance.GetText("corruptionAltarUpgrade");
        stringBuilder.Append(text1);
        break;
      case "exoticshop":
        string str1 = this.cDataCorruption.CardRarity != Enums.CardRarity.Rare ? Texts.Instance.GetText("corruptionExoticShopDiscount") : Texts.Instance.GetText("corruptionExoticShop");
        stringBuilder.Append(str1);
        break;
      case "freecardremove":
        string text2 = Texts.Instance.GetText("corruptionRemoveCard");
        stringBuilder.Append(text2);
        break;
      case "freecardremove2":
        string text3 = Texts.Instance.GetText("corruptionRemoveCard2");
        stringBuilder.Append(text3);
        break;
      case "freecardupgrade":
        string text4 = Texts.Instance.GetText("corruptionFreeCardUpgrade");
        stringBuilder.Append(text4);
        break;
      case "goldshards0":
        string text5 = Texts.Instance.GetText("corruptionGainGold");
        if (this.cDataCorruption.CardRarity == Enums.CardRarity.Common)
        {
          int quantity1 = 320;
          int quantity2 = 320;
          int num1 = AtOManager.Instance.ModifyQuantityObeliskTraits(0, quantity1);
          int num2 = AtOManager.Instance.ModifyQuantityObeliskTraits(1, quantity2);
          stringBuilder.Append(string.Format(text5, (object) num1, (object) num2));
          break;
        }
        int quantity3 = 520;
        int quantity4 = 520;
        int num3 = AtOManager.Instance.ModifyQuantityObeliskTraits(0, quantity3);
        int num4 = AtOManager.Instance.ModifyQuantityObeliskTraits(1, quantity4);
        stringBuilder.Append(string.Format(text5, (object) num3, (object) num4));
        break;
      case "goldshards1":
        string text6 = Texts.Instance.GetText("corruptionGainGold2");
        if (this.cDataCorruption.CardRarity == Enums.CardRarity.Rare)
        {
          int quantity5 = 720;
          int quantity6 = 720;
          int num5 = AtOManager.Instance.ModifyQuantityObeliskTraits(0, quantity5);
          int num6 = AtOManager.Instance.ModifyQuantityObeliskTraits(1, quantity6);
          stringBuilder.Append(string.Format(text6, (object) num5, (object) num6, (object) "1"));
          break;
        }
        int quantity7 = 1000;
        int quantity8 = 1000;
        int num7 = AtOManager.Instance.ModifyQuantityObeliskTraits(0, quantity7);
        int num8 = AtOManager.Instance.ModifyQuantityObeliskTraits(1, quantity8);
        stringBuilder.Append(string.Format(text6, (object) num7, (object) num8, (object) "2"));
        break;
      case "heal20":
        string text7 = Texts.Instance.GetText("corruptionHeal20");
        stringBuilder.Append(text7);
        break;
      case "herocard":
        this.textReward.margin = new Vector4(0.0f, 0.0f, 1f, 0.0f);
        this.cardReward.gameObject.SetActive(true);
        this.bgCards.gameObject.SetActive(true);
        this.bgHand.gameObject.SetActive(false);
        string text8 = Texts.Instance.GetText("corruptionHeroFreeCard");
        CardData cardData = Globals.Instance.GetCardData(this.corruptionRewardCard, false);
        string str2 = cardData.CardUpgraded != Enums.CardUpgraded.No ? Globals.Instance.GetCardData(cardData.UpgradedFrom, false).CardName : cardData.CardName;
        stringBuilder.Append(string.Format(text8, (object) this.teamAtO[this.corruptionRewardChar].SourceName, (object) Globals.Instance.ClassColor[Enum.GetName(typeof (Enums.HeroClass), (object) this.teamAtO[this.corruptionRewardChar].HeroData.HeroClass)], (object) str2));
        break;
      case "increasedqualityofcardrewards":
        string text9 = Texts.Instance.GetText("corruptionIncreasedQuality");
        stringBuilder.Append(text9);
        break;
      case "randomcardupgrade":
        string text10 = Texts.Instance.GetText("corruptionRandomCardUpgrade");
        stringBuilder.Append(text10);
        break;
      case "rareshop":
        string str3 = this.cDataCorruption.CardRarity != Enums.CardRarity.Common ? Texts.Instance.GetText("corruptionRareShopDiscount") : Texts.Instance.GetText("corruptionRareShop");
        stringBuilder.Append(str3);
        break;
    }
    return stringBuilder.ToString();
  }

  public bool IsActive() => this.gameObject.activeSelf;

  public void HideButton() => this.corruptionButton.gameObject.SetActive(false);

  public void BoxClicked(bool setStatus = false, bool status = false)
  {
    this.clicked = setStatus ? status : !this.clicked;
    this.botonGenericX.PermaBorder(this.clicked);
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
      AtOManager.Instance.corruptionAccepted = this.clicked;
    this.ShowClicked();
    if (!GameManager.Instance.IsMultiplayer() || !NetworkManager.Instance.IsMaster())
      return;
    this.photonView.RPC("NET_BoxClicked", RpcTarget.Others, (object) this.clicked);
  }

  private void ShowClicked()
  {
    this.corruptionBoxX.gameObject.SetActive(this.clicked);
    this.bgCorruptionOn.gameObject.SetActive(this.clicked);
    if (this.clicked)
      return;
    this.rewardBotB.color = Functions.HexToColor("#FFFFFF");
    this.rewardBotB.SetColor();
    this.rewardBotB.PermaBorder(false);
    this.rewardBotA.color = Functions.HexToColor("#FFFFFF");
    this.rewardBotA.SetColor();
    this.rewardBotA.PermaBorder(false);
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
      return;
    AtOManager.Instance.corruptionId = "";
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    bool shoulderLeft = false,
    bool shoulderRight = false)
  {
    this.controllerList.Clear();
    if ((!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster()) && Functions.TransformIsVisible(this.elements))
    {
      if (Functions.TransformIsVisible(this.cardCorruption.transform))
        this.controllerList.Add(this.cardCorruption.transform);
      this.controllerList.Add(this.rewardBotA.transform);
      this.controllerList.Add(this.rewardBotB.transform);
      if ((UnityEngine.Object) this.cardReward != (UnityEngine.Object) null && this.cardReward.activeSelf)
        this.controllerList.Add(this.cardReward.transform);
      this.controllerList.Add(this.corruptionBoxX.transform);
      this.controllerList.Add(this.corruptionContinue.transform);
    }
    this.controllerList.Add(this.buttonShowText.transform);
    for (int index = 0; index < 4; ++index)
    {
      if (Functions.TransformIsVisible(MapManager.Instance.sideCharacters.charArray[index].transform))
        this.controllerList.Add(MapManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
    }
    if (Functions.TransformIsVisible(PlayerUIManager.Instance.giveGold))
      this.controllerList.Add(PlayerUIManager.Instance.giveGold);
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this.controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this.controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((UnityEngine.Object) this.controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }

  public void ControllerMoveBlock(bool _isRight)
  {
  }
}
