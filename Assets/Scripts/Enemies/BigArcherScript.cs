using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigArcherScript : MonoBehaviour
{
    public int scoreGiven;
    public float shootRange;
    public GameObject archerProjectile;
    public float shootCooldown;
    private float movementSpeed;
    private Rigidbody2D rb;
    private Renderer enemyRenderer;
    private Color originalColor;
    private int healthPoints;
    private bool onRange;
    void Start()
    {
        onRange = false;
        healthPoints = GameManager.Instance.bigarcherHP;
        movementSpeed = GameManager.Instance.bigarcherSpeed;
        rb = GetComponent<Rigidbody2D>();
        enemyRenderer = GetComponent<Renderer>();
        originalColor = enemyRenderer.material.color; // Store the original color
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > shootRange){
            rb.MovePosition(transform.position - transform.up * movementSpeed);
        }
        else if(!onRange){
            onRange = true;
            StartCoroutine("ShootArrow");
        }
    }

    public void ReceiveDamage(int damage){
        if(healthPoints - damage < 0){
            //Death Anim
            int randomChance = Random.Range(0, 10);
            if(!GameManager.Instance.activePowerUp && randomChance <= 3){
                Instantiate(GameManager.Instance.powerUpBox,transform.position,Quaternion.identity);
            }
            Destroy(gameObject);
            GameManager.Instance.AddScore(scoreGiven);
        }
        else{
            StartCoroutine("HitEffect");
            healthPoints-= damage;
        }
    }

    private IEnumerator ShootArrow(){
        while(true){
            GameObject arrow = Instantiate(archerProjectile,transform.position - Vector3.up/2, Quaternion.identity);
            arrow.GetComponent<SpriteRenderer>().flipY = true;
            yield return new WaitForSeconds(shootCooldown);
        }
    }

    private IEnumerator HitEffect()
    {
        enemyRenderer.material.color = Color.red; // Change color to red
        yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds
        enemyRenderer.material.color = originalColor; // Revert to original color
    }
}
