// Decompiled with JetBrains decompiler
// Type: CharacterReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class CharacterReward : MonoBehaviour
{
  public Transform buttonCharacterDeck;
  public TMP_Text buttonCharacterDeckText;
  public Transform characterImg;
  private SpriteRenderer characterImgSR;
  public Transform chooseCards;
  private TMP_Text chooseCardsText;
  public Transform chooseDust;
  private TMP_Text chooseDustText;
  public Transform quantityDust;
  public TMP_Text quantityDustText;
  public SpriteRenderer borderSPR;
  public Transform cardsTransform;
  public TMP_Text playerNick;
  public Transform combatRewardData;
  public Transform goldT;
  public TMP_Text combatRewardGold;
  public Transform experienceT;
  public TMP_Text combatRewardExperience;
  public Dictionary<string, CardItem> cardsByInternalId;
  private CardItem cardSelected;
  public BotonGeneric botonDust;
  private string ownerNick = "";
  private int index;
  private Dictionary<string, GameObject> cardsGO;
  private bool selected;
  private bool isMyReward = true;
  private Hero hero;

  private void Awake()
  {
    this.characterImgSR = this.characterImg.GetComponent<SpriteRenderer>();
    this.chooseCardsText = this.chooseCards.GetComponent<TMP_Text>();
    this.chooseDustText = this.chooseDust.GetComponent<TMP_Text>();
    this.buttonCharacterDeck.gameObject.SetActive(false);
  }

  private void Start()
  {
  }

  public void Init(int _index)
  {
    this.index = _index;
    this.hero = RewardsManager.Instance.theTeam[_index];
    this.ownerNick = this.hero.Owner;
    this.buttonCharacterDeck.GetComponent<BotonRollover>().auxInt = this.index;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Texts.Instance.GetText("deck"));
    stringBuilder.Append("\n<color=#bbb><size=-.5>");
    stringBuilder.Append(string.Format(Texts.Instance.GetText("cardsNum"), (object) this.hero.Cards.Count));
    this.buttonCharacterDeckText.text = stringBuilder.ToString();
    this.StartCoroutine(this.Show());
  }

  private IEnumerator Show()
  {
    this.cardsGO = new Dictionary<string, GameObject>();
    this.quantityDust.gameObject.SetActive(false);
    if (GameManager.Instance.IsMultiplayer())
    {
      if (this.ownerNick != NetworkManager.Instance.GetPlayerNick())
        this.isMyReward = false;
      this.playerNick.gameObject.SetActive(true);
      this.playerNick.text = "<" + NetworkManager.Instance.GetPlayerNickReal(this.ownerNick) + ">";
      this.playerNick.color = this.borderSPR.color = Functions.HexToColor(NetworkManager.Instance.GetColorFromNick(this.ownerNick));
    }
    else
      this.playerNick.gameObject.SetActive(false);
    if (RewardsManager.Instance.typeOfReward == 1)
    {
      this.combatRewardData.gameObject.SetActive(true);
      if (RewardsManager.Instance.goldEach > 0)
      {
        this.combatRewardGold.text = RewardsManager.Instance.goldEach.ToString();
        AtOManager.Instance.GivePlayer(0, RewardsManager.Instance.goldEach, this.ownerNick);
      }
      else
        this.goldT.gameObject.SetActive(false);
      if (RewardsManager.Instance.experienceEach > 0)
      {
        int rewardForCharacter = this.hero.CalculateRewardForCharacter(RewardsManager.Instance.experienceEach);
        this.combatRewardExperience.text = rewardForCharacter.ToString();
        this.hero.GrantExperience(rewardForCharacter);
      }
      else
        this.experienceT.gameObject.SetActive(false);
    }
    else
      this.combatRewardData.gameObject.SetActive(false);
    if (RewardsManager.Instance.combatScarabDust > 0)
      AtOManager.Instance.GivePlayer(1, RewardsManager.Instance.combatScarabDust, this.ownerNick);
    this.quantityDustText.text = RewardsManager.Instance.dustQuantity.ToString();
    this.characterImgSR.sprite = RewardsManager.Instance.theTeam[this.index].HeroData.HeroSubClass.SpriteBorder;
    this.cardsByInternalId = new Dictionary<string, CardItem>();
    yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    this.characterImg.gameObject.SetActive(true);
    this.buttonCharacterDeck.transform.localPosition = new Vector3(-7f, 0.5f, this.buttonCharacterDeck.transform.localPosition.z);
    this.buttonCharacterDeck.gameObject.SetActive(true);
    if (this.isMyReward)
    {
      this.characterImgSR.color = new Color(1f, 1f, 1f, 1f);
      this.chooseCards.gameObject.SetActive(true);
    }
    else
      this.characterImgSR.color = new Color(1f, 1f, 1f, 0.3f);
    int _cardIndexPosition = 0;
    for (int i = 0; i < 4; ++i)
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.05f);
      if (RewardsManager.Instance.cardsByOrder[this.index].Length > i && RewardsManager.Instance.cardsByOrder[this.index][i] != null)
      {
        yield return (object) Globals.Instance.WaitForSeconds(0.1f);
        this.DoCard(RewardsManager.Instance.cardsByOrder[this.index][i], _cardIndexPosition);
        ++_cardIndexPosition;
      }
    }
    yield return (object) Globals.Instance.WaitForSeconds(0.05f);
    if (!((Object) this.chooseDust == (Object) null) && !((Object) this.quantityDust == (Object) null) && this.isMyReward)
    {
      this.chooseDust.gameObject.SetActive(true);
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      this.quantityDust.gameObject.SetActive(true);
      PlayerManager.Instance.GoldGainedSum(RewardsManager.Instance.goldEach, false);
    }
  }

  private void DoCard(string cardName, int position)
  {
    GameObject gameObject = Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, Vector3.zero, Quaternion.identity, this.cardsTransform);
    this.cardsGO.Add(cardName, gameObject);
    CardItem component = gameObject.GetComponent<CardItem>();
    gameObject.name = "card_" + cardName + "_" + this.index.ToString() + "_" + position.ToString();
    string str = cardName;
    component.SetCard(str, false, this.hero);
    component.DisableCollider();
    gameObject.transform.localPosition = new Vector3((float) ((double) position * 1.9500000476837158 - 1.8999999761581421), 0.0f, 0.0f);
    component.DoReward();
    if (!this.isMyReward)
      component.ShowDisableReward();
    this.cardsByInternalId.Add(gameObject.name, component);
    PlayerManager.Instance.CardUnlock(str, cardItem: component);
  }

  public void DustSelected(string playerNick)
  {
    if (!(playerNick == "") && !(RewardsManager.Instance.theTeam[this.index].Owner == playerNick))
      return;
    this.ShowSelected("dust");
    RewardsManager.Instance.DustSelected(this.index);
    PlayerManager.Instance.DustGainedSum(RewardsManager.Instance.dustQuantity, false);
  }

  public void CardSelected(string playerNick, string internalId)
  {
    if (this.cardsByInternalId == null || !this.cardsByInternalId.ContainsKey(internalId) || !(playerNick == "") && !(RewardsManager.Instance.theTeam[this.index].Owner == playerNick))
      return;
    if ((Object) this.cardSelected != (Object) null)
      this.cardSelected.DrawBorder("");
    this.cardSelected = this.cardsByInternalId[internalId];
    this.ShowSelected(this.cardSelected.CardData.Id);
    RewardsManager.Instance.CardSelected(this.index, this.cardSelected.CardData.Id);
    PlayerManager.Instance.CardUnlock(this.cardSelected.CardData.Id, cardItem: this.cardSelected);
  }

  public void ShowSelected(string rewardId)
  {
    if ((Object) this.chooseDust == (Object) null || (Object) this.quantityDust == (Object) null || (Object) this.chooseCards == (Object) null || (Object) this.combatRewardData == (Object) null || this.selected)
      return;
    this.selected = true;
    this.chooseDust.gameObject.SetActive(false);
    this.chooseCards.gameObject.SetActive(false);
    this.combatRewardData.gameObject.SetActive(false);
    this.playerNick.gameObject.SetActive(false);
    if (rewardId == "dust")
    {
      if (this.cardsGO != null)
      {
        foreach (KeyValuePair<string, GameObject> keyValuePair in this.cardsGO)
          Object.Destroy((Object) keyValuePair.Value);
      }
      this.quantityDust.gameObject.SetActive(true);
      this.botonDust.buttonEnabled = false;
      this.botonDust.ShowBorder(false);
      this.botonDust.ShowBackgroundPlain(false);
      this.quantityDust.localPosition = new Vector3(-4f, -0.3f, 0.0f);
      this.quantityDust.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
    }
    else
    {
      Object.Destroy((Object) this.quantityDust.gameObject);
      Transform transform = (Transform) null;
      if (this.cardsGO != null)
      {
        foreach (KeyValuePair<string, GameObject> keyValuePair in this.cardsGO)
        {
          if (keyValuePair.Key != rewardId)
            Object.Destroy((Object) keyValuePair.Value);
          else
            transform = keyValuePair.Value.transform;
        }
      }
      if (!((Object) transform != (Object) null))
        return;
      transform.localPosition = new Vector3(-4f, 0.0f, 0.0f);
      PlayerManager.Instance.CardUnlock(rewardId, cardItem: transform.GetComponent<CardItem>());
    }
  }

  private void HideGO() => this.gameObject.SetActive(false);
}
