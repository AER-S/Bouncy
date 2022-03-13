using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomTo;
    private Camera _camera;
    private float _initialSize;

    private Coroutine _zooming;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _initialSize = _camera.orthographicSize;
    }

    private void OnEnable()
    {
        TakeInputs.StartPulling += ZoomOut;
        TakeInputs.FinishedPulling += ZoomIn;
    }

    private void OnDisable()
    {
        TakeInputs.StartPulling -= ZoomOut;
        TakeInputs.FinishedPulling -= ZoomIn;
    }

    private void ZoomIn()
    {
        ZoomTo(_initialSize);
    }

    private void ZoomOut()
    {
        ZoomTo(zoomTo);
    }

    private void ZoomTo(float newZoomValue)
    {
        if(_zooming!=null) StopCoroutine(_zooming);
        StartCoroutine(LerpZoomTo(newZoomValue));
    }

    IEnumerator LerpZoomTo(float newZoomValue)
    {
        float initialDifference = newZoomValue - _camera.orthographicSize;
        while (Mathf.Abs(_camera.orthographicSize-newZoomValue)>0.01f)
        {
            _camera.orthographicSize += initialDifference / zoomSpeed;
            yield return null;
        }
    }
}
