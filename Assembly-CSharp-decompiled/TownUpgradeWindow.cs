// Decompiled with JetBrains decompiler
// Type: TownUpgradeWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TownUpgradeWindow : MonoBehaviour
{
  public Transform elements;
  public List<BotonSupply> botonSupply;
  public TMP_Text requiredTM;
  public Transform exitButton;
  public Transform resetButton;
  public Transform pointsButton;
  public Transform sellSupplyT;
  public Transform sellSupplyButton;
  public TMP_Text supplySellQuantity;
  public TMP_Text supplySellResult;
  public Transform supplySellBg;
  public List<Transform> sellSupplyTransforms;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();

  private void Start()
  {
    if (GameManager.Instance.GetDeveloperMode())
    {
      this.resetButton.gameObject.SetActive(true);
      this.pointsButton.gameObject.SetActive(true);
    }
    else
    {
      this.resetButton.gameObject.SetActive(false);
      this.pointsButton.gameObject.SetActive(false);
    }
    this.sellSupplyT.gameObject.SetActive(false);
  }

  public void Show(bool state)
  {
    this.elements.gameObject.SetActive(state);
    if (state)
    {
      this.StartCoroutine(this.SetButtonsCo());
      TownManager.Instance.ShowButtons(false);
    }
    else
      TownManager.Instance.ShowButtons(true);
  }

  public void Refresh()
  {
    this.SetButtons();
    TownManager.Instance.SetTownBuildings();
  }

  public void ShowSellSupply(bool state)
  {
    this.sellSupplyT.gameObject.SetActive(state);
    this.supplySellQuantity.text = "0";
    this.ModifySupplyQuantity(0);
  }

  public void ModifySupplyQuantity(int _quantity)
  {
    int quantity = int.Parse(this.supplySellQuantity.text.Split(' ', StringSplitOptions.None)[0]) + _quantity;
    if (quantity < 0)
      quantity = 0;
    else if (quantity > PlayerManager.Instance.SupplyActual)
      quantity = PlayerManager.Instance.SupplyActual;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(quantity);
    stringBuilder.Append(" <voffset=-.2><size=-.1><sprite name=supply>");
    this.supplySellQuantity.text = stringBuilder.ToString();
    this.ConvertSupply(quantity);
  }

  private void ConvertSupply(int quantity)
  {
    int num = quantity * 100;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(num);
    stringBuilder.Append(" <sprite name=gold>");
    stringBuilder.Append(" ");
    stringBuilder.Append(num);
    stringBuilder.Append(" <sprite name=dust>");
    this.supplySellResult.text = stringBuilder.ToString();
    this.supplySellBg.gameObject.SetActive(true);
  }

  public void SellSupplyAction()
  {
    AtOManager.Instance.SellSupply(int.Parse(this.supplySellQuantity.text.Split(' ', StringSplitOptions.None)[0]));
    this.ShowSellSupply(false);
  }

  private IEnumerator SetButtonsCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    this.SetButtons();
  }

  private void SetButtons()
  {
    int playerSupplyActual = PlayerManager.Instance.GetPlayerSupplyActual();
    int num = PlayerManager.Instance.TotalPointsSpentInSupplys();
    if (num < 30)
    {
      this.requiredTM.gameObject.SetActive(true);
      this.requiredTM.text = string.Format(Texts.Instance.GetText("townRequired"), (object) (30 - num));
    }
    else
      this.requiredTM.gameObject.SetActive(false);
    for (int index = 0; index < this.botonSupply.Count; ++index)
    {
      if (PlayerManager.Instance.PlayerHaveSupply(this.botonSupply[index].supplyId))
        this.botonSupply[index].ShowSelected();
      else if (this.botonSupply[index].row == 1 && playerSupplyActual > 0)
        this.botonSupply[index].ShowAvailable();
      else if (PlayerManager.Instance.PointsRequiredForSupply(this.botonSupply[index].supplyId) <= playerSupplyActual && PlayerManager.Instance.PlayerHaveSupply(PlayerManager.Instance.SupplyRequiredForSupply(this.botonSupply[index].supplyId)))
      {
        if (this.botonSupply[index].row <= 3 || this.botonSupply[index].row > 3 && num >= 30)
          this.botonSupply[index].ShowAvailable();
        else
          this.botonSupply[index].ShowLocked();
      }
      else
        this.botonSupply[index].ShowLocked();
    }
    if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_6_6") && AtOManager.Instance.GetNgPlus() < 6)
      this.sellSupplyButton.gameObject.SetActive(true);
    else
      this.sellSupplyButton.gameObject.SetActive(false);
  }

  public bool IsActive() => this.elements.gameObject.activeSelf;

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    int absolutePosition = -1)
  {
    this._controllerList.Clear();
    if (Functions.TransformIsVisible(this.sellSupplyT))
    {
      for (int index = 0; index < this.sellSupplyTransforms.Count; ++index)
        this._controllerList.Add(this.sellSupplyTransforms[index].transform);
    }
    else
    {
      for (int index = 0; index < this.botonSupply.Count; ++index)
        this._controllerList.Add(this.botonSupply[index].transform);
      if (Functions.TransformIsVisible(this.sellSupplyButton))
      {
        this._controllerList.Add(this.sellSupplyButton);
        this._controllerList.Add(this.exitButton);
      }
      else
      {
        this._controllerList.Add(this.exitButton);
        this._controllerList.Add(this.exitButton);
      }
    }
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }
}
