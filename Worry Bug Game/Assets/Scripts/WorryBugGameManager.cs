using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class WorryBugGameManager : MonoBehaviour
{

    [SerializeField] float worryTimeAmount;
    float timeLeft;

    
    private TextMeshProUGUI worryText;
    private TextMeshProUGUI worryTimeDisplay;
    private UI_InputWindow worryinput;

    bool gameRunning = false;

    private List<string> worries = new List<string>{};

    // Start is called before the first frame update
    void Start()
    {
       
        worryText = transform.Find("WorryText").GetComponent<TextMeshProUGUI>();        
        worryTimeDisplay = transform.Find("WorryTimer").GetComponent<TextMeshProUGUI>();
        worryinput = transform.Find("UI_TextInput").GetComponent<UI_InputWindow>();
        worryinput.Hide();
 
    }

    // Update is called once per frame
    void Update()
    {
        if(gameRunning && timeLeft > 0){
            timeLeft -= Time.deltaTime;
                        int minutes = ((int)timeLeft) / 60;
            float seconds = (timeLeft - minutes ) * 60;
            worryTimeDisplay.text = string.Format("{0}:{1}", minutes, seconds.ToString("00.0"));


        } else {
            worryTimeDisplay.text = "0";
        }
        
        if(Input.GetKeyDown("enter")) {
            if (!gameRunning){
            timeLeft = worryTimeAmount;
            worryText.text = "Feed the Worry Bug!";
            worryinput.Show("What are you worried about?", "Enter text here");
            gameRunning = true;
            } 
            else if (gameRunning){
                string worry = worryinput.getInput();
                worries.Add(worry);
                worryText.text = worry; 
            }
        } else if(!gameRunning)  {
            gameRunning = false;
            worryinput.Hide();
            worryTimeDisplay.gameObject.SetActive(false);
            worryText.text = "Press Enter to Start";
        }
    }
}
