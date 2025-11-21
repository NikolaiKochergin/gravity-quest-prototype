using UnityEngine;

namespace Source.Scripts.Gameplay.Configs
{
    [CreateAssetMenu(
        fileName = "New Player Config", 
        menuName = "Gravity Quest/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float _gravity = 30f;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _acceleration = 20f;
        [SerializeField] private float _jumpForce = 10f;
        [SerializeField] private float _rotationSpeed = 25f;
        
        [Header("Ground Check")]
        [SerializeField] private float _groundMaxDistance = 0.1f;
        
        public float Gravity => _gravity;
        public float MoveSpeed => _moveSpeed;
        public float Acceleration => _acceleration;
        public float JumpForce => _jumpForce;
        public float RotationSpeed => _rotationSpeed;
        public float GroundMaxDistance => _groundMaxDistance;
    }
}