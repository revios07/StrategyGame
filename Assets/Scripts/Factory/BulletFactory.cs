using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : GameObjectFactory<Bullet>
{
    [SerializeField]
    private GameObject _bulletPrefab; 

    public override Bullet GetNewInstance(string gameObjectType)
    {
        Bullet bulletCreated = Instantiate(_bulletPrefab).GetComponent<Bullet>();
        return bulletCreated;
    }
}
