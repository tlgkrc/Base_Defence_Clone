using UnityEngine;

namespace Interfaces
{
    public interface IStack
    {
        void Add(GameObject gameObject);
        void Remove();
        void ClearStaticStack(Transform transform);
    }
}