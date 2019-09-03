using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHop : MonoBehaviour
{
    public int VelocityUpPower;
    public int MaxRandomX;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var rigidbody = GetComponent<Rigidbody>();
        var pos = rigidbody.position;
        var vel = rigidbody.velocity;

        if (-10 > pos.x || pos.x > 10)
            rigidbody.velocity = new Vector3(-vel.x, vel.y, vel.z);

    }

    void OnMouseDown()
    {
        float randomX = Random.Range(-MaxRandomX, MaxRandomX);

        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(randomX, VelocityUpPower, 0);
    }
}
