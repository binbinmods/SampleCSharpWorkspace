// Decompiled with JetBrains decompiler
// Type: JsonHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public static class JsonHelper
{
  public static T[] FromJson<T>(string json) => JsonUtility.FromJson<JsonHelper.Wrapper<T>>(json).Items;

  public static string ToJson<T>(T[] array) => JsonUtility.ToJson((object) new JsonHelper.Wrapper<T>()
  {
    Items = array
  });

  public static string ToJson<T>(T[] array, bool prettyPrint) => JsonUtility.ToJson((object) new JsonHelper.Wrapper<T>()
  {
    Items = array
  }, prettyPrint);

  [Serializable]
  private class Wrapper<T>
  {
    public T[] Items;
  }
}
