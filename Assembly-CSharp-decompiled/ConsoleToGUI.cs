// Decompiled with JetBrains decompiler
// Type: ConsoleToGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class ConsoleToGUI : MonoBehaviour
{
  public GameObject consoleCanvas;
  public TextMeshProUGUI consoleText;
  private string myLog = "*begin log";
  private bool doShow = true;
  private int kChars = 1200;

  private void OnEnable() => Application.logMessageReceived += new Application.LogCallback(this.Log);

  private void OnDisable() => Application.logMessageReceived -= new Application.LogCallback(this.Log);

  private void Start() => this.doShow = false;

  public void ConsoleShow()
  {
    this.doShow = !this.doShow;
    if (this.doShow)
    {
      this.consoleCanvas.gameObject.SetActive(true);
      this.SetText();
    }
    else
      this.consoleCanvas.gameObject.SetActive(false);
  }

  private void SetText() => this.consoleText.text = this.myLog;

  public void Log(string logString, string stackTrace, UnityEngine.LogType type)
  {
    this.myLog = this.myLog + "\n" + logString;
    if (this.myLog.Length > this.kChars)
      this.myLog = this.myLog.Substring(this.myLog.Length - this.kChars);
    if (!this.consoleCanvas.gameObject.activeSelf)
      return;
    this.SetText();
  }
}
