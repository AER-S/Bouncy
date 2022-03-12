using System;
using UnityEngine;

public class TrajectoryIndicator : MonoBehaviour
{
        [SerializeField] private PlayerController player;
        [SerializeField] private LineRenderer trajectory;
        [SerializeField] private int trajectorySteps;
        [SerializeField] private float timeStep;
        private bool _pulling;

        private void OnEnable()
        {
                player.StartPulling += StartPulling;
                player.FinishedPulling += EndPulling;
        }

        private void OnDisable()
        {
                player.StartPulling -= StartPulling;
                player.FinishedPulling -= EndPulling;
        }

        private void Start()
        {
                trajectory.enabled = false;
                trajectory.positionCount = trajectorySteps;
        }

        private void Update()
        {
                if (trajectory.enabled) CalculateTrajectory();
        }

        private void CalculateTrajectory()
        {
                Vector3 startPosition = player.transform.position;
                Vector3 force = player.Force;
                Vector3 startVelocity = force.normalized * (force.magnitude / player.Rigidbody.mass);
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