using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Round manager controls the flow of rounds and keeps track of round data
/// </summary>

public class RoundManager : MonoBehaviour
{
   
    //////// GAMEPLAY SET VARIABLES ////////
    
    // Time of round in seconds
    private int _timeOfRound;
    // Number of guesses for user
    private int _numberOfGuesses;
    // Number of rounds for players
    private int _numberOfRounds;
    // Time of light getting used to pattern and person closing eyes
    private int _eyesClosedTime;
    // Time player gets to open their eyes and see what's going on
    private float _eyesOpenTime;
    // How much each wrong guess is worth
    private int _wrongGuessValue;

    private AudioSource source;
    public AudioClip start_find;
    
    //////// VARIABLES THAT CHANGE WITH ROUND ////////
    
    // Number of rounds remaining
    // private int _currentRound;
    // Turn number of round (either 1 or 2)
    private int _turn;
   
    //////// ASSETS ////////
    
    // Game Object to hold instructions to diplay
    private GameObject _instructions;
    // Game Object to display timer for coundown
    private GameObject _timer;
    // GameObject - strand of lights
    private GameObject _strandOfLights;
    // GameObject - wrench
    private GameObject _wrench;
    // GameObject - select instructions
    private GameObject _selectInstructions;
    // Manager to keep track of points during play
    private GameObject _scoreManager;



    //////// STATS ////////

    // Game Object to show stats after turn and round
    private GameObject _statBox;
    private GameObject _statBoxRound;
    // Enum to show what display is up
    enum DisplayName
    {
        none, turnStat, roundStat
    }
    // Enum to track what display is up
    private DisplayName _myDisplay;
    // Array of all the round stats
    private int[] _roundStats;
    // Number that tracks which part of the array to add to
    public int _turnTrack;
    // Array of the evasionTimes for every round
    public int[] _evasionTimes;

