using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody2D _rb;
    public Sprite WrenchOpen;
    public Sprite WrenchClosed;
    
    // Boolean to allow use of mouse pad instead of controller
    private bool useMouse;

    RoundManager RoundManager;
    public int turn;

    //private Transform _myTransform;
    // Start is called before the first frame update
    void Start()
    {
        // set to TRUE if you want to use mouse
        useMouse = false;
        
        _rb = GetComponent<Rigidbody2D>();
        RoundManager = FindObjectOfType<RoundManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // useMouse = true;
        MoveWrenchToMousePosition();
        turn = RoundManager.GetComponent<RoundManager>().getTurn();
    }

    void MoveWrenchToMousePosition()
    {
        if (useMouse)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _rb.MovePosition(new Vector3(mousePos.x, mousePos.y, 0));
        }
        
        else
        {
            float mousePosX;
            float mousePosY;

            if (turn == 1)
            {
                mousePosX = Input.GetAxis("HorizontalP2");
                mousePosY = Input.GetAxis("VerticalP2");
            }
            else
            {
                mousePosX = Input.GetAxis("HorizontalP1");
                mousePosY = Input.GetAxis("VerticalP1");
            }
            _rb.MovePosition(new Vector2(_rb.position.x + (mousePosX/5), _rb.position.y - (mousePosY/5)));
        }

        //var newDirection = new Vector3(mousePosX, mousePosY, 0);
        //_myTransform.position += newDirection;

        _rb.freezeRotation = true;
    }

    public void ToggleWrench()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sprite == WrenchClosed)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = WrenchOpen;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = WrenchClosed;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Lightbulb>() != null)
        {
            //gameObject.GetComponent<Target>().ToggleWrench();
            ToggleWrench();
        }


    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Lightbulb>() != null)
        {
            gameObject.GetComponent<Target>().ToggleWrench();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("Hovering over lightbult");


        if (turn == 1 && Input.GetAxis("APlayer2") > 0.5 || turn == 2 && Input.GetAxis("APlayer1") > 0.5)
        {
            col.GetComponent<Lightbulb>().GuessMade();
            Debug.Log("A Button pressed from Target");
        }

    }

}
