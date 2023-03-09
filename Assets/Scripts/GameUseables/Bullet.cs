using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolableObject
{
    private bool _isMoveing;
    private IEnumerator _attackCoroutine;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    public void ShootFire(Transform spawn, Transform to, int damage)
    {
        if (_isMoveing)
            return;

        _isMoveing = true;
        transform.localPosition = spawn.localPosition;
        transform.LookAt(to);
        transform.eulerAngles += Vector3.forward * 90f;
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

            transform.position = Vector3.Lerp(transform.position, to.position, 2f * Time.fixedDeltaTime);

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
