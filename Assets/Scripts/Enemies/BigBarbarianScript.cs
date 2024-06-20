using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBarbarianScript : MonoBehaviour
{
    public int scoreGiven;
    private float movementSpeed;
    private Rigidbody2D rb;
    private Renderer enemyRenderer;
    private Color originalColor;
    private int healthPoints;
    private float wallDamage = 0.15f;
    void Start()
    {
        healthPoints = GameManager.Instance.bigBarbHP;
        movementSpeed = GameManager.Instance.bigBarbSpeed;
        rb = GetComponent<Rigidbody2D>();
        enemyRenderer = GetComponent<Renderer>();
        originalColor = enemyRenderer.material.color; // Store the original color
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + -transform.up * movementSpeed);
    }

    public void ReceiveDamage(int damage){
        if(healthPoints - damage < 0){
            //Death Anim
            Destroy(gameObject);
            GameManager.Instance.AddScore(scoreGiven);
        }
        else{
            StartCoroutine("HitEffect");
            healthPoints-= damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Wall"){
            GameManager.Instance.ReceiveDamage(wallDamage);
            Destroy(gameObject);
        }
    }

    private IEnumerator HitEffect()
    {
        enemyRenderer.material.color = Color.red; // Change color to red
        yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds
        enemyRenderer.material.color = originalColor; // Revert to original color
    }
}
