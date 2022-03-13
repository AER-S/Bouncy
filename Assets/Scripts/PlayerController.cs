using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    
    
    [SerializeField] private float forceMax;
    [SerializeField] private float forceFactor;
    
    [SerializeField] private float bouncingForce;
    
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
        TakeInputs.StartPulling += OnStartPulling;
        TakeInputs.FinishedPulling += OnStopPulling;
        
    }

    private void OnDisable()
    {
        TakeInputs.StartPulling -= OnStartPulling;
        TakeInputs.FinishedPulling -= OnStopPulling;
    }
    
    
    void Update()
    {
        if (_pulling)
        {
            _force = -(TakeInputs.Instance.CurrentPosition - TakeInputs.Instance.DragStartPosition)*forceFactor;
            _force = _force.normalized * ((_force.magnitude > forceMax) ? forceMax : _force.magnitude);
        }
    }

    
    private void Release()
    {
        _rigidbody.velocity=Vector3.zero;
        _rigidbody.AddForce(_force,ForceMode.Impulse);
    }

    

    private void OnStartPulling()
    {
        _pulling = true;
        
    }

    private void OnStopPulling()
    {
        _pulling = false;
        Release();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<IDamageable>() != null)
        {
            (other.gameObject.GetComponent<IDamageable>()).TakeDamage();
            ShootUp();
        }
    }

    private void ShootUp()
    {
        _rigidbody.AddForce(Vector3.up * bouncingForce, ForceMode.Impulse);
    }
}
