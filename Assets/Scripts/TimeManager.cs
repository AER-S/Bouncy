using UnityEngine;

public class TimeManager : GenericSingleton<TimeManager>
{
    [SerializeField] private float timeFactor;
    
    public float TimeFactor => timeFactor;
    private void OnEnable()
    {
        TakeInputs.StartPulling += SlowerTime;
        TakeInputs.FinishedPulling += NormalTime;
    }

    private void OnDisable()
    {
        TakeInputs.StartPulling -= SlowerTime;
        TakeInputs.FinishedPulling -= NormalTime;
        
    }
    private void SlowerTime()
    {
        Debug.Log("Stretching time");
        Time.timeScale = timeFactor;
        Time.fixedDeltaTime *= timeFactor;
    }
    
    private void NormalTime()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime /= timeFactor;
    }
}
