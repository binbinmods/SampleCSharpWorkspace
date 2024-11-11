// Decompiled with JetBrains decompiler
// Type: GiveManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GiveManager : MonoBehaviour
{
  public Transform elements;
  public TMP_Text quantityText;
  public TMP_Text target;
  public TMP_Text descriptionText;
  public int playerTarget;
  public int quantity;
  public Transform prevPlayer;
  public Transform nextPlayer;
  public Transform bgGold;
  public Transform bgShards;
  public BotonGeneric botonGold;
  public BotonGeneric botonDust;
  public Transform botonGive;
  private int type;
  public List<Transform> buttonsController;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();

  public static GiveManager Instance { get; private set; }

  private void Awake()
  {
    if ((Object) GiveManager.Instance == (Object) null)
      GiveManager.Instance = this;
    else if ((Object) GiveManager.Instance != (Object) this)
      Object.Destroy((Object) this.gameObject);
    Object.DontDestroyOnLoad((Object) this.gameObject);
  }

  public bool IsActive() => this.elements.gameObject.activeSelf;

  public void ShowGive(bool state, int _type = 0)
  {
    if (GameManager.Instance.IsMultiplayer() && state && AtOManager.Instance.townDivinationCreator == NetworkManager.Instance.GetPlayerNick())
    {
      AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("cantGiveGoldDivination"));
    }
    else
    {
      this.elements.gameObject.SetActive(state);
      if (state)
      {
        if ((bool) (Object) MapManager.Instance)
          MapManager.Instance.characterWindow.Hide();
        else if ((bool) (Object) TownManager.Instance)
        {
          TownManager.Instance.characterWindow.Hide();
          TownManager.Instance.ShowTownUpgrades(false);
        }
        this.botonGive.gameObject.SetActive(false);
        this.playerTarget = 0;
        if (this.playerTarget == NetworkManager.Instance.GetMyPosition())
          this.NextTarget();
        this.SetQuantity(0);
        if (NetworkManager.Instance.GetNumPlayers() > 2)
        {
          this.prevPlayer.gameObject.SetActive(true);
          this.nextPlayer.gameObject.SetActive(true);
        }
        else
        {
          this.prevPlayer.gameObject.SetActive(false);
          this.nextPlayer.gameObject.SetActive(false);
        }
        this.type = _type;
        if (this.type == 0)
        {
          this.bgGold.gameObject.SetActive(true);
          this.bgShards.gameObject.SetActive(false);
          this.descriptionText.text = Texts.Instance.GetText("selectGiveGold");
          this.botonGold.Disable();
          this.botonDust.Enable();
        }
        else
        {
          this.bgGold.gameObject.SetActive(false);
          this.bgShards.gameObject.SetActive(true);
          this.descriptionText.text = Texts.Instance.GetText("selectGiveShards");
          this.botonGold.Enable();
          this.botonDust.Disable();
        }
        this.RefreshTargetName();
        if (!(bool) (Object) CardCraftManager.Instance)
          return;
        CardCraftManager.Instance.ShowSearch(false);
      }
      else
      {
        this.target.text = "";
        if (!(bool) (Object) CardCraftManager.Instance || CardCraftManager.Instance.craftType != 2)
          return;
        CardCraftManager.Instance.ShowSearch(true);
      }
    }
  }

  private void SetQuantity(int q)
  {
    this.quantity = q;
    if (this.quantity < 0)
      this.quantity = 0;
    else if (this.type == 0 && this.quantity > AtOManager.Instance.GetPlayerGold())
      this.quantity = AtOManager.Instance.GetPlayerGold();
    else if (this.type == 1 && this.quantity > AtOManager.Instance.GetPlayerDust())
      this.quantity = AtOManager.Instance.GetPlayerDust();
    this.quantityText.text = this.quantity.ToString();
    if (this.quantity != 0)
      this.botonGive.gameObject.SetActive(true);
    else
      this.botonGive.gameObject.SetActive(false);
  }

  public void Give(int q) => this.SetQuantity(this.quantity + q);

  public void NextTarget()
  {
    ++this.playerTarget;
    if (this.playerTarget >= NetworkManager.Instance.GetNumPlayers())
      this.playerTarget = 0;
    if (this.playerTarget == NetworkManager.Instance.GetMyPosition())
      this.NextTarget();
    else
      this.RefreshTargetName();
  }

  private void RefreshTargetName()
  {
    StringBuilder stringBuilder = new StringBuilder();
    string playerNickPosition = NetworkManager.Instance.GetPlayerNickPosition(this.playerTarget);
    stringBuilder.Append(NetworkManager.Instance.GetPlayerNickReal(playerNickPosition));
    stringBuilder.Append("<br><size=3.5><color=#BBB>(");
    Hero[] team = AtOManager.Instance.GetTeam();
    for (int index = 0; index < team.Length; ++index)
    {
      if (team[index].Owner == playerNickPosition)
      {
        stringBuilder.Append(team[index].HeroData.HeroSubClass.CharacterName);
        stringBuilder.Append(", ");
      }
    }
    stringBuilder.Remove(stringBuilder.Length - 2, 2);
    stringBuilder.Append(")");
    this.target.text = stringBuilder.ToString();
  }

  public void PrevTarget()
  {
    --this.playerTarget;
    if (this.playerTarget < 0)
      this.playerTarget = NetworkManager.Instance.GetNumPlayers() - 1;
    if (this.playerTarget == NetworkManager.Instance.GetMyPosition())
      this.PrevTarget();
    else
      this.RefreshTargetName();
  }

  public void GiveAction()
  {
    if (this.quantity <= 0)
      return;
    if (NetworkManager.Instance.IsMaster())
      AtOManager.Instance.GivePlayer(this.type, this.quantity, NetworkManager.Instance.GetPlayerNickPosition(this.playerTarget), NetworkManager.Instance.GetPlayerNick(), save: true);
    else
      AtOManager.Instance.AskGivePlayerToPlayer(this.type, this.quantity, NetworkManager.Instance.GetPlayerNickPosition(this.playerTarget), NetworkManager.Instance.GetPlayerNick());
    this.ShowGive(false);
  }

  public void ControllerMovement(bool goingUp = false, bool goingRight = false, bool goingDown = false, bool goingLeft = false)
  {
    this._controllerList.Clear();
    if (Functions.TransformIsVisible(this.nextPlayer))
    {
      this._controllerList.Add(this.nextPlayer);
      this._controllerList.Add(this.prevPlayer);
    }
    if (Functions.TransformIsVisible(this.botonGive))
      this._controllerList.Add(this.botonGive);
    for (int index = 0; index < this.buttonsController.Count; ++index)
    {
      if (Functions.TransformIsVisible(this.buttonsController[index]))
        this._controllerList.Add(this.buttonsController[index]);
    }
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((Object) this._controllerList[this.controllerHorizontalIndex] != (Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }
}
