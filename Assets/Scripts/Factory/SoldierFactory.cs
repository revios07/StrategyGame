using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierFactory : GameObjectFactory<Soldier>
{
    [SerializeField]
    private SoldierScriptable[] _soldierDatas;
    [SerializeField]
    private GameObject[] _soldierPrefabs;
    private int index = -1;

    public override Soldier GetNewInstance(string gameObjectType)
    {
        //Spawn Soldier With Random Health
        ++index;
        if (index >= _soldierPrefabs.Length)
        {
            index = 0;
        }

        Soldier soldierCreated = Instantiate(_soldierPrefabs[index]).GetComponent<Soldier>();
        return soldierCreated;
    }
}
