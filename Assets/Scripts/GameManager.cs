using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //PowerUp variables
    public GameObject powerUpBox;
    public GameObject[] powerUpScreens;
    public bool activePowerUp;
    public bool powerUpProjectileState;
    public bool powerUpProjectilePiercing;

    //UI related variables
    public GameObject gameOverScreen;
    public bool gameOver;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI currentScoreText;
    private int currentScorevalue;
    public Slider wallHealth;
    private bool boostedSpeed;

    //Projectile Stats
    public float projectileSpeed;
    public int projectileDamage;
    public int projectileSize;
    public float spawnCooldown;

    //Basic Barbarian stats
    public float basicBarbSpeed;
    public int basicBarbHP;
    //Big Barbarian stats
    public float bigBarbSpeed;
    public int bigBarbHP;
    //Archer stats
    public float archerSpeed;
    public int archerHP;
    public float ArcherProjectileSpeed;

    //Big Archer
    public float bigarcherSpeed;
    public int bigarcherHP;
    //Archers projectile damage
    public float archerProjectileDamage;

    
    //Variables to increase difficulty
    public List<GameObject> enemies = new List<GameObject>();
    private float timeElapsed;
    private bool boostedSpeed_final;

    private void Awake() {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        UpdateDisplay();
        gameOverScreen.SetActive(false);
        Application.targetFrameRate = 60;
        StartCoroutine("SpawnEnemies",spawnCooldown);
    }

    void Update(){
        //Difficulty based on score
        // switch (currentScorevalue)
        // {
        //     case 0:
        //     break;
            
        // }

        timeElapsed += Time.deltaTime;

        if(wallHealth.value <= 0 && !gameOver){
            SetGameOver();
        }
        else if(gameOver && Input.anyKeyDown){
            RestartGame();
        }
    }

    private IEnumerator SpawnEnemies(){
        while(!gameOver){
            Vector2 randomPos = new Vector2(Random.Range(-6f, 6f), 5.25f);
            float randomNum = Random.Range(0f, 1f);

            float timeElapsed = Time.timeSinceLevelLoad;
            float[] probabilities;
            
            if (timeElapsed > 10 && timeElapsed < 20)
            {
                probabilities = new float[] { 0.65f, 0.2f, 0.1f, 0.1f }; // Probabilities for enemies
                if(spawnCooldown >=  2.75f){
                    spawnCooldown = 2.75f;
                }
            }
            else if(timeElapsed > 20 && timeElapsed < 30){
                probabilities = new float[] { 0.6f, 0.25f, 0.15f, 0.15f }; // Probabilities for enemies
                if(spawnCooldown >=  2.5f){
                    spawnCooldown = 2.5f;
                }
            }
            else if(timeElapsed > 30 && timeElapsed < 40){
                probabilities = new float[] { 0.6f, 0.25f, 0.18f, 0.18f }; // Probabilities for enemies
                if(spawnCooldown >=  2.25f){
                    spawnCooldown = 2.25f;
                }
            }
            else if(timeElapsed > 50 && timeElapsed < 60){
                probabilities = new float[] { 0.5f, 0.3f, 0.2f, 0.2f }; // Probabilities for enemies
                if(spawnCooldown >= 2f){
                    spawnCooldown = 2f;
                }
            }
            else if(timeElapsed > 60 && timeElapsed < 70){
                probabilities = new float[] { 0.4f, 0.4f, 0.3f, 0.3f }; // Probabilities for enemies
                if(!boostedSpeed){
                    boostedSpeed = true;
                    basicBarbSpeed = basicBarbSpeed*1.15f;
                    bigBarbSpeed = bigBarbSpeed*1.15f;
                    archerSpeed = archerSpeed*1.15f;
                    bigarcherSpeed = bigarcherSpeed*1.15f;
                    ArcherProjectileSpeed = ArcherProjectileSpeed *1.15f;
                }
                if(spawnCooldown >= 1.75f){
                    spawnCooldown = 1.75f;
                }
            }
            else if(timeElapsed > 70 && timeElapsed < 80){
                probabilities = new float[] { 0.4f, 0.4f, 0.5f, 0.5f }; // Probabilities for enemies
                if(spawnCooldown >= 1.5f){
                    spawnCooldown = 1.5f;
                }
            }
            else if(timeElapsed > 80 && timeElapsed < 90){
                probabilities = new float[] { 0.3f, 0.3f, 0.6f, 0.6f }; // Probabilities for enemies
                if(spawnCooldown >= 1.25f){
                    spawnCooldown = 1.25f;
                }
            }
            else if(timeElapsed > 90 && timeElapsed < 100){
                probabilities = new float[] { 0.3f, 0.3f, 0.6f, 0.6f }; // Probabilities for enemies
                if(spawnCooldown >= 1.25f){
                    spawnCooldown = 1.25f;
                }
                if(!boostedSpeed_final){
                    boostedSpeed = true;
                    basicBarbSpeed = basicBarbSpeed*1.15f;
                    bigBarbSpeed = bigBarbSpeed*1.15f;
                    archerSpeed = archerSpeed*1.15f;
                    bigarcherSpeed = bigarcherSpeed*1.15f;
                    ArcherProjectileSpeed = ArcherProjectileSpeed *1.15f;
                }
            }
            else if(timeElapsed > 100 && timeElapsed < 110){
                probabilities = new float[] { 0.2f, 0.2f, 0.7f, 0.7f }; // Probabilities for enemies
                if(spawnCooldown >= 1f){
                    spawnCooldown = 1f;
                }
                if(!boostedSpeed_final){
                    boostedSpeed_final = true;
                    basicBarbSpeed = basicBarbSpeed*1.15f;
                    bigBarbSpeed = bigBarbSpeed*1.15f;
                    archerSpeed = archerSpeed*1.15f;
                    bigarcherSpeed = bigarcherSpeed*1.15f;
                    ArcherProjectileSpeed = ArcherProjectileSpeed *1.15f;
                }
            }

            
            else
            {
                probabilities = new float[] { 0.7f, 0.1f, 0.05f, 0.025f }; // Higher probability for enemies[0] early on
            }

            // Use the probabilities to select which enemy to spawn
            int selectedEnemy = GetRandomEnemyIndex(probabilities);

            Instantiate(enemies[selectedEnemy], randomPos, Quaternion.identity);

            yield return new WaitForSecondsRealtime(spawnCooldown);
        }
    }

    private int GetRandomEnemyIndex(float[] probabilities){
        float total = 0;
        foreach (float prob in probabilities)
        {
            total += prob;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probabilities.Length; i++)
        {
            if (randomPoint < probabilities[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probabilities[i];
            }
        }
        return probabilities.Length - 1; // Shouldn't reach here, but just in case
    }

    public void ReceiveDamage(float damage){
        if(wallHealth.value - damage < 0){
            wallHealth.value = 0;
        }
        else{
            wallHealth.value -= damage;
        }
    }

    private void SetGameOver(){
        Time.timeScale = 0;
        gameOver = true;
        gameOverScreen.SetActive(true);
        CheckHighScore();
        DeactivatePowerUpScreen();
    }

    private void RestartGame(){
        SceneManager.LoadScene("GameScene");
    }

    public void AddScore(int score){
        currentScorevalue += score;
        currentScoreText.text = currentScorevalue.ToString("00000");
        CheckHighScore();
    }

    private void CheckHighScore(){
        if(currentScorevalue > PlayerPrefs.GetInt("HighScore")){
            PlayerPrefs.SetInt("HighScore",currentScorevalue);
            highScoreText.color = Color.green;
            highScoreText.text = currentScorevalue.ToString("00000");
        }
    }

    private void UpdateDisplay(){
        currentScorevalue = 0;
        currentScoreText.text = currentScorevalue.ToString("00000");
        if(PlayerPrefs.HasKey("HighScore")){
            highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString("00000");
        }
    }

    public void ActivateProjectilePiercing(){
        powerUpProjectilePiercing = true;

        Invoke("ResetProjectilePiercing",15);
    }

    private void ResetProjectilePiercing(){
        powerUpProjectilePiercing = false;
        activePowerUp = false;
    }

    public void ShowActivatedPowerUp(int powerUp){
        switch(powerUp){
            case 0:
                powerUpScreens[powerUp].SetActive(true);
                Invoke("DeactivatePowerUpScreen",3);
            break;
            case 1:
                ActivatePowerUpScreen(powerUpScreens[powerUp]);
            break;
            case 2:
                ActivatePowerUpScreen(powerUpScreens[powerUp]);
            break;
            case 3:
                ActivatePowerUpScreen(powerUpScreens[powerUp]);
            break;
            case 4:
                ActivatePowerUpScreen(powerUpScreens[powerUp]);
            break;
        }
    }

    private void ActivatePowerUpScreen(GameObject screen){
        screen.SetActive(true);
        Invoke("DeactivatePowerUpScreen",14);
    }

    private void DeactivatePowerUpScreen(){
        foreach (GameObject screen in powerUpScreens){   
            screen.SetActive(false);
        }
    }

    
}
