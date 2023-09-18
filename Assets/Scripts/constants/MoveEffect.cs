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
    Growth,
    Minimize,
    DefenseCurl,
    AllUp1,
    BellyDrum,
    Charge,
    AttackDefenseUp1,
    AttackSpeedUp1,
    DefenseSpDefUp1,
    SpAtkSpDefUp1,
    AttackDefenseDown1,
    //Other status moves
    LeechSeed,
    Disable,
    Torment,
    Taunt,
    Encore,
    ForcedSwitch,
    PerishSong,
    Attract,
    Trap,
    Curse,
    PsychUp,
    Spite,
    Nightmare,
    MindReader,
    Foresight,
    Memento,
    Trick,
    RolePlay,
    SkillSwap,
    Yawn,
    Imprison,
    Snatch,
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
    Flinch,
    FakeOut,
    Absorb50,
    PayDay,
    SmellingSalts,
    SecretPower,
    RapidSpin,
    Thief,
    KnockOff,
    BreakScreens,
    //Unique attack types
    ChargingAttack,
    Recharge,
    ContinuousDamage,
    Thrash,
    Counter,
    SelfDestruct,
    DreamEater,
    OHKO,
    Rage,
    WeightPower,
    HealthPower,
    Return,
    Frustration,
    Rollout,
    FalseSwipe,
    PainSplit,
    Snore,
    Magnitude,
    Reversal,
    HiddenPower,
    FuryCutter,
    Pursuit,
    FutureSight,
    Present,
    BeatUp,
    Uproar,
    Facade,
    NaturePower,
    Revenge,
    Endeavor,
    WeatherBall,
    //Paired effects
    Bide,
    BideHit,
    Stockpile,
    Swallow,
    SpitUp,
    FocusPunchWindup,
    FocusPunchAttack,
    //Field effects
    Mist,
    Safeguard,
    Haze,
    Reflect,
    LightScreen,
    Weather,
    Spikes,
    MudSport,
    WaterSport,
    //Self-targeting effects
    Rest,
    Substitute,
    Transform,
    Teleport,
    BatonPass,
    SleepTalk,
    MirrorMove,
    Mimic,
    Metronome,
    DestinyBond,
    Grudge,
    Recycle,
    Conversion,
    Conversion2,
    Camouflage,
    Protect,
    Endure,
    MagicCoat,
    HealBell,
    Heal50,
    HealWeather,
    HealStatus,
    Roost,
    Sketch,
    Wish,
    Assist,
    Ingrain,
    //Effects for doubles/triples
    FollowMe,
    HelpingHand,
}