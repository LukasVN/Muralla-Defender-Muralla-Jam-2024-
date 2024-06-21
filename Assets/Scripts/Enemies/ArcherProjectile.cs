using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherProjectile : MonoBehaviour
{
    private float projectileSpeed;
    private float damage;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = GameManager.Instance.archerProjectileDamage;
        projectileSpeed = GameManager.Instance.ArcherProjectileSpeed;
    }

    void Update()
    {
        rb.MovePosition(transform.position - transform.up * projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Wall"){
            GameManager.Instance.ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }

}
