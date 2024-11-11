// Decompiled with JetBrains decompiler
// Type: Demo2AutoScroll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class Demo2AutoScroll : MonoBehaviour
{
  private Transform[] children;
  public float totalTime;
  public GameObject sceneDescription;

  private void Start()
  {
    this.sceneDescription.SetActive(false);
    Camera.main.fieldOfView = 60f;
    this.children = this.GetComponentsInChildren<Transform>();
    for (int index = 0; index < this.children.Length; ++index)
    {
      if ((Object) this.children[index].gameObject != (Object) this.gameObject)
      {
        this.children[index].gameObject.SetActive(false);
        this.children[index].localPosition = Vector3.zero;
      }
    }
    this.totalTime /= (float) this.children.Length;
    this.StartCoroutine(this.ScrollElements());
  }

  private IEnumerator ScrollElements()
  {
    Demo2AutoScroll demo2AutoScroll = this;
    int i = 0;
    while (true)
    {
      for (; !((Object) demo2AutoScroll.children[i].gameObject == (Object) demo2AutoScroll.gameObject); i = (i + 1) % demo2AutoScroll.children.Length)
      {
        demo2AutoScroll.children[i].gameObject.SetActive(true);
        yield return (object) new WaitForSeconds(demo2AutoScroll.totalTime);
        demo2AutoScroll.children[i].gameObject.SetActive(false);
      }
      i = (i + 1) % demo2AutoScroll.children.Length;
    }
  }
}
