using System.Collections;
using UnityEngine;

public class addForce : MonoBehaviour
{
    private Animator _animator;
    private float thrust = 22F;
    private Rigidbody rb;
    private bool isRightHand = false;
    private bool isLeftHand = false;
    private BoxCollider leftHandCollider;
    private BoxCollider rightHandCollider;
    private GameControl gc;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        leftHandCollider = GameObject.FindGameObjectsWithTag("LeftHand")[0].GetComponent<BoxCollider>();
        rightHandCollider = GameObject.FindGameObjectsWithTag("RightHand")[0].GetComponent<BoxCollider>();
        gc = GameObject.FindObjectOfType<GameControl>();
        Debug.Log(gc.throwCount);
        Debug.Log(rb);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Ball")
        {
            //SceneManager.LoadScene("Prototype3");
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.tag);
        Debug.Log(collider.ToString());

        if (collider.tag == "RightHand")
        {
            isRightHand = true;
            rb.position = new Vector3(-1.46F, 0, 0);
        }
        else if (collider.tag == "LeftHand")
        {
            isLeftHand = true;
            rb.position = new Vector3(1.46F, 0, 0);
        }
        else
        {
            Debug.Log("What is this??");
        }
        
        rb.velocity = new Vector3(0, 0, 0);
        //rb.useGravity = false;
    }

    private void OnTriggerExit(Collider collider)
    {
        Debug.Log("OnTriggerExit");
        isRightHand = false;
        isLeftHand = false;
    }
    public void throwRight()
    {
        if (isLeftHand)
        {
            //applyForce(new Vector3(-0.2F, 1, 0));
            StartCoroutine(JuggleRight());
            isLeftHand = false;
        }
        else
        {
            Debug.Log("Not tapable");
        }
    }
    public void throwLeft()
    {
        if (gc.getCurrentLevel() == 1 && isRightHand) //on level 1 just throw upwards
        {
            applyForce(new Vector3(0, 0.8F, 0));
            isRightHand = false;
        }
        else if (isRightHand)
        {
            //applyForce(new Vector3(0.35F, 0.6F, 0));           
            StartCoroutine(JuggleLeft());
            isRightHand = false;
        }
        else
        {
            Debug.Log("Not tapable");
        }
    }

    /// <summary>
    /// Setting up animator settings for juggling with the right hand
    /// </summary>
    private IEnumerator JuggleRight()
    {
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

    /// <summary>
    /// Setting up animator settings for juggling with the left hand
    /// </summary>
    private IEnumerator JuggleLeft()
    {
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

    private void Update()
    {
        if(transform.position.y<-3)
        {
            gc.removeBall();
            Destroy(transform.gameObject);
        }
    }
}
