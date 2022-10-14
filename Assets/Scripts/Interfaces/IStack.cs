using UnityEngine;

namespace Interfaces
{
    public interface IStack
    {
        void Add();
        void Remove();
        void ClearStaticStack(Transform transform);
    }
}