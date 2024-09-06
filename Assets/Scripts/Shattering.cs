using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shattering : MonoBehaviour
{
    [SerializeField] private GameObject shatteredPrefab;
    public float breakForce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        shatteredPrefab.SetActive(true);
        Destroy(gameObject);
    }
}
