// Decompiled with JetBrains decompiler
// Type: TomeManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Steamworks.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TomeManager : MonoBehaviour
{
  public GameObject content;
  public GameObject unlockedPiece;
  public Transform exitButton;
  public Transform cardsMask;
  private Coroutine cardsMaskCo;
  public Transform mainSection;
  public Transform mainSectionButtonT;
  private BotonGeneric mainSectionButton;
  public Transform cardsSection;
  public Transform cardsSectionButtonT;
  private BotonGeneric cardsSectionButton;
  public Transform itemsSection;
  public Transform itemsSectionButtonT;
  private BotonGeneric itemsSectionButton;
  public Transform challengeSection;
  public Transform challengeSectionButtonT;
  private BotonGeneric challengeSectionButton;
  public Transform monsterSection;
  public Transform monsterSectionButtonT;
  private BotonGeneric monsterSectionButton;
  public Transform scoreboardSection;
  public Transform scoreboardSectionButtonT;
  private BotonGeneric scoreboardSectionButton;
  public Transform glossarySection;
  public Transform glossarySectionButtonT;
  private BotonGeneric glossarySectionButton;
  public Transform glossaryIndex;
  public Transform glossaryPageIndex;
  public Transform glossaryPageIndex2;
  public Transform glossaryPageLeft;
  public Transform glossaryPageRight;
  public GameObject glossaryIndexItem;
  private int glossaryTermSelected = -1;
  private SortedDictionary<string, string> glossaryTerms;
  private Dictionary<string, string> glossaryTermsTitle;
  public TMP_Text[] glossaryTexts;
  private Coroutine glossaryCo;
  public Transform tomeSideButtons;
  public Transform scores;
  public Transform runsSection;
  public Transform runsSectionButtonT;
  private BotonGeneric runsSectionButton;
  public Transform page;
  private Animator pageAnim;
  public TomeButton[] TomeButtons;
  public Transform cardContainer;
  public GameObject TomeCard;
  public GameObject TomeNumber;
  public Transform Paginator;
  public GameObject MoreNumbersIcon;
  public TMP_Text[] statisticsTexts;
  public UnlockedBar[] unlockedBars;
  private global::TomeCard[] tomeCards;
  private Transform[] tomeTs;
  private GameObject[] cardGO;
  private GameObject[] cardGOBlue;
  private GameObject[] cardGOGold;
  private GameObject[] cardGORare;
  private global::TomeNumber[] tomeNumbers = new global::TomeNumber[30];
  private int pageAct;
  private int pageMax = 1;
  private int pageOld;
  private int numCards = 18;
  private int maxTomeNumbers = 30;
  private int activeTomeCards = -100;
  private List<string> cardList = new List<string>();
  private Coroutine SetTomeCardsCo;
  public Transform searchBox;
  public TMP_InputField searchInput;
  public TMP_Text searchInputPlaceholder;
  public TextMeshProUGUI searchInputPlaceholderGUI;
  public TMP_Text searchInputText;
  public Transform canvasSearchCloseT;
  private string searchTerm = "";
  private Coroutine searchCo;
  public Score[] scoresName;
  private Coroutine SetScoresCo;
  private Coroutine ShowCoroutine;
  private int numScoreRows = 20;
  private int scoreboardType;
  private int currentScoreboard;
  private int week;
  public bool playerOnScoreboard;
  public TMP_Text scoreTitle;
  public TMP_Text scoreTitleSub;
  public TMP_Text scoreStatus;
  public Transform[] scoreButtons;
  public BotonGeneric buttonPrevWeekly;
  public BotonGeneric buttonNextWeekly;
  public List<PlayerRun> playerRunsList;
  public GameObject run;
  public Transform runsContainer;
  public Transform runDetails;
  private int activeRun = -1;
  public Transform runPathMask;
  private int numRunsPerPage = 8;
  public int controllerHorizontalIndex = -1;
  private Vector2 warpPosition = Vector2.zero;
  private List<Transform> _controllerList = new List<Transform>();

  public static TomeManager Instance { get; private set; }

  private void Awake()
  {
    if ((UnityEngine.Object) TomeManager.Instance == (UnityEngine.Object) null)
      TomeManager.Instance = this;
    else if ((UnityEngine.Object) TomeManager.Instance != (UnityEngine.Object) this)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    this.pageAnim = this.page.GetComponent<Animator>();
    this.mainSectionButton = this.mainSectionButtonT.GetComponent<BotonGeneric>();
    this.cardsSectionButton = this.cardsSectionButtonT.GetComponent<BotonGeneric>();
    this.itemsSectionButton = this.itemsSectionButtonT.GetComponent<BotonGeneric>();
    this.challengeSectionButton = this.challengeSectionButtonT.GetComponent<BotonGeneric>();
    this.monsterSectionButton = this.monsterSectionButtonT.GetComponent<BotonGeneric>();
    this.scoreboardSectionButton = this.scoreboardSectionButtonT.GetComponent<BotonGeneric>();
    this.glossarySectionButton = this.glossarySectionButtonT.GetComponent<BotonGeneric>();
    this.runsSectionButton = this.runsSectionButtonT.GetComponent<BotonGeneric>();
    this.cardGO = new GameObject[18];
    this.cardGOBlue = new GameObject[18];
    this.cardGOGold = new GameObject[18];
    this.cardGORare = new GameObject[18];
  }

  public void Resize()
  {
  }

  public void InitTome()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<voffset=-4><size=+10><sprite name=cards></size></voffset>");
    stringBuilder.Append(Texts.Instance.GetText("searchCards"));
    this.searchInputPlaceholder.text = stringBuilder.ToString();
    this.CreateNumbers();
    this.CreateTomeCards();
  }

  public void ShowTome(bool _status)
  {
    if (!_status && SceneStatic.GetSceneName() == "TomeOfKnowledge")
    {
      this.content.gameObject.SetActive(false);
      SceneStatic.LoadByName("MainMenu");
    }
    else
    {
      this.content.gameObject.SetActive(_status);
      if (_status)
      {
        for (int index = 0; index < this.unlockedBars.Length; ++index)
          this.unlockedBars[index].InitBar();
        this.playerRunsList = new List<PlayerRun>();
        for (int index = PlayerManager.Instance.PlayerRuns.Count - 1; index >= 0; --index)
        {
          PlayerRun playerRun = JsonUtility.FromJson<PlayerRun>(PlayerManager.Instance.PlayerRuns[index]);
          if (playerRun.Version != null && playerRun.Version != "" && playerRun.FinalScore > 0 && Functions.GameVersionToNumber(playerRun.Version) >= Functions.GameVersionToNumber("0.7.0"))
            this.playerRunsList.Add(playerRun);
        }
        this.DoTomeMain();
        this.SelectTomeCards(0);
      }
      else
      {
        GameManager.Instance.CleanTempContainer();
        PopupManager.Instance.ClosePopup();
      }
    }
  }

  public bool IsActive() => !((UnityEngine.Object) this.content == (UnityEngine.Object) null) && !((UnityEngine.Object) this.content.gameObject == (UnityEngine.Object) null) && this.content.gameObject.activeSelf;

  public void ShowCardsMask(bool _status, bool _instant = false)
  {
  }

  private IEnumerator ShowCardsMaskCo(bool _status, bool _instant)
  {
    float num = 0.4f;
    float destineAlpha = 0.0f;
    if (_status)
      destineAlpha = num;
    SpriteRenderer SPR = this.cardsMask.GetComponent<SpriteRenderer>();
    if (_instant)
    {
      SPR.color = new UnityEngine.Color(1f, 1f, 1f, destineAlpha);
    }
    else
    {
      if (!_status)
        yield return (object) new WaitForSeconds(0.1f);
      while ((double) SPR.color.a != (double) destineAlpha)
      {
        if (_status)
        {
          SPR.color = new UnityEngine.Color(SPR.color.r, SPR.color.g, SPR.color.b, SPR.color.a + 0.05f);
          if ((double) SPR.color.a > (double) destineAlpha)
            break;
        }
        else
        {
          SPR.color = new UnityEngine.Color(SPR.color.r, SPR.color.g, SPR.color.b, SPR.color.a - 0.05f);
          if ((double) SPR.color.a < (double) destineAlpha)
            break;
        }
        yield return (object) new WaitForSeconds(0.01f);
      }
      SPR.color = new UnityEngine.Color(SPR.color.r, SPR.color.g, SPR.color.b, destineAlpha);
    }
  }

  public void DoMouseScroll(Vector2 vectorScroll)
  {
    if (!this.IsActive() || CardScreenManager.Instance.IsActive() || !this.cardsSection.gameObject.activeSelf && !this.itemsSection.gameObject.activeSelf && !this.glossarySection.gameObject.activeSelf && !this.scoreboardSection.gameObject.activeSelf && !this.runsSection.gameObject.activeSelf)
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
      this.DoNextPage();
    }
  }

  public void DoTomeMain()
  {
    this.ActivateSection("main");
    this.statisticsTexts[0].text = PlayerManager.Instance.BestScore.ToString();
    this.statisticsTexts[1].text = this.playerRunsList.Count.ToString();
    this.statisticsTexts[2].text = PlayerManager.Instance.TreasuresClaimed == null ? "0" : PlayerManager.Instance.TreasuresClaimed.Count.ToString();
    this.statisticsTexts[3].text = PlayerManager.Instance.MonstersKilled.ToString();
    this.statisticsTexts[4].text = PlayerManager.Instance.BossesKilled.ToString();
    TMP_Text statisticsText1 = this.statisticsTexts[5];
    int num = PlayerManager.Instance.ExpGained;
    string str1 = num.ToString();
    statisticsText1.text = str1;
    TMP_Text statisticsText2 = this.statisticsTexts[6];
    num = PlayerManager.Instance.CardsCrafted;
    string str2 = num.ToString();
    statisticsText2.text = str2;
    TMP_Text statisticsText3 = this.statisticsTexts[7];
    num = PlayerManager.Instance.CardsUpgraded;
    string str3 = num.ToString();
    statisticsText3.text = str3;
    TMP_Text statisticsText4 = this.statisticsTexts[8];
    num = PlayerManager.Instance.GoldGained;
    string str4 = num.ToString();
    statisticsText4.text = str4;
    TMP_Text statisticsText5 = this.statisticsTexts[9];
    num = PlayerManager.Instance.DustGained;
    string str5 = num.ToString();
    statisticsText5.text = str5;
    TMP_Text statisticsText6 = this.statisticsTexts[10];
    num = PlayerManager.Instance.PurchasedItems;
    string str6 = num.ToString();
    statisticsText6.text = str6;
    TMP_Text statisticsText7 = this.statisticsTexts[11];
    num = PlayerManager.Instance.CorruptionsCompleted;
    string str7 = num.ToString();
    statisticsText7.text = str7;
  }

  private void ActivateSection(string theSection)
  {
    this.activeRun = -1;
    this.runDetails.gameObject.SetActive(false);
    this.runPathMask.gameObject.SetActive(false);
    this.ShowCardsMask(false, true);
    if (this.SetTomeCardsCo != null)
      this.StopCoroutine(this.SetTomeCards());
    if (this.ShowCoroutine != null)
      this.StopCoroutine(this.ShowCoroutine);
    this.Paginator.gameObject.SetActive(false);
    if (theSection == "main")
    {
      this.mainSection.gameObject.SetActive(true);
      this.mainSectionButton.Disable();
      this.mainSectionButton.ShowDisableMask(false);
      this.mainSectionButton.ShowBorder(true);
    }
    else
    {
      this.mainSection.gameObject.SetActive(false);
      this.mainSectionButton.Enable();
      this.mainSectionButton.ShowDisableMask(true);
      this.mainSectionButton.ShowBorder(false);
    }
    if (theSection == "cards")
    {
      this.cardsSection.gameObject.SetActive(true);
      this.cardsSectionButton.Disable();
      this.cardsSectionButton.ShowDisableMask(false);
      this.cardsSectionButton.ShowBorder(true);
      this.cardContainer.gameObject.SetActive(true);
      this.Paginator.gameObject.SetActive(true);
    }
    else
    {
      if (this.cardContainer.gameObject.activeSelf)
      {
        for (int index = this.cardContainer.childCount - 1; index >= 0; --index)
          this.cardContainer.GetChild(index).gameObject.SetActive(false);
      }
      this.cardsSection.gameObject.SetActive(false);
      this.cardsSectionButton.Enable();
      this.cardsSectionButton.ShowDisableMask(true);
      this.cardsSectionButton.ShowBorder(false);
      this.cardContainer.gameObject.SetActive(false);
    }
    if (theSection == "items")
    {
      this.itemsSection.gameObject.SetActive(true);
      this.itemsSectionButton.Disable();
      this.itemsSectionButton.ShowDisableMask(false);
      this.itemsSectionButton.ShowBorder(true);
      this.cardContainer.gameObject.SetActive(true);
      this.Paginator.gameObject.SetActive(true);
    }
    else
    {
      if (this.cardContainer.gameObject.activeSelf)
      {
        for (int index = this.cardContainer.childCount - 1; index >= 0; --index)
          this.cardContainer.GetChild(index).gameObject.SetActive(false);
      }
      this.itemsSection.gameObject.SetActive(false);
      this.itemsSectionButton.Enable();
      this.itemsSectionButton.ShowDisableMask(true);
      this.itemsSectionButton.ShowBorder(false);
      if (theSection != "cards")
        this.cardContainer.gameObject.SetActive(false);
    }
    if (theSection == "glossary")
    {
      this.glossarySection.gameObject.SetActive(true);
      this.glossarySectionButton.Disable();
      this.glossarySectionButton.ShowDisableMask(false);
      this.glossarySectionButton.ShowBorder(true);
      this.Paginator.gameObject.SetActive(true);
    }
    else
    {
      this.glossarySection.gameObject.SetActive(false);
      this.glossarySectionButton.Enable();
      this.glossarySectionButton.ShowDisableMask(true);
      this.glossarySectionButton.ShowBorder(false);
    }
    if (theSection == "scoreboard")
    {
      this.scoreboardSection.gameObject.SetActive(true);
      this.scoreboardSectionButton.Disable();
      this.scoreboardSectionButton.ShowDisableMask(false);
      this.scoreboardSectionButton.ShowBorder(true);
      this.Paginator.gameObject.SetActive(true);
    }
    else
    {
      this.scoreboardSection.gameObject.SetActive(false);
      this.scoreboardSectionButton.Enable();
      this.scoreboardSectionButton.ShowDisableMask(true);
      this.scoreboardSectionButton.ShowBorder(false);
    }
    if (theSection == "runs")
    {
      this.runsSection.gameObject.SetActive(true);
      this.runsSectionButton.Disable();
      this.runsSectionButton.ShowDisableMask(false);
      this.runsSectionButton.ShowBorder(true);
      this.Paginator.gameObject.SetActive(true);
    }
    else
    {
      this.runsSection.gameObject.SetActive(false);
      this.runsSectionButton.Enable();
      this.runsSectionButton.ShowDisableMask(true);
      this.runsSectionButton.ShowBorder(false);
    }
    if (theSection == "cards" || theSection == "items")
      this.ShowSearch(true);
    else
      this.ShowSearch(false);
  }

  public void DoTomeCards()
  {
    this.ActivateSection("cards");
    this.ShowTomeCardsButtons();
  }

  public void DoTomeItems()
  {
    this.ActivateSection("items");
    this.ShowTomeItemsButtons();
  }

  public void DoTomeScoreboard()
  {
    this.ActivateSection("scoreboard");
    this.ShowScoreboard(0);
  }

  public void DoTomeRuns()
  {
    this.ActivateSection("runs");
    this.ShowRuns(0);
  }

  public void DoTomeGlossary()
  {
    if (this.glossaryTerms == null)
      this.CreateGlossary();
    this.ActivateSection("glossary");
    this.SetPage(0, true);
  }

  public void DoTomeChallenge() => this.ActivateSection("challenge");

  public void DoTomeMonsters() => this.ActivateSection("monsters");

  private void NextPage() => ++this.pageAct;

  private void PrevPage() => --this.pageAct;

  public void CreateGlossary()
  {
    KeyNotesData[] keyNotesDataArray = new KeyNotesData[Globals.Instance.KeyNotes.Count];
    this.glossaryTerms = new SortedDictionary<string, string>();
    this.glossaryTermsTitle = new Dictionary<string, string>();
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    int num1 = 0;
    int num2 = 0;
    string str = "";
    Transform parent = this.glossaryPageIndex;
    foreach (KeyValuePair<string, KeyNotesData> keyNote in Globals.Instance.KeyNotes)
    {
      if (!(keyNote.Value.Id == "daze") && !(keyNote.Value.Id == "haste") && !(keyNote.Value.Id == "frail"))
      {
        stringBuilder1.Clear();
        stringBuilder2.Clear();
        stringBuilder1.Append("<voffset=.1><sprite name=");
        if (keyNote.Value.Id == "chain" || keyNote.Value.Id == "jump" || keyNote.Value.Id == "jump(bonus%)" || keyNote.Value.Id == "overcharge" || keyNote.Value.Id == "repeat" || keyNote.Value.Id == "repeatupto" || keyNote.Value.Id == "dispel" || keyNote.Value.Id == "purge" || keyNote.Value.Id == "discover" || keyNote.Value.Id == "reveal" || keyNote.Value.Id == "transfer" || keyNote.Value.Id == "steal" || keyNote.Value.Id == "aura" || keyNote.Value.Id == "curse" || keyNote.Value.Id == "escapes")
          stringBuilder1.Append("cards");
        else if (keyNote.Value.Id == "resistance")
          stringBuilder1.Append("ui_resistance");
        else if (keyNote.Value.Id == "speed")
          stringBuilder1.Append("speedMini");
        else
          stringBuilder1.Append(keyNote.Value.Id);
        stringBuilder1.Append("></voffset>");
        stringBuilder1.Append(keyNote.Value.KeynoteName);
        if (keyNote.Value.Id == "overcharge")
        {
          stringBuilder1.Append(" [");
          stringBuilder1.Append(Texts.Instance.GetText("overchargeAcronym"));
          stringBuilder1.Append("]");
        }
        stringBuilder2.Append(stringBuilder1.ToString());
        if (!this.glossaryTermsTitle.ContainsKey(keyNote.Value.KeynoteName))
        {
          this.glossaryTermsTitle.Add(keyNote.Value.KeynoteName, stringBuilder2.ToString());
          stringBuilder1.Replace("<voffset=.1>", "<size=+.3><voffset=-.1>");
          stringBuilder1.Replace("</voffset>", "</voffset></size>");
          AuraCurseData auraCurseData = Globals.Instance.GetAuraCurseData(keyNote.Value.Id);
          if ((UnityEngine.Object) auraCurseData != (UnityEngine.Object) null)
          {
            if (auraCurseData.IsAura)
            {
              stringBuilder1.Append("  <size=-.4><color=#3765A9>(");
              stringBuilder1.Append(Texts.Instance.GetText("aura"));
              stringBuilder1.Append(")</color></size>");
            }
            else
            {
              stringBuilder1.Append("  <size=-.4><color=#B0363E>(");
              stringBuilder1.Append(Texts.Instance.GetText("curse"));
              stringBuilder1.Append(")</color></size>");
            }
          }
          stringBuilder1.Append("<line-height=30%><br></line-height><br><size=-.5><color=#794D31>");
          stringBuilder1.Append(Functions.StripTags(keyNote.Value.DescriptionExtended.Replace("<br3>", " ").Trim()));
          if ((UnityEngine.Object) auraCurseData != (UnityEngine.Object) null && !auraCurseData.GainCharges)
          {
            stringBuilder1.Append(" ");
            stringBuilder1.Append(Texts.Instance.GetText("notStackPlain"));
            stringBuilder1.Append(".");
          }
          stringBuilder1.Append("</color></size>");
          this.glossaryTerms.Add(keyNote.Value.KeynoteName, stringBuilder1.ToString());
        }
      }
    }
    this.glossaryTermsTitle.Add("TerrainCondition", "<sprite name=node>" + Texts.Instance.GetText("terrainCondition"));
    stringBuilder1.Clear();
    stringBuilder1.Append("<size=+.3><voffset=-.1><sprite name=node></voffset></size>");
    stringBuilder1.Append(Texts.Instance.GetText("terrainCondition"));
    stringBuilder1.Append("<line-height=30%><br></line-height><br><size=-.5><color=#794D31>");
    stringBuilder1.Append(Texts.Instance.GetText("terrainConditionDesc"));
    stringBuilder1.Append("  ");
    bool flag = true;
    foreach (Enums.NodeGround _ground in Enum.GetValues(typeof (Enums.NodeGround)))
    {
      if (_ground != Enums.NodeGround.None)
      {
        if (flag)
          flag = false;
        else
          stringBuilder1.Append(",  ");
        stringBuilder1.Append(Functions.GetNodeGroundSprite(_ground));
        stringBuilder1.Append(Texts.Instance.GetText(Enum.GetName(typeof (Enums.NodeGround), (object) _ground)));
      }
    }
    this.glossaryTerms.Add("TerrainCondition", stringBuilder1.ToString());
    int num3 = 45;
    foreach (KeyValuePair<string, string> glossaryTerm in this.glossaryTerms)
    {
      if (Globals.Instance.CurrentLang == "en" || Globals.Instance.CurrentLang == "es")
      {
        if (num2 >= num3)
          parent = this.glossaryPageIndex2;
        if (glossaryTerm.Key.ToString() != "" && glossaryTerm.Key[0].ToString() != str)
        {
          str = glossaryTerm.Key[0].ToString();
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.glossaryIndexItem, Vector3.zero, Quaternion.identity, parent);
          gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0.0f);
          gameObject.transform.GetChild(0).GetComponent<BotonGeneric>().SetText("<voffset=-1.5><size=3.8><color=#8C5B34><b>" + str + "</b></color></size>");
          gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
          ++num2;
        }
      }
      if (num2 >= num3)
        parent = this.glossaryPageIndex2;
      GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.glossaryIndexItem, Vector3.zero, Quaternion.identity, parent);
      gameObject1.transform.localPosition = new Vector3(gameObject1.transform.localPosition.x, gameObject1.transform.localPosition.y, 0.0f);
      gameObject1.transform.GetChild(0).GetComponent<BotonGeneric>().SetText(this.glossaryTermsTitle[glossaryTerm.Key]);
      gameObject1.transform.GetChild(0).GetComponent<BotonGeneric>().auxInt = num1;
      gameObject1.transform.GetChild(0).GetComponent<BotonGeneric>().auxString = "GlossaryItemIndex";
      ++num1;
      ++num2;
    }
  }

  public void SetGlossaryPageFromButton(int _buttonIndex)
  {
    this.glossaryTermSelected = _buttonIndex;
    this.SetPage(Mathf.CeilToInt((float) (_buttonIndex + 1) / 12f), true);
    if (this.glossaryCo != null)
      this.StopCoroutine(this.glossaryCo);
    this.glossaryCo = this.StartCoroutine(this.ClearGlossaryTermCo());
  }

  private IEnumerator ClearGlossaryTermCo()
  {
    yield return (object) new WaitForSeconds(1.5f);
    if (this.IsActive())
      this.ClearGlossaryTerm();
  }

  private void ClearGlossaryTerm()
  {
    this.glossaryTermSelected = -1;
    for (int index = 0; index < 12; ++index)
      this.glossaryTexts[index].transform.parent.GetChild(1).gameObject.SetActive(false);
    if (this.glossaryCo == null)
      return;
    this.StopCoroutine(this.glossaryCo);
  }

  public void ShowGlossary()
  {
    this.pageMax = Mathf.CeilToInt((float) this.glossaryTerms.Count / 12f);
    this.RedoPageNumbers();
    if (this.pageAct == 0)
    {
      this.glossaryIndex.gameObject.SetActive(true);
      this.glossaryPageLeft.gameObject.SetActive(false);
      this.glossaryPageRight.gameObject.SetActive(false);
      this.TomeButtons[23].Activate();
      this.ClearGlossaryTerm();
    }
    else
    {
      this.glossaryIndex.gameObject.SetActive(false);
      this.glossaryPageLeft.gameObject.SetActive(true);
      this.glossaryPageRight.gameObject.SetActive(true);
      this.TomeButtons[23].Deactivate();
      int num1 = 0;
      int index1 = 0;
      int num2 = (this.pageAct - 1) * 12;
      int num3 = this.pageAct * 12;
      if (num2 < 0)
      {
        num2 = 0;
        num3 = 12;
      }
      foreach (KeyValuePair<string, string> glossaryTerm in this.glossaryTerms)
      {
        if (this.glossaryTerms.Count > num1 && num1 >= num2 && num1 < num3)
        {
          this.glossaryTexts[index1].text = glossaryTerm.Value;
          this.glossaryTexts[index1].transform.parent.gameObject.SetActive(true);
          if (this.glossaryTermSelected > -1 && this.glossaryTermSelected == num1)
            this.glossaryTexts[index1].transform.parent.GetChild(1).gameObject.SetActive(true);
          ++index1;
        }
        ++num1;
      }
      for (int index2 = index1; index2 < 12; ++index2)
        this.glossaryTexts[index2].transform.parent.gameObject.SetActive(false);
    }
  }

  public void ShowRuns(int _index, int _subindex = -1)
  {
    this.pageOld = this.pageAct = 0;
    this.pageMax = Mathf.CeilToInt((float) this.playerRunsList.Count / (float) this.numRunsPerPage);
    this.RedoPageNumbers();
    if (this.pageMax <= 0)
      return;
    this.RedoPageNumbers();
    this.TomeButtons[14].gameObject.SetActive(true);
    this.TomeButtons[15].gameObject.SetActive(true);
    if (this.scoreboardType == 0)
    {
      this.TomeButtons[14].Activate();
      this.TomeButtons[15].Deactivate();
    }
    else
    {
      this.TomeButtons[15].Activate();
      this.TomeButtons[14].Deactivate();
    }
    this.SetPage(1, true);
  }

  private void SetRuns()
  {
    GameManager.Instance.PlayLibraryAudio("ui_book_page", 0.2f);
    this.runsContainer.gameObject.SetActive(true);
    for (int index = this.runsContainer.childCount - 1; index >= 0; --index)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.runsContainer.GetChild(index).gameObject);
    int num1 = (this.pageAct - 1) * this.numRunsPerPage;
    int num2 = num1 + this.numRunsPerPage;
    int num3 = this.numRunsPerPage / 2;
    for (int _index = num1; _index < num2; ++_index)
    {
      if (_index < this.playerRunsList.Count)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.run, Vector3.zero, Quaternion.identity, this.runsContainer);
        gameObject.name = "run_" + _index.ToString();
        Mathf.FloorToInt((float) (_index / 3));
        float x;
        int num4;
        if ((num2 - _index > num3 ? 1 : 0) != 0)
        {
          x = -5.48f;
          num4 = _index - num1;
        }
        else
        {
          x = 0.7f;
          num4 = num3 - (num2 - _index);
        }
        gameObject.transform.localPosition = new Vector3(x, (float) (2.6500000953674316 - 1.8999999761581421 * (double) num4), -1f);
        gameObject.GetComponent<TomeRun>().SetRun(_index);
      }
    }
  }

  public void DoRun(int _index)
  {
    this.activeRun = _index;
    this.runPathMask.gameObject.SetActive(true);
    this.runDetails.gameObject.SetActive(true);
    this.Paginator.gameObject.SetActive(false);
    this.runsContainer.gameObject.SetActive(false);
    this.runDetails.GetComponent<TomeRunDetails>().SetRun(_index);
  }

  public void RunDetailOpenCharacter(int _index) => this.runDetails.GetComponent<TomeRunDetails>().DoCharacter(_index);

  public void RunDetailClose()
  {
    this.activeRun = -1;
    this.runPathMask.gameObject.SetActive(false);
    this.runDetails.gameObject.SetActive(false);
    this.Paginator.gameObject.SetActive(true);
    this.runsContainer.gameObject.SetActive(true);
  }

  public void RunDetailButton(int _index) => this.runDetails.GetComponent<TomeRunDetails>().RunDetailButton(_index);

  public void RunCombatStats()
  {
    DamageMeterManager.Instance.SaveATOStats();
    TomeRunDetails component = this.runDetails.GetComponent<TomeRunDetails>();
    AtOManager.Instance.combatStats = new int[4, component.playerRun.CombatStats0.GetLength(0)];
    for (int index = 0; index < component.playerRun.CombatStats0.GetLength(0); ++index)
    {
      AtOManager.Instance.combatStats[0, index] = component.playerRun.CombatStats0[index];
      AtOManager.Instance.combatStats[1, index] = component.playerRun.CombatStats1[index];
      AtOManager.Instance.combatStats[2, index] = component.playerRun.CombatStats2[index];
      AtOManager.Instance.combatStats[3, index] = component.playerRun.CombatStats3[index];
    }
    DamageMeterManager.Instance.Show();
  }

  public void NextPath() => this.runDetails.GetComponent<TomeRunDetails>().NextPath();

  public void PrevPath() => this.runDetails.GetComponent<TomeRunDetails>().PrevPath();

  public async void ShowScoreboard(int _index, int _subindex = -1)
  {
    for (int index = 0; index < this.scoresName.Length; ++index)
      this.scoresName[index].gameObject.SetActive(false);
    this.scoreStatus.text = "";
    this.pageMax = 0;
    this.RedoPageNumbers();
    this.scoreTitle.text = this.scoreTitleSub.text = "";
    this.buttonPrevWeekly.transform.gameObject.SetActive(false);
    this.buttonNextWeekly.transform.gameObject.SetActive(false);
    this.week = _subindex;
    this.currentScoreboard = _index;
    int currentWeek = Functions.GetCurrentWeeklyWeek();
    if (this.week == -1)
      this.week = currentWeek;
    else if (this.week == 0)
      this.week = 1;
    switch (_index)
    {
      case 0:
        await SteamManager.Instance.GetLeaderboards(0);
        this.scoreTitle.text = this.scoreTitleSub.text = Texts.Instance.GetText("singleplayer");
        break;
      case 1:
        await SteamManager.Instance.GetLeaderboards(1);
        this.scoreTitle.text = this.scoreTitleSub.text = Texts.Instance.GetText("multiplayer");
        break;
      case 2:
        await SteamManager.Instance.GetLeaderboards(2);
        this.scoreTitle.text = this.scoreTitleSub.text = Texts.Instance.GetText("obeliskChallengeSingleplayer");
        break;
      case 3:
        await SteamManager.Instance.GetLeaderboards(3);
        this.scoreTitle.text = this.scoreTitleSub.text = Texts.Instance.GetText("obeliskChallengeMultiplayer");
        break;
      case 4:
        await SteamManager.Instance.GetLeaderboards(4, this.week);
        StringBuilder stringBuilder1 = new StringBuilder();
        stringBuilder1.Append(Texts.Instance.GetText("menuWeekly"));
        stringBuilder1.Append("<br><size=-.5><color=#FFAE88>");
        stringBuilder1.Append(AtOManager.Instance.GetWeeklyName(this.week));
        stringBuilder1.Append(" (");
        stringBuilder1.Append(Texts.Instance.GetText("singleplayer"));
        stringBuilder1.Append(")");
        this.scoreTitle.text = this.scoreTitleSub.text = stringBuilder1.ToString();
        break;
      case 5:
        await SteamManager.Instance.GetLeaderboards(5, this.week);
        StringBuilder stringBuilder2 = new StringBuilder();
        stringBuilder2.Append(Texts.Instance.GetText("menuWeekly"));
        stringBuilder2.Append("<br><size=-.5><color=#FFAE88>");
        stringBuilder2.Append(AtOManager.Instance.GetWeeklyName(this.week));
        stringBuilder2.Append(" (");
        stringBuilder2.Append(Texts.Instance.GetText("multiplayer"));
        stringBuilder2.Append(")");
        this.scoreTitle.text = this.scoreTitleSub.text = stringBuilder2.ToString();
        break;
    }
    this.buttonPrevWeekly.transform.gameObject.SetActive(false);
    this.buttonNextWeekly.transform.gameObject.SetActive(false);
    if (_index == 4 || _index == 5)
    {
      if (this.week > 1)
        this.buttonPrevWeekly.transform.gameObject.SetActive(true);
      if (this.week < currentWeek)
        this.buttonNextWeekly.transform.gameObject.SetActive(true);
    }
    this.scoreStatus.text = "[" + Texts.Instance.GetText("loading") + "]";
    this.DoScoreboard();
  }

  public void PrevWeekly()
  {
    --this.week;
    this.ShowScoreboard(this.currentScoreboard, this.week);
  }

  public void NextWeekly()
  {
    ++this.week;
    this.ShowScoreboard(this.currentScoreboard, this.week);
  }

  private void ScoresNotFound() => this.scoreStatus.text = Texts.Instance.GetText("scoresNotFound");

  private void DoScoreboard()
  {
    this.pageMax = this.pageOld = this.pageAct = 0;
    if (this.scoreboardType == 0)
    {
      if (SteamManager.Instance.scoreboardGlobal != null)
        this.pageMax = Mathf.CeilToInt((float) SteamManager.Instance.scoreboardGlobal.Length / (float) this.numScoreRows);
    }
    else if (SteamManager.Instance.scoreboardFriends != null)
      this.pageMax = Mathf.CeilToInt((float) SteamManager.Instance.scoreboardFriends.Length / (float) this.numScoreRows);
    this.RedoPageNumbers();
    if (this.scoreboardType == 0)
    {
      this.TomeButtons[14].Activate();
      this.TomeButtons[15].Deactivate();
    }
    else
    {
      this.TomeButtons[15].Activate();
      this.TomeButtons[14].Deactivate();
    }
    if (this.pageMax == 0)
    {
      this.ScoresNotFound();
      this.scores.gameObject.SetActive(false);
    }
    else
    {
      this.scores.gameObject.SetActive(true);
      this.scoreStatus.text = "";
      if (this.pageMax > 10)
        this.pageMax = 10;
      this.SetPage(1, true);
    }
  }

  public void SetPage(int page, bool absolute = false)
  {
    if (!absolute && this.pageAct == page)
      return;
    this.pageOld = this.pageAct;
    this.pageAct = page;
    this.ChangePage();
  }

  private void ChangePage()
  {
    if (this.runsSection.gameObject.activeSelf)
      this.SetRuns();
    else if (this.scoreboardSection.gameObject.activeSelf)
    {
      if (this.SetScoresCo != null)
        this.StopCoroutine(this.SetScores());
      this.SetScoresCo = this.StartCoroutine(this.SetScores());
    }
    else if (this.glossarySection.gameObject.activeSelf)
    {
      this.ShowGlossary();
    }
    else
    {
      if (this.SetTomeCardsCo != null)
        this.StopCoroutine(this.SetTomeCards());
      this.SetTomeCardsCo = this.StartCoroutine(this.SetTomeCards());
    }
    this.RedoPageNumbersPositions();
  }

  public bool IsThereNext() => this.pageAct < this.pageMax;

  public bool IsLastPage() => this.pageAct + 1 == this.pageMax;

  public void DoNextPage()
  {
    if (this.activeRun > -1 || this.pageAct >= this.pageMax)
      return;
    this.pageOld = this.pageAct;
    int pageOld = this.pageOld;
    this.NextPage();
    this.ChangePage();
  }

  public bool IsTherePrev() => this.pageAct > 1;

  public bool IsFirstPage() => this.pageAct == 2;

  public void DoPrevPage()
  {
    if (this.activeRun > -1 || this.pageAct <= 1)
      return;
    this.pageOld = this.pageAct;
    this.PrevPage();
    this.ChangePage();
  }

  private void CreateTomeCards()
  {
    this.tomeTs = new Transform[18];
    this.tomeCards = new global::TomeCard[18];
    for (int index = 0; index < this.numCards; ++index)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TomeCard, Vector3.zero, Quaternion.identity, this.cardContainer);
      gameObject.name = "card_" + index.ToString();
      Transform transform = gameObject.transform;
      int num1 = Mathf.FloorToInt((float) (index / 3));
      int num2 = index % 3;
      if (index < 9)
      {
        transform.localPosition = new Vector3((float) ((double) num2 * 2.1500000953674316 - 5.0999999046325684), (float) (2.0999999046325684 - (double) num1 * 3.0), 0.0f);
      }
      else
      {
        int num3 = num1 - 3;
        transform.localPosition = new Vector3((float) (2.2000000476837158 + (double) num2 * 2.1500000953674316), (float) (2.0999999046325684 - (double) num3 * 3.0), 0.0f);
      }
      this.tomeTs[index] = transform;
      this.tomeCards[index] = transform.GetComponent<global::TomeCard>();
    }
  }

  private void RedoPageNumbersPositions() => this.CreateCraftPages(this.pageAct, this.pageMax);

  private void CreateCraftPages(int page, int total)
  {
    int num1 = 12;
    int num2 = 2;
    int index1 = 0;
    if (total <= 1)
      return;
    for (int index2 = 0; index2 < total; ++index2)
    {
      bool flag = false;
      GameObject gameObject = this.tomeNumbers[index1].transform.gameObject;
      gameObject.gameObject.SetActive(true);
      gameObject.transform.localPosition = new Vector3(-6.75f, (float) (8.0 - 0.550000011920929 * (double) index1), 0.0f);
      gameObject.name = "TomePage";
      global::TomeNumber component = gameObject.transform.GetComponent<global::TomeNumber>();
      component.Deactivate();
      if (total < num1)
      {
        component.Init(index2 + 1);
        if (page - 1 == index2)
          this.tomeNumbers[index2].Activate();
        flag = true;
      }
      else if (index2 == num2 && index2 != page && (double) page > (double) Mathf.Ceil((float) num1 * 0.5f))
      {
        component.SetText("...");
        flag = true;
      }
      else if (index2 == total - num2 - 1 && index2 != page && (double) page < (double) total - (double) Mathf.Ceil((float) num1 * 0.5f) + (double) num2)
      {
        component.SetText("...");
        flag = true;
      }
      else if ((index2 <= num2 || index2 >= page - num2 - 1 || index2 >= total - Functions.FuncRoundToInt((float) num1 * 0.5f) - num2) && (index2 <= page + num2 - 1 || index2 >= total - num2 || index2 <= Functions.FuncRoundToInt((float) num1 * 0.5f) + 1))
      {
        component.Init(index2 + 1);
        if (page - 1 == index2)
          component.Activate();
        flag = true;
      }
      if (flag)
        ++index1;
    }
  }

  private void CreateNumbers()
  {
    for (int index = 0; index < this.maxTomeNumbers; ++index)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TomeNumber, Vector3.zero, Quaternion.identity, this.Paginator);
      gameObject.transform.localPosition = new Vector3(-6.75f, (float) (8.0 - 0.550000011920929 * (double) index), 0.0f);
      global::TomeNumber component = gameObject.GetComponent<global::TomeNumber>();
      this.tomeNumbers[index] = component;
      component.Init(index + 1);
      gameObject.transform.gameObject.SetActive(false);
      component.Deactivate();
    }
  }

  public void ShowTomeCardsButtons()
  {
    if (GameManager.Instance.GetDeveloperMode())
      this.TomeButtons[6].transform.gameObject.SetActive(true);
    else
      this.TomeButtons[6].transform.gameObject.SetActive(false);
    this.activeTomeCards = -100;
    this.SelectTomeCards(0);
  }

  public void ShowTomeItemsButtons()
  {
    this.activeTomeCards = -100;
    this.SelectTomeCards(7);
  }

  public void SelectTomeCards(int index = -1, bool absolute = false)
  {
    if (index == this.activeTomeCards && !absolute)
      return;
    this.activeTomeCards = index;
    List<string> stringList = new List<string>();
    switch (index)
    {
      case -1:
        stringList = Globals.Instance.CardListNotUpgraded;
        break;
      case 0:
        stringList = Globals.Instance.CardListNotUpgradedByClass[Enums.CardClass.Warrior];
        break;
      case 1:
        stringList = Globals.Instance.CardListNotUpgradedByClass[Enums.CardClass.Mage];
        break;
      case 2:
        stringList = Globals.Instance.CardListNotUpgradedByClass[Enums.CardClass.Healer];
        break;
      case 3:
        stringList = Globals.Instance.CardListNotUpgradedByClass[Enums.CardClass.Scout];
        break;
      default:
        if (index == 4 && GameManager.Instance.GetDeveloperMode())
        {
          stringList = Globals.Instance.CardListNotUpgradedByClass[Enums.CardClass.Monster];
          break;
        }
        switch (index)
        {
          case 5:
            stringList = Globals.Instance.CardListNotUpgradedByClass[Enums.CardClass.Boon];
            break;
          case 6:
            stringList = Globals.Instance.CardListNotUpgradedByClass[Enums.CardClass.Injury];
            break;
          case 7:
            stringList = Globals.Instance.CardItemByType[Enums.CardType.Weapon];
            break;
          case 8:
            stringList = Globals.Instance.CardItemByType[Enums.CardType.Armor];
            break;
          case 9:
            stringList = Globals.Instance.CardItemByType[Enums.CardType.Jewelry];
            break;
          case 10:
            stringList = Globals.Instance.CardItemByType[Enums.CardType.Accesory];
            break;
          case 11:
            stringList = Globals.Instance.CardItemByType[Enums.CardType.Pet];
            break;
          case 22:
            stringList = Globals.Instance.CardListByType[Enums.CardType.Enchantment];
            break;
        }
        break;
    }
    this.cardList.Clear();
    for (int index1 = 0; index1 < stringList.Count; ++index1)
    {
      CardData cardData = Globals.Instance.GetCardData(stringList[index1], false);
      if (!((UnityEngine.Object) cardData != (UnityEngine.Object) null) || cardData.ShowInTome)
      {
        if (this.searchTerm.Trim() != "")
        {
          if (index != 22 || cardData.CardClass != Enums.CardClass.Monster)
          {
            if (Globals.Instance.IsInSearch(this.searchTerm, stringList[index1]))
              this.cardList.Add(stringList[index1]);
            if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && index != 22)
            {
              if (cardData.UpgradesTo1 != "" && Globals.Instance.IsInSearch(this.searchTerm, cardData.UpgradesTo1))
                this.cardList.Add(cardData.UpgradesTo1);
              if (cardData.UpgradesTo2 != "" && Globals.Instance.IsInSearch(this.searchTerm, cardData.UpgradesTo2))
                this.cardList.Add(cardData.UpgradesTo2);
              if ((UnityEngine.Object) cardData.UpgradesToRare != (UnityEngine.Object) null && Globals.Instance.IsInSearch(this.searchTerm, cardData.UpgradesToRare.Id))
                this.cardList.Add(cardData.UpgradesToRare.Id);
            }
          }
        }
        else if (index != 22 || cardData.CardUpgraded == Enums.CardUpgraded.No && cardData.CardClass != Enums.CardClass.Monster)
          this.cardList.Add(stringList[index1]);
      }
    }
    this.cardList.Sort();
    this.pageOld = this.pageAct = 0;
    this.pageMax = Mathf.CeilToInt((float) this.cardList.Count / (float) this.numCards);
    this.RedoPageNumbers();
    this.ActivateDeactivateButtons(index);
    this.SetPage(1, true);
  }

  private void ActivateDeactivateButtons(int index)
  {
    for (int index1 = 0; index1 < this.TomeButtons.Length; ++index1)
    {
      if (this.TomeButtons[index1].transform.gameObject.activeSelf)
      {
        if (this.TomeButtons[index1].tomeClass == index)
          this.TomeButtons[index1].Activate();
        else
          this.TomeButtons[index1].Deactivate();
      }
    }
  }

  private void RedoPageNumbers()
  {
    int num = 14;
    if (this.pageMax < num)
      num = this.pageMax;
    for (int index = 0; index < num; ++index)
    {
      if (this.IsActive() && this.tomeNumbers[index].gameObject.activeSelf)
        this.tomeNumbers[index].Show();
      this.tomeNumbers[index].Deactivate();
    }
    for (int index = num; index < this.maxTomeNumbers; ++index)
      this.tomeNumbers[index].Hide();
    this.tomeNumbers[0].Activate();
  }

  public void SelectTomeScores(int tomeButton)
  {
    if (tomeButton == 14 && this.scoreboardType == 1)
    {
      this.scoreboardType = 0;
      this.DoScoreboard();
    }
    else
    {
      if (tomeButton != 15 || this.scoreboardType != 0)
        return;
      this.scoreboardType = 1;
      this.DoScoreboard();
    }
  }

  private IEnumerator SetScores()
  {
    if (this.scoreboardType == 0)
    {
      if (SteamManager.Instance.scoreboardGlobal == null)
        yield break;
    }
    else if (SteamManager.Instance.scoreboardFriends == null)
      yield break;
    GameManager.Instance.PlayLibraryAudio("ui_book_page", 0.2f);
    int num1 = (this.pageAct - 1) * this.numScoreRows;
    int num2 = num1 + this.numScoreRows;
    int pageAct = this.pageAct;
    int num3 = 0;
    int index1 = 0;
    this.playerOnScoreboard = false;
    if (this.scoreboardType == 0)
    {
      foreach (LeaderboardEntry leaderboardEntry in SteamManager.Instance.scoreboardGlobal)
      {
        if (pageAct == this.pageAct)
        {
          if ((ulong) leaderboardEntry.User.Id != 0UL)
          {
            if (num3 >= num1 && num3 < num2)
            {
              this.scoresName[index1].gameObject.SetActive(true);
              this.scoresName[index1].SetScore(num3 + 1, leaderboardEntry.User.Name, leaderboardEntry.Score, (ulong) leaderboardEntry.User.Id);
              ++index1;
            }
            ++num3;
            if (num3 >= num2)
              break;
          }
        }
        else
          break;
      }
    }
    else
    {
      foreach (LeaderboardEntry scoreboardFriend in SteamManager.Instance.scoreboardFriends)
      {
        if (pageAct == this.pageAct)
        {
          if (num3 >= num1 && num3 < num2)
          {
            this.scoresName[index1].gameObject.SetActive(true);
            this.scoresName[index1].SetScore(num3 + 1, scoreboardFriend.User.Name, scoreboardFriend.Score, (ulong) scoreboardFriend.User.Id);
            ++index1;
          }
          ++num3;
          if (num3 >= num2)
            break;
        }
        else
          break;
      }
    }
    for (int index2 = index1; index2 < this.scoresName.Length; ++index2)
      this.scoresName[index2].gameObject.SetActive(false);
    if (!this.playerOnScoreboard && SteamManager.Instance.scoreboardSingle != null)
    {
      foreach (LeaderboardEntry leaderboardEntry in SteamManager.Instance.scoreboardSingle)
      {
        this.scoresName[this.scoresName.Length - 2].gameObject.SetActive(true);
        this.scoresName[this.scoresName.Length - 2].SetScore(-1, ".....");
        this.scoresName[this.scoresName.Length - 1].gameObject.SetActive(true);
        this.scoresName[this.scoresName.Length - 1].SetScore(leaderboardEntry.GlobalRank, leaderboardEntry.User.Name, leaderboardEntry.Score);
      }
    }
  }

  private IEnumerator SetTomeCards()
  {
    if (this.cardList != null)
    {
      if (!this.mainSection.gameObject.activeSelf)
      {
        GameManager.Instance.PlayLibraryAudio("ui_book_page", 0.1f);
        yield return (object) Globals.Instance.WaitForSeconds(0.06f);
      }
      if (this.pageOld > 0 && this.pageAct < this.pageOld && this.cardGO != null && this.cardGO.Length != 0)
      {
        for (int index = 0; index < this.cardGO.Length; ++index)
        {
          if ((UnityEngine.Object) this.cardGO[index] != (UnityEngine.Object) null)
          {
            this.cardGO[index].gameObject.SetActive(false);
            this.tomeCards[index].ShowButtons(false);
            this.tomeCards[index].ShowButtonRare(false);
          }
        }
        for (int index = 0; index < this.cardGOBlue.Length; ++index)
        {
          if ((UnityEngine.Object) this.cardGOBlue[index] != (UnityEngine.Object) null)
            this.cardGOBlue[index].gameObject.SetActive(false);
        }
        for (int index = 0; index < this.cardGOGold.Length; ++index)
        {
          if ((UnityEngine.Object) this.cardGOGold[index] != (UnityEngine.Object) null)
            this.cardGOGold[index].gameObject.SetActive(false);
        }
        for (int index = 0; index < this.cardGORare.Length; ++index)
        {
          if ((UnityEngine.Object) this.cardGORare[index] != (UnityEngine.Object) null)
            this.cardGORare[index].gameObject.SetActive(false);
        }
      }
      int indexAct = (this.pageAct - 1) * this.numCards;
      int indexTomeCard = -1;
      int pageFor = this.pageAct;
      int cardCount = this.cardList.Count;
      GameManager.Instance.CleanTempContainer();
      PopupManager.Instance.ClosePopup();
      for (int i = indexAct; i < indexAct + this.numCards && pageFor == this.pageAct; ++i)
      {
        ++indexTomeCard;
        if (i < this.cardList.Count)
        {
          if (!this.tomeTs[indexTomeCard].gameObject.activeSelf)
            this.tomeTs[indexTomeCard].gameObject.SetActive(true);
          GameObject gameObject1;
          if ((UnityEngine.Object) this.cardGO[indexTomeCard] == (UnityEngine.Object) null)
          {
            gameObject1 = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.tomeTs[indexTomeCard]);
          }
          else
          {
            gameObject1 = this.cardGO[indexTomeCard];
            gameObject1.gameObject.SetActive(true);
          }
          CardItem component1 = gameObject1.GetComponent<CardItem>();
          gameObject1.name = "tomecd_" + i.ToString();
          string card = this.cardList[i];
          component1.SetCard(card, GetFromGlobal: true);
          component1.SetLocalScale(new Vector3(1f, 1f, 1f));
          component1.CreateColliderAdjusted();
          component1.cardoutsidecombat = true;
          component1.cardoutsidelibary = true;
          component1.TopLayeringOrder("UI", 30100);
          component1.HideRarityParticles();
          component1.HideCardIconParticles();
          component1.ShowSpriteOverCard();
          int num = PlayerManager.Instance.IsCardUnlocked(card) ? 1 : 0;
          if (num == 0)
          {
            component1.ShowLockedBackground(true);
          }
          else
          {
            component1.ShowDisable(false);
            component1.ShowBackImage(false);
            component1.ShowLockedBackground(false);
          }
          component1.PlaySightParticle();
          gameObject1.transform.localPosition = Vector3.zero;
          this.cardGO[indexTomeCard] = gameObject1;
          if (num == 0 || this.searchTerm != "")
          {
            this.tomeCards[indexTomeCard].ShowButtons(false);
            this.tomeCards[indexTomeCard].ShowButtonRare(false);
            if ((UnityEngine.Object) this.cardGOBlue[indexTomeCard] != (UnityEngine.Object) null)
              this.cardGOBlue[indexTomeCard].gameObject.SetActive(false);
            if ((UnityEngine.Object) this.cardGOGold[indexTomeCard] != (UnityEngine.Object) null)
              this.cardGOGold[indexTomeCard].gameObject.SetActive(false);
            if ((UnityEngine.Object) this.cardGORare[indexTomeCard] != (UnityEngine.Object) null)
              this.cardGORare[indexTomeCard].gameObject.SetActive(false);
            if (this.searchTerm == "")
              component1.DoTome(true, 1 + i, cardCount);
            else
              component1.DoTome(false, 1 + i, cardCount);
          }
          else
          {
            bool flag1 = false;
            bool flag2 = false;
            bool flag3 = false;
            component1.DoTome(false, 1 + i, cardCount);
            if (this.searchTerm == "")
            {
              this.tomeCards[indexTomeCard].ShowButtons(true);
              float y = -1.452f;
              GameObject gameObject2 = !((UnityEngine.Object) this.cardGOBlue[indexTomeCard] == (UnityEngine.Object) null) ? this.cardGOBlue[indexTomeCard] : UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.tomeTs[indexTomeCard]);
              if (Globals.Instance.Cards.ContainsKey(card + "a"))
              {
                flag1 = true;
                gameObject2.gameObject.SetActive(true);
                CardItem component2 = gameObject2.GetComponent<CardItem>();
                gameObject2.name = "cardblue_" + i.ToString();
                component2.SetCard(card + "a", GetFromGlobal: true);
                component2.SetLocalScale(new Vector3(0.25f, 0.08f, 1f));
                component2.CreateColliderAdjusted();
                component2.ShowBackImage(true);
                component2.cardoutsidecombat = true;
                component2.cardoutsidelibary = true;
                component2.TopLayeringOrder("UI", 30100);
                gameObject2.transform.localPosition = new Vector3(-0.58f, y, 0.0f);
                this.cardGOBlue[indexTomeCard] = gameObject2;
              }
              else
              {
                gameObject2.gameObject.SetActive(false);
                this.tomeCards[indexTomeCard].ShowButtons(false);
              }
              GameObject gameObject3 = !((UnityEngine.Object) this.cardGOGold[indexTomeCard] == (UnityEngine.Object) null) ? this.cardGOGold[indexTomeCard] : UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.tomeTs[indexTomeCard]);
              if (Globals.Instance.Cards.ContainsKey(card + "b"))
              {
                flag2 = true;
                gameObject3.gameObject.SetActive(true);
                CardItem component3 = gameObject3.GetComponent<CardItem>();
                gameObject3.name = "cardGold_" + i.ToString();
                component3.SetCard(card + "b", GetFromGlobal: true);
                component3.SetLocalScale(new Vector3(0.25f, 0.08f, 1f));
                component3.CreateColliderAdjusted();
                component3.ShowBackImage(true);
                component3.cardoutsidecombat = true;
                component3.cardoutsidelibary = true;
                component3.TopLayeringOrder("UI", 30100);
                gameObject3.transform.localPosition = new Vector3(0.02f, y, 0.0f);
                this.cardGOGold[indexTomeCard] = gameObject3;
              }
              else
              {
                gameObject3.gameObject.SetActive(false);
                this.tomeCards[indexTomeCard].ShowButtons(false);
              }
              GameObject gameObject4 = !((UnityEngine.Object) this.cardGORare[indexTomeCard] == (UnityEngine.Object) null) ? this.cardGORare[indexTomeCard] : UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.tomeTs[indexTomeCard]);
              if (Globals.Instance.Cards.ContainsKey(card + "rare"))
              {
                flag3 = true;
                gameObject4.gameObject.SetActive(true);
                this.tomeCards[indexTomeCard].ShowButtonRare(true);
                CardItem component4 = gameObject4.GetComponent<CardItem>();
                gameObject4.name = "cardRare_" + i.ToString();
                component4.SetCard(card + "rare", GetFromGlobal: true);
                component4.SetLocalScale(new Vector3(0.25f, 0.08f, 1f));
                component4.CreateColliderAdjusted();
                component4.ShowBackImage(true);
                component4.cardoutsidecombat = true;
                component4.cardoutsidelibary = true;
                component4.TopLayeringOrder("UI", 30100);
                gameObject4.transform.localPosition = new Vector3(0.6f, y, 0.0f);
                this.cardGORare[indexTomeCard] = gameObject4;
              }
              else
              {
                gameObject4.gameObject.SetActive(false);
                this.tomeCards[indexTomeCard].ShowButtonRare(false);
              }
              if (flag1 & flag2 & flag3)
              {
                this.tomeCards[indexTomeCard].buttonBlue.transform.localPosition = new Vector3(-0.55f, y, 0.0f);
                gameObject2.transform.localPosition = new Vector3(-0.58f, y, 0.0f);
                this.tomeCards[indexTomeCard].buttonGold.transform.localPosition = new Vector3(0.05f, y, 0.0f);
                gameObject3.transform.localPosition = new Vector3(0.02f, y, 0.0f);
                this.tomeCards[indexTomeCard].buttonRare.transform.localPosition = new Vector3(0.65f, y, 0.0f);
                gameObject4.transform.localPosition = new Vector3(0.62f, y, 0.0f);
              }
              else if (flag1 & flag2 && !flag3)
              {
                this.tomeCards[indexTomeCard].buttonBlue.transform.localPosition = new Vector3(-0.25f, y, 0.0f);
                gameObject2.transform.localPosition = new Vector3(-0.28f, y, 0.0f);
                this.tomeCards[indexTomeCard].buttonGold.transform.localPosition = new Vector3(0.41f, y, 0.0f);
                gameObject3.transform.localPosition = new Vector3(0.38f, y, 0.0f);
              }
              else if (((flag1 ? 0 : (!flag2 ? 1 : 0)) & (flag3 ? 1 : 0)) != 0)
              {
                this.tomeCards[indexTomeCard].buttonRare.transform.localPosition = new Vector3(0.03f, y, 0.0f);
                gameObject4.transform.localPosition = new Vector3(0.0f, y, 0.0f);
              }
            }
          }
          yield return (object) null;
        }
        else if (this.tomeTs[indexTomeCard].gameObject.activeSelf)
          this.tomeTs[indexTomeCard].gameObject.SetActive(false);
      }
    }
  }

  public void ShowSearchFocus() => this.StartCoroutine(this.ShowSearchFocusCo());

  private IEnumerator ShowSearchFocusCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    if (this.searchInput.text == "")
      this.searchInputPlaceholder.GetComponent<TextMeshProUGUI>().enabled = false;
  }

  public void ShowSearch(bool state) => this.searchBox.gameObject.SetActive(state);

  public void ResetSearch() => this.searchInput.text = "";

  public void Search(string _term)
  {
    _term = _term.Trim();
    if (this.searchCo != null)
      this.StopCoroutine(this.searchCo);
    this.searchCo = this.StartCoroutine(this.SearchCoroutine(_term.Trim()));
  }

  private IEnumerator SearchCoroutine(string _term)
  {
    if (_term != "")
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.35f);
      this.searchTerm = _term;
      this.canvasSearchCloseT.gameObject.SetActive(true);
    }
    else
    {
      this.searchTerm = "";
      this.canvasSearchCloseT.gameObject.SetActive(false);
    }
    this.SelectTomeCards(this.activeTomeCards, true);
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    int controllerBlockFrom = -1)
  {
    this._controllerList.Clear();
    foreach (Transform tomeSideButton in this.tomeSideButtons)
    {
      if (Functions.TransformIsVisible(tomeSideButton))
        this._controllerList.Add(tomeSideButton);
    }
    if (Functions.TransformIsVisible(this.cardContainer))
    {
      foreach (Transform transform in this.cardContainer)
      {
        if (transform.gameObject.activeSelf)
          this._controllerList.Add(transform);
      }
    }
    if (Functions.TransformIsVisible(this.searchInput.transform))
    {
      this._controllerList.Add(this.searchInput.transform);
      if (Functions.TransformIsVisible(this.canvasSearchCloseT.transform))
        this._controllerList.Add(this.canvasSearchCloseT.transform);
    }
    if (Functions.TransformIsVisible(this.glossarySection))
    {
      foreach (Transform transform in this.glossaryPageIndex)
      {
        if (Functions.TransformIsVisible(transform) && transform.GetChild(0).GetComponent<BotonGeneric>().auxInt > -1)
          this._controllerList.Add(transform.GetChild(0).transform);
      }
      foreach (Transform transform in this.glossaryPageIndex2)
      {
        if (Functions.TransformIsVisible(transform) && transform.GetChild(0).GetComponent<BotonGeneric>().auxInt > -1)
          this._controllerList.Add(transform.GetChild(0).transform);
      }
    }
    if (Functions.TransformIsVisible(this.runDetails))
    {
      TomeRunDetails component = this.runDetails.GetComponent<TomeRunDetails>();
      for (int index = 0; index < component.tomeButtons.Length; ++index)
      {
        if (Functions.TransformIsVisible(component.tomeButtons[index].transform))
          this._controllerList.Add(component.tomeButtons[index].transform);
      }
      for (int index = 0; index < component.characters.Length; ++index)
      {
        if (Functions.TransformIsVisible(component.characters[index].transform))
          this._controllerList.Add(component.charactersButtons[index].transform);
      }
      if (Functions.TransformIsVisible(component.pathNext))
        this._controllerList.Add(component.pathNext);
      if (Functions.TransformIsVisible(component.pathPrev))
        this._controllerList.Add(component.pathPrev);
      this._controllerList.Add(component.closeButton);
    }
    else if (Functions.TransformIsVisible(this.runsSection))
    {
      foreach (Transform transform in this.runsContainer)
        this._controllerList.Add(transform);
    }
    if (Functions.TransformIsVisible(this.scoreboardSection))
    {
      for (int index = 0; index < this.scoreButtons.Length; ++index)
        this._controllerList.Add(this.scoreButtons[index]);
    }
    for (int index = 0; index < this.TomeButtons.Length; ++index)
    {
      if (Functions.TransformIsVisible(this.TomeButtons[index].transform))
        this._controllerList.Add(this.TomeButtons[index].transform);
    }
    for (int index = 0; index < this.tomeNumbers.Length; ++index)
    {
      if (Functions.TransformIsVisible(this.tomeNumbers[index].transform) && this.tomeNumbers[index].GetComponent<global::TomeNumber>().IsVisible())
        this._controllerList.Add(this.tomeNumbers[index].transform);
    }
    this._controllerList.Add(this.exitButton);
    this.controllerHorizontalIndex = Functions.GetListClosestIndexToMousePosition(this._controllerList);
    this.controllerHorizontalIndex = Functions.GetClosestIndexBasedOnDirection(this._controllerList, this.controllerHorizontalIndex, goingUp, goingRight, goingDown, goingLeft);
    if (!((UnityEngine.Object) this._controllerList[this.controllerHorizontalIndex] != (UnityEngine.Object) null))
      return;
    this.warpPosition = (Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this._controllerList[this.controllerHorizontalIndex].position);
    Mouse.current.WarpCursorPosition(this.warpPosition);
  }

  public void ControllerMoveShoulder(bool _isRight = false)
  {
  }
}
