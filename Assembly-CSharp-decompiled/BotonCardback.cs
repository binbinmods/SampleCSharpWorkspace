// Decompiled with JetBrains decompiler
// Type: BotonCardback
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class BotonCardback : MonoBehaviour
{
  public SpriteRenderer spriteCardback;
  public Transform overT;
  public Transform overRedT;
  public Transform overHoverT;
  public Transform lockT;
  public TMP_Text cardbackName;
  public int auxInt = -1000;
  private CardbackData cardbackData;
  private bool unlocked;
  private bool active;
  private string subclassName = "";
  private Color colorCardbackActive = new Color(1f, 0.68f, 0.09f);
  private Color colorCardbackAvailable = new Color(1f, 1f, 1f, 1f);
  private Color colorCardbackLocked = new Color(0.92f, 0.28f, 0.33f, 1f);

  private void Awake()
  {
    if (this.cardbackName.transform.childCount > 0)
    {
      for (int index = 0; index < this.cardbackName.transform.childCount; ++index)
      {
        MeshRenderer component = this.cardbackName.transform.GetChild(index).GetComponent<MeshRenderer>();
        component.sortingLayerName = this.cardbackName.GetComponent<MeshRenderer>().sortingLayerName;
        component.sortingOrder = this.cardbackName.GetComponent<MeshRenderer>().sortingOrder;
      }
    }
    this.overT.gameObject.SetActive(false);
  }

  public void SetSelected(bool state) => this.overT.gameObject.SetActive(state);

  public void SetCardbackData(string _cardbackDataId, bool _unlocked, string _subclassName)
  {
    this.cardbackData = Globals.Instance.GetCardbackData(_cardbackDataId);
    if ((Object) this.cardbackData == (Object) null)
      return;
    this.active = false;
    this.overT.gameObject.SetActive(false);
    this.overRedT.gameObject.SetActive(false);
    this.overHoverT.gameObject.SetActive(false);
    this.spriteCardback.sprite = this.cardbackData.CardbackSprite;
    string input = Texts.Instance.GetText(this.cardbackData.CardbackName);
    if (input == "")
      input = this.cardbackData.CardbackName;
    this.cardbackName.text = input.OnlyFirstCharToUpper();
    this.unlocked = _unlocked;
    this.subclassName = _subclassName;
    this.overT.gameObject.SetActive(false);
    if (this.unlocked)
    {
      this.lockT.gameObject.SetActive(false);
      if (PlayerManager.Instance.GetActiveCardback(_subclassName) != null && PlayerManager.Instance.GetActiveCardback(_subclassName) == this.cardbackData.CardbackId.ToLower())
      {
        this.overT.gameObject.SetActive(true);
        this.active = true;
        this.cardbackName.color = this.colorCardbackActive;
      }
      else
        this.cardbackName.color = this.colorCardbackAvailable;
    }
    else
    {
      this.cardbackName.color = this.colorCardbackLocked;
      this.lockT.gameObject.SetActive(true);
    }
  }

  private void OnMouseEnter()
  {
    if ((Object) this.cardbackData == (Object) null || AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive())
      return;
    if (!this.unlocked)
    {
      this.overRedT.gameObject.SetActive(true);
      string theText = "";
      if (this.cardbackData.Sku != "")
      {
        if (!SteamManager.Instance.PlayerHaveDLC(this.cardbackData.Sku))
          theText = string.Format(Texts.Instance.GetText("requiredDLC"), (object) SteamManager.Instance.GetDLCName(this.cardbackData.Sku));
      }
      else if (this.cardbackData.SteamStat != "")
        theText = string.Format(Texts.Instance.GetText("requiredWeekly"), (object) Texts.Instance.GetText(this.cardbackData.SteamStat));
      else if (this.cardbackData.AdventureLevel > 0)
        theText = this.cardbackData.AdventureLevel != 1 ? string.Format(Texts.Instance.GetText("requiredAdventureLevel"), (object) this.cardbackData.AdventureLevel) : Texts.Instance.GetText("requiredAdventureComplete");
      else if (this.cardbackData.ObeliskLevel > 0)
        theText = this.cardbackData.ObeliskLevel != 1 ? string.Format(Texts.Instance.GetText("requiredObeliskLevel"), (object) this.cardbackData.ObeliskLevel) : Texts.Instance.GetText("requiredObeliskComplete");
      else if (this.cardbackData.RankLevel > 0)
        theText = string.Format(Texts.Instance.GetText("skinRequiredRankLevel"), (object) this.cardbackData.RankLevel);
      if (!(theText != ""))
        return;
      PopupManager.Instance.SetText(theText, true, alwaysCenter: true);
    }
    else
    {
      if (this.active)
        return;
      GameManager.Instance.SetCursorHover();
      this.overHoverT.gameObject.SetActive(true);
    }
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
  }

  public void OnMouseUp()
  {
    if ((Object) this.cardbackData == (Object) null || AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive() || !this.unlocked)
      return;
    PlayerManager.Instance.SetCardback(this.subclassName, this.cardbackData.CardbackId);
    HeroSelectionManager.Instance.AssignHeroCardback(this.subclassName, this.cardbackData.CardbackId);
    HeroSelectionManager.Instance.charPopup.DoCardbacks();
  }
}
