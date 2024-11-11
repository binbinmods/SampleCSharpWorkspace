// Decompiled with JetBrains decompiler
// Type: TownBuilding
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class TownBuilding : MonoBehaviour
{
  public Sprite[] bgShaderLevel;
  public Sprite[] imgOverLevel;
  public Transform[] specialLevel;
  public SpriteRenderer bgShader;
  public SpriteRenderer bgPlain;
  public Transform imgOver;
  private SpriteRenderer imgOverSprite;
  private SpriteRenderer imgBaseSprite;
  public string idTitle;
  public string idDescription;
  private Color colorOri;
  private Coroutine shadowCo;

  private void Awake()
  {
    this.imgOver.gameObject.SetActive(false);
    this.imgOverSprite = this.imgOver.GetComponent<SpriteRenderer>();
    this.imgBaseSprite = this.GetComponent<SpriteRenderer>();
    this.HideShadow(true);
  }

  public void Init(int level)
  {
    this.bgShader.sprite = this.bgPlain.sprite = this.bgShaderLevel[level];
    this.imgOverSprite.sprite = this.imgBaseSprite.sprite = this.imgOverLevel[level];
    if (this.specialLevel != null && level < this.specialLevel.Length && (Object) this.specialLevel[level] != (Object) null && !this.specialLevel[level].gameObject.activeSelf)
      this.specialLevel[level].gameObject.SetActive(true);
    this.colorOri = new Color(0.0f, 0.68f, 1f, 1f);
    this.StartCoroutine(this.UpdateShapeToSprite());
  }

  private void Start()
  {
  }

  private void HideShadow(bool instant)
  {
    if (instant)
    {
      SpriteRenderer bgShader = this.bgShader;
      SpriteRenderer bgPlain = this.bgPlain;
      Color color1 = new Color(this.colorOri.r, this.colorOri.g, this.colorOri.b, 0.0f);
      Color color2 = color1;
      bgPlain.color = color2;
      Color color3 = color1;
      bgShader.color = color3;
      this.bgShader.gameObject.SetActive(false);
      this.bgPlain.gameObject.SetActive(false);
    }
    else
    {
      if (this.shadowCo != null)
        this.StopCoroutine(this.shadowCo);
      this.shadowCo = this.StartCoroutine(this.AnimationShadow(0));
    }
  }

  private void ShowShadow()
  {
    if (this.shadowCo != null)
      this.StopCoroutine(this.shadowCo);
    this.shadowCo = this.StartCoroutine(this.AnimationShadow(1));
  }

  private IEnumerator AnimationShadow(int direction)
  {
    float currentAlpha = this.bgShader.color.a;
    if (direction == 0)
    {
      while ((double) currentAlpha > 0.0)
      {
        currentAlpha -= 0.035f;
        Color color1 = new Color(this.colorOri.r, this.colorOri.g, this.colorOri.b, currentAlpha);
        Color color2 = new Color(this.colorOri.r, this.colorOri.g, this.colorOri.b, color1.a * 2f);
        this.bgShader.color = color1;
        this.bgPlain.color = color2;
        yield return (object) null;
      }
      this.bgShader.gameObject.SetActive(false);
      this.bgPlain.gameObject.SetActive(false);
    }
    else
    {
      this.bgShader.gameObject.SetActive(true);
      this.bgPlain.gameObject.SetActive(true);
      while ((double) currentAlpha < 0.3)
      {
        currentAlpha += 0.035f;
        Color color3 = new Color(this.colorOri.r, this.colorOri.g, this.colorOri.b, currentAlpha);
        Color color4 = new Color(this.colorOri.r, this.colorOri.g, this.colorOri.b, color3.a * 2f);
        this.bgShader.color = color3;
        this.bgPlain.color = color4;
        yield return (object) null;
      }
    }
  }

  private void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive())
      return;
    this.ShowShadow();
    this.imgOver.gameObject.SetActive(true);
    GameManager.Instance.PlayLibraryAudio("ui_click");
    PopupManager.Instance.SetTown(this.idTitle, this.idDescription);
    GameManager.Instance.SetCursorHover();
  }

  private void OnMouseExit() => this.fHide();

  private void OnMouseUp()
  {
    if (!Functions.ClickedThisTransform(this.transform) || AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || TownManager.Instance.AreTreasuresLocked())
      return;
    bool flag = false;
    if (AtOManager.Instance.TownTutorialStep > -1 && AtOManager.Instance.TownTutorialStep < 3)
      flag = true;
    if (this.idTitle == "craftCards")
    {
      if (flag && AtOManager.Instance.TownTutorialStep != 0)
      {
        AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("tutorialTownNeedComplete"));
        return;
      }
      AtOManager.Instance.DoCardCraft();
    }
    else if (this.idTitle == "upgradeCards")
    {
      if (flag && AtOManager.Instance.TownTutorialStep != 1)
      {
        AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("tutorialTownNeedComplete"));
        return;
      }
      AtOManager.Instance.DoCardUpgrade();
    }
    else if (this.idTitle == "removeCards")
    {
      if (flag)
      {
        AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("tutorialTownNeedComplete"));
        return;
      }
      AtOManager.Instance.DoCardHealer();
    }
    else if (this.idTitle == "divinationCards")
    {
      if (flag)
      {
        AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("tutorialTownNeedComplete"));
        return;
      }
      AtOManager.Instance.DoCardDivination();
    }
    else if (this.idTitle == "buyItems")
    {
      if (flag && AtOManager.Instance.TownTutorialStep != 2)
      {
        AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("tutorialTownNeedComplete"));
        return;
      }
      AtOManager.Instance.DoItemShop("");
    }
    this.fHide();
  }

  private void fHide()
  {
    this.HideShadow(false);
    this.imgOver.gameObject.SetActive(false);
    PopupManager.Instance.ClosePopup();
    GameManager.Instance.SetCursorPlain();
  }

  public IEnumerator UpdateShapeToSprite()
  {
    TownBuilding townBuilding = this;
    if ((Object) townBuilding.gameObject.GetComponent<PolygonCollider2D>() != (Object) null)
      Object.Destroy((Object) townBuilding.gameObject.GetComponent<PolygonCollider2D>());
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    townBuilding.gameObject.AddComponent<PolygonCollider2D>();
  }
}
