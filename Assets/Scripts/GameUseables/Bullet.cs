using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool _isMoveing;
    private IEnumerator _attackCoroutine;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    public void SetFire(Transform spawn, Transform to, int damage)
    {
        transform.position = spawn.position;
        transform.LookAt(to);
        transform.gameObject.SetActive(true);
        _attackCoroutine = MoveToTarget(spawn, to, damage);
        StartCoroutine(_attackCoroutine);
    }

    private IEnumerator MoveToTarget(Transform spawn, Transform to, int damage)
    {
        _isMoveing = true;

        yield return waitForFixedUpdate;

        while (true)
        {
            yield return waitForFixedUpdate;

            transform.position = Vector3.Lerp(transform.position, to.position, 10f * Time.fixedDeltaTime);

            if(Vector2.Distance(transform.position, to.position) < 0.5f)
            {
                //Hitted Here
                to.GetComponent<ICanTakeDamagePlayableObject>().TakeDamage(damage);
                _isMoveing = false;
                EventManager.onObjectAddToPool(Enums.ObjectType.Bullet, transform);
                yield break;
            }
        }
    }
}
