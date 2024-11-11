// Decompiled with JetBrains decompiler
// Type: PerkBackgroundRow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class PerkBackgroundRow : MonoBehaviour
{
  public TMP_Text rowLevel;
  public TMP_Text rowReq;
  public Transform lockIcon;
  public Transform tabIcon;
  private PopupText popupText;
  private Color colorLevel = new Color(0.88f, 0.71f, 0.16f);
  private Color colorReq = new Color(0.63f, 0.48f, 0.0f);
  private Color colorDis = new Color(0.63f, 0.63f, 0.63f);

  public void SetRequired(int _value)
  {
    this.popupText = this.GetComponent<PopupText>();
    if (_value > 0)
    {
      this.rowReq.text = string.Format(Texts.Instance.GetText("requireSkill"), (object) _value.ToString());
      this.popupText.text = string.Format(Texts.Instance.GetText("requiredPoints"), (object) _value.ToString());
    }
    else
    {
      this.rowReq.text = "";
      this.ShowLockIcon(false);
    }
  }

  public void ShowLockIcon(bool _state)
  {
    this.lockIcon.gameObject.SetActive(_state);
    if (_state)
    {
      this.rowLevel.gameObject.SetActive(false);
      this.tabIcon.gameObject.SetActive(false);
    }
    else
    {
      this.rowLevel.gameObject.SetActive(true);
      this.tabIcon.gameObject.SetActive(true);
    }
  }
}
