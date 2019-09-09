using System;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private List<ThrowableObject> balls;
    private Vector3 position;
    private float width;

    // mobile input variables
    private Vector3 _firstTouchPos;
    private Vector3 _lastTouchPos;
    // minimum distance for a swipe to be registered
    [SerializeField]
    private float _minSwipeDistanceInPercentage = 0.10f;
    private float _minSwipeDistance;

    private float _swipeTimerLeft;
    private float _swipeTimerRight;
    private bool _hasSwipedLeft;
    private bool _hasSwipedRight;

    [SerializeField]
    private float _coyoteTime = 0.2f;

    //Disable control variables
    private bool disableControls = false;
    private float disableControlsTimer = 0; //timer for counting how long the controls have been turned off
    private static float disableControlsTime = 3f; //how long should the controls be turned off


    private GameControl gc;

    private FountainGameController _fountainGameController;


    // Start is called before the first frame update
    void Start()
    {
        width = (float)Screen.width;
        _minSwipeDistance = Screen.height * _minSwipeDistanceInPercentage; // 10% height of the screen

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
        if (Input.touchCount == 1)
        {
            // get the touch
            Touch touch = Input.GetTouch(0);
            // check for the first touch
            if (touch.phase == TouchPhase.Began)
            {
                _firstTouchPos = touch.position;
                _lastTouchPos = touch.position;
            }
            // update the last position based on where it moved
            else if (touch.phase == TouchPhase.Moved)
            {
                _lastTouchPos = touch.position;
            }
            // check if the finger is removed from the screen
            else if (touch.phase == TouchPhase.Ended)
            {
                // last touch position
                _lastTouchPos = touch.position;

                // difference vector
                var differenceVec = _lastTouchPos - _firstTouchPos;

                // check if swipe distance is greater than 15% of the screen height
                if (!(Math.Abs(differenceVec.x) > _minSwipeDistance) &&
                    !(Math.Abs(differenceVec.y) > _minSwipeDistance)) return;

                // check if the swipe is vertical
                if (!(Mathf.Abs(differenceVec.x) <= Mathf.Abs(differenceVec.y))) return;

                // check if we have swiped up
                if (!(_lastTouchPos.y > _firstTouchPos.y)) return;

                // swiped on left side of the screen
                if (_lastTouchPos.x < width / 2)
                {
                    ThrowLeft();
                    _swipeTimerLeft = 0;
                    _hasSwipedLeft = true;
                }
                // swiped on right side of the screen
                else
                {
                    ThrowRight();
                    _swipeTimerRight = 0;
                    _hasSwipedRight = true;
                }
            }
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

            Debug.Log(to.gameObject.GetInstanceID() + "Size = " + gc.rightHandObjects.Count);
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

            Debug.Log(to.gameObject.GetInstanceID() + "Size = " + gc.leftHandObjects.Count);
        }
    }

    public void DisableControls()
    {
        disableControls = true;
        disableControlsTimer = 0;
    }
}
