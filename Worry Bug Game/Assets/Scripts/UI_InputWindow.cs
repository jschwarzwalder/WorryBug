using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Input : MonoBehaviour
{

    // private Button_UI okBtn;
    // private Button_UI cancelBtn;

    private TextMeshProUGUI titleText;
    private TMP_InputField inputField;

    // Start is called before the first frame update
    void Awake()
    {
        
       // okBtn = transform.Find("okBtn").GetComponent<Button_UI>();
       // cancelBtn =transform.Find("cancelBtn").GetComponent<Button_UI>();
        titleText = transform.Find("titleText").GetComponent<TextMeshProUGUI>();
        inputField = transform.Find("inputField").GetComponent<TMP_InputField>();
        Hide();
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
}
