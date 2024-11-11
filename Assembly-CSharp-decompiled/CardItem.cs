// Decompiled with JetBrains decompiler
// Type: CardItem
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

[Serializable]
public class CardItem : MonoBehaviour
{
  [ContextMenuItem("Init Card", "Init")]
  [SerializeField]
  private CardData cardData;
  private ItemData itemData;
  private string internalId;
  public SpriteRenderer portraitDrop;
  public Transform keyTransform;
  public Transform keyRed;
  public Transform keyBackground;
  public TMP_Text keyNumber;
  private Floating floating;
  public Transform cardSpriteT;
  public Transform rightClick;
  public Transform diffEnergy;
  public Transform diffTarget;
  public Transform diffRequire;
  public Transform diffVanish;
  public Transform diffInnate;
  public Transform diffSkillA;
  public Transform diffSkillB;
  public Transform soldT;
  public Transform disableT;
  public Transform cardRelatedT;
  public SpriteRenderer cardRelatedBg;
  public Transform cardUpgradeT;
  public Transform cardUnlockT;
  public Transform cardElementsT;
  public Transform skillImageT;
  public Transform titleTextT;
  public Transform titleTextTBlue;
  public Transform titleTextTGold;
  public Transform titleTextTRed;
  public Transform titleTextTPurple;
  public Transform requireTextT;
  public Transform targetT;
  public Transform targetTextT;
  public Transform descriptionTextT;
  public Transform energyText;
  public Transform energyTextBg;
  private SpriteRenderer energyBg;
  public Transform energyTextItemBg;
  public Transform typeText;
  public Transform typeTextImage;
  public Transform backImageT;
  public Transform skullImageT;
  public Transform checkImageT;
  public Transform lockImage;
  public Transform lockShadowImage;
  public Transform skillUpgradedShader;
  public Transform skillRare;
  public Transform energyModification;
  public TMP_Text energyModificationTM;
  public Transform innateT;
  public Transform innateIconParticle;
  public Transform vanishT;
  public Transform vanishIconParticle;
  public SpriteRenderer borderSprite;
  public Transform rarityUncommonParticle;
  public Transform rarityRareParticle;
  public Transform rarityEpicParticle;
  public Transform rarityMythicParticle;
  public Transform cardBorder;
  public Transform cardBorderMythic;
  private SpriteRenderer cardBorderSR;
  public ParticleSystem trailParticlesNPC;
  public ParticleSystem sightParticle;
  public ParticleSystem vanishParticle;
  public ParticleSystem borderParticle;
  public Transform dissolveParticleT;
  private ParticleSystem dissolveParticle;
  public Transform backParticleT;
  public ParticleSystem backParticle;
  public ParticleSystem backParticleSpark;
  private Transform cardParticlesBorderT;
  public int tableCards;
  public int tablePosition;
  private BoxCollider2D cardCollider;
  private LineRenderer cardLine;
  private Transform cardLineT;
  private CursorArrow cursorArrow;
  private SpriteRenderer cardSpriteSR;
  private SpriteRenderer skillImageSR;
  private TMP_Text titleTextTM;
  private TMP_Text titleTextTMBlue;
  private TMP_Text titleTextTMGold;
  private TMP_Text titleTextTMRed;
  private TMP_Text titleTextTMPurple;
  private TMP_Text requireTextTM;
  private TMP_Text targetTextTM;
  private TMP_Text descriptionTextTM;
  private TMP_Text energyTextTM;
  private TMP_Text typeTextTM;
  private TMP_Text npcOrderTM;
  public TMP_Text nameTome;
  public TMP_Text numberTome;
  public Transform numberTomeBg;
  public string lootId;
  private List<Renderer> childRendererList;
  private SpriteRenderer iconTSpriteRenderer;
  private Vector3 initialLocalPosition;
  private Vector3 destinationLocalPosition;
  private Quaternion initialLocalRotation;
  private Quaternion destinationLocalRotation;
  private Vector3 mouseClickedPosition;
  private Vector3 transformVerticalDesplazament;
  private int canInstaCast = -1;
  private int canEnergyCast = -1;
  private int canTargetCast = -1;
  private float cardSize = 1f;
  private float cardSizeTable = 1.2f;
  private float cardSizeAmplified = 1.4f;
  private float smooth = 0.18f;
  private float tiltAngle = 60f;
  private float cardScaleNPC = 0.3f;
  private float cardDistanceNPC = 0.45f;
  private Vector3 NPCTargetPosition = new Vector3(-100f, -100f, 0.0f);
  private NPC theNPC;
  private Hero theHero;
  public bool prediscard;
  public bool discard;
  public bool active;
  public bool casting;
  public bool cardnpc;
  public bool cardhided;
  public bool cardrevealed;
  public bool cardoutsidecombat;
  public bool cardoutsidecombatamplified;
  public bool cardoutsidelibary;
  public bool cardoutsideselection;
  public bool cardoutsidereward;
  public bool cardoutsideloot;
  public bool cardmakebig;
  public float cardmakebigSize;
  public float cardmakebigSizeMax;
  public bool cardoutsideverticallist;
  public bool lockPosition;
  private bool cardDraggedCanCast;
  private int distanceForDragCast = 80;
  public bool destroyAtLocation;
  public bool cardVanish;
  public bool cardfordiscard;
  public bool cardforaddcard;
  public bool cardselectedfordiscard;
  public bool cardselectedforaddcard;
  public bool cardfordisplay;
  private int sortingOrderDiscard = -30000;
  private GameObject cardAmplifyNPC;
  private GameObject cardAmplifyOutside;
  private Coroutine revealedCoroutine;
  private Coroutine floatingCoroutine;
  private Coroutine npcOrderCoroutine;
  private Color greenColor = new Color(0.0f, 1f, 0.05f, 0.75f);
  private Color redColor = new Color(1f, 0.0f, 0.07f, 0.75f);
  private Color blueColor = new Color(0.0f, 0.5f, 1f, 0.75f);
  private Color orangeColor = new Color(1f, 0.6f, 0.0f, 0.75f);
  private Color blackColor = new Color(0.0f, 0.0f, 0.0f, 0.5f);
  private Color purpleColor = new Color(0.23f, 0.05f, 0.24f, 0.5f);
  public GameObject relatedCard;
  public int CardPlayerIndex = -1;
  public Transform mpMarks;
  public SpriteRenderer[] mpMark;
  public Transform portraits;
  public SpriteRenderer[] portrait;
  public Transform emotes;
  public SpriteRenderer emote0;
  public SpriteRenderer emote1;
  public SpriteRenderer emote2;
  public Transform emoteIcon;
  private float adjustForCardSize;
  private float adjustForCardHeight;

