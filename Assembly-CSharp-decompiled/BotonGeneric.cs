// Decompiled with JetBrains decompiler
// Type: BotonGeneric
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonGeneric : MonoBehaviour
{
  public Transform border;
  public Transform backgroundPlain;
  public Transform backgroundOver;
  public Transform backgroundDisable;
  public TMP_Text text;
  private TextMesh textTM;
  public string idTranslate = "";
  public string idPopup = "";
  private string popupText = "";
  public string auxString = "";
  public int auxInt = -1000;
  public Color color;
  private SpriteRenderer borderSR;
  private SpriteRenderer backgroundOverSR;
  private SpriteRenderer backgroundPlainSR;
  private Animator anim;
  private Color totalAlpha;
  private Color textColor;
  private Color textColorOri;
  private bool show;
  private bool permaBorder;
  public bool buttonEnabled = true;
  private float scaleOriX;
  private float scaleOriY;
  private Coroutine borderCo;
  private Color incrementColor;

  private void Awake()
  {
    this.borderSR = this.border.GetComponent<SpriteRenderer>();
    this.backgroundOverSR = this.backgroundOver.GetComponent<SpriteRenderer>();
    if ((Object) this.backgroundPlain != (Object) null)
      this.backgroundPlainSR = this.backgroundPlain.GetComponent<SpriteRenderer>();
    this.anim = this.GetComponent<Animator>();
  }

  private void Start()
  {
    this.textColor = this.textColorOri = this.text.color;
    this.incrementColor = new Color(0.0f, 0.0f, 0.0f, 0.025f);
    this.totalAlpha = new Color(this.color.r, this.color.g, this.color.b, 0.0f);
    this.SetColor();
    this.StartCoroutine(this.SetTextCoroutine());
    if ((double) this.scaleOriX == 0.0)
      this.scaleOriX = this.transform.localScale.x;
    if ((double) this.scaleOriY == 0.0)
      this.scaleOriY = this.transform.localScale.y;
    if (this.text.transform.childCount > 0)
    {
      for (int index = 0; index < this.text.transform.childCount; ++index)
      {
        MeshRenderer component = this.text.transform.GetChild(index).GetComponent<MeshRenderer>();
        component.sortingLayerName = this.text.GetComponent<MeshRenderer>().sortingLayerName;
        component.sortingOrder = this.text.GetComponent<MeshRenderer>().sortingOrder;
      }
    }
    this.DoShow(false);
  }

  private IEnumerator SetTextCoroutine()
  {
    int limit = 0;
    if (this.idTranslate != "")
    {
      while ((Object) Texts.Instance == (Object) null || Texts.Instance.GetText(this.idTranslate) == "")
      {
        yield return (object) Globals.Instance.WaitForSeconds(0.02f);
        ++limit;
        if (limit > 100)
          break;
      }
      if (this.idTranslate != "" && (Object) Texts.Instance != (Object) null)
        this.SetText(Texts.Instance.GetText(this.idTranslate));
    }
    limit = 0;
    if (this.idPopup != "")
    {
      while ((Object) Texts.Instance == (Object) null || Texts.Instance.GetText(this.idPopup) == "")
      {
        yield return (object) Globals.Instance.WaitForSeconds(0.02f);
        ++limit;
        if (limit > 100)
          break;
      }
      if (this.idPopup != "" && (Object) Texts.Instance != (Object) null)
        this.SetPopupText(Texts.Instance.GetText(this.idPopup));
    }
  }

  public void ShowBackgroundPlain(bool state)
  {
    if (!((Object) this.backgroundPlainSR != (Object) null))
      return;
    this.backgroundPlainSR.gameObject.SetActive(state);
  }

  public void ShowBackground(bool state) => this.backgroundOverSR.gameObject.SetActive(state);

  public void ShowBackgroundDisable(bool state) => this.backgroundDisable.gameObject.SetActive(state);

  public void SetColorAbsolute(Color _color)
  {
    this.backgroundOverSR.color = _color;
    this.borderSR.color = new Color(_color.r, _color.g, _color.b, 0.0f);
  }

  public void SetColor()
  {
    this.backgroundOverSR.color = this.color;
    this.borderSR.color = this.totalAlpha;
  }

  public void SetBackgroundColor(Color _color) => this.backgroundOverSR.color = _color;

  public void SetBorderColor(Color _color) => this.borderSR.color = _color;

  public void ResetText()
  {
    if (!(this.idTranslate != ""))
      return;
    this.SetText(Texts.Instance.GetText(this.idTranslate));
  }

  public void SetText(string _text) => this.text.text = _text;

  public string GetText() => this.text.text;

  public void SetTextColor(Color color) => this.text.color = color;

  public void ResetTextColor() => this.text.color = this.textColorOri;

  public void SetPopupId(string _id)
  {
    this.idPopup = _id;
    this.SetPopupText(Texts.Instance.GetText(_id));
  }

  public void SetPopupText(string text) => this.popupText = text;

  public void ShowDisableMask(bool state) => this.backgroundDisable.gameObject.SetActive(state);

  public void ShowBorder(bool state)
  {
    this.border.gameObject.SetActive(state);
    if (state)
      this.borderSR.color = new Color(this.borderSR.color.r, this.borderSR.color.g, this.borderSR.color.b, 1f);
    else
      this.borderSR.color = new Color(this.borderSR.color.r, this.borderSR.color.g, this.borderSR.color.b, 0.0f);
  }

  public void HideBorderNow() => this.DoShow(false);

  public void Disable()
  {
    this.buttonEnabled = false;
    this.border.gameObject.SetActive(false);
    this.backgroundDisable.gameObject.SetActive(true);
  }

  public void Enable()
  {
    this.buttonEnabled = true;
    this.border.gameObject.SetActive(true);
    this.backgroundDisable.gameObject.SetActive(false);
  }

  public bool IsEnabled() => this.buttonEnabled;

  public void EnabledButton(bool state) => this.buttonEnabled = state;

  public void PermaBorder(bool state)
  {
    this.permaBorder = state;
    this.DoShow(state);
  }

  private void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive() || PerkTree.Instance.IsActive() && this.gameObject.name.Contains("Character_"))
      return;
    if (this.buttonEnabled)
      GameManager.Instance.PlayLibraryAudio("ui_menu");
    if (!(this.popupText != ""))
      return;
    PopupManager.Instance.SetText(this.popupText, true, alwaysCenter: true);
  }

  private void OnMouseOver()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive() || PerkTree.Instance.IsActive() && this.gameObject.name.Contains("Character_") || (bool) (Object) MatchManager.Instance && MatchManager.Instance.CardDrag || !this.buttonEnabled)
      return;
    GameManager.Instance.SetCursorHover();
    if ((double) this.scaleOriX == 0.0)
      this.scaleOriX = this.transform.localScale.x;
    if ((double) this.scaleOriY == 0.0)
      this.scaleOriY = this.transform.localScale.y;
    if ((double) this.scaleOriX != 0.0 && (double) this.scaleOriY != 0.0)
      this.transform.localScale = new Vector3(this.scaleOriX + 0.05f, this.scaleOriY + 0.05f, 1f);
    if (this.permaBorder)
      return;
    this.DoShow(true);
  }

  private void OnMouseExit()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive() || PerkTree.Instance.IsActive() && this.gameObject.name.Contains("Character_") || (bool) (Object) MatchManager.Instance && MatchManager.Instance.CardDrag)
      return;
    GameManager.Instance.SetCursorPlain();
    if (this.popupText != "")
      PopupManager.Instance.ClosePopup();
    if (!this.buttonEnabled)
      return;
    if ((double) this.scaleOriX != 0.0 && (double) this.scaleOriY != 0.0)
      this.transform.localScale = new Vector3(this.scaleOriX, this.scaleOriY, 1f);
    if (this.permaBorder)
      return;
    this.DoShow(false);
  }

  public void OnMouseUp()
  {
    if (!Functions.ClickedThisTransform(this.transform) || AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive() || PerkTree.Instance.IsActive() && this.gameObject.name.Contains("Character_"))
      return;
    if (this.idPopup != "")
      PopupManager.Instance.ClosePopup();
    if (!this.buttonEnabled)
      return;
    this.Clicked();
  }

  public void Clicked()
  {
    GameManager.Instance.SetCursorPlain();
    if ((Object) this.borderSR != (Object) null)
      this.borderSR.color = this.totalAlpha;
    if ((double) this.scaleOriX != 0.0 && (double) this.scaleOriY != 0.0)
      this.transform.localScale = new Vector3(this.scaleOriX, this.scaleOriY, 1f);
    this.DoShow(false);
    Scene activeScene = SceneManager.GetActiveScene();
    string name = this.gameObject.name;
    if ((bool) (Object) CardCraftManager.Instance)
    {
      bool flag = true;
      switch (name)
      {
        case "AdvancedCraftButton":
          CardCraftManager.Instance.AdvancedCraft(true);
          break;
        case "AffordableCraftButton":
          CardCraftManager.Instance.AffordableCraft(true);
          break;
        case "ApplyFilterButton":
          CardCraftManager.Instance.ApplyFilter();
          break;
        case "ChallengeReady":
          ChallengeSelectionManager.Instance.Ready();
          break;
        case "ChallengeReroll":
          ChallengeSelectionManager.Instance.RerollFromButton();
          break;
        case "ChallengeSelect1":
          ChallengeSelectionManager.Instance.SelectPack(this.auxInt, 0);
          break;
        case "ChallengeSelect2":
          ChallengeSelectionManager.Instance.SelectPack(this.auxInt, 1);
          break;
        case "ChallengeSelect3":
          ChallengeSelectionManager.Instance.SelectPack(this.auxInt, 2);
          break;
        case "ChallengeSelect4":
          ChallengeSelectionManager.Instance.SelectPack(this.auxInt, 3);
          break;
        case "ChallengeSelect5":
          ChallengeSelectionManager.Instance.SelectPack(this.auxInt, 4);
          break;
        case "ChallengeSelect6":
          ChallengeSelectionManager.Instance.SelectPack(this.auxInt, 5);
          break;
        case "ChallengeSelect7":
          ChallengeSelectionManager.Instance.SelectPack(this.auxInt, 6);
          break;
        case "ChallengeSelect8":
          ChallengeSelectionManager.Instance.SelectPack(this.auxInt, 7);
          break;
        case "CloseFilterButton":
          CardCraftManager.Instance.ShowFilter(false);
          break;
        case "CorruptButtonAction":
          CardCraftManager.Instance.DoButtonCorruption(this.auxInt);
          break;
        case "CorruptCard":
          CardCraftManager.Instance.CorruptCard();
          break;
        case "CorruptCharacterStats":
          if ((bool) (Object) MapManager.Instance)
          {
            MapManager.Instance.ShowCharacterStats();
            break;
          }
          break;
        case "CraftBuyButton":
          CardCraftManager.Instance.BuyCraft(this.auxString);
          break;
        case "CraftDivination":
          CardCraftManager.Instance.BuyDivination(this.auxInt);
          break;
        case "CraftItemBuyButton":
          CardCraftManager.Instance.BuyItem(this.auxString);
          break;
        case "CraftPage":
          CardCraftManager.Instance.ChangePage(this.auxInt);
          break;
        case "ExitCraft":
          CardCraftManager.Instance.Ready();
          break;
        case "FilterButton":
          CardCraftManager.Instance.ShowFilter(true);
          break;
        case "RemoveCard":
          CardCraftManager.Instance.RemoveCard();
          break;
        case "RerollItems":
          CardCraftManager.Instance.RerollItems();
          break;
        case "ResetFilterButton":
          CardCraftManager.Instance.ResetFilter();
          break;
        case "UpgradeA":
          CardCraftManager.Instance.BuyUpgrade("A");
          break;
        case "UpgradeB":
          CardCraftManager.Instance.BuyUpgrade("B");
          break;
        default:
          flag = false;
          break;
      }
      if (flag)
        return;
    }
    switch (name)
    {
      case "Tutorial_Continue":
        GameManager.Instance.HideTutorialPopup();
        break;
      case "MainMenu":
        SceneStatic.LoadByName("MainMenu");
        break;
      case "CloseCardScreen":
        CardScreenManager.Instance.ShowCardScreen(false);
        break;
      default:
        if (TomeManager.Instance.IsActive())
        {
          switch (name)
          {
            case "TomeExit":
              TomeManager.Instance.ShowTome(false);
              return;
            case "TomeMainButton":
              TomeManager.Instance.DoTomeMain();
              return;
            case "TomeCardsButton":
              TomeManager.Instance.DoTomeCards();
              return;
            case "TomeItemsButton":
              TomeManager.Instance.DoTomeItems();
              return;
            case "TomeGlossaryButton":
              TomeManager.Instance.DoTomeGlossary();
              return;
            case "TomeRunsButton":
              TomeManager.Instance.DoTomeRuns();
              return;
            case "TomeScoreboardButton":
              TomeManager.Instance.DoTomeScoreboard();
              return;
            default:
              if (this.auxString == "GlossaryItemIndex")
              {
                TomeManager.Instance.SetGlossaryPageFromButton(this.auxInt);
                return;
              }
              switch (name)
              {
                case "TomeChallengeButton":
                  return;
                case "TomeMonstersButton":
                  return;
                case "ButtonPrevWeekly":
                  TomeManager.Instance.PrevWeekly();
                  return;
                case "ButtonNextWeekly":
                  TomeManager.Instance.NextWeekly();
                  return;
                case "RunDetailClose":
                  TomeManager.Instance.RunDetailClose();
                  return;
                case "tomeCharactersButton":
                  TomeManager.Instance.RunDetailOpenCharacter(this.auxInt);
                  return;
                case "ButtonPrevPath":
                  TomeManager.Instance.PrevPath();
                  return;
                case "ButtonNextPath":
                  TomeManager.Instance.NextPath();
                  return;
                default:
                  return;
              }
          }
        }
        else if ((bool) (Object) PerkTree.Instance && PerkTree.Instance.IsActive())
        {
          switch (name)
          {
            case "PerkTreeType":
              PerkTree.Instance.SetCategory(this.auxInt);
              return;
            case "ClosePerkTree":
              PerkTree.Instance.Hide();
              return;
            case "PerkTreeReset":
              PerkTree.Instance.PerksReset();
              return;
            case "PerkTreeImport":
              PerkTree.Instance.ImportTree();
              return;
            case "PerkTreeExport":
              PerkTree.Instance.ExportTree();
              return;
            case "PerkConfirm":
              PerkTree.Instance.PerksAssignConfirm();
              return;
            case "PerkSaveSlot":
              PerkTree.Instance.SavePerkSlot(this.auxInt);
              return;
            case "PerkRemoveSlot":
              PerkTree.Instance.RemovePerkSlot(this.auxInt);
              return;
            default:
              return;
          }
        }
        else
        {
          if (name == "ExitSideCharacter")
          {
            Transform parent = this.transform.parent.transform.parent;
            if (!((Object) parent != (Object) null))
              break;
            parent.GetComponent<CharacterWindowUI>().Hide();
            break;
          }
          if ((bool) (Object) LobbyManager.Instance)
          {
            switch (name)
            {
              case "EUregion":
                LobbyManager.Instance.SetRegion("eu");
                return;
              case "USregion":
                LobbyManager.Instance.SetRegion("us");
                return;
              case "ASIAregion":
                LobbyManager.Instance.SetRegion("asia");
                return;
              case "DisconnectRegion":
                LobbyManager.Instance.DisconnectRegion(true);
                return;
              default:
                return;
            }
          }
          else
          {
            switch (name)
            {
              case "MadnessLevel":
                MadnessManager.Instance.ShowMadness();
                return;
              case "SandboxMode":
                SandboxManager.Instance.ShowSandbox();
                return;
              case "WeeklyModifiers":
                MadnessManager.Instance.ShowMadness();
                return;
              default:
                if (MadnessManager.Instance.IsActive())
                {
                  switch (name)
                  {
                    case "Madness":
                      MadnessManager.Instance.SelectMadness(this.auxInt);
                      return;
                    case "MadnessBox":
                      MadnessManager.Instance.SelectMadnessCorruptor(this.auxInt);
                      return;
                    case "MadnessExit":
                      MadnessManager.Instance.ShowMadness();
                      return;
                    case "MadnessConfirm":
                      MadnessManager.Instance.MadnessConfirm();
                      return;
                    case "MadnessGoSandbox":
                      AtOManager.Instance.GoSandboxFromMadness();
                      return;
                    default:
                      return;
                  }
                }
                else if (SandboxManager.Instance.IsActive())
                {
                  switch (name)
                  {
                    case "SandboxBox":
                    case "SandboxBoxCheck":
                      SandboxManager.Instance.BoxClick(this.auxString, this.auxInt);
                      return;
                    case "SandboxReset":
                      SandboxManager.Instance.Reset();
                      return;
                    case "SandboxExit":
                      SandboxManager.Instance.CloseSandbox();
                      return;
                    case "SandboxEnable":
                      SandboxManager.Instance.EnableSandbox();
                      return;
                    case "SandboxDisable":
                      SandboxManager.Instance.DisableSandbox();
                      return;
                    case "SandboxGoMadness":
                      AtOManager.Instance.GoMadnessFromSandbox();
                      return;
                    default:
                      return;
                  }
                }
                else
                {
                  if (name == "GiveGold")
                  {
                    GiveManager.Instance.ShowGive(true);
                    return;
                  }
                  if ((bool) (Object) GiveManager.Instance && GiveManager.Instance.IsActive())
                  {
                    switch (name)
                    {
                      case "GiveShards":
                        GiveManager.Instance.ShowGive(true, 1);
                        return;
                      case "GiveClose":
                        GiveManager.Instance.ShowGive(false);
                        return;
                      case "GiveWindowGold":
                        GiveManager.Instance.ShowGive(true);
                        return;
                      case "GiveWindowDust":
                        GiveManager.Instance.ShowGive(true, 1);
                        return;
                      case "GiveMinus1":
                        GiveManager.Instance.Give(-1);
                        return;
                      case "GiveMinus20":
                        GiveManager.Instance.Give(-20);
                        return;
                      case "GiveMinus100":
                        GiveManager.Instance.Give(-100);
                        return;
                      case "GiveMinus1000":
                        GiveManager.Instance.Give(-1000);
                        return;
                      case "GivePlus1":
                        GiveManager.Instance.Give(1);
                        return;
                      case "GivePlus20":
                        GiveManager.Instance.Give(20);
                        return;
                      case "GivePlus100":
                        GiveManager.Instance.Give(100);
                        return;
                      case "GivePlus1000":
                        GiveManager.Instance.Give(1000);
                        return;
                      case "GivePrev":
                        GiveManager.Instance.PrevTarget();
                        return;
                      case "GiveNext":
                        GiveManager.Instance.NextTarget();
                        return;
                      case "GiveAction":
                        GiveManager.Instance.GiveAction();
                        return;
                      default:
                        return;
                    }
                  }
                  else
                  {
                    if (activeScene.name == "CardPlayer")
                    {
                      if (!(name == "ShuffleGame"))
                        return;
                      CardPlayerManager.Instance.Shuffle();
                      return;
                    }
                    if (activeScene.name == "CardPlayerPairs")
                    {
                      switch (name)
                      {
                        case "ShuffleGame":
                          CardPlayerPairsManager.Instance.Shuffle();
                          return;
                        case "FinishPairGame":
                          CardPlayerPairsManager.Instance.FinishPairGame();
                          return;
                        default:
                          return;
                      }
                    }
                    else if (activeScene.name == "HeroSelection")
                    {
                      switch (name)
                      {
                        case "BeginAdventure":
                          HeroSelectionManager.Instance.BeginAdventure();
                          return;
                        case "ChangeSeed":
                          HeroSelectionManager.Instance.ChangeSeed();
                          return;
                        case "CharPopStats":
                          HeroSelectionManager.Instance.DoCharPopMenu("stats");
                          return;
                        case "CharPopRank":
                          HeroSelectionManager.Instance.DoCharPopMenu("rank");
                          return;
                        case "CharPopSkins":
                          HeroSelectionManager.Instance.DoCharPopMenu("skins");
                          return;
                        case "CharPopPerks":
                          HeroSelectionManager.Instance.DoCharPopMenu("perks");
                          return;
                        case "CharPopCardbacks":
                          HeroSelectionManager.Instance.DoCharPopMenu("cardbacks");
                          return;
                        case "SupplyLevelingAction":
                          HeroSelectionManager.Instance.LevelWithSupplies(this.auxString);
                          return;
                        case "NGBox":
                          HeroSelectionManager.Instance.NGBox();
                          return;
                        case "FollowBox":
                          HeroSelectionManager.Instance.FollowTheLeader();
                          return;
                        case "HeroSelectionReady":
                          HeroSelectionManager.Instance.Ready();
                          return;
                        default:
                          return;
                      }
                    }
                    else
                    {
                      if (activeScene.name == "IntroNewGame")
                      {
                        if (!(name == "Skip"))
                          return;
                        IntroNewGameManager.Instance.SkipIntro();
                        return;
                      }
                      if (activeScene.name == "Combat")
                      {
                        switch (name)
                        {
                          case "Character_CombatDeck":
                            MatchManager.Instance.ShowCharacterWindow("combatdeck");
                            return;
                          case "Character_CombatDiscard":
                            MatchManager.Instance.ShowCharacterWindow("combatdiscard");
                            return;
                          case "Character_CombatVanish":
                            MatchManager.Instance.ShowCharacterWindow("combatvanish");
                            return;
                          case "Character_Level":
                            MatchManager.Instance.ShowCharacterWindow("level");
                            return;
                          case "Character_Items":
                            MatchManager.Instance.ShowCharacterWindow("items");
                            return;
                          case "Character_Stats":
                            MatchManager.Instance.ShowCharacterWindow("stats");
                            return;
                          case "Character_Perks":
                            MatchManager.Instance.ShowCharacterWindow("perks");
                            return;
                          case "NPC_Casted":
                            MatchManager.Instance.ShowCharacterWindow("combatdiscard", false);
                            return;
                          case "NPC_Stats":
                            MatchManager.Instance.ShowCharacterWindow("stats", false);
                            return;
                          case "CombatConsoleClose":
                            MatchManager.Instance.ShowLog();
                            return;
                          default:
                            return;
                        }
                      }
                      else if (activeScene.name == "Town")
                      {
                        switch (name)
                        {
                          case "Ready":
                            if ((Object) AtOManager.Instance.GetTownDivinationTier() != (Object) null)
                              return;
                            TownManager.Instance.Ready();
                            return;
                          case "PointsTownUpgrade":
                            PlayerManager.Instance.GainSupply(10);
                            return;
                          case "ResetTownUpgrade":
                            PlayerManager.Instance.ResetTownUpgrade();
                            TownManager.Instance.RefreshTownUpgrades();
                            return;
                          case "ClaimTreasure":
                            if (this.auxInt == 1000)
                            {
                              TownManager.Instance.ClaimTreasureCommunity(this.auxString);
                              return;
                            }
                            TownManager.Instance.ClaimTreasure(this.auxString);
                            return;
                          case "Join_Divination":
                            AtOManager.Instance.JoinCardDivination();
                            return;
                          case "Character_Deck":
                            TownManager.Instance.ShowCharacterWindow("deck");
                            return;
                          case "Character_Level":
                            TownManager.Instance.ShowCharacterWindow("level");
                            return;
                          case "Character_Items":
                            TownManager.Instance.ShowCharacterWindow("items");
                            return;
                          case "Character_Stats":
                            TownManager.Instance.ShowCharacterWindow("stats");
                            return;
                          case "Character_Perks":
                            TownManager.Instance.ShowCharacterWindow("perks");
                            return;
                          case "PetShop":
                            CardCraftManager.Instance.DoPetShop();
                            return;
                          case "ItemShop":
                            CardCraftManager.Instance.DoItemShop();
                            return;
                          case "ExitTownUpgrades":
                            TownManager.Instance.ShowTownUpgrades(false);
                            return;
                          case "SaveLoad":
                            CardCraftManager.Instance.ShowSaveLoad();
                            return;
                          case "SaveSlot":
                            CardCraftManager.Instance.SaveDeck(this.auxInt);
                            return;
                          case "RemoveSlot":
                            CardCraftManager.Instance.RemoveDeck(this.auxInt);
                            return;
                          case "BotCraftingDeck":
                            CardCraftManager.Instance.CraftDeck();
                            return;
                          case "SellSupply":
                            TownManager.Instance.SellSupply();
                            return;
                          case "SupplyClose":
                            TownManager.Instance.CloseSupply();
                            return;
                          case "SupplyMinus1":
                            TownManager.Instance.ModifySupplyQuantity(-1);
                            return;
                          case "SupplyMinus5":
                            TownManager.Instance.ModifySupplyQuantity(-5);
                            return;
                          case "SupplyPlus1":
                            TownManager.Instance.ModifySupplyQuantity(1);
                            return;
                          case "SupplyPlus5":
                            TownManager.Instance.ModifySupplyQuantity(5);
                            return;
                          case "SellSupplyAction":
                            TownManager.Instance.SellSupplyAction();
                            return;
                          default:
                            return;
                        }
                      }
                      else if (activeScene.name == "Rewards")
                      {
                        switch (name)
                        {
                          case "Dust":
                            if (GameManager.Instance.IsMultiplayer())
                            {
                              this.transform.parent.transform.parent.GetComponent<CharacterReward>().DustSelected(NetworkManager.Instance.GetPlayerNick());
                              return;
                            }
                            this.transform.parent.transform.parent.GetComponent<CharacterReward>().DustSelected("");
                            return;
                          case "Character_Deck":
                            RewardsManager.Instance.ShowCharacterWindow("deck");
                            return;
                          case "Character_Level":
                            RewardsManager.Instance.ShowCharacterWindow("level");
                            return;
                          case "Character_Items":
                            RewardsManager.Instance.ShowCharacterWindow("items");
                            return;
                          case "Character_Stats":
                            RewardsManager.Instance.ShowCharacterWindow("stats");
                            return;
                          case "Character_Perks":
                            RewardsManager.Instance.ShowCharacterWindow("perks");
                            return;
                          case "RestartRewards":
                            RewardsManager.Instance.RestartRewards();
                            return;
                          default:
                            return;
                        }
                      }
                      else if (activeScene.name == "Loot")
                      {
                        switch (name)
                        {
                          case "GoldLoot":
                            LootManager.Instance.LootGold();
                            return;
                          case "Character_Deck":
                            LootManager.Instance.ShowCharacterWindow("deck");
                            return;
                          case "Character_Level":
                            LootManager.Instance.ShowCharacterWindow("level");
                            return;
                          case "Character_Items":
                            LootManager.Instance.ShowCharacterWindow("items");
                            return;
                          case "Character_Stats":
                            LootManager.Instance.ShowCharacterWindow("stats");
                            return;
                          case "Character_Perks":
                            LootManager.Instance.ShowCharacterWindow("perks");
                            return;
                          case "RestartLoot":
                            LootManager.Instance.RestartLoot();
                            return;
                          default:
                            return;
                        }
                      }
                      else
                      {
                        if (!(activeScene.name == "Map"))
                          return;
                        switch (name)
                        {
                          case "EventContinue":
                            MapManager.Instance.EventReady();
                            return;
                          case "CharacterUnlockClose":
                            MapManager.Instance.CharacterUnlockClose();
                            return;
                          case "Character_Deck":
                            MapManager.Instance.ShowCharacterWindow("deck");
                            return;
                          case "Character_Level":
                            MapManager.Instance.ShowCharacterWindow("level");
                            return;
                          case "Character_Items":
                            MapManager.Instance.ShowCharacterWindow("items");
                            return;
                          case "Character_Stats":
                            MapManager.Instance.ShowCharacterWindow("stats");
                            return;
                          case "Character_Perks":
                            MapManager.Instance.ShowCharacterWindow("perks");
                            return;
                          case "CorruptionBox":
                            MapManager.Instance.CorruptionBox();
                            return;
                          case "CorruptionContinue":
                            MapManager.Instance.CorruptionContinue();
                            return;
                          case "CorruptionHide":
                            MapManager.Instance.CorruptionShowHide();
                            return;
                          case "CorruptionRewardA":
                            MapManager.Instance.CorruptionSelectReward("A");
                            return;
                          case "CorruptionRewardB":
                            MapManager.Instance.CorruptionSelectReward("B");
                            return;
                          case "ConflictButton":
                            MapManager.Instance.ConflictSelection(this.auxInt);
                            return;
                          case "EventShowHide":
                            MapManager.Instance.ShowHideEvent();
                            return;
                          default:
                            return;
                        }
                      }
                    }
                  }
                }
            }
          }
        }
    }
  }

  public void DoShow(bool _show)
  {
    this.show = _show;
    if (this.borderCo != null)
      this.StopCoroutine(this.borderCo);
    this.borderCo = this.StartCoroutine(this.DoShowCo());
  }

  private IEnumerator DoShowCo()
  {
    bool exit = false;
    while (!exit)
    {
      if (this.buttonEnabled)
      {
        if (this.show && (double) this.borderSR.color.a < 0.60000002384185791)
          this.borderSR.color += this.incrementColor;
        else if (!this.show && (double) this.borderSR.color.a > 0.0)
          this.borderSR.color -= this.incrementColor;
        else
          exit = true;
      }
      else
        exit = true;
      yield return (object) null;
    }
  }
}
