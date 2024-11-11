// Decompiled with JetBrains decompiler
// Type: CharacterItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterItem : MonoBehaviour
{
  public SpriteOutline spriteOutline;
  [SerializeField]
  private Transform charImageT;
  [SerializeField]
  private Transform charImageShadowT;
  [SerializeField]
  private TMP_Text hpText;
  [SerializeField]
  private Hero _hero;
  [SerializeField]
  private NPC _npc;
  private Animator anim;
  public Animator animPet;
  private PopupSheet popupSheet;
  private Coroutine KillCoroutine;
  public Transform characterTransform;
  public Transform keyTransform;
  public Transform keyRed;
  public Transform keyBackground;
  public TMP_Text keyNumber;
  public ItemCombatIcon iconEnchantment;
  public ItemCombatIcon iconEnchantment2;
  public ItemCombatIcon iconEnchantment3;
  public Transform healthBar;
  public Transform hpRed;
  public Transform hpPoison;
  public Transform hpBleed;
  public Transform hpRegen;
  public SpriteRenderer hpSR;
  public Transform activeMarkTR;
  public Transform hpT;
  public Transform skull;
  public ParticleSystem skullParticle;
  public TMP_Text purgedispel;
  public TMP_Text purgedispelTitle;
  public TMP_Text purgedispelQuantity;
  public TMP_Text overDebuff;
  public TMP_Text hpBlockText;
  public Transform hpBlockT;
  public Transform hpBackground;
  public Transform hpBackgroundHigh;
  public Transform hpBlockIconT;
  public Transform blockBorderT;
  public TMP_Text hpShieldText;
  public Transform hpShieldT;
  public Transform hpDoomIconT;
  public TMP_Text hpDoomText;
  public TMP_Text dmgPreviewText;
  public Transform thornsTransform;
  public Transform tauntTextTransform;
  private TMP_Text tauntTextT;
  public GameObject GO_Buffs;
  private List<Buff> GoBuffs = new List<Buff>();
  public GameObject GO_Taunt;
  public GameObject BuffPrefab;
  public Shader energyDefaultShader;
  public Shader energyShader;
  public ParticleSystem trailParticle;
  public ParticleSystem stealthParticle;
  public Material defaultMaterial;
  public Material stealthMaterial;
  public Material paralyzeMaterial;
  public Material tauntMaterial;
  public GameObject combatTextPrefab;
  private CombatText CT;
  private SpriteRenderer charImageSR;
  private bool isHero;
  private bool isDying;
  private Color colorFade;
  private Vector3 vectorFade;
  private Vector3 originalLocalPosition;
  private bool charIsMoving;
  public Transform energyT;
  public TMP_Text energyTxt;
  private Transform[] energyArr;
  private SpriteRenderer[] energySR;
  private Animator[] energySRAnimator;
  private EnergyPoint[] energyPoint;
  private List<SpriteRenderer> animatedSprites;
  private Dictionary<string, Material> animatedSpritesDefaultMaterial;
  private List<SetSpriteLayerFromBase> animatedSpritesOutOfCharacter;
  private Transform shadowSprite;
  public Transform heroDecks;
  public TMP_Text heroDecksCounter;
  public TMP_Text heroDecksDeckText;
  public TMP_Text heroDecksDeckTextBg;
  public TMP_Text heroDecksDiscardText;
  public TMP_Text heroDecksDiscardTextBg;
  private Coroutine moveCenterCo;
  private Coroutine moveBackCo;
  private Coroutine helpCo;
  private Coroutine blockCo;
  private Coroutine hitCo;
  private bool characterBeingHitted;
  private Dictionary<string, int> buffAnimationList = new Dictionary<string, int>();
  private Coroutine buffAnimationCo;
  public Transform transformForCombatText;
  private bool isActive;
  public float heightModel;
  private int petMagnusCounter;
  private Coroutine petMagnusCoroutine;
  private int petMagnusAnswer;
  private int petYoggerCounter;
  private Coroutine petYoggerCoroutine;
  private int petYoggerAnswer;
  public NPCItem PetItem;
  public bool PetItemFront = true;
  public NPCItem PetItemEnchantment;
  public bool PetItemEnchantmentFront = true;
  private Coroutine drawBuffsCoroutine;
  private int counterEffectItemOwner;
  private int indexEffectItemOwner;
  public EmoteCharacterPing emoteCharacterPing;
  private bool animationBusy;
  private Coroutine animationBusyCo;

  public virtual void Awake()
  {
    this.animatedSprites = new List<SpriteRenderer>();
    this.animatedSpritesDefaultMaterial = new Dictionary<string, Material>();
    this.animatedSpritesOutOfCharacter = new List<SetSpriteLayerFromBase>();
    this.colorFade = new Color(0.0f, 0.0f, 0.0f, 0.02f);
    this.vectorFade = new Vector3(1f / 1000f, 0.0f, 0.0f);
    if ((UnityEngine.Object) this.charImageT == (UnityEngine.Object) null)
      return;
    this.charImageSR = this.charImageT.GetComponent<SpriteRenderer>();
    this.CT = UnityEngine.Object.Instantiate<GameObject>(this.combatTextPrefab, Vector3.zero, Quaternion.identity, this.charImageT).GetComponent<CombatText>();
    if ((bool) (UnityEngine.Object) MatchManager.Instance)
      this.popupSheet = MatchManager.Instance.popupSheet;
    this.energyArr = new Transform[10];
    this.energyArr[0] = this.energyT;
    this.energyPoint = new EnergyPoint[10];
    this.energyPoint[0] = this.energyT.GetComponent<EnergyPoint>();
    this.energySR = new SpriteRenderer[10];
    this.energySR[0] = this.energyT.GetComponent<SpriteRenderer>();
    this.energySRAnimator = new Animator[10];
    this.energySRAnimator[0] = this.energyT.GetComponent<Animator>();
    this.tauntTextTransform.gameObject.SetActive(false);
    this.tauntTextT = this.tauntTextTransform.GetComponent<TMP_Text>();
    this.thornsTransform.gameObject.SetActive(false);
    this.skull.gameObject.SetActive(false);
    Vector3 localPosition = this.energyT.localPosition;
    for (int index = 1; index < 10; ++index)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.energyT.gameObject, this.energyT.parent);
      gameObject.transform.localPosition = new Vector3(localPosition.x + 0.11f * (float) index, localPosition.y, localPosition.z - 0.01f * (float) index);
      this.energyArr[index] = gameObject.transform;
      this.energyPoint[index] = gameObject.transform.GetComponent<EnergyPoint>();
      this.energySR[index] = gameObject.transform.GetComponent<SpriteRenderer>();
      this.energySRAnimator[index] = gameObject.transform.GetComponent<Animator>();
    }
    for (int index = 0; index < 20; ++index)
    {
      this.GoBuffs.Add(UnityEngine.Object.Instantiate<GameObject>(this.BuffPrefab, Vector3.zero, Quaternion.identity, this.GO_Buffs.transform).GetComponent<Buff>());
      this.GoBuffs[index].CleanBuff();
    }
    if (!((UnityEngine.Object) this.keyTransform != (UnityEngine.Object) null) || !this.keyTransform.gameObject.activeSelf)
      return;
    this.keyTransform.gameObject.SetActive(false);
  }

  public virtual void Start()
  {
  }

  public List<string> CalculateDamagePrePostForThisCharacter()
  {
    Character _characterCaster = this._hero == null ? (Character) this._npc : (Character) this._hero;
    if (MatchManager.Instance.prePostDamageDictionary != null && _characterCaster != null && MatchManager.Instance.prePostDamageDictionary.ContainsKey(_characterCaster.Id))
      return MatchManager.Instance.prePostDamageDictionary[_characterCaster.Id];
    int num1 = 0;
    int num2 = 0;
    bool flag1 = false;
    bool flag2 = false;
    int hp = _characterCaster.GetHp();
    int auraCharges1 = _characterCaster.GetAuraCharges("block");
    int auraCharges2 = _characterCaster.GetAuraCharges("shield");
    bool flag3 = false;
    string str1 = Globals.Instance.ClassColor["warrior"];
    string str2 = "#1B9604";
    string str3 = "#00A49E";
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.Append("<size=+26><sprite name=skull></size><size=+5>");
    stringBuilder1.Append(Texts.Instance.GetText("characterDies"));
    stringBuilder1.Append("</size><size=0><br><br></size>");
    string str4 = stringBuilder1.ToString();
    if (_characterCaster.RoundMoved >= MatchManager.Instance.GetCurrentRound())
      flag3 = true;
    List<string> forThisCharacter = new List<string>();
    forThisCharacter.Add("0");
    int num3 = 0;
    forThisCharacter.Add("0");
    int num4 = 0;
    forThisCharacter.Add("0");
    bool flag4 = true;
    for (int index = 0; index < _characterCaster.AuraList.Count; ++index)
    {
      if (index < _characterCaster.AuraList.Count && _characterCaster.AuraList[index] != null && (UnityEngine.Object) _characterCaster.AuraList[index].ACData != (UnityEngine.Object) null && _characterCaster.AuraList[index].ACData.NoRemoveBlockAtTurnEnd)
        flag4 = false;
    }
    int num5;
    if (!flag3)
    {
      num5 = auraCharges1;
    }
    else
    {
      num5 = auraCharges2;
      if (!flag4)
        num5 += auraCharges1;
    }
    StringBuilder stringBuilder2 = new StringBuilder();
    int num6 = 0;
    Character characterActive = MatchManager.Instance.GetCharacterActive();
    for (int index1 = 0; index1 < 2; ++index1)
    {
      if (index1 == 0)
        num6 = characterActive == null || characterActive.Id != _characterCaster.Id ? 0 : 1;
      else if (index1 == 1)
        num6 = characterActive == null || characterActive.Id != _characterCaster.Id ? 1 : 0;
      for (int index2 = 0; index2 < _characterCaster.AuraList.Count; ++index2)
      {
        AuraCurseData auraCurseData = AtOManager.Instance.GlobalAuraCurseModificationByTraitsAndItems("consume", _characterCaster.AuraList[index2].ACData.Id, _characterCaster, (Character) null);
        if ((UnityEngine.Object) auraCurseData == (UnityEngine.Object) null)
          auraCurseData = _characterCaster.AuraList[index2].ACData;
        if (auraCurseData.ProduceHealWhenConsumed && (auraCurseData.ConsumedAtTurnBegin && num6 == 0 || auraCurseData.ConsumedAtTurn && num6 == 1))
        {
          int heal = 0 + auraCurseData.HealWhenConsumed + Functions.FuncRoundToInt((float) _characterCaster.AuraList[index2].AuraCharges * auraCurseData.HealWhenConsumedPerCharge);
          if (heal > 0)
          {
            int num7 = _characterCaster.HealReceivedFinal(heal);
            if (_characterCaster.GetHpLeftForMax() < num7)
              num7 = _characterCaster.GetHpLeftForMax();
            if (num7 > 0)
            {
              hp += num7;
              if (num1 == 0 && num6 == 0)
              {
                stringBuilder2.Append("<size=-1><color=#FFF>");
                stringBuilder2.Append(Texts.Instance.GetText("preturnEffects"));
                stringBuilder2.Append("</color></size>");
                stringBuilder2.Append("<br>");
                num1 = 1;
              }
              if (num2 == 0 && num6 == 1)
              {
                stringBuilder2.Append("<size=-1><color=#FFF>");
                stringBuilder2.Append(Texts.Instance.GetText("postturnEffects"));
                stringBuilder2.Append("</color></size>");
                stringBuilder2.Append("<br>");
                num2 = 1;
              }
              stringBuilder2.Append("<color=");
              stringBuilder2.Append(str3);
              stringBuilder2.Append("> ");
              stringBuilder2.Append("+");
              stringBuilder2.Append(num7);
              stringBuilder2.Append("  <sprite name=" + auraCurseData.Id);
              stringBuilder2.Append(">");
              stringBuilder2.Append("</color>");
              stringBuilder2.Append("<size=-1.5><voffset=2> <color=#666>|</color>   <voffset=0>");
              stringBuilder2.Append(hp);
              stringBuilder2.Append("  <sprite name=heart>");
              stringBuilder2.Append("</size>");
              stringBuilder2.Append("<br>");
              num3 += num7;
            }
          }
        }
        if (auraCurseData.ProduceDamageWhenConsumed && (auraCurseData.ConsumedAtTurnBegin && num6 == 0 || auraCurseData.ConsumedAtTurn && num6 == 1) && (auraCurseData.DamageWhenConsumed > 0 || (double) auraCurseData.DamageWhenConsumedPerCharge > 0.0))
        {
          int auraCharges3 = _characterCaster.AuraList[index2].AuraCharges;
          int num8 = 0;
          int num9 = 0;
          int num10 = num8 + auraCurseData.DamageWhenConsumed;
          int num11 = auraCharges3;
          if ((UnityEngine.Object) auraCurseData.ConsumedDamageChargesBasedOnACCharges != (UnityEngine.Object) null)
            num11 = _characterCaster.GetAuraCharges(auraCurseData.ConsumedDamageChargesBasedOnACCharges.Id);
          int num12 = num10 + Functions.FuncRoundToInt(auraCurseData.DamageWhenConsumedPerCharge * (float) num11);
          if (auraCurseData.DoubleDamageIfCursesLessThan > 0 && _characterCaster.GetAuraCurseTotal(false, true) < auraCurseData.DoubleDamageIfCursesLessThan)
            num12 *= 2;
          int num13;
          if (auraCurseData.DamageTypeWhenConsumed != Enums.DamageType.None)
          {
            num9 = num12 <= num5 ? num12 : num5;
            num12 -= num9;
            num5 -= num9;
            float num14 = (float) (-1 * _characterCaster.BonusResists(auraCurseData.DamageTypeWhenConsumed, _characterCaster.AuraList[index2].ACData.Id, num6 == 0, num6 == 1));
            num13 = Functions.FuncRoundToInt((float) num12 + (float) ((double) num12 * (double) num14 * 0.0099999997764825821));
          }
          else
            num13 = num12;
          hp -= num13;
          if (num6 == 0)
            num3 -= num13;
          else
            num4 -= num13;
          if (num1 == 0 && num6 == 0)
          {
            stringBuilder2.Append("<size=-1><color=#FFF>");
            stringBuilder2.Append(Texts.Instance.GetText("preturnEffects"));
            stringBuilder2.Append("</color></size>");
            stringBuilder2.Append("<br>");
            num1 = 1;
          }
          if (num2 == 0 && num6 == 1)
          {
            stringBuilder2.Append("<size=-1><color=#FFF>");
            stringBuilder2.Append(Texts.Instance.GetText("postturnEffects"));
            stringBuilder2.Append("</color></size>");
            stringBuilder2.Append("<br>");
            num2 = 1;
          }
          if (num6 == 0)
          {
            stringBuilder2.Append("<color=");
            stringBuilder2.Append(str1);
            stringBuilder2.Append("> ");
          }
          else
          {
            stringBuilder2.Append("<color=");
            stringBuilder2.Append(str2);
            stringBuilder2.Append("> ");
          }
          if (num13 != 0)
            stringBuilder2.Append("-");
          stringBuilder2.Append(num13);
          stringBuilder2.Append(" ");
          stringBuilder2.Append(" <sprite name=" + auraCurseData.Id);
          stringBuilder2.Append(">");
          stringBuilder2.Append("</color>");
          stringBuilder2.Append("<size=-1.5><voffset=2> <color=#666>|</color>   <voffset=0>");
          if (num9 > 0)
          {
            stringBuilder2.Append(num9);
            stringBuilder2.Append(" <sprite name=block> ");
          }
          int num15 = (num13 - num12) * -1;
          if (num15 != 0)
          {
            stringBuilder2.Append(num15);
            stringBuilder2.Append("  <sprite name=ui_resistance>  ");
          }
          stringBuilder2.Append(hp);
          stringBuilder2.Append("  <sprite name=heart>");
          stringBuilder2.Append("</size>");
          stringBuilder2.Append("<br>");
          if (hp <= 0 && !flag1)
          {
            stringBuilder2.Append(str4);
            if (num6 == 0)
              flag2 = true;
            flag1 = true;
          }
        }
      }
    }
    forThisCharacter[0] = num3.ToString();
    forThisCharacter[1] = num4.ToString();
    forThisCharacter[2] = !flag1 ? "0" : "1";
    if ((UnityEngine.Object) this.skull != (UnityEngine.Object) null)
    {
      if (forThisCharacter[2] == "1")
      {
        if (!this.skull.gameObject.activeSelf)
          this.skull.gameObject.SetActive(true);
        this.skullParticle.main.startColor = !flag2 ? (ParticleSystem.MinMaxGradient) Functions.HexToColor(str2 + "50") : (ParticleSystem.MinMaxGradient) Functions.HexToColor(str1 + "50");
      }
      else if (this.skull.gameObject.activeSelf)
        this.skull.gameObject.SetActive(false);
    }
    if (stringBuilder2.Length > 0)
    {
      stringBuilder2.Append("<line-height=160%><space=62><sprite name=sepwhite>");
      stringBuilder2.Append("<line-height=16><br><line-height=100%><size=-1.5>");
      stringBuilder2.Append("<sprite name=block>");
      stringBuilder2.Append(Texts.Instance.GetText("blocked"));
      stringBuilder2.Append("   <sprite name=ui_resistance> ");
      stringBuilder2.Append(Texts.Instance.GetText("resisted"));
      stringBuilder2.Append("   <sprite name=heart>");
      stringBuilder2.Append(Texts.Instance.GetText("currentHp"));
      stringBuilder2.Append("</size>");
      stringBuilder2.Insert(0, "<size=+2>");
      forThisCharacter.Add(stringBuilder2.ToString());
    }
    if (!MatchManager.Instance.prePostDamageDictionary.ContainsKey(_characterCaster.Id))
      MatchManager.Instance.prePostDamageDictionary.Add(_characterCaster.Id, forThisCharacter);
    else
      MatchManager.Instance.prePostDamageDictionary[_characterCaster.Id] = forThisCharacter;
    return forThisCharacter;
  }

  public void DeleteShadow(GameObject GO)
  {
    foreach (Transform transform in GO.transform)
    {
      if ((bool) (UnityEngine.Object) transform.GetComponent<SpriteRenderer>() && transform.gameObject.name.ToLower() == "shadow")
      {
        if (!transform.gameObject.activeSelf)
          break;
        transform.gameObject.SetActive(false);
        break;
      }
    }
  }

  public void GetSpritesFromAnimated(GameObject GO)
  {
    foreach (Transform transform in GO.transform)
    {
      if ((bool) (UnityEngine.Object) transform.GetComponent<SpriteRenderer>())
      {
        this.animatedSprites.Add(transform.GetComponent<SpriteRenderer>());
        if (!this.animatedSpritesDefaultMaterial.ContainsKey(transform.name))
          this.animatedSpritesDefaultMaterial.Add(transform.name, transform.GetComponent<SpriteRenderer>().sharedMaterial);
        if (transform.gameObject.name.ToLower() == "shadow")
          this.shadowSprite = transform;
      }
      if ((UnityEngine.Object) transform.GetComponent<SetSpriteLayerFromBase>() != (UnityEngine.Object) null)
        this.animatedSpritesOutOfCharacter.Add(transform.GetComponent<SetSpriteLayerFromBase>());
      if (transform.childCount > 0)
        this.GetSpritesFromAnimated(transform.gameObject);
    }
    this.charImageT = GO.transform;
  }

  public void SetOverDebuff(string msg = "")
  {
    if ((UnityEngine.Object) this.overDebuff == (UnityEngine.Object) null)
      return;
    if (msg == "")
    {
      if (!this.overDebuff.gameObject.activeSelf)
        return;
      this.overDebuff.gameObject.SetActive(false);
    }
    else
    {
      this.overDebuff.text = msg;
      if (this.overDebuff.gameObject.activeSelf)
        return;
      this.overDebuff.gameObject.SetActive(true);
    }
  }

  public void SetDoomIcon()
  {
    if (this._hero == null)
      return;
    int _charges = this._hero.EffectCharges("doom");
    if (_charges > 0)
    {
      if (!this.hpDoomIconT.gameObject.activeSelf)
        this.hpDoomIconT.gameObject.SetActive(true);
      this.hpDoomText.text = _charges.ToString();
      this.hpDoomIconT.GetComponent<PopupAuraCurse>().SetAuraCurse(Globals.Instance.GetAuraCurseData("doom"), _charges, true);
    }
    else
    {
      if (!this.hpDoomIconT.gameObject.activeSelf)
        return;
      this.hpDoomIconT.gameObject.SetActive(false);
    }
  }

  public bool IsItemStealth()
  {
    if (this.animatedSprites != null)
    {
      for (int index = 0; index < this.animatedSprites.Count; ++index)
      {
        if (!(bool) (UnityEngine.Object) this.animatedSprites[index].transform.GetComponent("StealthHide"))
          return this.animatedSprites[index].sharedMaterial.name.Split(' ', StringSplitOptions.None)[0] == "stealth";
      }
    }
    return false;
  }

  public bool IsItemTaunt()
  {
    if (this.animatedSprites != null)
    {
      for (int index = 0; index < this.animatedSprites.Count; ++index)
      {
        if (!(bool) (UnityEngine.Object) this.animatedSprites[index].transform.GetComponent("StealthHide"))
          return this.animatedSprites[index].sharedMaterial.name.Split(' ', StringSplitOptions.None)[0] == "taunt";
      }
    }
    return false;
  }

  public bool IsItemParalyzed()
  {
    if (this.animatedSprites != null)
    {
      for (int index = 0; index < this.animatedSprites.Count; ++index)
      {
        if (!(bool) (UnityEngine.Object) this.animatedSprites[index].transform.GetComponent("StealthHide"))
          return (UnityEngine.Object) this.animatedSprites[index].sharedMaterial == (UnityEngine.Object) this.paralyzeMaterial;
      }
    }
    return false;
  }

  public void SetStealth(bool state)
  {
    if (state && this.IsItemStealth() || !state && !this.IsItemStealth() || this.animatedSprites == null || this.animatedSprites.Count <= 0)
      return;
    for (int index = 0; index < this.animatedSprites.Count; ++index)
    {
      if (state)
      {
        if ((bool) (UnityEngine.Object) this.animatedSprites[index].transform.GetComponent("StealthHide"))
        {
          if (this.animatedSprites[index].gameObject.activeSelf)
            this.animatedSprites[index].transform.gameObject.SetActive(false);
        }
        else
          this.animatedSprites[index].sharedMaterial = this.stealthMaterial;
      }
      else if ((bool) (UnityEngine.Object) this.animatedSprites[index].transform.GetComponent("StealthHide"))
      {
        if (!this.animatedSprites[index].gameObject.activeSelf)
          this.animatedSprites[index].transform.gameObject.SetActive(true);
      }
      else
        this.animatedSprites[index].sharedMaterial = this.animatedSpritesDefaultMaterial[this.animatedSprites[index].name];
    }
    if (state)
    {
      if ((UnityEngine.Object) this.shadowSprite != (UnityEngine.Object) null && this.shadowSprite.gameObject.activeSelf)
        this.shadowSprite.gameObject.SetActive(false);
      if (!((UnityEngine.Object) this.stealthParticle != (UnityEngine.Object) null) || this.stealthParticle.gameObject.activeSelf)
        return;
      this.stealthParticle.gameObject.SetActive(true);
    }
    else
    {
      if ((UnityEngine.Object) this.shadowSprite != (UnityEngine.Object) null && !this.shadowSprite.gameObject.activeSelf)
        this.shadowSprite.gameObject.SetActive(true);
      if (!((UnityEngine.Object) this.stealthParticle != (UnityEngine.Object) null) || !this.stealthParticle.gameObject.activeSelf)
        return;
      this.stealthParticle.gameObject.SetActive(false);
    }
  }

  public void SetTaunt(bool state)
  {
    if (state && this.IsItemTaunt() || !state && !this.IsItemTaunt())
      return;
    if (this.animatedSprites != null && this.animatedSprites.Count > 0)
    {
      for (int index = 0; index < this.animatedSprites.Count; ++index)
      {
        if (state)
          this.animatedSprites[index].sharedMaterial = this.tauntMaterial;
        else
          this.animatedSprites[index].sharedMaterial = this.animatedSpritesDefaultMaterial[this.animatedSprites[index].name];
      }
    }
    else if (state)
      this.charImageSR.sharedMaterial = this.tauntMaterial;
    else
      this.charImageSR.sharedMaterial = this.animatedSpritesDefaultMaterial[this.charImageSR.name];
  }

  public void SetParalyze(bool state)
  {
    if (state && this.IsItemParalyzed())
      return;
    if (!state && !this.IsItemParalyzed())
    {
      if ((UnityEngine.Object) this.anim != (UnityEngine.Object) null)
        this.anim.speed = 1f;
      if (this.IsItemStealth() || this.IsItemTaunt())
        return;
    }
    if (this.animatedSprites != null && this.animatedSprites.Count > 0)
    {
      if (state && (UnityEngine.Object) this.animatedSprites[0].sharedMaterial == (UnityEngine.Object) this.paralyzeMaterial || !state && (UnityEngine.Object) this.animatedSprites[0].sharedMaterial == (UnityEngine.Object) this.animatedSpritesDefaultMaterial[this.animatedSprites[0].name])
        return;
      if (state)
      {
        if ((double) this.anim.speed > 0.0)
        {
          this.anim.SetTrigger("hit");
          this.StartCoroutine(this.StopAnim());
        }
      }
      else
        this.anim.speed = 1f;
      for (int index = 0; index < this.animatedSprites.Count; ++index)
      {
        if (state)
        {
          if ((bool) (UnityEngine.Object) this.animatedSprites[index].transform.GetComponent("StealthHide"))
          {
            if (this.animatedSprites[index].gameObject.activeSelf)
              this.animatedSprites[index].transform.gameObject.SetActive(false);
          }
          else
            this.animatedSprites[index].sharedMaterial = this.paralyzeMaterial;
        }
        else if ((bool) (UnityEngine.Object) this.animatedSprites[index].transform.GetComponent("StealthHide"))
        {
          if (!this.animatedSprites[index].gameObject.activeSelf)
            this.animatedSprites[index].transform.gameObject.SetActive(true);
        }
        else
          this.animatedSprites[index].sharedMaterial = this.animatedSpritesDefaultMaterial[this.animatedSprites[index].name];
      }
    }
    else if (state)
      this.charImageSR.sharedMaterial = this.paralyzeMaterial;
    else
      this.charImageSR.sharedMaterial = this.animatedSpritesDefaultMaterial[this.charImageSR.name];
    if (state || !((UnityEngine.Object) this.shadowSprite != (UnityEngine.Object) null) || this.shadowSprite.gameObject.activeSelf)
      return;
    this.shadowSprite.gameObject.SetActive(true);
  }

  private IEnumerator StopAnim()
  {
    if ((double) this.anim.speed != 0.0)
    {
      for (int i = 0; i < 100; ++i)
      {
        yield return (object) null;
        this.anim.speed *= 0.92f;
        if ((double) this.anim.speed < 0.0099999997764825821)
        {
          this.anim.speed = 0.0f;
          break;
        }
      }
    }
  }

  public void SetDamagePreview(
    bool state,
    int dmg = 0,
    string dmgType = "",
    int dmg2 = 0,
    string dmgType2 = "",
    int heal = 0,
    int blocked = 0,
    CardData _cardData = null)
  {
    if ((UnityEngine.Object) this.dmgPreviewText == (UnityEngine.Object) null)
      return;
    if (!state)
    {
      if (this.dmgPreviewText.transform.gameObject.activeSelf)
        this.dmgPreviewText.transform.gameObject.SetActive(false);
      if (this.purgedispel.transform.gameObject.activeSelf)
        this.purgedispel.transform.gameObject.SetActive(false);
      this.AmplifyBuffs((List<string>) null);
      if (this._npc == null)
        return;
      this.ShowTauntText(false);
    }
    else
    {
      Character character = (Character) null;
      if (this._npc != null)
      {
        character = (Character) this._npc;
        if ((UnityEngine.Object) _cardData != (UnityEngine.Object) null && _cardData.TargetSide != Enums.CardTargetSide.Anyone && _cardData.TargetSide != Enums.CardTargetSide.Enemy)
          return;
      }
      else if (this._hero != null)
      {
        character = (Character) this._hero;
        if ((UnityEngine.Object) _cardData != (UnityEngine.Object) null && _cardData.TargetSide == Enums.CardTargetSide.Enemy)
          return;
      }
      if (character == null)
        return;
      bool flag = false;
      int num = heal;
      if (this._hero != null)
      {
        Hero heroHeroActive = MatchManager.Instance.GetHeroHeroActive();
        if (heroHeroActive != null && heroHeroActive.Id == character.Id)
          flag = true;
        else if ((UnityEngine.Object) _cardData != (UnityEngine.Object) null && _cardData.TargetSide == Enums.CardTargetSide.Self)
          return;
        int hpLeftForMax = this._hero.GetHpLeftForMax();
        if (hpLeftForMax < heal)
          heal = hpLeftForMax;
      }
      else
      {
        int hpLeftForMax = this._npc.GetHpLeftForMax();
        if (hpLeftForMax < heal)
          heal = hpLeftForMax;
      }
      if (this._npc != null)
        this.ShowTauntText(false);
      if (!state)
        return;
      StringBuilder stringBuilder1 = new StringBuilder();
      if ((UnityEngine.Object) _cardData != (UnityEngine.Object) null && ((UnityEngine.Object) _cardData.Aura != (UnityEngine.Object) null || _cardData.HealCurses > 0 || _cardData.DispelAuras > 0 || _cardData.StealAuras > 0 || (UnityEngine.Object) _cardData.HealAuraCurseSelf != (UnityEngine.Object) null || (UnityEngine.Object) _cardData.HealAuraCurseName != (UnityEngine.Object) null || (UnityEngine.Object) _cardData.HealAuraCurseName2 != (UnityEngine.Object) null || (UnityEngine.Object) _cardData.HealAuraCurseName3 != (UnityEngine.Object) null || (UnityEngine.Object) _cardData.HealAuraCurseName4 != (UnityEngine.Object) null || (UnityEngine.Object) _cardData.Curse != (UnityEngine.Object) null))
      {
        int healCurses = _cardData.HealCurses;
        int dispelAuras = _cardData.DispelAuras;
        int stealAuras = _cardData.StealAuras;
        List<string> stringList = new List<string>();
        if (healCurses > 0)
          stringList = character.GetAuraCurseByOrder(1, healCurses, true);
        else if (dispelAuras > 0)
          stringList = character.GetAuraCurseByOrder(0, dispelAuras, true);
        else if (stealAuras > 0)
          stringList = character.GetAuraCurseByOrder(0, stealAuras, true);
        if ((UnityEngine.Object) _cardData.HealAuraCurseSelf != (UnityEngine.Object) null && flag && character.HasEffect(_cardData.HealAuraCurseSelf.Id) && character.EffectCharges(_cardData.HealAuraCurseSelf.Id) > 0)
          stringList.Add(_cardData.HealAuraCurseSelf.Id);
        if ((UnityEngine.Object) _cardData.HealAuraCurseName != (UnityEngine.Object) null && character.HasEffect(_cardData.HealAuraCurseName.Id) && character.EffectCharges(_cardData.HealAuraCurseName.Id) > 0)
          stringList.Add(_cardData.HealAuraCurseName.Id);
        if ((UnityEngine.Object) _cardData.HealAuraCurseName2 != (UnityEngine.Object) null && character.HasEffect(_cardData.HealAuraCurseName2.Id) && character.EffectCharges(_cardData.HealAuraCurseName2.Id) > 0)
          stringList.Add(_cardData.HealAuraCurseName2.Id);
        if ((UnityEngine.Object) _cardData.HealAuraCurseName3 != (UnityEngine.Object) null && character.HasEffect(_cardData.HealAuraCurseName3.Id) && character.EffectCharges(_cardData.HealAuraCurseName3.Id) > 0)
          stringList.Add(_cardData.HealAuraCurseName3.Id);
        if ((UnityEngine.Object) _cardData.HealAuraCurseName4 != (UnityEngine.Object) null && character.HasEffect(_cardData.HealAuraCurseName4.Id) && character.EffectCharges(_cardData.HealAuraCurseName4.Id) > 0)
          stringList.Add(_cardData.HealAuraCurseName4.Id);
        if (stringList.Count > 0)
        {
          StringBuilder stringBuilder2 = new StringBuilder();
          StringBuilder stringBuilder3 = new StringBuilder();
          for (int index = 0; index < stringList.Count; ++index)
          {
            stringBuilder2.Append("<sprite name=");
            stringBuilder2.Append(stringList[index]);
            stringBuilder2.Append(">");
            if (stringBuilder3.Length > 0)
              stringBuilder3.Append("<space=2.5>");
            stringBuilder3.Append(character.GetAuraCharges(stringList[index]));
          }
          if (stringBuilder2.Length > 0)
          {
            this.purgedispel.text = stringBuilder2.ToString();
            this.purgedispelQuantity.text = stringBuilder3.ToString();
            if (!this.purgedispel.gameObject.activeSelf)
              this.purgedispel.transform.gameObject.SetActive(true);
            this.purgedispelTitle.text = stealAuras <= 0 ? Texts.Instance.GetText("remove") : Texts.Instance.GetText("steal");
          }
        }
        if ((UnityEngine.Object) _cardData.Aura != (UnityEngine.Object) null || (UnityEngine.Object) _cardData.Aura2 != (UnityEngine.Object) null || (UnityEngine.Object) _cardData.Aura3 != (UnityEngine.Object) null || (UnityEngine.Object) _cardData.Curse != (UnityEngine.Object) null || (UnityEngine.Object) _cardData.Curse2 != (UnityEngine.Object) null || (UnityEngine.Object) _cardData.Curse3 != (UnityEngine.Object) null)
        {
          List<string> _listAuraCurse = new List<string>();
          if ((UnityEngine.Object) _cardData.Aura != (UnityEngine.Object) null)
            _listAuraCurse.Add(_cardData.Aura.Id.ToLower());
          if ((UnityEngine.Object) _cardData.Aura2 != (UnityEngine.Object) null)
            _listAuraCurse.Add(_cardData.Aura2.Id.ToLower());
          if ((UnityEngine.Object) _cardData.Aura3 != (UnityEngine.Object) null)
            _listAuraCurse.Add(_cardData.Aura3.Id.ToLower());
          if ((UnityEngine.Object) _cardData.Curse != (UnityEngine.Object) null)
            _listAuraCurse.Add(_cardData.Curse.Id.ToLower());
          if ((UnityEngine.Object) _cardData.Curse2 != (UnityEngine.Object) null)
            _listAuraCurse.Add(_cardData.Curse2.Id.ToLower());
          if ((UnityEngine.Object) _cardData.Curse3 != (UnityEngine.Object) null)
            _listAuraCurse.Add(_cardData.Curse3.Id.ToLower());
          this.AmplifyBuffs(_listAuraCurse);
        }
      }
      if ((UnityEngine.Object) _cardData != (UnityEngine.Object) null)
      {
        List<string> stringList = character.CharacterImmunitiesList();
        StringBuilder stringBuilder4 = new StringBuilder();
        if ((UnityEngine.Object) _cardData.Curse != (UnityEngine.Object) null && stringList.Contains(_cardData.Curse.Id))
        {
          stringBuilder4.Append("<size=4><sprite name=");
          stringBuilder4.Append(_cardData.Curse.Id);
          stringBuilder4.Append("></size><size=-1><color=#e88>");
          stringBuilder4.Append(Texts.Instance.GetText("immune"));
          stringBuilder4.Append("</color></size><br>");
        }
        if ((UnityEngine.Object) _cardData.Curse2 != (UnityEngine.Object) null && stringList.Contains(_cardData.Curse2.Id))
        {
          stringBuilder4.Append("<size=4><sprite name=");
          stringBuilder4.Append(_cardData.Curse2.Id);
          stringBuilder4.Append("></size><size=-1><color=#e88>");
          stringBuilder4.Append(Texts.Instance.GetText("immune"));
          stringBuilder4.Append("</color></size><br>");
        }
        if ((UnityEngine.Object) _cardData.Curse3 != (UnityEngine.Object) null && stringList.Contains(_cardData.Curse3.Id))
        {
          stringBuilder4.Append("<size=4><sprite name=");
          stringBuilder4.Append(_cardData.Curse3.Id);
          stringBuilder4.Append("></size><size=-1><color=#e88>");
          stringBuilder4.Append(Texts.Instance.GetText("immune"));
          stringBuilder4.Append("</color></size><br>");
        }
        if (stringBuilder4.Length > 0)
          stringBuilder1.Append(stringBuilder4.ToString());
      }
      if (heal == 0 && this._npc != null && this._npc.HasEffect("evasion"))
      {
        stringBuilder1.Append("<sprite name=evasion><color=#559F2B>");
        stringBuilder1.Append(Functions.Substring(Texts.Instance.GetText("evasion"), 5));
      }
      else if (heal == 0 && this._npc != null && this._npc.HasEffect("invulnerable"))
      {
        stringBuilder1.Append("<sprite name=invulnerable><color=#DECD02>");
        stringBuilder1.Append(Functions.Substring(Texts.Instance.GetText("invulnerable"), 5));
      }
      else if (dmg >= 1 && dmgType != "" && dmgType != "heart" && character.HasEffect("evasion"))
      {
        stringBuilder1.Append("<sprite name=evasion><color=#559F2B>");
        stringBuilder1.Append(Functions.Substring(Texts.Instance.GetText("evasion"), 5));
      }
      else
      {
        if ((dmg >= 1 || dmg2 >= 1) && blocked > 0)
        {
          stringBuilder1.Append(blocked);
          stringBuilder1.Append(" <sprite name=block>");
        }
        if (dmg > 0)
        {
          stringBuilder1.Append(dmg);
          if (dmgType != "")
          {
            stringBuilder1.Append(" <sprite name=");
            stringBuilder1.Append(dmgType);
            stringBuilder1.Append(">");
          }
        }
        if (dmg2 > 0)
        {
          stringBuilder1.Append("\n");
          stringBuilder1.Append(dmg2);
          if (dmgType2 != "")
          {
            stringBuilder1.Append(" <sprite name=");
            stringBuilder1.Append(dmgType2);
            stringBuilder1.Append(">");
          }
        }
        if (dmg < 1 && dmg2 < 1 && blocked > 0)
        {
          stringBuilder1.Append(blocked);
          stringBuilder1.Append(" <sprite name=block>");
        }
        if (stringBuilder1.Length > 0 && this._npc != null && this._npc.HasEffect("thorns"))
        {
          stringBuilder1.Insert(0, "<sprite name=thorns><br>");
          stringBuilder1.Insert(0, "</color> ");
          stringBuilder1.Insert(0, this._npc.EffectCharges("thorns"));
          stringBuilder1.Insert(0, "<color=#E59F40>");
        }
      }
      if (heal > 0 || num > 0)
      {
        if (stringBuilder1.Length > 0)
          stringBuilder1.Append("\n");
        stringBuilder1.Append(heal);
        stringBuilder1.Append(" <sprite name=heal>");
      }
      if (stringBuilder1.Length == 0 && dmg <= 0 && (UnityEngine.Object) _cardData != (UnityEngine.Object) null && (_cardData.Damage > 0 && !flag || _cardData.DamageSelf > 0 & flag))
        stringBuilder1.Append(0);
      if (!this.dmgPreviewText.transform.gameObject.activeSelf)
        this.dmgPreviewText.transform.gameObject.SetActive(true);
      if (stringBuilder1.Length > 0)
        this.dmgPreviewText.text = stringBuilder1.ToString();
      else
        this.dmgPreviewText.transform.gameObject.SetActive(false);
      if (this._npc == null || !this._npc.IsTaunted())
        return;
      this.ShowTauntText(true);
    }
  }

  private void ShowThornsText(bool state)
  {
    if ((UnityEngine.Object) this.thornsTransform == (UnityEngine.Object) null || this.thornsTransform.gameObject.activeSelf == state)
      return;
    this.thornsTransform.gameObject.SetActive(state);
  }

  private void ShowTauntText(bool state)
  {
    if ((UnityEngine.Object) this.tauntTextTransform == (UnityEngine.Object) null)
      return;
    if (this.tauntTextTransform.gameObject.activeSelf != state)
      this.tauntTextTransform.gameObject.SetActive(state);
    if (!state)
      return;
    this.tauntTextT.text = "<size=+.5><sprite name=taunt></size>" + Texts.Instance.GetText("taunt");
  }

  public void DisableCollider() => this.GetComponent<BoxCollider2D>().enabled = false;

  public void DrawOrderSprites(bool goToFront, int _order)
  {
    if (!this.isHero)
    {
      string str = this.gameObject.name.Trim();
      if (str.StartsWith("flamethrower_"))
      {
        _order = 3;
        goToFront = false;
      }
      else if (str.StartsWith("dwarfface_"))
      {
        _order = 0;
        goToFront = false;
      }
      else if (str.StartsWith("dt800_"))
      {
        _order = 2;
        goToFront = false;
      }
      else if (str.StartsWith("launcher_"))
      {
        _order = 1;
        goToFront = false;
      }
    }
    int num;
    if (goToFront)
    {
      num = -600;
      if ((UnityEngine.Object) this.PetItem != (UnityEngine.Object) null)
      {
        if (this.PetItemFront)
          this.PetItem.DrawOrderSprites(false, -3);
        else
          this.PetItem.DrawOrderSprites(false, -1);
      }
      if ((UnityEngine.Object) this.PetItemEnchantment != (UnityEngine.Object) null)
      {
        if (this.PetItemEnchantmentFront)
          this.PetItemEnchantment.DrawOrderSprites(false, _order + 1000);
        else
          this.PetItemEnchantment.DrawOrderSprites(false, _order - 1000 + 2);
      }
    }
    else
    {
      num = 40 * (8 - _order) - 1000;
      if ((UnityEngine.Object) this.PetItem != (UnityEngine.Object) null)
      {
        if (this.PetItemFront)
          this.PetItem.DrawOrderSprites(false, _order - 1);
        else
          this.PetItem.DrawOrderSprites(false, _order - 1000 + 1);
      }
      if ((UnityEngine.Object) this.PetItemEnchantment != (UnityEngine.Object) null)
      {
        if (this.PetItemEnchantmentFront)
          this.PetItemEnchantment.DrawOrderSprites(false, _order + 1000);
        else
          this.PetItemEnchantment.DrawOrderSprites(false, _order - 1000 + 2);
      }
    }
    if (this.animatedSprites != null)
    {
      for (int index = 0; index < this.animatedSprites.Count; ++index)
      {
        if ((UnityEngine.Object) this.animatedSprites[index] != (UnityEngine.Object) null)
        {
          this.animatedSprites[index].sortingOrder = num - index;
          this.animatedSprites[index].sortingLayerName = "Characters";
        }
      }
    }
    else
      this.charImageSR.sortingOrder = num;
    if (this.animatedSpritesOutOfCharacter == null)
      return;
    for (int index = 0; index < this.animatedSpritesOutOfCharacter.Count; ++index)
    {
      if ((UnityEngine.Object) this.animatedSpritesOutOfCharacter[index] != (UnityEngine.Object) null)
        this.animatedSpritesOutOfCharacter[index].ReOrderLayer();
    }
  }

  public void DrawBlock(bool state)
  {
    if (this.blockCo != null)
      this.StopCoroutine(this.blockCo);
    if (state)
      this.blockCo = this.StartCoroutine(this.ShowBlock(true));
    else
      this.blockCo = this.StartCoroutine(this.ShowBlock(false));
  }

  private IEnumerator ShowBlock(bool state)
  {
    CharacterItem characterItem = this;
    float scaleMax = 1f;
    if (characterItem._npc != null && (UnityEngine.Object) characterItem._npc.NpcData != (UnityEngine.Object) null && characterItem._npc.NpcData.BigModel)
      scaleMax = 1.5f;
    if (characterItem.hpBlockIconT.gameObject.activeSelf != state || characterItem.hpBlockT.gameObject.activeSelf != state)
    {
      characterItem.hpBlockT.gameObject.SetActive(false);
      if (state)
      {
        characterItem.hpBlockIconT.gameObject.SetActive(true);
        float scale = 0.0f;
        while ((double) scale < (double) scaleMax + 0.25 && !((UnityEngine.Object) characterItem.hpBlockIconT == (UnityEngine.Object) null) && !((UnityEngine.Object) characterItem.hpBlockIconT.gameObject == (UnityEngine.Object) null))
        {
          characterItem.hpBlockIconT.localScale = new Vector3(scale, scale, 1f);
          scale += 0.1f;
          yield return (object) Globals.Instance.WaitForSeconds(0.01f);
        }
        while ((double) scale > (double) scaleMax && !((UnityEngine.Object) characterItem.hpBlockIconT == (UnityEngine.Object) null) && !((UnityEngine.Object) characterItem.hpBlockIconT.gameObject == (UnityEngine.Object) null))
        {
          characterItem.hpBlockIconT.localScale = new Vector3(scale, scale, 1f);
          scale -= 0.1f;
          yield return (object) Globals.Instance.WaitForSeconds(0.005f);
        }
        if ((UnityEngine.Object) characterItem.hpBlockIconT == (UnityEngine.Object) null || (UnityEngine.Object) characterItem.hpBlockIconT.gameObject == (UnityEngine.Object) null)
          characterItem.hpBlockIconT.localScale = new Vector3(scaleMax, scaleMax, 1f);
      }
      else
      {
        characterItem.blockBorderT.gameObject.SetActive(false);
        characterItem.hpBlockIconT.localScale = new Vector3(0.0f, 0.0f, 1f);
        characterItem.hpBlockIconT.gameObject.SetActive(false);
      }
      characterItem.StartCoroutine(characterItem.ShowBlockHP(state));
    }
  }

  private IEnumerator ShowBlockHP(bool state, bool animation = true)
  {
    if (state)
    {
      float percent = this.hpRed.localScale.x;
      if ((double) percent > 1.0)
        percent = 1f;
      else if ((double) percent < 0.0)
        percent = 0.0f;
      if (animation)
      {
        float t = 0.0f;
        float seconds = 0.5f;
        this.hpBlockT.localScale = Vector3.zero;
        if (!this.hpBlockT.gameObject.activeSelf)
          this.hpBlockT.gameObject.SetActive(true);
        this.blockBorderT.localScale = Vector3.zero;
        if (!this.blockBorderT.gameObject.activeSelf)
          this.blockBorderT.gameObject.SetActive(true);
        while ((double) t <= 1.0)
        {
          t += Time.deltaTime / seconds;
          this.hpBlockT.localScale = Vector3.Lerp(new Vector3(0.0f, 1f, 1f), new Vector3(percent, 1f, 1f), Mathf.SmoothStep(0.0f, 1f, t));
          this.blockBorderT.localScale = Vector3.Lerp(new Vector3(0.0f, 1f, 1f), new Vector3(1f, 1f, 1f), Mathf.SmoothStep(0.0f, 1f, t));
          yield return (object) null;
        }
      }
      else
      {
        this.hpBlockT.localScale = new Vector3(percent, this.hpBlockT.localScale.y, this.hpBlockT.localScale.z);
        if (!this.hpBlockT.gameObject.activeSelf)
          this.hpBlockT.gameObject.SetActive(true);
      }
    }
    else
    {
      if (this.hpBlockT.gameObject.activeSelf)
        this.hpBlockT.gameObject.SetActive(false);
      if (this.blockBorderT.gameObject.activeSelf)
        this.blockBorderT.gameObject.SetActive(false);
    }
  }

  private IEnumerator ShowShieldHP(int charges)
  {
    if (!((UnityEngine.Object) this.hpShieldT == (UnityEngine.Object) null))
    {
      this.hpShieldText.text = charges.ToString();
      if (!this.hpShieldT.gameObject.activeSelf)
      {
        float scaleMax = 1.1f;
        if (this._npc != null && (UnityEngine.Object) this._npc.NpcData != (UnityEngine.Object) null && this._npc.NpcData.BigModel)
          scaleMax = 1.55f;
        float t = 0.0f;
        float seconds = 0.5f;
        this.hpShieldT.localScale = Vector3.zero;
        if (!this.hpShieldT.gameObject.activeSelf)
          this.hpShieldT.gameObject.SetActive(true);
        while ((double) t <= 1.0)
        {
          t += Time.deltaTime / seconds;
          this.hpShieldT.localScale = Vector3.Lerp(new Vector3(0.0f, 0.0f, 1f), new Vector3(scaleMax + 0.1f, scaleMax + 0.1f, 1f), Mathf.SmoothStep(0.0f, 1f, t));
          yield return (object) null;
        }
        this.hpShieldT.localScale = new Vector3(scaleMax, scaleMax, 1f);
      }
      yield return (object) null;
    }
  }

  public void DrawEnergy()
  {
    if (!this.isHero)
      return;
    int energy;
    int energyTurn;
    if (this.isHero)
    {
      energy = this._hero.GetEnergy();
      energyTurn = this._hero.GetEnergyTurn();
    }
    else
    {
      energy = this._npc.GetEnergy();
      energyTurn = this._npc.GetEnergyTurn();
    }
    this.energyTxt.text = energy.ToString();
    for (int index = 0; index < 10; ++index)
    {
      if (!((UnityEngine.Object) this.energySR[index] == (UnityEngine.Object) null))
      {
        if (index < energy)
        {
          this.energySR[index].color = Color.white;
          this.energySRAnimator[index].enabled = false;
        }
        else if (index < energy + energyTurn)
        {
          this.energySR[index].gameObject.SetActive(false);
          this.energySR[index].gameObject.SetActive(true);
          this.energySRAnimator[index].enabled = true;
          this.energySR[index].color = new Color(0.0f, 1f, 0.5f, 1f);
        }
        else
        {
          this.energySR[index].color = new Color(0.0f, 0.0f, 0.0f, 0.45f);
          this.energySRAnimator[index].enabled = false;
        }
      }
    }
  }

  public void ScrollCombatText(string text, Enums.CombatScrollEffectType type) => this.CT.SetText(text, type);

  public void ScrollCombatTextDamageNew(CastResolutionForCombatText _cast) => this.CT.SetDamageNew(_cast);

  public bool IsCombatScrollEffectActive() => this.CT.IsPlaying();

  public void ActivateMark(bool state)
  {
    if (!((UnityEngine.Object) this.transform != (UnityEngine.Object) null))
      return;
    this.isActive = state;
    if (!((UnityEngine.Object) this.activeMarkTR != (UnityEngine.Object) null) || this.activeMarkTR.gameObject.activeSelf == state)
      return;
    this.activeMarkTR.gameObject.SetActive(state);
  }

  public bool AnimNameIs(string name) => !((UnityEngine.Object) this.anim == (UnityEngine.Object) null) && this.anim.GetCurrentAnimatorStateInfo(0).IsName(name);

  public void CharacterAttackAnim()
  {
    if (!((UnityEngine.Object) this.anim != (UnityEngine.Object) null))
      return;
    if (this.animationBusyCo != null)
      this.StopCoroutine(this.animationBusyCo);
    this.animationBusyCo = this.StartCoroutine(this.SetAnimationBusy());
    this.anim.ResetTrigger("attack");
    this.anim.SetTrigger("attack");
    this.PetCastAnim("cast");
  }

  public void CharacterCastAnim()
  {
    if (!((UnityEngine.Object) this.anim != (UnityEngine.Object) null))
      return;
    if (this.animationBusyCo != null)
      this.StopCoroutine(this.animationBusyCo);
    this.animationBusyCo = this.StartCoroutine(this.SetAnimationBusy());
    this.anim.ResetTrigger("cast");
    this.anim.SetTrigger("cast");
    this.PetCastAnim("cast");
  }

  private IEnumerator SetAnimationBusy()
  {
    this.animationBusy = true;
    yield return (object) Globals.Instance.WaitForSeconds(1f);
    this.animationBusy = false;
  }

  private IEnumerator PetCastAnimTO(string animState)
  {
    CharacterItem characterItem = this;
    List<Animator> petAnimators = new List<Animator>();
    for (int index = 0; index < 3; ++index)
    {
      Transform transform = characterItem.transform.Find("thePetEnchantment" + index.ToString());
      if ((UnityEngine.Object) transform != (UnityEngine.Object) null && (UnityEngine.Object) transform.GetComponent<Animator>() != (UnityEngine.Object) null)
        petAnimators.Add(transform.GetComponent<Animator>());
    }
    for (int i = 0; i < petAnimators.Count; ++i)
    {
      if (animState != "hit")
        yield return (object) Globals.Instance.WaitForSeconds((float) UnityEngine.Random.Range(0, 30) * 0.01f);
      if ((UnityEngine.Object) petAnimators[i] != (UnityEngine.Object) null)
      {
        petAnimators[i].ResetTrigger(animState);
        petAnimators[i].SetTrigger(animState);
      }
    }
    yield return (object) null;
  }

  public void PetCastAnim(string animState)
  {
    if (!this.isHero)
    {
      this.StartCoroutine(this.PetCastAnimTO(animState));
    }
    else
    {
      if (!((UnityEngine.Object) this.animPet != (UnityEngine.Object) null))
        return;
      this.animPet.ResetTrigger(animState);
      this.animPet.SetTrigger(animState);
      if (!(animState == "attack"))
        return;
      this.StartCoroutine(this.PetMoveToCenter());
    }
  }

  private IEnumerator PetMoveToCenter()
  {
    if (!((UnityEngine.Object) this.PetItem == (UnityEngine.Object) null))
    {
      this.PetItem.MoveToCenter();
      yield return (object) Globals.Instance.WaitForSeconds(0.8f);
      this.PetItem.MoveToCenterBack();
    }
  }

  public void CharacterEnableAnim(bool state)
  {
    if (!((UnityEngine.Object) this.anim != (UnityEngine.Object) null))
      return;
    if (state)
      this.anim.enabled = true;
    else
      this.anim.enabled = false;
  }

  public void CharacterHitted(bool fromHit = false)
  {
    if (this.characterBeingHitted)
      return;
    if (this._hero != null)
    {
      if (this._hero.IsParalyzed())
      {
        GameManager.Instance.PlayLibraryAudio("hit_metal2");
        return;
      }
    }
    else if (this._npc != null)
    {
      if (this._npc.IsParalyzed())
      {
        GameManager.Instance.PlayLibraryAudio("hit_metal2");
        return;
      }
      if (fromHit && (UnityEngine.Object) this._npc.NpcData != (UnityEngine.Object) null)
      {
        string str = this._npc.NpcData.Id.ToLower().Split('_', StringSplitOptions.None)[0];
        if (str == "trunky" || str == "taintedtrunky" || str == "sapling" || str == "taintedsapling" || str == "ylmer")
          GameManager.Instance.PlayLibraryAudio("impact_wood");
        else if ((UnityEngine.Object) MatchManager.Instance.CardActive != (UnityEngine.Object) null)
        {
          if (MatchManager.Instance.CardActive.DamageType == Enums.DamageType.Slashing)
            GameManager.Instance.PlayLibraryAudio("impact_slashing");
          else if (MatchManager.Instance.CardActive.DamageType == Enums.DamageType.Blunt)
            GameManager.Instance.PlayLibraryAudio("impact_crushing");
          else if (MatchManager.Instance.CardActive.DamageType == Enums.DamageType.Piercing)
            GameManager.Instance.PlayLibraryAudio("impact_piercing");
        }
      }
    }
    this.characterBeingHitted = true;
    if ((UnityEngine.Object) this.anim != (UnityEngine.Object) null && !this.animationBusy)
    {
      this.anim.ResetTrigger("hit");
      this.anim.SetTrigger("hit");
      this.PetCastAnim("hit");
    }
    if (fromHit)
    {
      if (this._npc != null && (UnityEngine.Object) this._npc.NpcData != (UnityEngine.Object) null && (UnityEngine.Object) this._npc.NpcData.HitSound != (UnityEngine.Object) null)
        GameManager.Instance.PlayAudio(this._npc.NpcData.HitSound, 0.25f);
      if (this._hero != null && (UnityEngine.Object) this._hero.HeroData != (UnityEngine.Object) null && (UnityEngine.Object) this._hero.HeroData.HeroSubClass.HitSound != (UnityEngine.Object) null)
        GameManager.Instance.PlayAudio(this._hero.HeroData.HeroSubClass.HitSound, 0.25f);
    }
    if (this.hitCo != null)
      this.StopCoroutine(this.hitCo);
    this.hitCo = this.StartCoroutine(this.CharacterBeingHittedCo());
  }

  private IEnumerator CharacterBeingHittedCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.3f);
    this.characterBeingHitted = false;
  }

  public void MoveToCenter()
  {
    if ((UnityEngine.Object) this.charImageT == (UnityEngine.Object) null)
      return;
    if (this.moveCenterCo != null)
      this.StopCoroutine(this.moveCenterCo);
    if (this.moveBackCo != null)
      this.StopCoroutine(this.moveBackCo);
    this.moveCenterCo = this.StartCoroutine(this.SetPositionCO(this.charImageT, new Vector3(-this.charImageT.parent.transform.localPosition.x + this.charImageT.localPosition.x, this.charImageT.localPosition.y, 0.0f), false));
  }

  public void MoveToCenterBack()
  {
    if ((UnityEngine.Object) this.charImageT == (UnityEngine.Object) null || (double) Mathf.Abs(this.charImageT.localPosition.x - this.originalLocalPosition.x) <= 0.019999999552965164)
      return;
    if (this.moveCenterCo != null)
      this.StopCoroutine(this.moveCenterCo);
    if (this.moveBackCo != null)
      this.StopCoroutine(this.moveBackCo);
    this.moveBackCo = this.StartCoroutine(this.SetPositionCO(this.charImageT, new Vector3(this.originalLocalPosition.x, this.charImageT.localPosition.y, 0.0f), false));
  }

  public void SetOriginalLocalPosition(Vector3 pos) => this.originalLocalPosition = pos;

  public void SetPosition(bool instant, int _position = -10)
  {
    if (!instant && this._npc != null && this._npc.NpcData.IsBoss)
      return;
    float num1 = 2.4f;
    float y = 0.0f;
    float num2 = 1.9f;
    float x1 = 0.0f;
    int num3 = 0;
    if (this._hero != null)
    {
      num3 = _position != -10 ? _position : this._hero.Position;
      x1 = (float) (-(double) num1 - (double) num3 * (double) num2);
    }
    else if (this._npc != null)
    {
      num3 = _position != -10 ? _position : this._npc.Position;
      x1 = num1 + (float) num3 * num2;
      if (this._npc.IsBigModel())
        x1 += 0.5f * num2;
      if (this._npc.NPCIsBoss() && this._npc.Id.Contains("faebor"))
      {
        float x2 = 2.4f;
        this.healthBar.transform.localPosition = new Vector3(x2, this.healthBar.transform.localPosition.y, this.healthBar.transform.localPosition.z);
        Vector3 vector3 = new Vector3(x2, 0.0f, 0.0f);
        this.iconEnchantment.transform.position += vector3;
        this.iconEnchantment2.transform.position += vector3;
        this.iconEnchantment3.transform.position += vector3;
      }
    }
    if ((double) this.transform.localPosition.x == (double) x1)
      return;
    float z = (float) num3 * 1E-05f;
    if (!this.isHero)
    {
      string str = this.gameObject.name.Trim();
      if (str.StartsWith("flamethrower_"))
        z = 3E-05f;
      else if (str.StartsWith("dwarfface_"))
        z = 0.0f;
      else if (str.StartsWith("dt800_"))
        z = 2E-05f;
      else if (str.StartsWith("launcher_"))
        z = 1E-05f;
    }
    Vector3 vector3_1 = new Vector3(x1, y, z);
    if (!instant)
      return;
    this.transform.localPosition = vector3_1;
  }

  public bool CharIsMoving() => this.charIsMoving;

  private IEnumerator SetPositionCO(
    Transform theTransform,
    Vector3 vectorPosition,
    bool returnPosition)
  {
    this.charIsMoving = true;
    bool flag1 = false;
    if ((double) Mathf.Abs(theTransform.localPosition.x - vectorPosition.x) < 0.019999999552965164)
      flag1 = true;
    else if (this._hero != null && !this._hero.Alive)
      flag1 = true;
    else if (this._npc != null && !this._npc.Alive)
      flag1 = true;
    if (flag1)
    {
      this.charIsMoving = false;
    }
    else
    {
      bool flip = true;
      if ((double) theTransform.localPosition.x < (double) vectorPosition.x)
        flip = false;
      if (AtOManager.Instance.GetTownZoneId() == "Senenthia")
        GameManager.Instance.PlayLibraryAudio("walk_grass");
      else
        GameManager.Instance.PlayLibraryAudio("walk_stone");
      EffectsManager.Instance.PlayEffectAC("smoke", this.isHero, theTransform, flip);
      bool flag2 = false;
      if (!this.isActive)
        this.CharacterEnableAnim(false);
      if (!GameManager.Instance.IsMultiplayer() && GameManager.Instance.configGameSpeed == Enums.ConfigSpeed.Ultrafast)
      {
        if ((UnityEngine.Object) theTransform != (UnityEngine.Object) null)
          theTransform.localPosition = vectorPosition;
      }
      else
      {
        for (; !flag2 && (UnityEngine.Object) theTransform != (UnityEngine.Object) null; flag2 = true)
        {
          while ((UnityEngine.Object) theTransform != (UnityEngine.Object) null && (double) Mathf.Abs(theTransform.localPosition.x - vectorPosition.x) > 0.05000000074505806)
          {
            theTransform.localPosition = Vector3.Lerp(theTransform.localPosition, vectorPosition, 0.5f);
            yield return (object) Globals.Instance.WaitForSeconds(0.01f);
          }
          if ((UnityEngine.Object) theTransform != (UnityEngine.Object) null)
            theTransform.localPosition = vectorPosition;
        }
      }
      if (!this.isActive)
        this.CharacterEnableAnim(true);
      this.charIsMoving = false;
    }
  }

  public void SetHP()
  {
    Character character;
    float num1;
    float maxHp;
    int block;
    int auraCharges;
    if (this._hero != null)
    {
      character = (Character) this._hero;
      num1 = (float) this._hero.HpCurrent;
      maxHp = (float) this._hero.GetMaxHP();
      if ((double) num1 > (double) maxHp)
      {
        this._hero.HpCurrent = (int) maxHp;
        num1 = maxHp;
      }
      block = this._hero.GetBlock();
      auraCharges = this._hero.GetAuraCharges("shield");
    }
    else
    {
      if (this._npc == null)
        return;
      character = (Character) this._npc;
      num1 = (float) this._npc.HpCurrent;
      maxHp = (float) this._npc.GetMaxHP();
      if ((double) num1 > (double) maxHp)
      {
        this._npc.HpCurrent = (int) maxHp;
        num1 = maxHp;
      }
      block = this._npc.GetBlock();
      auraCharges = this._npc.GetAuraCharges("shield");
    }
    if (block > 0)
    {
      if ((UnityEngine.Object) this.hpBlockText != (UnityEngine.Object) null)
        this.hpBlockText.text = block.ToString();
      if (block > 49)
        PlayerManager.Instance.AchievementUnlock("CHARGES_DEFENDER");
      if (block > 199)
        PlayerManager.Instance.AchievementUnlock("CHARGES_FORTRESS");
      if ((UnityEngine.Object) this.hpBlockIconT != (UnityEngine.Object) null)
        this.hpBlockIconT.GetComponent<PopupAuraCurse>().SetAuraCurse(Globals.Instance.GetAuraCurseData("block"), block, true);
    }
    if ((UnityEngine.Object) this.hpShieldT != (UnityEngine.Object) null && (UnityEngine.Object) this.hpShieldT.gameObject != (UnityEngine.Object) null)
    {
      if (auraCharges > 0)
      {
        this.hpShieldT.GetComponent<PopupAuraCurse>().SetAuraCurse(Globals.Instance.GetAuraCurseData("shield"), auraCharges, true);
        this.StartCoroutine(this.ShowShieldHP(auraCharges));
      }
      else
      {
        this.hpShieldT.gameObject.SetActive(false);
        this.hpShieldText.text = "";
      }
    }
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(num1);
    stringBuilder.Append("<size=-.5><color=#bbb>/");
    stringBuilder.Append(maxHp);
    this.hpText.text = stringBuilder.ToString();
    if (character.GetHp() < 1)
    {
      if ((UnityEngine.Object) this.hpRed != (UnityEngine.Object) null)
        this.hpRed.localScale = new Vector3(0.0f, 1f, 1f);
      if ((UnityEngine.Object) this.hpBleed != (UnityEngine.Object) null)
        this.hpBleed.localScale = new Vector3(0.0f, 1f, 1f);
      if ((UnityEngine.Object) this.hpPoison != (UnityEngine.Object) null)
        this.hpPoison.localScale = new Vector3(0.0f, 1f, 1f);
      if ((UnityEngine.Object) this.hpRegen != (UnityEngine.Object) null)
        this.hpRegen.localScale = new Vector3(0.0f, 1f, 1f);
      if (this.isDying)
        return;
      this.KillCoroutine = this.StartCoroutine(this.KillCharacterCO());
    }
    else
    {
      MatchManager.Instance.CleanPrePostDamageDictionary(character.Id);
      this.CalculateDamagePrePostForThisCharacter();
      int num2 = int.Parse(MatchManager.Instance.prePostDamageDictionary[character.Id][0]);
      int num3 = int.Parse(MatchManager.Instance.prePostDamageDictionary[character.Id][1]);
      int num4 = 0;
      int num5;
      if (num2 < 0)
      {
        num5 = -1 * num2;
      }
      else
      {
        num4 = num2;
        num5 = 0;
      }
      int num6 = num3 >= 0 ? 0 : -1 * num3;
      float x1 = (num1 - (float) num5 - (float) num6) / maxHp;
      if ((double) x1 < 0.0)
        x1 = 0.0f;
      else if ((double) x1 > 1.0)
        x1 = 1f;
      if ((UnityEngine.Object) this.hpRed != (UnityEngine.Object) null)
        this.hpRed.localScale = new Vector3(x1, 1f, 1f);
      if ((UnityEngine.Object) this.hpBlockT != (UnityEngine.Object) null)
        this.hpBlockT.localScale = this.hpRed.localScale;
      float x2 = 0.0f;
      if (num5 > 0)
        x2 = num1 / maxHp;
      if ((UnityEngine.Object) this.hpBleed != (UnityEngine.Object) null)
        this.hpBleed.localScale = new Vector3(x2, 1f, 1f);
      if (num4 > 0 && (UnityEngine.Object) this.hpRegen != (UnityEngine.Object) null)
      {
        float x3 = (num1 + (float) num4) / maxHp;
        if ((double) x3 > 1.0)
          x3 = 1f;
        this.hpRegen.localScale = new Vector3(x3, 1f, 1f);
      }
      else if ((UnityEngine.Object) this.hpRegen != (UnityEngine.Object) null)
        this.hpRegen.localScale = Vector3.zero;
      float x4 = 0.0f;
      if (num6 > 0)
        x4 = (num1 - (float) num5) / maxHp;
      if ((double) x4 < 0.0)
        x4 = 0.0f;
      if (!((UnityEngine.Object) this.hpPoison != (UnityEngine.Object) null))
        return;
      this.hpPoison.localScale = new Vector3(x4, 1f, 1f);
    }
  }

  private void AmplifyBuffs(List<string> _listAuraCurse)
  {
    if (_listAuraCurse != null)
    {
      for (int index = 0; index < this.GoBuffs.Count; ++index)
      {
        if (_listAuraCurse.Contains(this.GoBuffs[index].buffId))
          this.GoBuffs[index].DisplayBecauseCard(true);
        else
          this.GoBuffs[index].DisplayBecauseCard(false);
      }
    }
    else
    {
      for (int index = 0; index < this.GoBuffs.Count; ++index)
        this.GoBuffs[index].RestoreBecauseCard();
    }
  }

  public void DrawBuffs(AuraCurseData auraIncluded = null, int auraIncludedCharges = 0, int previousCharges = -1)
  {
    if ((UnityEngine.Object) this.GO_Buffs == (UnityEngine.Object) null)
      return;
    if (this.drawBuffsCoroutine != null)
      this.StopCoroutine(this.drawBuffsCoroutine);
    this.drawBuffsCoroutine = this.StartCoroutine(this.DrawBuffsCo(auraIncluded, auraIncludedCharges, previousCharges));
  }

  private IEnumerator DrawBuffsCo(
    AuraCurseData auraIncluded = null,
    int auraIncludedCharges = 0,
    int previousCharges = -1)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    CharacterItem characterItem = this;
    if (num != 0)
      return false;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    if ((UnityEngine.Object) characterItem.GO_Buffs == (UnityEngine.Object) null)
      return false;
    int count;
    string id;
    if (characterItem.isHero)
    {
      count = characterItem._hero.AuraList.Count;
      id = characterItem._hero.Id;
    }
    else
    {
      count = characterItem._npc.AuraList.Count;
      id = characterItem._npc.Id;
    }
    int index1 = -1;
    for (int index2 = 0; index2 < count; ++index2)
    {
      if ((UnityEngine.Object) characterItem.GO_Buffs == (UnityEngine.Object) null)
        return false;
      Aura aura = !characterItem.isHero ? characterItem._npc.AuraList[index2] : characterItem._hero.AuraList[index2];
      if (aura != null && (UnityEngine.Object) aura.ACData != (UnityEngine.Object) null && aura.ACData.IconShow)
      {
        if ((UnityEngine.Object) characterItem.GO_Buffs == (UnityEngine.Object) null)
          return false;
        ++index1;
        Buff goBuff = characterItem.GoBuffs[index1];
        if ((UnityEngine.Object) goBuff != (UnityEngine.Object) null)
          goBuff.SetBuff(aura.ACData, aura.GetCharges(), _charId: id);
        if (!((UnityEngine.Object) goBuff == (UnityEngine.Object) null) && !((UnityEngine.Object) goBuff.gameObject == (UnityEngine.Object) null))
        {
          goBuff.gameObject.name = aura.ACData.Id;
          if ((UnityEngine.Object) auraIncluded != (UnityEngine.Object) null && goBuff.gameObject.name == auraIncluded.Id)
          {
            if (characterItem.buffAnimationList.ContainsKey(goBuff.gameObject.name))
              characterItem.buffAnimationList[goBuff.gameObject.name] += auraIncludedCharges;
            else
              characterItem.buffAnimationList.Add(goBuff.gameObject.name, auraIncludedCharges);
          }
          string lower = goBuff.name.ToLower();
          int charges = aura.GetCharges();
          switch (lower)
          {
            case "poison":
              if (charges > 49)
                PlayerManager.Instance.AchievementUnlock("CHARGES_POISONOUS");
              if (charges > 199)
              {
                PlayerManager.Instance.AchievementUnlock("CHARGES_TOXICDISASTER");
                continue;
              }
              continue;
            case "bleed":
              if (charges > 49)
                PlayerManager.Instance.AchievementUnlock("CHARGES_BLOODY");
              if (charges > 199)
              {
                PlayerManager.Instance.AchievementUnlock("CHARGES_BLOODBATH");
                continue;
              }
              continue;
            case "burn":
              if (charges > 49)
                PlayerManager.Instance.AchievementUnlock("CHARGES_INCENDIARY");
              if (charges > 199)
              {
                PlayerManager.Instance.AchievementUnlock("CHARGES_INFERNO");
                continue;
              }
              continue;
            case "crack":
              if (charges > 49)
                PlayerManager.Instance.AchievementUnlock("CHARGES_WRECKINGBALL");
              if (charges > 199)
              {
                PlayerManager.Instance.AchievementUnlock("CHARGES_SIEGEBREAKER");
                continue;
              }
              continue;
            case "fury":
              if (charges > 49)
                PlayerManager.Instance.AchievementUnlock("CHARGES_FURIOUS");
              if (charges > 199)
              {
                PlayerManager.Instance.AchievementUnlock("CHARGES_ENDLESSFURY");
                continue;
              }
              continue;
            case "sanctify":
              if (charges > 39)
              {
                PlayerManager.Instance.AchievementUnlock("CHARGES_HOLYGROUND");
                continue;
              }
              continue;
            case "bless":
              if (charges > 39)
              {
                PlayerManager.Instance.AchievementUnlock("CHARGES_DIVINE");
                continue;
              }
              continue;
            case "regeneration":
              if (charges > 39)
              {
                PlayerManager.Instance.AchievementUnlock("CHARGES_DRYAD");
                continue;
              }
              continue;
            case "thorns":
              if (charges > 39)
              {
                PlayerManager.Instance.AchievementUnlock("CHARGES_SPIKY");
                continue;
              }
              continue;
            case "chill":
              if (charges > 49)
                PlayerManager.Instance.AchievementUnlock("CHARGES_CHILLY");
              if (charges > 199)
              {
                PlayerManager.Instance.AchievementUnlock("CHARGES_GLACIER");
                continue;
              }
              continue;
            case "spark":
              if (charges > 49)
                PlayerManager.Instance.AchievementUnlock("CHARGES_SPARKLY");
              if (charges > 199)
              {
                PlayerManager.Instance.AchievementUnlock("CHARGES_ELECTROCUTER");
                continue;
              }
              continue;
            case "insane":
              if (charges > 39)
              {
                PlayerManager.Instance.AchievementUnlock("CHARGES_INSANE");
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
    }
    for (int index3 = index1 + 1; index3 < characterItem.GoBuffs.Count - 1; ++index3)
    {
      if ((UnityEngine.Object) characterItem.GoBuffs[index3] != (UnityEngine.Object) null && (UnityEngine.Object) characterItem.GoBuffs[index3].gameObject != (UnityEngine.Object) null)
        characterItem.GoBuffs[index3].CleanBuff();
    }
    if (characterItem.buffAnimationCo != null)
      characterItem.StopCoroutine(characterItem.buffAnimationCo);
    if (characterItem.buffAnimationList.Count > 0)
      characterItem.buffAnimationCo = characterItem.StartCoroutine(characterItem.BuffAnimationCoroutine());
    return false;
  }

  private IEnumerator BuffAnimationCoroutine()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    for (int index = 0; index < this.GoBuffs.Count; ++index)
    {
      if ((UnityEngine.Object) this.GoBuffs[index] != (UnityEngine.Object) null && (UnityEngine.Object) this.GoBuffs[index].gameObject != (UnityEngine.Object) null && this.GoBuffs[index].gameObject.name != "" && this.buffAnimationList != null && this.buffAnimationList.ContainsKey(this.GoBuffs[index].gameObject.name))
      {
        Buff component = this.GoBuffs[index].transform.GetComponent<Buff>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.Amplify(this.buffAnimationList[this.GoBuffs[index].gameObject.name]);
      }
    }
    this.buffAnimationList.Clear();
  }

  public void InstantFadeOutCharacter()
  {
    if (this.animatedSprites == null)
      return;
    for (int index = 0; index < this.animatedSprites.Count; ++index)
    {
      if ((UnityEngine.Object) this.animatedSprites[index] != (UnityEngine.Object) null)
        this.animatedSprites[index].color = new Color(1f, 1f, 1f, 0.0f);
    }
  }

  public IEnumerator FadeInCharacter()
  {
    float index = 0.0f;
    while ((double) index < 1.0)
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.03f);
      index += 0.1f;
      if ((UnityEngine.Object) this.anim != (UnityEngine.Object) null)
      {
        for (int index1 = 0; index1 < this.animatedSprites.Count; ++index1)
          this.animatedSprites[index1].color = new Color(1f, 1f, 1f, index);
      }
      else
        this.charImageSR.color = new Color(1f, 1f, 1f, index);
    }
    yield return (object) null;
  }

  public void KillCharacterFromOutside() => this.KillCoroutine = this.StartCoroutine(this.KillCharacterCO());

  private IEnumerator KillCharacterCO()
  {
    CharacterItem characterItem = this;
    if (GameManager.Instance.GetDeveloperMode())
      Debug.Log((object) ("[KILLCHARACTERCO] isDying -> " + characterItem.isDying.ToString()));
    if (Globals.Instance.ShowDebug)
      Functions.DebugLogGD("[KILLCHARACTERCO] isDying -> " + characterItem.isDying.ToString(), "trace");
    if (!characterItem.isDying)
    {
      if (characterItem.isHero && characterItem._hero == null || !characterItem.isHero && characterItem._npc == null)
      {
        Debug.LogError((object) "[KILLCHARACTERCO] STOP because NULL");
      }
      else
      {
        characterItem.isDying = true;
        MatchManager.Instance.SetWaitingKill(true);
        StringBuilder stringBuilder = new StringBuilder();
        if (characterItem.isHero && characterItem._hero != null)
        {
          stringBuilder.Append(characterItem._hero.HpCurrent);
          stringBuilder.Append("<size=-.5><color=#bbb>/");
          stringBuilder.Append(characterItem._hero.GetMaxHP());
          characterItem.hpText.text = stringBuilder.ToString();
          MatchManager.Instance.CreateLogEntry(true, "", "", characterItem._hero, (NPC) null, (Hero) null, (NPC) null, MatchManager.Instance.GameRound(), Enums.EventActivation.Killed);
          for (int _order = 0; _order < 4; ++_order)
          {
            Hero hero = MatchManager.Instance.GetHero(_order);
            if (hero != null && hero.Alive)
              hero.SetEvent(Enums.EventActivation.CharacterKilled, (Character) characterItem._hero);
          }
          if (characterItem._hero.HaveResurrectItem())
          {
            characterItem._hero.ActivateItem(Enums.EventActivation.Killed, (Character) characterItem._hero, 0, "");
            MatchManager.Instance.waitingDeathScreen = false;
            MatchManager.Instance.SetWaitingKill(false);
            characterItem.isDying = false;
            MatchManager.Instance.CreateLogEntry(true, "", "", characterItem._hero, (NPC) null, (Hero) null, (NPC) null, MatchManager.Instance.GameRound(), Enums.EventActivation.Resurrect);
            yield break;
          }
          else
          {
            characterItem._hero.SetEvent(Enums.EventActivation.Killed);
            yield return (object) Globals.Instance.WaitForSeconds(0.3f);
            if (characterItem._hero.GetHp() > 0)
            {
              characterItem.isDying = false;
              MatchManager.Instance.SetWaitingKill(false);
              MatchManager.Instance.waitingDeathScreen = false;
              yield break;
            }
            else
            {
              characterItem._hero.Alive = false;
              if (MatchManager.Instance.AnyHeroAlive())
              {
                if (!MatchManager.Instance.waitingDeathScreen)
                {
                  MatchManager.Instance.waitingDeathScreen = true;
                  MatchManager.Instance.ShowDeathScreen(characterItem._hero);
                }
                while (MatchManager.Instance.waitingDeathScreen)
                  yield return (object) Globals.Instance.WaitForSeconds(0.01f);
              }
              else
              {
                MatchManager.Instance.waitingDeathScreen = false;
                MatchManager.Instance.SetWaitingKill(false);
              }
            }
          }
        }
        else if (characterItem._npc != null)
        {
          stringBuilder.Append(characterItem._npc.HpCurrent);
          stringBuilder.Append("<size=-.5><color=#bbb>/");
          stringBuilder.Append(characterItem._npc.GetMaxHP());
          characterItem.hpText.text = stringBuilder.ToString();
          MatchManager.Instance.CreateLogEntry(true, "", "", (Hero) null, characterItem._npc, (Hero) null, (NPC) null, MatchManager.Instance.GameRound(), Enums.EventActivation.Killed);
          for (int _order = 0; _order < 4; ++_order)
          {
            Hero hero = MatchManager.Instance.GetHero(_order);
            if (hero != null && hero.Alive)
              hero.SetEvent(Enums.EventActivation.CharacterKilled, (Character) characterItem._npc);
          }
          if (characterItem._npc.HaveResurrectItem())
          {
            characterItem._npc.ActivateItem(Enums.EventActivation.Killed, (Character) characterItem._npc, 0, "");
            yield return (object) Globals.Instance.WaitForSeconds(0.8f);
            characterItem.isDying = false;
            MatchManager.Instance.SetWaitingKill(false);
            yield break;
          }
          else
          {
            characterItem._npc.SetEvent(Enums.EventActivation.Killed);
            int hp;
            if (Globals.Instance.ShowDebug)
            {
              hp = characterItem._npc.GetHp();
              Functions.DebugLogGD("[KILLCHARACTERCO] Kill NPC _ Life -> " + hp.ToString());
            }
            if (characterItem._npc.GetHp() > 0)
            {
              Debug.Log((object) "[KILLCHARACTERCO] npc resurrect");
              characterItem.isDying = false;
              MatchManager.Instance.SetWaitingKill(false);
              yield break;
            }
            else
            {
              characterItem._npc.Alive = false;
              if (Globals.Instance.ShowDebug)
              {
                hp = characterItem._npc.GetHp();
                Functions.DebugLogGD("[KILLCHARACTERCO] Kill 2 NPC _ Life -> " + hp.ToString(), "trace");
              }
            }
          }
        }
        for (int index1 = characterItem.transform.childCount - 1; index1 >= 0; --index1)
        {
          Transform child = characterItem.transform.GetChild(index1);
          if ((UnityEngine.Object) child != (UnityEngine.Object) null && child.name != "Character" && child.name != characterItem.transform.gameObject.name)
            UnityEngine.Object.Destroy((UnityEngine.Object) child.gameObject);
        }
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("[KILLCHARACTERCO] Kill 3", "trace");
        float index = 1f;
        while ((double) index > 0.0)
        {
          yield return (object) Globals.Instance.WaitForSeconds(0.02f);
          index -= 0.1f;
          if ((UnityEngine.Object) characterItem.anim != (UnityEngine.Object) null)
          {
            for (int index2 = 0; index2 < characterItem.animatedSprites.Count; ++index2)
            {
              if ((UnityEngine.Object) characterItem.animatedSprites[index2] != (UnityEngine.Object) null)
                characterItem.animatedSprites[index2].color = new Color(1f, 1f, 1f, index);
            }
          }
          else if ((UnityEngine.Object) characterItem.charImageSR != (UnityEngine.Object) null)
            characterItem.charImageSR.color = new Color(1f, 1f, 1f, index);
        }
        if (characterItem.isHero && (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null)
          MatchManager.Instance.RemoveFromTransformDict(characterItem._hero.Id);
        if (!characterItem.isHero && (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null)
          MatchManager.Instance.RemoveFromTransformDict(characterItem._npc.Id);
        if (Globals.Instance.ShowDebug)
          Functions.DebugLogGD("[KILLCHARACTERCO] Kill 4 - Call KillHero or KillNPC", "trace");
        if (characterItem.isHero)
        {
          yield return (object) Globals.Instance.WaitForSeconds(0.01f * (float) characterItem._hero.Position);
          MatchManager.Instance.SetWaitingKill(false);
          MatchManager.Instance.KillHero(characterItem._hero);
        }
        else
        {
          yield return (object) Globals.Instance.WaitForSeconds(0.01f * (float) characterItem._npc.Position);
          MatchManager.Instance.SetWaitingKill(false);
          MatchManager.Instance.KillNPC(characterItem._npc);
        }
      }
    }
  }

  public void ShowOverCards()
  {
    if ((UnityEngine.Object) this.heroDecks == (UnityEngine.Object) null || this._hero == null)
      return;
    TMP_Text heroDecksDeckText = this.heroDecksDeckText;
    TMP_Text heroDecksDeckTextBg = this.heroDecksDeckTextBg;
    int num1 = MatchManager.Instance.CountHeroDeck(this._hero.HeroIndex);
    string str1;
    string str2 = str1 = num1.ToString();
    heroDecksDeckTextBg.text = str1;
    string str3 = str2;
    heroDecksDeckText.text = str3;
    TMP_Text decksDiscardText = this.heroDecksDiscardText;
    TMP_Text decksDiscardTextBg = this.heroDecksDiscardTextBg;
    int num2 = MatchManager.Instance.CountHeroDiscard(this._hero.HeroIndex);
    string str4;
    string str5 = str4 = num2.ToString();
    decksDiscardTextBg.text = str4;
    string str6 = str5;
    decksDiscardText.text = str6;
    if (0 > 0)
      this.heroDecksDeckText.color = new Color(1f, 0.41f, 0.56f);
    else
      this.heroDecksDeckText.color = new Color(0.88f, 0.71f, 0.3f);
    if (this.heroDecks.gameObject.activeSelf)
      return;
    this.heroDecks.gameObject.SetActive(true);
  }

  public void HideOverCards()
  {
    if ((UnityEngine.Object) this.heroDecks == (UnityEngine.Object) null || !this.heroDecks.gameObject.activeSelf)
      return;
    this.heroDecks.gameObject.SetActive(false);
  }

  private void OnMouseExit() => this.fOnMouseExit();

  private void OnMouseEnter() => this.fOnMouseEnter();

  private void OnMouseOver() => this.fOnMouseOver();

  public void fOnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive())
      return;
    if (MatchManager.Instance.CardDrag)
    {
      if (GameManager.Instance.IsMultiplayer() && MatchManager.Instance.IsYourTurn() && !MatchManager.Instance.CanInstaCast(MatchManager.Instance.CardItemActive.CardData) && (UnityEngine.Object) MatchManager.Instance.CardItemActive != (UnityEngine.Object) null)
      {
        bool isHero = false;
        byte characterIndex = 0;
        if (this.Hero != null)
        {
          isHero = true;
          characterIndex = (byte) this.Hero.HeroIndex;
        }
        if (this.NPC != null)
          characterIndex = (byte) this.NPC.NPCIndex;
        MatchManager.Instance.DrawArrowNet(MatchManager.Instance.CardItemActive.tablePosition, MatchManager.Instance.CardItemActive.transform.position, this.transform.position + new Vector3(0.0f, this.GetComponent<BoxCollider2D>().size.y * 0.7f, 0.0f), isHero, characterIndex);
      }
      if (!MatchManager.Instance.CanInstaCast(MatchManager.Instance.CardActive))
      {
        if (MatchManager.Instance.CheckTarget(this.transform))
        {
          if ((UnityEngine.Object) MatchManager.Instance.CardItemActive != (UnityEngine.Object) null)
            MatchManager.Instance.CardItemActive.SetColorArrow("green");
          this.OutlineWhite();
          if (this._hero != null)
            MatchManager.Instance.combatTarget.SetTargetTMP((Character) this._hero);
          else
            MatchManager.Instance.combatTarget.SetTargetTMP((Character) this._npc);
        }
        else if ((UnityEngine.Object) MatchManager.Instance.CardItemActive != (UnityEngine.Object) null)
          MatchManager.Instance.CardItemActive.SetColorArrow("red");
      }
    }
    else
    {
      if (this._hero != null)
      {
        MatchManager.Instance.combatTarget.SetTargetTMP((Character) this._hero);
        if (this._hero.SourceName == "Magnus")
          this.PetMagnus();
        if (this._hero.SourceName == "Yogger")
          this.PetYogger();
      }
      else
        MatchManager.Instance.combatTarget.SetTargetTMP((Character) this._npc);
      this.OutlineGray();
      GameManager.Instance.SetCursorHover();
      GameManager.Instance.PlayLibraryAudio("castnpccardfast");
      this.ShowHelp(true);
    }
    MatchManager.Instance.PortraitHighlight(true, this);
  }

  private void PetMagnus()
  {
    if (!(bool) (UnityEngine.Object) MatchManager.Instance)
      return;
    ++this.petMagnusCounter;
    if (this.petMagnusCounter > 5)
    {
      this.anim.ResetTrigger("pet");
      this.anim.SetTrigger("pet");
      this.petMagnusCounter = 0;
      if (this.petMagnusAnswer == 0)
      {
        MatchManager.Instance.DoComic((Character) this._hero, Texts.Instance.GetText("magnusPet1"), 2f);
        this.petMagnusAnswer = 1;
      }
      else if (this.petMagnusAnswer == 1)
      {
        MatchManager.Instance.DoComic((Character) this._hero, Texts.Instance.GetText("magnusPet2"), 2f);
        this.petMagnusAnswer = 0;
      }
    }
    if (this.petMagnusCoroutine != null)
      this.StopCoroutine(this.petMagnusCoroutine);
    this.petMagnusCoroutine = this.StartCoroutine(this.PetMagnusStop());
  }

  private IEnumerator PetMagnusStop()
  {
    yield return (object) Globals.Instance.WaitForSeconds(1.5f);
    this.petMagnusCounter = 0;
  }

  private void PetYogger()
  {
    if (!(bool) (UnityEngine.Object) MatchManager.Instance)
      return;
    ++this.petYoggerCounter;
    if (this.petYoggerCounter > 5)
    {
      this.anim.ResetTrigger("pet");
      this.anim.SetTrigger("pet");
      this.petYoggerCounter = 0;
      if (this.petYoggerAnswer == 0)
      {
        MatchManager.Instance.DoComic((Character) this._hero, Texts.Instance.GetText("yoggerPet1"), 2f);
        this.petYoggerAnswer = 1;
      }
      else if (this.petYoggerAnswer == 1)
      {
        MatchManager.Instance.DoComic((Character) this._hero, Texts.Instance.GetText("yoggerPet2"), 2f);
        this.petYoggerAnswer = 0;
      }
    }
    if (this.petYoggerCoroutine != null)
      this.StopCoroutine(this.petYoggerCoroutine);
    this.petYoggerCoroutine = this.StartCoroutine(this.PetYoggerStop());
  }

  private IEnumerator PetYoggerStop()
  {
    yield return (object) Globals.Instance.WaitForSeconds(1.5f);
    this.petYoggerCounter = 0;
  }

  public void fOnMouseUp()
  {
    if (!Functions.ClickedThisTransform(this.transform) || !((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null))
      return;
    if (!MatchManager.Instance.CardDrag)
    {
      if (MatchManager.Instance.justCasted)
        return;
      this.ShowHelp(false);
      if (this._hero != null)
        MatchManager.Instance.ShowCharacterWindow("stats", characterIndex: this._hero.HeroIndex);
      else
        MatchManager.Instance.ShowCharacterWindow("stats", false, this._npc.NPCIndex);
    }
    else
    {
      if (!MatchManager.Instance.controllerClickedCard)
        return;
      MatchManager.Instance.ControllerExecute();
    }
  }

  public void fOnMouseOver()
  {
    if (SettingsManager.Instance.IsActive() || AlertManager.Instance.IsActive() || MadnessManager.Instance.IsActive() || SandboxManager.Instance.IsActive() || EventSystem.current.IsPointerOverGameObject() || !((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null) || MatchManager.Instance.CardDrag || MatchManager.Instance.justCasted || !Input.GetMouseButtonUp(1))
      return;
    this.ShowHelp(false);
    if (this._hero != null)
      MatchManager.Instance.ShowCharacterWindow("combatdeck", characterIndex: this._hero.HeroIndex);
    else
      MatchManager.Instance.ShowCharacterWindow("combatdiscard", false, this._npc.NPCIndex);
  }

  public void fOnMouseExit()
  {
    if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null)
    {
      MatchManager.Instance.combatTarget.ClearTarget();
      this.ShowHelp(false);
    }
    if (MatchManager.Instance.CardDrag)
    {
      if ((UnityEngine.Object) MatchManager.Instance.CardItemActive != (UnityEngine.Object) null)
      {
        if (GameManager.Instance.IsMultiplayer() && MatchManager.Instance.IsYourTurn())
          MatchManager.Instance.StopArrowNet(MatchManager.Instance.CardItemActive.tablePosition);
        if (!MatchManager.Instance.CanInstaCast(MatchManager.Instance.CardActive))
        {
          MatchManager.Instance.CardItemActive.SetColorArrow("gold");
          MatchManager.Instance.SetGlobalOutlines(true, MatchManager.Instance.CardActive);
        }
      }
    }
    else
    {
      if ((UnityEngine.Object) this.popupSheet != (UnityEngine.Object) null)
        this.popupSheet.ClosePopup();
      this.OutlineHide();
      GameManager.Instance.SetCursorPlain();
    }
    MatchManager.Instance.PortraitHighlight(false, this);
  }

  public void ShowHelp(bool state)
  {
    if (!state)
    {
      if (this.helpCo != null)
        this.StopCoroutine(this.helpCo);
      MatchManager.Instance.helpCharacterTransform.gameObject.SetActive(false);
    }
    else
      this.helpCo = this.StartCoroutine(this.ShowHelpCo());
  }

  private IEnumerator ShowHelpCo()
  {
    CharacterItem characterItem = this;
    if (characterItem.helpCo != null)
      characterItem.StopCoroutine(characterItem.helpCo);
    yield return (object) Globals.Instance.WaitForSeconds(0.5f);
    MatchManager.Instance.helpRight.text = characterItem._hero == null ? Texts.Instance.GetText("helpCasted") : Texts.Instance.GetText("helpDeck");
    MatchManager.Instance.helpCharacterTransform.gameObject.SetActive(true);
  }

  private void ResetMaterial()
  {
    if (this._hero != null)
    {
      this._hero.SetTaunt();
      this._hero.SetStealth();
      this._hero.SetParalyze();
    }
    else
    {
      if (this._npc == null)
        return;
      this._npc.SetTaunt();
      this._npc.SetStealth();
      this._npc.SetParalyze();
    }
  }

  public void OutlineGreen()
  {
    if (this.animatedSprites != null)
    {
      for (int index = 0; index < this.animatedSprites.Count; ++index)
      {
        if (this.animatedSprites[index].gameObject.name.ToLower() != "shadow")
          this.animatedSprites[index].color = new Color(1f, 1f, 1f, 1f);
      }
      this.ResetMaterial();
    }
    else
    {
      this.spriteOutline.EnableGreen();
      this.charImageSR.color = new Color(1f, 1f, 1f, 1f);
    }
  }

  public void OutlineRed()
  {
    if (this.animatedSprites != null)
    {
      for (int index = 0; index < this.animatedSprites.Count; ++index)
      {
        if (this.animatedSprites[index].gameObject.name.ToLower() != "shadow")
          this.animatedSprites[index].color = new Color(0.3f, 0.3f, 0.3f, 1f);
        this.animatedSprites[index].sharedMaterial = this.animatedSpritesDefaultMaterial[this.animatedSprites[index].name];
      }
    }
    else
    {
      this.spriteOutline.EnableRed();
      this.charImageSR.color = new Color(0.5f, 0.5f, 0.5f, 1f);
    }
  }

  public void OutlineGray()
  {
    if (this.animatedSprites != null)
    {
      for (int index = 0; index < this.animatedSprites.Count; ++index)
      {
        if ((UnityEngine.Object) this.animatedSprites[index] != (UnityEngine.Object) null)
        {
          if (this.animatedSprites[index].gameObject.name.ToLower() != "shadow")
            this.animatedSprites[index].color = new Color(0.6f, 0.6f, 0.6f, 1f);
          this.animatedSprites[index].sharedMaterial = this.animatedSpritesDefaultMaterial[this.animatedSprites[index].name];
        }
      }
    }
    else
    {
      this.spriteOutline.EnableWhite();
      this.charImageSR.color = new Color(0.6f, 0.6f, 0.6f, 1f);
    }
  }

  public void OutlineWhite()
  {
    if (this.animatedSprites != null)
    {
      for (int index = 0; index < this.animatedSprites.Count; ++index)
      {
        if ((UnityEngine.Object) this.animatedSprites[index] != (UnityEngine.Object) null)
        {
          if (this.animatedSprites[index].gameObject.name.ToLower() != "shadow")
            this.animatedSprites[index].color = new Color(0.55f, 1f, 0.56f, 1f);
          this.animatedSprites[index].sharedMaterial = this.animatedSpritesDefaultMaterial[this.animatedSprites[index].name];
        }
      }
    }
    else
    {
      this.spriteOutline.EnableWhite();
      this.charImageSR.color = new Color(1f, 1f, 1f, 1f);
    }
  }

  public void OutlineHide()
  {
    if (this.animatedSprites != null)
    {
      for (int index = 0; index < this.animatedSprites.Count; ++index)
      {
        if ((UnityEngine.Object) this.animatedSprites[index] != (UnityEngine.Object) null)
          this.animatedSprites[index].color = new Color(1f, 1f, 1f, 1f);
      }
      this.ResetMaterial();
    }
    else
    {
      this.spriteOutline.Hide();
      this.charImageSR.color = new Color(1f, 1f, 1f, 1f);
    }
  }

  public void ShowEnchantments()
  {
    if (this.isHero)
    {
      if ((UnityEngine.Object) this.iconEnchantment != (UnityEngine.Object) null)
      {
        if (this._hero == null || this._hero.Enchantment == "")
        {
          if (this.iconEnchantment.gameObject.activeSelf)
          {
            this.iconEnchantment.gameObject.SetActive(false);
            this.iconEnchantment.StopCardAnimation();
          }
        }
        else if (this._hero.Alive)
        {
          this.iconEnchantment.gameObject.SetActive(true);
          this.iconEnchantment.ShowIconExternal("enchantment", (Character) this._hero);
          this.iconEnchantment.TheHero = this._hero;
        }
      }
      if ((UnityEngine.Object) this.iconEnchantment2 != (UnityEngine.Object) null)
      {
        if (this._hero == null || this._hero.Enchantment2 == "")
        {
          if (this.iconEnchantment2.gameObject.activeSelf)
          {
            this.iconEnchantment2.gameObject.SetActive(false);
            this.iconEnchantment2.StopCardAnimation();
          }
        }
        else if (this._hero.Alive)
        {
          this.iconEnchantment2.gameObject.SetActive(true);
          this.iconEnchantment2.ShowIconExternal("enchantment2", (Character) this._hero);
          this.iconEnchantment2.TheHero = this._hero;
        }
      }
      if ((UnityEngine.Object) this.iconEnchantment3 != (UnityEngine.Object) null)
      {
        if (this._hero == null || this._hero.Enchantment3 == "")
        {
          if (this.iconEnchantment3.gameObject.activeSelf)
          {
            this.iconEnchantment3.gameObject.SetActive(false);
            this.iconEnchantment3.StopCardAnimation();
          }
        }
        else if (this._hero.Alive)
        {
          this.iconEnchantment3.gameObject.SetActive(true);
          this.iconEnchantment3.ShowIconExternal("enchantment3", (Character) this._hero);
          this.iconEnchantment3.TheHero = this._hero;
        }
      }
      this._hero.ShowPetsFromEnchantments();
    }
    else
    {
      if ((UnityEngine.Object) this.iconEnchantment != (UnityEngine.Object) null)
      {
        if (this._npc == null || this._npc.Enchantment == "")
        {
          if (this.iconEnchantment.gameObject.activeSelf)
          {
            this.iconEnchantment.gameObject.SetActive(false);
            this.iconEnchantment.StopCardAnimation();
          }
        }
        else if (this._npc.Alive)
        {
          this.iconEnchantment.gameObject.SetActive(true);
          this.iconEnchantment.ShowIconExternal("enchantment", (Character) this._npc);
          this.iconEnchantment.TheNPC = this._npc;
        }
      }
      if ((UnityEngine.Object) this.iconEnchantment2 != (UnityEngine.Object) null)
      {
        if (this._npc == null || this._npc.Enchantment2 == "")
        {
          if (this.iconEnchantment2.gameObject.activeSelf)
          {
            this.iconEnchantment2.gameObject.SetActive(false);
            this.iconEnchantment2.StopCardAnimation();
          }
        }
        else if (this._npc.Alive)
        {
          this.iconEnchantment2.gameObject.SetActive(true);
          this.iconEnchantment2.ShowIconExternal("enchantment2", (Character) this._npc);
          this.iconEnchantment2.TheNPC = this._npc;
        }
      }
      if ((UnityEngine.Object) this.iconEnchantment3 != (UnityEngine.Object) null)
      {
        if (this._npc == null || this._npc.Enchantment3 == "")
        {
          if (this.iconEnchantment3.gameObject.activeSelf)
          {
            this.iconEnchantment3.gameObject.SetActive(false);
            this.iconEnchantment3.StopCardAnimation();
          }
        }
        else if (this._npc.Alive)
        {
          this.iconEnchantment3.gameObject.SetActive(true);
          this.iconEnchantment3.ShowIconExternal("enchantment3", (Character) this._npc);
          this.iconEnchantment3.TheNPC = this._npc;
        }
      }
      this._npc.ShowPetsFromEnchantments();
    }
  }

  public IEnumerator EnchantEffectCo()
  {
    CharacterItem characterItem = this;
    yield return (object) Globals.Instance.WaitForSeconds(0.3f);
    ++characterItem.counterEffectItemOwner;
    if (characterItem.counterEffectItemOwner >= 10)
    {
      ItemData itemData = (ItemData) null;
      if (characterItem._hero != null)
      {
        if (characterItem.indexEffectItemOwner == 0 && characterItem._hero.Enchantment != "")
          itemData = Globals.Instance.GetItemData(characterItem._hero.Enchantment);
        else if (characterItem.indexEffectItemOwner == 1 && characterItem._hero.Enchantment2 != "")
          itemData = Globals.Instance.GetItemData(characterItem._hero.Enchantment2);
        else if (characterItem.indexEffectItemOwner == 2 && characterItem._hero.Enchantment3 != "")
          itemData = Globals.Instance.GetItemData(characterItem._hero.Enchantment3);
        if ((UnityEngine.Object) itemData != (UnityEngine.Object) null && itemData.EffectItemOwner != "")
          EffectsManager.Instance.PlayEffectAC(itemData.EffectItemOwner, true, characterItem.CharImageT, false);
        characterItem.counterEffectItemOwner = 0;
        ++characterItem.indexEffectItemOwner;
        if (characterItem.indexEffectItemOwner > 2)
          characterItem.indexEffectItemOwner = 0;
        if (characterItem.indexEffectItemOwner == 0 && characterItem._hero.Enchantment == "")
          characterItem.indexEffectItemOwner = 1;
        if (characterItem.indexEffectItemOwner == 1 && characterItem._hero.Enchantment2 == "")
          characterItem.indexEffectItemOwner = 2;
        if (characterItem.indexEffectItemOwner == 2 && characterItem._hero.Enchantment3 == "")
          characterItem.indexEffectItemOwner = 0;
      }
      else if (characterItem._npc != null)
      {
        if (characterItem.indexEffectItemOwner == 0 && characterItem._npc.Enchantment != "")
          itemData = Globals.Instance.GetItemData(characterItem._npc.Enchantment);
        else if (characterItem.indexEffectItemOwner == 1 && characterItem._npc.Enchantment2 != "")
          itemData = Globals.Instance.GetItemData(characterItem._npc.Enchantment2);
        else if (characterItem.indexEffectItemOwner == 2 && characterItem._npc.Enchantment3 != "")
          itemData = Globals.Instance.GetItemData(characterItem._npc.Enchantment3);
        if ((UnityEngine.Object) itemData != (UnityEngine.Object) null && itemData.EffectItemOwner != "")
          EffectsManager.Instance.PlayEffectAC(itemData.EffectItemOwner, true, characterItem.CharImageT, false);
        characterItem.counterEffectItemOwner = 0;
        ++characterItem.indexEffectItemOwner;
        if (characterItem.indexEffectItemOwner > 2)
          characterItem.indexEffectItemOwner = 0;
        if (characterItem.indexEffectItemOwner == 0 && characterItem._npc.Enchantment == "")
          characterItem.indexEffectItemOwner = 1;
        if (characterItem.indexEffectItemOwner == 1 && characterItem._npc.Enchantment2 == "")
          characterItem.indexEffectItemOwner = 2;
        if (characterItem.indexEffectItemOwner == 2 && characterItem._npc.Enchantment3 == "")
          characterItem.indexEffectItemOwner = 0;
      }
    }
    characterItem.StartCoroutine(characterItem.EnchantEffectCo());
  }

  public void EnchantmentExecute(int index)
  {
    if (this._hero != null && !this._hero.Alive || this._npc != null && !this._npc.Alive)
      return;
    switch (index)
    {
      case 0:
        this.iconEnchantment.SetTimesExecuted(1);
        break;
      case 1:
        this.iconEnchantment2.SetTimesExecuted(1);
        break;
      case 2:
        this.iconEnchantment3.SetTimesExecuted(1);
        break;
    }
  }

  public void ShowCharacterPing(int _action) => this.emoteCharacterPing.Show((this._hero == null ? (Character) this._npc : (Character) this._hero).Id, _action);

  public void HideCharacterPing() => this.emoteCharacterPing.Hide();

  public void ShowKeyNum(bool _state, string _num = "", bool _disabled = false)
  {
    if (this.keyTransform.gameObject.activeSelf != _state)
      this.keyTransform.gameObject.SetActive(_state);
    if (!_state)
      return;
    this.keyNumber.text = _num;
    if (_disabled)
    {
      if (!this.keyRed.gameObject.activeSelf)
        this.keyRed.gameObject.SetActive(true);
      if (!this.keyBackground.gameObject.activeSelf)
        return;
      this.keyBackground.gameObject.SetActive(false);
    }
    else
    {
      if (this.keyRed.gameObject.activeSelf)
        this.keyRed.gameObject.SetActive(false);
      if (this.keyBackground.gameObject.activeSelf)
        return;
      this.keyBackground.gameObject.SetActive(true);
    }
  }

  public SpriteRenderer CharImageSR
  {
    get => this.charImageSR;
    set => this.charImageSR = value;
  }

  public Transform CharImageT
  {
    get => this.charImageT;
    set => this.charImageT = value;
  }

  public Transform CharImageShadowT
  {
    get => this.charImageShadowT;
    set => this.charImageShadowT = value;
  }

  public TMP_Text HpText
  {
    get => this.hpText;
    set => this.hpText = value;
  }

  public bool IsHero
  {
    get => this.isHero;
    set => this.isHero = value;
  }

  public Hero Hero
  {
    get => this._hero;
    set => this._hero = value;
  }

  public NPC NPC
  {
    get => this._npc;
    set => this._npc = value;
  }

  public Animator Anim
  {
    get => this.anim;
    set => this.anim = value;
  }
}