  private void Awake()
  {
    this.floating = this.GetComponent<Floating>();
    this.cardCollider = this.GetComponent<BoxCollider2D>();
    if ((UnityEngine.Object) this.cardSpriteT != (UnityEngine.Object) null)
    {
      this.cardSpriteSR = this.cardSpriteT.GetComponent<SpriteRenderer>();
      this.skillImageSR = this.skillImageT.GetComponent<SpriteRenderer>();
      this.titleTextTM = this.titleTextT.GetComponent<TMP_Text>();
      this.titleTextTMBlue = this.titleTextTBlue.GetComponent<TMP_Text>();
      this.titleTextTMGold = this.titleTextTGold.GetComponent<TMP_Text>();
      this.titleTextTMRed = this.titleTextTRed.GetComponent<TMP_Text>();
      this.titleTextTMPurple = this.titleTextTPurple.GetComponent<TMP_Text>();
      this.descriptionTextTM = this.descriptionTextT.GetComponent<TMP_Text>();
      this.targetTextTM = this.targetTextT.GetComponent<TMP_Text>();
      this.requireTextTM = this.requireTextT.GetComponent<TMP_Text>();
      this.energyTextTM = this.energyText.GetComponent<TMP_Text>();
      this.energyBg = this.energyTextBg.GetComponent<SpriteRenderer>();
      this.typeTextTM = this.typeText.GetComponent<TMP_Text>();
      this.cardBorderSR = this.cardBorder.GetComponent<SpriteRenderer>();
      this.dissolveParticle = this.dissolveParticleT.GetComponent<ParticleSystem>();
      this.childRendererList = new List<Renderer>();
      foreach (Transform transform in this.transform)
      {
        Renderer component1 = transform.GetComponent<Renderer>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
        {
          component1.sortingLayerName = "Cards";
          this.childRendererList.Add(component1);
        }
        else if (transform.gameObject.name == "CardGO" || transform.gameObject.name == "Emotes" || transform.gameObject.name == "Key")
        {
          foreach (Component component2 in transform)
          {
            Renderer component3 = component2.GetComponent<Renderer>();
            if ((UnityEngine.Object) component3 != (UnityEngine.Object) null)
            {
              component3.sortingLayerName = "Cards";
              this.childRendererList.Add(component3);
            }
          }
        }
      }
      if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null)
        this.cursorArrow = MatchManager.Instance.cursorArrow;
      if (this.backImageT.gameObject.activeSelf)
        this.backImageT.gameObject.SetActive(false);
      this.HideDifferences();
    }
    if (!((UnityEngine.Object) this.keyTransform != (UnityEngine.Object) null) || !this.keyTransform.gameObject.activeSelf)
      return;
    this.keyTransform.gameObject.SetActive(false);
  }

  private void Start()
  {
    this.transformVerticalDesplazament = Vector3.zero;
    this.adjustForCardSize = Globals.Instance.sizeW * 0.15f;
    this.adjustForCardHeight = Globals.Instance.sizeH * 0.25f;
  }

  private void Update()
  {
    if (this.cardoutsidecombatamplified)
    {
      Vector3 b1;
      b1.x = this.cardSize;
      b1.y = this.cardSize;
      b1.z = 1f;
      if ((double) Mathf.Abs(this.transform.localScale.x - this.cardSize) > 0.004999999888241291)
        this.transform.localScale = Vector3.Slerp(this.transform.localScale, b1, this.smooth);
      else if (this.transform.localScale != b1)
        this.transform.localScale = b1;
      Vector3 worldPoint = GameManager.Instance.cameraMain.ScreenToWorldPoint(Input.mousePosition);
      Vector3 b2;
      if (this.cardoutsideselection)
      {
        b2.x = worldPoint.x;
        b2.y = worldPoint.y + 3.5f;
        b2.z = 1f;
      }
      else if (this.cardoutsidelibary)
      {
        if ((double) worldPoint.x > 0.0)
        {
          b2.x = worldPoint.x - 2f;
          b2.y = worldPoint.y;
          b2.z = 1f;
        }
        else
        {
          b2.x = worldPoint.x + 2f;
          b2.y = worldPoint.y;
          b2.z = 1f;
        }
      }
      else
      {
        b2.x = worldPoint.x + 2f;
        b2.y = worldPoint.y;
        b2.z = 1f;
      }
      if (this.cardoutsideverticallist)
      {
        b2.x = this.transform.localPosition.x;
        b2.y -= 1.6f;
        b2.z = 1f;
        if ((bool) (UnityEngine.Object) ChallengeSelectionManager.Instance)
          b2.y += 2f;
      }
      if ((double) b2.x + (double) this.adjustForCardSize > (double) Globals.Instance.sizeW * 0.5)
        b2.x = Globals.Instance.sizeW * 0.5f - this.adjustForCardSize;
      if ((double) b2.y + (double) this.adjustForCardHeight > (double) Globals.Instance.sizeH * 0.5)
        b2.y = Globals.Instance.sizeH * 0.5f - this.adjustForCardHeight;
      else if ((double) b2.y - (double) this.adjustForCardHeight < -(double) Globals.Instance.sizeH * 0.5)
        b2.y = (float) (-(double) Globals.Instance.sizeH * 0.5) + this.adjustForCardHeight;
      this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, b2, this.smooth);
    }
    else
    {
      if (this.cardoutsidelibary && TomeManager.Instance.IsActive())
        return;
      if (this.active || this.discard || this.cardnpc || this.cardrevealed || (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && MatchManager.Instance.GameStatus == "BeginTurnHero" && !this.cardfordisplay)
      {
        if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && (UnityEngine.Object) MatchManager.Instance.CardActiveT == (UnityEngine.Object) this.transform)
        {
          if (Input.GetMouseButtonUp(1))
          {
            this.DoReturnCardToDeckFromDrag();
            return;
          }
          float x1;
          float num;
          Quaternion b;
          if (this.canInstaCast == 1 && this.canEnergyCast == 1)
          {
            x1 = (float) (-((double) this.mouseClickedPosition.x - (double) Input.mousePosition.x) * 0.0040000001899898052);
            num = (float) (((double) this.mouseClickedPosition.y - (double) Input.mousePosition.y) * 0.0040000001899898052);
            b = Quaternion.Euler(0.0f, 0.0f, 0.0f);
          }
          else
          {
            x1 = (float) (((double) this.mouseClickedPosition.x - (double) Input.mousePosition.x) * 0.00050000002374872565);
            num = (float) (((double) this.mouseClickedPosition.y - (double) Input.mousePosition.y) * 0.000699999975040555);
            float z = x1 * this.tiltAngle;
            float x2 = num * this.tiltAngle;
            if ((double) x2 < -30.0)
              x2 = -30f;
            if ((double) z < -40.0)
              z = -40f;
            if ((double) z > 40.0)
              z = 40f;
            b = Quaternion.Euler(x2, 0.0f, z);
          }
          this.transform.rotation = Quaternion.Slerp(this.transform.rotation, b, this.smooth);
          this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, this.initialLocalPosition + this.transformVerticalDesplazament + new Vector3(x1, -num, 0.0f) * 2f, this.smooth);
        }
        else
        {
          if ((double) this.transform.localScale.x != (double) this.cardSize)
          {
            if ((double) Mathf.Abs(this.transform.localScale.x - this.cardSize) > 0.004999999888241291)
              this.transform.localScale = Vector3.Slerp(this.transform.localScale, new Vector3(this.cardSize, this.cardSize, 1f), Time.deltaTime * 14f);
            else
              this.transform.localScale = new Vector3(this.cardSize, this.cardSize, 1f);
          }
          if (!this.lockPosition && this.transform.localPosition != this.destinationLocalPosition)
          {
            if ((double) Vector3.Distance(this.transform.localPosition, this.destinationLocalPosition) > 0.004999999888241291)
            {
              if (!this.casting)
              {
                if (!this.cardnpc)
                  this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, this.destinationLocalPosition, Time.deltaTime * 14f);
                else
                  this.transform.localPosition = Vector3.Slerp(this.transform.localPosition, this.destinationLocalPosition, Time.deltaTime * 14f);
              }
              else
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, this.destinationLocalPosition, Time.deltaTime * 10f);
            }
            else
            {
              this.transform.localPosition = this.destinationLocalPosition;
              if (this.cardforaddcard || this.cardfordiscard)
                this.active = false;
              if (this.destroyAtLocation)
                UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
            }
          }
          if (this.transform.rotation != this.destinationLocalRotation)
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.destinationLocalRotation, Time.deltaTime * 14f);
          if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && this.discard && (double) Vector3.Distance(this.transform.localPosition, MatchManager.Instance.GetDiscardPilePosition()) < 0.0099999997764825821)
          {
            this.discard = false;
            this.transform.localPosition = MatchManager.Instance.GetDiscardPilePosition();
            this.transform.rotation = this.destinationLocalRotation;
            this.enabled = false;
          }
        }
      }
      if (!(bool) (UnityEngine.Object) MatchManager.Instance || !MatchManager.Instance.controllerClickedCard || !((UnityEngine.Object) MatchManager.Instance.CardActiveT == (UnityEngine.Object) this.transform))
        return;
      this.OnMouseDrag();
    }
  }

  public void ShowSpriteOverCard()
  {
    if (!((UnityEngine.Object) this.itemData != (UnityEngine.Object) null) || !((UnityEngine.Object) this.itemData.SpriteBossDrop != (UnityEngine.Object) null))
      return;
    this.portraitDrop.transform.gameObject.SetActive(true);
    this.portraitDrop.sprite = this.itemData.SpriteBossDrop;
  }

  private void CardChildSorting(string _layerName = "Cards", int _position = 0, int _offset = 100)
  {
    if (this.childRendererList == null)
      return;
    int num = _offset;
    for (int index = 0; index < this.childRendererList.Count && !((UnityEngine.Object) this == (UnityEngine.Object) null) && !((UnityEngine.Object) this.gameObject == (UnityEngine.Object) null); ++index)
    {
      if (_layerName != "")
        this.childRendererList[index].sortingLayerName = _layerName;
      this.childRendererList[index].sortingOrder = _position + num;
      if (this.childRendererList[index].transform.childCount > 0 && (UnityEngine.Object) this.childRendererList[index].transform.GetComponent<MeshRenderer>() != (UnityEngine.Object) null)
      {
        foreach (Transform transform in this.childRendererList[index].transform)
        {
          MeshRenderer component1 = transform.GetComponent<MeshRenderer>();
          if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          {
            if (_layerName != "")
              component1.sortingLayerName = _layerName;
            component1.sortingOrder = _position + num;
          }
          ParticleSystemRenderer component2 = transform.GetComponent<ParticleSystemRenderer>();
          if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          {
            if (_layerName != "")
              component2.sortingLayerName = _layerName;
            component2.sortingOrder = _position + num - 1;
          }
        }
      }
      if ((UnityEngine.Object) this.cardData != (UnityEngine.Object) null && this.childRendererList[index].name == "EnergyItemBg" && this.childRendererList[index].gameObject.activeSelf)
      {
        if ((UnityEngine.Object) this.iconTSpriteRenderer == (UnityEngine.Object) null)
          this.iconTSpriteRenderer = this.cardData.CardType != Enums.CardType.Weapon ? (this.cardData.CardType != Enums.CardType.Armor ? (this.cardData.CardType != Enums.CardType.Jewelry ? (this.cardData.CardType != Enums.CardType.Pet ? this.childRendererList[index].transform.GetChild(3).GetComponent<SpriteRenderer>() : this.childRendererList[index].transform.GetChild(4).GetComponent<SpriteRenderer>()) : this.childRendererList[index].transform.GetChild(2).GetComponent<SpriteRenderer>()) : this.childRendererList[index].transform.GetChild(1).GetComponent<SpriteRenderer>()) : this.childRendererList[index].transform.GetChild(0).GetComponent<SpriteRenderer>();
        SpriteRenderer component = this.childRendererList[index].transform.GetComponent<SpriteRenderer>();
        this.iconTSpriteRenderer.sortingOrder = component.sortingOrder + 1;
        this.iconTSpriteRenderer.sortingLayerName = component.sortingLayerName;
      }
      --num;
    }
  }

  public void DefaultElementsLayeringOrder(int auxPosition = 20)
  {
    if (this.discard)
      return;
    int _offset = 100;
    this.CardChildSorting(_position: auxPosition != -1 ? auxPosition + _offset * this.tablePosition : -20 * -MatchManager.Instance.CountHeroDiscard(), _offset: _offset);
  }

  public void TopLayeringOrder(string layerName = "Cards", int _position = 3000)
  {
    int _position1 = _position;
    int _offset = 100;
    this.CardChildSorting(layerName, _position1, _offset);
  }

  public IEnumerator SelfDestruct(float delay = 0.0f)
  {
    CardItem cardItem = this;
    yield return (object) Globals.Instance.WaitForSeconds(0.2f);
    cardItem.DisableTrail();
    cardItem.HideKeyNotes();
    cardItem.active = false;
    cardItem.discard = false;
    cardItem.transform.localPosition += new Vector3(0.0f, 0.0f, 1000f);
    yield return (object) Globals.Instance.WaitForSeconds(delay);
    if ((bool) (UnityEngine.Object) MatchManager.Instance)
      cardItem.enabled = false;
    else
      UnityEngine.Object.Destroy((UnityEngine.Object) cardItem.gameObject);
  }

  public void CreateColliderAdjusted(float sizeX = 0.0f, float sizeY = 0.0f)
  {
    if ((UnityEngine.Object) this.cardCollider == (UnityEngine.Object) null)
      return;
    this.cardCollider.enabled = true;
    Sprite sprite = this.backImageT.GetComponent<SpriteRenderer>().sprite;
    BoxCollider2D cardCollider = this.cardCollider;
    Bounds bounds = sprite.bounds;
    double x = (double) bounds.size.x + (double) sizeX;
    bounds = sprite.bounds;
    double y = (double) bounds.size.y + (double) sizeY;
    Vector2 vector2 = new Vector2((float) x, (float) y);
    cardCollider.size = vector2;
    this.cardCollider.offset = new Vector2(0.0f, 0.0f);
  }

  public void CreateColliderHand()
  {
    this.cardCollider.enabled = true;
    Sprite sprite = this.backImageT.GetComponent<SpriteRenderer>().sprite;
    BoxCollider2D cardCollider1 = this.cardCollider;
    Bounds bounds1 = sprite.bounds;
    double x = (double) bounds1.size.x;
    bounds1 = sprite.bounds;
    double y1 = (double) bounds1.size.y * 1.2999999523162842;
    Vector2 vector2_1 = new Vector2((float) x, (float) y1);
    cardCollider1.size = vector2_1;
    BoxCollider2D cardCollider2 = this.cardCollider;
    Bounds bounds2 = sprite.bounds;
    double num = (double) bounds2.size.y * 1.2999999523162842;
    bounds2 = sprite.bounds;
    double y2 = (double) bounds2.size.y;
    Vector2 vector2_2 = new Vector2(0.0f, (float) (-(num - y2) * 0.5));
    cardCollider2.offset = vector2_2;
  }

  public void DisableCollider()
  {
    if (!((UnityEngine.Object) this.cardCollider != (UnityEngine.Object) null))
      return;
    this.cardCollider.enabled = false;
  }

  public void EnableCollider()
  {
    if (!((UnityEngine.Object) this.cardCollider != (UnityEngine.Object) null))
      return;
    this.cardCollider.enabled = true;
  }

  public CardData GetCardData() => this.cardData;

  public void DoTome(bool showName, int position, int total)
  {
    if (PlayerManager.Instance.IsCardUnlocked(this.internalId))
    {
      this.numberTome.gameObject.SetActive(true);
      this.numberTomeBg.gameObject.SetActive(true);
      this.numberTome.text = position.ToString();
    }
    else
    {
      if (this.numberTome.gameObject.activeSelf)
        this.numberTome.gameObject.SetActive(false);
      if (!this.numberTomeBg.gameObject.activeSelf)
        return;
      this.numberTomeBg.gameObject.SetActive(false);
    }
  }

  public void ShowSold() => this.soldT.gameObject.SetActive(true);

  public void ShowLock()
  {
    if (!this.lockImage.gameObject.activeSelf)
      this.lockImage.gameObject.SetActive(true);
    if (this.lockShadowImage.gameObject.activeSelf)
      return;
    this.lockShadowImage.gameObject.SetActive(true);
  }

  public void HideLock()
  {
    if (this.lockImage.gameObject.activeSelf)
      this.lockImage.gameObject.SetActive(false);
    if (!this.lockShadowImage.gameObject.activeSelf)
      return;
    this.lockShadowImage.gameObject.SetActive(false);
  }

  public void ShowRelated()
  {
    this.cardRelatedT.gameObject.SetActive(true);
    this.cardRelatedBg.sortingOrder = this.cardRelatedT.GetComponent<Renderer>().sortingOrder - 1;
    this.cardRelatedBg.sortingLayerName = this.cardRelatedT.GetComponent<Renderer>().sortingLayerName;
  }

  public void RemoveCardData() => this.cardData = (CardData) null;

  public void SetCardback(Hero theHero)
  {
    string cardbackUsed = theHero.CardbackUsed;
    if (!(cardbackUsed != ""))
      return;
    CardbackData cardbackData = Globals.Instance.GetCardbackData(cardbackUsed);
    if ((UnityEngine.Object) cardbackData == (UnityEngine.Object) null)
    {
      cardbackData = Globals.Instance.GetCardbackData(Globals.Instance.GetCardbackBaseIdBySubclass(theHero.HeroData.HeroSubClass.Id));
      if ((UnityEngine.Object) cardbackData == (UnityEngine.Object) null)
        cardbackData = Globals.Instance.GetCardbackData("defaultCardback");
    }
    Sprite cardbackSprite = cardbackData.CardbackSprite;
    if (!((UnityEngine.Object) cardbackSprite != (UnityEngine.Object) null))
      return;
    this.backImageT.GetComponent<SpriteRenderer>().sprite = cardbackSprite;
  }

  public void SetCard(
    string id,
    bool deckScale = true,
    Hero _theHero = null,
    NPC _theNPC = null,
    bool GetFromGlobal = false,
    bool _generated = false)
  {
    this.iconTSpriteRenderer = (SpriteRenderer) null;
    if (_theHero != null)
      this.SetCardback(_theHero);
    if ((UnityEngine.Object) this.descriptionTextTM != (UnityEngine.Object) null)
      this.descriptionTextTM.fontSizeMax = 1.8f;
    if ((UnityEngine.Object) this.rightClick != (UnityEngine.Object) null && this.rightClick.gameObject.activeSelf)
      this.rightClick.gameObject.SetActive(false);
    if ((UnityEngine.Object) this.soldT != (UnityEngine.Object) null && this.soldT.gameObject.activeSelf)
      this.soldT.gameObject.SetActive(false);
    if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && !GetFromGlobal)
    {
      this.cardData = MatchManager.Instance.GetCardData(id);
      if ((UnityEngine.Object) this.cardData == (UnityEngine.Object) null)
      {
        id = MatchManager.Instance.CreateCardInDictionary(id);
        this.cardData = MatchManager.Instance.GetCardData(id);
      }
    }
    else
      this.cardData = Globals.Instance.GetCardData(id);
    if ((UnityEngine.Object) this.cardData == (UnityEngine.Object) null)
      return;
    if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && _theHero != null)
      this.cardData.ModifyDamageType(_theHero.GetEnchantModifiedDamageType(), (Character) _theHero);
    this.cardData.InternalId = this.internalId = id;
    if ((UnityEngine.Object) this.lockImage != (UnityEngine.Object) null)
    {
      if (GameManager.Instance.IsObeliskChallenge() || PlayerManager.Instance.IsCardUnlocked(id) || (UnityEngine.Object) this.cardData.Item != (UnityEngine.Object) null && this.cardData.Item.QuestItem || SceneStatic.GetSceneName() == "CardPlayer" || !this.cardData.ShowInTome)
      {
        if (this.lockImage.gameObject.activeSelf)
          this.lockImage.gameObject.SetActive(false);
      }
      else if (!this.lockImage.gameObject.activeSelf)
        this.lockImage.gameObject.SetActive(true);
    }
    this.cardData.Visible = false;
    if (_theHero != null)
    {
      this.RedrawDescriptionPrecalculated(_theHero);
      this.theHero = _theHero;
    }
    else if (_theNPC != null)
    {
      this.RedrawDescriptionPrecalculatedNPC(_theNPC);
      this.theNPC = _theNPC;
    }
    if ((UnityEngine.Object) this.cardSpriteSR == (UnityEngine.Object) null)
      return;
    this.cardSpriteSR.sprite = GameManager.Instance.cardSprites[(int) this.cardData.CardClass];
    this.skillImageSR.sprite = this.cardData.Sprite;
    if (!this.cardData.FlipSprite)
      this.skillImageSR.flipX = false;
    else if (this.cardData.FlipSprite)
      this.skillImageSR.flipX = true;
    this.itemData = (ItemData) null;
    if ((UnityEngine.Object) this.skillRare != (UnityEngine.Object) null && this.skillRare.gameObject.activeSelf)
      this.skillRare.gameObject.SetActive(false);
    if ((UnityEngine.Object) this.portraitDrop != (UnityEngine.Object) null && this.portraitDrop.gameObject.activeSelf)
      this.portraitDrop.transform.gameObject.SetActive(false);
    if (this.cardData.CardUpgraded == Enums.CardUpgraded.No)
    {
      if (!this.titleTextT.gameObject.activeSelf)
        this.titleTextT.gameObject.SetActive(true);
      this.titleTextTM.text = this.cardData.CardName;
      if (this.titleTextTBlue.gameObject.activeSelf)
        this.titleTextTBlue.gameObject.SetActive(false);
      if (this.titleTextTGold.gameObject.activeSelf)
        this.titleTextTGold.gameObject.SetActive(false);
      if (this.titleTextTRed.gameObject.activeSelf)
        this.titleTextTRed.gameObject.SetActive(false);
      if (this.titleTextTPurple.gameObject.activeSelf)
        this.titleTextTPurple.gameObject.SetActive(false);
    }
    else if (this.cardData.CardUpgraded == Enums.CardUpgraded.A)
    {
      if (!this.titleTextTBlue.gameObject.activeSelf)
        this.titleTextTBlue.gameObject.SetActive(true);
      this.titleTextTMBlue.text = !(this.cardData.UpgradedFrom.Trim() != "") ? "" : (!((UnityEngine.Object) Globals.Instance.GetCardData(this.cardData.UpgradedFrom, false) != (UnityEngine.Object) null) ? "" : this.cardData.CardName);
      if (this.titleTextT.gameObject.activeSelf)
        this.titleTextT.gameObject.SetActive(false);
      if (this.titleTextTGold.gameObject.activeSelf)
        this.titleTextTGold.gameObject.SetActive(false);
      if (this.titleTextTRed.gameObject.activeSelf)
        this.titleTextTRed.gameObject.SetActive(false);
      if (this.titleTextTPurple.gameObject.activeSelf)
        this.titleTextTPurple.gameObject.SetActive(false);
    }
    else if (this.cardData.CardUpgraded == Enums.CardUpgraded.B)
    {
      if (!this.titleTextTGold.gameObject.activeSelf)
        this.titleTextTGold.gameObject.SetActive(true);
      this.titleTextTMGold.text = !(this.cardData.UpgradedFrom.Trim() != "") ? "" : (!((UnityEngine.Object) Globals.Instance.GetCardData(this.cardData.UpgradedFrom, false) != (UnityEngine.Object) null) ? "" : this.cardData.CardName);
      if (this.titleTextT.gameObject.activeSelf)
        this.titleTextT.gameObject.SetActive(false);
      if (this.titleTextTBlue.gameObject.activeSelf)
        this.titleTextTBlue.gameObject.SetActive(false);
      if (this.titleTextTRed.gameObject.activeSelf)
        this.titleTextTRed.gameObject.SetActive(false);
      if (this.titleTextTPurple.gameObject.activeSelf)
        this.titleTextTPurple.gameObject.SetActive(false);
    }
    else if (this.cardData.CardUpgraded == Enums.CardUpgraded.Rare)
    {
      if (!this.titleTextTPurple.gameObject.activeSelf)
        this.titleTextTPurple.gameObject.SetActive(true);
      this.titleTextTMPurple.text = this.cardData.CardName;
      if (this.titleTextT.gameObject.activeSelf)
        this.titleTextT.gameObject.SetActive(false);
      if (this.titleTextTGold.gameObject.activeSelf)
        this.titleTextTGold.gameObject.SetActive(false);
      if (this.titleTextTBlue.gameObject.activeSelf)
        this.titleTextTBlue.gameObject.SetActive(false);
      if (this.titleTextTRed.gameObject.activeSelf)
        this.titleTextTRed.gameObject.SetActive(false);
    }
    if (this.cardData.Innate)
    {
      if (!this.innateIconParticle.gameObject.activeSelf)
        this.innateIconParticle.gameObject.SetActive(true);
      if (!this.innateT.gameObject.activeSelf)
        this.innateT.gameObject.SetActive(true);
    }
    else
    {
      if (this.innateIconParticle.gameObject.activeSelf)
        this.innateIconParticle.gameObject.SetActive(false);
      if (this.innateT.gameObject.activeSelf)
        this.innateT.gameObject.SetActive(false);
    }
    if (this.cardData.Vanish)
    {
      if (!this.vanishIconParticle.gameObject.activeSelf)
        this.vanishIconParticle.gameObject.SetActive(true);
      if (!this.vanishT.gameObject.activeSelf)
        this.vanishT.gameObject.SetActive(true);
    }
    else
    {
      if (this.vanishIconParticle.gameObject.activeSelf)
        this.vanishIconParticle.gameObject.SetActive(false);
      if (this.vanishT.gameObject.activeSelf)
        this.vanishT.gameObject.SetActive(false);
    }
    this.ShowRarity(this.cardData);
    if (this.cardData.CardRarity == Enums.CardRarity.Mythic)
    {
      if (!this.cardBorderMythic.gameObject.activeSelf)
        this.cardBorderMythic.gameObject.SetActive(true);
    }
    else if (this.cardBorderMythic.gameObject.activeSelf)
      this.cardBorderMythic.gameObject.SetActive(false);
    this.descriptionTextTM.text = this.cardData.DescriptionNormalized;
    this.NormalizeHeight(this.descriptionTextTM, this.cardData.Item);
    if (this.cardData.CardClass != Enums.CardClass.Item)
    {
      if (!this.targetTextT.gameObject.activeSelf)
        this.targetTextT.gameObject.SetActive(true);
      if (!this.targetT.gameObject.activeSelf)
        this.targetT.gameObject.SetActive(true);
      if (!this.requireTextT.gameObject.activeSelf)
        this.requireTextT.gameObject.SetActive(true);
      if (!this.typeText.gameObject.activeSelf)
        this.typeText.gameObject.SetActive(true);
      if (!this.typeTextImage.gameObject.activeSelf)
        this.typeTextImage.gameObject.SetActive(true);
      this.targetTextTM.text = this.cardData.Target;
    }
    else
    {
      if (this.targetTextT.gameObject.activeSelf)
        this.targetTextT.gameObject.SetActive(false);
      if (this.targetT.gameObject.activeSelf)
        this.targetT.gameObject.SetActive(false);
      if (this.requireTextT.gameObject.activeSelf)
        this.requireTextT.gameObject.SetActive(false);
      if (this.typeText.gameObject.activeSelf)
        this.typeText.gameObject.SetActive(false);
      if (this.typeTextImage.gameObject.activeSelf)
        this.typeTextImage.gameObject.SetActive(false);
      this.itemData = this.cardData.Item;
      if ((UnityEngine.Object) this.itemData != (UnityEngine.Object) null && this.itemData.CursedItem)
      {
        if (this.titleTextT.gameObject.activeSelf)
          this.titleTextT.gameObject.SetActive(false);
        if (this.titleTextTPurple.gameObject.activeSelf)
          this.titleTextTPurple.gameObject.SetActive(false);
        if (!this.titleTextTRed.gameObject.activeSelf)
          this.titleTextTRed.gameObject.SetActive(true);
        this.titleTextTMRed.text = this.cardData.CardName;
      }
    }
    string[] strArray = this.cardData.Id.Split('_', StringSplitOptions.None);
    if (strArray != null && strArray[0] == "success")
    {
      if (this.targetTextT.gameObject.activeSelf)
        this.targetTextT.gameObject.SetActive(false);
      if (this.targetT.gameObject.activeSelf)
        this.targetT.gameObject.SetActive(false);
    }
    if (this.cardData.CardType != Enums.CardType.None && this.cardData.CardClass != Enums.CardClass.Item)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Texts.Instance.GetText(Enum.GetName(typeof (Enums.CardType), (object) this.cardData.CardType)));
      if (this.cardData.CardTypeAux.Length != 0)
        stringBuilder.Append(" <size=-.2>[</size><size=+.1>...</size><size=-.2>]</size>");
      this.typeTextTM.text = stringBuilder.ToString();
      this.typeTextImage.gameObject.SetActive(true);
    }
    else
    {
      this.typeTextTM.text = "";
      if (this.typeTextImage.gameObject.activeSelf)
        this.typeTextImage.gameObject.SetActive(false);
    }
    if (this.cardData.CardClass != Enums.CardClass.Item)
    {
      string str = "";
      bool flag = false;
      if (this.cardData.CardClass != Enums.CardClass.Monster)
      {
        str = this.cardData.GetRequireText();
        if (str != "")
          flag = true;
      }
      else
      {
        if (_theNPC != null)
          str = _theNPC.GetCardPriorityText(this.cardData.Id);
        else if (this.theNPC != null)
          str = this.theNPC.GetCardPriorityText(this.cardData.Id);
        if (str != "")
        {
          this.requireTextTM.color = Functions.HexToColor("#5E0016");
          flag = true;
        }
      }
      this.requireTextTM.text = str;
      if (!flag)
      {
        this.descriptionTextTM.margin = new Vector4(0.02f, 0.16f, 0.02f, -0.0f);
        if (this.cardData.CardClass == Enums.CardClass.Monster)
          this.descriptionTextTM.margin = new Vector4(0.02f, 0.16f, 0.02f, -0.04f);
        else if (this.cardData.CardType == Enums.CardType.Enchantment)
          this.descriptionTextTM.margin = new Vector4(0.02f, 0.14f, 0.02f, -0.04f);
      }
      else
      {
        this.descriptionTextTM.margin = new Vector4(0.02f, 0.28f, 0.02f, 0.0f);
        if (this.cardData.CardType == Enums.CardType.Enchantment)
          this.descriptionTextTM.margin = new Vector4(0.02f, 0.28f, 0.02f, -0.04f);
      }
    }
    else
      this.descriptionTextTM.margin = new Vector4(0.02f, -0.02f, 0.02f, -0.04f);
    if (this.energyTextItemBg.gameObject.activeSelf)
      this.energyTextItemBg.gameObject.SetActive(false);
    if (this.cardData.Playable)
    {
      if (_generated && _theHero != null && _theHero.HasEffect("Exhaust"))
        this.cardData.DoExhaust();
      this.DrawEnergyCost();
    }
    else
    {
      if (this.energyText.gameObject.activeSelf)
        this.energyText.gameObject.SetActive(false);
      if (this.energyTextBg.gameObject.activeSelf)
        this.energyTextBg.gameObject.SetActive(false);
      if (this.cardData.CardClass == Enums.CardClass.Item)
      {
        if (!this.energyTextItemBg.gameObject.activeSelf)
          this.energyTextItemBg.gameObject.SetActive(true);
        for (int index = 0; index < 5; ++index)
        {
          if (this.energyTextItemBg.GetChild(index).gameObject.activeSelf)
            this.energyTextItemBg.GetChild(index).gameObject.SetActive(false);
        }
        if (this.cardData.CardType == Enums.CardType.Weapon)
        {
          if (!this.energyTextItemBg.GetChild(0).gameObject.activeSelf)
            this.energyTextItemBg.GetChild(0).gameObject.SetActive(true);
        }
        else if (this.cardData.CardType == Enums.CardType.Armor)
        {
          if (!this.energyTextItemBg.GetChild(1).gameObject.activeSelf)
            this.energyTextItemBg.GetChild(1).gameObject.SetActive(true);
        }
        else if (this.cardData.CardType == Enums.CardType.Jewelry)
        {
          if (!this.energyTextItemBg.GetChild(2).gameObject.activeSelf)
            this.energyTextItemBg.GetChild(2).gameObject.SetActive(true);
        }
        else if (this.cardData.CardType == Enums.CardType.Pet)
        {
          if (!this.energyTextItemBg.GetChild(4).gameObject.activeSelf)
            this.energyTextItemBg.GetChild(4).gameObject.SetActive(true);
        }
        else if (!this.energyTextItemBg.GetChild(3).gameObject.activeSelf)
          this.energyTextItemBg.GetChild(3).gameObject.SetActive(true);
      }
    }
    if (deckScale)
      this.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
    if (this.cardData.CardType == Enums.CardType.Corruption)
    {
      if (this.targetTextT.gameObject.activeSelf)
        this.targetTextT.gameObject.SetActive(false);
      if (this.targetT.gameObject.activeSelf)
        this.targetT.gameObject.SetActive(false);
      if (this.requireTextT.gameObject.activeSelf)
        this.requireTextT.gameObject.SetActive(false);
      if (this.typeText.gameObject.activeSelf)
        this.typeText.gameObject.SetActive(false);
      if (this.typeTextImage.gameObject.activeSelf)
        this.typeTextImage.gameObject.SetActive(false);
      this.descriptionTextT.localPosition = new Vector3(this.descriptionTextT.localPosition.x, -0.63f, this.descriptionTextT.localPosition.z);
      this.descriptionTextTM.margin = new Vector4(0.02f, -0.02f, 0.02f, -0.04f);
    }
    this.CreateColliderHand();
    if (!((UnityEngine.Object) this.descriptionTextTM != (UnityEngine.Object) null))
      return;
    string str1 = Functions.StripTagsString(this.descriptionTextTM.text);
    if (str1.Length < 20)
      this.descriptionTextTM.fontSizeMax = 1.8f;
    else if (str1.Length < 50)
      this.descriptionTextTM.fontSizeMax = 1.6f;
    else
      this.descriptionTextTM.fontSizeMax = 1.45f;
  }

  public void ShowRarity(CardData _cardData)
  {
    if (this.rarityUncommonParticle.gameObject.activeSelf)
      this.rarityUncommonParticle.gameObject.SetActive(false);
    if (this.rarityRareParticle.gameObject.activeSelf)
      this.rarityRareParticle.gameObject.SetActive(false);
    if (this.rarityEpicParticle.gameObject.activeSelf)
      this.rarityEpicParticle.gameObject.SetActive(false);
    if (this.rarityMythicParticle.gameObject.activeSelf)
      this.rarityMythicParticle.gameObject.SetActive(false);
    if (_cardData.CardRarity == Enums.CardRarity.Common)
    {
      this.borderSprite.sprite = GameManager.Instance.cardBorderSprites[0];
      this.energyBg.sprite = GameManager.Instance.cardEnergySprites[0];
    }
    else if (_cardData.CardRarity == Enums.CardRarity.Uncommon)
    {
      this.borderSprite.sprite = GameManager.Instance.cardBorderSprites[1];
      this.energyBg.sprite = GameManager.Instance.cardEnergySprites[1];
      this.rarityUncommonParticle.gameObject.SetActive(true);
    }
    else if (_cardData.CardRarity == Enums.CardRarity.Rare)
    {
      this.energyBg.sprite = GameManager.Instance.cardEnergySprites[2];
      this.borderSprite.sprite = GameManager.Instance.cardBorderSprites[2];
      this.rarityRareParticle.gameObject.SetActive(true);
    }
    else if (_cardData.CardRarity == Enums.CardRarity.Epic)
    {
      this.energyBg.sprite = GameManager.Instance.cardEnergySprites[3];
      this.borderSprite.sprite = GameManager.Instance.cardBorderSprites[3];
      this.rarityEpicParticle.gameObject.SetActive(true);
    }
    else
    {
      if (_cardData.CardRarity != Enums.CardRarity.Mythic)
        return;
      this.energyBg.sprite = GameManager.Instance.cardEnergySprites[4];
      this.borderSprite.sprite = GameManager.Instance.cardBorderSprites[4];
      this.rarityMythicParticle.gameObject.SetActive(true);
    }
  }

  public void HideRarityParticles()
  {
    if (!((UnityEngine.Object) this.cardData != (UnityEngine.Object) null))
      return;
    if (this.cardData.CardRarity == Enums.CardRarity.Uncommon)
    {
      if (!this.rarityUncommonParticle.gameObject.activeSelf)
        return;
      this.rarityUncommonParticle.gameObject.SetActive(false);
    }
    else if (this.cardData.CardRarity == Enums.CardRarity.Rare)
    {
      if (!this.rarityRareParticle.gameObject.activeSelf)
        return;
      this.rarityRareParticle.gameObject.SetActive(false);
    }
    else if (this.cardData.CardRarity == Enums.CardRarity.Epic)
    {
      if (!this.rarityEpicParticle.gameObject.activeSelf)
        return;
      this.rarityEpicParticle.gameObject.SetActive(false);
    }
    else
    {
      if (this.cardData.CardRarity != Enums.CardRarity.Mythic || !this.rarityMythicParticle.gameObject.activeSelf)
        return;
      this.rarityMythicParticle.gameObject.SetActive(false);
    }
  }

  public void DoReward(
    bool fromReward = true,
    bool fromEvent = false,
    bool fromLoot = false,
    bool selectable = true,
    float modspeed = 1f)
  {
    this.ShowBackImage(true);
    this.SetLocalScale(new Vector3(0.0f, 0.0f, 1f));
    this.active = true;
    this.cardoutsidecombat = true;
    this.cardoutsidereward = true;
    this.TopLayeringOrder("Book");
    this.HideRarityParticles();
    this.HideCardIconParticles();
    this.SetDestinationScaleRotation(this.transform.localPosition, 0.8f, this.transform.localRotation);
    if (fromReward | fromLoot && this.cardData.CardRarity != Enums.CardRarity.Common)
    {
      Color color;
      if (this.cardData.CardRarity == Enums.CardRarity.Uncommon)
      {
        color = Globals.Instance.RarityColor["uncommon"];
        color = new Color(color.r, color.g, color.b, 0.1f);
      }
      else if (this.cardData.CardRarity == Enums.CardRarity.Rare)
      {
        color = Globals.Instance.RarityColor["rare"];
        color = new Color(color.r, color.g, color.b, 0.3f);
      }
      else if (this.cardData.CardRarity == Enums.CardRarity.Epic)
      {
        color = Globals.Instance.RarityColor["epic"];
        color = new Color(color.r, color.g, color.b, 0.6f);
      }
      else
      {
        color = Globals.Instance.RarityColor["mythic"];
        color = new Color(color.r, color.g, color.b, 0.7f);
      }
      this.backParticle.main.startColor = (ParticleSystem.MinMaxGradient) color;
      this.backParticleSpark.main.startColor = (ParticleSystem.MinMaxGradient) color;
      this.backParticleT.gameObject.SetActive(true);
      this.backParticle.Play();
      this.backParticleSpark.Play();
    }
    this.StartCoroutine(this.DoRewardCo(fromReward, fromEvent, fromLoot, selectable, modspeed));
  }

  private IEnumerator DoRewardCo(
    bool fromReward,
    bool fromEvent,
    bool fromLoot,
    bool selectable,
    float modspeed)
  {
    CardItem cardItem = this;
    if (fromEvent)
      GameManager.Instance.PlayLibraryAudio("dealcard");
    yield return (object) Globals.Instance.WaitForSeconds(0.2f * modspeed);
    if (fromReward)
    {
      if (cardItem.cardData.CardUpgraded != Enums.CardUpgraded.No)
      {
        GameManager.Instance.PlayLibraryAudio("ui_cardupgrade");
        cardItem.cardUpgradeT.gameObject.SetActive(true);
      }
      yield return (object) Globals.Instance.WaitForSeconds(1.6f * modspeed);
    }
    if (fromEvent)
      yield return (object) Globals.Instance.WaitForSeconds(1.8f * modspeed);
    cardItem.active = false;
    cardItem.ShowBackImage(false);
    cardItem.DisableTrail();
    cardItem.backImageT.gameObject.SetActive(true);
    cardItem.backImageT.GetComponent<Animator>().enabled = true;
    cardItem.cardElementsT.GetComponent<Animator>().enabled = true;
    if (fromReward | fromLoot)
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.1f * modspeed);
      int cardRarity = (int) cardItem.cardData.CardRarity;
      GameManager.Instance.PlayLibraryAudio("castnpccard");
    }
    if ((UnityEngine.Object) RewardsManager.Instance != (UnityEngine.Object) null)
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      cardItem.CreateColliderAdjusted();
    }
    else if (!((UnityEngine.Object) EventManager.Instance != (UnityEngine.Object) null) || !(cardItem.gameObject.name == "EventRollCard"))
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.2f);
      cardItem.CreateColliderAdjusted();
    }
    if (fromEvent)
    {
      yield return (object) Globals.Instance.WaitForSeconds(0.1f * modspeed);
      cardItem.PlayDissolveParticle();
    }
    if (!selectable)
      cardItem.DisableCollider();
  }

  public void TurnBack()
  {
    this.cardElementsT.GetComponent<Animator>().enabled = true;
    this.cardElementsT.GetComponent<Animator>().SetTrigger("turnBack");
    this.backImageT.GetComponent<Animator>().enabled = true;
    this.backImageT.GetComponent<Animator>().SetTrigger("turnBack");
    this.DisableCollider();
    if (this.backParticleT.gameObject.activeSelf)
      this.backParticleT.gameObject.SetActive(false);
    this.StartCoroutine(this.TurnBackCo());
  }

  private IEnumerator TurnBackCo()
  {
    yield return (object) Globals.Instance.WaitForSeconds(1f);
    this.ShowBackImage(true);
  }

  public void ShowUnlocked(bool showEffects = true)
  {
    this.HideLock();
    if (!showEffects)
      return;
    GameManager.Instance.PlayLibraryAudio("ui_cardupgrade", 0.05f);
    this.cardUnlockT.gameObject.SetActive(true);
    this.PlaySightParticle();
  }

  public void ShowLockedBackground(bool status) => this.lockShadowImage.gameObject.SetActive(status);

  public void HideCardIconParticles()
  {
    if (this.innateIconParticle.gameObject.activeSelf)
      this.innateIconParticle.gameObject.SetActive(false);
    if (!this.vanishIconParticle.gameObject.activeSelf)
      return;
    this.vanishIconParticle.gameObject.SetActive(false);
  }

  public void ShowBackImage(bool state)
  {
    this.backImageT.gameObject.SetActive(state);
    this.cardElementsT.gameObject.SetActive(!state);
    this.trailParticlesNPC.gameObject.SetActive(!state);
    if (state)
      return;
    this.HideCardIconParticles();
    this.HideRarityParticles();
  }

  public void ShowDisable(bool state)
  {
    if (!((UnityEngine.Object) this.disableT != (UnityEngine.Object) null) || this.disableT.gameObject.activeSelf == state)
      return;
    this.disableT.gameObject.SetActive(state);
  }

  public void ShowDisableReward()
  {
    this.disableT.gameObject.SetActive(true);
    Color color = this.disableT.GetComponent<SpriteRenderer>().color;
    this.disableT.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.35f);
  }

  public void ShowDiscardImage()
  {
    this.backImageT.gameObject.SetActive(false);
    this.cardSpriteT.gameObject.SetActive(true);
    this.skillImageT.gameObject.SetActive(true);
    this.descriptionTextT.gameObject.SetActive(true);
    this.energyTextTM.gameObject.SetActive(false);
    this.energyTextBg.gameObject.SetActive(false);
    this.typeText.gameObject.SetActive(false);
    this.typeTextImage.gameObject.SetActive(false);
    this.trailParticlesNPC.gameObject.SetActive(false);
    this.titleTextT.gameObject.SetActive(false);
    this.titleTextTMBlue.gameObject.SetActive(false);
    this.titleTextTMGold.gameObject.SetActive(false);
  }

  public void RevealCard()
  {
    this.cardrevealed = true;
    this.cardSize = this.cardScaleNPC;
    this.RedrawDescriptionPrecalculatedNPC(this.theNPC);
    this.ShowBackImage(false);
    this.PlaySightParticle();
  }

  public bool IsRevealed() => this.cardrevealed;

  public void SetTablePositionValues(int _position, int _totalCards)
  {
    this.tablePosition = _position;
    this.tableCards = _totalCards;
    if (this.discard)
      return;
    this.DefaultElementsLayeringOrder();
  }

  public Vector3 GetDestination() => this.destinationLocalPosition;

  public void SetDestination(Vector3 position) => this.destinationLocalPosition = position;

  public void SetLocalPosition(Vector3 position) => this.transform.localPosition = position;

  public void SetDestinationScaleRotation(Vector3 position, float scale, Quaternion rotation)
  {
    this.destinationLocalPosition = position;
    this.cardSize = scale;
    this.destinationLocalRotation = rotation;
  }

  public void SetDestinationLocalScale(float scale) => this.cardSize = scale;

  public void SetLocalScale(Vector3 scale) => this.transform.localScale = scale;

  public void PositionCardInTable()
  {
    float num1 = (float) ((double) this.tableCards * 0.5 - 0.5);
    float f = Mathf.Abs(num1 - (float) this.tablePosition);
    float num2 = this.cardSizeTable + 0.4f;
    int num3 = 5;
    if (this.tableCards > num3)
    {
      float num4 = (float) (this.tableCards - num3) * 0.14f;
      num2 -= num4;
    }
    float num5 = 0.0f;
    if ((double) Mathf.Floor(f) > 2.0)
    {
      if ((double) this.tablePosition < (double) num1)
        num5 -= f * 0.5f;
      else
        num5 += f * 0.5f;
    }
    float x = (float) (1.2000000476837158 + -(double) num2 * ((double) this.tableCards - (double) this.tablePosition * 1.1000000238418579));
    float y = (float) (-1.0 * ((double) f * 0.05000000074505806 + ((double) f - 1.0) * 0.0099999997764825821 * (double) f));
    if (MatchManager.Instance.GameStatus == "BeginTurnHero" || !this.discard)
    {
      Quaternion.Euler(0.0f, 0.0f, 0.0f);
      this.destinationLocalRotation = this.initialLocalRotation = Quaternion.Euler(0.0f, 0.0f, (float) (3.0 * ((double) num1 - (double) this.tablePosition)) + num5);
      this.destinationLocalPosition = this.initialLocalPosition = new Vector3(x, y, (float) (1.0 - (double) this.tablePosition * 0.0099999997764825821));
      this.cardSize = this.cardSizeTable;
    }
    if (MatchManager.Instance.GameStatus != "BeginTurnHero" && !this.active && !this.discard)
      this.active = true;
    this.EnableCollider();
  }

  public void AmplifySetEnergy()
  {
    this.CardChildSorting("Default", 20000);
    this.destinationLocalPosition = Vector3.zero - new Vector3(3.5f, 0.5f, 0.0f) - GameObject.Find("GOs/Hand").transform.localPosition;
    this.destinationLocalRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    this.cardSize = 1.4f;
    this.HideEnergyBorder();
  }

  public void AmplifyForSelection(int index, int total)
  {
    this.CardChildSorting("Default", 20000, 100 * -index);
    this.CreateColliderAdjusted(0.2f, 0.15f);
    Vector3 vector3_1 = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 vector3_2 = vector3_1;
    float y1 = vector3_1.y;
    float num1 = vector3_1.y + 1.6f;
    float num2 = vector3_1.y - 1.6f;
    float num3 = 2.3f;
    int num4 = 0;
    if (total > 10)
    {
      num4 = Mathf.FloorToInt((float) index / 10f);
      total = 10;
      index %= 10;
    }
    float y2 = num2 - (float) num4 * 6.4f;
    float y3 = num1 - (float) num4 * 6.4f;
    switch (total)
    {
      case 2:
        vector3_2 = index != 0 ? new Vector3(vector3_1.x + num3 * 0.5f, y1) : new Vector3(vector3_1.x - num3 * 0.5f, y1);
        break;
      case 3:
        switch (index)
        {
          case 0:
            vector3_2 = new Vector3(vector3_1.x - num3, y1);
            break;
          case 1:
            vector3_2 = vector3_1;
            break;
          default:
            vector3_2 = new Vector3(vector3_1.x + num3, y1);
            break;
        }
        break;
      case 4:
        switch (index)
        {
          case 0:
            vector3_2 = new Vector3(vector3_1.x - num3 * 0.5f, y3);
            break;
          case 1:
            vector3_2 = new Vector3(vector3_1.x + num3 * 0.5f, y3);
            break;
          case 2:
            vector3_2 = new Vector3(vector3_1.x - num3 * 0.5f, y2);
            break;
          default:
            vector3_2 = new Vector3(vector3_1.x + num3 * 0.5f, y2);
            break;
        }
        break;
      case 5:
        switch (index)
        {
          case 0:
            vector3_2 = new Vector3(vector3_1.x - num3, y3);
            break;
          case 1:
            vector3_2 = new Vector3(vector3_1.x, y3);
            break;
          case 2:
            vector3_2 = new Vector3(vector3_1.x + num3, y3);
            break;
          case 3:
            vector3_2 = new Vector3(vector3_1.x - num3 * 0.5f, y2);
            break;
          default:
            vector3_2 = new Vector3(vector3_1.x + num3 * 0.5f, y2);
            break;
        }
        break;
      case 6:
        switch (index)
        {
          case 0:
            vector3_2 = new Vector3(vector3_1.x - 2.2f, y3);
            break;
          case 1:
            vector3_2 = new Vector3(vector3_1.x, y3);
            break;
          case 2:
            vector3_2 = new Vector3(vector3_1.x + 2.2f, y3);
            break;
          case 3:
            vector3_2 = new Vector3(vector3_1.x - 2.2f, y2);
            break;
          case 4:
            vector3_2 = new Vector3(vector3_1.x, y2);
            break;
          default:
            vector3_2 = new Vector3(vector3_1.x + 2.2f, y2);
            break;
        }
        break;
      case 7:
        switch (index)
        {
          case 0:
            vector3_2 = new Vector3(vector3_1.x - (num3 * 0.5f + num3), y3);
            break;
          case 1:
            vector3_2 = new Vector3(vector3_1.x - num3 * 0.5f, y3);
            break;
          case 2:
            vector3_2 = new Vector3(vector3_1.x + num3 * 0.5f, y3);
            break;
          case 3:
            vector3_2 = new Vector3(vector3_1.x + num3 * 0.5f + num3, y3);
            break;
          case 4:
            vector3_2 = new Vector3(vector3_1.x - num3, y2);
            break;
          case 5:
            vector3_2 = new Vector3(vector3_1.x, y2);
            break;
          default:
            vector3_2 = new Vector3(vector3_1.x + num3, y2);
            break;
        }
        break;
      case 8:
        switch (index)
        {
          case 0:
            vector3_2 = new Vector3(vector3_1.x - (num3 * 0.5f + num3), y3);
            break;
          case 1:
            vector3_2 = new Vector3(vector3_1.x - num3 * 0.5f, y3);
            break;
          case 2:
            vector3_2 = new Vector3(vector3_1.x + num3 * 0.5f, y3);
            break;
          case 3:
            vector3_2 = new Vector3(vector3_1.x + num3 * 0.5f + num3, y3);
            break;
          case 4:
            vector3_2 = new Vector3(vector3_1.x - (num3 * 0.5f + num3), y2);
            break;
          case 5:
            vector3_2 = new Vector3(vector3_1.x - num3 * 0.5f, y2);
            break;
          case 6:
            vector3_2 = new Vector3(vector3_1.x + num3 * 0.5f, y2);
            break;
          default:
            vector3_2 = new Vector3(vector3_1.x + num3 * 0.5f + num3, y2);
            break;
        }
        break;
      case 9:
        switch (index)
        {
          case 0:
            vector3_2 = new Vector3(vector3_1.x - num3 * 2f, y3);
            break;
          case 1:
            vector3_2 = new Vector3(vector3_1.x - num3, y3);
            break;
          case 2:
            vector3_2 = new Vector3(vector3_1.x, y3);
            break;
          case 3:
            vector3_2 = new Vector3(vector3_1.x + num3, y3);
            break;
          case 4:
            vector3_2 = new Vector3(vector3_1.x + num3 * 2f, y3);
            break;
          case 5:
            vector3_2 = new Vector3(vector3_1.x - (num3 * 0.5f + num3), y2);
            break;
          case 6:
            vector3_2 = new Vector3(vector3_1.x - num3 * 0.5f, y2);
            break;
          case 7:
            vector3_2 = new Vector3(vector3_1.x + num3 * 0.5f, y2);
            break;
          default:
            vector3_2 = new Vector3(vector3_1.x + num3 * 0.5f + num3, y2);
            break;
        }
        break;
      case 10:
        switch (index)
        {
          case 0:
            vector3_2 = new Vector3(vector3_1.x - num3 * 2f, y3);
            break;
          case 1:
            vector3_2 = new Vector3(vector3_1.x - num3, y3);
            break;
          case 2:
            vector3_2 = new Vector3(vector3_1.x, y3);
            break;
          case 3:
            vector3_2 = new Vector3(vector3_1.x + num3, y3);
            break;
          case 4:
            vector3_2 = new Vector3(vector3_1.x + num3 * 2f, y3);
            break;
          case 5:
            vector3_2 = new Vector3(vector3_1.x - num3 * 2f, y2);
            break;
          case 6:
            vector3_2 = new Vector3(vector3_1.x - num3, y2);
            break;
          case 7:
            vector3_2 = new Vector3(vector3_1.x, y2);
            break;
          case 8:
            vector3_2 = new Vector3(vector3_1.x + num3, y2);
            break;
          default:
            vector3_2 = new Vector3(vector3_1.x + num3 * 2f, y2);
            break;
        }
        break;
    }
    vector3_2 = new Vector3(vector3_2.x, vector3_2.y, (float) ((double) index * -0.0099999997764825821 - 3.0));
    this.destinationLocalPosition = vector3_2;
    this.destinationLocalRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    this.cardSize = this.cardSizeTable;
    this.HideEnergyBorder();
    this.ShowDisable(false);
  }

  public void EnableDisableDiscardAction(bool state, bool iconSkull)
  {
    this.cardselectedfordiscard = state;
    if (state)
    {
      if (iconSkull)
      {
        this.DrawSkull(true);
        this.DrawBorder("red");
      }
      else
      {
        this.DrawCheck(true);
        this.DrawBorder("green");
      }
    }
    else
    {
      this.DrawSkull(false);
      this.DrawCheck(false);
      this.DrawBorder("");
    }
  }

  public void EnableDisableAddcardAction(bool state)
  {
    this.cardselectedforaddcard = state;
    if (state)
    {
      this.DrawBorder("green");
      this.DrawCheck(true);
    }
    else
    {
      this.DrawBorder("");
      this.DrawCheck(false);
      this.DrawSkull(false);
    }
  }

  private IEnumerator OnStopFloating(Vector3 destination)
  {
    CardItem cardItem = this;
    Vector3 position = cardItem.transform.localPosition;
    while ((double) Vector3.Distance(position, destination) > 0.10000000149011612)
    {
      position = cardItem.transform.localPosition;
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    }
    cardItem.floating.enabled = true;
  }

  public void CenterToDiscard()
  {
    this.discard = true;
    this.active = false;
    this.trailParticlesNPC.gameObject.SetActive(true);
    this.CardChildSorting("Default", MatchManager.Instance.CountHeroDiscard() * 20 + 100);
    this.DrawSkull(true);
    this.destinationLocalPosition = new Vector3(this.transform.localPosition.x, 2f, this.transform.localPosition.z);
    this.destinationLocalRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
  }

  public bool CardIsPrediscarding() => this.prediscard && this.discard;

  public void PreDiscardCard()
  {
    this.RemoveEmotes();
    this.ShowKeyNum(false);
    if (this.tablePosition == -1)
      return;
    if (this.floatingCoroutine != null)
      this.StopCoroutine(this.floatingCoroutine);
    this.floating.enabled = false;
    this.DisableCollider();
    this.prediscard = true;
    this.discard = true;
    this.active = false;
    this.casting = false;
    this.transform.parent = MatchManager.Instance.tempTransform;
    this.destinationLocalPosition = this.transform.localPosition + (MatchManager.Instance.GetDiscardPileTransform().position - this.transform.position) + new Vector3(2.5f, 0.0f, 0.0f);
    this.destinationLocalRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    this.cardSize = 0.8f;
    this.CardChildSorting("Default", (MatchManager.Instance.CountHeroDiscard() + MatchManager.Instance.NumChildsInTemporal()) * 100);
    this.DrawBorder("blue");
  }

  public void MoveCardToDeckPile()
  {
    this.transform.parent = MatchManager.Instance.GetDeckPileTransform();
    this.destinationLocalPosition = MatchManager.Instance.GetDeckPilePosition();
    this.destinationLocalRotation = Quaternion.Euler(0.0f, 0.0f, 180f);
    this.cardSize = 0.0f;
    this.EnableTrail();
    int num = (MatchManager.Instance.CountHeroDiscard() - this.tablePosition) * 100;
    if (num != 0)
      this.CardChildSorting(_position: num - 30000);
    this.destroyAtLocation = true;
  }

  public void DiscardCard(bool discardedFromHand, Enums.CardPlace whereToDiscard = Enums.CardPlace.Discard, int auxIndex = -1)
  {
    if ((UnityEngine.Object) this.gameObject == (UnityEngine.Object) null || (UnityEngine.Object) this.transform == (UnityEngine.Object) null)
      return;
    this.RemoveEmotes();
    this.ShowKeyNum(false);
    this.DisableCollider();
    if (this.tablePosition <= -1)
      return;
    if (this.floatingCoroutine != null)
      this.StopCoroutine(this.floatingCoroutine);
    if ((UnityEngine.Object) this.floating != (UnityEngine.Object) null)
      this.floating.enabled = false;
    this.prediscard = false;
    this.discard = true;
    this.active = false;
    this.casting = false;
    this.cardSize = 0.75f;
    this.destinationLocalRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    int sortingOrderDiscard = this.sortingOrderDiscard;
    if (this.cardVanish)
      whereToDiscard = Enums.CardPlace.Vanish;
    switch (whereToDiscard)
    {
      case Enums.CardPlace.Discard:
        this.transform.parent = MatchManager.Instance.GetDiscardPileTransform();
        if ((UnityEngine.Object) this.transform.parent == (UnityEngine.Object) null)
          this.transform.parent = MatchManager.Instance.tempTransform;
        this.SetDiscardSortingOrder(auxIndex);
        this.destinationLocalPosition = MatchManager.Instance.GetDiscardPilePosition();
        MatchManager.Instance.RedoDiscardPileDepth();
        break;
      case Enums.CardPlace.TopDeck:
      case Enums.CardPlace.BottomDeck:
      case Enums.CardPlace.RandomDeck:
        if (whereToDiscard != Enums.CardPlace.RandomDeck)
          this.cardData.Visible = true;
        this.MoveCardToDeckPile();
        this.CardChildSorting(_position: sortingOrderDiscard);
        break;
      case Enums.CardPlace.Vanish:
        this.transform.parent = MatchManager.Instance.GetWorldTransform();
        if (this.trailParticlesNPC.gameObject.activeSelf)
          this.trailParticlesNPC.gameObject.SetActive(false);
        this.StartCoroutine(this.VanishToZero());
        break;
    }
    this.DrawBorder("");
    this.HideRarityParticles();
    this.HideCardIconParticles();
    if (this.skullImageT.gameObject.activeSelf)
      this.skullImageT.gameObject.SetActive(false);
    if (this.checkImageT.gameObject.activeSelf)
      this.checkImageT.gameObject.SetActive(false);
    if (discardedFromHand)
      MatchManager.Instance.DiscardCard(this.tablePosition, whereToDiscard);
    this.tablePosition = -1;
  }

  public IEnumerator VanishToZero()
  {
    CardItem cardItem = this;
    if ((UnityEngine.Object) cardItem.gameObject != (UnityEngine.Object) null && cardItem.gameObject.activeSelf)
    {
      cardItem.lockPosition = true;
      cardItem.PlayVanishParticle();
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
      cardItem.SetDestinationLocalScale(0.0f);
      yield return (object) Globals.Instance.WaitForSeconds(1f);
      if ((bool) (UnityEngine.Object) MatchManager.Instance)
      {
        cardItem.transform.parent = MatchManager.Instance.tempVanishedTransform;
        cardItem.StartCoroutine(cardItem.SelfDestruct(3f));
      }
      else
        cardItem.StartCoroutine(cardItem.SelfDestruct(2f));
    }
  }

  public void Vanish()
  {
    this.PlayVanishParticle();
    if ((bool) (UnityEngine.Object) MatchManager.Instance)
    {
      this.transform.parent = MatchManager.Instance.tempVanishedTransform;
      this.StartCoroutine(this.SelfDestruct(10f));
    }
    else
      this.StartCoroutine(this.SelfDestruct(2f));
  }

  public void PlayDissolveParticle()
  {
    this.dissolveParticleT.gameObject.SetActive(true);
    this.dissolveParticle.Play();
  }

  public void PlayVanishParticle()
  {
    GameManager.Instance.PlayLibraryAudio("vanish_woosh", 0.5f);
    this.vanishParticle.gameObject.SetActive(false);
    this.vanishParticle.gameObject.SetActive(true);
  }

  public void PlaySightParticle()
  {
    if (this.sightParticle.gameObject.activeSelf)
      this.sightParticle.gameObject.SetActive(false);
    if (!this.cardoutsidelibary && !this.cardoutsidereward)
      this.sightParticle.transform.localScale = new Vector3(1f, 1f, 1f);
    else
      this.sightParticle.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
    this.sightParticle.gameObject.SetActive(true);
  }

  public void RedrawCardsDamageType(Hero theHero)
  {
  }

  public void RedrawDescriptionPrecalculated(Hero theHero, bool includeInSearch = true)
  {
    if (theHero == null)
      return;
    Enums.DamageType modifiedDamageType1 = theHero.GetItemModifiedDamageType();
    Enums.DamageType modifiedDamageType2 = theHero.GetEnchantModifiedDamageType();
    Enums.DamageType dt = Enums.DamageType.None;
    if (modifiedDamageType2 != Enums.DamageType.None)
      dt = modifiedDamageType2;
    else if (modifiedDamageType1 != Enums.DamageType.None)
      dt = modifiedDamageType1;
    this.cardData.ModifyDamageType(dt);
    this.cardData.SetDamagePrecalculated(theHero.DamageWithCharacterBonus(this.cardData.Damage, this.cardData.DamageType, this.cardData.CardClass));
    this.cardData.SetDamagePrecalculated2(theHero.DamageWithCharacterBonus(this.cardData.Damage2, this.cardData.DamageType2, this.cardData.CardClass));
    this.cardData.SetDamageSelfPrecalculated(theHero.DamageWithCharacterBonus(this.cardData.DamageSelf, this.cardData.DamageType, this.cardData.CardClass));
    this.cardData.SetDamageSelfPrecalculated2(theHero.DamageWithCharacterBonus(this.cardData.DamageSelf2, this.cardData.DamageType, this.cardData.CardClass));
    this.cardData.SetDamageSidesPrecalculated(theHero.DamageWithCharacterBonus(this.cardData.DamageSides, this.cardData.DamageType, this.cardData.CardClass));
    this.cardData.SetDamageSidesPrecalculated2(theHero.DamageWithCharacterBonus(this.cardData.DamageSides2, this.cardData.DamageType2, this.cardData.CardClass));
    this.cardData.SetHealPrecalculated(theHero.HealWithCharacterBonus(this.cardData.Heal, this.cardData.CardClass));
    this.cardData.SetHealSelfPrecalculated(theHero.HealWithCharacterBonus(this.cardData.HealSelf, this.cardData.CardClass));
    if ((UnityEngine.Object) this.cardData.ItemEnchantment != (UnityEngine.Object) null)
      this.cardData.SetEnchantDamagePrecalculated(theHero.DamageWithCharacterBonus(this.cardData.ItemEnchantment.DamageToTarget, this.cardData.ItemEnchantment.DamageToTargetType, this.cardData.CardClass));
    else if ((UnityEngine.Object) this.cardData.Item != (UnityEngine.Object) null)
      this.cardData.SetEnchantDamagePrecalculated(theHero.DamageWithCharacterBonus(this.cardData.Item.DamageToTarget, this.cardData.Item.DamageToTargetType, this.cardData.CardClass));
    this.cardData.SetDescriptionNew(true, (Character) theHero, includeInSearch);
    this.descriptionTextTM.text = this.cardData.DescriptionNormalized;
    this.NormalizeHeight(this.descriptionTextTM, this.cardData.Item);
    this.DrawEnergyCost();
  }

  public void NormalizeHeight(TMP_Text textField, ItemData item)
  {
    textField.ForceMeshUpdate();
    double preferredHeight = (double) textField.preferredHeight;
    string input = textField.text;
    StringBuilder stringBuilder = new StringBuilder();
    if (preferredHeight < 0.5 && (UnityEngine.Object) item == (UnityEngine.Object) null)
      input = new Regex(" <nobr>").Replace(input, "<br><nobr>", 1);
    stringBuilder.Append(input);
    textField.text = stringBuilder.ToString();
  }

  public int GetEnergyCost()
  {
    int energyCost = this.theHero == null ? (this.theNPC == null ? this.cardData.GetCardFinalCost() : this.theNPC.GetCardFinalCost(this.cardData)) : this.theHero.GetCardFinalCost(this.cardData);
    if (energyCost < 0)
      energyCost = 0;
    return energyCost;
  }

  public void DrawEnergyCost(bool ActualCost = true)
  {
    if (!this.cardData.Playable)
      return;
    this.energyText.gameObject.SetActive(true);
    this.energyTextBg.gameObject.SetActive(true);
    int num;
    if (ActualCost)
    {
      num = this.GetEnergyCost();
      this.energyTextTM.text = num.ToString();
    }
    else
    {
      num = this.cardData.EnergyCostForShow;
      this.energyTextTM.text = num.ToString();
    }
    if (num == this.GetEnergyCost() && (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && MatchManager.Instance.WaitingForActionScreen())
      this.energyTextTM.color = Globals.Instance.ColorColor["white"];
    else if (num < this.cardData.EnergyCostOriginal)
      this.energyTextTM.color = Globals.Instance.ColorColor["greenCard"];
    else if (num > this.cardData.EnergyCostOriginal)
      this.energyTextTM.color = Globals.Instance.ColorColor["redCard"];
    else
      this.energyTextTM.color = Globals.Instance.ColorColor["white"];
  }

  public void ShowEnergyModification(int value)
  {
    this.energyModificationTM.text = value.ToString();
    if (value < 0)
      this.energyModificationTM.color = Globals.Instance.ColorColor["greenCard"];
    else
      this.energyModificationTM.color = Globals.Instance.ColorColor["redCard"];
    this.energyModification.gameObject.SetActive(false);
    this.energyModification.gameObject.SetActive(true);
  }

  public bool IsPlayable()
  {
    if ((UnityEngine.Object) this.cardData != (UnityEngine.Object) null && !this.cardData.Playable)
      return false;
    if (this.theHero != null)
      return this.theHero.CanPlayCard(this.cardData);
    return this.theNPC != null && this.theNPC.CanPlayCard(this.cardData);
  }

  public bool IsPlayableRightNow() => this.IsPlayable() && this.active && this.GetEnergyCost() <= MatchManager.Instance.GetHeroEnergy() && MatchManager.Instance.IsThereAnyTargetForCard(this.cardData);

  public void DrawEnergyBorder()
  {
    if ((UnityEngine.Object) this.cardData == (UnityEngine.Object) null)
      return;
    this.ShowDisable(false);
    if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && (MatchManager.Instance.MatchIsOver || MatchManager.Instance.GameStatus == "DrawingCards"))
    {
      this.DrawBorder("");
    }
    else
    {
      if (this.cardData.CardClass == Enums.CardClass.Injury || this.cardData.CardClass == Enums.CardClass.Boon)
        return;
      if (this.IsPlayable() && this.active && this.GetEnergyCost() <= MatchManager.Instance.GetHeroEnergy())
      {
        if (MatchManager.Instance.IsThereAnyTargetForCard(this.cardData))
        {
          if (this.cardData.LookCards > 0 && this.cardData.LookCards > MatchManager.Instance.CountHeroDeck())
            this.DrawBorder("orange");
          else
            this.DrawBorder("green");
        }
        else
          this.DrawBorder("red");
      }
      else
      {
        if (this.theHero != null && this.active)
        {
          if (!this.IsPlayable())
            this.DrawBorder("red");
          else
            this.DrawBorder("");
        }
        else
          this.DrawBorder("");
        if (this.IsPlayable() && this.theHero != null && this.active && this.GetEnergyCost() > MatchManager.Instance.GetHeroEnergy())
          this.ShowDisable(true);
      }
      if (!(this.cardData.EffectRequired != "") || this.theHero == null)
        return;
      if (!this.theHero.HasEffect(this.cardData.EffectRequired))
      {
        if (this.cardData.EffectRequired == "stanzai")
        {
          if (!this.theHero.HasEffect("stanzaii") && !this.theHero.HasEffect("stanzaiii"))
            this.requireTextTM.color = Functions.HexToColor("#910303");
          else
            this.requireTextTM.color = Functions.HexToColor("#1E650F");
        }
        else if (this.cardData.EffectRequired == "stanzaii")
        {
          if (!this.theHero.HasEffect("stanzaiii"))
            this.requireTextTM.color = Functions.HexToColor("#910303");
          else
            this.requireTextTM.color = Functions.HexToColor("#1E650F");
        }
        else
          this.requireTextTM.color = Functions.HexToColor("#910303");
      }
      else
        this.requireTextTM.color = Functions.HexToColor("#1E650F");
    }
  }

  public void DrawSkull(bool state) => this.skullImageT.gameObject.SetActive(state);

  public void DrawCheck(bool state) => this.checkImageT.gameObject.SetActive(state);

  public void DrawBorder(string color)
  {
    if (color != "blue" && color != "" && (UnityEngine.Object) this.disableT != (UnityEngine.Object) null && this.disableT.gameObject.activeSelf || (UnityEngine.Object) this.cardBorder == (UnityEngine.Object) null || (UnityEngine.Object) this.cardBorderSR == (UnityEngine.Object) null)
      return;
    if (color == "")
    {
      if (!this.cardBorder.gameObject.activeSelf)
        return;
      this.cardBorder.gameObject.SetActive(false);
    }
    else
    {
      if (!this.cardBorder.gameObject.activeSelf)
        this.cardBorder.gameObject.SetActive(true);
      switch (color)
      {
        case "green":
          if (GameManager.Instance.IsMultiplayer() && (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && !MatchManager.Instance.IsYourTurn())
          {
            this.cardBorderSR.color = this.orangeColor;
            break;
          }
          this.cardBorderSR.color = this.greenColor;
          break;
        case "red":
          this.cardBorderSR.color = this.redColor;
          break;
        case "blue":
          this.cardBorderSR.color = this.blueColor;
          break;
        case "orange":
          this.cardBorderSR.color = this.orangeColor;
          break;
        case "black":
          this.cardBorderSR.color = this.blackColor;
          break;
        case "purple":
          this.cardBorderSR.color = this.purpleColor;
          break;
      }
    }
  }

  public void HideEnergyBorder() => this.DrawBorder("");

  public void AdjustPositionBecauseHover(int cardAmplifiedPosition, bool open, int totalCards)
  {
    if (open)
    {
      float num = (float) Mathf.Abs(cardAmplifiedPosition - this.tablePosition);
      Vector3 vector3 = new Vector3((float) (0.40000000596046448 * (1.0 / (double) num)), 0.0f, 0.0f);
      if (this.tablePosition < cardAmplifiedPosition)
      {
        float x = (float) (0.012000000104308128 * (double) Mathf.Abs(totalCards - 5) * (9.0 - (double) num));
        this.destinationLocalPosition = this.initialLocalPosition - vector3 - new Vector3(x, 0.0f, 0.0f);
      }
      else
      {
        float x = (float) (0.017999999225139618 * (double) Mathf.Abs(totalCards - 5) * (9.0 - (double) num));
        this.destinationLocalPosition = this.initialLocalPosition + vector3 + new Vector3(x, 0.0f, 0.0f);
      }
    }
    else
      this.destinationLocalPosition = this.initialLocalPosition;
  }

  public void AmplifyCard()
  {
    MatchManager.Instance.SetCardHover(this.tablePosition, true);
    int _position = 10000;
    if ((bool) (UnityEngine.Object) MatchManager.Instance)
      this.CardChildSorting("Book", _position);
    else
      this.CardChildSorting("", _position);
    if (this.transformVerticalDesplazament == Vector3.zero)
      this.transformVerticalDesplazament = new Vector3(0.0f, this.cardCollider.bounds.size.y * 0.25f, -1f);
    this.destinationLocalPosition = this.initialLocalPosition + this.transformVerticalDesplazament;
    this.destinationLocalRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    this.cardSize = this.cardSizeAmplified;
  }

  public void RestoreCard()
  {
    if (this.floatingCoroutine != null)
      this.StopCoroutine(this.floatingCoroutine);
    if ((UnityEngine.Object) this.floating != (UnityEngine.Object) null)
      this.floating.enabled = false;
    this.DefaultElementsLayeringOrder();
    this.cardSize = this.cardSizeTable;
    this.destinationLocalPosition = this.initialLocalPosition;
    this.destinationLocalRotation = this.initialLocalRotation;
    MatchManager.Instance.SetCardHover(this.tablePosition, false);
    this.EnableDisableAddcardAction(false);
    this.EnableDisableDiscardAction(false, false);
    if (!MatchManager.Instance.controllerClickedCard && MatchManager.Instance.PreCastNum == -1 && !((UnityEngine.Object) MatchManager.Instance.CardActive != (UnityEngine.Object) null))
      MatchManager.Instance.SetTarget((Transform) null);
    this.cursorArrow.StopDraw();
    this.DrawEnergyBorder();
    if (!MatchManager.Instance.controllerClickedCard)
      return;
    MatchManager.Instance.ResetController();
  }

  public void SetDiscardSortingOrder(int index = -1)
  {
    int num = this.sortingOrderDiscard;
    Transform discardPileTransform = MatchManager.Instance.GetDiscardPileTransform();
    if (discardPileTransform.childCount > 1)
    {
      Transform child = discardPileTransform.GetChild(discardPileTransform.childCount - 2);
      if ((UnityEngine.Object) child != (UnityEngine.Object) null)
      {
        foreach (Component component1 in child)
        {
          SpriteRenderer component2 = component1.GetComponent<SpriteRenderer>();
          if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          {
            num = component2.sortingOrder;
            break;
          }
        }
      }
    }
    int _offset = 100;
    this.CardChildSorting("Discards", index == -1 ? num : num + index * _offset, _offset);
  }

  public void SetColorArrow(string theColor) => this.cursorArrow.SetColor(theColor);

  public void PositionCardInNPC(int order, int total)
  {
    float num1 = (float) ((double) total * 0.5 - 0.5);
    double num2 = (double) Mathf.Abs(num1 - (float) order);
    this.destinationLocalRotation = this.initialLocalRotation = Quaternion.Euler(0.0f, 0.0f, (float) (-6.0 * ((double) num1 - (double) order)));
    if (total > 4)
      this.cardDistanceNPC *= 1f;
    else if (total > 3)
      this.cardDistanceNPC *= 0.85f;
    float x = this.cardDistanceNPC * (num1 - (float) order);
    float y = (float) (num2 * num2 * -0.019999999552965164);
    Transform transform = this.transform;
    Vector3 vector3_1 = new Vector3(x, y, (float) (1.0 + (double) order * 0.10000000149011612));
    Vector3 vector3_2 = vector3_1;
    transform.localPosition = vector3_2;
    this.destinationLocalPosition = this.initialLocalPosition = vector3_1;
    this.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
  }

  public void ShowCardNPC(int iteration)
  {
    this.gameObject.SetActive(true);
    this.RedrawDescriptionPrecalculatedNPC(this.theNPC);
    this.StartCoroutine(this.ShowCardNPCCo(iteration));
  }

  private IEnumerator ShowCardNPCCo(int iteration)
  {
    yield return (object) Globals.Instance.WaitForSeconds(0.2f * (float) iteration);
    this.cardSize = this.cardScaleNPC;
    this.discard = true;
    yield return (object) Globals.Instance.WaitForSeconds(0.5f);
    this.discard = false;
  }

  public void RedrawDescriptionPrecalculatedNPC(NPC theNPC)
  {
    if (theNPC == null || (UnityEngine.Object) this.cardData == (UnityEngine.Object) null)
      return;
    Enums.DamageType modifiedDamageType1 = theNPC.GetItemModifiedDamageType();
    Enums.DamageType modifiedDamageType2 = theNPC.GetEnchantModifiedDamageType();
    Enums.DamageType dt = Enums.DamageType.None;
    if (modifiedDamageType2 != Enums.DamageType.None)
      dt = modifiedDamageType2;
    else if (modifiedDamageType1 != Enums.DamageType.None)
      dt = modifiedDamageType1;
    this.cardData.ModifyDamageType(dt);
    int energyCost = this.GetEnergyCost();
    this.cardData.SetDamagePrecalculated(theNPC.DamageWithCharacterBonus(this.cardData.Damage, this.cardData.DamageType, this.cardData.CardClass, energyCost));
    this.cardData.SetDamagePrecalculated2(theNPC.DamageWithCharacterBonus(this.cardData.Damage2, this.cardData.DamageType2, this.cardData.CardClass, energyCost));
    this.cardData.SetDamageSelfPrecalculated(theNPC.DamageWithCharacterBonus(this.cardData.DamageSelf, this.cardData.DamageType, this.cardData.CardClass, energyCost));
    this.cardData.SetDamageSelfPrecalculated2(theNPC.DamageWithCharacterBonus(this.cardData.DamageSelf2, this.cardData.DamageType, this.cardData.CardClass, energyCost));
    this.cardData.SetDamageSidesPrecalculated(theNPC.DamageWithCharacterBonus(this.cardData.DamageSides, this.cardData.DamageType, this.cardData.CardClass, energyCost));
    this.cardData.SetDamageSidesPrecalculated2(theNPC.DamageWithCharacterBonus(this.cardData.DamageSides2, this.cardData.DamageType2, this.cardData.CardClass, energyCost));
    this.cardData.SetHealPrecalculated(theNPC.HealWithCharacterBonus(this.cardData.Heal, this.cardData.CardClass, energyCost));
    this.cardData.SetHealSelfPrecalculated(theNPC.HealWithCharacterBonus(this.cardData.HealSelf, this.cardData.CardClass, energyCost));
    if ((UnityEngine.Object) this.cardData.ItemEnchantment != (UnityEngine.Object) null)
      this.cardData.SetEnchantDamagePrecalculated(theNPC.DamageWithCharacterBonus(this.cardData.ItemEnchantment.DamageToTarget, this.cardData.ItemEnchantment.DamageToTargetType, this.cardData.CardClass, energyCost));
    else if ((UnityEngine.Object) this.cardData.Item != (UnityEngine.Object) null)
      this.cardData.SetEnchantDamagePrecalculated(theNPC.DamageWithCharacterBonus(this.cardData.Item.DamageToTarget, this.cardData.Item.DamageToTargetType, this.cardData.CardClass, energyCost));
    this.cardData.SetDescriptionNew(true, (Character) theNPC);
    this.descriptionTextTM.text = this.cardData.DescriptionNormalized;
    this.NormalizeHeight(this.descriptionTextTM, this.cardData.Item);
  }

  public void CastCardNPC(Transform theTransform)
  {
    this.cardCollider.enabled = false;
    this.RemoveAmplifyNewCard();
    this.StartCoroutine(this.CastCardNPCCo(theTransform));
  }

  private IEnumerator CastCardNPCCo(Transform theTransform)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    CardItem cardItem = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      cardItem.cardnpc = false;
      cardItem.destinationLocalRotation = Quaternion.Euler(0.0f, 0.0f, 179f);
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    cardItem.cardrevealed = false;
    cardItem.RedrawDescriptionPrecalculatedNPC(cardItem.theNPC);
    Vector3 vector3 = cardItem.transform.parent.localPosition + cardItem.transform.parent.transform.parent.localPosition;
    cardItem.destinationLocalPosition = -vector3 - new Vector3(0.0f, 2.1f, 0.0f);
    cardItem.destinationLocalRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    cardItem.cardSize = 1.4f;
    cardItem.DrawBorder("orange");
    cardItem.NPCTargetPosition = theTransform.localPosition + new Vector3(0.0f, vector3.y * 0.5f, 0.0f);
    cardItem.TopLayeringOrder("Book");
    cardItem.cardnpc = true;
    cardItem.ShowBackImage(false);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) Globals.Instance.WaitForSeconds(0.5f);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  public void DiscardCardNPC(int i)
  {
    if (!this.gameObject.activeSelf)
      return;
    this.StartCoroutine(this.DiscardCardNPCCo(i));
  }

  private IEnumerator DiscardCardNPCCo(int i)
  {
    CardItem cardItem = this;
    yield return (object) Globals.Instance.WaitForSeconds(0.1f * (float) i);
    cardItem.discard = true;
    cardItem.cardSize = 0.0f;
    yield return (object) Globals.Instance.WaitForSeconds(0.3f);
    UnityEngine.Object.Destroy((UnityEngine.Object) cardItem.gameObject);
  }

  public void EnableTrail() => this.trailParticlesNPC.gameObject.SetActive(true);

  public void DisableTrail()
  {
    this.trailParticlesNPC.Stop();
    this.trailParticlesNPC.gameObject.SetActive(false);
  }

  public IEnumerator DrawArrowRemote(Vector3 ori, Vector3 dest)
  {
    // ISSUE: reference to a compiler-generated field
    int num1 = this.\u003C\u003E1__state;
    CardItem cardItem = this;
    if (num1 != 0)
      return false;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    if ((UnityEngine.Object) cardItem.gameObject == (UnityEngine.Object) null)
      return false;
    cardItem.cursorArrow.SetColor("gold");
    float num2 = Mathf.Clamp(Mathf.Abs(ori.x - dest.x) * 0.01f, 4f, 6f);
    Vector3 vector3_1 = new Vector3(cardItem.transform.position.x + (float) (((double) ori.x - (double) dest.x) * 9.9999997473787516E-06), cardItem.transform.position.y + 0.5f, -10f);
    Vector3 vector3_2 = new Vector3(cardItem.cursorArrow.point1.x, cardItem.cursorArrow.point3.y + num2, 0.0f);
    Vector3 vector3_3 = new Vector3(dest.x, dest.y, 5f);
    cardItem.cursorArrow.point1 = vector3_1;
    cardItem.cursorArrow.point2 = vector3_2;
    cardItem.cursorArrow.point3 = vector3_3;
    cardItem.cursorArrow.StartDraw(MatchManager.Instance.CanInstaCast(cardItem.cardData));
    return false;
  }

  public void DrawArrow(Vector3 ori, Vector3 dest)
  {
    float num = Mathf.Clamp(Mathf.Abs(ori.x - dest.x) * 0.01f, 4f, 6f);
    this.cursorArrow.point1 = new Vector3(this.transform.position.x + (float) (((double) ori.x - (double) dest.x) * 9.9999997473787516E-06), this.transform.position.y + 0.5f, 10f);
    this.cursorArrow.point2 = new Vector3(this.cursorArrow.point1.x, this.cursorArrow.point3.y + num, 0.0f);
    this.cursorArrow.point3 = new Vector3(dest.x, dest.y, 5f);
    this.cursorArrow.StartDraw(MatchManager.Instance.CanInstaCast(this.cardData));
  }

  private void CreateAmplifyNewCard(Vector3 oriPosition)
  {
    oriPosition -= new Vector3(2f, 1f, 0.0f);
    MatchManager.Instance.amplifiedTransformShow = true;
    this.cardAmplifyNPC = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, oriPosition, Quaternion.identity, MatchManager.Instance.amplifiedTransform);
    this.cardAmplifyNPC.transform.localScale = Vector3.zero;
    this.cardAmplifyNPC.name = this.internalId;
    CardItem component = this.cardAmplifyNPC.GetComponent<CardItem>();
    component.SetCard(this.cardData.InternalId, false, this.theHero, this.theNPC);
    component.discard = true;
    component.RedrawDescriptionPrecalculatedNPC(this.theNPC);
    component.DisableTrail();
    if ((bool) (UnityEngine.Object) MatchManager.Instance)
      component.CardChildSorting("UI", 30000);
    else
      component.CardChildSorting("UI", 15000);
    component.SetDestinationScaleRotation(oriPosition, this.cardSizeTable + 0.2f, Quaternion.Euler(0.0f, 0.0f, 0.0f));
    component.ShowKeyNotes(position: "left");
  }

  private void CreateAmplifyRelatedCard(string cardId, Transform theTransform)
  {
    if ((UnityEngine.Object) this.relatedCard != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.relatedCard);
    this.relatedCard = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, theTransform.position + new Vector3(0.0f, 0.0f, 1f), Quaternion.identity, theTransform);
    this.relatedCard.gameObject.SetActive(false);
    CardItem component = this.relatedCard.GetComponent<CardItem>();
    component.SetCard(cardId, false, GetFromGlobal: true);
    component.DisableCollider();
    component.DisableTrail();
    component.TopLayeringOrder("UI", 32000);
    component.ShowRelated();
    if ((double) theTransform.position.y > -1.0)
      component.SetDestinationScaleRotation(new Vector3(0.0f, -2.6f, 0.0f), 0.8f, Quaternion.Euler(0.0f, 0.0f, 0.0f));
    else
      component.SetDestinationScaleRotation(new Vector3(0.0f, 2.5f, 0.0f), 0.8f, Quaternion.Euler(0.0f, 0.0f, 0.0f));
    component.discard = true;
    this.relatedCard.gameObject.SetActive(true);
  }

  public void CreateAmplifyOutsideCard(CardData _cardData = null, BoxCollider2D collider = null, Hero hero = null)
  {
    if ((bool) (UnityEngine.Object) CardPlayerManager.Instance || (bool) (UnityEngine.Object) CardPlayerPairsManager.Instance)
      return;
    if ((UnityEngine.Object) _cardData != (UnityEngine.Object) null)
      this.cardData = _cardData;
    if (hero == null)
      hero = this.theHero;
    Vector3 worldPoint = GameManager.Instance.cameraMain.ScreenToWorldPoint(Input.mousePosition);
    if (this.cardoutsidelibary)
    {
      if ((double) worldPoint.x > 0.0)
        worldPoint.x -= 2f;
      else
        worldPoint.x += 2f;
    }
    else if (this.cardoutsidereward)
      worldPoint.x += 2.5f;
    else if (this.cardoutsideverticallist)
    {
      if (CardScreenManager.Instance.IsActive())
        worldPoint.x = -8.05f;
      else if ((UnityEngine.Object) collider != (UnityEngine.Object) null)
      {
        Vector2 size = collider.size;
        Vector3 vector3 = new Vector3(collider.offset.x, collider.offset.y, 0.0f);
        float num = -1f;
        if ((bool) (UnityEngine.Object) CardCraftManager.Instance && CardCraftManager.Instance.craftType == 5)
          num = -2f;
        worldPoint.x = (double) this.transform.position.x <= (double) num ? (float) ((double) this.transform.TransformPoint((Vector3) collider.offset).x + (double) collider.size.x * 0.5 + (double) this.cardCollider.size.x * 0.5 + 0.10000000149011612) : (float) ((double) this.transform.TransformPoint((Vector3) collider.offset).x - (double) collider.size.x * 0.5 - (double) this.cardCollider.size.x * 0.5);
        if ((bool) (UnityEngine.Object) ChallengeSelectionManager.Instance)
          worldPoint.y += 2f;
        else if ((bool) (UnityEngine.Object) EventManager.Instance)
        {
          worldPoint.y -= 2.5f;
          worldPoint.x += 0.7f;
        }
      }
    }
    else
      worldPoint += new Vector3(0.0f, 4f, 0.0f);
    if ((UnityEngine.Object) this.cardAmplifyOutside == (UnityEngine.Object) null)
    {
      if ((UnityEngine.Object) CardCraftManager.Instance != (UnityEngine.Object) null)
      {
        Transform transform = CardCraftManager.Instance.cardListContainer.Find("CardAmplifyOutside");
        if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
          UnityEngine.Object.Destroy((UnityEngine.Object) transform.gameObject);
        this.cardAmplifyOutside = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, worldPoint, Quaternion.identity, GameManager.Instance.TempContainer);
      }
      else
      {
        Transform transform = GameManager.Instance.TempContainer.Find("CardAmplifyOutside");
        if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
          UnityEngine.Object.Destroy((UnityEngine.Object) transform.gameObject);
        this.cardAmplifyOutside = UnityEngine.Object.Instantiate<GameObject>(GameManager.Instance.CardPrefab, worldPoint, Quaternion.identity, GameManager.Instance.TempContainer);
      }
    }
    this.cardAmplifyOutside.name = "CardAmplifyOutside";
    CardItem component = this.cardAmplifyOutside.GetComponent<CardItem>();
    component.SetCard(this.cardData.InternalId, false, hero, this.theNPC, true);
    component.TopLayeringOrder("UI", 31600);
    component.cardoutsidelibary = this.cardoutsidelibary;
    component.cardoutsideselection = this.cardoutsideselection;
    component.cardoutsidereward = this.cardoutsidereward;
    component.cardoutsideverticallist = this.cardoutsideverticallist;
    component.DisableTrail();
    if (this.cardoutsidelibary)
    {
      component.SetDestinationScaleRotation(worldPoint, 1.2f, Quaternion.Euler(0.0f, 0.0f, 0.0f));
      component.ShowKeyNotes(true);
    }
    else if (this.cardoutsidereward)
    {
      component.SetDestinationScaleRotation(worldPoint, 1.4f, Quaternion.Euler(0.0f, 0.0f, 0.0f));
      component.ShowKeyNotes(true);
      if (this.gameObject.name == "EventRollCard")
        component.DisableCollider();
    }
    else if (this.cardoutsideverticallist)
    {
      component.SetDestinationScaleRotation(worldPoint, 1.4f, Quaternion.Euler(0.0f, 0.0f, 0.0f));
      if (!CardScreenManager.Instance.IsActive())
      {
        if ((double) this.transform.position.x > -2.0)
          component.ShowKeyNotes(true, "followleft");
        else
          component.ShowKeyNotes(true);
      }
    }
    else
    {
      component.SetDestinationScaleRotation(worldPoint, 1.6f, Quaternion.Euler(0.0f, 0.0f, 0.0f));
      component.ShowKeyNotes(true);
    }
    component.cardoutsidecombatamplified = true;
    if (!(this.cardData.RelatedCard != ""))
      return;
    this.CreateAmplifyRelatedCard(this.cardData.RelatedCard, component.transform);
  }

  public void ShowKeyNotes(bool followCardPosition = false, string position = "right")
  {
    if ((UnityEngine.Object) this.backImageT != (UnityEngine.Object) null && this.backImageT.gameObject.activeSelf && this.backImageT.GetComponent<SpriteRenderer>().enabled || (UnityEngine.Object) this.cardData == (UnityEngine.Object) null)
      return;
    if ((UnityEngine.Object) this.rightClick != (UnityEngine.Object) null && this.cardData.RelatedCard == "" && !CardScreenManager.Instance.IsActive() && (TomeManager.Instance.IsActive() || (bool) (UnityEngine.Object) RewardsManager.Instance || (bool) (UnityEngine.Object) HeroSelectionManager.Instance))
      this.rightClick.gameObject.SetActive(true);
    if (followCardPosition)
    {
      Vector3 worldPoint = GameManager.Instance.cameraMain.ScreenToWorldPoint(Input.mousePosition);
      if (position != "followleft" && position != "followright")
        position = (double) worldPoint.x <= 0.0 ? "followright" : "followleft";
    }
    if (this.cardoutsidereward)
    {
      position = "followright";
      if ((UnityEngine.Object) ChallengeSelectionManager.Instance != (UnityEngine.Object) null && (double) GameManager.Instance.cameraMain.ScreenToWorldPoint(Input.mousePosition).x > 5.0)
        position = "followleft";
    }
    PopupManager.Instance.SetCard(this.transform, this.cardData, this.cardData.KeyNotes, position);
  }

  public void HideKeyNotes()
  {
    if ((UnityEngine.Object) this.rightClick != (UnityEngine.Object) null && this.rightClick.gameObject.activeSelf)
      this.rightClick.gameObject.SetActive(false);
    if (CardScreenManager.Instance.IsActive())
      return;
    PopupManager.Instance.ClosePopup();
  }

  public void RemoveAmplifyNewCard()
  {
    if (!((UnityEngine.Object) this.cardAmplifyNPC != (UnityEngine.Object) null))
      return;
    this.DrawBorder("");
    this.cardSize = this.cardScaleNPC;
    UnityEngine.Object.Destroy((UnityEngine.Object) this.cardAmplifyNPC);
    this.cardAmplifyNPC = (GameObject) null;
  }

  public void ShowBorderParticle()
  {
    this.DrawBorder("blue");
    this.borderParticle.GetComponent<ParticleSystemRenderer>().sortingOrder = this.cardBorderSR.sortingOrder - 1;
    this.borderParticle.gameObject.SetActive(true);
    this.borderParticle.Clear();
    this.borderParticle.Play();
  }

  private IEnumerator RevealedCoroutine()
  {
    CardItem cardItem = this;
    if (cardItem.revealedCoroutine == null)
      yield return (object) Globals.Instance.WaitForSeconds(0.1f);
    if (!CardScreenManager.Instance.IsActive())
    {
      cardItem.DrawBorder("blue");
      cardItem.CreateAmplifyNewCard(cardItem.transform.position);
      cardItem.revealedCoroutine = (Coroutine) null;
    }
  }

  private IEnumerator RevealedOutsideCoroutine()
  {
    this.DrawBorder("blue");
    if (TomeManager.Instance.IsActive())
      TomeManager.Instance.ShowCardsMask(true);
    this.CreateAmplifyOutsideCard();
    yield return (object) null;
  }

  public void DestroyReveleadOutside()
  {
    this.HideKeyNotes();
    UnityEngine.Object.Destroy((UnityEngine.Object) this.cardAmplifyOutside);
    this.cardAmplifyOutside = (GameObject) null;
  }

  public void HideReveleadOutside()
  {
    this.DrawBorder("");
    if (TomeManager.Instance.IsActive())
      TomeManager.Instance.ShowCardsMask(false);
    this.DestroyReveleadOutside();
  }

  public void DoReturnCardToDeckFromDrag()
  {
    MatchManager.Instance.CardDrag = false;
    MatchManager.Instance.CardActiveT = (Transform) null;
    MatchManager.Instance.SetDamagePreview(false);
    MatchManager.Instance.SetOverDeck(false);
    this.cursorArrow.StopDraw();
    if (GameManager.Instance.IsMultiplayer() && MatchManager.Instance.IsYourTurn())
      MatchManager.Instance.StopArrowNet(this.tablePosition);
    this.RestoreCard();
    if (GameManager.Instance.IsMultiplayer() && (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && MatchManager.Instance.IsYourTurn())
      MatchManager.Instance.AmplifyCardOut(this.tablePosition);
    Cursor.visible = true;
  }

  public void OnMouseOver()
  {
    if ((UnityEngine.Object) this.cardData == (UnityEngine.Object) null || SettingsManager.Instance.IsActive() || AlertManager.Instance.IsActive() || MadnessManager.Instance.IsActive() || SandboxManager.Instance.IsActive() || (bool) (UnityEngine.Object) MatchManager.Instance && MatchManager.Instance.console.IsActive() && (UnityEngine.Object) this.transform.parent != (UnityEngine.Object) MatchManager.Instance.console.transform || (UnityEngine.Object) this.backImageT != (UnityEngine.Object) null && this.backImageT.gameObject.activeSelf && this.backImageT.GetComponent<SpriteRenderer>().enabled && !TomeManager.Instance.IsActive())
      return;
    if (Input.GetMouseButtonUp(1))
      this.RightClick();
    if (!((UnityEngine.Object) this.cardAmplifyNPC != (UnityEngine.Object) null))
      return;
    MatchManager.Instance.amplifiedTransformShow = true;
  }

  public void RightClick()
  {
    if ((UnityEngine.Object) this.cardData == (UnityEngine.Object) null || PerkTree.Instance.IsActive() && !TomeManager.Instance.IsActive() || (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && (MatchManager.Instance.CardDrag || MatchManager.Instance.waitingDeathScreen) || (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && (MatchManager.Instance.DeckCardsWindow.IsActive() || MatchManager.Instance.EnergySelector.IsActive() || MatchManager.Instance.WaitingForActionScreen()) && this.destinationLocalPosition != this.transform.localPosition)
      return;
    if (this.cardData.CardClass == Enums.CardClass.Item && (UnityEngine.Object) CardCraftManager.Instance != (UnityEngine.Object) null)
      CardCraftManager.Instance.HoverItem(false, this.cardData.CardType);
    if (!((UnityEngine.Object) CardScreenManager.Instance != (UnityEngine.Object) null))
      return;
    if (TomeManager.Instance.IsActive())
      TomeManager.Instance.ShowCardsMask(false);
    CardScreenManager.Instance.ShowCardScreen(true);
    CardScreenManager.Instance.SetCardData(this.cardData);
    this.DrawBorder("");
    if (this.cardforaddcard || this.cardfordiscard)
      return;
    if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && this.active && !MatchManager.Instance.characterWindow.IsActive() && !MatchManager.Instance.WaitingForCardEnergyAssignment)
      this.fOnMouseExit();
    if (this.revealedCoroutine != null)
      this.StopCoroutine(this.revealedCoroutine);
    this.RemoveAmplifyNewCard();
    UnityEngine.Object.Destroy((UnityEngine.Object) this.cardAmplifyOutside);
    UnityEngine.Object.Destroy((UnityEngine.Object) this.cardAmplifyNPC);
    this.cardAmplifyNPC = (GameObject) null;
    if ((UnityEngine.Object) this.relatedCard != (UnityEngine.Object) null)
      UnityEngine.Object.Destroy((UnityEngine.Object) this.relatedCard);
    if (!this.cardmakebig)
      return;
    this.SetDestinationLocalScale(this.cardmakebigSize);
    this.HideKeyNotes();
  }

  public void OnMouseEnter()
  {
    if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && (UnityEngine.Object) this.transform.parent == (UnityEngine.Object) MatchManager.Instance.tempTransform && ((UnityEngine.Object) MatchManager.Instance.CardActive == (UnityEngine.Object) null || MatchManager.Instance.CardActive.InternalId != this.InternalId))
    {
      if (Globals.Instance.ShowDebug)
        Functions.DebugLogGD("Destroyed because card is temporal", "general");
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      this.HideKeyNotes();
    }
    else
      this.fOnMouseEnterMaster();
  }

  private void fOnMouseEnterMaster()
  {
    if ((UnityEngine.Object) this.cardData == (UnityEngine.Object) null || SettingsManager.Instance.IsActive() || AlertManager.Instance.IsActive() || (bool) (UnityEngine.Object) MatchManager.Instance && MatchManager.Instance.console.IsActive() || CardScreenManager.Instance.IsActive() || (bool) (UnityEngine.Object) CardPlayerManager.Instance && !CardPlayerManager.Instance.CanClick() || (bool) (UnityEngine.Object) CardPlayerPairsManager.Instance && !CardPlayerPairsManager.Instance.CanClick() || PerkTree.Instance.IsActive() && !TomeManager.Instance.IsActive() || (bool) (UnityEngine.Object) MatchManager.Instance && MatchManager.Instance.PreCastNum > -1 && this.tablePosition != MatchManager.Instance.PreCastNum - 1)
      return;
    if ((bool) (UnityEngine.Object) MatchManager.Instance && MatchManager.Instance.GameStatus == "" && this.cardData.CardType == Enums.CardType.Enchantment && (UnityEngine.Object) this.transform.parent == (UnityEngine.Object) MatchManager.Instance.tempTransform && ((UnityEngine.Object) MatchManager.Instance.CardActive == (UnityEngine.Object) null || MatchManager.Instance.CardActive.InternalId != this.InternalId))
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    }
    else
    {
      if ((UnityEngine.Object) LootManager.Instance != (UnityEngine.Object) null && (UnityEngine.Object) CardCraftManager.Instance == (UnityEngine.Object) null)
        LootManager.Instance.HighLight(true, Enum.GetName(typeof (Enums.CardType), (object) this.cardData.CardType));
      if (this.cardData.CardClass == Enums.CardClass.Item && (UnityEngine.Object) CardCraftManager.Instance != (UnityEngine.Object) null)
        CardCraftManager.Instance.HoverItem(true, this.cardData.CardType);
      if (this.cardmakebig)
      {
        if ((double) this.cardmakebigSize == 0.0)
          this.cardmakebigSize = this.transform.localScale.x;
        GameManager.Instance.PlayLibraryAudio("castnpccardfast");
        if ((double) this.cardmakebigSizeMax == 0.0)
          this.cardmakebigSizeMax = this.cardmakebigSize + 0.2f;
        this.SetDestinationLocalScale(this.cardmakebigSizeMax);
        this.SetDestination(this.transform.localPosition);
        this.cardrevealed = true;
        this.ShowKeyNotes();
        if (this.cardData.CardType != Enums.CardType.Corruption)
        {
          this.DrawBorder("blue");
        }
        else
        {
          if (!(this.cardData.RelatedCard != ""))
            return;
          this.CreateAmplifyRelatedCard(this.cardData.RelatedCard, this.transform);
        }
      }
      else if (this.cardoutsidecombatamplified)
        this.HideReveleadOutside();
      else if (this.cardoutsidecombat)
      {
        GameManager.Instance.PlayLibraryAudio("ui_menu_popup_01");
        if (this.revealedCoroutine != null)
          this.StopCoroutine(this.revealedCoroutine);
        this.revealedCoroutine = this.StartCoroutine(this.RevealedOutsideCoroutine());
      }
      else
      {
        if (GameManager.Instance.IsMultiplayer() && (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && !this.cardrevealed)
        {
          if (MatchManager.Instance.heroIndexWaitingForAddDiscard > -1)
          {
            if (!MatchManager.Instance.IsYourTurnForAddDiscard())
              return;
          }
          else if (!MatchManager.Instance.IsYourTurn())
          {
            this.ShowEmoteButton(true);
            return;
          }
        }
        if (this.cardforaddcard || this.cardfordiscard || this.cardfordisplay || this.cardoutsidelibary || (bool) (UnityEngine.Object) EventManager.Instance && this.cardrevealed)
          this.ShowKeyNotes();
        if (this.cardfordiscard && !this.cardselectedfordiscard)
          this.DrawBorder("blue");
        else if (this.cardforaddcard && !this.cardselectedforaddcard)
        {
          this.DrawBorder("blue");
        }
        else
        {
          if (!(bool) (UnityEngine.Object) MatchManager.Instance || MatchManager.Instance.WaitingForActionScreen() || MatchManager.Instance.WaitingForCardEnergyAssignment)
            return;
          if (this.cardrevealed && !MatchManager.Instance.CardDrag && MatchManager.Instance.GetNPCActive() != this.theNPC.NPCIndex)
          {
            if (this.revealedCoroutine != null)
              this.StopCoroutine(this.revealedCoroutine);
            this.revealedCoroutine = this.StartCoroutine(this.RevealedCoroutine());
          }
          if (!this.active || this.discard || MatchManager.Instance.CardDrag || (!GameManager.Instance.IsMultiplayer() || MatchManager.Instance.IsYourTurn()) && MatchManager.Instance.HandMask.gameObject.activeSelf)
            return;
          if (GameManager.Instance.IsMultiplayer() && MatchManager.Instance.IsYourTurn())
            MatchManager.Instance.AmplifyCard(this.tablePosition);
          this.fOnMouseEnter();
        }
      }
    }
  }

  public void fOnMouseEnter()
  {
    GameManager.Instance.SetCursorHover();
    GameManager.Instance.PlayLibraryAudio("card_play");
    this.AmplifyCard();
    if (MatchManager.Instance.IsYourTurn())
    {
      int num = 0;
      if (MatchManager.Instance.GetHeroActive() > -1)
      {
        if (MatchManager.Instance.CountHeroDeck() > 0)
          ++num;
        if (MatchManager.Instance.CountHeroDiscard() > 0)
          ++num;
      }
      MatchManager.Instance.controllerCurrentIndex = this.tablePosition + num;
    }
    if (GameManager.Instance.IsMultiplayer() && (!GameManager.Instance.IsMultiplayer() || !((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null) || !MatchManager.Instance.IsYourTurn()) && (!GameManager.Instance.IsMultiplayer() || !((UnityEngine.Object) MatchManager.Instance == (UnityEngine.Object) null)))
      return;
    this.ShowKeyNotes();
  }

  public void OnMouseExit()
  {
    if ((UnityEngine.Object) this.cardData == (UnityEngine.Object) null || SettingsManager.Instance.IsActive() || AlertManager.Instance.IsActive() || (bool) (UnityEngine.Object) MatchManager.Instance && MatchManager.Instance.console.IsActive() || CardScreenManager.Instance.IsActive() || MadnessManager.Instance.IsActive() || SandboxManager.Instance.IsActive() || PerkTree.Instance.IsActive() && !TomeManager.Instance.IsActive() || (bool) (UnityEngine.Object) MatchManager.Instance && MatchManager.Instance.PreCastNum > -1)
      return;
    GameManager.Instance.SetCursorPlain();
    if ((UnityEngine.Object) LootManager.Instance != (UnityEngine.Object) null)
      LootManager.Instance.HighLight(false, Enum.GetName(typeof (Enums.CardType), (object) this.cardData.CardType));
    if (this.cardData.CardClass == Enums.CardClass.Item && (UnityEngine.Object) CardCraftManager.Instance != (UnityEngine.Object) null)
      CardCraftManager.Instance.HoverItem(false, this.cardData.CardType);
    if (this.cardmakebig)
    {
      this.SetDestinationLocalScale(this.cardmakebigSize);
      this.HideKeyNotes();
      if (this.cardData.CardType != Enums.CardType.Corruption)
        this.DrawBorder("");
      if (!((UnityEngine.Object) this.relatedCard != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.relatedCard);
    }
    else
    {
      this.RemoveAmplifyNewCard();
      this.HideKeyNotes();
      if (this.cardoutsidecombatamplified)
        return;
      if (this.cardoutsidecombat)
      {
        this.HideReveleadOutside();
      }
      else
      {
        if (GameManager.Instance.IsMultiplayer() && (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && this.cardData.CardClass != Enums.CardClass.Monster)
        {
          if (MatchManager.Instance.heroIndexWaitingForAddDiscard > -1)
          {
            if (!MatchManager.Instance.IsYourTurnForAddDiscard())
              return;
          }
          else if (!MatchManager.Instance.IsYourTurn())
          {
            this.ShowEmoteButton(false);
            return;
          }
        }
        if (this.cardfordiscard)
        {
          if (this.cardselectedfordiscard)
            return;
          this.DrawBorder("");
        }
        else if (this.cardforaddcard)
        {
          if (this.cardselectedforaddcard)
            return;
          this.DrawBorder("");
        }
        else
        {
          if (!((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null))
            return;
          if (!MatchManager.Instance.CardDrag)
          {
            MatchManager.Instance.SetDamagePreview(false);
            MatchManager.Instance.SetOverDeck(false);
          }
          if (MatchManager.Instance.WaitingForActionScreen() || MatchManager.Instance.WaitingForCardEnergyAssignment)
            return;
          if (this.revealedCoroutine != null)
          {
            this.StopCoroutine(this.revealedCoroutine);
            this.revealedCoroutine = (Coroutine) null;
          }
          this.HideKeyNotes();
          if (EventSystem.current.IsPointerOverGameObject() || !this.active || this.discard || MatchManager.Instance.CardDrag)
            return;
          if (GameManager.Instance.IsMultiplayer() && (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && MatchManager.Instance.IsYourTurn())
            MatchManager.Instance.AmplifyCardOut(this.tablePosition);
          this.fOnMouseExit();
        }
      }
    }
  }

  public void fOnMouseExit()
  {
    this.RestoreCard();
    MatchManager.Instance.SetGlobalOutlines(false);
  }

  private void OnMouseDown()
  {
    if (SettingsManager.Instance.IsActive() || AlertManager.Instance.IsActive() || (bool) (UnityEngine.Object) MatchManager.Instance && MatchManager.Instance.console.IsActive() || MadnessManager.Instance.IsActive() || SandboxManager.Instance.IsActive())
      return;
    if (GameManager.Instance.IsMultiplayer() && (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null)
    {
      if (MatchManager.Instance.heroIndexWaitingForAddDiscard > -1)
      {
        if (!MatchManager.Instance.IsYourTurnForAddDiscard())
          return;
      }
      else if (!MatchManager.Instance.IsYourTurn())
        return;
    }
    if (this.cardoutsidereward || (UnityEngine.Object) MatchManager.Instance == (UnityEngine.Object) null)
      return;
    if (this.cardfordiscard)
      MatchManager.Instance.PreSelectCard();
    else if (this.cardforaddcard)
    {
      MatchManager.Instance.PreSelectCard();
    }
    else
    {
      if (MatchManager.Instance.WaitingForActionScreen() || !this.IsPlayable() || MatchManager.Instance.IsGameBusy())
        return;
      this.HideKeyNotes();
      if (EventSystem.current.IsPointerOverGameObject() || !this.active || this.discard)
        return;
      this.AmplifyCard();
      MatchManager.Instance.SetTarget((Transform) null);
      MatchManager.Instance.CardDrag = true;
      MatchManager.Instance.SetCardActive(this.cardData);
      MatchManager.Instance.CardActiveT = this.transform;
      this.mouseClickedPosition = Input.mousePosition;
      GameManager.Instance.cameraMain.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
      Cursor.visible = false;
      this.canInstaCast = -1;
      MatchManager.Instance.SetDamagePreview(true, this.cardData, this.tablePosition);
      this.canInstaCast = !MatchManager.Instance.CanInstaCast(this.cardData) ? 0 : 1;
      this.canTargetCast = !MatchManager.Instance.IsThereAnyTargetForCard(this.cardData) ? 0 : 1;
      this.canEnergyCast = this.GetEnergyCost() > MatchManager.Instance.GetHeroEnergy() ? 0 : 1;
      this.fOnMouseDownCardData();
    }
  }

  public void fOnMouseDownCardData()
  {
    MatchManager.Instance.CardItemActive = this;
    MatchManager.Instance.SetCardHover(this.tablePosition, false);
  }

  private void OnMouseDrag()
  {
    if ((UnityEngine.Object) MatchManager.Instance == (UnityEngine.Object) null || !MatchManager.Instance.CardDrag || this.cardforaddcard || this.cardfordiscard || GameManager.Instance.IsMultiplayer() && (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && !MatchManager.Instance.IsYourTurn())
      return;
    this.fOnMouseDrag();
  }

  private void fOnMouseDrag()
  {
    if (!this.IsPlayable() || MatchManager.Instance.IsGameBusy() || !this.active || this.discard)
      return;
    Vector3 mousePosition = Input.mousePosition;
    if (this.canTargetCast != 1)
      return;
    if (this.canInstaCast != 1)
    {
      this.DrawArrow(this.mouseClickedPosition, GameManager.Instance.cameraMain.ScreenToWorldPoint(mousePosition));
      Cursor.visible = false;
    }
    else if ((double) mousePosition.y - (double) this.mouseClickedPosition.y > (double) this.distanceForDragCast)
    {
      if (!this.cardDraggedCanCast)
        this.ShowBorderParticle();
      this.cardDraggedCanCast = true;
    }
    else
    {
      if (this.cardDraggedCanCast)
        this.DrawEnergyBorder();
      this.cardDraggedCanCast = false;
    }
  }

  public void OnMouseUpController()
  {
    if (!(bool) (UnityEngine.Object) MatchManager.Instance)
      return;
    if (MatchManager.Instance.DeckCardsWindow.IsActive())
      this.OnMouseUp();
    else if (MatchManager.Instance.DiscardSelector.IsActive())
    {
      this.OnMouseUp();
    }
    else
    {
      if (!this.IsPlayable() || MatchManager.Instance.IsGameBusy())
        return;
      if (MatchManager.Instance.IsYourTurn())
      {
        if (MatchManager.Instance.CanInstaCast(this.cardData))
        {
          int indexInTableById = MatchManager.Instance.GetCardIndexInTableById(this.internalId);
          if (indexInTableById == -1)
            return;
          MatchManager.Instance.CastCardNum(indexInTableById + 1);
          MatchManager.Instance.ResetCardHoverIndex();
        }
        else
        {
          this.OnMouseDown();
          MatchManager.Instance.SetControllerCardClicked();
        }
      }
      else
        MatchManager.Instance.SendEmoteCard(this.tablePosition);
    }
  }

  public void OnMouseUp()
  {
    if (SettingsManager.Instance.IsActive() || AlertManager.Instance.IsActive() || (bool) (UnityEngine.Object) MatchManager.Instance && MatchManager.Instance.console.IsActive() || MadnessManager.Instance.IsActive() || SandboxManager.Instance.IsActive() || PerkTree.Instance.IsActive() && !CardScreenManager.Instance.IsActive() || (!(bool) (UnityEngine.Object) MatchManager.Instance || !MatchManager.Instance.CardDrag) && (!(bool) (UnityEngine.Object) MatchManager.Instance || !this.keyTransform.gameObject.activeSelf) && !Functions.ClickedThisTransform(this.transform))
      return;
    if ((UnityEngine.Object) CardPlayerManager.Instance != (UnityEngine.Object) null && this.CardPlayerIndex > -1)
    {
      CardPlayerManager.Instance.SelectCard(this.CardPlayerIndex);
      GameManager.Instance.CleanTempContainer();
    }
    else if ((UnityEngine.Object) CardPlayerPairsManager.Instance != (UnityEngine.Object) null && this.CardPlayerIndex > -1)
    {
      CardPlayerPairsManager.Instance.SelectCard(this.CardPlayerIndex);
      GameManager.Instance.CleanTempContainer();
    }
    else if (this.cardoutsideloot)
    {
      if ((UnityEngine.Object) LootManager.Instance != (UnityEngine.Object) null && !LootManager.Instance.IsMyLoot || !(bool) (UnityEngine.Object) LootManager.Instance)
        return;
      LootManager.Instance.Looted(this.lootId);
      this.DisableCollider();
      GameManager.Instance.CleanTempContainer();
    }
    else if (this.cardoutsidereward)
    {
      this.HideReveleadOutside();
      if (!((UnityEngine.Object) RewardsManager.Instance != (UnityEngine.Object) null))
        return;
      if (GameManager.Instance.IsMultiplayer())
        RewardsManager.Instance.SetCardReward(NetworkManager.Instance.GetPlayerNick(), this.gameObject.name);
      else
        RewardsManager.Instance.SetCardReward("", this.gameObject.name);
      GameManager.Instance.CleanTempContainer();
    }
    else
    {
      if ((UnityEngine.Object) MatchManager.Instance == (UnityEngine.Object) null)
        return;
      Cursor.visible = true;
      if (GameManager.Instance.IsMultiplayer() && (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null)
      {
        if (MatchManager.Instance.heroIndexWaitingForAddDiscard > -1)
        {
          if (!MatchManager.Instance.IsYourTurnForAddDiscard())
            return;
        }
        else if (!MatchManager.Instance.IsYourTurn())
        {
          MatchManager.Instance.SendEmoteCard(this.tablePosition);
          return;
        }
      }
      if (this.cardfordiscard)
        MatchManager.Instance.SelectCardToDiscard(this);
      else if (this.cardforaddcard)
      {
        MatchManager.Instance.SelectCardToAddcard(this);
      }
      else
      {
        if (!MatchManager.Instance.CardDrag || MatchManager.Instance.WaitingForActionScreen() || !this.IsPlayable() || MatchManager.Instance.IsGameBusy() || EventSystem.current.IsPointerOverGameObject() || !this.active || this.discard)
          return;
        if ((UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null)
        {
          this.cursorArrow.StopDraw();
          if (GameManager.Instance.IsMultiplayer() && MatchManager.Instance.IsYourTurn())
            MatchManager.Instance.StopArrowNet(this.tablePosition);
        }
        MatchManager.Instance.CardDrag = false;
        MatchManager.Instance.CardActiveT = (Transform) null;
        MatchManager.Instance.SetDamagePreview(false);
        MatchManager.Instance.SetOverDeck(false);
        if (MatchManager.Instance.CheckTarget(cardToCheck: this.cardData) || MatchManager.Instance.CanInstaCast(this.cardData) && this.cardDraggedCanCast)
        {
          if (this.canEnergyCast == 1)
          {
            this.StartCoroutine(MatchManager.Instance.CastCard(this));
            this.StartCoroutine(MatchManager.Instance.JustCastedCo());
            return;
          }
          MatchManager.Instance.NoEnergy();
        }
        if (GameManager.Instance.IsMultiplayer() && (UnityEngine.Object) MatchManager.Instance != (UnityEngine.Object) null && MatchManager.Instance.IsYourTurn())
          MatchManager.Instance.AmplifyCardOut(this.tablePosition);
        if (MatchManager.Instance.CanInstaCast(this.cardData))
          this.fOnMouseEnter();
        this.RestoreCard();
      }
    }
  }

  public void ShowEmoteButton(bool _state)
  {
    if (!GameManager.Instance.IsMultiplayer() || (UnityEngine.Object) MatchManager.Instance == (UnityEngine.Object) null || !((UnityEngine.Object) this.emoteIcon != (UnityEngine.Object) null) || _state && (!this.active || this.discard || this.prediscard || this.cardoutsidecombat || this.cardoutsidelibary || this.cardoutsideselection || MatchManager.Instance.WaitingForActionScreen() || MatchManager.Instance.WaitingForCardEnergyAssignment))
      return;
    this.emoteIcon.gameObject.SetActive(_state);
    this.emoteIcon.GetComponent<EmoteTarget>().SetActiveHeroOnCardEmoteButton();
  }

  public void ShowEmoteIcon(byte _heroIndex)
  {
    this.ShowEmoteTransform();
    Hero hero = MatchManager.Instance.GetHero((int) _heroIndex);
    if (this.theHero == null || !((UnityEngine.Object) this.theHero.HeroData != (UnityEngine.Object) null) || !((UnityEngine.Object) this.theHero.HeroData.HeroSubClass != (UnityEngine.Object) null))
      return;
    Sprite stickerBase = hero.HeroData.HeroSubClass.StickerBase;
    if ((UnityEngine.Object) this.emote0.sprite == (UnityEngine.Object) stickerBase || (UnityEngine.Object) this.emote1.sprite == (UnityEngine.Object) stickerBase || (UnityEngine.Object) this.emote2.sprite == (UnityEngine.Object) stickerBase)
      return;
    if ((UnityEngine.Object) this.emote0.sprite == (UnityEngine.Object) null)
    {
      this.emote0.sprite = stickerBase;
      this.emote0.gameObject.SetActive(true);
    }
    else if ((UnityEngine.Object) this.emote1.sprite == (UnityEngine.Object) null)
    {
      this.emote1.sprite = stickerBase;
      this.emote1.gameObject.SetActive(true);
    }
    else
    {
      if (!((UnityEngine.Object) this.emote2.sprite == (UnityEngine.Object) null))
        return;
      this.emote2.sprite = stickerBase;
      this.emote2.gameObject.SetActive(true);
    }
  }

  public void RemoveEmotes()
  {
    this.ShowEmoteButton(false);
    this.RemoveEmoteIcons();
  }

  public void ShowEmoteTransform()
  {
    if (this.emotes.transform.gameObject.activeSelf)
      return;
    this.emotes.transform.gameObject.SetActive(true);
  }

  public void RemoveEmoteIcons()
  {
    this.emote0.sprite = (Sprite) null;
    this.emote0.gameObject.SetActive(false);
    this.emote1.sprite = (Sprite) null;
    this.emote1.gameObject.SetActive(false);
    this.emote2.sprite = (Sprite) null;
    this.emote2.gameObject.SetActive(false);
  }

  public void RemoveEmoteIcon(byte _heroIndex)
  {
    Hero hero = MatchManager.Instance.GetHero((int) _heroIndex);
    if (this.theHero == null || !((UnityEngine.Object) this.theHero.HeroData != (UnityEngine.Object) null) || !((UnityEngine.Object) this.theHero.HeroData.HeroSubClass != (UnityEngine.Object) null))
      return;
    Sprite stickerBase = hero.HeroData.HeroSubClass.StickerBase;
    if ((UnityEngine.Object) this.emote0.sprite == (UnityEngine.Object) stickerBase)
    {
      this.emote0.sprite = (Sprite) null;
      this.emote0.gameObject.SetActive(false);
    }
    else if ((UnityEngine.Object) this.emote1.sprite == (UnityEngine.Object) stickerBase)
    {
      this.emote1.sprite = (Sprite) null;
      this.emote1.gameObject.SetActive(false);
    }
    else
    {
      if (!((UnityEngine.Object) this.emote2.sprite == (UnityEngine.Object) stickerBase))
        return;
      this.emote2.sprite = (Sprite) null;
      this.emote2.gameObject.SetActive(false);
    }
  }

  public bool HaveEmoteIcon(byte _heroIndex)
  {
    Hero hero = MatchManager.Instance.GetHero((int) _heroIndex);
    if (hero != null && (UnityEngine.Object) hero.HeroData != (UnityEngine.Object) null && (UnityEngine.Object) hero.HeroData.HeroSubClass != (UnityEngine.Object) null)
    {
      Sprite stickerBase = hero.HeroData.HeroSubClass.StickerBase;
      if ((UnityEngine.Object) this.emote0.sprite == (UnityEngine.Object) stickerBase || (UnityEngine.Object) this.emote1.sprite == (UnityEngine.Object) stickerBase || (UnityEngine.Object) this.emote2.sprite == (UnityEngine.Object) stickerBase)
        return true;
    }
    return false;
  }

  public void ShowPortrait(Sprite _sprite, int _position = 0)
  {
    this.portraits.gameObject.SetActive(true);
    if (_position == 1)
      this.portraits.transform.localPosition = new Vector3(0.45f, 0.85f, this.portraits.transform.localPosition.z);
    for (int index = 0; index < 4; ++index)
    {
      if (!this.portrait[index].transform.gameObject.activeSelf)
      {
        this.portrait[index].transform.gameObject.SetActive(true);
        this.portrait[index].sprite = _sprite;
        break;
      }
    }
  }

  public void ClearMPMark()
  {
    for (int index = 0; index < 4; ++index)
      this.mpMark[index].transform.gameObject.SetActive(false);
  }

  public void ShowMPMark(string _nick)
  {
    this.mpMarks.gameObject.SetActive(true);
    for (int index = 0; index < 4; ++index)
    {
      if (!this.mpMark[index].transform.gameObject.activeSelf)
      {
        this.mpMark[index].color = Functions.HexToColor(NetworkManager.Instance.GetColorFromNick(_nick));
        this.mpMark[index].transform.gameObject.SetActive(true);
        break;
      }
    }
  }

  public void ShowDifferences(CardData targetData)
  {
    if ((UnityEngine.Object) targetData == (UnityEngine.Object) null || (UnityEngine.Object) this.cardData == (UnityEngine.Object) null)
      return;
    if (this.cardData.EnergyCost != targetData.EnergyCost)
    {
      if (!this.diffEnergy.gameObject.activeSelf)
        this.diffEnergy.gameObject.SetActive(true);
    }
    else if (this.diffEnergy.gameObject.activeSelf)
      this.diffEnergy.gameObject.SetActive(false);
    if (this.cardData.Vanish != targetData.Vanish)
    {
      if (!this.diffVanish.gameObject.activeSelf)
        this.diffVanish.gameObject.SetActive(true);
    }
    else if (this.diffVanish.gameObject.activeSelf)
      this.diffVanish.gameObject.SetActive(false);
    if (this.cardData.Innate != targetData.Innate)
    {
      if (!this.diffInnate.gameObject.activeSelf)
        this.diffInnate.gameObject.SetActive(true);
    }
    else if (this.diffInnate.gameObject.activeSelf)
      this.diffInnate.gameObject.SetActive(false);
    if (this.cardData.TargetType != targetData.TargetType || this.cardData.TargetSide != targetData.TargetSide || this.cardData.TargetPosition != targetData.TargetPosition)
    {
      if (!this.diffTarget.gameObject.activeSelf)
        this.diffTarget.gameObject.SetActive(true);
    }
    else if (this.diffTarget.gameObject.activeSelf)
      this.diffTarget.gameObject.SetActive(false);
    if (this.cardData.EffectRequired != targetData.EffectRequired)
    {
      if (this.diffRequire.gameObject.activeSelf)
        return;
      this.diffRequire.gameObject.SetActive(true);
    }
    else
    {
      if (!this.diffRequire.gameObject.activeSelf)
        return;
      this.diffRequire.gameObject.SetActive(false);
    }
  }

  public void HideDifferences()
  {
    if (this.diffEnergy.gameObject.activeSelf)
      this.diffEnergy.gameObject.SetActive(false);
    if (this.diffTarget.gameObject.activeSelf)
      this.diffTarget.gameObject.SetActive(false);
    if (this.diffRequire.gameObject.activeSelf)
      this.diffRequire.gameObject.SetActive(false);
    if (this.diffVanish.gameObject.activeSelf)
      this.diffVanish.gameObject.SetActive(false);
    if (this.diffInnate.gameObject.activeSelf)
      this.diffInnate.gameObject.SetActive(false);
    if (this.diffSkillA.gameObject.activeSelf)
      this.diffSkillA.gameObject.SetActive(false);
    if (!this.diffSkillB.gameObject.activeSelf)
      return;
    this.diffSkillB.gameObject.SetActive(false);
  }

  public void ShowKeyNum(bool _state, string _num = "", bool _disabled = false)
  {
    if (_state && !GameManager.Instance.ConfigKeyboardShortcuts && !MatchManager.Instance.KeyClickedCard)
      _state = false;
    if (this.keyTransform.gameObject.activeSelf != _state)
      this.keyTransform.gameObject.SetActive(_state);
    if (!_state)
      return;
    if (_num == "10")
      _num = "0";
    this.keyNumber.text = _num;
    if (!_disabled && (this.disableT.gameObject.activeSelf || this.cardBorderSR.color == this.redColor))
      _disabled = true;
    if (_disabled)
    {
      if (!this.keyRed.gameObject.activeSelf)
        this.keyRed.gameObject.SetActive(true);
      if (this.keyBackground.gameObject.activeSelf)
        this.keyBackground.gameObject.SetActive(false);
    }
    else
    {
      if (this.keyRed.gameObject.activeSelf)
        this.keyRed.gameObject.SetActive(false);
      if (!this.keyBackground.gameObject.activeSelf)
        this.keyBackground.gameObject.SetActive(true);
    }
    if (MatchManager.Instance.DeckCardsWindow.IsActive() || MatchManager.Instance.DiscardSelector.IsActive())
      this.keyTransform.localPosition = new Vector3(0.75f, 1.1f, 0.0f);
    else
      this.keyTransform.localPosition = new Vector3(0.0f, 1.6f, 0.0f);
  }

  public CardData CardData
  {
    get => this.cardData;
    set => this.cardData = value;
  }

  public ItemData ItemData
  {
    get => this.itemData;
    set => this.itemData = value;
  }

  public string InternalId
  {
    get => this.internalId;
    set => this.internalId = value;
  }

  public int TablePosition
  {
    get => this.tablePosition;
    set => this.tablePosition = value;
  }

  public float CardSize
  {
    get => this.cardSize;
    set => this.cardSize = value;
  }
}
