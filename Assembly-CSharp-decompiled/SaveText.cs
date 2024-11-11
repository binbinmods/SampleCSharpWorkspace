// Decompiled with JetBrains decompiler
// Type: SaveText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.IO;

public static class SaveText
{
  public static void SaveEvents(string str)
  {
    string str1 = "EventsText.txt";
    string path = "Assets/TextsFromGame/";
    if (!Directory.Exists(path))
      return;
    StreamWriter text = File.CreateText(path + str1);
    text.WriteLine(str);
    text.Close();
  }

  public static void SaveAuras(string str)
  {
    string str1 = "AurasText.txt";
    string path = "Assets/TextsFromGame/";
    if (!Directory.Exists(path))
      return;
    StreamWriter text = File.CreateText(path + str1);
    text.WriteLine(str);
    text.Close();
  }

  public static void SaveTraits(string str)
  {
    string str1 = "TraitsText.txt";
    string path = "Assets/TextsFromGame/";
    if (!Directory.Exists(path))
      return;
    StreamWriter text = File.CreateText(path + str1);
    text.WriteLine(str);
    text.Close();
  }

  public static void SaveClass(string str)
  {
    string str1 = "ClassText.txt";
    string path = "Assets/TextsFromGame/";
    if (!Directory.Exists(path))
      return;
    StreamWriter text = File.CreateText(path + str1);
    text.WriteLine(str);
    text.Close();
  }

  public static void SaveMonster(string str)
  {
    string str1 = "MonsterText.txt";
    string path = "Assets/TextsFromGame/";
    if (!Directory.Exists(path))
      return;
    StreamWriter text = File.CreateText(path + str1);
    text.WriteLine(str);
    text.Close();
  }

  public static void SaveCards(string str)
  {
    string str1 = "CardsText.txt";
    string path = "Assets/TextsFromGame/";
    if (!Directory.Exists(path))
      return;
    StreamWriter text = File.CreateText(path + str1);
    text.WriteLine(str);
    text.Close();
  }

  public static void SaveCardsFluff(string str)
  {
    string str1 = "CardsFluffText.txt";
    string path = "Assets/TextsFromGame/";
    if (!Directory.Exists(path))
      return;
    StreamWriter text = File.CreateText(path + str1);
    text.WriteLine(str);
    text.Close();
  }

  public static void SaveKeynotes(string str)
  {
    string str1 = "KeynotesText.txt";
    string path = "Assets/TextsFromGame/";
    if (!Directory.Exists(path))
      return;
    StreamWriter text = File.CreateText(path + str1);
    text.WriteLine(str);
    text.Close();
  }

  public static void SaveNodes(string str)
  {
    string str1 = "NodesText.txt";
    string path = "Assets/TextsFromGame/";
    if (!Directory.Exists(path))
      return;
    StreamWriter text = File.CreateText(path + str1);
    text.WriteLine(str);
    text.Close();
  }

  public static void SaveRequirements(string str)
  {
    string str1 = "RequirementsText.txt";
    string path = "Assets/TextsFromGame/";
    if (!Directory.Exists(path))
      return;
    StreamWriter text = File.CreateText(path + str1);
    text.WriteLine(str);
    text.Close();
  }

  public static void SaveCards()
  {
  }
}
