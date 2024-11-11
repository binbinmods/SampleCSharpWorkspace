// Decompiled with JetBrains decompiler
// Type: MapManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
  private PhotonView photonView;
  public Transform sceneCamera;
  public CorruptionManager corruption;
  public Transform worldTransform;
  public Transform trackList;
  public PopupNode popupNode;
  public Transform ItemCreator;
  public Transform maskObject;
  public Transform maskMP;
  public Image maskImage;
  public Node nodeActive;
  private Node nodeDestination;
  private List<string> roadTemp = new List<string>();
  private ZoneData zoneActive;
  private Dictionary<string, GameObject> zoneGOs;
  private Dictionary<string, Node> mapNode;
  private Dictionary<string, bool> canTravelDict;
  private List<string> mapNodes;
  private List<string> availableNodes;
  private bool gettingMapNodes;
  public GameObject sourceTmpRoad;
  public Transform tmpRoads;
  public Material materialTmpRoad;
  private Dictionary<string, Transform> roads;
  private string roadActive = "";
  public SideCharacters sideCharacters;
  public CharacterWindowUI characterWindow;
  public GameObject unlockCharacterPrefab;
  private GameObject unlockGO;
  public GameObject eventTrackPrefab;
  public GameObject eventPrefab;
  public Transform eventShowHideButton;
  public GameObject conflictPrefab;
  private ConflictManager conflict;
  private GameObject conflictGO;
  public MapLegend mapLegend;
  private GameObject eventGO;
  private EventManager eventManager;
  private Coroutine showPopupCo;
  private Coroutine maskCoroutine;
  private List<string> randomStringArr = new List<string>();
  private int randomIndex;
  public TMP_Text tipText;
  private Dictionary<string, string> playerSelectedNodesDict = new Dictionary<string, string>();
  private bool corruptionSetted;
  public bool selectedNode;
  public Transform giveWindow;
  public List<GameObject> mapList;
  private Coroutine followingCo;
  public ChallengeBossBanners challengeBossBanners;
  private List<Transform> availableNodesTransform;
  private List<Transform> instantTravelNodesTransform;
  private int controllerCurrentOption = -1;
  private int controllerShoulderCurrentOption = -1;
  private List<Transform> controllerList = new List<Transform>();
  private Vector2 warpPosition;

  public static MapManager Instance { get; private set; }

  public ConflictManager Conflict
  {
    get => this.conflict;
    set => this.conflict = value;
  }

  public PhotonView GetPhotonView() => this.photonView;

  private void Awake()
  {
    if ((UnityEngine.Object) GameManager.Instance == (UnityEngine.Object) null)
    {
      SceneStatic.LoadByName("Map");
    }
    else
    {
      if ((UnityEngine.Object) MapManager.Instance == (UnityEngine.Object) null)
        MapManager.Instance = this;
      else if ((UnityEngine.Object) MapManager.Instance != (UnityEngine.Object) this)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("Awake Map Manager", "general");
      this.photonView = PhotonView.Get((Component) this);
      this.sceneCamera.gameObject.SetActive(false);
      if (GameManager.Instance.IsObeliskChallenge())
        AtOManager.Instance.SetObeliskNodes();
      this.GetMapNodes(true);
      this.corruption.gameObject.SetActive(false);
      NetworkManager.Instance.StartStopQueue(true);
    }
  }

  private void Start()
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("Begin Map", "general");
    AtOManager.Instance.ClearReroll();
    AtOManager.Instance.ResetCombatScarab();
    this.eventShowHideButton.gameObject.SetActive(false);
    this.ShowMask(true);
    this.StartCoroutine(this.StartCo());
  }

  private IEnumerator StartCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    if (GameManager.Instance.IsMultiplayer())
    {
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("**************************");
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("WaitingSyncro startmap", "net");
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersReady("startmap"))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("Game ready, Everybody checked startmap", "net");
        NetworkManager.Instance.PlayersNetworkContinue("startmap");
      }
      else
      {
        NetworkManager.Instance.SetWaitingSyncro("startmap", true);
        NetworkManager.Instance.SetStatusReady("startmap");
        while (NetworkManager.Instance.WaitingSyncro["startmap"])
          yield return (object) Globals.Instance.WaitForSeconds(0.1f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("startmap, we can continue!", "net");
      }
    }
    int exhaustGettingMapNodes = 0;
    this.controllerCurrentOption = -1;
    for (; this.gettingMapNodes && exhaustGettingMapNodes < 100; ++exhaustGettingMapNodes)
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    if (exhaustGettingMapNodes > 100)
      Debug.LogError((object) "ERROR getting map nodes");
    if (this.CurrentNode() == null || this.CurrentNode() == "")
    {
      AtOManager.Instance.BeginAdventure();
    }
    else
    {
      this.zoneActive = this.mapNode[this.CurrentNode()].nodeData.NodeZone;
      this.zoneGOs[this.zoneActive.ZoneId].SetActive(true);
      if (GameManager.Instance.IsObeliskChallenge())
      {
        if (this.zoneActive.ObeliskHigh)
          this.zoneGOs["ChallengeHighMap"].SetActive(true);
        else if (this.zoneActive.ObeliskFinal)
          this.zoneGOs["ChallengeFinalMap"].SetActive(true);
        else
          this.zoneGOs["ChallengeCommonMap"].SetActive(true);
      }
      AtOManager.Instance.SetCharInTown(false);
      AtOManager.Instance.SetTownZoneId(this.zoneActive.ZoneId);
      this.GetRoads();
      AtOManager.Instance.ClearCombatThermometerData();
      this.BeginMap();
      this.Resize();
    }
  }

  public void BeginMap()
  {
    AtOManager.Instance.NodeScore();
    this.selectedNode = false;
    if (GameManager.Instance.IsMultiplayer())
      this.playerSelectedNodesDict.Clear();
    this.StartCoroutine(this.BeginMapCo());
  }

  private IEnumerator BeginMapCo()
  {
    this.canTravelDict = new Dictionary<string, bool>();
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
    {
      this.AssignGameNodes();
      AtOManager.Instance.saveLoadStatus = true;
      AtOManager.Instance.SaveGame(backUp: true);
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
      while (AtOManager.Instance.saveLoadStatus)
        yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    }
    this.BeginMapContinue();
    yield return (object) null;
  }

  private void BeginMapContinue()
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD(nameof (BeginMapContinue), "general");
    AtOManager.Instance.ClearTeamNPC();
    this.ShowRequirementTracks();
    this.HideAllArrows();
    AtOManager.Instance.SetPositionText();
    this.sideCharacters.EnableAll();
    if (!GameManager.Instance.IsMultiplayer() || GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
      this.GenerateRandomStringBatch(100);
    if (GameManager.Instance.IsMultiplayer())
    {
      this.ClearMultiplayerSelectedNodes();
      this.StartCoroutine(this.WaitMultiplayer());
    }
    else if ((UnityEngine.Object) AtOManager.Instance.fromEventDestinationNode != (UnityEngine.Object) null)
    {
      string nodeId = AtOManager.Instance.fromEventDestinationNode.NodeId;
      AtOManager.Instance.fromEventDestinationNode = (NodeData) null;
      this.TravelToThisNode(this.GetNodeFromId(nodeId));
    }
    else if ((UnityEngine.Object) AtOManager.Instance.fromEventCombatData != (UnityEngine.Object) null)
      this.DoCombat(AtOManager.Instance.fromEventCombatData);
    else if ((UnityEngine.Object) AtOManager.Instance.fromEventEventData != (UnityEngine.Object) null)
    {
      this.DoEvent(AtOManager.Instance.fromEventEventData);
    }
    else
    {
      this.DrawMap();
      this.AssignGameNodes();
      this.BeginMapContinueEnd();
    }
  }

  private void DoMapTips()
  {
    this.tipText.gameObject.SetActive(false);
    List<string> tipsList = Texts.Instance.TipsList;
    UnityEngine.Random.InitState((int) DateTime.Now.Ticks);
    int index = UnityEngine.Random.Range(0, tipsList.Count);
    string str = tipsList[index];
    if (str.Trim() != "")
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<line-height=100%>");
      stringBuilder.Append(str.Trim());
      this.tipText.text = Regex.Replace(stringBuilder.ToString(), "\\<icon=(\\w+)\\>", "<space=1><size=+1.2><voffset=-.5><sprite name=$1><voffset=0></size>");
    }
    this.tipText.gameObject.SetActive(true);
  }

  private void BeginMapContinueEnd()
  {
    if (GameManager.Instance.IsObeliskChallenge())
      this.challengeBossBanners.SetBosses();
    this.DoMapTips();
    PlayerUIManager.Instance.SetItems();
    ProgressManager.Instance.CheckProgress();
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("comingFromCombatDoRewards->" + AtOManager.Instance.comingFromCombatDoRewards.ToString(), "map");
    if (AtOManager.Instance.comingFromCombatDoRewards)
      this.StartCoroutine(this.DoCorruptionReward());
    else if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
      this.ShowMask(false);
    this.controllerList.Clear();
    if (Functions.TransformIsVisible(PlayerUIManager.Instance.giveGold))
      this.controllerList.Add(PlayerUIManager.Instance.giveGold);
    for (int index = 0; index < this.sideCharacters.transform.childCount; ++index)
    {
      if (Functions.TransformIsVisible(this.sideCharacters.transform.GetChild(index).transform))
        this.controllerList.Add(this.sideCharacters.transform.GetChild(index).transform);
    }
    foreach (Transform track in this.trackList)
      this.controllerList.Add(track);
    if (this.instantTravelNodesTransform != null)
    {
      this.controllerList.Add(this.mapNode[this.CurrentNode()].transform);
      for (int index = 0; index < this.instantTravelNodesTransform.Count; ++index)
      {
        if ((UnityEngine.Object) this.instantTravelNodesTransform[index] != (UnityEngine.Object) this.mapNode[this.CurrentNode()].transform)
          this.controllerList.Add(this.instantTravelNodesTransform[index]);
      }
    }
    AudioManager.Instance.DoBSO("Map");
  }

  private IEnumerator DoCorruptionReward()
  {
    MapManager mapManager = this;
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("Corruption Accepted->" + AtOManager.Instance.corruptionAccepted.ToString(), "map");
    if (AtOManager.Instance.corruptionAccepted)
    {
      if (GameManager.Instance.IsMultiplayer())
      {
        yield return (object) Globals.Instance.WaitForSeconds(0.1f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("**************************");
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("WaitingSyncro DoCorruptionReward", "net");
        if (NetworkManager.Instance.IsMaster())
        {
          while (!NetworkManager.Instance.AllPlayersReady(nameof (DoCorruptionReward)))
            yield return (object) Globals.Instance.WaitForSeconds(0.01f);
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD("Game ready, Everybody checked DoCorruptionReward", "net");
          NetworkManager.Instance.PlayersNetworkContinue(nameof (DoCorruptionReward));
        }
        else
        {
          NetworkManager.Instance.SetWaitingSyncro(nameof (DoCorruptionReward), true);
          NetworkManager.Instance.SetStatusReady(nameof (DoCorruptionReward));
          while (NetworkManager.Instance.WaitingSyncro[nameof (DoCorruptionReward)])
            yield return (object) Globals.Instance.WaitForSeconds(0.1f);
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD("DoCorruptionReward, we can continue!", "net");
        }
      }
      PlayerManager.Instance.CorruptionCompleted();
      string corruptionId = AtOManager.Instance.corruptionId;
      string shopListId = "";
      CardData cDataCorruption = Globals.Instance.GetCardData(AtOManager.Instance.corruptionIdCard);
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("[CorruptionRewardId] " + corruptionId);
      switch (corruptionId)
      {
        case "altarupgrade":
          mapManager.sideCharacters.InCharacterScreen(true);
          yield return (object) Globals.Instance.WaitForSeconds(0.2f);
          AtOManager.Instance.DoCardUpgrade(maxQuantity: 0);
          break;
        case "exoticshop":
          mapManager.sideCharacters.InCharacterScreen(true);
          shopListId = "Exoticshop_Tier" + AtOManager.Instance.GetTownTier().ToString();
          AtOManager.Instance.RemoveItemList(shopListId);
          yield return (object) Globals.Instance.WaitForSeconds(0.2f);
          if (cDataCorruption.CardRarity == Enums.CardRarity.Rare)
          {
            AtOManager.Instance.DoItemShop(shopListId);
            break;
          }
          AtOManager.Instance.DoItemShop(shopListId, 30);
          break;
        case "freecardremove":
          mapManager.sideCharacters.InCharacterScreen(true);
          yield return (object) Globals.Instance.WaitForSeconds(0.2f);
          AtOManager.Instance.DoCardHealer(100, 1);
          break;
        case "freecardremove2":
          mapManager.sideCharacters.InCharacterScreen(true);
          yield return (object) Globals.Instance.WaitForSeconds(0.2f);
          AtOManager.Instance.DoCardHealer(100, 2);
          break;
        case "freecardupgrade":
          mapManager.sideCharacters.InCharacterScreen(true);
          yield return (object) Globals.Instance.WaitForSeconds(0.2f);
          AtOManager.Instance.DoCardUpgrade(100, 1);
          break;
        case "goldshards0":
          if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
          {
            Character[] team = (Character[]) AtOManager.Instance.GetTeam();
            if (cDataCorruption.CardRarity == Enums.CardRarity.Common)
            {
              for (int index = 0; index < 4; ++index)
              {
                if (team[index] != null && (UnityEngine.Object) team[index].HeroData != (UnityEngine.Object) null)
                {
                  AtOManager.Instance.GivePlayer(0, AtOManager.Instance.ModifyQuantityObeliskTraits(0, 80), team[index].Owner);
                  AtOManager.Instance.GivePlayer(1, AtOManager.Instance.ModifyQuantityObeliskTraits(1, 80), team[index].Owner);
                }
              }
              break;
            }
            for (int index = 0; index < 4; ++index)
            {
              if (team[index] != null && (UnityEngine.Object) team[index].HeroData != (UnityEngine.Object) null)
              {
                AtOManager.Instance.GivePlayer(0, AtOManager.Instance.ModifyQuantityObeliskTraits(0, 130), team[index].Owner);
                AtOManager.Instance.GivePlayer(1, AtOManager.Instance.ModifyQuantityObeliskTraits(1, 130), team[index].Owner);
              }
            }
            break;
          }
          break;
        case "goldshards1":
          if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
          {
            Character[] team = (Character[]) AtOManager.Instance.GetTeam();
            if (cDataCorruption.CardRarity == Enums.CardRarity.Rare)
            {
              for (int index = 0; index < 4; ++index)
              {
                if (team[index] != null && (UnityEngine.Object) team[index].HeroData != (UnityEngine.Object) null)
                {
                  AtOManager.Instance.GivePlayer(0, AtOManager.Instance.ModifyQuantityObeliskTraits(0, 180), team[index].Owner);
                  AtOManager.Instance.GivePlayer(1, AtOManager.Instance.ModifyQuantityObeliskTraits(1, 180), team[index].Owner);
                }
              }
              if (!GameManager.Instance.IsMultiplayer())
              {
                AtOManager.Instance.GivePlayer(2, 1, team[0].Owner);
                break;
              }
              for (int position = 0; position < NetworkManager.Instance.GetNumPlayers(); ++position)
                AtOManager.Instance.GivePlayer(2, 1, NetworkManager.Instance.GetPlayerNickPosition(position));
              break;
            }
            for (int index = 0; index < 4; ++index)
            {
              if (team[index] != null && (UnityEngine.Object) team[index].HeroData != (UnityEngine.Object) null)
              {
                AtOManager.Instance.GivePlayer(0, AtOManager.Instance.ModifyQuantityObeliskTraits(0, 250), team[index].Owner);
                AtOManager.Instance.GivePlayer(1, AtOManager.Instance.ModifyQuantityObeliskTraits(1, 250), team[index].Owner);
              }
            }
            if (!GameManager.Instance.IsMultiplayer())
            {
              AtOManager.Instance.GivePlayer(2, 2, team[0].Owner);
              break;
            }
            for (int position = 0; position < NetworkManager.Instance.GetNumPlayers(); ++position)
              AtOManager.Instance.GivePlayer(2, 2, NetworkManager.Instance.GetPlayerNickPosition(position));
            break;
          }
          break;
        case "heal20":
          if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
          {
            AtOManager.Instance.ModifyHeroLife(_percent: 30f);
            if (GameManager.Instance.IsMultiplayer())
            {
              mapManager.StartCoroutine(AtOManager.Instance.ShareTeam());
              break;
            }
            break;
          }
          break;
        case "herocard":
          if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
          {
            if (!GameManager.Instance.IsMultiplayer())
            {
              AtOManager.Instance.AddCardToHero(AtOManager.Instance.corruptionRewardChar, AtOManager.Instance.corruptionRewardCard);
              break;
            }
            AtOManager.Instance.AddCardToHeroMP(AtOManager.Instance.corruptionRewardChar, AtOManager.Instance.corruptionRewardCard);
            break;
          }
          break;
        case "randomcardupgrade":
          if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
          {
            Character[] team = (Character[]) AtOManager.Instance.GetTeam();
            for (int _heroIndex = 0; _heroIndex < 4; ++_heroIndex)
            {
              if (team[_heroIndex] != null && (UnityEngine.Object) team[_heroIndex].HeroData != (UnityEngine.Object) null)
                AtOManager.Instance.UpgradeRandomCardToHero(_heroIndex);
            }
            if (AtOManager.Instance.upgradedCardsList != null && AtOManager.Instance.upgradedCardsList.Count > 0)
            {
              if (GameManager.Instance.IsMultiplayer())
              {
                string json = JsonHelper.ToJson<string>(AtOManager.Instance.upgradedCardsList.ToArray());
                mapManager.photonView.RPC("NET_ShareUpgradedCards", RpcTarget.Others, (object) json);
              }
              mapManager.characterWindow.ShowUpgradedCards(AtOManager.Instance.upgradedCardsList);
              break;
            }
            break;
          }
          break;
        case "rareshop":
          mapManager.sideCharacters.InCharacterScreen(true);
          shopListId = "Rareshop_Tier" + AtOManager.Instance.GetTownTier().ToString();
          AtOManager.Instance.RemoveItemList(shopListId);
          yield return (object) Globals.Instance.WaitForSeconds(0.2f);
          if (cDataCorruption.CardRarity == Enums.CardRarity.Common)
          {
            AtOManager.Instance.DoItemShop(shopListId);
            break;
          }
          AtOManager.Instance.DoItemShop(shopListId, 30);
          break;
      }
      if (!GameManager.Instance.IsMultiplayer())
        mapManager.sideCharacters.Refresh();
      shopListId = (string) null;
      cDataCorruption = (CardData) null;
    }
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("end corruption check in map", "map");
    AtOManager.Instance.comingFromCombatDoRewards = false;
    AtOManager.Instance.ClearCorruption();
    AtOManager.Instance.SaveGame();
    GameManager.Instance.SceneLoaded();
    mapManager.ShowMask(false);
  }

  [PunRPC]
  public void NET_ShareUpgradedCards(string upgradedCards)
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("NET_ShareUpgradedCards " + upgradedCards);
    List<string> upgradedCards1 = new List<string>();
    upgradedCards1.AddRange((IEnumerable<string>) JsonHelper.FromJson<string>(upgradedCards));
    this.characterWindow.ShowUpgradedCards(upgradedCards1);
  }

  private IEnumerator WaitMultiplayer()
  {
    string key = this.CurrentNode();
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("**************************", "net");
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("WaitingSyncro mapmanager" + key, "net");
    if (NetworkManager.Instance.IsMaster())
    {
      while (!NetworkManager.Instance.AllPlayersReady("mapmanager" + key))
        yield return (object) Globals.Instance.WaitForSeconds(0.01f);
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("Game ready, Everybody checked mapmanager" + key, "net");
      NetworkManager.Instance.PlayersNetworkContinue("mapmanager" + key);
      if ((UnityEngine.Object) AtOManager.Instance.fromEventDestinationNode != (UnityEngine.Object) null)
      {
        string nodeId = AtOManager.Instance.fromEventDestinationNode.NodeId;
        AtOManager.Instance.fromEventDestinationNode = (NodeData) null;
        this.TravelToThisNode(this.GetNodeFromId(nodeId));
        yield break;
      }
      else if ((UnityEngine.Object) AtOManager.Instance.fromEventCombatData != (UnityEngine.Object) null)
      {
        this.DoCombat(AtOManager.Instance.fromEventCombatData);
        yield break;
      }
      else if ((UnityEngine.Object) AtOManager.Instance.fromEventEventData != (UnityEngine.Object) null)
      {
        this.DoEvent(AtOManager.Instance.fromEventEventData);
        yield break;
      }
      else
      {
        this.DrawMap();
        this.ShareAssignGameNodes();
      }
    }
    else
    {
      NetworkManager.Instance.SetWaitingSyncro("mapmanager" + key, true);
      NetworkManager.Instance.SetStatusReady("mapmanager" + key);
      while (NetworkManager.Instance.WaitingSyncro["mapmanager" + key])
        yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("mapmanager" + key + ", we can continue!", "net");
      if ((UnityEngine.Object) AtOManager.Instance.fromEventDestinationNode != (UnityEngine.Object) null)
      {
        string nodeId = AtOManager.Instance.fromEventDestinationNode.NodeId;
        AtOManager.Instance.fromEventDestinationNode = (NodeData) null;
        this.TravelToThisNode(this.GetNodeFromId(nodeId));
        this.ShowMask(false);
        yield break;
      }
    }
    this.BeginMapContinueEnd();
  }

  public void CleanFromEvent()
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("[MAPMANAGER] CleanFromEvent", "map");
    AtOManager.Instance.fromEventCombatData = (CombatData) null;
    AtOManager.Instance.fromEventEventData = (EventData) null;
  }

  private void DrawMap()
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD(nameof (DrawMap), "map");
    this.ShowWorld();
    this.availableNodes = new List<string>();
    this.instantTravelNodesTransform = new List<Transform>();
    this.availableNodesTransform = new List<Transform>();
    this.GetAvailableNodes(this.mapNode[this.CurrentNode()].nodeData);
    this.DrawNodes();
    this.SetPositionInCurrentNode();
    this.ShowTMPRoads();
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
    {
      CombatData currentCombatData = AtOManager.Instance.GetCurrentCombatData();
      if ((UnityEngine.Object) currentCombatData != (UnityEngine.Object) null)
      {
        if ((UnityEngine.Object) currentCombatData.EventData != (UnityEngine.Object) null && ((UnityEngine.Object) currentCombatData.EventRequirementData == (UnityEngine.Object) null || AtOManager.Instance.PlayerHasRequirement(currentCombatData.EventRequirementData)))
          this.DoEvent(currentCombatData.EventData);
        AtOManager.Instance.ClearCurrentCombatData();
      }
    }
    GameManager.Instance.SceneLoaded();
    if ((UnityEngine.Object) AtOManager.Instance.characterUnlockData != (UnityEngine.Object) null)
      this.CharacterUnlock();
    if (!((UnityEngine.Object) AtOManager.Instance.skinUnlockData != (UnityEngine.Object) null))
      return;
    this.SkinUnlock();
  }

  private void ShowRequirementTracks()
  {
    foreach (Component track in this.trackList)
      UnityEngine.Object.Destroy((UnityEngine.Object) track.gameObject);
    int num = 0;
    List<string> playerRequeriments = AtOManager.Instance.GetPlayerRequeriments();
    for (int index = 0; index < playerRequeriments.Count; ++index)
    {
      EventRequirementData requirementData = Globals.Instance.GetRequirementData(playerRequeriments[index]);
      if (requirementData.RequirementTrack && requirementData.CanShowRequeriment(AtOManager.Instance.GetMapZone(AtOManager.Instance.currentMapNode), Enums.Zone.None))
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.eventTrackPrefab, Vector3.zero, Quaternion.identity, this.trackList);
        gameObject.transform.localPosition = new Vector3(0.0f, -0.55f * (float) num, 0.0f);
        gameObject.transform.GetComponent<EventTrack>().SetTrack(playerRequeriments[index]);
        ++num;
      }
    }
  }

  public void Resize()
  {
    this.sideCharacters.Resize();
    this.mapLegend.Resize();
    this.characterWindow.Resize();
    this.trackList.transform.localPosition = new Vector3((float) (-(double) Globals.Instance.sizeW * 0.5 + 2.0 * (double) Globals.Instance.multiplierX), (float) (-(double) Globals.Instance.sizeH * 0.5 + 3.75 * (double) Globals.Instance.multiplierY), this.trackList.transform.localPosition.z);
  }

  public void ShowCharacterWindow(string type = "", int heroIndex = -1)
  {
    this.HidePopup();
    this.characterWindow.Show(type, heroIndex);
  }

  public void ShowDeck(int auxInt) => this.characterWindow.Show("deck", auxInt);

  public void HideCharacterWindow()
  {
  }

  private void GetRoads()
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD(nameof (GetRoads), "map");
    this.roads = new Dictionary<string, Transform>();
    foreach (Transform transform1 in this.worldTransform)
    {
      if (!(transform1.gameObject.name != AtOManager.Instance.GetTownZoneId()))
      {
        foreach (Transform transform2 in transform1)
        {
          if (transform2.gameObject.name == "Roads")
          {
            foreach (Transform transform3 in transform2)
              this.roads.Add(transform3.gameObject.name.ToLower().Trim(), transform3);
          }
        }
      }
    }
  }

  public Dictionary<string, Node> GetMapNodeDict() => this.mapNode;

  private void GetMapNodes(bool useDelay = false) => this.StartCoroutine(this.GetMapNodesCo(useDelay));

  private IEnumerator GetMapNodesCo(bool useDelay)
  {
    this.gettingMapNodes = true;
    string nodeId = this.CurrentNode();
    if (!(nodeId == ""))
    {
      this.IncludeMapPrefab(nodeId);
      this.mapNode = new Dictionary<string, Node>();
      this.mapNodes = new List<string>();
      this.zoneGOs = new Dictionary<string, GameObject>();
      foreach (Transform transform1 in this.worldTransform)
      {
        GameObject gameObject = transform1.gameObject;
        if (!this.zoneGOs.ContainsKey(gameObject.name))
          this.zoneGOs.Add(gameObject.name, gameObject);
        gameObject.SetActive(false);
        string lower1 = gameObject.name.ToLower();
        if (GameManager.Instance.IsObeliskChallenge())
        {
          if (lower1 != "challengecommonmap" && lower1 != "challengehighmap" && lower1 != "challengefinalmap" && lower1 != AtOManager.Instance.obeliskLow && lower1 != AtOManager.Instance.obeliskHigh && lower1 != AtOManager.Instance.obeliskFinal)
            continue;
        }
        else if (lower1 == "challengecommonmap" || lower1 == "challengehighmap" || lower1 == "challengefinalmap" || lower1.Substring(0, 2) == "ol" || lower1.Substring(0, 2) == "oh" || lower1.Substring(0, 2) == "of")
          continue;
        foreach (Transform transform2 in transform1)
        {
          if (transform2.gameObject.name == "Nodes")
          {
            foreach (Transform transform3 in transform2)
            {
              Node component = transform3.GetComponent<Node>();
              component.InitNode();
              string lower2 = component.nodeData.NodeId.ToLower();
              this.mapNodes.Add(lower2);
              if (AtOManager.Instance.mapVisitedNodes.Contains(lower2))
                component.SetVisited();
              this.mapNode.Add(transform3.gameObject.name.ToLower(), component);
            }
            if (useDelay)
              yield return (object) Globals.Instance.WaitForSeconds(1f / 1000f);
          }
          if (useDelay)
            yield return (object) Globals.Instance.WaitForSeconds(1f / 1000f);
        }
        if (useDelay)
          yield return (object) Globals.Instance.WaitForSeconds(1f / 1000f);
      }
      this.gettingMapNodes = false;
    }
  }

  public void IncludeObeliskBgs()
  {
    for (int index = 0; index < this.mapList.Count; ++index)
    {
      if (this.mapList[index].name == "ChallengeCommonMap")
        UnityEngine.Object.Instantiate<GameObject>(this.mapList[index], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, this.worldTransform).name = "ChallengeCommonMap";
      else if (this.mapList[index].name == "ChallengeHighMap")
        UnityEngine.Object.Instantiate<GameObject>(this.mapList[index], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, this.worldTransform).name = "ChallengeHighMap";
      else if (this.mapList[index].name == "ChallengeFinalMap")
        UnityEngine.Object.Instantiate<GameObject>(this.mapList[index], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, this.worldTransform).name = "ChallengeFinalMap";
    }
  }

  public bool IncludeMapPrefab(string nodeId)
  {
    if (nodeId == "")
      return false;
    string zoneId = Globals.Instance.GetNodeData(nodeId).NodeZone.ZoneId;
    bool flag1 = false;
    for (int index1 = 0; index1 < this.mapList.Count; ++index1)
    {
      if (this.mapList[index1].name.ToLower() == zoneId.ToLower())
      {
        bool flag2 = false;
        for (int index2 = 0; index2 < this.worldTransform.childCount; ++index2)
        {
          if (this.worldTransform.GetChild(index2).gameObject.name == this.mapList[index1].name)
          {
            flag2 = true;
            break;
          }
        }
        if (!flag2)
        {
          UnityEngine.Object.Instantiate<GameObject>(this.mapList[index1], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, this.worldTransform).name = this.mapList[index1].name;
          flag1 = true;
        }
      }
    }
    return flag1;
  }

  public Node GetNodeFromId(string nodeId)
  {
    if (this.IncludeMapPrefab(nodeId))
      this.GetMapNodes();
    return this.mapNode.ContainsKey(nodeId) ? this.mapNode[nodeId] : (Node) null;
  }

  private void DrawNodes()
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("Draw Nodes", "map");
    if (GameManager.Instance.IsObeliskChallenge())
      AtOManager.Instance.GenerateObeliskMap();
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("+++ Draw Nodes +++", "general");
    for (int index = 0; index < this.mapNodes.Count; ++index)
    {
      Node node = this.mapNode[this.mapNodes[index]];
      if (AtOManager.Instance.currentMapNode == "tutorial_0" || AtOManager.Instance.currentMapNode == "tutorial_1" || AtOManager.Instance.currentMapNode == "tutorial_2")
      {
        if (node.nodeData.NodeId != "tutorial_0" && node.nodeData.NodeId != "tutorial_1" && node.nodeData.NodeId != "tutorial_2" && node.nodeData.NodeId != "sen_41")
        {
          node.gameObject.SetActive(false);
          continue;
        }
      }
      else if (node.nodeData.NodeId == "tutorial_0" || node.nodeData.NodeId == "tutorial_1" || node.nodeData.NodeId == "tutorial_2")
      {
        node.gameObject.SetActive(false);
        continue;
      }
      if (!AtOManager.Instance.gameNodeAssigned.ContainsKey(node.nodeData.NodeId) || (UnityEngine.Object) node.nodeData.NodeRequirement != (UnityEngine.Object) null && !AtOManager.Instance.PlayerHasRequirement(node.nodeData.NodeRequirement) && !node.nodeData.VisibleIfNotRequirement)
      {
        node.SetHidden();
      }
      else
      {
        if (!this.availableNodes.Contains(this.mapNodes[index]) && this.mapNodes[index] != this.CurrentNode())
          node.SetBlocked();
        else
          node.SetPlain();
        if (!node.gameObject.activeSelf)
          node.gameObject.SetActive(true);
        this.availableNodesTransform.Add(node.transform);
      }
    }
    for (int index = 0; index < AtOManager.Instance.mapVisitedNodes.Count; ++index)
    {
      if (AtOManager.Instance.mapVisitedNodes[index] != "" && this.mapNode.ContainsKey(AtOManager.Instance.mapVisitedNodes[index]))
        this.mapNode[AtOManager.Instance.mapVisitedNodes[index]].SetVisited();
    }
  }

  private void AssignGameNodes()
  {
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
      return;
    bool flag = true;
    if (!GameManager.Instance.IsObeliskChallenge())
      flag = true;
    else if (AtOManager.Instance.gameNodeAssigned.Count > 0)
      flag = false;
    string gameId = AtOManager.Instance.GetGameId();
    UnityEngine.Random.InitState(gameId.GetDeterministicHashCode());
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("AssignGameNodes -> " + gameId, "map");
    for (int index = 0; index < this.mapNode.Count; ++index)
    {
      Node _node = this.mapNode.ElementAt<KeyValuePair<string, Node>>(index).Value;
      if (AtOManager.Instance.gameNodeAssigned.ContainsKey(_node.nodeData.NodeId) && AtOManager.Instance.mapVisitedNodes.Contains(_node.nodeData.NodeId))
      {
        _node.AssignBackground();
      }
      else
      {
        if (flag)
          AtOManager.Instance.AssignSingleGameNode(_node);
        _node.AssignNode();
      }
    }
    if (!GameManager.Instance.IsObeliskChallenge())
      return;
    AtOManager.Instance.SetObeliskBosses();
  }

  private void ShareAssignGameNodes()
  {
    this.AssignGameNodesNode();
    string[] array1 = new string[AtOManager.Instance.gameNodeAssigned.Count];
    AtOManager.Instance.gameNodeAssigned.Keys.CopyTo(array1, 0);
    string[] array2 = new string[AtOManager.Instance.gameNodeAssigned.Count];
    AtOManager.Instance.gameNodeAssigned.Values.CopyTo(array2, 0);
    string json1 = JsonHelper.ToJson<string>(array1);
    string json2 = JsonHelper.ToJson<string>(array2);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(json1);
    stringBuilder.Append("|");
    stringBuilder.Append(json2);
    this.photonView.RPC("NET_AssignGameNodes", RpcTarget.Others, (object) Functions.CompressString(stringBuilder.ToString()));
  }

  [PunRPC]
  private void NET_AssignGameNodes(string _compressed)
  {
    string[] strArray1 = Functions.DecompressString(_compressed).Split('|', StringSplitOptions.None);
    string[] strArray2 = JsonHelper.FromJson<string>(strArray1[0]);
    string[] strArray3 = JsonHelper.FromJson<string>(strArray1[1]);
    AtOManager.Instance.gameNodeAssigned = new Dictionary<string, string>();
    for (int index = 0; index < strArray2.Length; ++index)
      AtOManager.Instance.gameNodeAssigned.Add(strArray2[index], strArray3[index]);
    this.DrawMap();
    this.AssignGameNodesNode();
    this.ShowMask(false);
  }

  private void AssignGameNodesNode()
  {
    for (int index = 0; index < this.mapNode.Count; ++index)
    {
      Node node = this.mapNode.ElementAt<KeyValuePair<string, Node>>(index).Value;
      if (this.availableNodes == null)
        this.availableNodes = new List<string>();
      node.AssignNode();
    }
  }

  private void GetAvailableNodes(NodeData _nodeData)
  {
    for (int index = 0; index < _nodeData.NodesConnected.Length; ++index)
    {
      if (_nodeData.NodesConnected[index].NodeId != _nodeData.NodeId && this.CanTravelToThisNode(this.mapNode[_nodeData.NodesConnected[index].NodeId], this.mapNode[_nodeData.NodeId]))
      {
        this.availableNodes.Add(_nodeData.NodesConnected[index].NodeId);
        this.GetAvailableNodes(_nodeData.NodesConnected[index]);
      }
    }
  }

  private string CurrentNode() => AtOManager.Instance.currentMapNode;

  private void SetPositionInCurrentNode()
  {
    string str = this.CurrentNode();
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("SetPositionInCurrentNode " + str, "map");
    Node node = this.mapNode[str];
    this.nodeActive = node;
    node.SetActive();
    this.zoneActive = node.nodeData.NodeZone;
    if (GameManager.Instance.IsObeliskChallenge())
      this.DrawAllArrows();
    this.DrawArrowActive(true);
    if (str == "aqua_16")
      this.StartCoroutine(this.ChangeZoneBecauseStuck(str));
    if (str == "spider_8" && AtOManager.Instance.PlayerHasRequirement(Globals.Instance.GetRequirementData("spidercaveout")) && !this.NodeExists(Globals.Instance.GetNodeData("spider_9")))
    {
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("Exit", "map");
      this.StartCoroutine(this.ChangeZoneBecauseStuck(str));
    }
    if (str == "secta_5" && AtOManager.Instance.PlayerHasRequirement(Globals.Instance.GetRequirementData("belphyordead")) && !this.NodeExists(Globals.Instance.GetNodeData("secta_8")))
    {
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("Exit", "map");
      this.StartCoroutine(this.ChangeZoneBecauseStuck(str));
    }
    if (!(str == "velka_33"))
      return;
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("Exit", "map");
    this.StartCoroutine(this.ChangeZoneBecauseStuck(str));
  }

  private IEnumerator ChangeZoneBecauseStuck(string nodeName)
  {
    if (GameManager.Instance.IsMultiplayer())
    {
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("**************************", "net");
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("WaitingSyncro changeZoneBecauseStuck" + nodeName, "net");
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersReady("changeZoneBecauseStuck" + nodeName))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("Game ready, Everybody checked changeZoneBecauseStuck" + nodeName, "net");
        NetworkManager.Instance.PlayersNetworkContinue("changeZoneBecauseStuck" + nodeName);
      }
      else
      {
        NetworkManager.Instance.SetWaitingSyncro("changeZoneBecauseStuck" + nodeName, true);
        NetworkManager.Instance.SetStatusReady("changeZoneBecauseStuck" + nodeName);
        while (NetworkManager.Instance.WaitingSyncro["changeZoneBecauseStuck" + nodeName])
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("changeZoneBecauseStuck" + nodeName + ", we can continue!", "net");
      }
    }
    switch (nodeName)
    {
      case "spider_8":
        this.TravelToThisNode(this.GetNodeFromId("aqua_32"));
        break;
      case "secta_5":
        this.TravelToThisNode(this.GetNodeFromId("sen_12"));
        break;
      case "aqua_16":
        this.TravelToThisNode(this.GetNodeFromId("spider_1"));
        break;
      case "velka_33":
        if (AtOManager.Instance.mapVisitedNodes.Contains(nodeName))
          AtOManager.Instance.mapVisitedNodes.Remove(nodeName);
        this.TravelToThisNode(this.GetNodeFromId("velka_33"));
        break;
    }
  }

  private void DrawArrowActive(bool iconHover = false)
  {
    if ((UnityEngine.Object) this.nodeActive == (UnityEngine.Object) null)
      return;
    NodeData nodeData = this.nodeActive.nodeData;
    for (int index = 0; index < nodeData.NodesConnected.Length; ++index)
    {
      Node node = this.mapNode[nodeData.NodesConnected[index].NodeId];
      if (this.CanTravelToThisNode(node))
      {
        this.DrawArrow(this.nodeActive, node);
        if (iconHover)
          node.SetAvailable();
        this.instantTravelNodesTransform.Add(node.transform);
      }
    }
    this.instantTravelNodesTransform.Add(this.mapNode[this.CurrentNode()].transform);
  }

  private void DrawAllArrows()
  {
    foreach (Component tmpRoad in this.tmpRoads)
      UnityEngine.Object.Destroy((UnityEngine.Object) tmpRoad.gameObject);
    foreach (KeyValuePair<string, Node> keyValuePair in this.mapNode)
    {
      Node _nodeSource = keyValuePair.Value;
      if (_nodeSource.availableT.gameObject.activeSelf || _nodeSource.plainT.gameObject.activeSelf)
        this.DrawArrowsTempObelisk(_nodeSource);
    }
  }

  public void DrawArrow(
    Node _nodeSource,
    Node _nodeDestination,
    bool highlight = false,
    bool temp = false,
    bool challenge = false)
  {
    string key = _nodeSource.nodeData.NodeId + "-" + _nodeDestination.nodeData.NodeId;
    if (this.roads.ContainsKey(key))
    {
      Transform transform = !challenge ? this.roads[key] : UnityEngine.Object.Instantiate<Transform>(this.roads[key], this.tmpRoads).transform;
      transform.gameObject.SetActive(true);
      LineRenderer component = transform.GetComponent<LineRenderer>();
      if (challenge)
      {
        component.materials = new Material[1];
        component.material = this.materialTmpRoad;
        component.widthMultiplier = 0.04f;
        LineRenderer lineRenderer1 = component;
        LineRenderer lineRenderer2 = component;
        Color color1 = new Color(1f, 1f, 1f, 0.65f);
        Color color2 = color1;
        lineRenderer2.endColor = color2;
        Color color3 = color1;
        lineRenderer1.startColor = color3;
      }
      else if (highlight)
      {
        component.startColor = Globals.Instance.MapArrow;
        component.endColor = Globals.Instance.MapArrowHighlight;
      }
      else if (temp)
      {
        if (GameManager.Instance.IsObeliskChallenge())
        {
          component.startColor = Globals.Instance.MapArrowTempChallenge;
          component.endColor = Globals.Instance.MapArrowTempChallenge;
        }
        else
        {
          component.startColor = Globals.Instance.MapArrowTemp;
          component.endColor = Globals.Instance.MapArrowTemp;
        }
        this.roadTemp.Add(key);
      }
      else
      {
        component.startColor = Globals.Instance.MapArrow;
        component.endColor = Globals.Instance.MapArrow;
      }
    }
    this.roadActive = key;
  }

  public void DrawArrowsTemp(Node _nodeSource)
  {
    foreach (KeyValuePair<string, Node> keyValuePair in this.mapNode)
    {
      Node _nodeSource1 = keyValuePair.Value;
      for (int index = 0; index < _nodeSource1.nodeData.NodesConnected.Length; ++index)
      {
        if (_nodeSource1.nodeData.NodesConnected[index].NodeId == _nodeSource.nodeData.NodeId && this.mapNode[_nodeSource1.nodeData.NodeId].gameObject.activeSelf && this.CanTravelToThisNode(_nodeSource, _nodeSource1))
          this.DrawArrow(_nodeSource1, _nodeSource, temp: true);
      }
    }
    for (int index = 0; index < _nodeSource.nodeData.NodesConnected.Length; ++index)
    {
      Node node = this.mapNode[_nodeSource.nodeData.NodesConnected[index].NodeId];
      if (this.CanTravelToThisNode(node, _nodeSource))
        this.DrawArrow(_nodeSource, node, temp: true);
    }
  }

  public void DrawArrowsTempObelisk(Node _nodeSource)
  {
    foreach (KeyValuePair<string, Node> keyValuePair in this.mapNode)
    {
      Node _nodeSource1 = keyValuePair.Value;
      for (int index = 0; index < _nodeSource1.nodeData.NodesConnected.Length; ++index)
      {
        if (_nodeSource1.nodeData.NodesConnected[index].NodeId == _nodeSource.nodeData.NodeId && this.mapNode[_nodeSource1.nodeData.NodeId].gameObject.activeSelf && !(AtOManager.Instance.currentMapNode == _nodeSource1.nodeData.NodeId) && !_nodeSource1.blockedT.gameObject.activeSelf && this.CanTravelToThisNode(_nodeSource, _nodeSource1))
          this.DrawArrow(_nodeSource1, _nodeSource, challenge: true);
      }
    }
  }

  public void HideArrowsTemp(Node _nodeSource)
  {
    for (int index = 0; index < this.roadTemp.Count; ++index)
      this.roads[this.roadTemp[index]].gameObject.SetActive(false);
    this.roadTemp.Clear();
    this.DrawArrowActive();
  }

  private void HideAllArrows()
  {
    for (int index = 0; index < this.roads.Count; ++index)
      this.roads.ElementAt<KeyValuePair<string, Transform>>(index).Value.gameObject.SetActive(false);
  }

  public void HideArrow()
  {
    if (this.roads.ContainsKey(this.roadActive))
      this.roads[this.roadActive].gameObject.SetActive(false);
    this.roadActive = "";
  }

  public bool NodeExists(NodeData _nodeData) => !((UnityEngine.Object) _nodeData == (UnityEngine.Object) null) && AtOManager.Instance.gameNodeAssigned.ContainsKey(_nodeData.NodeId);

  public bool NodeVisible(NodeData _nodeData) => !((UnityEngine.Object) _nodeData.NodeRequirement != (UnityEngine.Object) null) || AtOManager.Instance.PlayerHasRequirement(_nodeData.NodeRequirement);

  public bool CanTravelToThisNode(Node _node, Node _nodeSource = null)
  {
    if ((UnityEngine.Object) _nodeSource == (UnityEngine.Object) null)
      _nodeSource = this.nodeActive;
    if ((UnityEngine.Object) _nodeSource == (UnityEngine.Object) null)
      return false;
    string key = _nodeSource.nodeData.NodeId + "_" + _node.nodeData.NodeId;
    if (this.canTravelDict.ContainsKey(key))
      return this.canTravelDict[key];
    if (!AtOManager.Instance.gameNodeAssigned.ContainsKey(_node.nodeData.NodeId) || !AtOManager.Instance.gameNodeAssigned.ContainsKey(_nodeSource.nodeData.NodeId) || (UnityEngine.Object) _node.nodeData.NodeRequirement != (UnityEngine.Object) null && !AtOManager.Instance.PlayerHasRequirement(_node.nodeData.NodeRequirement))
    {
      this.canTravelDict.Add(key, false);
      return false;
    }
    foreach (NodeData nodeData in _nodeSource.nodeData.NodesConnected)
    {
      if (nodeData.NodeId == _node.nodeData.NodeId)
      {
        if (_nodeSource.nodeData.NodesConnectedRequirement != null)
        {
          for (int index = 0; index < _nodeSource.nodeData.NodesConnectedRequirement.Length; ++index)
          {
            if (_nodeSource.nodeData.NodesConnectedRequirement[index].NodeData.NodeId == _node.nodeData.NodeId && ((UnityEngine.Object) _nodeSource.nodeData.NodesConnectedRequirement[index].ConectionRequeriment != (UnityEngine.Object) null && !AtOManager.Instance.PlayerHasRequirement(_nodeSource.nodeData.NodesConnectedRequirement[index].ConectionRequeriment) || (UnityEngine.Object) _nodeSource.nodeData.NodesConnectedRequirement[index].ConectionIfNotNode != (UnityEngine.Object) null && this.NodeExists(_nodeSource.nodeData.NodesConnectedRequirement[index].ConectionIfNotNode) && this.NodeVisible(_nodeSource.nodeData.NodesConnectedRequirement[index].ConectionIfNotNode)))
            {
              this.canTravelDict.Add(key, false);
              return false;
            }
          }
        }
        this.canTravelDict.Add(key, true);
        return true;
      }
    }
    this.canTravelDict.Add(key, false);
    return false;
  }

  public void ChangeZone(string nodeDest)
  {
    ZoneData nodeZone = this.GetNodeFromId(nodeDest).nodeData.NodeZone;
    if ((UnityEngine.Object) nodeZone == (UnityEngine.Object) null || (UnityEngine.Object) this.zoneActive == (UnityEngine.Object) null)
      return;
    if (nodeZone.ZoneId != this.zoneActive.ZoneId)
    {
      this.zoneGOs[this.zoneActive.ZoneId].SetActive(false);
      AtOManager.Instance.TravelBetweenZones(this.zoneActive, nodeZone);
    }
    else
      this.BeginMap();
  }

  public void PlayerSelectedNode(Node _node, bool _fromFollowTheLeader = false)
  {
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster() && AtOManager.Instance.followingTheLeader && !_fromFollowTheLeader || this.selectedNode)
      return;
    this.selectedNode = true;
    if (!GameManager.Instance.IsMultiplayer())
    {
      this.TravelToThisNode(_node);
    }
    else
    {
      if (this.followingCo != null)
        this.StopCoroutine(this.followingCo);
      this.followingCo = this.StartCoroutine(this.FollowCoroutine(NetworkManager.Instance.GetPlayerNick(), _node.nodeData.NodeId));
    }
  }

  private IEnumerator FollowCoroutine(string _nick, string _nodeId)
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    this.photonView.RPC("NET_PlayerSelectedNode", RpcTarget.MasterClient, (object) _nick, (object) _nodeId);
  }

  [PunRPC]
  private void NET_PlayerSelectedNode(string _nick, string _nodeId)
  {
    if (this.playerSelectedNodesDict.ContainsKey(_nick))
      return;
    this.playerSelectedNodesDict.Add(_nick, _nodeId);
    string[] array1 = new string[this.playerSelectedNodesDict.Count];
    this.playerSelectedNodesDict.Keys.CopyTo(array1, 0);
    string[] array2 = new string[this.playerSelectedNodesDict.Count];
    this.playerSelectedNodesDict.Values.CopyTo(array2, 0);
    this.photonView.RPC("NET_SharePlayerSelectedNode", RpcTarget.All, (object) JsonHelper.ToJson<string>(array1), (object) JsonHelper.ToJson<string>(array2));
    if (this.playerSelectedNodesDict.Count != NetworkManager.Instance.GetNumPlayers())
      return;
    bool flag = true;
    string _nodeId1 = "";
    foreach (KeyValuePair<string, string> keyValuePair in this.playerSelectedNodesDict)
    {
      if (_nodeId1 == "")
        _nodeId1 = keyValuePair.Value;
      else if (keyValuePair.Value != _nodeId1)
      {
        flag = false;
        break;
      }
    }
    if (!flag)
      this.photonView.RPC("NET_DoConflict", RpcTarget.All);
    else
      this.StartCoroutine(this.TravelToThisNodeCo(_nodeId1));
  }

  [PunRPC]
  private void NET_SharePlayerSelectedNode(string _keys, string _values)
  {
    this.playerSelectedNodesDict.Clear();
    string[] strArray1 = JsonHelper.FromJson<string>(_keys);
    string[] strArray2 = JsonHelper.FromJson<string>(_values);
    this.ClearMultiplayerSelectedNodes();
    for (int index = 0; index < strArray1.Length; ++index)
    {
      this.playerSelectedNodesDict.Add(strArray1[index], strArray2[index]);
      this.GetNodeFromId(strArray2[index]).ShowSelectedNode(strArray1[index]);
      if (!this.selectedNode && GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster() && AtOManager.Instance.followingTheLeader && NetworkManager.Instance.PlayerIsMaster(strArray1[index]))
      {
        this.PlayerSelectedNode(this.GetNodeFromId(strArray2[index]), true);
        return;
      }
    }
    GameManager.Instance.PlayLibraryAudio("ui_mapnodeselection");
  }

  private void ClearMultiplayerSelectedNodes()
  {
    for (int index = 0; index < this.mapNodes.Count; ++index)
      this.mapNode[this.mapNodes[index]].ClearSelectedNode();
  }

  private IEnumerator TravelToThisNodeCo(string _nodeId)
  {
    MapManager mapManager = this;
    if (mapManager.followingCo != null)
    {
      Debug.Log((object) "STOP FollowCoruitne");
      mapManager.StopCoroutine(mapManager.followingCo);
    }
    yield return (object) Globals.Instance.WaitForSeconds(0.5f);
    mapManager.TravelToThisNode(mapManager.GetNodeFromId(_nodeId));
  }

  public void TravelToThisNode(Node _node)
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("TravelToThisNode " + _node?.ToString(), "map");
    this.StartCoroutine(this.TravelToThisNodeCorruption(_node));
  }

  private IEnumerator TravelToThisNodeCorruption(Node _node)
  {
    if (GameManager.Instance.IsMultiplayer())
      this.playerSelectedNodesDict.Clear();
    string nodeId = _node.nodeData.NodeId;
    bool flag = false;
    if (AtOManager.Instance.GetGameId() != "cban29t".ToUpper() && nodeId != "tutorial_1" && nodeId != "sen_1" && nodeId != "sen_2" && nodeId != "sen_3" && nodeId != "aqua_27")
      flag = true;
    if (_node.nodeData.DisableCorruption)
      flag = false;
    if (flag && _node.action == "combat")
    {
      CombatData combatData = Globals.Instance.GetCombatData(_node.actionId);
      if ((UnityEngine.Object) combatData.EventData == (UnityEngine.Object) null || (UnityEngine.Object) combatData.EventRequirementData != (UnityEngine.Object) null && !AtOManager.Instance.PlayerHasRequirement(combatData.EventRequirementData))
      {
        this.corruptionSetted = false;
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("corruptionType->" + AtOManager.Instance.corruptionType.ToString(), "map");
        this.corruption.InitCorruption(_node);
        while (!this.corruptionSetted)
          yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      }
    }
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("Travel To This Node " + nodeId, "map");
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD(AtOManager.Instance.currentMapNode + "!=" + nodeId, "map");
    _node.SetActive();
    string _nodeObeliskIcon = "";
    if (GameManager.Instance.IsObeliskChallenge())
    {
      foreach (Transform transform in _node.transform)
      {
        if (transform.gameObject.name == "nodeIcon" && (UnityEngine.Object) transform.GetComponent<SpriteRenderer>() != (UnityEngine.Object) null && (UnityEngine.Object) transform.GetComponent<SpriteRenderer>().sprite != (UnityEngine.Object) null)
          _nodeObeliskIcon = transform.GetComponent<SpriteRenderer>().sprite.name;
      }
    }
    bool executeActionInNode = AtOManager.Instance.SetCurrentNode(nodeId, _node.actionId, _nodeObeliskIcon);
    if (GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
    {
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("**************************", "net");
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("WaitingSyncro waitingSetCurrentNode", "net");
      while (!NetworkManager.Instance.AllPlayersReady("waitingSetCurrentNode"))
        yield return (object) Globals.Instance.WaitForSeconds(0.01f);
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("Game ready, Everybody checked", "net");
      NetworkManager.Instance.PlayersNetworkContinue("waitingSetCurrentNode");
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    }
    if (_node.nodeData.GoToTown)
    {
      AtOManager.Instance.GoToTown(nodeId);
    }
    else
    {
      if (executeActionInNode)
      {
        if (_node.action == "combat")
        {
          this.DoCombat(Globals.Instance.GetCombatData(_node.actionId));
          yield break;
        }
        else if (_node.action == "event")
        {
          this.DoEvent(Globals.Instance.GetEventData(_node.actionId));
          yield break;
        }
      }
      this.ChangeZone(nodeId);
    }
  }

  private void DoCombat(CombatData _combatData)
  {
    if (GameManager.Instance.IsMultiplayer() && (!GameManager.Instance.IsMultiplayer() || !NetworkManager.Instance.IsMaster()))
      return;
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("[MAPMANAGER] DoCombat");
    this.HidePopup();
    AtOManager.Instance.LaunchCombat(_combatData);
    this.CleanFromEvent();
  }

  public void ShowPopup(Node _node)
  {
    if ((UnityEngine.Object) CardCraftManager.Instance != (UnityEngine.Object) null || (UnityEngine.Object) EventManager.Instance != (UnityEngine.Object) null)
      return;
    if (this.showPopupCo != null)
      this.StopCoroutine(this.showPopupCo);
    if (this.IsMaskActive())
      return;
    this.showPopupCo = this.StartCoroutine(this.ShowPopupCo(_node));
  }

  private IEnumerator ShowPopupCo(Node _node)
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.05f);
    this.popupNode.Show(_node);
  }

  public void HidePopup()
  {
    if (this.showPopupCo != null)
      this.StopCoroutine(this.showPopupCo);
    this.popupNode.Hide();
  }

  private void HideWorld() => this.worldTransform.gameObject.SetActive(false);

  private void ShowWorld() => this.worldTransform.gameObject.SetActive(true);

  public void ShowHideEvent()
  {
    if (!this.worldTransform.gameObject.activeSelf)
    {
      this.worldTransform.gameObject.SetActive(true);
      this.eventShowHideButton.GetComponent<BotonGeneric>().SetText(Texts.Instance.GetText("show"));
      this.eventGO.transform.localPosition = new Vector3(this.eventGO.transform.localPosition.x, this.eventGO.transform.localPosition.y, -100f);
    }
    else
    {
      this.worldTransform.gameObject.SetActive(false);
      this.eventShowHideButton.GetComponent<BotonGeneric>().SetText(Texts.Instance.GetText("hide"));
      this.eventGO.transform.localPosition = new Vector3(this.eventGO.transform.localPosition.x, this.eventGO.transform.localPosition.y, -1f);
    }
  }

  private void ShowTMPRoads() => this.tmpRoads.gameObject.SetActive(true);

  private void HideTMPRoads() => this.tmpRoads.gameObject.SetActive(false);

  public void HighlightNode(string _nodeDestinationId, bool _status) => this.mapNode[_nodeDestinationId].HighlightNode(_status);

  public void HighlightNodeMP(Node _nodeDestination, bool _status) => this.photonView.RPC("NET_HightlightNode", RpcTarget.Others, (object) _nodeDestination.nodeData.NodeId, (object) _status);

  [PunRPC]
  private void NET_HightlightNode(string _nodeDestination, bool _status) => this.mapNode[_nodeDestination].HighlightNode(_status);

  private void DoEvent(EventData _eventData)
  {
    if (GameManager.Instance.IsMultiplayer() && (!GameManager.Instance.IsMultiplayer() || !NetworkManager.Instance.IsMaster()))
      return;
    if (GameManager.Instance.IsMultiplayer())
      this.photonView.RPC("NET_DoEvent", RpcTarget.Others, (object) _eventData.EventId);
    this.LaunchEvent(_eventData);
    this.CleanFromEvent();
  }

  [PunRPC]
  public void NET_DoEvent(string eventId)
  {
    if (this.followingCo != null)
    {
      Debug.Log((object) "STOP FollowCoruitne");
      this.StopCoroutine(this.followingCo);
    }
    this.LaunchEvent(Globals.Instance.GetEventData(eventId));
  }

  private void LaunchEvent(EventData _eventData)
  {
    GameManager.Instance.SceneLoaded();
    this.ShowMask(false);
    if ((UnityEngine.Object) this.eventGO != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.eventGO);
    this.StartCoroutine(this.DoEventCo(_eventData));
  }

  private IEnumerator DoEventCo(EventData _eventData)
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.02f);
    this.eventGO = UnityEngine.Object.Instantiate<GameObject>(this.eventPrefab, new Vector3(0.0f, 0.0f, -1f), Quaternion.identity);
    this.eventGO.transform.localScale = new Vector3(1f, 1f, 1f);
    this.eventManager = this.eventGO.GetComponent<EventManager>();
    this.eventManager.SetEvent(_eventData);
    this.HidePopup();
    this.HideWorld();
    this.HideTMPRoads();
    this.eventShowHideButton.gameObject.SetActive(true);
    this.eventShowHideButton.GetComponent<BotonGeneric>().SetText(Texts.Instance.GetText("hide"));
    yield return (object) null;
  }

  public void EventReady() => EventManager.Instance.Ready();

  [PunRPC]
  private void NET_CloseEvent() => EventManager.Instance.CloseEvent();

  public void CloseEventFromEvent(
    NodeData _destinationNode = null,
    CombatData _combatData = null,
    EventData _eventData = null,
    bool _upgrade = false,
    int _discount = 0,
    int _maxQuantity = -1,
    bool _healer = false,
    bool _craft = false,
    string _shopListId = "",
    string _lootListId = "",
    Enums.CardRarity _maxCraftRarity = Enums.CardRarity.Common,
    bool _cardPlayerGame = false,
    CardPlayerPackData _cardPlayerGamePack = null,
    bool _cardPlayerPairsGame = false,
    CardPlayerPairsPackData _cardPlayerPairsGamePack = null,
    bool _corruption = false)
  {
    this.StartCoroutine(this.CloseEventFromEventCo(_destinationNode, _combatData, _eventData, _upgrade, _discount, _maxQuantity, _healer, _craft, _shopListId, _lootListId, _maxCraftRarity, _cardPlayerGame, _cardPlayerGamePack, _cardPlayerPairsGame, _cardPlayerPairsGamePack, _corruption));
  }

  private IEnumerator CloseEventFromEventCo(
    NodeData _destinationNode = null,
    CombatData _combatData = null,
    EventData _eventData = null,
    bool _upgrade = false,
    int _discount = 0,
    int _maxQuantity = -1,
    bool _healer = false,
    bool _craft = false,
    string _shopListId = "",
    string _lootListId = "",
    Enums.CardRarity _maxCraftRarity = Enums.CardRarity.Common,
    bool _cardPlayerGame = false,
    CardPlayerPackData _cardPlayerGamePack = null,
    bool _cardPlayerPairsGame = false,
    CardPlayerPairsPackData _cardPlayerPairsGamePack = null,
    bool _corruption = false)
  {
    MapManager mapManager = this;
    UnityEngine.Object.Destroy((UnityEngine.Object) mapManager.eventGO);
    mapManager.eventGO = (GameObject) null;
    mapManager.eventManager = (EventManager) null;
    mapManager.eventShowHideButton.gameObject.SetActive(false);
    if (GameManager.Instance.IsMultiplayer())
    {
      string key = mapManager.CurrentNode();
      mapManager.ShowMask(true);
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("**************************");
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("WaitingSyncro closevent" + key, "net");
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersReady("closevent" + key))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("Game ready, Everybody checked closevent" + key, "net");
        NetworkManager.Instance.PlayersNetworkContinue("closevent" + key);
      }
      else
      {
        NetworkManager.Instance.SetWaitingSyncro("closevent" + key, true);
        NetworkManager.Instance.SetStatusReady("closevent" + key);
        while (NetworkManager.Instance.WaitingSyncro["closevent" + key])
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("closevent" + key + ", we can continue!", "net");
      }
      key = (string) null;
    }
    AtOManager.Instance.fromEventCombatData = _combatData;
    AtOManager.Instance.fromEventEventData = _eventData;
    if ((UnityEngine.Object) AtOManager.Instance.GetEventRewardTier() != (UnityEngine.Object) null)
      AtOManager.Instance.LaunchRewards();
    else if (_upgrade)
      AtOManager.Instance.DoCardUpgrade(_discount, _maxQuantity);
    else if (_healer)
      AtOManager.Instance.DoCardHealer(_discount, _maxQuantity);
    else if (_craft)
      AtOManager.Instance.DoCardCraft(_discount, _maxQuantity, _maxCraftRarity);
    else if (_corruption)
      AtOManager.Instance.DoCardCorruption(_discount, _maxQuantity);
    else if (_shopListId != "")
      AtOManager.Instance.DoItemShop(_shopListId, _discount);
    else if (_lootListId != "")
    {
      AtOManager.Instance.fromEventDestinationNode = _destinationNode;
      mapManager.StartCoroutine(mapManager.DoLoot(_lootListId));
    }
    else if (_cardPlayerGame)
      AtOManager.Instance.DoCardPlayerGame(_cardPlayerGamePack);
    else if (_cardPlayerPairsGame)
      AtOManager.Instance.DoCardPlayerPairsGame(_cardPlayerPairsGamePack);
    else if ((UnityEngine.Object) _destinationNode != (UnityEngine.Object) null)
      mapManager.TravelToThisNode(mapManager.GetNodeFromId(_destinationNode.NodeId));
    else
      mapManager.BeginMap();
  }

  private IEnumerator DoLoot(string _lootListId)
  {
    if (GameManager.Instance.IsMultiplayer())
    {
      this.ShowMask(true);
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("**************************");
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("WaitingSyncro mapdoloot", "net");
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersReady("mapdoloot"))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("Game ready, Everybody checked mapdoloot", "net");
        NetworkManager.Instance.PlayersNetworkContinue("mapdoloot");
        AtOManager.Instance.DoLoot(_lootListId);
      }
      else
      {
        NetworkManager.Instance.SetWaitingSyncro("mapdoloot", true);
        NetworkManager.Instance.SetStatusReady("mapdoloot");
        while (NetworkManager.Instance.WaitingSyncro["mapdoloot"])
          yield return (object) Globals.Instance.WaitForSeconds(0.1f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("mapdoloot, we can continue!", "net");
      }
    }
    else
      AtOManager.Instance.DoLoot(_lootListId);
  }

  [PunRPC]
  public void NET_CloseCardCraft() => CardCraftManager.Instance.ExitCardCraft();

  [PunRPC]
  private void NET_Event_OptionSelected(string _playerNick, int _option) => this.eventManager.NET_OptionSelected(_playerNick, _option);

  [PunRPC]
  public void NET_Event_SelectAnswer(int _answerId) => this.eventManager.NET_SelectAnswer(_answerId);

  public void GenerateRandomStringBatch(int total, int _seed = -1)
  {
    this.randomStringArr.Clear();
    int seed = (int) DateTime.Now.Ticks;
    if (_seed != -1)
      seed = _seed;
    UnityEngine.Random.InitState(seed);
    for (int index = 0; index < total; ++index)
      this.randomStringArr.Add(Functions.RandomString(6f));
    if (!GameManager.Instance.IsMultiplayer() || !NetworkManager.Instance.IsMaster())
      return;
    this.photonView.RPC("NET_SetRandomSeed", RpcTarget.Others, (object) (short) total, (object) seed);
  }

  private void SendRandomStringBatch()
  {
    string json = JsonHelper.ToJson<string>(this.randomStringArr.ToArray());
    byte[] bytes = Encoding.UTF8.GetBytes(json);
    Debug.Log((object) ("randomArr=>" + json));
    Debug.Log((object) bytes);
    Debug.Log((object) ("compress=>" + Functions.CompressString(json)));
    this.photonView.RPC("NET_SetRandomStringBatch", RpcTarget.Others, (object) bytes, (object) this.randomIndex);
  }

  [PunRPC]
  private void NET_SetRandomStringBatch(byte[] arrByte, int index)
  {
    this.randomStringArr = ((IEnumerable<string>) JsonHelper.FromJson<string>(Encoding.UTF8.GetString(arrByte))).ToList<string>();
    this.randomIndex = index;
  }

  [PunRPC]
  private void NET_SetRandomSeed(short _total, int _seed) => this.GenerateRandomStringBatch((int) _total, _seed);

  public void SetRandomIndexRandom() => this.randomIndex = UnityEngine.Random.Range(0, this.randomStringArr.Count);

  public string GetRandomString()
  {
    string randomString = this.randomStringArr[this.randomIndex];
    ++this.randomIndex;
    if (this.randomIndex < this.randomStringArr.Count)
      return randomString;
    this.randomIndex = 0;
    return randomString;
  }

  public int GetRandomIntRange(int min, int max) => Functions.Random(min, max, this.GetRandomString());

  public int GetRandomIntRangeOLD(int min, int max)
  {
    if (min == max)
      return min;
    string randomString = this.GetRandomString();
    int num1 = 0;
    for (int index = 0; index < randomString.Length; ++index)
    {
      if (randomString[index] == ' ')
        ++num1;
    }
    long[] sumArr = new long[num1 + 1];
    long num2 = Functions.ASCIIWordSum(randomString, sumArr);
    return min + Mathf.FloorToInt((float) (num2 % (long) (max - min)));
  }

  public void ShowCharacterStats()
  {
    this.sideCharacters.Show();
    int num = 0;
    if ((bool) (UnityEngine.Object) CardCraftManager.Instance)
      num = CardCraftManager.Instance.heroIndex;
    this.sideCharacters.ShowActiveStatus(num);
    this.characterWindow.Show("stats", num);
  }

  public void CorruptionBox()
  {
    if (this.corruptionSetted)
      return;
    this.corruption.BoxClicked();
  }

  public void CorruptionContinue()
  {
    if (!this.corruption.CorruptionOk())
      return;
    this.corruptionSetted = true;
    this.corruption.HideButton();
  }

  public void CorruptionShowHide() => this.corruption.ShowHide();

  public bool IsCorruptionOver() => this.corruption.IsActive();

  public void CorruptionSelectReward(string type)
  {
    if (this.corruptionSetted)
      return;
    this.corruption.ChooseReward(type);
  }

  [PunRPC]
  public void NET_ShareCorruption(
    string _corruptionRewardId,
    string _corruptionRewardIdB,
    string _corruptionIdCard,
    int _corruptionRewardChar,
    string _corruptionRewardCard,
    string _nodeSelectedAssignedId,
    string _nodeSelectedDataId)
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD(nameof (NET_ShareCorruption));
    this.corruption.DrawCorruptionFromNet(_corruptionRewardId, _corruptionRewardIdB, _corruptionIdCard, _corruptionRewardChar, _corruptionRewardCard, _nodeSelectedAssignedId, _nodeSelectedDataId);
  }

  [PunRPC]
  public void NET_ChooseRewardCorruption(short choosed)
  {
    string choosed1 = "";
    switch (choosed)
    {
      case 1:
        choosed1 = "A";
        break;
      case 2:
        choosed1 = "B";
        break;
    }
    this.corruption.ChooseReward(choosed1);
  }

  [PunRPC]
  public void NET_BoxClicked(bool status) => this.corruption.BoxClicked(true, status);

  public void ConflictSelection(int option) => this.conflict.SelectOption(option);

  public bool IsConflictOver() => (UnityEngine.Object) this.conflict != (UnityEngine.Object) null;

  [PunRPC]
  public void NET_DoConflict()
  {
    if (this.followingCo != null)
    {
      Debug.Log((object) "STOP FollowCoruitne");
      this.StopCoroutine(this.followingCo);
    }
    this.StartCoroutine(this.InitConflictResolutionCo());
  }

  private IEnumerator InitConflictResolutionCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.15f);
    this.InitConflictResolution();
  }

  private void InitConflictResolution()
  {
    this.conflictGO = UnityEngine.Object.Instantiate<GameObject>(this.conflictPrefab, new Vector3(0.0f, 0.0f, -2f), Quaternion.identity);
    this.conflict = this.conflictGO.GetComponent<ConflictManager>();
    this.conflict.Show();
  }

  [PunRPC]
  public void NET_ShareConflictOrder(int _playerChoosing) => this.StartCoroutine(this.NET_ShareConflictOrderCo(_playerChoosing));

  private IEnumerator NET_ShareConflictOrderCo(int _playerChoosing)
  {
    MapManager mapManager = this;
    while ((UnityEngine.Object) mapManager.conflict == (UnityEngine.Object) null)
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    mapManager.conflict.playerChoosing = _playerChoosing;
    mapManager.StartCoroutine(mapManager.conflict.NET_ShareConflictOrderCo());
  }

  [PunRPC]
  public void NET_SelectConflictOptionFromSlave(int _option) => this.conflict.SelectOptionFromOutside(_option);

  [PunRPC]
  public void NET_SelectConflictOption(int _option) => this.conflict.SelectOptionFromOutside(_option);

  public void ResultConflict(string _playerWin) => this.StartCoroutine(this.ResultConflictCo(_playerWin));

  private IEnumerator ResultConflictCo(string _playerWin)
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("ResultConflict " + _playerWin);
    yield return (object) Globals.Instance.WaitForSeconds(2.5f);
    UnityEngine.Object.Destroy((UnityEngine.Object) this.conflictGO);
    this.conflict = (ConflictManager) null;
    if (NetworkManager.Instance.IsMaster())
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.8f);
      this.ResultConflictAction(_playerWin);
    }
  }

  private void ResultConflictAction(string _playerWin)
  {
    if ((UnityEngine.Object) EventManager.Instance != (UnityEngine.Object) null)
    {
      int num = 0;
      if (EventManager.Instance.MultiplayerPlayerSelection != null && EventManager.Instance.MultiplayerPlayerSelection.ContainsKey(_playerWin))
        num = EventManager.Instance.MultiplayerPlayerSelection[_playerWin];
      this.photonView.RPC("NET_Event_SelectAnswer", RpcTarget.All, (object) num);
    }
    else
    {
      if (this.playerSelectedNodesDict.Count <= 0)
        return;
      this.TravelToThisNode(this.GetNodeFromId(this.playerSelectedNodesDict[_playerWin]));
    }
  }

  private void SkinUnlock(bool admin = false)
  {
    if (!((UnityEngine.Object) AtOManager.Instance.skinUnlockData != (UnityEngine.Object) null))
      return;
    string steamStat = AtOManager.Instance.skinUnlockData.SteamStat;
    if (steamStat == "" || SteamManager.Instance.GetStatInt(steamStat) == 1)
      return;
    SteamManager.Instance.SetStatInt(steamStat, 1);
    this.unlockGO = UnityEngine.Object.Instantiate<GameObject>(this.unlockCharacterPrefab, new Vector3(0.0f, 0.0f, -7f), Quaternion.identity);
    this.unlockGO.GetComponent<global::CharacterUnlock>().ShowUnlock(AtOManager.Instance.skinUnlockData.SkinSubclass, AtOManager.Instance.skinUnlockData);
    AtOManager.Instance.skinUnlockData = (SkinData) null;
  }

  private void CharacterUnlock()
  {
    if (!((UnityEngine.Object) AtOManager.Instance.characterUnlockData != (UnityEngine.Object) null))
      return;
    if (!PlayerManager.Instance.IsHeroUnlocked(AtOManager.Instance.characterUnlockData.Id))
    {
      this.unlockGO = UnityEngine.Object.Instantiate<GameObject>(this.unlockCharacterPrefab, new Vector3(0.0f, 0.0f, -7f), Quaternion.identity);
      this.unlockGO.GetComponent<global::CharacterUnlock>().ShowUnlock(AtOManager.Instance.characterUnlockData);
      PlayerManager.Instance.HeroUnlock(AtOManager.Instance.characterUnlockData.Id);
    }
    AtOManager.Instance.characterUnlockData = (SubClassData) null;
  }

  public bool IsCharacterUnlock() => !((UnityEngine.Object) this.unlockGO == (UnityEngine.Object) null) && this.unlockGO.gameObject.activeSelf;

  public void CharacterUnlockClose() => UnityEngine.Object.Destroy((UnityEngine.Object) this.unlockGO);

  public void ShowMask(bool state, float alpha = 1f)
  {
    if (this.maskCoroutine != null)
      this.StopCoroutine(this.maskCoroutine);
    if (state)
      this.maskCoroutine = this.StartCoroutine(this.ShowMaskCo(alpha));
    else
      this.StartCoroutine(this.HideMaskCo());
  }

  private IEnumerator ShowMaskCo(float alpha = 1f)
  {
    this.maskImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
    this.maskObject.gameObject.SetActive(true);
    this.maskMP.gameObject.SetActive(false);
    this.HidePopup();
    if (GameManager.Instance.IsMultiplayer())
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.2f);
      this.maskMP.gameObject.SetActive(true);
    }
  }

  private IEnumerator HideMaskCo()
  {
    this.maskMP.gameObject.SetActive(false);
    float index = this.maskImage.color.a;
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    while ((double) index > 0.0)
    {
      this.maskImage.color = new Color(0.0f, 0.0f, 0.0f, index);
      index -= 0.2f;
      yield return (object) null;
    }
    this.HidePopup();
    this.maskObject.gameObject.SetActive(false);
  }

  public bool IsMaskActive() => this.maskObject.gameObject.activeSelf || this.maskMP.gameObject.activeSelf;

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    bool shoulderLeft = false,
    bool shoulderRight = false)
  {
    if (this.corruption.IsActive())
    {
      this.corruption.ControllerMovement(goingUp, goingRight, goingDown, goingLeft, shoulderLeft, shoulderRight);
    }
    else
    {
      this.controllerShoulderCurrentOption = -1;
      if (this.controllerList.Count <= 0)
        return;
      if (this.controllerCurrentOption == -1)
      {
        for (int index = 0; index < this.controllerList.Count; ++index)
        {
          if ((UnityEngine.Object) this.controllerList[index] == (UnityEngine.Object) this.mapNode[this.CurrentNode()].transform)
          {
            this.controllerCurrentOption = index;
            break;
          }
        }
      }
      else if (goingLeft | goingUp)
      {
        --this.controllerCurrentOption;
        if (this.controllerCurrentOption < 0)
          this.controllerCurrentOption = 0;
      }
      else
      {
        ++this.controllerCurrentOption;
        if (this.controllerCurrentOption > this.controllerList.Count - 1)
          this.controllerCurrentOption = this.controllerList.Count - 1;
      }
      if (!((UnityEngine.Object) this.controllerList[this.controllerCurrentOption] != (UnityEngine.Object) null) || !Functions.TransformIsVisible(this.controllerList[this.controllerCurrentOption]))
        return;
      this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.controllerList[this.controllerCurrentOption].position);
      Mouse.current.WarpCursorPosition(this.warpPosition);
    }
  }

  public void ControllerMoveBlock(bool _isRight)
  {
    if (this.characterWindow.IsActive())
      return;
    if (this.corruption.IsActive())
    {
      this.corruption.ControllerMoveBlock(_isRight);
    }
    else
    {
      if (this.controllerCurrentOption > -1)
      {
        string name = this.instantTravelNodesTransform[this.controllerCurrentOption].gameObject.name;
        for (int index = 0; index < this.availableNodes.Count; ++index)
        {
          if (name == this.availableNodesTransform[index].gameObject.name)
          {
            this.controllerShoulderCurrentOption = index;
            break;
          }
        }
      }
      this.controllerCurrentOption = -1;
      if (this.availableNodesTransform == null || this.availableNodesTransform.Count <= 0)
        return;
      if (!_isRight)
      {
        --this.controllerShoulderCurrentOption;
        if (this.controllerShoulderCurrentOption < 0)
          this.controllerShoulderCurrentOption = this.availableNodesTransform.Count - 1;
      }
      else
      {
        ++this.controllerShoulderCurrentOption;
        if (this.controllerShoulderCurrentOption > this.availableNodesTransform.Count - 1)
          this.controllerShoulderCurrentOption = 0;
      }
      Transform transform = this.availableNodesTransform[this.controllerShoulderCurrentOption];
      if (!((UnityEngine.Object) transform != (UnityEngine.Object) null) || !Functions.TransformIsVisible(transform))
        return;
      Mouse.current.WarpCursorPosition((Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(transform.position));
    }
  }
}
