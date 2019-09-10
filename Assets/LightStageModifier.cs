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

    public Sprite SkyColourSprite1;
    public Sprite SkyColourSprite2;
    public Sprite SkyColourSprite3;
    public Sprite SkyColourSprite4;
    public Sprite SkyColourSprite5;
    public Sprite SkyColourSprite6;
    public Sprite SkyColourSprite7;

    public Sprite LightConeSprite1;
    public Sprite LightConeSprite2;
    public Sprite LightConeSprite3;
    public Sprite LightConeSprite4;
    public Sprite LightConeSprite5;
    public Sprite LightConeSprite6;
    public Sprite LightConeSprite7;
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
        SkyColorSpriteRenderer.sprite = SkyColourSprite1;
        LightConeSpriteRenderer.sprite = LightConeSprite1;
    }

    public void ToStageTwo()
    {
        RenderSettings.skybox = Skybox2;
        Light.color = LightColor2;
        SkyColorSpriteRenderer.sprite = SkyColourSprite2;
        LightConeSpriteRenderer.sprite = LightConeSprite2;
    }

    public void ToStageThree()
    {
        RenderSettings.skybox = Skybox3;
        Light.color = LightColor3;
        SkyColorSpriteRenderer.sprite = SkyColourSprite3;
        LightConeSpriteRenderer.sprite = LightConeSprite3;
    }

    public void ToStageFour()
    {
        RenderSettings.skybox = Skybox4;
        Light.color = LightColor4;
        SkyColorSpriteRenderer.sprite = SkyColourSprite4;
        LightConeSpriteRenderer.sprite = LightConeSprite4;
    }

    public void ToStageFive()
    {
        RenderSettings.skybox = Skybox5;
        Light.color = LightColor5;
        SkyColorSpriteRenderer.sprite = SkyColourSprite5;
        LightConeSpriteRenderer.sprite = LightConeSprite5;
    }

    public void ToStageSix()
    {
        RenderSettings.skybox = Skybox6;
        Light.color = LightColor6;
        SkyColorSpriteRenderer.sprite = SkyColourSprite6;
        LightConeSpriteRenderer.sprite = LightConeSprite6;
    }

    public void ToStageSeven()
    {
        RenderSettings.skybox = Skybox7;
        Light.color = LightColor7;
        SkyColorSpriteRenderer.sprite = SkyColourSprite7;
        LightConeSpriteRenderer.sprite = LightConeSprite7;
    }

    #endregion

}
