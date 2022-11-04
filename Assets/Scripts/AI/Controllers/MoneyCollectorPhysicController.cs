using AI.Subscribers;
using Enums;
using UnityEngine;

namespace AI.Controllers
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class MoneyCollectorPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private MoneyCollector moneyCollector;
        [SerializeField] private CapsuleCollider capsuleCollider;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(StackTypes.Money.ToString()))
            {
                moneyCollector.Target = other.transform.parent;
            }
        }
        
        public void IncreaseColliderRadius()
        {
            if (capsuleCollider.radius<=50f)
            {
                capsuleCollider.radius += 2f;
            }
            else
            {
                ResetColliderRadius();
            }
        }

        public void ResetColliderRadius()
        {
            capsuleCollider.radius = .5f;
        }
    }
}