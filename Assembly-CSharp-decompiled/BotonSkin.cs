// Decompiled with JetBrains decompiler
// Type: BotonSkin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class BotonSkin : MonoBehaviour
{
  public SpriteRenderer spriteSkin;
  public Transform overT;
  public Transform overRedT;
  public Transform overHoverT;
  public Transform lockT;
  public TMP_Text skinName;
  public int auxInt = -1000;
  private string dlcSku = "";
  private SkinData skinData;
  private bool locked;
  private bool active;
  private bool mustQuest;

  private void Awake()
  {
    if (this.skinName.transform.childCount > 0)
    {
      for (int index = 0; index < this.skinName.transform.childCount; ++index)
      {
        MeshRenderer component = this.skinName.transform.GetChild(index).GetComponent<MeshRenderer>();
        component.sortingLayerName = this.skinName.GetComponent<MeshRenderer>().sortingLayerName;
        component.sortingOrder = this.skinName.GetComponent<MeshRenderer>().sortingOrder;
      }
    }
    this.overT.gameObject.SetActive(false);
  }

  public void SetSelected(bool state) => this.overT.gameObject.SetActive(state);

  public void SetSkinData(SkinData _skinData)
  {
    this.skinData = _skinData;
    this.active = false;
    this.overT.gameObject.SetActive(false);
    this.overRedT.gameObject.SetActive(false);
    this.overHoverT.gameObject.SetActive(false);
    this.spriteSkin.sprite = this.skinData.SpritePortrait;
    string str = Texts.Instance.GetText(this.skinData.SkinName);
    if (str == "")
      str = this.skinData.SkinName;
    this.skinName.text = Functions.UppercaseFirst(str);
    this.locked = false;
    if (!this.skinData.BaseSkin)
    {
      if (this.skinData.Sku != "")
      {
        if (!SteamManager.Instance.PlayerHaveDLC(this.skinData.Sku))
        {
          this.locked = true;
          this.dlcSku = this.skinData.Sku;
        }
        else if (this.skinData.SteamStat != "" && SteamManager.Instance.GetStatInt(this.skinData.SteamStat) != 1)
        {
          this.locked = true;
          this.dlcSku = this.skinData.Sku;
          this.mustQuest = true;
        }
      }
      if (!this.locked && this.skinData.PerkLevel > 0 && PlayerManager.Instance.GetPerkRank(this.skinData.SkinSubclass.SubClassName) < this.skinData.PerkLevel)
        this.locked = true;
    }
    this.overT.gameObject.SetActive(false);
    if (!this.locked)
    {
      this.lockT.gameObject.SetActive(false);
      if (PlayerManager.Instance.SkinUsed.ContainsKey(this.skinData.SkinSubclass.SubClassName) && PlayerManager.Instance.SkinUsed[this.skinData.SkinSubclass.SubClassName] == this.skinData.SkinId)
      {
        this.overT.gameObject.SetActive(true);
        this.active = true;
        this.skinName.color = new Color(1f, 0.68f, 0.09f);
      }
      else
        this.skinName.color = new Color(1f, 1f, 1f, 1f);
    }
    else
    {
      this.skinName.color = Functions.HexToColor(Globals.Instance.ClassColor["warrior"]);
      this.lockT.gameObject.SetActive(true);
    }
  }

  private void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive())
      return;
    if (this.locked)
    {
      this.overRedT.gameObject.SetActive(true);
      PopupManager.Instance.SetText(!(this.dlcSku != "") ? string.Format(Texts.Instance.GetText("skinRequiredRankLevel"), (object) this.skinData.PerkLevel) : (!this.mustQuest ? string.Format(Texts.Instance.GetText("requiredDLC"), (object) SteamManager.Instance.GetDLCName(this.dlcSku)) : string.Format(Texts.Instance.GetText("requiredDLCandQuest"), (object) SteamManager.Instance.GetDLCName(this.dlcSku))), true, alwaysCenter: true);
    }
    else if (!this.active)
    {
      GameManager.Instance.SetCursorHover();
      this.overHoverT.gameObject.SetActive(true);
    }
    if (!((Object) this.skinData != (Object) null))
      return;
    HeroSelectionManager.Instance.charPopup.ShowSkin(this.skinData.SkinGo);
  }

  private void OnMouseOver()
  {
    if (AlertManager.Instance.IsActive())
      return;
    SettingsManager.Instance.IsActive();
  }

  private void OnMouseExit()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive())
      return;
    this.overRedT.gameObject.SetActive(false);
    this.overHoverT.gameObject.SetActive(false);
    GameManager.Instance.SetCursorPlain();
    PopupManager.Instance.ClosePopup();
    HeroSelectionManager.Instance.charPopup.ResetSkin();
  }

  public void OnMouseUp()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive() || this.locked)
      return;
    PlayerManager.Instance.SetSkin(this.skinData.SkinSubclass.SubClassName, this.skinData.SkinId);
    HeroSelectionManager.Instance.SetSkinIntoSubclassData(this.skinData.SkinSubclass.SubClassName, this.skinData.SkinId);
    HeroSelectionManager.Instance.charPopup.DoSkins();
  }
}
