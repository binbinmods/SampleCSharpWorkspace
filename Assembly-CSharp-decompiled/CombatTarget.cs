// Decompiled with JetBrains decompiler
// Type: CombatTarget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class CombatTarget : MonoBehaviour
{
  public Transform elements;
  public SpriteRenderer image;
  public TMP_Text name;
  public Transform bg;
  public Transform cards;
  public Transform cardsDeckT;
  public Transform cardsDiscardT;
  public TMP_Text cardsDeck;
  public TMP_Text cardsDiscard;
  public TMP_Text r0;
  public TMP_Text r1;
  public TMP_Text r2;
  public TMP_Text imm;
  private Character characterActive;
  private Character characterInStats;
  private string colorRed = "#FD7A76";
  private string colorGreen = "#50D75A";
  public GameObject GO_Buffs;
  public GameObject BuffPrefab;
  private Dictionary<string, List<string>> dictImmunityByItems = new Dictionary<string, List<string>>();
  private Vector3 bgSourcePosition;
  private Vector2 bgSourceSize;
  private int auraInTargetBox;
  private List<string> immuneList;

  private void Awake()
  {
    this.immuneList = new List<string>();
    this.bgSourcePosition = this.bg.localPosition;
    this.bgSourceSize = this.bg.GetComponent<SpriteRenderer>().size;
    this.imm.text = "";
    this.ShowElements(false);
  }

  public void SetTarget(Character character)
  {
    this.characterActive = character;
    this.ShowTarget();
  }

  public void SetTargetTMP(Character character) => this.ShowTarget(character);

  private void ShowElements(bool state)
  {
    if (!state)
    {
      this.elements.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -100f);
      if (!(bool) (Object) MatchManager.Instance)
        return;
      MatchManager.Instance.ShowTraitInfo(true);
    }
    else
    {
      this.elements.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0.0f);
      if (!(bool) (Object) MatchManager.Instance)
        return;
      MatchManager.Instance.ShowTraitInfo(false);
    }
  }

  public void ClearTarget() => this.ShowElements(false);

  private void ShowTarget(Character character = null)
  {
    if (character == null)
      character = this.characterActive;
    if (character == null)
      return;
    this.ShowElements(true);
    this.name.text = character.SourceName;
    this.image.sprite = character.SpriteSpeed;
    this.DoStats(character);
    this.DoCards(character);
    this.DoBuffs(character);
    this.ResizeBox();
  }

  private void ResizeBox()
  {
    float num1 = (float) this.auraInTargetBox * 0.85f;
    float num2 = 0.0f;
    if (this.immuneList.Count > 0)
      num2 = 0.7f;
    this.bg.GetComponent<SpriteRenderer>().size = new Vector2(this.bgSourceSize.x + num1, this.bgSourceSize.y + num2);
    this.bg.localPosition = this.bgSourcePosition + new Vector3(num1 * 0.5f * this.bg.transform.localScale.x, (float) (-(double) num2 * 0.5) * this.bg.transform.localScale.y, 0.0f);
  }

  private void DoStats(Character character)
  {
    if (character == null)
      return;
    string id = character.Id;
    this.characterInStats = character;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<size=2><sprite name=resist_slash></size> ");
    stringBuilder.Append(this.FormatColor(character.BonusResists(Enums.DamageType.Slashing), character.GetAuraResistModifiers(Enums.DamageType.Slashing)));
    stringBuilder.Append("\n");
    stringBuilder.Append("<size=2><sprite name=resist_fire></size> ");
    stringBuilder.Append(this.FormatColor(character.BonusResists(Enums.DamageType.Fire), character.GetAuraResistModifiers(Enums.DamageType.Fire)));
    stringBuilder.Append("\n");
    stringBuilder.Append("<size=2><sprite name=resist_holy></size> ");
    stringBuilder.Append(this.FormatColor(character.BonusResists(Enums.DamageType.Holy), character.GetAuraResistModifiers(Enums.DamageType.Holy)));
    stringBuilder.Append("\n");
    this.r0.text = stringBuilder.ToString();
    stringBuilder.Clear();
    stringBuilder.Append("<size=2><sprite name=resist_blunt></size> ");
    stringBuilder.Append(this.FormatColor(character.BonusResists(Enums.DamageType.Blunt), character.GetAuraResistModifiers(Enums.DamageType.Blunt)));
    stringBuilder.Append("\n");
    stringBuilder.Append("<size=2><sprite name=resist_cold></size> ");
    stringBuilder.Append(this.FormatColor(character.BonusResists(Enums.DamageType.Cold), character.GetAuraResistModifiers(Enums.DamageType.Cold)));
    stringBuilder.Append("\n");
    stringBuilder.Append("<size=2><sprite name=resist_shadow></size> ");
    stringBuilder.Append(this.FormatColor(character.BonusResists(Enums.DamageType.Shadow), character.GetAuraResistModifiers(Enums.DamageType.Shadow)));
    stringBuilder.Append("\n");
    this.r1.text = stringBuilder.ToString();
    stringBuilder.Clear();
    stringBuilder.Append("<size=2><sprite name=resist_piercing></size> ");
    stringBuilder.Append(this.FormatColor(character.BonusResists(Enums.DamageType.Piercing), character.GetAuraResistModifiers(Enums.DamageType.Piercing)));
    stringBuilder.Append("\n");
    stringBuilder.Append("<size=2><sprite name=resist_lightning></size> ");
    stringBuilder.Append(this.FormatColor(character.BonusResists(Enums.DamageType.Lightning), character.GetAuraResistModifiers(Enums.DamageType.Lightning)));
    stringBuilder.Append("\n");
    stringBuilder.Append("<size=2><sprite name=resist_mind></size> ");
    stringBuilder.Append(this.FormatColor(character.BonusResists(Enums.DamageType.Mind), character.GetAuraResistModifiers(Enums.DamageType.Mind)));
    stringBuilder.Append("\n");
    this.r2.text = stringBuilder.ToString();
    stringBuilder.Clear();
    this.immuneList.Clear();
    this.dictImmunityByItems.Clear();
    if (character != null && character.AuracurseImmune.Count > 0)
    {
      for (int index = 0; index < character.AuracurseImmune.Count; ++index)
        this.immuneList.Add(character.AuracurseImmune[index]);
    }
    if (character != null && id != "" && !this.dictImmunityByItems.ContainsKey(id) && character.AuraCurseImmunitiesByItemsList() != null)
      this.dictImmunityByItems.Add(id, character.AuraCurseImmunitiesByItemsList());
    for (int index = 0; index < this.dictImmunityByItems[id].Count; ++index)
    {
      if (!this.immuneList.Contains(this.dictImmunityByItems[id][index]))
        this.immuneList.Add(this.dictImmunityByItems[id][index]);
    }
    if (this.immuneList.Count > 0)
    {
      stringBuilder.Append(Texts.Instance.GetText("immune"));
      stringBuilder.Append(":  <voffset=-.2><size=+.3>");
      for (int index = 0; index < this.immuneList.Count; ++index)
      {
        stringBuilder.Append("<sprite name=");
        stringBuilder.Append(this.immuneList[index]);
        stringBuilder.Append("> ");
      }
      stringBuilder.Append("</size>");
      this.imm.text = stringBuilder.ToString();
    }
    else
      this.imm.text = "";
  }

  public void DoCards(Character character)
  {
    if ((Object) character.HeroData != (Object) null)
    {
      this.cards.gameObject.SetActive(true);
      this.cardsDeck.text = MatchManager.Instance.CountHeroDeck(character.HeroIndex).ToString();
      this.cardsDiscard.text = MatchManager.Instance.CountHeroDiscard(character.HeroIndex).ToString();
      this.cardsDeckT.GetComponent<DeckInHero>().heroIndex = character.HeroIndex;
      this.cardsDiscardT.GetComponent<DeckInHero>().heroIndex = character.HeroIndex;
    }
    else
      this.cards.gameObject.SetActive(false);
  }

  public void Refresh()
  {
    if (!this.elements.gameObject.activeSelf || (double) this.elements.transform.position.z == -100.0 || this.characterInStats == null)
      return;
    this.DoStats(this.characterInStats);
    this.DoBuffs(this.characterInStats);
    this.ResizeBox();
  }

  public void RefreshCards()
  {
    if (this.characterInStats == null)
      return;
    this.DoCards(this.characterInStats);
  }

  private string FormatColor(int value, int mod)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (mod > 0)
    {
      stringBuilder.Append("<color=");
      stringBuilder.Append(this.colorGreen);
      stringBuilder.Append(">");
    }
    else if (mod < 0)
    {
      stringBuilder.Append("<color=");
      stringBuilder.Append(this.colorRed);
      stringBuilder.Append(">");
    }
    stringBuilder.Append(value);
    stringBuilder.Append("%");
    if (mod != 0)
      stringBuilder.Append("</color>");
    return stringBuilder.ToString();
  }

  public void DoBuffs(Character character)
  {
    Vector3 zero = Vector3.zero;
    foreach (Component component in this.GO_Buffs.transform)
      Object.Destroy((Object) component.gameObject);
    int count = character.AuraList.Count;
    this.auraInTargetBox = 0;
    for (int index = 0; index < count; ++index)
    {
      Aura aura = character.AuraList[index];
      if (aura != null && (Object) aura.ACData != (Object) null && (aura.ACData.Id == "energize" || aura.ACData.Id == "inspire" || aura.ACData.Id == "stress" || aura.ACData.Id == "fatigue"))
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.BuffPrefab, Vector3.zero, Quaternion.identity, this.GO_Buffs.transform);
        string id = character == null ? "" : character.Id;
        gameObject.GetComponent<Buff>().SetBuff(aura.ACData, aura.GetCharges(), _charId: id);
        gameObject.name = aura.ACData.ACName;
        ++this.auraInTargetBox;
      }
    }
  }

  public void OnMouseUp()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive() || this.characterInStats == null)
      return;
    GameManager.Instance.SetCursorPlain();
    MatchManager.Instance.ShowCharacterWindow("stats", this.characterInStats.IsHero, !this.characterInStats.IsHero ? this.characterInStats.NPCIndex : this.characterInStats.HeroIndex);
  }

  public void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive())
      return;
    GameManager.Instance.SetCursorHover();
  }

  public void OnMouseExit()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive())
      return;
    GameManager.Instance.SetCursorPlain();
  }
}
