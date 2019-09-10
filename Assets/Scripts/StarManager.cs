﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StarManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> stars = new List<GameObject>(7);

    private int _totalAmountOfStars;

    private InputController _inputController;

    // Start is called before the first frame update
    void Start()
    {
        _inputController = FindObjectOfType<InputController>();

        if (_inputController.ThrowEvent == null)
            _inputController.ThrowEvent = new UnityEvent();

        _totalAmountOfStars = stars.Count;
    }

    public void CalculatePercentage(int current, int total)
    {
        float percentage = (float)current / total;
        int starsToFill = (int)Mathf.Floor(percentage * _totalAmountOfStars);

        for (int i = 0; i < starsToFill; i++)
        {
            Debug.Log("Stars to fill: " + percentage * _totalAmountOfStars);

            if (i < _totalAmountOfStars)
            {
                if (i >= 3 && i < 7)
                {
                    if (!stars[i].GetComponent<Star>().IsActivated)
                    {
                        stars[i].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                        AkSoundEngine.PostEvent("stars_event" + (i + 1), gameObject);
                    }
                }
                else
                {
                    if (!stars[i].GetComponent<Star>().IsActivated)
                    {
                        stars[i].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                        AkSoundEngine.PostEvent("stars_event0", gameObject);
                    }
                }
            }

            if (starsToFill >= _totalAmountOfStars)
            {
                StartCoroutine(ResetStars(1f));
            }
        }
    }

    public IEnumerator ResetStars(float waitForReset)
    {
        if (_inputController.LevelEnd)
            yield break;

        yield return new WaitForSeconds(waitForReset);
        foreach (GameObject star in stars)
        {
            star.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
    }
}
