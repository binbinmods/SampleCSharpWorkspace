// Decompiled with JetBrains decompiler
// Type: DemoCircleExpositor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DemoCircleExpositor : MonoBehaviour
{
  [SerializeField]
  private float radius = 40f;
  [SerializeField]
  private float rotateSpeed = 10f;
  private Transform[] items;
  private int count;
  private int currentTarget;
  private float offsetRotation;
  private float iniY;
  private Quaternion dummyRotation;

  private void Start()
  {
    this.dummyRotation = this.transform.rotation;
    this.iniY = this.transform.position.y;
    this.items = new Transform[this.transform.childCount];
    foreach (Transform transform in this.transform)
    {
      this.items[this.count] = transform;
      ++this.count;
    }
    this.offsetRotation = 360f / (float) this.count;
    for (int index = 0; index < this.count; ++index)
    {
      float f = (float) ((double) index * 3.1415927410125732 * 2.0) / (float) this.count;
      Vector3 vector3 = new Vector3(Mathf.Sin(f) * this.radius, this.iniY, -Mathf.Cos(f) * this.radius);
      this.items[index].position = vector3;
    }
  }

  private void Update() => this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.dummyRotation, this.rotateSpeed * Time.deltaTime);

  public void ChangeTarget(int offset)
  {
    this.currentTarget += offset;
    if (this.currentTarget > this.items.Length - 1)
      this.currentTarget = 0;
    else if (this.currentTarget < 0)
      this.currentTarget = this.items.Length - 1;
    this.dummyRotation *= Quaternion.Euler(Vector3.up * (float) offset * this.offsetRotation);
  }
}
