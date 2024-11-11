// Decompiled with JetBrains decompiler
// Type: ProfanityFilter.ProfanityFilter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using ProfanityFilter.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ProfanityFilter
{
  public class ProfanityFilter : ProfanityBase, IProfanityFilter
  {
    public ProfanityFilter() => this.AllowList = (IAllowList) new ProfanityFilter.AllowList();

    public ProfanityFilter(string[] profanityList)
      : base(profanityList)
    {
      this.AllowList = (IAllowList) new ProfanityFilter.AllowList();
    }

    public ProfanityFilter(List<string> profanityList)
      : base(profanityList)
    {
      this.AllowList = (IAllowList) new ProfanityFilter.AllowList();
    }

    public IAllowList AllowList { get; }

    public bool IsProfanity(string word) => !string.IsNullOrEmpty(word) && !this.AllowList.Contains(word.ToLower(CultureInfo.InvariantCulture)) && this._profanities.Contains(word.ToLower(CultureInfo.InvariantCulture));

    public ReadOnlyCollection<string> DetectAllProfanities(string sentence) => this.DetectAllProfanities(sentence, false);

    public ReadOnlyCollection<string> DetectAllProfanities(
      string sentence,
      bool removePartialMatches)
    {
      if (string.IsNullOrEmpty(sentence))
        return new ReadOnlyCollection<string>((IList<string>) new List<string>());
      sentence = sentence.ToLower();
      sentence = sentence.Replace(".", "");
      sentence = sentence.Replace(",", "");
      List<string> postAllowList = this.FilterWordListByAllowList(sentence.Split(' ', StringSplitOptions.None));
      List<string> swearList = new List<string>();
      this.AddMultiWordProfanities(swearList, ProfanityFilter.ProfanityFilter.ConvertWordListToSentence(postAllowList));
      if (removePartialMatches)
        swearList.RemoveAll((Predicate<string>) (x => swearList.Any<string>((Func<string, bool>) (y => x != y && y.Contains(x)))));
      return new ReadOnlyCollection<string>((IList<string>) this.FilterSwearListForCompleteWordsOnly(sentence, swearList).Distinct<string>().ToList<string>());
    }

    public string CensorString(string sentence) => this.CensorString(sentence, '*');

    public string CensorString(string sentence, char censorCharacter) => this.CensorString(sentence, censorCharacter, false);

    public string CensorString(string sentence, char censorCharacter, bool ignoreNumbers)
    {
      if (string.IsNullOrEmpty(sentence))
        return string.Empty;
      List<string> postAllowList = this.FilterWordListByAllowList(Regex.Replace(sentence.Trim().ToLower(), "[^\\w\\s]", "").Split(' ', StringSplitOptions.None));
      List<string> swearList = new List<string>();
      this.AddMultiWordProfanities(swearList, ProfanityFilter.ProfanityFilter.ConvertWordListToSentence(postAllowList));
      StringBuilder censored = new StringBuilder(sentence);
      StringBuilder tracker = new StringBuilder(sentence);
      return this.CensorStringByProfanityList(censorCharacter, swearList, censored, tracker, ignoreNumbers).ToString();
    }

    public (int, int, string)? GetCompleteWord(string toCheck, string profanity)
    {
      if (string.IsNullOrEmpty(toCheck))
        return new (int, int, string)?();
      string lower1 = profanity.ToLower(CultureInfo.InvariantCulture);
      string lower2 = toCheck.ToLower(CultureInfo.InvariantCulture);
      if (!lower2.Contains(lower1))
        return new (int, int, string)?();
      int startIndex = lower2.IndexOf(lower1, StringComparison.Ordinal);
      int index = startIndex;
      while (startIndex > 0 && toCheck[startIndex - 1] != ' ' && !char.IsPunctuation(toCheck[startIndex - 1]))
        --startIndex;
      while (index < toCheck.Length && toCheck[index] != ' ' && !char.IsPunctuation(toCheck[index]))
        ++index;
      return new (int, int, string)?((startIndex, index, lower2.Substring(startIndex, index - startIndex).ToLower(CultureInfo.InvariantCulture)));
    }

    public bool ContainsProfanity(string term)
    {
      if (string.IsNullOrWhiteSpace(term))
        return false;
      List<string> list = this._profanities.Where<string>((Func<string, bool>) (word => word.Length <= term.Length)).ToList<string>();
      if (list.Count == 0)
        return false;
      foreach (Capture match in new Regex(string.Format("(?:{0})", (object) string.Join("|", (IEnumerable<string>) list).Replace("$", "\\$"), (object) RegexOptions.IgnoreCase)).Matches(term))
      {
        if (!this.AllowList.Contains(match.Value.ToLower(CultureInfo.InvariantCulture)))
          return true;
      }
      return false;
    }

    private StringBuilder CensorStringByProfanityList(
      char censorCharacter,
      List<string> swearList,
      StringBuilder censored,
      StringBuilder tracker,
      bool ignoreNumeric)
    {
      foreach (string str1 in (IEnumerable<string>) swearList.OrderByDescending<string, int>((Func<string, int>) (x => x.Length)))
      {
        (int, int, string)? nullable = new (int, int, string)?((0, 0, ""));
        if (str1.Split(' ', StringSplitOptions.None).Length == 1)
        {
          do
          {
            nullable = this.GetCompleteWord(tracker.ToString(), str1);
            if (nullable.HasValue)
            {
              string str2 = nullable.Value.Item3;
              if (ignoreNumeric)
                str2 = Regex.Replace(nullable.Value.Item3, "[\\d-]", string.Empty);
              if (str2 == str1)
              {
                for (int index = nullable.Value.Item1; index < nullable.Value.Item2; ++index)
                {
                  censored[index] = censorCharacter;
                  tracker[index] = censorCharacter;
                }
              }
              else
              {
                for (int index = nullable.Value.Item1; index < nullable.Value.Item2; ++index)
                  tracker[index] = censorCharacter;
              }
            }
          }
          while (nullable.HasValue);
        }
        else
          censored = censored.Replace(str1, ProfanityFilter.ProfanityFilter.CreateCensoredString(str1, censorCharacter));
      }
      return censored;
    }

    private List<string> FilterSwearListForCompleteWordsOnly(
      string sentence,
      List<string> swearList)
    {
      List<string> stringList = new List<string>();
      StringBuilder stringBuilder = new StringBuilder(sentence);
      foreach (string str in (IEnumerable<string>) swearList.OrderByDescending<string, int>((Func<string, int>) (x => x.Length)))
      {
        (int, int, string)? nullable = new (int, int, string)?((0, 0, ""));
        if (str.Split(' ', StringSplitOptions.None).Length == 1)
        {
          do
          {
            nullable = this.GetCompleteWord(stringBuilder.ToString(), str);
            if (nullable.HasValue)
            {
              if (nullable.Value.Item3 == str)
              {
                stringList.Add(str);
                for (int index = nullable.Value.Item1; index < nullable.Value.Item2; ++index)
                  stringBuilder[index] = '*';
                break;
              }
              for (int index = nullable.Value.Item1; index < nullable.Value.Item2; ++index)
                stringBuilder[index] = '*';
            }
          }
          while (nullable.HasValue);
        }
        else
        {
          stringList.Add(str);
          stringBuilder.Replace(str, " ");
        }
      }
      return stringList;
    }

    private List<string> FilterWordListByAllowList(string[] words)
    {
      List<string> stringList = new List<string>();
      foreach (string word in words)
      {
        if (!string.IsNullOrEmpty(word) && !this.AllowList.Contains(word.ToLower(CultureInfo.InvariantCulture)))
          stringList.Add(word);
      }
      return stringList;
    }

    private static string ConvertWordListToSentence(List<string> postAllowList)
    {
      string sentence = string.Empty;
      foreach (string postAllow in postAllowList)
        sentence = sentence + postAllow + " ";
      return sentence;
    }

    private void AddMultiWordProfanities(List<string> swearList, string postAllowListSentence) => swearList.AddRange(this._profanities.Cast<string>().Where<string>((Func<string, bool>) (profanity => postAllowListSentence.ToLower(CultureInfo.InvariantCulture).Contains(profanity))));

    private static string CreateCensoredString(string word, char censorCharacter)
    {
      string censoredString = string.Empty;
      for (int index = 0; index < word.Length; ++index)
        censoredString = word[index] == ' ' ? censoredString + " " : censoredString + censorCharacter.ToString();
      return censoredString;
    }
  }
}
