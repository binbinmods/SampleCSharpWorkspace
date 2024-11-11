// Decompiled with JetBrains decompiler
// Type: RandomHeroSelector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class RandomHeroSelector : MonoBehaviour
{
  public int boxId;
  private SpriteRenderer diceSpr;
  private Color colorOver = new Color(0.5f, 0.5f, 0.5f, 1f);
  private Color colorOut = new Color(1f, 1f, 1f, 1f);
  private Vector3 oriScale;
  private Vector3 maxScale;

  private void Awake()
  {
    this.diceSpr = this.GetComponent<SpriteRenderer>();
    this.oriScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
    this.maxScale = this.oriScale + new Vector3(0.1f, 0.1f, 0.0f);
  }

  public void SetId(int _id) => this.boxId = _id;

  private void OnMouseEnter()
  {
    this.diceSpr.color = this.colorOver;
    this.transform.localScale = this.maxScale;
  }

  private void OnMouseExit()
  {
    this.diceSpr.color = this.colorOut;
    this.transform.localScale = this.oriScale;
  }

  public void OnMouseUp() => HeroSelectionManager.Instance.SetRandomHero(this.boxId);
}
