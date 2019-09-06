using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    private int catchCounter;
    [SerializeField]
    private int currentLevel=1;
    public int currentThrowCount=0;
    [SerializeField]
    private int numOfBalls = 1;
    private int currentNumOfBalls = 0;
    [SerializeField]
    private float ballSpawnInterval = 1.0f;
    private float spawnTimer = 0f;
    [HideInInspector]
    public bool ballWaiting = false;
    private GameObject waitingBall;
    private AddBall addBall;

    [SerializeField]
    private int toLevel2Count = 3;
    [SerializeField]
    private int toLevel3Count = 13;
    [SerializeField]
    private int toLevel4Count = 25;
    [SerializeField]
    private int toLevel5Count = 45;

    // Start is called before the first frame update
    void Start()
    {
        addBall = GetComponent<AddBall>();
        Time.timeScale = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentThrowCount > toLevel2Count && currentLevel==1)
        {
            currentLevel=2;
        }
        if (currentThrowCount > toLevel3Count && currentLevel == 2)
        {
            currentLevel = 3;
            numOfBalls++;
        }
        if (currentThrowCount > toLevel4Count && currentLevel == 3)
        {
            currentLevel = 4;
            numOfBalls++;
        }

        if (currentThrowCount > toLevel5Count && currentLevel == 4)
        {
            SceneManager.LoadScene(1);
        }

        if(numOfBalls>currentNumOfBalls && 
           spawnTimer>ballSpawnInterval)
        {
            if (!ballWaiting)
            {
                spawnTimer = 0;
                waitingBall = addBall.SpawnBall();
                waitingBall.GetComponent<addForce>().Wait();
                currentNumOfBalls++;
            }
            else
            {
                spawnTimer = 0;
            }
            
        }
        spawnTimer += Time.deltaTime;


    }

    public void AddBall()
    {
        addBall.SpawnBall();
    }

    public int getCurrentLevel()
    {
        return currentLevel;
    }

    public void removeBall()
    {
        currentNumOfBalls--;
    }

}
