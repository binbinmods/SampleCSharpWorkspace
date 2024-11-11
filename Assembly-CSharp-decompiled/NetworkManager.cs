// Decompiled with JetBrains decompiler
// Type: NetworkManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
  [SerializeField]
  public Dictionary<string, bool> PlayerReady;
  public Dictionary<string, Dictionary<string, bool>> PlayerStatusReady;
  [SerializeField]
  public Dictionary<string, bool> PlayerManualReady;
  [SerializeField]
  public Dictionary<string, bool> PlayerDivinationReady;
  private PhotonView photonView;
  private string gameVersion = "1";
  private bool onRoom;
  public Player[] PlayerList;
  public string[] PlayerPositionList;
  public string[] PlayerHeroPositionOwner;
  public string netAuxValue = "";
  public string Owner0 = "";
  public string Owner1 = "";
  public string Owner2 = "";
  public string Owner3 = "";
  public Dictionary<string, string> PlayerNickRealDict;
  public Dictionary<string, string> PlayerVersionDict;
  public Dictionary<string, List<string>> PlayerSkuList;
  private string realName = "";
  public string regionSelected = "";
  public Dictionary<string, bool> WaitingSyncro = new Dictionary<string, bool>();
  private PhotonLagSimulationGui lagSimulator;
  private PhotonStatsGui statsGui;
  public string WantToJoinRoomName = "";
  public List<string> waitingCalls = new List<string>();
  private static Timer aTimer;
  private int rrc;
  public int networkDisconnectAlert;
  private AuthTicket hAuthTicket;
  private Coroutine connTimeoutCo;

  public static NetworkManager Instance { get; private set; }

  public bool IsConnected() => PhotonNetwork.IsConnected;

  public string GetPing() => PhotonNetwork.IsConnected ? PhotonNetwork.GetPing().ToString() : "";

  private void Update()
  {
    if (!Globals.Instance.ShowDebug || !PhotonNetwork.IsConnected || Time.frameCount % 24 != 0 || this.rrc == PhotonNetwork.ResentReliableCommands)
      return;
    this.rrc = PhotonNetwork.ResentReliableCommands;
    Debug.LogError((object) ("PhotonNetwork.ResentReliableCommands=>" + this.rrc.ToString()));
  }

  private void Awake()
  {
    if ((UnityEngine.Object) NetworkManager.Instance == (UnityEngine.Object) null)
      NetworkManager.Instance = this;
    else if ((UnityEngine.Object) NetworkManager.Instance != (UnityEngine.Object) this)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    this.photonView = PhotonView.Get((Component) this);
    PhotonNetwork.LogLevel = PunLogLevel.ErrorsOnly;
    this.PlayerReady = new Dictionary<string, bool>();
    this.PlayerStatusReady = new Dictionary<string, Dictionary<string, bool>>();
    this.PlayerManualReady = new Dictionary<string, bool>();
    this.PlayerDivinationReady = new Dictionary<string, bool>();
    this.PlayerNickRealDict = new Dictionary<string, string>();
    this.PlayerVersionDict = new Dictionary<string, string>();
    this.PlayerSkuList = new Dictionary<string, List<string>>();
    this.lagSimulator = this.GetComponent<PhotonLagSimulationGui>();
    this.ShowLagSimulator(false);
    this.statsGui = this.GetComponent<PhotonStatsGui>();
    GameManager.Instance.SceneLoaded();
  }

  private void Start()
  {
    PhotonNetwork.SendRate = 20;
    PhotonNetwork.SerializationRate = 10;
    PhotonNetwork.UseRpcMonoBehaviourCache = true;
  }

  public void StartStopQueue(bool state)
  {
    if (!GameManager.Instance.IsMultiplayer() || !PhotonNetwork.IsConnected)
      return;
    PhotonNetwork.IsMessageQueueRunning = state;
  }

  public void ClearSyncro()
  {
    if (this.WaitingSyncro != null)
      this.WaitingSyncro.Clear();
    else
      this.WaitingSyncro = new Dictionary<string, bool>();
  }

  public void ClearPlayerStatus()
  {
    if (this.PlayerStatusReady != null)
      this.PlayerStatusReady.Clear();
    else
      this.PlayerStatusReady = new Dictionary<string, Dictionary<string, bool>>();
  }

  public bool IsSyncroClean()
  {
    if (this.WaitingSyncro.Count == 0)
      return true;
    foreach (KeyValuePair<string, bool> keyValuePair in this.WaitingSyncro)
    {
      if (keyValuePair.Value)
      {
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("*WAITING SYNCRO* " + keyValuePair.Key);
        return false;
      }
    }
    return true;
  }

  public void ShowLagSimulator(bool state) => this.lagSimulator.Visible = state;

  public void ShowLagSimulatorTrigger() => this.lagSimulator.Visible = !this.lagSimulator.Visible;

  public void JoinLobby() => PhotonNetwork.JoinLobby(TypedLobby.Default);

  public void AddPlayerSkuList(string _nick, List<string> _list)
  {
    if (this.PlayerSkuList == null)
      this.PlayerSkuList = new Dictionary<string, List<string>>();
    _nick = this.GetPlayerNickReal(_nick);
    if (this.PlayerSkuList.ContainsKey(_nick))
      this.PlayerSkuList[_nick] = _list;
    else
      this.PlayerSkuList.Add(_nick, _list);
    if (GameManager.Instance.GetDeveloperMode())
      Debug.Log((object) ("AddPlayerSkuList -> " + _nick));
    for (int index = 0; index < _list.Count; ++index)
    {
      if (GameManager.Instance.GetDeveloperMode())
        Debug.Log((object) ("** " + _list[index]));
    }
  }

  public bool PlayerHaveSku(string _nick, string _sku) => this.PlayerSkuList != null && this.PlayerSkuList.ContainsKey(_nick) && this.PlayerSkuList[_nick] != null && this.PlayerSkuList[_nick].Contains(_sku);

  public bool AllPlayersHaveSku(string _sku)
  {
    if (this.PlayerSkuList == null)
      return false;
    foreach (KeyValuePair<string, List<string>> playerSku in this.PlayerSkuList)
    {
      if (playerSku.Value == null)
        return false;
      if (!playerSku.Value.Contains(_sku))
      {
        if (GameManager.Instance.GetDeveloperMode())
          Debug.LogError((object) (playerSku.Key + " doesn't have sku " + _sku));
        return false;
      }
    }
    return true;
  }

  public bool AnyPlayersHaveSku(string _sku)
  {
    if (this.PlayerSkuList == null)
      return false;
    foreach (KeyValuePair<string, List<string>> playerSku in this.PlayerSkuList)
    {
      if (playerSku.Value != null && playerSku.Value.Contains(_sku))
      {
        if (GameManager.Instance.GetDeveloperMode())
          Debug.LogError((object) (playerSku.Key + " have sku " + _sku));
        return true;
      }
    }
    return false;
  }

  public bool AllPlayersHaveSkuList() => this.PlayerSkuList.Count == this.GetNumPlayers();

  public void Connect(string region = "")
  {
    if (PhotonNetwork.IsConnected)
    {
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("already connected");
      this.ConnectedToMaster();
    }
    else
    {
      this.PlayerNickRealDict.Clear();
      this.PlayerVersionDict.Clear();
      this.PlayerSkuList.Clear();
      if (SteamManager.Instance.steamName != "")
      {
        PhotonNetwork.NickName = this.realName = SteamManager.Instance.steamName.Replace(" ", "").Replace("_", "");
      }
      else
      {
        PhotonNetwork.NickName = PlayerManager.Instance.GetPlayerName();
        this.realName = PlayerManager.Instance.GetPlayerName();
      }
      PhotonNetwork.NickName = PhotonNetwork.NickName + "_" + Functions.RandomString(0.0f, 4f);
      if (!this.PlayerNickRealDict.ContainsKey(PhotonNetwork.NickName))
        this.PlayerNickRealDict.Add(PhotonNetwork.NickName, this.realName);
      if (!this.PlayerVersionDict.ContainsKey(PhotonNetwork.NickName))
        this.PlayerVersionDict.Add(PhotonNetwork.NickName, GameManager.Instance.gameVersion);
      PhotonNetwork.GameVersion = this.gameVersion;
      PhotonNetwork.NetworkingClient.ServerPortOverrides = PhotonPortDefinition.AlternativeUdpPorts;
      PhotonNetwork.QuickResends = 3;
      PhotonNetwork.MaxResendsBeforeDisconnect = 8;
      PhotonNetwork.NetworkingClient.LoadBalancingPeer.DisconnectTimeout = 60000;
      PhotonNetwork.NetworkingClient.LoadBalancingPeer.MaximumTransferUnit = 520;
      PhotonNetwork.KeepAliveInBackground = 60000f;
      PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region;
      this.regionSelected = region;
      string steamAuthTicket = this.GetSteamAuthTicket(out this.hAuthTicket);
      PhotonNetwork.AuthValues = new AuthenticationValues();
      PhotonNetwork.AuthValues.UserId = SteamManager.Instance.steamId.ToString();
      PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Steam;
      PhotonNetwork.AuthValues.AddAuthParameter("ticket", steamAuthTicket);
      PhotonNetwork.ConnectUsingSettings();
      this.connTimeoutCo = this.StartCoroutine(this.ConnectionTimeout());
    }
  }

  private IEnumerator ConnectionTimeout()
  {
    yield return (object) Globals.Instance.WaitForSeconds(15f);
    if ((UnityEngine.Object) LobbyManager.Instance.regionsDisconnect != (UnityEngine.Object) null && !LobbyManager.Instance.regionsDisconnect.gameObject.activeSelf)
      this.Disconnect();
  }

  public void Disconnect()
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD(nameof (Disconnect), "net");
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD(nameof (Disconnect));
    PhotonNetwork.Disconnect();
    this.regionSelected = "";
  }

  public void ChatSend(string _text, bool showAlertIfClosed) => this.photonView.RPC("GotChat", RpcTarget.Others, (object) Encoding.UTF8.GetBytes(_text), (object) showAlertIfClosed);

  [PunRPC]
  public void GotChat(byte[] _text, bool showAlertIfClosed) => ChatManager.Instance.ChatText(Encoding.UTF8.GetString(_text), showAlertIfClosed);

  public void CreateRoom(string roomName, string roomPlayers, string roomPassword = "", string lfm = "")
  {
    if (this.onRoom)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(roomName);
    stringBuilder.Append("<br><size=20><color=#A0A0A0>");
    if (AtOManager.Instance.GetGameId() != "")
      stringBuilder.Append(" <sprite name=disk> ");
    string str = "";
    if (GameManager.Instance.GameType == Enums.GameType.Adventure)
    {
      stringBuilder.Append(Texts.Instance.GetText("modeAdventure"));
      if (AtOManager.Instance.GetGameId() != "")
      {
        stringBuilder.Append(" <color=#666>|</color> ");
        str = string.Format(Texts.Instance.GetText("madnessNumber"), (object) AtOManager.Instance.GetMadnessDifficulty().ToString());
      }
    }
    else if (GameManager.Instance.GameType == Enums.GameType.Challenge)
    {
      stringBuilder.Append(Texts.Instance.GetText("modeObelisk"));
      if (AtOManager.Instance.GetGameId() != "")
      {
        stringBuilder.Append(" <color=#666>|</color> ");
        str = string.Format(Texts.Instance.GetText("madnessNumber"), (object) AtOManager.Instance.GetObeliskMadness().ToString());
      }
    }
    else
    {
      stringBuilder.Append(Texts.Instance.GetText("modeWeekly"));
      if (AtOManager.Instance.GetGameId() != "")
      {
        stringBuilder.Append(" <color=#666>|</color> ");
        str = AtOManager.Instance.GetWeeklyName(AtOManager.Instance.GetWeekly());
      }
    }
    if (AtOManager.Instance.GetGameId() == "" && GameManager.Instance.GameType != Enums.GameType.Adventure && GameManager.Instance.GameType != Enums.GameType.Challenge)
    {
      stringBuilder.Append(" <color=#666>|</color> ");
      str = AtOManager.Instance.GetWeeklyName(Functions.GetCurrentWeeklyWeek());
    }
    stringBuilder.Append(str);
    ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
    hashtable.Add((object) "pwd", (object) roomPassword);
    hashtable.Add((object) "creator", (object) this.GetPlayerNickReal(PhotonNetwork.NickName));
    hashtable.Add((object) "description", (object) stringBuilder.ToString());
    hashtable.Add((object) "status", (object) "lobby");
    hashtable.Add((object) "version", (object) GameManager.Instance.gameVersion.ToString());
    hashtable.Add((object) nameof (lfm), (object) lfm);
    roomName = Functions.RandomStringSafe(6f).ToUpper();
    string[] strArray = new string[6]
    {
      "pwd",
      "creator",
      "description",
      "status",
      "version",
      nameof (lfm)
    };
    string roomName1 = roomName;
    RoomOptions roomOptions = new RoomOptions();
    roomOptions.PlayerTtl = 1000;
    roomOptions.EmptyRoomTtl = 1000;
    roomOptions.MaxPlayers = byte.Parse(roomPlayers);
    roomOptions.CustomRoomProperties = hashtable;
    roomOptions.CustomRoomPropertiesForLobby = strArray;
    roomOptions.PublishUserId = true;
    roomOptions.IsOpen = true;
    roomOptions.IsVisible = true;
    TypedLobby typedLobby = TypedLobby.Default;
    PhotonNetwork.JoinOrCreateRoom(roomName1, roomOptions, typedLobby);
    this.PlayerHeroPositionOwner = new string[4];
  }

  public void JoinRoom(string roomName)
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("JoinRoom " + roomName);
    PhotonNetwork.JoinRoom(roomName);
  }

  public void JoinRoomByPreloadedSteam()
  {
    if (!(this.WantToJoinRoomName != ""))
      return;
    this.JoinRoom(this.WantToJoinRoomName);
    this.WantToJoinRoomName = "";
  }

  public void ExitRoom()
  {
    Debug.Log((object) nameof (ExitRoom));
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("Exitroom");
    this.CloseRoom();
    if (!PhotonNetwork.IsConnected || PhotonNetwork.CurrentRoom == null)
      return;
    PhotonNetwork.LeaveRoom();
  }

  public void LoadScene(string scene, bool showMask = true)
  {
    if (!PhotonNetwork.IsMasterClient)
      GlobalLog.Instance.Log("PhotonNetwork", "Trying to Load a level but we are not the master Client");
    int num = 0;
    if (GameManager.Instance.IsObeliskChallenge())
      num = !GameManager.Instance.IsWeeklyChallenge() ? 1 : 2;
    this.photonView.RPC("NET_LoadScene", RpcTarget.All, (object) scene, (object) showMask, (object) num);
  }

  [PunRPC]
  public void NET_LoadScene(string scene, bool showMask, int gameType)
  {
    UnityEngine.Object.Destroy((UnityEngine.Object) GameObject.Find("CardAmplifyOutside"));
    GameManager.Instance.SetMaskLoading();
    if (scene == "HeroSelection")
    {
      switch (gameType)
      {
        case 0:
          GameManager.Instance.GameType = Enums.GameType.Adventure;
          break;
        case 1:
          GameManager.Instance.GameType = Enums.GameType.Challenge;
          break;
        case 2:
          GameManager.Instance.GameType = Enums.GameType.WeeklyChallenge;
          break;
      }
    }
    SceneStatic.LoadByName(scene, showMask);
  }

  public string GetRoomDescription() => PhotonNetwork.NetworkingClient.CurrentRoom.CustomProperties[(object) "description"].ToString();

  public string GetRoomPassword()
  {
    string encrypted = PhotonNetwork.NetworkingClient.CurrentRoom.CustomProperties[(object) "pwd"].ToString();
    if (encrypted != "")
      encrypted = new SimplerAES().Decrypt(encrypted);
    return encrypted;
  }

  public string GetRoomName() => PhotonNetwork.NetworkingClient.CurrentRoom != null ? PhotonNetwork.NetworkingClient.CurrentRoom.Name : "";

  public int GetNumPlayers()
  {
    int numPlayers = 0;
    if (this.PlayerPositionList != null)
    {
      for (int index = 0; index < this.PlayerPositionList.Length; ++index)
      {
        if (this.PlayerPositionList[index] != null && this.PlayerPositionList[index] != "")
          ++numPlayers;
      }
    }
    return numPlayers;
  }

  public int GetMaxPlayers() => (int) PhotonNetwork.NetworkingClient.CurrentRoom.MaxPlayers;

  public Player GetPlayer() => PhotonNetwork.LocalPlayer;

  public bool VersionMatch()
  {
    for (int index = 0; index < this.PlayerPositionList.Length; ++index)
    {
      if (this.PlayerPositionList[index] != null && (this.PlayerVersionDict == null || !this.PlayerVersionDict.ContainsKey(this.PlayerPositionList[index]) || this.PlayerVersionDict[this.PlayerPositionList[index]] != GameManager.Instance.gameVersion))
        return false;
    }
    return true;
  }

  public string GetMyNickReal() => this.realName;

  public string GetPlayerNickReal(string nick) => nick != "" && nick != null && this.PlayerNickRealDict != null && this.PlayerNickRealDict.ContainsKey(nick) ? this.PlayerNickRealDict[nick] : nick;

  public string GetPlayerVersion(string nick) => nick != "" && nick != null && this.PlayerVersionDict != null && this.PlayerVersionDict.ContainsKey(nick) ? this.PlayerVersionDict[nick] : nick;

  public string GetPlayerNick() => PhotonNetwork.IsConnected ? this.GetPlayer().NickName : Globals.Instance.DefaultNickName;

  public string[] PlayerPositionListArray() => this.PlayerPositionList;

  public void NormalizePlayerPositionList()
  {
    int length = 0;
    for (int index = 0; index < this.PlayerPositionList.Length; ++index)
    {
      if (this.PlayerPositionList[index] != null && this.PlayerPositionList[index] != "")
        ++length;
    }
    if (length >= this.PlayerPositionList.Length)
      return;
    string[] strArray = new string[length];
    int index1 = 0;
    for (int index2 = 0; index2 < this.PlayerPositionList.Length; ++index2)
    {
      if (this.PlayerPositionList[index2] != null && this.PlayerPositionList[index2] != "")
      {
        strArray[index1] = this.PlayerPositionList[index2];
        ++index1;
      }
    }
    this.PlayerPositionList = new string[length];
    for (int index3 = 0; index3 < this.PlayerPositionList.Length; ++index3)
      this.PlayerPositionList[index3] = strArray[index3];
    this.photonView.RPC("NET_SharePlayerPositionList", RpcTarget.Others, (object) JsonHelper.ToJson<string>(this.PlayerPositionList));
  }

  public string GetPlayerNickPosition(int position) => this.PlayerPositionList != null && position > -1 && position < this.PlayerPositionList.Length ? this.PlayerPositionList[position] : "";

  public int GetPlayerListPosition(string nick)
  {
    int playerListPosition = -1;
    if (this.PlayerPositionList != null)
    {
      for (int index = 0; index < this.PlayerPositionList.Length; ++index)
      {
        if (this.PlayerPositionList[index] == nick)
        {
          playerListPosition = index;
          break;
        }
      }
    }
    return playerListPosition;
  }

  public int GetMyPosition() => this.GetPlayerListPosition(this.GetPlayerNick());

  public bool PlayerIsMaster(string _nick) => this.GetPlayerListPosition(_nick) == 0;

  public bool IsMaster() => PhotonNetwork.IsConnected && this.GetPlayer().IsMasterClient;

  public void OpenRoomDoors() => PhotonNetwork.CurrentRoom.IsOpen = true;

  public void CloseRoomDoors() => PhotonNetwork.CurrentRoom.IsOpen = false;

  public void CloseRoom(bool disconnect = false)
  {
    if (!PhotonNetwork.IsConnected)
      return;
    if (PhotonNetwork.IsMasterClient)
    {
      int num = (bool) (UnityEngine.Object) LobbyManager.Instance ? 1 : 0;
    }
    if (!disconnect)
      return;
    this.StartCoroutine(this.CoDisconnect());
  }

  private IEnumerator CoDisconnect()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    PhotonNetwork.Disconnect();
  }

  public void SetRoomStatus(string newStatus)
  {
  }

  public string GetColorFromNick(string nick) => this.ColorFromPosition(this.GetPlayerListPosition(nick));

  public string ColorFromPosition(int position)
  {
    string str;
    switch (position)
    {
      case 0:
        str = Globals.Instance.ClassColor["warrior"];
        break;
      case 1:
        str = Globals.Instance.ClassColor["mage"];
        break;
      case 2:
        str = Globals.Instance.ClassColor["scout"];
        break;
      default:
        str = Globals.Instance.ClassColor["magicknight"];
        break;
    }
    return str;
  }

  private void CreateStatus(string matchStatus)
  {
    Functions.DebugLogGD("[NET] CreateStatus " + matchStatus);
    if (this.PlayerStatusReady == null)
    {
      Functions.DebugLogGD("[NET] Null");
      this.PlayerStatusReady = new Dictionary<string, Dictionary<string, bool>>();
    }
    if (this.PlayerStatusReady.ContainsKey(matchStatus))
    {
      Functions.DebugLogGD("[NET] Contains");
      if (this.PlayerStatusReady[matchStatus] == null)
        this.PlayerStatusReady[matchStatus] = new Dictionary<string, bool>();
      else
        this.PlayerStatusReady[matchStatus].Clear();
    }
    else
    {
      Functions.DebugLogGD("[NET] Not contains");
      this.PlayerStatusReady.Add(matchStatus, new Dictionary<string, bool>());
    }
    string playerNick = this.GetPlayerNick();
    foreach (Player player in this.PlayerList)
      this.PlayerStatusReady[matchStatus].Add(player.NickName, player.NickName == playerNick);
  }

  public void ClearAllPlayerReadyStatus(string matchStatus)
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("Clearing all players status ->" + matchStatus);
    if (this.PlayerStatusReady == null || !this.PlayerStatusReady.ContainsKey(matchStatus))
      this.CreateStatus(matchStatus);
    else
      this.PlayerStatusReady[matchStatus].Clear();
  }

  public void SetStatusReady(string matchStatus)
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("SetPlayerStatusReady " + matchStatus);
    this.photonView.RPC("NET_SetPlayerStatusReady", RpcTarget.MasterClient, (object) true, (object) matchStatus, (object) (byte) this.GetPlayerListPosition(this.GetPlayerNick()));
  }

  [PunRPC]
  private void NET_SetPlayerStatusReady(bool status, string matchStatus, byte _position)
  {
    string playerNickPosition = this.GetPlayerNickPosition((int) _position);
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD(playerNickPosition + " was about to set ready for " + matchStatus, "net");
    if (this.PlayerStatusReady == null || !this.PlayerStatusReady.ContainsKey(matchStatus) || this.PlayerStatusReady[matchStatus] == null)
    {
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("dictionary is not ready", "net");
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD(playerNickPosition + " != " + this.GetPlayerNick(), "net");
      if (playerNickPosition != this.GetPlayerNick())
      {
        string str = playerNickPosition + "%" + matchStatus;
        if (!this.waitingCalls.Contains(str))
        {
          Debug.LogWarning((object) ("SAVED FOR LATER " + str));
          this.waitingCalls.Add(str);
        }
        else
        {
          if (!Globals.Instance.ShowDebug)
            return;
          Functions.DebugLogGD("waitingCalls already contains" + str, "net");
        }
      }
      else
      {
        if (!Globals.Instance.ShowDebug)
          return;
        Functions.DebugLogGD(playerNickPosition + " == " + this.GetPlayerNick(), "net");
      }
    }
    else if (this.PlayerStatusReady.ContainsKey(matchStatus))
    {
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("PlayerStatusReady contains " + matchStatus, "net");
      if (this.PlayerStatusReady[matchStatus] == null)
      {
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("status " + matchStatus + " was NULL", "net");
        this.PlayerStatusReady[matchStatus] = new Dictionary<string, bool>();
      }
      if (this.PlayerStatusReady[matchStatus].ContainsKey(playerNickPosition))
      {
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("status " + matchStatus + " modified for " + playerNickPosition + " -> " + status.ToString(), "net");
        this.PlayerStatusReady[matchStatus][playerNickPosition] = status;
      }
      else
      {
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("status " + matchStatus + " created for " + playerNickPosition + " -> " + status.ToString(), "net");
        this.PlayerStatusReady[matchStatus].Add(playerNickPosition, status);
      }
    }
    else
    {
      if (!Globals.Instance.ShowDebug)
        return;
      Functions.DebugLogGD("PlayerStatusReady not containskey " + matchStatus, "net");
    }
  }

  public bool AllPlayersReady(string matchStatus)
  {
    if (this.PlayerStatusReady == null || !this.PlayerStatusReady.ContainsKey(matchStatus) || this.PlayerStatusReady[matchStatus] == null)
    {
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("PlayerStatusReady Not Exist. Create matchStatus -> " + matchStatus, "net");
      this.CreateStatus(matchStatus);
    }
    for (int index1 = this.waitingCalls.Count - 1; index1 >= 0; --index1)
    {
      string[] strArray = this.waitingCalls[index1].Split('%', StringSplitOptions.None);
      string key = "";
      for (int index2 = 0; index2 < strArray.Length - 1; ++index2)
      {
        key += strArray[index2];
        if (index2 < strArray.Length - 2)
          key += "%";
      }
      if (strArray[strArray.Length - 1] == matchStatus)
      {
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("[AllPlayerSReady] Insert " + this.waitingCalls[index1], "net");
        if (this.PlayerStatusReady[matchStatus].ContainsKey(key))
          this.PlayerStatusReady[matchStatus][key] = true;
        else
          this.PlayerStatusReady[matchStatus].Add(key, true);
      }
    }
    if (this.PlayerStatusReady[matchStatus].Count < this.PlayerList.Length - 1)
      return false;
    string playerNick = this.GetPlayerNick();
    foreach (KeyValuePair<string, bool> keyValuePair in this.PlayerStatusReady[matchStatus])
    {
      if (!keyValuePair.Value && keyValuePair.Key != playerNick)
        return false;
    }
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("Clear PlayerStatusReady for " + matchStatus, "net");
    this.PlayerStatusReady[matchStatus].Clear();
    this.ClearWaitingCalls(matchStatus);
    return true;
  }

  public void ClearWaitingCalls(string _matchStatus = "")
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("Clear Waiting Calls ->  " + _matchStatus, "net");
    if (_matchStatus == "")
    {
      this.waitingCalls.Clear();
    }
    else
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("%");
      stringBuilder.Append(_matchStatus);
      for (int index = this.waitingCalls.Count - 1; index >= 0; --index)
      {
        if (this.waitingCalls[index].Contains(stringBuilder.ToString()))
        {
          if (Globals.Instance.ShowDebug)
            Functions.DebugLogGD("Clear Waiting Calls (REMOVED) ->  " + this.waitingCalls[index], "net");
          this.waitingCalls.RemoveAt(index);
        }
      }
    }
  }

  public void PlayersNetworkContinue(string key, string auxValue = "") => this.photonView.RPC("NET_NetworkContinue", RpcTarget.Others, (object) key, (object) auxValue);

  [PunRPC]
  public void NET_NetworkContinue(string key, string auxValue)
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("Received continue sync signal from master -> " + key, "net");
    this.netAuxValue = auxValue;
    this.SetWaitingSyncro(key, false);
  }

  public void SetWaitingSyncro(string key, bool status)
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("SetWaitingSyncro " + key + " -> " + status.ToString());
    if (this.WaitingSyncro == null)
      this.WaitingSyncro = new Dictionary<string, bool>();
    if (this.WaitingSyncro.ContainsKey(key))
      this.WaitingSyncro[key] = status;
    else
      this.WaitingSyncro.Add(key, status);
  }

  public void ClearAllPlayerManualReady()
  {
    this.PlayerManualReady.Clear();
    if (this.PlayerList == null)
      return;
    foreach (Player player in this.PlayerList)
      this.PlayerManualReady.Add(player.NickName, false);
  }

  public void SetManualReady(bool status) => this.photonView.RPC("NET_SetPlayerManualReady", RpcTarget.MasterClient, (object) status, (object) this.GetPlayerNick());

  [PunRPC]
  private void NET_SetPlayerManualReady(bool status, string nickname)
  {
    this.PlayerManualReady[nickname] = status;
    this.DoReadyStatus();
    string[] array1 = new string[this.PlayerManualReady.Count];
    this.PlayerManualReady.Keys.CopyTo(array1, 0);
    bool[] array2 = new bool[this.PlayerManualReady.Count];
    this.PlayerManualReady.Values.CopyTo(array2, 0);
    this.photonView.RPC("NET_SharePlayerManualReady", RpcTarget.Others, (object) JsonHelper.ToJson<string>(array1), (object) JsonHelper.ToJson<bool>(array2));
  }

  [PunRPC]
  private void NET_SharePlayerManualReady(string _keys, string _values)
  {
    this.PlayerManualReady = new Dictionary<string, bool>();
    string[] strArray = JsonHelper.FromJson<string>(_keys);
    bool[] flagArray = JsonHelper.FromJson<bool>(_values);
    for (int index = 0; index < strArray.Length; ++index)
      this.PlayerManualReady.Add(strArray[index], flagArray[index]);
    this.DoReadyStatus();
  }

  public bool IsPlayerReady(string _nick)
  {
    foreach (KeyValuePair<string, bool> keyValuePair in this.PlayerManualReady)
    {
      if (keyValuePair.Value && _nick == keyValuePair.Key)
        return true;
    }
    return false;
  }

  public bool IsMasterReady()
  {
    foreach (KeyValuePair<string, bool> keyValuePair in this.PlayerManualReady)
    {
      if (keyValuePair.Value && this.PlayerIsMaster(keyValuePair.Key))
        return true;
    }
    return false;
  }

  private void DoReadyStatus()
  {
    int ready = 0;
    foreach (KeyValuePair<string, bool> keyValuePair in this.PlayerManualReady)
    {
      if (keyValuePair.Value)
        ++ready;
    }
    if ((UnityEngine.Object) HeroSelectionManager.Instance != (UnityEngine.Object) null)
      HeroSelectionManager.Instance.SetPlayersReady();
    else if ((UnityEngine.Object) TownManager.Instance != (UnityEngine.Object) null)
      TownManager.Instance.SetWaitingPlayersText(this.GetWaitingPlayersString(ready, this.PlayerManualReady.Count));
    else if ((UnityEngine.Object) ChallengeSelectionManager.Instance != (UnityEngine.Object) null)
      ChallengeSelectionManager.Instance.SetWaitingPlayersText(this.GetWaitingPlayersString(ready, this.PlayerManualReady.Count));
    else if ((UnityEngine.Object) CardCraftManager.Instance != (UnityEngine.Object) null)
    {
      CardCraftManager.Instance.SetWaitingPlayersText(this.GetWaitingPlayersString(ready, this.PlayerManualReady.Count));
    }
    else
    {
      if (!((UnityEngine.Object) EventManager.Instance != (UnityEngine.Object) null))
        return;
      EventManager.Instance.SetWaitingPlayersText(this.GetWaitingPlayersString(ready, this.PlayerManualReady.Count));
    }
  }

  public string GetWaitingPlayersString(int ready, int total)
  {
    if (ready <= 0)
      return "";
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("(");
    stringBuilder.Append(ready);
    stringBuilder.Append("/");
    stringBuilder.Append(total);
    stringBuilder.Append(")");
    string str = stringBuilder.ToString();
    stringBuilder.Clear();
    stringBuilder.Append(Texts.Instance.GetText("playersReady"));
    stringBuilder.Append(" ");
    stringBuilder.Append(str);
    return stringBuilder.ToString();
  }

  public bool AllPlayersManualReady()
  {
    foreach (KeyValuePair<string, bool> keyValuePair in this.PlayerManualReady)
    {
      if (!keyValuePair.Value)
        return false;
    }
    return true;
  }

  public void ClearAllPlayerDivinationReady()
  {
    this.PlayerDivinationReady.Clear();
    foreach (Player player in this.PlayerList)
      this.PlayerDivinationReady.Add(player.NickName, false);
  }

  public void SetPlayerDivinationReady(string nickname)
  {
    this.PlayerDivinationReady[nickname] = true;
    bool flag = true;
    foreach (KeyValuePair<string, bool> keyValuePair in this.PlayerDivinationReady)
    {
      if (!keyValuePair.Value)
      {
        flag = false;
        break;
      }
    }
    if (flag)
    {
      AtOManager.Instance.LaunchRewards(isFromDivination: true);
    }
    else
    {
      string[] array1 = new string[this.PlayerDivinationReady.Count];
      this.PlayerDivinationReady.Keys.CopyTo(array1, 0);
      bool[] array2 = new bool[this.PlayerDivinationReady.Count];
      this.PlayerDivinationReady.Values.CopyTo(array2, 0);
      this.photonView.RPC("NET_SharePlayerDivinationReady", RpcTarget.Others, (object) JsonHelper.ToJson<string>(array1), (object) JsonHelper.ToJson<bool>(array2));
      AtOManager.Instance.UpdateDivinationStatus();
    }
  }

  [PunRPC]
  private void NET_SharePlayerDivinationReady(string _keys, string _values)
  {
    this.PlayerDivinationReady = new Dictionary<string, bool>();
    string[] strArray = JsonHelper.FromJson<string>(_keys);
    bool[] flagArray = JsonHelper.FromJson<bool>(_values);
    for (int index = 0; index < strArray.Length; ++index)
      this.PlayerDivinationReady.Add(strArray[index], flagArray[index]);
    AtOManager.Instance.UpdateDivinationStatus();
  }

  public override void OnLeftRoom()
  {
    Debug.Log((object) nameof (OnLeftRoom));
    this.onRoom = false;
    if ((UnityEngine.Object) ChatManager.Instance != (UnityEngine.Object) null)
      ChatManager.Instance.DisableChat();
    if (!((UnityEngine.Object) LobbyManager.Instance != (UnityEngine.Object) null) || !PhotonNetwork.IsConnected)
      return;
    LobbyManager.Instance.ShowJoin();
  }

  public override void OnCreatedRoom()
  {
    this.onRoom = true;
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("OnCreatedRoom() called by PUN.");
    this.CreatePlayerPositions(this.GetMaxPlayers());
    this.photonView.RPC("SetPlayerPosition", RpcTarget.MasterClient, (object) this.realName, (object) this.GetPlayerNick(), (object) GameManager.Instance.gameVersion);
  }

  public override void OnJoinedRoom()
  {
    this.onRoom = false;
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("OnJoinedRoom() called by PUN. Now this client is in a room.");
    ChatManager.Instance.EnableChat();
    ChatManager.Instance.WelcomeMsg(this.GetRoomName());
    LobbyManager.Instance.ShowRoom();
    LobbyManager.Instance.RemoveAllRooms();
    this.photonView.RPC("SetPlayerPosition", RpcTarget.MasterClient, (object) this.realName, (object) this.GetPlayerNick(), (object) GameManager.Instance.gameVersion);
  }

  private void OnJoinRoomFailed(object[] codeAndMsg) => Debug.LogErrorFormat("Room join failed with error code {0} and error message {1}", codeAndMsg[0], codeAndMsg[1]);

  public void CreatePlayerPositions(int numPlayers) => this.PlayerPositionList = new string[numPlayers];

  [PunRPC]
  public void SetPlayerPosition(string playerNickReal, string playerNick, string gameVersion = "0.0.0")
  {
    bool flag = false;
    for (int index = 0; index < this.PlayerPositionList.Length; ++index)
    {
      if (this.PlayerPositionList[index] == playerNick)
      {
        flag = true;
        break;
      }
    }
    if (!flag)
    {
      for (int index = 0; index < this.PlayerPositionList.Length; ++index)
      {
        if (this.PlayerPositionList[index] == "" || this.PlayerPositionList[index] == null)
        {
          this.PlayerPositionList[index] = playerNick;
          break;
        }
      }
    }
    if (!this.PlayerNickRealDict.ContainsKey(playerNick))
      this.PlayerNickRealDict.Add(playerNick, playerNickReal);
    if (!this.PlayerVersionDict.ContainsKey(playerNick))
      this.PlayerVersionDict.Add(playerNick, gameVersion);
    string[] array1 = new string[this.PlayerNickRealDict.Count];
    this.PlayerNickRealDict.Keys.CopyTo(array1, 0);
    string[] array2 = new string[this.PlayerNickRealDict.Count];
    this.PlayerNickRealDict.Values.CopyTo(array2, 0);
    string json1 = JsonHelper.ToJson<string>(array1);
    string json2 = JsonHelper.ToJson<string>(array2);
    string[] array3 = new string[this.PlayerVersionDict.Count];
    this.PlayerVersionDict.Values.CopyTo(array3, 0);
    string json3 = JsonHelper.ToJson<string>(array3);
    this.photonView.RPC("NET_SharePlayerNickReal", RpcTarget.Others, (object) json1, (object) json2, (object) json3);
    this.photonView.RPC("NET_SharePlayerPositionList", RpcTarget.Others, (object) JsonHelper.ToJson<string>(this.PlayerPositionList));
    if (playerNick != this.GetPlayerNick())
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<color=");
      stringBuilder.Append(this.GetColorFromNick(playerNick));
      stringBuilder.Append(">");
      stringBuilder.Append(this.GetPlayerNickReal(playerNick));
      stringBuilder.Append("</color>");
      this.ChatSend(Texts.Instance.GetText("chatPlayerJoined").Replace("%p", stringBuilder.ToString()), false);
    }
    this.SetLobbyPlayersData();
  }

  public bool IsPlayerPositionZero(string playerNick) => this.PlayerPositionList != null && this.PlayerPositionList[0] != null && this.PlayerPositionList[0] == playerNick;

  public void RemovePlayerPosition(string playerNick)
  {
    Debug.Log((object) ("RemovePlayerPosition " + playerNick));
    if (this.PlayerPositionList != null)
    {
      for (int index = 0; index < this.PlayerPositionList.Length; ++index)
      {
        if (this.PlayerPositionList[index] != null && this.PlayerPositionList[index] == playerNick)
        {
          this.PlayerPositionList[index] = "";
          break;
        }
      }
      foreach (KeyValuePair<string, string> keyValuePair in this.PlayerNickRealDict)
      {
        if (keyValuePair.Key == playerNick)
        {
          this.PlayerNickRealDict.Remove(keyValuePair.Key);
          break;
        }
      }
      foreach (KeyValuePair<string, string> keyValuePair in this.PlayerVersionDict)
      {
        if (keyValuePair.Key == playerNick)
        {
          this.PlayerVersionDict.Remove(keyValuePair.Key);
          break;
        }
      }
      this.photonView.RPC("NET_SharePlayerPositionList", RpcTarget.Others, (object) JsonHelper.ToJson<string>(this.PlayerPositionList));
    }
    if (!((UnityEngine.Object) LobbyManager.Instance != (UnityEngine.Object) null))
      return;
    this.SetLobbyPlayersData();
  }

  [PunRPC]
  public void NET_SharePlayerPositionList(string _PlayerPositionList)
  {
    this.PlayerPositionList = JsonHelper.FromJson<string>(_PlayerPositionList);
    this.SetLobbyPlayersData();
  }

  [PunRPC]
  public void NET_SharePlayerNickReal(string _keys, string _values, string _versions)
  {
    string[] strArray1 = JsonHelper.FromJson<string>(_keys);
    string[] strArray2 = JsonHelper.FromJson<string>(_values);
    string[] strArray3 = JsonHelper.FromJson<string>(_versions);
    this.PlayerNickRealDict = new Dictionary<string, string>();
    this.PlayerVersionDict = new Dictionary<string, string>();
    for (int index = 0; index < strArray1.Length; ++index)
    {
      this.PlayerNickRealDict.Add(strArray1[index], strArray2[index]);
      this.PlayerVersionDict.Add(strArray1[index], strArray3[index]);
    }
  }

  public void KickPlayer(int position, bool sendKickMsg = true)
  {
    int num = 0;
    foreach (Player player in PhotonNetwork.PlayerList)
    {
      if (num == position)
      {
        switch (player.NickName.ToLower().Split('_', StringSplitOptions.None)[0])
        {
          case "dreamsitegames":
            return;
          case "rhin":
            return;
          default:
            if (sendKickMsg)
            {
              StringBuilder stringBuilder = new StringBuilder();
              stringBuilder.Append("<color=");
              stringBuilder.Append(this.GetColorFromNick(player.NickName));
              stringBuilder.Append(">");
              stringBuilder.Append(this.GetPlayerNickReal(player.NickName));
              stringBuilder.Append("</color>");
              this.ChatSend(Texts.Instance.GetText("chatPlayerKicked").Replace("%p", stringBuilder.ToString()), false);
              this.photonView.RPC("BeenKicked", RpcTarget.Others, (object) player.NickName);
            }
            this.RemovePlayerPosition(player.NickName);
            return;
        }
      }
      else
        ++num;
    }
  }

  [PunRPC]
  private void BeenKicked(string nick)
  {
    if (!(this.GetPlayerNick() == nick))
      return;
    if (PhotonNetwork.IsConnected && PhotonNetwork.CurrentRoom != null)
      PhotonNetwork.LeaveRoom();
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.BeenKickedAction);
    AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("masterKickedYou"));
  }

  private void BeenKickedAction() => AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.BeenKickedAction);

  public void AssignHeroPlayerPositionOwner(int id, string nickName)
  {
    if (this.PlayerHeroPositionOwner == null)
      this.PlayerHeroPositionOwner = new string[4];
    this.PlayerHeroPositionOwner[id] = nickName;
  }

  public void SetLobbyPlayersData() => LobbyManager.Instance.SetLobbyPlayersData(PhotonNetwork.PlayerList);

  public override void OnPlayerEnteredRoom(Player other)
  {
    Debug.LogFormat("OnPlayerEnteredRoom() {0}", (object) other.NickName);
    if (!PhotonNetwork.IsMasterClient)
      return;
    if (this.PlayerReady.ContainsKey(other.NickName))
      this.PlayerReady[other.NickName] = false;
    else
      this.PlayerReady.Add(other.NickName, false);
    if (!Globals.Instance.ShowDebug)
      return;
    Functions.DebugLogGD(other.NickName + " added to PlayerReady array");
  }

  public override void OnPlayerLeftRoom(Player other)
  {
    Debug.LogFormat("OnPlayerLeftRoom() {0}", (object) other.NickName);
    string sceneName = SceneStatic.GetSceneName();
    switch (sceneName)
    {
      case "FinishRun":
        return;
      case "IntroNewGame":
        if (AtOManager.Instance.GetTownTier() == 3)
          return;
        break;
    }
    if (this.IsPlayerPositionZero(other.NickName))
    {
      Debug.Log((object) "leaveRoom");
      PhotonNetwork.LeaveRoom();
      AtOManager.Instance.ClearGame();
      if ((bool) (UnityEngine.Object) LobbyManager.Instance)
        return;
      this.networkDisconnectAlert = 1;
      if (SaveManager.PrefsHasKey("coopRoomId"))
        SaveManager.PrefsRemoveKey("coopRoomId");
      SceneStatic.LoadByName("Lobby");
    }
    else
    {
      if (PhotonNetwork.IsMasterClient && (bool) (UnityEngine.Object) LobbyManager.Instance)
        this.RemovePlayerPosition(other.NickName);
      if (!((UnityEngine.Object) LobbyManager.Instance == (UnityEngine.Object) null) || !(AtOManager.Instance.GetGameId() != "") && !(sceneName == "HeroSelection"))
        return;
      AtOManager.Instance.ClearGame();
      this.RemovePlayerPosition(other.NickName);
      this.networkDisconnectAlert = 2;
      if (PhotonNetwork.IsMasterClient)
      {
        if ((sceneName == "HeroSelection" || sceneName == "ChallengeSelection") && !GameManager.Instance.IsLoadingGame())
          SceneStatic.LoadByName("Lobby");
        else
          AtOManager.Instance.LoadGame(AtOManager.Instance.GetSaveSlot());
      }
      else
        SceneStatic.LoadByName("Lobby");
    }
  }

  public override void OnJoinedLobby()
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD(nameof (OnJoinedLobby));
    if (this.connTimeoutCo != null)
      this.StopCoroutine(this.connTimeoutCo);
    LobbyManager.Instance.JustConnectedToPhoton();
  }

  public override void OnRoomListUpdate(List<RoomInfo> _roomList)
  {
    foreach (RoomInfo room in _roomList)
      LobbyManager.Instance.RoomReceived(room);
    LobbyManager.Instance.RemoveOldRooms();
  }

  public override void OnConnectedToMaster()
  {
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("OnConnectedToMaster() was called by PUN");
    this.ConnectedToMaster();
  }

  private void ConnectedToMaster()
  {
    LobbyManager.Instance.SetStatus(true, Texts.Instance.GetText("statusConnected"));
    LobbyManager.Instance.RemoveAllRooms();
    this.JoinLobby();
    this.CancelAuthTicket(this.hAuthTicket);
  }

  public void ClearPreviousInfo()
  {
    this.PlayerList = (Player[]) null;
    this.PlayerPositionList = (string[]) null;
    this.PlayerHeroPositionOwner = (string[]) null;
  }

  public override void OnDisconnected(DisconnectCause cause)
  {
    Debug.LogFormat("OnDisconnected() was called by PUN with reason {0}", (object) cause);
    this.onRoom = false;
    if (cause == DisconnectCause.CustomAuthenticationFailed)
    {
      this.regionSelected = "";
      LobbyManager.Instance.InitLobby();
    }
    else if ((bool) (UnityEngine.Object) LobbyManager.Instance)
    {
      LobbyManager.Instance.InitLobby();
    }
    else
    {
      if ((bool) (UnityEngine.Object) MainMenuManager.Instance)
        return;
      SceneStatic.LoadByName("MainMenu");
    }
  }

  public override void OnJoinRandomFailed(short returnCode, string message)
  {
    if (!Globals.Instance.ShowDebug)
      return;
    Functions.DebugLogGD("OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
  }

  public override void OnCustomAuthenticationFailed(string errorMessage)
  {
    Debug.Log((object) ("Authentication Failed: " + errorMessage));
    this.CancelAuthTicket(this.hAuthTicket);
    AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("steamErrorConnect"));
  }

  public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
  {
    Debug.Log((object) "Authentication Response");
    foreach (KeyValuePair<string, object> keyValuePair in data)
      Debug.Log((object) (keyValuePair.Key + "=>" + keyValuePair.Value?.ToString()));
  }

  public string GetSteamAuthTicket(out AuthTicket authTicket)
  {
    authTicket = SteamUser.GetAuthSessionTicket();
    if (authTicket == null || authTicket.Data == null)
      return "";
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < authTicket.Data.Length; ++index)
      stringBuilder.AppendFormat("{0:x2}", (object) authTicket.Data[index]);
    return stringBuilder.ToString();
  }

  private void CancelAuthTicket(AuthTicket ticket) => ticket?.Cancel();
}
