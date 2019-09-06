using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

    private int catchCounter;
    [SerializeField]
    public int currentLevel=1;
    public int currentThrowCount=0; // Total number of throws.
    [SerializeField]
    public int MaximumNumberOfBalls = 1;
    private int currentNumOfBalls = 0; // As it say on the label.
    [SerializeField]
    private float ballSpawnInterval = 1.0f;
    private float spawnTimer = 0f;
    [HideInInspector]
    public bool ballWaiting = false;
    private GameObject waitingBall;
    private AddBall addBall;
    private List<ThrowableObject> throwableObjectList;
    public Queue<ThrowableObject> leftHandObjects;
    public Queue<ThrowableObject> rightHandObjects;

    [SerializeField]
    public int toLevel2Count = 3;
    [SerializeField]
    public int toLevel3Count = 13;
    [SerializeField]
    public int toLevel4Count = 25;
    [SerializeField]
    public int toLevel5Count = 45;
    [SerializeField]
    public int toLevel6Count = 60;

    public float gameSpeed = 0.8f;

    [SerializeField]
    private GameObject _endGameObject;

    public List<GameObject> Balls = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _endGameObject.SetActive(false);
        leftHandObjects = new Queue<ThrowableObject>();
        rightHandObjects = new Queue<ThrowableObject>();
        addBall = GetComponent<AddBall>();
        Time.timeScale = gameSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // to test preloading scenes
        ToNextScene();

        if (currentThrowCount > toLevel2Count && currentLevel==1)
        {
            currentLevel=2;
        }
        if (currentThrowCount > toLevel3Count && currentLevel == 2)
        {
            currentLevel = 3;
            MaximumNumberOfBalls++;
        }
        if (currentThrowCount > toLevel4Count && currentLevel == 3)
        {
            currentLevel = 4;
            MaximumNumberOfBalls++;
        }

        if (currentThrowCount > toLevel5Count && currentLevel == 4)
        {
            currentLevel = 5;
            MaximumNumberOfBalls++;
        }

        if (currentThrowCount > toLevel6Count && currentLevel == 5)
        {
            _endGameObject.SetActive(true);
        }

        if (MaximumNumberOfBalls > currentNumOfBalls)
        {
             GameObject ball = addBall.SpawnBall();
            Balls.Add(ball);
             currentNumOfBalls++;
        }


    }

    private void ToNextScene()
    {
        if (Input.GetKeyDown(KeyCode.N))
            StartCoroutine(LoadYourAsyncScene());
    }

    private IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
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
