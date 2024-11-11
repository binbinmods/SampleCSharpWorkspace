// Decompiled with JetBrains decompiler
// Type: ChallengeSelectionManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ChallengeSelectionManager : MonoBehaviour
{
  private PhotonView photonView;
  public ChallengeBossBanners challengeBossBanners;
  public SideCharacters sideCharacters;
  public int currentHeroIndex;
  private int maxRound = 4;
  public PerkChallengeItem[] perkChallengeItems;
  private int[] heroRoundArr = new int[4];
  private int[] heroRerolledTimesArr = new int[4];
  private int[,] heroPackSelectedRerolledTimesArr = new int[4, 8];
  private int maxSelectablePerks = 4;
  private int cardsForPack = 3;
  private Hero currentHero;
  private Hero[] theTeam;
  private Dictionary<string, string[]> cardsDrafted = new Dictionary<string, string[]>();
  private Dictionary<string, string> cardsDraftedSpecial = new Dictionary<string, string>();
  private Dictionary<string, string> cardsDraftedPackname = new Dictionary<string, string>();
  private Dictionary<string, string> cardsDraftedPackid = new Dictionary<string, string>();
  private Dictionary<int, string> packsSelected = new Dictionary<int, string>();
  private Dictionary<int, List<int>> perkDrafted = new Dictionary<int, List<int>>();
  private Dictionary<string, int> dictBonus = new Dictionary<string, int>();
  private Dictionary<string, int> dictBonusSingle = new Dictionary<string, int>();
  private Dictionary<string, int> dictAura = new Dictionary<string, int>();
  private Dictionary<string, int> dictAuraSingle = new Dictionary<string, int>();
  private Dictionary<string, float> dictEnergyCost = new Dictionary<string, float>();
  private bool statusReady;
  private Coroutine manualReadyCo;
  public TMP_Text _HP;
  public TMP_Text _Energy;
  public TMP_Text _Speed;
  public Transform sceneCamera;

  public static ChallengeSelectionManager Instance { get; private set; }

  public int CardsForPack
  {
    get => this.cardsForPack;
    set => this.cardsForPack = value;
  }

  private void Awake()
  {
    if ((UnityEngine.Object) GameManager.Instance == (UnityEngine.Object) null)
    {
      SceneStatic.LoadByName("Game");
    }
    else
    {
      if ((UnityEngine.Object) ChallengeSelectionManager.Instance == (UnityEngine.Object) null)
        ChallengeSelectionManager.Instance = this;
      else if ((UnityEngine.Object) ChallengeSelectionManager.Instance != (UnityEngine.Object) this)
        UnityEngine.Object.Destroy((UnityEngine.Object) this);
      this.photonView = PhotonView.Get((Component) this);
      this.sceneCamera.gameObject.SetActive(false);
      NetworkManager.Instance.StartStopQueue(true);
    }
  }

  private void Start() => this.StartCoroutine(this.StartCo());

  private IEnumerator StartCo()
  {
    ChallengeSelectionManager selectionManager = this;
    AudioManager.Instance.DoBSO("Town");
    if (GameManager.Instance.IsMultiplayer())
    {
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("**************************");
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("WaitingSyncro startchallenge", "net");
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersReady("startchallenge"))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("Game ready, Everybody checked startchallenge", "net");
        NetworkManager.Instance.PlayersNetworkContinue("startchallenge");
      }
      else
      {
        NetworkManager.Instance.SetWaitingSyncro("startchallenge", true);
        NetworkManager.Instance.SetStatusReady("startchallenge");
        while (NetworkManager.Instance.WaitingSyncro["startchallenge"])
          yield return (object) Globals.Instance.WaitForSeconds(0.1f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("startchallenge, we can continue!", "net");
      }
    }
    for (int key = 0; key < 4; ++key)
    {
      selectionManager.heroRoundArr[key] = 0;
      selectionManager.heroRerolledTimesArr[key] = 0;
      for (int index = 0; index < 8; ++index)
        selectionManager.heroPackSelectedRerolledTimesArr[key, index] = 0;
      if (!selectionManager.perkDrafted.ContainsKey(key))
        selectionManager.perkDrafted.Add(key, new List<int>());
      else
        selectionManager.perkDrafted[key] = new List<int>();
    }
    selectionManager.theTeam = AtOManager.Instance.GetTeam();
    selectionManager.SetDefaultCards(0);
    selectionManager.SetDefaultCards(1);
    selectionManager.SetDefaultCards(2);
    selectionManager.SetDefaultCards(3);
    AtOManager.Instance.DoChallengeShop();
    selectionManager.perkChallengeItems = CardCraftManager.Instance.perkChallengeItems;
    selectionManager.sideCharacters.Show();
    selectionManager.GeneratePacks();
    CardCraftManager.Instance.SetWaitingPlayerTextChallenge("");
    CardCraftManager.Instance.EnableChallengeReadyButton(false);
    if (GameManager.Instance.IsMultiplayer())
      CardCraftManager.Instance.ChallengeReadySetButton(false);
    selectionManager.challengeBossBanners.SetBosses();
    GameManager.Instance.SceneLoaded();
    selectionManager.StartCoroutine(selectionManager.NextHero(false));
  }

  private void GeneratePacks()
  {
    UnityEngine.Random.InitState(AtOManager.Instance.GetGameId().GetDeterministicHashCode());
    for (int index1 = 0; index1 < 4; ++index1)
    {
      Hero hero = this.theTeam[index1];
      if (hero != null && !((UnityEngine.Object) hero.HeroData == (UnityEngine.Object) null))
      {
        Enums.CardClass result1 = Enums.CardClass.None;
        Enum.TryParse<Enums.CardClass>(Enum.GetName(typeof (Enums.HeroClass), (object) hero.HeroData.HeroClass), out result1);
        List<string> stringList1 = Globals.Instance.CardListNotUpgradedByClass[result1];
        Enums.CardClass result2 = Enums.CardClass.None;
        Enum.TryParse<Enums.CardClass>(Enum.GetName(typeof (Enums.HeroClass), (object) hero.HeroData.HeroSubClass.HeroClassSecondary), out result2);
        List<string> stringList2 = (List<string>) null;
        if (result2 != Enums.CardClass.None)
          stringList2 = Globals.Instance.CardListNotUpgradedByClass[result2];
        for (int round = 0; round < 3; ++round)
        {
          List<PackData> packListForClass = this.GetPackListForClass(result1, hero.HeroData.HeroSubClass.Id, round);
          for (int index2 = 0; index2 < 8; ++index2)
          {
            List<CardData> ts1 = new List<CardData>();
            if (index2 < 7)
            {
              if ((UnityEngine.Object) packListForClass[index2].Card0 != (UnityEngine.Object) null)
                ts1.Add(packListForClass[index2].Card0);
              if ((UnityEngine.Object) packListForClass[index2].Card1 != (UnityEngine.Object) null)
                ts1.Add(packListForClass[index2].Card1);
              if ((UnityEngine.Object) packListForClass[index2].Card2 != (UnityEngine.Object) null)
                ts1.Add(packListForClass[index2].Card2);
              if ((UnityEngine.Object) packListForClass[index2].Card3 != (UnityEngine.Object) null)
                ts1.Add(packListForClass[index2].Card3);
              if ((UnityEngine.Object) packListForClass[index2].Card4 != (UnityEngine.Object) null)
                ts1.Add(packListForClass[index2].Card4);
              if ((UnityEngine.Object) packListForClass[index2].Card5 != (UnityEngine.Object) null)
                ts1.Add(packListForClass[index2].Card5);
            }
            else
            {
              List<string> stringList3 = new List<string>();
              for (int index3 = 0; index3 < 6; ++index3)
              {
                bool flag = false;
                while (!flag)
                {
                  string id = stringList2 != null && UnityEngine.Random.Range(0, 2) != 0 ? stringList2[UnityEngine.Random.Range(0, stringList2.Count)] : stringList1[UnityEngine.Random.Range(0, stringList1.Count)];
                  if (!stringList3.Contains(id))
                  {
                    stringList3.Add(id);
                    ts1.Add(Globals.Instance.GetCardData(id));
                    flag = true;
                  }
                }
              }
            }
            List<CardData> ts2 = new List<CardData>();
            if (index2 < 7)
            {
              if ((UnityEngine.Object) packListForClass[index2].CardSpecial0 != (UnityEngine.Object) null)
                ts2.Add(packListForClass[index2].CardSpecial0);
              if ((UnityEngine.Object) packListForClass[index2].CardSpecial1 != (UnityEngine.Object) null)
                ts2.Add(packListForClass[index2].CardSpecial1);
            }
            else
            {
              List<string> stringList4 = new List<string>();
              for (int index4 = 0; index4 < 2; ++index4)
              {
                bool flag = false;
                while (!flag)
                {
                  string id = stringList1[UnityEngine.Random.Range(0, stringList1.Count)];
                  if (!stringList4.Contains(id))
                  {
                    CardData cardData = Globals.Instance.GetCardData(id, false);
                    if (cardData.CardRarity == Enums.CardRarity.Rare || cardData.CardRarity == Enums.CardRarity.Epic)
                    {
                      stringList4.Add(id);
                      ts2.Add(cardData);
                      flag = true;
                    }
                  }
                }
              }
            }
            List<CardData> cardDataList1 = ts1.ShuffleList<CardData>();
            List<CardData> cardDataList2 = ts2.ShuffleList<CardData>();
            string[] strArray1 = new string[this.cardsForPack];
            for (int index5 = 0; index5 < this.cardsForPack; ++index5)
              strArray1[index5] = Functions.GetCardByRarity(UnityEngine.Random.Range(0, 100), cardDataList1[index5], true);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(index1);
            stringBuilder.Append("_");
            stringBuilder.Append(round);
            stringBuilder.Append("_");
            stringBuilder.Append(index2);
            this.cardsDrafted.Add(stringBuilder.ToString(), strArray1);
            if (index2 < 7)
              this.cardsDraftedPackname.Add(stringBuilder.ToString(), packListForClass[index2].PackName);
            else
              this.cardsDraftedPackname.Add(stringBuilder.ToString(), "random");
            this.cardsDraftedPackid.Add(stringBuilder.ToString(), index2.ToString());
            bool flag1 = false;
            string str = "";
            int index6 = UnityEngine.Random.Range(0, cardDataList2.Count);
            for (int index7 = 0; !flag1 && index7 < 500; ++index7)
            {
              for (str = Functions.GetCardByRarity(UnityEngine.Random.Range(0, 100), cardDataList2[index6], true) + "_" + hero.HeroData.HeroSubClass.Id; this.cardsDraftedSpecial.ContainsValue(str); str = Functions.GetCardByRarity(UnityEngine.Random.Range(0, 100), cardDataList2[index6], true) + "_" + hero.HeroData.HeroSubClass.Id)
              {
                ++index6;
                if (index6 >= cardDataList2.Count)
                  index6 = 0;
              }
              string[] strArray2 = str.Split('_', StringSplitOptions.None);
              if (strArray2 != null && strArray2.Length >= 1)
              {
                CardData cardData = Globals.Instance.GetCardData(strArray2[0], false);
                if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && cardData.CardUpgraded != Enums.CardUpgraded.No)
                  flag1 = true;
              }
            }
            stringBuilder.Clear();
            stringBuilder.Append(index1);
            stringBuilder.Append("_");
            stringBuilder.Append(round);
            stringBuilder.Append("_");
            stringBuilder.Append(index2);
            stringBuilder.Append("_special");
            this.cardsDraftedSpecial.Add(stringBuilder.ToString(), str);
          }
        }
      }
    }
  }

  public void Resize() => this.sideCharacters.Resize();

  private void SetCurrentHeroAndRound() => AtOManager.Instance.SideBarCharacterClicked(this.currentHeroIndex);

  public void RerollFromButton()
  {
    if (GameManager.Instance.IsMultiplayer() && !(AtOManager.Instance.GetHero(this.currentHeroIndex).Owner == NetworkManager.Instance.GetPlayerNick()))
      return;
    this.Reroll(this.currentHeroIndex);
  }

  public void Reroll(int _heroId, bool fromMP = false)
  {
    if (!fromMP && GameManager.Instance.IsMultiplayer() && AtOManager.Instance.GetHero(_heroId).Owner == NetworkManager.Instance.GetPlayerNick())
      this.photonView.RPC("NET_Reroll", RpcTarget.Others, (object) _heroId);
    if (this.heroRerolledTimesArr[_heroId] >= 1)
      return;
    ++this.heroRerolledTimesArr[_heroId];
    if (_heroId != this.currentHeroIndex)
      return;
    this.ShowCardList(_heroId, comingFromReroll: true);
  }

  [PunRPC]
  private void NET_Reroll(int _heroId) => this.Reroll(_heroId, true);

  public void ChangeCharacter(int _heroIndex)
  {
    CardCraftManager.Instance.CleanChallengeBlocks();
    SubClassData heroSubClass = this.theTeam[_heroIndex].HeroData.HeroSubClass;
    this._HP.text = heroSubClass.Hp.ToString();
    int energy = heroSubClass.Energy;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(energy);
    stringBuilder.Append(" <size=1.3>");
    stringBuilder.Append(Texts.Instance.GetText("dataPerTurn").Replace("<%>", heroSubClass.EnergyTurn.ToString()));
    stringBuilder.Append("</size>");
    this._Energy.text = stringBuilder.ToString();
    this._Speed.text = heroSubClass.Speed.ToString();
    this.ShowCardList(_heroIndex, true);
  }

  public int GetCurrentRound() => this.heroRoundArr[this.currentHeroIndex];

  private void ShowCardList(int heroId, bool refreshAllPacks = false, bool comingFromReroll = false)
  {
    this.currentHeroIndex = heroId;
    GameManager.Instance.CleanTempContainer();
    CardCraftManager.Instance.ReassignChallengeButtons();
    this.WriteBonus();
    if (this.heroRoundArr[heroId] < 3)
    {
      List<int> intList = new List<int>();
      if (this.packsSelected != null && this.packsSelected.ContainsKey(heroId))
      {
        foreach (string s in this.packsSelected[heroId].Split('_', StringSplitOptions.None))
        {
          int result;
          if (int.TryParse(s, out result))
            intList.Add(result);
        }
      }
      CardCraftManager.Instance.AssignChallengeRoundCards(this.heroRoundArr[heroId] + 1, this.maxRound);
      int num1 = 8;
      int num2 = 0;
      for (int _block = 0; _block < num1; ++_block)
      {
        bool flag = true;
        bool selectedPack = false;
        string key = heroId.ToString() + "_" + this.heroRerolledTimesArr[heroId].ToString() + "_" + _block.ToString();
        if (intList.Contains(_block))
        {
          key = heroId.ToString() + "_" + this.heroPackSelectedRerolledTimesArr[heroId, _block].ToString() + "_" + _block.ToString();
          selectedPack = true;
          flag = false;
        }
        else
          CardCraftManager.Instance.ShowChallengePackSelected(false, _block);
        if (refreshAllPacks || comingFromReroll & flag)
        {
          CardCraftManager.Instance.CleanChallengeBlocks(num2);
          CardCraftManager.Instance.AssignChallengeTitle(num2, Functions.UppercaseFirst(Texts.Instance.GetText(this.cardsDraftedPackname[key])));
          for (int row = 0; row < this.cardsForPack; ++row)
            CardCraftManager.Instance.AssignChallengeCard(heroId, num2, row, this.cardsDrafted[key][row], selectedPack);
        }
        ++num2;
      }
      CardCraftManager.Instance.ShowChallengeRerollFully(true);
      CardCraftManager.Instance.ShowChallengePerks(false);
    }
    else if (this.heroRoundArr[heroId] == 3)
    {
      CardCraftManager.Instance.CleanChallengeBlocks();
      CardCraftManager.Instance.AssignChallengeRoundCards(this.heroRoundArr[heroId] + 1, this.maxRound);
      string[] strArray = this.packsSelected[heroId].Split('_', StringSplitOptions.None);
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      for (int block = 0; block < 3; ++block)
      {
        stringBuilder2.Clear();
        stringBuilder2.Append(heroId);
        stringBuilder2.Append("_");
        stringBuilder2.Append(this.heroPackSelectedRerolledTimesArr[heroId, int.Parse(strArray[block])]);
        stringBuilder2.Append("_");
        stringBuilder2.Append(strArray[block]);
        stringBuilder2.Append("_special");
        string str = this.cardsDraftedSpecial[stringBuilder2.ToString()].Split('_', StringSplitOptions.None)[0];
        stringBuilder1.Append(str);
        stringBuilder1.Append('_');
        string key = heroId.ToString() + "_" + block.ToString() + "_" + strArray[block];
        CardCraftManager.Instance.AssignChallengeTitle(block, Functions.UppercaseFirst(Texts.Instance.GetText(this.cardsDraftedPackname[key])));
      }
      bool showButtons = false;
      if (!GameManager.Instance.IsMultiplayer() || AtOManager.Instance.GetHero(heroId).Owner == NetworkManager.Instance.GetPlayerNick())
        showButtons = true;
      CardCraftManager.Instance.AssignChallengeCardSpecial(stringBuilder1.ToString(), showButtons);
      CardCraftManager.Instance.ShowChallengeRerollFully(false);
      CardCraftManager.Instance.ShowChallengePerks(false);
      CardCraftManager.Instance.controllerHorizontalIndex = -1;
    }
    else if (this.heroRoundArr[heroId] == 4)
    {
      CardCraftManager.Instance.CleanChallengeBlocks();
      CardCraftManager.Instance.ShowChallengeRerollFully(false);
      CardCraftManager.Instance.ShowChallengePerks(true);
      this.AssignPerkButtons();
    }
    CardCraftManager.Instance.ShowChallengeReroll(this.heroRerolledTimesArr[heroId] < 1);
    CardCraftManager.Instance.ActivateChallengeReroll(true);
    if (!GameManager.Instance.IsMultiplayer() || !(AtOManager.Instance.GetHero(heroId).Owner != NetworkManager.Instance.GetPlayerNick()))
      return;
    CardCraftManager.Instance.ActivateChallengeReroll(false);
  }

  private void AssignPerkButtons()
  {
    List<PerkData> perkDataClass = Globals.Instance.GetPerkDataClass((Enums.CardClass) Enum.Parse(typeof (Enums.CardClass), Enum.GetName(typeof (Enums.HeroClass), (object) this.theTeam[this.currentHeroIndex].HeroData.HeroClass)));
    int _index = 0;
    int num = 0;
    SortedDictionary<int, PerkData> sortedDictionary = new SortedDictionary<int, PerkData>();
    for (int index = 0; index < perkDataClass.Count; ++index)
    {
      if (perkDataClass[index].ObeliskPerk && !sortedDictionary.ContainsKey(perkDataClass[index].Level))
        sortedDictionary.Add(perkDataClass[index].Level, perkDataClass[index]);
    }
    foreach (KeyValuePair<int, PerkData> keyValuePair in sortedDictionary)
    {
      PerkChallengeItem perkChallengeItem = this.perkChallengeItems[_index];
      perkChallengeItem.SetPerk(this.currentHeroIndex, _index, keyValuePair.Value.Id);
      if (this.perkDrafted.ContainsKey(this.currentHeroIndex) && this.perkDrafted[this.currentHeroIndex].Contains(_index))
      {
        perkChallengeItem.SetActive(true);
        ++num;
      }
      else
        perkChallengeItem.SetActive(false);
      ++_index;
    }
    Enums.CardClass result = Enums.CardClass.None;
    Enum.TryParse<Enums.CardClass>(Enum.GetName(typeof (Enums.HeroClass), (object) this.theTeam[this.currentHeroIndex].HeroData.HeroSubClass.HeroClassSecondary), out result);
    for (int index = 0; index < this.perkChallengeItems.Length; ++index)
    {
      if (index > 20)
      {
        if (result != Enums.CardClass.None)
          this.perkChallengeItems[index].gameObject.SetActive(true);
        else
          this.perkChallengeItems[index].gameObject.SetActive(false);
      }
    }
    this.WriteSelectedPerks(this.currentHeroIndex);
    this.FinishDraw();
  }

  public void AssignPerk(int _heroId, int _perkIndex, bool fromMP = false)
  {
    if (!fromMP && GameManager.Instance.IsMultiplayer())
    {
      if (!(AtOManager.Instance.GetHero(_heroId).Owner == NetworkManager.Instance.GetPlayerNick()))
        return;
      this.photonView.RPC("NET_AssignPerk", RpcTarget.Others, (object) _heroId, (object) _perkIndex);
    }
    if (!this.perkDrafted.ContainsKey(_heroId))
      this.perkDrafted.Add(_heroId, new List<int>());
    if (this.perkDrafted[_heroId].Contains(_perkIndex))
    {
      this.perkDrafted[_heroId].Remove(_perkIndex);
      if (GameManager.Instance.IsMultiplayer() && this.statusReady && !fromMP)
        this.Ready();
    }
    else if (this.perkDrafted[_heroId].Count < this.maxSelectablePerks)
      this.perkDrafted[_heroId].Add(_perkIndex);
    if (this.currentHeroIndex != _heroId)
      return;
    this.AssignPerkButtons();
  }

  [PunRPC]
  private void NET_AssignPerk(int _heroId, int _perk) => this.AssignPerk(_heroId, _perk, true);

  private void WriteSelectedPerks(int _heroId) => CardCraftManager.Instance.AssignChallengeRoundPerks(this.perkDrafted[_heroId].Count, this.maxSelectablePerks);

  public int GetActiveHero() => this.currentHeroIndex;

  private void SetDefaultCards(int _heroIndex)
  {
    Hero hero = this.theTeam[_heroIndex];
    if (hero == null || (UnityEngine.Object) hero.HeroData == (UnityEngine.Object) null)
      return;
    foreach (KeyValuePair<string, PackData> keyValuePair in Globals.Instance.PackDataSource)
    {
      if ((UnityEngine.Object) keyValuePair.Value.RequiredClass != (UnityEngine.Object) null && keyValuePair.Value.RequiredClass.Id == hero.HeroData.HeroSubClass.Id)
      {
        if ((UnityEngine.Object) keyValuePair.Value.Card0 != (UnityEngine.Object) null)
          AtOManager.Instance.AddCardToHero(_heroIndex, this.StarterUpgradeCard(keyValuePair.Value.Card0.Id));
        if ((UnityEngine.Object) keyValuePair.Value.Card1 != (UnityEngine.Object) null)
          AtOManager.Instance.AddCardToHero(_heroIndex, this.StarterUpgradeCard(keyValuePair.Value.Card1.Id));
        if ((UnityEngine.Object) keyValuePair.Value.Card2 != (UnityEngine.Object) null)
          AtOManager.Instance.AddCardToHero(_heroIndex, this.StarterUpgradeCard(keyValuePair.Value.Card2.Id));
        if ((UnityEngine.Object) keyValuePair.Value.Card3 != (UnityEngine.Object) null)
          AtOManager.Instance.AddCardToHero(_heroIndex, this.StarterUpgradeCard(keyValuePair.Value.Card3.Id));
        if (!((UnityEngine.Object) keyValuePair.Value.Card4 != (UnityEngine.Object) null))
          break;
        AtOManager.Instance.AddCardToHero(_heroIndex, this.StarterUpgradeCard(keyValuePair.Value.Card4.Id));
        break;
      }
    }
  }

  private string StarterUpgradeCard(string _cardId)
  {
    if (!GameManager.Instance.IsWeeklyChallenge())
    {
      int obeliskMadness = AtOManager.Instance.GetObeliskMadness();
      if (obeliskMadness >= 5)
      {
        CardData cardData = Globals.Instance.GetCardData(_cardId, false);
        if (cardData.Starter)
          return obeliskMadness <= 8 ? cardData.UpgradesTo1 : cardData.UpgradesTo2;
      }
    }
    else
    {
      CardData cardData = Globals.Instance.GetCardData(_cardId, false);
      if (cardData.Starter)
        return cardData.UpgradesTo2;
    }
    return _cardId;
  }

  public List<PackData> GetPackListForClass(
    Enums.CardClass cardClass,
    string subclassId,
    int round)
  {
    List<PackData> packListForClass = new List<PackData>();
    SubClassData subClassData = Globals.Instance.GetSubClassData(subclassId);
    packListForClass.Add(subClassData.ChallengePack0);
    packListForClass.Add(subClassData.ChallengePack1);
    packListForClass.Add(subClassData.ChallengePack2);
    packListForClass.Add(subClassData.ChallengePack3);
    packListForClass.Add(subClassData.ChallengePack4);
    packListForClass.Add(subClassData.ChallengePack5);
    packListForClass.Add(subClassData.ChallengePack6);
    return packListForClass;
  }

  public void SelectPack(int _heroId, int _pack, bool fromMP = false)
  {
    if (!fromMP && GameManager.Instance.IsMultiplayer() && AtOManager.Instance.GetHero(_heroId).Owner == NetworkManager.Instance.GetPlayerNick())
      this.photonView.RPC("NET_SelectPack", RpcTarget.Others, (object) _heroId, (object) _pack);
    StringBuilder stringBuilder = new StringBuilder();
    if (this.heroRoundArr[_heroId] < 3)
    {
      stringBuilder.Append(_heroId);
      stringBuilder.Append("_");
      stringBuilder.Append(this.heroRerolledTimesArr[_heroId]);
      stringBuilder.Append("_");
      stringBuilder.Append(_pack);
      foreach (string _cardName in this.cardsDrafted[stringBuilder.ToString()])
        AtOManager.Instance.AddCardToHero(_heroId, _cardName);
      if (!this.packsSelected.ContainsKey(_heroId))
        this.packsSelected.Add(_heroId, "");
      Dictionary<int, string> packsSelected = this.packsSelected;
      int key = _heroId;
      packsSelected[key] = packsSelected[key] + _pack.ToString() + "_";
      this.heroPackSelectedRerolledTimesArr[_heroId, _pack] = this.heroRerolledTimesArr[_heroId];
      if (this.currentHeroIndex == _heroId)
        CardCraftManager.Instance.ShowChallengePackSelected(true, _pack);
    }
    else
    {
      string[] strArray = this.packsSelected[_heroId].Split('_', StringSplitOptions.None);
      stringBuilder.Append(_heroId);
      stringBuilder.Append("_");
      stringBuilder.Append(this.heroPackSelectedRerolledTimesArr[_heroId, int.Parse(strArray[_pack])]);
      stringBuilder.Append("_");
      stringBuilder.Append(strArray[_pack]);
      stringBuilder.Append("_special");
      string _cardName = this.cardsDraftedSpecial[stringBuilder.ToString()].Split('_', StringSplitOptions.None)[0];
      AtOManager.Instance.AddCardToHero(_heroId, _cardName);
    }
    ++this.heroRoundArr[_heroId];
    this.sideCharacters.RefreshCards(_heroId);
    if (this.currentHeroIndex != _heroId)
      return;
    CardCraftManager.Instance.ShowChallengeButtons(false, _pack);
    CardCraftManager.Instance.CreateDeck(this.currentHeroIndex);
    if (this.heroRoundArr[this.currentHeroIndex] <= this.maxRound)
      this.StartCoroutine(this.NextRound());
    else
      this.StartCoroutine(this.NextHero());
  }

  [PunRPC]
  private void NET_SelectPack(int _heroId, int _pack) => this.SelectPack(_heroId, _pack, true);

  private IEnumerator NextRound()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.15f);
    this.ShowCardList(this.currentHeroIndex);
  }

  public void NextHeroFunc(bool _isRight)
  {
    int currentHeroIndex = this.currentHeroIndex;
    int num = !_isRight ? currentHeroIndex - 1 : currentHeroIndex + 1;
    if (num > 3)
      num = 0;
    else if (num < 0)
      num = 3;
    GameObject gameObject = GameObject.Find("/SideCharacters/OverCharacter" + num.ToString());
    if (!((UnityEngine.Object) gameObject != (UnityEngine.Object) null))
      return;
    gameObject.transform.GetComponent<OverCharacter>().Clicked();
  }

  private IEnumerator NextHero(bool timeOut = true)
  {
    if (timeOut)
      yield return (object) Globals.Instance.WaitForSeconds(0.15f);
    bool flag = false;
    this.currentHeroIndex = 0;
    while (this.currentHeroIndex < 4 && !flag)
    {
      if (this.heroRoundArr[this.currentHeroIndex] < 3 && (!GameManager.Instance.IsMultiplayer() || AtOManager.Instance.GetHero(this.currentHeroIndex).Owner == NetworkManager.Instance.GetPlayerNick()))
        flag = true;
      if (!flag)
        ++this.currentHeroIndex;
    }
    if (this.currentHeroIndex < 4)
    {
      this.SetCurrentHeroAndRound();
      this.ShowCardList(this.currentHeroIndex);
    }
    else
      this.FinishDraw();
  }

  private void FinishDraw()
  {
    bool flag = true;
    for (int index = 0; index < 4; ++index)
    {
      this.sideCharacters.ShowChallengeButtons(index);
      if (this.perkDrafted != null && this.perkDrafted[index] != null && this.perkDrafted[index].Count < 4)
        this.sideCharacters.ShowChallengeButtons(index, false);
    }
    if (!GameManager.Instance.IsMultiplayer())
    {
      for (int key = 0; key < 4; ++key)
      {
        if (this.theTeam[key] != null && !((UnityEngine.Object) this.theTeam[key].HeroData == (UnityEngine.Object) null) && this.perkDrafted != null && this.perkDrafted[key] != null && this.perkDrafted[key].Count < 4)
        {
          flag = false;
          break;
        }
      }
    }
    else
    {
      for (int index = 0; index < 4; ++index)
      {
        if (this.theTeam[index] != null && !((UnityEngine.Object) this.theTeam[index].HeroData == (UnityEngine.Object) null) && AtOManager.Instance.GetHero(index).Owner == NetworkManager.Instance.GetPlayerNick() && this.perkDrafted != null && this.perkDrafted[index] != null && this.perkDrafted[index].Count < 4)
        {
          flag = false;
          break;
        }
      }
    }
    if (flag)
    {
      if (!GameManager.Instance.IsMultiplayer())
      {
        CardCraftManager.Instance.ChallengeReadySetButton(true);
        CardCraftManager.Instance.EnableChallengeReadyButton(true);
      }
      else
        CardCraftManager.Instance.EnableChallengeReadyButton(true);
    }
    else
    {
      CardCraftManager.Instance.EnableChallengeReadyButton(false);
      CardCraftManager.Instance.ChallengeReadySetButton(false);
    }
  }

  private void FinishObeliskDraft()
  {
    AtOManager.Instance.SetPlayerPerksChallenge(this.perkDrafted);
    AtOManager.Instance.FinishObeliskDraft();
  }

  public void Ready()
  {
    if (!GameManager.Instance.IsMultiplayer())
    {
      this.FinishObeliskDraft();
    }
    else
    {
      if (this.manualReadyCo != null)
        this.StopCoroutine(this.manualReadyCo);
      this.statusReady = !this.statusReady;
      NetworkManager.Instance.SetManualReady(this.statusReady);
      if (this.statusReady)
      {
        CardCraftManager.Instance.ChallengeReadySetButton(true);
        if (!NetworkManager.Instance.IsMaster())
          return;
        this.manualReadyCo = this.StartCoroutine(this.CheckForAllManualReady());
      }
      else
        CardCraftManager.Instance.ChallengeReadySetButton(false);
    }
  }

  public void SetWaitingPlayersText(string msg) => CardCraftManager.Instance.SetWaitingPlayerTextChallenge(msg);

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
    this.FinishObeliskDraft();
  }

  private void AddToDictDT(string dt, int index)
  {
    if (!(dt != ""))
      return;
    dt = dt.ToLower();
    if (!this.dictBonus.ContainsKey(dt))
      this.dictBonus.Add(dt, 0);
    if (index <= -1)
      return;
    this.dictBonus[dt]++;
    if (this.currentHeroIndex != index)
      return;
    if (!this.dictBonusSingle.ContainsKey(dt))
      this.dictBonusSingle.Add(dt, 0);
    this.dictBonusSingle[dt]++;
  }

  private void AddToDictAU(string au, int index)
  {
    if (!(au != ""))
      return;
    au = au.ToLower();
    if (!this.dictAura.ContainsKey(au))
      this.dictAura.Add(au, 0);
    if (index <= -1)
      return;
    this.dictAura[au]++;
    if (this.currentHeroIndex != index)
      return;
    if (!this.dictAuraSingle.ContainsKey(au))
      this.dictAuraSingle.Add(au, 0);
    this.dictAuraSingle[au]++;
  }

  private void AddToDictEnergy(string value)
  {
    if (int.Parse(value) > 5)
      value = "5";
    if (this.dictEnergyCost.ContainsKey(value))
      this.dictEnergyCost[value]++;
    else
      this.dictEnergyCost.Add(value, 1f);
  }

  private void WriteBonusFullParty()
  {
    this.dictBonus.Clear();
    this.dictBonusSingle.Clear();
    this.dictAura.Clear();
    this.dictAuraSingle.Clear();
    this.dictEnergyCost.Clear();
    this.AddToDictDT("Slashing", -1);
    this.AddToDictDT("Blunt", -1);
    this.AddToDictDT("Piercing", -1);
    this.AddToDictDT("Fire", -1);
    this.AddToDictDT("Cold", -1);
    this.AddToDictDT("Lightning", -1);
    this.AddToDictDT("Mind", -1);
    this.AddToDictDT("Holy", -1);
    this.AddToDictDT("Shadow", -1);
    this.AddToDictAU("block", -1);
    this.AddToDictAU("shield", -1);
    for (int index1 = 0; index1 < 4; ++index1)
    {
      List<string> cards = AtOManager.Instance.GetHero(index1).Cards;
      for (int index2 = 0; index2 < cards.Count; ++index2)
      {
        CardData cardData = Globals.Instance.GetCardData(cards[index2], false);
        if (index1 == this.currentHeroIndex)
          this.AddToDictEnergy(cardData.EnergyCost.ToString());
        if (cardData.DamageType != Enums.DamageType.None)
          this.AddToDictDT(Enum.GetName(typeof (Enums.DamageType), (object) cardData.DamageType), index1);
        if (cardData.DamageType2 != Enums.DamageType.None)
          this.AddToDictDT(Enum.GetName(typeof (Enums.DamageType), (object) cardData.DamageType2), index1);
        if ((UnityEngine.Object) cardData.Aura != (UnityEngine.Object) null)
          this.AddToDictAU(cardData.Aura.Id, index1);
        if ((UnityEngine.Object) cardData.AuraSelf != (UnityEngine.Object) null)
          this.AddToDictAU(cardData.AuraSelf.Id, index1);
        if ((UnityEngine.Object) cardData.Aura2 != (UnityEngine.Object) null)
          this.AddToDictAU(cardData.Aura2.Id, index1);
        if ((UnityEngine.Object) cardData.AuraSelf2 != (UnityEngine.Object) null)
          this.AddToDictAU(cardData.AuraSelf2.Id, index1);
        if ((UnityEngine.Object) cardData.Aura3 != (UnityEngine.Object) null)
          this.AddToDictAU(cardData.Aura3.Id, index1);
        if ((UnityEngine.Object) cardData.AuraSelf3 != (UnityEngine.Object) null)
          this.AddToDictAU(cardData.AuraSelf3.Id, index1);
        if ((UnityEngine.Object) cardData.Curse != (UnityEngine.Object) null)
          this.AddToDictAU(cardData.Curse.Id, index1);
        if ((UnityEngine.Object) cardData.CurseSelf != (UnityEngine.Object) null)
          this.AddToDictAU(cardData.CurseSelf.Id, index1);
        if ((UnityEngine.Object) cardData.Curse2 != (UnityEngine.Object) null)
          this.AddToDictAU(cardData.Curse2.Id, index1);
        if ((UnityEngine.Object) cardData.CurseSelf2 != (UnityEngine.Object) null)
          this.AddToDictAU(cardData.CurseSelf2.Id, index1);
        if ((UnityEngine.Object) cardData.Curse3 != (UnityEngine.Object) null)
          this.AddToDictAU(cardData.Curse3.Id, index1);
        if ((UnityEngine.Object) cardData.CurseSelf3 != (UnityEngine.Object) null)
          this.AddToDictAU(cardData.CurseSelf3.Id, index1);
      }
    }
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<sprite name=cards><size=+.4>");
    stringBuilder.Append(Texts.Instance.GetText("damageTypes"));
    stringBuilder.Append("</size><br><indent=4>");
    int num1 = 0;
    foreach (KeyValuePair<string, int> dictBonu in this.dictBonus)
    {
      if (dictBonu.Value > 0)
      {
        stringBuilder.Append("<mspace=3>");
        if (num1 > 0)
          stringBuilder.Append("<color=#666><voffset=.4><size=-.8>|</size></voffset></color>");
        stringBuilder.Append("<size=+.2><sprite name=");
        stringBuilder.Append(dictBonu.Key.ToLower());
        stringBuilder.Append("></size><space=1><mspace=1>");
        stringBuilder.Append("<color=#FC0><size=+.2>");
        if (this.dictBonusSingle.ContainsKey(dictBonu.Key))
          stringBuilder.Append(this.dictBonusSingle[dictBonu.Key]);
        else
          stringBuilder.Append("0");
        stringBuilder.Append("</size></color>");
        stringBuilder.Append("/");
        stringBuilder.Append(dictBonu.Value);
        ++num1;
      }
    }
    stringBuilder.Append("</mspace>");
    CardCraftManager.Instance.cardChallengeBonus.text = stringBuilder.ToString();
    stringBuilder.Clear();
    stringBuilder.Append("<line-height=60%><br><br><line-height=100%>");
    stringBuilder.Append("<indent=0><sprite name=cards><size=+.4>");
    stringBuilder.Append(Texts.Instance.GetText("combatEffects"));
    stringBuilder.Append("</size><br><indent=4>");
    int num2 = 0;
    foreach (KeyValuePair<string, int> keyValuePair in this.dictAura)
    {
      if (keyValuePair.Value > 0)
      {
        stringBuilder.Append("<nobr>");
        stringBuilder.Append("<mspace=3>");
        if (num2 % 8 == 0)
          stringBuilder.Append("<br>");
        else if (num2 > 0)
          stringBuilder.Append("<color=#666><voffset=.4><size=-.8>|</size></voffset></color>");
        stringBuilder.Append("<size=+.2><sprite name=");
        stringBuilder.Append(keyValuePair.Key.ToLower());
        stringBuilder.Append("></size><space=1><mspace=1>");
        stringBuilder.Append("<color=#FC0><size=+.2>");
        if (this.dictAuraSingle.ContainsKey(keyValuePair.Key))
          stringBuilder.Append(this.dictAuraSingle[keyValuePair.Key]);
        else
          stringBuilder.Append("0");
        stringBuilder.Append("</size></color>");
        stringBuilder.Append("/");
        stringBuilder.Append(keyValuePair.Value);
        stringBuilder.Append("</nobr>");
        ++num2;
      }
    }
    stringBuilder.Append("</mspace>");
    CardCraftManager.Instance.cardChallengeBonus.text += stringBuilder.ToString();
  }

  private void WriteBonus()
  {
    this.dictBonus.Clear();
    this.dictBonusSingle.Clear();
    this.dictAura.Clear();
    this.dictAuraSingle.Clear();
    this.dictEnergyCost.Clear();
    this.AddToDictDT("Slashing", -1);
    this.AddToDictDT("Blunt", -1);
    this.AddToDictDT("Piercing", -1);
    this.AddToDictDT("Fire", -1);
    this.AddToDictDT("Cold", -1);
    this.AddToDictDT("Lightning", -1);
    this.AddToDictDT("Mind", -1);
    this.AddToDictDT("Holy", -1);
    this.AddToDictDT("Shadow", -1);
    this.AddToDictAU("heal", -1);
    this.AddToDictAU("energy", -1);
    this.AddToDictAU("block", -1);
    this.AddToDictAU("shield", -1);
    for (int index1 = 0; index1 < 4; ++index1)
    {
      if (index1 == this.currentHeroIndex)
      {
        List<string> cards = AtOManager.Instance.GetHero(index1).Cards;
        for (int index2 = 0; index2 < cards.Count; ++index2)
        {
          CardData cardData = Globals.Instance.GetCardData(cards[index2], false);
          this.AddToDictEnergy(cardData.EnergyCost.ToString());
          if (cardData.DamageType != Enums.DamageType.None)
            this.AddToDictDT(Enum.GetName(typeof (Enums.DamageType), (object) cardData.DamageType), index1);
          if (cardData.DamageType2 != Enums.DamageType.None)
            this.AddToDictDT(Enum.GetName(typeof (Enums.DamageType), (object) cardData.DamageType2), index1);
          if (cardData.EnergyRecharge > 0)
            this.AddToDictAU("energy", index1);
          if (cardData.Heal > 0)
            this.AddToDictAU("heal", index1);
          if ((UnityEngine.Object) cardData.Aura != (UnityEngine.Object) null)
            this.AddToDictAU(cardData.Aura.Id, index1);
          if ((UnityEngine.Object) cardData.AuraSelf != (UnityEngine.Object) null)
            this.AddToDictAU(cardData.AuraSelf.Id, index1);
          if ((UnityEngine.Object) cardData.Aura2 != (UnityEngine.Object) null)
            this.AddToDictAU(cardData.Aura2.Id, index1);
          if ((UnityEngine.Object) cardData.AuraSelf2 != (UnityEngine.Object) null)
            this.AddToDictAU(cardData.AuraSelf2.Id, index1);
          if ((UnityEngine.Object) cardData.Aura3 != (UnityEngine.Object) null)
            this.AddToDictAU(cardData.Aura3.Id, index1);
          if ((UnityEngine.Object) cardData.AuraSelf3 != (UnityEngine.Object) null)
            this.AddToDictAU(cardData.AuraSelf3.Id, index1);
          if ((UnityEngine.Object) cardData.Curse != (UnityEngine.Object) null)
            this.AddToDictAU(cardData.Curse.Id, index1);
          if ((UnityEngine.Object) cardData.CurseSelf != (UnityEngine.Object) null)
            this.AddToDictAU(cardData.CurseSelf.Id, index1);
          if ((UnityEngine.Object) cardData.Curse2 != (UnityEngine.Object) null)
            this.AddToDictAU(cardData.Curse2.Id, index1);
          if ((UnityEngine.Object) cardData.CurseSelf2 != (UnityEngine.Object) null)
            this.AddToDictAU(cardData.CurseSelf2.Id, index1);
          if ((UnityEngine.Object) cardData.Curse3 != (UnityEngine.Object) null)
            this.AddToDictAU(cardData.Curse3.Id, index1);
          if ((UnityEngine.Object) cardData.CurseSelf3 != (UnityEngine.Object) null)
            this.AddToDictAU(cardData.CurseSelf3.Id, index1);
        }
      }
    }
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<sprite name=cards><size=-.6><color=#FFF>");
    stringBuilder.Append(Texts.Instance.GetText("damageTypes"));
    stringBuilder.Append("</color></size><br><indent=4>");
    int num1 = 0;
    foreach (KeyValuePair<string, int> dictBonu in this.dictBonus)
    {
      if (dictBonu.Value > 0)
      {
        stringBuilder.Append("<mspace=2.6>");
        if (num1 > 0)
          stringBuilder.Append("<color=#666><voffset=.6><size=-1.5>|</size></voffset></color>");
        stringBuilder.Append("<sprite name=");
        stringBuilder.Append(dictBonu.Key.ToLower());
        stringBuilder.Append("><space=1><mspace=1.4>");
        stringBuilder.Append("<color=#FC0>");
        if (this.dictBonusSingle.ContainsKey(dictBonu.Key))
          stringBuilder.Append(this.dictBonusSingle[dictBonu.Key]);
        else
          stringBuilder.Append("0");
        stringBuilder.Append("</color>");
        ++num1;
      }
    }
    stringBuilder.Append("</mspace>");
    CardCraftManager.Instance.cardChallengeBonus.text = stringBuilder.ToString();
    stringBuilder.Clear();
    stringBuilder.Append("<line-height=70%><br><br></line-height><indent=0><sprite name=cards><size=-.6><color=#FFF>");
    stringBuilder.Append(Texts.Instance.GetText("combatEffects"));
    stringBuilder.Append("</color></size><br><indent=4>");
    int num2 = 0;
    foreach (KeyValuePair<string, int> keyValuePair in this.dictAura)
    {
      if (keyValuePair.Value > 0)
      {
        stringBuilder.Append("<nobr>");
        stringBuilder.Append("<mspace=2.6>");
        if (num2 > 0)
        {
          if (num2 % 14 == 0)
            stringBuilder.Append("<br>");
          else
            stringBuilder.Append("<color=#666><voffset=.6><size=-1.5>|</size></voffset></color>");
        }
        stringBuilder.Append("<sprite name=");
        stringBuilder.Append(keyValuePair.Key.ToLower());
        stringBuilder.Append("><space=1><mspace=1.4>");
        stringBuilder.Append("<color=#FC0>");
        if (this.dictAuraSingle.ContainsKey(keyValuePair.Key))
          stringBuilder.Append(this.dictAuraSingle[keyValuePair.Key]);
        else
          stringBuilder.Append("0");
        stringBuilder.Append("</color>");
        stringBuilder.Append("</nobr>");
        ++num2;
      }
    }
    stringBuilder.Append("</mspace>");
    CardCraftManager.Instance.cardChallengeBonus.text += stringBuilder.ToString();
  }
}
