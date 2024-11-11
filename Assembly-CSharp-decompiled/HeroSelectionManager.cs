// Decompiled with JetBrains decompiler
// Type: HeroSelectionManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroSelectionManager : MonoBehaviour
{
  public Transform sceneCamera;
  private GameObject box;
  public GameObject[] boxGO;
  private BoxSelection[] boxSelection;
  public Dictionary<string, HeroSelection> heroSelectionDictionary = new Dictionary<string, HeroSelection>();
  private Dictionary<GameObject, bool> boxFilled = new Dictionary<GameObject, bool>();
  private Dictionary<GameObject, HeroSelection> boxHero = new Dictionary<GameObject, HeroSelection>();
  public GameObject heroSelectionPrefab;
  public GameObject cursorDragPrefab;
  public bool dragging;
  public Transform titleGroupDefault;
  public Transform titleWeeklyDefault;
  public Transform boxCharacters;
  public GameObject charPopupPrefab;
  public GameObject charPopupGO;
  public CharPopup charPopup;
  public Transform charContainerBg;
  public GameObject charContainerGO;
  public GameObject warriorsGO;
  public GameObject healersGO;
  public GameObject magesGO;
  public GameObject scoutsGO;
  public GameObject dlcsGO;
  public TMP_Text _ClassWarriors;
  public TMP_Text _ClassHealers;
  public TMP_Text _ClassMages;
  public TMP_Text _ClassScouts;
  public TMP_Text _ClassMagicKnights;
  public Transform masterDescription;
  public Transform gameSeed;
  public Transform gameSeedModify;
  public TMP_Text gameSeedTxt;
  public BotonGeneric botonBegin;
  public BotonGeneric botonFollow;
  public Transform popupMask;
  public Transform ngTransform;
  public BotonGeneric ngButton;
  public Transform ngSelectionX;
  public Transform ngLock;
  public Transform separator;
  public TMP_Text madnessLevel;
  public Transform madnessParticle;
  public Transform madnessButton;
  public Transform weeklyModifiersButton;
  public Transform sandboxButton;
  public Transform sandboxButtonCircleOn;
  public Transform sandboxButtonCircleOff;
  public Transform weeklyT;
  public TMP_Text weeklyNumber;
  public TMP_Text weeklyLeft;
  private bool setWeekly;
  private int ngValue;
  private int ngValueMaster;
  private string ngCorruptors = "";
  private int obeliskMadnessValue;
  private int obeliskMadnessValueMaster;
  private Dictionary<string, SubClassData[]> subclassDictionary = new Dictionary<string, SubClassData[]>();
  private Dictionary<string, SubClassData> nonHistorySubclassDictionary = new Dictionary<string, SubClassData>();
  private PhotonView photonView;
  private Dictionary<string, string> SubclassByName = new Dictionary<string, string>();
  private Dictionary<string, List<string>> playerHeroPerksDict;
  public Dictionary<string, string> playerHeroSkinsDict;
  public Dictionary<string, string> playerHeroCardbackDict;
  public Transform beginAdventureButton;
  public Transform readyButtonText;
  public Transform readyButton;
  private bool statusReady;
  private Coroutine manualReadyCo;
  public Transform weeklySelector;
  private int controllerCurrentOption = -1;
  private int controllerCurrentBlock;
  public List<Transform> menuController;
  public List<Transform> positionsController;
  public HeroSelection controllerCurrentHS;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();
  private List<Transform> _controllerVerticalList = new List<Transform>();

  public static HeroSelectionManager Instance { get; private set; }

  public string NgCorruptors
  {
    get => this.ngCorruptors;
    set => this.ngCorruptors = value;
  }

  public int NgValue
  {
    get => this.ngValue;
    set => this.ngValue = value;
  }

  public int NgValueMaster
  {
    get => this.ngValueMaster;
    set => this.ngValueMaster = value;
  }

  public int ObeliskMadnessValueMaster
  {
    get => this.obeliskMadnessValueMaster;
    set => this.obeliskMadnessValueMaster = value;
  }

  public int ObeliskMadnessValue
  {
    get => this.obeliskMadnessValue;
    set => this.obeliskMadnessValue = value;
  }

  public Dictionary<string, List<string>> PlayerHeroPerksDict
  {
    get => this.playerHeroPerksDict;
    set => this.playerHeroPerksDict = value;
  }

  private void Awake()
  {
    if ((UnityEngine.Object) GameManager.Instance == (UnityEngine.Object) null)
    {
      SceneStatic.LoadByName("MainMenu");
    }
    else
    {
      if ((UnityEngine.Object) HeroSelectionManager.Instance == (UnityEngine.Object) null)
        HeroSelectionManager.Instance = this;
      else if ((UnityEngine.Object) HeroSelectionManager.Instance != (UnityEngine.Object) this)
        UnityEngine.Object.Destroy((UnityEngine.Object) this);
      this.photonView = PhotonView.Get((Component) this);
      this.sceneCamera.gameObject.SetActive(false);
      NetworkManager.Instance.StartStopQueue(true);
    }
  }

  public void ForceWeekly(string _weekly)
  {
    AtOManager.Instance.weeklyForcedId = _weekly;
    if (GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
      this.photonView.RPC("NET_ForceWeekly", RpcTarget.Others, (object) _weekly);
    SceneStatic.LoadByName("HeroSelection");
  }

  [PunRPC]
  private void NET_ForceWeekly(string _weekly) => this.ForceWeekly(_weekly);

  private void Start() => this.StartCoroutine(this.StartCo());

  private void Update()
  {
    if (!this.setWeekly || Time.frameCount % 24 != 0)
      return;
    this.SetWeeklyLeft();
  }

  public void IncreaseHeroProgressDev(int _slot)
  {
    if (PerkTree.Instance.IsActive() || !((UnityEngine.Object) this.boxHero[this.boxGO[_slot]] != (UnityEngine.Object) null))
      return;
    PlayerManager.Instance.ModifyProgress(this.boxHero[this.boxGO[_slot]].Id, 5000, _slot);
    PlayerManager.Instance.ModifyPlayerRankProgress(5000);
    this.RefreshPerkPoints(this.boxHero[this.boxGO[_slot]].Id);
    SaveManager.SavePlayerData();
  }

  public void IncreaseHeroProgressSupplies(string _scdId)
  {
    int _quantity = 400;
    PlayerManager.Instance.ModifyProgress(_scdId, _quantity);
    PlayerManager.Instance.ModifyPlayerRankProgress(_quantity);
    this.RefreshPerkPoints(_scdId);
    SaveManager.SavePlayerData();
  }

  private IEnumerator StartCo()
  {
    HeroSelectionManager selectionManager = this;
    selectionManager.ngValueMaster = selectionManager.ngValue = 0;
    selectionManager.ngCorruptors = "";
    selectionManager.obeliskMadnessValue = selectionManager.obeliskMadnessValueMaster = 0;
    selectionManager.madnessLevel.text = string.Format(Texts.Instance.GetText("madnessNumber"), (object) 0);
    if (GameManager.Instance.IsMultiplayer())
    {
      if (NetworkManager.Instance.IsMaster())
      {
        NetworkManager.Instance.PlayerSkuList.Clear();
        while (!NetworkManager.Instance.AllPlayersReady("heroSelection"))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("Game ready, Everybody checked heroSelection");
        if (GameManager.Instance.IsLoadingGame())
          selectionManager.photonView.RPC("NET_SetLoadingGame", RpcTarget.Others);
        NetworkManager.Instance.PlayersNetworkContinue("heroSelection", AtOManager.Instance.GetWeekly().ToString());
        yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      }
      else
      {
        GameManager.Instance.SetGameStatus(Enums.GameStatus.NewGame);
        NetworkManager.Instance.SetWaitingSyncro("heroSelection", true);
        NetworkManager.Instance.SetStatusReady("heroSelection");
        while (NetworkManager.Instance.WaitingSyncro["heroSelection"])
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (NetworkManager.Instance.netAuxValue != "")
          AtOManager.Instance.SetWeekly(int.Parse(NetworkManager.Instance.netAuxValue));
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("heroSelection, we can continue!");
      }
    }
    selectionManager.StartCoroutine(selectionManager.StartCoContinue());
  }

  private IEnumerator StartCoContinue()
  {
    if (GameManager.Instance.IsMultiplayer())
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < Globals.Instance.SkuAvailable.Count; ++index)
      {
        if (SteamManager.Instance.PlayerHaveDLC(Globals.Instance.SkuAvailable[index]))
          stringList.Add(Globals.Instance.SkuAvailable[index]);
      }
      string str = "";
      if (stringList.Count > 0)
        str = JsonHelper.ToJson<string>(stringList.ToArray());
      if (NetworkManager.Instance.IsMaster())
      {
        this.photonView.RPC("NET_SetSku", RpcTarget.All, (object) NetworkManager.Instance.GetPlayerNick(), (object) str);
      }
      else
      {
        string roomName = NetworkManager.Instance.GetRoomName();
        if (roomName != "")
        {
          SaveManager.SaveIntoPrefsString("coopRoomId", roomName);
          SaveManager.SavePrefs();
        }
        NetworkManager.Instance.SetWaitingSyncro("skuWait", true);
        this.photonView.RPC("NET_SetSku", RpcTarget.All, (object) NetworkManager.Instance.GetPlayerNick(), (object) str);
      }
      Debug.Log((object) "WaitingSyncro skuWait");
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersHaveSkuList())
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("Game ready, Everybody checked skuWait");
        NetworkManager.Instance.PlayersNetworkContinue("skuWait");
        yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      }
      else
      {
        while (NetworkManager.Instance.WaitingSyncro["skuWait"])
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("skuWait, we can continue!");
      }
    }
    MadnessManager.Instance.ShowMadness();
    MadnessManager.Instance.RefreshValues();
    MadnessManager.Instance.ShowMadness();
    this.playerHeroSkinsDict = new Dictionary<string, string>();
    this.playerHeroCardbackDict = new Dictionary<string, string>();
    this.boxSelection = new BoxSelection[this.boxGO.Length];
    for (int index = 0; index < this.boxGO.Length; ++index)
    {
      this.boxHero[this.boxGO[index]] = (HeroSelection) null;
      this.boxFilled[this.boxGO[index]] = false;
      this.boxSelection[index] = this.boxGO[index].GetComponent<BoxSelection>();
    }
    this.ShowDrag(false, Vector3.zero);
    foreach (KeyValuePair<string, SubClassData> keyValuePair in Globals.Instance.SubClass)
    {
      if (!keyValuePair.Value.MainCharacter)
      {
        if (!this.nonHistorySubclassDictionary.ContainsKey(keyValuePair.Key))
          this.nonHistorySubclassDictionary.Add(keyValuePair.Key, Globals.Instance.SubClass[keyValuePair.Key]);
      }
      else if (keyValuePair.Value.ExpansionCharacter)
      {
        string key = "dlc";
        if (!this.subclassDictionary.ContainsKey(key))
          this.subclassDictionary.Add(key, new SubClassData[4]);
        this.subclassDictionary[key][Globals.Instance.SubClass[keyValuePair.Key].OrderInList] = Globals.Instance.SubClass[keyValuePair.Key];
      }
      else
      {
        string key = Enum.GetName(typeof (Enums.HeroClass), (object) Globals.Instance.SubClass[keyValuePair.Key].HeroClass).ToLower().Replace(" ", "");
        if (!this.subclassDictionary.ContainsKey(key))
          this.subclassDictionary.Add(key, new SubClassData[4]);
        this.subclassDictionary[key][Globals.Instance.SubClass[keyValuePair.Key].OrderInList] = Globals.Instance.SubClass[keyValuePair.Key];
      }
    }
    this._ClassWarriors.color = Functions.HexToColor(Globals.Instance.ClassColor["warrior"]);
    this._ClassHealers.color = Functions.HexToColor(Globals.Instance.ClassColor["healer"]);
    this._ClassMages.color = Functions.HexToColor(Globals.Instance.ClassColor["mage"]);
    this._ClassScouts.color = Functions.HexToColor(Globals.Instance.ClassColor["scout"]);
    this._ClassMagicKnights.color = Functions.HexToColor(Globals.Instance.ClassColor["magicknight"]);
    int num1 = 5;
    for (int index1 = 0; index1 < num1; ++index1)
    {
      for (int index2 = 0; index2 < 4; ++index2)
      {
        SubClassData _subclassdata = (SubClassData) null;
        GameObject gameObject1 = (GameObject) null;
        switch (index1)
        {
          case 0:
            _subclassdata = this.subclassDictionary["warrior"][index2];
            gameObject1 = this.warriorsGO;
            break;
          case 1:
            _subclassdata = this.subclassDictionary["scout"][index2];
            gameObject1 = this.scoutsGO;
            break;
          case 2:
            _subclassdata = this.subclassDictionary["mage"][index2];
            gameObject1 = this.magesGO;
            break;
          case 3:
            _subclassdata = this.subclassDictionary["healer"][index2];
            gameObject1 = this.healersGO;
            break;
          case 4:
            if (this.subclassDictionary.ContainsKey("dlc"))
            {
              _subclassdata = this.subclassDictionary["dlc"][index2];
              gameObject1 = this.dlcsGO;
              break;
            }
            break;
        }
        if (!((UnityEngine.Object) _subclassdata == (UnityEngine.Object) null))
        {
          GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.heroSelectionPrefab, Vector3.zero, Quaternion.identity, gameObject1.transform);
          gameObject2.transform.localPosition = new Vector3((float) (0.550000011920929 + 1.8500000238418579 * (double) index2), -0.65f, 0.0f);
          gameObject2.name = _subclassdata.SubClassName.ToLower();
          HeroSelection component = gameObject2.transform.Find("Portrait").transform.GetComponent<HeroSelection>();
          this.heroSelectionDictionary.Add(gameObject2.name, component);
          component.blocked = !PlayerManager.Instance.IsHeroUnlocked(_subclassdata.Id);
          if (component.blocked && GameManager.Instance.IsObeliskChallenge() && !GameManager.Instance.IsWeeklyChallenge())
            component.blocked = false;
          if (_subclassdata.Id == "mercenary" || _subclassdata.Id == "ranger" || _subclassdata.Id == "elementalist" || _subclassdata.Id == "cleric")
            component.blocked = false;
          if (component.blocked && GameManager.Instance.IsWeeklyChallenge())
          {
            ChallengeData weeklyData = Globals.Instance.GetWeeklyData(Functions.GetCurrentWeeklyWeek());
            if ((UnityEngine.Object) weeklyData != (UnityEngine.Object) null && (_subclassdata.Id == weeklyData.Hero1.Id || _subclassdata.Id == weeklyData.Hero2.Id || _subclassdata.Id == weeklyData.Hero3.Id || _subclassdata.Id == weeklyData.Hero4.Id))
              component.blocked = false;
          }
          component.SetSubclass(_subclassdata);
          component.SetSprite(_subclassdata.SpriteSpeed, _subclassdata.SpriteBorderSmall, _subclassdata.SpriteBorderLocked);
          string activeSkin = PlayerManager.Instance.GetActiveSkin(_subclassdata.Id);
          if (activeSkin != "")
          {
            SkinData skinData = Globals.Instance.GetSkinData(activeSkin);
            this.AddToPlayerHeroSkin(_subclassdata.Id, activeSkin);
            component.SetSprite(skinData.SpritePortrait, skinData.SpriteSilueta, _subclassdata.SpriteBorderLocked);
          }
          component.SetName(_subclassdata.CharacterName);
          component.Init();
          if ((UnityEngine.Object) _subclassdata.SpriteBorderLocked != (UnityEngine.Object) null && _subclassdata.SpriteBorderLocked.name == "regularBorderSmall")
            component.ShowComingSoon();
          this.SubclassByName.Add(_subclassdata.Id, _subclassdata.SubClassName);
          if (GameManager.Instance.IsWeeklyChallenge())
            component.blocked = true;
          this.menuController.Add(component.transform);
        }
      }
    }
    foreach (KeyValuePair<string, SubClassData> nonHistorySubclass in this.nonHistorySubclassDictionary)
    {
      SubClassData _subclassdata = nonHistorySubclass.Value;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.heroSelectionPrefab, Vector3.zero, Quaternion.identity);
      gameObject.transform.localPosition = new Vector3(-10f, -10f, 100f);
      gameObject.name = _subclassdata.SubClassName.ToLower();
      HeroSelection component = gameObject.transform.Find("Portrait").transform.GetComponent<HeroSelection>();
      this.heroSelectionDictionary.Add(gameObject.name, component);
      component.blocked = true;
      component.SetSubclass(_subclassdata);
      component.SetSprite(_subclassdata.SpriteSpeed, _subclassdata.SpriteBorderSmall, _subclassdata.SpriteBorderLocked);
      component.SetName(_subclassdata.CharacterName);
      component.Init();
      this.SubclassByName.Add(_subclassdata.Id, _subclassdata.SubClassName);
    }
    if (!GameManager.Instance.IsObeliskChallenge() && AtOManager.Instance.IsFirstGame() && !GameManager.Instance.IsMultiplayer())
    {
      AtOManager.Instance.SetGameId("cban29t");
      this.heroSelectionDictionary["mercenary"].AssignHeroToBox(this.boxGO[0]);
      this.heroSelectionDictionary["ranger"].AssignHeroToBox(this.boxGO[1]);
      this.heroSelectionDictionary["elementalist"].AssignHeroToBox(this.boxGO[2]);
      this.heroSelectionDictionary["cleric"].AssignHeroToBox(this.boxGO[3]);
      SandboxManager.Instance.DisableSandbox();
      yield return (object) Globals.Instance.WaitForSeconds(1f);
      this.BeginAdventure();
    }
    else
    {
      this.charPopupGO = UnityEngine.Object.Instantiate<GameObject>(this.charPopupPrefab, new Vector3(0.0f, 0.0f, -1f), Quaternion.identity);
      this.charPopup = this.charPopupGO.GetComponent<CharPopup>();
      this.charPopup.HideNow();
      this.separator.gameObject.SetActive(true);
      if (!GameManager.Instance.IsWeeklyChallenge())
      {
        this.titleGroupDefault.gameObject.SetActive(true);
        this.titleWeeklyDefault.gameObject.SetActive(false);
        this.weeklyModifiersButton.gameObject.SetActive(false);
        this.weeklyT.gameObject.SetActive(false);
      }
      else
      {
        this.titleGroupDefault.gameObject.SetActive(false);
        this.titleWeeklyDefault.gameObject.SetActive(true);
        this.weeklyModifiersButton.gameObject.SetActive(true);
        this.weeklyT.gameObject.SetActive(true);
        this.setWeekly = true;
        if (!GameManager.Instance.IsLoadingGame())
          AtOManager.Instance.SetWeekly(Functions.GetCurrentWeeklyWeek());
        this.weeklyNumber.text = AtOManager.Instance.GetWeeklyName(AtOManager.Instance.GetWeekly());
      }
      if (!GameManager.Instance.IsObeliskChallenge())
      {
        this.madnessButton.gameObject.SetActive(true);
        if (GameManager.Instance.IsMultiplayer())
        {
          if (NetworkManager.Instance.IsMaster())
          {
            if (GameManager.Instance.IsLoadingGame())
            {
              this.ngValueMaster = this.ngValue = AtOManager.Instance.GetNgPlus();
              this.ngCorruptors = AtOManager.Instance.GetMadnessCorruptors();
              this.SetMadnessLevel();
            }
            else if (SaveManager.PrefsHasKey("madnessLevelCoop") && SaveManager.PrefsHasKey("madnessCorruptorsCoop"))
            {
              int num2 = SaveManager.LoadPrefsInt("madnessLevelCoop");
              string str = SaveManager.LoadPrefsString("madnessCorruptorsCoop");
              if (PlayerManager.Instance.NgLevel >= num2)
              {
                this.ngValueMaster = this.ngValue = num2;
                if (str != "")
                  this.ngCorruptors = str;
              }
              else
              {
                this.ngValueMaster = this.ngValue = 0;
                this.ngCorruptors = "";
              }
              this.SetMadnessLevel();
            }
          }
        }
        else if (SaveManager.PrefsHasKey("madnessLevel") && SaveManager.PrefsHasKey("madnessCorruptors"))
        {
          int num3 = SaveManager.LoadPrefsInt("madnessLevel");
          string str = SaveManager.LoadPrefsString("madnessCorruptors");
          if (PlayerManager.Instance.NgLevel >= num3)
          {
            this.ngValueMaster = this.ngValue = num3;
            if (str != "")
              this.ngCorruptors = str;
          }
          else
          {
            this.ngValueMaster = this.ngValue = 0;
            this.ngCorruptors = "";
          }
          this.SetMadnessLevel();
        }
      }
      else if (!GameManager.Instance.IsWeeklyChallenge())
      {
        this.madnessButton.gameObject.SetActive(true);
        if (GameManager.Instance.IsMultiplayer())
        {
          if (NetworkManager.Instance.IsMaster())
          {
            if (GameManager.Instance.IsLoadingGame())
            {
              this.obeliskMadnessValue = this.obeliskMadnessValueMaster = AtOManager.Instance.GetObeliskMadness();
              this.SetObeliskMadnessLevel();
            }
            else if (SaveManager.PrefsHasKey("obeliskMadnessCoop"))
            {
              int num4 = SaveManager.LoadPrefsInt("obeliskMadnessCoop");
              this.obeliskMadnessValue = PlayerManager.Instance.ObeliskMadnessLevel < num4 ? (this.obeliskMadnessValueMaster = 0) : (this.obeliskMadnessValueMaster = num4);
              this.SetObeliskMadnessLevel();
            }
          }
        }
        else if (SaveManager.PrefsHasKey("obeliskMadness"))
        {
          int num5 = SaveManager.LoadPrefsInt("obeliskMadness");
          this.obeliskMadnessValue = PlayerManager.Instance.ObeliskMadnessLevel < num5 ? (this.obeliskMadnessValueMaster = 0) : (this.obeliskMadnessValueMaster = num5);
          this.SetObeliskMadnessLevel();
        }
      }
      else
        this.madnessButton.gameObject.SetActive(false);
      this.Resize();
      if (GameManager.Instance.IsWeeklyChallenge() && !GameManager.Instance.IsLoadingGame())
      {
        this.gameSeedModify.gameObject.SetActive(false);
        ChallengeData weeklyData = Globals.Instance.GetWeeklyData(Functions.GetCurrentWeeklyWeek());
        if ((UnityEngine.Object) weeklyData != (UnityEngine.Object) null)
        {
          this.heroSelectionDictionary[weeklyData.Hero1.Id].AssignHeroToBox(this.boxGO[0]);
          this.heroSelectionDictionary[weeklyData.Hero2.Id].AssignHeroToBox(this.boxGO[1]);
          this.heroSelectionDictionary[weeklyData.Hero3.Id].AssignHeroToBox(this.boxGO[2]);
          this.heroSelectionDictionary[weeklyData.Hero4.Id].AssignHeroToBox(this.boxGO[3]);
        }
        if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
        {
          if ((UnityEngine.Object) weeklyData != (UnityEngine.Object) null)
            AtOManager.Instance.SetGameId(weeklyData.Seed);
          else
            AtOManager.Instance.SetGameId();
        }
        GameManager.Instance.SceneLoaded();
      }
      else if (GameManager.Instance.IsLoadingGame() || AtOManager.Instance.IsFirstGame() && !GameManager.Instance.IsMultiplayer() && !GameManager.Instance.IsObeliskChallenge())
      {
        this.gameSeedModify.gameObject.SetActive(false);
        if (AtOManager.Instance.IsFirstGame())
          AtOManager.Instance.SetGameId("cban29t");
      }
      else
      {
        if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
          AtOManager.Instance.SetGameId();
        this.gameSeed.gameObject.SetActive(true);
      }
      if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
        this.SetSeed(AtOManager.Instance.GetGameId());
      if (GameManager.Instance.IsWeeklyChallenge() || GameManager.Instance.IsObeliskChallenge() && this.obeliskMadnessValue > 7)
        this.gameSeed.gameObject.SetActive(false);
      this.playerHeroPerksDict = new Dictionary<string, List<string>>();
      if (GameManager.Instance.IsMultiplayer())
      {
        this.masterDescription.gameObject.SetActive(true);
        if (NetworkManager.Instance.IsMaster())
        {
          this.DrawBoxSelectionNames();
          this.botonBegin.gameObject.SetActive(true);
          this.botonBegin.Disable();
          this.botonFollow.transform.parent.gameObject.SetActive(false);
        }
        else
        {
          this.gameSeedModify.gameObject.SetActive(false);
          this.botonBegin.gameObject.SetActive(false);
          this.botonFollow.transform.parent.gameObject.SetActive(true);
          this.ShowFollowStatus();
        }
        if (NetworkManager.Instance.IsMaster() && GameManager.Instance.IsLoadingGame())
        {
          for (int index = 0; index < 4; ++index)
          {
            Hero hero = AtOManager.Instance.GetHero(index);
            if (hero != null && !((UnityEngine.Object) hero.HeroData == (UnityEngine.Object) null))
            {
              string subclassName = hero.SubclassName;
              int perkRank = hero.PerkRank;
              string skinUsed = hero.SkinUsed;
              string cardbackUsed = hero.CardbackUsed;
              this.AddToPlayerHeroSkin(subclassName, skinUsed);
              this.AddToPlayerHeroCardback(subclassName, cardbackUsed);
              if (this.heroSelectionDictionary.ContainsKey(subclassName))
              {
                this.heroSelectionDictionary[subclassName].AssignHeroToBox(this.boxGO[index]);
                if (hero.HeroData.HeroSubClass.MainCharacter)
                {
                  this.heroSelectionDictionary[subclassName].SetRankBox(perkRank);
                  this.heroSelectionDictionary[subclassName].SetSkin(skinUsed);
                }
              }
              this.photonView.RPC("NET_AssignHeroToBox", RpcTarget.Others, (object) hero.SubclassName.ToLower(), (object) index, (object) perkRank, (object) skinUsed, (object) cardbackUsed);
            }
          }
        }
      }
      else
      {
        this.masterDescription.gameObject.SetActive(false);
        this.botonFollow.transform.parent.gameObject.SetActive(false);
        this.botonBegin.gameObject.SetActive(true);
        this.botonBegin.Disable();
        if (!GameManager.Instance.IsWeeklyChallenge())
          this.PreAssign();
      }
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      this.RefreshSandboxButton();
      if (!GameManager.Instance.IsObeliskChallenge())
      {
        this.sandboxButton.gameObject.SetActive(true);
        this.madnessButton.localPosition = new Vector3(2.43f, this.madnessButton.localPosition.y, this.madnessButton.localPosition.z);
        this.sandboxButton.localPosition = new Vector3(5.23f, this.sandboxButton.localPosition.y, this.sandboxButton.localPosition.z);
        if (!GameManager.Instance.IsMultiplayer() || GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
        {
          string sandboxMods;
          if (GameManager.Instance.GameStatus != Enums.GameStatus.LoadGame)
          {
            if ((!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster()) && PlayerManager.Instance.NgLevel == 0)
            {
              SandboxManager.Instance.DisableSandbox();
              AtOManager.Instance.ClearSandbox();
            }
            else
              AtOManager.Instance.SetSandboxMods(SaveManager.LoadPrefsString("sandboxSettings"));
            SandboxManager.Instance.LoadValuesFromAtOManager();
            SandboxManager.Instance.AdjustTotalHeroesBoxToCoop();
            SandboxManager.Instance.SaveValuesToAtOManager();
            sandboxMods = AtOManager.Instance.GetSandboxMods();
          }
          else
          {
            sandboxMods = AtOManager.Instance.GetSandboxMods();
            SandboxManager.Instance.LoadValuesFromAtOManager();
          }
          if (GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
            this.photonView.RPC("NET_ShareSandbox", RpcTarget.Others, (object) Functions.CompressString(sandboxMods));
          this.RefreshCharBoxesBySandboxHeroes();
        }
      }
      else
      {
        this.sandboxButton.gameObject.SetActive(false);
        this.madnessButton.localPosition = new Vector3(3.8f, this.madnessButton.localPosition.y, this.madnessButton.localPosition.z);
        SandboxManager.Instance.DisableSandbox();
      }
      this.readyButtonText.gameObject.SetActive(false);
      this.readyButton.gameObject.SetActive(false);
      if (GameManager.Instance.IsMultiplayer())
      {
        if (NetworkManager.Instance.IsMaster())
        {
          NetworkManager.Instance.ClearAllPlayerManualReady();
          NetworkManager.Instance.SetManualReady(true);
        }
        else
        {
          this.readyButtonText.gameObject.SetActive(true);
          this.readyButton.gameObject.SetActive(true);
        }
      }
      GameManager.Instance.SceneLoaded();
      if (!GameManager.Instance.TutorialWatched("characterPerks"))
      {
        foreach (KeyValuePair<string, HeroSelection> heroSelection in this.heroSelectionDictionary)
        {
          if (heroSelection.Value.perkPointsT.gameObject.activeSelf)
          {
            GameManager.Instance.ShowTutorialPopup("characterPerks", heroSelection.Value.perkPointsT.gameObject.transform.position, Vector3.zero);
            break;
          }
        }
      }
      if (GameManager.Instance.IsMultiplayer() && GameManager.Instance.IsLoadingGame() && NetworkManager.Instance.IsMaster())
      {
        bool flag = true;
        List<string> stringList1 = new List<string>();
        List<string> stringList2 = new List<string>();
        for (int index = 0; index < 4; ++index)
        {
          Hero hero = AtOManager.Instance.GetHero(index);
          if (hero != null && !((UnityEngine.Object) hero.HeroData == (UnityEngine.Object) null))
          {
            if (hero.OwnerOriginal != null)
            {
              string lower = hero.OwnerOriginal.ToLower();
              if (!stringList1.Contains(lower))
                stringList1.Add(lower);
            }
            else
              break;
          }
        }
        foreach (Player player in NetworkManager.Instance.PlayerList)
        {
          string lower = NetworkManager.Instance.GetPlayerNickReal(player.NickName).ToLower();
          if (!stringList2.Contains(lower))
            stringList2.Add(lower);
        }
        if (stringList1.Count != stringList2.Count)
        {
          flag = false;
        }
        else
        {
          for (int index = 0; index < stringList2.Count; ++index)
          {
            if (!stringList1.Contains(stringList2[index]))
            {
              flag = false;
              break;
            }
          }
        }
        if (!flag)
          this.photonView.RPC("NET_SetNotOriginal", RpcTarget.All);
      }
    }
  }

  [PunRPC]
  private void NET_SetSku(string _nick, string _playerSkuList)
  {
    List<string> _list = new List<string>();
    if (_playerSkuList != "")
      _list = ((IEnumerable<string>) JsonHelper.FromJson<string>(_playerSkuList)).ToList<string>();
    if (GameManager.Instance.GetDeveloperMode())
      Debug.Log((object) ("Got list from " + _nick + " --> " + _playerSkuList));
    NetworkManager.Instance.AddPlayerSkuList(_nick, _list);
  }

  [PunRPC]
  private void NET_SetNotOriginal() => this.masterDescription.GetComponent<TMP_Text>().text = Texts.Instance.GetText("notOriginalPlayers");

  private void SetWeeklyLeft()
  {
    if (AtOManager.Instance.weeklyForcedId == "")
    {
      TimeSpan timeSpan = Functions.TimeSpanLeftWeekly();
      this.weeklyLeft.text = string.Format("{0:D2}h. {1:D2}m. {2:D2}s.", (object) (int) timeSpan.TotalHours, (object) timeSpan.Minutes, (object) timeSpan.Seconds);
    }
    else
      this.weeklyLeft.text = "-- [test] --";
  }

  [PunRPC]
  private void NET_SetLoadingGame() => GameManager.Instance.SetGameStatus(Enums.GameStatus.LoadGame);

  public void AssignHeroCardback(string _subclass, string _cardbackId)
  {
    if (GameManager.Instance.IsMultiplayer())
      this.SendHeroCardbackMP(_subclass, _cardbackId);
    else
      this.AddToPlayerHeroCardback(_subclass, _cardbackId);
  }

  public void SendHeroCardbackMP(string _subclass, string _cardbackId)
  {
    Debug.Log((object) ("SendHeroCardback for " + _subclass + " => " + _cardbackId));
    if (!GameManager.Instance.IsMultiplayer())
      return;
    this.photonView.RPC("NET_SetHeroCardback", RpcTarget.All, (object) NetworkManager.Instance.GetPlayerNick(), (object) _subclass, (object) _cardbackId);
  }

  [PunRPC]
  private void NET_SetHeroCardback(string _nick, string _subclass, string _cardbackId)
  {
    Debug.Log((object) ("NET_SetHeroCardback " + _nick + " " + _subclass + " " + _cardbackId));
    this.SetHeroCardbackToBoxOwner(_nick, _subclass, _cardbackId);
  }

  private void SetHeroCardbackToBoxOwner(string _nick, string _subclass, string _cardbackId)
  {
    for (int index = 0; index < this.boxHero.Count; ++index)
    {
      if (this.boxSelection[index].GetOwner() == _nick && (UnityEngine.Object) this.boxHero[this.boxGO[index]] != (UnityEngine.Object) null && this.boxHero[this.boxGO[index]].GetSubclassName().ToLower() == _subclass.ToLower())
        this.AddToPlayerHeroCardback(_subclass, _cardbackId);
    }
  }

  private void AddToPlayerHeroCardback(string _subclass, string _cardbackId)
  {
    string lower = _subclass.ToLower();
    if (!this.playerHeroCardbackDict.ContainsKey(lower))
      this.playerHeroCardbackDict.Add(lower, _cardbackId);
    else
      this.playerHeroCardbackDict[lower] = _cardbackId;
  }

  public void SendHeroSkinMP(string _subclass, string _skinId)
  {
    Debug.Log((object) ("SendHeroSkinMP for " + _subclass + " => " + _skinId));
    if (!GameManager.Instance.IsMultiplayer())
      return;
    this.photonView.RPC("NET_SetHeroSkin", RpcTarget.All, (object) NetworkManager.Instance.GetPlayerNick(), (object) _subclass, (object) _skinId);
  }

  [PunRPC]
  private void NET_SetHeroSkin(string _nick, string _subclass, string _skinId)
  {
    Debug.Log((object) ("NET_SetHeroSkin " + _nick + " " + _subclass + " " + _skinId));
    this.SetHeroSkinToBoxOwner(_nick, _subclass, _skinId);
  }

  private void AddToPlayerHeroSkin(string _subclass, string _skinId)
  {
    string lower = _subclass.ToLower();
    if (!this.playerHeroSkinsDict.ContainsKey(lower))
      this.playerHeroSkinsDict.Add(lower, _skinId);
    else
      this.playerHeroSkinsDict[lower] = _skinId;
  }

  private void SetHeroSkinToBoxOwner(string _nick, string _subclass, string _skinId)
  {
    for (int index = 0; index < this.boxHero.Count; ++index)
    {
      if (this.boxSelection[index].GetOwner() == _nick && (UnityEngine.Object) this.boxHero[this.boxGO[index]] != (UnityEngine.Object) null && this.boxHero[this.boxGO[index]].GetSubclassName().ToLower() == _subclass.ToLower())
      {
        this.boxHero[this.boxGO[index]].SetSkin(_skinId);
        this.AddToPlayerHeroSkin(_subclass, _skinId);
      }
    }
  }

  public void SendHeroPerksMP(string _hero)
  {
    if (!GameManager.Instance.IsMultiplayer())
      return;
    List<string> heroPerks = PlayerManager.Instance.GetHeroPerks(_hero);
    if (heroPerks != null)
    {
      string json = JsonHelper.ToJson<string>(heroPerks.ToArray());
      this.photonView.RPC("NET_SetHeroPerk", RpcTarget.All, (object) NetworkManager.Instance.GetPlayerNick(), (object) _hero, (object) json);
    }
    else
      this.photonView.RPC("NET_SetHeroPerk", RpcTarget.All, (object) NetworkManager.Instance.GetPlayerNick(), (object) _hero, (object) "");
  }

  [PunRPC]
  private void NET_SetHeroPerk(string _nick, string _hero, string _perkListStr)
  {
    List<string> stringList = new List<string>();
    if (_perkListStr != "")
      stringList = ((IEnumerable<string>) JsonHelper.FromJson<string>(_perkListStr)).ToList<string>();
    string lower = (_nick + "_" + _hero).ToLower();
    if (!this.playerHeroPerksDict.ContainsKey(lower))
      this.playerHeroPerksDict.Add(lower, stringList);
    else
      this.playerHeroPerksDict[lower] = stringList;
    if (!PerkTree.Instance.IsActive())
      return;
    PerkTree.Instance.DoTeamPerks();
  }

  private void PreAssign()
  {
    if (PlayerManager.Instance.LastUsedTeam == null || PlayerManager.Instance.LastUsedTeam.Length != 4)
      return;
    for (int index = 0; index < 4; ++index)
    {
      if (this.heroSelectionDictionary.ContainsKey(PlayerManager.Instance.LastUsedTeam[index]) && (GameManager.Instance.IsObeliskChallenge() || PlayerManager.Instance.IsHeroUnlocked(PlayerManager.Instance.LastUsedTeam[index])))
        this.heroSelectionDictionary[PlayerManager.Instance.LastUsedTeam[index]].AssignHeroToBox(this.boxGO[index]);
    }
  }

  private void AlfaPreAssign()
  {
    if (GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
    {
      int index = 0;
      for (int boxId = 0; boxId < 4; ++boxId)
      {
        this.AssignPlayerToBox(NetworkManager.Instance.PlayerList[index].NickName, boxId);
        ++index;
        if (index >= NetworkManager.Instance.PlayerList.Length)
          index = 0;
      }
    }
    this.heroSelectionDictionary["mercenary"].AssignHeroToBox(this.boxGO[0]);
    this.heroSelectionDictionary["ranger"].AssignHeroToBox(this.boxGO[1]);
    this.heroSelectionDictionary["elementalist"].AssignHeroToBox(this.boxGO[2]);
    this.heroSelectionDictionary["cleric"].AssignHeroToBox(this.boxGO[3]);
  }

  private void DrawBoxSelectionNames()
  {
    int num = 0;
    foreach (Player player in NetworkManager.Instance.PlayerList)
    {
      for (int index = 0; index < 4; ++index)
      {
        this.boxSelection[index].ShowPlayer(num);
        this.boxSelection[index].SetPlayerPosition(num, player.NickName);
      }
      ++num;
    }
    for (int position = num; position < 4; ++position)
    {
      for (int index = 0; index < 4; ++index)
        this.boxSelection[index].SetPlayerPosition(position, "");
    }
    foreach (Player player in NetworkManager.Instance.PlayerList)
    {
      string playerNickReal = NetworkManager.Instance.GetPlayerNickReal(player.NickName);
      if (playerNickReal == NetworkManager.Instance.Owner0)
        this.AssignPlayerToBox(player.NickName, 0);
      if (playerNickReal == NetworkManager.Instance.Owner1)
        this.AssignPlayerToBox(player.NickName, 1);
      if (playerNickReal == NetworkManager.Instance.Owner2)
        this.AssignPlayerToBox(player.NickName, 2);
      if (playerNickReal == NetworkManager.Instance.Owner3)
        this.AssignPlayerToBox(player.NickName, 3);
    }
  }

  public void AssignPlayerToBox(string playerNick, int boxId)
  {
    if (!GameManager.Instance.IsWeeklyChallenge() && !GameManager.Instance.IsLoadingGame())
      this.ClearBox(boxId);
    this.boxSelection[boxId].SetOwner(playerNick);
    if (GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
    {
      this.photonView.RPC("NET_AssignPlayerToBox", RpcTarget.Others, (object) playerNick, (object) boxId);
      NetworkManager.Instance.AssignHeroPlayerPositionOwner(boxId, playerNick);
    }
    if (GameManager.Instance.IsWeeklyChallenge() && playerNick == NetworkManager.Instance.GetPlayerNick())
    {
      ChallengeData weeklyData = Globals.Instance.GetWeeklyData(AtOManager.Instance.GetWeekly());
      if ((UnityEngine.Object) weeklyData != (UnityEngine.Object) null)
      {
        string id;
        switch (boxId)
        {
          case 0:
            this.AssignHeroToBox(weeklyData.Hero1.Id, 0);
            id = weeklyData.Hero1.Id;
            break;
          case 1:
            this.AssignHeroToBox(weeklyData.Hero2.Id, 1);
            id = weeklyData.Hero2.Id;
            break;
          case 2:
            this.AssignHeroToBox(weeklyData.Hero3.Id, 2);
            id = weeklyData.Hero3.Id;
            break;
          default:
            this.AssignHeroToBox(weeklyData.Hero4.Id, 3);
            id = weeklyData.Hero4.Id;
            break;
        }
        if (!GameManager.Instance.IsLoadingGame())
        {
          string activeSkin = PlayerManager.Instance.GetActiveSkin(Globals.Instance.GetSubClassData(id).Id);
          this.heroSelectionDictionary[id].SetSkin(activeSkin);
        }
      }
    }
    this.CheckButtonEnabled();
    this.SetPlayersReady();
  }

  [PunRPC]
  private void NET_AssignPlayerToBox(string playerNick, int boxId) => this.AssignPlayerToBox(playerNick, boxId);

  public void ResetHero(string _heroId) => this.photonView.RPC("NET_ResetHero", RpcTarget.Others, (object) _heroId);

  [PunRPC]
  private void NET_ResetHero(string _heroId)
  {
    for (int id = 0; id < this.boxHero.Count; ++id)
    {
      if ((UnityEngine.Object) this.boxHero[this.boxGO[id]] != (UnityEngine.Object) null && this.boxHero[this.boxGO[id]].Id == _heroId)
      {
        this.ClearBox(id);
        break;
      }
    }
  }

  public bool IsYourBox(string boxName) => !GameManager.Instance.IsMultiplayer() || this.boxSelection[int.Parse(boxName.Split('_', StringSplitOptions.None)[1])].GetOwner() == NetworkManager.Instance.GetPlayerNick();

  public void PopupTrait(string traitId) => PopupManager.Instance.SetTrait(Globals.Instance.GetTraitData(traitId));

  public void Resize()
  {
    float x = (float) ((double) Globals.Instance.sizeW * -0.5 + 0.85000002384185791);
    float y = (float) ((double) Globals.Instance.sizeH * 0.5 - 0.85000002384185791);
    float num = (float) (1.9500000476837158 * ((double) Globals.Instance.sizeH / 10.800000190734863));
    this.charContainerGO.transform.localScale = Globals.Instance.scaleV;
    this.warriorsGO.transform.position = new Vector3(x, y, 1f);
    this.scoutsGO.transform.position = new Vector3(x, y - num, 1f);
    this.magesGO.transform.position = new Vector3(x, y - num * 2f, 1f);
    this.healersGO.transform.position = new Vector3(x, y - num * 3f, 1f);
    this.dlcsGO.transform.position = new Vector3(x, y - num * 4f, 1f);
    this.charPopup.RepositionResolution();
  }

  public void ShowMask(bool state)
  {
  }

  private IEnumerator ShowMaskCo(bool state)
  {
    float maxAlplha = 0.2f;
    SpriteRenderer imageBg = this.popupMask.GetComponent<SpriteRenderer>();
    float index = imageBg.color.a;
    if (!state)
    {
      while ((double) index > 0.0)
      {
        imageBg.color = new Color(0.0f, 0.0f, 0.0f, index);
        index -= 0.05f;
        yield return (object) null;
      }
      imageBg.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
      this.popupMask.gameObject.SetActive(false);
    }
    else
    {
      this.popupMask.gameObject.SetActive(true);
      while ((double) index < (double) maxAlplha)
      {
        imageBg.color = new Color(0.0f, 0.0f, 0.0f, index);
        index += 0.05f;
        yield return (object) null;
      }
      imageBg.color = new Color(0.0f, 0.0f, 0.0f, maxAlplha);
    }
  }

  public bool IsBoxFilled(GameObject _box)
  {
    if (this.boxFilled.ContainsKey(_box))
      return this.boxFilled[_box];
    this.boxFilled[_box] = false;
    return false;
  }

  private bool AllBoxWithHeroes()
  {
    if (!GameManager.Instance.IsObeliskChallenge() && SandboxManager.Instance.IsEnabled())
    {
      switch (SandboxManager.Instance.GetSandboxBoxValue("sbTotalHeroes"))
      {
        case 1:
          return this.boxFilled[this.boxGO[0]];
        case 2:
          return this.boxFilled[this.boxGO[0]] && this.boxFilled[this.boxGO[1]];
        case 3:
          return this.boxFilled[this.boxGO[0]] && this.boxFilled[this.boxGO[1]] && this.boxFilled[this.boxGO[2]];
      }
    }
    if (this.boxFilled.Count <= 0)
      return false;
    int num = 0;
    foreach (GameObject key in this.boxFilled.Keys)
    {
      if (this.boxFilled[key])
        ++num;
    }
    return num == 4;
  }

  private bool AllBoxWithOwners()
  {
    if (!GameManager.Instance.IsMultiplayer())
      return true;
    int num1 = 4;
    if (!GameManager.Instance.IsObeliskChallenge() && SandboxManager.Instance.IsEnabled() && SandboxManager.Instance.GetSandboxBoxValue("sbTotalHeroes") > 0)
      num1 = SandboxManager.Instance.GetSandboxBoxValue("sbTotalHeroes");
    int num2 = 0;
    for (int index = 0; index < 4; ++index)
    {
      if (this.boxSelection[index].gameObject.activeSelf && this.boxSelection[index].GetOwner() != null && this.boxSelection[index].GetOwner() != "")
        ++num2;
    }
    return num2 == num1;
  }

  public void ShowDrag(bool state, Vector3 position)
  {
    this.cursorDragPrefab.gameObject.SetActive(state);
    if (!state)
      return;
    this.cursorDragPrefab.transform.position = position;
  }

  public void FillBox(GameObject _box, HeroSelection _heroSelection, bool _state)
  {
    this.boxFilled[_box] = _state;
    this.boxHero[_box] = _heroSelection;
    int num = int.Parse(_box.name.Split('_', StringSplitOptions.None)[1]);
    if ((UnityEngine.Object) _heroSelection != (UnityEngine.Object) null)
    {
      string str = this.SubclassByName[_heroSelection.Id];
      SubClassData subClassData = Globals.Instance.GetSubClassData(str);
      if (this.IsYourBox("Box_" + int.Parse(_box.name.Split('_', StringSplitOptions.None)[1]).ToString()))
        this.AddToPlayerHeroCardback(str, PlayerManager.Instance.GetActiveCardback(subClassData.Id));
      if (GameManager.Instance.IsMultiplayer())
        this.AssignHeroToBox(str, num);
    }
    else if (GameManager.Instance.IsMultiplayer())
    {
      this.ClearBox(num, false);
      this.photonView.RPC("NET_ClearBox", RpcTarget.Others, (object) num, (object) false);
    }
    this.CheckButtonEnabled();
  }

  private void CheckButtonEnabled()
  {
    bool flag = true;
    if (GameManager.Instance.IsMultiplayer())
    {
      flag = false;
      List<string> stringList = new List<string>();
      for (int index = 0; index < 4; ++index)
      {
        if (this.boxSelection[index].gameObject.activeSelf)
        {
          string owner = this.boxSelection[index].GetOwner();
          if (owner == "")
          {
            flag = false;
            break;
          }
          if (!stringList.Contains(owner))
            stringList.Add(owner);
        }
      }
      if (stringList.Count == NetworkManager.Instance.PlayerList.Length)
        flag = true;
      if (flag && !NetworkManager.Instance.AllPlayersManualReady())
        flag = false;
    }
    if (flag && this.AllBoxWithOwners() && this.AllBoxWithHeroes())
      this.botonBegin.Enable();
    else
      this.botonBegin.Disable();
  }

  private void AssignHeroToBox(string _hero, int _boxId)
  {
    if (this.IsYourBox("Box_" + _boxId.ToString()) && !GameManager.Instance.IsLoadingGame())
    {
      SubClassData subClassData = Globals.Instance.GetSubClassData(_hero);
      subClassData.CharacterName.ToLower();
      string lower1 = subClassData.Id.ToLower();
      if (this.SubclassByName.ContainsKey(lower1))
        lower1 = this.SubclassByName[lower1];
      string lower2 = lower1.ToLower();
      int perkRank = PlayerManager.Instance.GetPerkRank(subClassData.Id);
      this.heroSelectionDictionary[lower2].SetRankBox(perkRank);
      this.AddToPlayerHeroCardback(_hero, PlayerManager.Instance.GetActiveCardback(subClassData.Id));
      if (GameManager.Instance.IsMultiplayer())
      {
        this.photonView.RPC("NET_AssignHeroToBox", RpcTarget.Others, (object) _hero, (object) _boxId, (object) perkRank, (object) PlayerManager.Instance.GetActiveSkin(subClassData.Id), (object) PlayerManager.Instance.GetActiveCardback(subClassData.Id));
        this.SendHeroPerksMP(subClassData.Id);
      }
    }
    if (PerkTree.Instance.IsActive())
      PerkTree.Instance.DoTeamPerks();
    if (!GameManager.Instance.IsMultiplayer() || !GameManager.Instance.IsLoadingGame() || GameManager.Instance.IsObeliskChallenge() || !NetworkManager.Instance.IsMaster())
      return;
    for (int index = 0; index < this.boxSelection.Length; ++index)
      this.boxSelection[index].CheckSkuForHero();
  }

  [PunRPC]
  private void NET_AssignHeroToBox(
    string _hero,
    int _boxId,
    int _perkRank,
    string _skinId,
    string _cardbackId)
  {
    _hero = _hero.ToLower();
    if (this.SubclassByName.ContainsKey(_hero))
      _hero = this.SubclassByName[_hero];
    _hero = _hero.ToLower();
    GameObject key = this.boxGO[_boxId];
    if (this.heroSelectionDictionary[_hero].selected)
      this.heroSelectionDictionary[_hero].Reset();
    if (!this.boxHero.ContainsKey(key) || (UnityEngine.Object) this.boxHero[key] == (UnityEngine.Object) null)
    {
      this.heroSelectionDictionary[_hero].AssignHeroToBox(this.boxGO[_boxId]);
      this.heroSelectionDictionary[_hero].SetRankBox(_perkRank);
      this.heroSelectionDictionary[_hero].SetSkin(_skinId);
      this.AddToPlayerHeroSkin(_hero, _skinId);
      this.AddToPlayerHeroCardback(_hero, _cardbackId);
    }
    else
    {
      if (!(this.boxHero[key].nameTM.text != _hero))
        return;
      this.boxHero[key].GoBackToOri();
      this.heroSelectionDictionary[_hero].AssignHeroToBox(this.boxGO[_boxId]);
      this.heroSelectionDictionary[_hero].SetRankBox(_perkRank);
      this.heroSelectionDictionary[_hero].SetSkin(_skinId);
      this.AddToPlayerHeroSkin(_hero, _skinId);
      this.AddToPlayerHeroCardback(_hero, _cardbackId);
    }
  }

  [PunRPC]
  private void NET_ClearBox(int _boxId, bool _moveBackHero)
  {
    this.ClearBox(_boxId, _moveBackHero);
    if (!PerkTree.Instance.IsActive())
      return;
    PerkTree.Instance.DoTeamPerks();
  }

  public void ClearBox(int id, bool moveBackHero = true)
  {
    if (!this.IsBoxFilled(this.boxGO[id]))
      return;
    HeroSelection boxHero = this.GetBoxHero(this.boxGO[id]);
    if ((UnityEngine.Object) boxHero != (UnityEngine.Object) null)
      boxHero.GoBackToOri();
    this.FillBox(this.boxGO[id], (HeroSelection) null, false);
  }

  public bool IsHeroSelected(string heroName)
  {
    for (int index = 0; index < 4; ++index)
    {
      HeroSelection boxHero = this.GetBoxHero(this.boxGO[index]);
      if ((UnityEngine.Object) boxHero != (UnityEngine.Object) null && boxHero.nameTM.text == heroName)
        return true;
    }
    return false;
  }

  public string GetBoxOwnerFromIndex(int _index) => (UnityEngine.Object) this.boxSelection[_index] != (UnityEngine.Object) null ? this.boxSelection[_index].GetOwner() : "";

  public HeroSelection GetBoxHeroFromIndex(int _index) => (UnityEngine.Object) this.boxGO[_index] != (UnityEngine.Object) null && this.boxHero.ContainsKey(this.boxGO[_index]) ? this.boxHero[this.boxGO[_index]] : (HeroSelection) null;

  public HeroSelection GetBoxHero(GameObject _box) => this.boxHero.ContainsKey(_box) ? this.boxHero[_box] : (HeroSelection) null;

  public void MouseOverBox(GameObject _box) => this.box = _box;

  public GameObject GetOverBox() => this.box;

  public void BeginAdventure()
  {
    this.botonBegin.gameObject.SetActive(false);
    if (GameManager.Instance.IsMultiplayer() && (!GameManager.Instance.IsMultiplayer() || !NetworkManager.Instance.IsMaster()))
      return;
    if (GameManager.Instance.GameStatus == Enums.GameStatus.LoadGame)
    {
      AtOManager.Instance.DoLoadGameFromMP();
    }
    else
    {
      string[] strArray = new string[4];
      for (int index = 0; index < this.boxHero.Count; ++index)
        strArray[index] = !((UnityEngine.Object) this.boxHero[this.boxGO[index]] != (UnityEngine.Object) null) ? "" : this.boxHero[this.boxGO[index]].GetSubclassName();
      if (!GameManager.Instance.IsMultiplayer() && !GameManager.Instance.IsWeeklyChallenge())
      {
        PlayerManager.Instance.LastUsedTeam = new string[4];
        for (int index = 0; index < 4; ++index)
          PlayerManager.Instance.LastUsedTeam[index] = strArray[index].ToLower();
        SaveManager.SavePlayerData();
      }
      if (!GameManager.Instance.IsObeliskChallenge())
      {
        AtOManager.Instance.SetPlayerPerks(this.playerHeroPerksDict, strArray);
        AtOManager.Instance.SetNgPlus(this.ngValue);
        AtOManager.Instance.SetMadnessCorruptors(this.ngCorruptors);
      }
      else if (!GameManager.Instance.IsWeeklyChallenge())
        AtOManager.Instance.SetObeliskMadness(this.obeliskMadnessValue);
      AtOManager.Instance.SetTeamFromArray(strArray);
      AtOManager.Instance.BeginAdventure();
    }
  }

  public void ChangeSeed()
  {
    if (GameManager.Instance.IsLoadingGame() || AtOManager.Instance.IsFirstGame() || GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
      return;
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.SetSeedId);
    AlertManager.Instance.AlertInput(Texts.Instance.GetText("gameSeedInput"), Texts.Instance.GetText("accept").ToUpper());
  }

  public void SetSeedId()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.SetSeedId);
    if (AlertManager.Instance.GetInputValue() == null)
      return;
    string upper = AlertManager.Instance.GetInputValue().ToUpper();
    if (!(upper.Trim() != ""))
      return;
    this.SetSeed(upper, true);
  }

  private void SetSeed(string _seed, bool stablishGameId = false)
  {
    this.gameSeedTxt.text = _seed;
    if (stablishGameId)
      AtOManager.Instance.SetGameId(_seed);
    if (!GameManager.Instance.IsMultiplayer() || !NetworkManager.Instance.IsMaster())
      return;
    this.photonView.RPC("NET_SetSeed", RpcTarget.Others, (object) _seed);
  }

  [PunRPC]
  private void NET_SetSeed(string _seed)
  {
    Debug.Log((object) ("Net_SeetSeed " + _seed));
    this.gameSeedTxt.text = _seed;
  }

  public void RefreshPerkPoints(string _hero)
  {
    this.heroSelectionDictionary[_hero].SetPerkPoints();
    if (!((UnityEngine.Object) this.charPopup != (UnityEngine.Object) null) || !PerkTree.Instance.IsActive())
      return;
    this.charPopup.RefreshBecauseOfPerks();
  }

  public void DoCharPopMenu(string type)
  {
    switch (type)
    {
      case "stats":
        this.charPopup.ShowStats();
        break;
      case "perks":
        this.charPopup.ShowPerks();
        break;
      case "skins":
        this.charPopup.ShowSkins();
        break;
      case "rank":
        this.charPopup.ShowRank();
        break;
      case "cardbacks":
        this.charPopup.ShowCardbacks();
        break;
    }
  }

  public void NGBox(int value = -1)
  {
  }

  [PunRPC]
  private void NET_SetNGBox(int _value) => this.NGBox(_value);

  public void SetMadnessLevel()
  {
    int madnessTotal = MadnessManager.Instance.CalculateMadnessTotal(this.ngValue, this.ngCorruptors);
    this.madnessLevel.text = string.Format(Texts.Instance.GetText("madnessNumber"), (object) madnessTotal);
    if (madnessTotal == 0)
    {
      this.madnessParticle.gameObject.SetActive(false);
      this.madnessButton.GetComponent<BotonGeneric>().ShowBackgroundDisable(true);
    }
    else
    {
      this.madnessParticle.gameObject.SetActive(true);
      this.madnessButton.GetComponent<BotonGeneric>().ShowBackgroundDisable(false);
    }
    MadnessManager.Instance.RefreshValues(this.ngCorruptors);
    if (!GameManager.Instance.IsMultiplayer() || !NetworkManager.Instance.IsMaster())
      return;
    this.photonView.RPC("NET_SetMadness", RpcTarget.Others, (object) this.ngValue, (object) this.ngCorruptors);
  }

  [PunRPC]
  private void NET_SetMadness(int mLevel, string mCorruptors)
  {
    this.ngValue = this.ngValueMaster = mLevel;
    this.ngCorruptors = mCorruptors;
    this.SetMadnessLevel();
  }

  public void RefreshSandboxButton()
  {
    if (!this.sandboxButton.gameObject.activeSelf)
      return;
    if (SandboxManager.Instance.IsEnabled())
    {
      this.sandboxButton.GetComponent<BotonGeneric>().ShowBackgroundDisable(false);
      this.sandboxButtonCircleOn.gameObject.SetActive(true);
      this.sandboxButtonCircleOff.gameObject.SetActive(false);
    }
    else
    {
      this.sandboxButton.GetComponent<BotonGeneric>().ShowBackgroundDisable(true);
      this.sandboxButtonCircleOn.gameObject.SetActive(false);
      this.sandboxButtonCircleOff.gameObject.SetActive(true);
    }
  }

  public void RefreshCharBoxesBySandboxHeroes()
  {
    if (SandboxManager.Instance.IsEnabled())
    {
      switch (SandboxManager.Instance.GetSandboxBoxValue("sbTotalHeroes"))
      {
        case 1:
          this.ClearBox(1, false);
          this.ClearBox(2, false);
          this.ClearBox(3, false);
          this.boxSelection[1].gameObject.SetActive(false);
          this.boxSelection[2].gameObject.SetActive(false);
          this.boxSelection[3].gameObject.SetActive(false);
          this.CheckButtonEnabled();
          return;
        case 2:
          this.ClearBox(2, false);
          this.ClearBox(3, false);
          this.boxSelection[1].gameObject.SetActive(true);
          this.boxSelection[2].gameObject.SetActive(false);
          this.boxSelection[3].gameObject.SetActive(false);
          this.CheckButtonEnabled();
          return;
        case 3:
          this.ClearBox(3, false);
          this.boxSelection[1].gameObject.SetActive(true);
          this.boxSelection[2].gameObject.SetActive(true);
          this.boxSelection[3].gameObject.SetActive(false);
          this.CheckButtonEnabled();
          return;
      }
    }
    this.boxSelection[1].gameObject.SetActive(true);
    this.boxSelection[2].gameObject.SetActive(true);
    this.boxSelection[3].gameObject.SetActive(true);
    this.CheckButtonEnabled();
  }

  public void ShareResetSandbox() => this.photonView.RPC("NET_ShareResetSandbox", RpcTarget.Others);

  [PunRPC]
  private void NET_ShareResetSandbox() => SandboxManager.Instance.Reset();

  [PunRPC]
  private void NET_ShareSandbox(string json)
  {
    AtOManager.Instance.SetSandboxMods(Functions.DecompressString(json));
    SandboxManager.Instance.LoadValuesFromAtOManager();
    this.RefreshCharBoxesBySandboxHeroes();
  }

  public void ShareSandboxCombo(string key, int value) => this.photonView.RPC("NET_ShareSandboxCombo", RpcTarget.Others, (object) key, (object) value);

  [PunRPC]
  private void NET_ShareSandboxCombo(string key, int value) => SandboxManager.Instance.SetComboValueByVal(key, value);

  public void ShareSandboxBox(string key, int value) => this.photonView.RPC("NET_ShareSandboxBox", RpcTarget.Others, (object) key, (object) value);

  [PunRPC]
  private void NET_ShareSandboxBox(string key, int value)
  {
    SandboxManager.Instance.SetBoxValueByVal(key, value);
    this.RefreshCharBoxesBySandboxHeroes();
  }

  public void ShareSandboxEnabledState(bool state) => this.photonView.RPC("NET_ShareSandboxEnabledState", RpcTarget.Others, (object) state);

  [PunRPC]
  private void NET_ShareSandboxEnabledState(bool state)
  {
    if (state)
      SandboxManager.Instance.EnableSandbox();
    else
      SandboxManager.Instance.DisableSandbox();
    this.RefreshSandboxButton();
  }

  public void SetObeliskMadnessLevel()
  {
    int obeliskMadnessValue = this.obeliskMadnessValue;
    this.madnessLevel.text = string.Format(Texts.Instance.GetText("madnessNumber"), (object) obeliskMadnessValue);
    if (obeliskMadnessValue == 0)
      this.madnessParticle.gameObject.SetActive(false);
    else
      this.madnessParticle.gameObject.SetActive(true);
    MadnessManager.Instance.RefreshValues();
    if (GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
      this.photonView.RPC("NET_SetObeliskMadness", RpcTarget.Others, (object) obeliskMadnessValue);
    if (this.obeliskMadnessValue > 7)
      this.gameSeed.gameObject.SetActive(false);
    else
      this.gameSeed.gameObject.SetActive(true);
  }

  [PunRPC]
  private void NET_SetObeliskMadness(int mLevel)
  {
    Debug.Log((object) ("NET_SetObeliskMadness " + mLevel.ToString()));
    this.obeliskMadnessValue = this.obeliskMadnessValueMaster = mLevel;
    this.SetObeliskMadnessLevel();
  }

  public void SetSkinIntoSubclassData(string _subclass, string _skinId)
  {
    SubClassData subClassData = Globals.Instance.GetSubClassData(_subclass);
    SkinData skinData = Globals.Instance.GetSkinData(_skinId);
    foreach (KeyValuePair<string, HeroSelection> heroSelection in this.heroSelectionDictionary)
    {
      if (heroSelection.Key == subClassData.Id)
      {
        if (!GameManager.Instance.IsMultiplayer())
        {
          heroSelection.Value.SetSprite(skinData.SpritePortrait, skinData.SpriteSilueta, subClassData.SpriteBorderLocked);
          this.AddToPlayerHeroSkin(_subclass, _skinId);
        }
        else
        {
          heroSelection.Value.SetSpriteSilueta(skinData.SpriteSilueta);
          this.SendHeroSkinMP(_subclass, _skinId);
        }
      }
    }
  }

  public void FollowTheLeader()
  {
    AtOManager.Instance.followingTheLeader = !AtOManager.Instance.followingTheLeader;
    SaveManager.SaveIntoPrefsBool("followLeader", AtOManager.Instance.followingTheLeader);
    SaveManager.SavePrefs();
    this.ShowFollowStatus();
  }

  public void ShowFollowStatus()
  {
    if (AtOManager.Instance.followingTheLeader)
      this.botonFollow.SetText("X");
    else
      this.botonFollow.SetText("");
  }

  public void Ready()
  {
    if (!GameManager.Instance.IsMultiplayer())
      return;
    if (this.manualReadyCo != null)
      this.StopCoroutine(this.manualReadyCo);
    this.statusReady = !this.statusReady;
    NetworkManager.Instance.SetManualReady(this.statusReady);
    if (this.statusReady)
      this.ReadySetButton(true);
    else
      this.ReadySetButton(false);
  }

  public void SetPlayersReady()
  {
    for (int index = 0; index < 4; ++index)
      this.boxSelection[index].SetReady(NetworkManager.Instance.IsPlayerReady(this.boxSelection[index].GetOwner()));
    if (!NetworkManager.Instance.IsMaster())
      return;
    this.CheckButtonEnabled();
  }

  public void ReadySetButton(bool state)
  {
    if (state)
    {
      this.readyButtonText.gameObject.SetActive(false);
      this.readyButton.GetComponent<BotonGeneric>().SetColorAbsolute(Functions.HexToColor("#15A42E"));
      if (GameManager.Instance.IsMultiplayer())
        this.readyButton.GetComponent<BotonGeneric>().SetText(Texts.Instance.GetText("waitingForPlayers"));
      else
        this.readyButton.GetComponent<BotonGeneric>().SetText(Texts.Instance.GetText("ready"));
    }
    else
    {
      this.readyButtonText.gameObject.SetActive(true);
      this.readyButton.GetComponent<BotonGeneric>().SetColorAbsolute(Functions.HexToColor(Globals.Instance.ClassColor["warrior"]));
      this.readyButton.GetComponent<BotonGeneric>().SetText(Texts.Instance.GetText("ready"));
    }
  }

  public void SetSkinFromNetPlayer(string _nick, string _subclass, string _skinId)
  {
  }

  public void SetRandomHero(int _boxId)
  {
    int count = this.heroSelectionDictionary.Count;
    HeroSelection heroSelection1 = (HeroSelection) null;
    bool flag;
    do
    {
      do
      {
        int num1 = UnityEngine.Random.Range(0, count);
        int num2 = 0;
        foreach (KeyValuePair<string, HeroSelection> heroSelection2 in this.heroSelectionDictionary)
        {
          if (num2 == num1)
          {
            heroSelection1 = heroSelection2.Value;
            break;
          }
          ++num2;
        }
        if (!((UnityEngine.Object) heroSelection1 != (UnityEngine.Object) null))
          goto label_7;
      }
      while (!heroSelection1.RandomAvailable());
      flag = true;
      for (int index = 0; index < this.boxGO.Length; ++index)
      {
        if ((UnityEngine.Object) this.boxHero[this.boxGO[index]] != (UnityEngine.Object) null && this.boxHero[this.boxGO[index]].GetSubclassName().ToLower() == heroSelection1.Id)
        {
          flag = false;
          break;
        }
      }
    }
    while (!flag);
    goto label_17;
label_7:
    return;
label_17:
    heroSelection1.PickHero(true);
    heroSelection1.PickStop(_boxId);
  }

  public void LevelWithSupplies(string _scdId)
  {
    PlayerManager.Instance.SpendSupply(1);
    this.IncreaseHeroProgressSupplies(_scdId);
    this.UpdateSubclassRank(_scdId);
    this.charPopup.DoRank();
  }

  private void UpdateSubclassRank(string _scdId)
  {
    SubClassData subClassData = Globals.Instance.GetSubClassData(_scdId);
    foreach (KeyValuePair<string, HeroSelection> heroSelection in this.heroSelectionDictionary)
    {
      if (heroSelection.Key == subClassData.Id)
      {
        if (!GameManager.Instance.IsMultiplayer())
          heroSelection.Value.SetRank();
        else
          heroSelection.Value.SetRank();
      }
    }
  }

  public void ControllerMoveBlock(bool _isRight)
  {
  }

  public void ResetController()
  {
    Debug.Log((object) nameof (ResetController));
    this.controllerCurrentBlock = -1;
    this.controllerCurrentOption = -1;
    this.HideCharacterArrowController();
  }

  public void BackToControllerCharacterSelection()
  {
    Debug.Log((object) nameof (BackToControllerCharacterSelection));
    this.controllerCurrentBlock = 0;
    this.HideCharacterArrowController();
    this.ControllerMovement();
  }

  public void MoveAbsoluteToCharactersAfterClick()
  {
    Debug.Log((object) nameof (MoveAbsoluteToCharactersAfterClick));
    this.controllerHorizontalIndex = 0;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.boxSelection[0].transform.position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }

  private void HideCharacterArrowController()
  {
    for (int index = 0; index < 4; ++index)
      this.boxSelection[index].ShowHideArrowController(false);
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    int absolutePosition = -1)
  {
    this._controllerList.Clear();
    if (!this.dragging)
    {
      for (int index = 0; index < this.menuController.Count; ++index)
        this._controllerList.Add(this.menuController[index]);
      if (Functions.TransformIsVisible(this.botonFollow.transform))
        this._controllerList.Add(this.botonFollow.transform);
      if (Functions.TransformIsVisible(this.beginAdventureButton))
        this._controllerList.Add(this.beginAdventureButton.transform);
      if (Functions.TransformIsVisible(this.readyButton))
        this._controllerList.Add(this.readyButton.transform);
      if (Functions.TransformIsVisible(this.madnessButton))
        this._controllerList.Add(this.madnessButton);
      if (Functions.TransformIsVisible(this.sandboxButton))
        this._controllerList.Add(this.sandboxButton);
      if (Functions.TransformIsVisible(this.gameSeed))
        this._controllerList.Add(this.gameSeed);
      if (Functions.TransformIsVisible(this.weeklyModifiersButton))
        this._controllerList.Add(this.weeklyModifiersButton);
    }
    for (int index1 = 0; index1 < 4; ++index1)
    {
      if (Functions.TransformIsVisible(this.boxSelection[index1].transform))
      {
        this._controllerList.Add(this.boxSelection[index1].transform);
        if (!this.dragging)
        {
          if (Functions.TransformIsVisible(this.boxSelection[index1].dice))
            this._controllerList.Add(this.boxSelection[index1].dice);
          for (int index2 = 0; index2 < 4; ++index2)
          {
            if (Functions.TransformIsVisible(this.boxSelection[index1].boxPlayer[index2].transform))
              this._controllerList.Add(this.boxSelection[index1].boxPlayer[index2].transform);
          }
        }
      }
    }
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }

  public void ControllerMovementOLD(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    int absolutePosition = -1)
  {
    if (absolutePosition != -1)
      this.controllerCurrentOption = absolutePosition;
    if (this.controllerCurrentBlock == 0)
    {
      bool flag = false;
      List<Transform> menuController = this.menuController;
      while (!flag)
      {
        if (menuController != null)
        {
          if (goingLeft)
            --this.controllerCurrentOption;
          else if (goingUp)
            this.controllerCurrentOption -= 4;
          else if (goingDown)
          {
            this.controllerCurrentOption += 4;
          }
          else
          {
            ++this.controllerCurrentOption;
            if (this.controllerCurrentOption % 4 == 0 || this.controllerCurrentOption == menuController.Count - 1)
            {
              this.controllerCurrentBlock = 1;
              this.controllerCurrentOption = -1;
              this.ControllerMovement(goingRight: true);
              break;
            }
          }
          if (this.controllerCurrentOption < 0)
            this.controllerCurrentOption = menuController.Count - 1;
          if (this.controllerCurrentOption > menuController.Count - 1)
            this.controllerCurrentOption = 0;
          Transform transform = menuController[this.controllerCurrentOption];
          if ((UnityEngine.Object) transform != (UnityEngine.Object) null && Functions.TransformIsVisible(transform) && !((UnityEngine.Object) transform.parent == (UnityEngine.Object) this.boxCharacters))
          {
            this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(transform.position);
            Mouse.current.WarpCursorPosition(this.warpPosition);
            flag = true;
          }
        }
        else
          flag = true;
      }
    }
    else if (this.controllerCurrentBlock == 1)
    {
      bool flag = false;
      int num = 0;
      if (absolutePosition != -1)
      {
        --absolutePosition;
        goingLeft = true;
      }
      while (!flag)
      {
        if (goingRight)
        {
          --this.controllerCurrentOption;
          if (this.controllerCurrentOption < 0)
            this.controllerCurrentOption = 3;
        }
        else if (goingLeft)
        {
          ++this.controllerCurrentOption;
          if (this.controllerCurrentOption > 3)
            this.controllerCurrentOption = 0;
        }
        else if (goingUp)
        {
          if (this.boxSelection[this.controllerCurrentOption].dice.gameObject.activeSelf)
          {
            this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.boxSelection[this.controllerCurrentOption].dice.position);
            Mouse.current.WarpCursorPosition(this.warpPosition);
            return;
          }
        }
        else
        {
          this.controllerCurrentBlock = 4;
          this.ControllerMovement(true);
          return;
        }
        flag = Functions.TransformIsVisible(this.boxSelection[this.controllerCurrentOption].transform);
        ++num;
        if (num > 10)
          break;
      }
      this.HideCharacterArrowController();
      this.boxSelection[this.controllerCurrentOption].ShowHideArrowController(true);
      this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.boxSelection[this.controllerCurrentOption].transform.position);
      Mouse.current.WarpCursorPosition(this.warpPosition);
    }
    else if (this.controllerCurrentBlock == 2)
    {
      if (goingUp)
      {
        this.controllerCurrentBlock = 1;
        this.controllerCurrentOption = -1;
        this.ControllerMovement(goingRight: true);
      }
      else if (goingDown)
      {
        this.controllerCurrentBlock = 4;
        this.ControllerMovement(goingRight: true);
      }
      else
      {
        this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.beginAdventureButton.position);
        Mouse.current.WarpCursorPosition(this.warpPosition);
      }
    }
    else if (this.controllerCurrentBlock == 3)
    {
      if (goingUp)
      {
        this.controllerCurrentBlock = 1;
        this.controllerCurrentOption = -1;
        this.ControllerMovement(goingRight: true);
      }
      else if (goingDown)
      {
        this.controllerCurrentBlock = 4;
        this.ControllerMovement(goingRight: true);
      }
      else
      {
        this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.botonFollow.transform.parent.position);
        Mouse.current.WarpCursorPosition(this.warpPosition);
      }
    }
    else if (this.controllerCurrentBlock == 4)
    {
      if (goingUp)
      {
        if (Functions.TransformIsVisible(this.botonFollow.transform))
        {
          this.controllerCurrentBlock = 3;
          this.ControllerMovement(goingRight: true);
        }
        else
        {
          this.controllerCurrentBlock = 2;
          this.ControllerMovement(goingRight: true);
        }
      }
      else if (goingDown)
      {
        this.controllerCurrentBlock = 5;
        this.ControllerMovement(goingRight: true);
      }
      else
      {
        this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.madnessButton.position);
        Mouse.current.WarpCursorPosition(this.warpPosition);
      }
    }
    else if (this.controllerCurrentBlock == 5)
    {
      this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.sandboxButton.position);
      Mouse.current.WarpCursorPosition(this.warpPosition);
    }
    else
    {
      if (this.controllerCurrentBlock != 6)
        return;
      if (this.gameSeed.gameObject.activeSelf)
      {
        this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.gameSeed.position);
        Mouse.current.WarpCursorPosition(this.warpPosition);
      }
      else
        this.ControllerMovement(goingUp, goingRight, goingDown, goingLeft, absolutePosition);
    }
  }
}
