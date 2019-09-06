using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private List<ThrowableObject> throwableObjectList;
    public Queue<ThrowableObject> leftHandObjects;
    public Queue<ThrowableObject> rightHandObjects;

    // Start is called before the first frame update
    void Start()
    {
        leftHandObjects = new Queue<ThrowableObject>();
        rightHandObjects = new Queue<ThrowableObject>();
        addBall = GetComponent<AddBall>();
        Time.timeScale = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (throwCount > 3 && currentLevel==1)
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

        if (throwCount > 40 && currentLevel == 4)
        {
            SceneManager.LoadScene(1);
        }

        if(numOfBalls>currentNumOfBalls)
        {
             addBall.SpawnBall();
             currentNumOfBalls++;
        }


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
