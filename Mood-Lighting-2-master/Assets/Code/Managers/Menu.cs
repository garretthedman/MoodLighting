using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class Menu : MonoBehaviour
{

    public GameObject startLight;
    public GameObject startText;
    public GameObject practiceLight;
    public GameObject practiceText;
    public GameObject quitLight;
    public GameObject quitText;

    private int _menuLocation;
    private int _menuSize;
    
    
    private enum JoystickDirection
    {
        Up, Down, None
    }
    
    private JoystickDirection _joystickDirection;

    void Awake()
    {
        // var buttons = transform.Find("Buttons");
        
        /*startButton = buttons.Find("Start").GetComponent<Button>();
        startButton.onClick.AddListener(StartGame);

        practiceButton = buttons.Find("Options").GetComponent<Button>();
        practiceButton.onClick.AddListener(ShowOptions);
        
        var quitButton = buttons.Find("Quit").GetComponent<Button>();
        quitButton.onClick.AddListener(QuitGame);*/
    }

    // Start is called before the first frame update
    void Start()
    {
        _menuLocation = 0;
        _menuSize = 3;
        _joystickDirection = JoystickDirection.None;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    // Method called when "Start" button clicked
    private void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    // Method called when "Option" button clicked
    private void PracticeGame()
    {
        SceneManager.LoadScene("Playground");
    }
    
    // Method called when "Quit" button clicked
    private void QuitGame () {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
    private void HandleInput()
    {
        var joystickInput = (int) Input.GetAxis("BrightnessP1");
        var aButtonInput = (int) Input.GetAxis("APlayer1");

        if (aButtonInput == 1)
        {
            GoToScene();
        }
        
        // Joystick is DOWN
        if (joystickInput < 0 && _joystickDirection != JoystickDirection.Down)
        {
            _joystickDirection = JoystickDirection.Down;
            MoveMenuLocation(-1);
        }
        
        // Joystick is UP
        if (joystickInput > 0 && _joystickDirection != JoystickDirection.Up)
        {
            _joystickDirection = JoystickDirection.Up;
            MoveMenuLocation(1);
        }
        
        if (joystickInput == 0 )
        {
            _joystickDirection = JoystickDirection.None;
        }
    }
    
    // A Button Response
    private void GoToScene()
    {
        switch (_menuLocation)
        {
            case 0:
                StartGame();
                break;
            case 1:
                PracticeGame();
                break;
            case 2:
                QuitGame();
                break;
            default:
                break;
        }
    }

    // Joystick Response
    private void MoveMenuLocation(int moveDirection)
    {
        switch (moveDirection)
        {
            // Going UP
            case 1 when _menuLocation != 0:
                _menuLocation += -1;
                UpdateAssets();
                break;
            
            // Going DOWN
            case -1 when _menuLocation < (_menuSize - 1):
                _menuLocation += 1;
                UpdateAssets();
                break;
            
            default:
                break;
        }
    }

    private void UpdateAssets()
    {
        // Default Images and Colors
        var bulbOn = Resources.Load<Sprite>("Sprites/menuLightOn");
        var bulbOffImage = Resources.Load<Sprite>("Sprites/menuLightOff");
        var greyColor = Color.gray;
        var whiteColor = Color.white;

        // Default Image - bulbOffImage
        var startLightSpriteRenderer = startLight.GetComponent<UnityEngine.UI.Image>();
        startLightSpriteRenderer.overrideSprite = bulbOffImage;

        var practiceLightSpriteRenderer = practiceLight.GetComponent<UnityEngine.UI.Image>();
        practiceLightSpriteRenderer.overrideSprite = bulbOffImage;
        
        var quitLightSpriteRenderer = quitLight.GetComponent<UnityEngine.UI.Image>();
        quitLightSpriteRenderer.overrideSprite = bulbOffImage;
        
        // Set to Default Color - greyColor
        var startTextMeshProUGUI = startText.GetComponent<TextMeshProUGUI>();
        startTextMeshProUGUI.color = greyColor;
        
        var practiceTextMeshProUGUI = practiceText.GetComponent<TextMeshProUGUI>();
        practiceTextMeshProUGUI.color = greyColor;

        var quitTextMeshProUGUI = quitText.GetComponent<TextMeshProUGUI>();
        quitTextMeshProUGUI.color = greyColor;
        
        // Turn On Selected
        switch (_menuLocation)
        {
            case 0:
                startLightSpriteRenderer.overrideSprite = bulbOn;
                startTextMeshProUGUI.color = whiteColor;
                break;
            case 1:
                practiceLightSpriteRenderer.overrideSprite = bulbOn;
                practiceTextMeshProUGUI.color = whiteColor;
                break;
            case 2:
                quitLightSpriteRenderer.overrideSprite = bulbOn;
                quitTextMeshProUGUI.color = whiteColor;
                break;
            default:
                break;
        }

    }
}
