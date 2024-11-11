// Decompiled with JetBrains decompiler
// Type: CinematicManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CinematicManager : MonoBehaviour
{
  private PhotonView photonView;
  private CinematicData cinematicData;
  private GameObject cinematicGO;
  private Animator cinematicGOAnim;
  private bool playCinematic = true;
  private int totalPlayersReady;
  public TMP_Text textBottom;
  public TMP_Text textMiddle;
  public TMP_Text UIPlayers;
  public Transform buttonSkip;
  private bool skipable;
  private bool finishCinematic;
  private Coroutine hideTextCo;

  public static CinematicManager Instance { get; private set; }

  private void Awake()
  {
    if ((Object) CinematicManager.Instance == (Object) null)
      CinematicManager.Instance = this;
    else if ((Object) CinematicManager.Instance != (Object) this)
      Object.Destroy((Object) this.gameObject);
    if ((Object) GameManager.Instance == (Object) null)
    {
      SceneStatic.LoadByName("Cinematic");
    }
    else
    {
      this.photonView = PhotonView.Get((Component) this);
      GameManager.Instance.SetCamera();
      NetworkManager.Instance.StartStopQueue(true);
      this.DoCinematic();
    }
  }

  private void Update()
  {
    if (!this.playCinematic || !((Object) this.cinematicGOAnim != (Object) null))
      return;
    double normalizedTime = (double) this.cinematicGOAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    float num = (float) normalizedTime - Mathf.Floor((float) normalizedTime);
    if (!this.finishCinematic && (double) num <= 0.949999988079071)
      return;
    this.EndCinematic();
  }

  public void DoText(string _cinematic, int _index, int _position)
  {
    if (this.hideTextCo != null)
    {
      this.StopCoroutine(this.hideTextCo);
      this.textBottom.gameObject.SetActive(false);
      this.textMiddle.gameObject.SetActive(false);
    }
    switch (_cinematic)
    {
      case "intro":
        if (_position == 0)
        {
          this.textBottom.text = Texts.Instance.GetText("cinematicIntro" + _index.ToString());
          break;
        }
        if (_position == 1)
        {
          this.textMiddle.text = Texts.Instance.GetText("cinematicIntro" + _index.ToString());
          break;
        }
        break;
      case "outro":
        if (_position == 0)
        {
          this.textBottom.text = Texts.Instance.GetText("cinematicOutro" + _index.ToString());
          break;
        }
        if (_position == 1)
        {
          this.textMiddle.text = Texts.Instance.GetText("cinematicOutro" + _index.ToString());
          break;
        }
        break;
    }
    if (_position == 0)
    {
      this.textBottom.color = Color.white;
      this.textBottom.gameObject.SetActive(true);
    }
    else if (_position == 1)
    {
      this.textMiddle.color = Color.white;
      this.textMiddle.gameObject.SetActive(true);
    }
    this.hideTextCo = this.StartCoroutine(this.HideText());
  }

  private IEnumerator HideText()
  {
    yield return (object) Globals.Instance.WaitForSeconds(6f);
    this.textBottom.gameObject.SetActive(false);
    this.textMiddle.gameObject.SetActive(false);
  }

  private void FadeOutBSO()
  {
    if (!((Object) this.cinematicData != (Object) null) || !((Object) this.cinematicData.CinematicBSO != (Object) null))
      return;
    AudioManager.Instance.FadeOutBSO();
  }

  private void DoCinematic()
  {
    this.UIPlayers.text = "";
    this.buttonSkip.gameObject.SetActive(true);
    if (AtOManager.Instance.CinematicId != "")
    {
      this.cinematicData = Globals.Instance.GetCinematicData(AtOManager.Instance.CinematicId);
      if ((Object) this.cinematicData != (Object) null)
      {
        if (this.cinematicData.CinematicEndAdventure && !GameManager.Instance.IsObeliskChallenge())
          AtOManager.Instance.SetAdventureCompleted(true);
        if ((Object) this.cinematicData.CinematicBSO != (Object) null)
          AudioManager.Instance.DoBSO(acBSO: this.cinematicData.CinematicBSO);
      }
    }
    AtOManager.Instance.CinematicId = "";
    if ((Object) this.cinematicData == (Object) null)
    {
      GameManager.Instance.ChangeScene("Map");
    }
    else
    {
      this.cinematicGO = Object.Instantiate<GameObject>(this.cinematicData.CinematicGo, Vector3.zero, Quaternion.identity);
      this.cinematicGOAnim = this.cinematicGO.GetComponent<Animator>();
      GameManager.Instance.SceneLoaded();
      this.cinematicGOAnim.Play(this.cinematicGOAnim.GetCurrentAnimatorStateInfo(0).shortNameHash);
      if ((Object) this.cinematicGOAnim != (Object) null && this.cinematicGOAnim.GetCurrentAnimatorClipInfo(0) != null && (double) this.cinematicGOAnim.GetCurrentAnimatorClipInfo(0)[0].clip.length > 5.0)
      {
        this.skipable = true;
        this.buttonSkip.gameObject.SetActive(true);
      }
      else
      {
        this.finishCinematic = true;
        this.buttonSkip.gameObject.SetActive(false);
      }
      this.ControllerMovement(true);
    }
  }

  private void EndCinematic()
  {
    this.playCinematic = false;
    this.cinematicGOAnim.enabled = false;
    this.FadeOutBSO();
    this.StartCoroutine(this.EndCinematicCo());
  }

  private IEnumerator EndCinematicCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.5f);
    if (this.cinematicData.CinematicEndAdventure && !GameManager.Instance.IsObeliskChallenge())
    {
      AtOManager.Instance.FinishGame();
    }
    else
    {
      AtOManager.Instance.fromEventCombatData = this.cinematicData.CinematicCombat;
      AtOManager.Instance.fromEventEventData = this.cinematicData.CinematicEvent;
      GameManager.Instance.ChangeScene("Map");
    }
  }

  public void SkipCinematic()
  {
    if (!this.skipable || !this.buttonSkip.gameObject.activeSelf)
      return;
    this.buttonSkip.gameObject.SetActive(false);
    if (!GameManager.Instance.IsMultiplayer())
      this.EndCinematic();
    else
      this.photonView.RPC("NET_SkipCinematic", RpcTarget.All);
  }

  [PunRPC]
  private void NET_SkipCinematic()
  {
    ++this.totalPlayersReady;
    this.SetTotalPlayersReady();
  }

  private void SetTotalPlayersReady()
  {
    int numPlayers = NetworkManager.Instance.GetNumPlayers();
    this.UIPlayers.text = NetworkManager.Instance.GetWaitingPlayersString(this.totalPlayersReady, numPlayers);
    if (this.totalPlayersReady < numPlayers)
      return;
    this.EndCinematic();
  }

  public void ControllerMovement(
    bool goingUp = false,
    bool goingRight = false,
    bool goingDown = false,
    bool goingLeft = false,
    bool shoulderLeft = false,
    bool shoulderRight = false)
  {
    if (!(goingUp | goingLeft | goingRight | goingDown))
      return;
    Mouse.current.WarpCursorPosition((Vector2) GameManager.Instance.cameraMain.WorldToScreenPoint(this.buttonSkip.GetChild(0).position));
  }
}
