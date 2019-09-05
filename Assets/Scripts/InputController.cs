using System;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private List<addForce> balls;
    private Vector3 position;
    private float width;

    // mobile input variables
    private Vector3 _firstTouchPos;
    private Vector3 _lastTouchPos;
    // minimum distance for a swipe to be registered
    private float _minSwipeDistance;

    private float _swipeTimerLeft;
    private float _swipeTimerRight;
    private bool _hasSwipedLeft;
    private bool _hasSwipedRight;

    [SerializeField]
    private float _stopTimerValue = 0.2f;

    [SerializeField]
    private bool _isFountain;

    private FountainGameController _fountainGameController;


    // Start is called before the first frame update
    void Start()
    {
        width = (float)Screen.width;
        _minSwipeDistance = Screen.height * 0.10f; // 10% height of the screen

        if (_isFountain)
            _fountainGameController = FindObjectOfType<FountainGameController>();
    }

    // Update is called once per frame
    void Update()
    {
        balls = new List<addForce>(FindObjectsOfType<addForce>());
        //Debug.Log(balls.Capacity);

        if (_hasSwipedLeft)
        {
            ThrowLeft();
            _swipeTimerLeft += Time.deltaTime;
            if (_swipeTimerLeft > _stopTimerValue)
                _hasSwipedLeft = false;
        }

        if (_hasSwipedRight)
        {
            ThrowRight();
            _swipeTimerRight += Time.deltaTime;
            if (_swipeTimerRight > _stopTimerValue)
                _hasSwipedRight = false;
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
        if (!_isFountain)
        {
            addForce waitingBall = null;

            foreach (addForce ball in balls)
            {
                if (ball.isWaiting)
                {
                    waitingBall = ball;

                }

                else if (ball.throwLeft())
                {
                    return;
                }

            }

            if (waitingBall != null)
            {
                waitingBall.Begin();
            }
        }
        else
        {
            _fountainGameController.JuggleLeft();
        }

    }

    private void ThrowRight()
    {
        if (!_isFountain)
        {
            foreach (addForce ball in balls)
                ball.throwRight();
        }
        else
        {
            _fountainGameController.JuggleRight();
        }
    }
}
