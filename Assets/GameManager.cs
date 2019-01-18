using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    public static GameManager instance = null;
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] GameObject tanker;
    [SerializeField] GameObject soldier;
    [SerializeField] GameObject ranger;
    [SerializeField] GameObject arrow;
    [SerializeField] Text levelText;
    [SerializeField] GameObject[] powerUpSpawns;
    [SerializeField] GameObject healthPowerup;
    [SerializeField] GameObject speedPowerup;
    [SerializeField] int maxPowerUps = 4;
    private GameObject newPowerUp;


    private bool gameOver = false;
    private int currentLevel;
    private float generateSpawnTime = 1;
    private float currentSpawnTime = 0;
    private GameObject newEnemy;
    private List<EnemyHealth> enemies = new List<EnemyHealth>();
    private List<EnemyHealth> killedEnemies = new List<EnemyHealth>();
    private float powerSpawnTime = 5;
    private float currentPowerUpSpawnTime = 0;
    private int powerups = 0;

    public void RegisterEnemy(EnemyHealth enemy)
    {
        enemies.Add(enemy);
    }



    public void RegisterPowerUp()
    {
        powerups++;
    }


    public void KilledEnemy(EnemyHealth enemy)
    {
        killedEnemies.Add(enemy);
    }
    public bool GameOver
    {
        get
        {
            return gameOver;
        }
    }


    public GameObject Arrow
    {
        get
        {
            return arrow;
        }
    }

    public GameObject Player
    {
        get
        {
            return player;
        }
    }

    // Start is called before the first frame update


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        StartCoroutine(spawn());
        StartCoroutine(powerupSpawn());
        currentLevel = 1;
        
        
    }

    // Update is called once per frame
    void Update()
    {


        currentPowerUpSpawnTime += Time.deltaTime;
        currentSpawnTime += Time.deltaTime;
        Debug.Log("current spawn time" + currentSpawnTime);
    }


    public void PlayerHit(int currentHP)
    {
        if(currentHP > 0)
        {
            gameOver = false;
        }
        else
        {
            gameOver = true;
        }
    }

    IEnumerator spawn()
    {
        // check that spawn time is greater than the current 
        // if less enemies on screen than the current level, randomly select a spawn point and spawn a random Enemy.


        Debug.Log("current spawn tiem " + currentSpawnTime);
        Debug.Log("gen spawn tiem" + generateSpawnTime);

        if(currentSpawnTime > generateSpawnTime)
        {


            Debug.Log("In the here of the last");
            currentSpawnTime = 0;
            if(enemies.Count < currentLevel)
            {
                Debug.Log("In hereza");
                
                int randomNumber = Random.Range(0, spawnPoints.Length-1);
                GameObject spawnPoint = spawnPoints[randomNumber];
                int randomNumber1 = Random.Range(0, 3);

                if(randomNumber1 == 0)
                {
                    newEnemy = Instantiate(soldier) as GameObject;
                }

                else if (randomNumber1 == 1)
                {
                    newEnemy = Instantiate(ranger) as GameObject;
                }


                else
                {
                    newEnemy = Instantiate(tanker) as GameObject;
                }

                newEnemy.transform.position = spawnPoint.transform.position;
                
            }


            else if(killedEnemies.Count == currentLevel)
            {
                enemies.Clear();
                killedEnemies.Clear();
                yield return new WaitForSeconds(3f);
                currentLevel++;
                levelText.text = "Level" + currentLevel; 
                
            }


            


        
        }





        //if we have killed the same number of enemies as the current level, clear out the enemies  and killed
        // enemies, arrays, increment the current level by 1, and start over.
        yield return null;
        StartCoroutine(spawn());
    }


    IEnumerator powerupSpawn()
    {
        if(currentPowerUpSpawnTime > powerSpawnTime)

        {
            currentPowerUpSpawnTime = 0;
            if(powerups < maxPowerUps)
            {
                int randomNumber = Random.Range(0, powerUpSpawns.Length - 1);
                GameObject spawnLocation = powerUpSpawns[randomNumber];
                int randomPowerUp = Random.Range(0, 2);
                if(randomPowerUp == 0)
                {
                    newPowerUp = Instantiate(healthPowerup) as GameObject;
                }

                else if(randomPowerUp == 1)
                {
                    newPowerUp = Instantiate(speedPowerup) as GameObject;
                }

                newPowerUp.transform.position = spawnLocation.transform.position;

            }

        }

        yield return null;
        StartCoroutine(powerupSpawn());
;  }
}
