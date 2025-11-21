using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Scripts.Gameplay
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Player _player;
        
        private InputAction _moveAction;
        private InputAction _jumpAction;

        private void Awake()
        {
            _moveAction = InputSystem.actions.FindAction("Move");
            _jumpAction = InputSystem.actions.FindAction("Jump");
        }

        private void OnEnable()
        {
            _moveAction.performed += OnMove;
            _moveAction.canceled += OnMove;

            _jumpAction.performed += OnJump;
        }

        private void OnMove(InputAction.CallbackContext ctx)
        {
            float value = ctx.ReadValue<float>();
            _player.SetMoveInput(value);
        }

        private void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                _player.RequestJump();
        }
    }
}