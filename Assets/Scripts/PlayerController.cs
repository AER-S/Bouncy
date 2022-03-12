using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public event Action StartPulling;
    public event Action FinishedPulling;
    
    [SerializeField] private float forceMax;
    [SerializeField] private float forceFactor;
    [SerializeField] private float timeFactor;
    private Vector2 _clickStartPosition;
    private Vector2 _mouseCurrentPosition;
    private Rigidbody _rigidbody;
    private bool _pulling;
    private Vector2 _force;

    #region Getters

    public Vector2 Force => _force;
    public Rigidbody Rigidbody => _rigidbody;

    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartPulling += OnStartPulling;
        FinishedPulling += OnStopPulling;
        
    }

    private void OnDisable()
    {
        StartPulling -= OnStartPulling;
        FinishedPulling -= OnStopPulling;
    }
    

    // Update is called once per frame
    void Update()
    {
        TakeInput();
        if (_pulling)
        {
            _force = -(_mouseCurrentPosition - _clickStartPosition)*forceFactor;
            _force = _force.normalized * ((_force.magnitude > forceMax) ? forceMax : _force.magnitude);
        }
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

    private void Release()
    {
        _rigidbody.velocity=Vector3.zero;
        _rigidbody.AddForce(_force,ForceMode.Impulse);
    }

    private void SlowerTime()
    {
        Time.timeScale = timeFactor;
        Time.fixedDeltaTime *= timeFactor;
    }

    private void NormalTime()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime /= timeFactor;
    }

    private void OnStartPulling()
    {
        _pulling = true;
        SlowerTime();
    }

    private void OnStopPulling()
    {
        _pulling = false;
        NormalTime();
        Release();
    }
}
