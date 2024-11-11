// Decompiled with JetBrains decompiler
// Type: Effects
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Effects : MonoBehaviour
{
  public Transform childTransform;
  public Animator childAnimator;
  private SpriteRenderer childSpr;

  private void Awake() => this.childSpr = this.childTransform.GetComponent<SpriteRenderer>();

  public void Play(
    string effect,
    Transform theTransform,
    bool isHero,
    bool flip,
    bool castInCenter)
  {
    if ((Object) this.transform == (Object) null || (Object) theTransform == (Object) null || effect == "")
      return;
    if (castInCenter)
      this.transform.localPosition = new Vector3(0.0f, 1.4f, 1f);
    else if (effect == "smoke" && (isHero & flip || !isHero && !flip))
    {
      this.transform.localPosition = new Vector3(0.0f, 1.4f, 0.0f);
    }
    else
    {
      this.transform.localPosition = new Vector3(theTransform.position.x, 1.4f, 1f);
      this.transform.localPosition -= new Vector3(theTransform.localPosition.x, 0.0f, 0.0f);
    }
    GameObject resourceEffect = Globals.Instance.GetResourceEffect(effect);
    if ((Object) resourceEffect == (Object) null)
    {
      Debug.LogError((object) ("Effect doesn't exists => " + effect));
    }
    else
    {
      this.childTransform.localPosition = resourceEffect.transform.localPosition;
      this.childTransform.localRotation = resourceEffect.transform.localRotation;
      this.childTransform.localScale = resourceEffect.transform.localScale;
      if ((Object) resourceEffect.GetComponent<Animator>() != (Object) null)
        this.childAnimator.runtimeAnimatorController = resourceEffect.GetComponent<Animator>().runtimeAnimatorController;
      if (!((Object) this.childSpr != (Object) null))
        return;
      if ((Object) resourceEffect.GetComponent<SpriteRenderer>() != (Object) null)
      {
        this.childSpr.sortingOrder = resourceEffect.GetComponent<SpriteRenderer>().sortingOrder;
        this.childSpr.color = resourceEffect.GetComponent<SpriteRenderer>().color;
      }
      if (flip)
        this.childSpr.flipX = true;
      else
        this.childSpr.flipX = false;
    }
  }

  public bool HasStopped() => (Object) this.childAnimator == (Object) null || (Object) this.childAnimator.runtimeAnimatorController == (Object) null || (double) this.childAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0;

  public void Stop(string effect) => this.childAnimator.runtimeAnimatorController = (RuntimeAnimatorController) null;
}
