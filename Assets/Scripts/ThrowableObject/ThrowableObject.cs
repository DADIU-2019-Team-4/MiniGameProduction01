using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{

    private Animator _animator;
    private float thrust = 22F;
    private Rigidbody rb;
    private bool isRightHand = false;
    private bool isLeftHand = false;

    [HideInInspector]
    public GameControl gc;
    [HideInInspector]
    public bool isWaiting;

    public enum Type
    {
        Ball,
        Bell,
        Dad,
        Mom,
        Rocket
    }
    public Type type;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        gc = GameObject.FindObjectOfType<GameControl>();
    }


    private void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "RightHand")
        {
            isRightHand = true;
            gc.QueueRightHand(this);
            Debug.Log("Ball Caught," + this.gameObject.GetInstanceID() + "Size = " + gc.rightHandObjects.Count);
            rb.position = new Vector3(-1.46F, 0, 0);
            rb.useGravity = false;
        }
        else if (collider.tag == "LeftHand")
        {
            isLeftHand = true;
            gc.QueueLeftHand(this);
            Debug.Log("Ball Caught," + this.gameObject.GetInstanceID() + "Size = " + gc.leftHandObjects.Count);
            rb.position = new Vector3(1.46F, 0, 0);
            rb.useGravity = false;
        }
        else
        {
            //some other trigger
        }

        rb.velocity = new Vector3(0, 0, 0);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "RightHand" || collider.tag == "LeftHand")
        {
            isRightHand = false;
            isLeftHand = false;
            rb.useGravity = true;
        }
           
    }

    public bool throwRight()
    {
        if (isLeftHand)
        {
            //applyForce(new Vector3(-0.2F, 1, 0));
            rb.useGravity = true;
            StartCoroutine(JuggleRight());
            isLeftHand = false;

            return true;
        }
        else
        {
            Debug.Log("Not tapable");
            return false;
        }
    }
    public bool throwLeft()
    {
        if (isRightHand)
        {

            if (gc.getCurrentLevel() == 1) //on level 1 just throw upwards
            {
                //applyForce(new Vector3(0, 0.8F, 0));
            }
            else
            {
                //applyForce(new Vector3(0.35F, 0.6F, 0));           

            }

            StartCoroutine(JuggleLeft());
            rb.useGravity = true;
            isRightHand = false;

            return true;
        }

        else
        {
            Debug.Log("Not tapable");
            return false;
        }
    }


    private IEnumerator JuggleRight()
    {
        /// <summary>
        /// Setting up animator settings for juggling with the right hand
        /// </summary>


        Debug.Log("Animating");

        _animator.enabled = true;
        _animator.SetBool("isInLeftHand", false);
        _animator.SetBool("isInRightHand", true);
        _animator.SetBool("swipedRightSide", true);

        yield return new WaitForSeconds(1.05f);

        _animator.SetBool("isInLeftHand", true);
        _animator.SetBool("isInRightHand", false);
        _animator.SetBool("swipedRightSide", false);
        _animator.enabled = false;

        Debug.Log("Done");
        gc.currentLevelThrowCount++;
    }


    private IEnumerator JuggleLeft()
    {
        /// <summary>
        /// Setting up animator settings for juggling with the left hand
        /// </summary>


        _animator.enabled = true;
        _animator.SetBool("isInLeftHand", true);
        _animator.SetBool("isInRightHand", false);
        _animator.SetBool("swipedLeftSide", true);

        yield return new WaitForSeconds(1.05f);

        _animator.SetBool("isInLeftHand", false);
        _animator.SetBool("isInRightHand", true);
        _animator.SetBool("swipedLeftSide", false);
        _animator.enabled = false;

        gc.currentLevelThrowCount++;
    }

    public void applyForce(Vector3 dir)
    {
        Debug.Log("Tap!");
        rb.velocity = new Vector3(0, 0, 0);
        rb.AddForce(dir * thrust);
        gc.currentLevelThrowCount++;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
