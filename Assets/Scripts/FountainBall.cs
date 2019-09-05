using UnityEngine;
using UnityEngine.SceneManagement;

public class FountainBall : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private Animation _animation;

    private bool _inHand;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animation = GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("LeftHand") || col.gameObject.CompareTag("RightHand"))
        {
            _rigidBody.isKinematic = true;
            _inHand = true;
        }

        if (col.gameObject.CompareTag("Ball"))
        {
            col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        if (col.gameObject.CompareTag("Ground"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Juggle()
    {
        if (_inHand)
        {
            _animation.Play();
        }
    }

}
