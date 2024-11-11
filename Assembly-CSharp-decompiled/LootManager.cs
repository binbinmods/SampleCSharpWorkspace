// Decompiled with JetBrains decompiler
// Type: LootManager
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

public class LootManager : MonoBehaviour
{
  private PhotonView photonView;
  public Transform sceneCamera;
  public CharacterWindowUI characterWindowUI;
  public Transform cardContainer;
  public CharacterLoot[] characterLootArray = new CharacterLoot[4];
  public Transform[] characterPotraitArray = new Transform[4];
  private List<string> lootedItems = new List<string>();
  private string lootListId = "";
  private int goldQuantity;
  private int goldSelected;
  private int activeCharacter = -1;
  private Dictionary<string, CardItem> cardCI = new Dictionary<string, CardItem>();
  private Dictionary<string, string> cardType = new Dictionary<string, string>();
  private List<int> characterOrder;
  public BotonGeneric botonGold;
  public TMP_Text subtitle;
  public TMP_Text description;
  private List<int> listCharacterOrder;
  private bool isMyLoot;
  private bool[] looted = new bool[4];
  private bool finishLoot;
  private bool reseting;
  public Transform buttonRestart;
  private string teamAtOToJson;
  private string[] keyListGold;
  private int[] valueListGold;
  private int playerGold;
  private string[] keyListDust;
  private int[] valueListDust;
  private int playerDust;
  private int totalGoldGained;
  private int totalDustGained;
  private int expGained;
  private int atoGoldGained;
  private int atoDustGained;
  private int clientGold;
  private int clientDust;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();

  public static LootManager Instance { get; private set; }

  public bool IsMyLoot
  {
    get => this.isMyLoot;
    set => this.isMyLoot = value;
  }

