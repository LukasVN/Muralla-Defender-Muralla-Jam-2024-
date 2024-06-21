using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBox : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.activePowerUp = true;
        Invoke("Despawn",3f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(DefenderScript.Instance.tripleShot){
                return;
            }
            GameManager.Instance.activePowerUp = true;
            int randomPowerUp = Random.Range(0,5);
            switch (randomPowerUp)
            {
                case 0:
                    Debug.Log("+ WALL HEALTH");
                    GameManager.Instance.wallHealth.value += 0.15f;
                    GameManager.Instance.activePowerUp = false;
                    DefenderScript.Instance.crateBreakSource.Stop();
                    DefenderScript.Instance.crateBreakSource.PlayOneShot(DefenderScript.Instance.breakingCrate);
                    Destroy(gameObject);
                break;
                case 1:
                    Debug.Log("X2 Damage");
                    DefenderScript.Instance.ActivateProjectilePowerUp();
                    GameManager.Instance.powerUpProjectileState = true;
                    GameManager.Instance.ShowActivatedPowerUp(randomPowerUp);
                    DefenderScript.Instance.crateBreakSource.Stop();
                    DefenderScript.Instance.crateBreakSource.PlayOneShot(DefenderScript.Instance.breakingCrate);
                    Destroy(gameObject);
                break;
                case 2:
                    Debug.Log("Piercing Damage");
                    GameManager.Instance.ActivateProjectilePiercing();
                    GameManager.Instance.ShowActivatedPowerUp(randomPowerUp);
                    DefenderScript.Instance.crateBreakSource.Stop();
                    DefenderScript.Instance.crateBreakSource.PlayOneShot(DefenderScript.Instance.breakingCrate);
                    Destroy(gameObject);
                break;
                case 3:
                    Debug.Log("x2 Shoot Speed");
                    DefenderScript.Instance.ActivateDoubleSpeedShooting();
                    GameManager.Instance.ShowActivatedPowerUp(randomPowerUp);
                    DefenderScript.Instance.crateBreakSource.Stop();
                    DefenderScript.Instance.crateBreakSource.PlayOneShot(DefenderScript.Instance.breakingCrate);
                    Destroy(gameObject);
                break;
                case 4:
                    Debug.Log("Triple shot");
                    DefenderScript.Instance.ActivateTripleShot();
                    GameManager.Instance.ShowActivatedPowerUp(randomPowerUp);
                    DefenderScript.Instance.crateBreakSource.Stop();
                    DefenderScript.Instance.crateBreakSource.PlayOneShot(DefenderScript.Instance.breakingCrate);
                    Destroy(gameObject);
                break;
                
            }
        }
    }

    private void Despawn(){
        GameManager.Instance.activePowerUp = false;
        Destroy(gameObject);
    }

}
