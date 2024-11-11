// Decompiled with JetBrains decompiler
// Type: BotHeroChar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class BotHeroChar : MonoBehaviour
{
  public bool isHeroStats;
  private SubClassData subClassData;
  private SpriteRenderer spr;

  public void SetSubClassData(SubClassData _subClassData) => this.subClassData = _subClassData;

  private void Awake() => this.spr = this.GetComponent<BotonRollover>().image.GetComponent<SpriteRenderer>();

  private void Start() => this.turnOffColor();

  private void OnMouseEnter() => this.turnOnColor();

  private void OnMouseExit() => this.turnOffColor();

  private void OnMouseUp()
  {
    if (AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive())
      return;
    if (this.isHeroStats)
      HeroSelectionManager.Instance.charPopup.Trigger(this.subClassData, this.isHeroStats);
    else
      PerkTree.Instance.Show(this.subClassData.Id);
    this.turnOffColor();
  }

  private void turnOffColor() => this.spr.color = new Color(0.8f, 0.8f, 0.8f, 1f);

  private void turnOnColor() => this.spr.color = new Color(1f, 1f, 1f, 1f);
}
