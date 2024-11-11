// Decompiled with JetBrains decompiler
// Type: BotonEndTurn
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BotonEndTurn : MonoBehaviour
{
  public Transform border;
  public Transform background;
  private SpriteRenderer backgroundSR;
  private SpriteRenderer borderSR;
  private Color sourceColor;
  private Color bgColor;
  private bool show;
  private bool buttonEnabled = true;

  private void Awake()
  {
    this.borderSR = this.border.GetComponent<SpriteRenderer>();
    this.backgroundSR = this.background.GetComponent<SpriteRenderer>();
    this.bgColor = this.backgroundSR.color;
    this.sourceColor = new Color(this.borderSR.color.r, this.borderSR.color.g, this.borderSR.color.b, 0.0f);
  }

  private void Update()
  {
    if (!this.buttonEnabled)
      return;
    if (this.show && (double) this.borderSR.color.a < 0.699999988079071)
    {
      this.borderSR.color += new Color(0.0f, 0.0f, 0.0f, 0.1f);
    }
    else
    {
      if (this.show || (double) this.borderSR.color.a <= 0.0)
        return;
      this.borderSR.color -= new Color(0.0f, 0.0f, 0.0f, 0.1f);
    }
  }

  public void Enable(bool changeBg = true)
  {
    this.buttonEnabled = false;
    if (!changeBg)
      return;
    this.backgroundSR.color = new Color(this.bgColor.r, this.bgColor.g, this.bgColor.b, 1f);
  }

  public void Disable(bool changeBg = true)
  {
    this.buttonEnabled = false;
    if (!changeBg)
      return;
    this.backgroundSR.color = new Color(this.bgColor.r * 0.7f, this.bgColor.g * 0.7f, this.bgColor.b * 0.7f, 1f);
  }

  private void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive() || !this.buttonEnabled)
      return;
    this.transform.localScale = new Vector3(1.02f, 1.02f, 1f);
    GameManager.Instance.SetCursorHover();
    this.borderSR.color = this.sourceColor;
    this.show = true;
  }

  private void OnMouseExit()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive())
      return;
    GameManager.Instance.SetCursorPlain();
    if (!this.buttonEnabled)
      return;
    this.transform.localScale = new Vector3(1f, 1f, 1f);
    this.show = false;
  }

  public void OnMouseUp()
  {
    if (!Functions.ClickedThisTransform(this.transform) || AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive())
      return;
    GameManager.Instance.SetCursorPlain();
    if (!this.buttonEnabled || EventSystem.current.IsPointerOverGameObject())
      return;
    this.show = false;
    Scene activeScene = SceneManager.GetActiveScene();
    string name = this.gameObject.name;
    if (activeScene.name == "Combat")
    {
      if (MatchManager.Instance.CardDrag)
        return;
      MatchManager.Instance.EndTurn();
    }
    else if (activeScene.name == "Game")
    {
      if (name == "SinglePlayer")
        SceneManager.LoadScene("TeamManagement");
      else
        SceneManager.LoadScene("Lobby");
    }
    else if (activeScene.name == "TeamManagement")
    {
      TeamManagement.Instance.Draw();
    }
    else
    {
      if (!(activeScene.name == "Lobby"))
        return;
      Debug.Log((object) this.gameObject.name);
      switch (name)
      {
        case "ButtonMultiplayerCreate":
          LobbyManager.Instance.ShowCreate();
          break;
        case "ButtonMultiplayerJoin":
          LobbyManager.Instance.ShowJoin();
          break;
        case "ButtonMultiplayerBack":
          LobbyManager.Instance.GoBack();
          break;
        case "SetReady":
          LobbyManager.Instance.SetReady();
          break;
        case "AllUnready":
          LobbyManager.Instance.AllUnready();
          break;
      }
    }
  }
}
