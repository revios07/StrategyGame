using UnityEngine;

namespace Interfaces
{
    public interface ICanAttackObject
    {
        public void GiveDamage(Transform targetTransform, ICanTakeDamagePlayableObject canTakeDamagePlayableObject);
    }
}
