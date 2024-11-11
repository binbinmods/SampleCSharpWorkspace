// Decompiled with JetBrains decompiler
// Type: BotonRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;

public class BotonRollover : MonoBehaviour
{
  public Transform image;
  private SpriteRenderer imageBanner;
  public AudioClip sound;
  public Transform particles;
  public Transform rollOverText;
  public int auxInt;
  public bool fadeOnRoll = true;
  private TMP_Text textTMP;
  private float textY;
  private Sprite imgOriginal;
  private Vector3 imgSizeOriginal;
  private SpriteRenderer SR;
  private Coroutine Co;
  private float posEndTop;
  private float posEndBottom;

  private void Awake()
  {
    if (!((Object) this.image != (Object) null))
      return;
    this.SR = this.image.GetComponent<SpriteRenderer>();
  }

  private void Start()
  {
    if ((Object) this.SR != (Object) null)
      this.imgOriginal = this.SR.sprite;
    if ((Object) this.image != (Object) null)
    {
      this.imgSizeOriginal = this.image.localScale;
      if (this.gameObject.name != "BotStats" && this.gameObject.name != "BotPerks" && this.image.childCount > 0 && (Object) this.image.GetChild(0) != (Object) null && (Object) this.image.GetChild(0).GetComponent<SpriteRenderer>() != (Object) null)
      {
        this.imageBanner = this.image.GetChild(0).GetComponent<SpriteRenderer>();
        this.imageBanner.color = new Color(0.7f, 0.7f, 0.7f, 0.9f);
      }
    }
    if (!((Object) this.rollOverText != (Object) null))
      return;
    this.textTMP = this.rollOverText.GetComponent<TMP_Text>();
    this.textY = this.rollOverText.localPosition.y;
    this.posEndTop = this.textY + 0.05f;
    this.posEndBottom = this.textY;
  }

  private void fRollOver()
  {
    GameManager.Instance.SetCursorHover();
    if ((Object) this.sound != (Object) null)
      GameManager.Instance.PlayAudio(this.sound);
    int num = (Object) this.particles != (Object) null ? 1 : 0;
    if ((Object) this.rollOverText != (Object) null)
      this.Co = this.StartCoroutine(this.ShowText(true));
    if (!((Object) this.image != (Object) null))
      return;
    this.image.localScale = this.imgSizeOriginal + new Vector3(0.1f, 0.1f, 0.1f);
    if (!((Object) this.imageBanner != (Object) null))
      return;
    this.imageBanner.color = new Color(0.86f, 0.58f, 0.43f, 0.9f);
  }

  private void fRollOut()
  {
    GameManager.Instance.SetCursorPlain();
    if ((Object) this.particles != (Object) null)
      this.particles.gameObject.SetActive(false);
    if ((Object) this.rollOverText != (Object) null)
      this.Co = this.StartCoroutine(this.ShowText(false));
    if (!((Object) this.image != (Object) null))
      return;
    this.image.localScale = this.imgSizeOriginal;
    if (!((Object) this.imageBanner != (Object) null))
      return;
    this.imageBanner.color = new Color(0.7f, 0.7f, 0.7f, 0.9f);
  }

  private IEnumerator ShowText(bool state)
  {
    BotonRollover botonRollover = this;
    if (botonRollover.Co != null)
      botonRollover.StopCoroutine(botonRollover.Co);
    float num = !state ? botonRollover.posEndBottom : botonRollover.posEndTop;
    int steps = 10;
    float step = (num - botonRollover.rollOverText.localPosition.y) / (float) steps;
    for (int i = 0; i < steps; ++i)
    {
      botonRollover.rollOverText.localPosition += new Vector3(0.0f, step, 0.0f);
      if (botonRollover.fadeOnRoll)
      {
        if (state)
        {
          if ((double) botonRollover.textTMP.color.a < 1.0)
            botonRollover.textTMP.color = new Color(botonRollover.textTMP.color.r, botonRollover.textTMP.color.g, botonRollover.textTMP.color.b, botonRollover.textTMP.color.a + 0.1f);
        }
        else if ((double) botonRollover.textTMP.color.a > 0.0)
          botonRollover.textTMP.color = new Color(botonRollover.textTMP.color.r, botonRollover.textTMP.color.g, botonRollover.textTMP.color.b, botonRollover.textTMP.color.a - 0.1f);
      }
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    }
  }

  private void CloseWindows(string botName)
  {
    if (!(bool) (Object) MatchManager.Instance)
      return;
    if (MatchManager.Instance.characterWindow.IsActive())
      MatchManager.Instance.characterWindow.Hide();
    if (!(botName != "OptionsLog") || !((Object) MatchManager.Instance != (Object) null) || !MatchManager.Instance.console.IsActive())
      return;
    MatchManager.Instance.ShowLog();
  }

  public void OnMouseUp()
  {
    if (!Functions.ClickedThisTransform(this.transform) || (bool) (Object) MatchManager.Instance && MatchManager.Instance.CombatLoading || AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || (bool) (Object) MapManager.Instance && MapManager.Instance.IsCharacterUnlock() || (bool) (Object) MatchManager.Instance && MatchManager.Instance.console.IsActive())
      return;
    string name = this.gameObject.name;
    this.CloseWindows(name);
    switch (name)
    {
      case "OptionsSettings":
        SettingsManager.Instance.ShowSettings(true);
        break;
      case "OptionsExit":
        OptionsManager.Instance.Exit();
        break;
      case "OptionsTome":
        TomeManager.Instance.ShowTome(true);
        break;
      case "OptionsResign":
        if ((Object) MatchManager.Instance != (Object) null)
        {
          MatchManager.Instance.ResignCombat();
          break;
        }
        break;
      case "OptionsStats":
        DamageMeterManager.Instance.Show();
        break;
      case "OptionsRetry":
        if ((Object) MatchManager.Instance != (Object) null)
        {
          MatchManager.Instance.ALL_BreakByDesync();
          break;
        }
        break;
      case "MadnessTransform":
        MadnessManager.Instance.ShowMadness();
        break;
      case "CharacterDeck":
        if (CardScreenManager.Instance.IsActive())
          return;
        if ((Object) RewardsManager.Instance != (Object) null)
        {
          RewardsManager.Instance.ShowDeck(this.auxInt);
          break;
        }
        if ((Object) LootManager.Instance != (Object) null)
        {
          LootManager.Instance.ShowDeck(this.auxInt);
          break;
        }
        if ((Object) TownManager.Instance != (Object) null)
        {
          TownManager.Instance.ShowDeck(this.auxInt);
          break;
        }
        if ((Object) MapManager.Instance != (Object) null)
        {
          MapManager.Instance.ShowDeck(this.auxInt);
          break;
        }
        break;
      case "OptionsLog":
        MatchManager.Instance.ShowLog();
        break;
    }
    this.fRollOut();
  }

  private void OnMouseExit() => this.fRollOut();

  private void OnMouseEnter()
  {
    if ((bool) (Object) MatchManager.Instance && MatchManager.Instance.CombatLoading || AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || (bool) (Object) MapManager.Instance && MapManager.Instance.IsCharacterUnlock() || (bool) (Object) MatchManager.Instance && MatchManager.Instance.console.IsActive())
      return;
    this.fRollOver();
  }
}
