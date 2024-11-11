// Decompiled with JetBrains decompiler
// Type: Floating
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class Floating : MonoBehaviour
{
  private float originalY;
  private float originalX;
  public float floatStrength = 0.03f;
  public bool moveX = true;
  public bool moveY = true;
  private bool locked = true;
  private float random;
  public bool floatingTimeRandom = true;
  private float posX;
  private float posY;
  private Vector3 destination;

  private void Start()
  {
    this.originalY = this.transform.position.y;
    this.originalX = this.transform.position.x;
    if (this.floatingTimeRandom)
      this.random = UnityEngine.Random.Range(0.0f, 25f);
    this.locked = false;
  }

  private void OnEnable()
  {
    this.originalX = this.transform.position.y;
    this.originalY = this.transform.position.y;
  }

  private void Update()
  {
    if (this.locked || !this.moveX && !this.moveY)
      return;
    this.posX = this.transform.localPosition.x;
    this.posY = this.transform.localPosition.y;
    if (this.moveX)
      this.posX += (float) (Math.Sin((double) Time.time + (double) this.random) * (double) this.floatStrength * 0.10000000149011612);
    if (this.moveY)
      this.posY += (float) Math.Sin((double) Time.time + (double) this.random) * this.floatStrength;
    this.destination = new Vector3(this.posX, this.posY, this.transform.localPosition.z);
    this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, this.destination, 0.5f);
  }
}
