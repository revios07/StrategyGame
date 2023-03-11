using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Enums;

public class GridPlacementSystem : MonoBehaviour
{
    public static GridPlacementSystem instance;

    public GridLayout gridLayout;
    [NaughtyAttributes.BoxGroup("TileMaps")]
    public Tilemap playableAreaTilemap, backGroundTilemap, agentWalkableTilemap, agentBlockerTilemap;
    protected BoundsInt previousArea;
    protected Vector3 previousPos;

    [NaughtyAttributes.BoxGroup("Tile Bases")]
    [SerializeField]
    private TileBase _whiteTile, _greenTile, _redTile, _soldierSpawn;
    public static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("Can't be 2 Placement System!");
        }
    }
    private void Start()
    {
        tileBases.Clear();

        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, _whiteTile);
        tileBases.Add(TileType.Green, _greenTile);
        tileBases.Add(TileType.Red, _redTile);
        tileBases.Add(TileType.SoldierSpawn, _soldierSpawn);
    }

    public static TileBase[] GetTileBases(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] tileBases = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var pos in area.allPositionsWithin)
        {
            Vector3Int posVec = new Vector3Int(pos.x, pos.y, 0);
            tileBases[counter] = tilemap.GetTile(posVec);
            ++counter;
        }

        return tileBases;
    }
    public static void FillTiles(TileBase[] arr, TileType type)
    {
        for (int i = 0; i < arr.Length; ++i)
        {
            arr[i] = tileBases[type];
        }
    }
    public static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }
    protected void ClearArea()
    {
        TileBase[] toClear = new TileBase[previousArea.size.x * previousArea.size.y * previousArea.size.z];
        FillTiles(toClear, TileType.Empty);
        backGroundTilemap.SetTilesBlock(previousArea, toClear);
    }

    #region Calculate Placement and Show With Tiles
    protected void FollowBuildings(SelectableAbstract selectableAbstract)
    {
        if (selectableAbstract == null)
            return;

        ClearArea();

        Vector3 selectedPos = selectableAbstract.transform.localPosition;
        selectedPos.z = 0;
        selectedPos.y -= 1;

        Vector3Int pos = gridLayout.LocalToCell(selectedPos);

        //Soldier Spawn Area Calculate For Barracks
        if (selectableAbstract.objectType == ObjectType.Barracks)
            pos.y -= 1;

        selectableAbstract.sizeArea.position = pos;
        BoundsInt buildingArea = selectableAbstract.sizeArea;

        TileBase[] baseArray = GetTileBases(buildingArea, playableAreaTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for (int i = 0; i < baseArray.Length; ++i)
        {
            if (baseArray[i] == tileBases[TileType.White])
            {
                //Can Placed
                tileArray[i] = tileBases[TileType.Green];
            }
            else
            {
                //Can't Place
                FillTiles(tileArray, TileType.Red);
                break;
            }
        }

        backGroundTilemap.SetTilesBlock(buildingArea, tileArray);
        previousArea = buildingArea;
    }
    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTileBases(area, playableAreaTilemap);
        foreach (var tileBase in baseArray)
        {
            if (tileBase != tileBases[TileType.White])
            {
                //Cannot Place
                Debug.Log("Cannot Place Here!");
                return false;
            }
        }

        return true;
    }
    public void TakeArea(BoundsInt area, ObjectType objectType)
    {
        SetTilesBlock(area, TileType.Empty, backGroundTilemap);
        SetTilesBlock(area, TileType.Green, playableAreaTilemap);

        if (objectType == ObjectType.Barracks)
        {
            BoundsInt areaCalculate = area;
            areaCalculate.size = new Vector3Int(4, 1, 1);

            SetTilesBlock(areaCalculate, TileType.SoldierSpawn, playableAreaTilemap);
        }
    }
    #endregion

    #region Soldier Spawner Barracks
    public bool CanSpawnSoldier(Building building)
    {
        if (!GamePlayController.lastSelectedBuilding.isPlaced && !GamePlayController.lastSelectedBuilding.isActiveAndEnabled)
        {
            //Building Destroyed or Didn't Placed
            //Cant Spawn Soldiers
            return false;
        }

        TileBase[] tiles = GetTileBases(building.sizeArea, playableAreaTilemap);

        for (int i = 0; i < tiles.Length; ++i)
        {
            if (tiles[i] == tileBases[TileType.SoldierSpawn])
            {
                //Can Spawn Soldier
                return true;
            }
        }

        //Can't Spawn Soldier Here
        return false;
    }
    public void SpawnSoldier()
    {
        bool isSpawned = false;

        for (int i = 0; i < GamePlayController.lastSelectedBuilding.sizeArea.size.x; ++i)
        {
            BoundsInt controllArea = GamePlayController.lastSelectedBuilding.sizeArea;
            controllArea.size = Vector3Int.one;

            controllArea.position = new Vector3Int(i + GamePlayController.lastSelectedBuilding.sizeArea.position.x, controllArea.position.y, 0);

            TileBase[] tiles = GetTileBases(controllArea, playableAreaTilemap);

            foreach (var tile in tiles)
            {
                if (tile == tileBases[TileType.SoldierSpawn])
                {
                    //Spawn Here
                    EventManager.onSoldierSpawnedRequest?.Invoke();
                    Transform soldierTransform = EventManager.pickRequestFromPool?.Invoke(ObjectType.Soldier);

                    soldierTransform.GetComponent<Soldier>().sizeArea = controllArea;

                    SetTilesBlock(controllArea, TileType.Green, playableAreaTilemap);

                    Vector3 spawnPos = gridLayout.CellToWorld(controllArea.position);
                    Vector3Int cellPos = gridLayout.LocalToCell(spawnPos);

                    soldierTransform.transform.parent = gridLayout.transform;
                    soldierTransform.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos + new Vector3(0.5f, 0.5f, 0.0f));

                    isSpawned = true;
                    break;
                }
            }

            if (isSpawned)
                break;
        }
    }
    #endregion
    #region Soldier Spawner Area Calculate for Barracks
    public void ControllAndSetSoldiersTilesBlocks(Transform soldierTransform, BoundsInt area, Tilemap tilemap)
    {
        //When Soldier Dead Or Move Controll Spawn Area
        Vector3 raycastPos = soldierTransform.position + Vector3.up * 32f + Vector3.back;

        int counter = 0;

        for (int i = 1; i <= 4; ++i)
        {
            BoundsInt bounds = soldierTransform.GetComponent<Soldier>().sizeArea;
            Collider2D hit = Physics2D.OverlapCircle(soldierTransform.position + Vector3.up * 32f * i, 1f);

            if (hit != null)
            {
                Building building;
                bool isBuilding = hit.gameObject.TryGetComponent<Building>(out building);

                if (isBuilding && building.objectType == ObjectType.Barracks)
                {
                    //There is a barrack of upper 
                    //Rebuild Spawn Area Again
                    Debug.Log("There is a Barracks Upper of Soldier!");

                    bounds.position = gridLayout.WorldToCell(soldierTransform.position);
                    TileBase[] controllTiles = GetTileBases(bounds, playableAreaTilemap);

                    for (int j = 0; j < controllTiles.Length; j++)
                    {
                        if (controllTiles[j] == tileBases[TileType.Green])
                        {
                            break;
                        }
                        else
                        {
                            ++counter;
                        }
                    }
                }
                else
                {

                }
            }
        }

        if (counter >= 4)
        {
            int size = area.size.x * area.size.y * area.size.z;
            TileBase[] tileArray = new TileBase[size];
            FillTiles(tileArray, TileType.SoldierSpawn);
            tilemap.SetTilesBlock(area, tileArray);
        }
    }
    #endregion

}
