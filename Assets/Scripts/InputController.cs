using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    private List<ThrowableObject> balls;
    private Vector3 position;
    private float width;

    // mobile input variables
    private Vector3 _firstTouchPos1;
    private Vector3 _lastTouchPos1;

    private Vector3 _firstTouchPos2;
    private Vector3 _lastTouchPos2;

    // minimum distance for a swipe to be registered
    [SerializeField]
    private float _minSwipeDistanceInPercentage = 0.10f;
    private float _minSwipeDistance;

    private float _swipeTimerLeft;
    private float _swipeTimerRight;
    private bool _hasSwipedLeft;
    private bool _hasSwipedRight;

    private bool _madeSwipe1;
    private bool _madeSwipe2;

    [SerializeField]
    private float _coyoteTime = 0.2f;

    //Disable control variables
    private bool disableControls = false;
    private float disableControlsTimer = 0; //timer for counting how long the controls have been turned off
    private static float disableControlsTime = 3f; //how long should the controls be turned off

    Animator m_Animator;

    private GameControl gc;

    private FountainGameController _fountainGameController;
    public UnityEvent ThrowEvent;


    // Start is called before the first frame update
    void Start()
    {
        width = (float)Screen.width;
        _minSwipeDistance = Screen.height * _minSwipeDistanceInPercentage; // 10% height of the screen

        //Get the Animator attached to the Timmy's Model
        m_Animator = GameObject.Find("Timmy_fbx").GetComponent<Animator>();

        //if (_isFountain)
        //    _fountainGameController = FindObjectOfType<FountainGameController>();

        gc = GameObject.FindObjectOfType<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        balls = new List<ThrowableObject>(FindObjectsOfType<ThrowableObject>());
        //Debug.Log(balls.Capacity);

        if (_hasSwipedLeft)
        {
            //ThrowLeft();
            _swipeTimerLeft += Time.deltaTime;
            if (_swipeTimerLeft > _coyoteTime)
                _hasSwipedLeft = false;
        }

        if (_hasSwipedRight)
        {
            //ThrowRight();
            _swipeTimerRight += Time.deltaTime;
            if (_swipeTimerRight > _coyoteTime)
                _hasSwipedRight = false;
        }

        if (disableControls)
        {
            if (disableControlsTimer > disableControlsTime)
            {
                disableControls = false;
            }
            else
            {
                disableControlsTimer += Time.deltaTime;
            }
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        PcInput();
#elif UNITY_ANDROID || UNITY_IOS
        MobileInput();
#endif
    }

    private void PcInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x < width / 2)
                ThrowLeft();
            else
                ThrowRight();
        }
    }

    private void MobileInput()
    {
        // if user is touching the screen with a single touch...
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (i == 0)
                    CheckSwipe1();
                else if (i == 1)
                    CheckSwipe2();
            }
        }
    }

    private void CheckSwipe1()
    {
        // get the touch
        Touch touch = Input.GetTouch(0);
        // check for the first touch
        if (touch.phase == TouchPhase.Began)
        {
            _firstTouchPos1 = touch.position;
            _lastTouchPos1 = touch.position;
        }
        // update the last position based on where it moved
        else if (touch.phase == TouchPhase.Moved)
        {
            // last touch position
            _lastTouchPos1 = touch.position;

            // difference vector
            var differenceVec = _lastTouchPos1 - _firstTouchPos1;

            // check if swipe distance is greater than 15% of the screen height
            if (!(Math.Abs(differenceVec.x) > _minSwipeDistance) &&
                !(Math.Abs(differenceVec.y) > _minSwipeDistance)) return;

            // check if the swipe is vertical
            if (!(Mathf.Abs(differenceVec.x) <= Mathf.Abs(differenceVec.y))) return;

            // check if we have swiped up
            if (!(_lastTouchPos1.y > _firstTouchPos1.y)) return;

            if (_madeSwipe1) return;

            // swiped on left side of the screen
            if (_lastTouchPos1.x < width / 2)
            {
                ThrowLeft();
                _swipeTimerLeft = 0;
                _hasSwipedLeft = true;
                _madeSwipe1 = true;
            }
            // swiped on right side of the screen
            else
            {
                ThrowRight();
                _swipeTimerRight = 0;
                _hasSwipedRight = true;
                _madeSwipe1 = true;
            }
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            _madeSwipe1 = false;
        }
    }

    private void CheckSwipe2()
    {
        // get the touch
        Touch touch = Input.GetTouch(1);
        // check for the first touch
        if (touch.phase == TouchPhase.Began)
        {
            _firstTouchPos2 = touch.position;
            _lastTouchPos2 = touch.position;
        }
        // update the last position based on where it moved
        else if (touch.phase == TouchPhase.Moved)
        {
            // last touch position
            _lastTouchPos2 = touch.position;

            // difference vector
            var differenceVec = _lastTouchPos2 - _firstTouchPos2;

            // check if swipe distance is greater than 15% of the screen height
            if (!(Math.Abs(differenceVec.x) > _minSwipeDistance) &&
                !(Math.Abs(differenceVec.y) > _minSwipeDistance)) return;

            // check if the swipe is vertical
            if (!(Mathf.Abs(differenceVec.x) <= Mathf.Abs(differenceVec.y))) return;

            // check if we have swiped up
            if (!(_lastTouchPos2.y > _firstTouchPos2.y)) return;

            if (_madeSwipe2) return;

            // swiped on left side of the screen
            if (_lastTouchPos2.x < width / 2)
            {
                ThrowLeft();
                _swipeTimerLeft = 0;
                _hasSwipedLeft = true;
                _madeSwipe2 = true;
            }
            // swiped on right side of the screen
            else
            {
                ThrowRight();
                _swipeTimerRight = 0;
                _hasSwipedRight = true;
                _madeSwipe2 = true;
            }
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            _madeSwipe2 = false;
        }
    }

    private void ThrowLeft()
    {
        ThrowableObject to = null;

        if (gc.rightHandObjects.Count > 0 )
        {
            to = gc.rightHandObjects.Dequeue();
            to.throwLeft();
            gc.stackingIsAllowed = false;

            // Trigger throwing Animation
            m_Animator.SetTrigger("throwR");

            Debug.Log(to.gameObject.GetInstanceID() + "Size = " + gc.rightHandObjects.Count);
            ThrowEvent.Invoke();
        }

    }

    private void ThrowRight()
    {
        ThrowableObject to = null;

        if (gc.leftHandObjects.Count > 0 )
        {
            to = gc.leftHandObjects.Dequeue();
            to.throwRight();
            gc.stackingIsAllowed = false;

            // Trigger throwing Animation
            m_Animator.SetTrigger("throwL");

            Debug.Log(to.gameObject.GetInstanceID() + "Size = " + gc.leftHandObjects.Count);
            ThrowEvent.Invoke();
        }
    }

    public void DisableControls()
    {
        disableControls = true;
        disableControlsTimer = 0;
    }
}
