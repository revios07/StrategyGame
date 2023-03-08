using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Enums;

public class GridPlacementSystem : MonoBehaviour
{
    public static GridPlacementSystem instance;

    [SerializeField]
    private GridLayout _gridLayout;
    [SerializeField]
    private Tilemap _playableAreaTilemap, _backGroundTilemap;

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
        }
    }

    private void Start()
    {
        string tilePath = @"Tiles\";
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, Resources.Load<TileBase>(tilePath + "white"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(tilePath + "green"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(tilePath + "red"));
    }

    public static TileBase[] GetTileBases(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] tileBases = new TileBase[area.size.x * area.size.y + area.size.z];
        int counter = 0;

        foreach(var pos in area.allPositionsWithin)
        {
            Vector3Int posVec = new Vector3Int(pos.x, pos.z, 0);
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
}
