// Decompiled with JetBrains decompiler
// Type: Thunder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class Thunder : MonoBehaviour
{
  private int count;
  private int countRay = -1;
  private Animator anim;

  private void Awake()
  {
    this.anim = this.GetComponent<Animator>();
    this.anim.enabled = false;
  }

  private void Start() => this.GenerateCountRay();

  private void Update()
  {
    if (Time.frameCount % 48 != 0)
      return;
    ++this.count;
    if (this.count == this.countRay)
    {
      this.anim.enabled = true;
      this.anim.Play(0);
    }
    if (this.count <= this.countRay + 5)
      return;
    this.anim.enabled = false;
    this.count = 0;
  }

  private void GenerateCountRay() => this.countRay = Random.Range(15, 80);
}
