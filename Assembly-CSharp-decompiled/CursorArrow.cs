// Decompiled with JetBrains decompiler
// Type: CursorArrow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorArrow : MonoBehaviour
{
  public Vector3 point1 = Vector3.zero;
  public Vector3 point2 = Vector3.zero;
  public Vector3 point3 = Vector3.zero;
  private bool drawLine;
  public List<Vector3> pointList;
  public LineRenderer lineRenderer;
  private int vertexCount = 30;
  private Gradient colorGreen;
  private Color darkGreen = new Color(0.0f, 1f, 0.0f, 0.5f);
  private Color darkRed = new Color(1f, 0.0f, 0.0f, 0.5f);
  private Color darkGold = new Color(1f, 0.69f, 0.0f, 0.5f);
  private Color standart;
  private Transform auxHitTransform;
  private Vector3 desiredPosition;
  private Vector3 safePosition;
  private float safeDifferenceY;
  private Vector3 oldPosition = Vector3.zero;

  private void Start()
  {
    this.lineRenderer = this.GetComponent<LineRenderer>();
    this.pointList = new List<Vector3>();
    this.standart = this.lineRenderer.startColor;
  }

  public void ClearPoints() => this.pointList.Clear();

  private void Update()
  {
    if (!this.drawLine)
      return;
    this.desiredPosition.x = this.point1.x;
    this.desiredPosition.y = 1f - Mathf.Pow(this.point3.x / 20f, 2f);
    this.desiredPosition.z = 1f;
    if ((double) this.desiredPosition.x == (double) this.oldPosition.x && (double) this.desiredPosition.y == (double) this.oldPosition.y)
      return;
    this.oldPosition = this.desiredPosition;
    this.safePosition = (this.point1 + this.point3) * 0.5f;
    this.safeDifferenceY = this.desiredPosition.y + 1f - this.point1.y;
    this.point2 = Vector3.Lerp(this.safePosition, this.desiredPosition, Mathf.Clamp((float) (((double) Mathf.Abs(this.point3.x - this.point1.x) - 1.0) * 0.25 + ((double) this.point3.y - (double) this.point1.y) / (double) this.safeDifferenceY), 0.0f, 1f));
    for (float t = 0.0f; (double) t < 1.0; t += 1f / (float) this.vertexCount)
      this.pointList.Add(Vector3.Lerp(Vector3.Lerp(this.point1, this.point2, t), Vector3.Lerp(this.point2, this.point3, t), t));
    this.pointList.Add(this.point3);
    this.lineRenderer.positionCount = this.pointList.Count;
    this.lineRenderer.SetPositions(this.pointList.ToArray());
    this.pointList.Clear();
    if (!((Object) MatchManager.Instance != (Object) null))
      return;
    RaycastHit2D raycastHit2D = Physics2D.Raycast((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition with
    {
      z = 10f
    }), Vector2.zero);
    if ((bool) raycastHit2D)
    {
      if (Gamepad.current != null)
      {
        this.auxHitTransform = raycastHit2D.collider.transform;
        MatchManager.Instance.SetTarget(this.auxHitTransform);
      }
      else
      {
        if (!((Object) raycastHit2D.collider.transform != (Object) this.auxHitTransform))
          return;
        this.auxHitTransform = raycastHit2D.collider.transform;
        MatchManager.Instance.SetTarget(this.auxHitTransform);
      }
    }
    else
    {
      if (!((Object) this.auxHitTransform != (Object) null))
        return;
      this.auxHitTransform = (Transform) null;
      MatchManager.Instance.SetTarget((Transform) null);
    }
  }

  public void SetColor(string theColor)
  {
    switch (theColor)
    {
      case "red":
        this.lineRenderer.startColor = new Color(this.darkRed.r, this.darkRed.g, this.darkRed.b, this.darkRed.a + 0.3f);
        this.lineRenderer.endColor = this.darkRed;
        break;
      case "green":
        this.lineRenderer.startColor = new Color(this.darkGreen.r, this.darkGreen.g, this.darkGreen.b, this.darkGreen.a + 0.3f);
        this.lineRenderer.endColor = this.darkGreen;
        break;
      case "gold":
        this.lineRenderer.startColor = new Color(this.darkGold.r, this.darkGold.g, this.darkGold.b, this.darkGold.a + 0.3f);
        this.lineRenderer.endColor = this.darkGold;
        break;
      default:
        this.lineRenderer.startColor = new Color(this.darkGold.r, this.darkGold.g, this.darkGold.b, this.darkGold.a + 0.3f);
        this.lineRenderer.endColor = this.darkGold;
        break;
    }
  }

  public void Rotation(Quaternion rotation) => this.transform.localRotation = rotation;

  public void StartDraw(bool canInstaCast = true)
  {
    if (this.drawLine)
      return;
    this.drawLine = true;
    if (!((Object) MatchManager.Instance != (Object) null))
      return;
    if (canInstaCast)
      this.SetColor("green");
    else
      this.SetColor("");
    MatchManager.Instance.SetGlobalOutlines(true);
  }

  public void StopDraw()
  {
    if (!this.drawLine)
      return;
    this.drawLine = false;
    this.lineRenderer.positionCount = 0;
    if (!((Object) MatchManager.Instance != (Object) null))
      return;
    MatchManager.Instance.SetGlobalOutlines(false);
  }

  public void DrawArrowWithPoints(Vector3 ori, Vector3 dest)
  {
    double num = (double) Mathf.Clamp(Mathf.Abs(ori.x - dest.x) * 0.01f, 4f, 6f);
    this.point1 = new Vector3(ori.x, ori.y, 1f);
    this.point2 = this.point1;
    this.point3 = new Vector3(dest.x, dest.y, 1f);
    this.StartDraw();
  }
}
