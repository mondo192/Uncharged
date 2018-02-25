using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public PlayerHP playerHP;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public static int EnemyLimit = 10;
    public static int currEnemies = 0;
    public bool isSpider = false;
    int stop = 1;
    // Use this for initialization
    void Start ()
    {
        //Calls spawn function, amount of time to wait before doing it, amount of time to wait before repeating it
        //So will always wait 2 seconds initially, and then every 3 seconds to spawn enemies
        InvokeRepeating("Spawn", 0, spawnTime);
    }
	
	//Called from enemy
	void Spawn ()
    {
        //Don't want to spawn enemies if player is dead
         if(playerHP.currentHealth <= 0f)
         {
             return;
         }

        if(currEnemies >= EnemyLimit)
        {
            Debug.Log("Enemy tried to spawn, but no luck as max reached." + "   enemieL = " + EnemyLimit);
        }
        else
        {
            //will pick any spawn location from the array randomly
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            //Creates enemy
            if(isSpider)
            {
                if(ScoreManager.score % 100 == 0 && ScoreManager.score != 0 && stop == 1)
                {
                    stop = 0;
                    Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                    currEnemies++;
                    Debug.Log("stop = " + stop);
                }

                if (ScoreManager.score % 100 != 0)
                {
                    stop = 1;
                }
            }

            else
            {
                Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                currEnemies++;
            }
        }
        
    }
}
