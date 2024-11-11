// Decompiled with JetBrains decompiler
// Type: ThermometerPiece
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

public class ThermometerPiece : MonoBehaviour
{
  public int piece;
  public int round;
  private ThermometerTierData thermometerTierData;
  private ThermometerData thermometerData;
  private SpriteRenderer spr;
  private Color oriColor;
  private int pieceRound;

  private void Awake() => this.spr = this.GetComponent<SpriteRenderer>();

  private void Start() => this.Init(this.round);

  public void SetThermometerTierData(ThermometerTierData _thermometerTierData) => this.thermometerTierData = _thermometerTierData;

  public void SetThermometerData(ThermometerData _thermometerData) => this.thermometerData = _thermometerData;

  public void Init(int _currentRound)
  {
    if ((Object) this.thermometerTierData != (Object) null)
    {
      this.thermometerData = (ThermometerData) null;
      this.round = _currentRound;
      this.pieceRound = this.round + this.piece - 2;
      if (this.pieceRound > 0)
      {
        for (int index = 0; index < this.thermometerTierData.RoundThermometer.Length; ++index)
        {
          if (this.pieceRound >= this.thermometerTierData.RoundThermometer[index].Round)
          {
            this.thermometerData = this.thermometerTierData.RoundThermometer[index].ThermometerData;
            if (this.pieceRound == this.thermometerTierData.RoundThermometer[index].Round)
              break;
          }
        }
      }
      if ((Object) this.thermometerData == (Object) null)
      {
        this.gameObject.SetActive(false);
      }
      else
      {
        this.gameObject.SetActive(true);
        this.spr.color = this.thermometerData.ThermometerColor;
      }
    }
    else
      this.gameObject.SetActive(false);
  }

  private void OnMouseEnter()
  {
    if (MatchManager.Instance.CardDrag || EventSystem.current.IsPointerOverGameObject())
      return;
    this.oriColor = this.spr.color;
    this.spr.color = new Color(this.oriColor.r - 0.15f, this.oriColor.g - 0.15f, this.oriColor.b - 0.15f, 1f);
    PopupManager.Instance.SetText(Functions.ThermometerTextForPopup(this.thermometerData), true, "followdown", true);
    MatchManager.Instance.AdjustRoundForThermoDisplay(this.piece, this.pieceRound, this.oriColor);
  }

  private void OnMouseExit()
  {
    if (MatchManager.Instance.CardDrag || EventSystem.current.IsPointerOverGameObject())
      return;
    this.spr.color = this.oriColor;
    PopupManager.Instance.ClosePopup();
    MatchManager.Instance.AdjustRoundForThermoDisplay(2, 0, new Color(1f, 1f, 1f, 1f));
  }
}
