// Decompiled with JetBrains decompiler
// Type: RoomList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class RoomList : MonoBehaviour
{
  [SerializeField]
  private string _roomName = "";
  private string _roomPwd = "";
  private string _roomDescription = "";
  private string _roomVersion = "";
  public TMP_Text _Name;
  public TMP_Text _Creator;
  public TMP_Text _GameDescription;
  public TMP_Text _Players;
  public TMP_Text _Version;
  public GameObject lfm;
  private int _ActivePlayers;
  private int _MaxPlayers;
  public Transform _Lock;

  public string RoomName => this._roomName;

  public void SetRoomName(string text) => this._roomName = text;

  public void SetGameDescription(string text) => this._GameDescription.text = text;

  public void SetLfm(string state)
  {
    if (state != "")
      this.lfm.SetActive(true);
    else
      this.lfm.SetActive(false);
  }

  public void SetRoomDescription(string text)
  {
    this._roomDescription = text;
    this._Name.text = text;
  }

  public void SetRoomPlayers(int numPlayers, int maxPlayers)
  {
    this._Players.text = numPlayers.ToString() + "/" + maxPlayers.ToString();
    this._ActivePlayers = numPlayers;
    this._MaxPlayers = maxPlayers;
  }

  public void SetRoomVersion(string text)
  {
    this._roomVersion = text;
    this._Version.text = text;
  }

  public bool IsEmpty() => this._ActivePlayers == 0;

  public bool IsFull() => this._ActivePlayers == this._MaxPlayers;

  private int GetRoomPlayers() => this._ActivePlayers;

  public void SetRoomCreator(string text) => this._Creator.text = text;

  public void SetRoomPassword(string text)
  {
    this._roomPwd = text;
    if (text != "")
      this.SetLock(true);
    else
      this.SetLock(false);
  }

  private void SetLock(bool value)
  {
    if ((Object) this._Lock == (Object) null || (Object) this._Lock.gameObject == (Object) null)
      return;
    if (value)
      this._Lock.gameObject.SetActive(true);
    else
      this._Lock.gameObject.SetActive(false);
  }

  public void JoinRoom() => LobbyManager.Instance.JoinRoom(this._roomName, this._roomPwd);

  public bool Updated { get; set; }
}
