// Decompiled with JetBrains decompiler
// Type: CombatTextInstance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class CombatTextInstance : MonoBehaviour
{
  public Transform textTMT;
  private Animator animator;
  private TMP_Text textTM;
  private CastResolutionForCombatText _cast;
  private bool inversed;

  private void Awake()
  {
    this.textTM = this.textTMT.GetComponent<TMP_Text>();
    this.transform.localPosition = new Vector3(0.0f, 2.6f, 0.0f);
    this.textTMT.gameObject.SetActive(false);
    this.animator = this.GetComponent<Animator>();
  }

  public void ShowDamage(
    CombatText CT,
    CharacterItem characterItem,
    CastResolutionForCombatText _castObj)
  {
    this._cast = _castObj;
    int num1 = this._cast.damage + this._cast.damage2;
    int heal = this._cast.heal;
    string effect = this._cast.effect;
    if (num1 > 0)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (effect != "")
      {
        stringBuilder.Append("<sprite name=");
        stringBuilder.Append(effect.ToLower());
        stringBuilder.Append(">");
      }
      stringBuilder.Append("-");
      stringBuilder.Append(num1);
      stringBuilder.Append(" Hp");
      this.ShowText(CT, characterItem, stringBuilder.ToString(), Enums.CombatScrollEffectType.Damage);
    }
    else if (heal > 0)
    {
      this.ShowText(CT, characterItem, heal.ToString(), Enums.CombatScrollEffectType.Heal);
    }
    else
    {
      int blocked = this._cast.blocked;
      bool immune = this._cast.immune;
      int num2 = this._cast.invulnerable ? 1 : 0;
      bool evaded = this._cast.evaded;
      bool fullblocked = this._cast.fullblocked;
      StringBuilder stringBuilder = new StringBuilder();
      if (num2 != 0)
      {
        if (effect != "")
        {
          stringBuilder.Append(effect);
          stringBuilder.Append("\n*");
          stringBuilder.Append(Texts.Instance.GetText("invulnerable"));
          stringBuilder.Append("*");
          this.ShowText(CT, characterItem, stringBuilder.ToString(), Enums.CombatScrollEffectType.Damage);
        }
        else
        {
          stringBuilder.Append("*");
          stringBuilder.Append(Texts.Instance.GetText("invulnerable"));
          stringBuilder.Append("*");
          this.ShowText(CT, characterItem, stringBuilder.ToString(), Enums.CombatScrollEffectType.Aura);
        }
        GameManager.Instance.PlayLibraryAudio("invulnerable_sound");
        if (characterItem.IsHero)
          EffectsManager.Instance.PlayEffectAC("invulnerable", true, characterItem.CharImageT, false);
        else
          EffectsManager.Instance.PlayEffectAC("invulnerable", false, characterItem.CharImageT, true);
      }
      else if (evaded)
      {
        if (effect != "")
        {
          stringBuilder.Append(effect);
          stringBuilder.Append("\n*");
          stringBuilder.Append(Texts.Instance.GetText("evaded"));
          stringBuilder.Append("*");
          this.ShowText(CT, characterItem, stringBuilder.ToString(), Enums.CombatScrollEffectType.Damage);
        }
        else
        {
          stringBuilder.Append("*");
          stringBuilder.Append(Texts.Instance.GetText("evaded"));
          stringBuilder.Append("*");
          this.ShowText(CT, characterItem, stringBuilder.ToString(), Enums.CombatScrollEffectType.Aura);
        }
        if (characterItem.IsHero)
          EffectsManager.Instance.PlayEffectAC("evasion", true, characterItem.CharImageT, false);
        else
          EffectsManager.Instance.PlayEffectAC("evasion", false, characterItem.CharImageT, true);
      }
      else if (fullblocked)
      {
        if (effect != "")
        {
          stringBuilder.Append(effect);
          stringBuilder.Append("\n*");
          stringBuilder.Append(Texts.Instance.GetText("blocked"));
          stringBuilder.Append("*");
          this.ShowText(CT, characterItem, stringBuilder.ToString(), Enums.CombatScrollEffectType.Damage);
        }
        else
        {
          stringBuilder.Append("*");
          stringBuilder.Append(Texts.Instance.GetText("blocked"));
          stringBuilder.Append("*");
          this.ShowText(CT, characterItem, stringBuilder.ToString(), Enums.CombatScrollEffectType.Aura);
        }
        GameManager.Instance.PlayLibraryAudio("block_attack_sound");
        if (characterItem.IsHero)
          EffectsManager.Instance.PlayEffectAC("blocked", true, characterItem.CharImageT, false);
        else
          EffectsManager.Instance.PlayEffectAC("blocked", false, characterItem.CharImageT, true);
      }
      else if (immune)
      {
        if (effect != "")
        {
          stringBuilder.Append(effect);
          stringBuilder.Append("\n*");
          stringBuilder.Append(Texts.Instance.GetText("immune"));
          stringBuilder.Append("*");
          this.ShowText(CT, characterItem, stringBuilder.ToString(), Enums.CombatScrollEffectType.Damage);
        }
        else
        {
          stringBuilder.Append("*");
          stringBuilder.Append(Texts.Instance.GetText("immune"));
          stringBuilder.Append("*");
          this.ShowText(CT, characterItem, stringBuilder.ToString(), Enums.CombatScrollEffectType.Damage);
        }
      }
      else if (blocked > 0)
      {
        if (effect != "")
        {
          stringBuilder.Append(effect);
          stringBuilder.Append("\n*");
          stringBuilder.Append(Texts.Instance.GetText("blocked"));
          stringBuilder.Append("*");
          this.ShowText(CT, characterItem, stringBuilder.ToString(), Enums.CombatScrollEffectType.Damage);
        }
        else
        {
          stringBuilder.Append("*");
          stringBuilder.Append(Texts.Instance.GetText("blocked"));
          stringBuilder.Append("*");
          this.ShowText(CT, characterItem, stringBuilder.ToString(), Enums.CombatScrollEffectType.Aura);
        }
        GameManager.Instance.PlayLibraryAudio("block_attack_sound");
        if (characterItem.IsHero)
          EffectsManager.Instance.PlayEffectAC("blocked", true, characterItem.CharImageT, false);
        else
          EffectsManager.Instance.PlayEffectAC("blocked", false, characterItem.CharImageT, true);
      }
      else
      {
        if (!this._cast.mitigated)
          return;
        this.ShowText(CT, characterItem, "0", Enums.CombatScrollEffectType.Damage);
      }
    }
  }

  public void ShowText(
    CombatText CT,
    CharacterItem characterItem,
    string text,
    Enums.CombatScrollEffectType type)
  {
    Color color1;
    switch (type)
    {
      case Enums.CombatScrollEffectType.Damage:
        color1 = new Color(0.87f, 0.16f, 0.07f, 1f);
        break;
      case Enums.CombatScrollEffectType.Heal:
        color1 = new Color(0.13f, 0.56f, 0.17f, 1f);
        break;
      case Enums.CombatScrollEffectType.Aura:
        color1 = new Color(0.2f, 0.76f, 0.94f, 1f);
        break;
      case Enums.CombatScrollEffectType.Curse:
        color1 = new Color(0.61f, 0.61f, 0.61f, 1f);
        break;
      case Enums.CombatScrollEffectType.Energy:
        color1 = new Color(1f, 0.66f, 0.0f, 1f);
        break;
      case Enums.CombatScrollEffectType.Block:
        color1 = new Color(0.45f, 0.45f, 0.45f, 1f);
        break;
      case Enums.CombatScrollEffectType.Trait:
        color1 = new Color(0.74f, 0.43f, 0.21f, 1f);
        break;
      case Enums.CombatScrollEffectType.Weapon:
      case Enums.CombatScrollEffectType.Armor:
      case Enums.CombatScrollEffectType.Jewelry:
      case Enums.CombatScrollEffectType.Accesory:
        color1 = new Color(1f, 0.78f, 0.4f, 1f);
        break;
      case Enums.CombatScrollEffectType.Corruption:
        color1 = new Color(0.64f, 0.18f, 0.57f, 1f);
        break;
      default:
        color1 = new Color(0.41f, 0.41f, 0.41f, 1f);
        break;
    }
    Color color2 = color1 + new Color(0.2f, 0.2f, 0.2f);
    Color color3 = color1;
    if (type != Enums.CombatScrollEffectType.Damage && type != Enums.CombatScrollEffectType.Heal)
      this.textTM.colorGradient = new VertexGradient(color2, color2, color3, color3);
    switch (type)
    {
      case Enums.CombatScrollEffectType.Trait:
        text = "<sprite name=experience> " + text;
        break;
      case Enums.CombatScrollEffectType.Weapon:
        text = "<sprite name=weapon> " + text;
        break;
      case Enums.CombatScrollEffectType.Armor:
        text = "<sprite name=armor> " + text;
        break;
      case Enums.CombatScrollEffectType.Jewelry:
        text = "<sprite name=jewelry> " + text;
        break;
      case Enums.CombatScrollEffectType.Accesory:
        text = "<sprite name=accesory> " + text;
        break;
      case Enums.CombatScrollEffectType.Corruption:
        text = "<sprite name=corruption> " + text;
        break;
    }
    this.textTM.text = text;
    this.StartCoroutine(this.ScrollCombatText());
  }

  private IEnumerator ScrollCombatText()
  {
    CombatTextInstance combatTextInstance = this;
    combatTextInstance.textTMT.gameObject.SetActive(true);
    Color color = new Color(0.0f, 0.0f, 0.0f, 0.03f);
    ++MatchManager.Instance.CombatTextIterations;
    if (combatTextInstance._cast != null && (combatTextInstance._cast.damage != 0 || combatTextInstance._cast.damage2 != 0 || combatTextInstance._cast.heal != 0 || combatTextInstance._cast.mitigated))
    {
      if (combatTextInstance.inversed)
        combatTextInstance.transform.localScale = new Vector3(-1f * combatTextInstance.transform.localScale.x, combatTextInstance.transform.localScale.y, combatTextInstance.transform.localScale.z);
      combatTextInstance.textTM.GetComponent<MeshRenderer>().sortingOrder = 20000 + MatchManager.Instance.CombatTextIterations;
      StringBuilder stringBuilder = new StringBuilder();
      if (combatTextInstance._cast.damage != 0 || combatTextInstance._cast.mitigated)
      {
        stringBuilder.Append(combatTextInstance._cast.damage);
        if (combatTextInstance._cast.damageType != Enums.DamageType.None)
        {
          stringBuilder.Append(" <size=2.4><sprite name=");
          stringBuilder.Append(Enum.GetName(typeof (Enums.DamageType), (object) combatTextInstance._cast.damageType).ToLower());
          stringBuilder.Append("></size>");
        }
        else if (combatTextInstance._cast.effect != "")
        {
          stringBuilder.Append(" <size=2.4><sprite name=");
          stringBuilder.Append(combatTextInstance._cast.effect.ToLower());
          stringBuilder.Append("></size>");
        }
        else if (combatTextInstance._cast.damage > 0)
        {
          stringBuilder.Append(" <size=2.4><sprite name=");
          stringBuilder.Append("heart");
          stringBuilder.Append("></size>");
        }
        if (combatTextInstance._cast.damage2 > 0)
        {
          stringBuilder.Append("\n");
          stringBuilder.Append(combatTextInstance._cast.damage2);
          if (combatTextInstance._cast.damageType2 != Enums.DamageType.None)
          {
            stringBuilder.Append(" <size=2.4><sprite name=");
            stringBuilder.Append(Enum.GetName(typeof (Enums.DamageType), (object) combatTextInstance._cast.damageType2).ToLower());
            stringBuilder.Append("></size>");
          }
          else if (combatTextInstance._cast.effect != "")
          {
            stringBuilder.Append(" <size=2.4><sprite name=");
            stringBuilder.Append(combatTextInstance._cast.effect.ToLower());
            stringBuilder.Append("></size>");
          }
          else if (combatTextInstance._cast.damage2 > 0)
          {
            stringBuilder.Append(" <size=2.4><sprite name=");
            stringBuilder.Append("heart");
            stringBuilder.Append("></size>");
          }
        }
      }
      else if (combatTextInstance._cast.damage2 > 0)
      {
        stringBuilder.Append(combatTextInstance._cast.damage2);
        stringBuilder.Append(" <size=2.4><sprite name=");
        if (combatTextInstance._cast.damageType2 != Enums.DamageType.None)
          stringBuilder.Append(Enum.GetName(typeof (Enums.DamageType), (object) combatTextInstance._cast.damageType2).ToLower());
        else if (combatTextInstance._cast.effect != "")
          stringBuilder.Append(combatTextInstance._cast.effect.ToLower());
        else
          stringBuilder.Append("heart");
        stringBuilder.Append("></size>");
      }
      else
      {
        stringBuilder.Append(combatTextInstance._cast.heal);
        stringBuilder.Append(" <size=2.4><sprite name=heal>");
      }
      combatTextInstance.textTM.text = stringBuilder.ToString();
      if ((double) combatTextInstance.transform.position.x > 0.0)
        combatTextInstance.animator.SetTrigger("popL");
      else
        combatTextInstance.animator.SetTrigger("popR");
    }
    else
    {
      combatTextInstance.textTM.GetComponent<MeshRenderer>().sortingOrder = 10000 + MatchManager.Instance.CombatTextIterations;
      if (!combatTextInstance.inversed)
        combatTextInstance.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
      else
        combatTextInstance.transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
      combatTextInstance.animator.SetTrigger("scroll");
    }
    combatTextInstance.animator.enabled = true;
    yield return (object) Globals.Instance.WaitForSeconds(3f);
    UnityEngine.Object.Destroy((UnityEngine.Object) combatTextInstance.gameObject);
  }
}
