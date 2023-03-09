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

    #region Attack and Take Damage
    public override void TakeDamage(int damage)
    {
        soldierStructData.soldierHealth -= damage;
        if (soldierStructData.soldierHealth <= 0)
        {
            soldierStructData.soldierHealth = 0;
            base.isDead = true;
            //Soldier Destroyed Here
            //Add Pool Again GameObject
        }

        base.TakeDamage(damage);
        healthTextUpdater.WriteHealth(soldierStructData.soldierHealth);
        SetSliderValue(soldierStructData.soldierHealth);
    }
    public void GiveDamage(Transform targetTransform, ICanTakeDamagePlayableObject canTakeDamagePlayableObject)
    {
        //Give Damage To Object
        canTakeDamagePlayableObject.TakeDamage(soldierStructData.soldierDamage);

        Transform bullet = EventManager.pickRequestFromPool(Enums.ObjectType.Bullet);

        StartCoroutine(StartAttack(targetTransform, canTakeDamagePlayableObject));
    }
    private IEnumerator StartAttack(Transform targetTransform, ICanTakeDamagePlayableObject canTakeDamagePlayableObject)
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        yield return waitForFixedUpdate;

        SelectableAbstract targetSelectableAbstract = targetTransform.GetComponent<SelectableAbstract>();

        while (!targetSelectableAbstract.isDead)
        {
            yield return waitForFixedUpdate;

            Transform bulletTransform = EventManager.pickRequestFromPool(Enums.ObjectType.Bullet);

            Bullet bullet = bulletTransform.GetComponent<Bullet>();
            bullet.ShootFire(transform, targetTransform, soldierStructData.soldierDamage);

            //Only One Bullet Can Fire Same Time
            yield return new WaitUntil(() => !bullet.IsMoveing());
        }
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
