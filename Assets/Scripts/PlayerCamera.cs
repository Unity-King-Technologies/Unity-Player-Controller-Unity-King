using UnityEngine;

namespace UnityKing.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("Target")]
        public Transform target;
        public Vector3 offset = new Vector3(0, 1.6f, 0);

        [Header("Look Settings")]
        public float sensitivity = 2f;
        public float minY = -60f;
        public float maxY = 80f;

        private float rotX;
        private float rotY;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void HandleLook(Vector2 lookInput)
        {
            if (!target) return;

            rotX += lookInput.x * sensitivity;
            rotY -= lookInput.y * sensitivity;
            rotY = Mathf.Clamp(rotY, minY, maxY);

            transform.rotation = Quaternion.Euler(rotY, rotX, 0f);
            transform.position = target.position + offset;

            target.rotation = Quaternion.Euler(0f, rotX, 0f);
        }
    }
}
