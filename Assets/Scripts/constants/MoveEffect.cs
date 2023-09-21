public enum MoveEffect : ushort
{
    //No added effect
    None,
    Hit,
    //Multi-hit moves
    MultiHit2,
    MultiHit2to5,
    Twineedle,
    TripleHit,
    //Status-inducing moves
    Burn,
    Paralyze,
    Poison,
    Toxic,
    Freeze,
    Sleep,
    Confuse,
    TriAttack,
    Swagger,
    Flatter,
    //Stat changes
    AttackUp1,
    AttackUp2,
    DefenseUp1,
    DefenseUp2,
    SpAtkUp3,
    SpDefUp2,
    SpeedUp2,
    EvasionUp1,
    EvasionUp2,
    CritRateUp2,
    AttackDown1,
    AttackDown2,
    DefenseDown1,
    DefenseDown2,
    SpAtkDown1,
    SpAtkDown2,
    SpDefDown1,
    SpDefDown2,
    SpeedDown1,
    SpeedDown2,
    AccuracyDown1,
    EvasionDown2,
    AttackDefenseUp1,
    AttackSpeedUp1,
    DefenseSpDefUp1,
    SpAtkSpDefUp1,
    AttackDefenseDown1,
    DefenseSpDefDown1,
    AllUp1,
    Growth,
    Minimize,
    DefenseCurl,
    BellyDrum,
    Charge,
    Acupressure,
    //Other status moves
    Attract,
    Curse,
    Disable,
    Embargo,
    Encore,
    ForcedSwitch,
    Foresight,
    GuardSwap,
    HealBlock,
    Imprison,
    LeechSeed,
    MeFirst,
    Memento,
    MindReader,
    MiracleEye,
    Nightmare,
    PerishSong,
    PowerSwap,
    PsychoShift,
    PsychUp,
    RolePlay,
    SkillSwap,
    Snatch,
    Spite,
    SuppressAbility,
    Taunt,
    Torment,
    Trap,
    Trick,
    WorrySeed,
    Yawn,
    //Direct damage
    Direct20,
    Direct40,
    DirectLevel,
    Psywave,
    SuperFang,
    //Recoil
    Recoil33,
    Recoil25,
    Recoil25Max,
    Crash50Max,
    VoltTackle,
    //Other added effects
    Absorb50,
    BreakScreens,
    FakeOut,
    Flinch,
    KnockOff,
    PayDay,
    RapidSpin,
    SecretPower,
    SmellingSalts,
    Thief,
    WakeUpSlap,
    //Unique attack types
    Assurance,
    BeatUp,
    ChargingAttack,
    ContinuousDamage,
    Counter,
    DreamEater,
    Endeavor,
    FalseSwipe,
    Feint,
    Fling,
    FuryCutter,
    FutureSight,
    HiddenPower,
    LastResort,
    MetalBurst,
    NaturalGift,
    NaturePower,
    OHKO,
    PainSplit,
    Pluck,
    Present,
    Rage,
    Recharge,
    Rollout,
    SelfDestruct,
    Snore,
    SuckerPunch,
    SwitchHit,
    Thrash,
    TrumpCard,
    Uproar,
    //Moves which double in power under certain conditions
    Brine,
    Facade,
    Pursuit,
    Revenge,
    Payback,
    WeatherBall,
    //Moves with unique power calcs
    WeightPower,
    HealthPower,
    Reversal,
    TargetHealthPower,
    LowSpeedPower,
    UserStatPower,
    TargetStatPower,
    Return,
    Frustration,
    Magnitude,
    //Paired effects
    Bide,
    BideHit,
    Stockpile,
    Swallow,
    SpitUp,
    FocusPunchWindup,
    FocusPunchAttack,
    //Field effects
    Gravity,
    Haze,
    LightScreen,
    LuckyChant,
    Mist,
    MudSport,
    Reflect,
    Safeguard,
    Spikes,
    Tailwind,
    WaterSport,
    Weather,
    //Self-targeting effects
    Assist,
    BatonPass,
    Camouflage,
    Conversion,
    Conversion2,
    Copycat,
    DestinyBond,
    Endure,
    Grudge,
    Heal50,
    HealBell,
    HealingWish,
    HealStatus,
    HealWeather,
    Ingrain,
    MagicCoat,
    Metronome,
    Mimic,
    MirrorMove,
    PowerTrick,
    Protect,
    Recycle,
    Rest,
    Roost,
    SleepTalk,
    Substitute,
    Teleport,
    Transform,
    Sketch,
    Wish,
    //Effects for doubles/triples
    FollowMe,
    HelpingHand,
}