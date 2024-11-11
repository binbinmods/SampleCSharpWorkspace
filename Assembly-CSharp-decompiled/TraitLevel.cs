// Decompiled with JetBrains decompiler
// Type: TraitLevel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Text;
using TMPro;
using UnityEngine;

public class TraitLevel : MonoBehaviour
{
  public Transform boxInnate;
  public Transform textInnate;
  public Transform boxRegular;
  public Transform textRegular;
  private SpriteRenderer boxSpr;
  private TMP_Text boxText;
  private Color colorText;
  private int heroIndex;
  private int boxType;
  private TraitData traitData;
  private string heroColor = "";
  private Animator anim;
  private bool enabled;
  private bool active;
  private BoxCollider2D collider;
  private int traitLevel;
  private CardItem CI;

  private void Awake()
  {
    this.collider = this.GetComponent<BoxCollider2D>();
    this.anim = this.GetComponent<Animator>();
    this.anim.enabled = false;
    this.CI = this.GetComponent<CardItem>();
  }

  public void SetHeroIndex(int _heroIndex) => this.heroIndex = _heroIndex;

  public void SetColor(string _color) => this.heroColor = _color;

  public void SetPosition(int _boxType)
  {
    switch (_boxType)
    {
      case 1:
        this.boxRegular.gameObject.SetActive(true);
        this.textRegular.gameObject.SetActive(true);
        this.boxInnate.gameObject.SetActive(false);
        this.textInnate.gameObject.SetActive(false);
        this.boxSpr = this.boxRegular.GetComponent<SpriteRenderer>();
        this.boxText = this.textRegular.GetComponent<TMP_Text>();
        break;
      case 2:
        this.boxRegular.gameObject.SetActive(true);
        this.boxRegular.localScale = new Vector3(-1f, this.boxRegular.localScale.y, 1f);
        this.textRegular.gameObject.SetActive(true);
        this.boxInnate.gameObject.SetActive(false);
        this.textInnate.gameObject.SetActive(false);
        this.boxSpr = this.boxRegular.GetComponent<SpriteRenderer>();
        this.boxText = this.textRegular.GetComponent<TMP_Text>();
        break;
      default:
        this.boxRegular.gameObject.SetActive(false);
        this.textRegular.gameObject.SetActive(false);
        this.boxInnate.gameObject.SetActive(true);
        this.textInnate.gameObject.SetActive(true);
        this.boxSpr = this.boxInnate.GetComponent<SpriteRenderer>();
        this.boxText = this.textInnate.GetComponent<TMP_Text>();
        break;
    }
    this.boxType = _boxType;
    this.colorText = this.boxText.color;
  }

  public void SetTrait(TraitData _traitData, int _traitLevel = 0)
  {
    this.traitLevel = _traitLevel;
    if (GameManager.Instance.IsObeliskChallenge())
    {
      if (GameManager.Instance.IsWeeklyChallenge())
      {
        this.traitLevel = 2;
      }
      else
      {
        int obeliskMadness = AtOManager.Instance.GetObeliskMadness();
        if (obeliskMadness >= 5)
          this.traitLevel = obeliskMadness > 8 ? 2 : 1;
      }
    }
    this.traitData = Globals.Instance.GetTraitData(_traitData.Id);
    if (!((Object) this.traitData != (Object) null))
      return;
    StringBuilder stringBuilder = new StringBuilder();
    if (!this.enabled && !this.active)
      stringBuilder.Append("<color=#666666>");
    stringBuilder.Append("<size=+.25><color=");
    if (this.enabled || this.active)
      stringBuilder.Append(this.heroColor);
    else if (!this.active)
      stringBuilder.Append("#999");
    stringBuilder.Append(">");
    if ((Object) this.traitData.TraitCard == (Object) null)
      stringBuilder.Append(this.traitData.TraitName);
    else
      stringBuilder.Append(Globals.Instance.GetCardData(this.traitData.TraitCard.Id, false).CardName);
    stringBuilder.Append("</color></size>\n");
    if ((Object) this.traitData.TraitCard == (Object) null)
      stringBuilder.Append(this.traitData.Description);
    else
      stringBuilder.Append(string.Format(Texts.Instance.GetText("traitAddCard"), (object) Globals.Instance.GetCardData(this.traitData.TraitCard.Id, false).CardName));
    if (!this.enabled && !this.active)
      stringBuilder.Append("</color>");
    this.boxText.text = stringBuilder.ToString();
    if (!this.active)
      return;
    this.boxText.color = Functions.HexToColor("#D4AC5B");
  }

