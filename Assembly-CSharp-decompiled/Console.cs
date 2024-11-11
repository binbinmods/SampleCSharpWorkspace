// Decompiled with JetBrains decompiler
// Type: Console
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
  public TMP_Text logTM;
  private List<string> consoleText = new List<string>();
  private Dictionary<string, string> consoleDict;
  private string key = "";
  public CardItem consoleCardItem;
  public Transform[] characterTransform;
  public Transform characterContainerTransform;
  public Transform cardContainerTransform;
  public Transform cardTransform;
  public TMP_Text characterContainerTitle;
  public Transform characterContainerMsgNone;
  private Image[] characterSprite;
  private Image[] characterSpriteSkull;
  private Image[] characterSpriteBg;
  private TMP_Text[] characterText;
  private Color32 colorBgHero;
  private Color32 colorBgNPC;
  private StringBuilder SBCardEvent = new StringBuilder();
  private string oldHeroCardEvent = "";
  private string oldEntryTo = "";

  private void Awake()
  {
    this.consoleDict = new Dictionary<string, string>();
    this.characterSprite = new Image[this.characterTransform.Length];
    this.characterSpriteSkull = new Image[this.characterTransform.Length];
    this.characterSpriteBg = new Image[this.characterTransform.Length];
    this.characterText = new TMP_Text[this.characterTransform.Length];
    for (int index = 0; index < this.characterTransform.Length; ++index)
    {
      this.characterSpriteBg[index] = this.characterTransform[index].GetChild(0).GetComponent<Image>();
      this.characterSprite[index] = this.characterTransform[index].GetChild(1).GetComponent<Image>();
      this.characterText[index] = this.characterTransform[index].GetChild(2).GetComponent<TMP_Text>();
      this.characterSpriteSkull[index] = this.characterTransform[index].GetChild(3).GetComponent<Image>();
    }
    this.colorBgHero = (Color32) new Color(0.1f, 0.21f, 0.32f, 0.7f);
    this.colorBgNPC = (Color32) new Color(0.27f, 0.08f, 0.08f, 0.7f);
  }

  private void Start() => this.consoleCardItem.DisableTrail();

  public bool IsActive() => this.gameObject.activeSelf;

  public void Show(bool _state)
  {
    if (this.gameObject.activeSelf != _state)
      this.gameObject.SetActive(_state);
    if (!_state)
      return;
    this.DoLog();
  }

  public void DoLog()
  {
    if (!this.gameObject.activeSelf)
      return;
    this.ShowHideCardTransform(false);
    this.ShowHideCharacterTransform(false);
    this.HideCharactersLog(0);
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    int num = 0;
    foreach (KeyValuePair<string, LogEntry> log1 in MatchManager.Instance.LogDictionary)
    {
      if ((log1.Value.logCardId == "" || log1.Value.logActivation == Enums.EventActivation.TraitActivation || log1.Value.logActivation == Enums.EventActivation.ItemActivation) && this.SBCardEvent.Length > 0 && !log1.Key.StartsWith("toHand:") && !log1.Key.StartsWith("toTopDeck:") && !log1.Key.StartsWith("toBottomDeck:") && !log1.Key.StartsWith("toRandomDeck:") && !log1.Key.StartsWith("toDiscard:") && !log1.Key.StartsWith("toVanish:"))
      {
        this.SBCardEvent.Append("</size></margin>");
        this.SBCardEvent.Append("<br>");
        stringBuilder2.Insert(0, this.SBCardEvent.ToString());
        this.SBCardEvent.Clear();
        this.oldHeroCardEvent = "";
        this.oldEntryTo = "";
      }
      if (log1.Value.logActivation != Enums.EventActivation.None)
      {
        string str = "";
        if (log1.Value.logHeroName != "")
          str = log1.Value.logHeroName;
        else if (log1.Value.logNPCName != "")
          str = log1.Value.logNPCName;
        if (log1.Value.logActivation == Enums.EventActivation.BeginTurn)
        {
          if (log1.Key.StartsWith("begineffects:"))
          {
            stringBuilder1.Append(this.TimeStamp(log1.Value.logDateTime));
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("consoleBeginTurn"), (object) str, (object) log1.Key));
            stringBuilder1.Append("<br>");
          }
        }
        else if (log1.Value.logActivation == Enums.EventActivation.EndTurn)
        {
          if (log1.Key.StartsWith("status:"))
          {
            stringBuilder1.Append("<line-height=30%><br><align=center><size=-5><sprite name=sep></size><space=80><sprite name=damage> <link=");
            stringBuilder1.Append(log1.Key);
            stringBuilder1.Append("><color=#A1D8E5><u>");
            stringBuilder1.Append(Texts.Instance.GetText("consoleCombatStatus"));
            stringBuilder1.Append("</u></color></link>  <sprite name=damage><size=-5><space=75><sprite name=sep></size></align><br></line-height>");
            stringBuilder1.Append("<br>");
          }
          else if (log1.Key.StartsWith("endeffects:"))
          {
            stringBuilder1.Append(this.TimeStamp(log1.Value.logDateTime));
            stringBuilder1.Append(string.Format(Texts.Instance.GetText("consoleEndTurn"), (object) str, (object) log1.Key));
            stringBuilder1.Append("<br>");
          }
        }
        else if (log1.Value.logActivation == Enums.EventActivation.Killed)
        {
          stringBuilder1.Append("<margin=50>");
          stringBuilder1.Append("<size=-4>");
          stringBuilder1.Append("<color=");
          stringBuilder1.Append(Globals.Instance.ClassColor["injury"]);
          stringBuilder1.Append(">");
          stringBuilder1.Append(string.Format("<b>{0}</b> dies", (object) str));
          stringBuilder1.Append("</color>");
          stringBuilder1.Append("</size>");
          stringBuilder1.Append("</margin>");
          stringBuilder1.Append("<br>");
        }
        else if (log1.Value.logActivation == Enums.EventActivation.Resurrect)
        {
          stringBuilder1.Append(this.TimeStamp(log1.Value.logDateTime));
          stringBuilder1.Append("<color=");
          stringBuilder1.Append(Globals.Instance.ClassColor["boon"]);
          stringBuilder1.Append(">");
          stringBuilder1.Append(string.Format("<b>{0}</b> resurrects", (object) str));
          stringBuilder1.Append("</color>");
          stringBuilder1.Append("<br>");
        }
        else if (log1.Value.logActivation == Enums.EventActivation.BeginTurnAboutToDealCards)
        {
          StringBuilder stringBuilder3 = new StringBuilder();
          foreach (KeyValuePair<string, LogResult> keyValuePair1 in log1.Value.logResult)
          {
            if (!(log1.Value.logHero.Id != keyValuePair1.Key) && keyValuePair1.Value.logResultDict.Count > 0)
            {
              foreach (KeyValuePair<string, int> keyValuePair2 in keyValuePair1.Value.logResultDict)
              {
                if (keyValuePair2.Key == "inspire" || keyValuePair2.Key == "stress")
                {
                  stringBuilder3.Append("  ");
                  stringBuilder3.Append("<sprite name=");
                  stringBuilder3.Append(keyValuePair2.Key);
                  stringBuilder3.Append(">");
                  stringBuilder3.Append(keyValuePair2.Value);
                }
              }
            }
          }
          stringBuilder1.Append(this.TimeStamp(log1.Value.logDateTime));
          stringBuilder1.Append(string.Format(Texts.Instance.GetText("consoleDraw"), (object) log1.Value.logAuxInt, (object) stringBuilder3.ToString()));
          stringBuilder1.Append("<br>");
        }
      }
      if (log1.Value.logCardId != "")
      {
        if (log1.Key.StartsWith("toHand:") || log1.Key.StartsWith("toTopDeck:") || log1.Key.StartsWith("toBottomDeck:") || log1.Key.StartsWith("toRandomDeck:") || log1.Key.StartsWith("toDiscard:") || log1.Key.StartsWith("toVanish:"))
        {
          string key = log1.Key;
          if (MatchManager.Instance.LogDictionary.ContainsKey(key))
          {
            LogEntry log2 = MatchManager.Instance.LogDictionary[key];
            string str1 = this.DoLogCard(key, log2, log2.logCardId);
            string str2 = "";
            string str3 = "";
            if (log1.Value.logHero != null)
            {
              str2 = log1.Value.logHero.SourceName;
              str3 = log1.Value.logHero.InternalId;
            }
            else if (log1.Value.logNPC != null)
            {
              str2 = log1.Value.logNPC.SourceName;
              str3 = log1.Value.logNPC.InternalId;
            }
            if (this.SBCardEvent.Length > 0 && this.oldEntryTo != "" && this.oldEntryTo != log1.Key.Substring(0, 6))
              this.oldEntryTo = "";
            if (this.SBCardEvent.Length > 0 && this.oldHeroCardEvent == str3 && this.oldEntryTo != "")
            {
              this.SBCardEvent.Append(", ");
            }
            else
            {
              if (this.SBCardEvent.Length == 0)
                this.SBCardEvent.Append("<margin=50><size=-4>");
              else
                this.SBCardEvent.Append("<br>");
              if (log1.Key.StartsWith("toHand:"))
              {
                this.SBCardEvent.Append(string.Format(Texts.Instance.GetText("consoleCardToHand"), (object) str2));
                this.SBCardEvent.Append("  ");
              }
              else if (log1.Key.StartsWith("toTopDeck:"))
              {
                this.SBCardEvent.Append(string.Format(Texts.Instance.GetText("consoleCardToTopDeck"), (object) str2));
                this.SBCardEvent.Append("  ");
              }
              else if (log1.Key.StartsWith("toBottomDeck:"))
              {
                this.SBCardEvent.Append(string.Format(Texts.Instance.GetText("consoleCardToBottomDeck"), (object) str2));
                this.SBCardEvent.Append("  ");
              }
              else if (log1.Key.StartsWith("toRandomDeck:"))
              {
                this.SBCardEvent.Append(string.Format(Texts.Instance.GetText("consoleCardToRandomDeck"), (object) str2));
                this.SBCardEvent.Append("  ");
              }
              else if (log1.Key.StartsWith("toDiscard:"))
              {
                this.SBCardEvent.Append(string.Format(Texts.Instance.GetText("consoleCardToDiscard"), (object) str2));
                this.SBCardEvent.Append("  ");
              }
              else if (log1.Key.StartsWith("toVanish:"))
              {
                this.SBCardEvent.Append(string.Format(Texts.Instance.GetText("consoleCardToVanish"), (object) str2));
                this.SBCardEvent.Append("  ");
              }
              else if (log1.Key.StartsWith("cardModification:"))
              {
                this.SBCardEvent.Append(string.Format(Texts.Instance.GetText("consoleCardModification"), (object) str2));
                this.SBCardEvent.Append("  ");
              }
            }
            this.oldHeroCardEvent = str3;
            this.oldEntryTo = log1.Key.Substring(0, 6);
            this.SBCardEvent.Append(str1);
          }
        }
        else
        {
          if (this.SBCardEvent.Length > 0)
          {
            this.SBCardEvent.Append("</size></margin>");
            this.SBCardEvent.Append("<br>");
            stringBuilder1.Append(this.SBCardEvent.ToString());
            this.SBCardEvent.Clear();
            this.oldHeroCardEvent = "";
            this.oldEntryTo = "";
          }
          string key = log1.Key;
          if (MatchManager.Instance.LogDictionary.ContainsKey(key))
          {
            LogEntry log3 = MatchManager.Instance.LogDictionary[key];
            if (log1.Key.StartsWith("cardModification:"))
            {
              string str = this.DoLogCard(key, log3, log3.logCardId);
              if (str != "")
              {
                stringBuilder1.Append("<margin=50><size=-4>");
                stringBuilder1.Append(string.Format(Texts.Instance.GetText("consoleCardModification"), (object) log1.Value.logHero.SourceName));
                stringBuilder1.Append("  ");
                stringBuilder1.Append(str);
                stringBuilder1.Append("</size></margin>");
                stringBuilder1.Append("<br>");
              }
            }
            else
            {
              string str = this.DoLogCard(key, log3);
              if (str != "")
                stringBuilder1.Append(str);
            }
          }
        }
      }
      if (log1.Key.StartsWith("round:"))
      {
        stringBuilder1.Append(this.TimeStamp(log1.Value.logDateTime));
        stringBuilder1.Append("<color=orange>");
        stringBuilder1.Append(string.Format(Texts.Instance.GetText("roundNumber"), (object) log1.Key.Replace("round:", "")));
        stringBuilder1.Append("</color>");
        stringBuilder1.Append("<line-height=20><br><br></line-height>");
      }
      ++num;
      stringBuilder2.Insert(0, stringBuilder1.ToString());
      stringBuilder1.Clear();
    }
    if (this.SBCardEvent.Length > 0)
    {
      this.SBCardEvent.Append("</size></margin>");
      this.SBCardEvent.Append("<br>");
      stringBuilder2.Insert(0, this.SBCardEvent.ToString());
      this.SBCardEvent.Clear();
      this.oldHeroCardEvent = "";
      this.oldEntryTo = "";
    }
    this.logTM.text = stringBuilder2.ToString();
  }

  public void ShowCard(string _key, string _title)
  {
    bool flag1 = false;
    bool flag2 = true;
    bool _state = true;
    if (_key.StartsWith("crd:"))
      _key = _key.Replace("crd:", "");
    if (_key.StartsWith("plain:"))
      _key = _key.Replace("plain:", "");
    if (_key.StartsWith("log:"))
      _key = _key.Replace("log:", "");
    if (_key.StartsWith("logTrait:"))
      _key = _key.Replace("logTrait:", "");
    if (_key.StartsWith("cardModification:") || _key.StartsWith("toHand:") || _key.StartsWith("toTopDeck:") || _key.StartsWith("toBottomDeck:") || _key.StartsWith("toRandomDeck:") || _key.StartsWith("toDiscard:") || _key.StartsWith("toVanish:"))
      _state = false;
    if (_key.StartsWith("status:"))
    {
      flag1 = true;
      flag2 = false;
    }
    if (_key.StartsWith("begineffects:") || _key.StartsWith("endeffects:"))
      flag2 = false;
    if (!MatchManager.Instance.LogDictionary.ContainsKey(_key))
      return;
    LogEntry log = MatchManager.Instance.LogDictionary[_key];
    if (flag2)
    {
      if (log.logActivation == Enums.EventActivation.TraitActivation)
        flag2 = false;
      else if (log.logActivation != Enums.EventActivation.CorruptionBeginRound)
        this.consoleCardItem.SetCard(log.logCardId, _theHero: log.logHero, _theNPC: log.logNPC);
      else
        this.consoleCardItem.SetCard(log.logCardId, GetFromGlobal: true);
      this.consoleCardItem.TopLayeringOrder("UI", 30200);
      this.consoleCardItem.SetLocalScale(new Vector3(1.2f, 1.2f, 1f));
      this.consoleCardItem.HideKeyNotes();
    }
    int index = 0;
    if (_state)
    {
      foreach (KeyValuePair<string, LogResult> keyValuePair1 in log.logResult)
      {
        if (keyValuePair1.Value.logResultDict.Count > 0 && (!keyValuePair1.Value.logResultDict.ContainsKey("hp") || keyValuePair1.Value.logResultDict["hp"] != 0))
        {
          this.ShowCharactersLog(index);
          StringBuilder stringBuilder1 = new StringBuilder();
          Character heroById = (Character) MatchManager.Instance.GetHeroById(keyValuePair1.Key);
          this.characterSprite[index].sprite = keyValuePair1.Value.logResultSprite;
          this.characterSpriteSkull[index].gameObject.SetActive(false);
          stringBuilder1.Append("<size=20>");
          if (heroById != null)
          {
            stringBuilder1.Append(heroById.SourceName);
            this.characterSpriteBg[index].color = (Color) this.colorBgHero;
          }
          else
          {
            Character npcById = (Character) MatchManager.Instance.GetNPCById(keyValuePair1.Key);
            if (npcById != null)
            {
              stringBuilder1.Append(npcById.SourceName);
              this.characterSpriteBg[index].color = (Color) this.colorBgNPC;
            }
          }
          stringBuilder1.Append("</size><line-height=14><br></line-height><br>");
          bool flag3 = false;
          bool flag4 = false;
          StringBuilder stringBuilder2 = new StringBuilder();
          foreach (KeyValuePair<string, int> keyValuePair2 in keyValuePair1.Value.logResultDict)
          {
            if (keyValuePair2.Key == "hp")
            {
              stringBuilder2.Append("<nobr>");
              stringBuilder2.Append("<sprite name=heart>");
              stringBuilder2.Append("<space=3><size=-3>");
              if (!flag1)
              {
                if (keyValuePair2.Value > 0)
                {
                  stringBuilder2.Append("<color=");
                  stringBuilder2.Append(Globals.Instance.ClassColor["scout"]);
                  stringBuilder2.Append(">");
                  stringBuilder2.Append("+");
                }
                else
                {
                  stringBuilder2.Append("<color=");
                  stringBuilder2.Append(Globals.Instance.ClassColor["warrior"]);
                  stringBuilder2.Append(">");
                }
                stringBuilder2.Append(keyValuePair2.Value);
                stringBuilder2.Append("</color>");
              }
              else
                stringBuilder2.Append(keyValuePair2.Value);
              stringBuilder2.Append("</size>");
              if (!flag1)
              {
                stringBuilder2.Append(" <size=-5><color=#AAA>(");
                stringBuilder2.Append(keyValuePair1.Value.logResultDict["hpCurrent"]);
                stringBuilder2.Append(")</color></size>");
                if (keyValuePair1.Value.logResultDict["hpCurrent"] <= 0)
                  this.characterSpriteSkull[index].gameObject.SetActive(true);
              }
              stringBuilder2.Append("</nobr>");
              flag3 = true;
            }
            else if (!(keyValuePair2.Key == "hpCurrent") && !(keyValuePair2.Key == "stealthbonus"))
            {
              if (flag3 | flag4)
                stringBuilder2.Append("<space=5><size=15><color=#666><voffset=4>|</voffset></color></size><space=8>");
              stringBuilder2.Append("<nobr><sprite name=");
              stringBuilder2.Append(keyValuePair2.Key);
              stringBuilder2.Append(">");
              stringBuilder2.Append("<space=-3><size=-3><color=#AAA> ");
              if (keyValuePair2.Value > 0 && !flag1)
                stringBuilder2.Append("+");
              stringBuilder2.Append(keyValuePair2.Value);
              stringBuilder2.Append("</color></size>");
              stringBuilder2.Append("</nobr>");
              flag4 = true;
            }
          }
          if (stringBuilder2.Length > 0)
          {
            stringBuilder1.Append(stringBuilder2.ToString());
            this.characterText[index].text = stringBuilder1.ToString();
            ++index;
          }
        }
      }
      this.HideCharactersLog(index);
    }
    if (flag2)
      this.ShowHideCardTransform(true);
    else
      this.ShowHideCardTransform(false);
    this.ShowHideCharacterTransform(_state);
    if (index > 0)
      this.characterContainerMsgNone.gameObject.SetActive(false);
    else
      this.characterContainerMsgNone.gameObject.SetActive(true);
    this.characterContainerTitle.text = _title;
  }

  private void ShowCharactersLog(int _target)
  {
    if (this.characterTransform[_target].gameObject.activeSelf)
      return;
    this.characterTransform[_target].gameObject.SetActive(true);
  }

  private void HideCharactersLog(int _base)
  {
    for (int index = _base; index < 8; ++index)
    {
      if (this.characterTransform[index].gameObject.activeSelf)
        this.characterTransform[index].gameObject.SetActive(false);
    }
  }

  private void ShowHideCardTransform(bool _state)
  {
    if (this.cardTransform.gameObject.activeSelf != _state)
      this.cardTransform.gameObject.SetActive(_state);
    if (this.cardContainerTransform.gameObject.activeSelf == _state)
      return;
    this.cardContainerTransform.gameObject.SetActive(_state);
  }

  private void ShowHideCharacterTransform(bool _state)
  {
    if (this.characterContainerTransform.gameObject.activeSelf == _state)
      return;
    this.characterContainerTransform.gameObject.SetActive(_state);
  }

  public void HideLogCard()
  {
    PopupManager.Instance.ClosePopup();
    this.ShowHideCardTransform(false);
    this.ShowHideCharacterTransform(false);
  }

  public string DoLogCard(string _key, LogEntry _logEntry, string _cardId = "")
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    if (_logEntry.logActivation == Enums.EventActivation.CorruptionBeginRound)
    {
      stringBuilder1.Append("<margin=50>");
      stringBuilder1.Append("<size=-4>");
      stringBuilder1.Append("<color=");
      stringBuilder1.Append(Globals.Instance.ClassColor["magicknight"]);
      stringBuilder1.Append(">");
      stringBuilder1.Append(Texts.Instance.GetText("consoleCorruptionActivated"));
      stringBuilder1.Append("</color>  <sprite name=cards><link=log:{1}><u>{2}</u></link>");
      stringBuilder1.Append("</size>");
      stringBuilder1.Append("</margin>");
      stringBuilder1.Append("<br>");
      if (_logEntry.logHero == null && _logEntry.logNPC == null)
        stringBuilder1.Append("<line-height=20%><br></line-height>");
    }
    else if (_logEntry.logActivation == Enums.EventActivation.ItemActivation)
    {
      if (_logEntry.logHero != null || _logEntry.logNPC != null)
        stringBuilder1.Append("<line-height=20%><br></line-height>");
      stringBuilder1.Append("<margin=50>");
      stringBuilder1.Append("<size=-4>");
      stringBuilder1.Append("<color=#A29279>");
      stringBuilder1.Append(Texts.Instance.GetText("consoleItemActivated"));
      stringBuilder1.Append("</color>  <sprite name=cards><link=log:{1}><u>{2}</u></link>");
      stringBuilder1.Append("</size>");
      stringBuilder1.Append("</margin>");
      stringBuilder1.Append("<br>");
    }
    else if (_logEntry.logActivation == Enums.EventActivation.TraitActivation)
    {
      if (_logEntry.logHero != null || _logEntry.logNPC != null)
        stringBuilder1.Append("<line-height=20%><br></line-height>");
      stringBuilder1.Append("<margin=50>");
      stringBuilder1.Append("<size=-4>");
      stringBuilder1.Append("<color=#A29279>");
      stringBuilder1.Append(Texts.Instance.GetText("consoleTraitActivated"));
      stringBuilder1.Append("</color>  <sprite name=experience><link=logTrait:{1}><u>{2}</u></link>");
      stringBuilder1.Append("</size>");
      stringBuilder1.Append("</margin>");
      stringBuilder1.Append("<br>");
    }
    else if (_cardId != "")
    {
      stringBuilder1.Append("<sprite name=cards><link=log:{1}><u>{2}</u></link>");
    }
    else
    {
      stringBuilder1.Append(this.TimeStamp(_logEntry.logDateTime));
      stringBuilder1.Append(Texts.Instance.GetText("consoleCharacterCasted"));
      stringBuilder1.Append(" <sprite name=cards><link=log:{1}><u>{2}</u></link>");
      stringBuilder1.Append("<br>");
    }
    string format = stringBuilder1.ToString();
    stringBuilder1.Clear();
    string str = "";
    if (_logEntry.logHero != null)
      str = _logEntry.logHero.SourceName;
    else if (_logEntry.logNPC != null)
      str = _logEntry.logNPC.SourceName;
    StringBuilder stringBuilder2 = new StringBuilder();
    if (_logEntry.logActivation == Enums.EventActivation.TraitActivation)
    {
      TraitData traitData = Globals.Instance.GetTraitData(_logEntry.logCardId);
      if ((Object) traitData != (Object) null)
        stringBuilder2.Append(traitData.TraitName);
    }
    else if (_logEntry.logCardId != "")
    {
      CardData cardData = MatchManager.Instance.GetCardData(_logEntry.logCardId);
      if ((Object) cardData != (Object) null)
      {
        stringBuilder2.Append("<color=#");
        if (cardData.CardUpgraded == Enums.CardUpgraded.Rare)
          stringBuilder2.Append(ColorUtility.ToHtmlStringRGBA(Globals.Instance.ColorColor["purple"]));
        else if (cardData.CardUpgraded == Enums.CardUpgraded.A)
          stringBuilder2.Append(ColorUtility.ToHtmlStringRGBA(Globals.Instance.ColorColor["blueCardTitle"]));
        else if (cardData.CardUpgraded == Enums.CardUpgraded.B)
          stringBuilder2.Append(ColorUtility.ToHtmlStringRGBA(Globals.Instance.ColorColor["goldCardTitle"]));
        else
          stringBuilder2.Append(ColorUtility.ToHtmlStringRGBA(Globals.Instance.ColorColor["white"]));
        stringBuilder2.Append(">");
        stringBuilder2.Append(cardData.CardName);
        stringBuilder2.Append("</color>");
      }
    }
    return string.Format(format, (object) str, (object) _key, (object) stringBuilder2);
  }

  public string GetText(string key) => this.consoleDict == null || !this.consoleDict.TryGetValue(key, out string _) ? "" : this.consoleDict[key];

  public void SetKey(string _key) => this.key = _key;

  public void LogNew(LogItem logItem)
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    if (logItem.LogType == Enums.LogType.Text)
    {
      stringBuilder1.Append(logItem.text);
    }
    else
    {
      string str1 = "#8D3901";
      if (this.GetText(this.key).LastIndexOf(logItem.CasterName + "]</color> <color=#FFCC00>" + logItem.SkillName) == -1)
      {
        if (logItem.ClassName != "")
          str1 = Globals.Instance.ClassColor[logItem.ClassName];
        stringBuilder1.Append("<line-height=125%><size=24>");
        stringBuilder1.Append("<color=");
        stringBuilder1.Append(str1);
        stringBuilder1.Append(">[");
        stringBuilder1.Append(logItem.CasterName);
        stringBuilder1.Append("]</color> <color=#FFCC00>");
        stringBuilder1.Append(logItem.SkillName);
        stringBuilder1.Append("</color></size><br>");
      }
      if (logItem.LogType == Enums.LogType.Damage)
      {
        stringBuilder1.Append("<line-height=65%><size=24><b>");
        if (logItem.Invulnerable || logItem.Evaded)
          stringBuilder1.Append("0");
        else
          stringBuilder1.Append(logItem.DamageFinal.ToString());
        stringBuilder1.Append(" ");
        if (logItem.DamageType != Enums.DamageType.None)
          stringBuilder1.Append((object) logItem.DamageType);
        else
          stringBuilder1.Append("damage");
        stringBuilder1.Append("</b><color=#AAA> to ");
        stringBuilder1.Append(logItem.TargetName);
        stringBuilder1.Append("</color></size><br><color=#999>____________</color></line-height>\n");
        string str2 = "<color=#FFCC00>" + logItem.DamagePre.ToString() + " ";
        string str3 = (logItem.DamageType == Enums.DamageType.None ? str2 + "damage" : str2 + logItem.DamageType.ToString()) + "</color> ";
        if (logItem.Invulnerable)
        {
          stringBuilder1.Append(str3);
          stringBuilder1.Append("<color=#00E6FF>*Invulnerable*</color><br>");
        }
        else if (logItem.Evaded)
        {
          stringBuilder1.Append(str3);
          stringBuilder1.Append("<color=#00E6FF>*Evaded*</color><br>");
        }
        else if (logItem.DamageBlocked > 0 && logItem.DamageBlocked == logItem.DamagePre)
        {
          stringBuilder1.Append(str3);
          stringBuilder1.Append("<color=#00E6FF>*Blocked*</color><br>");
        }
        else if (logItem.Immune)
        {
          stringBuilder1.Append(str3);
          stringBuilder1.Append("<color=#00E6FF>*Immune*</color>");
        }
        else
        {
          stringBuilder1.Append("<pos=16>");
          stringBuilder1.Append(str3);
          if (logItem.DamageAuraCurse > 0)
          {
            stringBuilder1.Append(" <color=#FBF>*defended ");
            if (logItem.DamageAuraCurse > 0)
              stringBuilder1.Append("-");
            else
              stringBuilder1.Append("-");
            stringBuilder1.Append(Mathf.Abs(logItem.DamageAuraCurse));
            stringBuilder1.Append("*</color>");
            stringBuilder1.Append(" => ");
            stringBuilder1.Append(logItem.DamagePostAuraCurse);
            stringBuilder1.Append("Hp");
            stringBuilder1.Append("\n");
            stringBuilder1.Append("<pos=16>");
            stringBuilder1.Append(" <color=#F3404E>");
            stringBuilder1.Append(logItem.DamagePostAuraCurse);
            stringBuilder1.Append("Hp");
            stringBuilder1.Append("</color>");
          }
          if (logItem.DamageBlocked > 0)
          {
            stringBuilder1.Append(" <color=#00E6FF>*blocked ");
            stringBuilder1.Append(logItem.DamageBlocked.ToString());
            stringBuilder1.Append("*</color>");
            stringBuilder1.Append(" => ");
            StringBuilder stringBuilder2 = stringBuilder1;
            int num = logItem.DamagePre - logItem.DamageBlocked;
            string str4 = num.ToString();
            stringBuilder2.Append(str4);
            stringBuilder1.Append(" Hp");
            stringBuilder1.Append("\n");
            stringBuilder1.Append("<pos=16>");
            stringBuilder1.Append(" <color=#F3404E>");
            StringBuilder stringBuilder3 = stringBuilder1;
            num = logItem.DamagePre - logItem.DamageBlocked;
            string str5 = num.ToString();
            stringBuilder3.Append(str5);
            stringBuilder1.Append(" Hp");
            stringBuilder1.Append("</color>");
          }
          stringBuilder1.Append(" <color=#FF9B34>");
          stringBuilder1.Append(logItem.DamageResist.ToString());
          stringBuilder1.Append("% resist</color>");
          stringBuilder1.Append(" => ");
          stringBuilder1.Append("<color=white>");
          stringBuilder1.Append(logItem.DamageFinal);
          stringBuilder1.Append(" Hp</color>");
        }
      }
    }
    if (!this.consoleDict.TryGetValue(this.key, out string _))
      this.consoleDict.Add(this.key, stringBuilder1.ToString());
    else
      this.consoleDict[this.key] = this.consoleDict[this.key] + "\n\n" + stringBuilder1.ToString();
  }

  private string TimeStamp(string _date)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<size=-4><voffset=1><color=#666>[");
    stringBuilder.Append(_date);
    stringBuilder.Append("]</color></voffset></size> ");
    return stringBuilder.ToString();
  }
}
