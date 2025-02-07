using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using Scene = SceneID;

public class Player : LoadedChar
{
    public bool[] TM = new bool[96];
    public bool[] HM = new bool[8];
    public bool[] keyItem = new bool[8];
    public bool[] storyFlags = new bool[(int)Flag.Count];
    public bool[] trainerFlags = new bool[(int)TrainerFlag.Count];
    public List<Box> boxes = new();
    public int money = 0;


    public Dictionary<ItemID, int> Bag;
    public Dictionary<MapData, bool[,]> neighborCollision;


    public List<TileTrigger> triggers;
    public List<TileTrigger> signposts;

    public BattleTerrain currentTerrain;
    //public MapID currentMap;
    public MapData lastMap = null;
    //public Vector2Int pos;
    //public Vector2Int moveTarget;
    public PlayerState state;
    //public bool active;
    public bool locked;
    //public Direction facing;

    public Dictionary<string, (MapData map, LoadedChar chara)> loadedChars;
    public LoadedChar opponent;

    public bool whichStep;

    public MapDirectory mapDirectory;

    //public CollisionID currentHeight;
    public MapManager mapManager;
    public PlayerMovement playerGraphics;
    public GUIManager announcer;

    public new GameObject camera;
    private GameObject blackScreen;

    public override IEnumerator WalkNorth() { yield return playerGraphics.WalkNorth(this, 0.3F); }
    public override IEnumerator WalkSouth() { yield return playerGraphics.WalkSouth(this, 0.3F); }
    public override IEnumerator WalkEast() { yield return playerGraphics.WalkEast(this, 0.3F); }
    public override IEnumerator WalkWest() { yield return playerGraphics.WalkWest(this, 0.3F); }
    public override IEnumerator BumpNorth() { yield return playerGraphics.BumpNorth(this, 0.3F); }
    public override IEnumerator BumpSouth() { yield return playerGraphics.BumpSouth(this, 0.3F); }
    public override IEnumerator BumpEast() { yield return playerGraphics.BumpEast(this, 0.3F); }
    public override IEnumerator BumpWest() { yield return playerGraphics.BumpWest(this, 0.3F); }
    public override IEnumerator FaceNorth() { yield return playerGraphics.FaceNorth(this, 0); }
    public override IEnumerator FaceSouth() { yield return playerGraphics.FaceSouth(this, 0); }
    public override IEnumerator FaceEast() { yield return playerGraphics.FaceEast(this, 0); }
    public override IEnumerator FaceWest() { yield return playerGraphics.FaceWest(this, 0); }
    public override IEnumerator RunNorth() => throw new NotImplementedException();
    public override IEnumerator RunSouth() => throw new NotImplementedException();
    public override IEnumerator RunEast() => throw new NotImplementedException();
    public override IEnumerator RunWest() => throw new NotImplementedException();

    public float textSpeed = 25;

    public Pokemon[] Party = new Pokemon[6];
    private int monsInParty
    {
        get
        {
            int mons = 0;
            for (int i = 0; i < 6; i++)
            {
                if (Party[i].exists) mons++;
            }
            return mons;
        }
    }

    public IEnumerator GetItem(ItemID item, int amount)
    {
        switch (Item.ItemTable[(int)item].type)
        {
            case ItemType.TM:
                //yield return Field.Announce("Received" + Item.ItemTable[item].name");
                TM[Item.ItemTable[(int)item].ItemSubdata[0]] = true;
                break;
            case ItemType.KeyItem:
                //yield return Field.Announce("Received" + Item.ItemTable[item].name");
                keyItem[Item.ItemTable[(int)item].ItemSubdata[0]] = true;
                break;
            default:
                if (Bag.ContainsKey(item))
                {
                    Bag[item] += amount;
                }
                else
                {
                    Bag.Add(item, amount);
                }
                break;
        }
        yield break;
    }

    public void DeactivateAll() { foreach ((MapData _, LoadedChar i) in loadedChars.Values) i.Deactivate(); }
    public void ActivateAll() { foreach ((MapData _, LoadedChar i) in loadedChars.Values) i.Activate(); }

    public void RemoveAllChars() => loadedChars = new();

