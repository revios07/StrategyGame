using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using UnityEngine.UI;

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

    public virtual void OnSelectedItemFromGame()
    {

    }

    public virtual void OnItemSelectedFromMenu()
    {

    }

    public virtual void AddToPool()
    {

    }

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

    public virtual Transform UseFromPool()
    {
        GetComponent<Collider2D>().enabled = true;
        //Reset Slider
        SetSliderValue(1000);
        isDead = false;
        isPlaced = false;
        return this.transform;
    }

    public virtual void TakeDamage(int damage)
    {
        if (isDead)
        {
            GetComponent<Collider2D>().enabled = false;
            //Clear Area For Another Placement
            //Can Spawn Explosion Effect Here
            GamePlayController.isAttackContinue = false;


            GridPlacementSystem.SetTilesBlock(sizeArea, Enums.TileType.White, GridPlacementSystem.instance.playableAreaTilemap);

            if (objectType == Enums.ObjectType.Soldier)
                GridPlacementSystem.instance.ControllAndSetSoldiersTilesBlocks(transform, sizeArea, GridPlacementSystem.instance.playableAreaTilemap);

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
}
