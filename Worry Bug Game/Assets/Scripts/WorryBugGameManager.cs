using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class WorryBugGameManager : MonoBehaviour
{

    [SerializeField] float worryTimeAmount;
    [SerializeField] float affirmationDisplayTimeAmount;
    [SerializeField] string defaultInputMessage = "Enter text here";
    float timeLeft;

    
    private TextMeshProUGUI worryText;
    private TextMeshProUGUI worryTimeDisplay;
    private UI_InputWindow worryinput;
    private UI_InputWindow rephraseWorry;

    private AudioSource burp;
    private AudioSource eat;

    private Image neutral;
    private Image sleepy;
    private Image defeated;
    private Image sendWorry;

    bool gameRunning = false;
    bool rephraseActive = false;
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
        burp = transform.Find("Burp").GetComponent<AudioSource>();
        eat = transform.Find("Eat").GetComponent<AudioSource>();
        neutral = transform.Find("Worry Bug Neutral").GetComponent<Image>();
        sleepy = transform.Find("Worry Bug Sleepy").GetComponent<Image>();
        defeated = transform.Find("Worry Bug Defeated").GetComponent<Image>();
        sendWorry = transform.Find("Send Worry").GetComponent<Image>();
        worryinput.Hide();
        rephraseWorry.Hide();
        rephraseActive = false;
        sleepy.gameObject.SetActive(false);
        defeated.gameObject.SetActive(false);
        sendWorry.gameObject.SetActive(false);
        neutral.gameObject.SetActive(true);
 
    }

    // Update is called once per frame
    void Update()
    {
        if(gameRunning && timeLeft > 0){
            worryTimeDisplay.gameObject.SetActive(true);
            timeLeft -= Time.deltaTime;
            if (gameRunning){
                int minutes = ((int)timeLeft) / 60;
                float seconds = timeLeft - minutes  * 60;
                worryTimeDisplay.text = string.Format("{0}:{1}", minutes, seconds.ToString("00.0"));
            } 
        } else if (gameRunning && timeLeft <=0){
           
                worryinput.Hide();
                worryTimeDisplay.text = "Time's Up";
                rephraseActive = true;
                gameRunning = false;
                if ( worries.Count > 0 ){
                    worryText.text = "Choose to Send your worry to the Bug as is or rephrase your worry into a positive statement";
                    worryTimeDisplay.text = "Example: I can do this";
                    string worry = worries[0];
                    rephraseWorry.Show(worry, "Rephrase into a positive statment");
                }
             
        } else {
            timeLeft -= Time.deltaTime;
            if (!rephraseActive && index < affirmations.Count && timeLeft <= 0) {
                Debug.Log("affirmations[" + index +"]: " + affirmations[index]);
                worryTimeDisplay.text = affirmations[index];
                timeLeft = affirmationDisplayTimeAmount;
                worryTimeDisplay.gameObject.SetActive(true);
                index++;
                worryText.text = "Remember:";
            }
            else if (!rephraseActive && index >= affirmations.Count && timeLeft <= 0) {
                gameRunning = false;
                worryinput.Hide();
                worryTimeDisplay.gameObject.SetActive(false);
                worryText.text = "Press Enter to Start";
            
            } else if (rephraseActive && worries.Count == 0) {
                worryText.text = "You Really Don't have any worries?";
                worryTimeDisplay.text = "Press Enter to add fears and worries to feed the Worry Bug.";
                gameRunning = false;
                worryinput.Hide();
                rephraseWorry.Hide();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (!gameRunning && !rephraseActive){
                StartGame();
            } 
            else if (gameRunning && timeLeft > 0 && !rephraseActive){
                string worry = worryinput.getInput();
                
                if (worry != "" && worry !=defaultInputMessage){
                    worries.Add(worry);
                    worryText.text = worry; 
                }
            } 
            else if (rephraseActive){
                onRephrase();
                if(worries.Count == 0){
                    StartGame();
                }
                
            }
        }
    }

    public void onSendWorry(){
        eat.Play();
        index += 1;
            if (index >= worries.Count){
                gameRunning = false;
                rephraseActive = false;
                rephraseWorry.Hide();
                index = 0;
                sleepy.gameObject.SetActive(false);
                defeated.gameObject.SetActive(true);
                sendWorry.gameObject.SetActive(false);
                neutral.gameObject.SetActive(false);
            } else {
                string worry = worries[index];
                rephraseWorry.Show(worry, "Rephrase into a positive statment");
                sleepy.gameObject.SetActive(false);
                defeated.gameObject.SetActive(false);
                sendWorry.gameObject.SetActive(true);
                neutral.gameObject.SetActive(false);
            }
        
            
    }

    public void onRephrase(){
            string affirmation = rephraseWorry.getInput();
                Debug.Log(affirmation);
                if (affirmation != "" && affirmation != "Rephrase into a positive statment"){
                    affirmations.Add(affirmation);
                    burp.Play();
                    index += 1;
                    if (index >= worries.Count){
                        gameRunning = false;
                        rephraseActive = false;
                        rephraseWorry.Hide();
                        index = 0;
                        sleepy.gameObject.SetActive(false);
                        defeated.gameObject.SetActive(true);
                        sendWorry.gameObject.SetActive(false);
                        neutral.gameObject.SetActive(false);
                    } else {
                        string worry = worries[index];
                        rephraseWorry.Show(worry, "Rephrase into a positive statment");
                        sleepy.gameObject.SetActive(true);
                        defeated.gameObject.SetActive(false);
                        neutral.gameObject.SetActive(false);
                        sendWorry.gameObject.SetActive(false);
                    } 
                }
                
    }

    void StartGame(){
        timeLeft = worryTimeAmount;
            worryTimeDisplay.gameObject.SetActive(true);
            worryText.text = "Feed the Worry Bug!";
            worryinput.Show("What are you worried about?", defaultInputMessage);
            gameRunning = true;
            sleepy.gameObject.SetActive(false);
            defeated.gameObject.SetActive(false);
            neutral.gameObject.SetActive(true);
            sendWorry.gameObject.SetActive(false);
    }
}



