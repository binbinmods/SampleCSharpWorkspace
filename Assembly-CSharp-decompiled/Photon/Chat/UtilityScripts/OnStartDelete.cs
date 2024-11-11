// Decompiled with JetBrains decompiler
// Type: Photon.Chat.UtilityScripts.OnStartDelete
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace Photon.Chat.UtilityScripts
{
  public class OnStartDelete : MonoBehaviour
  {
    private void Start() => Object.Destroy((Object) this.gameObject);
  }
}
