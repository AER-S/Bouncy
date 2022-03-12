using System;
using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private float explosionTime;
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        mesh.gameObject.SetActive(true);
        explosion.gameObject.SetActive(false);
        _collider.enabled = true;
    }

    public void TakeDamage()
    {
        mesh.gameObject.SetActive(false);
        _collider.enabled = false;
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        explosion.gameObject.SetActive(true);
        yield return new WaitForSeconds(explosionTime);
        Destroy(gameObject);
    }
}