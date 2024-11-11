// Decompiled with JetBrains decompiler
// Type: PlayerStatusManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{
  public GameObject playerStatusPrefab;
  private Dictionary<string, PlayerStatus> playerStatusList = new Dictionary<string, PlayerStatus>();

  public static PlayerStatusManager Instance { get; private set; }

  private void Awake()
  {
    if ((Object) PlayerStatusManager.Instance == (Object) null)
    {
      PlayerStatusManager.Instance = this;
    }
    else
    {
      if (!((Object) PlayerStatusManager.Instance != (Object) this))
        return;
      Object.Destroy((Object) this.gameObject);
    }
  }

  private void Start() => this.Hide();

  private void Hide() => this.gameObject.SetActive(false);

  private void Show() => this.gameObject.SetActive(true);

  public void DrawPlayers()
  {
    this.Show();
    for (int index = 0; index < this.gameObject.transform.childCount; ++index)
      Object.Destroy((Object) this.gameObject.transform.GetChild(index).gameObject);
    string[] strArray = NetworkManager.Instance.PlayerPositionListArray();
    this.playerStatusList.Clear();
    for (int index = 0; index < strArray.Length; ++index)
      this.AddPlayer(strArray[index]);
  }

  public void ShowPlayerStatusReady(bool state = true)
  {
    foreach (KeyValuePair<string, PlayerStatus> playerStatus in this.playerStatusList)
    {
      if (state)
        playerStatus.Value.ShowStatusReady();
      else
        playerStatus.Value.HideStatusReady();
    }
  }

  public void AddPlayer(string nick)
  {
    if (NetworkManager.Instance.GetPlayerListPosition(nick) <= -1 || this.playerStatusList.ContainsKey(nick))
      return;
    PlayerStatus component = Object.Instantiate<GameObject>(this.playerStatusPrefab, Vector3.zero, Quaternion.identity, this.transform).GetComponent<PlayerStatus>();
    component.SetPlayer(nick);
    component.SetOnline();
    this.playerStatusList.Add(nick, component);
  }

  public void SetStatus(string nick, bool status)
  {
    foreach (KeyValuePair<string, PlayerStatus> playerStatus in this.playerStatusList)
    {
      if (playerStatus.Key == nick)
        playerStatus.Value.SetStatus(status);
    }
  }
}
