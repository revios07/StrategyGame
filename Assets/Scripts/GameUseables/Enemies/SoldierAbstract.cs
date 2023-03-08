using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public abstract class SoldierAbstract : SelectableAbstract, IPoolableObject, ICanAttackObject, ICanTakeDamagePlayableObject
{
    public SoldierScriptable[] soldierScriptable;
    protected Structs.SoldierStruct soldierStructData;
    public int soldierHealth;

    public void CanMoveable(Vector3Int movePosition)
    {

    }
    public bool MoveToDirection()
    {
        //CanMoveable()
        if (true)
        {
            return true;
        }

        //return false;
    }
    protected Structs.SoldierStruct GetRandomSoldier()
    {
        return soldierScriptable[Random.Range(0, soldierScriptable.Length)].GetSoldierData();
    }

    [NaughtyAttributes.Button("Placed")]
    public override void PlaceToArea()
    {
        base.PlaceToArea();

        if (isPlaced)
        {
            isPlaced = false;
            soldierStructData.isPlaced = isPlaced;
            return;
        }

        isPlaced = true;
        soldierStructData.isPlaced = isPlaced;
    }

    //Interface Implementations
    #region Attack and Take Damage
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        soldierStructData.soldierHealth -= damage;
        if (damage <= 0)
        {
            soldierStructData.soldierHealth = 0;
            //Soldier Destroyed Here
            //Add Pool Again GameObject
        }

        SetSliderValue(soldierStructData.soldierHealth);
    }
    public void GiveDamage(ICanTakeDamagePlayableObject canTakeDamagePlayableObject)
    {
        //Give Damage To Object
        canTakeDamagePlayableObject.TakeDamage(soldierStructData.soldierDamage);

        throw new System.NotImplementedException();
    }
    #endregion

    #region Pool Calls
    public override void AddToPool()
    {
        //Reset Soldier Health Here
        //Give Random Health To Soldier
        soldierStructData = GetRandomSoldier();

        base.AddToPool();
    }
    public override Transform UseFromPool()
    {
        base.UseFromPool();

        return this.transform;
    }
    #endregion
}
