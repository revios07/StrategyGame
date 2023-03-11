using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Enums;

public class ObjectPoolSystem : MonoBehaviour
{
    //Need Input For Spawn Object At Click Area
    [SerializeField]
    private InputData _inputData;
    //Factories
    [SerializeField]
    private SoldierFactory _soldierFactory;
    [SerializeField]
    private BuildingFactory _buildingFactory;
    [SerializeField]
    private BulletFactory _bulletFactory;

    private Dictionary<ObjectType, Queue<GameObject>> _pool = new Dictionary<ObjectType, Queue<GameObject>>();

    private void OnEnable()
    {
        EventManager.pickRequestFromPool += GetObjectFromPool;
        EventManager.onObjectAddToPool += AddToPool;
    }

    private void OnDisable()
    {
        EventManager.pickRequestFromPool -= GetObjectFromPool;
        EventManager.onObjectAddToPool -= AddToPool;
    }

    private void Awake()
    {
        if(_soldierFactory == null || _buildingFactory == null || _bulletFactory == null)
        {
            _soldierFactory = GetComponentInChildren<SoldierFactory>();
            _buildingFactory = GetComponentInChildren<BuildingFactory>();
            _bulletFactory = GetComponentInChildren<BulletFactory>();
        }

        CreatePool();
    }

    private void CreatePool()
    {
        _pool.Add(ObjectType.Barracks, new Queue<GameObject>());
        _pool.Add(ObjectType.PowerPlant, new Queue<GameObject>());
        _pool.Add(ObjectType.Soldier, new Queue<GameObject>());
        _pool.Add(ObjectType.Bullet, new Queue<GameObject>());


        for (int i = 0; i < 20; ++i)
        {
            Building barracksClone = _buildingFactory.GetNewInstance("Barracks");
            Building powerPlantClone = _buildingFactory.GetNewInstance("PowerPlant");
            Bullet bulletClone = _bulletFactory.GetNewInstance("Bullet");

            barracksClone.gameObject.SetActive(false);
            powerPlantClone.gameObject.SetActive(false);
            bulletClone.gameObject.SetActive(false);

            _pool.GetValueOrDefault(ObjectType.Barracks).Enqueue(barracksClone.gameObject);
            _pool.GetValueOrDefault(ObjectType.PowerPlant).Enqueue(powerPlantClone.gameObject);
            _pool.GetValueOrDefault(ObjectType.Bullet).Enqueue(bulletClone.gameObject);
        }

        for (int i = 0; i < 50; ++i)
        {
            Soldier soldierClone = (_soldierFactory.GetNewInstance("Soldier"));

            soldierClone.gameObject.SetActive(false);

            _pool.GetValueOrDefault(ObjectType.Soldier).Enqueue(soldierClone.gameObject);
        }
    }

    public Transform GetObjectFromPool(ObjectType pooledObjectType)
    {
        Transform pickTransform = null;
        ObjectType pickedObjectType = ObjectType.Barracks;

        switch (pooledObjectType)
        {
            case (ObjectType.Soldier):
                {
                    pickTransform = _pool.GetValueOrDefault(ObjectType.Soldier).Dequeue().transform;
                    pickedObjectType = ObjectType.Soldier;
                    break;
                }
            case (ObjectType.Barracks):
                {
                    pickTransform = _pool.GetValueOrDefault(ObjectType.Barracks).Dequeue().transform;
                    pickedObjectType = ObjectType.Barracks;
                    break;
                }
            case (ObjectType.PowerPlant):
                {
                    pickTransform = _pool.GetValueOrDefault(ObjectType.PowerPlant).Dequeue().transform;
                    pickedObjectType = ObjectType.PowerPlant;
                    break;
                }
            case (ObjectType.Bullet):
                {
                    pickTransform = _pool.GetValueOrDefault(ObjectType.Bullet).Dequeue().transform;
                    pickedObjectType = ObjectType.Bullet;
                    break;
                }
        }

        if (pickTransform == null)
            Debug.LogError("Non Assigned Picked Object!");

        pickTransform.GetComponent<IPoolableObject>().UseFromPool();

        pickTransform.transform.position = Camera.main.ScreenToWorldPoint(_inputData.GetMousePosition());
        pickTransform.transform.position += Vector3.forward * 10f;
        pickTransform.gameObject.SetActive(true);

        EventManager.pickedFromPool?.Invoke(pickedObjectType, pickTransform);

        return pickTransform;
    }

    public void AddToPool(ObjectType pooledObjectType, Transform pooledObject)
    {
        pooledObject.gameObject.SetActive(false);

        switch (pooledObjectType)
        {
            case (ObjectType.Soldier):
                {
                    _pool.GetValueOrDefault(ObjectType.Soldier).Enqueue(pooledObject.gameObject);
                    break;
                }
            case (ObjectType.Barracks):
                {
                    _pool.GetValueOrDefault(ObjectType.Barracks).Enqueue(pooledObject.gameObject);
                    break;
                }
            case (ObjectType.PowerPlant):
                {
                    _pool.GetValueOrDefault(ObjectType.PowerPlant).Enqueue(pooledObject.gameObject);
                    break;
                }
            case (ObjectType.Bullet):
                {
                    _pool.GetValueOrDefault(ObjectType.Bullet).Enqueue(pooledObject.gameObject);
                    break;
                }
        }
    }
}