  private void Awake()
  {
    if ((UnityEngine.Object) GameManager.Instance == (UnityEngine.Object) null)
    {
      SceneStatic.LoadByName("Loot");
    }
    else
    {
      if ((UnityEngine.Object) LootManager.Instance == (UnityEngine.Object) null)
        LootManager.Instance = this;
      else if ((UnityEngine.Object) LootManager.Instance != (UnityEngine.Object) this)
        UnityEngine.Object.Destroy((UnityEngine.Object) this);
      this.sceneCamera.gameObject.SetActive(false);
      this.photonView = PhotonView.Get((Component) this);
      NetworkManager.Instance.StartStopQueue(true);
    }
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
      this.totalGoldGained = AtOManager.Instance.totalGoldGained;
      this.totalDustGained = AtOManager.Instance.totalDustGained;
      this.atoGoldGained = PlayerManager.Instance.GoldGained;
      this.atoDustGained = PlayerManager.Instance.DustGained;
      this.expGained = PlayerManager.Instance.ExpGained;
    }
    else
    {
      this.clientGold = AtOManager.Instance.GetPlayerGold();
      this.clientDust = AtOManager.Instance.GetPlayerDust();
    }
    for (int index = 0; index < 4; ++index)
    {
      this.characterLootArray[index].gameObject.SetActive(false);
      this.looted[index] = false;
    }
    this.description.transform.gameObject.SetActive(false);
    this.botonGold.transform.gameObject.SetActive(false);
    AtOManager.Instance.ClearReroll();
    this.StartCoroutine(this.SetLoot());
  }

  private IEnumerator SetLoot()
  {
    if (GameManager.Instance.IsMultiplayer())
    {
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersReady("doLoot"))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("Game ready, Everybody checked doLoot");
        NetworkManager.Instance.PlayersNetworkContinue("doLoot");
        this.lootListId = AtOManager.Instance.GetLootListId();
        UnityEngine.Random.InitState((AtOManager.Instance.GetGameId() + "_" + AtOManager.Instance.mapVisitedNodes.Count.ToString() + "_" + AtOManager.Instance.currentMapNode + "_" + this.lootListId).GetDeterministicHashCode());
        this.listCharacterOrder = AtOManager.Instance.GetLootCharacterOrder();
        this.SetLootOrder();
      }
      else
      {
        NetworkManager.Instance.SetWaitingSyncro("doLoot", true);
        NetworkManager.Instance.SetWaitingSyncro("setloot", true);
        NetworkManager.Instance.SetStatusReady("doLoot");
        while (NetworkManager.Instance.WaitingSyncro["doLoot"])
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("doLoot, we can continue!");
      }
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("SetWaitingSyncro setloot");
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersReady("setloot"))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("Game ready, Everybody checked setloot");
        NetworkManager.Instance.PlayersNetworkContinue("setloot");
      }
      else
      {
        while (NetworkManager.Instance.WaitingSyncro["setloot"])
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("setloot, we can continue!");
      }
      this.SetCharacters();
    }
    else
    {
      this.lootListId = AtOManager.Instance.GetLootListId();
      this.characterOrder = new List<int>();
      this.characterOrder.Add(0);
      this.characterOrder.Add(1);
      this.characterOrder.Add(2);
      this.characterOrder.Add(3);
      this.SetCharacters();
    }
    GameManager.Instance.SceneLoaded();
  }

  private void SetLootOrder()
  {
    this.characterOrder = new List<int>();
    this.characterOrder.Add(this.listCharacterOrder[0]);
    this.characterOrder.Add(this.listCharacterOrder[1]);
    this.characterOrder.Add(this.listCharacterOrder[2]);
    this.characterOrder.Add(this.listCharacterOrder[3]);
    this.photonView.RPC("NET_ShareLootIdAndOrder", RpcTarget.Others, (object) this.lootListId, (object) (byte) this.characterOrder[0], (object) (byte) this.characterOrder[1], (object) (byte) this.characterOrder[2], (object) (byte) this.characterOrder[3]);
  }

  [PunRPC]
  private void NET_ShareLootIdAndOrder(
    string _lootListId,
    byte char0,
    byte char1,
    byte char2,
    byte char3)
  {
    this.lootListId = _lootListId;
    this.characterOrder = new List<int>();
    this.characterOrder.Add((int) char0);
    this.characterOrder.Add((int) char1);
    this.characterOrder.Add((int) char2);
    this.characterOrder.Add((int) char3);
    NetworkManager.Instance.SetStatusReady("setloot");
  }

  private void SetCharacters()
  {
    for (int index = 0; index < 4; ++index)
    {
      int _heroIndex = this.characterOrder[index];
      this.characterLootArray[index].AssignHero(_heroIndex);
    }
    this.ContinueSetCharacters();
  }

  private void ContinueSetCharacters()
  {
    this.StartCoroutine(this.ShowCharacters());
    this.StartCoroutine(this.ShowItemsForLoot(this.lootListId));
  }

  public void ChangeCharacter(int _heroIndex)
  {
    if (GameManager.Instance.IsMultiplayer() || this.activeCharacter == -1 || this.looted[_heroIndex])
      return;
    this.activeCharacter = _heroIndex;
    this.ActivateCharacter(this.activeCharacter);
  }

  private void ActivateCharacter(int charToActivate) => this.StartCoroutine(this.ActivateCharacterCo(charToActivate));

  private IEnumerator ActivateCharacterCo(int charToActivate)
  {
    if (GameManager.Instance.IsMultiplayer())
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersReady("activateCharacter" + charToActivate.ToString()))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("Game ready, Everybody checked activateCharacter" + charToActivate.ToString());
        NetworkManager.Instance.PlayersNetworkContinue("activateCharacter" + charToActivate.ToString());
        yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      }
      else
      {
        NetworkManager.Instance.SetWaitingSyncro("activateCharacter" + charToActivate.ToString(), true);
        NetworkManager.Instance.SetStatusReady("activateCharacter" + charToActivate.ToString());
        while (NetworkManager.Instance.WaitingSyncro["activateCharacter" + charToActivate.ToString()])
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("activateCharacter" + charToActivate.ToString() + ", we can continue!");
      }
    }
    this.activeCharacter = charToActivate;
    bool flag = false;
    while (!flag)
    {
      Hero hero = AtOManager.Instance.GetHero(this.characterOrder[this.activeCharacter]);
      if (hero == null || (UnityEngine.Object) hero.HeroData == (UnityEngine.Object) null)
        ++this.activeCharacter;
      else
        flag = true;
    }
    for (int i = 0; i < 4; ++i)
    {
      if (i == this.activeCharacter)
      {
        Hero hero = AtOManager.Instance.GetHero(this.characterOrder[i]);
        if (hero != null && !((UnityEngine.Object) hero.HeroData == (UnityEngine.Object) null))
        {
          this.characterLootArray[i].Activate(true);
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append(Texts.Instance.GetText("itemRewardsChosing"));
          stringBuilder.Append(" <size=+.5><color=#FFF>");
          stringBuilder.Append(hero.HeroData.HeroSubClass.CharacterName);
          stringBuilder.Append("</color></size>");
          this.subtitle.text = stringBuilder.ToString();
          if (GameManager.Instance.IsMultiplayer())
          {
            this.isMyLoot = hero.Owner == NetworkManager.Instance.GetPlayerNick();
            yield return (object) Globals.Instance.WaitForSeconds(0.2f);
            this.EnableLoot(this.isMyLoot);
          }
          else
            this.isMyLoot = true;
        }
      }
      else
        this.characterLootArray[i].Activate(false);
    }
  }

  private IEnumerator ShowCharacters()
  {
    for (int i = 0; i < 4; ++i)
    {
      if (AtOManager.Instance.GetHero(this.characterOrder[i]) != null && (UnityEngine.Object) AtOManager.Instance.GetHero(this.characterOrder[i]).HeroData != (UnityEngine.Object) null)
      {
        yield return (object) Globals.Instance.WaitForSeconds(0.1f);
        this.characterLootArray[i].gameObject.SetActive(true);
      }
    }
  }

  private IEnumerator ShowItemsForLoot(string itemListId)
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    List<string> itemList = new List<string>();
    itemList = AtOManager.Instance.GetItemList(itemListId);
    if (itemList != null)
    {
      this.goldQuantity = Globals.Instance.GetLootData(itemListId).GoldQuantity;
      if (MadnessManager.Instance.IsMadnessTraitActive("poverty") || AtOManager.Instance.IsChallengeTraitActive("poverty"))
      {
        if (!GameManager.Instance.IsObeliskChallenge())
          this.goldQuantity -= Functions.FuncRoundToInt((float) this.goldQuantity * 0.5f);
        else
          this.goldQuantity -= Functions.FuncRoundToInt((float) this.goldQuantity * 0.3f);
      }
      this.description.transform.gameObject.SetActive(true);
      this.botonGold.transform.gameObject.SetActive(true);
      this.botonGold.SetText(this.goldQuantity.ToString());
      int position = 0;
      Functions.FuncRoundToInt((float) (itemList.Count / 2));
      for (int i = 0; i < itemList.Count; ++i)
      {
        string key = itemList[i] + "_" + i.ToString();
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.cardContainer);
        CardItem component = gameObject.GetComponent<CardItem>();
        this.cardCI.Add(key, component);
        gameObject.name = key;
        component.SetCard(itemList[i], false);
        component.DisableCollider();
        Vector3 cardPosition = Functions.GetCardPosition(new Vector3(-2f, -1.9f, 0.0f), position, itemList.Count);
        float x = cardPosition.x;
        float y = cardPosition.y;
        component.transform.position = new Vector3(x, y, 0.0f);
        component.DoReward(false, fromLoot: true);
        component.SetDestination(new Vector3(x, y, 0.0f));
        component.SetLocalScale(new Vector3(1.15f, 1.15f, 1f));
        component.SetDestinationLocalScale(1.15f);
        component.cardmakebig = true;
        component.cardoutsideloot = true;
        component.lootId = key;
        CardData cardData = Globals.Instance.GetCardData(itemList[i]);
        if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
        {
          if (cardData.CardType == Enums.CardType.Weapon)
            this.cardType.Add(key, "Weapon");
          else if (cardData.CardType == Enums.CardType.Armor)
            this.cardType.Add(key, "Armor");
          else if (cardData.CardType == Enums.CardType.Jewelry)
            this.cardType.Add(key, "Jewelry");
          else if (cardData.CardType == Enums.CardType.Accesory)
            this.cardType.Add(key, "Accesory");
          else if (cardData.CardType == Enums.CardType.Pet)
            this.cardType.Add(key, "Pet");
        }
        if (GameManager.Instance.IsMultiplayer() && !this.isMyLoot)
          component.ShowDisableReward();
        PlayerManager.Instance.CardUnlock(itemList[i], cardItem: component);
        ++position;
        yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      }
      this.ActivateCharacter(0);
    }
  }

  private void EnableLoot(bool state)
  {
    if (state)
      this.botonGold.Enable();
    else
      this.botonGold.Disable();
    foreach (KeyValuePair<string, CardItem> keyValuePair in this.cardCI)
    {
      bool flag = state;
      if (this.lootedItems.Contains(keyValuePair.Key))
        flag = false;
      keyValuePair.Value.ShowDisable(!flag);
      keyValuePair.Value.CreateColliderAdjusted();
    }
  }

  public void Looted(string lootId)
  {
    if (this.activeCharacter >= 4 || this.lootedItems.Contains(lootId))
      return;
    this.looted[this.activeCharacter] = true;
    this.lootedItems.Add(lootId);
    this.cardCI[lootId].ShowDisableReward();
    Transform transform = this.cardCI[lootId].transform;
    string _cardName = lootId.Split('_', StringSplitOptions.None)[0];
    this.StartCoroutine(this.LootTrail(this.activeCharacter, lootId, transform));
    AtOManager.Instance.AddItemToHero(this.characterOrder[this.activeCharacter], _cardName);
    this.characterPotraitArray[this.activeCharacter].gameObject.SetActive(true);
    this.characterPotraitArray[this.activeCharacter].GetComponent<SpriteRenderer>().sprite = AtOManager.Instance.GetHero(this.characterOrder[this.activeCharacter]).HeroData.HeroSubClass.SpriteSpeed;
    this.characterPotraitArray[this.activeCharacter].position = this.cardCI[lootId].transform.position + new Vector3(0.85f, 0.85f, 0.0f);
    this.activeCharacter = 4;
    if (GameManager.Instance.IsMultiplayer())
    {
      if (this.isMyLoot)
      {
        this.photonView.RPC("NET_Looted", RpcTarget.Others, (object) lootId);
        this.isMyLoot = false;
        this.EnableLoot(false);
      }
      this.StartCoroutine(this.NextCharacterMP());
    }
    else
      this.NextCharacter();
  }

  [PunRPC]
  private void NET_Looted(string lootId)
  {
    if (this.reseting)
      return;
    this.Looted(lootId);
  }

  private IEnumerator LootTrail(int character, string cardId, Transform theTransform)
  {
    this.HighLight(false, this.cardType[cardId]);
    Transform child = this.characterLootArray[this.activeCharacter].transform.GetChild(2);
    if (!((UnityEngine.Object) child == (UnityEngine.Object) null))
    {
      Vector3 to = Vector3.zero;
      if (this.cardType[cardId] == "Weapon")
        to = child.GetChild(0).transform.position;
      else if (this.cardType[cardId] == "Armor")
        to = child.GetChild(1).transform.position;
      else if (this.cardType[cardId] == "Jewelry")
        to = child.GetChild(2).transform.position;
      else if (this.cardType[cardId] == "Accesory")
        to = child.GetChild(3).transform.position;
      else if (this.cardType[cardId] == "Pet")
        to = child.GetChild(4).transform.position;
      GameManager.Instance.GenerateParticleTrail(0, theTransform.position, to);
      yield return (object) Globals.Instance.WaitForSeconds(0.15f);
      this.characterLootArray[character].ShowItems();
    }
  }

  public void LootGold(bool comingFromNet = false)
  {
    if (!comingFromNet && !this.isMyLoot || this.looted == null || this.activeCharacter > this.looted.Length || this.activeCharacter >= 4)
      return;
    this.looted[this.activeCharacter] = true;
    Hero hero = AtOManager.Instance.GetHero(this.characterOrder[this.activeCharacter]);
    this.characterPotraitArray[this.activeCharacter].gameObject.SetActive(true);
    this.characterPotraitArray[this.activeCharacter].GetComponent<SpriteRenderer>().sprite = hero.HeroData.HeroSubClass.SpriteSpeed;
    this.characterPotraitArray[this.activeCharacter].position = this.botonGold.transform.position + new Vector3((float) (0.800000011920929 * (double) this.goldSelected - 0.34999999403953552), -1f, 0.0f);
    ++this.goldSelected;
    this.activeCharacter = 4;
    if (GameManager.Instance.IsMultiplayer())
    {
      if (this.isMyLoot)
      {
        this.photonView.RPC("NET_LootGold", RpcTarget.Others);
        this.isMyLoot = false;
        this.EnableLoot(false);
      }
      if (NetworkManager.Instance.IsMaster())
        AtOManager.Instance.GivePlayer(0, this.goldQuantity, hero.Owner);
      this.StartCoroutine(this.NextCharacterMP());
    }
    else
    {
      AtOManager.Instance.GivePlayer(0, this.goldQuantity, hero.Owner);
      this.NextCharacter();
    }
  }

  [PunRPC]
  private void NET_LootGold()
  {
    if (this.reseting)
      return;
    this.LootGold(true);
  }

  private IEnumerator NextCharacterMP()
  {
    if (GameManager.Instance.IsMultiplayer())
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersReady("nextCharacterMP"))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("Game ready, Everybody checked nextCharacterMP");
        NetworkManager.Instance.PlayersNetworkContinue("nextCharacterMP");
        yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      }
      else
      {
        NetworkManager.Instance.SetWaitingSyncro("nextCharacterMP", true);
        NetworkManager.Instance.SetStatusReady("nextCharacterMP");
        while (NetworkManager.Instance.WaitingSyncro["nextCharacterMP"])
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("nextCharacterMP, we can continue!");
      }
    }
    this.NextCharacter();
  }

  private void NextCharacter()
  {
    int charToActivate = -1;
    bool flag = true;
    for (int index = 0; index < 4; ++index)
    {
      Hero hero = AtOManager.Instance.GetHero(this.characterOrder[index]);
      if (hero != null && !((UnityEngine.Object) hero.HeroData == (UnityEngine.Object) null) && !this.looted[index])
      {
        flag = false;
        charToActivate = index;
        break;
      }
    }
    if (flag)
      this.StartCoroutine(this.FinishLoot());
    else
      this.ActivateCharacter(charToActivate);
  }

  private IEnumerator FinishLoot()
  {
    this.finishLoot = true;
    this.buttonRestart.gameObject.SetActive(false);
    this.botonGold.Disable();
    SaveManager.SavePlayerData();
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
    {
      yield return (object) Globals.Instance.WaitForSeconds(1f);
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("Load finish loot", "trace");
      GameManager.Instance.SetMaskLoading();
      AtOManager.Instance.FinishLoot();
    }
  }

  public void HighLight(bool state, string itemType)
  {
    if (this.activeCharacter >= 4 || this.activeCharacter <= -1 || this.characterLootArray == null || this.activeCharacter >= this.characterLootArray.Length || !((UnityEngine.Object) this.characterLootArray[this.activeCharacter] != (UnityEngine.Object) null))
      return;
    switch (itemType)
    {
      case "Weapon":
        this.characterLootArray[this.activeCharacter].item0.DoHover(state);
        break;
      case "Armor":
        this.characterLootArray[this.activeCharacter].item1.DoHover(state);
        break;
      case "Jewelry":
        this.characterLootArray[this.activeCharacter].item2.DoHover(state);
        break;
      case "Accesory":
        this.characterLootArray[this.activeCharacter].item3.DoHover(state);
        break;
      case "Pet":
        this.characterLootArray[this.activeCharacter].item4.DoHover(state);
        break;
    }
  }

  public void ShowCharacterWindow(string type = "", bool isHero = true, int characterIndex = -1) => this.characterWindowUI.Show(type, characterIndex);

  public void ShowDeck(int auxInt) => this.characterWindowUI.Show("deck", auxInt);

  public void RestartLoot()
  {
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
    {
      this.reseting = true;
      this.StartCoroutine(this.RestartLootMaster());
    }
    else
    {
      this.buttonRestart.gameObject.SetActive(false);
      this.photonView.RPC("NET_RestartLoot", RpcTarget.MasterClient, (object) NetworkManager.Instance.GetPlayerNickReal(NetworkManager.Instance.GetPlayerNick()));
    }
  }

  private IEnumerator RestartLootMaster()
  {
    if (!this.finishLoot)
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
      AtOManager.Instance.totalGoldGained = this.totalGoldGained;
      AtOManager.Instance.totalDustGained = this.totalDustGained;
      PlayerManager.Instance.GoldGained = this.atoGoldGained;
      PlayerManager.Instance.DustGained = this.atoDustGained;
      PlayerManager.Instance.ExpGained = this.expGained;
      AtOManager.Instance.DoLoot(this.lootListId);
    }
  }

  [PunRPC]
  private void NET_RestartLoot(string _nick)
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
    AtOManager.Instance.SetPlayerGold(this.clientGold);
    AtOManager.Instance.SetPlayerDust(this.clientDust);
  }

  private void WantToRestart()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.WantToRestart);
    if (!AlertManager.Instance.GetConfirmAnswer())
      return;
    this.RestartLoot();
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    int absolutePosition = -1)
  {
    this._controllerList.Clear();
    this._controllerList.Add(this.buttonRestart);
    this._controllerList.Add(this.botonGold.transform);
    for (int index = 0; index < 4; ++index)
    {
      if (Functions.TransformIsVisible(this.characterLootArray[index].transform))
      {
        this._controllerList.Add(this.characterLootArray[index].transform.GetChild(3).transform);
        this._controllerList.Add(this.characterLootArray[index].transform.GetChild(7).transform);
        foreach (Transform transform in this.characterLootArray[index].transform.GetChild(2).transform)
        {
          if (transform.GetComponent<BoxCollider2D>().enabled)
            this._controllerList.Add(transform);
        }
      }
    }
    foreach (Transform transform in this.cardContainer)
      this._controllerList.Add(transform);
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
