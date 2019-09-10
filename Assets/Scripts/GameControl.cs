using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

    //private int catchCounter;
    [SerializeField]
    public int currentLevel = 1;
    public int currentLevelThrowCount = 0; // Total number of throws.
    [SerializeField]
    public int MaximumNumberOfBalls = 1;
    private int currentNumOfBalls = 0; // As it say on the label.
    [SerializeField]
    private float ballSpawnInterval = 1.0f;
    private float spawnTimer = 0f;
    [HideInInspector]
    //public bool ballWaiting = false;
    //private GameObject waitingBall;
    private AddBall addBall;
    public List<ThrowableObject> throwableObjectList;
    public Queue<ThrowableObject> leftHandObjects;
    public Queue<ThrowableObject> rightHandObjects;


    [SerializeField]
    public int toLevel2Count = 3;
    [SerializeField]
    public int toLevel3Count = 5;
    [SerializeField]
    public int toLevel4Count = 10;
    [SerializeField]
    public int toLevel5Count = 10;
    [SerializeField]
    public int toLevel6Count = 10;
    [SerializeField]
    public int toLevel7Count = 10;
    

    public float gameSpeed = 0.8f;
    public float speedUpValue = 0.1f;

    [SerializeField]
    private GameObject _endGameObject;

    [SerializeField]
    private float levelTimer = 0;

    [SerializeField]
    private float bellSpawnTime = 36f;

    [SerializeField]
    private float packageSpawnTime = 0;

    [SerializeField]
    private float toySpawnTime = 0;

    [SerializeField]
    private float dvdSpawnTime = 0;



    public bool stackingIsAllowed = false;

    private InputController inputController;

    public List<GameObject> Balls = new List<GameObject>();

    private StarManager _starManager;

    Animator m_Animator;

    internal void QueueLeftHand(ThrowableObject throwableObject)
    {

        if (!stackingIsAllowed)
        {
            if (leftHandObjects.Count > 0)
            {
                // Trigger Timmy's animations for the failing
                m_Animator.SetTrigger("failC");
                m_Animator.SetTrigger("failL");
                m_Animator.SetTrigger("failR");

				AkSoundEngine.PostEvent("failFeed_event", gameObject);

                //restart level
                StartLevel(currentLevel);
                StartCoroutine(_starManager.ResetStars(0f));
                Debug.Log("Fail!!");

                if (currentLevel == 7)
                    _endGameObject.SetActive(true);
            }
        }

        leftHandObjects.Enqueue(throwableObject);
    }

    internal void QueueRightHand(ThrowableObject throwableObject)
    {

        if (!stackingIsAllowed)
        {
            if (rightHandObjects.Count > 0)
            {
                // Trigger Timmy's animations for the failing
                m_Animator.SetTrigger("failC");
                m_Animator.SetTrigger("failL");
                m_Animator.SetTrigger("failR");

				AkSoundEngine.PostEvent("failFeed_event", gameObject);

                //restart level
                StartLevel(currentLevel);
                StartCoroutine(_starManager.ResetStars(0f));
                Debug.Log("Fail!!");

                if (currentLevel == 7)
                    _endGameObject.SetActive(true);
            }
        }

        rightHandObjects.Enqueue(throwableObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get the Animator attached to the Timmy's Model
        m_Animator = GameObject.Find("Timmy_fbx").GetComponent< Animator > ();

        _endGameObject.SetActive(false);
        leftHandObjects = new Queue<ThrowableObject>();
        rightHandObjects = new Queue<ThrowableObject>();
        addBall = GetComponent<AddBall>();
        Time.timeScale = gameSpeed;
        _starManager = FindObjectOfType<StarManager>();

        inputController = FindObjectOfType<InputController>();
        inputController.ThrowEvent.AddListener(UpdateStars);

		AkSoundEngine.PostEvent("sun_event", gameObject);

        //level 1 setup
        StartLevel(currentLevel);
    }

    void PickUpBallScene() {
        m_Animator.SetTrigger("startC");
        m_Animator.SetTrigger("startL");
        m_Animator.SetTrigger("startR");
    }

    // Update is called once per frame
    void Update()
    {
        // to test preloading scenes
        ToNextScene();

        if (currentLevelThrowCount >= toLevel2Count && currentLevel == 1)
        {
            StartLevel(2);
            AkSoundEngine.SetSwitch("game_stage", "phase1", gameObject);
			AkSoundEngine.PostEvent("DialogueEN_event", gameObject);
        }
        if (currentLevelThrowCount >= toLevel3Count && currentLevel == 2)
        {
            StartLevel(3);
            AkSoundEngine.SetSwitch("game_stage", "phase2", gameObject);
            AkSoundEngine.PostEvent("DialogueEN_event", gameObject);
        }
        
        if (currentLevelThrowCount >= toLevel4Count && currentLevel == 3)
        {
            StartLevel(4);
            AkSoundEngine.SetSwitch("game_stage", "phase3", gameObject);
			AkSoundEngine.PostEvent("DialogueEN_event", gameObject);
			AkSoundEngine.PostEvent("crows_event", gameObject);
        }

        if (currentLevelThrowCount >= toLevel5Count && currentLevel == 4)
        {
            StartLevel(5);
            AkSoundEngine.SetSwitch("game_stage", "phase4", gameObject);
			AkSoundEngine.PostEvent("DialogueEN_event", gameObject);
        }

        if (currentLevelThrowCount >= toLevel6Count && currentLevel == 5)
        {
            StartLevel(6);
            AkSoundEngine.SetSwitch("game_stage", "phase5", gameObject);
            AkSoundEngine.PostEvent("DialogueEN_event", gameObject);
			AkSoundEngine.PostEvent("rain_event", gameObject);
        }
        

        if (currentLevelThrowCount >= toLevel7Count && currentLevel == 6)
        {
            StartLevel(7);
            AkSoundEngine.SetSwitch("game_stage", "phase6", gameObject);
            AkSoundEngine.PostEvent("DialogueEN_event", gameObject);
			AkSoundEngine.PostEvent("thunder_event", gameObject);
        }

        if (currentLevel == 7)
        {
            // speed up at level 7
            gameSpeed += Time.deltaTime * speedUpValue;
            Time.timeScale = gameSpeed;
        }

        if (MaximumNumberOfBalls > currentNumOfBalls)
        {
            //GameObject ball = addBall.SpawnBall(Side.Left);
            //Balls.Add(ball);
            //currentNumOfBalls++;
        }

        levelTimer += Time.deltaTime;
        TimedSpawnHandler();
    }

    private void UpdateStars()
    {
        switch (currentLevel)
        {
            case 1:
                _starManager.CalculatePercentage(currentLevelThrowCount, toLevel2Count);
                break;
            case 2:
                _starManager.CalculatePercentage(currentLevelThrowCount, toLevel3Count);
                break;
            case 3:
                _starManager.CalculatePercentage(currentLevelThrowCount, toLevel4Count);
                break;
            case 4:
                _starManager.CalculatePercentage(currentLevelThrowCount, toLevel5Count);
                break;
            case 5:
                _starManager.CalculatePercentage(currentLevelThrowCount, toLevel6Count);
                break;
            case 6:
                _starManager.CalculatePercentage(currentLevelThrowCount, toLevel7Count);
                break;
            default:
                Debug.Log("Level 7 started");
                _starManager.LevelEnd = true;
                break;
        }
    }

    private void TimedSpawnHandler()
    {    
        if (currentLevel==3 && levelTimer>bellSpawnTime)
        {
            throwableObjectList[0].gameObject.GetComponent<ModifyObjectMesh>().SetToBellMesh();
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

    public void AddBall(Side side, Type type)
    {
        addBall.SpawnBall(side, type);
        currentNumOfBalls++;
    }

    public void StartLevel(int levelNumber)
    {
        currentLevel = levelNumber;
        currentLevelThrowCount = 0;
        inputController.DisableControls(0f);
        stackingIsAllowed = true;
        levelTimer = 0; //reset time used on level
        
        foreach (ThrowableObject ball in throwableObjectList)
        {
            Destroy(ball.gameObject);
        }
        throwableObjectList.Clear();
        leftHandObjects.Clear();
        rightHandObjects.Clear();

        switch (levelNumber)
        {
            case 1:
                AddBall(Side.Left, Type.Ball);
                break;
            case 2:
                AddBall(Side.Left, Type.Ball);
                AddBall(Side.Right, Type.Ball1);
                break;
            case 3:
                AddBall(Side.Left, Type.Bell);
                AddBall(Side.Left, Type.Ball1);
                AddBall(Side.Right, Type.Ball2);
                break;
            case 4:
                AddBall(Side.Left, Type.Bell);
                AddBall(Side.Left, Type.Ball);
                AddBall(Side.Right, Type.Package);
                AddBall(Side.Right, Type.Rocket);
                break;
            case 5:
                AddBall(Side.Left, Type.Bell);
                AddBall(Side.Left, Type.Sex);
                AddBall(Side.Right, Type.Package);
                AddBall(Side.Right, Type.Rocket);
                gameSpeed =1f;
                Time.timeScale = gameSpeed;
                break;
            case 6:

                AddBall(Side.Left, Type.Car);
                AddBall(Side.Left, Type.Porcelain1);
                AddBall(Side.Left, Type.Porcelain2);
                AddBall(Side.Right, Type.Porcelain3);
                AddBall(Side.Right, Type.Porcelain1);
                break;
            case 7:
                if (_starManager.LevelEnd)
                    break;

                AddBall(Side.Left, Type.Car);
                AddBall(Side.Left, Type.Porcelain1);
                AddBall(Side.Left, Type.Porcelain2);
                AddBall(Side.Right, Type.Porcelain3);
                AddBall(Side.Right, Type.Porcelain1);

                break;
            default:

                // Trigger Timmy's animations for the new level
                m_Animator.SetTrigger("endGameC");
                m_Animator.SetTrigger("endGameL");
                m_Animator.SetTrigger("endGameR");

                break;
        }
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