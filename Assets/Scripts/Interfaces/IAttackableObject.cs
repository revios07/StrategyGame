using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanAttackObject
{
    public void GiveDamage(ICanTakeDamagePlayableObject canTakeDamagePlayableObject);
}
