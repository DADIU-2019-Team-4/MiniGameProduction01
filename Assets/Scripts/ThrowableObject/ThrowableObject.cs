using System.Collections;
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
    private BoxCollider bc;
    private bool isRightHand = false;
    private bool isLeftHand = false;

    [HideInInspector]
    public GameControl gc;
    [HideInInspector]
    public bool isWaiting;

    public Vector3 RightHandHold;
    public Vector3 LeftHandHold;

    public GameObject dustCloud;

    //vibration
    public long shakeDur = 5;

    //screen shake
    public GameObject mainCamera;
    public CameShake1 shake;


    public Type type;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        gc = GameObject.FindObjectOfType<GameControl>();
        //finding main camera
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        shake = mainCamera.GetComponent<CameShake1>();
        rb.velocity = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        //Debug.Log(transform.position);
    }


    private void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("vvvvvvvvv");
        Debug.Log(">>>>  collided: "+ collider.gameObject.name +" ID "+ gameObject.GetInstanceID() + "   " + transform.position + "<<<<<");
        //Debug.Log(transform.position);
        //Debug.Log(gameObject.GetInstanceID());

        //Debug.Log("^^^^^^^^^^");
        //Debug.Break();
        if (collider.tag == "RightHand")
        {
            isRightHand = true;
            gc.QueueRightHand(this);
            Debug.Log("Ball Caught," + this.gameObject.GetInstanceID() + "Size = " + gc.rightHandObjects.Count);
            rb.position = RightHandHold;
            rb.useGravity = false;
		    PlaySFXCaughtItem();


            
            

            rb.velocity = new Vector3(0, 0, 0);
            _animator.SetBool("isInRightHand", true);
        }
        if (collider.tag == "LeftHand")
        {
            isLeftHand = true;
            gc.QueueLeftHand(this);
            Debug.Log("Ball Caught," + this.gameObject.GetInstanceID() + "Size = " + gc.leftHandObjects.Count);
            rb.position = LeftHandHold;
            rb.useGravity = false;
		    PlaySFXCaughtItem();


            

            rb.velocity = new Vector3(0, 0, 0);
            
            _animator.SetBool("isInLeftHand", true);
        }
        else
        {
            //some other trigger
        }

    }

    private void PlaySFXCaughtItem()
    {
        string meshname = GetComponent<MeshFilter>().mesh.name;
        meshname = meshname.Substring(0, meshname.Length - " Instance".Length);
        AkSoundEngine.PostEvent("caught_" + meshname, gameObject);
        //Debug.Log("caught_" + meshname);
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
            Vibration.Vibrate(shakeDur);
           // StartCoroutine(shake.Shake(.04f, 0.03f));
            StartCoroutine(JuggleToRightHand());
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
            Vibration.Vibrate(shakeDur);
           // StartCoroutine(shake.Shake(.04f, 0.03f));
            StartCoroutine(JuggleToLeftHand());
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
    private IEnumerator JuggleToRightHand()
    {
        //Debug.Log("Animating");

        gc.currentLevelThrowCount++;

        _animator.enabled = true;

        if (gc.currentLevel == 4 || gc.currentLevel == 5)
        {
            _animator.SetBool("4Balls", true);
        }

        else if (gc.currentLevel >= 6)
        {
            _animator.SetBool("5Balls", true);
        }

        else
        {
            _animator.SetBool("Default", true);
        }

        _animator.SetBool("isInLeftHand", true);
        _animator.SetBool("isInRightHand", false);
        _animator.SetBool("swipedRightSide", true);

        yield return new WaitForSeconds(0.3f);

        _animator.SetBool("isInLeftHand", false);

        _animator.SetBool("swipedRightSide", false);
        _animator.SetBool("isInRightHand", true);

        

        //Debug.Log("Done");
    }

    /// <summary>
    /// Setting up animator settings for juggling with the left hand
    /// </summary>
    private IEnumerator JuggleToLeftHand()
    {
        gc.currentLevelThrowCount++;

        _animator.enabled = true;

        if (gc.currentLevel == 4 || gc.currentLevel == 5)
        {
            _animator.SetBool("4Balls", true);
        }
            
        else if (gc.currentLevel >= 6)
        {
            _animator.SetBool("5Balls", true);
        }
            
        else
        {
            _animator.SetBool("Default", true);
        }


        _animator.SetBool("isInLeftHand", false);
        _animator.SetBool("isInRightHand", true);
        _animator.SetBool("swipedLeftSide", true);

        yield return new WaitForSeconds(0.3f);


        _animator.SetBool("isInRightHand", false);
        _animator.SetBool("swipedLeftSide", false);
        _animator.SetBool("isInLeftHand", true);
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


public static class Vibration
{

#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    public static void Vibrate()
    {
        if (isAndroid())
            vibrator.Call("vibrate");
        else
            Handheld.Vibrate();
    }


    public static void Vibrate(long milliseconds)
    {
        if (isAndroid())
            vibrator.Call("vibrate", milliseconds);
        else
            Handheld.Vibrate();
    }

    public static void Vibrate(long[] pattern, int repeat)
    {
        if (isAndroid())
            vibrator.Call("vibrate", pattern, repeat);
        else
            Handheld.Vibrate();
    }

    public static bool HasVibrator()
    {
        return isAndroid();
    }

    public static void Cancel()
    {
        if (isAndroid())
            vibrator.Call("cancel");
    }

    private static bool isAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
	return true;
#else
        return false;
#endif
    }
}