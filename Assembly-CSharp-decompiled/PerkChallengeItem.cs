// Decompiled with JetBrains decompiler
// Type: PerkChallengeItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class PerkChallengeItem : MonoBehaviour
{
  private SpriteRenderer perkIcon;
  private TMP_Text perkText;
  private Transform perkBg;
  private Transform perkActive;
  private BoxCollider2D boxCollider;
  private PerkData perkData;
  private bool enabled;
  private bool active;
  private string textPopup = "";
  private string colorAvailable = "#226529";
  private string colorUnavailable = "#652523";
  private int heroId;
  private int index = -1;

  private void Awake()
  {
    this.boxCollider = this.transform.GetComponent<BoxCollider2D>();
    this.perkIcon = this.transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
    this.perkText = this.transform.GetChild(1).transform.GetComponent<TMP_Text>();
    this.perkBg = this.transform.GetChild(2).transform;
    this.perkActive = this.transform.GetChild(4).transform;
    if (!this.perkBg.gameObject.activeSelf)
      return;
    this.perkBg.gameObject.SetActive(false);
  }

  public void EnablePerk(bool state) => this.enabled = state;

  public void SetPerk(int _heroId, int _index, string _perkId)
  {
    this.heroId = _heroId;
    this.index = _index;
    if (this.perkBg.gameObject.activeSelf)
      this.perkBg.gameObject.SetActive(false);
    this.SetActive(false);
    this.perkData = Globals.Instance.GetPerkData(_perkId);
    if (!((Object) this.perkData != (Object) null))
      return;
    if ((Object) this.perkData.Icon != (Object) null)
      this.perkIcon.sprite = this.perkData.Icon;
    this.perkText.text = this.perkData.IconTextValue;
    this.textPopup = Perk.PerkDescription(this.perkData, true, _index, 0, this.enabled, this.active);
    this.EnablePerk(true);
  }

  public void SetActive(bool state)
  {
    if (this.perkActive.gameObject.activeSelf != state)
      this.perkActive.gameObject.SetActive(state);
    this.active = state;
  }

  private void AssignPerk() => ChallengeSelectionManager.Instance.AssignPerk(this.heroId, this.index);

  public void OnMouseUp()
  {
    if (this.enabled)
      this.AssignPerk();
    this.DoPopup();
  }

  private void DoPopup()
  {
    if (!(this.textPopup != ""))
      return;
    AuraCurseData auraCurseData = Globals.Instance.GetAuraCurseData(this.perkData.Icon.name);
    string keynote = "";
    if ((Object) auraCurseData != (Object) null)
      keynote = Functions.UppercaseFirst(this.perkData.Icon.name);
    PopupManager.Instance.SetPerk("", this.textPopup, keynote);
    if (this.enabled)
      PopupManager.Instance.SetBackgroundColor(this.colorAvailable);
    else
      PopupManager.Instance.SetBackgroundColor(this.colorUnavailable);
  }

  private void OnMouseEnter()
  {
    this.DoPopup();
    if (this.active || this.perkBg.gameObject.activeSelf)
      return;
    this.perkBg.gameObject.SetActive(true);
  }

  private void OnMouseExit()
  {
    if (!this.active && this.perkBg.gameObject.activeSelf)
      this.perkBg.gameObject.SetActive(false);
    PopupManager.Instance.ClosePopup();
  }
}
