// Decompiled with JetBrains decompiler
// Type: CharacterGOItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CharacterGOItem : MonoBehaviour
{
  public CharacterItem _characterItem;

  private void Start()
  {
    Animator component = this.gameObject.GetComponent<Animator>();
    if (!((Object) component != (Object) null) || !(this.transform.parent.parent.name == "NPCs"))
      return;
    AnimatorClipInfo[] animatorClipInfo = component.GetCurrentAnimatorClipInfo(0);
    float length = animatorClipInfo[0].clip.length;
    string name = animatorClipInfo[0].clip.name;
    float normalizedTime = Random.Range(0.0f, length);
    AnimatorStateInfo animatorStateInfo = component.GetCurrentAnimatorStateInfo(0);
    component.Play(animatorStateInfo.shortNameHash, 0, normalizedTime);
  }

  public void OnMouseUp()
  {
    if (!((Object) this._characterItem != (Object) null))
      return;
    this._characterItem.fOnMouseUp();
  }

  private void OnMouseOver()
  {
    if (!((Object) this._characterItem != (Object) null))
      return;
    this._characterItem.fOnMouseOver();
  }

  private void OnMouseEnter()
  {
    if (!((Object) this._characterItem != (Object) null))
      return;
    this._characterItem.fOnMouseEnter();
  }

  private void OnMouseExit()
  {
    if (!((Object) this._characterItem != (Object) null))
      return;
    this._characterItem.fOnMouseExit();
  }
}
