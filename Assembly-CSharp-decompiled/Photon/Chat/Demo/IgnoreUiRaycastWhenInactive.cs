// Decompiled with JetBrains decompiler
// Type: Photon.Chat.Demo.IgnoreUiRaycastWhenInactive
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace Photon.Chat.Demo
{
  public class IgnoreUiRaycastWhenInactive : MonoBehaviour, ICanvasRaycastFilter
  {
    public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera) => this.gameObject.activeInHierarchy;
  }
}
