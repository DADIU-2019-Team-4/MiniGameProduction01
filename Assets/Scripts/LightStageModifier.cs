using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStageModifier : MonoBehaviour
{

    #region Fields
    public Light Light;
    public GameObject SkyColour;
    public GameObject LightCone;

    private SpriteRenderer SkyColorSpriteRenderer;
    private SpriteRenderer LightConeSpriteRenderer;

    public Material Skybox1;
    public Material Skybox2;
    public Material Skybox3;
    public Material Skybox4;
    public Material Skybox5;
    public Material Skybox6;
    public Material Skybox7;

    public Color LightColor1;
    public Color LightColor2;
    public Color LightColor3;
    public Color LightColor4;
    public Color LightColor5;
    public Color LightColor6;
    public Color LightColor7;

    public Color SkyColourSprite1;
    public Color SkyColourSprite2;
    public Color SkyColourSprite3;
    public Color SkyColourSprite4;
    public Color SkyColourSprite5;
    public Color SkyColourSprite6;
    public Color SkyColourSprite7;

    public Color LightConeSprite1;
    public Color LightConeSprite2;
    public Color LightConeSprite3;
    public Color LightConeSprite4;
    public Color LightConeSprite5;
    public Color LightConeSprite6;
    public Color LightConeSprite7;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SkyColorSpriteRenderer = SkyColour.GetComponent<SpriteRenderer>();
        LightConeSpriteRenderer = LightCone.GetComponent<SpriteRenderer>();
        Debug.Log("Lightning/Stage system activated successfully!");
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Functions

    public void ToStageOne()
    {
        RenderSettings.skybox = Skybox1;
        Light.color = LightColor1;
        SkyColorSpriteRenderer.color = SkyColourSprite1;
        LightConeSpriteRenderer.color = LightConeSprite1;
    }

    public void ToStageTwo()
    {
        RenderSettings.skybox = Skybox2;
        Light.color = LightColor2;
        SkyColorSpriteRenderer.color = SkyColourSprite2;
        LightConeSpriteRenderer.color = LightConeSprite2;
    }

    public void ToStageThree()
    {
        RenderSettings.skybox = Skybox3;
        Light.color = LightColor3;
        SkyColorSpriteRenderer.color = SkyColourSprite3;
        LightConeSpriteRenderer.color = LightConeSprite3;
    }

    public void ToStageFour()
    {
        RenderSettings.skybox = Skybox4;
        Light.color = LightColor4;
        SkyColorSpriteRenderer.color = SkyColourSprite4;
        LightConeSpriteRenderer.color = LightConeSprite4;
    }

    public void ToStageFive()
    {
        RenderSettings.skybox = Skybox5;
        Light.color = LightColor5;
        SkyColorSpriteRenderer.color = SkyColourSprite5;
        LightConeSpriteRenderer.color = LightConeSprite5;
    }

    public void ToStageSix()
    {
        RenderSettings.skybox = Skybox6;
        Light.color = LightColor6;
        SkyColorSpriteRenderer.color = SkyColourSprite6;
        LightConeSpriteRenderer.color = LightConeSprite6;
    }

    public void ToStageSeven()
    {
        RenderSettings.skybox = Skybox7;
        Light.color = LightColor7;
        SkyColorSpriteRenderer.color = SkyColourSprite7;
        LightConeSpriteRenderer.color = LightConeSprite7;
    }

    #endregion

}
