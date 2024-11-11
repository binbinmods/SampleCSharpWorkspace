// Decompiled with JetBrains decompiler
// Type: BlurRenderer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class BlurRenderer : MonoBehaviour
{
  public Camera blurCamera;
  public Material blurMaterial;

  private void Start() => this.CaptureScreen();

  private void OnEnable() => this.CaptureScreen();

  public void CaptureScreen()
  {
    Debug.Log((object) nameof (CaptureScreen));
    int width = Screen.width;
    int height = Screen.height;
    int depth = 24;
    RenderTexture renderTexture = new RenderTexture(height, width, depth);
    Rect source = new Rect(0.0f, 0.0f, (float) height, (float) width);
    Texture2D texture2D = new Texture2D(height, width, TextureFormat.RGBA32, false);
    this.blurCamera.targetTexture = renderTexture;
    this.blurCamera.Render();
    RenderTexture active = RenderTexture.active;
    RenderTexture.active = renderTexture;
    texture2D.ReadPixels(source, 0, 0);
    texture2D.Apply();
    this.blurCamera.targetTexture = (RenderTexture) null;
    RenderTexture.active = active;
    Object.Destroy((Object) renderTexture);
    this.blurMaterial.SetTexture("_RenTex", (Texture) texture2D);
  }
}
