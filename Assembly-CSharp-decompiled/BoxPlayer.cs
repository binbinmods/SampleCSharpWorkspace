// Decompiled with JetBrains decompiler
// Type: BoxPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class BoxPlayer : MonoBehaviour
{
  public TMP_Text playerName;
  public Transform border;
  public SpriteRenderer background;
  public SpriteRenderer borderSPR;
  private bool boxEnabled;
  private bool active;
  public string playerNick = "";
  private bool skuDisabled;

  private void Awake() => this.HideBorder();

  public void Activate(bool state)
  {
    this.active = state;
    if (state)
      this.DrawBorder();
    else
      this.HideBorder();
  }

  public void SetName(string name)
  {
    this.playerNick = name;
    this.playerName.text = NetworkManager.Instance.GetPlayerNickReal(name);
    if (name != "")
    {
      this.background.color = Functions.HexToColor(NetworkManager.Instance.GetColorFromNick(name));
      this.boxEnabled = true;
    }
    else
      this.boxEnabled = false;
  }

  public void DrawBorder() => this.border.gameObject.SetActive(true);

  public void HideBorder() => this.border.gameObject.SetActive(false);

  public void DisableSKU(string _sku)
  {
    this.skuDisabled = true;
    this.background.color = Functions.HexToColor("#666666");
    this.borderSPR.color = Functions.HexToColor("#300000");
    this.GetComponent<PopupText>().text = string.Format(Texts.Instance.GetText("playerDontHaveDLC"), (object) SteamManager.Instance.GetDLCName(_sku));
  }

  private void OnMouseEnter()
  {
    if (this.skuDisabled)
    {
      this.DrawBorder();
    }
    else
    {
      if (!this.boxEnabled)
        return;
      this.DrawBorder();
    }
  }

  private void OnMouseExit()
  {
    if (this.skuDisabled)
    {
      this.HideBorder();
    }
    else
    {
      if (!this.boxEnabled || this.active)
        return;
      this.HideBorder();
    }
  }

  private void OnMouseUp()
  {
    if (this.skuDisabled)
      return;
    HeroSelectionManager.Instance.AssignPlayerToBox(this.playerNick, this.transform.parent.transform.parent.GetComponent<BoxSelection>().GetId());
  }
}
