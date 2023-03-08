using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Enums;

public class GridPlacementSystem : MonoBehaviour
{
    public static GridPlacementSystem instance;

    public GridLayout gridLayout;
    [SerializeField]
    protected Tilemap playableAreaTilemap, backGroundTilemap;
    protected BoundsInt previousArea;
    protected Vector3 previousPos;

    [NaughtyAttributes.BoxGroup("Tile Bases")]
    [SerializeField]
    private TileBase _whiteTile, _greenTile, _redTile;

    public static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    private void Awake()
    {
        if(instance == null)
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
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, _whiteTile);
        tileBases.Add(TileType.Green, _greenTile);
        tileBases.Add(TileType.Red, _redTile);
    }

    private void OnDestroy()
    {
        tileBases.Clear();
    }

    public static TileBase[] GetTileBases(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] tileBases = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach(var pos in area.allPositionsWithin)
        {
            Vector3Int posVec = new Vector3Int(pos.x, pos.y, 0);
            tileBases[counter] = tilemap.GetTile(posVec);
            ++counter;
        }

        return tileBases;
    }

    public static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    public static void FillTiles(TileBase[] arr, TileType type)
    {
        for(int i = 0; i < arr.Length; ++i)
        {
            arr[i] = tileBases[type];
        }
    }

    protected void ClearArea()
    {
        TileBase[] toClear = new TileBase[previousArea.size.x * previousArea.size.y * previousArea.size.z];
        FillTiles(toClear, TileType.Empty);
        backGroundTilemap.SetTilesBlock(previousArea, toClear);
    }

    protected void FollowBuildings(SelectableAbstract selectableAbstract)
    {
        ClearArea();

        Vector3 selectedPos = selectableAbstract.transform.localPosition;
        selectedPos.z = 0;

        selectableAbstract.sizeArea.position = gridLayout.LocalToCell(selectedPos);
        BoundsInt buildingArea = selectableAbstract.sizeArea;

        TileBase[] baseArray = GetTileBases(buildingArea, playableAreaTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for(int i = 0; i < baseArray.Length; ++i)
        {
            if(baseArray[i] == tileBases[TileType.White])
            {
                //Can Placed
                tileArray[i] = tileBases[TileType.Green];
            }
            else
            {
                //Cant Place
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
        foreach(var tileBase in baseArray)
        {
            if(tileBase != tileBases[TileType.White])
            {
                //Cannot Place
                Debug.Log("Cannot Place Here!");
                return false;
            }
        }

        return true;
    }

    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.Empty, backGroundTilemap);
        SetTilesBlock(area, TileType.Green, playableAreaTilemap);
    }
}
