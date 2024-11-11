// Decompiled with JetBrains decompiler
// Type: Texts
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class Texts : MonoBehaviour
{
  [SerializeField]
  private Dictionary<string, Dictionary<string, string>> TextStrings;
  private Dictionary<string, Dictionary<string, string>> TextKeynotes;
  private List<string> tipsList;
  private string lang = "";
  private bool countWords;
  private bool translationLoaded;
  private bool translationFinished;

  public static Texts Instance { get; private set; }

  public List<string> TipsList
  {
    get => this.tipsList;
    set => this.tipsList = value;
  }

  private void Awake()
  {
    if ((UnityEngine.Object) Texts.Instance == (UnityEngine.Object) null)
      Texts.Instance = this;
    else if ((UnityEngine.Object) Texts.Instance != (UnityEngine.Object) this)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    this.TextStrings = new Dictionary<string, Dictionary<string, string>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this.TextKeynotes = new Dictionary<string, Dictionary<string, string>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  }

  public bool GotTranslations() => this.translationFinished;

  public void LoadTranslation()
  {
    if (!GameManager.Instance.PrefsLoaded)
      return;
    if (this.translationLoaded)
    {
      Debug.LogError((object) "LoadTranslation translationLoaded");
    }
    else
    {
      this.translationLoaded = true;
      Debug.Log((object) nameof (LoadTranslation));
      this.lang = "en";
      this.TextStrings[this.lang] = new Dictionary<string, string>();
      this.TextKeynotes[this.lang] = new Dictionary<string, string>();
      this.LoadTranslationText("cards");
      this.LoadTranslationText("traits");
      this.LoadTranslationText("");
      this.LoadTranslationText("keynotes");
      this.LoadTranslationText("auracurse");
      this.LoadTranslationText("events");
      this.LoadTranslationText("nodes");
      this.LoadTranslationText("fluff");
      this.LoadTranslationText("class");
      this.LoadTranslationText("monsters");
      this.LoadTranslationText("requirements");
      this.LoadTranslationText("tips");
      if (Globals.Instance.CurrentLang != "en")
      {
        this.lang = Globals.Instance.CurrentLang;
        this.TextStrings[this.lang] = this.TextStrings["en"];
        this.TextKeynotes[this.lang] = this.TextKeynotes["en"];
        this.LoadTranslationText("cards");
        this.LoadTranslationText("traits");
        this.LoadTranslationText("");
        this.LoadTranslationText("keynotes");
        this.LoadTranslationText("auracurse");
        this.LoadTranslationText("events");
        this.LoadTranslationText("nodes");
        this.LoadTranslationText("fluff");
        this.LoadTranslationText("class");
        this.LoadTranslationText("monsters");
        this.LoadTranslationText("requirements");
        this.LoadTranslationText("tips");
      }
      this.translationFinished = true;
    }
  }

  private void LoadTranslationText(string type)
  {
    TextAsset textAsset = new TextAsset();
    string str1 = "";
    type = type.ToLower();
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.Append("Lang/");
    stringBuilder1.Append(this.lang);
    stringBuilder1.Append("/");
    stringBuilder1.Append(this.lang);
    switch (type)
    {
      case "":
        str1 = this.lang + ".txt";
        textAsset = Resources.Load(stringBuilder1.ToString()) as TextAsset;
        break;
      case "keynotes":
        str1 = this.lang + "_keynotes.txt";
        stringBuilder1.Append("_keynotes");
        textAsset = Resources.Load(stringBuilder1.ToString()) as TextAsset;
        break;
      case "traits":
        str1 = this.lang + "_traits.txt";
        stringBuilder1.Append("_traits");
        textAsset = Resources.Load(stringBuilder1.ToString()) as TextAsset;
        break;
      case "auracurse":
        str1 = this.lang + "_auracurse.txt";
        stringBuilder1.Append("_auracurse");
        textAsset = Resources.Load(stringBuilder1.ToString()) as TextAsset;
        break;
      case "events":
        str1 = this.lang + "_events.txt";
        stringBuilder1.Append("_events");
        textAsset = Resources.Load(stringBuilder1.ToString()) as TextAsset;
        break;
      case "nodes":
        str1 = this.lang + "_nodes.txt";
        stringBuilder1.Append("_nodes");
        textAsset = Resources.Load(stringBuilder1.ToString()) as TextAsset;
        break;
      case "cards":
        str1 = this.lang + "_cards.txt";
        stringBuilder1.Append("_cards");
        textAsset = Resources.Load(stringBuilder1.ToString()) as TextAsset;
        break;
      case "fluff":
        str1 = this.lang + "_cardsfluff.txt";
        stringBuilder1.Append("_cardsfluff");
        textAsset = Resources.Load(stringBuilder1.ToString()) as TextAsset;
        break;
      case "class":
        str1 = this.lang + "_class.txt";
        stringBuilder1.Append("_class");
        textAsset = Resources.Load(stringBuilder1.ToString()) as TextAsset;
        break;
      case "monsters":
        str1 = this.lang + "_monsters.txt";
        stringBuilder1.Append("_monsters");
        textAsset = Resources.Load(stringBuilder1.ToString()) as TextAsset;
        break;
      case "requirements":
        str1 = this.lang + "_requirements.txt";
        stringBuilder1.Append("_requirements");
        textAsset = Resources.Load(stringBuilder1.ToString()) as TextAsset;
        break;
      case "tips":
        str1 = this.lang + "_tips.txt";
        stringBuilder1.Append("_tips");
        textAsset = Resources.Load(stringBuilder1.ToString()) as TextAsset;
        this.tipsList = new List<string>();
        break;
    }
    if ((UnityEngine.Object) textAsset == (UnityEngine.Object) null)
      return;
    List<string> stringList = new List<string>((IEnumerable<string>) textAsset.text.Split('\n', StringSplitOptions.None));
    int num = 0;
    StringBuilder stringBuilder2 = new StringBuilder();
    StringBuilder stringBuilder3 = new StringBuilder();
    for (int index = 0; index < stringList.Count; ++index)
    {
      string str2 = stringList[index];
      if (!(str2 == "") && str2[0] != '#')
      {
        string[] strArray = str2.Trim().Split(new char[1]
        {
          '='
        }, 2);
        if (strArray != null && strArray.Length >= 2)
        {
          strArray[0] = strArray[0].Trim().ToLower();
          strArray[1] = Functions.SplitString("//", strArray[1])[0].Trim();
          switch (type)
          {
            case "keynotes":
              stringBuilder2.Append("keynotes_");
              break;
            case "traits":
              stringBuilder2.Append("traits_");
              break;
            case "auracurse":
              stringBuilder2.Append("auracurse_");
              break;
            case "events":
              stringBuilder2.Append("events_");
              break;
            case "nodes":
              stringBuilder2.Append("nodes_");
              break;
            case "cards":
            case "fluff":
              stringBuilder2.Append("cards_");
              break;
            case "class":
              stringBuilder2.Append("class_");
              break;
            case "monsters":
              stringBuilder2.Append("monsters_");
              break;
            case "requirements":
              stringBuilder2.Append("requirements_");
              break;
            case "tips":
              stringBuilder2.Append("tips_");
              break;
          }
          stringBuilder2.Append(strArray[0]);
          if (this.TextStrings[this.lang].ContainsKey(stringBuilder2.ToString()))
            this.TextStrings[this.lang][stringBuilder2.ToString()] = strArray[1];
          else
            this.TextStrings[this.lang].Add(stringBuilder2.ToString(), strArray[1]);
          if (type == "tips")
            this.tipsList.Add(strArray[1]);
          bool flag = true;
          if (type == "")
          {
            if (strArray[1].StartsWith("rptd_", StringComparison.OrdinalIgnoreCase))
            {
              stringBuilder3.Append(strArray[1].Substring(5).ToLower());
              this.TextStrings[this.lang][stringBuilder2.ToString()] = this.TextStrings[this.lang][stringBuilder3.ToString()];
              flag = false;
              stringBuilder3.Clear();
            }
          }
          else if (type == "events")
          {
            if (strArray[1].StartsWith("rptd_", StringComparison.OrdinalIgnoreCase))
            {
              stringBuilder3.Append("events_");
              stringBuilder3.Append(strArray[1].Substring(5).ToLower());
              if (this.TextStrings[this.lang].ContainsKey(stringBuilder3.ToString()))
                this.TextStrings[this.lang][stringBuilder2.ToString()] = this.TextStrings[this.lang][stringBuilder3.ToString()];
              flag = false;
              stringBuilder3.Clear();
            }
          }
          else if (type == "cards")
          {
            if (strArray[1].StartsWith("rptd_", StringComparison.OrdinalIgnoreCase))
            {
              stringBuilder3.Append("cards_");
              stringBuilder3.Append(strArray[1].Substring(5).ToLower());
              this.TextStrings[this.lang][stringBuilder2.ToString()] = this.TextStrings[this.lang][stringBuilder3.ToString()];
              flag = false;
              stringBuilder3.Clear();
            }
          }
          else if (type == "monsters" && strArray[1].StartsWith("rptd_", StringComparison.OrdinalIgnoreCase))
          {
            stringBuilder3.Append("monsters_");
            stringBuilder3.Append(strArray[1].Substring(5).ToLower());
            this.TextStrings[this.lang][stringBuilder2.ToString()] = this.TextStrings[this.lang][stringBuilder3.ToString()];
            flag = false;
            stringBuilder3.Clear();
          }
          if (flag && this.countWords)
          {
            string str3 = Regex.Replace(Regex.Replace(strArray[1], "<(.*?)>", ""), "\\s+", " ");
            num += str3.Split(" ", StringSplitOptions.None).Length;
          }
          stringBuilder2.Clear();
        }
      }
    }
    if (this.countWords)
      Debug.Log((object) ("Count words file -> " + str1 + " = " + num.ToString()));
  }

  public string GetText(string _id, string _type = "")
  {
    if ((UnityEngine.Object) Globals.Instance == (UnityEngine.Object) null || !GameManager.Instance.PrefsLoaded)
      return "";
    _id = _id.Replace(" ", "").ToLower();
    if (!(_id != ""))
      return "";
    if (_type != "")
      _id = _type.ToLower() + "_" + _id.ToLower();
    return !this.TextStrings[Globals.Instance.CurrentLang].ContainsKey(_id) ? "" : this.TextStrings[Globals.Instance.CurrentLang][_id];
  }
}
