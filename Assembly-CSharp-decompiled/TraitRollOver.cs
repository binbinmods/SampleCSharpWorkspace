// Decompiled with JetBrains decompiler
// Type: TraitRollOver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Text;
using TMPro;
using UnityEngine;

public class TraitRollOver : MonoBehaviour
{
  private Color actColor;
  public TMP_Text traitName;
  public string traitId;
  public SpriteRenderer background;
  private TraitData td;
  private int traitLevel;
  private CardItem CI;

  private void Start() => this.actColor = this.background.color;

  public void SetTrait(string _traitId, int _traitLevel = 0)
  {
    this.td = Globals.Instance.GetTraitData(_traitId);
    if (!((Object) this.td != (Object) null))
      return;
    StringBuilder stringBuilder = new StringBuilder();
    if ((Object) this.td.TraitCard != (Object) null)
    {
      stringBuilder.Append("<voffset=-.15><size=2.6><sprite name=cards></size></voffset>");
      stringBuilder.Append(Globals.Instance.GetCardData(this.td.TraitCard.Id, false).CardName);
      this.CI = this.GetComponent<CardItem>();
      if ((Object) this.CI != (Object) null)
      {
        this.CI.cardoutsidelibary = true;
        this.CI.cardoutsidecombat = true;
      }
    }
    else
    {
      stringBuilder.Append(this.td.TraitName);
      this.CI = this.GetComponent<CardItem>();
      if ((Object) this.CI != (Object) null)
        this.CI.RemoveCardData();
    }
    this.traitName.text = stringBuilder.ToString();
    this.traitId = _traitId;
    this.traitLevel = _traitLevel;
  }

  private void OnMouseEnter()
  {
    if ((Object) this.td != (Object) null)
    {
      if ((Object) this.td.TraitCard == (Object) null)
      {
        PopupManager.Instance.SetTrait(this.td);
      }
      else
      {
        string id = this.td.TraitCard.Id;
        if (this.traitLevel == 0)
          this.CI.CreateAmplifyOutsideCard(Globals.Instance.GetCardData(id, false), this.GetComponent<BoxCollider2D>());
        else if (this.traitLevel == 1)
          this.CI.CreateAmplifyOutsideCard(Globals.Instance.GetCardData(Globals.Instance.GetCardData(id, false).UpgradesTo1), this.GetComponent<BoxCollider2D>());
        else if (this.traitLevel == 2)
          this.CI.CreateAmplifyOutsideCard(Globals.Instance.GetCardData(Globals.Instance.GetCardData(id, false).UpgradesTo2), this.GetComponent<BoxCollider2D>());
      }
    }
    if (TomeManager.Instance.IsActive())
      this.background.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
    else
      this.background.color = new Color(1f, 0.5f, 0.13f, 0.25f);
  }

  private void OnMouseExit()
  {
    GameManager.Instance.CleanTempContainer();
    PopupManager.Instance.ClosePopup();
    this.background.color = this.actColor;
  }
}
