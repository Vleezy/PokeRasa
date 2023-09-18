using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class Player : MonoBehaviour
{


    public bool[] TM = new bool[96];
    public bool[] HM = new bool[8];
    public bool[] keyItem = new bool[8];

    public Dictionary<ItemID, int> Bag;

    public bool[] storyFlags = new bool[100];

    public BattleTerrain currentTerrain;
    public MapID currentMap;
    public int xPos;
    public int yPos;
    public PlayerState state;
    public bool active;
    public bool locked;

    public bool whichStep;

    public CollisionID currentHeight;
    public MapManager mapManager;
    public PlayerGraphics playerGraphics;

    public GameObject camera;

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
        mapManager.mapID = currentMap;
        mapManager.ReadMap();
    }

    public void SwitchAndReposition(int xPosition, int yPosition)
    {
        mapManager.mapID = currentMap;
        mapManager.ReadAndReposition(this, xPosition, yPosition);
    }

    public void AlignPlayer() => playerGraphics.playerTransform.position = new Vector3(xPos + 0.5F, yPos + 0.5F);

    public void UpdateCollision() => currentHeight = CheckCollision(xPos, yPos);

    public void CreatePlayerGraphics(HumanoidGraphicsID id) => playerGraphics = new(this, id);

    private CollisionID CheckCollision(int x, int y)
    {
        return (CollisionID)mapManager.collision[x + 1, y + 1];
    }

    private bool CheckCollisionAllowed(Direction direction)
    {
        CollisionID nextCollision = direction switch
        {
            Direction.N => CheckCollision(xPos, yPos + 1),
            Direction.W => CheckCollision(xPos - 1, yPos),
            Direction.E => CheckCollision(xPos + 1, yPos),
            Direction.S => CheckCollision(xPos, yPos - 1),
            _ => CollisionID.Impassable
        };
        if (nextCollision == CollisionID.Impassable) return false;
        if (currentHeight == CollisionID.Change) return true;
        if (nextCollision is CollisionID.Bridge or CollisionID.Change) return true;
        if (currentHeight == nextCollision) return true;
        else return false;
    }

    private void TryChangeMap(int x, int y)
    {
        if(x >= mapManager.mapData.width)
        {
            foreach (Connection i in mapManager.mapData.connection)
            {
                if (i.direction == Direction.E && y >= i.offset && y < i.map.Data().height + i.offset)
                {
                    currentMap = i.map;
                    SwitchAndReposition(-1, y - i.offset);
                    AlignPlayer();
                }
            }
        }
        else if (x < 0)
        {
            foreach (Connection i in mapManager.mapData.connection)
            {
                if (i.direction == Direction.W && y >= i.offset && y < i.map.Data().height + i.offset)
                {
                    currentMap = i.map;
                    SwitchAndReposition(i.map.Data().width, y - i.offset);
                    AlignPlayer();
                }
            }
        }
        else if (y >= mapManager.mapData.height)
        {
            foreach (Connection i in mapManager.mapData.connection)
            {
                if (i.direction == Direction.N && x >= i.offset && x < i.map.Data().width + i.offset)
                {
                    currentMap = i.map;
                    SwitchAndReposition(x - i.offset, -1);
                    AlignPlayer();
                }
            }
        }
        else if (y < 0)
        {
            foreach (Connection i in mapManager.mapData.connection)
            {
                if (i.direction == Direction.S && x >= i.offset && x < i.map.Data().width + i.offset)
                {
                    currentMap = i.map;
                    SwitchAndReposition(x - i.offset, i.map.Data().height);
                    AlignPlayer();
                }
            }
        }
    }

    private void FindCamera() => camera = GameObject.Find("Main Camera");

    private void CaptureCamera() => camera.transform.parent = playerGraphics.playerObject.transform;

    public void StartBattle(Pokemon[] opponentParty, BattleType battleType)
    {
        Pokemon[] useOpponentParty = new Pokemon[6];
        for (int i = 0; i < opponentParty.Length; i++)
        {
            useOpponentParty[i] = opponentParty[i];
        }
        for (int i = opponentParty.Length; i < 6; i++)
        {
            useOpponentParty[i] = Pokemon.MakeEmptyMon;
        }
        Battle battle = GameObject.Find("BattleController").GetComponent<Battle>();
        battle.player = this;
        battle.PlayerPokemon = Party;
        battle.OpponentPokemon = useOpponentParty;
        battle.battleType = battleType;
        battle.battleTerrain = currentTerrain;
        battle.StartCoroutine(battle.StartBattle());
    }

    public void TryGoSouth()
    {
        if (CheckCollisionAllowed(Direction.S)){
            TryChangeMap(xPos, yPos - 1);
            GoSouth();
        }
        else
        {
            StartCoroutine(playerGraphics.BumpSouth(this, 0.3F));
        }
    }

    public void TryGoNorth()
    {
        if (CheckCollisionAllowed(Direction.N))
        {
            TryChangeMap(xPos, yPos + 1);
            GoNorth();
        }
        else
        {
            StartCoroutine(playerGraphics.BumpNorth(this, 0.3F));
        }
    }

    public void TryGoWest()
    {
        if (CheckCollisionAllowed(Direction.W))
        {
            TryChangeMap(xPos - 1, yPos);
            GoWest();
        }
        else
        {
            StartCoroutine(playerGraphics.BumpWest(this, 0.3F));
        }
    }

    public void TryGoEast()
    {
        if (CheckCollisionAllowed(Direction.E))
        {
            TryChangeMap(xPos + 1, yPos);
            GoEast();
        }
        else
        {
            StartCoroutine(playerGraphics.BumpEast(this, 0.3F));
        }
    }
    public void GoSouth()
    {
        StartCoroutine(playerGraphics.WalkSouth(this, 0.3F));
        UpdateCollision();
    }

    public void GoNorth()
    {
        StartCoroutine(playerGraphics.WalkNorth(this, 0.3F));
        UpdateCollision();
    }

    public void GoWest()
    {
        StartCoroutine(playerGraphics.WalkWest(this, 0.3F));
        UpdateCollision();
    }

    public void GoEast()
    {
        StartCoroutine(playerGraphics.WalkEast(this, 0.3F));
        UpdateCollision();
    }
    // Start is called before the first frame update
    public void Start()
    {
        mapManager = gameObject.AddComponent<MapManager>();
        currentMap = MapID.Test;
        RenderMap();
        CreatePlayerGraphics(HumanoidGraphicsID.brendanWalk);
        FindCamera();
        CaptureCamera();
        UpdateCollision();
        active = true;
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    public void Update()
    {
        if(active)
        {
            switch (state)
            {
                case PlayerState.Free:
                    AlignPlayer();
                    if (Input.GetKey(KeyCode.UpArrow)) TryGoNorth();
                    else if (Input.GetKey(KeyCode.DownArrow)) TryGoSouth();
                    else if (Input.GetKey(KeyCode.LeftArrow)) TryGoWest();
                    else if (Input.GetKey(KeyCode.RightArrow)) TryGoEast();
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
