// Decompiled with JetBrains decompiler
// Type: EventItemTrack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

public class EventItemTrack : MonoBehaviour
{
  private string description;

  public void SetItemTrack(EventRequirementData requirementData)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<size=+3><color=#FFF>");
    stringBuilder.Append(requirementData.RequirementName);
    stringBuilder.Append("</color></size><line-height=30><br></line-height>");
    stringBuilder.Append(requirementData.Description);
    this.description = stringBuilder.ToString();
    this.GetComponent<SpriteRenderer>().sprite = requirementData.ItemSprite;
    if (!((Object) this.transform.GetChild(0).GetComponent<SpriteMask>() != (Object) null))
      return;
    this.transform.GetChild(0).GetComponent<SpriteMask>().sprite = requirementData.ItemSprite;
  }

  private void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || (bool) (Object) MapManager.Instance && MapManager.Instance.IsCharacterUnlock())
      return;
    PopupManager.Instance.SetText(this.description, true, "followdown", true);
  }

  private void OnMouseExit() => PopupManager.Instance.ClosePopup();
}
