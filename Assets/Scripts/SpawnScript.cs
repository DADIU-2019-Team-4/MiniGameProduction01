using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnScript : MonoBehaviour
{

    private bool SpawnedSecondBall = false;
    public GameObject Plane;
    public GameObject SecondBall;
    public GameObject ThirdBall;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!SpawnedSecondBall)
        {
            var rigidbody = GetComponent<Rigidbody>();
            if (rigidbody.position.y > 7)
            {
                Plane.SetActive(false);
                SecondBall.SetActive(true);
                SpawnedSecondBall = true;
            }
        }
        else
        {
            //var mainCamera = Camera.main.transform.position
            var mainCamera = Camera.main.transform;

            if (mainCamera.position.z < -9)
                Camera.main.transform.position += new Vector3(0, 0, 0.005f);

            else if (!ThirdBall.activeSelf)
            {
                ThirdBall.SetActive(true);
                ThirdBall.GetComponent<Rigidbody>().velocity = new Vector3(0, 2, 0);
            }
        }


    }

}
