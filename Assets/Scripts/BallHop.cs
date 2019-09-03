using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //var vel = rigidbody.velocity;

        //if (-SideClamp > pos.x || pos.x > SideClamp)
        //    vel.x *= -1;

        //rigidbody.velocity = new Vector3(vel.x, vel.y, 0);
        if (pos.y < -4.5)
            SceneManager.LoadScene(0);
    }

    void OnMouseDown()
    {
        var rigidbody = GetComponent<Rigidbody>();
        if (rigidbody.position.y < 3)
        {
            float randomX = Random.Range(-MaxRandomX, MaxRandomX);
            rigidbody.velocity = new Vector3(randomX, VelocityUpPower, 0);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Contains("Sphere"))
        {
            var animation = GetComponent<Animator>();


            string thisTag = gameObject.tag;
            if (thisTag.Contains("0"))
                animation.Play("blueballTouch");
            if (thisTag.Contains("1"))
                animation.Play("redballTouch");
            if (thisTag.Contains("2"))
                animation.Play("whiteballTouch");
        }
    }
}
