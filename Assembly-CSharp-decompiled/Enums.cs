// Decompiled with JetBrains decompiler
// Type: Enums
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

// Assembly location: D:\Steam Games\steamapps\common\Across the Obelisk\AcrossTheObelisk_Data\Managed\Assembly-CSharp.dll

public class Enums
{
  public enum GameMode
  {
    SinglePlayer,
    Multiplayer,
  }

  public enum GameStatus
  {
    NewGame,
    LoadGame,
  }

  public enum GameType
  {
    Adventure,
    Challenge,
    WeeklyChallenge,
  }

  public enum ConfigSpeed
  {
    Slow,
    Fast,
    Ultrafast,
  }

  public enum ItemTarget
  {
    Self,
    RandomHero,
    RandomEnemy,
    AllHero,
    AllEnemy,
    CurrentTarget,
    SelfEnemy,
    HighestFlatHpHero,
    LowestFlatHpHero,
    HighestFlatHpEnemy,
    LowestFlatHpEnemy,
    Random,
  }

  public enum ItemSlot
  {
    None,
    Weapon,
    Armor,
    Jewelry,
    Accesory,
    Pet,
    AllWithoutPet,
    AllIncludedPet,
  }

  public enum CardClass
  {
    Warrior,
    Mage,
    Healer,
    Scout,
    MagicKnight,
    Monster,
    Injury,
    Boon,
    Item,
    Special,
    Enchantment,
    None,
  }

  public enum CardRarity
  {
    Common,
    Uncommon,
    Rare,
    Epic,
    Mythic,
  }

  public enum CardType
  {
    None,
    Melee_Attack,
    Ranged_Attack,
    Magic_Attack,
    Defense,
    Fire_Spell,
    Cold_Spell,
    Lightning_Spell,
    Mind_Spell,
    Shadow_Spell,
    Holy_Spell,
    Curse_Spell,
    Healing_Spell,
    Book,
    Small_Weapon,
    Song,
    Skill,
    Power,
    Injury,
    Attack,
    Spell,
    Boon,
    Weapon,
    Armor,
    Jewelry,
    Accesory,
    Pet,
    Corruption,
    Enchantment,
    Food,
  }

  public enum CardUpgraded
  {
    No,
    A,
    B,
    Rare,
  }

  public enum CardTargetType
  {
    Single,
    Global,
    SingleSided,
  }

  public enum CardTargetSide
  {
    Enemy,
    Friend,
    Anyone,
    Self,
    FriendNotSelf,
  }

  public enum CardTargetPosition
  {
    Anywhere,
    Front,
    Random,
    Back,
    Middle,
    Slowest,
    Fastest,
    LeastHP,
    MostHP,
  }

  public enum CardPlace
  {
    Discard,
    TopDeck,
    BottomDeck,
    RandomDeck,
    Vanish,
    Hand,
    Cast,
  }

  public enum CardFrom
  {
    Deck,
    Discard,
    Game,
    Hand,
    Vanish,
  }

  public enum CardSpecialValue
  {
    None,
    AuraCurseYours,
    AuraCurseTarget,
    CardsHand,
    CardsDeck,
    CardsDiscard,
    HealthYours,
    HealthTarget,
    CardsVanish,
    CardsDeckTarget,
    CardsDiscardTarget,
    CardsVanishTarget,
    SpeedYours,
    SpeedTarget,
    SpeedDifference,
    MissingHealthYours,
    MissingHealthTarget,
    DiscardedCards,
    VanishedCards,
  }

  public enum DamageType
  {
    None,
    Slashing,
    Blunt,
    Piercing,
    Fire,
    Cold,
    Lightning,
    Mind,
    Holy,
    Shadow,
    All,
  }

  public enum EffectRepeatTarget
  {
    Same,
    Random,
    Chain,
    NoRepeat,
    NeverRepeat,
  }

  public enum CharacterStat
  {
    None,
    Hp,
    Speed,
    Energy,
    EnergyTurn,
  }

  public enum HeroClass
  {
    Warrior,
    Mage,
    Healer,
    Scout,
    MagicKnight,
    Monster,
    None,
  }

  public enum CombatScrollEffectType
  {
    Damage,
    Heal,
    Aura,
    Curse,
    Energy,
    Text,
    Block,
    Trait,
    Weapon,
    Armor,
    Jewelry,
    Accesory,
    Corruption,
  }

  public enum TargetCast
  {
    Random,
    Front,
    Middle,
    Back,
    AnyButFront,
    AnyButBack,
    LessHealthPercent,
    LessHealthFlat,
    MoreHealthPercent,
    MoreHealthFlat,
    LessHealthAbsolute,
    MoreHealthAbsolute,
    LessInitiative,
    MoreInitiative,
  }

  public enum OnlyCastIf
  {
    Always,
    TargetLifeLessThanPercent,
    TargetHasNotAuraCurse,
    TargetLifeHigherThanPercent,
    TargetHasAuraCurse,
    TargetHasAnyAura,
    TargetHasAnyCurse,
  }

  public enum LogType
  {
    Text,
    Damage,
    Heal,
    Aura,
    Curse,
  }

  public enum EffectTrailAngle
  {
    Horizontal,
    Parabolic,
    Diagonal,
    Vertical,
  }

