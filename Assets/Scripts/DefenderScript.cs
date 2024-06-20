using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderScript : MonoBehaviour
{
    public float speed; // Speed of the player
    public GameObject projectile;
    private float initialSpeed;
    private float sprintSpeed;
    private float xLimit = 6f; // Minimum x position
    private float shootCooldown;
    private float cooldownTime = 0.2f; 

    private void Start() {
        shootCooldown = 0;
        initialSpeed = speed;
        sprintSpeed = speed * 1.5f;
    }

    void Update()
    {
        if(shootCooldown > 0){
            shootCooldown-= Time.deltaTime;
        }
        float moveInput = Input.GetAxis("Horizontal");

        if(Input.GetKey(KeyCode.LeftShift)){
            if(speed != sprintSpeed){
                speed = sprintSpeed;
            }
        }
        else{
            if(speed != initialSpeed){
                speed = initialSpeed;
            }
        }

        float newX = transform.position.x + moveInput * speed * Time.deltaTime;

        newX = Mathf.Clamp(newX, -xLimit, xLimit);

        transform.position = new Vector2(newX, transform.position.y);

        if(shootCooldown <= 0 && Input.GetKeyDown(KeyCode.Space)){
            shootCooldown = cooldownTime;
            GameObject firedProjectile = Instantiate(projectile,transform.position + Vector3.up, Quaternion.identity);
        }
    }
}
