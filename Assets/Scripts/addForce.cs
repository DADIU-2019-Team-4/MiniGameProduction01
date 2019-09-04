using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
            SceneManager.LoadScene("Prototype3");
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
    public int throwRight()
    {
        if (isLeftHand)
        {
            Debug.Log("Tap!");

            rb.useGravity = true;
            rb.AddForce(new Vector3(-0.2F, 1, 0) * thrust);
            gc.throwCount++;

            isLeftHand = false;

            return 1;
        }
        else
        {
            Debug.Log("Not tapable");
            return 0;
        }
    }
    public int throwLeft()
    {
        if (gc.getCurrentLevel() == 1) //on level 1 just throw upwards
        {
            rb.AddForce(new Vector3(0, 0.8F, 0) * thrust);
            gc.throwCount++;
            return 1;
        }
        else if (isRightHand)
        {
            Debug.Log("Tap!");

            rb.useGravity = true;
            rb.AddForce(new Vector3(0.35F, 0.6F, 0) * thrust);
            gc.throwCount++;

            isRightHand = false;
            return 1;
        }
        else
        {
            Debug.Log("Not tapable");
            return 0;
        }
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
    }
}
