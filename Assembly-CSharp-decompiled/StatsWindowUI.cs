// Decompiled with JetBrains decompiler
// Type: StatsWindowUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class StatsWindowUI : MonoBehaviour
{
  public GameObject BuffPrefab;
  public GameObject GO_Buffs;
  public GameObject GO_Immunities;
  public GameObject GO_AuraCurse;
  public Transform[] dmageTypeT;
  private TMP_Text[] damageTypeText;
  private TMP_Text[] resistanceText;
  private TMP_Text[] ddText;
  private TMP_Text[] dtText;
  private PopupText[] resistancePop;
  private PopupText[] damagedonePop;
  private PopupText[] damagetakenPop;
  public TMP_Text statsName;
  public TMP_Text statsHealth;
  public TMP_Text statsEnergy;
  public TMP_Text statsSpeed;
  public TMP_Text statsCards;
  public TMP_Text globalDamageDonePercent;
  public PopupText globalDamageDonePop;
  public TMP_Text globalHealingDonePercent;
  public PopupText globalHealingDonePercentPop;
  public TMP_Text globalHealingDoneFlat;
  public PopupText globalHealingDoneFlatPop;
  public TMP_Text globalHealingTakenPercent;
  public PopupText globalHealingTakenPercentPop;
  public TMP_Text globalHealingTakenFlat;
  public PopupText globalHealingTakenFlatPop;
  private string colorRed = "#C34738";
  private string colorGreen = "#2FBA23";
  public Transform notEffects;
  public Transform notImmune;
  public Transform yesImmune;
  public Transform notCharges;
  private Character character;

  private void Awake()
  {
    this.damageTypeText = new TMP_Text[this.dmageTypeT.Length];
    this.resistanceText = new TMP_Text[this.dmageTypeT.Length];
    this.resistancePop = new PopupText[this.dmageTypeT.Length];
    this.damagedonePop = new PopupText[this.dmageTypeT.Length];
    this.damagetakenPop = new PopupText[this.dmageTypeT.Length];
    this.ddText = new TMP_Text[this.dmageTypeT.Length];
    this.dtText = new TMP_Text[this.dmageTypeT.Length];
    for (int index = 0; index < this.dmageTypeT.Length; ++index)
    {
      this.damageTypeText[index] = this.dmageTypeT[index].GetChild(0).GetComponent<TMP_Text>();
      this.resistancePop[index] = this.dmageTypeT[index].GetChild(2).GetComponent<PopupText>();
      this.resistanceText[index] = this.dmageTypeT[index].GetChild(3).GetComponent<TMP_Text>();
      this.damagedonePop[index] = this.dmageTypeT[index].GetChild(4).GetComponent<PopupText>();
      this.ddText[index] = this.dmageTypeT[index].GetChild(5).GetComponent<TMP_Text>();
      this.damagetakenPop[index] = this.dmageTypeT[index].GetChild(6).GetComponent<PopupText>();
      this.dtText[index] = this.dmageTypeT[index].GetChild(7).GetComponent<TMP_Text>();
    }
  }

  private void Start()
  {
    this.damageTypeText[0].text = this.GetTextDT("slash");
    this.damageTypeText[1].text = this.GetTextDT("blunt");
    this.damageTypeText[2].text = this.GetTextDT("piercing");
    this.damageTypeText[3].text = this.GetTextDT("fire");
    this.damageTypeText[4].text = this.GetTextDT("cold");
    this.damageTypeText[5].text = this.GetTextDT("lightning");
    this.damageTypeText[6].text = this.GetTextDT("holy");
    this.damageTypeText[7].text = this.GetTextDT("shadow");
    this.damageTypeText[8].text = this.GetTextDT("mind");
  }

  public void DoStats(Character _character)
  {
    this.character = _character;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(_character.HpCurrent);
    stringBuilder.Append(" <color=#AAA><size=1.7>/");
    stringBuilder.Append(_character.Hp);
    stringBuilder.Append("</size></color>");
    this.statsHealth.text = stringBuilder.ToString();
    stringBuilder.Clear();
    int[] speed = _character.GetSpeed();
    stringBuilder.Append(speed[0].ToString());
    stringBuilder.Append(" <color=#AAA><size=1.7>/");
    stringBuilder.Append(speed[1].ToString());
    stringBuilder.Append("</size></color>");
    this.statsSpeed.text = stringBuilder.ToString();
    stringBuilder.Clear();
    if (_character.IsHero)
    {
      this.statsEnergy.transform.parent.gameObject.SetActive(true);
      this.statsCards.transform.parent.gameObject.SetActive(true);
      this.statsName.gameObject.SetActive(false);
      if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null)
        stringBuilder.Append(_character.EnergyCurrent);
      else
        stringBuilder.Append(_character.Energy);
      stringBuilder.Append(" <color=#AAA><size=1.7>");
      stringBuilder.Append(Texts.Instance.GetText("dataPerTurn").Replace("<%>", _character.EnergyTurn.ToString()));
      stringBuilder.Append("</size></color>");
      this.statsEnergy.text = stringBuilder.ToString();
      stringBuilder.Clear();
      stringBuilder.Append(_character.GetDrawCardsTurnForDisplayInDeck());
      stringBuilder.Append(" <color=#AAA><size=1.7>");
      stringBuilder.Append(Texts.Instance.GetText("dataPerTurn").Replace("<%>", ""));
      stringBuilder.Append("</size></color>");
      this.statsCards.text = stringBuilder.ToString();
    }
    else
    {
      this.statsName.gameObject.SetActive(true);
      this.statsName.text = _character.SourceName;
      this.statsEnergy.transform.parent.gameObject.SetActive(false);
      this.statsCards.transform.parent.gameObject.SetActive(false);
    }
    int percentModifiers = (int) _character.GetTraitDamagePercentModifiers(Enums.DamageType.All);
    int num1 = 0;
    Dictionary<string, int> percentDictionary1 = _character.GetItemDamageDonePercentDictionary(Enums.DamageType.All);
    foreach (KeyValuePair<string, int> keyValuePair in percentDictionary1)
      num1 += keyValuePair.Value;
    int mod = 0;
    Dictionary<string, int> percentDictionary2 = _character.GetAuraDamageDonePercentDictionary(Enums.DamageType.All);
    foreach (KeyValuePair<string, int> keyValuePair in percentDictionary2)
      mod += keyValuePair.Value;
    int num2 = percentModifiers + num1 + mod;
    if (num2 < -50)
      num2 = -50;
    this.globalDamageDonePercent.text = this.FormatPercent(num2, mod);
    this.DoPopupGeneral(percentDictionary1, percentDictionary2, this.globalDamageDonePop, showBase: true, valueBase: percentModifiers);
    float[] numArray1 = _character.HealBonus(0);
    int num3 = (int) numArray1[1];
    int healPercentBonus1 = (int) _character.GetTraitHealPercentBonus();
    int healPercentBonus2 = (int) _character.GetItemHealPercentBonus();
    this.globalHealingDonePercent.text = this.FormatPercent(num3 + (healPercentBonus1 + healPercentBonus2), (int) numArray1[1]);
    this.DoPopupGeneral(_character.GetItemHealPercentDictionary(), _character.GetAuraHealPercentDictionary(), this.globalHealingDonePercentPop, showBase: true, valueBase: healPercentBonus1 + healPercentBonus2);
    int num4 = (int) numArray1[0];
    int traitHealFlatBonus = _character.GetTraitHealFlatBonus();
    int itemHealFlatBonus = _character.GetItemHealFlatBonus();
    int num5 = 0;
    if (_character.IsHero)
      num5 = PlayerManager.Instance.GetPerkHealBonus(_character.HeroData.HeroSubClass.Id);
    this.globalHealingDoneFlat.text = this.FormatSum(num4 + (traitHealFlatBonus + itemHealFlatBonus + num5), (int) numArray1[0], false);
    this.DoPopupGeneral(_character.GetItemHealFlatDictionary(), _character.GetAuraHealFlatDictionary(), this.globalHealingDoneFlatPop, "flat", true, traitHealFlatBonus + itemHealFlatBonus);
    float[] numArray2 = _character.HealReceivedBonus();
    int num6 = (int) numArray2[1];
    int receivedPercentBonus1 = (int) _character.GetTraitHealReceivedPercentBonus();
    int receivedPercentBonus2 = (int) _character.GetItemHealReceivedPercentBonus();
    this.globalHealingTakenPercent.text = this.FormatPercent(num6 + (receivedPercentBonus1 + receivedPercentBonus2), (int) numArray2[1]);
    this.DoPopupGeneral((Dictionary<string, int>) null, _character.GetAuraHealReceivedPercentDictionary(), this.globalHealingTakenPercentPop, showBase: true, valueBase: receivedPercentBonus1);
    int num7 = (int) numArray2[0];
    int receivedFlatBonus1 = _character.GetTraitHealReceivedFlatBonus();
    int receivedFlatBonus2 = _character.GetItemHealReceivedFlatBonus();
    this.globalHealingTakenFlat.text = this.FormatSum(num7 + (receivedFlatBonus1 + receivedFlatBonus2), (int) numArray2[0]);
    this.DoPopupGeneral((Dictionary<string, int>) null, _character.GetAuraHealReceivedFlatDictionary(), this.globalHealingTakenFlatPop, "flat", true, receivedFlatBonus1);
    Enums.DamageType damageType1 = Enums.DamageType.Slashing;
    int resistanceIndex1 = 0;
    int num8 = _character.BonusResists(damageType1);
    int auraResistModifiers1 = _character.GetAuraResistModifiers(damageType1);
    int itemResistModifiers1 = _character.GetItemResistModifiers(damageType1);
    this.DoPopupResistance(resistanceIndex1, damageType1, _character.ResistSlashing, auraResistModifiers1, itemResistModifiers1);
    this.resistanceText[resistanceIndex1].text = this.FormatResistance(num8, auraResistModifiers1);
    int damageBase1 = _character.TotalDamageWithCharacterFlatBonus(damageType1);
    int damageBonu1 = (int) _character.DamageBonus(damageType1)[0];
    int damageFlatModifiers1 = _character.GetItemDamageFlatModifiers(damageType1);
    this.DoPopupDamageDone(resistanceIndex1, damageType1, damageBase1, damageBonu1, damageFlatModifiers1);
    this.ddText[resistanceIndex1].text = this.FormatSum(damageBase1, damageBonu1);
    int damageAura1 = _character.IncreasedCursedDamagePerStack(damageType1);
    this.DoPopupDamageTaken(resistanceIndex1, damageType1, damageAura1);
    this.dtText[resistanceIndex1].text = this.FormatTaken(damageAura1);
    Enums.DamageType damageType2 = Enums.DamageType.Blunt;
    int resistanceIndex2 = 1;
    int num9 = _character.BonusResists(damageType2);
    int auraResistModifiers2 = _character.GetAuraResistModifiers(damageType2);
    int itemResistModifiers2 = _character.GetItemResistModifiers(damageType2);
    this.DoPopupResistance(resistanceIndex2, damageType2, _character.ResistBlunt, auraResistModifiers2, itemResistModifiers2);
    this.resistanceText[resistanceIndex2].text = this.FormatResistance(num9, auraResistModifiers2);
    int damageBase2 = _character.TotalDamageWithCharacterFlatBonus(damageType2);
    int damageBonu2 = (int) _character.DamageBonus(damageType2)[0];
    int damageFlatModifiers2 = _character.GetItemDamageFlatModifiers(damageType2);
    this.DoPopupDamageDone(resistanceIndex2, damageType2, damageBase2, damageBonu2, damageFlatModifiers2);
    this.ddText[resistanceIndex2].text = this.FormatSum(damageBase2, damageBonu2);
    int damageAura2 = _character.IncreasedCursedDamagePerStack(damageType2);
    this.DoPopupDamageTaken(resistanceIndex2, damageType2, damageAura2);
    this.dtText[resistanceIndex2].text = this.FormatTaken(damageAura2);
    Enums.DamageType damageType3 = Enums.DamageType.Piercing;
    int resistanceIndex3 = 2;
    int num10 = _character.BonusResists(damageType3);
    int auraResistModifiers3 = _character.GetAuraResistModifiers(damageType3);
    int itemResistModifiers3 = _character.GetItemResistModifiers(damageType3);
    this.DoPopupResistance(resistanceIndex3, damageType3, _character.ResistPiercing, auraResistModifiers3, itemResistModifiers3);
    this.resistanceText[resistanceIndex3].text = this.FormatResistance(num10, auraResistModifiers3);
    int damageBase3 = _character.TotalDamageWithCharacterFlatBonus(damageType3);
    int damageBonu3 = (int) _character.DamageBonus(damageType3)[0];
    int damageFlatModifiers3 = _character.GetItemDamageFlatModifiers(damageType3);
    this.DoPopupDamageDone(resistanceIndex3, damageType3, damageBase3, damageBonu3, damageFlatModifiers3);
    this.ddText[resistanceIndex3].text = this.FormatSum(damageBase3, damageBonu3);
    int damageAura3 = _character.IncreasedCursedDamagePerStack(damageType3);
    this.DoPopupDamageTaken(resistanceIndex3, damageType3, damageAura3);
    this.dtText[resistanceIndex3].text = this.FormatTaken(damageAura3);
    Enums.DamageType damageType4 = Enums.DamageType.Fire;
    int resistanceIndex4 = 3;
    int num11 = _character.BonusResists(damageType4);
    int auraResistModifiers4 = _character.GetAuraResistModifiers(damageType4);
    int itemResistModifiers4 = _character.GetItemResistModifiers(damageType4);
    this.DoPopupResistance(resistanceIndex4, damageType4, _character.ResistFire, auraResistModifiers4, itemResistModifiers4);
    this.resistanceText[resistanceIndex4].text = this.FormatResistance(num11, auraResistModifiers4);
    int damageBase4 = _character.TotalDamageWithCharacterFlatBonus(damageType4);
    int damageBonu4 = (int) _character.DamageBonus(damageType4)[0];
    int damageFlatModifiers4 = _character.GetItemDamageFlatModifiers(damageType4);
    this.DoPopupDamageDone(resistanceIndex4, damageType4, damageBase4, damageBonu4, damageFlatModifiers4);
    this.ddText[resistanceIndex4].text = this.FormatSum(damageBase4, damageBonu4);
    int damageAura4 = _character.IncreasedCursedDamagePerStack(damageType4);
    this.DoPopupDamageTaken(resistanceIndex4, damageType4, damageAura4);
    this.dtText[resistanceIndex4].text = this.FormatTaken(damageAura4);
    Enums.DamageType damageType5 = Enums.DamageType.Cold;
    int resistanceIndex5 = 4;
    int num12 = _character.BonusResists(damageType5);
    int auraResistModifiers5 = _character.GetAuraResistModifiers(damageType5);
    int itemResistModifiers5 = _character.GetItemResistModifiers(damageType5);
    this.DoPopupResistance(resistanceIndex5, damageType5, _character.ResistCold, auraResistModifiers5, itemResistModifiers5);
    this.resistanceText[resistanceIndex5].text = this.FormatResistance(num12, auraResistModifiers5);
    int damageBase5 = _character.TotalDamageWithCharacterFlatBonus(damageType5);
    int damageBonu5 = (int) _character.DamageBonus(damageType5)[0];
    int damageFlatModifiers5 = _character.GetItemDamageFlatModifiers(damageType5);
    this.DoPopupDamageDone(resistanceIndex5, damageType5, damageBase5, damageBonu5, damageFlatModifiers5);
    this.ddText[resistanceIndex5].text = this.FormatSum(damageBase5, damageBonu5);
    int damageAura5 = _character.IncreasedCursedDamagePerStack(damageType5);
    this.DoPopupDamageTaken(resistanceIndex5, damageType5, damageAura5);
    this.dtText[resistanceIndex5].text = this.FormatTaken(damageAura5);
    Enums.DamageType damageType6 = Enums.DamageType.Lightning;
    int resistanceIndex6 = 5;
    int num13 = _character.BonusResists(damageType6);
    int auraResistModifiers6 = _character.GetAuraResistModifiers(damageType6);
    int itemResistModifiers6 = _character.GetItemResistModifiers(damageType6);
    this.DoPopupResistance(resistanceIndex6, damageType6, _character.ResistLightning, auraResistModifiers6, itemResistModifiers6);
    this.resistanceText[resistanceIndex6].text = this.FormatResistance(num13, auraResistModifiers6);
    int damageBase6 = _character.TotalDamageWithCharacterFlatBonus(damageType6);
    int damageBonu6 = (int) _character.DamageBonus(damageType6)[0];
    int damageFlatModifiers6 = _character.GetItemDamageFlatModifiers(damageType6);
    this.DoPopupDamageDone(resistanceIndex6, damageType6, damageBase6, damageBonu6, damageFlatModifiers6);
    this.ddText[resistanceIndex6].text = this.FormatSum(damageBase6, damageBonu6);
    int damageAura6 = _character.IncreasedCursedDamagePerStack(damageType6);
    this.DoPopupDamageTaken(resistanceIndex6, damageType6, damageAura6);
    this.dtText[resistanceIndex6].text = this.FormatTaken(damageAura6);
    Enums.DamageType damageType7 = Enums.DamageType.Holy;
    int resistanceIndex7 = 6;
    int num14 = _character.BonusResists(damageType7);
    int auraResistModifiers7 = _character.GetAuraResistModifiers(damageType7);
    int itemResistModifiers7 = _character.GetItemResistModifiers(damageType7);
    this.DoPopupResistance(resistanceIndex7, damageType7, _character.ResistHoly, auraResistModifiers7, itemResistModifiers7);
    this.resistanceText[resistanceIndex7].text = this.FormatResistance(num14, auraResistModifiers7);
    int damageBase7 = _character.TotalDamageWithCharacterFlatBonus(damageType7);
    int damageBonu7 = (int) _character.DamageBonus(damageType7)[0];
    int damageFlatModifiers7 = _character.GetItemDamageFlatModifiers(damageType7);
    this.DoPopupDamageDone(resistanceIndex7, damageType7, damageBase7, damageBonu7, damageFlatModifiers7);
    this.ddText[resistanceIndex7].text = this.FormatSum(damageBase7, damageBonu7);
    int damageAura7 = _character.IncreasedCursedDamagePerStack(damageType7);
    this.DoPopupDamageTaken(resistanceIndex7, damageType7, damageAura7);
    this.dtText[resistanceIndex7].text = this.FormatTaken(damageAura7);
    Enums.DamageType damageType8 = Enums.DamageType.Shadow;
    int resistanceIndex8 = 7;
    int num15 = _character.BonusResists(damageType8);
    int auraResistModifiers8 = _character.GetAuraResistModifiers(damageType8);
    int itemResistModifiers8 = _character.GetItemResistModifiers(damageType8);
    this.DoPopupResistance(resistanceIndex8, damageType8, _character.ResistShadow, auraResistModifiers8, itemResistModifiers8);
    this.resistanceText[resistanceIndex8].text = this.FormatResistance(num15, auraResistModifiers8);
    int damageBase8 = _character.TotalDamageWithCharacterFlatBonus(damageType8);
    int damageBonu8 = (int) _character.DamageBonus(damageType8)[0];
    int damageFlatModifiers8 = _character.GetItemDamageFlatModifiers(damageType8);
    this.DoPopupDamageDone(resistanceIndex8, damageType8, damageBase8, damageBonu8, damageFlatModifiers8);
    this.ddText[resistanceIndex8].text = this.FormatSum(damageBase8, damageBonu8);
    int damageAura8 = _character.IncreasedCursedDamagePerStack(damageType8);
    this.DoPopupDamageTaken(resistanceIndex8, damageType8, damageAura8);
    this.dtText[resistanceIndex8].text = this.FormatTaken(damageAura8);
    Enums.DamageType damageType9 = Enums.DamageType.Mind;
    int resistanceIndex9 = 8;
    int num16 = _character.BonusResists(damageType9);
    int auraResistModifiers9 = _character.GetAuraResistModifiers(damageType9);
    int itemResistModifiers9 = _character.GetItemResistModifiers(damageType9);
    this.DoPopupResistance(resistanceIndex9, damageType9, _character.ResistMind, auraResistModifiers9, itemResistModifiers9);
    this.resistanceText[resistanceIndex9].text = this.FormatResistance(num16, auraResistModifiers9);
    int damageBase9 = _character.TotalDamageWithCharacterFlatBonus(damageType9);
    int damageBonu9 = (int) _character.DamageBonus(damageType9)[0];
    int damageFlatModifiers9 = _character.GetItemDamageFlatModifiers(damageType9);
    this.DoPopupDamageDone(resistanceIndex9, damageType9, damageBase9, damageBonu9, damageFlatModifiers9);
    this.ddText[resistanceIndex9].text = this.FormatSum(damageBase9, damageBonu9);
    int damageAura9 = _character.IncreasedCursedDamagePerStack(damageType9);
    this.DoPopupDamageTaken(resistanceIndex9, damageType9, damageAura9);
    this.dtText[resistanceIndex9].text = this.FormatTaken(damageAura9);
    for (int index = 0; index < this.GO_Buffs.transform.childCount; ++index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.GO_Buffs.transform.GetChild(index).gameObject);
    int count = _character.AuraList.Count;
    if (count > 0)
    {
      this.notEffects.transform.gameObject.SetActive(false);
      for (int index = 0; index < count; ++index)
      {
        Aura aura = _character.AuraList[index];
        if (aura != null && aura.ACData.Id != "stealthbonus" && aura.ACData.Id != "furnace")
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BuffPrefab, Vector3.zero, Quaternion.identity, this.GO_Buffs.transform);
          gameObject.GetComponent<Buff>().SetBuff(aura.ACData, aura.GetCharges(), _charId: _character.Id);
          gameObject.GetComponent<Buff>().SetBuffInStats();
        }
      }
    }
    else
      this.notEffects.transform.gameObject.SetActive(true);
    for (int index = 0; index < this.GO_Immunities.transform.childCount; ++index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.GO_Immunities.transform.GetChild(index).gameObject);
    List<string> stringList1 = new List<string>();
    if (this.character.AuracurseImmune.Count > 0)
    {
      for (int index = 0; index < this.character.AuracurseImmune.Count; ++index)
        stringList1.Add(this.character.AuracurseImmune[index]);
    }
    List<string> stringList2 = this.character.AuraCurseImmunitiesByItemsList();
    for (int index = 0; index < stringList2.Count; ++index)
    {
      if (!stringList1.Contains(stringList2[index]))
        stringList1.Add(stringList2[index]);
    }
    if (stringList1.Count > 0)
    {
      for (int index = 0; index < stringList1.Count; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BuffPrefab, Vector3.zero, Quaternion.identity, this.GO_Immunities.transform);
        gameObject.GetComponent<Buff>().SetBuff(Globals.Instance.GetAuraCurseData(stringList1[index]), 1, _charId: _character.Id);
        gameObject.GetComponent<Buff>().SetBuffInStats(true);
      }
      this.notImmune.gameObject.SetActive(false);
    }
    else
    {
      this.yesImmune.gameObject.SetActive(false);
      this.notImmune.gameObject.SetActive(true);
    }
    Dictionary<string, int> dictionary1 = this.character.AuraCurseModification;
    Dictionary<string, int> auraCurseModifiers1 = this.character.GetItemAuraCurseModifiers();
    Dictionary<string, int> auraCurseModifiers2 = this.character.GetTraitAuraCurseModifiers();
    Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
    if ((UnityEngine.Object) this.character.HeroData != (UnityEngine.Object) null && (UnityEngine.Object) this.character.HeroData.HeroSubClass != (UnityEngine.Object) null)
      dictionary1 = Perk.GetAuraCurseBonusDict(this.character.HeroData.HeroSubClass.Id);
    foreach (KeyValuePair<string, int> keyValuePair in auraCurseModifiers1)
    {
      if (keyValuePair.Key != "")
      {
        if (dictionary2.ContainsKey(keyValuePair.Key))
          dictionary2[keyValuePair.Key] += keyValuePair.Value;
        else
          dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
      }
    }
    foreach (KeyValuePair<string, int> keyValuePair in auraCurseModifiers2)
    {
      if (keyValuePair.Key != "")
      {
        if (dictionary2.ContainsKey(keyValuePair.Key))
          dictionary2[keyValuePair.Key] += keyValuePair.Value;
        else
          dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
      }
    }
    foreach (KeyValuePair<string, int> keyValuePair in dictionary1)
    {
      if (keyValuePair.Key != "")
      {
        if (dictionary2.ContainsKey(keyValuePair.Key))
          dictionary2[keyValuePair.Key] += keyValuePair.Value;
        else
          dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
      }
    }
    for (int index = 0; index < this.GO_AuraCurse.transform.childCount; ++index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.GO_AuraCurse.transform.GetChild(index).gameObject);
    if (dictionary2.Count > 0)
    {
      foreach (KeyValuePair<string, int> keyValuePair in dictionary2)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BuffPrefab, Vector3.zero, Quaternion.identity, this.GO_AuraCurse.transform);
        int num17;
        string _chargesStr;
        if (keyValuePair.Value > 0)
        {
          num17 = keyValuePair.Value;
          _chargesStr = "+" + num17.ToString();
        }
        else if (keyValuePair.Value > 0)
        {
          num17 = keyValuePair.Value;
          _chargesStr = "-" + num17.ToString();
        }
        else
        {
          num17 = keyValuePair.Value;
          _chargesStr = num17.ToString() ?? "";
        }
        gameObject.GetComponent<Buff>().SetBuff(Globals.Instance.GetAuraCurseData(keyValuePair.Key), keyValuePair.Value, _chargesStr, _character.Id);
        gameObject.GetComponent<Buff>().SetBuffInStatsCharges();
      }
      this.notCharges.gameObject.SetActive(false);
    }
    else
      this.notCharges.gameObject.SetActive(true);
  }

  private void DoPopupResistance(
    int resistanceIndex,
    Enums.DamageType resistanceType,
    int resistanceValue,
    int resistanceMod,
    int resistanceItems)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<line-height=140%><size=+2>");
    stringBuilder.Append("<size=+4><sprite name=man></size> <color=#FFF>");
    stringBuilder.Append(Texts.Instance.GetText("baseResist"));
    stringBuilder.Append(":</color> ");
    stringBuilder.Append(resistanceValue);
    stringBuilder.Append("%\n");
    int num = 0;
    if (resistanceMod != 0 || resistanceItems != 0)
    {
      if (resistanceItems != 0)
      {
        foreach (KeyValuePair<string, int> itemResistModifiers in this.character.GetItemResistModifiersDictionary(resistanceType))
        {
          string[] strArray = itemResistModifiers.Key.Split('_', StringSplitOptions.None);
          if (strArray.Length > 1)
          {
            if (num > 0)
              stringBuilder.Append("\n");
            stringBuilder.Append("<size=+4><sprite name=");
            if (strArray[1].ToLower() == "enchantment")
              stringBuilder.Append("card");
            else
              stringBuilder.Append(strArray[1].ToLower());
            stringBuilder.Append("></size> ");
            stringBuilder.Append("<color=#A9815D>");
            stringBuilder.Append(strArray[0]);
            stringBuilder.Append(":</color> ");
            if (itemResistModifiers.Value > 0)
            {
              stringBuilder.Append("+");
              stringBuilder.Append(itemResistModifiers.Value);
            }
            else
              stringBuilder.Append(itemResistModifiers.Value);
            stringBuilder.Append("%");
            ++num;
          }
        }
      }
      if (resistanceMod != 0)
      {
        foreach (KeyValuePair<string, int> auraResistModifiers in this.character.GetAuraResistModifiersDictionary(resistanceType))
        {
          if (num > 0)
            stringBuilder.Append("\n");
          stringBuilder.Append("<size=+4><sprite name=");
          if (auraResistModifiers.Key == "enchantment")
            stringBuilder.Append("card");
          else
            stringBuilder.Append(auraResistModifiers.Key);
          stringBuilder.Append("></size> ");
          if (auraResistModifiers.Value > 0)
            stringBuilder.Append("<color=#5D82A8>");
          else if (auraResistModifiers.Value < 0)
            stringBuilder.Append("<color=#A85D6A>");
          else
            stringBuilder.Append("<color=#FFF>");
          stringBuilder.Append(Functions.UppercaseFirst(auraResistModifiers.Key));
          stringBuilder.Append(":</color> ");
          if (auraResistModifiers.Value > 0)
          {
            stringBuilder.Append("+");
            stringBuilder.Append(auraResistModifiers.Value);
          }
          else
            stringBuilder.Append(auraResistModifiers.Value);
          stringBuilder.Append("%");
          ++num;
        }
      }
    }
    if (!this.character.IsHero && AtOManager.Instance.IsChallengeTraitActive("vulnerablemonsters"))
    {
      if (num > 0)
        stringBuilder.Append("\n");
      stringBuilder.Append("<color=#DE96C2>");
      stringBuilder.Append(Texts.Instance.GetText("vulnerablemonsters"));
      stringBuilder.Append(":</color> -15%");
    }
    if (this.resistancePop == null || resistanceIndex >= this.resistancePop.Length)
      return;
    this.resistancePop[resistanceIndex].text = stringBuilder.ToString();
  }

  private void DoPopupDamageDone(
    int resistanceIndex,
    Enums.DamageType damageType,
    int damageBase,
    int damageAura,
    int damageItems)
  {
    StringBuilder stringBuilder = new StringBuilder();
    int num1 = damageBase - damageAura - damageItems;
    if (num1 != 0 || damageAura != 0 || damageItems != 0)
    {
      int num2 = 0;
      stringBuilder.Append("<line-height=140%><size=+2>");
      if (num1 >= 0)
      {
        stringBuilder.Append("<size=+4><sprite name=man></size> <color=#FFF>");
        stringBuilder.Append(Texts.Instance.GetText("baseDamage"));
        stringBuilder.Append(":</color> ");
        if (num1 > 0)
        {
          stringBuilder.Append("+");
          stringBuilder.Append(num1);
        }
        else
          stringBuilder.Append(num1);
        stringBuilder.Append("\n");
      }
      if (damageItems != 0)
      {
        foreach (KeyValuePair<string, int> itemDamageDone in this.character.GetItemDamageDoneDictionary(damageType))
        {
          string[] strArray = itemDamageDone.Key.Split('_', StringSplitOptions.None);
          if (num2 > 0)
            stringBuilder.Append("\n");
          stringBuilder.Append("<size=+4><sprite name=");
          if (strArray[1].ToLower() == "enchantment")
            stringBuilder.Append("card");
          else
            stringBuilder.Append(strArray[1].ToLower());
          stringBuilder.Append("></size> ");
          stringBuilder.Append("<color=#A9815D>");
          stringBuilder.Append(strArray[0]);
          stringBuilder.Append(":</color> ");
          if (itemDamageDone.Value > 0)
          {
            stringBuilder.Append("+");
            stringBuilder.Append(itemDamageDone.Value);
          }
          else
            stringBuilder.Append(itemDamageDone.Value);
          ++num2;
        }
      }
      if (damageAura != 0)
      {
        foreach (KeyValuePair<string, int> auraDamageDone in this.character.GetAuraDamageDoneDictionary(damageType))
        {
          if (num2 > 0)
            stringBuilder.Append("\n");
          stringBuilder.Append("<size=+4><sprite name=");
          stringBuilder.Append(auraDamageDone.Key);
          stringBuilder.Append("></size> ");
          if (auraDamageDone.Value > 0)
            stringBuilder.Append("<color=#5D82A8>");
          else if (auraDamageDone.Value < 0)
            stringBuilder.Append("<color=#A85D6A>");
          else
            stringBuilder.Append("<color=#FFF>");
          stringBuilder.Append(Functions.UppercaseFirst(auraDamageDone.Key));
          stringBuilder.Append(":</color> ");
          if (auraDamageDone.Value > 0)
          {
            stringBuilder.Append("+");
            stringBuilder.Append(auraDamageDone.Value);
          }
          else
            stringBuilder.Append(auraDamageDone.Value);
          ++num2;
        }
      }
    }
    this.damagedonePop[resistanceIndex].text = stringBuilder.ToString();
  }

  private void DoPopupDamageTaken(int resistanceIndex, Enums.DamageType damageType, int damageAura)
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    int num1 = 0;
    Dictionary<string, int> damageTakenDictionary = this.character.GetAuraDamageTakenDictionary(damageType);
    int num2 = 0;
    foreach (KeyValuePair<string, int> keyValuePair in damageTakenDictionary)
    {
      if (num1 > 0)
        stringBuilder1.Append("\n");
      stringBuilder1.Append("<size=+4><sprite name=");
      stringBuilder1.Append(keyValuePair.Key);
      stringBuilder1.Append("></size> ");
      if (keyValuePair.Value > 0)
        stringBuilder1.Append("<color=#5D82A8>");
      else if (keyValuePair.Value < 0)
        stringBuilder1.Append("<color=#A85D6A>");
      else
        stringBuilder1.Append("<color=#FFF>");
      stringBuilder1.Append(Functions.UppercaseFirst(keyValuePair.Key));
      stringBuilder1.Append(":</color> ");
      if (keyValuePair.Value > 0)
      {
        stringBuilder1.Append("+");
        stringBuilder1.Append(keyValuePair.Value);
      }
      else
        stringBuilder1.Append(keyValuePair.Value);
      ++num1;
      num2 += keyValuePair.Value;
    }
    if (num2 != 0 || damageAura != 0)
    {
      StringBuilder stringBuilder2 = new StringBuilder();
      stringBuilder2.Append("<line-height=140%><size=+2><size=+4><sprite name=man></size> <color=#FFF>");
      stringBuilder2.Append(Texts.Instance.GetText("baseDT"));
      stringBuilder2.Append(":</color> ");
      stringBuilder2.Append(-1 * (num2 - damageAura));
      stringBuilder2.Append("</size></line-height>\n");
      stringBuilder1.Insert(0, stringBuilder2.ToString());
    }
    if (stringBuilder1.Length > 0)
      stringBuilder1.Insert(0, "<line-height=140%><size=+2>");
    this.damagetakenPop[resistanceIndex].text = stringBuilder1.ToString();
  }

  private void DoPopupGeneral(
    Dictionary<string, int> itemValues,
    Dictionary<string, int> auraValues,
    PopupText popText,
    string type = "percent",
    bool showBase = false,
    int valueBase = 0)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (itemValues != null && itemValues.Count > 0 || auraValues != null && auraValues.Count > 0)
    {
      if (showBase && valueBase != 0)
      {
        stringBuilder.Append("<line-height=140%><size=+2>");
        stringBuilder.Append("<size=+4><sprite name=man></size> <color=#FFF>");
        stringBuilder.Append(Texts.Instance.GetText("baseValue"));
        stringBuilder.Append(":</color> ");
        stringBuilder.Append(valueBase);
        if (type == "percent")
          stringBuilder.Append("%");
        stringBuilder.Append("\n");
      }
      int num = 0;
      if (!showBase)
        stringBuilder.Append("<line-height=140%><size=+2>");
      if (itemValues != null)
      {
        foreach (KeyValuePair<string, int> itemValue in itemValues)
        {
          string[] strArray = itemValue.Key.Split('_', StringSplitOptions.None);
          if (num > 0)
            stringBuilder.Append("\n");
          stringBuilder.Append("<size=+4><sprite name=");
          if (strArray[1].ToLower() == "enchantment")
            stringBuilder.Append("card");
          else
            stringBuilder.Append(strArray[1].ToLower());
          stringBuilder.Append("></size> ");
          stringBuilder.Append("<color=#A9815D>");
          stringBuilder.Append(strArray[0]);
          stringBuilder.Append(":</color> ");
          if (itemValue.Value > 0)
          {
            stringBuilder.Append("+");
            stringBuilder.Append(itemValue.Value);
          }
          else
            stringBuilder.Append(itemValue.Value);
          if (type == "percent")
            stringBuilder.Append("%");
          ++num;
        }
      }
      if (auraValues != null)
      {
        foreach (KeyValuePair<string, int> auraValue in auraValues)
        {
          if (num > 0)
            stringBuilder.Append("\n");
          stringBuilder.Append("<size=+4><sprite name=");
          stringBuilder.Append(auraValue.Key);
          stringBuilder.Append("></size> ");
          if (auraValue.Value > 0)
            stringBuilder.Append("<color=#5D82A8>");
          else if (auraValue.Value < 0)
            stringBuilder.Append("<color=#A85D6A>");
          else
            stringBuilder.Append("<color=#FFF>");
          stringBuilder.Append(Functions.UppercaseFirst(auraValue.Key));
          stringBuilder.Append(":</color> ");
          if (auraValue.Value > 0)
          {
            stringBuilder.Append("+");
            stringBuilder.Append(auraValue.Value);
          }
          else
            stringBuilder.Append(auraValue.Value);
          if (type == "percent")
            stringBuilder.Append("%");
          ++num;
        }
      }
    }
    popText.text = stringBuilder.ToString();
  }

  private string FormatResistance(int value, int mod)
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

  private string FormatSum(int value, int mod, bool notLessThanZero = true)
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
    if (notLessThanZero && value < 0)
      value = 0;
    if (value > 0)
    {
      stringBuilder.Append("+");
      stringBuilder.Append(value);
    }
    else if (value < 0)
      stringBuilder.Append(value);
    else
      stringBuilder.Append("--");
    if (mod != 0)
      stringBuilder.Append("</color>");
    return stringBuilder.ToString();
  }

  private string FormatTaken(int value)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (value > 0)
    {
      stringBuilder.Append("<color=");
      stringBuilder.Append(this.colorRed);
      stringBuilder.Append(">");
      stringBuilder.Append("+");
    }
    if (value == 0)
      stringBuilder.Append("--");
    else
      stringBuilder.Append(value);
    if (value > 0)
      stringBuilder.Append("</color>");
    return stringBuilder.ToString();
  }

  private string FormatPercent(int value, int mod)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (mod > 0)
    {
      stringBuilder.Append("<color=");
      stringBuilder.Append(this.colorGreen);
      stringBuilder.Append(">");
      stringBuilder.Append("+");
    }
    else if (mod < 0)
    {
      stringBuilder.Append("<color=");
      stringBuilder.Append(this.colorRed);
      stringBuilder.Append(">");
    }
    else
      stringBuilder.Append("+");
    stringBuilder.Append(value);
    stringBuilder.Append("%");
    if (mod != 0)
      stringBuilder.Append("</color>");
    return stringBuilder.ToString();
  }

  private string GetTextDT(string type)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<size=4><sprite name=");
    stringBuilder.Append(type);
    stringBuilder.Append("></size> ");
    stringBuilder.Append(Texts.Instance.GetText(type));
    return stringBuilder.ToString();
  }

  public bool IsActive() => this.gameObject.activeSelf;
}
