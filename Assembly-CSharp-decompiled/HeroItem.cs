// Decompiled with JetBrains decompiler
// Type: HeroItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class HeroItem : CharacterItem
{
  [SerializeField]
  private HeroData heroData;
  public Transform animatedTransform;
  public int indexInTeam;

  public override void Awake() => base.Awake();

  public override void Start() => base.Start();

  public void Init(Hero _hero)
  {
    this.Hero = _hero;
    this.NPC = (NPC) null;
    this.IsHero = true;
    if ((Object) _hero.GameObjectAnimated != (Object) null)
    {
      GameObject GO = Object.Instantiate<GameObject>(_hero.GameObjectAnimated, Vector3.zero, Quaternion.identity, this.transform);
      this.animatedTransform = GO.transform;
      GO.transform.localPosition = _hero.GameObjectAnimated.transform.localPosition;
      this.GetComponent<CharacterItem>().SetOriginalLocalPosition(GO.transform.localPosition);
      GO.name = this.transform.name;
      this.DisableCollider();
      GO.GetComponent<CharacterGOItem>()._characterItem = this.GetComponent<CharacterItem>();
      this.CharImageSR.sprite = (Sprite) null;
      this.Anim = GO.GetComponent<Animator>();
      this.GetSpritesFromAnimated(GO);
      this.transformForCombatText = GO.transform;
      this.heightModel = GO.GetComponent<BoxCollider2D>().size.y;
    }
    else
    {
      this.CharImageSR.sprite = _hero.HeroSprite;
      this.transformForCombatText = this.transform;
    }
    this.ActivateMark(false);
    this.SetHP();
    this.DrawEnergy();
    if (!((Object) MatchManager.Instance != (Object) null))
      return;
    this.StartCoroutine(this.EnchantEffectCo());
  }

  private IEnumerator TurnOnOffEnergy(SpriteRenderer SR, bool state, bool incoming = false)
  {
    Color colorFade = new Color(0.0f, 0.0f, 0.0f, 0.1f);
    SR.color = !incoming ? new Color(1f, 1f, 1f, SR.color.a) : new Color(0.0f, 0.9f, 1f, SR.color.a);
    if (!state)
    {
      while ((double) SR.color.a > 0.20000000298023224)
      {
        SR.color -= colorFade;
        yield return (object) Globals.Instance.WaitForSeconds(0.05f);
      }
    }
    else
    {
      while ((double) SR.color.a < 1.0)
      {
        SR.color += colorFade;
        yield return (object) Globals.Instance.WaitForSeconds(0.05f);
      }
    }
  }

  public HeroData HeroData
  {
    get => this.heroData;
    set => this.heroData = value;
  }
}
