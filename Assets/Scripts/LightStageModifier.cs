using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStageModifier : MonoBehaviour
{

    #region Fields
    public Light LightA;
    public Light LightB;
    public Light LightC;
    public GameObject SkyColour;
    public GameObject LightCone;

    private SpriteRenderer SkyColorSpriteRenderer;
    private SpriteRenderer LightConeSpriteRenderer;

    public Color LightColourA1;
    public Color LightColourA2;
    public Color LightColourA3;
    public Color LightColourA4;
    public Color LightColourA5;
    public Color LightColourA6;
    public Color LightColourA7;

    public Color LightColourB1;
    public Color LightColourB2;
    public Color LightColourB3;
    public Color LightColourB4;
    public Color LightColourB5;
    public Color LightColourB6;
    public Color LightColourB7;

    public Color LightColourC1;
    public Color LightColourC2;
    public Color LightColourC3;
    public Color LightColourC4;
    public Color LightColourC5;
    public Color LightColourC6;
    public Color LightColourC7;

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
        LightA.color = LightColourA1;
        LightB.color = LightColourB1;
        LightC.color = LightColourC1;
        SkyColorSpriteRenderer.color = SkyColourSprite1;
        LightConeSpriteRenderer.color = LightConeSprite1;
    }

    public void ToStageTwo()
    {
        LightA.color = LightColourA2;
        LightB.color = LightColourB2;
        LightC.color = LightColourC2;
        SkyColorSpriteRenderer.color = SkyColourSprite2;
        LightConeSpriteRenderer.color = LightConeSprite2;
    }

    public void ToStageThree()
    {
        LightA.color = LightColourA3;
        LightB.color = LightColourB3;
        LightC.color = LightColourC3;
        SkyColorSpriteRenderer.color = SkyColourSprite3;
        LightConeSpriteRenderer.color = LightConeSprite3;
    }

    public void ToStageFour()
    {
        LightA.color = LightColourA4;
        LightB.color = LightColourB4;
        LightC.color = LightColourC4;
        SkyColorSpriteRenderer.color = SkyColourSprite4;
        LightConeSpriteRenderer.color = LightConeSprite4;
    }

    public void ToStageFive()
    {
        LightA.color = LightColourA5;
        LightB.color = LightColourB5;
        LightC.color = LightColourC5;
        SkyColorSpriteRenderer.color = SkyColourSprite5;
        LightConeSpriteRenderer.color = LightConeSprite5;
    }

    public void ToStageSix()
    {
        LightA.color = LightColourA6;
        LightB.color = LightColourB6;
        LightC.color = LightColourC6;
        SkyColorSpriteRenderer.color = SkyColourSprite6;
        LightConeSpriteRenderer.color = LightConeSprite6;
    }

    public void ToStageSeven()
    {
        LightA.color = LightColourA7;
        LightB.color = LightColourB7;
        LightC.color = LightColourC7;
        SkyColorSpriteRenderer.color = SkyColourSprite7;
        LightConeSpriteRenderer.color = LightConeSprite7;
    }

    #endregion

}
