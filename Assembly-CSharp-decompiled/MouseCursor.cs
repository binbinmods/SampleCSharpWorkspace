// Decompiled with JetBrains decompiler
// Type: MouseCursor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MouseCursor : MonoBehaviour
{
  public Transform imageUI;
  private RectTransform imageUIR;
  private GameObject click;
  private float originalSize = 25f;
  private float scale = 1f;
  private float cursorOffset;

  public static MouseCursor Instance { get; private set; }

  private void Awake()
  {
    if ((Object) MouseCursor.Instance == (Object) null)
      MouseCursor.Instance = this;
    else if ((Object) MouseCursor.Instance != (Object) this)
      Object.Destroy((Object) this.gameObject);
    Object.DontDestroyOnLoad((Object) this.gameObject);
    this.imageUIR = this.imageUI.GetComponent<RectTransform>();
  }

  private void Start()
  {
    Cursor.visible = false;
    this.scale = (float) (Screen.width / 1920);
    this.imageUI.localScale = new Vector3(this.scale, this.scale, 1f);
    this.cursorOffset = this.originalSize * this.scale;
  }

  public void Show() => this.imageUI.gameObject.SetActive(true);

  public void Hide() => this.imageUI.gameObject.SetActive(false);
}
