// Decompiled with JetBrains decompiler
// Type: TrailerPoster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class TrailerPoster : MonoBehaviour
{
  public GameObject[] poster;

  public static TrailerPoster Instance { get; private set; }

  private void Awake()
  {
    if ((Object) TrailerPoster.Instance == (Object) null)
      TrailerPoster.Instance = this;
    else if ((Object) TrailerPoster.Instance != (Object) this)
      Object.Destroy((Object) this);
    Object.DontDestroyOnLoad((Object) this.gameObject);
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Alpha1))
      this.ShowPoster(0);
    if (Input.GetKeyDown(KeyCode.Alpha2))
      this.ShowPoster(1);
    if (Input.GetKeyDown(KeyCode.Alpha3))
      this.ShowPoster(2);
    if (Input.GetKeyDown(KeyCode.Alpha4))
      this.ShowPoster(3);
    if (!Input.GetKeyDown(KeyCode.Alpha5))
      return;
    this.ShowPoster(4);
  }

  private void ShowPoster(int _poster)
  {
    Debug.Log((object) ("Show Poster " + _poster.ToString()));
    for (int index = 0; index < this.poster.Length; ++index)
      this.poster[index].gameObject.SetActive(false);
    if (_poster >= this.poster.Length || !((Object) this.poster[_poster] != (Object) null))
      return;
    this.poster[_poster].gameObject.SetActive(true);
  }
}
