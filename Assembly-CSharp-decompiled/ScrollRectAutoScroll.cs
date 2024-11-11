// Decompiled with JetBrains decompiler
// Type: ScrollRectAutoScroll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof (ScrollRect))]
public class ScrollRectAutoScroll : 
  MonoBehaviour,
  IPointerEnterHandler,
  IEventSystemHandler,
  IPointerExitHandler
{
  public float scrollSpeed = 10f;
  private bool mouseOver;
  private List<Selectable> m_Selectables = new List<Selectable>();
  private ScrollRect m_ScrollRect;
  private Vector2 m_NextScrollPosition = Vector2.up;

  private void OnEnable()
  {
    if (!(bool) (Object) this.m_ScrollRect)
      return;
    this.m_ScrollRect.content.GetComponentsInChildren<Selectable>(this.m_Selectables);
  }

  private void Awake() => this.m_ScrollRect = this.GetComponent<ScrollRect>();

  private void Start()
  {
    if ((bool) (Object) this.m_ScrollRect)
      this.m_ScrollRect.content.GetComponentsInChildren<Selectable>(this.m_Selectables);
    this.ScrollToSelected(true);
  }

  private void Update()
  {
    if (SystemInfo.deviceType == DeviceType.Handheld && Gamepad.all.Count <= 1)
      return;
    this.InputScroll();
    if (!this.mouseOver)
      this.m_ScrollRect.normalizedPosition = Vector2.Lerp(this.m_ScrollRect.normalizedPosition, this.m_NextScrollPosition, this.scrollSpeed * Time.unscaledDeltaTime);
    else
      this.m_NextScrollPosition = this.m_ScrollRect.normalizedPosition;
  }

  private void InputScroll()
  {
    if (this.m_Selectables.Count <= 0)
      return;
    Keyboard current1 = Keyboard.current;
    Gamepad current2 = Gamepad.current;
    if (current1 != null && (Keyboard.current.upArrowKey.isPressed || Keyboard.current.downArrowKey.isPressed))
      this.ScrollToSelected(false);
    if (current2 == null || !Gamepad.current.dpad.up.isPressed && !Gamepad.current.dpad.down.isPressed && !Gamepad.current.leftStick.up.isPressed && !Gamepad.current.leftStick.down.isPressed)
      return;
    this.ScrollToSelected(false);
  }

  private void ScrollToSelected(bool quickScroll)
  {
    int num = -1;
    Selectable component = (bool) (Object) EventSystem.current.currentSelectedGameObject ? EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>() : (Selectable) null;
    if ((bool) (Object) component)
      num = this.m_Selectables.IndexOf(component);
    if (num <= -1)
      return;
    if (quickScroll)
    {
      this.m_ScrollRect.normalizedPosition = new Vector2(0.0f, (float) (1.0 - (double) num / ((double) this.m_Selectables.Count - 1.0)));
      this.m_NextScrollPosition = this.m_ScrollRect.normalizedPosition;
    }
    else
      this.m_NextScrollPosition = new Vector2(0.0f, (float) (1.0 - (double) num / ((double) this.m_Selectables.Count - 1.0)));
  }

  public void OnPointerEnter(PointerEventData eventData) => this.mouseOver = true;

  public void OnPointerExit(PointerEventData eventData)
  {
    this.mouseOver = false;
    this.ScrollToSelected(false);
  }
}
