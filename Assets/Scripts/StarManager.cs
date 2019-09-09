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
        int starsToFill =  (int)Mathf.Floor(percentage * _totalAmountOfStars);

		int i = 0;

        for ( i=0; i< starsToFill; i++)
        {
            
            

            Debug.Log("Stars to fill: " + percentage * _totalAmountOfStars);

            if (starsToFill >= _totalAmountOfStars)
            {
                StartCoroutine(ResetStars(1f));
            }
        }

		if (i < _totalAmountOfStars)
        {
				

			if(i >= 3 && i<7)
			{
				AkSoundEngine.PostEvent("stars_event" + (i+1) , stars[i].transform.GetChild(0).gameObject);
				stars[i].transform.GetChild(0).gameObject.SetActive(true);
			}
			else
			{
				stars[i].transform.GetChild(0).gameObject.SetActive(true);
				// todo uncomment this for 3D models and play sound
				//stars[i].GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
				AkSoundEngine.PostEvent("stars_event0" , gameObject);
			}

		}
    }

    public IEnumerator ResetStars(float waitForReset)
    {
        yield return new WaitForSeconds(waitForReset);
        foreach (GameObject star in stars)
        {
            star.transform.GetChild(0).gameObject.SetActive(false);
            // todo uncomment this for 3D models
            //star.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        }
    }
}
