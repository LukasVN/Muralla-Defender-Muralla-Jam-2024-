using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool piercing;
    private int damage;
    private float projectileSpeed;
    private Rigidbody2D rb;
    void Start()
    {
        piercing = false;
        if(GameManager.Instance.powerUpProjectileState){
            damage = GameManager.Instance.projectileDamage * 2;
        }
        else{
            damage = GameManager.Instance.projectileDamage;
        }
        projectileSpeed = GameManager.Instance.projectileSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!piercing && GameManager.Instance.powerUpProjectilePiercing){
            piercing = true;
        }
        else if(piercing && !GameManager.Instance.powerUpProjectilePiercing){
            piercing = false;
        }

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
                other.gameObject.GetComponent<BarbarianScript>().ReceiveDamage(damage);
                if(!piercing){
                    Destroy(gameObject);
                }
            break;
            case "BigBarbarian":
                other.gameObject.GetComponent<BigBarbarianScript>().ReceiveDamage(damage);
                if(!piercing){
                    Destroy(gameObject);
                }
            break;
            case "Archer":
                other.gameObject.GetComponent<ArcherScript>().ReceiveDamage(damage);
                if(!piercing){
                    Destroy(gameObject);
                }
            break;
            case "BigArcher":
                other.gameObject.GetComponent<BigArcherScript>().ReceiveDamage(damage);
                if(!piercing){
                    Destroy(gameObject);
                }
            break;
            case "PowerUpBox":
                Destroy(gameObject);
            break;
            case "Projectile":
                Destroy(other.gameObject);
                if(!piercing){
                    Destroy(gameObject);
                }
            break;
        }
    }
}
