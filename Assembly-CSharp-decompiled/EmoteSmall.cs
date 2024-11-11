// Decompiled with JetBrains decompiler
// Type: EmoteSmall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class EmoteSmall : MonoBehaviour
{
  public TMP_Text letter;
  public SpriteRenderer icon;
  public Transform background;
  public Transform backgroundHover;
  public Transform backgroundBlocked;
  public int action;
  private bool blocked;

  public void Show()
  {
    this.gameObject.SetActive(true);
    this.backgroundHover.gameObject.SetActive(false);
  }

  public void Hide() => this.gameObject.SetActive(false);

  public void SetSprite(Sprite _sprite) => this.icon.sprite = _sprite;

  public void SetAction(int _action)
  {
    this.action = _action;
    this.icon.sprite = MatchManager.Instance.emoteManager.emotesSprite[_action];
    switch (_action)
    {
      case 0:
        this.letter.text = "R";
        break;
      case 1:
        this.letter.text = "E";
        break;
      case 2:
        this.letter.text = "S";
        break;
      case 3:
        this.letter.text = "A";
        break;
      case 4:
        this.letter.text = "W";
        break;
      case 5:
        this.letter.text = "Q";
        break;
    }
  }

  public void SetBlocked(bool _state)
  {
    this.blocked = _state;
    this.backgroundBlocked.gameObject.SetActive(_state);
  }

  public void OnMouseEnter()
  {
    if (!(bool) (Object) MatchManager.Instance)
      return;
    MatchManager.Instance.emoteManager.ShowEmotes();
    if (this.blocked)
      return;
    this.backgroundHover.gameObject.SetActive(true);
  }

  public void OnMouseExit()
  {
    if (!(bool) (Object) MatchManager.Instance)
      return;
    MatchManager.Instance.emoteManager.HideEmotesCo();
    this.backgroundHover.gameObject.SetActive(false);
  }

  public void OnMouseUp()
  {
    if (this.blocked || !(bool) (Object) MatchManager.Instance)
      return;
    MatchManager.Instance.SetCharactersPing(this.action);
  }
}
