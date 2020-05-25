using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Strand : MonoBehaviour
{
    private Lightbulb[] lightbulbs;
    private bool lightChangeDone;
    private bool jumpDone;
    private bool colorChangeDone;
    private bool shortCircuiting;
    private int numLightbulbs;
    private float time;
    private Dictionary<Lightbulb, float[]> LightChangeHash;         // Lightbulb : [start, end, goal]
    private Dictionary<Lightbulb, float[]> LightChangeHashNull;
    private Dictionary<Lightbulb, float[]> JumpHash;                // Lightbulb : [start, finished]
    private Dictionary<Lightbulb, float[]> JumpHashNull;
    private Dictionary<Lightbulb, float[]> ColorChangeHash;         // Lightbulb : [start]
    private Dictionary<Lightbulb, float[]> ColorChangeHashNull;

    void Start()
    {
        lightbulbs = gameObject.GetComponentsInChildren<Lightbulb>();
        lightChangeDone = true;
        jumpDone = true;
        colorChangeDone = true;
        shortCircuiting = false;
        numLightbulbs = (lightbulbs.Length == 0) ? 1 : lightbulbs.Length;
        LightChangeHash = new Dictionary<Lightbulb, float[]>();
        LightChangeHashNull = new Dictionary<Lightbulb, float[]>();
        JumpHash = new Dictionary<Lightbulb, float[]>();
        JumpHashNull = new Dictionary<Lightbulb, float[]>();
        ColorChangeHash = new Dictionary<Lightbulb, float[]>();
        ColorChangeHashNull = new Dictionary<Lightbulb, float[]>();

        foreach (Lightbulb lb in lightbulbs)
        {
            // Use -1.0f to represent disabled
            LightChangeHash.Add(lb, new float[] { -1.0f, -1.0f, -1.0f });
            LightChangeHashNull.Add(lb, new float[] { -1.0f, -1.0f });
            JumpHash.Add(lb, new float[] { -1.0f, -1.0f });
            JumpHashNull.Add(lb, new float[] { -1.0f, -1.0f });
            ColorChangeHash.Add(lb, new float[] { -1.0f });
            ColorChangeHashNull.Add(lb, new float[] { -1.0f });
        }
    }

    void Update()
    {
        time = Time.time;

        // update pattern status
        lightChangeDone = true;
        jumpDone = true;
        colorChangeDone = true;
        
        foreach (Lightbulb lb in lightbulbs)
        {
            // If the lightbulb is the player's then do nothing
            if (lb.isPlayer)
            {
                LightChangeHash[lb][0] = -1.0f;
                LightChangeHash[lb][1] = -1.0f;
                LightChangeHash[lb][2] = -1.0f;
                JumpHash[lb][0] = -1.0f;
                JumpHash[lb][1] = -1.0f;
                ColorChangeHash[lb][0] = -1.0f;

                // If player is short circuiting, update
                shortCircuiting = lb.shortCircuit ? true : false;

                continue;
            }

            // update pattern status
            float[] checkLightChangeReal = LightChangeHash[lb];
            float[] checkLightChangeNull = LightChangeHashNull[lb];
            if (checkLightChangeReal[0] != checkLightChangeNull[0] || checkLightChangeReal[1] != checkLightChangeNull[1])
                lightChangeDone = false;

            float[] checkJumpReal = JumpHash[lb];
            float[] checkJumpNull = JumpHashNull[lb];
            if (checkJumpReal[0] != checkJumpNull[0] || checkJumpReal[1] != checkJumpNull[1])
                jumpDone = false;

            float[] checkColorChangeReal = ColorChangeHash[lb];
            float[] checkColorChangeNull = ColorChangeHashNull[lb];
            if (checkColorChangeReal[0] != checkColorChangeNull[0])
                colorChangeDone = false;

            // LIGHT CHANGE PATTERNS
            float lightChangeStart = LightChangeHash[lb][0];
            float lightChangeEnd = LightChangeHash[lb][1];
            float lightChangeGoal = LightChangeHash[lb][2];

            if (lightChangeStart == -1.0f && lightChangeEnd == -1.0f) { } // do nothing
            else if (lightChangeStart == -1.0f && lightChangeEnd != -1.0f)
            {
                if (time < lightChangeEnd)
                {
                    float diffBrightness = lightChangeGoal - lb.brightness;
                    float changeTime = lightChangeEnd - time;
                    float changeRate = diffBrightness / changeTime;
                    float increment = changeRate * Time.deltaTime;
                    lb.SetBrightness(lb.brightness + increment);
                }
                else
                {
                    LightChangeHash[lb][1] = -1.0f;
                    LightChangeHash[lb][2] = -1.0f;
                    // to catch cases where time exceeds the end before change happens
                    lb.SetBrightness(lightChangeGoal);
                }
            }
            else if (lightChangeStart != -1.0f)
            {
                if (time >= lightChangeStart)
                {
                    LightChangeHash[lb][0] = -1.0f;
                }
            }

            //// JUMP PATTERNS
            //float jumpStart = JumpHash[lb][0];
            //float jumpFinished = JumpHash[lb][1];

            //if (jumpStart == -1.0f && jumpFinished == -1.0f) { } // do nothing
            //else if (jumpStart == -1.0f && jumpFinished != -1.0f)
            //{
            //    if (lb.GetComponent<Rigidbody2D>().position.y == lb.og_y_posn)
            //        JumpHash[lb][1] = -1.0f;
            //}
            //else if (jumpStart != -1.0f)
            //{
            //    if (time >= jumpStart)
            //    {
            //        lb.Jump(1.0f); // find a way to not hardcode this
            //        JumpHash[lb][0] = -1.0f;
            //    }
            //}

            // COLOR CHANGE PATTERNS
            float colorChangeStart = ColorChangeHash[lb][0];

            if (colorChangeStart == -1.0f) { } // do nothing
            else if (colorChangeStart != -1.0f)
            {
                if (time >= colorChangeStart)
                {
                    lb.ChangeColor();
                    ColorChangeHash[lb][0] = -1.0f;
                }
            }
        }
    }

    // UTILITIES
    private void RequestLightChange(Lightbulb lb, float start, float end, float goal)
    {
        LightChangeHash[lb] = new float[] { start, end, goal };
    }

    private void RequestJump(Lightbulb lb, float start)
    {
        JumpHash[lb] = new float[] { start, 1.0f };
    }

    private void RequestColorChange(Lightbulb lb, float start)
    {
        ColorChangeHash[lb] = new float[] { start };
    }

    // GENERAL
    public int GetNumLightBulbs()
    {
        return numLightbulbs;
    }

    public bool IsLightChangeDone()
    {
        return lightChangeDone;
    }

    public bool IsJumpDone()
    {
        return jumpDone;
    }

    public bool IsColorChangeDone()
    {
        return colorChangeDone;
    }

    public bool IsStrandShortCircuiting()
    {
        return shortCircuiting;
    }

    // LIGHT CHANGE
    public void LightChangeAll(float[][] pattern)
    {
        // pattern[i] : [start, end, goal]
        for (int i = 0; i < numLightbulbs; i++)
            RequestLightChange(lightbulbs[i], pattern[i][0], pattern[i][1], pattern[i][2]);
    }

    public void JumpAll(float[][] pattern)
    {
        // pattern[i] : [start] FOR NOW
        for (int i = 0; i < numLightbulbs; i++)
            RequestJump(lightbulbs[i], pattern[i][0]);
    }

    public void ColorChangeAll(float[][] pattern)
    {
        // pattern[i] : [start]
        for (int i = 0; i < numLightbulbs; i++)
            RequestColorChange(lightbulbs[i], pattern[i][0]);
    }
}
