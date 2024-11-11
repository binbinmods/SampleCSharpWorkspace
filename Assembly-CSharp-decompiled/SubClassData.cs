// Decompiled with JetBrains decompiler
// Type: SubClassData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(fileName = "New SubClass", menuName = "SubClass Data", order = 53)]
[Serializable]
public class SubClassData : ScriptableObject
{
  [SerializeField]
  private string subClassName;
  private string id;
  [SerializeField]
  private string characterName;
  [SerializeField]
  private bool mainCharacter;
  [SerializeField]
  private bool expansionCharacter;
  [TextArea]
  [SerializeField]
  private string characterDescription;
  [TextArea]
  [SerializeField]
  private string characterDescriptionStrength;
  [SerializeField]
  private Sprite sprite;
  [SerializeField]
  private GameObject gameObjectAnimated;
  [SerializeField]
  private AudioClip actionSound;
  [SerializeField]
  private AudioClip hitSound;
  [SerializeField]
  private Sprite spriteBorder;
  [SerializeField]
  private Sprite spriteBorderSmall;
  [SerializeField]
  private Sprite spriteBorderLocked;
  [SerializeField]
  private Sprite spriteSpeed;
  [SerializeField]
  private Sprite spritePortrait;
  [Header("Class")]
  [SerializeField]
  private Enums.HeroClass heroClass;
  [SerializeField]
  private Enums.HeroClass heroClassSecondary;
  [Header("Misc")]
  [SerializeField]
  private float fluffOffsetX;
  [SerializeField]
  private float fluffOffsetY;
  [SerializeField]
  private bool female;
  [Header("DLC Requeriment")]
  [SerializeField]
  private string sku;
  [Header("Sticker")]
  [SerializeField]
  private Sprite stickerBase;
  [SerializeField]
  private Sprite stickerLove;
  [SerializeField]
  private Sprite stickerSurprise;
  [SerializeField]
  private Sprite stickerAngry;
  [SerializeField]
  private Sprite stickerIndiferent;
  [SerializeField]
  private float stickerOffsetX;
  [Header("Ingame Parameters")]
  [SerializeField]
  private int orderInList;
  [SerializeField]
  private bool blocked = true;
  [Header("Main Stats")]
  [SerializeField]
  private int speed;
  [SerializeField]
  private int hp;
  [SerializeField]
  private int energy;
  [SerializeField]
  private int energyTurn;
  [Header("Resists")]
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
  [Header("Item")]
  [SerializeField]
  private CardData item;
  [Header("LevelUp")]
  [SerializeField]
  private int[] maxHp;
  [Header("Cards")]
  [SerializeField]
  private HeroCards[] cards;
  [Header("Traits")]
  [SerializeField]
  private List<TraitData> traits;
  [Header("Trait List")]
  [SerializeField]
  private TraitData trait0;
  [Space(10f)]
  [SerializeField]
  private TraitData trait1A;
  [SerializeField]
  private CardData trait1ACard;
  [SerializeField]
  private TraitData trait1B;
  [SerializeField]
  private CardData trait1BCard;
  [Space(10f)]
  [SerializeField]
  private TraitData trait2A;
  [SerializeField]
  private TraitData trait2B;
  [Space(10f)]
  [SerializeField]
  private TraitData trait3A;
  [SerializeField]
  private CardData trait3ACard;
  [SerializeField]
  private TraitData trait3B;
  [SerializeField]
  private CardData trait3BCard;
  [Space(10f)]
  [SerializeField]
  private TraitData trait4A;
  [SerializeField]
  private TraitData trait4B;
  [Header("Challenge packs")]
  [SerializeField]
  private PackData challengePack0;
  [SerializeField]
  private PackData challengePack1;
  [SerializeField]
  private PackData challengePack2;
  [SerializeField]
  private PackData challengePack3;
  [SerializeField]
  private PackData challengePack4;
  [SerializeField]
  private PackData challengePack5;
  [SerializeField]
  private PackData challengePack6;

  private void Awake() => this.id = Regex.Replace(this.subClassName, "\\s+", "").ToLower();

  public Sprite GetEmoteBase() => this.stickerBase;

  public Sprite GetEmote(int _action)
  {
    switch (_action)
    {
      case 0:
        return this.stickerLove;
      case 1:
        return this.stickerSurprise;
      case 4:
        return this.stickerIndiferent;
      case 5:
        return this.stickerAngry;
      default:
        return (Sprite) null;
    }
  }

  public int GetTraitLevel(string traitName)
  {
    traitName = traitName.ToLower();
    if (this.trait0.Id == traitName)
      return 0;
    if (this.trait1A.Id == traitName || this.trait1B.Id == traitName)
      return 1;
    if (this.trait2A.Id == traitName || this.trait2B.Id == traitName)
      return 2;
    if (this.trait3A.Id == traitName || this.trait3B.Id == traitName)
      return 3;
    return this.trait4A.Id == traitName || this.trait4B.Id == traitName ? 4 : -1;
  }

  public List<string> GetCardsId()
  {
    List<string> cardsId = new List<string>();
    for (int index = 0; index < this.cards.Length; ++index)
    {
      if (!cardsId.Contains(this.cards[index].Card.Id))
        cardsId.Add(this.cards[index].Card.Id);
    }
    return cardsId;
  }

  public string SubClassName
  {
    get => this.subClassName;
    set => this.subClassName = value;
  }

  public string Id
  {
    get => this.id;
    set => this.id = value;
  }

  public string CharacterName
  {
    get => this.characterName;
    set => this.characterName = value;
  }

  public string CharacterDescription
  {
    get => this.characterDescription;
    set => this.characterDescription = value;
  }

