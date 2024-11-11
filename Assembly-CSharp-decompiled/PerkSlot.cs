// Decompiled with JetBrains decompiler
// Type: PerkSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class PerkSlot : MonoBehaviour
{
  public TMP_Text title;
  public TMP_Text cards;
  public SpriteRenderer background;
  public Transform backgroundDisable;
  public Transform numContainer;
  public int slot;
  public Transform saveButton;
  public Transform deleteButton;
  private string colorActive = "#37303C";
  private string colorHover = "#735687";
  private string colorEmpty = "#54475E";
  private BoxCollider2D collider;
  private bool initiated;

  private void Init()
  {
    this.collider = this.GetComponent<BoxCollider2D>();
    this.saveButton.GetComponent<BotonGeneric>().auxInt = this.slot;
    this.deleteButton.GetComponent<BotonGeneric>().auxInt = this.slot;
  }

  public void SetDisable(bool _state)
  {
    if (!this.initiated)
      this.Init();
    this.backgroundDisable.gameObject.SetActive(_state);
  }

  public void SetEmpty(bool state)
  {
    if (!this.initiated)
      this.Init();
    this.title.text = Texts.Instance.GetText("emptySave");
    this.background.color = Functions.HexToColor(this.colorEmpty);
    this.numContainer.gameObject.SetActive(false);
    this.saveButton.gameObject.SetActive(true);
    this.deleteButton.gameObject.SetActive(false);
    this.collider.enabled = false;
    if (state)
      this.saveButton.GetComponent<BotonGeneric>().Enable();
    else
      this.saveButton.GetComponent<BotonGeneric>().Disable();
  }

  public void SetActive(string _title, string _num)
  {
    if (!this.initiated)
      this.Init();
    this.title.text = _title;
    this.cards.text = _num;
    this.background.color = Functions.HexToColor(this.colorActive);
    this.numContainer.gameObject.SetActive(true);
    this.saveButton.gameObject.SetActive(false);
    this.deleteButton.gameObject.SetActive(true);
    this.collider.enabled = true;
    this.background.color = Functions.HexToColor(this.colorActive);
  }

  public void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive())
      return;
    this.background.color = Functions.HexToColor(this.colorHover);
    PopupManager.Instance.SetText(string.Format(Texts.Instance.GetText("loadThisPerkConf"), (object) this.cards.text), true);
  }

  public void OnMouseExit()
  {
    this.background.color = Functions.HexToColor(this.colorActive);
    PopupManager.Instance.ClosePopup();
  }

  public void OnMouseUp()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive())
      return;
    PerkTree.Instance.LoadPerkConfig(this.slot);
    PopupManager.Instance.ClosePopup();
  }
}
