using UnityEngine;

namespace Source.Scripts.Gameplay
{
    public class CameraAspectHandler : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _horizontalSize = 5f;
        [SerializeField] private float _verticalSize = 15f;

        private void Update()
        {
            var size = GetSize();
            
            if(!Mathf.Approximately(_camera.orthographicSize, size))
                _camera.orthographicSize = size;
        }

        private float GetSize() =>
            Screen.width > Screen.height 
                ? _horizontalSize 
                : _verticalSize;
    }
}
