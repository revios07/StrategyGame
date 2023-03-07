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

    public virtual Transform UseFromPool()
    {
        //Reset Slider
        SetSliderValue(1000);
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
