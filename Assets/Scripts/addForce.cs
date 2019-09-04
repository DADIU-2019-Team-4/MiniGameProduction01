using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class addForce : MonoBehaviour
{
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
        leftHandCollider = GameObject.FindGameObjectsWithTag("LeftHand")[0].GetComponent<BoxCollider>();
        rightHandCollider = GameObject.FindGameObjectsWithTag("LeftHand")[0].GetComponent<BoxCollider>();
        gc = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameControl>();
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
            applyForce(new Vector3(-0.2F, 1, 0));
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
            applyForce(new Vector3(0.35F, 0.6F, 0));
            
            isRightHand = false;
        }
        else
        {
            Debug.Log("Not tapable");
        }
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
        if (Input.GetKeyDown(KeyCode.X))
        {
            throwRight();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            throwLeft();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SceneSetup");
        }

        if(transform.position.y<-3)
        {
            gc.removeBall();
            Destroy(transform.gameObject);
        }
    }
}
