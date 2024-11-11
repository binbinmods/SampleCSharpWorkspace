// Decompiled with JetBrains decompiler
// Type: EventManager
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

public class EventManager : MonoBehaviour
{
  public TMP_Text title;
  public TMP_Text description;
  public TMP_Text descriptionRolls;
  public TMP_Text result;
  public TMP_Text notMeeted;
  public TMP_Text resultOK;
  public TMP_Text resultOKc;
  public TMP_Text resultKO;
  public TMP_Text resultKOc;
  public SpriteRenderer image;
  public Transform replysT;
  public GameObject replyPrefab;
  public Transform continueButton;
  public Transform cardKO;
  public Transform cardKOCards;
  public TMP_Text cardKOText;
  public Transform cardOK;
  public Transform cardOKCards;
  public TMP_Text cardOKText;
  private GameObject[] replysGOs;
  private Reply[] replys;
  private int numReplys;
  private int replysInvalid;
  public Transform[] characterT = new Transform[4];
  private Transform[] characterTcards = new Transform[4];
  private SpriteRenderer[] characterSPR = new SpriteRenderer[4];
  private TMP_Text[] characterTnum = new TMP_Text[4];
  private TMP_Text[] charTresultOK = new TMP_Text[4];
  private TMP_Text[] charTresultKO = new TMP_Text[4];
  private EventData currentEvent;
  private EventReplyData replySelected;
  private float topReply = 3.3f;
  private float distanceReply = 1.2f;
  public int optionSelected = -1;
  private Hero[] heroes = new Hero[4];
  private Hero[] heroesSource = new Hero[4];
  private bool[] charRoll;
  private int[] charRollIterations;
  private int[] charRollResult;
  private CardData[] charRollType;
  private bool criticalSuccess;
  private bool criticalFail;
  private bool[] charWinner = new bool[4];
  private bool groupWinner;
  private int cardOrder;
  private bool isGroup;
  private CombatData followUpCombatData;
  private EventData followUpEventData;
  private int followUpDiscount;
  private int followUpMaxQuantity;
  private Enums.CardRarity followUpMaxCraftRarity;
  private bool followUpUpgrade;
  private bool followUpHealer;
  private bool followUpCraft;
  private bool followUpCorruption;
  private bool followUpCardPlayerGame;
  private CardPlayerPackData followUpCardPlayerGamePack;
  private bool followUpCardPlayerPairsGame;
  private CardPlayerPairsPackData followUpCardPlayerPairsGamePack;
  private string followUpShopListId;
  private string followUpLootListId;
  private NodeData destinationNode;
  private List<string> cardCharacters;
  public Dictionary<string, int> MultiplayerPlayerSelection = new Dictionary<string, int>();
  private List<int> probability;
  private bool statusReady;
  public Transform waitingMsg;
  public TMP_Text waitingMsgText;
  private Coroutine manualReadyCo;
  private PhotonView photonView;
  private int modRollMadness;
  private int dustQuantity;
  private int goldQuantity;
  private bool[] characterPassRoll = new bool[4];
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();

  public static EventManager Instance { get; private set; }

  public Hero[] Heroes
  {
    get => this.heroes;
    set => this.heroes = value;
  }

  private void Awake()
  {
    if ((UnityEngine.Object) EventManager.Instance == (UnityEngine.Object) null)
      EventManager.Instance = this;
    else if ((UnityEngine.Object) EventManager.Instance != (UnityEngine.Object) this)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    this.photonView = MapManager.Instance.GetPhotonView();
    for (int index = 0; index < 4; ++index)
    {
      this.characterSPR[index] = this.characterT[index].GetComponent<SpriteRenderer>();
      this.characterTcards[index] = this.characterT[index].GetChild(0).transform;
      this.characterTnum[index] = this.characterT[index].GetChild(1).transform.GetComponent<TMP_Text>();
      this.charTresultKO[index] = this.characterT[index].GetChild(2).transform.GetComponent<TMP_Text>();
      this.charTresultOK[index] = this.characterT[index].GetChild(3).transform.GetComponent<TMP_Text>();
      this.characterPassRoll[index] = false;
    }
    this.waitingMsg.gameObject.SetActive(false);
  }

  private void Update()
  {
    if (!GameManager.Instance.GetDeveloperMode() || !Input.GetKeyDown(KeyCode.F1))
      return;
    MapManager.Instance.ShowHideEvent();
  }

  private void Start()
  {
    if (GameManager.Instance.IsMultiplayer())
    {
      this.statusReady = false;
      this.continueButton.GetComponent<BotonGeneric>().SetBackgroundColor(Functions.HexToColor(Globals.Instance.ClassColor["warrior"]));
      if (NetworkManager.Instance.IsMaster())
        NetworkManager.Instance.ClearAllPlayerManualReady();
    }
    this.continueButton.gameObject.SetActive(false);
  }

  public void Show()
  {
    AudioManager.Instance.DoBSO("Event");
    this.gameObject.SetActive(true);
    GameManager.Instance.CleanTempContainer();
    PopupManager.Instance.ClosePopup();
    MapManager.Instance.HidePopup();
    MapManager.Instance.sideCharacters.InCharacterScreen(true);
    MapManager.Instance.sideCharacters.ResetCharacters();
    AlertManager.Instance.HideAlert();
    SettingsManager.Instance.ShowSettings(false);
    DamageMeterManager.Instance.Hide();
    MapManager.Instance.characterWindow.HideAllWindows();
  }

  public void CloseEvent()
  {
    Functions.DebugLogGD("EVENT CloseEvent", "trace");
    foreach (Component component in this.replysT)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
    MapManager.Instance.sideCharacters.InCharacterScreen(false);
    PopupManager.Instance.ClosePopup();
    AudioManager.Instance.DoBSO("Map");
    SaveManager.SavePlayerData();
    MapManager.Instance.CloseEventFromEvent(this.destinationNode, this.followUpCombatData, this.followUpEventData, this.followUpUpgrade, this.followUpDiscount, this.followUpMaxQuantity, this.followUpHealer, this.followUpCraft, this.followUpShopListId, this.followUpLootListId, this.followUpMaxCraftRarity, this.followUpCardPlayerGame, this.followUpCardPlayerGamePack, this.followUpCardPlayerPairsGame, this.followUpCardPlayerPairsGamePack, this.followUpCorruption);
  }

  public void SetEvent(EventData _eventData)
  {
    this.destinationNode = (NodeData) null;
    this.followUpCombatData = (CombatData) null;
    this.followUpEventData = (EventData) null;
    this.followUpDiscount = 0;
    this.followUpMaxQuantity = -1;
    this.followUpMaxCraftRarity = Enums.CardRarity.Common;
    this.followUpUpgrade = false;
    this.followUpHealer = false;
    this.followUpCraft = false;
    this.followUpCardPlayerGame = false;
    this.followUpCardPlayerGamePack = (CardPlayerPackData) null;
    this.followUpShopListId = "";
    this.followUpLootListId = "";
    this.currentEvent = Globals.Instance.GetEventData(_eventData.EventId);
    this.optionSelected = -1;
    this.cardOrder = 1000;
    if (AtOManager.Instance.GetNgPlus() >= 4 || AtOManager.Instance.IsChallengeTraitActive("unlucky"))
      this.modRollMadness = 1;
    else if (AtOManager.Instance.IsChallengeTraitActive("lucky"))
      this.modRollMadness = -1;
    if ((UnityEngine.Object) this.currentEvent == (UnityEngine.Object) null)
      return;
    this.cardCharacters = new List<string>();
    string text1 = Texts.Instance.GetText(_eventData.EventId + "_nm", "events");
    if (text1 != "")
    {
      this.title.text = text1;
    }
    else
    {
      Debug.LogError((object) (_eventData.EventId + " <EventName> missing translation"));
      this.title.text = this.currentEvent.EventName;
    }
    StringBuilder stringBuilder = new StringBuilder();
    string text2 = Texts.Instance.GetText(_eventData.EventId + "_dsc", "events");
    if (text2 != "")
    {
      stringBuilder.Append(text2);
    }
    else
    {
      Debug.LogError((object) (_eventData.EventId + " <Description> missing translation"));
      stringBuilder.Append(this.currentEvent.Description);
    }
    stringBuilder.Append("\n\n<color=#333>");
    string text3 = Texts.Instance.GetText(_eventData.EventId + "_dsca", "events");
    if (text3 != "")
      stringBuilder.Append(text3);
    else
      stringBuilder.Append(this.currentEvent.DescriptionAction);
    this.description.text = stringBuilder.ToString();
    stringBuilder.Clear();
    stringBuilder.Append("<color=#FFF>");
    stringBuilder.Append(Texts.Instance.GetText("eventRolls"));
    stringBuilder.Append("</color><br><br>");
    stringBuilder.Append("<u>");
    stringBuilder.Append(Texts.Instance.GetText("single"));
    stringBuilder.Append("</u><br>");
    stringBuilder.Append(Texts.Instance.GetText("singleDesc"));
    stringBuilder.Append("<br><br>");
    stringBuilder.Append("<u>");
    stringBuilder.Append(Texts.Instance.GetText("competition"));
    stringBuilder.Append("</u><br>");
    stringBuilder.Append(Texts.Instance.GetText("competitionDesc"));
    stringBuilder.Append("<br><br>");
    stringBuilder.Append("<u>");
    stringBuilder.Append(Texts.Instance.GetText("group"));
    stringBuilder.Append("</u><br>");
    stringBuilder.Append(Texts.Instance.GetText("groupDesc"));
    this.descriptionRolls.text = stringBuilder.ToString();
    if ((UnityEngine.Object) this.currentEvent.EventSpriteBook != (UnityEngine.Object) null)
      this.image.sprite = this.currentEvent.EventSpriteBook;
    this.numReplys = this.currentEvent.Replys.Length;
    this.replysGOs = new GameObject[this.numReplys];
    this.replys = new Reply[this.numReplys];
    this.heroes = AtOManager.Instance.GetTeam();
    for (int index = 0; index < 4; ++index)
      this.charWinner[index] = true;
    this.Show();
    this.SetProbability();
  }

  private void EndProbability() => this.StartCoroutine(this.SetReplys());

  private void SetProbability()
  {
    bool flag = false;
    for (int index = 0; index < this.numReplys; ++index)
    {
      if (this.currentEvent.Replys[index].SsRoll)
      {
        flag = true;
        break;
      }
    }
    if (!flag)
    {
      this.EndProbability();
    }
    else
    {
      if (this.probability == null)
        this.probability = new List<int>();
      else
        this.probability.Clear();
      Dictionary<int, List<string>> dictionary = new Dictionary<int, List<string>>();
      for (int key = 0; key < 4; ++key)
      {
        dictionary[key] = new List<string>();
        if (this.heroes[key] != null && (UnityEngine.Object) this.heroes[key].HeroData != (UnityEngine.Object) null && this.heroes[key].Cards != null)
        {
          for (int index = 0; index < this.heroes[key].Cards.Count; ++index)
          {
            string lower = this.heroes[key].Cards[index].ToLower();
            if (!Globals.Instance.CardListByClass[Enums.CardClass.Boon].Contains(lower) && !Globals.Instance.CardListByClass[Enums.CardClass.Injury].Contains(lower))
              dictionary[key].Add(lower);
          }
        }
      }
      int num1 = 50;
      int[] numArray = new int[4];
      for (int key = 0; key < 4; ++key)
      {
        numArray[key] = dictionary[key].Count;
        if (numArray[key] > num1)
          numArray[key] = num1;
      }
      int count1 = numArray[0];
      int count2 = numArray[1];
      int count3 = numArray[2];
      int count4 = numArray[3];
      if (count1 > dictionary[0].Count)
        count1 = dictionary[0].Count;
      if (count2 > dictionary[1].Count)
        count2 = dictionary[1].Count;
      if (count3 > dictionary[2].Count)
        count3 = dictionary[2].Count;
      if (count4 > dictionary[3].Count)
        count4 = dictionary[3].Count;
      for (int index1 = 0; index1 < count1; ++index1)
      {
        string key1 = dictionary[0][index1];
        int num2 = !Globals.Instance.CardEnergyCost.ContainsKey(key1) ? 0 : Globals.Instance.CardEnergyCost[key1];
        if (count2 > 0)
        {
          for (int index2 = 0; index2 < count2; ++index2)
          {
            string key2 = dictionary[1][index2];
            int num3 = !Globals.Instance.CardEnergyCost.ContainsKey(key2) ? num2 : num2 + Globals.Instance.CardEnergyCost[key2];
            if (count3 > 0)
            {
              for (int index3 = 0; index3 < count3; ++index3)
              {
                string key3 = dictionary[2][index3];
                int num4 = !Globals.Instance.CardEnergyCost.ContainsKey(key3) ? num3 : num3 + Globals.Instance.CardEnergyCost[key3];
                if (count4 > 0)
                {
                  for (int index4 = 0; index4 < count4; ++index4)
                  {
                    string key4 = dictionary[3][index4];
                    this.probability.Add(!Globals.Instance.CardEnergyCost.ContainsKey(key4) ? num4 : num4 + Globals.Instance.CardEnergyCost[key4]);
                  }
                }
                else
                  this.probability.Add(num4);
              }
            }
            else
              this.probability.Add(num3);
          }
        }
        else
          this.probability.Add(num2);
      }
      this.EndProbability();
    }
  }

