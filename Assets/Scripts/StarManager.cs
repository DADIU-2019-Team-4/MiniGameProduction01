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

    private InputController _inputController;

    public GameObject LightShineparticle; 

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
                Star star = stars[i].GetComponent<Star>();
                if (i >= 3 && i < 7)
                {
                    if (!star.IsActivated)
                    {
                        stars[i].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                        AkSoundEngine.PostEvent("stars_event" + (i + 1), gameObject);
                        Instantiate(LightShineparticle, stars[i].transform.position, Quaternion.identity);
                        star.IsActivated = true;
                    }
                }
                else
                {
                    if (!star.IsActivated)
                    {
                        stars[i].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                        AkSoundEngine.PostEvent("stars_event0", gameObject);
                        Instantiate(LightShineparticle, stars[i].transform.position, Quaternion.identity);
                        star.IsActivated = true;
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
            star.GetComponent<Star>().IsActivated = false;
        }
    }
}
