using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private bool _isInLeftHand;
    [SerializeField]
    private bool _isInRightHand;

    private Animator _animator;

    private float _screenWidth;

    // mobile input variables
    private Vector3 _firstTouchPos;
    private Vector3 _lastTouchPos;
    // minimum distance for a swipe to be registered
    [SerializeField]
    private float _minSwipeDistanceInPercentage = 0.15f;
    private float _minSwipeDistance;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _minSwipeDistance = Screen.height * _minSwipeDistanceInPercentage; // 15% height of the screen
        _screenWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        PcInput();
#elif UNITY_ANDROID || UNITY_IOS
        MobileInput();
#endif
    }

    /// <summary>
    /// Read PC input and act accordingly
    /// </summary>
    private void PcInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x < _screenWidth / 2)
            {
                if (_isInLeftHand)
                    StartCoroutine(JuggleLeft());
                Debug.Log("left side");
            }
            else
            {
                if (_isInRightHand)
                    StartCoroutine(JuggleRight());
                Debug.Log("right side");
            }
        }
    }

    /// <summary>
    /// Reads mobile input and act accordingly
    /// </summary>
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
                if (_lastTouchPos.x < _screenWidth / 2)
                {
                    if (_isInLeftHand)
                        StartCoroutine(JuggleLeft());
                }
                // swiped on right side of the screen
                else
                {
                    if (_isInRightHand)
                        StartCoroutine(JuggleRight());
                }
            }
        }
    }

    /// <summary>
    /// Setting up animator settings for juggling with the right hand
    /// </summary>
    private IEnumerator JuggleRight()
    {
        _animator.SetBool("isInLeftHand", false);
        _animator.SetBool("isInRightHand", true);
        _animator.SetBool("swipedRightSide", true);

        yield return new WaitForSeconds(1);

        _animator.SetBool("isInLeftHand", true);
        _animator.SetBool("isInRightHand", false);
        _animator.SetBool("swipedRightSide", false);
        _animator.SetBool("FirstTime", false);

        _isInLeftHand = true;
        _isInRightHand = false;
    }

    /// <summary>
    /// Setting up animator settings for juggling with the left hand
    /// </summary>
    private IEnumerator JuggleLeft()
    {
        _animator.SetBool("isInLeftHand", true);
        _animator.SetBool("isInRightHand", false);
        _animator.SetBool("swipedLeftSide", true);

        yield return new WaitForSeconds(1);

        _animator.SetBool("isInLeftHand", false);
        _animator.SetBool("isInRightHand", true);
        _animator.SetBool("swipedLeftSide", false);
        _animator.SetBool("FirstTime", false);

        _isInLeftHand = false;
        _isInRightHand = true;
    }
}
