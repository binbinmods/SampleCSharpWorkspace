// Decompiled with JetBrains decompiler
// Type: OpenHyperlinks
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof (TextMeshProUGUI))]
public class OpenHyperlinks : MonoBehaviour
{
  public bool doesColorChangeOnHover = true;
  public Color hoverColor = new Color(0.235294119f, 0.470588237f, 1f);
  public TMP_Text pTextMeshPro;
  private int pCurrentLink = -1;
  private List<Color32[]> pOriginalVertexColors = new List<Color32[]>();
  private string currentInfo = "";

  public bool isLinkHighlighted => this.pCurrentLink != -1;

  private void LateUpdate()
  {
    if (Time.frameCount % 2 == 0)
      return;
    int linkIndex = TMP_TextUtilities.IsIntersectingRectTransform(this.pTextMeshPro.rectTransform, GameManager.Instance.cameraMain.ScreenToWorldPoint(Input.mousePosition), (Camera) null) ? TMP_TextUtilities.FindIntersectingLink(this.pTextMeshPro, GameManager.Instance.cameraMain.ScreenToWorldPoint(Input.mousePosition), (Camera) null) : -1;
    if (this.pCurrentLink != -1)
    {
      if (linkIndex != this.pCurrentLink)
      {
        try
        {
          this.SetLinkToColor(this.pCurrentLink, (Func<int, int, Color32>) ((linkIdx, vertIdx) => this.pOriginalVertexColors[linkIdx][vertIdx]));
        }
        catch (Exception ex)
        {
          if (GameManager.Instance.GetDeveloperMode())
            Debug.LogWarning((object) ("AddCharges exception-> " + ex?.ToString()));
          this.pOriginalVertexColors.Clear();
          this.pCurrentLink = -1;
          this.currentInfo = "";
          return;
        }
        this.pOriginalVertexColors.Clear();
        this.pCurrentLink = -1;
      }
    }
    if (linkIndex != -1 && linkIndex != this.pCurrentLink)
    {
      this.pCurrentLink = linkIndex;
      if (this.doesColorChangeOnHover)
        this.pOriginalVertexColors = this.SetLinkToColor(linkIndex, (Func<int, int, Color32>) ((_linkIdx, _vertIdx) => (Color32) this.hoverColor));
      TMP_LinkInfo tmpLinkInfo = this.pTextMeshPro.textInfo.linkInfo[linkIndex];
      if (!(this.currentInfo != tmpLinkInfo.GetLinkID()))
        return;
      this.currentInfo = tmpLinkInfo.GetLinkID();
      if (!(bool) (UnityEngine.Object) MatchManager.Instance || !((UnityEngine.Object) MatchManager.Instance.console != (UnityEngine.Object) null))
        return;
      MatchManager.Instance.console.ShowCard(this.currentInfo, tmpLinkInfo.GetLinkText());
    }
    else
    {
      if (!(this.currentInfo != ""))
        return;
      this.currentInfo = "";
    }
  }

  private List<Color32[]> SetLinkToColor(int linkIndex, Func<int, int, Color32> colorForLinkAndVert)
  {
    TMP_LinkInfo tmpLinkInfo = this.pTextMeshPro.textInfo.linkInfo[linkIndex];
    List<Color32[]> color = new List<Color32[]>();
    for (int index = 0; index < tmpLinkInfo.linkTextLength; ++index)
    {
      TMP_CharacterInfo tmpCharacterInfo = this.pTextMeshPro.textInfo.characterInfo[tmpLinkInfo.linkTextfirstCharacterIndex + index];
      int materialReferenceIndex = tmpCharacterInfo.materialReferenceIndex;
      int vertexIndex = tmpCharacterInfo.vertexIndex;
      Color32[] colors32 = this.pTextMeshPro.textInfo.meshInfo[materialReferenceIndex].colors32;
      color.Add(((IEnumerable<Color32>) colors32).ToArray<Color32>());
      if (tmpCharacterInfo.isVisible)
      {
        colors32[vertexIndex] = colorForLinkAndVert(index, vertexIndex);
        colors32[vertexIndex + 1] = colorForLinkAndVert(index, vertexIndex + 1);
        colors32[vertexIndex + 2] = colorForLinkAndVert(index, vertexIndex + 2);
        colors32[vertexIndex + 3] = colorForLinkAndVert(index, vertexIndex + 3);
      }
    }
    this.pTextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    return color;
  }
}
