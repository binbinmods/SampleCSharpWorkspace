// Decompiled with JetBrains decompiler
// Type: SteamManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Steamworks;
using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SteamManager : MonoBehaviour
{
  public bool steamLoaded;
  public bool steamConnected = true;
  public string steamName = "";
  public SteamId steamId = (SteamId) 0UL;
  private uint releaseAppId = 1385380;
  public Lobby lobby;
  private List<string> achievementsUnlocked = new List<string>();
  public LeaderboardEntry[] scoreboardGlobal;
  public LeaderboardEntry[] scoreboardFriends;
  public LeaderboardEntry[] scoreboardSingle;
  public Dictionary<string, string> dlcInfo;
  public bool gettingScoreboards;
  public List<string> shameList;
  public List<string> weeklyScoreboards;

  public static SteamManager Instance { get; private set; }

  private void Awake()
  {
    if ((UnityEngine.Object) SteamManager.Instance == (UnityEngine.Object) null)
      SteamManager.Instance = this;
    else if ((UnityEngine.Object) SteamManager.Instance != (UnityEngine.Object) this)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    this.shameList = new List<string>();
    this.shameList.Add("76561197967061331");
    this.shameList.Add("76561198090333522");
    this.shameList.Add("76561198010763033");
    this.shameList.Add("76561198973152554");
    this.shameList.Add("76561198071221108");
    this.shameList.Add("76561198161693534");
    this.shameList.Add("76561198014409560");
    this.shameList.Add("76561198360113370");
    this.shameList.Add("76561198104667997");
    this.shameList.Add("76561198166576962");
    this.shameList.Add("76561198110796654");
    this.shameList.Add("76561198099219329");
    this.shameList.Add("76561198005796435");
    this.shameList.Add("76561198308284145");
    this.shameList.Add("76561198364083963");
    this.shameList.Add("76561198051322233");
    this.shameList.Add("76561199010874940");
    this.shameList.Add("76561198151550085");
    this.shameList.Add("76561198091595297");
    this.shameList.Add("76561198045144129");
    this.shameList.Add("76561198103653826");
    this.shameList.Add("76561198968892849");
    this.shameList.Add("76561198127761770");
    this.shameList.Add("76561198351803536");
    this.shameList.Add("76561198069814663");
    this.shameList.Add("76561198215385943");
    this.shameList.Add("76561198394562597");
    this.shameList.Add("76561198002690061");
    this.shameList.Add("76561198298394060");
    this.shameList.Add("76561198035495634");
    this.shameList.Add("76561198015014615");
    this.shameList.Add("76561198258642840");
    this.shameList.Add("76561198039423148");
    this.shameList.Add("76561198046686325");
    this.shameList.Add("76561198100345602");
    this.shameList.Add("76561199098349852");
    this.shameList.Add("76561198050815767");
    this.shameList.Add("76561198065259678");
    this.shameList.Add("76561198314042772");
    this.shameList.Add("76561198158188514");
    this.shameList.Add("76561198006577550");
    this.shameList.Add("76561198059708390");
    this.shameList.Add("76561198015294057");
    this.shameList.Add("76561198004205610");
    this.shameList.Add("76561198054470753");
    this.shameList.Add("76561198846477761");
    this.shameList.Add("76561198346627042");
    this.shameList.Add("76561198881899421");
    this.shameList.Add("76561198986226022");
    this.shameList.Add("76561198363545598");
    this.shameList.Add("76561199256732247");
    this.shameList.Add("76561198179268647");
    this.shameList.Add("76561198080679919");
    this.shameList.Add("76561198060522910");
    this.shameList.Add("76561198046652127");
    this.shameList.Add("76561198034940625");
    this.shameList.Add("76561198009205578");
    this.shameList.Add("76561197980643859");
    this.shameList.Add("76561198151550085");
    this.shameList.Add("76561198846477761");
    this.shameList.Add("76561198290102386");
    this.shameList.Add("76561198170429125");
    this.shameList.Add("76561198247692284");
    this.shameList.Add("76561198101912322");
    this.shameList.Add("76561198307388683");
    this.shameList.Add("76561198797186891");
    this.shameList.Add("76561198070643116");
    this.shameList.Add("76561198080367358");
    this.shameList.Add("76561198071602793");
    this.shameList.Add("76561199199873267");
    this.shameList.Add("76561198841015318");
    this.shameList.Add("76561198288441233");
    this.shameList.Add("76561198350975460");
    this.shameList.Add("76561198065708345");
    this.shameList.Add("76561198254155353");
    this.shameList.Add("76561199205050622");
    this.shameList.Add("76561198374766185");
    this.shameList.Add("76561198016895913");
    this.shameList.Add("76561198171587526");
    this.shameList.Add("76561198140422317");
    this.shameList.Add("76561198046571676");
    this.shameList.Add("76561197974637386");
    this.shameList.Add("76561198004538781");
    this.shameList.Add("76561198198851386");
    this.shameList.Add("76561198005638497");
    this.shameList.Add("76561198023548551");
    this.shameList.Add("76561198177945821");
    this.shameList.Add("76561199168522784");
    this.shameList.Add("76561198164878710");
    this.shameList.Add("76561198006755583");
    this.shameList.Add("76561199217708664");
    this.shameList.Add("76561198799221793");
    this.shameList.Add("76561198011821425");
    this.shameList.Add("76561198397021079");
    this.shameList.Add("76561198000980157");
    this.shameList.Add("76561198154927630");
    this.shameList.Add("76561198214203984");
    this.shameList.Add("76561198274612857");
    this.shameList.Add("76561198313485330");
    this.shameList.Add("76561199025382063");
    this.shameList.Add("76561198340738336");
    this.shameList.Add("76561198293218125");
    this.shameList.Add("76561198314825649");
    this.shameList.Add("76561197999985728");
    this.shameList.Add("76561198031450406");
    this.shameList.Add("76561198079013335");
    this.shameList.Add("76561198131655603");
    this.shameList.Add("76561198050338192");
    this.shameList.Add("76561198402790968");
    this.shameList.Add("76561198091899280");
    this.shameList.Add("76561198225341324");
    this.shameList.Add("76561198140064824");
    this.shameList.Add("76561198296474483");
    this.shameList.Add("76561198363974009");
    this.shameList.Add("76561198402790968");
    this.shameList.Add("76561197995670501");
    this.shameList.Add("76561198040759667");
    this.shameList.Add("76561198294003247");
    this.shameList.Add("76561198256108408");
    this.shameList.Add("76561198119337267");
    this.shameList.Add("76561198124951939");
    this.shameList.Add("76561198384354335");
    this.shameList.Add("76561198282949213");
    this.shameList.Add("76561199348762256");
    this.shameList.Add("76561198850068192");
    this.shameList.Add("76561198348456538");
    this.shameList.Add("76561198423590954");
    this.shameList.Add("76561198863792752");
    this.shameList.Add("76561198070802868");
    this.shameList.Add("76561198829816933");
    this.shameList.Add("76561199362839599");
    this.shameList.Add("76561197986029843");
    this.shameList.Add("76561199362839599");
    this.shameList.Add("76561198824056936");
    this.shameList.Add("76561198254975029");
    this.shameList.Add("76561198065096276");
    this.shameList.Add("76561197997194195");
    this.shameList.Add("76561199074073309");
    this.shameList.Add("76561198031312496");
    this.shameList.Add("76561198102228964");
    this.shameList.Add("76561198324161664");
    this.shameList.Add("76561198288397625");
    this.shameList.Add("76561198124937642");
    this.shameList.Add("76561198048763052");
    this.shameList.Add("76561199234795844");
    this.shameList.Add("76561198119823284");
    this.shameList.Add("76561198424109209");
    this.shameList.Add("76561198086161708");
    this.shameList.Add("76561198070161930");
    this.shameList.Add("76561198197183203");
    this.shameList.Add("76561198124334507");
    this.shameList.Add("76561198985445897");
    this.shameList.Add("76561198103477347");
    this.shameList.Add("76561197992613000");
    this.shameList.Add("76561198048621985");
    this.shameList.Add("76561198424017417");
    this.shameList.Add("76561198264284174");
    this.shameList.Add("76561198126097202");
    this.shameList.Add("76561198263680470");
  }

  private void OnDisable() => SteamClient.Shutdown();

  public void DoSteam()
  {
    uint releaseAppId = this.releaseAppId;
    try
    {
      if (SteamClient.RestartAppIfNecessary((uint) (AppId) releaseAppId))
      {
        Application.Quit();
        return;
      }
    }
    catch (DllNotFoundException ex)
    {
      Debug.LogError((object) ("[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n" + ex?.ToString()), (UnityEngine.Object) this);
      this.steamConnected = false;
      Application.Quit();
      return;
    }
    try
    {
      SteamClient.Init(releaseAppId);
    }
    catch (Exception ex)
    {
      Debug.Log((object) "[STEAM] Error connecting to Steam");
      Debug.Log((object) ex);
      this.steamConnected = false;
    }
    if (!this.steamConnected)
      return;
    if (SteamApps.IsSubscribedToApp((AppId) this.releaseAppId))
      GameManager.Instance.SetDemo(false);
    this.steamName = SteamClient.Name;
    this.steamId = SteamClient.SteamId;
    if (this.steamId.ToString() == "76561198229850604" || this.steamId.ToString() == "76561198018931074" || this.steamId.ToString() == "76561198019918417" || this.steamId.ToString() == "76561198856292125" || this.steamId.ToString() == "76561199225796884" || this.steamId.ToString() == "76561198965756754" || this.steamId.ToString() == "76561198036268174" || this.steamId.ToString() == "76561198180023935" || this.steamId.ToString() == "76561198019330206" || this.steamId.ToString() == "76561198049739831" || this.steamId.ToString() == "76561197995379359")
      GameManager.Instance.SetDeveloperMode(true);
    this.GetDLCInformation();
    SteamFriends.OnGameRichPresenceJoinRequested += new Action<Friend, string>(this.OnGameRichPresenceJoinRequested);
    SteamMatchmaking.OnLobbyCreated += new Action<Result, Lobby>(this.OnLobbyCreated);
    SteamMatchmaking.OnLobbyMemberJoined += new Action<Lobby, Friend>(this.OnLobbyMemberJoined);
    SteamFriends.OnGameLobbyJoinRequested += new Action<Lobby, SteamId>(this.OnGameLobbyJoinRequested);
    SteamMatchmaking.OnLobbyEntered += new Action<Lobby>(this.OnLobbyEntered);
    SteamMatchmaking.OnLobbyInvite += new Action<Friend, Lobby>(this.OnLobbyInvite);
    SteamFriends.OnChatMessage += new Action<Friend, string, string>(this.OnChatMessage);
    SteamApps.OnNewLaunchParameters += new Action(this.OnNewLaunchParameters);
    SteamApps.GetLaunchParam("+connect_lobby");
    int num = -1;
    string s = "";
    string[] commandLineArgs = Environment.GetCommandLineArgs();
    for (int index = 0; index < commandLineArgs.Length; ++index)
    {
      if (index == num)
        s = commandLineArgs[index];
      else if (commandLineArgs[index] == "+connect_lobby")
        num = index + index;
    }
    if (s != "")
    {
      SteamId lobbyId = (SteamId) ulong.Parse(s);
      try
      {
        SteamMatchmaking.JoinLobbyAsync(lobbyId);
      }
      catch (Exception ex)
      {
        Debug.Log((object) "[STEAM] Error connecting to Steam");
        Debug.Log((object) ex);
        this.steamLoaded = false;
      }
    }
    else
      this.steamLoaded = true;
  }

  public int GetStatInt(string _name) => SteamUserStats.GetStatInt(_name);

  public void SetStatInt(string _name, int _value) => SteamUserStats.SetStat(_name, _value);

  public DateTime GetSteamTime() => SteamUtils.SteamServerTime;

  public bool PlayerHaveDLC(string _sku) => GameManager.Instance.GetDeveloperMode() || SteamApps.IsSubscribedToApp((AppId) uint.Parse(_sku));

  public void GetDLCInformation()
  {
    this.dlcInfo = new Dictionary<string, string>();
    foreach (DlcInformation dlcInformation in SteamApps.DlcInformation())
      this.dlcInfo.Add(dlcInformation.AppId.ToString(), dlcInformation.Name);
  }

  public string GetDLCName(string _sku) => this.dlcInfo != null && this.dlcInfo.ContainsKey(_sku) ? this.dlcInfo[_sku] : "";

  public void AchievementUnlock(string id)
  {
    if (!this.steamConnected || this.achievementsUnlocked.Contains(id))
      return;
    new Achievement(id).Trigger();
    this.achievementsUnlocked.Add(id);
  }

  private void OnInvitedToGame(Friend _friendId, string connect)
  {
    Debug.Log((object) nameof (OnInvitedToGame));
    Debug.Log((object) _friendId);
    Debug.Log((object) connect);
  }

  private void OnGameLobbyJoinRequested(Lobby _lobby, SteamId _friendId)
  {
    Debug.Log((object) nameof (OnGameLobbyJoinRequested));
    Debug.Log((object) _lobby.Id);
    Debug.Log((object) _friendId);
    SteamMatchmaking.JoinLobbyAsync(_lobby.Id);
  }

  private void OnNewLaunchParameters()
  {
    Debug.Log((object) nameof (OnNewLaunchParameters));
    Debug.Log((object) ("[Steam] launchParam -> " + SteamApps.GetLaunchParam("+connect_lobby")));
  }

  private void OnChatMessage(Friend _friendId, string _string0, string _string1)
  {
    Debug.Log((object) nameof (OnChatMessage));
    Debug.Log((object) _friendId);
    Debug.Log((object) _string0);
    Debug.Log((object) _string1);
  }

  private void OnGameRichPresenceJoinRequested(Friend _friendId, string _action)
  {
    Debug.Log((object) nameof (OnGameRichPresenceJoinRequested));
    Debug.Log((object) _friendId);
    Debug.Log((object) _action);
  }

  private void OnLobbyCreated(Result result, Lobby _lobby)
  {
    this.lobby = _lobby;
    this.lobby.SetPublic();
    this.lobby.SetJoinable(true);
    this.lobby.SetData("RoomName", NetworkManager.Instance.GetRoomName());
    Debug.Log((object) ("[Lobby] OnLobbyCreated " + this.lobby.Id.ToString()));
    SteamFriends.OpenGameInviteOverlay(this.lobby.Id);
  }

  private void OnLobbyMemberJoined(Lobby _lobby, Friend _friendId)
  {
  }

  private void OnLobbyEntered(Lobby _lobby)
  {
    Debug.Log((object) "[Lobby] OnLobbyEntered");
    if (_lobby.IsOwnedBy(this.steamId))
      return;
    string data = _lobby.GetData("RoomName");
    Debug.Log((object) ("Steam wants to join room -> " + data));
    NetworkManager.Instance.WantToJoinRoomName = data;
    this.steamLoaded = true;
  }

  private void OnLobbyInvite(Friend _friendId, Lobby _lobby)
  {
    Debug.Log((object) "[Lobby] OnLobbyInvite");
    Debug.Log((object) _friendId);
  }

  public async Task GetLeaderboards(int _index, int _subindex = 1)
  {
    this.gettingScoreboards = true;
    this.scoreboardGlobal = (LeaderboardEntry[]) null;
    this.scoreboardFriends = (LeaderboardEntry[]) null;
    this.scoreboardSingle = (LeaderboardEntry[]) null;
    Leaderboard? leaderboard = new Leaderboard?();
    switch (_index)
    {
      case 0:
        leaderboard = await SteamUserStats.FindLeaderboardAsync("RankingAct4");
        break;
      case 1:
        leaderboard = await SteamUserStats.FindLeaderboardAsync("RankingAct4Coop");
        break;
      case 2:
        leaderboard = await SteamUserStats.FindLeaderboardAsync("Challenge");
        break;
      case 3:
        leaderboard = await SteamUserStats.FindLeaderboardAsync("ChallengeCoop");
        break;
      case 4:
        leaderboard = await SteamUserStats.FindLeaderboardAsync("Weekly" + _subindex.ToString());
        break;
      case 5:
        leaderboard = await SteamUserStats.FindLeaderboardAsync("Weekly" + _subindex.ToString() + "Coop");
        break;
    }
    if (!leaderboard.HasValue)
    {
      Debug.Log((object) "Couldn't Get Leaderboard!");
    }
    else
    {
      this.scoreboardGlobal = await leaderboard.Value.GetScoresAsync(450);
      Leaderboard leaderboard1 = leaderboard.Value;
      this.scoreboardFriends = await leaderboard1.GetScoresFromFriendsAsync();
      leaderboard1 = leaderboard.Value;
      this.scoreboardSingle = await leaderboard1.GetScoresAroundUserAsync(0, 0);
      if (!TomeManager.Instance.IsActive())
        return;
      bool flag = false;
      if (this.shameList.Contains(this.steamId.ToString()))
        flag = true;
      int index = 0;
      foreach (LeaderboardEntry leaderboardEntry in this.scoreboardGlobal)
      {
        Convert.ToInt32(leaderboardEntry.Details[0] + leaderboardEntry.Score * 101);
        if (!flag && this.shameList.Contains(leaderboardEntry.User.Id.ToString()))
          this.scoreboardGlobal[index].User.Id = (SteamId) 0UL;
        ++index;
      }
    }
    this.gettingScoreboards = false;
  }

  public async Task Leaderboards(int delay = 100)
  {
    Leaderboard? leaderboardAsync = await SteamUserStats.FindLeaderboardAsync("Ranking");
    if (!leaderboardAsync.HasValue)
    {
      Debug.Log((object) "Couldn't Get Leaderboard!");
    }
    else
    {
      LeaderboardEntry[] scoresAsync = await leaderboardAsync.Value.GetScoresAsync(400);
    }
  }

  public async Task SetScoreLeaderboard(int score, bool singleplayer = true)
  {
    int gameId32 = Functions.StringToAsciiInt32(AtOManager.Instance.GetGameId());
    int details = Convert.ToInt32(gameId32 + score * 101);
    if (singleplayer)
    {
      Leaderboard? leaderboardAsync = await SteamUserStats.FindLeaderboardAsync("RankingAct4");
      if (leaderboardAsync.HasValue)
      {
        LeaderboardUpdate? nullable = await leaderboardAsync.Value.SubmitScoreAsync(score, new int[2]
        {
          gameId32,
          details
        });
      }
      else
        Debug.Log((object) "Couldn't Get Leaderboard!");
    }
    else
    {
      Leaderboard? leaderboardAsync = await SteamUserStats.FindLeaderboardAsync("RankingAct4Coop");
      if (leaderboardAsync.HasValue)
      {
        LeaderboardUpdate? nullable = await leaderboardAsync.Value.SubmitScoreAsync(score, new int[2]
        {
          gameId32,
          details
        });
      }
      else
        Debug.Log((object) "Couldn't Get Leaderboard!");
    }
  }

  public async void SetScore(int score, bool singleplayer = true)
  {
    if (score <= 0)
      return;
    await this.SetScoreLeaderboard(score, singleplayer);
  }

  public async void SetObeliskScore(int score, bool singleplayer = true)
  {
    if (score <= 0)
      return;
    await this.SetObeliskScoreLeaderboard(score, singleplayer);
  }

  public async Task SetObeliskScoreLeaderboard(int score, bool singleplayer = true)
  {
    int gameId32 = Functions.StringToAsciiInt32(AtOManager.Instance.GetGameId());
    int details = Convert.ToInt32(gameId32 + score * 101);
    if (singleplayer)
    {
      Leaderboard? leaderboardAsync = await SteamUserStats.FindLeaderboardAsync("Challenge");
      if (leaderboardAsync.HasValue)
      {
        LeaderboardUpdate? nullable = await leaderboardAsync.Value.SubmitScoreAsync(score, new int[2]
        {
          gameId32,
          details
        });
      }
      else
        Debug.Log((object) "Couldn't Get Leaderboard!");
    }
    else
    {
      Leaderboard? leaderboardAsync = await SteamUserStats.FindLeaderboardAsync("ChallengeCoop");
      if (leaderboardAsync.HasValue)
      {
        LeaderboardUpdate? nullable = await leaderboardAsync.Value.SubmitScoreAsync(score, new int[2]
        {
          gameId32,
          details
        });
      }
      else
        Debug.Log((object) "Couldn't Get Leaderboard!");
    }
  }

  public async void SetWeeklyScore(
    int score,
    int week,
    string nick,
    string nickgroup,
    bool singleplayer = true)
  {
    if (score <= 0)
      return;
    await this.SetWeeklyScoreLeaderboard(score, week, nick, nickgroup, singleplayer);
  }

  public async Task SetWeeklyScoreLeaderboard(
    int score,
    int week,
    string nick,
    string nickgroup,
    bool singleplayer = true)
  {
    int gameId32 = Functions.StringToAsciiInt32(AtOManager.Instance.GetGameId());
    int details = Convert.ToInt32(gameId32 + score * 101);
    Debug.Log((object) details);
    if (singleplayer)
    {
      Leaderboard? leaderboardAsync = await SteamUserStats.FindOrCreateLeaderboardAsync("Weekly" + week.ToString(), LeaderboardSort.Descending, LeaderboardDisplay.Numeric);
      if (leaderboardAsync.HasValue)
      {
        LeaderboardUpdate? nullable = await leaderboardAsync.Value.SubmitScoreAsync(score, new int[2]
        {
          gameId32,
          details
        });
      }
      else
        Debug.Log((object) "Couldn't Get Leaderboard!");
    }
    else
    {
      Leaderboard? leaderboardAsync = await SteamUserStats.FindOrCreateLeaderboardAsync("Weekly" + week.ToString() + "Coop", LeaderboardSort.Descending, LeaderboardDisplay.Numeric);
      if (leaderboardAsync.HasValue)
      {
        LeaderboardUpdate? nullable = await leaderboardAsync.Value.SubmitScoreAsync(score, new int[2]
        {
          gameId32,
          details
        });
      }
      else
        Debug.Log((object) "Couldn't Get Leaderboard!");
    }
  }

  public void SetRichPresence(string key, string value)
  {
    if (!this.steamConnected)
      return;
    SteamFriends.SetRichPresence(key, value);
  }

  public void InviteSteam() => SteamMatchmaking.CreateLobbyAsync(4);

  public Dictionary<string, string> GetFriends(bool onlyOnline = false)
  {
    Dictionary<string, string> friends = new Dictionary<string, string>();
    foreach (Friend friend in SteamFriends.GetFriends())
    {
      if (!onlyOnline || friend.IsOnline)
      {
        Dictionary<string, string> dictionary = friends;
        string name1 = friend.Name;
        SteamId id = friend.Id;
        string str1 = id.ToString();
        dictionary.Add(name1, str1);
        string name2 = friend.Name;
        id = friend.Id;
        string str2 = id.ToString();
        Debug.Log((object) (name2 + "->" + str2));
      }
    }
    return friends;
  }
}
