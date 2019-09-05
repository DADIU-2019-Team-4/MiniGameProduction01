using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private int catchCounter;
    private int currentLevel=1;
    public int throwCount=0;
    private int numOfBalls = 1;
    private int currentNumOfBalls = 0;
    private float spawnInterval = 1.0f;
    private float spawnTimer = 0f;
    [HideInInspector]
    public bool ballWaiting = false;
    private GameObject waitingBall;
    private AddBall addBall;

    // Start is called before the first frame update
    void Start()
    {
        addBall = GetComponent<AddBall>();
        Time.timeScale = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (throwCount > 6 && currentLevel==1)
        {
            currentLevel=2;
        }
        if (throwCount > 13 && currentLevel == 2)
        {
            currentLevel = 3;
            numOfBalls++;
        }
        if (throwCount > 25 && currentLevel == 3)
        {
            currentLevel = 4;
            numOfBalls++;
        }

        if(numOfBalls>currentNumOfBalls && 
           spawnTimer>spawnInterval)
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
