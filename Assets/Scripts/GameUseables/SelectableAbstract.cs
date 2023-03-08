using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using UnityEngine.UI;

[RequireComponent(typeof(ItemPointerHandler))]
public abstract class SelectableAbstract : MonoBehaviour, ISelectableObject, ICanTakeDamagePlayableObject
{
    public Enums.ObjectType objectType;
    protected Slider healthSlider;
    protected HealthTextUpdater healthTextUpdater;
    public bool isPlaced;

    public BoundsInt sizeArea;

    protected virtual void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
        healthTextUpdater = healthSlider.transform.GetComponent<HealthTextUpdater>();
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

    public void Placed()
    {
        isPlaced = true;
    }

    public bool CanBePlaced()
    {
        Vector3Int posiitonInt = GridPlacementSystem.instance.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = sizeArea;
        areaTemp.position = posiitonInt;

        if (GridPlacementSystem.instance.CanTakeArea(areaTemp))
        {
            return true;
        }

        return false;
    }

    public virtual Transform UseFromPool()
    {
        //Reset Slider
        SetSliderValue(1000);
        isPlaced = false;
        return this.transform;
    }

    public virtual void TakeDamage(int damage)
    {
        
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
