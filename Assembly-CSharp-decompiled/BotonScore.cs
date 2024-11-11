// Decompiled with JetBrains decompiler
// Type: BotonScore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class BotonScore : MonoBehaviour
{
  public SpriteRenderer background;
  public TMP_Text text;
  public Color textColorOver;
  public Color textColorOff;
  public Color bgColorOver;
  public Color bgColorOff;
  public string idTranslate = "";
  public int auxInt = -1000;

  private void Awake()
  {
    if (this.idTranslate != "" && (Object) Texts.Instance != (Object) null)
      this.SetText(Texts.Instance.GetText(this.idTranslate));
    if (this.text.transform.childCount <= 0)
      return;
    for (int index = 0; index < this.text.transform.childCount; ++index)
    {
      MeshRenderer component = this.text.transform.GetChild(index).GetComponent<MeshRenderer>();
      component.sortingLayerName = this.text.GetComponent<MeshRenderer>().sortingLayerName;
      component.sortingOrder = this.text.GetComponent<MeshRenderer>().sortingOrder;
    }
  }

  private void Start() => this.SetPlainColors();

  private void SetPlainColors()
  {
    this.background.color = this.bgColorOff;
    this.text.color = this.textColorOff;
  }

  public void SetText(string _text) => this.text.text = _text;

  private void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive())
      return;
    this.background.color = this.bgColorOver;
    this.text.color = this.textColorOver;
    GameManager.Instance.SetCursorHover();
  }

  private void OnMouseOver()
  {
    if (AlertManager.Instance.IsActive())
      return;
    SettingsManager.Instance.IsActive();
  }

  private void OnMouseExit()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive())
      return;
    this.SetPlainColors();
    GameManager.Instance.SetCursorPlain();
  }

  public void OnMouseUp()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive())
      return;
    TomeManager.Instance.ShowScoreboard(this.auxInt);
  }
}
