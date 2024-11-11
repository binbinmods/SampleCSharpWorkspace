// Decompiled with JetBrains decompiler
// Type: CardPlayerManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardPlayerManager : MonoBehaviour
{
  private PhotonView photonView;
  public CharacterWindowUI characterWindowUI;
  public SideCharacters sideCharacters;
  public Transform cardContainer;
  public Transform choose;
  public Transform onlyMaster;
  private List<string> cardList;
  private List<CardItem> cards;
  private List<Vector3> positions;
  private List<bool> moving;
  private bool cardsMoved;
  private bool cardSelected;
  public Transform botShuffle;
  private Dictionary<string, int> playerSelectedCard = new Dictionary<string, int>();
  private CardPlayerPackData _pack;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();

  public static CardPlayerManager Instance { get; private set; }

  private void Awake()
  {
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.LoadByName("CardPlayer");
    }
    else
    {
      if ((Object) CardPlayerManager.Instance == (Object) null)
        CardPlayerManager.Instance = this;
      else if ((Object) CardPlayerManager.Instance != (Object) this)
        Object.Destroy((Object) this);
      GameManager.Instance.SetCamera();
      this.photonView = PhotonView.Get((Component) this);
      NetworkManager.Instance.StartStopQueue(true);
    }
  }

  public bool CanExit() => this.botShuffle.gameObject.activeSelf;

  private void Start()
  {
    this.choose.gameObject.SetActive(false);
    this.StartCoroutine(this.StartSync());
  }

  private IEnumerator StartSync()
  {
    if (GameManager.Instance.IsMultiplayer())
    {
      Debug.Log((object) "WaitingSyncro cardplayer");
      if (NetworkManager.Instance.IsMaster())
      {
        while (!NetworkManager.Instance.AllPlayersReady("cardplayer"))
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        Functions.DebugLogGD("Game ready, Everybody checked cardplayer");
        NetworkManager.Instance.PlayersNetworkContinue("cardplayer");
      }
      else
      {
        NetworkManager.Instance.SetWaitingSyncro("cardplayer", true);
        NetworkManager.Instance.SetStatusReady("cardplayer");
        while (NetworkManager.Instance.WaitingSyncro["cardplayer"])
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        Functions.DebugLogGD("cardplayer, we can continue!");
      }
    }
    GameManager.Instance.SceneLoaded();
    this.sideCharacters.EnableAll();
    this.botShuffle.GetComponent<BotonGeneric>().Disable();
    if (!GameManager.Instance.IsMultiplayer())
      this.onlyMaster.gameObject.SetActive(false);
    Random.InitState((AtOManager.Instance.currentMapNode + AtOManager.Instance.GetGameId() + MapManager.Instance.GetRandomString()).GetDeterministicHashCode());
    this.SetCards();
  }

  private Vector3 GetPosition(int _index, bool _bigSize) => _bigSize ? new Vector3((float) (3.0 * (double) _index - 4.5), 0.0f, 0.0f) : new Vector3((float) (3.7000000476837158 * (double) _index - 5.5), 0.0f, 0.0f);

  private void SetCards()
  {
    this.cardList = new List<string>();
    this._pack = AtOManager.Instance.cardPlayerPackData;
    if ((Object) this._pack != (Object) null)
    {
      if ((Object) this._pack.Card0 != (Object) null)
        this.cardList.Add(this._pack.Card0.Id);
      if ((Object) this._pack.Card1 != (Object) null)
        this.cardList.Add(this._pack.Card1.Id);
      if ((Object) this._pack.Card2 != (Object) null)
        this.cardList.Add(this._pack.Card2.Id);
      if ((Object) this._pack.Card3 != (Object) null)
        this.cardList.Add(this._pack.Card3.Id);
    }
    this.cards = new List<CardItem>();
    this.positions = new List<Vector3>();
    this.moving = new List<bool>();
    for (int index = 0; index < this.cardList.Count; ++index)
      this.moving.Add(false);
    this.StartCoroutine(this.SetCardsCo());
  }

  private IEnumerator SetCardsCo()
  {
    for (int i = 0; i < this.cardList.Count; ++i)
    {
      string card = this.cardList[i];
      GameObject gameObject = Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.cardContainer);
      CardItem component = gameObject.GetComponent<CardItem>();
      component.DisableCollider();
      this.cards.Add(component);
      gameObject.name = card;
      component.SetCard(this.cardList[i], false);
      Vector3 position = this.GetPosition(i, true);
      float x = position.x;
      float y = position.y;
      this.positions.Add(position);
      component.transform.position = new Vector3(x, y, 0.0f);
      component.DoReward(false, fromLoot: true, selectable: false);
      component.SetDestination(new Vector3(x, y, 0.0f));
      component.SetLocalScale(new Vector3(1.4f, 1.4f, 1f));
      component.SetDestinationLocalScale(1.4f);
      component.CardPlayerIndex = i;
      component.GetComponent<Floating>().enabled = true;
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
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
    Random.InitState((AtOManager.Instance.currentMapNode + AtOManager.Instance.GetGameId() + MapManager.Instance.GetRandomString()).GetDeterministicHashCode());
    this.StartCoroutine(this.ShuffleCo());
  }

  [PunRPC]
  private void NET_Shuffle() => this.Shuffle(true);

  private IEnumerator ShuffleCo()
  {
    for (int index = 0; index < this.cards.Count; ++index)
    {
      Vector3 position = this.GetPosition(index, false);
      float x = position.x;
      float y = position.y;
      this.positions[index] = position;
      this.cards[index].GetComponent<Floating>().enabled = false;
      this.cards[index].cardrevealed = true;
      this.cards[index].DisableCollider();
      this.cards[index].SetDestination(new Vector3(x, y, 0.0f));
      this.cards[index].SetDestinationLocalScale(1.1f);
    }
    yield return (object) Globals.Instance.WaitForSeconds(0.25f);
    for (int i = 0; i < this.cards.Count; ++i)
    {
      this.cards[i].TurnBack();
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
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
    CardPlayerManager cardPlayerManager = this;
    int objectToMove = 0;
    int lastObject = 0;
    int destineToMove = 0;
    int iterations = 10 + cardPlayerManager._pack.ModIterations;
    int speed = 10 + cardPlayerManager._pack.ModSpeed;
    for (int i = 0; i < iterations; ++i)
    {
      speed += 4;
      objectToMove = lastObject;
      while (objectToMove == lastObject)
      {
        objectToMove = Random.Range(0, cardPlayerManager.cardList.Count);
        destineToMove = objectToMove;
      }
      while (destineToMove == objectToMove)
        destineToMove = Random.Range(0, cardPlayerManager.cardList.Count);
      cardPlayerManager.StartCoroutine(cardPlayerManager.MoveIndividualCo(objectToMove, destineToMove, (float) speed));
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
      cardPlayerManager.StartCoroutine(cardPlayerManager.MoveIndividualCo(destineToMove, objectToMove, (float) speed));
      while (cardPlayerManager.moving[destineToMove])
        yield return (object) null;
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    }
    cardPlayerManager.EnableCards();
  }

  private void EnableCards()
  {
    this.choose.gameObject.SetActive(true);
    for (int index = 0; index < this.cards.Count; ++index)
    {
      this.cards[index].enabled = false;
      this.cards[index].CreateColliderAdjusted();
      this.cards[index].GetComponent<Floating>().enabled = true;
    }
    this.cardsMoved = true;
  }

  public bool CanClick() => !this.cardsMoved || this.cardsMoved && !this.cardSelected;

  public void SelectCard(int _index)
  {
    if (!this.cardsMoved || this.cardSelected)
      return;
    for (int index = 0; index < 4; ++index)
    {
      if (this.cards[_index].CardPlayerIndex == _index)
      {
        this.cardSelected = true;
        if (!GameManager.Instance.IsMultiplayer())
        {
          this.playerSelectedCard.Add(NetworkManager.Instance.GetPlayerNick(), _index);
          this.FinishSelection();
          break;
        }
        this.photonView.RPC("NET_AssignSelection", RpcTarget.MasterClient, (object) NetworkManager.Instance.GetPlayerNick(), (object) _index);
        break;
      }
    }
  }

  [PunRPC]
  private void NET_ShareAssignSelection(string _keys, string _values)
  {
    this.playerSelectedCard.Clear();
    string[] strArray1 = JsonHelper.FromJson<string>(_keys);
    string[] strArray2 = JsonHelper.FromJson<string>(_values);
    for (int index = 0; index < strArray1.Length; ++index)
      this.playerSelectedCard.Add(strArray1[index], int.Parse(strArray2[index]));
    for (int index = 0; index < 4; ++index)
      this.cards[index].ClearMPMark();
    foreach (KeyValuePair<string, int> keyValuePair in this.playerSelectedCard)
      this.cards[keyValuePair.Value].ShowMPMark(keyValuePair.Key);
    GameManager.Instance.PlayLibraryAudio("ui_mapnodeselection");
  }

  [PunRPC]
  private void NET_AssignSelection(string _nick, int _cardIndex)
  {
    if (this.playerSelectedCard.ContainsKey(_nick))
      return;
    this.playerSelectedCard.Add(_nick, _cardIndex);
    string[] array1 = new string[this.playerSelectedCard.Count];
    this.playerSelectedCard.Keys.CopyTo(array1, 0);
    int[] array2 = new int[this.playerSelectedCard.Count];
    this.playerSelectedCard.Values.CopyTo(array2, 0);
    this.photonView.RPC("NET_ShareAssignSelection", RpcTarget.All, (object) JsonHelper.ToJson<string>(array1), (object) JsonHelper.ToJson<int>(array2));
    if (this.playerSelectedCard.Count != NetworkManager.Instance.GetNumPlayers())
      return;
    this.photonView.RPC("NET_FinishSelection", RpcTarget.All);
  }

  [PunRPC]
  private void NET_FinishSelection() => this.FinishSelection();

  private void FinishSelection() => this.StartCoroutine(this.FinishSelectionCo());

  private IEnumerator FinishSelectionCo()
  {
    this.choose.gameObject.SetActive(false);
    for (int index = 0; index < 4; ++index)
    {
      this.cards[index].DoReward(false, fromLoot: true);
      this.cards[index].SetLocalScale(new Vector3(1.1f, 1.1f, 1f));
      this.cards[index].SetDestinationLocalScale(1.1f);
    }
    yield return (object) Globals.Instance.WaitForSeconds(0.6f);
    Hero[] team = AtOManager.Instance.GetTeam();
    for (int index = 0; index < 4; ++index)
    {
      if (team[index] != null && !((Object) team[index].HeroData == (Object) null))
      {
        foreach (KeyValuePair<string, int> keyValuePair in this.playerSelectedCard)
        {
          if (team[index].Owner == keyValuePair.Key || team[index].Owner == "" || team[index].Owner == null)
          {
            Vector3 to = this.sideCharacters.CharacterIconPosition(index);
            GameManager.Instance.GenerateParticleTrail(0, this.cards[keyValuePair.Value].transform.position, to);
            bool flag = true;
            if (this.cards[keyValuePair.Value].CardData.GoldGainQuantity != 0)
            {
              flag = false;
              AtOManager.Instance.GivePlayer(0, this.cards[keyValuePair.Value].CardData.GoldGainQuantity, team[index].Owner);
            }
            if (this.cards[keyValuePair.Value].CardData.ShardsGainQuantity != 0)
            {
              flag = false;
              AtOManager.Instance.GivePlayer(1, this.cards[keyValuePair.Value].CardData.ShardsGainQuantity, team[index].Owner);
            }
            if (this.cards[keyValuePair.Value].CardData.Id == "success")
              flag = false;
            if (flag)
              AtOManager.Instance.AddCardToHero(index, this.cards[keyValuePair.Value].CardData.Id);
            PlayerManager.Instance.CardUnlock(this.cards[keyValuePair.Value].CardData.Id);
            this.cards[keyValuePair.Value].ShowPortrait(team[index].HeroData.HeroSubClass.SpriteSpeed);
          }
        }
      }
    }
    if (!GameManager.Instance.IsMultiplayer() || NetworkManager.Instance.IsMaster())
    {
      SaveManager.SavePlayerData();
      yield return (object) Globals.Instance.WaitForSeconds(4f);
      AtOManager.Instance.FinishCardPlayer();
    }
  }

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
      float x3 = Mathf.MoveTowards(cardT.position.x, x2, speed * Time.deltaTime);
      float num5 = Mathf.Lerp(startPos.y, targetPos.y, (x3 - x1) / num4);
      float num6 = (float) ((double) arcHeight * ((double) x3 - (double) x1) * ((double) x3 - (double) x2) / (-0.25 * (double) num4 * (double) num4));
      Vector3 vector3 = new Vector3(x3, num5 + num6, cardT.position.z);
      cardT.position = vector3;
      if ((double) Mathf.Abs(vector3.x - targetPos.x) < 0.0099999997764825821 && (double) Mathf.Abs(vector3.y - targetPos.y) < 0.0099999997764825821)
        finished = true;
      yield return (object) Globals.Instance.WaitForSeconds(Time.deltaTime * iterationDelay);
    }
    cardT.position = targetPos;
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
    if (!((Object) this._controllerList[this.controllerHorizontalIndex] != (Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }
}
