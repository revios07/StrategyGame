using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolableObject
{
    private bool _isMoveing;
    private IEnumerator _attackCoroutine;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    [SerializeField]
    private BulletScriptable _bulletData;

    public void ShootFire(Transform spawn, Transform to, int damage)
    {
        if (_isMoveing)
            return;

        _isMoveing = true;
        transform.localPosition = spawn.localPosition;

        //Angle Look To Target
        Vector3 diff = to.position - spawn.position;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        transform.gameObject.SetActive(true);
        _attackCoroutine = MoveToTarget(spawn, to, damage);
        StartCoroutine(_attackCoroutine);
    }
    public bool IsMoveing()
    {
        return _isMoveing;
    }
    private IEnumerator MoveToTarget(Transform spawn, Transform to, int damage)
    {
        Debug.Log(transform.position);

        while (true)
        {
            yield return waitForFixedUpdate;

            transform.position = Vector3.MoveTowards(transform.position, to.position, _bulletData.bulletSpeed * Time.fixedDeltaTime);

            if (Vector2.Distance(transform.position, to.position) < 10f)
            {
                //Hitted Here
                to.GetComponent<ICanTakeDamagePlayableObject>().TakeDamage(damage);
                break;
            }
        }

        _isMoveing = false;
        AddToPool();
    }
    public void AddToPool()
    {
        _isMoveing = false;
        gameObject.SetActive(false);
        EventManager.onObjectAddToPool(Enums.ObjectType.Bullet, transform);
    }
    public Transform UseFromPool()
    {
        return this.transform;
    }
}
