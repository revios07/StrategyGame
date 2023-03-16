using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public abstract class SoldierAbstract : SelectableAbstract, IPoolableObject, ICanAttackObject, ICanTakeDamagePlayableObject
{
    public int soldierHealth => soldierStructData.soldierHealth;
    public SoldierDataSO[] soldierScriptable;
    protected Structs.SoldierStruct soldierStructData;

    protected Structs.SoldierStruct GetRandomSoldier()
    {
        return soldierScriptable[Random.Range(0, soldierScriptable.Length)].GetSoldierData();
    }

    #region PathFind Functions
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
    #endregion

    #region Pool Calls
    public override void AddToPool()
    {
        base.AddToPool();
    }
    public override Transform UseFromPool()
    {
        //Reset Soldier Health Here
        //Give Random Health To Soldier
        soldierStructData = GetRandomSoldier();
        SetMaxValueOfSlide(soldierStructData.soldierHealth);
        SetSliderValue(soldierStructData.soldierHealth);

        base.UseFromPool();

        return this.transform;
    }
    #endregion

    #region Placement
    public override void PlaceToArea()
    {
        base.PlaceToArea();

        isPlaced = true;
        soldierStructData.isPlaced = isPlaced;
    }
    #endregion

    #region Damage/Health
    public override void TakeDamage(int damage)
    {
        if (base.isDead)
            return;

        soldierStructData.soldierHealth -= damage;
        if (soldierStructData.soldierHealth <= 0)
        {
            GamePlayController.currentlyTakeingDamageSoldiers.Remove(this as Soldier);
            soldierStructData.soldierHealth = 0;
            base.isDead = true;
            AddToPool();
            //Soldier Destroyed Here
            //Add Pool Again GameObject
        }

        if (!GamePlayController.currentlyTakeingDamageSoldiers.Contains(this as Soldier))
        {
            GamePlayController.currentlyTakeingDamageSoldiers.Add(this as Soldier);
        }

        EventManager.onSelectableTakeDamageInGame?.Invoke(this);
        base.TakeDamage(damage);
        healthTextUpdater.WriteHealth(soldierStructData.soldierHealth);
        SetSliderValue(soldierStructData.soldierHealth);
    }
    public void GiveDamage(Transform targetTransform, ICanTakeDamagePlayableObject canTakeDamagePlayableObject)
    {
        //Give Damage To Object
        if (this.gameObject.activeInHierarchy)
        {
            StartCoroutine(StartAttack(targetTransform, canTakeDamagePlayableObject));
        }
        else if (GamePlayController.currentlyAttakingSoldiers.Contains(this as Soldier))
        {
            GamePlayController.currentlyAttakingSoldiers.Remove(this as Soldier);
        }
    }
    private IEnumerator StartAttack(Transform targetTransform, ICanTakeDamagePlayableObject canTakeDamagePlayableObject)
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        yield return waitForFixedUpdate;

        SelectableAbstract targetSelectableAbstract = targetTransform.GetComponent<SelectableAbstract>();

        while (!targetSelectableAbstract.isDead || isDead)
        {
            yield return waitForFixedUpdate;

            Transform bulletTransform = EventManager.pickRequestFromPool(Enums.ObjectType.Bullet);

            Bullet bullet = bulletTransform.GetComponent<Bullet>();
            bullet.ShootFire(transform, targetTransform, soldierStructData.soldierDamage);

            //Only One Bullet Can Fire Same Time
            yield return new WaitUntil(() => !bullet.IsMoveing());

            if (targetSelectableAbstract.isDead)
                Debug.Log("Dead!");
        }

        GamePlayController.currentlyAttakingSoldiers.Remove(GetComponent<Soldier>());
    }
    #endregion
}
