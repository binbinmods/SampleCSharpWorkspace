// Decompiled with JetBrains decompiler
// Type: SandboxManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SandboxManager : MonoBehaviour
{
  public Transform sandboxWindow;
  public Transform buttonReset;
  public Transform buttonEnable;
  public Transform buttonDisable;
  public Transform buttonMadness;
  public Transform buttonExit;
  public Transform enabledBorder;
  public BotonGeneric boxTotalHeroes3;
  public BotonGeneric boxTotalHeroes2;
  public BotonGeneric boxTotalHeroes1;
  public Transform sandboxOptions;
  private Dictionary<string, BotonGeneric[]> boxs;
  private Dictionary<string, int> sandBoxValues;
  private Color bgOn = new Color(0.88f, 0.2f, 0.75f, 0.2f);
  private Color bgOff = new Color(0.0f, 0.0f, 0.0f, 0.2f);
  private Dictionary<string, int> currentValue;
  private Dictionary<string, List<int>> keyValue;
  private Dictionary<string, int> defaultValue;
  private Dictionary<string, TMP_Text> comboValues;
  private List<string> showPositiveSign;
  private List<string> valueIsPercent;
  private bool isEnabled;
  private List<Transform> allButons;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();
  private List<Transform> _controllerVerticalList = new List<Transform>();

  public static SandboxManager Instance { get; private set; }

  private void Awake()
  {
    if ((UnityEngine.Object) SandboxManager.Instance == (UnityEngine.Object) null)
      SandboxManager.Instance = this;
    else if ((UnityEngine.Object) SandboxManager.Instance != (UnityEngine.Object) this)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
  }

  private void Start() => this.allButons = Functions.FindChildrenByName(this.sandboxOptions, new string[2]
  {
    "SandboxBox",
    "SandboxBoxCheck"
  });

  public void InitSandbox()
  {
    this.InitCombos();
    this.InitOptions();
    this.CloseSandbox();
  }

  public bool IsEnabled() => this.isEnabled;

  public void EnableSandbox()
  {
    this.isEnabled = true;
    this.ShowEnableButtons();
    if (!(bool) (UnityEngine.Object) HeroSelectionManager.Instance)
      return;
    if (GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
      HeroSelectionManager.Instance.ShareSandboxEnabledState(true);
    HeroSelectionManager.Instance.RefreshCharBoxesBySandboxHeroes();
  }

  public void DisableSandbox()
  {
    this.isEnabled = false;
    this.ShowEnableButtons();
    if (!(bool) (UnityEngine.Object) HeroSelectionManager.Instance)
      return;
    if (GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
      HeroSelectionManager.Instance.ShareSandboxEnabledState(false);
    HeroSelectionManager.Instance.RefreshCharBoxesBySandboxHeroes();
  }

  private void ShowEnableButtons()
  {
    if (this.isEnabled)
      this.enabledBorder.gameObject.SetActive(true);
    else
      this.enabledBorder.gameObject.SetActive(false);
    if (this.CanClickOptions())
    {
      if (this.isEnabled)
      {
        this.buttonEnable.gameObject.SetActive(false);
        this.buttonDisable.gameObject.SetActive(true);
      }
      else
      {
        this.buttonEnable.gameObject.SetActive(true);
        this.buttonDisable.gameObject.SetActive(false);
      }
    }
    else
    {
      this.buttonEnable.gameObject.SetActive(false);
      this.buttonDisable.gameObject.SetActive(false);
    }
    if (!(bool) (UnityEngine.Object) HeroSelectionManager.Instance)
      return;
    HeroSelectionManager.Instance.RefreshSandboxButton();
  }

  private void InitCombos()
  {
    this.keyValue = new Dictionary<string, List<int>>();
    this.currentValue = new Dictionary<string, int>();
    this.defaultValue = new Dictionary<string, int>();
    this.comboValues = new Dictionary<string, TMP_Text>();
    this.valueIsPercent = new List<string>();
    this.showPositiveSign = new List<string>();
    this.SetCombo("sbEnergy", 0, -10, 10, 1, true, false);
    this.SetCombo("sbSpeed", 0, -20, 20, 1, true, false);
    this.SetCombo("sbGold", 0, -10000, 50000, 2500, true, false);
    this.SetCombo("sbShards", 0, -10000, 50000, 2500, true, false);
    this.SetCombo("sbCraftCost", 0, -100, 300, 25, true, true);
    this.SetCombo("sbUpgradeCost", 0, -100, 300, 25, true, true);
    this.SetCombo("sbTransformCost", 0, -100, 300, 25, true, true);
    this.SetCombo("sbRemoveCost", 0, -100, 300, 25, true, true);
    this.SetCombo("sbEquipmentCost", 0, -100, 300, 25, true, true);
    this.SetCombo("sbPetsCost", 0, -100, 300, 25, true, true);
    this.SetCombo("sbDivinationCost", 0, -100, 300, 25, true, true);
    this.SetCombo("sbMonstersHP", 0, -75, 300, 25, true, true);
    this.SetCombo("sbMonstersDamage", 0, -75, 300, 25, true, true);
  }

  private void InitOptions()
  {
    Transform[] children = SandboxManager.FindChildren(this.sandboxWindow, "SandboxBoxCheck");
    this.boxs = new Dictionary<string, BotonGeneric[]>();
    this.sandBoxValues = new Dictionary<string, int>();
    int length = 5;
    foreach (Component component1 in children)
    {
      BotonGeneric component2 = component1.GetComponent<BotonGeneric>();
      if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
      {
        if (!this.sandBoxValues.ContainsKey(component2.auxString))
          this.sandBoxValues.Add(component2.auxString, 0);
        if (!this.boxs.ContainsKey(component2.auxString))
        {
          this.boxs.Add(component2.auxString, new BotonGeneric[length]);
          for (int index = 0; index < length; ++index)
            this.boxs[component2.auxString][index] = (BotonGeneric) null;
        }
        this.boxs[component2.auxString][component2.auxInt] = component2;
      }
    }
  }

  private void SetCombo(
    string key,
    int def,
    int min,
    int max,
    int step,
    bool showPositive,
    bool isPercent)
  {
    Transform transform = GameObject.Find(key).transform;
    if (!((UnityEngine.Object) transform != (UnityEngine.Object) null))
      return;
    if (!this.keyValue.ContainsKey(key))
      this.keyValue.Add(key, new List<int>());
    int num = 0;
    for (int index = min; index <= max; index += step)
    {
      this.keyValue[key].Add(index);
      if (index == def)
      {
        this.defaultValue.Add(key, num);
        this.currentValue.Add(key, num);
      }
      ++num;
    }
    this.comboValues.Add(key, transform.GetComponent<TMP_Text>());
    if (isPercent)
      this.valueIsPercent.Add(key);
    if (showPositive)
      this.showPositiveSign.Add(key);
    this.SetComboValue(key);
  }

  private void SetComboValue(string key)
  {
    if (!this.currentValue.ContainsKey(key) || !this.keyValue.ContainsKey(key) || !this.comboValues.ContainsKey(key))
      return;
    int num = this.keyValue[key][this.currentValue[key]];
    StringBuilder stringBuilder = new StringBuilder();
    if (this.showPositiveSign.Contains(key) && num > 0)
      stringBuilder.Append("+");
    stringBuilder.Append(num);
    if (this.valueIsPercent.Contains(key))
      stringBuilder.Append("%");
    this.comboValues[key].text = stringBuilder.ToString();
  }

  public void SetComboValueByVal(string key, int value)
  {
    for (int index = 0; index < this.keyValue[key].Count; ++index)
    {
      if (this.keyValue[key][index] == value)
      {
        this.currentValue[key] = index;
        break;
      }
    }
    this.SetComboValue(key);
  }

  private void SetBoxValue(string key)
  {
    if (this.boxs == null || !this.boxs.ContainsKey(key))
      return;
    for (int index = 0; index < this.boxs[key].Length; ++index)
    {
      if ((UnityEngine.Object) this.boxs[key][index] != (UnityEngine.Object) null)
      {
        if (this.boxs[key][index].auxInt == this.sandBoxValues[key])
          this.boxs[key][index].SetText("X");
        else
          this.boxs[key][index].SetText("");
      }
    }
  }

  public void SetBoxValueByVal(string key, int value)
  {
    if (!this.sandBoxValues.ContainsKey(key))
      return;
    this.sandBoxValues[key] = value;
    this.SetBoxValue(key);
  }

  public void BoxClick(string auxString, int auxInt)
  {
    if (!this.CanClickOptions())
      return;
    if (this.currentValue.ContainsKey(auxString))
    {
      if (auxInt < 0)
      {
        if (this.currentValue[auxString] > 0)
          this.currentValue[auxString]--;
      }
      else if (auxInt > 0)
      {
        if (this.currentValue[auxString] < this.keyValue[auxString].Count - 1)
          this.currentValue[auxString]++;
      }
      else
        this.currentValue[auxString] = this.defaultValue[auxString];
      this.SetComboValue(auxString);
      if (!(bool) (UnityEngine.Object) HeroSelectionManager.Instance || !GameManager.Instance.IsMultiplayer() || !NetworkManager.Instance.IsMaster())
        return;
      HeroSelectionManager.Instance.ShareSandboxCombo(auxString, this.keyValue[auxString][this.currentValue[auxString]]);
    }
    else
    {
      this.sandBoxValues[auxString] = this.sandBoxValues[auxString] != auxInt ? auxInt : 0;
      this.SetBoxValue(auxString);
      if (!(bool) (UnityEngine.Object) HeroSelectionManager.Instance)
        return;
      if (GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
        HeroSelectionManager.Instance.ShareSandboxBox(auxString, this.sandBoxValues[auxString]);
      HeroSelectionManager.Instance.RefreshCharBoxesBySandboxHeroes();
    }
  }

  public int GetSandboxBoxValue(string auxString) => this.sandBoxValues.ContainsKey(auxString) ? this.sandBoxValues[auxString] : 0;

  public void Reset()
  {
    foreach (KeyValuePair<string, List<int>> keyValuePair in this.keyValue)
    {
      this.currentValue[keyValuePair.Key] = this.defaultValue[keyValuePair.Key];
      this.SetComboValue(keyValuePair.Key);
    }
    foreach (string key in new List<string>((IEnumerable<string>) this.sandBoxValues.Keys))
    {
      this.sandBoxValues[key] = 0;
      this.SetBoxValue(key);
    }
    if (!(bool) (UnityEngine.Object) HeroSelectionManager.Instance)
      return;
    if (GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
      HeroSelectionManager.Instance.ShareResetSandbox();
    HeroSelectionManager.Instance.RefreshCharBoxesBySandboxHeroes();
  }

  public void LoadValuesFromAtOManager()
  {
    this.SetComboValueByVal("sbEnergy", AtOManager.Instance.Sandbox_startingEnergy);
    this.SetComboValueByVal("sbSpeed", AtOManager.Instance.Sandbox_startingSpeed);
    this.SetComboValueByVal("sbGold", AtOManager.Instance.Sandbox_additionalGold);
    this.SetComboValueByVal("sbShards", AtOManager.Instance.Sandbox_additionalShards);
    this.SetComboValueByVal("sbCraftCost", AtOManager.Instance.Sandbox_cardCraftPrice);
    this.SetComboValueByVal("sbUpgradeCost", AtOManager.Instance.Sandbox_cardUpgradePrice);
    this.SetComboValueByVal("sbTransformCost", AtOManager.Instance.Sandbox_cardTransformPrice);
    this.SetComboValueByVal("sbRemoveCost", AtOManager.Instance.Sandbox_cardRemovePrice);
    this.SetComboValueByVal("sbEquipmentCost", AtOManager.Instance.Sandbox_itemsPrice);
    this.SetComboValueByVal("sbPetsCost", AtOManager.Instance.Sandbox_petsPrice);
    this.SetComboValueByVal("sbDivinationCost", AtOManager.Instance.Sandbox_divinationsPrice);
    this.SetBoxValueByVal("sbCraftUnlocked", Convert.ToInt32(AtOManager.Instance.Sandbox_craftUnlocked));
    this.SetBoxValueByVal("sbCardCraftRarity", Convert.ToInt32(AtOManager.Instance.Sandbox_allRarities));
    this.SetBoxValueByVal("sbCraftAvailable", Convert.ToInt32(AtOManager.Instance.Sandbox_unlimitedAvailableCards));
    this.SetBoxValueByVal("sbArmoryRerolls", Convert.ToInt32(AtOManager.Instance.Sandbox_freeRerolls));
    this.SetBoxValueByVal("sbUnlimitedRerolls", Convert.ToInt32(AtOManager.Instance.Sandbox_unlimitedRerolls));
    this.SetBoxValueByVal("sbMinimumDeckSize", Convert.ToInt32(AtOManager.Instance.Sandbox_noMinimumDecksize));
    this.SetBoxValueByVal("sbEventRolls", Convert.ToInt32(AtOManager.Instance.Sandbox_alwaysPassEventRoll));
    this.SetBoxValueByVal("sbTotalHeroes", Convert.ToInt32(AtOManager.Instance.Sandbox_totalHeroes));
    this.SetBoxValueByVal("sbLessMonsters", Convert.ToInt32(AtOManager.Instance.Sandbox_lessNPCs));
    this.SetComboValueByVal("sbMonstersHP", AtOManager.Instance.Sandbox_additionalMonsterHP);
    this.SetComboValueByVal("sbMonstersDamage", AtOManager.Instance.Sandbox_additionalMonsterDamage);
    this.SetBoxValueByVal("sbDoubleChampions", Convert.ToInt32(AtOManager.Instance.Sandbox_doubleChampions));
  }

  public void SaveValuesToAtOManager()
  {
    AtOManager.Instance.Sandbox_startingEnergy = this.keyValue["sbEnergy"][this.currentValue["sbEnergy"]];
    AtOManager.Instance.Sandbox_startingSpeed = this.keyValue["sbSpeed"][this.currentValue["sbSpeed"]];
    AtOManager.Instance.Sandbox_additionalGold = this.keyValue["sbGold"][this.currentValue["sbGold"]];
    AtOManager.Instance.Sandbox_additionalShards = this.keyValue["sbShards"][this.currentValue["sbShards"]];
    AtOManager.Instance.Sandbox_cardCraftPrice = this.keyValue["sbCraftCost"][this.currentValue["sbCraftCost"]];
    AtOManager.Instance.Sandbox_cardUpgradePrice = this.keyValue["sbUpgradeCost"][this.currentValue["sbUpgradeCost"]];
    AtOManager.Instance.Sandbox_cardTransformPrice = this.keyValue["sbTransformCost"][this.currentValue["sbTransformCost"]];
    AtOManager.Instance.Sandbox_cardRemovePrice = this.keyValue["sbRemoveCost"][this.currentValue["sbRemoveCost"]];
    AtOManager.Instance.Sandbox_itemsPrice = this.keyValue["sbEquipmentCost"][this.currentValue["sbEquipmentCost"]];
    AtOManager.Instance.Sandbox_petsPrice = this.keyValue["sbPetsCost"][this.currentValue["sbPetsCost"]];
    AtOManager.Instance.Sandbox_divinationsPrice = this.keyValue["sbDivinationCost"][this.currentValue["sbDivinationCost"]];
    AtOManager.Instance.Sandbox_craftUnlocked = Convert.ToBoolean(this.sandBoxValues["sbCraftUnlocked"]);
    AtOManager.Instance.Sandbox_allRarities = Convert.ToBoolean(this.sandBoxValues["sbCardCraftRarity"]);
    AtOManager.Instance.Sandbox_unlimitedAvailableCards = Convert.ToBoolean(this.sandBoxValues["sbCraftAvailable"]);
    AtOManager.Instance.Sandbox_freeRerolls = Convert.ToBoolean(this.sandBoxValues["sbArmoryRerolls"]);
    AtOManager.Instance.Sandbox_unlimitedRerolls = Convert.ToBoolean(this.sandBoxValues["sbUnlimitedRerolls"]);
    AtOManager.Instance.Sandbox_noMinimumDecksize = Convert.ToBoolean(this.sandBoxValues["sbMinimumDeckSize"]);
    AtOManager.Instance.Sandbox_alwaysPassEventRoll = Convert.ToBoolean(this.sandBoxValues["sbEventRolls"]);
    AtOManager.Instance.Sandbox_totalHeroes = this.sandBoxValues["sbTotalHeroes"];
    AtOManager.Instance.Sandbox_lessNPCs = this.sandBoxValues["sbLessMonsters"];
    AtOManager.Instance.Sandbox_additionalMonsterHP = this.keyValue["sbMonstersHP"][this.currentValue["sbMonstersHP"]];
    AtOManager.Instance.Sandbox_additionalMonsterDamage = this.keyValue["sbMonstersDamage"][this.currentValue["sbMonstersDamage"]];
    AtOManager.Instance.Sandbox_doubleChampions = Convert.ToBoolean(this.sandBoxValues["sbDoubleChampions"]);
    AtOManager.Instance.GetSandboxMods();
  }

  public bool IsActive() => this.sandboxWindow.gameObject.activeSelf;

  private IEnumerator CloseSandboxCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(1f);
  }

  public void CloseSandbox()
  {
    if ((bool) (UnityEngine.Object) HeroSelectionManager.Instance && (!GameManager.Instance.IsMultiplayer() || GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster()) && GameManager.Instance.GameStatus != Enums.GameStatus.LoadGame)
    {
      this.SaveValuesToAtOManager();
      SaveManager.SaveIntoPrefsString("sandboxSettings", AtOManager.Instance.GetSandboxMods());
      SaveManager.SavePrefs();
    }
    if (this.IsActive())
      this.sandboxWindow.gameObject.SetActive(false);
    PopupManager.Instance.ClosePopup();
  }

  public void ShowSandbox()
  {
    if (this.IsActive())
    {
      this.CloseSandbox();
    }
    else
    {
      if (!(bool) (UnityEngine.Object) HeroSelectionManager.Instance)
        this.LoadValuesFromAtOManager();
      if (!this.CanClickOptions())
        this.buttonReset.gameObject.SetActive(false);
      else
        this.buttonReset.gameObject.SetActive(true);
      this.ShowEnableButtons();
      this.sandboxWindow.gameObject.SetActive(true);
    }
  }

  private bool CanClickOptions() => (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster() || PlayerManager.Instance.NgLevel != 0) && (bool) (UnityEngine.Object) HeroSelectionManager.Instance && !GameManager.Instance.IsLoadingGame() && (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster());

  public static Transform[] FindChildren(Transform parent, string name) => Array.FindAll<Transform>(parent.GetComponentsInChildren<Transform>(true), (Predicate<Transform>) (child => child.name == name));

  public void AdjustTotalHeroesBoxToCoop()
  {
    if (!GameManager.Instance.IsMultiplayer())
    {
      this.boxTotalHeroes3.Enable();
      this.boxTotalHeroes2.Enable();
      this.boxTotalHeroes1.Enable();
    }
    else
    {
      switch (NetworkManager.Instance.GetNumPlayers())
      {
        case 2:
          this.boxTotalHeroes1.Disable();
          if (this.GetSandboxBoxValue("sbTotalHeroes") != 1)
            break;
          this.SetBoxValueByVal("sbTotalHeroes", 0);
          break;
        case 3:
          this.boxTotalHeroes1.Disable();
          this.boxTotalHeroes2.Disable();
          if (this.GetSandboxBoxValue("sbTotalHeroes") != 1 && this.GetSandboxBoxValue("sbTotalHeroes") != 2)
            break;
          this.SetBoxValueByVal("sbTotalHeroes", 0);
          break;
        case 4:
          this.boxTotalHeroes1.Disable();
          this.boxTotalHeroes2.Disable();
          this.boxTotalHeroes3.Disable();
          if (this.GetSandboxBoxValue("sbTotalHeroes") != 1 && this.GetSandboxBoxValue("sbTotalHeroes") != 2 && this.GetSandboxBoxValue("sbTotalHeroes") != 3)
            break;
          this.SetBoxValueByVal("sbTotalHeroes", 0);
          break;
      }
    }
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    int absolutePosition = -1)
  {
    this._controllerList.Clear();
    for (int index = 0; index < this.allButons.Count; ++index)
    {
      if (Functions.TransformIsVisible(this.allButons[index]))
        this._controllerList.Add(this.allButons[index]);
    }
    this._controllerList.Add(this.buttonDisable);
    this._controllerList.Add(this.buttonReset);
    if (Functions.TransformIsVisible(this.buttonMadness))
      this._controllerList.Add(this.buttonMadness);
    this._controllerList.Add(this.buttonExit);
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }
}
