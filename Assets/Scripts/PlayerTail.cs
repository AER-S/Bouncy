using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTail : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private LineRenderer tail;
    [SerializeField] private int pointsCount;

    private void Start()
    {
        tail.positionCount = pointsCount;
    }

    private void FixedUpdate()
    {
        UpdateTail();
    }

    private void UpdateTail()
    {
        for (int i = pointsCount-1; i > 0; i--)
        {
            tail.SetPosition(i,tail.GetPosition(i-1));
        }
        tail.SetPosition(0, player.transform.position);
    }
}
