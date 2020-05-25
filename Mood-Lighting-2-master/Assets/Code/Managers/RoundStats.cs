using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using Image = UnityEngine.UI.Image;


public class RoundStats : MonoBehaviour
{

    public GameObject P1R1;
    public GameObject P1R2;
    public GameObject P1R3;
    public GameObject P2R1;
    public GameObject P2R2;
    public GameObject P2R3;
    public GameObject P1Total;
    public GameObject P2Total;
    public GameObject WinText;
    public GameObject P1Winner;
    public GameObject P2Winner;
    public GameObject MenuButton;
    public GameObject ContinueButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Initialize(int[] roundStats, bool finalTurn)
    {
        var p1Round1 = roundStats[0];
        P1R1.GetComponent<TextMeshProUGUI>().text = p1Round1.ToString();
        
        var p2Round1 = roundStats[1];
        P2R1.GetComponent<TextMeshProUGUI>().text = p2Round1.ToString();
        
        var p1Round2 = roundStats[2];
        P1R2.GetComponent<TextMeshProUGUI>().text = p1Round2.ToString();
        
        var p2Round2 = roundStats[3];
        P2R2.GetComponent<TextMeshProUGUI>().text = p2Round2.ToString();
        
        var p1Round3 = roundStats[4];
        P1R3.GetComponent<TextMeshProUGUI>().text = p1Round3.ToString();
        
        var p2Round3 = roundStats[5];
        P2R3.GetComponent<TextMeshProUGUI>().text = p2Round3.ToString();

        var p1RoundTotal = p1Round1 + p1Round2 + p1Round3;
        var p2RoundTotal = p2Round1 + p2Round2 + p2Round3;

        P1Total.GetComponent<TextMeshProUGUI>().text = p1RoundTotal.ToString();
        P2Total.GetComponent<TextMeshProUGUI>().text = p2RoundTotal.ToString();
        
        // hide win condition assets
        WinText.GetComponent<TextMeshProUGUI>().enabled = false;
        P1Winner.GetComponent<Image>().enabled = false;
        P2Winner.GetComponent<Image>().enabled = false;
        MenuButton.GetComponent<Image>().enabled = false;

        if (finalTurn)
        {

            if (p1RoundTotal > p2RoundTotal)
            {
                DisplayWinnerImages(1);
            }
            else if (p2RoundTotal > p1RoundTotal)
            {
                DisplayWinnerImages(2);
            }
            else
            {
                DisplayWinnerImages(3);
            }
        }
    }

    private void DisplayWinnerImages(int winner)
    {
        WinText.GetComponent<TextMeshProUGUI>().enabled = true;
        MenuButton.GetComponent<Image>().enabled = true; 
        ContinueButton.GetComponent<Image>().enabled = false;

        switch (winner)     
        {
            case 1:
                P1Winner.GetComponent<Image>().enabled = true;
                P2Winner.GetComponent<Image>().enabled = false;
                break;
            
            case 2:
                P1Winner.GetComponent<Image>().enabled = false;
                P2Winner.GetComponent<Image>().enabled = true;
                break;
           
            default:
                P1Winner.GetComponent<Image>().enabled = true;
                P2Winner.GetComponent<Image>().enabled = true;
                break;
        }
    }
   
}
