// Decompiled with JetBrains decompiler
// Type: InputController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
  private ActionsController actionsController;
  private PointerEventData pointerEventData;
  private List<RaycastResult> raycastResultList;
  private float _mouseSpeed = 300f;
  private float _maxSpeed = 1500f;
  private float _incrementSpeed;
  private Vector2 stickL;
  private Vector2 stickR;
  private float _duration = 0.15f;
  private float _timer;
  private Vector2 oldVector = Vector2.zero;
  private Vector2 mouseTranslation;
  private Vector2 movVector;
  private Vector2 zoomVector;
  private Vector2 keyVector;

  private void Awake()
  {
    this.actionsController = new ActionsController();
    this.pointerEventData = new PointerEventData(EventSystem.current);
    this.raycastResultList = new List<RaycastResult>();
    this.keyVector = Vector2.zero;
  }

  private void Start()
  {
    this._incrementSpeed = this._maxSpeed * 0.01f;
    this.mouseTranslation = new Vector2();
    this.movVector = new Vector2();
  }

  private void Update()
  {
    if (Gamepad.current == null || SettingsManager.Instance.IsActive())
      return;
    this._timer += Time.deltaTime;
    this.stickR = Gamepad.current.rightStick.ReadValue();
    if ((bool) (UnityEngine.Object) KeyboardManager.Instance && KeyboardManager.Instance.IsChat())
    {
      if ((double) Mathf.Abs(this.stickR.y) <= 0.10000000149011612)
        return;
      if ((double) this.stickR.y > 0.0)
        KeyboardManager.Instance.DoScroll(true);
      else
        KeyboardManager.Instance.DoScroll(false);
    }
    else if ((double) Mathf.Abs(this.stickR.x) > 0.25 || (double) Mathf.Abs(this.stickR.y) > 0.25)
    {
      if ((double) this._mouseSpeed < (double) this._maxSpeed)
        this._mouseSpeed += this._incrementSpeed;
      this.mouseTranslation.x = Mathf.Clamp(Mouse.current.position.ReadValue().x + this._mouseSpeed * this.stickR.x * Time.deltaTime, 0.0f, (float) Screen.width);
      this.mouseTranslation.y = Mathf.Clamp(Mouse.current.position.ReadValue().y + this._mouseSpeed * this.stickR.y * Time.deltaTime, 0.0f, (float) Screen.height);
      Mouse.current.WarpCursorPosition(this.mouseTranslation);
    }
    else
    {
      if ((double) this._mouseSpeed > 0.0)
        this._mouseSpeed = 0.0f;
      this.stickL = Gamepad.current.leftStick.ReadValue();
      this.DoMovementVector(this.stickL);
    }
  }

  private void OnEnable()
  {
    this.actionsController.Player.Movement.Enable();
    this.actionsController.Player.Fire.Enable();
    this.actionsController.Player.Escape.Enable();
    this.actionsController.Player.KeyBinding.Enable();
    this.actionsController.Player.Zoom.Enable();
    this.actionsController.Player.Movement.performed += new Action<InputAction.CallbackContext>(this.DoMovement);
    this.actionsController.Player.Fire.performed += new Action<InputAction.CallbackContext>(this.DoFire);
    this.actionsController.Player.Escape.performed += new Action<InputAction.CallbackContext>(this.DoEscape);
    this.actionsController.Player.KeyBinding.performed += new Action<InputAction.CallbackContext>(this.DoKeyBinding);
    this.actionsController.Player.Zoom.performed += new Action<InputAction.CallbackContext>(this.DoZoom);
  }

  private void OnDisable()
  {
    this.actionsController.Player.Movement.Disable();
    this.actionsController.Player.Fire.Disable();
    this.actionsController.Player.Escape.Disable();
    this.actionsController.Player.KeyBinding.Disable();
    this.actionsController.Player.Zoom.Disable();
  }

  private void DoShoulder(bool _isRight)
  {
    if ((bool) (UnityEngine.Object) KeyboardManager.Instance && KeyboardManager.Instance.IsActive())
    {
      KeyboardManager.Instance.DoShift();
    }
    else
    {
      if (AlertManager.Instance.IsActive())
        return;
      if (TomeManager.Instance.IsActive())
        TomeManager.Instance.ControllerMoveShoulder(_isRight);
      else if ((bool) (UnityEngine.Object) HeroSelectionManager.Instance)
        HeroSelectionManager.Instance.ControllerMoveBlock(_isRight);
      else if ((bool) (UnityEngine.Object) ChallengeSelectionManager.Instance)
        CardCraftManager.Instance.ControllerMoveShoulder(_isRight);
      else if ((bool) (UnityEngine.Object) TownManager.Instance)
      {
        if (TownManager.Instance.characterWindow.IsActive())
          TownManager.Instance.characterWindow.ControllerMoveShoulder(_isRight);
        else if ((bool) (UnityEngine.Object) CardCraftManager.Instance)
          CardCraftManager.Instance.ControllerMoveShoulder(_isRight);
        else
          TownManager.Instance.ControllerMoveBlock(_isRight);
      }
      else if ((bool) (UnityEngine.Object) MapManager.Instance)
      {
        if (MapManager.Instance.characterWindow.IsActive())
          MapManager.Instance.characterWindow.ControllerMoveShoulder(_isRight);
        else if ((bool) (UnityEngine.Object) CardCraftManager.Instance)
        {
          if (_isRight)
            CardCraftManager.Instance.ControllerMoveShoulder(true);
          else
            MapManager.Instance.characterWindow.ControllerMoveShoulder();
        }
        else
          MapManager.Instance.ControllerMoveBlock(_isRight);
      }
      else if ((bool) (UnityEngine.Object) MatchManager.Instance)
      {
        if (MatchManager.Instance.characterWindow.IsActive())
          MatchManager.Instance.characterWindow.ControllerMoveShoulder(_isRight);
        else
          MatchManager.Instance.ControllerMoveShoulder(_isRight);
      }
      else if ((bool) (UnityEngine.Object) LootManager.Instance)
      {
        if (LootManager.Instance.characterWindowUI.IsActive())
          LootManager.Instance.characterWindowUI.ControllerMoveShoulder(_isRight);
        else
          LootManager.Instance.ControllerMoveShoulder(_isRight);
      }
      else
      {
        if (!(bool) (UnityEngine.Object) RewardsManager.Instance)
          return;
        Debug.Log((object) ("HERE -> " + RewardsManager.Instance.characterWindowUI.IsActive().ToString()));
        if (RewardsManager.Instance.characterWindowUI.IsActive())
          RewardsManager.Instance.characterWindowUI.ControllerMoveShoulder(_isRight);
        else
          RewardsManager.Instance.ControllerMoveShoulder(_isRight);
      }
    }
  }

  private void DoTrigger(bool _isRight)
  {
    if (KeyboardManager.Instance.IsActive() || AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive() || CardScreenManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || TomeManager.Instance.IsActive() || !OptionsManager.Instance.IsActive())
      return;
    OptionsManager.Instance.InputMoveController(_isRight);
  }

  private void DoButtonNorth()
  {
    if ((bool) (UnityEngine.Object) KeyboardManager.Instance && KeyboardManager.Instance.IsActive())
    {
      KeyboardManager.Instance.DoDelete();
    }
    else
    {
      RaycastHit2D raycastHit2D = Physics2D.Raycast((Vector2) GameManager.Instance.cameraMain.ScreenToWorldPoint((Vector3) Mouse.current.position.ReadValue()), Vector2.zero);
      if (!((UnityEngine.Object) raycastHit2D.collider != (UnityEngine.Object) null) || !((UnityEngine.Object) raycastHit2D.transform != (UnityEngine.Object) null))
        return;
      HeroSelection component1;
      if ((bool) (UnityEngine.Object) HeroSelectionManager.Instance && raycastHit2D.collider.TryGetComponent<HeroSelection>(out component1))
      {
        component1.RightClick();
      }
      else
      {
        CardItem component2;
        if (!raycastHit2D.collider.TryGetComponent<CardItem>(out component2))
          return;
        component2.RightClick();
      }
    }
  }

  private void DoButtonEast() => GameManager.Instance.EscapeFunction();

  private void DoButtonWest()
  {
    if (!(bool) (UnityEngine.Object) KeyboardManager.Instance || !GameManager.Instance.IsMultiplayer() || !NetworkManager.Instance.IsConnected())
      return;
    if (!KeyboardManager.Instance.IsActive())
      KeyboardManager.Instance.ShowKeyboard(true, true);
    else
      KeyboardManager.Instance.ShowKeyboard(false);
  }

  private void DoKeyBinding(InputAction.CallbackContext _context)
  {
    if (Keyboard.current != null && GameManager.Instance.ConfigKeyboardShortcuts)
    {
      if (_context.control == Keyboard.current[Key.Digit0])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(0);
      }
      else if (_context.control == Keyboard.current[Key.Numpad0])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(0);
      }
      else if (_context.control == Keyboard.current[Key.Digit1])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(1);
      }
      else if (_context.control == Keyboard.current[Key.Numpad1])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(1);
      }
      else if (_context.control == Keyboard.current[Key.Digit2])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(2);
      }
      else if (_context.control == Keyboard.current[Key.Numpad2])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(2);
      }
      else if (_context.control == Keyboard.current[Key.Digit3])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(3);
      }
      else if (_context.control == Keyboard.current[Key.Numpad3])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(3);
      }
      else if (_context.control == Keyboard.current[Key.Digit4])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(4);
      }
      else if (_context.control == Keyboard.current[Key.Numpad4])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(4);
      }
      else if (_context.control == Keyboard.current[Key.Digit5])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(5);
      }
      else if (_context.control == Keyboard.current[Key.Numpad5])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(5);
      }
      else if (_context.control == Keyboard.current[Key.Digit6])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(6);
      }
      else if (_context.control == Keyboard.current[Key.Numpad6])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(6);
      }
      else if (_context.control == Keyboard.current[Key.Digit7])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(7);
      }
      else if (_context.control == Keyboard.current[Key.Numpad7])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(7);
      }
      else if (_context.control == Keyboard.current[Key.Digit8])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(8);
      }
      else if (_context.control == Keyboard.current[Key.Numpad8])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(8);
      }
      else if (_context.control == Keyboard.current[Key.Digit9])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(9);
      }
      else if (_context.control == Keyboard.current[Key.Numpad9])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardNum(9);
      }
      else if (_context.control == Keyboard.current[Key.Space])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardSpace();
      }
      else if (_context.control == Keyboard.current[Key.R])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardEmote(0);
      }
      else if (_context.control == Keyboard.current[Key.E])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardEmote(1);
      }
      else if (_context.control == Keyboard.current[Key.S])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardEmote(2);
      }
      else if (_context.control == Keyboard.current[Key.A])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardEmote(3);
      }
      else if (_context.control == Keyboard.current[Key.W])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardEmote(4);
      }
      else if (_context.control == Keyboard.current[Key.Q])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardEmote(5);
      }
      else if (_context.control == Keyboard.current[Key.NumpadEnter] || _context.control == Keyboard.current[Key.Enter])
      {
        if ((bool) (UnityEngine.Object) MatchManager.Instance)
          MatchManager.Instance.KeyboardEnter();
      }
      else if (_context.control == Keyboard.current[Key.LeftCtrl] || _context.control == Keyboard.current[Key.RightCtrl])
        this.DoFirePerformed();
      else if (_context.control == Keyboard.current[Key.LeftAlt] || _context.control == Keyboard.current[Key.RightAlt])
        this.DoButtonNorth();
      else if (_context.control == Keyboard.current[Key.PageDown])
        this.DoNextPage();
      else if (_context.control == Keyboard.current[Key.PageUp])
        this.DoPrevPage();
    }
    if (Gamepad.current != null)
    {
      if (_context.control == Gamepad.current.leftTrigger)
        this.DoTrigger(false);
      else if (_context.control == Gamepad.current.rightTrigger)
        this.DoTrigger(true);
      else if (_context.control == Gamepad.current.leftShoulder)
        this.DoShoulder(false);
      else if (_context.control == Gamepad.current.rightShoulder)
        this.DoShoulder(true);
      else if (_context.control == Gamepad.current.buttonNorth)
        this.DoButtonNorth();
      else if (_context.control == Gamepad.current.buttonEast)
        this.DoButtonEast();
      else if (_context.control == Gamepad.current.buttonWest)
        this.DoButtonWest();
    }
    if (!GameManager.Instance.GetDeveloperMode() || Keyboard.current == null)
      return;
    if (_context.control == Keyboard.current[Key.F1])
    {
      if ((bool) (UnityEngine.Object) MapManager.Instance || (bool) (UnityEngine.Object) TownManager.Instance)
      {
        AtOManager.Instance.GetHero(0).GrantExperience(150);
      }
      else
      {
        if (!(bool) (UnityEngine.Object) MatchManager.Instance)
          return;
        MatchManager.Instance.KeyboardEnergy();
      }
    }
    else if (_context.control == Keyboard.current[Key.F2])
    {
      if (!(bool) (UnityEngine.Object) MapManager.Instance && !(bool) (UnityEngine.Object) TownManager.Instance)
        return;
      AtOManager.Instance.GetHero(1).GrantExperience(150);
    }
    else if (_context.control == Keyboard.current[Key.F3])
    {
      if (!(bool) (UnityEngine.Object) MapManager.Instance && !(bool) (UnityEngine.Object) TownManager.Instance)
        return;
      AtOManager.Instance.GetHero(2).GrantExperience(150);
    }
    else if (_context.control == Keyboard.current[Key.F4])
    {
      if (!(bool) (UnityEngine.Object) MapManager.Instance && !(bool) (UnityEngine.Object) TownManager.Instance)
        return;
      AtOManager.Instance.GetHero(3).GrantExperience(150);
    }
    else if (_context.control == Keyboard.current[Key.F5])
    {
      if (!(bool) (UnityEngine.Object) MapManager.Instance && !(bool) (UnityEngine.Object) TownManager.Instance)
        return;
      AtOManager.Instance.GivePlayer(0, 400);
    }
    else if (_context.control == Keyboard.current[Key.F6])
    {
      if (!(bool) (UnityEngine.Object) MapManager.Instance && !(bool) (UnityEngine.Object) TownManager.Instance)
        return;
      AtOManager.Instance.GivePlayer(1, 400);
    }
    else if (_context.control == Keyboard.current[Key.F7])
    {
      if (!(bool) (UnityEngine.Object) MapManager.Instance && !(bool) (UnityEngine.Object) TownManager.Instance && !(bool) (UnityEngine.Object) MatchManager.Instance)
        return;
      SandboxManager.Instance.ShowSandbox();
    }
    else if (_context.control == Keyboard.current[Key.F8])
    {
      if ((bool) (UnityEngine.Object) MapManager.Instance || (bool) (UnityEngine.Object) TownManager.Instance)
      {
        GameManager.Instance.SaveGameDeveloper();
      }
      else
      {
        if (!(bool) (UnityEngine.Object) MatchManager.Instance)
          return;
        MatchManager.Instance.KeyboardDbg();
      }
    }
    else if (_context.control == Keyboard.current[Key.F9])
      NetworkManager.Instance.ShowLagSimulatorTrigger();
    else if (_context.control == Keyboard.current[Key.F10])
    {
      if (TomeManager.Instance.IsActive())
      {
        PlayerManager.Instance.UnlockHeroes();
      }
      else
      {
        if (!(bool) (UnityEngine.Object) MatchManager.Instance)
          return;
        MatchManager.Instance.KeyboardReloadCombat();
      }
    }
    else if (_context.control == Keyboard.current[Key.F11])
    {
      if (TomeManager.Instance.IsActive())
      {
        PlayerManager.Instance.UnlockCards();
      }
      else
      {
        if (!((UnityEngine.Object) MapManager.Instance != (UnityEngine.Object) null))
          return;
        MapManager.Instance.corruption.NextCorruption();
      }
    }
    else if (_context.control == Keyboard.current[Key.F12])
    {
      if ((bool) (UnityEngine.Object) MapManager.Instance)
        MapManager.Instance.ItemCreator.GetComponent<ItemCreator>().Draw();
      else if ((bool) (UnityEngine.Object) TownManager.Instance)
        TownManager.Instance.ItemCreator.GetComponent<ItemCreator>().Draw();
      else if ((bool) (UnityEngine.Object) MatchManager.Instance)
        MatchManager.Instance.CardCreator.GetComponent<CardCreator>().Draw();
      else if ((bool) (UnityEngine.Object) ChallengeSelectionManager.Instance)
      {
        AtOManager.Instance.FinishObeliskDraft();
      }
      else
      {
        if (!(bool) (UnityEngine.Object) HeroSelectionManager.Instance)
          return;
        HeroSelectionManager.Instance.weeklySelector.GetComponent<WeeklySelector>().Draw();
      }
    }
    else if (_context.control == Keyboard.current[Key.Digit1])
    {
      if (!(bool) (UnityEngine.Object) HeroSelectionManager.Instance)
        return;
      HeroSelectionManager.Instance.IncreaseHeroProgressDev(0);
    }
    else if (_context.control == Keyboard.current[Key.Digit2])
    {
      if (!(bool) (UnityEngine.Object) HeroSelectionManager.Instance)
        return;
      HeroSelectionManager.Instance.IncreaseHeroProgressDev(1);
    }
    else if (_context.control == Keyboard.current[Key.Digit3])
    {
      if (!(bool) (UnityEngine.Object) HeroSelectionManager.Instance)
        return;
      HeroSelectionManager.Instance.IncreaseHeroProgressDev(2);
    }
    else if (_context.control == Keyboard.current[Key.Digit4])
    {
      if (!(bool) (UnityEngine.Object) HeroSelectionManager.Instance)
        return;
      HeroSelectionManager.Instance.IncreaseHeroProgressDev(3);
    }
    else if (_context.control == Keyboard.current[Key.Period])
    {
      GameManager.Instance.consoleGUI.ConsoleShow();
    }
    else
    {
      if (_context.control != Keyboard.current[Key.Comma])
        return;
      GameManager.Instance.DebugShow();
    }
  }

  private void DoFire(InputAction.CallbackContext _context)
  {
    if (!_context.performed)
      return;
    this.DoFirePerformed();
  }

  private void DoFirePerformed()
  {
    if (SettingsManager.Instance.IsActive())
      return;
    this.pointerEventData.position = (Vector2) Input.mousePosition;
    this.raycastResultList.Clear();
    EventSystem.current.RaycastAll(this.pointerEventData, this.raycastResultList);
    for (int index = 0; index < this.raycastResultList.Count; ++index)
    {
      Button component1;
      if (this.raycastResultList[index].gameObject.TryGetComponent<Button>(out component1))
      {
        component1.onClick.Invoke();
        return;
      }
      Toggle component2;
      if (this.raycastResultList[index].gameObject.TryGetComponent<Toggle>(out component2))
      {
        component2.isOn = !component2.isOn;
        return;
      }
      Toggle component3;
      if (this.raycastResultList[index].gameObject.transform.parent.gameObject.TryGetComponent<Toggle>(out component3))
      {
        component3.isOn = !component3.isOn;
        return;
      }
      TMP_InputField component4;
      if (this.raycastResultList[index].gameObject.TryGetComponent<TMP_InputField>(out component4))
      {
        if (component4.gameObject.name == "ChatField")
        {
          KeyboardManager.Instance.ShowKeyboard(true, true);
          return;
        }
        KeyboardManager.Instance.ShowKeyboard(true);
        KeyboardManager.Instance.SetInputField(component4);
        return;
      }
      TMP_Dropdown component5;
      if (this.raycastResultList[index].gameObject.TryGetComponent<TMP_Dropdown>(out component5))
      {
        component5.transform.GetChild(2).transform.localPosition = new Vector3(component5.transform.GetChild(2).transform.localPosition.x, component5.transform.GetChild(2).transform.localPosition.y, -100f);
        if (component5.value < component5.options.Count - 1)
          ++component5.value;
        else
          component5.value = 0;
        component5.RefreshShownValue();
        this.StartCoroutine(this.HideDrop(component5));
        return;
      }
    }
    RaycastHit2D raycastHit2D = Physics2D.Raycast((Vector2) GameManager.Instance.cameraMain.ScreenToWorldPoint((Vector3) Mouse.current.position.ReadValue()), Vector2.zero);
    if (!((UnityEngine.Object) raycastHit2D.collider != (UnityEngine.Object) null) || !((UnityEngine.Object) raycastHit2D.transform != (UnityEngine.Object) null))
      return;
    CardItem component6;
    if ((bool) (UnityEngine.Object) MatchManager.Instance && raycastHit2D.collider.TryGetComponent<CardItem>(out component6))
    {
      component6.OnMouseUpController();
    }
    else
    {
      HeroSelection component7;
      if ((bool) (UnityEngine.Object) HeroSelectionManager.Instance && raycastHit2D.collider.TryGetComponent<HeroSelection>(out component7))
        component7.OnClickedController();
      else
        raycastHit2D.collider.gameObject.SendMessage("OnMouseUp", SendMessageOptions.DontRequireReceiver);
    }
  }

  private IEnumerator HideDrop(TMP_Dropdown dpd)
  {
    yield return (object) new WaitForSeconds(0.01f);
    dpd.Hide();
  }

  private void DoZoom(InputAction.CallbackContext _context)
  {
    this.zoomVector.x = _context.ReadValue<Vector2>().x;
    this.zoomVector.y = _context.ReadValue<Vector2>().y;
    if ((double) this.zoomVector.x == 0.0 && (double) this.zoomVector.y == 0.0)
      return;
    if (TomeManager.Instance.IsActive())
    {
      TomeManager.Instance.DoMouseScroll(this.zoomVector);
    }
    else
    {
      if (!(bool) (UnityEngine.Object) CardCraftManager.Instance)
        return;
      CardCraftManager.Instance.DoMouseScroll(this.zoomVector);
    }
  }

  private void DoMovement(InputAction.CallbackContext _context)
  {
    this.movVector.x = _context.ReadValue<Vector2>().x;
    this.movVector.y = _context.ReadValue<Vector2>().y;
    if (_context.control.device.displayName != "Keyboard")
    {
      this.DoMovementVector(this.movVector);
    }
    else
    {
      if (!GameManager.Instance.ConfigKeyboardShortcuts)
        return;
      this.DoMovementVector(this.movVector, true);
    }
  }

  private void DoMovementVector(Vector2 vct, bool fromKeyboard = false)
  {
    if ((double) Mathf.Abs(vct.x) != 1.0 && (double) Mathf.Abs(vct.y) != 1.0 && ((double) Mathf.Abs(vct.x) <= 0.550000011920929 || (double) Mathf.Abs(vct.y) <= 0.550000011920929))
      return;
    if (!fromKeyboard)
    {
      if ((double) this._timer < (double) this._duration)
        return;
      this._timer = 0.0f;
    }
    if ((double) Mathf.Abs(vct.x) > (double) Mathf.Abs(vct.y))
      vct.y = 0.0f;
    else
      vct.x = 0.0f;
    bool goingUp = false;
    bool goingRight = false;
    bool goingDown = false;
    bool goingLeft = false;
    if ((double) vct.y > 0.0)
      goingUp = true;
    else if ((double) vct.y < 0.0)
      goingDown = true;
    else if ((double) vct.x < 0.0)
      goingLeft = true;
    else
      goingRight = true;
    if (KeyboardManager.Instance.IsActive())
      KeyboardManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if (AlertManager.Instance.IsActive())
      AlertManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if (SettingsManager.Instance.IsActive())
      SettingsManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if (CardScreenManager.Instance.IsActive())
      CardScreenManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if (DamageMeterManager.Instance.IsActive())
      DamageMeterManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if (TomeManager.Instance.IsActive())
      TomeManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if ((bool) (UnityEngine.Object) MainMenuManager.Instance)
      MainMenuManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if (PerkTree.Instance.IsActive())
      PerkTree.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if (SandboxManager.Instance.IsActive())
      SandboxManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if (MadnessManager.Instance.IsActive())
      MadnessManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if ((bool) (UnityEngine.Object) LobbyManager.Instance)
      LobbyManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if ((bool) (UnityEngine.Object) HeroSelectionManager.Instance)
    {
      if (HeroSelectionManager.Instance.charPopup.IsOpened())
        HeroSelectionManager.Instance.charPopup.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
      else
        HeroSelectionManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    }
    else if (GiveManager.Instance.IsActive())
      GiveManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if ((bool) (UnityEngine.Object) IntroNewGameManager.Instance)
      IntroNewGameManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if ((bool) (UnityEngine.Object) CinematicManager.Instance)
      CinematicManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if ((bool) (UnityEngine.Object) ChallengeSelectionManager.Instance)
      CardCraftManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if ((bool) (UnityEngine.Object) CardPlayerManager.Instance)
      CardPlayerManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if ((bool) (UnityEngine.Object) CardPlayerPairsManager.Instance)
      CardPlayerPairsManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    else if ((bool) (UnityEngine.Object) EventManager.Instance)
    {
      if (MapManager.Instance.characterWindow.IsActive())
        MapManager.Instance.characterWindow.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
      else if ((UnityEngine.Object) MapManager.Instance.Conflict != (UnityEngine.Object) null && MapManager.Instance.Conflict.IsActive())
        MapManager.Instance.Conflict.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
      else
        EventManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    }
    else if ((bool) (UnityEngine.Object) TownManager.Instance)
    {
      if (TownManager.Instance.characterWindow.IsActive())
        TownManager.Instance.characterWindow.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
      else if ((bool) (UnityEngine.Object) CardCraftManager.Instance)
        CardCraftManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
      else
        TownManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    }
    else if ((bool) (UnityEngine.Object) MapManager.Instance)
    {
      if (MapManager.Instance.characterWindow.IsActive())
        MapManager.Instance.characterWindow.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
      else if ((UnityEngine.Object) MapManager.Instance.Conflict != (UnityEngine.Object) null && MapManager.Instance.Conflict.IsActive())
        MapManager.Instance.Conflict.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
      else if ((bool) (UnityEngine.Object) CardCraftManager.Instance)
        CardCraftManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
      else
        MapManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    }
    else if ((bool) (UnityEngine.Object) RewardsManager.Instance)
    {
      if (RewardsManager.Instance.characterWindowUI.IsActive())
        RewardsManager.Instance.characterWindowUI.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
      else
        RewardsManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    }
    else if ((bool) (UnityEngine.Object) LootManager.Instance)
    {
      if (LootManager.Instance.characterWindowUI.IsActive())
        LootManager.Instance.characterWindowUI.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
      else
        LootManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    }
    else if ((bool) (UnityEngine.Object) FinishRunManager.Instance)
    {
      FinishRunManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    }
    else
    {
      if (!(bool) (UnityEngine.Object) MatchManager.Instance)
        return;
      if (MatchManager.Instance.characterWindow.IsActive())
        MatchManager.Instance.characterWindow.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
      else
        MatchManager.Instance.ControllerMovement(goingUp, goingRight, goingDown, goingLeft);
    }
  }

  private void DoEscape(InputAction.CallbackContext _context) => GameManager.Instance.EscapeFunction();

  private void DoNextPage()
  {
    if (TomeManager.Instance.IsActive())
    {
      TomeManager.Instance.DoNextPage();
    }
    else
    {
      if (!(bool) (UnityEngine.Object) CardCraftManager.Instance)
        return;
      CardCraftManager.Instance.ControllerNextPage();
    }
  }

  private void DoPrevPage()
  {
    if (TomeManager.Instance.IsActive())
    {
      TomeManager.Instance.DoPrevPage();
    }
    else
    {
      if (!(bool) (UnityEngine.Object) CardCraftManager.Instance)
        return;
      CardCraftManager.Instance.ControllerNextPage(false);
    }
  }
}
