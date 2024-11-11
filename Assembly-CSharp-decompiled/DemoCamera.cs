// Decompiled with JetBrains decompiler
// Type: DemoCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

public class DemoCamera : MonoBehaviour
{
  [SerializeField]
  private Transform targetedItem;
  [SerializeField]
  private All1ShaderDemoController demoController;
  [SerializeField]
  private float speed;
  private Vector3 offset;
  private Vector3 target;
  private bool canUpdate;

  private void Awake()
  {
    this.offset = this.transform.position - this.targetedItem.position;
    this.StartCoroutine(this.SetCamAfterStart());
  }

  private void Update()
  {
    if (!this.canUpdate)
      return;
    this.target.y = (float) this.demoController.GetCurrExpositor() * this.demoController.expositorDistance;
    this.transform.position = Vector3.Lerp(this.transform.position, this.target, this.speed * Time.deltaTime);
  }

  private IEnumerator SetCamAfterStart()
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    DemoCamera demoCamera = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      demoCamera.transform.position = demoCamera.targetedItem.position + demoCamera.offset;
      demoCamera.target = demoCamera.transform.position;
      demoCamera.canUpdate = true;
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) null;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }
}
