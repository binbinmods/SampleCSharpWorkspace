// Decompiled with JetBrains decompiler
// Type: EmoteManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;

public class EmoteManager : MonoBehaviour
{
  public EmoteSmall[] emotes;
  public Sprite[] emotesSprite;
  private CircleCollider2D collider;
  private Coroutine hideCo;
  public SpriteRenderer characterPortrait;
  public SpriteRenderer characterPortraitBlocked;
  public Transform emoteText;
  public int heroActive = -1;
  public Transform blockedT;
  public TMP_Text blockedCounter;
  private bool blocked;
  private int counter;
  private int blockedTimeout = 3;
  private Vector3 posIni;
  private Vector3 posIniBlocked;
  private Coroutine blockedCo;
  private bool initiated;

  private void Awake()
  {
    this.collider = this.GetComponent<CircleCollider2D>();
    this.posIni = this.characterPortrait.transform.localPosition;
    this.posIniBlocked = this.characterPortraitBlocked.transform.parent.transform.localPosition;
  }

  private void Start()
  {
    for (int _action = 0; _action < this.emotes.Length; ++_action)
      this.emotes[_action].SetAction(_action);
    this.HideEmotes();
  }

  public void Init()
  {
    if (this.initiated)
      return;
    this.SelectNextCharacter();
    this.initiated = true;
  }

  public void SetBlocked(bool _state)
  {
    this.blocked = _state;
    if (!((Object) this.blockedT != (Object) null))
      return;
    if (_state)
    {
      this.emoteText.gameObject.SetActive(false);
      this.blockedT.gameObject.SetActive(true);
      this.SetCounter();
    }
    else
    {
      this.emoteText.gameObject.SetActive(true);
      this.blockedT.gameObject.SetActive(false);
    }
  }

  public bool IsBlocked() => this.blocked;

  private void SetCounter()
  {
    if (this.blockedCo != null)
      this.StopCoroutine(this.blockedCo);
    this.counter = this.blockedTimeout;
    this.blockedCo = this.StartCoroutine(this.SetCounterCo());
  }

  private IEnumerator SetCounterCo()
  {
    for (; this.counter > 0; --this.counter)
    {
      if ((Object) this.blockedCounter != (Object) null)
        this.blockedCounter.text = this.counter.ToString();
      yield return (object) Globals.Instance.WaitForSeconds(1f);
    }
    this.SetBlocked(false);
  }

  private void SelectNextCharacter()
  {
    Hero[] teamHero = MatchManager.Instance.GetTeamHero();
    if (teamHero != null)
    {
      bool flag = false;
      int num = 0;
      while (!flag && num < 100)
      {
        ++num;
        ++this.heroActive;
        if (this.heroActive > 3)
          this.heroActive = 0;
        if (teamHero[this.heroActive] != null && (teamHero[this.heroActive].Owner == NetworkManager.Instance.GetPlayerNick() || teamHero[this.heroActive].Owner == "") || !GameManager.Instance.IsMultiplayer())
          flag = true;
      }
      if (flag && teamHero[this.heroActive] != null && (Object) teamHero[this.heroActive].HeroData != (Object) null)
      {
        this.characterPortrait.sprite = this.characterPortraitBlocked.sprite = teamHero[this.heroActive].HeroData.HeroSubClass.StickerBase;
        this.characterPortrait.transform.localPosition = this.posIni + new Vector3(teamHero[this.heroActive].HeroData.HeroSubClass.StickerOffsetX, 0.0f, 0.0f);
        this.characterPortraitBlocked.transform.parent.transform.localPosition = this.posIniBlocked + new Vector3(teamHero[this.heroActive].HeroData.HeroSubClass.StickerOffsetX, 0.0f, 0.0f);
      }
    }
    if (teamHero[this.heroActive] == null || !((Object) teamHero[this.heroActive].HeroData != (Object) null))
      return;
    if (teamHero[this.heroActive].Alive)
    {
      for (int index = 0; index < this.emotes.Length; ++index)
        this.emotes[index].SetBlocked(false);
    }
    else
    {
      for (int _action = 0; _action < this.emotes.Length; ++_action)
      {
        if (!this.EmoteNeedsTarget(_action))
          this.emotes[_action].SetBlocked(true);
      }
    }
  }

  private void SetEmotesForThisCharacter()
  {
    for (int _action = 0; _action < this.emotes.Length; ++_action)
      this.emotes[_action].SetAction(_action);
  }

  public bool EmoteNeedsTarget(int _action) => _action == 2 || _action == 3;

  private void OnMouseEnter() => this.OnMouseEnterF();

  private void OnMouseOver()
  {
    if (this.blocked || this.emotes[0].gameObject.activeSelf)
      return;
    this.ShowEmotes();
  }

  private void OnMouseEnterF()
  {
    if (this.blocked || MatchManager.Instance.waitingDeathScreen || MatchManager.Instance.WaitingForActionScreen())
      return;
    this.ShowEmotes();
  }

  private void OnMouseExit()
  {
    if (this.blocked)
      return;
    this.HideEmotesCo();
  }

  private void OnMouseUp()
  {
    if (this.blocked)
      return;
    this.SelectNextCharacter();
  }

  public void HideEmotesCo()
  {
    if (this.hideCo != null)
      this.StopCoroutine(this.hideCo);
    this.hideCo = this.StartCoroutine(this.HideEmotesCoAction());
  }

  private IEnumerator HideEmotesCoAction()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.2f);
    this.HideEmotes();
  }

  public void HideEmotes()
  {
    for (int index = 0; index < this.emotes.Length; ++index)
      this.emotes[index].Hide();
    this.collider.radius = 0.48f;
    this.emoteText.gameObject.SetActive(true);
  }

  public void ShowEmotes()
  {
    if (this.hideCo != null)
      this.StopCoroutine(this.hideCo);
    if (!this.emotes[0].gameObject.activeSelf)
    {
      for (int index = 0; index < this.emotes.Length; ++index)
        this.emotes[index].Show();
    }
    this.collider.radius = 1.4f;
    this.emoteText.gameObject.SetActive(false);
  }
}
