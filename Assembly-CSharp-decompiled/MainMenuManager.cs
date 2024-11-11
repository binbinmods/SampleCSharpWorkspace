// Decompiled with JetBrains decompiler
// Type: MainMenuManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
  public Transform sceneCamera;
  public Transform gameModeType;
  public Transform gameModeChoose;
  public Transform gameModeSelectionMode;
  public Transform gameModeSelectionChoose;
  public Transform gameModeSelectionDescription;
  public Transform gameModeObeliskChains;
  public Transform gameModeWeeklyChains;
  private bool challengeLocked;
  private bool weeklyLocked;
  public TMP_Text roadmapTxt;
  public Transform credits;
  public Transform gameModeSelectionT;
  public Transform gameModeSelection0;
  public Transform gameModeSelection1;
  public Transform gameModeSelection2;
  public TMP_Text gameModeWeekly;
  public Transform joinT;
  public Transform exitT;
  public Transform logo;
  public Transform[] menuOps;
  public Transform[] profileOps;
  public TMP_Text[] profileOpsText;
  public MenuSaveButton[] menuSaveButtons;
  public Transform fadeBorders;
  public Transform menuT;
  public Transform socialT;
  public Transform saveT;
  public Transform profilesT;
  public Transform saveSlots;
  public Transform saveSingleT;
  public Transform saveMultiT;
  public Transform saveChallengeT;
  public Transform saveChallengeTSingle;
  public Transform saveChallengeTMulti;
  public TMP_Text saveTitle;
  public TMP_Text saveDescription;
  public TMP_Text challengeTitle;
  public TMP_Text challengeDescription;
  public Button challengeSPButton;
  public Button challengeMPButton;
  public TMP_Text version;
  public Transform demoMsg;
  public Transform roadmapMsg;
  public Transform bannerObject;
  public Transform exitSaveGameButton;
  public Transform wishlistButton;
  private GameData[] saveGames;
  private Coroutine maskCoroutine;
  public SpriteRenderer maskImage;
  private bool setWeekly;
  private bool singlePlayer = true;
  private int controllerCurrentOption = -1;
  public List<Transform> menuController0;
  public List<Transform> menuController1;
  public List<Transform> menuControllerProfiler;
  private int rankNeededForObelisk = 3;
  public TMP_Text creditsAdam;
  public TMP_Text creditsJavier;
  public TMP_Text creditsJuanjo;
  public TMP_Text creditsCollaborations;
  public TMP_Text creditsParadox;
  public TMP_Text creditsThanks;
  public Transform weeklyReward;
  public SpriteRenderer weeklyRewardCardback;
  public TMP_Text profileMenuText;
  private string[] profiles;
  private int profileCreateSlot;
  private Coroutine gameModeSelectionCoroutine;
  public Button DLChalloween;
  public Button DLCwolfwars;
  public Button DLCulminin;
  private List<Transform> controllerList = new List<Transform>();
  private Vector2 warpPosition;
  private Transform _opt;

  public static MainMenuManager Instance { get; private set; }

  private void Awake()
  {
    if ((UnityEngine.Object) GameManager.Instance == (UnityEngine.Object) null)
    {
      SceneStatic.LoadByName("MainMenu");
    }
    else
    {
      if ((UnityEngine.Object) MainMenuManager.Instance == (UnityEngine.Object) null)
        MainMenuManager.Instance = this;
      else if ((UnityEngine.Object) MainMenuManager.Instance != (UnityEngine.Object) this)
        UnityEngine.Object.Destroy((UnityEngine.Object) this);
      this.sceneCamera.gameObject.SetActive(false);
      NetworkManager.Instance.StartStopQueue(true);
    }
  }

  private void Update()
  {
    if (!this.setWeekly || Time.frameCount % 24 != 0)
      return;
    this.SetWeeklyLeft();
  }

  private void DoCredits()
  {
    string str1 = "<size=-1.5><color=#FFF>";
    string str2 = "</color></size>";
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("Adam Gándara Espart");
    stringBuilder.Append("<br>");
    stringBuilder.Append(str1);
    stringBuilder.Append(Texts.Instance.GetText("gameDesigner"));
    stringBuilder.Append("<br>");
    stringBuilder.Append(Texts.Instance.GetText("gameDeveloper"));
    stringBuilder.Append(str2);
    this.creditsAdam.text = stringBuilder.ToString();
    stringBuilder.Clear();
    stringBuilder.Append("Javier de Miguel del Valle");
    stringBuilder.Append("<br>");
    stringBuilder.Append(str1);
    stringBuilder.Append(Texts.Instance.GetText("gameDeveloper"));
    stringBuilder.Append("<br>");
    stringBuilder.Append(Texts.Instance.GetText("gameDesigner"));
    stringBuilder.Append(str2);
    this.creditsJavier.text = stringBuilder.ToString();
    stringBuilder.Clear();
    stringBuilder.Append("Juan Jose Nicolás Alvarado");
    stringBuilder.Append("<br>");
    stringBuilder.Append(str1);
    stringBuilder.Append(Texts.Instance.GetText("gameLeadArtist"));
    stringBuilder.Append("<br>");
    stringBuilder.Append(Texts.Instance.GetText("gameSeniorArtis"));
    stringBuilder.Append(str2);
    this.creditsJuanjo.text = stringBuilder.ToString();
    stringBuilder.Clear();
    stringBuilder.Append("<u><color=#FFF>");
    stringBuilder.Append(Texts.Instance.GetText("collaborations"));
    stringBuilder.Append("</color></u><br><line-height=50%><br></line-height>");
    stringBuilder.Append("<color=#AAA><size=-.5>(");
    stringBuilder.Append(Texts.Instance.GetText("iconArt"));
    stringBuilder.Append(")</size></color>  ");
    stringBuilder.Append("Francisco J.Cobo Molina");
    stringBuilder.Append("<br>");
    stringBuilder.Append("<color=#AAA><size=-.5>(");
    stringBuilder.Append(Texts.Instance.GetText("music"));
    stringBuilder.Append(")</size></color>  ");
    stringBuilder.Append("Alexander Nakarada");
    this.creditsCollaborations.text = stringBuilder.ToString();
    stringBuilder.Clear();
    stringBuilder.Append("<u><color=#FFF>");
    stringBuilder.Append(Texts.Instance.GetText("paradoxPublishing"));
    stringBuilder.Append("</color></u><br><line-height=50%><br></line-height>");
    stringBuilder.Append("Arnaud Schwarz, Dale Emasiri, Daniel Grigorov, Nils Brolin, Nils Löf, Ross Kaminskyi, Sebastian Forsström, Vivy Zhao");
    this.creditsParadox.text = stringBuilder.ToString();
    stringBuilder.Clear();
    stringBuilder.Append("<u><color=#FFF>");
    stringBuilder.Append(Texts.Instance.GetText("specialThanks"));
    stringBuilder.Append("</color></u><br><line-height=50%><br></line-height>");
    stringBuilder.Append(string.Format(Texts.Instance.GetText("creditsAnd"), (object) "Brandon, Corrufiles, Corydonn, Dyingshadow, Hatsusama, Otaku Oblivion, Stickmen"));
    this.creditsThanks.text = stringBuilder.ToString();
    stringBuilder.Clear();
  }

  private void Start()
  {
    this.DoCredits();
    this.credits.gameObject.SetActive(false);
    this.LoadProfiles();
    this.WriteRoadMap();
    GiveManager.Instance.ShowGive(false);
    ChatManager.Instance.DisableChat();
    this.HideGameModeSelection();
    SteamManager.Instance.SetRichPresence("steam_display", "#Status_MainMenu");
    AtOManager.Instance.ClearGame();
    PlayerManager.Instance.ClearAdventurePerks();
    NetworkManager.Instance.ClearPreviousInfo();
    PerkTree.Instance.Hide();
    CardScreenManager.Instance.ShowCardScreen(false);
    DamageMeterManager.Instance.Hide();
    GameManager.Instance.GameMode = Enums.GameMode.SinglePlayer;
    this.PositionMenuItems();
    this.ShowSaveGame(false);
    GameManager.Instance.SceneLoaded();
    GameManager.Instance.SetGameVersionText();
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("v.");
    stringBuilder.Append(GameManager.Instance.gameVersion);
    this.version.text = stringBuilder.ToString();
    AudioManager.Instance.StopAmbience();
    AudioManager.Instance.DoBSO("Game");
    AtOManager.Instance.CleanSaveSlot();
    if (GameManager.Instance.mainMenuGoToMultiplayer)
    {
      GameManager.Instance.mainMenuGoToMultiplayer = false;
      this.Multiplayer();
    }
    else
    {
      NetworkManager.Instance.Disconnect();
      this.exitSaveGameButton.gameObject.SetActive(true);
      this.ShowMask(false);
    }
    this.ShowDLCButtons();
  }

  private void ShowDLCButtons()
  {
    Color color = new Color(0.3f, 0.3f, 0.3f, 1f);
    if (!SteamManager.Instance.PlayerHaveDLC(Globals.Instance.SkuAvailable[0]))
      this.DLChalloween.colors = this.DLChalloween.colors with
      {
        normalColor = color
      };
    if (!SteamManager.Instance.PlayerHaveDLC(Globals.Instance.SkuAvailable[1]))
      this.DLCwolfwars.colors = this.DLCwolfwars.colors with
      {
        normalColor = color
      };
    if (SteamManager.Instance.PlayerHaveDLC(Globals.Instance.SkuAvailable[2]))
      return;
    this.DLCulminin.colors = this.DLCulminin.colors with
    {
      normalColor = color
    };
  }

  public void ButtonDLCAction(int _index) => Application.OpenURL("https://store.steampowered.com/app/" + Globals.Instance.SkuAvailable[_index]);

  public void LoadProfiles() => this.profiles = SaveManager.GetProfileNames();

  private void SetMenuCurrentProfile() => this.profileMenuText.text = string.Format(Texts.Instance.GetText("profileMenu"), (object) this.profiles[GameManager.Instance.ProfileId]);

  private void HideGameModeSelection()
  {
    if (this.weeklyReward.gameObject.activeSelf)
      this.weeklyReward.gameObject.SetActive(false);
    this.SelectGameMode(-1);
    this.gameModeSelectionMode.gameObject.SetActive(false);
    this.gameModeSelectionChoose.gameObject.SetActive(false);
    this.gameModeSelectionDescription.gameObject.SetActive(false);
    this.gameModeSelectionT.gameObject.SetActive(false);
    this.gameModeSelection0.gameObject.SetActive(false);
    this.gameModeSelection1.gameObject.SetActive(false);
    this.gameModeSelection2.gameObject.SetActive(false);
    this.joinT.gameObject.SetActive(false);
    this.exitT.gameObject.SetActive(false);
  }

  private void ShowGameModeSelection(int _type)
  {
    for (int index = 0; index < this.menuController0.Count; ++index)
    {
      if ((bool) (UnityEngine.Object) this.menuController0[index].GetComponent<MenuButton>())
        this.menuController0[index].GetComponent<MenuButton>().HoverOff();
    }
    this.exitT.GetChild(0).GetComponent<MenuButton>().HoverOff();
    this.challengeLocked = PlayerManager.Instance.GetHighestCharacterRank() >= this.rankNeededForObelisk ? (this.weeklyLocked = false) : (this.weeklyLocked = true);
    this.LoadSaveGames();
    this.ShowMask(true, 0.8f);
    this.profilesT.gameObject.SetActive(false);
    this.menuT.gameObject.SetActive(false);
    this.bannerObject.gameObject.SetActive(false);
    this.HideGameModeSelection();
    this.gameModeSelection0.GetChild(0).GetComponent<BotonMenuGameMode>().TurnOffState();
    this.gameModeSelection1.GetChild(0).GetComponent<BotonMenuGameMode>().TurnOffState();
    this.gameModeSelection2.GetChild(0).GetComponent<BotonMenuGameMode>().TurnOffState();
    switch (_type)
    {
      case 0:
        this.gameModeType.GetComponent<TMP_Text>().text = Texts.Instance.GetText("mainMenuSaveSingle");
        this.gameModeSelectionMode.GetComponent<TMP_Text>().text = Texts.Instance.GetText("mainMenuSaveSingle");
        break;
      case 1:
        this.gameModeType.GetComponent<TMP_Text>().text = Texts.Instance.GetText("mainMenuSaveMulti");
        this.gameModeSelectionMode.GetComponent<TMP_Text>().text = Texts.Instance.GetText("mainMenuSaveMulti");
        this.joinT.gameObject.SetActive(true);
        break;
    }
    this.exitT.gameObject.SetActive(true);
    this.gameModeType.gameObject.SetActive(true);
    this.gameModeChoose.gameObject.SetActive(true);
    if (this.gameModeSelectionCoroutine != null)
      this.StopCoroutine(this.gameModeSelectionCoroutine);
    this.gameModeSelectionCoroutine = this.StartCoroutine(this.ShowGameModeSelectionCo());
  }

  private void SelectGameMode(int _index)
  {
    if (this.gameModeSelectionCoroutine != null)
      this.StopCoroutine(this.gameModeSelectionCoroutine);
    this.gameModeWeekly.text = "";
    this.setWeekly = false;
    this.gameModeType.gameObject.SetActive(false);
    this.gameModeChoose.gameObject.SetActive(false);
    this.gameModeSelectionMode.gameObject.SetActive(true);
    this.gameModeSelectionChoose.gameObject.SetActive(true);
    this.gameModeSelectionDescription.gameObject.SetActive(true);
    this.gameModeSelection0.gameObject.SetActive(true);
    this.gameModeSelection1.gameObject.SetActive(true);
    this.gameModeSelection2.gameObject.SetActive(true);
    if (_index == 0 || _index == 1)
    {
      GameManager.Instance.GameType = Enums.GameType.Adventure;
      this.gameModeSelection0.localPosition = new Vector3(-8f, 0.0f, 0.0f);
      this.gameModeSelection0.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
      this.StartCoroutine(this.BotonMenuGameModeStateOn(0));
      this.gameModeSelectionChoose.GetComponent<TMP_Text>().text = Texts.Instance.GetText("modeAdventure");
      this.gameModeSelectionDescription.GetComponent<TMP_Text>().text = Texts.Instance.GetText("mainMenuAdventureDescription");
    }
    else if (_index > -1)
    {
      this.gameModeSelection0.gameObject.SetActive(false);
    }
    else
    {
      this.gameModeSelection0.localPosition = new Vector3(-8f, 0.0f, 0.0f);
      this.gameModeSelection0.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
    }
    if (_index == 2 || _index == 3)
    {
      GameManager.Instance.GameType = Enums.GameType.Challenge;
      this.gameModeSelection1.localPosition = new Vector3(-8f, 0.0f, 0.0f);
      this.gameModeSelection1.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
      this.StartCoroutine(this.BotonMenuGameModeStateOn(1));
      this.gameModeSelectionChoose.GetComponent<TMP_Text>().text = Texts.Instance.GetText("modeObelisk");
      this.gameModeSelectionDescription.GetComponent<TMP_Text>().text = Texts.Instance.GetText("mainMenuObeliskDescription");
    }
    else if (_index > -1)
    {
      this.gameModeSelection1.gameObject.SetActive(false);
    }
    else
    {
      this.gameModeSelection1.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
      this.gameModeSelection1.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
    }
    if (_index == 4 || _index == 5)
    {
      GameManager.Instance.GameType = Enums.GameType.WeeklyChallenge;
      this.gameModeSelection2.localPosition = new Vector3(-8f, 0.0f, 0.0f);
      this.gameModeSelection2.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
      this.StartCoroutine(this.BotonMenuGameModeStateOn(2));
      this.gameModeSelectionChoose.GetComponent<TMP_Text>().text = Texts.Instance.GetText("modeWeekly");
      this.gameModeSelectionDescription.GetComponent<TMP_Text>().text = Texts.Instance.GetText("mainMenuWeeklyDescription");
      this.SetWeeklyLeft();
      this.setWeekly = true;
      ChallengeData weeklyData = Globals.Instance.GetWeeklyData(Functions.GetCurrentWeeklyWeek());
      if (!((UnityEngine.Object) weeklyData != (UnityEngine.Object) null))
        return;
      CardbackData cardbackData = weeklyData.GetCardbackData();
      if (!((UnityEngine.Object) cardbackData != (UnityEngine.Object) null))
        return;
      this.weeklyReward.gameObject.SetActive(true);
      this.weeklyRewardCardback.sprite = cardbackData.CardbackSprite;
    }
    else if (_index > -1)
    {
      this.gameModeSelection2.gameObject.SetActive(false);
    }
    else
    {
      this.gameModeSelection2.localPosition = new Vector3(8f, 0.0f, 0.0f);
      this.gameModeSelection2.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
      this.gameModeSelection2.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
    }
  }

  private IEnumerator BotonMenuGameModeStateOn(int _option)
  {
    yield return (object) null;
    switch (_option)
    {
      case 0:
        this.gameModeSelection0.GetChild(0).GetComponent<BotonMenuGameMode>().TurnOnState();
        break;
      case 1:
        this.gameModeSelection1.GetChild(0).GetComponent<BotonMenuGameMode>().TurnOnState();
        break;
      case 2:
        this.gameModeSelection2.GetChild(0).GetComponent<BotonMenuGameMode>().TurnOnState();
        break;
    }
  }

  private IEnumerator ShowGameModeSelectionCo()
  {
    this.gameModeSelectionT.gameObject.SetActive(true);
    if (this.challengeLocked)
      this.gameModeObeliskChains.gameObject.SetActive(true);
    else
      this.gameModeObeliskChains.gameObject.SetActive(false);
    if (this.weeklyLocked)
      this.gameModeWeeklyChains.gameObject.SetActive(true);
    else
      this.gameModeWeeklyChains.gameObject.SetActive(false);
    this.gameModeSelection0.gameObject.SetActive(true);
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    this.gameModeSelection1.gameObject.SetActive(true);
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    this.gameModeSelection2.gameObject.SetActive(true);
  }

  public void GoToSteamPage() => Application.OpenURL("https://store.steampowered.com/app/1385380/Across_the_Obelisk/");

  public void GoToLeaderboard() => Application.OpenURL("https://steamcommunity.com/stats/1421400/leaderboards/6109803");

  public void GoToDiscord() => GameManager.Instance.Discord();

  private void LoadSaveGames() => this.saveGames = SaveManager.SaveGamesList();

  private void PositionSaveItems(int section)
  {
    int num = 0;
    switch (section)
    {
      case 0:
        num = 0;
        break;
      case 1:
        num = 6;
        break;
      case 2:
        num = 12;
        break;
      case 3:
        num = 18;
        break;
      case 4:
        num = 24;
        break;
      case 5:
        num = 30;
        break;
    }
    for (int index = 0; index < 3; ++index)
    {
      if ((UnityEngine.Object) this.menuSaveButtons[index] != (UnityEngine.Object) null)
      {
        this.menuSaveButtons[index].SetSlot(num + index);
        if (this.saveGames[num + index] != null)
        {
          this.menuSaveButtons[index].SetActive(true);
          this.menuSaveButtons[index].SetGameData(this.saveGames[num + index]);
        }
        else
          this.menuSaveButtons[index].SetActive(false);
      }
    }
  }

  public void Resize() => this.PositionMenuItems();

  private void PositionMenuItems()
  {
    float num1 = 50f * Globals.Instance.multiplierY * (float) (1920.0 * (double) Screen.height / (1080.0 * (double) Screen.width));
    float num2 = (float) ((double) num1 * 2.0 - 530.0);
    int num3 = 0;
    for (int index = this.menuOps.Length - 1; index >= 0; --index)
    {
      if ((UnityEngine.Object) this.menuOps[index] != (UnityEngine.Object) null)
        this.menuOps[index].transform.localPosition = new Vector3(this.menuOps[index].transform.localPosition.x, num2 + (float) num3 * num1);
      ++num3;
    }
    int num4 = 0;
    float y = this.menuOps[this.menuOps.Length - 3].transform.localPosition.y;
    for (int index = this.profileOps.Length - 1; index >= 0; --index)
    {
      if ((UnityEngine.Object) this.profileOps[index] != (UnityEngine.Object) null)
        this.profileOps[index].transform.localPosition = new Vector3(this.profileOps[index].transform.localPosition.x, y + (float) num4 * num1);
      ++num4;
    }
  }

  private void ClearButtonsState()
  {
    for (int index = 0; index < this.menuOps.Length; ++index)
    {
      if ((UnityEngine.Object) this.menuOps[index] != (UnityEngine.Object) null)
      {
        Button component = this.menuOps[index].GetComponent<Button>();
        component.interactable = false;
        component.interactable = true;
      }
    }
  }

  public bool IsSaveMenuActive() => this.saveT.gameObject.activeSelf;

  public bool IsGameModesActive() => this.gameModeSelectionT.gameObject.activeSelf;

  public void ShowCredits()
  {
    this.ShowMask(true, 0.8f);
    this.profilesT.gameObject.SetActive(false);
    this.menuT.gameObject.SetActive(false);
    this.bannerObject.gameObject.SetActive(false);
    this.credits.gameObject.SetActive(true);
    this.ClearButtonsState();
  }

  public void ShowSaveGame(bool status, int section = -1)
  {
    if (this.challengeLocked && section == 2 || this.weeklyLocked && section == 4)
      return;
    this.ShowMask(status, 0.8f);
    this.profilesT.gameObject.SetActive(false);
    this.saveT.gameObject.SetActive(status);
    this.menuT.gameObject.SetActive(!status);
    this.credits.gameObject.SetActive(false);
    this.bannerObject.gameObject.SetActive(!status);
    if (status)
    {
      this.saveSlots.gameObject.SetActive(true);
      if (!this.singlePlayer)
        ++section;
      this.PositionSaveItems(section);
      this.SelectGameMode(section);
    }
    else
      this.SetMenuCurrentProfile();
  }

  public void NewGame()
  {
    GameManager.Instance.GameType = Enums.GameType.Adventure;
    this.singlePlayer = true;
    this.ShowGameModeSelection(0);
  }

  public void LoadGame()
  {
    GameManager.Instance.GameType = Enums.GameType.Adventure;
    this.ShowSaveGame(true, 0);
    this.ClearButtonsState();
  }

  public void ExitSaveGame(bool _forceIt = false)
  {
    if (!_forceIt && AlertManager.Instance.IsActive())
      return;
    if (NetworkManager.Instance.IsConnected())
      NetworkManager.Instance.Disconnect();
    this.setWeekly = false;
    this.HideGameModeSelection();
    this.LoadSaveGames();
    this.ShowSaveGame(false);
  }

  public void ObeliskChallenge()
  {
    GameManager.Instance.GameType = Enums.GameType.Challenge;
    this.ShowSaveGame(true, 2);
    this.ClearButtonsState();
    this.challengeTitle.text = Texts.Instance.GetText("menuChallenge");
  }

  public void ObeliskChallengeSP()
  {
    this.ShowSaveGame(true, 2);
    this.ClearButtonsState();
  }

  public void ObeliskChallengeMP()
  {
    this.ShowSaveGame(true, 3);
    this.ClearButtonsState();
  }

  public void WeeklyChallenge()
  {
    GameManager.Instance.GameType = Enums.GameType.WeeklyChallenge;
    this.ShowSaveGame(true, 2);
    this.ClearButtonsState();
    this.SetWeeklyLeft();
    this.setWeekly = true;
  }

  public void Multiplayer()
  {
    this.ShowGameModeSelection(1);
    this.singlePlayer = false;
    GameManager.Instance.GameType = Enums.GameType.Adventure;
  }

  public void JoinMultiplayer()
  {
    GameManager.Instance.GameType = Enums.GameType.Adventure;
    SceneStatic.LoadByName("Lobby");
  }

  public void TomeOfKnowledge()
  {
    SceneStatic.LoadByName(nameof (TomeOfKnowledge));
    this.ClearButtonsState();
  }

  public void Statistics() => this.ClearButtonsState();

  public void Settings()
  {
    SettingsManager.Instance.ShowSettings(true);
    this.ClearButtonsState();
  }

  public void Credits()
  {
    this.ShowCredits();
    this.ClearButtonsState();
  }

  public void QuitGame()
  {
    GameManager.Instance.QuitGame();
    this.ClearButtonsState();
  }

  public void ShowProfiles()
  {
    this.LoadProfiles();
    this.profilesT.gameObject.SetActive(true);
    this.menuT.gameObject.SetActive(false);
    this.exitT.gameObject.SetActive(true);
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < this.profileOps.Length; ++index)
    {
      stringBuilder.Clear();
      if (index == GameManager.Instance.ProfileId)
        stringBuilder.Append("<b><sprite name=experience> ");
      if (this.profiles[index] == "")
      {
        stringBuilder.Append("<size=-4><color=#B0B0B0>");
        stringBuilder.Append(Texts.Instance.GetText("profileCreate"));
      }
      else
        stringBuilder.Append(this.profiles[index]);
      this.profileOpsText[index].text = stringBuilder.ToString();
    }
  }

  public void UseProfile(int _slot)
  {
    if (_slot == GameManager.Instance.ProfileId)
      this.ExitSaveGame();
    else if (this.profiles[_slot] == "")
    {
      this.CreateProfile(_slot);
    }
    else
    {
      GameManager.Instance.UseProfileFile(_slot);
      GameManager.Instance.ReloadProfile();
      this.ExitSaveGame(true);
    }
  }

  private void CreateProfile(int _slot)
  {
    if (_slot == 0)
      return;
    this.profileCreateSlot = _slot;
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.CreateProfileAction);
    AlertManager.Instance.AlertInput(Texts.Instance.GetText("inputConfigSaveName"), Texts.Instance.GetText("accept").ToUpper());
  }

  public void CreateProfileAction()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.CreateProfileAction);
    string _name = Functions.OnlyAscii(AlertManager.Instance.GetInputValue()).ToUpper().Trim();
    if (_name == "")
      return;
    SaveManager.CreateProfileFolder(this.profileCreateSlot, _name);
  }

  public void CloseDemoMsg() => this.demoMsg.gameObject.SetActive(false);

  private void ShowMask(bool state, float alpha = 1f)
  {
    if (this.maskCoroutine != null)
      this.StopCoroutine(this.maskCoroutine);
    if (state)
      this.maskCoroutine = this.StartCoroutine(this.ShowMaskCo(alpha));
    else
      this.maskCoroutine = this.StartCoroutine(this.HideMaskCo());
  }

  private IEnumerator ShowMaskCo(float alpha = 1f)
  {
    float index = this.maskImage.color.a;
    while ((double) index < (double) alpha)
    {
      this.maskImage.color = new Color(0.0f, 0.0f, 0.0f, index);
      index += 0.025f;
      yield return (object) null;
    }
    this.maskImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
  }

  private IEnumerator HideMaskCo()
  {
    float index = this.maskImage.color.a;
    while ((double) index > 0.0)
    {
      this.maskImage.color = new Color(0.0f, 0.0f, 0.0f, index);
      index -= 0.025f;
      yield return (object) null;
    }
  }

  private void SetWeeklyLeft()
  {
    TimeSpan timeSpan = Functions.TimeSpanLeftWeekly();
    string str = string.Format("{0:D2}h. {1:D2}m. {2:D2}s.", (object) (int) timeSpan.TotalHours, (object) timeSpan.Minutes, (object) timeSpan.Seconds);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<b><size=+2>");
    stringBuilder.Append(AtOManager.Instance.GetWeeklyName(Functions.GetCurrentWeeklyWeek()));
    stringBuilder.Append("</size></b>\n");
    stringBuilder.Append(str);
    this.gameModeWeekly.text = stringBuilder.ToString();
  }

  private void WriteRoadMap()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Texts.Instance.GetText("roadmapHomeText"));
    stringBuilder.Replace("<tit>", "<color=#A3D2FF><size=+4>");
    stringBuilder.Replace("</tit>", "</color></size><br><br>");
    this.roadmapTxt.text = stringBuilder.ToString();
  }

  public void ControllerMovement(bool goingUp = false, bool goingRight = false, bool goingDown = false, bool goingLeft = false)
  {
    if (Functions.TransformIsVisible(this.credits))
    {
      this.ExitSaveGame();
    }
    else
    {
      bool flag = false;
      int num = 0;
      this.controllerList = this.IsSaveMenuActive() || this.IsGameModesActive() ? this.menuController1 : (!this.profilesT.gameObject.activeSelf ? this.menuController0 : this.menuControllerProfiler);
      if (this.controllerList == null)
        return;
      for (; !flag && num < 20; ++num)
      {
        if (goingUp | goingLeft)
        {
          --this.controllerCurrentOption;
          if (this.controllerCurrentOption < 0)
            this.controllerCurrentOption = 0;
        }
        else
        {
          if (goingDown && this.controllerList == this.menuController1 && (this.controllerCurrentOption == 5 || this.controllerCurrentOption == 7 || this.controllerCurrentOption == 9))
            ++this.controllerCurrentOption;
          ++this.controllerCurrentOption;
          if (this.controllerCurrentOption > this.controllerList.Count - 1)
            this.controllerCurrentOption = this.controllerList.Count - 1;
        }
        this._opt = this.controllerList[this.controllerCurrentOption];
        if ((UnityEngine.Object) this._opt != (UnityEngine.Object) null && Functions.TransformIsVisible(this._opt))
        {
          if ((bool) (UnityEngine.Object) this._opt.GetComponent<Button>())
          {
            this.warpPosition = (Vector2) this._opt.position;
            Mouse.current.WarpCursorPosition(this.warpPosition);
            flag = true;
          }
          else if (this._opt.GetComponent<BoxCollider2D>().enabled)
          {
            this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._opt.position);
            Mouse.current.WarpCursorPosition(this.warpPosition);
            flag = true;
          }
        }
      }
    }
  }
}