  public void SetEnable(bool _state)
  {
    this.boxSpr.color = !_state ? new Color(0.5f, 0.5f, 0.5f, 0.4f) : Functions.HexToColor(this.heroColor);
    this.enabled = _state;
  }

  public void SetActive(bool _state)
  {
    this.active = _state;
    if (this.active)
    {
      if ((Object) this.anim != (Object) null)
        this.anim.enabled = true;
      this.boxSpr.color = Functions.HexToColor("#FFCC00");
      this.heroColor = "#FFCC00";
    }
    else
    {
      if (!((Object) this.anim != (Object) null))
        return;
      this.anim.enabled = false;
    }
  }

  private void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive())
      return;
    if ((Object) this.traitData != (Object) null)
    {
      if ((Object) this.traitData.TraitCard == (Object) null && (Object) this.traitData.TraitCardForAllHeroes == (Object) null)
      {
        PopupManager.Instance.SetTrait(this.traitData, false);
        if ((Object) this.CI != (Object) null)
        {
          this.CI.CardData = (CardData) null;
          this.CI.enabled = false;
        }
      }
      else
      {
        if ((Object) this.traitData.TraitCard != (Object) null)
        {
          string id = this.traitData.TraitCard.Id;
          if (this.traitLevel == 0)
            this.CI.CreateAmplifyOutsideCard(Globals.Instance.GetCardData(id), this.GetComponent<BoxCollider2D>());
          else if (this.traitLevel == 1)
            this.CI.CreateAmplifyOutsideCard(Globals.Instance.GetCardData(Globals.Instance.GetCardData(id, false).UpgradesTo1), this.GetComponent<BoxCollider2D>());
          else if (this.traitLevel == 2)
            this.CI.CreateAmplifyOutsideCard(Globals.Instance.GetCardData(Globals.Instance.GetCardData(id, false).UpgradesTo2), this.GetComponent<BoxCollider2D>());
        }
        else if ((Object) this.traitData.TraitCardForAllHeroes != (Object) null)
          this.CI.CreateAmplifyOutsideCard(Globals.Instance.GetCardData(this.traitData.TraitCardForAllHeroes.Id), this.GetComponent<BoxCollider2D>());
        if ((Object) this.CI != (Object) null)
          this.CI.enabled = true;
      }
    }
    if (!this.active && !this.enabled)
    {
      this.boxSpr.color = new Color(0.8f, 0.8f, 0.8f, 0.8f);
    }
    else
    {
      if (!this.active)
        return;
      this.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
      GameManager.Instance.PlayLibraryAudio("ui_menu_popup_01");
    }
  }

  private void OnMouseExit()
  {
    GameManager.Instance.CleanTempContainer();
    PopupManager.Instance.ClosePopup();
    if (!this.active && !this.enabled)
    {
      this.boxSpr.color = new Color(0.5f, 0.5f, 0.5f, 0.4f);
    }
    else
    {
      if (!this.active)
        return;
      this.transform.localScale = new Vector3(1f, 1f, 1f);
    }
  }

  public void OnMouseUp()
  {
    if (!this.active || !Functions.ClickedThisTransform(this.transform) || AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive())
      return;
    if ((bool) (Object) TownManager.Instance || (bool) (Object) MapManager.Instance)
    {
      if (!GameManager.Instance.IsMultiplayer())
        AtOManager.Instance.HeroLevelUp(this.heroIndex, this.traitData.Id);
      else
        AtOManager.Instance.HeroLevelUpMP(this.heroIndex, this.traitData.Id);
    }
    else
      AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("cantLevelUp"));
    this.transform.localScale = new Vector3(1f, 1f, 1f);
    PopupManager.Instance.ClosePopup();
  }
}
