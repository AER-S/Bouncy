using System;
using UnityEngine;

public class TrajectoryIndicator : MonoBehaviour
{
        [SerializeField] private LineRenderer trajectory;
        [SerializeField] private int trajectorySteps;
        [SerializeField] private float timeStep;
        private PlayerController _player;
        
        private void OnEnable()
        {
                
                PlayerController.StartPulling += StartPulling;
                PlayerController.FinishedPulling += EndPulling;
        }

        private void OnDisable()
        {
                PlayerController.StartPulling -= StartPulling;
                PlayerController.FinishedPulling -= EndPulling;
        }

        private void Start()
        {
                _player = PlayerManager.Instance.Player;
                trajectory.enabled = false;
                trajectory.positionCount = trajectorySteps;
        }

        private void Update()
        {
                if (trajectory.enabled) CalculateTrajectory();
        }

        private void CalculateTrajectory()
        {
                Vector3 startPosition = _player.transform.position;
                Vector3 force = _player.Force;
                Vector3 startVelocity = force.normalized * (force.magnitude / _player.Rigidbody.mass);
                for (int i = 0; i < trajectorySteps; i++)
                {
                        float t = timeStep * i;
                        Vector3 position = startPosition + (startVelocity * t) + Physics.gravity * (0.5f * (t * t));
                        trajectory.SetPosition(i,position);
                }
        }

        private void StartPulling()
        {
                trajectory.enabled = true;
        }

        private void EndPulling()
        {
                trajectory.enabled = false;
        }
}