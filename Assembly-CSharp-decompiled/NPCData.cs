// Decompiled with JetBrains decompiler
// Type: NPCData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC Data", order = 54)]
public class NPCData : ScriptableObject
{
  [SerializeField]
  private string npcName = "";
  [SerializeField]
  private string scriptableObjectName = "";
  [SerializeField]
  private string id;
  [SerializeField]
  private string description;
  [SerializeField]
  private bool isBoss;
  [SerializeField]
  private bool isNamed;
  [SerializeField]
  private NPCData baseMonster;
  [SerializeField]
  private bool bigModel;
  [SerializeField]
  private bool finishCombatOnDead;
  [SerializeField]
  private bool female;
  [Header("Tier and upgrade")]
  [SerializeField]
  private Enums.CombatTier tierMob;
  [SerializeField]
  private NPCData upgradedMob;
  [Header("NG+ Id")]
  [SerializeField]
  private NPCData ngPlusMob;
  [Header("Hell/Hard Id")]
  [SerializeField]
  private NPCData hellModeMob;
  [Header("Obelisk Challenge")]
  [SerializeField]
  private int difficulty;
  [SerializeField]
  private Enums.CardTargetPosition preferredPosition;
  [Header("Game objects")]
  [SerializeField]
  private GameObject gameObjectAnimated;
  [SerializeField]
  private AudioClip hitSound;
  [SerializeField]
  private Sprite sprite;
  [SerializeField]
  private Sprite spriteSpeed;
  [SerializeField]
  private Sprite spritePortrait;
  [SerializeField]
  private float posBottom;
  [SerializeField]
  private float fluffOffsetX;
  [SerializeField]
  private float fluffOffsetY;
  [Header("Stats")]
  [SerializeField]
  private int hp;
  [SerializeField]
  private int energy;
  [SerializeField]
  private int energyTurn;
  [SerializeField]
  private int speed;
  [Header("Resits")]
  [SerializeField]
  private int resistSlashing;
  [SerializeField]
  private int resistBlunt;
  [SerializeField]
  private int resistPiercing;
  [SerializeField]
  private int resistFire;
  [SerializeField]
  private int resistCold;
  [SerializeField]
  private int resistLightning;
  [SerializeField]
  private int resistMind;
  [SerializeField]
  private int resistHoly;
  [SerializeField]
  private int resistShadow;
  [Header("Immunities")]
  [SerializeField]
  private List<string> auracurseImmune = new List<string>();
  [Header("Cards")]
  [SerializeField]
  private int cardsInHand;
  [SerializeField]
  private global::AICards[] aiCards;
  [Header("Rewards")]
  [SerializeField]
  private int experienceReward;
  [SerializeField]
  private int goldReward;
  [SerializeField]
  private TierRewardData tierReward;

  public string NPCName
  {
    get => this.npcName;
    set => this.npcName = value;
  }

  public string Id => this.id;

  public string Description
  {
    get => this.description;
    set => this.description = value;
  }

  public bool IsBoss
  {
    get => this.isBoss;
    set => this.isBoss = value;
  }

  public bool BigModel
  {
    get => this.bigModel;
    set => this.bigModel = value;
  }

  public bool FinishCombatOnDead
  {
    get => this.finishCombatOnDead;
    set => this.finishCombatOnDead = value;
  }

  public int Difficulty
  {
    get => this.difficulty;
    set => this.difficulty = value;
  }

  public Enums.CardTargetPosition PreferredPosition
  {
    get => this.preferredPosition;
    set => this.preferredPosition = value;
  }

  public GameObject GameObjectAnimated
  {
    get => this.gameObjectAnimated;
    set => this.gameObjectAnimated = value;
  }

  public Sprite Sprite => this.sprite;

  public Sprite SpriteSpeed => this.spriteSpeed;

  public float PosBottom
  {
    get => this.posBottom;
    set => this.posBottom = value;
  }

  public int Hp => this.hp;

  public int Energy
  {
    get => this.energy;
    set => this.energy = value;
  }

  public int EnergyTurn
  {
    get => this.energyTurn;
    set => this.energyTurn = value;
  }

  public int Speed => this.speed;

  public int ResistSlashing
  {
    get => this.resistSlashing;
    set => this.resistSlashing = value;
  }

  public int ResistBlunt
  {
    get => this.resistBlunt;
    set => this.resistBlunt = value;
  }

  public int ResistPiercing
  {
    get => this.resistPiercing;
    set => this.resistPiercing = value;
  }

  public int ResistFire
  {
    get => this.resistFire;
    set => this.resistFire = value;
  }

  public int ResistCold
  {
    get => this.resistCold;
    set => this.resistCold = value;
  }

  public int ResistLightning
  {
    get => this.resistLightning;
    set => this.resistLightning = value;
  }

  public int ResistMind
  {
    get => this.resistMind;
    set => this.resistMind = value;
  }

  public int ResistHoly
  {
    get => this.resistHoly;
    set => this.resistHoly = value;
  }

  public int ResistShadow
  {
    get => this.resistShadow;
    set => this.resistShadow = value;
  }

  public global::AICards[] AICards
  {
    get => this.aiCards;
    set => this.aiCards = value;
  }

  public int CardsInHand
  {
    get => this.cardsInHand;
    set => this.cardsInHand = value;
  }

  public AudioClip HitSound
  {
    get => this.hitSound;
    set => this.hitSound = value;
  }

  public int ExperienceReward
  {
    get => this.experienceReward;
    set => this.experienceReward = value;
  }

  public int GoldReward
  {
    get => this.goldReward;
    set => this.goldReward = value;
  }

  public TierRewardData TierReward
  {
    get => this.tierReward;
    set => this.tierReward = value;
  }

  public Sprite SpritePortrait
  {
    get => this.spritePortrait;
    set => this.spritePortrait = value;
  }

  public List<string> AuracurseImmune
  {
    get => this.auracurseImmune;
    set => this.auracurseImmune = value;
  }

  public float FluffOffsetX
  {
    get => this.fluffOffsetX;
    set => this.fluffOffsetX = value;
  }

  public float FluffOffsetY
  {
    get => this.fluffOffsetY;
    set => this.fluffOffsetY = value;
  }

  public Enums.CombatTier TierMob
  {
    get => this.tierMob;
    set => this.tierMob = value;
  }

  public NPCData UpgradedMob
  {
    get => this.upgradedMob;
    set => this.upgradedMob = value;
  }

  public NPCData NgPlusMob
  {
    get => this.ngPlusMob;
    set => this.ngPlusMob = value;
  }

  public bool IsNamed
  {
    get => this.isNamed;
    set => this.isNamed = value;
  }

  public NPCData HellModeMob
  {
    get => this.hellModeMob;
    set => this.hellModeMob = value;
  }

  public string ScriptableObjectName
  {
    get => this.scriptableObjectName;
    set => this.scriptableObjectName = value;
  }

  public bool Female
  {
    get => this.female;
    set => this.female = value;
  }

  public NPCData BaseMonster
  {
    get => this.baseMonster;
    set => this.baseMonster = value;
  }
}
