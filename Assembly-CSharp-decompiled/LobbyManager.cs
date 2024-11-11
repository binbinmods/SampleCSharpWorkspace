// Decompiled with JetBrains decompiler
// Type: LobbyManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
  public TMP_Dropdown dropRegion;
  public TMP_Text statusTM;
  public Transform buttonLaunch;
  public Transform buttonSteam;
  public Transform regions;
  public Transform regionsDisconnect;
  public GameObject RoomPrefab;
  public Transform CreateRoomT;
  public Transform JoinRoomT;
  public Transform RoomT;
  public Transform GridTransform;
  public TMP_InputField UICreateName;
  public TMP_Dropdown UICreatePlayers;
  public TMP_InputField UICreatePwd;
  public Toggle UITogglePwd;
  public Toggle UIToggleLfm;
  public TMP_Text roomTitle;
  public TMP_Text roomWaiting;
  public TMP_Text[] roomSlots;
  public Image[] roomSlotsImage;
  public Transform[] roomSlotsKick;
  private RoomInfo roomInfo;
  private string roomPassword = "";
  private string roomName = "";
  private bool automaticJoin;
  private List<RoomList> _roomListButtons = new List<RoomList>();
  public Transform[] buttonsController;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();

  public static LobbyManager Instance { get; private set; }

  private List<RoomList> RoomListButtons => this._roomListButtons;

  private void Awake()
  {
    if ((UnityEngine.Object) GameManager.Instance == (UnityEngine.Object) null)
    {
      SceneStatic.LoadByName("Lobby");
    }
    else
    {
      if ((UnityEngine.Object) LobbyManager.Instance == (UnityEngine.Object) null)
        LobbyManager.Instance = this;
      else if ((UnityEngine.Object) LobbyManager.Instance != (UnityEngine.Object) this)
        UnityEngine.Object.Destroy((UnityEngine.Object) this);
      GameManager.Instance.SetCamera();
      NetworkManager.Instance.StartStopQueue(true);
      GameManager.Instance.SceneLoaded();
      this.automaticJoin = true;
      this.ShowJoin();
    }
  }

  private void Start()
  {
    if (PhotonNetwork.IsConnected && NetworkManager.Instance.GetRoomName() != "")
    {
      this.ShowRoom();
      this.regions.gameObject.SetActive(false);
      this.ShowConnectedStatus();
      if (PhotonNetwork.IsMasterClient)
        NetworkManager.Instance.OpenRoomDoors();
      if (NetworkManager.Instance.networkDisconnectAlert == 1)
      {
        AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("abortMultiplayerMaster"));
        AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.LobbyAlertFinishAction);
      }
      else if (NetworkManager.Instance.networkDisconnectAlert == 2)
      {
        AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("abortMultiplayerPlayer"));
        AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.LobbyAlertFinishAction);
      }
    }
    else
      this.InitLobby();
    NetworkManager.Instance.networkDisconnectAlert = 0;
    AudioManager.Instance.DoBSO("Game");
    AudioManager.Instance.StopAmbience();
  }

  private void LobbyAlertFinishAction() => AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.LobbyAlertFinishAction);

  public void InitLobby()
  {
    Debug.Log((object) nameof (InitLobby));
    GameManager.Instance.GameMode = Enums.GameMode.Multiplayer;
    this.RoomT.gameObject.SetActive(false);
    this.CreateRoomT.gameObject.SetActive(false);
    this.JoinRoomT.gameObject.SetActive(false);
    this.regionsDisconnect.gameObject.SetActive(false);
    this.SetStatus(true);
    if (NetworkManager.Instance.regionSelected != "")
    {
      this.SetRegion(NetworkManager.Instance.regionSelected);
    }
    else
    {
      this.regions.gameObject.SetActive(true);
      if (SaveManager.PrefsHasKey("networkRegion"))
      {
        this.dropRegion.value = SaveManager.LoadPrefsInt("networkRegion");
        if (this.automaticJoin && !PhotonNetwork.IsConnected)
          this.StartCoroutine(this.SelectRegionCo());
      }
      else
      {
        this.automaticJoin = false;
        return;
      }
    }
    this.automaticJoin = false;
  }

  public void DisconnectRegion(bool _fromButton = false)
  {
    NetworkManager.Instance.Disconnect();
    if (!_fromButton || !SaveManager.PrefsHasKey("networkRegion"))
      return;
    SaveManager.PrefsRemoveKey("networkRegion");
  }

  private IEnumerator SelectRegionCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    this.SelectRegion();
  }

  public void SelectRegion()
  {
    string regionName;
    switch (this.dropRegion.value)
    {
      case 0:
        regionName = "asia";
        break;
      case 1:
        regionName = "au";
        break;
      case 2:
        regionName = "cae";
        break;
      case 3:
        regionName = "eu";
        break;
      case 4:
        regionName = "in";
        break;
      case 5:
        regionName = "jp";
        break;
      case 6:
        regionName = "ru";
        break;
      case 7:
        regionName = "rue";
        break;
      case 8:
        regionName = "za";
        break;
      case 9:
        regionName = "sa";
        break;
      case 10:
        regionName = "kr";
        break;
      case 11:
        regionName = "us";
        break;
      case 12:
        regionName = "usw";
        break;
      default:
        regionName = "";
        break;
    }
    if (!(regionName != ""))
      return;
    this.SetRegion(regionName);
  }

  public void SetRegion(string regionName)
  {
    this.regions.gameObject.SetActive(false);
    SaveManager.SaveIntoPrefsInt("networkRegion", this.dropRegion.value);
    NetworkManager.Instance.Connect(regionName);
    this.SetStatus(true, Texts.Instance.GetText("statusConnecting"));
  }

  private void GetPlayerName()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.GetPlayerName);
    AlertManager.Instance.GetInputValue();
  }

  private void ConnectToPhoton()
  {
    Debug.Log((object) nameof (ConnectToPhoton));
    NetworkManager.Instance.Connect();
  }

  public void JustConnectedToPhoton()
  {
    if (NetworkManager.Instance.WantToJoinRoomName != "")
      NetworkManager.Instance.JoinRoomByPreloadedSteam();
    else if (AtOManager.Instance.GetSaveSlot() > -1)
      this.ShowCreate();
    else
      this.ShowJoin();
    this.ShowConnectedStatus();
    if (!((UnityEngine.Object) this.regionsDisconnect != (UnityEngine.Object) null))
      return;
    this.regionsDisconnect.gameObject.SetActive(true);
  }

  private void ShowReconnectAlert()
  {
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.AutomaticJoinId);
    AlertManager.Instance.AlertConfirmDouble(Texts.Instance.GetText("joinOldGame"), Texts.Instance.GetText("accept").ToUpper(), Texts.Instance.GetText("cancel").ToUpper());
  }

  public void AutomaticJoinId()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.AutomaticJoinId);
    if (AlertManager.Instance.GetConfirmAnswer())
      NetworkManager.Instance.JoinRoom(SaveManager.LoadPrefsString("coopRoomId"));
    SaveManager.PrefsRemoveKey("coopRoomId");
  }

  private void ShowConnectedStatus() => this.SetStatus(true, string.Format(Texts.Instance.GetText("connectedRegion"), (object) NetworkManager.Instance.regionSelected.ToUpper()));

  public void ShowCreate()
  {
    Functions.DebugLogGD(nameof (ShowCreate), "trace");
    if (!((UnityEngine.Object) this.CreateRoomT != (UnityEngine.Object) null))
      return;
    this.CreateRoomT.gameObject.SetActive(true);
    this.JoinRoomT.gameObject.SetActive(false);
    if (!SaveManager.PrefsHasKey("LobbyRoomName"))
      return;
    this.UICreateName.text = SaveManager.LoadPrefsString("LobbyRoomName");
  }

  public void ShowJoin()
  {
    Functions.DebugLogGD(nameof (ShowJoin), "trace");
    if (!((UnityEngine.Object) this.RoomT != (UnityEngine.Object) null) || !((UnityEngine.Object) this.RoomT.gameObject != (UnityEngine.Object) null) || !PhotonNetwork.IsConnected)
      return;
    this.RoomT.gameObject.SetActive(false);
    this.CreateRoomT.gameObject.SetActive(false);
    this.JoinRoomT.gameObject.SetActive(true);
  }

  public void JoinRoomById()
  {
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.GetRoomId);
    AlertManager.Instance.AlertInput(Texts.Instance.GetText("inputIDRoom"), Texts.Instance.GetText("accept").ToUpper());
  }

  public void GetRoomId()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.GetRoomId);
    if (AlertManager.Instance.GetInputValue() == null)
      return;
    string upper = AlertManager.Instance.GetInputValue().ToUpper();
    if (!(upper.Trim() != ""))
      return;
    NetworkManager.Instance.JoinRoom(upper);
  }

  public void JoinRoom(string _roomName, string _roomPassword)
  {
    Functions.DebugLogGD("joinRoom " + _roomName + " // " + _roomPassword, "trace");
    this.roomPassword = _roomPassword;
    this.roomName = _roomName;
    if (_roomPassword != "")
    {
      AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.GetRoomPassword);
      AlertManager.Instance.AlertInput(Texts.Instance.GetText("inputPasswordRoom"), Texts.Instance.GetText("accept").ToUpper());
    }
    else
    {
      NetworkManager.Instance.JoinRoom(this.roomName);
      this.roomPassword = "";
      this.roomName = "";
    }
  }

  public void GetRoomPassword()
  {
    string upper = AlertManager.Instance.GetInputValue().ToUpper();
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.GetRoomPassword);
    if (!(new SimplerAES().Encrypt(upper) == this.roomPassword))
      return;
    NetworkManager.Instance.JoinRoom(this.roomName);
    this.roomPassword = "";
    this.roomName = "";
  }

  public void ShowRoom()
  {
    Debug.Log((object) nameof (ShowRoom));
    this.CreateRoomT.gameObject.SetActive(false);
    this.JoinRoomT.gameObject.SetActive(false);
    for (int index = 0; index < this.roomSlots.Length; ++index)
    {
      this.roomSlots[index].gameObject.SetActive(false);
      this.roomSlotsImage[index].gameObject.SetActive(false);
    }
    this.RoomT.gameObject.SetActive(true);
    this.buttonLaunch.gameObject.SetActive(false);
    this.buttonSteam.gameObject.SetActive(false);
    StringBuilder stringBuilder = new StringBuilder();
    string roomPassword = NetworkManager.Instance.GetRoomPassword();
    stringBuilder.Append(NetworkManager.Instance.GetRoomDescription());
    stringBuilder.Append("<br><size=30>");
    stringBuilder.Append("<color=#BBB>");
    stringBuilder.Append(Texts.Instance.GetText("roomId"));
    stringBuilder.Append(":</color> ");
    stringBuilder.Append(NetworkManager.Instance.GetRoomName());
    if (roomPassword != "")
    {
      stringBuilder.Append("<br>");
      stringBuilder.Append("<color=#BBB>");
      stringBuilder.Append(Texts.Instance.GetText("password"));
      stringBuilder.Append(":</color> ");
      stringBuilder.Append(roomPassword);
    }
    stringBuilder.Append("</size>");
    this.roomTitle.text = stringBuilder.ToString();
  }

  public void InviteSteam()
  {
    if (TomeManager.Instance.IsActive())
      return;
    SteamManager.Instance.InviteSteam();
  }

  public void GoToDiscord() => GameManager.Instance.Discord();

  public void ExitRoom()
  {
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.ExitRoomAction);
    AlertManager.Instance.AlertConfirmDouble(Texts.Instance.GetText("exitRoomConfirm"), Texts.Instance.GetText("accept").ToUpper(), Texts.Instance.GetText("cancel").ToUpper());
  }

  public void ExitRoomAction()
  {
    int num = AlertManager.Instance.GetConfirmAnswer() ? 1 : 0;
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.ExitRoomAction);
    if (num == 0)
      return;
    NetworkManager.Instance.ExitRoom();
    AtOManager.Instance.CleanSaveSlot();
    this.ShowJoin();
  }

  public void SetLobbyPlayersData(Player[] _PlayerList)
  {
    NetworkManager.Instance.PlayerList = _PlayerList;
    this.DrawLobbyNames();
  }

  public void KickPlayer(int position) => NetworkManager.Instance.KickPlayer(position);

  private void DrawLobbyNames()
  {
    int index1 = 0;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (Player player in NetworkManager.Instance.PlayerList)
    {
      stringBuilder.Clear();
      stringBuilder.Append("<color=#FFF>");
      if (player.NickName == NetworkManager.Instance.GetPlayerNick())
      {
        stringBuilder.Append("<u>");
        stringBuilder.Append(NetworkManager.Instance.GetPlayerNickReal(player.NickName));
        stringBuilder.Append("</u>");
      }
      else
        stringBuilder.Append(NetworkManager.Instance.GetPlayerNickReal(player.NickName));
      stringBuilder.Append("</color>");
      stringBuilder.Append("<br><size=24>");
      if (NetworkManager.Instance.GetPlayerVersion(player.NickName) != GameManager.Instance.gameVersion)
        stringBuilder.Append("<color=#D73232>");
      else
        stringBuilder.Append("<color=#A0A0A0>");
      stringBuilder.Append(NetworkManager.Instance.GetPlayerVersion(player.NickName));
      stringBuilder.Append("</size></color>");
      if (player.IsMasterClient)
      {
        stringBuilder.Append("  <color=#DE70BF><size=26>");
        stringBuilder.Append(Texts.Instance.GetText("master"));
        stringBuilder.Append("</size></color>");
      }
      if ((UnityEngine.Object) this.roomSlots[index1] != (UnityEngine.Object) null)
      {
        this.roomSlots[index1].gameObject.SetActive(true);
        this.roomSlots[index1].text = stringBuilder.ToString();
      }
      if ((UnityEngine.Object) this.roomSlotsImage[index1] != (UnityEngine.Object) null)
      {
        this.roomSlotsImage[index1].gameObject.SetActive(true);
        this.roomSlotsImage[index1].color = Functions.HexToColor(NetworkManager.Instance.GetColorFromNick(player.NickName));
      }
      if (index1 > 0)
      {
        if (PhotonNetwork.IsMasterClient)
          this.roomSlotsKick[index1].gameObject.SetActive(true);
        else
          this.roomSlotsKick[index1].gameObject.SetActive(false);
      }
      ++index1;
    }
    int num = index1;
    bool flag = true;
    if (index1 < (int) PhotonNetwork.CurrentRoom.MaxPlayers)
    {
      flag = false;
      for (int index2 = index1; index2 < (int) PhotonNetwork.CurrentRoom.MaxPlayers; ++index2)
      {
        stringBuilder.Clear();
        stringBuilder.Append("<color=#999>");
        stringBuilder.Append(Texts.Instance.GetText("openSlot"));
        stringBuilder.Append("</color>");
        this.roomSlots[index2].gameObject.SetActive(true);
        this.roomSlots[index2].text = stringBuilder.ToString();
        this.roomSlotsImage[index2].gameObject.SetActive(false);
        this.roomSlotsKick[index2].gameObject.SetActive(false);
        ++index1;
      }
    }
    for (int index3 = index1; index3 < this.roomSlots.Length; ++index3)
    {
      this.roomSlots[index3].gameObject.SetActive(false);
      this.roomSlotsImage[index3].gameObject.SetActive(false);
    }
    this.roomWaiting.text = flag ? Texts.Instance.GetText("roomFull") : Texts.Instance.GetText("waitingPlayers");
    if (!PhotonNetwork.IsMasterClient)
      return;
    if (num > 1)
      this.buttonLaunch.gameObject.SetActive(true);
    else
      this.buttonLaunch.gameObject.SetActive(false);
    if (SteamManager.Instance.steamConnected)
      this.buttonSteam.gameObject.SetActive(true);
    else
      this.buttonSteam.gameObject.SetActive(false);
  }

  public void RoomReceived(RoomInfo room)
  {
    int index = this.RoomListButtons.FindIndex((Predicate<RoomList>) (x => x.RoomName == room.Name));
    if (index == -1 && room.IsVisible && room.PlayerCount < (int) room.MaxPlayers)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RoomPrefab);
      gameObject.transform.SetParent(this.GridTransform, false);
      this.RoomListButtons.Add(gameObject.GetComponent<RoomList>());
      index = this.RoomListButtons.Count - 1;
    }
    if (index == -1)
      return;
    RoomList roomListButton = this.RoomListButtons[index];
    roomListButton.SetRoomName(room.Name);
    roomListButton.SetRoomPlayers(room.PlayerCount, (int) room.MaxPlayers);
    string str = "";
    if (SaveManager.PrefsHasKey("coopRoomId"))
      str = SaveManager.LoadPrefsString("coopRoomId");
    if (room.IsOpen && room.PlayerCount > 0 && room.PlayerCount < (int) room.MaxPlayers)
    {
      roomListButton.SetRoomCreator(room.CustomProperties[(object) "creator"].ToString());
      roomListButton.SetRoomPassword(room.CustomProperties[(object) "pwd"].ToString());
      roomListButton.SetRoomDescription(room.CustomProperties[(object) "description"].ToString());
      if (room.CustomProperties.ContainsKey((object) "lfm"))
        roomListButton.SetLfm(room.CustomProperties[(object) "lfm"].ToString());
      else
        roomListButton.SetLfm("");
      if (room.CustomProperties.ContainsKey((object) "gamedescription"))
        roomListButton.SetGameDescription(room.CustomProperties[(object) "gamedescription"].ToString());
      else
        roomListButton.SetGameDescription("");
      if (room.CustomProperties.ContainsKey((object) "version"))
        roomListButton.SetRoomVersion(room.CustomProperties[(object) "version"].ToString());
      if (!(str != "") || !(str == room.Name))
        return;
      this.ShowReconnectAlert();
    }
    else
    {
      if (!((UnityEngine.Object) roomListButton != (UnityEngine.Object) null))
        return;
      GameObject gameObject = roomListButton.gameObject;
      this.RoomListButtons.Remove(roomListButton);
      UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
    }
  }

  public void RemoveAllRooms()
  {
    if ((UnityEngine.Object) this.GridTransform != (UnityEngine.Object) null)
    {
      foreach (Component component in this.GridTransform)
        UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
    }
    this.RoomListButtons.Clear();
  }

  [PunRPC]
  public void RemoveOldRooms()
  {
  }

  public void GoBack()
  {
    if (GameManager.Instance.GameStatus == Enums.GameStatus.LoadGame)
      AtOManager.Instance.CleanSaveSlot();
    this.ShowJoin();
  }

  public void CreateMultiplayerGame()
  {
    GameManager.Instance.mainMenuGoToMultiplayer = true;
    SceneStatic.LoadByName("MainMenu");
  }

  public void CreateRoom()
  {
    string sentence = this.UICreateName.text.ToString().Trim();
    string roomPlayers = this.UICreatePlayers.options[this.UICreatePlayers.value].text.ToString();
    string unencrypted = this.UICreatePwd.text.ToString().ToUpper().Trim();
    string roomPassword = !this.UITogglePwd.isOn || !(unencrypted != "") ? "" : new SimplerAES().Encrypt(unencrypted);
    string lfm = !this.UIToggleLfm.isOn ? "" : "1";
    if (!(sentence != ""))
      return;
    string roomName = new ProfanityFilter.ProfanityFilter().CensorString(sentence);
    SaveManager.SaveIntoPrefsString("LobbyRoomName", roomName);
    NetworkManager.Instance.CreateRoom(roomName, roomPlayers, roomPassword, lfm);
  }

  public void SetStatus(bool reset = false, string text = "")
  {
    if (reset)
    {
      this.statusTM.text = text;
    }
    else
    {
      TMP_Text statusTm = this.statusTM;
      statusTm.text = statusTm.text + text + "\n";
    }
  }

  public void LaunchGame() => this.LaunchGameCheckVersion();

  private void LaunchGameCheckVersion()
  {
    if (NetworkManager.Instance.VersionMatch())
    {
      NetworkManager.Instance.CloseRoomDoors();
      NetworkManager.Instance.NormalizePlayerPositionList();
      NetworkManager.Instance.LoadScene("HeroSelection");
    }
    else
      AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("versionMismatch"));
  }

  public void ShowPlayerStatus()
  {
    this.SetStatus(text: "Player status");
    List<string> stringList = new List<string>((IEnumerable<string>) NetworkManager.Instance.PlayerStatusReady["lobbymanager"].Keys);
    for (int index = 0; index < stringList.Count; ++index)
      this.SetStatus(text: stringList[index] + ": " + NetworkManager.Instance.PlayerStatusReady["lobbymanager"][stringList[index]].ToString());
  }

  public void SetReady() => NetworkManager.Instance.SetStatusReady("lobbymanager");

  public void AllUnready()
  {
    if (GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
      NetworkManager.Instance.ClearAllPlayerReadyStatus("lobbymanager");
    this.ShowPlayerStatus();
  }

  public void ControllerMovement(bool goingUp = false, bool goingRight = false, bool goingDown = false, bool goingLeft = false)
  {
    this._controllerList.Clear();
    if (Functions.TransformIsVisible(this.regionsDisconnect))
      this._controllerList.Add(this.regionsDisconnect);
    if (Functions.TransformIsVisible(this.regions))
    {
      this._controllerList.Add(this.regions.GetChild(0));
      this._controllerList.Add(this.regions.GetChild(1));
    }
    for (int index = 0; index < this.buttonsController.Length; ++index)
    {
      if (Functions.TransformIsVisible(this.buttonsController[index]))
        this._controllerList.Add(this.buttonsController[index]);
    }
    if (Functions.TransformIsVisible(this.GridTransform))
    {
      foreach (Transform transform in this.GridTransform)
        this._controllerList.Add(transform.GetChild(5));
    }
    if (this._controllerList == null || this._controllerList.Count == 0)
      return;
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }
}
