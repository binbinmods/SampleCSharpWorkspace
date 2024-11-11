// Decompiled with JetBrains decompiler
// Type: CardCraftManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CardCraftManager : MonoBehaviour
{
  public Transform deckUI;
  public DeckEnergy deckEnergy;
  public Transform canvasSearchT;
  public TMP_InputField searchInput;
  public TMP_Text searchInputPlaceholder;
  public TextMeshProUGUI searchInputPlaceholderGUI;
  public TMP_Text searchInputText;
  public Transform canvasSearchCloseT;
  public Transform backgroundCraft;
  public Transform backgroundTransmute;
  public Transform backgroundRemove;
  public Transform backgroundDivination;
  public Transform backgroundCorruption;
  public Transform backgroundItems;
  public Transform backgroundChallenge;
  public Transform exitCraftButton;
  public GameObject cardVerticalPrefab;
  public GameObject cardCraftBuyButton;
  public GameObject cardCraftItemBuyButton;
  public GameObject cardCraftPageButton;
  public GameObject cardDivinationButton;
  public Transform cardCraftElements;
  public Transform cardCraftSave;
  public GameObject cardCraftItem;
  public RectTransform cardListContainerRectTransform;
  public Transform cardListContainer;
  public TMP_Text cardChallengeGlobalTitle;
  public TMP_Text cardChallengeGlobalIntro;
  public Transform[] cardChallengeContainer;
  public Transform[] cardChallengeButton;
  public TitleMovement[] cardChallengeTitle;
  public Transform[] cardChallengeSelected;
  public TMP_Text cardChallengeRound;
  public BotonGeneric rerollChallenge;
  public Transform challengeReadyBlock;
  public Transform readyChallenge;
  public Transform waitingMsgChallenge;
  public TMP_Text waitingMsgTextChallenge;
  public TMP_Text cardChallengeBonus;
  public PerkChallengeItem[] perkChallengeItems;
  public Transform challengePerks;
  public Transform cardUpgradeContainer;
  public Transform cardRemoveContainer;
  public Transform cardCraftContainer;
  public Transform cardItemContainer;
  public Transform cardCraftPageContainer;
  public Transform cardCraftEnergySelectorContainer;
  public Transform cardCraftRaritySelectorContainer;
  public Transform itemsCraftPageContainer;
  public Transform divinationButtonContainer;
  public Transform divinationWaitingContainer;
  public Transform cardCorruptionContainer;
  public Animator removeAnim;
  public Animator transformAnim;
  public Animator corruptAnim;
  public Transform tmpContainer;
  public TMP_Text divinationWaitingMsg;
  public TMP_Text cardsOwner;
  public Transform rerollButton;
  public Transform rerollButtonLock;
  public Transform rerollButtonWarning;
  public Transform petShopButton;
  public Transform itemShopButton;
  public Transform maxPetNumber;
  public Transform upgradeAText;
  public Transform upgradeBText;
  public TMP_Text oldcostAText;
  public TMP_Text oldcostBText;
  public Transform transformAText;
  public Transform transformBText;
  public Transform transformRemoveText;
  public TMP_Text oldcostRemoveText;
  public Transform usesLeftT;
  public TMP_Text usesLeftText;
  public Transform minCardsT;
  public TMP_Text minCardsText;
  public TMP_Text discountText;
  public ItemCombatIcon iconWeapon;
  public ItemCombatIcon iconArmor;
  public ItemCombatIcon iconAccesory;
  public ItemCombatIcon iconJewelry;
  public ItemCombatIcon iconPet;
  public TMP_Text itemsOwner;
  public BotonRollover iconDeck;
  public TMP_Text iconDeckText;
  public SpriteRenderer deckBgBorder;
  public Transform cardSingle0;
  public Transform cardSingle1;
  public Transform cardSingle2;
  public Transform arrowTR;
  public Transform arrowTL;
  public Transform arrowRL;
  public Transform arrowLR;
  public Transform buttonL;
  public Transform buttonR;
  public Transform buttonRemove;
  public Transform buttonCorruption;
  public List<Transform> corruptionArrows;
  public List<BotonGeneric> corruptionButtons;
  public Transform corruptionCharacterStats;
  public List<TMP_Text> corruptionTexts;
  public TMP_Text corruptionPercent;
  public TMP_Text corruptionPercentRoll;
  public Transform corruptionPercentRollSuccess;
  public Transform corruptionPercentRollFail;
  private int[] corruptionTry = new int[4];
  private int corruptionValue;
  public BotonAdvancedCraft buttonAffordableCraft;
  public BotonAdvancedCraft buttonAdvancedCraft;
  public BotonAdvancedCraft buttonFilterCraft;
  public Transform filterWindow;
  private string itemListId;
  private bool isPetShop;
  private GameObject[] deckGOs;
  private Hero currentHero;
  public int heroIndex;
  private Coroutine blockedCoroutine;
  private int costA;
  private int costB;
  private int costRemove;
  private int costCorruption;
  private int costDust;
  private CardData cardData;
  private string cardActiveName = "";
  private CardVertical cardActive;
  private CardVertical[] cardVerticalDeck;
  private Vector3 posPlain = new Vector3(0.0f, 1.92f, -1f);
  private Vector3 posUpgradeA = new Vector3(-1.84f, -1.69f, -1f);
  private Vector3 posUpgradeB = new Vector3(1.84f, -1.69f, -1f);
  private Vector3 posRemove = new Vector3(0.03f, 0.63f, -1f);
  private CardItem CIPlain;
  private CardItem CIA;
  private CardItem CIB;
  private CardItem CIRemove;
  private CardItem CICorruption;
  private BotonGeneric BG_Left;
  private BotonGeneric BG_Right;
  private BotonGeneric BG_Remove;
  private BotonGeneric BG_Corruption;
  public bool blocked;
  private int discount;
  private int remainingUses = -1;
  private Coroutine craftCoroutine;
  private Coroutine createDeckCoroutine;
  private int currentItemsPageNum;
  private int currentCraftPageNum;
  private int maxCraftPageNum;
  private bool currentCraftAllRarities;
  private bool currentCraftAllCosts;
  private Dictionary<Enums.CardRarity, bool> currentCraftRarity;
  private int currentCraftCost;
  private CardCraftSelectorEnergy selectorEnergy;
  private Enums.CardRarity maxCraftRarity;
  private int craftTierZone;
  private Dictionary<int, CardCraftItem> craftCardItemDict = new Dictionary<int, CardCraftItem>();
  public List<string> craftFilterAura = new List<string>();
  public List<string> craftFilterCurse = new List<string>();
  public List<string> craftFilterDT = new List<string>();
  private List<BotonFilter> craftBotonFilters = new List<BotonFilter>();
  public GameObject portraitItem;
  public int craftType;
  private int savingSlot = -1;
  public DeckSlot[] deckSlot;
  public Transform loadDeckContainer;
  public Transform loadDeckCardContainer;
  public SpriteRenderer loadDeckHeroSprite;
  public TMP_Text deckCraftPrice;
  public TMP_Text loadDeckHeroName;
  public TMP_Text containerDeckName;
  public BotonGeneric botCraftingDeck;
  public BotonGeneric botSaveLoad;
  private int deckCraftingCostGold;
  private int deckCraftingCostDust;
  private Dictionary<string, int> craftTimes;
  public bool deckAvailableForSaveLoad = true;
  private string searchTerm = "";
  private Coroutine searchCo;
  private bool statusReady;
  public Transform waitingMsg;
  public TMP_Text waitingMsgText;
  private Coroutine manualReadyCo;
  private PhotonView photonView;
  public SpriteRenderer[] cardbackSprites;
  private Coroutine assignSpecial;
  private Coroutine assignCard;
  private int controllerBlock = -1;
  private int controllerVerticalIndex = -1;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private Vector3 auxVector = Vector3.zero;
  private List<Transform> _controllerList = new List<Transform>();
  private List<Transform> _controllerVerticalList = new List<Transform>();
  private bool controllerIsOnVerticalList;

  public static CardCraftManager Instance { get; private set; }

  public int CurrentItemsPageNum
  {
    get => this.currentItemsPageNum;
    set => this.currentItemsPageNum = value;
  }

  private void Awake()
  {
    if ((UnityEngine.Object) CardCraftManager.Instance == (UnityEngine.Object) null)
      CardCraftManager.Instance = this;
    else if ((UnityEngine.Object) CardCraftManager.Instance != (UnityEngine.Object) this)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    this.BG_Right = this.buttonR.GetComponent<BotonGeneric>();
    this.BG_Left = this.buttonL.GetComponent<BotonGeneric>();
    this.BG_Remove = this.buttonRemove.GetComponent<BotonGeneric>();
    this.BG_Corruption = this.buttonCorruption.GetComponent<BotonGeneric>();
    this.currentCraftRarity = new Dictionary<Enums.CardRarity, bool>();
    this.currentCraftRarity.Add(Enums.CardRarity.Common, false);
    this.currentCraftRarity.Add(Enums.CardRarity.Uncommon, false);
    this.currentCraftRarity.Add(Enums.CardRarity.Rare, false);
    this.currentCraftRarity.Add(Enums.CardRarity.Epic, false);
    this.currentCraftRarity.Add(Enums.CardRarity.Mythic, false);
    if (!((UnityEngine.Object) MapManager.Instance != (UnityEngine.Object) null))
      return;
    this.photonView = MapManager.Instance.GetPhotonView();
  }

  private void Start() => this.Resize();

  public void DoMouseScroll(Vector2 vectorScroll)
  {
    if (!this.cardCraftElements.gameObject.activeSelf || this.maxCraftPageNum <= 1)
      return;
    if ((double) vectorScroll.y > 0.0)
    {
      RaycastHit2D raycastHit2D = Physics2D.Raycast((Vector2) GameManager.Instance.cameraMain.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
      if ((UnityEngine.Object) raycastHit2D.collider != (UnityEngine.Object) null && (UnityEngine.Object) raycastHit2D.transform != (UnityEngine.Object) null && raycastHit2D.transform.name.Split('_', StringSplitOptions.None)[0] == "deckcard")
        return;
      this.DoPrevPage();
    }
    else
    {
      if ((double) vectorScroll.y >= 0.0)
        return;
      RaycastHit2D raycastHit2D = Physics2D.Raycast((Vector2) GameManager.Instance.cameraMain.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
      if ((UnityEngine.Object) raycastHit2D.collider != (UnityEngine.Object) null && (UnityEngine.Object) raycastHit2D.transform != (UnityEngine.Object) null && raycastHit2D.transform.name.Split('_', StringSplitOptions.None)[0] == "deckcard")
        return;
      this.DoNextPage();
    }
  }

  public void AffordableCraft(bool change = false)
  {
    if (change)
    {
      AtOManager.Instance.affordableCraft = !AtOManager.Instance.affordableCraft;
      this.ShowListCardsForCraft(1, true);
    }
    this.buttonAffordableCraft.SetState(AtOManager.Instance.affordableCraft);
    this.buttonAffordableCraft.GetComponent<BotonGeneric>().ShowDisableMask(!AtOManager.Instance.affordableCraft);
  }

  public void AdvancedCraft(bool change = false)
  {
    this.maxCraftPageNum = 1;
    if (change)
    {
      AtOManager.Instance.advancedCraft = !AtOManager.Instance.advancedCraft;
      this.ShowListCardsForCraft(1, true);
    }
    this.buttonAdvancedCraft.SetState(AtOManager.Instance.advancedCraft);
    this.buttonAdvancedCraft.GetComponent<BotonGeneric>().ShowDisableMask(!AtOManager.Instance.advancedCraft);
  }

  public void ExitCardCraftAlert()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.ExitCardCraftAlert);
    AtOManager.Instance.CloseCardCraft();
  }

  public void ExitCardCraft()
  {
    if (this.blocked)
      return;
    AtOManager.Instance.CloseCardCraft();
  }

  public void Resize() => this.exitCraftButton.transform.localPosition = new Vector3((float) (-(double) Globals.Instance.sizeW * 0.5 + 1.0 * (double) Globals.Instance.multiplierX), (float) (-(double) Globals.Instance.sizeH * 0.5 + 3.7200000286102295 * (double) Globals.Instance.multiplierY), this.exitCraftButton.transform.localPosition.z);

  private void DoNextPage()
  {
    if (this.craftType != 2 && this.craftType != 4)
      return;
    if (this.craftType == 2)
    {
      if (this.currentCraftPageNum == this.maxCraftPageNum)
        this.ChangePage(1);
      else
        this.ChangePage(this.currentCraftPageNum + 1);
    }
    else if (this.currentItemsPageNum == this.maxCraftPageNum)
      this.ChangePage(1);
    else
      this.ChangePage(this.currentItemsPageNum + 1);
  }

  private void DoPrevPage()
  {
    if (this.craftType != 2 && this.craftType != 4)
      return;
    if (this.craftType == 2)
    {
      if (this.currentCraftPageNum == 1)
        this.ChangePage(this.maxCraftPageNum);
      else
        this.ChangePage(this.currentCraftPageNum - 1);
    }
    else if (this.currentItemsPageNum == 1)
      this.ChangePage(this.maxCraftPageNum);
    else
      this.ChangePage(this.currentItemsPageNum - 1);
  }

  public void ShowCardCraft(int type = 0)
  {
    this.waitingMsg.gameObject.SetActive(false);
    if (GameManager.Instance.IsMultiplayer())
    {
      if (!(bool) (UnityEngine.Object) TownManager.Instance)
      {
        this.statusReady = false;
        this.exitCraftButton.GetComponent<BotonGeneric>().SetBackgroundColor(Functions.HexToColor(Globals.Instance.ClassColor["warrior"]));
        if (NetworkManager.Instance.IsMaster())
          NetworkManager.Instance.ClearAllPlayerManualReady();
      }
      else
        TownManager.Instance.DisableReady();
    }
    this.transform.localPosition = new Vector3(0.0f, 0.2f, this.transform.localPosition.z);
    this.craftType = type;
    if ((bool) (UnityEngine.Object) MapManager.Instance)
      AudioManager.Instance.DoBSO("Craft");
    this.SetMaxQuantity();
    if ((bool) (UnityEngine.Object) MapManager.Instance)
    {
      MapManager.Instance.HidePopup();
      if (MapManager.Instance.characterWindow.IsActive())
        MapManager.Instance.characterWindow.Hide();
      if (this.craftType != 3)
      {
        MapManager.Instance.sideCharacters.EnableOwnedCharacters();
        this.heroIndex = MapManager.Instance.sideCharacters.GetFirstEnabledCharacter();
        MapManager.Instance.sideCharacters.SetActive(this.heroIndex);
      }
      MapManager.Instance.sideCharacters.ShowLevelUpCharacters();
      MapManager.Instance.sideCharacters.InCharacterScreen(true);
    }
    else if ((bool) (UnityEngine.Object) TownManager.Instance)
    {
      if (this.craftType != 3)
      {
        TownManager.Instance.sideCharacters.EnableOwnedCharacters();
        this.heroIndex = TownManager.Instance.LastUsedCharacter == -1 ? TownManager.Instance.sideCharacters.GetFirstEnabledCharacter() : TownManager.Instance.LastUsedCharacter;
        TownManager.Instance.sideCharacters.SetActive(this.heroIndex);
      }
      TownManager.Instance.sideCharacters.ShowLevelUpCharacters();
      TownManager.Instance.ShowButtons(false);
    }
    this.craftCardItemDict = new Dictionary<int, CardCraftItem>();
    this.currentHero = AtOManager.Instance.GetHero(this.heroIndex);
    this.SetCardbacks(this.heroIndex);
    if (this.craftType == 0)
    {
      this.backgroundTransmute.gameObject.SetActive(true);
      this.backgroundRemove.gameObject.SetActive(false);
      this.backgroundCraft.gameObject.SetActive(false);
      this.backgroundDivination.gameObject.SetActive(false);
      this.backgroundItems.gameObject.SetActive(false);
      this.backgroundChallenge.gameObject.SetActive(false);
      this.backgroundCorruption.gameObject.SetActive(false);
      this.minCardsT.gameObject.SetActive(false);
      this.StartCoroutine(this.ShowCardsSingle());
      if (AtOManager.Instance.TownTutorialStep == 1)
      {
        Hero hero = (Hero) null;
        for (int index = 0; index < 4; ++index)
        {
          hero = AtOManager.Instance.GetHero(index);
          if (hero != null)
          {
            int num = hero.SourceName == "Magnus" ? 1 : 0;
          }
        }
        if (hero == null)
          AtOManager.Instance.IncreaseTownTutorialStep();
        else
          GameManager.Instance.ShowTutorialPopup("townItemUpgrade", Vector3.zero, Vector3.zero);
      }
    }
    else if (this.craftType == 1)
    {
      this.backgroundRemove.gameObject.SetActive(true);
      this.backgroundTransmute.gameObject.SetActive(false);
      this.backgroundCraft.gameObject.SetActive(false);
      this.backgroundDivination.gameObject.SetActive(false);
      this.backgroundItems.gameObject.SetActive(false);
      this.backgroundChallenge.gameObject.SetActive(false);
      this.backgroundCorruption.gameObject.SetActive(false);
      if (!AtOManager.Instance.Sandbox_noMinimumDecksize)
      {
        this.minCardsT.gameObject.SetActive(true);
        this.minCardsText.text = "*" + Texts.Instance.GetText("minDeckCards").Replace("<n>", 15.ToString());
      }
      else
        this.minCardsT.gameObject.SetActive(false);
    }
    else if (this.craftType == 2)
    {
      this.ShowSearch(true);
      this.backgroundCraft.gameObject.SetActive(true);
      this.cardCraftElements.gameObject.SetActive(true);
      this.cardCraftSave.gameObject.SetActive(false);
      this.backgroundTransmute.gameObject.SetActive(false);
      this.backgroundRemove.gameObject.SetActive(false);
      this.backgroundDivination.gameObject.SetActive(false);
      this.backgroundItems.gameObject.SetActive(false);
      this.backgroundChallenge.gameObject.SetActive(false);
      this.backgroundCorruption.gameObject.SetActive(false);
      this.minCardsT.gameObject.SetActive(false);
      this.canvasSearchCloseT.gameObject.SetActive(false);
      this.searchInputPlaceholder.text = "<voffset=-4><size=+10><sprite name=cards></size></voffset>" + Texts.Instance.GetText("searchCards");
      this.currentCraftAllCosts = true;
      this.currentCraftAllRarities = true;
      this.currentCraftPageNum = 1;
      this.maxCraftPageNum = 1;
      this.craftTierZone = AtOManager.Instance.GetTownTier();
      bool flag = true;
      if ((bool) (UnityEngine.Object) TownManager.Instance)
        flag = false;
      if (flag)
      {
        AtOManager.Instance.advancedCraft = false;
        AtOManager.Instance.affordableCraft = false;
        this.ResetFilter();
      }
      else
      {
        this.AdvancedCraft();
        this.AffordableCraft();
        this.SetFilterButtonState();
        this.ShowListCardsForCraft(1);
      }
      if ((bool) (UnityEngine.Object) MapManager.Instance)
      {
        this.botSaveLoad.gameObject.SetActive(false);
        this.buttonAdvancedCraft.gameObject.SetActive(false);
      }
      else if ((bool) (UnityEngine.Object) TownManager.Instance)
      {
        if (AtOManager.Instance.GetTownTier() == 0 && (!GameManager.Instance.IsMultiplayer() || AtOManager.Instance.GetHero(this.heroIndex) != null && (UnityEngine.Object) AtOManager.Instance.GetHero(this.heroIndex).HeroData != (UnityEngine.Object) null && AtOManager.Instance.GetHero(this.heroIndex).Owner == NetworkManager.Instance.GetPlayerNick()))
          this.botSaveLoad.gameObject.SetActive(true);
        else
          this.botSaveLoad.gameObject.SetActive(false);
        this.buttonAdvancedCraft.gameObject.SetActive(true);
        if (AtOManager.Instance.TownTutorialStep == 0)
        {
          Hero hero = (Hero) null;
          for (int index = 0; index < 4; ++index)
          {
            hero = AtOManager.Instance.GetHero(index);
            if (hero != null)
            {
              int num = hero.SourceName == "Evelyn" ? 1 : 0;
            }
          }
          if (hero == null || hero.Cards.Contains("fireball"))
            AtOManager.Instance.IncreaseTownTutorialStep();
          else
            GameManager.Instance.ShowTutorialPopup("townItemCraft", Vector3.zero, Vector3.zero);
        }
      }
    }
    else if (this.craftType == 3)
    {
      this.backgroundDivination.gameObject.SetActive(true);
      this.backgroundTransmute.gameObject.SetActive(false);
      this.backgroundRemove.gameObject.SetActive(false);
      this.backgroundCraft.gameObject.SetActive(false);
      this.backgroundItems.gameObject.SetActive(false);
      this.backgroundChallenge.gameObject.SetActive(false);
      this.backgroundCorruption.gameObject.SetActive(false);
      this.minCardsT.gameObject.SetActive(false);
      this.deckUI.gameObject.SetActive(false);
      this.deckEnergy.gameObject.SetActive(false);
      this.craftTierZone = AtOManager.Instance.GetTownTier();
      this.SetDivinationButtons();
      if ((UnityEngine.Object) AtOManager.Instance.GetTownDivinationTier() != (UnityEngine.Object) null)
        this.SetDivinationWaiting();
    }
    else if (this.craftType == 4)
    {
      this.ClearItemListContainer();
      this.backgroundItems.gameObject.SetActive(true);
      this.backgroundTransmute.gameObject.SetActive(false);
      this.backgroundRemove.gameObject.SetActive(false);
      this.backgroundCraft.gameObject.SetActive(false);
      this.backgroundDivination.gameObject.SetActive(false);
      this.backgroundChallenge.gameObject.SetActive(false);
      this.backgroundCorruption.gameObject.SetActive(false);
      this.minCardsT.gameObject.SetActive(false);
      this.deckUI.gameObject.SetActive(false);
      this.deckEnergy.gameObject.SetActive(false);
      this.craftTierZone = AtOManager.Instance.GetTownTier();
      this.currentItemsPageNum = 1;
      this.ShowItemsForBuy(this.currentItemsPageNum);
      this.ShowCharacterItems();
      if (!this.isPetShop && AtOManager.Instance.TownTutorialStep == 2 && AtOManager.Instance.CharInTown())
      {
        if (this.craftCardItemDict[this.craftCardItemDict.Count - 1].cardId == "spyglass" && this.craftCardItemDict[this.craftCardItemDict.Count - 1].Available)
        {
          GameManager.Instance.ShowTutorialPopup("townItemLoot", this.craftCardItemDict[this.craftCardItemDict.Count - 1].buttonItem.position, Vector3.zero);
          for (int key = 0; key < this.craftCardItemDict.Count - 1; ++key)
            this.craftCardItemDict[key].EnableButton(false);
        }
        else
          AtOManager.Instance.IncreaseTownTutorialStep();
      }
    }
    else if (this.craftType == 5)
    {
      this.backgroundChallenge.gameObject.SetActive(true);
      this.backgroundTransmute.gameObject.SetActive(false);
      this.backgroundRemove.gameObject.SetActive(false);
      this.backgroundCraft.gameObject.SetActive(false);
      this.backgroundDivination.gameObject.SetActive(false);
      this.backgroundItems.gameObject.SetActive(false);
      this.backgroundCorruption.gameObject.SetActive(false);
      this.minCardsT.gameObject.SetActive(false);
      this.exitCraftButton.gameObject.SetActive(false);
      this.heroIndex = -1;
      this.SelectCharacter(0);
    }
    else if (this.craftType == 6)
    {
      this.corruptAnim.SetTrigger("hide");
      this.corruptionTry = new int[4];
      for (int index = 0; index < 4; ++index)
        this.corruptionTry[index] = 0;
      this.backgroundCorruption.gameObject.SetActive(true);
      this.backgroundRemove.gameObject.SetActive(false);
      this.backgroundTransmute.gameObject.SetActive(false);
      this.backgroundCraft.gameObject.SetActive(false);
      this.backgroundDivination.gameObject.SetActive(false);
      this.backgroundItems.gameObject.SetActive(false);
      this.backgroundChallenge.gameObject.SetActive(false);
      this.minCardsT.gameObject.SetActive(false);
      UnityEngine.Random.InitState((AtOManager.Instance.currentMapNode + AtOManager.Instance.GetGameId() + NetworkManager.Instance.GetPlayerNick() + MapManager.Instance.GetRandomString()).GetDeterministicHashCode());
      this.InitCorruption();
    }
    if (this.craftType >= 3 && this.craftType != 6)
      return;
    this.CreateDeck(this.heroIndex);
  }

  public void ShowSearchFocus() => this.StartCoroutine(this.ShowSearchFocusCo());

  private IEnumerator ShowSearchFocusCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    if (this.searchInput.text == "")
      this.searchInputPlaceholder.GetComponent<TextMeshProUGUI>().enabled = false;
  }

  public void ShowSearch(bool state) => this.canvasSearchT.gameObject.SetActive(state);

  public void ResetSearch() => this.searchInput.text = "";

  public void Search(string _term)
  {
    if (this.searchCo != null)
      this.StopCoroutine(this.searchCo);
    this.searchCo = this.StartCoroutine(this.SearchCoroutine(_term));
  }

  private IEnumerator SearchCoroutine(string _term)
  {
    if (_term != "")
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.35f);
      this.searchTerm = _term.Trim();
      this.canvasSearchCloseT.gameObject.SetActive(true);
    }
    else
    {
      this.searchTerm = "";
      this.canvasSearchCloseT.gameObject.SetActive(false);
    }
    this.ShowListCardsForCraft(1, true);
  }

  private IEnumerator ShowCardsSingle()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.05f);
    this.cardSingle0.gameObject.SetActive(true);
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    this.cardSingle1.gameObject.SetActive(true);
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    this.cardSingle2.gameObject.SetActive(true);
  }

  private void SetCardbacks(int _heroIndex)
  {
    Hero hero = AtOManager.Instance.GetHero(_heroIndex);
    if (hero == null || (UnityEngine.Object) hero.HeroData == (UnityEngine.Object) null)
      return;
    string cardbackUsed = hero.CardbackUsed;
    if (!(cardbackUsed != ""))
      return;
    CardbackData cardbackData = Globals.Instance.GetCardbackData(cardbackUsed);
    if ((UnityEngine.Object) cardbackData == (UnityEngine.Object) null)
    {
      cardbackData = Globals.Instance.GetCardbackData(Globals.Instance.GetCardbackBaseIdBySubclass(hero.HeroData.HeroSubClass.Id));
      if ((UnityEngine.Object) cardbackData == (UnityEngine.Object) null)
        cardbackData = Globals.Instance.GetCardbackData("defaultCardback");
    }
    Sprite cardbackSprite = cardbackData.CardbackSprite;
    if (!((UnityEngine.Object) cardbackSprite != (UnityEngine.Object) null))
      return;
    for (int index = 0; index < this.cardbackSprites.Length; ++index)
    {
      if ((UnityEngine.Object) this.cardbackSprites[index] != (UnityEngine.Object) null)
        this.cardbackSprites[index].sprite = cardbackSprite;
    }
  }

  public void SelectCharacter(int characterIndex)
  {
    if (characterIndex == this.heroIndex)
      return;
    this.ClearTmpContainer();
    if (this.craftCoroutine != null)
    {
      this.StopCoroutine(this.craftCoroutine);
      this.craftCoroutine = (Coroutine) null;
    }
    this.heroIndex = characterIndex;
    this.SetCardbacks(this.heroIndex);
    if (this.craftType != 3)
      this.CreateDeck(characterIndex);
    this.controllerVerticalIndex = -1;
    if (this.craftType == 0)
    {
      this.oldcostAText.text = this.oldcostBText.text = "";
      this.transformAnim.SetTrigger("hide");
    }
    else if (this.craftType == 1)
    {
      this.oldcostRemoveText.text = "";
      this.removeAnim.SetTrigger("hide");
    }
    else if (this.craftType == 2)
    {
      if (this.cardCraftElements.gameObject.activeSelf)
      {
        this.currentCraftPageNum = 1;
        this.maxCraftPageNum = 1;
        this.ShowListCardsForCraft(1);
      }
      else if (!GameManager.Instance.IsMultiplayer() || AtOManager.Instance.GetHero(characterIndex).Owner == NetworkManager.Instance.GetPlayerNick())
        this.LoadDecks();
      else
        this.ShowSaveLoad();
    }
    else if (this.craftType == 4)
    {
      this.ShowCharacterItems();
      if ((UnityEngine.Object) TownManager.Instance != (UnityEngine.Object) null && TownManager.Instance.characterWindow.IsActive())
        TownManager.Instance.ShowDeck(characterIndex);
      else if ((UnityEngine.Object) MapManager.Instance != (UnityEngine.Object) null && MapManager.Instance.characterWindow.IsActive())
        MapManager.Instance.ShowDeck(characterIndex);
      if (AtOManager.Instance.CharInTown() && AtOManager.Instance.TownTutorialStep == 2)
        return;
      this.ShowItemsForBuy(this.currentItemsPageNum);
    }
    else if (this.craftType == 6)
    {
      this.InitCorruption();
      this.corruptAnim.SetTrigger("hide");
    }
    if (!((UnityEngine.Object) TownManager.Instance != (UnityEngine.Object) null))
      return;
    if (!GameManager.Instance.IsMultiplayer() || AtOManager.Instance.GetHero(characterIndex).Owner == NetworkManager.Instance.GetPlayerNick())
      TownManager.Instance.LastUsedCharacter = characterIndex;
    if (this.craftType != 2)
      return;
    if (AtOManager.Instance.GetTownTier() == 0 && (!GameManager.Instance.IsMultiplayer() || AtOManager.Instance.GetHero(this.heroIndex) != null && AtOManager.Instance.GetHero(this.heroIndex).Owner == NetworkManager.Instance.GetPlayerNick()))
      this.botSaveLoad.gameObject.SetActive(true);
    else
      this.botSaveLoad.gameObject.SetActive(false);
  }

  public void SetDiscount(int _discount)
  {
    this.discount = _discount;
    if (this.discount != 0)
    {
      this.discountText.gameObject.SetActive(true);
      StringBuilder stringBuilder = new StringBuilder();
      if (this.discount > 0)
      {
        this.discountText.color = Globals.Instance.ColorColor["greenCard"];
        stringBuilder.Append(Texts.Instance.GetText("shopDiscount"));
      }
      else
      {
        this.discountText.color = Globals.Instance.ColorColor["redCard"];
        stringBuilder.Append(Texts.Instance.GetText("shopIncrease"));
      }
      stringBuilder.Append("<br><size=+1>");
      stringBuilder.Append(Mathf.Abs(this.discount));
      stringBuilder.Append("%</size>");
      this.discountText.text = stringBuilder.ToString();
    }
    else
      this.discountText.gameObject.SetActive(false);
  }

  public void SetMaxCraftRarity(Enums.CardRarity _maxCraftRarity) => this.maxCraftRarity = _maxCraftRarity;

  public void SetMaxQuantity(int _maxQuantity = -1)
  {
    for (int heroIndex = 0; heroIndex < 4; ++heroIndex)
      AtOManager.Instance.SetCraftReaminingUses(heroIndex, _maxQuantity);
    this.ShowRemainingUses();
  }

  private void ShowRemainingUses()
  {
    int num = AtOManager.Instance.GetCraftReaminingUses(this.heroIndex);
    if (this.craftType == 6)
      num = 3 - this.corruptionTry[this.heroIndex];
    if (num > 0)
    {
      this.usesLeftT.gameObject.SetActive(true);
      this.usesLeftText.text = Texts.Instance.GetText("reaminingUses") + ": <color=#FFFFFF>" + num.ToString();
      this.buttonAffordableCraft.gameObject.SetActive(false);
    }
    else if (num == 0)
    {
      this.usesLeftT.gameObject.SetActive(true);
      this.usesLeftText.text = Texts.Instance.GetText("noMoreUses");
      this.buttonAffordableCraft.gameObject.SetActive(false);
    }
    else
      this.usesLeftT.gameObject.SetActive(false);
    this.usesLeftT.localPosition = this.craftType == 2 ? new Vector3(4.02f, -4.89f, 0.0f) : new Vector3(0.45f, 4f, 0.0f);
    this.remainingUses = num;
  }

  public bool CanBuy(string type, string cardId = "")
  {
    if (this.remainingUses == 0 || GameManager.Instance.IsMultiplayer() && AtOManager.Instance.GetHero(this.heroIndex) != null && AtOManager.Instance.GetHero(this.heroIndex).Owner != NetworkManager.Instance.GetPlayerNick())
      return false;
    cardId = cardId.Trim();
    int playerDust = AtOManager.Instance.GetPlayerDust();
    int playerGold = AtOManager.Instance.GetPlayerGold();
    bool flag = false;
    if (type == "A" && this.costA <= playerDust)
      flag = true;
    else if (type == "B" && this.costB <= playerDust)
      flag = true;
    else if (type == "Remove" && this.costRemove <= playerGold)
    {
      flag = true;
    }
    else
    {
      switch (type)
      {
        case "Craft":
          if (this.SetPrice("Craft", "", cardId, this.craftTierZone) <= playerDust)
          {
            flag = true;
            break;
          }
          break;
        case "Item":
          if (this.SetPrice("Item", Enum.GetName(typeof (Enums.CardRarity), (object) Globals.Instance.GetCardData(cardId, false).CardRarity), cardId, this.craftTierZone) <= playerGold)
          {
            flag = true;
            break;
          }
          break;
        case "Reroll":
          if (!AtOManager.Instance.IsTownRerollAvailable())
            return false;
          if (Globals.Instance.GetCostReroll() <= playerGold)
          {
            flag = true;
            break;
          }
          break;
        default:
          if (type == "Corruption" && this.SetPrice("Corruption", "") <= playerDust)
          {
            flag = true;
            break;
          }
          break;
      }
    }
    return flag;
  }

  public void RemoveCard()
  {
    if (!this.CanBuy("Remove"))
      return;
    this.craftCoroutine = this.StartCoroutine(this.BuyRemoveCo());
  }

  private IEnumerator BuyRemoveCo()
  {
    if (!this.blocked)
    {
      GameManager.Instance.PlayLibraryAudio("ui_anvil");
      this.SetBlocked(true);
      this.buttonRemove.gameObject.SetActive(false);
      this.cardActiveName = "";
      this.ShowElements("");
      this.CIRemove.SetDestinationScaleRotation(this.CIRemove.transform.localPosition, 0.0f, Quaternion.Euler(0.0f, 0.0f, 180f));
      this.CIRemove.Vanish();
      this.CIRemove.cardrevealed = true;
      int cardIndex = int.Parse(this.CIRemove.transform.gameObject.name.Split('_', StringSplitOptions.None)[2]);
      AtOManager.Instance.PayGold(this.costRemove);
      AtOManager.Instance.RemoveCardInDeck(this.heroIndex, cardIndex);
      AtOManager.Instance.SubstractCraftReaminingUses(this.heroIndex);
      this.ShowRemainingUses();
      this.removeAnim.SetTrigger("hide");
      this.oldcostRemoveText.text = "";
      if (this.costRemove > 0)
        AtOManager.Instance.HeroCraftRemoved(this.heroIndex);
      AtOManager.Instance.SideBarRefreshCards(this.heroIndex);
      this.SetControllerIntoVerticalList();
      yield return (object) Globals.Instance.WaitForSeconds(0.5f);
      if ((UnityEngine.Object) this.CIRemove != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.CIRemove.gameObject);
      this.CreateDeck(this.heroIndex);
      this.SetBlocked(false);
    }
  }

  public void BuyUpgrade(string type)
  {
    if (!this.CanBuy(type))
      return;
    this.craftCoroutine = this.StartCoroutine(this.BuyUpgradeCo(type));
  }

  private IEnumerator BuyUpgradeCo(string type)
  {
    CardCraftManager cardCraftManager = this;
    if (!cardCraftManager.blocked)
    {
      cardCraftManager.SetBlocked(true);
      cardCraftManager.cardActiveName = "";
      cardCraftManager.ShowElements("");
      cardCraftManager.CIPlain.SetDestinationScaleRotation(cardCraftManager.CIPlain.transform.localPosition, 0.0f, Quaternion.Euler(0.0f, 0.0f, 180f));
      cardCraftManager.CIPlain.Vanish();
      cardCraftManager.CIPlain.cardrevealed = true;
      CardItem CIselected = (CardItem) null;
      CardItem cardItem = (CardItem) null;
      switch (type)
      {
        case "A":
          CIselected = cardCraftManager.CIA;
          cardItem = cardCraftManager.CIB;
          cardCraftManager.costDust = cardCraftManager.costA;
          break;
        case "B":
          CIselected = cardCraftManager.CIB;
          cardItem = cardCraftManager.CIA;
          cardCraftManager.costDust = cardCraftManager.costB;
          break;
      }
      int cardIndex = int.Parse(CIselected.transform.gameObject.name.Split('_', StringSplitOptions.None)[2]);
      CardData cardDataAux = Functions.GetCardDataFromCardData(cardCraftManager.cardData, type);
      AtOManager.Instance.PayDust(cardCraftManager.costDust);
      AtOManager.Instance.ReplaceCardInDeck(cardCraftManager.heroIndex, cardIndex, cardDataAux.Id);
      AtOManager.Instance.SubstractCraftReaminingUses(cardCraftManager.heroIndex);
      if ((UnityEngine.Object) cardCraftManager.CIPlain != (UnityEngine.Object) null && (UnityEngine.Object) cardCraftManager.CIPlain.CardData != (UnityEngine.Object) null && cardCraftManager.CIPlain.CardData.Id == "faststrike" && AtOManager.Instance.TownTutorialStep == 1 && AtOManager.Instance.CharInTown())
      {
        AtOManager.Instance.IncreaseTownTutorialStep();
        AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(cardCraftManager.ExitCardCraftAlert);
        AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("tutorialTownUpgradeDone"));
      }
      cardCraftManager.deckEnergy.WriteEnergy(cardCraftManager.heroIndex, 0);
      cardCraftManager.ShowRemainingUses();
      cardCraftManager.SetControllerIntoVerticalList();
      cardCraftManager.transformAnim.SetTrigger("hide");
      cardCraftManager.oldcostAText.text = cardCraftManager.oldcostBText.text = "";
      if (cardCraftManager.costDust > 0)
        AtOManager.Instance.HeroCraftUpgraded(cardCraftManager.heroIndex);
      cardItem.SetDestinationScaleRotation(cardItem.transform.localPosition, 0.0f, Quaternion.Euler(0.0f, 0.0f, 180f));
      cardItem.Vanish();
      cardItem.cardrevealed = true;
      bool showParticles = true;
      if ((double) cardCraftManager.deckGOs[cardIndex].transform.position.y < -3.2000000476837158 || (double) cardCraftManager.deckGOs[cardIndex].transform.position.y > 3.7000000476837158)
        showParticles = false;
      if (showParticles)
      {
        yield return (object) Globals.Instance.WaitForSeconds(0.2f);
        CIselected.cardrevealed = true;
        CIselected.EnableTrail();
        CIselected.TopLayeringOrder("UI", 20000);
        CIselected.PlayDissolveParticle();
        cardCraftManager.SetBlocked(false);
        CIselected.SetDestinationScaleRotation(cardCraftManager.deckGOs[cardIndex].transform.position + new Vector3(-1.1f, -0.6f, 0.0f), 0.0f, Quaternion.Euler(0.0f, 0.0f, 180f));
        yield return (object) Globals.Instance.WaitForSeconds(0.4f);
      }
      else
      {
        CIselected.SetDestinationScaleRotation(CIselected.transform.localPosition, 0.0f, Quaternion.Euler(0.0f, 0.0f, 180f));
        CIselected.Vanish();
        CIselected.cardrevealed = true;
        yield return (object) Globals.Instance.WaitForSeconds(0.2f);
      }
      cardCraftManager.cardVerticalDeck[cardIndex].ReplaceWithCard(cardDataAux, type, showParticles);
      cardCraftManager.SetBlocked(false);
    }
  }

  public void BuyCraft(string cardId)
  {
    if (this.blocked || !this.CanBuy("Craft", cardId))
      return;
    this.SetBlocked(true);
    this.craftCoroutine = this.StartCoroutine(this.BuyCraftCo(cardId));
  }

  private IEnumerator BuyCraftCo(string cardId)
  {
    CardCraftManager cardCraftManager = this;
    cardId = cardId.Trim();
    GameManager.Instance.PlayLibraryAudio("ui_anvil");
    int costDust = cardCraftManager.SetPrice("Craft", "", cardId, cardCraftManager.craftTierZone);
    AtOManager.Instance.PayDust(costDust);
    AtOManager.Instance.SubstractCraftReaminingUses(cardCraftManager.heroIndex);
    cardCraftManager.ShowRemainingUses();
    if (!GameManager.Instance.IsMultiplayer())
    {
      AtOManager.Instance.AddCardToHero(cardCraftManager.heroIndex, cardId);
      AtOManager.Instance.SideBarRefreshCards(cardCraftManager.heroIndex);
      if (cardId == "fireball" && AtOManager.Instance.TownTutorialStep == 0 && AtOManager.Instance.CharInTown())
      {
        AtOManager.Instance.IncreaseTownTutorialStep();
        AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(cardCraftManager.ExitCardCraftAlert);
        AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("tutorialTownCraftDone"));
      }
    }
    else
    {
      int actualCards = AtOManager.Instance.GetHero(cardCraftManager.heroIndex).Cards.Count;
      AtOManager.Instance.AddCardToHeroMP(cardCraftManager.heroIndex, cardId);
      while (AtOManager.Instance.GetHero(cardCraftManager.heroIndex).Cards.Count == actualCards)
        yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    }
    string cardId1 = cardId;
    CardData cardData1 = Globals.Instance.GetCardData(cardId);
    if (cardData1.CardUpgraded != Enums.CardUpgraded.No && cardData1.UpgradedFrom != "")
      cardId1 = cardData1.UpgradedFrom.ToLower();
    AtOManager.Instance.SaveCraftedCard(cardCraftManager.heroIndex, cardId1);
    cardCraftManager.ShowListCardsForCraft(cardCraftManager.currentCraftPageNum);
    if (costDust > 0)
      AtOManager.Instance.HeroCraftCrafted(cardCraftManager.heroIndex);
    yield return (object) Globals.Instance.WaitForSeconds(0.05f);
    CardItem CI = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, GameManager.Instance.cameraMain.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0.0f, 2.5f, 0.0f), Quaternion.identity, cardCraftManager.tmpContainer).GetComponent<CardItem>();
    CI.SetCard(cardId, false, cardCraftManager.currentHero);
    CI.TopLayeringOrder("UI", -500);
    CI.DrawEnergyCost();
    CardData cardData2 = CI.CardData;
    CI.cardrevealed = true;
    CI.EnableTrail();
    CI.TopLayeringOrder("UI", 20000);
    CI.SetDestinationLocalScale(1.6f);
    yield return (object) Globals.Instance.WaitForSeconds(0.3f);
    cardCraftManager.CreateDeck(cardCraftManager.heroIndex);
    yield return (object) Globals.Instance.WaitForSeconds(0.2f);
    CI.SetDestinationLocalScale(0.0f);
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    UnityEngine.Object.Destroy((UnityEngine.Object) CI.gameObject);
    GameManager.Instance.GenerateParticleTrail(0, Vector3.zero, !(bool) (UnityEngine.Object) TownManager.Instance ? MapManager.Instance.sideCharacters.CharacterIconPosition(cardCraftManager.heroIndex) : TownManager.Instance.sideCharacters.CharacterIconPosition(cardCraftManager.heroIndex));
    yield return (object) Globals.Instance.WaitForSeconds(0.7f);
    cardCraftManager.SetBlocked(false);
  }

  private IEnumerator BuyItemCo(string cardId)
  {
    CardCraftManager cardCraftManager = this;
    AtOManager.Instance.PayGold(cardCraftManager.SetPrice("Item", "", cardId, cardCraftManager.craftTierZone));
    PlayerManager.Instance.PurchasedItem();
    if (!GameManager.Instance.IsMultiplayer())
    {
      AtOManager.Instance.AddItemToHero(cardCraftManager.heroIndex, cardId, cardCraftManager.itemListId);
      if (cardId == "spyglass" && AtOManager.Instance.TownTutorialStep == 2 && AtOManager.Instance.CharInTown())
      {
        AtOManager.Instance.IncreaseTownTutorialStep();
        AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(cardCraftManager.ExitCardCraftAlert);
        AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("tutorialTownLootDone"));
      }
    }
    else
      AtOManager.Instance.AddItemToHeroMP(cardCraftManager.heroIndex, cardId, cardCraftManager.itemListId);
    yield return (object) Globals.Instance.WaitForSeconds(0.05f);
    GameManager.Instance.PlayLibraryAudio("ui_equip_item");
    CardItem CI = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, GameManager.Instance.cameraMain.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0.0f, 2.5f, 0.0f), Quaternion.identity, cardCraftManager.tmpContainer).GetComponent<CardItem>();
    CI.SetCard(cardId, false, cardCraftManager.currentHero);
    CI.TopLayeringOrder("UI", -500);
    CI.DrawEnergyCost();
    CardData cardData = CI.CardData;
    CI.cardrevealed = true;
    CI.EnableTrail();
    CI.TopLayeringOrder("UI", 25000);
    CI.SetDestinationLocalScale(1.6f);
    yield return (object) Globals.Instance.WaitForSeconds(0.5f);
    CI.SetDestinationLocalScale(0.0f);
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    UnityEngine.Object.Destroy((UnityEngine.Object) CI.gameObject);
    string name = Enum.GetName(typeof (Enums.CardType), (object) cardData.CardType);
    GameManager.Instance.GenerateParticleTrail(0, Vector3.zero, cardCraftManager.transform.Find("backgroundItems/CharacterItems/" + name).transform.position);
    yield return (object) Globals.Instance.WaitForSeconds(0.2f);
    cardCraftManager.ShowCharacterItems();
    cardCraftManager.SetBlocked(false);
  }

  private void SetBlocked(bool _status)
  {
    this.blocked = _status;
    if (this.blockedCoroutine != null)
      this.StopCoroutine(this.blockedCoroutine);
    if (!this.blocked)
      return;
    this.blockedCoroutine = this.StartCoroutine(this.BlockedCo());
  }

  private IEnumerator BlockedCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(5f);
    this.blocked = false;
  }

  public void BuyItem(string cardId)
  {
    if (this.blocked)
      return;
    this.SetBlocked(true);
    if (this.CanBuy("Item", cardId))
    {
      if (cardId == "spyglass" && AtOManager.Instance.TownTutorialStep == 2 && AtOManager.Instance.CharInTown() && this.currentHero.SourceName != "Andrin")
      {
        AlertManager.Instance.MustSelect("Andrin");
        this.SetBlocked(false);
      }
      else
      {
        this.SetBlocked(true);
        this.craftCoroutine = this.StartCoroutine(this.BuyItemCo(cardId));
      }
    }
    else
      this.SetBlocked(false);
  }

  public string ButtonText(int cost)
  {
    if (cost == 0)
      return Texts.Instance.GetText("freeCost");
    StringBuilder stringBuilder = new StringBuilder();
    if (this.craftType == 0)
      stringBuilder.Append("<sprite name=dust> ");
    else if (this.craftType == 1)
      stringBuilder.Append("<sprite name=gold> ");
    else if (this.craftType == 2)
      stringBuilder.Append("<sprite name=dust> ");
    else if (this.craftType == 4)
      stringBuilder.Append("<sprite name=gold> ");
    else if (this.craftType == 6)
      stringBuilder.Append("<sprite name=dust> ");
    stringBuilder.Append(cost);
    return stringBuilder.ToString();
  }

  public void ShowElements(string direction, string cardId = "")
  {
    if (this.craftType == 0)
    {
      if (direction == "R")
      {
        this.arrowLR.gameObject.SetActive(true);
        this.buttonR.gameObject.SetActive(true);
        this.transformBText.gameObject.SetActive(true);
        this.BG_Right.SetText(this.ButtonText(this.costB));
      }
      else
      {
        this.arrowLR.gameObject.SetActive(false);
        this.buttonR.gameObject.SetActive(false);
        this.transformBText.gameObject.SetActive(false);
      }
      if (direction == "L")
      {
        this.arrowRL.gameObject.SetActive(true);
        this.buttonL.gameObject.SetActive(true);
        this.transformAText.gameObject.SetActive(true);
        this.BG_Left.SetText(this.ButtonText(this.costA));
      }
      else
      {
        this.arrowRL.gameObject.SetActive(false);
        this.buttonL.gameObject.SetActive(false);
        this.transformAText.gameObject.SetActive(false);
      }
      if (direction == "T")
      {
        this.arrowTR.gameObject.SetActive(true);
        this.arrowTL.gameObject.SetActive(true);
        this.buttonL.gameObject.SetActive(true);
        this.buttonR.gameObject.SetActive(true);
        this.upgradeAText.gameObject.SetActive(true);
        this.upgradeBText.gameObject.SetActive(true);
        this.BG_Left.SetText(this.ButtonText(this.costA));
        this.BG_Right.SetText(this.ButtonText(this.costB));
      }
      else
      {
        this.arrowTR.gameObject.SetActive(false);
        this.arrowTL.gameObject.SetActive(false);
        this.upgradeAText.gameObject.SetActive(false);
        this.upgradeBText.gameObject.SetActive(false);
      }
      if (!this.CanBuy("A"))
        this.BG_Left.Disable();
      else
        this.BG_Left.Enable();
      if (!this.CanBuy("B"))
        this.BG_Right.Disable();
      else
        this.BG_Right.Enable();
      if (AtOManager.Instance.TownTutorialStep != 1 || !((UnityEngine.Object) this.CIPlain != (UnityEngine.Object) null) || !((UnityEngine.Object) this.CIPlain.CardData != (UnityEngine.Object) null) || !(this.CIPlain.CardData.Id != "faststrike"))
        return;
      this.BG_Left.Disable();
      this.BG_Right.Disable();
    }
    else
    {
      if (this.craftType != 1)
        return;
      if (direction == "")
      {
        this.buttonRemove.gameObject.SetActive(false);
        this.transformRemoveText.gameObject.SetActive(false);
      }
      else
      {
        this.buttonRemove.gameObject.SetActive(true);
        this.transformRemoveText.gameObject.SetActive(true);
        bool flag = true;
        if (this.currentHero.GetTotalCardsInDeck() <= 15)
          flag = false;
        else if (this.currentHero.GetTotalCardsInDeck(true) <= 15)
        {
          CardData cardData = Globals.Instance.GetCardData(cardId, false);
          if (cardData.CardClass != Enums.CardClass.Injury && cardData.CardClass != Enums.CardClass.Boon)
            flag = false;
        }
        if (flag || AtOManager.Instance.Sandbox_noMinimumDecksize)
        {
          if (this.CanBuy("Remove"))
            this.BG_Remove.Enable();
          else
            this.BG_Remove.Disable();
        }
        else
          this.BG_Remove.Disable();
      }
    }
  }

  private int SetPrice(
    string function,
    string rarity,
    string cardName = "",
    int zoneTier = 0,
    bool useShopDiscount = true)
  {
    int num1 = 0;
    if (this.isPetShop)
    {
      if (cardName != "")
        rarity = Enum.GetName(typeof (Enums.CardRarity), (object) Globals.Instance.GetCardData(cardName).CardRarity);
      if (rarity.ToLower() == "common")
        num1 = 72;
      else if (rarity.ToLower() == "uncommon")
        num1 = 156;
      else if (rarity.ToLower() == "rare")
        num1 = 348;
      else if (rarity.ToLower() == "epic")
        num1 = 744;
      else if (rarity.ToLower() == "mythic")
        num1 = 1200;
    }
    else
    {
      switch (function)
      {
        case "Remove":
          num1 = Globals.Instance.GetRemoveCost(this.cardData.CardType, this.cardData.CardRarity);
          if (this.cardData.CardType == Enums.CardType.Injury)
          {
            if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_3_4"))
            {
              num1 -= Functions.FuncRoundToInt((float) num1 * 0.3f);
              break;
            }
            if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_3_2"))
            {
              num1 -= Functions.FuncRoundToInt((float) num1 * 0.15f);
              break;
            }
            break;
          }
          if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_3_5"))
            num1 -= Functions.FuncRoundToInt((float) num1 * 0.5f);
          else if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_3_3"))
            num1 -= Functions.FuncRoundToInt((float) num1 * 0.25f);
          if (AtOManager.Instance.CharInTown())
          {
            if (AtOManager.Instance.GetTownTier() == 1)
            {
              if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_3_6"))
              {
                num1 = 0;
                break;
              }
              break;
            }
            if (AtOManager.Instance.GetTownTier() == 0 && PlayerManager.Instance.PlayerHaveSupply("townUpgrade_3_1"))
            {
              num1 = 0;
              break;
            }
            break;
          }
          break;
        case "Upgrade":
        case "Transform":
          num1 = Globals.Instance.GetUpgradeCost(function, rarity);
          if (function == "Upgrade")
          {
            if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_2_6"))
              num1 -= Functions.FuncRoundToInt((float) num1 * 0.5f);
            else if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_2_4"))
              num1 -= Functions.FuncRoundToInt((float) num1 * 0.3f);
            else if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_2_2"))
              num1 -= Functions.FuncRoundToInt((float) num1 * 0.15f);
          }
          if (function == "Transform")
          {
            if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_2_5"))
            {
              num1 = 0;
              break;
            }
            if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_2_3"))
            {
              num1 -= Functions.FuncRoundToInt((float) num1 * 0.5f);
              break;
            }
            if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_2_1"))
            {
              num1 -= Functions.FuncRoundToInt((float) num1 * 0.25f);
              break;
            }
            break;
          }
          break;
        case "Craft":
          float discountCraft = 0.0f;
          if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_1_5"))
            discountCraft = 0.3f;
          else if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_1_2"))
            discountCraft = 0.15f;
          float discountUpgrade = 0.0f;
          if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_2_6"))
            discountUpgrade = 0.5f;
          else if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_2_4"))
            discountUpgrade = 0.3f;
          else if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_2_2"))
            discountUpgrade = 0.15f;
          num1 = Globals.Instance.GetCraftCost(cardName, discountCraft, discountUpgrade);
          break;
        case "Item":
          num1 = Globals.Instance.GetItemCost(cardName);
          if (AtOManager.Instance.CharInTown())
          {
            if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_5_5"))
            {
              num1 -= Functions.FuncRoundToInt((float) num1 * 0.3f);
              break;
            }
            if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_5_2"))
            {
              num1 -= Functions.FuncRoundToInt((float) num1 * 0.15f);
              break;
            }
            break;
          }
          break;
        case "Corruption":
          num1 = 300;
          break;
      }
    }
    if (num1 > 0)
    {
      int num2 = 0;
      for (int index = 0; index < 4; ++index)
      {
        if (AtOManager.Instance.GetHero(index) != null)
          num2 += AtOManager.Instance.GetHero(index).GetItemDiscountModification();
      }
      int num3 = num2;
      if (useShopDiscount)
        num3 += this.discount;
      if (num3 != 0)
      {
        num1 -= Functions.FuncRoundToInt((float) (num1 * num3 / 100));
        if (num1 < 0)
          num1 = 0;
      }
    }
    if (function == "Corruption" && this.IsCorruptionEnabled(0))
      num1 += 400;
    if (function == "Craft" && AtOManager.Instance.Sandbox_cardCraftPrice != 0)
      num1 += Functions.FuncRoundToInt((float) ((double) num1 * (double) AtOManager.Instance.Sandbox_cardCraftPrice * 0.0099999997764825821));
    else if (function == "Upgrade" && AtOManager.Instance.Sandbox_cardUpgradePrice != 0)
      num1 += Functions.FuncRoundToInt((float) ((double) num1 * (double) AtOManager.Instance.Sandbox_cardUpgradePrice * 0.0099999997764825821));
    else if (function == "Transform" && AtOManager.Instance.Sandbox_cardTransformPrice != 0)
      num1 += Functions.FuncRoundToInt((float) ((double) num1 * (double) AtOManager.Instance.Sandbox_cardTransformPrice * 0.0099999997764825821));
    else if (function == "Remove" && AtOManager.Instance.Sandbox_cardRemovePrice != 0)
      num1 += Functions.FuncRoundToInt((float) ((double) num1 * (double) AtOManager.Instance.Sandbox_cardRemovePrice * 0.0099999997764825821));
    else if (function == "Item" && AtOManager.Instance.Sandbox_itemsPrice != 0)
      num1 += Functions.FuncRoundToInt((float) ((double) num1 * (double) AtOManager.Instance.Sandbox_itemsPrice * 0.0099999997764825821));
    else if (this.isPetShop && AtOManager.Instance.Sandbox_petsPrice != 0)
      num1 += Functions.FuncRoundToInt((float) ((double) num1 * (double) AtOManager.Instance.Sandbox_petsPrice * 0.0099999997764825821));
    return num1;
  }

  public void RefreshCardPrices()
  {
    if (this.craftType == 0 || this.craftType == 1)
    {
      if (this.blocked || !(this.cardActiveName != ""))
        return;
      this.SelectCard(this.cardActiveName);
    }
    else if (this.craftType == 2)
    {
      this.RefreshCraftButtonsPrices();
      if (this.cardCraftSave.gameObject.activeSelf || !AtOManager.Instance.affordableCraft)
        return;
      this.ShowListCardsForCraft(this.currentCraftPageNum);
    }
    else if (this.craftType == 3)
      this.RefreshDivinationButtonsPrices();
    else if (this.craftType == 4)
    {
      this.RefreshCraftButtonsPrices();
    }
    else
    {
      if (this.craftType != 6)
        return;
      this.RefreshCraftButtonsPrices();
    }
  }

  public void SelectCard(string cardName)
  {
    if (this.remainingUses == 0)
      return;
    this.cardActiveName = cardName;
    this.RemoveUpgradeCards();
    this.RemoveRemoveCards();
    this.RemoveCorruptionCards();
    string[] strArray = cardName.Split('_', StringSplitOptions.None);
    string str1 = strArray[0];
    string cardIndex = strArray[1];
    this.cardData = Globals.Instance.GetCardData(str1, false);
    this.CreateCard(str1, cardIndex, true);
    if (this.craftType == 0)
    {
      this.oldcostAText.text = this.oldcostBText.text = "";
      if (this.cardData.CardUpgraded == Enums.CardUpgraded.No)
      {
        this.CreateCard(this.cardData.UpgradesTo1, cardIndex, false);
        this.CreateCard(this.cardData.UpgradesTo2, cardIndex, false);
        string name1 = Enum.GetName(typeof (Enums.CardRarity), (object) Globals.Instance.GetCardData(this.cardData.UpgradesTo1, false).CardRarity);
        this.costA = this.SetPrice("Upgrade", name1);
        int num = this.SetPrice("Upgrade", name1, useShopDiscount: false);
        if (num != this.costA)
          this.oldcostAText.text = string.Format(Texts.Instance.GetText("oldCost"), (object) num.ToString());
        string name2 = Enum.GetName(typeof (Enums.CardRarity), (object) Globals.Instance.GetCardData(this.cardData.UpgradesTo2, false).CardRarity);
        this.costB = this.SetPrice("Upgrade", name2);
        num = this.SetPrice("Upgrade", name2, useShopDiscount: false);
        if (num != this.costB)
          this.oldcostBText.text = string.Format(Texts.Instance.GetText("oldCost"), (object) num.ToString());
        this.ShowElements("T");
      }
      else if (this.cardData.CardUpgraded == Enums.CardUpgraded.A)
      {
        string str2 = str1.Remove(str1.Length - 1, 1) + "B";
        this.CreateCard(this.cardData.UpgradedFrom, cardIndex, false);
        this.CreateCard(str2, cardIndex, false);
        this.costA = 1000000;
        string name = Enum.GetName(typeof (Enums.CardRarity), (object) Globals.Instance.GetCardData(str2, false).CardRarity);
        this.costB = this.SetPrice("Transform", name);
        int num = this.SetPrice("Transform", name, useShopDiscount: false);
        if (num != this.costB)
          this.oldcostBText.text = string.Format(Texts.Instance.GetText("oldCost"), (object) num.ToString());
        this.ShowElements("R");
      }
      else
      {
        if (this.cardData.CardUpgraded != Enums.CardUpgraded.B)
          return;
        string str3 = str1.Remove(str1.Length - 1, 1) + "A";
        this.CreateCard(this.cardData.UpgradedFrom, cardIndex, false);
        this.CreateCard(str3, cardIndex, false);
        string name = Enum.GetName(typeof (Enums.CardRarity), (object) Globals.Instance.GetCardData(str3, false).CardRarity);
        this.costA = this.SetPrice("Transform", name);
        int num = this.SetPrice("Transform", name, useShopDiscount: false);
        if (num != this.costA)
          this.oldcostAText.text = string.Format(Texts.Instance.GetText("oldCost"), (object) num.ToString());
        this.costB = 1000000;
        this.ShowElements("L");
      }
    }
    else if (this.craftType == 1)
    {
      this.oldcostRemoveText.text = "";
      this.costRemove = this.SetPrice("Remove", "");
      this.BG_Remove.SetText(this.ButtonText(this.costRemove));
      int num = this.SetPrice("Remove", "", useShopDiscount: false);
      if (num != this.costRemove)
        this.oldcostRemoveText.text = string.Format(Texts.Instance.GetText("oldCost"), (object) num.ToString());
      this.ShowElements("Remove", str1);
    }
    else
    {
      if (this.craftType != 6)
        return;
      this.oldcostRemoveText.text = "";
      this.costCorruption = this.SetPrice("Corruption", "");
      this.BG_Corruption.SetText(this.ButtonText(this.costCorruption));
      int num = this.SetPrice("Corruption", "", useShopDiscount: false);
      if (num != this.costCorruption)
        this.oldcostRemoveText.text = string.Format(Texts.Instance.GetText("oldCost"), (object) num.ToString());
      this.ActivateButtonCorruption();
    }
  }

  public void SetActiveCard(CardVertical cv) => this.cardActive = cv;

  public void ClearActiveCard()
  {
    if ((UnityEngine.Object) this.cardActive != (UnityEngine.Object) null)
      this.cardActive.ClearActive();
    this.cardActive = (CardVertical) null;
  }

  private void RemoveUpgradeCards()
  {
    foreach (Component component in this.cardUpgradeContainer)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
    this.CIPlain = (CardItem) null;
    this.CIA = (CardItem) null;
    this.CIB = (CardItem) null;
  }

  private void RemoveRemoveCards()
  {
    foreach (Component component in this.cardRemoveContainer)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
    this.CIRemove = (CardItem) null;
  }

  private void RemoveCraftCards()
  {
    foreach (Component component in this.cardCraftContainer)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
    this.CIRemove = (CardItem) null;
  }

  private void RemoveCorruptionCards()
  {
    foreach (Component component in this.cardCorruptionContainer)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
    this.CICorruption = (CardItem) null;
    this.corruptionPercentRoll.text = "";
    this.corruptionPercentRollSuccess.gameObject.SetActive(false);
    this.corruptionPercentRollFail.gameObject.SetActive(false);
  }

  public void CreateCard(string cardId, string cardIndex, bool isYours)
  {
    Transform parent = (Transform) null;
    if (this.craftType == 0)
      parent = this.cardUpgradeContainer;
    else if (this.craftType == 1)
      parent = this.cardRemoveContainer;
    else if (this.craftType == 6)
      parent = this.cardCorruptionContainer;
    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, parent);
    CardItem component = gameObject.GetComponent<CardItem>();
    if (this.craftType == 6)
    {
      CardData dataFromCardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(cardId, false), "RARE");
      if ((UnityEngine.Object) dataFromCardData != (UnityEngine.Object) null)
      {
        component.SetCard(dataFromCardData.Id, false, this.currentHero);
      }
      else
      {
        this.RemoveCorruptionCards();
        this.DeactivateCorruptions();
        return;
      }
    }
    else
      component.SetCard(cardId, false, this.currentHero);
    component.TopLayeringOrder("UI", -500);
    component.DrawEnergyCost();
    CardData cardData = component.CardData;
    if (this.craftType == 0)
    {
      component.SetLocalScale(new Vector3(1.2f, 1.2f, 1f));
      if (cardData.CardUpgraded == Enums.CardUpgraded.No)
      {
        gameObject.name = "TMP_Plain_" + cardIndex;
        component.SetLocalPosition(this.posPlain);
        this.CIPlain = component;
      }
      else if (cardData.CardUpgraded == Enums.CardUpgraded.A)
      {
        gameObject.name = "TMP_A_" + cardIndex;
        component.SetLocalPosition(this.posUpgradeA);
        component.ShowDifferences(Globals.Instance.GetCardData(cardData.UpgradedFrom, false));
        this.CIA = component;
      }
      else if (cardData.CardUpgraded == Enums.CardUpgraded.B)
      {
        gameObject.name = "TMP_B_" + cardIndex;
        component.SetLocalPosition(this.posUpgradeB);
        component.ShowDifferences(Globals.Instance.GetCardData(cardData.UpgradedFrom, false));
        this.CIB = component;
      }
      int num = isYours ? 1 : 0;
      this.transformAnim.SetTrigger("fade");
    }
    else if (this.craftType == 1)
    {
      gameObject.name = "TMP_Plain_" + cardIndex;
      this.CIRemove = component;
      component.SetLocalPosition(this.posRemove);
      this.removeAnim.SetTrigger("fade");
    }
    else if (this.craftType == 6)
    {
      gameObject.name = "TMP_Plain_" + cardIndex;
      this.CICorruption = component;
      component.SetLocalPosition(this.posRemove);
      this.corruptAnim.SetTrigger("fade");
    }
    component.cardforaddcard = true;
    component.cardoutsidereward = true;
    component.CreateColliderAdjusted();
    component.DisableTrail();
  }

  private void ShowCharacterItems()
  {
    if (this.currentHero == null || (UnityEngine.Object) this.currentHero.HeroData == (UnityEngine.Object) null)
      return;
    this.iconWeapon.ShowIconExternal("weapon", (Character) this.currentHero);
    this.iconArmor.ShowIconExternal("armor", (Character) this.currentHero);
    this.iconJewelry.ShowIconExternal("jewelry", (Character) this.currentHero);
    this.iconAccesory.ShowIconExternal("accesory", (Character) this.currentHero);
    this.iconPet.ShowIconExternal("pet", (Character) this.currentHero);
    this.itemsOwner.text = string.Format(Texts.Instance.GetText("heroEquipment"), (object) this.currentHero.SourceName);
    for (int index = 0; index < 4; ++index)
    {
      Hero hero = AtOManager.Instance.GetHero(index);
      if (hero.Id == this.currentHero.Id)
      {
        this.iconDeck.auxInt = index;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(Texts.Instance.GetText("deck"));
        stringBuilder.Append("\n<color=#bbb><size=-.5>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("cardsNum"), (object) hero.Cards.Count));
        this.iconDeckText.text = stringBuilder.ToString();
        break;
      }
    }
  }

  public void HoverItem(bool state, Enums.CardType cardType)
  {
    switch (cardType)
    {
      case Enums.CardType.Weapon:
        if (!((UnityEngine.Object) this.iconWeapon != (UnityEngine.Object) null))
          break;
        this.iconWeapon.DoHover(state);
        break;
      case Enums.CardType.Armor:
        if (!((UnityEngine.Object) this.iconArmor != (UnityEngine.Object) null))
          break;
        this.iconArmor.DoHover(state);
        break;
      case Enums.CardType.Jewelry:
        if (!((UnityEngine.Object) this.iconJewelry != (UnityEngine.Object) null))
          break;
        this.iconJewelry.DoHover(state);
        break;
      case Enums.CardType.Accesory:
        if (!((UnityEngine.Object) this.iconAccesory != (UnityEngine.Object) null))
          break;
        this.iconAccesory.DoHover(state);
        break;
      case Enums.CardType.Pet:
        if (!((UnityEngine.Object) this.iconPet != (UnityEngine.Object) null))
          break;
        this.iconPet.DoHover(state);
        break;
    }
  }

  private void ClearItemListContainer()
  {
    foreach (Component component in this.cardItemContainer)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
  }

  public void SetItemList(string _itemListId)
  {
    if (_itemListId != "")
      this.itemListId = _itemListId.ToLower();
    else if (this.isPetShop)
      this.itemListId = "petShop";
    else
      this.itemListId = AtOManager.Instance.GetTownItemListId();
  }

  public void DoPetShop()
  {
    this.craftCardItemDict = new Dictionary<int, CardCraftItem>();
    this.ClearItemListContainer();
    this.isPetShop = true;
    this.SetItemList("");
    this.ShowItemsForBuy();
  }

  public void DoItemShop()
  {
    this.craftCardItemDict = new Dictionary<int, CardCraftItem>();
    this.ClearItemListContainer();
    this.isPetShop = false;
    this.SetItemList("");
    this.ShowItemsForBuy();
  }

  public void RerollItems()
  {
    if (!this.CanBuy("Reroll"))
      return;
    AtOManager.Instance.PayGold(Globals.Instance.GetCostReroll(), isReroll: true);
    AtOManager.Instance.DoTownReroll();
    this.DoItemShop();
  }

  public void ShowItemsForBuy(int pageNum = 1, string itemBought = "")
  {
    this.SetBlocked(false);
    List<string> stringList = new List<string>();
    bool flag = true;
    this.maxPetNumber.gameObject.SetActive(false);
    if ((bool) (UnityEngine.Object) TownManager.Instance)
    {
      if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_6_2"))
      {
        this.petShopButton.gameObject.SetActive(true);
        this.itemShopButton.gameObject.SetActive(true);
      }
      else
      {
        this.petShopButton.gameObject.SetActive(false);
        this.itemShopButton.gameObject.SetActive(false);
      }
    }
    else
    {
      this.petShopButton.gameObject.SetActive(false);
      this.itemShopButton.gameObject.SetActive(false);
    }
    this.rerollButton.gameObject.SetActive(false);
    this.rerollButtonLock.gameObject.SetActive(false);
    this.rerollButtonWarning.gameObject.SetActive(false);
    List<string> itemList;
    if (!this.isPetShop)
    {
      if (AtOManager.Instance.CharInTown() && PlayerManager.Instance.PlayerHaveSupply("townUpgrade_5_1"))
      {
        this.rerollButton.gameObject.SetActive(true);
        int costReroll = Globals.Instance.GetCostReroll();
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<br><size=-.3><sprite name=gold> ");
        stringBuilder.Append(costReroll);
        this.rerollButton.GetComponent<BotonGeneric>().SetText(string.Format(Texts.Instance.GetText("rerollButton"), (object) stringBuilder.ToString()));
        if (!GameManager.Instance.IsMultiplayer())
          this.rerollButton.GetComponent<BotonGeneric>().SetPopupId("rerollButtonCostSP");
        else
          this.rerollButton.GetComponent<BotonGeneric>().SetPopupId("rerollButtonCostCoop");
        if (AtOManager.Instance.IsTownRerollLimited())
          this.rerollButtonWarning.gameObject.SetActive(true);
        if (AtOManager.Instance.GetPlayerGold() < costReroll || !AtOManager.Instance.IsTownRerollAvailable())
        {
          this.rerollButton.GetComponent<BotonGeneric>().Disable();
          if (!AtOManager.Instance.IsTownRerollAvailable())
          {
            this.rerollButtonLock.gameObject.SetActive(true);
            this.rerollButtonWarning.gameObject.SetActive(false);
          }
        }
        else
          this.rerollButton.GetComponent<BotonGeneric>().Enable();
      }
      if (this.itemShopButton.gameObject.activeSelf)
      {
        this.itemShopButton.GetComponent<BotonGeneric>().Disable();
        this.itemShopButton.GetComponent<BotonGeneric>().ShowBackground(true);
        this.petShopButton.GetComponent<BotonGeneric>().Enable();
        this.petShopButton.GetComponent<BotonGeneric>().ShowBackground(false);
      }
      itemList = AtOManager.Instance.GetItemList(this.itemListId);
    }
    else
    {
      if (this.petShopButton.gameObject.activeSelf)
      {
        this.petShopButton.GetComponent<BotonGeneric>().Disable();
        this.petShopButton.GetComponent<BotonGeneric>().ShowBackground(true);
        this.itemShopButton.GetComponent<BotonGeneric>().Enable();
        this.itemShopButton.GetComponent<BotonGeneric>().ShowBackground(false);
      }
      itemList = Globals.Instance.CardItemByType[Enums.CardType.Pet];
    }
    if (itemList == null)
    {
      pageNum = this.currentItemsPageNum;
    }
    else
    {
      int num1 = 0;
      float num2 = 4f;
      float num3 = num2 * 2f;
      int total = Mathf.CeilToInt((float) itemList.Count / num3);
      if (pageNum < 1 || pageNum > total)
      {
        pageNum = this.currentItemsPageNum;
      }
      else
      {
        this.currentItemsPageNum = pageNum;
        this.ClearCraftPages();
        int num4 = 0;
        for (int index = 0; index < itemList.Count; ++index)
        {
          if ((double) num4 >= (double) (pageNum - 1) * (double) num3 && (double) num4 < (double) pageNum * (double) num3)
          {
            CardCraftItem component;
            CardData cardData;
            if (!this.craftCardItemDict.ContainsKey(num1))
            {
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cardCraftItem, new Vector3(0.0f, 0.0f, -3f), Quaternion.identity, this.cardItemContainer);
              component = gameObject.transform.GetComponent<CardCraftItem>();
              gameObject.name = itemList[index];
              this.craftCardItemDict.Add(num1, component);
              int num5 = Mathf.FloorToInt((float) num1 / num2);
              float x = (float) ((double) num1 % (double) num2 * 2.5999999046325684 - 1.7000000476837158);
              float y = (float) (2.6500000953674316 - 4.1999998092651367 * (double) num5);
              component.SetPosition(new Vector3(x, y, 0.0f));
              component.SetIndex(num1);
              component.SetHero(this.currentHero);
              component.SetGenericCard(true);
              cardData = Globals.Instance.GetCardData(itemList[index], false);
              int cost = this.SetPrice("Item", Enum.GetName(typeof (Enums.CardRarity), (object) cardData.CardRarity), itemList[index], this.craftTierZone);
              component.SetButtonTextItem(this.ButtonText(cost));
              component.SetCard(itemList[index], this.itemListId, this.currentHero);
            }
            else
            {
              component = this.craftCardItemDict[num1];
              component.gameObject.name = itemList[index];
              component.gameObject.SetActive(true);
              cardData = Globals.Instance.GetCardData(itemList[index], false);
              int cost = this.SetPrice("Item", Enum.GetName(typeof (Enums.CardRarity), (object) cardData.CardRarity), itemList[index], this.craftTierZone);
              component.SetButtonTextItem(this.ButtonText(cost));
              component.SetCard(itemList[index], this.itemListId, this.currentHero);
            }
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            {
              string key = this.itemListId + component.cardId;
              if (AtOManager.Instance.boughtItemInShopByWho != null && AtOManager.Instance.boughtItemInShopByWho.ContainsKey(key))
              {
                this.ShowPortraitItemBought(AtOManager.Instance.boughtItemInShopByWho[key], _CCI: component);
                component.EnableButton(false);
              }
              else
              {
                Transform transform = component.transform.GetChild(1).transform.GetChild(0).transform.Find("itemBuyer");
                if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
                  UnityEngine.Object.Destroy((UnityEngine.Object) transform.gameObject);
              }
            }
            if (!this.isPetShop)
            {
              if (!PlayerManager.Instance.IsCardUnlocked(itemList[index]))
              {
                PlayerManager.Instance.CardUnlock(itemList[index]);
                if ((UnityEngine.Object) component != (UnityEngine.Object) null)
                  component.GetCI().ShowUnlocked(false);
              }
            }
            else if ((!flag || !PlayerManager.Instance.IsCardUnlocked(itemList[index])) && (UnityEngine.Object) component != (UnityEngine.Object) null)
              component.EnableButton(false);
            if ((UnityEngine.Object) component != (UnityEngine.Object) null && component.enabled)
            {
              if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && cardData.CardType == Enums.CardType.Pet && cardData.Sku != "" && !SteamManager.Instance.PlayerHaveDLC(cardData.Sku))
              {
                component.EnableButton(false);
                string _text = string.Format(Texts.Instance.GetText("requiredDLC"), (object) SteamManager.Instance.GetDLCName(cardData.Sku));
                component.ShowLock(true, _text);
              }
              else
                component.ShowLock(false);
            }
            ++num1;
          }
          ++num4;
        }
        if (total <= 1)
          return;
        if ((double) num1 < (double) num2 * 2.0)
        {
          for (int key = num1; (double) key < (double) num2 * 2.0; ++key)
          {
            if (this.craftCardItemDict.ContainsKey(key))
              this.craftCardItemDict[key].transform.gameObject.SetActive(false);
          }
        }
        this.CreateCraftPages(pageNum, total);
      }
    }
  }

  public void ShowPortraitItemBought(int _heroIndex, string _itemId = "", CardCraftItem _CCI = null)
  {
    CardCraftItem cardCraftItem = _CCI;
    if ((UnityEngine.Object) cardCraftItem == (UnityEngine.Object) null)
      cardCraftItem = this.craftCardItemDict[0];
    if (!((UnityEngine.Object) cardCraftItem != (UnityEngine.Object) null))
      return;
    Transform transform1 = cardCraftItem.transform.GetChild(1).transform.GetChild(0).transform;
    Transform transform2 = transform1.Find("itemBuyer");
    if ((UnityEngine.Object) transform2 != (UnityEngine.Object) null)
    {
      transform2.transform.GetComponent<SpriteRenderer>().sprite = AtOManager.Instance.GetHero(_heroIndex).HeroData.HeroSubClass.SpriteSpeed;
    }
    else
    {
      SpriteRenderer component1 = transform1.Find("Disable").GetComponent<SpriteRenderer>();
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.portraitItem, Vector3.zero, Quaternion.identity, transform1);
      gameObject.name = "itemBuyer";
      gameObject.transform.gameObject.SetActive(true);
      SpriteRenderer component2 = gameObject.transform.GetComponent<SpriteRenderer>();
      component2.sprite = AtOManager.Instance.GetHero(_heroIndex).HeroData.HeroSubClass.SpriteSpeed;
      component2.sortingOrder = component1.sortingOrder + 1;
      component2.sortingLayerName = component1.sortingLayerName;
      gameObject.transform.localPosition = new Vector3(0.65f, 0.75f, 0.0f);
    }
  }

  private void RefreshCraftButtonsPrices()
  {
    for (int key = 0; key < this.craftCardItemDict.Count; ++key)
    {
      CardCraftItem cardCraftItem = this.craftCardItemDict[key];
      if (this.craftType == 2)
        cardCraftItem.SetAvailability();
      else if (this.craftType < 3)
        cardCraftItem.EnableButton(this.CanBuy("Craft", cardCraftItem.cardId));
      else if (this.craftType == 4 && !cardCraftItem.lockIcon.gameObject.activeSelf)
        cardCraftItem.SetAvailability();
    }
    if (this.craftType == 6)
      this.ActivateButtonCorruption();
    if (!this.rerollButton.gameObject.activeSelf)
      return;
    if (AtOManager.Instance.GetPlayerGold() < Globals.Instance.GetCostReroll())
      this.rerollButton.GetComponent<BotonGeneric>().Disable();
    else
      this.rerollButton.GetComponent<BotonGeneric>().Enable();
  }

  public void ClearCraftPages()
  {
    Transform craftPageContainer = this.cardCraftPageContainer;
    if (this.craftType == 4)
      craftPageContainer = this.itemsCraftPageContainer;
    foreach (Component component in craftPageContainer)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
  }

  public void ClearCardListContainer()
  {
    foreach (Component component in this.cardListContainer)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
  }

  public void ClearTmpContainer()
  {
    foreach (Component component in this.tmpContainer)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
  }

  private void SetDivinationButtons()
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    int num = 0;
    int playerGold = AtOManager.Instance.GetPlayerGold();
    for (int index = 0; index < 5; ++index)
    {
      if (AtOManager.Instance.GetTownTier() == 0)
      {
        if (index > 2 || index == 1 && !PlayerManager.Instance.PlayerHaveSupply("townUpgrade_4_2") || index == 2 && !PlayerManager.Instance.PlayerHaveSupply("townUpgrade_4_6"))
          continue;
      }
      else if (AtOManager.Instance.GetTownTier() == 1)
      {
        if (index > 2 || index == 2 && !PlayerManager.Instance.PlayerHaveSupply("townUpgrade_4_4"))
          continue;
      }
      else if (AtOManager.Instance.GetTownTier() == 2)
      {
        if (index == 0 || index == 4)
          continue;
      }
      else if (AtOManager.Instance.GetTownTier() >= 2 && (index == 0 || index == 1))
        continue;
      stringBuilder2.Clear();
      stringBuilder2.Append(index);
      int divinationCost = Globals.Instance.GetDivinationCost(stringBuilder2.ToString());
      if (divinationCost > -1)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cardDivinationButton, new Vector3((float) (4.0 * (double) num - 2.0), 0.0f, 0.0f), Quaternion.identity, this.divinationButtonContainer);
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, 0.0f, -1f);
        gameObject.name = "CraftDivination";
        BotonGeneric component = gameObject.transform.GetComponent<BotonGeneric>();
        component.auxString = divinationCost.ToString();
        component.auxInt = index;
        switch (index)
        {
          case 0:
            component.color = new Color(0.6f, 0.6f, 0.6f, 1f);
            break;
          case 1:
            component.color = Globals.Instance.RarityColor["uncommon"];
            break;
          case 2:
            component.color = Globals.Instance.RarityColor["rare"];
            break;
          case 3:
            component.color = Globals.Instance.RarityColor["epic"];
            break;
          case 4:
            component.color = Globals.Instance.RarityColor["mythic"];
            break;
        }
        component.SetColor();
        stringBuilder1.Clear();
        stringBuilder1.Append("<size=4.5><sprite name=gold></size><size=5.5><color=#FFAA00> ");
        if (divinationCost > 0)
          stringBuilder1.Append(divinationCost.ToString());
        else
          stringBuilder1.Append(Texts.Instance.GetText("freeCost"));
        stringBuilder1.Append("</color></size><br>");
        stringBuilder1.Append(Texts.Instance.GetText("divinationT" + index.ToString()));
        component.SetText(stringBuilder1.ToString());
        if (playerGold < divinationCost)
          component.Disable();
        ++num;
      }
    }
    this.divinationButtonContainer.localPosition = new Vector3(-0.5f - (float) ((num - 2) * 2), -2.6f, this.divinationButtonContainer.localPosition.z);
  }

  private void RefreshDivinationButtonsPrices()
  {
    int playerGold = AtOManager.Instance.GetPlayerGold();
    foreach (Component component1 in this.divinationButtonContainer)
    {
      BotonGeneric component2 = component1.transform.GetComponent<BotonGeneric>();
      if (playerGold < int.Parse(component2.auxString))
        component2.Disable();
      else
        component2.Enable();
    }
  }

  public void BuyDivination(int divinationType)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(divinationType);
    int divinationCost = Globals.Instance.GetDivinationCost(stringBuilder.ToString());
    int playerGold = AtOManager.Instance.GetPlayerGold();
    int divinationTier = Globals.Instance.GetDivinationTier(divinationType);
    int num = divinationCost;
    if (playerGold >= num)
    {
      if (!GameManager.Instance.IsMultiplayer())
      {
        AtOManager.Instance.PayGold(divinationCost);
        AtOManager.Instance.SetTownDivinationTier(divinationTier);
        AtOManager.Instance.LaunchRewards();
      }
      else if ((UnityEngine.Object) AtOManager.Instance.GetTownDivinationTier() != (UnityEngine.Object) null)
      {
        AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.SetDivinationWaitState);
        AlertManager.Instance.AlertConfirm(Texts.Instance.GetText("divinationRoundExists"));
      }
      else
      {
        AtOManager.Instance.SetTownDivinationTier(divinationTier, NetworkManager.Instance.GetPlayerNick(), divinationCost);
        this.SetDivinationWaiting();
      }
    }
  }

  private void SetDivinationWaitState()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.SetDivinationWaitState);
    this.SetDivinationWaiting();
  }

  private void SetDivinationWaiting()
  {
    this.divinationButtonContainer.gameObject.SetActive(false);
    this.divinationWaitingContainer.gameObject.SetActive(true);
    this.RefreshDivinationWaiting();
  }

  public bool IsWaitingDivination() => this.divinationWaitingContainer.gameObject.activeSelf;

  public void RefreshDivinationWaiting()
  {
    if (!this.divinationWaitingContainer.gameObject.activeSelf)
      return;
    int ready = 0;
    foreach (KeyValuePair<string, bool> keyValuePair in NetworkManager.Instance.PlayerDivinationReady)
    {
      if (keyValuePair.Value)
        ++ready;
    }
    if (ready == NetworkManager.Instance.PlayerDivinationReady.Count)
      this.divinationWaitingMsg.text = Texts.Instance.GetText("divinationRoundLaunch");
    else
      this.divinationWaitingMsg.text = NetworkManager.Instance.GetWaitingPlayersString(ready, NetworkManager.Instance.PlayerDivinationReady.Count);
  }

  public void CraftSelectorEnergy(CardCraftSelectorEnergy CCSE, string energy)
  {
    int num = !(energy == "4+") ? int.Parse(energy) : 4;
    if ((UnityEngine.Object) this.selectorEnergy != (UnityEngine.Object) null)
      this.selectorEnergy.SetEnable(false);
    if (!this.currentCraftAllCosts && this.currentCraftCost == num)
    {
      this.currentCraftAllCosts = true;
      this.currentCraftCost = 0;
    }
    else
    {
      this.currentCraftAllCosts = false;
      this.currentCraftCost = num;
      this.selectorEnergy = CCSE;
      CCSE.SetEnable(true);
    }
    this.maxCraftPageNum = 1;
    this.ChangePage(1);
  }

  public void CraftSelectorRarity(CardCraftSelectorRarity CCSR, Enums.CardRarity rarity)
  {
    this.currentCraftRarity[rarity] = !this.currentCraftRarity[rarity];
    if (!this.currentCraftRarity[rarity])
      CCSR.SetEnable(false);
    this.currentCraftAllRarities = true;
    foreach (KeyValuePair<Enums.CardRarity, bool> keyValuePair in this.currentCraftRarity)
    {
      if (keyValuePair.Value)
      {
        this.currentCraftAllRarities = false;
        break;
      }
    }
    this.maxCraftPageNum = 1;
    this.ChangePage(1);
  }

  private void CreateCraftPages(int page, int total)
  {
    this.ClearCraftPages();
    this.maxCraftPageNum = total;
    int num1 = 6;
    int num2 = 1;
    int num3 = 0;
    if (total <= 1)
      return;
    for (int index = 0; index < total; ++index)
    {
      bool flag = false;
      Transform craftPageContainer = this.cardCraftPageContainer;
      if (this.craftType == 4)
        craftPageContainer = this.itemsCraftPageContainer;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cardCraftPageButton, Vector3.zero, Quaternion.identity, craftPageContainer);
      gameObject.transform.localPosition = new Vector3(0.625f * (float) num3, 0.0f, 0.0f);
      gameObject.name = "CraftPage";
      BotonGeneric component = gameObject.transform.GetComponent<BotonGeneric>();
      component.transform.localScale = new Vector3(1f, 1f, 1f);
      if (total < num1)
      {
        component.SetText((index + 1).ToString() ?? "");
        component.auxInt = index + 1;
        if (page - 1 == index)
        {
          component.ShowDisableMask(false);
          component.Disable();
          component.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        }
        flag = true;
      }
      else if (index == num2 && index != page && page > num2 + num2 + 1)
      {
        component.SetText("...");
        component.Disable();
        component.ShowBackgroundDisable(false);
        component.ShowBackground(false);
        flag = true;
      }
      else if (index == total - num2 - 1 && index != page && page < total - num2 - num2)
      {
        component.SetText("...");
        component.Disable();
        component.ShowBackgroundDisable(false);
        component.ShowBackground(false);
        flag = true;
      }
      else if ((index <= num2 || index >= page - num2 - 1 || index >= total - Functions.FuncRoundToInt((float) num1 * 0.5f) - num2) && (index <= page + num2 - 1 || index >= total - num2 || index <= Functions.FuncRoundToInt((float) num1 * 0.5f) + 1))
      {
        component.SetText((index + 1).ToString() ?? "");
        component.auxInt = index + 1;
        if (page - 1 == index)
        {
          component.ShowDisableMask(false);
          component.Disable();
          component.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        }
        flag = true;
      }
      if (flag)
        ++num3;
      else
        gameObject.gameObject.SetActive(false);
    }
  }

  public void ChangePage(int page)
  {
    if (this.craftType == 2)
    {
      this.ShowListCardsForCraft(page);
    }
    else
    {
      if (this.craftType != 4)
        return;
      this.ShowItemsForBuy(page);
    }
  }

  public void CraftCard(string cardId) => this.SetPrice("Craft", "", cardId, this.craftTierZone);

  public void CreateDeck(int _heroIndex, bool fast = false)
  {
    this.heroIndex = _heroIndex;
    this.currentHero = AtOManager.Instance.GetHero(_heroIndex);
    if (this.currentHero == null || (UnityEngine.Object) this.currentHero.HeroData == (UnityEngine.Object) null)
      return;
    int count = this.currentHero.Cards.Count;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Texts.Instance.GetText("heroCards").Replace("<hero>", this.currentHero.SourceName));
    stringBuilder.Append("  <size=-.2><color=#CCC>(");
    stringBuilder.Append(count);
    if (this.craftType == 1 && !AtOManager.Instance.Sandbox_noMinimumDecksize)
      stringBuilder.Append(")*</color>");
    else
      stringBuilder.Append(")</color>");
    this.cardsOwner.text = stringBuilder.ToString();
    if (this.craftType == 4)
    {
      this.RedrawGridLayout();
    }
    else
    {
      this.ShowRemainingUses();
      this.ShowElements("");
      this.cardActiveName = "";
      this.ClearCardListContainer();
      this.RemoveUpgradeCards();
      this.RemoveRemoveCards();
      this.SetBlocked(false);
      this.deckBgBorder.color = Functions.HexToColor(Globals.Instance.ClassColor[Enum.GetName(typeof (Enums.HeroClass), (object) this.currentHero.HeroData.HeroClass)]);
      this.deckGOs = new GameObject[count];
      Dictionary<string, int> source = new Dictionary<string, int>();
      SortedList sortedList = new SortedList();
      for (int index = 0; index < count; ++index)
        sortedList.Add((object) (Globals.Instance.GetCardData(this.currentHero.Cards[index], false).CardName + this.currentHero.Cards[index] + index.ToString()), (object) (this.currentHero.Cards[index] + "_" + index.ToString()));
      for (int index = 0; index < count; ++index)
      {
        CardData cardData = Globals.Instance.GetCardData(sortedList.GetByIndex(index).ToString().Split('_', StringSplitOptions.None)[0], false);
        int num = cardData.CardClass != Enums.CardClass.Injury ? (cardData.CardClass != Enums.CardClass.Boon ? cardData.EnergyCost : -2) : -1;
        source.Add(cardData.Id + "_" + sortedList.GetByIndex(index).ToString().Split('_', StringSplitOptions.None)[1], num);
      }
      Dictionary<string, int> dictionary = source.OrderBy<KeyValuePair<string, int>, int>((Func<KeyValuePair<string, int>, int>) (x => x.Value)).ToDictionary<KeyValuePair<string, int>, string, int>((Func<KeyValuePair<string, int>, string>) (x => x.Key), (Func<KeyValuePair<string, int>, int>) (x => x.Value));
      this.cardVerticalDeck = new CardVertical[dictionary.Count];
      for (int index1 = 0; index1 < dictionary.Count; ++index1)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cardVerticalPrefab, new Vector3(0.0f, 0.0f, -3f), Quaternion.identity, this.cardListContainer);
        string key = dictionary.ElementAt<KeyValuePair<string, int>>(index1).Key;
        int index2 = int.Parse(key.Split('_', StringSplitOptions.None)[1]);
        gameObject.name = "deckcard_" + index2.ToString();
        this.deckGOs[index2] = gameObject;
        this.cardVerticalDeck[index2] = gameObject.GetComponent<CardVertical>();
        this.cardVerticalDeck[index2].SetCard(key, this.craftType, this.currentHero);
        if (this.craftType == 6 || this.craftType == 0)
          this.CheckForCorruptableCards(this.cardVerticalDeck[index2]);
      }
      this.RedrawGridLayout();
      this.deckEnergy.WriteEnergy(_heroIndex, 0);
    }
  }

  private void RedrawGridLayout()
  {
    this.cardListContainer.GetComponent<GridLayoutGroup>().enabled = false;
    this.cardListContainer.GetComponent<GridLayoutGroup>().enabled = true;
  }

  public bool CanCraftThisCard(CardData cData)
  {
    if (cData.CardUpgraded == Enums.CardUpgraded.Rare)
      return false;
    cData = Functions.GetCardDataFromCardData(cData, "");
    if ((bool) (UnityEngine.Object) MapManager.Instance && GameManager.Instance.IsObeliskChallenge())
      return this.maxCraftRarity == Enums.CardRarity.Mythic || this.maxCraftRarity == Enums.CardRarity.Epic && cData.CardRarity != Enums.CardRarity.Mythic || this.maxCraftRarity == Enums.CardRarity.Rare && cData.CardRarity != Enums.CardRarity.Mythic && cData.CardRarity != Enums.CardRarity.Epic || this.maxCraftRarity == Enums.CardRarity.Uncommon && cData.CardRarity != Enums.CardRarity.Mythic && cData.CardRarity != Enums.CardRarity.Epic && cData.CardRarity != Enums.CardRarity.Rare || this.maxCraftRarity == Enums.CardRarity.Common && cData.CardRarity == Enums.CardRarity.Common;
    if (AtOManager.Instance.Sandbox_allRarities)
      return true;
    if (cData.CardRarity == Enums.CardRarity.Mythic)
      return false;
    if (AtOManager.Instance.GetTownTier() == 0)
    {
      if (cData.CardRarity == Enums.CardRarity.Rare && (!PlayerManager.Instance.PlayerHaveSupply("townUpgrade_1_4") || AtOManager.Instance.GetNgPlus() >= 8) || cData.CardRarity == Enums.CardRarity.Epic || cData.CardRarity == Enums.CardRarity.Mythic)
        return false;
    }
    else if (AtOManager.Instance.GetTownTier() == 1 && cData.CardRarity == Enums.CardRarity.Epic && (!PlayerManager.Instance.PlayerHaveSupply("townUpgrade_1_6") || AtOManager.Instance.GetNgPlus() >= 8))
      return false;
    return true;
  }

  public void ShowListCardsForCraft(int pageNum, bool reset = false)
  {
    if (this.heroIndex == -1 || AtOManager.Instance.GetHero(this.heroIndex) == null || (UnityEngine.Object) AtOManager.Instance.GetHero(this.heroIndex).HeroData == (UnityEngine.Object) null)
      return;
    this.SetBlocked(false);
    PopupManager.Instance.ClosePopup();
    if (pageNum < 1)
      return;
    if (reset)
      this.maxCraftPageNum = 1;
    if (pageNum > this.maxCraftPageNum)
      return;
    this.currentCraftPageNum = pageNum;
    Enums.CardClass result1 = Enums.CardClass.None;
    Enum.TryParse<Enums.CardClass>(Enum.GetName(typeof (Enums.HeroClass), (object) AtOManager.Instance.GetHero(this.heroIndex).HeroData.HeroClass), out result1);
    if (result1 == Enums.CardClass.None)
      return;
    Enums.CardClass result2 = Enums.CardClass.None;
    Enum.TryParse<Enums.CardClass>(Enum.GetName(typeof (Enums.HeroClass), (object) AtOManager.Instance.GetHero(this.heroIndex).HeroData.HeroSubClass.HeroClassSecondary), out result2);
    List<string> stringList = new List<string>();
    if (AtOManager.Instance.advancedCraft)
    {
      int count1 = Globals.Instance.CardListByClass[result1].Count;
      for (int index = 0; index < count1; ++index)
        stringList.Add(Globals.Instance.CardListByClass[result1][index]);
      if (result2 != Enums.CardClass.None)
      {
        int count2 = Globals.Instance.CardListByClass[result2].Count;
        for (int index = 0; index < count2; ++index)
          stringList.Add(Globals.Instance.CardListByClass[result2][index]);
        stringList.Sort();
      }
    }
    else
    {
      int count3 = Globals.Instance.CardListNotUpgradedByClass[result1].Count;
      for (int index = 0; index < count3; ++index)
        stringList.Add(Globals.Instance.CardListNotUpgradedByClass[result1][index]);
      if (result2 != Enums.CardClass.None)
      {
        int count4 = Globals.Instance.CardListNotUpgradedByClass[result2].Count;
        for (int index = 0; index < count4; ++index)
          stringList.Add(Globals.Instance.CardListNotUpgradedByClass[result2][index]);
        stringList.Sort();
      }
    }
    Transform cardCraftContainer = this.cardCraftContainer;
    float num1 = 5f;
    int num2 = 0;
    float num3 = num1 * 2f;
    int num4 = 0;
    int playerDust = AtOManager.Instance.GetPlayerDust();
    for (int index = 0; index < stringList.Count; ++index)
    {
      string str1 = stringList[index];
      CardData cardData = Globals.Instance.GetCardData(str1, false);
      if (cardData.CardUpgraded != Enums.CardUpgraded.No)
        str1 = cardData.UpgradedFrom.Trim();
      if ((PlayerManager.Instance.IsCardUnlocked(str1) || GameManager.Instance.IsObeliskChallenge()) && this.CanCraftThisCard(cardData))
      {
        if (!this.currentCraftAllRarities || !this.currentCraftAllCosts)
        {
          if (this.currentCraftAllRarities || this.currentCraftRarity[cardData.CardRarity])
          {
            if (!this.currentCraftAllCosts)
            {
              if (this.currentCraftCost < 4)
              {
                if (cardData.EnergyCost != this.currentCraftCost)
                  continue;
              }
              else if (cardData.EnergyCost < this.currentCraftCost)
                continue;
            }
          }
          else
            continue;
        }
        bool flag = true;
        if (flag && AtOManager.Instance.craftFilterDT.Count > 0)
        {
          foreach (string str2 in AtOManager.Instance.craftFilterDT)
          {
            flag = false;
            string str3 = str2;
            if (str3 != "heal" && str3 != "energy" && str3 != "draw" && str3 != "discard")
            {
              if (str3 == "slash")
                str3 = "slashing";
              if (cardData.DamageType != Enums.DamageType.None && Enum.GetName(typeof (Enums.DamageType), (object) cardData.DamageType).ToLower() == str3)
                flag = true;
              else if (cardData.DamageType2 != Enums.DamageType.None && Enum.GetName(typeof (Enums.DamageType), (object) cardData.DamageType2).ToLower() == str3)
                flag = true;
            }
            else
            {
              switch (str3)
              {
                case "heal":
                  if (cardData.Heal > 0 || cardData.HealSides > 0 || cardData.HealSelf > 0)
                  {
                    flag = true;
                    break;
                  }
                  break;
                case "energy":
                  if (cardData.EnergyRecharge > 0)
                  {
                    flag = true;
                    break;
                  }
                  break;
                case "draw":
                  if (cardData.DrawCard > 0)
                  {
                    flag = true;
                    break;
                  }
                  break;
                case "discard":
                  if (cardData.DiscardCard > 0)
                  {
                    flag = true;
                    break;
                  }
                  break;
              }
            }
            if (!flag)
              break;
          }
        }
        if (flag && AtOManager.Instance.craftFilterAura.Count > 0)
        {
          foreach (string str4 in AtOManager.Instance.craftFilterAura)
          {
            flag = false;
            string str5 = !(str4 == "stanza") ? str4 : "stanzai";
            if ((UnityEngine.Object) cardData.Aura != (UnityEngine.Object) null && cardData.Aura.Id == str5)
              flag = true;
            else if ((UnityEngine.Object) cardData.Aura2 != (UnityEngine.Object) null && cardData.Aura2.Id == str5)
              flag = true;
            else if ((UnityEngine.Object) cardData.Aura3 != (UnityEngine.Object) null && cardData.Aura3.Id == str5)
              flag = true;
            else if ((UnityEngine.Object) cardData.AuraSelf != (UnityEngine.Object) null && cardData.AuraSelf.Id == str5)
              flag = true;
            else if ((UnityEngine.Object) cardData.AuraSelf2 != (UnityEngine.Object) null && cardData.AuraSelf2.Id == str5)
              flag = true;
            else if ((UnityEngine.Object) cardData.AuraSelf3 != (UnityEngine.Object) null && cardData.AuraSelf3.Id == str5)
              flag = true;
            else if ((UnityEngine.Object) cardData.SpecialAuraCurseNameGlobal != (UnityEngine.Object) null && cardData.SpecialAuraCurseNameGlobal.Id == str5)
              flag = true;
            else if ((UnityEngine.Object) cardData.SpecialAuraCurseName1 != (UnityEngine.Object) null && cardData.SpecialAuraCurseName1.Id == str5)
              flag = true;
            else if ((UnityEngine.Object) cardData.SpecialAuraCurseName2 != (UnityEngine.Object) null && cardData.SpecialAuraCurseName2.Id == str5)
              flag = true;
            else if ((UnityEngine.Object) cardData.HealAuraCurseSelf != (UnityEngine.Object) null && cardData.HealAuraCurseSelf.Id == str5)
              flag = true;
            else if ((UnityEngine.Object) cardData.HealAuraCurseName != (UnityEngine.Object) null && cardData.HealAuraCurseName.Id == str5)
              flag = true;
            else if ((UnityEngine.Object) cardData.HealAuraCurseName2 != (UnityEngine.Object) null && cardData.HealAuraCurseName2.Id == str5)
              flag = true;
            else if ((UnityEngine.Object) cardData.HealAuraCurseName3 != (UnityEngine.Object) null && cardData.HealAuraCurseName3.Id == str5)
              flag = true;
            else if ((UnityEngine.Object) cardData.HealAuraCurseName4 != (UnityEngine.Object) null && cardData.HealAuraCurseName4.Id == str5)
              flag = true;
            if (!flag)
              break;
          }
        }
        if (flag && AtOManager.Instance.craftFilterCurse.Count > 0)
        {
          foreach (string str6 in AtOManager.Instance.craftFilterCurse)
          {
            flag = false;
            if ((UnityEngine.Object) cardData.Curse != (UnityEngine.Object) null && cardData.Curse.Id == str6)
              flag = true;
            else if ((UnityEngine.Object) cardData.Curse2 != (UnityEngine.Object) null && cardData.Curse2.Id == str6)
              flag = true;
            else if ((UnityEngine.Object) cardData.Curse3 != (UnityEngine.Object) null && cardData.Curse3.Id == str6)
              flag = true;
            else if ((UnityEngine.Object) cardData.CurseSelf != (UnityEngine.Object) null && cardData.CurseSelf.Id == str6)
              flag = true;
            else if ((UnityEngine.Object) cardData.CurseSelf2 != (UnityEngine.Object) null && cardData.CurseSelf2.Id == str6)
              flag = true;
            else if ((UnityEngine.Object) cardData.CurseSelf3 != (UnityEngine.Object) null && cardData.CurseSelf3.Id == str6)
              flag = true;
            else if ((UnityEngine.Object) cardData.SpecialAuraCurseNameGlobal != (UnityEngine.Object) null && cardData.SpecialAuraCurseNameGlobal.Id == str6)
              flag = true;
            else if ((UnityEngine.Object) cardData.SpecialAuraCurseName1 != (UnityEngine.Object) null && cardData.SpecialAuraCurseName1.Id == str6)
              flag = true;
            else if ((UnityEngine.Object) cardData.SpecialAuraCurseName2 != (UnityEngine.Object) null && cardData.SpecialAuraCurseName2.Id == str6)
              flag = true;
            else if ((UnityEngine.Object) cardData.HealAuraCurseSelf != (UnityEngine.Object) null && cardData.HealAuraCurseSelf.Id == str6)
              flag = true;
            else if ((UnityEngine.Object) cardData.HealAuraCurseName != (UnityEngine.Object) null && cardData.HealAuraCurseName.Id == str6)
              flag = true;
            else if ((UnityEngine.Object) cardData.HealAuraCurseName2 != (UnityEngine.Object) null && cardData.HealAuraCurseName2.Id == str6)
              flag = true;
            else if ((UnityEngine.Object) cardData.HealAuraCurseName3 != (UnityEngine.Object) null && cardData.HealAuraCurseName3.Id == str6)
              flag = true;
            else if ((UnityEngine.Object) cardData.HealAuraCurseName4 != (UnityEngine.Object) null && cardData.HealAuraCurseName4.Id == str6)
              flag = true;
            if (!flag)
              break;
          }
        }
        if (flag && (!(this.searchTerm != "") || Globals.Instance.IsInSearch(this.searchTerm, cardData.Id)))
        {
          int cost = this.SetPrice("Craft", "", stringList[index], this.craftTierZone);
          if (cost != -1)
          {
            if (AtOManager.Instance.affordableCraft)
            {
              if (cost <= playerDust)
              {
                int[] cardAvailability = this.GetCardAvailability(stringList[index], "");
                if (cardAvailability[0] >= cardAvailability[1])
                  continue;
              }
              else
                continue;
            }
            if ((double) num2 >= (double) (pageNum - 1) * (double) num3 && (double) num2 < (double) pageNum * (double) num3)
            {
              if (!this.craftCardItemDict.ContainsKey(num4))
              {
                CardCraftItem component = UnityEngine.Object.Instantiate<GameObject>(this.cardCraftItem, new Vector3(0.0f, 0.0f, -3f), Quaternion.identity, cardCraftContainer).transform.GetComponent<CardCraftItem>();
                this.craftCardItemDict.Add(num4, component);
                int num5 = Mathf.FloorToInt((float) num4 / num1);
                float x = (float) ((double) num4 % (double) num1 * 2.25 - 1.7999999523162842);
                float y = (float) (1.7999999523162842 - 3.7000000476837158 * (double) num5);
                component.SetPosition(new Vector3(x, y, 0.0f));
                component.SetIndex(num4);
                component.SetHero(this.currentHero);
                component.SetGenericCard();
                component.SetButtonText(this.ButtonText(cost));
                component.SetCard(stringList[index], _hero: this.currentHero);
              }
              else
              {
                CardCraftItem cardCraftItem = this.craftCardItemDict[num4];
                cardCraftItem.SetButtonText(this.ButtonText(cost));
                cardCraftItem.SetCard(stringList[index], _hero: this.currentHero);
                cardCraftItem.transform.gameObject.SetActive(true);
              }
              ++num4;
            }
            ++num2;
          }
        }
      }
    }
    if ((double) num4 < (double) num1 * 2.0)
    {
      for (int key = num4; (double) key < (double) num1 * 2.0; ++key)
      {
        if (this.craftCardItemDict.ContainsKey(key))
          this.craftCardItemDict[key].transform.gameObject.SetActive(false);
      }
    }
    this.CreateCraftPages(pageNum, Mathf.CeilToInt((float) num2 / num3));
    if (AtOManager.Instance.TownTutorialStep != 0)
      return;
    foreach (KeyValuePair<int, CardCraftItem> keyValuePair in this.craftCardItemDict)
    {
      if (keyValuePair.Value.cardId != "fireball")
        keyValuePair.Value.EnableButton(false);
      else
        keyValuePair.Value.EnableButton(true);
    }
  }

  public void SelectFilter(string type, string name, bool state)
  {
    switch (type)
    {
      case "dt":
        if (state)
        {
          if (this.craftFilterDT.Contains(name))
            break;
          this.craftFilterDT.Add(name);
          break;
        }
        this.craftFilterDT.Remove(name);
        break;
      case "aura":
        if (state)
        {
          if (this.craftFilterAura.Contains(name))
            break;
          this.craftFilterAura.Add(name);
          break;
        }
        this.craftFilterAura.Remove(name);
        break;
      case "curse":
        if (state)
        {
          if (this.craftFilterCurse.Contains(name))
            break;
          this.craftFilterCurse.Add(name);
          break;
        }
        this.craftFilterCurse.Remove(name);
        break;
    }
  }

  public void ShowFilter(bool state)
  {
    PopupManager.Instance.ClosePopup();
    this.filterWindow.gameObject.SetActive(state);
    if (state)
    {
      this.controllerVerticalIndex = -1;
      this.ShowSearch(false);
      this.craftFilterDT = new List<string>();
      for (int index = 0; index < AtOManager.Instance.craftFilterDT.Count; ++index)
        this.craftFilterDT.Add(AtOManager.Instance.craftFilterDT[index]);
      this.craftFilterAura = new List<string>();
      for (int index = 0; index < AtOManager.Instance.craftFilterAura.Count; ++index)
        this.craftFilterAura.Add(AtOManager.Instance.craftFilterAura[index]);
      this.craftFilterCurse = new List<string>();
      for (int index = 0; index < AtOManager.Instance.craftFilterCurse.Count; ++index)
        this.craftFilterCurse.Add(AtOManager.Instance.craftFilterCurse[index]);
      if (this.craftBotonFilters.Count == 0)
      {
        foreach (Transform transform in this.filterWindow.GetChild(2))
        {
          if ((bool) (UnityEngine.Object) transform.GetComponent<BotonFilter>())
            this.craftBotonFilters.Add(transform.GetComponent<BotonFilter>());
        }
        foreach (Transform transform in this.filterWindow.GetChild(3))
        {
          if ((bool) (UnityEngine.Object) transform.GetComponent<BotonFilter>())
            this.craftBotonFilters.Add(transform.GetComponent<BotonFilter>());
        }
        foreach (Transform transform in this.filterWindow.GetChild(4))
        {
          if ((bool) (UnityEngine.Object) transform.GetComponent<BotonFilter>())
            this.craftBotonFilters.Add(transform.GetComponent<BotonFilter>());
        }
        foreach (Transform transform in this.filterWindow.GetChild(5))
        {
          if ((bool) (UnityEngine.Object) transform.GetComponent<BotonFilter>())
            this.craftBotonFilters.Add(transform.GetComponent<BotonFilter>());
        }
      }
      foreach (Transform transform in this.filterWindow.GetChild(2))
      {
        if ((bool) (UnityEngine.Object) transform.GetComponent<BotonFilter>())
        {
          if (this.craftFilterDT.Contains(transform.GetComponent<BotonFilter>().id))
            transform.GetComponent<BotonFilter>().select(true);
          else
            transform.GetComponent<BotonFilter>().select(false);
        }
      }
      foreach (Transform transform in this.filterWindow.GetChild(3))
      {
        if ((bool) (UnityEngine.Object) transform.GetComponent<BotonFilter>())
        {
          if (this.craftFilterDT.Contains(transform.GetComponent<BotonFilter>().id))
            transform.GetComponent<BotonFilter>().select(true);
          else
            transform.GetComponent<BotonFilter>().select(false);
        }
      }
      foreach (Transform transform in this.filterWindow.GetChild(4))
      {
        if ((bool) (UnityEngine.Object) transform.GetComponent<BotonFilter>())
        {
          if (this.craftFilterAura.Contains(transform.GetComponent<BotonFilter>().id))
            transform.GetComponent<BotonFilter>().select(true);
          else
            transform.GetComponent<BotonFilter>().select(false);
        }
      }
      foreach (Transform transform in this.filterWindow.GetChild(5))
      {
        if ((bool) (UnityEngine.Object) transform.GetComponent<BotonFilter>())
        {
          if (this.craftFilterCurse.Contains(transform.GetComponent<BotonFilter>().id))
            transform.GetComponent<BotonFilter>().select(true);
          else
            transform.GetComponent<BotonFilter>().select(false);
        }
      }
    }
    else
    {
      this.ShowSearch(true);
      this.SetFilterButtonState();
    }
  }

  private void SetFilterButtonState()
  {
    if (AtOManager.Instance.craftFilterDT.Count > 0 || AtOManager.Instance.craftFilterAura.Count > 0 || AtOManager.Instance.craftFilterCurse.Count > 0)
    {
      this.buttonFilterCraft.SetState(true);
      this.buttonFilterCraft.GetComponent<BotonGeneric>().ShowDisableMask(false);
    }
    else
    {
      this.buttonFilterCraft.SetState(false);
      this.buttonFilterCraft.GetComponent<BotonGeneric>().ShowDisableMask(true);
    }
  }

  public void ResetFilter()
  {
    this.craftFilterAura.Clear();
    this.craftFilterCurse.Clear();
    this.craftFilterDT.Clear();
    foreach (BotonFilter craftBotonFilter in this.craftBotonFilters)
      craftBotonFilter.select(false);
    this.ApplyFilter();
  }

  public void ApplyFilter()
  {
    AtOManager.Instance.craftFilterDT = this.craftFilterDT;
    AtOManager.Instance.craftFilterAura = this.craftFilterAura;
    AtOManager.Instance.craftFilterCurse = this.craftFilterCurse;
    this.ShowListCardsForCraft(1, true);
    this.ShowFilter(false);
  }

  public void AssignChallengeTitle(int block, string title)
  {
    this.cardChallengeTitle[block].SetText(title);
    this.ShowChallengeTitle(true, block);
  }

  public void AssignChallengeRoundCards(int round, int maxRound)
  {
    this.cardChallengeGlobalTitle.text = Texts.Instance.GetText("challengeSelection");
    this.cardChallengeGlobalIntro.text = Texts.Instance.GetText("challengeSelectionIntro");
    this.cardChallengeRound.text = string.Format("<color=#FFF>{0}</color> <size=+.2>[ <color=#FC0>{1}</color> / {2} ]", (object) Texts.Instance.GetText("challengeRound"), (object) round, (object) maxRound);
  }

  public void AssignChallengeRoundPerks(int selected, int maxPerks)
  {
    this.cardChallengeGlobalTitle.text = Texts.Instance.GetText("challengeSelectionPerk");
    this.cardChallengeGlobalIntro.text = Texts.Instance.GetText("challengeSelectionPerkIntro");
    this.cardChallengeRound.text = string.Format("<color=#FFF>{0}</color><br><size=+2>[ <color=#FC0>{1}</color> / {2} ]", (object) Texts.Instance.GetText("challengeRoundPerks"), (object) selected, (object) maxPerks);
  }

  public void AssignChallengeCard(
    int hero,
    int block,
    int row,
    string theCard,
    bool selectedPack = false)
  {
    if (this.heroIndex != hero)
      return;
    GameObject card = UnityEngine.Object.Instantiate<GameObject>(this.cardVerticalPrefab, new Vector3(0.0f, 0.0f, -3f), Quaternion.identity, this.cardChallengeContainer[block]);
    card.gameObject.SetActive(false);
    if (this.assignSpecial != null)
      this.StopCoroutine(this.assignSpecial);
    this.assignCard = this.StartCoroutine(this.ShowChallengeCardCo(card, row, theCard, block, selectedPack));
  }

  private IEnumerator ShowChallengeCardCo(
    GameObject card,
    int row,
    string theCard,
    int block,
    bool selectedPack)
  {
    yield return (object) Globals.Instance.WaitForSeconds((float) (0.15000000596046448 + (double) (row + 1) * 0.05000000074505806));
    GameManager.Instance.PlayLibraryAudio("dealcard", 0.08f);
    if ((UnityEngine.Object) card != (UnityEngine.Object) null)
    {
      CardVertical component = card.GetComponent<CardVertical>();
      component.SetCard(theCard, _hero: this.currentHero);
      card.gameObject.SetActive(true);
      CardData cardData = Globals.Instance.GetCardData(theCard, false);
      if (cardData.CardUpgraded == Enums.CardUpgraded.A)
        component.PlayParticle("A");
      else if (cardData.CardUpgraded == Enums.CardUpgraded.B)
        component.PlayParticle("B");
      if (row == ChallengeSelectionManager.Instance.CardsForPack - 1)
      {
        if (selectedPack)
        {
          this.ShowChallengePackSelected(true, block);
          this.ShowChallengeButtons(false, block);
        }
        else if (!GameManager.Instance.IsMultiplayer() || AtOManager.Instance.GetHero(ChallengeSelectionManager.Instance.currentHeroIndex).Owner == NetworkManager.Instance.GetPlayerNick())
          this.ShowChallengeButtons(true, block);
        else
          this.ShowChallengeButtons(false, block);
      }
    }
  }

  public void AssignChallengeCardSpecial(string theCards, bool showButtons)
  {
    if (this.assignCard != null)
      this.StopCoroutine(this.assignCard);
    if (this.assignSpecial != null)
      this.StopCoroutine(this.assignSpecial);
    this.assignSpecial = this.StartCoroutine(this.ShowChallengeCardSpecialCo(theCards, showButtons));
  }

  private IEnumerator ShowChallengeCardSpecialCo(string theCards, bool showButtons)
  {
    string[] theCardsArr = theCards.Split('_', StringSplitOptions.None);
    for (int i = 0; i < 3; ++i)
    {
      string id = theCardsArr[i];
      if (id != null)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, new Vector3((float) (3.7999999523162842 * (double) i - 0.699999988079071), 0.35f, -5f), Quaternion.identity, this.tmpContainer);
        CardItem component = gameObject.GetComponent<CardItem>();
        gameObject.name = "card_" + i.ToString();
        component.SetCard(id);
        component.DoReward(false, true, modspeed: 0.3f);
        component.SetDestinationLocalScale(1.2f);
        component.TopLayeringOrder("UI");
        component.cardmakebig = true;
        component.CreateColliderAdjusted();
        component.cardmakebigSize = 1.2f;
        component.cardmakebigSizeMax = 1.3f;
        yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      }
    }
    if (showButtons)
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.35f);
      for (int block = 0; block < 3; ++block)
        this.ShowChallengeButtons(true, block);
    }
  }

  public void CleanChallengeBlocks(int _theBlock = -1)
  {
    for (int _block = 0; _block < this.cardChallengeContainer.Length; ++_block)
    {
      if (_theBlock == -1 || _block == _theBlock)
      {
        foreach (Component component in this.cardChallengeContainer[_block])
          UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
        this.ShowChallengePackSelected(false, _block);
      }
    }
    if (_theBlock != -1)
      return;
    this.ShowChallengeButtons(false);
    this.ShowChallengeTitles(false);
    this.ClearTmpContainer();
  }

  public void ShowChallengeTitle(bool state, int _theBlock = -1)
  {
    if (this.cardChallengeTitle[_theBlock].gameObject.activeSelf != state)
      this.cardChallengeTitle[_theBlock].gameObject.SetActive(state);
    if (!state)
      return;
    if (ChallengeSelectionManager.Instance.GetCurrentRound() < 3)
    {
      this.cardChallengeTitle[_theBlock].transform.localPosition = new Vector3((float) ((double) (_theBlock % 4) * 3.7999999523162842 - 8.25), -1.3f, this.cardChallengeTitle[_theBlock].transform.localPosition.z);
      if (_theBlock != 0 && _theBlock != 1)
        return;
      foreach (Transform transform in this.cardChallengeTitle[_theBlock].transform)
      {
        if ((UnityEngine.Object) transform != (UnityEngine.Object) null && transform.name == "Recommended")
        {
          transform.gameObject.SetActive(true);
          break;
        }
      }
    }
    else
    {
      this.cardChallengeTitle[_theBlock].transform.localPosition = new Vector3((float) ((double) _theBlock * 4.75 - 7.3499999046325684), -1.65f, this.cardChallengeTitle[_theBlock].transform.localPosition.z);
      if (_theBlock != 0 && _theBlock != 1)
        return;
      foreach (Transform transform in this.cardChallengeTitle[_theBlock].transform)
      {
        if ((UnityEngine.Object) transform != (UnityEngine.Object) null && transform.name == "Recommended")
        {
          transform.gameObject.SetActive(false);
          break;
        }
      }
    }
  }

  public void ShowChallengeTitles(bool state)
  {
    for (int index = 0; index < this.cardChallengeTitle.Length; ++index)
    {
      if (this.cardChallengeTitle[index].gameObject.activeSelf != state)
        this.cardChallengeTitle[index].gameObject.SetActive(state);
    }
  }

  public void ShowChallengePackSelected(bool _state, int _block = -1)
  {
    if (this.cardChallengeSelected[_block].gameObject.activeSelf != _state)
      this.cardChallengeSelected[_block].gameObject.SetActive(_state);
    if (!_state)
      return;
    foreach (Component component in this.cardChallengeContainer[_block])
      component.GetComponent<CardVertical>().SetBgColor("#FF8500");
  }

  public void ReassignChallengeButtons()
  {
    if (!(bool) (UnityEngine.Object) ChallengeSelectionManager.Instance)
      return;
    for (int index = 0; index < this.cardChallengeButton.Length; ++index)
      this.cardChallengeButton[index].GetComponent<BotonGeneric>().auxInt = ChallengeSelectionManager.Instance.currentHeroIndex;
  }

  public void ShowChallengeButtons(bool state, int block = -1)
  {
    if (!(bool) (UnityEngine.Object) ChallengeSelectionManager.Instance)
      return;
    for (int index = 0; index < this.cardChallengeButton.Length; ++index)
    {
      if (block == -1 || block == index)
      {
        if (this.cardChallengeButton[index].gameObject.activeSelf != state)
        {
          if (state)
            this.cardChallengeButton[index].gameObject.SetActive(true);
          else
            this.cardChallengeButton[index].gameObject.SetActive(false);
        }
        if (state)
          this.cardChallengeButton[index].localPosition = ChallengeSelectionManager.Instance.GetCurrentRound() >= 3 ? new Vector3((float) ((double) index * 4.75 - 7.1999998092651367), -6.25f, this.cardChallengeButton[index].localPosition.z) : new Vector3((float) ((double) (index % 4) * 3.7999999523162842 - 8.1000003814697266), -3.3f, this.cardChallengeButton[index].localPosition.z);
      }
    }
    if (!state)
      return;
    for (int index = 0; index < this.cardChallengeButton.Length; ++index)
      this.cardChallengeButton[index].GetComponent<BotonGeneric>().auxInt = ChallengeSelectionManager.Instance.currentHeroIndex;
  }

  public void ShowChallengeReroll(bool state)
  {
    if (state)
      this.rerollChallenge.Enable();
    else
      this.rerollChallenge.Disable();
  }

  public void ActivateChallengeReroll(bool state) => this.rerollChallenge.EnabledButton(state);

  public void ShowChallengeRerollFully(bool state) => this.rerollChallenge.gameObject.SetActive(state);

  public void ShowChallengePerks(bool state)
  {
    if (this.challengePerks.gameObject.activeSelf != state)
      this.challengePerks.gameObject.SetActive(state);
    if (!state)
      return;
    if (this.assignCard != null)
      this.StopCoroutine(this.assignCard);
    if (this.assignSpecial == null)
      return;
    this.StopCoroutine(this.assignSpecial);
  }

  public void EnableChallengeReadyButton(bool state)
  {
    if (state)
      this.readyChallenge.GetComponent<BotonGeneric>().Enable();
    else
      this.readyChallenge.GetComponent<BotonGeneric>().Disable();
  }

  public void ChallengeReadySetButton(bool state)
  {
    if (state)
    {
      this.readyChallenge.GetComponent<BotonGeneric>().SetColorAbsolute(Functions.HexToColor("#15A42E"));
      if (GameManager.Instance.IsMultiplayer())
        this.readyChallenge.GetComponent<BotonGeneric>().SetText(Texts.Instance.GetText("waitingForPlayers"));
      else
        this.readyChallenge.GetComponent<BotonGeneric>().SetText(Texts.Instance.GetText("ready"));
    }
    else
    {
      this.readyChallenge.GetComponent<BotonGeneric>().SetColorAbsolute(Functions.HexToColor(Globals.Instance.ClassColor["warrior"]));
      this.readyChallenge.GetComponent<BotonGeneric>().SetText(Texts.Instance.GetText("ready"));
    }
  }

  public void SetWaitingPlayerTextChallenge(string msg)
  {
    if (msg != "")
    {
      this.waitingMsgChallenge.gameObject.SetActive(true);
      this.waitingMsgTextChallenge.text = msg;
    }
    else
      this.waitingMsgChallenge.gameObject.SetActive(false);
  }

  public void ShowSaveLoad()
  {
    if (!this.cardCraftSave.gameObject.activeSelf)
    {
      this.cardCraftElements.gameObject.SetActive(false);
      this.cardCraftSave.gameObject.SetActive(true);
      this.deckCraftPrice.text = "";
      this.botSaveLoad.SetText(Texts.Instance.GetText("saveLoadReturn"));
      this.botSaveLoad.SetPopupText("");
      this.LoadDecks();
    }
    else
    {
      this.cardCraftElements.gameObject.SetActive(true);
      this.cardCraftSave.gameObject.SetActive(false);
      this.botSaveLoad.SetText(Texts.Instance.GetText("saveLoad"));
      this.botSaveLoad.SetPopupText(Texts.Instance.GetText("saveLoadDes"));
      this.CheckForLoadSaveCorrect(false);
      this.ShowListCardsForCraft(1, true);
    }
  }

  public void SaveDeck(int slot)
  {
    this.savingSlot = slot;
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.SaveDeckAction);
    AlertManager.Instance.AlertInput(Texts.Instance.GetText("inputSaveName"), Texts.Instance.GetText("accept").ToUpper());
  }

  public void SaveDeckAction()
  {
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.SaveDeckAction);
    string str = Functions.OnlyAscii(AlertManager.Instance.GetInputValue()).Trim();
    if (str == "")
      return;
    int num = (int) Enum.Parse(typeof (Enums.CardClass), Enum.GetName(typeof (Enums.HeroClass), (object) AtOManager.Instance.GetHero(this.heroIndex).HeroData.HeroClass));
    Hero hero = AtOManager.Instance.GetHero(this.heroIndex);
    List<string> stringList = new List<string>();
    List<string> cards = hero.Cards;
    for (int index = 0; index < cards.Count; ++index)
    {
      CardData cardData = Globals.Instance.GetCardData(cards[index]);
      if (cardData.CardClass != Enums.CardClass.Injury && cardData.CardClass != Enums.CardClass.Boon && cardData.CardUpgraded != Enums.CardUpgraded.Rare)
        stringList.Add(cards[index]);
    }
    string sourceName = AtOManager.Instance.GetHero(this.heroIndex).SourceName;
    if (!PlayerManager.Instance.PlayerSavedDeck.DeckTitle.ContainsKey(hero.SourceName))
      PlayerManager.Instance.PlayerSavedDeck.DeckTitle.Add(sourceName, new string[20]);
    if (!PlayerManager.Instance.PlayerSavedDeck.DeckCards.ContainsKey(hero.SourceName))
      PlayerManager.Instance.PlayerSavedDeck.DeckCards.Add(sourceName, new List<string>[20]);
    PlayerManager.Instance.PlayerSavedDeck.DeckTitle[sourceName][this.savingSlot] = str;
    PlayerManager.Instance.PlayerSavedDeck.DeckCards[sourceName][this.savingSlot] = stringList;
    SaveManager.SavePlayerDeck();
    this.LoadDecks();
  }

  private void LoadDecks()
  {
    this.loadDeckContainer.gameObject.SetActive(false);
    this.CheckForLoadSaveCorrect();
    Hero hero = AtOManager.Instance.GetHero(this.heroIndex);
    string sourceName = hero.SourceName;
    Enum.GetName(typeof (Enums.HeroClass), (object) hero.HeroData.HeroClass);
    this.loadDeckHeroSprite.sprite = hero.HeroData.HeroSubClass.SpriteBorder;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<size=+.5><color=");
    stringBuilder.Append(Globals.Instance.ClassColor[Enum.GetName(typeof (Enums.HeroClass), (object) hero.HeroData.HeroClass)]);
    stringBuilder.Append(">");
    stringBuilder.Append(sourceName);
    stringBuilder.Append("</size></color><br>");
    stringBuilder.Append(Texts.Instance.GetText("savedDecks"));
    this.loadDeckHeroName.text = stringBuilder.ToString();
    for (int index = 0; index < this.deckSlot.Length; ++index)
    {
      if ((UnityEngine.Object) this.deckSlot[index] != (UnityEngine.Object) null)
      {
        stringBuilder.Clear();
        if (PlayerManager.Instance.PlayerSavedDeck.DeckTitle.ContainsKey(sourceName) && PlayerManager.Instance.PlayerSavedDeck.DeckTitle[sourceName][index] != null && PlayerManager.Instance.PlayerSavedDeck.DeckTitle[sourceName][index] != "")
        {
          stringBuilder.Append(PlayerManager.Instance.PlayerSavedDeck.DeckTitle[sourceName][index]);
          this.deckSlot[index].SetActive(stringBuilder.ToString(), PlayerManager.Instance.PlayerSavedDeck.DeckCards[sourceName][index].Count.ToString());
        }
        else
          this.deckSlot[index].SetEmpty(this.deckAvailableForSaveLoad);
      }
    }
  }

  private void CheckForLoadSaveCorrect(bool checkLockStatus = true)
  {
    this.deckAvailableForSaveLoad = true;
    for (int index = 0; index < this.cardVerticalDeck.Length; ++index)
    {
      if (checkLockStatus && this.cardVerticalDeck[index].cardData.CardClass == Enums.CardClass.Injury || this.cardVerticalDeck[index].cardData.CardClass == Enums.CardClass.Boon || this.cardVerticalDeck[index].cardData.CardUpgraded == Enums.CardUpgraded.Rare || this.cardVerticalDeck[index].cardData.CardClass == Enums.CardClass.Special && !this.cardVerticalDeck[index].cardData.Starter)
      {
        this.cardVerticalDeck[index].ShowLock(true);
        this.deckAvailableForSaveLoad = false;
      }
      else
        this.cardVerticalDeck[index].ShowLock(false);
    }
  }

  public void LoadDeck(int slot)
  {
    this.deckCraftingCostDust = 0;
    this.deckCraftingCostGold = 0;
    this.craftTimes = new Dictionary<string, int>();
    List<string> stringList = new List<string>();
    this.savingSlot = slot;
    string sourceName = AtOManager.Instance.GetHero(this.heroIndex).SourceName;
    Enum.GetName(typeof (Enums.HeroClass), (object) AtOManager.Instance.GetHero(this.heroIndex).HeroData.HeroClass);
    List<string> _targetDeck = PlayerManager.Instance.PlayerSavedDeck.DeckCards[sourceName][slot];
    if (_targetDeck == null)
      return;
    int characterTier = PlayerManager.Instance.GetCharacterTier("", "card", AtOManager.Instance.GetHero(this.heroIndex).PerkRank);
    if (characterTier > 0)
    {
      for (int index = 0; index < _targetDeck.Count; ++index)
      {
        CardData cardData = Globals.Instance.GetCardData(_targetDeck[index], false);
        if (cardData.Starter)
        {
          switch (characterTier)
          {
            case 1:
              _targetDeck[index] = Functions.GetCardDataFromCardData(cardData, "A").Id.ToLower();
              continue;
            case 2:
              _targetDeck[index] = Functions.GetCardDataFromCardData(cardData, "B").Id.ToLower();
              continue;
            default:
              continue;
          }
        }
      }
    }
    foreach (Component component in this.loadDeckCardContainer)
      UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
    this.containerDeckName.text = PlayerManager.Instance.PlayerSavedDeck.DeckTitle[sourceName][slot];
    this.loadDeckContainer.gameObject.SetActive(true);
    Dictionary<string, int> source = new Dictionary<string, int>();
    SortedList sortedList = new SortedList();
    for (int index = 0; index < _targetDeck.Count; ++index)
      sortedList.Add((object) (Globals.Instance.GetCardData(_targetDeck[index], false).CardName + _targetDeck[index] + index.ToString()), (object) (_targetDeck[index] + "_" + index.ToString()));
    for (int index = 0; index < _targetDeck.Count; ++index)
    {
      CardData cardData = Globals.Instance.GetCardData(sortedList.GetByIndex(index).ToString().Split('_', StringSplitOptions.None)[0], false);
      int num = cardData.CardClass != Enums.CardClass.Injury ? (cardData.CardClass != Enums.CardClass.Boon ? cardData.EnergyCost : -2) : -1;
      source.Add(cardData.Id + "_" + sortedList.GetByIndex(index).ToString().Split('_', StringSplitOptions.None)[1], num);
    }
    Dictionary<string, int> dictionary = source.OrderBy<KeyValuePair<string, int>, int>((Func<KeyValuePair<string, int>, int>) (x => x.Value)).ToDictionary<KeyValuePair<string, int>, string, int>((Func<KeyValuePair<string, int>, string>) (x => x.Key), (Func<KeyValuePair<string, int>, int>) (x => x.Value));
    this.cardVerticalDeck = new CardVertical[dictionary.Count];
    for (int index1 = 0; index1 < dictionary.Count; ++index1)
    {
      Globals instance = Globals.Instance;
      KeyValuePair<string, int> keyValuePair = dictionary.ElementAt<KeyValuePair<string, int>>(index1);
      string id = keyValuePair.Key.Split('_', StringSplitOptions.None)[0];
      CardData cardData = instance.GetCardData(id);
      if (this.craftType != 0 || cardData.CardClass != Enums.CardClass.Injury && cardData.CardClass != Enums.CardClass.Boon)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cardVerticalPrefab, new Vector3(0.0f, 0.0f, -3f), Quaternion.identity, this.loadDeckCardContainer);
        keyValuePair = dictionary.ElementAt<KeyValuePair<string, int>>(index1);
        string key = keyValuePair.Key;
        int index2 = int.Parse(key.Split('_', StringSplitOptions.None)[1]);
        gameObject.name = key.Split('_', StringSplitOptions.None)[0];
        this.cardVerticalDeck[index2] = gameObject.GetComponent<CardVertical>();
        this.cardVerticalDeck[index2].SetCard(key, this.craftType, this.currentHero);
      }
    }
    this.loadDeckCardContainer.GetComponent<GridLayoutGroup>().enabled = false;
    this.loadDeckCardContainer.GetComponent<GridLayoutGroup>().enabled = true;
    this.SetPrice(_targetDeck);
  }

  private void SetPrice(List<string> _targetDeck)
  {
    bool flag1 = true;
    int townTier = AtOManager.Instance.GetTownTier();
    int num1 = 0;
    int num2 = 0;
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    List<string> stringList3 = new List<string>();
    List<string> stringList4 = new List<string>();
    for (int index = 0; index < this.currentHero.Cards.Count; ++index)
      stringList1.Add(this.currentHero.Cards[index].ToLower());
    for (int index = 0; index < _targetDeck.Count; ++index)
      stringList2.Add(_targetDeck[index]);
    if (stringList2.Count < 15 && (!SandboxManager.Instance.IsEnabled() || !AtOManager.Instance.Sandbox_noMinimumDecksize))
      flag1 = false;
    if (flag1)
    {
      Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
      for (int index1 = stringList2.Count - 1; index1 >= 0; --index1)
      {
        for (int index2 = 0; index2 < stringList1.Count; ++index2)
        {
          if (stringList2[index1] == stringList1[index2])
          {
            if (dictionary1.ContainsKey(stringList2[index1]))
              dictionary1[stringList2[index1]]++;
            else
              dictionary1[stringList2[index1]] = 1;
            stringList2.RemoveAt(index1);
            stringList1.RemoveAt(index2);
            break;
          }
        }
      }
      for (int index3 = stringList2.Count - 1; index3 >= 0; --index3)
      {
        CardData cardData1 = Globals.Instance.GetCardData(stringList2[index3], false);
        if (cardData1.CardUpgraded != Enums.CardUpgraded.No)
        {
          CardData cardData2 = Globals.Instance.GetCardData(cardData1.UpgradedFrom, false);
          for (int index4 = 0; index4 < stringList1.Count; ++index4)
          {
            if (stringList1[index4].ToLower() == cardData2.UpgradesTo1.ToLower() || stringList1[index4].ToLower() == cardData2.UpgradesTo2.ToLower())
            {
              stringList3.Add(stringList2[index3]);
              stringList2.RemoveAt(index3);
              stringList1.RemoveAt(index4);
              break;
            }
            if (stringList1[index4].ToLower() == cardData1.UpgradedFrom.ToLower())
            {
              stringList4.Add(stringList2[index3]);
              stringList2.RemoveAt(index3);
              stringList1.RemoveAt(index4);
              break;
            }
          }
        }
      }
      for (int index = 0; index < stringList2.Count; ++index)
      {
        int num3 = this.SetPrice("Craft", "", stringList2[index], townTier);
        num1 += num3;
      }
      for (int index = 0; index < stringList1.Count; ++index)
      {
        this.cardData = Globals.Instance.GetCardData(stringList1[index], false);
        int num4 = this.SetPrice("Remove", "");
        num2 += num4;
      }
      for (int index = 0; index < stringList3.Count; ++index)
      {
        this.cardData = Globals.Instance.GetCardData(stringList3[index], false);
        int num5 = this.SetPrice("Transform", Enum.GetName(typeof (Enums.CardRarity), (object) this.cardData.CardRarity));
        num1 += num5;
      }
      for (int index = 0; index < stringList4.Count; ++index)
      {
        this.cardData = Globals.Instance.GetCardData(stringList4[index], false);
        int num6 = this.SetPrice("Upgrade", Enum.GetName(typeof (Enums.CardRarity), (object) this.cardData.CardRarity));
        num1 += num6;
      }
      for (int index = 0; index < stringList2.Count; ++index)
      {
        if (!this.craftTimes.ContainsKey(stringList2[index]))
          this.craftTimes.Add(stringList2[index], 1);
        else
          ++this.craftTimes[stringList2[index]];
      }
      Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
      foreach (KeyValuePair<string, int> craftTime in this.craftTimes)
      {
        string id = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(craftTime.Key, false), "").Id;
        if (!dictionary2.ContainsKey(id))
          dictionary2.Add(id, craftTime.Value);
        else
          dictionary2[id] += craftTime.Value;
      }
      foreach (KeyValuePair<string, int> keyValuePair in dictionary2)
      {
        if (!PlayerManager.Instance.IsCardUnlocked(keyValuePair.Key))
        {
          this.StartCoroutine(this.LockTimeout(keyValuePair.Key));
          flag1 = false;
        }
        else if (!this.CanCraftThisCard(Globals.Instance.GetCardData(keyValuePair.Key, false)))
        {
          this.StartCoroutine(this.LockTimeout(keyValuePair.Key));
          flag1 = false;
        }
        else
        {
          bool flag2 = false;
          if (SandboxManager.Instance.IsEnabled() && AtOManager.Instance.Sandbox_unlimitedAvailableCards)
            flag2 = true;
          if (!flag2)
          {
            int[] cardAvailability = this.GetCardAvailability(keyValuePair.Key, this.itemListId);
            if (cardAvailability[0] + keyValuePair.Value > cardAvailability[1])
            {
              flag1 = false;
              int num7 = 0;
              if (dictionary1.ContainsKey(keyValuePair.Key))
                num7 = dictionary1[keyValuePair.Key];
              this.StartCoroutine(this.LockTimeout(keyValuePair.Key, cardAvailability[1] - cardAvailability[0] + num7));
            }
          }
        }
      }
    }
    StringBuilder stringBuilder = new StringBuilder();
    if (flag1 && this.deckAvailableForSaveLoad)
    {
      stringBuilder.Append(Texts.Instance.GetText("craftWillCost"));
      stringBuilder.Append("<br><size=+1>");
      stringBuilder.Append("<sprite name=gold> ");
      stringBuilder.Append(num2);
      stringBuilder.Append("  ");
      stringBuilder.Append("<sprite name=dust> ");
      stringBuilder.Append(num1);
      this.deckCraftPrice.text = stringBuilder.ToString();
      this.deckCraftingCostGold = num2;
      this.deckCraftingCostDust = num1;
      int playerDust = AtOManager.Instance.GetPlayerDust();
      int playerGold = AtOManager.Instance.GetPlayerGold();
      int num8 = num1;
      if (playerDust >= num8 && playerGold >= num2)
        this.botCraftingDeck.Enable();
      else
        this.botCraftingDeck.Disable();
    }
    else
    {
      stringBuilder.Append("<sprite name=lock> ");
      stringBuilder.Append(Texts.Instance.GetText("craftCant"));
      this.deckCraftPrice.text = stringBuilder.ToString();
      this.botCraftingDeck.Disable();
    }
  }

  private IEnumerator LockTimeout(string name, int index = 0)
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.05f);
    int num = 0;
    foreach (Transform transform in this.loadDeckCardContainer)
    {
      if (transform.gameObject.name == name)
      {
        if (num >= index)
          transform.GetComponent<CardVertical>().ShowLock(true);
        ++num;
      }
    }
    if (num == 0)
    {
      foreach (Transform transform in this.loadDeckCardContainer)
      {
        if (transform.gameObject.name == name + "a" || transform.gameObject.name == name + "b")
        {
          if (num >= index)
            transform.GetComponent<CardVertical>().ShowLock(true);
          ++num;
        }
      }
    }
  }

  public void CraftDeck()
  {
    AtOManager.Instance.PayGold(this.deckCraftingCostGold);
    AtOManager.Instance.PayDust(this.deckCraftingCostDust);
    List<string> stringList = PlayerManager.Instance.PlayerSavedDeck.DeckCards[AtOManager.Instance.GetHero(this.heroIndex).SourceName][this.savingSlot];
    List<string> cards = AtOManager.Instance.GetHero(this.heroIndex).Cards;
    List<string> _cardList = new List<string>();
    for (int index = 0; index < cards.Count; ++index)
    {
      CardData cardData = Globals.Instance.GetCardData(cards[index], false);
      if (cardData.CardClass == Enums.CardClass.Injury || cardData.CardClass == Enums.CardClass.Boon)
        _cardList.Add(cards[index]);
    }
    AtOManager.Instance.GetHero(this.heroIndex).Cards = new List<string>();
    for (int index = 0; index < stringList.Count; ++index)
      _cardList.Add(stringList[index]);
    AtOManager.Instance.GetHero(this.heroIndex).Cards = _cardList;
    this.loadDeckContainer.gameObject.SetActive(false);
    this.CreateDeck(this.heroIndex);
    AtOManager.Instance.SideBarRefreshCards(this.heroIndex);
    foreach (KeyValuePair<string, int> craftTime in this.craftTimes)
    {
      for (int index = 0; index < craftTime.Value; ++index)
        AtOManager.Instance.SaveCraftedCard(this.heroIndex, Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(craftTime.Key, false), "").Id);
    }
    if (!GameManager.Instance.IsMultiplayer())
      return;
    AtOManager.Instance.AddDeckToHeroMP(this.heroIndex, _cardList);
  }

  public int[] GetCardAvailability(string cardId, string shopId)
  {
    CardData cardData1 = Globals.Instance.GetCardData(cardId, false);
    int num1;
    CardData cardData2;
    if (cardData1.CardUpgraded != Enums.CardUpgraded.No && cardData1.UpgradedFrom != "")
    {
      num1 = AtOManager.Instance.HowManyCrafted(this.heroIndex, cardData1.UpgradedFrom.ToLower());
      cardData2 = Globals.Instance.GetCardData(cardData1.UpgradedFrom.ToLower());
    }
    else
    {
      num1 = AtOManager.Instance.HowManyCrafted(this.heroIndex, cardId);
      cardData2 = cardData1;
    }
    int num2;
    if (cardData2.CardClass == Enums.CardClass.Item)
    {
      num2 = cardData2.CardType != Enums.CardType.Pet ? 1 : (PlayerManager.Instance.IsCardUnlocked(cardData2.Id) ? 1 : 0);
      if (AtOManager.Instance.ItemBoughtOnThisShop(shopId, cardId) || cardData2.CardType == Enums.CardType.Pet && AtOManager.Instance.TeamHaveItem(cardId, 5, true))
        num1 = 1;
    }
    else if (cardData2.CardRarity == Enums.CardRarity.Common)
    {
      num2 = 1;
      if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_1_1"))
        num2 = 2;
    }
    else if (cardData2.CardRarity == Enums.CardRarity.Uncommon)
    {
      num2 = 1;
      if (PlayerManager.Instance.PlayerHaveSupply("townUpgrade_1_3") && AtOManager.Instance.GetNgPlus() < 5)
        num2 = 2;
    }
    else
      num2 = 1;
    if (num1 > num2)
      num1 = num2;
    return new int[2]{ num1, num2 };
  }

  public void RemoveDeck(int slot)
  {
    this.savingSlot = slot;
    AlertManager.buttonClickDelegate = new AlertManager.OnButtonClickDelegate(this.RemoveDeckAction);
    AlertManager.Instance.AlertConfirmDouble(Texts.Instance.GetText("savedDeckDeleteConfirm"));
  }

  public void RemoveDeckAction()
  {
    int num = AlertManager.Instance.GetConfirmAnswer() ? 1 : 0;
    AlertManager.buttonClickDelegate -= new AlertManager.OnButtonClickDelegate(this.RemoveDeckAction);
    if (num == 0)
      return;
    string sourceName = AtOManager.Instance.GetHero(this.heroIndex).SourceName;
    PlayerManager.Instance.PlayerSavedDeck.DeckTitle[sourceName][this.savingSlot] = "";
    PlayerManager.Instance.PlayerSavedDeck.DeckCards[sourceName][this.savingSlot] = new List<string>();
    this.LoadDecks();
  }

  public void SetWaitingPlayersText(string msg)
  {
    if (msg != "")
    {
      this.waitingMsg.gameObject.SetActive(true);
      this.waitingMsgText.text = msg;
    }
    else
      this.waitingMsg.gameObject.SetActive(false);
  }

  public void Ready()
  {
    if (!GameManager.Instance.IsMultiplayer() || (UnityEngine.Object) TownManager.Instance != (UnityEngine.Object) null)
    {
      this.ExitCardCraft();
    }
    else
    {
      if (this.manualReadyCo != null)
        this.StopCoroutine(this.manualReadyCo);
      this.statusReady = !this.statusReady;
      NetworkManager.Instance.SetManualReady(this.statusReady);
      if (this.statusReady)
      {
        this.exitCraftButton.GetComponent<BotonGeneric>().SetBackgroundColor(Functions.HexToColor(Globals.Instance.ClassColor["scout"]));
        this.exitCraftButton.GetComponent<BotonGeneric>().SetText(Texts.Instance.GetText("waitingForPlayers"));
        if (!NetworkManager.Instance.IsMaster())
          return;
        this.manualReadyCo = this.StartCoroutine(this.CheckForAllManualReady());
      }
      else
      {
        this.exitCraftButton.GetComponent<BotonGeneric>().SetBackgroundColor(Functions.HexToColor(Globals.Instance.ClassColor["warrior"]));
        this.exitCraftButton.GetComponent<BotonGeneric>().SetText(Texts.Instance.GetText("exit"));
      }
    }
  }

  private IEnumerator CheckForAllManualReady()
  {
    bool check = true;
    while (check)
    {
      if (!NetworkManager.Instance.AllPlayersManualReady())
        yield return (object) Globals.Instance.WaitForSeconds(1f);
      else
        check = false;
    }
    this.photonView.RPC("NET_CloseCardCraft", RpcTarget.Others);
    this.ExitCardCraft();
  }

  private void ActivateButtonCorruption()
  {
    if ((UnityEngine.Object) this.CICorruption == (UnityEngine.Object) null)
      this.BG_Corruption.Disable();
    else if (this.CanBuy("Corruption"))
      this.BG_Corruption.Enable();
    else
      this.BG_Corruption.Disable();
  }

  public void InitCorruption()
  {
    this.RemoveCorruptionCards();
    this.DeactivateCorruptions();
    this.ActivateButtonCorruption();
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<align=center><size=24>");
    stringBuilder.Append(string.Format(Texts.Instance.GetText("corruptionSacrifyDust"), (object) 400, (object) 10));
    this.corruptionButtons[0].SetPopupText(stringBuilder.ToString());
    stringBuilder.Clear();
    stringBuilder.Append("<sprite name=dust> -");
    stringBuilder.Append(400);
    this.corruptionButtons[0].SetText(stringBuilder.ToString());
    stringBuilder.Clear();
    stringBuilder.Append("<align=center><size=24>");
    stringBuilder.Append(string.Format(Texts.Instance.GetText("corruptionSacrifySpeed"), (object) 2, (object) 10));
    this.corruptionButtons[1].SetPopupText(stringBuilder.ToString());
    stringBuilder.Clear();
    stringBuilder.Append("<sprite name=speedMini> -");
    stringBuilder.Append(2);
    this.corruptionButtons[1].SetText(stringBuilder.ToString());
    stringBuilder.Clear();
    stringBuilder.Append("<align=center><size=24>");
    stringBuilder.Append(string.Format(Texts.Instance.GetText("corruptionSacrifyHealth"), (object) 20, (object) 10));
    this.corruptionButtons[2].SetPopupText(stringBuilder.ToString());
    stringBuilder.Clear();
    stringBuilder.Append("<sprite name=heart> -");
    stringBuilder.Append(20);
    this.corruptionButtons[2].SetText(stringBuilder.ToString());
    stringBuilder.Clear();
    stringBuilder.Append("<align=center><size=24>");
    stringBuilder.Append(string.Format(Texts.Instance.GetText("corruptionSacrifyResistance"), (object) 10, (object) 10));
    this.corruptionButtons[3].SetPopupText(stringBuilder.ToString());
    stringBuilder.Clear();
    stringBuilder.Append("<sprite name=ui_resistance> -");
    stringBuilder.Append(10);
    this.corruptionButtons[3].SetText(stringBuilder.ToString());
  }

  private void CheckForCorruptableCards(CardVertical _cardVertical)
  {
    if (_cardVertical.cardData.CardClass == Enums.CardClass.Injury || _cardVertical.cardData.CardClass == Enums.CardClass.Boon || _cardVertical.cardData.CardUpgraded == Enums.CardUpgraded.Rare || _cardVertical.cardData.CardClass == Enums.CardClass.Special)
      _cardVertical.ShowLock(true, false);
    else
      _cardVertical.ShowLock(false);
  }

  private void DeactivateCorruptions()
  {
    for (int _corruptionNum = 0; _corruptionNum < 4; ++_corruptionNum)
      this.ActivateCorruption(_corruptionNum, false);
    this.StartCoroutine(this.IncrementCorruption(int.Parse(this.corruptionPercent.text.Replace("%", "")), this.CalculateCorruption()));
  }

  public void DoButtonCorruption(int _corruptionNum)
  {
    if (this.IsCorruptionEnabled(_corruptionNum))
      this.ActivateCorruption(_corruptionNum, false);
    else
      this.ActivateCorruption(_corruptionNum);
    this.StartCoroutine(this.IncrementCorruption(int.Parse(this.corruptionPercent.text.Replace("%", "")), this.CalculateCorruption()));
  }

  public void ActivateCorruption(int _corruptionNum, bool _state = true)
  {
    if (_state && (UnityEngine.Object) this.CICorruption == (UnityEngine.Object) null)
      return;
    this.corruptionArrows[_corruptionNum].gameObject.SetActive(_state);
    Color color = this.corruptionButtons[_corruptionNum].color;
    if (_state)
    {
      Color _color = new Color(color.r, color.g, color.b, 1f);
      this.corruptionTexts[_corruptionNum].color = _color;
      this.corruptionButtons[_corruptionNum].SetColorAbsolute(_color);
    }
    else
    {
      Color _color = new Color(color.r, color.g, color.b, 0.3f);
      this.corruptionTexts[_corruptionNum].color = _color;
      this.corruptionButtons[_corruptionNum].SetColorAbsolute(_color);
    }
    this.BG_Corruption.SetText(this.ButtonText(this.SetPrice("Corruption", "")));
    this.ActivateButtonCorruption();
  }

  private bool IsCorruptionEnabled(int _index) => this.corruptionArrows[_index].gameObject.activeSelf;

  public int CalculateCorruption()
  {
    if (this.corruptionTry[this.heroIndex] > 2)
      return 0;
    this.corruptionValue = Globals.Instance.CorruptionBasePercent[this.corruptionTry[this.heroIndex]];
    for (int _index = 0; _index < 4; ++_index)
    {
      if (this.IsCorruptionEnabled(_index))
        this.corruptionValue += 10;
    }
    if (this.corruptionValue > 100)
      this.corruptionValue = 100;
    else if (this.corruptionValue < 0)
      this.corruptionValue = 0;
    return this.corruptionValue;
  }

  private IEnumerator IncrementCorruption(int _begin, int _end)
  {
    if (_begin != _end)
    {
      bool goingUp = _begin < _end;
      int value = _begin;
      while (goingUp && value < _end)
      {
        value += 2;
        this.corruptionPercent.text = value.ToString() + "%";
        yield return (object) null;
      }
      while (!goingUp && value > _end)
      {
        value -= 2;
        this.corruptionPercent.text = value.ToString() + "%";
        yield return (object) null;
      }
      this.corruptionPercent.text = _end.ToString() + "%";
    }
  }

  private IEnumerator CorruptionRollForChance(int _chanceRolled, int _chance)
  {
    for (int i = 0; i < 100; ++i)
    {
      int num = UnityEngine.Random.Range(0, 100);
      this.corruptionPercentRoll.text = num >= 10 ? num.ToString() + "%" : "0" + num.ToString() + "%";
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    }
    this.corruptionPercentRoll.text = _chanceRolled.ToString() + "%";
    if (_chanceRolled <= _chance)
      this.corruptionPercentRollSuccess.gameObject.SetActive(true);
    else
      this.corruptionPercentRollFail.gameObject.SetActive(true);
  }

  public void CorruptCard()
  {
    if (!this.CanBuy("Corruption"))
      return;
    this.craftCoroutine = this.StartCoroutine(this.BuyCorruptionCo());
  }

  private IEnumerator BuyCorruptionCo()
  {
    CardCraftManager cardCraftManager = this;
    if (!cardCraftManager.blocked && !((UnityEngine.Object) cardCraftManager.CICorruption == (UnityEngine.Object) null))
    {
      cardCraftManager.SetBlocked(true);
      cardCraftManager.BG_Corruption.Disable();
      int cardIndex = int.Parse(cardCraftManager.CICorruption.transform.gameObject.name.Split('_', StringSplitOptions.None)[2]);
      AtOManager.Instance.PayDust(cardCraftManager.SetPrice("Corruption", ""));
      AtOManager.Instance.SubstractCraftReaminingUses(cardCraftManager.heroIndex);
      if (cardCraftManager.IsCorruptionEnabled(1))
        AtOManager.Instance.AddPerkToHeroGlobal(cardCraftManager.heroIndex, "perkcorruptionspeed" + cardCraftManager.corruptionTry[cardCraftManager.heroIndex].ToString());
      if (cardCraftManager.IsCorruptionEnabled(2))
        AtOManager.Instance.AddPerkToHeroGlobal(cardCraftManager.heroIndex, "perkcorruptionhealth" + cardCraftManager.corruptionTry[cardCraftManager.heroIndex].ToString());
      if (cardCraftManager.IsCorruptionEnabled(3))
        AtOManager.Instance.AddPerkToHeroGlobal(cardCraftManager.heroIndex, "perkcorruptionresist" + cardCraftManager.corruptionTry[cardCraftManager.heroIndex].ToString());
      int corruption = cardCraftManager.CalculateCorruption();
      CardData cardDataAux = (CardData) null;
      bool success = false;
      if (corruption < 100)
      {
        int _chanceRolled = UnityEngine.Random.Range(0, 100);
        cardCraftManager.StartCoroutine(cardCraftManager.CorruptionRollForChance(_chanceRolled, corruption));
        if (_chanceRolled <= corruption)
          success = true;
        yield return (object) Globals.Instance.WaitForSeconds(2.5f);
      }
      else
        success = true;
      if (success)
      {
        cardDataAux = Functions.GetCardDataFromCardData(cardCraftManager.cardData, "RARE");
      }
      else
      {
        cardDataAux = Globals.Instance.GetCardData("voidmemory");
        PlayerManager.Instance.CardUnlock("voidmemory");
      }
      AtOManager.Instance.ReplaceCardInDeck(cardCraftManager.heroIndex, cardIndex, cardDataAux.Id);
      ++cardCraftManager.corruptionTry[cardCraftManager.heroIndex];
      cardCraftManager.ShowRemainingUses();
      cardCraftManager.SetControllerIntoVerticalList();
      AtOManager.Instance.HeroCraftUpgraded(cardCraftManager.heroIndex);
      AtOManager.Instance.SideBarRefreshCards(cardCraftManager.heroIndex);
      cardCraftManager.CICorruption.cardrevealed = true;
      cardCraftManager.CICorruption.EnableTrail();
      cardCraftManager.CICorruption.TopLayeringOrder("UI", -2000);
      cardCraftManager.CICorruption.PlayDissolveParticle();
      cardCraftManager.CICorruption.SetDestinationScaleRotation(cardCraftManager.deckGOs[cardIndex].transform.position + new Vector3(0.0f, -1f, 0.0f), 0.0f, Quaternion.Euler(0.0f, 0.0f, 180f));
      yield return (object) Globals.Instance.WaitForSeconds(0.2f);
      cardCraftManager.cardVerticalDeck[cardIndex].ReplaceWithCard(cardDataAux, "RARE");
      cardCraftManager.cardVerticalDeck[cardIndex].ShowLock(true, false);
      cardCraftManager.corruptAnim.SetTrigger("hide");
      cardCraftManager.RemoveCorruptionCards();
      cardCraftManager.ActivateButtonCorruption();
      cardCraftManager.DeactivateCorruptions();
      cardCraftManager.SetBlocked(false);
    }
  }

  private void SetControllerIntoVerticalList()
  {
    if (Gamepad.current == null || this.controllerHorizontalIndex == -1)
      return;
    this.controllerHorizontalIndex = Functions.GetTransformIndexInList(this._controllerList, this.cardListContainer.gameObject.name);
    this.controllerIsOnVerticalList = true;
    this.ControllerMovement(true, absolutePosition: this.controllerVerticalIndex);
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    int absolutePosition = -1)
  {
    if (goingUp | goingDown && this.controllerIsOnVerticalList)
    {
      if (!((UnityEngine.Object) this.cardListContainer != (UnityEngine.Object) null))
        return;
      int childCount = this.cardListContainer.childCount;
      if (childCount > 0)
      {
        bool flag = false;
        int num = 0;
        while (!flag && num < 20)
        {
          if (absolutePosition > -1)
            this.controllerVerticalIndex = absolutePosition;
          else if (goingUp)
          {
            --this.controllerVerticalIndex;
            if (this.controllerVerticalIndex < 0)
              this.controllerVerticalIndex = 0;
          }
          else
          {
            ++this.controllerVerticalIndex;
            if (this.controllerVerticalIndex > childCount - 1)
              this.controllerVerticalIndex = childCount - 1;
          }
          if (this.cardListContainer.childCount > this.controllerVerticalIndex && (UnityEngine.Object) this.cardListContainer.GetChild(this.controllerVerticalIndex) != (UnityEngine.Object) null && !this.cardListContainer.GetChild(this.controllerVerticalIndex).GetComponent<CardVertical>().IsLocked())
            flag = true;
          else
            ++num;
        }
        Canvas.ForceUpdateCanvases();
        if (childCount > 15)
        {
          if (this.controllerVerticalIndex > 8)
          {
            this.auxVector.x = 0.5f;
            this.auxVector.y = (float) (1.0 / 400.0 * (double) this.controllerVerticalIndex - 3.6600000858306885);
            if ((UnityEngine.Object) this.cardListContainerRectTransform.GetChild(0) != (UnityEngine.Object) null && (UnityEngine.Object) this.cardListContainer.GetChild(this.controllerVerticalIndex) != (UnityEngine.Object) null)
              this.cardListContainer.GetComponent<RectTransform>().anchoredPosition = (Vector2) this.cardListContainerRectTransform.GetChild(0).transform.InverseTransformPoint(this.cardListContainer.position + this.auxVector) - (Vector2) this.cardListContainerRectTransform.GetChild(0).transform.InverseTransformPoint(new Vector3(-4.9f, this.cardListContainer.GetChild(this.controllerVerticalIndex).position.y, 0.0f));
          }
          else
          {
            this.auxVector.x = 0.5f;
            this.auxVector.y = (float) (-3.0 - (double) (childCount - 15) * 0.20000000298023224);
            this.cardListContainer.GetComponent<RectTransform>().anchoredPosition = (Vector2) this.auxVector;
          }
        }
        else
        {
          this.auxVector.x = 0.5f;
          this.auxVector.y = -3f;
          this.cardListContainer.GetComponent<RectTransform>().anchoredPosition = (Vector2) this.auxVector;
        }
        this.auxVector.x = 1f;
        this.auxVector.y = 0.0f;
        if ((UnityEngine.Object) this.cardListContainer.GetChild(this.controllerVerticalIndex) != (UnityEngine.Object) null)
        {
          this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.cardListContainer.GetChild(this.controllerVerticalIndex).position + this.auxVector);
          Mouse.current.WarpCursorPosition(this.warpPosition);
        }
      }
      if (this.craftType == 2)
        return;
      this.controllerHorizontalIndex = 0;
    }
    else
    {
      this.controllerIsOnVerticalList = false;
      this._controllerList.Clear();
      if (this.craftType == 0)
      {
        this._controllerList.Add(this.cardListContainer);
        if (Functions.TransformIsVisible(this.buttonL))
          this._controllerList.Add(this.buttonL);
        if (Functions.TransformIsVisible(this.buttonR))
          this._controllerList.Add(this.buttonR);
        this._controllerList.Add(this.exitCraftButton);
        for (int index = 0; index < 4; ++index)
        {
          if ((bool) (UnityEngine.Object) TownManager.Instance)
          {
            if (Functions.TransformIsVisible(TownManager.Instance.sideCharacters.charArray[index].transform))
              this._controllerList.Add(TownManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
          }
          else if (Functions.TransformIsVisible(MapManager.Instance.sideCharacters.charArray[index].transform))
            this._controllerList.Add(MapManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
        }
        if (Functions.TransformIsVisible(PlayerUIManager.Instance.giveGold))
          this._controllerList.Add(PlayerUIManager.Instance.giveGold);
        if (this.controllerHorizontalIndex == -1 || this.controllerHorizontalIndex < this._controllerList.Count && (UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) this.cardListContainer)
          this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
        this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
        if ((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] == (UnityEngine.Object) this.cardListContainer)
        {
          this.controllerIsOnVerticalList = true;
          this.ControllerMovement(true, absolutePosition: this.controllerVerticalIndex);
        }
        else
        {
          this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
          Mouse.current.WarpCursorPosition(this.warpPosition);
        }
      }
      else if (this.craftType == 1)
      {
        this._controllerList.Add(this.cardListContainer);
        if (Functions.TransformIsVisible(this.buttonRemove))
          this._controllerList.Add(this.buttonRemove);
        this._controllerList.Add(this.exitCraftButton);
        for (int index = 0; index < 4; ++index)
        {
          if ((bool) (UnityEngine.Object) TownManager.Instance)
          {
            if (Functions.TransformIsVisible(TownManager.Instance.sideCharacters.charArray[index].transform))
              this._controllerList.Add(TownManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
          }
          else if (Functions.TransformIsVisible(MapManager.Instance.sideCharacters.charArray[index].transform))
            this._controllerList.Add(MapManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
        }
        if (Functions.TransformIsVisible(PlayerUIManager.Instance.giveGold))
          this._controllerList.Add(PlayerUIManager.Instance.giveGold);
        if (this.controllerHorizontalIndex == -1 || (UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) this.cardListContainer)
          this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
        this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
        if ((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] == (UnityEngine.Object) this.cardListContainer)
        {
          this.controllerIsOnVerticalList = true;
          this.ControllerMovement(true, absolutePosition: this.controllerVerticalIndex);
        }
        else
        {
          this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
          Mouse.current.WarpCursorPosition(this.warpPosition);
        }
      }
      else if (this.craftType == 2)
      {
        if (this.filterWindow.gameObject.activeSelf)
        {
          for (int index = 0; index < this.craftBotonFilters.Count; ++index)
            this._controllerList.Add(this.craftBotonFilters[index].transform);
          this._controllerList.Add(this.filterWindow.GetChild(8));
          this._controllerList.Add(this.filterWindow.GetChild(7));
          this._controllerList.Add(this.filterWindow.GetChild(6));
          this.controllerVerticalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
          this.controllerVerticalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerVerticalIndex, goingUp, goingRight, goingDown, goingLeft);
          if (!((UnityEngine.Object) this._controllerList[this.controllerVerticalIndex] != (UnityEngine.Object) null))
            return;
          this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerVerticalIndex].position);
          Mouse.current.WarpCursorPosition(this.warpPosition);
        }
        else
        {
          if (!Functions.TransformIsVisible(this.cardCraftSave.transform))
          {
            foreach (Transform transform in this.cardCraftContainer)
            {
              if ((bool) (UnityEngine.Object) transform.GetComponent<CardCraftItem>() && Functions.TransformIsVisible(transform))
              {
                this._controllerList.Add(transform.GetChild(1).transform.GetChild(0).transform);
                this._controllerList.Add(transform.GetChild(2).transform);
              }
            }
            foreach (Transform transform in this.cardCraftEnergySelectorContainer)
            {
              if ((bool) (UnityEngine.Object) transform.GetComponent<CardCraftSelectorEnergy>())
                this._controllerList.Add(transform);
            }
            foreach (Transform transform in this.cardCraftRaritySelectorContainer)
            {
              if ((bool) (UnityEngine.Object) transform.GetComponent<CardCraftSelectorRarity>())
                this._controllerList.Add(transform);
            }
            this._controllerList.Add(this.buttonAffordableCraft.transform);
            this._controllerList.Add(this.buttonAdvancedCraft.transform);
            this._controllerList.Add(this.buttonFilterCraft.transform);
            foreach (Transform transform in this.cardCraftPageContainer)
              this._controllerList.Add(transform);
            this._controllerList.Add(this.searchInput.transform);
            if (Functions.TransformIsVisible(this.canvasSearchCloseT))
              this._controllerList.Add(this.canvasSearchCloseT);
          }
          else
          {
            for (int index = 0; index < this.deckSlot.Length; ++index)
            {
              if (this.deckSlot[index].transform.GetComponent<BoxCollider2D>().enabled)
              {
                this._controllerList.Add(this.deckSlot[index].transform);
                this._controllerList.Add(this.deckSlot[index].transform.GetChild(5).transform);
              }
              else
                this._controllerList.Add(this.deckSlot[index].transform.GetChild(4).transform);
            }
          }
          this._controllerList.Add(this.botSaveLoad.transform);
          this._controllerList.Add(this.exitCraftButton);
          this._controllerList.Add(this.cardListContainer);
          for (int index = 0; index < 4; ++index)
          {
            if ((bool) (UnityEngine.Object) TownManager.Instance)
            {
              if (Functions.TransformIsVisible(TownManager.Instance.sideCharacters.charArray[index].transform))
                this._controllerList.Add(TownManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
            }
            else if (Functions.TransformIsVisible(MapManager.Instance.sideCharacters.charArray[index].transform))
              this._controllerList.Add(MapManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
          }
          if (Functions.TransformIsVisible(PlayerUIManager.Instance.giveGold))
            this._controllerList.Add(PlayerUIManager.Instance.giveGold);
          if (this.controllerHorizontalIndex == -1 || this.controllerHorizontalIndex >= this._controllerList.Count || (UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) this.cardListContainer)
            this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
          this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
          if ((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] == (UnityEngine.Object) this.cardListContainer)
          {
            this.controllerIsOnVerticalList = true;
            this.ControllerMovement(true, absolutePosition: this.controllerVerticalIndex);
          }
          else
          {
            this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
            Mouse.current.WarpCursorPosition(this.warpPosition);
          }
        }
      }
      else if (this.craftType == 3)
      {
        foreach (Transform transform in this.divinationButtonContainer)
        {
          if (Functions.TransformIsVisible(transform))
            this._controllerList.Add(transform);
        }
        this._controllerList.Add(this.exitCraftButton);
        this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
        this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
        if (!((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
          return;
        this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
        Mouse.current.WarpCursorPosition(this.warpPosition);
      }
      else if (this.craftType == 4)
      {
        foreach (Transform transform in this.cardItemContainer)
        {
          if (Functions.TransformIsVisible(transform) && (bool) (UnityEngine.Object) transform.GetComponent<CardCraftItem>())
          {
            this._controllerList.Add(transform.GetChild(1).transform.GetChild(0).transform);
            this._controllerList.Add(transform.GetChild(3).transform);
          }
        }
        this._controllerList.Add(this.exitCraftButton);
        if (Functions.TransformIsVisible(this.itemShopButton.transform))
          this._controllerList.Add(this.itemShopButton.transform);
        if (Functions.TransformIsVisible(this.petShopButton.transform))
          this._controllerList.Add(this.petShopButton.transform);
        if (Functions.TransformIsVisible(this.rerollButton.transform))
          this._controllerList.Add(this.rerollButton.transform);
        if (this.iconWeapon.transform.GetChild(0).gameObject.activeSelf)
          this._controllerList.Add(this.iconWeapon.transform);
        if (this.iconArmor.transform.GetChild(0).gameObject.activeSelf)
          this._controllerList.Add(this.iconArmor.transform);
        if (this.iconJewelry.transform.GetChild(0).gameObject.activeSelf)
          this._controllerList.Add(this.iconJewelry.transform);
        if (this.iconAccesory.transform.GetChild(0).gameObject.activeSelf)
          this._controllerList.Add(this.iconAccesory.transform);
        if (this.iconPet.transform.GetChild(0).gameObject.activeSelf)
          this._controllerList.Add(this.iconPet.transform);
        for (int index = 0; index < 4; ++index)
        {
          if ((bool) (UnityEngine.Object) TownManager.Instance)
          {
            if (Functions.TransformIsVisible(TownManager.Instance.sideCharacters.charArray[index].transform))
              this._controllerList.Add(TownManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
          }
          else if (Functions.TransformIsVisible(MapManager.Instance.sideCharacters.charArray[index].transform))
            this._controllerList.Add(MapManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
        }
        if (Functions.TransformIsVisible(PlayerUIManager.Instance.giveGold))
          this._controllerList.Add(PlayerUIManager.Instance.giveGold);
        foreach (Transform transform in this.itemsCraftPageContainer)
          this._controllerList.Add(transform);
        this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
        this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
        if (!((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
          return;
        this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
        Mouse.current.WarpCursorPosition(this.warpPosition);
      }
      else if (this.craftType == 6)
      {
        this._controllerList.Add(this.buttonCorruption);
        for (int index = 0; index < 4; ++index)
          this._controllerList.Add(this.corruptionButtons[index].transform);
        this._controllerList.Add(this.corruptionCharacterStats);
        this._controllerList.Add(this.cardListContainer);
        this._controllerList.Add(this.exitCraftButton);
        for (int index = 0; index < 4; ++index)
        {
          if ((bool) (UnityEngine.Object) TownManager.Instance)
          {
            if (Functions.TransformIsVisible(TownManager.Instance.sideCharacters.charArray[index].transform))
              this._controllerList.Add(TownManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
          }
          else if (Functions.TransformIsVisible(MapManager.Instance.sideCharacters.charArray[index].transform))
            this._controllerList.Add(MapManager.Instance.sideCharacters.charArray[index].transform.GetChild(0).transform);
        }
        if (Functions.TransformIsVisible(PlayerUIManager.Instance.giveGold))
          this._controllerList.Add(PlayerUIManager.Instance.giveGold);
        if (this.controllerHorizontalIndex == -1 || (UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) this.cardListContainer)
          this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
        this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
        if ((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] == (UnityEngine.Object) this.cardListContainer)
        {
          this.controllerIsOnVerticalList = true;
          this.ControllerMovement(true, absolutePosition: this.controllerVerticalIndex);
        }
        else
        {
          this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
          Mouse.current.WarpCursorPosition(this.warpPosition);
        }
      }
      else
      {
        if (this.craftType != 5)
          return;
        this._controllerVerticalList.Clear();
        if (Functions.TransformIsVisible(this.challengePerks))
        {
          for (int index = 0; index < this.perkChallengeItems.Length; ++index)
          {
            if (Functions.TransformIsVisible(this.perkChallengeItems[index].transform))
              this._controllerList.Add(this.perkChallengeItems[index].transform);
          }
        }
        else if (this.tmpContainer.childCount > 0)
        {
          for (int index = 0; index < 3; ++index)
          {
            if ((UnityEngine.Object) this.tmpContainer.GetChild(index) != (UnityEngine.Object) null)
            {
              this._controllerList.Add(this.tmpContainer.GetChild(index).transform);
              this._controllerList.Add(this.cardChallengeButton[index].transform);
            }
          }
        }
        else
        {
          for (int index = 0; index < this.cardChallengeContainer.Length; ++index)
          {
            foreach (Transform transform in this.cardChallengeContainer[index])
            {
              if ((bool) (UnityEngine.Object) transform.GetComponent<CardVertical>())
                this._controllerList.Add(transform);
            }
            this._controllerList.Add(this.cardChallengeSelected[index].transform);
          }
        }
        foreach (Transform transform in this.cardListContainer)
          this._controllerVerticalList.Add(transform);
        if (this.controllerHorizontalIndex == -1 && this.controllerBlock == -1)
        {
          this.controllerHorizontalIndex = 0;
          this.controllerBlock = 0;
        }
        else if (this.controllerBlock == 0)
        {
          if (Functions.TransformIsVisible(this.challengePerks))
          {
            if (goingUp)
            {
              if (this.controllerHorizontalIndex > 7)
                this.controllerHorizontalIndex = Functions.GetClosestIndexFromList(this._controllerList[this.controllerHorizontalIndex], this._controllerList, this.controllerHorizontalIndex, new Vector3(0.0f, 0.5f, 0.0f));
            }
            else if (goingDown)
            {
              if (this._controllerList.Count == 28 && this.controllerHorizontalIndex > 20 || this._controllerList.Count == 21 && this.controllerHorizontalIndex > 13)
              {
                this.controllerBlock = 2;
                this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.readyChallenge.position);
                Mouse.current.WarpCursorPosition(this.warpPosition);
                return;
              }
              this.controllerHorizontalIndex = Functions.GetClosestIndexFromList(this._controllerList[this.controllerHorizontalIndex], this._controllerList, this.controllerHorizontalIndex, new Vector3(0.0f, -0.5f, 0.0f));
            }
            else if (goingRight)
            {
              if (this.controllerHorizontalIndex != 6 && this.controllerHorizontalIndex != 13 && this.controllerHorizontalIndex != 19 && this.controllerHorizontalIndex != 25)
                this.controllerHorizontalIndex = Functions.GetClosestIndexFromList(this._controllerList[this.controllerHorizontalIndex], this._controllerList, this.controllerHorizontalIndex, new Vector3(1.5f, 0.0f, 0.0f));
            }
            else
            {
              if (this.controllerHorizontalIndex == 0 || this.controllerHorizontalIndex == 7 || this.controllerHorizontalIndex == 14 || this.controllerHorizontalIndex == 21)
              {
                this.controllerBlock = 3;
                this.controllerHorizontalIndex = 0;
                this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerVerticalList[0].position);
                Mouse.current.WarpCursorPosition(this.warpPosition);
                return;
              }
              this.controllerHorizontalIndex = Functions.GetClosestIndexFromList(this._controllerList[this.controllerHorizontalIndex], this._controllerList, this.controllerHorizontalIndex, new Vector3(-1.5f, 0.0f, 0.0f));
            }
          }
          else if (this.tmpContainer.childCount > 0)
          {
            if (goingUp)
            {
              if (this.controllerHorizontalIndex % 2 == 1)
                --this.controllerHorizontalIndex;
            }
            else if (goingDown)
            {
              if (this.controllerHorizontalIndex % 2 == 0)
              {
                ++this.controllerHorizontalIndex;
              }
              else
              {
                this.controllerBlock = 2;
                this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.readyChallenge.position);
                Mouse.current.WarpCursorPosition(this.warpPosition);
                return;
              }
            }
            else if (goingLeft)
            {
              if (this.controllerHorizontalIndex == 0 || this.controllerHorizontalIndex == 1)
              {
                this.controllerBlock = 3;
                this.controllerHorizontalIndex = 0;
                this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerVerticalList[0].position);
                Mouse.current.WarpCursorPosition(this.warpPosition);
                return;
              }
              this.controllerHorizontalIndex -= 2;
            }
            else if (goingRight && this.controllerHorizontalIndex != 4 && this.controllerHorizontalIndex != 5)
              this.controllerHorizontalIndex += 2;
          }
          else if (goingUp)
          {
            if (this.controllerHorizontalIndex % 4 == 0)
            {
              if (this.controllerHorizontalIndex < 16)
              {
                this.controllerBlock = 1;
                this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.rerollChallenge.transform.position);
                Mouse.current.WarpCursorPosition(this.warpPosition);
                return;
              }
              this.controllerHorizontalIndex -= 13;
            }
            else
              --this.controllerHorizontalIndex;
          }
          else if (goingDown)
          {
            if (this.controllerHorizontalIndex % 4 == 3)
            {
              if (this.controllerHorizontalIndex > 16)
              {
                this.controllerBlock = 2;
                this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.readyChallenge.position);
                Mouse.current.WarpCursorPosition(this.warpPosition);
                return;
              }
              this.controllerHorizontalIndex += 13;
            }
            else
              ++this.controllerHorizontalIndex;
          }
          else if (goingRight)
          {
            if ((this.controllerHorizontalIndex < 12 || this.controllerHorizontalIndex >= 16) && (this.controllerHorizontalIndex < 28 || this.controllerHorizontalIndex >= 32))
              this.controllerHorizontalIndex += 4;
          }
          else if (goingLeft)
          {
            if (this.controllerHorizontalIndex >= 0 && this.controllerHorizontalIndex < 4 || this.controllerHorizontalIndex >= 16 && this.controllerHorizontalIndex < 20)
            {
              this.controllerBlock = 3;
              this.controllerHorizontalIndex = 0;
              this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerVerticalList[0].position);
              Mouse.current.WarpCursorPosition(this.warpPosition);
              return;
            }
            this.controllerHorizontalIndex -= 4;
          }
        }
        else if (this.controllerBlock == 1)
        {
          if (!(goingLeft | goingDown))
            return;
          this.controllerHorizontalIndex = 12;
          this.controllerBlock = 0;
        }
        else if (this.controllerBlock == 2)
        {
          if (!(goingLeft | goingUp))
            return;
          this.controllerHorizontalIndex = this._controllerList.Count - 1;
          this.controllerBlock = 0;
        }
        else
        {
          if (this.controllerBlock == 3)
          {
            if (goingDown)
              ++this.controllerHorizontalIndex;
            else if (goingUp)
            {
              --this.controllerHorizontalIndex;
            }
            else
            {
              if (goingLeft)
              {
                this.controllerBlock = 4;
                this.controllerHorizontalIndex = 0;
                this.controllerVerticalIndex = this.heroIndex;
                this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(ChallengeSelectionManager.Instance.sideCharacters.charArray[this.controllerVerticalIndex].transform.position);
                Mouse.current.WarpCursorPosition(this.warpPosition);
                return;
              }
              if (goingRight)
              {
                this.controllerBlock = 0;
                this.controllerHorizontalIndex = 0;
                this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
                Mouse.current.WarpCursorPosition(this.warpPosition);
                return;
              }
            }
            if (this.controllerHorizontalIndex > this._controllerVerticalList.Count - 1)
              this.controllerHorizontalIndex = this._controllerVerticalList.Count - 1;
            else if (this.controllerHorizontalIndex < 0)
              this.controllerHorizontalIndex = 0;
            this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerVerticalList[this.controllerHorizontalIndex].position);
            Mouse.current.WarpCursorPosition(this.warpPosition);
            return;
          }
          if (this.controllerBlock == 4)
          {
            if (goingDown)
              ++this.controllerVerticalIndex;
            else if (goingUp)
            {
              --this.controllerVerticalIndex;
            }
            else
            {
              if (!goingRight)
                return;
              this.controllerBlock = 3;
              this.controllerHorizontalIndex = 0;
              this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerVerticalList[this.controllerHorizontalIndex].position);
              Mouse.current.WarpCursorPosition(this.warpPosition);
              return;
            }
            if (this.controllerVerticalIndex > 3)
              this.controllerVerticalIndex = 3;
            else if (this.controllerVerticalIndex < 0)
              this.controllerVerticalIndex = 0;
            this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(ChallengeSelectionManager.Instance.sideCharacters.charArray[this.controllerVerticalIndex].transform.position);
            Mouse.current.WarpCursorPosition(this.warpPosition);
            return;
          }
        }
        if (this.controllerHorizontalIndex <= -1 || !((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
          return;
        this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
        Mouse.current.WarpCursorPosition(this.warpPosition);
      }
    }
  }

  public void ControllerMoveShoulder(bool _isRight = false)
  {
    if (this.craftType == 2 || this.craftType == 4)
    {
      if (_isRight)
        this.DoNextPage();
      else
        this.DoPrevPage();
    }
    else
    {
      if (this.craftType != 5)
        return;
      ChallengeSelectionManager.Instance.NextHeroFunc(_isRight);
    }
  }

  public void ControllerNextPage(bool _isNext = true)
  {
    if (this.craftType != 2 && this.craftType != 4)
      return;
    if (_isNext)
      this.DoNextPage();
    else
      this.DoPrevPage();
  }
}
