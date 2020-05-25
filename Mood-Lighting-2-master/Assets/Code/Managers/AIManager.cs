using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    private Strand strand;
    private float time;
    private bool lightChangePatternRan;
    private bool colorChangePatternRan;
    private bool timingPatternRan;
    public int difficulty; // do not set difficulty in start
    void Start()
    {
        strand = gameObject.GetComponent<Strand>();
    }
    void Update()
    {
        time = Time.time;
        // DIFFICULTY SCAFFOLDING
        switch (difficulty)
        {
            // Light Change: Cascade Slow(1.0) Random(.6)
            // Color Change: None
            case 0:
                if (strand.IsLightChangeDone())
                {
                    float speed = 1.0f;
                    float delay = 0.0f;
                    if (!lightChangePatternRan)
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + speed*i + Random.Range(0.0f, 0.6f),
                            time + speed*i + delay*i + Random.Range(0.6f, 1.2f),
                            Random.Range(0.0f, 0.0f)
                            };
                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = true;
                    }
                    else
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + speed*i + Random.Range(0.0f, 0.6f),
                            time + speed*i + delay*i + Random.Range(0.6f, 1.2f),
                            Random.Range(100.0f, 100.0f)
                            };
                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = false;
                    }
                }
                break;

            // Light Change: Cascade Medium(.75) Random(.4)
            // Color Change: None
            case 1:
                if (strand.IsLightChangeDone())
                {
                    float speed = 1.0f;
                    float delay = 0.0f;
                    if (!lightChangePatternRan)
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + speed*i + Random.Range(0.0f, 0.4f),
                            time + speed*i + delay*i + Random.Range(0.4f, 0.8f),
                            Random.Range(0.0f, 0.0f)
                            };
                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = true;
                    }
                    else
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + speed*i + Random.Range(0.0f, 0.4f),
                            time + speed*i + delay*i + Random.Range(0.4f, 0.8f),
                            Random.Range(100.0f, 100.0f)
                            };
                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = false;
                    }
                }
                break;

            // Light Change: Cascade Fast(.35) Random(.3)
            // Color Change: None
            case 2:
                if (strand.IsLightChangeDone())
                {
                    float speed = 0.35f;
                    float delay = 0.0f;
                    if (!lightChangePatternRan)
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + speed*i + Random.Range(0.0f, 0.3f),
                            time + speed*i + delay*i + Random.Range(0.3f, 0.6f),
                            Random.Range(0.0f, 0.0f)
                            };
                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = true;
                    }
                    else
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + speed*i + Random.Range(0.0f, 0.3f),
                            time + speed*i + delay*i + Random.Range(0.3f, 0.6f),
                            Random.Range(100.0f, 100.0f)
                            };
                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = false;
                    }
                }
                break;

            // Light Change: Alternate Medium(.25) Random(.3)
            // Color Change: None
            case 3:
                if (strand.IsLightChangeDone())
                {
                    float speed = 0.25f;
                    if (!lightChangePatternRan)
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            if (i % 2 == 0)
                            {
                                pattern[i] = new float[] {
                                time + speed + Random.Range(0.0f, 0.3f),
                                time + speed + Random.Range(0.3f, 0.6f),
                                Random.Range(0.0f, 0.0f)
                                };
                            }
                            else
                            {
                                pattern[i] = new float[] {
                                time + speed + Random.Range(0.0f, 0.3f),
                                time + speed + Random.Range(0.3f, 0.6f),
                                Random.Range(100.0f, 100.0f)
                                };
                            }

                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = true;
                    }
                    else
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            if (i % 2 == 0)
                            {
                                pattern[i] = new float[] {
                                time + speed + Random.Range(0.0f, 0.3f),
                                time + speed + Random.Range(0.3f, 0.6f),
                                Random.Range(100.0f, 100.0f)
                            };
                            }
                            else
                            {
                                pattern[i] = new float[] {
                                time + speed + Random.Range(0.0f, 0.3f),
                                time + speed + Random.Range(0.3f, 0.6f),
                                Random.Range(0.0f, 0.0f)
                            };
                            }

                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = false;
                    }
                }
                break;

            // Light Change: Alternate Fast(.05) Random(.3)
            // Color Change: None
            case 4:
                if (strand.IsLightChangeDone())
                {
                    float speed = 0.05f;
                    if (!lightChangePatternRan)
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            if (i % 2 == 0)
                            {
                                pattern[i] = new float[] {
                                time + speed + Random.Range(0.0f, 0.3f),
                                time + speed + Random.Range(0.3f, 0.6f),
                                Random.Range(0.0f, 0.0f)
                                };
                            }
                            else
                            {
                                pattern[i] = new float[] {
                                time + speed + Random.Range(0.0f, 0.3f),
                                time + speed + Random.Range(0.3f, 0.6f),
                                Random.Range(100.0f, 100.0f)
                                };
                            }

                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = true;
                    }
                    else
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            if (i % 2 == 0)
                            {
                                pattern[i] = new float[] {
                                time + speed + Random.Range(0.0f, 0.3f),
                                time + speed + Random.Range(0.3f, 0.6f),
                                Random.Range(100.0f, 100.0f)
                            };
                            }
                            else
                            {
                                pattern[i] = new float[] {
                                time + speed + Random.Range(0.0f, 0.3f),
                                time + speed + Random.Range(0.3f, 0.6f),
                                Random.Range(0.0f, 0.0f)
                            };
                            }

                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = false;
                    }
                }
                break;

            // Light Change: None
            // Color Change: On/Off Medium(.75) Random(.2)
            case 5:
                if (strand.IsColorChangeDone())
                {
                    float speed = 0.75f;
                    float[][] pattern = new float[strand.GetNumLightBulbs()][];
                    for (int i = 0; i < pattern.Length; i++)
                    {
                        pattern[i] = new float[] {
                        time + speed + Random.Range(0.0f, 0.2f),
                        };
                    }
                    strand.ColorChangeAll(pattern);
                }
                break;

            // Light Change: None
            // Color Change: On/Off Fast(.55) Random(.2)
            case 6:
                if (strand.IsColorChangeDone())
                {
                    float speed = 0.55f;
                    float[][] pattern = new float[strand.GetNumLightBulbs()][];
                    for (int i = 0; i < pattern.Length; i++)
                    {
                        pattern[i] = new float[] {
                        time + speed + Random.Range(0.0f, 0.2f),
                        };
                    }
                    strand.ColorChangeAll(pattern);
                }
                break;

            // Light Change: None
            // Color Change: On/Off Fast(.55) Timing(1+34) Random(.2)
            case 7:
                if (strand.IsColorChangeDone())
                {
                    float speed = 0.55f;
                    if (!colorChangePatternRan || !timingPatternRan)
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + speed + Random.Range(0.0f, 0.2f),
                            };
                        }
                        strand.ColorChangeAll(pattern);
                        if (!colorChangePatternRan && !timingPatternRan)
                            colorChangePatternRan = true;
                        else if (colorChangePatternRan)
                            timingPatternRan = true;
                    }
                    else
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + speed/3 + Random.Range(0.0f, 0.2f),
                            };
                        }
                        strand.ColorChangeAll(pattern);
                        colorChangePatternRan = false;
                        timingPatternRan = false;
                    }
                }
                break;

            // Light Change: None
            // Color Change: Alternate Medium(.55) Random(.2)
            case 8:
                if (strand.IsColorChangeDone())
                {
                    float speed = 0.55f;
                    if (!colorChangePatternRan)
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            if (i % 2 == 0)
                            {
                                pattern[i] = new float[] {
                                time + speed + Random.Range(0.0f, 0.2f)
                                };
                            }
                            else
                            {
                                pattern[i] = new float[] { -1.0f };
                            }

                        }
                        strand.ColorChangeAll(pattern);
                        colorChangePatternRan = true;
                    }
                    else
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            if (i % 2 == 0)
                            {
                                pattern[i] = new float[] { -1.0f };
                            }
                            else
                            {
                                pattern[i] = new float[] {
                                time + speed + Random.Range(0.0f, 0.2f)
                                };
                            }

                        }
                        strand.ColorChangeAll(pattern);
                        colorChangePatternRan = false;
                    }
                }
                break;

            // Light Change: On/Off Slower(.95) Random(.2)
            // Color Change: On/Off Slow(.75) Random(.2)
            case 9:
                if (strand.IsLightChangeDone() && strand.IsColorChangeDone())
                {
                    float lightSpeed = 0.95f;
                    float colorSpeed = 0.75f;
                    if (!lightChangePatternRan)
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + lightSpeed + Random.Range(0.0f, 0.2f),
                            time + lightSpeed + Random.Range(0.2f, 0.4f),
                            Random.Range(100.0f, 100.0f)
                            };
                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = true;
                    }
                    else if (!colorChangePatternRan)
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + colorSpeed + Random.Range(0.0f, 0.2f)
                            };

                        }
                        strand.ColorChangeAll(pattern);
                        colorChangePatternRan = true;
                    }
                    else
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + lightSpeed + Random.Range(0.0f, 0.2f),
                            time + lightSpeed + Random.Range(0.2f, 0.4f),
                            Random.Range(0.0f, 0.0f)
                            };
                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = false;
                        colorChangePatternRan = false;
                    }
                }
                break;

            // Light Change: On/Off Slow(.75) Random(.2)
            // Color Change: On/Off Medium(.55) Random(.2)
            case 10:
                if (strand.IsLightChangeDone() && strand.IsColorChangeDone())
                {
                    float lightSpeed = 0.75f;
                    float colorSpeed = 0.55f;
                    if (!lightChangePatternRan)
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + lightSpeed + Random.Range(0.0f, 0.2f),
                            time + lightSpeed + Random.Range(0.2f, 0.4f),
                            Random.Range(100.0f, 100.0f)
                            };
                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = true;
                    }
                    else if (!colorChangePatternRan)
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + colorSpeed + Random.Range(0.0f, 0.2f)
                            };

                        }
                        strand.ColorChangeAll(pattern);
                        colorChangePatternRan = true;
                    }
                    else
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + lightSpeed + Random.Range(0.0f, 0.2f),
                            time + lightSpeed + Random.Range(0.2f, 0.4f),
                            Random.Range(0.0f, 0.0f)
                            };
                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = false;
                        colorChangePatternRan = false;
                    }
                }
                break;

            // Light Change: On/Off Medium(.55) Random(.2)
            // Color Change: On/Off Fast(.35) Random(.2)
            case 11:
                if (strand.IsLightChangeDone() && strand.IsColorChangeDone())
                {
                    float lightSpeed = 0.55f;
                    float colorSpeed = 0.35f;
                    if (!lightChangePatternRan)
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + lightSpeed + Random.Range(0.0f, 0.2f),
                            time + lightSpeed + Random.Range(0.2f, 0.4f),
                            Random.Range(100.0f, 100.0f)
                            };
                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = true;
                    }
                    else if (!colorChangePatternRan)
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + colorSpeed + Random.Range(0.0f, 0.2f)
                            };

                        }
                        strand.ColorChangeAll(pattern);
                        colorChangePatternRan = true;
                    }
                    else
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + lightSpeed + Random.Range(0.0f, 0.2f),
                            time + lightSpeed + Random.Range(0.2f, 0.4f),
                            Random.Range(0.0f, 0.0f)
                            };
                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = false;
                        colorChangePatternRan = false;
                    }
                }
                break;

            // Light Change: On/Off Fast(.35) Random(.2)
            // Color Change: On/Off Faster(.25) Random(.2)
            case 12:
                if (strand.IsLightChangeDone() && strand.IsColorChangeDone())
                {
                    float lightSpeed = 0.35f;
                    float colorSpeed = 0.25f;
                    if (!lightChangePatternRan)
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + lightSpeed + Random.Range(0.0f, 0.2f),
                            time + lightSpeed + Random.Range(0.2f, 0.4f),
                            Random.Range(100.0f, 100.0f)
                            };
                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = true;
                    }
                    else if (!colorChangePatternRan)
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + colorSpeed + Random.Range(0.0f, 0.2f)
                            };

                        }
                        strand.ColorChangeAll(pattern);
                        colorChangePatternRan = true;
                    }
                    else
                    {
                        float[][] pattern = new float[strand.GetNumLightBulbs()][];
                        for (int i = 0; i < pattern.Length; i++)
                        {
                            pattern[i] = new float[] {
                            time + lightSpeed + Random.Range(0.0f, 0.2f),
                            time + lightSpeed + Random.Range(0.2f, 0.4f),
                            Random.Range(0.0f, 0.0f)
                            };
                        }
                        strand.LightChangeAll(pattern);
                        lightChangePatternRan = false;
                        colorChangePatternRan = false;
                    }
                }
                break;

            default:
                break;
        }
    }

    public void SetDifficulty(int i)
    {
        difficulty = i;
        lightChangePatternRan = false;
        colorChangePatternRan = false;
        timingPatternRan = false;
    }
}
