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
            _barracksPool.Enqueue(_buildingFactory.GetNewInstance("Barracks").gameObject);
            _powerPlantsPool.Enqueue(_buildingFactory.GetNewInstance("PowerPlant").gameObject);
        }

        for (int i = 0; i < 50; ++i)
        {
            _soldiersPool.Enqueue(_soldierFactory.GetNewInstance("Soldier").gameObject);
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
}

public enum ObjectType
{
    Soldier,
    Barracks,
    PowerPlant
}
