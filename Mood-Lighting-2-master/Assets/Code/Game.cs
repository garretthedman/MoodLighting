using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Game : MonoBehaviour
{
    private GameObject _roundManager;

    private int _eyesClosedTime;
    private int _timeInRound;
    private int _numberOfGuesses;
    private int _numberOfRounds;

    // Start is called before the first frame update
    void Start()
    {
        _eyesClosedTime = 2;
        _timeInRound = 15;
        _numberOfGuesses = 2;
        _numberOfRounds = 3;
        
        StartGame();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void StartGame()
    {
        _roundManager = (GameObject) Instantiate(Resources.Load("Prefabs/RoundManager"));
        _roundManager.GetComponent<RoundManager>().Instantiate(_timeInRound, _numberOfGuesses, _numberOfRounds, _eyesClosedTime);
    }

}
