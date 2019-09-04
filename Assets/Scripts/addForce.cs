using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class addForce : MonoBehaviour
{
    private float thrust = 22F;
    public Rigidbody rb;
    public bool isRightHand = false;
    public bool isLeftHand = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            isLeftHand = false;
            rb.position = new Vector3(-1.46F, 0, 0);
        }
        else if (collider.tag == "LeftHand")
        {
            isRightHand = false;
            isLeftHand = true;
            rb.position = new Vector3(1.46F, 0, 0);
        }
        else
        {
            Debug.Log("What is this??");
        }
        
        rb.velocity = new Vector3(0, 0, 0);
        rb.useGravity = false;
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
            Debug.Log("Tap!");

            rb.useGravity = true;
            rb.AddForce(new Vector3(-0.2F, 1, 0) * thrust);

            isLeftHand = false;
        }
        else
        {
            Debug.Log("Not tapable");
        }
    }
    public void throwLeft()
    {
        if (isRightHand)
        {
            Debug.Log("Tap!");

            rb.useGravity = true;
            rb.AddForce(new Vector3(0.3F, 0.6F, 0) * thrust);

            isRightHand = false;
        }
        else
        {
            Debug.Log("Not tapable");
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
