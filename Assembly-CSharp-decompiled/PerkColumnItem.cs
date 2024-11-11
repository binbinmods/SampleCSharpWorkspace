// Decompiled with JetBrains decompiler
// Type: PerkColumnItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class PerkColumnItem : MonoBehaviour
{
  private SpriteRenderer perkIcon;
  private TMP_Text perkText;
  private Transform perkBg;
  private BoxCollider2D boxCollider;
  private PerkData perkData;
  private bool enabled;
  private bool active;
  private string textPopup = "";
  private string colorAvailable = "#226529";
  private string colorUnavailable = "#652523";
  private int pointsUsed;
  private int pointsAvailable;
  private string heroName;
  private string className;
  private string perkName;

  private void AwakeInit()
  {
    this.boxCollider = this.transform.GetComponent<BoxCollider2D>();
    this.perkIcon = this.transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
    this.perkText = this.transform.GetChild(1).transform.GetComponent<TMP_Text>();
    this.perkBg = this.transform.GetChild(2).transform;
    this.perkBg.gameObject.SetActive(false);
  }

  public void EnablePerk(bool state)
  {
    if ((Object) this.perkIcon == (Object) null)
      this.AwakeInit();
    if (state)
    {
      SpriteRenderer perkIcon = this.perkIcon;
      TMP_Text perkText = this.perkText;
      Color color1 = new Color(1f, 1f, 1f, 1f);
      Color color2 = color1;
      perkText.color = color2;
      Color color3 = color1;
      perkIcon.color = color3;
    }
    else
    {
      SpriteRenderer perkIcon = this.perkIcon;
      TMP_Text perkText = this.perkText;
      Color color4 = new Color(0.6f, 0.6f, 0.6f, 1f);
      Color color5 = color4;
      perkText.color = color5;
      Color color6 = color4;
      perkIcon.color = color6;
    }
    this.enabled = state;
  }

  public void SetPerk(
    string _className,
    string _heroName,
    int _index,
    int _subindex,
    int _pointsAvailable,
    int _pointsUsed)
  {
    this.heroName = _heroName;
    this.className = _className;
    this.pointsUsed = _pointsUsed;
    this.pointsAvailable = _pointsAvailable;
    this.perkName = _className + (_index + 1).ToString();
    switch (_subindex)
    {
      case 0:
        this.perkName += "a";
        break;
      case 1:
        this.perkName += "b";
        break;
      case 2:
        this.perkName += "c";
        break;
      case 3:
        this.perkName += "d";
        break;
      case 4:
        this.perkName += "e";
        break;
      case 5:
        this.perkName += "f";
        break;
    }
    this.active = PlayerManager.Instance.HeroHavePerk(_heroName, this.perkName);
    this.SetActive();
    this.perkData = Globals.Instance.GetPerkData(this.perkName);
    if (!((Object) this.perkData != (Object) null))
      return;
    if ((Object) this.perkData.Icon != (Object) null)
      this.perkIcon.sprite = this.perkData.Icon;
    this.perkText.text = this.perkData.IconTextValue;
    this.textPopup = Perk.PerkDescription(this.perkData, true, _index, this.pointsAvailable, this.enabled, this.active);
  }

  private void SetActive()
  {
    if ((Object) this.perkBg == (Object) null)
      this.AwakeInit();
    if (this.active)
    {
      this.perkBg.gameObject.SetActive(true);
      this.perkBg.GetComponent<SpriteRenderer>().color = new Color(0.39f, 0.2f, 0.52f, 1f);
    }
    else
    {
      this.perkBg.gameObject.SetActive(false);
      this.perkBg.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    }
  }

  private void AssignPerk()
  {
    PlayerManager.Instance.AssignPerk(this.heroName, this.perkName);
    HeroSelectionManager.Instance.RefreshPerkPoints(this.heroName);
  }

  private void OnMouseUp()
  {
    Debug.Log((object) this.enabled);
    if (GameManager.Instance.IsLoadingGame())
    {
      AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("cantModifyPerkMP"));
    }
    else
    {
      if (this.enabled)
        this.AssignPerk();
      this.DoPopup();
    }
  }

  private void DoPopup()
  {
    if (!(this.textPopup != "") || (Object) this.perkData.Icon == (Object) null)
      return;
    AuraCurseData auraCurseData = Globals.Instance.GetAuraCurseData(this.perkData.Icon.name);
    string keynote = "";
    if ((Object) auraCurseData != (Object) null)
      keynote = Functions.UppercaseFirst(this.perkData.Icon.name);
    PopupManager.Instance.SetPerk(this.perkName, this.textPopup, keynote);
    if (this.enabled)
      PopupManager.Instance.SetBackgroundColor(this.colorAvailable);
    else
      PopupManager.Instance.SetBackgroundColor(this.colorUnavailable);
  }

  private void OnMouseEnter()
  {
    this.DoPopup();
    if (this.active)
      return;
    this.perkBg.gameObject.SetActive(true);
  }

  private void OnMouseExit()
  {
    if (!this.active)
      this.perkBg.gameObject.SetActive(false);
    PopupManager.Instance.ClosePopup();
  }
}
