using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(ItemPointerHandler))]
public abstract class SelectableAbstract : MonoBehaviour, ISelectableObject, ICanTakeDamagePlayableObject
{
    public bool isPlaced;
    public bool isDead;
    public Enums.ObjectType objectType;
    public BoundsInt sizeArea;

    protected Slider healthSlider;
    protected HealthTextUpdater healthTextUpdater;

    protected virtual void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
        healthTextUpdater = healthSlider.transform.GetComponent<HealthTextUpdater>();
        isDead = false;
    }

    #region Selections
    public virtual void OnSelectedItemFromGame()
    {

    }
    public virtual void OnItemSelectedFromMenu()
    {

    }
    #endregion

    #region Pool Calls
    public virtual Transform UseFromPool()
    {
        GetComponent<Collider2D>().enabled = true;
        //Reset Slider
        SetSliderValue(1000);
        isDead = false;
        isPlaced = false;
        return this.transform;
    }
    public virtual void AddToPool()
    {

    }
    #endregion

    #region Placement on Game
    public virtual void PlaceToArea()
    {

    }
    public bool CanBePlaced()
    {
        Vector3Int posiitonInt = GridPlacementSystem.instance.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = sizeArea;

        if (objectType == Enums.ObjectType.Barracks)
            posiitonInt.y -= 1;

        areaTemp.position = posiitonInt;

        if (GridPlacementSystem.instance.CanTakeArea(areaTemp))
        {
            return true;
        }

        return false;
    }
    #endregion

    #region Damage/Health
    public virtual void TakeDamage(int damage)
    {
        if (isDead)
        {
            GetComponent<Collider2D>().enabled = false;
            //Clear Area For Another Placement
            //Can Spawn Explosion Effect Here
            GamePlayController.isAttackContinue = false;
            SelectedObjectAssigner.instance.ControllIsAssigned(transform);

            if(objectType != Enums.ObjectType.Barracks)
            {
                GridPlacementSystem.SetTilesBlock(sizeArea, Enums.TileType.White, GridPlacementSystem.instance.playableAreaTilemap);
            }
            else if(objectType == Enums.ObjectType.Barracks)
            {
                //Soldier Spawner Area Check!
                BoundsInt soldierSpawnerArea = sizeArea;

                Vector3Int soldierSpawnerAreaPosition = sizeArea.position;
                Vector3Int soldierSpawnerSize = Vector3Int.one;
               
                soldierSpawnerArea.size = soldierSpawnerSize;
                soldierSpawnerArea.position = soldierSpawnerAreaPosition;


                for(int i = 0; i < sizeArea.size.x; ++i)
                {
                    //Controll area there any soldier at spawn area!
                    TileBase[] tiles = GridPlacementSystem.GetTileBases(soldierSpawnerArea, GridPlacementSystem.instance.playableAreaTilemap);
                    for(int j = 0; j < tiles.Length; ++j)
                    {
                        if(tiles[j] == GridPlacementSystem.tileBases[Enums.TileType.SoldierSpawn])
                        {
                            //Empty Spawn area
                            GridPlacementSystem.SetTilesBlock(soldierSpawnerArea, Enums.TileType.White, GridPlacementSystem.instance.playableAreaTilemap);
                        }
                        else
                        {
                            //Stay with green
                        }
                    }

                    Vector3Int pos = soldierSpawnerArea.position;
                    pos.x += 1;
                    soldierSpawnerArea.position = pos;
                }

                //Clear Barracks Self area Without SoldierSpawner
                Vector3Int posAreaWithoutSpawner = sizeArea.position;
                posAreaWithoutSpawner.y += 1;
                BoundsInt sizeAreaWithoutSpawner = sizeArea;
                sizeAreaWithoutSpawner.position = posAreaWithoutSpawner;

                GridPlacementSystem.SetTilesBlock(sizeAreaWithoutSpawner, Enums.TileType.White, GridPlacementSystem.instance.playableAreaTilemap);
            }


            if (objectType == Enums.ObjectType.Soldier)
            {
                GridPlacementSystem.instance.ControllAndSetSoldiersTilesBlocks(transform, sizeArea, GridPlacementSystem.instance.playableAreaTilemap);
            }

            EventManager.onObjectAddToPool(objectType, transform);
        }
    }
    protected void SetMaxValueOfSlide(int maxSliderValue)
    {
        healthSlider.maxValue = maxSliderValue;
    }
    protected void SetSliderValue(int value)
    {
        healthSlider.value = value;
        healthTextUpdater.WriteHealth(healthSlider.value);
    }
    #endregion
}
