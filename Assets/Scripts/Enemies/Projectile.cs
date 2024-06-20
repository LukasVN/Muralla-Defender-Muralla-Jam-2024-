using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float projectileSpeed;
    private Rigidbody2D rb;
    void Start()
    {
        projectileSpeed = GameManager.Instance.projectileSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.MovePosition(transform.position + transform.up * projectileSpeed);

        if(transform.position.y > 5.25){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.tag)
        {
            case "Defender": break;
            case "Wall": break;
            case "Barbarian":
                other.gameObject.GetComponent<BarbarianScript>().ReceiveDamage(GameManager.Instance.projectileDamage);
                Destroy(gameObject);
            break;
            case "BigBarbarian":
                other.gameObject.GetComponent<BigBarbarianScript>().ReceiveDamage(GameManager.Instance.projectileDamage);
                Destroy(gameObject);
            break;
            case "Archer":
                other.gameObject.GetComponent<ArcherScript>().ReceiveDamage(GameManager.Instance.projectileDamage);
                Destroy(gameObject);
            break;
            case "BigArcher":
                other.gameObject.GetComponent<BigArcherScript>().ReceiveDamage(GameManager.Instance.projectileDamage);
                Destroy(gameObject);
            break;
            default:
                Destroy(other.gameObject);
                Destroy(gameObject);
            break;
        }
    }
}
