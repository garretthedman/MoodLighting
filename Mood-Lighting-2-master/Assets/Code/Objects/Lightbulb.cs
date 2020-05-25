using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightbulb : MonoBehaviour
{

    private Transform _myTransform;
    
    // private variables of different 
    private SpriteRenderer colorSprite;
    private SpriteRenderer holderSprite;

    public GameObject colorObject;
    public GameObject holderObject;
    
    public float axis;
    public float lastAxis;

    public static bool queriesHitTriggers = true;

    public float brightness;
    public bool isPlayer;
    public bool isOn;
    public bool isDead;
    private float _lastColorChange;
    private const float colorChangeCooldown = 1f;
    
    public bool shortCircuit = false;

    public float og_y_posn;

    ScoreManager scoreManager;
    RoundManager RoundManager;
    public int turn;
    public float selectPlayer;

    // Start is called before the first frame update
    void Start()
    {
       // scoreManager = GameObject.Find("ScoreManager");
        
        og_y_posn = GetComponent<Rigidbody2D>().position.y;
        // Add Sprite Images: Brightness, LightColor, and Bulb

        colorSprite = colorObject.GetComponent<SpriteRenderer>();
       holderSprite = holderObject.GetComponent<SpriteRenderer>();
       
        // Set bulb variables 

        SetBrightness(50);
        if (gameObject.tag == "Player")
            isPlayer = true;
        else isPlayer = false;
        
        //lightbul starts off and not dead
        isOn = false;
        isDead = false;

        RoundManager = FindObjectOfType<RoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        turn = RoundManager.GetComponent<RoundManager>().getTurn();
    }
    
    // Method to turn lightbulb on and off
    public void ToggleBulb()
    {
        if (isDead) return;
        if (isOn)
        {
            LightOff();
        }

        else
        {
            LightOn();
        }
    }

    public void LightOff()
    {
        SetBrightness(0f);
        isOn = false;
    }

    public void LightOn()
    {
        SetBrightness(100f);
        isOn = true;
    }

    public void GuessMade()
    {
        var lightDead = Resources.Load<Sprite>("Sprites/lightDead");

        if (isPlayer)
        {
            LightOff();
            holderSprite.sprite = lightDead;
            isDead = true;
            
            scoreManager = FindObjectOfType<ScoreManager>();
            scoreManager.GetComponent<ScoreManager>().EndGame(false);
            
            // Debug.Log("Correct guess - You win!");
        }

        else
        {
            LightOff();
            holderSprite.sprite = lightDead;
            isDead = true;
            
            scoreManager = FindObjectOfType<ScoreManager>();

            //THIS ISN'T WORKING ??
            scoreManager.GetComponent<ScoreManager>().Wrong_Guess();
        }
    }
    
    public void SetBrightness(float newBrightness)
    {
        //don't change bulb if dead
        if (isDead) return;
        
        brightness = newBrightness;
        var brightFloat = newBrightness / 100f;
        colorSprite.color = new Color(1f, 1f, 1f, brightFloat);
    }

    public void ChangeColor()
    {
        var colorPink = Resources.Load<Sprite>("Sprites/pinkColor");
        var colorBlue = Resources.Load<Sprite>("Sprites/blueColor");
        var colorYellow = Resources.Load<Sprite>("Sprites/yellowColor");

        if (colorSprite.sprite == colorPink)
        {
            //color = colorChoice.blue;
            colorSprite.sprite = colorBlue;
        }
        else if (colorSprite.sprite == colorBlue)
        {
            //color = colorChoice.yellow;
            colorSprite.sprite = colorYellow;
        }
        else if (colorSprite.sprite == colorYellow)
        {
            //color = colorChoice.pink;
            colorSprite.sprite = colorPink;
        }
    }

    // Updates light brightness as long as light is not dead
    private void HandleInput()
    {
        if (isPlayer && !isDead)
        {
            //BRIGHTNESS CONTROL
            float newBrightness;
            if (turn == 1)
            {
                newBrightness = Input.GetAxis("BrightnessP1");
            }
            else
            {
                newBrightness = Input.GetAxis("BrightnessP2");
            }

            SetBrightness((newBrightness+1)*50);


            //CHANGE COLORS
            // joystick button 1, A button

            if (turn == 1)
            {
                axis = Input.GetAxis("APlayer1");
            }
            else
            {
                axis = Input.GetAxis("APlayer2");
            }

            if (axis > 0 && System.Math.Abs(lastAxis) < 0.001)
            {
                //Debug.Log("Color Change button pressed!");
                ChangeColor();
            }
            lastAxis = axis;        
        }
    }

    public void SetPlayer()
    {
        isPlayer = true;
    }

    // Used at end of game to stop lightbulbs from changing
    public void KillAll()
    {
        isDead = true;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Selection")
        {
            if (turn == 1)
            {
                selectPlayer = Input.GetAxis("APlayer1");
            }
            else
            {
                selectPlayer = Input.GetAxis("APlayer2");
            }

            if (selectPlayer == 1)
            {
                isPlayer = true;
            }
        }
    }
}
