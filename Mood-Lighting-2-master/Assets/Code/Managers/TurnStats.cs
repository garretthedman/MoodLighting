using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Script sets text values for UICanvas Turn Stats
/// </summary>

public class TurnStats : MonoBehaviour
{
    
    private GameObject UIBox;
    
    public GameObject wrongGuessNumber;
    public GameObject timeEvadedNumber;
    public GameObject totalNumber;
    public GameObject guessValue;
    public GameObject roundMultiplierValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
    }

    public void Initialize(int numberOfWrongGuesses, int timeEvaded, int wrongGuessValue, int roundNumber)
    {
        
        var totalPoints = numberOfWrongGuesses * wrongGuessValue + timeEvaded * roundNumber;
        
        timeEvadedNumber.GetComponent<TextMeshProUGUI>().text = timeEvaded.ToString();
        roundMultiplierValue.GetComponent<TextMeshProUGUI>().text = "x" + roundNumber;

        wrongGuessNumber.GetComponent<TextMeshProUGUI>().text = numberOfWrongGuesses.ToString();
        guessValue.GetComponent<TextMeshProUGUI>().text = "x" + wrongGuessValue;

        totalNumber.GetComponent<TextMeshProUGUI>().text = totalPoints.ToString();

    }

}
