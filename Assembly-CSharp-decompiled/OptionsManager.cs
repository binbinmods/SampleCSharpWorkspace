// Decompiled with JetBrains decompiler
// Type: OptionsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class OptionsManager : MonoBehaviour
{
  public Transform elements;
  public Transform iconExit;
  public Transform iconStats;
  public Transform iconResign;
  public Transform iconSettings;
  public Transform iconRetry;
  public Transform iconTome;
  public Transform iconCombatLog;
  public Transform madness;
  public Transform madnessParticles;
  public TMP_Text madnessText;
  public Transform version;
  public Transform score;
  public TMP_Text scoreText;
  public Transform position;
  private float distanceBetweenButton = 0.65f;
  private float positionRightButton = 0.95f;
  private float adjustmentForBigButtons = 1.2f;
  private List<GameObject> buttonOrder = new List<GameObject>();
  private int _indexController = -1;

  public static OptionsManager Instance { get; private set; }

  private void Awake()
  {
    if ((Object) OptionsManager.Instance == (Object) null)
      OptionsManager.Instance = this;
    else if ((Object) OptionsManager.Instance != (Object) this)
      Object.Destroy((Object) this.gameObject);
    Object.DontDestroyOnLoad((Object) this.gameObject);
    this.buttonOrder.Add(this.iconExit.gameObject);
    this.buttonOrder.Add(this.iconResign.gameObject);
    this.buttonOrder.Add(this.iconRetry.gameObject);
    this.buttonOrder.Add(this.iconSettings.gameObject);
    this.buttonOrder.Add(this.iconStats.gameObject);
    this.buttonOrder.Add(this.iconCombatLog.gameObject);
    this.buttonOrder.Add(this.iconTome.gameObject);
    this.buttonOrder.Add(this.score.gameObject);
    this.buttonOrder.Add(this.madness.gameObject);
  }

  public void Show()
  {
    if (!this.elements.gameObject.activeSelf)
      this.elements.gameObject.SetActive(true);
    this.iconRetry.gameObject.SetActive(false);
    this.iconCombatLog.gameObject.SetActive(false);
    this.iconResign.gameObject.SetActive(false);
    this.iconTome.gameObject.SetActive(true);
    this.iconExit.gameObject.SetActive(true);
    if ((bool) (Object) MatchManager.Instance || (bool) (Object) MapManager.Instance || (bool) (Object) TownManager.Instance || (bool) (Object) FinishRunManager.Instance || (bool) (Object) RewardsManager.Instance)
      this.iconStats.gameObject.SetActive(true);
    else
      this.iconStats.gameObject.SetActive(false);
    if ((bool) (Object) FinishRunManager.Instance)
    {
      this.iconSettings.gameObject.SetActive(false);
      this.iconExit.gameObject.SetActive(false);
      this.iconTome.gameObject.SetActive(false);
    }
    else
      this.iconSettings.gameObject.SetActive(true);
    if ((bool) (Object) MatchManager.Instance)
    {
      this.version.gameObject.SetActive(true);
      this.iconCombatLog.gameObject.SetActive(true);
      if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
      {
        this.iconResign.gameObject.SetActive(true);
        this.iconRetry.gameObject.SetActive(true);
      }
      else
        this.iconExit.gameObject.SetActive(false);
    }
    else
      this.version.gameObject.SetActive(false);
    float positionRightButton = this.positionRightButton;
    bool flag = false;
    for (int index = 0; index < this.buttonOrder.Count; ++index)
    {
      if (this.buttonOrder[index].activeSelf)
      {
        if ((Object) this.buttonOrder[index] == (Object) this.score.gameObject || (Object) this.buttonOrder[index] == (Object) this.madness.gameObject)
        {
          if (!flag)
            positionRightButton -= this.adjustmentForBigButtons * 0.5f;
          else
            positionRightButton -= this.adjustmentForBigButtons;
          flag = true;
        }
        else
          flag = false;
        this.buttonOrder[index].transform.localPosition = new Vector3(positionRightButton, this.buttonOrder[index].transform.localPosition.y, this.buttonOrder[index].transform.localPosition.z);
        positionRightButton -= this.distanceBetweenButton;
      }
    }
  }

  public void Hide()
  {
    if (!this.elements.gameObject.activeSelf)
      return;
    this.elements.gameObject.SetActive(false);
    this.ResetIndexController();
  }

  public bool IsActive() => this.elements.gameObject.activeSelf;

  public void SetMadness()
  {
    string str = string.Format(Texts.Instance.GetText("actNumber"), (object) AtOManager.Instance.GetActNumberForText());
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(str);
    stringBuilder.Append("<br><size=-.8><color=#EFA0FF>");
    int num = 0;
    if (!GameManager.Instance.IsObeliskChallenge())
      num = AtOManager.Instance.GetMadnessDifficulty();
    else if (!GameManager.Instance.IsWeeklyChallenge())
      num = AtOManager.Instance.GetObeliskMadness();
    bool flag = false;
    if (num > 0)
    {
      stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessNumber"), (object) num.ToString()));
      flag = true;
    }
    else if (GameManager.Instance.IsWeeklyChallenge())
    {
      stringBuilder.Append(AtOManager.Instance.GetWeeklyName(AtOManager.Instance.GetWeekly()));
      flag = true;
    }
    this.madnessText.text = stringBuilder.ToString();
    if ((bool) (Object) MatchManager.Instance || (bool) (Object) MapManager.Instance || (bool) (Object) TownManager.Instance || (bool) (Object) FinishRunManager.Instance)
    {
      this.madness.gameObject.SetActive(true);
      if (flag)
        this.madnessParticles.gameObject.SetActive(true);
      else
        this.madnessParticles.gameObject.SetActive(false);
    }
    else
      this.madness.gameObject.SetActive(false);
  }

  public void ShowScore(bool state)
  {
    this.score.gameObject.SetActive(state);
    if (!state)
      return;
    this.SetScore();
  }

  public void Resize()
  {
    this.transform.localScale = Globals.Instance.scaleV;
    this.transform.position = new Vector3((float) ((double) Globals.Instance.sizeW * 0.5 - 1.5 * (double) Globals.Instance.scale), (float) ((double) Globals.Instance.sizeH * 0.5 - 0.31999999284744263 * (double) Globals.Instance.scale), this.transform.position.z);
    this.position.localPosition = new Vector3((float) (-(double) Globals.Instance.sizeW * 0.5 + 1.3999999761581421 * (double) Globals.Instance.scale), -0.045f * Globals.Instance.scale, this.position.localPosition.z);
    this.version.transform.position = new Vector3((float) ((double) Globals.Instance.sizeW * 0.5 - 2.3499999046325684 * (double) Globals.Instance.scale), (float) (-(double) Globals.Instance.sizeH * 0.5 + 0.2199999988079071 * (double) Globals.Instance.scale), this.version.transform.position.z);
  }

  public void SetPositionText(string str = "")
  {
  }

  public void CantExitBecauseRewards()
  {
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.CantExitBecauseRewardsAction);
    AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("exitGameRewards"));
  }

  public void CantExitBecauseRewardsAction() => AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.CantExitBecauseRewardsAction);

  public void CantExitBecauseEvent()
  {
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.CantExitBecauseEventAction);
    AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("exitGameEvent"));
  }

  public void CantExitBecauseEventAction() => AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.CantExitBecauseEventAction);

  public void SetScore()
  {
    int score = AtOManager.Instance.CalculateScore();
    this.score.gameObject.SetActive(true);
    this.scoreText.text = Functions.ScoreFormat(score);
  }

  public void Exit()
  {
    if (GameManager.Instance.IsMaskActive() || AtOManager.Instance.saveLoadStatus)
      return;
    if ((Object) RewardsManager.Instance != (Object) null || (Object) LootManager.Instance != (Object) null)
      this.CantExitBecauseRewards();
    else if ((Object) EventManager.Instance != (Object) null)
      this.CantExitBecauseEvent();
    else if ((Object) CardPlayerManager.Instance != (Object) null && !CardPlayerManager.Instance.CanExit())
      this.CantExitBecauseEvent();
    else if ((Object) CardPlayerPairsManager.Instance != (Object) null && !CardPlayerPairsManager.Instance.CanExit())
    {
      this.CantExitBecauseEvent();
    }
    else
    {
      bool flag = false;
      if ((Object) MapManager.Instance != (Object) null || (Object) HeroSelectionManager.Instance != (Object) null || (Object) TownManager.Instance != (Object) null || (Object) LobbyManager.Instance != (Object) null || (Object) ChallengeSelectionManager.Instance != (Object) null || (Object) MatchManager.Instance != (Object) null)
        flag = true;
      if (!flag)
      {
        SceneStatic.LoadByName("MainMenu");
      }
      else
      {
        AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.ExitGameAction);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(Texts.Instance.GetText("exitGameConfirm"));
        stringBuilder.Append("<br><size=-2><color=#aaa>");
        if (!((Object) LobbyManager.Instance != (Object) null))
        {
          if ((Object) HeroSelectionManager.Instance != (Object) null)
            stringBuilder.Append(Texts.Instance.GetText("exitGameConfirmLoss"));
          else
            stringBuilder.Append(Texts.Instance.GetText("exitGameConfirmSave"));
        }
        stringBuilder.Append("</color></size>");
        AlertManager.Instance.AlertConfirmDouble(stringBuilder.ToString(), Texts.Instance.GetText("accept").ToUpper(), Texts.Instance.GetText("cancel").ToUpper());
        AlertManager.Instance.ShowDoorIcon();
      }
    }
  }

  public void ExitGameAction()
  {
    bool confirmAnswer = AlertManager.Instance.GetConfirmAnswer();
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.ExitGameAction);
    if (GameManager.Instance.IsMaskActive() || AtOManager.Instance.saveLoadStatus || !confirmAnswer)
      return;
    this.DoExit();
  }

  public void DoExit()
  {
    if (GameManager.Instance.IsMaskActive() || AtOManager.Instance.saveLoadStatus)
      return;
    if (!((Object) MatchManager.Instance != (Object) null))
    {
      if ((Object) TownManager.Instance != (Object) null)
        AtOManager.Instance.SaveGame();
      else if ((Object) TownManager.Instance != (Object) null)
        AtOManager.Instance.SaveGame();
    }
    if (GameManager.Instance.IsMultiplayer())
      NetworkManager.Instance.Disconnect();
    if ((Object) ChatManager.Instance != (Object) null)
      ChatManager.Instance.DisableChat();
    SceneStatic.LoadByName("MainMenu");
  }

  public void InputMoveController(bool goingRight)
  {
    bool flag = false;
    while (!flag)
    {
      if (!goingRight)
      {
        ++this._indexController;
        if (this._indexController > this.buttonOrder.Count - 1)
          this._indexController = 0;
      }
      else
      {
        --this._indexController;
        if (this._indexController < 0)
          this._indexController = this.buttonOrder.Count - 1;
      }
      if (this.buttonOrder[this._indexController].activeSelf)
        flag = true;
    }
    Mouse.current.WarpCursorPosition((Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.buttonOrder[this._indexController].transform.position));
  }

  private void ResetIndexController() => this._indexController = -1;
}
