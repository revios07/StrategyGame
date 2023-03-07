using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using UnityEngine.UI;

public abstract class SelectableAbstract : MonoBehaviour, ISelectableObject, ICanTakeDamagePlayableObject
{
    public Enums.ObjectType objectType;
    protected Slider healthSlider;

    protected virtual void Start()
    {
        healthSlider = GetComponentInChildren<Slider>();
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
    }
}
