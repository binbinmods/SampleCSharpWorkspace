// Decompiled with JetBrains decompiler
// Type: EventBag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class EventBag : MonoBehaviour
{
  private void OnMouseDown()
  {
    if (AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || (bool) (Object) MapManager.Instance && MapManager.Instance.IsCharacterUnlock())
      return;
    PlayerUIManager.Instance.BagToggle();
  }

  private void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || (bool) (Object) MapManager.Instance && MapManager.Instance.IsCharacterUnlock())
      return;
    PopupManager.Instance.SetText(Texts.Instance.GetText("eventBag"), true, alwaysCenter: true);
  }

  private void OnMouseExit() => PopupManager.Instance.ClosePopup();
}
