// Decompiled with JetBrains decompiler
// Type: Reply
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class Reply : MonoBehaviour
{
  public Transform replyContainer;
  public Transform eventTrackContainer;
  public GameObject eventTrackPrefab;
  public SpriteRenderer replyImage;
  public TMP_Text replyText;
  public Transform probDice;
  public PopupText probPopup;
  public Transform replyChar0;
  public Transform replyChar1;
  public Transform replyChar2;
  public Transform replyChar3;
  public Transform replyButtonBlocked;
  public SpriteRenderer replyBg;
  public SpriteRenderer replyBgButton;
  private SpriteRenderer replyChar0Spr;
  private SpriteRenderer replyChar1Spr;
  private SpriteRenderer replyChar2Spr;
  private SpriteRenderer replyChar3Spr;
  public Transform replyRoll;
  public TMP_Text replyRollText;
  public Transform replyRollBox;
  public TMP_Text replyBoxText;
  public TMP_Text dlcText;
  private Color colorButton;
  private Color colorText;
  private Color colorHover;
  private Color colorOff;
  private Animator anim;
  private CardItem CI;
  private EventReplyData eventReplyData;
  private int optionIndex;
  private bool selected;
  private bool thereIsRoll;
  private bool blocked;
  private bool showGoldMsg;
  private int modRollMadness;

  private void Awake()
  {
    this.colorText = this.replyText.color;
    this.colorButton = this.replyBg.color;
    this.colorOff = new Color(this.colorButton.r, this.colorButton.g, this.colorButton.b, 0.2f);
    this.colorHover = new Color(this.colorButton.r, this.colorButton.g, this.colorButton.b, 0.7f);
    this.replyBgButton.color = this.colorOff;
    this.replyChar0Spr = this.replyChar0.GetComponent<SpriteRenderer>();
    this.replyChar1Spr = this.replyChar1.GetComponent<SpriteRenderer>();
    this.replyChar2Spr = this.replyChar2.GetComponent<SpriteRenderer>();
    this.replyChar3Spr = this.replyChar3.GetComponent<SpriteRenderer>();
    this.CI = this.GetComponent<CardItem>();
  }

  public int GetOptionIndex() => this.optionIndex;

  public void DisableOption() => this.blocked = true;

  public void Block(bool _showRedLayer = true, bool _showGoldShardMessage = true)
  {
    this.blocked = true;
    if (_showGoldShardMessage)
      this.showGoldMsg = _showRedLayer;
    if (!_showRedLayer)
      return;
    this.replyButtonBlocked.gameObject.SetActive(true);
  }

  public void SetImage(Sprite image) => this.replyImage.sprite = image;

  public EventReplyData GetEventReplyData() => this.eventReplyData;

  public void Init(string _eventId, int _replyIndex, int _replyOrder)
  {
    this.anim = this.GetComponent<Animator>();
    this.optionIndex = _replyIndex;
    EventData eventData = Globals.Instance.GetEventData(_eventId);
    this.eventReplyData = eventData.Replys[_replyIndex];
    if (AtOManager.Instance.GetNgPlus() >= 4 || AtOManager.Instance.IsChallengeTraitActive("unlucky"))
      this.modRollMadness = 1;
    else if (AtOManager.Instance.IsChallengeTraitActive("lucky"))
      this.modRollMadness = -1;
    bool flag = false;
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    this.probDice.gameObject.SetActive(false);
    if (this.eventReplyData.ReplyActionText != Enums.EventAction.None)
    {
      string str = "";
      if (this.eventReplyData.ReplyActionText == Enums.EventAction.CharacterName)
      {
        if ((UnityEngine.Object) this.eventReplyData.RequiredClass != (UnityEngine.Object) null)
        {
          this.replyImage.sprite = Globals.Instance.GetSubClassData(this.eventReplyData.RequiredClass.Id).SpriteBorderSmall;
          this.replyText.margin = new Vector4(0.4f, 0.05f, 0.0f, 0.05f);
          flag = true;
        }
      }
      else
        str = Texts.Instance.GetText(Enum.GetName(typeof (Enums.EventAction), (object) this.eventReplyData.ReplyActionText) + "Action");
      if (str != "")
      {
        stringBuilder1.Append("<color=#333>[");
        stringBuilder1.Append(str);
        if (this.eventReplyData.ReplyActionText == Enums.EventAction.Rest)
        {
          stringBuilder1.Append(" <size=-.4><color=#11490E>+");
          stringBuilder1.Append(this.eventReplyData.SsRewardHealthPercent.ToString());
          stringBuilder1.Append("% <sprite name=heart></color></size>");
        }
        stringBuilder1.Append("]</color> ");
      }
    }
    StringBuilder stringBuilder3 = new StringBuilder();
    stringBuilder3.Append(eventData.EventId);
    stringBuilder3.Append("_rp");
    stringBuilder3.Append(this.eventReplyData.IndexForAnswerTranslation);
    string text = Texts.Instance.GetText(stringBuilder3.ToString(), "events");
    if (text != "")
    {
      stringBuilder1.Append(text);
      if (this.eventReplyData.RequirementSku != "")
      {
        this.dlcText.gameObject.SetActive(true);
        this.dlcText.text = string.Format(Texts.Instance.GetText("requiredDLC").Replace("#FFF", "#46291A"), (object) SteamManager.Instance.GetDLCName(this.eventReplyData.RequirementSku));
      }
    }
    else
      stringBuilder1.Append(this.eventReplyData.ReplyText);
    if (flag)
    {
      stringBuilder1.Insert(0, '"');
      stringBuilder1.Append('"');
    }
    this.replyText.text = stringBuilder1.ToString();
    this.replyText.text = this.ParseTextToApplyNumericModifications(this.replyText.text);
    PopupText component = this.replyRoll.GetComponent<PopupText>();
    if (this.eventReplyData.SsRoll)
    {
      this.replyRoll.gameObject.SetActive(true);
      this.replyRollText.text = "";
      string name1 = Enum.GetName(typeof (Enums.RollMode), (object) this.eventReplyData.SsRollMode);
      string name2 = Enum.GetName(typeof (Enums.RollTarget), (object) this.eventReplyData.SsRollTarget);
      stringBuilder1.Clear();
      stringBuilder1.Append("<size=6><sprite name=cards></size><voffset=.7>");
      if (this.eventReplyData.SsRollTarget != Enums.RollTarget.Character)
      {
        stringBuilder1.Append(Texts.Instance.GetText(name2));
        stringBuilder1.Append("  ");
      }
      stringBuilder1.Append("<color=#CCC>[ ");
      StringBuilder stringBuilder4 = new StringBuilder();
      if (this.eventReplyData.SsRollCard != Enums.CardType.None)
      {
        stringBuilder4.Append("<color=#FFAA00><size=+.2>");
        stringBuilder4.Append(Texts.Instance.GetText(Enum.GetName(typeof (Enums.CardType), (object) this.eventReplyData.SsRollCard)));
        stringBuilder4.Append("</size></color>");
        stringBuilder1.Append(string.Format(Texts.Instance.GetText("rollCard"), (object) stringBuilder4.ToString()));
        stringBuilder2.Append(EventManager.Instance.GetProbabilityType(this.eventReplyData.SsRollCard, this.eventReplyData.RequiredClass.Id));
        stringBuilder2.Append("%");
      }
      else if (name1 == "HigherOrEqual" || name1 == "LowerOrEqual")
      {
        stringBuilder4.Append("<color=#FFAA00><b><size=+.2>");
        int ssRollNumber = this.eventReplyData.SsRollNumber;
        int result = !(name1 == "HigherOrEqual") ? ssRollNumber - this.modRollMadness : ssRollNumber + this.modRollMadness;
        stringBuilder4.Append(result);
        stringBuilder4.Append("</size></b></color>");
        stringBuilder1.Append(string.Format(Texts.Instance.GetText(name1), (object) stringBuilder4.ToString()));
        if (name2 == "Single")
        {
          Hero[] heroes = EventManager.Instance.Heroes;
          if (heroes[0] != null && (UnityEngine.Object) heroes[0].HeroData != (UnityEngine.Object) null)
          {
            stringBuilder2.Append("<size=+6><color=#AAA>");
            stringBuilder2.Append(heroes[0].SourceName);
            stringBuilder2.Append("</color></size> ");
            stringBuilder2.Append(EventManager.Instance.GetProbabilitySingle(result, name1 == "HigherOrEqual", 0));
            stringBuilder2.Append("%<br>");
          }
          if (heroes[1] != null && (UnityEngine.Object) heroes[1].HeroData != (UnityEngine.Object) null)
          {
            stringBuilder2.Append("<size=+6><color=#AAA>");
            stringBuilder2.Append(heroes[1].SourceName);
            stringBuilder2.Append("</color></size> ");
            stringBuilder2.Append(EventManager.Instance.GetProbabilitySingle(result, name1 == "HigherOrEqual", 1));
            stringBuilder2.Append("%<br>");
          }
          if (heroes[2] != null && (UnityEngine.Object) heroes[2].HeroData != (UnityEngine.Object) null)
          {
            stringBuilder2.Append("<size=+6><color=#AAA>");
            stringBuilder2.Append(heroes[2].SourceName);
            stringBuilder2.Append("</color></size> ");
            stringBuilder2.Append(EventManager.Instance.GetProbabilitySingle(result, name1 == "HigherOrEqual", 2));
            stringBuilder2.Append("%<br>");
          }
          if (heroes[3] != null && (UnityEngine.Object) heroes[3].HeroData != (UnityEngine.Object) null)
          {
            stringBuilder2.Append("<size=+6><color=#AAA>");
            stringBuilder2.Append(heroes[3].SourceName);
            stringBuilder2.Append("</color></size> ");
            stringBuilder2.Append(EventManager.Instance.GetProbabilitySingle(result, name1 == "HigherOrEqual", 3));
            stringBuilder2.Append("%");
          }
        }
        else
        {
          stringBuilder2.Append(EventManager.Instance.GetProbability(result, name1 == "HigherOrEqual"));
          stringBuilder2.Append("%");
        }
      }
      else
        stringBuilder1.Append(Texts.Instance.GetText(name1));
      stringBuilder1.Append(" ] ");
      if (stringBuilder2.Length > 0)
      {
        stringBuilder2.Insert(0, "<br><color=#FFAA00><size=+10>");
        this.probPopup.text = "<size=+3>" + string.Format(Texts.Instance.GetText("replyProbability"), (object) stringBuilder2.ToString());
        this.probDice.gameObject.SetActive(true);
      }
      this.replyRollText.text = stringBuilder1.ToString();
      switch (name2)
      {
        case "Single":
          component.SetId("singleDesc");
          break;
        case "Group":
          component.SetId("groupDesc");
          break;
        case "Competition":
          component.SetId("competitionDesc");
          break;
      }
      this.thereIsRoll = true;
    }
    else
    {
      this.replyRoll.gameObject.SetActive(false);
      UnityEngine.Object.Destroy((UnityEngine.Object) component);
    }
    this.ShowRequirementTracks();
    this.replyRollBox.gameObject.SetActive(false);
    this.StartCoroutine(this.ShowAnim(_replyOrder));
  }

  private string ParseTextToApplyNumericModifications(string input)
  {
    if (this.eventReplyData.SsGoldReward <= 0 && this.eventReplyData.SscGoldReward <= 0 && this.eventReplyData.FlGoldReward <= 0 && this.eventReplyData.FlcGoldReward <= 0 || this.eventReplyData.GoldCost != 0 || this.eventReplyData.DustCost != 0)
      return input;
    float modifier = 1f;
    if (MadnessManager.Instance.IsMadnessTraitActive("poverty") || AtOManager.Instance.IsChallengeTraitActive("poverty"))
      modifier = !GameManager.Instance.IsObeliskChallenge() ? 0.5f : 0.3f;
    float result;
    return new Regex("\\b\\d+(?:[,.]\\d+)?g\\b").Replace(input, (MatchEvaluator) (m => float.TryParse(m.Value.Replace("g", "").Replace(".", "").Replace(",", ""), out result) ? (result * modifier).ToString("#,##0") + "g" : m.Value));
  }

  private IEnumerator ShowAnim(int _replyOrder)
  {
    if (this.thereIsRoll && !GameManager.Instance.TutorialWatched("eventRolls"))
      GameManager.Instance.ShowTutorialPopup("eventRolls", this.replyRollText.transform.position, Vector3.zero);
    yield return (object) null;
  }

  private void ShowRequirementTracks()
  {
    if (!((UnityEngine.Object) this.eventReplyData.SsNodeTravel != (UnityEngine.Object) null))
      return;
    List<string> playerRequeriments = AtOManager.Instance.GetPlayerRequeriments();
    for (int index = 0; index < playerRequeriments.Count; ++index)
    {
      EventRequirementData requirementData = Globals.Instance.GetRequirementData(playerRequeriments[index]);
      if (requirementData.RequirementTrack && requirementData.CanShowRequeriment(Enums.Zone.None, AtOManager.Instance.GetMapZone(this.eventReplyData.SsNodeTravel.NodeId)))
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.eventTrackPrefab, Vector3.zero, Quaternion.identity, this.eventTrackContainer);
        gameObject.transform.localPosition = new Vector3(0.0f, 0.0f, -3f);
        gameObject.transform.GetComponent<EventTrack>().SetTrack(playerRequeriments[index]);
      }
    }
  }

  public void SelectedByMultiplayer(string nick)
  {
    if (!this.replyChar0.gameObject.activeSelf)
    {
      this.replyChar0.gameObject.SetActive(true);
      this.replyChar0Spr.color = Functions.HexToColor(NetworkManager.Instance.GetColorFromNick(nick));
    }
    else if (!this.replyChar1.gameObject.activeSelf)
    {
      this.replyChar1.gameObject.SetActive(true);
      this.replyChar1Spr.color = Functions.HexToColor(NetworkManager.Instance.GetColorFromNick(nick));
    }
    else if (!this.replyChar2.gameObject.activeSelf)
    {
      this.replyChar2.gameObject.SetActive(true);
      this.replyChar2Spr.color = Functions.HexToColor(NetworkManager.Instance.GetColorFromNick(nick));
    }
    else
    {
      if (this.replyChar3.gameObject.activeSelf)
        return;
      this.replyChar3.gameObject.SetActive(true);
      this.replyChar3Spr.color = Functions.HexToColor(NetworkManager.Instance.GetColorFromNick(nick));
    }
  }

  public void SetRollBox(string rollText)
  {
    if (this.eventReplyData.SsRollCard == Enums.CardType.None)
    {
      this.replyBoxText.text = rollText;
      this.replyRollBox.gameObject.SetActive(true);
    }
    else
      this.HideRollBox();
  }

  public void HideRollBox() => this.replyRollBox.gameObject.SetActive(false);

  private void ShowSelectedDesign()
  {
    TMP_Text replyText = this.replyText;
    SpriteRenderer replyImage = this.replyImage;
    Color color1 = new Color(1f, 1f, 1f, 1f);
    Color color2 = color1;
    replyImage.color = color2;
    Color color3 = color1;
    replyText.color = color3;
    this.replyBgButton.color = this.colorHover;
  }

  private void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || MapManager.Instance.characterWindow.gameObject.activeSelf && MapManager.Instance.characterWindow.IsActive())
      return;
    if (this.blocked)
    {
      if (this.showGoldMsg)
        PopupManager.Instance.SetText(Texts.Instance.GetText("notEnoughGold"), true);
    }
    else
    {
      GameManager.Instance.SetCursorHover();
      this.ShowSelectedDesign();
    }
    if ((UnityEngine.Object) this.eventReplyData.ReplyShowCard != (UnityEngine.Object) null && !this.blocked)
    {
      this.CI.cardoutsideverticallist = true;
      this.CI.CreateAmplifyOutsideCard(Globals.Instance.GetCardData(this.eventReplyData.ReplyShowCard.Id, false), this.GetComponent<BoxCollider2D>());
    }
    else if (this.eventReplyData.SsCorruptItemSlot != Enums.ItemSlot.None && !this.blocked)
    {
      SubClassData subClassData = Globals.Instance.GetSubClassData(this.eventReplyData.RequiredClass.Id);
      if (!((UnityEngine.Object) subClassData != (UnityEngine.Object) null))
        return;
      Hero[] heroes = EventManager.Instance.Heroes;
      int index1 = -1;
      for (int index2 = 0; index2 < heroes.Length; ++index2)
      {
        if (heroes[index2] != null && (UnityEngine.Object) heroes[index2].HeroData != (UnityEngine.Object) null && heroes[index2].HeroData.HeroSubClass.Id == subClassData.Id)
        {
          index1 = index2;
          break;
        }
      }
      if (index1 <= -1)
        return;
      CardData cardData = (CardData) null;
      if (this.eventReplyData.SsCorruptItemSlot == Enums.ItemSlot.Weapon && heroes[index1].Weapon != "")
        cardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(heroes[index1].Weapon, false), "");
      else if (this.eventReplyData.SsCorruptItemSlot == Enums.ItemSlot.Armor && heroes[index1].Armor != "")
        cardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(heroes[index1].Armor, false), "");
      else if (this.eventReplyData.SsCorruptItemSlot == Enums.ItemSlot.Jewelry && heroes[index1].Jewelry != "")
        cardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(heroes[index1].Jewelry, false), "");
      else if (this.eventReplyData.SsCorruptItemSlot == Enums.ItemSlot.Accesory && heroes[index1].Accesory != "")
        cardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(heroes[index1].Accesory, false), "");
      else if (this.eventReplyData.SsCorruptItemSlot == Enums.ItemSlot.Pet && heroes[index1].Pet != "")
        cardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(heroes[index1].Pet, false), "");
      if (!((UnityEngine.Object) cardData != (UnityEngine.Object) null) || !((UnityEngine.Object) cardData.UpgradesToRare != (UnityEngine.Object) null))
        return;
      this.CI.cardoutsideverticallist = true;
      this.CI.CreateAmplifyOutsideCard(Globals.Instance.GetCardData(cardData.UpgradesToRare.Id, false), this.GetComponent<BoxCollider2D>(), heroes[index1]);
    }
    else
    {
      if (this.eventReplyData.SsRemoveItemSlot == Enums.ItemSlot.None || this.blocked)
        return;
      SubClassData subClassData = Globals.Instance.GetSubClassData(this.eventReplyData.RequiredClass.Id);
      if (!((UnityEngine.Object) subClassData != (UnityEngine.Object) null))
        return;
      Hero[] heroes = EventManager.Instance.Heroes;
      int index3 = -1;
      for (int index4 = 0; index4 < heroes.Length; ++index4)
      {
        if (heroes[index4] != null && (UnityEngine.Object) heroes[index4].HeroData != (UnityEngine.Object) null && heroes[index4].HeroData.HeroSubClass.Id == subClassData.Id)
        {
          index3 = index4;
          break;
        }
      }
      if (index3 <= -1)
        return;
      CardData cardData = (CardData) null;
      if (this.eventReplyData.SsRemoveItemSlot == Enums.ItemSlot.Weapon && heroes[index3].Weapon != "")
        cardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(heroes[index3].Weapon, false), "");
      else if (this.eventReplyData.SsRemoveItemSlot == Enums.ItemSlot.Armor && heroes[index3].Armor != "")
        cardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(heroes[index3].Armor, false), "");
      else if (this.eventReplyData.SsRemoveItemSlot == Enums.ItemSlot.Jewelry && heroes[index3].Jewelry != "")
        cardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(heroes[index3].Jewelry, false), "");
      else if (this.eventReplyData.SsRemoveItemSlot == Enums.ItemSlot.Accesory && heroes[index3].Accesory != "")
        cardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(heroes[index3].Accesory, false), "");
      else if (this.eventReplyData.SsRemoveItemSlot == Enums.ItemSlot.Pet && heroes[index3].Pet != "")
        cardData = Functions.GetCardDataFromCardData(Globals.Instance.GetCardData(heroes[index3].Pet, false), "");
      if (!((UnityEngine.Object) cardData != (UnityEngine.Object) null))
        return;
      this.CI.cardoutsideverticallist = true;
      this.CI.CreateAmplifyOutsideCard(Globals.Instance.GetCardData(cardData.Id, false), this.GetComponent<BoxCollider2D>(), heroes[index3]);
    }
  }

  private void OnMouseExit()
  {
    GameManager.Instance.SetCursorPlain();
    GameManager.Instance.CleanTempContainer();
    PopupManager.Instance.ClosePopup();
    if (this.selected)
      return;
    this.replyText.color = this.colorText;
    this.replyBgButton.color = this.colorOff;
  }

  public void OnMouseUp()
  {
    if (AlertManager.Instance.IsActive() || GameManager.Instance.IsTutorialActive() || SettingsManager.Instance.IsActive() || DamageMeterManager.Instance.IsActive() || MapManager.Instance.characterWindow.gameObject.activeSelf && MapManager.Instance.characterWindow.IsActive() || this.blocked || GameManager.Instance.IsMultiplayer() && !NetworkManager.Instance.IsMaster() && AtOManager.Instance.followingTheLeader || !Functions.ClickedThisTransform(this.transform) || EventManager.Instance.optionSelected != -1)
      return;
    this.SelectThisOption();
  }

  public void SelectThisOption()
  {
    EventManager.Instance.SelectOption(this.optionIndex);
    this.selected = true;
    this.ShowSelectedDesign();
    GameManager.Instance.CleanTempContainer();
  }
}
