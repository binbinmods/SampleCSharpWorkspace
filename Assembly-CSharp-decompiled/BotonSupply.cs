// Decompiled with JetBrains decompiler
// Type: BotonSupply
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class BotonSupply : MonoBehaviour
{
  public int column;
  public int row;
  private BotonGeneric bGeneric;
  private TMP_Text textBoton;
  public string supplyId = "";
  public bool available;

  private void Awake()
  {
    this.bGeneric = this.GetComponent<BotonGeneric>();
    this.textBoton = this.transform.GetChild(0).GetComponent<TMP_Text>();
  }

  private void Start()
  {
    this.SetId();
    this.SetText();
  }

  private void SetId() => this.supplyId = TownManager.Instance.GetUpgradeButtonId(this.column, this.row);

  public void ShowAvailable()
  {
    this.bGeneric.Enable();
    this.bGeneric.color = Functions.HexToColor("#426C41");
    this.bGeneric.SetColor();
    this.bGeneric.SetTextColor(Functions.HexToColor("#FFFFFF"));
    this.available = true;
  }

  public void ShowSelected()
  {
    this.bGeneric.Disable();
    this.bGeneric.color = Functions.HexToColor("#967443");
    this.bGeneric.SetColor();
    this.bGeneric.ShowDisableMask(false);
    this.bGeneric.SetTextColor(Functions.HexToColor("#FFFFFF"));
    this.available = false;
  }

  public void ShowLocked()
  {
    this.bGeneric.Disable();
    this.bGeneric.color = Functions.HexToColor("#9C9C9C");
    this.bGeneric.SetColor();
    this.bGeneric.SetTextColor(Functions.HexToColor("#999999"));
    this.available = false;
  }

  private void SetText() => this.textBoton.text = Texts.Instance.GetText(this.supplyId).Replace("<c>", "<color=#E0A44E>").Replace("</c>", "</color>");

  public void BuySupply()
  {
    int num = AlertManager.Instance.GetConfirmAnswer() ? 1 : 0;
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.BuySupply);
    if (num == 0)
      return;
    PlayerManager.Instance.PlayerBuySupply(this.supplyId);
    TownManager.Instance.RefreshTownUpgrades();
  }

  private void OnMouseUp()
  {
    if (!Functions.ClickedThisTransform(this.transform) || AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || !this.available)
      return;
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.BuySupply);
    AlertManager.Instance.AlertConfirmDouble(Texts.Instance.GetText("townAssignWarning"));
    this.bGeneric.HideBorderNow();
  }
}
