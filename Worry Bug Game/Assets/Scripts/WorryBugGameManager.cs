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
    private UI_InputWindow rephraseWorry;

    bool gameRunning = false;
    int index; 

    private List<string> worries = new List<string>{};
     private List<string> affirmations = new List<string>{};

    // Start is called before the first frame update
    void Start()
    {
       
        worryText = transform.Find("WorryText").GetComponent<TextMeshProUGUI>();        
        worryTimeDisplay = transform.Find("WorryTimer").GetComponent<TextMeshProUGUI>();
        worryinput = transform.Find("UI_TextInput").GetComponent<UI_InputWindow>();
        rephraseWorry = transform.Find("RephraseWorry").GetComponent<UI_InputWindow>();
        worryinput.Hide();
        rephraseWorry.Hide();
 
    }

    // Update is called once per frame
    void Update()
    {
        if(gameRunning && timeLeft > 0){
            worryTimeDisplay.gameObject.SetActive(true);
            timeLeft -= Time.deltaTime;
                        int minutes = ((int)timeLeft) / 60;
            float seconds = timeLeft - minutes  * 60;
            worryTimeDisplay.text = string.Format("{0}:{1}", minutes, seconds.ToString("00.0"));

        } else if (gameRunning && timeLeft <=0){
            worryinput.Hide();
            worryTimeDisplay.text = "Time's Up";
            if(worries.Count > 0){
                worryText.text = "Choose to Send your worry to the Bug as is or rephrase your worry into a positive statement";
                worryTimeDisplay.text = "Example: I can do this";
                string worry = worries[index];
                rephraseWorry.Show(worry, "Rephrase into a positive statment");
            }
            
        
        } else {
            worryTimeDisplay.text = "0";
            worryTimeDisplay.gameObject.SetActive(false);
            
        }
        
        if(Input.GetKeyDown(KeyCode.Return)) {
            if (!gameRunning){
            timeLeft = worryTimeAmount;
            worryTimeDisplay.gameObject.SetActive(true);
            worryText.text = "Feed the Worry Bug!";
            worryinput.Show("What are you worried about?", "Enter text here");
            gameRunning = true;
            } 
            else if (gameRunning && timeLeft > 0){
                string worry = worryinput.getInput();
                
                if (worry != ""){
                    worries.Add(worry);
                    worryText.text = worry; 
                }
            } 
            else if (gameRunning && timeLeft <=0 && index < worries.Count){
                string affirmation = rephraseWorry.getInput();
                
                if (affirmation != ""){
                    affirmations.Add(affirmation);
                    index += 1;
                    if (index >= worries.Count){
                        gameRunning = false;
                        rephraseWorry.Hide();
                    } 
                }
                
                
            }
        } else if(!gameRunning)  {
            gameRunning = false;
            worryinput.Hide();
            worryTimeDisplay.gameObject.SetActive(false);
            worryText.text = "Press Enter to Start";
        }
    }
}