    public bool TryAddMon(Pokemon mon)
    {
        SortParty();
        if (monsInParty >= 6)
        {
            return false;
        }
        else
        {
            Party[monsInParty] = mon;
            return true;
        }
    }

    private void SortParty()
    {
        int currentPos = 0;
        for (int i = 0; i < 6; i++)
        {
            if (Party[i].exists)
            {
                Party[currentPos] = Party[i];
                currentPos++;
            }
        }
        for (int i = currentPos; i < 6; i++)
        {
            Party[i] = Pokemon.MakeEmptyMon;
        }
    }

    public void EmptyParty()
    {
        for (int i = 0; i < 6; i++)
        {
            Party[i] = Pokemon.MakeEmptyMon;
        }
    }

    public void RenderMap()
    {
        Grid grid = new GameObject("Grid").AddComponent<Grid>();
        grid.cellSize = new Vector3(0.5F, 0.5F, 1.0F);
        Tilemap level1 = new GameObject("Tilemap1").AddComponent<Tilemap>();
        Tilemap level2 = new GameObject("Tilemap2").AddComponent<Tilemap>();
        Tilemap level3 = new GameObject("Tilemap3").AddComponent<Tilemap>();
        TilemapRenderer renderer1 = level1.gameObject.AddComponent<TilemapRenderer>();
        TilemapRenderer renderer2 = level2.gameObject.AddComponent<TilemapRenderer>();
        TilemapRenderer renderer3 = level3.gameObject.AddComponent<TilemapRenderer>();
        renderer1.mode = TilemapRenderer.Mode.Chunk;
        renderer2.mode = TilemapRenderer.Mode.Chunk;
        renderer3.mode = TilemapRenderer.Mode.Chunk;
        renderer1.sortingLayerID = 0;
        renderer2.sortingLayerID = 0;
        renderer3.sortingLayerID = 0;
        renderer1.sortingOrder = -4;
        renderer2.sortingOrder = -3;
        renderer3.sortingOrder = 3;
        level1.transform.SetParent(grid.gameObject.transform);
        level2.transform.SetParent(grid.gameObject.transform);
        level3.transform.SetParent(grid.gameObject.transform);
        mapManager.level1 = level1;
        mapManager.level2 = level2;
        mapManager.level3 = level3;
        SwitchMap();
    }

    public void SwitchMap()
    {
        mapManager.mapData = currentMap;
        mapManager.ReadMap();
        currentMap.mapScripts.BeforeLoad(this);
        RefreshObjects();
        currentMap.mapScripts.OnLoad(this);
    }

    public void RefreshTriggers()
    {
        triggers = new();
        foreach (TileTrigger i in currentMap.triggers) triggers.Add(i);
    }

    public void RefreshSignposts()
    {
        signposts = new();
        foreach (TileTrigger i in currentMap.signposts) signposts.Add(i);
    }

    public void RefreshChars()
    {
        loadedChars ??= new();
        List<MapData> maps = new() { currentMap };
        foreach (Connection i in currentMap.connection)
        {
            Debug.Log("Adding " + i.map);
            maps.Add(i.map);
        }
        foreach ((MapData m, LoadedChar i) in loadedChars.Values)
        {
            bool keep = false;
            Debug.Log(currentMap);
            Debug.Log(i.currentMap);
            if (i.keepOnLoad)
            {
                if (i.currentMap != currentMap)
                    foreach (Connection j in currentMap.connection)
                    {
                        if (j.map == i.currentMap) i.pos += GetMapOffset(j);
                    };
                i.currentMap = currentMap;
                continue;
            }
            foreach (MapData j in maps)
            {
                Debug.Log("Testing " + j);
                Debug.Log("MapID is " + m);
                if (m == j)
                {
                    Debug.Log("Matched");
                    if (i.currentMap != currentMap)
                        foreach (Connection k in currentMap.connection)
                        {
                            if (k.map == i.currentMap)
                            {
                                Debug.Log(i.pos);
                                Debug.Log(GetMapOffset(k));
                                i.pos += GetMapOffset(k);
                                Debug.Log(i.pos);
                            }
                        };
                    i.currentMap = currentMap;
                    keep = true;
                    break;
                }
            }
            if (keep) continue;
            StartCoroutine(i.Unload());
        }
        foreach (MapData i in maps)
        {
            foreach (mapChar charData in i.chars)
            {
                if (!charData.data.IsLoaded(this))
                {
                    LoadedChar chara = charData.data.Load(this, i, charData.pos);
                    if (i != currentMap)
                    {
                        foreach (Connection k in currentMap.connection)
                        {
                            if (k.map == i)
                            {
                                chara.pos += GetMapOffset(k);
                                chara.AlignObject();
                            }
                        };
                    }
                }
            }
        }
    }

