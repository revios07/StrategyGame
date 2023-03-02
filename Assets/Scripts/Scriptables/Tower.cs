using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Tower", fileName = "Tower")]
public class Tower : ScriptableObject
{
    [SerializeField]
    private string _towerName;
    [SerializeField]
    private int _towerIndex;
    [SerializeField]
    private int _towerHealth;
    [SerializeField]
    private int _towerDamage;
}
