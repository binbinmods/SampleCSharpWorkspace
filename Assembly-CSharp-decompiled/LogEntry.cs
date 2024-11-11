// Decompiled with JetBrains decompiler
// Type: LogEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

public class LogEntry
{
  public string logCardId;
  public Hero logHero;
  public string logHeroName = "";
  public NPC logNPC;
  public string logNPCName = "";
  public Hero logHeroTarget;
  public string logHeroTargetName = "";
  public NPC logNPCTarget;
  public string logNPCTargetName = "";
  public int logRound;
  public string logDateTime = "";
  public bool logFinished;
  public int logAuxInt = -1;
  public string logAuxString = "";
  public Enums.EventActivation logActivation;
  public Dictionary<string, LogResult> logResult = new Dictionary<string, LogResult>();
}
