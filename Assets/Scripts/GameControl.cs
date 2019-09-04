using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private int catchCounter;
    private int currentLevel=1;
    public int throwCount=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (throwCount > 8 && currentLevel==1)
        {
            currentLevel=2;
        }
    }

    public int getCurrentLevel()
    {
        return currentLevel;
    }
}
