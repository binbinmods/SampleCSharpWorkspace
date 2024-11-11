// Decompiled with JetBrains decompiler
// Type: TomeButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class TomeButton : MonoBehaviour
{
  public int tomeClass;
  private GameObject button;
  private TMP_Text buttonTxt;
  private Transform border;
  private SpriteRenderer borderSpr;
  private Transform background;
  private SpriteRenderer backgroundSpr;
  private Color colorActive;
  private Color colorDefault = new Color(0.8f, 0.8f, 0.8f, 0.8f);
  private bool active;
  private Vector3 oriPosition;
  private float textSizeOri;

  private void Awake()
  {
    this.background = this.transform.GetChild(0).transform;
    this.backgroundSpr = this.background.GetComponent<SpriteRenderer>();
    this.border = this.background.GetChild(0).transform;
    this.borderSpr = this.border.GetComponent<SpriteRenderer>();
    this.colorActive = Functions.HexToColor("#DD5F07");
    this.buttonTxt = this.transform.GetChild(1).GetComponent<TMP_Text>();
    this.textSizeOri = this.buttonTxt.fontSize;
  }

  public void Init()
  {
  }

  private void Start()
  {
    if (this.tomeClass == -1)
    {
      this.buttonTxt.text = Texts.Instance.GetText("allcards");
      this.backgroundSpr.color = Functions.HexToColor("#FFCC00");
    }
    else if (this.tomeClass == 0)
    {
      this.buttonTxt.text = "<sprite name=slashing>";
      this.backgroundSpr.color = Functions.HexToColor(Globals.Instance.ClassColor["warrior"]);
    }
    else if (this.tomeClass == 1)
    {
      this.buttonTxt.text = "<sprite name=fire>";
      this.backgroundSpr.color = Functions.HexToColor(Globals.Instance.ClassColor["mage"]);
    }
    else if (this.tomeClass == 2)
    {
      this.buttonTxt.text = "<sprite name=heal>";
      this.backgroundSpr.color = Functions.HexToColor(Globals.Instance.ClassColor["healer"]);
    }
    else if (this.tomeClass == 3)
    {
      this.buttonTxt.text = "<sprite name=piercing>";
      this.backgroundSpr.color = Functions.HexToColor(Globals.Instance.ClassColor["scout"]);
    }
    else if (this.tomeClass == 4)
    {
      this.buttonTxt.text = "<sprite name=slash>";
      this.backgroundSpr.color = Functions.HexToColor(Globals.Instance.ClassColor["magicknight"]);
    }
    else if (this.tomeClass == 5)
    {
      this.buttonTxt.text = "<sprite name=bless>";
      this.backgroundSpr.color = Functions.HexToColor(Globals.Instance.ClassColor["boon"]);
    }
    else if (this.tomeClass == 6)
    {
      this.buttonTxt.text = "<sprite name=bleed>";
      this.backgroundSpr.color = Functions.HexToColor(Globals.Instance.ClassColor["injury"]);
    }
    else if (this.tomeClass == 7)
    {
      this.buttonTxt.text = "<sprite name=weapon>";
      this.backgroundSpr.color = Functions.HexToColor(Globals.Instance.ClassColor["item"]);
    }
    else if (this.tomeClass == 8)
    {
      this.buttonTxt.text = "<sprite name=armor>";
      this.backgroundSpr.color = Functions.HexToColor(Globals.Instance.ClassColor["item"]);
    }
    else if (this.tomeClass == 9)
    {
      this.buttonTxt.text = "<sprite name=jewelry>";
      this.backgroundSpr.color = Functions.HexToColor(Globals.Instance.ClassColor["item"]);
    }
    else if (this.tomeClass == 10)
    {
      this.buttonTxt.text = "<sprite name=accesory>";
      this.backgroundSpr.color = Functions.HexToColor(Globals.Instance.ClassColor["item"]);
    }
    else if (this.tomeClass == 11)
    {
      this.buttonTxt.text = "<sprite name=pet>";
      this.backgroundSpr.color = Functions.HexToColor(Globals.Instance.ClassColor["pet"]);
    }
    else if (this.tomeClass == 14)
      this.buttonTxt.text = Texts.Instance.GetText("global");
    else if (this.tomeClass == 15)
      this.buttonTxt.text = Texts.Instance.GetText("friends");
    else if (this.tomeClass == 16)
    {
      this.buttonTxt.text = "<sprite name=pathMap>";
      this.backgroundSpr.color = Functions.HexToColor("#FF9800");
    }
    else
    {
      if (this.tomeClass == 17 || this.tomeClass == 18 || this.tomeClass == 19 || this.tomeClass == 20)
        return;
      if (this.tomeClass == 21)
        this.buttonTxt.text = Texts.Instance.GetText("combatStats");
      else if (this.tomeClass == 22)
      {
        this.buttonTxt.text = "<sprite name=experience>";
        this.backgroundSpr.color = Functions.HexToColor("#FF9B00");
      }
      else
      {
        if (this.tomeClass != 23)
          return;
        this.buttonTxt.text = Texts.Instance.GetText("index");
        this.backgroundSpr.color = Functions.HexToColor("#76736F");
      }
    }
  }

  public void SetText(string _text) => this.buttonTxt.text = _text;

  public void SetColor(string _colorHex) => this.backgroundSpr.color = Functions.HexToColor(_colorHex);

  public void Activate()
  {
    Vector3 oriPosition = this.oriPosition;
    if (this.oriPosition == Vector3.zero)
      this.oriPosition = this.transform.localPosition;
    this.transform.localPosition = this.oriPosition + new Vector3(0.0f, 0.1f, 0.0f);
    this.active = true;
    if ((Object) this.borderSpr != (Object) null)
      this.borderSpr.color = this.colorActive;
    if ((Object) this.border != (Object) null)
      this.border.gameObject.SetActive(true);
    if (!(bool) (Object) this.buttonTxt)
      return;
    this.buttonTxt.transform.localPosition = new Vector3(this.buttonTxt.transform.localPosition.x, 0.04f, this.buttonTxt.transform.localPosition.z);
    this.buttonTxt.fontSize = this.textSizeOri + 0.5f;
  }

  public void Deactivate()
  {
    Vector3 oriPosition = this.oriPosition;
    if (this.oriPosition == Vector3.zero)
      this.oriPosition = this.transform.localPosition;
    this.transform.localPosition = this.oriPosition;
    this.active = false;
    if ((Object) this.borderSpr != (Object) null)
      this.borderSpr.color = this.colorDefault;
    if ((Object) this.border != (Object) null)
      this.border.gameObject.SetActive(false);
    if (!(bool) (Object) this.buttonTxt)
      return;
    this.buttonTxt.transform.localPosition = new Vector3(this.buttonTxt.transform.localPosition.x, 0.08f, this.buttonTxt.transform.localPosition.z);
    this.buttonTxt.fontSize = this.textSizeOri;
  }

  public void OnMouseUp()
  {
    if (!Functions.ClickedThisTransform(this.transform))
      return;
    if (this.tomeClass == 14 || this.tomeClass == 15)
      TomeManager.Instance.SelectTomeScores(this.tomeClass);
    else if (this.tomeClass >= 16 && this.tomeClass <= 20)
      TomeManager.Instance.RunDetailButton(this.tomeClass - 16);
    else if (this.tomeClass == 21)
      TomeManager.Instance.RunCombatStats();
    else if (this.tomeClass == 23)
      TomeManager.Instance.SetPage(0);
    else
      TomeManager.Instance.SelectTomeCards(this.tomeClass);
  }

  private void OnMouseExit()
  {
    if (!this.active && this.border.gameObject.activeSelf)
      this.border.gameObject.SetActive(false);
    GameManager.Instance.SetCursorPlain();
  }

  private void OnMouseEnter()
  {
    if (this.active)
      return;
    this.borderSpr.color = this.colorDefault;
    if (!this.border.gameObject.activeSelf)
      this.border.gameObject.SetActive(true);
    GameManager.Instance.SetCursorHover();
  }
}
