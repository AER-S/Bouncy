using System;
using UnityEngine;

public class TakeInputs : GenericSingleton<TakeInputs>
{
    public static event Action StartPulling;
    public static event Action FinishedPulling;
    
    private Vector2 _clickStartPosition;
    private Vector2 _mouseCurrentPosition;

    #region Getters

    public Vector2 DragStartPosition => _clickStartPosition;
    public Vector2 CurrentPosition => _mouseCurrentPosition;

    #endregion

    private void Update()
    {
        TakeInput();
    }

    private void TakeInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartPulling?.Invoke();
            _clickStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0)) _mouseCurrentPosition = Input.mousePosition;
        if (Input.GetMouseButtonUp(0)) FinishedPulling?.Invoke();
    }
}
