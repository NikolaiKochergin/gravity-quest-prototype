using UnityEngine;

namespace Source.Scripts
{
    public class Player : MonoBehaviour
    {
        public void SetMoveInput(float value)
        {
            Debug.Log($"<color=orange> Move {value}");
        }

        public void RequestJump()
        {
            Debug.Log($"<color=green> RequestJump");
        }
    }
}