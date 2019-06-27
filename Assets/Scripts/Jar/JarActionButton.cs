using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JarActionButton : MonoBehaviour
{

    public delegate void buttonPressed(ButtonState buttonState);
    public event buttonPressed pressActions;

    public enum ButtonState
    {
        emptying = 1,
        selectingParents = 2,
        startingBreeding = 3,


    }
    private ButtonState buttonState;

    public Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(click);
        buttonState = ButtonState.selectingParents;
        buttonText.text = "Select Parents";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void advanceState(){
        buttonState++;
        if ((int)buttonState == 4) buttonState = ButtonState.emptying;
        switch ((int)buttonState){
            case 1:
                buttonText.text = "Empty Jar";
                break;
            case 2:
                buttonText.text = "Select Parents";
                break;
            case 3:
                buttonText.text = "Start Breeding";
                break;
        }
    }

    void click(){
        pressActions(buttonState);
    }

}
