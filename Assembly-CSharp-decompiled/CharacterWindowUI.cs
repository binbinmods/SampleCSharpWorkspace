// Decompiled with JetBrains decompiler
// Type: CharacterWindowUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterWindowUI : MonoBehaviour
{
  public SpriteRenderer portraitSR;
  public Transform exitButton;
  public Transform nonCombatButtons;
  public Transform combatButtons;
  public Transform globalButtons;
  public Transform npcButtons;
  public Transform mainCharacterButtons;
  public DeckEnergy deckEnergy;
  public DeckWindowUI deckWindow;
  public LevelWindowUI levelWindow;
  public ItemsWindowUI itemsWindow;
  public StatsWindowUI statsWindow;
  public PerksWindowUI perksWindow;
  public Image imageBg;
  public Transform elements;
  public Transform buttons;
  public SpriteRenderer borderDecoBg;
  public SpriteRenderer characterLevelSprite;
  public TMP_Text characterLevelText;
  public TMP_Text perkText;
  public TraitLevel[] traitLevel;
  public TMP_Text[] traitLevelText;
  public Transform[] itemCardsBack;
  public CardItem[] itemCardsCI;
  public BotonGeneric botDeck;
  public BotonGeneric botLevel;
  public BotonGeneric botItems;
  public BotonGeneric botStats;
  public BotonGeneric botPerks;
  public Transform botPerksSeparator;
  public BotonGeneric botCombatDeck;
  public BotonGeneric botCombatDiscard;
  public BotonGeneric botCombatVanish;
  public BotonGeneric botNPCCasted;
  public BotonGeneric botNPCStats;
  private SubClassData currentSCD;
  private Hero currentHero;
  private NPC currentNPC;
  public int heroIndex;
  private int npcIndex = -1;
  private string activeTab = "deck";
  private GameObject heroAnimated;
  private Coroutine coroutineMask;
  private bool windowActive;
  public List<BotonGeneric> allButtons;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();

  private void Awake() => this.HideAllWindows();

  private void Start() => this.Resize();

  public void Resize()
  {
    this.GetComponent<RectTransform>().sizeDelta = new Vector2(Globals.Instance.sizeW, Globals.Instance.sizeH);
    this.exitButton.transform.localPosition = new Vector3((float) (-(double) Globals.Instance.sizeW * 0.5 + 1.0 * (double) Globals.Instance.multiplierX), (float) (-(double) Globals.Instance.sizeH * 0.5 + 3.9000000953674316 * (double) Globals.Instance.multiplierY), this.exitButton.transform.localPosition.z);
  }

  public bool IsActive() => this.elements.gameObject.activeSelf;

  public void ShowUpgradedCards(List<string> upgradedCards)
  {
    if (upgradedCards.Count <= 0)
      return;
    this.HideButtons();
    this.Show("unlockedCards", -2);
    this.deckWindow.ShowUpgradedCards(upgradedCards);
  }

  public void ShowUnlockedCards(List<string> unlockedCards)
  {
    if (unlockedCards.Count <= 0)
      return;
    this.HideButtons();
    this.Show(nameof (unlockedCards), -2);
    this.deckWindow.ShowUnlockedCards(unlockedCards);
  }

  public void Show(string _element = "", int _heroIndex = -1, bool isHero = true)
  {
    if (!this.IsActive() && _element != "unlockedCards")
      GameManager.Instance.PlayLibraryAudio("action_bag");
    this.gameObject.SetActive(true);
    PopupManager.Instance.ClosePopup();
    this.portraitSR.gameObject.SetActive(false);
    this.npcButtons.gameObject.SetActive(false);
    this.globalButtons.gameObject.SetActive(true);
    if ((bool) (Object) MatchManager.Instance)
      this.globalButtons.transform.localPosition = new Vector3(this.globalButtons.transform.localPosition.x, -0.7f, this.globalButtons.transform.localPosition.z);
    else
      this.globalButtons.transform.localPosition = new Vector3(this.globalButtons.transform.localPosition.x, 0.0f, this.globalButtons.transform.localPosition.z);
    if (GameManager.Instance.IsObeliskChallenge())
    {
      this.botPerks.gameObject.SetActive(false);
      this.botPerksSeparator.gameObject.SetActive(false);
    }
    else
    {
      this.botPerks.gameObject.SetActive(true);
      this.botPerksSeparator.gameObject.SetActive(true);
    }
    if (isHero)
      this.npcIndex = -1;
    if (_element != "unlockedCards")
    {
      if (!(bool) (Object) MatchManager.Instance)
      {
        this.combatButtons.gameObject.SetActive(false);
        this.nonCombatButtons.gameObject.SetActive(true);
      }
      else
      {
        this.combatButtons.gameObject.SetActive(true);
        this.nonCombatButtons.gameObject.SetActive(false);
      }
      this.ShowMask(true);
      if (isHero)
      {
        this.buttons.gameObject.SetActive(true);
        if (_heroIndex == -1)
          _heroIndex = this.heroIndex;
        if (_heroIndex > -1)
        {
          this.heroIndex = _heroIndex;
          this.currentHero = AtOManager.Instance.GetHero(this.heroIndex);
          if ((Object) this.currentHero.HeroData == (Object) null)
            return;
          this.currentSCD = this.currentHero.HeroData.HeroSubClass;
          if (this.currentSCD.MainCharacter)
            this.mainCharacterButtons.gameObject.SetActive(true);
          else
            this.mainCharacterButtons.gameObject.SetActive(false);
        }
      }
      else if ((Object) MatchManager.Instance != (Object) null)
      {
        this.npcIndex = _heroIndex != -1 ? (this.heroIndex = _heroIndex) : this.heroIndex;
        this.currentNPC = MatchManager.Instance.GetNPCCharacter(this.npcIndex);
        if (this.currentNPC == null)
          return;
      }
      this.deckEnergy.gameObject.SetActive(true);
    }
    else
    {
      this.ShowMask(true, true);
      this.deckEnergy.gameObject.SetActive(false);
    }
    if (_element == "")
      _element = this.windowActive || !this.currentHero.IsReadyForLevelUp() ? this.activeTab : "level";
    if (_element != "perks")
    {
      this.activeTab = _element;
      this.HideAllWindows(_element);
    }
    switch (_element)
    {
      case "deck":
        this.deckWindow.Show(_heroIndex);
        this.botDeck.Disable();
        this.botDeck.SetTextColor(new Color(1f, 0.6f, 0.07f));
        this.botDeck.transform.localPosition = new Vector3(7.4f, this.botDeck.transform.localPosition.y, this.botDeck.transform.localPosition.z);
        if (!((Object) RewardsManager.Instance != (Object) null))
        {
          int num1 = (Object) LootManager.Instance != (Object) null ? 1 : 0;
        }
        if ((Object) CardCraftManager.Instance != (Object) null)
          CardCraftManager.Instance.gameObject.SetActive(false);
        this.deckEnergy.WriteEnergy(_heroIndex, 0);
        break;
      case "combatdeck":
        this.deckWindow.Show(_heroIndex);
        this.botCombatDeck.Disable();
        this.botCombatDeck.SetTextColor(new Color(1f, 0.6f, 0.07f));
        this.botCombatDeck.transform.localPosition = new Vector3(7.4f, this.botCombatDeck.transform.localPosition.y, this.botCombatDeck.transform.localPosition.z);
        this.deckEnergy.WriteEnergy(_heroIndex, 1);
        break;
      case "combatdiscard":
        if (isHero)
        {
          this.deckWindow.Show(_heroIndex, discard: true);
          this.botCombatDiscard.Disable();
          this.botCombatDiscard.SetTextColor(new Color(1f, 0.6f, 0.07f));
          this.botCombatDiscard.transform.localPosition = new Vector3(7.4f, this.botCombatDiscard.transform.localPosition.y, this.botCombatDiscard.transform.localPosition.z);
          this.deckEnergy.WriteEnergy(_heroIndex, 2);
          break;
        }
        this.deckEnergy.gameObject.SetActive(false);
        this.portraitSR.gameObject.SetActive(true);
        this.portraitSR.sprite = this.currentNPC.SpritePortrait;
        List<string> listCards = new List<string>();
        List<string> npcCardsCastedList = MatchManager.Instance.GetNPCCardsCastedList(this.currentNPC.InternalId);
        for (int index = npcCardsCastedList.Count - 1; index >= 0; --index)
          listCards.Add(npcCardsCastedList[index]);
        this.deckWindow.Show(this.npcIndex, listCards, sort: false);
        this.deckWindow.HideInjury();
        this.deckWindow.SetTitle(Texts.Instance.GetText("heroCastedCards").Replace("<hero>", this.currentNPC.SourceName), listCards.Count);
        this.buttons.gameObject.SetActive(true);
        this.nonCombatButtons.gameObject.SetActive(false);
        this.combatButtons.gameObject.SetActive(false);
        this.globalButtons.gameObject.SetActive(false);
        this.npcButtons.gameObject.SetActive(true);
        this.botNPCCasted.Disable();
        this.botNPCCasted.SetTextColor(new Color(1f, 0.6f, 0.07f));
        this.botNPCCasted.transform.localPosition = new Vector3(7.4f, this.botNPCCasted.transform.localPosition.y, this.botNPCCasted.transform.localPosition.z);
        break;
      case "combatvanish":
        this.deckEnergy.WriteEnergy(_heroIndex, 3);
        List<string> heroVanish = MatchManager.Instance.GetHeroVanish(_heroIndex);
        this.deckWindow.Show(0, heroVanish, sort: false);
        this.deckWindow.HideInjury();
        this.deckWindow.SetTitle(Texts.Instance.GetText("heroVanishedCards").Replace("<hero>", this.currentHero.SourceName), heroVanish.Count);
        this.botCombatVanish.Disable();
        this.botCombatVanish.SetTextColor(new Color(1f, 0.6f, 0.07f));
        this.botCombatVanish.transform.localPosition = new Vector3(7.4f, this.botCombatVanish.transform.localPosition.y, this.botCombatVanish.transform.localPosition.z);
        break;
      case "level":
        this.levelWindow.Show(_heroIndex);
        this.botLevel.Disable();
        this.botLevel.SetTextColor(new Color(1f, 0.6f, 0.07f));
        this.botLevel.transform.localPosition = new Vector3(7.4f, this.botLevel.transform.localPosition.y, this.botLevel.transform.localPosition.z);
        this.DoLevelWindow();
        break;
      case "perks":
        PerkTree.Instance.Show(this.currentHero.HeroData.HeroSubClass.Id, this.heroIndex);
        break;
      case "items":
        this.botItems.Disable();
        this.botItems.SetTextColor(new Color(1f, 0.6f, 0.07f));
        this.botItems.transform.localPosition = new Vector3(7.4f, this.botItems.transform.localPosition.y, this.botItems.transform.localPosition.z);
        this.DoItemsWindow();
        break;
      case "deckreward":
        this.deckWindow.Show(_heroIndex);
        this.botDeck.Disable();
        this.botDeck.SetTextColor(new Color(1f, 0.6f, 0.07f));
        this.botDeck.transform.localPosition = new Vector3(7.4f, this.botDeck.transform.localPosition.y, this.botDeck.transform.localPosition.z);
        break;
      case "stats":
        if (isHero)
        {
          this.statsWindow.DoStats((Character) this.currentHero);
          this.botStats.Disable();
          this.botStats.SetTextColor(new Color(1f, 0.6f, 0.07f));
          this.botStats.transform.localPosition = new Vector3(7.4f, this.botStats.transform.localPosition.y, this.botStats.transform.localPosition.z);
          break;
        }
        this.portraitSR.gameObject.SetActive(true);
        this.portraitSR.sprite = this.currentNPC.SpritePortrait;
        this.buttons.gameObject.SetActive(true);
        this.nonCombatButtons.gameObject.SetActive(false);
        this.combatButtons.gameObject.SetActive(false);
        this.globalButtons.gameObject.SetActive(false);
        this.npcButtons.gameObject.SetActive(true);
        this.statsWindow.DoStats((Character) this.currentNPC);
        this.botNPCStats.Disable();
        this.botNPCStats.SetTextColor(new Color(1f, 0.6f, 0.07f));
        this.botNPCStats.transform.localPosition = new Vector3(7.4f, this.botNPCStats.transform.localPosition.y, this.botNPCStats.transform.localPosition.z);
        break;
      default:
        int num2 = _element == "unlockedCards" ? 1 : 0;
        break;
    }
    this.windowActive = true;
    if (!isHero || !(_element != "unlockedCards"))
      return;
    if ((Object) MapManager.Instance != (Object) null)
      MapManager.Instance.sideCharacters.InCharacterScreen(true);
    else if ((Object) TownManager.Instance != (Object) null)
    {
      TownManager.Instance.sideCharacters.InCharacterScreen(true);
    }
    else
    {
      if (!((Object) MatchManager.Instance != (Object) null))
        return;
      MatchManager.Instance.sideCharacters.InCharacterScreen(true);
    }
  }

  public void ShowMask(bool state, bool instant = false)
  {
    if (this.coroutineMask != null)
      this.StopCoroutine(this.coroutineMask);
    this.coroutineMask = this.StartCoroutine(this.ShowMaskCo(state, true));
  }

  private IEnumerator ShowMaskCo(bool state, bool instant)
  {
    float maxAlplha = 0.935f;
    float step = 0.07f;
    if (instant)
    {
      this.imageBg.color = new Color(0.0f, 0.0f, 0.0f, maxAlplha);
    }
    else
    {
      float index = this.imageBg.color.a;
      if (!state)
      {
        while ((double) index > 0.0)
        {
          this.imageBg.color = new Color(0.0f, 0.0f, 0.0f, index);
          index -= step;
          yield return (object) null;
        }
        this.imageBg.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
      }
      else
      {
        while ((double) index < (double) maxAlplha)
        {
          this.imageBg.color = new Color(0.0f, 0.0f, 0.0f, index);
          index += step;
          yield return (object) null;
        }
        this.imageBg.color = new Color(0.0f, 0.0f, 0.0f, maxAlplha);
      }
    }
  }

  public void ReDrawLevel()
  {
    if (this.currentHero == null)
      return;
    this.DrawLevelButtons(this.currentHero.Level, this.currentHero.IsReadyForLevelUp());
  }

  private void DoPerksWindow()
  {
    if (this.currentHero == null)
      this.Hide();
    else
      this.perksWindow.DoPerks(this.currentHero);
  }

  private void DoLevelWindow()
  {
    if (this.currentHero == null)
    {
      this.Hide();
    }
    else
    {
      int level = this.currentHero.Level;
      bool levelUp = this.currentHero.IsReadyForLevelUp();
      string str = Globals.Instance.ClassColor[this.currentHero.ClassName];
      this.characterLevelSprite.sprite = this.currentHero.HeroData.HeroSubClass.SpriteBorder;
      this.DrawLevelButtons(level, levelUp);
    }
  }

  private void DrawLevelButtons(int heroLevel, bool levelUp)
  {
    string _color = Globals.Instance.ClassColor[this.currentHero.ClassName];
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.Append("<size=+2>");
    stringBuilder1.Append(Texts.Instance.GetText("levelNumber").Replace("<N>", heroLevel.ToString()));
    stringBuilder1.Append("</size>");
    stringBuilder1.Append("\n");
    stringBuilder1.Append("<sprite name=experience> <color=#FC0>");
    stringBuilder1.Append(this.currentHero.Experience);
    stringBuilder1.Append("/");
    stringBuilder1.Append(Globals.Instance.GetExperienceByLevel(heroLevel));
    stringBuilder1.Append("</color>");
    this.characterLevelText.text = stringBuilder1.ToString();
    int characterTier = PlayerManager.Instance.GetCharacterTier("", "trait", this.currentHero.PerkRank);
    TraitData traitData1 = this.GetTraitData(0, 2);
    this.traitLevel[0].SetColor(_color);
    this.traitLevel[0].SetPosition(0);
    this.traitLevel[0].SetEnable(true);
    this.traitLevel[0].SetTrait(traitData1, characterTier);
    for (int level = 1; level < 5; ++level)
    {
      bool _state1 = false;
      bool _state2 = false;
      int index1 = level * 2;
      int index2 = index1 + 1;
      TraitData traitData2 = this.GetTraitData(level);
      if (level < heroLevel)
      {
        if (this.currentHero.HaveTrait(traitData2.Id))
          _state2 = true;
      }
      else if (level == heroLevel & levelUp && (this.currentHero.Owner == null || this.currentHero.Owner == "" || this.currentHero.Owner == NetworkManager.Instance.GetPlayerNick()))
        _state1 = true;
      this.traitLevel[index1].SetHeroIndex(this.heroIndex);
      this.traitLevel[index1].SetColor(_color);
      this.traitLevel[index1].SetPosition(1);
      this.traitLevel[index1].SetEnable(_state2);
      this.traitLevel[index1].SetActive(_state1);
      this.traitLevel[index1].SetTrait(traitData2, characterTier);
      TraitData traitData3 = this.GetTraitData(level, 1);
      bool _state3 = false;
      bool _state4 = false;
      if (level < heroLevel)
      {
        if (this.currentHero.HaveTrait(traitData3.Id))
          _state3 = true;
      }
      else if (level == heroLevel & levelUp && (this.currentHero.Owner == null || this.currentHero.Owner == "" || this.currentHero.Owner == NetworkManager.Instance.GetPlayerNick()))
        _state4 = true;
      this.traitLevel[index2].SetHeroIndex(this.heroIndex);
      this.traitLevel[index2].SetColor(_color);
      this.traitLevel[index2].SetPosition(2);
      this.traitLevel[index2].SetEnable(_state3);
      this.traitLevel[index2].SetActive(_state4);
      this.traitLevel[index2].SetTrait(traitData3, characterTier);
      StringBuilder stringBuilder2 = new StringBuilder();
      bool flag = false;
      if ((level < heroLevel || level == heroLevel & levelUp) && (this.currentHero.Owner == null || this.currentHero.Owner == "" || this.currentHero.Owner == NetworkManager.Instance.GetPlayerNick()))
        flag = true;
      stringBuilder2.Append("<size=+.4>");
      if (flag)
        stringBuilder2.Append("<color=#FC0>");
      stringBuilder2.Append(Texts.Instance.GetText("levelNumber").Replace("<N>", (level + 1).ToString()));
      if (flag)
        stringBuilder2.Append("</color>");
      stringBuilder2.Append("</size>");
      stringBuilder2.Append("\n");
      if (flag)
        stringBuilder2.Append("<color=#EE5A3C>");
      stringBuilder2.Append(Texts.Instance.GetText("incrementMaxHp").Replace("<N>", this.currentSCD.MaxHp[level].ToString()));
      if (flag)
        stringBuilder2.Append("</color>");
      this.traitLevelText[level].text = stringBuilder2.ToString();
    }
  }

  private TraitData GetTraitData(int level, int index = 0)
  {
    if (index == 2 && level > 0)
      return Globals.Instance.GetTraitData(this.currentHero.Traits[level]);
    switch (level)
    {
      case 0:
        return this.currentSCD.Trait0;
      case 1:
        return index == 0 ? this.currentSCD.Trait1A : this.currentSCD.Trait1B;
      case 2:
        return index == 0 ? this.currentSCD.Trait2A : this.currentSCD.Trait2B;
      case 3:
        return index == 0 ? this.currentSCD.Trait3A : this.currentSCD.Trait3B;
      case 4:
        return index == 0 ? this.currentSCD.Trait4A : this.currentSCD.Trait4B;
      default:
        return (TraitData) null;
    }
  }

  private void SetCardbacks()
  {
    string cardbackUsed = this.currentHero.CardbackUsed;
    if (!(cardbackUsed != ""))
      return;
    CardbackData cardbackData = Globals.Instance.GetCardbackData(cardbackUsed);
    if ((Object) cardbackData == (Object) null)
      cardbackData = Globals.Instance.GetCardbackData(Globals.Instance.GetCardbackBaseIdBySubclass(this.currentHero.HeroData.HeroSubClass.Id));
    if ((Object) cardbackData == (Object) null)
      return;
    Sprite cardbackSprite = cardbackData.CardbackSprite;
    if (!((Object) cardbackSprite != (Object) null))
      return;
    for (int index = 0; index < this.itemCardsBack.Length; ++index)
    {
      if ((Object) this.itemCardsBack[index] != (Object) null)
        this.itemCardsBack[index].GetComponent<SpriteRenderer>().sprite = cardbackSprite;
    }
  }

  private void DoItemsWindow()
  {
    this.SetCardbacks();
    for (int index = 0; index < 5; ++index)
    {
      string id = "";
      if (index == 0)
        id = this.currentHero.Weapon;
      else if (index == 1)
        id = this.currentHero.Armor;
      else if (index == 2)
        id = this.currentHero.Jewelry;
      else if (index == 3)
        id = this.currentHero.Accesory;
      else if (index == 4)
        id = this.currentHero.Pet;
      if (id != "")
      {
        CardItem cardItem = this.itemCardsCI[index];
        cardItem.SetCard(id, _theHero: this.currentHero, GetFromGlobal: true);
        cardItem.TopLayeringOrder("UI", 20000);
        cardItem.transform.localScale = Vector3.zero;
        cardItem.SetDestinationLocalScale(1.25f);
        cardItem.cardmakebig = true;
        cardItem.cardmakebigSize = 1.25f;
        cardItem.cardmakebigSizeMax = 1.4f;
        cardItem.active = true;
        cardItem.lockPosition = true;
        this.itemCardsBack[index].gameObject.SetActive(false);
        this.itemCardsCI[index].transform.gameObject.SetActive(true);
      }
      else
      {
        this.itemCardsBack[index].gameObject.SetActive(true);
        this.itemCardsCI[index].transform.gameObject.SetActive(false);
      }
    }
  }

  public void Hide(bool goToDivination = false)
  {
    if (!this.IsActive())
      return;
    Functions.DebugLogGD("CHARACTERWINDOW Hide", "trace");
    this.deckWindow.DestroyDeck();
    this.HideAllWindows();
    this.heroIndex = 0;
    this.activeTab = "deck";
    this.windowActive = false;
    if (goToDivination)
      return;
    if ((Object) CardCraftManager.Instance != (Object) null)
    {
      CardCraftManager.Instance.gameObject.SetActive(true);
      if ((Object) TownManager.Instance != (Object) null)
      {
        TownManager.Instance.sideCharacters.ResetCharacters();
      }
      else
      {
        if (!((Object) MapManager.Instance != (Object) null))
          return;
        MapManager.Instance.sideCharacters.ResetCharacters();
      }
    }
    else if ((Object) MapManager.Instance != (Object) null)
    {
      MapManager.Instance.sideCharacters.ResetCharacters();
      if (!(bool) (Object) EventManager.Instance)
        MapManager.Instance.sideCharacters.InCharacterScreen(false);
      MapManager.Instance.HideCharacterWindow();
    }
    else if ((Object) TownManager.Instance != (Object) null)
    {
      TownManager.Instance.sideCharacters.ResetCharacters();
      TownManager.Instance.HideCharacterWindow();
    }
    else
    {
      if (!((Object) MatchManager.Instance != (Object) null))
        return;
      MatchManager.Instance.sideCharacters.ResetCharacters();
      MatchManager.Instance.sideCharacters.InCharacterScreen(false);
      MatchManager.Instance.HideCharacterWindow();
      MatchManager.Instance.ResetController();
    }
  }

  private void HideButtons() => this.buttons.gameObject.SetActive(false);

  public void HideAllWindows(string _element = "")
  {
    if ((bool) (Object) GameManager.Instance)
      GameManager.Instance.CleanTempContainer();
    if ((bool) (Object) PopupManager.Instance)
      PopupManager.Instance.ClosePopup();
    if (this.buttons.gameObject.activeSelf)
    {
      this.botDeck.Enable();
      this.botDeck.SetTextColor(Color.white);
      this.botDeck.transform.localPosition = new Vector3(7.6f, this.botDeck.transform.localPosition.y, this.botDeck.transform.localPosition.z);
      this.botLevel.Enable();
      this.botLevel.SetTextColor(Color.white);
      this.botLevel.transform.localPosition = new Vector3(7.6f, this.botLevel.transform.localPosition.y, this.botLevel.transform.localPosition.z);
      this.botItems.Enable();
      this.botItems.SetTextColor(Color.white);
      this.botItems.transform.localPosition = new Vector3(7.6f, this.botItems.transform.localPosition.y, this.botItems.transform.localPosition.z);
      this.botStats.Enable();
      this.botStats.SetTextColor(Color.white);
      this.botStats.transform.localPosition = new Vector3(7.6f, this.botStats.transform.localPosition.y, this.botStats.transform.localPosition.z);
      this.botPerks.Enable();
      this.botPerks.SetTextColor(Color.white);
      this.botPerks.transform.localPosition = new Vector3(7.6f, this.botPerks.transform.localPosition.y, this.botPerks.transform.localPosition.z);
      this.botCombatDeck.Enable();
      this.botCombatDeck.SetTextColor(Color.white);
      this.botCombatDeck.transform.localPosition = new Vector3(7.6f, this.botCombatDeck.transform.localPosition.y, this.botCombatDeck.transform.localPosition.z);
      this.botCombatDiscard.Enable();
      this.botCombatDiscard.SetTextColor(Color.white);
      this.botCombatDiscard.transform.localPosition = new Vector3(7.6f, this.botCombatDiscard.transform.localPosition.y, this.botCombatDiscard.transform.localPosition.z);
      this.botCombatVanish.Enable();
      this.botCombatVanish.SetTextColor(Color.white);
      this.botCombatVanish.transform.localPosition = new Vector3(7.6f, this.botCombatVanish.transform.localPosition.y, this.botCombatVanish.transform.localPosition.z);
      this.botNPCStats.Enable();
      this.botNPCStats.SetTextColor(Color.white);
      this.botNPCStats.transform.localPosition = new Vector3(7.6f, this.botNPCStats.transform.localPosition.y, this.botNPCStats.transform.localPosition.z);
      this.botNPCCasted.Enable();
      this.botNPCCasted.SetTextColor(Color.white);
      this.botNPCCasted.transform.localPosition = new Vector3(7.6f, this.botNPCCasted.transform.localPosition.y, this.botNPCCasted.transform.localPosition.z);
    }
    if (_element != "deck" && _element != "combatdeck" && _element != "combatdiscard" && _element != "combatvanish")
      this.deckWindow.gameObject.SetActive(false);
    else
      this.deckWindow.gameObject.SetActive(true);
    if (_element != "level")
      this.levelWindow.gameObject.SetActive(false);
    else
      this.levelWindow.gameObject.SetActive(true);
    if (_element != "items")
      this.itemsWindow.gameObject.SetActive(false);
    else
      this.itemsWindow.gameObject.SetActive(true);
    if (_element != "stats")
      this.statsWindow.gameObject.SetActive(false);
    else
      this.statsWindow.gameObject.SetActive(true);
    if (_element != "perks")
      this.perksWindow.gameObject.SetActive(false);
    else
      this.perksWindow.gameObject.SetActive(true);
    if (_element == "unlockedCards")
      this.deckWindow.gameObject.SetActive(true);
    if (_element == "")
      this.elements.gameObject.SetActive(false);
    else
      this.elements.gameObject.SetActive(true);
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    bool shoulderLeft = false,
    bool shoulderRight = false)
  {
    this._controllerList.Clear();
    for (int index = 0; index < this.allButtons.Count; ++index)
    {
      if (!((Object) this.allButtons[index] == (Object) null) && this.allButtons[index].IsEnabled() && Functions.TransformIsVisible(this.allButtons[index].transform))
        this._controllerList.Add(this.allButtons[index].transform);
    }
    for (int index = 0; index < 4; ++index)
    {
      if ((bool) (Object) TownManager.Instance)
      {
        if (Functions.TransformIsVisible(TownManager.Instance.sideCharacters.charArray[index].transform))
          this._controllerList.Add(TownManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
      }
      else if ((bool) (Object) MapManager.Instance)
      {
        if (Functions.TransformIsVisible(MapManager.Instance.sideCharacters.charArray[index].transform))
          this._controllerList.Add(MapManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
      }
      else if ((bool) (Object) MatchManager.Instance && Functions.TransformIsVisible(MatchManager.Instance.sideCharacters.charArray[index].transform))
        this._controllerList.Add(MatchManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
    }
    if (this.deckWindow.gameObject.activeSelf)
    {
      for (int index = 0; index < this.deckWindow.injuryContent.transform.childCount; ++index)
        this._controllerList.Add(this.deckWindow.injuryContent.transform.GetChild(index));
      for (int index = 0; index < this.deckWindow.deckContent.transform.childCount; ++index)
        this._controllerList.Add(this.deckWindow.deckContent.transform.GetChild(index));
    }
    else if (this.levelWindow.gameObject.activeSelf)
    {
      this._controllerList.Add(this.traitLevel[0].transform);
      this._controllerList.Add(this.traitLevel[2].transform);
      this._controllerList.Add(this.traitLevel[3].transform);
      this._controllerList.Add(this.traitLevel[4].transform);
      this._controllerList.Add(this.traitLevel[5].transform);
      this._controllerList.Add(this.traitLevel[6].transform);
      this._controllerList.Add(this.traitLevel[7].transform);
      this._controllerList.Add(this.traitLevel[8].transform);
      this._controllerList.Add(this.traitLevel[9].transform);
    }
    else if (this.itemsWindow.gameObject.activeSelf)
    {
      for (int index = 0; index < this.itemCardsCI.Length; ++index)
      {
        if (this.itemCardsCI[index].transform.gameObject.activeSelf)
          this._controllerList.Add(this.itemCardsCI[index].transform);
      }
    }
    this._controllerList.Add(this.exitButton);
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((Object) this._controllerList[this.controllerHorizontalIndex] != (Object) null))
      return;
    if ((bool) (Object) this._controllerList[this.controllerHorizontalIndex].GetComponent<CardItem>())
    {
      Canvas.ForceUpdateCanvases();
      Vector3 zero = Vector3.zero with
      {
        y = -1.425f - this._controllerList[this.controllerHorizontalIndex].localPosition.y
      };
      this.deckWindow.scrollContent.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = (Vector2) Vector3.zero;
      this.deckWindow.scrollContent.GetComponent<RectTransform>().anchoredPosition = (Vector2) zero;
    }
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }

  private void ControllerNextCharacter()
  {
    if (this.npcIndex != -1)
      return;
    int index = this.heroIndex;
    if ((bool) (Object) CardCraftManager.Instance)
      index = CardCraftManager.Instance.heroIndex;
    bool flag = false;
    while (!flag)
    {
      ++index;
      if (index > 3)
        index = 0;
      if (AtOManager.Instance.GetHero(index) != null && (Object) AtOManager.Instance.GetHero(index).HeroData != (Object) null)
        flag = true;
    }
    GameObject gameObject = GameObject.Find("/SideCharacters/OverCharacter" + index.ToString());
    if ((Object) gameObject != (Object) null)
      gameObject.transform.GetComponent<OverCharacter>().Clicked();
    if (!PerkTree.Instance.IsActive())
      return;
    this.ControllerNextOption();
  }

  private void ControllerNextOption()
  {
    Debug.Log((object) (nameof (ControllerNextOption) + PerkTree.Instance.IsActive().ToString()));
    bool flag = false;
    if (PerkTree.Instance.IsActive())
      flag = true;
    int num = -1;
    if (flag)
    {
      PerkTree.Instance.HideAction(false);
      for (int index = 0; index < this.allButtons.Count; ++index)
      {
        if (!((Object) this.allButtons[index] == (Object) null) && this.allButtons[index].IsEnabled() && Functions.TransformIsVisible(this.allButtons[index].transform))
        {
          this.allButtons[index].Clicked();
          break;
        }
      }
    }
    else
    {
      for (int index = 0; index < this.allButtons.Count; ++index)
      {
        if (!((Object) this.allButtons[index] == (Object) null) && !this.allButtons[index].IsEnabled() && Functions.TransformIsVisible(this.allButtons[index].transform))
        {
          num = index;
          break;
        }
      }
      if (num <= -1)
        return;
      for (int index = num; index < this.allButtons.Count; ++index)
      {
        if (!((Object) this.allButtons[index] == (Object) null) && this.allButtons[index].IsEnabled() && Functions.TransformIsVisible(this.allButtons[index].transform))
        {
          this.allButtons[index].Clicked();
          return;
        }
      }
      for (int index = 0; index < num; ++index)
      {
        if (!((Object) this.allButtons[index] == (Object) null) && this.allButtons[index].IsEnabled() && Functions.TransformIsVisible(this.allButtons[index].transform))
        {
          this.allButtons[index].Clicked();
          break;
        }
      }
    }
  }

  public void ControllerMoveShoulder(bool _isRight = false)
  {
    Debug.Log((object) ("ControllerMoveShoulder " + _isRight.ToString()));
    if (!_isRight)
      this.ControllerNextCharacter();
    else
      this.ControllerNextOption();
    this.ControllerMovement(goingRight: true);
  }
}
