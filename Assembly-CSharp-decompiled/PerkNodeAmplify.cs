// Decompiled with JetBrains decompiler
// Type: PerkNodeAmplify
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class PerkNodeAmplify : MonoBehaviour
{
  public Transform bg2;
  public Transform bg3;
  public Transform bg4;
  public Transform amplifyNodes;
  private RaycastHit[] hits;
  private int hideCounter;

  private void Update()
  {
    if (Time.frameCount % 4 != 0 || !this.gameObject.activeSelf)
      return;
    if (this.hideCounter < 5)
    {
      Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(new Vector2(worldPoint.x, worldPoint.y), Vector2.zero, 0.0f))
      {
        if (raycastHit2D.transform.gameObject.name == this.gameObject.name)
        {
          this.hideCounter = 0;
          return;
        }
      }
      ++this.hideCounter;
    }
    else
      this.Hide();
  }

  public void Hide()
  {
    this.gameObject.SetActive(false);
    this.hideCounter = 0;
  }

  public void Show() => this.gameObject.SetActive(true);

  public void SetForNodes(int _numNodes)
  {
    PolygonCollider2D component = this.GetComponent<PolygonCollider2D>();
    if (_numNodes == 2)
    {
      this.bg2.gameObject.SetActive(true);
      this.bg3.gameObject.SetActive(false);
      this.bg4.gameObject.SetActive(false);
      this.amplifyNodes.localPosition = new Vector3(-0.7f, this.amplifyNodes.localPosition.y, this.amplifyNodes.localPosition.z);
      component.points = this.bg2.GetComponent<PolygonCollider2D>().points;
    }
    else if (_numNodes == 3)
    {
      this.bg2.gameObject.SetActive(false);
      this.bg3.gameObject.SetActive(true);
      this.bg4.gameObject.SetActive(false);
      this.amplifyNodes.localPosition = new Vector3(-1.4f, this.amplifyNodes.localPosition.y, this.amplifyNodes.localPosition.z);
      component.points = this.bg3.GetComponent<PolygonCollider2D>().points;
    }
    else
    {
      this.bg2.gameObject.SetActive(false);
      this.bg3.gameObject.SetActive(false);
      this.bg4.gameObject.SetActive(true);
      this.amplifyNodes.localPosition = new Vector3(-2.1f, this.amplifyNodes.localPosition.y, this.amplifyNodes.localPosition.z);
      component.points = this.bg4.GetComponent<PolygonCollider2D>().points;
    }
  }
}
