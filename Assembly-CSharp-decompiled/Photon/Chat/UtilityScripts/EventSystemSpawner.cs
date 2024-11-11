// Decompiled with JetBrains decompiler
// Type: Photon.Chat.UtilityScripts.EventSystemSpawner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

namespace Photon.Chat.UtilityScripts
{
  public class EventSystemSpawner : MonoBehaviour
  {
    private void OnEnable()
    {
      if (!((Object) Object.FindObjectOfType<EventSystem>() == (Object) null))
        return;
      GameObject gameObject = new GameObject("EventSystem");
      gameObject.AddComponent<EventSystem>();
      gameObject.AddComponent<StandaloneInputModule>();
    }
  }
}
