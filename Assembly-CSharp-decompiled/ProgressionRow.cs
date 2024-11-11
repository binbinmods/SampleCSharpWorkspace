// Decompiled with JetBrains decompiler
// Type: ProgressionRow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Text;
using TMPro;
using UnityEngine;

public class ProgressionRow : MonoBehaviour
{
  public TMP_Text title;
  public TMP_Text description;

  private void Awake() => this.Enable(false);

  public void Enable(bool _state)
  {
    if (_state)
    {
      this.title.color = Functions.HexToColor("#F1D2A9");
      this.description.color = Functions.HexToColor("#F1D2A9");
      this.title.fontStyle = this.description.fontStyle = FontStyles.Bold;
    }
    else
    {
      this.title.color = Functions.HexToColor("#A0A0A0");
      this.description.color = Functions.HexToColor("#A0A0A0");
      this.title.fontStyle = this.description.fontStyle = FontStyles.Normal;
    }
  }

  public void Init(int _rank)
  {
    this.InitRank(_rank);
    this.Enable(false);
  }

  public void InitRank(int _rank)
  {
    this.title.text = Globals.Instance.RankLevel[_rank].ToString();
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("rankDescription");
    stringBuilder.Append(_rank);
    this.description.text = Texts.Instance.GetText(stringBuilder.ToString());
  }
}
