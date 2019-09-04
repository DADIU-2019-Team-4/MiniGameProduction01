using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private bool _isInLeftHand;
    [SerializeField]
    private bool _isInRightHand;

    private Animator _animator;

    // mobile input variables
    private Vector3 _firstTouchPos;
    private Vector3 _lastTouchPos;
    // minimum distance for a swipe to be registered
    private float _minSwipeDistance;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _minSwipeDistance = Screen.height * 0.15f; //dragDistance is 15% height of the screen
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

    private void PcInput()
    {
        if (Input.GetMouseButtonDown(0))
            if (_isInLeftHand)
                StartCoroutine(JuggleLeft());

        if (Input.GetMouseButtonDown(1))
            if (_isInRightHand)
                StartCoroutine(JuggleRight());
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
                if (Math.Abs(differenceVec.x) > _minSwipeDistance || Math.Abs(differenceVec.y) > _minSwipeDistance)
                {

                    // check if the swipe is vertical or horizontal
                    if (Mathf.Abs(differenceVec.x) <= Mathf.Abs(differenceVec.y))
                    {
                        // swipe up
                        if (_lastTouchPos.y > _firstTouchPos.y)
                        {
                            if (_isInLeftHand)
                                StartCoroutine(JuggleLeft());

                            if (_isInRightHand)
                                StartCoroutine(JuggleRight());
                        }
                    }
                }
            }
        }
    }

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
