using UnityEngine;

namespace Interfaces
{
    public interface IStack
    {
        void Add(GameObject gameObject);
        void Remove(string name);
        void ClearStaticStack(Transform transform);
    }
}