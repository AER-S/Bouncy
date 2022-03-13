using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTail : MonoBehaviour
{
    [SerializeField] private LineRenderer tail;
    [SerializeField] private int pointsCount;

    private PlayerController _player;
    private float _counter;

    private float _timeFactor;

    private void OnEnable()
    {
        
       PlayerController.StartPulling += StretchTime;
       PlayerController.FinishedPulling += NormalTime;
    }

    private void OnDisable()
    {
        PlayerController.StartPulling -= StretchTime;
        PlayerController.FinishedPulling -= NormalTime;
    }

    private void Start()
    {
        _player = PlayerManager.Instance.Player;
        tail.positionCount = pointsCount;
        ResetCounter();
    }

    private void Update()
    {
        IncreaseCounter(Time.fixedDeltaTime);
        if (_counter >= _timeFactor * Time.fixedDeltaTime)
        {
            UpdateTail();
            ResetCounter();
        }
    }

    private void StretchTime()
    {
        _timeFactor = 1 / _player.TimeFactor;
    }

    private void NormalTime()
    {
        _timeFactor = 1;
    }

    private void ResetCounter() => _counter = 0;

    private void IncreaseCounter(float time) => _counter += time;

    private void UpdateTail()
    {
        for (int i = pointsCount-1; i > 0; i--)
        {
            tail.SetPosition(i,tail.GetPosition(i-1));
        }
        tail.SetPosition(0, _player.transform.position);
    }
}
