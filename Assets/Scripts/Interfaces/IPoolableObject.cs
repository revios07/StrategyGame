using UnityEngine;

namespace Interfaces
{
    public interface IPoolableObject
    {
        public void AddToPool();
        public Transform UseFromPool();
    }
}
