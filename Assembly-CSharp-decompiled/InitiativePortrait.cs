// Decompiled with JetBrains decompiler
// Type: InitiativePortrait
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InitiativePortrait : MonoBehaviour
{
  public Hero Hero;
  public NPCData NpcData;
  public string id;
  public HeroItem heroItem;
  public NPCItem npcItem;
  public int portraitPosition;
  public int portraitElements;
  public Transform activeTransform;
  public TMP_Text speedTM;
  public SpriteRenderer charSprite;
  public Transform playing;
  private SpriteRenderer spriteRenderer;
  private Renderer[] childRenderers;
  private bool isHero;
  private bool isNPC;
  private int position = -1;
  public Vector3 destinationLocalPosition = Vector3.zero;
  private Coroutine movePortraitCoroutine;

  private void Awake()
  {
    this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    this.childRenderers = new Renderer[this.transform.childCount];
    int index = 0;
    foreach (Component component1 in this.transform)
    {
      Renderer component2 = component1.GetComponent<Renderer>();
      this.childRenderers[index] = component2;
      ++index;
    }
  }

  private void MovePortrait()
  {
    if (this.movePortraitCoroutine != null)
      this.StopCoroutine(this.movePortraitCoroutine);
    this.movePortraitCoroutine = this.StartCoroutine(this.MovePortraitCo());
  }

  private IEnumerator MovePortraitCo()
  {
    InitiativePortrait initiativePortrait = this;
    while (initiativePortrait.transform.localPosition != initiativePortrait.destinationLocalPosition)
    {
      initiativePortrait.transform.localPosition = Vector3.Lerp(initiativePortrait.transform.localPosition, initiativePortrait.destinationLocalPosition, Time.deltaTime * 8f);
      if ((double) Mathf.Abs(initiativePortrait.transform.localPosition.x - initiativePortrait.destinationLocalPosition.x) < 0.0099999997764825821)
        initiativePortrait.transform.localPosition = initiativePortrait.destinationLocalPosition;
      yield return (object) null;
    }
    initiativePortrait.SortingOrder();
  }

  public void AdjustForRoundSeparator()
  {
    this.destinationLocalPosition += new Vector3(0.359999985f, 0.0f, 0.0f);
    this.MovePortrait();
  }

  private void SortingOrder()
  {
    int num = 10;
    for (int index = 0; index < this.childRenderers.Length; ++index)
    {
      this.childRenderers[index].sortingOrder = this.position * 10 + num;
      --num;
    }
  }

  public void Init(int _position)
  {
    if (this.Hero != null)
      this.charSprite.sprite = this.Hero.SpriteSpeed;
    else if ((Object) this.NpcData != (Object) null)
      this.charSprite.sprite = this.NpcData.SpriteSpeed;
    this.position = _position;
    Vector3 vector3 = this.CalcVectorPosition(this.position);
    if (this.destinationLocalPosition == Vector3.zero)
      this.transform.localPosition = this.destinationLocalPosition = vector3;
    else
      this.destinationLocalPosition = vector3;
    this.SortingOrder();
    this.activeTransform.gameObject.SetActive(false);
  }

  public void SetPlaying(bool state) => this.playing.gameObject.SetActive(state);

  public void SetActive(bool state) => this.activeTransform.gameObject.SetActive(state);

  public int GetPos() => this.position;

  public void RedoPos(int _position, bool adjust)
  {
    this.destinationLocalPosition = this.CalcVectorPosition(_position);
    if (adjust)
      this.destinationLocalPosition += new Vector3(0.359999985f, 0.0f, 0.0f);
    this.position = _position;
    this.MovePortrait();
    this.SortingOrder();
  }

  private Vector3 CalcVectorPosition(int _position) => new Vector3((float) ((double) _position * 0.47999998927116394 + (double) _position * 0.23999999463558197), 0.0f, 0.0f);

  public void SetSpeed(int[] speed)
  {
    string str = speed[0].ToString();
    int num = speed[2];
    Color color = new Color(1f, 1f, 1f, 1f);
    if (num > 0)
      ColorUtility.TryParseHtmlString(Globals.Instance.ClassColor["scout"], out color);
    else if (num < 0)
      ColorUtility.TryParseHtmlString(Globals.Instance.ClassColor["warrior"], out color);
    this.speedTM.color = color;
    this.speedTM.text = str;
  }

  public void SetHero(Hero _hero, HeroItem _heroItem)
  {
    this.Hero = _hero;
    this.heroItem = _heroItem;
    this.NpcData = (NPCData) null;
    this.npcItem = (NPCItem) null;
    this.isHero = true;
  }

  public void SetNPC(NPCData _npcData, NPCItem _npcItem)
  {
    this.Hero = (Hero) null;
    this.heroItem = (HeroItem) null;
    this.NpcData = _npcData;
    this.npcItem = _npcItem;
    this.isNPC = true;
  }

  public void OnMouseUp()
  {
    if ((bool) (Object) MatchManager.Instance && MatchManager.Instance.console.IsActive() || EventSystem.current.IsPointerOverGameObject())
      return;
    GameManager.Instance.SetCursorPlain();
    MatchManager.Instance.ShowCharacterWindow("stats", this.isHero, !this.isHero ? this.npcItem.NPC.NPCIndex : this.Hero.HeroIndex);
    MatchManager.Instance.combatTarget.ClearTarget();
  }

  private void OnMouseEnter()
  {
    if (AlertManager.Instance.IsActive() || SettingsManager.Instance.IsActive() || (bool) (Object) MatchManager.Instance && MatchManager.Instance.console.IsActive() || EventSystem.current.IsPointerOverGameObject())
      return;
    if (this.isHero)
    {
      this.heroItem.OutlineGray();
      MatchManager.Instance.combatTarget.SetTargetTMP((Character) this.Hero);
    }
    else if (this.isNPC)
    {
      this.npcItem.OutlineGray();
      MatchManager.Instance.combatTarget.SetTargetTMP((Character) this.npcItem.NPC);
    }
    GameManager.Instance.SetCursorHover();
    this.SetActive(true);
  }

  private void OnMouseExit()
  {
    if (!EventSystem.current.IsPointerOverGameObject())
    {
      if (MatchManager.Instance.CardDrag)
        MatchManager.Instance.SetGlobalOutlines(true, MatchManager.Instance.CardActive);
      else if (this.isHero)
        this.heroItem.OutlineHide();
      else if (this.isNPC)
        this.npcItem.OutlineHide();
      GameManager.Instance.SetCursorPlain();
      MatchManager.Instance.combatTarget.ClearTarget();
    }
    this.SetActive(false);
  }
}
