// Decompiled with JetBrains decompiler
// Type: ChallengeMadness
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ChallengeMadness : MonoBehaviour
{
  public SpriteRenderer background;
  public SpriteRenderer icon;

  public void SetBackground(string _color) => this.background.color = Functions.HexToColor(_color);

  public void SetDisable() => this.SetBackground("#353535");

  public void SetActive() => this.SetBackground("#AD844D");

  public void SetDefault() => this.SetBackground("#5D3578");

  public void SetIcon(Sprite _sprite) => this.icon.sprite = _sprite;
}
