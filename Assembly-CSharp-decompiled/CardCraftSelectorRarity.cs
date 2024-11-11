// Decompiled with JetBrains decompiler
// Type: CardCraftSelectorRarity
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CardCraftSelectorRarity : MonoBehaviour
{
  public Transform tCommon;
  public Transform tUncommon;
  public Transform tRare;
  public Transform tEpic;
  public Transform tMythic;
  public Transform tLock;
  public Enums.CardRarity rarity;
  public Transform tOn;
  public Transform tOff;
  private bool enabled;
  private bool locked;

  private void Start()
  {
    this.SetEnable(false);
    this.SetRarity(this.rarity);
  }

  public void SetRarity(Enums.CardRarity rarity)
  {
    this.tCommon.gameObject.SetActive(false);
    this.tUncommon.gameObject.SetActive(false);
    this.tRare.gameObject.SetActive(false);
    this.tEpic.gameObject.SetActive(false);
    this.tMythic.gameObject.SetActive(false);
    switch (rarity)
    {
      case Enums.CardRarity.Common:
        this.tCommon.gameObject.SetActive(true);
        break;
      case Enums.CardRarity.Uncommon:
        this.tUncommon.gameObject.SetActive(true);
        break;
      case Enums.CardRarity.Rare:
        this.tRare.gameObject.SetActive(true);
        break;
      case Enums.CardRarity.Epic:
        this.tEpic.gameObject.SetActive(true);
        break;
      default:
        this.tMythic.gameObject.SetActive(true);
        break;
    }
  }

  public void SetEnable(bool state)
  {
    this.enabled = state;
    if (state)
    {
      this.tOn.gameObject.SetActive(true);
      this.tOff.gameObject.SetActive(false);
    }
    else
    {
      this.tOn.gameObject.SetActive(false);
      this.tOff.gameObject.SetActive(true);
    }
  }

  private void OnMouseEnter()
  {
    if (this.locked || this.enabled)
      return;
    this.tOn.gameObject.SetActive(true);
    this.tOff.gameObject.SetActive(false);
  }

  private void OnMouseExit()
  {
    if (this.locked || this.enabled)
      return;
    this.tOn.gameObject.SetActive(false);
    this.tOff.gameObject.SetActive(true);
  }

  private void OnMouseUp()
  {
    if (this.locked)
      return;
    this.SetEnable(true);
    CardCraftManager.Instance.CraftSelectorRarity(this, this.rarity);
  }
}
