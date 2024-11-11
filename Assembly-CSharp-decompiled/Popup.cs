// Decompiled with JetBrains decompiler
// Type: Popup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
  public GameObject popupPrefab;
  public Sprite plainBg;
  public Sprite warriorBg;
  private Image imageBg;
  private bool popupActive;
  private Coroutine coroutine;
  private GameObject GO_Popup;
  private Transform canvasContainer;
  private RectTransform canvasRect;
  private Transform popupContainer;
  private RectTransform popupRect;
  private GameObject pop;
  private TMP_Text popText;
  private string lastPop = "";
  private string popupText = "<line-height=45><size=26><color=#{2}><b>{0}</b></color></size>\n</line-height>{1}";
  private Dictionary<string, GameObject> KeyNotesGO;
  private List<string> KeyNotesActive;
  private Transform theTF;
  private Vector3 destinationPosition;
  private Vector3 adjustPosition;
  private string position = "";

  private void Awake()
  {
    this.KeyNotesGO = new Dictionary<string, GameObject>();
    this.KeyNotesActive = new List<string>();
  }

  private void Start()
  {
    this.GO_Popup = UnityEngine.Object.Instantiate<GameObject>(this.popupPrefab, Vector3.zero, Quaternion.identity);
    this.canvasContainer = this.GO_Popup.transform.GetChild(0);
    this.canvasRect = this.canvasContainer.GetComponent<RectTransform>();
    this.popupContainer = this.canvasContainer.GetChild(0);
    this.popupRect = this.popupContainer.GetComponent<RectTransform>();
    this.pop = this.popupContainer.GetChild(0).gameObject;
    this.popText = this.pop.transform.Find("Background/Text").GetComponent<TMP_Text>();
    this.imageBg = this.pop.transform.GetChild(0).GetComponent<Image>();
    this.pop.SetActive(false);
    this.GO_Popup.SetActive(false);
    this.CreateKeyNotes();
  }

  private void Update()
  {
    if (!this.popupActive)
      return;
    if ((double) Vector3.Distance(this.popupContainer.localPosition, this.destinationPosition) > 0.019999999552965164)
    {
      this.popupContainer.localPosition = Vector3.Lerp(this.popupContainer.localPosition, this.destinationPosition, 6f * Time.deltaTime);
    }
    else
    {
      this.popupActive = false;
      this.popupContainer.localPosition = new Vector3((float) Mathf.CeilToInt(this.destinationPosition.x), (float) Mathf.CeilToInt(this.destinationPosition.y), 1f);
    }
  }

  private IEnumerator ShowPopup()
  {
    yield return (object) new WaitForSeconds(0.25f);
    if (!((UnityEngine.Object) this.theTF == (UnityEngine.Object) null))
    {
      if (this.position == "right")
      {
        this.adjustPosition = new Vector3(this.popupContainer.GetComponent<RectTransform>().sizeDelta.x, this.theTF.GetComponent<BoxCollider2D>().size.y, 0.0f);
        this.destinationPosition = new Vector3((float) ((double) this.theTF.position.x * 100.0 + (double) this.adjustPosition.x * 0.85000002384185791), (float) ((double) this.theTF.position.y * 100.0 + (double) this.adjustPosition.y * 30.0 * 0.20000000298023224), 1f);
      }
      else if (this.position == "centerright")
      {
        this.adjustPosition = new Vector3(this.popupContainer.GetComponent<RectTransform>().sizeDelta.x, this.theTF.GetComponent<BoxCollider2D>().size.y, 0.0f);
        this.destinationPosition = new Vector3((float) ((double) this.theTF.position.x * 100.0 + (double) this.adjustPosition.x * 0.699999988079071), (float) ((double) this.theTF.position.y * 100.0 - (double) this.adjustPosition.y * 130.0 * 0.5), 1f);
      }
      else if (this.position == "center")
      {
        this.adjustPosition = new Vector3(0.0f, 30f, 0.0f);
        Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ((UnityEngine.Object) this.theTF != (UnityEngine.Object) null)
        {
          this.destinationPosition = new Vector3(this.theTF.position.x * 100f + this.adjustPosition.x, this.theTF.position.y * 100f + this.adjustPosition.y, 1f);
          if ((double) this.destinationPosition.x * 0.0099999997764825821 > (double) Screen.width * 0.004999999888241291 - 2.0)
            this.destinationPosition -= new Vector3((float) ((double) this.destinationPosition.x * 0.0099999997764825821 - ((double) Screen.width * 0.004999999888241291 - 2.0)) * 100f, 0.0f, 0.0f);
          if ((double) this.destinationPosition.x * 0.0099999997764825821 < (double) -Screen.width * 0.004999999888241291 + 2.0)
            this.destinationPosition += new Vector3((float) ((double) -Screen.width * 0.004999999888241291 + 2.0 - (double) this.destinationPosition.x * 0.0099999997764825821) * 100f, 0.0f, 0.0f);
        }
        else
        {
          Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          this.destinationPosition = new Vector3(worldPoint.x * 100f, worldPoint.y * 100f, 1f);
        }
      }
      this.destinationPosition = new Vector3((float) Mathf.CeilToInt(this.destinationPosition.x), (float) Mathf.CeilToInt(this.destinationPosition.y), 1f);
      this.GO_Popup.SetActive(true);
      this.popupContainer.localPosition = this.destinationPosition - new Vector3(0.0f, 20f, 0.0f);
      this.popupActive = true;
    }
  }

  public void ClosePopup()
  {
    if (this.coroutine != null)
      this.StopCoroutine(this.coroutine);
    this.popupActive = false;
    this.GO_Popup.SetActive(false);
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

  private void CenterRightPop()
  {
    this.position = "centerright";
    this.coroutine = this.StartCoroutine(this.ShowPopup());
  }

  public void SetCard(Transform tf, CardData cardData, List<KeyNotesData> cardDataKeyNotes)
  {
    if (this.lastPop == cardData.Id)
    {
      this.DrawPopup(tf);
      this.RightPop();
    }
    else
    {
      int count = cardDataKeyNotes.Count;
      if (count <= 0 && cardData.CardType == Enums.CardType.None)
        return;
      this.DrawPopup(tf);
      if (cardData.CardType != Enums.CardType.None)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<voffset=-2><size=30><sprite name=cards></size></voffset> ");
        stringBuilder.Append("<size=26><color=#fc0><b>");
        stringBuilder.Append(Enum.GetName(typeof (Enums.CardType), (object) cardData.CardType).Replace("_", " "));
        stringBuilder.Append("</b></color>");
        for (int index = 0; index < cardData.CardTypeAux.Length; ++index)
        {
          if (cardData.CardTypeAux[index] != Enums.CardType.None)
          {
            stringBuilder.Append(", ");
            stringBuilder.Append(Enum.GetName(typeof (Enums.CardType), (object) cardData.CardTypeAux[index]));
          }
        }
        stringBuilder.Append("</size>");
        this.popText.text = stringBuilder.ToString();
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
            this.KeyNotesActive.Add(cardDataKeyNotes[index].KeynoteName);
            this.KeyNotesGO[cardDataKeyNotes[index].KeynoteName].SetActive(true);
          }
        }
      }
      this.RightPop();
      this.lastPop = cardData.Id;
    }
  }

  public void SetAuraCurse(Transform tf, AuraCurseData acData, string charges)
  {
    if (EventSystem.current.IsPointerOverGameObject() || charges == "")
      return;
    this.imageBg.sprite = this.plainBg;
    if (this.lastPop == acData.ACName + charges)
    {
      this.DrawPopup(tf);
      this.CenterPop();
    }
    else
    {
      this.DrawPopup(tf);
      this.CleanKeyNotes();
      string[] strArray = new string[3]
      {
        acData.ACName,
        null,
        null
      };
      string str = this.ReplaceGeneralTags(acData.Description.Replace("%ChargesMultiplier%", (acData.ChargesMultiplierDescription * int.Parse(charges)).ToString()).Replace("%ChargesAux1%", Mathf.FloorToInt((float) (int.Parse(charges) / acData.ChargesAuxNeedForOne1)).ToString()).Replace("%ChargesAux2%", Mathf.FloorToInt((float) (int.Parse(charges) / acData.ChargesAuxNeedForOne2)).ToString()).Replace("%CharacterStatModifiedValue%", acData.CharacterStatModifiedValue.ToString()).Replace("%DamageWhenConsumed%", acData.DamageWhenConsumed.ToString()).Replace("%DamageSidesWhenConsumed%", acData.DamageSidesWhenConsumed.ToString()).Replace("%HealAttackerConsumeCharges%", acData.HealAttackerConsumeCharges.ToString()));
      strArray[1] = str;
      strArray[2] = "#FFF";
      this.popText.text = "<voffset=-2><size=30><sprite name=" + strArray[0].Replace(" ", "").ToLower() + "></size></voffset> " + string.Format(this.popupText, (object[]) strArray);
      this.pop.SetActive(true);
      this.CenterPop();
      this.lastPop = acData.ACName + charges;
    }
  }

  private string ReplaceGeneralTags(string textDescription)
  {
    textDescription = Functions.FormatString(textDescription);
    return textDescription;
  }

  public void SetConsoleText(Transform tf, string theText)
  {
    this.imageBg.sprite = this.plainBg;
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
  }

  private void CreateKeyNotes()
  {
    KeyNotesData[] keyNotesDataArray = UnityEngine.Resources.LoadAll<KeyNotesData>("KeyNotes");
    for (int index = 0; index < keyNotesDataArray.Length; ++index)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pop, Vector3.zero, Quaternion.identity, this.popupContainer);
      gameObject.name = keyNotesDataArray[index].KeynoteName;
      TMP_Text component = gameObject.transform.Find("Background/Text").GetComponent<TMP_Text>();
      string[] strArray = new string[3]
      {
        keyNotesDataArray[index].KeynoteName,
        keyNotesDataArray[index].Description,
        "FFF"
      };
      string str = "<voffset=-2><size=30><sprite name=" + strArray[0].Replace(" ", "").ToLower() + "></size></voffset> " + this.ReplaceGeneralTags(string.Format(this.popupText, (object[]) strArray));
      component.text = str;
      gameObject.SetActive(false);
      this.KeyNotesGO[keyNotesDataArray[index].KeynoteName] = gameObject;
    }
  }

  private void CleanKeyNotes()
  {
    for (int index = 0; index < this.KeyNotesActive.Count; ++index)
      this.KeyNotesGO[this.KeyNotesActive[index]].SetActive(false);
    this.KeyNotesActive.Clear();
  }
}
