using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddBall : MonoBehaviour
{
    public GameObject BallOne;
    public GameObject BallTwo;
    public GameObject BallThree;
    public GameObject BallFour;
    private int MaximumNumberOfBalls;

    public List<GameObject> Balls = new List<GameObject>();

    public Vector3 StartPosition;
   


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.N))
        //    SpawnBall();
    }

    public GameObject SpawnBall(Side side)
    {
        MaximumNumberOfBalls = GetComponent<GameControl>().MaximumNumberOfBalls;
        GameObject g = null;
        switch (MaximumNumberOfBalls)
        {
            case 1:
                g = BallOne;
                break;

            case 2:
                g = BallTwo;
                break;

            case 3:
                g = BallThree;
                break;

            case 4:
                g = BallFour;
                break;
            case 5:
                g = BallFour;
            break;
        }
        g = Instantiate(g, StartPosition, Quaternion.identity);
        Balls.Add(g);
        return g;
    }
}


public enum Side { Left, Right };
