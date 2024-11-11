// Decompiled with JetBrains decompiler
// Type: WhiteSquare
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class WhiteSquare : MonoBehaviour
{
  public bool isControllerAttached;
  public GameObject square;

  private void Update()
  {
    if (this.isControllerAttached)
    {
      if ((double) Input.GetAxis("Horizontal") != 0.0 || (double) Input.GetAxis("Vertical") != 0.0)
      {
        if (this.square.activeSelf)
          return;
        this.square.SetActive(true);
      }
      else
      {
        if (!this.square.activeSelf)
          return;
        this.square.SetActive(false);
      }
    }
    else if ((double) Input.GetAxis("Mouse X") != 0.0 || (double) Input.GetAxis("Mouse Y") != 0.0)
    {
      if (!this.square.activeSelf)
        return;
      this.square.SetActive(false);
    }
    else
    {
      if (this.square.activeSelf)
        return;
      this.square.SetActive(true);
    }
  }
}
