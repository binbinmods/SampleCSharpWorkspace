// Decompiled with JetBrains decompiler
// Type: CardPlayerPairsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardPlayerPairsManager : MonoBehaviour
{
  private PhotonView photonView;
  public CharacterWindowUI characterWindowUI;
  public SideCharacters sideCharacters;
  public TMP_Text playerTurnText;
  public Transform cardContainer;
  public Transform onlyMaster;
  public Transform selection;
  public Transform finishBlock;
  public BotonGeneric finishButton;
  private List<string> cardList;
  private List<CardItem> cards;
  private List<Vector3> positions;
  private List<bool> moving;
  public Transform botShuffle;
  private Dictionary<string, int> playerSelectedCard = new Dictionary<string, int>();
  private CardPlayerPairsPackData _pack;
  private int cardSelected1 = -1;
  private int cardSelected2 = -1;
  private List<int> cardsSelectedList = new List<int>();
  private bool canSelect;
  private int currentRound = -1;
  private int currentRoundGlobal = -1;
  private int maxRounds = 8;
  private int[] orderArray;
  public SpriteRenderer[] charSpr;
  private Hero[] theTeam;
  private Vector3 spriteBig = new Vector3(0.8f, 0.8f, 1f);
  private Vector3 spriteSmall = new Vector3(0.6f, 0.6f, 1f);
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();

  public static CardPlayerPairsManager Instance { get; private set; }

  private void Awake()
  {
    if ((UnityEngine.Object) GameManager.Instance == (UnityEngine.Object) null)
    {
      SceneStatic.LoadByName("MainMenu");
    }
    else
    {
      if ((UnityEngine.Object) CardPlayerPairsManager.Instance == (UnityEngine.Object) null)
        CardPlayerPairsManager.Instance = this;
      else if ((UnityEngine.Object) CardPlayerPairsManager.Instance != (UnityEngine.Object) this)
        UnityEngine.Object.Destroy((UnityEngine.Object) this);
      GameManager.Instance.SetCamera();
      this.photonView = PhotonView.Get((Component) this);
      NetworkManager.Instance.StartStopQueue(true);
    }
  }

  public bool CanExit() => this.botShuffle.gameObject.activeSelf;

  private void Start()
  {
    this.theTeam = AtOManager.Instance.GetTeam();
    List<Hero> heroList = new List<Hero>();
    List<int> ts = new List<int>();
    for (int index = 0; index < 4; ++index)
    {
      if (this.theTeam[index] != null && (UnityEngine.Object) this.theTeam[index].HeroData != (UnityEngine.Object) null)
        ts.Add(index);
    }
    if (ts.Count < 4)
    {
      ts = ts.ShuffleList<int>();
      int index = 0;
      while (ts.Count < 4)
      {
        ts.Add(ts[index]);
        ++index;
      }
    }
    this.orderArray = new int[4];
    for (int index = 0; index < 4; ++index)
      this.orderArray[index] = ts[index];
    this.selection.gameObject.SetActive(false);
    this.finishBlock.gameObject.SetActive(false);
    this.StartCoroutine(this.StartSync());
  }

  private IEnumerator StartSync()
  {
    if (GameManager.Instance.IsMultiplayer())
    {
      Debug.Log((object) "WaitingSyncro cardplayerpairs");
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersReady("cardplayerpairs"))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        Functions.DebugLogGD("Game ready, Everybody checked cardplayerpairs");
        NetworkManager.Instance.PlayersNetworkContinue("cardplayerpairs");
      }
      else
      {
        NetworkManager.Instance.SetWaitingSyncro("cardplayerpairs", true);
        NetworkManager.Instance.SetStatusReady("cardplayerpairs");
        while (NetworkManager.Instance.WaitingSyncro["cardplayerpairs"])
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        Functions.DebugLogGD("cardplayerpairs, we can continue!");
      }
    }
    GameManager.Instance.SceneLoaded();
    this.sideCharacters.EnableAll();
    this.botShuffle.GetComponent<BotonGeneric>().Disable();
    if (!GameManager.Instance.IsMultiplayer())
      this.onlyMaster.gameObject.SetActive(false);
    UnityEngine.Random.InitState((AtOManager.Instance.currentMapNode + AtOManager.Instance.GetGameId() + MapManager.Instance.GetRandomString()).GetDeterministicHashCode());
    this.SetCards();
    this.NextRound(true);
  }

  private Vector3 GetPosition(int _index, bool _initial)
  {
    float num = Mathf.Ceil((float) (_index / 4));
    return new Vector3((float) (2.2000000476837158 * (double) (_index % 4) - 3.2999999523162842), (float) ((double) num * 3.0 - 3.2000000476837158), 0.0f);
  }

  private void PreFinishGame()
  {
    if (this.cardSelected1 != -1)
    {
      this.cards[this.cardSelected1].ShowDisable(true);
      this.cards[this.cardSelected2].ShowDisable(true);
    }
    for (int index = 0; index < this.cards.Count; ++index)
    {
      if (!this.cardsSelectedList.Contains(index))
      {
        this.cards[index].DoReward(false, fromLoot: true, selectable: false);
        this.cards[index].SetLocalScale(new Vector3(1f, 1f, 1f));
        this.cards[index].SetDestinationLocalScale(1f);
        this.cards[index].ShowDisable(true);
      }
    }
    this.selection.gameObject.SetActive(false);
    this.finishButton.Enable();
  }

  public void FinishPairGame()
  {
    if (GameManager.Instance.IsMultiplayer())
    {
      if (!NetworkManager.Instance.IsMaster())
        return;
      this.photonView.RPC("NET_FinishSelection", RpcTarget.All);
    }
    else
      this.FinishSelection();
  }

  private void NextRound(bool initGame = false)
  {
    ++this.currentRound;
    this.currentRoundGlobal = this.currentRound / 2;
    if (this.currentRound / 2 >= this.maxRounds)
    {
      this.PreFinishGame();
    }
    else
    {
      if (this.currentRound % 4 == 0)
        this.SetCharacterOrder();
      if (this.currentRound % 2 == 0)
        this.finishButton.Enable();
      if (initGame)
        return;
      this.ShowCurrentCharacter();
      this.canSelect = true;
    }
  }

  private void SetCharacterOrder()
  {
    this.orderArray = Functions.ShuffleArray<int>(this.orderArray);
    for (int index = 0; index < 4; ++index)
      this.charSpr[index].sprite = this.theTeam[this.orderArray[index]].HeroData.HeroSubClass.SpriteSpeed;
  }

  private void ShowCurrentCharacter()
  {
    for (int index = 0; index < 4; ++index)
    {
      if (index != this.currentRound % 4)
      {
        this.charSpr[index].transform.localScale = this.spriteSmall;
        this.charSpr[index].color = Globals.Instance.ColorColor["grey"];
      }
      else
      {
        this.charSpr[index].transform.localScale = this.spriteBig;
        this.charSpr[index].color = Globals.Instance.ColorColor["white"];
      }
    }
    this.ShowPlayerName();
  }

  private void SetCards()
  {
    this.cardList = new List<string>();
    this._pack = AtOManager.Instance.cardPlayerPairsPackData;
    if ((UnityEngine.Object) this._pack != (UnityEngine.Object) null)
    {
      if ((UnityEngine.Object) this._pack.Card0 != (UnityEngine.Object) null)
      {
        this.cardList.Add(this._pack.Card0.Id);
        this.cardList.Add(this._pack.Card0.Id);
      }
      if ((UnityEngine.Object) this._pack.Card1 != (UnityEngine.Object) null)
      {
        this.cardList.Add(this._pack.Card1.Id);
        this.cardList.Add(this._pack.Card1.Id);
      }
      if ((UnityEngine.Object) this._pack.Card2 != (UnityEngine.Object) null)
      {
        this.cardList.Add(this._pack.Card2.Id);
        this.cardList.Add(this._pack.Card2.Id);
      }
      if ((UnityEngine.Object) this._pack.Card3 != (UnityEngine.Object) null)
      {
        this.cardList.Add(this._pack.Card3.Id);
        this.cardList.Add(this._pack.Card3.Id);
      }
      if ((UnityEngine.Object) this._pack.Card4 != (UnityEngine.Object) null)
      {
        this.cardList.Add(this._pack.Card4.Id);
        this.cardList.Add(this._pack.Card4.Id);
      }
      if ((UnityEngine.Object) this._pack.Card5 != (UnityEngine.Object) null)
      {
        this.cardList.Add(this._pack.Card5.Id);
        this.cardList.Add(this._pack.Card5.Id);
      }
    }
    if (this.cardList.Count < 12)
    {
      this.FinishPairGame();
    }
    else
    {
      this.cardList.Reverse();
      this.cards = new List<CardItem>();
      this.positions = new List<Vector3>();
      this.moving = new List<bool>();
      for (int index = 0; index < this.cardList.Count; ++index)
        this.moving.Add(false);
      this.StartCoroutine(this.SetCardsCo());
    }
  }

  private IEnumerator SetCardsCo()
  {
    for (int i = 0; i < this.cardList.Count; ++i)
    {
      string card = this.cardList[i];
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.cardContainer);
      CardItem component = gameObject.GetComponent<CardItem>();
      component.DisableCollider();
      this.cards.Add(component);
      gameObject.name = card;
      component.SetCard(this.cardList[i], false);
      Vector3 position = this.GetPosition(i, true);
      float x = position.x;
      float y = position.y;
      this.positions.Add(position);
      component.transform.localPosition = new Vector3(x, y, 0.0f);
      component.DoReward(false, fromLoot: true, selectable: false);
      component.SetDestination(new Vector3(x, y, 0.0f));
      component.SetLocalScale(new Vector3(1f, 1f, 1f));
      component.SetDestinationLocalScale(1f);
      component.CardPlayerIndex = i;
      component.GetComponent<Floating>().enabled = true;
      component.HideLock();
      yield return (object) Globals.Instance.WaitForSeconds(0.025f);
    }
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
      this.botShuffle.GetComponent<BotonGeneric>().Enable();
  }

  public void Shuffle(bool fromNet = false)
  {
    if (GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster() && !fromNet)
      return;
    this.botShuffle.gameObject.SetActive(false);
    if (GameManager.Instance.IsMultiplayer())
    {
      if (NetworkManager.Instance.IsMaster())
        this.photonView.RPC("NET_Shuffle", RpcTarget.Others);
      this.onlyMaster.gameObject.SetActive(false);
    }
    UnityEngine.Random.InitState((AtOManager.Instance.currentMapNode + AtOManager.Instance.GetGameId() + MapManager.Instance.GetRandomString()).GetDeterministicHashCode());
    this.StartCoroutine(this.ShuffleCo());
  }

  [PunRPC]
  private void NET_Shuffle() => this.Shuffle(true);

  private IEnumerator ShuffleCo()
  {
    for (int index = 0; index < this.cards.Count; ++index)
    {
      Vector3 position = this.GetPosition(index, true);
      this.positions[index] = position;
      this.cards[index].GetComponent<Floating>().enabled = false;
      this.cards[index].cardrevealed = true;
      this.cards[index].DisableCollider();
    }
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    for (int i = 0; i < this.cards.Count; ++i)
    {
      this.cards[i].TurnBack();
      yield return (object) Globals.Instance.WaitForSeconds(0.025f);
    }
    yield return (object) Globals.Instance.WaitForSeconds(0.2f);
    for (int index = 0; index < this.cards.Count; ++index)
      this.cards[index].enabled = false;
    yield return (object) Globals.Instance.WaitForSeconds(0.5f);
    this.Move();
  }

  private void Move() => this.StartCoroutine(this.MoveCo());

  private IEnumerator MoveCo()
  {
    CardPlayerPairsManager playerPairsManager = this;
    int objectToMove = 0;
    int lastObject = 0;
    int destineToMove = 0;
    int iterations = playerPairsManager.cardList.Count * 2;
    int speed = 10;
    int speedIncrement = 5;
    int maxSpeed = 60;
    bool[] objMoved = new bool[playerPairsManager.cardList.Count];
    for (int index = 0; index < objMoved.Length; ++index)
      objMoved[index] = false;
    for (int i = 0; i < iterations; ++i)
    {
      if (speed < maxSpeed)
        speed += speedIncrement;
      bool flag = false;
      while (!flag)
      {
        objectToMove = UnityEngine.Random.Range(0, objMoved.Length);
        if (objectToMove != lastObject)
          flag = true;
      }
      destineToMove = objectToMove;
      while (destineToMove == objectToMove)
        destineToMove = UnityEngine.Random.Range(0, objMoved.Length);
      playerPairsManager.StartCoroutine(playerPairsManager.MoveIndividualCo(objectToMove, destineToMove, (float) speed));
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
      playerPairsManager.StartCoroutine(playerPairsManager.MoveIndividualCo(destineToMove, objectToMove, (float) speed));
      lastObject = destineToMove;
      while (playerPairsManager.moving[destineToMove])
        yield return (object) null;
      objMoved[objectToMove] = true;
      yield return (object) Globals.Instance.WaitForSeconds((float) (0.05000000074505806 - 0.039999999105930328 * ((double) i / (double) iterations)));
    }
    playerPairsManager.EnableCards();
  }

  [PunRPC]
  private void NET_SelectCard(int _index) => this.SelectCard(_index, true);

  public void SelectCard(int _index, bool _fromNet = false)
  {
    if (!this.CanClick() && !_fromNet)
      return;
    if (!_fromNet && GameManager.Instance.IsMultiplayer())
      this.photonView.RPC("NET_SelectCard", RpcTarget.Others, (object) _index);
    this.canSelect = false;
    for (int index = 0; index < this.cards.Count; ++index)
    {
      if (!this.cardsSelectedList.Contains(index) && this.cards[index].CardPlayerIndex == _index)
      {
        this.cards[index].DoReward(false, fromLoot: true, selectable: false);
        this.cards[index].SetLocalScale(new Vector3(1f, 1f, 1f));
        this.cards[index].SetDestinationLocalScale(1f);
        if (this.cardSelected1 == -1)
          this.cardSelected1 = index;
        else
          this.cardSelected2 = index;
        this.cardsSelectedList.Add(index);
        break;
      }
    }
    this.StartCoroutine(this.SelectCardCo());
  }

  private IEnumerator SelectCardCo()
  {
    CardPlayerPairsManager playerPairsManager = this;
    playerPairsManager.finishButton.Disable();
    if (playerPairsManager.cardSelected2 != -1)
    {
      yield return (object) Globals.Instance.WaitForSeconds(1f);
      if (playerPairsManager.cards[playerPairsManager.cardSelected1].CardData.Id == playerPairsManager.cards[playerPairsManager.cardSelected2].CardData.Id)
      {
        Debug.Log((object) "MATCH!!!");
        for (int heroIndex = 0; heroIndex < 4; ++heroIndex)
        {
          if (playerPairsManager.theTeam[heroIndex] != null && (UnityEngine.Object) playerPairsManager.theTeam[heroIndex].HeroData != (UnityEngine.Object) null)
          {
            if ((UnityEngine.Object) playerPairsManager.theTeam[heroIndex].HeroData.HeroSubClass.SpriteSpeed == (UnityEngine.Object) playerPairsManager.charSpr[playerPairsManager.currentRound % 4 - 1].sprite)
              playerPairsManager.GiveCardToHero(heroIndex, playerPairsManager.cardSelected1);
            else if ((UnityEngine.Object) playerPairsManager.theTeam[heroIndex].HeroData.HeroSubClass.SpriteSpeed == (UnityEngine.Object) playerPairsManager.charSpr[playerPairsManager.currentRound % 4].sprite)
              playerPairsManager.GiveCardToHero(heroIndex, playerPairsManager.cardSelected2);
          }
        }
        playerPairsManager.cardSelected1 = playerPairsManager.cardSelected2 = -1;
      }
      else if (playerPairsManager.currentRoundGlobal + 1 < playerPairsManager.maxRounds)
      {
        playerPairsManager.cards[playerPairsManager.cardSelected1].TurnBack();
        playerPairsManager.cards[playerPairsManager.cardSelected2].TurnBack();
        playerPairsManager.cardsSelectedList.Remove(playerPairsManager.cardSelected1);
        playerPairsManager.cardsSelectedList.Remove(playerPairsManager.cardSelected2);
        yield return (object) Globals.Instance.WaitForSeconds(0.2f);
        int destineToMove = UnityEngine.Random.Range(0, playerPairsManager.cards.Count);
        int num1 = 0;
        while (destineToMove == playerPairsManager.cardSelected1 || destineToMove == playerPairsManager.cardSelected2 || playerPairsManager.cardsSelectedList.Contains(destineToMove))
        {
          destineToMove = UnityEngine.Random.Range(0, playerPairsManager.cards.Count);
          ++num1;
          if (num1 > 20)
          {
            destineToMove = playerPairsManager.cardSelected1;
            break;
          }
        }
        if (destineToMove != playerPairsManager.cardSelected1)
        {
          playerPairsManager.StartCoroutine(playerPairsManager.MoveIndividualCo(playerPairsManager.cardSelected1, destineToMove, 10f));
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
          playerPairsManager.StartCoroutine(playerPairsManager.MoveIndividualCo(destineToMove, playerPairsManager.cardSelected1, 10f));
          yield return (object) Globals.Instance.WaitForSeconds(0.5f);
        }
        destineToMove = UnityEngine.Random.Range(0, playerPairsManager.cards.Count);
        int num2 = 0;
        while (destineToMove == playerPairsManager.cardSelected2 || destineToMove == playerPairsManager.cardSelected1 || playerPairsManager.cardsSelectedList.Contains(destineToMove))
        {
          destineToMove = UnityEngine.Random.Range(0, playerPairsManager.cards.Count);
          ++num2;
          if (num2 > 20)
          {
            destineToMove = playerPairsManager.cardSelected2;
            break;
          }
        }
        if (destineToMove != playerPairsManager.cardSelected2)
        {
          playerPairsManager.StartCoroutine(playerPairsManager.MoveIndividualCo(playerPairsManager.cardSelected2, destineToMove, 10f));
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
          playerPairsManager.StartCoroutine(playerPairsManager.MoveIndividualCo(destineToMove, playerPairsManager.cardSelected2, 10f));
        }
        playerPairsManager.cardSelected1 = playerPairsManager.cardSelected2 = -1;
      }
    }
    if (GameManager.Instance.IsMultiplayer())
    {
      Debug.Log((object) ("WaitingSyncro selectCard" + playerPairsManager.currentRound.ToString()));
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersReady("selectCard" + playerPairsManager.currentRound.ToString()))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        Functions.DebugLogGD("Game ready, Everybody checked selectCard" + playerPairsManager.currentRound.ToString());
        NetworkManager.Instance.PlayersNetworkContinue("selectCard" + playerPairsManager.currentRound.ToString());
      }
      else
      {
        NetworkManager.Instance.SetWaitingSyncro("selectCard" + playerPairsManager.currentRound.ToString(), true);
        NetworkManager.Instance.SetStatusReady("selectCard" + playerPairsManager.currentRound.ToString());
        while (NetworkManager.Instance.WaitingSyncro["selectCard" + playerPairsManager.currentRound.ToString()])
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        Functions.DebugLogGD("selectCard" + playerPairsManager.currentRound.ToString() + ", we can continue!");
      }
      yield return (object) Globals.Instance.WaitForSeconds(0.3f);
    }
    else
      yield return (object) Globals.Instance.WaitForSeconds(0.5f);
    for (int index = 0; index < playerPairsManager.cards.Count; ++index)
    {
      if (!playerPairsManager.cardsSelectedList.Contains(index))
        playerPairsManager.cards[index].DisableCollider();
    }
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    for (int index = 0; index < playerPairsManager.cards.Count; ++index)
    {
      if (!playerPairsManager.cardsSelectedList.Contains(index))
        playerPairsManager.cards[index].EnableCollider();
    }
    playerPairsManager.NextRound();
  }

  private void ShowPlayerName()
  {
    if (!this.selection.gameObject.activeSelf)
      this.selection.gameObject.SetActive(true);
    if (!this.finishBlock.gameObject.activeSelf)
      this.finishBlock.gameObject.SetActive(true);
    this.playerTurnText.text = string.Format(Texts.Instance.GetText("cardPlayerPairsRound"), (object) (float) ((double) Mathf.Floor((float) this.currentRound / 2f) + 1.0), (object) this.maxRounds, (object) Globals.Instance.ClassColor[Enum.GetName(typeof (Enums.HeroClass), (object) this.theTeam[this.orderArray[this.currentRound % 4]].HeroData.HeroClass)], (object) this.theTeam[this.orderArray[this.currentRound % 4]].SourceName);
  }

  private void GiveCardToHero(int heroIndex, int cardIndex)
  {
    CardItem card = this.cards[cardIndex];
    string id = card.CardData.Id;
    Debug.Log((object) ("GiveCardToHero card " + id + " hero " + heroIndex.ToString()));
    bool flag = true;
    if (card.CardData.GoldGainQuantity != 0)
    {
      flag = false;
      AtOManager.Instance.GivePlayer(0, card.CardData.GoldGainQuantity, this.theTeam[heroIndex].Owner);
    }
    if (card.CardData.ShardsGainQuantity != 0)
    {
      flag = false;
      AtOManager.Instance.GivePlayer(1, card.CardData.ShardsGainQuantity, this.theTeam[heroIndex].Owner);
    }
    if (flag)
      AtOManager.Instance.AddCardToHero(heroIndex, id);
    this.sideCharacters.RefreshCards(heroIndex);
    card.ShowPortrait(this.theTeam[heroIndex].HeroData.HeroSubClass.SpriteSpeed, 1);
  }

  [PunRPC]
  private void NET_FinishSelection() => this.FinishSelection();

  private void FinishSelection() => this.StartCoroutine(this.FinishSelectionCo());

  private IEnumerator FinishSelectionCo()
  {
    this.finishBlock.gameObject.SetActive(false);
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
    {
      SaveManager.SavePlayerData();
      yield return (object) Globals.Instance.WaitForSeconds(0.5f);
      AtOManager.Instance.FinishCardPlayer();
    }
  }

  private void EnableCards()
  {
    for (int index = 0; index < this.cards.Count; ++index)
    {
      this.cards[index].enabled = false;
      this.cards[index].CreateColliderAdjusted();
      this.cards[index].GetComponent<Floating>().enabled = true;
    }
    this.canSelect = true;
    this.ShowCurrentCharacter();
  }

  public bool CanClick() => (!GameManager.Instance.IsMultiplayer() || !(this.theTeam[this.orderArray[this.currentRound % 4]].Owner != NetworkManager.Instance.GetPlayerNick())) && this.canSelect;

  private IEnumerator MoveIndividualCo(int sourceIndex, int targetIndex, float speed = 24f)
  {
    this.moving[sourceIndex] = true;
    Transform cardT = this.cards[sourceIndex].transform;
    Vector3 position1 = this.positions[sourceIndex];
    Vector3 position2 = this.positions[targetIndex];
    float num1 = Globals.Instance.sizeW * 0.8f;
    float num2 = Globals.Instance.sizeW * 0.2f;
    float iterationDelay = 0.5f;
    if (GameManager.Instance.IsMultiplayer() || GameManager.Instance.configGameSpeed == Enums.ConfigSpeed.Fast || GameManager.Instance.configGameSpeed == Enums.ConfigSpeed.Ultrafast)
      speed *= 1.4f;
    else
      speed *= 1.2f;
    Vector3 startPos = position1;
    Vector3 targetPos = position2;
    float num3 = Mathf.Abs(startPos.x - targetPos.x);
    bool finished = false;
    float min = 0.1f;
    float max = 4f;
    float arcHeight = Mathf.Clamp(min + (float) (((double) num3 - (double) num2) / ((double) num1 - (double) num2) * ((double) max - (double) min)), min, max);
    if ((double) position2.x < (double) position1.x)
      arcHeight *= -1f;
    while (!finished)
    {
      float x1 = startPos.x;
      float x2 = targetPos.x;
      float num4 = x2 - x1;
      float x3 = Mathf.MoveTowards(cardT.localPosition.x, x2, speed * Time.deltaTime);
      double num5 = (double) Mathf.Lerp(startPos.y, targetPos.y, (x3 - x1) / num4);
      double num6 = (double) arcHeight * ((double) x3 - (double) x1) * ((double) x3 - (double) x2) / (-0.25 * (double) num4 * (double) num4);
      float y = Mathf.MoveTowards(cardT.localPosition.y, targetPos.y, speed * Time.deltaTime);
      Vector3 vector3 = new Vector3(x3, y, cardT.localPosition.z);
      cardT.localPosition = vector3;
      if ((double) Mathf.Abs(vector3.x - targetPos.x) < 0.0099999997764825821 && (double) Mathf.Abs(vector3.y - targetPos.y) < 0.0099999997764825821)
        finished = true;
      yield return (object) Globals.Instance.WaitForSeconds(Time.deltaTime * iterationDelay);
    }
    cardT.localPosition = targetPos;
    this.cards[targetIndex] = cardT.GetComponent<CardItem>();
    this.cards[targetIndex].CardPlayerIndex = targetIndex;
    this.moving[sourceIndex] = false;
  }

  private Quaternion LookAt2D(Vector2 forward) => Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(forward.y, forward.x) * 57.29578f);

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    int absolutePosition = -1)
  {
    this._controllerList.Clear();
    if (Functions.TransformIsVisible(this.botShuffle))
      this._controllerList.Add(this.botShuffle);
    if (Functions.TransformIsVisible(this.finishButton.transform))
      this._controllerList.Add(this.finishButton.transform);
    for (int index = 0; index < 4; ++index)
    {
      if (Functions.TransformIsVisible(this.sideCharacters.charArray[index].transform))
        this._controllerList.Add(this.sideCharacters.charArray[index].transform.GetChild(0).transform);
    }
    if (Functions.TransformIsVisible(PlayerUIManager.Instance.giveGold))
      this._controllerList.Add(PlayerUIManager.Instance.giveGold);
    if (this.CanClick())
    {
      foreach (Transform transform in this.cardContainer)
        this._controllerList.Add(transform);
    }
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }
}
