// Decompiled with JetBrains decompiler
// Type: ThermometerData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(fileName = "New Thermometer", menuName = "Thermometer Data", order = 69)]
public class ThermometerData : ScriptableObject
{
  [SerializeField]
  private string thermometerId;
  [SerializeField]
  private Color thermometerColor;
  [SerializeField]
  private float goldBonus;
  [SerializeField]
  private float expBonus;
  [SerializeField]
  private int cardBonus;
  [SerializeField]
  private bool cardReward;
  [SerializeField]
  private int expertiseBonus;
  [SerializeField]
  private int uiGold;
  [SerializeField]
  private int uiExp;
  [SerializeField]
  private int uiCard;

  public string ThermometerId
  {
    get => this.thermometerId;
    set => this.thermometerId = value;
  }

  public float GoldBonus
  {
    get => this.goldBonus;
    set => this.goldBonus = value;
  }

  public float ExpBonus
  {
    get => this.expBonus;
    set => this.expBonus = value;
  }

  public int CardBonus
  {
    get => this.cardBonus;
    set => this.cardBonus = value;
  }

  public int ExpertiseBonus
  {
    get => this.expertiseBonus;
    set => this.expertiseBonus = value;
  }

  public Color ThermometerColor
  {
    get => this.thermometerColor;
    set => this.thermometerColor = value;
  }

  public bool CardReward
  {
    get => this.cardReward;
    set => this.cardReward = value;
  }

  public int UiGold
  {
    get => this.uiGold;
    set => this.uiGold = value;
  }

  public int UiExp
  {
    get => this.uiExp;
    set => this.uiExp = value;
  }

  public int UiCard
  {
    get => this.uiCard;
    set => this.uiCard = value;
  }
}