  public int GetProbability(int result, bool higherOrEqual)
  {
    int _times = 0;
    for (int index = 0; index < this.probability.Count; ++index)
    {
      if (result == this.probability[index])
        ++_times;
      else if (this.probability[index] > result & higherOrEqual)
        ++_times;
      else if (this.probability[index] < result && !higherOrEqual)
        ++_times;
    }
    return AtOManager.Instance.currentMapNode == "tutorial_2" ? 100 : this.ProbabilityResult(_times, this.probability.Count);
  }

  private int ProbabilityResult(int _times, int _total)
  {
    int num = Mathf.CeilToInt((float) ((double) _times / (double) _total * 100.0));
    if (num > 100)
      num = 100;
    else if (num < 0)
      num = 0;
    return num;
  }

  public int GetProbabilityType(Enums.CardType cType, string cClassId)
  {
    int _times = 0;
    int index1 = -1;
    for (int index2 = 0; index2 < this.heroes.Length; ++index2)
    {
      if (this.heroes[index2].HeroData.HeroSubClass.Id == cClassId)
      {
        index1 = index2;
        break;
      }
    }
    if (this.heroes[index1].GetItemToPassEventRoll() != "")
      return 100;
    int _total = 0;
    for (int index3 = 0; index3 < this.heroes[index1].Cards.Count; ++index3)
    {
      string lower = this.heroes[index1].Cards[index3].ToLower();
      if (!Globals.Instance.CardListByClass[Enums.CardClass.Boon].Contains(lower) && !Globals.Instance.CardListByClass[Enums.CardClass.Injury].Contains(lower))
      {
        if (Globals.Instance.CardListByType[cType].Contains(lower))
          ++_times;
        ++_total;
      }
    }
    return this.ProbabilityResult(_times, _total);
  }

  public int GetProbabilitySingle(int result, bool higherOrEqual, int heroId)
  {
    if (this.heroes[heroId].GetItemToPassEventRoll() != "")
      return 100;
    int _times = 0;
    int _total = 0;
    if (this.heroes[heroId].Cards != null)
    {
      for (int index = 0; index < this.heroes[heroId].Cards.Count; ++index)
      {
        string lower = this.heroes[heroId].Cards[index].ToLower();
        if (!Globals.Instance.CardListByClass[Enums.CardClass.Boon].Contains(lower) && !Globals.Instance.CardListByClass[Enums.CardClass.Injury].Contains(lower))
        {
          if (!Globals.Instance.CardEnergyCost.ContainsKey(lower))
            Globals.Instance.CardEnergyCost.Add(lower, Globals.Instance.GetCardData(lower, false).EnergyCost);
          int num = Globals.Instance.CardEnergyCost[lower];
          if (result == num)
            ++_times;
          else if (num > result & higherOrEqual)
            ++_times;
          else if (num < result && !higherOrEqual)
            ++_times;
          ++_total;
        }
      }
    }
    return this.ProbabilityResult(_times, _total);
  }

