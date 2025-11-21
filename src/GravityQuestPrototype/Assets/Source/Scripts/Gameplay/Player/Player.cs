using Source.Scripts.Gameplay.Configs;
using UnityEngine;

namespace Source.Scripts.Gameplay
{
    public class Player : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Collider2D _platformCollider;

        [Header("Movement")] 
        [SerializeField] private float _gravity = 30f;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _acceleration = 20f;
        [SerializeField] private float _jumpForce = 10;
        [SerializeField] private float _rotationSpeed = 25f;
        
        [Header("Ground Check")]
        [SerializeField] private float _groundMaxDistance = 0.1f;
        
        private float _moveInput;
        private bool _isJumpRequested;
        private bool _isGrounded;

        private Vector2 _currentDown = Vector2.down;

        private void Awake()
        {
            _rigidbody.gravityScale = 0f;
            _rigidbody.freezeRotation = true;
        }

        public void SetConfiguration(PlayerConfig config)
        {
            _gravity = config.Gravity;
            _moveSpeed = config.MoveSpeed;
            _acceleration = config.Acceleration;
            _jumpForce = config.JumpForce;
            _rotationSpeed = config.RotationSpeed;
            _groundMaxDistance = config.GroundMaxDistance;
        }

        public void SetPlatform(Collider2D collider) => 
            _platformCollider = collider;

        public void SetMoveInput(float value) => 
            _moveInput = Mathf.Clamp(value, -1f, 1f);

        public void RequestJump() => 
            _isJumpRequested = true;

        private void FixedUpdate()
        {
            if(!_platformCollider)
                return;

            HandleGravity();
            HandleMove();
            HandleJump();
            HandleRotation();
        }

        private void HandleGravity()
        {
            Vector2 position = _rigidbody.position;
            Vector2 closest = _platformCollider.ClosestPoint(position);
            Vector2 toSurface = closest - position;
            float distance = toSurface.magnitude;
            
            Vector2 targetDown = distance > 0.0001f ? toSurface / distance : _currentDown;

            _currentDown = Vector2.Lerp(_currentDown, targetDown, _rotationSpeed * Time.fixedDeltaTime);
            _currentDown.Normalize();
            
            _isGrounded = distance <= _groundMaxDistance;
            _rigidbody.AddForce(_currentDown * _gravity);
        }

        private void HandleMove()
        {
            Vector2 currentRight = new Vector2(-_currentDown.y, _currentDown.x);
            
            float targetSpeed = _moveInput * _moveSpeed;
            float currentSpeed = Vector2.Dot(_rigidbody.linearVelocity, currentRight);
            float speedDifference = targetSpeed - currentSpeed;
            
            _rigidbody.AddForce(currentRight * (speedDifference * _acceleration));
        }

        private void HandleJump()
        {
            if (_isJumpRequested && _isGrounded)
            {
                float velocityAlongDown = Vector2.Dot(_rigidbody.linearVelocity, _currentDown);
                if(velocityAlongDown > 0f)
                    _rigidbody.linearVelocity -= _currentDown * velocityAlongDown;
                
                _rigidbody.AddForce(-_currentDown * _jumpForce, ForceMode2D.Impulse);
            }
            _isJumpRequested = false;
        }

        private void HandleRotation()
        {
            Vector2 upDirection = -_currentDown;
            float angle = Mathf.Atan2(upDirection.y, upDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
        }
    }
}