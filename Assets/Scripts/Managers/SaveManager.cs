using UnityEngine;


namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables



        #endregion

        #endregion

        private void Awake()
        {

        }

        #region EventSubscribtion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {

        }

        private void UnsubscribeEvents()
        {

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

    }
}