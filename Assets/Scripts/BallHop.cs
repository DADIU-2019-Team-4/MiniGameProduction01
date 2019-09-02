using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHop : MonoBehaviour
{
    public int VelocityUpPower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(0, VelocityUpPower, 0);
    }
}