  public enum EventActivation
  {
    None,
    BeginCombat,
    BeginRound,
    BeginTurn,
    EndTurn,
    EndRound,
    DrawCard,
    CastCard,
    Hit,
    Hitted,
    Block,
    Blocked,
    Evade,
    Evaded,
    Heal,
    Healed,
    FinishCast,
    BeginCombatEnd,
    CharacterAssign,
    Damage,
    Damaged,
    PreBeginCombat,
    BeginTurnCardsDealt,
    AuraCurseSet,
    BeginTurnAboutToDealCards,
    Killed,
    DestroyItem,
    CorruptionCombatStart,
    CorruptionBeginRound,
    DamagedSecondary,
    FinishFinishCast,
    PreFinishCast,
    Resurrect,
    ItemActivation,
    TraitActivation,
    CharacterKilled,
  }

  public enum Zone
  {
    Senenthia,
    Velkarath,
    Aquarfall,
    Faeborg,
    VoidLow,
    VoidHigh,
    Sectarium,
    SpiderLair,
    FrozenSewers,
    BlackForge,
    None,
    WolfWars,
    Ulminin,
    Pyramid,
  }

  public enum NodeGround
  {
    None,
    HeavyRain,
    ExtremeHeat,
    FreezingCold,
    HolyGround,
    Graveyard,
    PoisonousAir,
  }

  public enum CombatBackground
  {
    Senenthia_Plains,
    Senenthia_Forest,
    Senenthia_Night,
    Sectarium,
    Velkarath_Stone,
    Velkarath_Lava0,
    Velkarath_Lavavolcan,
    Velkarath_Lava2,
    Spider_Lair,
    Aquarfall_Main,
    Aquarfall_Water,
    Aquarfall_Boss,
    Senenthia_Deepforest,
    Void_Regular,
    Void_Stairs,
    Void_Obelisk,
    Void_StairsClose,
    Void_StairsClose2,
    Void_RegularUP,
    Void_ObeliskClose,
    Void_ObeliskClose2,
    Senenthia_Dia,
    Senenthia_Tarde,
    Senenthia_LobosTarde,
    Senenthia_LobosNoche,
    Senenthia_BosqueEntrada,
    Senenthia_BosqueBoss,
    Senenthia_Bosque,
    Velkarath_Lava1,
    Velkarath_Arena,
    Velkarath_Cave,
    Void_Boss,
    Void_BossFinal,
    Void_Obelisk_Lila,
    Void_ObeliskClose2_Lila,
    Void_Regular_Rosa,
    Void_Stairs_Rosa,
    Void_StairsClose_Rosa,
    Void_StairsClose2_Rosa,
    Void_Twins,
    Faeborg_Armeria,
    Faeborg_Bosque,
    Faeborg_BosqueCastor,
    Faeborg_Boss,
    Faeborg_Ciudad,
    Faeborg_Costa,
    Faeborg_Rabbit,
    Forge_Boss,
    Forge_Mid,
    Forge_Pared,
    Sewers,
    Wolfwars_Boss,
    Wolfwars_Bree,
    Wolfwars_Forest,
    Wolfwars_Talado,
    Wolfwars_Torre,
    Ulminin_Arriba,
    Ulminin_Arriba2,
    Ulminin_Centro,
    Ulminin_Centro2,
    Ulminin_Desierto,
    Ulminin_Desierto2,
    Ulminin_DesiertoGusano,
    Ulminin_Oasis,
    Ulminin_Oasis2,
    Ulminin_Primero,
    Ulminin_Catacombs,
    Ulminin_CatacombsBoss,
    Ulminin_Pyramid,
    Ulminin_PyramidBoss,
  }

  public enum CombatTier
  {
    T0,
    T1,
    T2,
    T3,
    T4,
    T5,
    T6,
    T7,
    T8,
    T9,
    T10,
    T11,
    T12,
    T13,
  }

  public enum CombatUnit
  {
    All,
    Heroes,
    Monsters,
    Hero0,
    Hero1,
    Hero2,
    Hero3,
    Monster0,
    Monster1,
    Monster2,
    Monster3,
  }

  public enum RollMode
  {
    HigherOrEqual,
    LowerOrEqual,
    Highest,
    Lowest,
    Closest,
  }

  public enum RollTarget
  {
    Single,
    Competition,
    Group,
    Character,
  }

  public enum EventAction
  {
    None,
    CharacterName,
    Combat,
    Jump,
    Run,
    Bribe,
    Threaten,
    Persuade,
    Stealth,
    Steal,
    SneakAway,
    Leave,
    Loot,
    Profane,
    Forage,
    Decline,
    Accept,
    Extorsion,
    Ambush,
    Race,
    Rest,
    Bury,
    Dig,
    Break,
    Bet,
    Play,
    Fishing,
    Pay,
    Bargain,
    Continue,
    Explore,
    Enter,
    Pretend,
    Capture,
    Throw,
    Look,
    Examine,
    Repair,
    Rebuild,
    Heal,
    Climb,
    Evade,
    Use,
    Ask,
    Answer,
    Open,
    Buy,
    Murder,
    Burn,
    Kill,
    Give,
    Talk,
    Sing,
    Read,
  }

  public enum MapIconShader
  {
    None,
    Green,
    Blue,
    Orange,
    Purple,
    Red,
    Black,
  }

  public enum PerkCost
  {
    PerkCostBase = 1,
    PerkCostAdvanced = 3,
  }
}