    public void RefreshObjects()
    {
        RefreshTriggers();
        RefreshSignposts();
        RefreshChars();
    }

    public LoadedChar GetChar(string id)
    {
        return loadedChars[id].chara;
    }

    public void SwitchAndReposition(Vector2Int pos)
    {
        mapManager.mapData = currentMap;
        mapManager.ReadAndReposition(this, pos);
        currentMap.mapScripts.BeforeLoad(this);
        RefreshObjects();
        currentMap.mapScripts.OnLoad(this);
    }

    public MapData GetMapByID(string id)
    {
        if (currentMap.id == id) { return currentMap; }
        foreach (MapData map in mapDirectory.maps)
        {
            if (map.id == id) return map;
        }
        return null;
    }

    public void AlignPlayer() => playerGraphics.playerTransform.position = new Vector3(pos.x + 0.5F, pos.y + 0.5F, pos.y);

    public new void UpdateCollision() => currentHeight = CheckCollision(pos, false);

    public void CreatePlayerGraphics(HumanoidGraphics graphics) => playerGraphics = new(this, graphics);

    public CollisionID CheckCollision(Vector2Int pos, bool checkChars)
    {
        if (checkChars)
        {
            if (pos == this.pos || (state == PlayerState.Moving && pos == moveTarget)) return CollisionID.Impassable;
            if (loadedChars.Count > 0) foreach ((MapData _, LoadedChar loadedChar) in loadedChars.Values)
                    if (loadedChar.pos == pos || (loadedChar.moving && loadedChar.moveTarget == pos)) return CollisionID.Impassable;
        }
        if (pos.x >= 0 && pos.y >= 0 && pos.x < currentMap.width && pos.y < currentMap.height)
            return (CollisionID)mapManager.collision[pos.x, pos.y];
        else
        {
            return CollisionOnBorderingMaps(pos);
        }
    }

    private CollisionID CollisionOnBorderingMaps(Vector2Int pos)
    {
        (MapData map, Vector2Int pos) relativeTile = TileOutsideMap(pos);
        if (relativeTile.map == null) return CollisionID.Impassable;
        else
        {
            return (CollisionID)mapManager.borderingCollision[relativeTile.map]
                [relativeTile.pos.x, relativeTile.pos.y];
        }
    }

    private (MapData, Vector2Int) TileOutsideMap(Vector2Int pos)
    {
        foreach (Connection i in currentMap.connection)
        {
            Vector2Int checkPos = pos - GetMapOffset(i);
            if (checkPos.x >= 0 && checkPos.x < i.map.width
                && checkPos.y >= 0 && checkPos.y < i.map.height)
                return (i.map, checkPos);
        }
        return (null, Vector2Int.zero);
    }

    public Vector2Int GetMapOffset(Connection connection)
    {
        return connection.direction switch
        {
            Direction.N => new Vector2Int(connection.offset, currentMap.height),
            Direction.S => new Vector2Int(connection.offset, 0 - connection.map.height),
            Direction.E => new Vector2Int(currentMap.width, connection.offset),
            Direction.W => new Vector2Int(0 - connection.map.width, connection.offset),
            _ => Vector2Int.zero
        };
    }

    public Vector2Int GetCoordinateWithOffset(Connection i, Vector2Int basePos)
        => GetMapOffset(i) + basePos;

    public bool CheckCollisionAllowed(Vector2Int pos, CollisionID currentCollision, bool checkChars = true)
    {
        CollisionID nextCollision = CheckCollision(pos, checkChars);
        if (nextCollision == CollisionID.Impassable) return false;
        if (currentCollision == CollisionID.Change) return true;
        if (nextCollision is CollisionID.Bridge or CollisionID.Change) return true;
        if (currentCollision == nextCollision) return true;
        else return false;
    }