  private IEnumerator SetReplys()
  {
    if (GameManager.Instance.IsMultiplayer())
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersReady("eventreply"))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        Functions.DebugLogGD("Game ready, Everybody checked eventreply");
        NetworkManager.Instance.PlayersNetworkContinue("eventreply");
        yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      }
      else
      {
        NetworkManager.Instance.SetWaitingSyncro("eventreply", true);
        NetworkManager.Instance.SetStatusReady("eventreply");
        while (NetworkManager.Instance.WaitingSyncro["eventreply"])
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        Functions.DebugLogGD("eventreply, we can continue!");
      }
    }
    GameManager.Instance.PlayLibraryAudio("ui_book_page");
    GameManager.Instance.PlayLibraryAudio("ui_book_write");
    int num = 0;
    this.replysInvalid = 0;
    int totalPlayersGold = AtOManager.Instance.GetTotalPlayersGold();
    int totalPlayersDust = AtOManager.Instance.GetTotalPlayersDust();
    for (int index1 = 0; index1 < this.numReplys && this.optionSelected <= -1; ++index1)
    {
      EventReplyData reply = this.currentEvent.Replys[index1];
      bool flag1 = true;
      if (!GameManager.Instance.IsMultiplayer() && reply.RequirementMultiplayer)
        flag1 = false;
      if (flag1 && (UnityEngine.Object) reply.RequiredClass != (UnityEngine.Object) null && !AtOManager.Instance.PlayerHasRequirementClass(reply.RequiredClass.Id))
        flag1 = false;
      if (flag1 && (UnityEngine.Object) reply.Requirement != (UnityEngine.Object) null && !AtOManager.Instance.PlayerHasRequirement(reply.Requirement))
        flag1 = false;
      if (flag1 && (UnityEngine.Object) reply.RequirementBlocked != (UnityEngine.Object) null && AtOManager.Instance.PlayerHasRequirement(reply.RequirementBlocked))
        flag1 = false;
      if (flag1 && (UnityEngine.Object) reply.RequirementItem != (UnityEngine.Object) null && AtOManager.Instance.PlayerHasRequirementItem(reply.RequirementItem, reply.RequiredClass) == -1)
        flag1 = false;
      if (flag1)
      {
        if (reply.SsRemoveItemSlot != Enums.ItemSlot.None && !AtOManager.Instance.SubClassDataHaveAnythingInSlot(reply.SsRemoveItemSlot, reply.RequiredClass))
          flag1 = false;
        if (flag1 && reply.SsCorruptItemSlot != Enums.ItemSlot.None && !AtOManager.Instance.SubClassDataHaveAnythingInSlot(reply.SsCorruptItemSlot, reply.RequiredClass))
          flag1 = false;
      }
      if (flag1 && reply.RequirementCard != null && reply.RequirementCard.Count > 0)
      {
        flag1 = false;
        for (int index2 = 0; index2 < reply.RequirementCard.Count; ++index2)
        {
          if ((UnityEngine.Object) reply.RequirementCard[index2] != (UnityEngine.Object) null && AtOManager.Instance.PlayerHasRequirementCard(reply.RequirementCard[index2], reply.RequiredClass))
          {
            flag1 = true;
            break;
          }
        }
      }
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      if (flag1)
      {
        if (reply.RequirementSku != "")
        {
          if (!GameManager.Instance.IsMultiplayer())
          {
            if (!SteamManager.Instance.PlayerHaveDLC(reply.RequirementSku))
              flag4 = true;
          }
          else if (!NetworkManager.Instance.AnyPlayersHaveSku(reply.RequirementSku))
            flag4 = true;
        }
        if (reply.GoldCost > 0 && totalPlayersGold < reply.GoldCost)
          flag2 = true;
        if (reply.DustCost > 0 && totalPlayersDust < reply.DustCost)
          flag3 = true;
      }
      if (flag1)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.replyPrefab, this.replysT);
        gameObject.name = "reply_" + index1.ToString();
        this.replysGOs[index1] = gameObject;
        this.replys[index1] = gameObject.transform.GetComponent<Reply>();
        if ((UnityEngine.Object) reply.Requirement != (UnityEngine.Object) null && (UnityEngine.Object) reply.Requirement.ItemSprite != (UnityEngine.Object) null)
          this.replys[index1].SetImage(reply.Requirement.ItemSprite);
        if (flag4)
          this.replys[index1].Block(_showGoldShardMessage: false);
        else if (flag2 | flag3)
          this.replys[index1].Block();
        ++num;
      }
      else
      {
        ++this.replysInvalid;
        this.replysGOs[index1] = (GameObject) null;
        this.replys[index1] = (Reply) null;
      }
    }
    if (this.currentEvent.ReplyRandom > 0)
    {
      while (this.TotalValidGoInReplys() > this.currentEvent.ReplyRandom)
      {
        int randomIntRange = MapManager.Instance.GetRandomIntRange(0, this.replysGOs.Length);
        if ((UnityEngine.Object) this.replysGOs[randomIntRange] != (UnityEngine.Object) null)
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) this.replysGOs[randomIntRange]);
          this.replysGOs[randomIntRange] = (GameObject) null;
        }
      }
      this.replysInvalid = this.replysGOs.Length - this.TotalValidGoInReplys();
    }
    int _replyOrder = 0;
    int replyIndexForAutomaticSelection = -1;
    for (int _replyIndex = 0; _replyIndex < this.replysGOs.Length; ++_replyIndex)
    {
      if ((UnityEngine.Object) this.replysGOs[_replyIndex] != (UnityEngine.Object) null)
      {
        this.replysGOs[_replyIndex].transform.localPosition = new Vector3(0.18f, this.topReply - this.distanceReply * (float) _replyOrder, -1f);
        this.replys[_replyIndex].Init(this.currentEvent.EventId, _replyIndex, _replyOrder);
        ++_replyOrder;
        replyIndexForAutomaticSelection = _replyIndex;
      }
    }
    this.notMeeted.gameObject.SetActive(false);
    if (this.replysInvalid > 0)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Texts.Instance.GetText("eventMissingOptions"));
      stringBuilder.Append(" (");
      stringBuilder.Append(this.replysInvalid);
      stringBuilder.Append(")");
      this.notMeeted.text = stringBuilder.ToString();
      this.notMeeted.gameObject.SetActive(true);
    }
    if (!GameManager.Instance.IsMultiplayer() && _replyOrder == 1 && AtOManager.Instance.currentMapNode != "tutorial_2" && (UnityEngine.Object) this.currentEvent != (UnityEngine.Object) null && this.currentEvent.EventId != "e_challenge_finish")
    {
      if (this.notMeeted.gameObject.activeSelf)
      {
        Debug.Log((object) "Break because hiddenAnswers");
      }
      else
      {
        yield return (object) new WaitForSeconds(0.5f);
        this.replys[replyIndexForAutomaticSelection].SelectThisOption();
      }
    }
  }

  private int TotalValidGoInReplys()
  {
    int num = 0;
    for (int index = 0; index < this.replysGOs.Length; ++index)
    {
      if ((UnityEngine.Object) this.replysGOs[index] != (UnityEngine.Object) null)
        ++num;
    }
    return num;
  }

  public void SelectOption(int _index)
  {
    if (this.optionSelected > -1)
      return;
    this.optionSelected = _index;
    for (int index = 0; index < this.replys.Length; ++index)
    {
      if ((UnityEngine.Object) this.replys[index] != (UnityEngine.Object) null)
        this.replys[index].DisableOption();
    }
    if (GameManager.Instance.IsMultiplayer())
      this.photonView.RPC("NET_Event_OptionSelected", RpcTarget.All, (object) NetworkManager.Instance.GetPlayerNick(), (object) _index);
    else
      this.StartCoroutine(this.SelectOptionCo());
  }

  public void NET_OptionSelected(string _playerNick, int _option)
  {
    this.SelectOptionMP(_playerNick, _option);
    GameManager.Instance.PlayLibraryAudio("ui_eventoptionselection");
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster() || !AtOManager.Instance.followingTheLeader || !NetworkManager.Instance.PlayerIsMaster(_playerNick))
      return;
    this.StartCoroutine(this.AutoSelectClient(_option));
  }

  private IEnumerator AutoSelectClient(int _option)
  {
    if (_option < 0)
    {
      AtOManager.Instance.followingTheLeader = false;
    }
    else
    {
      while (this.replys == null || _option > this.replys.Length || (UnityEngine.Object) this.replys[_option] == (UnityEngine.Object) null)
        yield return (object) Globals.Instance.WaitForSeconds(0.01f);
      this.replys[_option].SelectThisOption();
    }
  }

  private void SelectOptionMP(string _playerNick, int _option)
  {
    if (!this.MultiplayerPlayerSelection.ContainsKey(_playerNick))
      this.MultiplayerPlayerSelection.Add(_playerNick, _option);
    for (int index = 0; index < this.replys.Length; ++index)
    {
      if ((UnityEngine.Object) this.replys[index] != (UnityEngine.Object) null && this.replys[index].GetOptionIndex() == _option)
        this.replys[index].SelectedByMultiplayer(_playerNick);
    }
    if (!NetworkManager.Instance.IsMaster() || this.MultiplayerPlayerSelection.Count != NetworkManager.Instance.GetNumPlayers())
      return;
    this.SelectMultiplayerAnswer();
  }

  private void SelectMultiplayerAnswer()
  {
    int num = this.MultiplayerPlayerSelection.ElementAt<KeyValuePair<string, int>>(0).Value;
    bool flag = false;
    for (int index = 1; index < this.MultiplayerPlayerSelection.Count; ++index)
    {
      if (this.MultiplayerPlayerSelection.ElementAt<KeyValuePair<string, int>>(index).Value != num)
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      this.photonView.RPC("NET_Event_SelectAnswer", RpcTarget.All, (object) this.MultiplayerPlayerSelection.ElementAt<KeyValuePair<string, int>>(0).Value);
    else
      this.photonView.RPC("NET_DoConflict", RpcTarget.All);
  }

  public void NET_SelectAnswer(int _answerId)
  {
    this.optionSelected = _answerId;
    this.StartCoroutine(this.SelectOptionCo());
  }

  private IEnumerator SelectOptionCo()
  {
    PopupManager.Instance.ClosePopup();
    GameObject selectedGO = (GameObject) null;
    for (int index = 0; index < this.replys.Length; ++index)
    {
      if ((UnityEngine.Object) this.replys[index] != (UnityEngine.Object) null)
      {
        if (this.replys[index].GetOptionIndex() != this.optionSelected)
          this.replysGOs[index].gameObject.SetActive(false);
        else
          selectedGO = this.replysGOs[index];
      }
    }
    float speed = (float) (((double) this.topReply - (double) selectedGO.transform.localPosition.y) * 0.20000000298023224);
    yield return (object) Globals.Instance.WaitForSeconds(0.05f);
    while ((double) selectedGO.transform.localPosition.y < (double) this.topReply)
    {
      selectedGO.transform.localPosition += new Vector3(0.0f, speed, 0.0f);
      if ((double) speed > 0.10000000149011612)
        speed *= 0.8f;
      yield return (object) null;
    }
    selectedGO.transform.localPosition = new Vector3(selectedGO.transform.localPosition.x, this.topReply, selectedGO.transform.localPosition.z);
    this.SelectOptionResult();
  }

  private void SelectOptionResult()
  {
    this.replySelected = this.currentEvent.Replys[this.optionSelected];
    if (this.replySelected.GoldCost > 0)
      AtOManager.Instance.PayGold(this.replySelected.GoldCost, false);
    if (this.replySelected.DustCost > 0)
      AtOManager.Instance.PayDust(this.replySelected.DustCost, false);
    if (this.replySelected.SsRoll)
    {
      this.StartCoroutine(this.ShowCharactersRoll());
    }
    else
    {
      this.groupWinner = true;
      this.FinalResolution();
    }
  }

  private IEnumerator ShowCharactersRoll()
  {
    EventManager eventManager = this;
    StringBuilder stringBuilder = new StringBuilder();
    if (eventManager.replySelected.SsRollMode == Enums.RollMode.HigherOrEqual)
    {
      stringBuilder.Append(">=");
      stringBuilder.Append(eventManager.replySelected.SsRollNumber + eventManager.modRollMadness);
    }
    else if (eventManager.replySelected.SsRollMode == Enums.RollMode.LowerOrEqual)
    {
      stringBuilder.Append("<=");
      stringBuilder.Append(eventManager.replySelected.SsRollNumber - eventManager.modRollMadness);
    }
    else if (eventManager.replySelected.SsRollMode == Enums.RollMode.Highest)
      stringBuilder.Append(">>");
    else if (eventManager.replySelected.SsRollMode == Enums.RollMode.Lowest)
    {
      stringBuilder.Append("<<");
    }
    else
    {
      stringBuilder.Append("~");
      stringBuilder.Append(eventManager.replySelected.SsRollNumber);
    }
    eventManager.replys[eventManager.optionSelected].SetRollBox(stringBuilder.ToString());
    eventManager.charRoll = new bool[4];
    eventManager.charRollIterations = new int[4];
    eventManager.charRollResult = new int[4];
    eventManager.charRollType = new CardData[4];
    for (int i = 0; i < 4; ++i)
    {
      if (eventManager.heroes[i] != null && !((UnityEngine.Object) eventManager.heroes[i].HeroData == (UnityEngine.Object) null))
      {
        bool flag = true;
        eventManager.charRollIterations[i] = -1;
        eventManager.characterSPR[i].sprite = eventManager.heroes[i].SpritePortrait;
        if (eventManager.replySelected.SsRollTarget == Enums.RollTarget.Character && eventManager.replySelected.RequiredClass.SubClassName != eventManager.heroes[i].HeroData.HeroSubClass.SubClassName)
          flag = false;
        eventManager.charRoll[i] = flag;
        if (!flag)
          eventManager.TurnOffCharacter(i);
        eventManager.characterT[i].gameObject.SetActive(true);
        yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      }
    }
    if ((bool) (UnityEngine.Object) MapManager.Instance)
    {
      string[] strArray = new string[7]
      {
        AtOManager.Instance.GetGameId(),
        "_",
        AtOManager.Instance.currentMapNode,
        "_",
        null,
        null,
        null
      };
      int num = AtOManager.Instance.GetTeamTotalHp();
      strArray[4] = num.ToString();
      strArray[5] = "_";
      num = AtOManager.Instance.GetTeamTotalExperience();
      strArray[6] = num.ToString();
      MapManager.Instance.GenerateRandomStringBatch(100, string.Concat(strArray).GetDeterministicHashCode());
    }
    eventManager.StartCoroutine(eventManager.RollCards());
  }

  private IEnumerator RollCards()
  {
    EventManager eventManager = this;
    int rolls = 0;
    for (int i = 0; i < 4; ++i)
    {
      if (eventManager.charRoll[i])
      {
        yield return (object) Globals.Instance.WaitForSeconds(0.25f);
        eventManager.DoCard(i);
        ++rolls;
      }
    }
    yield return (object) Globals.Instance.WaitForSeconds((float) (2.2999999523162842 + (double) rolls * 0.10000000149011612));
    eventManager.StartCoroutine(eventManager.EventResult());
  }

  private void DoCard(int heroIndex)
  {
    if (this.heroes[heroIndex] == null || this.heroes[heroIndex].Cards == null)
      return;
    ++this.charRollIterations[heroIndex];
    if (this.charRollIterations[heroIndex] > 3)
      this.charRollIterations[heroIndex] = 3;
    this.charRollResult[heroIndex] = -1;
    this.charRollType[heroIndex] = (CardData) null;
    List<string> stringList = new List<string>();
    for (int index = 0; index < this.heroes[heroIndex].Cards.Count; ++index)
      stringList.Add(this.heroes[heroIndex].Cards[index]);
    bool flag = false;
    string id = "";
    string itemToPassEventRoll = this.heroes[heroIndex].GetItemToPassEventRoll();
    if (itemToPassEventRoll != "" && (this.replySelected.SsRollTarget == Enums.RollTarget.Character || this.replySelected.SsRollTarget == Enums.RollTarget.Single))
    {
      id = itemToPassEventRoll;
      this.characterPassRoll[heroIndex] = true;
    }
    else
    {
      while (!flag)
      {
        id = stringList[MapManager.Instance.GetRandomIntRange(0, stringList.Count)];
        CardData cardData = Globals.Instance.GetCardData(id, false);
        if (cardData.CardClass != Enums.CardClass.Injury && cardData.CardClass != Enums.CardClass.Boon)
          flag = true;
      }
    }
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.characterTcards[heroIndex]);
    CardItem component = gameObject.GetComponent<CardItem>();
    component.gameObject.name = "EventRollCard";
    component.SetCard(id, false);
    component.SetCardback(this.heroes[heroIndex]);
    gameObject.transform.localPosition = new Vector3(0.0f, (float) (-0.75 - 0.10000000149011612 * (double) this.charRollIterations[heroIndex]), 0.0f);
    component.DoReward(false, true, selectable: false);
    component.SetDestinationLocalScale(0.9f);
    component.TopLayeringOrder("Book", this.cardOrder);
    this.cardOrder += 50;
    component.DisableCollider();
    this.charRollResult[heroIndex] = Globals.Instance.GetCardData(id, false).EnergyCost;
    this.charRollType[heroIndex] = Globals.Instance.GetCardData(id, false);
    if (this.replySelected.SsRollCard != Enums.CardType.None)
      return;
    this.StartCoroutine(this.ShowNumRoll(heroIndex));
  }

  private void ClearNumRoll(int heroIndex) => this.characterTnum[heroIndex].text = "";

  private IEnumerator ShowNumRoll(int heroIndex)
  {
    if (!this.charRoll[heroIndex])
    {
      this.characterTnum[heroIndex].text = "";
    }
    else
    {
      yield return (object) Globals.Instance.WaitForSeconds(2.2f);
      this.characterTcards[heroIndex].GetChild(0).GetComponent<CardItem>().active = true;
      this.characterTnum[heroIndex].text = this.charRollResult[heroIndex].ToString();
    }
  }

  private IEnumerator EventResult()
  {
    EventManager eventManager = this;
    bool flag = false;
    int num1 = 0;
    while (!flag)
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      num1 = 0;
      flag = true;
      for (int index = 0; index < 4; ++index)
      {
        if (eventManager.charRoll[index] && eventManager.charRollResult[index] == -1)
        {
          flag = false;
          break;
        }
        if (eventManager.charRoll[index])
          num1 += eventManager.charRollResult[index];
      }
    }
    eventManager.isGroup = true;
    bool success1 = false;
    int ssRollNumber = eventManager.replySelected.SsRollNumber;
    int rollNumberCritical = eventManager.replySelected.SsRollNumberCritical;
    int numberCriticalFail = eventManager.replySelected.SsRollNumberCriticalFail;
    if (eventManager.replySelected.SsRollTarget == Enums.RollTarget.Group)
    {
      bool success2 = false;
      if (eventManager.replySelected.SsRollMode == Enums.RollMode.HigherOrEqual)
      {
        if (num1 >= ssRollNumber + eventManager.modRollMadness)
          success2 = true;
        if (rollNumberCritical > -1 && num1 >= rollNumberCritical + eventManager.modRollMadness)
          eventManager.criticalSuccess = true;
        else if (rollNumberCritical > -1 && num1 <= numberCriticalFail + eventManager.modRollMadness)
          eventManager.criticalFail = true;
      }
      else if (eventManager.replySelected.SsRollMode == Enums.RollMode.LowerOrEqual)
      {
        if (num1 <= ssRollNumber - eventManager.modRollMadness)
          success2 = true;
        if (rollNumberCritical > -1 && num1 <= rollNumberCritical - eventManager.modRollMadness)
          eventManager.criticalSuccess = true;
        else if (rollNumberCritical > -1 && num1 >= numberCriticalFail - eventManager.modRollMadness)
          eventManager.criticalFail = true;
      }
      for (int index = 0; index < 4; ++index)
        eventManager.charWinner[index] = success2;
      eventManager.groupWinner = success2;
      eventManager.StartCoroutine(eventManager.ShowResultTitle(success2));
    }
    else if (eventManager.replySelected.SsRollTarget == Enums.RollTarget.Character)
    {
      for (int index = 0; index < 4; ++index)
      {
        if (eventManager.charRoll[index])
        {
          success1 = false;
          if (eventManager.characterPassRoll[index])
          {
            success1 = true;
            if (eventManager.replySelected.SsRollMode == Enums.RollMode.HigherOrEqual)
              eventManager.charRollResult[index] = ssRollNumber + eventManager.modRollMadness;
            else if (eventManager.replySelected.SsRollMode == Enums.RollMode.LowerOrEqual)
              eventManager.charRollResult[index] = ssRollNumber - eventManager.modRollMadness;
          }
          else if (eventManager.replySelected.SsRollCard != Enums.CardType.None)
          {
            if (eventManager.charRollType[index].HasCardType(eventManager.replySelected.SsRollCard))
              success1 = true;
          }
          else if (eventManager.replySelected.SsRollMode == Enums.RollMode.HigherOrEqual)
          {
            if (eventManager.charRollResult[index] >= ssRollNumber + eventManager.modRollMadness)
              success1 = true;
          }
          else if (eventManager.replySelected.SsRollMode == Enums.RollMode.LowerOrEqual && eventManager.charRollResult[index] <= ssRollNumber - eventManager.modRollMadness)
            success1 = true;
          eventManager.groupWinner = success1;
        }
      }
      for (int index = 0; index < 4; ++index)
        eventManager.charWinner[index] = eventManager.groupWinner;
      eventManager.StartCoroutine(eventManager.ShowResultTitle(success1));
    }
    else if (eventManager.replySelected.SsRollTarget == Enums.RollTarget.Single)
    {
      eventManager.isGroup = false;
      for (int heroIndex = 0; heroIndex < 4; ++heroIndex)
      {
        bool success3;
        if (eventManager.characterPassRoll[heroIndex])
        {
          success3 = true;
          if (eventManager.replySelected.SsRollMode == Enums.RollMode.HigherOrEqual)
            eventManager.charRollResult[heroIndex] = ssRollNumber + eventManager.modRollMadness;
          else if (eventManager.replySelected.SsRollMode == Enums.RollMode.LowerOrEqual)
            eventManager.charRollResult[heroIndex] = ssRollNumber - eventManager.modRollMadness;
        }
        else
        {
          success3 = false;
          if (eventManager.replySelected.SsRollMode == Enums.RollMode.HigherOrEqual)
          {
            if (eventManager.charRollResult[heroIndex] >= ssRollNumber + eventManager.modRollMadness)
              success3 = true;
          }
          else if (eventManager.replySelected.SsRollMode == Enums.RollMode.LowerOrEqual && eventManager.charRollResult[heroIndex] <= ssRollNumber - eventManager.modRollMadness)
            success3 = true;
        }
        eventManager.charWinner[heroIndex] = success3;
        eventManager.StartCoroutine(eventManager.ShowResultTitle(success3, heroIndex));
      }
    }
    else if (eventManager.replySelected.SsRollTarget == Enums.RollTarget.Competition)
    {
      eventManager.isGroup = false;
      Dictionary<int, int> source = new Dictionary<int, int>();
      for (int key = 0; key < 4; ++key)
      {
        eventManager.charWinner[key] = false;
        if (eventManager.charRoll[key])
          source.Add(key, eventManager.charRollResult[key]);
      }
      Dictionary<int, int> dictionary = source.OrderBy<KeyValuePair<int, int>, int>((Func<KeyValuePair<int, int>, int>) (x => x.Value)).ToDictionary<KeyValuePair<int, int>, int, int>((Func<KeyValuePair<int, int>, int>) (x => x.Key), (Func<KeyValuePair<int, int>, int>) (x => x.Value));
      int num2 = -1;
      int heroIndex = -1;
      int index1 = dictionary.Count - 1;
      KeyValuePair<int, int> keyValuePair;
      if (index1 == 0)
        heroIndex = dictionary.ElementAt<KeyValuePair<int, int>>(0).Key;
      else if (eventManager.replySelected.SsRollMode == Enums.RollMode.Highest)
      {
        int num3 = dictionary.ElementAt<KeyValuePair<int, int>>(index1).Value;
        keyValuePair = dictionary.ElementAt<KeyValuePair<int, int>>(index1 - 1);
        int num4 = keyValuePair.Value;
        if (num3 == num4)
        {
          keyValuePair = dictionary.ElementAt<KeyValuePair<int, int>>(index1);
          num2 = keyValuePair.Value;
        }
        else
        {
          keyValuePair = dictionary.ElementAt<KeyValuePair<int, int>>(index1);
          heroIndex = keyValuePair.Key;
        }
      }
      else if (eventManager.replySelected.SsRollMode == Enums.RollMode.Lowest)
      {
        int num5 = dictionary.ElementAt<KeyValuePair<int, int>>(0).Value;
        keyValuePair = dictionary.ElementAt<KeyValuePair<int, int>>(1);
        int num6 = keyValuePair.Value;
        if (num5 == num6)
        {
          keyValuePair = dictionary.ElementAt<KeyValuePair<int, int>>(0);
          num2 = keyValuePair.Value;
        }
        else
        {
          keyValuePair = dictionary.ElementAt<KeyValuePair<int, int>>(0);
          heroIndex = keyValuePair.Key;
        }
      }
      if (num2 > -1)
      {
        for (int index2 = 0; index2 < dictionary.Count; ++index2)
        {
          keyValuePair = dictionary.ElementAt<KeyValuePair<int, int>>(index2);
          int key = keyValuePair.Key;
          if (eventManager.charRoll[key])
          {
            keyValuePair = dictionary.ElementAt<KeyValuePair<int, int>>(index2);
            if (keyValuePair.Value == num2)
            {
              eventManager.charRoll[key] = true;
              goto label_87;
            }
          }
          eventManager.charRoll[key] = false;
          eventManager.StartCoroutine(eventManager.ShowResultTitle(false, key));
          eventManager.TurnOffCharacter(key);
label_87:
          eventManager.ClearNumRoll(key);
        }
        eventManager.StartCoroutine(eventManager.RollCards());
        yield break;
      }
      else
      {
        eventManager.StartCoroutine(eventManager.ShowResultTitle(true, heroIndex));
        eventManager.charWinner[heroIndex] = true;
        for (int index3 = 0; index3 < dictionary.Count; ++index3)
        {
          keyValuePair = dictionary.ElementAt<KeyValuePair<int, int>>(index3);
          int key = keyValuePair.Key;
          if (key != heroIndex)
          {
            if (eventManager.charRoll[key])
              eventManager.StartCoroutine(eventManager.ShowResultTitle(false, key));
            eventManager.TurnOffCharacter(key);
          }
        }
      }
    }
    eventManager.FinalResolution();
  }

  private void FinalResolution()
  {
    GameManager.Instance.PlayLibraryAudio("ui_book_write");
    bool flag1 = false;
    bool flag2 = false;
    if (this.replySelected.SsRoll)
    {
      if (this.isGroup)
      {
        if (this.groupWinner)
          flag1 = true;
        else
          flag2 = true;
      }
      else
      {
        for (int index = 0; index < 4; ++index)
        {
          if (this.charWinner[index])
            flag1 = true;
          else
            flag2 = true;
        }
      }
    }
    else
    {
      flag1 = true;
      this.isGroup = true;
    }
    this.replys[this.optionSelected].HideRollBox();
    if (AtOManager.Instance.Sandbox_alwaysPassEventRoll)
    {
      for (int index = 0; index < 4; ++index)
        this.charWinner[index] = true;
      this.criticalFail = false;
      flag1 = true;
      flag2 = false;
    }
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    for (int index1 = 0; index1 < 4; ++index1)
    {
      if (this.heroes[index1] != null && !((UnityEngine.Object) this.heroes[index1].HeroData == (UnityEngine.Object) null))
      {
        if ((this.replySelected.ReplyActionText != Enums.EventAction.CharacterName || this.replySelected.RequiredClass.SubClassName == this.heroes[index1].HeroData.HeroSubClass.SubClassName) && this.charWinner[index1])
        {
          if (!this.criticalSuccess)
          {
            if ((UnityEngine.Object) this.replySelected.SsAddCard1 != (UnityEngine.Object) null)
            {
              AtOManager.Instance.AddCardToHero(index1, this.replySelected.SsAddCard1.Id);
              this.cardCharacters.Add(AtOManager.Instance.GetHero(index1).SourceName);
            }
            if ((UnityEngine.Object) this.replySelected.SsAddCard2 != (UnityEngine.Object) null)
              AtOManager.Instance.AddCardToHero(index1, this.replySelected.SsAddCard2.Id);
            if ((UnityEngine.Object) this.replySelected.SsAddCard3 != (UnityEngine.Object) null)
              AtOManager.Instance.AddCardToHero(index1, this.replySelected.SsAddCard3.Id);
          }
          else
          {
            if ((UnityEngine.Object) this.replySelected.SscAddCard1 != (UnityEngine.Object) null)
            {
              AtOManager.Instance.AddCardToHero(index1, this.replySelected.SscAddCard1.Id);
              this.cardCharacters.Add(AtOManager.Instance.GetHero(index1).SourceName);
            }
            if ((UnityEngine.Object) this.replySelected.SscAddCard2 != (UnityEngine.Object) null)
              AtOManager.Instance.AddCardToHero(index1, this.replySelected.SscAddCard2.Id);
            if ((UnityEngine.Object) this.replySelected.SscAddCard3 != (UnityEngine.Object) null)
              AtOManager.Instance.AddCardToHero(index1, this.replySelected.SscAddCard3.Id);
          }
          if ((UnityEngine.Object) this.replySelected.SsPerkData != (UnityEngine.Object) null)
            AtOManager.Instance.AddPerkToHero(index1, this.replySelected.SsPerkData.Id);
          if ((UnityEngine.Object) this.replySelected.SsPerkData1 != (UnityEngine.Object) null)
            AtOManager.Instance.AddPerkToHero(index1, this.replySelected.SsPerkData1.Id);
        }
        if ((this.replySelected.ReplyActionText != Enums.EventAction.CharacterName || this.replySelected.RequiredClass.SubClassName == this.heroes[index1].HeroData.HeroSubClass.SubClassName) && !this.charWinner[index1])
        {
          if (!this.criticalFail)
          {
            if ((UnityEngine.Object) this.replySelected.FlAddCard1 != (UnityEngine.Object) null)
            {
              AtOManager.Instance.AddCardToHero(index1, this.replySelected.FlAddCard1.Id);
              this.cardCharacters.Add(AtOManager.Instance.GetHero(index1).SourceName);
            }
            if ((UnityEngine.Object) this.replySelected.FlAddCard2 != (UnityEngine.Object) null)
              AtOManager.Instance.AddCardToHero(index1, this.replySelected.FlAddCard2.Id);
            if ((UnityEngine.Object) this.replySelected.FlAddCard3 != (UnityEngine.Object) null)
              AtOManager.Instance.AddCardToHero(index1, this.replySelected.FlAddCard3.Id);
          }
          else
          {
            if ((UnityEngine.Object) this.replySelected.FlcAddCard1 != (UnityEngine.Object) null)
            {
              AtOManager.Instance.AddCardToHero(index1, this.replySelected.FlcAddCard1.Id);
              this.cardCharacters.Add(AtOManager.Instance.GetHero(index1).SourceName);
            }
            if ((UnityEngine.Object) this.replySelected.FlcAddCard2 != (UnityEngine.Object) null)
              AtOManager.Instance.AddCardToHero(index1, this.replySelected.FlcAddCard2.Id);
            if ((UnityEngine.Object) this.replySelected.FlcAddCard3 != (UnityEngine.Object) null)
              AtOManager.Instance.AddCardToHero(index1, this.replySelected.FlcAddCard3.Id);
          }
        }
        Enums.ItemSlot itemSlot;
        if (this.charWinner[index1])
        {
          if (!GameManager.Instance.IsMultiplayer() || this.heroes[index1].Owner == NetworkManager.Instance.GetPlayerNick())
          {
            ++num1;
            if (!this.criticalSuccess)
            {
              if (this.replySelected.SsUpgradeRandomCard)
                AtOManager.Instance.UpgradeRandomCardToHero(index1, this.replySelected.SsMaxQuantity);
              if ((bool) (UnityEngine.Object) this.replySelected.SsAddItem && ((UnityEngine.Object) this.replySelected.RequiredClass == (UnityEngine.Object) null || this.replySelected.RequiredClass.SubClassName == this.heroes[index1].HeroData.HeroSubClass.SubClassName))
              {
                string id = this.replySelected.SsAddItem.Id;
                this.cardCharacters.Add(AtOManager.Instance.GetHero(index1).SourceName);
                this.StartCoroutine(this.GenerateRewardCard(true, id));
                if (!GameManager.Instance.IsMultiplayer())
                  AtOManager.Instance.AddItemToHero(index1, id);
                else
                  AtOManager.Instance.AddItemToHeroMP(index1, id);
              }
              if (this.replySelected.SsRemoveItemSlot != Enums.ItemSlot.None && ((UnityEngine.Object) this.replySelected.RequiredClass == (UnityEngine.Object) null || this.replySelected.RequiredClass.SubClassName == this.heroes[index1].HeroData.HeroSubClass.SubClassName))
              {
                List<string> stringList1 = new List<string>();
                if (this.replySelected.SsRemoveItemSlot == Enums.ItemSlot.AllWithoutPet)
                {
                  stringList1.Add("weapon");
                  stringList1.Add("armor");
                  stringList1.Add("jewelry");
                  stringList1.Add("accesory");
                }
                else if (this.replySelected.SsRemoveItemSlot == Enums.ItemSlot.AllIncludedPet)
                {
                  stringList1.Add("weapon");
                  stringList1.Add("armor");
                  stringList1.Add("jewelry");
                  stringList1.Add("accesory");
                  stringList1.Add("pet");
                }
                else
                {
                  List<string> stringList2 = stringList1;
                  itemSlot = this.replySelected.SsRemoveItemSlot;
                  string lower = itemSlot.ToString().ToLower();
                  stringList2.Add(lower);
                }
                for (int index2 = 0; index2 < stringList1.Count; ++index2)
                  AtOManager.Instance.RemoveItemFromHeroFromEvent(true, index1, stringList1[index2]);
              }
              if (this.replySelected.SsCorruptItemSlot != Enums.ItemSlot.None && ((UnityEngine.Object) this.replySelected.RequiredClass == (UnityEngine.Object) null || this.replySelected.RequiredClass.SubClassName == this.heroes[index1].HeroData.HeroSubClass.SubClassName))
              {
                List<string> stringList3 = new List<string>();
                if (this.replySelected.SsCorruptItemSlot == Enums.ItemSlot.AllWithoutPet)
                {
                  stringList3.Add("weapon");
                  stringList3.Add("armor");
                  stringList3.Add("jewelry");
                  stringList3.Add("accesory");
                }
                else if (this.replySelected.SsCorruptItemSlot == Enums.ItemSlot.AllIncludedPet)
                {
                  stringList3.Add("weapon");
                  stringList3.Add("armor");
                  stringList3.Add("jewelry");
                  stringList3.Add("accesory");
                  stringList3.Add("pet");
                }
                else
                {
                  List<string> stringList4 = stringList3;
                  itemSlot = this.replySelected.SsCorruptItemSlot;
                  string lower = itemSlot.ToString().ToLower();
                  stringList4.Add(lower);
                }
                for (int index3 = 0; index3 < stringList3.Count; ++index3)
                  AtOManager.Instance.CorruptItemSlot(index1, stringList3[index3]);
              }
            }
            else
            {
              if (this.replySelected.SscUpgradeRandomCard)
                AtOManager.Instance.UpgradeRandomCardToHero(index1, this.replySelected.SscMaxQuantity);
              if ((bool) (UnityEngine.Object) this.replySelected.SscAddItem && ((UnityEngine.Object) this.replySelected.RequiredClass == (UnityEngine.Object) null || this.replySelected.RequiredClass.SubClassName == this.heroes[index1].HeroData.HeroSubClass.SubClassName))
              {
                string id = this.replySelected.SscAddItem.Id;
                this.cardCharacters.Add(AtOManager.Instance.GetHero(index1).SourceName);
                this.StartCoroutine(this.GenerateRewardCard(true, id));
                if (!GameManager.Instance.IsMultiplayer())
                  AtOManager.Instance.AddItemToHero(index1, id);
                else
                  AtOManager.Instance.AddItemToHeroMP(index1, id);
              }
              if (this.replySelected.SscRemoveItemSlot != Enums.ItemSlot.None && ((UnityEngine.Object) this.replySelected.RequiredClass == (UnityEngine.Object) null || this.replySelected.RequiredClass.SubClassName == this.heroes[index1].HeroData.HeroSubClass.SubClassName))
              {
                List<string> stringList5 = new List<string>();
                if (this.replySelected.SscRemoveItemSlot == Enums.ItemSlot.AllWithoutPet)
                {
                  stringList5.Add("weapon");
                  stringList5.Add("armor");
                  stringList5.Add("jewelry");
                  stringList5.Add("accesory");
                }
                else if (this.replySelected.SscRemoveItemSlot == Enums.ItemSlot.AllIncludedPet)
                {
                  stringList5.Add("weapon");
                  stringList5.Add("armor");
                  stringList5.Add("jewelry");
                  stringList5.Add("accesory");
                  stringList5.Add("pet");
                }
                else
                {
                  List<string> stringList6 = stringList5;
                  itemSlot = this.replySelected.SscRemoveItemSlot;
                  string lower = itemSlot.ToString().ToLower();
                  stringList6.Add(lower);
                }
                for (int index4 = 0; index4 < stringList5.Count; ++index4)
                  AtOManager.Instance.RemoveItemFromHeroFromEvent(true, index1, stringList5[index4]);
              }
              if (this.replySelected.SscCorruptItemSlot != Enums.ItemSlot.None && ((UnityEngine.Object) this.replySelected.RequiredClass == (UnityEngine.Object) null || this.replySelected.RequiredClass.SubClassName == this.heroes[index1].HeroData.HeroSubClass.SubClassName))
              {
                List<string> stringList7 = new List<string>();
                if (this.replySelected.SscCorruptItemSlot == Enums.ItemSlot.AllWithoutPet)
                {
                  stringList7.Add("weapon");
                  stringList7.Add("armor");
                  stringList7.Add("jewelry");
                  stringList7.Add("accesory");
                }
                else if (this.replySelected.SscCorruptItemSlot == Enums.ItemSlot.AllIncludedPet)
                {
                  stringList7.Add("weapon");
                  stringList7.Add("armor");
                  stringList7.Add("jewelry");
                  stringList7.Add("accesory");
                  stringList7.Add("pet");
                }
                else
                {
                  List<string> stringList8 = stringList7;
                  itemSlot = this.replySelected.SscCorruptItemSlot;
                  string lower = itemSlot.ToString().ToLower();
                  stringList8.Add(lower);
                }
                for (int index5 = 0; index5 < stringList7.Count; ++index5)
                  AtOManager.Instance.CorruptItemSlot(index1, stringList7[index5]);
              }
            }
          }
          if (!this.criticalSuccess)
          {
            AtOManager.Instance.ModifyHeroLife(index1, this.replySelected.SsRewardHealthFlat, this.replySelected.SsRewardHealthPercent);
            if (this.replySelected.SsExperienceReward != 0)
            {
              int rewardForCharacter = this.heroes[index1].CalculateRewardForCharacter(this.replySelected.SsExperienceReward);
              num3 += rewardForCharacter;
              this.heroes[index1].GrantExperience(rewardForCharacter);
            }
            if (this.replySelected.SsGoldReward != 0)
            {
              this.goldQuantity = this.replySelected.SsGoldReward;
              if (MadnessManager.Instance.IsMadnessTraitActive("poverty") || AtOManager.Instance.IsChallengeTraitActive("poverty"))
              {
                if (!GameManager.Instance.IsObeliskChallenge())
                  this.goldQuantity -= Functions.FuncRoundToInt((float) this.goldQuantity * 0.5f);
                else
                  this.goldQuantity -= Functions.FuncRoundToInt((float) this.goldQuantity * 0.3f);
                if (this.goldQuantity < 0)
                  this.goldQuantity = 0;
              }
              if (AtOManager.Instance.IsChallengeTraitActive("prosperity"))
                this.goldQuantity += Functions.FuncRoundToInt((float) this.goldQuantity * 0.5f);
              AtOManager.Instance.GivePlayer(0, this.goldQuantity, this.heroes[index1].Owner);
              if (this.goldQuantity > 0 && (!GameManager.Instance.IsMultiplayer() || this.heroes[index1].Owner == NetworkManager.Instance.GetPlayerNick()))
                PlayerManager.Instance.GoldGainedSum(this.goldQuantity, false);
            }
            if (this.replySelected.SsDustReward != 0)
            {
              this.dustQuantity = this.replySelected.SsDustReward;
              if (MadnessManager.Instance.IsMadnessTraitActive("poverty") || AtOManager.Instance.IsChallengeTraitActive("poverty"))
              {
                if (!GameManager.Instance.IsObeliskChallenge())
                  this.dustQuantity -= Functions.FuncRoundToInt((float) this.dustQuantity * 0.5f);
                else
                  this.dustQuantity -= Functions.FuncRoundToInt((float) this.dustQuantity * 0.3f);
                if (this.dustQuantity < 0)
                  this.dustQuantity = 0;
              }
              if (AtOManager.Instance.IsChallengeTraitActive("prosperity"))
                this.dustQuantity += Functions.FuncRoundToInt((float) this.dustQuantity * 0.5f);
              AtOManager.Instance.GivePlayer(1, this.dustQuantity, this.heroes[index1].Owner);
              if (this.dustQuantity > 0 && (!GameManager.Instance.IsMultiplayer() || this.heroes[index1].Owner == NetworkManager.Instance.GetPlayerNick()))
                PlayerManager.Instance.DustGainedSum(this.dustQuantity, false);
            }
          }
          else
          {
            AtOManager.Instance.ModifyHeroLife(index1, this.replySelected.SscRewardHealthFlat, this.replySelected.SscRewardHealthPercent);
            if (this.replySelected.SscExperienceReward != 0)
            {
              int rewardForCharacter = this.heroes[index1].CalculateRewardForCharacter(this.replySelected.SscExperienceReward);
              num3 += rewardForCharacter;
              this.heroes[index1].GrantExperience(rewardForCharacter);
            }
            if (this.replySelected.SscGoldReward != 0)
            {
              this.goldQuantity = this.replySelected.SscGoldReward;
              if (MadnessManager.Instance.IsMadnessTraitActive("poverty") || AtOManager.Instance.IsChallengeTraitActive("poverty"))
              {
                if (!GameManager.Instance.IsObeliskChallenge())
                  this.goldQuantity -= Functions.FuncRoundToInt((float) this.goldQuantity * 0.5f);
                else
                  this.goldQuantity -= Functions.FuncRoundToInt((float) this.goldQuantity * 0.3f);
                if (this.goldQuantity < 0)
                  this.goldQuantity = 0;
              }
              if (AtOManager.Instance.IsChallengeTraitActive("prosperity"))
                this.goldQuantity += Functions.FuncRoundToInt((float) this.goldQuantity * 0.5f);
              AtOManager.Instance.GivePlayer(0, this.goldQuantity, this.heroes[index1].Owner);
              if (this.goldQuantity > 0 && (!GameManager.Instance.IsMultiplayer() || this.heroes[index1].Owner == NetworkManager.Instance.GetPlayerNick()))
                PlayerManager.Instance.GoldGainedSum(this.goldQuantity, false);
            }
            if (this.replySelected.SscDustReward != 0)
            {
              this.dustQuantity = this.replySelected.SscDustReward;
              if (MadnessManager.Instance.IsMadnessTraitActive("poverty") || AtOManager.Instance.IsChallengeTraitActive("poverty"))
              {
                if (!GameManager.Instance.IsObeliskChallenge())
                  this.dustQuantity -= Functions.FuncRoundToInt((float) this.dustQuantity * 0.5f);
                else
                  this.dustQuantity -= Functions.FuncRoundToInt((float) this.dustQuantity * 0.3f);
                if (this.dustQuantity < 0)
                  this.dustQuantity = 0;
              }
              if (AtOManager.Instance.IsChallengeTraitActive("prosperity"))
                this.dustQuantity += Functions.FuncRoundToInt((float) this.dustQuantity * 0.5f);
              AtOManager.Instance.GivePlayer(1, this.dustQuantity, this.heroes[index1].Owner);
              if (this.dustQuantity > 0 && (!GameManager.Instance.IsMultiplayer() || this.heroes[index1].Owner == NetworkManager.Instance.GetPlayerNick()))
                PlayerManager.Instance.DustGainedSum(this.dustQuantity, false);
            }
          }
        }
        else if (!this.criticalFail)
        {
          if (!GameManager.Instance.IsMultiplayer() || this.heroes[index1].Owner == NetworkManager.Instance.GetPlayerNick())
          {
            ++num2;
            if (this.replySelected.FlUpgradeRandomCard)
              AtOManager.Instance.UpgradeRandomCardToHero(index1, this.replySelected.FlMaxQuantity);
            if ((bool) (UnityEngine.Object) this.replySelected.FlAddItem && ((UnityEngine.Object) this.replySelected.RequiredClass == (UnityEngine.Object) null || this.replySelected.RequiredClass.SubClassName == this.heroes[index1].HeroData.HeroSubClass.SubClassName))
            {
              string id = this.replySelected.FlAddItem.Id;
              this.cardCharacters.Add(AtOManager.Instance.GetHero(index1).SourceName);
              this.StartCoroutine(this.GenerateRewardCard(false, id));
              if (!GameManager.Instance.IsMultiplayer())
                AtOManager.Instance.AddItemToHero(index1, id);
              else
                AtOManager.Instance.AddItemToHeroMP(index1, id);
            }
            if (this.replySelected.FlRemoveItemSlot != Enums.ItemSlot.None && ((UnityEngine.Object) this.replySelected.RequiredClass == (UnityEngine.Object) null || this.replySelected.RequiredClass.SubClassName == this.heroes[index1].HeroData.HeroSubClass.SubClassName))
            {
              List<string> stringList9 = new List<string>();
              if (this.replySelected.FlRemoveItemSlot == Enums.ItemSlot.AllWithoutPet)
              {
                stringList9.Add("weapon");
                stringList9.Add("armor");
                stringList9.Add("jewelry");
                stringList9.Add("accesory");
              }
              else if (this.replySelected.FlRemoveItemSlot == Enums.ItemSlot.AllIncludedPet)
              {
                stringList9.Add("weapon");
                stringList9.Add("armor");
                stringList9.Add("jewelry");
                stringList9.Add("accesory");
                stringList9.Add("pet");
              }
              else
              {
                List<string> stringList10 = stringList9;
                itemSlot = this.replySelected.FlRemoveItemSlot;
                string lower = itemSlot.ToString().ToLower();
                stringList10.Add(lower);
              }
              for (int index6 = 0; index6 < stringList9.Count; ++index6)
                AtOManager.Instance.RemoveItemFromHeroFromEvent(true, index1, stringList9[index6]);
            }
            if (this.replySelected.FlCorruptItemSlot != Enums.ItemSlot.None && ((UnityEngine.Object) this.replySelected.RequiredClass == (UnityEngine.Object) null || this.replySelected.RequiredClass.SubClassName == this.heroes[index1].HeroData.HeroSubClass.SubClassName))
            {
              List<string> stringList11 = new List<string>();
              if (this.replySelected.FlCorruptItemSlot == Enums.ItemSlot.AllWithoutPet)
              {
                stringList11.Add("weapon");
                stringList11.Add("armor");
                stringList11.Add("jewelry");
                stringList11.Add("accesory");
              }
              else if (this.replySelected.FlCorruptItemSlot == Enums.ItemSlot.AllIncludedPet)
              {
                stringList11.Add("weapon");
                stringList11.Add("armor");
                stringList11.Add("jewelry");
                stringList11.Add("accesory");
                stringList11.Add("pet");
              }
              else
              {
                List<string> stringList12 = stringList11;
                itemSlot = this.replySelected.FlCorruptItemSlot;
                string lower = itemSlot.ToString().ToLower();
                stringList12.Add(lower);
              }
              for (int index7 = 0; index7 < stringList11.Count; ++index7)
                AtOManager.Instance.CorruptItemSlot(index1, stringList11[index7]);
            }
          }
          AtOManager.Instance.ModifyHeroLife(index1, this.replySelected.FlRewardHealthFlat, this.replySelected.FlRewardHealthPercent);
          if (this.replySelected.FlExperienceReward != 0)
          {
            int rewardForCharacter = this.heroes[index1].CalculateRewardForCharacter(this.replySelected.FlExperienceReward);
            num4 += rewardForCharacter;
            this.heroes[index1].GrantExperience(rewardForCharacter);
          }
          if (this.replySelected.FlGoldReward != 0)
          {
            this.goldQuantity = this.replySelected.FlGoldReward;
            if (MadnessManager.Instance.IsMadnessTraitActive("poverty") || AtOManager.Instance.IsChallengeTraitActive("poverty"))
            {
              if (!GameManager.Instance.IsObeliskChallenge())
                this.goldQuantity -= Functions.FuncRoundToInt((float) this.goldQuantity * 0.5f);
              else
                this.goldQuantity -= Functions.FuncRoundToInt((float) this.goldQuantity * 0.3f);
              if (this.goldQuantity < 0)
                this.goldQuantity = 0;
            }
            if (AtOManager.Instance.IsChallengeTraitActive("prosperity"))
              this.goldQuantity += Functions.FuncRoundToInt((float) this.goldQuantity * 0.5f);
            AtOManager.Instance.GivePlayer(0, this.goldQuantity, this.heroes[index1].Owner);
            if (this.goldQuantity > 0 && (!GameManager.Instance.IsMultiplayer() || this.heroes[index1].Owner == NetworkManager.Instance.GetPlayerNick()))
              PlayerManager.Instance.GoldGainedSum(this.goldQuantity, false);
          }
          if (this.replySelected.FlDustReward != 0)
          {
            this.dustQuantity = this.replySelected.FlDustReward;
            if (MadnessManager.Instance.IsMadnessTraitActive("poverty") || AtOManager.Instance.IsChallengeTraitActive("poverty"))
            {
              if (!GameManager.Instance.IsObeliskChallenge())
                this.dustQuantity -= Functions.FuncRoundToInt((float) this.dustQuantity * 0.5f);
              else
                this.dustQuantity -= Functions.FuncRoundToInt((float) this.dustQuantity * 0.3f);
              if (this.dustQuantity < 0)
                this.dustQuantity = 0;
            }
            if (AtOManager.Instance.IsChallengeTraitActive("prosperity"))
              this.dustQuantity += Functions.FuncRoundToInt((float) this.dustQuantity * 0.5f);
            AtOManager.Instance.GivePlayer(1, this.dustQuantity, this.heroes[index1].Owner);
            if (this.dustQuantity > 0 && (!GameManager.Instance.IsMultiplayer() || this.heroes[index1].Owner == NetworkManager.Instance.GetPlayerNick()))
              PlayerManager.Instance.DustGainedSum(this.dustQuantity, false);
          }
        }
        else
        {
          if (!GameManager.Instance.IsMultiplayer() || this.heroes[index1].Owner == NetworkManager.Instance.GetPlayerNick())
          {
            ++num2;
            if (this.replySelected.FlcUpgradeRandomCard)
              AtOManager.Instance.UpgradeRandomCardToHero(index1, this.replySelected.FlcMaxQuantity);
            if ((bool) (UnityEngine.Object) this.replySelected.FlcAddItem && ((UnityEngine.Object) this.replySelected.RequiredClass == (UnityEngine.Object) null || this.replySelected.RequiredClass.SubClassName == this.heroes[index1].HeroData.HeroSubClass.SubClassName))
            {
              string id = this.replySelected.FlcAddItem.Id;
              this.cardCharacters.Add(AtOManager.Instance.GetHero(index1).SourceName);
              this.StartCoroutine(this.GenerateRewardCard(false, id));
              if (!GameManager.Instance.IsMultiplayer())
                AtOManager.Instance.AddItemToHero(index1, id);
              else
                AtOManager.Instance.AddItemToHeroMP(index1, id);
            }
            if (this.replySelected.FlcRemoveItemSlot != Enums.ItemSlot.None && ((UnityEngine.Object) this.replySelected.RequiredClass == (UnityEngine.Object) null || this.replySelected.RequiredClass.SubClassName == this.heroes[index1].HeroData.HeroSubClass.SubClassName))
            {
              List<string> stringList13 = new List<string>();
              if (this.replySelected.FlcRemoveItemSlot == Enums.ItemSlot.AllWithoutPet)
              {
                stringList13.Add("weapon");
                stringList13.Add("armor");
                stringList13.Add("jewelry");
                stringList13.Add("accesory");
              }
              else if (this.replySelected.FlcRemoveItemSlot == Enums.ItemSlot.AllIncludedPet)
              {
                stringList13.Add("weapon");
                stringList13.Add("armor");
                stringList13.Add("jewelry");
                stringList13.Add("accesory");
                stringList13.Add("pet");
              }
              else
              {
                List<string> stringList14 = stringList13;
                itemSlot = this.replySelected.FlcRemoveItemSlot;
                string lower = itemSlot.ToString().ToLower();
                stringList14.Add(lower);
              }
              for (int index8 = 0; index8 < stringList13.Count; ++index8)
                AtOManager.Instance.RemoveItemFromHeroFromEvent(true, index1, stringList13[index8]);
            }
            if (this.replySelected.FlcCorruptItemSlot != Enums.ItemSlot.None && ((UnityEngine.Object) this.replySelected.RequiredClass == (UnityEngine.Object) null || this.replySelected.RequiredClass.SubClassName == this.heroes[index1].HeroData.HeroSubClass.SubClassName))
            {
              List<string> stringList15 = new List<string>();
              if (this.replySelected.FlcCorruptItemSlot == Enums.ItemSlot.AllWithoutPet)
              {
                stringList15.Add("weapon");
                stringList15.Add("armor");
                stringList15.Add("jewelry");
                stringList15.Add("accesory");
              }
              else if (this.replySelected.FlcCorruptItemSlot == Enums.ItemSlot.AllIncludedPet)
              {
                stringList15.Add("weapon");
                stringList15.Add("armor");
                stringList15.Add("jewelry");
                stringList15.Add("accesory");
                stringList15.Add("pet");
              }
              else
              {
                List<string> stringList16 = stringList15;
                itemSlot = this.replySelected.FlcCorruptItemSlot;
                string lower = itemSlot.ToString().ToLower();
                stringList16.Add(lower);
              }
              for (int index9 = 0; index9 < stringList15.Count; ++index9)
                AtOManager.Instance.CorruptItemSlot(index1, stringList15[index9]);
            }
          }
          AtOManager.Instance.ModifyHeroLife(index1, this.replySelected.FlcRewardHealthFlat, this.replySelected.FlcRewardHealthPercent);
          if (this.replySelected.FlcExperienceReward != 0)
          {
            int rewardForCharacter = this.heroes[index1].CalculateRewardForCharacter(this.replySelected.FlcExperienceReward);
            num4 += rewardForCharacter;
            this.heroes[index1].GrantExperience(rewardForCharacter);
          }
          if (this.replySelected.FlcGoldReward != 0)
          {
            this.goldQuantity = this.replySelected.FlcGoldReward;
            if (MadnessManager.Instance.IsMadnessTraitActive("poverty") || AtOManager.Instance.IsChallengeTraitActive("poverty"))
            {
              if (!GameManager.Instance.IsObeliskChallenge())
                this.goldQuantity -= Functions.FuncRoundToInt((float) this.goldQuantity * 0.5f);
              else
                this.goldQuantity -= Functions.FuncRoundToInt((float) this.goldQuantity * 0.3f);
              if (this.goldQuantity < 0)
                this.goldQuantity = 0;
            }
            if (AtOManager.Instance.IsChallengeTraitActive("prosperity"))
              this.goldQuantity += Functions.FuncRoundToInt((float) this.goldQuantity * 0.5f);
            AtOManager.Instance.GivePlayer(0, this.goldQuantity, this.heroes[index1].Owner);
            if (this.goldQuantity > 0 && (!GameManager.Instance.IsMultiplayer() || this.heroes[index1].Owner == NetworkManager.Instance.GetPlayerNick()))
              PlayerManager.Instance.GoldGainedSum(this.goldQuantity, false);
          }
          if (this.replySelected.FlcDustReward != 0)
          {
            this.dustQuantity = this.replySelected.FlcDustReward;
            if (MadnessManager.Instance.IsMadnessTraitActive("poverty") || AtOManager.Instance.IsChallengeTraitActive("poverty"))
            {
              if (!GameManager.Instance.IsObeliskChallenge())
                this.dustQuantity -= Functions.FuncRoundToInt((float) this.dustQuantity * 0.5f);
              else
                this.dustQuantity -= Functions.FuncRoundToInt((float) this.dustQuantity * 0.3f);
              if (this.dustQuantity < 0)
                this.dustQuantity = 0;
            }
            if (AtOManager.Instance.IsChallengeTraitActive("prosperity"))
              this.dustQuantity += Functions.FuncRoundToInt((float) this.dustQuantity * 0.5f);
            AtOManager.Instance.GivePlayer(1, this.dustQuantity, this.heroes[index1].Owner);
            if (this.dustQuantity > 0 && (!GameManager.Instance.IsMultiplayer() || this.heroes[index1].Owner == NetworkManager.Instance.GetPlayerNick()))
              PlayerManager.Instance.DustGainedSum(this.dustQuantity, false);
          }
        }
      }
    }
    StringBuilder stringBuilder = new StringBuilder();
    if (flag1)
    {
      if (!this.criticalSuccess)
      {
        this.destinationNode = this.replySelected.SsNodeTravel;
        this.followUpCombatData = this.replySelected.SsCombat;
        this.followUpEventData = this.replySelected.SsEvent;
        if (!this.isGroup)
        {
          stringBuilder.Append("<color=#098C20><u>");
          stringBuilder.Append(Texts.Instance.GetText("success"));
          stringBuilder.Append("</u></color>\n\n");
        }
        string text = Texts.Instance.GetText(this.currentEvent.EventId + "_rp" + this.replySelected.IndexForAnswerTranslation.ToString() + "_s", "events");
        if (text != "")
        {
          stringBuilder.Append(text);
        }
        else
        {
          Debug.LogError((object) (this.currentEvent.EventId + " <" + this.optionSelected.ToString() + "-> success> missing translation"));
          stringBuilder.Append(this.replySelected.SsRewardText);
        }
        bool flag3 = false;
        if (this.replySelected.SsGoldReward != 0 || this.replySelected.SsDustReward != 0 || num3 > 0 || this.replySelected.SsSupplyReward != 0)
        {
          stringBuilder.Append("\n\n<color=#202020>");
          stringBuilder.Append(Texts.Instance.GetText("eventYouGet"));
          stringBuilder.Append(" ");
          flag3 = true;
        }
        if (this.replySelected.SsGoldReward != 0)
        {
          stringBuilder.Append(" <sprite name=gold>");
          stringBuilder.Append(this.goldQuantity * num1);
          stringBuilder.Append("  ");
        }
        if (this.replySelected.SsDustReward != 0)
        {
          stringBuilder.Append(" <sprite name=dust>");
          stringBuilder.Append(this.dustQuantity * num1);
          stringBuilder.Append("  ");
        }
        if (num3 > 0)
        {
          stringBuilder.Append(" <sprite name=experience>");
          stringBuilder.Append(num3);
          stringBuilder.Append("  ");
        }
        if (this.replySelected.SsSupplyReward != 0)
        {
          stringBuilder.Append(" <sprite name=supply>");
          stringBuilder.Append(this.replySelected.SsSupplyReward);
          stringBuilder.Append("  ");
          PlayerManager.Instance.GainSupply(this.replySelected.SsSupplyReward);
        }
        if ((UnityEngine.Object) this.replySelected.SsPerkData != (UnityEngine.Object) null)
        {
          stringBuilder.Append("<sprite name=");
          stringBuilder.Append(this.replySelected.SsPerkData.Icon.name);
          stringBuilder.Append("><space=-.1>");
          stringBuilder.Append(this.replySelected.SsPerkData.IconTextValue);
          stringBuilder.Append("  ");
        }
        if ((UnityEngine.Object) this.replySelected.SsPerkData1 != (UnityEngine.Object) null)
        {
          stringBuilder.Append("<sprite name=");
          stringBuilder.Append(this.replySelected.SsPerkData1.Icon.name);
          stringBuilder.Append("><space=-.1>");
          stringBuilder.Append(this.replySelected.SsPerkData1.IconTextValue);
          stringBuilder.Append("  ");
        }
        if (flag3)
          stringBuilder.Append("</color>");
        if ((UnityEngine.Object) this.replySelected.SsRequirementUnlock != (UnityEngine.Object) null)
          AtOManager.Instance.AddPlayerRequirement(this.replySelected.SsRequirementUnlock);
        if ((UnityEngine.Object) this.replySelected.SsRequirementUnlock2 != (UnityEngine.Object) null)
          AtOManager.Instance.AddPlayerRequirement(this.replySelected.SsRequirementUnlock2);
        if ((UnityEngine.Object) this.replySelected.SsAddCard1 != (UnityEngine.Object) null)
          this.StartCoroutine(this.GenerateRewardCard(true, this.replySelected.SsAddCard1.Id));
        if ((UnityEngine.Object) this.replySelected.SsRewardTier != (UnityEngine.Object) null)
          AtOManager.Instance.SetEventRewardTier(this.replySelected.SsRewardTier);
        if ((bool) (UnityEngine.Object) this.replySelected.SsRequirementLock2)
        {
          if ((bool) (UnityEngine.Object) this.replySelected.SsRequirementLock)
            AtOManager.Instance.RemovePlayerRequirement(this.replySelected.SsRequirementLock2, false);
          else
            AtOManager.Instance.RemovePlayerRequirement(this.replySelected.SsRequirementLock2);
        }
        if ((bool) (UnityEngine.Object) this.replySelected.SsRequirementLock)
          AtOManager.Instance.RemovePlayerRequirement(this.replySelected.SsRequirementLock);
        if (this.replySelected.SsUpgradeUI)
        {
          this.followUpUpgrade = true;
          this.followUpDiscount = this.replySelected.SsDiscount;
          this.followUpMaxQuantity = this.replySelected.SsMaxQuantity;
        }
        if (this.replySelected.SsHealerUI)
        {
          this.followUpHealer = true;
          this.followUpDiscount = this.replySelected.SsDiscount;
          this.followUpMaxQuantity = this.replySelected.SsMaxQuantity;
        }
        if (this.replySelected.SsHealerUI)
        {
          this.followUpHealer = true;
          this.followUpDiscount = this.replySelected.SsDiscount;
          this.followUpMaxQuantity = this.replySelected.SsMaxQuantity;
        }
        if (this.replySelected.SsCraftUI)
        {
          this.followUpCraft = true;
          this.followUpDiscount = this.replySelected.SsDiscount;
          this.followUpMaxQuantity = this.replySelected.SsMaxQuantity;
          this.followUpMaxCraftRarity = this.replySelected.SsCraftUIMaxType;
        }
        if (this.replySelected.SsCorruptionUI)
        {
          this.followUpCorruption = true;
          this.followUpDiscount = this.replySelected.SsDiscount;
          this.followUpMaxQuantity = this.replySelected.SsMaxQuantity;
        }
        if ((UnityEngine.Object) this.replySelected.SsShopList != (UnityEngine.Object) null)
        {
          this.followUpShopListId = this.replySelected.SsShopList.Id;
          this.followUpDiscount = this.replySelected.SsDiscount;
        }
        if ((UnityEngine.Object) this.replySelected.SsLootList != (UnityEngine.Object) null)
          this.followUpLootListId = this.replySelected.SsLootList.Id;
        if (this.replySelected.SsCardPlayerGame)
        {
          this.followUpCardPlayerGame = true;
          this.followUpCardPlayerGamePack = this.replySelected.SsCardPlayerGamePackData;
        }
        if (this.replySelected.SsCardPlayerPairsGame)
        {
          this.followUpCardPlayerPairsGame = true;
          this.followUpCardPlayerPairsGamePack = this.replySelected.SsCardPlayerPairsGamePackData;
        }
        if ((bool) (UnityEngine.Object) this.replySelected.SsUnlockClass)
          AtOManager.Instance.characterUnlockData = this.replySelected.SsUnlockClass;
        if ((bool) (UnityEngine.Object) this.replySelected.SsUnlockSkin)
          AtOManager.Instance.skinUnlockData = this.replySelected.SsUnlockSkin;
        if (this.replySelected.SsUnlockSteamAchievement != "")
          PlayerManager.Instance.AchievementUnlock(this.replySelected.SsUnlockSteamAchievement);
        if (this.replySelected.SsFinishEarlyAccess)
        {
          AtOManager.Instance.FinishGame();
          return;
        }
        if (this.replySelected.SsFinishGame)
        {
          AtOManager.Instance.FinishRun();
          return;
        }
        if (this.replySelected.SsFinishObeliskMap)
        {
          ZoneData zoneData = Globals.Instance.ZoneDataSource[AtOManager.Instance.GetTownZoneId().ToLower()];
          if ((UnityEngine.Object) zoneData != (UnityEngine.Object) null && zoneData.ObeliskLow)
          {
            this.destinationNode = MapManager.Instance.GetNodeFromId(AtOManager.Instance.obeliskHigh + "_0").nodeData;
            AtOManager.Instance.UpgradeTownTier();
          }
          else if ((UnityEngine.Object) zoneData != (UnityEngine.Object) null && zoneData.ObeliskHigh)
          {
            this.destinationNode = MapManager.Instance.GetNodeFromId(AtOManager.Instance.obeliskFinal + "_0").nodeData;
            AtOManager.Instance.UpgradeTownTier();
          }
          else
          {
            AtOManager.Instance.FinishObeliskChallenge();
            return;
          }
        }
        if ((UnityEngine.Object) this.replySelected.SsCharacterReplacement != (UnityEngine.Object) null && this.replySelected.SsCharacterReplacementPosition > -1 && this.replySelected.SsCharacterReplacementPosition < 4)
          AtOManager.Instance.SwapCharacter(this.replySelected.SsCharacterReplacement, this.replySelected.SsCharacterReplacementPosition);
      }
      else
      {
        this.destinationNode = this.replySelected.SscNodeTravel;
        this.followUpCombatData = this.replySelected.SscCombat;
        this.followUpEventData = this.replySelected.SscEvent;
        if (!this.isGroup)
        {
          stringBuilder.Append("<color=#098C20><u>");
          stringBuilder.Append(Texts.Instance.GetText("success"));
          stringBuilder.Append("</u></color>\n\n");
        }
        string text = Texts.Instance.GetText(this.currentEvent.EventId + "_rp" + this.replySelected.IndexForAnswerTranslation.ToString() + "_sc", "events");
        if (text != "")
        {
          stringBuilder.Append(text);
        }
        else
        {
          Debug.LogError((object) (this.currentEvent.EventId + " <" + this.optionSelected.ToString() + "-> successCrit> missing translation"));
          stringBuilder.Append(this.replySelected.SscRewardText);
        }
        bool flag4 = false;
        if (this.replySelected.SscGoldReward != 0 || this.replySelected.SscDustReward != 0 || num3 > 0 || this.replySelected.SscSupplyReward != 0)
        {
          stringBuilder.Append("\n\n<color=#202020>");
          stringBuilder.Append(Texts.Instance.GetText("eventYouGet"));
          stringBuilder.Append(" ");
          flag4 = true;
        }
        if (this.replySelected.SscGoldReward != 0)
        {
          stringBuilder.Append(" <sprite name=gold>");
          stringBuilder.Append(this.goldQuantity * num1);
          stringBuilder.Append("  ");
        }
        if (this.replySelected.SscDustReward != 0)
        {
          stringBuilder.Append(" <sprite name=dust>");
          stringBuilder.Append(this.dustQuantity * num1);
          stringBuilder.Append("  ");
        }
        if (num3 > 0)
        {
          stringBuilder.Append(" <sprite name=experience>");
          stringBuilder.Append(num3);
          stringBuilder.Append("  ");
        }
        if (this.replySelected.SscSupplyReward != 0)
        {
          stringBuilder.Append(" <sprite name=supply>");
          stringBuilder.Append(this.replySelected.SscSupplyReward);
          stringBuilder.Append("  ");
          PlayerManager.Instance.GainSupply(this.replySelected.SscSupplyReward);
        }
        if (flag4)
          stringBuilder.Append("</color>");
        if ((UnityEngine.Object) this.replySelected.SscRequirementUnlock != (UnityEngine.Object) null)
          AtOManager.Instance.AddPlayerRequirement(this.replySelected.SscRequirementUnlock);
        if ((UnityEngine.Object) this.replySelected.SscRequirementUnlock2 != (UnityEngine.Object) null)
          AtOManager.Instance.AddPlayerRequirement(this.replySelected.SscRequirementUnlock2);
        if ((UnityEngine.Object) this.replySelected.SscAddCard1 != (UnityEngine.Object) null)
          this.StartCoroutine(this.GenerateRewardCard(true, this.replySelected.SscAddCard1.Id));
        if ((UnityEngine.Object) this.replySelected.SscRewardTier != (UnityEngine.Object) null)
          AtOManager.Instance.SetEventRewardTier(this.replySelected.SscRewardTier);
        if ((bool) (UnityEngine.Object) this.replySelected.SscRequirementLock)
          AtOManager.Instance.RemovePlayerRequirement(this.replySelected.SscRequirementLock);
        if (this.replySelected.SscUpgradeUI)
        {
          this.followUpUpgrade = true;
          this.followUpDiscount = this.replySelected.SscDiscount;
          this.followUpMaxQuantity = this.replySelected.SscMaxQuantity;
        }
        if (this.replySelected.SscHealerUI)
        {
          this.followUpHealer = true;
          this.followUpDiscount = this.replySelected.SscDiscount;
          this.followUpMaxQuantity = this.replySelected.SscMaxQuantity;
        }
        if (this.replySelected.SscHealerUI)
        {
          this.followUpHealer = true;
          this.followUpDiscount = this.replySelected.SscDiscount;
          this.followUpMaxQuantity = this.replySelected.SscMaxQuantity;
        }
        if (this.replySelected.SscCraftUI)
        {
          this.followUpCraft = true;
          this.followUpDiscount = this.replySelected.SscDiscount;
          this.followUpMaxQuantity = this.replySelected.SscMaxQuantity;
          this.followUpMaxCraftRarity = this.replySelected.SscCraftUIMaxType;
        }
        if (this.replySelected.SscCorruptionUI)
        {
          this.followUpCorruption = true;
          this.followUpDiscount = this.replySelected.SscDiscount;
          this.followUpMaxQuantity = this.replySelected.SscMaxQuantity;
        }
        if ((UnityEngine.Object) this.replySelected.SscShopList != (UnityEngine.Object) null)
        {
          this.followUpShopListId = this.replySelected.SscShopList.Id;
          this.followUpDiscount = this.replySelected.SscDiscount;
        }
        if ((UnityEngine.Object) this.replySelected.SscLootList != (UnityEngine.Object) null)
          this.followUpLootListId = this.replySelected.SscLootList.Id;
        if (this.replySelected.SscCardPlayerGame)
        {
          this.followUpCardPlayerGame = true;
          this.followUpCardPlayerGamePack = this.replySelected.SscCardPlayerGamePackData;
        }
        if (this.replySelected.SscCardPlayerPairsGame)
        {
          this.followUpCardPlayerPairsGame = true;
          this.followUpCardPlayerPairsGamePack = this.replySelected.SscCardPlayerPairsGamePackData;
        }
        if ((bool) (UnityEngine.Object) this.replySelected.SscUnlockClass)
          AtOManager.Instance.characterUnlockData = this.replySelected.SscUnlockClass;
        if (this.replySelected.SscUnlockSteamAchievement != "")
          PlayerManager.Instance.AchievementUnlock(this.replySelected.SscUnlockSteamAchievement);
        if (this.replySelected.SscFinishEarlyAccess)
        {
          AtOManager.Instance.FinishGame();
          return;
        }
        if (this.replySelected.SscFinishGame)
        {
          AtOManager.Instance.FinishRun();
          return;
        }
      }
    }
    if (flag2)
    {
      if (!this.criticalFail)
      {
        this.destinationNode = this.replySelected.FlNodeTravel;
        this.followUpCombatData = this.replySelected.FlCombat;
        this.followUpEventData = this.replySelected.FlEvent;
        if (flag1)
          stringBuilder.Append("\n\n\n");
        if (!this.isGroup)
        {
          stringBuilder.Append("<color=#980B06><u>");
          stringBuilder.Append(Texts.Instance.GetText("fail"));
          stringBuilder.Append("</u></color>\n\n");
        }
        string text = Texts.Instance.GetText(this.currentEvent.EventId + "_rp" + this.replySelected.IndexForAnswerTranslation.ToString() + "_f", "events");
        if (text != "")
        {
          stringBuilder.Append(text);
        }
        else
        {
          Debug.LogError((object) (this.currentEvent.EventId + " <" + this.optionSelected.ToString() + "-> fail> missing translation"));
          stringBuilder.Append(this.replySelected.FlRewardText);
        }
        if (this.replySelected.FlGoldReward != 0 || this.replySelected.FlDustReward != 0 || num4 > 0 || this.replySelected.FlSupplyReward != 0)
        {
          stringBuilder.Append("\n\n<color=#202020>");
          stringBuilder.Append(Texts.Instance.GetText("eventYouGet"));
          stringBuilder.Append(" ");
        }
        if (this.replySelected.FlGoldReward != 0)
        {
          stringBuilder.Append(" <sprite name=gold>");
          stringBuilder.Append(this.goldQuantity * num2);
          stringBuilder.Append("  ");
        }
        if (this.replySelected.FlDustReward != 0)
        {
          stringBuilder.Append(" <sprite name=dust>");
          stringBuilder.Append(this.dustQuantity * num2);
          stringBuilder.Append("  ");
        }
        if (num4 > 0)
        {
          stringBuilder.Append(" <sprite name=experience>");
          stringBuilder.Append(num4);
          stringBuilder.Append("  ");
        }
        if (this.replySelected.FlSupplyReward != 0)
        {
          stringBuilder.Append(" <sprite name=supply>");
          stringBuilder.Append(this.replySelected.FlSupplyReward);
          stringBuilder.Append("  ");
          PlayerManager.Instance.GainSupply(this.replySelected.FlSupplyReward);
        }
        stringBuilder.Append("\n");
        if ((UnityEngine.Object) this.replySelected.FlRequirementUnlock != (UnityEngine.Object) null)
          AtOManager.Instance.AddPlayerRequirement(this.replySelected.FlRequirementUnlock);
        if ((UnityEngine.Object) this.replySelected.FlRequirementUnlock2 != (UnityEngine.Object) null)
          AtOManager.Instance.AddPlayerRequirement(this.replySelected.FlRequirementUnlock2);
        if ((UnityEngine.Object) this.replySelected.FlAddCard1 != (UnityEngine.Object) null)
          this.StartCoroutine(this.GenerateRewardCard(false, this.replySelected.FlAddCard1.Id));
        if ((UnityEngine.Object) this.replySelected.FlRewardTier != (UnityEngine.Object) null)
          AtOManager.Instance.SetEventRewardTier(this.replySelected.FlRewardTier);
        if ((bool) (UnityEngine.Object) this.replySelected.FlRequirementLock)
          AtOManager.Instance.RemovePlayerRequirement(this.replySelected.FlRequirementLock);
        if (this.replySelected.FlUpgradeUI)
        {
          this.followUpUpgrade = true;
          this.followUpDiscount = this.replySelected.FlDiscount;
          this.followUpMaxQuantity = this.replySelected.FlMaxQuantity;
        }
        if (this.replySelected.FlHealerUI)
        {
          this.followUpHealer = true;
          this.followUpDiscount = this.replySelected.FlDiscount;
          this.followUpMaxQuantity = this.replySelected.FlMaxQuantity;
        }
        if (this.replySelected.FlCraftUI)
        {
          this.followUpCraft = true;
          this.followUpDiscount = this.replySelected.FlDiscount;
          this.followUpMaxQuantity = this.replySelected.FlMaxQuantity;
          this.followUpMaxCraftRarity = this.replySelected.FlCraftUIMaxType;
        }
        if (this.replySelected.FlCorruptionUI)
        {
          this.followUpCorruption = true;
          this.followUpDiscount = this.replySelected.FlDiscount;
          this.followUpMaxQuantity = this.replySelected.FlMaxQuantity;
        }
        if ((UnityEngine.Object) this.replySelected.FlShopList != (UnityEngine.Object) null)
        {
          this.followUpShopListId = this.replySelected.FlShopList.Id;
          this.followUpDiscount = this.replySelected.FlDiscount;
        }
        if ((UnityEngine.Object) this.replySelected.FlLootList != (UnityEngine.Object) null)
          this.followUpLootListId = this.replySelected.FlLootList.Id;
        if (this.replySelected.FlCardPlayerGame)
        {
          this.followUpCardPlayerGame = true;
          this.followUpCardPlayerGamePack = this.replySelected.FlCardPlayerGamePackData;
        }
        if (this.replySelected.FlCardPlayerPairsGame)
        {
          this.followUpCardPlayerPairsGame = true;
          this.followUpCardPlayerPairsGamePack = this.replySelected.FlCardPlayerPairsGamePackData;
        }
        if ((bool) (UnityEngine.Object) this.replySelected.FlUnlockClass)
          AtOManager.Instance.characterUnlockData = this.replySelected.FlUnlockClass;
        if (this.replySelected.FlUnlockSteamAchievement != "")
          PlayerManager.Instance.AchievementUnlock(this.replySelected.FlUnlockSteamAchievement);
      }
      else
      {
        this.destinationNode = this.replySelected.FlcNodeTravel;
        this.followUpCombatData = this.replySelected.FlcCombat;
        this.followUpEventData = this.replySelected.FlcEvent;
        if (flag1)
          stringBuilder.Append("\n\n\n");
        if (!this.isGroup)
        {
          stringBuilder.Append("<color=#980B06><u>");
          stringBuilder.Append(Texts.Instance.GetText("failCritical"));
          stringBuilder.Append("</u></color>\n\n");
        }
        string text = Texts.Instance.GetText(this.currentEvent.EventId + "_rp" + this.replySelected.IndexForAnswerTranslation.ToString() + "_fc", "events");
        if (text != "")
        {
          stringBuilder.Append(text);
        }
        else
        {
          Debug.LogError((object) (this.currentEvent.EventId + " <" + this.optionSelected.ToString() + "-> failCrit> missing translation"));
          stringBuilder.Append(this.replySelected.FlcRewardText);
        }
        if (this.replySelected.FlcGoldReward != 0 || this.replySelected.FlcDustReward != 0 || num4 > 0 || this.replySelected.FlcSupplyReward != 0)
        {
          stringBuilder.Append("\n\n<color=#202020>");
          stringBuilder.Append(Texts.Instance.GetText("eventYouGet"));
          stringBuilder.Append(" ");
        }
        if (this.replySelected.FlcGoldReward != 0)
        {
          stringBuilder.Append(" <sprite name=gold>");
          stringBuilder.Append(this.goldQuantity * num2);
          stringBuilder.Append("  ");
        }
        if (this.replySelected.FlcDustReward != 0)
        {
          stringBuilder.Append(" <sprite name=dust>");
          stringBuilder.Append(this.dustQuantity * num2);
          stringBuilder.Append("  ");
        }
        if (num4 > 0)
        {
          stringBuilder.Append(" <sprite name=experience>");
          stringBuilder.Append(num4);
          stringBuilder.Append("  ");
        }
        if (this.replySelected.FlcSupplyReward != 0)
        {
          stringBuilder.Append(" <sprite name=supply>");
          stringBuilder.Append(this.replySelected.FlcSupplyReward);
          stringBuilder.Append("  ");
          PlayerManager.Instance.GainSupply(this.replySelected.FlcSupplyReward);
        }
        stringBuilder.Append("\n");
        if ((UnityEngine.Object) this.replySelected.FlcRequirementUnlock != (UnityEngine.Object) null)
          AtOManager.Instance.AddPlayerRequirement(this.replySelected.FlcRequirementUnlock);
        if ((UnityEngine.Object) this.replySelected.FlcRequirementUnlock2 != (UnityEngine.Object) null)
          AtOManager.Instance.AddPlayerRequirement(this.replySelected.FlcRequirementUnlock2);
        if ((UnityEngine.Object) this.replySelected.FlcAddCard1 != (UnityEngine.Object) null)
          this.StartCoroutine(this.GenerateRewardCard(false, this.replySelected.FlcAddCard1.Id));
        if ((UnityEngine.Object) this.replySelected.FlcRewardTier != (UnityEngine.Object) null)
          AtOManager.Instance.SetEventRewardTier(this.replySelected.FlcRewardTier);
        if ((bool) (UnityEngine.Object) this.replySelected.FlcRequirementLock)
          AtOManager.Instance.RemovePlayerRequirement(this.replySelected.FlcRequirementLock);
        if (this.replySelected.FlcUpgradeUI)
        {
          this.followUpUpgrade = true;
          this.followUpDiscount = this.replySelected.FlcDiscount;
          this.followUpMaxQuantity = this.replySelected.FlcMaxQuantity;
        }
        if (this.replySelected.FlcHealerUI)
        {
          this.followUpHealer = true;
          this.followUpDiscount = this.replySelected.FlcDiscount;
          this.followUpMaxQuantity = this.replySelected.FlcMaxQuantity;
        }
        if (this.replySelected.FlcCraftUI)
        {
          this.followUpCraft = true;
          this.followUpDiscount = this.replySelected.FlcDiscount;
          this.followUpMaxQuantity = this.replySelected.FlcMaxQuantity;
          this.followUpMaxCraftRarity = this.replySelected.FlcCraftUIMaxType;
        }
        if (this.replySelected.FlcCorruptionUI)
        {
          this.followUpCorruption = true;
          this.followUpDiscount = this.replySelected.FlcDiscount;
          this.followUpMaxQuantity = this.replySelected.FlcMaxQuantity;
        }
        if ((UnityEngine.Object) this.replySelected.FlcShopList != (UnityEngine.Object) null)
        {
          this.followUpShopListId = this.replySelected.FlcShopList.Id;
          this.followUpDiscount = this.replySelected.FlcDiscount;
        }
        if ((UnityEngine.Object) this.replySelected.FlcLootList != (UnityEngine.Object) null)
          this.followUpLootListId = this.replySelected.FlcLootList.Id;
        if (this.replySelected.FlcCardPlayerGame)
        {
          this.followUpCardPlayerGame = true;
          this.followUpCardPlayerGamePack = this.replySelected.FlcCardPlayerGamePackData;
        }
        if (this.replySelected.FlcCardPlayerPairsGame)
        {
          this.followUpCardPlayerPairsGame = true;
          this.followUpCardPlayerPairsGamePack = this.replySelected.FlcCardPlayerPairsGamePackData;
        }
        if ((bool) (UnityEngine.Object) this.replySelected.FlcUnlockClass)
          AtOManager.Instance.characterUnlockData = this.replySelected.FlcUnlockClass;
        if (this.replySelected.FlcUnlockSteamAchievement != "")
          PlayerManager.Instance.AchievementUnlock(this.replySelected.FlcUnlockSteamAchievement);
      }
    }
    if (!this.replySelected.SsRoll)
    {
      RectTransform component = this.result.GetComponent<RectTransform>();
      component.localPosition = (Vector3) new Vector2(component.localPosition.x, 1.2f);
      this.result.fontSizeMin = 2.3f;
    }
    stringBuilder.Replace("(", "<color=#333><size=-.2><voffset=.2>(");
    stringBuilder.Replace(")", ")</voffset></size></color>");
    this.result.text = stringBuilder.ToString();
    this.result.gameObject.SetActive(true);
    if ((UnityEngine.Object) MapManager.Instance != (UnityEngine.Object) null)
      MapManager.Instance.sideCharacters.Refresh();
    this.continueButton.gameObject.SetActive(true);
  }

  private IEnumerator GenerateRewardCard(bool ok, string cardName)
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.2f);
    CardData cardData = Globals.Instance.GetCardData(cardName);
    if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && cardData.CardClass == Enums.CardClass.Injury)
    {
      int ngPlus = AtOManager.Instance.GetNgPlus();
      if (ngPlus > 0)
      {
        if (ngPlus >= 3 && ngPlus <= 4 && cardData.UpgradesTo1 != "")
        {
          cardName = cardData.UpgradesTo1;
          cardData = Globals.Instance.GetCardData(cardName);
        }
        else if (ngPlus >= 5 && cardData.UpgradesTo2 != "")
        {
          cardName = cardData.UpgradesTo2;
          cardData = Globals.Instance.GetCardData(cardName);
        }
      }
    }
    if (!((UnityEngine.Object) cardData == (UnityEngine.Object) null))
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < this.cardCharacters.Count; ++index)
      {
        if (index > 0)
          stringBuilder.Append(", ");
        stringBuilder.Append(this.cardCharacters[index]);
      }
      stringBuilder.Append("\n");
      string str = this.cardCharacters.Count <= 1 ? Texts.Instance.GetText("charReceive").Replace("<char>", stringBuilder.ToString()) : Texts.Instance.GetText("charsReceives").Replace("<chars>", stringBuilder.ToString());
      GameObject gameObject;
      if (cardData.CardClass == Enums.CardClass.Injury)
      {
        this.cardKO.gameObject.SetActive(true);
        this.cardKOText.text = str;
        gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, new Vector3(-2.5f, 0.0f, 0.0f), Quaternion.identity, this.cardKOCards);
      }
      else
      {
        this.cardOK.gameObject.SetActive(true);
        this.cardOKText.text = str;
        gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, new Vector3(-2.5f, 0.0f, 0.0f), Quaternion.identity, this.cardOKCards);
      }
      CardItem component = gameObject.GetComponent<CardItem>();
      component.SetCard(cardName, false);
      gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
      component.SetDestinationScaleRotation(new Vector3(3.3f, 1.1f, -1f), 1.4f, Quaternion.Euler(0.0f, 0.0f, 0.0f));
      component.cardrevealed = true;
      component.TopLayeringOrder("Book", 20000);
      component.CreateColliderAdjusted();
      if (cardData.CardType != Enums.CardType.Pet || cardData.CardRarity != Enums.CardRarity.Rare)
        PlayerManager.Instance.CardUnlock(cardName, true, component);
    }
  }

  private IEnumerator ShowResultTitle(bool success, int heroIndex = -1)
  {
    if (AtOManager.Instance.Sandbox_alwaysPassEventRoll)
      success = true;
    TMP_Text textObj;
    if (success)
    {
      if (heroIndex == -1)
      {
        textObj = this.resultOK;
        if (this.criticalSuccess)
          textObj = this.resultOKc;
      }
      else
        textObj = this.charTresultOK[heroIndex];
    }
    else if (heroIndex == -1)
    {
      textObj = this.resultKO;
      if (this.criticalFail)
        textObj = this.resultKOc;
    }
    else
      textObj = this.charTresultKO[heroIndex];
    Color colorOri = textObj.color;
    textObj.gameObject.SetActive(true);
    float alpha = colorOri.a;
    while ((double) alpha < 1.0)
    {
      alpha += 0.1f;
      textObj.color = new Color(colorOri.r, colorOri.g, colorOri.b, alpha);
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    }
  }

  private void TurnOffCharacter(int heroIndex) => this.characterSPR[heroIndex].color = new Color(0.3f, 0.3f, 0.3f, 1f);

  public void SetWaitingPlayersText(string msg)
  {
    if (msg != "")
    {
      this.waitingMsg.gameObject.SetActive(true);
      this.waitingMsgText.text = msg;
    }
    else
      this.waitingMsg.gameObject.SetActive(false);
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster() || !AtOManager.Instance.followingTheLeader)
      return;
    if (NetworkManager.Instance.IsMasterReady())
    {
      if (this.statusReady)
        return;
      this.Ready(true);
    }
    else
    {
      if (!this.statusReady)
        return;
      this.Ready(true);
    }
  }

  public void Ready(bool forceIt = false)
  {
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster() && AtOManager.Instance.followingTheLeader && !forceIt)
      return;
    if (!GameManager.Instance.IsMultiplayer() || (UnityEngine.Object) TownManager.Instance != (UnityEngine.Object) null)
    {
      this.CloseEvent();
    }
    else
    {
      if (this.manualReadyCo != null)
        this.StopCoroutine(this.manualReadyCo);
      this.statusReady = !this.statusReady;
      NetworkManager.Instance.SetManualReady(this.statusReady);
      if (this.statusReady)
      {
        this.continueButton.GetComponent<BotonGeneric>().SetBackgroundColor(Functions.HexToColor(Globals.Instance.ClassColor["scout"]));
        this.continueButton.GetComponent<BotonGeneric>().SetText(Texts.Instance.GetText("waitingForPlayers"));
        if (!NetworkManager.Instance.IsMaster())
          return;
        this.manualReadyCo = this.StartCoroutine(this.CheckForAllManualReady());
      }
      else
      {
        this.continueButton.GetComponent<BotonGeneric>().SetBackgroundColor(Functions.HexToColor(Globals.Instance.ClassColor["warrior"]));
        this.continueButton.GetComponent<BotonGeneric>().SetText(Texts.Instance.GetText("continueAction"));
      }
    }
  }

  private IEnumerator CheckForAllManualReady()
  {
    bool check = true;
    while (check)
    {
      if (!NetworkManager.Instance.AllPlayersManualReady())
        yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      else
        check = false;
    }
    this.photonView.RPC("NET_CloseEvent", RpcTarget.Others);
    this.CloseEvent();
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    int absolutePosition = -1)
  {
    this._controllerList.Clear();
    if (Functions.TransformIsVisible(this.continueButton))
      this._controllerList.Add(this.continueButton);
    else if (this.replysGOs.Length != 0)
    {
      for (int index = 0; index < this.replysGOs.Length; ++index)
      {
        if ((UnityEngine.Object) this.replysGOs[index] != (UnityEngine.Object) null)
        {
          this._controllerList.Add(this.replysGOs[index].transform);
          if (Functions.TransformIsVisible(this.replys[index].probDice))
            this._controllerList.Add(this.replys[index].probDice);
        }
      }
    }
    this._controllerList.Add(MapManager.Instance.eventShowHideButton);
    for (int index = 0; index < 4; ++index)
    {
      if (Functions.TransformIsVisible(MapManager.Instance.sideCharacters.charArray[index].transform))
        this._controllerList.Add(MapManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
    }
    if (Functions.TransformIsVisible(PlayerUIManager.Instance.giveGold))
      this._controllerList.Add(PlayerUIManager.Instance.giveGold);
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }
}
