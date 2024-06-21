using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderScript : MonoBehaviour
{
    public static DefenderScript Instance;
    public AudioSource arrowShotSource;
    public AudioSource arrowHitSource;
    public AudioSource crateBreakSource;
    public AudioClip arrowShot;
    public AudioClip arrowHit;
    public AudioClip breakingCrate;
    public float speed; // Speed of the player
    public GameObject projectile;
    public GameObject powerUpProjectile;
    private Animator animator;
    private GameObject defaultProjectile;
    private float initialSpeed;
    private float sprintSpeed;
    private float xLimit = 6f; // Minimum x position
    private float shootCooldown;
    private float defaultCooldownTime;
    private float cooldownTime = 0.2f; 
    public bool tripleShot;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        arrowHitSource = GetComponent<AudioSource>();
        arrowShotSource = GetComponent<AudioSource>();
        crateBreakSource = GetComponent<AudioSource>();
        defaultCooldownTime = cooldownTime;
        defaultProjectile = projectile;
        shootCooldown = 0;
        initialSpeed = speed;
        sprintSpeed = speed * 1.5f;
        animator = GetComponent<Animator>();
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
            animator.SetTrigger("ShootArrow");
            shootCooldown = cooldownTime;
            arrowShotSource.Stop();
            arrowShotSource.PlayOneShot(arrowShot);
            if(!tripleShot){
                Instantiate(projectile,transform.position + Vector3.up, Quaternion.identity);
            }
            else{
                Instantiate(projectile,transform.position + Vector3.up + Vector3.left, Quaternion.identity);
                Instantiate(projectile,transform.position + Vector3.up, Quaternion.identity);
                Instantiate(projectile,transform.position + Vector3.up + Vector3.right, Quaternion.identity);
            }
        }
    }

    public void ActivateProjectilePowerUp(){
        projectile = powerUpProjectile;
        Invoke("ResetProjectile",15);
    }

    private void ResetProjectile(){
        projectile = defaultProjectile;
        GameManager.Instance.powerUpProjectileState = false;
        GameManager.Instance.activePowerUp = false;
    }

    public void ActivateTripleShot(){
        tripleShot = true;
        GameManager.Instance.activePowerUp = true;
        Invoke("ResetTripleShot",15);
    }

    private void ResetTripleShot(){
        tripleShot = false;
        GameManager.Instance.activePowerUp = false;
    }

    public void ActivateDoubleSpeedShooting(){
        cooldownTime = shootCooldown/2.5f;
        Invoke("ResetProjectileSpeed",15);
    }

    private void ResetProjectileSpeed(){
        cooldownTime = defaultCooldownTime;
        GameManager.Instance.activePowerUp = false;
    }
}