    private void TryChangeMap(int x, int y)
    {
        if (x >= mapManager.mapData.width)
        {
            foreach (Connection i in mapManager.mapData.connection)
            {
                if (i.direction == Direction.E && y >= i.offset && y < i.map.height + i.offset)
                {
                    lastMap = currentMap;
                    currentMap = i.map;
                    SwitchAndReposition(new Vector2Int(-1, y - i.offset));
                    AlignPlayer();
                }
            }
        }
        else if (x < 0)
        {
            foreach (Connection i in mapManager.mapData.connection)
            {
                if (i.direction == Direction.W && y >= i.offset && y < i.map.height + i.offset)
                {
                    lastMap = currentMap;
                    currentMap = i.map;
                    SwitchAndReposition(new Vector2Int(i.map.width, y - i.offset));
                    AlignPlayer();
                }
            }
        }
        else if (y >= mapManager.mapData.height)
        {
            foreach (Connection i in mapManager.mapData.connection)
            {
                if (i.direction == Direction.N && x >= i.offset && x < i.map.width + i.offset)
                {
                    lastMap = currentMap;
                    currentMap = i.map;
                    SwitchAndReposition(new Vector2Int(x - i.offset, -1));
                    AlignPlayer();
                }
            }
        }
        else if (y < 0)
        {
            foreach (Connection i in mapManager.mapData.connection)
            {
                if (i.direction == Direction.S && x >= i.offset && x < i.map.width + i.offset)
                {
                    lastMap = currentMap;
                    currentMap = i.map;
                    SwitchAndReposition(new Vector2Int(x - i.offset, i.map.height));
                    AlignPlayer();
                }
            }
        }
    }

    private void FindAnnouncer()
    {
        announcer = FindAnyObjectByType<GUIManager>();
        announcer.player = this;
    }

    private void CaptureCamera()
    {
        camera.transform.parent = playerGraphics.playerObject.transform;
        camera.transform.localPosition = new Vector3(0, 0, -100);
    }

    public IEnumerator StartSingleTrainerBattle(LoadedChar opponentChar, TeamData opponentTeam)
    {
        Pokemon[] opponentParty = opponentTeam.GetParty();
        state = PlayerState.Locked;
        active = false;
        DeactivateAll();
        mapManager.ClearMap();
        opponent = opponentChar;
        yield return Scene.Battle.Load();
        Battle battle = FindAnyObjectByType<Battle>();
        battle.player = this;
        SortParty();
        battle.OpponentName = opponentTeam.trainerName;
        battle.PlayerPokemon = Party;
        battle.OpponentPokemon = opponentParty;
        battle.battleType = BattleType.Single;
        battle.wildBattle = false;
        battle.battleTerrain = currentTerrain;
        battle.StartCoroutine(battle.StartBattle());
    }

    public IEnumerator StartSingleWildBattle(Pokemon wildMon)
    {
        Debug.Log("Start Single Wild Battle");
        state = PlayerState.Locked;
        active = false;
        DeactivateAll();
        yield return FadeToBlack(0.2F);
        yield return Scene.Battle.Load();
        Battle battle = FindAnyObjectByType<Battle>();
        battle.player = this;
        SortParty();
        battle.PlayerPokemon = Party;
        battle.OpponentPokemon = new Pokemon[6];
        battle.OpponentPokemon[0] = wildMon;
        for (int i = 1; i < 6; i++)
        {
            battle.OpponentPokemon[i] = Pokemon.MakeEmptyMon;
        }
        battle.battleType = BattleType.Single;
        battle.battleTerrain = currentTerrain;
        battle.wildBattle = true;
        yield return FadeFromBlack(0.2F);
        battle.StartCoroutine(battle.StartBattle());
    }

