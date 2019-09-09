using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StarManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> stars = new List<GameObject>(7);

    private int _totalAmountOfStars;

    private GameControl _gameControl;
    private InputController _inputController;


    // Start is called before the first frame update
    void Start()
    {
        _gameControl = FindObjectOfType<GameControl>();
        _inputController = FindObjectOfType<InputController>();

        if (_inputController.ThrowEvent == null)
            _inputController.ThrowEvent = new UnityEvent();

        _inputController.ThrowEvent.AddListener(UpdateStars);

        _totalAmountOfStars = stars.Count;
    }

    private void UpdateStars()
    {
        switch (_gameControl.currentLevel)
        {
            case 1:
                CalculatePercentage(_gameControl.toLevel2Count);
                break;
            case 2:
                CalculatePercentage(_gameControl.toLevel3Count);
                break;
            case 3:
                CalculatePercentage(_gameControl.toLevel4Count);
                break;
            case 4:
                CalculatePercentage(_gameControl.toLevel5Count);
                break;
            case 5:
                CalculatePercentage(_gameControl.toLevel6Count);
                break;
        }
    }

    private void CalculatePercentage(int total)
    {
        float percentage = (float)_gameControl.currentLevelThrowCount / total;
        int starsToFill =  (int)Mathf.Floor(percentage * _totalAmountOfStars);

        for (int i = 0; i < starsToFill; i++)
        {
            if (i < _totalAmountOfStars)
                stars[i].transform.GetChild(0).gameObject.SetActive(true);

            if (starsToFill >= _totalAmountOfStars)
            {
                StartCoroutine(ResetStars());
            }
        }
    }

    private IEnumerator ResetStars()
    {
        yield return new WaitForSeconds(1f);
        foreach (GameObject star in stars)
        {
            star.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
