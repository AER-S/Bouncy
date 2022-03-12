using System;
using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private float explosionTime;

    private void Start()
    {
        mesh.gameObject.SetActive(true);
        explosion.gameObject.SetActive(false);
    }

    public void TakeDamage()
    {
        mesh.gameObject.SetActive(false);
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        explosion.gameObject.SetActive(true);
        yield return new WaitForSeconds(explosionTime);
        Destroy(gameObject);
    }
}