    public IEnumerator BattleWon()
    {
        yield return FadeToBlack(0.2F);
        ResetTransformations();
        yield return Scene.Map.Load();
        Debug.Log("Map loaded");
        camera = Instantiate(Resources.Load<GameObject>("Prefabs/Map CameraGUI"));
        FindAnnouncer();
        mapManager = gameObject.AddComponent<MapManager>();
        RenderMap();
        CreatePlayerGraphics(CharGraphics.brendanWalk);
        AlignPlayer();
        CaptureCamera();
        UpdateCollision();
        ActivateAll();
        yield return FadeFromBlack(0.2F);
        active = true;
    }

    public IEnumerator WildBattleWon()
    {
        yield return BattleWon();
        state = PlayerState.Free;
    }

    public IEnumerator TrainerBattleWon()
    {
        yield return BattleWon();
        yield return opponent.charData.OnWin(this, opponent);
    }

    public IEnumerator FadeToBlack(float duration)
    {
        if (blackScreen != null) Destroy(blackScreen);
        blackScreen = new();
        blackScreen.transform.parent = transform;
        blackScreen.transform.localScale = new(1000, 1000, 1000);
        blackScreen.transform.position = new(0, 0, -20);
        SpriteRenderer renderer = blackScreen.AddComponent<SpriteRenderer>();
        renderer.sprite = Sprite.Create(Resources.Load<Texture2D>("Sprites/Box"), new Rect(0, 0, 4, 4), new Vector2(0.5F, 0.5F), 4);
        renderer.color = new(0, 0, 0, 0);
        renderer.sortingOrder = 10;
        float baseTime = Time.time;
        float endTime = baseTime + duration;
        while(Time.time < endTime)
        {
            renderer.color = new(0, 0, 0, (Time.time - baseTime) / duration);
            yield return null;
        }
    }

    public IEnumerator FadeFromBlack(float duration)
    {
        SpriteRenderer renderer = blackScreen.GetComponent<SpriteRenderer>();
        float baseTime = Time.time;
        float endTime = baseTime + duration;
        while (Time.time < endTime)
        {
            renderer.color = new(0, 0, 0, (endTime - Time.time) / duration);
            yield return null;
        }
        Destroy(blackScreen);
    }



    public void CheckGrassEncounter()
    {
        byte index = mapManager.wildData[pos.x, pos.y];
        if (index == 0) return;
        WildDataset dataset = currentMap.grassData[index - 1];
        if (random.NextDouble() * 100 < dataset.encounterPercent)
        {
            StartCoroutine(StartSingleWildBattle(dataset.GetWildMon()));
        }
    }

    public void TryGoSouth()
    {
        if (facing != Direction.S)
            StartCoroutine(playerGraphics.FaceSouth(this, 0.1F));
        else if (CheckCollisionAllowed(GetFacingTile(), currentHeight))
        {
            TryChangeMap(pos.x, pos.y - 1);
            CheckTileBehavior(GetFacingTile());
            StartCoroutine(GoSouth());
        }
        else
        {
            StartCoroutine(playerGraphics.BumpSouth(this, 0.3F));
        }
    }

    public void TryGoNorth()
    {
        if (facing != Direction.N)
            StartCoroutine(playerGraphics.FaceNorth(this, 0.1F));
        else if (CheckCollisionAllowed(GetFacingTile(), currentHeight))
        {
            TryChangeMap(pos.x, pos.y + 1);
            CheckTileBehavior(GetFacingTile());
            StartCoroutine(GoNorth());
        }
        else
        {
            StartCoroutine(playerGraphics.BumpNorth(this, 0.3F));
        }
    }

    public void TryGoWest()
    {
        if (facing != Direction.W)
            StartCoroutine(playerGraphics.FaceWest(this, 0.1F));
        else if (CheckCollisionAllowed(GetFacingTile(), currentHeight))
        {
            TryChangeMap(pos.x - 1, pos.y);
            CheckTileBehavior(GetFacingTile());
            StartCoroutine(GoWest());
        }
        else
        {
            StartCoroutine(playerGraphics.BumpWest(this, 0.3F));
        }
    }

