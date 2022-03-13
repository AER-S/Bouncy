using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class LensDistortionVFX : VFX<LensDistortion>
{
    protected override void LerpEffect()
    {
        _volumeComponent.intensity.value = (_pulling)
            ? (Mathf.Lerp(_initialDistortion, intensity, _timeElapsed * effectsSpeed))
            : Mathf.Lerp(_initialDistortion, 0f, _timeElapsed * effectsSpeed);
    }

    protected override void InitializeEffect()
    {
        _initialDistortion = _volumeComponent.intensity.value;
    }
}
