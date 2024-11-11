// Decompiled with JetBrains decompiler
// Type: TomeManagerScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class TomeManagerScene : MonoBehaviour
{
  public Transform sceneCamera;

  private void Awake() => this.sceneCamera.gameObject.SetActive(false);

  private void Start()
  {
    AudioManager.Instance.DoBSO("Town");
    TomeManager.Instance.ShowTome(true);
    GameManager.Instance.SceneLoaded();
  }
}
