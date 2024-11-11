// Decompiled with JetBrains decompiler
// Type: MadnessManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MadnessManager : MonoBehaviour
{
  public Transform madnessWindow;
  public Transform madnessChallengeWindow;
  public Transform madnessWeeklyWindow;
  public Transform buttonSandbox;
  public Transform buttonExit;
  public Transform buttonChallengeExit;
  public Transform buttonWeeklyExit;
  public Transform madnessConfirmButton;
  public Transform madnessChallengeConfirmButton;
  public TMP_Text madnessChallengeDescription;
  public TMP_Text mContent;
  public BotonGeneric[] mButton;
  public TMP_Text[] mCorruptorText;
  public BotonGeneric[] mCorruptor;
  public TMP_Text mFinalLevel;
  public TMP_Text mScoreMod;
  public TMP_Text mChallengeFinalLevel;
  public TMP_Text mChallengeScoreMod;
  public Transform corruptorLocks;
  public TMP_Text[] mChallengeText;
  public BotonGeneric[] mChallengeButton;
  public TMP_Text[] mChallengeWeeklyText;
  public TMP_Text weeklyModificators;
  private string madnessColorOn = "FFC77E";
  private string madnessColorOff = "956984";
  private string madnessCorruptors = "";
  private int madnessSelected;
  private Coroutine showCo;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();
  private List<Transform> _controllerVerticalList = new List<Transform>();

  public static MadnessManager Instance { get; private set; }

  private void Awake()
  {
    if ((Object) MadnessManager.Instance == (Object) null)
      MadnessManager.Instance = this;
    else if ((Object) MadnessManager.Instance != (Object) this)
      Object.Destroy((Object) this.gameObject);
    Object.DontDestroyOnLoad((Object) this.gameObject);
  }

  public void InitMadness()
  {
    string[] strArray = new string[11];
    foreach (KeyValuePair<string, ChallengeTrait> keyValuePair in Globals.Instance.ChallengeTraitsSource)
    {
      if (keyValuePair.Value.IsMadnessTrait)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if ((Object) keyValuePair.Value.Icon != (Object) null)
          this.mChallengeButton[keyValuePair.Value.Order].transform.parent.GetComponent<ChallengeMadness>().SetIcon(keyValuePair.Value.Icon);
        stringBuilder.Append(Texts.Instance.GetText(keyValuePair.Value.Id));
        stringBuilder.Append("\n<size=-.4><color=#999>");
        stringBuilder.Append(Texts.Instance.GetText(keyValuePair.Value.Id + "desc"));
        strArray[keyValuePair.Value.Order] = stringBuilder.ToString();
      }
    }
    for (int index = 0; index < this.mChallengeText.Length; ++index)
      this.mChallengeText[index].text = strArray[index];
    this.madnessWindow.gameObject.SetActive(false);
    this.madnessChallengeWindow.gameObject.SetActive(false);
    this.madnessWeeklyWindow.gameObject.SetActive(false);
  }

  public bool IsActive()
  {
    if (!GameManager.Instance.IsObeliskChallenge())
      return this.madnessWindow.gameObject.activeSelf;
    return GameManager.Instance.IsWeeklyChallenge() ? this.madnessWeeklyWindow.gameObject.activeSelf : this.madnessChallengeWindow.gameObject.activeSelf;
  }

  public void RefreshValues(string masterCorruptors = "")
  {
    this.SetMadnessMaster();
    this.madnessCorruptors = masterCorruptors;
    if (this.madnessCorruptors.Length != this.mCorruptor.Length)
      this.madnessCorruptors = "";
    this.SetCorruptors(fromMaster: true);
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster() && GameManager.Instance.IsObeliskChallenge() && !GameManager.Instance.IsWeeklyChallenge())
      this.SetMadnessChallengeRows(HeroSelectionManager.Instance.ObeliskMadnessValueMaster);
    this.SetFinalLevel();
  }

  public void CloseMadness()
  {
    if (this.IsActive())
    {
      if ((Object) this.madnessWindow != (Object) null)
        this.madnessWindow.gameObject.SetActive(false);
      if ((Object) this.madnessChallengeWindow != (Object) null)
        this.madnessChallengeWindow.gameObject.SetActive(false);
      if ((Object) this.madnessWeeklyWindow != (Object) null)
        this.madnessWeeklyWindow.gameObject.SetActive(false);
    }
    PopupManager.Instance.ClosePopup();
  }

  public void ShowMadness()
  {
    if (this.showCo != null)
      this.StopCoroutine(this.showCo);
    this.showCo = this.StartCoroutine(this.ShowMadnessCo());
  }

  private IEnumerator ShowMadnessCo()
  {
    if (this.IsActive())
    {
      this.CloseMadness();
      if ((bool) (Object) CardCraftManager.Instance)
        CardCraftManager.Instance.ShowSearch(true);
    }
    else
    {
      this.mScoreMod.gameObject.SetActive(false);
      if (!GameManager.Instance.IsObeliskChallenge())
      {
        this.madnessWindow.gameObject.SetActive(true);
        yield return (object) new WaitForSeconds(0.01f);
        if ((bool) (Object) HeroSelectionManager.Instance && !GameManager.Instance.IsLoadingGame() && (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster()))
          this.madnessConfirmButton.gameObject.SetActive(true);
        else
          this.madnessConfirmButton.gameObject.SetActive(false);
        this.SetMadness();
        this.SetCorruptors();
        if (PlayerManager.Instance.NgLevel == 0)
        {
          this.corruptorLocks.gameObject.SetActive(true);
          for (int index = 0; index < this.mCorruptor.Length; ++index)
          {
            this.mCorruptor[index].Disable();
            this.TurnOffCorruptor(index);
          }
        }
        else
        {
          this.corruptorLocks.gameObject.SetActive(false);
          for (int index = 0; index < this.mCorruptor.Length; ++index)
            this.mCorruptor[index].Enable();
        }
        if (!(bool) (Object) HeroSelectionManager.Instance || GameManager.Instance.IsLoadingGame())
        {
          for (int index = 0; index < this.mCorruptor.Length; ++index)
            this.mCorruptor[index].Disable();
          for (int index = 0; index < this.mButton.Length; ++index)
            this.mButton[index].Disable();
          if (this.madnessSelected > -1)
            this.mButton[this.madnessSelected].ShowBackgroundDisable(false);
        }
      }
      else if (!GameManager.Instance.IsWeeklyChallenge())
      {
        this.madnessChallengeWindow.gameObject.SetActive(true);
        if ((bool) (Object) HeroSelectionManager.Instance && !GameManager.Instance.IsLoadingGame() && (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster()))
        {
          this.madnessChallengeConfirmButton.gameObject.SetActive(true);
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append(Texts.Instance.GetText("madnessChallengeSelect"));
          stringBuilder.Append(" ");
          stringBuilder.Append(Texts.Instance.GetText("madnessChallengeBeat"));
          this.madnessChallengeDescription.text = stringBuilder.ToString();
        }
        else
        {
          this.madnessChallengeDescription.text = "";
          this.madnessChallengeConfirmButton.gameObject.SetActive(false);
        }
        this.SetMadness();
        if (!(bool) (Object) HeroSelectionManager.Instance || GameManager.Instance.IsLoadingGame() || GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
        {
          for (int index = 0; index < this.mChallengeButton.Length; ++index)
            this.mChallengeButton[index].Disable();
          if (this.madnessSelected > -1)
            this.mChallengeButton[this.madnessSelected].ShowBackgroundDisable(false);
        }
      }
      else
      {
        ChallengeData weeklyData = Globals.Instance.GetWeeklyData(AtOManager.Instance.GetWeekly());
        if ((Object) weeklyData != (Object) null && weeklyData.Traits != null)
        {
          StringBuilder stringBuilder = new StringBuilder();
          for (int index = 0; index < weeklyData.Traits.Count; ++index)
          {
            this.mChallengeWeeklyText[index].transform.parent.gameObject.SetActive(true);
            ChallengeTrait trait = weeklyData.Traits[index];
            if ((Object) trait.Icon != (Object) null)
              this.mChallengeWeeklyText[index].transform.parent.GetComponent<ChallengeMadness>().SetIcon(trait.Icon);
            stringBuilder.Append(Texts.Instance.GetText(trait.Id));
            stringBuilder.Append("\n<size=-.4><color=#999>");
            stringBuilder.Append(Texts.Instance.GetText(trait.Id + "desc"));
            this.mChallengeWeeklyText[index].text = stringBuilder.ToString();
            stringBuilder.Clear();
          }
          for (int count = weeklyData.Traits.Count; count < this.mChallengeWeeklyText.Length; ++count)
            this.mChallengeWeeklyText[count].transform.parent.gameObject.SetActive(false);
          stringBuilder.Clear();
          stringBuilder.Append("<size=+2><color=#C19ED9>");
          stringBuilder.Append(AtOManager.Instance.GetWeeklyName(AtOManager.Instance.GetWeekly()));
          stringBuilder.Append("</color></size>\n");
          stringBuilder.Append(Texts.Instance.GetText("weeklyModificatorsDescription"));
          this.weeklyModificators.text = stringBuilder.ToString();
        }
        this.madnessWeeklyWindow.gameObject.SetActive(true);
      }
      if ((bool) (Object) CardCraftManager.Instance)
        CardCraftManager.Instance.ShowSearch(false);
    }
  }

  public void MadnessConfirm()
  {
    if (!(bool) (Object) HeroSelectionManager.Instance)
      return;
    string str = "";
    if (GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
      str = "Coop";
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      HeroSelectionManager.Instance.NgValue = this.madnessSelected;
      HeroSelectionManager.Instance.NgValueMaster = this.madnessSelected;
      HeroSelectionManager.Instance.NgCorruptors = this.madnessCorruptors;
      SaveManager.SaveIntoPrefsInt("madnessLevel" + str, this.madnessSelected);
      SaveManager.SaveIntoPrefsString("madnessCorruptors" + str, this.madnessCorruptors);
      HeroSelectionManager.Instance.SetMadnessLevel();
    }
    else if (!GameManager.Instance.IsWeeklyChallenge())
    {
      HeroSelectionManager.Instance.ObeliskMadnessValue = this.madnessSelected;
      HeroSelectionManager.Instance.ObeliskMadnessValueMaster = this.madnessSelected;
      SaveManager.SaveIntoPrefsInt("obeliskMadness" + str, this.madnessSelected);
      HeroSelectionManager.Instance.SetObeliskMadnessLevel();
    }
    this.ShowMadness();
  }

  public string InitCorruptors()
  {
    string str = "";
    for (int index = 0; index < this.mCorruptor.Length; ++index)
    {
      str += "0";
      this.mCorruptorText[index].text = Functions.PregReplaceIcon(Texts.Instance.GetText("madnessCorruptor" + index.ToString()));
      if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
        this.mCorruptor[index].Disable();
    }
    return str;
  }

  private void SetMadnessMaster()
  {
    if (!GameManager.Instance.IsMultiplayer() || !((Object) HeroSelectionManager.Instance != (Object) null))
      return;
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      for (int index = 0; index < this.mButton.Length; ++index)
      {
        if (HeroSelectionManager.Instance.NgValueMaster == index)
          this.mButton[index].transform.Find("Master").gameObject.SetActive(true);
        else
          this.mButton[index].transform.Find("Master").gameObject.SetActive(false);
      }
    }
    else
    {
      if (GameManager.Instance.IsWeeklyChallenge())
        return;
      for (int index = 0; index < this.mChallengeButton.Length; ++index)
      {
        if (HeroSelectionManager.Instance.ObeliskMadnessValueMaster == index)
        {
          this.mChallengeButton[index].transform.Find("Master").gameObject.SetActive(true);
          if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
          {
            this.mChallengeButton[index].SetBackgroundColor(new Color(1f, 0.78f, 0.49f));
            this.mChallengeButton[index].SetBorderColor(new Color(1f, 0.78f, 0.49f));
            this.mChallengeButton[index].transform.localPosition = new Vector3(-5.8f, this.mChallengeButton[index].transform.localPosition.y, this.mChallengeButton[index].transform.localPosition.z);
          }
        }
        else
        {
          this.mChallengeButton[index].transform.Find("Master").gameObject.SetActive(false);
          if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
          {
            this.mChallengeButton[index].ShowBackgroundDisable(true);
            this.mChallengeButton[index].SetColor();
            this.mChallengeButton[index].transform.localPosition = new Vector3(-5.6f, this.mChallengeButton[index].transform.localPosition.y, this.mChallengeButton[index].transform.localPosition.z);
          }
        }
      }
    }
  }

  private void SetMadness()
  {
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      if ((bool) (Object) HeroSelectionManager.Instance)
        this.SelectMadness(HeroSelectionManager.Instance.NgValue);
      else
        this.SelectMadness(AtOManager.Instance.GetNgPlus());
    }
    else
    {
      if (GameManager.Instance.IsWeeklyChallenge())
        return;
      if ((bool) (Object) HeroSelectionManager.Instance)
        this.SelectMadness(HeroSelectionManager.Instance.ObeliskMadnessValue);
      else
        this.SelectMadness(AtOManager.Instance.GetObeliskMadness());
    }
  }

  public void SetCorruptors(string strCorruptors = "", bool fromMaster = false)
  {
    if (GameManager.Instance.IsObeliskChallenge())
      return;
    if (strCorruptors == "" && this.madnessCorruptors == "")
      strCorruptors = this.InitCorruptors();
    for (int index = 0; index < this.mCorruptor.Length; ++index)
    {
      if (this.IsMadnessCorruptorSelected(index))
        this.TurnOnCorruptor(index, fromMaster);
      else
        this.TurnOffCorruptor(index, fromMaster);
    }
  }

  private void ResetMadnessButtons()
  {
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      for (int index = 0; index < this.mButton.Length; ++index)
      {
        this.mButton[index].Enable();
        this.mButton[index].SetColor();
        this.mButton[index].transform.localPosition = new Vector3(0.0f, this.mButton[index].transform.localPosition.y, this.mButton[index].transform.localPosition.z);
        this.mButton[index].transform.Find("Lock").gameObject.SetActive(false);
        this.mButton[index].transform.Find("Master").gameObject.SetActive(false);
      }
    }
    else if (!GameManager.Instance.IsWeeklyChallenge())
    {
      for (int index = 0; index < this.mChallengeButton.Length; ++index)
      {
        this.mChallengeButton[index].Enable();
        this.mChallengeButton[index].SetColor();
        this.mChallengeButton[index].transform.localPosition = new Vector3(-5.6f, this.mChallengeButton[index].transform.localPosition.y, this.mChallengeButton[index].transform.localPosition.z);
        this.mChallengeButton[index].transform.Find("Lock").gameObject.SetActive(false);
        this.mChallengeButton[index].transform.Find("Master").gameObject.SetActive(false);
      }
    }
    this.SetMadnessMaster();
  }

  private void DisableMadnessButton(int value)
  {
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      this.mButton[value].ShowBorder(false);
      this.mButton[value].SetBackgroundColor(new Color(0.3f, 0.3f, 0.3f));
      this.mButton[value].SetBorderColor(new Color(0.3f, 0.3f, 0.3f));
      this.mButton[value].transform.Find("Lock").gameObject.SetActive(true);
    }
    else
    {
      if (GameManager.Instance.IsWeeklyChallenge())
        return;
      this.mChallengeButton[value].ShowBorder(false);
      this.mChallengeButton[value].SetBackgroundColor(new Color(0.3f, 0.3f, 0.3f));
      this.mChallengeButton[value].SetBorderColor(new Color(0.3f, 0.3f, 0.3f));
      this.mChallengeButton[value].transform.Find("Lock").gameObject.SetActive(true);
      this.mChallengeButton[value].transform.parent.GetComponent<ChallengeMadness>().SetDisable();
    }
  }

  public void SelectMadness(int value)
  {
    this.ResetMadnessButtons();
    int num = 0;
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      this.mButton[value].transform.localPosition = new Vector3(-0.25f, this.mButton[value].transform.localPosition.y, this.mButton[value].transform.localPosition.z);
      this.mButton[value].SetBackgroundColor(new Color(1f, 0.78f, 0.49f));
      this.mButton[value].SetBorderColor(new Color(1f, 0.78f, 0.49f));
      num = PlayerManager.Instance.NgLevel;
      for (int index = 1; index < this.mButton.Length; ++index)
      {
        if (num < index)
          this.DisableMadnessButton(index);
      }
    }
    else if (!GameManager.Instance.IsWeeklyChallenge())
    {
      this.mChallengeButton[value].transform.localPosition = new Vector3(-5.8f, this.mChallengeButton[value].transform.localPosition.y, this.mChallengeButton[value].transform.localPosition.z);
      this.mChallengeButton[value].SetBackgroundColor(new Color(1f, 0.78f, 0.49f));
      this.mChallengeButton[value].SetBorderColor(new Color(1f, 0.78f, 0.49f));
      num = PlayerManager.Instance.ObeliskMadnessLevel;
      this.SetMadnessChallengeRows(value);
    }
    this.mContent.text = Functions.GetMadnessBonusText(value);
    if (value <= num)
      this.madnessSelected = value;
    this.SetFinalLevel();
  }

  private void SetFinalLevel()
  {
    int madnessSelected = this.madnessSelected;
    string madnessCorruptors = this.madnessCorruptors;
    int level = this.CalculateMadnessTotal(madnessSelected, madnessCorruptors);
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
    {
      if (!GameManager.Instance.IsObeliskChallenge())
        level = this.CalculateMadnessTotal(!((Object) HeroSelectionManager.Instance != (Object) null) ? AtOManager.Instance.GetNgPlus() : HeroSelectionManager.Instance.NgValueMaster, madnessCorruptors);
      else if (!GameManager.Instance.IsWeeklyChallenge())
        level = !((Object) HeroSelectionManager.Instance != (Object) null) ? AtOManager.Instance.GetObeliskMadness() : HeroSelectionManager.Instance.ObeliskMadnessValueMaster;
    }
    this.mFinalLevel.text = this.mChallengeFinalLevel.text = level.ToString();
    if (level > 0)
    {
      this.mScoreMod.gameObject.SetActive(true);
      this.mChallengeScoreMod.gameObject.SetActive(true);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Texts.Instance.GetText("finalScore"));
      stringBuilder.Append(" <color=#AAA>(+");
      stringBuilder.Append(Functions.GetMadnessScoreMultiplier(level, !GameManager.Instance.IsObeliskChallenge()));
      stringBuilder.Append("%)");
      this.mScoreMod.text = this.mChallengeScoreMod.text = stringBuilder.ToString();
    }
    else
    {
      this.mScoreMod.gameObject.SetActive(false);
      this.mChallengeScoreMod.gameObject.SetActive(false);
    }
  }

  private void SetMadnessChallengeRows(int _value)
  {
    int obeliskMadnessLevel = PlayerManager.Instance.ObeliskMadnessLevel;
    for (int index = 0; index < this.mChallengeButton.Length; ++index)
    {
      if (index < _value)
        this.mChallengeButton[index].transform.parent.GetComponent<ChallengeMadness>().SetActive();
      if (index == _value)
        this.mChallengeButton[index].transform.parent.GetComponent<ChallengeMadness>().SetActive();
      if (index > _value)
        this.mChallengeButton[index].transform.parent.GetComponent<ChallengeMadness>().SetDefault();
      if (obeliskMadnessLevel < index)
      {
        this.DisableMadnessButton(index);
        this.mChallengeButton[index].Disable();
      }
      else
        this.mChallengeButton[index].Enable();
      if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster())
      {
        this.mChallengeButton[index].Disable();
        this.mChallengeButton[_value].ShowBackgroundDisable(false);
      }
    }
  }

  public int CalculateMadnessTotal(int lvl, string corr = "")
  {
    int num = 0;
    if (corr != "")
      num = this.GetMadnessCorruptorNumber(corr);
    return lvl + num;
  }

  private int GetMadnessCorruptorNumber(string corr = "")
  {
    if (corr == "" || corr == null)
      return 0;
    int madnessCorruptorNumber = 0;
    for (int index = 0; index < corr.Length; ++index)
    {
      if (corr[index] == '1')
        ++madnessCorruptorNumber;
    }
    return madnessCorruptorNumber;
  }

  public void SelectMadnessCorruptor(int index, bool fromButton = true)
  {
    if (!(bool) (Object) HeroSelectionManager.Instance & fromButton)
      return;
    if (this.IsMadnessCorruptorSelected(index))
      this.TurnOffCorruptor(index);
    else
      this.TurnOnCorruptor(index);
    this.CalculateMadnessTotal(this.madnessSelected, this.madnessCorruptors);
  }

  private void TurnOnCorruptor(int index, bool fromMaster = false)
  {
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster() && !fromMaster)
      return;
    this.SetMadnessCorruptor(index, true);
    this.mCorruptor[index].SetText("X");
    this.mCorruptor[index].SetBackgroundColor(Functions.HexToColor(this.madnessColorOn));
    this.mCorruptorText[index].color = Functions.HexToColor(this.madnessColorOn);
  }

  private void TurnOffCorruptor(int index, bool fromMaster = false)
  {
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster() && !fromMaster)
      return;
    this.SetMadnessCorruptor(index, false);
    this.mCorruptor[index].SetText("");
    this.mCorruptor[index].SetBackgroundColor(Functions.HexToColor(this.madnessColorOff));
    this.mCorruptorText[index].color = Functions.HexToColor(this.madnessColorOff);
  }

  public bool IsMadnessTraitActive(string corruptor)
  {
    if (!AtOManager.Instance.IsZoneAffectedByMadness())
      return false;
    int index = -1;
    switch (corruptor)
    {
      case "decadence":
        index = 1;
        break;
      case "despair":
        index = 7;
        break;
      case "impedingdoom":
        index = 0;
        break;
      case "overchargedmonsters":
        index = 5;
        break;
      case "poverty":
        index = 4;
        break;
      case "randomcombats":
        index = 6;
        break;
      case "resistantmonsters":
        index = 3;
        break;
      case "restrictedpower":
        index = 2;
        break;
    }
    return index > -1 && this.IsMadnessCorruptorSelected(index);
  }

  public bool IsMadnessCorruptorSelected(int index)
  {
    string str = !((Object) HeroSelectionManager.Instance != (Object) null) ? AtOManager.Instance.GetMadnessCorruptors() : this.madnessCorruptors;
    if (str == "" || str == null || index >= str.Length)
      return false;
    int num = (int) str[index];
    return str[index] == '1';
  }

  private void SetMadnessCorruptor(int index, bool value)
  {
    if (this.madnessCorruptors == null)
      this.madnessCorruptors = "";
    if (this.madnessCorruptors.Trim() == "")
    {
      for (int index1 = 0; index1 < this.mCorruptor.Length; ++index1)
        this.madnessCorruptors += "0";
    }
    if (this.madnessCorruptors == "")
      return;
    this.madnessCorruptors = new StringBuilder(this.madnessCorruptors)
    {
      [index] = (!value ? '0' : '1')
    }.ToString();
    this.SetFinalLevel();
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    int absolutePosition = -1)
  {
    this._controllerList.Clear();
    for (int index = 0; index < this.mButton.Length; ++index)
    {
      if (Functions.TransformIsVisible(this.mButton[index].transform))
        this._controllerList.Add(this.mButton[index].transform);
    }
    for (int index = 0; index < this.mCorruptor.Length; ++index)
    {
      if (Functions.TransformIsVisible(this.mCorruptor[index].transform))
        this._controllerList.Add(this.mCorruptor[index].transform);
    }
    for (int index = 0; index < this.mChallengeButton.Length; ++index)
    {
      if (Functions.TransformIsVisible(this.mChallengeButton[index].transform))
        this._controllerList.Add(this.mChallengeButton[index].transform);
    }
    if (Functions.TransformIsVisible(this.buttonSandbox))
      this._controllerList.Add(this.buttonSandbox);
    if (Functions.TransformIsVisible(this.madnessConfirmButton))
      this._controllerList.Add(this.madnessConfirmButton);
    if (Functions.TransformIsVisible(this.madnessChallengeConfirmButton))
      this._controllerList.Add(this.madnessChallengeConfirmButton);
    if (Functions.TransformIsVisible(this.buttonExit))
      this._controllerList.Add(this.buttonExit);
    if (Functions.TransformIsVisible(this.buttonChallengeExit))
      this._controllerList.Add(this.buttonChallengeExit);
    if (Functions.TransformIsVisible(this.buttonWeeklyExit))
      this._controllerList.Add(this.buttonWeeklyExit);
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((Object) this._controllerList[this.controllerHorizontalIndex] != (Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }
}
