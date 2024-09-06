using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float force;
    public float damage;
    private void Start()
    {
        rb.AddForce(transform.right*force);
    }
    void Update()
    {
        //rb.AddForce(new Vector2(force,0.1f) * Time.deltaTime);
    }
}
