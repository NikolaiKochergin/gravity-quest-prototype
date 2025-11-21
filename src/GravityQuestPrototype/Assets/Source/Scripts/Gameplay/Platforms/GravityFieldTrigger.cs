using UnityEngine;

namespace Source.Scripts.Gameplay
{
    public class GravityFieldTrigger : MonoBehaviour
    {
        [SerializeField] private Collider2D _platformCollider;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.attachedRigidbody.TryGetComponent(out Player player))
                player.SetPlatform(_platformCollider);
        }
    }
}