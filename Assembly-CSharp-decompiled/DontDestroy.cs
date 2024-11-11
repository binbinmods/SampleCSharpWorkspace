// Decompiled with JetBrains decompiler
// Type: DontDestroy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DontDestroy : MonoBehaviour
{
  private void Awake() => Object.DontDestroyOnLoad((Object) this.gameObject);
}