    public void TryGoEast()
    {
        if (facing != Direction.E)
            StartCoroutine(playerGraphics.FaceEast(this, 0.1F));
        else if (CheckCollisionAllowed(GetFacingTile(), currentHeight))
        {
            TryChangeMap(pos.x + 1, pos.y);
            CheckTileBehavior(GetFacingTile());
            StartCoroutine(GoEast());
        }
        else
        {
            StartCoroutine(playerGraphics.BumpEast(this, 0.3F));
        }
    }
    public IEnumerator GoSouth()
    {
        yield return playerGraphics.WalkSouth(this, 0.3F);
        UpdateCollision();
        if (!CheckForTriggers())
            CheckGrassEncounter();
    }

    public IEnumerator GoNorth()
    {
        yield return playerGraphics.WalkNorth(this, 0.3F);
        UpdateCollision();
        if (!CheckForTriggers())
            CheckGrassEncounter();
    }

    public IEnumerator GoWest()
    {
        yield return playerGraphics.WalkWest(this, 0.3F);
        UpdateCollision();
        if (!CheckForTriggers())
            CheckGrassEncounter();
    }

    public IEnumerator GoEast()
    {
        yield return playerGraphics.WalkEast(this, 0.3F);
        UpdateCollision();
        if (!CheckForTriggers())
            CheckGrassEncounter();
    }

    public bool CheckForTriggers()
    {
        foreach (TileTrigger i in triggers)
        {
            if (i.pos == new Vector2Int(pos.x, pos.y))
            {
                StartCoroutine(i.script.OnTrigger(this));
                return true;
            }
        }
        return false;
    }

    public bool CheckForCharacters()
    {
        Vector2Int facingTile = GetFacingTile();
        foreach ((MapData _, LoadedChar i) in loadedChars.Values)
        {
            if (i.pos == facingTile && i.available)
            {
                StartCoroutine(i.charData.OnInteract(this, i));
                return true;
            }
        }
        return false;
    }

    public bool CheckForSignposts()
    {
        Vector2Int facingTile = GetFacingTile();
        foreach (TileTrigger i in signposts)
        {
            if (i.pos == facingTile)
            {
                StartCoroutine(i.script.OnTrigger(this));
                return true;
            }
        }
        return false;
    }

    public void CheckForInteractables()
    {
        if (!CheckForCharacters())
            CheckForSignposts();
    }

    public IEnumerator DoAnnouncements(List<string> text)
    {
        state = PlayerState.Announce;
        yield return announcer.AnnouncementUp();
        foreach (string i in text)
        {
            yield return announcer.Announce(i);
        }
        yield return announcer.AnnouncementDown();
        state = locked ? PlayerState.Locked : PlayerState.Free;
    }

    public void ResetTransformations()
    {
        foreach (Pokemon mon in Party)
            mon.transformed = false;
    }

    public IEnumerator InitMapTest()
    {
        yield return Scene.Map.Load();
        camera = Instantiate(Resources.Load<GameObject>("Prefabs/Map CameraGUI"));
        FindAnnouncer();
        mapManager = gameObject.AddComponent<MapManager>();
        RenderMap();
        CreatePlayerGraphics(CharGraphics.brendanWalk);
        AlignPlayer();
        CaptureCamera();
        UpdateCollision();
        Party[0] = Pokemon.WildPokemon(SpeciesID.Venusaur, 4);
        Party[0].item = ItemID.Venusaurite;
        active = true;
    }
    // Start is called before the first frame update
    public void Start()
    {
        p = this;
        random = new();
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex((int)Scene.Main))
        {
            StartCoroutine(InitMapTest());
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    public new void Update()
    {
        if (active)
        {
            switch (state)
            {
                case PlayerState.Free:
                    AlignPlayer();
                    if (Input.GetKey(KeyCode.UpArrow)) TryGoNorth();
                    else if (Input.GetKey(KeyCode.DownArrow)) TryGoSouth();
                    else if (Input.GetKey(KeyCode.LeftArrow)) TryGoWest();
                    else if (Input.GetKey(KeyCode.RightArrow)) TryGoEast();
                    else if (Input.GetKeyDown(KeyCode.Return)) CheckForInteractables();
                    break;
                case PlayerState.Moving:
                    break;
                case PlayerState.Locked:
                    break;
                case PlayerState.Announce:
                    break;
                default:
                    break;
            }
        }
    }
}
