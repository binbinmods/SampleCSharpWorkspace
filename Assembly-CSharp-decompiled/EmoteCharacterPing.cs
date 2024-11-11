// Decompiled with JetBrains decompiler
// Type: EmoteCharacterPing
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class EmoteCharacterPing : MonoBehaviour
{
  public TMP_Text letter;
  public SpriteRenderer icon;
  public Transform background;
  public Transform backgroundHover;
  public int action;
  private string characterId;

  private void Start() => this.Hide();

  public void Show(string _id, int _action)
  {
    if (!(bool) (Object) MatchManager.Instance)
      return;
    this.characterId = _id;
    this.action = _action;
    this.icon.sprite = MatchManager.Instance.emoteManager.emotesSprite[_action];
    this.gameObject.SetActive(true);
    this.backgroundHover.gameObject.SetActive(false);
  }

  public void Hide() => this.gameObject.SetActive(false);

  public void OnMouseEnter()
  {
    if (!(bool) (Object) MatchManager.Instance)
      return;
    this.backgroundHover.gameObject.SetActive(true);
  }

  public void OnMouseExit()
  {
    if (!(bool) (Object) MatchManager.Instance)
      return;
    this.backgroundHover.gameObject.SetActive(false);
  }

  public void OnMouseUp()
  {
    if (!(bool) (Object) MatchManager.Instance)
      return;
    MatchManager.Instance.EmoteTarget(this.characterId, this.action);
  }
}
