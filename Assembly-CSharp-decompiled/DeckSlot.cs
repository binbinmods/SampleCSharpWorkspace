// Decompiled with JetBrains decompiler
// Type: DeckSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class DeckSlot : MonoBehaviour
{
  public Transform icon;
  public TMP_Text title;
  public TMP_Text cards;
  public SpriteRenderer background;
  public Transform numContainer;
  public int slot;
  public Transform saveButton;
  public Transform deleteButton;
  private string colorActive = "#AA580A";
  private string colorHover = "#00EDFF";
  private string colorEmpty = "#999999";
  private BoxCollider2D collider;

  private void Awake()
  {
    this.collider = this.GetComponent<BoxCollider2D>();
    this.saveButton.GetComponent<BotonGeneric>().auxInt = this.slot;
    this.deleteButton.GetComponent<BotonGeneric>().auxInt = this.slot;
  }

  public void SetEmpty(bool state)
  {
    this.title.text = Texts.Instance.GetText("emptySave");
    this.background.color = Functions.HexToColor(this.colorEmpty);
    this.icon.gameObject.SetActive(false);
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
    this.title.text = _title;
    this.cards.text = _num;
    this.background.color = Functions.HexToColor(this.colorActive);
    this.icon.gameObject.SetActive(true);
    this.numContainer.gameObject.SetActive(true);
    this.saveButton.gameObject.SetActive(false);
    this.deleteButton.gameObject.SetActive(true);
    this.collider.enabled = true;
    this.background.color = Functions.HexToColor(this.colorActive);
  }

  public void OnMouseEnter() => this.background.color = Functions.HexToColor(this.colorHover);

  public void OnMouseExit() => this.background.color = Functions.HexToColor(this.colorActive);

  public void OnMouseUp() => CardCraftManager.Instance.LoadDeck(this.slot);
}
