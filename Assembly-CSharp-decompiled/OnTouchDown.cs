// Decompiled with JetBrains decompiler
// Type: OnTouchDown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class OnTouchDown : MonoBehaviour
{
  private void Update()
  {
    RaycastHit hitInfo = new RaycastHit();
    for (int index = 0; index < Input.touchCount; ++index)
    {
      Touch touch = Input.GetTouch(index);
      if (touch.phase.Equals((object) TouchPhase.Began))
      {
        Camera main = Camera.main;
        touch = Input.GetTouch(index);
        Vector3 position = (Vector3) touch.position;
        if (Physics.Raycast(main.ScreenPointToRay(position), out hitInfo))
          hitInfo.transform.gameObject.SendMessage("OnMouseDown");
      }
      else
      {
        touch = Input.GetTouch(index);
        if (touch.phase.Equals((object) TouchPhase.Ended))
        {
          Camera main = Camera.main;
          touch = Input.GetTouch(index);
          Vector3 position = (Vector3) touch.position;
          if (Physics.Raycast(main.ScreenPointToRay(position), out hitInfo))
            hitInfo.transform.gameObject.SendMessage("OnMouseUp");
        }
        else
        {
          touch = Input.GetTouch(index);
          if (touch.phase.Equals((object) TouchPhase.Moved))
          {
            Camera main = Camera.main;
            touch = Input.GetTouch(index);
            Vector3 position = (Vector3) touch.position;
            if (Physics.Raycast(main.ScreenPointToRay(position), out hitInfo))
              hitInfo.transform.gameObject.SendMessage("OnMouseDrag");
          }
        }
      }
    }
  }
}
