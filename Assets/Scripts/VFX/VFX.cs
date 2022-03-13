using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public abstract class VFX<T> : MonoBehaviour where T : VolumeComponent
{
    [SerializeField]protected int componentIndex;
    [SerializeField] protected float effectsSpeed;
    [SerializeField] protected float intensity;

    protected T _volumeComponent;
    protected bool _pulling;
    protected float _timeElapsed;
    protected float _initialDistortion;

    protected virtual void Awake()
    {

        _volumeComponent = (T) GetComponent<Volume>().profile.components[componentIndex];
        _timeElapsed = 0;
        _initialDistortion = 0;
    }
    
    private void OnEnable()
    {
        TakeInputs.StartPulling += OnPullingStart;
        TakeInputs.FinishedPulling += OnPullingExit;
    }

    private void OnDisable()
    {
        TakeInputs.StartPulling -= OnPullingStart;
        TakeInputs.FinishedPulling -= OnPullingExit;
    }

    private void Update()
    {
        if (_timeElapsed < effectsSpeed)
        {
            LerpEffect();
            _timeElapsed += Time.deltaTime;
        }
    }

    protected abstract void LerpEffect();
    protected abstract void InitializeEffect();

    
    private void OnPullingStart()
    {
        _pulling = true;
        _timeElapsed = 0;
        InitializeEffect();
        
    }
    
    private void OnPullingExit()
    {
        _pulling = false;
        _timeElapsed = 0;
        
    }
}