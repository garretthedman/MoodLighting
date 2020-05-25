using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    
    // POINT RULES:
    // Wrong guess = 2 points for deceiver (Can be set in Round Manager)
    // 1 second of deception = 1 pt for deceiver
    // If you run out of guesses, deceiver gets time left points

    
    /// STATS ///
    
    // Determines time of round in seconds
    private int _timeOfRound;
    // Current round
    private int _roundNumber;
    // Determine the number of guess for user
    private int _numberOfGuesses;
    
    // Variables to Track
    private int _numberOfWrongGuesses;
    private int _wrongGuessValue;
    private int _timeEvaded;
    private int _points;

    /// GAME ASSETS ///
    
    // Timer
    private float time;
    private GameObject timer;
    // Parent that is a Round Manager
    private RoundManager _roundManager;
    // Determines if ended or not
    private bool _hasEnded;

    /// PUBLIC TEXT TO CHANGE ///
    public GameObject guessNumberImage;
    public GameObject numberOfPointsText;
    public GameObject roundNumberText;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        // if time is out
        if (time == 0 && !_hasEnded)
        {
            _hasEnded = true;
            EndGame(false);
        }
    }

    public void Initialize(int timeInRound, int numberOfGuesses, int wrongGuessValue, int roundNumber)
    {
        _timeOfRound = timeInRound;
        _numberOfGuesses = numberOfGuesses;
        _wrongGuessValue = wrongGuessValue;
        _numberOfWrongGuesses = 0;
        _timeEvaded = 0;

        _hasEnded = false;
        
        // Update Round Text
        roundNumberText.GetComponent<TextMeshProUGUI>().text = "round " + roundNumber;

        // Adds Timer to Canvas -- Sets Time.
        timer = (GameObject) Instantiate(Resources.Load("Prefabs/Timer"));
        timer.GetComponent<Timer>().Initialize(_timeOfRound, true);

        SetGuessImage(numberOfGuesses);

    }

    public void Wrong_Guess()
    {

        // update wrong guesses
        _numberOfWrongGuesses += 1;
        
        // add points
        AddPoints();
        
        var guessesLeft = _numberOfGuesses - _numberOfWrongGuesses;
        
        SetGuessImage(guessesLeft);
        
        // if player has no more guesses
        if (guessesLeft == 0)
        {
            EndGame(true);
        }
    }

    // Updates stat screen if wrong guess
    public void AddPoints()
    {
        // Guesses left goes down one points go up by value of wrong guess
        _points += _wrongGuessValue;
        var guessesLeft = _numberOfGuesses - _numberOfWrongGuesses;

    }

    public void EndGame(bool outOfGuesses)
    {
        // if player is out of guesses, give them full time points
        if (outOfGuesses)
        {
            _timeEvaded = _timeOfRound;
        }
        
        // else time evaded is total minus time left on clock
        else
        {
            _timeEvaded = _timeOfRound - (int) time;   
        }

        Destroy(timer);

        _roundManager = FindObjectOfType<RoundManager>();

        var turnTrack = _roundManager.GetComponent<RoundManager>()._turnTrack;
        _roundManager.GetComponent<RoundManager>()._evasionTimes[turnTrack] = _timeEvaded;

        _roundManager.GetComponent<RoundManager>().DisplayStats(_numberOfWrongGuesses, _timeEvaded);
    }

    private void SetGuessImage(int numberOfGuesses)
    {
        var guessNumberSpriteImage = guessNumberImage.GetComponent<Image>();

        switch (numberOfGuesses)
        {
            case 1:
                var guessLeftOneImage = Resources.Load<Sprite>("Sprites/guess1");
                guessNumberSpriteImage.sprite = guessLeftOneImage;
                break;
            case 2:
                var guessLeftTwoImage = Resources.Load<Sprite>("Sprites/guess2");
                guessNumberSpriteImage.sprite = guessLeftTwoImage;
                break;
            case 3:
                var guessLeftThreeImage = Resources.Load<Sprite>("Sprites/guess3");
                guessNumberSpriteImage.sprite = guessLeftThreeImage;
                break;
        }
    }
    
    public void NewTime(float timerTime)
    {
        time = timerTime;
    }

    // David's Addition
    public int GetTimeEvaded()
    {
        return _timeEvaded;
    }
}
