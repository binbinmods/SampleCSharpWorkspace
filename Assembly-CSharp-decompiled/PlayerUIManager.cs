// Decompiled with JetBrains decompiler
// Type: PlayerUIManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
  public Transform elements;
  public Transform goldIcon;
  public Transform dustIcon;
  public Transform supplyIcon;
  public TMP_Text goldText;
  public TMP_Text dustText;
  public TMP_Text supplyText;
  public Transform goldTextAnim;
  public Transform dustTextAnim;
  public Transform supplyTextAnim;
  public Transform giveGold;
  private TMP_Text goldTextAnimText;
  private TMP_Text dustTextAnimText;
  private TMP_Text supplyTextAnimText;
  public TMP_Text bagQuantityText;
  public Transform bagQuantity;
  private int itemsNum;
  public Transform eventItems;
  public Transform bagItems;
  public GameObject eventItemPrefab;
  private bool eventItemsBagOpened = true;
  private List<string> itemsOnBag = new List<string>();
  private Coroutine goldCo;
  private Coroutine dustCo;
  private Coroutine supplyCo;

  public static PlayerUIManager Instance { get; private set; }

  private void Awake()
  {
    if ((Object) PlayerUIManager.Instance == (Object) null)
      PlayerUIManager.Instance = this;
    else if ((Object) PlayerUIManager.Instance != (Object) this)
      Object.Destroy((Object) this.gameObject);
    Object.DontDestroyOnLoad((Object) this.gameObject);
    this.goldTextAnimText = this.goldTextAnim.GetComponent<TMP_Text>();
    this.dustTextAnimText = this.dustTextAnim.GetComponent<TMP_Text>();
    this.supplyTextAnimText = this.supplyTextAnim.GetComponent<TMP_Text>();
  }

  public void Resize()
  {
    this.transform.localScale = Globals.Instance.scaleV;
    this.transform.position = new Vector3((float) (-(double) Globals.Instance.sizeW * 0.5 + 1.5 * (double) Globals.Instance.scale), (float) ((double) Globals.Instance.sizeH * 0.5 - 0.34999999403953552 * (double) Globals.Instance.scale), this.transform.position.z);
  }

  public bool IsActive() => this.elements.gameObject.activeSelf;

  public void Show()
  {
    if (!this.elements.gameObject.activeSelf)
      this.elements.gameObject.SetActive(true);
    string sceneName = SceneStatic.GetSceneName();
    if (sceneName == "Rewards" || sceneName == "Loot")
    {
      this.ShowItems(false);
    }
    else
    {
      this.ShowItems(true);
      this.SetItems();
      this.ShowBagItems();
      this.SetSupply();
    }
    if (GameManager.Instance.IsMultiplayer() && (sceneName == "Map" || sceneName == "Town"))
    {
      if (!this.giveGold.gameObject.activeSelf)
        this.giveGold.gameObject.SetActive(true);
    }
    else if (this.giveGold.gameObject.activeSelf)
      this.giveGold.gameObject.SetActive(false);
    this.SetGold();
    this.SetDust();
  }

  public void Hide()
  {
    if (!this.elements.gameObject.activeSelf)
      return;
    this.elements.gameObject.SetActive(false);
  }

  public void ShowItems(bool state)
  {
    if (this.eventItems.gameObject.activeSelf == state)
      return;
    this.eventItems.gameObject.SetActive(state);
  }

  public void BagToggle()
  {
    this.eventItemsBagOpened = !this.eventItemsBagOpened;
    this.ShowBagItems();
  }

  private void ShowBagItems()
  {
    if (this.bagItems.gameObject.activeSelf != this.eventItemsBagOpened)
      this.bagItems.gameObject.SetActive(this.eventItemsBagOpened);
    if (this.itemsNum > 0)
      this.bagQuantity.gameObject.SetActive(!this.eventItemsBagOpened);
    else
      this.bagQuantity.gameObject.SetActive(true);
    if (!this.eventItemsBagOpened || this.itemsNum <= 0 || this.bagItems.childCount != 0)
      return;
    this.SetItems();
  }

  public void RemoveItems()
  {
    foreach (Component bagItem in this.bagItems)
      Object.Destroy((Object) bagItem.gameObject);
  }

  public void ClearBag()
  {
    this.RemoveItems();
    this.itemsOnBag.Clear();
    this.eventItemsBagOpened = true;
    this.itemsNum = 0;
  }

  public void SetItems()
  {
    List<string> playerRequeriments = AtOManager.Instance.GetPlayerRequeriments();
    if (playerRequeriments == null)
    {
      this.bagQuantityText.text = "0";
      this.itemsNum = 0;
      this.RemoveItems();
    }
    else
    {
      if (this.eventItemsBagOpened)
        this.RemoveItems();
      int num = 0;
      for (int index = 0; index < playerRequeriments.Count; ++index)
      {
        EventRequirementData requirementData = Globals.Instance.GetRequirementData(playerRequeriments[index]);
        if (requirementData.ItemTrack)
        {
          if (this.eventItemsBagOpened)
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.eventItemPrefab, Vector3.zero, Quaternion.identity, this.bagItems);
            gameObject.transform.GetChild(0).GetComponent<EventItemTrack>().SetItemTrack(requirementData);
            if (!this.itemsOnBag.Contains(requirementData.RequirementId))
            {
              this.itemsOnBag.Add(requirementData.RequirementId);
              gameObject.transform.GetChild(0).transform.GetComponent<Animator>().enabled = true;
              gameObject.transform.localPosition = new Vector3(0.8f * (float) num, 0.0f, 0.0f);
            }
            else
              gameObject.transform.localPosition = new Vector3(0.8f * (float) num, -0.416f, 0.0f);
          }
          ++num;
        }
      }
      this.itemsNum = num;
      this.bagQuantityText.text = this.itemsNum.ToString();
    }
  }

  public Vector3 GoldIconPosition() => this.goldIcon.position;

  public Vector3 DustIconPosition() => this.dustIcon.position;

  public void SetGold(bool animation = false)
  {
    this.goldTextAnimText.text = "";
    if (!animation)
    {
      this.goldText.text = AtOManager.Instance.GetPlayerGold().ToString();
    }
    else
    {
      if (this.goldCo != null)
        this.StopCoroutine(this.goldCo);
      if (this.gameObject.activeSelf)
        this.goldCo = this.StartCoroutine(this.QuantityAnimation("gold"));
      else
        this.goldText.text = AtOManager.Instance.GetPlayerGold().ToString();
    }
  }

  public void SetDust(bool animation = false)
  {
    this.dustTextAnimText.text = "";
    if (!animation)
    {
      this.dustText.text = AtOManager.Instance.GetPlayerDust().ToString();
    }
    else
    {
      if (this.dustCo != null)
        this.StopCoroutine(this.dustCo);
      if (this.gameObject.activeSelf)
        this.dustCo = this.StartCoroutine(this.QuantityAnimation("dust"));
      else
        this.dustText.text = AtOManager.Instance.GetPlayerDust().ToString();
    }
  }

  public void SetSupply(bool animation = false)
  {
    this.supplyTextAnimText.text = "";
    if (!animation)
    {
      this.supplyText.text = PlayerManager.Instance.GetPlayerSupplyActual().ToString();
    }
    else
    {
      if (this.supplyCo != null)
        this.StopCoroutine(this.supplyCo);
      if (this.gameObject.activeSelf)
        this.supplyCo = this.StartCoroutine(this.QuantityAnimation("supply"));
      else
        this.supplyText.text = PlayerManager.Instance.GetPlayerSupplyActual().ToString();
    }
  }

  private IEnumerator QuantityAnimation(string type)
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    int value = 0;
    int end = 0;
    switch (type)
    {
      case "gold":
        value = !(this.goldText.text == "") ? int.Parse(this.goldText.text) : 0;
        end = AtOManager.Instance.GetPlayerGold();
        break;
      case "dust":
        value = !(this.dustText.text == "") ? int.Parse(this.dustText.text) : 0;
        end = AtOManager.Instance.GetPlayerDust();
        break;
      default:
        value = !(this.supplyText.text == "") ? int.Parse(this.supplyText.text) : 0;
        end = PlayerManager.Instance.GetPlayerSupplyActual();
        break;
    }
    int increment = 0;
    if (end > value)
      increment = 1;
    else if (end >= value)
      yield break;
    else
      increment = -1;
    switch (type)
    {
      case "gold":
        GameManager.Instance.PlayLibraryAudio("ui_coins");
        break;
      case "dust":
        GameManager.Instance.PlayLibraryAudio("ui_gems");
        break;
      case "supply":
        GameManager.Instance.PlayLibraryAudio("ui_gems");
        break;
    }
    int difference = end - value;
    while (value != end)
    {
      value += Mathf.Abs(value - end) <= 1000 ? (Mathf.Abs(value - end) <= 100 ? (Mathf.Abs(value - end) <= 50 ? (Mathf.Abs(value - end) <= 10 ? increment : increment * 5) : increment * 22) : increment * 53) : increment * 104;
      switch (type)
      {
        case "gold":
          this.goldText.text = value.ToString();
          break;
        case "dust":
          this.dustText.text = value.ToString();
          break;
        default:
          this.supplyText.text = value.ToString();
          break;
      }
      yield return (object) null;
    }
    switch (type)
    {
      case "gold":
        this.ShowAnim(0, difference);
        break;
      case "dust":
        this.ShowAnim(1, difference);
        break;
      default:
        this.ShowAnim(2, difference);
        break;
    }
    yield return (object) null;
  }

  private void ShowAnim(int type, int quantity)
  {
    if (quantity == 0)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    if (quantity > 0)
      stringBuilder.Append("+");
    stringBuilder.Append(quantity);
    switch (type)
    {
      case 0:
        this.goldTextAnimText.text = stringBuilder.ToString();
        this.goldTextAnim.gameObject.SetActive(false);
        this.goldTextAnim.gameObject.SetActive(true);
        break;
      case 1:
        this.dustTextAnimText.text = stringBuilder.ToString();
        this.dustTextAnim.gameObject.SetActive(false);
        this.dustTextAnim.gameObject.SetActive(true);
        break;
      default:
        this.supplyTextAnimText.text = stringBuilder.ToString();
        this.supplyTextAnim.gameObject.SetActive(false);
        this.supplyTextAnim.gameObject.SetActive(true);
        break;
    }
    this.StartCoroutine(this.HideAnim(type));
  }

  private IEnumerator HideAnim(int type)
  {
    yield return (object) Globals.Instance.WaitForSeconds(1.5f);
    switch (type)
    {
      case 0:
        this.goldTextAnim.gameObject.SetActive(false);
        break;
      case 1:
        this.dustTextAnim.gameObject.SetActive(false);
        break;
      default:
        this.supplyTextAnim.gameObject.SetActive(false);
        break;
    }
    yield return (object) null;
  }
}
