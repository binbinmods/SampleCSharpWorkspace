// Decompiled with JetBrains decompiler
// Type: Functions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public static class Functions
{
  private static string[] serversMaster = new string[24]
  {
    "time-a-g.nist.gov",
    "time-b-g.nist.gov",
    "time-c-g.nist.gov",
    "time-d-g.nist.gov",
    "time-d-g.nist.gov",
    "time-e-g.nist.gov",
    "time-e-g.nist.gov",
    "time-a-wwv.nist.gov",
    "time-b-wwv.nist.gov",
    "time-c-wwv.nist.gov",
    "time-d-wwv.nist.gov",
    "time-d-wwv.nist.gov",
    "time-e-wwv.nist.gov",
    "time-e-wwv.nist.gov",
    "time-a-b.nist.gov",
    "time-b-b.nist.gov",
    "time-c-b.nist.gov",
    "time-d-b.nist.gov",
    "time-d-b.nist.gov",
    "time-e-b.nist.gov",
    "time-e-b.nist.gov",
    "time.nist.gov",
    "utcnist.colorado.edu",
    "utcnist2.colorado.edu"
  };
  private static int serverIndex = 0;
  private static bool serversRandomized = false;

  public static string CompressString(string text)
  {
    byte[] bytes = Encoding.UTF8.GetBytes(text);
    MemoryStream memoryStream = new MemoryStream();
    using (GZipStream gzipStream = new GZipStream((Stream) memoryStream, CompressionMode.Compress, true))
      gzipStream.Write(bytes, 0, bytes.Length);
    memoryStream.Position = 0L;
    byte[] numArray1 = new byte[memoryStream.Length];
    memoryStream.Read(numArray1, 0, numArray1.Length);
    byte[] numArray2 = new byte[numArray1.Length + 4];
    Buffer.BlockCopy((Array) numArray1, 0, (Array) numArray2, 4, numArray1.Length);
    Buffer.BlockCopy((Array) BitConverter.GetBytes(bytes.Length), 0, (Array) numArray2, 0, 4);
    return Convert.ToBase64String(numArray2);
  }

  public static string DecompressString(string compressedText)
  {
    byte[] buffer = Convert.FromBase64String(compressedText);
    using (MemoryStream memoryStream = new MemoryStream())
    {
      int int32 = BitConverter.ToInt32(buffer, 0);
      memoryStream.Write(buffer, 4, buffer.Length - 4);
      byte[] numArray = new byte[int32];
      memoryStream.Position = 0L;
      using (GZipStream gzipStream = new GZipStream((Stream) memoryStream, CompressionMode.Decompress))
        gzipStream.Read(numArray, 0, numArray.Length);
      return Encoding.UTF8.GetString(numArray);
    }
  }

  public static int GameVersionToNumber(string _str = "")
  {
    float num1 = 0.0f;
    _str = _str.Trim();
    if (_str == "")
      return Functions.FuncRoundToInt(num1);
    string[] strArray = _str.Split('.', StringSplitOptions.None);
    if (strArray.Length != 0)
    {
      float num2 = float.Parse(strArray[0]);
      for (int index = 1; index < strArray[0].Length; ++index)
        num2 *= 0.1f;
      num1 += num2 * 1E+07f;
    }
    if (strArray.Length > 1)
    {
      float num3 = float.Parse(strArray[1]);
      for (int index = 1; index < strArray[1].Length; ++index)
        num3 *= 0.1f;
      num1 += num3 * 10000f;
    }
    if (strArray.Length > 2)
    {
      strArray[2] = strArray[2].ToLower().Split(' ', StringSplitOptions.None)[0];
      strArray[2] = strArray[2].Replace("a", "1");
      strArray[2] = strArray[2].Replace("b", "2");
      strArray[2] = strArray[2].Replace("c", "3");
      strArray[2] = strArray[2].Replace("d", "4");
      strArray[2] = strArray[2].Replace("e", "5");
      strArray[2] = strArray[2].Replace("f", "6");
      strArray[2] = strArray[2].Replace("g", "7");
      strArray[2] = strArray[2].Replace("h", "8");
      strArray[2] = strArray[2].Replace("i", "9");
      float num4 = float.Parse(strArray[2]);
      for (int index = 1; index < strArray[2].Length; ++index)
        num4 *= 0.1f;
      num1 += num4 * 100f;
    }
    return Functions.FuncRoundToInt(num1);
  }

  public static string StripTagsString(string _str) => Regex.Replace(_str, "<[^>]*>", "");

  public static TimeSpan TimeDifference(string dateInput)
  {
    DateTime.Parse(dateInput);
    return DateTime.Parse(dateInput) - GameManager.Instance.GetTime();
  }

  public static bool Expired(string dateInput)
  {
    if (dateInput == "")
      return true;
    DateTime dateTime = DateTime.Parse(dateInput);
    return GameManager.Instance.GetTime() > dateTime;
  }

  public static string NormalizeTextForArchive(string str)
  {
    str = str.Trim().Replace("\t", "").Replace("\r", "").Replace("\n", "<br>");
    return str;
  }

  public static string[] SplitString(string needle, string haystack) => haystack.Split(new string[1]
  {
    needle
  }, StringSplitOptions.None);

  public static string OnlyFirstCharToUpper(this string input)
  {
    switch (input)
    {
      case null:
        throw new ArgumentNullException(nameof (input));
      case "":
        throw new ArgumentException("input cannot be empty", nameof (input));
      default:
        return input[0].ToString().ToUpper() + input.Substring(1);
    }
  }

  public static string UppercaseFirst(string str) => string.IsNullOrEmpty(str) ? string.Empty : char.ToUpper(str[0]).ToString() + str.Substring(1).ToLower();

  public static string LowercaseFirst(string str)
  {
    if (string.IsNullOrEmpty(str))
      return string.Empty;
    str = str.Trim();
    return char.ToLower(str[0]).ToString() + str.Substring(1);
  }

  public static string RemoveLastDoubleDot(string str)
  {
    if (str == "")
      return "";
    return str[str.Length - 1] == ':' ? str.Substring(0, str.Length - 2) : str;
  }

  public static string Substring(string str, int num)
  {
    if (str.Length <= num)
      return str;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(str.Substring(0, num));
    stringBuilder.Append(".");
    return stringBuilder.ToString();
  }

  public static int FuncRoundToInt(float num) => (double) num % 1.0 < 0.5 ? Mathf.FloorToInt(num) : Mathf.CeilToInt(num);

  public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

  public static string Base64Decode(string base64EncodedData) => Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));

  public static string FloatToTime(float theTime)
  {
    float num1 = Mathf.Floor(theTime / 3600f);
    double num2 = (double) Mathf.Floor(theTime - num1 * 3600f);
    float num3 = Mathf.Floor((float) (num2 / 60.0));
    float num4 = Mathf.Floor((float) (num2 - (double) num3 * 60.0));
    StringBuilder stringBuilder = new StringBuilder();
    if ((double) num1 < 10.0)
      stringBuilder.Append("0");
    stringBuilder.Append(num1);
    stringBuilder.Append(":");
    if ((double) num3 < 10.0)
      stringBuilder.Append("0");
    stringBuilder.Append(num3);
    stringBuilder.Append(":");
    if ((double) num4 < 10.0)
      stringBuilder.Append("0");
    stringBuilder.Append(num4);
    return stringBuilder.ToString();
  }

  public static int GetDeterministicHashCode(this string str)
  {
    int num1 = 352654597;
    int num2 = num1;
    for (int index = 0; index < str.Length; index += 2)
    {
      num1 = (num1 << 5) + num1 ^ (int) str[index];
      if (index != str.Length - 1)
        num2 = (num2 << 5) + num2 ^ (int) str[index + 1];
      else
        break;
    }
    return num1 + num2 * 1566083941;
  }

  public static string RandomString(float minCharAmount, float maxCharAmount = 0.0f)
  {
    float num = (double) maxCharAmount != 0.0 ? UnityEngine.Random.Range(minCharAmount, maxCharAmount) : minCharAmount;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; (double) index < (double) num; ++index)
      stringBuilder.Append("abcdefghkmnpqrstuvwxyz123456789"[UnityEngine.Random.Range(0, "abcdefghkmnpqrstuvwxyz123456789".Length)]);
    return stringBuilder.ToString();
  }

  public static string RandomStringSafe(float minCharAmount, float maxCharAmount = 0.0f)
  {
    float num = (double) maxCharAmount != 0.0 ? UnityEngine.Random.Range(minCharAmount, maxCharAmount) : minCharAmount;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; (double) index < (double) num; ++index)
      stringBuilder.Append("abcdefghkmnpqrstwxyz2345689"[UnityEngine.Random.Range(0, "abcdefghkmnpqrstwxyz2345689".Length)]);
    return stringBuilder.ToString();
  }

  public static T[] ShuffleArray<T>(T[] arr)
  {
    T[] objArray = arr;
    for (int index1 = 0; index1 < 2; ++index1)
    {
      for (int maxExclusive = arr.Length - 1; maxExclusive > 0; --maxExclusive)
      {
        int index2 = UnityEngine.Random.Range(0, maxExclusive);
        T obj = objArray[maxExclusive];
        objArray[maxExclusive] = objArray[index2];
        objArray[index2] = obj;
      }
    }
    return objArray;
  }

  public static List<T> ShuffleList<T>(this List<T> ts)
  {
    int count = ts.Count;
    int num = count - 1;
    List<T> objList = new List<T>();
    for (int index = 0; index < count; ++index)
      objList.Add(ts[index]);
    for (int index1 = 0; index1 < 2; ++index1)
    {
      for (int index2 = 0; index2 < num; ++index2)
      {
        int index3 = !((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null) ? UnityEngine.Random.Range(index2, count) : (MatchManager.Instance.EventList == null || !MatchManager.Instance.EventList.Contains("ResetDeck") ? MatchManager.Instance.GetRandomIntRange(index2, count) : MatchManager.Instance.GetRandomIntRange(index2, count, "shuffle"));
        T obj = objList[index2];
        objList[index2] = objList[index3];
        objList[index3] = obj;
      }
    }
    return objList;
  }

  public static string ClassIconFromString(string _class)
  {
    _class = _class.ToLower().Replace(" ", "");
    switch (_class)
    {
      case "warrior":
        return "block";
      case "scout":
        return "piercing";
      case "mage":
        return "fire";
      default:
        return "regeneration";
    }
  }

  public static float DecimalPositions(float number, int positions) => positions <= 0 ? Mathf.Floor(number) : Mathf.Floor(number * Mathf.Pow(10f, (float) positions)) / Mathf.Pow(10f, (float) positions);

  public static string Log(params object[] data)
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < data.Length; ++index)
      stringBuilder.Append(data[index].ToString());
    return stringBuilder.ToString();
  }

  public static long ASCIIWordSum(string str, long[] sumArr)
  {
    int length = str.Length;
    int index1 = 0;
    long num1 = 0;
    long num2 = 0;
    for (int index2 = 0; index2 < length; ++index2)
    {
      if (str[index2] == ' ')
      {
        num2 += num1;
        sumArr[index1++] = num1;
        num1 = 0L;
      }
      else
        num1 += (long) str[index2];
    }
    sumArr[index1] = num1;
    return num2 + num1;
  }

  public static string ThermometerTextForRewards(ThermometerData _thermometerData)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Texts.Instance.GetText("performanceBonus"));
    stringBuilder.Append(" [");
    stringBuilder.Append("<color=#");
    stringBuilder.Append(ColorUtility.ToHtmlStringRGBA(_thermometerData.ThermometerColor));
    stringBuilder.Append(">");
    stringBuilder.Append(Texts.Instance.GetText(_thermometerData.ThermometerId).ToUpper());
    stringBuilder.Append("</color>");
    stringBuilder.Append("]");
    stringBuilder.Append(":\n");
    bool flag1 = false;
    flag1 = true;
    stringBuilder.Append("<sprite name=gold> ");
    if (_thermometerData.UiGold > -1)
      stringBuilder.Append("+");
    stringBuilder.Append(_thermometerData.UiGold);
    stringBuilder.Append("%    ");
    bool flag2 = true;
    stringBuilder.Append("<sprite name=experience> ");
    if (_thermometerData.UiExp > -1)
      stringBuilder.Append("+");
    stringBuilder.Append(_thermometerData.UiExp);
    stringBuilder.Append("%    ");
    if (_thermometerData.UiCard != 0)
    {
      flag2 = true;
      stringBuilder.Append("<sprite name=cards>");
      if (_thermometerData.UiCard > 0)
        stringBuilder.Append("+");
      stringBuilder.Append(_thermometerData.UiCard);
      stringBuilder.Append(" ");
      stringBuilder.Append(Texts.Instance.GetText("cardsTier"));
    }
    return flag2 ? stringBuilder.ToString() : "";
  }

  public static string ThermometerTextForPopup(ThermometerData _thermometerData)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<size=+2><color=#");
    stringBuilder.Append(ColorUtility.ToHtmlStringRGBA(_thermometerData.ThermometerColor));
    stringBuilder.Append("><b>");
    stringBuilder.Append(Texts.Instance.GetText(_thermometerData.ThermometerId).ToUpper());
    stringBuilder.Append("</b></color></size>\n");
    if (AtOManager.Instance.currentMapNode == "voidhigh_13" || AtOManager.Instance.currentMapNode == "of1_10" || AtOManager.Instance.currentMapNode == "of2_10")
      return stringBuilder.ToString();
    stringBuilder.Append("<color=#FFFFFF>");
    stringBuilder.Append(Texts.Instance.GetText("performanceBonus"));
    stringBuilder.Append("</color>");
    stringBuilder.Append("\n<line-height=110%><size=+3>");
    stringBuilder.Append("<sprite name=gold> ");
    if (_thermometerData.UiGold > -1)
      stringBuilder.Append("+");
    stringBuilder.Append(_thermometerData.UiGold);
    stringBuilder.Append("%");
    stringBuilder.Append("     <sprite name=experience> ");
    if (_thermometerData.UiExp > -1)
      stringBuilder.Append("+");
    stringBuilder.Append(_thermometerData.UiExp);
    stringBuilder.Append("%");
    if (_thermometerData.UiCard != 0)
    {
      stringBuilder.Append("\n");
      stringBuilder.Append("<sprite name=cards> ");
      if (_thermometerData.UiCard > 0)
        stringBuilder.Append("+");
      stringBuilder.Append(_thermometerData.UiCard);
      stringBuilder.Append(" ");
      stringBuilder.Append(Texts.Instance.GetText("cardsTier"));
    }
    return stringBuilder.ToString();
  }

  public static int GetMadnessScoreMultiplier(int level, bool adventureMode = true)
  {
    if (adventureMode)
    {
      if (level == 1)
        return 50;
      if (level == 2)
        return 75;
      if (level == 3)
        return 100;
      if (level == 4)
        return 125;
      if (level == 5)
        return 150;
      if (level == 6)
        return 175;
      if (level == 7)
        return 200;
      if (level == 8)
        return 225;
      if (level == 9)
        return 250;
      if (level == 10)
        return 275;
      if (level == 11)
        return 300;
      if (level == 12)
        return 325;
      if (level == 13)
        return 350;
      if (level == 14)
        return 400;
      if (level == 15)
        return 600;
      if (level >= 16)
        return 800;
    }
    else
    {
      switch (level)
      {
        case 1:
          return 25;
        case 2:
          return 50;
        case 3:
          return 75;
        case 4:
          return 100;
        case 5:
          return 125;
        case 6:
          return 150;
        case 7:
          return 175;
        case 8:
          return 250;
        case 9:
          return 400;
        case 10:
          return 550;
      }
    }
    return 0;
  }

  public static string GetMadnessBonusText(int value)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (value == 0)
    {
      stringBuilder.Append(Texts.Instance.GetText("madnessBasicInfo"));
      stringBuilder.Append("<br>");
    }
    else
    {
      stringBuilder.Append(Texts.Instance.GetText("madnessBasic"));
      stringBuilder.Append("<br>");
    }
    switch (value)
    {
      case 2:
        stringBuilder.Append(Texts.Instance.GetText("madnessExhaust"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterHp"), (object) " <color=#E5A44E>+0% / +0% / +10% / +15%</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterSpeed"), (object) " <color=#E5A44E>0 / 0 / +1 / +1</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterDamageTaken"), (object) " <color=#E5A44E>-1 / -1 / -1 / -1</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        break;
      case 3:
        stringBuilder.Append(Texts.Instance.GetText("madnessExhaust"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterHp"), (object) " <color=#E5A44E>+5% / +10% / +15% / +20%</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterSpeed"), (object) " <color=#E5A44E>+1 / +1 / +1 / +1</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterDamageTaken"), (object) " <color=#E5A44E>-1 / -1 / -2 / -2</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterDamageDone"), (object) " <color=#E5A44E>+1 / +1 / +1 / +1</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessSeriousInjuries"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessNoChest"), (object) "800"));
        break;
      case 4:
        stringBuilder.Append(Texts.Instance.GetText("madnessExhaust"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterHp"), (object) " <color=#E5A44E>+15% / +25% / +30% / +35%</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterSpeed"), (object) " <color=#E5A44E>+1 / +1 / +2 / +2</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterDamageTaken"), (object) " <color=#E5A44E>-1 / -2 / -2 / -2</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterDamageDone"), (object) " <color=#E5A44E>+1 / +1 / +2 / +2</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessSeriousInjuries"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessNoChest"), (object) "700"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessHeroEnergy"), (object) " <color=#E5A44E>-1</color> "));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessReroll"));
        break;
      case 5:
        stringBuilder.Append(Texts.Instance.GetText("madnessExhaust"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterHp"), (object) " <color=#E5A44E>+25% / +35% / +40% / +45%</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterSpeed"), (object) " <color=#E5A44E>+2 / +2 / +2 / +2</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterDamageTaken"), (object) " <color=#E5A44E>-2 / -2 / -2 / -2</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterDamageDone"), (object) " <color=#E5A44E>+1 / +2 / +2 / +2</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessDeadlyInjuries"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessNoChest"), (object) "600"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessHeroEnergy"), (object) " <color=#E5A44E>-1</color> "));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessReroll"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessHarderEvents"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessInfrequent"));
        break;
      case 6:
        stringBuilder.Append(Texts.Instance.GetText("madnessExhaust"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterHp"), (object) " <color=#E5A44E>+30% / +45% / +50% / +55%</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterSpeed"), (object) " <color=#E5A44E>+2 / +2 / +2 / +3</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterDamageTaken"), (object) " <color=#E5A44E>-2 / -2 / -2 / -3</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterDamageDone"), (object) " <color=#E5A44E>+2 / +2 / +2 / +2</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessDeadlyInjuries"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessNoChest"), (object) "500"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessNoSupplyExchange"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessHeroEnergy"), (object) " <color=#E5A44E>-1</color> "));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessReroll"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessHarderEvents"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessInfrequent"));
        break;
      case 7:
        stringBuilder.Append(Texts.Instance.GetText("madnessExhaust"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterHp"), (object) " <color=#E5A44E>+35% / +50% / +55% / +60%</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterSpeed"), (object) " <color=#E5A44E>+2 / +2 / +3 / +3</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterDamageTaken"), (object) " <color=#E5A44E>-2 / -2 / -3 / -3</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterDamageDone"), (object) " <color=#E5A44E>+2 / +2 / +2 / +3</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessDeadlyInjuries"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessNoChest"), (object) "400"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessNoSupplyExchange"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessHeroEnergy"), (object) " <color=#E5A44E>-2</color> "));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessReroll"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessHarderEvents"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessInfrequent"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessRandomSeed"));
        break;
      case 8:
        stringBuilder.Append(Texts.Instance.GetText("madnessExhaust"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterHp"), (object) " <color=#E5A44E>+40% / +55% / +60% / +65%</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterSpeed"), (object) " <color=#E5A44E>+2 / +3 / +3 / +3</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterDamageTaken"), (object) " <color=#E5A44E>-2 / -3 / -3 / -3</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessMonsterDamageDone"), (object) " <color=#E5A44E>+2 / +2 / +3 / +3</color> ", (object) Texts.Instance.GetText("madnessPerAct")));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessDeadlyInjuries"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessNoChest"), (object) "300"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessNoSupplyExchange"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(string.Format(Texts.Instance.GetText("madnessHeroEnergy"), (object) " <color=#E5A44E>-2</color> "));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessReroll"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessHarderEvents"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessInfrequent"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessRandomSeed"));
        stringBuilder.Append("<br>");
        stringBuilder.Append(Texts.Instance.GetText("madnessDisabledCards"));
        break;
    }
    return Functions.PregReplaceIcon(stringBuilder.ToString());
  }

  public static NPCData[] GetRandomCombat(
    Enums.CombatTier combatTier,
    int seed,
    string nodeSelectedId,
    bool forceIsThereRare = false)
  {
    NPCData[] _teamNPC = new NPCData[4];
    UnityEngine.Random.InitState(seed);
    bool flag1 = false;
    bool flag2 = false;
    if (GameManager.Instance.IsObeliskChallenge())
    {
      if (AtOManager.Instance.NodeHaveBossRare(nodeSelectedId) | forceIsThereRare)
        flag1 = true;
    }
    else if (MadnessManager.Instance.IsMadnessTraitActive("randomcombats"))
      flag1 = true;
    NPCData npcData = (NPCData) null;
    if (flag1)
    {
      bool flag3 = false;
      for (int index = 0; !flag3 && index < 200; ++index)
      {
        npcData = Globals.Instance.GetNPCForRandom(true, 0, combatTier, _teamNPC);
        if ((UnityEngine.Object) npcData != (UnityEngine.Object) null)
          flag3 = true;
      }
      if ((UnityEngine.Object) npcData != (UnityEngine.Object) null)
      {
        if (npcData.PreferredPosition == Enums.CardTargetPosition.Front)
        {
          _teamNPC[0] = npcData;
          flag2 = true;
        }
        else if (npcData.BigModel)
          _teamNPC[2] = npcData;
        else
          _teamNPC[3] = npcData;
      }
      if (AtOManager.Instance.Sandbox_doubleChampions)
      {
        bool flag4 = false;
        for (int index = 0; !flag4 && index < 200; ++index)
        {
          npcData = Globals.Instance.GetNPCForRandom(true, 0, combatTier, _teamNPC);
          if ((UnityEngine.Object) npcData != (UnityEngine.Object) null)
          {
            if (flag2 && npcData.PreferredPosition != Enums.CardTargetPosition.Front)
              flag4 = true;
            else if (!flag2 && npcData.PreferredPosition == Enums.CardTargetPosition.Front)
              flag4 = true;
          }
        }
        if ((UnityEngine.Object) npcData != (UnityEngine.Object) null)
        {
          if (npcData.PreferredPosition == Enums.CardTargetPosition.Front)
          {
            _teamNPC[0] = npcData;
            flag2 = true;
          }
          else if (npcData.BigModel)
            _teamNPC[2] = npcData;
          else
            _teamNPC[3] = npcData;
        }
      }
    }
    for (int index = 0; index < 4; ++index)
    {
      if ((!((UnityEngine.Object) npcData != (UnityEngine.Object) null) || !npcData.IsBoss || (flag2 || index <= 1) && (!flag2 || index != 1)) && (UnityEngine.Object) _teamNPC[index] == (UnityEngine.Object) null)
        _teamNPC[index] = index == 0 || index == 1 ? (!((UnityEngine.Object) npcData != (UnityEngine.Object) null) || !npcData.IsBoss || flag2 || index != 1 ? Globals.Instance.GetNPCForRandom(false, 1, combatTier, _teamNPC) : Globals.Instance.GetNPCForRandom(false, -1, combatTier, _teamNPC)) : Globals.Instance.GetNPCForRandom(false, -1, combatTier, _teamNPC);
    }
    return _teamNPC;
  }

  public static string ScoreFormat(int _score) => string.Format("{0:#,0}", (object) _score);

  public static string PregReplaceIcon(string str) => Regex.Replace(str, "\\<i=(\\w+)\\>", "<size=+.5><voffset=-.3><sprite name=$1></voffset></size>");

  public static string GetClassFromCards(List<string> _cards)
  {
    if (_cards.Contains("captainshowlstart"))
      return "mercenary";
    if (_cards.Contains("heatlaserstart"))
      return "sentinel";
    if (_cards.Contains("vigorousfurystart"))
      return "berserker";
    if (_cards.Contains("experttrackerstart"))
      return "ranger";
    if (_cards.Contains("killerinstincstart"))
      return "assassin";
    if (_cards.Contains("coldsparkstart"))
      return "elementalist";
    if (_cards.Contains("fieryshieldstart"))
      return "pyromancer";
    if (_cards.Contains("divinegracestart"))
      return "cleric";
    if (_cards.Contains("atonementstart"))
      return "priest";
    return _cards.Contains("maledictionstart") ? "voodoowitch" : "";
  }

  public static void DebugLogGD(string str, string type = "")
  {
    if (!GameManager.Instance.GetDeveloperMode())
      return;
    bool flag;
    switch (type)
    {
      case "cache":
        flag = false;
        break;
      case "error":
        flag = false;
        break;
      case "gamebusy":
        flag = false;
        break;
      case "gamestatus":
        flag = false;
        break;
      case "general":
        flag = false;
        break;
      case "item":
        flag = false;
        break;
      case "map":
        flag = false;
        break;
      case "net":
        flag = true;
        break;
      case "ocmap":
        flag = false;
        break;
      case "randomindex":
        flag = false;
        break;
      case "reload":
        flag = false;
        break;
      case "save":
        flag = false;
        break;
      case "synccode":
        flag = false;
        break;
      case "trace":
        flag = false;
        break;
      default:
        flag = false;
        break;
    }
    if (!flag)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    if (type != "")
    {
      stringBuilder.Append("[");
      stringBuilder.Append(type.ToUpper());
      stringBuilder.Append("] ");
    }
    stringBuilder.Append(str);
    if (type != "cache")
      Debug.Log((object) stringBuilder.ToString());
    else
      Debug.LogWarning((object) stringBuilder.ToString());
  }

  public static string FormatString(string text) => text.Replace("<y>", "<b><color=#fc0>").Replace("</y>", "</color></b>").Replace("<r>", "<b><color=#FF8181>").Replace("</r>", "</color></b>").Replace("<re>", "<color=#8C0800>").Replace("</re>", "</color>").Replace("<g>", "<b><color=#41CD41>").Replace("</g>", "</color></b>").Replace("<gr>", "<color=#444>").Replace("</gr>", "</color>").Replace("<b>", "<b><color=#6af>").Replace("</b>", "</color></b>").Replace("<bl>", "<color=#002B72>").Replace("</bl>", "</color>").Replace("<w>", "<b><color=#fff>").Replace("</w>", "</color></b>").Replace("<o>", "<b><color=#f90>").Replace("</o>", "</color></b>").Replace("<col>", "<b><color=#00e4ff>").Replace("</col>", "</color></b>").Replace("<lig>", "<b><color=#ffff11>").Replace("</lig>", "</color></b>").Replace("<sha>", "<b><color=#BF5DFF>").Replace("</sha>", "</color></b>").Replace("<min>", "<b><color=#FF69F6>").Replace("</min>", "</color></b>").Replace("<hol>", "<b><color=#fffbb9>").Replace("</hol>", "</color></b>").Replace("<pie>", "<b><color=#bafcc0>").Replace("</pie>", "</color></b>").Replace("<blu>", "<b><color=#ffe3ca>").Replace("</blu>", "</color></b>").Replace("<sla>", "<b><color=#bad5fc>").Replace("</sla>", "</color></b>").Replace("<sy>", "<color=#111111>").Replace("</sy>", "</color>").Replace("<br><br>", "<br><line-height=50%><br></line-height>").Replace("<br1>", "\n").Replace("<br2>", "\n").Replace("<br3>", "\n").Replace("\\n", "\n");

  public static string FormatStringCard(string text)
  {
    string input = Regex.Replace(new StringBuilder(text).Replace("<gl>", "<color=#5E3016>").Replace("</gl>", "</color>").Replace("<bl>", "<color=#263ABC>").Replace("</bl>", "</color>").Replace("<rd>", "<color=#720070>").Replace("</rd>", "</color>").Replace("<act>", "<size=-.15><color=#444>").Replace("</act>", "</size></color>").ToString(), "\\<i=(\\w+)\\>", "<size=+.1><voffset=-.1><space=.5><sprite name=$1></voffset></size>");
    string pattern = "<crd=(.*?),(.*?)>";
    foreach (Match match in Regex.Matches(input, pattern))
    {
      CardData cardData = Globals.Instance.GetCardData(match.Groups[1].ToString(), false);
      if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
      {
        string s = match.Groups[2].ToString();
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<color=#5E3016>");
        if (int.Parse(s) > 1)
        {
          stringBuilder.Append(s);
          stringBuilder.Append(" ");
        }
        stringBuilder.Append("  <sprite name=cards>");
        stringBuilder.Append(cardData.CardName);
        stringBuilder.Append("</color>");
        input = input.Replace(match.ToString(), stringBuilder.ToString());
      }
    }
    return input;
  }

  public static string StripTags(string input) => Regex.Replace(input, "<.*?>", string.Empty);

  public static string OnlyAscii(string str) => str != "" ? Regex.Replace(str, "[^\\u0000-\\u007F]+", string.Empty) : "";

  public static string Sanitize(string str)
  {
    if (str != "")
    {
      string str1 = Regex.Replace(str, "\\s+", "");
      if (str1 != "")
        return str1.ToLower().Trim().Replace("'", "");
    }
    return "";
  }

  public static Vector3 GetCardPosition(Vector3 posZero, int index, int total)
  {
    Vector3 cardPosition = posZero;
    float y1 = posZero.y;
    float num1 = 2.6f;
    float num2 = 3.2f;
    float num3 = posZero.y + num2 * 0.5f;
    float num4 = posZero.y - num2 * 0.5f;
    int num5 = 0;
    if (total > 10)
    {
      num5 = Mathf.FloorToInt((float) index / 10f);
      total = 10;
      index %= 10;
    }
    float y2 = num4 - (float) ((double) num5 * (double) num2 * 2.0);
    float y3 = num3 - (float) ((double) num5 * (double) num2 * 2.0);
    switch (total)
    {
      case 2:
        cardPosition = index != 0 ? new Vector3(posZero.x + num1 * 0.5f, y1) : new Vector3(posZero.x - num1 * 0.5f, y1);
        break;
      case 3:
        switch (index)
        {
          case 0:
            cardPosition = new Vector3(posZero.x - num1, y1);
            break;
          case 1:
            cardPosition = posZero;
            break;
          default:
            cardPosition = new Vector3(posZero.x + num1, y1);
            break;
        }
        break;
      case 4:
        switch (index)
        {
          case 0:
            cardPosition = new Vector3(posZero.x - num1 * 0.5f, y3);
            break;
          case 1:
            cardPosition = new Vector3(posZero.x + num1 * 0.5f, y3);
            break;
          case 2:
            cardPosition = new Vector3(posZero.x - num1 * 0.5f, y2);
            break;
          default:
            cardPosition = new Vector3(posZero.x + num1 * 0.5f, y2);
            break;
        }
        break;
      case 5:
        switch (index)
        {
          case 0:
            cardPosition = new Vector3(posZero.x - num1, y3);
            break;
          case 1:
            cardPosition = new Vector3(posZero.x, y3);
            break;
          case 2:
            cardPosition = new Vector3(posZero.x + num1, y3);
            break;
          case 3:
            cardPosition = new Vector3(posZero.x - num1 * 0.5f, y2);
            break;
          default:
            cardPosition = new Vector3(posZero.x + num1 * 0.5f, y2);
            break;
        }
        break;
      case 6:
        switch (index)
        {
          case 0:
            cardPosition = new Vector3(posZero.x - 2.2f, y3);
            break;
          case 1:
            cardPosition = new Vector3(posZero.x, y3);
            break;
          case 2:
            cardPosition = new Vector3(posZero.x + 2.2f, y3);
            break;
          case 3:
            cardPosition = new Vector3(posZero.x - 2.2f, y2);
            break;
          case 4:
            cardPosition = new Vector3(posZero.x, y2);
            break;
          default:
            cardPosition = new Vector3(posZero.x + 2.2f, y2);
            break;
        }
        break;
      case 7:
        switch (index)
        {
          case 0:
            cardPosition = new Vector3(posZero.x - (num1 * 0.5f + num1), y3);
            break;
          case 1:
            cardPosition = new Vector3(posZero.x - num1 * 0.5f, y3);
            break;
          case 2:
            cardPosition = new Vector3(posZero.x + num1 * 0.5f, y3);
            break;
          case 3:
            cardPosition = new Vector3(posZero.x + num1 * 0.5f + num1, y3);
            break;
          case 4:
            cardPosition = new Vector3(posZero.x - num1, y2);
            break;
          case 5:
            cardPosition = new Vector3(posZero.x, y2);
            break;
          default:
            cardPosition = new Vector3(posZero.x + num1, y2);
            break;
        }
        break;
      case 8:
        switch (index)
        {
          case 0:
            cardPosition = new Vector3(posZero.x - (num1 * 0.5f + num1), y3);
            break;
          case 1:
            cardPosition = new Vector3(posZero.x - num1 * 0.5f, y3);
            break;
          case 2:
            cardPosition = new Vector3(posZero.x + num1 * 0.5f, y3);
            break;
          case 3:
            cardPosition = new Vector3(posZero.x + num1 * 0.5f + num1, y3);
            break;
          case 4:
            cardPosition = new Vector3(posZero.x - (num1 * 0.5f + num1), y2);
            break;
          case 5:
            cardPosition = new Vector3(posZero.x - num1 * 0.5f, y2);
            break;
          case 6:
            cardPosition = new Vector3(posZero.x + num1 * 0.5f, y2);
            break;
          default:
            cardPosition = new Vector3(posZero.x + num1 * 0.5f + num1, y2);
            break;
        }
        break;
      case 9:
        switch (index)
        {
          case 0:
            cardPosition = new Vector3(posZero.x - num1 * 2f, y3);
            break;
          case 1:
            cardPosition = new Vector3(posZero.x - num1, y3);
            break;
          case 2:
            cardPosition = new Vector3(posZero.x, y3);
            break;
          case 3:
            cardPosition = new Vector3(posZero.x + num1, y3);
            break;
          case 4:
            cardPosition = new Vector3(posZero.x + num1 * 2f, y3);
            break;
          case 5:
            cardPosition = new Vector3(posZero.x - (num1 * 0.5f + num1), y2);
            break;
          case 6:
            cardPosition = new Vector3(posZero.x - num1 * 0.5f, y2);
            break;
          case 7:
            cardPosition = new Vector3(posZero.x + num1 * 0.5f, y2);
            break;
          default:
            cardPosition = new Vector3(posZero.x + num1 * 0.5f + num1, y2);
            break;
        }
        break;
      case 10:
        switch (index)
        {
          case 0:
            cardPosition = new Vector3(posZero.x - num1 * 2f, y3);
            break;
          case 1:
            cardPosition = new Vector3(posZero.x - num1, y3);
            break;
          case 2:
            cardPosition = new Vector3(posZero.x, y3);
            break;
          case 3:
            cardPosition = new Vector3(posZero.x + num1, y3);
            break;
          case 4:
            cardPosition = new Vector3(posZero.x + num1 * 2f, y3);
            break;
          case 5:
            cardPosition = new Vector3(posZero.x - num1 * 2f, y2);
            break;
          case 6:
            cardPosition = new Vector3(posZero.x - num1, y2);
            break;
          case 7:
            cardPosition = new Vector3(posZero.x, y2);
            break;
          case 8:
            cardPosition = new Vector3(posZero.x + num1, y2);
            break;
          default:
            cardPosition = new Vector3(posZero.x + num1 * 2f, y2);
            break;
        }
        break;
    }
    cardPosition = new Vector3(cardPosition.x, cardPosition.y, (float) ((double) index * -0.0099999997764825821 - 3.0));
    return cardPosition;
  }

  public static MonoBehaviour GetSpriteSkinComponent(this GameObject gameObject)
  {
    foreach (MonoBehaviour component in gameObject.GetComponents<MonoBehaviour>())
    {
      if (component.GetType().FullName == "UnityEngine.Experimental.U2D.Animation.SpriteSkin")
        return component;
    }
    return (MonoBehaviour) null;
  }

  public static string GetRandomCardIdByTypeAndRandomRarity(Enums.CardType _cardType)
  {
    CardData cardData = Globals.Instance.GetCardData(Globals.Instance.CardListByType[Enums.CardType.Small_Weapon][MatchManager.Instance.GetRandomIntRange(0, Globals.Instance.CardListByType[Enums.CardType.Small_Weapon].Count)], false);
    return Functions.GetCardByRarity(MatchManager.Instance.GetRandomIntRange(0, 100), cardData);
  }

  public static List<string> GetIdListVarsFromCard(string _card)
  {
    CardData cardData = Globals.Instance.GetCardData(_card, false);
    if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
    {
      List<string> listVarsFromCard = new List<string>();
      if (cardData.CardUpgraded == Enums.CardUpgraded.No)
      {
        listVarsFromCard.Add(_card.ToLower());
      }
      else
      {
        cardData = Globals.Instance.GetCardData(cardData.UpgradedFrom, false);
        if ((UnityEngine.Object) cardData != (UnityEngine.Object) null)
          listVarsFromCard.Add(cardData.Id.ToLower());
      }
      if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && listVarsFromCard.Count == 1)
      {
        listVarsFromCard.Add(cardData.UpgradesTo1.ToLower());
        listVarsFromCard.Add(cardData.UpgradesTo2.ToLower());
        if ((UnityEngine.Object) cardData.UpgradesToRare != (UnityEngine.Object) null)
          listVarsFromCard.Add(cardData.UpgradesToRare.Id.ToLower());
        return listVarsFromCard;
      }
    }
    return (List<string>) null;
  }

  public static CardData GetCardDataFromCardData(CardData _cardData, string type)
  {
    if ((UnityEngine.Object) _cardData == (UnityEngine.Object) null)
      return (CardData) null;
    CardData dataFromCardData = _cardData;
    if (type != "")
      type = type.ToUpper();
    switch (type)
    {
      case "A":
        dataFromCardData = dataFromCardData.CardUpgraded != Enums.CardUpgraded.No ? Globals.Instance.GetCardData(_cardData.UpgradedFrom + "A", false) : Globals.Instance.GetCardData(_cardData.UpgradesTo1, false);
        break;
      case "B":
        dataFromCardData = dataFromCardData.CardUpgraded != Enums.CardUpgraded.No ? Globals.Instance.GetCardData(_cardData.UpgradedFrom + "B", false) : Globals.Instance.GetCardData(_cardData.UpgradesTo2, false);
        break;
      case "RARE":
        if (dataFromCardData.CardUpgraded != Enums.CardUpgraded.No)
          dataFromCardData = Globals.Instance.GetCardData(_cardData.UpgradedFrom, false);
        if (!((UnityEngine.Object) dataFromCardData.UpgradesToRare != (UnityEngine.Object) null))
          return (CardData) null;
        dataFromCardData = dataFromCardData.UpgradesToRare;
        break;
      case "":
        if (dataFromCardData.CardUpgraded != Enums.CardUpgraded.No)
        {
          dataFromCardData = Globals.Instance.GetCardData(_cardData.UpgradedFrom, false);
          break;
        }
        break;
    }
    return dataFromCardData;
  }

  public static bool CardIsPercentRarity(int rarity, CardData _cardData) => rarity < 15 && _cardData.CardRarity == Enums.CardRarity.Common || rarity >= 15 && rarity < 45 && _cardData.CardRarity == Enums.CardRarity.Uncommon || rarity >= 45 && rarity < 80 && _cardData.CardRarity == Enums.CardRarity.Rare || rarity >= 80 && rarity < 95 && _cardData.CardRarity == Enums.CardRarity.Epic || rarity >= 95 && rarity < 100 && _cardData.CardRarity == Enums.CardRarity.Mythic;

  public static string GetCardByRarity(int rarity, CardData _cardData, bool isChallenge = false)
  {
    int num1 = 78;
    int num2 = 88;
    int num3 = 98;
    int num4;
    if (!GameManager.Instance.IsObeliskChallenge())
    {
      num4 = AtOManager.Instance.GetMadnessDifficulty();
      if (num4 > 0)
      {
        num1 = 75;
        num2 = 85;
        num3 = 95;
      }
    }
    else
      num4 = AtOManager.Instance.GetObeliskMadness();
    if (num4 > 0)
    {
      num1 -= num4;
      num2 -= Functions.FuncRoundToInt((float) num4 * 0.5f);
    }
    if (rarity < num1)
      return _cardData.CardUpgraded == Enums.CardUpgraded.No || !(_cardData.UpgradedFrom != "") ? _cardData.Id.ToLower() : _cardData.UpgradedFrom.ToLower();
    if (rarity >= num1 && rarity < num2)
    {
      if (_cardData.CardUpgraded == Enums.CardUpgraded.A)
        return _cardData.Id.ToLower();
      if (_cardData.CardUpgraded == Enums.CardUpgraded.No)
        return _cardData.UpgradesTo1.ToLower();
      if (_cardData.CardUpgraded == Enums.CardUpgraded.B)
        return (_cardData.UpgradedFrom + "A").ToLower();
    }
    else
    {
      if (rarity >= num2 && rarity < num3)
      {
        if (_cardData.CardUpgraded == Enums.CardUpgraded.B)
          return _cardData.Id.ToLower();
        return _cardData.CardUpgraded == Enums.CardUpgraded.No ? _cardData.UpgradesTo2.ToLower() : (_cardData.UpgradedFrom + "B").ToLower();
      }
      if (_cardData.CardUpgraded == Enums.CardUpgraded.No)
        return (UnityEngine.Object) _cardData.UpgradesToRare != (UnityEngine.Object) null ? _cardData.UpgradesToRare.Id.ToLower() : _cardData.Id.ToLower();
      CardData cardData = Globals.Instance.GetCardData(_cardData.UpgradedFrom, false);
      if ((UnityEngine.Object) cardData != (UnityEngine.Object) null && (UnityEngine.Object) cardData.UpgradesToRare != (UnityEngine.Object) null)
        return cardData.UpgradesToRare.Id.ToLower();
    }
    return _cardData.Id.ToLower();
  }

  public static string GetTimestamp() => GameManager.Instance.GetTime().ToString("yyyyMMddHHmmss");

  public static string GetTimestampString() => DateTime.Now.ToString("HH:mm:ss");

  public static string Md5Sum(string strToEncrypt)
  {
    byte[] hash = new MD5CryptoServiceProvider().ComputeHash(new UTF8Encoding().GetBytes(strToEncrypt));
    string str = "";
    for (int index = 0; index < hash.Length; ++index)
      str += Convert.ToString(hash[index], 16).PadLeft(2, '0');
    return str.PadLeft(32, '0');
  }

  public static Color HexToColor(string hex)
  {
    hex = hex.Replace("0x", "");
    hex = hex.Replace("#", "");
    byte maxValue = byte.MaxValue;
    int r = (int) byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
    byte num1 = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
    byte num2 = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
    if (hex.Length == 8)
      maxValue = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
    int g = (int) num1;
    int b = (int) num2;
    int a = (int) maxValue;
    return (Color) new Color32((byte) r, (byte) g, (byte) b, (byte) a);
  }

  public static IEnumerator FollowArc(
    Transform mover,
    Vector2 start,
    Vector2 end,
    float radius,
    float duration)
  {
    Vector2 vector2_1 = end - start;
    float magnitude = vector2_1.magnitude;
    float absRadius = Mathf.Abs(radius);
    if ((double) magnitude > 2.0 * (double) absRadius)
      radius = absRadius = magnitude / 2f;
    Vector2 vector2_2 = new Vector2(vector2_1.y, -vector2_1.x) / magnitude * (Mathf.Sign(radius) * Mathf.Sqrt((float) ((double) radius * (double) radius - (double) magnitude * (double) magnitude / 4.0)));
    Vector2 center = start + vector2_1 / 2f + vector2_2;
    Vector2 vector2_3 = start - center;
    float startAngle = Mathf.Atan2(vector2_3.y, vector2_3.x);
    Vector2 vector2_4 = end - center;
    float travel = (float) (((double) Mathf.Atan2(vector2_4.y, vector2_4.x) - (double) startAngle + 15.707963943481445) % 6.2831854820251465 - 3.1415927410125732);
    float progress = 0.0f;
    do
    {
      float f = startAngle + progress * travel;
      mover.position = (Vector3) (center + new Vector2(Mathf.Cos(f), Mathf.Sin(f)) * absRadius);
      progress += Time.deltaTime / duration;
      yield return (object) null;
    }
    while ((double) progress < 1.0);
    mover.position = (Vector3) end;
  }

  public static int GetCurrentWeeklyWeek()
  {
    if (AtOManager.Instance.weeklyForcedId != "")
      return int.Parse(AtOManager.Instance.weeklyForcedId.Replace("week", ""));
    DateTime time = GameManager.Instance.GetTime();
    foreach (KeyValuePair<string, ChallengeData> keyValuePair in Globals.Instance.WeeklyDataSource)
    {
      if (keyValuePair.Value.GetDateFrom() < time && keyValuePair.Value.GetDateTo() > time)
        return keyValuePair.Value.Week;
    }
    return time < Globals.Instance.InitialWeeklyDate ? 1 : (int) ((time - Globals.Instance.InitialWeeklyDate).TotalDays / 7.0) + 1;
  }

  public static DateTime LastDateForWeek()
  {
    DateTime dateTo = Globals.Instance.GetWeeklyData(Functions.GetCurrentWeeklyWeek()).GetDateTo();
    DateTime time = GameManager.Instance.GetTime();
    return dateTo > time ? dateTo : Globals.Instance.InitialWeeklyDate.AddDays((double) (Functions.GetCurrentWeeklyWeek() * 7));
  }

  public static TimeSpan TimeSpanLeftWeekly()
  {
    DateTime time = GameManager.Instance.GetTime();
    return Functions.LastDateForWeek() - time;
  }

  public static DateTime GetDummyDate() => new DateTime(1000, 1, 1);

  public static DateTime GetServerTime()
  {
    Debug.Log((object) "GetServerTime <----------------");
    System.Random random = new System.Random(DateTime.Now.Millisecond);
    DateTime serverTime = Functions.GetDummyDate();
    string empty = string.Empty;
    if (!Functions.serversRandomized)
      Functions.ShuffleArray<string>(Functions.serversMaster);
    for (int index = 0; index < 5; ++index)
    {
      try
      {
        string hostname = Functions.serversMaster[Functions.serverIndex];
        ++Functions.serverIndex;
        if (Functions.serverIndex >= Functions.serversMaster.Length)
          Functions.serverIndex = 0;
        TcpClient tcpClient = new TcpClient();
        tcpClient.Connect(hostname, 13);
        if (!tcpClient.Connected)
        {
          Debug.Log((object) "Socket not connected");
        }
        else
        {
          StreamReader streamReader = new StreamReader((Stream) tcpClient.GetStream());
          string end = streamReader.ReadToEnd();
          streamReader.Close();
          if (end.Length > 47)
          {
            if (end.Substring(38, 9).Equals("UTC(NIST)"))
            {
              int num1 = int.Parse(end.Substring(1, 5));
              int num2 = int.Parse(end.Substring(7, 2));
              int month = int.Parse(end.Substring(10, 2));
              int day = int.Parse(end.Substring(13, 2));
              int hour = int.Parse(end.Substring(16, 2));
              int minute = int.Parse(end.Substring(19, 2));
              int second = int.Parse(end.Substring(22, 2));
              serverTime = new DateTime(num1 <= 51544 ? num2 + 1999 : num2 + 2000, month, day, hour, minute, second);
              break;
            }
          }
        }
      }
      catch (Exception ex)
      {
        if (GameManager.Instance.GetDeveloperMode())
          Debug.LogWarning((object) ("AddCharges exception-> " + ex?.ToString()));
      }
    }
    return serverTime;
  }

  public static int StringToAsciiInt32(string str)
  {
    byte[] bytes = Encoding.ASCII.GetBytes(AtOManager.Instance.GetGameId());
    int total = 0;
    Action<byte> action = (Action<byte>) (i => total += (int) i);
    Array.ForEach<byte>(bytes, action);
    return Convert.ToInt32(total);
  }

  public static double ConvertBytesToMegabytes(long bytes) => (double) bytes / 1024.0 / 1024.0;

  public static int AsciiSumFromString(string _str)
  {
    byte[] bytes = Encoding.ASCII.GetBytes(_str);
    int total = 0;
    Action<byte> action = (Action<byte>) (i => total += (int) i);
    Array.ForEach<byte>(bytes, action);
    return total;
  }

  public static string GetAuraCurseImmune(NPCData _npcData, string _mapNode = "")
  {
    List<string> stringList = new List<string>();
    stringList.Add("bleed");
    stringList.Add("burn");
    stringList.Add("chill");
    stringList.Add("crack");
    stringList.Add("dark");
    stringList.Add("decay");
    stringList.Add("insane");
    stringList.Add("mark");
    stringList.Add("poison");
    stringList.Add("sanctify");
    stringList.Add("sight");
    stringList.Add("slow");
    stringList.Add("spark");
    stringList.Add("vulnerable");
    stringList.Add("wet");
    StringBuilder stringBuilder = new StringBuilder();
    if (_mapNode == "")
      _mapNode = AtOManager.Instance.currentMapNode;
    stringBuilder.Append(_mapNode);
    stringBuilder.Append(AtOManager.Instance.GetGameId());
    stringBuilder.Append(AtOManager.Instance.GetObeliskMadness());
    stringBuilder.Append(AtOManager.Instance.GetMadnessDifficulty());
    int num = 0;
    string auraCurseImmune = "";
    for (; num < 100; ++num)
    {
      int index = (int) ((double) Functions.AsciiSumFromString(stringBuilder.ToString()) % (double) stringList.Count);
      auraCurseImmune = stringList[index];
      if ((UnityEngine.Object) _npcData != (UnityEngine.Object) null && _npcData.AuracurseImmune != null && _npcData.AuracurseImmune.Contains(auraCurseImmune))
        auraCurseImmune = "";
      else
        break;
    }
    return auraCurseImmune;
  }

  public static bool TransformIsVisible(Transform _obj)
  {
    if ((UnityEngine.Object) _obj == (UnityEngine.Object) null)
      return false;
    for (; (UnityEngine.Object) _obj != (UnityEngine.Object) null; _obj = _obj.parent)
    {
      if (!_obj.gameObject.activeSelf)
        return false;
    }
    return true;
  }

  public static int GetTransformIndexInList(List<Transform> theList, string theName)
  {
    for (int index = 0; index < theList.Count; ++index)
    {
      if ((UnityEngine.Object) theList[index] != (UnityEngine.Object) null && theList[index].gameObject.name == theName)
        return index;
    }
    return 0;
  }

  public static int GetListClosestIndexToMousePosition(List<Transform> theList)
  {
    Vector3 b = GameManager.Instance.cameraMain.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0.0f, 0.0f, 10f);
    Vector3 zero = Vector3.zero;
    float num = 10000f;
    int indexToMousePosition = 0;
    for (int index = 0; index < theList.Count; ++index)
    {
      if ((UnityEngine.Object) theList[index] != (UnityEngine.Object) null)
      {
        zero.x = theList[index].position.x;
        zero.y = theList[index].position.y;
        if ((double) Vector3.Distance(zero, b) < (double) num)
        {
          indexToMousePosition = index;
          num = Vector3.Distance(zero, b);
        }
      }
    }
    return indexToMousePosition;
  }

  public static int GetClosestIndexFromList(
    Transform theTransform,
    List<Transform> theList,
    int excludeIndex = -1,
    Vector3 offset = default (Vector3))
  {
    Vector3 vector3 = theTransform.position + offset;
    Vector3 zero1 = Vector3.zero with
    {
      x = vector3.x,
      y = vector3.y
    };
    Vector3 zero2 = Vector3.zero;
    float num = float.MaxValue;
    int closestIndexFromList = -1;
    for (int index = 0; index < theList.Count; ++index)
    {
      if (index != excludeIndex && (UnityEngine.Object) theList[index] != (UnityEngine.Object) null)
      {
        zero2.x = theList[index].position.x;
        zero2.y = theList[index].position.y;
        if ((double) Vector3.Distance(zero2, zero1) < (double) num)
        {
          num = Vector3.Distance(zero2, zero1);
          closestIndexFromList = index;
        }
      }
    }
    return closestIndexFromList;
  }

  public static int GetClosestIndexBasedOnDirection(
    List<Transform> theList,
    int index,
    bool goingUp,
    bool goingRight,
    bool goingDown,
    bool goingLeft,
    bool checkUiItems = false)
  {
    if (theList == null || theList.Count == 0 || index < 0 || index > theList.Count - 1)
      return 0;
    Vector3 position = theList[index].position;
    List<Transform> transformList1 = new List<Transform>();
    List<Transform> transformList2 = new List<Transform>();
    transformList1.Add(theList[index]);
    transformList2.Add(theList[index]);
    for (int index1 = 0; index1 < theList.Count; ++index1)
    {
      if (index1 != index && (UnityEngine.Object) theList[index1] != (UnityEngine.Object) null)
      {
        if ((double) theList[index1].position.y == (double) position.y)
        {
          bool flag = false;
          for (int index2 = 0; index2 < transformList1.Count; ++index2)
          {
            if ((double) theList[index1].position.x < (double) transformList1[index2].position.x)
            {
              transformList1.Insert(index2, theList[index1]);
              flag = true;
              break;
            }
          }
          if (!flag)
            transformList1.Add(theList[index1]);
        }
        else if ((double) theList[index1].position.x == (double) position.x)
        {
          bool flag = false;
          for (int index3 = 0; index3 < transformList2.Count; ++index3)
          {
            if ((double) theList[index1].position.y < (double) transformList2[index3].position.y)
            {
              transformList2.Insert(index3, theList[index1]);
              flag = true;
              break;
            }
          }
          if (!flag)
            transformList2.Add(theList[index1]);
        }
      }
    }
    if (goingLeft | goingRight && transformList1.Count > 1)
    {
      for (int index4 = 0; index4 < transformList1.Count; ++index4)
      {
        if ((UnityEngine.Object) transformList1[index4] == (UnityEngine.Object) theList[index])
        {
          if (goingRight)
          {
            if (index4 < transformList1.Count - 1)
            {
              Transform transform = transformList1[index4 + 1];
              break;
            }
          }
          else if (index4 > 0)
          {
            Transform transform = transformList1[index4 - 1];
            break;
          }
        }
      }
    }
    bool flag1 = false;
    int num1 = -1;
    int num2 = index;
    int num3 = 100;
    while (!flag1 && num1 < num3)
    {
      ++num1;
      float num4 = num1 >= num3 / 2 ? (float) (num1 - num3 / 2 + 1) * 0.3f : (!(goingLeft | goingRight) ? (float) (num1 + 1) * 0.035f : (float) (num1 + 1) * 0.04f);
      if (checkUiItems)
      {
        if (goingUp | goingDown)
          num4 *= 200f;
        else
          num4 *= 1000f;
      }
      index = num2;
      if (goingDown)
      {
        index = Functions.GetClosestIndexFromList(theList[index], theList, index, new Vector3(0.0f, -num4, 0.0f));
        if (num1 < num3 / 2)
        {
          if (index > -1 && (double) position.y - (double) theList[index].position.y > 0.25 && (double) Mathf.Abs(position.x - theList[index].position.x) < 0.10000000149011612)
            flag1 = true;
        }
        else if (index > -1 && (double) position.y - (double) theList[index].position.y > 0.25)
          flag1 = true;
      }
      else if (goingUp)
      {
        index = Functions.GetClosestIndexFromList(theList[index], theList, index, new Vector3(0.0f, num4, 0.0f));
        if (num1 < num3 / 2)
        {
          if (index > -1 && (double) theList[index].position.y - (double) position.y > 0.25 && (double) Mathf.Abs(position.x - theList[index].position.x) < 0.10000000149011612)
            flag1 = true;
        }
        else if (index > -1 && (double) theList[index].position.y - (double) position.y > 0.25)
          flag1 = true;
      }
      else if (goingRight)
      {
        index = Functions.GetClosestIndexFromList(theList[index], theList, index, new Vector3(num4, 0.0f, 0.0f));
        if (index <= -1 || !transformList2.Contains(theList[index]))
        {
          if (num1 < num3 / 2)
          {
            if (index > -1 && (double) theList[index].position.x - (double) position.x > 0.25 && (double) Mathf.Abs(position.y - theList[index].position.y) < 0.30000001192092896)
              flag1 = true;
          }
          else if (index > -1 && (double) theList[index].position.x - (double) position.x > 0.25)
            flag1 = true;
        }
      }
      else if (goingLeft)
      {
        index = Functions.GetClosestIndexFromList(theList[index], theList, index, new Vector3(-num4, 0.0f, 0.0f));
        if (index <= -1 || !transformList2.Contains(theList[index]))
        {
          if (num1 < num3 / 2)
          {
            if (index > -1 && (double) position.x - (double) theList[index].position.x > 0.25 && (double) Mathf.Abs(position.y - theList[index].position.y) < 0.30000001192092896)
              flag1 = true;
          }
          else if (index > -1 && (double) position.x - (double) theList[index].position.x > 0.25)
            flag1 = true;
        }
      }
    }
    if (!flag1)
      index = num2;
    if (index > theList.Count - 1)
      index = theList.Count - 1;
    else if (index < 0)
      index = 0;
    return index;
  }

  public static List<Transform> FindChildrenByName(Transform transform, string[] namesToFind)
  {
    List<Transform> foundChildren = new List<Transform>();
    Functions.FindChildrenByNameRecursive(transform, namesToFind, foundChildren);
    return foundChildren;
  }

  private static void FindChildrenByNameRecursive(
    Transform parent,
    string[] namesToFind,
    List<Transform> foundChildren)
  {
    foreach (Transform parent1 in parent)
    {
      if (Functions.ArrayContainsString(namesToFind, parent1.name))
        foundChildren.Add(parent1);
      Functions.FindChildrenByNameRecursive(parent1, namesToFind, foundChildren);
    }
  }

  private static bool ArrayContainsString(string[] array, string target)
  {
    foreach (string str in array)
    {
      if (str == target)
        return true;
    }
    return false;
  }

  public static bool ClickedThisTransform(Transform _transform)
  {
    RaycastHit2D[] raycastHit2DArray = Physics2D.RaycastAll((Vector2) GameManager.Instance.cameraMain.ScreenToWorldPoint(Input.mousePosition), (Vector2) Vector3.forward, 10f);
    for (int index = 0; index < raycastHit2DArray.Length; ++index)
    {
      if ((UnityEngine.Object) raycastHit2DArray[index].collider != (UnityEngine.Object) null && ((UnityEngine.Object) raycastHit2DArray[index].transform == (UnityEngine.Object) _transform || raycastHit2DArray[index].transform.gameObject.name == _transform.gameObject.name))
        return true;
    }
    return false;
  }

  public static string GetNodeGroundSprite(Enums.NodeGround _ground)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<sprite name=");
    switch (_ground)
    {
      case Enums.NodeGround.HeavyRain:
        stringBuilder.Append("wet");
        break;
      case Enums.NodeGround.ExtremeHeat:
        stringBuilder.Append("burn");
        break;
      case Enums.NodeGround.FreezingCold:
        stringBuilder.Append("chill");
        break;
      case Enums.NodeGround.HolyGround:
        stringBuilder.Append("sanctify");
        break;
      case Enums.NodeGround.Graveyard:
        stringBuilder.Append("dark");
        break;
      case Enums.NodeGround.PoisonousAir:
        stringBuilder.Append("poison");
        break;
      default:
        return "";
    }
    stringBuilder.Append(">");
    return stringBuilder.ToString();
  }

  public static int Random(int min, int max, string seed)
  {
    if (min == max)
      return min;
    int num1 = 0;
    int length = seed.Length;
    for (int index = 0; index < length; ++index)
    {
      int num2 = num1 + (int) seed[index];
      int num3 = num2 + (num2 << 10);
      num1 = num3 ^ num3 >> 6;
    }
    int num4 = num1 + (num1 << 3);
    int num5 = num4 ^ num4 >> 11;
    int num6 = num5 + (num5 << 15);
    int num7 = max - min;
    return min + Mathf.Abs(num6) % num7;
  }
}
