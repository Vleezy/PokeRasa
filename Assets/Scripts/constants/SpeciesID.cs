using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpeciesID
{
    //Gen 1
    Missingno,
    Bulbasaur,
    Ivysaur,
    Venusaur,
    Charmander,
    Charmeleon,
    Charizard,
    Squirtle,
    Wartortle,
    Blastoise,
    Caterpie,
    Metapod,
    Butterfree,
    Weedle,
    Kakuna,
    Beedrill,
    Pidgey,
    Pidgeotto,
    Pidgeot,
    Rattata,
    Raticate,
    Spearow,
    Fearow,
    Ekans,
    Arbok,
    Pikachu,
    Raichu,
    Sandshrew,
    Sandslash,
    NidoranF,
    Nidorina,
    Nidoqueen,
    NidoranM,
    Nidorino,
    Nidoking,
    Clefairy,
    Clefable,
    Vulpix,
    Ninetales,
    Jigglypuff,
    Wigglytuff,
    Zubat,
    Golbat,
    Oddish,
    Gloom,
    Vileplume,
    Paras,
    Parasect,
    Venonat,
    Venomoth,
    Diglett,
    Dugtrio,
    Meowth,
    Persian,
    Psyduck,
    Golduck,
    Mankey,
    Primeape,
    Growlithe,
    Arcanine,
    Poliwag,
    Poliwhirl,
    Poliwrath,
    Abra,
    Kadabra,
    Alakazam,
    Machop,
    Machoke,
    Machamp,
    Bellsprout,
    Weepinbell,
    Victreebel,
    Tentacool,
    Tentacruel,
    Geodude,
    Graveler,
    Golem,
    Ponyta,
    Rapidash,
    Slowpoke,
    Slowbro,
    Magnemite,
    Magneton,
    Farfetchd,
    Doduo,
    Dodrio,
    Seel,
    Dewgong,
    Grimer,
    Muk,
    Shellder,
    Cloyster,
    Gastly,
    Haunter,
    Gengar,
    Onix,
    Drowzee,
    Hypno,
    Krabby,
    Kingler,
    Voltorb,
    Electrode,
    Exeggcute,
    Exeggutor,
    Cubone,
    Marowak,
    Hitmonlee,
    Hitmonchan,
    Lickitung,
    Koffing,
    Weezing,
    Rhyhorn,
    Rhydon,
    Chansey,
    Tangela,
    Kangaskhan,
    Horsea,
    Seadra,
    Goldeen,
    Seaking,
    Staryu,
    Starmie,
    MrMime,
    Scyther,
    Jynx,
    Electabuzz,
    Magmar,
    Pinsir,
    Tauros,
    Magikarp,
    Gyarados,
    Lapras,
    Ditto,
    Eevee,
    Vaporeon,
    Jolteon,
    Flareon,
    Porygon,
    Omanyte,
    Omastar,
    Kabuto,
    Kabutops,
    Aerodactyl,
    Snorlax,
    Articuno,
    Zapdos,
    Moltres,
    Dratini,
    Dragonair,
    Dragonite,
    Mewtwo,
    Mew,
    //Gen 2
    Chikorita,
    Bayleef,
    Meganium,
    Cyndaquil,
    Quilava,
    Typhlosion,
    Totodile,
    Croconaw,
    Feraligatr,
    Sentret,
    Furret,
    Hoothoot,
    Noctowl,
    Ledyba,
    Ledian,
    Spinarak,
    Ariados,
    Crobat,
    Chinchou,
    Lanturn,
    Pichu,
    Cleffa,
    Igglybuff,
    Togepi,
    Togetic,
    Natu,
    Xatu,
    Mareep,
    Flaaffy,
    Ampharos,
    Bellossom,
    Marill,
    Azumarill,
    Sudowoodo,
    Politoed,
    Hoppip,
    Skiploom,
    Jumpluff,
    Aipom,
    Sunkern,
    Sunflora,
    Yanma,
    Wooper,
    Quagsire,
    Espeon,
    Umbreon,
    Murkrow,
    Slowking,
    Misdreavus,
    Unown,
    Wobbuffet,
    Girafarig,
    Pineco,
    Forretress,
    Dunsparce,
    Gligar,
    Steelix,
    Snubbull,
    Granbull,
    Qwilfish,
    Scizor,
    Shuckle,
    Heracross,
    Sneasel,
    Teddiursa,
    Ursaring,
    Slugma,
    Magcargo,
    Swinub,
    Piloswine,
    Corsola,
    Remoraid,
    Octillery,
    Delibird,
    Mantine,
    Skarmory,
    Houndour,
    Houndoom,
    Kingdra,
    Phanpy,
    Donphan,
    Porygon2,
    Stantler,
    Smeargle,
    Tyrogue,
    Hitmontop,
    Smoochum,
    Elekid,
    Magby,
    Miltank,
    Blissey,
    Raikou,
    Entei,
    Suicune,
    Larvitar,
    Pupitar,
    Tyranitar,
    Lugia,
    HoOh,
    Celebi,

    //Gen 3
    Treecko,
    Grovyle,
    Sceptile,
    Torchic,
    Combusken,
    Blaziken,
    Mudkip,
    Marshtomp,
    Swampert,
    Poochyena,
    Mightyena,
    Zigzagoon,
    Linoone,
    Wurmple,
    Silcoon,
    Beautifly,
    Cascoon,
    Dustox,
    Lotad,
    Lombre,
    Ludicolo,
    Seedot,
    Nuzleaf,
    Shiftry,
    Taillow,
    Swellow,
    Wingull,
    Pelipper,
    Ralts,
    Kirlia,
    Gardevoir,
    Surskit,
    Masquerain,
    Shroomish,
    Breloom,
    Slakoth,
    Vigoroth,
    Slaking,
    Nincada,
    Ninjask,
    Shedinja,
    Whismur,
    Loudred,
    Exploud,
    Makuhita,
    Hariyama,
    Azurill,
    Nosepass,
    Skitty,
    Delcatty,
    Sableye,
    Mawile,
    Aron,
    Lairon,
    Aggron,
    Meditite,
    Medicham,
    Electrike,
    Manectric,
    Plusle,
    Minun,
    Volbeat,
    Illumise,
    Roselia,
    Gulpin,
    Swalot,
    Carvanha,
    Sharpedo,
    Wailmer,
    Wailord,
    Numel,
    Camerupt,
    Torkoal,
    Spoink,
    Grumpig,
    Spinda,
    Trapinch,
    Vibrava,
    Flygon,
    Cacnea,
    Cacturne,
    Swablu,
    Altaria,
    Zangoose,
    Seviper,
    Lunatone,
    Solrock,
    Barboach,
    Whiscash,
    Corphish,
    Crawdaunt,
    Baltoy,
    Claydol,
    Lileep,
    Cradily,
    Anorith,
    Armaldo,
    Feebas,
    Milotic,
    Castform,
    Kecleon,
    Shuppet,
    Banette,
    Duskull,
    Dusclops,
    Tropius,
    Chimecho,
    Absol,
    Wynaut,
    Snorunt,
    Glalie,
    Spheal,
    Sealeo,
    Walrein,
    Clamperl,
    Huntail,
    Gorebyss,
    Relicanth,
    Luvdisc,
    Bagon,
    Shelgon,
    Salamence,
    Beldum,
    Metang,
    Metagross,
    Regirock,
    Regice,
    Registeel,
    Latias,
    Latios,
    Kyogre,
    Groudon,
    Rayquaza,
    Jirachi,
    Deoxys,

    //Gen 4
    Turtwig,
    Grotle,
    Torterra,
    Chimchar,
    Monferno,
    Infernape,
    Piplup,
    Prinplup,
    Empoleon,
    Starly,
    Staravia,
    Staraptor,
    Bidoof,
    Bibarel,
    Kricketot,
    Kricketune,
    Shinx,
    Luxio,
    Luxray,
    Budew,
    Roserade,
    Cranidos,
    Rampardos,
    Shieldon,
    Bastiodon,
    Burmy,
    Wormadam,
    Mothim,
    Combee,
    Vespiquen,
    Pachirisu,
    Buizel,
    Floatzel,
    Cherubi,
    Cherrim,
    Shellos,
    Gastrodon,
    Ambipom,
    Drifloon,
    Drifblim,
    Buneary,
    Lopunny,
    Mismagius,
    Honchkrow,
    Glameow,
    Purugly,
    Chingling,
    Stunky,
    Skuntank,
    Bronzor,
    Bronzong,
    Bonsly,
    MimeJr,
    Happiny,
    Chatot,
    Spiritomb,
    Gible,
    Gabite,
    Garchomp,
    Munchlax,
    Riolu,
    Lucario,
    Hippopotas,
    Hippowdon,
    Skorupi,
    Drapion,
    Croagunk,
    Toxicroak,
    Carnivine,
    Finneon,
    Lumineon,
    Mantyke,
    Snover,
    Abomasnow,
    Weavile,
    Magnezone,
    Lickilicky,
    Rhyperior,
    Tangrowth,
    Electivire,
    Magmortar,
    Togekiss,
    Yanmega,
    Leafeon,
    Glaceon,
    Gliscor,
    Mamoswine,
    PorygonZ,
    Gallade,
    Probopass,
    Dusknoir,
    Froslass,
    Rotom,
    Uxie,
    Mesprit,
    Azelf,
    Dialga,
    Palkia,
    Heatran,
    Regigigas,
    Giratina,
    Cresselia,
    Phione,
    Manaphy,
    Darkrai,
    Shaymin,
    Arceus,

    //Gen 5
    Victini,
    Snivy,
    Servine,
    Serperior,
    Tepig,
    Pignite,
    Emboar,
    Oshawott,
    Dewott,
    Samurott,
    Patrat,
    Watchog,
    Lillipup,
    Herdier,
    Stoutland,
    Purrloin,
    Liepard,
    Pansage,
    Simisage,
    Pansear,
    Simisear,
    Panpour,
    Simipour,
    Munna,
    Musharna,
    Pidove,
    Tranquill,
    Unfezant,
    Blitzle,
    Zebstrika,
    Roggenrola,
    Boldore,
    Gigalith,
    Woobat,
    Swoobat,
    Drilbur,
    Excadrill,
    Audino,
    Timburr,
    Gurdurr,
    Conkeldurr,
    Tympole,
    Palpitoad,
    Seismitoad,
    Throh,
    Sawk,
    Sewaddle,
    Swadloon,
    Leavanny,
    Venipede,
    Whirlipede,
    Scolipede,
    Cottonee,
    Whimsicott,
    Petilil,
    Lilligant,
    BasculinRed,
    Sandile,
    Krokorok,
    Krookodile,
    Darumaka,
    Darmanitan,
    Maractus,
    Dwebble,
    Crustle,
    Scraggy,
    Scrafty,
    Sigilyph,
    Yamask,
    Cofagrigus,
    Tirtouga,
    Carracosta,
    Archen,
    Archeops,
    Trubbish,
    Garbodor,
    Zorua,
    Zoroark,
    Minccino,
    Cinccino,
    Gothita,
    Gothorita,
    Gothitelle,
    Solosis,
    Duosion,
    Reuniclus,
    Ducklett,
    Swanna,
    Vanillite,
    Vanillish,
    Vanilluxe,
    DeerlingSpring,
    SawsbuckSpring,
    Emolga,
    Karrablast,
    Escavalier,
    Foongus,
    Amoonguss,
    Frillish,
    Jellicent,
    Alomomola,
    Joltik,
    Galvantula,
    Ferroseed,
    Ferrothorn,
    Klink,
    Klang,
    Klinklang,
    Tynamo,
    Eelektrik,
    Eelektross,
    Elgyem,
    Beheeyem,
    Litwick,
    Lampent,
    Chandelure,
    Axew,
    Fraxure,
    Haxorus,
    Cubchoo,
    Beartic,
    Cryogonal,
    Shelmet,
    Accelgor,
    Stunfisk,
    Mienfoo,
    Mienshao,
    Druddigon,
    Golett,
    Golurk,
    Pawniard,
    Bisharp,
    Bouffalant,
    Rufflet,
    Braviary,
    Vullaby,
    Mandibuzz,
    Heatmor,
    Durant,
    Deino,
    Zweilous,
    Hydreigon,
    Larvesta,
    Volcarona,
    Cobalion,
    Terrakion,
    Virizion,
    TornadusI,
    ThundurusI,
    Reshiram,
    Zekrom,
    LandorusI,
    Kyurem,
    KeldeoOriginal,
    MeloettaAria,
    GenesectNormal,

    //Gen 6
    Chespin,
    Quilladin,
    Chesnaught,
    Fennekin,
    Braixen,
    Delphox,
    Froakie,
    Frogadier,
    Greninja,
    Bunnelby,
    Diggersby,
    Fletchling,
    Fletchinder,
    Talonflame,
    ScatterbugMeadow,
    SpewpaMeadow,
    VivillonMeadow,
    Litleo,
    Pyroar,
    FlabebeRed,
    FloetteRed,
    FlorgesRed,
    Skiddo,
    Gogoat,
    Pancham,
    Pangoro,
    FurfrouNatural,
    Espurr,
    MeowsticM,
    Honedge,
    Doublade,
    AegislashShield,
    Spritzee,
    Aromatisse,
    Swirlix,
    Slurpuff,
    Inkay,
    Malamar,
    Binacle,
    Barbaracle,
    Skrelp,
    Dragalge,
    Clauncher,
    Clawitzer,
    Helioptile,
    Heliolisk,
    Tyrunt,
    Tyrantrum,
    Amaura,
    Aururos,
    Sylveon,
    Hawlucha,
    Dedenne,
    Carbink,
    Goomy,
    Sliggoo,
    Goodra,
    Klefki,
    Phantump,
    Trevenant,
    PumpkabooAverage,
    GourgeistAverage,
    Bergmite,
    Avalugg,
    Noibat,
    Noivern,
    XerneasInactive,
    Yveltal,
    Zygarde50,
    Diancie,
    Hoopa,
    Volcanion,

    //Forms

    Unown_B,
    Unown_C,
    Unown_D,
    Unown_E,
    Unown_F,
    Unown_G,
    Unown_H,
    Unown_I,
    Unown_J,
    Unown_K,
    Unown_L,
    Unown_M,
    Unown_N,
    Unown_O,
    Unown_P,
    Unown_Q,
    Unown_R,
    Unown_S,
    Unown_T,
    Unown_U,
    Unown_V,
    Unown_W,
    Unown_X,
    Unown_Y,
    Unown_Z,
    UnownQuestion,
    UnownExclamation,

    CastformSunny,
    CastformRainy,
    CastformSnowy,

    KyogrePrimal,
    GroudonPrimal,

    DeoxysAttack,
    DeoxysDefense,
    DeoxysSpeed,

    BurmySand,
    BurmyTrash,
    WormadamSand,
    WormadamTrash,

    CherrimSunshine,

    ShellosEast,
    GastrodonEast,

    RotomHeat,
    RotomWash,
    RotomFrost,
    RotomFan,
    RotomMow,

    DialgaOrigin,
    PalkiaOrigin,
    GiratinaOrigin,

    ShayminSky,

    ArceusFighting,
    ArceusFlying,
    ArceusPoison,
    ArceusGround,
    ArceusRock,
    ArceusBug,
    ArceusGhost,
    ArceusSteel,
    ArceusFire,
    ArceusWater,
    ArceusGrass,
    ArceusElectric,
    ArceusIce,
    ArceusDragon,
    ArceusDark,
    ArceusFairy,

    BasculinBlue,

    DarmanitanZen,

    DeerlingSummer,
    DeerlingAutumn,
    DeerlingWinter,

    SawsbuckSummer,
    SawsbuckAutumn,
    SawsbuckWinter,

    TornadusT,

    ThundurusT,

    LandorusT,

    KyuremWhite,
    KyuremBlack,

    KeldeoResolute,

    MeloettaPirouette,

    GenesectDouse,
    GenesectShock,
    GenesectBurn,
    GenesectChill,

    GreninjaBB,

    ScatterbugArchipelago,
    ScatterbugContinental,
    ScatterbugElegant,
    ScatterbugGarden,
    ScatterbugHighPlains,
    ScatterbugIcySnow,
    ScatterbugJungle,
    ScatterbugMarine,
    ScatterbugModern,
    ScatterbugMonsoon,
    ScatterbugOcean,
    ScatterbugPolar,
    ScatterbugRiver,
    ScatterbugSandstorm,
    ScatterbugSavanna,
    ScatterbugSun,
    ScatterbugTundra,
    ScatterbugFancy,
    ScatterbugPokeBall,

    SpewpaArchipelago,
    SpewpaContinental,
    SpewpaElegant,
    SpewpaGarden,
    SpewpaHighPlains,
    SpewpaIcySnow,
    SpewpaJungle,
    SpewpaMarine,
    SpewpaModern,
    SpewpaMonsoon,
    SpewpaOcean,
    SpewpaPolar,
    SpewpaRiver,
    SpewpaSandstorm,
    SpewpaSavanna,
    SpewpaSun,
    SpewpaTundra,
    SpewpaFancy,
    SpewpaPokeBall,

    VivillonArchipelago,
    VivillonContinental,
    VivillonElegant,
    VivillonGarden,
    VivillonHighPlains,
    VivillonIcySnow,
    VivillonJungle,
    VivillonMarine,
    VivillonModern,
    VivillonMonsoon,
    VivillonOcean,
    VivillonPolar,
    VivillonRiver,
    VivillonSandstorm,
    VivillonSavanna,
    VivillonSun,
    VivillonTundra,
    VivillonFancy,
    VivillonPokeBall,

    FlabebeYellow,
    FlabebeOrange,
    FlabebeBlue,
    FlabebeWhite,

    FloetteYellow,
    FloetteOrange,
    FloetteBlue,
    FloetteWhite,
    FloetteEternal,

    FlorgesYellow,
    FlorgesOrange,
    FlorgesBlue,
    FlorgesWhite,

    FurfrouHeart,
    FurfrouStar,
    FurfrouDiamond,
    FurfrouDebutante,
    FurfrouMatron,
    FurfrouDandy,
    FurfrouLaReine,
    FurfrouKabuki,
    FurfrouPharaoh,

    MeowsticF,

    AegislashBlade,

    PumpkabooSmall,
    PumpkabooLarge,
    PumpkabooSuper,

    GourgeistSmall,
    GourgeistLarge,
    GourgeistSuper,

    XerneasActive,

    Zygarde10,
    Zygarde10PC,
    Zygarde50PC,
    ZygardeComplete,

    HoopaUnbound,


    //Megas

    VenusaurMega,
    CharizardMegaX,
    CharizardMegaY,
    BlastoiseMega,
    BeedrillMega,
    PidgeotMega,
    AlakazamMega,
    SlowbroMega,
    GengarMega,
    KangaskhanMega,
    PinsirMega,
    GyaradosMega,
    AerodactylMega,
    MewtwoMegaX,
    MewtwoMegaY,
    AmpharosMega,
    SteelixMega,
    ScizorMega,
    HeracrossMega,
    HoundoomMega,
    TyranitarMega,
    SceptileMega,
    BlazikenMega,
    SwampertMega,
    GardevoirMega,
    SableyeMega,
    MawileMega,
    AggronMega,
    MedichamMega,
    ManectricMega,
    SharpedoMega,
    CameruptMega,
    AltariaMega,
    BanetteMega,
    AbsolMega,
    GlalieMega,
    SalamenceMega,
    MetagrossMega,
    LatiasMega,
    LatiosMega,
    RayquazaMega,
    LopunnyMega,
    GarchompMega,
    LucarioMega,
    AbomasnowMega,
    GalladeMega,
    AudinoMega,
    DiancieMega,


    Count,

    Egg = Count + 1,

}