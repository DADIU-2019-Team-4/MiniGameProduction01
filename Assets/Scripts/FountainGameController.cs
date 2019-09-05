using System.Collections.Generic;
using UnityEngine;

public class FountainGameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnPointLeft;
    [SerializeField]
    private GameObject _spawnPointRight;

    [SerializeField]
    private FountainBall _leftBall;
    [SerializeField]
    private FountainBall _rightBall;

    private List<FountainBall> _rightBalls =  new List<FountainBall>();
    private List<FountainBall> _leftBalls = new List<FountainBall>();

    private bool _firstTimeLeft = true;
    private bool _firstTimeRight = true;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.6f;
        _leftBalls.Add(Instantiate(_leftBall, _spawnPointLeft.transform.position, Quaternion.identity));
        _rightBalls.Add(Instantiate(_rightBall, _spawnPointRight.transform.position, Quaternion.identity));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JuggleLeft()
    {
        if (_firstTimeLeft)
        {
            _leftBalls.Add(Instantiate(_leftBall, _spawnPointLeft.transform.position, Quaternion.identity));
            _firstTimeLeft = false;
        }

        foreach (FountainBall ball in _leftBalls)
        {
            ball.Juggle();
        }
    }

    public void JuggleRight()
    {
        if (_firstTimeRight)
        {
            _rightBalls.Add(Instantiate(_rightBall, _spawnPointRight.transform.position, Quaternion.identity));
            _firstTimeRight = false;
        }

        foreach (FountainBall ball in _rightBalls)
        {
            ball.Juggle();
        }
    }
}
