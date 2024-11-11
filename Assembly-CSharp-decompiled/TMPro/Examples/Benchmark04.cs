// Decompiled with JetBrains decompiler
// Type: TMPro.Examples.Benchmark04
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace TMPro.Examples
{
  public class Benchmark04 : MonoBehaviour
  {
    public int SpawnType;
    public int MinPointSize = 12;
    public int MaxPointSize = 64;
    public int Steps = 4;
    private Transform m_Transform;

    private void Start()
    {
      this.m_Transform = this.transform;
      float num1 = 0.0f;
      float num2 = Camera.main.orthographicSize = (float) (Screen.height / 2);
      float num3 = (float) Screen.width / (float) Screen.height;
      for (int minPointSize = this.MinPointSize; minPointSize <= this.MaxPointSize; minPointSize += this.Steps)
      {
        if (this.SpawnType == 0)
        {
          GameObject gameObject = new GameObject("Text - " + minPointSize.ToString() + " Pts");
          if ((double) num1 > (double) num2 * 2.0)
            break;
          gameObject.transform.position = this.m_Transform.position + new Vector3((float) ((double) num3 * -(double) num2 * 0.97500002384185791), num2 * 0.975f - num1, 0.0f);
          TextMeshPro textMeshPro = gameObject.AddComponent<TextMeshPro>();
          textMeshPro.rectTransform.pivot = new Vector2(0.0f, 0.5f);
          textMeshPro.enableWordWrapping = false;
          textMeshPro.extraPadding = true;
          textMeshPro.isOrthographic = true;
          textMeshPro.fontSize = (float) minPointSize;
          textMeshPro.text = minPointSize.ToString() + " pts - Lorem ipsum dolor sit...";
          textMeshPro.color = (Color) new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
          num1 += (float) minPointSize;
        }
      }
    }
  }
}
