using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bullet/BulletData", fileName = "BulletData")]
public class BulletScriptable : ScriptableObject
{
    [Range(1f, 500f)]
    public float bulletSpeed = 10f;
}
