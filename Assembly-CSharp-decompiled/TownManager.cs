// Decompiled with JetBrains decompiler
// Type: TownManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TownManager : MonoBehaviour
{
  public CharacterWindowUI characterWindow;
  public SideCharacters sideCharacters;
  public Transform townButtons;
  public Transform tutorialBanner;
  public TMP_Text tutorialBannerText;
  public TreasureRun[] treasureItems;
  public Transform treasureDescription;
  public Transform treasureButtons;
  public Transform treasureHeader;
  public TreasureRun[] treasureItemsCommunity;
  public Transform treasureDescriptionCommunity;
  public Transform treasureButtonsCommunity;
  public Transform treasureHeaderCommunity;
  public Transform treasureBannerDown;
  public TMP_Text treasureTitleCommunity;
  public TMP_Text treasureSubtitleCommunity;
  public BotonGeneric townReady;
  public Transform townUpgrades;
  public TownUpgradeWindow townUpgradeWindow;
  public Transform bgSenenthia;
  public Transform bgVelkarath;
  public Transform bgAquarfall;
  public Transform bgFaeborg;
  public Transform bgVoid;
  public Transform bgUlminin;
  public SpriteRenderer treeSR;
  private TreasureRun treasureTarget;
  private bool isThereTreasure;
  public Transform waitingMsg;
  public TMP_Text waitingMsgText;
  private bool statusReady;
  private Coroutine manualReadyCo;
  public Transform joinDivination;
  public TMP_Text joinDivinationText;
  public Transform ItemCreator;
  private bool treasureLock;
  public int LastUsedCharacter = -1;
  public TownBuilding buildingForge;
  public TownBuilding buildingAltar;
  public TownBuilding buildingChurch;
  public TownBuilding buildingCart;
  public TownBuilding buildingArmory;
  public Transform arrowForge;
  public Transform arrowAltar;
  public Transform arrowArmory;
  private int controllerHorizontalIndex = -1;
  private int controllerTreasureIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();

  public static TownManager Instance { get; private set; }

  private void Awake()
  {
    if ((UnityEngine.Object) GameManager.Instance == (UnityEngine.Object) null)
    {
      SceneStatic.LoadByName("Town");
    }
    else
    {
      if ((UnityEngine.Object) TownManager.Instance == (UnityEngine.Object) null)
        TownManager.Instance = this;
      else if ((UnityEngine.Object) TownManager.Instance != (UnityEngine.Object) this)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      GameManager.Instance.SetCamera();
      NetworkManager.Instance.StartStopQueue(true);
    }
  }

  private void Start()
  {
    this.Resize();
    this.ShowTownUpgrades(false, true);
    AudioManager.Instance.DoBSO("Town");
    AtOManager.Instance.ResetTownDivination();
    this.treasureButtonsCommunity.gameObject.SetActive(false);
    this.treasureHeaderCommunity.gameObject.SetActive(false);
    this.treasureDescriptionCommunity.gameObject.SetActive(false);
    this.waitingMsg.gameObject.SetActive(false);
    string townZoneId = AtOManager.Instance.GetTownZoneId();
    this.bgSenenthia.gameObject.SetActive(false);
    this.bgVelkarath.gameObject.SetActive(false);
    this.bgAquarfall.gameObject.SetActive(false);
    this.bgFaeborg.gameObject.SetActive(false);
    this.bgVoid.gameObject.SetActive(false);
    this.bgUlminin.gameObject.SetActive(false);
    switch (townZoneId)
    {
      case "Velkarath":
        this.bgVelkarath.gameObject.SetActive(true);
        this.treeSR.color = Functions.HexToColor("#E99F94");
        break;
      case "Aquarfall":
        this.bgAquarfall.gameObject.SetActive(true);
        break;
      case "Senenthia":
        this.bgSenenthia.gameObject.SetActive(true);
        break;
      case "Faeborg":
        this.bgFaeborg.gameObject.SetActive(true);
        break;
      case "Ulminin":
        this.bgUlminin.gameObject.SetActive(true);
        break;
      default:
        this.bgVoid.gameObject.SetActive(true);
        break;
    }
    AtOManager.Instance.SetPositionText(string.Format(Texts.Instance.GetText("townPlace"), (object) townZoneId));
    this.SetTownBuildings();
    if (GameManager.Instance.IsMultiplayer())
    {
      this.statusReady = false;
      this.townReady.color = Functions.HexToColor(Globals.Instance.ClassColor["warrior"]);
      this.townReady.SetColor();
      if (NetworkManager.Instance.IsMaster())
        NetworkManager.Instance.ClearAllPlayerManualReady();
    }
    AtOManager.Instance.SetCharInTown(true);
    if (!GameManager.Instance.IsMultiplayer() || GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
    {
      AtOManager.Instance.GenerateTownItemList();
      AtOManager.Instance.SaveGame();
    }
    AtOManager.Instance.NodeScore();
    GameManager.Instance.SceneLoaded();
    GameManager.Instance.ShowTutorialPopup("town", Vector3.zero, Vector3.zero);
    this.StartCoroutine(this.TutorialReward());
  }

  private IEnumerator TutorialReward()
  {
    while (GameManager.Instance.IsTutorialActive())
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    if (this.isThereTreasure)
      GameManager.Instance.ShowTutorialPopup("townReward", this.treasureButtons.position + new Vector3(0.6f, -0.5f, 0.0f), Vector3.zero);
  }

  public void Resize()
  {
    float num = 1f;
    this.tutorialBanner.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
    this.tutorialBanner.transform.position = new Vector3((float) ((double) Globals.Instance.sizeW * 0.5 - 2.4000000953674316 * (double) num * (double) Globals.Instance.multiplierX), (float) ((double) Globals.Instance.sizeH * 0.5 - 2.2000000476837158 * (double) num * (double) Globals.Instance.multiplierY), this.tutorialBanner.transform.position.z);
    this.treasureButtons.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
    this.treasureButtons.transform.position = new Vector3((float) ((double) Globals.Instance.sizeW * 0.5 - 2.4000000953674316 * (double) num * (double) Globals.Instance.multiplierX), (float) ((double) Globals.Instance.sizeH * 0.5 - 2.2000000476837158 * (double) num * (double) Globals.Instance.multiplierY), this.treasureButtons.transform.position.z);
    this.treasureButtonsCommunity.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
    this.treasureButtonsCommunity.transform.position = new Vector3((float) ((double) Globals.Instance.sizeW * 0.5 - 2.4000000953674316 * (double) num * (double) Globals.Instance.multiplierX), (float) ((double) Globals.Instance.sizeH * 0.5 - 6.1999998092651367 * (double) num * (double) Globals.Instance.multiplierY), this.treasureButtonsCommunity.transform.position.z);
    this.townReady.transform.position = new Vector3((float) ((double) Globals.Instance.sizeW * 0.5 - 1.6000000238418579 * (double) num * (double) Globals.Instance.multiplierX), (float) (-(double) Globals.Instance.sizeH * 0.5 + 0.89999997615814209 * (double) num * (double) Globals.Instance.multiplierY), this.townReady.transform.position.z);
    this.sideCharacters.Resize();
    this.characterWindow.Resize();
  }

  public void DisableReady()
  {
    if (!this.statusReady)
      return;
    this.Ready();
  }

  public void Ready()
  {
    if (!GameManager.Instance.IsMultiplayer())
    {
      if (AtOManager.Instance.TownTutorialStep > -1 && AtOManager.Instance.TownTutorialStep < 3)
        AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("tutorialTownNeedComplete"));
      else
        this.ExitTown();
    }
    else
    {
      if (this.manualReadyCo != null)
        this.StopCoroutine(this.manualReadyCo);
      this.statusReady = !this.statusReady;
      NetworkManager.Instance.SetManualReady(this.statusReady);
      if (this.statusReady)
      {
        this.townReady.color = Functions.HexToColor(Globals.Instance.ClassColor["scout"]);
        this.townReady.SetColor();
        this.townReady.SetText(Texts.Instance.GetText("waitingForPlayers"));
        if (!NetworkManager.Instance.IsMaster())
          return;
        this.manualReadyCo = this.StartCoroutine(this.CheckForAllManualReady());
      }
      else
      {
        this.townReady.color = Functions.HexToColor(Globals.Instance.ClassColor["warrior"]);
        this.townReady.SetColor();
        this.townReady.SetText(Texts.Instance.GetText("ready"));
      }
    }
  }

  private IEnumerator CheckForAllManualReady()
  {
    bool check = true;
    while (check)
    {
      if (!NetworkManager.Instance.AllPlayersManualReady())
        yield return (object) Globals.Instance.WaitForSeconds(1f);
      else
        check = false;
    }
    this.ExitTown();
  }

  public void SetWaitingPlayersText(string msg)
  {
    if (msg != "")
    {
      this.waitingMsg.gameObject.SetActive(true);
      this.waitingMsgText.text = msg;
    }
    else
      this.waitingMsg.gameObject.SetActive(false);
  }

  private void ExitTown() => AtOManager.Instance.LaunchMap();

  public void ShowCharacterWindow(string type = "", int heroIndex = -1)
  {
    this.ShowButtons(false);
    this.characterWindow.Show(type, heroIndex);
  }

  public void HideCharacterWindow()
  {
    if ((UnityEngine.Object) CardCraftManager.Instance != (UnityEngine.Object) null && CardCraftManager.Instance.craftType == 3)
      return;
    this.ShowButtons(true);
  }

  private void CreateTutorialBanner()
  {
    bool flag1 = false;
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.Append("<line-height=110%><br>-<br></line-height>");
    StringBuilder stringBuilder2 = new StringBuilder();
    int townTutorialStep = AtOManager.Instance.TownTutorialStep;
    if (townTutorialStep == 0)
    {
      flag1 = true;
      stringBuilder2.Append("<color=");
      stringBuilder2.Append("#461518");
      stringBuilder2.Append(">");
      stringBuilder2.Append("<size=+.5><sprite name=questBegin></size> <size=+.3>");
    }
    stringBuilder2.Append(string.Format(Texts.Instance.GetText("townTutorialStep0"), (object) Globals.Instance.GetCardData("fireball").CardName, (object) "Evelyn"));
    if (flag1)
      stringBuilder2.Append("</size></color>");
    bool flag2 = false;
    stringBuilder2.Append(stringBuilder1.ToString());
    if (townTutorialStep == 1)
    {
      flag2 = true;
      stringBuilder2.Append("<color=");
      stringBuilder2.Append("#461518");
      stringBuilder2.Append(">");
      stringBuilder2.Append("<size=+.5><sprite name=questBegin></size> <size=+.3>");
    }
    stringBuilder2.Append(string.Format(Texts.Instance.GetText("townTutorialStep1"), (object) Globals.Instance.GetCardData("faststrike").CardName, (object) "Magnus"));
    if (flag2)
      stringBuilder2.Append("</size></color>");
    bool flag3 = false;
    stringBuilder2.Append(stringBuilder1.ToString());
    if (townTutorialStep == 2)
    {
      flag3 = true;
      stringBuilder2.Append("<color=");
      stringBuilder2.Append("#461518");
      stringBuilder2.Append(">");
      stringBuilder2.Append("<size=+.5><sprite name=questBegin></size> <size=+.3>");
    }
    stringBuilder2.Append(string.Format(Texts.Instance.GetText("townTutorialStep2"), (object) Globals.Instance.GetCardData("spyglass").CardName, (object) "Andrin"));
    if (flag3)
      stringBuilder2.Append("</size></color>");
    this.tutorialBannerText.text = stringBuilder2.ToString();
  }

  public void ShowButtons(bool state)
  {
    this.townButtons.gameObject.SetActive(state);
    this.treasureButtons.gameObject.SetActive(state);
    this.arrowForge.gameObject.SetActive(false);
    this.arrowAltar.gameObject.SetActive(false);
    this.arrowArmory.gameObject.SetActive(false);
    bool flag = false;
    if (state && AtOManager.Instance.TownTutorialStep >= 0 && AtOManager.Instance.TownTutorialStep < 3)
    {
      this.tutorialBanner.gameObject.SetActive(true);
      this.CreateTutorialBanner();
      if (AtOManager.Instance.TownTutorialStep == 0)
        this.arrowForge.gameObject.SetActive(true);
      else if (AtOManager.Instance.TownTutorialStep == 1)
        this.arrowAltar.gameObject.SetActive(true);
      else
        this.arrowArmory.gameObject.SetActive(true);
      flag = true;
    }
    else
      this.tutorialBanner.gameObject.SetActive(false);
    if (flag || AtOManager.Instance.GetNgPlus() > 2)
    {
      this.treasureButtons.gameObject.SetActive(false);
      this.treasureButtonsCommunity.gameObject.SetActive(false);
    }
    else if (state)
      this.StartCoroutine(this.SetTreasures());
    else
      this.treasureButtons.transform.position = new Vector3(this.treasureButtons.transform.position.x, this.treasureButtons.transform.position.y, -100f);
  }

  public void ShowJoinDivination(bool state = true)
  {
    if (state)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<color=");
      stringBuilder.Append(NetworkManager.Instance.GetColorFromNick(AtOManager.Instance.townDivinationCreator));
      stringBuilder.Append(">");
      stringBuilder.Append(AtOManager.Instance.townDivinationCreator);
      stringBuilder.Append("</color>");
      this.joinDivinationText.text = string.Format(Texts.Instance.GetText("divinationRoundJoin"), (object) stringBuilder.ToString());
    }
    this.joinDivination.gameObject.SetActive(state);
  }

  public void ShowDeck(int auxInt) => this.characterWindow.Show("deck", auxInt);

  private IEnumerator SetTreasures()
  {
    int index = 0;
    this.isThereTreasure = false;
    this.LockTreasures(false);
    this.treasureButtons.transform.position = new Vector3(this.treasureButtons.transform.position.x, this.treasureButtons.transform.position.y, 0.0f);
    this.treasureButtons.gameObject.SetActive(false);
    this.treasureButtons.gameObject.SetActive(true);
    this.treasureDescription.gameObject.SetActive(false);
    this.treasureItems[0].Hide();
    this.treasureItems[1].Hide();
    this.treasureItems[2].Hide();
    this.treasureBannerDown.localPosition = new Vector3(this.treasureBannerDown.localPosition.x, 0.8f, this.treasureBannerDown.localPosition.z);
    if (PlayerManager.Instance.PlayerRuns != null)
    {
      for (int i = PlayerManager.Instance.PlayerRuns.Count - 1; i >= 0; --i)
      {
        if (i >= 0)
        {
          PlayerRun _playerRun = JsonUtility.FromJson<PlayerRun>(PlayerManager.Instance.PlayerRuns[i]);
          if (_playerRun.MadnessLevel <= 2 && !_playerRun.ObeliskChallenge && !_playerRun.SandboxEnabled && (PlayerManager.Instance.TreasuresClaimed == null || !PlayerManager.Instance.IsTreasureClaimed(_playerRun.Id)))
          {
            if (index <= 2)
            {
              if (_playerRun.GoldGained > 0 || _playerRun.DustGained > 0)
              {
                this.isThereTreasure = true;
                if (!((UnityEngine.Object) this.treasureItems[index] == (UnityEngine.Object) null))
                {
                  this.treasureItems[index].SetTreasure(_playerRun, index);
                  yield return (object) Globals.Instance.WaitForSeconds(0.1f);
                  ++index;
                  if (!this.treasureButtons.gameObject.activeSelf)
                  {
                    this.treasureButtons.gameObject.SetActive(true);
                    this.treasureHeader.gameObject.SetActive(true);
                  }
                  switch (index)
                  {
                    case 1:
                      this.treasureBannerDown.localPosition = new Vector3(this.treasureBannerDown.localPosition.x, 0.8f, this.treasureBannerDown.localPosition.z);
                      continue;
                    case 2:
                      this.treasureBannerDown.localPosition = new Vector3(this.treasureBannerDown.localPosition.x, -0.56f, this.treasureBannerDown.localPosition.z);
                      continue;
                    case 3:
                      this.treasureBannerDown.localPosition = new Vector3(this.treasureBannerDown.localPosition.x, -1.86f, this.treasureBannerDown.localPosition.z);
                      continue;
                    default:
                      continue;
                  }
                }
              }
            }
            else
              PlayerManager.Instance.ClaimTreasure(_playerRun.Id, false);
          }
        }
      }
    }
    if (index == 0)
      this.ShowTreasureDescription();
    this.treasureItemsCommunity[0].Hide();
    this.treasureItemsCommunity[1].Hide();
    this.treasureItemsCommunity[2].Hide();
    if (GameManager.Instance.communityRewards != null && GameManager.Instance.communityRewards.Count > 0 && !Functions.Expired(GameManager.Instance.communityRewardsExpire))
    {
      int _index = 0;
      for (int index1 = GameManager.Instance.communityRewards.Count - 1; index1 >= 0; --index1)
      {
        string[] strArray = GameManager.Instance.communityRewards[index1].Split(',', StringSplitOptions.None);
        if (strArray == null || strArray.Length != 3)
        {
          yield break;
        }
        else
        {
          string str = strArray[0].Trim();
          int _gold = int.Parse(strArray[1].Trim());
          int _dust = int.Parse(strArray[2].Trim());
          if (_gold < 0 && _dust < 0)
            yield break;
          else if (PlayerManager.Instance.TreasuresClaimed == null || !PlayerManager.Instance.IsTreasureClaimed(str))
          {
            this.treasureItemsCommunity[_index].SetTreasureCommunity(str, _gold, _dust, _index);
            ++_index;
          }
        }
      }
      if (_index > 0)
      {
        this.treasureButtonsCommunity.gameObject.SetActive(true);
        this.treasureHeaderCommunity.gameObject.SetActive(true);
        this.treasureDescriptionCommunity.gameObject.SetActive(true);
      }
      else
      {
        this.treasureButtonsCommunity.gameObject.SetActive(false);
        this.treasureHeaderCommunity.gameObject.SetActive(false);
        this.treasureDescriptionCommunity.gameObject.SetActive(false);
      }
    }
    else
    {
      this.treasureButtonsCommunity.gameObject.SetActive(false);
      this.treasureHeaderCommunity.gameObject.SetActive(false);
      this.treasureDescriptionCommunity.gameObject.SetActive(false);
    }
  }

  private void ShowTreasureDescription()
  {
    this.treasureButtons.gameObject.SetActive(false);
    this.treasureDescription.gameObject.SetActive(false);
    this.treasureHeader.gameObject.SetActive(false);
  }

  public void ClaimTreasureCommunity(string _treasureId)
  {
    if (this.treasureLock)
      return;
    for (int index = 0; index < 3; ++index)
    {
      if ((UnityEngine.Object) this.treasureItemsCommunity[index] != (UnityEngine.Object) null && this.treasureItemsCommunity[index].treasureId == _treasureId)
      {
        this.treasureTarget = this.treasureItemsCommunity[index];
        AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.OpenTreasureCommunity);
        AlertManager.Instance.AlertConfirmDouble(Texts.Instance.GetText("wantToClaimTreasure"));
        break;
      }
    }
  }

  private void OpenTreasureCommunity()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.OpenTreasureCommunity);
    if (!AlertManager.Instance.GetConfirmAnswer())
      return;
    this.LockTreasures(true);
    PlayerManager.Instance.ClaimTreasure(this.treasureTarget.treasureId);
    this.treasureTarget.ClaimCommunity();
  }

  public void ClaimTreasure(string _treasureId)
  {
    if (this.treasureLock)
      return;
    for (int index = 0; index < 3; ++index)
    {
      this.treasureButtonsCommunity.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.treasureItems[index] != (UnityEngine.Object) null && this.treasureItems[index].treasureId == _treasureId)
      {
        this.treasureTarget = this.treasureItems[index];
        AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.OpenTreasure);
        AlertManager.Instance.AlertConfirmDouble(Texts.Instance.GetText("wantToClaimTreasure"));
        break;
      }
    }
  }

  private void OpenTreasure()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.OpenTreasure);
    if (!AlertManager.Instance.GetConfirmAnswer())
      return;
    this.LockTreasures(true);
    PlayerManager.Instance.ClaimTreasure(this.treasureTarget.treasureId, false);
    this.treasureTarget.Claim();
  }

  public void LockTreasures(bool state) => this.treasureLock = state;

  public bool AreTreasuresLocked() => this.treasureLock;

  public void MoveTreasuresUp(string _treasureFromId, bool _isCommunity)
  {
    int num1 = -1;
    for (int index = 0; index < 3; ++index)
    {
      if (!_isCommunity)
      {
        if ((UnityEngine.Object) this.treasureItems[index] != (UnityEngine.Object) null && this.treasureItems[index].treasureId == _treasureFromId)
        {
          num1 = index;
          break;
        }
      }
      else if ((UnityEngine.Object) this.treasureItemsCommunity[index] != (UnityEngine.Object) null && this.treasureItemsCommunity[index].treasureId == _treasureFromId)
      {
        num1 = index;
        break;
      }
    }
    TreasureRun treasureRun = (TreasureRun) null;
    if (num1 > -1)
    {
      for (int index = 0; index < 3; ++index)
      {
        if (!_isCommunity)
        {
          if (index != num1 && index > num1 && (UnityEngine.Object) this.treasureItems[index] != (UnityEngine.Object) null && this.treasureItems[index].treasureId != "" && this.treasureItems[index].gameObject.activeSelf)
          {
            this.treasureItems[index].MoveUp();
            treasureRun = this.treasureItems[index];
          }
        }
        else if (index != num1 && index > num1 && (UnityEngine.Object) this.treasureItemsCommunity[index] != (UnityEngine.Object) null && this.treasureItemsCommunity[index].treasureId != "" && this.treasureItemsCommunity[index].gameObject.activeSelf)
          this.treasureItemsCommunity[index].MoveUp();
      }
    }
    bool flag = true;
    int num2 = 0;
    for (int index = 0; index < 3; ++index)
    {
      if (!_isCommunity)
      {
        if ((UnityEngine.Object) this.treasureItems[index] != (UnityEngine.Object) null && this.treasureItems[index].treasureId != "" && !this.treasureItems[index].claimed)
        {
          flag = false;
          ++num2;
        }
      }
      else if ((UnityEngine.Object) this.treasureItemsCommunity[index] != (UnityEngine.Object) null && this.treasureItemsCommunity[index].treasureId != "" && !this.treasureItemsCommunity[index].claimed)
        flag = false;
    }
    if (flag)
    {
      if (!_isCommunity)
      {
        this.treasureButtons.gameObject.SetActive(false);
        this.treasureHeader.gameObject.SetActive(false);
      }
      else
        this.treasureHeaderCommunity.gameObject.SetActive(false);
    }
    else
    {
      switch (num2)
      {
        case 1:
          this.treasureBannerDown.localPosition = new Vector3(this.treasureBannerDown.localPosition.x, 1.1f, this.treasureBannerDown.localPosition.z);
          if (!((UnityEngine.Object) treasureRun != (UnityEngine.Object) null) || !((UnityEngine.Object) treasureRun.separator != (UnityEngine.Object) null))
            break;
          treasureRun.separator.gameObject.SetActive(false);
          break;
        case 2:
          this.treasureBannerDown.localPosition = new Vector3(this.treasureBannerDown.localPosition.x, -0.26f, this.treasureBannerDown.localPosition.z);
          break;
        case 3:
          this.treasureBannerDown.localPosition = new Vector3(this.treasureBannerDown.localPosition.x, -1.56f, this.treasureBannerDown.localPosition.z);
          break;
        default:
          this.ShowTreasureDescription();
          break;
      }
    }
  }

  public void SellSupply() => this.townUpgradeWindow.ShowSellSupply(true);

  public void SellSupplyAction() => this.townUpgradeWindow.SellSupplyAction();

  public void CloseSupply() => this.townUpgradeWindow.ShowSellSupply(false);

  public void ModifySupplyQuantity(int quantity) => this.townUpgradeWindow.ModifySupplyQuantity(quantity);

  public void ShowTownUpgrades(bool state, bool forceIt = false)
  {
    if (this.townUpgradeWindow.IsActive() == state && !forceIt)
      return;
    this.townUpgradeWindow.Show(state);
    if (state)
    {
      this.sideCharacters.gameObject.SetActive(false);
    }
    else
    {
      this.sideCharacters.gameObject.SetActive(true);
      this.sideCharacters.Show();
    }
  }

  public void RefreshTownUpgrades() => this.townUpgradeWindow.Refresh();

  public string GetUpgradeButtonId(int column, int row)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("townUpgrade_");
    stringBuilder.Append(column);
    stringBuilder.Append("_");
    stringBuilder.Append(row);
    return stringBuilder.ToString();
  }

  public void SetTownBuildings()
  {
    int[] numArray = new int[5];
    for (int index = 0; index < 5; ++index)
      numArray[index] = 0;
    for (int index = 0; index < this.townUpgradeWindow.botonSupply.Count; ++index)
    {
      if (PlayerManager.Instance.PlayerHaveSupply(this.GetUpgradeButtonId(Mathf.CeilToInt((float) (index / 6)) + 1, Mathf.CeilToInt((float) (index % 6)) + 1)))
      {
        if (index >= 0 && index < 6)
        {
          if (index >= 2 && index < 4)
            numArray[0] = 1;
          else if (index >= 4)
            numArray[0] = 2;
        }
        else if (index >= 6 && index < 12)
        {
          if (index >= 8 && index < 10)
            numArray[1] = 1;
          else if (index >= 10)
            numArray[1] = 2;
        }
        else if (index >= 12 && index < 18)
        {
          if (index >= 14 && index < 16)
            numArray[2] = 1;
          else if (index >= 16)
            numArray[2] = 2;
        }
        else if (index >= 18 && index < 24)
        {
          if (index >= 20 && index < 22)
            numArray[3] = 1;
          else if (index >= 22)
            numArray[3] = 2;
        }
        else if (index >= 24 && index < 30)
        {
          if (index >= 26 && index < 28)
            numArray[4] = 1;
          else if (index >= 28)
            numArray[4] = 2;
        }
      }
    }
    this.buildingForge.Init(numArray[0]);
    this.buildingAltar.Init(numArray[1]);
    this.buildingChurch.Init(numArray[2]);
    this.buildingCart.Init(numArray[3]);
    this.buildingArmory.Init(numArray[4]);
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    bool shoulderLeft = false,
    bool shoulderRight = false)
  {
    if (this.townUpgradeWindow.IsActive())
    {
      this.townUpgradeWindow.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    }
    else
    {
      this._controllerList.Clear();
      this._controllerList.Add(this.buildingForge.transform);
      this._controllerList.Add(this.buildingChurch.transform);
      this._controllerList.Add(this.buildingAltar.transform);
      this._controllerList.Add(this.buildingCart.transform);
      this._controllerList.Add(this.buildingArmory.transform);
      this._controllerList.Add(this.townReady.transform);
      this._controllerList.Add(this.townUpgrades.transform);
      this.controllerTreasureIndex = -1;
      for (int index = 0; index < this.treasureItems.Length; ++index)
      {
        if (Functions.TransformIsVisible(this.treasureItems[index].transform))
        {
          this._controllerList.Add(this.treasureItems[index].transform);
          this.controllerTreasureIndex = this._controllerList.Count - 1;
        }
      }
      for (int index = 0; index < this.sideCharacters.transform.childCount; ++index)
      {
        if (Functions.TransformIsVisible(this.sideCharacters.transform.GetChild(index).transform))
          this._controllerList.Add(this.sideCharacters.transform.GetChild(index).transform);
      }
      if (Functions.TransformIsVisible(PlayerUIManager.Instance.giveGold))
        this._controllerList.Add(PlayerUIManager.Instance.giveGold);
      this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
      this.controllerHorizontalIndex = (UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] == (UnityEngine.Object) this.townUpgrades.transform & goingRight || (UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] == (UnityEngine.Object) this.buildingArmory.transform & goingUp ? this.controllerTreasureIndex : Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
      if (!((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
        return;
      this.warpPosition = this.controllerHorizontalIndex == 1 ? (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position - new Vector3(0.0f, 2f, 0.0f)) : (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
      Mouse.current.WarpCursorPosition(this.warpPosition);
    }
  }

  public void ControllerMoveBlock(bool _isRight) => this.characterWindow.IsActive();
}
