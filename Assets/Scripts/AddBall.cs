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

    private GameControl gc;

    public List<GameObject> Balls = new List<GameObject>();

    public Vector3 LeftHandSpawn;
    public Vector3 RightHandSpawn;
    private ModifyObjectMesh modifyObjectMesh;

    private void Start()
    {
        gc = GameObject.FindObjectOfType<GameControl>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.N))
        //SpawnBall();
    }

    public GameObject SpawnBall(Side side, Type type)
    {
        MaximumNumberOfBalls = GetComponent<GameControl>().MaximumNumberOfBalls;
        GameObject g = BallOne;
        modifyObjectMesh = g.GetComponent<ModifyObjectMesh>();

        /* switch (MaximumNumberOfBalls)
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
         }*/

        switch (type)
        {
            case Type.Ball:
                modifyObjectMesh.SetToBallMesh();
                break;
            case Type.Ball1:
                modifyObjectMesh.SetToBall1Mesh();
                break;
            case Type.Ball2:
                modifyObjectMesh.SetToBall2Mesh();
                break;
            case Type.Bell:
                modifyObjectMesh.SetToBellMesh();
                break;
            case Type.Package:
                modifyObjectMesh.SetToPackageMesh();
                break;
            case Type.Rocket:
                modifyObjectMesh.SetToRocketMesh();
                break;
            case Type.Sex:
                modifyObjectMesh.SetToSexDrawingMesh();
                break;
            case Type.Car:
                modifyObjectMesh.SetToToyCarMesh();
                break;
            case Type.Dad:
                modifyObjectMesh.SetToDadMesh();
                break;
            case Type.Mom:
                modifyObjectMesh.SetToMomMesh();
                break;
            case Type.Porcelain1:
                modifyObjectMesh.SetToPorcelain1Mesh();
                break;
            case Type.Porcelain2:
                modifyObjectMesh.SetToPorcelain2Mesh();
                break;
            case Type.Porcelain3:
                modifyObjectMesh.SetToPorcelain3Mesh();
                break;
            default:
                break;
        }

        if(side == Side.Left)
        {
            g = Instantiate(g, LeftHandSpawn, Quaternion.identity);
        }
        else if (side == Side.Right)
        {
            g = Instantiate(g, RightHandSpawn, Quaternion.identity);
        }

        gc.throwableObjectList.Add(g.GetComponent<ThrowableObject>());
        return g;
    }
}


public enum Side { Left, Right };