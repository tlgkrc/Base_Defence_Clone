using UnityEngine;

namespace Interfaces
{
    public interface IDamagable
    {
        void TakeDamage(int damage);
        Transform GetTransform();
    }
}