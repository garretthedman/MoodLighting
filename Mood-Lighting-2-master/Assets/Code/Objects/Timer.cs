using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // to set time change time variable
    public float time;
    
    // determine who is calling timer
    private bool _isScoreManager;
    
    // Child of timer and its TextMesh
    public GameObject countdownTimerText ;
    public GameObject roundTimerText;

    private TextMeshProUGUI _countdownTextMesh;
    private TextMeshProUGUI _roundTextMesh;

    // Start is called before the first frame update
    void Start()
    {
        // Assign text to TextMeshProUGUI of TimeText
        _countdownTextMesh = countdownTimerText.GetComponent<TextMeshProUGUI>();
        _roundTextMesh = roundTimerText.GetComponent<TextMeshProUGUI>();
        // Call Countdown every second

    }
    

    // Update is called once per frame
    void Update()
    {
    }

    // Class should be initialized with set time -- this also start countdown once time is set.
    public void Initialize(int setTime, bool isScoreManager)
    {
        time = setTime;
        _isScoreManager = isScoreManager;
        InvokeRepeating("Countdown", 0-.0f,1.0f);

        // hide countdownText if in game (or ScoreManager)
        if (isScoreManager)
        {
            countdownTimerText.SetActive(false);
            roundTimerText.SetActive(true);
        }
        else
        {
            countdownTimerText.SetActive(true);
            roundTimerText.SetActive(false);
        }
        
    }    
    
    void Countdown()
    {
        if (time > 0)
        {
            _countdownTextMesh.text = string.Format("{0}", time);
            _roundTextMesh.text = string.Format("{0}", time);
            
            // Update Score Manager with Time
            if (_isScoreManager)
            {
                var scoreManager = FindObjectOfType<ScoreManager>();
                scoreManager.GetComponent<ScoreManager>().NewTime(time);
            }
            
            time -= 1;

        }

        else 
        {
            _countdownTextMesh.text = string.Format("", time);
            _roundTextMesh.text = string.Format("", time);

            if (!_isScoreManager)
            {
                var roundManager = FindObjectOfType<RoundManager>();
                roundManager.GetComponent<RoundManager>().StartFind();
            }

            else
            {
                var scoreManager = FindObjectOfType<ScoreManager>();
                scoreManager.GetComponent<ScoreManager>().NewTime(time);
            }
            CancelInvoke();
        }
    }
}
