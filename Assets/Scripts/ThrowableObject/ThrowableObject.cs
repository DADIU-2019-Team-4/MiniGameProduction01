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
    private void OnCollisionEnter(Collision collision)
    {
        //restart if balls hit eachother
        if (collision.gameObject.tag == "Ball")
        {
            if (collision.gameObject.GetComponent<Rigidbody>().isKinematic)
            {
                Destroy(transform.gameObject);
                gc.removeBall();
            }

            //SceneManager.LoadScene("Prototype3");
        }
    }

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "RightHand")
        {
            isRightHand = true;
            rb.position = new Vector3(-1.46F, 0, 0);
            rb.isKinematic = true;
        }
        else if (collider.tag == "LeftHand")
        {
            isLeftHand = true;
            rb.position = new Vector3(1.46F, 0, 0);
            rb.isKinematic = true;
        }
        else
        {
            //some other trigger
        }

        rb.velocity = new Vector3(0, 0, 0);
    }

    private void OnTriggerExit(Collider collider)
    {
        isRightHand = false;
        isLeftHand = false;
    }

    public bool throwRight()
    {
        if (isLeftHand)
        {
            //applyForce(new Vector3(-0.2F, 1, 0));
            rb.isKinematic = false;
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
            rb.isKinematic = false;
            if (gc.getCurrentLevel() == 1) //on level 1 just throw upwards
            {
                applyForce(new Vector3(0, 0.8F, 0));
            }
            else
            {
                //applyForce(new Vector3(0.35F, 0.6F, 0));           
                StartCoroutine(JuggleLeft());
            }

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

        _animator.enabled = true;
        _animator.SetBool("isInLeftHand", false);
        _animator.SetBool("isInRightHand", true);
        _animator.SetBool("swipedRightSide", true);

        yield return new WaitForSeconds(1.05f);

        _animator.SetBool("isInLeftHand", true);
        _animator.SetBool("isInRightHand", false);
        _animator.SetBool("swipedRightSide", false);
        _animator.enabled = false;

        gc.throwCount++;
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

        gc.throwCount++;
    }

    public void applyForce(Vector3 dir)
    {
        Debug.Log("Tap!");
        rb.velocity = new Vector3(0, 0, 0);
        rb.AddForce(dir * thrust);
        gc.throwCount++;
    }

    public void Wait()
    {
        gc.ballWaiting = isWaiting = true;
        rb.position = new Vector3(-2.6F, 0, 0);
        rb.useGravity = false;
    }

    public void Begin()
    {
        gc.ballWaiting = isWaiting = false;
        rb.position = new Vector3(-1.46F, 0, 0);
        rb.useGravity = true;
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