    /// <summary>
    /// METHODS
    /// </summary>
    ///

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Only handle input if display is up
        if (_myDisplay != DisplayName.none)
        {
            HandleDisplay();
        }
        
    }

    public int getTurn()
    {
        return _turn;
    }

    public void Instantiate(int timeInRound, int numberOfGuesses, int numberOfRounds, int eyesClosedTime)
    {
        _timeOfRound = timeInRound;
        _numberOfGuesses = numberOfGuesses;
        _numberOfRounds = numberOfRounds;
        _eyesClosedTime = eyesClosedTime;
        _eyesOpenTime = 1.5f;
        _wrongGuessValue = 2;
        _turnTrack = 0;

        // Assumes 3 rounds right now
        _roundStats = new int[6];
        _evasionTimes = new int[6];

        _turn = 1;
        // _currentRound = 1;
        
        StartRound();
    }

    /// <summary>
    /// GAME PROGRESSION METHODS
    /// </summary>

    // Round set up methods
    private void ResetForNewRound()
    {
        switch (_myDisplay)
        {
            case DisplayName.roundStat:
                Destroy(_statBoxRound);
                break;
            
            case DisplayName.turnStat:
                Destroy(_statBox);
                break;
            
            case DisplayName.none:
                Debug.Log("Called none (this shouldn't happen)'");
                break;
            
            default:
                Debug.Log("Called without stats");
                break;
        }

        //take off all displays
        _myDisplay = DisplayName.none;

        // Swap which turn it is
        _turn = _turn == 1 ? 2 : 1;
        
        // Destroy(_scoreManager);
        Destroy(_strandOfLights);
        StartRound();
        
    }
    
    // Called when the beginning of a round starts
    public void StartRound()
    {
        _strandOfLights = (GameObject) Instantiate(Resources.Load("Prefabs/Strand"));
        if (RoundNumber() == 1)
        {
            _strandOfLights.GetComponent<AIManager>().SetDifficulty(2);
        }
        else
        {
            int difficultyOffset = 0;
            for (int i = _turn-1; i < _evasionTimes.Length; i += 2)
            {
                if (i == _turnTrack) break;
                difficultyOffset += DifficultyOffset(_evasionTimes[i]);
            }
            _strandOfLights.GetComponent<AIManager>().SetDifficulty(2*RoundNumber() + difficultyOffset);
        }
        

         Instantiate(Resources.Load("Prefabs/Finger"));
         
         // Add select button instructions and finger instsructions to screen
         _selectInstructions = (GameObject) Instantiate(Resources.Load("Prefabs/Select"));
        _instructions = (GameObject) Instantiate(Resources.Load("Prefabs/Instructions"));
        
    }

    // Called from selector after selecting object
    public void StartCountown()
    {
        
        Destroy(_selectInstructions);
        
        //don't do anything yet
        var instructions2 = Resources.Load<Sprite>("Sprites/instructions2");
        _instructions.GetComponent<SpriteRenderer>().sprite = instructions2;

        // Adds Timer Canvas -- Be sure to set time on canvas.
        _timer = (GameObject) Instantiate(Resources.Load("Prefabs/Timer"));
        _timer.GetComponent<Timer>().Initialize(_eyesClosedTime, false);

    }
    
    // Called from timer after timer reaches zero
    public void StartFind()
    {
        // Add audio
        source.PlayOneShot(start_find, 1);

        // Add select instructions
        
        var instructions3 = Resources.Load<Sprite>("Sprites/instructions3");
        _instructions.GetComponent<SpriteRenderer>().sprite = instructions3;

        IEnumerator openEyesCoroutine = OpenEyes(_eyesOpenTime);
        StartCoroutine(openEyesCoroutine);
        
    }

    private IEnumerator OpenEyes(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        // Add wrench to the screen
        _wrench = (GameObject)Instantiate(Resources.Load("Prefabs/Wrench"));
       
        // Add Score to Screen
        _scoreManager = (GameObject) Instantiate(Resources.Load("Prefabs/ScoreManager"));
        _scoreManager.GetComponent<ScoreManager>().Initialize(_timeOfRound, _numberOfGuesses, _wrongGuessValue, RoundNumber());
        
        // Add Select instructions to screen
        _selectInstructions = (GameObject) Instantiate(Resources.Load("Prefabs/Select"));

        Destroy(_instructions.gameObject);
        Destroy(_timer.gameObject);
    }
    
    /// <summary>
    /// METHODS FOR RELATED TO THE DISPLAYING STATS
    /// </summary>
    
   public void DisplayStats(int wrongGuesses, int timeEvaded)
    {
        //Debug.Log("Display stats");
        // Destory the _wrench object
        Destroy(_wrench);
        Destroy(_scoreManager);
        Destroy(_selectInstructions);

        
        Lightbulb[] lightBox = FindObjectsOfType<Lightbulb>();
        
        foreach (Lightbulb light in lightBox)
        {
            light.GetComponent<Lightbulb>().KillAll();
        }
        
        _statBox = (GameObject) Instantiate(Resources.Load("Prefabs/TurnStats"));
        _statBox.GetComponent<TurnStats>().Initialize(wrongGuesses, timeEvaded, _wrongGuessValue, RoundNumber());
        
        // update Round Manager stats
        var turnStat = wrongGuesses * _wrongGuessValue + timeEvaded;
        _roundStats[_turnTrack] = turnStat;
        _turnTrack += 1;

        _myDisplay = DisplayName.turnStat;
    }

    private void DisplayRoundStats()
    {
        var isFinalRound = _turnTrack == 6;

        _myDisplay = DisplayName.roundStat;
        _statBoxRound = (GameObject) Instantiate(Resources.Load("Prefabs/RoundStats"));
        _statBoxRound.GetComponent<RoundStats>().Initialize(_roundStats, isFinalRound);
        
    }
    
    private void HandleDisplay()
    {
        if (Input.GetButtonDown("B"))
        {
            // If it is the second turn show round totals after stats; otherwise reset.
            if (_myDisplay == DisplayName.turnStat && _turn == 2)
            {
                Destroy(_statBox);
                DisplayRoundStats();
            }

            else
            {
                if (RoundNumber() == 0)
                {
                    SceneManager.LoadScene("Main");
                }
                else
                {
                    ResetForNewRound();
                }
            }
        }
    }
    int RoundNumber()
    {
        switch (_turnTrack)
        {
            case 0:
            case 1:
                return 1;
            case 2:
            case 3:
                return 2;
            case 4:
            case 5:
                return 3;
            default:
                return 0;
        }
    }

    /// <summary>
    /// METHODS FOR ADAPTIVE DIFFICULTY
    /// </summary>
    private int DifficultyOffset(int evasionTime)
    {
        int offset;

        if (evasionTime >= 14) offset = 3;
        else if (evasionTime >= 12) offset = 2;
        else if (evasionTime >= 10) offset = 1;
        else if (evasionTime >= 8) offset = 0;
        else if (evasionTime >= 6) offset = -1;
        else if (evasionTime >= 4) offset = -2;
        else offset = -3;
        return offset;
    }
}
