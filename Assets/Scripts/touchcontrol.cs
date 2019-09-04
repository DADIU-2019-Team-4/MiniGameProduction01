using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchcontrol : MonoBehaviour
{
    private List<addForce> balls;
    private Vector3 position;
    private float width;
    private float height;
    public Camera camera;
    private Vector3 currentMouseClickWorldSpace;

    // Start is called before the first frame update
    void Start()
    {
        width = (float)Screen.width;
        height = (float)Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        balls = new List<addForce>(FindObjectsOfType<addForce>());
        //Debug.Log(balls.Capacity);


        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {

            currentMouseClickWorldSpace = camera.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log("clicked x is "+ Input.mousePosition);
            
                if(Input.mousePosition.x < width/2)
                {
                    foreach(addForce ball in balls)
                    {
                        ball.throwLeft();
                    }
                    Debug.Log("clicked left side");
                }
                else
                {
                    foreach (addForce ball in balls)
                    {
                        ball.throwRight();
                    }
                    Debug.Log("clicked right side");
                }
            
        }
    }
}
