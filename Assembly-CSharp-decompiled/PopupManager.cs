// Decompiled with JetBrains decompiler
// Type: PopupManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
  public GameObject popupPrefab;
  private Image imageBg;
  private bool popupActive;
  private bool followMouse;
  private bool fastPopup;
  private Coroutine coroutine;
  private GameObject GO_Popup;
  private Transform canvasContainer;
  private CanvasScaler canvasContainerScaler;
  private RectTransform canvasRect;
  private Transform popupContainer;
  private RectTransform popupRect;
  private GameObject pop;
  private GameObject popTrait;
  private GameObject popTown;
  private GameObject popUnlocked;
  private GameObject popPerk;
  private TMP_Text popText;
  private TMP_Text popTraitText;
  private TMP_Text popTownText;
  private TMP_Text popPerkText;
  private List<string> initialPopupGOs;
  private string lastPop = "";
  private Transform lastTransform;
  private string popupText = "<line-height=24><size=21><color=#{2}>{0}</color></size>{3}\n</line-height>{1}";
  private Dictionary<string, GameObject> KeyNotesGO;
  private List<string> KeyNotesActive;
  private Transform theTF;
  private Vector3 destinationPosition;
  private Vector3 adjustPosition;
  private Vector3 theTFposition;
  private Vector3 absolutePosition;
  private bool absolutePositionStablished;
  private Vector2 followSizeDelta;
  private string position = "";
  private Color colorBad = new Color(0.49f, 0.0f, 0.04f, 1f);
  private Color colorGood = new Color(0.01f, 0.22f, 0.52f, 1f);
  private Color colorPlain = new Color(0.25f, 0.25f, 0.25f, 1f);
  private Color colorCardtype = new Color(0.35f, 0.16f, 0.0f, 1f);
  private Color colorVanish = new Color(0.36f, 0.0f, 0.47f, 1f);
  private Color colorInnate = new Color(0.0f, 0.44f, 0.14f, 1f);
  private Color colorTrait = new Color(0.33f, 0.3f, 0.23f, 1f);
  private Color colorUnlocked = new Color(0.02f, 0.65f, 0.0f, 1f);
  private Color colorTown = new Color(0.29f, 0.34f, 0.47f, 1f);
  private int adjustCardX = 60;

  public static PopupManager Instance { get; private set; }

  private void Awake()
  {
    if ((UnityEngine.Object) PopupManager.Instance == (UnityEngine.Object) null)
      PopupManager.Instance = this;
    else if ((UnityEngine.Object) PopupManager.Instance != (UnityEngine.Object) this)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    this.KeyNotesGO = new Dictionary<string, GameObject>();
    this.KeyNotesActive = new List<string>();
  }

  private void Start()
  {
    this.GO_Popup = UnityEngine.Object.Instantiate<GameObject>(this.popupPrefab, Vector3.zero, Quaternion.identity);
    this.canvasContainer = this.GO_Popup.transform.GetChild(0);
    this.canvasContainerScaler = this.canvasContainer.GetComponent<CanvasScaler>();
    this.canvasRect = this.canvasContainer.GetComponent<RectTransform>();
    this.popupContainer = this.canvasContainer.GetChild(0);
    this.popupRect = this.popupContainer.GetComponent<RectTransform>();
    this.popUnlocked = this.popupContainer.GetChild(0).gameObject;
    this.popUnlocked.gameObject.SetActive(false);
    this.pop = this.popupContainer.GetChild(1).gameObject;
    this.popText = this.pop.transform.Find("Background/Text").GetComponent<TMP_Text>();
    this.imageBg = this.pop.transform.GetChild(0).GetComponent<Image>();
    this.imageBg.color = this.colorPlain;
    this.pop.SetActive(false);
    this.GO_Popup.SetActive(false);
    this.initialPopupGOs = new List<string>();
    foreach (Component component in this.popupContainer)
      this.initialPopupGOs.Add(component.gameObject.name);
    this.CreateKeyNotes();
    this.Resize();
  }

  public void Resize()
  {
    if (!((UnityEngine.Object) this.canvasContainerScaler != (UnityEngine.Object) null))
      return;
    if ((double) Globals.Instance.scale < 1.0)
      this.canvasContainerScaler.matchWidthOrHeight = 1f;
    else
      this.canvasContainerScaler.matchWidthOrHeight = 0.0f;
  }

  public void StablishPopupPositionSize(Vector3 position, Vector3 scale)
  {
    this.absolutePosition = position;
    this.popupContainer.transform.localScale = scale;
    this.absolutePositionStablished = true;
    this.popupActive = false;
  }

  private void Update()
  {
    if (!this.popupActive)
      return;
    if (this.followMouse)
    {
      if ((UnityEngine.Object) this.theTF != (UnityEngine.Object) null)
      {
        this.adjustPosition = !(this.position == "followright") ? new Vector3(-this.followSizeDelta.x - (float) this.adjustCardX, this.theTF.GetComponent<BoxCollider2D>().size.y, 0.0f) : new Vector3(this.followSizeDelta.x + (float) this.adjustCardX, this.theTF.GetComponent<BoxCollider2D>().size.y, 0.0f);
        this.destinationPosition = new Vector3((float) ((double) this.theTF.position.x * 100.0 + (double) this.adjustPosition.x * 0.89999997615814209), (float) ((double) this.theTF.position.y * 100.0 + (double) this.adjustPosition.y * 1.0 - (double) this.followSizeDelta.y * 5.0 * 0.10000000149011612), 1f);
        this.destinationPosition = this.RecalcPositionLimits(this.destinationPosition);
        if ((double) Vector3.Distance(this.popupContainer.localPosition, this.destinationPosition) <= 1.0)
          return;
        this.popupContainer.localPosition = Vector3.Lerp(this.popupContainer.localPosition, this.destinationPosition, 8f * Time.deltaTime);
      }
      else
      {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.destinationPosition = !(this.position == "followdown") ? (!(this.position == "followdown2") ? new Vector3(worldPoint.x * 100f, (float) (((double) worldPoint.y + 0.20000000298023224) * 100.0), 1f) : new Vector3((float) (((double) worldPoint.x - 3.0) * 100.0), (float) (((double) worldPoint.y - 5.4000000953674316 - (double) this.followSizeDelta.y * (1.0 / 1000.0)) * 100.0), 1f)) : new Vector3(worldPoint.x * 100f, (float) (((double) worldPoint.y - 1.5 - (double) this.followSizeDelta.y * (1.0 / 1000.0)) * 100.0), 1f);
        float num = this.followSizeDelta.x * 0.6f;
        if ((double) this.destinationPosition.x * 0.0099999997764825821 - (double) num * 0.0099999997764825821 < (double) Globals.Instance.sizeW * -0.47999998927116394)
          this.destinationPosition = new Vector3((float) ((double) Globals.Instance.sizeW * -0.47999998927116394 * 100.0) + num, this.destinationPosition.y, this.destinationPosition.z);
        else if ((double) this.destinationPosition.x * 0.0099999997764825821 + (double) num * 0.0099999997764825821 > (double) Globals.Instance.sizeW * 0.47999998927116394)
          this.destinationPosition = new Vector3((float) ((double) Globals.Instance.sizeW * 0.47999998927116394 * 100.0) - num, this.destinationPosition.y, this.destinationPosition.z);
        if ((double) this.destinationPosition.y * 0.0099999997764825821 + (double) this.followSizeDelta.y * 0.0099999997764825821 > (double) Globals.Instance.sizeH * 0.44999998807907104)
          this.destinationPosition = new Vector3(this.destinationPosition.x, (float) ((double) Globals.Instance.sizeH * 0.44999998807907104 * 100.0) - this.followSizeDelta.y, this.destinationPosition.z);
        if ((double) Vector3.Distance(this.popupContainer.localPosition, this.destinationPosition) <= 1.0)
          return;
        this.popupContainer.localPosition = Vector3.Lerp(this.popupContainer.localPosition, this.destinationPosition, 8f * Time.deltaTime);
      }
    }
    else
    {
      if ((UnityEngine.Object) this.theTF != (UnityEngine.Object) null)
      {
        Vector3 zero = Vector3.zero;
        if (this.theTFposition != Vector3.zero)
        {
          Vector3 vector3 = this.theTFposition - this.theTF.localPosition;
          this.theTFposition = this.theTF.localPosition;
          this.destinationPosition -= new Vector3(vector3.x * 10f, vector3.y * 10f, 0.0f);
        }
      }
      float num = (float) ((double) this.followSizeDelta.x * 0.5 + 50.0);
      if ((double) this.destinationPosition.x * 0.0099999997764825821 - (double) num * 0.0099999997764825821 < (double) Globals.Instance.sizeW * -0.5)
        this.destinationPosition = new Vector3((float) ((double) Globals.Instance.sizeW * -0.48500001430511475 * 100.0) + num, this.destinationPosition.y, this.destinationPosition.z);
      else if ((double) this.destinationPosition.x * 0.0099999997764825821 + (double) num * 0.0099999997764825821 > (double) Globals.Instance.sizeW * 0.5)
        this.destinationPosition = new Vector3((float) ((double) Globals.Instance.sizeW * 0.48500001430511475 * 100.0) - num, this.destinationPosition.y, this.destinationPosition.z);
      else if ((double) this.destinationPosition.y * 0.0099999997764825821 + (double) this.followSizeDelta.y * 0.0099999997764825821 > (double) Globals.Instance.sizeH * 0.44999998807907104)
        this.destinationPosition = new Vector3(this.destinationPosition.x, (float) ((double) Globals.Instance.sizeH * 0.44999998807907104 * 100.0) - this.followSizeDelta.y, this.destinationPosition.z);
      if ((double) Vector3.Distance(this.popupContainer.localPosition, this.destinationPosition) > 1.0)
      {
        this.popupContainer.localPosition = Vector3.Lerp(this.popupContainer.localPosition, this.destinationPosition, 8f * Time.deltaTime);
      }
      else
      {
        this.popupActive = false;
        this.popupContainer.localPosition = new Vector3((float) Mathf.CeilToInt(this.destinationPosition.x), (float) Mathf.CeilToInt(this.destinationPosition.y), this.popupContainer.localPosition.z);
      }
    }
  }

  private IEnumerator ShowPopup()
  {
    PopupManager popupManager = this;
    popupManager.ClosePopup();
    popupManager.popupContainer.transform.localScale = new Vector3(1f, 1f, 1f);
    popupManager.theTFposition = !((UnityEngine.Object) popupManager.theTF != (UnityEngine.Object) null) ? Vector3.zero : popupManager.theTF.localPosition;
    if (TomeManager.Instance.IsActive())
      popupManager.fastPopup = true;
    if (!popupManager.fastPopup)
      yield return (object) Globals.Instance.WaitForSeconds(0.9f);
    else
      yield return (object) Globals.Instance.WaitForSeconds(0.15f);
    if (!(popupManager.position != "follow") || !(popupManager.position != "followdown") || !(popupManager.position != "followdown2") || !((UnityEngine.Object) popupManager.theTF == (UnityEngine.Object) null))
    {
      popupManager.followMouse = false;
      if (popupManager.position == "right" || popupManager.position == "followright")
      {
        Vector2 sizeDelta = popupManager.popupContainer.GetComponent<RectTransform>().sizeDelta;
        popupManager.adjustPosition = new Vector3(sizeDelta.x + 80f, popupManager.theTF.GetComponent<BoxCollider2D>().size.y, 0.0f);
        popupManager.destinationPosition = new Vector3((float) ((double) popupManager.theTF.position.x * 100.0 + (double) popupManager.adjustPosition.x * 0.800000011920929), (float) ((double) popupManager.theTF.position.y * 100.0 + (double) popupManager.adjustPosition.y * 6.0), 1f);
        if (popupManager.position == "followright")
          popupManager.followMouse = true;
      }
      else if (popupManager.position == "left" || popupManager.position == "followleft")
      {
        Vector2 sizeDelta = popupManager.popupContainer.GetComponent<RectTransform>().sizeDelta;
        popupManager.adjustPosition = new Vector3((float) (-(double) sizeDelta.x - 80.0), popupManager.theTF.GetComponent<BoxCollider2D>().size.y, 0.0f);
        popupManager.destinationPosition = new Vector3((float) ((double) popupManager.theTF.position.x * 100.0 + (double) popupManager.adjustPosition.x * 0.800000011920929), (float) ((double) popupManager.theTF.position.y * 100.0 + (double) popupManager.adjustPosition.y * 6.0), 1f);
        if (popupManager.position == "followleft")
          popupManager.followMouse = true;
      }
      else if (popupManager.position == "centerright")
      {
        popupManager.adjustPosition = new Vector3(popupManager.popupContainer.GetComponent<RectTransform>().sizeDelta.x, popupManager.theTF.GetComponent<BoxCollider2D>().size.y, 0.0f);
        popupManager.destinationPosition = new Vector3((float) ((double) popupManager.theTF.position.x * 100.0 + (double) popupManager.adjustPosition.x * 0.699999988079071), (float) ((double) popupManager.theTF.position.y * 100.0 - (double) popupManager.adjustPosition.y * 130.0 * 0.5), 1f);
      }
      else if (popupManager.position == "center")
      {
        popupManager.adjustPosition = new Vector3(0.0f, 30f, 0.0f);
        Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ((UnityEngine.Object) popupManager.theTF != (UnityEngine.Object) null)
        {
          popupManager.destinationPosition = new Vector3(popupManager.theTF.position.x * 100f + popupManager.adjustPosition.x, popupManager.theTF.position.y * 100f + popupManager.adjustPosition.y, 1f);
          if ((double) popupManager.destinationPosition.x * 0.0099999997764825821 > (double) Screen.width * 0.004999999888241291)
          {
            float num = (float) ((double) popupManager.destinationPosition.x * 0.0099999997764825821 - (double) Screen.width * 0.004999999888241291);
            popupManager.destinationPosition -= new Vector3(num * 100f, 0.0f, 0.0f);
          }
          if ((double) popupManager.destinationPosition.x * 0.0099999997764825821 < (double) -Screen.width * 0.004999999888241291)
          {
            float num = (float) ((double) -Screen.width * 0.004999999888241291 - (double) popupManager.destinationPosition.x * 0.0099999997764825821);
            popupManager.destinationPosition += new Vector3(num * 100f, 0.0f, 0.0f);
          }
        }
        else
        {
          Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          popupManager.destinationPosition = new Vector3(worldPoint.x * 100f, worldPoint.y * 100f, 1f);
        }
      }
      else if (popupManager.position == "follow")
      {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        popupManager.destinationPosition = new Vector3(worldPoint.x * 100f, (float) (((double) worldPoint.y + 0.20000000298023224) * 100.0), 1f);
        popupManager.followMouse = true;
      }
      else if (popupManager.position == "followdown")
      {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        popupManager.destinationPosition = new Vector3(worldPoint.x * 100f, (float) (((double) worldPoint.y - 1.0) * 100.0), 1f);
        popupManager.followMouse = true;
      }
      else if (popupManager.position == "followdown2")
      {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        popupManager.destinationPosition = new Vector3((float) (((double) worldPoint.x - 2.0) * 100.0), (float) (((double) worldPoint.y - 4.0) * 100.0), 1f);
        popupManager.followMouse = true;
      }
      popupManager.destinationPosition = new Vector3((float) Mathf.CeilToInt(popupManager.destinationPosition.x), (float) Mathf.CeilToInt(popupManager.destinationPosition.y), 1f);
      popupManager.GO_Popup.SetActive(true);
      popupManager.popupContainer.localPosition = new Vector3(1000f, 1000f, -10f);
      popupManager.coroutine = popupManager.StartCoroutine(popupManager.CalcSize());
    }
  }

  private IEnumerator CalcSize()
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.01f);
    float num = (this.followSizeDelta = this.popupContainer.GetComponent<RectTransform>().sizeDelta).y * 0.5f;
    if (!this.absolutePositionStablished)
    {
      if (this.position == "right" || this.position == "left" || this.position == "followleft" || this.position == "followright")
        this.destinationPosition = new Vector3(this.destinationPosition.x, this.destinationPosition.y - num, this.destinationPosition.z);
      this.destinationPosition = this.RecalcPositionLimits(this.destinationPosition);
      if (this.position == "followdown")
        this.destinationPosition -= new Vector3(0.0f, (float) ((double) this.followSizeDelta.y * 0.5 - 0.20000000298023224), 0.0f);
    }
    else
    {
      this.destinationPosition = new Vector3(this.absolutePosition.x, this.absolutePosition.y - num, this.absolutePosition.z);
      this.absolutePositionStablished = false;
    }
    this.popupContainer.localPosition = this.destinationPosition - new Vector3(0.0f, 5f, 0.0f);
    this.popupActive = true;
  }

  private Vector3 RecalcPositionLimits(Vector3 destinationPosition)
  {
    if ((double) destinationPosition.x * 0.0099999997764825821 < (double) Globals.Instance.sizeW * -0.5)
      destinationPosition = new Vector3((float) ((double) Globals.Instance.sizeW * -0.5 * 100.0), destinationPosition.y, destinationPosition.z);
    else if ((double) destinationPosition.x * 0.0099999997764825821 > (double) Globals.Instance.sizeW * 0.5)
      destinationPosition = new Vector3((float) ((double) Globals.Instance.sizeW * 0.5 * 100.0), destinationPosition.y, destinationPosition.z);
    else if ((double) destinationPosition.y * 0.0099999997764825821 + (double) this.followSizeDelta.y * 0.0099999997764825821 > (double) Globals.Instance.sizeH * 0.44999998807907104)
      destinationPosition = new Vector3(destinationPosition.x, (float) ((double) Globals.Instance.sizeH * 0.44999998807907104 * 100.0) - this.followSizeDelta.y, destinationPosition.z);
    return destinationPosition;
  }

  public void ClosePopup()
  {
    if ((UnityEngine.Object) this == (UnityEngine.Object) null)
      return;
    if (this.coroutine != null)
      this.StopCoroutine(this.coroutine);
    if (CardScreenManager.Instance.IsActive())
      return;
    this.popupActive = false;
    this.absolutePositionStablished = false;
    if ((UnityEngine.Object) this.popUnlocked != (UnityEngine.Object) null)
      this.popUnlocked.gameObject.SetActive(false);
    if (!((UnityEngine.Object) this.GO_Popup != (UnityEngine.Object) null))
      return;
    this.GO_Popup.SetActive(false);
  }

  public bool IsActive() => this.GO_Popup.gameObject.activeSelf;

  private void FollowRight()
  {
    this.position = "followright";
    this.coroutine = this.StartCoroutine(this.ShowPopup());
  }

  private void FollowPop()
  {
    this.position = "follow";
    this.coroutine = this.StartCoroutine(this.ShowPopup());
  }

  private void FollowPopDown()
  {
    this.position = "followdown";
    this.coroutine = this.StartCoroutine(this.ShowPopup());
  }

  private void FollowPopDown2()
  {
    this.position = "followdown2";
    this.coroutine = this.StartCoroutine(this.ShowPopup());
  }

  private void CenterPop()
  {
    this.position = "center";
    this.coroutine = this.StartCoroutine(this.ShowPopup());
  }

  private void RightPop()
  {
    this.position = "right";
    this.coroutine = this.StartCoroutine(this.ShowPopup());
  }

  private void LeftPop()
  {
    this.position = "left";
    this.coroutine = this.StartCoroutine(this.ShowPopup());
  }

  private void Bottom()
  {
    this.position = "bottom";
    this.coroutine = this.StartCoroutine(this.ShowPopup());
  }

  private void CenterRightPop()
  {
    this.position = "centerright";
    this.coroutine = this.StartCoroutine(this.ShowPopup());
  }

  private void ShowInPosition(string _position)
  {
    this.position = _position;
    this.coroutine = this.StartCoroutine(this.ShowPopup());
  }

  public void SetCard(
    Transform tf,
    CardData cardData,
    List<KeyNotesData> cardDataKeyNotes,
    string position = "right",
    bool fast = false)
  {
    if (CardScreenManager.Instance.IsActive())
      fast = true;
    else if ((UnityEngine.Object) MatchManager.Instance == (UnityEngine.Object) null)
      fast = true;
    this.fastPopup = fast;
    if (this.coroutine != null)
      this.StopCoroutine(this.coroutine);
    if (position == "right" && (UnityEngine.Object) tf != (UnityEngine.Object) null && (double) tf.transform.position.x > (double) Screen.width * 0.004999999888241291 - 6.0 * ((double) Screen.width / 1920.0))
      position = "left";
    if (!PlayerManager.Instance.IsCardUnlocked(cardData.Id))
      this.popUnlocked.gameObject.SetActive(true);
    else
      this.popUnlocked.gameObject.SetActive(false);
    bool flag = false;
    if (cardData.EnergyReductionToZeroPermanent || cardData.EnergyReductionToZeroTemporal || cardData.EnergyReductionPermanent > 0 || cardData.EnergyReductionTemporal > 0 || cardData.ExhaustCounter > 0)
      flag = true;
    if (this.lastPop == cardData.Id && !flag)
    {
      this.DrawPopup(tf);
      this.imageBg.color = this.colorCardtype;
      this.ShowInPosition(position);
    }
    else
    {
      this.popTrait.SetActive(false);
      this.popTown.SetActive(false);
      this.popPerk.SetActive(false);
      this.adjustCardX = 60;
      if (cardDataKeyNotes == null)
        return;
      int count = cardDataKeyNotes.Count;
      if (count <= 0 && cardData.CardType == Enums.CardType.None)
        return;
      this.DrawPopup(tf);
      this.imageBg.color = this.colorCardtype;
      if (cardData.CardType != Enums.CardType.None)
      {
        StringBuilder stringBuilder1 = new StringBuilder();
        stringBuilder1.Append("<line-height=140%><voffset=-1><size=26><sprite name=cards></size></voffset>");
        stringBuilder1.Append("<size=21><color=#fc0><b>");
        stringBuilder1.Append(Texts.Instance.GetText(Enum.GetName(typeof (Enums.CardType), (object) cardData.CardType)));
        stringBuilder1.Append("</b></color>");
        for (int index = 0; index < cardData.CardTypeAux.Length; ++index)
        {
          if (cardData.CardTypeAux[index] != Enums.CardType.None)
          {
            stringBuilder1.Append(", ");
            stringBuilder1.Append(Texts.Instance.GetText(Enum.GetName(typeof (Enums.CardType), (object) cardData.CardTypeAux[index])));
          }
        }
        stringBuilder1.Append("</size></line-height>");
        if (cardData.CardType == Enums.CardType.Enchantment)
        {
          stringBuilder1.Append("<br><size=17><color=#AAAAAA>");
          stringBuilder1.Append(Texts.Instance.GetText("maximumEnchantments"));
          stringBuilder1.Append("</size></color>");
        }
        if (flag)
        {
          stringBuilder1.Append("<br><sprite name=energy>");
          StringBuilder stringBuilder2 = new StringBuilder();
          if (cardData.EnergyReductionToZeroPermanent)
          {
            stringBuilder2.Append(" <color=#FFF>");
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsCost"), (object) 0));
            stringBuilder2.Append("</color>");
          }
          else if (cardData.EnergyReductionToZeroTemporal)
          {
            stringBuilder2.Append(" <color=#FFF>");
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsCost"), (object) 0));
            stringBuilder2.Append("</color>");
            stringBuilder2.Append(" (");
            stringBuilder2.Append(Texts.Instance.GetText("cardsCostUntilDiscarded"));
            stringBuilder2.Append(")");
          }
          if (cardData.EnergyReductionPermanent > 0 || cardData.EnergyReductionTemporal > 0)
          {
            if (stringBuilder2.Length > 0)
              stringBuilder2.Append(" <voffset=1.5>|</voffset>");
            int num = cardData.EnergyReductionPermanent + cardData.EnergyReductionTemporal;
            stringBuilder2.Append(" <color=#FFF>");
            stringBuilder2.Append(string.Format(Texts.Instance.GetText("cardsCostReducedBy"), (object) num));
            stringBuilder2.Append("</color> ");
            if (cardData.EnergyReductionPermanent == 0)
            {
              stringBuilder2.Append("(");
              stringBuilder2.Append(Texts.Instance.GetText("cardsCostUntilDiscarded"));
              stringBuilder2.Append(")");
            }
            else if (cardData.EnergyReductionTemporal > 0)
            {
              stringBuilder2.Append("(");
              stringBuilder2.Append(cardData.EnergyReductionPermanent);
              stringBuilder2.Append(" + ");
              stringBuilder2.Append(cardData.EnergyReductionTemporal);
              stringBuilder2.Append(" ");
              stringBuilder2.Append(Texts.Instance.GetText("cardsCostUntilDiscarded"));
              stringBuilder2.Append(") ");
            }
          }
          if (cardData.ExhaustCounter > 0)
          {
            if (stringBuilder2.Length > 0)
              stringBuilder2.Append(" <voffset=1.5>|</voffset>");
            stringBuilder2.Append(" <color=#EC75D3>");
            stringBuilder2.Append(Texts.Instance.GetText("exhaustion"));
            stringBuilder2.Append(" +");
            stringBuilder2.Append(cardData.ExhaustCounter);
            stringBuilder2.Append("</color>");
          }
          stringBuilder1.Append(stringBuilder2.ToString());
        }
        if (cardData.Sku != "")
        {
          stringBuilder1.Append("<br><size=16><color=#66CCBB>");
          stringBuilder1.Append(SteamManager.Instance.GetDLCName(cardData.Sku));
          stringBuilder1.Append(" ");
          stringBuilder1.Append(Texts.Instance.GetText("dlcAcronymForCharSelection"));
          stringBuilder1.Append("</color></size>");
        }
        this.TextAdjust(stringBuilder1.ToString());
        this.pop.SetActive(true);
      }
      else
        this.pop.SetActive(false);
      this.CleanKeyNotes();
      if (count > 0)
      {
        for (int index = 0; index < count; ++index)
        {
          if ((UnityEngine.Object) cardDataKeyNotes[index] != (UnityEngine.Object) null)
          {
            this.KeyNotesActive.Add(cardDataKeyNotes[index].Id);
            this.KeyNotesGO[cardDataKeyNotes[index].Id].SetActive(true);
          }
        }
      }
      else
      {
        float x = 300f;
        this.adjustCardX = -30;
        RectTransform component = this.popText.GetComponent<RectTransform>();
        component.sizeDelta = new Vector2(x, component.sizeDelta.y);
        this.popText.ForceMeshUpdate(true);
      }
      this.ShowInPosition(position);
      if (flag)
        this.lastPop = (string) null;
      else
        this.lastPop = cardData.Id;
    }
  }

  public void ShowKeyNote(Transform tf, string keyNote, string position = "right", bool fast = false)
  {
    this.fastPopup = fast;
    if (this.coroutine != null)
      this.StopCoroutine(this.coroutine);
    this.popTrait.SetActive(false);
    this.popTown.SetActive(false);
    this.popPerk.SetActive(false);
    this.pop.SetActive(false);
    this.CleanKeyNotes();
    this.KeyNotesActive.Add(keyNote);
    this.KeyNotesGO[keyNote].SetActive(true);
    this.DrawPopup(tf);
    this.ShowInPosition(position);
    this.lastPop = keyNote;
  }

  public void SetTrait(TraitData td, bool includeDescription = true)
  {
    if ((UnityEngine.Object) td == (UnityEngine.Object) null)
      return;
    this.fastPopup = true;
    this.DrawPopup();
    this.pop.SetActive(false);
    this.popPerk.SetActive(false);
    this.popTown.SetActive(false);
    if (includeDescription)
    {
      string[] strArray = new string[4]
      {
        td.TraitName,
        !((UnityEngine.Object) td.TraitCard == (UnityEngine.Object) null) ? string.Format(Texts.Instance.GetText("traitAddCard"), (object) td.TraitCard.CardName) : td.Description,
        "D4AC5B",
        null
      };
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(" <voffset=0><size=21><sprite name=experience></size></voffset> ");
      stringBuilder.Append(string.Format(this.popupText, (object[]) strArray));
      this.popTraitText.text = stringBuilder.ToString();
      this.popTrait.SetActive(true);
    }
    this.CleanKeyNotes();
    int count = td.KeyNotes.Count;
    if (count > 0)
    {
      for (int index = 0; index < count; ++index)
      {
        if ((UnityEngine.Object) td.KeyNotes[index] != (UnityEngine.Object) null)
        {
          this.KeyNotesActive.Add(td.KeyNotes[index].Id);
          this.KeyNotesGO[td.KeyNotes[index].Id].SetActive(true);
        }
      }
    }
    if (this.position == "followdown")
      this.FollowPopDown();
    else
      this.FollowPop();
    this.lastPop = td.TraitName;
  }

  public void SetPerk(string title, string text, string keynote = "")
  {
    this.fastPopup = true;
    this.DrawPopup();
    this.pop.SetActive(false);
    this.popTown.SetActive(false);
    this.popTrait.SetActive(false);
    string[] strArray = new string[4]
    {
      text,
      "",
      "D4AC5B",
      ""
    };
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(string.Format(this.popupText, (object[]) strArray));
    this.popPerkText.text = stringBuilder.ToString();
    this.popPerk.SetActive(true);
    this.CleanKeyNotes();
    if (keynote != "")
    {
      keynote = keynote.ToLower();
      this.KeyNotesActive.Add(keynote);
      if (this.KeyNotesGO.ContainsKey(keynote))
        this.KeyNotesGO[keynote].SetActive(true);
    }
    this.FollowPop();
    this.lastPop = title;
  }

  public void SetTown(string idTitle, string idDescription)
  {
    if (this.lastPop == idTitle)
    {
      this.DrawPopup();
      this.FollowPop();
    }
    else
    {
      this.fastPopup = true;
      this.DrawPopup();
      string[] strArray = new string[4]
      {
        Texts.Instance.GetText(idTitle),
        Texts.Instance.GetText(idDescription),
        "FFC88F",
        ""
      };
      StringBuilder stringBuilder = new StringBuilder();
      string str = "";
      switch (idTitle)
      {
        case "craftCards":
          str = "cards";
          break;
        case "upgradeCards":
          str = "nodeUpgrade";
          break;
        case "removeCards":
          str = "nodeHeal";
          break;
        case "divinationCards":
          str = "nodeDivination";
          break;
        case "buyItems":
          str = "nodeShop";
          break;
      }
      stringBuilder.Append(" <voffset=-3><size=30><sprite name=");
      stringBuilder.Append(str);
      stringBuilder.Append("></size></voffset> ");
      stringBuilder.Append(string.Format(this.popupText, (object[]) strArray));
      this.pop.SetActive(false);
      this.popTrait.SetActive(false);
      this.popPerk.SetActive(false);
      this.popTownText.text = stringBuilder.ToString();
      this.popTown.SetActive(true);
      this.CleanKeyNotes();
      this.FollowPop();
      this.lastPop = idTitle;
    }
  }

  public void SetAuraCurse(
    Transform tf,
    AuraCurseData acData,
    string charges,
    bool fast = false,
    string charId = "")
  {
    if (EventSystem.current.IsPointerOverGameObject())
      return;
    this.fastPopup = fast;
    if (charges == "" || (UnityEngine.Object) acData == (UnityEngine.Object) null)
      return;
    if (this.lastPop == acData.ACName + charges && (UnityEngine.Object) this.lastTransform == (UnityEngine.Object) tf)
    {
      this.DrawPopup(tf);
      if (acData.IsAura)
        this.imageBg.color = this.colorGood;
      else
        this.imageBg.color = this.colorBad;
      this.CenterPop();
    }
    else
    {
      this.DrawPopup(tf);
      this.CleanKeyNotes();
      string[] strArray = new string[4]
      {
        acData.ACName,
        null,
        null,
        null
      };
      int num1 = -1000;
      string input = acData.Description;
      Character character = (Character) null;
      if ((bool) (UnityEngine.Object) MatchManager.Instance)
      {
        character = MatchManager.Instance.GetCharacterById(charId);
        if (character != null)
        {
          Match match = new Regex("_([^>]*)>").Match(input);
          if (match.Success)
          {
            string ACName = match.Groups[1].Value;
            num1 = character.GetAuraCharges(ACName);
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder1.Append("_");
            stringBuilder2.Append("_");
            stringBuilder1.Append(ACName);
            stringBuilder2.Append("sec");
            stringBuilder1.Append(">");
            stringBuilder2.Append(">");
            input = input.Replace(stringBuilder1.ToString(), stringBuilder2.ToString());
          }
        }
      }
      int num2 = Functions.FuncRoundToInt((float) (acData.ChargesMultiplierDescription * int.Parse(charges)));
      string str1 = input.Replace("<ChargesMultiplier>", num2.ToString());
      if (num1 != -1000)
      {
        num2 = Functions.FuncRoundToInt((float) (acData.ChargesMultiplierDescription * num1));
        str1 = str1.Replace("<ChargesMultiplier_sec>", num2.ToString());
      }
      int num3 = Mathf.FloorToInt((float) (int.Parse(charges) / acData.ChargesAuxNeedForOne1));
      string str2 = str1.Replace("<ChargesAux1>", num3.ToString());
      if (num1 != -1000)
      {
        num3 = Mathf.FloorToInt((float) (num1 / acData.ChargesAuxNeedForOne1));
        str2 = str2.Replace("<ChargesAux1_sec>", num3.ToString());
      }
      int num4 = Mathf.FloorToInt((float) (int.Parse(charges) / acData.ChargesAuxNeedForOne2));
      string str3 = str2.Replace("<ChargesAux2>", num4.ToString());
      if (num1 != -1000)
      {
        num4 = Mathf.FloorToInt((float) (num1 / acData.ChargesAuxNeedForOne2));
        str3 = str3.Replace("<ChargesAux2_sec>", num4.ToString());
      }
      int num5 = Functions.FuncRoundToInt((float) int.Parse(charges) * acData.AuraDamageIncreasedPerStack);
      string str4 = str3.Replace("<AuraDamageIncreasedPerStack>", num5.ToString()).Replace("<AuraDamageIncreasedPerStack2>", Functions.FuncRoundToInt((float) int.Parse(charges) * acData.AuraDamageIncreasedPerStack2).ToString()).Replace("<AuraDamageIncreasedPercentPerStack>", Functions.FuncRoundToInt((float) int.Parse(charges) * acData.AuraDamageIncreasedPercentPerStack).ToString()).Replace("<DamageAux1>", Mathf.Abs(Functions.FuncRoundToInt((float) int.Parse(charges) * acData.AuraDamageIncreasedPercentPerStack)).ToString());
      if (num1 != -1000)
      {
        int num6 = Functions.FuncRoundToInt((float) num1 * acData.AuraDamageIncreasedPercentPerStack);
        str4 = str4.Replace("<DamageAux1_sec>", Mathf.Abs(num6).ToString());
      }
      int num7 = Functions.FuncRoundToInt((float) int.Parse(charges) * acData.AuraDamageIncreasedPercentPerStack2);
      string str5 = str4.Replace("<DamageAux2>", Mathf.Abs(num7).ToString());
      if (num1 != -1000)
      {
        int num8 = Functions.FuncRoundToInt((float) num1 * acData.AuraDamageIncreasedPercentPerStack2);
        str5 = str5.Replace("<DamageAux2_sec>", Mathf.Abs(num8).ToString());
      }
      int num9 = Functions.FuncRoundToInt((float) int.Parse(charges) * acData.ResistModifiedPercentagePerStack);
      string str6 = str5.Replace("<ResistAux1>", Math.Abs(num9).ToString()).Replace("<ResistAux2>", Mathf.Abs(Functions.FuncRoundToInt((float) int.Parse(charges) * acData.ResistModifiedPercentagePerStack2)).ToString());
      int num10 = acData.MaxCharges;
      if (MadnessManager.Instance.IsMadnessTraitActive("restrictedpower") || AtOManager.Instance.IsChallengeTraitActive("restrictedpower"))
        num10 = acData.MaxMadnessCharges;
      string str7 = str6.Replace("<MaxCharges>", num10.ToString()).Replace("<HealReceivedPercent>", acData.HealReceivedPercent.ToString()).Replace("<CharacterStatModifiedValue>", acData.CharacterStatModifiedValue.ToString());
      int num11 = acData.DamageWhenConsumed;
      string newValue1 = num11.ToString();
      string str8 = str7.Replace("<DamageWhenConsumed>", newValue1);
      num11 = acData.DamageSidesWhenConsumed;
      string newValue2 = num11.ToString();
      string str9 = str8.Replace("<DamageSidesWhenConsumed>", newValue2);
      num11 = acData.HealAttackerConsumeCharges;
      string newValue3 = num11.ToString();
      string str10 = str9.Replace("<HealAttackerConsumeCharges>", newValue3).Replace("<Resistance1>", (acData.ResistModifiedValue + Functions.FuncRoundToInt(acData.ResistModifiedPercentagePerStack * (float) int.Parse(charges))).ToString());
      num11 = Functions.FuncRoundToInt(acData.DamageWhenConsumedPerCharge * (float) int.Parse(charges));
      string newValue4 = num11.ToString();
      string str11 = str10.Replace("<DamageWhenConsumedPerCharge>", newValue4);
      num11 = acData.ExplodeAtStacks;
      string newValue5 = num11.ToString();
      string textDescription = str11.Replace("<ExplodeAtStacks>", newValue5).Replace("<ChillSpeed>", Mathf.FloorToInt(Mathf.Abs(1f / (float) acData.CharacterStatChargesMultiplierNeededForOne * (float) int.Parse(charges) * (float) acData.CharacterStatModifiedValuePerStack)).ToString()).Replace("<CharacterStatModifiedPerStack>", Mathf.FloorToInt((float) Mathf.Abs(int.Parse(charges) * acData.CharacterStatModifiedValuePerStack)).ToString());
      if (acData.Id == "zeal")
      {
        int num12 = 0;
        if (character != null)
        {
          num12 = Functions.FuncRoundToInt((float) character.GetAuraCharges("burn") * acData.AuraDamageIncreasedPercentPerStack);
          if (character.HaveTrait("righteousflame"))
            num12 += Functions.FuncRoundToInt((float) int.Parse(charges) * acData.AuraDamageIncreasedPercentPerStack2);
        }
        textDescription = textDescription.Replace("<DamageZeal>", num12.ToString());
      }
      string str12 = this.ReplaceGeneralTags(textDescription);
      strArray[1] = str12;
      strArray[2] = "#FFF";
      strArray[3] = "";
      if ((UnityEngine.Object) acData != (UnityEngine.Object) null)
      {
        if (acData.IsAura)
        {
          strArray[2] = "AADDFF";
          this.imageBg.color = this.colorGood;
        }
        else
        {
          strArray[2] = "FF8181";
          this.imageBg.color = this.colorBad;
        }
        if (!acData.GainCharges)
          strArray[3] = "<space=.5em><size=18><color=#AAAAAA>(" + Texts.Instance.GetText("notStackPlain") + ")</color></size>";
      }
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<voffset=-1><size=26><sprite name=");
      stringBuilder.Append(acData.Id.ToLower());
      stringBuilder.Append("></size></voffset>");
      stringBuilder.Append(string.Format(this.popupText, (object[]) strArray));
      this.TextAdjust(stringBuilder.ToString());
      this.pop.SetActive(true);
      this.popTrait.SetActive(false);
      this.popTown.SetActive(false);
      this.popPerk.SetActive(false);
      this.CenterPop();
      this.lastPop = acData.ACName + charges;
      this.lastTransform = tf;
    }
  }

  private string ReplaceGeneralTags(string textDescription)
  {
    textDescription = Functions.FormatString(textDescription);
    return textDescription;
  }

  public void SetText(
    string theText,
    bool fast = false,
    string position = "",
    bool alwaysCenter = false,
    string keynote = "")
  {
    this.popTrait.SetActive(false);
    this.popTown.SetActive(false);
    this.popPerk.SetActive(false);
    this.fastPopup = fast;
    if (this.lastPop == theText)
    {
      this.DrawPopup();
      switch (position)
      {
        case "followdown":
          this.FollowPopDown();
          break;
        case "centerpop":
          this.CenterPop();
          break;
        case "followdown2":
          this.FollowPopDown2();
          break;
        default:
          this.FollowPop();
          break;
      }
    }
    else
    {
      this.DrawPopup();
      this.CleanKeyNotes();
      if (keynote != "")
      {
        this.KeyNotesActive.Add(keynote);
        this.KeyNotesGO[keynote].SetActive(true);
      }
      this.TextAdjust(theText, true, alwaysCenter);
      switch (position)
      {
        case "followdown":
          this.FollowPopDown();
          break;
        case "centerpop":
          this.CenterPop();
          break;
        case "followdown2":
          this.FollowPopDown2();
          break;
        default:
          this.FollowPop();
          break;
      }
      this.lastPop = theText;
    }
  }

  public void SetBackgroundColor(string _color) => this.imageBg.color = Functions.HexToColor(_color);

  private void TextAdjust(string theText, bool adjust = false, bool alwaysCenter = false)
  {
    float x1 = 460f;
    this.GO_Popup.SetActive(true);
    this.pop.SetActive(true);
    RectTransform component = this.popText.GetComponent<RectTransform>();
    this.popText.text = theText;
    this.popText.horizontalAlignment = HorizontalAlignmentOptions.Left;
    this.popText.fontSize = 20f;
    component.sizeDelta = new Vector2(x1, component.sizeDelta.y);
    this.popText.ForceMeshUpdate(true);
    if (adjust && alwaysCenter)
    {
      float x2 = this.popText.bounds.max.x;
      component.sizeDelta = new Vector2(x2 + 50f, component.sizeDelta.y);
      this.popText.horizontalAlignment = HorizontalAlignmentOptions.Center;
    }
    this.GO_Popup.SetActive(false);
  }

  public void SetConsoleText(Transform tf, string theText)
  {
    if (this.lastPop == theText)
    {
      this.DrawPopup(tf);
      this.CenterPop();
    }
    else
    {
      this.DrawPopup(tf);
      this.CleanKeyNotes();
      this.popText.text = MatchManager.Instance.console.GetText(theText);
      this.pop.SetActive(true);
      this.CenterPop();
      this.lastPop = theText;
    }
  }

  public void DrawPopup(Transform tf = null)
  {
    this.theTF = tf;
    this.adjustPosition = (Vector3) this.popupContainer.GetComponent<RectTransform>().sizeDelta;
    this.imageBg.color = this.colorPlain;
  }

  public void RefreshKeyNotes() => this.StartCoroutine(this.RefreshKeyNotesCo());

  private IEnumerator RefreshKeyNotesCo()
  {
    if (this.KeyNotesGO.Count > 0)
    {
      this.CleanKeyNotes();
      foreach (KeyValuePair<string, GameObject> keyValuePair in this.KeyNotesGO)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.KeyNotesGO[keyValuePair.Key]);
      foreach (Transform transform in this.popupContainer)
      {
        if (!this.initialPopupGOs.Contains(transform.gameObject.name))
          UnityEngine.Object.Destroy((UnityEngine.Object) transform.gameObject);
      }
      while (this.popupContainer.childCount > this.initialPopupGOs.Count)
        yield return (object) Globals.Instance.WaitForSeconds(0.01f);
      this.CreateKeyNotes();
      this.lastPop = "";
    }
  }

  public void CreateKeyNotes()
  {
    if (this.popupContainer.childCount > this.initialPopupGOs.Count)
    {
      Debug.LogWarning((object) "[PopupManager] CreateKeyNotes exit because child counter > 5");
    }
    else
    {
      KeyNotesData[] keyNotesDataArray = new KeyNotesData[Globals.Instance.KeyNotes.Count];
      int index1 = 0;
      foreach (KeyValuePair<string, KeyNotesData> keyNote in Globals.Instance.KeyNotes)
      {
        keyNotesDataArray[index1] = keyNote.Value;
        ++index1;
      }
      this.imageBg.color = this.colorPlain;
      for (int index2 = 0; index2 < keyNotesDataArray.Length; ++index2)
      {
        if (keyNotesDataArray[index2].Id == "vanish" || keyNotesDataArray[index2].Id == "innate")
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pop, Vector3.zero, Quaternion.identity, this.popupContainer);
          gameObject.name = keyNotesDataArray[index2].Id;
          TMP_Text component1 = gameObject.transform.Find("Background/Text").GetComponent<TMP_Text>();
          string[] strArray = new string[4];
          string text = Texts.Instance.GetText(keyNotesDataArray[index2].Id);
          strArray[0] = !(text != "") ? keyNotesDataArray[index2].Id : text;
          strArray[1] = keyNotesDataArray[index2].DescriptionExtended;
          Image component2 = gameObject.transform.GetChild(0).GetComponent<Image>();
          if (keyNotesDataArray[index2].Id == "vanish")
          {
            component2.color = this.colorVanish;
            strArray[2] = "EBAAFF";
          }
          else
          {
            strArray[2] = "AFFFC9";
            component2.color = this.colorInnate;
          }
          strArray[3] = "";
          string str1 = this.ReplaceGeneralTags(string.Format(this.popupText, (object[]) strArray));
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("<voffset=-1><size=24><sprite name=");
          stringBuilder.Append(keyNotesDataArray[index2].Id);
          stringBuilder.Append("></size></voffset>");
          stringBuilder.Append(str1);
          string str2 = stringBuilder.ToString();
          component1.text = str2;
          gameObject.SetActive(false);
          this.KeyNotesGO[keyNotesDataArray[index2].Id] = gameObject;
        }
      }
      for (int index3 = 0; index3 < keyNotesDataArray.Length; ++index3)
      {
        if (keyNotesDataArray[index3].Id != "vanish" && keyNotesDataArray[index3].Id != "innate" && keyNotesDataArray[index3].Id != "chain" && keyNotesDataArray[index3].Id != "jump" && keyNotesDataArray[index3].Id != "jump(bonus%)" && keyNotesDataArray[index3].Id != "overcharge" && keyNotesDataArray[index3].Id != "repeat" && keyNotesDataArray[index3].Id != "repeatupto" && keyNotesDataArray[index3].Id != "dispel" && keyNotesDataArray[index3].Id != "purge" && keyNotesDataArray[index3].Id != "discover" && keyNotesDataArray[index3].Id != "reveal" && keyNotesDataArray[index3].Id != "transfer" && keyNotesDataArray[index3].Id != "steal" && keyNotesDataArray[index3].Id != "escapes")
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pop, Vector3.zero, Quaternion.identity, this.popupContainer);
          gameObject.name = keyNotesDataArray[index3].Id;
          TMP_Text component3 = gameObject.transform.Find("Background/Text").GetComponent<TMP_Text>();
          string[] strArray = new string[4];
          string text = Texts.Instance.GetText(keyNotesDataArray[index3].Id);
          strArray[0] = !(text != "") ? keyNotesDataArray[index3].Id : text;
          strArray[1] = GameManager.Instance.ConfigExtendedDescriptions ? keyNotesDataArray[index3].DescriptionExtended : keyNotesDataArray[index3].Description;
          Image component4 = gameObject.transform.GetChild(0).GetComponent<Image>();
          component4.color = this.colorPlain;
          AuraCurseData auraCurseData = Globals.Instance.GetAuraCurseData(keyNotesDataArray[index3].Id);
          if ((UnityEngine.Object) auraCurseData != (UnityEngine.Object) null)
          {
            if (auraCurseData.IsAura)
            {
              component4.color = this.colorGood;
              strArray[2] = "AADDFF";
            }
            else
            {
              component4.color = this.colorBad;
              strArray[2] = "FF8181";
            }
            if (!auraCurseData.GainCharges)
            {
              ref string local = ref strArray[0];
              local = local + "<space=.5em><size=18><color=#AAAAAA>(" + Texts.Instance.GetText("notStackPlain") + ")</color></size>";
            }
          }
          else if (keyNotesDataArray[index3].Id == "energy")
          {
            component4.color = this.colorGood;
            strArray[2] = "AADDFF";
          }
          strArray[3] = "";
          string str3 = this.ReplaceGeneralTags(string.Format(this.popupText, (object[]) strArray));
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("<voffset=-1><size=24><sprite name=");
          stringBuilder.Append(keyNotesDataArray[index3].Id);
          stringBuilder.Append("></size></voffset>");
          stringBuilder.Append(str3);
          string str4 = stringBuilder.ToString();
          component3.text = str4;
          gameObject.SetActive(false);
          this.KeyNotesGO[keyNotesDataArray[index3].Id] = gameObject;
        }
      }
      for (int index4 = 0; index4 < keyNotesDataArray.Length; ++index4)
      {
        if (keyNotesDataArray[index4].Id == "chain" || keyNotesDataArray[index4].Id == "jump" || keyNotesDataArray[index4].Id == "jump(bonus%)" || keyNotesDataArray[index4].Id == "overcharge" || keyNotesDataArray[index4].Id == "repeat" || keyNotesDataArray[index4].Id == "repeatupto" || keyNotesDataArray[index4].Id == "dispel" || keyNotesDataArray[index4].Id == "purge" || keyNotesDataArray[index4].Id == "discover" || keyNotesDataArray[index4].Id == "reveal" || keyNotesDataArray[index4].Id == "transfer" || keyNotesDataArray[index4].Id == "steal" || keyNotesDataArray[index4].Id == "escapes")
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pop, Vector3.zero, Quaternion.identity, this.popupContainer);
          gameObject.name = keyNotesDataArray[index4].Id;
          TMP_Text component = gameObject.transform.Find("Background/Text").GetComponent<TMP_Text>();
          gameObject.transform.GetChild(0).GetComponent<Image>().color = this.colorPlain;
          string[] strArray = new string[4];
          string str5 = Texts.Instance.GetText(keyNotesDataArray[index4].Id);
          if (keyNotesDataArray[index4].Id == "overcharge")
            str5 = "[" + Texts.Instance.GetText("overchargeAcronym") + "] " + str5;
          strArray[0] = !(str5 != "") ? keyNotesDataArray[index4].Id : str5;
          strArray[1] = GameManager.Instance.ConfigExtendedDescriptions ? keyNotesDataArray[index4].DescriptionExtended : keyNotesDataArray[index4].Description;
          strArray[2] = "FFF";
          strArray[3] = "";
          string str6 = this.ReplaceGeneralTags(string.Format(this.popupText, (object[]) strArray));
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("<voffset=-1><size=24><sprite name=cards></size></voffset>");
          stringBuilder.Append(str6);
          string str7 = stringBuilder.ToString();
          component.text = str7;
          gameObject.SetActive(false);
          this.KeyNotesGO[keyNotesDataArray[index4].Id] = gameObject;
        }
      }
      this.popTrait = UnityEngine.Object.Instantiate<GameObject>(this.pop, Vector3.zero, Quaternion.identity, this.popupContainer);
      this.popTrait.transform.Find("Background").GetComponent<Image>().color = this.colorTrait;
      this.popTraitText = this.popTrait.transform.Find("Background/Text").GetComponent<TMP_Text>();
      this.popTrait.transform.GetComponent<VerticalLayoutGroup>().padding = new RectOffset(0, 0, 20, 0);
      this.popTrait.transform.localScale = new Vector3(1f, 1f, 1f);
      this.popTrait.name = "trait";
      this.popTown = UnityEngine.Object.Instantiate<GameObject>(this.pop, Vector3.zero, Quaternion.identity, this.popupContainer);
      this.popTown.transform.Find("Background").GetComponent<Image>().color = this.colorTown;
      this.popTownText = this.popTown.transform.Find("Background/Text").GetComponent<TMP_Text>();
      this.popTown.transform.GetComponent<VerticalLayoutGroup>().padding = new RectOffset(0, 0, 20, 0);
      this.popTown.transform.localScale = new Vector3(1f, 1f, 1f);
      this.popTown.name = "town";
      this.popPerk = UnityEngine.Object.Instantiate<GameObject>(this.pop, Vector3.zero, Quaternion.identity, this.popupContainer);
      this.popPerk.transform.Find("Background").GetComponent<Image>().color = this.colorTrait;
      this.popPerkText = this.popPerk.transform.Find("Background/Text").GetComponent<TMP_Text>();
      this.popPerk.transform.GetComponent<VerticalLayoutGroup>().padding = new RectOffset(0, 0, 10, 0);
      this.popPerk.transform.localScale = new Vector3(1f, 1f, 1f);
      this.popPerk.name = "perk";
      this.popPerkText.lineSpacing = 10f;
    }
  }

  private void CleanKeyNotes()
  {
    if (this.KeyNotesActive.Count <= 0)
      return;
    for (int index = 0; index < this.KeyNotesActive.Count; ++index)
    {
      if (this.KeyNotesGO.ContainsKey(this.KeyNotesActive[index]) && (UnityEngine.Object) this.KeyNotesGO[this.KeyNotesActive[index]] != (UnityEngine.Object) null)
        this.KeyNotesGO[this.KeyNotesActive[index]].SetActive(false);
    }
    this.KeyNotesActive.Clear();
  }
}
