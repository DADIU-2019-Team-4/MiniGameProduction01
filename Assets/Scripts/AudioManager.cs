using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // UI Elements
    public Slider MusicSlider;
    public Slider SFXSlider;

    // Variables the UI Sliders write to.
    public float MusicSliderValue;
    public float SFXSliderValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MusicSliderValue = MusicSlider.value;
        SFXSliderValue = SFXSlider.value;
    }
}
