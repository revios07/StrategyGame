using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class ObjectPoolSystem : MonoBehaviour
{
    [SerializeField]
    private SoldierFactory _soldierFactory;
    [SerializeField]
    private BuildingFactory _buildingFactory;

    private Queue<GameObject> _soldiersPool = new Queue<GameObject>();
    private Queue<GameObject> _barracksPool = new Queue<GameObject>();
    private Queue<GameObject> _powerPlantsPool = new Queue<GameObject>();

    private void Awake()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < 20; ++i)
        {
            Building barracksClone = _buildingFactory.GetNewInstance("Barracks");
            Building powerPlantClone = _buildingFactory.GetNewInstance("PowerPlant");

            barracksClone.gameObject.SetActive(false);
            powerPlantClone.gameObject.SetActive(false);

            _barracksPool.Enqueue(barracksClone.gameObject);
            _powerPlantsPool.Enqueue(powerPlantClone.gameObject);
        }

        for (int i = 0; i < 50; ++i)
        {
            Soldier soldierClone = (_soldierFactory.GetNewInstance("Soldier"));
            soldierClone.gameObject.SetActive(false);

            _soldiersPool.Enqueue(soldierClone.gameObject);

        }
    }

    public Transform GetObjectFromPool(ObjectType pooledObjectType)
    {
        switch (pooledObjectType)
        {
            case (ObjectType.Soldier):
                {
                    return _soldiersPool.Dequeue().transform;
                }
            case (ObjectType.Barracks):
                {
                    return _barracksPool.Dequeue().transform;
                }
            case (ObjectType.PowerPlant):
                {
                    return _powerPlantsPool.Dequeue().transform;
                }
        }

        return null;
    }

    public void AddToPool(ObjectType pooledObjectType, Transform pooledObject)
    {
        pooledObject.gameObject.SetActive(false);

        switch (pooledObjectType)
        {
            case (ObjectType.Soldier):
                {
                    _soldiersPool.Enqueue(pooledObject.gameObject);
                    break;
                }
            case (ObjectType.Barracks):
                {
                    _barracksPool.Enqueue(pooledObject.gameObject);
                    break;
                }
            case (ObjectType.PowerPlant):
                {
                    _powerPlantsPool.Enqueue(pooledObject.gameObject);
                    break;
                }
        }
    }
}

public enum ObjectType
{
    Soldier,
    Barracks,
    PowerPlant
}
