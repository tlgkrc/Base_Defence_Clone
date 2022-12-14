using Commands;
using Data.ValueObject;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion

        #region Serialized Variables

        [SerializeField] private bool isReadyForTouch, isFirstTimeTouchTaken;
        [SerializeField] private FloatingJoystick floatingJoystick;

        #endregion

        #region Private Variables

        private bool _isTouching;
        private float _currentVelocity; 
        private Vector2? _mousePosition; 
        private Vector3 _moveVector; 
        private QueryPointerOverUIElementCommand _queryPointerOverUIElementCommand;

        #endregion

        #endregion
        
        private void Awake()
        {
            Init();
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            InitSettings();
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
            CoreGameSignals.Instance.onReset += OnReset;
            BaseSignals.Instance.onSetTurretRotation += OnSetTurretRotation;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
            CoreGameSignals.Instance.onReset -= OnReset;
            BaseSignals.Instance.onSetTurretRotation -= OnSetTurretRotation;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void Init()
        {
            _queryPointerOverUIElementCommand = new QueryPointerOverUIElementCommand();
        }
        
        private void InitSettings()
        {
            isReadyForTouch = true;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                InputSignals.Instance.onInputTaken?.Invoke();
            }
            if (Input.GetMouseButton(0))
            {
                MouseButtonDown();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                MouseButtonUp();   
            }
        }

        #region Event Methods
        
        private void OnEnableInput()
        {
            isReadyForTouch = true;
        }
        
        private void OnDisableInput()
        {
            isReadyForTouch = false;
        }

        private void OnReset()
        {
            isReadyForTouch = false;
            isFirstTimeTouchTaken = false;
        }

        private void OnNextLevel() 
        {
            isReadyForTouch = false;
            isFirstTimeTouchTaken = false;
        }

        #endregion

        private void MouseButtonUp()
        {
            _moveVector = Vector3.zero;
            InputSignals.Instance.onInputReleased?.Invoke();
        }

        private void MouseButtonDown()
        {
            if (!isFirstTimeTouchTaken)
            {
                isFirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
            }
            _mousePosition = Input.mousePosition;
            JoystickInput();
        }
        
        private void JoystickInput()
        {
            _moveVector.x = floatingJoystick.Horizontal;
            _moveVector.z = floatingJoystick.Vertical;

            InputSignals.Instance.onJoystickDragged?.Invoke(new IdleInputParams()
            {
                ValueX = _moveVector.x,
                ValueZ = _moveVector.z
            });
        }

        private float OnSetTurretRotation()
        {
            return floatingJoystick.Horizontal;
        }
    }
}