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

    [SerializeField]
    private GameObject _endGameObject;

    public bool stackingIsAllowed = false;

    private InputController inputController;

    public List<GameObject> Balls = new List<GameObject>();

    internal void QueueLeftHand(ThrowableObject throwableObject)
    {

        if (!stackingIsAllowed)
        {
            if (leftHandObjects.Count > 0)
            {
                //restart level
                StartLevel(currentLevel);
                Debug.Log("Fail!!");
                return;
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
                //restart level
                StartLevel(currentLevel);
                Debug.Log("Fail!!");
                return;
            }
        }

        rightHandObjects.Enqueue(throwableObject);

    }   

    // Start is called before the first frame update
    void Start()
    {
        _endGameObject.SetActive(false);
        leftHandObjects = new Queue<ThrowableObject>();
        rightHandObjects = new Queue<ThrowableObject>();
        addBall = GetComponent<AddBall>();
        Time.timeScale = gameSpeed;

   //     if (currentLevel == 1)
        // NO SOUND IN PHASE 1   
		//  AkSoundEngine.SetSwitch("game_stage", "phase1", gameObject);
        inputController = FindObjectOfType<InputController>();
		//	AkSoundEngine.PostEvent("DialogueEN_event", gameObject);

        //level 1 setup
        StartLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
        // to test preloading scenes
        ToNextScene();

        if (currentLevelThrowCount > toLevel2Count && currentLevel == 1)
        {
            StartLevel(2);
            AkSoundEngine.SetSwitch("game_stage", "phase1", gameObject);
			AkSoundEngine.PostEvent("DialogueEN_event", gameObject);
        }
        if (currentLevelThrowCount > toLevel3Count && currentLevel == 2)
        {
            StartLevel(3);
            AkSoundEngine.SetSwitch("game_stage", "phase2", gameObject);
			AkSoundEngine.PostEvent("DialogueEN_event", gameObject);
        }
        if (currentLevelThrowCount > toLevel4Count && currentLevel == 3)
        {
            StartLevel(4);
            AkSoundEngine.SetSwitch("game_stage", "phase3", gameObject);
			AkSoundEngine.PostEvent("DialogueEN_event", gameObject);
        }

        if (currentLevelThrowCount > toLevel5Count && currentLevel == 4)
        {
            StartLevel(5);
            AkSoundEngine.SetSwitch("game_stage", "phase4", gameObject);
			AkSoundEngine.PostEvent("DialogueEN_event", gameObject);
        }

        if (currentLevelThrowCount > toLevel6Count && currentLevel == 5)
        {
            StartLevel(6);
            AkSoundEngine.SetSwitch("game_stage", "phase5", gameObject);
            AkSoundEngine.PostEvent("DialogueEN_event", gameObject);
        }

        if (currentLevelThrowCount > toLevel7Count && currentLevel == 6)
        {
            StartLevel(7);
            AkSoundEngine.SetSwitch("game_stage", "phase6", gameObject);
            AkSoundEngine.PostEvent("DialogueEN_event", gameObject);
            //_endGameObject.SetActive(true);
        }

        if (MaximumNumberOfBalls > currentNumOfBalls)
        {
            //GameObject ball = addBall.SpawnBall(Side.Left);
            //Balls.Add(ball);
            //currentNumOfBalls++;
        }

        CheckLooseCondition();

    }

    private void CheckLooseCondition()
    {
        
    
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

    public void AddBall(Side side)
    {
        addBall.SpawnBall(side);
        currentNumOfBalls++;
    }

    public void StartLevel(int levelNumber)
    {
        currentLevel = levelNumber;
        currentLevelThrowCount = 0;
        inputController.DisableControls();
        stackingIsAllowed = true;
        
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
                AddBall(Side.Left);
                break;
            case 2:
                AddBall(Side.Left);
                AddBall(Side.Right);
                break;
            case 3:
                AddBall(Side.Left);
                AddBall(Side.Left);
                AddBall(Side.Right);
                break;
            case 4:
                AddBall(Side.Left);
                AddBall(Side.Left);
                AddBall(Side.Right);
                AddBall(Side.Right);
                break;
            case 5:
                AddBall(Side.Left);
                AddBall(Side.Left);
                AddBall(Side.Right);
                AddBall(Side.Right);
                Time.timeScale = gameSpeed = 1;
                break;
            case 6:

                AddBall(Side.Left);
                AddBall(Side.Left);
                AddBall(Side.Left);
                AddBall(Side.Right);
                AddBall(Side.Right);
                
                break;
            case 7:

                break;
            default:
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