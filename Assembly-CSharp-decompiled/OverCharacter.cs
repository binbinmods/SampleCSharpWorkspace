// Decompiled with JetBrains decompiler
// Type: OverCharacter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class OverCharacter : MonoBehaviour
{
  private Animator anim;
  public SpriteRenderer shadowSPR;
  public Transform shadowOver;
  public Transform shadowHover;
  public Transform borderActive;
  public Transform lifeMod;
  public TMP_Text lifeModTxt;
  public Transform lifeModOk;
  public Transform lifeModKo;
  public Transform expMod;
  public Transform upgradeMod;
  public SpriteRenderer characterSR;
  public TMP_Text nameText;
  public TMP_Text hpText;
  public TMP_Text experienceText;
  public Transform deckT;
  public TMP_Text deckText;
  public Transform injuryT;
  public TMP_Text injuryText;
  public OverCharacterDeck overDeckDeck;
  public OverCharacterDeck overDeckInjury;
  public Transform mpMark;
  public SpriteRenderer mpMarkSPR;
  public SpriteRenderer mpMarkLife;
  private int currentHp = -1;
  private Hero hero;
  private bool active;
  private bool enabled = true;
  private bool cardEnabled = true;
  private bool clickable;
  private bool inCharacterScreen;
  private int charIndex;
  private Color colorShadow = new Color(0.15f, 0.15f, 0.15f, 0.7f);
  private Color colorShadowActive = new Color(0.0f, 0.6f, 1f, 0.7f);
  public Transform challengeReadyButtons;
  public Transform challengeReadyOk;
  public Transform challengeReadyKo;

  private void Awake() => this.anim = this.GetComponent<Animator>();

  public void Init(int index)
  {
    this.charIndex = index;
    this.lifeMod.gameObject.SetActive(false);
    this.upgradeMod.gameObject.SetActive(false);
    this.challengeReadyButtons.gameObject.SetActive(false);
    this.overDeckDeck.SetIndex(index);
    this.overDeckInjury.SetIndex(index);
    this.hero = AtOManager.Instance.GetHero(index);
    if (this.hero == null || (Object) this.hero.HeroData == (Object) null)
      return;
    this.characterSR.sprite = this.hero.HeroData.HeroSubClass.SpriteSpeed;
    this.nameText.text = this.hero.SourceName;
    this.DoStats();
    this.DoCards();
    this.SetClickable(true);
    this.ShowLevelUp();
  }

  public void ShowChallengeButtonReady(bool state)
  {
    this.challengeReadyButtons.gameObject.SetActive(true);
    if (state)
    {
      this.challengeReadyOk.gameObject.SetActive(true);
      this.challengeReadyKo.gameObject.SetActive(false);
    }
    else
    {
      this.challengeReadyKo.gameObject.SetActive(true);
      this.challengeReadyOk.gameObject.SetActive(false);
    }
  }

  public Vector3 CharacterIconPosition()
  {
    if ((Object) this.characterSR != (Object) null)
      return this.characterSR.transform.position;
    Debug.LogError((object) "[CharacterIconPosition] characterSR = null");
    return Vector3.zero;
  }

  public void DoCards()
  {
    int num1 = 0;
    int num2 = 0;
    for (int index = 0; index < this.hero.Cards.Count; ++index)
    {
      CardData cardData = Globals.Instance.GetCardData(this.hero.Cards[index], false);
      if ((Object) cardData != (Object) null)
      {
        if (cardData.CardClass != Enums.CardClass.Injury)
          ++num1;
        else
          ++num2;
      }
    }
    this.deckText.text = num1.ToString();
    if (num2 == 0)
    {
      this.injuryT.gameObject.SetActive(false);
    }
    else
    {
      this.injuryText.text = num2.ToString();
      this.injuryT.gameObject.SetActive(true);
    }
  }

  public void DoStats()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(this.hero.HpCurrent);
    stringBuilder.Append("<size=-.5><color=#FFA7A5>/");
    stringBuilder.Append(this.hero.Hp);
    stringBuilder.Append("</color></size>");
    this.hpText.text = stringBuilder.ToString();
    if (this.currentHp != -1 && this.currentHp != this.hero.HpCurrent)
      this.ShowLifeMod(this.hero.HpCurrent - this.currentHp);
    this.currentHp = this.hero.HpCurrent;
    stringBuilder.Clear();
    stringBuilder.Append("L");
    stringBuilder.Append(this.hero.Level);
    if (this.hero.Level < 5)
    {
      stringBuilder.Append("  <voffset=.15><size=-.5><color=#FFC086>[");
      stringBuilder.Append(this.hero.Experience);
      stringBuilder.Append("/");
      stringBuilder.Append(Globals.Instance.GetExperienceByLevel(this.hero.Level));
      stringBuilder.Append("]");
    }
    this.experienceText.text = stringBuilder.ToString();
    this.ShowLevelUp();
    this.mpMark.gameObject.SetActive(true);
    this.mpMarkSPR.color = GameManager.Instance.IsMultiplayer() ? Functions.HexToColor(NetworkManager.Instance.GetColorFromNick(this.hero.Owner)) : Functions.HexToColor(Globals.Instance.ClassColor["warrior"]);
    this.SetMPBarLife();
  }

  private void SetMPBarLife()
  {
    if (this.hero.HpCurrent < this.hero.Hp)
      this.mpMarkLife.size = new Vector2(6f, (float) (4.0 - (double) this.hero.HpCurrent / (double) this.hero.Hp * 100.0 * 4.0 / 100.0));
    else
      this.mpMarkLife.size = new Vector2(6f, 0.0f);
  }

  public void EnableCards(bool status)
  {
  }

  public void ShowActiveStatus(bool status)
  {
    this.active = status;
    if (status)
    {
      this.shadowSPR.color = this.colorShadowActive;
      this.shadowHover.gameObject.SetActive(false);
      this.challengeReadyButtons.transform.localPosition = new Vector3(1.79f, -0.35f, 1f);
    }
    else
    {
      this.shadowSPR.color = this.colorShadow;
      this.challengeReadyButtons.transform.localPosition = new Vector3(1.79f, -0.35f, 1f);
    }
  }

  public void SetActive(bool status)
  {
    this.ShowActiveStatus(status);
    if (status && (Object) ChallengeSelectionManager.Instance != (Object) null)
      ChallengeSelectionManager.Instance.ChangeCharacter(this.charIndex);
    if ((!((Object) MapManager.Instance != (Object) null) || !MapManager.Instance.characterWindow.IsActive()) && (Object) CardCraftManager.Instance != (Object) null && CardCraftManager.Instance.craftType != 3 || !this.active)
      return;
    if ((Object) MapManager.Instance != (Object) null)
      MapManager.Instance.ShowCharacterWindow(heroIndex: this.charIndex);
    else if ((Object) TownManager.Instance != (Object) null)
    {
      TownManager.Instance.ShowCharacterWindow(heroIndex: this.charIndex);
    }
    else
    {
      if (!((Object) MatchManager.Instance != (Object) null))
        return;
      MatchManager.Instance.ShowCharacterWindow(characterIndex: this.charIndex);
    }
  }

  public void SetClickable(bool status)
  {
    this.GetComponent<BoxCollider2D>().enabled = status;
    this.clickable = status;
  }

  public void Enable()
  {
    this.shadowOver.gameObject.SetActive(false);
    this.enabled = true;
    this.cardEnabled = true;
  }

  public void Disable()
  {
    this.shadowOver.gameObject.SetActive(true);
    this.enabled = false;
  }

  public bool IsEnabled() => this.enabled;

  public bool IsCardEnabled() => this.cardEnabled;

  private void ShowLifeMod(int value)
  {
    if ((bool) (Object) MatchManager.Instance)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    if (value < 0)
    {
      stringBuilder.Append(value.ToString());
      stringBuilder.Append(" <sprite name=heart>");
      this.lifeModTxt.text = stringBuilder.ToString();
      this.lifeModKo.gameObject.SetActive(true);
      this.lifeModOk.gameObject.SetActive(false);
      this.lifeMod.gameObject.SetActive(true);
    }
    else
    {
      if (value <= 0)
        return;
      stringBuilder.Append("+");
      stringBuilder.Append(value.ToString());
      stringBuilder.Append(" <sprite name=heart>");
      this.lifeModTxt.text = stringBuilder.ToString();
      this.lifeModKo.gameObject.SetActive(false);
      this.lifeModOk.gameObject.SetActive(true);
      this.lifeMod.gameObject.SetActive(true);
    }
  }

  public void ShowLevelUp()
  {
    if (this.hero == null || !this.gameObject.activeInHierarchy)
      return;
    this.StartCoroutine(this.ShowLevelUpCo());
  }

  private IEnumerator ShowLevelUpCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    if ((Object) CardCraftManager.Instance != (Object) null && CardCraftManager.Instance.gameObject.activeSelf && CardCraftManager.Instance.craftType != 3 || (Object) CardPlayerManager.Instance != (Object) null || (bool) (Object) MatchManager.Instance)
    {
      this.expMod.gameObject.SetActive(false);
    }
    else
    {
      string playerNick = NetworkManager.Instance.GetPlayerNick();
      if ((this.hero.Owner == null || this.hero.Owner == "" || this.hero.Owner == playerNick) && this.hero.IsReadyForLevelUp())
        this.expMod.gameObject.SetActive(true);
      else
        this.expMod.gameObject.SetActive(false);
    }
  }

  public void ShowUpgrade()
  {
    if (this.hero == null)
      return;
    string playerNick = NetworkManager.Instance.GetPlayerNick();
    if (this.hero.Owner != null && !(this.hero.Owner == "") && !(this.hero.Owner == playerNick))
      return;
    this.upgradeMod.gameObject.SetActive(false);
    this.upgradeMod.gameObject.SetActive(true);
  }

  private void SetDeckNum(int num) => this.deckText.text = num.ToString();

  private void SetInjuryNum(int num)
  {
    if (num == 0)
    {
      this.injuryT.gameObject.SetActive(false);
    }
    else
    {
      this.injuryT.gameObject.SetActive(true);
      this.injuryText.text = num.ToString();
    }
  }

  public void InCharacterScreen(bool state)
  {
    this.inCharacterScreen = state;
    if (this.hero == null || !((Object) this.anim != (Object) null))
      return;
    this.anim.SetBool("show", state);
  }

  public void OnMouseUp()
  {
    if (!Functions.ClickedThisTransform(this.transform) || AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || MadnessManager.Instance.IsActive() || GiveManager.Instance.IsActive() || (bool) (Object) MapManager.Instance && MapManager.Instance.IsCharacterUnlock() || !this.clickable || !this.enabled || this.active)
      return;
    this.Clicked();
  }

  public void Clicked()
  {
    if ((Object) CardCraftManager.Instance != (Object) null && CardCraftManager.Instance.blocked)
      return;
    GameManager.Instance.SetCursorPlain();
    AtOManager.Instance.SideBarCharacterClicked(this.charIndex);
  }

  private void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || MadnessManager.Instance.IsActive() || GiveManager.Instance.IsActive() || (bool) (Object) MapManager.Instance && MapManager.Instance.IsCharacterUnlock() || !this.clickable || !this.enabled || this.active)
      return;
    GameManager.Instance.SetCursorHover();
    this.shadowHover.gameObject.SetActive(true);
    GameManager.Instance.PlayLibraryAudio("ui_click");
    if (this.inCharacterScreen)
      return;
    this.anim.SetBool("show", true);
  }

  private void OnMouseExit()
  {
    if (!this.clickable || !this.enabled)
      return;
    GameManager.Instance.SetCursorPlain();
    this.shadowHover.gameObject.SetActive(false);
    if (this.inCharacterScreen)
      return;
    this.anim.SetBool("show", false);
  }
}
