// Decompiled with JetBrains decompiler
// Type: DeckPile
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DeckPile : MonoBehaviour
{
  private SpriteRenderer spr;

  private void Awake() => this.spr = this.GetComponent<SpriteRenderer>();

  private void OnMouseUp()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive() || MatchManager.Instance.characterWindow.IsActive())
      return;
    GameManager.Instance.SetCursorPlain();
    if (this.gameObject.name == "discardpile")
    {
      if (MatchManager.Instance.CountHeroDiscard() <= 0)
        return;
      MatchManager.Instance.ShowCharacterWindow("combatdiscard", characterIndex: MatchManager.Instance.GetHeroActive());
    }
    else
      MatchManager.Instance.ShowCharacterWindow("combatdeck", characterIndex: MatchManager.Instance.GetHeroActive());
  }

  private void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive() || MatchManager.Instance.characterWindow.IsActive() || (bool) (Object) MatchManager.Instance && MatchManager.Instance.CardDrag)
      return;
    if (this.gameObject.name != "discardpile" || MatchManager.Instance.CountHeroDiscard() > 0)
    {
      GameManager.Instance.SetCursorHover();
      GameManager.Instance.PlayLibraryAudio("castnpccardfast");
    }
    if (this.gameObject.name == "discardpile")
    {
      MatchManager.Instance.DeckParticlesShow(1, true);
    }
    else
    {
      MatchManager.Instance.DeckParticlesShow(0, true);
      this.spr.color = new Color(0.9f, 0.8f, 0.1f, 1f);
    }
  }

  private void OnMouseExit()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive() || (bool) (Object) MatchManager.Instance && MatchManager.Instance.CardDrag)
      return;
    GameManager.Instance.SetCursorPlain();
    if (this.gameObject.name == "discardpile")
    {
      MatchManager.Instance.DeckParticlesShow(1, false);
    }
    else
    {
      MatchManager.Instance.DeckParticlesShow(0, false);
      this.spr.color = new Color(1f, 1f, 1f, 1f);
    }
  }
}
