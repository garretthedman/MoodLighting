using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Selector : MonoBehaviour
{

    private Transform _myTransform;
    private bool didSelect;
    RoundManager RoundManager;
    private int turn;
    private float horizontalAxis;

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        didSelect = false;

        RoundManager = FindObjectOfType<RoundManager>();

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        turn = RoundManager.GetComponent<RoundManager>().getTurn();
    }
    
    private void HandleInput()
    {
        if (turn == 1)
        {
            horizontalAxis = Input.GetAxis("SelectorP1");
        }
        else
        {
            horizontalAxis = Input.GetAxis("SelectorP2");
        }
            

        //var verticalAxis = Input.GetAxis("Vertical");

       // horizontalAxis *= 0.1f;
        
        // verticalAxis *= -0.1f;

        // Edge values in which we don't want cursor to move
        
       /* if (horizontalAxis > 0 && horizontalAxis < 0.05f)
        {horizontalAxis = 0;}
        
        if (horizontalAxis < 0 && horizontalAxis > -0.05f)
        {horizontalAxis = 0;}*/

        horizontalAxis = horizontalAxis / 5;
        
        // Uncomment for change to vertical axis
        /* if (verticalAxis > 0 && verticalAxis < 0.05f)
        {verticalAxis = 0;}
        
        if (verticalAxis < 0 && verticalAxis > -0.05f)
        {verticalAxis = 0;}*/
        
        // move selection tool according to axis; change 0 to verticalAxis if want to enable vertical
        var newDirection = new Vector3(horizontalAxis, 0, 0);
        _myTransform.position += newDirection;
        
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        float selectPlayer;
        if (turn == 1)
        {
            selectPlayer = Input.GetAxis("APlayer1");
        }
        else
        {
            selectPlayer = Input.GetAxis("APlayer2");
        }

        if (selectPlayer == 1 && !didSelect)
        {
            //Debug.Log("ColorChange button pressed!");
            other.GetComponent<Lightbulb>().SetPlayer();

            didSelect = true;
            
            //Find the game and call the next method
            var roundManager = FindObjectOfType<RoundManager>();
            roundManager.GetComponent<RoundManager>().StartCountown();
            
            Destroy(this.gameObject);
        }
        
    }
}
