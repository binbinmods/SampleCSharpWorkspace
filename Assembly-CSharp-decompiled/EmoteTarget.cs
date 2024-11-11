// Decompiled with JetBrains decompiler
// Type: EmoteTarget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class EmoteTarget : MonoBehaviour
{
  public Transform characterT;
  public SpriteRenderer portraitStickerBase;
  public SpriteRenderer icon;
  public SpriteRenderer iconStickerBase;
  private Animator animator;

  private void Awake()
  {
    this.gameObject.SetActive(false);
    this.animator = this.GetComponent<Animator>();
  }

  private void Start() => this.StartCoroutine(this.DestroyEmote());

  public void SetActiveHeroOnCardEmoteButton()
  {
    if (!((Object) MatchManager.Instance != (Object) null))
      return;
    Hero hero = MatchManager.Instance.GetHero(MatchManager.Instance.emoteManager.heroActive);
    if (hero == null || !((Object) hero.HeroData != (Object) null) || !((Object) hero.HeroData.HeroSubClass != (Object) null))
      return;
    this.icon.sprite = hero.HeroData.HeroSubClass.StickerBase;
  }

  public void SetIcons(int _heroIndex, int _action)
  {
    if (!((Object) MatchManager.Instance != (Object) null))
      return;
    Hero hero = MatchManager.Instance.GetHero(_heroIndex);
    if (hero == null || (Object) hero.HeroData == (Object) null || (Object) hero.HeroData.HeroSubClass == (Object) null)
      return;
    if (MatchManager.Instance.emoteManager.EmoteNeedsTarget(_action))
    {
      this.characterT.gameObject.SetActive(true);
      this.icon.sprite = MatchManager.Instance.emoteManager.emotesSprite[_action];
      this.portraitStickerBase.sprite = hero.HeroData.HeroSubClass.GetEmoteBase();
    }
    else
    {
      this.characterT.gameObject.SetActive(false);
      this.icon.sprite = hero.HeroData.HeroSubClass.GetEmote(_action);
      this.iconStickerBase.sprite = hero.HeroData.HeroSubClass.GetEmoteBase();
      this.transform.localPosition += new Vector3(hero.HeroData.HeroSubClass.StickerOffsetX, 0.0f, 0.0f);
    }
    this.gameObject.SetActive(true);
    if (_action == 2 || _action == 3)
      return;
    this.animator.SetTrigger("sticker");
  }

  private IEnumerator DestroyEmote()
  {
    yield return (object) Globals.Instance.WaitForSeconds(4f);
    this.Dest();
  }

  private void Dest() => Object.Destroy((Object) this.gameObject);
}
