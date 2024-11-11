// Decompiled with JetBrains decompiler
// Type: ParticleArrow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class ParticleArrow : MonoBehaviour
{
  public Vector3 point1 = Vector3.zero;
  public Vector3 point2 = Vector3.zero;
  public Vector3 point3 = Vector3.zero;
  public bool drawLine = true;
  private List<Vector3> pointList;
  private int vertexCount = 24;
  public int movementIndex;
  public TrailRenderer trailRenderer;
  public ParticleSystem particleS;
  public CursorArrow cursorArrow;

  private void Start()
  {
    this.trailRenderer = this.GetComponent<TrailRenderer>();
    this.particleS = this.GetComponent<ParticleSystem>();
    this.pointList = new List<Vector3>();
  }

  private void Update()
  {
    if (Time.frameCount % 1 != 0)
      return;
    ++this.movementIndex;
    if (this.movementIndex >= this.vertexCount)
    {
      this.movementIndex = 0;
      this.transform.position = this.cursorArrow.lineRenderer.GetPosition(0);
    }
    else
    {
      this.transform.position = this.cursorArrow.lineRenderer.GetPosition(this.movementIndex);
      if (this.movementIndex > this.vertexCount - 2)
      {
        this.particleS.Stop();
      }
      else
      {
        if (this.movementIndex != 4)
          return;
        this.particleS.Play();
      }
    }
  }
}
