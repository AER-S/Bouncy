using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
        private PlayerController cameraTarget;

        private Vector3 initialOffset;
        
        [System.Serializable]
        private struct MyRange
        {
                public float min;
                public float max;
        }

        [SerializeField] private MyRange xRange;
        [SerializeField] private MyRange yRange;

        private void Start()
        {
                cameraTarget = PlayerManager.Instance.Player;
                initialOffset = transform.position - cameraTarget.transform.position;
        }

        private void LateUpdate()
        {
                UpdatePosition();
        }

        private void UpdatePosition()
        {
                Vector3 newPosition = NewPosition();
                MoveTo(newPosition);
        }

        private void MoveTo(Vector3 newPosition) => transform.position = newPosition;

        private Vector3 NewPosition()
        {
                
                Vector3 targetPosition = cameraTarget.transform.position;
                Vector3 position = targetPosition + initialOffset;
                if (position.x > xRange.max || position.x < xRange.min) Clamp(ref position.x, xRange);
                if (position.y > yRange.max || position.y < yRange.min) Clamp(ref position.y, yRange);
                return position;
        }

        private void Clamp(ref float value, MyRange valueRange)
        {
                if (value > valueRange.max) value = valueRange.max;
                if (value < valueRange.min) value = valueRange.min;
        }
}