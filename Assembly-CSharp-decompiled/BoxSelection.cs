// Decompiled with JetBrains decompiler
// Type: BoxSelection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class BoxSelection : MonoBehaviour
{
  public SpriteRenderer sr;
  private Coroutine coColor;
  public Transform selection;
  public Transform boxBorder;
  public BoxPlayer[] boxPlayer;
  public TMP_Text playerOwner;
  public Transform lockImg;
  public Transform classImg;
  public Transform dice;
  public Transform disabledLayer;
  private int id;
  private string owner = "";
  public Transform playerReadyT;
  public Transform iconReady;
  public Transform iconNotReady;
  public Transform arrowController;
  private bool enabledRandom = true;

  private void Awake() => this.HidePlayers();

  private void Start()
  {
    this.id = int.Parse(this.gameObject.name.Split('_', StringSplitOptions.None)[1]);
    this.SetOwner("");
    this.ShowHideArrowController(false);
    if (GameManager.Instance.IsLoadingGame() || GameManager.Instance.IsWeeklyChallenge())
      this.enabledRandom = false;
    if (this.enabledRandom)
    {
      this.dice.GetChild(0).GetComponent<RandomHeroSelector>().SetId(this.id);
      if (GameManager.Instance.IsMultiplayer())
        this.dice.gameObject.SetActive(false);
      else
        this.dice.gameObject.SetActive(true);
    }
    else
      this.dice.gameObject.SetActive(false);
  }

  private void HidePlayers()
  {
    for (int index = 0; index < 4; ++index)
      this.boxPlayer[index].gameObject.SetActive(false);
  }

  public void ShowHideArrowController(bool _state)
  {
    if (this.arrowController.gameObject.activeSelf == _state)
      return;
    this.arrowController.gameObject.SetActive(_state);
  }

  public void ShowPlayer(int num) => this.boxPlayer[num].gameObject.SetActive(true);

  public void SetOwner(string playerNick)
  {
    this.owner = playerNick;
    this.playerReadyT.gameObject.SetActive(false);
    if (GameManager.Instance.IsLoadingGame() || GameManager.Instance.IsWeeklyChallenge())
      this.enabledRandom = false;
    if (GameManager.Instance.IsMultiplayer())
    {
      if (playerNick != "")
        this.playerReadyT.gameObject.SetActive(true);
      if (playerNick != NetworkManager.Instance.GetPlayerNick())
      {
        this.lockImg.gameObject.SetActive(true);
        this.classImg.gameObject.SetActive(false);
        this.dice.gameObject.SetActive(false);
      }
      else
      {
        this.lockImg.gameObject.SetActive(false);
        this.classImg.gameObject.SetActive(true);
        if (this.enabledRandom)
          this.dice.gameObject.SetActive(true);
        else
          this.dice.gameObject.SetActive(false);
      }
    }
    else
    {
      this.lockImg.gameObject.SetActive(false);
      this.classImg.gameObject.SetActive(true);
      if (this.enabledRandom)
        this.dice.gameObject.SetActive(true);
      else
        this.dice.gameObject.SetActive(false);
    }
    this.playerOwner.text = NetworkManager.Instance.GetPlayerNickReal(playerNick);
    this.ActivePlayerNick(playerNick);
    if (playerNick != "")
    {
      this.playerOwner.color = Functions.HexToColor(NetworkManager.Instance.GetColorFromNick(playerNick));
      this.TransformColor(NetworkManager.Instance.GetColorFromNick(playerNick));
    }
    else
      this.TransformColor("#87552D");
  }

  public void SetReady(bool _state)
  {
    this.iconReady.gameObject.SetActive(_state);
    this.iconNotReady.gameObject.SetActive(!_state);
  }

  public int GetId() => this.id;

  public string GetOwner() => this.owner;

  public void HideSelection() => this.selection.gameObject.SetActive(false);

  public void ShowSelection() => this.selection.gameObject.SetActive(true);

  private void SelectPlayer(int num)
  {
    for (int index = 0; index < 4; ++index)
    {
      if (index != num)
        this.boxPlayer[index].HideBorder();
      else
        this.boxPlayer[index].DrawBorder();
    }
  }

  public void ActivePlayerNick(string playerNick)
  {
    for (int index = 0; index < 4; ++index)
    {
      if (this.boxPlayer[index].playerNick != playerNick || playerNick == "")
        this.boxPlayer[index].Activate(false);
      else
        this.boxPlayer[index].Activate(true);
    }
  }

  public void SetPlayerPosition(int position, string playerName) => this.boxPlayer[position].SetName(playerName);

  public void CheckSkuForHero()
  {
    if (!GameManager.Instance.IsMultiplayer() || !GameManager.Instance.IsLoadingGame() || GameManager.Instance.IsWeeklyChallenge() || !NetworkManager.Instance.IsMaster() || !((UnityEngine.Object) HeroSelectionManager.Instance.GetBoxHeroFromIndex(this.id) != (UnityEngine.Object) null))
      return;
    SubClassData subClassData = Globals.Instance.GetSubClassData(HeroSelectionManager.Instance.GetBoxHeroFromIndex(this.id).GetSubclassName());
    for (int index = 0; index < this.boxPlayer.Length; ++index)
    {
      if ((UnityEngine.Object) subClassData != (UnityEngine.Object) null && subClassData.Sku != "" && !NetworkManager.Instance.PlayerHaveSku(this.boxPlayer[index].playerName.text, subClassData.Sku))
        this.boxPlayer[index].DisableSKU(subClassData.Sku);
    }
  }

  public void TransformColor(string color)
  {
    if (this.coColor != null)
      this.StopCoroutine(this.coColor);
    this.coColor = this.StartCoroutine(this.TransformColorCo(color));
  }

  private IEnumerator TransformColorCo(string color)
  {
    Color targetColor = Functions.HexToColor(color);
    float timeLeft = 0.2f;
    while ((double) timeLeft > 0.0)
    {
      this.sr.color = Color.Lerp(this.sr.color, targetColor, Time.deltaTime / timeLeft);
      timeLeft -= Time.deltaTime;
      yield return (object) null;
    }
  }

  private void OnMouseExit()
  {
    if (!(SceneStatic.GetSceneName() == "HeroSelection") || (UnityEngine.Object) HeroSelectionManager.Instance.charPopupGO != (UnityEngine.Object) null && HeroSelectionManager.Instance.charPopup.IsOpened())
      return;
    HeroSelectionManager.Instance.MouseOverBox((GameObject) null);
    this.boxBorder.gameObject.SetActive(false);
    if (!HeroSelectionManager.Instance.dragging)
      return;
    HeroSelectionManager.Instance.MouseOverBox((GameObject) null);
  }

  private void OnMouseOver()
  {
    if (!(SceneStatic.GetSceneName() == "HeroSelection") || (UnityEngine.Object) HeroSelectionManager.Instance.charPopupGO != (UnityEngine.Object) null && HeroSelectionManager.Instance.charPopup.IsOpened())
      return;
    HeroSelectionManager.Instance.MouseOverBox(this.gameObject);
  }

  private void OnMouseEnter()
  {
    if (!(SceneStatic.GetSceneName() == "HeroSelection") || (UnityEngine.Object) HeroSelectionManager.Instance.charPopupGO != (UnityEngine.Object) null && HeroSelectionManager.Instance.charPopup.IsOpened())
      return;
    if (HeroSelectionManager.Instance.dragging)
    {
      if (HeroSelectionManager.Instance.IsYourBox(this.gameObject.name))
        this.boxBorder.gameObject.SetActive(true);
      GameManager.Instance.PlayLibraryAudio("ui_click");
    }
    else
      GameManager.Instance.PlayLibraryAudio("castnpccardfast");
  }

  public void ShowDisabledLayer(bool state) => this.disabledLayer.gameObject.SetActive(state);
}
