using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class VisualFX : MonoBehaviour
{
    [SerializeField] private float lerpDuration;
    [SerializeField] private float distortion;
    [SerializeField] private float aberration;
    private Volume _volume;
    private LensDistortion _lensDistortion;
    private ChromaticAberration _chromaticAberration;
    private bool _pulling;
    private float _timeElapsed;
    private float _initialDistortion;
    private float _initialAberration;

    private void Awake()
    {
        _volume = GetComponent<Volume>();
        _lensDistortion = (LensDistortion) _volume.profile.components[0];
        _chromaticAberration = (ChromaticAberration) _volume.profile.components[1];
        _timeElapsed = 0;
        _initialDistortion = 0;
        _initialAberration = 0;
    }
    
    private void OnEnable()
    {
        PlayerController.StartPulling += OnPullingStart;
        PlayerController.FinishedPulling += OnPullingExit;
    }

    private void OnDisable()
    {
        PlayerController.StartPulling -= OnPullingStart;
        PlayerController.FinishedPulling -= OnPullingExit;
    }

    private void Update()
    {
        if (_pulling)
        {
            if (_timeElapsed < lerpDuration)
            {
                _lensDistortion.intensity.value = Mathf.Lerp(_initialDistortion, distortion, _timeElapsed/lerpDuration);
                _chromaticAberration.intensity.value = Mathf.Lerp(_initialAberration, aberration, _timeElapsed / lerpDuration);
                _timeElapsed += Time.deltaTime;
            }
            
        }

        else 
        {
            if (_timeElapsed < lerpDuration)
            {
                _lensDistortion.intensity.value = Mathf.Lerp(_initialDistortion, 0f, _timeElapsed/lerpDuration);
                _chromaticAberration.intensity.value = Mathf.Lerp(_initialAberration, 0, _timeElapsed / lerpDuration);
                _timeElapsed += Time.deltaTime;
            }
        }
    }


    private void OnPullingStart()
    {
        _pulling = true;
        _timeElapsed = 0;
        _initialDistortion = _lensDistortion.intensity.value;
    }

    private void OnPullingExit()
    {
        _pulling = false;
        _timeElapsed = 0;
        _initialDistortion = _lensDistortion.intensity.value;
    }
}