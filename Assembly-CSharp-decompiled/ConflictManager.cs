// Decompiled with JetBrains decompiler
// Type: ConflictManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConflictManager : MonoBehaviour
{
  public SpriteRenderer[] cardbacks;
  public SpriteRenderer[] characterSPR;
  public TMP_Text[] nicks;
  public Transform[] xSymbol;
  public Transform buttons;
  public BotonGeneric[] botonConflict;
  public TMP_Text nickChoosing;
  public TMP_Text charWins;
  public Transform charWinsTransform;
  private int optionSelected = -1;
  public int playerChoosing = -1;
  private int cardOrder;
  private Hero[] heroes = new Hero[4];
  private bool[] charRoll;
  private int[] charRollIterations;
  private int[] charRollResult;
  private bool[] charWinner;
  public TMP_Text[] characterTnum;
  public Transform[] characterTcards;
  private int finalTieTimes;
  private PhotonView photonView;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();

  private void Awake() => this.photonView = MapManager.Instance.GetPhotonView();

  public bool IsActive() => this.gameObject.activeSelf;

  public void Show()
  {
    if ((UnityEngine.Object) EventManager.Instance == (UnityEngine.Object) null)
      this.transform.localPosition = new Vector3(0.0f, 0.5f, this.transform.localPosition.z);
    else
      this.transform.localPosition = new Vector3(1.1f, 0.5f, this.transform.localPosition.z);
    this.gameObject.SetActive(true);
    UnityEngine.Random.InitState((AtOManager.Instance.currentMapNode + AtOManager.Instance.GetGameId()).GetDeterministicHashCode());
    this.SetConflict();
    this.SetButtons();
    this.StartCoroutine(this.ShowCo());
  }

  private IEnumerator ShowCo()
  {
    if (NetworkManager.Instance.IsMaster())
    {
      while (!NetworkManager.Instance.AllPlayersReady("startConflict"))
        yield return (object) Globals.Instance.WaitForSeconds(0.01f);
      Functions.DebugLogGD("Game ready, Everybody checked startConflict");
      NetworkManager.Instance.PlayersNetworkContinue("startConflict");
      List<int> conflictResolutionOrder = AtOManager.Instance.GetConflictResolutionOrder();
      int index = 0;
      while (this.heroes[conflictResolutionOrder[index]] == null || (UnityEngine.Object) this.heroes[conflictResolutionOrder[index]].HeroData == (UnityEngine.Object) null)
        ++index;
      this.playerChoosing = conflictResolutionOrder[index];
      this.photonView.RPC("NET_ShareConflictOrder", RpcTarget.Others, (object) this.playerChoosing);
      while (!NetworkManager.Instance.AllPlayersReady("settingConflictOrder"))
        yield return (object) Globals.Instance.WaitForSeconds(0.01f);
      Functions.DebugLogGD("Game ready, Everybody checked settingConflictOrder");
      NetworkManager.Instance.PlayersNetworkContinue("settingConflictOrder");
      this.EnableButtonsForPlayerChoosing();
    }
    else
    {
      NetworkManager.Instance.SetStatusReady("startConflict");
      Functions.DebugLogGD("startConflict, we can continue!");
    }
  }

  public void Hide() => this.gameObject.SetActive(false);

  private void SetConflict()
  {
    this.heroes = AtOManager.Instance.GetTeam();
    this.SetCardbacks();
    for (int index = 0; index < this.heroes.Length; ++index)
      this.characterSPR[index].sprite = this.heroes[index].SpritePortrait;
    this.charRollIterations = new int[4];
    this.charRollResult = new int[4];
    this.cardOrder = 1100;
    this.finalTieTimes = 0;
    this.playerChoosing = -1;
    this.charRoll = new bool[4];
    this.charWinner = new bool[4];
    this.charWins.text = "";
    this.nickChoosing.text = "";
    for (int index = 0; index < 4; ++index)
    {
      this.charRoll[index] = true;
      this.charWinner[index] = false;
      this.xSymbol[index].gameObject.SetActive(false);
    }
  }

  private void SetCardbacks()
  {
    for (int index = 0; index < 4; ++index)
    {
      Hero hero = AtOManager.Instance.GetHero(index);
      string cardbackUsed = hero.CardbackUsed;
      if (cardbackUsed != "")
      {
        CardbackData cardbackData = Globals.Instance.GetCardbackData(cardbackUsed);
        Debug.Log((object) cardbackData);
        if ((UnityEngine.Object) cardbackData == (UnityEngine.Object) null)
        {
          cardbackData = Globals.Instance.GetCardbackData(Globals.Instance.GetCardbackBaseIdBySubclass(hero.HeroData.HeroSubClass.Id));
          if ((UnityEngine.Object) cardbackData == (UnityEngine.Object) null)
            cardbackData = Globals.Instance.GetCardbackData("defaultCardback");
        }
        Sprite cardbackSprite = cardbackData.CardbackSprite;
        if ((UnityEngine.Object) cardbackSprite != (UnityEngine.Object) null)
          this.cardbacks[index].sprite = cardbackSprite;
      }
    }
  }

  private void EnableButtonsForPlayerChoosing()
  {
    this.nickChoosing.text = string.Format(Texts.Instance.GetText("conflictPlayerChooses"), (object) NetworkManager.Instance.GetPlayerNickReal(this.heroes[this.playerChoosing].Owner));
    for (int index = 0; index < 4; ++index)
    {
      if (this.heroes[index] != null && (UnityEngine.Object) this.heroes[index].HeroData != (UnityEngine.Object) null)
      {
        this.nicks[index].text = NetworkManager.Instance.GetPlayerNickReal(this.heroes[index].Owner);
        this.nicks[index].color = Functions.HexToColor(NetworkManager.Instance.GetColorFromNick(this.heroes[index].Owner));
      }
    }
    if (!(this.heroes[this.playerChoosing].Owner == NetworkManager.Instance.GetPlayerNick()))
      return;
    this.botonConflict[0].Enable();
    this.botonConflict[1].Enable();
    this.botonConflict[2].Enable();
  }

  public void SetButtons()
  {
    this.botonConflict[0].Disable();
    this.botonConflict[1].Disable();
    this.botonConflict[2].Disable();
  }

  private IEnumerator SyncroOrder()
  {
    Functions.DebugLogGD("WaitingSyncro settingConflictOrder");
    if (NetworkManager.Instance.IsMaster())
    {
      this.playerChoosing = AtOManager.Instance.GetConflictResolutionOrder()[0];
      this.photonView.RPC("NET_ShareConflictOrder", RpcTarget.Others, (object) this.playerChoosing);
      while (!NetworkManager.Instance.AllPlayersReady("settingConflictOrder"))
        yield return (object) Globals.Instance.WaitForSeconds(0.01f);
      Functions.DebugLogGD("Game ready, Everybody checked settingConflictOrder");
      NetworkManager.Instance.PlayersNetworkContinue("settingConflictOrder");
      this.EnableButtonsForPlayerChoosing();
    }
  }

  public IEnumerator NET_ShareConflictOrderCo()
  {
    NetworkManager.Instance.SetWaitingSyncro("settingConflictOrder", true);
    NetworkManager.Instance.SetStatusReady("settingConflictOrder");
    Functions.DebugLogGD("settingConflictOrder, waiting");
    while (NetworkManager.Instance.WaitingSyncro["settingConflictOrder"])
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    Functions.DebugLogGD("settingConflictOrder, we can continue!");
    this.EnableButtonsForPlayerChoosing();
  }

  public void SelectOption(int option)
  {
    for (int index = 0; index < 3; ++index)
      this.botonConflict[index].Disable();
    this.botonConflict[option].ShowDisableMask(false);
    this.botonConflict[option].color = Functions.HexToColor("#E0A44E");
    this.botonConflict[option].SetColor();
    this.botonConflict[option].PermaBorder(true);
    if (!NetworkManager.Instance.IsMaster())
      this.photonView.RPC("NET_SelectConflictOptionFromSlave", RpcTarget.MasterClient, (object) option);
    else
      this.StartCoroutine(this.SelectOptionCo(option));
  }

  public void SelectOptionFromOutside(int option) => this.StartCoroutine(this.SelectOptionCo(option));

  private IEnumerator SelectOptionCo(int option)
  {
    ConflictManager conflictManager = this;
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    if (NetworkManager.Instance.IsMaster())
    {
      conflictManager.photonView.RPC("NET_SelectConflictOption", RpcTarget.Others, (object) option);
      while (!NetworkManager.Instance.AllPlayersReady("selectConflictOption"))
        yield return (object) Globals.Instance.WaitForSeconds(0.01f);
      Functions.DebugLogGD("Game ready, Everybody checked selectConflictOption");
      NetworkManager.Instance.PlayersNetworkContinue("selectConflictOption");
    }
    else
    {
      NetworkManager.Instance.SetWaitingSyncro("selectConflictOption", true);
      NetworkManager.Instance.SetStatusReady("selectConflictOption");
      while (NetworkManager.Instance.WaitingSyncro["selectConflictOption"])
        yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      Functions.DebugLogGD("selectConflictOption, we can continue!");
    }
    conflictManager.optionSelected = option;
    for (int index = 0; index < 3; ++index)
      conflictManager.botonConflict[index].Disable();
    conflictManager.botonConflict[option].ShowDisableMask(false);
    conflictManager.botonConflict[option].color = Functions.HexToColor("#E0A44E");
    conflictManager.botonConflict[option].SetColor();
    conflictManager.botonConflict[option].PermaBorder(true);
    conflictManager.StartCoroutine(conflictManager.RollCards());
  }

  private IEnumerator RollCards()
  {
    ConflictManager conflictManager = this;
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    int rolls = 0;
    for (int i = 0; i < 4; ++i)
    {
      if (conflictManager.heroes[i] != null && (UnityEngine.Object) conflictManager.heroes[i].HeroData != (UnityEngine.Object) null && conflictManager.charRoll[i])
      {
        yield return (object) Globals.Instance.WaitForSeconds(0.25f);
        conflictManager.DoCard(i);
        ++rolls;
      }
    }
    yield return (object) Globals.Instance.WaitForSeconds((float) (2.2999999523162842 + (double) rolls * 0.10000000149011612));
    conflictManager.StartCoroutine(conflictManager.RollResult());
  }

  private void DoCard(int heroIndex)
  {
    ++this.charRollIterations[heroIndex];
    if (this.charRollIterations[heroIndex] > 3)
      this.charRollIterations[heroIndex] = 3;
    this.charRollResult[heroIndex] = -1;
    List<string> stringList = new List<string>();
    for (int index = 0; index < this.heroes[heroIndex].Cards.Count; ++index)
      stringList.Add(this.heroes[heroIndex].Cards[index]);
    bool flag = false;
    string id = "";
    while (!flag)
    {
      id = stringList[MapManager.Instance.GetRandomIntRange(0, stringList.Count)];
      CardData cardData = Globals.Instance.GetCardData(id);
      if (cardData.CardClass != Enums.CardClass.Injury && cardData.CardClass != Enums.CardClass.Boon)
        flag = true;
    }
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.characterTcards[heroIndex]);
    CardItem component = gameObject.GetComponent<CardItem>();
    component.SetCard(id, false);
    component.SetCardback(this.heroes[heroIndex]);
    gameObject.transform.localPosition = new Vector3(0.0f, (float) (0.10000000149011612 - 0.10000000149011612 * (double) this.charRollIterations[heroIndex]), 0.0f);
    component.DoReward(false, true, selectable: false);
    component.SetDestinationLocalScale(1f);
    component.TopLayeringOrder("Book", this.cardOrder);
    this.cardOrder += 50;
    component.DisableCollider();
    this.charRollResult[heroIndex] = Globals.Instance.GetCardData(id).EnergyCost;
    this.StartCoroutine(this.ShowNumRoll(heroIndex));
  }

  private IEnumerator ShowNumRoll(int heroIndex)
  {
    if (!this.charRoll[heroIndex])
    {
      this.characterTnum[heroIndex].text = "";
    }
    else
    {
      yield return (object) Globals.Instance.WaitForSeconds(2.2f);
      this.characterTcards[heroIndex].GetChild(0).GetComponent<CardItem>().active = true;
      this.characterTnum[heroIndex].text = this.charRollResult[heroIndex].ToString();
    }
  }

  private IEnumerator RollResult()
  {
    ConflictManager conflictManager = this;
    bool flag1 = false;
    while (!flag1)
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      int num = 0;
      flag1 = true;
      for (int index = 0; index < 4; ++index)
      {
        if (conflictManager.charRoll[index] && conflictManager.charRollResult[index] == -1)
        {
          flag1 = false;
          break;
        }
        if (conflictManager.charRoll[index])
          num += conflictManager.charRollResult[index];
      }
    }
    Dictionary<int, int> source1 = new Dictionary<int, int>();
    for (int key = 0; key < 4; ++key)
    {
      conflictManager.charWinner[key] = false;
      if (conflictManager.heroes[key] != null && !((UnityEngine.Object) conflictManager.heroes[key].HeroData == (UnityEngine.Object) null) && conflictManager.charRoll[key])
        source1.Add(key, conflictManager.charRollResult[key]);
    }
    Dictionary<int, int> dictionary = source1.OrderBy<KeyValuePair<int, int>, int>((Func<KeyValuePair<int, int>, int>) (x => x.Value)).ToDictionary<KeyValuePair<int, int>, int, int>((Func<KeyValuePair<int, int>, int>) (x => x.Key), (Func<KeyValuePair<int, int>, int>) (x => x.Value));
    int num1 = -1;
    int index1 = -1;
    int index2 = dictionary.Count - 1;
    KeyValuePair<int, int> keyValuePair1;
    if (conflictManager.optionSelected == 2)
    {
      int num2 = dictionary.ElementAt<KeyValuePair<int, int>>(index2).Value;
      keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(index2 - 1);
      int num3 = keyValuePair1.Value;
      if (num2 == num3)
      {
        keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(index2);
        num1 = keyValuePair1.Value;
      }
      else
      {
        keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(index2);
        index1 = keyValuePair1.Key;
      }
    }
    else if (conflictManager.optionSelected == 0)
    {
      int num4 = dictionary.ElementAt<KeyValuePair<int, int>>(0).Value;
      keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(1);
      int num5 = keyValuePair1.Value;
      if (num4 == num5)
      {
        keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(0);
        num1 = keyValuePair1.Value;
      }
      else
      {
        keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(0);
        index1 = keyValuePair1.Key;
      }
    }
    else if (conflictManager.optionSelected == 1)
    {
      Dictionary<int, int> source2 = new Dictionary<int, int>();
      foreach (KeyValuePair<int, int> keyValuePair2 in dictionary)
        source2.Add(keyValuePair2.Key, Mathf.Abs(keyValuePair2.Value - 2));
      dictionary = source2.OrderBy<KeyValuePair<int, int>, int>((Func<KeyValuePair<int, int>, int>) (x => x.Value)).ToDictionary<KeyValuePair<int, int>, int, int>((Func<KeyValuePair<int, int>, int>) (x => x.Key), (Func<KeyValuePair<int, int>, int>) (x => x.Value));
      int num6 = dictionary.ElementAt<KeyValuePair<int, int>>(0).Value;
      keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(1);
      int num7 = keyValuePair1.Value;
      if (num6 == num7)
      {
        keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(0);
        num1 = keyValuePair1.Value;
      }
      else
      {
        keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(0);
        index1 = keyValuePair1.Key;
      }
    }
    if (index1 == -1)
    {
      string str1 = "";
      bool flag2 = true;
      for (int index3 = 0; index3 < dictionary.Count; ++index3)
      {
        if (index3 == 0)
        {
          Hero[] heroes = conflictManager.heroes;
          keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(index3);
          int key = keyValuePair1.Key;
          str1 = heroes[key].Owner;
        }
        else
        {
          string str2 = str1;
          Hero[] heroes = conflictManager.heroes;
          keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(index3);
          int key = keyValuePair1.Key;
          string owner = heroes[key].Owner;
          if (str2 != owner)
          {
            flag2 = false;
            break;
          }
        }
        Functions.DebugLogGD("sameOwner " + flag2.ToString() + " _ " + str1);
      }
      if (flag2)
      {
        keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(0);
        index1 = keyValuePair1.Key;
      }
      else if (dictionary.Count == 2)
      {
        ++conflictManager.finalTieTimes;
        if (conflictManager.finalTieTimes >= 3)
        {
          keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(UnityEngine.Random.Range(0, dictionary.Count));
          index1 = keyValuePair1.Key;
        }
      }
    }
    int heroWinner = -1;
    if (index1 == -1)
    {
      for (int index4 = 0; index4 < dictionary.Count; ++index4)
      {
        keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(index4);
        int key = keyValuePair1.Key;
        if (conflictManager.charRoll[key])
        {
          keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(index4);
          if (keyValuePair1.Value == num1)
          {
            conflictManager.charRoll[key] = true;
            goto label_50;
          }
        }
        conflictManager.charRoll[key] = false;
        conflictManager.TurnOffCharacter(key);
label_50:
        conflictManager.ClearNumRoll(key);
      }
      conflictManager.StartCoroutine(conflictManager.RollCards());
    }
    else
    {
      conflictManager.charWinner[index1] = true;
      for (int index5 = 0; index5 < dictionary.Count; ++index5)
      {
        keyValuePair1 = dictionary.ElementAt<KeyValuePair<int, int>>(index5);
        int key = keyValuePair1.Key;
        if (key != index1)
          conflictManager.TurnOffCharacter(key);
        else
          heroWinner = key;
        conflictManager.ClearNumRoll(key);
      }
      conflictManager.FinalResolution(heroWinner);
    }
  }

  private void ClearNumRoll(int heroIndex) => this.characterTnum[heroIndex].text = "";

  private void FinalResolution(int heroWinner)
  {
    this.buttons.gameObject.SetActive(false);
    this.charWinsTransform.gameObject.SetActive(true);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<size=+1>");
    stringBuilder.Append("<color=");
    stringBuilder.Append(NetworkManager.Instance.GetColorFromNick(this.heroes[heroWinner].Owner));
    stringBuilder.Append(">");
    stringBuilder.Append(NetworkManager.Instance.GetPlayerNickReal(this.heroes[heroWinner].Owner));
    stringBuilder.Append("</color></size><br>");
    this.charWins.text = string.Format(Texts.Instance.GetText("conflictPlayerWins"), (object) stringBuilder.ToString());
    MapManager.Instance.ResultConflict(this.heroes[heroWinner].Owner);
  }

  private void TurnOffCharacter(int heroIndex)
  {
    this.characterSPR[heroIndex].color = new Color(0.3f, 0.3f, 0.3f, 1f);
    this.xSymbol[heroIndex].gameObject.SetActive(true);
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    int absolutePosition = -1)
  {
    this._controllerList.Clear();
    if (this.botonConflict[0].IsEnabled())
    {
      this._controllerList.Add(this.botonConflict[0].transform);
      this._controllerList.Add(this.botonConflict[1].transform);
      this._controllerList.Add(this.botonConflict[2].transform);
    }
    for (int index = 0; index < 4; ++index)
    {
      if (Functions.TransformIsVisible(MapManager.Instance.sideCharacters.charArray[index].transform))
        this._controllerList.Add(MapManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
    }
    if (Functions.TransformIsVisible(PlayerUIManager.Instance.giveGold))
      this._controllerList.Add(PlayerUIManager.Instance.giveGold);
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }
}
