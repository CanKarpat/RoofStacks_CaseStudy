using UnityEngine;

namespace _Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;

        public float smoothedSpeed = 0.125f;

        private void FixedUpdate()
        {
            var smoothedPosition = Vector3.Lerp(transform.position, target.position, smoothedSpeed);
            transform.position = smoothedPosition;
        }
    }
}
