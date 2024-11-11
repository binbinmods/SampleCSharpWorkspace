// Decompiled with JetBrains decompiler
// Type: ThermometerTierData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[CreateAssetMenu(fileName = "New ThermometerTier", menuName = "ThermometerTier Data", order = 69)]
public class ThermometerTierData : ScriptableObject
{
  [SerializeField]
  private string thermometerTierId;
  [SerializeField]
  private global::RoundThermometer[] roundThermometer;

  public string ThermometerTierId
  {
    get => this.thermometerTierId;
    set => this.thermometerTierId = value;
  }

  public global::RoundThermometer[] RoundThermometer
  {
    get => this.roundThermometer;
    set => this.roundThermometer = value;
  }
}
