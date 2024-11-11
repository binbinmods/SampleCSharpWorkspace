// Decompiled with JetBrains decompiler
// Type: TomeRun
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class TomeRun : MonoBehaviour
{
  public Transform hoverT;
  public Transform[] characters;
  private SpriteRenderer[] charactersSpr;
  private TMP_Text[] charactersName;
  public Transform madness;
  public TMP_Text madnessText;
  public TMP_Text description;
  public TMP_Text date;
  public TMP_Text type;
  public TMP_Text score;
  private PlayerRun playerRun;
  private int runIndex = -1;

  private void Awake()
  {
    if (this.characters.Length == 0)
      return;
    this.charactersSpr = new SpriteRenderer[this.characters.Length];
    this.charactersName = new TMP_Text[this.characters.Length];
    for (int index = 0; index < this.characters.Length; ++index)
    {
      this.charactersSpr[index] = this.characters[index].GetComponent<SpriteRenderer>();
      this.charactersName[index] = this.characters[index].GetChild(0).GetComponent<TMP_Text>();
    }
  }

  public void SetRun(int _index = -1)
  {
    this.runIndex = _index;
    if (_index <= -1)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    this.playerRun = TomeManager.Instance.playerRunsList[_index];
    this.madness.gameObject.SetActive(false);
    if (!this.playerRun.ObeliskChallenge)
      stringBuilder.Append(Texts.Instance.GetText("adventure"));
    else if (this.playerRun.WeeklyChallenge)
      stringBuilder.Append(Texts.Instance.GetText("menuWeekly"));
    else
      stringBuilder.Append(Texts.Instance.GetText("menuChallenge"));
    if (this.playerRun.TotalPlayers > 1)
    {
      stringBuilder.Append(" <color=#24646D><size=-.5>(");
      stringBuilder.Append(Texts.Instance.GetText("menuMultiplayer"));
      stringBuilder.Append(")</color></size>");
    }
    this.type.text = stringBuilder.ToString();
    stringBuilder.Clear();
    if (!this.playerRun.ObeliskChallenge)
    {
      int madnessTotal = MadnessManager.Instance.CalculateMadnessTotal(this.playerRun.MadnessLevel, this.playerRun.MadnessCorruptors);
      if (madnessTotal > 0)
      {
        this.madness.gameObject.SetActive(true);
        this.madnessText.text = madnessTotal.ToString();
      }
    }
    else if (!this.playerRun.WeeklyChallenge && this.playerRun.ObeliskMadness > 0)
    {
      int obeliskMadness = this.playerRun.ObeliskMadness;
      this.madness.gameObject.SetActive(true);
      this.madnessText.text = obeliskMadness.ToString();
    }
    this.date.text = Convert.ToDateTime(this.playerRun.gameDate).ToString("d");
    stringBuilder.Append(Functions.ScoreFormat(this.playerRun.FinalScore));
    stringBuilder.Append("  <voffset=.2><size=-.6><sprite name=experience></size></voffset>");
    this.score.text = stringBuilder.ToString();
    stringBuilder.Clear();
    stringBuilder.Append(Texts.Instance.GetText("placesVisited"));
    stringBuilder.Append(": <color=#222>");
    stringBuilder.Append(this.playerRun.VisitedNodes.Count);
    stringBuilder.Append("</color>\n");
    stringBuilder.Append(Texts.Instance.GetText("monstersKilledTome"));
    stringBuilder.Append(" <color=#222>");
    stringBuilder.Append(this.playerRun.MonstersKilled);
    stringBuilder.Append("</color>\n");
    stringBuilder.Append(Texts.Instance.GetText("bossesKilledTome"));
    stringBuilder.Append(" <color=#222>");
    stringBuilder.Append(this.playerRun.BossesKilled);
    stringBuilder.Append("</color>\n");
    this.description.text = stringBuilder.ToString();
    for (int index = 0; index < 4; ++index)
      this.charactersName[index].text = "";
    this.DoPortraits();
  }

  private void DoPortraits()
  {
    SkinData skinData = (SkinData) null;
    string[] source = new string[4]
    {
      "warrior",
      "scout",
      "mage",
      "healer"
    };
    if (((IEnumerable<string>) source).Contains<string>(this.playerRun.Char0))
      this.playerRun.Char0 = Functions.GetClassFromCards(this.playerRun.Char0Cards);
    if (this.playerRun.Char0 != "")
    {
      if (this.playerRun.Char0Skin != "")
        skinData = Globals.Instance.GetSkinData(this.playerRun.Char0Skin);
      if (this.playerRun.Char0Skin == "" || (UnityEngine.Object) skinData == (UnityEngine.Object) null)
        skinData = Globals.Instance.GetSkinData(Globals.Instance.GetSkinBaseIdBySubclass(this.playerRun.Char0));
      if ((UnityEngine.Object) skinData != (UnityEngine.Object) null)
        this.charactersSpr[0].sprite = skinData.SpritePortrait;
    }
    this.charactersName[0].text = this.playerRun.Char0Owner;
    if (((IEnumerable<string>) source).Contains<string>(this.playerRun.Char1))
      this.playerRun.Char1 = Functions.GetClassFromCards(this.playerRun.Char1Cards);
    if (this.playerRun.Char1 != "")
    {
      if (this.playerRun.Char1Skin != "")
        skinData = Globals.Instance.GetSkinData(this.playerRun.Char1Skin);
      if (this.playerRun.Char1Skin == "" || (UnityEngine.Object) skinData == (UnityEngine.Object) null)
        skinData = Globals.Instance.GetSkinData(Globals.Instance.GetSkinBaseIdBySubclass(this.playerRun.Char1));
      if ((UnityEngine.Object) skinData != (UnityEngine.Object) null)
        this.charactersSpr[1].sprite = skinData.SpritePortrait;
    }
    this.charactersName[1].text = this.playerRun.Char1Owner;
    if (((IEnumerable<string>) source).Contains<string>(this.playerRun.Char2))
      this.playerRun.Char2 = Functions.GetClassFromCards(this.playerRun.Char2Cards);
    if (this.playerRun.Char2 != "")
    {
      if (this.playerRun.Char2Skin != "")
        skinData = Globals.Instance.GetSkinData(this.playerRun.Char2Skin);
      if (this.playerRun.Char2Skin == "" || (UnityEngine.Object) skinData == (UnityEngine.Object) null)
        skinData = Globals.Instance.GetSkinData(Globals.Instance.GetSkinBaseIdBySubclass(this.playerRun.Char2));
      if ((UnityEngine.Object) skinData != (UnityEngine.Object) null)
        this.charactersSpr[2].sprite = skinData.SpritePortrait;
    }
    this.charactersName[2].text = this.playerRun.Char2Owner;
    if (((IEnumerable<string>) source).Contains<string>(this.playerRun.Char3))
      this.playerRun.Char3 = Functions.GetClassFromCards(this.playerRun.Char3Cards);
    if (this.playerRun.Char3 != "")
    {
      if (this.playerRun.Char3Skin != "")
        skinData = Globals.Instance.GetSkinData(this.playerRun.Char3Skin);
      if (this.playerRun.Char3Skin == "" || (UnityEngine.Object) skinData == (UnityEngine.Object) null)
        skinData = Globals.Instance.GetSkinData(Globals.Instance.GetSkinBaseIdBySubclass(this.playerRun.Char3));
      if ((UnityEngine.Object) skinData != (UnityEngine.Object) null)
        this.charactersSpr[3].sprite = skinData.SpritePortrait;
    }
    this.charactersName[3].text = this.playerRun.Char3Owner;
  }

  private void OnMouseEnter()
  {
    this.hoverT.gameObject.SetActive(true);
    GameManager.Instance.SetCursorHover();
  }

  private void OnMouseExit()
  {
    this.hoverT.gameObject.SetActive(false);
    GameManager.Instance.SetCursorPlain();
  }

  public void OnMouseUp()
  {
    this.hoverT.gameObject.SetActive(false);
    GameManager.Instance.SetCursorPlain();
    if (this.runIndex <= -1)
      return;
    TomeManager.Instance.DoRun(this.runIndex);
  }
}
