// Decompiled with JetBrains decompiler
// Type: SettingsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
  public GameObject canvas;
  public TMP_Text seedText;
  public AudioMixer audioMixer;
  public Transform graphicsTab;
  public Transform audioTab;
  public Transform gameplayTab;
  public Button[] optionButton;
  public Transform[] optionSelector;
  public Transform resetSavedT;
  public Toggle vsyncToggle;
  public Toggle fullscreenToggle;
  public Toggle resetSavedToggle;
  public Toggle resetTutorialToggle;
  public Toggle extendedDescriptionsToggle;
  public Toggle backgroundMuteToggle;
  public Slider masterVolumeSlider;
  public Slider effectsVolumeSlider;
  public Slider bsoVolumeSlider;
  public Slider ambienceVolumeSlider;
  public Toggle fastModeToggle;
  public Toggle autoEndToggle;
  public Toggle showEffectsToggle;
  public Toggle acbackgroundEffectsToggle;
  public Toggle restartCombatOptionToggle;
  public Toggle screenShakeOptionToggle;
  public Toggle keyboardShortcutsToggle;
  public Toggle followingTheLeaderToggle;
  private Resolution[] resolutions;
  private List<string> resolutionsList;
  public TMP_Dropdown resolutionDropdown;
  public TMP_Dropdown languageDropdown;
  private bool languageDropdownInitiated;
  public Transform langCommunity;
  private int actualTabIndex = -1;

  public static SettingsManager Instance { get; private set; }

  private void Awake()
  {
    if ((UnityEngine.Object) SettingsManager.Instance == (UnityEngine.Object) null)
      SettingsManager.Instance = this;
    else if ((UnityEngine.Object) SettingsManager.Instance != (UnityEngine.Object) this)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
  }

  private void Start() => this.GetResolutions();

  public void Resize()
  {
    if ((double) Screen.width / (double) Screen.height > 1.7799999713897705)
      this.canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;
    else
      this.canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.0f;
  }

  public bool IsActive() => this.canvas.gameObject.activeSelf;

  public void ShowSettings(bool _state)
  {
    this.canvas.gameObject.SetActive(_state);
    if (!_state)
    {
      SaveManager.SavePrefs();
      if ((bool) (UnityEngine.Object) CardCraftManager.Instance)
        CardCraftManager.Instance.ShowSearch(true);
      if (!TomeManager.Instance.IsActive())
        return;
      TomeManager.Instance.ShowSearch(true);
    }
    else
    {
      if ((bool) (UnityEngine.Object) PopupManager.Instance)
        PopupManager.Instance.ClosePopup();
      if ((bool) (UnityEngine.Object) CardCraftManager.Instance)
        CardCraftManager.Instance.ShowSearch(false);
      if (TomeManager.Instance.IsActive())
        TomeManager.Instance.ShowSearch(false);
      string str = "";
      if (!GameManager.Instance.IsWeeklyChallenge())
        str = AtOManager.Instance.GetGameId();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<color=#AAA><size=-10>");
      stringBuilder.Append("AtO #");
      stringBuilder.Append(GameManager.Instance.gameVersion);
      if (str != "")
      {
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("gameSeed"));
        stringBuilder.Append("</size></color><br>");
        stringBuilder.Append(str);
      }
      this.seedText.text = stringBuilder.ToString();
      this.graphicsTab.gameObject.SetActive(true);
      this.audioTab.gameObject.SetActive(false);
      this.gameplayTab.gameObject.SetActive(false);
      if (SceneStatic.GetSceneName() == "MainMenu")
        this.resetSavedT.gameObject.SetActive(true);
      else
        this.resetSavedT.gameObject.SetActive(false);
      this.optionButton[0].Select();
    }
  }

  public void SelectTab(int _tabIndex)
  {
    if (this.actualTabIndex == _tabIndex)
      return;
    this.ShowTab(this.actualTabIndex, false);
    this.actualTabIndex = _tabIndex;
    this.ShowTab(_tabIndex, true);
  }

  public void ShowTab(int _tabIndex, bool state)
  {
    switch (_tabIndex)
    {
      case -1:
        return;
      case 0:
        this.graphicsTab.gameObject.SetActive(state);
        break;
      case 1:
        this.audioTab.gameObject.SetActive(state);
        break;
      case 2:
        this.gameplayTab.gameObject.SetActive(state);
        break;
    }
    this.optionSelector[_tabIndex].gameObject.SetActive(state);
  }

  public static List<Resolution> GetResolutionsList()
  {
    Resolution[] resolutions = Screen.resolutions;
    HashSet<Tuple<int, int>> tupleSet = new HashSet<Tuple<int, int>>();
    Dictionary<Tuple<int, int>, int> dictionary = new Dictionary<Tuple<int, int>, int>();
    for (int index = 0; index < resolutions.GetLength(0); ++index)
    {
      Tuple<int, int> key = new Tuple<int, int>(resolutions[index].width, resolutions[index].height);
      tupleSet.Add(key);
      if (!dictionary.ContainsKey(key))
        dictionary.Add(key, resolutions[index].refreshRate);
      else
        dictionary[key] = resolutions[index].refreshRate;
    }
    List<Resolution> resolutionsList = new List<Resolution>(tupleSet.Count);
    foreach (Tuple<int, int> key in tupleSet)
    {
      Resolution resolution = new Resolution();
      resolution.width = key.Item1;
      resolution.height = key.Item2;
      int num;
      if (dictionary.TryGetValue(key, out num))
        resolution.refreshRate = num;
      resolutionsList.Add(resolution);
    }
    return resolutionsList;
  }

  private void GetResolutions()
  {
    this.resolutions = ((IEnumerable<Resolution>) Screen.resolutions).Select<Resolution, Resolution>((Func<Resolution, Resolution>) (resolution => new Resolution()
    {
      width = resolution.width,
      height = resolution.height
    })).Where<Resolution>((Func<Resolution, bool>) (resolution => resolution.width >= 1024)).Distinct<Resolution>().ToArray<Resolution>();
    this.resolutionDropdown.ClearOptions();
    this.resolutionsList = new List<string>();
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < this.resolutions.Length; ++index)
    {
      stringBuilder.Clear();
      stringBuilder.Append(this.resolutions[index].width);
      stringBuilder.Append(" x ");
      stringBuilder.Append(this.resolutions[index].height);
      this.resolutionsList.Add(stringBuilder.ToString());
    }
    this.resolutionDropdown.AddOptions(this.resolutionsList);
    this.SelectCurrentResolution();
  }

  private void SelectCurrentResolution()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Screen.width);
    stringBuilder.Append(" x ");
    stringBuilder.Append(Screen.height);
    for (int index = 0; index < this.resolutionsList.Count; ++index)
    {
      if (this.resolutionsList[index] == stringBuilder.ToString())
      {
        this.resolutionDropdown.value = index;
        this.resolutionDropdown.RefreshShownValue();
        break;
      }
    }
    this.fullscreenToggle.isOn = Screen.fullScreen;
  }

  public void LoadPrefs()
  {
    if (SaveManager.PrefsHasKey("language"))
    {
      Globals.Instance.SetLang(SaveManager.LoadPrefsInt("language"));
      this.languageDropdown.value = SaveManager.LoadPrefsInt("language");
      this.languageDropdown.RefreshShownValue();
      if (this.languageDropdown.value > 2)
        this.langCommunity.gameObject.SetActive(true);
      else
        this.langCommunity.gameObject.SetActive(false);
    }
    this.languageDropdownInitiated = true;
    if (SaveManager.PrefsHasKey("vsync"))
    {
      GameManager.Instance.ConfigVsync = SaveManager.LoadPrefsBool("vsync");
      GameManager.Instance.SetVsync();
    }
    this.vsyncToggle.isOn = GameManager.Instance.ConfigVsync;
    if (SaveManager.PrefsHasKey("masterVolume"))
    {
      float num = SaveManager.LoadPrefsFloat("masterVolume");
      this.audioMixer.SetFloat("masterVolume", num);
      this.masterVolumeSlider.value = num;
    }
    if (SaveManager.PrefsHasKey("effectsVolume"))
    {
      float num = SaveManager.LoadPrefsFloat("effectsVolume");
      this.audioMixer.SetFloat("effectsVolume", num);
      this.effectsVolumeSlider.value = num;
    }
    if (SaveManager.PrefsHasKey("bsoVolume"))
    {
      float num = SaveManager.LoadPrefsFloat("bsoVolume");
      this.audioMixer.SetFloat("bsoVolume", num);
      this.bsoVolumeSlider.value = num;
    }
    if (SaveManager.PrefsHasKey("ambienceVolume"))
    {
      float num = SaveManager.LoadPrefsFloat("ambienceVolume");
      this.audioMixer.SetFloat("ambienceVolume", num);
      this.ambienceVolumeSlider.value = num;
    }
    if (SaveManager.PrefsHasKey("backgroundMute"))
      GameManager.Instance.ConfigBackgroundMute = SaveManager.LoadPrefsBool("backgroundMute");
    this.backgroundMuteToggle.isOn = GameManager.Instance.ConfigBackgroundMute;
    bool flag1 = GameManager.Instance.configGameSpeed == Enums.ConfigSpeed.Fast;
    if (SaveManager.PrefsHasKey("fastMode"))
      flag1 = SaveManager.LoadPrefsBool("fastMode");
    this.fastModeToggle.isOn = flag1;
    GameManager.Instance.configGameSpeed = !flag1 ? Enums.ConfigSpeed.Slow : Enums.ConfigSpeed.Fast;
    bool flag2 = GameManager.Instance.ConfigAutoEnd;
    if (SaveManager.PrefsHasKey("autoEnd"))
      flag2 = SaveManager.LoadPrefsBool("autoEnd");
    this.autoEndToggle.isOn = flag2;
    GameManager.Instance.ConfigAutoEnd = flag2;
    bool flag3 = GameManager.Instance.ConfigShowEffects;
    if (SaveManager.PrefsHasKey("showEffects"))
      flag3 = SaveManager.LoadPrefsBool("showEffects");
    this.showEffectsToggle.isOn = flag3;
    GameManager.Instance.ConfigShowEffects = flag3;
    bool flag4 = GameManager.Instance.ConfigACBackgrounds;
    if (SaveManager.PrefsHasKey("acBackgrounds"))
      flag4 = SaveManager.LoadPrefsBool("acBackgrounds");
    this.acbackgroundEffectsToggle.isOn = flag4;
    GameManager.Instance.ConfigACBackgrounds = flag4;
    bool flag5 = GameManager.Instance.ConfigRestartCombatOption;
    if (SaveManager.PrefsHasKey("restartCombatOptionNew"))
      flag5 = SaveManager.LoadPrefsBool("restartCombatOptionNew");
    this.restartCombatOptionToggle.isOn = flag5;
    GameManager.Instance.ConfigRestartCombatOption = flag5;
    bool flag6 = GameManager.Instance.ConfigScreenShakeOption;
    if (SaveManager.PrefsHasKey("screenShakeOption"))
      flag6 = SaveManager.LoadPrefsBool("screenShakeOption");
    this.screenShakeOptionToggle.isOn = flag6;
    GameManager.Instance.ConfigScreenShakeOption = flag6;
    bool flag7 = GameManager.Instance.ConfigKeyboardShortcuts;
    if (SaveManager.PrefsHasKey("keyboardShortcuts"))
      flag7 = SaveManager.LoadPrefsBool("keyboardShortcuts");
    this.keyboardShortcutsToggle.isOn = flag7;
    GameManager.Instance.ConfigKeyboardShortcuts = flag7;
    bool flag8 = false;
    if (SaveManager.PrefsHasKey("followLeader"))
      flag8 = SaveManager.LoadPrefsBool("followLeader");
    this.followingTheLeaderToggle.isOn = flag8;
    AtOManager.Instance.followingTheLeader = flag8;
    this.SetResolutionDefault();
  }

  public void SetLanguage(int _langIndex)
  {
    if (!this.IsActive())
      return;
    SaveManager.SaveIntoPrefsInt("language", _langIndex);
    if (this.languageDropdownInitiated)
      AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("selectLanguageChanged"));
    if (_langIndex > 2)
      this.langCommunity.gameObject.SetActive(true);
    else
      this.langCommunity.gameObject.SetActive(false);
  }

  public void SetResolution(int _resolutionIndex)
  {
    if (!this.IsActive())
      return;
    Resolution resolution = this.resolutions[_resolutionIndex];
    if (resolution.width == Screen.width && resolution.height == Screen.height)
      return;
    if (resolution.width < 1024)
      resolution.width = 1024;
    if (resolution.height < 768)
      resolution.height = 768;
    Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    SaveManager.SaveIntoPrefsInt("widthResolution", resolution.width);
    SaveManager.SaveIntoPrefsInt("heightResolution", resolution.height);
    SaveManager.SaveIntoPrefsBool("fullScreen", Screen.fullScreen);
  }

  public void SetResolutionDefault()
  {
    if (!SaveManager.PrefsHasKey("widthResolution") || !SaveManager.PrefsHasKey("heightResolution"))
      return;
    int width = SaveManager.LoadPrefsInt("widthResolution");
    int height = SaveManager.LoadPrefsInt("heightResolution");
    bool fullscreen = true;
    if (width < 1024)
      width = 1024;
    if (height < 768)
      height = 768;
    if (SaveManager.PrefsHasKey("fullScreen"))
      fullscreen = SaveManager.LoadPrefsBool("fullScreen");
    Screen.SetResolution(width, height, fullscreen);
  }

  public void SetQuality(int _qualityIndex) => QualitySettings.SetQualityLevel(_qualityIndex);

  public void SetFullscreen(bool _isFullscreen)
  {
    Screen.fullScreen = _isFullscreen;
    SaveManager.SaveIntoPrefsBool("fullScreen", _isFullscreen);
  }

  public void SetVsync(bool _isVsync)
  {
    SaveManager.SaveIntoPrefsBool("vsync", _isVsync);
    GameManager.Instance.ConfigVsync = _isVsync;
    GameManager.Instance.SetVsync();
  }

  public void ResetSavedData()
  {
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.ResetSavedDataAction);
    AlertManager.Instance.AlertConfirmDouble(Texts.Instance.GetText("removeProgressQuestion"));
    this.resetSavedToggle.isOn = false;
  }

  private void ResetSavedDataAction()
  {
    int num = AlertManager.Instance.GetConfirmAnswer() ? 1 : 0;
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.ResetSavedDataAction);
    if (num == 0)
      return;
    SaveManager.CleanSavePlayerData();
  }

  public void ResetTutorial()
  {
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.ResetTutorialAction);
    AlertManager.Instance.AlertConfirmDouble(Texts.Instance.GetText("resetTutorialQuestion"));
    this.resetTutorialToggle.isOn = false;
  }

  private void ResetTutorialAction()
  {
    int num = AlertManager.Instance.GetConfirmAnswer() ? 1 : 0;
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.ResetTutorialAction);
    if (num == 0)
      return;
    SaveManager.ResetTutorial();
  }

  public void SetMasterVolume(float _volume)
  {
    this.audioMixer.SetFloat("masterVolume", Mathf.Log10(_volume) * 20f);
    SaveManager.SaveIntoPrefsFloat("masterVolume", _volume);
  }

  public void SetEffectsVolume(float _volume)
  {
    this.audioMixer.SetFloat("effectsVolume", Mathf.Log10(_volume) * 20f);
    SaveManager.SaveIntoPrefsFloat("effectsVolume", _volume);
  }

  public void SetBSOVolume(float _volume)
  {
    this.audioMixer.SetFloat("bsoVolume", Mathf.Log10(_volume) * 20f);
    SaveManager.SaveIntoPrefsFloat("bsoVolume", _volume);
  }

  public void SetAmbienceVolume(float _volume)
  {
    this.audioMixer.SetFloat("ambienceVolume", Mathf.Log10(_volume) * 20f);
    SaveManager.SaveIntoPrefsFloat("ambienceVolume", _volume);
  }

  public void SetBackgroundMute(bool _backgroundMute)
  {
    SaveManager.SaveIntoPrefsBool("backgroundMute", _backgroundMute);
    GameManager.Instance.ConfigBackgroundMute = _backgroundMute;
  }

  public void SetFastMode(bool _fastMode)
  {
    SaveManager.SaveIntoPrefsBool("fastMode", _fastMode);
    if (_fastMode)
      GameManager.Instance.configGameSpeed = Enums.ConfigSpeed.Fast;
    else
      GameManager.Instance.configGameSpeed = Enums.ConfigSpeed.Slow;
  }

  public void SetAutoEnd(bool _autoEnd)
  {
    SaveManager.SaveIntoPrefsBool("autoEnd", _autoEnd);
    GameManager.Instance.ConfigAutoEnd = _autoEnd;
  }

  public void SetShowEffects(bool _showEffects)
  {
    SaveManager.SaveIntoPrefsBool("showEffects", _showEffects);
    GameManager.Instance.ConfigShowEffects = _showEffects;
  }

  public void SetRestartCombat(bool _restartCombat)
  {
    SaveManager.SaveIntoPrefsBool("restartCombatOptionNew", _restartCombat);
    GameManager.Instance.ConfigRestartCombatOption = _restartCombat;
  }

  public void SetScreenShake(bool _screenShake)
  {
    SaveManager.SaveIntoPrefsBool("screenShakeOption", _screenShake);
    GameManager.Instance.ConfigScreenShakeOption = _screenShake;
  }

  public void SetKeyboardShortcuts(bool _keyShortcuts)
  {
    SaveManager.SaveIntoPrefsBool("keyboardShortcuts", _keyShortcuts);
    GameManager.Instance.ConfigKeyboardShortcuts = _keyShortcuts;
    if ((bool) (UnityEngine.Object) MatchManager.Instance)
      MatchManager.Instance.ShowCombatKeyboardByConfig();
    this.keyboardShortcutsToggle.isOn = _keyShortcuts;
  }

  public void SetExtendedDescriptions(bool _extendedDescriptions)
  {
    SaveManager.SaveIntoPrefsBool("extendedDescriptionsNew", _extendedDescriptions);
    GameManager.Instance.ConfigExtendedDescriptions = _extendedDescriptions;
    PopupManager.Instance.RefreshKeyNotes();
  }

  public void SetFollowingTheLeader(bool _followingTheLeader)
  {
    SaveManager.SaveIntoPrefsBool("followLeader", _followingTheLeader);
    AtOManager.Instance.followingTheLeader = _followingTheLeader;
    if (!(bool) (UnityEngine.Object) HeroSelectionManager.Instance)
      return;
    HeroSelectionManager.Instance.ShowFollowStatus();
  }

  public void SetACBackgrounds(bool _acBackgrounds)
  {
    SaveManager.SaveIntoPrefsBool("acBackgrounds", _acBackgrounds);
    GameManager.Instance.ConfigACBackgrounds = _acBackgrounds;
    if (!((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null))
      return;
    MatchManager.Instance.RefreshStatusEffects();
  }

  public void ControllerMovement(bool goingUp = false, bool goingRight = false, bool goingDown = false, bool goingLeft = false)
  {
  }
}
