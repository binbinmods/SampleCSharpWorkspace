// Decompiled with JetBrains decompiler
// Type: NPCItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class NPCItem : CharacterItem
{
  [SerializeField]
  private NPCData npcData;
  public Transform animatedTransform;
  public Transform bossParticles;
  public Transform bossSmallParticles;
  public Transform namedSmallParticles;
  public Transform cardsGOT;
  public Transform[] cardsT;
  public CardItem[] cardsCI;

  public override void Awake() => base.Awake();

  public override void Start() => base.Start();

  public void Init(NPC _npc)
  {
    if ((Object) this.npcData == (Object) null)
      return;
    this.NPC = _npc;
    this.Hero = (Hero) null;
    this.IsHero = false;
    this.energyT.parent.gameObject.SetActive(false);
    this.GO_Buffs.transform.localPosition = new Vector3(0.03f, -1.05f, 0.0f);
    if ((Object) this.npcData.GameObjectAnimated != (Object) null)
    {
      GameObject GO = Object.Instantiate<GameObject>(this.npcData.GameObjectAnimated, Vector3.zero, Quaternion.identity, this.transform);
      this.animatedTransform = GO.transform;
      GO.transform.localPosition = this.npcData.GameObjectAnimated.transform.localPosition;
      GO.transform.localRotation = this.npcData.GameObjectAnimated.transform.localRotation;
      this.GetComponent<CharacterItem>().SetOriginalLocalPosition(GO.transform.localPosition);
      GO.name = this.transform.name;
      this.DisableCollider();
      CharacterGOItem characterGoItem = GO.GetComponent<CharacterGOItem>();
      if ((Object) characterGoItem == (Object) null)
        characterGoItem = GO.AddComponent(typeof (CharacterGOItem)) as CharacterGOItem;
      characterGoItem._characterItem = this.GetComponent<CharacterItem>();
      this.CharImageSR.sprite = (Sprite) null;
      this.Anim = GO.GetComponent<Animator>();
      this.GetSpritesFromAnimated(GO);
      this.transformForCombatText = GO.transform;
      if ((Object) GO.GetComponent<BoxCollider2D>() == (Object) null)
        GO.AddComponent(typeof (BoxCollider2D));
      this.heightModel = GO.GetComponent<BoxCollider2D>().size.y;
    }
    else
    {
      this.CharImageSR.sprite = this.npcData.Sprite;
      if ((double) this.npcData.PosBottom != 0.0)
        this.CharImageSR.transform.localPosition = new Vector3(this.CharImageT.localPosition.x, (float) ((double) this.npcData.PosBottom * (double) Screen.height * (1.0 / 1000.0)), this.CharImageT.localPosition.z);
      this.transformForCombatText = this.transform;
    }
    this.cardsGOT.position = this.npcData.BigModel ? new Vector3(this.cardsGOT.position.x, 4.8f, this.cardsGOT.position.z) : new Vector3(this.cardsGOT.position.x, 4.2f, this.cardsGOT.position.z);
    if (this.npcData.IsBoss)
    {
      if (this.npcData.BigModel)
        this.bossParticles.gameObject.SetActive(true);
      else
        this.bossSmallParticles.gameObject.SetActive(true);
    }
    else if (this.npcData.IsNamed)
      this.namedSmallParticles.gameObject.SetActive(true);
    if (this.npcData.BigModel)
    {
      this.hpBackground.gameObject.SetActive(false);
      this.hpBackgroundHigh.gameObject.SetActive(true);
      this.hpT.localScale = new Vector3(2.5f, 1.2f, 1f);
      this.hpT.localPosition = new Vector3(-2.16f, -0.5f, 0.0f);
      this.hpShieldT.localScale = new Vector3(1.5f, 1.5f, 1f);
      this.hpShieldT.localPosition = new Vector3(-0.79f, -0.15f, 0.0f);
      this.hpBlockT.localPosition = new Vector3(0.31f, 0.07f, 0.0f);
      this.hpBlockIconT.localScale = new Vector3(1.5f, 1.5f, 1f);
      this.hpBlockIconT.localPosition = new Vector3(-1.19f, -0.1f, 0.0f);
      this.HpText.fontSize = 3f;
      this.HpText.transform.localPosition = new Vector3(1.38f, 0.07f, 1f);
      this.GO_Buffs.transform.localScale = new Vector3(1.4f, 1.4f, 1f);
      this.GO_Buffs.transform.localPosition = new Vector3(0.39f, -1.3f, 0.0f);
      this.GO_Buffs.GetComponent<GridLayoutGroup>().cellSize = (Vector2) new Vector3(0.32f, 0.3f, 0.0f);
      this.GO_Buffs.GetComponent<GridLayoutGroup>().constraintCount = 8;
      this.skull.transform.localPosition = new Vector3(0.4f, this.skull.transform.localPosition.y, this.skull.transform.localPosition.z);
      this.skull.transform.localScale = new Vector3(0.45f, 0.45f, 1f);
    }
    this.ActivateMark(false);
    this.SetHP();
    this.DrawEnergy();
    if (!((Object) MatchManager.Instance != (Object) null))
      return;
    this.StartCoroutine(this.EnchantEffectCo());
  }

  public void RemoveAllCards()
  {
    for (int index = 0; index < this.cardsT.Length; ++index)
    {
      if ((Object) this.cardsT[index] != (Object) null)
        Object.Destroy((Object) this.cardsT[index].gameObject);
    }
    this.cardsT = (Transform[]) null;
  }

  public NPCData NpcData
  {
    get => this.npcData;
    set => this.npcData = value;
  }
}
