// Decompiled with JetBrains decompiler
// Type: TreasureRun
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class TreasureRun : MonoBehaviour
{
  public BotonGeneric bGeneric;
  public TMP_Text qText;
  public string treasureId;
  public Transform particles;
  public Transform separator;
  public bool claimed;
  private int communityGold;
  private int communityDust;
  private PlayerRun playerRun;

  private void Awake()
  {
    this.gameObject.SetActive(false);
    this.particles.gameObject.SetActive(false);
  }

  public void SetTreasure(PlayerRun _playerRun, int _index)
  {
    this.particles.gameObject.SetActive(false);
    if (_playerRun.GoldGained < 0)
      _playerRun.GoldGained = 0;
    if (_playerRun.DustGained < 0)
      _playerRun.DustGained = 0;
    this.playerRun = _playerRun;
    this.treasureId = _playerRun.Id;
    this.claimed = false;
    this.bGeneric.gameObject.SetActive(true);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<sprite name=gold> ");
    stringBuilder.Append(_playerRun.GoldGained);
    stringBuilder.Append("<br>");
    stringBuilder.Append("<sprite name=dust> ");
    stringBuilder.Append(_playerRun.DustGained);
    this.qText.text = stringBuilder.ToString();
    this.bGeneric.auxString = _playerRun.Id;
    this.gameObject.SetActive(true);
    this.transform.localPosition = new Vector3(this.transform.localPosition.x, (float) (-0.60000002384185791 - 1.3500000238418579 * (double) _index), this.transform.localPosition.z);
    if (!this.gameObject.activeSelf)
      return;
    this.StartCoroutine(this.ShowTreasure());
  }

  public void SetTreasureCommunity(string _id, int _gold, int _dust, int _index)
  {
    this.treasureId = _id;
    this.communityGold = _gold;
    this.communityDust = _dust;
    this.claimed = false;
    this.bGeneric.gameObject.SetActive(true);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<sprite name=gold>");
    stringBuilder.Append(_gold);
    stringBuilder.Append("<br>");
    stringBuilder.Append("<sprite name=dust>");
    stringBuilder.Append(_dust);
    this.qText.text = stringBuilder.ToString();
    this.bGeneric.auxString = _id;
    this.bGeneric.auxInt = 1000;
    this.gameObject.SetActive(true);
    if (!this.transform.parent.gameObject.activeSelf || !this.transform.parent.transform.parent.gameObject.activeSelf)
      return;
    this.transform.localPosition = new Vector3(this.transform.localPosition.x, -1.2f * (float) _index, this.transform.localPosition.z);
    this.StartCoroutine(this.ShowTreasure());
  }

  private IEnumerator ShowTreasure()
  {
    TreasureRun treasureRun = this;
    Vector3 vectorDestination = new Vector3((float) ((double) Globals.Instance.sizeW * 0.20000000298023224 - 3.880000114440918 * (double) Globals.Instance.multiplierX), treasureRun.transform.localPosition.y, treasureRun.transform.localPosition.z);
    treasureRun.transform.localPosition = new Vector3(Globals.Instance.sizeW * 0.2f, treasureRun.transform.localPosition.y, treasureRun.transform.localPosition.z);
    yield return (object) Globals.Instance.WaitForSeconds(0.2f);
    while ((double) Vector3.Distance(treasureRun.transform.localPosition, vectorDestination) > 0.019999999552965164)
    {
      treasureRun.transform.localPosition = Vector3.Lerp(treasureRun.transform.localPosition, vectorDestination, 0.5f);
      yield return (object) null;
    }
    treasureRun.transform.localPosition = vectorDestination;
  }

  public void ClaimCommunity()
  {
    if (this.claimed)
      return;
    this.claimed = true;
    this.bGeneric.gameObject.SetActive(false);
    this.StartCoroutine(this.ClaimCo(true));
  }

  public void Claim()
  {
    if (this.claimed)
      return;
    this.claimed = true;
    this.bGeneric.gameObject.SetActive(false);
    this.StartCoroutine(this.ClaimCo());
  }

  private IEnumerator ClaimCo(bool isCommunity = false)
  {
    TreasureRun treasureRun = this;
    GameManager.Instance.PlayLibraryAudio("action_openBox");
    yield return (object) Globals.Instance.WaitForSeconds(0.2f);
    int dust = 0;
    int quantity;
    if (!isCommunity)
    {
      quantity = treasureRun.playerRun.GoldGained;
      dust = treasureRun.playerRun.DustGained;
    }
    else
    {
      quantity = treasureRun.communityGold;
      dust = treasureRun.communityDust;
    }
    if (!GameManager.Instance.IsMultiplayer())
    {
      AtOManager.Instance.GivePlayer(0, quantity);
      AtOManager.Instance.GivePlayer(1, dust);
      AtOManager.Instance.SaveGame();
    }
    else
    {
      AtOManager.Instance.AskForGold(NetworkManager.Instance.GetPlayerNick(), quantity);
      yield return (object) Globals.Instance.WaitForSeconds(0.01f);
      AtOManager.Instance.AskForDust(NetworkManager.Instance.GetPlayerNick(), dust);
    }
    SaveManager.SavePlayerData();
    treasureRun.particles.gameObject.SetActive(true);
    GameManager.Instance.PlayLibraryAudio("ui_cardupgrade");
    yield return (object) Globals.Instance.WaitForSeconds(0.2f);
    GameManager.Instance.GenerateParticleTrail(0, treasureRun.transform.position, PlayerUIManager.Instance.GoldIconPosition());
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    GameManager.Instance.GenerateParticleTrail(1, treasureRun.transform.position, PlayerUIManager.Instance.DustIconPosition());
    Vector3 vectorDestination = new Vector3(Globals.Instance.sizeW * 0.3f, treasureRun.transform.localPosition.y, treasureRun.transform.localPosition.z);
    while ((double) Vector3.Distance(treasureRun.transform.localPosition, vectorDestination) > 0.019999999552965164)
    {
      treasureRun.transform.localPosition = Vector3.Lerp(treasureRun.transform.localPosition, vectorDestination, 0.5f);
      yield return (object) null;
    }
    treasureRun.transform.localPosition = vectorDestination;
    TownManager.Instance.MoveTreasuresUp(treasureRun.treasureId, isCommunity);
    TownManager.Instance.LockTreasures(false);
    treasureRun.transform.gameObject.SetActive(false);
  }

  public void Hide() => this.transform.gameObject.SetActive(false);

  public void MoveUp() => this.StartCoroutine(this.MoveUpCo());

  private IEnumerator MoveUpCo()
  {
    TreasureRun treasureRun = this;
    Vector3 vectorDestination = new Vector3(treasureRun.transform.localPosition.x, treasureRun.transform.localPosition.y + 1.4f, treasureRun.transform.localPosition.z);
    while ((double) Vector3.Distance(treasureRun.transform.localPosition, vectorDestination) > 0.0099999997764825821)
    {
      treasureRun.transform.localPosition = Vector3.Lerp(treasureRun.transform.localPosition, vectorDestination, 0.5f);
      yield return (object) null;
    }
    treasureRun.transform.localPosition = vectorDestination;
  }
}