  public string CharacterDescriptionStrength
  {
    get => this.characterDescriptionStrength;
    set => this.characterDescriptionStrength = value;
  }

  public Sprite Sprite
  {
    get => this.sprite;
    set => this.sprite = value;
  }

  public GameObject GameObjectAnimated
  {
    get => this.gameObjectAnimated;
    set => this.gameObjectAnimated = value;
  }

  public Enums.HeroClass HeroClass
  {
    get => this.heroClass;
    set => this.heroClass = value;
  }

  public int OrderInList
  {
    get => this.orderInList;
    set => this.orderInList = value;
  }

  public bool Blocked
  {
    get => this.blocked;
    set => this.blocked = value;
  }

  public int Speed
  {
    get => this.speed;
    set => this.speed = value;
  }

  public int Hp
  {
    get => this.hp;
    set => this.hp = value;
  }

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

  public HeroCards[] Cards
  {
    get => this.cards;
    set => this.cards = value;
  }

  public Sprite SpriteSpeed
  {
    get => this.spriteSpeed;
    set => this.spriteSpeed = value;
  }

  public Sprite SpritePortrait
  {
    get => this.spritePortrait;
    set => this.spritePortrait = value;
  }

  public List<TraitData> Traits
  {
    get => this.traits;
    set => this.traits = value;
  }

  public TraitData Trait0
  {
    get => this.trait0;
    set => this.trait0 = value;
  }

  public TraitData Trait1A
  {
    get => this.trait1A;
    set => this.trait1A = value;
  }

  public TraitData Trait1B
  {
    get => this.trait1B;
    set => this.trait1B = value;
  }

  public TraitData Trait2A
  {
    get => this.trait2A;
    set => this.trait2A = value;
  }

  public TraitData Trait2B
  {
    get => this.trait2B;
    set => this.trait2B = value;
  }

  public TraitData Trait3A
  {
    get => this.trait3A;
    set => this.trait3A = value;
  }

  public TraitData Trait3B
  {
    get => this.trait3B;
    set => this.trait3B = value;
  }

  public TraitData Trait4A
  {
    get => this.trait4A;
    set => this.trait4A = value;
  }

  public TraitData Trait4B
  {
    get => this.trait4B;
    set => this.trait4B = value;
  }

  public AudioClip HitSound
  {
    get => this.hitSound;
    set => this.hitSound = value;
  }

  public Sprite SpriteBorder
  {
    get => this.spriteBorder;
    set => this.spriteBorder = value;
  }

  public Sprite SpriteBorderSmall
  {
    get => this.spriteBorderSmall;
    set => this.spriteBorderSmall = value;
  }

  public int[] MaxHp
  {
    get => this.maxHp;
    set => this.maxHp = value;
  }

  public CardData Item
  {
    get => this.item;
    set => this.item = value;
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

  public Sprite SpriteBorderLocked
  {
    get => this.spriteBorderLocked;
    set => this.spriteBorderLocked = value;
  }

  public CardData Trait1ACard
  {
    get => this.trait1ACard;
    set => this.trait1ACard = value;
  }

  public CardData Trait1BCard
  {
    get => this.trait1BCard;
    set => this.trait1BCard = value;
  }

  public CardData Trait3ACard
  {
    get => this.trait3ACard;
    set => this.trait3ACard = value;
  }

  public CardData Trait3BCard
  {
    get => this.trait3BCard;
    set => this.trait3BCard = value;
  }

  public AudioClip ActionSound
  {
    get => this.actionSound;
    set => this.actionSound = value;
  }

  public Sprite StickerLove
  {
    get => this.stickerLove;
    set => this.stickerLove = value;
  }

  public Sprite StickerSurprise
  {
    get => this.stickerSurprise;
    set => this.stickerSurprise = value;
  }

  public Sprite StickerAngry
  {
    get => this.stickerAngry;
    set => this.stickerAngry = value;
  }

  public Sprite StickerIndiferent
  {
    get => this.stickerIndiferent;
    set => this.stickerIndiferent = value;
  }

  public Sprite StickerBase
  {
    get => this.stickerBase;
    set => this.stickerBase = value;
  }

  public float StickerOffsetX
  {
    get => this.stickerOffsetX;
    set => this.stickerOffsetX = value;
  }

  public bool Female
  {
    get => this.female;
    set => this.female = value;
  }

  public PackData ChallengePack0
  {
    get => this.challengePack0;
    set => this.challengePack0 = value;
  }

  public PackData ChallengePack1
  {
    get => this.challengePack1;
    set => this.challengePack1 = value;
  }

  public PackData ChallengePack2
  {
    get => this.challengePack2;
    set => this.challengePack2 = value;
  }

  public PackData ChallengePack3
  {
    get => this.challengePack3;
    set => this.challengePack3 = value;
  }

  public PackData ChallengePack4
  {
    get => this.challengePack4;
    set => this.challengePack4 = value;
  }

  public PackData ChallengePack5
  {
    get => this.challengePack5;
    set => this.challengePack5 = value;
  }

  public PackData ChallengePack6
  {
    get => this.challengePack6;
    set => this.challengePack6 = value;
  }

  public bool MainCharacter
  {
    get => this.mainCharacter;
    set => this.mainCharacter = value;
  }

  public bool ExpansionCharacter
  {
    get => this.expansionCharacter;
    set => this.expansionCharacter = value;
  }

  public string Sku
  {
    get => this.sku;
    set => this.sku = value;
  }

  public Enums.HeroClass HeroClassSecondary
  {
    get => this.heroClassSecondary;
    set => this.heroClassSecondary = value;
  }

  public bool IsDualClass() => this.heroClassSecondary != Enums.HeroClass.None;
}
