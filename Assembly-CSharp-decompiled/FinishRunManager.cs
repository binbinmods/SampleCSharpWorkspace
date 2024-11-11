// Decompiled with JetBrains decompiler
// Type: FinishRunManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinishRunManager : MonoBehaviour
{
  private int goldSummary;
  private int dustSummary;
  public Transform flag;
  public BotonGeneric mainMenuButton;
  public List<TMP_Text> titlesTexts;
  public TMP_Text subHeader;
  public TMP_Text placesText;
  public TMP_Text placesTextGold;
  public TMP_Text deathText;
  public TMP_Text deathTextGold;
  public TMP_Text deathsText;
  public TMP_Text deathsTextGold;
  public TMP_Text experienceText;
  public TMP_Text experienceTextGold;
  public TMP_Text bossesText;
  public TMP_Text bossesTextGold;
  public TMP_Text corruptionsText;
  public TMP_Text corruptionsTextGold;
  public Transform completedBlock;
  public TMP_Text completedText;
  public TMP_Text completedTextGold;
  public TMP_Text retentionText;
  public TMP_Text retentionTextGold;
  public TMP_Text finalScore;
  public TMP_Text playedTimeText;
  public TMP_Text finalScoreMadness;
  public TMP_Text gameScoreSub;
  public TMP_Text gameScoreTextGold;
  public TMP_Text totalTextGold;
  public TMP_Text gameReward;
  public TMP_Text mpBonus;
  public Transform bestScore;
  public Transform rewardsT;
  public Transform retentionTransform;
  public Transform rewardGroup;
  public SpriteRenderer spr0;
  public FinishProgression fp0;
  public SpriteRenderer spr1;
  public FinishProgression fp1;
  public SpriteRenderer spr2;
  public FinishProgression fp2;
  public SpriteRenderer spr3;
  public FinishProgression fp3;
  public CharacterWindowUI characterWindow;
  private bool[] lockedSate = new bool[4];
  private List<string> unlockedCards = new List<string>();
  private Coroutine finishCo;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();

  public static FinishRunManager Instance { get; private set; }

  private void Awake()
  {
    if ((UnityEngine.Object) GameManager.Instance == (UnityEngine.Object) null)
    {
      SceneStatic.LoadByName("FinishRun");
    }
    else
    {
      if ((UnityEngine.Object) FinishRunManager.Instance == (UnityEngine.Object) null)
        FinishRunManager.Instance = this;
      else if ((UnityEngine.Object) FinishRunManager.Instance != (UnityEngine.Object) this)
        UnityEngine.Object.Destroy((UnityEngine.Object) this);
      GameManager.Instance.SetCamera();
      this.rewardsT.gameObject.SetActive(false);
      this.bestScore.gameObject.SetActive(false);
      NetworkManager.Instance.StartStopQueue(true);
    }
  }

  private void Start()
  {
    if (AtOManager.Instance.currentMapNode == null || AtOManager.Instance.currentMapNode == "")
      SceneStatic.LoadByName("MainMenu");
    else
      this.StartCoroutine(this.StartCo());
  }

  private IEnumerator StartCo()
  {
    this.mainMenuButton.Disable();
    this.CalculateFinishRunReward();
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
      AtOManager.Instance.RemoveSave();
    GameManager.Instance.SceneLoaded();
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      if (PlayerManager.Instance.UnlockedCardsByGame.ContainsKey(AtOManager.Instance.GetGameId()))
      {
        this.unlockedCards = PlayerManager.Instance.UnlockedCardsByGame[AtOManager.Instance.GetGameId()];
        if (this.unlockedCards != null && this.unlockedCards.Count > 0)
        {
          this.characterWindow.ShowUnlockedCards(this.unlockedCards);
          yield return (object) Globals.Instance.WaitForSeconds(0.15f);
        }
      }
      while (this.characterWindow.IsActive())
        yield return (object) Globals.Instance.WaitForSeconds(0.05f);
    }
    yield return (object) Globals.Instance.WaitForSeconds(1f);
    this.mainMenuButton.Enable();
  }

  private string GoldDust(int gold, int dust)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(gold);
    stringBuilder.Append(" <sprite name=gold>  ");
    stringBuilder.Append(dust);
    stringBuilder.Append(" <sprite name=dust>");
    return stringBuilder.ToString();
  }

  private void CalculateFinishRunReward()
  {
    int townTier = AtOManager.Instance.GetTownTier();
    string str1 = "";
    if (AtOManager.Instance.currentMapNode != "" && (UnityEngine.Object) Globals.Instance.GetNodeData(AtOManager.Instance.currentMapNode) != (UnityEngine.Object) null)
      str1 = Globals.Instance.GetNodeData(AtOManager.Instance.currentMapNode).NodeName;
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.Append("<color=#FFFFFF>");
    stringBuilder1.Append(Texts.Instance.GetText("questEndsIn"));
    stringBuilder1.Append("</color>\n<size=+3>");
    if (str1 != "")
    {
      stringBuilder1.Append(str1);
      stringBuilder1.Append("</size>");
      stringBuilder1.Append("<br>");
    }
    string str2;
    switch (townTier)
    {
      case 0:
        str2 = "I";
        break;
      case 1:
        str2 = "II";
        break;
      case 2:
        str2 = "III";
        break;
      default:
        str2 = "IV";
        break;
    }
    string str3 = string.Format(Texts.Instance.GetText("actNumber"), (object) str2);
    stringBuilder1.Append(str3);
    this.subHeader.text = stringBuilder1.ToString();
    this.goldSummary = 0;
    this.dustSummary = 0;
    Hero[] heroArray1 = new Hero[4];
    ZoneData zoneData = (ZoneData) null;
    if (AtOManager.Instance.currentMapNode != "" && (UnityEngine.Object) Globals.Instance.GetNodeData(AtOManager.Instance.currentMapNode) != (UnityEngine.Object) null)
      zoneData = Globals.Instance.GetNodeData(AtOManager.Instance.currentMapNode).NodeZone;
    Hero[] heroArray2 = !((UnityEngine.Object) zoneData != (UnityEngine.Object) null) || zoneData.ChangeTeamOnEntrance ? AtOManager.Instance.GetTeamBackup() : AtOManager.Instance.GetTeam();
    int num1 = 0;
    if (heroArray2 != null)
    {
      for (int index = 0; index < heroArray2.Length; ++index)
      {
        if (heroArray2[index] != null && (heroArray2[index].Owner == NetworkManager.Instance.GetPlayerNick() || heroArray2[index].Owner == "" || heroArray2[index].Owner == null))
          ++num1;
      }
    }
    int num2 = 0;
    for (int index = 0; index < AtOManager.Instance.mapVisitedNodes.Count; ++index)
    {
      if ((UnityEngine.Object) Globals.Instance.GetNodeData(AtOManager.Instance.mapVisitedNodes[index]) != (UnityEngine.Object) null && (UnityEngine.Object) Globals.Instance.GetNodeData(AtOManager.Instance.mapVisitedNodes[index]).NodeZone != (UnityEngine.Object) null && !Globals.Instance.GetNodeData(AtOManager.Instance.mapVisitedNodes[index]).NodeZone.DisableExperienceOnThisZone)
        ++num2;
    }
    int num3 = num2 - 2;
    if (!GameManager.Instance.IsObeliskChallenge())
      --num3;
    if (num3 < 0)
      num3 = 0;
    int num4 = num3 * 36;
    this.placesText.text = num3.ToString();
    this.placesTextGold.text = num4.ToString();
    int num5 = AtOManager.Instance.combatExpertise;
    if (num5 < 0)
      num5 = 0;
    int num6 = num5 * 13;
    this.deathText.text = num5.ToString();
    this.deathTextGold.text = num6.ToString();
    int num7 = 0;
    int num8 = 0;
    for (int index = 0; index < 4; ++index)
    {
      num8 += heroArray2[index].Experience;
      num7 += heroArray2[index].TotalDeaths;
    }
    int num9 = -num7 * 100;
    this.deathsText.text = num7.ToString();
    this.deathsTextGold.text = num9.ToString();
    int num10 = Functions.FuncRoundToInt((float) num8 * 0.5f);
    this.experienceText.text = num8.ToString();
    this.experienceTextGold.text = num10.ToString();
    int bossesKilled = AtOManager.Instance.bossesKilled;
    this.bossesText.text = bossesKilled.ToString();
    int num11 = bossesKilled * 80;
    this.bossesTextGold.text = num11.ToString();
    this.corruptionsText.text = (AtOManager.Instance.corruptionCommonCompleted + AtOManager.Instance.corruptionUncommonCompleted + AtOManager.Instance.corruptionRareCompleted + AtOManager.Instance.corruptionEpicCompleted).ToString();
    int num12 = AtOManager.Instance.corruptionCommonCompleted * 40 + AtOManager.Instance.corruptionUncommonCompleted * 80 + AtOManager.Instance.corruptionRareCompleted * 130 + AtOManager.Instance.corruptionEpicCompleted * 200;
    this.corruptionsTextGold.text = num12.ToString();
    int _quantity = 0;
    if (AtOManager.Instance.IsAdventureCompleted())
    {
      this.completedBlock.gameObject.SetActive(true);
      _quantity = 500;
      this.completedTextGold.text = _quantity.ToString();
    }
    else
      this.completedBlock.gameObject.SetActive(false);
    int num13 = num4 + num6 + num9 + num10 + num11 + num12 + _quantity;
    if (num13 < 0)
      num13 = 0;
    int level = 0;
    if (!GameManager.Instance.IsObeliskChallenge())
      level = AtOManager.Instance.GetMadnessDifficulty();
    else if (!GameManager.Instance.IsWeeklyChallenge())
      level = AtOManager.Instance.GetObeliskMadness();
    int madnessScoreMultiplier = Functions.GetMadnessScoreMultiplier(level, !GameManager.Instance.IsObeliskChallenge());
    if (level > 0)
    {
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append("+");
      stringBuilder2.Append(madnessScoreMultiplier);
      stringBuilder2.Append("% <size=-.5>");
      stringBuilder2.Append(Texts.Instance.GetText("madness"));
      stringBuilder2.Append("</size>");
      this.finalScoreMadness.text = stringBuilder2.ToString();
    }
    else
      this.finalScoreMadness.text = "";
    int score = num13 + Functions.FuncRoundToInt((float) (num13 * madnessScoreMultiplier / 100));
    this.finalScore.text = score.ToString();
    bool flag = true;
    if (SandboxManager.Instance.IsEnabled())
      flag = false;
    else if (GameManager.Instance.IsMultiplayer())
    {
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      for (int index = 0; index < 4; ++index)
      {
        if (heroArray2[index] != null && !((UnityEngine.Object) heroArray2[index].HeroData == (UnityEngine.Object) null))
        {
          if (heroArray2[index].OwnerOriginal != null)
          {
            string lower = heroArray2[index].OwnerOriginal.ToLower();
            if (!stringList1.Contains(lower))
              stringList1.Add(lower);
          }
          else
            break;
        }
      }
      for (int index = 0; index < 4; ++index)
      {
        if (heroArray2[index] != null && !((UnityEngine.Object) heroArray2[index].HeroData == (UnityEngine.Object) null))
        {
          string playerNickReal = NetworkManager.Instance.GetPlayerNickReal(heroArray2[index].Owner);
          if (playerNickReal != "")
          {
            string lower = playerNickReal.ToLower();
            if (!stringList2.Contains(lower))
              stringList2.Add(lower);
          }
        }
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
    }
    if (flag)
    {
      if (!GameManager.Instance.IsObeliskChallenge())
      {
        if (score > PlayerManager.Instance.BestScore)
        {
          this.bestScore.gameObject.SetActive(true);
          PlayerManager.Instance.SetBestScore(score);
        }
        if ((score <= 100000 || level >= 16) && (score <= 130000 || level < 16))
          SteamManager.Instance.SetScore(score, !GameManager.Instance.IsMultiplayer());
      }
      else if (score <= 100000)
      {
        if (GameManager.Instance.IsWeeklyChallenge())
        {
          string steamName = SteamManager.Instance.steamName;
          string nickgroup = "";
          if (GameManager.Instance.IsMultiplayer())
          {
            foreach (Player player in NetworkManager.Instance.PlayerList)
              nickgroup = nickgroup + player.NickName + "-";
          }
          int weekly = AtOManager.Instance.GetWeekly();
          SteamManager.Instance.SetWeeklyScore(score, weekly, steamName, nickgroup, !GameManager.Instance.IsMultiplayer());
          ChallengeData weeklyData = Globals.Instance.GetWeeklyData(weekly);
          if (weeklyData.IdSteam != "")
            SteamManager.Instance.SetStatInt(weeklyData.IdSteam, 1);
        }
        else
          SteamManager.Instance.SetObeliskScore(score, !GameManager.Instance.IsMultiplayer());
      }
    }
    else if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("DON'T SEND Steam Score", "trace");
    this.playedTimeText.text = string.Format(Texts.Instance.GetText("timePlayed"), (object) Functions.FloatToTime(AtOManager.Instance.playedTime));
    if (num1 == 4)
    {
      this.goldSummary = Functions.FuncRoundToInt((float) num13 * 0.6f);
      switch (townTier)
      {
        case 0:
          this.dustSummary = Functions.FuncRoundToInt((float) num13 * 1f);
          break;
        case 1:
          this.goldSummary = Functions.FuncRoundToInt((float) num13 * 0.5f);
          this.dustSummary = Functions.FuncRoundToInt((float) num13 * 0.75f);
          break;
        case 2:
          this.goldSummary = Functions.FuncRoundToInt((float) num13 * 0.5f);
          this.dustSummary = Functions.FuncRoundToInt((float) num13 * 0.7f);
          break;
        case 3:
          this.goldSummary = Functions.FuncRoundToInt((float) num13 * 0.5f);
          this.dustSummary = Functions.FuncRoundToInt((float) num13 * 0.6f);
          break;
        default:
          this.dustSummary = num13;
          break;
      }
    }
    else
    {
      this.goldSummary = num1 * Functions.FuncRoundToInt((float) num13 * 0.3f);
      this.dustSummary = num1 * Functions.FuncRoundToInt((float) num13 * 0.4f);
      switch (townTier)
      {
        case 1:
          this.goldSummary = num1 * Functions.FuncRoundToInt((float) num13 * 0.25f);
          this.dustSummary = num1 * Functions.FuncRoundToInt((float) num13 * 0.35f);
          break;
        case 2:
          this.goldSummary = num1 * Functions.FuncRoundToInt((float) num13 * 0.2f);
          this.dustSummary = num1 * Functions.FuncRoundToInt((float) num13 * 0.25f);
          break;
        case 3:
          this.goldSummary = num1 * Functions.FuncRoundToInt((float) num13 * 0.2f);
          this.dustSummary = num1 * Functions.FuncRoundToInt((float) num13 * 0.25f);
          break;
      }
    }
    int num14 = 0;
    string str4 = "";
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      num14 = AtOManager.Instance.GetNgPlus();
      str4 = AtOManager.Instance.GetMadnessCorruptors();
    }
    if (num14 > 0 && num14 < 3)
    {
      this.goldSummary *= 2;
      this.dustSummary *= 2;
      StringBuilder stringBuilder3 = new StringBuilder();
      stringBuilder3.Append("+100%<br><size=-.5>");
      stringBuilder3.Append(Texts.Instance.GetText("madness"));
      stringBuilder3.Append("</size>");
      this.gameScoreSub.text = stringBuilder3.ToString();
    }
    else
      this.gameScoreSub.text = "";
    this.gameScoreTextGold.text = this.GoldDust(this.goldSummary, this.dustSummary);
    float num15 = 5f;
    if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_6_5"))
      num15 = 30f;
    else if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_6_3"))
      num15 = 20f;
    else if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_6_1"))
      num15 = 10f;
    int num16 = 0;
    for (int index = 0; index < 4; ++index)
      num16 += heroArray2[index].GetItemFinalRewardRetentionModification();
    float num17 = num15 + (float) num16;
    this.retentionText.text = num17.ToString() + "%";
    if (num3 == 0)
    {
      int num18;
      int num19 = num18 = 0;
      this.retentionTransform.gameObject.SetActive(false);
    }
    else
    {
      int gold = Functions.FuncRoundToInt((float) ((double) AtOManager.Instance.GetPlayerGold() * (double) num17 * 0.0099999997764825821));
      int dust = Functions.FuncRoundToInt((float) ((double) AtOManager.Instance.GetPlayerDust() * (double) num17 * 0.0099999997764825821));
      this.goldSummary += gold;
      this.dustSummary += dust;
      this.retentionTextGold.text = this.GoldDust(gold, dust);
    }
    if (this.goldSummary < 0)
      this.goldSummary = 0;
    if (this.dustSummary < 0)
      this.dustSummary = 0;
    if (num14 > 2 || GameManager.Instance.IsObeliskChallenge() || SandboxManager.Instance.IsEnabled())
    {
      this.rewardGroup.gameObject.SetActive(false);
      this.flag.transform.localPosition = new Vector3(this.flag.transform.localPosition.x, 0.0f, this.flag.transform.localPosition.z);
    }
    else
      this.rewardGroup.gameObject.SetActive(true);
    this.rewardsT.gameObject.SetActive(true);
    this.totalTextGold.text = this.GoldDust(this.goldSummary, this.dustSummary);
    int num20 = 1;
    if (GameManager.Instance.IsMultiplayer())
      num20 = NetworkManager.Instance.PlayerList.Length;
    if (_quantity > 0)
    {
      if (GameManager.Instance.IsObeliskChallenge())
        _quantity = Functions.FuncRoundToInt((float) _quantity * 0.5f);
      else
        _quantity += 80;
    }
    this.spr0.sprite = heroArray2[0].SpritePortrait;
    this.spr1.sprite = heroArray2[1].SpritePortrait;
    this.spr2.sprite = heroArray2[2].SpritePortrait;
    this.spr3.sprite = heroArray2[3].SpritePortrait;
    if (_quantity > 0)
    {
      PlayerManager.Instance.ModifyProgress(heroArray2[0].SubclassName, _quantity, 0);
      PlayerManager.Instance.ModifyProgress(heroArray2[1].SubclassName, _quantity, 1);
      PlayerManager.Instance.ModifyProgress(heroArray2[2].SubclassName, _quantity, 2);
      PlayerManager.Instance.ModifyProgress(heroArray2[3].SubclassName, _quantity, 3);
      PlayerManager.Instance.ModifyPlayerRankProgress(_quantity);
    }
    SaveManager.SavePlayerData();
    if (heroArray2[0] != null && PlayerManager.Instance.IsHeroUnlocked(heroArray2[0].SubclassName))
    {
      this.lockedSate[0] = true;
      this.fp0.SetCharacter(heroArray2[0].SourceName, heroArray2[0].ClassName, heroArray2[0].SubclassName, 0);
    }
    else
    {
      this.fp0.Hide();
      this.lockedSate[0] = false;
    }
    if (heroArray2[1] != null && PlayerManager.Instance.IsHeroUnlocked(heroArray2[1].SubclassName))
    {
      this.lockedSate[1] = true;
      this.fp1.SetCharacter(heroArray2[1].SourceName, heroArray2[1].ClassName, heroArray2[1].SubclassName, 1);
    }
    else
    {
      this.fp1.Hide();
      this.lockedSate[1] = false;
    }
    if (heroArray2[2] != null && PlayerManager.Instance.IsHeroUnlocked(heroArray2[2].SubclassName))
    {
      this.lockedSate[2] = true;
      this.fp2.SetCharacter(heroArray2[2].SourceName, heroArray2[2].ClassName, heroArray2[2].SubclassName, 2);
    }
    else
    {
      this.fp2.Hide();
      this.lockedSate[2] = false;
    }
    if (heroArray2[3] != null && PlayerManager.Instance.IsHeroUnlocked(heroArray2[3].SubclassName))
    {
      this.lockedSate[3] = true;
      this.fp3.SetCharacter(heroArray2[3].SourceName, heroArray2[3].ClassName, heroArray2[3].SubclassName, 3);
    }
    else
    {
      this.fp3.Hide();
      this.lockedSate[3] = false;
    }
    PlayerRun _playerRun = new PlayerRun();
    _playerRun.Id = AtOManager.Instance.GetGameId() + "_" + DateTime.UtcNow.Millisecond.ToString() + "_" + num13.ToString();
    _playerRun.Version = GameManager.Instance.gameVersion;
    _playerRun.GameSeed = AtOManager.Instance.GetGameId();
    _playerRun.gameDate = DateTime.Now.ToString();
    _playerRun.ObeliskChallenge = GameManager.Instance.IsObeliskChallenge();
    _playerRun.WeeklyChallenge = GameManager.Instance.IsWeeklyChallenge();
    _playerRun.WeekChallenge = AtOManager.Instance.GetWeekly();
    _playerRun.FinalScore = score;
    _playerRun.PlayedTime = AtOManager.Instance.playedTime;
    _playerRun.MadnessLevel = num14;
    _playerRun.MadnessCorruptors = str4;
    _playerRun.ObeliskMadness = AtOManager.Instance.GetObeliskMadness();
    _playerRun.ActTier = townTier;
    _playerRun.TotalPlayers = num20;
    _playerRun.PlaceOfDeath = str1;
    _playerRun.PlacesVisited = num3;
    _playerRun.ExperienceGained = num8;
    _playerRun.TotalGoldGained = AtOManager.Instance.totalGoldGained;
    _playerRun.TotalDustGained = AtOManager.Instance.totalDustGained;
    _playerRun.GoldGained = this.goldSummary;
    _playerRun.DustGained = this.dustSummary;
    _playerRun.SandboxEnabled = SandboxManager.Instance.IsEnabled();
    _playerRun.SandboxConfig = AtOManager.Instance.GetSandboxMods();
    int length = AtOManager.Instance.combatStats.GetLength(1);
    int[] numArray1 = new int[length];
    int[] numArray2 = new int[length];
    int[] numArray3 = new int[length];
    int[] numArray4 = new int[length];
    for (int index = 0; index < length; ++index)
    {
      numArray1[index] = AtOManager.Instance.combatStats[0, index];
      numArray2[index] = AtOManager.Instance.combatStats[1, index];
      numArray3[index] = AtOManager.Instance.combatStats[2, index];
      numArray4[index] = AtOManager.Instance.combatStats[3, index];
    }
    _playerRun.CombatStats0 = numArray1;
    _playerRun.CombatStats1 = numArray2;
    _playerRun.CombatStats2 = numArray3;
    _playerRun.CombatStats3 = numArray4;
    _playerRun.MonstersKilled = AtOManager.Instance.monstersKilled;
    _playerRun.BossesKilled = bossesKilled;
    _playerRun.BossesKilledName = AtOManager.Instance.bossesKilledName;
    _playerRun.UnlockedCards = this.unlockedCards;
    _playerRun.VisitedNodes = AtOManager.Instance.mapVisitedNodes;
    _playerRun.VisitedNodesAction = AtOManager.Instance.mapVisitedNodesAction;
    _playerRun.CommonCorruptions = AtOManager.Instance.corruptionCommonCompleted;
    _playerRun.UncommonCorruptions = AtOManager.Instance.corruptionUncommonCompleted;
    _playerRun.RareCorruptions = AtOManager.Instance.corruptionRareCompleted;
    _playerRun.EpicCorruptions = AtOManager.Instance.corruptionEpicCompleted;
    _playerRun.TotalDeaths = num7;
    _playerRun.Char0 = heroArray2[0].SubclassName;
    _playerRun.Char0Skin = heroArray2[0].SkinUsed;
    _playerRun.Char0Rank = heroArray2[0].PerkRank;
    _playerRun.Char0Cards = heroArray2[0].Cards;
    if (num20 > 1)
      _playerRun.Char0Owner = NetworkManager.Instance.GetPlayerNickReal(heroArray2[0].Owner);
    _playerRun.Char0Items = new List<string>();
    _playerRun.Char0Items.Add(heroArray2[0].Weapon);
    _playerRun.Char0Items.Add(heroArray2[0].Armor);
    _playerRun.Char0Items.Add(heroArray2[0].Jewelry);
    _playerRun.Char0Items.Add(heroArray2[0].Accesory);
    _playerRun.Char0Items.Add(heroArray2[0].Pet);
    _playerRun.Char0Traits = new List<string>();
    _playerRun.Char0Traits.Add(heroArray2[0].Traits[0]);
    _playerRun.Char0Traits.Add(heroArray2[0].Traits[1]);
    _playerRun.Char0Traits.Add(heroArray2[0].Traits[2]);
    _playerRun.Char0Traits.Add(heroArray2[0].Traits[3]);
    _playerRun.Char0Traits.Add(heroArray2[0].Traits[4]);
    if (heroArray2[1] != null && (UnityEngine.Object) heroArray2[1].HeroData != (UnityEngine.Object) null)
    {
      _playerRun.Char1 = heroArray2[1].SubclassName;
      _playerRun.Char1Skin = heroArray2[1].SkinUsed;
      _playerRun.Char1Rank = heroArray2[1].PerkRank;
      _playerRun.Char1Cards = heroArray2[1].Cards;
      if (num20 > 1)
        _playerRun.Char1Owner = NetworkManager.Instance.GetPlayerNickReal(heroArray2[1].Owner);
      _playerRun.Char1Items = new List<string>();
      _playerRun.Char1Items.Add(heroArray2[1].Weapon);
      _playerRun.Char1Items.Add(heroArray2[1].Armor);
      _playerRun.Char1Items.Add(heroArray2[1].Jewelry);
      _playerRun.Char1Items.Add(heroArray2[1].Accesory);
      _playerRun.Char1Items.Add(heroArray2[1].Pet);
      _playerRun.Char1Traits = new List<string>();
      _playerRun.Char1Traits.Add(heroArray2[1].Traits[0]);
      _playerRun.Char1Traits.Add(heroArray2[1].Traits[1]);
      _playerRun.Char1Traits.Add(heroArray2[1].Traits[2]);
      _playerRun.Char1Traits.Add(heroArray2[1].Traits[3]);
      _playerRun.Char1Traits.Add(heroArray2[1].Traits[4]);
    }
    if (heroArray2[2] != null && (UnityEngine.Object) heroArray2[2].HeroData != (UnityEngine.Object) null)
    {
      _playerRun.Char2 = heroArray2[2].SubclassName;
      _playerRun.Char2Skin = heroArray2[2].SkinUsed;
      _playerRun.Char2Rank = heroArray2[2].PerkRank;
      _playerRun.Char2Cards = heroArray2[2].Cards;
      if (num20 > 1)
        _playerRun.Char2Owner = NetworkManager.Instance.GetPlayerNickReal(heroArray2[2].Owner);
      _playerRun.Char2Items = new List<string>();
      _playerRun.Char2Items.Add(heroArray2[2].Weapon);
      _playerRun.Char2Items.Add(heroArray2[2].Armor);
      _playerRun.Char2Items.Add(heroArray2[2].Jewelry);
      _playerRun.Char2Items.Add(heroArray2[2].Accesory);
      _playerRun.Char2Items.Add(heroArray2[2].Pet);
      _playerRun.Char2Traits = new List<string>();
      _playerRun.Char2Traits.Add(heroArray2[2].Traits[0]);
      _playerRun.Char2Traits.Add(heroArray2[2].Traits[1]);
      _playerRun.Char2Traits.Add(heroArray2[2].Traits[2]);
      _playerRun.Char2Traits.Add(heroArray2[2].Traits[3]);
      _playerRun.Char2Traits.Add(heroArray2[2].Traits[4]);
    }
    if (heroArray2[3] != null && (UnityEngine.Object) heroArray2[3].HeroData != (UnityEngine.Object) null)
    {
      _playerRun.Char3 = heroArray2[3].SubclassName;
      _playerRun.Char3Skin = heroArray2[3].SkinUsed;
      _playerRun.Char3Rank = heroArray2[3].PerkRank;
      _playerRun.Char3Cards = heroArray2[3].Cards;
      if (num20 > 1)
        _playerRun.Char3Owner = NetworkManager.Instance.GetPlayerNickReal(heroArray2[3].Owner);
      _playerRun.Char3Items = new List<string>();
      _playerRun.Char3Items.Add(heroArray2[3].Weapon);
      _playerRun.Char3Items.Add(heroArray2[3].Armor);
      _playerRun.Char3Items.Add(heroArray2[3].Jewelry);
      _playerRun.Char3Items.Add(heroArray2[3].Accesory);
      _playerRun.Char3Items.Add(heroArray2[3].Pet);
      _playerRun.Char3Traits = new List<string>();
      _playerRun.Char3Traits.Add(heroArray2[3].Traits[0]);
      _playerRun.Char3Traits.Add(heroArray2[3].Traits[1]);
      _playerRun.Char3Traits.Add(heroArray2[3].Traits[2]);
      _playerRun.Char3Traits.Add(heroArray2[3].Traits[3]);
      _playerRun.Char3Traits.Add(heroArray2[3].Traits[4]);
    }
    SaveManager.SaveRun(_playerRun);
  }

  private IEnumerator RemoveDoubleDots()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    for (int index = 0; index < this.titlesTexts.Count; ++index)
      this.titlesTexts[index].text = Functions.RemoveLastDoubleDot(this.titlesTexts[index].text);
  }

  private IEnumerator ProgressCo(FinishProgression _fp, int _increment)
  {
    if (_increment > 0)
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.3f);
      GameManager.Instance.GenerateParticleTrail(0, this.finalScore.transform.position, _fp.gameObject.transform.position);
    }
    yield return (object) Globals.Instance.WaitForSeconds(0.4f);
    _fp.Increment(_increment);
  }

  public void UnlockState(int index)
  {
    this.lockedSate[index] = false;
    for (int index1 = 0; index1 < 4; ++index1)
    {
      if (this.lockedSate[index1])
        return;
    }
    if (this.finishCo != null)
      this.StopCoroutine(this.finishCo);
    this.finishCo = this.StartCoroutine(this.FinishThisCo());
  }

  private IEnumerator FinishThisCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(1f);
    this.mainMenuButton.Enable();
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    int absolutePosition = -1)
  {
    this._controllerList.Clear();
    this._controllerList.Add(this.mainMenuButton.transform);
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }
}
