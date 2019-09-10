﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Ball,
    Ball1,
    Ball2,
    Bell,
    Package,
    Rocket,
    Sex,
    Car,
    Dad,
    Mom,
    Porcelain1,
    Porcelain2,
    Porcelain3
}

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

    public Vector3 RightHandHold;
    public Vector3 LeftHandHold;

    public GameObject dustCloud;

    
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
            rb.position = RightHandHold;
            rb.useGravity = false;
		PlaySFXCaughtItem();
        }
        else if (collider.tag == "LeftHand")
        {
            isLeftHand = true;
            gc.QueueLeftHand(this);
            Debug.Log("Ball Caught," + this.gameObject.GetInstanceID() + "Size = " + gc.leftHandObjects.Count);
            rb.position = LeftHandHold;
            rb.useGravity = false;
		PlaySFXCaughtItem();
        }
        else
        {
            //some other trigger
        }

        rb.velocity = new Vector3(0, 0, 0);
    }

    private void PlaySFXCaughtItem()
    {
        string meshname = GetComponent<MeshFilter>().mesh.name;
        meshname = meshname.Substring(0, meshname.Length - " Instance".Length);
        AkSoundEngine.PostEvent("caught_" + meshname, gameObject);
        Debug.Log("caught_" + meshname);
    }


    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "RightHand" || collider.tag == "LeftHand")
        {
            isRightHand = false;
            isLeftHand = false;
        }
           
    }

    public bool throwRight()
    {
        if (isLeftHand)
        {
            //applyForce(new Vector3(-0.2F, 1, 0));
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
            isRightHand = false;

            return true;
        }

        else
        {
            Debug.Log("Not tapable");
            return false;
        }
    }

    /// <summary>
    /// Setting up animator settings for juggling with the right hand
    /// </summary>
    private IEnumerator JuggleRight()
    {
        Debug.Log("Animating");

        gc.currentLevelThrowCount++;

        _animator.enabled = true;
        _animator.SetBool("isInLeftHand", false);
        _animator.SetBool("isInRightHand", true);
        _animator.SetBool("swipedRightSide", true);

        yield return new WaitForSeconds(1.05f);

        _animator.SetBool("isInLeftHand", true);
        _animator.SetBool("isInRightHand", false);
        _animator.SetBool("swipedRightSide", false);

        Debug.Log("Done");
    }

    /// <summary>
    /// Setting up animator settings for juggling with the left hand
    /// </summary>
    private IEnumerator JuggleLeft()
    {
        gc.currentLevelThrowCount++;

        _animator.enabled = true;
        _animator.SetBool("isInLeftHand", true);
        _animator.SetBool("isInRightHand", false);
        _animator.SetBool("swipedLeftSide", true);

        yield return new WaitForSeconds(1.05f);

        _animator.SetBool("isInLeftHand", false);
        _animator.SetBool("isInRightHand", true);
        _animator.SetBool("swipedLeftSide", false);
    }

    public void applyForce(Vector3 dir)
    {
        Debug.Log("Tap!");
        rb.velocity = new Vector3(0, 0, 0);
        rb.AddForce(dir * thrust);
        gc.currentLevelThrowCount++;
    }

    public void instantiateDust()
    {
        Instantiate(dustCloud, transform.position, Quaternion.identity); 
    }
}
