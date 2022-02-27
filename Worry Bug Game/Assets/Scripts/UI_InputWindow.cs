using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UI_InputWindow : MonoBehaviour
{


    private Text titleText;
    private TMP_InputField inputField;

    // Start is called before the first frame update
    void Awake()
    {        
        titleText = transform.Find("titleText").GetComponent<Text>();
        inputField = transform.Find("inputField").GetComponent<TMP_InputField>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(string titleString, string inputString){
       gameObject.SetActive(true); 
       titleText.text = titleString;
       inputField.text = inputString;
    }

    public void Hide(){
        gameObject.SetActive(false);
    }

    public string getInput(){
        return inputField.text;
    }
}
