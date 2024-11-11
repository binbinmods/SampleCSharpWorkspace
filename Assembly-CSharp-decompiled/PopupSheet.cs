// Decompiled with JetBrains decompiler
// Type: PopupSheet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class PopupSheet : MonoBehaviour
{
  public GameObject popupPrefab;
  private GameObject GO_Popup;
  private Transform popupT;
  private Coroutine coroutine;
  public bool activated;
  private Vector3 destinationPosition;
  public TMP_Text _name;
  public TMP_Text _class;
  public TMP_Text _life;
  public TMP_Text _cards;
  public TMP_Text _energy;
  public TMP_Text _speed;
  public TMP_Text _resistSlashing;
  public TMP_Text _resistBlunt;
  public TMP_Text _resistPiercing;
  public TMP_Text _resistFire;
  public TMP_Text _resistCold;
  public TMP_Text _resistLightning;
  public TMP_Text _resistMind;
  public TMP_Text _resistHoly;
  public TMP_Text _resistShadow;
  private string sHealth = "<size=2.2><color=#FFF>{0}</color></size>\n{1}";
  private string sStats = "<size=2.4>{0}</size>\n{1}";
  private string sResists = "<size=1.4><color=#FFF>{0}</color></size>\n{1}";
  private string colorRed = "#C34738";
  private string colorGreen = "#2FBA23";

  private void Awake()
  {
    this.GO_Popup = Object.Instantiate<GameObject>(this.popupPrefab, Vector3.zero, Quaternion.identity, this.transform);
    this.popupT = this.GO_Popup.transform;
    this._name = this.popupT.Find("Personal/Name").GetComponent<TMP_Text>();
    this._class = this.popupT.Find("Personal/Class").GetComponent<TMP_Text>();
    this._life = this.popupT.Find("Personal/HealthV").GetComponent<TMP_Text>();
    this._cards = this.popupT.Find("Personal/CardV").GetComponent<TMP_Text>();
    this._energy = this.popupT.Find("Personal/EnergyV").GetComponent<TMP_Text>();
    this._speed = this.popupT.Find("Personal/SpeedV").GetComponent<TMP_Text>();
    this._resistSlashing = this.popupT.Find("Resists/Slashing").GetComponent<TMP_Text>();
    this._resistBlunt = this.popupT.Find("Resists/Blunt").GetComponent<TMP_Text>();
    this._resistPiercing = this.popupT.Find("Resists/Piercing").GetComponent<TMP_Text>();
    this._resistFire = this.popupT.Find("Resists/Fire").GetComponent<TMP_Text>();
    this._resistCold = this.popupT.Find("Resists/Cold").GetComponent<TMP_Text>();
    this._resistLightning = this.popupT.Find("Resists/Lightning").GetComponent<TMP_Text>();
    this._resistMind = this.popupT.Find("Resists/Mind").GetComponent<TMP_Text>();
    this._resistHoly = this.popupT.Find("Resists/Holy").GetComponent<TMP_Text>();
    this._resistShadow = this.popupT.Find("Resists/Shadow").GetComponent<TMP_Text>();
  }

  private void Start() => this.ClosePopup();

  public void ShowPopup(Character _character) => this.coroutine = this.StartCoroutine(this.ShowPopupCo(_character));

  private IEnumerator ShowPopupCo(Character _character)
  {
    this.SetPopup(_character);
    this.popupT.localPosition = Vector3.zero;
    this.GO_Popup.SetActive(true);
    yield return (object) null;
  }

  private void SetPopup(Character _character)
  {
    this._name.text = _character.GameName;
    this._class.text = _character.SubclassName;
    this._life.text = string.Format(this.sHealth, (object) _character.HpCurrent, (object) _character.Hp);
    this._cards.text = string.Format(this.sStats, (object) this.FormatColorBig("", _character.GetDrawCardsTurn(), _character.GetAuraDrawModifiers()), (object) this.FormatColorSmall("", _character.GetAuraDrawModifiers()));
    this._energy.text = string.Format(this.sStats, (object) this.FormatColorBig("", _character.GetEnergy(), _character.GetAuraStatModifiers(_character.GetEnergyTurn(), Enums.CharacterStat.Energy)), (object) this.FormatColorSmall("", _character.GetEnergyTurn()));
    int[] speed = _character.GetSpeed();
    this._speed.text = string.Format(this.sStats, (object) this.FormatColorBig("", speed[0], speed[2]), (object) this.FormatColorSmall("", speed[2]));
    this._resistSlashing.text = string.Format(this.sResists, (object) this.FormatColorBig("percent", _character.BonusResists(Enums.DamageType.Slashing), _character.GetAuraResistModifiers(Enums.DamageType.Slashing)), (object) this.FormatColorSmall("percent", _character.GetAuraResistModifiers(Enums.DamageType.Slashing)));
    this._resistBlunt.text = string.Format(this.sResists, (object) this.FormatColorBig("percent", _character.BonusResists(Enums.DamageType.Blunt), _character.GetAuraResistModifiers(Enums.DamageType.Blunt)), (object) this.FormatColorSmall("percent", _character.GetAuraResistModifiers(Enums.DamageType.Blunt)));
    this._resistPiercing.text = string.Format(this.sResists, (object) this.FormatColorBig("percent", _character.BonusResists(Enums.DamageType.Piercing), _character.GetAuraResistModifiers(Enums.DamageType.Piercing)), (object) this.FormatColorSmall("percent", _character.GetAuraResistModifiers(Enums.DamageType.Piercing)));
    this._resistFire.text = string.Format(this.sResists, (object) this.FormatColorBig("percent", _character.BonusResists(Enums.DamageType.Fire), _character.GetAuraResistModifiers(Enums.DamageType.Fire)), (object) this.FormatColorSmall("percent", _character.GetAuraResistModifiers(Enums.DamageType.Fire)));
    this._resistCold.text = string.Format(this.sResists, (object) this.FormatColorBig("percent", _character.BonusResists(Enums.DamageType.Cold), _character.GetAuraResistModifiers(Enums.DamageType.Cold)), (object) this.FormatColorSmall("percent", _character.GetAuraResistModifiers(Enums.DamageType.Cold)));
    this._resistLightning.text = string.Format(this.sResists, (object) this.FormatColorBig("percent", _character.BonusResists(Enums.DamageType.Lightning), _character.GetAuraResistModifiers(Enums.DamageType.Lightning)), (object) this.FormatColorSmall("percent", _character.GetAuraResistModifiers(Enums.DamageType.Lightning)));
    this._resistMind.text = string.Format(this.sResists, (object) this.FormatColorBig("percent", _character.BonusResists(Enums.DamageType.Mind), _character.GetAuraResistModifiers(Enums.DamageType.Mind)), (object) this.FormatColorSmall("percent", _character.GetAuraResistModifiers(Enums.DamageType.Mind)));
    this._resistHoly.text = string.Format(this.sResists, (object) this.FormatColorBig("percent", _character.BonusResists(Enums.DamageType.Holy), _character.GetAuraResistModifiers(Enums.DamageType.Holy)), (object) this.FormatColorSmall("percent", _character.GetAuraResistModifiers(Enums.DamageType.Holy)));
    this._resistShadow.text = string.Format(this.sResists, (object) this.FormatColorBig("percent", _character.BonusResists(Enums.DamageType.Shadow), _character.GetAuraResistModifiers(Enums.DamageType.Shadow)), (object) this.FormatColorSmall("percent", _character.GetAuraResistModifiers(Enums.DamageType.Shadow)));
  }

  private string FormatColorBig(string type, int value, int mod)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<color=");
    if (mod > 0)
      stringBuilder.Append(this.colorGreen);
    else if (mod < 0)
      stringBuilder.Append(this.colorRed);
    else
      stringBuilder.Append("#FFF");
    stringBuilder.Append(">");
    stringBuilder.Append(value);
    if (type == "percent")
      stringBuilder.Append("%");
    stringBuilder.Append("</color>");
    return stringBuilder.ToString();
  }

  private string FormatColorSmall(string type, int value)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (value > 0)
      stringBuilder.Append("+");
    else if (value == 0)
    {
      stringBuilder.Append("--");
      return stringBuilder.ToString();
    }
    stringBuilder.Append(value);
    if (type == "percent")
      stringBuilder.Append("%");
    return stringBuilder.ToString();
  }

  public void ClosePopup()
  {
    if (this.coroutine != null)
      this.StopCoroutine(this.coroutine);
    this.GO_Popup.SetActive(false);
    this.activated = false;
  }

  private Vector3 Position()
  {
    Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector3 vector3_1 = new Vector3(3f, 1f, 10f);
    Vector3 vector3_2 = new Vector3(-1.5f, 1f, 10f);
    return (double) worldPoint.x < 5.1999998092651367 ? worldPoint + vector3_1 : worldPoint + vector3_2;
  }
}
