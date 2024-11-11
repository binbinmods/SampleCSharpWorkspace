// Decompiled with JetBrains decompiler
// Type: SkillLog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SkillLog : MonoBehaviour
{
  public Transform borderRed;
  public Transform borderGreen;
  public SpriteRenderer sprite;
  private SkillForLog SFL;
  private CardItem CI;
  private float distanceBetweenIcons = 0.55f;

  private void Position(int index, bool isHero)
  {
    float x = (float) (-0.20000000298023224 - (double) index * (double) this.distanceBetweenIcons);
    if (isHero)
      this.transform.localPosition = new Vector3(x, 0.0f, -1f);
    else
      this.transform.localPosition = new Vector3(-x, 0.0f, -1f);
  }

  public void SetSkill(SkillForLog _SFL, int index, bool isHero)
  {
    this.SFL = _SFL;
    this.sprite.sprite = this.SFL.cardData.Sprite;
    if (this.SFL.isFromHero)
      this.borderRed.gameObject.SetActive(false);
    else
      this.borderGreen.gameObject.SetActive(false);
    this.Position(index, isHero);
  }

  private void DestroyCard()
  {
    if ((Object) this.CI != (Object) null)
      this.CI.HideKeyNotes();
    Object.Destroy((Object) MatchManager.Instance.skillLogCard);
    MatchManager.Instance.ShowMask(false);
  }

  private void ShowCard()
  {
    if ((Object) MatchManager.Instance.skillLogCard != (Object) null)
      Object.Destroy((Object) MatchManager.Instance.skillLogCard);
    Vector3 position = this.transform.position + new Vector3(0.0f, 2.6f, 0.0f);
    MatchManager.Instance.skillLogCard = Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, position, Quaternion.identity);
    this.CI = MatchManager.Instance.skillLogCard.GetComponent<CardItem>();
    this.CI.SetCard(this.SFL.cardData.InternalId, false);
    MatchManager.Instance.skillLogCard.transform.localScale = Vector3.zero;
    this.CI.DisableCollider();
    this.CI.DisableTrail();
    this.CI.TopLayeringOrder("UI");
    this.CI.SetDestinationScaleRotation(position, 1.4f, Quaternion.Euler(0.0f, 0.0f, 0.0f));
    this.CI.discard = true;
    this.CI.ShowKeyNotes();
    MatchManager.Instance.ShowMask(true);
  }

  private void OnMouseEnter() => this.ShowCard();

  private void OnMouseExit() => this.DestroyCard();
}
