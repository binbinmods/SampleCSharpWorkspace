// Decompiled with JetBrains decompiler
// Type: RewardsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RewardsManager : MonoBehaviour
{
  private PhotonView photonView;
  public Transform sceneCamera;
  public CharacterWindowUI characterWindowUI;
  public Transform[] characterRewardArray = new Transform[4];
  public Dictionary<int, string[]> cardsByOrder;
  public Hero[] theTeam;
  public string[] cardSelectedArr;
  private string[] combatScarab;
  private int combatScarabGold;
  public int combatScarabDust;
  private int combatScarabExp;
  public int dustQuantity;
  private ThermometerData thermometerData;
  public TMP_Text title;
  public TMP_Text subtitle;
  public TMP_Text corruptionRewardText;
  public Transform corruptionReward;
  public Transform corruptionRewardBgText;
  public Transform corruptionRewardBgCard;
  private int numRewards;
  private int numCardsReward = 3;
  public int typeOfReward;
  private TierRewardData tierReward;
  private TierRewardData tierRewardBase;
  private TierRewardData tierRewardInf;
  public int experienceEach;
  public int goldEach;
  private int cardTierModFromCorruption;
  private bool finishReward;
  private bool reseting;
  public Transform buttonRestart;
  private string teamAtOToJson;
  private string[] keyListGold;
  private int[] valueListGold;
  private int playerGold;
  private string[] keyListDust;
  private int[] valueListDust;
  private int playerDust;
  private int divinationsNumber;
  private int totalGoldGained;
  private int totalDustGained;
  private int expGained;
  private int atoGoldGained;
  private int atoDustGained;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();

  public static RewardsManager Instance { get; private set; }

  private void Awake()
  {
    if ((UnityEngine.Object) GameManager.Instance == (UnityEngine.Object) null)
    {
      SceneStatic.LoadByName("Rewards");
    }
    else
    {
      if ((UnityEngine.Object) RewardsManager.Instance == (UnityEngine.Object) null)
        RewardsManager.Instance = this;
      else if ((UnityEngine.Object) RewardsManager.Instance != (UnityEngine.Object) this)
        UnityEngine.Object.Destroy((UnityEngine.Object) this);
      this.sceneCamera.gameObject.SetActive(false);
      this.photonView = PhotonView.Get((Component) this);
      this.corruptionReward.gameObject.SetActive(false);
      NetworkManager.Instance.StartStopQueue(true);
    }
  }

  public void RestartRewards()
  {
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
    {
      this.reseting = true;
      this.StartCoroutine(this.RestartRewardsMaster());
    }
    else
    {
      this.buttonRestart.gameObject.SetActive(false);
      this.photonView.RPC("NET_RestartRewards", RpcTarget.MasterClient, (object) NetworkManager.Instance.GetPlayerNickReal(NetworkManager.Instance.GetPlayerNick()));
    }
  }

  private IEnumerator RestartRewardsMaster()
  {
    if (!this.finishReward)
    {
      if (GameManager.Instance.IsMultiplayer())
      {
        this.photonView.RPC("NET_ShowMaskLoading", RpcTarget.Others);
        GameManager.Instance.SetMaskLoading();
        yield return (object) Globals.Instance.WaitForSeconds(1f);
      }
      AtOManager.Instance.SetTeamFromTeamHero(JsonHelper.FromJson<Hero>(this.teamAtOToJson));
      AtOManager.Instance.SetPlayerGold(this.playerGold);
      Dictionary<string, int> _mpPlayersGold = new Dictionary<string, int>();
      for (int index = 0; index < this.keyListGold.Length; ++index)
        _mpPlayersGold.Add(this.keyListGold[index], this.valueListGold[index]);
      AtOManager.Instance.SetMpPlayersGold(_mpPlayersGold);
      AtOManager.Instance.SetPlayerDust(this.playerDust);
      Dictionary<string, int> _mpPlayersDust = new Dictionary<string, int>();
      for (int index = 0; index < this.keyListDust.Length; ++index)
        _mpPlayersDust.Add(this.keyListDust[index], this.valueListDust[index]);
      AtOManager.Instance.SetMpPlayersDust(_mpPlayersDust);
      AtOManager.Instance.divinationsNumber = this.divinationsNumber;
      AtOManager.Instance.totalGoldGained = this.totalGoldGained;
      AtOManager.Instance.totalDustGained = this.totalDustGained;
      PlayerManager.Instance.GoldGained = this.atoGoldGained;
      PlayerManager.Instance.DustGained = this.atoDustGained;
      PlayerManager.Instance.ExpGained = this.expGained;
      AtOManager.Instance.RelaunchRewards();
    }
  }

  [PunRPC]
  private void NET_RestartRewards(string _nick)
  {
    AlertManager.Instance.AlertConfirmDouble(string.Format(Texts.Instance.GetText("restartClient"), (object) _nick));
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.WantToRestart);
    AlertManager.Instance.ShowReloadIcon();
  }

  [PunRPC]
  private void NET_ShowMaskLoading()
  {
    this.reseting = true;
    GameManager.Instance.SetMaskLoading();
  }

  private void WantToRestart()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.WantToRestart);
    if (!AlertManager.Instance.GetConfirmAnswer())
      return;
    this.RestartRewards();
  }

  private void Start()
  {
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
    {
      this.teamAtOToJson = JsonHelper.ToJson<Hero>(AtOManager.Instance.GetTeam());
      this.playerGold = AtOManager.Instance.GetPlayerGold();
      Dictionary<string, int> mpPlayersGold = AtOManager.Instance.GetMpPlayersGold();
      this.keyListGold = new string[mpPlayersGold.Count];
      mpPlayersGold.Keys.CopyTo(this.keyListGold, 0);
      this.valueListGold = new int[mpPlayersGold.Count];
      mpPlayersGold.Values.CopyTo(this.valueListGold, 0);
      this.playerDust = AtOManager.Instance.GetPlayerDust();
      Dictionary<string, int> mpPlayersDust = AtOManager.Instance.GetMpPlayersDust();
      this.keyListDust = new string[mpPlayersDust.Count];
      mpPlayersDust.Keys.CopyTo(this.keyListDust, 0);
      this.valueListDust = new int[mpPlayersDust.Count];
      mpPlayersDust.Values.CopyTo(this.valueListDust, 0);
      this.divinationsNumber = AtOManager.Instance.divinationsNumber;
      this.totalGoldGained = AtOManager.Instance.totalGoldGained;
      this.totalDustGained = AtOManager.Instance.totalDustGained;
      this.atoGoldGained = PlayerManager.Instance.GoldGained;
      this.atoDustGained = PlayerManager.Instance.DustGained;
      this.expGained = PlayerManager.Instance.ExpGained;
    }
    this.cardSelectedArr = new string[4];
    this.theTeam = AtOManager.Instance.GetTeam();
    AudioManager.Instance.DoBSO("Rewards");
    this.StartCoroutine(this.SetRewards());
  }

  private IEnumerator SetRewards()
  {
    if (GameManager.Instance.IsMultiplayer())
    {
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersReady("setrewards"))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("Game ready, Everybody checked setrewards");
        NetworkManager.Instance.PlayersNetworkContinue("setrewards");
      }
      else
      {
        NetworkManager.Instance.SetWaitingSyncro("setrewards", true);
        NetworkManager.Instance.SetStatusReady("setrewards");
        while (NetworkManager.Instance.WaitingSyncro["setrewards"])
          yield return (object) Globals.Instance.WaitForSeconds(0.1f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("setrewards, we can continue!");
      }
    }
    GameManager.Instance.SceneLoaded();
    if (AtOManager.Instance.corruptionAccepted)
    {
      AtOManager.Instance.comingFromCombatDoRewards = true;
      CardData cardData = Globals.Instance.GetCardData(AtOManager.Instance.corruptionIdCard, false);
      if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
      {
        StringBuilder stringBuilder = new StringBuilder();
        Animator component1 = this.corruptionReward.GetComponent<Animator>();
        switch (AtOManager.Instance.corruptionId)
        {
          case "increasedqualityofcardrewards":
            stringBuilder.Append("<sprite name=cards> +1 ");
            stringBuilder.Append(Texts.Instance.GetText("cardsTier"));
            this.corruptionRewardText.text = stringBuilder.ToString();
            this.corruptionRewardBgText.gameObject.SetActive(true);
            this.corruptionReward.gameObject.SetActive(true);
            this.cardTierModFromCorruption = 1;
            this.numCardsReward = 4;
            component1.SetTrigger("gold");
            break;
          case "goldshards0":
            if (cardData.CardRarity == Enums.CardRarity.Common)
            {
              int num1 = AtOManager.Instance.ModifyQuantityObeliskTraits(0, 320);
              stringBuilder.Append("<sprite name=gold> ");
              stringBuilder.Append(num1);
              int num2 = AtOManager.Instance.ModifyQuantityObeliskTraits(1, 320);
              stringBuilder.Append("  <sprite name=dust> ");
              stringBuilder.Append(num2);
            }
            else
            {
              int num3 = AtOManager.Instance.ModifyQuantityObeliskTraits(0, 520);
              stringBuilder.Append("<sprite name=gold> ");
              stringBuilder.Append(num3);
              int num4 = AtOManager.Instance.ModifyQuantityObeliskTraits(1, 520);
              stringBuilder.Append("  <sprite name=dust> ");
              stringBuilder.Append(num4);
            }
            this.corruptionRewardText.text = stringBuilder.ToString();
            this.corruptionRewardBgText.gameObject.SetActive(true);
            this.corruptionReward.gameObject.SetActive(true);
            component1.SetTrigger("gold");
            break;
          case "goldshards1":
            if (cardData.CardRarity == Enums.CardRarity.Rare)
            {
              int num5 = AtOManager.Instance.ModifyQuantityObeliskTraits(0, 720);
              stringBuilder.Append("<sprite name=gold> ");
              stringBuilder.Append(num5);
              int num6 = AtOManager.Instance.ModifyQuantityObeliskTraits(1, 720);
              stringBuilder.Append("  <sprite name=dust> ");
              stringBuilder.Append(num6);
              stringBuilder.Append("  <sprite name=supply> 1");
            }
            else
            {
              int num7 = AtOManager.Instance.ModifyQuantityObeliskTraits(0, 1000);
              stringBuilder.Append("<sprite name=gold> ");
              stringBuilder.Append(num7);
              int num8 = AtOManager.Instance.ModifyQuantityObeliskTraits(1, 1000);
              stringBuilder.Append("  <sprite name=dust> ");
              stringBuilder.Append(num8);
              stringBuilder.Append("  <sprite name=supply> 2");
            }
            this.corruptionRewardText.text = stringBuilder.ToString();
            this.corruptionRewardBgText.gameObject.SetActive(true);
            this.corruptionReward.gameObject.SetActive(true);
            component1.SetTrigger("gold");
            break;
          case "herocard":
            stringBuilder.Append(this.theTeam[AtOManager.Instance.corruptionRewardChar].SourceName);
            this.corruptionRewardText.text = stringBuilder.ToString();
            this.corruptionRewardBgCard.gameObject.SetActive(true);
            this.corruptionReward.gameObject.SetActive(true);
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.corruptionRewardBgCard);
            gameObject.transform.localPosition = new Vector3(0.0f, -0.9f, 0.0f);
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            CardItem component2 = gameObject.GetComponent<CardItem>();
            component2.SetCard(AtOManager.Instance.corruptionRewardCard, false, this.theTeam[AtOManager.Instance.corruptionRewardChar]);
            component2.TopLayeringOrder("Book", 2000);
            component2.cardmakebig = true;
            component2.CreateColliderAdjusted();
            component2.cardmakebigSize = 1f;
            component2.cardmakebigSizeMax = 1.1f;
            if (!PlayerManager.Instance.IsCardUnlocked(AtOManager.Instance.corruptionRewardCard))
            {
              PlayerManager.Instance.CardUnlock(AtOManager.Instance.corruptionRewardCard, true);
              component2.ShowUnlocked(false);
            }
            component1.SetTrigger("card");
            break;
        }
      }
      else
        AtOManager.Instance.ClearCorruption();
    }
    Debug.Log((object) ("scarab->" + AtOManager.Instance.combatScarab));
    if (AtOManager.Instance.combatScarab != "")
    {
      this.combatScarab = AtOManager.Instance.combatScarab.Split('%', StringSplitOptions.None);
      if (this.combatScarab.Length == 2 && this.combatScarab[1] == "1")
      {
        if (this.combatScarab[0] == "goldenscarab")
          this.combatScarabGold = 150;
        else if (this.combatScarab[0] == "jadescarab")
        {
          this.combatScarabGold = 50;
          this.combatScarabDust = 50;
          this.combatScarabExp = 50;
        }
        else if (this.combatScarab[0] == "crystalscarab")
          this.combatScarabDust = 150;
      }
    }
    TierRewardData eventRewardTier = AtOManager.Instance.GetEventRewardTier();
    TierRewardData townDivinationTier = AtOManager.Instance.GetTownDivinationTier();
    this.subtitle.text = Texts.Instance.GetText("eventRewardsSubtitle");
    if ((UnityEngine.Object) townDivinationTier != (UnityEngine.Object) null)
    {
      this.title.text = Texts.Instance.GetText("divinationRoundRewards");
      if (townDivinationTier.TierNum > 5)
        this.numCardsReward = 4;
    }
    else if ((UnityEngine.Object) eventRewardTier != (UnityEngine.Object) null)
      this.title.text = Texts.Instance.GetText("eventRewards");
    else if (AtOManager.Instance.GetTeamNPC().Length != 0)
    {
      this.title.text = Texts.Instance.GetText("combatRewards");
      this.thermometerData = AtOManager.Instance.GetCombatThermometerData();
    }
    else
      this.title.text = "";
    if ((UnityEngine.Object) this.thermometerData != (UnityEngine.Object) null)
      this.subtitle.text = Functions.ThermometerTextForRewards(this.thermometerData);
    if (this.combatScarabGold > 0 || this.combatScarabDust > 0 || this.combatScarabExp > 0)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      if (this.combatScarabGold > 0)
      {
        stringBuilder2.Append("<space=1><sprite name=gold>+");
        stringBuilder2.Append(this.combatScarabGold);
      }
      if (this.combatScarabDust > 0)
      {
        stringBuilder2.Append("<space=1><sprite name=dust>+");
        stringBuilder2.Append(this.combatScarabDust);
      }
      if (this.combatScarabExp > 0)
      {
        stringBuilder2.Append("<space=1><sprite name=experience>+");
        stringBuilder2.Append(this.combatScarabExp);
      }
      stringBuilder1.Append("\n<size=-.5><color=#FFEBA5><color=#A48D3D>[</color>");
      stringBuilder1.Append(string.Format(Texts.Instance.GetText("scarabBonus"), (object) stringBuilder2.ToString()));
      stringBuilder1.Append("<color=#A48D3D>]</color></size></color>");
      this.subtitle.text += stringBuilder1.ToString();
    }
    bool flag1 = false;
    if (GameManager.Instance.IsObeliskChallenge() && Globals.Instance.ZoneDataSource[AtOManager.Instance.GetTownZoneId().ToLower()].ObeliskLow)
      flag1 = true;
    if (!GameManager.Instance.IsMultiplayer() || GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
    {
      UnityEngine.Random.InitState((AtOManager.Instance.GetGameId() + "_" + AtOManager.Instance.mapVisitedNodes.Count.ToString() + "_" + AtOManager.Instance.currentMapNode + "_" + AtOManager.Instance.divinationsNumber.ToString()).GetDeterministicHashCode());
      ++AtOManager.Instance.divinationsNumber;
      this.cardsByOrder = new Dictionary<int, string[]>();
      if ((UnityEngine.Object) townDivinationTier != (UnityEngine.Object) null)
      {
        this.tierRewardBase = townDivinationTier;
        this.typeOfReward = 2;
      }
      else if ((UnityEngine.Object) eventRewardTier != (UnityEngine.Object) null)
      {
        this.tierRewardBase = eventRewardTier;
        this.typeOfReward = 2;
      }
      else if (AtOManager.Instance.GetTeamNPC().Length != 0)
      {
        this.tierRewardBase = AtOManager.Instance.GetTeamNPCReward();
        this.typeOfReward = 1;
      }
      else
      {
        this.tierRewardBase = Globals.Instance.GetTierRewardData(0);
        this.typeOfReward = 0;
      }
      this.dustQuantity = this.tierRewardBase.Dust;
      int num9 = this.tierRewardBase.TierNum;
      AtOManager.Instance.currentRewardTier = num9;
      if ((UnityEngine.Object) this.thermometerData != (UnityEngine.Object) null)
        num9 += this.thermometerData.CardBonus + this.cardTierModFromCorruption;
      if (num9 < 0)
        num9 = 0;
      this.tierRewardBase = Globals.Instance.GetTierRewardData(num9);
      if (GameManager.Instance.IsObeliskChallenge())
      {
        if (flag1)
          num9 += 2;
        else
          ++num9;
      }
      this.tierRewardInf = num9 <= 0 ? this.tierRewardBase : Globals.Instance.GetTierRewardData(num9 - 1);
      CardData _cardData = (CardData) null;
      string str = "";
      for (int key = 0; key < this.theTeam.Length; ++key)
      {
        if (this.theTeam[key] == null || (UnityEngine.Object) this.theTeam[key].HeroData == (UnityEngine.Object) null)
        {
          this.cardsByOrder[key] = new string[3]
          {
            "",
            "",
            ""
          };
        }
        else
        {
          Hero hero = this.theTeam[key];
          Enums.CardClass result1 = Enums.CardClass.None;
          Enum.TryParse<Enums.CardClass>(Enum.GetName(typeof (Enums.HeroClass), (object) hero.HeroData.HeroClass), out result1);
          Enums.CardClass result2 = Enums.CardClass.None;
          Enum.TryParse<Enums.CardClass>(Enum.GetName(typeof (Enums.HeroClass), (object) hero.HeroData.HeroSubClass.HeroClassSecondary), out result2);
          int length = this.numCardsReward;
          if (this.numCardsReward == 3 && result2 != Enums.CardClass.None)
            length = 4;
          string[] arr = new string[length];
          List<string> stringList1 = Globals.Instance.CardListNotUpgradedByClass[result1];
          List<string> stringList2 = result2 == Enums.CardClass.None ? new List<string>() : Globals.Instance.CardListNotUpgradedByClass[result2];
          for (int index1 = 0; index1 < length; ++index1)
          {
            this.tierReward = index1 != 0 ? this.tierRewardInf : this.tierRewardBase;
            int num10 = UnityEngine.Random.Range(0, 100);
            bool flag2 = true;
            while (flag2)
            {
              flag2 = true;
              bool flag3 = false;
              while (!flag3)
              {
                flag2 = false;
                str = stringList1[UnityEngine.Random.Range(0, stringList1.Count)];
                _cardData = Globals.Instance.GetCardData(index1 < 2 || result2 == Enums.CardClass.None ? stringList1[UnityEngine.Random.Range(0, stringList1.Count)] : stringList2[UnityEngine.Random.Range(0, stringList2.Count)], false);
                if (!flag2)
                {
                  if (num10 < this.tierReward.Common)
                  {
                    if (_cardData.CardRarity == Enums.CardRarity.Common)
                      flag3 = true;
                  }
                  else if (num10 < this.tierReward.Common + this.tierReward.Uncommon)
                  {
                    if (_cardData.CardRarity == Enums.CardRarity.Uncommon)
                      flag3 = true;
                  }
                  else if (num10 < this.tierReward.Common + this.tierReward.Uncommon + this.tierReward.Rare)
                  {
                    if (_cardData.CardRarity == Enums.CardRarity.Rare)
                      flag3 = true;
                  }
                  else if (num10 < this.tierReward.Common + this.tierReward.Uncommon + this.tierReward.Rare + this.tierReward.Epic)
                  {
                    if (_cardData.CardRarity == Enums.CardRarity.Epic)
                      flag3 = true;
                  }
                  else if (_cardData.CardRarity == Enums.CardRarity.Mythic)
                    flag3 = true;
                }
              }
              int rarity = UnityEngine.Random.Range(0, 100);
              string id = _cardData.Id;
              _cardData = Globals.Instance.GetCardData(Functions.GetCardByRarity(rarity, _cardData), false);
              if ((UnityEngine.Object) _cardData == (UnityEngine.Object) null)
              {
                flag2 = true;
              }
              else
              {
                for (int index2 = 0; index2 < arr.Length; ++index2)
                {
                  if (arr[index2] == _cardData.Id)
                  {
                    flag2 = true;
                    break;
                  }
                }
              }
            }
            arr[index1] = _cardData.Id;
          }
          this.cardsByOrder[key] = Functions.ShuffleArray<string>(arr);
        }
      }
      this.experienceEach = 0;
      this.goldEach = 0;
      if (this.typeOfReward == 1)
      {
        this.experienceEach = Functions.FuncRoundToInt((float) (AtOManager.Instance.GetExperienceFromCombat() / 4));
        this.goldEach = Functions.FuncRoundToInt((float) (AtOManager.Instance.GetGoldFromCombat() / 4));
        if ((UnityEngine.Object) this.thermometerData != (UnityEngine.Object) null)
        {
          this.experienceEach += Functions.FuncRoundToInt((float) ((double) this.experienceEach * (double) this.thermometerData.ExpBonus / 100.0));
          this.goldEach += Functions.FuncRoundToInt((float) ((double) this.goldEach * (double) this.thermometerData.GoldBonus / 100.0));
        }
      }
      if (GameManager.Instance.IsObeliskChallenge() & flag1)
      {
        this.goldEach *= 2;
        this.dustQuantity *= 2;
      }
      if (MadnessManager.Instance.IsMadnessTraitActive("poverty") || AtOManager.Instance.IsChallengeTraitActive("poverty"))
      {
        if (!GameManager.Instance.IsObeliskChallenge())
        {
          this.dustQuantity -= Functions.FuncRoundToInt((float) this.dustQuantity * 0.5f);
          this.goldEach -= Functions.FuncRoundToInt((float) this.goldEach * 0.5f);
        }
        else
        {
          this.dustQuantity -= Functions.FuncRoundToInt((float) this.dustQuantity * 0.3f);
          this.goldEach -= Functions.FuncRoundToInt((float) this.goldEach * 0.3f);
        }
      }
      if (AtOManager.Instance.IsChallengeTraitActive("prosperity"))
      {
        this.dustQuantity += Functions.FuncRoundToInt((float) this.dustQuantity * 0.5f);
        this.goldEach += Functions.FuncRoundToInt((float) this.dustQuantity * 0.5f);
      }
      this.goldEach += this.combatScarabGold;
      this.experienceEach += this.combatScarabExp;
      if (GameManager.Instance.IsMultiplayer())
        this.photonView.RPC("NET_ShareRewards", RpcTarget.Others, (object) this.cardsByOrder[0], (object) this.cardsByOrder[1], (object) this.cardsByOrder[2], (object) this.cardsByOrder[3], (object) this.dustQuantity, (object) this.typeOfReward, (object) this.experienceEach, (object) this.goldEach, (object) this.combatScarabDust);
      this.ShowRewards();
    }
  }

  public void ShowCharacterWindow(string type = "", bool isHero = true, int characterIndex = -1) => this.characterWindowUI.Show(type, characterIndex);

  public void ShowDeck(int auxInt) => this.characterWindowUI.Show("deck", auxInt);

  [PunRPC]
  private void NET_ShareRewards(
    string[] cards0,
    string[] cards1,
    string[] cards2,
    string[] cards3,
    int _dustQuantity,
    int _typeOfReward,
    int _experienceEach,
    int _goldEach,
    int _combatScarabDust)
  {
    this.cardsByOrder = new Dictionary<int, string[]>();
    this.cardsByOrder.Add(0, cards0);
    this.cardsByOrder.Add(1, cards1);
    this.cardsByOrder.Add(2, cards2);
    this.cardsByOrder.Add(3, cards3);
    this.dustQuantity = _dustQuantity;
    this.typeOfReward = _typeOfReward;
    this.experienceEach = _experienceEach;
    this.goldEach = _goldEach;
    this.combatScarabDust = _combatScarabDust;
    this.ShowRewards();
  }

  private void ShowRewards() => this.StartCoroutine(this.ShowRewardsCo());

  private IEnumerator ShowRewardsCo()
  {
    for (int i = 0; i < 4; ++i)
    {
      if (this.theTeam[i] != null && !((UnityEngine.Object) this.theTeam[i].HeroData == (UnityEngine.Object) null))
      {
        yield return (object) Globals.Instance.WaitForSeconds(0.15f);
        this.characterRewardArray[i].gameObject.SetActive(true);
        this.characterRewardArray[i].GetComponent<CharacterReward>().Init(i);
        ++this.numRewards;
      }
    }
    GameManager.Instance.ShowTutorialPopup("cardsReward", Vector3.zero, Vector3.zero);
  }

  public void SetCardReward(string playerNick, string internalId)
  {
    for (int index = 0; index < 4; ++index)
      this.characterRewardArray[index].GetComponent<CharacterReward>().CardSelected(playerNick, internalId);
  }

  public void CardSelected(int index, string cardId)
  {
    if (GameManager.Instance.IsMultiplayer())
      this.photonView.RPC("MASTER_CardSelected", RpcTarget.MasterClient, (object) (short) index, (object) cardId);
    else
      this.NET_CardSelected((short) index, cardId);
  }

  [PunRPC]
  private void MASTER_CardSelected(short index, string cardId)
  {
    if (this.reseting)
      return;
    this.photonView.RPC("NET_CardSelected", RpcTarget.All, (object) index, (object) cardId);
  }

  [PunRPC]
  private void NET_CardSelected(short _index, string cardId)
  {
    int index = (int) _index;
    this.cardSelectedArr[index] = cardId;
    this.characterRewardArray[index].GetComponent<CharacterReward>().ShowSelected(cardId);
    this.CheckAllAssigned();
  }

  public void DustSelected(int index)
  {
    if (GameManager.Instance.IsMultiplayer())
      this.photonView.RPC("MASTER_DustSelected", RpcTarget.MasterClient, (object) (short) index);
    else
      this.NET_DustSelected((short) index);
  }

  [PunRPC]
  private void MASTER_DustSelected(short index)
  {
    if (this.reseting)
      return;
    this.photonView.RPC("NET_DustSelected", RpcTarget.All, (object) index);
  }

  [PunRPC]
  private void NET_DustSelected(short _index)
  {
    int index = (int) _index;
    this.cardSelectedArr[index] = "dust";
    this.characterRewardArray[index].GetComponent<CharacterReward>().ShowSelected("dust");
    this.CheckAllAssigned();
  }

  private void CheckAllAssigned()
  {
    for (int index = 0; index < this.numRewards; ++index)
    {
      if (this.cardSelectedArr[index] == null)
        return;
    }
    if (!GameManager.Instance.IsMultiplayer() || GameManager.Instance.IsMultiplayer() && NetworkManager.Instance.IsMaster())
    {
      this.finishReward = true;
      this.buttonRestart.gameObject.SetActive(false);
      this.StartCoroutine(this.CloseWindow());
    }
    this.SetCombatCorruption();
    SaveManager.SavePlayerData();
  }

  public void Reload() => SceneStatic.LoadByName("Rewards");

  private void SetCombatCorruption() => AtOManager.Instance.SetCombatCorruptionForScore();

  private IEnumerator CloseWindow()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.4f);
    AtOManager.Instance.DeleteSaveGameTurn();
    AtOManager.Instance.FinishCardRewards(this.cardSelectedArr);
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    bool shoulderLeft = false,
    bool shoulderRight = false)
  {
    this._controllerList.Clear();
    this._controllerList.Add(this.buttonRestart);
    for (int index = 0; index < 4; ++index)
    {
      if (Functions.TransformIsVisible(this.characterRewardArray[index].transform))
      {
        foreach (Transform transform in this.characterRewardArray[index].GetComponent<CharacterReward>().cardsTransform)
        {
          if (transform.gameObject.activeSelf)
            this._controllerList.Add(transform);
        }
        if ((UnityEngine.Object) this.characterRewardArray[index].GetComponent<CharacterReward>().quantityDust != (UnityEngine.Object) null && this.characterRewardArray[index].GetComponent<CharacterReward>().quantityDust.GetChild(0).GetComponent<BoxCollider2D>().enabled)
          this._controllerList.Add(this.characterRewardArray[index].GetComponent<CharacterReward>().quantityDust);
        this._controllerList.Add(this.characterRewardArray[index].GetComponent<CharacterReward>().buttonCharacterDeck);
      }
    }
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }

  public void ControllerMoveShoulder(bool _isRight = false)
  {
  }
}
