// Decompiled with JetBrains decompiler
// Type: GameManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public Texture2D cursorTexture;
  public Texture2D cursorTextureHover;
  public Transform globalBlack;
  public GameObject CardPrefab;
  public GameObject trailGoldPrefab;
  public GameObject trailCardPrefab;
  public GameObject trailDustPrefab;
  public GameObject popTutorialPrefab;
  public GameObject keyboardPrefab;
  public GameObject alertPrefab;
  private GameObject popTutorialGO;
  public Transform TempContainer;
  public Transform TutorialContainer;
  public AudioSource Audio;
  [SerializeField]
  private Dictionary<string, Hero> gameHeroes;
  public Sprite[] cardSprites;
  public Sprite[] cardBackSprites;
  public Sprite[] cardEnergySprites;
  public Sprite[] cardBorderSprites;
  private Dictionary<AudioClip, float> AudioPlayed = new Dictionary<AudioClip, float>();
  public float TimeSyncro;
  public Enums.GameMode GameMode;
  public Enums.GameStatus GameStatus;
  public Enums.GameType GameType;
  public CameraManager cameraManager;
  public Camera cameraMain;
  private Coroutine coroutineMask;
  public Transform MaskWindow;
  public Transform LoadingT;
  public Transform ChatWindow;
  public TMP_Text gameVersionT;
  public string gameVersion = "0.0.0";
  private float _timer;
  public Enums.ConfigSpeed configGameSpeed;
  private bool configAutoEnd;
  private bool configShowEffects = true;
  private bool configExtendedDescriptions = true;
  private bool configACBackgrounds = true;
  private bool configRestartCombatOption = true;
  private bool configScreenShakeOption = true;
  private bool configBackgroundMute = true;
  private bool configVsync;
  private bool configKeyboardShortcuts;
  private bool developerMode;
  private bool isDemo;
  private bool showedDemoMsg;
  private StringBuilder SBversion;
  public bool mainMenuGoToMultiplayer;
  public bool gameIsOnFocus = true;
  private Coroutine steamCo;
  private Coroutine gameVersionTextCo;
  public List<string> communityRewards = new List<string>();
  public string communityRewardsExpire = "";
  public DateTime storedDateTime;
  private Coroutine clockCoroutine;
  private double clockSeconds;
  public bool PrefsLoaded;
  private string profileFolder = "";
  private int profileId;
  public ConsoleToGUI consoleGUI;

  public static GameManager Instance { get; private set; }

  private void OnApplicationFocus(bool hasFocus)
  {
    this.gameIsOnFocus = hasFocus;
    AudioManager.Instance.StartStopBSO(this.gameIsOnFocus);
    AudioManager.Instance.StartStopAmbience(this.gameIsOnFocus);
  }

  private void Awake()
  {
    if ((UnityEngine.Object) GameManager.Instance == (UnityEngine.Object) null)
      GameManager.Instance = this;
    else if ((UnityEngine.Object) GameManager.Instance != (UnityEngine.Object) this)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    this.globalBlack.gameObject.SetActive(true);
    CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US", false);
  }

  private void Start()
  {
    Application.targetFrameRate = Globals.Instance.NormalFPS;
    this.gameVersion = ((TextAsset) UnityEngine.Resources.Load("runtime-version")).text;
    this.GameType = Enums.GameType.Adventure;
    this.configGameSpeed = Enums.ConfigSpeed.Slow;
    this.configAutoEnd = false;
    this.configShowEffects = true;
    this.configExtendedDescriptions = true;
    SettingsManager.Instance.LoadPrefs();
    this.PrefsLoaded = true;
    this.SetCamera();
    this.Resize();
    Texts.Instance.LoadTranslation();
    Globals.Instance.CreateGameContent();
    this.GenerateHeroes();
    this.steamCo = this.StartCoroutine(this.WaitForSteam());
  }

  private void SteamNotConnected()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.SteamNotConnected);
    Application.Quit();
  }

  private IEnumerator WaitForSteam()
  {
    GameManager gameManager = this;
    yield return (object) Globals.Instance.WaitForSeconds(0.2f);
    SteamManager.Instance.DoSteam();
    int exhaustSteam;
    for (exhaustSteam = 0; (!SteamManager.Instance.steamLoaded || (ulong) SteamManager.Instance.steamId == 0UL) && exhaustSteam < 30; ++exhaustSteam)
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    if (exhaustSteam >= 30)
    {
      AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(gameManager.SteamNotConnected);
      AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("steamErrorConnect"));
    }
    else
    {
      gameManager.InitProfileId();
      TomeManager.Instance.InitTome();
      PerkTree.Instance.InitPerkTree();
      MadnessManager.Instance.InitMadness();
      SandboxManager.Instance.InitSandbox();
      PlayerUIManager.Instance.Hide();
      OptionsManager.Instance.Hide();
      GiveManager.Instance.ShowGive(false);
      SettingsManager.Instance.ShowSettings(false);
      UnityEngine.Object.Instantiate<GameObject>(gameManager.keyboardPrefab, Vector3.zero, Quaternion.identity);
      UnityEngine.Object.Instantiate<GameObject>(gameManager.alertPrefab, Vector3.zero, Quaternion.identity);
      PlayerManager.Instance.PreBeginGame();
      gameManager.LoadPlayerData();
    }
  }

  private void LoadPlayerData()
  {
    PlayerData playerData = SaveManager.LoadPlayerData() ?? SaveManager.LoadPlayerData(true);
    if (playerData != null)
    {
      SaveManager.RestorePlayerData(playerData);
      if (PlayerManager.Instance.UnlockedCards.Count != 33 && PlayerManager.Instance.UnlockedNodes.Count != 0 && !AtOManager.Instance.IsFirstGame())
        SaveManager.SavePlayerDataBackup();
    }
    this.BeginGame();
  }

  private void BeginGame()
  {
    this.SBversion = new StringBuilder();
    this.SBversion.Append("AtO #");
    this.SBversion.Append(this.gameVersion);
    this.InvokeRepeating("SetGameVersionText", 0.0f, 2f);
    PlayerManager.Instance.BeginGame();
    SaveManager.SavePlayerData();
    this.StartTimer();
    if (NetworkManager.Instance.WantToJoinRoomName != "")
      this.ChangeScene("Lobby");
    else if (SceneStatic.CrossScene != null && SceneStatic.CrossScene != "")
    {
      this.ChangeScene(SceneStatic.CrossScene);
      SceneStatic.CrossScene = "";
    }
    else
    {
      if ((bool) (UnityEngine.Object) MainMenuManager.Instance)
        return;
      this.ChangeScene("MainMenu");
    }
  }

  private void InitProfileId()
  {
    if (!SaveManager.PrefsHasKey("profileId"))
    {
      this.UseProfileFile(0);
    }
    else
    {
      this.profileId = SaveManager.LoadPrefsInt("profileId");
      if (this.profileId < 0 || this.profileId > 4)
        this.profileId = 0;
    }
    this.SetProfileFolder();
  }

  public void UseProfileFile(int _profile)
  {
    SaveManager.SaveIntoPrefsInt("profileId", _profile);
    SaveManager.SavePrefs();
    this.profileId = _profile;
  }

  public void SetProfileFolder()
  {
    this.profileFolder = this.profileId != 0 ? "profile" + this.profileId.ToString() + "/" : "";
    if (SaveManager.ExistsProfileFolder(this.profileId))
      return;
    this.UseProfileFile(0);
    this.SetProfileFolder();
  }

  public void ReloadProfile()
  {
    this.SetProfileFolder();
    PlayerManager.Instance.InitPlayerData();
    PlayerManager.Instance.PreBeginGame();
    PlayerManager.Instance.BeginGame();
    this.LoadPlayerData();
  }

  public void SaveGameDeveloper()
  {
    if (this.IsMultiplayer() && !NetworkManager.Instance.IsMaster() || !(bool) (UnityEngine.Object) TownManager.Instance && !(bool) (UnityEngine.Object) MapManager.Instance)
      return;
    if (this.IsWeeklyChallenge())
    {
      if (!this.IsMultiplayer())
      {
        SaveManager.SaveGame(24);
        SaveManager.SaveGame(25);
        SaveManager.SaveGame(26);
      }
      else
      {
        SaveManager.SaveGame(30);
        SaveManager.SaveGame(31);
        SaveManager.SaveGame(32);
      }
    }
    else if (this.IsObeliskChallenge())
    {
      if (!this.IsMultiplayer())
      {
        SaveManager.SaveGame(12);
        SaveManager.SaveGame(13);
        SaveManager.SaveGame(14);
      }
      else
      {
        SaveManager.SaveGame(18);
        SaveManager.SaveGame(19);
        SaveManager.SaveGame(20);
      }
    }
    else if (!this.IsMultiplayer())
    {
      SaveManager.SaveGame(0);
      SaveManager.SaveGame(1);
      SaveManager.SaveGame(2);
    }
    else
    {
      SaveManager.SaveGame(6);
      SaveManager.SaveGame(7);
      SaveManager.SaveGame(8);
    }
  }

  public void SetVsync()
  {
    if (this.configVsync)
    {
      QualitySettings.vSyncCount = 1;
      Application.targetFrameRate = 60;
    }
    else
    {
      QualitySettings.vSyncCount = 0;
      Application.targetFrameRate = Globals.Instance.NormalFPS;
    }
  }

  private void AlphaExpired() => this.QuitGame(true);

  public void AbortGameSave()
  {
    Debug.Log((object) "Abort Game Save");
    AtOManager.Instance.CleanGameId();
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.AbortGameSaveAction);
    AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("abortSave"));
  }

  private void AbortGameSaveAction()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.AbortGameSaveAction);
    NetworkManager.Instance.Disconnect();
    this.StartCoroutine(this.CoMainMenu());
  }

  private IEnumerator CoMainMenu()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    SceneStatic.LoadByName("MainMenu");
  }

  public void Discord() => Application.OpenURL("https://discord.gg/VuQKR2yVxC");

  public void AbortGame() => this.StartCoroutine(this.AbortGameCo());

  private IEnumerator AbortGameCo()
  {
    GameManager gameManager = this;
    yield return (object) Globals.Instance.WaitForSeconds(0.5f);
    Debug.Log((object) "Abort Game");
    if (!AtOManager.Instance.IsAdventureCompleted() && !(SceneStatic.GetSceneName() == "FinishRun") && NetworkManager.Instance.IsConnected())
    {
      AtOManager.Instance.CleanGameId();
      AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(gameManager.AbortGameAction);
      AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("abortMultiplayer"));
    }
  }

  private void AbortGameAction()
  {
    Debug.Log((object) "Abort Game Action");
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.AbortGameAction);
    NetworkManager.Instance.Disconnect();
    this.StartCoroutine(this.CoMainMenu());
  }

  private void SteamConfirm()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.SteamConfirm);
    this.QuitGame(true);
  }

  public void SetDeveloperMode(bool state) => this.developerMode = state;

  public void DebugShow() => Globals.Instance.ShowDebug = !Globals.Instance.ShowDebug;

  public bool GetDeveloperMode() => this.developerMode;

  public void SetDemo(bool state) => this.isDemo = state;

  public bool IsDemo() => this.isDemo;

  public void SetCursorPlain() => Cursor.SetCursor(this.cursorTexture, Vector2.zero, CursorMode.Auto);

  public void SetCursorHover() => Cursor.SetCursor(this.cursorTextureHover, Vector2.zero, CursorMode.Auto);

  public void SetGameVersionText()
  {
    if (!((UnityEngine.Object) this.gameVersionT != (UnityEngine.Object) null) || this.SBversion == null)
      return;
    if (NetworkManager.Instance.IsConnected())
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.SBversion.ToString());
      string ping = NetworkManager.Instance.GetPing();
      if (ping != "")
      {
        stringBuilder.Append(" | Ping: ");
        stringBuilder.Append(ping);
        stringBuilder.Append("ms");
      }
      this.gameVersionT.text = stringBuilder.ToString();
    }
    else
      this.gameVersionT.text = this.SBversion.ToString();
  }

  public bool TutorialWatched(string popType) => PlayerManager.Instance.TutorialWatched != null && PlayerManager.Instance.TutorialWatched.Contains(popType);

  public bool IsTutorialGame()
  {
    if (AtOManager.Instance.GetGameId().ToLower() != "cban29t".ToLower())
      return false;
    Hero[] team = AtOManager.Instance.GetTeam();
    List<string> stringList = new List<string>();
    stringList.Add("magnus");
    stringList.Add("andrin");
    stringList.Add("evelyn");
    stringList.Add("reginald");
    if (team != null && team.Length == 4)
    {
      for (int index = 0; index < team.Length; ++index)
      {
        if (team[index] == null || (UnityEngine.Object) team[index].HeroData == (UnityEngine.Object) null || !stringList.Contains(team[index].SourceName.ToLower()))
          return false;
      }
    }
    return true;
  }

  public void ShowTutorialPopup(string popType, Vector3 position, Vector3 position2)
  {
    if (PlayerManager.Instance.TutorialWatched == null)
      PlayerManager.Instance.TutorialWatched = new List<string>();
    if (!PlayerManager.Instance.TutorialWatched.Contains(popType) || popType == "townItemCraft" || popType == "townItemUpgrade" || popType == "townItemLoot")
    {
      PlayerManager.Instance.TutorialWatched.Add(popType);
      SaveManager.SavePlayerData();
      if ((UnityEngine.Object) this.popTutorialGO != (UnityEngine.Object) null)
        this.HideTutorialPopup();
      this.popTutorialGO = UnityEngine.Object.Instantiate<GameObject>(this.popTutorialPrefab, new Vector3(0.0f, 0.0f, -7f), Quaternion.identity, this.TutorialContainer);
      this.popTutorialGO.GetComponent<PopTutorialManager>().Show(popType, position, position2);
    }
    else
      this.TutorialCombatContinue();
  }

  private void TutorialCombatContinue()
  {
    if (!((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null))
      return;
    MatchManager.Instance.waitingTutorial = false;
  }

  public void HideTutorialPopup()
  {
    this.TutorialCombatContinue();
    UnityEngine.Object.Destroy((UnityEngine.Object) this.popTutorialGO);
  }

  public bool IsTutorialActive() => (UnityEngine.Object) this.popTutorialGO != (UnityEngine.Object) null;

  private void ChangeLanguage()
  {
    Globals.Instance.CurrentLang = !(Globals.Instance.CurrentLang == "es") ? "es" : "en";
    foreach (Component component1 in UnityEngine.Resources.FindObjectsOfTypeAll<TMP_Text>())
    {
      Translate component2 = component1.GetComponent<Translate>();
      if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
        component2.SetText();
    }
    foreach (BotonGeneric botonGeneric in UnityEngine.Resources.FindObjectsOfTypeAll<BotonGeneric>())
      botonGeneric.ResetText();
  }

  public void EscapeFunction(bool activateExit = true)
  {
    if (!this.PrefsLoaded || this.IsMaskActive() || AtOManager.Instance.saveLoadStatus)
      return;
    string sceneName = SceneStatic.GetSceneName();
    if (sceneName == "FinishRun")
      return;
    if (KeyboardManager.Instance.IsActive())
      KeyboardManager.Instance.ShowKeyboard(false);
    else if ((UnityEngine.Object) this.popTutorialGO != (UnityEngine.Object) null && this.popTutorialGO.gameObject.activeSelf)
      this.HideTutorialPopup();
    else if (AlertManager.Instance.IsActive())
      AlertManager.Instance.CloseAlert();
    else if (SettingsManager.Instance.IsActive())
      SettingsManager.Instance.ShowSettings(false);
    else if (CardScreenManager.Instance.IsActive())
      CardScreenManager.Instance.ShowCardScreen(false);
    else if (DamageMeterManager.Instance.IsActive())
      DamageMeterManager.Instance.Hide();
    else if (TomeManager.Instance.IsActive())
    {
      if (Functions.TransformIsVisible(TomeManager.Instance.runDetails))
        TomeManager.Instance.RunDetailClose();
      else if (Functions.TransformIsVisible(TomeManager.Instance.glossarySection))
        TomeManager.Instance.SetPage(0);
      else
        TomeManager.Instance.ShowTome(false);
    }
    else if (MadnessManager.Instance.IsActive())
      MadnessManager.Instance.ShowMadness();
    else if (SandboxManager.Instance.IsActive())
      SandboxManager.Instance.CloseSandbox();
    else if (GiveManager.Instance.IsActive())
      GiveManager.Instance.ShowGive(false);
    else if (PerkTree.Instance.IsActive())
      PerkTree.Instance.Hide();
    else if (sceneName != "ChallengeSelection" && sceneName != "Map" && (UnityEngine.Object) CardCraftManager.Instance != (UnityEngine.Object) null && !CardCraftManager.Instance.IsWaitingDivination())
    {
      if (Functions.TransformIsVisible(CardCraftManager.Instance.filterWindow))
        CardCraftManager.Instance.ShowFilter(false);
      else if (Functions.TransformIsVisible(CardCraftManager.Instance.cardCraftSave.transform))
        CardCraftManager.Instance.ShowSaveLoad();
      else
        CardCraftManager.Instance.ExitCardCraft();
    }
    else if (sceneName == "Combat")
    {
      if (MatchManager.Instance.console.IsActive())
        MatchManager.Instance.ShowLog();
      else if (MatchManager.Instance.characterWindow.IsActive())
        MatchManager.Instance.characterWindow.Hide();
      else if (MatchManager.Instance.PreCastNum != -1)
      {
        MatchManager.Instance.ResetCastCardNum();
      }
      else
      {
        if (!MatchManager.Instance.CardDrag)
          return;
        MatchManager.Instance.ControllerStopDrag();
      }
    }
    else if (sceneName == "Town" && TownManager.Instance.characterWindow.IsActive())
      TownManager.Instance.characterWindow.Hide();
    else if (sceneName == "Town" && TownManager.Instance.townUpgradeWindow.IsActive())
      TownManager.Instance.ShowTownUpgrades(false);
    else if (sceneName == "Map" && MapManager.Instance.characterWindow.IsActive())
      MapManager.Instance.characterWindow.Hide();
    else if (sceneName == "HeroSelection")
    {
      if ((UnityEngine.Object) HeroSelectionManager.Instance.controllerCurrentHS != (UnityEngine.Object) null)
      {
        HeroSelectionManager.Instance.controllerCurrentHS.ResetClickedController();
      }
      else
      {
        if (!((UnityEngine.Object) HeroSelectionManager.Instance.charPopup != (UnityEngine.Object) null) || !HeroSelectionManager.Instance.charPopup.IsOpened())
          return;
        HeroSelectionManager.Instance.charPopup.Close();
      }
    }
    else if (sceneName == "MainMenu" && MainMenuManager.Instance.IsGameModesActive())
      MainMenuManager.Instance.ExitSaveGame();
    else if (sceneName == "MainMenu" && MainMenuManager.Instance.credits.gameObject.activeSelf)
      MainMenuManager.Instance.ExitSaveGame();
    else if (sceneName == "MainMenu" && MainMenuManager.Instance.profilesT.gameObject.activeSelf)
    {
      MainMenuManager.Instance.ExitSaveGame();
    }
    else
    {
      switch (sceneName)
      {
        case "MainMenu":
          this.QuitGame();
          break;
        case "IntroNewGame":
          IntroNewGameManager.Instance.SkipIntro();
          break;
        case "Rewards":
          if (RewardsManager.Instance.characterWindowUI.IsActive())
          {
            RewardsManager.Instance.characterWindowUI.Hide();
            break;
          }
          OptionsManager.Instance.CantExitBecauseRewards();
          break;
        case "Loot":
          if (LootManager.Instance.characterWindowUI.IsActive())
          {
            LootManager.Instance.characterWindowUI.Hide();
            break;
          }
          OptionsManager.Instance.CantExitBecauseRewards();
          break;
        case "Cinematic":
          CinematicManager.Instance.SkipCinematic();
          break;
        default:
          if (!activateExit)
            break;
          OptionsManager.Instance.Exit();
          break;
      }
    }
  }

  public void QuitGame(bool instant = false)
  {
    if (!instant)
    {
      AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.QuitGameAction);
      AlertManager.Instance.AlertConfirmDouble(Texts.Instance.GetText("wantToQuitGame"), Texts.Instance.GetText("accept").ToUpper(), Texts.Instance.GetText("cancel").ToUpper());
      AlertManager.Instance.ShowDoorIcon();
    }
    else
      Application.Quit();
  }

  private void QuitGameAction()
  {
    if (AlertManager.Instance.GetConfirmAnswer())
      Application.Quit();
    else
      AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.QuitGameAction);
  }

  public void Resize()
  {
    Globals.Instance.sizeW = Camera.main.orthographicSize * 2f * Camera.main.aspect;
    Globals.Instance.sizeH = Camera.main.orthographicSize * 2f;
    Globals.Instance.multiplierX = 0.9999999f;
    Globals.Instance.multiplierY = 1f;
    Globals.Instance.scale = (float) (1920.0 * (double) Screen.height / (1080.0 * (double) Screen.width));
    Globals.Instance.scaleV = new Vector3(Globals.Instance.scale, Globals.Instance.scale, 1f);
    if ((UnityEngine.Object) MainMenuManager.Instance != (UnityEngine.Object) null)
      MainMenuManager.Instance.Resize();
    else if ((UnityEngine.Object) HeroSelectionManager.Instance != (UnityEngine.Object) null)
      HeroSelectionManager.Instance.Resize();
    else if ((UnityEngine.Object) MapManager.Instance != (UnityEngine.Object) null)
      MapManager.Instance.Resize();
    else if ((UnityEngine.Object) ChallengeSelectionManager.Instance != (UnityEngine.Object) null)
      ChallengeSelectionManager.Instance.Resize();
    else if ((UnityEngine.Object) TownManager.Instance != (UnityEngine.Object) null)
      TownManager.Instance.Resize();
    else if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null)
      MatchManager.Instance.Resize();
    if ((UnityEngine.Object) OptionsManager.Instance != (UnityEngine.Object) null)
      OptionsManager.Instance.Resize();
    if ((UnityEngine.Object) PlayerUIManager.Instance != (UnityEngine.Object) null)
      PlayerUIManager.Instance.Resize();
    if ((UnityEngine.Object) CardCraftManager.Instance != (UnityEngine.Object) null)
      CardCraftManager.Instance.Resize();
    if ((UnityEngine.Object) SettingsManager.Instance != (UnityEngine.Object) null)
      SettingsManager.Instance.Resize();
    if ((UnityEngine.Object) CardScreenManager.Instance != (UnityEngine.Object) null)
      CardScreenManager.Instance.Resize();
    if (!((UnityEngine.Object) PopupManager.Instance != (UnityEngine.Object) null))
      return;
    PopupManager.Instance.Resize();
  }

  private void GenerateHeroes()
  {
    this.gameHeroes = new Dictionary<string, Hero>();
    this.gameHeroes.Add("mercenary", this.CreateHero("mercenary"));
    this.gameHeroes.Add("sentinel", this.CreateHero("sentinel"));
    this.gameHeroes.Add("berserker", this.CreateHero("berserker"));
    this.gameHeroes.Add("warden", this.CreateHero("warden"));
    this.gameHeroes.Add("cleric", this.CreateHero("cleric"));
    this.gameHeroes.Add("priest", this.CreateHero("priest"));
    this.gameHeroes.Add("voodoowitch", this.CreateHero("voodoowitch"));
    this.gameHeroes.Add("prophet", this.CreateHero("prophet"));
    this.gameHeroes.Add("elementalist", this.CreateHero("elementalist"));
    this.gameHeroes.Add("pyromancer", this.CreateHero("pyromancer"));
    this.gameHeroes.Add("warlock", this.CreateHero("warlock"));
    this.gameHeroes.Add("loremaster", this.CreateHero("loremaster"));
    this.gameHeroes.Add("ranger", this.CreateHero("ranger"));
    this.gameHeroes.Add("assassin", this.CreateHero("assassin"));
    this.gameHeroes.Add("archer", this.CreateHero("archer"));
    this.gameHeroes.Add("minstrel", this.CreateHero("minstrel"));
  }

  public bool IsObeliskChallenge()
  {
    bool flag = false;
    if (this.GameType == Enums.GameType.Challenge || this.GameType == Enums.GameType.WeeklyChallenge)
      flag = true;
    return flag;
  }

  public bool IsWeeklyChallenge() => this.GameType == Enums.GameType.WeeklyChallenge;

  public bool IsGameAdventure() => this.GameType == Enums.GameType.Adventure;

  public bool IsMultiplayer() => this.GameMode == Enums.GameMode.Multiplayer;

  public bool IsLoadingGame() => this.GameStatus == Enums.GameStatus.LoadGame;

  public void SetGameStatus(Enums.GameStatus status) => this.GameStatus = status;

  public void LoadCombat() => SceneManager.LoadScene("Combat");

  public void BackToGame() => SceneManager.LoadScene("Game");

  public void SetCamera()
  {
    Camera[] allCameras = Camera.allCameras;
    for (int index = 0; index < allCameras.Length; ++index)
    {
      if (allCameras[index].gameObject.tag != "MainCamera")
      {
        allCameras[index].gameObject.SetActive(false);
      }
      else
      {
        this.cameraManager = allCameras[index].GetComponent<CameraManager>();
        this.cameraMain = allCameras[index];
      }
    }
  }

  public void PlayLibraryAudio(string name, float timePassed = 0.0f)
  {
    if (!this.gameIsOnFocus && this.configBackgroundMute)
      return;
    if ((double) timePassed == 0.0)
      timePassed = 0.1f;
    if (!((UnityEngine.Object) AudioManager.Instance.audioLibrary[name] != (UnityEngine.Object) null))
      return;
    this.PlayAudio(AudioManager.Instance.audioLibrary[name], timePassed);
  }

  public void PlayAudio(AudioClip sound, float timePassed = 0.0f, float volume = 1f)
  {
    if (!this.gameIsOnFocus && this.configBackgroundMute || (UnityEngine.Object) sound == (UnityEngine.Object) null)
      return;
    bool flag = true;
    if ((double) timePassed > 0.0 && this.AudioPlayed.TryGetValue(sound, out float _) && (double) Time.time < (double) this.AudioPlayed[sound] + (double) timePassed)
      flag = false;
    if (!flag)
      return;
    this.Audio.PlayOneShot(sound, volume);
    this.AudioPlayed[sound] = Time.time;
  }

  public void SceneLoaded()
  {
    if ((UnityEngine.Object) OptionsManager.Instance == (UnityEngine.Object) null)
      return;
    if ((UnityEngine.Object) PopupManager.Instance != (UnityEngine.Object) null)
      PopupManager.Instance.ClosePopup();
    this.CleanTempContainer();
    bool flag1 = true;
    bool flag2 = true;
    bool state = false;
    switch (SceneStatic.GetSceneName())
    {
      case "ChallengeSelection":
        flag2 = false;
        break;
      case "Cinematic":
        flag1 = false;
        flag2 = false;
        break;
      case "Combat":
        flag2 = false;
        break;
      case "FinishRun":
        flag2 = false;
        break;
      case "HeroSelection":
        flag2 = false;
        break;
      case "IntroNewGame":
        flag1 = false;
        flag2 = false;
        break;
      case "Lobby":
        flag2 = false;
        break;
      case "MainMenu":
        flag1 = false;
        flag2 = false;
        break;
      case "Map":
        state = true;
        break;
      case "TeamManagement":
        flag2 = false;
        break;
      case "TomeOfKnowledge":
        flag2 = false;
        flag1 = false;
        break;
      case "Town":
        state = true;
        break;
      case "TrailerEnd":
        flag2 = false;
        break;
      case "TrailerPoster":
        flag2 = false;
        break;
    }
    if (flag2)
      PlayerUIManager.Instance.Show();
    else
      PlayerUIManager.Instance.Hide();
    if (flag1)
    {
      OptionsManager.Instance.ShowScore(state);
      OptionsManager.Instance.SetMadness();
      OptionsManager.Instance.Show();
    }
    else
      OptionsManager.Instance.Hide();
    this.gameVersionT.transform.parent.gameObject.SetActive(true);
    this.ShowMask(false);
  }

  public void SetMaskLoading()
  {
    if (!((UnityEngine.Object) this.MaskWindow.gameObject != (UnityEngine.Object) null) || this.MaskWindow.gameObject.activeSelf)
      return;
    this.MaskWindow.gameObject.SetActive(true);
    this.MaskWindow.GetChild(0).transform.GetComponent<RawImage>().color = new Color(0.0f, 0.0f, 0.0f, 1f);
    this.LoadingT.gameObject.SetActive(true);
  }

  public void ChangeScene(string scene, bool showMask = true)
  {
    this.SetMaskLoading();
    this.HideTutorialPopup();
    if (TomeManager.Instance.IsActive())
      TomeManager.Instance.ShowTome(false);
    if (MadnessManager.Instance.IsActive())
      MadnessManager.Instance.ShowMadness();
    if (SandboxManager.Instance.IsActive())
      SandboxManager.Instance.CloseSandbox();
    if (PerkTree.Instance.IsActive())
      PerkTree.Instance.HideAction(false);
    UnityEngine.Resources.UnloadUnusedAssets();
    GC.Collect();
    Globals.Instance.CleanInstantiatedCardData();
    NetworkManager.Instance.StartStopQueue(false);
    SceneManager.LoadScene(scene);
  }

  public void CleanTempContainer()
  {
    foreach (Component component in this.TempContainer)
      component.gameObject.SetActive(false);
    foreach (Component component in this.TempContainer)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
  }

  public void ShowMask(bool state, string sceneToLoad = "")
  {
    if (this.coroutineMask != null)
      this.StopCoroutine(this.coroutineMask);
    this.coroutineMask = this.StartCoroutine(this.ShowMaskCo(state, sceneToLoad));
  }

  private IEnumerator ShowMaskCo(bool state, string sceneToLoad)
  {
    float maxAlplha = 1f;
    if (!((UnityEngine.Object) this.MaskWindow == (UnityEngine.Object) null) && !((UnityEngine.Object) this.MaskWindow.GetChild(0) == (UnityEngine.Object) null) && !((UnityEngine.Object) this.MaskWindow.GetChild(0).transform == (UnityEngine.Object) null))
    {
      RawImage imageBg = this.MaskWindow.GetChild(0).transform.GetComponent<RawImage>();
      float index = imageBg.color.a;
      if (!state)
      {
        this.LoadingT.gameObject.SetActive(false);
        while ((double) index > 0.0)
        {
          imageBg.color = new Color(0.0f, 0.0f, 0.0f, index);
          index -= 0.1f;
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        }
        imageBg.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        this.MaskWindow.gameObject.SetActive(false);
      }
      else
      {
        this.MaskWindow.gameObject.SetActive(true);
        while ((double) index < (double) maxAlplha)
        {
          imageBg.color = new Color(0.0f, 0.0f, 0.0f, index);
          index += 0.2f;
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        }
        imageBg.color = new Color(0.0f, 0.0f, 0.0f, maxAlplha);
        this.LoadingT.gameObject.SetActive(true);
      }
    }
  }

  public bool IsMaskActive() => this.MaskWindow.gameObject.activeSelf;

  public Hero CreateHero(string subClass)
  {
    SubClassData subClassData = Globals.Instance.GetSubClassData(subClass);
    HeroData heroData = UnityEngine.Object.Instantiate<HeroData>(Globals.Instance.GetHeroData(subClassData.HeroClass.ToString().ToLower()));
    Hero hero = new Hero();
    heroData.HeroSubClass = subClassData;
    hero.HeroData = heroData;
    hero.InitData();
    hero.GameName = subClassData.SubClassName;
    return hero;
  }

  public Hero AssignDataToHero(Hero hh)
  {
    if (hh == null || hh.SubclassName == "")
      return (Hero) null;
    SubClassData subClassData = Globals.Instance.GetSubClassData(hh.SubclassName);
    if ((UnityEngine.Object) subClassData == (UnityEngine.Object) null)
      return (Hero) null;
    HeroData heroData = UnityEngine.Object.Instantiate<HeroData>(Globals.Instance.GetHeroData(hh.ClassName));
    heroData.HeroSubClass = subClassData;
    hh.HeroData = heroData;
    hh.InitGeneralData();
    return hh;
  }

  public void GenerateParticleTrail(int type, Vector3 from, Vector3 to) => this.StartCoroutine(this.GenerateParticleTrailCo(type, from, to));

  private IEnumerator GenerateParticleTrailCo(int type, Vector3 from, Vector3 to)
  {
    GameObject trail = type != 0 ? (type != 2 ? UnityEngine.Object.Instantiate<GameObject>(this.trailDustPrefab, from, Quaternion.identity) : UnityEngine.Object.Instantiate<GameObject>(this.trailCardPrefab, from, Quaternion.identity)) : UnityEngine.Object.Instantiate<GameObject>(this.trailGoldPrefab, from, Quaternion.identity);
    if ((bool) (UnityEngine.Object) MatchManager.Instance)
    {
      ParticleSystemRenderer component = trail.GetComponent<ParticleSystemRenderer>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.sortingLayerName = "Book";
    }
    Vector3[] inputPoints = new Vector3[3];
    float x = (double) from.x <= (double) to.x ? from.x + Mathf.Abs(from.x - to.x) * UnityEngine.Random.Range(0.35f, 0.65f) : from.x - Mathf.Abs(to.x - from.x) * UnityEngine.Random.Range(0.35f, 0.65f);
    float y = UnityEngine.Random.Range(0, 2) != 1 ? from.y - Mathf.Abs((float) ((double) to.y - (double) from.y + 2.0)) * UnityEngine.Random.Range(0.35f, 0.65f) : from.y + Mathf.Abs((float) ((double) to.y - (double) from.y + 2.0)) * UnityEngine.Random.Range(0.35f, 0.65f);
    if ((double) y > (double) Globals.Instance.sizeH * 0.5 || (double) y < -(double) Globals.Instance.sizeH * 0.5)
      y *= -1f;
    if ((double) y > (double) Globals.Instance.sizeH * 0.5 || (double) y < -(double) Globals.Instance.sizeH * 0.5)
      y *= 0.5f;
    Vector3 vector3 = new Vector3(x, y, 0.0f);
    inputPoints[0] = from;
    inputPoints[1] = vector3;
    inputPoints[2] = to;
    Vector3[] gotPoints = LineSmoother.SmoothLine(inputPoints, 0.6f);
    float speed = 50f;
    this.PlayLibraryAudio("ui_swoosh");
    for (int i = 0; i < gotPoints.Length - 1 && !((UnityEngine.Object) trail == (UnityEngine.Object) null); ++i)
    {
      trail.transform.position = gotPoints[i];
      Vector3 destination = gotPoints[i + 1];
      while ((UnityEngine.Object) trail != (UnityEngine.Object) null && (double) Vector3.Distance(trail.transform.position, destination) > 0.20000000298023224)
      {
        trail.transform.position = Vector3.MoveTowards(trail.transform.position, destination, speed * Time.deltaTime);
        yield return (object) null;
      }
      destination = new Vector3();
    }
    yield return (object) Globals.Instance.WaitForSeconds(0.3f);
    UnityEngine.Object.Destroy((UnityEngine.Object) trail);
  }

  public void ShowedDemoMsg() => this.showedDemoMsg = true;

  public bool ShowDemoMsgState() => this.showedDemoMsg;

  private void StartTimer()
  {
    this.storedDateTime = SteamManager.Instance.GetSteamTime().AddHours(2.0);
    if (this.clockCoroutine != null)
      return;
    this.clockCoroutine = this.StartCoroutine(this.clockCounter());
  }

  public DateTime GetTime()
  {
    DateTime storedDateTime = this.storedDateTime;
    return this.storedDateTime.AddSeconds(this.clockSeconds);
  }

  private IEnumerator clockCounter()
  {
    while (true)
    {
      yield return (object) Globals.Instance.WaitForSeconds(1f);
      ++this.clockSeconds;
    }
  }

  public Dictionary<string, Hero> GameHeroes
  {
    get => this.gameHeroes;
    set => this.gameHeroes = value;
  }

  public bool ConfigAutoEnd
  {
    get => this.configAutoEnd;
    set => this.configAutoEnd = value;
  }

  public bool ConfigShowEffects
  {
    get => this.configShowEffects;
    set => this.configShowEffects = value;
  }

  public bool ConfigExtendedDescriptions
  {
    get => this.configExtendedDescriptions;
    set => this.configExtendedDescriptions = value;
  }

  public bool ConfigACBackgrounds
  {
    get => this.configACBackgrounds;
    set => this.configACBackgrounds = value;
  }

  public bool ConfigRestartCombatOption
  {
    get => this.configRestartCombatOption;
    set => this.configRestartCombatOption = value;
  }

  public bool ConfigScreenShakeOption
  {
    get => this.configScreenShakeOption;
    set => this.configScreenShakeOption = value;
  }

  public bool ConfigBackgroundMute
  {
    get => this.configBackgroundMute;
    set => this.configBackgroundMute = value;
  }

  public bool ConfigVsync
  {
    get => this.configVsync;
    set => this.configVsync = value;
  }

  public string ProfileFolder
  {
    get => this.profileFolder;
    set => this.profileFolder = value;
  }

  public int ProfileId
  {
    get => this.profileId;
    set => this.profileId = value;
  }

  public bool ConfigKeyboardShortcuts
  {
    get => this.configKeyboardShortcuts;
    set => this.configKeyboardShortcuts = value;
  }
}
